using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_CargarDocsIndividuales : System.Web.UI.Page
    {
        private string rutaArchivo = ConfigurationManager.AppSettings["RutaCorpalDocIndividuales"];

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            /*if(tienePermisoDeIngreso(12345) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }*/

            if (!IsPostBack)
            {
                
                buscarArchivo(rutaArchivo, tx_nomArchivo.Text);
            }
        }

        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usu = Session["NameUser"].ToString();
            string pass = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usu, pass);

            NA_DetallePermiso Npermiso = new NA_DetallePermiso();
            return Npermiso.tienePermisoResponsable(permiso, codUser);
        }


        protected void btn_subirDoc_Click(object sender, EventArgs e)
        {
            try
            {
                cargarDocumentos();

                buscarArchivo(rutaArchivo, tx_nomArchivo.Text);
            }
            catch (Exception ex)
            {
                showalert("Error inesperado al subir el archivo" + ex.Message);
            }
        }

        public void cargarDocumentos()
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    guardarArchivo(FileUpload1.PostedFile);

                    NA_Historial Nhistoria = new NA_Historial();
                    NA_Responsables Nresp = new NA_Responsables();
                    int codUser = Convert.ToInt32(Session["coduser"].ToString());

                    DataSet dsResponsable = Nresp.get_responsable(codUser);
                    string NombreResponsable = "";

                    if(dsResponsable != null && dsResponsable.Tables.Count > 0 && dsResponsable.Tables[0].Rows.Count > 0)
                    {
                        NombreResponsable = dsResponsable.Tables[0].Rows[0]["nombre"].ToString();
                    } else
                    {
                        NombreResponsable = "Desconocido";
                    }
                    string nombreArchivo = Path.GetFileName(FileUpload1.PostedFile.FileName);

                    Nhistoria.insertar(codUser,
                        "Se ha cargado el archivo " + FileUpload1.PostedFile.FileName + " a la base de datos " + Session["BaseDatos"].ToString());

                    showalert("Archivo Subido Correctamente! OK");
                }
                else
                {
                    showalert("Error: Seleccione un archivo válido");
                }
            }
            catch (Exception ex)
            {
                showalert("Error inesperado al cargar el documento. " + ex.Message);
            }
        }

        private void guardarArchivo(HttpPostedFile file)
        {
            try
            {
                NA_Responsables Nresp = new NA_Responsables();
                string usu = Session["NameUser"].ToString();
                string pass = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usu, pass);

                string ruta = ConfigurationManager.AppSettings["RutaCorpalDocIndividuales"] + Session["BaseDatos"].ToString() +"/"+codUser;

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                string archivo = String.Format("{0}\\{1}", ruta, file.FileName);
                file.SaveAs(archivo);
            }
            catch(Exception ex)
            {
                showalert("Error en el metodo Guardar Archivo." + ex.Message);
            }
        }


        /////// ver documentos en GV
        public void buscarArchivo(string rutaArchivo, string archivo)
        {
            string db = Session["BaseDatos"].ToString();
            NA_Responsables Nresp = new NA_Responsables();
            string usu = Session["NameUser"].ToString();
            string pass = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usu, pass);

            string rutaFinal = Path.Combine(rutaArchivo, db, codUser.ToString());

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3]
            {
                new DataColumn("Archivo", typeof(string)),
                new DataColumn("Subido al SGI", typeof(DateTime)),
                new DataColumn("Ruta", typeof(string))
            });

            if (Directory.Exists(rutaFinal))
            {
                buscarRecursivo(rutaFinal, archivo, dt);
            }
            else
            {
                showalert("No tienes archivos en tu carpeta.");
            }
            
            if(dt.Rows.Count == 0)
            {
                showalert("No tienes archivos en tu carpeta");
            }

            DataView dv = dt.DefaultView;
            dv.Sort = "[Subido al SGI] DESC";
            DataTable dtOrdenado = dv.ToTable();

            gv_docPersonales.DataSource = dtOrdenado;
            gv_docPersonales.DataBind();
        }


        private void buscarRecursivo(string ruta, string archivoBuscar, DataTable dt)
        {
            try
            {
                foreach(string f in Directory.GetFiles(ruta, "*" + archivoBuscar + "*"))
                {
                    string rutaF = f.Replace("\\", "/");
                    int index = rutaF.LastIndexOf('/');
                    string archivoF = rutaF.Substring(index + 1);
                    FileInfo fileInfo = new FileInfo(f);

                    DateTime fechaHora = fileInfo.CreationTime;

                    dt.Rows.Add(archivoF, fechaHora, rutaF);
                }

                foreach(string subdir in Directory.GetDirectories(ruta))
                {
                    buscarRecursivo(subdir, archivoBuscar, dt);
                }
            }
            catch(Exception ex)
            {
                showalert("Error accediendo a " + ruta + ": " + ex.Message);
            }
        }

        // Descargar archivo
        private void descargarArchivo(string rutaArchivo, string nombreArchivo)
        {
            try
            {
                if (!Directory.Exists(rutaArchivo))
                    Directory.CreateDirectory(rutaArchivo);

                rutaArchivo = rutaArchivo + "/" + nombreArchivo;

                NA_Historial Nhistorial = new NA_Historial();
                NA_Responsables Nresp = new NA_Responsables();
                string usu = Session["NameUser"].ToString();
                string pass = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usu, pass);
                string responsable = "";

                DataSet ds = Nresp.get_responsable(codUser);

                if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    responsable = row["nombre"].ToString();
                }
                else
                {
                    responsable = string.Empty;
                }

                Response.Clear();

                Response.ContentType = "application/octet-stream";

                string nombreAux = nombreArchivo.Replace(" ", "_");
                Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreAux);

                Response.WriteFile(rutaArchivo);

                Response.Flush();

                Response.End();

                showalert("Arhivo Descargado.");
            } catch (Exception ex)
            {
                showalert("Error al momento de descargar el archivo. " + ex.Message);
            }
        }

        // mensaje JS
        private void showalert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        protected void gv_docPersonales_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Name = gv_docPersonales.SelectedRow.Cells[1].Text;
            string ruta = gv_docPersonales.SelectedRow.Cells[3].Text.Replace(Name, string.Empty);
            descargarArchivo(ruta, HttpUtility.HtmlDecode(Name));

        }

        protected void btn_buscarDoc_Click(object sender, EventArgs e)
        {
            buscarArchivo(rutaArchivo, tx_nomArchivo.Text);
        }
    }
}