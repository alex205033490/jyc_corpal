using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIinventario;
using static jycboliviaASP.net.Negocio.NA_APIAlmacen;
using static jycboliviaASP.net.Negocio.NA_APIMotivoContable;

using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Http;
using Newtonsoft.Json;
using static jycboliviaASP.net.Negocio.NA_APIproductos;
using System.Web.Services.Description;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIInventarioIngresos : System.Web.UI.Page
    {
        private readonly NA_APIMotivoContable _NA_APIMotivoC = new NA_APIMotivoContable();
        private readonly NA_APIAlmacen _NA_APIAlmacen = new NA_APIAlmacen();
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = await ObtenerTokenAsync("adm", "Corpal205010180");
                var almacen = await ObtenerListCodAlmacen(token);
                var motMovimiento = await ObtenerListCodMotMov(token);
                dd_CodAlmacenIIngreso.DataSource = almacen;
                dd_CodAlmacenIIngreso.DataTextField = "Nombre";
                dd_CodAlmacenIIngreso.DataValueField = "CodigoAlmacen";
                dd_CodAlmacenIIngreso.DataBind();
                dd_CodAlmacenIIngreso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un almacén", ""));

                dd_motMovI.DataSource = motMovimiento;
                dd_motMovI.DataTextField = "MotivoContable";
                dd_motMovI.DataValueField = "CodigoMotivo";
                dd_motMovI.DataBind();
                dd_motMovI.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un motivo", ""));

                //
                List<Productos> productos = ObtenerProductosDesdeSession();
                gv_productAgregados.DataSource = productos;
                gv_productAgregados.DataBind();
                //Session["Productos"] = datoListaProductos;
            }
        }

        //-------------------------- GET - INVENTARIO INGRESO DETALLE
        protected async void btn_InvIngresoGET_Click(object sender, EventArgs e)
        {
            string numTransaccion = TextBox1.Text.Trim();

            // Validación de entrada
            if (string.IsNullOrEmpty(numTransaccion))
            {
                showalert("Por favor ingrese un número de ingreso válido.");
                LimpiarGrids();
                return;
            }
            try
            {
                // Llamar al método de la capa de negocio
                NA_APIinventario apiInv = new NA_APIinventario();
                InventarioIngreso ingreso = await apiInv.GetInventarioIngresoDetalleAsync("adm", "123", numTransaccion);

                if (ingreso != null)
                {
                    MostrarDetallesInventario(ingreso);
                }
                else
                {
                    showalert("No se encontraron registros con el número de ingreso proporcionado.");
                    LimpiarGrids();
                }
            }
            catch (ApplicationException ex)
            {
                // Excepciones esperadas (errores conocidos)
                showalert($"{ex.Message}");
                LimpiarGrids();
            }
            catch (Exception ex)
            {
                // Excepciones generales (errores no esperados)
                showalert($"Ha ocurrido un error inesperado: {ex.Message}");
                LimpiarGrids();
            }
        }

        // Mostrar detalles en los controles GridView
        private void MostrarDetallesInventario(InventarioIngreso ingreso)
        {
            var invIngreso = new List<InventarioIngreso> { ingreso };
            gv_Inventario.DataSource = invIngreso;
            gv_Inventario.DataBind();

            if (ingreso.DetalleProductos != null && ingreso.DetalleProductos.Count > 0)
            {
                gv_DetalleProductos.DataSource = ingreso.DetalleProductos;
                gv_DetalleProductos.DataBind();
            }
            else
            {
                gv_DetalleProductos.DataSource = null;
                gv_DetalleProductos.DataBind();
            }
        }

        // Limpiar las grillas de datos
        private void LimpiarGrids()
        {
            gv_Inventario.DataSource = null;
            gv_Inventario.DataBind();
            gv_DetalleProductos.DataSource = null;
            gv_DetalleProductos.DataBind();
        }


        //-------------------------- GET - BUSCAR INVENTARIO INGRESO
        protected async void btn_invIngreso2_Click(object sender, EventArgs e)
        {
            string numTransaccion = TextBox2.Text.Trim();

            try
            {
                if (string.IsNullOrEmpty(numTransaccion))
                {
                    NA_APIinventario apiInv = new NA_APIinventario();
                    List<Ingresos> ingresos = await apiInv.ObtenerIngresosAsync("adm", "123", numTransaccion);
                    gv_invIngresos2.DataSource = ingresos;
                    gv_invIngresos2.DataBind();
                }
                else
                {
                    NA_APIinventario apiInv = new NA_APIinventario();
                    List<Ingresos> ingresos = await apiInv.ObtenerIngresosAsync("adm", "123", numTransaccion);

                    if (ingresos.Any())
                    {
                        gv_invIngresos2.DataSource = ingresos;
                        gv_invIngresos2.DataBind();
                    }
                    else
                    {
                        showalert($"No se encontró ningún ingreso con el codigo : {numTransaccion}");
                        gv_invIngresos2.DataSource = new List<Ingresos>();
                        gv_invIngresos2.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener los ingresos: {ex.Message}");
            }
        }


        //-------------------------- POST - INVENTARIO INGRESO
        protected async void btn_registrarIngreso_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    return;
                }

                List<Productos> productos = ObtenerProductosDesdeSession();

                if(!ValidarProductos(productos))
                {
                    showalert("Tu lista de productos esta vacia.");
                    return;
                }

                InventarioIngreso ingreso = CrearIngreso(productos);

                string token = await ObtenerTokenAsync("adm", "Corpal205010180");

                string resultado = await RegistrarIngresoAsync(ingreso, token);

                showalert($"Ingreso Registrado. Nro de Ingreso: {resultado}");

                LimpiarCamposRegistrarII();
                LimpiarCamposAddProductos();
            }
            catch (Exception ex)
            {
                showalert($"Error al registrar el ingreso: {ex.Message}\n{ex.StackTrace}");
            }
        }


        private InventarioIngreso CrearIngreso(List<Productos> productos)
        {
            try
            {
                return new InventarioIngreso
                {
                    NumeroIngreso = 0,
                    Fecha = "2024-11-30T00:00:00",
                    Referencia = txt_Referencia.Text.Trim(),
                    CodigoMoneda = int.Parse(dd_codMoneda.SelectedValue),
                    CodigoAlmacen = int.Parse(dd_CodAlmacenIIngreso.SelectedValue),
                    MotivoMovimiento = dd_motMovI.Text.Trim(),
                    ItemAnalisis = int.Parse(txt_itemAnalisis.Text),
                    Glosa = txt_glosa.Text,

                    DetalleProductos = productos.Select(p => new ItemIngresoDTO
                    {
                        Item = 0,
                        CodigoProducto = p.CodigoProducto,
                        UnidadMedida = p.UnidadMedida,
                        Cantidad = p.Cantidad,
                        CostoUnitario = p.CostoUnitario,
                        CostoTotal = p.CostoTotal
                    }).ToList(),
                    Usuario = "adm"
                };

            } catch (Exception ex)
            {
                showalert($"error: {ex.Message}");
                return null;
            }
        }
        private async Task<string> RegistrarIngresoAsync(InventarioIngreso ingreso, string token)
        {
            try
            {
                NA_APIinventario negocio = new NA_APIinventario();
 
                string resultado = await negocio.PostInventarioIngresoAsync(ingreso, token);
                return resultado;

            }
            catch (Exception ex)
            {
                showalert($"Error al registrar el ingreso en el sistema. {ex.Message}");
                return null;
            }
        }

        private void LimpiarCamposRegistrarII()
        {
            txt_Referencia.Text = "";
            dd_motMovI.SelectedIndex = 0;
            txt_itemAnalisis.Text = "";
            txt_glosa.Text = "";

            dd_codMoneda.SelectedIndex = 0;
            dd_CodAlmacenIIngreso.SelectedIndex = 0;

            Session["productos"] = null;

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
                showalert($"error validarProductos: {ex.Message}");
                return false;
            }
        }
        private bool ValidarCampos()
        {
            int itemAnalisis = 0;
            if (string.IsNullOrWhiteSpace(txt_itemAnalisis.Text.Trim()))
            {
                txt_itemAnalisis.Text = "0";
            }
            else
            {
                if (!int.TryParse(txt_itemAnalisis.Text, out itemAnalisis) || itemAnalisis<0)
                {
                    showalert("El campo ítem análisis debe contener un número valido mayor o igual que 0");
                    return true;
                }
            }

            if (string.IsNullOrEmpty(dd_CodAlmacenIIngreso.SelectedValue))
            {
                showalert("Por favor, Seleccione un almacén válido.");
                return true;
            }

            if (string.IsNullOrEmpty(dd_motMovI.SelectedValue))
            {
                showalert("Por favor, Seleccione un Motivo Movimiento válido.");
                return true;
            }

            return false;
        }



        //////////// CARGAR PRODUCTOS GV
        protected void txt_producto_TextChanged(object sender, EventArgs e)
        {
            string criterio = txt_producto.Text.Trim();
            if(!string.IsNullOrEmpty(criterio))
            {
                cargarProductosUpon(criterio);
            }

        }
        private async void cargarProductosUpon(string criterio)
        {
            try
            {
                string token = await ObtenerTokenAsync("adm", "Corpal205010180");

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
            string precioUnitario = row.Cells[4].Text;

            txt_producto.Text = nombre;
            Session["SnombreProducto"] = nombre;
            Session["ScodigoProducto"] = codigoProducto;
            Session["ScodigoUnidadMedida"] = codigoUnidadMedida;
            Session["SprecioUnitario"] = precioUnitario;

            gv_listProdIngresos.Visible = false;
        }


/////////// AGREGAR PRODUCTOS AL GRIDVIEW
        protected void btn_addProd_Click(object sender, EventArgs e)
        {
            Productos nuevoProducto = CrearNuevoProducto();
            if(nuevoProducto != null)
            {
                List<Productos> listaProductos = ObtenerProductosDesdeSession();

                listaProductos.Add(nuevoProducto);

                Session["Productos"] = listaProductos;

                gv_productAgregados.DataSource = listaProductos;
                gv_productAgregados.DataBind();

                LimpiarCamposAddProductos();
            }
            else
            {
                showalert("Ingrese un producto valido");
            }
        }
        
        List<Productos> productos = new List<Productos>();
        public class Productos
        {
            public string CodigoProducto {  get; set; }
            public string Nombre {  get; set; }
            public int UnidadMedida {  get; set; }
            public decimal Cantidad {  get; set; }
            public decimal CostoUnitario {  get; set; }
            public decimal CostoTotal {  get; set; }
        }
        private List<Productos> ObtenerProductosDesdeSession()
        {
            List<Productos> productos = Session["Productos"] as List<Productos>; 
            if (productos == null)
            {
                productos = new List<Productos>();
            }
            return productos;
        }
        private Productos CrearNuevoProducto()
        {
            try
            {
               
                if (Session["SnombreProducto"] == null || string.IsNullOrWhiteSpace(Session["SnombreProducto"].ToString()))
                {
                    showalert("Debe buscar y seleccionar un producto.");
                    return null;
                }
                string nombre = (Session["SnombreProducto"].ToString());

                if (Session["ScodigoProducto"] == null || string.IsNullOrWhiteSpace(Session["ScodigoProducto"].ToString())){
                    showalert("El codigo del producto no esta disponible en la sesión");
                    return null;
                }
                string codigo = (Session["ScodigoProducto"].ToString());

                if (Session["ScodigoUnidadMedida"] ==null || string.IsNullOrWhiteSpace(Session["ScodigoUnidadMedida"].ToString()))
                {
                    showalert("La unidad medida del producto no esta disponible en la sesión");
                    return null;
                }
                int codigoUnidadMedida = int.Parse(Session["ScodigoUnidadMedida"].ToString());

                if (Session["SprecioUnitario"] == null || string.IsNullOrWhiteSpace(Session["SprecioUnitario"].ToString()))
                {
                    showalert("El precio del producto no esta disponible en la sessión");
                    return null;
                }
                //decimal precio = decimal.Parse(Session["SprecioUnitario"].ToString());
                decimal precio = 1;

                decimal cantidad = 0;
                if (!decimal.TryParse(txt_cantProducto.Text, out cantidad) || cantidad <= 0)
                {
                    showalert("La cantidad del producto debe ser un número válido mayor que cero.");
                    return null;
                }

                decimal costoTotal = precio * cantidad;

                return new Productos
                {
                    Nombre = nombre,
                    CodigoProducto = codigo,
                    UnidadMedida = codigoUnidadMedida,
                    Cantidad = cantidad,
                    CostoUnitario = precio,
                    CostoTotal = costoTotal
                };
            } catch(Exception ex)
            {
                showalert($"Error al crear el producto: " + ex.Message);
                return null;
            }
        }

        private void LimpiarCamposAddProductos()
        {
            txt_producto.Text = string.Empty;
            txt_cantProducto.Text = string.Empty;
            Session.Remove("SnombreProducto");
            Session.Remove("ScodigoProducto");
            Session.Remove("ScodigoUnidadMedida");
            Session.Remove("SprecioUnitario");

            gv_listProdIngresos.DataSource = null;
            gv_listProdIngresos.DataBind();
        }
        private void ActualizarGVProductosADD(List<Productos> productos)
        {
            gv_productAgregados.DataSource = productos;
            gv_productAgregados.DataBind();
        }

////////    LISTAR ALMACENES EN DD
        private async Task<List<ListAlmacenesDTO>> ObtenerListCodAlmacen(string token)
        {
            try
            {
                var almacen = await _NA_APIAlmacen.Get_ListAlmacenAsync(token);
                return almacen.OrderBy(a => a.Nombre).ToList();
            }   
            catch(Exception ex)
            {
                showalert($"Error al obtener la lista de almacenes: {ex.Message}");
                return null;
            }
        }
////////    LISTAR MOTIVOS EN DD
        private async Task<List<ListMotMovIDTO>> ObtenerListCodMotMov(string token)
        {
            try
            {
                var motivo = await _NA_APIMotivoC.Get_ListMotMovIAsync(token);
                return motivo.OrderBy(m => m.MotivoContable).ToList();
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener la lista de motivos: {ex.Message}");
                return null;
            }
        }
            
        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            NA_APIinventario negocio = new NA_APIinventario();
            return await negocio.GetTokenAsync(usuario, password);
        }

        private void showalert(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        protected void gv_productAgregados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                string codigoProducto = (e.CommandArgument.ToString());
                List<Productos> productos = Session["Productos"] as List<Productos>;
                var productoAEliminar = productos.FirstOrDefault(p => p.CodigoProducto == codigoProducto);

                if (productoAEliminar != null)
                {
                    productos.Remove(productoAEliminar);
                }
                Session["Productos"] = productos;

                gv_productAgregados.DataSource = productos;
                gv_productAgregados.DataBind();
            }
        }
    }
}