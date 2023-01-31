using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Drawing;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_DeudaPlanPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(58) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if (!IsPostBack)
            {
              //  Cargaredificios("");
              //  VerPlanPagos(gv_edificionPlanPago);
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

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            Cargaredificios(tx_edificio.Text);
            VerPlanPagos(gv_edificionPlanPago);
        }


        public void VerPlanPagos(GridView gv_tablaDatosAux)
        {
            NProyecto nproyec = new NProyecto();

            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++)
            {
                int codigoEdificio = Convert.ToInt32(gv_tablaDatosAux.Rows[i].Cells[3].Text);
                if (nproyec.tienePlanPago(codigoEdificio))
                {
                    gv_tablaDatosAux.Rows[i].BackColor = Color.LightGreen;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.Black;
                    CheckBox cb = (CheckBox)gv_edificionPlanPago.Rows[i].Cells[0].FindControl("CheckBox1");
                    cb.Checked = true;
                }

                if (nproyec.tieneServicioSuspendido(codigoEdificio))
                {
                    gv_tablaDatosAux.Rows[i].BackColor = Color.LightGreen;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.Black;
                    CheckBox cb = (CheckBox)gv_edificionPlanPago.Rows[i].Cells[1].FindControl("CheckBox2");
                    cb.Checked = true;
                }

                string horarioatencion = nproyec.horariodeAtencion(codigoEdificio);                

                if (horarioatencion.Equals("solo Horario Oficina"))
                {
                    gv_tablaDatosAux.Rows[i].BackColor = Color.LightGreen;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.Black;
                    CheckBox cb = (CheckBox)gv_edificionPlanPago.Rows[i].Cells[2].FindControl("CheckBox3");
                    cb.Checked = true;
                }
                

            }

        }


        private void Cargaredificios(string nombreEdificio)
        {
            NProyecto nproyecto = new NProyecto();
            DataSet datos = nproyecto.buscador1(nombreEdificio);
            gv_edificionPlanPago.DataSource = datos;
            gv_edificionPlanPago.DataBind();            
        }


        private void marcarEdificios()
        {
            NProyecto Nproy = new NProyecto();


            for (int i = 0; i < gv_edificionPlanPago.Rows.Count; i++)
            {
                int codigoEdificio = Convert.ToInt32(gv_edificionPlanPago.Rows[i].Cells[3].Text);
                string Edificio = gv_edificionPlanPago.Rows[i].Cells[4].Text;
                CheckBox cbPlanPago = (CheckBox)gv_edificionPlanPago.Rows[i].Cells[0].FindControl("CheckBox1");
                CheckBox cbServicioSuspendido = (CheckBox)gv_edificionPlanPago.Rows[i].Cells[1].FindControl("CheckBox2");
                CheckBox cbSolohorarioOficina = (CheckBox)gv_edificionPlanPago.Rows[i].Cells[2].FindControl("CheckBox3");
                //-----------------usuario-------------------
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                //----------------------------------------------------------
                NA_Historial nhistorial = new NA_Historial();

                if(cbPlanPago.Checked){
                    Nproy.modificarPlanPago(codigoEdificio, cbPlanPago.Checked);
                    //-------------------- historial ------------------
                    nhistorial.insertar(codUser, "Ha colocado un plan de pago para el Edificio " + Edificio + " con codigo " + codigoEdificio + " por el usuario " + codUser);
                    //------------------------------------------------
                }else
                    Nproy.modificarPlanPago(codigoEdificio, cbPlanPago.Checked);

                if(cbServicioSuspendido.Checked){
                    Nproy.modificarServicioSuspendido(codigoEdificio, cbServicioSuspendido.Checked);
                    //-------------------- historial ------------------
                    nhistorial.insertar(codUser, "Ha colocado un Servicio Suspendido para el Edificio " + Edificio + " con codigo " + codigoEdificio + " por el usuario " + codUser);
                    //------------------------------------------------
                }else
                    Nproy.modificarServicioSuspendido(codigoEdificio, cbServicioSuspendido.Checked);


                if (cbSolohorarioOficina.Checked)
                {
                    Nproy.modificarHorarioOficina(codigoEdificio, cbSolohorarioOficina.Checked);
                    //-------------------- historial ------------------
                    nhistorial.insertar(codUser, "Ha colocado un Servicio Suspendido para el Edificio " + Edificio + " con codigo " + codigoEdificio + " por el usuario " + codUser);
                    //------------------------------------------------
                }
                else
                    Nproy.modificarHorarioOficina(codigoEdificio, cbSolohorarioOficina.Checked);

                

                             
                

            }
        }

        protected void bt_marcados_Click(object sender, EventArgs e)
        {
            marcarEdificios();
            Cargaredificios(tx_edificio.Text);
            VerPlanPagos(gv_edificionPlanPago);
        }

    }
}