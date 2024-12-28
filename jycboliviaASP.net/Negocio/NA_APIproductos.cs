using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Http;
using Clases.ApiRest;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Web.UI;


namespace jycboliviaASP.net.Negocio
{
    internal class NA_APIproductos
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        //private readonly HttpClient httpClient;
        //DBApi api = new DBApi();

        public NA_APIproductos()
        {
            //this.httpClient = new HttpClient();
        }

//  --------------------------------------------   GET TOKEN
        public async Task<string> GetTokenAsync(string usu, string pass)
        {
            try
            {
                var datoP = new
                {
                    username = usu,
                    password = pass
                };

                var json = JsonConvert.SerializeObject(datoP);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://192.168.11.63/ServcioUponApi/api/v1/auth/login", content);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<dynamic>(result);

                return data.Resultado?.Token?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener el token de autenticacion: {ex.Message}", ex);
            }
        }


// ---------------------------------------------    GET BUSCAR PRODUCTO POR CRITERIO
        public class ApiResponseProd
        {
            public bool EsValido { get; set; }
            public List<productoCriterioGet> Resultado { get; set; }
        }
        public async Task<List<productoCriterioGet>> get_ProductoCriterioAsync(string token, string criterio)
        {
            try
            {

                string url = $"http://192.168.11.63/ServcioUponApi/api/v1/productos/buscar/adm/{Uri.EscapeDataString(criterio)}";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var searchResponse = await _httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(searchResponseBody);

                var apiResponse = JsonConvert.DeserializeObject<ApiResponseProd>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<productoCriterioGet>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar el producto", ex);
            }
        }
        public class productoCriterioGet
        {
                public string CodigoProducto { get; set; }
                public string Nombre { get; set; }
                public decimal Stock { get; set; }
                public int CodigoUnidadMedida { get; set; }
                public string UnidadMedida { get; set; }
                public string UnidadMedidaAbreviatura { get; set; }
                public decimal PrecioUnitario { get; set; }
                public decimal DescuentosPermitido { get; set; }
                public decimal CostoUnitario {  get; set; }
                //public string UrlImagen { get; set; }
        }


// ---------------------------------------------    GET BUSCAR PRODUCTO POR CODIGO PRODUCTO
        public class ApiResponseProdCodigo
        {
            public bool EsValido { get; set; }
            public productoCodigoGet Resultado { get; set; }
        }
        internal async Task<productoCodigoGet> get_ProductoCodigoAsync(string usuario, string password, string criterio)
        {
            try
            {
                string token = await GetTokenAsync(usuario, password);
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("No se pudo obtener el token.");
                    return null;
                }
                string url = $"http://192.168.11.63/ServcioUponApi/api/v1/productos/{Uri.EscapeDataString(criterio)}/{usuario}";
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var searchResponse = await _httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseProdCodigo>(searchResponseBody);

                if(apiResponse == null || !apiResponse.EsValido)
                {
                    Console.WriteLine("API Response no valido o nulo");
                    return null;
                }
                return apiResponse.Resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ ex.Message}");
                return null;
            }
        }
        public class productoCodigoDetalleGet
        {
            public int CodigoUnidadMedida { get; set; }
            public string Descripcion { get; set; }
            public string Abreviatura { get; set; }
            public decimal CantidadRelacion { get; set; }
        }
        public class productoCodigoGet
        {
            public string CodigoProducto { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Stock { get; set; }
            public decimal DescuentosPermitido { get; set; }
            public int CodigoMoneda { get; set; }
            public Boolean EsFraccionado { get; set; }
            public string Categoria { get; set; }
            public string Grupo { get; set; }
            public string SubGrupo { get; set; }
            public string Marca { get; set; }
            public int UnidadMedida { get; set; }
            public List<productoCodigoDetalleGet> DetalleUnidadesMedida { get; set; }
        }
        
        
        // ---------------------------------------------    GET BUSCAR PRODUCTO/VENTAS POR CRITERIO(producto)
        internal async Task<List<productoCriterioGet>> get_ProductoVentasCriterioAsync(string usuario, string password, string criterio)
        {
            try
            {
                string token = await GetTokenAsync(usuario, password);
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("No se pudo obtener el token.");
                    return null;
                }

                string url = $"http://192.168.11.63/ServcioUponApi/api/v1/productos/ventas/buscar/adm/{Uri.EscapeDataString(criterio)}";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var searchResponse = await _httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseProd>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<productoCriterioGet>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar productos", ex);
            }
        }


// ---------------------------------------------    GET BUSCAR PRODUCTO/COMPRAS
        public class ApiResponseProdCompra
        {
            public bool EsValido { get; set; }
            public List<productoComprasDTO> Resultado { get; set; }
        }
        internal async Task<List<productoComprasDTO>> get_prodComprasAsync(string usuario, string password, string critCodProducto, string critProveedor)
        {
            try
            {
                string token = await GetTokenAsync(usuario, password);
                string url = $"http://192.168.11.63/ServcioUponApi/api/v1/productos/compras/buscar/{usuario}/{Uri.EscapeDataString(critCodProducto)}?proveedor={Uri.EscapeDataString(critProveedor)}";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var searchResponse = await _httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseProdCompra>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<productoComprasDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se encontraron registros con ese criterio de bísqueda.", ex);
            }


        }
        public class productoComprasDTO
        {
            public string CodigoProducto { get; set; }
            public string Nombre { get; set; }
            public decimal Stock { get; set; }
            public int CodigoUnidadMedida { get; set; }
            public string UnidadMedida { get; set; }
            public string UnidadMedidaAbreviatura { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal DescuentosPermitido { get; set; }
            public decimal CostoUnitario { get; set; }
        }


// --------------------------------------------- GET FILTRADO PROVEEDORES

        public class APIResponseListProveedor
        {
            public bool EsValido{ get; set; }
            public List<ListProveedorDTO> Resultado{  get; set; }
        }
        internal async Task<List<ListProveedorDTO>> Get_ListProveedorAsync(string token)
        {
            try
            {
                string url = $"http://192.168.11.63/ServcioUponApi/api/v1/sincronizarProveedores";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var searchResponseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponseListProveedor>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<ListProveedorDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar proveedor", ex);
            }
        }
        public class ListProveedorDTO
        {
            public int CodigoProveedor { get; set; }
            public string NombreCompleto { get; set; }
        }


// --------------------------------------------- VW CATALOGO PRODUCTOS
        public class APIResponseListProductos
        {
            public bool EsValido { get; set; }
            public List<ListProductosDTO> Resultado { get; set; }
        }
        internal async Task<List<ListProductosDTO>> GET_ListProductosAsync(string token)
        {
            try
            {
                string url = $"http://192.168.11.63/ServcioUponApi/api/v1/sincronizarCatalogoProductos";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var searchResponseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Respuesta de la API: {searchResponseBody}");
                var apiResponse = JsonConvert.DeserializeObject<APIResponseListProductos>(searchResponseBody);
                if (!apiResponse.EsValido)
                {
                    throw new ApplicationException("La respuesta de la API no es válida.");
                }

                return apiResponse.EsValido ? apiResponse.Resultado : new List<ListProductosDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar producto: {ex.Message}");
                throw new ApplicationException("Error al buscar producto", ex);
            }
        }
        public class ListProductosDTO
        {
            public string CodigoProducto { get; set; }
            public string Producto { get; set; }
            public string Descripcion { get; set; }
            public int UnidadMedida { get; set; }
            public string DescripcionUnidadMedida { get; set; }
            public string AbreviaturaUnidadMedida { get; set; }
            public Boolean ParaVenta { get; set; }
            // public string UrlImagen
        }

    }
}