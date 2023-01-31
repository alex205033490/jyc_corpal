using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Drawing;
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ImportacionDatosGeneral_JyCIA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(65) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                Limpiar_Datos();
                Buscar_Datos("", "", "","");
                cargarddlEstadoEquipo();

            }
        }

        private void Limpiar_Datos()
        {
            tx_edificio.Text = "";
            tx_exbo.Text = "";
            tx_sem_Exp.Text = "";
            
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

        private void Buscar_Datos(string Edificio, string exbo, string estado, string semanaExpedicion)
        {
            //gv_datos.DataSource = null;
            NEquipo equipo1 = new NEquipo();
            DataSet tuplaRes = equipo1.buscar_DatosGeneral_JYCIA(Edificio, exbo, estado, false, semanaExpedicion);
            gv_datos.DataSource = tuplaRes;
            gv_datos.DataBind();
            lb_cantidad.Text = equipo1.cant_DatosGeneral_JYCIA(Edificio, exbo, estado, semanaExpedicion).ToString();
            //  OnCheckedChanged_seleccionTodo();
        }

        protected void gv_datos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            Label listaDesplegable = (Label)e.Row.Cells[21].Controls.OfType<Label>().FirstOrDefault();
            if (listaDesplegable != null)
            {
                for (int i = 20; i < e.Row.Cells.Count; i++)
                {
                    e.Row.RowState = DataControlRowState.Edit;
                    e.Row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Visible = false;
                    string monto = e.Row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Text;
                    e.Row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Visible = true;
                    e.Row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().BackColor = Color.Yellow;
                    e.Row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Text = monto;
                }

                e.Row.Cells[26].Controls.OfType<Label>().FirstOrDefault().Visible = true;                
                e.Row.Cells[26].Controls.OfType<TextBox>().FirstOrDefault().Visible = false;

                e.Row.Cells[32].Controls.OfType<Label>().FirstOrDefault().Visible = true;
                e.Row.Cells[32].Controls.OfType<TextBox>().FirstOrDefault().Visible = false;

                e.Row.Cells[38].Controls.OfType<Label>().FirstOrDefault().Visible = true;
                e.Row.Cells[38].Controls.OfType<TextBox>().FirstOrDefault().Visible = false;                
            }

        }



        protected void bt_Update_Click(object sender, EventArgs e)
        {
            actualizarlosDAtosGeneralesdeJyCIA();
            string exbo = tx_exbo.Text;
            string edificio = tx_edificio.Text;
            string estado = dd_estado.SelectedItem.Text;
            string semanaExpedicion = tx_sem_Exp.Text;
            Buscar_Datos(edificio, exbo, estado, semanaExpedicion);
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

                        string semanaExp = row.Cells[20].Controls.OfType<TextBox>().FirstOrDefault().Text;                        
                        string giros1_nroproforma = row.Cells[21].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        string giros1_nrooperacion = row.Cells[22].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        string fechaGiro1 = convertidorFecha(row.Cells[23].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        float giros1_euros_cp;
                        float.TryParse(row.Cells[24].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out giros1_euros_cp);
                        float giros1_tc_orona;
                        float.TryParse(row.Cells[25].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out giros1_tc_orona);
                        float giros1_dolares;
                        float.TryParse(row.Cells[26].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out giros1_dolares);

                        string giros2_nroproforma = row.Cells[27].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        string giros2_nrooperacion = row.Cells[28].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        string fechaGiro2 = convertidorFecha(row.Cells[29].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        float giros2_euros_cp;
                        float.TryParse(row.Cells[30].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out giros2_euros_cp);
                        float giros2_tc_orona;
                        float.TryParse(row.Cells[31].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out giros2_tc_orona);
                        float giros2_dolares;
                        float.TryParse(row.Cells[32].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out giros2_dolares);

                        string giros3_nroproforma = row.Cells[33].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        string giros3_nrooperacion = row.Cells[34].Controls.OfType<TextBox>().FirstOrDefault().Text;
                        string fechaGiro3 = convertidorFecha(row.Cells[35].Controls.OfType<TextBox>().FirstOrDefault().Text);
                        float giros3_euros_cp;
                        float.TryParse(row.Cells[36].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out giros3_euros_cp);
                        float giros3_tc_orona;
                        float.TryParse(row.Cells[37].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out giros3_tc_orona);
                        float giros3_dolares;
                        float.TryParse(row.Cells[38].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','), out giros3_dolares);

                        NEquipo equipo = new NEquipo();
                        bool actualizado = equipo.actualizar_DatosGeneralJYCIA_General(codigoEquipo, Ciudad,
                        giros1_nroproforma, giros1_nrooperacion, fechaGiro1, giros1_euros_cp, giros1_tc_orona, giros1_dolares,
                        giros2_nroproforma, giros2_nrooperacion, fechaGiro2, giros2_euros_cp, giros2_tc_orona, giros2_dolares,
                        giros3_nroproforma, giros3_nrooperacion, fechaGiro3, giros3_euros_cp, giros3_tc_orona, giros3_dolares,
                        semanaExp);
                        ///---------------modificar Estado pero ay k cambiar la base de datos-----------------------------------------------------                                                                       

                        NA_Responsables Nresp = new NA_Responsables();
                        string usuarioAux = Session["NameUser"].ToString();
                        string passwordAux = Session["passworuser"].ToString();
                        int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);                        
                            
                            ///--------------------------------------------
                            NA_Historial nhistorial = new NA_Historial();
                            // int codUser = Convert.ToInt32(Session["coduser"].ToString());
                            nhistorial.insertar(codUser, "Ha Modificado el equipo " + codigoEquipo + " del Exbo " + tx_exbo.Text);
                            ///--------------------------------------------
                            ///----------------------------------fin de modificar estado-----------------------------------------------------
                            

                        }
                    }
                }

            }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
             string exbo = tx_exbo.Text;
            string edificio = tx_edificio.Text;
            string estado = dd_estado.SelectedItem.Text;
            string semanaExp = tx_sem_Exp.Text;
            Buscar_Datos(edificio, exbo, estado, semanaExp);
        }



        public void Exportar_Excel()
        {
            string edificio = tx_edificio.Text;
            string exbo = tx_exbo.Text;
            string estado = dd_estado.SelectedItem.Text;
            string semanaExp = tx_sem_Exp.Text;

            NEquipo equipo1 = new NEquipo();
            DataSet tuplaRes = equipo1.buscar_DatosGeneral_JYCIA(edificio, exbo, estado, true, semanaExp);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombreDB = Session["BaseDatos"].ToString();
            string nombre = "Datos de Importacion General JYCIA" + nombreDB + " " + DateTime.Now.ToString("dd/MM/yyyy");
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

        protected void bt_Exportar_Click(object sender, EventArgs e)
        {
            Exportar_Excel();
        }

        protected void bt_Limpiar_Click(object sender, EventArgs e)
        {
            Limpiar_Datos();
        }

       


    }
}