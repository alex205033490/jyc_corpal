//using jycboliviaASP.net.Negocio;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIcompras;

using System.Threading.Tasks;
using static jycboliviaASP.net.Negocio.NA_APIProveedores;
using static jycboliviaASP.net.Negocio.NA_APIproductos;
using System.Web.Services.Description;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APICompras : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = await ObtenerTokenAsync("adm", "123");
                var proveedor = await ObtenerListCodProveedor(token);

                dd_codProveedor.DataSource = proveedor;
                dd_codProveedor.DataTextField = "NombreCompleto";
                dd_codProveedor.DataValueField = "CodigoProveedor";
                dd_codProveedor.DataBind();
                dd_codProveedor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un proveedor", ""));

                List<Productos> productos = ObtenerProductosDesdeSession();
                gv_productAgregados.DataSource = productos;
                gv_productAgregados.DataBind();             
            }
        }

        //------------------------------------  POST - POST API COMPRAS
        protected async void btn_registrarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    return;
                }

                List<Productos> productos = ObtenerProductosDesdeSession();

                if (!ValidarProductosCompras(productos))
                {
                    showalert("Tu lista de productos esta vacia.");
                    return;
                }

                DTOCompras compras = CrearCompra(productos);

                string token = await ObtenerTokenAsync("adm", "123");

                string resultado = await RegistrarCompraAsync(compras, token);

                LimpiarCamposRegistrarCompra();
            
                LimpiarCamposADDProductosCompra();
            
                showalert($"Compra Registrado. Nro de registros: {resultado}");
            }
            
            catch (Exception ex)
            {
                // Captura cualquier otra excepción inesperada
                showalert($"Error inesperado: {ex.Message}.");
            }
        }

        private bool ValidarProductosCompras(List<Productos> productos)
        {
            try
            {
                return productos != null && productos.Count > 0;
            
            } catch(Exception ex)
            {
                showalert($"Error validar productos: {ex.Message}");
                return false;
            }
        }

        private DTOCompras CrearCompra(List<Productos> productos)
        {
            decimal impDescuento = 0;
            string importeDescuentoTx = txt_importe_Descuentos.Text.Trim();
            if (string.IsNullOrWhiteSpace(importeDescuentoTx))
            {
                impDescuento = 0;
            }
            else
            {
                if(!decimal.TryParse(importeDescuentoTx, out impDescuento) || impDescuento < 0)
                {
                    showalert("El descuento debe ser un número válido mayor o igual que 0.");
                    return null;
                }
            }

            decimal importeProductosTx = productos.Sum(p => p.importeTotal);
            //decimal importDescuentoTx = decimal.Parse(txt_importe_Descuentos.Text);
            decimal importeTotalTx = importeProductosTx - impDescuento;
            decimal totalIva = (importeProductosTx * 13)/100 ;
            decimal importeNeto = importeProductosTx - totalIva;

            try
            {
                
                return new DTOCompras
                {
                    NumeroCompra = 0,
                    Fecha = "2024-11-30T00:00:00",
                    Referencia = txt_referencia.Text,
                    ImporteProductos = importeProductosTx,
                    ImporteDescuento = impDescuento,
                    ImporteTotal = importeTotalTx,
                    CodigoMoneda = int.Parse(dd_codMoneda.SelectedValue),
                    CodigoProveedor = int.Parse(dd_codProveedor.SelectedValue),
                    CodigoDistribucionGastos = int.Parse(dd_codDistGastos.SelectedValue),
                    Glosa = txt_glosa.Text,
                    
                    Pagos = new PagosDTO
                    {
                        TotalEfectivo = importeProductosTx,
                    },

                    Factura = new FacturaDTO
                    {
                        NIT_CI = txt_nit.Text,
                        RazonSocial = txt_razonSocial.Text,
                        NumeroFactura = txt_nFactura.Text,
                        CodigoAutorizacion = txt_codAutorizacion.Text,
                        CodigoControl = txt_codControl.Text,
                        ImporteTotal = 0, 
                        ImporteDescuento = 0,
                        ImporteGift = 0,
                        ImporteNeto = 0,
                        AplicaCredictoFiscal = bool.Parse(dd_apCredFiscal.SelectedValue)
                    },
                    DetalleProductos = productos.Select(p => new DetalleProductoCompra
                    {
                        NumeroItem = 0,
                        CodigoProducto = p.codigoProducto,
                        Cantidad = p.cantidad,
                        CodigoUnidadMedida = p.codigoUnidadMedida,
                        PrecioUnitario = p.precioUnitario,
                        ImporteDescuento = p.importeDescuento,
                        PorcentajeGasto = p.porcentajeGasto,
                        ImporteTotal = p.importeTotal
                    }).ToList(),
                    

                    Usuario = "adm"
                };
            }
            catch(Exception ex)
            {
                showalert($"Error: {ex.Message}");
                return null;
            }
        }

        private async Task<string> RegistrarCompraAsync(DTOCompras compras, string token)
        {
            try
            {
                NA_APIcompras negocio = new NA_APIcompras();

                string resultado = await negocio.PostComprasAsync(compras, token);
                return resultado;
            }
            catch (Exception ex)
            {
                showalert($"Error al registrar la compra: {ex.Message}");
                return null;
            }
        }

        private void LimpiarCamposRegistrarCompra()
        {
            // Datos compra
            txt_referencia.Text = string.Empty;
            txt_importe_Descuentos.Text = string.Empty;
            dd_codMoneda.SelectedIndex = 0;
            dd_codProveedor.SelectedIndex = 0;
            dd_codDistGastos.SelectedIndex = 0;
            txt_glosa.Text = string.Empty;

            // Datos Factura
            txt_nit.Text = string.Empty;
            txt_razonSocial.Text = "";
            txt_nFactura.Text = "";
            txt_codAutorizacion.Text = string.Empty;
            txt_codControl.Text = string.Empty;
            dd_apCredFiscal.SelectedIndex = 0;

            // GV
            Session["ProductosCompras"] = null;

            gv_productAgregados.DataSource = null;
            gv_productAgregados.DataBind();
        }

        private void LimpiarCamposADDProductosCompra()
        {
            txt_producto.Text = string.Empty;
            txt_cantProducto.Text = string.Empty;
            txt_impDescProd.Text = string.Empty;
            txt_porceGastos.Text = string.Empty;

            Session.Remove("SCnombre");
            Session.Remove("SCcodigoProducto");
            Session.Remove("SCcodigoUnidadMedida");
            Session.Remove("SCprecioUnitario");
        }

        private bool ValidarCampos()
        {
            
            if (string.IsNullOrEmpty(dd_codMoneda.SelectedValue))
            {
                showalert("Por favor, seleccione un tipo de moneda válido.");
                return true;
            }
            if (string.IsNullOrEmpty(dd_codProveedor.SelectedValue))
            {
                showalert("Por favor, seleccione un proveedor válido.");
                return true;
            }
            if (string.IsNullOrEmpty(txt_razonSocial.Text))
            {
                showalert("Por favor, complete el campo razón social.");
                return true;
            }
            if(string.IsNullOrEmpty(txt_nFactura.Text))
            {
                showalert("Por favor, complete el campo número de factura.");
            }    

            return false;
        }


