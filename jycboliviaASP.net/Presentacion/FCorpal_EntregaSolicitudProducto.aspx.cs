using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;   // ✅ ESTE ES EL ÚNICO CheckBox QUE NECESITAS
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;



namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_EntregaSolicitudProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(146) == false)
            {                   
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {
                Session["despachoListGV"] = null;
                GET_MostrarSolicitudProductos("Abierto");
                cargarRegistroVehiculosDD();
            }

        }
        // VW PRINCIPAL LISTA DE SOLICITUDES DE PEDIDOS 
        private void GET_MostrarSolicitudProductos(string estadoSolicitud)
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            DataSet datos = negocio.get_VWRegistrosEntregaSolicitudProductos(estadoSolicitud);
            gv_solicitudesProductos.DataSource = datos;
            gv_solicitudesProductos.DataBind();
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

        public string aFecha2(string fecha)
        {
            if (string.IsNullOrEmpty(fecha) || fecha == "&nbsp;")
            {
                return null;
            }
            else
            {
                try
                {
                    DateTime fecha_ = DateTime.ParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    return fecha_.ToString("yyyy-MM-dd");
                }
                catch(Exception ex)
                {
                    throw new Exception($"error al convertir la fecha : " + ex.Message);
                }
            }
        }
        public string aFecha(string fecha)
        {
            if (fecha == "" || fecha == "&nbsp;")
            {
                return fecha = "null";
            }
            else
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = "'" + anio + "/" + mes + "/" + dia + "'";
                return _fecha;
            }
        }

       
        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }

        private void limpiarForm()
        {
            GET_MostrarSolicitudProductos("Abierto");

            gv_despachoProductos.DataSource = null;
            gv_despachoProductos.DataBind();

            Session["despachoListGV"] = null;
            Session["ItemsTotalListGV"] = null;

            dd_listVehiculo.SelectedIndex = 0;
           
            gv_detCar.DataSource = null;
            gv_detCar.DataBind();

            txt_detalleRegistro.Text = string.Empty;

            tx_chofer.Text = string.Empty;
            hf_codChofer.Value = string.Empty;
        }

        protected void bt_verRecibo_Click(object sender, EventArgs e)
        {
            if (dd_listVehiculo.SelectedIndex >= 0) {
                int codigo = int.Parse(dd_listVehiculo.SelectedValue);
                Session["codigoCamion"] = codigo;
                Session["ReporteGeneral"] = "Reporte_ProductoCamionEntrega";
                Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");
            }
        }

        private void verReciboSeleccionado()
        {
            if(gv_solicitudesProductos.SelectedIndex >= 0){
                int codigoSolicitud;
                int.TryParse(gv_solicitudesProductos.SelectedRow.Cells[2].Text, out codigoSolicitud);
                Session["codigoEntregaSolicitudProducto"] = codigoSolicitud;
                Response.Redirect("../Presentacion/FCorpal_ReporteEntregaSolicitudProducto.aspx");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccionar Entrega') </script>");
        }


        protected void gv_solicitudesProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.CheckBox chkSelect =
                    (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkSelect");

                if (chkSelect != null)
                {
                    List<Product> despachoList = (List<Product>)Session["despachoListGV"];

                    if (despachoList != null)
                    {
                        string nroBoleta = e.Row.Cells[1].Text;
                        string producto = e.Row.Cells[2].Text;

                        var selectedProduct = despachoList
                            .FirstOrDefault(p => p.nroboleta == nroBoleta && p.producto == producto);

                        if (selectedProduct != null)
                        {
                            chkSelect.Checked = true;
                            e.Row.CssClass += " highlighted";
                        }
                        else
                        {
                            chkSelect.Checked = false;
                            e.Row.CssClass = e.Row.CssClass.Replace("highlighted", "").Trim();
                        }
                    }
                }
            }
        }



        public class Product
        {
            public int codigoSolicitud {  get; set; }
            public string nroboleta { get; set; }
            public string producto { get; set; }
            public int cantidadEntregada {  get; set; }
        }
        protected void chk_seleccionar_CheckedChanged(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.CheckBox chkBox = sender as System.Web.UI.WebControls.CheckBox;

            if (chkBox == null) return;

            GridViewRow row = chkBox.NamingContainer as GridViewRow;
            if (row == null) return;

            int codigoSolicitud = int.Parse(row.Cells[1].Text);
            string nroBoleta = row.Cells[2].Text;
            string producto = row.Cells[4].Text;

            System.Web.UI.WebControls.TextBox txtCantidadEntregada =
                row.FindControl("tx_cantidadEntregarOK") as System.Web.UI.WebControls.TextBox;

            int cantidadEntregada = 0;
            if (txtCantidadEntregada != null)
                int.TryParse(txtCantidadEntregada.Text, out cantidadEntregada);

            Product selectedProduct = new Product
            {
                codigoSolicitud = codigoSolicitud,
                nroboleta = nroBoleta,
                producto = producto,
                cantidadEntregada = cantidadEntregada
            };


            List<Product> despachoList = (List<Product>)Session["despachoListGV"];
            if (despachoList == null)

            // P2 
            List<Product> sumTotalItems = (List<Product>) Session["ItemsTotalListGV"];
            if(sumTotalItems == null)
            {
                sumTotalItems = new List<Product>();
            }

            var existente = sumTotalItems.FirstOrDefault(p => p.producto == producto);

            if (chkBox.Checked)
            {
                if(existente != null)
                {
                    existente.cantidadEntregada += cantidadEntregada;
                }
                else
                {
                    sumTotalItems.Add(selectedProduct);
                }
            }
            else
            {
                if(existente != null)
                {
                    existente.cantidadEntregada -= cantidadEntregada;

                    if(existente.cantidadEntregada <= 0)
                    {
                        sumTotalItems.Remove(existente);
                    }
                }
                /*sumTotalItems.RemoveAll(p =>
                p.codigoSolicitud == codigoSolicitud &&
                p.producto == producto &&
                p.cantidadEntregada == cantidadEntregada);*/
            }

            //P1
            List<Product> despachoList = (List <Product>) Session["despachoListGV"];
            if(despachoList == null)

            {
                despachoList = new List<Product>();
            }

            if (chkBox.Checked)
            {
                despachoList.Add(selectedProduct);
            }
            else
            {
                despachoList.RemoveAll(p =>
                    p.codigoSolicitud == codigoSolicitud &&
                    p.nroboleta == nroBoleta &&
                    p.producto == producto);
            }

            Session["despachoListGV"] = despachoList;
            Session["ItemsTotalListGV"] = sumTotalItems;

            gv_despachoProductos.DataSource = despachoList;
            gv_despachoProductos.DataBind();

            gv_sumTotalItems.DataSource = sumTotalItems;
            gv_sumTotalItems.DataBind();
        }


        /***********************************   VW solicitud entrega producto     *************************************/
        protected void btn_registrarDespacho_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarForm())
                    return;

                int codVehiculo = int.Parse(dd_listVehiculo.SelectedValue);
                string detalle = txt_detalleRegistro.Text.Trim();
                int codResponsable = obtenerCodResponsable();

                int codDespacho = RegistrarDespachoPrincipal(detalle, codVehiculo, codResponsable);

                bool result_AsignacionChoferCamion = Registro_AsignacionChoferCamion();

                if (!result_AsignacionChoferCamion)
                {
                    showalert("Error no se pudo asignar el chofer al Camión");
                    return;
                }

                if (codDespacho <= 0)
                {
                    showalert("Error al registrar el despacho principal.");
                    return;
                }

                if (!RegistrarDetalleDespacho(codDespacho))
                {
                    showalert("Error al registrar el detalle del despacho.");
                    return;
                }

                ProcesarSolicitudesSeleccionadas(codVehiculo);
                FinalizarRegistro(codVehiculo);
                /*
                int codVehiculo = int.Parse(dd_listVehiculo.SelectedValue);
                string detalle = txt_detalleRegistro.Text.Trim();
                int codResponsable = obtenerCodResponsable();

                ValidarForm();

                int codDespacho = CrearDespachoPrincipal123(detalle, codVehiculo, codResponsable);
                if (codDespacho <= 0)
                {
                    showalert("Error al registrar el despacho principal.");
                    return;
                }

                if (!RegistrarDetalleDespacho(codDespacho))
                {
                    showalert("Error al registrar el detalle del despacho.");
                    return;
                }

                foreach (GridViewRow row in gv_solicitudesProductos.Rows)
                {
                    CheckBox chk = row.FindControl("chkSelect") as CheckBox;
                    if (chk != null && chk.Checked)
                    {
                        ProcesarRegistroSolicitudPedido123(row, codVehiculo);

                        int codigoSolicitud = int.Parse(row.Cells[1].Text);
                        int codProducto = int.Parse(row.Cells[3].Text);
                        string producto = row.Cells[4].Text;
                        int codigoCliente = int.Parse(row.Cells[11].Text);
                        string solicitante = row.Cells[10].Text;
                        string fechaEntrega = aFecha2(row.Cells[9].Text);

                        NCorpal_EntregaSolicitudProducto2 nego = new NCorpal_EntregaSolicitudProducto2();
                        int codVendedor = nego.ObtenerCodVendedor_EntregaSolProductos(codigoSolicitud);

                        TextBox txt_cantidad = (TextBox)row.FindControl("tx_cantidadEntregarOK");
                        int cantidad = 0;
                        if (!string.IsNullOrEmpty(txt_cantidad.Text))
                        {
                            int.TryParse(txt_cantidad.Text, out cantidad);
                        }

                        RegistrarIngresoAlmacenDinamico(codigoSolicitud, codProducto, producto, cantidad, codVendedor);

                        RegistrarVentaConDetalle123(codigoSolicitud, codigoCliente, solicitante, fechaEntrega);
                    }
                }

                limpiarForm();
                GET_MostrarSolicitudProductos("Abierto");
                //  showalert("Registro insertado exitosamente.");
                Session["codigoDespacho"] = codDespacho;
                Session["ReporteGeneral"] = "Report_DespachoBoletasProdEntrega";                
                Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");
                */
            }
            catch(Exception ex)
            {
                showalert($"Error inesperado en el boton: {ex.Message}");
            }
        }

        /* Validar Formulario */
        private bool ValidarForm()
        {
            bool RegistroSolicitudSeleccionado = false;

            foreach (GridViewRow row in gv_solicitudesProductos.Rows)
            {
                System.Web.UI.WebControls.CheckBox chk =
                    row.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;

                if (chk != null && chk.Checked)
                {
                    RegistroSolicitudSeleccionado = true;
                    break;
                }
            }

            if (!RegistroSolicitudSeleccionado)
            {
                showalert("Debe seleccionar al menos 1 registro");
                return false;
            }

            if (string.IsNullOrEmpty(tx_chofer.Text.Trim()) && string.IsNullOrEmpty(hf_codChofer.Value))
            {
                showalert("Debe seleccionar un Chofer Válido");
                return false;
            }

            if (string.IsNullOrEmpty(hf_codChofer.Value))
            {
                showalert("Debe seleccionar un chofer válido.");
                return false;
            }

            if (dd_listVehiculo.SelectedIndex == 0)
            {
                showalert("Por favor seleccione un vehículo válido.");
                return false;
            }

            return true;
        }


        private void ProcesarSolicitudesSeleccionadas(int codVehiculo)
        {
            foreach (GridViewRow row in gv_solicitudesProductos.Rows)
            {
                System.Web.UI.WebControls.CheckBox chk =
                    row.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;

                if (chk == null || !chk.Checked)
                    continue;

                ProcesarRegistroSolicitudPedido123(row, codVehiculo);

                int codSolicitud = int.Parse(row.Cells[1].Text);
                int codProducto = int.Parse(row.Cells[3].Text);
                string producto = row.Cells[4].Text;
                int codCliente = int.Parse(row.Cells[12].Text);
                string solicitante = row.Cells[11].Text;
                string fechaEntrega = aFecha2(row.Cells[10].Text);

                NCorpal_EntregaSolicitudProducto2 nego = new NCorpal_EntregaSolicitudProducto2();
                int codVendedor = nego.ObtenerCodVendedor_EntregaSolProductos(codSolicitud);
                int codMetPagoSol = nego.Obtener_codMetodoPagoSolicitud(codSolicitud);

                System.Web.UI.WebControls.TextBox txt_cantidad =
                    row.FindControl("tx_cantidadEntregarOK") as System.Web.UI.WebControls.TextBox;

                decimal cantidad = 0;
                if (txt_cantidad != null)
                    decimal.TryParse(txt_cantidad.Text, out cantidad);

                bool RegistroIngresoAlmacen = RegistrarIngresoAlmacenDinamico(
                    codVendedor, codProducto, producto, cantidad);

                if (!RegistroIngresoAlmacen)
                {
                    showalert("No se pudo ingresar el stock al almacén.");
                    return;
                }

                RegistrarVentaConDetalle123(
                    codSolicitud, codCliente, solicitante, fechaEntrega, codMetPagoSol);
            }
        }


        private void FinalizarRegistro(int codDespacho)
        {
            limpiarForm();
            GET_MostrarSolicitudProductos("Abierto");

            Session["codigoDespacho"] = codDespacho;
            Session["ReporteGeneral"] = "Report_DespachoBoletasProdEntrega";

            Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");
        }

        /*PARTE 2 AUTOCORR*/
        private void RegistrarVentaConDetalle123(int codigoSolicitud, int codigoCliente,string solicitante, string fechaEntrega, int codMetPago)
        {
            try
            {
                var clienteData = new NCorpal_Cliente().get_ClienteCodigo(codigoCliente);

                if (clienteData.Tables[0].Rows.Count == 0)
                {
                    showalert("Cliente no encontrado.");
                    return;
                }

                var row = clienteData.Tables[0].Rows[0];
                string tiendaName = row["tiendaname"].ToString();
                string direccion = row["tiendadir"].ToString();
                string telefono = row["tiendatelefono"].ToString();
                string municipio = row["tiendadepartamento"].ToString();
                string correo = row["propietariocorreo"].ToString();
                string razonSocial = row["facturar_a"].ToString();
                string documento = row["facturar_nit"].ToString();
                decimal tipoCambio = decimal.Parse("6,96");

                int codResponsable = obtenerCodVendedorSolicitud(codigoSolicitud);

                NCorpal_Venta nventa = new NCorpal_Venta();

                bool ventaCreada = nventa.crearVentas3(
                    codigoCliente, tiendaName, codigoSolicitud, correo, municipio, telefono,
                    "", direccion, razonSocial, documento,
                    codMetPago, 0, 1, tipoCambio, 0, 0, "LeyendaNinguna",
                    codResponsable, solicitante, 0, fechaEntrega, codigoSolicitud);

                if (!ventaCreada)
                {
                    showalert("Error al crear la venta");
                    return;
                }
                else
                {
                    
                    bool productosInsertados = nventa.insertarTodoslosProductosAVenta3(codigoSolicitud);
                    if (!productosInsertados)
                    {
                        showalert($"Error al insertar productos de la venta {codigoSolicitud}");
                    }
                }
            }
            catch(Exception ex)
            {
                showalert($"Error al registrar la venta: {ex.Message}");
            }
        }

        private void ProcesarRegistroSolicitudPedido123(GridViewRow row, int codVehiculo)
        {
            try
            {
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                int codigoSolicitud = int.Parse(row.Cells[1].Text);
                int codigoProducto = int .Parse(row.Cells[3].Text);
                TextBox txtCantidad = (TextBox)row.FindControl("tx_cantidadEntregarOK");
                Label lblCantidadActual = (Label)row.FindControl("lb_cantentregada");
                CheckBox chkTipoEntrega = (CheckBox)row.FindControl("chkTipoEntrega");

                if(!float.TryParse(txtCantidad.Text, out float cantidadNueva)) cantidadNueva = 0;
                if(!float.TryParse(lblCantidadActual.Text, out float cantidadActual)) cantidadActual = 0;

                float totalEntregado = cantidadNueva + cantidadActual;
                float stockARestar = cantidadNueva;
                string estadoEntrega = chkTipoEntrega != null && chkTipoEntrega.Checked ? "parcial" : "total";

                int codResponsable = obtenerCodResponsable();
                string personal = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();



                NCorpal_EntregaSolicitudProducto2 nentrega_solicitud = new NCorpal_EntregaSolicitudProducto2();
                bool actualizado = nentrega_solicitud.UPDATE_camposDetalleSolicitudPedido(codigoSolicitud, codigoProducto, totalEntregado, 
                                                        estadoEntrega, stockARestar, codResponsable, codVehiculo);

                if (actualizado)
                {
                    nentrega_solicitud.update_CierreAutSolicitudProd(codigoSolicitud, codResponsable, personal);
                }
                else
                {
                    showalert($"Error al actualizar solicitud {codigoSolicitud} - producto {codigoProducto}");
                }
            }

            catch(Exception ex)
            {
                showalert($"Error en solicitud producto: {ex.Message}");
            }
        }
        
        private bool RegistrarDestalleDespacho123(int codigoDespacho)
        {
            bool resultadoGeneral = true;
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();

            foreach (GridViewRow row in gv_solicitudesProductos.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect != null && chkSelect.Checked)
                {
                    int codPedido = int.Parse(row.Cells[1].Text);
                    int codigoProducto = int.Parse(row.Cells[3].Text);
                    TextBox txtCantidad = (TextBox)row.FindControl("tx_cantidadEntregarOK");

                    if (!float.TryParse(txtCantidad.Text, out float cantidadEntregar)) cantidadEntregar = 0;

                    if (!negocio.POST_INSERTdetalleDespacho(codigoDespacho, codPedido, codigoProducto, cantidadEntregar))
                    {
                        resultadoGeneral = false;
                        break;
                    }
                }
            }

            return resultadoGeneral;
        }

        private int RegistrarDespachoPrincipal(string detalle, int codVehiculo, int codResponsable)
        {
            int codConductor = Convert.ToInt32(hf_codChofer.Value);
            string conductor = tx_chofer.Text.Trim();

            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            return negocio.POST_INSERTdespachoRetornoID(detalle, codVehiculo, codResponsable, codConductor, conductor);
        }

        private bool Registro_AsignacionChoferCamion()
        {
            try
            {
                NCorpal_EntregaSolicitudProducto2 nego = new NCorpal_EntregaSolicitudProducto2();

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                DataSet dsResp = Nresp.get_responsable(codUser);
                string nameResp = "";

                if(dsResp != null && dsResp.Tables[0].Rows.Count > 0)
                {
                    nameResp = dsResp.Tables[0].Rows[0]["nombre"].ToString();
                }

                int codigoVehiculo = int.Parse(dd_listVehiculo.SelectedValue);
                
                int codChofer = int.Parse(hf_codChofer.Value);

                nego.POST_RegistroAsignacionChoferAVehiculo(codigoVehiculo, codChofer, codUser, nameResp);
                return true;
            }
            catch (Exception ex)
            {
                showalert("Error inesperado en el metodo RegistroAsignacionChoferCamion" + ex.Message);
                return false;
            }
        }
      
        /* Registro stock dinamico por vendedor*/
        private bool RegistrarIngresoAlmacenDinamico(int codVendedor, int codProducto, string producto,
                                                        decimal cantidad)
        {
            try
            {
                NCorpal_StockVendedores neg = new NCorpal_StockVendedores();
                neg.POST_registroEntradaStock(codVendedor, codProducto, producto, cantidad);
                return true;
            } catch(Exception ex)
            {
                showalert("Error inesperado en el metodo RegistroIngresoAlmacen. " + ex.Message);
                return false;
            }
        }


        /*FIN*/
        /*******************************  REGISTRAR VENTA *******************************/
        private void registrarVentaAut(int codigoSolicitud, int codigoCliente, string solicitante, string fechaEntrega)
        {
            try
            {
                int codigoMetodoPago = 1;
                decimal montoTotal = 0;
                decimal montoTotalMoneda = montoTotal;
                int codigoMoneda = 1;
                decimal tipoCambio = decimal.Parse("6.96");
                decimal descuentoAdicional = decimal.Parse("0");
                string leyendaF = "leyendaNinguna";
                int factura = 0;

                NA_Responsables Nresp = new NA_Responsables();
                NCorpal_Cliente Ncliente = new NCorpal_Cliente();
                DataSet tuplasCLI = Ncliente.get_ClienteCodigo(codigoCliente);
                int codigoSolicitante = 1;
                //Datos Clientes
                string tiendaName = "", tiendaCorreoCliente = "", municipio = "", tiendaTelefono = "", tiendaDir = "", tiendaNombreRazonSocial = "", numeroDocumento = "";
                string numeroFactura = "";

                if (tuplasCLI.Tables[0].Rows.Count > 0)
                {
                    tiendaName = tuplasCLI.Tables[0].Rows[0][1].ToString();
                    tiendaDir = tuplasCLI.Tables[0].Rows[0][2].ToString();
                    tiendaTelefono = tuplasCLI.Tables[0].Rows[0][3].ToString();
                    municipio = tuplasCLI.Tables[0].Rows[0][4].ToString();
                    tiendaCorreoCliente = tuplasCLI.Tables[0].Rows[0][11].ToString();
                    tiendaNombreRazonSocial = tuplasCLI.Tables[0].Rows[0][12].ToString();
                    numeroDocumento = tuplasCLI.Tables[0].Rows[0][13].ToString();
                }

                NCorpal_Venta Nventa = new NCorpal_Venta();
                bool resultado = Nventa.crearVentas3(codigoCliente, tiendaName, codigoSolicitud,
                    tiendaCorreoCliente, municipio, tiendaTelefono, numeroFactura, tiendaDir,
                    tiendaNombreRazonSocial, numeroDocumento, codigoMetodoPago, montoTotal, codigoMoneda,
                    tipoCambio, montoTotalMoneda, descuentoAdicional, leyendaF, codigoSolicitante, solicitante, factura,
                    fechaEntrega, codigoSolicitud);

                if (!resultado)
                {
                    showalert($"Error al registrar la venta.");
                    return;
                }
                bool resultadoProductoVentas = Nventa.insertarTodoslosProductosAVenta3(codigoSolicitud);

                if (!resultadoProductoVentas)
                {
                    showalert("Error al insertar productos de la venta.");
                }
            }
            catch(Exception ex)
            {
                showalert($"Error al insertar la venta. {ex.Message}");
            }
        }

        /* REGISTRAR DETALLE DESPACHO */
        private bool RegistrarDetalleDespacho(int codigodespacho)
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            bool resultadoGeneral = true;

            foreach (GridViewRow row in gv_solicitudesProductos.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.Cells[0].FindControl("chkSelect");
                if (chkSelect != null && chkSelect.Checked)
                {

                    int codPedido = int.Parse(row.Cells[1].Text);
                    string producto = row.Cells[4].Text;
                    int codigoProducto = int.Parse(row.Cells[3].Text);

                    TextBox txtCantidadAEntregar = (TextBox)row.FindControl("tx_cantidadEntregarOK");
                    float cantidadEntregar;
                    if (!float.TryParse(txtCantidadAEntregar.Text, out cantidadEntregar))
                    {
                        cantidadEntregar = 0;
                    }

                    bool resultado = negocio.POST_INSERTdetalleDespacho(codigodespacho, codPedido, codigoProducto, cantidadEntregar);

                    if (!resultado)
                    {
                        resultadoGeneral = false;
                        break;
                    }
                    else
                    {
                        //showalert("Detalle de despacho insertado correctamente");
                    }
                }
            }
            return resultadoGeneral;
        }
        /*  REGISTRO DETALLE SOLICITUD PEDIDO*/
        private void ProcesarRegistroSolicitudPedido(GridViewRow row)
        {
            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            if(chkSelect != null && chkSelect.Checked)
            {
                int codigoSolicitud = Convert.ToInt32(row.Cells[1].Text);
                int codigoProducto = Convert.ToInt32(row.Cells[3].Text);

                TextBox txtCantidadAEntregar = (TextBox)row.FindControl("tx_cantidadEntregarOK");

                CheckBox chkTipoEntrega = (CheckBox)row.FindControl("chkTipoEntrega");

                string tipoEntrega = chkTipoEntrega != null && chkTipoEntrega.Checked ? "parcial" : "total";

                Label lblCantEntregada = (Label)row.FindControl("lb_cantentregada");

                int codigoVehiculo = int.Parse(dd_listVehiculo.SelectedValue);

                if (string.IsNullOrEmpty(txtCantidadAEntregar.Text))
                {
                    txtCantidadAEntregar.Text = "0";
                }
                ActualizarDetalleSolicitudPedido(codigoSolicitud, codigoProducto, txtCantidadAEntregar, tipoEntrega, lblCantEntregada, codigoVehiculo);
            }
        }
        /*  DETALLE SOLICITUD Y CIERRE AUT DE SOLICITUD PEDIDO*/
        private void ActualizarDetalleSolicitudPedido(int codigoSolicitud, int codigoProducto, TextBox txtCantidadAEntregar, string estadoProducto, Label lblCantEntregada, int codVehiculo)
        {
            try
            {
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
              



                float cantidadEntregar2 = 0;
                if(!float.TryParse(txtCantidadAEntregar.Text, out cantidadEntregar2)){
                    showalert("La cantidad a entregar no es válida");
                    return;
                }

                float cantActual2 = 0;
                if(!float.TryParse(lblCantEntregada.Text, out cantActual2)){
                    showalert("La cantidad entregada previamente no es váliad.");
                    return;
                }

                float cantActual = float.Parse(lblCantEntregada.Text);
                float cantidadTotalEntregada = cantidadEntregar2 + cantActual;
                float restarStock = cantidadEntregar2;

                int codResponsable = obtenerCodResponsable();
                NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();

                string personal = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();

                bool resultado = negocio.UPDATE_camposDetalleSolicitudPedido(codigoSolicitud, codigoProducto, cantidadTotalEntregada, estadoProducto, 
                                                        restarStock, codResponsable, codVehiculo);
                if (!resultado)
                {
                    showalert($"Error al acttualizar la solicitud : {codigoSolicitud} - codigo Producto: {codigoProducto}");
                }
                else
                {
                    bool cierreAut = negocio.update_CierreAutSolicitudProd(codigoSolicitud, codigoSolicitud, personal);
                }
            }
            catch(Exception ex)
            {
                showalert($"Error al actualizar la cantidad. {ex.Message  }");
            }
        }

        private int obtenerCodResponsable()
        {
            try
            {
                NA_Responsables negocio = new NA_Responsables();
                string usuario = Session["NameUser"].ToString();
                string password = Session["passworuser"].ToString();
                return negocio.getCodUsuario(usuario, password);
            }
            catch (Exception ex)
            {
                showalert($"Error al obtener el codigo del responsable.{ex.Message}");
                return 0;
            }
        }
        private int obtenerCodVendedorSolicitud(int codSolicitud)
        {
            try
            {
                NCorpal_EntregaSolicitudProducto2 Nentrega = new NCorpal_EntregaSolicitudProducto2();
                int codVendedor = Nentrega.ObtenerCodVendedor_EntregaSolProductos(codSolicitud);
                return codVendedor;
            }
            catch(Exception ex)
            {
                showalert("Error al obtener el codigo del vendedor. " + ex.Message);
                return 0;
            }
        }

        // cargar datos (det Car y ConductorCar) en gridview
        protected void dd_listVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int codigo = int.Parse(dd_listVehiculo.SelectedValue);
                NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();

                DataSet conductorData = negocio.GET_obtener_UltConductorVehiculo(codigo);

                DataSet carData = negocio.get_detVehiculoGV(codigo);

                if (conductorData.Tables[0].Rows.Count > 0)
                {
                    DataRow row = conductorData.Tables[0].Rows[0];

                    hf_codChofer.Value = row["codigo"].ToString();
                    tx_chofer.Text = row["nombre"].ToString();
                }
                else
                {
                    hf_codChofer.Value = "";
                    tx_chofer.Text = "";
                }
                    
                if (carData.Tables[0].Rows.Count > 0)
                {

                    gv_detCar.DataSource = carData;
                    gv_detCar.DataBind();
                    gv_detCar.Visible = true;
                }
                else
                {
                    gv_detCar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                showalert($"Error al cargar los datos. {ex.Message}");
            }
        }
        // cargar datos vehiculo al dropdownlist
        private void cargarRegistroVehiculosDD()
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            DataSet dsCar = negocio.get_showVehiculosDD();

            if (dsCar != null && dsCar.Tables.Count > 0)
            {
                dd_listVehiculo.DataSource = dsCar.Tables[0];
                dd_listVehiculo.DataTextField = "detalle";
                dd_listVehiculo.DataValueField = "codigo";

                dd_listVehiculo.DataBind();
                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Seleccione un vehiculo", "0");
                dd_listVehiculo.Items.Insert(0, li);
            }
        }
        // cargar list choferes 
        [WebMethod]
        [ScriptMethod]
        public static string[] getListResponsable (string prefixText, int count)
        {
            string nombre = prefixText;
            NA_Responsables nResp = new NA_Responsables();
            DataSet tuplas = nResp.mostrarTodosDatos2(nombre);

            int fin = tuplas.Tables[0].Rows.Count;
            string [] lista = new string [fin];

            for(int i=0; i<fin; i++)
            {
                string cod = tuplas.Tables[0].Rows[i]["codigo"].ToString();
                string nomResp = tuplas.Tables[0].Rows[i]["nombre"].ToString();

                lista[i] = $"{cod} - {nomResp}";

            }
            return lista;
        }


        private void showalert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
        }


        private void ExportarExcel(DataSet datos)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Buffer = true;
            response.Charset = "";
            response.ContentType = "application/vnd.ms-excel";

            string nombre = "Entrega_Solicitud" + Session["BaseDatos"].ToString() + ".xls";
            response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    DataGrid dg = new DataGrid();
                    dg.DataSource = datos;
                    dg.DataBind();
                    dg.RenderControl(htw);

                    response.Output.Write(sw.ToString());
                    response.Flush();
                    response.End();   // AQUÍ SÍ VA
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            DataSet datos = negocio.get_VWRegistrosEntregaSolicitudProductos("Abierto");

            ExportarExcel(datos);

        protected void btn_newChofer_Click(object sender, EventArgs e)
        {
            tx_chofer.Text = string.Empty;
            hf_codChofer.Value = string.Empty;

        }
    }
}


