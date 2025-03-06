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

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_EntregaSolicitudProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(119) == false)
            {                   
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {
                GET_MostrarSolicitudProductos("Abierto");
                cargarRegistroVehiculosDD();
                //buscarDatosSolicitud("","","Abierto");
            }

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            tx_entregoSolicitud.Text = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
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

        /*protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string nroSolicitud = tx_nrosolicitud.Text;
            string solicitante = tx_SolicitanteProducto.Text;
            string estado = dd_estadoCierre.SelectedItem.Text;
            buscarDatosSolicitud(nroSolicitud, solicitante, estado);
        }*/

        private void buscarDatosSolicitud(string nroSolicitud, string solicitante, string estadoSolicitud)
        {
            NCorpal_SolicitudEntregaProducto ncc = new NCorpal_SolicitudEntregaProducto();
            DataSet datos = ncc.get_solicitudesRealizadasProductos(nroSolicitud, solicitante, estadoSolicitud);
            gv_solicitudesProductos.DataSource = datos;
            gv_solicitudesProductos.DataBind();
        }
        /*
        protected void gv_solicitudesProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();

            GridViewRow row = gv_solicitudesProductos.SelectedRow;

            TextBox tx_cantidadEntregarOK = (TextBox)row.FindControl("tx_cantidadEntregarOK");
            string cantidadEntregarText = tx_cantidadEntregarOK.Text.Trim();

            if (string.IsNullOrEmpty(cantidadEntregarText) || float.Parse(cantidadEntregarText) == 0)
            {
                showalert("La cantidad no puede ser 0 o estar vacia.");
                return;
            }

            int codigoSolicitud = Convert.ToInt32(gv_solicitudesProductos.DataKeys[row.RowIndex].Value);
            int codigoProducto = Convert.ToInt32(((Label)row.FindControl("lbl_codigoProducto")).Text);

            float cantidadEntregada = float.Parse(cantidadEntregarText);

            float restarStock = cantidadEntregada;

            bool resultado = negocio.update_cantProductosEntregados(codigoSolicitud, codigoProducto, cantidadEntregada, restarStock);

            if (resultado)
            {
                showalert($"Datos actualizados");
            }
            else
            {
                showalert("Hubo un error al actualizar los datos ");
            }



        }
        */
        /*private void seleccionarDatos()
        {
            if(gv_solicitudesProductos.SelectedIndex > -1){
                int codigoSolicitud = int.Parse(gv_solicitudesProductos.SelectedRow.Cells[5].Text);
                int codigoProducto = int.Parse(gv_solicitudesProductos.SelectedRow.Cells[8].Text);
                string nroboleta = gv_solicitudesProductos.SelectedRow.Cells[6].Text;
                string fechaentrega = gv_solicitudesProductos.SelectedRow.Cells[10].Text;
                string horaentrega = gv_solicitudesProductos.SelectedRow.Cells[11].Text;
                string personaSolicitud = gv_solicitudesProductos.SelectedRow.Cells[7].Text;
                tx_SolicitanteProducto.Text = HttpUtility.HtmlDecode(personaSolicitud);
                
                dd_estadoCierre.SelectedValue = gv_solicitudesProductos.SelectedRow.Cells[12].Text;

               
                tx_fechaEngrega.Text = fechaentrega;
                tx_horaentrega.Text = horaentrega;

                NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
                DataSet datos = negocio.get_detSolicitudProducto(codigoSolicitud, codigoProducto);

            }
        }*/

        protected void btn_anularSolicitud_click(object sender, EventArgs e)
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            List<int> selectCodSolicitud = new List<int>();
            List<int> selectCodProducto = new List<int>();


            foreach (GridViewRow row in gv_solicitudesProductos.Rows)
            {
                CheckBox chkAnular = (CheckBox)row.FindControl("chkSelect");

                if(chkAnular != null && chkAnular.Checked)
                {
                    int codigo = Convert.ToInt32(gv_solicitudesProductos.DataKeys[row.RowIndex].Value);
                    int codProducto = Convert.ToInt32(((Label)row.FindControl("lb_codproducto")).Text);

                    selectCodSolicitud.Add(codigo);
                    selectCodProducto.Add(codProducto);
                }
            }
            if (selectCodSolicitud.Count > 0)
            {
                bool exito = negocio.update_RetirarSolicitud(selectCodSolicitud, selectCodProducto);

                if (exito)
                {
                    showalert($"Se han anulado los registros exitosamente.");
                    limpiarDatos();
                }
                else
                {
                    string solicitudesStr = string.Join(",", selectCodSolicitud);
                    string productosStr = string.Join(",", selectCodProducto);
                    showalert($"Hubo un error al anular el registro : {solicitudesStr}, {productosStr}");
                }
            }
            else
            {
                showalert("Por favor, seleccione al menos un registro");
            }
        }

        private void eliminarSolicitud()
        {
            foreach (GridViewRow row in gv_solicitudesProductos.Rows)
            {
                CheckBox cbokeliminar = row.Cells[1].FindControl("cbk_eliminar") as CheckBox;
                if (cbokeliminar.Checked)
                {
                    int codigoSolicitud = int.Parse(row.Cells[2].Text);
                    NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
                    bool bandera = nss.eliminarSolicitud(codigoSolicitud);
                }
            }
            buscarDatosSolicitud("", "", "Abierto");
        }

        protected void bt_actualizar_Click(object sender, EventArgs e)
        {
            //actualizarTodoslosDatos();
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

        private void actualizarTodoslosDatos()
        {
            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            if (gv_solicitudesProductos.SelectedIndex > -1)
            {
                int codigoSolicitud = int.Parse(gv_solicitudesProductos.SelectedRow.Cells[2].Text);
               // float sumStock = nss.get_SumStockTotal(codigoSolicitud);
                bool banderaerror = false;
                foreach (GridViewRow row in gv_solicitudesProductos.Rows)
                {                    
                    int codProducto ;
                    int.TryParse(row.Cells[0].Text, out codProducto);
                    string tipoSolicitud = row.Cells[4].Text;
                    float StockProductos = nss.get_Stock(codProducto, tipoSolicitud);

                    float cantAentregar;
                    TextBox tx_cant2 = row.Cells[6].FindControl("tx_cantidadEntregarOK") as TextBox;
                    float.TryParse(tx_cant2.Text, out cantAentregar);
                    if (cantAentregar > StockProductos) {
                        banderaerror = true;
                    }                   
                }

                if (banderaerror == false) {
                    foreach (GridViewRow row in gv_solicitudesProductos.Rows)
                    {
                        int codProducto;
                        int.TryParse(row.Cells[0].Text, out codProducto);
                        float cantAentrego;
                        Label tx_cant = row.Cells[5].FindControl("lb_cantentregado") as Label;
                        float.TryParse(tx_cant.Text, out cantAentrego);

                        float cantAentregar;
                        TextBox tx_cant2 = row.Cells[6].FindControl("tx_cantidadEntregarOK") as TextBox;
                        float.TryParse(tx_cant2.Text, out cantAentregar);

                        float totalEntregado = cantAentrego + cantAentregar;
                        bool bb = nss.update_cantProductosEntregados(codigoSolicitud, codProducto, totalEntregado, cantAentregar);
                    }

                    NA_Responsables Nresp = new NA_Responsables();
                    string usuarioAux = Session["NameUser"].ToString();
                    string passwordAux = Session["passworuser"].ToString();
                    int codresponsable = Nresp.getCodUsuario(usuarioAux, passwordAux);
                    string nombreResponsable = Nresp.get_responsable(codresponsable).Tables[0].Rows[0][1].ToString();
                    string fechaEntrega = aFecha(tx_fechaEngrega.Text);
                    string horaEntrega = tx_horaentrega.Text;

                    string estadoCierre = dd_estadoCierre.SelectedItem.Text;
                    string motivoCierre = dd_motivoCierre.SelectedItem.Text;
                    string EstadoActual = gv_solicitudesProductos.SelectedRow.Cells[10].Text;

                    bool cerrado = nss.update_cerrarSolicitud(codigoSolicitud, codresponsable, nombreResponsable, estadoCierre, motivoCierre, fechaEntrega, horaEntrega);
                    if (cerrado == true)
                    {                                                
                        if (estadoCierre.Equals("Cerrado") && EstadoActual.Equals("Abierto")) {
                            string cliente = gv_solicitudesProductos.SelectedRow.Cells[11].Text;
                            NCorpal_Cliente nc = new NCorpal_Cliente();
                            DataSet tuplaCli = nc.get_ClienteNombreEspecifico(cliente);
                            int codCliente = 0;
                            string direccion = "";
                            string correoCliente = "";
                            string municipio = "Santa Cruz";
                            string telefono = "";
                            string nombreRazonSocial = "";
                            string numeroDocumento = "";
                            int codigoMetodoPago = 1;
                            bool factura = false;
                            string numeroFactura = codigoSolicitud.ToString();
                            int codigoMoneda = 1;
                            decimal descuentoAdicional = decimal.Parse("0");
                            string leyendaF = "LeyendaNinguna";
                            decimal tipoCambio = decimal.Parse("6,96");
                            decimal montoTotal = decimal.Parse(gv_solicitudesProductos.SelectedRow.Cells[9].Text);
                            decimal montoTotalMoneda = montoTotal;
                            string solicitandte  = tx_SolicitanteProducto.Text;
                            int codigoSolicitante = Nresp.getCodigo_NombreResponsable(solicitandte);



                            if (tuplaCli.Tables[0].Rows.Count > 0)
                            {
                                codCliente = int.Parse(tuplaCli.Tables[0].Rows[0][0].ToString());
                                direccion = tuplaCli.Tables[0].Rows[0][2].ToString();
                                telefono = tuplaCli.Tables[0].Rows[0][3].ToString();
                                municipio = tuplaCli.Tables[0].Rows[0][4].ToString();
                                correoCliente = tuplaCli.Tables[0].Rows[0][14].ToString();
                                nombreRazonSocial = tuplaCli.Tables[0].Rows[0][12].ToString();
                                numeroDocumento = tuplaCli.Tables[0].Rows[0][13].ToString();
                            }

                            NCorpal_Venta nv = new NCorpal_Venta();
                            bool banderaV = nv.crearVenta(codCliente, cliente, correoCliente, municipio, telefono, direccion, numeroFactura, nombreRazonSocial, numeroDocumento, codigoMetodoPago, montoTotal, codigoMoneda, tipoCambio, montoTotalMoneda, descuentoAdicional, leyendaF, codigoSolicitante, solicitandte, factura, fechaEntrega, codigoSolicitud);
                            int codigoVenta = nv.get_codigoVentaUltimoInsertado(cliente, nombreRazonSocial, nombreResponsable);
                            bool banderaAllTodosProducto = nv.insertarTodoslosProductosAVenta(codigoVenta, codigoSolicitud);

                        }

                        limpiarDatos();
                        buscarDatosSolicitud("", "", estadoCierre);
                        Session["codigoEntregaSolicitudProducto"] = codigoSolicitud;
                        Response.Redirect("../Presentacion/FCorpal_ReporteEntregaSolicitudProducto.aspx");
                    }
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Suma Entrega mayor a Stock') </script>");  

                }else
                    Response.Write("<script type='text/javascript'> alert('Error: Suma Entrega mayor a Stock') </script>");
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_fechaEngrega.Text = string.Empty;
            tx_horaentrega.Text = string.Empty;
            tx_SolicitanteProducto.Text = string.Empty;
            GET_MostrarSolicitudProductos("Abierto");
            cargarRegistroVehiculosDD();
        }

        protected void gv_detallesolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void bt_verRecibo_Click(object sender, EventArgs e)
        {
            //verReciboSeleccionado();
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

        /* -------- P2 ------- */
        //Mostrar Registros
        private void GET_MostrarSolicitudProductos(string estadoSolicitud)
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            DataSet datos = negocio.get_VWRegistrosEntregaSolicitudProductos(estadoSolicitud);
            gv_solicitudesProductos.DataSource = datos;
            gv_solicitudesProductos.DataBind();
        }
        private void GET_MostrarXVehiculoSolicitudesProd()
        {
            try
            {
                int codigo = int.Parse(dd_listVehiculo.SelectedValue);
                NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();

                DataSet carData = negocio.get_VWRegistrosEntregaSolicitudProductoXCamion("Abierto", codigo);
                if (carData.Tables[0].Rows.Count > 0)
                {
                    gv_solicitudesProductos.DataSource = carData;
                    gv_solicitudesProductos.DataBind();
                    gv_solicitudesProductos.Visible = true;
                }
                else
                {
                    GET_MostrarSolicitudProductos("Abierto");
                }
            }
            catch(Exception ex)
            {
                showalert($"Error al cargar los datos. {ex.Message}");
            }
        }

        protected void dd_listVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int codigo = int.Parse(dd_listVehiculo.SelectedValue);
                NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();

                DataSet carData = negocio.get_VWRegistrosEntregaSolicitudProductoXCamion("Abierto",codigo);

                if (carData.Tables[0].Rows.Count > 0)
                {
                    
                    gv_solicitudesProductos.DataSource = carData;
                    gv_solicitudesProductos.DataBind();
                    gv_solicitudesProductos.Visible = true;

                }
                else
                {
                    GET_MostrarSolicitudProductos("Abierto");
                }
                
            }
            catch(Exception ex)
            {
                showalert($"Error al cargar los datos. {ex.Message}");
            }
        }

        private void cargarRegistroVehiculosDD()
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            DataSet dsCar = negocio.get_showCarDD();

            if(dsCar != null && dsCar.Tables.Count > 0)
            {
                dd_listVehiculo.DataSource = dsCar.Tables[0];
                dd_listVehiculo.DataTextField = "Vehiculo";
                dd_listVehiculo.DataValueField = "codvehiculo";

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

        protected void gv_solicitudesProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if(e.CommandName == "GuardarCantidad")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gv_solicitudesProductos.Rows[index];

                int codigoSolicitud = Convert.ToInt32(row.Cells[5].Text);
                int codigoProducto = Convert.ToInt32(row.Cells[7].Text);
                string producto = row.Cells[9].Text;

                // txt Cantidad a entregar
                TextBox txtCantidadAEntregar = (TextBox)row.FindControl("tx_cantidadEntregarOK");

                // lb Cantidad entregada
                Label lblCantEntregada = (Label)row.FindControl("lb_cantentregada");

                bool cantidadValida = validarCantidad(txtCantidadAEntregar, lblCantEntregada);

                if (cantidadValida)
                {
                    ActualizarCantidad(codigoSolicitud, codigoProducto, txtCantidadAEntregar, lblCantEntregada);
                    txtCantidadAEntregar.Text = "";
                    GET_MostrarXVehiculoSolicitudesProd();
                    showalert($"Solicitud entregada exitosamente.");
                }
            }
        }

        private bool validarCantidad(TextBox txtCantidadAEntregar, Label lblCantEntregada)
        {
            try
            {
                if(txtCantidadAEntregar != null && !string.IsNullOrEmpty(txtCantidadAEntregar.Text))
                {
                    float cantidadEntregar = float.Parse(txtCantidadAEntregar.Text);

                    if (cantidadEntregar < 0)
                    {
                        showalert("La cantidad es inválida");
                        return false;
                    }
                    float cantActual = float.Parse(lblCantEntregada.Text);
                    GridViewRow row = (GridViewRow)txtCantidadAEntregar.NamingContainer;
                    int stockDisponible = int.Parse(((Label)row.FindControl("lb_stockAlmacen")).Text);

                    if(cantidadEntregar > stockDisponible)
                    {
                        showalert("Cantidad no válida. Supera el stock disponible");
                        return false;
                    }
                    
                    return true;
                }
                else
                {
                    showalert("Ingrese una cantidad válida");
                    return false;
                }
            }
            catch(Exception ex)
            {
                showalert($"error al validar la cantidad: {ex.Message}");
                return false;
            }
        }

        private void ActualizarCantidad(int codigoSolicitud, int codigoProducto, TextBox txtCantidadAEntregar, Label lblCantEntregada)
        {
            try
            {
                float cantidadEntregar = float.Parse(txtCantidadAEntregar.Text);
                float cantActual = float.Parse(lblCantEntregada.Text);
                float cantEntregadaAnterior = cantActual;

                float cantidadTotalEntregada = cantidadEntregar + cantActual;
                float restarStock = cantidadEntregar - cantEntregadaAnterior;

                NA_Responsables NResponsable = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = NResponsable.getCodUsuario(usuarioAux, passwordAux);

                NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
                bool resultado = negocio.update_cantProductosEntregados(codigoSolicitud, codigoProducto, cantidadTotalEntregada, restarStock);

                if (resultado)
                {
                    //showalert($"Se ha actualizado el registro exitosamente.");
                    negocio.update_CierreAutSolicitudProd(codigoSolicitud, codUser);


                }
                else
                {
                    showalert($"Error al actualizar el producto con codigo: {codigoProducto}");
                }
            }
            catch(Exception ex)
            {
                showalert($"Error al actualizar la cantidad: {ex.Message}");
            }
        }

        protected void gv_solicitudesProductos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gv_solicitudesProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                if (chkSelect != null && chkSelect.Checked)
                {
                    e.Row.CssClass += "highlighted";
                }

            }
        }
    }
}