// - - - - - - - - DD OBTENER LISTA PROVEEDORES
        private async Task<List<ProveedorDTO>> ObtenerListCodProveedor(string token)
        {
            try
            {
                NA_APIProveedores negocio = new NA_APIProveedores();
                List<ProveedorDTO> proveedores = await negocio.Get_ListProveedorAsync(token);

                var proveedoresOrd = proveedores.OrderBy(p => p.NombreCompleto).ToList();
                return proveedoresOrd;
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener la lista de proveedores: {ex.Message}");
                return null;
            }
        }


//- - - - - - - - Buscar Productos GV
        protected void txt_producto_TextChanged(object sender, EventArgs e)
        {
            string criterio = txt_producto.Text.Trim();
            if (!string.IsNullOrEmpty(criterio))
            {
                cargarProductosVW(criterio);
            }
        }
        private async void cargarProductosVW(string criterio)
        {
            try
            {
                string token = await ObtenerTokenAsync("adm", "123");

                NA_APIproductos negocio = new NA_APIproductos();
                List<productoCriterioGet> productos = await negocio.get_ProductoCriterioAsync(token, criterio); 

                gv_listProdCompras.DataSource = productos;
                gv_listProdCompras.DataBind();

                gv_listProdCompras.Visible = productos.Count > 0;

            }
            catch (Exception ex)
            {
                showalert($"Error al realizar la busqueda: {ex.Message}");
            }
        }


