using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string criterio = TextBox1.Text.Trim();

            NA_APIinventario apiTras = new NA_APIinventario();
            List<InvTraspasoDTO> traspaso = await apiTras.ObtenerTraspasoAsync("adm", "123", criterio);

            if (traspaso != null && traspaso.Count > 0)
            {
                gv_invTraspaso.DataSource = traspaso;
                gv_invTraspaso.DataBind();
            }
            else
            {
                gv_invTraspaso.DataSource = new List<InvTraspasoDTO>();
                gv_invTraspaso.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron registros con el codigo proporcionado.');", true);
            }
        }


        //------------------------------------      GET - INVENTARIO TRASPASO DETALLE
        protected async void btn_GetinvTraspasoDet_Click(object sender, EventArgs e)
        {
            string numTransaccion = TextBox2.Text.Trim();

            if (string.IsNullOrEmpty(numTransaccion))
            {
                gv_invTraspasoDet.DataBind();
                gv_invTraspasoDet2.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor ingrese un numero de ingreso valido.');", true);
                return;
            }

            try
            {
                NA_APIinventario apiTras = new NA_APIinventario();
                InventarioTraspasoDTO traspaso = await apiTras.GetInventarioTraspasoDetAsync("adm", "123", numTransaccion);

                if (traspaso != null)
                {
                    //encapsulamiento
                    var invTras = new List<InventarioTraspasoDTO> { traspaso };
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
                }

                else
                {
                    gv_invTraspasoDet.DataSource = null;
                    gv_invTraspasoDet2.DataSource = null;

                    gv_invTraspasoDet.DataBind();
                    gv_invTraspasoDet2.DataBind();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert ('No se encontraron registros con el numero de traspaso proporcionado.');", true);

                }
            } catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }

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