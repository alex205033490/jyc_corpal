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
    public partial class FCorpal_ObjetivoProduccion : System.Web.UI.Page
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
                /*NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                tx_responsableEntrega.Text = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
                llenarProductosNax();
                ponerMedidadelProducto();
                buscarDatos("", "");*/
            }

            
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
            buscarDatos();
        }

        private void buscarDatos()
        {
            string fechalimite = convertirFecha(tx_fechalimite.Text);
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            string producto = dd_productosNax.SelectedItem.Text;

            NCorpal_Produccion np = new NCorpal_Produccion();
            DataSet datos = np.get_objetivosDeProduccion(fechalimite,producto);
            
        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            insertardatos();
        }

        public string convertirFecha(string fecha)
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


        private void insertardatos()
        {
            string fechalimite = convertirFecha(tx_fechalimite.Text);

            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            string producto = dd_productosNax.SelectedItem.Text;
            int codprod = pp.get_CodigoProductos(producto);    

            float cantidadprod;
            float.TryParse(tx_cantcajas.Text.Replace('.',','), out cantidadprod );                    
            string medida = tx_medida.Text; 
            string detalle = tx_detalle.Text; 
             //---------------------------------------
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            //---------------------------------------
            int codusergra = codUser;
            string respgra = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();

            NCorpal_Produccion cp = new NCorpal_Produccion();
            bool bandera = cp.set_objetivoProduccion(fechalimite, codprod, producto, cantidadprod, medida, detalle, codusergra, respgra);
            if (bandera)
            {
                Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('ERROR: No Ingreso Datos') </script>");
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_cantcajas.Text = "0";
            tx_detalle.Text = "";
            tx_fechalimite.Text = "0";
            tx_medida.Text = "";
            dd_productosNax.SelectedIndex = 0;

        }
    }
}