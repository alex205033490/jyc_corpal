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
    public partial class FA_ReporteCobranza2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                int codigoCobranza = Convert.ToInt32(Session["CodigoCobranza"].ToString());
                generarReporte(codigoCobranza);
            }
        }

        private void generarReporte(int codigoCobranza)
        {
            NA_Seguimiento nseg = new NA_Seguimiento();
            DataSet tuplasCobranza = nseg.get_cobranzadeldia(codigoCobranza);

            ReportParameter NroCobranza = new ReportParameter("NroCobranza", tuplasCobranza.Tables[0].Rows[0][2].ToString());
            ReportParameter DB_Datos = new ReportParameter("DB_Datos", Session["BaseDatos"].ToString());
            ReportParameter tipocambio = new ReportParameter("tipocambio", tuplasCobranza.Tables[0].Rows[0][3].ToString());
            ReportParameter moneda = new ReportParameter("moneda", tuplasCobranza.Tables[0].Rows[0][4].ToString());
            ReportParameter fechacobranza = new ReportParameter("fechacobranza", tuplasCobranza.Tables[0].Rows[0][1].ToString());
            ReportViewer1.LocalReport.SetParameters(NroCobranza);
            ReportViewer1.LocalReport.SetParameters(DB_Datos);
            ReportViewer1.LocalReport.SetParameters(tipocambio);
            ReportViewer1.LocalReport.SetParameters(moneda);
            ReportViewer1.LocalReport.SetParameters(fechacobranza);

            DataSet tuplasFilas = nseg.get_Recibo_cobranzadeldia(codigoCobranza);
            DataTable filasConsulta = tuplasFilas.Tables[0];

           
            DataTable datoRepuesto = new DataTable();
            datoRepuesto.Columns.Add("codigo_cliente", typeof(string));
            datoRepuesto.Columns.Add("edificio_exbo", typeof(string));
            datoRepuesto.Columns.Add("recibo", typeof(string));
            datoRepuesto.Columns.Add("fecha_pago", typeof(string));
            datoRepuesto.Columns.Add("banco", typeof(string));
            datoRepuesto.Columns.Add("nrocheque", typeof(string));
            datoRepuesto.Columns.Add("factura", typeof(string));
            datoRepuesto.Columns.Add("importe", typeof(string));
            datoRepuesto.Columns.Add("importe_Bs", typeof(string));
            datoRepuesto.Columns.Add("observaciones", typeof(string));
            float importeSUS = 0;
            float importeBs = 0;

            for (int i = 0; i < filasConsulta.Rows.Count; i++)
            {
                DataRow row = filasConsulta.Rows[i];
                DataRow tupla = datoRepuesto.NewRow();
                tupla["codigo_cliente"] = row[0].ToString();
                tupla["edificio_exbo"] = row[1].ToString();
                tupla["recibo"] = row[2].ToString();
                tupla["fecha_pago"] = row[3].ToString();
                tupla["banco"] = row[4].ToString();
                tupla["nrocheque"] = row[5].ToString();
                tupla["factura"] = row[6].ToString();
                tupla["importe"] = row[7].ToString();
                tupla["importe_Bs"] = row[8].ToString();
                tupla["observaciones"] = row[9].ToString();
                importeSUS = importeSUS + Single.Parse(row[7].ToString());
                importeBs = importeBs + Single.Parse(row[8].ToString());
                datoRepuesto.Rows.Add(tupla);
            }

            DataRow tupla1 = datoRepuesto.NewRow();
            tupla1["codigo_cliente"] = "";
            tupla1["edificio_exbo"] = "";
            tupla1["recibo"] = "";
            tupla1["fecha_pago"] = "";
            tupla1["banco"] = "";
            tupla1["nrocheque"] = "";
            tupla1["factura"] = "Total";
            tupla1["importe"] = importeSUS.ToString();
            tupla1["importe_Bs"] = importeBs.ToString();
            tupla1["observaciones"] = "";
            datoRepuesto.Rows.Add(tupla1);


            ReportDataSource DSRepuesto = new ReportDataSource("DS_Mantenimiento", datoRepuesto);
            ReportViewer1.LocalReport.DataSources.Add(DSRepuesto);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();



        }
    }
}