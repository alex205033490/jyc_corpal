using AjaxControlToolkit;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIproduccion;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIProduccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //      POST PRODUCCION partePRODUCCION
        protected async void btn_Insert_parteProd_Click(object sender, EventArgs e)
        {
            var parteProd = new ParteProduccionDTO
            {
                NumeroParteProduccion = 0,
                Fecha = DateTime.Now,
                Referencia = txt_referencia.Text,
                CodigoResponsable = int.Parse(txt_codResponsable.Text),
                ItemAnalisis = int.Parse(txt_itemAnalisis.Text),
                LineaProduccion = int.Parse(txt_itemAnalisis.Text),
                RealizaDescarga = Boolean.Parse(dd_realDescarga.Text),
                Glosa = txt_glosa.Text,
                Usuario = "adm"
            };
            //  obtener los detalles de productos
            var detalles = new List<ProductoParteProduccionDTO>();
            int rowCount = Request.Form.AllKeys.Length;

            for (int i = 0; i < rowCount; i++)
            {
                if (Request.Form["item" + i] != null)
                {
                    detalles.Add(new ProductoParteProduccionDTO
                    {
                        Item = int.Parse(Request.Form["item" + i]),
                        CodigoProducto = Request.Form["codigoProducto" + i],
                        Cantidad = int.Parse(Request.Form["cantidad" + i]),
                        UnidadMedida = int.Parse(Request.Form["unidadMedida" + i]),
                        CodigoReceta = Request.Form["codigoReceta" + i]
                    });
                }
            }
            parteProd.Detalle = detalles;

            // obtener token y enviar datos
            var api = new NA_APIproduccion();
            var token = await api.GetTokenAsync("adm", "123");

            try
            {
                var result = await api.PostParteProduccionAsync(parteProd, token);
                lblResult.Text = $"Numero Produccion: {result}";
            }
            catch (Exception ex) 
            {
                lblResult.Text = $"Error: {ex.Message}";
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Produccion registrado exitosamente.');", true);
        }
        //      GET PRODUCCION partePRODUCCION  
        protected async void btn_buscProduccion_Click(object sender, EventArgs e)
        {
            string numProduccion = txt_numProduccion1.Text.Trim();
            
            if (string.IsNullOrEmpty(numProduccion)) 
            {
                gv_produccion.DataBind();
                gv_detalle.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor ingrese un numero de produccion valido.');", true);
                return;
            }
            try
            {
                NA_APIproduccion apiProd = new NA_APIproduccion();
                ParteProduccionDTO produccion = await apiProd.GetProduccionAsync("adm", "123", numProduccion);

                if (produccion != null)
                {
                    //encapsulamiento
                    var parProduccion = new List<ParteProduccionDTO> { produccion };
                    gv_produccion.DataSource = parProduccion;
                    gv_produccion.DataBind();

                    if (produccion.Detalle != null && produccion.Detalle.Count > 0)
                    {
                        gv_detalle.DataSource = produccion.Detalle;
                        gv_detalle.DataBind();
                    }
                    else
                    {
                        gv_detalle.DataSource = null;
                        gv_detalle.DataBind();
                    }
                }
                else
                {
                    gv_detalle.DataSource = null;
                    gv_detalle.DataBind();
                    gv_produccion.DataSource = null;
                    gv_produccion.DataBind();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert","alert ('");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}')", true);
            }



        }
    }
}