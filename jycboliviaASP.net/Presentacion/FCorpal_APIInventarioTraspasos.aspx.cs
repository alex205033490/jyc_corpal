using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIinventario;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIInventarioTraspasos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //----------------------------------        GET - INVENTARIO TRASPASO
        protected async void btn_GetinvTraspaso_Click(object sender, EventArgs e)
        {
            try
            {
                string criterio = TextBox1.Text.Trim();
                List<InvTraspasoDTO> traspasos = await ObtenerTraspasosAsync(criterio);
                actualizarVwTraspaso(traspasos);

            }catch(Exception ex)
            {
                showAlert("Ocurrió un error al realizar la búsqueda. Intente nuevamente más tarde.");
                LogError(ex);
            }

        }
        private void actualizarVwTraspaso(List<InvTraspasoDTO> traspasos)
        {
            gv_invTraspaso.DataSource = null;
            gv_invTraspaso.DataBind();

            if (traspasos != null && traspasos.Count > 0)
            {
                gv_invTraspaso.DataSource = traspasos;
                gv_invTraspaso.DataBind();
            }
            else
            {
                gv_invTraspaso.DataSource = traspasos;
                gv_invTraspaso.DataBind();
                showAlert("No se encontraron registros con el código proporcionado");
            }
        }
        private async Task<List<InvTraspasoDTO>> ObtenerTraspasosAsync(string criterio)
        {
            try
            {
                NA_APIinventario negocio = new NA_APIinventario();
                var result = await negocio.ObtenerTraspasoAsync("adm", "123", criterio);

                if (result == null)
                {
                    throw new Exception("La llamada a la API no devolvió resultados.");
                }
                return result;
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }


        //------------------------------------      GET - INVENTARIO TRASPASO DETALLE
        protected async void btn_GetinvTraspasoDet_Click(object sender, EventArgs e)
        {
            try
            {
                string numTransaccion = TextBox2.Text.Trim();

                if (!IsValidTransactionnumber(numTransaccion))
                {
                    showAlert("Por favor ingrese un número de transacción válido.");
                    LimpiarGvDet();
                    return;
                }
                await CargarDetallesTraspasosDet(numTransaccion);

            } catch (Exception ex)
            {
                showAlert($"Ocurrio un error: {ex.Message}");
                LimpiarGvDet();
            }

        }
        private bool IsValidTransactionnumber(string numTransaccion)
        {
            return !string.IsNullOrEmpty(numTransaccion);
        }
        private async Task CargarDetallesTraspasosDet(string numTransaccion)
        {
            try
            {
                NA_APIinventario negocio = new NA_APIinventario();
                InventarioTraspasoDTO traspaso = await negocio.GetInventarioTraspasoDetAsync("adm", "123", numTransaccion);

                if (traspaso != null)
                {
                    CargarGvDet(traspaso);
                }
                else
                {
                    LimpiarGvDet();
                    showAlert("No se encontraron registros con el número de transacción proporcionado.");
                }
            } catch (Exception ex)
            {
                showAlert($"{ex.Message}");
                LimpiarGvDet();
            }
        }
        private void CargarGvDet(InventarioTraspasoDTO traspaso)
        {
            try
            {
                var invTras = new List<InventarioTraspasoDTO> {traspaso};
                gv_invTraspasoDet.DataSource = invTras;
                gv_invTraspasoDet.DataBind();

                if (traspaso.DetalleProductos != null && traspaso.DetalleProductos.Count > 0)
                {
                    gv_invTraspasoDet2.DataSource = traspaso.DetalleProductos;
                    gv_invTraspasoDet2.DataBind();
                }
                else
                {
                    gv_invTraspasoDet2.DataSource = null;
                    gv_invTraspasoDet2.DataBind();
                }
            } catch (Exception ex)
            {
                showAlert($"Error al cargar los datos en el Grid :{ex.Message}");
            }
            
        }
        private void LimpiarGvDet()
        {
            gv_invTraspasoDet.DataSource = null;
            gv_invTraspasoDet2.DataSource = null;
            gv_invTraspasoDet.DataBind();
            gv_invTraspasoDet2.DataBind();
        }


        // - - - - - - - - - - - - - - - - - - - - - - 
        private void showAlert(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{mensaje}');", true);
        }
        private void LogError(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message} \n {ex.StackTrace}");
        }
    }
}








/*protected async void btn_registrarTraspaso_Click(object sender, EventArgs e)
{
    var traspaso = new InventarioTraspasoDTO
    {
        NumeroTraspasos = 0,
        Fecha = DateTime.Now,
        Referencia = txt_referencia.Text,
        CodigoAlmacenDestino = int.Parse(txt_codAlmacenDest.Text),
        Glosa = txt_glosa.Text,
        Usuario = "adm",
    };

    // obtener los detalles de productos
    var detalles = new List<DetalleProductoTraspasoDTO>();
    int rowCount = Request.Form.AllKeys.Length;
    for (int i = 0; i < rowCount; i++)
    {
        if (Request.Form["item" + i] != null)
        {
            detalles.Add(new DetalleProductoTraspasoDTO
            {
                Item = int.Parse(Request.Form["item" + i]),
                CodigoProducto = Request.Form["codigoProducto" + i],
                UnidadMedida = int.Parse(Request.Form["unidadMedida" + i]),
                Cantidad = decimal.Parse(Request.Form["cantidad" + i])
            });
        }
    }
    traspaso.DetalleProductos = detalles;

    // obtener token y enviar datos
    var api = new NA_APIinventario();
    var token = await api.GetTokenAsync("adm", "123");

    try
    {
        var result = await api.PostInventarioTraspasoAsync(traspaso, token);
        lblresult.Text = $"Numero Ingreso: {result}";
    }
    catch (Exception ex) 
    {
        Response.Write($"Error: {ex.Message}");
    }
    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('InventarioTraspaso registrado exitosamente.');", true);

}


}
}*/