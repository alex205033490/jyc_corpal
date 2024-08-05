using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Web.Script.Serialization;
using System.IO;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ViewCuadroXXX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(17) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if (!IsPostBack)
            {
                cargarYearSeguimiento();
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
            dd_yearSeguimiento.DataSource = datosCargar;
            dd_yearSeguimiento.DataValueField = "codigo";
            dd_yearSeguimiento.DataTextField = "years";
            dd_yearSeguimiento.Items.Add(new ListItem("", "-1"));
            dd_yearSeguimiento.AppendDataBoundItems = true;
            dd_yearSeguimiento.SelectedIndex = -1;
            dd_yearSeguimiento.DataBind();
        }

       

        private void buscarSeguimiento() {
            string year = dd_yearSeguimiento.SelectedItem.Text;
            if(year != ""){
                int datoYear = Convert.ToInt32(year);
                string nombreProyecto = tx_nombreProyecto.Text;
                string exbo = tx_exbo.Text;
                NA_Seguimiento Nsegui = new NA_Seguimiento();
                DataSet tuplas = Nsegui.GetCuadrosXXX(datoYear,exbo,nombreProyecto);
                gv_CuadroXXX.DataSource = tuplas;
                gv_CuadroXXX.DataBind();
            }
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            buscarSeguimiento();
        }


        private void buscarDatosAdicionales() {
            string ExboAux = gv_CuadroXXX.SelectedRow.Cells[2].Text;
            int year = Convert.ToInt32(dd_yearSeguimiento.SelectedItem.Text);

            NA_Equipo Nequipo = new NA_Equipo();
            int codEquipo = Nequipo.getCodigoEquipo(ExboAux);

            NA_Seguimiento Nsegui = new NA_Seguimiento();
            int codSeguimiento = Nsegui.getCodigoSeguimiento(codEquipo,year);
            cargarhistorialFechas(codSeguimiento);

            
            DataSet datosRellenar = Nequipo.getEncargadosResponsables(codEquipo,1);
            if (datosRellenar.Tables[0].Rows.Count > 0)
            {
                tx_TecnicoMantenimiento.Text = datosRellenar.Tables[0].Rows[0][1].ToString();               
            }
            else {
                tx_TecnicoMantenimiento.Text = "No Asignado";            
            }

            datosRellenar = Nequipo.getEncargadosResponsables(codEquipo, 2);
            if (datosRellenar.Tables[0].Rows.Count > 0)
            {
                tx_SupervisorTecnico.Text = datosRellenar.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                tx_SupervisorTecnico.Text = "No Asignado";
            }

            datosRellenar = Nequipo.getEncargadosResponsables(codEquipo, 3);
            if (datosRellenar.Tables[0].Rows.Count > 0)
            {
                tx_TecnicoInstalador.Text = datosRellenar.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                tx_TecnicoInstalador.Text = "No Asignado";
            }

            datosRellenar = Nequipo.getEncargadosResponsables(codEquipo, 4);
            if (datosRellenar.Tables[0].Rows.Count > 0)
            {
                tx_Cobrador.Text = datosRellenar.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                tx_Cobrador.Text = "No Asignado";
            }

            datosRellenar = Nequipo.getEncargadosResponsables(codEquipo, 5);
            if (datosRellenar.Tables[0].Rows.Count > 0)
            {
                tx_EncargadoCobranza.Text = datosRellenar.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                tx_EncargadoCobranza.Text = "No Asignado";
            }

        }


        private void cargarhistorialFechas(int codSeguimiento)
        {
            NA_FechaEstadoMan NfechaEs = new NA_FechaEstadoMan();
            DataSet datosCargar = NfechaEs.historial_fechas(codSeguimiento);
            gv_historialEstados.DataSource = datosCargar;
            gv_historialEstados.DataBind();

        }

        protected void gv_CuadroXXX_SelectedIndexChanged(object sender, EventArgs e)
        {
            buscarDatosAdicionales();
        }

        protected void bt_Excel_Click(object sender, EventArgs e)
        {


            string year = dd_yearSeguimiento.SelectedItem.Text;
            if (year != "")
            {
                int datoYear = Convert.ToInt32(year);
                string nombreProyecto = tx_nombreProyecto.Text;
                string exbo = tx_exbo.Text;
                NA_Seguimiento Nsegui = new NA_Seguimiento();
                DataSet tuplas = Nsegui.GetCuadrosXXX(datoYear, exbo, nombreProyecto);


                //// Creacion del Excel
                HttpResponse response = HttpContext.Current.Response;
                // first let's clean up the response.object
                response.Clear();
                response.Charset = "";
                // set the response mime type for excel
                response.ContentType = "application/vnd.ms-excel";
                string nombre = "Prueba";
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

                // create a string writer
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        // instantiate a datagrid
                        DataGrid dg = new DataGrid();
                        dg.DataSource = tuplas;
                        dg.DataBind();
                        dg.RenderControl(htw);
                        response.Write(sw.ToString());
                        response.End();
                    }

                }

            }
        }
    }
}