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
using System.Net.Http;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using Newtonsoft.Json.Linq;


namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIProductos : System.Web.UI.Page
    {
        private static readonly HttpClient _httpClient = new HttpClient();


        private readonly NA_APIproductos _na_apiproductos = new NA_APIproductos();
        protected async void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string token = await ObtenerTokenAsync("adm", "123");
                var productosNom = await ObtenerListNomProd(token);
                var productosCod = await ObtenerListCodProd(token);
                var proveedor = await ObtenerListCodProveedor(token);

                // dd 1
                ddListCodProductos.DataSource = productosCod;
                ddListCodProductos.DataTextField = "CodigoProducto";
                ddListCodProductos.DataValueField = "CodigoProducto";
                ddListCodProductos.DataBind();
                ddListCodProductos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un codigo", ""));
                
                // dd 2
                ddListCodProductos2.DataSource = productosNom;
                ddListCodProductos2.DataTextField = "Producto";
                ddListCodProductos2.DataValueField = "CodigoProducto";
                ddListCodProductos2.DataBind();
                ddListCodProductos2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona un producto", ""));

                // dd 3
                ddListCodProveedor.DataSource = proveedor;
                ddListCodProveedor.DataTextField = "NombreCompleto";
                ddListCodProveedor.DataValueField = "CodigoProveedor";
                ddListCodProveedor.DataBind();
                ddListCodProveedor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona un proveedor", ""));
            }
        }


////////////////////////////////////////////        GET - BUSCAR PRODUCTO POR NOMBRE
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

                var APIproducto = new NA_APIproductos();
                List<productoCriterioGet> productoDTO = await APIproducto.get_ProductoCriterioAsync(token, criterioBusqueda);

                if (productoDTO != null && productoDTO.Count > 0)
                {
                    MostrarProductoCriterio(productoDTO);
                    gv_prodNombre.Visible = true;
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


////////////////////////////////////////////        GET - BUSCAR PRODUCTOS POR CODPRODUCTO

        //-- Mostrar Lista de Productos Nombre Asc
        private async Task<List<ListProductosDTO>> ObtenerListNomProd(string token)
        {
            try
            {
                var productos = await _na_apiproductos.GET_ListProductosAsync(token);
                return productos.OrderBy(p => p.Producto).ToList();
            }
            catch (Exception ex)
            {
                ShowAlert($"Error al obtener los productos: {ex.Message}");
                return null;
            }
        }
        //-- Mostrar Lista de Productos Codigo Asc
        private async Task<List<ListProductosDTO>> ObtenerListCodProd(string token)
        {
            try
            {
                var productos = await _na_apiproductos.GET_ListProductosAsync(token);
                return productos.OrderBy(p => p.CodigoProducto).ToList();
            }
            catch (Exception ex)
            {
                ShowAlert($"Error al obtener los productos: {ex.Message}");
                return null;
            }
        }


        //-- btn buscar producto por codigo
        protected async void btn_BuscarcodProducto_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = ddListCodProductos.SelectedValue.Trim();

            if (string.IsNullOrEmpty(criterioBusqueda))
            {
                ShowAlert("Por favor, ingresa el Código de un producto válido.");
                return;
            }
            await BuscarProdCodigoAsync(criterioBusqueda);
        }
        // metodo buscar producto por codigo
        public async Task BuscarProdCodigoAsync(string criterioBusqueda)
        {
            try
            {
                string token = await ObtenerTokenAsync("adm", "123");

                var apiProd = new NA_APIproductos();
                var prodCodigo = await apiProd.get_ProductoCodigoAsync("adm", "123", criterioBusqueda);

                if (prodCodigo != null)
                {
                    MostrarProductos(prodCodigo);

                    MostrarDetallesProducto(prodCodigo.DetalleUnidadesMedida);
                }
                else
                {
                    limpiarGridsCodProducto();
                    ShowAlert("No se encontraron productos con ese código.");
                }
            }
            catch (Exception ex)
            {
                ShowAlert($"Error: {ex.Message}");
            }

        }
        private void MostrarProductos(productoCodigoGet prodCodigo)
        {
            var productos = new List<productoCodigoGet> { prodCodigo };
            gv_prodCod.DataSource = productos;
            gv_prodCod.DataBind();
        }
        private void MostrarDetallesProducto(List<productoCodigoDetalleGet> prodCodigoDet)
        {
            if (prodCodigoDet != null)
            {
                gv_prodCodDet.DataSource = prodCodigoDet;
                gv_prodCodDet.DataBind();
            } 
            else
            {
                gv_prodCodDet.DataSource = null;
                gv_prodCodDet.DataBind();

                gv_prodCod.DataSource = null;
                gv_prodCod.DataBind();
            }
        }
        private void limpiarGridsCodProducto()
        {
            gv_prodCod.DataSource = null;
            gv_prodCod.DataBind();
            gv_prodCodDet.DataSource = null;
            gv_prodCodDet.DataBind();
        }


//////////////////////////////////////////////      GET - BUSCAR VENTAS X PRODUCTO
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

                string token = await ObtenerTokenAsync("adm", "123");
                
                var BuscCodProd = new NA_APIproductos();
                List<productoCriterioGet> egresos = await BuscCodProd.get_ProductoVentasCriterioAsync(token, criterioBusqueda);

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


/////////////////////////////////////////////       GET - BUSCAR COMPRAS X PRODUCTO
        protected async void btn_buscarCompras_Click(object sender, EventArgs e)
        {
            string criterioCodProducto = ddListCodProductos2.SelectedValue;
            string criterioProveedor = ddListCodProveedor.SelectedValue;

            await BuscarProductoCompraAsync(criterioCodProducto, criterioProveedor);
        }
        public async Task BuscarProductoCompraAsync(string criterioCodProducto2, string criterioProveedor2)
        {
            string criterioCodProducto = ddListCodProductos2.SelectedValue.Trim();
            string criterioProveedor = ddListCodProveedor.SelectedValue.Trim();

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

        //-- obtener lista de proveedores 
        private async Task<List<ListProveedorDTO>> ObtenerListCodProveedor(string token)
        {
            try
            {
                var productos = await _na_apiproductos.Get_ListProveedorAsync(token);
                return productos.OrderBy(p => p.NombreCompleto).ToList();
            }
            catch(Exception ex)
            {
                ShowAlert($"Error al obtener los proveedores: {ex.Message}");
                return null;
            }
        }





////////        LISTA PRODUCTOS AUTOC
        [WebMethod]
        [ScriptMethod]
        public async Task<List<string>> GetProductosAsync(string prefixText, int count)
        {
            List<string> productos = new List<string>();

            try
            {
                string token = await ObtenerTokenAsync("adm", "123");

                NA_APIproductos pp = new NA_APIproductos();

                var productosDTO = await pp.GET_ListProductosAsync(token);

                productos = productosDTO
                    .Where(p => p.Producto.ToLower().Contains(prefixText.ToLower()))
                    .Select(p => p.Producto)
                    .Take(count)
                    .ToList();
            }
            catch (Exception ex)
            {
                ShowAlert($"Error: {ex.Message}");
            }
            return productos;
        }


/////////////////////////////////////////////// Otros
        private void ShowAlert(string message)
        {
            string script = $"alert('{message.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }
        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            NA_APIproductos prod = new NA_APIproductos();
            return await prod.GetTokenAsync(usuario, password);
        }
    }
}
