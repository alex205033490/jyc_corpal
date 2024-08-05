using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
using jycboliviaASP.net.Negocio;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ConsultaSGI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(99) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            /*   if (!IsPostBack)
               {
                   DateTime fechaNow = DateTime.Now;
                   int mes = fechaNow.Month;
                   int anio = fechaNow.Year;

                   dd_mes.SelectedIndex = mes - 1;
                   tx_anio.Text = anio.ToString();
               }*/
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


        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            consutadedatos();
        }

        private void consutadedatos()
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;

            if (dd_consulta.SelectedIndex > -1)
            {
                if (dd_consulta.SelectedIndex == 0)
                {
                }
                if (dd_consulta.SelectedIndex == 1)
                {
                    get_ActividadesPersonales_Todas();
                }
            }
        }

        private void get_ActividadesPersonales_Todas()
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_ActividadesPersonal.rdlc";

            string fechadesde = convertidorFecha(tx_desdeFecha.Text);
            string fechahasta = convertidorFecha(tx_hastaFecha.Text);
            string personal = tx_personal.Text;

            if (!fechadesde.Equals("null") && !fechahasta.Equals("null"))
            {
                NA_ActividadesPersonal nactividad = new NA_ActividadesPersonal();
                DataSet consulta1 = nactividad.get_ActividadesConsultaTodas(personal, fechadesde, fechahasta);
                DataTable DSconsulta = consulta1.Tables[0];


                ReportDataSource DS_BoletasEmergencia_todas = new ReportDataSource("DS_ActividadesPersonal", DSconsulta);

                ReportViewer1.LocalReport.DataSources.Add(DS_BoletasEmergencia_todas);
                this.ReportViewer1.LocalReport.Refresh();
                this.ReportViewer1.DataBind();
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccione Fechas') </script>");

        }
    }
}