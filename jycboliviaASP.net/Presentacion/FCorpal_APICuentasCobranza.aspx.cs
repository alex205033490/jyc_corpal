using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using static jycboliviaASP.net.Negocio.NA_APICuentasCobranza;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APICuentasCobranza : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btn_getCuentas_Click(object sender, EventArgs e)
        {
            NA_APICuentasCobranza apiCobr = new NA_APICuentasCobranza();
            List<CuentaDTO> cuentaDTO = await apiCobr.ObtenerCobranzaAsync("adm", "123");

            if (cuentaDTO != null && cuentaDTO.Count > 0)
            {
                gv_Cuentas.DataSource = cuentaDTO;
                gv_Cuentas.DataBind();
            }
            else
            {
                gv_Cuentas.DataSource = new List<CuentaDTO>();
                gv_Cuentas.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert'(No se encontraron registros.');", true);
            }
        }


        protected void btn_buscarCobranza_Click(object sender, EventArgs e)
        {

        }

        protected void btn_PostCobranza_Click(object sender, EventArgs e)
        {

        }
    }
}
