using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
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
        }
    }
}