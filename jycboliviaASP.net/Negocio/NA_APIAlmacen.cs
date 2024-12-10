using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    public class NA_APIAlmacen
    {
        private static readonly HttpClient _httpClient = new HttpClient();


        public class APIResponseAlmacen
        {
            public bool EsValido { get; set; }
            public List<ListAlmacenesDTO> Resultado { get; set; }
        }
        internal async Task<List<ListAlmacenesDTO>> Get_ListAlmacenAsync(string token)
        {
            try
            {
                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/sincronizarAlmacenes";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var searchResponseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponseAlmacen>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<ListAlmacenesDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar el almacen", ex);
            }
        }
        public class ListAlmacenesDTO
        {
            public int CodigoAlmacen { get; set; }
            public string Nombre { get; set; }
            public int CodigoSucursal { get; set; }
        }


    }
}