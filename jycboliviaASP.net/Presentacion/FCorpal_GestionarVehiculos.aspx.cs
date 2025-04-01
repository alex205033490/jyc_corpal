using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_GestionarVehiculos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarVehiculos();
            }
        }

        private void cargarVehiculos()
        {
            NCorpal_Vehiculos negocio = new NCorpal_Vehiculos();
            DataSet datos = negocio.get_mostrarVehiculosGV();
            gv_listaVehiculos.DataSource = datos;
            gv_listaVehiculos.DataBind();
        }

        protected void btn_registrarForm_Click(object sender, EventArgs e)
        {
            registrarFormulario();

        }
        private void registrarFormulario()
        {
            validarFormulario();
            try
            {
                string placa = txt_placa.Text.Trim();
                string marca = txt_marca.Text.Trim();
                string modelo = txt_modelo.Text.Trim();
                string detalle = txt_detalle.Text.Trim();
                string conductor = txt_conductor.Text.Trim();
                decimal capacidad = decimal.Parse(txt_capacidad.Text);
                int cargacajas = int.Parse(txt_capCajas.Text);

                NCorpal_Vehiculos negocio = new NCorpal_Vehiculos();
                bool resultado = negocio.post_registrarVehiculo(placa, marca, modelo, detalle, conductor, capacidad, cargacajas);

                if (resultado)
                {
                    showAlert("Registro guardado exitosamente");
                    limpiarFormulario();
                    cargarVehiculos();
                }
                else
                {
                    showAlert("Hubo un error al guardar el registro");
                }
            }
            catch(Exception ex)
            {
                showAlert($"Ocurrio un error inesperado. {ex.Message}");
            }

        }
        private void limpiarFormulario()
        {
            txt_placa.Text = string.Empty;
            txt_marca.Text = string.Empty;
            txt_modelo.Text = string.Empty;
            txt_detalle.Text = string.Empty;
            txt_conductor.Text = string.Empty;
            txt_capacidad.Text = string.Empty;
            txt_capCajas.Text = string.Empty;
        }
        private bool validarFormulario()
        {
            if (string.IsNullOrEmpty(txt_modelo.Text.Trim()))
            {
                showAlert("El campo Modelo no puede estar vacio");
                return false;
            }
            if (string.IsNullOrEmpty(txt_placa.Text.Trim()))
            {
                showAlert("El campo Placa no puede estar vacio");
                return false;
            }

            return true;
        }



        private void showAlert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        protected void btn_anular_Click(object sender, EventArgs e)
        {
            NCorpal_Vehiculos negocio = new NCorpal_Vehiculos();
            List<int> selectCodigo = new List<int>();

            foreach(GridViewRow row in gv_listaVehiculos.Rows)
            {
                CheckBox chkAnular = (CheckBox)row.FindControl("chkSelect");

                if(chkAnular != null & chkAnular.Checked)
                {
                    int codigo = Convert.ToInt32(gv_listaVehiculos.DataKeys[row.RowIndex].Value);
                    selectCodigo.Add(codigo);
                }
            }
            if (selectCodigo.Count > 0)
            {
                bool exito = negocio.anular_registro(selectCodigo);
                if (exito)
                {
                    string codigoStr = string.Join(", ", selectCodigo);
                    showAlert($"Se ha anulado el registro exitosamente.");
                    cargarVehiculos();
                }
                else
                {
                    string codigoStr = string.Join(",", selectCodigo);
                    showAlert($"Hubo un error al anular el registro: {codigoStr}");
                }
            }
            else
            {
                showAlert("Por favor seleccione al menos 1 registro.");
            }

        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> codigosActualizados = new List<int>();
                foreach(GridViewRow row in gv_listaVehiculos.Rows)
                {
                    CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;
                    if(chkSelect != null && chkSelect.Checked)
                    {
                        int codigo = Convert.ToInt32(row.Cells[1].Text);
                        actualizarRegistros(row, codigo);
                        codigosActualizados.Add(codigo);
                    }
                }
                if(codigosActualizados.Count > 0)
                {
                    showAlert($"Registros actualizados correctamente.");
                    cargarVehiculos();
                }
                else
                {
                    showAlert("No se seleccionaron registros para actualizar.");
                }
            }
            catch(Exception ex)
            {
                showAlert($"Error inesperado: {ex.Message}");
            }
        }

        private void actualizarRegistros(GridViewRow row, int codigo)
        {
            try
            {
                TextBox txtmarca = row.FindControl("tx_marcaCar") as TextBox;
                TextBox txtmodelo = row.FindControl("tx_modeloCar") as TextBox;
                TextBox txtplaca = row.FindControl("tx_placaCar") as TextBox;
                TextBox txtconductor = row.FindControl("tx_conductorCar") as TextBox;
                TextBox txtcapacidad = row.FindControl("tx_capacidadCar") as TextBox;
                TextBox txtcargacajas = row.FindControl("tx_cargacajasCar") as TextBox;
                TextBox txtdetalle = row.FindControl("tx_detalleCar") as TextBox;

                string marca = txtmarca?.Text ?? "";
                string modelo = txtmodelo?.Text ?? "";
                string placa = txtplaca?.Text ?? "";
                string conductor = txtconductor?.Text ?? "";
                string detalle = txtdetalle?.Text ?? "";

                float capacidad = 0;
                if(!string.IsNullOrEmpty(txtcapacidad?.Text) && !float.TryParse(txtcapacidad.Text, out capacidad))
                {
                    capacidad = 0;
                }

                int cargacajas = 0;
                if(!string.IsNullOrEmpty(txtcargacajas?.Text) && !int.TryParse(txtcargacajas.Text, out cargacajas))
                {
                    cargacajas = 0;
                }
                
                NCorpal_Vehiculos negocio = new NCorpal_Vehiculos();
                bool resultado = negocio.post_updateRegistroVehiculo(codigo,placa, marca, modelo, detalle, conductor, capacidad, cargacajas);

                if (!resultado)
                {
                    showAlert($"Error al actualizar el registro");
                }
            } catch(Exception ex)
            {
                showAlert($"Error inesperado al actualizar el registro. {ex.Message}");
            }
        }
    }
}