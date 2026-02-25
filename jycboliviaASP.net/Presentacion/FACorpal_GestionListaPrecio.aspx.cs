using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Datos;
using jycboliviaASP.net.Datos.Datos_old;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FACorpal_GestionListaPrecio : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)

        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(151) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {
                buscarListaProducto("");
                panelAgregarProducto.Visible = false;
            }

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



        // --- EVENTOS DE LA SECCIÓN 1: BUSCADOR Y NUEVA LISTA ---

        protected void btnBuscarLista_Click(object sender, EventArgs e)
        {
            // 1. Buscamos y recargamos la grilla superior
            buscarListaProducto(txtBuscarLista.Text.Trim());

            // 2. Ocultamos el panel inferior por si había una lista seleccionada antes
            panelProductos.Visible = false;
            panelAgregarProducto.Visible = false;
        }

        private void buscarListaProducto(string nombreLista)
        {
            NCorpal_Cliente Nproy = new NCorpal_Cliente();
            DataSet lista = Nproy.listarListaProducto(nombreLista);
            gvListasPrecio.DataSource = lista;
            gvListasPrecio.DataBind();
        }

        private void CargarProductosDeLista(int idLista)
        {
            NCorpal_Cliente Nproy = new NCorpal_Cliente();
            DataSet lista = Nproy.listarDetalleListaProducto(idLista);
            gvProductosLista.DataSource = lista;
            gvProductosLista.DataBind();
        }



        protected void btnNuevaLista_Click(object sender, EventArgs e)
        {
            // 1. Limpiamos los campos por si tenían datos viejos
            txtNombreLista.Text = "";
            txtDescripcionLista.Text = "";
            txtDescuentoGral.Text = "0.00";

            // 2. Mostramos el panel del formulario
            panelFormularioLista.Visible = true;

            // 3. Ocultamos las grillas para que la pantalla quede limpia
            txtBuscarLista.Visible = false;
            btnBuscarLista.Visible = false;
            btnNuevaLista.Visible = false;
            gvListasPrecio.Visible = false; // Oculta la grilla principal

            panelProductos.Visible = false; // Oculta la sección de abaj

        }
        protected void btnGuardarLista_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreLista.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('El nombre de la lista es obligatorio.');", true);
                return;
            }

            string nombre = txtNombreLista.Text.Trim();
            string descripcion = txtDescripcionLista.Text.Trim();

            decimal descuentoGral = 0;
            if (!string.IsNullOrWhiteSpace(txtDescuentoGral.Text))
            {
                // 1. Reemplazamos cualquier coma por un punto para unificar
                string valorNormalizado = txtDescuentoGral.Text.Replace(",", ".");

                // 2. Convertimos usando InvariantCulture para que C# respete el punto como decimal
                descuentoGral = Convert.ToDecimal(valorNormalizado, System.Globalization.CultureInfo.InvariantCulture);
            }

            // Instanciamos la capa de negocio
            NCorpal_Cliente Nproy = new NCorpal_Cliente();

            // =========================================================
            // NUEVA VALIDACIÓN: Verificar si el nombre ya existe
            // =========================================================
            if (Nproy.existeNombreLista(nombre))
            {
                // Si entra aquí, el nombre ya está ocupado. Mostramos alerta y detenemos el proceso.
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Atención: Ya existe una Lista de Precios con el nombre \"" + nombre + "\". Por favor, elija un nombre diferente.');", true);
                return;
            }

            // =========================================================
            // AQUÍ VA TU LÓGICA DE NEGOCIO PARA GUARDAR EN BASE DE DATOS
            // =========================================================
            bool exito = Nproy.insertarListaPrecio(nombre, descripcion, descuentoGral);

            // =========================================================

            if (exito)
            {
                // 1. Una vez guardado, ocultamos el formulario
                panelFormularioLista.Visible = false;

                // 2. Volvemos a hacer visibles los controles de búsqueda y la grilla
                txtBuscarLista.Visible = true;
                btnBuscarLista.Visible = true;
                btnNuevaLista.Visible = true;
                gvListasPrecio.Visible = true;

                // 3. Recargamos la grilla para ver la nueva lista en la pantalla
                buscarListaProducto("");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaError", "alert('Ocurrió un error al intentar guardar la lista. Por favor, intente nuevamente.');", true);
            }
        }


        protected void btnCancelarLista_Click(object sender, EventArgs e)
        {
            // 1. Limpiamos las cajitas de texto por si el usuario había escrito algo
            txtNombreLista.Text = "";
            txtDescripcionLista.Text = "";
            txtDescuentoGral.Text = ""; // O "0.00" dependiendo de cómo lo inicialices

            // 2. Ocultamos el panel del formulario
            panelFormularioLista.Visible = false;

            // 3. Volvemos a mostrar la grilla y los controles de búsqueda
            txtBuscarLista.Visible = true;
            btnBuscarLista.Visible = true;
            btnNuevaLista.Visible = true;
            gvListasPrecio.Visible = true;
        }

        // --- EVENTOS DE LA GRILLA MAESTRA (gvListasPrecio) ---

        protected void gvListasPrecio_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Obtenemos el ID de la lista seleccionada
            int idListaSeleccionada = Convert.ToInt32(gvListasPrecio.SelectedDataKey.Value);
            ViewState["IdListaSeleccionada"] = idListaSeleccionada;

            // 2. Extraemos la fila que el usuario acaba de seleccionar
            GridViewRow filaSeleccionada = gvListasPrecio.SelectedRow;

            // Extraemos el nombre para el título
            LinkButton lnkNombre = (LinkButton)filaSeleccionada.FindControl("lnkNombreLista");
            lblNombreListaSeleccionada.Text = lnkNombre != null ? lnkNombre.Text : "Lista Seleccionada";

            // ¡CORRECCIÓN CRÍTICA! Como cambiamos a TemplateField, buscamos el Label directamente
            Label lblDcto = (Label)filaSeleccionada.FindControl("lblDctoGral");
            string descuentoLista = lblDcto != null ? lblDcto.Text.Trim() : "0";
            ViewState["DescuentoListaSeleccionada"] = descuentoLista;

            // =========================================================
            // 3. NUEVA LÓGICA: CERRAR MODO EDICIÓN SI ESTABA ABIERTO
            // =========================================================
            if (gvListasPrecio.EditIndex != -1)
            {
                // Si había alguna fila con las cajitas abiertas, la cancelamos
                gvListasPrecio.EditIndex = -1;

                // Recargamos la grilla para que las cajitas desaparezcan visualmente
                buscarListaProducto("");
            }

            // 4. Cargamos los productos de esa lista en la grilla inferior
            CargarProductosDeLista(idListaSeleccionada);

            // 5. Hacemos visible el panel de abajo y ocultamos el de agregar
            panelProductos.Visible = true;
            panelAgregarProducto.Visible = false;
            btnAbrirAgregar.Visible = true;
        }




        protected void gvListasPrecio_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idLista = Convert.ToInt32(gvListasPrecio.DataKeys[e.RowIndex].Value);
            eliminarListaProducto(idLista);
        }

        private void eliminarListaProducto(int idLista)
        {

            // 2. Instanciamos la capa de negocio
            NCorpal_Cliente Nproy = new NCorpal_Cliente();

            // 3. Ejecutamos el método de eliminación (que crearemos en el Paso 2)
            bool exito = Nproy.eliminarListaPrecio(idLista);

            if (exito)
            {
                // 4. Mensaje de éxito
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaExito", "alert('La lista se eliminó correctamente.');", true);

                // 5. Recargamos la grilla para que la lista desaparezca de la pantalla
                buscarListaProducto(txtBuscarLista.Text.Trim());

                // 6. Ocultamos el panel de productos de abajo por seguridad 
                // (evita que el usuario siga viendo productos de una lista que acaba de borrar)
                panelProductos.Visible = false;
            }
            else
            {
                // Mensaje de error
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaError", "alert('Ocurrió un error al intentar eliminar la lista.');", true);
            }

        }

        // --- EVENTOS DE LA GRILLA DETALLE (gvProductosLista) ---

        // --- EVENTOS DEL PANEL AGREGAR PRODUCTO ---

        protected void btnAbrirAgregar_Click(object sender, EventArgs e)
        {
            // ==========================================================
            // 0. NUEVO: FORZAMOS A CERRAR CUALQUIER EDICIÓN EN LA GRILLA
            // ==========================================================
            if (gvProductosLista.EditIndex != -1)
            {
                gvProductosLista.EditIndex = -1;

                // Recargamos la grilla para que desaparezcan las cajitas de texto visualmente
                if (ViewState["IdListaSeleccionada"] != null)
                {
                    int idLista = Convert.ToInt32(ViewState["IdListaSeleccionada"]);
                    CargarProductosDeLista(idLista);
                }
            }

            // 1. LIMPIEZA TOTAL: Vaciamos el buscador principal
            txtBuscarProducto.Text = "";

            // 2. Restauramos los precios y la unidad a cero o vacío
            txtPrecioAgregar.Text = "0.00";
            txtUnidadAgregar.Text = "";
            txtPrecioEspecialAgregar.Text = "0.00";
            txtAumentoAgregar.Text = "0.00";

            // 3. Restauramos los campos ocultos a sus valores lógicos por defecto
            txtCantidadDesdeAgregar.Text = "1.00";
            txtCantidadMinimaAgregar.Text = "1";

            // 4. Recuperamos el % de descuento original de la lista seleccionada
            string dctoMemoria = ViewState["DescuentoListaSeleccionada"] != null ? ViewState["DescuentoListaSeleccionada"].ToString().Trim() : "";
            txtDctoAgregar.Text = string.IsNullOrEmpty(dctoMemoria) ? "0.00" : dctoMemoria.Replace(",", ".");

            // 5. Recargamos la lista de sugerencias (Datalist) por si se agregó un producto nuevo a la BD
            CargarComboProductos();

            // 6. Finalmente, mostramos el panel nuevecito y ocultamos este botón
            panelAgregarProducto.Visible = true;
            btnAbrirAgregar.Visible = false;
        }

        private void CargarComboProductos()
        {
            // 1. Instanciamos la capa de negocio (cambia "NCorpal_Producto" por el nombre de tu clase si es diferente)
            NCorpal_Cliente Nproy = new NCorpal_Cliente();
            DataSet dsProductos = Nproy.listarProductosActivos();
            if (dsProductos != null && dsProductos.Tables.Count > 0)
            {
                // Llenamos el Repeater oculto que alimenta a la caja de texto
                rptProductos.DataSource = dsProductos.Tables[0];
                rptProductos.DataBind();
            }
        }

        protected void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            string textoSeleccionado = txtBuscarProducto.Text;

            // Verificamos que hayan elegido una opción válida (que contenga el guion "-")
            if (!string.IsNullOrEmpty(textoSeleccionado) && textoSeleccionado.Contains("-"))
            {
                // Cortamos el texto para sacar el ID
                string[] partes = textoSeleccionado.Split('-');
                int idProducto = 0;

                if (int.TryParse(partes[0].Trim(), out idProducto))
                {
                    // Vamos a la base de datos
                    NCorpal_Cliente Nprod = new NCorpal_Cliente();
                    DataSet dsProd = Nprod.obtenerDatosProducto(idProducto);

                    if (dsProd != null && dsProd.Tables.Count > 0 && dsProd.Tables[0].Rows.Count > 0)
                    {
                        DataRow fila = dsProd.Tables[0].Rows[0];

                        // 1. Extraemos y asignamos precio base y unidad
                        string precioBD = fila["precio"].ToString();
                        string unidadBD = fila["medida"].ToString();

                        txtPrecioAgregar.Text = string.IsNullOrEmpty(precioBD) ? "0.00" : precioBD.Replace(",", ".");
                        txtUnidadAgregar.Text = unidadBD;

                        // =========================================================
                        // 2. MAGIA DE CÁLCULO EN C# (PRECIO ESPECIAL)
                        // =========================================================
                        // Convertimos el precio base recién traído a decimal
                        string precioTexto = txtPrecioAgregar.Text.Replace(".", ",");
                        decimal precioBase = string.IsNullOrEmpty(precioTexto) ? 0 : Convert.ToDecimal(precioTexto);

                        // Leemos lo que haya en la caja de Descuento
                        string dctoTexto = txtDctoAgregar.Text.Replace(".", ",");
                        decimal descuento = string.IsNullOrEmpty(dctoTexto) ? 0 : Convert.ToDecimal(dctoTexto);

                        // Leemos lo que haya en la caja de Aumento
                        string aumentoTexto = txtAumentoAgregar.Text.Replace(".", ",");
                        decimal aumento = string.IsNullOrEmpty(aumentoTexto) ? 0 : Convert.ToDecimal(aumentoTexto);

                        // Hacemos la matemática
                        decimal montoDescuento = precioBase * (descuento / 100m);
                        decimal montoAumento = precioBase * (aumento / 100m);
                        decimal precioFinal = precioBase - montoDescuento + montoAumento;

                        // 3. Asignamos el resultado a la caja de Precio Especial
                        // Usamos InvariantCulture para garantizar que se escriba con punto decimal "150.50"
                        txtPrecioEspecialAgregar.Text = precioFinal.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        // Limpiamos si no existe en BD
                        txtPrecioAgregar.Text = "0.00";
                        txtUnidadAgregar.Text = "";
                        txtPrecioEspecialAgregar.Text = "0.00";
                    }
                }
            }
            else
            {
                // Limpiamos si borraron la caja de búsqueda
                txtPrecioAgregar.Text = "0.00";
                txtUnidadAgregar.Text = "";
                txtPrecioEspecialAgregar.Text = "0.00";
            }
        }



        protected void btnCancelarProducto_Click(object sender, EventArgs e)
        {
            // 1. Ocultamos el panel verde
            panelAgregarProducto.Visible = false;

            // 2. Volvemos a mostrar el botón de agregar
            btnAbrirAgregar.Visible = true;
        }

        // --- EVENTOS DE LOS BOTONES DEL PANEL DE PRODUCTOS ---



        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            guardarDetalleListaProducto();
        }




        protected void guardarDetalleListaProducto()
        {
            // 1. Validar que tengamos una lista seleccionada en memoria
            if (ViewState["IdListaSeleccionada"] == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Error: No hay una lista de precios seleccionada.');", true);
                return;
            }

            // 2. Validar que hayan escrito/seleccionado un producto válido de la lista (que tenga el guion "-")
            string textoSeleccionado = txtBuscarProducto.Text;
            if (string.IsNullOrEmpty(textoSeleccionado) || !textoSeleccionado.Contains("-"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Debe seleccionar un producto de las sugerencias.');", true);
                return;
            }

            try
            {
                // 3. Extraemos los ID principales
                int idLista = Convert.ToInt32(ViewState["IdListaSeleccionada"]);

                // Extraemos el ID del producto (Ej: "45 - Tubo PVC" -> nos quedamos solo con "45")
                string[] partes = textoSeleccionado.Split('-');
                int idProducto = Convert.ToInt32(partes[0].Trim());


                NCorpal_Cliente Nproy = new NCorpal_Cliente();

                // Solo le pasamos idLista e idProducto (asume codigoDetalle = 0)
                if (Nproy.existeProductoEnLista(idLista, idProducto))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Atención: Este producto ya se encuentra agregado a esta Lista de Precios.');", true);
                    return; // Detenemos la ejecución, no guardamos el duplicado
                }

                // =====================================================================
                // 4. RECOLECTAMOS LOS VALORES BASE
                // =====================================================================
                string precioTexto = txtPrecioAgregar.Text.Replace(".", ",");
                decimal precioBase = string.IsNullOrEmpty(precioTexto) ? 0 : Convert.ToDecimal(precioTexto);

                string dctoTexto = txtDctoAgregar.Text.Replace(".", ",");
                decimal descuento = string.IsNullOrEmpty(dctoTexto) ? 0 : Convert.ToDecimal(dctoTexto);

                string aumentoTexto = txtAumentoAgregar.Text.Replace(".", ",");
                decimal aumento = string.IsNullOrEmpty(aumentoTexto) ? 0 : Convert.ToDecimal(aumentoTexto);

                string unidad = txtUnidadAgregar.Text.Trim();

                // Campos ocultos
                string cantDesdeTexto = txtCantidadDesdeAgregar.Text.Replace(".", ",");
                decimal cantidadDesde = string.IsNullOrEmpty(cantDesdeTexto) ? 1 : Convert.ToDecimal(cantDesdeTexto);

                int cantidadMinima = string.IsNullOrEmpty(txtCantidadMinimaAgregar.Text) ? 1 : Convert.ToInt32(txtCantidadMinimaAgregar.Text);

                // =====================================================================
                // LÓGICA DE PRECIO: CÁLCULO DEL PRECIO ESPECIAL (FINAL)
                // =====================================================================
                // La "m" al final del 100 le dice a C# que es un número decimal exacto
                decimal montoDescuento = precioBase * (descuento / 100m);
                decimal montoAumento = precioBase * (aumento / 100m);

                // Calculamos el Precio Especial
                decimal precioFinal = precioBase - montoDescuento + montoAumento;


                // =====================================================================
                // 5. LLAMADA A LA CAPA DE NEGOCIO PARA GUARDAR
                // =====================================================================
                NCorpal_Cliente Nlista = new NCorpal_Cliente();

  
                // y mandamos 'precioBase' directo a la columna de precio normal.
                bool exito = Nlista.insertarDetalleLista(idLista, idProducto, 0, descuento, precioFinal, unidad, cantidadDesde, cantidadMinima, aumento);


                // 6. Finalizamos
                if (exito)
                {
                    // Recargamos la grilla para que se vea el nuevo producto
                    CargarProductosDeLista(idLista);

                    // Ocultamos el panel, limpiamos el buscador y mostramos el botón "+ Agregar"
                    panelAgregarProducto.Visible = false;
                    btnAbrirAgregar.Visible = true;
                    txtBuscarProducto.Text = "";

                    ScriptManager.RegisterStartupScript(this, GetType(), "exito", "alert('Producto agregado correctamente a la lista.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Ocurrió un error al guardar en la base de datos.');", true);
                }
            }
            catch (Exception ex)
            {
                // Nota: Usamos la "ex" vacía para que no tire warning en el compilador
                ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Error en el formato de los números. Verifique los datos ingresados. " + ex.Message.Replace("'", "") + "');", true);
            }
        }

        // 1. CUANDO SE PRESIONA EL BOTÓN "EDITAR"


        // 2. CUANDO SE PRESIONA EL BOTÓN "CANCELAR"





        // 1. BOTÓN "EDITAR"
        protected void gvListasPrecio_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Entramos en modo edición en la fila seleccionada
            gvListasPrecio.EditIndex = e.NewEditIndex;

            // Recargamos usando TU método exacto
            buscarListaProducto("");
        }

        // 2. BOTÓN "CANCELAR"
        protected void gvListasPrecio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Salimos del modo edición (EditIndex = -1)
            gvListasPrecio.EditIndex = -1;

            // Recargamos usando TU método exacto para volver a la normalidad
            buscarListaProducto("");
        }

        // 3. BOTÓN "GUARDAR" (NUEVO)

        protected void gvListasPrecio_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                // 1. Obtenemos el ID de la lista que se está editando
                int codigoLista = Convert.ToInt32(gvListasPrecio.DataKeys[e.RowIndex].Value);

                // 2. Agarramos la fila de la grilla y extraemos las cajitas
                GridViewRow fila = gvListasPrecio.Rows[e.RowIndex];
                TextBox txtNombre = (TextBox)fila.FindControl("txtEditNombre");
                TextBox txtDescripcion = (TextBox)fila.FindControl("txtEditDescripcion");
                TextBox txtDcto = (TextBox)fila.FindControl("txtEditDctoGral");

                // 3. Limpiamos los valores
                string nuevoNombre = txtNombre.Text.Trim();
                string nuevaDescripcion = txtDescripcion.Text.Trim();

                // Convertimos el descuento a decimal de forma segura
                string dctoTexto = txtDcto.Text.Replace(",", ".");
                decimal nuevoDcto = string.IsNullOrEmpty(dctoTexto) ? 0 : Convert.ToDecimal(dctoTexto, System.Globalization.CultureInfo.InvariantCulture);

                NCorpal_Cliente Nproy = new NCorpal_Cliente();

                // ==========================================================
                // 4. VALIDACIÓN: Nombre repetido (Ignorando la lista actual)
                // ==========================================================
                // Le pasamos el 'codigoLista' como segundo parámetro
                if (Nproy.existeNombreLista(nuevoNombre, codigoLista))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Atención: Ya existe OTRA Lista de Precios con el nombre \"" + nuevoNombre + "\". Elija uno distinto.');", true);
                    return; // Detenemos el guardado
                }

                // ==========================================================
                // 5. GUARDAR Y ACTUALIZAR EN CASCADA
                // ==========================================================
                bool exito = Nproy.actualizarListaPrecio(codigoLista, nuevoNombre, nuevaDescripcion, nuevoDcto);

                if (exito)
                {
                    // Cerramos el modo edición
                    gvListasPrecio.EditIndex = -1;

                    // Recargamos la grilla
                    buscarListaProducto("");

                    panelProductos.Visible = false;      // Oculta la tablita de abajo
                    gvListasPrecio.SelectedIndex = -1;

                    ScriptManager.RegisterStartupScript(this, GetType(), "exito", "alert('Lista modificada y precios actualizados correctamente.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Ocurrió un error al actualizar la base de datos.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Error en el formato de los datos ingresados.');", true);
            }
        }








        // --- EVENTO PARA QUITAR UN PRODUCTO DE LA LISTA ---

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            // 1. Identificamos qué botón exactamente se presionó en la grilla
            LinkButton btn = (LinkButton)sender;

            // 2. Extraemos el ID del producto (el "codigo") y se lo pasamos a tu método
            int codigoDetalle = Convert.ToInt32(btn.CommandArgument);

            eliminarProductoDetalleLista(codigoDetalle);
        }


        // 1. Botón Editar Producto
        protected void gvProductosLista_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Ponemos la fila en modo edición
            gvProductosLista.EditIndex = e.NewEditIndex;

            // Recargamos la grilla leyendo el ID de la lista de la memoria
            if (ViewState["IdListaSeleccionada"] != null)
            {
                int idLista = Convert.ToInt32(ViewState["IdListaSeleccionada"]);
                CargarProductosDeLista(idLista);
            }
        }

        // 2. Botón Cancelar Edición
        protected void gvProductosLista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Apagamos el modo edición
            gvProductosLista.EditIndex = -1;

            // Recargamos la grilla
            if (ViewState["IdListaSeleccionada"] != null)
            {
                int idLista = Convert.ToInt32(ViewState["IdListaSeleccionada"]);
                CargarProductosDeLista(idLista);
            }
        }

        // 3. Botón Guardar Producto (El disquete/check)
        protected void gvProductosLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                // 1. Obtenemos el ID del detalle (el DataKeyNames="codigo")
                int codigoDetalle = Convert.ToInt32(gvProductosLista.DataKeys[e.RowIndex].Value);

                // 2. Encontramos la fila y extraemos las cajitas y el Label del precio base
                GridViewRow fila = gvProductosLista.Rows[e.RowIndex];
                TextBox txtDcto = (TextBox)fila.FindControl("txtEditDctoProd");
                TextBox txtAumento = (TextBox)fila.FindControl("txtEditAumentoProd");
                Label lblPrecioBase = (Label)fila.FindControl("lblEditPrecioBase");

                // ==========================================================
                // 3. VALIDACIÓN 1: Que no estén vacíos o llenos de espacios
                // ==========================================================
                if (string.IsNullOrWhiteSpace(txtDcto.Text) || string.IsNullOrWhiteSpace(txtAumento.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Los campos de Descuento y Aumento no pueden estar en blanco.');", true);
                    return; // Detenemos todo
                }

                // ==========================================================
                // 4. VALIDACIÓN 2: Que sean solo números (No letras)
                // ==========================================================
                decimal dcto = 0;
                decimal aumento = 0;
                decimal precioBase = 0;

                // TryParse intenta convertir. Si hay letras, devuelve false.
                bool esDctoValido = decimal.TryParse(txtDcto.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out dcto);
                bool esAumentoValido = decimal.TryParse(txtAumento.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out aumento);

                // El precio base siempre es válido porque viene de la BD, pero igual lo convertimos seguro
                decimal.TryParse(lblPrecioBase.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out precioBase);

                if (!esDctoValido || !esAumentoValido)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Por favor ingrese valores numéricos válidos. No se permiten letras ni símbolos raros.');", true);
                    return; // Detenemos todo
                }

                // ==========================================================
                // 5. CÁLCULO MATEMÁTICO EN C# (Para mandarlo a la BD)
                // ==========================================================
                decimal montoAumento = precioBase * (aumento / 100);
                decimal montoDcto = precioBase * (dcto / 100);
                decimal precioFinalCalculado = precioBase + montoAumento - montoDcto;

                if (precioFinalCalculado < 0) precioFinalCalculado = 0; // Evitamos precios negativos

                // ==========================================================
                // 6. GUARDAMOS EN LA BASE DE DATOS
                // ==========================================================
                NCorpal_Cliente Nproy = new NCorpal_Cliente();
                bool exito = Nproy.actualizarDetalleListaProducto(codigoDetalle, dcto, aumento, precioFinalCalculado);

                if (exito)
                {
                    // Apagamos el modo edición
                    gvProductosLista.EditIndex = -1;

                    // Recargamos la grilla para ver los cambios aplicados
                    if (ViewState["IdListaSeleccionada"] != null)
                    {
                        int idLista = Convert.ToInt32(ViewState["IdListaSeleccionada"]);
                        CargarProductosDeLista(idLista);
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "exito", "alert('Valores del producto actualizados correctamente.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Ocurrió un error al guardar en la base de datos.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Error inesperado al procesar los datos.');", true);
            }
        }

        private void eliminarProductoDetalleLista(int codigoDetalle)
        {
            // 1. Llamamos a la capa de negocio para hacer el borrado lógico
            NCorpal_Cliente Nlista = new NCorpal_Cliente();
            bool exito = Nlista.eliminarDetalleLista(codigoDetalle);

            // 2. Verificamos el resultado
            if (exito)
            {
                // Si se borró bien, recargamos la grilla para que desaparezca de la pantalla
                if (ViewState["IdListaSeleccionada"] != null)
                {
                    int idListaActual = Convert.ToInt32(ViewState["IdListaSeleccionada"]);
                    CargarProductosDeLista(idListaActual);
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "exito", "alert('Producto quitado de la lista correctamente.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Ocurrió un error al intentar quitar el producto.');", true);
            }
        }







    }
}