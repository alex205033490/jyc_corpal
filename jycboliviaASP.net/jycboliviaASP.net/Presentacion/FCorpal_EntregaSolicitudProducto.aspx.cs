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

                dd_estadoCierre.SelectedValue = gv_solicitudesProductos.SelectedRow.Cells[10].Text;

                string detalleCierre = gv_solicitudesProductos.SelectedRow.Cells[11].Text;
                if (!string.IsNullOrEmpty(detalleCierre) && !detalleCierre.Equals("&nbsp;"))
                {
                    dd_motivoCierre.SelectedValue = detalleCierre;
                }
                else
                    dd_motivoCierre.SelectedIndex = 0;


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

        public string aFecha(string fecha)
        {
            if (fecha == "" || fecha == "&nbsp;")
            {
                return fecha = "null";
            }

            else
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = "'" + anio + "/" + mes + "/" + dia + "'";
                return _fecha;
            }
        }

        private void actualizarTodoslosDatos()
        {
            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            if (gv_solicitudesProductos.SelectedIndex > -1)
            {
                int codigoSolicitud = int.Parse(gv_solicitudesProductos.SelectedRow.Cells[2].Text);
               // float sumStock = nss.get_SumStockTotal(codigoSolicitud);
                bool banderaerror = false;
                foreach (GridViewRow row in gv_detallesolicitud.Rows)
                {                    
                    int codProducto ;
                    int.TryParse(row.Cells[0].Text, out codProducto);
                    float StockProductos = nss.get_Stock(codProducto );

                    float cantAentregar;
                    TextBox tx_cant2 = row.Cells[6].FindControl("tx_cantidadEntregarOK") as TextBox;
                    float.TryParse(tx_cant2.Text, out cantAentregar);
                    if (cantAentregar > StockProductos) {
                        banderaerror = true;
                    }                   
                }

                if(banderaerror == false){
                    foreach (GridViewRow row in gv_detallesolicitud.Rows)
                    {
                        int codProducto;
                        int.TryParse(row.Cells[0].Text, out codProducto);
                        float cantAentrego;
                        Label tx_cant = row.Cells[5].FindControl("lb_cantentregado") as Label;
                        float.TryParse(tx_cant.Text, out cantAentrego);

                        float cantAentregar;
                        TextBox tx_cant2 = row.Cells[6].FindControl("tx_cantidadEntregarOK") as TextBox;
                        float.TryParse(tx_cant2.Text, out cantAentregar);

                        float totalEntregado = cantAentrego + cantAentregar;
                        bool bb = nss.update_cantProductosEntregados(codigoSolicitud, codProducto, totalEntregado, cantAentregar);
                    }

                    NA_Responsables Nresp = new NA_Responsables();
                    string usuarioAux = Session["NameUser"].ToString();
                    string passwordAux = Session["passworuser"].ToString();
                    int codresponsable = Nresp.getCodUsuario(usuarioAux, passwordAux);
                    string nombreResponsable = Nresp.get_responsable(codresponsable).Tables[0].Rows[0][1].ToString();
                    string fechaEntrega = aFecha(tx_fechaEngrega.Text);
                    string horaEntrega = tx_horaentrega.Text;

                    string estadoCierre = dd_estadoCierre.SelectedItem.Text;
                    string motivoCierre = dd_motivoCierre.SelectedItem.Text;

                    bool cerrado = nss.update_cerrarSolicitud(codigoSolicitud, codresponsable, nombreResponsable, estadoCierre, motivoCierre, fechaEntrega, horaEntrega);
                    if (cerrado == true)
                    {
                        limpiarDatos();
                        buscarDatosSolicitud("", "");
                        Session["codigoEntregaSolicitudProducto"] = codigoSolicitud;
                        Response.Redirect("../Presentacion/FCorpal_ReporteEntregaSolicitudProducto.aspx");
                    }
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Suma Entrega mayor a Stock') </script>");  

                }else
                    Response.Write("<script type='text/javascript'> alert('Error: Suma Entrega mayor a Stock') </script>");
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

        protected void gv_detallesolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}