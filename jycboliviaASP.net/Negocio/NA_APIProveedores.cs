using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace jycboliviaASP.net.Negocio
{
    public class NA_APIProveedores
    {
        private static readonly HttpClient _httpClient = new HttpClient();

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

                var response = await _httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(result);

                if (data?.Resultado?.Token == null)
                {
                    throw new ApplicationException("El Token de autenticacion no se pudo obtener");
                }
                return data.Resultado.Token.ToString();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener el token de autenticacion", ex);
            }
        }

        public class APIResponseProveedor
        {
            public bool EsValido { get; set; }
            public List<ProveedorDTO> Resultado { get; set; }
        }

        internal async Task<List<ProveedorDTO>> Get_ListProveedorAsync(string token)
        {
            try
            {
                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/sincronizarProveedores";
                
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var searchResponseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponseProveedor>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<ProveedorDTO>();

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar proveedor", ex);
            }
        }
        public class ProveedorDTO
        {
            public int CodigoProveedor { get; set; }
            public string NombreCompleto { get; set; }
        }

    }
}