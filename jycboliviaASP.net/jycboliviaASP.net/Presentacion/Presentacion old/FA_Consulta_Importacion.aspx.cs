using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_Consulta_Importacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(72) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

           
         
            if (!IsPostBack)
            {
                cargarConsultas();
            }
            //  habilitar();
        }

        private void cargarConsultas()
        {   
            dd_consulta.DataValueField = "codigo";
            dd_consulta.DataTextField = "nombre";
            dd_consulta.Items.Add(new ListItem("--Ninguno--", "-1"));
            dd_consulta.Items.Add(new ListItem("Cuadro Costos Polizas Importacion", "1"));
            dd_consulta.Items.Add(new ListItem("Seguros Credin Form", "2"));  
            if (tienePermisoDeIngreso(97) == true)                      
                  dd_consulta.Items.Add(new ListItem("Seguros Credin Form (Contable)", "3"));
            dd_consulta.SelectedIndex = -1;
            dd_consulta.DataBind();
        }
        

        private void borrarConsultaContable()
        {
            dd_consulta.Items[3].Enabled = false;
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
            string NroDui = tx_Dui.Text;
            string razonSocial = tx_RazonSocial.Text;
            string fechadesde = convertidorFecha(tx_fechadesde.Text);
            string fechahasta = convertidorFecha(tx_fechahasta.Text);
            int codigo;
            int.TryParse(dd_consulta.SelectedValue.ToString(), out codigo);

            if (dd_consulta.SelectedIndex > -1)
            {
                if (codigo == 1)
                {
                    //get_CodigoEquipoEdificio(NroDui, razonSocial);
                    get_CodigoEquipoEdificio_Mejorado(NroDui, razonSocial);
                }

                if (codigo == 2)
                {
                    get_consultaImportacionSeguro2(fechadesde, fechahasta);
                }

                if (codigo == 3)
                {
                    get_consultaSeguroCredinForm(fechadesde, fechahasta);
                }

            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Datos incorrectos') </script>");
        }


        private void get_consultaImportacionSeguro2(string fechadesde, string fechahasta)
        {
            LocalReport localreport = ReportViewer1.LocalReport;

            NA_Importacion Nimp = new NA_Importacion();
            DataSet tuplas = Nimp.get_SegurosCredinForm2(fechadesde, fechahasta);
            DataTable dsImportacion = tuplas.Tables[0];

            //report
            //reset
            // ReportViewer1.Reset();
            //parth
            localreport.ReportPath = "Reportes/Report_ImportacionSeguros2.rdlc";
            //data source
            ReportDataSource DS_ImportacionCostos = new ReportDataSource("DS_SeguroCredinFrom", dsImportacion);
            ReportViewer1.LocalReport.DataSources.Add(DS_ImportacionCostos);
            //refresh
            this.ReportViewer1.LocalReport.Refresh();
            // this.ReportViewer1.DataBind();
        }

        private void get_consultaSeguroCredinForm(string fechadesde, string fechahasta)
        {
            LocalReport localreport = ReportViewer1.LocalReport;

            NA_Importacion Nimp = new NA_Importacion();
            DataSet tuplas = Nimp.get_SegurosCredinForm(fechadesde,fechahasta);
            DataTable dsImportacion = tuplas.Tables[0];            

            //report
            //reset
           // ReportViewer1.Reset();
            //parth
            localreport.ReportPath = "Reportes/Report_ImportacionCredinForm.rdlc";
            //data source
            ReportDataSource DS_ImportacionCostos = new ReportDataSource("DS_SeguroCredinFrom", dsImportacion);
            ReportViewer1.LocalReport.DataSources.Add(DS_ImportacionCostos);            
            //refresh
            this.ReportViewer1.LocalReport.Refresh();
           // this.ReportViewer1.DataBind();
        }

        private void get_CodigoEquipoEdificio_Mejorado(string NroDui, string razonSocial)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            NA_CostosPolizaImportacion Npoliza = new NA_CostosPolizaImportacion();
            DataSet tuplas = Npoliza.get_ALLPolizasCostos_Mejorado(NroDui, razonSocial);
            DataTable dsImportacion = tuplas.Tables[0];            

            //report
            //reset
           // ReportViewer1.Reset();
            //parth
            localreport.ReportPath = "Reportes/Report_PolizasImportacionCostos2.rdlc";
            //data source
            ReportDataSource DS_ImportacionCostos = new ReportDataSource("DS_PolizasCostos", dsImportacion);
            ReportViewer1.LocalReport.DataSources.Add(DS_ImportacionCostos);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(detalle_facturaProveedoresComercialSubReportProcessing);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(detalle_facturaEquipoSeguroSubReportProcessing);
            //refresh
            this.ReportViewer1.LocalReport.Refresh();
           // this.ReportViewer1.DataBind();
                        
        }

        void detalle_facturaProveedoresComercialSubReportProcessing(object sender, SubreportProcessingEventArgs e)
        {

            string codnrodui = e.Parameters["nrodui"].Values[0].ToString();
            NA_CostosPolizaImportacion Npoliza = new NA_CostosPolizaImportacion();
            DataSet tuplas = Npoliza.get_detalleFacturaProovedoresComercial_DUI(codnrodui);
            DataTable dsImportacion = tuplas.Tables[0];
          //  if (tuplas.Tables[0].Rows.Count > 0)
          //  {
                ReportDataSource ds = new ReportDataSource("DS_facturaproveedorescomercial", dsImportacion);
                e.DataSources.Add(ds);
           // }
        }
         void detalle_facturaEquipoSeguroSubReportProcessing(object sender, SubreportProcessingEventArgs e) {
            
            string codnrodui = e.Parameters["nrodui"].Values[0].ToString();            
            NA_CostosPolizaImportacion Npoliza = new NA_CostosPolizaImportacion();
            DataSet tuplas = Npoliza.get_detalleSeguroEquipo(codnrodui);
            DataTable dsImportacion = tuplas.Tables[0];
           // if(tuplas.Tables[0].Rows.Count > 0){
                ReportDataSource ds = new ReportDataSource("DS_detalleSeguroEquipo", dsImportacion);
            e.DataSources.Add(ds);
          //  }
            
        }

        private void get_CodigoEquipoEdificio(string NroDui, string RazonSocial)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_PolizasImportacionCostos.rdlc";

            NA_CostosPolizaImportacion Npoliza = new NA_CostosPolizaImportacion();
            DataSet tuplas = Npoliza.get_ALLPolizasCostos(NroDui);
            DataTable dsImportacion = tuplas.Tables[0];

            ReportDataSource DS_ImportacionCostos = new ReportDataSource("DS_ImportacionCostos", dsImportacion);

            ReportViewer1.LocalReport.DataSources.Add(DS_ImportacionCostos);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

    }
}