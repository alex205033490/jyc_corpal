using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Drawing;
using System.IO;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CallCenter_Eventos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(57) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            lb_eventos.Text = "Eventos " + Session["BaseDatos"].ToString();
            tx_BaseDeDatos.Text = Session["BaseDatos"].ToString();        
           if(!IsPostBack){
               //negarAreaCliente();
               buscarEventos(0,"","","", "Abierto","","");
               llenarTipoEvento();
               llenarPrioridad();
           }
            
        }

        public void llenarPrioridad()
        {
            NA_Prioridad nprioridad = new NA_Prioridad();
            dd_prioridad.DataSource = nprioridad.getAllPrioridad();
            dd_prioridad.DataValueField = "codigo";
            dd_prioridad.DataTextField = "nombre";
            // dd_tipoEvento.Items.Add(new ListItem(""));
            dd_prioridad.AppendDataBoundItems = true;
            // ddlAnio.SelectedIndex = -1;
            dd_prioridad.DataBind();

        }

      /*  private void negarAreaCliente()
        {
             if (tienePermisoDeIngreso(67) == true)
            {
                ckb_areaCliente.Enabled = true;                
            } else
                 ckb_areaCliente.Enabled = false;                
           
        }
        */

        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(permiso, codUser);
        }

        public string convertidorFecha(string fecha)
        {
            if (fecha != "" )
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'" + _fecha + "'";
            }
            else
                return "null";
        }


        public void ponerColoresAequiposConPrioridad(GridView gv_tablaDatosAux)
        {   
            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++)
            {
                string Prioridad = gv_tablaEventos.Rows[i].Cells[10].Text;
                if (Prioridad.Equals("Alta"))
                {
                    gv_tablaDatosAux.Rows[i].BackColor = Color.Red;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.White;
                }else
                    if (Prioridad.Equals("Media"))
                    {
                        gv_tablaDatosAux.Rows[i].BackColor = Color.Yellow;
                        gv_tablaDatosAux.Rows[i].ForeColor = Color.Black;
                    }else
                        if(Prioridad.Equals("Baja")){
                            gv_tablaDatosAux.Rows[i].BackColor = Color.Green;
                            gv_tablaDatosAux.Rows[i].ForeColor = Color.Black;
                        }
                gv_tablaDatosAux.Rows[i].Cells[0].BackColor = Color.White;
                gv_tablaDatosAux.Rows[i].Cells[0].ForeColor = Color.Black;
            }
        }


        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaResponsable2(string prefixText, int count)
        {
            string nombreResponsable = prefixText;

            NA_Responsables Nrespon = new NA_Responsables();
            DataSet tuplas = Nrespon.mostrarTodos_AutoComplit(nombreResponsable);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }


        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProyectos5(string prefixText, int count)
        {
            string nombreProyecto = prefixText;

            NProyecto proyectoN = new NProyecto();
            DataSet tuplas = proyectoN.buscador2(nombreProyecto);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }




        public void buscarEventos(int tiket,string nombreEdificio, string nombretipoLLamada, string semana, string estado, string fechaDesde, string fechaHasta)
        {
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = Nevento.getAllEventos(tiket,nombreEdificio, nombretipoLLamada, semana, estado,0, fechaDesde,  fechaHasta);
            gv_tablaEventos.DataSource = resultado;
            gv_tablaEventos.DataBind();
            tx_cantidadEventos.Text = gv_tablaEventos.Rows.Count.ToString();
            ponerColoresAequiposConPrioridad(gv_tablaEventos);
            
        }


        public void llenarTipoEvento()
        {
            NA_TipoEvento NtipoEvento = new NA_TipoEvento();
            dd_tipoEvento1.DataSource = NtipoEvento.mostrarAllDatos();
            dd_tipoEvento1.DataValueField = "codigo";
            dd_tipoEvento1.DataTextField = "nombre";
            dd_tipoEvento1.Items.Add(new ListItem(""));
            dd_tipoEvento1.AppendDataBoundItems = true;
            dd_tipoEvento1.SelectedIndex = -1;
            dd_tipoEvento1.DataBind();

        }

        public void llenarDatos() {
            lb_numeroEvento.Text = gv_tablaEventos.SelectedRow.Cells[1].Text;
            lb_numeroSemana.Text = gv_tablaEventos.SelectedRow.Cells[2].Text;
            tx_fechaEvento.Text = gv_tablaEventos.SelectedRow.Cells[4].Text;
            tx_horaEvento.Text = gv_tablaEventos.SelectedRow.Cells[3].Text;
            tx_Regional.Text = Session["BaseDatos"].ToString();

            int codigoEvento = Convert.ToInt32(gv_tablaEventos.SelectedRow.Cells[1].Text);
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = Nevento.getEvento(codigoEvento);
            tx_tipoEvento.Text = gv_tablaEventos.SelectedRow.Cells[9].Text;
            tx_telefono.Text = resultado.Tables[0].Rows[0][5].ToString();
            tx_celular.Text = resultado.Tables[0].Rows[0][6].ToString();
            tx_Cliente.Text = resultado.Tables[0].Rows[0][4].ToString();
            tx_edificio.Text = resultado.Tables[0].Rows[0][7].ToString();
            tx_Direccion.Text = resultado.Tables[0].Rows[0][8].ToString();
            tx_Ascensores.Text = resultado.Tables[0].Rows[0][9].ToString();
            dd_estadoEvento.SelectedValue = resultado.Tables[0].Rows[0][10].ToString();
            tx_observacion.Text = resultado.Tables[0].Rows[0][11].ToString();

           // tx_defectoConstatadoEvento.Text = resultado.Tables[0].Rows[0][12].ToString();
           // tx_observacionNecesidadRepuestoEvento.Text = resultado.Tables[0].Rows[0][13].ToString();
           // tx_solicitudRepuestoEvento.Text = resultado.Tables[0].Rows[0][14].ToString();
           // tx_envioProformaEvento.Text = resultado.Tables[0].Rows[0][15].ToString();
           // tx_AceptacionProformaEvento.Text = resultado.Tables[0].Rows[0][16].ToString();
           // tx_verificacionCambioEvento.Text = resultado.Tables[0].Rows[0][17].ToString();
            tx_observacionEvento.Text = resultado.Tables[0].Rows[0][22].ToString();
            dd_estadoEvento.SelectedValue = resultado.Tables[0].Rows[0][10].ToString();
            string ascensorParado = resultado.Tables[0].Rows[0][20].ToString();
            CheckBox1_AscensorParado.Checked = Convert.ToBoolean(ascensorParado);      
            string personasAtrapadas = resultado.Tables[0].Rows[0][21].ToString();
            CheckBox2_PersonasAtrapadas.Checked = Convert.ToBoolean(personasAtrapadas);

            string Prioridad = resultado.Tables[0].Rows[0][23].ToString();
            dd_prioridad.SelectedValue = Prioridad;

        }


        protected void bt_Buscar_Click(object sender, EventArgs e)
        {
            string fechaDesde = convertidorFecha(tx_desdeBusqueda.Text);
            if (fechaDesde.Equals("null"))
                fechaDesde = "";

            string fechaHasta = convertidorFecha(tx_hastaBusqueda.Text);
            if (fechaHasta.Equals("null"))
                fechaHasta = "";
            int tiket = 0;
            if(!tx_tiket.Text.Equals("")){
            tiket = Convert.ToInt32(tx_tiket.Text);
            }

            buscarEventos(tiket,tx_nombreEdificioBusqueda.Text, dd_tipoEvento1.SelectedItem.Text,tx_SemanaBusqueda.Text,dd_evento.SelectedItem.Text,fechaDesde,fechaHasta);
        }

        protected void gv_tablaEventos_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDatos();
            llenarComboBoxTecnicoAsignado();
            llenarDatosSupervisor();
            limpiarDatosTecnico();
            ponerColoresAequiposConPrioridad(gv_tablaEventos);
           if(gv_tablaEventos.SelectedIndex > -1){
               gv_tablaEventos.SelectedRow.BackColor = Color.Lime;
           }
        }

        protected void bt_AsignarTecnico_Click(object sender, EventArgs e)
        {
            AsignarTecnico();
            llenarComboBoxTecnicoAsignado();
            limpiarDatosTecnico();
        }

        private void AsignarTecnico()
        {
            string nombreTecnico = tx_tecnicoAsignado.Text;
            NA_Responsables Nresp = new NA_Responsables();
            int codigoTecnico = Convert.ToInt32(Nresp.getResponsable_SinExepcion(nombreTecnico).Tables[0].Rows[0][0].ToString());
            int codigoEvento = Convert.ToInt32(lb_numeroEvento.Text);
            NA_DetalleEventoTecnico nDetalleEventoTecnico = new NA_DetalleEventoTecnico();
            nDetalleEventoTecnico.insertar(codigoEvento, codigoTecnico,0);
            //-----------------------historial            
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            NA_Historial nhistorial = new NA_Historial();
            nhistorial.insertar(codUser, "CallCenter Evento tiket = " + lb_numeroEvento.Text + " Agrego un Tecnico Mantenimiento = " + nombreTecnico);
            //-----------------------historial----

            Response.Write("<script type='text/javascript'> alert('El Tecnico ha sido Asignado Exitosamente') </script>");
        }

        private void llenarComboBoxTecnicoAsignado() {
            dd_tecnicosAsignados.Items.Clear();
            int codigoEvento = Convert.ToInt32(lb_numeroEvento.Text);
            NA_DetalleEventoTecnico Ndetalle = new NA_DetalleEventoTecnico();
            dd_tecnicosAsignados.DataSource = Ndetalle.getAllResponsablesAsignados(codigoEvento,0);
            dd_tecnicosAsignados.DataValueField = "codigo";
            dd_tecnicosAsignados.DataTextField = "nombre";
            dd_tecnicosAsignados.Items.Add(new ListItem(""));
            dd_tecnicosAsignados.AppendDataBoundItems = true;
            dd_tecnicosAsignados.SelectedIndex = -1;
            dd_tecnicosAsignados.DataBind();
        }

        protected void bt_AsignarSupervisor_Click(object sender, EventArgs e)
        {
            asignarSupervisor_ok();
            llenarDatosSupervisor();
            limpiarDatosTecnico();
        }

        private void asignarSupervisor_ok()
        {

            int codigoEvento = Convert.ToInt32(lb_numeroEvento.Text);
            NA_DetalleEventoTecnico nDetalleEventoTecnico = new NA_DetalleEventoTecnico();
            if (!nDetalleEventoTecnico.existeSupervisor(codigoEvento))
            { ///-------- verificar primero si ya inserto Supervisor
                string nombreTecnico = tx_SupervisorAsignado.Text;
                NA_Responsables Nresp = new NA_Responsables();
                int codigoTecnico = Convert.ToInt32(Nresp.getResponsable_SinExepcion(nombreTecnico).Tables[0].Rows[0][0].ToString());
                nDetalleEventoTecnico.insertar(codigoEvento, codigoTecnico, 1);

                //-----------------------historial                
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                NA_Historial nhistorial = new NA_Historial();
                nhistorial.insertar(codUser, "CallCenter Evento tiket = " + lb_numeroEvento.Text + " Agrego un Supervisor = "+nombreTecnico);
                //----------------- historial----
                Response.Write("<script type='text/javascript'> alert('El Supervisor ha sido Asignado Exitosamente') </script>");
            }
            else { 
                //Response.Write("<script type='text/javascript'> alert('Ya tiene Supervisor Asignado') </script>");
                string nombreTecnico = tx_SupervisorAsignado.Text;
                NA_Responsables Nresp = new NA_Responsables();
               // int codigoTecnico = Convert.ToInt32(Nresp.getResponsable(nombreTecnico).Tables[0].Rows[0][0].ToString());
                int codigoSupervisorAsignado = Convert.ToInt32(tx_codDetalleTecnico.Text);
                string fechaAsignacion = convertidorFecha(tx_fechaSupervisorAsignado.Text);
                string horaAsignacion = "'" + tx_horaSupervisorAsignado.Text + "'";

                nDetalleEventoTecnico.ModifiarDatosSupervisor2(codigoEvento, codigoSupervisorAsignado, fechaAsignacion, horaAsignacion, 1);
                Response.Write("<script type='text/javascript'> alert('El Supervisor ha sido Asignado Exitosamente') </script>");
            }              
        }

        public void llenarDatosSupervisor() {
            
            int codigoEvento = Convert.ToInt32(lb_numeroEvento.Text);
            NA_DetalleEventoTecnico Ndetalle = new NA_DetalleEventoTecnico();
            DataSet resultado = Ndetalle.getAllResponsablesAsignados(codigoEvento, 1);
            if (resultado.Tables[0].Rows.Count > 0)
            {
                tx_codDetalleTecnico.Text = resultado.Tables[0].Rows[0][0].ToString();
                tx_SupervisorAsignado.Text = resultado.Tables[0].Rows[0][1].ToString();
                tx_horaSupervisorAsignado.Text = resultado.Tables[0].Rows[0][2].ToString();
                tx_fechaSupervisorAsignado.Text = resultado.Tables[0].Rows[0][3].ToString();
            }
            else {
                tx_codDetalleTecnico.Text = "";
                tx_SupervisorAsignado.Text = "";
                tx_horaSupervisorAsignado.Text = "";
                tx_fechaSupervisorAsignado.Text = "";
            }
        
        }

        protected void dd_tecnicosAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDatosTecnicoSeleccionado();
        }

        private void llenarDatosTecnicoSeleccionado()
        {
            int codigoEvento = Convert.ToInt32(lb_numeroEvento.Text);
          //  int codigoTecnico = Convert.ToInt32(dd_tecnicosAsignados.SelectedValue.ToString());
            int codigoTecnicoAsignado = Convert.ToInt32(dd_tecnicosAsignados.SelectedValue.ToString());
            NA_DetalleEventoTecnico Ndetalle = new NA_DetalleEventoTecnico();
            DataSet resultado = Ndetalle.getAllResponsablesAsignados2(codigoEvento,codigoTecnicoAsignado, 0);           
            tx_HoraTecnicoAsignacion.Text = resultado.Tables[0].Rows[0][2].ToString();
            tx_FechaTecnicoAsignacion.Text = resultado.Tables[0].Rows[0][3].ToString();
            tx_horallegadaAlEdificioTecnico.Text = resultado.Tables[0].Rows[0][4].ToString();
            tx_horaSalidadelEdificioTecnico.Text = resultado.Tables[0].Rows[0][5].ToString();
            dd_EstadoLLegadaSalidaTecnico.SelectedValue = resultado.Tables[0].Rows[0][6].ToString();
            tx_observacionTecnico.Text = resultado.Tables[0].Rows[0][7].ToString();
            tx_nroboleta.Text = resultado.Tables[0].Rows[0][8].ToString();
            tx_ascensorReparado.Text = resultado.Tables[0].Rows[0][9].ToString();
            cbx_trabajoprogramado.Checked = Convert.ToBoolean(resultado.Tables[0].Rows[0][10].ToString());
            tx_fechahorallegada.Text = resultado.Tables[0].Rows[0][11].ToString();
            tx_fechahorasalida.Text = resultado.Tables[0].Rows[0][12].ToString();
            tx_horaReporte.Text = resultado.Tables[0].Rows[0][13].ToString();
            tx_fechaReporte.Text = resultado.Tables[0].Rows[0][14].ToString();

        }

        private void limpiarDatosTecnico() {
            dd_tecnicosAsignados.SelectedIndex = -1;
            tx_FechaTecnicoAsignacion.Text = "";
            tx_HoraTecnicoAsignacion.Text = "";            
            tx_FechaTecnicoAsignacion.Text = "";
            tx_horallegadaAlEdificioTecnico.Text = "";
            tx_horaSalidadelEdificioTecnico.Text = "";
            dd_EstadoLLegadaSalidaTecnico.SelectedIndex = -1;
            tx_observacionTecnico.Text = "";
            tx_nroboleta.Text = "";
            tx_tiket.Text = "";
            tx_ascensorReparado.Text = "";
            cbx_trabajoprogramado.Checked = false;
            tx_fechahorallegada.Text = "";
            tx_fechahorasalida.Text = "";
            tx_horaReporte.Text = "";
            tx_fechaReporte.Text = "";
        }
   

        protected void bt_datosTecnicos_Click(object sender, EventArgs e)
        {
            guardarDatosTecnicos();            
        }

        private void guardarDatosTecnicos()
        {
            int codigoEvento = Convert.ToInt32(lb_numeroEvento.Text);
            int codigoEventoTecnicoAsignado = Convert.ToInt32(dd_tecnicosAsignados.SelectedValue.ToString());
           
            NA_DetalleEventoTecnico Ndetalle = new NA_DetalleEventoTecnico();
            string horaAsignacion = tx_HoraTecnicoAsignacion.Text ;
            string fechaAsignacion = convertidorFecha(tx_FechaTecnicoAsignacion.Text);
            
            string horallegadaAlEdificio = tx_horallegadaAlEdificioTecnico.Text;
            string fecha_horallegadaEdificio = convertidorFecha(tx_fechahorallegada.Text);
            string horaSalidadelEdificio = tx_horaSalidadelEdificioTecnico.Text;
            string fecha_horasalidaEdicio = convertidorFecha(tx_fechahorasalida.Text);
            string horaReporte = tx_horaReporte.Text;
            string fechaReporte = convertidorFecha(tx_fechaReporte.Text);
            

            string estadoEntradaSalidaEdificio = dd_EstadoLLegadaSalidaTecnico.SelectedValue.ToString();
            string observacionTecnico = tx_observacionTecnico.Text;
            
            string nroboleta = tx_nroboleta.Text;
            bool trabajoProgramado = cbx_trabajoprogramado.Checked;
            string ascensorReparado = tx_ascensorReparado.Text;
            

            if (Ndetalle.modificarDatosTecnico2(codigoEvento, codigoEventoTecnicoAsignado, horallegadaAlEdificio, horaSalidadelEdificio, estadoEntradaSalidaEdificio, observacionTecnico, fechaAsignacion, horaAsignacion, nroboleta, trabajoProgramado,ascensorReparado, fecha_horallegadaEdificio, fecha_horasalidaEdicio, fechaReporte, horaReporte))
            {
                string nombreTecnico = dd_tecnicosAsignados.SelectedItem.Text;
                //-----------------------historial
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                NA_Historial nhistorial = new NA_Historial();
                nhistorial.insertar(codUser, "CallCenter Modifico la Observacion del Evento tiket = " + lb_numeroEvento.Text + ", del Tecnico Mantenimiento = " + nombreTecnico);
                //----------------- historial----

                Response.Write("<script type='text/javascript'> alert('Datos Guardados Correctamente') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error al Guardar') </script>");

        }

        protected void bt_guardarDatosEvento_Click(object sender, EventArgs e)
        {
           guardarDatosEvento();

           string fechaDesde = convertidorFecha(tx_desdeBusqueda.Text);
           if (fechaDesde.Equals("null"))
               fechaDesde = "";

           string fechaHasta = convertidorFecha(tx_hastaBusqueda.Text);
           if (fechaHasta.Equals("null"))
               fechaHasta = "";
           int tiket = 0;
           if (!tx_tiket.Text.Equals(""))
           {
               tiket = Convert.ToInt32(tx_tiket.Text);
           }

           buscarEventos(tiket, tx_nombreEdificioBusqueda.Text, dd_tipoEvento1.SelectedItem.Text, tx_SemanaBusqueda.Text, dd_evento.SelectedItem.Text, fechaDesde, fechaHasta);
        }

        private void guardarDatosEvento()
        {
            int codigoEvento = Convert.ToInt32(lb_numeroEvento.Text);
            NA_Evento Nevento = new NA_Evento();
            string observacionEvento = tx_observacionEvento.Text;
            string estadoEvento = dd_estadoEvento.SelectedValue.ToString();

            bool cambioRepuesto = ckb_cambioRespuesto.Checked;
            bool banderasolicitudCambioRepuesto = false;
            bool banderaCallcenter = true;
            bool banderaRin = false;
            bool banderaRCC = ckb_envioRcc.Checked;
            //bool banderaAreaCliente = ckb_areaCliente.Checked;
            bool banderaAreaCliente = false;

            int codPrioridad = Convert.ToInt32(dd_prioridad.SelectedValue.ToString());
            
            if(cambioRepuesto == true){
                banderaRin = true;
                banderasolicitudCambioRepuesto = false;
                banderaCallcenter = false;
                banderaRCC = false;
                banderaAreaCliente = false;
            }

            if(banderaRCC == true){
                banderaRin = false;
                banderaCallcenter = false;
                cambioRepuesto = true;
                banderasolicitudCambioRepuesto = true;
                banderaAreaCliente = false;
            }

            if (banderaAreaCliente == true)
            {
                banderaRin = false;
                banderaRCC = false;
                banderaCallcenter = false;
                cambioRepuesto = true;
                banderasolicitudCambioRepuesto = true;
            }
           
          
            string fechaEven = convertidorFecha(tx_fechaEvento.Text);
            string horaEven = "'"+tx_horaEvento.Text+"'";
            //----------saca nuevo codigo por si cambia de base de datos ---
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            //----------------------cambio datos evento---

            
            string edificio = tx_edificio.Text;
            string direccion = tx_Direccion.Text;
            string nombreCliente = tx_Cliente.Text;
            string celular = tx_celular.Text;
            string telefono = tx_telefono.Text;
            string ascensores = tx_Ascensores.Text;
            bool ascensorParado = CheckBox1_AscensorParado.Checked;
            int ascensorParado_aux = 0; 

            if (ascensorParado)
            {
                ascensorParado_aux = 1;
            }

            bool personasAtrapadas = CheckBox2_PersonasAtrapadas.Checked;
            int personasAtrapadas_aux = 0;
            if (personasAtrapadas)
            {
                personasAtrapadas_aux = 1;
            }

            string observacion = tx_observacion.Text;
            NProyecto Nproy = new NProyecto();
            int codproy = -1;
            if (Nproy.buscar(edificio).Tables[0].Rows.Count > 0)
            {
                codproy = Convert.ToInt32(Nproy.buscar(edificio).Tables[0].Rows[0][0].ToString());
            }


            DataSet dato = Nevento.getEvento(codigoEvento);
            string nombreEdificioReal = dato.Tables[0].Rows[0][7].ToString();

            if (edificio.Equals(nombreEdificioReal)) {
                if (Nevento.estaCerradoelEvento(codigoEvento) == false)
                {
                    Nevento.modificar(codigoEvento, nombreCliente, telefono, celular, edificio, direccion, ascensores, observacion, ascensorParado_aux, personasAtrapadas_aux);
                    //----------------------fin cambio evento
                    if (Nevento.modificarDatosEvento(codigoEvento, estadoEvento, observacionEvento, "", "", "null", "null", "null", "", cambioRepuesto, codUser, fechaEven, horaEven,banderasolicitudCambioRepuesto,banderaAreaCliente,banderaCallcenter,banderaRin,banderaRCC,codPrioridad))
                    {

                        //-----------------------historial                        
                        NA_Historial nhistorial = new NA_Historial();
                        nhistorial.insertar(codUser, "CallCenter Modifico el Evento tiket = " + lb_numeroEvento.Text + ", Estado = "+estadoEvento+", Cambio Repuesto= "+cambioRepuesto+ ", EnvioRCC= "+banderaRCC+", AreaCliente= "+banderaAreaCliente);
                        //----------------- historial----

                        Response.Write("<script type='text/javascript'> alert('Datos Guardado Correctamente') </script>");
                        Response.Redirect("../Presentacion/FA_CallCenter_Eventos.aspx");
                    }
                    else
                        Response.Write("<script type='text/javascript'> alert('Error al Guardar') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: No puede hacer Cambios Evento Cerrado') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Mantenga solo 1 Pagina Abierta') </script>");

        }

        public void cambiarBaseDeDatos(string departamento)
        {
            string BaseDatos = departamento;
            switch (BaseDatos)
            {
                case "Prueba":
                    Session["NombreBaseDatos"] = "db_prueba";
                    Session["BaseDatos"] = "Prueba";
                    break;
                case "Santa Cruz":
                    Session["NombreBaseDatos"] = "db_SantaCruz";
                    Session["BaseDatos"] = "Santa Cruz";
                    break;
                case "La Paz":
                    Session["NombreBaseDatos"] = "db_LaPaz";
                    Session["BaseDatos"] = "La Paz";
                    break;
                case "Cochabamba":
                    Session["NombreBaseDatos"] = "db_Cochabamba";
                    Session["BaseDatos"] = "Cochabamba";
                    break;
                case "Sucre":
                    Session["NombreBaseDatos"] = "db_Sucre";
                    Session["BaseDatos"] = "Sucre";
                    break;
                case "Oruro":
                    Session["NombreBaseDatos"] = "db_Oruro";
                    Session["BaseDatos"] = "Oruro";
                    break;
                case "Potosi":
                    Session["NombreBaseDatos"] = "db_Potosi";
                    Session["BaseDatos"] = "Potosi";
                    break;
                case "Tarija":
                    Session["NombreBaseDatos"] = "db_Tarija";
                    Session["BaseDatos"] = "Tarija";
                    break;
                case "Yacuiba":
                    Session["NombreBaseDatos"] = "db_Yacuiba";
                    Session["BaseDatos"] = "Yacuiba";
                    break;
                case "Villamontes":
                    Session["NombreBaseDatos"] = "db_Villamontes";
                    Session["BaseDatos"] = "Villamontes";
                    break;
                case "Asuncion-Paraguay":
                    Session["NombreBaseDatos"] = "db_Paraguay";
                    Session["BaseDatos"] = "Asuncion-Paraguay";
                    break;
                case "Beni":
                    Session["NombreBaseDatos"] = "db_beni";
                    Session["BaseDatos"] = "Beni";
                    Session["DB"] = "db_seguimientobeni_jyc";
                    break;
                case "Pando":
                    Session["NombreBaseDatos"] = "db_pando";
                    Session["BaseDatos"] = "Pando";
                    Session["DB"] = "db_seguimientopando_jyc";
                    break;  

                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }
      

        protected void dd_baseDeDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dpto = dd_baseDeDatos.SelectedValue.ToString();
            lb_eventos.Text = "Eventos " + dpto;
           //Response.Write("<script type='text/javascript'> alert('Error: " + dpto + "') </script>");
            // Response.Write("<script type='text/javascript'> alert('Error: usuario') </script>");
            cambiarBaseDeDatos(dpto);
            Response.Redirect("../Presentacion/FA_CallCenter_Eventos.aspx");  
        }

        protected void lk_excelEventos_Click(object sender, EventArgs e)
        {

            string fechaDesde = convertidorFecha(tx_desdeBusqueda.Text);
            if (fechaDesde.Equals("null"))
                fechaDesde = "";

            string fechaHasta = convertidorFecha(tx_hastaBusqueda.Text);
            if (fechaHasta.Equals("null"))
                fechaHasta = "";

            int tiket = 0;
            if (!tx_tiket.Text.Equals(""))
            {
                tiket = Convert.ToInt32(tx_tiket.Text);
            }
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = Nevento.getAllEventos(tiket,tx_nombreEdificioBusqueda.Text, dd_tipoEvento1.SelectedItem.Text, tx_SemanaBusqueda.Text, dd_evento.SelectedItem.Text, 0, fechaDesde, fechaHasta);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Eventos - " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = resultado;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }     
        }

        protected void lk_reporteEvento_Click(object sender, EventArgs e)
        {
            string fechaDesde = convertidorFecha(tx_desdeBusqueda.Text);
            if (fechaDesde.Equals("null"))
                fechaDesde = "";

            string fechaHasta = convertidorFecha(tx_hastaBusqueda.Text);
            if (fechaHasta.Equals("null"))
                fechaHasta = "";

            int tiket = 0;
            if (!tx_tiket.Text.Equals(""))
            {
                tiket = Convert.ToInt32(tx_tiket.Text);
            }
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = Nevento.getReporteAlexanderEventos(tiket, tx_nombreEdificioBusqueda.Text, dd_tipoEvento1.SelectedItem.Text, tx_SemanaBusqueda.Text, dd_evento.SelectedItem.Text, 0, fechaDesde, fechaHasta);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Eventos - " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = resultado;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }   
        }

     

            }
}