using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using jycboliviaASP.net.Negocio;
using Microsoft.Reporting.WebForms;
using LocalReport = Microsoft.Reporting.WebForms.LocalReport;
using ReportDataSource = Microsoft.Reporting.WebForms.ReportDataSource;
using ReportParameter = Microsoft.Reporting.WebForms.ReportParameter;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ConsultaProducto_SolicitudEntrega : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (!tienePermisoDeIngreso(120))
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                // cargarCobrador();
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string[] GetlistaResponsable2(string prefixText, int count)
        {
            NA_Responsables Nrespon = new NA_Responsables();
            DataSet tuplas = Nrespon.mostrarTodos_AutoComplit(prefixText);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            for (int i = 0; i < tuplas.Tables[0].Rows.Count; i++)
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            return lista;
        }

        [WebMethod]
        [ScriptMethod]
        public static string[] GetListClientes(string prefixText, int count)
        {
            NCorpal_Cliente nCli = new NCorpal_Cliente();
            DataSet tuplas = nCli.listarTiendas(prefixText);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            for (int i = 0; i < tuplas.Tables[0].Rows.Count; i++)
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            return lista;
        }

        [WebMethod]
        [ScriptMethod]
        public static string[] GetlistaProductos(string prefixText, int count)
        {
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos(prefixText);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            for (int i = 0; i < tuplas.Tables[0].Rows.Count; i++)
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
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
            if (!string.IsNullOrEmpty(fecha))
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                return $"'{fecha_.Year}/{fecha_.Month}/{fecha_.Day}'";
            }
            return "null";
        }

        private void consultadedatos()
        {
            try
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                string fechadesde = convertidorFecha(tx_desdeFecha.Text);
                string fechahasta = convertidorFecha(tx_hastaFecha.Text);
                string Responsable = tx_responsable.Text;
                string producto = tx_producto.Text;

                if (dd_consulta.SelectedIndex == 4 && !fechahasta.Equals("null"))
                {
                    get_StockProducctos(fechahasta);
                }
                else if (dd_consulta.SelectedIndex > -1 && !fechadesde.Equals("null") && !fechahasta.Equals("null"))
                {
                    switch (dd_consulta.SelectedIndex)
                    {
                        case 0:
                            get_datosProductosSolicitados(fechadesde, fechahasta, Responsable);
                            break;
                        case 1:
                            get_datosProductosSolicitados_VS_entregados(fechadesde, fechahasta);
                            break;
                        case 2:
                            get_datosSolicitadoEntregadoProducto_porPersona(fechadesde, fechahasta, Responsable, producto);
                            break;
                        case 3:
                            get_datosEntregaProduccion(fechadesde, fechahasta, Responsable, producto);
                            break;
                        case 5:
                            get_detalleEntregaSolicitudProductos(fechadesde, fechahasta, Responsable);
                            break;
                        case 6:
                            get_calidadnachosproceso_remojadoyLavado(fechadesde, fechahasta, Responsable);
                            break;
                        case 7:
                            get_calidadnachosproceso_Molinos(fechadesde, fechahasta, Responsable);
                            break;
                        case 8:
                            get_calidadnachosproceso_Formadora(fechadesde, fechahasta, Responsable);
                            break;
                        case 9:
                            get_calidadnachosproceso_Fritadora(fechadesde, fechahasta, Responsable);
                            break;
                        case 10:
                            get_calidadnachosproceso_SazonadoControlSensorial(fechadesde, fechahasta, Responsable);
                            break;
                        case 11:
                            get_calidadnachosproceso_Envasadora(fechadesde, fechahasta, Responsable);
                            break;
                        case 12:
                            get_ReporteSolicitudEntregaProductos_DespachoVenta(Responsable);
                            break;
                        default:
                            showalert("Opción de consulta no válida");
                            break;
                    }
                }
                else
                {
                    Response.Write("<script type='text/javascript'> alert('Error: Datos incorrectos') </script>");
                }
            }
            catch (Exception ex)
            {
                showalert("Error al obtener el reporte. " + ex.Message);
            }
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            consultadedatos();
        }

        private void showalert(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
        }

        // ------------------------ MÉTODOS DE REPORTES ------------------------

        private void get_ReporteSolicitudEntregaProductos_DespachoVenta(string vendedor)
        {
            try
            {
                DateTime fechaDesde, fechaHasta;

                if (!DateTime.TryParse(tx_desdeFecha.Text, out fechaDesde) ||
                    !DateTime.TryParse(tx_hastaFecha.Text, out fechaHasta))
                {
                    showalert("Formato de fecha inválido");
                    return;
                }

                string cliente = tx_cliente.Text.Trim();
                LocalReport localreport = ReportViewer1.LocalReport;
                localreport.ReportPath = "Reportes/Report_ConsultaEntregaProducto_DespachoVenta.rdlc";

                NCorpal_EntregaSolicitudProducto2 nes = new NCorpal_EntregaSolicitudProducto2();
                DataSet consulta1 = nes.GET_ReportSolicitudEntregaProducto(fechaDesde, fechaHasta, vendedor, cliente);
                DataTable dtConsulta = consulta1.Tables[0];

                ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
                ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);
                ReportDataSource DS_solicitudEntregaProducto = new ReportDataSource("DS_entregaProdSalida", dtConsulta);

                ReportViewer1.LocalReport.SetParameters(p_fecha1);
                ReportViewer1.LocalReport.SetParameters(p_fecha2);
                ReportViewer1.LocalReport.DataSources.Add(DS_solicitudEntregaProducto);
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.DataBind();
            }
            catch (Exception ex)
            {
                showalert("Error en el metodo al obtener los datos. " + ex.Message);
            }
        }

        private void get_SolicitadoyEntregado(string fechadesde, string fechahasta, string responsable, string producto)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_SolicitadoEntregado.rdlc";

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_SolicitadoEntregado(fechadesde, fechahasta, responsable, producto);
            DataTable DSconsulta = consulta1.Tables[0];

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);
            ReportDataSource DS_EntregadoSolicitado = new ReportDataSource("DS_EntregadoSolicitado", DSconsulta);

            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DS_EntregadoSolicitado);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        private void get_calidadnachosproceso_Envasadora(string fechadesde, string fechahasta, string responsable)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_CalidadEnvasadora.rdlc";

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_calidadNachosProceso_Envasadora(fechadesde, fechahasta);
            ReportDataSource DS_StockProduccto = new ReportDataSource("DS_RemojadoyLavado", consulta1.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Add(DS_StockProduccto);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        private void get_calidadnachosproceso_SazonadoControlSensorial(string fechadesde, string fechahasta, string responsable)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_CalidadSazonadoControlSensorial.rdlc";

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_calidadNachosProceso_SazonadoControlSensorial(fechadesde, fechahasta);
            ReportDataSource DS_StockProduccto = new ReportDataSource("DS_RemojadoyLavado", consulta1.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Add(DS_StockProduccto);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        private void get_calidadnachosproceso_Fritadora(string fechadesde, string fechahasta, string responsable)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_CalidadFritadora.rdlc";

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_calidadNachosProceso_Fritadora(fechadesde, fechahasta);
            ReportDataSource DS_StockProduccto = new ReportDataSource("DS_RemojadoyLavado", consulta1.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Add(DS_StockProduccto);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        private void get_calidadnachosproceso_Formadora(string fechadesde, string fechahasta, string responsable)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_CalidadFormadora.rdlc";

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_calidadNachosProceso_Formadora(fechadesde, fechahasta);
            ReportDataSource DS_StockProduccto = new ReportDataSource("DS_RemojadoyLavado", consulta1.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Add(DS_StockProduccto);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        private void get_calidadnachosproceso_Molinos(string fechadesde, string fechahasta, string responsable)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_CalidadMolinos.rdlc";

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_calidadNachosProceso_Molinos(fechadesde, fechahasta);
            ReportDataSource DS_StockProduccto = new ReportDataSource("DS_RemojadoyLavado", consulta1.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Add(DS_StockProduccto);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        private void get_calidadnachosproceso_remojadoyLavado(string fechadesde, string fechahasta, string responsable)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_CalidadRemojadoyLavado.rdlc";

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_calidadNachosProceso_RemojadoyLavado(fechadesde, fechahasta);
            ReportDataSource DS_StockProduccto = new ReportDataSource("DS_RemojadoyLavado", consulta1.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Add(DS_StockProduccto);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        private void get_detalleEntregaSolicitudProductos(string fechadesde, string fechahasta, string Responsable)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_DetalleEntregaSolicitudProductos.rdlc";

            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            DataSet consulta1 = nss.get_detalleEntregaSolicitudProductos(fechadesde, fechahasta);
            ReportDataSource DS_StockProduccto = new ReportDataSource("DS_DetalleEntregaSolicitudProductos", consulta1.Tables[0]);

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);

            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DS_StockProduccto);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        private void get_StockProducctos(string fechaHasta)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_StockProductos.rdlc";

            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            DataSet consulta1 = nss.get_StockProducctos(fechaHasta);
            ReportDataSource DS_StockProduccto = new ReportDataSource("DS_StockProductos", consulta1.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Add(DS_StockProduccto);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        private void get_datosSolicitadoEntregadoProducto_porPersona(string fechadesde, string fechahasta, string Responsable, string producto)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_Detalle_solicitudEntregaProductoporPersona.rdlc";

            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            DataSet consulta1 = nss.get_alldetalleProductoSolicitadosyEntregadosporpersona(fechadesde, fechahasta, Responsable);
            ReportDataSource DS_SolicitudEntregaProducto_PorPersona = new ReportDataSource("DS_SolicitudEntregaProducto_PorPersona", consulta1.Tables[0]);

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);

            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DS_SolicitudEntregaProducto_PorPersona);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        private void get_datosEntregaProduccion(string fechadesde, string fechahasta, string Responsable, string producto)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_ConsultaEntregaProduccion.rdlc";

            NCorpal_Produccion nss = new NCorpal_Produccion();
            DataSet consulta1 = nss.get_entregasProduccion(fechadesde, fechahasta, Responsable, producto);
            ReportDataSource DS_detalleproductosSolicitados = new ReportDataSource("DS_ConsultaEntregaProduccion", consulta1.Tables[0]);

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);

            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DS_detalleproductosSolicitados);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        private void get_datosProductosSolicitados_VS_entregados(string fechadesde, string fechahasta)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_DetalleProductosSolicitados_VS_Entregados.rdlc";

            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            DataSet consulta1 = nss.get_DetalleProductosSolicitados_vs_entregados(fechadesde, fechahasta);
            ReportDataSource DS_detalleproductosSolicitados = new ReportDataSource("DS_DetalleProductosSolicitados_vs_Entregados", consulta1.Tables[0]);

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);

            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DS_detalleproductosSolicitados);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }

        private void get_datosProductosSolicitados(string fechadesde, string fechahasta, string responsable)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_DetalleProductosSolicitados.rdlc";

            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            DataSet consulta1 = nss.get_DetalleProductosSolicitados(fechadesde, fechahasta, responsable);
            ReportDataSource DS_detalleproductosSolicitados = new ReportDataSource("DS_DetalleProductosSolicitados", consulta1.Tables[0]);

            ReportParameter p_fecha1 = new ReportParameter("p_fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fecha2 = new ReportParameter("p_fechahasta", tx_hastaFecha.Text);

            ReportViewer1.LocalReport.SetParameters(p_fecha1);
            ReportViewer1.LocalReport.SetParameters(p_fecha2);
            ReportViewer1.LocalReport.DataSources.Add(DS_detalleproductosSolicitados);
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
        }
    }
}
