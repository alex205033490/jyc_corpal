using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Drawing;
using System.IO;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_saldosCuentasGeneral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            
            if (tienePermisoDeIngreso(44) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
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

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            buscarSaldosCuentas();
            ponercolor();
        }

        private void ponercolor()
        {
            for (int i = 0; i < gv_saldosCuentasGeneral.Rows.Count; i++)
            {
                string banco = gv_saldosCuentasGeneral.Rows[i].Cells[0].Text;
                if (banco.Equals("Santa Cruz") || banco.Equals("Cochabamba") 
                    || banco.Equals("La Paz") || banco.Equals("JyC SRL") ||
                    banco.Equals("JyCIA SRL") || banco.Equals("Imven"))
                {
                    gv_saldosCuentasGeneral.Rows[i].BackColor = Color.Red;
                    gv_saldosCuentasGeneral.Rows[i].ForeColor = Color.White;
                }
            }    
        }

        private void buscarSaldosCuentas()
        {
            if (tx_fechaDesde.Text != "" && tx_fechahasta.Text != "")
            {
                NA_banco nbancos = new NA_banco();
                DataSet tuplas = nbancos.VistaSaldosCuentaGeneral(tx_fechaDesde.Text, tx_fechahasta.Text);
                gv_saldosCuentasGeneral.DataSource = tuplas;
                gv_saldosCuentasGeneral.DataBind();
           }
        }

        protected void linkb_excel_Click(object sender, EventArgs e)
        {
            exportarExcel();
        }

        private void exportarExcel()
        {


            NA_banco nbancos = new NA_banco();
            DataSet resultado = nbancos.VistaSaldosCuentaGeneral(tx_fechaDesde.Text, tx_fechahasta.Text);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Saldos Cuentas - General";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = resultado;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }

    }
}