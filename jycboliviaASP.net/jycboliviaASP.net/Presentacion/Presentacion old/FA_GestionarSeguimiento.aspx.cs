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
    public partial class FA_GestionarSeguimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(10) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

           

            if(!IsPostBack){
                cargarEquipos();

                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha ingresado a Gestionar Seguimiento");
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

        private void cargarEquipos() {
            NA_Equipo Nequipo = new NA_Equipo();
            DataSet datosEquipo = Nequipo.MostrarAllEquipoSeguimiento2("", "");
            gv_equipos.DataSource = datosEquipo;
            gv_equipos.DataBind();
        }

        protected void gv_equipos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_equipos.PageIndex = e.NewPageIndex;
            cargarEquipos();
        }

        private void RedireccionarPagina() {
            string codigoEquipo = gv_equipos.SelectedRow.Cells[1].Text;
            string codigoExbo = gv_equipos.SelectedRow.Cells[2].Text;
            // Response.Write("<script type='text/javascript'> alert('codigo ="+codigoEquipo+" exbo="+codigoExbo+"  ') </script>");
            NEquipo equipoN = new NEquipo();
            DataSet tupla = equipoN.getEquipo(Convert.ToInt32(codigoEquipo));
            if(tupla.Tables[0].Rows.Count > 0){
                int codEstado = Convert.ToInt32(tupla.Tables[0].Rows[0][9].ToString());
              if(codEstado == 10){  // si es igual a Habilitado
                  Session["codEquipo"] = codigoEquipo;
                  Session["codExbo"] = codigoExbo;
                  Response.Redirect("../Presentacion/FA_GestionarSeguimiento2.aspx");    
              }else
                  Response.Write("<script type='text/javascript'> alert('El Equipo no se Encuentra Habilitado') </script>");
            }

            
        }

        protected void gv_equipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedireccionarPagina();
        }

        private void cargarEquipos(string exbo, string nombreProyecto) {
            NA_Equipo Nequipo = new NA_Equipo();
            DataSet datosEquipo = Nequipo.MostrarAllEquipoSeguimiento2(exbo,nombreProyecto);
            gv_equipos.DataSource = datosEquipo;
            gv_equipos.DataBind();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string exboAux = tx_Exbo.Text;
            string nombreProyectoAux = tx_nombreProyecto.Text;
            cargarEquipos(exboAux,nombreProyectoAux);
        }
    }
}