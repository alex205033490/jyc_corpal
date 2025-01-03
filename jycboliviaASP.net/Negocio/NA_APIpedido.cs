using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

using Clases.ApiRest;
using Newtonsoft.Json;
using System.Text;
using jycboliviaASP.net.Presentacion;
using System.Diagnostics;

namespace jycboliviaASP.net.Negocio
{
    internal class NA_APIpedido
    {
        private static readonly HttpClient httpClient = new HttpClient();
        //private readonly HttpClient _httpClient;
        //DBApi api = new DBApi();

        public NA_APIpedido()
        {
            //_httpClient = new HttpClient();
        }
        //-------------------------------------     METODO PARA OBTENER TOKEN
        public async Task<string> GetTokenAsync(string usuario, string pass)
        {
            try
            {
                var loginData = new
                {
                    username = usuario,
                    password = pass
                };

                var json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("http://192.168.11.63/ServcioUponApi/api/v1/auth/login", content);
            
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(result);

                if (data?.Resultado?.Token == null)
                {
                    throw new ApplicationException("El token de autenticacion no se pudo obtener");
                }
                return data.Resultado.Token.ToString();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener el token de autenticación", ex);
            }
        }

        //-------------------------------------     GET - PEDIDO CON CRITERIO

        public class ApiResponsePedCrit
        {
            public bool EsValido { get; set; }
            public PedidoDTO Resultado { get; set; }
        }

        public async Task<PedidoDTO> ObtenerPedidoCriterioAsync(string usuario, string password, string criterio)
        {
            try
            {
                string token = await GetTokenAsync(usuario, password);

                string url = $"http://192.168.11.63/ServcioUponApi/api/v1/pedidos/{Uri.EscapeDataString(criterio)}?usuario={usuario}";

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var SearchResponseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponsePedCrit>(SearchResponseBody);

                if (apiResponse == null || !apiResponse.EsValido)
                {
                    throw new ApplicationException("La respuesta de la API no es válida o no se encontraron datos.");
                }
                return apiResponse.Resultado;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener registros con el valor proporcionado.", ex);
            }
        }

        //-------------------------------------     GET - PEDIDO C/S CRITERIO
        public class ApiResponsePedido
        {
            public bool EsValido { get; set; }
            public List<pedidoDTO2> Resultado { get; set; }
        }
        public async Task<List<pedidoDTO2>> ObtenerPedidoAsync(string usuario, string password, string criterio)
        {
            try
            //Obtener token de authenticacion
            {
                string token = await GetTokenAsync(usuario, password);
                string url = "http://192.168.11.63/ServcioUponApi/api/v1/pedidos/{usuario}";
                if (!string.IsNullOrEmpty(criterio))
                {
                    url += $"?criterio={Uri.EscapeDataString(criterio)}";
                }
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponsePedido>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<pedidoDTO2>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar pedido", ex);
            }
        }
        public class pedidoDTO2
        {
            public int NumeroPedido { get; set; }
            public string Fecha { get; set; }
            public string Cliente { get; set; }
            public int CodigoCliente { get; set; }
            public decimal ImporteTotal { get; set; }
        }


        //-------------------------------------     POST - PEDIDO
        public class ApiResponsePostPed
        {
            public bool EsVliado {  get; set; }
            public List<string> Mensajes {  get; set; }
            public int Resultado { get; set; }
        }
        public async Task<string> PostPedidoAsync(PedidoDTO pedido, string token)
        {
            try
            {
                var json = JsonConvert.SerializeObject(pedido);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.PostAsync("http://192.168.11.63/ServcioUponApi/api/v1/pedidos", content);

                if(!response.IsSuccessStatusCode)
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Error al registrar: {response.StatusCode}, {errorResponse}");
                    return $"Error: {response.StatusCode} - {errorResponse}";
                }

                var result = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponsePostPed>(result);
                return apiResponse.Resultado.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error general: {ex.Message}");
                return $"Error general: {ex.Message}";
            }
        }
        public class ItemPedidoDTO
        {
          public int NumeroItem { get; set; }
          public string CodigoProducto { get; set; }
          public decimal Cantidad { get; set; }
          public int CodigoUnidadMedida { get; set; }
          public decimal PrecioUnitario { get; set; }
          public decimal ImporteDescuento { get; set; }
          public decimal ImporteTotal { get; set; }
        }        
        public class PedidoDTO
        {
          public int NumeroPedido { get; set; }
          public string Fecha { get; set; } 
          public string Referencia { get; set; } //op
          public int CodigoCliente { get; set; }
          public decimal ImporteProductos { get; set; } 
          public decimal ImporteDescuentos { get; set; }
          public decimal ImporteTotal { get; set; }
          public string Glosa { get; set; } //op
          public List<ItemPedidoDTO> DetalleProductos { get; set; }
          public string Usuario { get; set; }
        }
        
    }
}