using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;
using System.IO;
using jycboliviaASP.net.Reportes;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CodigosDeEquipos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

         /*  if (tienePermisoDeIngreso(58) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            */
            if (!IsPostBack)
            {
                //  Cargaredificios("");
                //  VerPlanPagos(gv_edificionPlanPago);
            }
        }


        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string exbo = tx_exbo.Text;
            string edificio = tx_edificio.Text;
            buscarCodigoQRdelEdificio(exbo, edificio);
        }

        private void buscarCodigoQRdelEdificio(string exbo, string edificio)
        {
            NEquipo eq = new NEquipo();
           DataSet tuplas = eq.getConsultaCodigoDeAutenticacion(exbo, edificio);
           gv_CodificacionDeEdificio.DataSource = tuplas;
           gv_CodificacionDeEdificio.DataBind();
        }

      

        protected void gv_CodificacionDeEdificio_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow){
                Image image = (Image)e.Row.FindControl("imagenQR");
               // image.ImageUrl = "~/Images/jyc_1.jpg";

                // Se carga la ruta física de la carpeta temp del sitio            
                string ruta = ConfigurationManager.AppSettings["qr_codeEquipo"] + Session["BaseDatos"].ToString();
                string ruta2 = ConfigurationManager.AppSettings["qr_codeEquipoDibujo"] + Session["BaseDatos"].ToString();
                
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                string edificio = e.Row.Cells[0].Text;
                string exbo = e.Row.Cells[2].Text;
                string marca = e.Row.Cells[5].Text;
                string tipo = e.Row.Cells[6].Text;
                string parada = e.Row.Cells[7].Text;
                string pasajero = e.Row.Cells[8].Text;
                string velocidad = e.Row.Cells[9].Text;
                string modelo = e.Row.Cells[10].Text;

                string nombreArchivo = edificio + "_" + exbo;
                string contendidoQR = edificio + "|" + exbo + "|" + modelo + "|";
                string DirArchivo = ruta + "/" + nombreArchivo;
                string DirArchivoDibujo = ruta2 + "/" + nombreArchivo;

                if(!System.IO.File.Exists(DirArchivo+".jpg")){
                    NA_QRCodeNet qr = new NA_QRCodeNet();                   
                    qr.CrearImagenQR(DirArchivo, contendidoQR, 5);
                }
               

                image.ImageUrl = DirArchivoDibujo+".jpg";
                // image.ImageUrl = "~/QR_CodeEquipo/Prueba/2_0_VIMEC-.jpg";
               // image.ImageUrl = "D://PROYECTOS JYC/CuadroXXX/jycboliviaASP.net/jycboliviaASP.net/QR_CodeEquipo/Prueba/4 HERMANOS_41330-.jpg";
               
            }
        }

     
        protected void bt_excel_Click(object sender, EventArgs e)
        {
            exportarEn_Excel2();
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
            string nombreArchivo = "Codigos de Equipos QR " + Session["BaseDatos"].ToString() + " " + DateTime.Now;
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombreArchivo + ".xls" + "\"");

            GridView dg = gv_CodificacionDeEdificio;
            dg.GridLines = GridLines.Both;
            dg.HeaderStyle.Font.Bold = true;
            dg.Columns[0].Visible = false;
            dg.RenderControl(htmltextwrtter);

            Response.Write(strwritter.ToString());
            Response.End();
        }

    }
}