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
                GridView1.DataBind();
                GridView2.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor ingrese un numero de Egreso valido.');", true);
                return;
            }
            try
            {
                NA_APIinventario apiInv = new NA_APIinventario();
                InventarioEgreso egreso = await apiInv.GetInventarioEgresoDetalleAsync("adm", "123", numTransaccion);

                if(egreso!= null)
                {
                    //encapsulamiento
                    var invEgreso = new List<InventarioEgreso> { egreso };
                    GridView1.DataSource = invEgreso;
                    GridView1.DataBind();

                    if(egreso.DetalleProductos != null && egreso.DetalleProductos.Count > 0)
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
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    GridView2.DataSource = null;
                    GridView2.DataBind();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert ('No se encontraron registros con el numero de egreso proporcionado. ');", true);
                }    
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }
        ///         GET - INVENTARIO EGRESOS  
        protected async void BuscarEgresoInventario_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = TextBox2.Text.Trim();

            NA_APIinventario apiInv = new NA_APIinventario();
            List<Ingresos> egreso = await apiInv.ObtenerEgresosAsync("adm", "123", criterioBusqueda);

            if (egreso != null && egreso.Count > 0)
            {
                GridView3.DataSource = egreso;
                GridView3.DataBind();
            }
            else
            {
                GridView3.DataSource = new List<Ingresos>();
                GridView3.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron registros con el código proporcionado.'); ", true);
            }
        }

        ///         POST - INVENTARIO EGRESOS
        protected async void btn_InventarioEgresoPost2_Click(object sender, EventArgs e)
        {
            string CodigoAlmacen = TextBoxCodigoAlmacen.Text.Trim();
            string MotivoMovimiento = TextBoxMotivoMovimiento.Text.Trim();
            string ItemAnalisis = TextBoxItemAnalisis.Text.Trim();


            // Realiza las validaciones
            if (string.IsNullOrEmpty(CodigoAlmacen))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, Complete el campo Codigo almacen.');", true);
                return;
            }

            if (string.IsNullOrEmpty(MotivoMovimiento))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, Complete el campo Motivo Movimiento.');", true);
                return;
            }

            if (string.IsNullOrEmpty(ItemAnalisis))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, complete el campo Item Analisis.');", true);
                return;
            }// sub

            var egreso = new InventarioEgreso
            {
                NumeroEgreso = 0, 
                Fecha = DateTime.Now, 
                Referencia = TextBoxReferencia.Text,
                CodigoAlmacen = int.Parse(TextBoxCodigoAlmacen.Text),
                MotivoMovimiento = TextBoxMotivoMovimiento.Text,
                ItemAnalisis = int.Parse(TextBoxItemAnalisis.Text),
                Glosa = TextBoxGlosa.Text,
                Usuario = "ADM"
            };

            // Obtener los detalles de productos
            var detalles = new List<DetalleProductoEgre>();

            int rowCount = Request.Form.AllKeys.Length;

            for (int i = 0; i < rowCount; i++)
            {
                if (Request.Form["codigoProducto" + i] != null)
                {
                    detalles.Add(new DetalleProductoEgre
                    {
                        Item = 0,
                        CodigoProducto = Request.Form["codigoProducto" + i], //ob
                        UnidadMedida = int.Parse(Request.Form["unidadMedida" + i]),
                        Cantidad = decimal.Parse(Request.Form["cantidad" + i], CultureInfo.InvariantCulture)

                    });
                }
            }

            egreso.DetalleProductos = detalles;

            // Obtener token y enviar datos
            var api = new NA_APIinventario();
            var token = await api.GetTokenAsync("adm", "123");

            try
            {
                var result = await api.PostInventarioEgresoAsync(egreso, token);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('InventarioEgreso registrado exitosamente.');", true);
                lblResult.Text = $"Numero Egreso: {result}";
            }
            catch (Exception ex)
            {
                lblResult.Text = $"Error: {ex.Message}";
            }
        }
    }
}