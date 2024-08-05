using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using jycboliviaASP.net.Negocio;
using System.Configuration;
using System.Data;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CargaFechaExpedicion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(14) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 


            if(!IsPostBack){
                cargarTablaFechaExpedicion();
                cargarDepartamentos();
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha Ingresado a Carga Fecha Expedicion");
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

        private void cargarDepartamentos() {
            NA_FechaExpedicion NfechaExp = new NA_FechaExpedicion();
            DataSet datosCargo = NfechaExp.mostrarAllDepartamentos();
            dd_Departamento.DataSource = datosCargo;
            dd_Departamento.DataValueField = "codigo";
            dd_Departamento.DataTextField = "nombre";
            dd_Departamento.Items.Add(new ListItem("", "-1"));
            dd_Departamento.AppendDataBoundItems = true;
            dd_Departamento.SelectedIndex = -1;
            dd_Departamento.DataBind();   
        }


        private void GuardarArchivo(HttpPostedFile file)
        {
            // Se carga la ruta física de la carpeta temp del sitio
           // string ruta = Server.MapPath("../DocFechaExp");
            string ruta = ConfigurationManager.AppSettings["RutaFechasExpedicion"];

            // Si el directorio no existe, crearlo
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            string archivo = String.Format("{0}\\{1}", ruta, file.FileName);

            // Verificar que el archivo no exista
          //  if (File.Exists(archivo))
               // MensajeError(String.Format("Ya existe una imagen con nombre\"{0}\".", file.FileName));
          //      Response.Write("<script type='text/javascript'> alert('Ya existe el archivo con el nombre " + file.FileName+ "') </script>");
           // else
           // {
                Response.Write("<script type='text/javascript'> alert('listo para guardar "+ruta+file.FileName+"') </script>");
                file.SaveAs(archivo);
                Response.Write("<script type='text/javascript'> alert('guardo correctamente') </script>");
           // }
        }


        public void cargarTablaFechaExpedicion() {
            NA_FechaExpedicion NfechaExp = new NA_FechaExpedicion();
            DataSet resultMostrar = NfechaExp.mostrarFechaExpedicion();
            gv_fechaExpedicion.DataSource = resultMostrar;
            gv_fechaExpedicion.DataBind();
            tx_totalEquipos.Text = gv_fechaExpedicion.Rows.Count.ToString();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    // Se verifica que la extensión sea de un formato válido
                    string ext = FileUpload1.PostedFile.FileName;
                    ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
                    //string[] formatos = new string[] { "xlsx", "xls","csv"};
                    string[] formatos = new string[] { "csv" };
                    if (Array.IndexOf(formatos, ext) < 0)
                        //    MensajeError("Formato de imagen inválido.");
                        Response.Write("<script type='text/javascript'> alert('Formato del Archivo inválido.') </script>");
                    else {
                       Response.Write("<script type='text/javascript'> alert('entro a guardar') </script>");
                       GuardarArchivo(FileUpload1.PostedFile);
                       Response.Write("<script type='text/javascript'> alert('guardo el Archivo') </script>"); 
                       NA_FechaExpedicion NfechaExp = new NA_FechaExpedicion();
                       string ruta = ConfigurationManager.AppSettings["RutaFechasExpedicion"] + FileUpload1.PostedFile.FileName;
                       NfechaExp.insertarFechaExpedicion( ruta );

                       NA_Historial nhistorial = new NA_Historial();
                       int codUser = Convert.ToInt32(Session["coduser"].ToString());
                       nhistorial.insertar(codUser, "Se ha Cargado las Fechas de Expedicion del archivo" + FileUpload1.PostedFile.FileName);
 
                       Response.Write("<script type='text/javascript'> alert('guardo la Base de datos') </script>");  
                       cargarTablaFechaExpedicion();
                     //  Response.Write("<script type='text/javascript'> alert('inserto =   nombre archivo = "+ruta+"') </script>");
                    } 
                      //  GuardarArchivo(fileUploader1.PostedFile);

                    //else
                      //  GuardarBD(fileUploader1.PostedFile);
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Seleccione un archivo del disco duro') </script>");
                    //MensajeError("Seleccione un archivo del disco duro.");
            }
            catch (Exception)
            {
                //MensajeError(ex.Message);
                Response.Write("<script type='text/javascript'> alert('Error') </script>");
            }
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            NA_FechaExpedicion NfechaExp = new NA_FechaExpedicion();
            string valor = dd_Departamento.SelectedItem.Text;
            DataSet resultDAta = NfechaExp.mostrarBusqueda(valor);
            gv_fechaExpedicion.DataSource = resultDAta;
            gv_fechaExpedicion.DataBind();
            tx_totalEquipos.Text = gv_fechaExpedicion.Rows.Count.ToString();
        }


    }
}