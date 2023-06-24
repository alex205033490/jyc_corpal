using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ReporteGeneral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {                
                mostrarReporteGeneral();
            }
        }

        private void mostrarReporteGeneral()
        {
            string reporte = Session["ReporteGeneral"].ToString();
                switch (reporte)
                {
                    case "Reporte_Entrega_Produccion":
                        entregaProduccion();
                        break;
                    case "otro2":
                       //number = "Two";
                        break;
                    default:
                        //number = "Error";
                        break;
                }
        }

        private void entregaProduccion()
        {
            int codigoEntregaProduccion = int.Parse(Session["codigoEntregaProduccion"].ToString());

            NCorpal_Produccion npro = new NCorpal_Produccion();
            DataSet datoResult = npro.get_DatosEntregaProduccion(codigoEntregaProduccion);

            string ciudad = Session["BaseDatos"].ToString();
            //string nroboleta = datoResult.Tables[0].Rows[0][0].ToString();
            string nroboleta = datoResult.Tables[0].Rows[0][18].ToString();
            string fecha = datoResult.Tables[0].Rows[0][19].ToString();
            //string fecha = datoResult.Tables[0].Rows[0][1].ToString();
            string pagadoha = datoResult.Tables[0].Rows[0][3].ToString();
            float montotal;
            float.TryParse(datoResult.Tables[0].Rows[0][4].ToString(), out montotal);
            string moneda = datoResult.Tables[0].Rows[0][5].ToString();
            string cheque = datoResult.Tables[0].Rows[0][6].ToString();
            string banco = datoResult.Tables[0].Rows[0][7].ToString();
            string efectivo = datoResult.Tables[0].Rows[0][8].ToString();
            string concepto = datoResult.Tables[0].Rows[0][9].ToString();
            string detalle = datoResult.Tables[0].Rows[0][10].ToString();
            string responsable = datoResult.Tables[0].Rows[0][11].ToString();
            string porcentajeretencioniue = datoResult.Tables[0].Rows[0][12].ToString();
            string porcentajeretencionit = datoResult.Tables[0].Rows[0][13].ToString();
            string retencioniuebs = datoResult.Tables[0].Rows[0][14].ToString();
            string retencionitbs = datoResult.Tables[0].Rows[0][15].ToString();
            string totalapagar = datoResult.Tables[0].Rows[0][16].ToString();
            string facturanro = datoResult.Tables[0].Rows[0][17].ToString();


            ReportParameter p_nrocomprobante = new ReportParameter("p_nrocomprobante", nroboleta);
            ReportParameter p_fecharecibo = new ReportParameter("p_fecharecibo", fecha);
            ReportParameter p_pagadoha = new ReportParameter("p_pagadoha", pagadoha);
            ReportParameter p_facturanro = new ReportParameter("p_facturanro", facturanro);


            N_numLetra nl = new N_numLetra();
            string precioLetras = nl.Convertir(datoResult.Tables[0].Rows[0][4].ToString(), true, moneda);
            ReportParameter p_montoTotalLetras = new ReportParameter("p_montoTotalLetras", precioLetras);
            ReportParameter p_montoTotal = new ReportParameter("p_montoTotal", montotal.ToString().Replace('.', ','));

            ReportParameter p_nrocheque = new ReportParameter("p_nrocheque", cheque);
            ReportParameter p_concepto = new ReportParameter("p_concepto", concepto);

            ReportParameter p_banco = new ReportParameter("p_banco", banco);
            ReportParameter p_efectivo = new ReportParameter("p_efectivo", efectivo);
            ReportParameter p_retencionIUE = new ReportParameter("p_retencionIUE", retencioniuebs);
            ReportParameter p_retencionIT = new ReportParameter("p_retencionIT", retencionitbs);
            ReportParameter p_totalapagar = new ReportParameter("p_totalapagar", totalapagar);


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

            string rutaReciboIngreso = ConfigurationManager.AppSettings["repo_reciboEgreso"];

            ReportViewer1.LocalReport.ReportPath = rutaReciboIngreso;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      

            ReportViewer1.LocalReport.SetParameters(p_nrocomprobante);
            ReportViewer1.LocalReport.SetParameters(p_fecharecibo);
            ReportViewer1.LocalReport.SetParameters(p_pagadoha);
            ReportViewer1.LocalReport.SetParameters(p_montoTotalLetras);
            ReportViewer1.LocalReport.SetParameters(p_montoTotal);
            ReportViewer1.LocalReport.SetParameters(p_nrocheque);
            ReportViewer1.LocalReport.SetParameters(p_concepto);

            ReportViewer1.LocalReport.SetParameters(p_banco);
            ReportViewer1.LocalReport.SetParameters(p_efectivo);
            ReportViewer1.LocalReport.SetParameters(p_retencionIUE);
            ReportViewer1.LocalReport.SetParameters(p_retencionIT);
            ReportViewer1.LocalReport.SetParameters(p_totalapagar);
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