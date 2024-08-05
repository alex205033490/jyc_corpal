using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Configuration;
using System.Data;
using System.IO;
using System.Drawing;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_VaciadoR148 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

           if (tienePermisoDeIngreso(94) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                cargarTipoCambio();
                buscar_R148_ParaCargarASimec("");             
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha ingresado a Vaciado R-148");
            }
        }

        private void cargarTipoCambio()
        {
            NA_Seguimiento seg = new NA_Seguimiento();
            float TC = seg.get_TipoUltimoTipoCambio();
            tx_tipoCambio.Text = TC.ToString();
        }

        private void buscar_R148_ParaCargarASimec(string proyecto)
        {
            NA_Seguimiento nseg = new NA_Seguimiento();
            DataSet tuplas = nseg.buscar_R148(false, false, proyecto);
            lb_cantDatos.Text = Convert.ToString(tuplas.Tables[0].Rows.Count);
            gv_r148_vaciarSimec.DataSource = tuplas;
            gv_r148_vaciarSimec.DataBind();
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

       

        protected void bt_anularR148_Click(object sender, EventArgs e)
        {
            anularPagoCobro();
        }

        private void anularPagoCobro()
        {
            foreach (GridViewRow row in gv_r148_vaciarSimec.Rows)
            {
                NA_Seguimiento nseg = new NA_Seguimiento();

                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.RowState = DataControlRowState.Edit;
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked == true)
                    {
                        int CodigoR148 = int.Parse(row.Cells[1].Text);
                        bool bandera = nseg.anularR148(CodigoR148, isChecked);
                    }
                }
            }
            buscar_R148_ParaCargarASimec("");
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string proyecto = tx_proyecto.Text;
            buscar_R148_ParaCargarASimec(proyecto);

        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {
            exportarEn_Excel();
        }

        private void exportarEn_Excel()
        {

            NA_Seguimiento nseg = new NA_Seguimiento();
            DataSet tuplas = nseg.buscar_R148(false, false, tx_proyecto.Text);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "R-148 para Vaciar " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = tuplas;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }
        }

        protected void bt_vaciarAlSimec_Click(object sender, EventArgs e)
        {
            vaciarPordiaAlSimec();
        }

        private void vaciarPordiaAlSimec()
        {   
            NA_Responsables Nresp = new NA_Responsables();
            string usuario = Session["NameUser"].ToString();
            string password = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuario, password);
            DataSet tupla = Nresp.get_responsable(codUser);
            string usuarioCobrador = tupla.Tables[0].Rows[0][1].ToString();

            float VTC = float.Parse(tx_tipoCambio.Text.Replace('.',','));
              foreach (GridViewRow row in gv_r148_vaciarSimec.Rows)
            {
                NA_Seguimiento nseg = new NA_Seguimiento();

                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.RowState = DataControlRowState.Edit;
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked == true)
                    {
                        int CodigoR148 = int.Parse(row.Cells[1].Text);                        
                        string VUSUARIO = "JYC";
                        string VENCODIGO = "GUN";                        
                        string BaseDeDatos = Session["BaseDatos"].ToString();                        
                        nseg.vaciadoSimecR148_JYCIA(CodigoR148, VTC, VUSUARIO, VENCODIGO, BaseDeDatos);
                       bool bandera4 = nseg.marcarVaciadoR148(CodigoR148);                       
                    }
                }
            }
              buscar_R148_ParaCargarASimec("");
        }

        protected void gv_r148_vaciarSimec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label listaDesplegable = (Label)e.Row.Cells[23].Controls.OfType<Label>().FirstOrDefault();
            listaDesplegable.Visible = false;

           /* if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.RowState = DataControlRowState.Edit;
               // e.Row.Cells[23].Controls.OfType<Label>().FirstOrDefault().Visible = false;
               // e.Row.Cells[23].Controls.OfType<TextBox>().FirstOrDefault().Visible = true;
                e.Row.Cells[23].Controls.OfType<TextBox>().FirstOrDefault().BackColor = Color.Yellow;
            }*/
        }

   
    }
}