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
          /*     case "Santa Cruz":                  
                   Session["NombreBaseDatos"] = "db_SantaCruz";
                   Session["BaseDatos"] = "Santa Cruz";
                   Session["DB"] = "db_seguimientoscz_jyc";
                   Session["ID_UNE"] = "Interlogi SRL";
                   break;
               case "La Paz":                   
                   Session["NombreBaseDatos"] = "db_LaPaz";
                   Session["BaseDatos"] = "La Paz";
                   Session["DB"] = "db_seguimientolpz_jyc";
                   Session["ID_UNE"] = "Elevamerica SRL";
                   break;
               case "Cochabamba":                   
                   Session["NombreBaseDatos"] = "db_Cochabamba";
                   Session["BaseDatos"] = "Cochabamba";
                   Session["DB"] = "db_seguimientocbba_jyc";
                   Session["ID_UNE"] = "Melevar SRL";
                   break;
               case "Sucre":                   
                   Session["NombreBaseDatos"] = "db_Sucre";
                   Session["BaseDatos"] = "Sucre";
                   Session["DB"] = "db_seguimientosucre_jyc";
                   Session["ID_UNE"] = "JyC SRL";
                   break;
               case "Oruro":
                   Session["NombreBaseDatos"] = "db_Oruro";
                   Session["BaseDatos"] = "Oruro";
                   Session["DB"] = "db_seguimientooruro_jyc";
                   Session["ID_UNE"] = "JyC SRL";
                   break;
               case "Potosi":
                   Session["NombreBaseDatos"] = "db_Potosi";
                   Session["BaseDatos"] = "Potosi";
                   Session["DB"] = "db_seguimientopotosi_jyc";
                   Session["ID_UNE"] = "JyC SRL";
                   break;
               case "Tarija":
                   Session["NombreBaseDatos"] = "db_Tarija";
                   Session["BaseDatos"] = "Tarija";
                   Session["DB"] = "db_seguimientotarija_jyc";
                   Session["ID_UNE"] = "JyC SRL";
                   break;
               case "Yacuiba":
                   Session["NombreBaseDatos"] = "db_Yacuiba";
                   Session["BaseDatos"] = "Yacuiba";
                   Session["DB"] = "db_seguimientoyacuiba_jyc";
                   Session["ID_UNE"] = "Interlogy SRL";
                   break;
               case "Villamontes":
                   Session["NombreBaseDatos"] = "db_Villamontes";
                   Session["BaseDatos"] = "Villamontes";
                   Session["DB"] = "db_seguimientovillamontes_jyc";
                   Session["ID_UNE"] = "JyC SRL";
                   break;
               case "Asuncion-Paraguay":
                   Session["NombreBaseDatos"] = "db_Paraguay";
                   Session["BaseDatos"] = "Asuncion-Paraguay";
                   Session["DB"] = "db_seguimientoparaguay_jyc";
                   Session["ID_UNE"] = "JyC SRL";
                   break;
               case "Asuncion-Nuevo":
                   Session["NombreBaseDatos"] = "db_ParaguayNuevo";
                   Session["BaseDatos"] = "Asuncion-Nuevo";
                   Session["DB"] = "db_seguimientoparaguay_nuevo";
                   Session["ID_UNE"] = "JyC SRL";
                   break;
               case "JyC Srl":
                   Session["NombreBaseDatos"] = "db_jycsrl";
                   Session["BaseDatos"] = "JyC Srl";
                   Session["DB"] = "db_seguimientojycsrl_jyc";
                   Session["ID_UNE"] = "JyC SRL";
                   break;
               case "JyCIA Srl":
                   Session["NombreBaseDatos"] = "db_jyciasrl";
                   Session["BaseDatos"] = "JyCIA Srl";
                   Session["DB"] = "db_seguimientojyciasrl_jyc";
                   Session["ID_UNE"] = "JyC SRL";
                   break;
               case "Imven":
                   Session["NombreBaseDatos"] = "db_imven";
                   Session["BaseDatos"] = "Imven";
                   Session["DB"] = "db_seguimientoimven_jyc";
                   Session["ID_UNE"] = "JyC SRL";
                   break;
               case "Beni":
                   Session["NombreBaseDatos"] = "db_beni";
                   Session["BaseDatos"] = "Beni";
                   Session["DB"] = "db_seguimientobeni_jyc";
                   Session["ID_UNE"] = "Interlogy SRL";
                   break;
               case "Pando":
                   Session["NombreBaseDatos"] = "db_pando";
                   Session["BaseDatos"] = "Pando";
                   Session["DB"] = "db_seguimientopando_jyc";
                   Session["ID_UNE"] = "Interlogy SRL";
                   break;
               case "Stock":
                   Session["NombreBaseDatos"] = "db_equipostock";
                   Session["BaseDatos"] = "Equipo_Stock";
                   Session["DB"] = "db_equipostock_jyc";
                   Session["ID_UNE"] = "JyC SRL";
                   break;*/
               case "Corpal":
                   Session["NombreBaseDatos"] = "db_corpal";
                   Session["BaseDatos"] = "Corpal";
                   Session["DB"] = "db_corpal";
                   Session["ID_UNE"] = "Corpal";
                   break;

           /*    case "Prueba Santa Cruz":
                   Session["NombreBaseDatos"] = "db_SantaCruzprueba";
                   Session["BaseDatos"] = "Prueba Santa Cruz";
                   Session["DB"] = "db_seguimientosczprueba";
                   Session["ID_UNE"] = "Interlogy SRL";
                   break;
               case "Prueba La Paz":
                   Session["NombreBaseDatos"] = "db_LaPazprueba";
                   Session["BaseDatos"] = "Prueba La Paz";
                   Session["DB"] = "db_seguimientolpzprueba";
                   Session["ID_UNE"] = "Elevamerica SRL";
                   break;
               case "Prueba Cochabamba":
                   Session["NombreBaseDatos"] = "db_Cochabambaprueba";
                   Session["BaseDatos"] = "Prueba Cochabamba";
                   Session["DB"] = "db_seguimientocbbaprueba";
                   Session["ID_UNE"] = "Melevar SRL";
                   break;
               */
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
                ///---------para gestionar tarea ----------
                Session["nombreEdificioTarea"] = "";
                Session["banderatarea"] = false;
                Session["banderaEvento"] = false;
                //-------------------------------

                Response.Redirect(ruta + "/Presentacion/WelcomeJyC.aspx");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: usuario') </script>");

        }
    }
}