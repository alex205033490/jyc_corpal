using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ReporteSolicitudProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                int codigoSolicitudProducto = Convert.ToInt32(Session["codigoSolicitudProducto"].ToString());
                mostrarSolicitudProducto(codigoSolicitudProducto);
            }
        }

        public string convertidorFechaLetras(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = fecha_.ToString("dd MMMM yyy", CultureInfo.CreateSpecificCulture("es-ES"));
                return _fecha;
            }
            else
                return "null";
        }

        private void mostrarSolicitudProducto(int codigoSolicitudProducto)
        {
            throw new NotImplementedException();
        }
    }
}