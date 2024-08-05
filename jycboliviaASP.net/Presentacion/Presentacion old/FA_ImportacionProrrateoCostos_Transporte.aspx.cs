using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ImportacionProrrateoCostos_Transporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(90) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                Limpiar_Datos();
                Buscar_Datos("", "", "", "", "", "");
                cargarddlEstadoEquipo();

            }
        }

        private void Limpiar_Datos()
        {
            tx_edificio.Text = "";
            tx_exbo.Text = "";
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

        private void cargarddlEstadoEquipo()
        {
            NEstadoEquipo estadoEquipo = new NEstadoEquipo();
            dd_estado.DataSource = estadoEquipo.listar();
            dd_estado.DataValueField = "codigo";
            dd_estado.DataTextField = "nombre";
            dd_estado.Items.Add(new ListItem("", "-1"));
            dd_estado.AppendDataBoundItems = true;
            dd_estado.SelectedIndex = -1;
            dd_estado.DataBind();
        }

        private void Buscar_Datos(string Edificio, string exbo, string estado, string semanaExpedicion, string Dui, string Contenedor)
        {
            //gv_datos.DataSource = null;
            NEquipo equipo1 = new NEquipo();
//            DataSet tuplaRes = equipo1.buscar_ProrrateoCostosGeneral_JYCIA(Edificio, exbo, estado, false, semanaExpedicion,  Dui,  Contenedor);

            DataSet tuplaRes = equipo1.buscar_ProrrateoCostosGeneralTransporte_JyCIA(false, Edificio, exbo, semanaExpedicion, Dui, Contenedor);
            gv_datos.DataSource = tuplaRes;
            gv_datos.DataBind();
            lb_cantidad.Text = equipo1.cant_ProrrateoCostosGeneralTransporte_JyCIA(true, Edificio, exbo, semanaExpedicion, Dui, Contenedor).ToString();
            //  OnCheckedChanged_seleccionTodo();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string exbo = tx_exbo.Text;
            string edificio = tx_edificio.Text;
            string estado = dd_estado.SelectedItem.Text;
            string semanaExp = tx_sem_Exp.Text;
            string Dui = tx_nroDui.Text;
            string Contenedor = tx_contenedor.Text;
            Buscar_Datos(edificio, exbo, estado, semanaExp,  Dui,  Contenedor);
        }

        protected void gv_datos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label listaDesplegable = (Label)e.Row.Cells[21].Controls.OfType<Label>().FirstOrDefault();
            if (listaDesplegable != null)
            {
                for (int i = 11; i < e.Row.Cells.Count; i++)
                {
                    e.Row.RowState = DataControlRowState.Edit;
                    e.Row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Visible = false;
                    string monto = e.Row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Text;
                    e.Row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Visible = true;
                    e.Row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().BackColor = Color.Yellow;
                    e.Row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Text = monto;
                }

                e.Row.Cells[17].Controls.OfType<Label>().FirstOrDefault().Visible = true;
                e.Row.Cells[17].Controls.OfType<TextBox>().FirstOrDefault().Visible = false;

                e.Row.Cells[18].Controls.OfType<Label>().FirstOrDefault().Visible = true;
                e.Row.Cells[18].Controls.OfType<TextBox>().FirstOrDefault().Visible = false;

                e.Row.Cells[19].Controls.OfType<Label>().FirstOrDefault().Visible = true;
                e.Row.Cells[19].Controls.OfType<TextBox>().FirstOrDefault().Visible = false;

                e.Row.Cells[20].Controls.OfType<Label>().FirstOrDefault().Visible = true;
                e.Row.Cells[20].Controls.OfType<TextBox>().FirstOrDefault().Visible = false;
            }
        }



        private void actualizarlosDAtosGeneralesdeJyCIA()
        {

            foreach (GridViewRow row in gv_datos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked)
                    {
                       int codigoEquipo = Convert.ToInt32(row.Cells[1].Text);
                       string Ciudad = row.Cells[7].Text;

                       string semana_expedicion = row.Cells[11].Controls.OfType<TextBox>().FirstOrDefault().Text;
                       string codnrodui = row.Cells[12].Controls.OfType<TextBox>().FirstOrDefault().Text;
                       string codnrocontenedor = row.Cells[13].Controls.OfType<TextBox>().FirstOrDefault().Text;

                       string pct_tamanio_contenedor = row.Cells[14].Controls.OfType<TextBox>().FirstOrDefault().Text;
                       string nro_bl = row.Cells[15].Controls.OfType<TextBox>().FirstOrDefault().Text;

                       float pct_peso;
                       float.TryParse(row.Cells[16].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out pct_peso);

                       float pct_costo_transporte;
                       float.TryParse(row.Cells[21].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out pct_costo_transporte);

                       float pct_costo_internacional;
                       float.TryParse(row.Cells[22].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out pct_costo_internacional);

                       float pct_costo_nacional;
                       float.TryParse(row.Cells[23].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out pct_costo_nacional);

                       string pct_fechapagarproveedor = convertidorFecha(row.Cells[24].Controls.OfType<TextBox>().FirstOrDefault().Text);
                       
                        NEquipo equipo = new NEquipo();
                        bool actualizado = equipo.actualizar_ProrrateoCostos_General( codigoEquipo,  Ciudad,
                                            codnrodui,  codnrocontenedor,  semana_expedicion,  pct_tamanio_contenedor,  pct_peso,  pct_costo_transporte,  pct_costo_internacional,
                                             pct_costo_nacional,  pct_fechapagarproveedor,   nro_bl);
                        ///---------------modificar Estado pero ay k cambiar la base de datos-----------------------------------------------------                                                                       

                        NA_Responsables Nresp = new NA_Responsables();
                        string usuarioAux = Session["NameUser"].ToString();
                        string passwordAux = Session["passworuser"].ToString();
                        int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                        ///--------------------------------------------
                        NA_Historial nhistorial = new NA_Historial();
                        // int codUser = Convert.ToInt32(Session["coduser"].ToString());
                        nhistorial.insertar(codUser, "Ha Datos de Prorrateo de Costo del equipo " + codigoEquipo + " del Exbo " + tx_exbo.Text);
                        ///--------------------------------------------
                        ///----------------------------------fin de modificar estado-----------------------------------------------------


                    }
                }
            }

        }


        public void Exportar_Excel()
        {
            string edificio = tx_edificio.Text;
            string exbo = tx_exbo.Text;
            string estado = dd_estado.SelectedItem.Text;
            string semanaExp = tx_sem_Exp.Text;
            string Dui = tx_nroDui.Text;
            string Contenedor = tx_contenedor.Text;

            NEquipo equipo1 = new NEquipo();
            DataSet tuplaRes = equipo1.buscar_ProrrateoCostosGeneral_JYCIA(edificio, exbo, estado, true, semanaExp,  Dui,  Contenedor);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombreDB = Session["BaseDatos"].ToString();
            string nombre = "Datos de Importacion General " + nombreDB + " " + DateTime.Now.ToString("dd/MM/yyyy");
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = tuplaRes;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }

        }

       
        protected void bt_Update_Click(object sender, EventArgs e)
        {
            actualizarlosDAtosGeneralesdeJyCIA();
            string exbo = tx_exbo.Text;
            string edificio = tx_edificio.Text;
            string estado = dd_estado.SelectedItem.Text;
            string semanaExp = tx_sem_Exp.Text;
            string Dui = tx_nroDui.Text;
            string Contenedor = tx_contenedor.Text;
            Buscar_Datos(edificio, exbo, estado, semanaExp, Dui, Contenedor);
        }

    }
}