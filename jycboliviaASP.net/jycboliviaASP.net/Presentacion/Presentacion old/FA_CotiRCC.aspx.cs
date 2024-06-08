using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Drawing;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CotiRCC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(51) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if(!IsPostBack){
            buscarCotizacion("", "");
            cb_el.Checked = true;
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


        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string codigo = tx_codigo.Text;
            string edificio = tx_edificio.Text;            
            buscarCotizacion(codigo, edificio);
        }

        private void buscarCotizacion(string codigo, string edificio)
        {
            NA_Repuesto nrepuesto = new NA_Repuesto();
            DataSet dato = nrepuesto.getCotiRepuestoRCC2(codigo, edificio);

            gv_cotizacionRepuesto.DataSource = dato;
            gv_cotizacionRepuesto.DataBind();
            gv_cotizacionRepuesto.SelectedIndex = -1;

            gv_repuestoUne.DataSource = null;
            gv_repuestoUne.DataBind();
            ponerColoresAequiposConPrioridad(gv_cotizacionRepuesto);           
        }

        public void ponerColoresAequiposConPrioridad(GridView gv_tablaDatosAux)
        {
            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++)
            {
                string Prioridad = gv_tablaDatosAux.Rows[i].Cells[10].Text;
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


        public string aFecha(string fecha)
        {
            if (fecha == "" || fecha == "&nbsp;")
            {
                return fecha = "null";
            }

            else
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = "'" + anio + "/" + mes + "/" + dia + "'";
                return _fecha;
            }
        }

        protected void bt_crearCoti_Click(object sender, EventArgs e)
        {
            crearCotizacion();
        }


        public float getCantidadDistribuidaAlmacenesLocalJyCJyCia()
        {
            float cantResultado = 0;

            //Loop through all rows in GridView
            foreach (GridViewRow row in gv_repuestoUne.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 6; i < row.Cells.Count; i++)
                    {
                        string dato = row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.',',');
                        float cantidadAlmacen = Convert.ToSingle(dato);
                        cantResultado = cantResultado + cantidadAlmacen;                       
                    }

                    //--------------
                }
            }
            return cantResultado;
        }

        public bool getLasSumasCoincidenRepuestos() {
            bool bandera = true;
         
            //Loop through all rows in GridView
            foreach (GridViewRow row in gv_repuestoUne.Rows )
            {
                if (row.RowType == DataControlRowType.DataRow && bandera == true)
                {
                    float cantidadR = Convert.ToSingle(row.Cells[3].Text);
                    float cantResultado = 0;
                    for (int i = 6; i < row.Cells.Count; i++)
                    {
                        string dato = row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.',',');
                        float cantidadAlmacen = Convert.ToSingle(dato);
                        cantResultado = cantResultado + cantidadAlmacen;                       
                    }

                    if(cantidadR != cantResultado){
                    bandera = false;
                    }
                    //--------------
                }
            }
            return bandera;
        }

        private void crearCotizacion()
        {   

        if(!tx_envioProforma.Text.Equals("")){
            ///-------usuario
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            ///-------------------------
    
             NA_Repuesto nrepuesto = new NA_Repuesto();
             int codR144 = Convert.ToInt32(gv_cotizacionRepuesto.SelectedRow.Cells[2].Text);          
            
             float totalDB = nrepuesto.getTotalR144_Cotizacion(codR144);
             decimal totalDB_redondeado = Convert.ToDecimal(totalDB);
             totalDB_redondeado = Math.Round(totalDB_redondeado, 2);

             bool Bandera = getLasSumasCoincidenRepuestos();
             float totalApp = getCantidadDistribuidaAlmacenesLocalJyCJyCia();
             decimal totalApp_redondeado = Convert.ToDecimal(totalApp);
             totalApp_redondeado = Math.Round(totalApp_redondeado,2);

            if(totalDB_redondeado == totalApp_redondeado && Bandera == true ){
                // aki se va actualizar si todo esta bien
                 actualizarReparticionDeAlmacenesR144(codR144);
                //-----actualiza la fecha del envio de proforma  ---------
                int codEvento = nrepuesto.getCodigoEventoCallCenterAsignado(codR144);
                if(codEvento > -1){
                    string fechaEnvioProforma = aFecha(tx_envioProforma.Text);
                    nrepuesto.updateFechaEventoCotizacion(codEvento, fechaEnvioProforma);
                }
                //-----end envio de proforma ----------------------------

                //--------crear Cotizaciones con almacenes separados                
                crearCotizacionesConAlmacenesSeparadosPorAlmacenes(codR144);                
                //--------end crear Cotizaciones con almacenes separados
                //--------cerrar el R144 
                nrepuesto.cerrarR144(codR144,CodUser,"Cotizacion Cerrada por ser R-144");

                string codigo = tx_codigo.Text;
                string edificio = tx_edificio.Text;
                buscarCotizacion(codigo, edificio);
                Response.Write("<script type='text/javascript'> alert('OK : Guardado OK') </script>");  
                }else
                     Response.Write("<script type='text/javascript'> alert('ERROR: Datos') </script>");  

             }else
            Response.Write("<script type='text/javascript'> alert('Error: Fecha de Envio Proforma') </script>");  

        }

        

        private void crearCotizacionesConAlmacenesSeparadosPorAlmacenes(int codR144)
        {
            NA_Repuesto nrepuesto = new NA_Repuesto();
            bool tieneRepuestoLocal = nrepuesto.tieneRepuestosLocal(codR144);
            bool tieneRepuestoJYC = nrepuesto.tieneRepuestosJyC(codR144);
            bool tieneRepuestoJYCIA = nrepuesto.tieneRepuestosJyCIA(codR144);

            if(tieneRepuestoLocal){
                nrepuesto.crearCotizacionSeparadaAlmacenLocal(codR144);
            }

            if (tieneRepuestoJYC)
            {
                nrepuesto.crearCotizacionSeparadaAlmacenJYC(codR144);
            }

            if (tieneRepuestoJYCIA)
            {
                nrepuesto.crearCotizacionSeparadaAlmacenJYCIA(codR144);
            }
                        
        }

        private void actualizarReparticionDeAlmacenesR144(int codCoti)
        {               
            NA_Repuesto nrepuesto = new NA_Repuesto();
            //Loop through all rows in GridView
            foreach (GridViewRow row in gv_repuestoUne.Rows )
            {
                if (row.RowType == DataControlRowType.DataRow )
                {
                    int codRepuesto = Convert.ToInt32(row.Cells[0].Text);                                        
                    string almacen_Local = row.Cells[6].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace(',','.');
                    string almacen_JyC = row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace(',', '.');
                    string almacen_JyCIA = row.Cells[8].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace(',', '.');
                    nrepuesto.updateAlmacenesR144(codCoti,codRepuesto,almacen_Local,almacen_JyC,almacen_JyCIA);
                }
              }  
        }
        
        protected void cb_el_CheckedChanged(object sender, EventArgs e)
        {
            cb_ellos.Checked = false;
        }

        protected void cb_ellos_CheckedChanged(object sender, EventArgs e)
        {
            cb_el.Checked = false;
        }


        public void limpiar() {
            tx_cierre.Text = "";        
        }


        protected void bt_cerrarCotizacion_Click(object sender, EventArgs e)
        {
            CerrarCotizacionPendiente();
            limpiar();
            buscarCotizacion("", "");
        }



        private void CerrarCotizacionPendiente()
        {
            
            for (int i = 0; i < gv_cotizacionRepuesto.Rows.Count; i++)
            {
                int codigoCotizacion = Convert.ToInt32(gv_cotizacionRepuesto.Rows[i].Cells[2].Text);
                CheckBox cb = (CheckBox)gv_cotizacionRepuesto.Rows[i].Cells[1].FindControl("CheckBox1");
                string detalleCierre = tx_cierre.Text+" "+"(cierre en la parte de pendiente Cotizacion)";
                string estadoCoti = "Cerrado";
                bool banderaEliminado = cb.Checked;

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                if (banderaEliminado == true)
                {
                    NA_Repuesto nrepuesto = new NA_Repuesto();
                    bool bandera = nrepuesto.modificarCotizacionRepuesto(codigoCotizacion, estadoCoti, detalleCierre, false, banderaEliminado, false, codUser,false);
                    NA_Evento evento = new NA_Evento();

                    if (nrepuesto.tieneEventoCallCenterAsignado(codigoCotizacion))
                    {
                        int codEvento = nrepuesto.getCodigoEventoCallCenterAsignado(codigoCotizacion);                        
                        
                        if (!evento.updateCerrarEventoCotizacion(codEvento, codUser, detalleCierre))
                        {
                            Response.Write("<script type='text/javascript'> alert('Error: no se pudo Cerrar el evento') </script>");
                        }
                    }
                }

            }
        }

      
        protected void bt_R144_Click(object sender, EventArgs e)
        {
            if(gv_cotizacionRepuesto.SelectedIndex > -1){
                string rutaGuardarR144 = ConfigurationManager.AppSettings["guardar_r144"];
                if (!Directory.Exists(rutaGuardarR144))
                    Directory.CreateDirectory(rutaGuardarR144);

                int codigoCoti = Convert.ToInt32(gv_cotizacionRepuesto.SelectedRow.Cells[2].Text);
                string Edificio = gv_cotizacionRepuesto.SelectedRow.Cells[5].Text;
                string nombreArchivo = "R-144_coti" + codigoCoti + "_" + Edificio+".pdf";

                Session["CodigoR144"] = codigoCoti;
                Response.Redirect("../Presentacion/FA_Reporte_R144.aspx");     
               // descargarArchivo(rutaGuardarR144, nombreArchivo);
            }else
                Response.Write("<script type='text/javascript'> alert('ERROR: No Selecciono Cotizacion') </script>");
            
        }

        private void descargarArchivo(string RutaArchivo, string nombreArchivo)
        {
            try
            {
                if (!Directory.Exists(RutaArchivo))
                    Directory.CreateDirectory(RutaArchivo);

                RutaArchivo = RutaArchivo + "/" + nombreArchivo;

                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                string nombreAux = nombreArchivo.Replace(" ", "_");
                Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreAux);
                // Escribimos el fichero a enviar 
                Response.WriteFile(RutaArchivo);
                // volcamos el stream 
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
            }
            catch (Exception)
            {
                //  Response.Write("<script type='text/javascript'> alert('ERROR: " + ex.Message + "') </script>");
            }
        }


        private void seleccionarCotizacionRepuesto()
        {
            int codigoCotizacion = Convert.ToInt32(gv_cotizacionRepuesto.SelectedRow.Cells[2].Text);
            NA_Repuesto nrepuesto = new NA_Repuesto();
            DataSet dato = nrepuesto.getRepuestoCotizacionDetalleR114(codigoCotizacion);
            gv_repuestoUne.DataSource = dato;            
            gv_repuestoUne.DataBind();
            gv_cotizacionRepuesto.SelectedRow.BackColor = Color.Lime;
             
        }

        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            bool isUpdateVisible = false;
            // Label1.Text = string.Empty;

            //Loop through all rows in GridView
            foreach (GridViewRow row in gv_repuestoUne.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                  //  bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    bool isChecked = true;

                    //--------------  
                    //if (isChecked)
                        row.RowState = DataControlRowState.Edit;

                    for (int i = 6; i < row.Cells.Count; i++)
                    {
                        row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Visible = !isChecked;
                        if (row.Cells[i].Controls.OfType<TextBox>().ToList().Count > 0)
                        {
                            row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Visible = isChecked;
                            row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().BackColor = Color.Yellow;
                        }

                        if (isChecked && !isUpdateVisible)
                        {
                            isUpdateVisible = true;
                        }
                    }

                    //--------------
                }
            }
          //  bt_Update.Visible = isUpdateVisible;
        }

             

        protected void gv_cotizacionRepuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarCotizacionRepuesto();
            OnCheckedChanged(sender,e);

        }

        protected void gv_repuestoUne_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       




    }
}