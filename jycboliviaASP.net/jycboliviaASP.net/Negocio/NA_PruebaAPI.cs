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

        public string pruebaProducto() {
            Persona datoP = new Persona
            {
                Username = "adm",
                Password = "123"
            };

            string json = JsonConvert.SerializeObject(datoP);            
            dynamic respuesta = api.Post("http://192.168.11.62/ServcioUponApi/api/v1/auth/login",json);
            string codVendedor = respuesta.Resultado.CodVendedor.ToString();
            string login = respuesta.Resultado.UserName.ToString();
            //MessageBox.Show(nombre + " " + apellido);
            return "codigoVendedor="+ codVendedor + " y login=" + login;
           // return respuesta.ToString();
        }

        public static void Main()
        {
           NA_PruebaAPI nA_Pru = new NA_PruebaAPI();
            nA_Pru.pruebaProducto();
        }
    }
}
