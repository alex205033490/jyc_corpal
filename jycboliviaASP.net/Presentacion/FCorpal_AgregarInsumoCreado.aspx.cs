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
            if (!IsPostBack)
            {
                gv_insumoCreado.DataSource = insumosTable;
                gv_insumoCreado.DataBind();
            }
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
        // webservice q me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        //se devuelve un arreglo con la informacion
        public static string[] GetListaInsumo(string prefixText, int count)
        {
            string nombreInsumo = prefixText;
            NA_CorpalAddInsumoCreado NinsumoC = new NA_CorpalAddInsumoCreado();
            DataSet tuplas = NinsumoC.mostrarTodos_AutoComplitInsumo(nombreInsumo);

            // Crear la lista con el tamaño correcto
            string[] lista = new string[tuplas.Tables[0].Rows.Count];

            for (int i = 0; i < tuplas.Tables[0].Rows.Count; i++)
            {
                // Acceder al valor de la columna 'nombre' en cada fila
                string codigo = tuplas.Tables[0].Rows[i]["codigo"].ToString();
                string nombre = tuplas.Tables[0].Rows[i]["nombre"].ToString();
                string medida = tuplas.Tables[0].Rows[i]["Medida"].ToString();
                lista[i] = $"{codigo} | {nombre} | {medida}";
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

    }
}