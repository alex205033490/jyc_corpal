using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_PruebaAPI;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = "adm"; 
                string pass = "123";    
                string producto = tx_producto.Text.Trim(); 

                NA_PruebaAPI pp = new NA_PruebaAPI();
                string resultado = pp.get_producto(usuario, pass, producto);
                tx_resultado.Text = resultado;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                tx_resultado.Text = $"Error: {ex.Message}";
            }

        }
      
        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            string numTransaccion = tx_Ntransaccion1.Text.Trim();
            NA_PruebaAPI pp = new NA_PruebaAPI();

            
            List<Ingreso> ingresos = pp.get_InventarioIngresos("adm", "123", numTransaccion);

            if (ingresos != null && ingresos.Count > 0)
            {
                gv_Inventario.DataSource = ingresos;
                gv_Inventario.DataBind();
            }
            else
            {
                gv_Inventario.DataSource = new List<Ingreso>();
                gv_Inventario.DataBind();

            }
<<<<<<< HEAD
=======
        }

        protected async void Button_Post_Click(object sender, EventArgs e)
        {
            var na_pruebaapi = new NA_PruebaAPI();
            string usuario = "adm";
            string password = "123";

            try
            {
                var token = await na_pruebaapi.GetTokenAsync(usuario, password);
                var ingreso = new InventarioIngreso
                {
                    NumeroIngreso = int.Parse(TextBox1.Text),
                    Fecha = DateTime.Now,
                    Referencia = TextBox3.Text,
                    CodigoMoneda = int.Parse(TextBox4.Text),
                    CodigoAlmacen = int.Parse(TextBox5.Text),
                    MotivoMovimiento = TextBox6.Text,
                    ItemAnalisis = int.Parse(TextBox7.Text),
                    Glosa = TextBox8.Text,
                    DetalleProductos = new List<DetalleProducto>
                {
                    new DetalleProducto
                    {
                        Item = int.Parse(TextBox9.Text),
                        CodigoProducto = TextBox10.Text,
                        UnidadMedida = int.Parse(TextBox11.Text),
                        Cantidad = decimal.Parse(TextBox12.Text),
                        CostoUnitario = decimal.Parse(TextBox13.Text),
                        CostoTotal = decimal.Parse(TextBox12.Text)*decimal.Parse(TextBox13.Text) ,
                    }
                },
                    Usuario = "ADM"
                };
                var result = await na_pruebaapi.PostInventarioIngresoAsync(ingreso, token);
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }


            
        }

        protected async void Btn_buscarInventario_Click(object sender, EventArgs e)
        {
            var na_pruebaapi = new NA_PruebaAPI();
            string usuario = "adm";
            string pass = "123";
            int criterio;

            // validar que el criterio es un numero entero
            if (!int.TryParse(txt_buscarInventario.Text, out criterio))
            {
                Response.Write("Criterio no es un numero valido.");
                return;
            }
            try 
            {
                // obtener el token
                string token = await na_pruebaapi.GetTokenAsync(usuario, pass);

                // obtener los datos del inventario
                var resultado = await na_pruebaapi.Get_InventarioIngresosDetalleAsync(usuario, criterio, token);

                // mostrar los datos en el GridView
                DataTable dt = na_pruebaapi.ConvertToDataTable(resultado);

                gvInventarioIngresos.DataSource = dt;
                gvInventarioIngresos.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }

>>>>>>> origin/modulo3
        }
    }
}


 

