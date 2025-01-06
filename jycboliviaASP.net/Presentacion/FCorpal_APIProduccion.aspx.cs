using AjaxControlToolkit;
using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static jycboliviaASP.net.Negocio.NA_APIclientes;
using static jycboliviaASP.net.Negocio.NA_APIproduccion;
using static jycboliviaASP.net.Negocio.NA_APILineaProduccion;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_APIProduccion : System.Web.UI.Page
    {
        private readonly NA_APILineaProduccion _NA_APILineaProduccion = new NA_APILineaProduccion();
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = await ObtenerTokenAsync("adm", "123");
                var lineaProduccion = await ObtenerListLineaProduccion(token);
                dd_lineaProduccion.DataSource = lineaProduccion;
                dd_lineaProduccion.DataTextField = "LineaProducion";
                dd_lineaProduccion.DataValueField = "CodigoUnidadAnalisis";
                dd_lineaProduccion.DataBind();

            }
        }
// - - - - - - - - - - - - - - - - -  POST PRODUCCION
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
                        Item = 0,
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
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error: " + ex);
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Produccion registrado exitosamente.');", true);
        }


        // - - Obtener lista de clientes (Responsable)
        protected void txt_codResponsable_TextChanged(object sender, EventArgs e)
        {
            string criterio = txt_codResponsable.Text.Trim();
            if (!string.IsNullOrEmpty(criterio))
            {
                cargarResponsableUpon(criterio);
            }
        }
        private async void cargarResponsableUpon(string criterio)
        {
            try
            {
                string token = await ObtenerTokenAsync("adm", "123");

                NA_APIclientes negocio = new NA_APIclientes();
                List<ClienteGetDTO> cliente = await negocio.GET_ClientesAsync(token, criterio);

                gv_listResponsables.DataSource = cliente;
                gv_listResponsables.DataBind();

                gv_listResponsables.Visible = cliente.Count > 0;
            }
            catch(Exception ex)
            {
                showAlert($"Error al realizar la busqueda: {ex.Message}");  
            }
        }
        protected void gv_listResponsables_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gv_listResponsables.SelectedIndex;
            GridViewRow row = gv_listResponsables.Rows[index];

            string codigo = row.Cells[1].Text;
            string nombreCompleto = row.Cells[2].Text;

            txt_codResponsable.Text = nombreCompleto;
            Session["ScodigoRespProduccion"] = codigo;

            gv_listResponsables.Visible = false;
        }

        // - - Listar LineaProduccion en DD
        private async Task<List<ListLineaProduccionDTO>> ObtenerListLineaProduccion(string token)
        {
            try
            {
                var lProduccion = await _NA_APILineaProduccion.Get_ListLineaProduccion(token);
                return lProduccion.OrderBy(lp => lp.LineaProducion).ToList();
            }
            catch(Exception ex)
            {
                showAlert($"Erro al obtener la lista de linea de producción: {ex.Message}");
                return null;
            }

        }

        // - - - - - - - - - - - - - - - - -  GET PRODUCCION partePRODUCCION  
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



// - - - - - - - - - - - - - - - - - Otros
        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            NA_APIproduccion negocio = new NA_APIproduccion();
            return await negocio.GetTokenAsync(usuario, password);
        }
        private void showAlert(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

    }
}