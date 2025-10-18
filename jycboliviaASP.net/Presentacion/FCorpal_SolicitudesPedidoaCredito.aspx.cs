using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_SolicitudesPedidoaCredito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            /*if (tienePermiso(123) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }*/
            if (!IsPostBack)
            {
                ListaSolicitudesPedidoCredito();
                gv_listProductos.Visible=false;

            }
           

        }


        /* Lista solicitudes */
        private void ListaSolicitudesPedidoCredito()
        {
            NCorpal_EntregaSolicitudProducto2 nego = new NCorpal_EntregaSolicitudProducto2();
            DataSet datos = nego.get_listaPedidosACredito();
            gv_solicitudesProductos.DataSource = datos;
            gv_solicitudesProductos.DataBind();
        }

        private bool tienePermiso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso Nper = new NA_DetallePermiso();
            return Nper.tienePermisoResponsable(permiso, codUser);
        }




        /********    LISTA DE PRODUCTOS DE PEDIDO A CREDITO   ********/
        protected void chkSolicitud_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow rowAct = (GridViewRow)chk.NamingContainer;

            foreach(GridViewRow row in gv_solicitudesProductos.Rows)
            {
                CheckBox chkSol = (CheckBox)row.FindControl("chkSolicitud");

                if (chkSol != chk)
                {
                    chkSol.Checked = false;
                } 
            }

            if (chk.Checked)
            {
                int index = rowAct.RowIndex;
                int codigo = Convert.ToInt32(gv_solicitudesProductos.DataKeys[index].Value);

                MostrarDetalleSolicitud(codigo);
            }
            else
            {
                ocultarDetalledeSolicitud();
            }
        }

        private void MostrarDetalleSolicitud(int cod)
        {
            try
            {
                NCorpal_EntregaSolicitudProducto2 neg = new NCorpal_EntregaSolicitudProducto2();
                DataSet ds = neg.get_listDetallePedidoaCredito(cod);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gv_listProductos.DataSource = ds;
                    gv_listProductos.DataBind();
                    gv_listProductos.Visible = true;
                }
                else
                {
                    gv_listProductos = null;
                    gv_listProductos.DataBind();
                    gv_listProductos.Visible = false;
                }
            }
            catch(Exception ex)
            {
                showalert("Error al seleccionar el Item no hay datos. " + ex.Message);
            }
        }
        
        private void ocultarDetalledeSolicitud()
        {
            gv_listProductos.DataSource = null;
            gv_listProductos.DataBind();
            gv_listProductos.Visible = false;
        }

        
        private void showalert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }
    }
}