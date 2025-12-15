using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_AsignacionRuta_Camion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (!IsPostBack)
            {
                cargarVehiculosDD();
            }
        }

        // Cargar datos Vehiculo DD
        private void cargarVehiculosDD()
        {
            NCorpal_Vehiculos Nve = new NCorpal_Vehiculos();
            DataSet dsCar = Nve.get_showVehiculoDD();

            if(dsCar != null && dsCar.Tables.Count > 0)
            {
                dd_listVehiculo.DataSource = dsCar.Tables[0];
                dd_listVehiculo.DataTextField = "detalle";
                dd_listVehiculo.DataValueField = "codigo";

                dd_listVehiculo.DataBind();
                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Seleccione un vehiculo", "0");
                dd_listVehiculo.Items.Insert(0, li);
            }
        }

    }
}