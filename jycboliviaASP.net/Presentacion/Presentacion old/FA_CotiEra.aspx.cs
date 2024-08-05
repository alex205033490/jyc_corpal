using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CotiEra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(69) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                buscarCotizacion("", "","");
               
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

        private void buscarCotizacion(string codigoCotiR, string edificio, string cite)
        {
            NA_Repuesto nrepuesto = new NA_Repuesto();
            DataSet dato;
            dato = nrepuesto.getCotizacionesRepuesto_EraEntrega(codigoCotiR, edificio, cite);
            lb_cantidad.Text = dato.Tables[0].Rows.Count.ToString();
            gv_vistaCoti.DataSource = dato;
            gv_vistaCoti.DataBind();
            ponerColoresAequiposConPrioridad(gv_vistaCoti);
            gv_vistaCoti.SelectedIndex = -1;
        }

        public void ponerColoresAequiposConPrioridad(GridView gv_tablaDatosAux)
        {
            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++)
            {
                string Prioridad = gv_tablaDatosAux.Rows[i].Cells[16].Text;
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

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string codCoti = tx_codigoCoti.Text;
            string edificio = tx_edificio.Text;
            string cite = tx_cite.Text;
            buscarCotizacion(codCoti, edificio, cite);
        }


        private void mostrarTodoslosRepuestosdelaCotizacion()
        {
            int codigoCotizacion = Convert.ToInt32(gv_vistaCoti.SelectedRow.Cells[1].Text);
            NA_Repuesto nrepuesto = new NA_Repuesto();
            DataSet dato = nrepuesto.getRepuestoCotizacionDetalle(codigoCotizacion);
            gv_datos.DataSource = dato;
            gv_datos.DataBind();
            gv_vistaCoti.SelectedRow.BackColor = Color.Lime;

            
        }
        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            bool isUpdateVisible = false;
            // Label1.Text = string.Empty;

            //Loop through all rows in GridView
            foreach (GridViewRow row in gv_datos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    //--------------  
                    if (isChecked)
                        row.RowState = DataControlRowState.Edit;

                    for (int i = 5; i < row.Cells.Count; i++)
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
            bt_Update.Visible = isUpdateVisible;
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

        private void actualizarTodo()
        {
            foreach (GridViewRow row in gv_datos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked)
                    {
                        int codigoCoti = Convert.ToInt32(gv_vistaCoti.SelectedRow.Cells[1].Text);
                        int codigoRepuesto = Convert.ToInt32(row.Cells[1].Text);                        
                        string fechaentrega_ERA = convertidorFecha(row.Cells[5].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        string nroSerial = row.Cells[6].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        string nroFactura = row.Cells[7].Controls.OfType<TextBox>().FirstOrDefault().Text;

                        NA_Repuesto nrepuesto = new NA_Repuesto();
                        bool actualizado = nrepuesto.actualizar_DatosEra(codigoCoti,codigoRepuesto,fechaentrega_ERA,nroSerial,nroFactura);
                        if (actualizado == true)
                            row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = false;
                    }
                }
            }            
           
        }

        protected void bt_Update_Click(object sender, EventArgs e)
        {
            if(gv_vistaCoti.SelectedIndex >= 0 ){
                actualizarTodo();
                sitodoslosrepuestosentregados_pasaralRin();
                string codCoti = tx_codigoCoti.Text;
                string edificio = tx_edificio.Text;
                string cite = tx_cite.Text;
                buscarCotizacion(codCoti, edificio, cite);
                if (gv_vistaCoti.SelectedIndex > -1)
                {
                    mostrarTodoslosRepuestosdelaCotizacion();
                }
                else {
                    gv_datos.DataSource = null;
                    gv_datos.DataBind();
                }                
            }
            
        }

        private void sitodoslosrepuestosentregados_pasaralRin()
        {
            int codCoti = Convert.ToInt32(gv_vistaCoti.SelectedRow.Cells[1].Text);
            NA_Repuesto nrep = new NA_Repuesto();
            int cantRepuesto = nrep.getCantidadRepuestodeCotizacion(codCoti);
            if(cantRepuesto > 0){
                int cantRepuestosEntregados = nrep.getCantidadRepuesto_Entregados(codCoti);
                if(cantRepuesto == cantRepuestosEntregados){
                    bool bandera2 = nrep.cerrarCotizacion_era2(codCoti);
                    if (nrep.tieneEventoCallCenterAsignado(codCoti))
                    {
                        bool bandera = nrep.pasarEventoalRIN_evento(codCoti);                        
                        if(bandera)
                            Response.Write("<script type='text/javascript'> alert('Evento: Area Rin') </script>");

                   }
                    /// si no tiene cerrar la cotizacion.
                }
            }
        }

        protected void gv_datos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatosItem();
        }

        private void limpiarDatosItem()
        {
            tx_cite.Text = "";
            tx_codigoCoti.Text = "";
            tx_edificio.Text = "";
        }

        protected void gv_vistaCoti_SelectedIndexChanged1(object sender, EventArgs e)
        {
            mostrarTodoslosRepuestosdelaCotizacion();
            OnCheckedChanged_seleccionTodo(sender, e);
        }

        protected void OnCheckedChanged_seleccionTodo(object sender, EventArgs e)
        {
            bool isUpdateVisible = false;
            // Label1.Text = string.Empty;

            //Loop through all rows in GridView
            foreach (GridViewRow row in gv_datos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    //  bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    bool isChecked = true;
                    row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = true;
                    //--------------  
                    //if (isChecked)
                    row.RowState = DataControlRowState.Edit;

                    for (int i = 5; i < row.Cells.Count; i++)
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

        protected void bt_Exportar_Click(object sender, EventArgs e)
        {
            exportarEnExcel();
        }

        public void exportarEnExcel() {

            string codCoti = tx_codigoCoti.Text;
            string edificio = tx_edificio.Text;
            string cite = tx_cite.Text;
            NA_Repuesto nrepuesto = new NA_Repuesto();
            DataSet dato;
            dato = nrepuesto.getCotizacionesRepuesto_EraEntrega(codCoti, edificio, cite);


            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Repuesto ERA " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = dato;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }
        
        }

    }
}