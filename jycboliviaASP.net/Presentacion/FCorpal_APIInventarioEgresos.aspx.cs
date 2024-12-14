using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using static jycboliviaASP.net.Negocio.NA_APIinventario;
using System.Globalization;
using System.Threading.Tasks;
using static jycboliviaASP.net.Negocio.NA_APIAlmacen;
using static jycboliviaASP.net.Negocio.NA_APIMotivoContable;
using static jycboliviaASP.net.Negocio.NA_APIproductos;
using Newtonsoft.Json;
using System.Diagnostics;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIInventarioEgresos : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = await ObtenerTokenAsync("adm", "123");

                var almacen = await ObtenerListCodAlmacen(token);
                var motMovimiento = await ObtenerListCodMotMov(token);

                dd_codAlmacenIEgreso.DataSource = almacen;
                dd_codAlmacenIEgreso.DataTextField = "Nombre";
                dd_codAlmacenIEgreso.DataValueField = "CodigoAlmacen";
                dd_codAlmacenIEgreso.DataBind();
                dd_codAlmacenIEgreso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un almacén", ""));

                dd_motMoviIEgreso.DataSource = motMovimiento;
                dd_motMoviIEgreso.DataTextField = "MotivoContable";
                dd_motMoviIEgreso.DataValueField = "CodigoMotivo";
                dd_motMoviIEgreso.DataBind();
                dd_motMoviIEgreso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un motivo", ""));

                List<Productos> productos = ObtenerProductosDesdeSession();
                gv_productAgregadosIE.DataSource = productos;
                gv_productAgregadosIE.DataBind();
            }
        }

        ///         GET - INVENTARIO EGRESOS DETALLE 
        protected async void BuscarEgresoInventarioDetalle_Click(object sender, EventArgs e)
        {
            string numTransaccion = (TextBox1.Text.Trim());
            if (!ValidarEntrada(numTransaccion))
            {
                LimpiarGrids();
                return;
            }
            try
            {
                var egreso = await BuscarEgresoInventarioDetalleAsync(numTransaccion);
                if(egreso != null)
                {
                    MostrarDetallesInventario(egreso);
                }
                else
                {
                    MostrarMensajeYLimpiarGrids("No se encontraron registros con el número de egreso proporcionado.");
                }
            }
            catch (Exception ex)
            {
                ManejarErrores(ex);
            }
        }
        
        private bool ValidarEntrada(string numTransaccion)
        {
            if (string.IsNullOrWhiteSpace(numTransaccion))
            {
                showalert("Por favor, Ingrese un número de egreso válido.");
                return false;
            }
            return true;
        }
        private async Task<InventarioEgreso> BuscarEgresoInventarioDetalleAsync(string numTransaccion)
        {
            string us = "adm";
            string pass = "123";

            try
            {
                NA_APIinventario negocio = new NA_APIinventario();
                return await negocio.GetInventarioEgresoDetalleAsync(us, pass, numTransaccion);

            } catch(Exception ex)
            {
                showalert($"{ex.Message}");
                throw;
            }
        }
        private void MostrarDetallesInventario(InventarioEgreso egreso)
        {
            AsignarDatosGrids(egreso);
        }
        private void AsignarDatosGrids(InventarioEgreso egreso)
        {
            GridView1.DataSource = new List<InventarioEgreso> { egreso };
            GridView1.DataBind();

            GridView2.DataSource = (egreso.DetalleProductos != null && egreso.DetalleProductos.Count > 0)
                ? egreso.DetalleProductos 
                : null;
            GridView2.DataBind();
        }
        private void LimpiarGrids()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            GridView2.DataSource = null;
            GridView2.DataBind();
        }
        private void MostrarMensajeYLimpiarGrids(string mensaje)
        {
            showalert(mensaje);
            LimpiarGrids();
        }
        private void ManejarErrores(Exception ex)
        {
            if(ex is ApplicationException)
            {
                showalert($"Error; {ex.Message}");
            } else
            {
                showalert($"Ha ocurrido un error inesperado: {ex.Message}");
            }
            LimpiarGrids();
        }

        
        ///         GET - INVENTARIO EGRESOS  
        protected async void BuscarEgresoInventario_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = TextBox2.Text.Trim();

            try
            {
                if (string.IsNullOrEmpty(criterioBusqueda))
                {
                    NA_APIinventario apiInv = new NA_APIinventario();
                    List<Ingresos> egreso = await apiInv.ObtenerEgresosAsync("adm", "123", criterioBusqueda);
                    GridView3.DataSource = egreso;
                    GridView3.DataBind();

                }
                else
                {
                    NA_APIinventario apiInv = new NA_APIinventario();
                    List<Ingresos> egreso = await apiInv.ObtenerEgresosAsync("adm", "123", criterioBusqueda);

                    if (egreso.Any())
                    {
                        GridView3.DataSource = egreso;
                        GridView3.DataBind();
                    }
                    else
                    {
                        showalert($"No se encontró ningún egreso con el codigo : {criterioBusqueda}.");
                        GridView3.DataSource = new List<Ingresos>();
                        GridView3.DataBind();
                    }
                }
            }
            
            catch (Exception ex)
            {
                showalert($"Error al obtener los egresos: {ex.Message}");
            }
        }
        private async Task BuscarYMostrarEgresos(string criterioBusqueda)
        {
            NA_APIinventario negocio = new NA_APIinventario();
            List<Ingresos> egresos;

            string usu = "adm";
            string pass = "123";

            try
            {
                egresos = await negocio.ObtenerEgresosAsync(usu, pass, criterioBusqueda);

                if(egresos != null && egresos.Any())
                {
                    GridView3.DataSource = egresos;
                    GridView3.DataBind();
                }
                else
                {
                    showalert(criterioBusqueda == null
                        ? "No se encontraron egresos."
                        : $"No se encontró ningún egreso con el código: {criterioBusqueda}.");

                    GridView3.DataSource = new List<Ingresos>();
                    GridView3.DataBind();
                }
            }
            catch (Exception ex)
            {
                showalert($"Error al intentar obtener los egresos: {ex.Message}");
            }
        }


        ///         POST - INVENTARIO EGRESOS
        protected async void btn_InventarioEgresoPost2_Click(object sender, EventArgs e)
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
                    showalert("Tu lista de productos está vacía.");
                    return;
                }

                InventarioEgreso egreso = CrearEgreso(productos);

                string token = await ObtenerTokenAsync("adm", "123");

                // Llamamos al método de negocio para registrar el egreso
                string resultado = await RegistrarEgresoAsync(egreso, token);

                // Si el resultado contiene algún mensaje de error, lo mostramos
                if (resultado.Contains("Error"))
                {
                    showalert($"{resultado}");
                }
                else
                {
                    // Si no hay error, mostramos el número de egreso
                    showalert($"Egreso Registrado. Nro de Egreso: {resultado}");

                    LimpiarCampos();
                    LimpiarCamposAddProductos();
                }
            }
            catch (Exception ex)
            {
                showalert($"Error al registrar el Egreso: {ex.Message}");
            }
        }

        private InventarioEgreso CrearEgreso(List<Productos> productos)
        {
            try
            {
                return new InventarioEgreso
                {
                    NumeroEgreso = 0,
                    Fecha = "2024-11-30T00:00:00",
                    Referencia = TextBoxReferencia.Text,
                    CodigoAlmacen = int.Parse(dd_codAlmacenIEgreso.SelectedValue),
                    MotivoMovimiento = dd_motMoviIEgreso.Text.Trim(),
                    ItemAnalisis = int.Parse(TextBoxItemAnalisis.Text),
                    Glosa = TextBoxGlosa.Text,

                    DetalleProductos = productos.Select(p => new ItemEgresoDTO
                    {
                        Item = 0,
                        CodigoProducto = p.CodigoProducto,
                        UnidadMedida = p.UnidadMedida,
                        Cantidad = p.Cantidad
                    }).ToList(),
                    Usuario = "adm",
                };
            }
            catch (Exception ex)
            {
                showalert($"Error: {ex.Message}");
                    return null;
            }
        }
        
        private async Task<string> RegistrarEgresoAsync(InventarioEgreso egreso, string token)
        {
            try
            {
                NA_APIinventario negocio = new NA_APIinventario();

                string resultado = await negocio.PostInventarioEgresoAsync(egreso, token);
                return resultado;
            }

            catch (Exception ex)
            {
                showalert($"Error al registrar el egreso: {ex.Message}");
                return null;
            }
        }
        private bool ValidarCampos()
        {
            // Realiza las validaciones
            if (string.IsNullOrEmpty(dd_codAlmacenIEgreso.SelectedValue))
            {
                showalert("Por favor, Seleccione un almacén válido.");
                return true;
            }

            if (string.IsNullOrEmpty(dd_motMoviIEgreso.SelectedValue))
            {
                showalert("Por favor, Seleccione un Motivo movimiento válido.");
                return true;
            }

            if (string.IsNullOrEmpty(TextBoxItemAnalisis.Text.Trim()))
            {
                showalert("Por favor, Complete el campo Item Analisis.");
                return true;
            }// sub
            return false;
        }
        private bool ValidarProductos(List<Productos> productos)
        {
            try
            {
                return productos != null && productos.Count() > 0;
            }
            catch(Exception ex)
            {
                showalert($"Error validar productos: {ex.Message}");
                return false;
            }
        }
        private void LimpiarCampos()
        {
            TextBoxReferencia.Text = string.Empty;
            dd_codAlmacenIEgreso.SelectedIndex = 0;
            dd_motMoviIEgreso.SelectedIndex = 0;
            TextBoxItemAnalisis.Text = string.Empty;
            TextBoxGlosa.Text = string.Empty;

            Session["ProductosEgre"] = null;

            gv_productAgregadosIE.DataSource = null;
            gv_productAgregadosIE.DataBind();
        }


        ////////  CARGAR PRODUCTOS AL GV
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

                gv_listProdIngresos.DataSource = productos;
                gv_listProdIngresos.DataBind();

                gv_listProdIngresos.Visible = productos.Count > 0;
            }
            catch (Exception ex)
            {
                showalert($"Error al realizar la busqueda: {ex.Message}");
            }
        }
        protected void gv_listProdIngresos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_listProdIngresos.SelectedIndex;
            GridViewRow row = gv_listProdIngresos.Rows[index];

            string nombre = row.Cells[1].Text;
            string codigoProducto = row.Cells[2].Text;
            string codigoUnidadMedida = row.Cells[3].Text;

            txt_producto.Text = nombre;
            Session["ScodigoProductoEgre"] = codigoProducto;
            Session["SunidadMedidaEgre"] = codigoUnidadMedida;

            gv_listProdIngresos.Visible = false;
        }


        ////////    AGREGAR PRODUCTOS AL GRIDVIEW
        protected void btn_addProd_Click(object sender, EventArgs e)
        {
            Productos nuevoProducto = CrearNuevoProducto();
            if(nuevoProducto != null)
            {
                List<Productos> listaProductos = ObtenerProductosDesdeSession();

                listaProductos.Add(nuevoProducto);

                Session["ProductosEgre"] = listaProductos;

                gv_productAgregadosIE.DataSource = listaProductos;
                gv_productAgregadosIE.DataBind();

                LimpiarCamposAddProductos();
            }
            else
            {
                showalert("Ingrese un producto Valido");
            }

        }

        List<Productos> productos = new List<Productos>();
        public class Productos
        {
            public string CodigoProducto { get; set; }
            public string Nombre { get; set; }
            public int UnidadMedida { get; set; }
            public decimal Cantidad { get; set; }
            public decimal CostoUnitario { get; set; }
            public decimal CostoTotal { get; set; }
        }
        private List<Productos> ObtenerProductosDesdeSession()
        {
            List<Productos> productos = Session["ProductosEgre"] as List<Productos>;
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
                    showalert("Debe buscar y seleccionar un producto");
                    return null;
                }
                
                if (Session["ScodigoProductoEgre"] == null || string.IsNullOrWhiteSpace(Session["ScodigoProductoEgre"].ToString()))
                {
                    showalert("El codigo del producto no esta disponible en la sesión");
                    return null;
                }
                string codigo = (Session["ScodigoProductoEgre"].ToString());

                if (Session["SunidadMedidaEgre"] == null || string.IsNullOrWhiteSpace(Session["SunidadMedidaEgre"].ToString()))
                {
                    showalert("La unidad medida no esta disponible en la sesión");
                    return null;
                }
                int codigoUnidadMedida = int.Parse(Session["SunidadMedidaEgre"].ToString());

                decimal cantidad = 0;
                if(!decimal.TryParse(txt_cantProducto.Text, out cantidad) || cantidad <= 0)
                {
                    showalert("La cantidad del producto debe ser un número válido mayor que cero.");
                    return null;
                }

                return new Productos
                {
                    Nombre = producto,
                    CodigoProducto = codigo,
                    UnidadMedida = codigoUnidadMedida,
                    Cantidad = cantidad
                };
            }
            catch(Exception ex)
            {
                showalert($"Error al crear el producto: {ex.Message}");
                return null;
            }
        }
        private void LimpiarCamposAddProductos()
        {
            txt_producto.Text = string.Empty;
            txt_cantProducto.Text = string.Empty;
            Session.Remove("ScodigoProductoEgre");
            Session.Remove("SunidadMedidaEgre");
        }
        private void ActualizarGVProductosADD(List<Productos> productos)
        {
            gv_productAgregadosIE.DataSource = productos;
            gv_productAgregadosIE.DataBind();
        }
        

        ////////    LISTAR ALMACENES EN DD
        private async Task<List<ListAlmacenesDTO>> ObtenerListCodAlmacen(string token)
        {
            try
            {
                NA_APIAlmacen negocio = new NA_APIAlmacen();
                return await negocio.Get_ListAlmacenAsync(token);
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener la lista de almacenes: {ex.Message}");
                return null;
            }
        }
        ////////    LISTAR MOTIVOS EN DD EGRESO
        private async Task<List<ListMotMovIDTO>> ObtenerListCodMotMov(string token)
        {
            try
            {
                NA_APIMotivoContable negocio = new NA_APIMotivoContable();
                return await negocio.Get_ListMotMovEAsync(token);
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener la lista de motivos: {ex.Message}");
                return null;
            }
        }


        private void showalert(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{mensaje}');", true);
        }
        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            NA_APIinventario negocio = new NA_APIinventario();
            return await negocio.GetTokenAsync(usuario, password);
        }

        // Eliminar fila gv
        protected void gv_productAgregados(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                string codigoProducto = (e.CommandArgument.ToString());
                List<Productos> productos = Session["ProductosEgre"] as List<Productos>;
                var productoAEliminar = productos.FirstOrDefault(p => p.CodigoProducto == codigoProducto);

                if (productoAEliminar != null)
                {
                    productos.Remove(productoAEliminar);
                }
                Session["ProductosEgre"] = productos;

                gv_productAgregadosIE.DataSource = productos;
                gv_productAgregadosIE.DataBind();

            }
        }
    }
}