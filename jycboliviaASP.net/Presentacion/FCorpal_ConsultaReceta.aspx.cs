using jycboliviaASP.net.Datos;
using jycboliviaASP.net.Negocio;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ConsultaReceta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(137) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {
                //  cargarCobrador();
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
            consultadedatos();
        }

        private void consultadedatos()
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            

            if (dd_consulta.SelectedIndex > -1 )
            {
                if (dd_consulta.SelectedIndex == 0)
                {
                    get_Receta();
                }

            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Datos incorrectos') </script>");
        }

        private void get_Receta()
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Receta/Report_Receta_Todos.rdlc";

            NCorpal_Produccion npro = new NCorpal_Produccion();
            DataSet consulta1 = npro.get_allReceta(); 
            DataTable DSconsulta = consulta1.Tables[0];
            ReportDataSource DSReceta = new ReportDataSource("DS_Receta", DSconsulta);
            ReportViewer1.LocalReport.DataSources.Add(DSReceta);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubReport_detalleInsumoCreado);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        void SubReport_detalleInsumoCreado(object sender, SubreportProcessingEventArgs e)
        {
            string codigo = e.Parameters["CODIGO"].Values[0].ToString();
            NCorpal_Produccion npro2 = new NCorpal_Produccion();
            DataSet tuplas = npro2.get_allRecetaInsumoCreado(codigo); ;
            DataTable dsRecetaInsumoCreado = tuplas.Tables[0];
            
            ReportDataSource ds = new ReportDataSource("DS_RecestaInsumoCreado", dsRecetaInsumoCreado);            
            e.DataSources.Add(ds);
            
        }

    }
}