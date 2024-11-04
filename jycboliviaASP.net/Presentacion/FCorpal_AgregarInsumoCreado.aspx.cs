using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using System.Web.Services;
using jycboliviaASP.net.Negocio;
using System.Web.Script.Services;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using jycboliviaASP.net.DatosSimec;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_AgregarInsumoCreado : System.Web.UI.Page
    {
        private DataTable insumosTable
        {
            get
            {               

                if (ViewState["Insumos"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CodInsumo");
                    dt.Columns.Add("Nombre");
                    dt.Columns.Add("Cantidad");
                    dt.Columns.Add("Medida");
                    ViewState["Insumos"] = dt;
                }
                return (DataTable)ViewState["Insumos"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(133) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                gv_insumoCreado.DataSource = insumosTable;
                gv_insumoCreado.DataBind();
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

        // ELIMINAR FILA DEL GV
        protected void gv_insumoCreado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable insumosTable = this.insumosTable;

                if (insumosTable.Rows.Count > rowIndex)
                {
                    insumosTable.Rows.RemoveAt(rowIndex);
                    gv_insumoCreado.DataSource = insumosTable;
                    gv_insumoCreado.DataBind();
                }
            }
        }

        // AUTOCOMPLETAR INSUMOS 
        [WebMethod]
        [ScriptMethod]
        // devuelve cod, insumo y medida 
        public static string[] GetListaInsumo(string prefixText, int count)
        {
            string nombreInsumo = prefixText;
            NCorpal_AddInsumoCreado NinsumoC = new NCorpal_AddInsumoCreado();
            DataSet tuplas = NinsumoC.mostrarTodos_AutoComplitInsumo(nombreInsumo);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];

            for (int i = 0; i < tuplas.Tables[0].Rows.Count; i++)
            {
                string codigo = tuplas.Tables[0].Rows[i]["codigo"].ToString();
                string nombre = tuplas.Tables[0].Rows[i]["nombre"].ToString();
                string medida = tuplas.Tables[0].Rows[i]["Medida"].ToString();
                lista[i] = $"{codigo} | {nombre} | {medida}";
            }
            return lista;
        }

        // AUTOCOMPLETAR INSUMOS CREADOS
        [WebMethod]
        [ScriptMethod]
        public static string[] GetListaInsumoCreado(string prefixText, int count)
        {
            string NInsumoCreado = prefixText;
            NCorpal_AddInsumoCreado Icreado = new NCorpal_AddInsumoCreado();
            DataSet tuplas = Icreado.mostrarInsumoCreado_Autocomplit(NInsumoCreado);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];

            for (int i = 0; i < tuplas.Tables[0].Rows.Count; i++)
            {
                string nombre = tuplas.Tables[0].Rows[i]["nombre"].ToString();
                lista[i] = $"{nombre}";
            }
            return lista;
        }
        // BOTON PARA AGREGAR INSUMO AL GV
        protected void btn_ADD_Click(object sender, EventArgs e)
        {
            DataRow newRow = insumosTable.NewRow();

            if (!string.IsNullOrEmpty(txt_codInsumo.Text) &&
                !string.IsNullOrEmpty(txt_Nombre.Text) &&
                !string.IsNullOrEmpty(txt_Cantidad.Text) &&
                !string.IsNullOrEmpty(txt_Medida.Text))
            {
                newRow["CodInsumo"] = (txt_codInsumo.Text);
                newRow["Nombre"] = txt_Nombre.Text;
                newRow["Cantidad"] = (txt_Cantidad.Text);
                newRow["Medida"] = (txt_Medida.Text);

                insumosTable.Rows.Add(newRow);
                ViewState["Insumos"] = insumosTable;

                gv_insumoCreado.DataSource = insumosTable;
                gv_insumoCreado.DataBind();

                txt_codInsumo.Text = "";
                txt_Nombre.Text = "";
                txt_Cantidad.Text = "";
                txt_Medida.Text = "";
            }
            else
            {
                Response.Write("<script>alert('Por favor, complete los campos para añadir un insumo.');</script>");
            }
        }

        //-------------- MOSTRAR TABLA DetInsumoCreado
        public void MostrarInsumosPorCodigo(int codigo)
        {
            try
            {
                NCorpal_AddInsumoCreado negocio = new NCorpal_AddInsumoCreado();
                DataSet datosI = negocio.mostrarDetInsumoCreado(codigo);

                if (datosI != null && datosI.Tables.Count > 0 && datosI.Tables[0].Rows.Count > 0) 
                {


                    gv_MODInsumoCreado.DataSource = datosI.Tables[0];
                    gv_MODInsumoCreado.DataBind();
                }
                else
                {
                    gv_MODInsumoCreado.DataSource = null;
                    gv_MODInsumoCreado.DataBind();
                }

            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Ocurrio un error: {ex.Message}');</script>");
            }
        }


        //-------------- REGISTRAR NUEVO INSUMOCREADO CON INGREDIENTES
        protected void btn_registrarICreado2_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txt_nomInsumoCreado.Text;
                string medida = txt_medidaInsumoCreado.Text;

                if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(medida))
                {
                    if (gv_insumoCreado.Rows.Count > 0)
                    {
                        NCorpal_AddInsumoCreado neg = new NCorpal_AddInsumoCreado();
                        bool codinsumocreado = neg.InsertarInsumoYDetalles(nombre, medida, gv_insumoCreado);

                        if (codinsumocreado)
                        {
                            Response.Write("<script>alert('Insumo Registrado.');</script>");

                            txt_nomInsumoCreado.Text = string.Empty;
                            txt_medidaInsumoCreado.Text = string.Empty;

                            gv_insumoCreado.DataSource = null;
                            gv_insumoCreado.DataBind();

                        }
                        else
                        {
                            Response.Write("<script>alert('Error al insertar el insumo.');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Por favor, realice al menos una inserción de un insumo.')</script>");
                    }
                }
                else
                {
                        Response.Write("<script>alert('Por Favor, Complete todos los campos.');</script>");
                }
                       
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Ocurrio un error inesperado: " + ex.Message + "');</script>");
            }
        }
        // -------------------------  MODIFICAR INSUMO CREADO (cant insumo)
        // ----------------  BUSCAR INSUMOCREADO
        protected void btn_BuscarInsumoCreado_Click(object sender, EventArgs e)
        {
            NCorpal_AddInsumoCreado negocio = new NCorpal_AddInsumoCreado();
            string NomInsumoCreado = txt_MInsumoCreado.Text;

            DataSet resultado = negocio.MostrarDetInsumoCreado(NomInsumoCreado);
            try
            {
                if (resultado != null && resultado.Tables.Count > 0 )
                {
                    // asignar valores
                    DataRow insumoCreado = resultado.Tables[0].Rows[0];
                    txt_MCodICreado.Text = insumoCreado["CodigoIC"].ToString();
                    txt_MNombre.Text = insumoCreado["NombreIC"].ToString();
                    txt_MMedida.Text = insumoCreado["MedidaIC"].ToString();

                    // valores GV
                    DataTable dtInsumos = resultado.Tables[0];
                    gv_MODInsumoCreado.DataSource = dtInsumos;
                    gv_MODInsumoCreado.DataBind();
                }
                else
                {
                    txt_MCodICreado.Text = "";
                    txt_MNombre.Text = "";
                    txt_MMedida.Text = "";
                    gv_MODInsumoCreado.DataSource = null;
                    gv_MODInsumoCreado.DataBind();
                    Response.Write($"<script>alert('Error insumoCreado: {NomInsumoCreado} no existe');</script>");
                }

            }catch(Exception )
            {
                Response.Write($"<script>alert('Error no existe el Insumo. ');</script>");
            }
        }

        //-----------------------   MODIFICAR CANTIDAD INSUMO DEL INSUMOCREADO
        protected void btn_ModificarInsumoCreado_Click(object sender, EventArgs e)
        {
            NCorpal_AddInsumoCreado negocioIC = new NCorpal_AddInsumoCreado();
            bool resultadoGeneral = true;

            try
            {
                if (gv_MODInsumoCreado.Rows.Count == 0)
                {
                    Response.Write($"<script>alert('No actualizado. No hay registros para modificar')</script>");
                    return;
                }
                    foreach (GridViewRow row in gv_MODInsumoCreado.Rows)
                    {
                        int codInsumo = Convert.ToInt32(row.Cells[0].Text);

                        TextBox txtCantidad = (TextBox)row.FindControl("txt_NewCantidad");

                        string cantidadTexto = txtCantidad.Text.Replace(",", ".");
                        decimal nuevaCantidad;

                        if (decimal.TryParse(cantidadTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out nuevaCantidad))
                        {
                            int codInsumoCreado = Convert.ToInt32(txt_MCodICreado.Text);

                            bool resultado = negocioIC.ModificarDetInsumoCreado(codInsumoCreado, nuevaCantidad.ToString(CultureInfo.InvariantCulture), codInsumo);
                            resultadoGeneral &= resultado;
                        }
                        else
                        {
                            Response.Write($"<script>alert('Cantidad invalida en la fila con codigo :{codInsumo}')</script>");
                            return;
                        }
                    }
                    Response.Write($"<script>alert('Registro Actualizado')</script>");
            }
            catch (FormatException)
            {
                Response.Write($"<script>alert('Por favor, ingrese valores válidos');</script>");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Ocurrio un error: {ex.Message}');</script>");
            }
        }

        //  BTN AÑADIR NUEVO INSUMO A INSUMOCREADO EXISTENTE
        protected void btn_InsertarInsumo_Click(object sender, EventArgs e)
        {
            try
            {

                // Verificar campos
                if (string.IsNullOrEmpty(txt_MInsumoCodigo.Text) ||
                    string.IsNullOrEmpty(txt_MCodICreado.Text) ||
                    string.IsNullOrEmpty(txt_MInsumoCantidad.Text) ||
                    string.IsNullOrEmpty(txt_MInsumoMedida.Text))
                {
                    Response.Write("<script>alert('Por favor, complete todos los campos requeridos.');</script>");
                    return;
                }

                int codinsumo2;
                int codinsumoCreado2;

                if (!int.TryParse(txt_MInsumoCodigo.Text, out codinsumo2))
                {
                    Response.Write("<script>alert('El codigo de insumo es incorrecto');</script>");
                    return;
                }
                if (!int.TryParse(txt_MCodICreado.Text, out codinsumoCreado2))
                {
                    Response.Write("<script>alert('El codigo de insumo creado es incorrecto.');</script>");
                    return;
                }
                if (!decimal.TryParse(txt_MInsumoCantidad.Text, out _))
                {
                    Response.Write("<script>alert('Por favor, ingrese una cantidad valida.');</script>");
                }


                int codinsumo = int.Parse(txt_MInsumoCodigo.Text);
                int codinsumoCreado = int.Parse(txt_MCodICreado.Text);
                string cantidad = txt_MInsumoCantidad.Text;
                string medida = txt_MInsumoMedida.Text;

                NCorpal_AddInsumoCreado negocio = new NCorpal_AddInsumoCreado();
                bool resultado = negocio.InsertarInsumoNuevoIC(codinsumo, codinsumoCreado, cantidad, medida);

                if (resultado)
                {
                    Response.Write("<script>alert('Insumo Insertado Correctamente');</script>");

                    MostrarInsumosPorCodigo(codinsumoCreado);

                    txt_MInsumoCodigo.Text = "";
                    txt_MInsumoNombre.Text = "";
                    txt_MInsumoCantidad.Text = "";
                    txt_MInsumoMedida.Text = "";
                }
                else
                {
                    Response.Write("<script>alert('Error al insertar el insumo');</script>");
                }
            }
            /*catch (FormatException)
            {
                Response.Write("<script>alert('Por favor, Ingresa valores válidos.');</script>");
            }*/
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Ocurrio un error: {ex.Message}');</script>");
            }
        }

        // BTN CAMBIAR ESTADO DE UN INSUMO CREADO
        protected void btn_DeleteIC_Click(object sender, EventArgs e)
        {
            int codInsumoCreado;
            
            if (int.TryParse(txt_MCodICreado.Text, out codInsumoCreado))
            {
                NCorpal_AddInsumoCreado negocioIC = new NCorpal_AddInsumoCreado();
                bool resultado = negocioIC.ActualizarEstadoInsumoCreado(codInsumoCreado);

                if (resultado)
                {
                    Response.Write($"<script>alert('Insumo Eliminado.');</script>");
                    txt_MCodICreado.Text = string.Empty;
                    txt_MMedida.Text = string.Empty;
                    txt_MNombre.Text = string.Empty;
                    txt_MInsumoCreado.Text = string.Empty;

                    gv_MODInsumoCreado.DataSource = null;
                    gv_MODInsumoCreado.DataBind();
                }
                else
                {
                    Response.Write($"<script>alert('Error al eliminar el Insumo.');</script>");
                }
            }
            else
            {
                Response.Write($"<script>alert('Por Favor, Ingrese un valor valido a Eliminar.');</script>");
            }

        }

        protected void txt_Nombre_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            txt_MInsumoCodigo.Text = "";
            txt_MInsumoNombre.Text = "";
            txt_MInsumoCantidad.Text = "";
            txt_MInsumoMedida.Text = "";

            txt_MCodICreado.Text = string.Empty;
            txt_MMedida.Text = string.Empty;
            txt_MNombre.Text = string.Empty;
            txt_MInsumoCreado.Text = string.Empty;

            gv_MODInsumoCreado.DataSource = null;
            gv_MODInsumoCreado.DataBind();
        }
    }
}