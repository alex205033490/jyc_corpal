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
    public partial class FA_GestionarEstadoEquipo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            
            if (tienePermisoDeIngreso(16) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if (!IsPostBack)
            {
                cargarEquipos("","");
                cargarEstadoMantenimiento();
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ingresado a Gestionar Estado Mantenimiento");
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

        private void cargarEquipos(string exbo,string nombreProyecto)
        {
            NA_Equipo Nequipo = new NA_Equipo();
            DataSet datosEquipo = Nequipo.MostrarAllEquipoSeguimiento(exbo,nombreProyecto);
            gv_equipos.DataSource = datosEquipo;
            gv_equipos.DataBind();
        }

        private void cargarEstadoMantenimiento()
        {
            NA_EstadoMantenimiento Nman = new NA_EstadoMantenimiento();
            DataSet datosCargar = Nman.mostrarAllDatos();
            dd_nuevoEstado.DataSource = datosCargar;
            dd_nuevoEstado.DataValueField = "codigo";
            dd_nuevoEstado.DataTextField = "nombre";
            dd_nuevoEstado.Items.Add(new ListItem("", "-1"));
            dd_nuevoEstado.AppendDataBoundItems = true;
            dd_nuevoEstado.SelectedIndex = -1;
            dd_nuevoEstado.DataBind();

        }

        protected void bt_Buscar_Click(object sender, EventArgs e)
        {
            string exboAux = tx_Exbo.Text;
            string nombreProyectoAux = HttpUtility.HtmlDecode(tx_nombreProyecto.Text);
            cargarEquipos(exboAux, nombreProyectoAux);
        }

        protected void gv_equipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codigoEquipo = Convert.ToInt32(gv_equipos.SelectedRow.Cells[1].Text);            
            cargarSeguimientoEquipo(codigoEquipo);
                        
        }


        private void cargarSeguimientoEquipo(int codigoEquipoExbo)
        {
            NA_Seguimiento Nsegui = new NA_Seguimiento();
            DataSet mostrarTabla = Nsegui.mostrarSeguimiento(codigoEquipoExbo);
            gv_SeguiMantenimiento.DataSource = mostrarTabla;
            gv_SeguiMantenimiento.DataBind();
        }

        private void CargarSeleccionSeguimiento() {
            int codSeguimiento = Convert.ToInt32(gv_SeguiMantenimiento.SelectedRow.Cells[1].Text);
            string exboAux = gv_equipos.SelectedRow.Cells[2].Text;
            string nombreProyecAux = HttpUtility.HtmlDecode(gv_equipos.SelectedRow.Cells[3].Text);
            string EstadoAnteriorAux = gv_SeguiMantenimiento.SelectedRow.Cells[6].Text;
            tx_exboView.Text = exboAux;
            tx_NombreProyectoView.Text = nombreProyecAux;
            tx_EstadoAnteriorView.Text = EstadoAnteriorAux;
            cargarhistorialFechas(codSeguimiento);
        }
              
        protected void gv_SeguiMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSeleccionSeguimiento();
        }

        private void cargarhistorialFechas(int codSeguimiento)
        {
            NA_FechaEstadoMan NfechaEs = new NA_FechaEstadoMan();
            DataSet datosCargar = NfechaEs.historial_fechas(codSeguimiento);
            gv_historialSeguimiento.DataSource = datosCargar;
            gv_historialSeguimiento.DataBind();

        }

        private void modificarEstado() {
            //-------------------------------------------------
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            //----------------------------------------------------------

            int codSeguimiento = Convert.ToInt32(gv_SeguiMantenimiento.SelectedRow.Cells[1].Text);
            int codestadoMan = Convert.ToInt32(dd_nuevoEstado.SelectedValue);
            NA_FechaEstadoMan NfechaEstadoMan = new NA_FechaEstadoMan();
            NfechaEstadoMan.insertar(codSeguimiento, codestadoMan, codUser);
            int ultimaFechaEstadoMan_insertada = NfechaEstadoMan.ultimoinsertado();

            NA_Seguimiento nSegui = new NA_Seguimiento();
            nSegui.modificarFechaEstadoMan(codSeguimiento,ultimaFechaEstadoMan_insertada);

            int codigoEquipo = Convert.ToInt32(gv_equipos.SelectedRow.Cells[1].Text);
            cargarSeguimientoEquipo(codigoEquipo);
            CargarSeleccionSeguimiento();
        }

        protected void bt_ActualizarEstado_Click(object sender, EventArgs e)
        {
            modificarEstado();
        }

    }
}