using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FACorpal_GestionListaPrecio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // --- EVENTOS DE LA SECCIÓN 1: BUSCADOR Y NUEVA LISTA ---

        protected void btnBuscarLista_Click(object sender, EventArgs e)
        {
            // Lógica para buscar una lista por nombre
        }

        protected void btnNuevaLista_Click(object sender, EventArgs e)
        {
            // Lógica para abrir el formulario de nueva lista
        }

        // --- EVENTOS DE LA GRILLA MAESTRA (gvListasPrecio) ---

        protected void gvListasPrecio_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ESTE ES EL MÉTODO QUE FALTABA. 
            // Aquí capturaremos el ID de la lista seleccionada y mostraremos sus productos abajo.

            // Ejemplo de cómo capturar el ID de la fila seleccionada:
            // int idLista = Convert.ToInt32(gvListasPrecio.SelectedDataKey.Value);

            // Hacemos visible el panel de abajo
            panelProductos.Visible = true;
        }

        protected void gvListasPrecio_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Lógica para cuando se hace clic en el botón "Editar" de la lista
        }

        protected void gvListasPrecio_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Lógica para cuando se hace clic en el botón "Eliminar" de la lista
        }

        // --- EVENTOS DE LA GRILLA DETALLE (gvProductosLista) ---

        protected void btnAbrirAgregar_Click(object sender, EventArgs e)
        {
            // Lógica para abrir la ventana de agregar un nuevo producto a esta lista
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