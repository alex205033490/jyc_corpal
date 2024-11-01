using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.NegocioPlantilla
{
    public partial class Sidebar_Plantillanew : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                negarTodo();
                armarMenuSistema();
            }
        }


        public void negarTodo()
        {

            mn_gestionarFormularios.Visible = true;
            mn_gestionarPermisos.Visible = true;
            mn_gestionarResponsable.Visible = true;

            mn_SGI.Visible = true;
            mn_agenda.Visible = true;
            mn_conciliacionBancaria.Visible = true;
            mn_movimientoCheques.Visible = true;
            mn_saldosCuentasGeneral.Visible = true;
            mn_facturacion.Visible = true;
            mn_agendaTrabajo.Visible = true;
            mn_actividades.Visible = true;
            //--------------------------------            

            mn_gestionarTiendaPropietario.Visible = true;
            mn_rutaEntrega.Visible = true;
            mn_regiboIngreso.Visible = true;
            mn_regiboEgreso.Visible = true;

            mn_consultaIngresoEgreso.Visible = true;
            mn_solicitudproductos.Visible = true;
            mn_entregaSolicitudProducto.Visible = true;
            mn_detallesolicitudproductos.Visible = true;
            mn_entregaProduccion.Visible = true;
            mn_activosjyc.Visible = true;
            mn_consultaActividades.Visible = true;
            //------------------------------
            mn_solicitudMaterial.Visible = true;
            mn_compraMaterial.Visible = true;
            mn_MaterialRecibido.Visible = true;
            mn_objetivoProduccion.Visible = true;
            mn_objetivoProduccionMensual.Visible = true;

            mn_devolucionProductoTerminado.Visible = true;
            mn_AprobaciondevolucionProductoTerminado.Visible = true;
            mn_ordendeProduccion.Visible = true;
            mn_recetas.Visible = true;
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
                    //    NA_Responsables Nresp = new NA_Responsables();
                    List<int> listaPermisos = Nresp.getPermisoUsuario(codUser);
                    for (int i = 0; i < listaPermisos.Count; i++)
                    {
                        int codPermiso = Convert.ToInt32(listaPermisos[i].ToString());
                        switch (codPermiso)
                        {
                            case 1:
                                mn_gestionarResponsable.Visible = true;
                                break;
                            case 2:
                                mn_gestionarPermisos.Visible = true;
                                break;

                            case 4:
                                mn_gestionarFormularios.Visible = true;
                                break;

                            case 99:
                                mn_consultaActividades.Visible = true;
                                break;

                            case 34:
                                mn_SGI.Visible = true;
                                break;
                            case 40:
                                mn_agenda.Visible = true;
                                break;
                            case 41:
                                mn_conciliacionBancaria.Visible = true;
                                break;
                            case 42:
                                mn_movimientoCheques.Visible = true;
                                break;
                            case 44:
                                mn_saldosCuentasGeneral.Visible = true;
                                break;
                            case 45:
                                mn_facturacion.Visible = true;
                                break;
                            case 80:
                                mn_agendaTrabajo.Visible = true;
                                break;
                            case 83:
                                mn_actividades.Visible = true;
                                break;
                            case 115:
                                mn_regiboIngreso.Visible = true;
                                break;
                            case 116:
                                mn_regiboEgreso.Visible = true;
                                break;
                            case 117:
                                mn_consultaIngresoEgreso.Visible = true;
                                break;
                            case 118:
                                mn_solicitudproductos.Visible = true;
                                break;
                            case 119:
                                mn_entregaSolicitudProducto.Visible = true;
                                break;
                            case 120:
                                mn_detallesolicitudproductos.Visible = true;
                                break;
                            case 121:
                                mn_entregaProduccion.Visible = true;
                                break;
                            case 123:
                                mn_activosjyc.Visible = true;
                                break;
                            case 124:
                                mn_solicitudMaterial.Visible = true;
                                break;
                            case 125:
                                mn_compraMaterial.Visible = true;
                                break;
                            case 126:
                                mn_MaterialRecibido.Visible = true;
                                break;
                            case 127:
                                mn_objetivoProduccion.Visible = true;
                                break;
                            case 128:
                                mn_objetivoProduccionMensual.Visible = true;
                                break;
                            case 129:
                                mn_devolucionProductoTerminado.Visible = true;
                                break;
                            case 130:
                                mn_AprobaciondevolucionProductoTerminado.Visible = true;
                                break;
                            case 131:
                                mn_ordendeProduccion.Visible = true;
                                break;
                            case 132:
                                mn_recetas.Visible = true;
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