using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIproductos.ApiResponseProd;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static jycboliviaASP.net.Negocio.NA_APIproductos;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Data;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using DataGrid = System.Web.UI.WebControls.DataGrid;
using jycboliviaASP.net.NegocioApi;


namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_VaciadoUponVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();



            if (tienePermisoDeIngreso(141) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                buscarDatosParaCargarAUpon("");
                string baseDeDatos = Session["BaseDatos"].ToString();
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha ingresado a Vaciado Pedido Upon");
            }
        }

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaClienteV(string prefixText, int count)
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


        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(permiso, codUser);
        }


        private void buscarDatosParaCargarAUpon(string cliente)
        {
            NCorpal_Venta nss = new NCorpal_Venta();
            DataSet tuplas = nss.get_allVentasParaVaciarUpon(cliente);
            lb_cantDatos.Text = Convert.ToString(tuplas.Tables[0].Rows.Count);
            gv_datosCobros.DataSource = tuplas;
            gv_datosCobros.DataBind();
        }

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
            buscarDatosParaCargarAUpon("");
        }


        private void anularVenta()
        {
            foreach (GridViewRow row in gv_datosCobros.Rows)
            {
                NCorpal_Venta nss = new NCorpal_Venta();

                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.RowState = DataControlRowState.Edit;
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked == true)
                    {
                        int CodigoVendido = int.Parse(row.Cells[1].Text);
                        bool bandera = nss.anularVendidoVaciadoUpon(CodigoVendido, isChecked);
                    }
                }
            }
            buscarDatosParaCargarAUpon("");
        }

        protected void bt_anularPago_Click(object sender, EventArgs e)
        {
            anularVenta();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            exceldelaTabla();
        }

        private void exceldelaTabla()
        {
            string cliente = tx_cliente.Text;
            NCorpal_Venta nss = new NCorpal_Venta();
            DataSet tuplas = nss.get_allVentasParaVaciarUpon(cliente);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Vaciado Upon Vendido - " + Session["BaseDatos"].ToString();
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

        protected void bt_vaciarAlSimec_Click(object sender, EventArgs e)
        {
            string usuario = tx_usuarioUpon.Text;
            string password = tx_passUpon.Text;
            vaciaraUponVentasRealizadas(usuario, password);
        }

        private async void vaciaraUponVentasRealizadas(string usuario, string password)
        {
            foreach (GridViewRow row in gv_datosCobros.Rows)
            {
                NCorpal_Venta nss = new NCorpal_Venta();
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.RowState = DataControlRowState.Edit;
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked == true)
                    {
                        int CodigoVendido = int.Parse(row.Cells[1].Text);
                        DataSet tuplaVenta = nss.get_ventaRealizadaparaVaciar(CodigoVendido);
                        DataSet tuplasItemVendido = nss.get_ItemVendidos(CodigoVendido);

                        int codigo = CodigoVendido;
                        int NumeroVenta = 0;
                        //int.TryParse(tuplaVenta.Tables[0].Rows[0][0].ToString(), out NumeroVenta);
                        int NumeroPedido = 0;
                        //int.TryParse(tuplaVenta.Tables[0].Rows[0][0].ToString(), out NumeroPedido);
                        DateTime Fecha;
                        DateTime.TryParse("2024-11-30", out Fecha);
                        // DateTime.TryParse(tuplaVenta.Tables[0].Rows[0][2].ToString(), out Fecha);
                        int CodigoCliente;
                        int.TryParse(tuplaVenta.Tables[0].Rows[0][3].ToString(), out CodigoCliente);
                        
                        string Referencia = tuplaVenta.Tables[0].Rows[0][4].ToString();
                        string Glosa = tuplaVenta.Tables[0].Rows[0][4].ToString();
                        bool EmitirFactura = Convert.ToBoolean(tuplaVenta.Tables[0].Rows[0][31].ToString());
                        decimal ImporteProductos;
                        decimal.TryParse(tuplaVenta.Tables[0].Rows[0][22].ToString().Replace(".", ","), out ImporteProductos);
                        decimal ImporteDescuentos;
                        decimal.TryParse(tuplaVenta.Tables[0].Rows[0][27].ToString().Replace(".", ","), out ImporteDescuentos);
                        decimal ImporteTotal;
                        decimal.TryParse(tuplaVenta.Tables[0].Rows[0][22].ToString().Replace(".", ","), out ImporteTotal);
                        decimal Cobros_TotalEfectivo;
                        decimal.TryParse(tuplaVenta.Tables[0].Rows[0][22].ToString().Replace(".", ","), out Cobros_TotalEfectivo);
                        decimal Cobros_TotalDeposito;
                        decimal.TryParse(tuplaVenta.Tables[0].Rows[0][22].ToString().Replace(".", ","), out Cobros_TotalDeposito);
                        int Factura_TipoDocumentoIdentidad;
                        int.TryParse(tuplaVenta.Tables[0].Rows[0][18].ToString(), out Factura_TipoDocumentoIdentidad);
                        string Factura_NIT_CI = tuplaVenta.Tables[0].Rows[0][19].ToString();
                        string Factura_Complemento = tuplaVenta.Tables[0].Rows[0][19].ToString();
                        string Factura_RazonSocial = tuplaVenta.Tables[0].Rows[0][17].ToString();
                        string Factura_Telefono = tuplaVenta.Tables[0].Rows[0][9].ToString();
                        string Factura_Email = tuplaVenta.Tables[0].Rows[0][5].ToString();
                        int Factura_MetodoPago;
                        int.TryParse(tuplaVenta.Tables[0].Rows[0][20].ToString(), out Factura_MetodoPago);

                        NUpon_Ventas nupon = new NUpon_Ventas();
                        NA_endpoints napi = new NA_endpoints();
                        List<DetalleProductoV> listaProductosV = new List<DetalleProductoV>();

                        bool errorDatos = false;

                        for (int i = 0; i < tuplasItemVendido.Tables[0].Rows.Count; i++)
                        {
                            int DetProd_NumeroItem;
                            int.TryParse((i + 1).ToString(), out DetProd_NumeroItem);
                            string DetProd_CodigoProducto = tuplasItemVendido.Tables[0].Rows[0][9].ToString();
                            string cantidadAux = tuplasItemVendido.Tables[0].Rows[0][3].ToString();
                            decimal resultado = Math.Ceiling(decimal.Parse(cantidadAux));
                            int DetProd_Cantidad;
                            int.TryParse(resultado.ToString(), out DetProd_Cantidad);
                            int DetProd_CodigoUnidadMedida;
                            int.TryParse(tuplasItemVendido.Tables[0].Rows[0][10].ToString(), out DetProd_CodigoUnidadMedida);

                            string criterioBusqueda = DetProd_CodigoProducto.Trim();
                            
                            if (string.IsNullOrEmpty(criterioBusqueda)) {
                                errorDatos = true;
                                break;
                            }

                            string token = await ObtenerTokenAsync(usuario, password);
                            var BuscProducto = new NA_APIproductos();
                            productoCodigoGet product = await BuscProducto.get_ProductoCodigoAsync(usuario, password, criterioBusqueda);
                            //productoCriterioGet product = productos[0];

                            DetalleProductoV Item = new DetalleProductoV();
                            Item.NumeroItem = DetProd_NumeroItem;
                            Item.CodigoProducto = criterioBusqueda;
                            Item.Cantidad = DetProd_Cantidad;
                            Item.CodigoUnidadMedida = 14;//product.UnidadMedida;
                            Item.PrecioUnitario = 1;//product.PrecioUnitario;
                            //Item.ImporteDescuento = product.DescuentosPermitido;
                            Item.ImporteDescuento = 0;
                            Item.ImporteTotal = (1 * DetProd_Cantidad);
                            Item.NumeroItemOrigen = DetProd_NumeroItem;
                            listaProductosV.Add(Item);
                            /** -------------- datos para sumar --------- */
                            decimal montoUnitario;
                            decimal.TryParse(("1").ToString(), out montoUnitario);
                            ImporteProductos = ImporteProductos + montoUnitario;
                            ImporteTotal = ImporteTotal + montoUnitario;
                            Cobros_TotalEfectivo = Cobros_TotalEfectivo + montoUnitario;

                            /** -------------- datos para sumar --------- */
                        }

                        if (errorDatos == false)
                        {
                            bool banderaAut = napi.get_AutenticarUsuario(usuario, password);
                            if (banderaAut)
                            {
                                string Usuario = usuario;
                                string token = napi.get_TokenUsuario(usuario, password);
                                bool banderaUpon = napi.insertarVentas2(token, NumeroVenta, NumeroPedido, Fecha, CodigoCliente,
                                    Referencia, Glosa, EmitirFactura, ImporteProductos, ImporteDescuentos, ImporteTotal, Cobros_TotalEfectivo,
                                    Cobros_TotalDeposito, Factura_TipoDocumentoIdentidad, Factura_NIT_CI, Factura_Complemento, Factura_RazonSocial,
                                    Factura_Telefono, Factura_Email, Factura_MetodoPago, Usuario, listaProductosV);
                                if (banderaUpon == true)
                                {
                                    bool bandera = nupon.updateVaciadoOk(CodigoVendido);
                                    showalert("Se ha vaciado Correctamente!");
                                }
                                else
                                {
                                    showalert($"Error al vaciar el registro.");
                                }
                            }
                        }
                        else {
                            Console.WriteLine("Error de guardar.");
                        }
                    }
                }
            }
            buscarDatosParaCargarAUpon("");
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string cliente = tx_cliente.Text;
            buscarDatosParaCargarAUpon(cliente);
        }

        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            NA_APIproductos negocio = new NA_APIproductos();
            return await negocio.GetTokenAsync(usuario, password);
        }

        protected void cb_selecciontodo_CheckedChanged(object sender, EventArgs e)
        {
            seleccionarTodo();
        }

        private void seleccionarTodo()
        {
            bool seleccionado = cb_selecciontodo.Checked;           
            foreach (GridViewRow row in gv_datosCobros.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.RowState = DataControlRowState.Edit;
                    if (seleccionado == true) {
                        row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = true;
                    }else
                        row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = false;

                }
            }
        }

        private void showalert(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{mensaje}');", true);
        }
    }
}
      

