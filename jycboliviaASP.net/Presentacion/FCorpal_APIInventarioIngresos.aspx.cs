using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIinventario;

using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Http;
using Newtonsoft.Json;
using static jycboliviaASP.net.Negocio.NA_APIproductos;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIInventarioIngresos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Productos> datoListaProductos = ObtenerProductosDesdeSession();
                gv_productAgregados.DataSource = datoListaProductos;
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
                showalert($"Error: {ex.Message}");
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
            catch(Exception ex)
            {
                showalert($"Error al obtener los ingresos: {ex.Message}");
            }
        }


        //-------------------------- POST - INVENTARIO INGRESO
        protected async void btn_registrarIngreso_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Obtener los datos desde los controles de la página
                string referencia = txt_Referencia.Text.Trim();
                int codigoMoneda = int.Parse(dd_codMoneda.SelectedValue);
                int codigoAlmacen = int.Parse(txt_codAlmacen.Text.Trim());
                string motivoMovimiento = txt_motMovimiento.Text.Trim();
                int itemAnalisis = string.IsNullOrEmpty(txt_itemAnalisis.Text.Trim()) ? 0 : int.Parse(txt_itemAnalisis.Text.Trim());
                string glosa = txt_glosa.Text.Trim();
                string usuario = "adm"; // Esto puede ser dinámico dependiendo del usuario logueado.

                string password = "123";

                string token = await ObtenerTokenAsync(usuario, password);  // Asegúrate de obtener el token de manera adecuada
                // 2. Obtener la lista de productos desde la sesión
                List<Productos> productosSession = ObtenerProductosDesdeSession();

                // 3. Crear la lista de DetalleProductos
                List<ItemIngresoDTO> detalleProductos = new List<ItemIngresoDTO>();
                int item = 0;

                foreach (var producto in productosSession)
                {
                    detalleProductos.Add(new ItemIngresoDTO
                    {
                        Item = item++, // Incrementar el Item por cada producto
                        CodigoProducto = producto.CodigoProducto,
                        UnidadMedida = producto.UnidadMedida,
                        Cantidad = producto.Cantidad,
                        CostoUnitario = producto.CostoUnitario,
                        CostoTotal = producto.CostoTotal
                    });
                }

                // 4. Crear el objeto InventarioIngreso
                InventarioIngreso ingreso = new InventarioIngreso
                {
                    NumeroIngreso = 0,  // Puedes obtenerlo de algún otro lado si es necesario
                    Fecha = DateTime.UtcNow, // Ajusta esto según lo necesario
                    Referencia = referencia,
                    CodigoMoneda = codigoMoneda,
                    CodigoAlmacen = codigoAlmacen,
                    MotivoMovimiento = motivoMovimiento,
                    ItemAnalisis = itemAnalisis,
                    Glosa = glosa,
                    DetalleProductos = detalleProductos,
                    Usuario = usuario
                };

                // 5. Llamar a la API con el objeto InventarioIngreso
                NA_APIinventario negocio= new NA_APIinventario();
                
                string resultado = await negocio.PostInventarioIngresoAsync(ingreso, token);

                // 6. Mostrar el resultado de la API
                showalert($"Resultado: {resultado}");
            }
            catch (Exception ex)
            {
                showalert($"Error al registrar ingreso: {ex.Message}");
            }
        }
        private InventarioIngreso CrearIngreso()
        {
            var ingreso = new InventarioIngreso
            {
                NumeroIngreso = 0,
                Fecha = DateTime.Now,
                Referencia = txt_Referencia.Text,
                CodigoMoneda = int.Parse(dd_codMoneda.SelectedValue),
                CodigoAlmacen = int.Parse(txt_codAlmacen.Text.Trim()),
                MotivoMovimiento = txt_motMovimiento.Text.Trim(),
                ItemAnalisis = int.Parse(txt_itemAnalisis.Text.Trim()),
                Glosa = txt_glosa.Text,
                Usuario = "ADM",
                DetalleProductos = ObtenerDetallesProductos()
            };
            return ingreso;
        }
        private List<ItemIngresoDTO> ObtenerDetallesProductos()
        {
            List<ItemIngresoDTO> detalles = new List<ItemIngresoDTO>();

            foreach(GridViewRow row in gv_productAgregados.Rows)
            {
                try
                {
                    var detalle = new ItemIngresoDTO
                    {
                        Item = 0,
                        CodigoProducto = row.Cells[1].Text,
                        UnidadMedida = int.Parse(row.Cells[2].Text),
                        Cantidad = decimal.Parse(row.Cells[3].Text),
                        CostoUnitario = decimal.Parse(row.Cells[4].Text),
                        CostoTotal = decimal.Parse(row.Cells[5].Text)
                    };
                    detalles.Add(detalle);
                }
                catch (Exception ex)
                {
                    showalert($"Error al procesar fila: {ex.Message}");
                }
            }
            return detalles;
        }
        private async Task<string> RegistrarIngreso(InventarioIngreso ingreso)
        {
            try
            {
                var api = new NA_APIinventario();
                var token = await api.GetTokenAsync("adm", "123");

                var result = await api.PostInventarioIngresoAsync(ingreso, token);
                showalert($"Resultado POST: {result}");
                return result; 
            }
            catch (Exception ex)
            {
                showalert($"Error al registra el ingreso: {ex.Message}");
                return null; 
            }
        }
        private void LimpiarCampos()
        {
            txt_Referencia.Text = "";
            txt_codAlmacen.Text = "";
            txt_motMovimiento.Text = "";
            txt_itemAnalisis.Text = "";
            txt_glosa.Text = "";
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txt_codAlmacen.Text.Trim()))
            {
                showalert("Por favor, ingrese un codigo almacen.");
                return false;
            }

            if (string.IsNullOrEmpty(txt_motMovimiento.Text.Trim()))
            {
                showalert("Por favor, complete el campo motivo Movimiento.");
                return false;
            }

            if (string.IsNullOrEmpty(txt_itemAnalisis.Text.Trim()))
            {
                showalert("Por favor, complete el campo Item Analisis.");
                return false;
            }

            return true;
        }
        private void showalert(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{mensaje}');", true);
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
            string precioUnitario = row.Cells[4].Text;

            txt_producto.Text = nombre;
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
            }
        }
        
        List<Productos> productos = new List<Productos>();
        public class Productos
        {
            public int Item {  get; set; }
            public string Nombre {  get; set; }
            public string CodigoProducto {  get; set; }
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
                string producto = txt_producto.Text.Trim();
                if (string.IsNullOrEmpty(producto))
                {
                    showalert("Debe buscar y seleccionar el nombre del producto.");
                    return null;
                }

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
                decimal precio = decimal.Parse(Session["SprecioUnitario"].ToString());

                decimal cantidad = 0;
                if (!decimal.TryParse(txt_cantProducto.Text, out cantidad) || cantidad <= 0)
                {
                    showalert("La cantidad del producto debe ser un número válido mayor que cero.");
                    return null;
                }

                decimal costoTotal = precio * cantidad;

                return new Productos
                {
                    Nombre = producto,
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
            Session.Remove("ScodigoProducto");
            Session.Remove("ScodigoUnidadMedida");
            Session.Remove("SprecioUnitario");
        }
        private void ActualizarGVProductosADD(List<Productos> productos)
        {
            gv_productAgregados.DataSource = productos;
            gv_productAgregados.DataBind();
        }

        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            NA_APIinventario negocio = new NA_APIinventario();
            return await negocio.GetTokenAsync(usuario, password);
        }
    }
}