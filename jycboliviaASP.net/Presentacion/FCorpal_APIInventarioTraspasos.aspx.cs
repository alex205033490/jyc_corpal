using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIinventario;
using static jycboliviaASP.net.Negocio.NA_APIAlmacen;
using static jycboliviaASP.net.Negocio.NA_APIproductos;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIInventarioTraspasos : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = await ObtenerTokenAsync("adm", "123");
                var almacen = await ObtenerListCodAlmacen(token);

                dd_codAlmacenDestino.DataSource = almacen;
                dd_codAlmacenDestino.DataTextField = "Nombre";
                dd_codAlmacenDestino.DataValueField = "CodigoAlmacen";
                dd_codAlmacenDestino.DataBind();
                dd_codAlmacenDestino.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un almacén", ""));

                List<Productos> productos = ObtenerProductosDesdeSession();
                gv_productAgregados.DataSource = productos;
                gv_productAgregados.DataBind();
            }
        }

        //--------------------------------------        GET - INVENTARIO TRASPASO
        protected async void btn_GetinvTraspaso_Click(object sender, EventArgs e)
        {
            try
            {
                string criterio = TextBox1.Text.Trim();
                List<InvTraspasoDTO> traspasos = await ObtenerTraspasosAsync(criterio);
                actualizarVwTraspaso(traspasos);

            } catch (Exception ex)
            {
                showAlert("Ocurrió un error al realizar la búsqueda. Intente nuevamente más tarde.:");
                LogError(ex);
            }

        }
        private void actualizarVwTraspaso(List<InvTraspasoDTO> traspasos)
        {
            gv_invTraspaso.DataSource = null;
            gv_invTraspaso.DataBind();

            if (traspasos != null && traspasos.Count > 0)
            {
                gv_invTraspaso.DataSource = traspasos;
                gv_invTraspaso.DataBind();
            }
            else
            {
                gv_invTraspaso.DataSource = traspasos;
                gv_invTraspaso.DataBind();
                showAlert("No se encontraron registros con el código proporcionado");
            }
        }
        private async Task<List<InvTraspasoDTO>> ObtenerTraspasosAsync(string criterio)
        {
            try
            {
                NA_APIinventario negocio = new NA_APIinventario();
                var result = await negocio.ObtenerTraspasoAsync("adm", "123", criterio);

                if (result == null)
                {
                    throw new Exception("La llamada a la API no devolvió resultados.");
                }
                return result;
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }


        //-------------------------------------      GET - INVENTARIO TRASPASO DETALLE
        protected async void btn_GetinvTraspasoDet_Click(object sender, EventArgs e)
        {
            try
            {
                string numTransaccion = TextBox2.Text.Trim();

                if (!IsValidTransactionnumber(numTransaccion))
                {
                    showAlert("Por favor ingrese un número de transacción válido.");
                    LimpiarGvDet();
                    return;
                }
                await CargarDetallesTraspasosDet(numTransaccion);

            } catch (Exception ex)
            {
                showAlert($"Ocurrio un error: {ex.Message}");
                LimpiarGvDet();
            }

        }
        private bool IsValidTransactionnumber(string numTransaccion)
        {
            return !string.IsNullOrEmpty(numTransaccion);
        }
        private async Task CargarDetallesTraspasosDet(string numTransaccion)
        {
            try
            {
                NA_APIinventario negocio = new NA_APIinventario();
                InventarioTraspasoDTO traspaso = await negocio.GetInventarioTraspasoDetAsync("adm", "123", numTransaccion);

                if (traspaso != null)
                {
                    CargarGvDet(traspaso);
                }
                else
                {
                    LimpiarGvDet();
                    showAlert("No se encontraron registros con el número de transacción proporcionado.");
                }
            } catch (Exception ex)
            {
                showAlert($"{ex.Message}");
                LimpiarGvDet();
            }
        }
        private void CargarGvDet(InventarioTraspasoDTO traspaso)
        {
            try
            {
                var invTras = new List<InventarioTraspasoDTO> { traspaso };
                gv_invTraspasoDet.DataSource = invTras;
                gv_invTraspasoDet.DataBind();

                if (traspaso.DetalleProductos != null && traspaso.DetalleProductos.Count > 0)
                {
                    gv_invTraspasoDet2.DataSource = traspaso.DetalleProductos;
                    gv_invTraspasoDet2.DataBind();
                }
                else
                {
                    gv_invTraspasoDet2.DataSource = null;
                    gv_invTraspasoDet2.DataBind();
                }
            } catch (Exception ex)
            {
                showAlert($"Error al cargar los datos en el Grid :{ex.Message}");
            }

        }
        private void LimpiarGvDet()
        {
            gv_invTraspasoDet.DataSource = null;
            gv_invTraspasoDet2.DataSource = null;
            gv_invTraspasoDet.DataBind();
            gv_invTraspasoDet2.DataBind();
        }


        //-------------------------------------     POST - INVENTARIO TRASPASO




        // -- DD ALMACENES
        private async Task<List<ListAlmacenesDTO>> ObtenerListCodAlmacen(string token)
        {
            try
            {
                NA_APIAlmacen negocio = new NA_APIAlmacen();
                return await negocio.Get_ListAlmacenAsync(token);

            } catch (Exception ex)
            {
                showAlert($"Error al obtener la lista de almacenes");
                return null;
            }

        }







        // - - - - - - - - - - - - - - - - - - - - - - 
        private void showAlert(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }
        private void LogError(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message} \n {ex.StackTrace}");
        }
        private async Task<string> ObtenerTokenAsync(string usu, string pass)
        {
            NA_APIinventario negocio = new NA_APIinventario();
            return await negocio.GetTokenAsync(usu, pass);
        }

        protected void txt_producto_TextChanged(object sender, EventArgs e)
        {
            string criterio = txt_producto.Text.Trim();
            if (!string.IsNullOrEmpty(criterio))
            {
                cargarProductosUpon(criterio);
            }
        }

        private async void cargarProductosUpon(string criterio)
        {
            try
            {
                string token = await ObtenerTokenAsync("adm", "123");

                NA_APIproductos negocio = new NA_APIproductos();
                List<productoCriterioGet> productos = await negocio.get_ProductoCriterioAsync(token, criterio);

                gv_listProdTraspaso.DataSource = productos;
                gv_listProdTraspaso.DataBind();

                gv_listProdTraspaso.Visible = productos.Count > 0;
            }
            catch (Exception ex)
            {
                showAlert($"Error al realizar la busqueda: {ex.Message}");
            }
        }

        protected void gv_listProdTraspaso_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_listProdTraspaso.SelectedIndex;
            GridViewRow row = gv_listProdTraspaso.Rows[index];

            string codigoProducto = row.Cells[1].Text;
            string nombreProducto = row.Cells[2].Text;
            string unidadMedidaProducto = row.Cells[3].Text;

            txt_producto.Text = nombreProducto;
            Session["STnombreProducto"] = nombreProducto;
            Session["STcodigoProducto"] = codigoProducto;
            Session["STunidadMedidaProducto"] = unidadMedidaProducto;

            gv_listProdTraspaso.Visible = false;
        }

        protected void btn_addProd_Click(object sender, EventArgs e)
        {
            Productos newProducto = CrearNuevoProducto();
            if(newProducto != null)
            {
                List<Productos> listaProductos = ObtenerProductosDesdeSession();

                listaProductos.Add(newProducto);

                Session["ProductosTraspaso"] = listaProductos;

                gv_productAgregados.DataSource = listaProductos;
                gv_productAgregados.DataBind();

                LimpiarCamposAddProductos();
            }
            else
            {
                showAlert("Ingrese un producto valido");
            }

        }


        List<Productos> productos = new List<Productos>();
        public class Productos
        {
            public string Producto { get; set; }
            public string CodigoProducto {  get; set; }
            public int UnidadMedida {  get; set; }
            public decimal cantidad {  get; set; }
        }
        private List<Productos> ObtenerProductosDesdeSession()
        {
            List<Productos> productos = Session["ProductosTraspaso"] as List<Productos>;
            if(productos == null)
            {
                productos = new List<Productos>();
            }
            return productos;
        }
        private Productos CrearNuevoProducto()
        {
            try
            {
                string producto = txt_producto.Text.Trim();
                if (string.IsNullOrEmpty(producto))
                {
                    showAlert("Debe buscar y seleccionar el nombre de un producto.");
                    return null;
                }

                if (Session["STcodigoProducto"] == null || string.IsNullOrWhiteSpace(Session["STcodigoProducto"].ToString()))
                {
                    showAlert("El codigo del producto no esta disponible en la a sessión");
                    return null;
                }
                string codigo = (Session["STcodigoO"].ToString());

                if (Session["STcodigoUnidadMedida"] == null || string.IsNullOrWhiteSpace(Session["STcodigoProducto"].ToString()))
                {
                    showAlert("La unidad medida del producto no esta disponible en la sessión");
                    return null; 
                }
                int codigoUnidadMedida = int.Parse(Session["STcodigoUnidadMedida"].ToString());

                decimal cantidad = 0;
                if(!decimal.TryParse(txt_cantProducto.Text, out cantidad) || cantidad <= 0)
                {
                    showAlert("La cantidad del producto debe ser un número nayor que cero.");
                    return null;
                }

                return new Productos
                {
                    CodigoProducto = codigo,
                    Producto = producto,
                    UnidadMedida = codigoUnidadMedida,
                    cantidad = cantidad
                };
            } catch(Exception ex)
            {
                showAlert($"Error al crear el producto: {ex.Message}");
                return null;
            }
        }

        private void LimpiarCamposAddProductos()
        {
            txt_producto.Text = string.Empty;
            txt_cantProducto.Text = string.Empty;

            Session.Remove("STnombreProducto");
            Session.Remove("STcodigoProducto");
            Session.Remove("STunidadMedidaProducto");
        }

        private void ActualizarGVProductosADD(List<Productos> productos)
        {
            gv_productAgregados.DataSource = productos;
            gv_productAgregados.DataBind();
        }
    }
}


