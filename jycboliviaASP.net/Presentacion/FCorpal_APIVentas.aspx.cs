using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIventas;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btn_getVentaSimple_Click(object sender, EventArgs e)
        {
            NA_APIventas apiVent = new NA_APIventas();
            List<VentaSimpleDTO> ventaSimpleDTO = await apiVent.ObtenerVentaSimpleAsync("ADM", "123");

            if (ventaSimpleDTO != null && ventaSimpleDTO.Count > 0)
            {
                gv_VentaSimple.DataSource = ventaSimpleDTO;
                gv_VentaSimple.DataBind();
            }
            else
            {
                gv_VentaSimple.DataSource = new List<VentaSimpleDTO>();
                gv_VentaSimple.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron registros.');", true);
            }




        }
    }
}