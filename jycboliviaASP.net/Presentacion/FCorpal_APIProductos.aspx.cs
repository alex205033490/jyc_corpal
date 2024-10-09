using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIproductos.ApiResponseProd;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static jycboliviaASP.net.Negocio.NA_APIproductos;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;



namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
////////////////////////////////////////////      GET - BUSCAR PRODUCTO POR NOMBRE
        protected async void btn_buscarProdNombre_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txt_nomProducto.Text.Trim();
            if (string.IsNullOrEmpty(criterioBusqueda))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, ingrese el nombre del producto.');", true);
                return;
            }
            string usuario = "adm";
            string password = "123";

            var BuscProducto = new NA_APIproductos();
            List<productoCriterioGet> productos = await BuscProducto.get_ProductoCriterioAsync(usuario, password, criterioBusqueda);

            if (productos.Count > 0)
            {
                gv_prodNombre.DataSource = productos;
                gv_prodNombre.DataBind();
            }
            else
            {
                gv_prodNombre.DataSource= null;
                gv_prodNombre.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron productos con el nombre proporcionado. ');", true);

            }
        }


/////////////////////////////////////////////           GET - BUSCAR VENTAS X PRODUCTO
        protected async void btn_BuscarventProducto_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txt_ventProducto.Text.Trim();

            if (string.IsNullOrEmpty(criterioBusqueda))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor ingresa el nombre de un producto');", true);
                return;
            }
            string usuario = "adm";
            string password = "123";
            var BuscCodProd = new NA_APIproductos();
            List<productoCriterioGet> egresos = await BuscCodProd.get_ProductoVentasCriterioAsync(usuario, password, criterioBusqueda);
            
            if(egresos.Count > 0)
            {
                gv_prodVenta.DataSource = egresos;
                gv_prodVenta.DataBind();
            }
            else
            {
                gv_prodVenta.DataSource = null;
                gv_prodVenta.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron ventas con ese nombre')", true);
            }

        }


        /////////////////////////////////////////////           GET - BUSCAR COMPRAS X PRODUCTO
        protected async void btn_buscarCompras_Click(object sender, EventArgs e)
        {
            string criterioCodProducto = txt_codProductoComp.Text.Trim();
            string criterioProveedor = txt_codProveedorComp.Text.Trim();

            if (string.IsNullOrEmpty(criterioCodProducto))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor ingrese un codigo de producto valido.');", true);
                return;
            }
            if (string.IsNullOrEmpty(criterioProveedor))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor ingrese un codigo proveedor valido.');", true);
                return;
            }

            string usuario = "adm";
            string password = "123";
            var buscarCompra = new NA_APIproductos();
            List<productoComprasDTO> compras = await buscarCompra.get_prodComprasAsync(usuario, password, criterioCodProducto, criterioProveedor);

            if (compras.Count > 0)
            {
                gv_prodCompras.DataSource = compras;
                gv_prodCompras.DataBind();
            }
            else
            {
                gv_prodCompras.DataSource = null;
                gv_prodCompras.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron compras con ese criterio de busqueda.')", true);
            }


        }


        /////////////////////////////////////////////    GET - BUSCAR PRODUCTOS POR CODPRODUCTO

        protected async void btn_BuscarcodProducto_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txt_codProducto.Text.Trim();

            if (string.IsNullOrEmpty(criterioBusqueda)) 
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor ingresa el codigo de un producto valido.');", true);
                return;            
            }
            try
            {
                var apiProd = new NA_APIproductos();
                productoCodigoGet prodCodigo = await apiProd.get_ProductoCodigoAsync("adm", "123", criterioBusqueda);

                if (prodCodigo != null)
                {
                    // encapsulamiento
                    var productos = new List<productoCodigoGet> { prodCodigo };
                    gv_prodCod.DataSource =  productos ;
                    gv_prodCod.DataBind();

                    if (prodCodigo.DetalleUnidadesMedida != null && prodCodigo.DetalleUnidadesMedida.Count > 0)
                    {
                        gv_prodCodDet.DataSource = prodCodigo.DetalleUnidadesMedida;
                        gv_prodCodDet.DataBind();
                    }
                    else
                    {
                        gv_prodCodDet.DataSource = null;
                        gv_prodCodDet.DataBind();
                    }
                }
                else
                {
                    gv_prodCod.DataSource = null;
                    gv_prodCod.DataBind();
                    gv_prodCodDet.DataSource = null;
                    gv_prodCodDet.DataBind();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron productos con ese codigo');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
            
        }

        
    }
}
