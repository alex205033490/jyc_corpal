using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ReporteChequeBisa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string montoCheque = Session["montoCheque"].ToString();
                string lugarfechaCheque = Session["lugarfechaCheque"].ToString();
                string SumaletrasCheque = Session["SumaletrasCheque"].ToString();
                string pageseaCheque = Session["PageseACheque"].ToString();
                string relleno = Session["Relleno"].ToString();
                string departamento = Session["departamentoCheque"].ToString();
                string dia = Session["diaCheque"].ToString();
                string mes =  Session["mesCheque"].ToString();
                string anio = Session["anioCheque"].ToString();



                ReportParameter montoChequeCoti = new ReportParameter("p_monto", montoCheque);
                ReportParameter lugarfechaChequeCoti = new ReportParameter("p_lugarfecha", lugarfechaCheque);
                ReportParameter pageseaChequeCoti = new ReportParameter("p_pageseorden", pageseaCheque);
                ReportParameter SumaletrasChequeCoti = new ReportParameter("p_sumaletras", SumaletrasCheque);
                ReportParameter departamentocoti = new ReportParameter("p_departamento", departamento);
                ReportParameter rellenocoti = new ReportParameter("p_relleno", relleno);
                ReportParameter diacoti = new ReportParameter("p_dia", dia);
                ReportParameter mescoti = new ReportParameter("p_mes", mes);
                ReportParameter aniocoti = new ReportParameter("p_anio", anio);


                Report_chequebisa.LocalReport.SetParameters(montoChequeCoti);
                Report_chequebisa.LocalReport.SetParameters(lugarfechaChequeCoti);
                Report_chequebisa.LocalReport.SetParameters(pageseaChequeCoti);
                Report_chequebisa.LocalReport.SetParameters(SumaletrasChequeCoti);
                Report_chequebisa.LocalReport.SetParameters(rellenocoti);
                Report_chequebisa.LocalReport.SetParameters(departamentocoti);
                Report_chequebisa.LocalReport.SetParameters(diacoti);
                Report_chequebisa.LocalReport.SetParameters(mescoti);
                Report_chequebisa.LocalReport.SetParameters(aniocoti);

                this.Report_chequebisa.LocalReport.Refresh();
                this.Report_chequebisa.DataBind();
            }
        }
    }
}