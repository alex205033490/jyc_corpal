using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CuadroXXX3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(15) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 


            if(!IsPostBack){
                cargarCuadrosXXX();
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha Ingresado a Carga CuadrosXXX");
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

        private void GuardarArchivo(HttpPostedFile file)
        {
            // Se carga la ruta física de la carpeta temp del sitio
            //string ruta = Server.MapPath("../DocCuadroXXX");
            string ruta = ConfigurationManager.AppSettings["RutaCuadroXXX"] ;

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
            file.SaveAs(archivo);
            // }
        }


        public void cargarCuadrosXXX()
        {
            NA_CuadrosXXX NcuadrosXXX = new NA_CuadrosXXX();
            DataSet resultMostrar = NcuadrosXXX.mostrarCuadrosXXX();
            gv_CuadrosXXX.DataSource = resultMostrar;
            gv_CuadrosXXX.DataBind();
            tx_TotalEquiposXXX.Text = gv_CuadrosXXX.Rows.Count.ToString();
        }


        public void cargarCuadroXXX() {
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
                    else
                    {
                        GuardarArchivo(FileUpload1.PostedFile);
                        NA_CuadrosXXX NCuadrosXX = new NA_CuadrosXXX();
                        string ruta = ConfigurationManager.AppSettings["RutaCuadroXXX"] + FileUpload1.PostedFile.FileName;
                        NCuadrosXX.insertarCuadrosXXX(ruta);

                        NA_Historial nhistorial = new NA_Historial();
                        int codUser = Convert.ToInt32(Session["coduser"].ToString());
                        nhistorial.insertar(codUser, "Se ha Cargado las CuadroXXX del archivo" + FileUpload1.PostedFile.FileName);

                        cargarCuadrosXXX();
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

        protected void bt_CargarCuadroXXX_Click(object sender, EventArgs e)
        {
            //cargarCuadrosXXX();            
            if(SendMail()){
            Response.Write("<script type='text/javascript'> alert('Mensaje Enviado') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error') </script>");
        }

        public static Boolean SendMail()
        {
            try
            {
                //Configuración del Mensaje
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress("jyc.servidor@gmail.com", "JyC Mail", Encoding.UTF8);
                //Aquí ponemos el asunto del correo
                mail.Subject = "Prueba de Envío de Correo";
                //Aquí ponemos el mensaje que incluirá el correo
                mail.Body = "Prueba de Envío de Correo de Gmail desde CSharp";
                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                mail.To.Add("sistema@jycbolivia.com");
                //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
                mail.Attachments.Add(new Attachment(@"C:\Users\Sistemas\Desktop\SantaCruz.xls"));

                //Configuracion del SMTP
                SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
                //Especificamos las credenciales con las que enviaremos el mail
                SmtpServer.Credentials = new System.Net.NetworkCredential("jyc.servidor@gmail.com", "alex79016002");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

    }
}