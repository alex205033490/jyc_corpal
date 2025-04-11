using AjaxControlToolkit;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIclientes;
using static jycboliviaASP.net.Negocio.NA_APIproduccion;
using static jycboliviaASP.net.Negocio.NA_APILineaProduccion;
using static jycboliviaASP.net.Negocio.NA_APIReceta;
using static jycboliviaASP.net.Negocio.NA_APIproductos;
using System.Data;
using System.Runtime.CompilerServices;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIProduccion : System.Web.UI.Page
    {
        private readonly NA_APILineaProduccion _NA_APILineaProduccion = new NA_APILineaProduccion();
        private readonly NA_APIReceta _NA_APIRecetas = new NA_APIReceta();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = await ObtenerTokenAsync("adm", "Corpal205010180");
                // Lista de produccion
                var lineaProduccion = await ObtenerListLineaProduccion(token);
                dd_lineaProduccion.DataSource = lineaProduccion;
                dd_lineaProduccion.DataTextField = "LineaProducion";
                dd_lineaProduccion.DataValueField = "CodigoLineaProducion";
                dd_lineaProduccion.DataBind();

                // Lista de recetas
                var recetas = await ObtenerListReceta(token);
                dd_recetas.DataSource = recetas;
                dd_recetas.DataTextField = "Descripcion";
                dd_recetas.DataValueField = "CodigoReceta";
                dd_recetas.DataBind();
                dd_recetas.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un receta", ""));

                List<Producto> producto = ObtenerProductosDesdeSession();
                gv_productAgregados.DataSource = producto;
                gv_productAgregados.DataBind();


            }
        }


