using jycboliviaASP.net.Datos;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCopal_ConsultaStockVendedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            /*if(tienePermiso(123) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx" );
            }*/
            if (!IsPostBack)
            {

            }

        }

        private bool tienePermiso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usu = Session["NameUser"].ToString();
            string pass = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usu, pass);

            NA_DetallePermiso Nper = new NA_DetallePermiso();
            return Nper.tienePermisoResponsable(permiso, codUser);
        }

        [WebMethod]
        [ScriptMethod]
        public static string[] getListResponsable(string prefixText, int count)
        {
            string nombre = prefixText;
            NA_Responsables Nresp = new NA_Responsables();
            DataSet tuplas = Nresp.mostrarTodosDatos2(nombre);

            int fin = tuplas.Tables[0].Rows.Count;
            string[] lista = new string[fin];

            for(int i=0; i<fin; i++)
            {
                string cod = tuplas.Tables[0].Rows[i]["codigo"].ToString();
                string nombreResp = tuplas.Tables[0].Rows[i]["nombre"].ToString();

                lista[i] = $"{cod} - {nombreResp}";
            }
            return lista;
        }

        private void GET_StockProductosPorVendedor(int cod)
        {
            try
            {
                DCorpal_StockVendedores Nstock = new DCorpal_StockVendedores();
                DataSet ds = Nstock.GET_verStockProductosVendedor(cod);
                gv_stockProductos.DataSource = ds;
                gv_stockProductos.DataBind();
            } catch( Exception ex)
            {
                showalert("Error en el metodo al obtener datos del Stock. " + ex.Message);
            }
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }

        protected void btn_registrarAprobacion_Click(object sender, EventArgs e)
        {
            try
            {
                validarResponsable();

                int codResponsable = int.Parse(hf_codResponsable.Value);
                GET_StockProductosPorVendedor(codResponsable);

            } catch(Exception ex)
            {
                showalert("Error al buscar los registros. " + ex.Message);
            }

        }
        private bool validarResponsable()
        {
            string codResponsable1 = hf_codResponsable.Value;
            if (string.IsNullOrEmpty(codResponsable1))
            {
                showalert("Por favor selecciona un responsable válido.");
                return false;
            }
            return true;
        }

        private void limpiarForm()
        {
            hf_codResponsable.Value = string.Empty;
            tx_responsable.Text = string.Empty;
            gv_stockProductos.DataSource = null;
            gv_stockProductos.DataBind();
            gv_stockProductos.Visible = false;
        }

        private void showalert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }
    }
}