using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Clases.ApiRest;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using static jycboliviaASP.net.Negocio.NA_PruebaAPI;
using RestSharp;

namespace jycboliviaASP.net.Negocio
{
    internal class NA_APIclientes
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly HttpClient _httpClient;
        
        DBApi api = new DBApi();

        public NA_APIclientes()
        {
            _httpClient = new HttpClient();
        }

        public string get_personal()
        {
        Persona datoP = new Persona
        {
            Username = "adm",
            Password = "123"
        };
        string json = JsonConvert.SerializeObject(datoP);
        dynamic respuesta = api.Post("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", json);
        string codVendedor = respuesta.Resultado.CodigoVendedor.ToString(); 
        string login = respuesta.Resultado.UserName.ToString();

        return "codigoVendedor="+codVendedor + " y login=" + login;
        }
        
        public static void Main()
        {
            NA_APIclientes Na_Cli = new NA_APIclientes();
            Na_Cli.get_personal();
        }
        //////////////      METODO PARA OBTENER EL TOKEN

        public async Task<string> GetTokenAsync(string usuario, string password)
        {
            var loginData = new
            {
                Username = usuario,
                Password = password
            };
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);
            response.EnsureSuccessStatusCode();

            var result  = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(result);

            return data.Resultado.Token.ToString();

        }

        //////////////      METODO POST CLIENTES/PERSONAS
        public async Task<bool> PostClientesPersonasAsync(clientePersona cliente, string token)
        {
            var json = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
               
            var response = await _httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/clientes/personas", content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }
      
        public class clientePersona
        {
            public int CodigoContacto { get; set; }
            public string Nombres { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string ApellidoCasado { get; set; }
            public int TipoDocumentoIdentidad { get; set; }
            public string NumeroDocumento { get; set; }
            public string Complemento { get; set; }
            public string Telefono { get; set; }
            public string Correo { get; set; }
            public string Usuario { get; set; }
        }

        //////////////      METODO POST CLIENTES/EMPRESAS
        public async Task<string> PostClientesEmpresasAsync(clienteEmpresa empresa, string token)
        {
            var json = JsonConvert.SerializeObject(empresa);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/clientes/personas", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(result);
            return data.Resultado[0].Nombre.ToString();
        }

        public class clienteEmpresa
        {
            public int CodigoContacto { get; set; }
            public string NombreLegal { get; set; }
            public string NombreComercial { get; set; }
            public string Direccion { get; set; }
            public string Telefono { get; set; }
            public string NIT { get; set; }
            public string Correo { get; set; }
            public Boolean EsSucursal { get; set; }
            public string Usuario { get; set; }
        }


        //////////////      METODO GET CLIENTES/PERSONAS
        public class ApiResponse
        {
            public bool EsValido { get; set; }
            public List<clientePersonaGet> Resultado { get; set; }
        }

        //tengo algun error en mi codigo solo me muestra la 1ra columna 
        internal async Task<List<clientePersonaGet>> get_ClientesPersonasAsync(string usu, string pass, string criterio)
        {
            try
            {
                // Obtener el token de autenticación
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

                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/clientes/buscar/{Uri.EscapeDataString(criterio)}";

                // Configurar el encabezado de autorización
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Realizar la solicitud GET
                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Response Body: " + searchResponseBody);

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(searchResponseBody);
                
                return apiResponse.EsValido ? apiResponse.Resultado : new List<clientePersonaGet>();
            }
            catch (Exception ex)
            {
                // Manejar errores y registrar información
                Console.WriteLine($"Error: {ex.Message}");
                return new List<clientePersonaGet>();
            }
        }
        public class clientePersonaGet
        {
            public int CodigoContacto { get; set; }
            public string NombreCompleto { get;set; }  
            public int CodigoDocumentoIdentidad {  get;set; }
            public string NumeroDocumentoIdentidad { get;set; }

        }
    }
}