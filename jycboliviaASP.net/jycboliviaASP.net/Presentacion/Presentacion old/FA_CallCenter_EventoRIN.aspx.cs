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
    public partial class FA_CallCenter_EventoAntiguo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(53) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            lb_titulo.Text = "Eventos RIN " + Session["BaseDatos"].ToString();
            tx_BaseDeDatos.Text = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                buscarEventos(0,"", "", "", "Abierto","","");
                llenarTipoEvento();
                llenarPrioridad();
              //  negarAreaCliente();
            }

        }

     /*   private void negarAreaCliente()
        {
            if (tienePermisoDeIngreso(67) == true)
            {
                ckb_areaCliente.Enabled = true;
            }
            else
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

        protected void bt_Buscar_Click(object sender, EventArgs e)
        {
            string fechaDesde = convertidorFecha(tx_desdeBusqueda.Text);
            if (fechaDesde.Equals("null"))
                fechaDesde = "";

            string fechaHasta = convertidorFecha(tx_hastaBusqueda.Text);
            if (fechaHasta.Equals("null"))
                fechaHasta = "";
            int tiket = 0;
            if (!tx_tiketRIN.Text.Equals(""))
            {
                tiket = Convert.ToInt32(tx_tiketRIN.Text);
            }
            buscarEventos(tiket,tx_nombreEdificioBusqueda.Text, dd_tipoEvento1.SelectedItem.Text, tx_SemanaBusqueda.Text, dd_evento.SelectedItem.Text,fechaDesde,fechaHasta);
        }

        public void buscarEventos(int tiket,string nombreEdificio, string nombretipoLLamada, string semana, string estado, string fechaDesde, string fechaHasta)
        {
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = Nevento.getAllEventosRIN(tiket,nombreEdificio, nombretipoLLamada, semana, estado,1,fechaDesde,fechaHasta);
            gv_datosEvento.DataSource = resultado;
            gv_datosEvento.DataBind();
            tx_cantEventos.Text = gv_datosEvento.Rows.Count.ToString();
            ponerColoresAequiposConPrioridad(gv_datosEvento);
        }

        public void ponerColoresAequiposConPrioridad(GridView gv_tablaDatosAux)
        {
            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++)
            {
                string Prioridad = gv_datosEvento.Rows[i].Cells[10].Text;
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

        protected void gv_datosEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDatos();
            ponerColoresAequiposConPrioridad(gv_datosEvento);
            if (gv_datosEvento.SelectedIndex > -1)
            {
                gv_datosEvento.SelectedRow.BackColor = Color.Lime;
                llenarRepuestosSolicitados();
            }
        }

        private void llenarRepuestosSolicitados()
        {
            int codEvento;
            bool isnumeric = int.TryParse(gv_datosEvento.SelectedRow.Cells[1].Text,out codEvento);
            if(isnumeric){
            NA_Repuesto nrepuesto = new NA_Repuesto();
            DataSet tuplasRepuesto = nrepuesto.getRepuestosSolicitadosporEvento(codEvento);
            gv_repuestoSolicitado.DataSource = tuplasRepuesto;
            gv_repuestoSolicitado.DataBind();
            }
            

        }

      
        public void llenarDatos()
        {
            lb_numeroEvento.Text = gv_datosEvento.SelectedRow.Cells[1].Text;
            lb_numeroSemana.Text = gv_datosEvento.SelectedRow.Cells[2].Text;
            tx_horaEvento.Text = gv_datosEvento.SelectedRow.Cells[3].Text;
            tx_fechaEvento.Text = gv_datosEvento.SelectedRow.Cells[4].Text;
            
            tx_Regional.Text = Session["BaseDatos"].ToString();

            int codigoEvento = Convert.ToInt32(gv_datosEvento.SelectedRow.Cells[1].Text);
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = Nevento.getEvento(codigoEvento);
            tx_tipoEvento.Text = gv_datosEvento.SelectedRow.Cells[9].Text;
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

            string SolicitudRepuestoAux = resultado.Tables[0].Rows[0][29].ToString();
            Cbx_solicitudCambioRepuesto.Checked = Convert.ToBoolean(SolicitudRepuestoAux);

            string Prioridad = resultado.Tables[0].Rows[0][23].ToString();
            dd_prioridad.SelectedValue = Prioridad;

        }
       

        protected void bt_guardarBoton_Click(object sender, EventArgs e)
        {
            guardarDatosEvento();
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
            bool banderasolicitudRepuesto = Cbx_solicitudCambioRepuesto.Checked;
            bool banderaRin = true;
            bool banderaRCC = false;
            bool banderaCallcenter = false;
         //   bool banderaAreaCliente = ckb_areaCliente.Checked;
            bool banderaAreaCliente = false;
            
            if (banderaAreaCliente == true)
            {
                banderaRin = false;
                banderaRCC = false;
                banderaCallcenter = false;
                cambioRepuesto = true;
                banderasolicitudRepuesto = false;
            } 

            if (banderasolicitudRepuesto == true)
            {
                //solicitudRepuestoEvento = "'"+DateTime.Now.ToString("yyyy-MM-dd")+"'";
                solicitudRepuestoEvento = "now()";
                cambioRepuesto = true;
                banderaRCC = true;
                banderaRin = false;
                banderaAreaCliente = false;             
                banderaCallcenter = false;
            }
           
                
            DataSet dato = Nevento.getEvento(codigoEvento);
            string nombreEdificioReal = dato.Tables[0].Rows[0][7].ToString();

            if (tx_edificio.Text.Equals(nombreEdificioReal))
            {
                if (Nevento.estaCerradoelEvento(codigoEvento) == false)
                {
                    if (Nevento.modificarDatosEvento(codigoEvento, estadoEvento, observacionEvento, defectoContatadoEvento, observacionNecesidadRepuestoEvento, solicitudRepuestoEvento, envioProformaEvento, aceptacionProformaEvento, verificacionCambioEvento, cambioRepuesto, codUser, fechaEven, horaEven, banderasolicitudRepuesto,banderaAreaCliente,banderaCallcenter,banderaRin,banderaRCC,codPrioridad))
                    {
                        //-----------------------historial                        
                        NA_Historial nhistorial = new NA_Historial();
                        nhistorial.insertar(codUser, "RIN Modifico el Evento tiket = " + lb_numeroEvento.Text + ", Estado = " + estadoEvento + ", Cambio Repuesto= " + cambioRepuesto + ", AreaCliente= " + banderaAreaCliente);
                        //----------------- historial----
                        if (banderasolicitudRepuesto == true)
                        {
                            Session["codEvento"] = gv_datosEvento.SelectedRow.Cells[1].Text;
                            Session["nombreEdificioEvento"] = gv_datosEvento.SelectedRow.Cells[6].Text;
                            Session["banderaEvento"] = true;
                            Session["prioridadEvento"] = codPrioridad;
                            Response.Redirect("../Presentacion/FA_AdicionarCotizacionRepuesto.aspx");
                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'> alert('Datos Guardado Correctamente') </script>");
                            Response.Redirect("../Presentacion/FA_CallCenter_EventoRIN.aspx");
                        }

                    }
                    else
                        Response.Write("<script type='text/javascript'> alert('Error al Guardar') </script>");            
                }else
                    Response.Write("<script type='text/javascript'> alert('Error: No se puede Guardar el Evento esta Cerrado') </script>");            
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Por Favor Abrir solo 1 Pagina') </script>");            

               
                      
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
            if (!tx_tiketRIN.Text.Equals(""))
            {
                tiket = Convert.ToInt32(tx_tiketRIN.Text);
            }
            
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = Nevento.getAllEventosRIN(tiket,tx_nombreEdificioBusqueda.Text,  dd_tipoEvento1.SelectedItem.Text, tx_SemanaBusqueda.Text,dd_evento.SelectedItem.Text, 1, fechaDesde, fechaHasta);
          

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Eventos RIN - " + Session["BaseDatos"].ToString();
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