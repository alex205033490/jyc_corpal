using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using Clases.ApiRest;
using jycboliviaASP.net.Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.NegocioApi
{
    public class NA_endpoints
    {
        DBApi api = new DBApi();
        public NA_endpoints() { }

        public string get_TokenUsuario(string usuario , string password ) {
         //   NA_Responsables nres = new NA_Responsables();
         //   DataSet datosUpon = nres.get_datosUpon(usuario, password);
            string usuarioUpon = "adm";
            string passwordUpon = "123";
        /*
            if (datosUpon.Tables[1].Rows.Count > 0)
            {
                usuarioUpon = datosUpon.Tables[1].Rows[0][0].ToString();
                passwordUpon = datosUpon.Tables[1].Rows[0][1].ToString();
            }
          */              
            Persona datoP = new Persona
            {
                Username = usuarioUpon,
                Password = passwordUpon
            };

            string json = JsonConvert.SerializeObject(datoP);
            dynamic respuesta = api.Post("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", json);
            string token = respuesta.Resultado.Token.ToString();
            return token;
        }

        public int get_codigoUsuarioVendedor(string usuario, string password) {
            NA_Responsables nres = new NA_Responsables();
            DataSet datosUpon = nres.get_datosUpon(usuario, password);
            string usuarioUpon = "adm";
            string passwordUpon = "123";

            if (datosUpon.Tables[1].Rows.Count > 0)
            {
                usuarioUpon = datosUpon.Tables[1].Rows[0][0].ToString();
                passwordUpon = datosUpon.Tables[1].Rows[0][1].ToString();
            }

            Persona datoP = new Persona
            {
                Username = usuarioUpon,
                Password = passwordUpon
            };

            string json = JsonConvert.SerializeObject(datoP);
            string url = "http://192.168.11.62/ServcioUponApi/api/v1/auth/login";
            dynamic respuesta = api.Post(url, json);
            string codigo = respuesta.Resultado.CodVendedor.ToString();
            return int.Parse(codigo);
        }

        internal dynamic get_productoAlmacen(string nombreProducto, string usuario, string password)
        {
            string token = get_TokenUsuario(usuario, password);
            string parametro = "criterio=" + nombreProducto + "&usuario=" + usuario;

            string url = "http://192.168.11.62/ServcioUponApi/api/v1/almacenes/productos/Busqueda?";
            url = url + parametro;
            dynamic respuesta = api.Get_2(url, token);
            return  respuesta;
        }
    }
}


public class Persona
{
    public string Username { get; set; }
    public string Password { get; set; }

}


public class resultadoProducto { 
    public bool valido { get; set; }
    public List<MensajeProducto> Mensajes { get; set; }
    public List<producto> productos { get; set; }
}

public class MensajeProducto { 
    public string mensaje { get; set; }
}

public class producto { 
    public string Codigo { get; set; }
    public string Descripcion { get; set; }
}

