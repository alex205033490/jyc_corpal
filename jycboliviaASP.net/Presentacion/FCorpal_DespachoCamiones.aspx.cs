using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_DespachoCamiones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(148) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                mostrarRegistrosDespachoProductos("", "", "Abierto", 0);
                cargarVehiculos();
            }
        }

        private void mostrarRegistrosDespachoProductos(string fechadesde, string fechahasta, string estado, int codVehiculo)
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            DataSet datos = negocio.get_despachosdeCamiones(fechadesde, fechahasta, estado, codVehiculo);
            gv_despachos.DataSource = datos;
            gv_despachos.DataBind();
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

        // Cargar Vehiculos 
        private void cargarVehiculos()
        {
            NA_SolicitudEntregaProductoACamion negocio = new NA_SolicitudEntregaProductoACamion();

            DataSet dsVehiculos = negocio.get_ShowVehiculos();

            if (dsVehiculos != null && dsVehiculos.Tables.Count > 0)
            {
                dd_listVehiculo.DataSource = dsVehiculos.Tables[0];
                dd_listVehiculo.DataTextField = "detalle";
                dd_listVehiculo.DataValueField = "codigo";

                dd_listVehiculo.DataBind();

                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Seleccione un Vehiculo", "0");
                dd_listVehiculo.Items.Insert(0, li);
            }
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

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            dd_estadoCierre.SelectedIndex = 1;
            tx_fechaDesdeDespacho.Text = "";
            tx_fechaHastaDespacho.Text = "";
            gv_despachos.SelectedIndex = -1;
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string fechadesde = convertidorFecha(tx_fechaDesdeDespacho.Text);
            string fechahasta = convertidorFecha(tx_fechaHastaDespacho.Text);
            string estado = dd_estadoCierre.SelectedItem.Text;
            int codVehiculo = int.Parse(dd_listVehiculo.SelectedValue);
            mostrarRegistrosDespachoProductos(fechadesde, fechahasta, estado, codVehiculo);
        }

        protected void bt_actualizar_Click(object sender, EventArgs e)
        {
            guardarDatos();
        }

        // NUEVO: EVENTO BOTÓN MODIFICAR
        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            if (gv_despachos.SelectedIndex >= 0)
            {
                int codDespacho = Convert.ToInt32(gv_despachos.DataKeys[gv_despachos.SelectedIndex]["codigo"]);
                hf_codDespachoModificar.Value = codDespacho.ToString();
                gv_detalleModificar.EditIndex = -1;
                CargarDetalleModificar(codDespacho);
                mpeModificar.Show();
            }
            else
            {
                showalert("Seleccione un registro de la tabla para modificar.");
            }
        }

        // NUEVO: EVENTO CERRAR MODAL
        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            mpeModificar.Hide();
        }

        /*  ----   MODIFICAR DESPACHO: EDITAR CANTIDAD / ELIMINAR LINEA   ----*/

        private void CargarDetalleModificar(int codDespacho)
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
            DataSet datos = negocio.GET_DetalleDespachoParaModificar(codDespacho);
            gv_detalleModificar.DataSource = datos;
            gv_detalleModificar.DataBind();
        }

        // El ModalPopupExtender se re-renderiza oculto en CADA postback (incluso los "async" del
        // UpdatePanel), así que hay que volver a invocar Show() al final de todo evento originado
        // dentro del modal, si no la ventana "se cierra" sola aunque la página no haya recargado.
        protected void gv_detalleModificar_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_detalleModificar.EditIndex = e.NewEditIndex;
            CargarDetalleModificar(Convert.ToInt32(hf_codDespachoModificar.Value));
            mpeModificar.Show();
        }

        protected void gv_detalleModificar_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_detalleModificar.EditIndex = -1;
            CargarDetalleModificar(Convert.ToInt32(hf_codDespachoModificar.Value));
            mpeModificar.Show();
        }

        protected void gv_detalleModificar_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int idDetalle = Convert.ToInt32(gv_detalleModificar.DataKeys[e.RowIndex].Value);

                GridViewRow fila = gv_detalleModificar.Rows[e.RowIndex];
                TextBox txtCantidad = (TextBox)fila.FindControl("txtEditCantidad");

                if (!float.TryParse(txtCantidad.Text.Replace(",", "."),
                    System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture,
                    out float nuevaCantidad) || nuevaCantidad < 0)
                {
                    showalert("Ingrese una cantidad válida.");
                    mpeModificar.Show();
                    return;
                }

                bool exito = GuardarModificacionCantidad(idDetalle, nuevaCantidad);

                gv_detalleModificar.EditIndex = -1;
                CargarDetalleModificar(Convert.ToInt32(hf_codDespachoModificar.Value));

                if (!exito)
                {
                    showalert("No se pudo actualizar la cantidad.");
                }

                mpeModificar.Show();
            }
            catch (Exception ex)
            {
                showalert("Error al actualizar la cantidad: " + ex.Message);
                mpeModificar.Show();
            }
        }

        // "Eliminar" borra TODOS los productos de la solicitud (boleta) dentro de este despacho,
        // no solo el producto de la fila donde se hizo clic -- un despacho puede traer varias
        // solicitudes con productos repetidos, y el usuario quiere quitar la solicitud completa.
        protected void gv_detalleModificar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int codDespacho = Convert.ToInt32(hf_codDespachoModificar.Value);
                int codPedido = Convert.ToInt32(gv_detalleModificar.DataKeys[e.RowIndex].Values["codpedido"]);

                bool exito = EliminarSolicitudDelDespacho(codDespacho, codPedido);

                CargarDetalleModificar(codDespacho);

                if (!exito)
                {
                    showalert("No se pudo eliminar la solicitud del despacho.");
                }

                mpeModificar.Show();
            }
            catch (Exception ex)
            {
                showalert("Error al eliminar la solicitud: " + ex.Message);
                mpeModificar.Show();
            }
        }

        // Cambia la cantidad de una línea de despacho y sincroniza por delta la solicitud
        // original y (si el despacho ya está Cerrado) el almacén móvil. No toca tbcorpal_producto.stock.
        private bool GuardarModificacionCantidad(int idDetalle, float nuevaCantidad)
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();

            DataSet dsLinea = negocio.GET_LineaDespachoPorId(idDetalle);
            if (dsLinea == null || dsLinea.Tables[0].Rows.Count == 0)
            {
                showalert("No se encontró la línea de despacho.");
                return false;
            }

            DataRow linea = dsLinea.Tables[0].Rows[0];
            int codDespacho = Convert.ToInt32(linea["coddespacho"]);
            int codPedido = Convert.ToInt32(linea["codpedido"]);
            int codProd = Convert.ToInt32(linea["codprod"]);
            float cantidadActual = Convert.ToSingle(linea["cantentregada"]);
            bool fraccionado = linea["contenedorfraccionado"] != DBNull.Value && Convert.ToBoolean(linea["contenedorfraccionado"]);
            string producto = linea["producto"].ToString();
            string medida = linea["medida"].ToString();
            string medidaFraccionada = linea["medidaunidadcontenido"].ToString();

            float delta = nuevaCantidad - cantidadActual;
            if (delta == 0)
            {
                return true;
            }

            if (!negocio.UPDATE_CantidadLineaDespacho(idDetalle, nuevaCantidad))
            {
                return false;
            }

            negocio.UPDATE_SyncCantidadSolicitud(codPedido, codProd, fraccionado, delta);

            SincronizarAlmacenMovilSiCorresponde(negocio, codDespacho, codProd, producto, medida, medidaFraccionada, fraccionado, delta);

            //----------------historial-------------
            NA_Historial nhistorial = new NA_Historial();
            nhistorial.insertar(ObtenerCodUserActual(),
                $"Se modificó la cantidad del despacho {codDespacho}, producto {producto} (pedido {codPedido}), de {cantidadActual} a {nuevaCantidad}.");
            //--------------------------------------

            return true;
        }

        // Elimina TODOS los productos de una solicitud dentro de un despacho (no producto por
        // producto): marca cada línea como eliminada (estadoentrega = 0, cantentregada = 0) y
        // sincroniza por delta negativo la solicitud original y (si el despacho ya está Cerrado)
        // el almacén móvil, uno por producto.
        private bool EliminarSolicitudDelDespacho(int codDespacho, int codPedido)
        {
            NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();

            DataSet dsLineas = negocio.GET_LineasDespachoPorPedido(codDespacho, codPedido);
            if (dsLineas == null || dsLineas.Tables[0].Rows.Count == 0)
            {
                showalert("No se encontraron productos de esa solicitud en este despacho.");
                return false;
            }

            // La cabecera de ruta (si existe) es la señal de que el despacho ya está Cerrado y por
            // lo tanto tiene datos en almacén móvil -- se consulta una sola vez para todo el pedido.
            DataSet dsCabecera = negocio.GET_CabeceraRutaParaAlmacen(codDespacho);
            bool despachoCerrado = dsCabecera != null && dsCabecera.Tables[0].Rows.Count > 0;
            int codRuta = 0, codChofer = 0, codVehiculo = 0;
            if (despachoCerrado)
            {
                DataRow cabecera = dsCabecera.Tables[0].Rows[0];
                codRuta = Convert.ToInt32(cabecera["codruta"]);
                codChofer = Convert.ToInt32(cabecera["codchofer"]);
                codVehiculo = Convert.ToInt32(cabecera["codvehiculo"]);
            }

            bool exitoGeneral = true;
            var detalleHistorial = new System.Text.StringBuilder();
            string nroboleta = null;
            string cliente = null;

            foreach (DataRow linea in dsLineas.Tables[0].Rows)
            {
                int idDetalle = Convert.ToInt32(linea["codigo"]);
                int codProd = Convert.ToInt32(linea["codprod"]);
                float cantidadActual = Convert.ToSingle(linea["cantentregada"]);
                bool fraccionado = linea["contenedorfraccionado"] != DBNull.Value && Convert.ToBoolean(linea["contenedorfraccionado"]);
                string producto = linea["producto"].ToString();
                string medida = linea["medida"].ToString();
                string medidaFraccionada = linea["medidaunidadcontenido"].ToString();
                nroboleta = linea["nroboleta"] != DBNull.Value ? linea["nroboleta"].ToString() : nroboleta;
                cliente = linea["cliente"] != DBNull.Value ? linea["cliente"].ToString() : cliente;

                if (!negocio.SoftDelete_LineaDespacho(idDetalle))
                {
                    exitoGeneral = false;
                    showalert($"No se pudo eliminar el producto {producto}.");
                    continue;
                }

                float delta = -cantidadActual;
                negocio.UPDATE_SyncCantidadSolicitud(codPedido, codProd, fraccionado, delta);

                if (despachoCerrado)
                {
                    negocio.SincronizarCantidadAlmacenMovil(codDespacho, codRuta, codChofer, codVehiculo,
                        codProd, producto, medida, medidaFraccionada, fraccionado, (decimal)delta);
                }

                detalleHistorial.Append($"{producto} ({cantidadActual}); ");
            }

            //----------------historial-------------
            NA_Historial nhistorial = new NA_Historial();
            nhistorial.insertar(ObtenerCodUserActual(),
                $"Se eliminó la solicitud (boleta {nroboleta}, cliente {cliente}, pedido {codPedido}) del despacho {codDespacho}. Productos: {detalleHistorial}");
            //--------------------------------------

            return exitoGeneral;
        }

        private int ObtenerCodUserActual()
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            return Nresp.getCodUsuario(usuarioAux, passwordAux);
        }

        // tbcorpal_almacenmovil solo tiene datos de un despacho una vez que se cerró (Entregado).
        // Se usa la existencia de la ruta registrada (GET_CabeceraRutaParaAlmacen) como señal de que ya se cerró.
        private void SincronizarAlmacenMovilSiCorresponde(NCorpal_EntregaSolicitudProducto2 negocio, int codDespacho,
            int codProd, string producto, string medida, string medidaFraccionada, bool fraccionado, float delta)
        {
            DataSet dsCabecera = negocio.GET_CabeceraRutaParaAlmacen(codDespacho);
            if (dsCabecera == null || dsCabecera.Tables[0].Rows.Count == 0)
            {
                return;
            }

            DataRow cabecera = dsCabecera.Tables[0].Rows[0];
            int codRuta = Convert.ToInt32(cabecera["codruta"]);
            int codChofer = Convert.ToInt32(cabecera["codchofer"]);
            int codVehiculo = Convert.ToInt32(cabecera["codvehiculo"]);

            negocio.SincronizarCantidadAlmacenMovil(codDespacho, codRuta, codChofer, codVehiculo,
                codProd, producto, medida, medidaFraccionada, fraccionado, (decimal)delta);
        }

        private void guardarDatos()
        {
            if (gv_despachos.SelectedIndex >= 0)
            {

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                string estado = "Cerrado";
                int codigo = Convert.ToInt32(gv_despachos.DataKeys[gv_despachos.SelectedIndex]["codigo"]);
                string vehiculo = gv_despachos.DataKeys[gv_despachos.SelectedIndex]["Vehiculo"].ToString();

                NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();
                bool bandera = negocio.update_despachodeproductosCamiones(codigo, estado, codUser);
                if (bandera)
                {

                    Registro_RutaPuntosDEntrega_Despacho(codigo, vehiculo);
                    RegistrarAlmacenMovil(codigo);
                    Session["codigoDespacho"] = codigo;
                    Session["ReporteGeneral"] = "Reporte_DespachoProductoCamionEntrega";
                    Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Guardado') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Dato') </script>");
        }

        private void RegistrarAlmacenMovil(int codDespacho)
        {
            try
            {
                NCorpal_EntregaSolicitudProducto2 negocio = new NCorpal_EntregaSolicitudProducto2();

                DataSet dsCabecera = negocio.GET_CabeceraRutaParaAlmacen(codDespacho);

                if (dsCabecera == null || dsCabecera.Tables[0].Rows.Count == 0)
                {
                    showalert("Aviso: No se encontraron datos de ruta para cargar el almacén móvil.");
                    return;
                }

                DataRow rowCabecera = dsCabecera.Tables[0].Rows[0];
                int codRuta = Convert.ToInt32(rowCabecera["codruta"]);
                int codChofer = Convert.ToInt32(rowCabecera["codchofer"]);
                int codVehiculo = Convert.ToInt32(rowCabecera["codvehiculo"]);

                DataSet dsProductos = negocio.GET_ProductosParaAlmacenMovil(codDespacho);

                if (dsProductos != null && dsProductos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow rowProd in dsProductos.Tables[0].Rows)
                    {
                        int codProducto = Convert.ToInt32(rowProd["codproducto"]);
                        string producto = rowProd["producto"].ToString();
                        decimal cantidadTotal = rowProd["cantidad_total"] != DBNull.Value
                            ? Convert.ToDecimal(rowProd["cantidad_total"]) : 0;

                        string medida = rowProd["medida"].ToString();

                        decimal cantFraccionada = rowProd["cantidad_fraccionada_total"] != DBNull.Value
                                                ? Convert.ToDecimal(rowProd["cantidad_fraccionada_total"]) : 0;
                        string medidaFraccionada = rowProd["medida_unidadcontenedorfraccionada"].ToString();

                        int traspaso = 0;

                        if (cantidadTotal > 0)
                        {
                            bool result = negocio.POST_RegistroAlmacenMovil(
                                codDespacho, codRuta, codChofer, codVehiculo,
                                codProducto, producto,
                                cantidadTotal, medida,
                                0, "", traspaso
                            );
                            if (!result)
                            {
                                showalert($"Error al insertar el producto : {producto}");
                            }
                        }

                        if (cantFraccionada > 0)
                        {
                            bool result = negocio.POST_RegistroAlmacenMovil(
                                codDespacho, codRuta, codChofer, codVehiculo,
                                codProducto, producto,
                                0, "",
                                cantFraccionada, medidaFraccionada, traspaso
                            );
                            if (!result)
                            {
                                showalert($"Error al insertar el producto : {producto}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Registro_RutaPuntosDEntrega_Despacho(int codDespacho, string vehiculo)
        {
            try
            {
                NCorpal_EntregaSolicitudProducto2 nEntrega = new NCorpal_EntregaSolicitudProducto2();

                DataSet dsCli = nEntrega.GET_obtenerDatosClienteDespacho(codDespacho);

                if (dsCli.Tables[0].Rows.Count == 0)
                    throw new Exception("No se encontraron datos del cliente.");

                DataSet ds = nEntrega.get_DespachoProductoaCamion(codDespacho);

                if (ds.Tables[0].Rows.Count == 0)
                    throw new Exception("No se encontraron datos del despacho");

                DataRow row = ds.Tables[0].Rows[0];

                int codVehiculo = Convert.ToInt32(row["codVehiculo"]);

                int codConductor = Convert.ToInt32(row["codConductor"]);
                string conductor = row["Conductor"].ToString();

                int idRuta = nEntrega.post_RegistroRutaEntrega_despacho(codVehiculo, vehiculo, codConductor, conductor);

                if (idRuta <= 0)
                {
                    showalert("No se pudo registrar la ruta");
                    return;
                }

                int nOrden = 1;
                foreach (DataRow rowCli in dsCli.Tables[0].Rows)
                {
                    int codCli = Convert.ToInt32(rowCli["codCli"]);
                    string cliente = rowCli["tiendaname"].ToString();
                    string cliLat = (rowCli["direccion_lat"].ToString());
                    string cliLng = (rowCli["direccion_lng"].ToString());
                    string descripcion = "";

                    bool resultDet = nEntrega.post_RegistroRutaEntregaPuntos_despacho(
                                    nOrden, idRuta, codCli, cliente,
                                    codDespacho, descripcion, cliLat, cliLng);

                    if (!resultDet)
                    {
                        showalert("Error al registrar el punto del cliente: " + cliente);
                        return;
                    }
                    nOrden++;
                }

                showalert("ruta y puntos de entrega registrados correctamente.");
            }
            catch (Exception ex)
            {
                showalert("Error en el metodo de registro ruta y puntos. " + ex.Message);
            }
        }

        protected void bt_verRecibo_Click(object sender, EventArgs e)
        {
            if (gv_despachos.SelectedIndex >= 0)
            {
                int codigo = Convert.ToInt32(gv_despachos.DataKeys[gv_despachos.SelectedIndex]["codigo"]);
                Session["codigoDespacho"] = codigo;
                Session["ReporteGeneral"] = "Reporte_DespachoProductoCamionEntrega";
                Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");
            }
            else
            {
                showalert("Seleccione un registro de la tabla para ver el recibo.");
            }
        }

        protected void dd_listVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fechadesde = convertidorFecha(tx_fechaDesdeDespacho.Text);
            string fechahasta = convertidorFecha(tx_fechaHastaDespacho.Text);
            string estado = dd_estadoCierre.SelectedItem.Text;
            int codVehiculo = int.Parse(dd_listVehiculo.SelectedValue);
            mostrarRegistrosDespachoProductos(fechadesde, fechahasta, estado, codVehiculo);
        }

        protected void gv_solicitudesProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionDatos();
        }

        private void seleccionDatos()
        {
            throw new NotImplementedException();
        }

        private void showalert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }
    }
}