using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using jycboliviaASP.net.Negocio;
using System.IO;
using System.Data;
using System.Net;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CargarDocumentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(33) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if (!IsPostBack)
            {
                buscar("");
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


        private void buscar(string nombre)
        {
            NProyecto proyecto = new NProyecto();

            DataSet resultadoR = proyecto.buscar(nombre);
            gv_proyecto.DataSource = resultadoR;
            gv_proyecto.DataBind();
        }
   
        private void GuardarArchivo(HttpPostedFile file)
        {
            // Se carga la ruta física de la carpeta temp del sitio            
            string ruta = ConfigurationManager.AppSettings["RutaCuadroXXX"]  + Session["BaseDatos"].ToString();
            string codProy = gv_proyecto.SelectedRow.Cells[1].Text;
            string nombreProy = gv_proyecto.SelectedRow.Cells[2].Text;
            ruta = ruta+"/"+codProy + "_" + nombreProy;
            // Si el directorio no existe, crearlo
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            string archivo = String.Format("{0}\\{1}", ruta, file.FileName);
            file.SaveAs(archivo);          
        }

     public void cargarDocumentos()
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    if (gv_proyecto.SelectedIndex > -1)
                    {
                        GuardarArchivo(FileUpload1.PostedFile);

                        NA_Historial nhistorial = new NA_Historial();
                        int codUser = Convert.ToInt32(Session["coduser"].ToString());
                        nhistorial.insertar(codUser, "Se ha Cargado el archivo" + FileUpload1.PostedFile.FileName);
                        Response.Write("<script type='text/javascript'> alert('Archivo Subido') </script>");
                    }
                    else
                        Response.Write("<script type='text/javascript'> alert('ERROR: Seleccione un Proyecto') </script>");            
                        
                }
                else
                    Response.Write("<script type='text/javascript'> alert('ERROR: Seleccione un archivo del disco duro') </script>");             
            }
            catch (Exception )
            {                
                Response.Write("<script type='text/javascript'> alert('Error'+ex ) </script>");
            }
        }

        protected void bt_cargarDocumentos_Click(object sender, EventArgs e)
        {
            cargarDocumentos();            
            leerDocumentos();
            gv_documentos.SelectedIndex = -1;
        }

        protected void gv_proyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            leerDocumentos();
        }

        private void leerDocumentos()
        {
            if(gv_proyecto.SelectedIndex > -1){
                // Se carga la ruta física de la carpeta temp del sitio            
                string ruta = ConfigurationManager.AppSettings["RutaCuadroXXX"] + Session["BaseDatos"].ToString();
                string codProy = gv_proyecto.SelectedRow.Cells[1].Text;
                string nombreProy = gv_proyecto.SelectedRow.Cells[2].Text;
                ruta = ruta + "/" + codProy + "_" + nombreProy;
                // Si el directorio no existe, crearlo
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                string[] files = Directory.GetFiles(ruta);
                string[] documentos = new string[files.Length];
                for (int i = 0; i < documentos.Length; i++)
                {
                    string aux = files[i].ToString().Replace("\\", "/");
                    int inicio = aux.LastIndexOf("/");
                    documentos[i] = aux.Substring(inicio + 1);
                }

                gv_documentos.DataSource = documentos;
                gv_documentos.DataBind();
            
            }          
        }

        protected void gv_documentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            descargarArchivo();
        }

        private void descargarArchivo()
        {
            try
            {                
                string ruta = ConfigurationManager.AppSettings["RutaCuadroXXX"] + Session["BaseDatos"].ToString();
                string codProy = gv_proyecto.SelectedRow.Cells[1].Text;
                string nombreProy = gv_proyecto.SelectedRow.Cells[2].Text;
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                ruta = ruta + "/" + codProy + "_" + nombreProy+"/"+gv_documentos.SelectedRow.Cells[2].Text;
                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=" + gv_documentos.SelectedRow.Cells[2].Text);
                // Escribimos el fichero a enviar 
                Response.WriteFile(ruta);
                // volcamos el stream 
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write("<script type='text/javascript'> alert('ERROR: " + ex.Message + "') </script>");             
                
            }
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            buscar(tx_edificio.Text);
            gv_proyecto.SelectedIndex = -1;
            gv_documentos.SelectedIndex = -1;

        }

             

        protected void gv_documentos_SelectedIndexChanged1(object sender, EventArgs e)
        {
            descargarArchivo();
        }

        protected void gv_documentos_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            string ruta = ConfigurationManager.AppSettings["RutaCuadroXXX"] + Session["BaseDatos"].ToString();
            string codProy = gv_proyecto.SelectedRow.Cells[1].Text;
            string nombreProy = gv_proyecto.SelectedRow.Cells[2].Text;
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);
            ruta = ruta + "/" + codProy + "_" + nombreProy + "/" + gv_documentos.Rows[index].Cells[2].Text;
            System.IO.File.Delete(ruta);
            //---------------------
            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Se ha Eliminado el archivo " + gv_documentos.Rows[index].Cells[2].Text);
            Response.Write("<script type='text/javascript'> alert('Eliminar OK') </script>");
            //-----------------------
            leerDocumentos();            
            gv_documentos.SelectedIndex = -1;
        }





    }
}