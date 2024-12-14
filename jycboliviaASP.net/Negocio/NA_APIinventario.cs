using Clases.ApiRest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using static jycboliviaASP.net.Negocio.NA_PruebaAPI;

using System.Text.Encodings;
using System.Text;
using System.Diagnostics;
using static jycboliviaASP.net.Negocio.NA_APIclientes;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System.Security.Policy;
using System.Net.Http.Headers;
using RestSharp.Serialization.Json;


namespace jycboliviaASP.net.Negocio
{
    internal class NA_APIinventario
    {
        private static readonly HttpClient httpClient = new HttpClient();
        //private readonly HttpClient _httpClient;
        //DBApi api = new DBApi();

        public NA_APIinventario()
        {
            //_httpClient = new HttpClient();
        }

        /////////////////////////   METODO PARA OBTENER TOKEN
        public async Task<string> GetTokenAsync(string usu, string pass)
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

                var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);
            
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(result);

                if (data?.Resultado?.Token == null)
                {
                    throw new ApplicationException("El Token de autenticacion no se pudo obtener");
                }
                return data.Resultado.Token.ToString();
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error al obtener el token de autenticacion", ex);
            }
        }
        /********************************************   API INVENTARIO INGRESOS ********************************************/

        //---------------------- GET - INVENTARIO INGRESO DETALLE
        public class ApiResponseDet
        {
            public bool EsValido { get; set; }
            public InventarioIngreso Resultado { get; set; }
        }
        public async Task<InventarioIngreso> GetInventarioIngresoDetalleAsync(string usuario, string password, string numeroIngreso)
        {
            try
            {
                string token = await GetTokenAsync(usuario, password);

                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/inventarios/ingresos/{usuario}/{Uri.EscapeDataString(numeroIngreso)}";

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var searchResponseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseDet>(searchResponseBody);

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

        //---------------------- GET - INVENTARIOS INGRESOS
        public class ApiResponse
        {
            public bool EsValido { get; set; }
            public List<Ingresos> Resultado { get; set; }
        }

        public async Task<List<Ingresos>> ObtenerIngresosAsync(string usuario, string password, string criterio)
        {
            try
            // Obtener el token de autenticacion
            {
                string token = await GetTokenAsync(usuario, password);
                string url = "http://192.168.11.62/ServcioUponApi/api/v1/inventarios/ingresos";
                if (!string.IsNullOrEmpty(criterio))
                {
                    url += $"?criterio={Uri.EscapeDataString(criterio)}";
                }

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(searchResponseBody);
                
                return apiResponse.EsValido ? apiResponse.Resultado : new List<Ingresos>();


            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar Ingreso", ex);
            }
        }
    
        public class Ingresos
        {
            public string NumeroTransaccion { get; set; }
            public string Fecha { get; set; }
            public string Referencia { get; set; }
            public string Almacen { get; set; }
            public string Usuario { get; set; }
        }

        //---------------------- POST - INVENTARIO INGRESOS
        public class ApiResponse2
        {
            public bool EsValido {  get; set; }
            public List<string> Mensajes {  get; set; }
            public int Resultado { get; set; }           
        }
        public async Task<string> PostInventarioIngresoAsync(InventarioIngreso ingreso, string token)
         {
            try
            {
                var json = JsonConvert.SerializeObject(ingreso);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/inventarios/ingresos", content);
                
                if(!response.IsSuccessStatusCode)
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Error al registrar: {response.StatusCode}, {errorResponse}");
                    return $"Error: {response.StatusCode} - {errorResponse}";
                }

                var result = await response.Content.ReadAsStringAsync();
                var ApiResponse = JsonConvert.DeserializeObject<ApiResponse2>(result);
                return ApiResponse.Resultado.ToString(); 

            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error general: {ex.Message}");
                return $"Error general: {ex.Message}";
            }
        }
        public class ItemIngresoDTO
        {
            public int Item { get; set; }
            public string CodigoProducto { get; set; }
            public int UnidadMedida { get; set; }
            public decimal Cantidad { get; set; }
            public decimal CostoUnitario { get; set; }
            public decimal CostoTotal { get; set; }
        }
        public class InventarioIngreso
        {
            public int NumeroIngreso { get; set; }
            public string Fecha { get; set; }
            public string Referencia { get; set; }
            public int CodigoMoneda { get; set; }
            public int CodigoAlmacen { get; set; }
            public string MotivoMovimiento { get; set; }
            public int ItemAnalisis { get; set; }
            public string Glosa { get; set; }
            public List<ItemIngresoDTO> DetalleProductos { get; set; }
            public string Usuario { get; set; }
        }


        /********************************************   API INVENTARIO EGRESOS  **************************************/

        //----------------     GET - INVENTARIO EGRESOS CON DETALLES
        public class ApiResponseEgre
        {
            public bool EsValido { get; set; }
            public InventarioEgreso Resultado { get; set; }
        }
        public async Task<InventarioEgreso> GetInventarioEgresoDetalleAsync(string usuario, string password, string numIngreso)
        {
            try
            {
                string token = await GetTokenAsync(usuario, password);

                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/inventarios/egresos/{usuario}/{Uri.EscapeDataString(numIngreso)}";

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var searchResponseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseEgre>(searchResponseBody);

                if (apiResponse == null || !apiResponse.EsValido)
                {
                    throw new ApplicationException("La respuesta de la API no es válida o no se encontraron datos.");
                }
                return apiResponse.Resultado;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error al obtener registros con el valor proporcionado.", ex);
            }
        }


        //----------------     GET - INVENTARIOS EGRESOS
        public async Task<List<Ingresos>> ObtenerEgresosAsync(string usuario, string password, string criterio)
        {
            try
            {
                string token = await GetTokenAsync(usuario, password);
                string url = "http://192.168.11.62/ServcioUponApi/api/v1/inventarios/egresos";
                if (!string.IsNullOrEmpty(criterio))
                {
                    url += $"?criterio={Uri.EscapeDataString(criterio)}";
                }

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<Ingresos>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar el registro", ex);
            }
        }


        //----------------     POST - INVENTARIO EGRESOS 
        public async Task<string> PostInventarioEgresoAsync(InventarioEgreso egreso, string token)
        {
            try
            {
                var json = JsonConvert.SerializeObject(egreso);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/inventarios/egresos", content);

                if (!response.IsSuccessStatusCode)
                {
                    // Si hay error en la respuesta, obtenemos el error completo
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Error al registrar: {response.StatusCode}, {errorResponse}");

                    // Aquí solo retornamos el mensaje de error
                    return "Error: " + errorResponse;
                }

                var result = await response.Content.ReadAsStringAsync();
                var apiResponse2 = JsonConvert.DeserializeObject<ApiResponse2>(result);

                // Si la validación no fue exitosa, devolvemos los mensajes de error
                if (!apiResponse2.EsValido)
                {
                    // Concatenamos los mensajes y los retornamos
                    return string.Join(", ", apiResponse2.Mensajes);
                }

                // Si todo es correcto, retornamos el número de egreso
                return apiResponse2.Resultado.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return $"Error general: {ex.Message}";
            }
        }
        public class ApiResponse22
        {
            public bool EsValido { get; set; }
            public List<string> Mensajes { get; set; }
            public string Resultado { get; set; }
        }

        public class ItemEgresoDTO
        {
            public int Item { get; set; }
            public string CodigoProducto { get; set; }
            public int UnidadMedida { get; set; }
            public decimal Cantidad { get; set; }
        }
        public class InventarioEgreso
        {
            public int NumeroEgreso { get; set; }
            public string Fecha { get; set; }
            public string Referencia { get; set; }
            public int CodigoAlmacen { get; set; }
            public string MotivoMovimiento { get; set; }
            public int ItemAnalisis { get; set; }
            public string Glosa { get; set; }
            public List<ItemEgresoDTO> DetalleProductos { get; set; }
            public string Usuario { get; set; }
        }


        /********************************************   API INVENTARIO TRASPASOS    ********************************************/


        /////////////////////////      GET - INVENTARIO TRASPASO

        public class ApiResponseTras
        {
            public bool EsValido { get; set; }
            public List<InvTraspasoDTO> Resultado { get; set; }
        }

        public async Task<List<InvTraspasoDTO>> ObtenerTraspasoAsync(string usuario, string password, string criterio)
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

                // construir la URL de la API
                string url = "http://192.168.11.62/ServcioUponApi/api/v1/inventarios/traspasos";
                if (!string.IsNullOrEmpty(criterio))
                {
                    url += $"?criterio={Uri.EscapeDataString(criterio)}";
                }

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Response Body: " + searchResponseBody);

                var apiResponse = JsonConvert.DeserializeObject<ApiResponseTras>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<InvTraspasoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<InvTraspasoDTO>();
            }
        }

        public class InvTraspasoDTO
        {
            public int NumeroTransaccion {  get; set; }
            public DateTime Fecha {  get; set; }
            public string Referencia {  get; set; }
            public string Almacen {  get; set; }
            public string Usuario { get; set; }
        }

