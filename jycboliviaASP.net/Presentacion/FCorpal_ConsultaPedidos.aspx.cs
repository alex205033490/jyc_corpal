using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.Globalization;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ConsultaPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            this.Title = Session["BaseDatos"].ToString();
            /*
            if (tienePermisoDeIngreso() == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            */
            if (!IsPostBack)
            {
                rw_consultaPedidos.Visible = false;
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

        /*CARGAR RESPONSABLES*/
        [WebMethod]
        [ScriptMethod]
        public static string[] getListResponsable(string prefixText, int count)
        {
            string nombre = prefixText;
            NA_Responsables nResp = new NA_Responsables();
            DataSet tuplas = nResp.mostrarTodosDatos2(nombre);

            int fin = tuplas.Tables[0].Rows.Count;
            string[] lista = new string[fin];

            for (int i = 0; i < fin; i++)
            {
                string cod = tuplas.Tables[0].Rows[i]["codigo"].ToString();
                string nomResp = tuplas.Tables[0].Rows[i]["nombre"].ToString();

                lista[i] = $"{cod} - {nomResp}";

            }
            return lista;
        }

        protected void btn_buscarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                rw_consultaPedidos.LocalReport.DataSources.Clear();

                if(dd_consulta.SelectedIndex == 0)
                {
                    showalert("Seleccione una opción de consulta válida.");
                    return;
                }
                
                if(string.IsNullOrWhiteSpace(tx_fdesde.Text) || string.IsNullOrWhiteSpace(tx_fhasta.Text))
                {
                    showalert("Debe ingresar las fechas válidas.");
                    return;
                }

               
                if (dd_consulta.SelectedIndex == 1)
                {
                    get_productosSobrantes_OrdenEntrega();
                }
                else
                {
                    showalert("Seleccione una opcion de busqueda válida.");
                }
            }
            catch(Exception ex)
            {
                showalert("Error datos incorrectos. " + ex.Message);
            }
        }

        /* CONSULTA MOSTRAR PRODUCTOS SOBRANTES DE ORDEN DE ENTREGA */
        private void get_productosSobrantes_OrdenEntrega()
        {
            try
            {
                DateTime fecha1 = DateTime.ParseExact(tx_fdesde.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime fecha2 = DateTime.ParseExact(tx_fhasta.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                rw_consultaPedidos.Visible = true;

                int codChofer = 0;
                int.TryParse(hf_codConductor.Value, out codChofer);

                LocalReport localreport = rw_consultaPedidos.LocalReport;
                //localreport.ReportPath = "Reportes/Report_ConsultaProductosSobrantes_ordenEntrega.rdlc";

                NCorpal_Venta nventa = new NCorpal_Venta();
                DataSet consulta = nventa.get_mostrarProductosSobrantes_OrdenEntrega(fecha1, fecha2, codChofer);
                DataTable DSconsulta = consulta.Tables[0];

                if (DSconsulta.Rows.Count == 0)
                {
                    showalert("No hay datos para mostrar.");
                    return;
                }

                ReportParameter p_fecha1 = new ReportParameter("pfechadesde", tx_fdesde.Text.Trim());
                ReportParameter p_fecha2 = new ReportParameter("pfechahasta", tx_fhasta.Text.Trim());

                rw_consultaPedidos.LocalReport.DataSources.Clear();

                ReportDataSource DS_productosSobrante_ordenEntrega = new ReportDataSource("DS_productosSobrantesOrdenEntrega" +
                    "", DSconsulta);

                rw_consultaPedidos.LocalReport.SetParameters(p_fecha1);
                rw_consultaPedidos.LocalReport.SetParameters(p_fecha2);
                rw_consultaPedidos.LocalReport.DataSources.Clear();
                rw_consultaPedidos.LocalReport.DataSources.Add(DS_productosSobrante_ordenEntrega);
                this.rw_consultaPedidos.LocalReport.Refresh();
                this.rw_consultaPedidos.DataBind();

            }
            catch(Exception ex)
            {
                showalert("Error en el metodo para obtener datos. " + ex.Message);
            }
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

        private void showalert(string mensaje)
        {
            string script = $"alert(' {mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }
    }
}