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
using static jycboliviaASP.net.Negocio.NA_PruebaAPI;

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

        //  -------------   GET TOKEN

        private async Task<string> GetTokenAsync(string usu, string pass)
        {
            try
            {
                var loginData = new
                {
                    username = usu,
                    password = pass
                };

                var json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);

                // Verificamos si la respuesta fue exitosa
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(result);

                // Verificamos si el Token existe
                if (data?.Resultado?.Token == null)
                {
                    throw new ApplicationException("El token de autenticación no se pudo obtener.");
                }

                return data.Resultado.Token.ToString();
            }
            catch (Exception ex)
            {
                // Lanza una excepción con el detalle del error
                throw new ApplicationException("Error al obtener el token de autenticación", ex);
            }
        }

// ---------------------------------------------    GET BUSCAR PRODUCTO POR CRITERIO
        public class ApiResponseProd
        {
            public bool EsValido { get; set; }
            public List<productoCriterioGet> Resultado { get; set; }
        }
        internal async Task<List<productoCriterioGet>> get_ProductoCriterioAsync(string usu, string pass, string criterio)
        {
            try
            {
                string token = await GetTokenAsync(usu, pass);
                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/productos/buscar/{usu}/{Uri.EscapeDataString(criterio)}";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var searchResponse = await _httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseProd>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<productoCriterioGet>();
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error al buscar productos", ex);
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
        internal async Task<productoCodigoGet> get_ProductoCodigoAsync(string usu, string pass, string criterio)
        {
            try
            {
                string token = await GetTokenAsync(usu, pass);

                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/productos/{Uri.EscapeDataString(criterio)}/{usu}";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var searchResponseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseProdCodigo>(searchResponseBody);

                if (apiResponse == null || !apiResponse.EsValido)
                {
                    throw new ApplicationException("la respuesta de la API no es válida o no se encontraron datos.");
                }
                return apiResponse.Resultado;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener el producto", ex);
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
        internal async Task<List<productoCriterioGet>> get_ProductoVentasCriterioAsync(string usu, string pass, string criterio)
        {
            try
            {
                string token = await GetTokenAsync(usu, pass);
                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/productos/ventas/buscar/{usu}/{Uri.EscapeDataString(criterio)}";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var searchResponse = await _httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseProd>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<productoCriterioGet>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar productos");
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
                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/productos/compras/buscar/{usuario}/{Uri.EscapeDataString(critCodProducto)}?proveedor={Uri.EscapeDataString(critProveedor)}";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var searchResponse = await _httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseProdCompra>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<productoComprasDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar productos", ex);
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


    }
}