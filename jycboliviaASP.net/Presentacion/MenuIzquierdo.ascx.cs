using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;


namespace jycboliviaASP.net.Presentacion
{
    public partial class MenuIzquierdo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            NA_Evento nevento = new NA_Evento();
            DataSet dato = nevento.getDatosEventoCallCenter();
            gv_eventoAbierto.DataSource = dato;
            gv_eventoAbierto.DataBind();
            if (!IsPostBack)
            {
                negarmenuizquierdo();
                armarMenuSistema();
            }

            
        }

        private void negarmenuizquierdo()
        {
            mn_consultaCallcenter.Visible = false;
            mn_eventoestadisticaNormal.Visible = false;
            mn_eventonuevo.Visible = false;
            mn_eventos.Visible = false;
            mn_eventosRCC.Visible = false;
            mn_eventosRin.Visible = false;
            mn_deudaPlanPago.Visible = false;
            mn_areaCliente.Visible = false;
            mn_cotiRinCallcenter.Visible = false;
            mn_areaCotiRepuesto.Visible = false;
        }



        public void armarMenuSistema()
        {
            try
            {
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                if (codUser != -1)
                {
                    
                    List<int> listaPermisos = Nresp.getPermisoUsuario(codUser);
                    for (int i = 0; i < listaPermisos.Count; i++)
                    {
                        int codPermiso = Convert.ToInt32(listaPermisos[i].ToString());
                        switch (codPermiso)
                        {                            
                            case 52:
                                mn_eventonuevo.Visible = true;
                                break;
                            case 53:
                                mn_eventosRin.Visible = true;
                                break;
                            case 54:
                                mn_eventosRCC.Visible = true;
                                break;
                            case 55:
                                mn_eventoestadisticaNormal.Visible = true;
                                break;
                            case 56:
                                mn_consultaCallcenter.Visible = true;
                                break;
                            case 57:
                                mn_eventos.Visible = true;
                                break;
                            case 58:
                                mn_deudaPlanPago.Visible = true;
                                break;
                            case 59:
                                mn_areaCliente.Visible = true;
                                break;                                
                            case 60:
                                mn_cotiRinCallcenter.Visible = true;
                                break;
                            case 66:
                                mn_areaCotiRepuesto.Visible = true;
                                break;

                            default:
                                Console.WriteLine("Default case");
                                break;
                        }
                    }
                }
                else
                {
                    string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                    Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
                }


            }
            catch (Exception)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");

            }

        }

    }
}