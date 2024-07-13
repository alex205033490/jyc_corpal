using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["coduser"] = -1;
            Session["BaseDatos"] = -1;
        }

        protected void bt_login_Click(object sender, EventArgs e)
        {
            string BaseDatos = dd_loginDpto.SelectedValue;
           switch(BaseDatos){
               case "Prueba":                
                   Session["NombreBaseDatos"] = "db_prueba";
                   Session["BaseDatos"] = "Prueba";
                   Session["DB"] = "db_seguimientoprueba_jyc";
                   Session["ID_UNE"] = "Prueba SRL";
                   break;
          
               case "Corpal":
                   Session["NombreBaseDatos"] = "db_corpal";
                   Session["BaseDatos"] = "Corpal";
                   Session["DB"] = "db_corpal";
                   Session["ID_UNE"] = "Corpal";
                   break;

               default:
                   Console.WriteLine("Default case");
                   break;
           }
    
            /*  \ => \\ y  " => \"  */
            string usuario = tx_usuario.Value.Replace("'","/").Replace("\"","/");
            string password = tx_password.Value.Replace("'", "/").Replace("\"", "/");            
            NA_Responsables Nresp = new NA_Responsables();
            if(Nresp.autenticarUsuario(usuario,password)){
                int codigoUser = Nresp.getCodUsuario(usuario, password);
                Session["coduser"] = codigoUser;
                Session["NameUser"] = usuario;
                Session["passworuser"] = password;
                NA_Historial nhistorial = new NA_Historial();
                nhistorial.insertar(codigoUser, "Ha Ingresado al sistema");
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/index.aspx");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: usuario') </script>");

        }
    }
}