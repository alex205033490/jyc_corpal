using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_EncuestaPreguntas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_realizadoOk_Click(object sender, EventArgs e)
        {
            realizarEncuestaMantenimiento();
        }

        private void realizarEncuestaMantenimiento()
        {
           int codproyecto ;
           bool bandera = int.TryParse(Session["EM_codproy"].ToString(), out codproyecto);
           if (bandera)
           {
               int cumplimientofechasplanificadasmantenimiento = int.Parse(dd_Mcumplientodefechasplanificadasparamantenimiento.SelectedValue);
               int funcionamientodelosequipos = int.Parse(dd_Mfuncionamientodelosequipos.SelectedValue);
               int rapidezdelasreparaciones = int.Parse(dd_Rrapidezdelasreparaciones.SelectedValue);
               int resolucionefectivadelacausadereparacion = int.Parse(dd_Rresolucionefectivadelacausadereparacion.SelectedValue);
               int asesoramientoyrapidezenlaentregadecotizacionesinformes = int.Parse(dd_Rasesoramientoyrapidezenlaentregadecotizacionesoinformes.SelectedValue);
               int tiempoderespuestaanteunaemergencia = int.Parse(dd_Etiempoderespuestaanteunaemergencia.SelectedValue);
               int resolucionefectivadelasemergencias = int.Parse(dd_Eresolucionefectivadelasemergencia.SelectedValue);
               int cordialidadyatenciondelpersonaldecobranza = int.Parse(dd_Acordialidadyatenciondelpersonaldecobranza.SelectedValue);
               int tratoyatenciondelpersonalasministrativo = int.Parse(dd_Atratoyatenciondelpersonaladministrativo.SelectedValue);
               int cordialidadyatenciondelpersonaltecnico = int.Parse(dd_Acordialidadyatenciondelpersonaltecnico.SelectedValue);
               int tratoyatenciondelpersonaldeingenieria = int.Parse(dd_Atratoyatenciondelpersonaldeingenieria.SelectedValue);
               int tratoatencionyrespuestadelpersonaldecallcenter = int.Parse(dd_Atratoatencionyrespuestadelpersonaldecallcenter.SelectedValue);

               NA_EncuestaMantenimiento nencuesta = new NA_EncuestaMantenimiento();
               nencuesta.insertarEncuestaMantenimiento( codproyecto,  cumplimientofechasplanificadasmantenimiento,
                                                         funcionamientodelosequipos,
                                                         rapidezdelasreparaciones,
                                                         resolucionefectivadelacausadereparacion,
                                                         asesoramientoyrapidezenlaentregadecotizacionesinformes,
                                                         tiempoderespuestaanteunaemergencia,
                                                         resolucionefectivadelasemergencias,
                                                         cordialidadyatenciondelpersonaldecobranza,
                                                         tratoyatenciondelpersonalasministrativo,
                                                         cordialidadyatenciondelpersonaltecnico,
                                                         tratoyatenciondelpersonaldeingenieria,
                                                         tratoatencionyrespuestadelpersonaldecallcenter);
               Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
               string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
               Response.Redirect(ruta + "/Presentacion/FA_MenuPorArea.aspx");
           }
           else {
               string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
               Response.Redirect(ruta + "/Presentacion/FA_encuestajyc.aspx");
           }

        }

        protected void bt_cancelar_Click(object sender, EventArgs e)
        {
            string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
            Response.Redirect(ruta + "/Presentacion/FA_encuestajyc.aspx");
        }
    }
}