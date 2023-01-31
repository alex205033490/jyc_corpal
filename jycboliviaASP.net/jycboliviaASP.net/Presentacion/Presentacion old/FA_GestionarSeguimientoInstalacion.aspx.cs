using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.IO;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_GestionarSeguimientoInstalacion : System.Web.UI.Page
    {
        private object locker = new object();

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(20) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 


            desactivarTexto();

            if(!IsPostBack){
                mostrarEstadosInstalacion();
                mostrarSeguimientoInstalacion();
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

        public void desactivarTexto() {
            tx_Exbo.Enabled = false;
            tx_proyecto.Enabled = false;
            tx_tipologia.Enabled = false;
            tx_Une.Enabled = false;
           // tx_dirObra.Enabled = false;
            tx_Instalador_TecnicoAsignado.Enabled = false;
        }

        public void mostrarEstadosInstalacion() {
            NA_SeguimientoInstalacion NseguiInstalacion = new NA_SeguimientoInstalacion();
            dd_estadoInstalacion.DataSource = NseguiInstalacion.mostrarEstadosInstalacion();
            dd_estadoInstalacion.DataValueField = "codigo";
            dd_estadoInstalacion.DataTextField = "nombre";
            dd_estadoInstalacion.Items.Add(new ListItem(""));
            dd_estadoInstalacion.AppendDataBoundItems = true;
            dd_estadoInstalacion.SelectedIndex = -1;
            dd_estadoInstalacion.DataBind();
        }

        public void mostrarSeguimientoInstalacion() {
            NA_SeguimientoInstalacion seguiInstN = new NA_SeguimientoInstalacion();
            DataSet tuplasTabla = seguiInstN.BuscarDatos("","");
            gv_SeguiInstalacion.DataSource = tuplasTabla;
            gv_SeguiInstalacion.DataBind();
        }

        public void buscarDatos() {
            NA_SeguimientoInstalacion seguiInstN = new NA_SeguimientoInstalacion();
            DataSet tuplasTabla = seguiInstN.BuscarDatos(tx_ExboSerie.Text,tx_edificiobuscar.Text);
            gv_SeguiInstalacion.DataSource = tuplasTabla;
            gv_SeguiInstalacion.DataBind();
        
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            buscarDatos();
        }


        public void seleccionarDatos() {
            NA_SeguimientoInstalacion nseguinst = new NA_SeguimientoInstalacion();

            string exbo = gv_SeguiInstalacion.SelectedRow.Cells[1].Text;
            NA_Equipo Neq = new NA_Equipo();
            int codigoEquipo = Neq.getCodigoEquipo(exbo);
            NEquipo nequipo2 = new NEquipo();

          

            DataSet tuplaEquipo = nequipo2.getEquipo(codigoEquipo);
            if (tuplaEquipo.Tables[0].Rows.Count > 0)
            {
                tx_tipologia.Text = HttpUtility.HtmlDecode(tuplaEquipo.Tables[0].Rows[0][2].ToString());
            }
            else
                tx_tipologia.Text = "";

            

            tx_Exbo.Text = HttpUtility.HtmlDecode(gv_SeguiInstalacion.SelectedRow.Cells[1].Text);
            tx_proyecto.Text = HttpUtility.HtmlDecode(gv_SeguiInstalacion.SelectedRow.Cells[2].Text);
            if (gv_SeguiInstalacion.SelectedRow.Cells[3].Text != "&nbsp;")
            {
                tx_dirObra.Text = HttpUtility.HtmlDecode(gv_SeguiInstalacion.SelectedRow.Cells[3].Text);
            }
            else
                tx_dirObra.Text = "";
           // string nombreEstadoInstalacion = gv_SeguiInstalacion.SelectedRow.Cells[4].Text;
           // int codigoEstadoInstalacion = nseguinst.getCodigoEstadoInstalacion(nombreEstadoInstalacion);
           // dd_estadoInstalacion.SelectedIndex = codigoEstadoInstalacion;

            int codigoEstadoEquipo = Convert.ToInt32(tuplaEquipo.Tables[0].Rows[0][9].ToString());
            dd_estadoInstalacion.SelectedValue = Convert.ToString(codigoEstadoEquipo);

            
            if (gv_SeguiInstalacion.SelectedRow.Cells[5].Text != "&nbsp;")
            {
                tx_Supervisor.Text = HttpUtility.HtmlDecode(gv_SeguiInstalacion.SelectedRow.Cells[5].Text);
            }
            else {
                tx_Supervisor.Text = "";
            }


            if (gv_SeguiInstalacion.SelectedRow.Cells[6].Text != "&nbsp;")
            {
                tx_email.Text = HttpUtility.HtmlDecode(gv_SeguiInstalacion.SelectedRow.Cells[6].Text);
            }
            else {
                tx_email.Text = "";
            }

            if (gv_SeguiInstalacion.SelectedRow.Cells[7].Text != "&nbsp;")
            {
                tx_Telefono.Text = HttpUtility.HtmlDecode(gv_SeguiInstalacion.SelectedRow.Cells[7].Text);
            }
            else {
                tx_Telefono.Text = "";
            }
            

            string AOI = gv_SeguiInstalacion.SelectedRow.Cells[8].Text;
            dd_aoi.SelectedValue = AOI;
            // aoi

            string planos = gv_SeguiInstalacion.SelectedRow.Cells[9].Text;
            dd_plano.SelectedValue = planos;
            //--- poner Planos

            if (gv_SeguiInstalacion.SelectedRow.Cells[10].Text != "&nbsp;")
            {
                tx_otros_PendientesFabrica.Text = gv_SeguiInstalacion.SelectedRow.Cells[10].Text;
            }
            else {
                tx_otros_PendientesFabrica.Text = "";
            }

            string C01 = gv_SeguiInstalacion.SelectedRow.Cells[11].Text;
            dd_C01.SelectedValue = C01;
            // C-01

            string C05 = gv_SeguiInstalacion.SelectedRow.Cells[12].Text;
            dd_c05.SelectedValue = C05;
            // C-05

            string FAI = gv_SeguiInstalacion.SelectedRow.Cells[13].Text;
            dd_FAI.SelectedValue = FAI;
            // FAI

            string FAII = gv_SeguiInstalacion.SelectedRow.Cells[14].Text;
            dd_FAII.SelectedValue = FAII;
            // FAII

            string EstadoObraAdecualdo = gv_SeguiInstalacion.SelectedRow.Cells[15].Text;
            dd_EstadoObraAdecuado.SelectedValue = EstadoObraAdecualdo;
            // estado de Obra Adecuado

            if (gv_SeguiInstalacion.SelectedRow.Cells[16].Text != "&nbsp;")
            {
                tx_ObservacionesEstadoObra.Text = gv_SeguiInstalacion.SelectedRow.Cells[16].Text;
            }
            else {
                tx_ObservacionesEstadoObra.Text = "";
            }
            

            string electricidad = gv_SeguiInstalacion.SelectedRow.Cells[17].Text;
            dd_Electricidad.SelectedValue = electricidad;
            // electricidad

            if (gv_SeguiInstalacion.SelectedRow.Cells[18].Text != "&nbsp;")
            {
                tx_otrosApliModSegu.Text = gv_SeguiInstalacion.SelectedRow.Cells[18].Text;
            }
            else {
                tx_otrosApliModSegu.Text = "";
            }
            

            string CumplimientoReguisito = gv_SeguiInstalacion.SelectedRow.Cells[19].Text;
            dd_CumplimientoRequisitos.SelectedValue = CumplimientoReguisito;
            //---- cumplimientos de requisitos

            if (gv_SeguiInstalacion.SelectedRow.Cells[20].Text != "&nbsp;")
            {
                tx_IC_fechaExpedicion.Text = gv_SeguiInstalacion.SelectedRow.Cells[20].Text;
            }
            else {
                tx_IC_fechaExpedicion.Text = "";
            }

            if (gv_SeguiInstalacion.SelectedRow.Cells[21].Text != "&nbsp;")
            {
                tx_IC_EstimadoEquipoObraSemana.Text = gv_SeguiInstalacion.SelectedRow.Cells[21].Text;
            }
            else {
                tx_IC_EstimadoEquipoObraSemana.Text = "";
            }


            if (gv_SeguiInstalacion.SelectedRow.Cells[22].Text != "&nbsp;")
            {
                tx_IC_SemanaEntregaRequerida.Text = gv_SeguiInstalacion.SelectedRow.Cells[22].Text;
            }
            else {
                tx_IC_SemanaEntregaRequerida.Text = "0";
            }

            if (gv_SeguiInstalacion.SelectedRow.Cells[23].Text != "&nbsp;")
            {
                tx_IC_FechaEntregaRequerida.Text = gv_SeguiInstalacion.SelectedRow.Cells[23].Text;
            }
            else {
                tx_IC_FechaEntregaRequerida.Text = "";
            }

            if (gv_SeguiInstalacion.SelectedRow.Cells[24].Text != "&nbsp;")
            {
                tx_entrega_inicioFase1.Text = gv_SeguiInstalacion.SelectedRow.Cells[24].Text;
                int cantidadDias = calcularDiasCronogramaTecnico_faceI(codigoEquipo);
                DateTime fechaini = Convert.ToDateTime(tx_entrega_inicioFase1.Text);
                tx_entrega_entregaFase1.Text = fechaini.AddDays(cantidadDias).ToShortDateString();
            }
            else {
                tx_entrega_inicioFase1.Text = "";
                tx_entrega_entregaFase1.Text = "";
            }

            if (gv_SeguiInstalacion.SelectedRow.Cells[25].Text != "&nbsp;")
            {
                tx_entrega_inicioFase2.Text = gv_SeguiInstalacion.SelectedRow.Cells[25].Text;
                int cantidadDias = calcularDiasCronogramaTecnico_faceII(codigoEquipo);
                DateTime fechaini = Convert.ToDateTime(tx_entrega_inicioFase2.Text);
                tx_entrega_entregaFase2.Text = fechaini.AddDays(cantidadDias).ToShortDateString();
            }
            else {
                tx_entrega_inicioFase2.Text = "";
                tx_entrega_entregaFase2.Text = "";
            }

            
            if (gv_SeguiInstalacion.SelectedRow.Cells[26].Text != "&nbsp;")
            {
                tx_Entrega_FechaActaEntrega.Text = gv_SeguiInstalacion.SelectedRow.Cells[26].Text;
            }
            else {
                tx_Entrega_FechaActaEntrega.Text = "";
            }

            if (gv_SeguiInstalacion.SelectedRow.Cells[27].Text != "&nbsp;")
            {
                tx_Entrega_FechaEntregaYCertificacionR118.Text = gv_SeguiInstalacion.SelectedRow.Cells[27].Text;
            }
            else {
                tx_Entrega_FechaEntregaYCertificacionR118.Text = "";
            }

            if (gv_SeguiInstalacion.SelectedRow.Cells[28].Text != "&nbsp;")
            {
                tx_Entrega_SemanasEstimadaInstalacion.Text = gv_SeguiInstalacion.SelectedRow.Cells[28].Text;
            }
            else {
                tx_Entrega_SemanasEstimadaInstalacion.Text = "0";
            }

            if (gv_SeguiInstalacion.SelectedRow.Cells[29].Text != "&nbsp;")
            {
                tx_Instalador_ContratoInstalador.Text = gv_SeguiInstalacion.SelectedRow.Cells[29].Text;
            }
            else {
                tx_Instalador_ContratoInstalador.Text = "";
            }

            if (gv_SeguiInstalacion.SelectedRow.Cells[30].Text != "&nbsp;")
            {
                tx_Instalador_SemanasAcumuladas.Text = gv_SeguiInstalacion.SelectedRow.Cells[30].Text;
            }
            else {
                tx_Instalador_SemanasAcumuladas.Text = "0";
            }

            if (gv_SeguiInstalacion.SelectedRow.Cells[31].Text != "&nbsp;")
            {
                tx_Instalador_TecnicoAsignado.Text = HttpUtility.HtmlDecode(gv_SeguiInstalacion.SelectedRow.Cells[31].Text);
            }
            else {
                tx_Instalador_TecnicoAsignado.Text = "";
            }

        }


        public int calcularDiasCronogramaTecnico_faceI(int codEquipo)
        {
            try
            {
                NA_Equipo equipoA = new NA_Equipo();
                DataSet datoEquipoCrono = equipoA.getEquiposCronogramaTecnico2(codEquipo);
                string fechaAux = datoEquipoCrono.Tables[0].Rows[0][7].ToString();
                string paradasAux = datoEquipoCrono.Tables[0].Rows[0][3].ToString();

                int paradas = Convert.ToInt32(paradasAux);
                NA_CronogramaTecnico crono = new NA_CronogramaTecnico();
                int NroParadas = crono.getParadasExacta(paradas);
                int cantDias = crono.getDias_segunParadas_FaseI(NroParadas);

                //--------------crearcolumnas
                int cantidadSabados = crono.calcularSabadosenFecha(Convert.ToDateTime(fechaAux), cantDias);
                int cantDiasAux = cantDias + cantidadSabados;
                return cantDiasAux;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int calcularDiasCronogramaTecnico_faceII(int codEquipo) {

            try
            {
                NA_Equipo equipoA = new NA_Equipo();
                DataSet datoEquipoCrono = equipoA.getEquiposCronogramaTecnico2(codEquipo);
                string fechaAux = datoEquipoCrono.Tables[0].Rows[0][8].ToString();
                string paradasAux = datoEquipoCrono.Tables[0].Rows[0][3].ToString();

                NA_CronogramaTecnico crono = new NA_CronogramaTecnico();
                int paradas = Convert.ToInt32(paradasAux);
                int NroParadas = crono.getParadasExacta(paradas);
                int cantDias = crono.getDias_segunParadas_FaseII(NroParadas);

                //--------------crearcolumnas
                int cantidadSabados = crono.calcularSabadosenFecha(Convert.ToDateTime(fechaAux), cantDias);
                int sabadoAdicional = cantidadSabados;
                if (sabadoAdicional > 1)
                {
                    sabadoAdicional--;
                }
                int cantDiasAux = cantDias + sabadoAdicional;
                return cantDiasAux;
            }
            catch (Exception e) {
                return 0;
            }
        }




        protected void gv_SeguiInstalacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiar();
            seleccionarDatos();
        }

      public string convertidorFecha(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'"+_fecha+"'";
            }
            else
                return "null";
        }
        


        public void limpiar() {
            tx_Exbo.Text = "";
            tx_proyecto.Text = "";
            tx_tipologia.Text = "";
            tx_Une.Text = "";
            tx_dirObra.Text = "";
            dd_estadoInstalacion.SelectedIndex = -1;
            tx_Supervisor.Text = "";
            tx_email.Text = "";
            tx_Telefono.Text = "";
            dd_aoi.SelectedIndex = -1;
            dd_plano.SelectedIndex = -1;
            tx_otros_PendientesFabrica.Text = "";
            dd_C01.SelectedIndex = -1;
            dd_c05.SelectedIndex = -1;
            dd_FAI.SelectedIndex = -1;
            dd_FAII.SelectedIndex = -1;
            dd_EstadoObraAdecuado.SelectedIndex = -1;
            tx_ObservacionesEstadoObra.Text = "";
            dd_Electricidad.SelectedIndex = -1;
            tx_otrosApliModSegu.Text = "";
            dd_CumplimientoRequisitos.SelectedIndex = -1;
            tx_IC_fechaExpedicion.Text = "";
            tx_IC_EstimadoEquipoObraSemana.Text = "";
            
            tx_IC_SemanaEntregaRequerida.Text = "0";
            tx_IC_FechaEntregaRequerida.Text = "";
            
            tx_Entrega_FechaActaEntrega.Text = "";
            tx_Entrega_FechaEntregaYCertificacionR118.Text = "";
            tx_Entrega_SemanasEstimadaInstalacion.Text = "0";
            tx_Instalador_ContratoInstalador.Text = "";
            tx_Instalador_SemanasAcumuladas.Text = "0";        
        
        }

                      
        public void ActualizarDatos() {
            string exbo = tx_Exbo.Text; 
            NA_Equipo Nequipo = new NA_Equipo();
            NEquipo equipo2 = new NEquipo();
        
                    string supervidor = HttpUtility.HtmlDecode(tx_Supervisor.Text);
                    string email = tx_email.Text;
                    string telefono = tx_Telefono.Text;
                    string pf_aoi = dd_aoi.SelectedValue;
                    string pf_plano = dd_plano.SelectedValue;
                    string pf_otros = tx_otros_PendientesFabrica.Text;
                    string pjyc_c01 = dd_C01.SelectedValue;
                    string pjyc_c05 = dd_c05.SelectedValue;
                    string pjyc_fa1 = dd_FAI.SelectedValue;
                    string pjyc_fa2 = dd_FAII.SelectedValue;
                    string eo_adecuado = dd_EstadoObraAdecuado.SelectedValue;
                    string eo_observaciones = tx_ObservacionesEstadoObra.Text;
                    string eo_electricidad = dd_Electricidad.SelectedValue;
                    string eo_apli_modif_seguri = tx_otrosApliModSegu.Text;
                    string eo_cumpliotrorequisito = dd_CumplimientoRequisitos.SelectedValue;
                    string ic_fechaexpedicion = convertidorFecha(tx_IC_fechaExpedicion.Text);
                    string ic_estimadoobraequiposemana = convertidorFecha(tx_IC_EstimadoEquipoObraSemana.Text);
                    
                    string ic_fechaentregarequerida = convertidorFecha(tx_IC_FechaEntregaRequerida.Text);
                    
                    int ic_semanaentregarequerida = 0;

                    if(!ic_fechaentregarequerida.Equals("null")){
                        System.Globalization.CultureInfo norwCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es");
                        System.Globalization.Calendar cal = norwCulture.Calendar;
                        int weekNo = cal.GetWeekOfYear(Convert.ToDateTime(tx_IC_FechaEntregaRequerida.Text), norwCulture.DateTimeFormat.CalendarWeekRule, norwCulture.DateTimeFormat.FirstDayOfWeek);
                        ic_semanaentregarequerida = weekNo;
                    }
                                        
                    string fecha_actaentregayconclusionconformidad = convertidorFecha(tx_Entrega_FechaActaEntrega.Text);
                    string fecha_entregaycertificacionhabilitadoR118 = convertidorFecha(tx_Entrega_FechaEntregaYCertificacionR118.Text);
                    int semanasestimadasintacion = Convert.ToInt32(tx_Entrega_SemanasEstimadaInstalacion.Text);
                    string contratoinstalacion = tx_Instalador_ContratoInstalador.Text;
                    int semanaacumuladaportecnicoasignado = Convert.ToInt32(tx_Instalador_SemanasAcumuladas.Text);

                    int codigoEquipo = Nequipo.getCodigoEquipo(gv_SeguiInstalacion.SelectedRow.Cells[1].Text);
                    NA_SeguimientoInstalacion nseguiInst = new NA_SeguimientoInstalacion();
                    lock (locker)
                    {  //---------------seccion Critica

                        if (Nequipo.tieneSeguimientoInstalacion(codigoEquipo))
                        {
                            int codigoSeguimientoInstalacion = Nequipo.getCodigoSeguimientoEquipo(codigoEquipo);
                            bool bandera =  nseguiInst.modificar(codigoSeguimientoInstalacion,supervidor, email,
                                               telefono, pf_aoi, pf_plano,
                                               pf_otros, pjyc_c01, pjyc_c05,
                                               pjyc_fa1, pjyc_fa2, eo_adecuado,
                                               eo_observaciones, eo_electricidad, eo_apli_modif_seguri,
                                               eo_cumpliotrorequisito, ic_fechaexpedicion, 
                                               ic_semanaentregarequerida, 
                                               semanasestimadasintacion, contratoinstalacion,
                                               semanaacumuladaportecnicoasignado);
                            bool bandera2 = Nequipo.modificarSeguimientoInstalacionFechaEquipoObra(codigoEquipo, ic_estimadoobraequiposemana);

                            if(bandera && bandera2){
                            Response.Write("<script type='text/javascript'> alert('Dato Actualizado Correctamente') </script>");
                            }else
                                Response.Write("<script type='text/javascript'> alert('ERROR: Dato erroneo') </script>");
                        }
                        else
                        {

                           bool bandera1 = nseguiInst.insertar(supervidor, email,
                                               telefono, pf_aoi, pf_plano,
                                               pf_otros, pjyc_c01, pjyc_c05,
                                               pjyc_fa1, pjyc_fa2, eo_adecuado,
                                               eo_observaciones, eo_electricidad, eo_apli_modif_seguri,
                                               eo_cumpliotrorequisito, ic_fechaexpedicion, 
                                               ic_semanaentregarequerida,                                                
                                               semanasestimadasintacion, contratoinstalacion,
                                               semanaacumuladaportecnicoasignado);
                         
                         if (bandera1)
                         {
                             int codSeguimientoInstalacion = nseguiInst.ultimoinsertado();
                             bool bandera2 = Nequipo.modificarSeguimientoInstalacion(codigoEquipo, codSeguimientoInstalacion, ic_estimadoobraequiposemana);
                             Response.Write("<script type='text/javascript'> alert('Dato Actualizado Correctamente') </script>");
                         }
                         else
                             Response.Write("<script type='text/javascript'> alert('ERROR: Dato erroneo') </script>");

                        }
                        
                    }  //-------- fin lock

                    NEquipo equipo = new NEquipo();
                    int CodestadoEquipo = Convert.ToInt32(dd_estadoInstalacion.SelectedValue);                    

                   //int codUser = Convert.ToInt32(Session["coduser"].ToString());
                    NA_Responsables Nresp = new NA_Responsables();
                    string usuarioAux = Session["NameUser"].ToString();
                    string passwordAux = Session["passworuser"].ToString();
                    int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                    NProyecto nproy = new NProyecto();
                    string nombreProyecto = HttpUtility.HtmlDecode(gv_SeguiInstalacion.SelectedRow.Cells[2].Text);
                    int codProyecto = nproy.getCodigoProyect(nombreProyecto);
                    string direccion = HttpUtility.HtmlDecode(tx_dirObra.Text);
                    if (codProyecto > 0)
                    {
                        nproy.modificarDireccion(codProyecto, direccion);
                    }
                    //-------------historial
                    NA_Historial nhistorial = new NA_Historial();
                    nhistorial.insertar(codUser, "Modifico en seguimiento Instalacion un Equipo con exbo " + exbo);
                    //---------------------

            ///-----------cambio de estados------------
                    int codEstadoActual = equipo.getCodigoEstadoActual(codigoEquipo);
                    bool permisodeProyecto = equipo.estaPermitidoEstadoProyecto(CodestadoEquipo);
                    bool permisodeImportacionEstadoNuevo = equipo.estaPermitidoEstadoImportacion(CodestadoEquipo);
                    bool permisodeImportacionEstadoActual = equipo.estaPermitidoEstadoImportacion(codEstadoActual);
                    if (
                        (permisodeProyecto == true &&
                        permisodeImportacionEstadoNuevo == false &&
                        permisodeImportacionEstadoActual == false)
                        ||
                        (codEstadoActual == 4 &&
                        permisodeImportacionEstadoNuevo == false)
                        )
                    {
                        //// ---------- reglas para los fpr de no cambiar los estados de importacion exepto equipo en obra

                        //poder modificar el estado (si es estado permitido FPR y son diferentes los estados antes y actual)
                        if (equipo.estaPermitidoEstadoProyecto(CodestadoEquipo) && (codEstadoActual != CodestadoEquipo))
                        {                         
                                // si es diferente al estado habilitado cambia nomas el estado                  
                                NA_FechaEstadoEquipo fechaEstadoEq = new NA_FechaEstadoEquipo();
                                fechaEstadoEq.insertar(codigoEquipo, CodestadoEquipo, codUser);
                                int codFechaEstadoUltimoInsertado = fechaEstadoEq.ultimoinsertado();
                                equipo.ModificarFechaEstadoEquipo(codigoEquipo, codFechaEstadoUltimoInsertado);
                                //-------------------historial --------------                                
                                nhistorial.insertar(codUser, "Modifico estado = " + dd_estadoInstalacion.SelectedItem.Text + " en la vista FA_GestionarSeguimientoInstalacion.aspx un Equipo con exbo " + exbo);
                                //-------------------------------------------
                        }

                        //// ----------FIN reglas para los fpr de no cambiar los estados de importacion exepto equipo en obra
                    }else
                    {
                        Response.Write("<script type='text/javascript'> alert('Error: No se pudo Cambiar el Estado') </script>");
                    }
            //---------------------fin de cambio de estado

             /*       
            ///----------------------aqui ay k trabajar en cambio de estados
                    NA_FechaEstadoEquipo fechaEstadoEquipo = new NA_FechaEstadoEquipo();
                    fechaEstadoEquipo.insertar(codigoEquipo, CodestadoEquipo,codUser);
                    int codFechaEstadoEquipoUltimoInsertado = fechaEstadoEquipo.ultimoinsertado();
                    equipo.ModificarFechaEstadoEquipo(codigoEquipo, codFechaEstadoEquipoUltimoInsertado);
             ///------------------------fin cambio de estados---------------
               */
           }             
        

        protected void bt_Actualizar_Click(object sender, EventArgs e)
        {               
                ActualizarDatos();
                limpiar();
                mostrarSeguimientoInstalacion();            
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            NA_SeguimientoInstalacion seguiInstN = new NA_SeguimientoInstalacion();
            DataSet tuplasTabla = seguiInstN.BuscarDatos(tx_ExboSerie.Text, tx_edificiobuscar.Text);
            
                //// Creacion del Excel
                HttpResponse response = HttpContext.Current.Response;
                // first let's clean up the response.object
                response.Clear();
                response.Charset = "";
                // set the response mime type for excel
                response.ContentType = "application/vnd.ms-excel";
                string nombre = "Seguimiento Instalacion";
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

                // create a string writer
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        // instantiate a datagrid
                        DataGrid dg = new DataGrid();
                        dg.DataSource = tuplasTabla;
                        dg.DataBind();
                        dg.RenderControl(htw);
                        response.Write(sw.ToString());
                        response.End();
                    }
                }
            }

        protected void bt_Limpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        }
    
}