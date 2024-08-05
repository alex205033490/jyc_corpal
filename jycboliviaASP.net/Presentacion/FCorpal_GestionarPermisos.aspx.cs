using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using jycboliviaASP.net.Negocio;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_GestionarPermisos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(2) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }  

            if(!IsPostBack){
                CargarResponsables("");

                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha ingresado a Gestionar Permisos");

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

        private void CargarResponsables(string nombreResponsable) {
            NA_Responsables Nresp = new NA_Responsables();
            DataSet datosResp = Nresp.mostrarTodosDatos2(nombreResponsable);
            GridView1.DataSource = datosResp;
            GridView1.DataBind();
        }

        private void CargarPermisos() {           
               int codigoResp = Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);
               NA_DetallePermiso Npermiso = new NA_DetallePermiso();
               DataSet accesoRes = Npermiso.mostrarPermiso_responsable(codigoResp);
               DataSet accesoSis = Npermiso.mostrarNOPermiso_responsable(codigoResp);
               gv_PermisosResp.DataSource = accesoRes;
               gv_PermisosResp.DataBind();
               gv_AccesosSistema.DataSource = accesoSis;
               gv_AccesosSistema.DataBind();
           
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPermisos();
        }


        protected void eliminarAccesoResponsable()
        {

            NA_DetallePermiso Npermiso = new NA_DetallePermiso();
            int codigoResp = Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);
            CheckBox cb = null;
            for (int i = 0; i < gv_PermisosResp.Rows.Count; i++)
            {
                cb = (CheckBox)gv_PermisosResp.Rows[i].Cells[1].FindControl("CheckBox1");
                if (cb != null && cb.Checked)
                {
                    int codigoForm = Convert.ToInt32(gv_PermisosResp.Rows[i].Cells[1].Text);
                    Npermiso.eliminar(codigoForm,codigoResp);
                }
            }         
                CargarPermisos();           
            
        }

        protected void AsignarAccesoResponsable()
        {
            NA_DetallePermiso Npermiso = new NA_DetallePermiso();
            int codigoResp = Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);
            CheckBox cb = null;
            for (int i = 0; i < gv_AccesosSistema.Rows.Count; i++)
            {
                cb = (CheckBox)gv_AccesosSistema.Rows[i].Cells[1].FindControl("CheckBox1");
                if (cb != null && cb.Checked)
                {
                    int codigoForm = Convert.ToInt32(gv_AccesosSistema.Rows[i].Cells[1].Text);
                    Npermiso.insertar(codigoForm, codigoResp, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"));
                }
            }
            CargarPermisos();
        }
        
        protected void bt_eliminarAccesos_Click(object sender, EventArgs e)
        {
            eliminarAccesoResponsable();
        }

        protected void bt_addAccesos_Click(object sender, EventArgs e)
        {
            AsignarAccesoResponsable();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string nombreResp = tx_nombreResponsable.Text;
            CargarResponsables(nombreResp);

        }


    }
}