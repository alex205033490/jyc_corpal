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

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_DevoluciondeProductoTerminado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
           /* if (tienePermisoDeIngreso(121) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }*/
            if (!IsPostBack)
            {
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                tx_nombreVendedor.Text = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
                llenarProductosNax();                
                buscarDatos("", "");
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

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaResponsable2(string prefixText, int count)
        {
            string nombreResponsable = prefixText;

            NA_Responsables Nrespon = new NA_Responsables();
            DataSet tuplas = Nrespon.mostrarSoloAutorizados_AutoComplit(nombreResponsable, "2,6,7,9,10,11,13");
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
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


        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_almaceneroquerecibe.Text = "";
            tx_cantcajas.Text = "0";
            tx_fechaDevolucion.Text = "";
            tx_MotivoDevolucion.Text = "";
           // tx_nombreVendedor.Text = "";
            tx_observacionesDevolucion.Text = "";
            dd_productosNax.SelectedIndex = 0;
            dd_seenviaraa.SelectedIndex = 0;
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string vendedor = tx_nombreVendedor.Text;
            string producto = dd_productosNax.SelectedItem.Text;
            buscarDatos(vendedor, producto);
        }

        private void buscarDatos(string vendedor, string producto)
        {
            NCorpal_Produccion npp = new NCorpal_Produccion();
            DataSet datos = npp.get_devolucionProductos(vendedor, producto);
            gv_DevoluciondelProduccion.DataSource = datos;
            gv_DevoluciondelProduccion.DataBind();
        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            guardarDatos();
        }

        private void guardarDatos()
        {
            string fechadevolucion = convertidorAFecha(tx_fechaDevolucion.Text);
            string vendedor = tx_nombreVendedor.Text;
            //--------------------------------------------
            NA_Responsables Nresp = new NA_Responsables();            
            int codvendedor = Nresp.getCodigo_NombreResponsable(vendedor);
            //--------------------------
            string producto = dd_productosNax.SelectedItem.Text;
            NCorpal_Produccion pp = new NCorpal_Produccion();
            int codproducto;
            int.TryParse(dd_productosNax.SelectedValue.ToString(), out codproducto);
            float cantidad;
            float.TryParse(tx_cantcajas.Text.Replace('.',','), out cantidad);
            string almacenerorecibe = tx_almaceneroquerecibe.Text;
            string motivodevolucion = tx_MotivoDevolucion.Text;
            string seenviaa = dd_seenviaraa.SelectedItem.Text;       
            string observacionesdevolucion = tx_observacionesDevolucion.Text;
            string medida = tx_medida.Text;

            NCorpal_Produccion Npp = new NCorpal_Produccion();
            bool bandera = Npp.insertarDevolucionProduccion( fechadevolucion, vendedor, codvendedor, producto, codproducto, cantidad, almacenerorecibe, motivodevolucion, seenviaa, observacionesdevolucion, medida);
            if(bandera == true){
                Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
            }

        }

        private string convertidorAFecha(string fecha)
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

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            modificarDatos();
        }

        private void modificarDatos()
        {
          if(gv_DevoluciondelProduccion.SelectedIndex > -1){
              int codigoD;
              int.TryParse(gv_DevoluciondelProduccion.SelectedRow.Cells[1].Text, out codigoD);

              string fechadevolucion = convertidorAFecha(tx_fechaDevolucion.Text);
              string vendedor = tx_nombreVendedor.Text;
              //--------------------------------------------
              NA_Responsables Nresp = new NA_Responsables();
              int codvendedor = Nresp.getCodigo_NombreResponsable(vendedor);
              //--------------------------
              string producto = dd_productosNax.SelectedItem.Text;
              NCorpal_Produccion pp = new NCorpal_Produccion();
              int codproducto;
              int.TryParse(dd_productosNax.SelectedValue.ToString(), out codproducto);
              float cantidad;
              float.TryParse(tx_cantcajas.Text.Replace('.', ','), out cantidad);
              string almacenerorecibe = tx_almaceneroquerecibe.Text;
              string motivodevolucion = tx_MotivoDevolucion.Text;
              string seenviaa = dd_seenviaraa.SelectedItem.Text;
              string observacionesdevolucion = tx_observacionesDevolucion.Text;
              string medida = tx_medida.Text;

              NCorpal_Produccion Npp = new NCorpal_Produccion();
              bool bandera = Npp.modificarDevolucionProduccion(codigoD, fechadevolucion, vendedor, codvendedor, producto, codproducto, cantidad, almacenerorecibe, motivodevolucion, seenviaa, observacionesdevolucion, medida);
              if (bandera == true)
              {
                  Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
              } else
                  Response.Write("<script type='text/javascript'> alert('ERROR: Guardado') </script>");
          }else
              Response.Write("<script type='text/javascript'> alert('Error: Seleccione Datos') </script>");
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarDatos();
        }

        private void eliminarDatos()
        {
            if (gv_DevoluciondelProduccion.SelectedIndex > -1)
            {
                int codigoD;
                int.TryParse(gv_DevoluciondelProduccion.SelectedRow.Cells[1].Text, out codigoD);
                                
                NCorpal_Produccion Npp = new NCorpal_Produccion();
                bool bandera = Npp.eliminarDevolucionProduccion(codigoD);
                if (bandera == true)
                {
                    Response.Write("<script type='text/javascript'> alert('Eliminado: OK') </script>");
                }
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccione Datos') </script>");
        }

        protected void bt_verRecibo_Click(object sender, EventArgs e)
        {

        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {

        }

        protected void gv_DevoluciondelProduccion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dd_productosNax_SelectedIndexChanged(object sender, EventArgs e)
        {
            ponerMedidadelProducto();
        }

        private void ponerMedidadelProducto()
        {
            string producto = dd_productosNax.SelectedItem.Text;
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos(producto);
            string medida = tuplas.Tables[0].Rows[0][2].ToString();
            tx_medida.Text = medida;
        }
       
    }
}