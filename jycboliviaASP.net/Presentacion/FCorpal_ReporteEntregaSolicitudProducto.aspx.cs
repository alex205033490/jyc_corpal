using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using jycboliviaASP.net.Negocio;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ReporteEntregaSolicitudProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                int codigoEntregaSolicitudProducto = Convert.ToInt32(Session["codigoEntregaSolicitudProducto"].ToString());
                mostrarEntregaSolicitudProducto(codigoEntregaSolicitudProducto);
            }
        }

        public string convertidorFechaLetras(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = fecha_.ToString("dd MMMM yyy", CultureInfo.CreateSpecificCulture("es-ES"));
                return _fecha;
            }
            else
                return "null";
        }

        private void mostrarEntregaSolicitudProducto(int codigoEntregaSolicitudProducto)
        {
            NCorpal_SolicitudEntregaProducto nie = new NCorpal_SolicitudEntregaProducto();
            DataSet datoResult = nie.get_entregaSolicitudProductos(codigoEntregaSolicitudProducto);

            string ciudad = Session["BaseDatos"].ToString();
            //string nroboleta = datoResult.Tables[0].Rows[0][0].ToString();
            string nrodocumento = HttpUtility.HtmlDecode(datoResult.Tables[0].Rows[0][1].ToString());
            string fechasolicitud = datoResult.Tables[0].Rows[0][2].ToString();
            string horasolicitud = datoResult.Tables[0].Rows[0][3].ToString();
            string nombresolicitante = HttpUtility.HtmlDecode(datoResult.Tables[0].Rows[0][4].ToString());
            string entrego = HttpUtility.HtmlDecode(datoResult.Tables[0].Rows[0][5].ToString());
            string Cliente = HttpUtility.HtmlDecode(datoResult.Tables[0].Rows[0][6].ToString());

            DataSet tuplasFilas = nie.get_productosEngregaSolicitudProducto(codigoEntregaSolicitudProducto);
            DataTable DSProductosAlmacen = tuplasFilas.Tables[0];

            ReportParameter p_nrocomprobante = new ReportParameter("p_nrodocumento", nrodocumento);
            ReportParameter p_fechasolicitud = new ReportParameter("p_fechasolicitud", fechasolicitud);
            ReportParameter p_nombresolicitante = new ReportParameter("p_nombresolicitante", nombresolicitante);
            ReportParameter p_horasolicitud = new ReportParameter("p_horasolicitud", horasolicitud);
            ReportParameter p_entrego = new ReportParameter("p_entrego", entrego);
            ReportParameter p_cliente = new ReportParameter("p_cliente", Cliente);
            ReportDataSource DS_ProductosAlmacen = new ReportDataSource("DS_ProductosAlmacen", DSProductosAlmacen);
                                    
            //ReportParameter p_edificio = new ReportParameter("p_edificio", HttpUtility.HtmlDecode(datoResult.Tables[0].Rows[0][3].ToString()));
            /*
            string ruta = ConfigurationManager.AppSettings["image_logo"];
            string nombreImagen = "jyc";
            string baseDatos = Session["BaseDatos"].ToString();

            if (baseDatos.Equals("La Paz"))
            {
                nombreImagen = "elevamerica";
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    nombreImagen = "melevar";
                }
                else
                    if (baseDatos.Equals("Santa Cruz") || baseDatos.Equals("Beni") || baseDatos.Equals("Pando") || baseDatos.Equals("Yacuiba"))
                    {
                        nombreImagen = "interlogy";
                    }

            string direccionImagen = ruta + nombreImagen;

            ReportParameter imagen = new ReportParameter("p_logo", @"file:\" + direccionImagen + ".jpg");
            //  ReportParameter imagen = new ReportParameter("p_logo", "d:/temp/alex.jpg");
            */

            string rutaEntregaSolicitudProducto = ConfigurationManager.AppSettings["repo_entregaSolicitudProducto"];

            ReportViewer1.LocalReport.ReportPath = rutaEntregaSolicitudProducto;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      

            ReportViewer1.LocalReport.SetParameters(p_nrocomprobante);
            ReportViewer1.LocalReport.SetParameters(p_fechasolicitud);
            ReportViewer1.LocalReport.SetParameters(p_horasolicitud);
            ReportViewer1.LocalReport.SetParameters(p_nombresolicitante);
            ReportViewer1.LocalReport.SetParameters(p_entrego);
            ReportViewer1.LocalReport.SetParameters(p_cliente);
            ReportViewer1.LocalReport.DataSources.Add(DS_ProductosAlmacen);

            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
            /*
                        Warning[] warnings;
                        string[] streamIds;
                        string mimeType = string.Empty;
                        string encoding = string.Empty;
                        // string encoding = System.Text.Encoding.Default.ToString();
                        string extension = string.Empty;

                        byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                        //  byte[] bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.          
                        // System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        //----------------------- enviar al cliente el archivo -----------------------
                        /* Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        Response.AddHeader("content-disposition", "attachment; filename= filename" + "." + extension);
                        Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
                        Response.Flush(); // send it to the client to download  
                        Response.End();*/
            //------------------------------------------------------------------------------

            /*     string rutaGuardarR144 = ConfigurationManager.AppSettings["guardar_r144"];
                 if (!Directory.Exists(rutaGuardarR144))
                     Directory.CreateDirectory(rutaGuardarR144);

                 int codigoCoti = Convert.ToInt32(Session["codcotiRepuesto"].ToString());
                 string Edificio = Session["EdificioRepuesto"].ToString();
                 string nombreArchivo = "R-144_coti" + codigoCoti + "_" + Edificio;
                 string direccionGuardarR144 = rutaGuardarR144 + nombreArchivo;

                 using (FileStream fs = new FileStream(@direccionGuardarR144 + "." + extension, FileMode.Create))
                 {
                     fs.Write(bytes, 0, bytes.Length);
                 }
                 */
        }

    }
}