using jycboliviaASP.net.Negocio;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ConsutaProduccion : System.Web.UI.Page
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

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProductos(string prefixText, int count)
        {
            string nombreProducto = prefixText;

            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos(nombreProducto);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;
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

        private void get_datosEntregaProduccion(string fechadesde, string fechahasta, string Responsable, string producto)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_ConsultaEntregaProduccion.rdlc";

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_entregasProduccion(fechadesde, fechahasta, Responsable, producto);
            DataTable DSconsulta = consulta1.Tables[0];

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);
            ReportDataSource DS_detalleproductosSolicitados = new ReportDataSource("DS_ConsultaEntregaProduccion", DSconsulta);

            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DS_detalleproductosSolicitados);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }
        private void consultadedatos()
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string fechadesde = convertidorFecha(tx_desdeFecha.Text);
            string fechahasta = convertidorFecha(tx_hastaFecha.Text);
            string Responsable = tx_responsable.Text;
            string producto = tx_producto.Text;

            if (dd_consulta.SelectedIndex > -1 && !fechadesde.Equals("null") && !fechahasta.Equals("null"))
            {
                if (dd_consulta.SelectedIndex == 0)
                {                
                    get_datosEntregaProduccion(fechadesde, fechahasta, Responsable, producto);
                }else
                  if (dd_consulta.SelectedIndex == 1)
                   {
                     get_datosEntregaProduccionFechaTurno(fechadesde, fechahasta, producto);
                   }else
                    if (dd_consulta.SelectedIndex == 2)
                        {
                            get_objetivoproduccion_vs_entregaproduccion_consalidaalmacen(fechadesde, fechahasta, producto);
                        }
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Datos incorrectos') </script>");
        }

        private void get_objetivoproduccion_vs_entregaproduccion_consalidaalmacen(string fechadesde, string fechahasta, string producto)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_Detalle_objetivoProduccion_Vs_EntregaProduccion.rdlc";

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_objetivoproduccion_vs_entregaproduccion_consalidaalmacen(fechahasta);
            DataTable DSconsulta = consulta1.Tables[0];

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);
            ReportDataSource DS_detalleproductosSolicitados = new ReportDataSource("DS_objetivoproduccion_vs_entregaproduccion_consalidaalmacen", DSconsulta);

            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DS_detalleproductosSolicitados);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void get_datosEntregaProduccionFechaTurno(string fechadesde, string fechahasta, string producto)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_ConsultaEntregaProduccionFechaTurno.rdlc";

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_datosEntregaProduccionFechaTurno(fechadesde, fechahasta, producto);
            DataTable DSconsulta = consulta1.Tables[0];

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);
            ReportDataSource DS_detalleproductosSolicitados = new ReportDataSource("DS_ConsultaEntregaProduccionFechaTurno", DSconsulta);

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