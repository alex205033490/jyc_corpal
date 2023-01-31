using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_consultaRutas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (!IsPostBack) {
                
                DateTime fechaNow = DateTime.Now;
                int mes = fechaNow.Month;
                int anio = fechaNow.Year;
                dd_mes.SelectedIndex = mes - 1;
                tx_year.Text = anio.ToString();
                consultaRutaPersonalAsignado("", "", mes, anio, "");
            }

        }

        protected void bt_Buscar_Click1(object sender, EventArgs e)
        {
            string nombreProyecto = tx_Edificio.Text;
            
            string exbo = tx_Exbo.Text;
            int anio = Convert.ToInt32(tx_year.Text);
            int mes = Convert.ToInt32(dd_mes.SelectedValue.ToString());
                        
            string codRuta = tx_codRuta.Text;
            consultaRutaPersonalAsignado(codRuta,exbo,mes,anio,nombreProyecto);
        }

        private void consultaRutaPersonalAsignado(string codRuta, string exbo, int mes, int anio, string nombreProyecto)
        {
            
            NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
            DataTable dato = nruta.mostrarALLEquiposAsignadosRutas_Bastones(mes, anio, codRuta,exbo, nombreProyecto ,"Todos");
            gv_tabla.DataSource = dato;            
            gv_tabla.DataBind();
            tx_cantidad.Text = Convert.ToString(gv_tabla.Rows.Count);
        }


     

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProyectos(string prefixText, int count)
        {
            string nombreProyecto = prefixText;

            NProyecto proyectoN = new NProyecto();
            DataSet tuplas = proyectoN.buscador2(nombreProyecto);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string nombreArchivo = "Consulta Rutas Mantenimiento  " + Session["BaseDatos"].ToString() + " " + DateTime.Now;
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombreArchivo + ".xls" + "\"");


            string nombreProyecto = tx_Edificio.Text;            
            string exbo = tx_Exbo.Text;
            int anio = Convert.ToInt32(tx_year.Text);
            int mes = Convert.ToInt32(dd_mes.SelectedValue.ToString());
            string codRuta = tx_codRuta.Text;          
            NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
            DataTable tuplas = nruta.mostrarALLEquiposAsignadosRutas_Bastones(mes, anio, codRuta, exbo, nombreProyecto, "Todos");

          
            DataGrid dg = new DataGrid();
            dg.DataSource = tuplas;
            dg.DataBind();
            dg.RenderControl(htmltextwrtter);

            Response.Write(strwritter.ToString());
            Response.End();
        }

        protected void bt_cargarDocumentos_Click(object sender, EventArgs e)
        {
            cargar_DocumentosAlaBaseDatos();
        }

        private void cargar_DocumentosAlaBaseDatos()
        {
            if (FileUpload1.HasFile)
            {

             HttpPostedFile file = FileUpload1.PostedFile;
             string ruta = ConfigurationManager.AppSettings["rutaCargaBastones"] + Session["BaseDatos"].ToString();             
             // Si el directorio no existe, crearlo
             if (!Directory.Exists(ruta))
                 Directory.CreateDirectory(ruta);

             string archivo = String.Format("{0}\\{1}", ruta, file.FileName);
             file.SaveAs(archivo);

             NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
             nruta.insertarExcelBastones(ruta, file.FileName);
             Response.Write("<script type='text/javascript'> alert('Guardado: "+ruta+"/"+file.FileName+"') </script>");

            }
        }

       

    }
}