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
    public partial class FA_MonitoreoGestionProyecto210 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(23) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if(!IsPostBack){

                listarEquipos();
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

        public void listarEquipos() {
            NProyecto proyectoN = new NProyecto();
            DataSet tuplaresultado = proyectoN.BuscarMonitoreoGestionProyecto("");
            gv_MonitoreoGestionProyecto.DataSource = tuplaresultado;
            gv_MonitoreoGestionProyecto.DataBind();        
        }

        public string convertidorFecha(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'"+_fecha+"'";
            }
            else
                return "null";
        }

        public void Actualizar() {
            string nombreProyecto = tx_nombreProyecto.Text;
            NProyecto proyectoN = new NProyecto();
            int codigoProyecto = proyectoN.getCodigoProyect(nombreProyecto);
            string fecha_Apertura = convertidorFecha(tx_fechaApertura.Text);
            string fecha_SalidaFab = convertidorFecha(tx_fechaSalidaFab.Text);
            string fecha_EntregaContrato = convertidorFecha(tx_fechaEntregaContrato.Text);
            string fecha_EntregaAcordada = convertidorFecha(tx_fechaEntregaAcordada.Text);
            string fecha_CobroADM = convertidorFecha(tx_fechaCobroAdm.Text);
            ///---- datos administrativo
            string fecha_R189 = convertidorFecha(tx_fechaR189.Text);
            string fecha_R190 = convertidorFecha(tx_fechaR190.Text);
            string fecha_R182 = convertidorFecha(tx_fechaR182.Text);
            string fecha_R197 = convertidorFecha(tx_fechaR197.Text);
            string fecha_R198 = convertidorFecha(tx_fechaR198.Text);
            string fecha_R199 = convertidorFecha(tx_fechaR199.Text);
            string fecha_R200 = convertidorFecha(tx_fechaR200.Text);
            string fecha_R201 = convertidorFecha(tx_fechaR201.Text);
            string fecha_R202 = convertidorFecha(tx_fechaR202.Text);  
            ////------- dpto Proyectos
            string fecha_R188 = convertidorFecha(tx_fechaR188.Text);
            string fecha_R183 = convertidorFecha(tx_fechaR183.Text);
            string fecha_R184 = convertidorFecha(tx_fechaR184.Text);
            string fecha_R194 = convertidorFecha(tx_fechaR194.Text);
            string fecha_R186 = convertidorFecha(tx_fechaR186.Text);

            proyectoN.actualicarControl(codigoProyecto,fecha_Apertura,fecha_SalidaFab,fecha_EntregaContrato,fecha_EntregaAcordada,
                fecha_CobroADM,fecha_R189,fecha_R190,fecha_R182,fecha_R197,fecha_R198,fecha_R199,fecha_R200,fecha_R201,fecha_R202,
                fecha_R188,fecha_R183,fecha_R184,fecha_R194,fecha_R186);
            listarEquipos();

        }


        protected void bt_Actualizar_datos_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        public void seleccionarDatos(){

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[1].Text != "&nbsp;")
            {
                tx_nombreProyecto.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[1].Text;
            }
            else
                tx_nombreProyecto.Text = "";


            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[2].Text != "&nbsp;")
            {
                tx_fechaApertura.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[2].Text;
            }
            else
                tx_fechaApertura.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[3].Text != "&nbsp;")
            {
                tx_fechaSalidaFab.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[3].Text;
            }
            else
                tx_fechaSalidaFab.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[4].Text != "&nbsp;")
            {
                tx_fechaEntregaContrato.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[4].Text;
            }
            else
                tx_fechaEntregaContrato.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[5].Text != "&nbsp;")
            {
                tx_fechaEntregaAcordada.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[5].Text;
            }
            else
                tx_fechaEntregaAcordada.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[6].Text != "&nbsp;")
            {
                tx_fechaCobroAdm.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[6].Text;
            }
            else
                tx_fechaCobroAdm.Text = "";
            ///---- datos administrativo
            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[7].Text != "&nbsp;")
            {
                tx_fechaR189.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[7].Text;
            }
            else
                tx_fechaR189.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[8].Text != "&nbsp;")
            {
                tx_fechaR190.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[8].Text;
            }
            else
                tx_fechaR190.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[9].Text != "&nbsp;")
            {
                tx_fechaR182.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[9].Text;
            }
            else
                tx_fechaR182.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[10].Text != "&nbsp;")
            {
                tx_fechaR197.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[10].Text;
            }
            else
                tx_fechaR197.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[11].Text != "&nbsp;")
            {
                tx_fechaR198.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[11].Text;
            }
            else
                tx_fechaR198.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[12].Text != "&nbsp;")
            {
                tx_fechaR199.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[12].Text;
            }
            else
                tx_fechaR199.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[13].Text != "&nbsp;")
            {
                tx_fechaR200.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[13].Text;
            }
            else
                tx_fechaR200.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[14].Text != "&nbsp;")
            {
                tx_fechaR201.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[14].Text;
            }
            else
                tx_fechaR201.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[15].Text != "&nbsp;")
            {
                tx_fechaR202.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[15].Text;
            }
            else
                tx_fechaR202.Text = "";
            ////------- dpto Proyectos
            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[16].Text != "&nbsp;")
            {
                tx_fechaR188.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[16].Text;
            }
            else
                tx_fechaR188.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[17].Text != "&nbsp;")
            {
                tx_fechaR183.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[17].Text;
            }
            else
                tx_fechaR183.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[18].Text != "&nbsp;")
            {
                tx_fechaR184.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[18].Text;
            }
            else
                tx_fechaR184.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[19].Text != "&nbsp;")
            {
                tx_fechaR194.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[19].Text;
            }
            else
                tx_fechaR194.Text = "";

            if (gv_MonitoreoGestionProyecto.SelectedRow.Cells[20].Text != "&nbsp;")
            {
                tx_fechaR186.Text = gv_MonitoreoGestionProyecto.SelectedRow.Cells[20].Text;
            }
            else
                tx_fechaR186.Text = "";

        
        }


        protected void gv_MonitoreoGestionProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarDatos();
        }


        public void buscarDatos() {
            string nombreProyecto = tx_nombreProyecto.Text;
            NProyecto proyectoN = new NProyecto();
            DataSet tuplasResult = proyectoN.BuscarMonitoreoGestionProyecto(nombreProyecto);
            gv_MonitoreoGestionProyecto.DataSource = tuplasResult;
            gv_MonitoreoGestionProyecto.DataBind();
        }


        protected void bt_Buscar_Click(object sender, EventArgs e)
        {
            buscarDatos();
        }
    }
}