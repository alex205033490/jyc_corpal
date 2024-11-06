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
        //private readonly HttpClient _httpClient;
        
        //DBApi api = new DBApi();

        public NA_APIclientes()
        {
            //_httpClient = new HttpClient();
        }        
        //--------------------------    METODO PARA OBTENER EL TOKEN    ------------------------//
        public async Task<string> GetTokenAsync(string usuario, string password)
        {
            var loginData = new
            {
                Username = usuario,
                Password = password
            };
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);
            response.EnsureSuccessStatusCode();

            var result  = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(result);

            return data.Resultado.Token.ToString();
        }

        //-------------------------     POST CLIENTES/PERSONAS       -------------------------//
        public async Task<bool> PostPersonaAsync(clientePersonaDTO cliente, string token)
        {
            var json = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            try
            {
                var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/clientes/personas", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en la comunicacion con el servicio", ex);
            }
        }
      
        public class clientePersonaDTO
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

        //--------------------      POST CLIENTES/EMPRESAS       -----------------------//
        public async Task<bool> PostEmpresaAsync(clienteEmpresaDTO empresa, string token)
        {
            var json = JsonConvert.SerializeObject(empresa);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/clientes/empresas", content);
                return response.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error en la comunicación con el servidor", ex);
            }

        }

        public class clienteEmpresaDTO
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


        //----------------------        GET CLIENTES/PERSONAEmpresa        ----------------------//
        public class ApiResponse
        {
            public bool EsValido { get; set; }
            public List<ClienteEmpresaGetDTO> Resultado { get; set; }
        }

        public async Task<List<ClienteEmpresaGetDTO>> get_ClientesPersonasAsync(string token, string criterio)
        {
            try
            {
                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/clientes/buscar/{Uri.EscapeDataString(criterio)}";

                // Configurar el encabezado de autorización
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Realizar la solicitud GET
                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<ClienteEmpresaGetDTO>();
            }
            catch (Exception ex)
            {
                // Manejar errores y registrar información
                Console.WriteLine($"Error en la búsqueda: {ex.Message}");
                return new List<ClienteEmpresaGetDTO>();
            }
        }
        public async Task<string> ObtenerTokenAsync(string usu, string pass)
        {
            try
            {
                var datoP = new Persona
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

                return loginResponse.Resultado?.Token?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de autenticación: {ex.Message}");
                return string.Empty;
            }
        
        }
       
        public class ClienteEmpresaGetDTO
        {
            public int CodigoContacto { get; set; }
            public string NombreCompleto { get;set; }  
            public int CodigoDocumentoIdentidad {  get;set; }
            public string NumeroDocumentoIdentidad { get;set; }
            public string Complemento {  get;set; }
            public string Correo {  get;set; }
            public string Telefono {  get;set; }

        }     
    }
}