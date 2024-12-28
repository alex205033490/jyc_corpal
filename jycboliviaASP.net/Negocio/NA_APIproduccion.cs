using Clases.ApiRest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Text;

namespace jycboliviaASP.net.Negocio
{
    internal class NA_APIproduccion
    {
        private static readonly HttpClient httpClient = new HttpClient();
        //private readonly HttpClient _httpClient;

        DBApi api = new DBApi();

        public NA_APIproduccion()
        {
            //_httpClient = new HttpClient();
        }
        //      METODO PARA OBTENER TOKEN
        public async Task<string> GetTokenAsync(string usuario, string password)
        {
            var loginData = new
            {
                username = usuario,
                password = password
            };
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://192.168.11.63/ServcioUponApi/api/v1/auth/login", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(result);

            return data.Resultado.Token.ToString();
        }
/*****************************************************      API POST PRODUCCION ParteProduccion      *****************************************************/

        public class ApiResponse
        {
            public int Resultado { get; set; }
        }

        public async Task<string> PostParteProduccionAsync(ParteProduccionDTO pProduccion, string token )
        {
            var json = JsonConvert.SerializeObject(pProduccion);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync("http://192.168.11.63/ServcioUponApi/api/v1/produccion/parteproduccion", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content?.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);

            var apiresponse = JsonConvert.DeserializeObject<ApiResponse>(result);
            return apiresponse.Resultado.ToString();
        }

        public class ParteProduccionDTO
        {
            public int NumeroParteProduccion { get; set; }
            public DateTime Fecha { get; set; }
            public string Referencia { get; set; }
            public int CodigoResponsable { get; set; }
            public int ItemAnalisis {  get; set; }
            public int LineaProduccion { get; set; }
            public Boolean RealizaDescarga {  get; set; }
            public string Glosa {  get; set; }
            public List<ProductoParteProduccionDTO> Detalle {  get; set; }
            public string Usuario { get; set; }
        }
        public class ProductoParteProduccionDTO
        {
            public int Item { get; set; }
            public string CodigoProducto { get; set; }
            public int Cantidad { get; set; }
            public int UnidadMedida { get; set; }
            public string CodigoReceta { get; set; }
        }

        /*****************************************************      GET PRODUCCION parteProduccion      *****************************************************/

        public class ApiResponseProd
        {
            public bool EsValido { get; set; }
            public ParteProduccionDTO Resultado {  get; set; }
        }

        public async Task<ParteProduccionDTO> GetProduccionAsync(string usuario, string password, string numProduccion)
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

                string url = $"http://192.168.11.63/ServcioUponApi/api/v1/produccion/parteproduccion/{usuario}/{Uri.EscapeDataString(numProduccion)}";

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseProd>(searchResponseBody);

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
    }
}