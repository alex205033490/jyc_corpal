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

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIInventarioIngresos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //------------------------------------        GET - BUSCAR INVENTARIO INGRESO DETALLE
        protected async void btn_InvIngresoGET_Click(object sender, EventArgs e)
        {
            string numTransaccion = (TextBox1.Text.Trim());
            
            if(string.IsNullOrEmpty(numTransaccion))
            {
                gv_DetalleProductos.DataBind();
                gv_Inventario.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor ingrese un numero de ingreso valido.');", true);
                return;
            }

            try
            {
                NA_APIinventario apiInv = new NA_APIinventario();
                InventarioIngreso ingreso = await apiInv.GetInventarioIngresoDetalleAsync("adm", "123", numTransaccion);

                if (ingreso != null)
                {
                    //encapsulamiento
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
                else
                {
                    gv_Inventario.DataSource = null;
                    gv_DetalleProductos.DataSource = null;
                    gv_DetalleProductos.DataBind();
                    gv_Inventario.DataBind();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert ('No se encontraron registros con el numero de ingreso proporcionado.');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }
        //------------------------------------      GET - BUSCAR INVENTARIO INGRESO
        protected async void btn_invIngreso2_Click(object sender, EventArgs e)
        {

            string numTransaccion = TextBox2.Text.Trim();

            NA_APIinventario apiInv = new NA_APIinventario();
            List<Ingresos> ingresos = await apiInv.ObtenerIngresosAsync("adm", "123", numTransaccion);

            if (ingresos != null && ingresos.Count > 0)
            {
                gv_invIngresos2.DataSource = ingresos;
                gv_invIngresos2.DataBind();
            }
            else
            {
                gv_invIngresos2.DataSource = new List<Ingresos>();
                gv_invIngresos2.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron registros con el codigo proporcionado.');", true);
            }


        }

        //------------------------------------      POST - POST INVENTARIO INGRESO
        protected async void btn_registrarIngreso_Click(object sender, EventArgs e)
        {
            string CodigoAlmacen = txt_codAlmacen.Text.Trim();
            string MotivoMovimiento = txt_motMovimiento.Text.Trim();
            string ItemAnalisis = txt_itemAnalisis.Text.Trim();


            // Realiza las validaciones
            if (string.IsNullOrEmpty(CodigoAlmacen))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, ingrese un codigo almacen.');", true);
                return;
            }

            if (string.IsNullOrEmpty(MotivoMovimiento))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, complete el campo motivoMovimiento.');", true);
                return;
            }

            if (string.IsNullOrEmpty(ItemAnalisis))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, complete el campo ItemAnalisis.');", true);
                return;
            }// sub

            var ingreso = new InventarioIngreso
            {
                NumeroIngreso = 0,
                Fecha = DateTime.Now,
                Referencia = txt_Referencia.Text,
                CodigoMoneda = int.Parse(dd_codMoneda.SelectedValue),
                CodigoAlmacen = int.Parse(txt_codAlmacen.Text), // no op
                MotivoMovimiento = txt_motMovimiento.Text, // no op
                ItemAnalisis = int.Parse(txt_itemAnalisis.Text), // no op
                Glosa = txt_glosa.Text,
                Usuario = "ADM"
            };

            //obtener los detalles de productos
            var detalles = new List<DetalleProductoIngre>();
            int rowCount = Request.Form.AllKeys.Length;
            for (int i = 0; i < rowCount; i++)
            {
                if (Request.Form["item" + i] != null)
                {
                    detalles.Add(new DetalleProductoIngre
                    {
                        Item = 0,
                        CodigoProducto = Request.Form["codigoProducto" + i], // no op
                        UnidadMedida = int.Parse(Request.Form["unidadMedida" + i]),
                        Cantidad = decimal.Parse(Request.Form["cantidad" + i]), // no op
                        CostoUnitario = decimal.Parse(Request.Form["costoUnitario" + i]), // no op
                        CostoTotal = decimal.Parse(Request.Form["cantidad" + i]) * decimal.Parse(Request.Form["costoUnitario" + i])
                    });
                }
            }
            ingreso.DetalleProductos = detalles;

            // obtener token y enviar datos
            var api = new NA_APIinventario();
            var token = await api.GetTokenAsync("adm", "123");

            try
            {
                var result = await api.PostInventarioIngresoAsync(ingreso, token);
                lblResult.Text = $"Numero Ingreso: {result}";
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('InventarioIngreso registrado exitosamente.');", true);
        }
    }
}