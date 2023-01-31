using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Globalization;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ReporteReciboIngreso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                int codigoReciboIngreso = Convert.ToInt32(Session["codigoReciboIngreso"].ToString());
                mostrarReciboIngreso(codigoReciboIngreso);
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

        private void mostrarReciboIngreso(int codigoReciboIngreso)
        {
            NA_Recibo_IngresoEgreso nie = new NA_Recibo_IngresoEgreso();
            DataSet datoResult = nie.getDatosReciboIngreso(codigoReciboIngreso);

            string ciudad = Session["BaseDatos"].ToString();
            //string nroboleta = datoResult.Tables[0].Rows[0][0].ToString();
            string nroboleta = datoResult.Tables[0].Rows[0][11].ToString();

            string fecha = datoResult.Tables[0].Rows[0][1].ToString();
            string cliente = datoResult.Tables[0].Rows[0][3].ToString();
            float montotal;
            float.TryParse(datoResult.Tables[0].Rows[0][4].ToString(), out montotal);
            string moneda = datoResult.Tables[0].Rows[0][5].ToString();
            string cheque = datoResult.Tables[0].Rows[0][6].ToString();
            string concepto = datoResult.Tables[0].Rows[0][7].ToString();
            string detalle = datoResult.Tables[0].Rows[0][8].ToString();
            string responsable = datoResult.Tables[0].Rows[0][9].ToString();
            string facturanro = datoResult.Tables[0].Rows[0][10].ToString();

            ReportParameter p_nrocomprobante = new ReportParameter("p_nrocomprobante", nroboleta);
            string fechaLetras = convertidorFechaLetras(fecha);
            ReportParameter p_fecharecibo = new ReportParameter("p_fecharecibo", fechaLetras);
            ReportParameter p_cliente = new ReportParameter("p_cliente", cliente);
            ReportParameter p_facturanro = new ReportParameter("p_facturanro", facturanro);
            

            N_numLetra nl = new N_numLetra();
            string precioLetras = nl.Convertir(datoResult.Tables[0].Rows[0][4].ToString(), true, moneda);
            ReportParameter p_montoTotalLetras = new ReportParameter("p_montoTotalLetras", precioLetras);
            ReportParameter p_montoTotal = new ReportParameter("p_montoTotal", montotal.ToString().Replace('.',','));
            ReportParameter p_detalle = new ReportParameter("p_detalle", detalle);
            ReportParameter p_nrocheque = new ReportParameter("p_nrocheque", cheque);
            ReportParameter p_concepto = new ReportParameter("p_concepto", concepto);
            
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

            string rutaReciboIngreso = ConfigurationManager.AppSettings["repo_reciboIngreso"];
            
            ReportViewer1.LocalReport.ReportPath = rutaReciboIngreso;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      

            ReportViewer1.LocalReport.SetParameters(p_nrocomprobante);
            ReportViewer1.LocalReport.SetParameters(p_fecharecibo);
            ReportViewer1.LocalReport.SetParameters(p_cliente);
            ReportViewer1.LocalReport.SetParameters(p_montoTotalLetras);
            ReportViewer1.LocalReport.SetParameters(p_montoTotal);
            ReportViewer1.LocalReport.SetParameters(p_detalle);
            ReportViewer1.LocalReport.SetParameters(p_nrocheque);
            ReportViewer1.LocalReport.SetParameters(p_concepto);
            ReportViewer1.LocalReport.SetParameters(p_facturanro);
            
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