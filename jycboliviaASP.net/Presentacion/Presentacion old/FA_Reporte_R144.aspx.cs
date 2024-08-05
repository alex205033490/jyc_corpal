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
    public partial class Reporte_R144 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                int codigoR144 = Convert.ToInt32(Session["CodigoR144"].ToString());
                mostrarR144(codigoR144);
            }
        }

        private void mostrarR144(int codigoR144)
        {
            NA_Repuesto nrepu = new NA_Repuesto();
            DataSet datoResult = nrepu.getCotiR144(codigoR144);

            string fechaAux = datoResult.Tables[0].Rows[0][1].ToString();
            string unidadNegocioAux = Session["BaseDatos"].ToString();

            DataTable datoRepuesto = new DataTable();
            datoRepuesto.Columns.Add("Codigo", typeof(string));
            datoRepuesto.Columns.Add("numeracion", typeof(string));
            datoRepuesto.Columns.Add("Detalle", typeof(string));
            datoRepuesto.Columns.Add("Precio", typeof(string));
            datoRepuesto.Columns.Add("Cantidad", typeof(string));
            datoRepuesto.Columns.Add("PrecioTotal", typeof(string));

            NA_Repuesto nrepuesto = new NA_Repuesto();
            DataSet tuplasDataSet = nrepuesto.getDetallesRepuestoR144(codigoR144);
            DataTable filasConsulta = tuplasDataSet.Tables[0];

            for (int i = 0; i < filasConsulta.Rows.Count; i++)
            {
                DataRow row = filasConsulta.Rows[i];
                DataRow tupla = datoRepuesto.NewRow();
                tupla["Codigo"] = row[0].ToString();
                tupla["numeracion"] = row[1].ToString();
                tupla["Detalle"] = row[2].ToString();
                tupla["Cantidad"] = row[3].ToString();
                tupla["Precio"] = row[4].ToString();
                tupla["PrecioTotal"] = row[5].ToString();
                datoRepuesto.Rows.Add(tupla);
            }

            DataRow tupla1 = datoRepuesto.NewRow();
            tupla1["Codigo"] = "";
            tupla1["numeracion"] = "";
            tupla1["Detalle"] = "";
            tupla1["Cantidad"] = "";
            tupla1["Precio"] = "Total";
            tupla1["PrecioTotal"] = datoResult.Tables[0].Rows[0][6].ToString();
            datoRepuesto.Rows.Add(tupla1);
            //  string montoTotalRepuestos = gv_cotizacionRepuesto.SelectedRow.Cells[6].Text;

            string exboEquipo = datoResult.Tables[0].Rows[0][4].ToString();
            if(exboEquipo.Equals("")){
                exboEquipo = "Ninguno";
            }

            ReportParameter pexboEq = new ReportParameter("p_exbo", exboEquipo);
            ReportParameter fechacoti = new ReportParameter("p_fecha", fechaAux);
            ReportParameter unidadNegocio = new ReportParameter("p_unidadnegocio", unidadNegocioAux);
            //----------------------------------------------
           /* NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            DataSet datoResponsable = Nresp.get_responsable(CodUser);*/
            //------------------------------------------------------------
            ReportParameter Solicitante = new ReportParameter("p_solicitante", datoResult.Tables[0].Rows[0][9].ToString());
            ReportDataSource DSRepuesto = new ReportDataSource("DSR144", datoRepuesto);
            ReportParameter p_edificio = new ReportParameter("p_edificio", HttpUtility.HtmlDecode(datoResult.Tables[0].Rows[0][3].ToString()));



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

            string ruta144 = ConfigurationManager.AppSettings["repo_r144"];


            ReportViewer1.LocalReport.ReportPath = ruta144;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();            
            ReportViewer1.LocalReport.SetParameters(fechacoti);
            ReportViewer1.LocalReport.SetParameters(unidadNegocio);
            ReportViewer1.LocalReport.SetParameters(Solicitante);
            ReportViewer1.LocalReport.SetParameters(p_edificio);
            ReportViewer1.LocalReport.SetParameters(pexboEq);
            ReportViewer1.LocalReport.SetParameters(imagen);
            ReportViewer1.LocalReport.DataSources.Add(DSRepuesto);
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