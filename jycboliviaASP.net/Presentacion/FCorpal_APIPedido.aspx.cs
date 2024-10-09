using jycboliviaASP.net.Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIpedido;

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
            var pedido = new PedidoDTO
            {
                NumeroPedido = 0,
                Fecha = DateTime.Now,
                Referencia = txt_Referencia.Text,
                CodigoCliente = int.Parse(txt_codCliente.Text),
                ImporteProductos = 30,
                ImporteDescuentos = decimal.Parse(txt_impDescuentos.Text),
                ImporteTotal = 30,
                Glosa = txt_glosa.Text,
                Usuario = "adm"
            };

            // obtener los detalles de productos
            var detalles = new List<ItemPedidoDTO>();
            int rowCount = Request.Form.AllKeys.Length;

            for (int i = 0; i < rowCount; i++)
            {
                if (Request.Form["item" + i] != null)
                {
                    var cantidad = decimal.Parse(Request.Form["cantidad" + i]);
                    var precioUnitario = decimal.Parse(Request.Form["precioUnitario" + i]);
                    var importeDescuento = decimal.Parse(Request.Form["importeDescuento" + i]);

                    var importeTotal = (cantidad * precioUnitario) -importeDescuento;

                    detalles.Add(new ItemPedidoDTO
                    {
                        NumeroItem = 1,
                        CodigoProducto = "PTMA02001",
                        Cantidad = 6,
                        CodigoUnidadMedida = 14,
                        PrecioUnitario = 5,
                        ImporteDescuento = 0,
                        ImporteTotal = 30
                    });
                }
            }
            pedido.DetalleProductos = detalles;

            //obtener token y enviar datos
            var api = new NA_APIpedido();
            var token = await api.GetTokenAsync("adm", "123");

            try
            {
                var result = await api.PostPedidoAsync(pedido, token);
                lblResult.Text = $"Numero Pedido: {result}";
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}')", true);
            }
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Pedido registrado Exitosamente.');", true);

        }
    }
}