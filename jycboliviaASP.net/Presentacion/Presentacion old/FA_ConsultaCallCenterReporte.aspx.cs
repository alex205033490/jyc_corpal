using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ConsultaCallCenterReporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            buscarPorConsulta();

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


        private void buscarPorConsulta()
        {
            if (!tx_fechaDesde.Text.Equals("") && !tx_fechahasta.Text.Equals(""))
            {
                int IdConsulta = dd_consulta.SelectedIndex;
                
                if (IdConsulta == 0)
                {                    
                    consultaReporte1();
                }
            }
        }

        private void consultaReporte1()
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_CallCenter_AscensoresParado.rdlc";

            NA_Evento evento = new NA_Evento();            
            DataSet consulta1 = evento.getEventoConAscensorParado2(convertidorFecha(tx_fechaDesde.Text), convertidorFecha(tx_fechahasta.Text), Session["BaseDatos"].ToString());             
            DataTable DSconsulta = consulta1.Tables[0];

            ReportDataSource DSAscensoresParado = new ReportDataSource("DSAscensoresParados", DSconsulta);

            ReportViewer1.LocalReport.DataSources.Add(DSAscensoresParado);            
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }



    }
}