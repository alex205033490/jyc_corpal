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
    public partial class FA_PolizaBoletaLetra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(46) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if (!IsPostBack)
            {
                Cargaredificios("");
            }
        }


        public string aFecha(string fecha)
        {
            if (fecha == "" || fecha == "&nbsp;")
            {
                return fecha = "null";
            }

            else
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'"+_fecha+"'";
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

        private void Cargaredificios(string nombreEdificio)
        {
            NProyecto nproyecto = new NProyecto();
            DataSet datos = nproyecto.buscar(nombreEdificio);
            gv_tablaEdificios.DataSource = datos;
            gv_tablaEdificios.DataBind();
            ponerColoresAequiposConDeudaPolizaBoletaLetra(gv_tablaEdificios);
        }


        public void ponerColoresAequiposConDeudaPolizaBoletaLetra(GridView gv_tablaDatosAux)
        {
            NProyecto nproyec = new NProyecto();

            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++)
            {
                string codigoAux = gv_tablaDatosAux.Rows[i].Cells[4].Text;
                int codigoEdificio = Convert.ToInt32(codigoAux);
                if (nproyec.tieneDeudasPendientesProyectoPolizadeSeguro(codigoEdificio))
                {
                    gv_tablaDatosAux.Rows[i].BackColor = Color.Red;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.White;
                    CheckBox cb = (CheckBox)gv_tablaEdificios.Rows[i].Cells[0].FindControl("CheckBox1");
                    cb.Checked = true;
                }

                if (nproyec.tieneDeudasPendientesProyectoBoletaBancaria(codigoEdificio))
                {
                    gv_tablaDatosAux.Rows[i].BackColor = Color.Red;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.White;
                    CheckBox cb = (CheckBox)gv_tablaEdificios.Rows[i].Cells[1].FindControl("CheckBox2");
                    cb.Checked = true;
                }

                if (nproyec.tieneDeudasPendientesProyectoLetradeCambio(codigoEdificio))
                {
                    gv_tablaDatosAux.Rows[i].BackColor = Color.Red;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.White;
                    CheckBox cb = (CheckBox)gv_tablaEdificios.Rows[i].Cells[2].FindControl("CheckBox3");
                    cb.Checked = true;
                }
                string fechaVencimientoBoletaGarantia = nproyec.getfechaVencimientoBoletaGarantia(codigoEdificio);
                TextBox fechaAux = (TextBox)gv_tablaEdificios.Rows[i].Cells[3].FindControl("tx_boletasGarantia");
                fechaAux.Text = fechaVencimientoBoletaGarantia;
            }

        }


        private void marcarEdificios()
        {
            NProyecto Nproy = new NProyecto();


            for (int i = 0; i < gv_tablaEdificios.Rows.Count; i++)
            {
                string codigoAux = gv_tablaEdificios.Rows[i].Cells[4].Text;
                int codigoEdificio = Convert.ToInt32(codigoAux);
                CheckBox cb = (CheckBox)gv_tablaEdificios.Rows[i].Cells[0].FindControl("CheckBox1");
                CheckBox cb2 = (CheckBox)gv_tablaEdificios.Rows[i].Cells[1].FindControl("CheckBox2");
                CheckBox cb3 = (CheckBox)gv_tablaEdificios.Rows[i].Cells[2].FindControl("CheckBox3");
                TextBox fechaAux = (TextBox)gv_tablaEdificios.Rows[i].Cells[3].FindControl("tx_boletasGarantia");
                bool bandera = cb.Checked;
                bool bandera2 = cb2.Checked;
                bool bandera3 = cb3.Checked;
                Nproy.modificarDeudaProyectoPolizadeSeguro(codigoEdificio, bandera);
                Nproy.modificarDeudaProyectoBoletaBancaria(codigoEdificio, bandera2);
                Nproy.modificarDeudaProyectoLetradeCambio(codigoEdificio, bandera3);
                string fechaVeBoGa = aFecha(fechaAux.Text);
                Nproy.modificarfechaVencimientoBoletaGarantia(codigoEdificio, fechaVeBoGa);
            }
        }


        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            Cargaredificios(tx_edificio.Text);
        }

        protected void bt_marcados_Click(object sender, EventArgs e)
        {
            marcarEdificios();
            Cargaredificios(tx_edificio.Text);
        }
    }
}