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
        private static readonly HttpClient httpClient = new HttpClient();
        //private readonly HttpClient _httpClient;

        //DBApi api = new DBApi();

        public NA_APIproductos()
        {
            //_httpClient = new HttpClient();
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
                    Persona datoP = new Persona
                    {
                        Username = usu,
                        Password = pass
                    };

                    string json = JsonConvert.SerializeObject(datoP);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    string token = loginResponse.Resultado?.Token?.ToString();

                    if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Token is null or empty");
                }    

                    string url = $"http://192.168.11.62/ServcioUponApi/api/v1/productos/buscar/{usu}/{Uri.EscapeDataString(criterio)}";

                    //  configurar el encabezado de autorizacion
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    // realizar la solicitud GET
                    var searchResponse = await httpClient.GetAsync(url);
                    searchResponse.EnsureSuccessStatusCode();

                    var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                    Console.WriteLine("Response Body: " + searchResponseBody);

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponseProd>(searchResponseBody);

                    return apiResponse.EsValido ? apiResponse.Resultado : new List<productoCriterioGet>();
                }
                catch (Exception ex) 
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return new List<productoCriterioGet>();
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


// ---------------------------------------------    GET BUSCAR PRODUCTO/VENTAS POR CRITERIO(producto)
        internal async Task<List<productoCriterioGet>> get_ProductoVentasCriterioAsync(string usu, string pass, string criterio)
        {
            try
            {
                Persona datoP = new Persona
                {
                    Username = usu,
                    Password = pass
                };

                string json = JsonConvert.SerializeObject(datoP);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);
                string token = loginResponse.Resultado.Token.ToString();

                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Token is null or empty");
                }

                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/productos/ventas/buscar/{usu}/{Uri.EscapeDataString(criterio)}";

                //  configurar el encabezado de autorizacion
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // realizar la solicitud GET
                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Response Body: " + searchResponseBody);

                var apiResponse = JsonConvert.DeserializeObject<ApiResponseProd>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<productoCriterioGet>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<productoCriterioGet>();
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
                Persona datoP = new Persona
                {
                    Username = usuario,
                    Password = password
                };

                string json = JsonConvert.SerializeObject(datoP);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);
                string token = loginResponse.Resultado.Token.ToString();

                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Token is null or empty");
                }
                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/productos/compras/buscar/{usuario}/{Uri.EscapeDataString(critCodProducto)}?proveedor={Uri.EscapeDataString(critProveedor)}";

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                //solicitud get
                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Response Body: " + searchResponseBody);

                var apiResponse = JsonConvert.DeserializeObject<ApiResponseProdCompra>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<productoComprasDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<productoComprasDTO>();
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


// ---------------------------------------------    GET BUSCAR PRODUCTO POR CODIGO PRODUCTO

        public class ApiResponseProdCodigo
        {
            public bool EsValido { get; set; }
            public productoCodigoGet Resultado { get; set; }
        }
        public async Task<productoCodigoGet> get_ProductoCodigoAsync(string usu, string pass, string criterio)
        {
            try
            {
                Persona datoP = new Persona
                {
                    Username = usu,
                    Password = pass
                };
                string json = JsonConvert.SerializeObject(datoP);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);
                string token = loginResponse.Resultado.Token.ToString();

                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/productos/{Uri.EscapeDataString(criterio)}/{usu}";

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<ApiResponseProdCodigo>(searchResponseBody);
                if (apiResponse == null || !apiResponse.EsValido)
                {
                    Console.WriteLine("API Response no valido o nulo");
                    return null;
                }
                return apiResponse.Resultado;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        public class productoCodigoDetalleGet
        {
            public int CodigoUnidadMedida { get; set; }
            public string Descripcion { get; set; }
            public string Abreviatura { get; set; }
            public decimal CantidadRelacion{ get; set; }
        }
        
        public class productoCodigoGet
        {
            public string CodigoProducto { get; set; }
            public string Nombre { get; set; }
            public string Descripcion {  get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Stock {  get; set; }
            public decimal DescuentosPermitido { get; set; }
            public int CodigoMoneda { get; set; }
            public Boolean EsFraccionado { get; set; }
            public string Categoria {  get; set; }
            public string Grupo { get; set; }
            public string SubGrupo { get; set; }
            public string Marca { get; set; }
            public int UnidadMedida { get; set; }

            public List<productoCodigoDetalleGet> DetalleUnidadesMedida { get; set; }
        } 
    }
}