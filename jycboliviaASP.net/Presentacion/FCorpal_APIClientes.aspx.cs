using jycboliviaASP.net.DatosSimec;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIclientes;
using static jycboliviaASP.net.Negocio.NA_PruebaAPI;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
// porq no me esta funcionando los if ejemplo con los campos vacion doy en el boton y no me sale ningun aviso y me actualiza la pagina nomas no esta funcionando el manejo de errores
        protected async void Btn_RegistrarCliente_Click(object sender, EventArgs e)
        {
            var na_clienteapi = new NA_APIclientes();
            string usuario = "adm";
            string password = "123";

            try
            {
                var token = await na_clienteapi.GetTokenAsync(usuario, password);
                var cliente = new clientePersona
                {
                    CodigoContacto = 0,
                    Nombres = TextBox1.Text,
                    ApellidoPaterno = TextBox2.Text,
                    ApellidoMaterno = TextBox3.Text,
                    ApellidoCasado = TextBox4.Text,
                    TipoDocumentoIdentidad = int.Parse(ddlNumbers.SelectedValue),
                    NumeroDocumento = TextBox5.Text,
                    Complemento = TextBox6.Text,
                    Telefono = TextBox7.Text,
                    Correo = TextBox8.Text,
                    Usuario = "adm"
                };
                var result = await na_clienteapi.PostClientesPersonasAsync(cliente, token);  
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }

        }
        protected async void Btn_RegistrarEmpresa_Click(object sender, EventArgs e)
        {
            var na_empresaapi = new NA_APIclientes();
            string usuario = "adm";
            string password = "123";

            try
            {
                var token = await na_empresaapi.GetTokenAsync(usuario, password);
                var empresa = new clienteEmpresa
                {
                    CodigoContacto = 0,
                    NombreLegal = TextBox9.Text,
                    NombreComercial = TextBox10.Text,
                    Direccion = TextBox11.Text,
                    Telefono = TextBox12.Text,
                    NIT = TextBox13.Text,
                    Correo = TextBox14.Text,
                    EsSucursal = Boolean.Parse(DropDownList1.SelectedValue),
                    Usuario = "adm"
                };
                var result = await na_empresaapi.PostClientesEmpresasAsync(empresa, token);
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
        }

        protected async void Btn_BuscarPersona_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = TextBox15.Text;
            NA_APIclientes pp = new NA_APIclientes();


            List<clientePersonaGet> personas = await pp.get_ClientesPersonasAsync("adm", "123", criterioBusqueda);

            if (personas != null && personas.Count > 0)
            {
                GridView1.DataSource = personas;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = new List<clientePersonaGet>();
                GridView1.DataBind();
            }
        }
    }
}