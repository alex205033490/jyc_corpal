//using jycboliviaASP.net.Negocio;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIcompras;

using System.Threading.Tasks;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APICompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //------------------------------------  POST - POST API COMPRAS
        protected async void btn_registrarCompra_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;
            var compra = CrearCompra();

            try
            {
                var result = await RegistrarCompra(compra);
                if (result != null)
                {
                    showalert($"Compra Registrada. Número de Compra: {result}");
                    LimpiarCampos();
                }
                else
                {
                    showalert("Ocurrio un error al registrar la compra.");
                }
            }
            catch (Exception ex)
            {
                showalert($"Error inesperado: {ex.Message}");
            }
        }
        
        private DTOCompras CrearCompra()
        {
            decimal costoTotalProductos = 0;
            var detallesProducto = ObtenerDetallesProducto();

            foreach (var detalle in detallesProducto)
            {
                costoTotalProductos += detalle.ImporteTotal;
            }

            var compra = new DTOCompras
            {
                NumeroCompra = 0,
                Fecha = DateTime.Now,
                Referencia = txt_referencia.Text,
                ImporteProductos = costoTotalProductos,
                ImporteDescuento = decimal.Parse(txt_importe_Descuentos.Text),
                ImporteTotal = costoTotalProductos - decimal.Parse(txt_importe_Descuentos.Text),// ImporteProductos-ImporteDescuento
                CodigoMoneda = int.Parse(dd_codMoneda.SelectedValue),
                CodigoProveedor = int.Parse(txt_codProveedor.Text),
                CodigoDistribucionGastos = int.Parse(txt_codDistribucionGastos.Text),
                Pagos = ObtenerDetallesPagos(),
                //Gastos = ObtenerDetallesGastos(),
                FacturaPosterior = Boolean.Parse(dd_fPosterior.SelectedValue),
                Factura = ObtenerDetalleFactura(),
                Glosa = txt_glosa.Text,
                DetalleProductos = ObtenerDetallesProducto(),
                Usuario = "adm"
            };
            return compra;
        }

        private List<DetalleProductoCompra> ObtenerDetallesProducto()
        {
            var detalles = new List<DetalleProductoCompra>();
            int rowCount = Request.Form.AllKeys.Length;

            try
            {
                for (int i = 0; i < rowCount; i++)
                {
                    if (Request.Form["codigoProducto" + i] != null)
                    {
                        decimal cantidad = decimal.Parse(Request.Form["cantidad" + i], CultureInfo.InvariantCulture);
                        decimal precioUnitario = decimal.Parse(Request.Form["precioUnitario" + i], CultureInfo.InvariantCulture);
                        decimal costoTotal = cantidad * precioUnitario;

                        var detalle = new DetalleProductoCompra
                        {
                            NumeroItem = 0,
                            CodigoProducto = Request.Form["codigoProducto" + i],
                            Cantidad = cantidad,
                            CodigoUnidadMedida = int.Parse(Request.Form["codUnidadMedida" + i]),
                            PrecioUnitario = precioUnitario,
                            ImporteDescuento = decimal.Parse(Request.Form["importeDescuento" + i]),
                            PorcentajeGasto = decimal.Parse(Request.Form["porcentajeGasto" + i]),
                            ImporteTotal = costoTotal
                        };
                        detalles.Add(detalle);
                    }
                }
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener detalles del producto: {ex.Message}");
                return null;
            }
            return detalles;
        }
        private async Task<string> RegistrarCompra(DTOCompras compras)
        {
            try
            {
                var api = new NA_APIcompras();
                var token = await api.ObtenerTokenAsync("adm", "123");

                var result = await api.PostComprasAsync(compras, token);
                return result;
            }
            catch (Exception ex)
            {
                showalert($"Error al registrar la compra: {ex.Message}");
                return null;
            }
        }

        private GastosDTO ObtenerDetallesGastos()
        {
            try
            {
                var gastos = new GastosDTO
                {
                    CodigoGasto = 0,
                    Importe = 0,
                    CodigoMoneda = 0,
                    AplicaIva = false
                };
                return gastos;
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener detalles del pago: {ex.Message}");
                return null;
            }
        }
        private PagosDTO ObtenerDetallesPagos()
        {
            try
            {
                var pagos = new PagosDTO
                {
                    TotalEfectivo = decimal.Parse(txt_totalEfectivo.Text),
                    TotalCredito = 0,
                    TotalCheques = 0,
                    TotalDeposito = 0,
                    //Credito = ObtenerDetallesCredito(),
                    //Cheque = ObtenerDetallesCheque(),
                    //Deposito = ObtenerDetallesDeposito(),
                };
                return pagos;
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener detalles del pago: {ex.Message}");
                return null;
            }
        }
        private CreditoDTO ObtenerDetallesCredito()
        {
            try
            {
                var credito = new CreditoDTO
                {
                    TipoCuenta = 0,
                    DiasCredito = 0
                };
                return credito;
            }
            catch(Exception ex)
            {
                showalert($"Error al obtener detalles del credito: {ex.Message}");
                return null;
            }
        }
        private ChequeDTO ObtenerDetallesCheque()
        {
            try
            {
                var cheque = new ChequeDTO
                {
                    CodigoBanco = 0,
                    NumeroCuenta = "",
                    CodigoCheque = 0, 
                    NumeroCheque = 0,
                    FechaGiro = ""
                };
                return cheque;
            }
            catch(Exception ex)
            {
                showalert($"Error al obtener detalles del cheque: {ex.Message}");
                return null;
            }
        }
        private DepositoDTO ObtenerDetallesDeposito()
        {
            try
            {
                var deposito = new DepositoDTO
                {
                    CodigoBanco = 0,
                    NumeroCuenta = "",
                    Referencia = ""
                };
                return deposito;
            }
            catch (Exception ex) 
            {
                showalert($"Error al obtener detalles del deposito: {ex.Message}");
                return null;
            }
        }
        private FacturaDTO ObtenerDetalleFactura()
        {
            try
            {
                var factura = new FacturaDTO
                {
                    NIT_CI = "9759005",//txt_nit.Text,
                    RazonSocial = "rs 19-11",//txt_razonSocial.Text,
                    NumeroFactura = "nrof 19-11",//txt_nFactura.Text,
                    CodigoAutorizacion = "ca 123",//txt_codAutorizacion.Text,
                    CodigoControl = "cc 123",//txt_codControl.Text,
                    ImporteTotal = 10,//decimal.Parse(txt_imptotal.Text, CultureInfo.InvariantCulture),
                    ImporteDescuento = 0,//decimal.Parse(txt_impDescuento.Text, CultureInfo.InvariantCulture),
                    ImporteGift = 0,//decimal.Parse(txt_impGift.Text, CultureInfo.InvariantCulture),
                    ImporteNeto = 0,//decimal.Parse(txt_impNeto.Text, CultureInfo.InvariantCulture),
                    AplicaCredictoFiscal = true,//Boolean.Parse(dd_apCredFiscal.SelectedValue)
                };
                return factura;
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener detalle de la factura: {ex.Message}");
                return null;
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txt_referencia.Text.Trim()))
            {
                showalert("Por favor, Complete el campo referencia");
                return false;
            }
            if (string.IsNullOrEmpty(txt_importe_Descuentos.Text.Trim()))
            {
                showalert("Por favor, Complete el campo importe descuento");
                return false;
            }
            if (string.IsNullOrEmpty(txt_codProveedor.Text.Trim()))
            {
                showalert("Por favor, Complete el campo código proveedor");
                return false;
            }
            if (string.IsNullOrEmpty(txt_codDistribucionGastos.Text.Trim()))
            {
                showalert("Por favor, Complete el campo codigo Distribución Gastos");
                return false;
            }
            return true;
        }
        private void LimpiarCampos()
        {
            txt_referencia.Text = "";
            txt_importe_Descuentos.Text = "";
            txt_codProveedor.Text = "";
            txt_codDistribucionGastos.Text = "";
        }
        private void showalert(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }
    }
}