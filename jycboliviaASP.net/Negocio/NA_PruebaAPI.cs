using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using Clases.ApiRest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;
using System.Windows;
using System.Windows.Controls;
using System.Data;



namespace jycboliviaASP.net.Negocio
{
    internal class NA_PruebaAPI
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly HttpClient _httpClient;

        DBApi api = new DBApi();
        public NA_PruebaAPI() 
        {
         _httpClient = new HttpClient();
        }

        public string get_personal() {
            Persona datoP = new Persona
            {
                Username = "adm",
                Password = "123"
            };

            string json = JsonConvert.SerializeObject(datoP);            
            dynamic respuesta = api.Post("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", json);
            string codVendedor = respuesta.Resultado.CodigoVendedor.ToString();
            string login = respuesta.Resultado.UserName.ToString();
           // MessageBox.Show(codVendedor + " " + login);
            return "codigoVendedor="+ codVendedor + " y login=" + login;
           // return respuesta.ToString();
        }

        public static void Main()
        {
           NA_PruebaAPI nA_Pru = new NA_PruebaAPI();
            nA_Pru.get_personal();
        }

        internal string get_producto(string usuario, string pass, string producto)
        {
            Persona datoP = new Persona
            {
                Username = usuario,
                Password = pass
            };
            string json = JsonConvert.SerializeObject(datoP);
            dynamic DatosResponsable = api.Post("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", json);
            string Token = DatosResponsable.Resultado.Token.ToString();

            dynamic resultProducto = api.Get_2("http://192.168.11.62/ServcioUponApi/api/v1/productos/buscar//" + usuario + "//" + producto, Token);
            return resultProducto.Resultado[0].Nombre.ToString();
           // return resultProducto.ToString();

        }
//////////////////////////////////////////       INVENTARIO API

        //              GET INVENTARIOS INGRESOS
        public class ApiResponse
        {
            public bool EsValido { get; set; }
            public List<Ingreso> Resultado { get; set; }
        }
        internal List<Ingreso> get_InventarioIngresos(string usuario, string pass, string criterio)
        {
            Persona datoP = new Persona
            {
                Username = usuario,
                Password = pass
            };
            string json = JsonConvert.SerializeObject(datoP);
            dynamic DatosResponsable = api.Post("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", json);
            string Token = DatosResponsable.Resultado.Token.ToString();

            string url = string.IsNullOrWhiteSpace(criterio)
                ? "http://192.168.11.62/ServcioUponApi/api/v1/inventarios/ingresos"
                : $"http://192.168.11.62/ServcioUponApi/api/v1/inventarios/ingresos?criterio={criterio}";
            dynamic resultProducto = api.Get_2(url, Token);
            // Convertir el resultado a una instancia de ApiResponse
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(resultProducto.ToString());

            // Extraer la lista de ingresos
            List<Ingreso> ingresos = apiResponse.Resultado;
            return ingresos;
        }
 
        public class Persona
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public class Ingreso
        {
            public string NumeroTransaccion { get; set; }
            public string Fecha { get; set; }
            public string Referencia { get; set; }
            public string Almacen { get; set; }
            public string Usuario { get; set; }
        }


        // POST INVENTARIOS INGRESOS
        
        // METODO PARA OBTENER EL TOKEN
        public async Task<string> GetTokenAsync (string usuario, string pass)
        {
            var loginData = new
            {
                username = usuario,
                Password = pass
            };
            var json = JsonConvert.SerializeObject(loginData); 
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(result);

            return data.Resultado.Token.ToString();
        }

        //          METODO POST PARA ENVIAR DATOS A TRAVES DE LA API INVENTARIO INGRESOS

        public async Task<string> PostInventarioIngresoAsync(InventarioIngreso ingreso, string token)
        {
            var json = JsonConvert.SerializeObject(ingreso);
            var content = new StringContent(json, Encoding.UTF8 , "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync("http://192.168.11.62/ServcioUponApi/api/v1/inventarios/ingresos", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(result);
            return data.Resultado[0].Nombre.ToString();
        }
        
        public class DetalleProducto
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
            public DateTime Fecha { get; set; }
            public string Referencia { get; set; }
            public int CodigoMoneda { get; set; }
            public int CodigoAlmacen { get; set; }
            public string MotivoMovimiento { get; set; }
            public int ItemAnalisis { get; set; }
            public string Glosa { get; set; }
            public List<DetalleProducto> DetalleProductos { get; set; }
            public string Usuario { get; set; }
        }


        //      METODO GET CONSUMO API INVENTARIO INGRESOS (CON DETALLES)
        public async Task<InventarioIngreso> Get_InventarioIngresosDetalleAsync(string usuario, int criterio, string token)
        {
            Persona datoP = new Persona
            {
                Username = usuario
            };

            var request = new HttpRequestMessage(HttpMethod.Get, $"http://192.168.11.62/ServcioUponApi/api/v1/inventarios/ingresos/{usuario}/{criterio}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<InventarioIngreso>(responseString);
            return result;
        }

        public DataTable ConvertToDataTable(InventarioIngreso inventario)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NumeroIngreso", typeof(int));
            dt.Columns.Add("Fecha", typeof(DateTime));
            dt.Columns.Add("Referencia", typeof(string));
            dt.Columns.Add("CodigoMoneda", typeof(int));
            dt.Columns.Add("CodigoAlmacen", typeof(int));
            dt.Columns.Add("MotivoMovimiento", typeof(string));
            dt.Columns.Add("ItemAnalisis", typeof(int));
            dt.Columns.Add("Glosa", typeof(string));
            dt.Columns.Add("Usuario", typeof(string));

            DataRow row = dt.NewRow();
            row["NumeroIngreso"] = inventario.NumeroIngreso;
            row["Fecha"] = inventario.Fecha;
            row["Referencia"] = inventario.Referencia;
            row["CodigoMoneda"] = inventario.CodigoMoneda;
            row["CodigoAlmacen"] = inventario.CodigoAlmacen;
            row["MotivoMovimiento"] = inventario.MotivoMovimiento;
            row["ItemAnalisis"] = inventario.ItemAnalisis;
            row["Glosa"] = inventario.Glosa;
            row["Usuario"] = inventario.Usuario;
            dt.Rows.Add(row);

            return dt;
        }

    }
}