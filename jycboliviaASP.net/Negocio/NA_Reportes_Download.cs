using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.IO;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Reportes_Download
    {

        public NA_Reportes_Download() { }

        private void crearR144(string UnidadCiudad, int CodCotiRepuesto ,string usuario, string contrasenia , string edificio,DataTable listaRepuesto, string PrecioTotal, string montoTotalEnLetras)
        {

            string fechaAux = DateTime.Now.ToString("dd/MM/yyyy");
            string unidadNegocioAux = UnidadCiudad;

            DataTable datoRepuesto = listaRepuesto;

            DataRow tupla = datoRepuesto.NewRow();
            tupla["Codigo"] = "";
            tupla["Detalle"] = "";
            tupla["Cantidad"] = "";
            tupla["Precio"] = "Total";
            tupla["PrecioTotal"] = PrecioTotal;
            datoRepuesto.Rows.Add(tupla);
            string montoTotalRepuestos = montoTotalEnLetras;


            ReportParameter fechacoti = new ReportParameter("p_fecha", fechaAux);
            ReportParameter unidadNegocio = new ReportParameter("p_unidadnegocio", unidadNegocioAux);
            //----------------------------------------------
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = usuario;
            string passwordAux = contrasenia;
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            DataSet datoResponsable = Nresp.get_responsable(CodUser);
            //------------------------------------------------------------
            ReportParameter Solicitante = new ReportParameter("p_solicitante", datoResponsable.Tables[0].Rows[0][1].ToString());
            ReportDataSource DSRepuesto = new ReportDataSource("DSR144", datoRepuesto);
            ReportParameter p_edificio = new ReportParameter("p_edificio", HttpUtility.HtmlDecode(edificio));



            string ruta = ConfigurationManager.AppSettings["image_logo"];
            string nombreImagen = "jyc";
            string baseDatos = UnidadCiudad;

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

            string direccionImagen = ruta + nombreImagen;

            ReportParameter imagen = new ReportParameter("p_logo", @"file:///" + direccionImagen + ".PNG");
            //  ReportParameter imagen = new ReportParameter("p_logo", "d:/temp/alex.jpg");

            string ruta144 = ConfigurationManager.AppSettings["repo_r144"];

            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.ReportPath = ruta144;
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.Refresh();
            viewer.LocalReport.SetParameters(fechacoti);
            viewer.LocalReport.SetParameters(unidadNegocio);
            viewer.LocalReport.SetParameters(Solicitante);
            viewer.LocalReport.SetParameters(p_edificio);
            viewer.LocalReport.SetParameters(imagen);
            viewer.LocalReport.DataSources.Add(DSRepuesto);

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

            string rutaGuardarR144 = ConfigurationManager.AppSettings["guardar_r144"];
            if (!Directory.Exists(rutaGuardarR144))
                Directory.CreateDirectory(rutaGuardarR144);

            int codigoCoti = CodCotiRepuesto;
            string Edificio = edificio;
            string nombreArchivo = "R-144_coti" + codigoCoti + "_" + Edificio;
            string direccionGuardarR144 = rutaGuardarR144 + nombreArchivo;

            using (FileStream fs = new FileStream(@direccionGuardarR144 + "." + extension, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
          //  Enviar_Correo("Se ha creado el R-144 " + nombreArchivo);

        }




    }
}