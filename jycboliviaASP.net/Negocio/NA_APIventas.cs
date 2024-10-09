using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Http;
using Clases.ApiRest;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace jycboliviaASP.net.Negocio
{
    internal class NA_APIventas
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly HttpClient _httpClient;

        DBApi api = new DBApi();

        public NA_APIventas()
        {
            _httpClient = new HttpClient();
        }

        //----------------------------GET VENTAS/{USUARIO}
        public class ApiResponseVentaSimple
        {
            public bool EsValido { get; set; }
            public List<VentaSimpleDTO> Resultado { get; set; }
        }
        public async Task<List<VentaSimpleDTO>> ObtenerVentaSimpleAsync(string usuario, string password)
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

                //Construccion de la URL
                string url = "http://192.168.11.62/ServcioUponApi/api/v1/ventas/{usuario}";

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Response Body: + searchResponseBody");

                var apiResponse = JsonConvert.DeserializeObject<ApiResponseVentaSimple>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<VentaSimpleDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<VentaSimpleDTO>();
            }
        }
        public class VentaSimpleDTO
        {
            public int NumeroVenta { get; set; }
            public DateTime Fecha { get; set; }
            public string Cliente { get; set; }
            public int CodigoCliente { get; set; }
            public decimal ImporteTotal { get; set; }
            public string NumeroFactura { get; set; }

        }
    }
}