using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_encuestajyc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
          /*  if (tienePermisoDeIngreso(25) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            */
            if (!IsPostBack)
            {
               
            }
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


       
        private void cargarSeleccion()
        {

            NProyecto nproy = new NProyecto();
            string edificio = tx_edificio.Text;
            int codigo = nproy.getCodigoProyect(edificio);

            NProyecto proyecto = new NProyecto();
            DataSet tuplaResp = proyecto.getProyect(codigo);
            
            tx_direccionEdificio.Text = tuplaResp.Tables[0].Rows[0][3].ToString();

            string codigoEncargado = tuplaResp.Tables[0].Rows[0][5].ToString();
           // string codigoZona = tuplaResp.Tables[0].Rows[0][8].ToString();
            string Departamento = tuplaResp.Tables[0].Rows[0][9].ToString();
            if (Departamento != "")
            {
                dd_departamento.SelectedValue = Departamento;
            }

            NEncargadoPago encargadoP = new NEncargadoPago();
            if (codigoEncargado != "")
            {
                string nombreEncargado = encargadoP.getEncargadoPago(Convert.ToInt32(codigoEncargado));
                DataSet resultEncargado = encargadoP.buscarEncargadoPago2(Convert.ToInt32(codigoEncargado));

                tx_encargadoPago.Text = nombreEncargado;
                //tx_carnet.Text = resultEncargado.Tables[0].Rows[0][2].ToString();
                tx_telefonoEncargado.Text = resultEncargado.Tables[0].Rows[0][3].ToString();
                tx_celularEncargado.Text = resultEncargado.Tables[0].Rows[0][4].ToString();
                tx_direccionEncargado.Text = resultEncargado.Tables[0].Rows[0][5].ToString();
                tx_correoEncargado.Text = resultEncargado.Tables[0].Rows[0][6].ToString();
                tx_observacionesEncargado.Text = resultEncargado.Tables[0].Rows[0][10].ToString();

                tx_nitEncargado.Text = tuplaResp.Tables[0].Rows[0][10].ToString();
                tx_facturarAEncargado.Text = tuplaResp.Tables[0].Rows[0][11].ToString();
                tx_bancoEncargado.Text = tuplaResp.Tables[0].Rows[0][12].ToString();
            }
            else
            {
                tx_encargadoPago.Text = "";
               // tx_carnet.Text = "";
                tx_celularEncargado.Text = "";
                tx_direccionEncargado.Text = "";
                tx_facturarAEncargado.Text = "";
                tx_correoEncargado.Text = "";
                tx_nitEncargado.Text = "";
                tx_telefonoEncargado.Text = "";

                tx_observacionesEncargado.Text = "";
                tx_bancoEncargado.Text = "";
            }
            
        }

        protected void bt_verificar_Click1(object sender, EventArgs e)
        {
          //  tx_encargadoPago.Text = "hola mundo";
           // tx_direccionEncargado.Text = "hola mundo";
                cargarSeleccion();
        }

        protected void bt_Actualizar_Click(object sender, EventArgs e)
        {
            actualizardatosProyecto();
        }

        private void actualizardatosProyecto()
        {
            NProyecto nproy = new NProyecto();
            string nombreProyecto = tx_edificio.Text;
            int codigoProyecto = nproy.getCodigoProyect(nombreProyecto);
            string direccionProyecto = tx_direccionEdificio.Text;
            string departamento = dd_departamento.SelectedValue;

            int CodencargadoPago = nproy.obtenerCodigoEncargadoPago(codigoProyecto);
            
            NEncargadoPago nencargadoP = new NEncargadoPago();

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            NA_Historial nhistorial = new NA_Historial();
            if (CodencargadoPago > 0)  // existe encargado de PAgo
            {
                string nombreEncargadoPago = tx_encargadoPago.Text;                
                string telefono = tx_telefonoEncargado.Text;
                string celular = tx_celularEncargado.Text;
                string direccionEncargado = tx_direccionEncargado.Text;
                string correoEncargado = tx_correoEncargado.Text;
                string facturarA = tx_facturarAEncargado.Text;
                string nitEncargado = tx_nitEncargado.Text;
                string banco = tx_bancoEncargado.Text;
                string observacion = tx_observacionesEncargado.Text;
                nencargadoP.modificar(CodencargadoPago, nombreEncargadoPago, "", telefono, celular, direccionEncargado, correoEncargado, facturarA, nitEncargado, banco, observacion);
                nhistorial.insertar(codUser, "Ha Modificado un Encargado Pago " + nombreEncargadoPago);

                nproy.modificarEncargadoProyPP2(codigoProyecto, nombreProyecto, direccionProyecto, CodencargadoPago, codUser, 1,  departamento, nitEncargado, facturarA, banco);
                nhistorial.insertar(codUser, "Ha Modificado el proyecto " + nombreProyecto);
                Response.Write("<script type='text/javascript'> alert('Datos Modificados con Exito') </script>");
            }
            else   //  no existe el encargado de pago  
            {
                string nombreEncargadoPago = tx_encargadoPago.Text;                
                string telefono = tx_telefonoEncargado.Text;
                string celular = tx_celularEncargado.Text;
                string direccionEncargado = tx_direccionEncargado.Text;
                string mailEncargadoP = tx_correoEncargado.Text;
                string facturarA = tx_facturarAEncargado.Text;
                string nitEncargado = tx_nitEncargado.Text;
                string banco = tx_bancoEncargado.Text;
                string observacion = tx_observacionesEncargado.Text;
                nencargadoP.registrar(nombreEncargadoPago, "", telefono, celular, direccionEncargado, mailEncargadoP, facturarA, nitEncargado, 1, banco, observacion);
                CodencargadoPago = nencargadoP.obtenerUltimoCodigo_insertado();
                nhistorial.insertar(codUser, "Ha Insertado un Encargado Pago " + nombreEncargadoPago + " codigo =" + CodencargadoPago);
                nproy.modificarEncargadoProyPP2(codigoProyecto, nombreProyecto, direccionProyecto, CodencargadoPago, codUser, 1, departamento, nitEncargado, facturarA, banco);                
                Response.Write("<script type='text/javascript'> alert('Datos Modificados con Exito') </script>");
            }  
        }

        protected void bt_encuesta_Click(object sender, EventArgs e)
        {
            cargarEncuestaManteniento();
            string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
            Response.Redirect(ruta + "/Presentacion/FA_EncuestaMantenimientoPreguntas.aspx");
        }

        private void cargarEncuestaManteniento()
        {            
            NProyecto nproy = new NProyecto();
            string nombreProyecto = tx_edificio.Text;
            int codigoProyecto = nproy.getCodigoProyect(nombreProyecto);
            Session["EM_codproy"] = codigoProyecto;
            string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
            Response.Redirect(ruta + "/Presentacion/FA_EncuestaMantenimientoPreguntas.aspx");

        }

        protected void bt_cancelar_Click(object sender, EventArgs e)
        {
            string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
            Response.Redirect(ruta + "/Presentacion/FA_MenuPorArea.aspx");
        }
    }
}