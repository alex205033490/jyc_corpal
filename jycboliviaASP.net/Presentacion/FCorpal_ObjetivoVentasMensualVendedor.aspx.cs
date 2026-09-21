using jycboliviaASP.net.Negocio;
using jycboliviaASP.net.NegocioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ObjetivoVentasMensualVendedor : System.Web.UI.Page
    {
        

        protected void Page_Init(object sender, EventArgs e)
        {

            crearGridVendedorProducto();
            cargarProductos();

            System.Diagnostics.Debug.WriteLine(
                "FILAS: " +
                gv_vendedorproductoObj.Rows.Count
            );

            System.Diagnostics.Debug.WriteLine(
                "COLUMNAS: " +
                gv_vendedorproductoObj.Columns.Count
            );
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermiso(155) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                cargarAnios();

                // cargarProductos();
                dd_mes.SelectedValue = DateTime.Now.Month.ToString();
                cargarObj();
            }
        }

        private bool tienePermiso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usu = Session["NameUser"].ToString();
            string pass = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usu, pass);

            NA_DetallePermiso Nper = new NA_DetallePermiso();
            return Nper.tienePermisoResponsable(permiso, codUser);
        }

        private void crearGridVendedorProducto()
        {
            NCorpal_Venta nego = new NCorpal_Venta();

            gv_vendedorproductoObj.Columns.Clear();

            BoundField columnaProducto = new BoundField();

            columnaProducto.DataField = "producto";
            columnaProducto.HeaderText = "Producto";

            columnaProducto.HeaderStyle.BackColor = 
                System.Drawing.Color.Orange;

            columnaProducto.HeaderStyle.ForeColor = 
                System.Drawing.Color.Black;

            columnaProducto.ItemStyle.Font.Bold = true;
            columnaProducto.ItemStyle.CssClass = "columnaProducto";


            gv_vendedorproductoObj.Columns.Add(columnaProducto);

            DataSet dsVendedores = nego.get_listaVendedoresCorpal();

            if (dsVendedores == null || 
                dsVendedores.Tables.Count == 0 ||
                dsVendedores.Tables[0].Rows.Count == 0)
            {
                return;
            }

            foreach (DataRow vendedor in dsVendedores.Tables[0].Rows)
            {
                int codVendedor = Convert.ToInt32(vendedor["codigo"]);

                string nomVendedor = vendedor["nombre"].ToString();

                TemplateField columnaVendedor = new TemplateField();

                columnaVendedor.HeaderText = nomVendedor;

                columnaVendedor.ItemTemplate = new TextBoxTemplate("txtObj_" + codVendedor, codVendedor);

                columnaVendedor.HeaderStyle.BackColor = System.Drawing.Color.Gray;

                columnaVendedor.HeaderStyle.ForeColor = System.Drawing.Color.Black;

                columnaVendedor.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                gv_vendedorproductoObj.Columns.Add(columnaVendedor);
            }
        }


        public class TextBoxTemplate : ITemplate
        {
            private string id;
            private int codVendedor;

            public TextBoxTemplate(
                string id,
                int codVendedor)
            {
                this.id = id;
                this.codVendedor = codVendedor;
            }

            public void InstantiateIn(Control container)
            {
                TextBox txt = new TextBox();

                txt.ID = id;

                txt.CssClass =
                    "form-control form-control-sm";

                txt.Width =
                    Unit.Pixel(80);

                //txt.TextMode = TextBoxMode.Number;

                txt.Attributes["data-codvendedor"] =
                    (codVendedor.ToString());

                txt.Attributes["oninput"] =
                    "this.value = this.value.replace(/\\./g, ',');";

                container.Controls.Add(txt);
            }
        }

        private void cargarProductos()
        {
            NCorpal_Venta nego = new NCorpal_Venta();
            DataSet dsProducto = nego.get_listaProductosCorpal();

            if (dsProducto != null && 
                    dsProducto.Tables.Count > 0)
            {
                gv_vendedorproductoObj.DataSource =
                    dsProducto.Tables[0];

                gv_vendedorproductoObj.DataBind();
            }
        }

        //*****************************************************//
        //****************     BOTON REGISTRO     ***************//
        protected void btn_registrar_Click(object sender, EventArgs e)
        {
            try
            {
                string mes = dd_mes.SelectedItem.Text;
                string anio = dd_anio.SelectedValue;

                bool resultado = registroOBJventas();
                if (resultado)
                {
                    showalert("Se registraron los objetivos del mes [ " + mes + " ] del año " + anio );
                    crearGridVendedorProducto();
                    cargarProductos();
                }
                else
                    showalert("Ocurrio un error inesperado al registrar los objetivos.");
            }
            catch(Exception ex)
            {
                showalert("Ocurrio un error al registrar los datos. " + ex.Message);
            }

        }

        private bool registroOBJventas()
        {
            try
            {
                int mes = Convert.ToInt32(dd_mes.SelectedValue);
                int anio = Convert.ToInt32(dd_anio.SelectedValue);

                NCorpal_Objetivos nego = new NCorpal_Objetivos();

                if (gv_vendedorproductoObj.Rows.Count == 0)
                {
                    showalert("La GridView no tiene filas.");
                    return false;
                }

                int cantidadRegistros = 0;

                foreach (GridViewRow fila in gv_vendedorproductoObj.Rows)
                {
                    int codProducto = Convert.ToInt32(
                        gv_vendedorproductoObj
                            .DataKeys[fila.RowIndex]
                            .Value
                    );

                    for (int i = 1; i < fila.Cells.Count; i++)
                    {
                        foreach (Control control in fila.Cells[i].Controls)
                        {
                            if (control is TextBox)
                            {
                                TextBox txt = (TextBox)control;

                                if (string.IsNullOrWhiteSpace(txt.Text))
                                {
                                    continue;
                                }

                                decimal objetivo;

                                if(!decimal.TryParse(txt.Text, out objetivo))
                                {
                                    throw new Exception("El objetivo ingresado no es válido.");
                                }


                                string valorCodVendedor =
                                    txt.Attributes["data-codvendedor"];

                                if (string.IsNullOrEmpty(valorCodVendedor))
                                {
                                    throw new Exception(
                                        "No se encontró el código del vendedor."
                                    );
                                }

                                int codVendedor =
                                    Convert.ToInt32(valorCodVendedor);

                                bool resultado =
                                    nego.set_registroObjMensualVentas(
                                        mes,
                                        anio,
                                        codVendedor,
                                        codProducto,
                                        objetivo
                                    );

                                if (!resultado)
                                {
                                    showalert("Ocurrio un error al actualizar los datos.");
                                    return false;
                                }

                                cantidadRegistros++;
                            }
                        }
                    }
                }

                if (cantidadRegistros == 0)
                {
                    showalert(
                        "No se encontró ningún TextBox en la GridView."
                    );

                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("error en el registroOBJventas. "+ ex.Message);
            }
        }

        private void showalert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }


        //*****************************************************//
        //****************     BOTON BUSCAR     ***************//
        protected void btn_buscar_Click(object sender, EventArgs e)
        {

            
            crearGridVendedorProducto();

            cargarProductos();

            cargarObj();
        }

        private void cargarObj()
        {
            int mes = Convert.ToInt32(dd_mes.SelectedValue);
            int anio = Convert.ToInt32(dd_anio.SelectedValue);

            NCorpal_Objetivos nego = new NCorpal_Objetivos();

            DataSet ds = nego.get_obtenerObjVentasMensual(mes, anio);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                foreach(GridViewRow fila in gv_vendedorproductoObj.Rows)
                {
                    foreach(Control control in fila.Controls)
                    {
                        LimpiarTextBox(control);
                    }
                }
                return;
            }

            DataTable dt = ds.Tables[0];

            foreach(GridViewRow fila in gv_vendedorproductoObj.Rows)
            {
                if (fila.RowType != DataControlRowType.DataRow)
                    continue;

                int codProducto = Convert.ToInt32(gv_vendedorproductoObj.DataKeys[fila.RowIndex].Value);

                foreach(DataRow registro in dt.Rows)
                {
                    int productoBD = Convert.ToInt32(registro["codproducto"]);

                    if (productoBD != codProducto)
                    {
                        continue;
                    }

                    int codVendedor = Convert.ToInt32(registro["codvendedor"]);

                    decimal objetivo = Convert.ToDecimal(registro["cantidad"]);

                    TextBox txt = BuscarControl(fila, "txtObj_" + codVendedor) as TextBox;

                    if(txt != null)
                    {
                        txt.Text = objetivo.ToString();
                    }
                }
            }
        }

        private void LimpiarTextBox(Control padre)
        {
            if(padre is TextBox)
            {
                ((TextBox)padre).Text = "";
                return;
            }

            foreach (Control control in padre.Controls)
            {
                LimpiarTextBox(control);
            }
        }


        private Control BuscarControl(
                        Control padre,
                        string id)
        {
            if (padre.ID == id)
            {
                return padre;
            }

            foreach (Control control in padre.Controls)
            {
                Control encontrado =
                    BuscarControl(control, id);

                if (encontrado != null)
                {
                    return encontrado;
                }
            }
            return null;
        }

        //*****************************************************//
        //****************     BOTON LIMPIAR     ***************//
        protected void btn_limpiarForm_Click(object sender, EventArgs e)
        {
            crearGridVendedorProducto();
            cargarProductos();
        }

        private void cargarAnios()
        {
            int anioactual = DateTime.Now.Year;

            dd_anio.Items.Clear();

            for(int anio = anioactual - 3; anio <= anioactual + 3; anio++)
            {
                dd_anio.Items.Add(
                        new ListItem(
                            anio.ToString(),
                            anio.ToString()
                            )
                        );
            }
            dd_anio.SelectedValue = anioactual.ToString();
        }



    }
}