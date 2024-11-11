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

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //--------------------------- GET - PEDIDO CON CRITERIO
        protected async void btn_buscarPedidoCriterio_Click(object sender, EventArgs e)
        {

            string numPedido = (TextBox1.Text.Trim());
            if(string.IsNullOrEmpty(numPedido))
            {
                gv_pedidoCriterio.DataBind();
                gv_detalleProd.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor ingrese un numero de pedido valido.');", true);
                return;
            }

            try
            {
                NA_APIpedido apiPedido = new NA_APIpedido();
                PedidoDTO pedido = await apiPedido.ObtenerPedidoCriterioAsync("adm", "123", numPedido);

                if(pedido != null)
                {
                    var pedidoGet = new List<PedidoDTO> { pedido };
                    gv_pedidoCriterio.DataSource = pedidoGet;
                    gv_pedidoCriterio.DataBind();

                    if (pedido.DetalleProductos != null && pedido.DetalleProductos.Count > 0)
                    {
                        gv_detalleProd.DataSource = pedido.DetalleProductos;
                        gv_detalleProd.DataBind();
                    }
                    else
                    {
                        gv_detalleProd.DataSource = null;
                        gv_detalleProd.DataBind();
                    }
                }    
                else
                {
                    gv_pedidoCriterio.DataSource = null;
                    gv_detalleProd.DataSource = null;

                    gv_pedidoCriterio.DataBind();
                    gv_detalleProd.DataBind();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron registros con el numero de pedido proporcionado.');", true);
                }
            }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }

        //--------------------------- GET - PEDIDO
        protected async void btn_buscarPedido_Click(object sender, EventArgs e)
        {
            string numPedido = TextBox2.Text.Trim();

            NA_APIpedido apiPedido = new NA_APIpedido();
            List<pedidoDTO2> pedidos = await apiPedido.ObtenerPedidoAsync("adm", "123", numPedido);

            if (pedidos != null && pedidos.Count > 0)
            {
                gv_pedido.DataSource = pedidos;
                gv_pedido.DataBind();
            }
            else
            {
                gv_pedido.DataSource = new List<pedidoDTO2>();
                gv_pedido.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron registros con el codigo proporcionado.');", true);
            }
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
                Fecha = DateTime.Now,
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

        
    }
}