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
    public partial class FA_EdificioDeudor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (tienePermisoDeIngreso(35) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if(!IsPostBack){
            Cargaredificios("");
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
            Cargaredificios(tx_edificio.Text);
        }

        private void Cargaredificios(string nombreEdificio)
        {
            NProyecto nproyecto = new NProyecto();
            DataSet datos = nproyecto.buscar(nombreEdificio);
            gv_tablaEdificios.DataSource = datos;
            gv_tablaEdificios.DataBind();
            ponerColoresAequiposConDeuda(gv_tablaEdificios);
        }


        public void ponerColoresAequiposConDeuda(GridView gv_tablaDatosAux)
        {
            NProyecto nproyec = new NProyecto();

            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++)
            {
                int codigoEdificio = Convert.ToInt32(gv_tablaDatosAux.Rows[i].Cells[2].Text);
                if (nproyec.tieneDeudasPendientesProyecto(codigoEdificio))
                {
                    gv_tablaDatosAux.Rows[i].BackColor = Color.Red;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.White;
                    CheckBox cb = (CheckBox)gv_tablaEdificios.Rows[i].Cells[1].FindControl("CheckBox1");
                    cb.Checked = true;
                }

                if (nproyec.tieneDeudasRepuestoPendientesProyecto(codigoEdificio)){
                    gv_tablaDatosAux.Rows[i].BackColor = Color.Red;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.White;
                    CheckBox cb = (CheckBox)gv_tablaEdificios.Rows[i].Cells[2].FindControl("CheckBox2");
                    cb.Checked = true;                
                }

            }

        }

        protected void bt_marcados_Click(object sender, EventArgs e)
        {
            marcarEdificios();
            Cargaredificios(tx_edificio.Text);
        }

        private void marcarEdificios()
        {
            NProyecto Nproy = new NProyecto();           
            
            for (int i = 0; i < gv_tablaEdificios.Rows.Count; i++)
            {
                int codigoEdificio = Convert.ToInt32(gv_tablaEdificios.Rows[i].Cells[2].Text);
                CheckBox cb = (CheckBox)gv_tablaEdificios.Rows[i].Cells[1].FindControl("CheckBox1");
                CheckBox cb2 = (CheckBox)gv_tablaEdificios.Rows[i].Cells[2].FindControl("CheckBox2");
                bool bandera = cb.Checked;
                bool bandera2 = cb2.Checked;
                Nproy.modificarDeudaProyecto(codigoEdificio, bandera);
                Nproy.modificarDeudaRepuestoProyeto(codigoEdificio, bandera2);
            }         
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            exportarEn_Excel();
        }


        private void exportarEn_Excel()
        {

            NProyecto nproyecto = new NProyecto();
            DataSet datos = nproyecto.buscar_Deuda_IntalacionRepueso(tx_edificio.Text);
            
            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Edificios Morosos " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = datos;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        private void exportarEn_Excel2()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string nombreArchivo = "Edificios Morosos " + Session["BaseDatos"].ToString() + " " + DateTime.Now;
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombreArchivo + ".xls" + "\"");

            GridView dg = gv_tablaEdificios;
            dg.GridLines = GridLines.Both;
            dg.HeaderStyle.Font.Bold = true;
            //dg.Columns[0].Visible = false;
            dg.RenderControl(htmltextwrtter);

            Response.Write(strwritter.ToString());
            Response.End();
        }

    }
}