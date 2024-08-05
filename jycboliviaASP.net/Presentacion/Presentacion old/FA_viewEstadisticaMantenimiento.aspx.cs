using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_viewEstadisticaMantenimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(18) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 


            if (!IsPostBack)
            {
               cargarYearSeguimiento();
               // cargarMeses();
            }
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

       private void cargarYearSeguimiento()
        {
            NA_Seguimiento Nsegui = new NA_Seguimiento();
            DataSet datosCargar = Nsegui.getAllyearSeguimiento();
            dd_Gestion1.DataSource = datosCargar;
         //   dd_anio1.DataValueField = "codigo";
            dd_Gestion1.DataTextField = "years";
            dd_Gestion1.Items.Add(new ListItem("", "-1"));
            dd_Gestion1.AppendDataBoundItems = true;
        //    dd_anio1.SelectedIndex = -1;
            dd_Gestion1.DataBind();

            dd_Gestion2.DataSource = datosCargar;
           // dd_anio2.DataValueField = "codigo";
            dd_Gestion2.DataTextField = "years";
            dd_Gestion2.Items.Add(new ListItem("", "-1"));
            dd_Gestion2.AppendDataBoundItems = true;
         //   dd_anio2.SelectedIndex = -1;
            dd_Gestion2.DataBind();
        } 

     /*    private void cargarMeses() {

             dd_mes1.Items.Add(new ListItem("", "-1"));
             dd_mes1.Items.Add(new ListItem("Enero", "1"));
             dd_mes1.Items.Add(new ListItem("Febrero", "2"));
             dd_mes1.Items.Add(new ListItem("Marzo", "3"));
             dd_mes1.Items.Add(new ListItem("Abril", "4"));
             dd_mes1.Items.Add(new ListItem("Mayo", "5"));
             dd_mes1.Items.Add(new ListItem("Junio", "6"));
             dd_mes1.Items.Add(new ListItem("Julio", "7"));
             dd_mes1.Items.Add(new ListItem("Agosto", "8"));
             dd_mes1.Items.Add(new ListItem("Septiembre", "9"));
             dd_mes1.Items.Add(new ListItem("Octubre", "10"));
             dd_mes1.Items.Add(new ListItem("Noviembre", "11"));
             dd_mes1.Items.Add(new ListItem("Diciembre", "12"));
             dd_mes1.AppendDataBoundItems = true;
             dd_mes1.SelectedIndex = -1;
             dd_mes1.DataBind();

             dd_mes2.Items.Add(new ListItem("", "-1"));
             dd_mes2.Items.Add(new ListItem("Enero", "1"));
             dd_mes2.Items.Add(new ListItem("Febrero", "2"));
             dd_mes2.Items.Add(new ListItem("Marzo", "3"));
             dd_mes2.Items.Add(new ListItem("Abril", "4"));
             dd_mes2.Items.Add(new ListItem("Mayo", "5"));
             dd_mes2.Items.Add(new ListItem("Junio", "6"));
             dd_mes2.Items.Add(new ListItem("Julio", "7"));
             dd_mes2.Items.Add(new ListItem("Agosto", "8"));
             dd_mes2.Items.Add(new ListItem("Septiembre", "9"));
             dd_mes2.Items.Add(new ListItem("Octubre", "10"));
             dd_mes2.Items.Add(new ListItem("Noviembre", "11"));
             dd_mes2.Items.Add(new ListItem("Diciembre", "12"));
             dd_mes2.AppendDataBoundItems = true;
             dd_mes2.SelectedIndex = -1;
             dd_mes2.DataBind();
        }  */


        public string convertidorFecha(string fecha)
        {
            DateTime fecha_ = Convert.ToDateTime(fecha);
            int dia = fecha_.Day;
            int mes = fecha_.Month;
            int anio = fecha_.Year;
            string _fecha = anio + "/" + mes + "/" + dia;
            return _fecha;
        }

         private void primeraBusqueda() {
             string fecha1 = tx_Fecha11.Text;
             string fecha2 = tx_Fecha12.Text;
             string Gestion = dd_Gestion1.SelectedValue;
            // Response.Write("<script type='text/javascript'> alert('codigo mes = "+codMes+"') </script>");
           //  Response.Write("<script type='text/javascript'> alert('anio es = " + anio + "') </script>");

             if(fecha1 != "" && fecha2 != "" && Gestion != ""){
                 fecha1 = convertidorFecha(fecha1);
                 fecha2 = convertidorFecha(fecha2);
                 NEstadisticaParqueAscensores nestadistica = new NEstadisticaParqueAscensores();
                 DataSet resultado1 = nestadistica.obtenerEstadosEquiposFuncionandoEntreFechas(fecha1,fecha2,Gestion);
                 gv_estadoM1.DataSource = resultado1;
                 gv_estadoM1.DataBind();
                 lb_cantidadEquipo1.Text = Convert.ToString(nestadistica.candidadEquipoEntreFechas(fecha1,fecha2,Gestion));
                 lb_EquiposFuncionandoPorcentaje1.Text = Convert.ToString(nestadistica.porcentajeEquipoEntreFechas(fecha1, fecha2, Gestion));
                 lb_TotalEquipos1.Text = Convert.ToString(nestadistica.totalEquipo());
             }
           
         }

        protected void bt_buscar1_Click(object sender, EventArgs e)
        {
            primeraBusqueda();
        }


        private void segundaBusqueda()
        {
            string fecha1 = tx_Fecha21.Text;
            string fecha2 = tx_Fecha22.Text;
            // Response.Write("<script type='text/javascript'> alert('codigo mes = "+codMes+"') </script>");
            //  Response.Write("<script type='text/javascript'> alert('anio es = " + anio + "') </script>");            
            string Gestion = dd_Gestion2.SelectedValue;
            // Response.Write("<script type='text/javascript'> alert('codigo mes = "+codMes+"') </script>");
            //  Response.Write("<script type='text/javascript'> alert('anio es = " + anio + "') </script>");

            if (fecha1 != "" && fecha2 != "" && Gestion != "")
            {
                fecha1 = convertidorFecha(fecha1);
                fecha2 = convertidorFecha(fecha2);
                NEstadisticaParqueAscensores nestadistica = new NEstadisticaParqueAscensores();
                DataSet resultado1 = nestadistica.obtenerEstadosEquiposFuncionandoEntreFechas(fecha1, fecha2, Gestion);
                gv_EstadoM2.DataSource = resultado1;
                gv_EstadoM2.DataBind();
                lb_cantidadEquipo2.Text = Convert.ToString(nestadistica.candidadEquipoEntreFechas(fecha1, fecha2, Gestion));
                lb_EquipoPorcentaje2.Text = Convert.ToString(nestadistica.porcentajeEquipoEntreFechas(fecha1,fecha2,Gestion));
                lb_TotalEquipos2.Text = Convert.ToString(nestadistica.totalEquipo());
            }
        }

        protected void bt_buscar2_Click(object sender, EventArgs e)
        {
            segundaBusqueda();
        }
    }
}