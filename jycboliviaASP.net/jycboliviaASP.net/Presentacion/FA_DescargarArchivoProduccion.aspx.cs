using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Data;
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_DescargarArchivoProduccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_descargarArchivo(object sender, EventArgs e)
        {
            entregaProduccionDescargaPDF();
        }

        private void entregaProduccionDescargaPDF()
        {                    

            int codigoEntregaProduccion = int.Parse(Session["codigoEntregaProduccion"].ToString());
            NCorpal_Produccion npro = new NCorpal_Produccion();
            DataSet datoResult = npro.get_DatosEntregaProduccion(codigoEntregaProduccion);

            string ciudad = Session["BaseDatos"].ToString();

            string codigo = datoResult.Tables[0].Rows[0][0].ToString();
            string fecha = datoResult.Tables[0].Rows[0][1].ToString();
            string hora = datoResult.Tables[0].Rows[0][2].ToString();
            string turno = datoResult.Tables[0].Rows[0][3].ToString();
            string resp_entrega = datoResult.Tables[0].Rows[0][4].ToString();
            string resp_recepcion = datoResult.Tables[0].Rows[0][5].ToString();
            string nroorden = datoResult.Tables[0].Rows[0][6].ToString();
            string productoNax = datoResult.Tables[0].Rows[0][7].ToString();
            string cantcajas = datoResult.Tables[0].Rows[0][8].ToString();
            string unidadsuelta = datoResult.Tables[0].Rows[0][9].ToString();
            string kgrdesperdicio = datoResult.Tables[0].Rows[0][10].ToString();
            string kgrparamix = datoResult.Tables[0].Rows[0][11].ToString();
            string codresprecepcion = datoResult.Tables[0].Rows[0][12].ToString();

            ReportParameter p_nroorden = new ReportParameter("p_nroorden", nroorden);
            ReportParameter p_fecha = new ReportParameter("p_fecha", fecha);
            ReportParameter p_hora = new ReportParameter("p_hora", hora);
            ReportParameter p_turno = new ReportParameter("p_turno", turno);

            ReportParameter p_entregaproduccion = new ReportParameter("p_entregaproduccion", resp_entrega);
            ReportParameter p_recepcionproduccion = new ReportParameter("p_recepcionproduccion", resp_recepcion);
            ReportParameter p_produccionnax = new ReportParameter("p_produccionnax", productoNax);
            ReportParameter p_cantcajas = new ReportParameter("p_cantcajas", cantcajas);
            ReportParameter p_unidadsuelta = new ReportParameter("p_unidadsuelta", unidadsuelta);
            ReportParameter p_kgrparamix = new ReportParameter("p_kgrparamix", kgrparamix);
            ReportParameter p_kgrdesperdicio = new ReportParameter("p_kgrdesperdicio", kgrdesperdicio);
            
            string rutaReciboIngreso = ConfigurationManager.AppSettings["repo_ReciboEntregaProduccion_Voucher"];
            LocalReport ReportViewer1 = new LocalReport();
            ReportViewer1.ReportPath = rutaReciboIngreso;
            ReportViewer1.DataSources.Clear();
            ReportViewer1.EnableExternalImages = true;

            //viewer.LocalReport.Refresh();      

            ReportViewer1.SetParameters(p_nroorden);
            ReportViewer1.SetParameters(p_fecha);
            ReportViewer1.SetParameters(p_hora);
            ReportViewer1.SetParameters(p_turno);
            ReportViewer1.SetParameters(p_entregaproduccion);
            ReportViewer1.SetParameters(p_recepcionproduccion);
            ReportViewer1.SetParameters(p_produccionnax);

            ReportViewer1.SetParameters(p_cantcajas);
            ReportViewer1.SetParameters(p_unidadsuelta);
            ReportViewer1.SetParameters(p_kgrparamix);
            ReportViewer1.SetParameters(p_kgrdesperdicio);
            
            ReportViewer1.Refresh();

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            // string encoding = System.Text.Encoding.Default.ToString();
            string extension = string.Empty;




            byte[] bytes = ReportViewer1.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            string ruta_voucher = ConfigurationManager.AppSettings["rutaVoucherProduccion"] ;
            if (!Directory.Exists(ruta_voucher))
                Directory.CreateDirectory(ruta_voucher);

            string nombreArchivo1 = "Voucher_Produccion_"+ codigo;
            string direccionGuardarR144 = ruta_voucher + nombreArchivo1;

            using (FileStream fs = new FileStream(@direccionGuardarR144 + "." + extension, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

            string rutaAux = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
            string url = rutaAux + "/Presentacion/FA_descargarArchivoPDF.aspx";
            string urlVolver = rutaAux + "/Presentacion/FA_RutasAsignadasTec.aspx";

            string RutaArchivo = ruta_voucher;
            string nombreArchivo = nombreArchivo1 + ".pdf";

            try
            {
                if (!Directory.Exists(RutaArchivo))
                    Directory.CreateDirectory(RutaArchivo);

                RutaArchivo = RutaArchivo + "/" + nombreArchivo;

                String prueba;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "pdf";
                prueba = Path.GetFileName(RutaArchivo).ToString();
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + prueba);
                HttpContext.Current.Response.TransmitFile(RutaArchivo);
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                Response.Write("<script type='text/javascript'> alert('Error: " + ex + "') </script>");
            }
          
           
           
        }

        protected void bt_descargarPC_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");
        }

        protected void bt_cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Presentacion/FCorpal_EntregaProduccion.aspx");
        }
    }
}