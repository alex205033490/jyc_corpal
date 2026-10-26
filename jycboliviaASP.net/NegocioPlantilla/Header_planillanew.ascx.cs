using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.NegocioPlantilla
{
    public partial class Header_planillanew : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       //     lb_ciudad.Text = Session["BaseDatos"].ToString();
            CargarTipoCambio();
        }

        private void LimpiarTipoCambio(string mensaje)
        {
            lblTipoCambio.Text = "--";
            lblOficial.Text = "--";
            lblBlue.Text = "--";
            lblUFV.Text = "--";
            lblFechaTC.Text = mensaje;
        }

        /*
       private void CargarTipoCambio()
       {
           try
           {
               NA_Importacion di = new NA_Importacion();
               DataSet dato = di.get_tipocambioActual();

               if (dato == null || dato.Tables.Count == 0 || dato.Tables[0].Rows.Count == 0)
               {
                   LimpiarTipoCambio("Sin información");
                   return;
               }

               DataRow dr = dato.Tables[0].Rows[0];

               lblTipoCambio.Text = dr["tipoCambio"].ToString();
               lblOficial.Text = dr["oficial"].ToString();
               lblBlue.Text = dr["Blue"].ToString();
               lblUFV.Text = dr["UFV"].ToString();
               lblFechaTC.Text = dr["FechaGrabacion"] + " " + dr["HoraGrabacion"];
           }
           catch
           {
               LimpiarTipoCambio("Error al cargar");
           }
       }
       */


        private void CargarTipoCambio()
        {
            try
            {
                NA_TipoCambioService servicio = new NA_TipoCambioService();

                TipoCambio oficial = servicio.ObtenerTipoCambioOficial();
                TipoCambio blue = servicio.ObtenerTipoCambioBlue();

                // Tipo de cambio oficial
                if (oficial != null)
                {
                    lblTipoCambio.Text = oficial.venta?.ToString("F2") ?? "N/A";
                    lblOficial.Text = oficial.venta?.ToString("F2") ?? "N/A";
                    // Fecha y hora del registro
                    lblFechaTC.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                }
                else
                {
                    lblTipoCambio.Text = "N/A";
                    lblOficial.Text = "N/A";
                    lblFechaTC.Text = "N/A";
                }

                // Tipo de cambio Blue
                lblBlue.Text = blue?.venta?.ToString("F2") ?? "N/A";
                // UFV (pendiente de implementar)
                lblUFV.Text = "0.00";

                string baseDeDatos = Session["BaseDatos"].ToString();

                if (baseDeDatos.Trim().Equals("Asuncion-Nuevo", StringComparison.OrdinalIgnoreCase) ||
                  baseDeDatos.Trim().Equals("Prueba", StringComparison.OrdinalIgnoreCase))
                {
                    TipoCambioPY oficialpy = servicio.ObtenerTipoCambioOficialPY();
                    lblTipoCambio.Text = oficialpy.dolarpy?.bcp?.venta?.ToString("F2") ?? "N/A";
                    lblOficial.Text = lblTipoCambio.Text;
                    // Fecha y hora del registro
                    lblFechaTC.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                }


            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error si tienes un Log
                // Log.Error(ex);

                LimpiarTipoCambio("Error al cargar");
            }
        }

    }
}