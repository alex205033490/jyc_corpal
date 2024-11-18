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

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIInventarioEgresos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        ///         GET - INVENTARIO EGRESOS DETALLE 
        protected async void BuscarEgresoInventarioDetalle_Click(object sender, EventArgs e)
        {
            string numTransaccion = (TextBox1.Text.Trim());

            if (string.IsNullOrEmpty(numTransaccion))
            {
                showalert("Por favor, Ingrese un número de egreso válido.");
                LimpiarGrids();
                return;
            }
            try
            {
                
                NA_APIinventario apiI = new NA_APIinventario();
                InventarioEgreso egreso = await apiI.GetInventarioEgresoDetalleAsync("adm", "123", numTransaccion);

                if(egreso!= null)
                {
                    MostrarDetallesInventario(egreso);
                }
                else
                {
                    showalert("No se encontraron registros con el número de egreso proporcionado.");
                    LimpiarGrids();
                }    
            }
            catch (ApplicationException ex)
            {
                showalert($"Error: {ex.Message}");
                LimpiarGrids();
            }
            catch (Exception ex)
            {
                showalert($"Ha ocurrido un error inesperado: {ex.Message}");
                LimpiarGrids();
            }
        }
        
        private void MostrarDetallesInventario(InventarioEgreso egreso)
        {
            var IEgreso = new List<InventarioEgreso> { egreso };
            GridView1.DataSource = IEgreso;
            GridView1.DataBind();

            if (egreso.DetalleProductos != null && egreso.DetalleProductos.Count >0)
            {
                GridView2.DataSource = egreso.DetalleProductos;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
            }

        }
        private void LimpiarGrids()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
        
        ///         GET - INVENTARIO EGRESOS  
        protected async void BuscarEgresoInventario_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = TextBox2.Text.Trim();

            try
            {
                NA_APIinventario apiInv = new NA_APIinventario();
                List<Ingresos> egreso = await apiInv.ObtenerEgresosAsync("adm", "123", criterioBusqueda);
                GridView3.DataSource = egreso.Any() ? egreso : new List<Ingresos>();
                GridView3.DataBind();

                if (egreso.Any())
                {
                    showalert("No se encontraron registros con el código proporcionado");
                }
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener los egresos: {ex.Message}");
            }
        }

        ///         POST - INVENTARIO EGRESOS
        protected async void btn_InventarioEgresoPost2_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;
            var egreso = CrearEgreso();

            try
            {
                var result = await RegistrarEgreso(egreso);

                if (result != null)
                {
                    showalert($"Egreso Registrado. Número de Egreso: {result}");
                    LimpiarCampos();
                }
                else
                {
                    showalert("Ocurrio un error al registrar el Egreso");
                }
            }
            catch (Exception ex)
            {
                showalert($"Error inesperado: {ex.Message}");
            }
        }

        private InventarioEgreso CrearEgreso()
        {
            var egreso = new InventarioEgreso
            {
                NumeroEgreso = 0,
                Fecha = DateTime.Now,
                Referencia = TextBoxReferencia.Text,
                CodigoAlmacen = int.Parse(TextBoxCodigoAlmacen.Text),
                MotivoMovimiento = TextBoxMotivoMovimiento.Text,
                ItemAnalisis = int.Parse(TextBoxItemAnalisis.Text),
                Glosa = TextBoxGlosa.Text,
                Usuario = "adm",
                DetalleProductos = obtenerDetalleProductos()
            };
            return egreso;
        }
        private List<DetalleProductoEgre> obtenerDetalleProductos()
        {
            var detalles = new List<DetalleProductoEgre>();
            int rowCount = Request.Form.AllKeys.Length;

            try
            {
                for (int i = 0; i< rowCount; i++)
                {
                    if (Request.Form["Item" +i] != null)
                    {
                        var detalle = new DetalleProductoEgre
                        {
                            Item = 0,
                            CodigoProducto = Request.Form["codigoProducto"],
                            UnidadMedida = int.Parse(Request.Form["unidadMedida"]),
                            Cantidad = int.Parse(Request.Form["cantidad"])
                        };
                        detalles.Add(detalle);
                    }
                        //Item, CodigoProducto, UnidadMedida, Cantidad
                }
            }
            catch(Exception ex)
            {
                showalert($"Error al obtener detalles de productos: {ex.Message}");
                return null;
            }
            return detalles;

        }
        private async Task<string> RegistrarEgreso(InventarioEgreso egreso)
        {
            try
            {
                var api = new NA_APIinventario();
                var token = await api.GetTokenAsync("adm", "123");

                var result = await api.PostInventarioEgresoAsync(egreso, token);
                return result;
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
            if (string.IsNullOrEmpty(TextBoxCodigoAlmacen.Text.Trim()))
            {
                showalert("Por favor, complete el campo código almacén");
                return false;
            }

            if (string.IsNullOrEmpty(TextBoxMotivoMovimiento.Text.Trim()))
            {
                showalert("Por favor, Complete el campo Motivo Movimiento.");
                return false;
            }

            if (string.IsNullOrEmpty(TextBoxItemAnalisis.Text.Trim()))
            {
                showalert("Por favor, complete el campo Ítem Análisis.");
                return false;
            }// sub
            return true;
        }
        private void LimpiarCampos()
        {
            TextBoxReferencia.Text = string.Empty;
            TextBoxCodigoAlmacen.Text = string.Empty;
            TextBoxMotivoMovimiento.Text = string.Empty;
            TextBoxItemAnalisis.Text = string.Empty;
            TextBoxGlosa.Text = string.Empty;

        }

        private void showalert(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{mensaje}');", true);
        }
    }
}