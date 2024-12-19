using jycboliviaASP.net.Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIpedido;
using System.Globalization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIPedido : System.Web.UI.Page
    {
        private static readonly HttpClient httpClient = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {
        }
  
        //--------------------------- GET - PEDIDO CON CRITERIO
        protected async void btn_buscarPedidoCriterio_Click(object sender, EventArgs e)
        {
            string numPedido = TextBox1.Text.Trim();

            if (!IsValidPedido(numPedido))
            {
                LimpiarGridViews();
                showAlert("Por favor ingrese un número de pedido válido.");
                return;
            }

            await BuscarPedidoPorCriterioAsync(numPedido);
        }
        private async Task BuscarPedidoPorCriterioAsync(string numPedido)
        {
            try
            {
                NA_APIpedido negocio = new NA_APIpedido();
                PedidoDTO pedidos = await negocio.ObtenerPedidoCriterioAsync("adm", "123", numPedido);

                if (pedidos != null)
                {
                    ActualizarGridViews(pedidos);
                }
                else
                {
                    LimpiarGridViews();
                    showAlert("No se encontraron registros con el número de pedido proporcionado.");
                }
            } catch (Exception ex)
            {
                showAlert($"No se encontro registros con el codigo: {numPedido}. {ex.Message}");
                LimpiarGridViews();
            }
        }
        private void ActualizarGridViews(PedidoDTO pedidos)
        {
            gv_pedidoCriterio.DataSource = new List<PedidoDTO> { pedidos };
            gv_pedidoCriterio.DataBind();

            gv_detalleProd.DataSource = pedidos.DetalleProductos != null && pedidos.DetalleProductos.Count > 0
                ? pedidos.DetalleProductos
                : null;
            gv_detalleProd.DataBind();
        }
        private void LimpiarGridViews()
        {
            gv_pedidoCriterio.DataSource = null;
            gv_detalleProd.DataSource = null;
            gv_pedidoCriterio.DataBind();
            gv_detalleProd.DataBind();
        }
        private bool IsValidPedido(string numPedido)
        {
            return !string.IsNullOrEmpty(numPedido);
        }

        //--------------------------- GET - PEDIDO
        protected async void btn_buscarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                string numPedido = TextBox2.Text.Trim();
                List<pedidoDTO2> pedido = await obtenerPedidoAsync(numPedido);

                ActualizarGridView(pedido);

            } catch (Exception ex)
            {
                showAlert($"Ocurrio un error al buscar el pedido: {ex.Message}");
                LimpiarGridView();
            }
        }
        private async Task<List<pedidoDTO2>> obtenerPedidoAsync(string numPedido)
        {
            try
            {
                NA_APIpedido negocio = new NA_APIpedido();
                return await negocio.ObtenerPedidoAsync("adm", "123", numPedido);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pedidos de la API", ex);
            }

        }
        private void ActualizarGridView(List<pedidoDTO2> pedido)
        {
            if(pedido != null && pedido.Count > 0)
            {
                gv_pedido.DataSource = pedido;
                gv_pedido.DataBind();
            }
            else
            {
                gv_pedido.DataSource = new List<pedidoDTO2>();
                gv_pedido.DataBind();
                showAlert($"No se encontraron registros con el código: {pedido}.");
            }
        }
        private void LimpiarGridView()
        {
            gv_pedido.DataSource = new List<pedidoDTO2>();
            gv_pedido.DataBind();
        }
        //--------------------------- POST PEDIDO
        protected async void btn_PostPedido_Click(object sender, EventArgs e)
        {
            string codCliente = txt_codCliente.Text.Trim();

            // Validaciones

            if (string.IsNullOrEmpty(codCliente))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, complete el campo codigo cliente.');", true);
                return;
            }


            var pedido = new PedidoDTO
            {
                NumeroPedido = 0,
                Fecha = "2024-11-30T00:00:00",
                Referencia = txt_Referencia.Text,
                CodigoCliente = int.Parse(txt_codCliente.Text),
                ImporteProductos = 0,
                ImporteDescuentos = decimal.Parse(txt_impDescuentos.Text),
                ImporteTotal = 0,
                Glosa = txt_glosa.Text,
                Usuario = "adm"
            };

            // obtener los detalles de productos
            var detalles = new List<ItemPedidoDTO>();
            decimal totalImporteProductos = 0;

            int rowCount = Request.Form.AllKeys.Length;

            for (int i = 0; i < rowCount; i++)
            {
                if (Request.Form["codigoProducto" + i] != null)
                {
                    decimal cantidad = decimal.Parse(Request.Form["cantidad" + i], CultureInfo.InvariantCulture);
                    decimal precioUnitario = decimal.Parse(Request.Form["precioUnitario" + i], CultureInfo.InvariantCulture);
                    decimal importeDescuento = decimal.Parse(Request.Form["importeDescuento" + i], CultureInfo.InvariantCulture);

                    decimal importeTotal = (cantidad * precioUnitario)-importeDescuento;

                    var detalle = new ItemPedidoDTO
                    {
                        NumeroItem = 0,
                        CodigoProducto = (Request.Form["codigoProducto" + i]),
                        Cantidad = cantidad,
                        CodigoUnidadMedida = int.Parse(Request.Form["codigoUnidadMedida" + i]),
                        PrecioUnitario = precioUnitario,
                        ImporteDescuento = importeDescuento,
                        ImporteTotal = importeTotal
                    };
                    detalles.Add(detalle);
                    totalImporteProductos += importeTotal;
                }
            }

            pedido.DetalleProductos = detalles;

            pedido.ImporteProductos = totalImporteProductos;
            pedido.ImporteTotal = totalImporteProductos - pedido.ImporteDescuentos;


            //obtener token y enviar datos
            var api = new NA_APIpedido();
            var token = await api.GetTokenAsync("adm", "123");

            try
            {
                var result = await api.PostPedidoAsync(pedido, token);
                
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Pedido registrado Exitosamente.');", true);

                txt_Referencia.Text = string.Empty;
                txt_codCliente.Text = string.Empty;
                txt_impDescuentos.Text = string.Empty; 
                txt_glosa.Text = string.Empty; 
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}')", true);
            }
        }

        //--------------------------

        private void showAlert(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }
    }
}