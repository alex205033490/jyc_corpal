using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
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

        protected async void btn_getVentas_Click(object sender, EventArgs e)
        {
            string criterio = TextBox1.Text.Trim();

            try
            {
                List<VentaDTO> ventas = await ObtenerVentas(criterio);

                MostrarVentasEnGV(ventas, criterio);
            }
            catch (Exception ex)
            {
                ManejoExcepcion(ex);
            }

        }


        private async Task<List<VentaDTO>> ObtenerVentas(string criterio)
        {
            if (string.IsNullOrEmpty(criterio))
            {
                criterio = null;
            }
            NA_APIventas negocio = new NA_APIventas();
            List<VentaDTO> ventas = await negocio.ObtenerVentaAsync("adm", "123", criterio);

            return ventas;
        }
        private void MostrarVentasEnGV(List<VentaDTO> ventas, string criterio)
        {
            if (ventas != null && ventas.Any())
            {
                gv_getVentas.DataSource = ventas;
                gv_getVentas.DataBind();
            }
            else
            {
                showAlert($"No se encontro ningúna venta con el codigo: {criterio}");
                LimpiarGridView();
            }
        }
        private void LimpiarGridView()
        {
            gv_getVentas.DataSource = new List<VentaDTO>();
            gv_getVentas.DataBind();
        }
        private void ManejoExcepcion(Exception ex)
        {
            showAlert($"Error al obtener las ventas. {ex.Message}");
        }


        // - - - -
        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            NA_APIventas negocio = new NA_APIventas();
            return await negocio.GetTokenAsync(usuario, password);
        }
        private void showAlert(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }
    }
}