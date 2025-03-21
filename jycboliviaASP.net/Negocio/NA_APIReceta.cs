using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace jycboliviaASP.net.Negocio
{
    public class NA_APIReceta
    {
        private static readonly HttpClient _httpClient = new HttpClient();


        public class APIRespondeReceta
        {
            public bool EsValido { get; set; }
            public List<ListRecetaDTO> Resultado { get; set; }
        }
        internal async Task<List<ListRecetaDTO>> Get_ListRecetaAsync(string token)
        {
            try
            {
                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/SincronizarRecetas";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var searchResponseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIRespondeReceta>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<ListRecetaDTO>();
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error  al buscar el almacen", ex);
            }
        }
        public class ListRecetaDTO
        {
            public string CodigoReceta {  get; set; }
            public string CodigoProducto { get; set; }
            public string Descripcion {  get; set; }
        }
    }
}