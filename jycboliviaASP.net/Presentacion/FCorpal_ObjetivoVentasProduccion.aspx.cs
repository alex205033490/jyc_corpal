using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Configuration;
using System.Data;
using System.IO;
using static jycboliviaASP.net.Presentacion.FCorpal_APIProduccion;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ObjetivoProduccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(127) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {
                // 1. Cargar el DropDownList para cuando toque Editar
                llenarProductosNax();

                // 2. Cargar el GridView Masivo (Panel por defecto)
                cargarGridMasivo();

                // 3. Cargar la tabla principal (Siempre visible abajo)
                buscarDatos("", "");

                // 4. Asegurar que inicia en la vista correcta
                MostrarPanelCargaMasiva();
            }
        }

        // ========================================================================================
        // CONTROL DE PANELES (VISTAS)
        // ========================================================================================
        private void MostrarPanelCargaMasiva()
        {
            pn_CargaMasiva.Visible = true;
            pn_Edicion.Visible = false;
        }

        private void MostrarPanelEdicion()
        {
            pn_CargaMasiva.Visible = false;
            pn_Edicion.Visible = true;
        }

        // Nuevo Botón para salir de la edición sin guardar
        protected void bt_cancelarEdicion_Click(object sender, EventArgs e)
        {
            limpiarDatos();
            // Deseleccionar la fila de la grilla principal
            gv_objetivoProduccion.SelectedIndex = -1;
            MostrarPanelCargaMasiva();
        }

        // ========================================================================================
        // CARGA MASIVA DE DATOS
        // ========================================================================================
        private void cargarGridMasivo()
        {
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos_quesoloestenvigente2("");

            gv_cargaMasiva.DataSource = tuplas;
            gv_cargaMasiva.DataBind();
        }

        protected void bt_insertarMasivo_Click(object sender, EventArgs e)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            string respgra = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();

            NCorpal_Produccion cp = new NCorpal_Produccion();
            int insertados = 0;
            string CuerpoCorreo = "Se han incorporado los siguientes objetivos de Ventas (Carga Masiva):<br><br>";

            foreach (GridViewRow row in gv_cargaMasiva.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");

                    if (chk != null && chk.Checked)
                    {
                        TextBox txtFecha = (TextBox)row.FindControl("tx_fechaGrid");
                        TextBox txtCant = (TextBox)row.FindControl("tx_cantGrid");
                        TextBox txtDetalle = (TextBox)row.FindControl("tx_detalleGrid");

                        if (string.IsNullOrEmpty(txtFecha.Text) || string.IsNullOrEmpty(txtCant.Text) || txtCant.Text == "0")
                        {
                            continue;
                        }

                        int codprod = Convert.ToInt32(gv_cargaMasiva.DataKeys[row.RowIndex].Value);
                        string producto = HttpUtility.HtmlDecode(row.Cells[2].Text);
                        string medida = HttpUtility.HtmlDecode(row.Cells[5].Text);

                        string fechalimite = convertirFecha(txtFecha.Text);
                        float cantidadprod;
                        float.TryParse(txtCant.Text.Replace('.', ','), out cantidadprod);
                        string detalle = txtDetalle.Text;

                        bool bandera = cp.set_objetivoProduccion(fechalimite, codprod, producto, cantidadprod, medida, detalle, codUser, respgra);

                        if (bandera)
                        {
                            insertados++;
                            CuerpoCorreo += $"<li><b>{producto}</b> - Cant: {cantidadprod} {medida} - Límite: {txtFecha.Text} - Det: {detalle}</li>";

                            chk.Checked = false;
                            txtCant.Text = "0";
                            txtDetalle.Text = "";
                            txtFecha.Text = "";
                        }
                    }
                }
            }

            if (insertados > 0)
            {
                string asunto = "(Corpal) Carga Masiva - Objetivos de Ventas por " + respgra;
                CuerpoCorreo += "<br><br>Responsable: " + respgra;

                NA_EnvioCorreo nenvio = new NA_EnvioCorreo();
                string baseDatos = Session["BaseDatos"].ToString();
                nenvio.Enviar_Correo_objetivosVentas(asunto, CuerpoCorreo);

                buscarDatos("", "");
                Response.Write($"<script type='text/javascript'> alert('Guardado Masivo OK: {insertados} registros ingresados.') </script>");
            }
            else
            {
                Response.Write("<script type='text/javascript'> alert('Atención: No se insertó nada. Verifique haber marcado los Checkbox y llenado las fechas/cantidades.') </script>");
            }
        }

        // ========================================================================================
        // MÉTODOS GENERALES (FECHA, PERMISOS, LLENAR DROPDOWN)
        // ========================================================================================
        private void llenarProductosNax()
        {
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos_quesoloestenvigente2("");

            dd_productosNax.DataSource = tuplas;
            dd_productosNax.DataValueField = "codigo";
            dd_productosNax.DataTextField = "producto";
            dd_productosNax.AppendDataBoundItems = true;
            dd_productosNax.SelectedIndex = 1;
            dd_productosNax.DataBind();

            string medida = tuplas.Tables[0].Rows[0][2].ToString();
            tx_medida.Text = medida;
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

        public string convertirFecha(string fecha)
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

        // ========================================================================================
        // TABLA INFERIOR (BÚSQUEDA Y SELECCIÓN PARA EDICIÓN)
        // ========================================================================================
        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string fechalimite = convertirFecha(tx_fechalimite.Text);
            string producto = dd_productosNax.SelectedItem.Text;
            buscarDatos(fechalimite, producto);
        }

        private void buscarDatos(string fechalimite, string producto)
        {
            NCorpal_Produccion np = new NCorpal_Produccion();
            DataSet datos = np.get_objetivosDeProduccion(fechalimite, producto);
            gv_objetivoProduccion.DataSource = datos;
            gv_objetivoProduccion.DataBind();
        }

        protected void gv_reciboIngresoEgreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecciondeDatosparaModificar();
            // Mostrar panel de edición tras seleccionar de la tabla
            MostrarPanelEdicion();
        }

        private void selecciondeDatosparaModificar()
        {
            tx_fechalimite.Text = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[2].Text);
            int codProducto;
            int.TryParse(gv_objetivoProduccion.SelectedRow.Cells[3].Text, out codProducto);

            if (codProducto >= 0)
            {
                dd_productosNax.SelectedValue = codProducto.ToString();
            }
            tx_cantcajas.Text = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[5].Text);
            tx_medida.Text = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[8].Text);
            tx_detalle.Text = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[9].Text);
        }

        protected void dd_productosNax_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatosSeleccionado();
        }

        private void cargarDatosSeleccionado()
        {
            int codigoProducto;
            int.TryParse(dd_productosNax.SelectedValue, out codigoProducto);

            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_producto(codigoProducto);
            string medida = tuplas.Tables[0].Rows[0][2].ToString();
            tx_medida.Text = medida;
        }

        // ========================================================================================
        // MODIFICACIÓN, ELIMINACIÓN Y LIMPIEZA
        // ========================================================================================
        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_cantcajas.Text = "0";
            tx_detalle.Text = "";
            tx_fechalimite.Text = "0";
            // No reseteamos la medida ni el producto aquí ya que bloqueamos el combo en HTML
        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            actualizarDatosSistema();
        }

        private void actualizarDatosSistema()
        {
            if (gv_objetivoProduccion.SelectedIndex >= 0)
            {
                int codigo;
                int.TryParse(gv_objetivoProduccion.SelectedRow.Cells[1].Text, out codigo);

                string fechalimite = convertirFecha(tx_fechalimite.Text);

                int codprod;
                int.TryParse(dd_productosNax.SelectedValue, out codprod);
                NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
                DataSet tuplas = pp.get_producto(codprod);
                string producto = tuplas.Tables[0].Rows[0][1].ToString();

                float cantidadprod;
                float.TryParse(tx_cantcajas.Text.Replace('.', ','), out cantidadprod);
                string medida = tx_medida.Text;
                string detalle = tx_detalle.Text;

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                int codusergra = codUser;
                string respgra = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();

                NCorpal_Produccion cp = new NCorpal_Produccion();
                bool bandera = cp.update_objetivoProduccion(codigo, fechalimite, codprod, producto, cantidadprod, medida, detalle, codusergra, respgra);
                if (bandera)
                {
                    string asunto = "(Corpal) Modificado-Objetivo de Ventas por " + respgra;
                    string Cuerpo = "Se ha Modificado el objetivo de Ventas <br><br>" +
                        "Codigo = " + codigo + "; <br>" +
                        "Producto = " + producto + "; <br>" +
                        "Medida = " + medida + "; <br>" +
                        "Fecha Limite = " + tx_fechalimite.Text + "; <br>" +
                        "Detalle = " + detalle + "; <br>" +
                        "Responsable = " + respgra;
                    NA_EnvioCorreo nenvio = new NA_EnvioCorreo();
                    string baseDatos = Session["BaseDatos"].ToString();
                    bool correoOK = nenvio.Enviar_Correo_objetivosVentas(asunto, Cuerpo);

                    buscarDatos("", "");
                    limpiarDatos();
                    gv_objetivoProduccion.SelectedIndex = -1; // Deseleccionamos
                    MostrarPanelCargaMasiva(); // Volvemos a la vista original

                    Response.Write("<script type='text/javascript'> alert('Modificación exitosa') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('ERROR: No se pudo modificar el registro') </script>");
            }
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarDatos();
        }

        private void eliminarDatos()
        {
            if (gv_objetivoProduccion.SelectedIndex >= 0)
            {
                int codigo;
                int.TryParse(gv_objetivoProduccion.SelectedRow.Cells[1].Text, out codigo);

                string fechalimite = convertirFecha(tx_fechalimite.Text);

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                NCorpal_Produccion cp = new NCorpal_Produccion();
                bool bandera = cp.delete_objetivoProduccion(codigo, codUser);
                if (bandera)
                {
                    buscarDatos("", "");
                    limpiarDatos();
                    gv_objetivoProduccion.SelectedIndex = -1; // Deseleccionamos
                    MostrarPanelCargaMasiva(); // Volvemos a la vista original

                    Response.Write("<script type='text/javascript'> alert('Registro eliminado') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('ERROR: No se pudo eliminar el registro') </script>");
            }
        }

        // ========================================================================================
        // DESCARGA DE EXCEL
        // ========================================================================================
        protected void bt_excel_Click(object sender, EventArgs e)
        {
            descargarExcel();
        }

        private void descargarExcel()
        {
            string fechalimite = convertirFecha(tx_fechalimite.Text);
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();

            int codigoProducto;
            int.TryParse(dd_productosNax.SelectedValue, out codigoProducto);

            DataSet tuplas = pp.get_producto(codigoProducto);
            string producto = tuplas.Tables[0].Rows[0][1].ToString();

            NCorpal_Produccion np = new NCorpal_Produccion();
            if (!(gv_objetivoProduccion.SelectedIndex >= 0))
            {
                fechalimite = "";
                producto = "";
            }

            DataSet datos = np.get_objetivosDeProduccion(fechalimite, producto);

            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Objetivos de Produccion - " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    DataGrid dg = new DataGrid();
                    dg.DataSource = datos;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }
    }
}