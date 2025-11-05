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
            catch(Exception ex)
            {
                showalert("Error inesperado en el metodo cargarDatosModPago. " + ex.Message );
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
            decimal cantidadIngresada;
            if (!decimal.TryParse(tx_cantidadProducto.Text.Trim(), out cantidadIngresada))
            {
                showalert("Ingrese una cantidad válida");
                return;
            }

            if (validadCantidadStockParcial())
            {
                adicionar_productos();
            }
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
        private bool validadCantidadStockParcial()
        {
            bool esValido = true;

            decimal cantidadIngresada = decimal.Parse(tx_cantidadProducto.Text.Trim());

            if(cantidadIngresada <= 0)
            {
                showalert("La cantidad debe ser mayor que cero.");
                return false;
            }

            foreach(GridViewRow row in gv_Productos.Rows)
            {
                CheckBox chk = row.FindControl("CheckBox1") as CheckBox;
                if(chk != null && chk.Checked)
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

                    if(cantidadIngresada > stockParcial)
                    {
                        showalert($"No cuentas con stock disponible. Stock disponible: {stockParcial} ");
                        esValido = false;
                        break;
                    }
                }
            }
            return esValido;
        }



        /*  --------------   BTN GUARDAR SOLICITUD  ----------------  */
        protected void bt_guardar_Click(object sender, EventArgs e)
        {
            guardarSolicitud();
        }

        private void guardarSolicitud()
        {
            int codMetPago = dd_metodoPago.SelectedIndex;
            if (codMetPago == 0)
            {
                showalert("Por favor, seleccione un Metodo de Pago válido");
                return;
            }

            DataTable datoRepuesto = Session["listaSolicitudProducto"] as DataTable;
            if(datoRepuesto.Rows.Count > 0){
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

              /*  if (codigCliente == 0) { 
                  string propietario = tx_propietario.Text;
                  string razonSocial = tx_razonSocial.Text;
                  string nit = tx_nit.Text;
                    bool okCliente = nc.set_clienteSolicitud(cliente, propietario, razonSocial, nit, codpersolicitante);
                    codigCliente = nc.get_clienteUltimoIngresado(cliente, propietario, razonSocial, nit);
                }
                */

                bool banderaActualizar = cb_actualizarCliente.Checked;
                if (codigCliente!=0 && banderaActualizar == true) {
                    string propietario = tx_propietario.Text;
                    string razonSocial = tx_razonSocial.Text;
                    string nit = tx_nit.Text;                    
                    banderaActualizar = nc.updateDatosTiendaSolicitud(codigCliente,cliente,propietario, razonSocial, nit, codpersolicitante);
                }

                if (codigCliente > 0) {
                    if (nss.set_guardarSolicitud(nroboleta, fechaentrega, horaentrega, personalsolicitud, codpersolicitante, true, codigCliente, codMetPago))

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
                        Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
                    }
                    else
                        showalert($"Error: No se pudo realizar la Solicitud. " +
                            $"{nroboleta}, {fechaentrega}, {horaentrega}, {personalsolicitud}, {codpersolicitante}, {codigCliente}, {codMetPago}");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: El Cliente no existe') </script>");

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


        private void showalert(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

    }
}