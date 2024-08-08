using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            string producto = tx_producto.Text;

            NA_PruebaAPI pp = new NA_PruebaAPI();
            string resultado = pp.get_producto("adm","123",producto);
            tx_resultado.Text = resultado;
        }
    }
}