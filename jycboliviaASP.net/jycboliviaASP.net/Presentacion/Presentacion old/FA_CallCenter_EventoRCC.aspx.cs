using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Drawing;
using System.IO;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CallCenter_EventoRCC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(54) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 


            lb_titulo.Text = "Eventos RCC " + Session["BaseDatos"].ToString();
            tx_BaseDeDatos.Text = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                buscarEventosRCC(0,"", "", "", "Abierto","","");
                llenarTipoEvento();
                negarAreaCliente();
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
        private void negarAreaCliente()
        {
            if (tienePermisoDeIngreso(67) == true)
            {
                ckb_areaCliente.Enabled = true;
            }
            else
                ckb_areaCliente.Enabled = false;
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

        public void ponerColoresAequiposConPrioridad(GridView gv_tablaDatosAux)
        {
            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++)
            {
                string Prioridad = gv_eventosRCC.Rows[i].Cells[10].Text;
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
            lb_numeroEvento.Text = gv_eventosRCC.SelectedRow.Cells[1].Text;
            lb_numeroSemana.Text = gv_eventosRCC.SelectedRow.Cells[2].Text;
            tx_fechaEvento.Text = gv_eventosRCC.SelectedRow.Cells[4].Text;
            tx_horaEvento.Text = gv_eventosRCC.SelectedRow.Cells[3].Text;
            tx_Regional.Text = Session["BaseDatos"].ToString();

            int codigoEvento = Convert.ToInt32(gv_eventosRCC.SelectedRow.Cells[1].Text);
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = Nevento.getEvento(codigoEvento);
            tx_tipoEvento.Text = gv_eventosRCC.SelectedRow.Cells[9].Text;
            tx_Cliente.Text = resultado.Tables[0].Rows[0][4].ToString();
            tx_telefono.Text = resultado.Tables[0].Rows[0][5].ToString();
            tx_celular.Text = resultado.Tables[0].Rows[0][6].ToString();
            tx_edificio.Text = resultado.Tables[0].Rows[0][7].ToString();
            tx_Direccion.Text = resultado.Tables[0].Rows[0][8].ToString();
            tx_Ascensores.Text = resultado.Tables[0].Rows[0][9].ToString();
            dd_estadoEvento.SelectedValue = resultado.Tables[0].Rows[0][10].ToString();
            tx_observacion.Text = resultado.Tables[0].Rows[0][11].ToString();

            tx_defectoConstatadoEvento.Text = resultado.Tables[0].Rows[0][12].ToString();
            tx_observacionNecesidadRepuestoEvento.Text = resultado.Tables[0].Rows[0][13].ToString();
            tx_solicitudRepuestoEvento.Text = resultado.Tables[0].Rows[0][14].ToString();
            tx_envioProformaEvento.Text = resultado.Tables[0].Rows[0][15].ToString();
            tx_AceptacionProformaEvento.Text = resultado.Tables[0].Rows[0][16].ToString();
            tx_verificacionCambioEvento.Text = resultado.Tables[0].Rows[0][17].ToString();
            tx_observacionEvento.Text = resultado.Tables[0].Rows[0][22].ToString();


            dd_estadoEvento.SelectedValue = resultado.Tables[0].Rows[0][10].ToString();

            string ascensorParado = resultado.Tables[0].Rows[0][20].ToString();
            CheckBox1_AscensorParado.Checked = Convert.ToBoolean(ascensorParado);

            string personasAtrapadas = resultado.Tables[0].Rows[0][21].ToString();
            CheckBox2_PersonasAtrapadas.Checked = Convert.ToBoolean(personasAtrapadas);

            string cambioRepuesto = resultado.Tables[0].Rows[0][24].ToString();
            ckb_cambioRespuesto.Checked = Convert.ToBoolean(cambioRepuesto);

           
            string fechaContactoCliente = resultado.Tables[0].Rows[0][31].ToString();
            tx_fechaContactoCliente.Text = fechaContactoCliente;

            string detalleContactoCliente = resultado.Tables[0].Rows[0][32].ToString();
            tx_detalleContactoCliente.Text = detalleContactoCliente;

            string Prioridad = resultado.Tables[0].Rows[0][23].ToString();
            dd_prioridad.SelectedValue = Prioridad;
        }


        public void buscarEventosRCC(int tiket, string nombreEdificio, string nombretipoLLamada, string semana, string estado, string fechaDesde, string fechaHasta)
        {
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = Nevento.getAllEventosRCC(tiket,nombreEdificio, nombretipoLLamada, semana, estado, 1,1, fechaDesde,  fechaHasta);
            gv_eventosRCC.DataSource = resultado;
            gv_eventosRCC.DataBind();
            ponerColoresAequiposConPrioridad(gv_eventosRCC);

            tx_cantidadgv.Text = gv_eventosRCC.Rows.Count.ToString();
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
            if (!tx_tiketRCC.Text.Equals(""))
            {
                tiket = Convert.ToInt32(tx_tiketRCC.Text);
            }
            buscarEventosRCC(tiket ,tx_nombreEdificioBusqueda.Text, dd_tipoEvento1.SelectedItem.Text, tx_SemanaBusqueda.Text, dd_evento.SelectedItem.Text, fechaDesde, fechaHasta);
        }

        protected void bt_guardarDatos_Click(object sender, EventArgs e)
        {
            guardarDatosEvento();
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

            string fechaContactoCliente = convertidorFecha(tx_fechaContactoCliente.Text);
            string detalleContactoCliente = tx_detalleContactoCliente.Text;

            
            bool cambioRepuesto = ckb_cambioRespuesto.Checked;
            bool banderaSolicitudRepuesto = true;
            bool banderaRCC = true;            
            bool banderaRin = false;
            bool banderaCallcenter = ckb_envioCallcenter.Checked;
            bool banderaAreaCliente = ckb_areaCliente.Checked;

            if (banderaCallcenter == true)
            {
                banderaRin = false;
                banderaRCC = false;
                banderaAreaCliente = false;
                banderaSolicitudRepuesto = false;
                cambioRepuesto = false;                
            }

            if (banderaAreaCliente == true)
            {
                banderaRin = false;
                banderaRCC = false;                
                banderaCallcenter = false;
                cambioRepuesto = true;
                banderaSolicitudRepuesto = false;                
            }


            if (!aceptacionProformaEvento.Equals("null"))
            {
                cambioRepuesto = true;
                banderaSolicitudRepuesto = false;
                banderaRin = true;
                banderaRCC = false;
                banderaCallcenter = false;
            }

            DataSet dato = Nevento.getEvento(codigoEvento);
            string nombreEdificioReal = dato.Tables[0].Rows[0][7].ToString();

            if (tx_edificio.Text.Equals(nombreEdificioReal))
            {
                if (Nevento.estaCerradoelEvento(codigoEvento) == false)
                {
                    if (Nevento.modificarDatosEvento(codigoEvento, estadoEvento, observacionEvento, defectoContatadoEvento, observacionNecesidadRepuestoEvento, solicitudRepuestoEvento, envioProformaEvento, aceptacionProformaEvento, verificacionCambioEvento, cambioRepuesto, codUser, fechaEven, horaEven, banderaSolicitudRepuesto,banderaAreaCliente,banderaCallcenter,banderaRin,banderaRCC,codPrioridad))
                    {
                        Nevento.modificarDatosEvento_FechaContactoCliente(codigoEvento, fechaContactoCliente, detalleContactoCliente);
                        //-----------------------historial                        
                        NA_Historial nhistorial = new NA_Historial();
                        nhistorial.insertar(codUser, "RCC Modifico el Evento tiket = " + lb_numeroEvento.Text + ", Estado = " + estadoEvento + ", Cambio Repuesto= " + cambioRepuesto + ", EnvioCallCenter= " + banderaCallcenter + ", AreaCliente= " + banderaAreaCliente);
                        //----------------- historial----

                        Response.Write("<script type='text/javascript'> alert('Datos Guardado Correctamente') </script>");
                        Response.Redirect("../Presentacion/FA_CallCenter_EventoRCC.aspx");
                    }
                    else
                        Response.Write("<script type='text/javascript'> alert('Error al Guardar') </script>");                
                }else
                    Response.Write("<script type='text/javascript'> alert('Error: el Evento esta Cerrado') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Por Favor abrir solo una pagina') </script>");


        }



        protected void gv_eventosRCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDatos();
            ponerColoresAequiposConPrioridad(gv_eventosRCC);
            if (gv_eventosRCC.SelectedIndex > -1)
            {
                gv_eventosRCC.SelectedRow.BackColor = Color.Lime;
            }
        }

        protected void lk_excelEventos_Click(object sender, EventArgs e)
        {
                NA_Evento Nevento = new NA_Evento();

                string fechaDesde = convertidorFecha(tx_desdeBusqueda.Text);
                if (fechaDesde.Equals("null"))
                    fechaDesde = "";

                string fechaHasta = convertidorFecha(tx_hastaBusqueda.Text);
                if (fechaHasta.Equals("null"))
                    fechaHasta = "";
                int tiket = 0;
                if (!tx_tiketRCC.Text.Equals(""))
                {
                    tiket = Convert.ToInt32(tx_tiketRCC.Text);
                }

                DataSet resultado = Nevento.getAllEventosRCC(tiket,tx_nombreEdificioBusqueda.Text, dd_tipoEvento1.SelectedItem.Text, tx_SemanaBusqueda.Text, dd_evento.SelectedItem.Text, 1, 1,fechaDesde,fechaHasta);
            
                //// Creacion del Excel
                HttpResponse response = HttpContext.Current.Response;
                // first let's clean up the response.object
                response.Clear();
                response.Charset = "";
                // set the response mime type for excel
                response.ContentType = "application/vnd.ms-excel";
                string nombre = "Eventos RCC - "+Session["BaseDatos"].ToString();
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