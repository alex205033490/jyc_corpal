using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Configuration;
using System.Data;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_OrdenProduccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(121) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {                
                llenarProductosNax();
                ponerMedidadelProducto();
                buscarDatos("");
            }
            
        }

        private void ponerMedidadelProducto()
        {
            string producto = dd_productosNax.SelectedItem.Text;
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos(producto);
            string medida = tuplas.Tables[0].Rows[0][2].ToString();
            tx_medida.Text = medida;
        }

        private void llenarProductosNax()
        {
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos("");

            dd_productosNax.DataSource = tuplas;
            dd_productosNax.DataValueField = "codigo";
            dd_productosNax.DataTextField = "producto";
            dd_productosNax.AppendDataBoundItems = true;
            dd_productosNax.SelectedIndex = 1;
            dd_productosNax.DataBind();
        }

        private void buscarDatos(string Producto)
        {
            NCorpal_Produccion npro = new NCorpal_Produccion();
            DataSet tupla = npro.get_datosOrdenProduccion(Producto);
            gv_EntregasdeProduccion.DataSource = tupla;
            gv_EntregasdeProduccion.DataBind();
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

        private void limpiarDatos()
        {
            tx_nroproduccion.Text = "";
            tx_cantcajas.Text = "";            
            tx_detalle.Text = "";            
            tx_medida.Text = "";
            tx_fechaProduccion.Text = "";
            tx_horaproduccion.Text = "";
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {

        }

        protected void dd_productosNax_SelectedIndexChanged(object sender, EventArgs e)
        {
            ponerMedidadelProducto();
        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            insertarDatosOrdenProduccion();
        }

        private void insertarDatosOrdenProduccion()
        {
           string nroProduccion = tx_nroproduccion.Text;
           float cantcajas;
           float.TryParse(tx_cantcajas.Text.Replace('.',',') , out cantcajas); 
           string detalleProduccion = tx_detalle.Text;
           string medidaProduccion = tx_medida.Text;
           string fechaProduccion = convertidorFecha(tx_fechaProduccion.Text);           
           int codProducto;
           int.TryParse(dd_productosNax.SelectedValue.ToString(), out codProducto);
           string producto = dd_productosNax.SelectedItem.Text;

           NA_Responsables Nresp = new NA_Responsables();
           string usuarioAux = Session["NameUser"].ToString();
           string passwordAux = Session["passworuser"].ToString();
           int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
           string responsable = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();

           NCorpal_Produccion nproduccion = new NCorpal_Produccion();
           bool bandera = nproduccion.insertarOrdenProduccion(fechaProduccion, codProducto, producto, cantcajas, medidaProduccion, detalleProduccion, codUser, responsable);
            
        }

        private string convertidorFecha(string p)
        {
            throw new NotImplementedException();
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarDatosOrdenProduccion();
        }

        private void eliminarDatosOrdenProduccion()
        {
            if(gv_OrdendeProduccion.SelectedIndex >= 0 ){
                int codigoOrden;
                int.TryParse(gv_OrdendeProduccion.SelectedRow.Cells[1].Text , out codigoOrden);
                NCorpal_Produccion nproduccion = new NCorpal_Produccion();
                bool bandera = nproduccion.eliminar_ordenProduccion(codigoOrden);
            }
        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            if(gv_OrdendeProduccion.SelectedIndex >= 0){
                modificarDatosOrdenProduccion();            
            }
        }

        private void modificarDatosOrdenProduccion()
        {
            int codigoOrden;
            int.TryParse(gv_OrdendeProduccion.SelectedRow.Cells[1].Text, out codigoOrden);

            string nroProduccion = tx_nroproduccion.Text;
            float cantcajas;
            float.TryParse(tx_cantcajas.Text.Replace('.', ','), out cantcajas);
            string detalleProduccion = tx_detalle.Text;
            string medidaProduccion = tx_medida.Text;
            string fechaProduccion = convertidorFecha(tx_fechaProduccion.Text);
            int codProducto;
            int.TryParse(dd_productosNax.SelectedValue.ToString(), out codProducto);
            string producto = dd_productosNax.SelectedItem.Text;

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            string responsable = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();

            NCorpal_Produccion nproduccion = new NCorpal_Produccion();
            bool bandera = nproduccion.modificarOrdenProduccion(codigoOrden, fechaProduccion, codProducto, producto, cantcajas, medidaProduccion, detalleProduccion, codUser, responsable);
        }

     
    }
}