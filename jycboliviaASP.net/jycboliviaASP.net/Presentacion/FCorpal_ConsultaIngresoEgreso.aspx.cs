using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ConsultaRepuesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(117) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {
              //  cargarCobrador();
            }
        }

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaResponsable2(string prefixText, int count)
        {
            string nombreResponsable = prefixText;

            NA_Responsables Nrespon = new NA_Responsables();
            DataSet tuplas = Nrespon.mostrarTodos_AutoComplit(nombreResponsable);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
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


        private void get_LibroDiarioIngreso(string fecha1, string fecha2, string responsable )
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_consultaReciboIngreso.rdlc";

            NA_Recibo_IngresoEgreso nre = new NA_Recibo_IngresoEgreso();
            DataSet consulta1 = nre.get_allreciboIngreso(fecha1, fecha2, responsable);
            DataTable DSconsulta = consulta1.Tables[0];

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);
            ReportDataSource DSLibroDiarioCobranzas = new ReportDataSource("DS_consultareciboingreso", DSconsulta);
            
            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DSLibroDiarioCobranzas);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            consultadedatos();
        }

        private void consultadedatos()
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string fechadesde = convertidorFecha(tx_desdeFecha.Text);
            string fechahasta = convertidorFecha(tx_hastaFecha.Text);
            string responsable = tx_responsable.Text;
            //  int codigoCobrador = Convert.ToInt32(dd_cobrador.SelectedValue.ToString());
          

            if (dd_consulta.SelectedIndex > -1 && !fechadesde.Equals("null") && !fechahasta.Equals("null") )
            {
                if (dd_consulta.SelectedIndex == 0)
                {
                    get_LibroDiarioIngreso(fechadesde, fechahasta, responsable);
                }

                if (dd_consulta.SelectedIndex == 1)
                {
                    get_LibroDiarioEgreso(fechadesde, fechahasta, responsable);
                }

            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Datos incorrectos') </script>");
        }

        private void get_LibroDiarioEgreso(string fechadesde, string fechahasta, string responsable)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_ConsultaReciboEgreso.rdlc";

            NA_Recibo_IngresoEgreso nre = new NA_Recibo_IngresoEgreso();
            DataSet consulta1 = nre.get_allreciboEgreso(fechadesde, fechahasta, responsable);
            DataTable DSconsulta = consulta1.Tables[0];

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);
            ReportDataSource DSLibroDiarioCobranzas = new ReportDataSource("DS_ConsultaReciboEgreso", DSconsulta);

            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DSLibroDiarioCobranzas);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }
    }
}