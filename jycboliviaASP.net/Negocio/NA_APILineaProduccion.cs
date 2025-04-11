using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    public class NA_APILineaProduccion
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public class APIResponseLineaProduccion
        {
            public bool EsValido { get; set; }
            public List<ListLineaProduccionDTO> Resultado { get; set; }
        }
        internal async Task<List<ListLineaProduccionDTO>> Get_ListLineaProduccion(string token)
        {
            try
            {
                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/SincronizarLineaProduccion";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var searchResponseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponseLineaProduccion>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<ListLineaProduccionDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar la linea de producción.", ex);
            }
        }

        public class ListLineaProduccionDTO
        {
            public int CodigoUnidadAnalisis { get; set; }
            public string UnidadAnalisis { get; set; }
            public int CodigoLineaProducion { get; set; }
            public string LineaProducion { get; set;}
            public int CodigoAlmacenMateriaPrima {  get; set; }
            public string AlmacenMateriPrima {  get; set; }


        }
    }
}