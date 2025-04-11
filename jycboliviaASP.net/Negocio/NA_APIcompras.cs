using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Http;
using Clases.ApiRest;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Text;
using System.Diagnostics;

namespace jycboliviaASP.net.Negocio
{
    internal class NA_APIcompras
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        //private readonly HttpClient _httpClient;

        //DBApi api = new DBApi();

        public NA_APIcompras()
        {
            //_httpClient = new HttpClient();
        }
//--------------------------    METODO PARA OBTENER EL TOKEN    ------------------------//
        public async Task<string> ObtenerTokenAsync(string usu, string pass)
        {
            try
            {
                var loginData = new
                {
                    Username = usu,
                    Password = pass
                };
                string json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);
                
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic loginResponse = JsonConvert.DeserializeObject(responseBody);
                
                if (loginResponse?.Resultado?.Token == null)
                {
                    throw new ApplicationException("El token de autenticacion no se pudo obtener");
                }

                return loginResponse.Resultado.Token.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de autenticación: {ex.Message}");
                return string.Empty;
            }
        }

        public class ApiResponse
        {
            public int Resultado { get; set; }
        }
        public async Task <string> PostComprasAsync(DTOCompras compras, string token)
        {
            var json = JsonConvert.SerializeObject(compras);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/compras", content);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                var errorContent= await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Error: {e.Message}, Content: { errorContent}");
                throw;//
            }

            var result = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(result);
                
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
            return apiResponse.Resultado.ToString();

        }
        public class DTOCompras
        {
            public int NumeroCompra { get; set; }
            public string Fecha { get; set; }
            public string Referencia { get; set; }
            public decimal ImporteProductos { get; set; }
            public decimal ImporteDescuento { get; set; }
            public decimal ImporteTotal { get; set; }
            public int CodigoMoneda { get; set; }
            public int CodigoProveedor { get; set; }
            public int CodigoDistribucionGastos { get; set; }
            public PagosDTO Pagos { get; set; }
            public GastosDTO Gastos { get; set; }
            public Boolean FacturaPosterior { get; set; }
            public FacturaDTO Factura { get; set; }
            public string Glosa { get; set; }
            public List<DetalleProductoCompra> DetalleProductos { get; set; }
            public string Usuario { get; set; }
        }
        public class PagosDTO
        {
            public decimal TotalEfectivo { get; set; }
            public decimal TotalCredito { get; set; }
            public decimal TotalCheques { get; set; }
            public decimal TotalDeposito { get; set; }
            public CreditoDTO Credito { get; set; }
            public ChequeDTO Cheque { get; set; }
            public DepositoDTO Deposito { get; set; }
        }
        public class CreditoDTO
        {
            public int TipoCuenta { get; set; }
            public int DiasCredito { get; set; }
        }
        public class ChequeDTO
        {
            public int CodigoBanco { get; set; }
            public string NumeroCuenta { get; set; }
            public int CodigoCheque { get; set; }
            public int NumeroCheque { get; set; }
            public string FechaGiro { get; set; }
        }
        public class DepositoDTO
        {
            public int CodigoBanco { get; set; }
            public string NumeroCuenta { get; set; }
            public string Referencia { get; set; }
        }
        public class GastosDTO
        {
            public int CodigoGasto { get; set; }
            public decimal Importe { get; set; }
            public int CodigoMoneda { get; set; }
            public Boolean AplicaIva { get; set; }
        }
        public class FacturaDTO
        {
            public string NIT_CI { get; set; }
            public string RazonSocial { get; set; }
            public string NumeroFactura { get; set; }
            public string CodigoAutorizacion { get; set; }
            public string CodigoControl { get; set; }
            public decimal ImporteTotal { get; set; }
            public decimal ImporteDescuento { get; set; }
            public decimal ImporteGift { get; set; }
            public decimal ImporteNeto { get; set; }
            public Boolean AplicaCredictoFiscal { get; set; }
        }
        public class DetalleProductoCompra
        {
            public int NumeroItem { get; set; }
            public string CodigoProducto { get; set; }
            public decimal Cantidad { get; set; }
            public int CodigoUnidadMedida { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal ImporteDescuento { get; set; }
            public decimal PorcentajeGasto { get; set; }
            public decimal ImporteTotal { get; set; }
        }
    }

}