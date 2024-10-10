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

namespace jycboliviaASP.net.Negocio
{
    internal class NA_APIcompras
    {

        private static readonly HttpClient httpClient = new HttpClient();
        private readonly HttpClient _httpClient;

        DBApi api = new DBApi();

        public NA_APIcompras()
        {
            _httpClient = new HttpClient();
        }
//////////      METODO PARA OBTENER TOKEN
        
        public async Task<string> GetTokenAsync (string usu, string pass)
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

            return data.Resultado.Token.ToString();
        }




        public async Task <string> PostComprasAsync(DTOCOMPRAS compras, string token)
        {
            var json = JsonConvert.SerializeObject(compras);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/compras", content);
            response.EnsureSuccessStatusCode();

            var result= await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(result);
            return data.Resultado[0].Nombre.ToString();
        }
        public class DTOCOMPRAS
        {
            public int NumeroCompra { get; set; }
            public DateTime Fecha { get; set; }
            public string Referencia { get; set; }
            public decimal ImporteProductos { get; set; }
            public decimal ImporteDescuento { get; set; }
            public decimal ImporteTotal { get; set; }
            public int CodigoMoneda { get; set; }
            public int CodigoProveedor { get; set; }
            public int CodigoDistribucionGastos { get; set; }
            public Pagos Pagos { get; set; }
            public List<Gastos> Gastos { get; set; }
            public bool FacturaPosterior { get; set; }
            public Factura Factura { get; set; }
            public string Glosa { get; set; }
            public List<DetalleProducto> DetalleProductos { get; set; }
            public string Usuario { get; set; }
        }
        public class Pagos
        {
            public decimal TotalEfectivo { get; set; }
            public decimal TotalCredito { get; set; }
            public decimal TotalCheques { get; set; }
            public decimal TotalDeposito { get; set; }
            public Credito Credito { get; set; }
            public Cheque Cheque { get; set; }
            public Deposito Deposito { get; set; }
        }
        public class Credito
        {
            public int TipoCuenta { get; set; }
            public int DiasCredito { get; set; }
        }
        public class Cheque
        {
            public int CodigoBanco { get; set; }
            public string NumeroCuenta { get; set; }
            public int CodigoCheque { get; set; }
            public int NumeroCheque { get; set; }
            public DateTime FechaGiro { get; set; }
        }
        public class Deposito
        {
            public int CodigoBanco { get; set; }
            public string NumeroCuenta { get; set; }
            public string Referencia { get; set; }
        }
        public class Gastos
        {
            public int CodigoGasto { get; set; }
            public decimal Importe { get; set; }
            public int CodigoMoneda { get; set; }
            public Boolean AplicaIva { get; set; }
        }
        public class Factura
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
        public class DetalleProducto
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