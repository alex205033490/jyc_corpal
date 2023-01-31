using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ReporteChequeBNB : System.Web.UI.Page
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
                

                ReportParameter montoChequeCoti = new ReportParameter("p_monto", montoCheque);
                ReportParameter lugarfechaChequeCoti = new ReportParameter("p_lugarfecha", lugarfechaCheque);
                ReportParameter pageseaChequeCoti = new ReportParameter("p_pageseorden", pageseaCheque);
                ReportParameter SumaletrasChequeCoti = new ReportParameter("p_sumaletras", SumaletrasCheque);
                ReportParameter rellenocoti = new ReportParameter("p_relleno", relleno);

                Report_ChequeBNB.LocalReport.SetParameters(montoChequeCoti);
                Report_ChequeBNB.LocalReport.SetParameters(lugarfechaChequeCoti);
                Report_ChequeBNB.LocalReport.SetParameters(pageseaChequeCoti);
                Report_ChequeBNB.LocalReport.SetParameters(SumaletrasChequeCoti);
                Report_ChequeBNB.LocalReport.SetParameters(rellenocoti);

                this.Report_ChequeBNB.LocalReport.Refresh();
                this.Report_ChequeBNB.DataBind();
            }

        }
    }
}