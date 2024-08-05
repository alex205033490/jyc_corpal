using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using jycboliviaASP.net.Negocio;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ReporteSolicitudProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                int codigoSolicitudProducto = Convert.ToInt32(Session["codigoSolicitudProducto"].ToString());
                mostrarSolicitudProducto(codigoSolicitudProducto);
            }
        }

        public string convertidorFechaLetras(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = fecha_.ToString("dd MMMM yyy", CultureInfo.CreateSpecificCulture("es-ES"));
                return _fecha;
            }
            else
                return "null";
        }

        private void mostrarSolicitudProducto(int codigoSolicitudProducto)
        {
            NCorpal_SolicitudEntregaProducto nie = new NCorpal_SolicitudEntregaProducto();
            DataSet datoResult = nie.get_entregaSolicitudProductos(codigoSolicitudProducto);
                        
            string nrodocumento = datoResult.Tables[0].Rows[0][1].ToString();
            string fechasolicitud = datoResult.Tables[0].Rows[0][2].ToString();
            string horasolicitud = datoResult.Tables[0].Rows[0][3].ToString();
            string nombresolicitante = datoResult.Tables[0].Rows[0][4].ToString();


            DataSet tuplasFilas = nie.get_productosSolicitudProducto(codigoSolicitudProducto);
            DataTable DSProductosAlmacen = tuplasFilas.Tables[0];

            ReportParameter p_nrocomprobante = new ReportParameter("p_nrodocumento", nrodocumento);
            ReportParameter p_fechasolicitud = new ReportParameter("p_fechasolicitud", fechasolicitud);
            ReportParameter p_nombresolicitante = new ReportParameter("p_nombresolicitante", nombresolicitante);
            ReportParameter p_horasolicitud = new ReportParameter("p_horasolicitud", horasolicitud);
            ReportDataSource DS_ProductosAlmacen = new ReportDataSource("DS_ProductosAlmacen", DSProductosAlmacen);

            string rutaEntregaSolicitudProducto = ConfigurationManager.AppSettings["repo_SolicitudProducto"];

            ReportViewer1.LocalReport.ReportPath = rutaEntregaSolicitudProducto;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            //viewer.LocalReport.Refresh();      

            ReportViewer1.LocalReport.SetParameters(p_nrocomprobante);
            ReportViewer1.LocalReport.SetParameters(p_fechasolicitud);
            ReportViewer1.LocalReport.SetParameters(p_horasolicitud);
            ReportViewer1.LocalReport.SetParameters(p_nombresolicitante);            
            ReportViewer1.LocalReport.DataSources.Add(DS_ProductosAlmacen);

            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
            
        }
    }
}