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
            this.Title = Session["BaseDatos"].ToString();
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
                string beneficiario = tx_beneficiario.Text.Trim();
                string estado = dd_estadoBoleta.SelectedItem.ToString();
                string moneda = tx_moneda.Text.ToString();
                string obj = tx_objeto.Text.Trim();
                string extencionObj = tx_extenciondObjeto.Text.Trim();

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
                DateTime fechaInicio;
                if (string.IsNullOrWhiteSpace(tx_fechainicio.Text))
                {
                    showalert("Debe ingresar una fecha Inicio.");
                    return false;
                }
                if (!DateTime.TryParseExact(
                    tx_fechainicio.Text.Trim(),
                    "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out fechaInicio))
                {
                    showalert("La fecha no tiene un formato válido. Use dd/MM/yyyy.");
                    return false;
                }
                DateTime fechaVencimiento;
                if (string.IsNullOrWhiteSpace(tx_fechaVencimiento.Text))
                {
                    showalert("Debe ingresar una fecha.");
                    return false;
                }
                if (!DateTime.TryParseExact(
                    tx_fechaVencimiento.Text.Trim(),
                    "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out fechaVencimiento))
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
                bool resultado = nboleta.set_guardarBoletaGarantia(fecha, moneda, monto, tipoBoleta, beneficiario, 
                                                cliente, estado, codUser, fechaInicio, fechaVencimiento, 
                                                obj, extencionObj);

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

            TextBox moneda = (TextBox)fila.FindControl("tx_monedaEditar");
            TextBox monto = (TextBox)fila.FindControl("tx_montoEditar");

            TextBox tipoBoleta = (TextBox)fila.FindControl("tx_tipoBoletaEditar");

            TextBox beneficiario = (TextBox)fila.FindControl("tx_beneficiarioEditar");
            TextBox cliente = (TextBox)fila.FindControl("tx_clienteEditar");

            DropDownList estado = (DropDownList)fila.FindControl("dd_estadoEditar");

            TextBox finicio = (TextBox)fila.FindControl("tx_fechainicioEditar");
            TextBox fvencimiento = (TextBox)fila.FindControl("tx_fechavencimientoEditar");
            TextBox objeto = (TextBox)fila.FindControl("tx_objetoEditar");
            TextBox extencionobjeto = (TextBox)fila.FindControl("tx_extencionobjetoEditar");

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

            string moneda1 = moneda.Text.Trim();
            if (montoDecimal <= 0)
            {
                showalert("el monto debe ser mayor a 0.");
                return;
            }

            string tipo = tipoBoleta.Text.Trim();
            string beneficiario1 = beneficiario.Text.Trim(); 
            string cli = cliente.Text.Trim();
            string est = estado.SelectedValue;
            DateTime finicio1 = DateTime.Parse(finicio.Text);
            DateTime fvenc1 = DateTime.Parse(fvencimiento.Text);
            string obj = objeto.Text.Trim();
            string extobj = extencionobjeto.Text.Trim();

            NCorpal_BoletasGarantia nboleta = new NCorpal_BoletasGarantia();

            bool resultado = nboleta.update_datosBoletaGarantia(
                    moneda1, montoDecimal, tipo, beneficiario1, cli,est, finicio1, fvenc1, obj, extobj, id);

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

        [System.Web.Services.WebMethod]
        public static List<string> ObtenerTiposBoleta(string prefixText, int count)
        {
            List<string> lista = new List<string>();

            lista.Add("Boleta de garantia");
            lista.Add("Garantia a primer requerimiento");
            lista.Add("Poliza de garantia");
            lista.Add("Letra de cambio");
            lista.Add("Deposito en garantia");

            return lista
                .Where(x => x.IndexOf(prefixText, StringComparison.OrdinalIgnoreCase) >= 0)
                .Take(count)
                .ToList();
        }
        [System.Web.Services.WebMethod]
        public static List<string> ObtenerTipoMoneda(string prefixText, int count)
        {
            List<string> lista = new List<string>();

            lista.Add("Bolivianos");
            lista.Add("Dolares");
            lista.Add("UFV");

            return lista
                .Where(x => x.IndexOf(prefixText, StringComparison.OrdinalIgnoreCase) >= 0)
                .Take(count)
                .ToList();
        }

        [System.Web.Services.WebMethod]
        public static List<string> obtenerTipoObjeto(string prefixText, int count)
        {
            List<string> lista = new List<string>();

            lista.Add("Seriedad de propuesta");
            lista.Add("Correcta inversion de anticipo");
            lista.Add("Cumplimiento de contrato");
            lista.Add("Pago Derechos arancelarios");
            lista.Add("Buena ejecucion de obra");
            lista.Add("Otro");

            return lista
                .Where(x => x.IndexOf(prefixText, StringComparison.OrdinalIgnoreCase) >= 0)
                .Take(count)
                .ToList();
        }


    }
}