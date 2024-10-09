//using jycboliviaASP.net.Negocio;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIcompras;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APICompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btn_registrarCompra_Click(object sender, EventArgs e)
        {
            var na_registrarComprasApi = new NA_APIcompras();
            string username = "adm";
            string password = "123";

            try
            {
                var token = await na_registrarComprasApi.GetTokenAsync(username, password);
                var compras = new DTOCOMPRAS
                {
                    NumeroCompra = 0,
                    Fecha = DateTime.Now,
                    Referencia = txt_referencia.Text,
                    ImporteProductos = decimal.Parse(txt_cantidad.Text) * decimal.Parse(txt_precUnitario.Text),
                    ImporteDescuento = decimal.Parse(txt_impDescuento.Text),
                    ImporteTotal = decimal.Parse(txt_cantidad.Text) * int.Parse(txt_precUnitario.Text),
                    CodigoMoneda = int.Parse(dd_codMoneda.SelectedValue),
                    CodigoProveedor = int.Parse(txt_codProveedor.Text),
                    CodigoDistribucionGastos = int.Parse(txt_codDistribucionGastos.Text),
                    // Pagos
                    Gastos = new List<Gastos>
                    {
                        new Gastos
                        {
                            CodigoGasto = int.Parse(txt_codGasto.Text),
                            Importe = decimal.Parse(txt_importe.Text),
                            CodigoMoneda = int.Parse(dd_codMonedaGastos.SelectedValue),
                            AplicaIva = Boolean.Parse(dd_aplicaIVA.SelectedValue),
                        }
                    },
                    FacturaPosterior = Boolean.Parse(dd_facturaPost.SelectedValue),
                    // Factura
                    Glosa = txt_glosa.Text,
                    DetalleProductos = new List<DetalleProducto>
                    {
                        new DetalleProducto
                        {
                            NumeroItem = int.Parse(txt_numItem.Text),
                            CodigoProducto = txt_codProducto.Text,
                            Cantidad = decimal.Parse(txt_cantidad.Text),
                            CodigoUnidadMedida = int.Parse(txt_codUnidadMedida.Text),
                            PrecioUnitario = decimal.Parse(txt_precUnitario.Text),
                            ImporteDescuento = decimal.Parse(txt_importe_Descuentos.Text),
                            PorcentajeGasto = decimal.Parse(txt_PorcGasto.Text),
                            ImporteTotal = decimal.Parse(txt_cantidad.Text) * decimal.Parse(txt_precUnitario.Text)

                        }
                    }






                };
                var result = await na_registrarComprasApi.PostComprasAsync(compras, token);
            }
            catch (Exception ex) 
            {
                Response.Write($"Error: {ex.Message}");
            }

        }
    }
}