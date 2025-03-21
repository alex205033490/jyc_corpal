using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    internal class NA_UPONSolicitudPedido
    {

        private static readonly HttpClient httpClient = new HttpClient();
        //--------------------------------  TOKEN
        public class LoginResponse
        {
            public Resultado Resultado { get; set; }
        }
        public class Resultado
        {
            public string Token { get; set; }
        }
        public async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            try
            {
                var loginData = new
                {
                    username = usuario,
                    password = password
                };

                var json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<LoginResponse>(result);

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

        //---------------------------------     POST - PEDIDO
        public class ApiResponsePostPedido
        {
            public int Resultado { get; set; }
        }
        public async Task<string> PostPedidoAsync(PedidoDTO pedido, string token)
        {
            var json = JsonConvert.SerializeObject(pedido);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/pedidos", content);
            if (!response.IsSuccessStatusCode)
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Error response: " + errorResponse);
            }

            response?.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);

            var apiresponse = JsonConvert.DeserializeObject<ApiResponsePostPedido>(result);
            return apiresponse.Resultado.ToString();

        }
        public class ItemPedidoDTO
        {
            public int NumeroItem { get; set; }
            public string CodigoProducto { get; set; }
            public decimal Cantidad { get; set; }
            public int CodigoUnidadMedida { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal ImporteDescuento { get; set; }
            public decimal ImporteTotal { get; set; }
        }
        public class PedidoDTO
        {
            public int NumeroPedido { get; set; }
            public DateTime Fecha { get; set; }
            public string Referencia { get; set; }
            public int CodigoCliente { get; set; }
            public decimal ImporteProductos { get; set; }
            public decimal ImporteDescuentos { get; set; }
            public decimal ImporteTotal { get; set; }
            public string Glosa { get; set; }
            public List<ItemPedidoDTO> DetalleProductos { get; set; }
            public string Usuario { get; set; }
        }
    }
}