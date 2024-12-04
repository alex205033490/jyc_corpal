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
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;



namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIProductos : System.Web.UI.Page
    {
        private readonly NA_APIproductos _na_apiproductos = new NA_APIproductos();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
////////////////////////////////////////////      GET - BUSCAR PRODUCTO POR NOMBRE
        protected async void btn_buscarProdNombre_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txt_nomProducto.Text.Trim();

            await BuscarProdCriterioAsync(criterioBusqueda);
        }
        // Metodo buscar productos por criterio
        public async Task BuscarProdCriterioAsync(string criterioBusqueda)
        {
            if (string.IsNullOrEmpty(criterioBusqueda))
            {
                gv_prodNombre.DataSource = null;
                gv_prodNombre.DataBind();
                ShowAlert("Por favor, ingrese el nombre del producto.");
                return;
            }
            try
            {
                string token = await ObtenerTokenAsync("adm", "123");

                if (string.IsNullOrEmpty(token))
                {
                    ShowAlert("Error de autenticación. No se pudo obtener el token");
                    return;
                }
                var APIproducto = new NA_APIproductos();
                List<productoCriterioGet> productoDTO = await APIproducto.get_ProductoCriterioAsync(token, criterioBusqueda);

                if (productoDTO != null && productoDTO.Count > 0)
                {
                    MostrarProductoCriterio(productoDTO);
                }
                else
                {
                    gv_prodNombre.DataSource = null;
                    gv_prodNombre.DataBind();
                    ShowAlert("No se encontraron registros con el nombre proporcionado.");
                }
            }
            catch (Exception ex)
            {
                ShowAlert($"Ha ocurrido un error inesperado. {ex.Message}");
            }
        }

        private void MostrarProductoCriterio(List<productoCriterioGet> productoDTO)
        {
            gv_prodNombre.DataSource = productoDTO;
            gv_prodNombre.DataBind();
        }

/////////////////////////////////////////////           GET - BUSCAR VENTAS X PRODUCTO
        protected async void btn_BuscarventProducto_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txt_ventProducto.Text.Trim();
            await BuscarProductoVentaAsync(criterioBusqueda);   
        }
        public async Task BuscarProductoVentaAsync(string criterioBusqueda)
        {
            
            if (string.IsNullOrEmpty(criterioBusqueda))
            {
                ShowAlert("Por favor, ingresa el nombre de un producto");
                gv_prodVenta.DataSource = null;
                gv_prodVenta.DataBind();
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

            await BuscarProductoCompraAsync(criterioCodProducto, criterioProveedor);
        }
        public async Task BuscarProductoCompraAsync(string criterioCodProducto2, string criterioProveedor2)
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
                List<productoComprasDTO> compras = await buscarCompra.get_prodComprasAsync(usuario, password, criterioCodProducto2, criterioProveedor2);

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


////////////////////////////////////////////    GET - BUSCAR PRODUCTOS POR CODPRODUCTO
        protected async void btn_BuscarcodProducto_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txt_codProducto.Text.Trim();
             
            await BuscarProdCodigoAsync(criterioBusqueda);
        }
        // metodo buscar producto por codigo
        public async Task BuscarProdCodigoAsync(string criterioBusqueda)
        {
            if (string.IsNullOrEmpty(criterioBusqueda))
            {
                ShowAlert("Por favor, ingresa el código de un producto válido.");
                return;
            }

            try
            {
                var apiProd = new NA_APIproductos();
                var prodCodigo = await apiProd.get_ProductoCodigoAsync("adm", "123", criterioBusqueda);

                if (prodCodigo != null)
                {
                    // Encapsulamiento de los productos encontrados
                    var productos = new List<productoCodigoGet> { prodCodigo };
                    gv_prodCod.DataSource = productos;
                    gv_prodCod.DataBind();

                    // Verifica y muestra detalles de unidades de medida
                    if (prodCodigo.DetalleUnidadesMedida != null && prodCodigo.DetalleUnidadesMedida.Count > 0)
                    {
                        gv_prodCodDet.DataSource = prodCodigo.DetalleUnidadesMedida; 
                        gv_prodCodDet.DataBind();
                    }
                }
                else
                {
                    gv_prodCod.DataSource = null;
                    gv_prodCod.DataBind();
                    gv_prodCodDet.DataSource = null;
                    gv_prodCodDet.DataBind();
                    ShowAlert("No se encontraron productos con ese código.");
                }
            }
            catch (Exception ex)
            {
                ShowAlert($"Error: {ex.Message}");
            }

        }

        private void MostrarCodProducto(List<productoCodigoGet> productos)
        {

            gv_prodCod.DataSource = productos;
            gv_prodCod.DataBind();
            
            if(productos.Count > 0 && productos[0].DetalleUnidadesMedida != null && productos[0].DetalleUnidadesMedida.Count> 0)
            {
                gv_prodCodDet.DataSource = productos[0].DetalleUnidadesMedida;
                gv_prodCodDet.DataBind();
            }
            else
            {
                gv_prodCodDet.DataSource = null;
                gv_prodCodDet.DataBind();
            }
        }
        private void limpiarGrids()
        {
            gv_prodCod.DataSource = null;
            gv_prodCod.DataBind();
            gv_prodCodDet.DataSource = null;
            gv_prodCodDet.DataBind();
        }

        [WebMethod]
        [ScriptMethod]
        public string[] GetListProductosXCod(string prefixText, int count)
        {
            string token = ObtenerTokenAsync("adm","123").Result; 

            NA_APIproductos apip = new NA_APIproductos();

            var productos = apip.GET_ListProductosAsync(token).Result;

            var productosFiltrados = productos
                .Where(p => p.CodigoProducto.StartsWith(prefixText, StringComparison.InvariantCultureIgnoreCase))
                .Take(count)
                .Select(p => p.CodigoProducto)
                .ToArray();

            return productosFiltrados;
        }


        private void ShowAlert(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }
        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            NA_APIproductos prod = new NA_APIproductos();
            return await prod.GetTokenAsync(usuario, password);
        }

        protected async void Unnamed1_Click(object sender, EventArgs e)
        {
            /*try
            {
                string usuario = "adm";
                string password = "123";

                string token = await ObtenerTokenAsync(usuario, password);

                List<ListProductosDTO> productos = await 
            }
            */
        }
    }
}