// - - - - - - - - - - - - - - - - -  POST PRODUCCION

        // - - Obtener lista de clientes (Responsable)
        protected void txt_codResponsable_TextChanged(object sender, EventArgs e)
        {
            string criterio = txt_codResponsable.Text.Trim();
            if (!string.IsNullOrEmpty(criterio))
            {
                cargarResponsableUpon(criterio);
            }
        }
        private async void cargarResponsableUpon(string criterio)
        {
            try
            {
                string token = await ObtenerTokenAsync("adm", "Corpal205010180");

                NA_APIclientes negocio = new NA_APIclientes();
                List<ClienteGetDTO> cliente = await negocio.GET_ClientesAsync(token, criterio);

                gv_listResponsables.DataSource = cliente;
                gv_listResponsables.DataBind();

                gv_listResponsables.Visible = cliente.Count > 0;
            }
            catch(Exception ex)
            {
                showAlert($"Error al realizar la busqueda: {ex.Message}");  
            }
        }
        protected void gv_listResponsables_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_listResponsables.SelectedIndex;
            GridViewRow row = gv_listResponsables.Rows[index];

            string codigo = row.Cells[1].Text;
            string nombreCompleto = row.Cells[2].Text;

            txt_codResponsable.Text = nombreCompleto;
            Session["ScodigoRespProduccion"] = codigo;

            gv_listResponsables.Visible = false;
        }


        // - - Listar LineaProduccion en DD
        private async Task<List<ListLineaProduccionDTO>> ObtenerListLineaProduccion(string token)
        {
            try
            {
                var lProduccion = await _NA_APILineaProduccion.Get_ListLineaProduccion(token);
                return lProduccion.OrderBy(lp => lp.LineaProducion).ToList();
            }
            catch(Exception ex)
            {
                showAlert($"Error al obtener la lista de linea de producción: {ex.Message}");
                return null;
            }

        }
        // - - Listar Recetas en DD
        private async Task<List<ListRecetaDTO>> ObtenerListReceta(string token)
        {
            try
            {
                var receta = await _NA_APIRecetas.Get_ListRecetaAsync(token);
                return receta.OrderBy(r => r.Descripcion).ToList();
            }
            catch(Exception ex)
            {
                showAlert($"Error al obtener la lista de recetas: {ex.Message}");
                return null;
            }
        }

        // - - Cargar Productos GV 
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
                string token = await ObtenerTokenAsync("adm", "Corpal205010180");

                NA_APIproductos negocio = new NA_APIproductos();
                List<productoCriterioGet> productos = await negocio.get_ProductoCriterioAsync(token, criterio);

                gv_listProdProduccion.DataSource = productos;
                gv_listProdProduccion.DataBind();

                gv_listProdProduccion.Visible = productos.Count > 0;
            }
            catch (Exception ex)
            {
                showAlert($"Error al realizar la busqueda: {ex.Message}");
            }
        }
        protected void gv_listProdProduccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_listProdProduccion.SelectedIndex;
            GridViewRow row = gv_listProdProduccion.Rows[index];

            string nombre = row.Cells[2].Text;
            string codigoProducto = row.Cells[1].Text;
            string unidadMedida = row.Cells[3].Text;

            txt_producto.Text = nombre;
            Session["SnombreProdProduccion"] = nombre;
            Session["ScodigoProdProduccion"] = codigoProducto;
            Session["SunidadMedidaProduccion"] = unidadMedida;

            gv_listProdProduccion.Visible = false;
        }

        // - - Add Productos al GV
        protected void btn_addProd_Click(object sender, EventArgs e)
        {
            Producto nuevoProducto = CrearNuevoProducto();
            if(nuevoProducto != null)
            {
                List<Producto> listaProductos = ObtenerProductosDesdeSession();

                listaProductos.Add(nuevoProducto);

                Session["SproductosProduccion"] = listaProductos;

                gv_productAgregados.DataSource = listaProductos;
                gv_productAgregados.DataBind();

                LimpiarCamposAddProductos();
            }
        }
        List<Producto> productos = new List<Producto>();
        public class Producto
        {
            public string CodigoProducto { get; set; }
            public string Nombre {  get; set; }
            public int CodUnidadMedida {  get; set; }
            public int Cantidad {  get; set; }
            public string CodReceta {  get; set; }
        }
        private List<Producto> ObtenerProductosDesdeSession()
        {
            List<Producto> productos = Session["SproductosProduccion"] as List<Producto>;
            if (productos == null)
            {
                productos = new List<Producto>();
            }
            return productos;
        }

        private Producto CrearNuevoProducto()
        {
            try
            {
                if (Session["SnombreProdProduccion"] == null || string.IsNullOrWhiteSpace(Session["SnombreProdProduccion"].ToString()))
                {
                    showAlert("Debe buscar y seleccionar un producto.");
                    return null;
                }
                string producto = (Session["SnombreProdProduccion"].ToString());

                if (Session["ScodigoProdProduccion"] == null || string.IsNullOrWhiteSpace(Session["ScodigoProdProduccion"].ToString()))
                {
                    showAlert("El codigo producto no esta disponible en la sesión.");
                    return null;
                }
                string codProducto = (Session["ScodigoProdProduccion"].ToString());

                if (Session["SunidadMedidaProduccion"] == null || string.IsNullOrWhiteSpace(Session["SunidadMedidaProduccion"].ToString()))
                {
                    showAlert("La unidad medida no esta disponible ne la sesión.");
                    return null;
                }
                int unidadMedida = int.Parse(Session["SunidadMedidaProduccion"].ToString());

                int cantidad = 0;
                if(!int.TryParse(txt_cantProducto.Text, out cantidad) || cantidad <= 0)
                {
                    showAlert("La cantidad del producto debe ser un número válido mayor que 0.");
                    return null;
                }

                if (dd_recetas.SelectedIndex == 0)
                {
                    showAlert("Seleccione una receta válida");
                    return null;
                }
                string receta = dd_recetas.SelectedValue;

                return new Producto
                {
                    CodigoProducto = codProducto,
                    Nombre = producto,
                    CodUnidadMedida = unidadMedida,
                    Cantidad = cantidad,
                    CodReceta = receta
                };
            }
            catch (Exception ex)
            {
                showAlert($"Error al crear el producto: " + ex.Message);
                return null;
            }
        }
        private void ActualizarGVProductosADD(List<Producto> producto)
        {
            gv_productAgregados.DataSource = producto;
            gv_productAgregados.DataBind();
        }
        private void LimpiarCamposAddProductos()
        {
            txt_producto.Text = string.Empty;
            txt_cantProducto.Text = string.Empty;
            dd_recetas.SelectedIndex = 0;
            Session.Remove("SnombreProdProduccion");
            Session.Remove("ScodigoProdProduccion");
            Session.Remove("SunidadMedidaProduccion");

            gv_listProdProduccion.DataSource = null;
            gv_listProdProduccion.DataBind();
        }

        // - - Delete Productos del GV
        protected void gv_productAgregados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Eliminar")
            {
                string codigoProducto = (e.CommandArgument.ToString());
                List <Producto> producto = Session["SproductosProduccion"] as List<Producto>;
                var productoAEliminar = producto.FirstOrDefault(p => p.CodigoProducto == codigoProducto);

                if (productoAEliminar != null)
                {
                    producto.Remove(productoAEliminar);
                }
                Session["SproductosProduccion"] = producto;

                gv_productAgregados.DataSource = producto;
                gv_productAgregados.DataBind();
            }            
        }

        // - - - - - BTN Registrar Produccion
        protected async void btn_registrarProduccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    return;
                }

                List<Producto> productos = ObtenerProductosDesdeSession();

                if (!ValidarProductos(productos))
                {
                    showAlert("Tu lista de productos esta vacia.");
                    return;
                }

                ParteProduccionDTO produccion = CrearProduccion(productos);

                string token = await ObtenerTokenAsync("adm", "Corpal205010180");

                string resultado = await RegistrarProduccionAsync(produccion, token);

                showAlert($"Producción registrado. Nro de producción: {resultado}");

                LimpiarCamposRegistroProduccion();
                LimpiarCamposAddProductos();
            }
            catch (Exception ex)
            {
                showAlert($"Error al registrar la producción: {ex.Message}");
            }
        }

        private ParteProduccionDTO CrearProduccion(List<Producto> productos)
        {
            string codResponsable = Session["ScodigoRespProduccion"].ToString();
            try
            {
                return new ParteProduccionDTO
                {
                    NumeroParteProduccion = 0,
                    Fecha = "2024-11-30T12:10:29.492Z",
                    Referencia = txt_referencia.Text.Trim(),
                    CodigoResponsable = int.Parse(codResponsable),
                    ItemAnalisis = int.Parse(txt_itemAnalisis.Text),
                    LineaProduccion = int.Parse(dd_lineaProduccion.SelectedValue),
                    RealizaDescarga = bool.Parse(dd_realDescarga.SelectedValue),
                    Glosa = txt_glosa.Text,

                    Detalle = productos.Select( p => new ProductoParteProduccionDTO
                    {
                        Item =0 ,
                        CodigoProducto = p.CodigoProducto,
                        Cantidad = p.Cantidad,
                        UnidadMedida = p.CodUnidadMedida,
                        CodigoReceta = p.CodReceta 
                    }).ToList(),
                    Usuario = "adm"
                };
            }
            catch(Exception ex)
            {
                showAlert($"Error: {ex.Message}");
                return null;
            }

        }
        private async Task<string> RegistrarProduccionAsync(ParteProduccionDTO produccion, string token)
        {
            try
            {
                NA_APIproduccion negocio = new NA_APIproduccion();

                string resultado = await negocio.PostParteProduccionAsync(produccion, token);
                return resultado;
            }
            catch(Exception ex)
            {
                showAlert($"Error al registrar la producción en el sistema. {ex.Message}");
                return null;
            }
        }
        private void LimpiarCamposRegistroProduccion()
        {
            txt_referencia.Text = "";
            txt_glosa.Text = "";
            dd_lineaProduccion.SelectedIndex = 0;
            dd_realDescarga.SelectedIndex = 0;
            txt_codResponsable.Text = "";
            Session.Remove("ScodigoRespProduccion");
            txt_itemAnalisis.Text = "";

            gv_listResponsables.DataSource = null;
            gv_listResponsables.DataBind();

            gv_productAgregados.DataSource = null;
            gv_productAgregados.DataBind();

            Session.Remove("SproductosProduccion");
        }
        private bool ValidarProductos(List<Producto> productos)
        {
            try
            {
                return productos != null && productos.Count() > 0;
            }
            catch (Exception ex)
            {
                showAlert($"Error validar Productos: {ex.Message}");
                return false;
            }
        }
        private bool ValidarCampos()
        {

            int itemAnalisis = 0;
            if (!int.TryParse(txt_itemAnalisis.Text, out itemAnalisis) || itemAnalisis <= 0)
            {
                showAlert("El campo ítem análisis debe contener un número válido mayor que 0.");
                return true;
            }
            
            if (Session["ScodigoRespProduccion"] == null || string.IsNullOrWhiteSpace(Session["ScodigoRespProduccion"].ToString()))
            {
                showAlert("Debe buscar y seleccionar un Responsable.");
                return true;
            }

            if(string.IsNullOrWhiteSpace(dd_lineaProduccion.DataValueField))
            {
                showAlert("Seleccione una linea de producción válida");
                return true;
            }
            return false;
        }



        // - - - - - - - - - - - - - - - - -  GET PRODUCCION partePRODUCCION  
        protected async void btn_buscProduccion_Click(object sender, EventArgs e)
        {
            string numProduccion = txt_numProduccion1.Text.Trim();
            
            if (string.IsNullOrEmpty(numProduccion)) 
            {
                gv_produccion.DataBind();
                gv_detalle.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor ingrese un numero de produccion valido.');", true);
                return;
            }
            try
            {
                NA_APIproduccion apiProd = new NA_APIproduccion();
                ParteProduccionDTO produccion = await apiProd.GetProduccionAsync("adm", "123", numProduccion);

                if (produccion != null)
                {
                    //encapsulamiento
                    var parProduccion = new List<ParteProduccionDTO> { produccion };
                    gv_produccion.DataSource = parProduccion;
                    gv_produccion.DataBind();

                    if (produccion.Detalle != null && produccion.Detalle.Count > 0)
                    {
                        gv_detalle.DataSource = produccion.Detalle;
                        gv_detalle.DataBind();
                    }
                    else
                    {
                        gv_detalle.DataSource = null;
                        gv_detalle.DataBind();
                    }
                }
                else
                {
                    gv_detalle.DataSource = null;
                    gv_detalle.DataBind();
                    gv_produccion.DataSource = null;
                    gv_produccion.DataBind();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert","alert ('");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}')", true);
            }



        }



// - - - - - - - - - - - - - - - - - Otros
        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            NA_APIproduccion negocio = new NA_APIproduccion();
            return await negocio.GetTokenAsync(usuario, password);
        }
        private void showAlert(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }



        



        
    }
}