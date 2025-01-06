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
        protected async void btn_registrarTraspaso_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    return; 
                }
                List<Productos> productos = ObtenerProductosDesdeSession();

                if (!ValidarProductos(productos))
                {
                    showAlert("Tu lista de productos esta vacia.");
                    return;
                }

                InventarioTraspasoDTO traspaso = CrearTraspaso(productos);

                string token = await ObtenerTokenAsync("adm", "123");

                string resultado = await RegistrarTraspasoAsync(traspaso, token);

                showAlert($"Traspaso Registrado. Nro de registro: {resultado}");

                LimpiarGvDet();
                LimpiarCamposAddProductos();

            } 
            catch (Exception ex)
            {
                showAlert($"Error al registrar el traspaso: {ex.Message}");
            }
        }

        private InventarioTraspasoDTO CrearTraspaso(List<Productos> productos)
        {
            try
            {
                return new InventarioTraspasoDTO
                {
                    NumeroTraspasos = 0,
                    Fecha = "2024-11-30T00:00:00",
                    Referencia = txt_Referencia.Text,
                    CodigoAlmacenDestino = int.Parse(dd_codAlmacenDestino.SelectedValue),
                    Glosa = txt_glosa.Text,
                    DetalleProductos = productos.Select(p => new DetalleProductoTraspasoDTO
                    {
                        Item = 0,
                        CodigoProducto = p.codigoProducto,
                        UnidadMedida = p.unidadMedida,
                        Cantidad = p.cantidad,
                    }).ToList(),
                    Usuario = "adm"
                };
            } catch (Exception ex)
            {
                showAlert($"Error: {ex.Message}");
                return null;
            }
        }

        private async Task<string> RegistrarTraspasoAsync(InventarioTraspasoDTO traspaso, string token)
        {
            try
            {
                NA_APIinventario negocio = new NA_APIinventario();

                string resultado = await negocio.PostInventarioTraspasoAsync(traspaso, token);
                return resultado;
            }
            catch (Exception ex)
            {
                showAlert($"Error al registrar el traspaso en el sistema. {ex.Message}");
                return null;
            }
        }

        // - - LISTAR DD ALMACENES
        private async Task<List<ListAlmacenesDTO>> ObtenerListCodAlmacen(string token)
        {
            try
            {
                NA_APIAlmacen negocio = new NA_APIAlmacen();
                var almacenes = await negocio.Get_ListAlmacenAsync(token);
                var almacenesOrdenado = almacenes.OrderBy(a => a.Nombre).ToList();
                return almacenesOrdenado;

            } catch (Exception ex)
            {
                showAlert($"Error al obtener la lista de almacenes: {ex.Message}");
                return null;
            }
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - TXT Producto
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

        // - - - - - - - ADD PRODUCTOS AL GV
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
            public string producto { get; set; }
            public string codigoProducto {  get; set; }
            public int unidadMedida {  get; set; }
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
                if (Session["STnombreProducto"] == null || string.IsNullOrWhiteSpace(Session["STnombreProducto"].ToString()))
                {
                    showAlert("Debe buscar y seleccionar un producto.");
                    return null;
                }
                string producto = Session["STnombreProducto"].ToString();

                if (Session["STcodigoProducto"] == null || string.IsNullOrWhiteSpace(Session["STcodigoProducto"].ToString()))
                {
                    showAlert("El codigo del producto no esta disponible en la a sessión");
                    return null;
                }
                string codigo = (Session["STcodigoProducto"].ToString());

                if (Session["STunidadMedidaProducto"] == null || string.IsNullOrWhiteSpace(Session["STunidadMedidaProducto"].ToString()))
                {
                    showAlert("La unidad medida del producto no esta disponible en la sessión");
                    return null; 
                }
                int codigoUnidadMedida = int.Parse(Session["STunidadMedidaProducto"].ToString());

                decimal cantidad = 0;
                if(!decimal.TryParse(txt_cantProducto.Text, out cantidad) || cantidad <= 0)
                {
                    showAlert("La cantidad del producto debe ser un número válido mayor que 0.");
                    return null;
                }

                return new Productos
                {
                    codigoProducto = codigo,
                    producto = producto,
                    unidadMedida = codigoUnidadMedida,
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

            gv_listProdTraspaso.DataSource = null;
            gv_listProdTraspaso.DataBind();
        }

        private void LimpiarCamposRegistrarIT()
        {
            txt_Referencia.Text = "";
            txt_glosa.Text = "";
            dd_codAlmacenDestino.SelectedIndex = 0;

            Session["productosTraspaso"] = null;

            gv_productAgregados.DataSource = null;
            gv_productAgregados.DataBind();
        }

        private bool ValidarProductos(List<Productos> productos)
        {
            try
            {
                return productos != null && productos.Count() > 0;
            }
            catch (Exception ex)
            {
                showAlert($"Error validar productos: {ex.Message}");
                return false;
            }
        }

        private void ActualizarGVProductosADD(List<Productos> productos)
        {
            gv_productAgregados.DataSource = productos;
            gv_productAgregados.DataBind();
        }


        // - - - - - - - Funcion eliminar Fila GV
        protected void gv_productAgregados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Eliminar")
            {
                string codigoProducto = (e.CommandArgument.ToString());
                List<Productos> productos = Session["ProductosTraspaso"] as List<Productos>;
                var productoAEliminar = productos.FirstOrDefault(p => p.codigoProducto == codigoProducto);

                if (productoAEliminar != null)
                {
                    productos.Remove(productoAEliminar);
                }
                Session["ProductosTraspaso"] = productos;

                gv_productAgregados.DataSource = productos;
                gv_productAgregados.DataBind();
            }
        }

        // ---- VALIDACION
        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txt_Referencia.Text.Trim()))
            {
                showAlert("Por favor, complete el campo Referencia.");
                return true;
            }

            if (string.IsNullOrEmpty(dd_codAlmacenDestino.SelectedValue))
            {
                showAlert("Por favor, Seleccione un almacén destino válido.");
                return true;
            }

            if (string.IsNullOrEmpty(txt_glosa.Text.Trim()))
            {
                showAlert("Por favor, Complete el campo glosa.");
                return true;
            }
            return false;
        }

        // - - - - - - - - - - - - - - - - TOKEN, ShowAlert
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
    }
}


