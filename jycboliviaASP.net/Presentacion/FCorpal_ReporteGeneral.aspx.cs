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
                    case "Reporte_QRActivos":
                        qr_activos();
                        break;
                    case "Reporte_Entrega_Produccion":
                        entregaProduccion();
                        break;
                    case "Reporte_SolicitudMaterialInsumos":
                        reporteSolicitudMaterialInsumos();
                        break;
                    case "Reporte_CompraMaterialInsumos":
                        reporteCompraMaterialInsumos();
                        break;
                    case "Reporte_RecibidoMaterialInsumos":
                        reporteRecibidoMaterialInsumos();
                        break;
                    default:
                        //number = "Error";
                        break;
                }
        }

        private void qr_activos()
        {
            string baseDatos = Session["BaseDatos"].ToString();
            NA_ActivosJyC aa = new NA_ActivosJyC();
            string ruta = ConfigurationManager.AppSettings["qr_codeActivo"] + Session["BaseDatos"].ToString() + "/";
            DataSet tuplas = aa.get_direccionQRActivos(ruta);

            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_QRActivos.rdlc";
            DataTable DSconsulta = tuplas.Tables[0];

            string rutaLogo = ConfigurationManager.AppSettings["image_logo"];
            string nombreImagen = "jyc";


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
                    if (baseDatos.Equals("Santa Cruz"))
                    {
                        nombreImagen = "interlogy";
                    }

            string direccionImagen = rutaLogo + nombreImagen;

            ReportParameter imagen = new ReportParameter("p_logo", @"file:\" + direccionImagen + ".jpg");

            ReportDataSource DS_Activos_QR = new ReportDataSource("DS_Activos", DSconsulta);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.LocalReport.SetParameters(imagen);
            ReportViewer1.LocalReport.DataSources.Add(DS_Activos_QR);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void reporteRecibidoMaterialInsumos()
        {
            int codigoSMI = int.Parse(Session["codigoRecibidoMaterialeInsumos"].ToString());
            NCorpal_PedidoMaterialeInsumos npp = new NCorpal_PedidoMaterialeInsumos();
            DataSet datoResult = npp.get_DatosSolicitudMaterialeInsumos(codigoSMI);
            
            string codigo = datoResult.Tables[0].Rows[0][0].ToString();
            string fechaSolicitud = datoResult.Tables[0].Rows[0][1].ToString();
            string fechaEstimadaEntrega = datoResult.Tables[0].Rows[0][2].ToString();
            string PersonalSolicitud = datoResult.Tables[0].Rows[0][3].ToString();
            string PersonalCompra = datoResult.Tables[0].Rows[0][6].ToString();


            ReportParameter p_numero = new ReportParameter("p_numero", "Nro. " + codigo.ToString());
            ReportParameter p_solicitante = new ReportParameter("p_solicitante", PersonalSolicitud);
            ReportParameter p_fechasolicitud = new ReportParameter("p_fechasolicitud", fechaSolicitud);
            ReportParameter p_fechaestimadaentrega = new ReportParameter("p_fechaestimadaentrega", fechaEstimadaEntrega);
            ReportParameter p_personalcomprado = new ReportParameter("p_personalcomprado", PersonalCompra);
            

            DataSet tuplasFilas = npp.get_todosItemInsumosComprados(codigoSMI);
            DataTable DSMaterialeInsumosSolicitados = tuplasFilas.Tables[0];
            ReportDataSource DSRecibidoMaterialInsumos = new ReportDataSource("DSRecibidoMaterialInsumos", DSMaterialeInsumosSolicitados);

            string rutaEntregaSolicitudProducto = ConfigurationManager.AppSettings["repo_RecibidoMaterialeInsumos"];

            ReportViewer1.LocalReport.ReportPath = rutaEntregaSolicitudProducto;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      

            ReportViewer1.LocalReport.SetParameters(p_numero);
            ReportViewer1.LocalReport.SetParameters(p_fechasolicitud);
            ReportViewer1.LocalReport.SetParameters(p_solicitante);
            ReportViewer1.LocalReport.SetParameters(p_fechaestimadaentrega);
            ReportViewer1.LocalReport.SetParameters(p_personalcomprado);
            ReportViewer1.LocalReport.DataSources.Add(DSRecibidoMaterialInsumos);

            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void reporteCompraMaterialInsumos()
        {
            int codigoSMI = int.Parse(Session["codigoCompraMaterialeInsumos"].ToString());
            NCorpal_PedidoMaterialeInsumos npp = new NCorpal_PedidoMaterialeInsumos();
            DataSet datoResult = npp.get_DatosSolicitudMaterialeInsumos(codigoSMI);

            string ciudad = Session["BaseDatos"].ToString();

            string codigo = datoResult.Tables[0].Rows[0][0].ToString();
            string fechaSolicitud = datoResult.Tables[0].Rows[0][1].ToString();
            string fechaEstimadaEntrega = datoResult.Tables[0].Rows[0][2].ToString();
            string PersonalSolicitud = datoResult.Tables[0].Rows[0][3].ToString();


            ReportParameter p_numero = new ReportParameter("p_numero", "Nro. " + codigo.ToString());
            ReportParameter p_solicitante = new ReportParameter("p_solicitante", PersonalSolicitud);
            ReportParameter p_fechasolicitud = new ReportParameter("p_fechasolicitud", fechaSolicitud);
            ReportParameter p_fechaestimadaentrega = new ReportParameter("p_fechaestimadaentrega", fechaEstimadaEntrega);

            DataSet tuplasFilas = npp.get_todosItemInsumosPedidos(codigoSMI);
            DataTable DSMaterialeInsumosSolicitados = tuplasFilas.Tables[0];
            ReportDataSource DSCompraMaterialeInsumos = new ReportDataSource("DSCompraMaterialeInsumos", DSMaterialeInsumosSolicitados);

            string rutaEntregaSolicitudProducto = ConfigurationManager.AppSettings["repo_CompradoMaterialeInsumos"];

            ReportViewer1.LocalReport.ReportPath = rutaEntregaSolicitudProducto;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      

            ReportViewer1.LocalReport.SetParameters(p_numero);
            ReportViewer1.LocalReport.SetParameters(p_fechasolicitud);
            ReportViewer1.LocalReport.SetParameters(p_solicitante);
            ReportViewer1.LocalReport.SetParameters(p_fechaestimadaentrega);
            ReportViewer1.LocalReport.DataSources.Add(DSCompraMaterialeInsumos);

            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void reporteSolicitudMaterialInsumos()
        {
            int codigoSMI = int.Parse(Session["codigoSolicitudMaterialeInsumos"].ToString());
            NCorpal_PedidoMaterialeInsumos npp = new NCorpal_PedidoMaterialeInsumos();
            DataSet datoResult = npp.get_DatosSolicitudMaterialeInsumos(codigoSMI);

            string ciudad = Session["BaseDatos"].ToString();

            string codigo = datoResult.Tables[0].Rows[0][0].ToString();
            string fechaSolicitud = datoResult.Tables[0].Rows[0][1].ToString();
            string fechaEstimadaEntrega = datoResult.Tables[0].Rows[0][2].ToString();
            string PersonalSolicitud = datoResult.Tables[0].Rows[0][3].ToString();


            ReportParameter p_numero = new ReportParameter("p_numero", "Nro. "+codigo.ToString());
            ReportParameter p_solicitante = new ReportParameter("p_solicitante", PersonalSolicitud);
            ReportParameter p_fechasolicitud = new ReportParameter("p_fechasolicitud", fechaSolicitud);
            ReportParameter p_fechaestimadaentrega = new ReportParameter("p_fechaestimadaentrega", fechaEstimadaEntrega);

            DataSet tuplasFilas = npp.get_todosItemInsumosPedidos(codigoSMI);
            DataTable DSMaterialeInsumosSolicitados = tuplasFilas.Tables[0];
            ReportDataSource DS_MaterialeInsumosSolicitados = new ReportDataSource("DSMaterialeInsumosSolicitados", DSMaterialeInsumosSolicitados);

            string rutaEntregaSolicitudProducto = ConfigurationManager.AppSettings["repo_SolicitudMaterialeInsumos"];

            ReportViewer1.LocalReport.ReportPath = rutaEntregaSolicitudProducto;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      

            ReportViewer1.LocalReport.SetParameters(p_numero);
            ReportViewer1.LocalReport.SetParameters(p_fechasolicitud);
            ReportViewer1.LocalReport.SetParameters(p_solicitante);
            ReportViewer1.LocalReport.SetParameters(p_fechaestimadaentrega);
            ReportViewer1.LocalReport.DataSources.Add(DS_MaterialeInsumosSolicitados);

            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void entregaProduccion()
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

            string rutaReciboIngreso = ConfigurationManager.AppSettings["repo_ReciboEntregaProduccion"];

            ReportViewer1.LocalReport.ReportPath = rutaReciboIngreso;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      

            ReportViewer1.LocalReport.SetParameters(p_nroorden);
            ReportViewer1.LocalReport.SetParameters(p_fecha);
            ReportViewer1.LocalReport.SetParameters(p_hora);
            ReportViewer1.LocalReport.SetParameters(p_turno);
            ReportViewer1.LocalReport.SetParameters(p_entregaproduccion);
            ReportViewer1.LocalReport.SetParameters(p_recepcionproduccion);
            ReportViewer1.LocalReport.SetParameters(p_produccionnax);

            ReportViewer1.LocalReport.SetParameters(p_cantcajas);
            ReportViewer1.LocalReport.SetParameters(p_unidadsuelta);
            ReportViewer1.LocalReport.SetParameters(p_kgrparamix);
            ReportViewer1.LocalReport.SetParameters(p_kgrdesperdicio);

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