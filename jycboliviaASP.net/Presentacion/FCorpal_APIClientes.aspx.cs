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

            var token = await na_clienteapi.ObtenerTokenAsync(usuario, password);
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

            var token = await na_clienteapi.ObtenerTokenAsync(usuario, password);
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


        //--------------------------        METODO GET CLIENTE/EMPRESA      -------------------------//
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
                string token = await ObtenerTokenAsync("adm","123");

                if (string.IsNullOrEmpty(token))
                {
                    ShowAlert("Error de autenticación. No se pudo obtener el token");
                    return;
                }
                var cli = new NA_APIclientes();
                List<ClienteGetDTO> personas = await cli.GET_ClientesAsync(token, criterioBusqueda);

                if (personas != null && personas.Count > 0)
                {
                    MostrarClientes(personas);
                }
                else
                {
                    ShowAlert("Lo siento, no hay registros que coincidan con la búsqueda");
                    LimpiarGridView();
                }
            } catch(Exception ex)
            {
                Response.Write($"Error inesperado: {ex.Message}");
            }
        }


        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            NA_APIclientes cl = new NA_APIclientes();
            return await cl.ObtenerTokenAsync(usuario, password);
        }
        private async Task<List<ClienteGetDTO>> BuscarClientesAsync(string token, string criterioBusqueda)
        {
            NA_APIclientes cli = new NA_APIclientes();
            return await cli.GET_ClientesAsync(token, criterioBusqueda);
        }
        private void MostrarClientes(List<ClienteGetDTO> personas)
        {
            GridView2.DataSource = personas;
            GridView2.DataBind();
        }
        private void LimpiarGridView()
        {
            GridView2.DataSource = new List<ClienteGetDTO>();
            {
                GridView2.DataBind();
            }
        }
        
        // -------------------------- METODO PARA VACIAR CLIENTES UPON
        protected void btn_vaciadoClienteUpon_Click(object sender, EventArgs e)
        {
            List<int> codigosContactoSeleccionados = new List<int>();
            List<string> nombresSeleccionados = new List<string>();
            List<string> ciSeleccionado = new List<string>();
            List<string> correosSeleccionados = new List<string>();
            List<string> telefonosSeleccionados = new List<string>();

            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkSeleccionar = (CheckBox)row.FindControl("chkSeleccionar");
                if(chkSeleccionar != null && chkSeleccionar.Checked)
                {
                    int codigocontacto = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                    string nombreCompleto = row.Cells[2].Text.Trim();
                    string ci = row.Cells[3].Text.Trim();
                    string correo = row .Cells[4].Text.Trim();
                    string telefono = row.Cells[5].Text.Trim();

                    telefono = string.IsNullOrEmpty(telefono) || telefono == "&nbsp;" ? null : telefono;
                    correo = string.IsNullOrEmpty(correo) || correo == "&nbsp;" ? null : correo;
                    ci = string.IsNullOrEmpty(ci) || ci == "&nbsp;" ? null : ci;
                    nombreCompleto = string.IsNullOrEmpty(nombreCompleto) || nombreCompleto == "&nbsp;" ? null : nombreCompleto;

                    codigosContactoSeleccionados.Add(codigocontacto);
                    nombresSeleccionados.Add(nombreCompleto);
                    ciSeleccionado.Add(ci);
                    correosSeleccionados.Add(correo);
                    telefonosSeleccionados.Add(telefono);
                }
            }
            if (codigosContactoSeleccionados.Count == 0)
            {
                ShowAlert("No se seleccionaron registros");
                return;
            }

            bool exito = VaciadoClientesUpon(codigosContactoSeleccionados, nombresSeleccionados, ciSeleccionado, correosSeleccionados, telefonosSeleccionados);

            if (exito)
            {
                ShowAlert("Registros Insertados Correctamente!");
            }
        }

        private bool VaciadoClientesUpon(List<int> codigosContacto, List<string>nombres, List<string> ci, List<string> correos, List<string> telefonos)
        {
            NCorpal_Clientes negocioCli = new NCorpal_Clientes();
            bool exito = false;

            for (int i = 0; i < codigosContacto.Count; i++)
            {
                try
                {
                    exito = negocioCli.insert_vaciadocliente(codigosContacto[i], nombres[i], ci[i], correos[i], telefonos[i]);
                    if (!exito)
                    {
                        ShowAlert($"Error al insertar el registro con código {codigosContacto[i]}");
                        break;
                    }
                
                }
                catch(Exception ex)
                {
                    ShowAlert($"Error al insertar el registro con código {codigosContacto[i]}: {ex.Message}");
                    break;
                }
            }
            return exito;
        }

        // ------------------------- METODO PARA VACIAR CLIENTES UPON2
        protected void VaciarClientes_Click(object sender, EventArgs e)
        {
            List<int> codigosContactoSelecc = new List<int>();
            List<string> nombresSelecc = new List<string>();
            List<string> ciselecc = new List<string>();
            List<string> correosselecc = new List<string>();
            List<string> telefonosselecc = new List<string>();

            foreach (GridViewRow row in GridView2.Rows)
            {
                int codigoContacto = Convert.ToInt32(GridView2.DataKeys[row.RowIndex].Value);
                string nombreCompleto = row.Cells[1].Text.Trim();
                string ci = row.Cells[2].Text.Trim();
                string correo = row.Cells[3].Text.Trim();
                string telefono = row.Cells[4].Text.Trim();

                telefono = string.IsNullOrEmpty(telefono) || telefono == "&nbsp;" ? null : telefono;
                correo = string.IsNullOrEmpty(correo) || correo == "&nbsp;" ? null : correo;
                ci = string.IsNullOrEmpty(ci) || ci == "&nbsp;" ? null : ci;
                nombreCompleto = string.IsNullOrEmpty(nombreCompleto) || nombreCompleto == "&nbsp;" ? null : nombreCompleto;

                codigosContactoSelecc.Add(codigoContacto);
                nombresSelecc.Add(nombreCompleto);
                ciselecc.Add(ci);
                correosselecc.Add(correo);
                telefonosselecc.Add(telefono);
            }
            if (codigosContactoSelecc.Count == 0)
            {
                ShowAlert("No hay registros a vaciar");
                return;
            }

            bool exito = VaciadoClientesUpon(codigosContactoSelecc, nombresSelecc, ciselecc, correosselecc, telefonosselecc);

            if (exito)
            {
                ShowAlert("Registros Insertados Correctamente");
                LimpiarGridView();
            }

        }


        private void ShowAlert(string message)
        {
            string script = $"alert('{message.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }
        protected async void btn_buscarCliente_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txt_inputCliente.Text;

            // Realiza las validaciones
            if (string.IsNullOrEmpty(criterioBusqueda))
            {
                ShowAlert("Por favor, ingrese un dato para buscar.");
                return;
            }

            try
            {
                string token = await ObtenerTokenAsync("adm", "123");

                if (string.IsNullOrEmpty(token))
                {
                    ShowAlert("Error de autenticación. No se pudo obtener el token");
                    return;
                }
                var cli = new NA_APIclientes();
                List<ClienteGetDTO> personas = await cli.GET_ClientesAsync(token, criterioBusqueda);

                if (personas != null && personas.Count > 0)
                {
                    MostrarClientes(personas);
                }
                else
                {
                    ShowAlert("Lo siento, no hay registros que coincidan con la búsqueda");
                    LimpiarGridView();
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error inesperado: {ex.Message}");
            }
        }
    }
}