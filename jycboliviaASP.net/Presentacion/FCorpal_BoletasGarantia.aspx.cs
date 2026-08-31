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
    public partial class FCorpal_BoletasGarantia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getBoletasGarantia();

                tx_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void btn_registrarBoletaGarantia_Click(object sender, EventArgs e)
        {
            if (!set_registroBoletaGarantia())
            {
                showalert("Error al registrar la boleta garantia.");
                return;
            }
            else
            {
                showalert("Boleta Registrado Correctamente.");
                limpiarForm();
            }


        }

        private bool set_registroBoletaGarantia()
        {
            try
            {
                NCorpal_BoletasGarantia nboleta = new NCorpal_BoletasGarantia();
                bool resultadoGral = true;

                NA_Responsables nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = nresp.getCodUsuario(usuarioAux, passwordAux);

                string tipoBoleta = tx_tipoBoletaGarantia.Text.Trim();
                string cliente = tx_cliente.Text.Trim();
                string estado = dd_estadoBoleta.SelectedItem.ToString();

                DateTime fecha;
                if (string.IsNullOrWhiteSpace(tx_fecha.Text))
                {
                    showalert("Debe ingresar una fecha.");
                    return false;
                }
                if (!DateTime.TryParseExact(
                    tx_fecha.Text.Trim(),
                    "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out fecha))
                        {
                            showalert("La fecha no tiene un formato válido. Use dd/MM/yyyy.");
                            return false;
                        }

                decimal monto;
                if (string.IsNullOrWhiteSpace(tx_Monto.Text))
                {
                    showalert("Debe ingresar el monto.");
                    return false;
                }

                if (!decimal.TryParse(
                    tx_Monto.Text.Trim(),
                    out monto))
                {
                    showalert("El monto ingresado no es válido.");
                    return false;
                }

                if (monto <= 0)
                {
                    showalert("El monto debe ser mayor a 0.");
                    return false;
                }
                bool resultado = nboleta.set_guardarBoletaGarantia(fecha, monto, tipoBoleta, cliente, estado, codUser);

                if (!resultado)
                {
                    resultadoGral = false;
                }

                return resultadoGral;
            } 
            catch(Exception ex)
            {
                showalert("Error al registrar la boleta. " + ex.Message);
                return false;
            }
        }

        private void getBoletasGarantia()
        {
            NCorpal_BoletasGarantia nboleta = new NCorpal_BoletasGarantia();
            DataSet datos = nboleta.get_getBoletasGarantia();

            if(datos != null && datos.Tables.Count > 0)
            {
                gv_boletasGarantia.DataSource = datos.Tables[0];
                gv_boletasGarantia.DataBind();
            }
        }


        private void showalert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        protected void gv_boletasGarantia_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_boletasGarantia.EditIndex = e.NewEditIndex;
            getBoletasGarantia();

        }

        protected void gv_boletasGarantia_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_boletasGarantia.EditIndex = -1;
            getBoletasGarantia();
        }

        protected void gv_boletasGarantia_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow fila = gv_boletasGarantia.Rows[e.RowIndex];

            int id = Convert.ToInt32(gv_boletasGarantia.DataKeys[e.RowIndex].Value);

            TextBox monto = (TextBox)fila.FindControl("tx_montoEditar");

            TextBox tipoBoleta = (TextBox)fila.FindControl("tx_tipoBoletaEditar");
            
            TextBox cliente = (TextBox)fila.FindControl("tx_clienteEditar");

            DropDownList estado = (DropDownList)fila.FindControl("dd_estadoEditar");

            decimal montoDecimal;

            if (string.IsNullOrWhiteSpace(monto.Text))
            {
                showalert("debe ingresar el monto");
                return;
            }

            if (!decimal.TryParse(monto.Text.Trim(), out montoDecimal))
            {
                showalert("el monto ingresado no es valido.");
            }

            if (montoDecimal <= 0)
            {
                showalert("el monto debe ser mayor a 0.");
                return;
            }

            string tipo = tipoBoleta.Text.Trim();
            string cli = cliente.Text.Trim();
            string est = estado.SelectedValue;

            NCorpal_BoletasGarantia nboleta = new NCorpal_BoletasGarantia();

            bool resultado = nboleta.update_datosBoletaGarantia(
                    montoDecimal, tipo, cli, est, id);

            if (resultado)
            {
                gv_boletasGarantia.EditIndex = -1;

                getBoletasGarantia();

                showalert("Boleta actualizada correctamente.");
            }
            else
            {
                showalert("No se pudo actualizar la boleta.");
            }
        }

        private void limpiarForm() {
            tx_Monto.Text = string.Empty;
            tx_tipoBoletaGarantia.Text = string.Empty;
            tx_cliente.Text = string.Empty;

            getBoletasGarantia();
        }
    }
}