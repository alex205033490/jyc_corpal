using jycboliviaASP.net.DatosSimec;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static jycboliviaASP.net.Negocio.NA_APIclientes;
using static jycboliviaASP.net.Negocio.NA_PruebaAPI;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //------------------------      POST CLIENTES/PERSONAS      ----------------------//
        // VALIDACION
        private bool ValidateEmpresData(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(txt_nomlegal.Text))
            {
                ShowAlert("Por favor, complete el campo nombre legal.");
            }
            if (string.IsNullOrWhiteSpace(txt_nomcomercial.Text))
            {
                ShowAlert("Por favor, complete el campo nombre comercial.");
            }
            if (string.IsNullOrWhiteSpace(txt_direccion.Text))
            {
                ShowAlert("Por favor, ingrese una dirección.");
            }
            if (string.IsNullOrWhiteSpace(txt_nit.Text))
            {
                ShowAlert("Por favor, ingrese un NIT.");
            }
            return true;
        }
        private bool ValidateClientData(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(txt_nombre.Text))
            {
                ShowAlert("Por favor, complete el campo nombre.");
               
            }
            if (string.IsNullOrWhiteSpace(txt_paterno.Text))
            {
                ShowAlert("Por favor, complete el campo Apellido Paterno.");
                
            }
            if (string.IsNullOrWhiteSpace(dd_tdocumento.Text))
            {
                ShowAlert("Por favor, Ingrese un tipo de documento de identidad.");
                
            }
            if (string.IsNullOrWhiteSpace(txt_numdocumento.Text))
            {
                ShowAlert("Por Favor, complete el campo Número Documento.");
               
            }
            return true;
        }
        // METODO PARA REGISTRAR EMPRESA
        private async Task RegisterEmpresaAsync()
        {
            string usuario = "adm";
            string password = "123";
            var na_clienteapi = new NA_APIclientes();

            var token = await na_clienteapi.GetTokenAsync(usuario, password);
            if (string.IsNullOrEmpty(token))
            {
                ShowAlert("Error al obtener el token.");
                return;
            }    

            var empresa = new clienteEmpresaDTO
            {
                CodigoContacto = 0,
                NombreLegal = txt_nomlegal.Text,
                NombreComercial = txt_nomcomercial.Text,
                Direccion = txt_direccion.Text,
                Telefono = txt_telefono2.Text,
                NIT = txt_nit.Text,
                Correo = txt_correo2.Text,
                EsSucursal = Boolean.Parse(dd_EsSucursal.SelectedValue),
                Usuario = "adm"
            };
            var result = await na_clienteapi.PostEmpresaAsync(empresa, token);

            if (result)
            {
                ShowAlert("Empresa Registrado Exitosamente.");
                txt_nomlegal.Text = string.Empty;
                txt_nomcomercial.Text = string.Empty;
                txt_direccion.Text = string.Empty;
                txt_telefono2.Text = string.Empty;
                txt_nit.Text = string.Empty;
                txt_correo2.Text = string.Empty;
            }
            else
            {
                ShowAlert("Error al registrar proveedor.");
            }
        }

        // METODO PARA REGISTRAR CLIENTE/PERSONA
        private async Task RegisterClienteAsync()
        {
            string usuario = "adm";
            string password = "123";
            var na_clienteapi = new NA_APIclientes();

            var token = await na_clienteapi.GetTokenAsync(usuario, password);
            var cliente = new clientePersonaDTO
            {
                CodigoContacto = 0,
                Nombres = txt_nombre.Text,
                ApellidoPaterno = txt_paterno.Text,
                ApellidoMaterno = txt_materno.Text,
                ApellidoCasado = txt_casado.Text,
                TipoDocumentoIdentidad = int.Parse(dd_tdocumento.SelectedValue),
                NumeroDocumento = txt_numdocumento.Text,
                Complemento = txt_complemento.Text,
                Telefono = txt_telefono.Text,
                Correo = txt_correo.Text,
                Usuario = "adm"
            };
            var result = await na_clienteapi.PostPersonaAsync(cliente, token);
            if (result)
            {
                ShowAlert("Cliente registrado exitosamente.");
                txt_nombre.Text = string.Empty;
                txt_paterno.Text = string.Empty;
                txt_materno.Text = string.Empty;
                txt_casado.Text = string.Empty;

                txt_numdocumento.Text = string.Empty;
                txt_complemento.Text = string.Empty;
                txt_telefono.Text = string.Empty;
                txt_correo.Text = string.Empty;
            }
            else
            {
                ShowAlert("Error al registrar el cliente.");
            }
        }

        protected async void Btn_RegistrarCliente_Click(object sender, EventArgs e)
        {
            if (!ValidateClientData(out string errorMessage))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{errorMessage}');", true);
                return;
            }
            try
            {
                await RegisterClienteAsync();
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
        }

        //--------------------------      METODO POST CLIENTES/EMPRESAS     --------------------------//
        protected async void btn_registrarEmpresa_Click(object sender, EventArgs e)
        {
            if(!ValidateEmpresData(out string errorMessage))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{errorMessage}');", true);
                return;
            }
            try
            {
                await RegisterEmpresaAsync();
            }
            catch(Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }

        }

        protected async void btn_buscar_CliEmpr_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txt_filtroBusqueda.Text;

            // Realiza las validaciones
            if (string.IsNullOrEmpty(criterioBusqueda))
            {
                ShowAlert("Por favor, ingrese un dato para buscar.");
                return;
            }

            try
            {
                NA_APIclientes pp = new NA_APIclientes();
                string token = await pp.ObtenerTokenAsync("adm","123");

                if (string.IsNullOrEmpty(token))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error de autenticación. No se pudo obtener el token');", true);
                    return;
                }

                List<ClienteEmpresaGetDTO> personas = await pp.get_ClientesPersonasAsync(token, criterioBusqueda);

                if (personas != null && personas.Count > 0)
                {
                    GridView1.DataSource = personas;
                    GridView1.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Lo siento, no hay registros que coincidan con la búsqueda');", true);
                    GridView1.DataSource = new List<ClienteEmpresaGetDTO>();
                    GridView1.DataBind();
                }
            } catch(Exception ex)
            {
                Response.Write($"Error inesperado: {ex.Message}");
            }
        }
        private void ShowAlert(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }

    }
}