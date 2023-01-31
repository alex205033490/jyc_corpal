using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_mostrarResultado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            lb_titulo.Text = "Resultado - " + Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                DataSet dato = (DataSet)Session["datoMostrar"];
                gvConsultas.DataSource = dato;
                gvConsultas.DataBind();
            
            }
        }
    }
}