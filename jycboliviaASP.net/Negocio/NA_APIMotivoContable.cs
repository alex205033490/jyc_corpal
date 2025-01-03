using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    public class NA_APIMotivoContable
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public class APIResponseMotMovI
        {
            public bool EsValido { get; set; }
            public List<ListMotMovIDTO> Resultado { get; set; }
        }
        internal async Task<List<ListMotMovIDTO>> Get_ListMotMovIAsync(string token)
        {
            try
            {
                string url = $"http://192.168.11.63/ServcioUponApi/api/v1/SincronizarMotivosContableIngresos";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var searchResponseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponseMotMovI>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<ListMotMovIDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar el Motivo", ex);
            }
        }
        public class ListMotMovIDTO
        {
            public string CodigoMotivo { get; set; }
            public string MotivoContable { get; set; }

        }


        internal async Task<List<ListMotMovIDTO>> Get_ListMotMovEAsync(string token)
        {
            try
            {
                string url = $"http://192.168.11.63/ServcioUponApi/api/v1/SincronizarMotivosContableEgresos";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var searchResponseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponseMotMovI>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<ListMotMovIDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar el Motivo", ex);
            }
        }








    }
}