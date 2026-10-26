using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Script.Serialization;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FACorpal_dashboard_Ventas : System.Web.UI.Page
    {
        // Propiedades públicas expuestas hacia el frontend (.aspx)
        public string DatosJsonParaFrontend { get; set; } = "[]";
        public string ObjetivosJsonParaFrontend { get; set; } = "[]";
        public decimal TotalOrigenGeneral { get; set; } = 0;
        public int TotalFilasOrigen { get; set; } = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();


            if (tienePermiso(157) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                CargarDatosParaMostrar();
                CargarObjetivosVentasVendedor();
            }
        }

        private bool tienePermiso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usu = Session["NameUser"].ToString();
            string pass = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usu, pass);

            NA_DetallePermiso Nper = new NA_DetallePermiso();
            return Nper.tienePermisoResponsable(permiso, codUser);
        }

        private void CargarDatosParaMostrar()
        {
            try
            {
                NCorpal_EntregaSolicitudProducto2 nda = new NCorpal_EntregaSolicitudProducto2();
                DataSet datosR = nda.get_VentasEntregaCliente();

                if (datosR != null && datosR.Tables.Count > 0 && datosR.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = datosR.Tables[0];
                    TotalFilasOrigen = dt.Rows.Count;
                    var listaFilas = new List<Dictionary<string, object>>();

                    foreach (DataRow row in dt.Rows)
                    {
                        var dict = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            string nombreCol = col.ColumnName.Trim();

                            if (nombreCol.Equals("Fecha", StringComparison.OrdinalIgnoreCase) && row[col] != DBNull.Value)
                            {
                                dict[col.ColumnName] = Convert.ToDateTime(row[col]).ToString("yyyy-MM-dd");
                            }
                            // Evaluamos tanto "Total" como "Total (Bs)" permitiendo variaciones
                            else if ((nombreCol.Equals("Total", StringComparison.OrdinalIgnoreCase) ||
                                      nombreCol.Equals("Total (Bs)", StringComparison.OrdinalIgnoreCase)) && row[col] != DBNull.Value)
                            {
                                decimal valTotal = Convert.ToDecimal(row[col]);
                                TotalOrigenGeneral += valTotal;
                                dict[col.ColumnName] = valTotal;
                            }
                            else
                            {
                                dict[col.ColumnName] = row[col] == DBNull.Value ? "" : row[col];
                            }
                        }
                        listaFilas.Add(dict);
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    serializer.MaxJsonLength = int.MaxValue;
                    DatosJsonParaFrontend = serializer.Serialize(listaFilas);
                }
                else
                {
                    DatosJsonParaFrontend = "[]";
                }
            }
            catch (Exception)
            {
                DatosJsonParaFrontend = "[]";
            }
        }

        private void CargarObjetivosVentasVendedor()
        {
            try
            {
                NCorpal_Objetivos nobjetivo = new NCorpal_Objetivos();
                DataSet datosR = nobjetivo.get_ObjetivosVentasVendedor();

                if (datosR != null && datosR.Tables.Count > 0 && datosR.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = datosR.Tables[0];
                    var listaObjetivos = new List<Dictionary<string, object>>();

                    foreach (DataRow row in dt.Rows)
                    {
                        var dict = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            dict[col.ColumnName] = row[col] == DBNull.Value ? "" : row[col];
                        }
                        listaObjetivos.Add(dict);
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    serializer.MaxJsonLength = int.MaxValue;
                    ObjetivosJsonParaFrontend = serializer.Serialize(listaObjetivos);
                }
                else
                {
                    ObjetivosJsonParaFrontend = "[]";
                }
            }
            catch (Exception)
            {
                ObjetivosJsonParaFrontend = "[]";
            }
        }
    }
}