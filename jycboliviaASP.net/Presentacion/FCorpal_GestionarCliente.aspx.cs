using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using jycboliviaASP.net.Datos;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_GestionarCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)

        {
            this.Title = Session["BaseDatos"].ToString();
             if (tienePermisoDeIngreso(150) == false)
             {
                 string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                 Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
             }
            if (!IsPostBack)
            {
                cargarTipoCliente();
                cargarListaPrecio();
                buscarClientesT("");
            }

        }

        protected void cargarListaPrecio()
        {
            NCorpal_Cliente Nproy = new NCorpal_Cliente();
            DataSet listaTipoCliente = Nproy.mostrarListaPrecio();

            ddl_listaprecio.DataSource = listaTipoCliente;
            ddl_listaprecio.DataValueField = "codigo";
            ddl_listaprecio.DataTextField = "nombre";
            ddl_listaprecio.Items.Add(new ListItem("", "-1"));
            ddl_listaprecio.AppendDataBoundItems = true;
            ddl_listaprecio.SelectedIndex = -1;
            ddl_listaprecio.DataBind();


        }
        protected void cargarTipoCliente()
        {

            NCorpal_Cliente Nproy = new NCorpal_Cliente();
            DataSet listaTipoCliente = Nproy.mostrarTipoCliente();

            ddl_tipocliente.DataSource = listaTipoCliente;
            ddl_tipocliente.DataValueField = "codigo";
            ddl_tipocliente.DataTextField = "nombre";
            ddl_tipocliente.Items.Add(new ListItem("", "-1"));
            ddl_tipocliente.AppendDataBoundItems = true;
            ddl_tipocliente.SelectedIndex = -1;
            ddl_tipocliente.DataBind();
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

        private void buscarClientesT(string nombreTienda)
        {
            NCorpal_Cliente Nproy = new NCorpal_Cliente();
            DataSet lista = Nproy.listarTiendas2(nombreTienda);
            gv_Clientes.DataSource = lista;
            gv_Clientes.DataBind();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            gv_Clientes.PageIndex = 0;

            // 2. Obtenemos el texto del buscador quitando espacios sobrantes
            string criterio = tx_buscar.Text.Trim();

            // 3. Llamamos al método que ya tienes creado para cargar la grilla
            buscarClientesT(criterio);
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            // 1. Limpiar todos los campos de texto
            tx_tiendaname.Text = "";
            tx_tiendadir.Text = "";
            tx_tiendatelefono.Text = "";
            tx_tiendadepartamento.Text = "";
            tx_tiendazona.Text = "";

            tx_propietarioname.Text = "";
            tx_propietarioci.Text = "";
            tx_propietariodir.Text = "";
            tx_propietariocelular.Text = "";
            tx_propietarionit.Text = "";
            tx_propietariocorreo.Text = "";

            tx_facturar_a.Text = "";
            tx_facturar_nit.Text = "";
            tx_facturar_correo.Text = "";
            tx_observacion.Text = "";

            // Opcional: Limpiar el buscador
            tx_buscar.Text = "";

            // 2. Reiniciar los DropDownLists al primer elemento (si existen datos)
            if (ddl_tipocliente.Items.Count > 0) ddl_tipocliente.SelectedIndex = 0;
            if (ddl_listaprecio.Items.Count > 0) ddl_listaprecio.SelectedIndex = 0;

            // 3. RESTABLECER BOTONES (Modo Creación)
            bt_insertar.Enabled = true;     // Habilitamos Guardar
            bt_modificar.Enabled = false;   // Deshabilitamos Editar
            bt_eliminar.Enabled = false;    // Deshabilitamos Eliminar

            // 4. Reiniciar la variable de estado (IMPORTANTE)
            // Esto evita que si le das a "Insertar" intente actualizar un ID viejo
            ViewState["IDClienteSeleccionado"] = 0;

            // 5. Limpiar mensajes de error/éxito
            lbl_mensaje.Text = "";
        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            // 1. VALIDACIONES
            if (string.IsNullOrWhiteSpace(tx_tiendaname.Text))
            {
                MostrarAlerta("ERROR: El nombre de la tienda es obligatorio.");
                tx_tiendaname.Focus();
                return;
            }

            if (ddl_tipocliente.SelectedValue == "-1" || ddl_tipocliente.SelectedIndex <= 0)
            {
                MostrarAlerta("ERROR: Debe seleccionar un TIPO DE CLIENTE válido.");
                ddl_tipocliente.Focus();
                return;
            }

            if (ddl_listaprecio.SelectedValue == "-1" || ddl_listaprecio.SelectedIndex <= 0)
            {
                MostrarAlerta("ERROR: Debe seleccionar una LISTA DE PRECIOS válida.");
                ddl_listaprecio.Focus();
                return;
            }

            // 2. PROCESO
            try
            {
                NCorpal_Cliente Nproy = new NCorpal_Cliente();
                string nombreValidar = tx_tiendaname.Text.Trim().ToUpper();

                if (Nproy.existeCliente(nombreValidar))
                {
                    // Usa el método ScriptManager para que funcione el alert
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('ADVERTENCIA: Ya existe un cliente con ese Nombre de Tienda.');", true);
                    return;
                }

                // Si no existe, procedemos...
                int codUser = 1; // Valor por defecto
                if (Session["coduser"] != null)
                    codUser = Convert.ToInt32(Session["coduser"].ToString());

                int idTipoCliente = int.Parse(ddl_tipocliente.SelectedValue);
                int idListaPrecio = int.Parse(ddl_listaprecio.SelectedValue);
                string latitud = "";
                string longitud = "";

                // Insertar (Asegúrate de que tu Negocio acepte los parámetros en este orden)
                bool insertado = Nproy.insertar_cliente(
                    tx_tiendaname.Text.Trim().ToUpper(),
                    tx_tiendadir.Text.Trim().ToUpper(),
                    tx_tiendatelefono.Text.Trim(),
                    tx_tiendadepartamento.Text.Trim().ToUpper(),
                    tx_tiendazona.Text.Trim().ToUpper(),
                    tx_propietarioname.Text.Trim().ToUpper(),
                    tx_propietarioci.Text.Trim(),
                    tx_propietariodir.Text.Trim().ToUpper(),
                    tx_propietariocelular.Text.Trim(),
                    tx_propietarionit.Text.Trim(),
                    tx_propietariocorreo.Text.Trim(),
                    tx_facturar_a.Text.Trim().ToUpper(),
                    tx_facturar_nit.Text.Trim(),
                    tx_facturar_correo.Text.Trim(),
                    tx_observacion.Text.Trim().ToUpper(),

                    // Tus parámetros extra tal como los tienes en la capa de Negocio:
                    codUser,
                    latitud,
                    longitud,
                    idTipoCliente,
                    idListaPrecio
                );

                if (insertado)
                {
                    MostrarAlerta("ÉXITO: Cliente registrado correctamente.");
                    buscarClientesT("");          // Recargar tabla
                    bt_limpiar_Click(null, null); // Limpiar formulario
                }
                else
                {
                    MostrarAlerta("ERROR: No se pudo registrar el cliente en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message.Replace("'", "").Replace("\n", " ");
                MostrarAlerta("ERROR CRÍTICO: " + mensajeError);
            }
        }

        // --- MÉTODO AUXILIAR PARA ALERTAS COMPATIBLES CON AJAX ---
        private void MostrarAlerta(string mensaje)
        {
            // Esto funciona tanto dentro como fuera de un UpdatePanel
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('" + mensaje + "');", true);
        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {



            // 1. VALIDACIÓN DE SEGURIDAD (¿Hay un cliente seleccionado?)
            if (ViewState["IDClienteSeleccionado"] == null || Convert.ToInt32(ViewState["IDClienteSeleccionado"]) == 0)
            {
                MostrarAlerta("ERROR: No hay ningún cliente seleccionado para modificar.");
                return;
            }

            // 2. VALIDACIONES DE CAMPOS (Igual que en Insertar)
            if (string.IsNullOrWhiteSpace(tx_tiendaname.Text))
            {
                MostrarAlerta("ERROR: El nombre de la tienda es obligatorio.");
                return;
            }

            if (ddl_tipocliente.SelectedValue == "-1" || ddl_tipocliente.SelectedIndex <= 0)
            {
                MostrarAlerta("ERROR: Debe seleccionar un TIPO DE CLIENTE válido.");
                return;
            }

            // ... (Puedes agregar la validación de Lista de Precio aquí si quieres) ...

            try
            {
                // 3. RECOLECCIÓN DE DATOS
                NCorpal_Cliente Nproy = new NCorpal_Cliente();

                int idCliente = Convert.ToInt32(ViewState["IDClienteSeleccionado"]);
                string nombreTienda = tx_tiendaname.Text.Trim().ToUpper();

                // 2. VALIDACIÓN INTELIGENTE
                // Verificamos si el nombre existe en OTRO cliente diferente a este
                if (Nproy.existeClienteParaModificar(nombreTienda, idCliente))
                {
                    MostrarAlerta("ADVERTENCIA: Ese nombre de tienda ya pertenece a otro cliente.");
                    return; // Detenemos el proceso
                }

               
                int codUser = 1;
                if (Session["coduser"] != null) codUser = Convert.ToInt32(Session["coduser"].ToString());

                int idTipoCliente = int.Parse(ddl_tipocliente.SelectedValue);
                int idListaPrecio = int.Parse(ddl_listaprecio.SelectedValue);

                // 4. LLAMADA A LA CAPA DE NEGOCIO (Método modificar)
                bool modificado = Nproy.modificar_cliente(
                    idCliente, // <--- PRIMER PARÁMETRO: El ID a modificar
                    tx_tiendaname.Text.Trim().ToUpper(),
                    tx_tiendadir.Text.Trim().ToUpper(),
                    tx_tiendatelefono.Text.Trim(),
                    tx_tiendadepartamento.Text.Trim().ToUpper(),
                    tx_tiendazona.Text.Trim().ToUpper(),
                    tx_propietarioname.Text.Trim().ToUpper(),
                    tx_propietarioci.Text.Trim(),
                    tx_propietariodir.Text.Trim().ToUpper(),
                    tx_propietariocelular.Text.Trim(),
                    tx_propietarionit.Text.Trim(),
                    tx_propietariocorreo.Text.Trim(),
                    tx_facturar_a.Text.Trim().ToUpper(),
                    tx_facturar_nit.Text.Trim(),
                    tx_facturar_correo.Text.Trim(),
                    tx_observacion.Text.Trim().ToUpper(),

                    codUser, // codrespgra (Quién modificó)
                   
                    "",      // latitud
                    "",      // longitud

                    idTipoCliente,
                    idListaPrecio
                );

                if (modificado)
                {
                    MostrarAlerta("ÉXITO: Cliente modificado correctamente.");
                    buscarClientesT("");          // Recargar tabla para ver cambios
                    bt_limpiar_Click(null, null); // Limpiar y volver a modo Insertar
                }
                else
                {
                    MostrarAlerta("ERROR: No se pudo modificar el registro.");
                }
            }
            catch (Exception ex)
            {
                string msj = ex.Message.Replace("'", "").Replace("\n", " ");
                MostrarAlerta("ERROR CRÍTICO: " + msj);
            }
        }
        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            // 1. VALIDACIÓN DE SEGURIDAD
            if (ViewState["IDClienteSeleccionado"] == null || Convert.ToInt32(ViewState["IDClienteSeleccionado"]) == 0)
            {
                MostrarAlerta("ERROR: Seleccione un cliente para eliminar.");
                return;
            }

            try
            {
                NCorpal_Cliente Nproy = new NCorpal_Cliente();
                int idCliente = Convert.ToInt32(ViewState["IDClienteSeleccionado"]);

                // 2. EJECUTAR ELIMINACIÓN
                bool eliminado = Nproy.eliminar_cliente(idCliente);

                if (eliminado)
                {
                    MostrarAlerta("ÉXITO: Cliente eliminado correctamente.");
                    buscarClientesT("");          // Recargar tabla
                    bt_limpiar_Click(null, null); // Limpiar formulario
                }
            }
            catch (Exception ex)
            {
                // 3. MANEJO DE ERRORES ESPECÍFICOS (Integridad Referencial)
                // El error 1451 en MySQL significa que hay datos relacionados (ej: ventas)
                if (ex.Message.Contains("foreign key constraint") || ex.Message.Contains("1451"))
                {
                    MostrarAlerta("NO SE PUEDE ELIMINAR: El cliente tiene registros asociados (Ventas, Pedidos, etc).");
                }
                else
                {
                    string msj = ex.Message.Replace("'", "").Replace("\n", " ");
                    MostrarAlerta("ERROR CRÍTICO: " + msj);
                }
            }
        }

        protected void gv_Clientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Cargar tus datos (tu lógica actual)
            cargarDatosSeleccionado();

        }
        protected void gv_Clientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // 1. Establece la nueva página del índice
            gv_Clientes.PageIndex = e.NewPageIndex;

            // 2. Vuelve a cargar los datos para mostrar la nueva página
            buscarClientesT("");
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {

        }

        protected void cargarDatosSeleccionado()
        {
            try
            {
                // 1. VERIFICACIÓN DE SEGURIDAD
                // Si por alguna razón no hay fila seleccionada, salimos para evitar errores.
                if (gv_Clientes.SelectedIndex < 0) return;

                // 2. OBTENER EL ID SELECCIONADO (FORMA CORRECTA)
                // Usamos DataKeys en lugar de Cells[1].Text. 
                // Esto toma el valor real oculto en la propiedad DataKeyNames="codigo"
                int idCliente = Convert.ToInt32(gv_Clientes.DataKeys[gv_Clientes.SelectedIndex].Value);

                // 3. GUARDAR EL ID
                ViewState["IDClienteSeleccionado"] = idCliente;

                // 4. CONSULTAR DATOS
                NCorpal_Cliente Nproy = new NCorpal_Cliente();
                DataSet ds = Nproy.get_cliente(idCliente);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    // --- RELLENAR CAMPOS ---

                    // Texto
                    tx_tiendaname.Text = row["tiendaname"].ToString();
                    tx_tiendadir.Text = row["tiendadir"].ToString();
                    tx_tiendatelefono.Text = row["tiendatelefono"].ToString();
                    tx_tiendadepartamento.Text = row["tiendadepartamento"].ToString();
                    tx_tiendazona.Text = row["tiendazona"].ToString();

                    // Propietario
                    tx_propietarioname.Text = row["propietarioname"].ToString();
                    tx_propietarioci.Text = row["propietarioci"].ToString();
                    tx_propietariodir.Text = row["propietariodir"].ToString();
                    tx_propietariocelular.Text = row["propietariocelular"].ToString();
                    tx_propietarionit.Text = row["propietarionit"].ToString();
                    tx_propietariocorreo.Text = row["propietariocorreo"].ToString();

                    // Facturación
                    tx_facturar_a.Text = row["facturar_a"].ToString();
                    tx_facturar_nit.Text = row["facturar_nit"].ToString();
                    tx_facturar_correo.Text = row["facturar_correo"].ToString();
                    tx_observacion.Text = row["observacion"].ToString();

                    // --- DROPDOWNS ---
                    string idTipo = row["id_tipocliente"].ToString();
                    string idLista = row["id_listaprecio"].ToString();

                    // Reseteamos primero para evitar errores de selección previa
                    ddl_tipocliente.SelectedIndex = -1;
                    ddl_listaprecio.SelectedIndex = -1;

                    if (!string.IsNullOrEmpty(idTipo) && ddl_tipocliente.Items.FindByValue(idTipo) != null)
                        ddl_tipocliente.SelectedValue = idTipo;

                    if (!string.IsNullOrEmpty(idLista) && ddl_listaprecio.Items.FindByValue(idLista) != null)
                        ddl_listaprecio.SelectedValue = idLista;

                    // --- BOTONES ---
                    bt_insertar.Enabled = false;  // Deshabilitar Guardar Nuevo
                    bt_modificar.Enabled = true;  // Habilitar Modificar
                    bt_eliminar.Enabled = true;   // Habilitar Eliminar

                    // --- MENSAJE DE ÉXITO ---
                    lbl_mensaje.Text = "Cliente seleccionado correctamente. ID: " + idCliente;
                    lbl_mensaje.ForeColor = System.Drawing.Color.Blue;
                }
            }
            catch (Exception ex)
            {
                // ¡IMPORTANTE! Descomenta esto para ver el error en pantalla
                lbl_mensaje.Text = "Error al seleccionar: " + ex.Message;
                lbl_mensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}