// - - - - - - - - Seleccionar Producto Del GV
        protected void gv_listProdCompras_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_listProdCompras.SelectedIndex;
            GridViewRow row = gv_listProdCompras.Rows[index];

            string nombre = row.Cells[1].Text;
            string codigoProducto = row.Cells[2].Text;
            int codigoUnidadMedida = int.Parse(row.Cells[3].Text);
            string precioUnitario = row.Cells[4].Text;

            txt_producto.Text = nombre;

            Session["SCnombre"] = nombre;
            Session["SCcodigoProducto"] = codigoProducto;
            Session["SCcodigoUnidadMedida"] = codigoUnidadMedida;
            Session["SCprecioUnitario"] = precioUnitario;

            gv_listProdCompras.Visible = false;
        }


        // - - - - - - - - Elimnar Fila GV
        protected void gv_productAgregados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Eliminar")
            {
                string codigoProducto = (e.CommandArgument.ToString());
                List<Productos> productos = Session["ProductosCompras"] as List<Productos>;
                var productoAEliminar = productos.FirstOrDefault(p => p.codigoProducto == codigoProducto);

                if(productoAEliminar != null)
                {
                    productos.Remove(productoAEliminar);
                }
                Session["ProductosCompras"] = productos;

                gv_productAgregados.DataSource = productos;
                gv_productAgregados.DataBind();
            }
        }


        // - - - - - - - - - btn ADD Productos a la lista GV
        protected void btn_addProd_Click(object sender, EventArgs e)
        {
            Productos nuevoProducto = CrearNuevoProductoCompra();

            if(nuevoProducto != null)
            {
                List<Productos> listaProductos = ObtenerProductosDesdeSession();

                listaProductos.Add(nuevoProducto);

                Session["ProductosCompras"] = listaProductos;

                gv_productAgregados.DataSource = listaProductos;
                gv_productAgregados.DataBind();

                LimpiarCamposADDProductosCompra();
            }
            else
            {
                showalert("Ingrese un producto válido");
            }
        }

        private Productos CrearNuevoProductoCompra()
        {
            try
            {
                if (Session["SCnombre"] ==null || string.IsNullOrWhiteSpace(Session["SCnombre"].ToString()))
                {
                    showalert("Debe buscar y seleccionar el nombre del producto.");
                    return null;
                }
                string producto = (Session["SCnombre"].ToString());
                //- - -
                decimal cantidad = 0;
                if(!decimal.TryParse(txt_cantProducto.Text, out cantidad) || cantidad <= 0)
                {
                    showalert("La cantidad del producto debe ser un número válido mayor que cero.");
                    return null;
                }
                //- - - 
                if (Session["SCcodigoProducto"] == null || string.IsNullOrWhiteSpace(Session["SCcodigoProducto"].ToString()))
                {
                    showalert("El codigo producto no esta disponible en la sessión");
                    return null;
                }
                string codigoProducto = (Session["SCcodigoProducto"].ToString());
                //- - -
                if (Session["SCcodigoUnidadMedida"] == null || string.IsNullOrWhiteSpace(Session["SCcodigoUnidadMedida"].ToString()))
                {
                    showalert("La unidad medida del producto no esta disponible en la sesión");
                    return null;
                }
                int codigoUnidadMedida = int.Parse(Session["SCcodigoUnidadMedida"].ToString());
                //- - -
                if (Session["SCprecioUnitario"] == null || string.IsNullOrWhiteSpace(Session["SCprecioUnitario"].ToString()))
                {
                    showalert("El precio del producto no esta disponible en la sesión");
                    return null;
                }
                decimal precioUnitario = 1;//decimal.Parse(Session["SCprecioUnitario"].ToString());
                //- - - 
                decimal importeDescuento = 0;
                string descuentoText = txt_impDescProd.Text.Trim();

                if (string.IsNullOrWhiteSpace(descuentoText))
                {
                    importeDescuento = 0;
                }
                else
                {
                    if (!decimal.TryParse(descuentoText, out importeDescuento) || importeDescuento < 0)
                    {
                        showalert("El descuento del producto debe ser un número válido mayor o igual que 0.");
                        return null;
                    }
                }
                //- - -
                decimal porcentajeGasto = 0;
                string porcGastoText = txt_porceGastos.Text.Trim();

                if (string.IsNullOrWhiteSpace(porcGastoText))
                {
                    porcentajeGasto = 0;
                }
                else
                {
                    if (!decimal.TryParse(descuentoText, out porcentajeGasto) || porcentajeGasto < 0)
                    {
                        showalert("El porcentaje de gastos debe ser un número válido mayor o igual que 0.");
                        return null;
                    }
                }

                decimal importeTotal = 0;
                importeTotal = (cantidad * precioUnitario) - importeDescuento;

                return new Productos
                {
                    codigoProducto = codigoProducto,
                    nombre = producto,
                    cantidad = cantidad,
                    codigoUnidadMedida = codigoUnidadMedida,
                    precioUnitario = precioUnitario,
                    importeDescuento = importeDescuento,
                    porcentajeGasto = porcentajeGasto,
                    importeTotal = importeTotal
                };

            }
            catch(FormatException ex)
            {
                showalert($"Error en formato de datos: {ex.Message}");
                return null;
            }
            catch(NullReferenceException ex)
            {
                showalert($"Error en referencia nula: {ex.Message}");
                return null;
            }
            catch(Exception ex)
            {
                showalert($"Error al crear el producto: {ex.Message}");
                return null;
            }
        }
        public class Productos
        {
            public int numeroItem { get; set; }
            public string nombre {  get; set; }
            public string codigoProducto {  get; set; }
            public decimal cantidad {  get; set; }
            public int codigoUnidadMedida {  get; set; }
            public decimal precioUnitario {  get; set; }
            public decimal importeDescuento {  get; set; }
            public decimal porcentajeGasto {  get; set; }
            public decimal importeTotal {  get; set; }
        }

        List<Productos> productos = new List<Productos>();

        private List<Productos> ObtenerProductosDesdeSession()
        {
            List<Productos> productos = Session["ProductosCompras"] as List<Productos>;
            if (productos == null)
            {
                productos = new List<Productos>();
            }
            return productos;
        }





// - - - - - - - -
        private void showalert(string message)
        {
            string script = $"alert('{message.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }
        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            NA_APIProveedores negocio = new NA_APIProveedores();
            return await negocio.GetTokenAsync(usuario, password);
        }


    }
}