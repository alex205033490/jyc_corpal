using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CallCenter_AreaCotiRepuesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(66) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            lb_eventos.Text = "Eventos Cotizacion Repuesto " + Session["BaseDatos"].ToString();
            tx_BaseDeDatos.Text = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                buscarEventos(0, "", "", "", "Abierto", "", "");
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

        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(permiso, codUser);
        }


        public void buscarEventos(int tiket, string nombreEdificio, string nombretipoLLamada, string semana, string estado, string fechaDesde, string fechaHasta)
        {
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = Nevento.getAllEventoAreaCortizacionRepuesto(tiket, nombreEdificio, nombretipoLLamada, semana, estado, fechaDesde, fechaHasta);
            gv_tablaEventosAreaCotiRepuesto.DataSource = resultado;
            gv_tablaEventosAreaCotiRepuesto.DataBind();
            tx_cantidadEventos.Text = gv_tablaEventosAreaCotiRepuesto.Rows.Count.ToString();
            ponerColoresAequiposConPrioridad(gv_tablaEventosAreaCotiRepuesto);
        }

        public void ponerColoresAequiposConPrioridad(GridView gv_tablaDatosAux)
        {
            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++)
            {
                string Prioridad = gv_tablaEventosAreaCotiRepuesto.Rows[i].Cells[10].Text;
                if (Prioridad.Equals("Alta"))
                {
                    gv_tablaDatosAux.Rows[i].BackColor = Color.Red;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.White;
                }
                else
                    if (Prioridad.Equals("Media"))
                    {
                        gv_tablaDatosAux.Rows[i].BackColor = Color.Yellow;
                        gv_tablaDatosAux.Rows[i].ForeColor = Color.Black;
                    }
                    else
                        if (Prioridad.Equals("Baja"))
                        {
                            gv_tablaDatosAux.Rows[i].BackColor = Color.Green;
                            gv_tablaDatosAux.Rows[i].ForeColor = Color.Black;
                        }
                gv_tablaDatosAux.Rows[i].Cells[0].BackColor = Color.White;
                gv_tablaDatosAux.Rows[i].Cells[0].ForeColor = Color.Black;
            }
        }


        public void llenarDatos()
        {
            lb_numeroEvento.Text = gv_tablaEventosAreaCotiRepuesto.SelectedRow.Cells[1].Text;
            lb_numeroSemana.Text = gv_tablaEventosAreaCotiRepuesto.SelectedRow.Cells[2].Text;
            tx_fechaEvento.Text = gv_tablaEventosAreaCotiRepuesto.SelectedRow.Cells[4].Text;
            tx_horaEvento.Text = gv_tablaEventosAreaCotiRepuesto.SelectedRow.Cells[3].Text;
            tx_Regional.Text = Session["BaseDatos"].ToString();

            int codigoEvento = Convert.ToInt32(gv_tablaEventosAreaCotiRepuesto.SelectedRow.Cells[1].Text);
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = Nevento.getEvento(codigoEvento);
            tx_tipoEvento.Text = gv_tablaEventosAreaCotiRepuesto.SelectedRow.Cells[9].Text;
            tx_telefono.Text = resultado.Tables[0].Rows[0][5].ToString();
            tx_celular.Text = resultado.Tables[0].Rows[0][6].ToString();
            tx_Cliente.Text = resultado.Tables[0].Rows[0][4].ToString();
            tx_edificio.Text = resultado.Tables[0].Rows[0][7].ToString();
            tx_Direccion.Text = resultado.Tables[0].Rows[0][8].ToString();
            tx_Ascensores.Text = resultado.Tables[0].Rows[0][9].ToString();
            dd_estadoEvento.SelectedValue = resultado.Tables[0].Rows[0][10].ToString();
            tx_observacion.Text = resultado.Tables[0].Rows[0][11].ToString();
            //--------------------------------------------------------
            tx_defectoConstatadoEvento.Text = resultado.Tables[0].Rows[0][12].ToString();
            tx_observacionNecesidadRepuestoEvento.Text = resultado.Tables[0].Rows[0][13].ToString();
            tx_solicitudRepuestoEvento.Text = resultado.Tables[0].Rows[0][14].ToString();
            tx_envioProformaEvento.Text = resultado.Tables[0].Rows[0][15].ToString();
            tx_AceptacionProformaEvento.Text = resultado.Tables[0].Rows[0][16].ToString();
            tx_verificacionCambioEvento.Text = resultado.Tables[0].Rows[0][17].ToString();
            //---------------------------------------------------------
            tx_observacionEvento.Text = resultado.Tables[0].Rows[0][22].ToString();


            dd_estadoEvento.SelectedValue = resultado.Tables[0].Rows[0][10].ToString();

            string ascensorParado = resultado.Tables[0].Rows[0][20].ToString();
            CheckBox1_AscensorParado.Checked = Convert.ToBoolean(ascensorParado);

            string personasAtrapadas = resultado.Tables[0].Rows[0][21].ToString();
            CheckBox2_PersonasAtrapadas.Checked = Convert.ToBoolean(personasAtrapadas);

            string cambioRepuesto = resultado.Tables[0].Rows[0][24].ToString();
            ckb_cambioRespuesto.Checked = Convert.ToBoolean(cambioRepuesto);

            // string areacliente = resultado.Tables[0].Rows[0][30].ToString();
            // ckb_areaCliente.Checked = Convert.ToBoolean(areacliente);

            string Prioridad = resultado.Tables[0].Rows[0][23].ToString();
            dd_prioridad.SelectedValue = Prioridad;

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
                return "'" + _fecha + "'";
            }
            else
                return "null";
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
            Response.Redirect("../Presentacion/FA_CallCenter_AreaCotiRepuesto.aspx");
        }

        protected void bt_Buscar_Click(object sender, EventArgs e)
        {

        }


        private void llenarComboBoxTecnicoAsignado()
        {
            dd_tecnicosAsignados.Items.Clear();
            int codigoEvento = Convert.ToInt32(lb_numeroEvento.Text);
            NA_DetalleEventoTecnico Ndetalle = new NA_DetalleEventoTecnico();
            dd_tecnicosAsignados.DataSource = Ndetalle.getAllResponsablesAsignados(codigoEvento, 0);
            dd_tecnicosAsignados.DataValueField = "codigo";
            dd_tecnicosAsignados.DataTextField = "nombre";
            dd_tecnicosAsignados.Items.Add(new ListItem(""));
            dd_tecnicosAsignados.AppendDataBoundItems = true;
            dd_tecnicosAsignados.SelectedIndex = -1;
            dd_tecnicosAsignados.DataBind();
        }

        public void llenarDatosSupervisor()
        {

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
            else
            {
                tx_codDetalleTecnico.Text = "";
                tx_SupervisorAsignado.Text = "";
                tx_horaSupervisorAsignado.Text = "";
                tx_fechaSupervisorAsignado.Text = "";
            }

        }

    

        private void limpiarDatosTecnico()
        {
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

     

        protected void gv_tablaEventos_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDatos();
            llenarComboBoxTecnicoAsignado();
            llenarDatosSupervisor();
            limpiarDatosTecnico();
            ponerColoresAequiposConPrioridad(gv_tablaEventosAreaCotiRepuesto);
            if (gv_tablaEventosAreaCotiRepuesto.SelectedIndex > -1)
            {
                gv_tablaEventosAreaCotiRepuesto.SelectedRow.BackColor = Color.Lime;
            }
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
                Response.Write("<script type='text/javascript'> alert('El Supervisor ha sido Asignado Exitosamente') </script>");
            }
            else
            {
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

        protected void bt_AsignarSupervisor_Click(object sender, EventArgs e)
        {
            asignarSupervisor_ok();
            llenarDatosSupervisor();
            limpiarDatosTecnico();
        }


        private void AsignarTecnico()
        {
            string nombreTecnico = tx_tecnicoAsignado.Text;
            NA_Responsables Nresp = new NA_Responsables();
            int codigoTecnico = Convert.ToInt32(Nresp.getResponsable_SinExepcion(nombreTecnico).Tables[0].Rows[0][0].ToString());
            int codigoEvento = Convert.ToInt32(lb_numeroEvento.Text);
            NA_DetalleEventoTecnico nDetalleEventoTecnico = new NA_DetalleEventoTecnico();
            nDetalleEventoTecnico.insertar(codigoEvento, codigoTecnico, 0);
            Response.Write("<script type='text/javascript'> alert('El Tecnico ha sido Asignado Exitosamente') </script>");
        }

        protected void bt_AsignarTecnico_Click(object sender, EventArgs e)
        {
            AsignarTecnico();
            llenarComboBoxTecnicoAsignado();
            limpiarDatosTecnico();
        }


        private void llenarDatosTecnicoSeleccionado()
        {
            int codigoEvento = Convert.ToInt32(lb_numeroEvento.Text);
            //  int codigoTecnico = Convert.ToInt32(dd_tecnicosAsignados.SelectedValue.ToString());
            int codigoTecnicoAsignado = Convert.ToInt32(dd_tecnicosAsignados.SelectedValue.ToString());
            NA_DetalleEventoTecnico Ndetalle = new NA_DetalleEventoTecnico();
            DataSet resultado = Ndetalle.getAllResponsablesAsignados2(codigoEvento, codigoTecnicoAsignado, 0);
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


        protected void dd_tecnicosAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDatosTecnicoSeleccionado();
        }

        private void guardarDatosTecnicos()
        {
            int codigoEvento = Convert.ToInt32(lb_numeroEvento.Text);
            int codigoEventoTecnicoAsignado = Convert.ToInt32(dd_tecnicosAsignados.SelectedValue.ToString());

            NA_DetalleEventoTecnico Ndetalle = new NA_DetalleEventoTecnico();
            string horaAsignacion = tx_HoraTecnicoAsignacion.Text;
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


            if (Ndetalle.modificarDatosTecnico2(codigoEvento, codigoEventoTecnicoAsignado, horallegadaAlEdificio, horaSalidadelEdificio, estadoEntradaSalidaEdificio, observacionTecnico, fechaAsignacion, horaAsignacion, nroboleta, trabajoProgramado, ascensorReparado, fecha_horallegadaEdificio, fecha_horasalidaEdicio, fechaReporte, horaReporte))
            {
                Response.Write("<script type='text/javascript'> alert('Datos Guardados Correctamente') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error al Guardar') </script>");

        }


        protected void bt_datosTecnicos_Click(object sender, EventArgs e)
        {
            guardarDatosTecnicos();
        }


        private void guardarDatosEvento()
        {
            int codigoEvento = Convert.ToInt32(lb_numeroEvento.Text);
            NA_Evento Nevento = new NA_Evento();
            string observacionEvento = tx_observacionEvento.Text;
            string defectoContatadoEvento = tx_defectoConstatadoEvento.Text;
            string observacionNecesidadRepuestoEvento = tx_observacionNecesidadRepuestoEvento.Text;
            string solicitudRepuestoEvento = convertidorFecha(tx_solicitudRepuestoEvento.Text);
            string envioProformaEvento = convertidorFecha(tx_envioProformaEvento.Text);
            string aceptacionProformaEvento = convertidorFecha(tx_AceptacionProformaEvento.Text);
            string verificacionCambioEvento = tx_verificacionCambioEvento.Text;
            string estadoEvento = dd_estadoEvento.SelectedValue.ToString();            
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            string fechaEven = convertidorFecha(tx_fechaEvento.Text);
            string horaEven = "'" + tx_horaEvento.Text + "'";
            int codPrioridad = Convert.ToInt32(dd_prioridad.SelectedValue.ToString());

            bool cambioRepuesto = ckb_cambioRespuesto.Checked;            
            bool banderasolicitudCambioRepuesto = true;
            bool banderaAreaCliente = true;
            bool banderaCallcenter = false;
            bool banderaRin = false;
            bool banderaRCC = false;
            

            if (!aceptacionProformaEvento.Equals("null"))
            {
                 cambioRepuesto = true;
                 banderasolicitudCambioRepuesto = true;
                 banderaRin = true;
                 banderaAreaCliente = false;
                 banderaCallcenter = false;                 
                 banderaRCC = false;
            }


            DataSet dato = Nevento.getEvento(codigoEvento);
            string nombreEdificioReal = dato.Tables[0].Rows[0][7].ToString();

            if (tx_edificio.Text.Equals(nombreEdificioReal))
            {
                if (Nevento.estaCerradoelEvento(codigoEvento) == false)
                {
                    if (Nevento.modificarDatosEvento(codigoEvento, estadoEvento, observacionEvento, defectoContatadoEvento, observacionNecesidadRepuestoEvento, solicitudRepuestoEvento, envioProformaEvento, aceptacionProformaEvento, verificacionCambioEvento, cambioRepuesto, codUser, fechaEven, horaEven,banderasolicitudCambioRepuesto,banderaAreaCliente,banderaCallcenter,banderaRin,banderaRCC,codPrioridad))
                    {
                        //-----------------------historial                        
                        NA_Historial nhistorial = new NA_Historial();
                        nhistorial.insertar(codUser, "AreaCliente Modifico el Evento tiket = " + lb_numeroEvento.Text + " Estado = " + estadoEvento + " Cambio Repuesto= " + cambioRepuesto);
                        //----------------- historial----
                        Response.Write("<script type='text/javascript'> alert('Datos Guardado Correctamente') </script>");
                        Response.Redirect("../Presentacion/FA_CallCenterAreaCliente.aspx");
                    }
                    else
                        Response.Write("<script type='text/javascript'> alert('Error al Guardar') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: el Evento esta Cerrado') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Por Favor abrir solo una pagina') </script>");


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
            DataSet resultado = Nevento.getAllEventoAreaCortizacionRepuesto(tiket, tx_nombreEdificioBusqueda.Text, dd_tipoEvento1.SelectedItem.Text, tx_SemanaBusqueda.Text, dd_evento.SelectedItem.Text, fechaDesde, fechaHasta);

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