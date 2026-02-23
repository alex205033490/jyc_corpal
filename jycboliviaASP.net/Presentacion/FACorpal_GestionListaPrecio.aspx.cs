using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using jycboliviaASP.net.Datos;
using System.Data;

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

        protected void btnCancelarLista_Click(object sender, EventArgs e)
        {
            // 1. Ocultamos el formulario
            panelFormularioLista.Visible = false;

            // 2. Volvemos a hacer visibles los controles de búsqueda y la grilla
            txtBuscarLista.Visible = true;
            btnBuscarLista.Visible = true;
            btnNuevaLista.Visible = true;
            gvListasPrecio.Visible = true;
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
            // =========================================================
            // AQUÍ VA TU LÓGICA DE NEGOCIO PARA GUARDAR EN BASE DE DATOS
            NCorpal_Cliente Nproy = new NCorpal_Cliente();
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

        // --- EVENTOS DE LA GRILLA MAESTRA (gvListasPrecio) ---

        protected void gvListasPrecio_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Obtenemos el ID de la lista seleccionada gracias a DataKeyNames="codigo"
            int idListaSeleccionada = Convert.ToInt32(gvListasPrecio.SelectedDataKey.Value);

            // 2. Extraemos el nombre de la lista para ponerlo en el título dinámico
            GridViewRow filaSeleccionada = gvListasPrecio.SelectedRow;
            LinkButton lnkNombre = (LinkButton)filaSeleccionada.FindControl("lnkNombreLista");
            lblNombreListaSeleccionada.Text = lnkNombre.Text;

            // 3. Llamamos a un método que busque los productos de ese ID
            CargarProductosDeLista(idListaSeleccionada);


            // Hacemos visible el panel de abajo
            panelProductos.Visible = true;
            panelAgregarProducto.Visible = false;
            btnAbrirAgregar.Visible = true;
        }



        protected void gvListasPrecio_RowEditing(object sender, GridViewEditEventArgs e)
        {

            /*
            // 1. Cancelamos la edición en línea por defecto del GridView
            gvListasPrecio.EditIndex = -1;

            // 2. Capturamos el ID de la lista que el usuario quiere editar
            int idLista = Convert.ToInt32(gvListasPrecio.DataKeys[e.NewEditIndex].Value);

            // ¡EL TRUCO! Guardamos este ID en memoria para usarlo luego al hacer clic en "Guardar"
            ViewState["IdListaEdicion"] = idLista;

            // 3. Obtenemos la fila exacta a la que se le hizo clic
            GridViewRow fila = gvListasPrecio.Rows[e.NewEditIndex];

            // 4. Extraemos los datos actuales de la grilla y llenamos nuestro formulario

            // El nombre está dentro de un LinkButton, así que lo buscamos:
            LinkButton lnkNombre = (LinkButton)fila.FindControl("lnkNombreLista");
            txtNombreLista.Text = lnkNombre.Text;

            // La descripción está en la celda 2 (índice 2). Usamos HtmlDecode por si hay espacios en blanco
            txtDescripcionLista.Text = Server.HtmlDecode(fila.Cells[2].Text);

            // El descuento está en la celda 3
            string descuentoActual = Server.HtmlDecode(fila.Cells[3].Text);

            // Limpiamos el formato (por si tiene separadores de miles) para que el input type="number" no falle
            txtDescuentoGral.Text = descuentoActual.Replace(",", ".");

            // 5. Cambiamos la vista: Mostramos el formulario y ocultamos lo demás
            panelFormularioLista.Visible = true;
            //panelBuscador.Visible = false;
            //panelGrillaMaestra.Visible = false;
            panelProductos.Visible = false;
            */

        }
        protected void gvListasPrecio_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idLista = Convert.ToInt32(gvListasPrecio.DataKeys[e.RowIndex].Value);
            eliminarListaProducto(idLista);
        }

        private void eliminarListaProducto(int idLista) {

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
            // 1. Limpiamos los campos
            txtPrecioAgregar.Text = "0.00";
            txtPrecioEspecialAgregar.Text = "0.00";
            txtDctoAgregar.Text = "0.00";
            txtCantidadDesdeAgregar.Text = "0.00";
            txtCantidadMinimaAgregar.Text = "1";
            txtAumentoAgregar.Text = "0.00";
            txtUnidadAgregar.Text = "";
            txtFechaInicioAgregar.Text = "";
            txtFechaFinAgregar.Text = "";

            // 2. Mostramos el panel verde
            panelAgregarProducto.Visible = true;

            // 3. Ocultamos el botón para que no lo presionen 2 veces
            btnAbrirAgregar.Visible = false;
        }

        protected void btnCancelarProducto_Click(object sender, EventArgs e)
        {
            // 1. Ocultamos el panel verde
            panelAgregarProducto.Visible = false;

            // 2. Volvemos a mostrar el botón de agregar
            btnAbrirAgregar.Visible = true;
        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            // ¡Aquí programaremos el INSERT a tbcorpal_detallelistaprecio en el siguiente paso!
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            // Lógica para quitar un producto de la lista
            // Como este es un LinkButton dentro de un TemplateField, se captura así:
            // LinkButton btn = (LinkButton)sender;
            // int idDetalle = Convert.ToInt32(btn.CommandArgument);
        }


    }
}