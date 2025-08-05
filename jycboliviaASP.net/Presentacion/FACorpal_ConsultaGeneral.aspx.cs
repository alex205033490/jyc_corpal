using jycboliviaASP.net.Datos;
using jycboliviaASP.net.Negocio;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ConsultaGeneral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
          /*  if (tienePermisoDeIngreso(120) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }*/
            if (!IsPostBack)
            {
                //  cargarCobrador();
            }

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
              new ScriptResourceDefinition
              {
                  Path = "~/Scripts/jquery-3.7.1.min.js",        // Ruta local
                  DebugPath = "~/Scripts/jquery-3.7.1.js",        // Ruta para modo Debug
                  CdnPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js", // Opcional
                  CdnDebugPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.js",
                  CdnSupportsSecureConnection = true
              });
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


        public string convertidorFecha2(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "-" + mes + "-" + dia;
                return _fecha;
            }
            else
                return "null";
        }


        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            consultadedatos();
        }

        private void consultadedatos()
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            DateTime fechadesde;
            DateTime fechahasta;

            bool fechaDesdeOk = DateTime.TryParseExact(
                tx_desdeFecha.Text,
                "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out fechadesde
            );

            bool fechaHastaOk = DateTime.TryParseExact(
                tx_hastaFecha.Text,
                "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out fechahasta
            );

            TimeSpan horadesde;
            TimeSpan horahasta;

            bool horadesdeOk = TimeSpan.TryParseExact(
                txtHoradesde.Text,
                @"hh\:mm",
                CultureInfo.InvariantCulture,                
                out horadesde);

            bool horahastaOk = TimeSpan.TryParseExact(
                txtHorahasta.Text,
                @"hh\:mm",
                CultureInfo.InvariantCulture,                
                out horahasta);

            if (dd_consulta.SelectedIndex > -1 && fechaDesdeOk && fechaHastaOk)
            {
                string desdeStr = fechadesde.ToString("yyyy-MM-dd");
                string hastaStr = fechahasta.ToString("yyyy-MM-dd");

                switch (dd_consulta.SelectedIndex)
                {
                    case 0:
                        get_AlmuerzoCorrespondiente(desdeStr, hastaStr);
                        break;
                    case 1:
                        get_MarcacionPersonal(fechadesde, fechahasta, horadesde, horahasta);
                        break;
                    // Puedes agregar más casos si hay más opciones en tu DropDownList
                    default:
                        Response.Write("<script type='text/javascript'> alert('Opción no reconocida') </script>");
                        break;
                }
            }
            else
            {
                Response.Write("<script type='text/javascript'> alert('Error: Datos incorrectos') </script>");
            }
        }

        private void get_MarcacionPersonal(DateTime desdeStr, DateTime hastaStr, TimeSpan horaDesde, TimeSpan horaHasta)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_Marcacion.rdlc";

            DateTime fechaHoraDesdeCompleta = desdeStr.Date + horaDesde;
            DateTime fechaHoraHastaCompleta = hastaStr.Date + horaHasta;

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_MarcacionPersonal(fechaHoraDesdeCompleta, fechaHoraHastaCompleta);
            DataTable DSconsulta = consulta1.Tables[0];

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);
            ReportDataSource DS_detalleproductosSolicitados = new ReportDataSource("DS_Marcacion", DSconsulta);

            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DS_detalleproductosSolicitados);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

        private void get_AlmuerzoCorrespondiente(string fechadesde, string fechahasta)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_CorrespondeAlmuerzoFichada.rdlc";

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_CorrespondeAlmuerzoFichada(fechadesde, fechahasta);
            DataTable DSconsulta = consulta1.Tables[0];

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);
            ReportDataSource DS_detalleproductosSolicitados = new ReportDataSource("DS_Fichada", DSconsulta);

            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DS_detalleproductosSolicitados);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();            
        }
    }
}