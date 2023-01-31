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
    public partial class FA_ViewSeguimientoInstalacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(21) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if(!IsPostBack){
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

        public void buscarDatos()
        {
            NA_SeguimientoInstalacion seguiInstN = new NA_SeguimientoInstalacion();
            DataSet tuplasTabla = seguiInstN.BuscarDatos(tx_exbo.Text, tx_nombreEdificio.Text);
            gv_seguimientoInstalacion.DataSource = tuplasTabla;
            gv_seguimientoInstalacion.DataBind();

        }

        public void mostrarSeguimientoInstalacion()
        {
            NA_SeguimientoInstalacion seguiInstN = new NA_SeguimientoInstalacion();
            DataSet tuplasTabla = seguiInstN.BuscarDatos("","");
            gv_seguimientoInstalacion.DataSource = tuplasTabla;
            gv_seguimientoInstalacion.DataBind();
        }

        protected void bt_Buscar_Click(object sender, EventArgs e)
        {
            buscarDatos();
        }

        protected void lk_excel_Click(object sender, EventArgs e)
        {

            NA_SeguimientoInstalacion seguiInstN = new NA_SeguimientoInstalacion();
            DataSet tuplasTabla = seguiInstN.BuscarDatos(tx_exbo.Text, tx_nombreEdificio.Text);
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

    }
}