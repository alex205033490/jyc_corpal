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
    internal class NA_APICuentasCobranza
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly HttpClient _httpClient;

        DBApi api = new DBApi();

        public NA_APICuentasCobranza()
        {
            _httpClient = new HttpClient();
        }

        // ----------------------------------- GET - CUENTAS
        public class ApiResponseCuenta
        {
            public bool EsValido { get; set; }
            public List<CuentaDTO> Resultado { get; set; } 
        }

        public async Task<List<CuentaDTO>> ObtenerCobranzaAsync(string usuario, string password)
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
                var response = await httpClient.PostAsync("http://192.168.11.63/ServcioUponApi/api/v1/auth/login", content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);
                string token = loginResponse.Resultado.Token.ToString();

                //construccion de la url
                string url = "http://192.168.11.63/ServcioUponApi/api/v1/cuentas/{usuario}";

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Response Body: " + searchResponseBody);

                var apiResponse = JsonConvert.DeserializeObject<ApiResponseCuenta>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<CuentaDTO>();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<CuentaDTO>();
            }

        }
        public class CuentaDTO
        {
            public int NumeroCuenta { get; set; }
            public int ContactoContacto { get; set; }
            public decimal ImporteTotal { get; set; }
            public decimal ImporteSaldo { get; set; }
            public decimal ImporteVencido { get; set; }
            public int CodigoMoneda { get; set; }
            public int CodigoModulo { get; set; }
            public string Glosa { get; set; }
            public string FechaVencimiento { get; set; }
        }
    }
}

