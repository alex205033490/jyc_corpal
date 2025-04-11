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
    public partial class FCorpal_DespachoCamiones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(146) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                mostrarRegistrosDespachoProductos("", "", "Abierto",0);
                cargarVehiculos();
            }
            
        }

        private void mostrarRegistrosDespachoProductos(string fechadesde, string fechahasta, string estado, int codVehiculo)
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            DataSet datos = negocio.get_despachosdeCamiones(fechadesde,fechahasta,estado, codVehiculo);
            gv_despachos.DataSource = datos;
            gv_despachos.DataBind();
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

        // Cargar Vehiculos 
        private void cargarVehiculos()
        {
            NA_SolicitudEntregaProductoACamion negocio = new NA_SolicitudEntregaProductoACamion();

            DataSet dsVehiculos = negocio.get_ShowVehiculos();

            if (dsVehiculos != null && dsVehiculos.Tables.Count > 0)
            {
                dd_listVehiculo.DataSource = dsVehiculos.Tables[0];
                dd_listVehiculo.DataTextField = "detalle";
                dd_listVehiculo.DataValueField = "codigo";

                dd_listVehiculo.DataBind();

                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Seleccione un Vehiculo", "0");
                dd_listVehiculo.Items.Insert(0, li);
            }
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

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            dd_estadoCierre.SelectedIndex = 1;
            tx_fechaDesdeDespacho.Text = "";
            tx_fechaHastaDespacho.Text = "";
            gv_despachos.SelectedIndex = -1;
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string fechadesde = convertidorFecha(tx_fechaDesdeDespacho.Text);
            string fechahasta = convertidorFecha(tx_fechaHastaDespacho.Text);
            string estado = dd_estadoCierre.SelectedItem.Text;
            int codVehiculo = int.Parse(dd_listVehiculo.SelectedValue);
            mostrarRegistrosDespachoProductos(fechadesde, fechahasta, estado, codVehiculo);
        }

        protected void bt_actualizar_Click(object sender, EventArgs e)
        {
            guardarDatos();
        }

        private void guardarDatos()
        {
            if (gv_despachos.SelectedIndex >= 0) {
                
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                //  string estado = dd_estadoCierre.SelectedItem.Text;
                string estado = "Cerrado";
                int codigo = int.Parse(gv_despachos.SelectedRow.Cells[1].Text);

                NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
                bool bandera = negocio.update_despachodeproductosCamiones(codigo, estado, codUser);
                if (bandera) {
                   
                    Session["codigoDespacho"] = codigo;
                    Session["ReporteGeneral"] = "Reporte_DespachoProductoCamionEntrega";
                    Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");                    
                }else
                    Response.Write("<script type='text/javascript'> alert('Error: Guardado') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Dato') </script>");
        }

        protected void bt_verRecibo_Click(object sender, EventArgs e)
        {
            if (dd_listVehiculo.SelectedIndex >= 0)
            {
                int codigo = int.Parse(gv_despachos.SelectedRow.Cells[1].Text);                
                Session["codigoDespacho"] = codigo;
                Session["ReporteGeneral"] = "Reporte_DespachoProductoCamionEntrega";
                Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");
            }
        }

        protected void dd_listVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fechadesde = convertidorFecha(tx_fechaDesdeDespacho.Text);
            string fechahasta = convertidorFecha(tx_fechaHastaDespacho.Text);
            string estado = dd_estadoCierre.SelectedItem.Text;
            int codVehiculo = int.Parse(dd_listVehiculo.SelectedValue);
            mostrarRegistrosDespachoProductos(fechadesde, fechahasta, estado, codVehiculo);
        }

        protected void gv_solicitudesProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionDatos();
        }

        private void seleccionDatos()
        {
            throw new NotImplementedException();
        }
    }
}