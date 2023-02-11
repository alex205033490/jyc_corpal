using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using jycboliviaASP.net.Negocio;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ConsultaProducto_SolicitudEntrega : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(120) == false)
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

        private void consultadedatos()
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string fechadesde = convertidorFecha(tx_desdeFecha.Text);
            string fechahasta = convertidorFecha(tx_hastaFecha.Text);
            //  int codigoCobrador = Convert.ToInt32(dd_cobrador.SelectedValue.ToString());
            //  string nombreCobrador = dd_cobrador.SelectedItem.Text;

            if (dd_consulta.SelectedIndex > -1 && !fechadesde.Equals("null") && !fechahasta.Equals("null"))
            {
                if (dd_consulta.SelectedIndex == 0)
                {
                    get_datosProductosSolicitados(fechadesde, fechahasta);
                }

            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Datos incorrectos') </script>");
        }

        private void get_datosProductosSolicitados(string fechadesde, string fechahasta)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_DetalleProductosSolicitadosEntregados.rdlc";

            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            DataSet consulta1 = nss.get_alldetalleProductoSolicitudEntregado(fechadesde, fechahasta);
            DataTable DSconsulta = consulta1.Tables[0];

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);
            ReportDataSource DS_detalleproductosSolicitados = new ReportDataSource("DS_detalleproductosSolicitados", DSconsulta);

            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DS_detalleproductosSolicitados);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            consultadedatos();
        }
    }
}