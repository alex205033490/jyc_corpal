using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using Clases.ApiRest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Windows;



namespace jycboliviaASP.net.Negocio
{
    public class NA_PruebaAPI
    {
        DBApi api = new DBApi();
        public NA_PruebaAPI() { }

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
    }
}
