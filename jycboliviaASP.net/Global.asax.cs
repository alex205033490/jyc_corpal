using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using Microsoft.Reporting.Map.WebForms.BingMaps;

namespace jycboliviaASP.net
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
        //ROUTING
        protected void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
                "Fcorpal_SolicitudesPedidoaCredito",
                "SolicitudesCredito123",
                "~/Presentacion/Fcorpal_SolicitudesPedidoaCredito.aspx"
                );
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}