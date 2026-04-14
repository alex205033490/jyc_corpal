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
using System.Security.Cryptography;
using System.Web.UI.DataVisualization.Charting;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_SolicitudPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            cb_actualizarCliente.Visible = false;

            if (tienePermisoDeIngreso(118) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                DataTable dRepuesto = new DataTable();
                dRepuesto.Columns.Add("Codigo", typeof(string));
                dRepuesto.Columns.Add("Producto", typeof(string));
                dRepuesto.Columns.Add("Medida", typeof(string));
                dRepuesto.Columns.Add("Precio", typeof(string));
                dRepuesto.Columns.Add("Descuento", typeof(string));
                dRepuesto.Columns.Add("Cantidad", typeof(string));
                dRepuesto.Columns.Add("PrecioTotal", typeof(string));
                dRepuesto.Columns.Add("ItemPackFerial", typeof(Boolean));
                dRepuesto.Columns.Add("idcategoriap", typeof(string));
                dRepuesto.Columns.Add("codupon", typeof(string));
                dRepuesto.Columns.Add("cb_itemFraccionado", typeof(bool));

                gv_adicionados.DataSource = dRepuesto;
                gv_adicionados.DataBind();
                Session["listaSolicitudProducto"] = dRepuesto;

                /*CARGAR mod Pago*/

                cargarDatosModPago();
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

        private void cargarDatosModPago()
        {
            try
            {
                NCorpal_SolicitudEntregaProducto nSol = new NCorpal_SolicitudEntregaProducto();
                DataSet ds = nSol.get_obtenerModalidadPago();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dd_metodoPago.DataSource = ds.Tables[0];
                    dd_metodoPago.DataTextField = "nombre";
                    dd_metodoPago.DataValueField = "codigo";
                    dd_metodoPago.DataBind();

                    dd_metodoPago.Items.Insert(0, new ListItem("Seleccione un valor", "0"));
                }
                else
                {
                    dd_metodoPago.Items.Clear();
                    showalert("Error al cargar los datos Metodo de Pago");
                }
            }
            catch (Exception ex)
            {
                showalert("Error inesperado en el metodo cargarDatosModPago. " + ex.Message);
            }
        }

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaClientes222(string prefixText, int count)
        {
            string nombrecliente = prefixText;

            NCorpal_Cliente cc = new NCorpal_Cliente();
            DataSet tuplas = cc.get_ClienteNombre(nombrecliente);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;
        }


        [WebMethod]
        [ScriptMethod]
        public static string[] GetlistaProductos(string prefixText, int count, string contextKey)
        {
            if (string.IsNullOrEmpty(contextKey))
                return new string[0];

            string nombreProducto = prefixText;
            string cliente = contextKey;

            int codigCliente;
            NCorpal_Cliente nc = new NCorpal_Cliente();
            codigCliente = nc.get_CodigoCliente(cliente);

            if (codigCliente == 0)
                return new string[0];

            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarListProductosCliente(codigCliente, nombreProducto);

            if (tuplas == null || tuplas.Tables.Count == 0 || tuplas.Tables[0].Rows.Count == 0)
                return new string[0];

            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;

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
            buscarProductos();
        }

        private void buscarProductos()
        {
            try
            {
                string producto = tx_producto.Text.Trim();
                string cliente = tx_cliente.Text;
                int codigCliente;
                NCorpal_Cliente nc = new NCorpal_Cliente();
                codigCliente = nc.get_CodigoCliente(cliente);

                if (codigCliente <= 0)
                {
                    showalert("Error ingrese un cliente válido.");
                    return;
                }

                NCorpal_SolicitudEntregaProducto npp = new NCorpal_SolicitudEntregaProducto();
                DataSet datos = npp.get_mostrarProductosClienteLista(producto, codigCliente);
                gv_Productos.DataSource = datos;
                gv_Productos.DataBind();
            }
            catch (Exception ex)
            {
                showalert("Error al encontrar datos. " + ex.Message);
            }
        }

        /* cargar datos producto autocomplete */
        public class clssProducto
        {
            public string codigo { get; set; }
            public string producto { get; set; }
            public string medida { get; set; }
            public string precio { get; set; }
            public string StockParcialAlmacen { get; set; }
            public string stockAlmacen { get; set; }
            public string codcategoriap { get; set; }
            public string codupon { get; set; }

        }


        /*******************************************************************************/
        /*      BOTTON AGREGAR PRODUCTO*/
        protected void bt_adicionar_Click(object sender, EventArgs e)
        {
            decimal cantidadIngresada;
            if (!decimal.TryParse(tx_cantidadProducto.Text.Trim(), out cantidadIngresada))
            {
                showalert("Ingrese una cantidad válida");
                return;
            }

            if (validadCantidadStockParcial())
            {
                adicionar_productos();
                limpiarCamposADDProducto();
            }
        }

        private void adicionar_productos()
        {
            decimal cantidad;
            decimal.TryParse(tx_cantidadProducto.Text, out cantidad);
            bool itemPackFerial = cb_itemPackFerial.Checked;

            string cliente = tx_cliente.Text;
            int codigCliente;
            NCorpal_Cliente nc = new NCorpal_Cliente();
            codigCliente = nc.get_CodigoCliente(cliente);

            if (codigCliente <= 0)
            {
                showalert("Error ingrese un cliente válido.");
                return;
            }

            if (cantidad > 0) {
                DataTable datoRepuesto = Session["listaSolicitudProducto"] as DataTable;
                CheckBox cb = null;

                bool usarPrecioFraccionado = cb_precioFraccionado.Checked;

                for (int i = 0; i < gv_Productos.Rows.Count; i++)
                {
                    cb = (CheckBox)gv_Productos.Rows[i].Cells[1].FindControl("CheckBox1");
                    if (cb != null && cb.Checked)
                    {
                        NCorpal_SolicitudEntregaProducto Nsol = new NCorpal_SolicitudEntregaProducto();

                        string codigo = HttpUtility.HtmlDecode(gv_Productos.Rows[i].Cells[1].Text);
                        int codProd = Convert.ToInt32(gv_Productos.DataKeys[i].Value);
                        string producto = gv_Productos.Rows[i].Cells[2].Text;
                        string Medida = HttpUtility.HtmlDecode(gv_Productos.Rows[i].Cells[3].Text);

                        if (usarPrecioFraccionado)
                        {
                            DataSet ds = Nsol.obtenerMedida_productoFraccionado(codigCliente, codProd);

                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                Medida = ds.Tables[0].Rows[0]["medidacontenedorfraccionada"].ToString();
                            }
                        }

                        decimal precio, precioFracc;
                        decimal.TryParse(gv_Productos.Rows[i].Cells[4].Text, out precio);
                        decimal.TryParse(gv_Productos.Rows[i].Cells[5].Text, out precioFracc);

                        // Seleccionar Precio
                        decimal precioFinal = usarPrecioFraccionado ? precioFracc : precio;
                        //string tipo = dd_tipoSolicitud.SelectedItem.Text;
                        float StockProducto;
                        float.TryParse(gv_Productos.Rows[i].Cells[6].Text, out StockProducto);
                        float StockPackFerial;
                        float.TryParse(gv_Productos.Rows[i].Cells[7].Text, out StockPackFerial);
                        int idcategiap;
                        int.TryParse(gv_Productos.Rows[i].Cells[8].Text, out idcategiap);
                        string codupon = gv_Productos.Rows[i].Cells[9].Text;

                        decimal subtotal = precioFinal * cantidad;
                        decimal porDescuento = 0;

                        subtotal = Math.Round(subtotal, 2, MidpointRounding.AwayFromZero);

                        DataRow tupla = datoRepuesto.NewRow();
                        tupla["Codigo"] = codigo;
                        tupla["codupon"] = codupon;
                        tupla["producto"] = producto;
                        tupla["Medida"] = Medida;
                        //tupla["Tipo"] = tipo;
                        tupla["Precio"] = precioFinal;
                        tupla["Descuento"] = porDescuento;
                        tupla["Cantidad"] = cantidad;
                        tupla["PrecioTotal"] = subtotal;
                        tupla["idcategoriap"] = idcategiap;
                        tupla["cb_itemFraccionado"] = usarPrecioFraccionado;

                        if (itemPackFerial == true)
                        {
                            tupla["ItemPackFerial"] = itemPackFerial;
                            tupla["Medida"] = "UNIDAD";
                            //tupla["Tipo"] = "ITEM PACK FERIAL";
                        }
                        else
                            tupla["ItemPackFerial"] = false;

                        datoRepuesto.Rows.Add(tupla);

                        //recalcularTotal();
                        /*
                        decimal totalVisual = 0;
                        decimal.TryParse(tx_total.Text, out totalVisual);
                        totalVisual += subtotal; engranaje
                        tx_total.Text = totalVisual.ToString("0.00");
                        */
                    }
                }
                int id_tipoCliente = verificarTipoCliente(codigCliente);
                if (id_tipoCliente == 1 || id_tipoCliente == 3)
                {
                    // falta recalcular los q no son fraccionados
                    recalcularDescuentos(datoRepuesto);
                }
                recalcularTotal();

                gv_adicionados.DataSource = datoRepuesto;
                gv_adicionados.DataBind();
            }
            else
                showalert("Error: Cantidad igual 0");
        }


        private bool validadCantidadStockParcial()
        {
            bool esValido = true;

            decimal cantidadIngresada = decimal.Parse(tx_cantidadProducto.Text.Trim());

            if (cantidadIngresada <= 0)
            {
                showalert("La cantidad debe ser mayor que cero.");
                return false;
            }

            foreach (GridViewRow row in gv_Productos.Rows)
            {
                CheckBox chk = row.FindControl("CheckBox1") as CheckBox;
                if (chk != null && chk.Checked)
                {
                    decimal stockParcial = 0;

                    if (gv_Productos.DataKeys[row.RowIndex] != null)
                    {
                        stockParcial = Convert.ToDecimal(gv_Productos.DataKeys[row.RowIndex].Value);
                    }
                    else
                    {
                        stockParcial = Convert.ToDecimal(row.Cells[6].Text);
                    }
                    /*
                    if(cantidadIngresada > stockParcial)
                    {
                        showalert($"No cuentas con stock disponible. Stock disponible: {stockParcial} ");
                        esValido = false;
                        break;
                    }
                    */
                }
            }
            return esValido;
        }



        /*  --------------   BTN GUARDAR SOLICITUD  ----------------  */
        protected void bt_guardar_Click(object sender, EventArgs e)
        {
            if (!validarGuardado())
                return;
            guardarSolicitud();
        }

        private bool validarGuardado()
        {
            int codTipoCliente = Convert.ToInt32(hf_tipoCliente.Value);
            
            if (codTipoCliente != 7)
            {
                foreach(GridViewRow row in gv_adicionados.Rows)
                {
                    CheckBox cb = (CheckBox)row.FindControl("cb_itemFraccionado");
                    if(cb != null && cb.Checked)
                    {
                        showalert("Este tipo de clientes no puede tener productos fraccionados.");
                        return false;
                    }
                }
            }

            string fechaentrega = tx_fechaEntrega.Text.Trim();
            string horaentrega = tx_horaEntrega.Text.Trim();

            if (string.IsNullOrWhiteSpace(fechaentrega))
            {
                showalert("Ingrese una fecha válida");
                return false;
            }
            if (string.IsNullOrWhiteSpace(horaentrega))
            {
                showalert("Ingrese una hora válida");
                return false;
            }

            int codMetPago = dd_metodoPago.SelectedIndex;
            if (codMetPago == 0)
            {
                showalert("Por favor, seleccione un Metodo de Pago válido");
                return false;
            }
            return true;
        }

        private void guardarSolicitud()
        {
            try
            {
                int codMetPago = dd_metodoPago.SelectedIndex;

                DataTable datoRepuesto = Session["listaSolicitudProducto"] as DataTable;
                if (datoRepuesto.Rows.Count > 0)
                {
                    NA_Responsables Nresp = new NA_Responsables();
                    string usuarioAux = Session["NameUser"].ToString();
                    string passwordAux = Session["passworuser"].ToString();
                    int codpersolicitante = Nresp.getCodUsuario(usuarioAux, passwordAux);

                    string nroboleta = tx_nrodocumento.Text;
                    string personalsolicitud = tx_solicitante.Text;
                    string fechaentrega = DateTime.Parse(tx_fechaEntrega.Text).ToString("yyyy-MM-dd");
                    string horaentrega = tx_horaEntrega.Text;

                    NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();

                    string repuestosSolicitados = "";
                    string cliente = tx_cliente.Text;
                    int codigCliente;
                    NCorpal_Cliente nc = new NCorpal_Cliente();
                    codigCliente = nc.get_CodigoCliente(cliente);

                    bool banderaActualizar = cb_actualizarCliente.Checked;
                    if (codigCliente != 0 && banderaActualizar == true)
                    {
                        string propietario = tx_propietario.Text;
                        string razonSocial = tx_razonSocial.Text;
                        string nit = tx_nit.Text;
                        banderaActualizar = nc.updateDatosTiendaSolicitud(codigCliente, cliente, propietario, razonSocial, nit, codpersolicitante);
                    }

                    if (codigCliente > 0)
                    {
                        if (nss.set_guardarSolicitud(nroboleta, fechaentrega, horaentrega, personalsolicitud, codpersolicitante, true, codigCliente, codMetPago))

                        {
                            int ultimoinsertado = nss.getultimaSolicitudproductoInsertado(codpersolicitante);
                            decimal montoTotal = 0;
                            for (int i = 0; i < datoRepuesto.Rows.Count; i++)
                            {
                                int codProducto = Convert.ToInt32(datoRepuesto.Rows[i]["codigo"].ToString());

                                decimal VALORcantidad = Convert.ToDecimal(datoRepuesto.Rows[i]["cantidad"].ToString());
                                decimal VALORprecio = Convert.ToDecimal(datoRepuesto.Rows[i]["Precio"].ToString());
                                decimal VALORtotal = Convert.ToDecimal(datoRepuesto.Rows[i]["PrecioTotal"].ToString());

                                string VALORmedida = datoRepuesto.Rows[i]["Medida"].ToString();
                                string Tipo = "";

                                bool esFraccionado = Convert.ToBoolean(datoRepuesto.Rows[i]["cb_itemFraccionado"]);

                                decimal? cantidad = null;
                                decimal? precio = null;
                                decimal? total = null;
                                string medida = null;

                                decimal? cantFracc = null;
                                decimal? precioFracc = null;
                                string medidaFracc = null;

                                if (esFraccionado)
                                {
                                    cantFracc = VALORcantidad;
                                    precioFracc = VALORprecio;
                                    medidaFracc = VALORmedida;

                                    montoTotal = VALORcantidad * VALORprecio;
                                } else
                                {

                                    cantidad = VALORcantidad;
                                    precio = VALORprecio;
                                    medida = VALORmedida;
                                    total = VALORtotal;

                                    montoTotal = VALORtotal;
                                }

                                //preciototalsumando continuamente

                                string producto = datoRepuesto.Rows[i]["producto"].ToString();
                                //decimal total = preciocompra * cantidad;

                                repuestosSolicitados = repuestosSolicitados + producto + " cant.=" + cantidad.ToString() + ", Medida=" + medida + ", Tipo=" + Tipo + "<br>";

                                nss.insertarDetalleSolicitudProducto(ultimoinsertado, codProducto, cantidad, precio, montoTotal, Tipo,
                                                                    medida, cantFracc, precioFracc, medidaFracc);

                                //montoTotal += totalProd;
                            }

                            nss.actualizarmontoTotal(ultimoinsertado);

                            //limpiarDatos();
                            buscarProductos();
                            Session["codigoSolicitudProducto"] = ultimoinsertado;

                            Response.Redirect("../Presentacion/FCorpal_ReporteSolicitudProducto.aspx");
                            Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
                        }
                        else
                            showalert($"Error: No se pudo realizar la Solicitud. ");
                    }
                    else
                        showalert("Error: El Cliente no existe");
                }
                else
                    showalert("Error: No tiene Solicitud Pedido");
            }
            catch (Exception ex)
            {
                showalert("Ocurrio un error inesperado al guardar la solicitud. " + ex.Message);
            }
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

            string cliente = tx_cliente.Text;
            int codigCliente;
            NCorpal_Cliente nc = new NCorpal_Cliente();
            codigCliente = nc.get_CodigoCliente(cliente);
            int id_tipoCliente = verificarTipoCliente(codigCliente);
            if (id_tipoCliente == 1 || id_tipoCliente == 3)
            {
                recalcularDescuentos(datoRepuesto);
            }

            Session["listaSolicitudProducto"] = datoRepuesto;

            gv_adicionados.EditIndex = -1;
            gv_adicionados.DataSource = datoRepuesto;
            gv_adicionados.DataBind();

            recalcularTotal();
        }

        private void recalcularTotal()
        {
            DataTable dt = Session["listaSolicitudProducto"] as DataTable;

            decimal total = 0;

            foreach (DataRow row in dt.Rows)
            {
                decimal valor2 = Convert.ToDecimal(row["PrecioTotal"]);

                total += valor2; 
            }

            tx_total.Text = total.ToString("0.00");
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
            hf_tipoCliente.Value = "";

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


        protected void bt_verificar_Click(object sender, EventArgs e)
        {
            verificarTienda();
        }

        private void verificarTienda()
        {
            string tienda = tx_cliente.Text;
            NCorpal_Cliente ncli = new NCorpal_Cliente();
            DataSet tuplaCliente = ncli.get_ClienteNombreEspecifico(tienda);
            if (tuplaCliente.Tables[0].Rows.Count>0) {
                tx_propietario.Text = tuplaCliente.Tables[0].Rows[0][6].ToString(); 
                tx_razonSocial.Text = tuplaCliente.Tables[0].Rows[0][12].ToString();
                tx_nit.Text = tuplaCliente.Tables[0].Rows[0][13].ToString();
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Tienda no existe') </script>");

        }
        private void limpiarCamposADDProducto()
        {
            tx_producto.Text = string.Empty;
            tx_cantidadProducto.Text = string.Empty;
            gv_Productos.DataSource = null;
            gv_Productos.DataBind();

            cb_precioFraccionado.Checked = false;
        }

        private int verificarTipoCliente(int codCli)
        {
            NCorpal_SolicitudEntregaProducto nsol = new NCorpal_SolicitudEntregaProducto();
            int tipoCliente = nsol.identificarTipoCliente(codCli);

            return tipoCliente;
        }

        private void recalcularDescuentos(DataTable datoRepuesto)
        {
            if (datoRepuesto == null || datoRepuesto.Rows.Count == 0)
                return;

            Dictionary<int, decimal> totalesXcategoria = new Dictionary<int, decimal>();

            foreach(DataRow fila in datoRepuesto.Rows)
            {
                bool esFraccionado = fila["cb_itemFraccionado"] != DBNull.Value
                                        && Convert.ToBoolean(fila["cb_itemFraccionado"]);
                if (esFraccionado)
                    continue;

                int categoria = Convert.ToInt32(fila["idcategoriap"]);
                decimal cantidadFila = Convert.ToDecimal(fila["Cantidad"]);

                if (totalesXcategoria.ContainsKey(categoria))
                    totalesXcategoria[categoria] += cantidadFila;
                else
                    totalesXcategoria.Add(categoria, cantidadFila);
            }

            NCorpal_SolicitudEntregaProducto nSol = new NCorpal_SolicitudEntregaProducto();
            Dictionary<int, decimal> descuentoCategoria = new Dictionary<int, decimal>();

            foreach(var item in totalesXcategoria)
            {
                decimal descuento = nSol.obtenerDescuentoCategoriaSolProd(item.Key, item.Value);
                descuentoCategoria.Add(item.Key, descuento);
            }

            foreach(DataRow fila in datoRepuesto.Rows)
            {
                bool esFraccionado = fila["cb_itemFraccionado"] != DBNull.Value
                                    && Convert.ToBoolean(fila["cb_itemFraccionado"]);
                if (esFraccionado)
                {
                    fila["Descuento"] = 0;
                    decimal precio1 = Convert.ToDecimal(fila["Precio"]);
                    decimal cantidadFila1 = Convert.ToDecimal(fila["Cantidad"]);

                    decimal subtotal1 = precio1 * cantidadFila1;
                    subtotal1 = Math.Round(subtotal1, 2, MidpointRounding.AwayFromZero);

                    fila["PrecioTotal"] = subtotal1;
                    continue;
                }

                
                int categoria = Convert.ToInt32(fila["idcategoriap"]);
                decimal precio = Convert.ToDecimal(fila["Precio"]);
                decimal cantidadFila = Convert.ToDecimal(fila["Cantidad"]);

                decimal porDescuento = descuentoCategoria.ContainsKey(categoria)
                                        ? descuentoCategoria[categoria]
                                        : 0;
                fila["Descuento"] = porDescuento;
                decimal subtotal = precio * cantidadFila;

                if(porDescuento > 0)
                {
                    decimal montoDescuento = (subtotal * porDescuento) / 100;
                    subtotal -= montoDescuento;
                }
                subtotal = Math.Round(subtotal, 2, MidpointRounding.AwayFromZero);
                fila["PrecioTotal"] = subtotal;
            }
        }

        public class clsscliente
        {
            public string direccion { get; set; }
            public string telefono { get; set; }
            public string propietario { get; set; }
            public string nit { get; set; }
            public string razonsocial { get; set; }
            public int tipoCliente { get; set; }
        }

        [System.Web.Services.WebMethod]
        public static clsscliente obtenerCliente(string nombreCliente)
        {
            NCorpal_Cliente ncli = new NCorpal_Cliente();
            DataSet ds = ncli.get_ClienteNombreEspecifico(nombreCliente);

            clsscliente c = new clsscliente();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                //c.direccion = row["tiendadir"].ToString();
                //c.telefono = row["tiendatelefono"].ToString();
                c.propietario = row["propietarioname"].ToString();
                c.nit = row["propietarionit"].ToString();
                c.razonsocial = row["facturar_a"].ToString();
                c.tipoCliente = Convert.ToInt32(row["id_tipocliente"].ToString());
            }
            return c;
        }


        private void showalert(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        protected void dd_metodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dd_metodoPago.SelectedIndex == 2)
            {
                tx_diasCredito.Visible = true;
            }
            else
            {
                tx_diasCredito.Visible = false;
            }
        }
    }
}