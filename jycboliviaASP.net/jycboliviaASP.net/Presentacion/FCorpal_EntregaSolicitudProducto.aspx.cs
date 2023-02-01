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
    public partial class FCorpal_EntregaSolicitudProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(119) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {
                buscarDatosSolicitud("","");
            }

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);                       
            tx_entregoSolicitud.Text = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
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

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string nroSolicitud = tx_nrosolicitud.Text;
            string solicitante = tx_SolicitanteProducto.Text;
            buscarDatosSolicitud(nroSolicitud, solicitante);
        }

        private void buscarDatosSolicitud(string nroSolicitud, string solicitante)
        {
            NCorpal_SolicitudEntregaProducto ncc = new NCorpal_SolicitudEntregaProducto();
            DataSet datos = ncc.get_solicitudesRealizadasProductos(nroSolicitud, solicitante);
            gv_solicitudesProductos.DataSource = datos;
            gv_solicitudesProductos.DataBind();
        }

        protected void gv_solicitudesProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarDatos();
        }

        private void seleccionarDatos()
        {
            if(gv_solicitudesProductos.SelectedIndex > -1){
                int codigoSolicitud = int.Parse(gv_solicitudesProductos.SelectedRow.Cells[2].Text);
                string nroboleta = gv_solicitudesProductos.SelectedRow.Cells[3].Text;
                string fechaentrega = gv_solicitudesProductos.SelectedRow.Cells[6].Text;
                string horaentrega = gv_solicitudesProductos.SelectedRow.Cells[7].Text;
                string personaSolicitud = gv_solicitudesProductos.SelectedRow.Cells[8].Text;

                tx_nrosolicitud.Text = nroboleta;
                tx_SolicitanteProducto.Text = personaSolicitud;
                tx_fechaEngrega.Text = fechaentrega;
                tx_horaentrega.Text = horaentrega;

                NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
                DataSet datos = nss.get_datosSolicitudProductos(codigoSolicitud);
                gv_detallesolicitud.DataSource = datos;
                gv_detallesolicitud.DataBind();
            }
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarSolicitud();
        }

        private void eliminarSolicitud()
        {
            foreach (GridViewRow row in gv_solicitudesProductos.Rows)
            {
                CheckBox cbokeliminar = row.Cells[1].FindControl("cbk_eliminar") as CheckBox;
                if (cbokeliminar.Checked)
                {
                    int codigoSolicitud = int.Parse(row.Cells[2].Text);
                    NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
                    bool bandera = nss.eliminarSolicitud(codigoSolicitud);
                }
            }
            buscarDatosSolicitud("", "");
        }

        protected void bt_actualizar_Click(object sender, EventArgs e)
        {
            actualizarTodoslosDatos();
        }

        private void actualizarTodoslosDatos()
        {
            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            if (gv_solicitudesProductos.SelectedIndex > -1)
            {
                int codigoSolicitud = int.Parse(gv_solicitudesProductos.SelectedRow.Cells[2].Text);

                foreach (GridViewRow row in gv_detallesolicitud.Rows)
                {
                    int codigoP = int.Parse(row.Cells[0].Text);
                    float cantEntregado;
                    TextBox tx_cant = row.Cells[5].FindControl("tx_cantentregado") as TextBox;
                    float.TryParse(tx_cant.Text,out cantEntregado);
                    bool bb = nss.update_cantProductosEntregados(codigoSolicitud,codigoP,cantEntregado);
                }

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codresponsable = Nresp.getCodUsuario(usuarioAux, passwordAux);
                string nombreResponsable = Nresp.get_responsable(codresponsable).Tables[0].Rows[0][1].ToString();

                bool cerrado = nss.update_cerrarSolicitud(codigoSolicitud, codresponsable, nombreResponsable);
                if (cerrado == true) {
                    limpiarDatos();
                    buscarDatosSolicitud("", "");
                }
            }
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_nrosolicitud.Text = "";
            tx_fechaEngrega.Text = "";
            tx_horaentrega.Text = "";
            tx_SolicitanteProducto.Text = "";
            gv_solicitudesProductos.SelectedIndex = -1;
            gv_detallesolicitud.DataSource = null;
            gv_detallesolicitud.DataBind();
        }
    }
}