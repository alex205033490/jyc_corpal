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
                ShowAlert("Por favor, ingrese el nombre del producto.");
                return;
            }
            try
            {
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
                    gv_prodNombre.DataSource = null;
                    gv_prodNombre.DataBind();
                    ShowAlert("No se encontraron registros con el nombre proporcionado.");
                }
            }
            catch(ApplicationException ex)
            {
                ShowAlert($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                ShowAlert($"Ha ocurrido un error inesperado. {ex.Message}");
            }
        }

/////////////////////////////////////////////           GET - BUSCAR VENTAS X PRODUCTO
        protected async void btn_BuscarventProducto_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txt_ventProducto.Text.Trim();
            if (string.IsNullOrEmpty(criterioBusqueda))
            {
                ShowAlert("Por favor, ingresa el nombre de un producto");
                return;
            }

            try
            {
                string usuario = "adm";
                string password = "123";

                var BuscCodProd = new NA_APIproductos();
                List<productoCriterioGet> egresos = await BuscCodProd.get_ProductoVentasCriterioAsync(usuario, password, criterioBusqueda);

                if (egresos.Count > 0)
                {
                    gv_prodVenta.DataSource = egresos;
                    gv_prodVenta.DataBind();
                }
                else
                {
                    gv_prodVenta.DataSource = null;
                    gv_prodVenta.DataBind();
                    ShowAlert("No se encontraron registros del producto proporcionado");
                }
            }
            catch (ApplicationException ex)
            {
                ShowAlert($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                ShowAlert($"Ha ocurrido un error inesperado. {ex.Message}");
            }
        }


        /////////////////////////////////////////////           GET - BUSCAR COMPRAS X PRODUCTO
        protected async void btn_buscarCompras_Click(object sender, EventArgs e)
        {
            string criterioCodProducto = txt_codProductoComp.Text.Trim();
            string criterioProveedor = txt_codProveedorComp.Text.Trim();

            if (string.IsNullOrEmpty(criterioCodProducto))
            {
                ShowAlert("Por favor, ingrese un codigo de producto válido.");
                return;
            }
            if (string.IsNullOrEmpty(criterioProveedor))
            {
                ShowAlert("Por favor, ingrese un codigo proveedor válido.");
                return;
            }

            try
            {
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
                    ShowAlert("No se encontraron compras con ese criterio de busqueda.");
                }
            }
            catch (ApplicationException ex)
            {
                ShowAlert($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                ShowAlert($"Ha ocurrido un error inesperado. {ex.Message}");
            }
        }


        /////////////////////////////////////////////    GET - BUSCAR PRODUCTOS POR CODPRODUCTO

        protected async void btn_BuscarcodProducto_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txt_codProducto.Text.Trim();
            if (string.IsNullOrEmpty(criterioBusqueda)) 
            {
                ShowAlert("Por favor, ingresa el codigo de un producto válido.");
                return;            
            }

            try
            {
                string usuario = "adm";
                string password = "123";


                var apiProd = new NA_APIproductos();
                List<productoCodigoGet> prodCodigo = await apiProd.get_ProductoCodigoAsync(usuario, password, criterioBusqueda);

                if (prodCodigo != null && prodCodigo.Any())
                {
                    // encapsulamiento
                    gv_prodCod.DataSource =  prodCodigo;
                    gv_prodCod.DataBind();

                    if (prodCodigo[0].DetalleUnidadesMedida != null && prodCodigo[0].DetalleUnidadesMedida.Count > 0)
                    {
                        gv_prodCodDet.DataSource = prodCodigo[0].DetalleUnidadesMedida;
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
                    ShowAlert("No se encontraron productos con ese codigo");
                }
            }
            catch (Exception ex)
            {
                ShowAlert($"Error: {ex.Message }");
            }  
        }

        private void ShowAlert(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }
    }
}
