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

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_AgregarInsumoCreado : System.Web.UI.Page
    {

        private DataTable insumosTable
        {
            get
            {
                if (Session["Insumos"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CodInsumo");
                    dt.Columns.Add("Nombre");
                    dt.Columns.Add("Cantidad");
                    dt.Columns.Add("Medida");
                    Session["Insumos"] = dt;
                }
                return (DataTable)Session["Insumos"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void gv_insumoCreado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                insumosTable.Rows.RemoveAt(rowIndex);
                gv_insumoCreado.DataSource = insumosTable;
                gv_insumoCreado.DataBind();
            }
        }

        [WebMethod]
        [ScriptMethod]
        // devuelve cod, insumo y medida 
        public static string[] GetListaInsumo(string prefixText, int count)
        {
            string nombreInsumo = prefixText;
            NA_CorpalAddInsumoCreado NinsumoC = new NA_CorpalAddInsumoCreado();
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

        [WebMethod]
        [ScriptMethod]
        public static string[] GetListaInsumoCreado(string prefixText, int count)
        {
            string NInsumoCreado = prefixText;
            NA_CorpalAddInsumoCreado Icreado = new NA_CorpalAddInsumoCreado();
            DataSet tuplas = Icreado.mostrarInsumoCreado_Autocomplit(NInsumoCreado);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];

            for (int i = 0; i < tuplas.Tables[0].Rows.Count; i++)
            {
                string nombre = tuplas.Tables[0].Rows[i]["nombre"].ToString();
                lista[i] = $"{nombre}";
            }
            return lista;
        }

        protected void btn_ADD_Click(object sender, EventArgs e)
        {
            DataRow newRow = insumosTable.NewRow();
 
            newRow["CodInsumo"] = (txt_codInsumo.Text);
            newRow["Nombre"] = txt_Nombre.Text;  
            newRow["Cantidad"] = (txt_Cantidad.Text); 
            newRow["Medida"] = (txt_Medida.Text); 

            insumosTable.Rows.Add(newRow);
            gv_insumoCreado.DataSource = insumosTable;
            gv_insumoCreado.DataBind();

            txt_codInsumo.Text = "";
            txt_Nombre.Text = "";
            txt_Cantidad.Text = "";
            txt_Medida.Text = "";


        }
        //-------------- MOSTRAR DETINSUMOCREADO
        public void MostrarInsumosPorCodigo(int codigo)
        {
            try
            {
                NA_CorpalAddInsumoCreado negocio = new NA_CorpalAddInsumoCreado();
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
                    NA_CorpalAddInsumoCreado neg = new NA_CorpalAddInsumoCreado();
                    bool codinsumocreado = neg.InsertarInsumoYDetalles(nombre, medida, gv_insumoCreado);

                    if (codinsumocreado)
                    {
                        Response.Write("<script>alert('InsumoCreado Registrado.');</script>");

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
                    Response.Write("<script>alert('Por Favor, Complete todos los campos.');</script>");
                } 

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Ocurrio un error inesperado: " + ex.Message + "');</script>");
            }
        }
        // -------------------------  EDITAR INSUMO CREADO (cant insumo)
        // ----------------  BUSCAR INSUMOCREADO
        protected void btn_BuscarInsumoCreado_Click(object sender, EventArgs e)
        {
            NA_CorpalAddInsumoCreado negocio = new NA_CorpalAddInsumoCreado();
            string NomInsumoCreado = txt_MInsumoCreado.Text;

            DataSet resultado = negocio.MostrarDetInsumoCreado(NomInsumoCreado);
            try
            {
                if (resultado != null && resultado.Tables.Count > 0 && resultado.Tables[0].Rows.Count > 0)
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

            }catch(Exception ex)
            {
                Response.Write($"<script>alert('Ocurrio un error: {ex.Message}');</script>");
            }
        }

        //-----------------------   MODIFICAR CANTIDAD INSUMO DEL INSUMOCREADO
        protected void btn_ModificarInsumoCreado_Click(object sender, EventArgs e)
        {
            NA_CorpalAddInsumoCreado negocioIC = new NA_CorpalAddInsumoCreado();
            bool resultadoGeneral = true;

            try
            {
                if (gv_MODInsumoCreado.Rows.Count > 0)
                {
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

        //     BTN INSERTAR NUEVO INSUMO A INSUMOCREADO EXISTENTE
        protected void btn_InsertarInsumo_Click(object sender, EventArgs e)
        {
            try
            {
                int codinsumo = int.Parse(txt_MInsumoCodigo.Text);
                int codinsumoCreado = int.Parse(txt_MCodICreado.Text);
                string cantidad = txt_MInsumoCantidad.Text;
                string medida = txt_MInsumoMedida.Text;

                NA_CorpalAddInsumoCreado negocio = new NA_CorpalAddInsumoCreado();
                bool resultado = negocio.InsertarInsumoNuevoIC(codinsumo, codinsumoCreado, cantidad, medida);

                if (resultado)
                {
                    Response.Write("<script>alert('Insumo Insertado Correctamente');</script>");
                    MostrarInsumosPorCodigo(codinsumoCreado);
                }
                else
                {
                    Response.Write("<script>alert('Error al insertar el insumo');</script>");
                }
            }
            catch (FormatException)
            {
                Response.Write("<script>alert('Por favor, Ingresa valores válidos.');</script>");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Ocurrio un error: {ex.Message}');</script>");
            }
        }
    }
}