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
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, complete el campo nombre legal');", true);
            }
            if (string.IsNullOrWhiteSpace(txt_nomcomercial.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, complete el campo nombre comercial');", true);
            }
            if (string.IsNullOrWhiteSpace(txt_direccion.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, ingrese una direccion');", true);
            }
            if (string.IsNullOrWhiteSpace(txt_nit.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, ingrese un NIT');", true);
            }
            return true;
        }
        private bool ValidateClientData(out string errorMessage)
        {
            errorMessage = string.Empty;
            
            if (string.IsNullOrWhiteSpace(txt_paterno.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, complete el campo Apellido Paterno.');", true);
                return false;
            }
            if (string.IsNullOrWhiteSpace(dd_tdocumento.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, Ingrese un tipo de documento de identidad.');", true);
                return false;
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
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Empresa Registrado Exitosamente.');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al registrar.');", true);
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
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cliente registrado exitosamente.');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al registrar el cliente.');", true);
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

            /*
            string nombreLegal = txt_nomlegal.Text.Trim();
            string nombreComercial = txt_nomcomercial.Text.Trim();
            string Direccion = txt_direccion.Text.Trim();
            string NIT = txt_nit.Text.Trim();
            

            // Realiza las validaciones
            if (string.IsNullOrEmpty(nombreLegal))
            {
                // Muestra un mensaje de error o maneja la validación
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, complete el campo Nombre Legal.');", true);
                return;
            }

            if (string.IsNullOrEmpty(nombreComercial))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, complete el campo Nombre Comercial.');", true);
                return;
            }

            if (string.IsNullOrEmpty(Direccion))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, Ingrese un Direccion .');", true);
                return;
            }

            if (string.IsNullOrEmpty(NIT))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, Ingrese un NIT.');", true);
                return;
            }



            var na_empresaapi = new NA_APIclientes();
            string usuario = "adm";
            string password = "123";

            try
            {
                var token = await na_empresaapi.GetTokenAsync(usuario, password);
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
                var result = await na_empresaapi.PostEmpresaAsync(empresa, token);
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('La empresa ha sido registrado exitosamente.');", true);
            */
        }

        protected async void btn_buscar_CliEmpr_Click(object sender, EventArgs e)
        {

            string criterioBusqueda = txt_filtroBusqueda.Text;
            NA_APIclientes pp = new NA_APIclientes();

            List<ClienteEmpresaGetDTO> personas = await pp.get_ClientesPersonasAsync("adm", "123", criterioBusqueda);

            if (personas != null && personas.Count > 0)
            {
                GridView1.DataSource = personas;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = new List<ClienteEmpresaGetDTO>();
                GridView1.DataBind();
            }
            

            // Realiza las validaciones
            if (string.IsNullOrEmpty(criterioBusqueda))
            {
                // Muestra un mensaje de error o maneja la validación
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, ingrese un valor para realizar su busqueda.');", true);
                return;
            }

        }
    }
}