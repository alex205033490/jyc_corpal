using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;
using static jycboliviaASP.net.Negocio.NA_APIcompras;
using System.Globalization;
using AjaxControlToolkit;
using MySql.Data.MySqlClient;
using MaterialDesignThemes.Wpf.Converters;
using ZstdSharp.Unsafe;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_EntregaSolicitudProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            /*if (tienePermisoDeIngreso(119) == false)
            {                   
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }*/
            if (!IsPostBack)
            {
                Session["despachoListGV"] = null;
                GET_MostrarSolicitudProductos("Abierto");
                cargarRegistroVehiculosDD();
            }

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            tx_entregoSolicitud.Text = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
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

            dd_listVehiculo.SelectedIndex = 0;

            gv_detCar.DataSource = null;
            gv_detCar.DataBind();

            txt_detalleRegistro.Text = string.Empty;
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
     
        /*
        private void RegistrarVentaAut2(int codigoSolicitud, int codigoCliente, string solicitante, string fechaentrega)
        {
            try
            {
                int codigoMetodoPago = 1;
                decimal montoTotal = 0;
                decimal montoTotalMoneda = montoTotal;
                int codigoMoneda = 1;
                decimal tipoCambio = decimal.Parse("6,96");
                decimal descuentoAdicional = decimal.Parse("0");
                string leyendaF = "leyendaNingun";
                int factura = 0;

                NA_Responsables Nresp = new NA_Responsables();
                NCorpal_Cliente negocioCLiente = new NCorpal_Cliente();
                DataSet tuplaCLI = negocioCLiente.get_ClienteCodigo(codigoCliente);

                int codSolicitante = Nresp.getCodigo_NombreResponsable(solicitante);
                // Datos Cliente
                string tiendaName = "", tiendaCorreoCliente = "", municipio = "", tiendaTelefono = "", tiendaDir = "", tiendaNombreRazonSocial = "", numeroDocumento = "";
                string numeroFactura = "";
                
                if (tuplaCLI.Tables[0].Rows.Count > 0)
                {
                    tiendaName = tuplaCLI.Tables[0].Rows[0][1].ToString();
                    tiendaDir = tuplaCLI.Tables[0].Rows[0][2].ToString();
                    tiendaTelefono = tuplaCLI.Tables[0].Rows[0][3].ToString();
                    municipio = tuplaCLI.Tables[0].Rows[0][4].ToString();
                    tiendaCorreoCliente = tuplaCLI.Tables[0].Rows[0][11].ToString();
                    tiendaNombreRazonSocial = tuplaCLI.Tables[0].Rows[0][12].ToString();
                    numeroDocumento = tuplaCLI.Tables[0].Rows[0][13].ToString();
                }
                NCorpal_Venta negocioVenta = new NCorpal_Venta();
                bool resultado = negocioVenta.crearVentas3(codigoCliente, tiendaName, codigoSolicitud, tiendaCorreoCliente, municipio, tiendaTelefono, numeroFactura, tiendaDir,
                    tiendaNombreRazonSocial, numeroDocumento, codigoMetodoPago, montoTotal, codigoMoneda, tipoCambio, montoTotalMoneda, 
                    descuentoAdicional, leyendaF, codSolicitante, solicitante, factura, fechaentrega, codigoSolicitud);
                if (!resultado)
                {
                    showalert("Error al registrar la venta.");
                    return;
                }

                bool resultadoProductosVenta = negocioVenta.insertarTodoslosProductosAVenta3(codigoSolicitud);
               
                if (resultadoProductosVenta)
                {
                    //showalert("Productos insertados en la venta ");
                }
                else
                {
                    showalert($"error al insertar los productos: {codigoSolicitud}");
                }
                
            }
            catch( Exception ex )
            {
                showalert($"Error al insertar la venta. {ex.Message}");
            }
        }*/
      

        protected void gv_solicitudesProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                
                if (chkSelect != null )
                {
                    List<Product> despachoList = (List<Product>)Session["despachoListGV"];

                    if(despachoList != null)
                    {
                        string nroBoleta = e.Row.Cells[1].Text;
                        string producto = e.Row.Cells[2].Text;

                        var selectedProduct = despachoList.FirstOrDefault(p => p.nroboleta == nroBoleta && p.producto == producto);

                        if(selectedProduct != null )
                        {
                            chkSelect.Checked = true;
                            e.Row.CssClass += "highlighted";
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
            CheckBox chkBox = sender as CheckBox;
            GridViewRow row = (chkBox.NamingContainer as GridViewRow);

            int codigoSolicitud = int.Parse(row.Cells[1].Text);
            string nroBoleta = row.Cells[2].Text;
            string producto = row.Cells[4].Text;

            TextBox txtCantidadEntregada = row.FindControl("tx_cantidadEntregarOK") as TextBox;
            int cantidadEntregada = 0;
            int.TryParse(txtCantidadEntregada.Text, out cantidadEntregada);

            Product selectedProduct = new Product
            {
                codigoSolicitud = codigoSolicitud,
                nroboleta = nroBoleta,
                producto = producto,
                cantidadEntregada = cantidadEntregada
            };
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
                p.producto == producto );
            }

            Session["despachoListGV"] = despachoList;

            gv_despachoProductos.DataSource = despachoList;
            gv_despachoProductos.DataBind();
        }

        /***********************************   VW solicitud entrega producto     *************************************/
        protected void btn_registrarDespacho_Click(object sender, EventArgs e)
        {
            try
            {
                int codVehiculo = int.Parse(dd_listVehiculo.SelectedValue);
                string detalle = txt_detalleRegistro.Text.Trim();
                int codResponsable = obtenerCodResponsable();

                if(dd_listVehiculo.SelectedIndex == 0)
                {
                    showalert("Por favor seleccione un vehiculo válido");
                    return;

                }

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
                        int codigoCliente = int.Parse(row.Cells[11].Text);
                        string solicitante = row.Cells[10].Text;
                        string fechaEntrega = aFecha2(row.Cells[9].Text);

                        RegistrarVentaConDetalle123(codigoSolicitud, codigoCliente, solicitante, fechaEntrega);
                    }
                }

                limpiarForm();
                GET_MostrarSolicitudProductos("Abierto");
                showalert("Registro insertado exitosamente.");
            }
            catch(Exception ex)
            {
                showalert($"Error inesperado: {ex.Message}");
            }




            /*
            string detalle = txt_detalleRegistro.Text.Trim();
            int codVehiculo = int.Parse(dd_listVehiculo.SelectedValue);

            NA_Responsables NResponsable = new NA_Responsables();
            string usu = Session["NameUser"].ToString();
            string pass = Session["passworuser"].ToString();
            int codResponsable = NResponsable.getCodUsuario(usu, pass);

            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            int codDespacho = negocio.POST_INSERTdespachoRetornoID(detalle, codVehiculo, codResponsable);

            if (codDespacho <= 0)
            {
                showalert("Error al registrar el despacho principal");
                return; 
            }

            if (RegistrarDetalleDespacho(codDespacho))
            {
                //showalert($"Detalle de despacho Nro: {codDespacho} registrado.");
            }
            foreach (GridViewRow row in gv_solicitudesProductos.Rows)
            {
                ProcesarRegistroSolicitudPedido(row);

                int codigoSolicitud = int.Parse(row.Cells[1].Text);
                int codigoCliente = int.Parse(row.Cells[11].Text);
                string solicitante = (row.Cells[10].Text);
                string fechaEntregarow = (row.Cells[9].Text);
                string fechaEntrega = aFecha2(fechaEntregarow);

                CheckBox chk = row.FindControl("chkSelect") as CheckBox;
                if (chk != null && chk.Checked)
                {
                    registrarVentaAut(codigoSolicitud, codigoCliente, solicitante, fechaEntrega);
                    showalert($"Solicitud registrada.");
                    limpiarForm();
                    GET_MostrarSolicitudProductos("Abierto");
                }
            }
            */
        }


        /*PARTE 2 AUTOCORR*/
        private void RegistrarVentaConDetalle123(int codigoSolicitud, int codigoCliente, string solicitante, string fechaEntrega)
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

                int codResponsable = obtenerCodResponsable();

                NCorpal_Venta nventa = new NCorpal_Venta();

                bool ventaCreada = nventa.crearVentas3(
                    codigoCliente, tiendaName, codigoSolicitud, correo, municipio, telefono,
                    "", direccion, razonSocial, documento,
                    1, 0, 1, tipoCambio, 0, 0, "LeyendaNinguna",
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
                string personal = tx_entregoSolicitud.Text.Trim();

                NCorpal_EntregaSolicitudProducto2 nentrega_solicitud = new NCorpal_EntregaSolicitudProducto2();
                bool actualizado = nentrega_solicitud.UPDATE_camposDetalleSolicitudPedido(codigoSolicitud, codigoProducto, totalEntregado, estadoEntrega, stockARestar, codResponsable, codVehiculo);

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

        private int CrearDespachoPrincipal123(string detalle, int codVehiculo, int codResponsable)
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            return negocio.POST_INSERTdespachoRetornoID(detalle, codVehiculo, codResponsable);
        }
      






        /*FIN*/




        /*  REGISTRAR VENTA */
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

                int codigoVehiculo = dd_listVehiculo.SelectedIndex;

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

                string personal = tx_entregoSolicitud.Text.Trim();

                bool resultado = negocio.UPDATE_camposDetalleSolicitudPedido(codigoSolicitud, codigoProducto, cantidadTotalEntregada, estadoProducto, restarStock, codResponsable, codVehiculo);
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
        // cargar datos (det Car) en gridview
        protected void dd_listVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int codigo = int.Parse(dd_listVehiculo.SelectedValue);
                NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();

                DataSet carData = negocio.get_detVehiculoGV(codigo);

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




        private void showalert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }
    }
}

