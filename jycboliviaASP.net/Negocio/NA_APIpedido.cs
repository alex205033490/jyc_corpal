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
            var loginData = new
            {
                username = usuario,
                password = pass
             
            };

            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(result);

            return data.Resultado.Token.ToString();
        }

        //-------------------------------------     GET - PEDIDO CON CRITERIO

        public class ApiResponsePedCrit
        {
            public bool EsValido { get; set; }
            public PedidoDTO Resultado { get; set; }
        }

        public async Task<PedidoDTO> ObtenerPedidoCriterioAsync(string usuario, string password, string numeroPedido)
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

                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/pedidos/{Uri.EscapeDataString(numeroPedido)}?usuario={usuario}";

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<ApiResponsePedCrit>(searchResponseBody);
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

//-------------------------------------     GET - PEDIDO
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

                // URL API
                string url = "http://192.168.11.62/ServcioUponApi/api/v1/pedidos/{usuario}";
                if (!string.IsNullOrEmpty(criterio))
                {
                    url += $"?criterio={Uri.EscapeDataString(criterio)}";
                }
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Response Body: " + searchResponseBody);

                var apiResponse = JsonConvert.DeserializeObject<ApiResponsePedido>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<pedidoDTO2>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<pedidoDTO2>();
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
            public int Resultado { get; set; }
        }
        public async Task<string> PostPedidoAsync(PedidoDTO pedido, string token)
        {
          var json = JsonConvert.SerializeObject(pedido);
          var content = new StringContent(json, Encoding.UTF8, "application/json");
          httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
          
          var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/pedidos", content);
            response.EnsureSuccessStatusCode();
          
            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);

            var apiresponse = JsonConvert.DeserializeObject<ApiResponsePostPed>(result);
            return apiresponse.Resultado.ToString();


          
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
          public DateTime Fecha { get; set; }
          public string Referencia { get; set; }
          public int CodigoCliente { get; set; }
          public decimal ImporteProductos { get; set; }
          public decimal ImporteDescuentos { get; set; }
          public decimal ImporteTotal { get; set; }
          public string Glosa { get; set; }
          public List<ItemPedidoDTO> DetalleProductos { get; set; }
          public string Usuario { get; set; }
        }
        
    }
}