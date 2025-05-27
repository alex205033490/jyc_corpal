using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIpedido;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_VaciadoUponPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(140) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                cargarDatosVaciadoUpon2("");
                //buscarDatosParaCargarAUpon("");
                string baseDeDatos = Session["BaseDatos"].ToString();
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha ingresado a Vaciado Pedido Upon");
            }
        }

        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(permiso, codUser);
        }

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaClienteP(string prefixText, int count)
        {
            string cliente = prefixText;

            NCorpal_Cliente cc = new NCorpal_Cliente();
            //      DataSet tuplas = Nrespon.mostrarSoloAutorizados_AutoComplit(nombreResponsable,"2,6,7,9,10,11,13");
            DataSet tuplas = cc.get_ClienteNombre(cliente);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }

            return lista;
        }

        /*private void buscarDatosParaCargarAUpon(string cliente)
        {
            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = nss.get_allPedidosParaVaciarUpon(cliente);
            lb_cantDatos.Text = Convert.ToString(tuplas.Tables[0].Rows.Count);
            gv_datosCobros.DataSource = tuplas;
            gv_datosCobros.DataBind();
        }*/

        private void anularPedido()
        {
            foreach (GridViewRow row in gv_datosCobros.Rows)
            {
                NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();

                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.RowState = DataControlRowState.Edit;
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked == true)
                    {
                        int CodigoPedido = int.Parse(row.Cells[1].Text);
                        bool bandera = nss.anularPedidoVaciadoUpon(CodigoPedido, isChecked);
                    }
                }
            }
            cargarDatosVaciadoUpon2("");
        }
        protected void bt_anularPago_Click(object sender, EventArgs e)
        {
            anularPedido();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            exceldelaTabla();
        }

        private void exceldelaTabla()
        {
            string cliente =tx_cliente.Text;
            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = nss.get_allPedidosParaVaciarUpon(cliente);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Vaciado Upon Pedido - " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = tuplas;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }

        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string cliente = tx_cliente.Text;
            cargarDatosVaciadoUpon2(cliente);
        }








        //------------------------------------------------- P2  VACIADO UPON
        private void cargarDatosVaciadoUpon2 (string cliente)
        {
            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = nss.get_allPedidoVWParaVaciado(cliente);
            lb_cantDatos.Text = Convert.ToString(tuplas.Tables[0].Rows.Count);
            gv_datosCobros.DataSource = tuplas;
            gv_datosCobros.DataBind();
        }
        
        protected async void bt_vaciarAlSimec_Click(object sender, EventArgs e)
        {
            List<PedidoDTO> pedidoAEnviar = ObtenerPedidosSeleccionados();

            if (pedidoAEnviar.Count == 0)
            {
                showalert("No se seleccionaron pedidos.");
                return;
            }
            string token = await ObtenerTokenAsync("adm", "Corpal205010180");

            if (string.IsNullOrEmpty(token))
            {
                showalert("Error al obtener el token");
                return;
            }

            foreach (var pedido in pedidoAEnviar)
            {
                string resultado = await EnviarPedidoAsync(pedido, token);

                if (resultado.StartsWith("Error"))
                {
                    showalert($"Error al vaciar el pedido : {resultado}");
                    continue;
                }
                anularPedido();
                showalert($"Vaciado completado. Codigo: {resultado}");
            }
        }

        private List<PedidoDTO> ObtenerPedidosSeleccionados() //1
        {
            List<PedidoDTO> pedidos = new List<PedidoDTO>();
            foreach (GridViewRow row in gv_datosCobros.Rows)
            {
                CheckBox chkSeleccionar = (CheckBox)row.FindControl("chkAll");
                if (chkSeleccionar != null && chkSeleccionar.Checked)
                {
                    PedidoDTO pedido = CrearPedidoDesdeFila(row);

                    if(pedido != null)
                    {
                        pedidos.Add(pedido);
                    }
                }
            }
            return pedidos;
        }

        private PedidoDTO CrearPedidoDesdeFila(GridViewRow row) //pedido
        {
            try
            {
                DateTime Actual = DateTime.Now;
                string fechaHoraActual = Actual.ToString("dd/MM/yyyy HH:mm");

                string referencia = $"Referencia {fechaHoraActual} ";
                string glosa = $"Glosa {fechaHoraActual} ";

                int codSolicitud = Convert.ToInt32(row.Cells[1].Text);
                string fecha = "2025-03-31T00:00:00";
                int codigoCliente = Convert.ToInt32(row.Cells[5].Text);
                decimal importeProductos = Convert.ToDecimal(row.Cells[7].Text);
                decimal importeTotal = Convert.ToDecimal(row.Cells[7].Text);
                List<ItemPedidoDTO> detalles = ObtenerDetalleProducto(codSolicitud);
                string usuario = "adm";

                if (detalles == null || detalles.Count == 0)
                {
                    showalert($"Pedido sin detalles. No se enviará. codSolicitud: {codSolicitud}");
                    return null;
                }


                return new PedidoDTO
                {
                    NumeroPedido = 0,
                    Fecha = fecha,
                    Referencia = referencia,
                    CodigoCliente = codigoCliente,
                    ImporteProductos = importeProductos,
                    ImporteDescuentos = 0,
                    ImporteTotal = importeTotal,
                    Glosa = glosa,
                    DetalleProductos = detalles,
                    Usuario = usuario,
                  
                };         
            } catch (Exception ex)
            {
                showalert($"Error al crear pedido: {ex.Message}");
                return null;
            }
        }
        private async Task<string> EnviarPedidoAsync(PedidoDTO pedido, string token)
        {
            try
            {
                NA_APIpedido negocio = new NA_APIpedido();
                return await negocio.PostPedidoAsync(pedido, token);
            } 
            catch(Exception ex)
            {
                Debug.WriteLine($"Error al enviar pedido: {ex.Message}");
                return string.Empty;
            }
        }
        private List<ItemPedidoDTO> ObtenerDetalleProducto(int codsolicitud)
        {
            List<ItemPedidoDTO> detalles = new List<ItemPedidoDTO>();
            try
            {
                NCorpal_SolicitudEntregaProducto negocio = new NCorpal_SolicitudEntregaProducto();
                DataSet dsDetalles = negocio.get_ObtenerDetalleProductoAsync(codsolicitud);

                if (dsDetalles != null && dsDetalles.Tables.Count > 0)
                {
                    foreach(DataRow row in dsDetalles.Tables[0].Rows)
                    {
                        ItemPedidoDTO item = MapItemPedido(row);
                        detalles.Add(item);
                    }
                }
                else
                {
                    showalert($"No se encontraron detalles para el producto.");
                }
            } catch (Exception ex)
            {
                showalert($"Error al obtener detalles del producto: {ex.Message}");
            }
            return detalles;
        }
        private ItemPedidoDTO MapItemPedido(DataRow row)
        {
            try
            {
                string codigoProd = row["CodigoProducto"].ToString();
                decimal cantidad = Convert.ToDecimal(row["cantidad"]);
                int unidadMedida = Convert.ToInt32(row["CodigoUnidadMedida"]);
                decimal precioUnitario = Convert.ToDecimal(row["PrecioUnitario"]);
                decimal importeTotal = Convert.ToDecimal(row["ImporteTotal"]);

                return new ItemPedidoDTO
                {
                    CodigoProducto = codigoProd,
                    Cantidad = cantidad,
                    CodigoUnidadMedida = unidadMedida,
                    PrecioUnitario = precioUnitario,//precioUnitario,
                    ImporteDescuento = 0,//importeTotal
                    ImporteTotal = importeTotal
                };

            } catch (Exception ex)
            {
                Debug.WriteLine($"Error al mapear el item: {ex.Message}");
                return null;
            }
        }
        //------------  OBTENER TOKEN
        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            try
            {
                NA_APIpedido negocio = new NA_APIpedido();
                return await negocio.GetTokenAsync(usuario, password);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener el token: {ex.Message}");
                return string.Empty;
            }
        }
        private void showalert(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{mensaje}');", true);
        }

    }
}
    
