using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.IO;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FDatosEquipo : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(12) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 


            listaEstadisticaInstalacion();
            lbTotalEquipoFuncionar.Text = totalEquipoPorFuncionar();
            lbTotalPorcentaje.Text = totalEquipoFuncionarPorcentaje() + " "+"%";
            lb_CandidadEquipos.Text = totalEquipos();

            if(!IsPostBack){
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ingresado a Estadistica de Instalacion");
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

        private void listaEstadisticaInstalacion()
        {
            NEstadisticaInstalacion estadisticaInstalacion = new NEstadisticaInstalacion();
            DataSet listaActualizacionEquipo = estadisticaInstalacion.listaEstadisticaInstalacion();
            GridView1.DataSource= listaActualizacionEquipo;
            GridView1.DataBind();
        }

        private void listaEquipoPorEstado()
        {
            NEstadisticaInstalacion estadisticaInstalacion = new NEstadisticaInstalacion();
            lbDatosEquipo.Visible = true;
            String nombreEstado = GridView1.SelectedRow.Cells[1].Text;
            DataSet listaEquipoPorEstado = estadisticaInstalacion.listarEquipoPorEstado(nombreEstado);
            GridView2.DataSource = listaEquipoPorEstado;
            GridView2.DataBind();
        }

        private string totalEquipoPorFuncionar() 
        {
            NEstadisticaInstalacion estadisticaInstalacion = new NEstadisticaInstalacion();
            int total = estadisticaInstalacion.totalEquipoPorFuncionar();
            string total_ = Convert.ToString(total);
            return total_;
        }

        private string totalEquipoFuncionarPorcentaje()
        {
            NEstadisticaInstalacion estadisticaInstalacion = new NEstadisticaInstalacion();
            float total = estadisticaInstalacion.totalEquipoPorFuncionarPorcentaje();
            string total_ = Convert.ToString(total);
            return total_;
        }

        private string totalEquipos() { 
            NEstadisticaParqueAscensores equipoPA = new NEstadisticaParqueAscensores();
            int total = equipoPA.totalEquipo();
            return Convert.ToString(total);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaEquipoPorEstado();
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            listaEquipoPorEstado();
        }

        protected void bt_ExportarExel_Click(object sender, EventArgs e)
        {
            NEstadisticaInstalacion estadisticaInstalacion = new NEstadisticaInstalacion();
            lbDatosEquipo.Visible = true;
            String nombreEstado = GridView1.SelectedRow.Cells[1].Text;
            DataSet listaEquipoPorEstado = estadisticaInstalacion.listarEquipoPorEstado(nombreEstado);
            
            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombreDB = Session["BaseDatos"].ToString();
            string nombre = "Estado Equipo " + nombreEstado+"_"+nombreDB ;            
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = listaEquipoPorEstado;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }

        }

    }
}