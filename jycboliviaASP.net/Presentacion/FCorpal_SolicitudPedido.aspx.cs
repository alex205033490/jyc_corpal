using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
using System.Drawing;
using jycboliviaASP.net.NegocioApi;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using static jycboliviaASP.net.Negocio.NA_APIclientes;
using static jycboliviaASP.net.Negocio.NA_APIproductos;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_SolicitudPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(118) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                DataTable datoRepuesto = new DataTable();
                datoRepuesto.Columns.Add("Codigo", typeof(string));
                datoRepuesto.Columns.Add("Producto", typeof(string));
                datoRepuesto.Columns.Add("Medida", typeof(string));
                datoRepuesto.Columns.Add("Tipo", typeof(string));                  
                datoRepuesto.Columns.Add("Precio", typeof(string));
                datoRepuesto.Columns.Add("Cantidad", typeof(string));
                datoRepuesto.Columns.Add("PrecioTotal", typeof(string));
                datoRepuesto.Columns.Add("ItemPackFerial", typeof(Boolean));

                gv_adicionados.DataSource = datoRepuesto;
                gv_adicionados.DataBind();
                Session["listaSolicitudProducto"] = datoRepuesto;
            }
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NCorpal_SolicitudEntregaProducto ncc = new NCorpal_SolicitudEntregaProducto();
            tx_nrodocumento.Text = ncc.get_siguentenumeroRecibo(codUser);
            tx_solicitante.Text = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
        }

        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(permiso, codUser);
        }


        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProductos(string prefixText, int count)
        {
            string nombreProducto = prefixText;
            
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos(nombreProducto);
         
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;
            
            /*
            NA_endpoints napi = new NA_endpoints();
            dynamic tuplas = napi.get_productoAlmacen(nombreProducto, "adm", "123");            
            JArray rowsArray = (JArray)tuplas["Resultado"];
            string[] lista = new string[rowsArray.Count];
            int i = 0;
            foreach (JObject obj in rowsArray)
            {
                lista[i] = obj["Descripcion"].ToString() ;
                i++;
            }
            
            return lista;
            */
        }

        public string convertidorFecha(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'" + _fecha + "'";
            }
            else
                return "null";
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string producto = tx_producto.Text;
            buscarProductos(producto);
        }

        private void buscarProductos(string producto)
        {
            NCorpal_SolicitudEntregaProducto npp = new NCorpal_SolicitudEntregaProducto();
            DataSet datos = npp.get_mostrarProductos(producto);
            gv_Productos.DataSource = datos;
            gv_Productos.DataBind();
        }

        protected void bt_adicionar_Click(object sender, EventArgs e)
        {
            adicionar_productos();
        }

        private void adicionar_productos()
        {
            float cantidad;
            float.TryParse(tx_cantidadProducto.Text, out cantidad);            
            bool itemPackFerial = cb_itemPackFerial.Checked;

            if(cantidad > 0){
                DataTable datoRepuesto = Session["listaSolicitudProducto"] as DataTable;
                CheckBox cb = null;
                for (int i = 0; i < gv_Productos.Rows.Count; i++)
                {
                    cb = (CheckBox)gv_Productos.Rows[i].Cells[1].FindControl("CheckBox1");
                    if (cb != null && cb.Checked)
                    {
                        string codigo = HttpUtility.HtmlDecode(gv_Productos.Rows[i].Cells[1].Text);
                        string producto = gv_Productos.Rows[i].Cells[2].Text;
                        string Medida = HttpUtility.HtmlDecode(gv_Productos.Rows[i].Cells[3].Text);
                        float precio;
                        float.TryParse(gv_Productos.Rows[i].Cells[4].Text, out precio);
                        string tipo = dd_tipoSolicitud.SelectedItem.Text;
                        float StockProducto;
                        float.TryParse(gv_Productos.Rows[i].Cells[5].Text, out StockProducto);
                        float StockPackFerial;
                        float.TryParse(gv_Productos.Rows[i].Cells[6].Text, out StockPackFerial);
                        /*
                        float StockLimite = 0;

                        if (itemPackFerial == true)
                        {
                            StockLimite = StockPackFerial;
                        }else
                            StockLimite = StockProducto;
                        
                        if (cantidad <= StockLimite) {  */
                            DataRow tupla = datoRepuesto.NewRow();
                            tupla["Codigo"] = codigo;
                            tupla["producto"] = producto;
                            tupla["Medida"] = Medida;
                            tupla["Tipo"] = tipo;
                            tupla["Precio"] = precio;
                            tupla["Cantidad"] = cantidad;
                            tupla["PrecioTotal"] = (precio * cantidad);

                            if (itemPackFerial == true)
                            {
                                tupla["ItemPackFerial"] = itemPackFerial;
                                tupla["Medida"] = "UNIDAD";
                                tupla["Tipo"] = "ITEM PACK FERIAL";
                            }
                            else
                                tupla["ItemPackFerial"] = false;

                            datoRepuesto.Rows.Add(tupla);
                      //  }                        
                    }
                }
                gv_adicionados.DataSource = datoRepuesto;
                gv_adicionados.DataBind();
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Cantidad igual 0') </script>");
            
        }

        protected void bt_guardar_Click(object sender, EventArgs e)
        {
            guardarSolicitud();
        }

        private void guardarSolicitud()
        {
            DataTable datoRepuesto = Session["listaSolicitudProducto"] as DataTable;
            if(datoRepuesto.Rows.Count > 0){
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codpersolicitante = Nresp.getCodUsuario(usuarioAux, passwordAux);

                string nroboleta = tx_nrodocumento.Text;
                string personalsolicitud = tx_solicitante.Text;
                string fechaentrega = convertidorFecha(tx_fechaEntrega.Text);
                string horaentrega = tx_horaEntrega.Text;

                NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
                string repuestosSolicitados = ""; 

                if (nss.set_guardarSolicitud(nroboleta, fechaentrega, horaentrega, personalsolicitud, codpersolicitante, true))
                {
                    int ultimoinsertado = nss.getultimaSolicitudproductoInsertado(codpersolicitante);

                    double montoTotal = 0;
                    for (int i = 0; i < datoRepuesto.Rows.Count; i++)
                    {
                        int codProducto = Convert.ToInt32(datoRepuesto.Rows[i]["codigo"].ToString());
                        double cantidad = Convert.ToDouble(datoRepuesto.Rows[i]["cantidad"].ToString());
                        double preciocompra = Convert.ToDouble(datoRepuesto.Rows[i]["Precio"].ToString());

                        string producto = datoRepuesto.Rows[i]["producto"].ToString();
                        string Medida = datoRepuesto.Rows[i]["Medida"].ToString();
                        string Tipo = datoRepuesto.Rows[i]["Tipo"].ToString();
                        double total = preciocompra * cantidad;

                        repuestosSolicitados = repuestosSolicitados + producto + " cant.=" + cantidad.ToString() + ", Medida=" + Medida + ", Tipo=" + Tipo + "<br>";
                        nss.insertarDetalleSolicitudProducto(ultimoinsertado, codProducto, cantidad, preciocompra, total, Tipo, Medida);
                        montoTotal = montoTotal + total;
                    }

                    nss.actualizarmontoTotal(ultimoinsertado, montoTotal);
                    //----------------envio de correo-------------
                   /* string asunto = "(Corpal)" + " Solicitud de Pedido - Solicitante = " + personalsolicitud ;
                    string cuerpo = "Correo Automatico. <br><br>" +
                                    "Se realizo la solicitud de los siguientes productos : <br>" +
                                    "Nro Recibo = " + nroboleta + "<br>" +
                                    "Solicitante = " + personalsolicitud + "<br>" +
                                    "Fecha Entrega = " + fechaentrega + " <br>" +
                                    "Hora Entrega = " + horaentrega + " <br>" +                                    
                                    "Repuesto Solicitado: <br>" +
                                    repuestosSolicitados +
                                    "<br><br><br>" +
                                    "Fin de Mensaje.";
                    NA_EnvioCorreo ncorreo = new NA_EnvioCorreo();                    
                    bool bandera = ncorreo.enviar_Correo_SolicitudProducto(asunto, cuerpo); */
                    //----------------fin envio de correo---------                    
                    limpiarDatos();
                    buscarProductos("");
                    Session["codigoSolicitudProducto"] = ultimoinsertado;
                    Response.Redirect("../Presentacion/FCorpal_ReporteSolicitudProducto.aspx");
                    //Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: No se pudo realizar la Solicitud') </script>");

            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: No tiene pedido') </script>");
            
        }

        protected void gv_adicionados_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_adicionados.EditIndex = -1;
            DataTable datoRepuesto = Session["listaSolicitudProducto"] as DataTable;
            gv_adicionados.DataSource = datoRepuesto;
            gv_adicionados.DataBind();
            
        }

        protected void gv_adicionados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex;

            DataTable datoRepuesto = Session["listaSolicitudProducto"] as DataTable;
            datoRepuesto.Rows[index].Delete();
            datoRepuesto.AcceptChanges();

            gv_adicionados.EditIndex = -1;

            Session["listaSolicitudProducto"] = datoRepuesto;
            gv_adicionados.DataSource = datoRepuesto;
            gv_adicionados.DataBind();            
        }

        protected void gv_adicionados_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_adicionados.EditIndex = e.NewEditIndex;
            int index = gv_adicionados.EditIndex;

            DataTable datoRepuesto = Session["listaSolicitudProducto"] as DataTable;
            gv_adicionados.DataSource = datoRepuesto;
            gv_adicionados.DataBind();

            GridViewRow row = gv_adicionados.Rows[index];
            TextBox auxtexto = (TextBox)row.Cells[2].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto.Enabled = false;
            auxtexto = (TextBox)row.Cells[3].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto.Enabled = false;
            auxtexto = (TextBox)row.Cells[4].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto.Enabled = false;
            auxtexto = (TextBox)row.Cells[5].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto.Enabled = false;
            auxtexto = (TextBox)row.Cells[6].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto.Enabled = false;
            
            TextBox MontoPago = (TextBox)row.Cells[7].Controls[0];
            MontoPago.BackColor = Color.Yellow;
            
            
            auxtexto = (TextBox)row.Cells[8].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto.Enabled = false;
        }

        protected void gv_adicionados_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = gv_adicionados.EditIndex;
            GridViewRow row = gv_adicionados.Rows[index];
            TextBox CantidadModificar = (TextBox)row.Cells[7].Controls[0];

            DataTable datoRepuesto = Session["listaSolicitudProducto"] as DataTable;
            datoRepuesto.Rows[index][5] = CantidadModificar.Text.Replace(".", ",");
            double precio = Convert.ToDouble(datoRepuesto.Rows[index][4].ToString());
            double cantidad = Convert.ToDouble(datoRepuesto.Rows[index][5].ToString());
            double preciototal = (precio * cantidad);
            datoRepuesto.Rows[index][6] = preciototal;

            gv_adicionados.EditIndex = -1;

            Session["listaSolicitudProducto"] = datoRepuesto;
            gv_adicionados.DataSource = datoRepuesto;
            gv_adicionados.DataBind();
            
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_producto.Text = "";
            tx_cantidadProducto.Text = "";
            dd_tipoSolicitud.SelectedIndex = 0;
            tx_nrodocumento.Text = "";
            tx_solicitante.Text = "";
            tx_fechaEntrega.Text = "";
            tx_horaEntrega.Text = "";

            DataTable datoRepuesto = new DataTable();
            datoRepuesto.Columns.Add("Codigo", typeof(string));
            datoRepuesto.Columns.Add("Producto", typeof(string));
            datoRepuesto.Columns.Add("Medida", typeof(string));
            datoRepuesto.Columns.Add("Tipo", typeof(string));
            datoRepuesto.Columns.Add("Precio", typeof(string));
            datoRepuesto.Columns.Add("Cantidad", typeof(string));
            datoRepuesto.Columns.Add("PrecioTotal", typeof(string));
            datoRepuesto.Columns.Add("ItemPackFerial", typeof(Boolean));

            gv_adicionados.DataSource = datoRepuesto;
            gv_adicionados.DataBind();
            Session["listaSolicitudProducto"] = datoRepuesto;

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NCorpal_SolicitudEntregaProducto ncc = new NCorpal_SolicitudEntregaProducto();
            tx_nrodocumento.Text = ncc.get_siguentenumeroRecibo(codUser);
            tx_solicitante.Text = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
        }

        protected void bt_prueba_Click(object sender, EventArgs e)
        {
            NA_endpoints napi = new NA_endpoints();
            dynamic tuplas = napi.get_productoAlmacen("Nax","adm","123");
            string dato = "";
            JArray rowsArray = (JArray)tuplas["Resultado"];
            foreach (JObject obj in rowsArray)
            { 
                dato = dato + obj["Descripcion"].ToString()+";";
            }

                tx_cantidadProducto.Text = rowsArray.Count.ToString();
        }


        ///////////////  SOLICITUD PEDIDO UPON (ERROR!)
        //  buscar clientes upon
        protected void txt_nomCliente_TextChanged(object sender, EventArgs e)
        {
            string criterio = txt_nomCliente.Text.Trim();
            if(!string.IsNullOrEmpty(criterio))
            {
                cargarClientes(criterio);
            }
        }
        private async void cargarClientes(string criterio)
        {
            try
            {
                string token = await ObtenerTokenAsync("adm", "123");

                NA_APIclientes apiCliente = new NA_APIclientes();
                List<ClienteGetDTO> clientes = await apiCliente.GET_ClientesAsync(token, criterio);

                gvPedidoClientes.DataSource = clientes;
                gvPedidoClientes.DataBind();

                gvPedidoClientes.Visible = clientes.Count > 0;
            }
            catch (Exception ex)
            {
                showalert($"Error al realizar la busqueda: {ex.Message}");
            }
        }
        protected void gv_Clientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gvPedidoClientes.SelectedIndex;
            GridViewRow row = gvPedidoClientes.Rows[index];

            string nombreCompleto = row.Cells[1].Text;
            int codigoContacto = int.Parse(row.Cells[2].Text);

            txt_nomCliente.Text = nombreCompleto;
            Session["codigoContacto"] = codigoContacto;

            gvPedidoClientes.Visible = false;
        }


        // buscar productos
        private void showalert(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{mensaje}');", true);
        }
        protected void txt_nomProducto_TextChanged(object sender, EventArgs e)
        {
            string criterio = txt_nomProducto.Text.Trim();
            if(!string.IsNullOrEmpty(criterio))
            {
                cargarProductos(criterio);
            }
        }
        protected void gv_PedidoGetProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index =gv_PedidoGetProductos.SelectedIndex;
            GridViewRow row = gv_PedidoGetProductos.Rows[index];

            string codigoProducto = row.Cells[1].Text;
            string nombreCompleto = row.Cells[2].Text;
            string codigoUnidadMedida = row.Cells[3].Text;
            string costoUnitario = row.Cells[4].Text;

            txt_nomProducto.Text =nombreCompleto;
            txt_precProducto.Text=costoUnitario;

            Session["SessionCodigoProducto"] = codigoProducto;
            Session["SessionCodigoUnidadMedida"] = codigoUnidadMedida;

            gv_PedidoGetProductos.Visible = false;
        }
        private async void cargarProductos(string criterio)
        {
            try
            {
                string token = await ObtenerTokenAsync("adm", "123");

                NA_APIproductos APIProductos = new NA_APIproductos();
                List<productoCriterioGet> producto = await APIProductos.get_ProductoCriterioAsync(token, criterio);
                
                gv_PedidoGetProductos.DataSource = producto;
                gv_PedidoGetProductos.DataBind();
                gv_PedidoGetProductos.Visible = producto.Count > 0;
            }
            catch(Exception ex)
            {
                showalert($"Error al realizar la busqueda: {ex.Message}");
            }
        }
        private async Task<string> ObtenerTokenAsync(string usuario, string password)
        {
            NA_APIclientes APIClientes = new NA_APIclientes();
            return await APIClientes.ObtenerTokenAsync(usuario, password);
        }

        protected void tx_producto_TextChanged(object sender, EventArgs e)
        {

        }

        // agregar producto

        //List<Productos> productos = new List<Productos>();

        /*
        public class Productos 
        {
            public string Nombre {  get; set; }
            public string CodigoProducto {  get; set; }
            public string Cantidad {  get; set; }
            public 
        }
        */
    }
}