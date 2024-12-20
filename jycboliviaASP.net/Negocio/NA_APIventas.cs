using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Http;
using Clases.ApiRest;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Management.Instrumentation;

namespace jycboliviaASP.net.Negocio
{
    internal class NA_APIventas
    {
        private static readonly HttpClient httpClient = new HttpClient();
        //private readonly HttpClient _httpClient;

        //DBApi api = new DBApi();

        /*public NA_APIventas()
        {
            _httpClient = new HttpClient();
        }*/
        //---------------------------- METODO PARA OBTENER TOKEN
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
                    throw new ApplicationException("El token de autenticacion nose pudo obtener");
                }
                return data.Resultado.Token.ToString();
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error al obtener el token de autenticacion", ex);
            }
        }


        //----------------------------GET VER VENTAS
        public class ApiResponseVentas
        {
            public bool EsValido { get; set; }
            public List<VentaDTO> Resultado { get; set; }
        }
        public async Task<List<VentaDTO>> ObtenerVentaAsync(string usuario, string password, string criterio)
        {
            try
            {
                string token = await GetTokenAsync(usuario, password);
                string url = "http://192.168.11.62/ServcioUponApi/api/v1/ventas/{usuario}";
                if (!string.IsNullOrEmpty(criterio))
                {
                    url += $"?criterio={Uri.EscapeDataString(criterio)}";
                }

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var searchResponse = await httpClient.GetAsync(url);
                searchResponse.EnsureSuccessStatusCode();

                var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseVentas>(searchResponseBody);

                return apiResponse.EsValido ? apiResponse.Resultado : new List<VentaDTO>();
            } 
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar Ingreso", ex);
            }
        }
        public class VentaDTO
        {
            public int NumeroVenta { get; set; }
            public DateTime Fecha { get; set; }
            public string Cliente { get; set; }
            public int CodigoCliente { get; set; }
            public decimal ImporteTotal { get; set; }
            public string NumeroFactura { get; set; }
        }


        //----------------------------GET VER VENTAS DETALLE
        public class ApiResponseVentasDet
        {
            public bool EsValido { get; set; }
            public VentaDetDTO Resultado {  get; set; }
        }
        public async Task<VentaDetDTO> GetVentasDetalleAsync(string usu, string pass, string numeroVenta)
        {
            try
            {
                string token = await GetTokenAsync(usu, pass);

                string url = $"http://192.168.11.62/ServcioUponApi/api/v1/pedidos/{Uri.EscapeDataString(numeroVenta)}?usuario={usu}";

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var searchResponseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseVentasDet>(searchResponseBody);

                if(apiResponse == null || !apiResponse.EsValido)
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




        public class VentaDetDTO
        {
            public int NumeroVenta { get; set; }
            public int NumeroPedido { get; set; }
            public string Fecha {  get; set; }
            public int CodigoCliente { get; set; }
            public string Referencia { get; set; }
            public string Glosa {  get; set; }
            public Boolean EmitirFactura { get; set; }
            public decimal ImporteProductos {  get; set; }
            public decimal ImporteDescuentos {  get; set; }
            public decimal ImporteTotal { get; set; }
            public CobrosDTO Cobros {  get; set; }
            public FacturaVentaDTO Factura {  get; set; }
            public List<ItemVentaDTO> DetalleProductos { get; set; }
            public string Usuario {  get; set; }
        }
        public class CobrosDTO
        {
            public decimal TotalEfectivo { get; set; }
            public decimal TotalDeposito {  get; set; }
            public DepositoDTO Deposito {  get; set; }
        }
        public class FacturaVentaDTO
        {
            public int TipoDocumentoIdentidad { get; set; }
            public string NIT_CI {  get; set; }
            public string Complemento {  get; set; }
            public string RazonSocial {  get; set; }
            public string Telefono {  get; set; }
            public string Email {  get; set; }
            public int MetodoPago {  get; set; }
        }
        public class ItemVentaDTO
        {
            public int NumeroItem {  get; set; }
            public string CodigoProducto {  get; set; }
            public decimal Cantidad {  get; set; }
            public int CodigoUnidadMedida {  get; set; }
            public decimal PrecioUnitario {  get; set; }
            public decimal ImporteDescuento {  get; set; }
            public decimal ImporteTotal {  get; set; }
            public int NumeroItemOrigen {  get; set; }
        }
        public class DepositoDTO
        {
            public int CodigoBanco { get; set; }
            public string NumeroCuenta {  get; set; }
            public string Referencia {  get; set; }
        }
    }
}