/////////////////////////      GET - INVENTARIO TRASPASO DETALLE

        public class ApiResponseInvTraspasoDet
        {
            public bool EsValido { get; set; }
            public InventarioTraspasoDTO Resultado {  get; set; }
        }

        public async Task<InventarioTraspasoDTO> GetInventarioTraspasoDetAsync(string usuario, string password, string numTraspaso)
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

                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/inventarios/traspasos/{usuario}/{Uri.EscapeDataString(numTraspaso)}";

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<ApiResponseInvTraspasoDet>(searchResponseBody);
                if (apiResponse == null || !apiResponse.EsValido)
                {
                    Console.WriteLine("API response no valido o nulo");
                    return null;
                }
                return apiResponse.Resultado;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex:Message}");
                return null;
            }

        }

        /////////////////////////     POST - INVENTARIO TRASPASOS
        public async Task<string> PostInventarioTraspasoAsync(InventarioTraspasoDTO traspaso, string token)
        {
            var json = JsonConvert.SerializeObject(traspaso);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/inventarios/traspasos", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);

            var apiResponse2 = JsonConvert.DeserializeObject<ApiResponse2>(result);
            return apiResponse2.Resultado.ToString();

        }
        public class DetalleProductoTraspasoDTO
        {
            public int Item { get; set; }
            public string CodigoProducto { get; set; }
            public int UnidadMedida { get; set; }
            public decimal Cantidad { get; set; }
        }        
        public class InventarioTraspasoDTO
        {
            public int NumeroTraspasos { get; set; }
            public DateTime Fecha { get; set; }
            public string Referencia { get; set; }
            public int CodigoAlmacenDestino { get; set; }
            public string Glosa { get; set; }
            public List<DetalleProductoTraspasoDTO> DetalleProductos { get; set; }
            public string Usuario { get; set; }
        }

    }
}


