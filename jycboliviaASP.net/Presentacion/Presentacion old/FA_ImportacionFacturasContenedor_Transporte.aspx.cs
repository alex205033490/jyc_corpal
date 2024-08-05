using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ImportacionFacturasContenedor_Transporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(91) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                Limpiar_Datos();
                Buscar_Datos("", "", "", "", "", "");
                cargarddlEstadoEquipo();

            }
        }

        private void Limpiar_Datos()
        {
            tx_edificio.Text = "";
            tx_exbo.Text = "";
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

        private void cargarddlEstadoEquipo()
        {
            NEstadoEquipo estadoEquipo = new NEstadoEquipo();
            dd_estado.DataSource = estadoEquipo.listar();
            dd_estado.DataValueField = "codigo";
            dd_estado.DataTextField = "nombre";
            dd_estado.Items.Add(new ListItem("", "-1"));
            dd_estado.AppendDataBoundItems = true;
            dd_estado.SelectedIndex = -1;
            dd_estado.DataBind();
        }


        protected void bt_VerificarContenedor_Click(object sender, EventArgs e)
        {
            verificaryColocarDatos();
        }

        public string convertidorFecha(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'" + _fecha + "'";
            }
            else
                return "null";
        }

        [WebMethod]
        [ScriptMethod]
        public static string[] getListaContenedor(string prefixText, int count)
        {
            string contenedor = prefixText;
            //-------------------cambio de base de datos------------------
            string _nombreDBOriginal = System.Web.HttpContext.Current.Session["NombreBaseDatos"].ToString();
            NA_VariablesGlobales nvar = new NA_VariablesGlobales();
            string baseDeDatosTemporal = nvar.getBasedeDatosTemporal(NA_VariablesGlobales.CiudadParaIngresarDui);
            System.Web.HttpContext.Current.Session["NombreBaseDatos"] = baseDeDatosTemporal;
            //-------------------fin de cambio de datos-------------------

            NA_Importacion nimportacion = new NA_Importacion();
            DataSet tuplas = nimportacion.getTodoslosContenedores(contenedor);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            //-------------------volver la base dedatos base de datos------------------                            
            System.Web.HttpContext.Current.Session["NombreBaseDatos"] = _nombreDBOriginal;
            //-------------------fin de volver base de datos-------------------

            return lista;
        }

        private void verificaryColocarDatos()
        {
            string nroContenedor = tx_nroContenedor.Text;
            NA_Importacion nimp = new NA_Importacion();
            bool existeContenedor = nimp.existeContenedor(nroContenedor);
            if(existeContenedor){
                DataSet datos = nimp.getFacturasContenedor(nroContenedor);
                tx_fechaASP.Text = datos.Tables[0].Rows[0][1].ToString();
                tx_nroASPB.Text = datos.Tables[0].Rows[0][2].ToString();                
                tx_montoASPB.Text = datos.Tables[0].Rows[0][3].ToString();

                tx_fechaMSC.Text = datos.Tables[0].Rows[0][4].ToString();
                tx_nroMSC.Text = datos.Tables[0].Rows[0][5].ToString();
                tx_montoMSC.Text = datos.Tables[0].Rows[0][6].ToString();

                tx_fechaTHC.Text = datos.Tables[0].Rows[0][7].ToString();
                tx_nroTHC.Text = datos.Tables[0].Rows[0][8].ToString();
                tx_montoTHC.Text = datos.Tables[0].Rows[0][9].ToString();

                Buscar_Datos("", "", "", "", "", nroContenedor);
            }

        }

        protected void bt_actualizarContenedor_Click(object sender, EventArgs e)
        {

            //-------------------cambio de base de datos------------------
            string _nombreDBOriginal = Session["NombreBaseDatos"].ToString();
            NA_VariablesGlobales nvar = new NA_VariablesGlobales();
            string baseDeDatosTemporal = nvar.getBasedeDatosTemporal(NA_VariablesGlobales.CiudadParaIngresarDui);
            Session["NombreBaseDatos"] = baseDeDatosTemporal;
            //-------------------fin de cambio de datos-------------------

            string nroContenedor = tx_nroContenedor.Text;
            NA_Importacion nimp = new NA_Importacion();
            bool existeContenedor = nimp.existeContenedor(nroContenedor);
            string fechaASP = convertidorFecha(tx_fechaASP.Text);
            string nroASPB = tx_nroASPB.Text ;
            float montoASPB;
            float.TryParse(tx_montoASPB.Text.Replace('.',','), out montoASPB);
                
            string fechaMSC = convertidorFecha(tx_fechaMSC.Text);
            string nroMSC = tx_nroMSC.Text;
            float montoMSC;
            float.TryParse(tx_montoMSC.Text.Replace('.',','), out montoMSC);
            
            string fechaTHC = convertidorFecha(tx_fechaTHC.Text);
            string nroTHC = tx_nroTHC.Text;
            float montoTHC;
            float.TryParse(tx_montoTHC.Text.Replace('.', ','), out montoTHC);
            bool bandera = false;

            if (existeContenedor)
            {
                bandera = nimp.actualizarDatoscontenedor(nroContenedor, fechaASP, nroASPB, montoASPB, fechaMSC, nroMSC, montoMSC, fechaTHC, nroTHC, montoTHC);
                Buscar_Datos("", "", "", "", "", nroContenedor);
                //-------------------volver la base dedatos base de datos------------------                            
                Session["NombreBaseDatos"] = _nombreDBOriginal;
                //-------------------fin de volver base de datos-------------------
            }
            else
            {
                bandera = nimp.ingresarNuevoContendor(nroContenedor, fechaASP, nroASPB, montoASPB, fechaMSC, nroMSC, montoMSC, fechaTHC, nroTHC, montoTHC);
                Buscar_Datos("", "", "", "", "", nroContenedor);
                //-------------------volver la base dedatos base de datos------------------                            
                Session["NombreBaseDatos"] = _nombreDBOriginal;
                //-------------------fin de volver base de datos-------------------
            }

            //-------------------volver la base dedatos base de datos------------------                            
            Session["NombreBaseDatos"] = _nombreDBOriginal;
            //-------------------fin de volver base de datos-------------------

            if(bandera){
                Response.Write("<script type='text/javascript'> alert('Guardado : ok') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error : Error') </script>");

        }

        protected void gv_datos_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarContenedor();
        }

        private void limpiarContenedor()
        {
            tx_fechaASP.Text = "";
            tx_nroASPB.Text = "";
            tx_montoASPB.Text = "0";
            tx_fechaMSC.Text = "";
            tx_nroMSC.Text = "";
            tx_montoMSC.Text = "0";
            tx_fechaTHC.Text = "";
            tx_nroTHC.Text = "";
            tx_montoTHC.Text = "0";
            tx_nroContenedor.Text = "";
            
        }

        protected void bt_limpiar1_Click(object sender, EventArgs e)
        {
            limpiarContenedor();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string exbo = tx_exbo.Text;
            string edificio = tx_edificio.Text;
            string estado = dd_estado.SelectedItem.Text;
            string semanaExp = tx_sem_Exp.Text;
            string Dui = tx_nroDui.Text;
            string Contenedor = tx_contenedorBusqueda.Text;
            Buscar_Datos(edificio, exbo, estado, semanaExp, Dui, Contenedor);
        }

        private void Buscar_Datos(string Edificio, string exbo, string estado, string semanaExpedicion, string Dui, string Contenedor)
        {
            //gv_datos.DataSource = null;
            NEquipo equipo1 = new NEquipo();
            //            DataSet tuplaRes = equipo1.buscar_ProrrateoCostosGeneral_JYCIA(Edificio, exbo, estado, false, semanaExpedicion,  Dui,  Contenedor);

            DataSet tuplaRes = equipo1.buscar_FacturaContenedorGeneral_JyCIA(false, Edificio, exbo, semanaExpedicion, Dui, Contenedor);
            gv_datos.DataSource = tuplaRes;
            gv_datos.DataBind();
         //   lb_cantidad.Text = equipo1.cant_ProrrateoCostosGeneralTransporte_JyCIA(true, Edificio, exbo, semanaExpedicion, Dui, Contenedor).ToString();
            //  OnCheckedChanged_seleccionTodo();
        }

        protected void bt_Limpiar_Click(object sender, EventArgs e)
        {
            limpiarBusqueda();
        }

        private void limpiarBusqueda()
        {
            tx_edificio.Text = "";
            tx_exbo.Text = "";
            tx_contenedorBusqueda.Text = "";
            tx_nroDui.Text = "";
            tx_sem_Exp.Text = "";
            dd_estado.SelectedIndex = -1;
        }

    }
}