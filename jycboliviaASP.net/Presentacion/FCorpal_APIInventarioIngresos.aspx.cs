using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIinventario;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIInventarioIngresos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //-------------------------- GET - INVENTARIO INGRESO DETALLE
        protected async void btn_InvIngresoGET_Click(object sender, EventArgs e)
        {
            string numTransaccion = TextBox1.Text.Trim();

            // Validación de entrada
            if (string.IsNullOrEmpty(numTransaccion))
            {
                showalert("Por favor ingrese un número de ingreso válido.");
                LimpiarGrids();
                return;
            }
            try
            {
                // Llamar al método de la capa de negocio
                NA_APIinventario apiInv = new NA_APIinventario();
                InventarioIngreso ingreso = await apiInv.GetInventarioIngresoDetalleAsync("adm", "123", numTransaccion);

                if (ingreso != null)
                {
                    MostrarDetallesInventario(ingreso);
                }
                else
                {
                    showalert("No se encontraron registros con el número de ingreso proporcionado.");
                    LimpiarGrids();
                }
            }
            catch (ApplicationException ex)
            {
                // Excepciones esperadas (errores conocidos)
                showalert($"Error: {ex.Message}");
                LimpiarGrids();
            }
            catch (Exception ex)
            {
                // Excepciones generales (errores no esperados)
                showalert($"Ha ocurrido un error inesperado: {ex.Message}");
                LimpiarGrids();
            }
        }

        // Mostrar detalles en los controles GridView
        private void MostrarDetallesInventario(InventarioIngreso ingreso)
        {
            var invIngreso = new List<InventarioIngreso> { ingreso };
            gv_Inventario.DataSource = invIngreso;
            gv_Inventario.DataBind();

            if (ingreso.DetalleProductos != null && ingreso.DetalleProductos.Count > 0)
            {
                gv_DetalleProductos.DataSource = ingreso.DetalleProductos;
                gv_DetalleProductos.DataBind();
            }
            else
            {
                gv_DetalleProductos.DataSource = null;
                gv_DetalleProductos.DataBind();
            }
        }

        // Limpiar las grillas de datos
        private void LimpiarGrids()
        {
            gv_Inventario.DataSource = null;
            gv_Inventario.DataBind();
            gv_DetalleProductos.DataSource = null;
            gv_DetalleProductos.DataBind();
        }

        //-------------------------- GET - BUSCAR INVENTARIO INGRESO
        protected async void btn_invIngreso2_Click(object sender, EventArgs e)
        {
            string numTransaccion = TextBox2.Text.Trim();

            NA_APIinventario apiInv = new NA_APIinventario();
            List<Ingresos> ingresos = await apiInv.ObtenerIngresosAsync("adm", "123", numTransaccion);

            if (ingresos != null && ingresos.Count > 0)
            {
                gv_invIngresos2.DataSource = ingresos;
                gv_invIngresos2.DataBind();
            }
            else
            {
                gv_invIngresos2.DataSource = new List<Ingresos>();
                gv_invIngresos2.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron registros con el codigo proporcionado.');", true);
            }
        }

        //-------------------------- POST - INVENTARIO INGRESO
        protected async void btn_registrarIngreso_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) 
                return;
            var ingreso = CrearIngreso();

            try
            {
                var result = await RegistrarIngreso(ingreso);

                if (result != null)
                {
                    showalert($"Ingreso Registrado. Número de Ingreso: {result}");
                    LimpiarCampos();
                }
                else
                {
                    showalert("Ocurrió un error al registrar el ingreso.");
                }
            }
            catch (Exception ex)
            {
                showalert($"Error inesperado: {ex.Message}");
            }
        }

        private InventarioIngreso CrearIngreso()
        {
            var ingreso = new InventarioIngreso
            {
                NumeroIngreso = 0,
                Fecha = DateTime.Now,
                Referencia = txt_Referencia.Text,
                CodigoMoneda = int.Parse(dd_codMoneda.SelectedValue),
                CodigoAlmacen = int.Parse(txt_codAlmacen.Text.Trim()),
                MotivoMovimiento = txt_motMovimiento.Text.Trim(),
                ItemAnalisis = int.Parse(txt_itemAnalisis.Text.Trim()),
                Glosa = txt_glosa.Text,
                Usuario = "ADM",
                DetalleProductos = ObtenerDetallesProductos()
            };
            return ingreso;
        }
        private List<DetalleProductoIngre> ObtenerDetallesProductos()
        {
            var detalles = new List<DetalleProductoIngre>();
            int rowCount = Request.Form.AllKeys.Length;

            try
            {
                for (int i = 0; i < rowCount; i++)
                {
                    if (Request.Form["codigoProducto" + i] != null)
                    {
                        decimal cantidad = decimal.Parse(Request.Form["cantidad" + i], CultureInfo.InvariantCulture);
                        decimal costoUnitario = decimal.Parse(Request.Form["costoUnitario" + i], CultureInfo.InvariantCulture);
                        decimal costoTotal = cantidad * costoUnitario;

                        var detalle = new DetalleProductoIngre
                        {
                            Item = 0,
                            CodigoProducto = Request.Form["codigoProducto" + i],
                            UnidadMedida = int.Parse(Request.Form["unidadMedida" + i]),
                            Cantidad = cantidad,
                            CostoUnitario = costoUnitario,
                            CostoTotal = costoTotal
                        };
                        detalles.Add(detalle);
                    }
                }
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener detalles de productos: {ex.Message}");
                return null;
            }
            return detalles;
        }

        private async Task<string> RegistrarIngreso(InventarioIngreso ingreso)
        {
            try
            {
                var api = new NA_APIinventario();
                var token = await api.GetTokenAsync("adm", "123");

                var result = await api.PostInventarioIngresoAsync(ingreso, token);
                return result; 
            }
            catch (Exception ex)
            {
                showalert($"Error al registra el ingreso: {ex.Message}");
                return null; 
            }
        }

        private void LimpiarCampos()
        {
            txt_Referencia.Text = "";
            txt_codAlmacen.Text = "";
            txt_motMovimiento.Text = "";
            txt_itemAnalisis.Text = "";
            txt_glosa.Text = "";
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txt_codAlmacen.Text.Trim()))
            {
                showalert("Por favor, ingrese un codigo almacen.");
                return false;
            }

            if (string.IsNullOrEmpty(txt_motMovimiento.Text.Trim()))
            {
                showalert("Por favor, complete el campo motivo Movimiento.");
                return false;
            }

            if (string.IsNullOrEmpty(txt_itemAnalisis.Text.Trim()))
            {
                showalert("Por favor, complete el campo Item Analisis.");
                return false;
            }

            return true;
        }
        private void showalert(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{mensaje}');", true);
        }
    }
}