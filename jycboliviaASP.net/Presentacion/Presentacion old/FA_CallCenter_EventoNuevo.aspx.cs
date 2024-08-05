using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.Services;
using System.Web.Script.Services;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Drawing;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CallCenter : System.Web.UI.Page
    {
        //string BaseDeDatosOriginal = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (tienePermisoDeIngreso(52) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            DateTime date = DateTime.Now;
            System.Globalization.CultureInfo norwCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es");
            System.Globalization.Calendar cal = norwCulture.Calendar;
            int weekNo = cal.GetWeekOfYear(date,norwCulture.DateTimeFormat.CalendarWeekRule,norwCulture.DateTimeFormat.FirstDayOfWeek);
            // Show the result
            lb_numeroSemana.Text = weekNo.ToString();
            this.Title = Session["BaseDatos"].ToString();
            NA_Evento Nevento = new NA_Evento();
            lb_numeroEvento.Text = Convert.ToString(Nevento.getTikect()+1);
            lb_EventoNuevo.Text = "Evento Nuevo " + Session["BaseDatos"].ToString();

            tx_regional.Text = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                llenarTipoEvento();
                llenarPrioridad();
            }
        }

        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(permiso, codUser);
        }

        public void llenarTipoEvento() {
            NA_TipoEvento NtipoEvento = new NA_TipoEvento();
            dd_tipoEvento.DataSource = NtipoEvento.mostrarAllDatos();
            dd_tipoEvento.DataValueField = "codigo";
            dd_tipoEvento.DataTextField = "nombre";
           // dd_tipoEvento.Items.Add(new ListItem(""));
            dd_tipoEvento.AppendDataBoundItems = true;
           // ddlAnio.SelectedIndex = -1;
            dd_tipoEvento.DataBind();
        
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



        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProyectos2(string prefixText, int count)
        {
            string nombreProyecto = prefixText;

            NProyecto proyectoN = new NProyecto();
            DataSet tuplas = proyectoN.buscadorCallCenter(nombreProyecto);
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
        public static string[] GetlistaResponsable(string prefixText, int count)
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


        protected void Button1_Click(object sender, EventArgs e)
        {

            try {
                insertarEvento();
                limpiarTodo();
            }catch(Exception ex){
                Response.Write("<script type='text/javascript'> alert('Error: "+ex+"') </script>");
            }

        }

        private void limpiarTodo()
        {
            dd_tipoEvento.SelectedIndex = -1;
            tx_Edificio.Text = "";
            tx_direccion.Text = "";
            tx_nombreCliente.Text = "";
            tx_celular.Text = "";
            tx_telefono.Text = "";
            tx_ascensores.Text = "";
            CheckBox1_ascensorParado.Checked = false;
            CheckBox2_personasAtrapadas.Checked = false;
            tx_observacion.Text = "";
        }

        private void insertarEvento()
        {           
            int CodTipoEvento1 = Convert.ToInt32(dd_tipoEvento.SelectedValue);
            int codPrioridad = Convert.ToInt32(dd_prioridad.SelectedValue);
            int semana = Convert.ToInt32(lb_numeroSemana.Text);
            string edificio = tx_Edificio.Text;
            string direccion = tx_direccion.Text;
            string nombreCliente = tx_nombreCliente.Text;
            string celular = tx_celular.Text;
            string telefono = tx_telefono.Text;
            string ascensores = tx_ascensores.Text;
            bool ascensorParado = CheckBox1_ascensorParado.Checked;
            int ascensorParado_aux = 0;

           // int CodUser = Convert.ToInt32(Session["coduser"].ToString());
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);


            if (ascensorParado)
            {
                ascensorParado_aux = 1;
            }
            
            bool personasAtrapadas = CheckBox2_personasAtrapadas.Checked;
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

           
           bool banderaSolicitudRepuesto = false;
           bool cambioRepuesto = cbx_cotizacionRepuesto.Checked;
           bool areaCallcenter = true;
           bool areaCotizacionRepuesto = false;
           bool areaCliente = false;
           bool areaRin = false;
           bool areaRCC = false;

           if (cambioRepuesto == true)
           {
               areaCotizacionRepuesto = true;
               areaRin = true;
               banderaSolicitudRepuesto = false;               
               areaCallcenter = false;
               areaRCC = false;               
               areaCliente = false;
           }

           NA_Evento Nevento = new NA_Evento();
           bool ok = Nevento.insertar(semana,nombreCliente,telefono,celular,edificio,direccion,ascensores,"Abierto",observacion,CodTipoEvento1,codproy,ascensorParado_aux,personasAtrapadas_aux, codPrioridad,CodUser, cambioRepuesto, areaCotizacionRepuesto,banderaSolicitudRepuesto,areaCallcenter,areaRin,areaRCC,areaCliente);
         
           if(ok){
            //-----------------------historial
            NA_Historial nhistorial = new NA_Historial();
            nhistorial.insertar(CodUser, "CallCenter Ha insertado un nuevo Evento tiket = "+lb_numeroEvento.Text+" del Edificio " + edificio );
            //----------------- historial----
               Response.Write("<script type='text/javascript'> alert('OK: El Evento fue insertado') </script>");
           } else
               Response.Write("<script type='text/javascript'> alert('Error: El Evento NO ha sido insertado') </script>");

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

               
        protected void dd_departamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dpto = dd_departamento.SelectedValue.ToString();
            lb_EventoNuevo.Text = "Evento Nuevo " + dpto;
           //Response.Write("<script type='text/javascript'> alert('Error: " + dpto + "') </script>");
           // Response.Write("<script type='text/javascript'> alert('Error: usuario') </script>");
            cambiarBaseDeDatos(dpto);
            Response.Redirect("../Presentacion/FA_CallCenter_EventoNuevo.aspx");    
        }

        protected void bt_validarEdificio_Click(object sender, EventArgs e)
        {
            lb_verificar.Text = "";
            lb_verificar2.Text = "";
            buscarEspecifico();
            verificarDeudaEdificio();
            VerificarRepuestoDeuda();
            VerificarCotizacionesPendientes();
            verificarPlanpago();
            cuantoAscensoresParadoClientehay();
            cuantoAscensoresParadoNosotroshay();
            verificarServicioSuspendido();
            verificarHorarioAtencion();
            verificarSiTienePrevisionActual();
            verificarEstadosEquipos();
            verificar_CantDiasDesdeLaAprobacionCotizacion();
        }

        private void verificar_CantDiasDesdeLaAprobacionCotizacion()
        {
            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);
            string edificio = tx_Edificio.Text;
            NA_Repuesto nrepuesto = new NA_Repuesto();
            int diasRetrazo = nrepuesto.get_diferenciaDiasDesdeLaAprobacionDeLasCotizaciones(codigoEdificio, edificio);

            if (diasRetrazo > 0)
            {
                lb_CantDiasDesdeLaAprobacionCotizacion.BackColor = Color.Red;
                lb_CantDiasDesdeLaAprobacionCotizacion.ForeColor = Color.White;
                lb_CantDiasDesdeLaAprobacionCotizacion.Text = diasRetrazo.ToString() + " Dias desde la ultima Aprobacion de cotizacion";
            }
            else
            {
                lb_CantDiasDesdeLaAprobacionCotizacion.BackColor = Color.LimeGreen;
                lb_CantDiasDesdeLaAprobacionCotizacion.ForeColor = Color.Black;
                lb_CantDiasDesdeLaAprobacionCotizacion.Text = diasRetrazo.ToString() + " Dias desde la ultima Aprobacion de cotizacion";
            }
        }

        private void verificarEstadosEquipos()
        {
            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);
            string atencionEstados= nproyec.VerificarEstadosEquipos(codigoEdificio);

            lb_atencionMantenimiento.Text = atencionEstados;

            if (!atencionEstados.Equals("Tiene Atencion"))
            {
                lb_atencionMantenimiento.BackColor = Color.Red;
                lb_atencionMantenimiento.ForeColor = Color.White;
            }
            else
            {
                lb_atencionMantenimiento.BackColor = Color.White;
                lb_atencionMantenimiento.ForeColor = Color.Black;
                lb_atencionMantenimiento.Text = "";
            }
        }

        private void verificarSiTienePrevisionActual()
        {
            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);
            string Prevision = nproyec.tienePrevisionActual(codigoEdificio);

            lb_notienePrevision.Text = Prevision;

            if (Prevision.Equals("No Esta en Mantenimiento"))
            {
                lb_notienePrevision.BackColor = Color.Red;
                lb_notienePrevision.ForeColor = Color.White;
                lb_notienePrevision.Text = "Sin Prevision (No Atender)";
            }
            else
            {
                lb_notienePrevision.BackColor = Color.LimeGreen;
                lb_notienePrevision.ForeColor = Color.Black;
                lb_notienePrevision.Text = "Area Mantenimiento";
            }
        }

        private void verificarHorarioAtencion()
        {
            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);
            string horarioatencion = nproyec.horariodeAtencion(codigoEdificio);
            lb_horacioAtencion.Text = horarioatencion;

            if (horarioatencion.Equals("solo Horario Oficina"))
            {
                lb_horacioAtencion.BackColor = Color.Red;
                lb_horacioAtencion.ForeColor = Color.White;
            }
            else
            {
                lb_horacioAtencion.BackColor = Color.LimeGreen;
                lb_horacioAtencion.ForeColor = Color.Black;
            }
        }


        private void cuantoAscensoresParadoClientehay()
        {
            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);
            int cantidad = nproyec.CuantosAscensorestieneParadoCliente(codigoEdificio);
            if (cantidad > 0)
            {
                lb_paradoCliente.Text = "Tiene "+cantidad+" Asc. Parado Cliente";
                lb_paradoCliente.BackColor = Color.Red;
                lb_paradoCliente.ForeColor = Color.White;
            }
            else
            {
                lb_paradoCliente.Text = "No tiene Asc. Parado Cliente";
                lb_paradoCliente.BackColor = Color.LimeGreen;
                lb_paradoCliente.ForeColor = Color.Black;
                
            }
        }

        private void cuantoAscensoresParadoNosotroshay()
        {
            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);
            int cantidad = nproyec.CuantosAscensorestieneParadoNosotros(codigoEdificio);
            if (cantidad > 0)
            {
                lb_paradoNosotros.Text = "Tiene " + cantidad + " Asc. Parado Nosotros";
                lb_paradoNosotros.BackColor = Color.Red;
                lb_paradoNosotros.ForeColor = Color.White;
            }
            else
            {
                lb_paradoNosotros.Text = "No tiene Asc. Parado Nosotros";
                lb_paradoNosotros.BackColor = Color.LimeGreen;
                lb_paradoNosotros.ForeColor = Color.Black;

            }
        }


        private void verificarPlanpago()
        {
            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);

            if (nproyec.tienePlanPago(codigoEdificio))
            {
                lb_planPago.Text = "Tiene Plan de Pagos";
                lb_planPago.BackColor = Color.Yellow;
                lb_planPago.ForeColor = Color.Black;
            }
            else
            {
                lb_planPago.Text = "No tiene Plan de Pagos";
                lb_planPago.BackColor = Color.Red;
                lb_planPago.ForeColor = Color.White;
            }
        }


        private void verificarServicioSuspendido()
        {
            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);

            if (nproyec.tieneServicioSuspendido(codigoEdificio))
            {
                lb_serviciosuspendido.Text = "Servicio Suspendido";
                lb_serviciosuspendido.BackColor = Color.Red;
                lb_serviciosuspendido.ForeColor = Color.White;
            }
            else
            {
                lb_serviciosuspendido.Text = "No tiene Servicio Suspendido";
                lb_serviciosuspendido.BackColor = Color.LimeGreen;
                lb_serviciosuspendido.ForeColor = Color.Black;
            }
        }


        public void VerificarCotizacionesPendientes()
        {

            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);
            if(codigoEdificio > -1){
                NA_Repuesto nrepuestos = new NA_Repuesto();
                int cantidadCoti = nrepuestos.getCantidadCotizacionesRepuesto(codigoEdificio);

                if (cantidadCoti > 0)
                {
                    lb_cotizacionRepuesto.Text = "Tiene " + cantidadCoti + " Cotizaciones Pendiente";
                    lb_cotizacionRepuesto.BackColor = Color.Red;
                    lb_cotizacionRepuesto.ForeColor = Color.White;
                }
                else
                {
                    lb_cotizacionRepuesto.Text = "No tiene Cotizaciones Pendientes";
                    lb_cotizacionRepuesto.BackColor = Color.LimeGreen;
                    lb_cotizacionRepuesto.ForeColor = Color.Black;                    
                    
                }
            }
          
        }



        public void VerificarRepuestoDeuda()
        {

            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);

            if (nproyec.tieneDeudasRepuestoPendientesProyecto(codigoEdificio))
            {
                lb_repuesto.Text = "Tiene Deudas de Repuesto";
                lb_repuesto.BackColor = Color.Red;
                lb_repuesto.ForeColor = Color.White;
            }
            else
            {
                lb_repuesto.Text = "No tiene Deudas de Repuesto";
                lb_repuesto.BackColor = Color.LimeGreen;
                lb_repuesto.ForeColor = Color.Black;
            }
        }


        public void verificarDeudaEdificio() {

            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);

            if (nproyec.tieneDeudasPendientesProyecto(codigoEdificio))
            {
                lb_verificar2.Text = "Tiene Deudas de Instalacion";
                lb_verificar2.BackColor = Color.Red;
                lb_verificar2.ForeColor = Color.White;
            }
            else {
                lb_verificar2.Text = "No tiene Deudas de Instalacion";
                lb_verificar2.BackColor = Color.LimeGreen;
                lb_verificar2.ForeColor = Color.Black;
            }

            

        }


        public void buscarEspecifico()
        {
      
            string year = "Todos";
            string edificio = tx_Edificio.Text;
            NProyecto nproy = new NProyecto();

            if (nproy.existeProyectoEspecifico(edificio))
            {

                if (year == "Todos")
                {
                    string exbo = "";
                    NA_Seguimiento segn = new NA_Seguimiento();
                    int mesesAtrazadosPermitidos = NA_VariablesGlobales.meseslimitesdeAtrazadosPermitidosMantenimiento;
                    // DataSet tuplaRes = segn.getTodosEquiposMantenimientoMorososEspecifico(exbo, edificio, mesesAtrazadosPermitidos);
                    // modificado sacamos todas los meses k debe
                    DataSet tuplaRes = segn.get_TodosEquiposMantenimientoMorososEspecifico_CallCenter(exbo, edificio, mesesAtrazadosPermitidos);
                    NEquipo neq = new NEquipo();

                    if (tuplaRes.Tables[0].Rows.Count == 0)
                    {
                       /* lb_verificar.Text = "No tiene Prevision realizada";
                        lb_verificar.BackColor = Color.Red;
                        lb_verificar.ForeColor = Color.White;*/
                        lb_verificar.Text = "No tiene Deudas Mantenimiento";
                        lb_verificar.BackColor = Color.LimeGreen;
                        lb_verificar.ForeColor = Color.Black;
                    }
                    else {
                        int mesesAtrazados = 0;
                        int cantEquipos = tuplaRes.Tables[0].Rows.Count;
                        for (int i = 0; i < cantEquipos; i++)
                        {
                            mesesAtrazados = mesesAtrazados + int.Parse(tuplaRes.Tables[0].Rows[i][2].ToString());
                        }

                         lb_verificar.Text = "Deuda "+mesesAtrazados+" meses, "+cantEquipos+" Equipos";
                         lb_verificar.BackColor = Color.Red;
                         lb_verificar.ForeColor = Color.White;
                    }
                    
                }

            }
            else
            {
                lb_verificar.Text = "El edificio no Existe";
                lb_verificar.BackColor = Color.Yellow;
            }

        }

        protected void tx_Edificio_TextChanged(object sender, EventArgs e)
        {
            ponerDireccion();
        }

        private void ponerDireccion()
        {
            string nombreEdificio = tx_Edificio.Text;
            if(!nombreEdificio.Equals("")){
                NProyecto nproyect = new NProyecto();
                DataSet res_edificio = nproyect.getProyect2(nombreEdificio);
               if(res_edificio.Tables[0].Rows.Count > 0 ){
                   tx_direccion.Text = res_edificio.Tables[0].Rows[0][3].ToString();
               }
            }
        }

        protected void cbx_cotizacionRepuesto_CheckedChanged(object sender, EventArgs e)
        {
            if(cbx_cotizacionRepuesto.Checked == true){
                dd_tipoEvento.SelectedIndex = 6;
            }else
                dd_tipoEvento.SelectedIndex = 0;
        }

        protected void dd_tipoEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dd_tipoEvento.SelectedIndex == 6){
            cbx_cotizacionRepuesto.Checked = true;
            }
        }
        
            
    }
}