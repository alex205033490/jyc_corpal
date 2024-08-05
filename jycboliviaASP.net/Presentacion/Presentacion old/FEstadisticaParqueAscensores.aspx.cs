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
    public partial class FEstadisticaParqueAscensores : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(13) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

             //listarEstadisticaParqueAscensor();
            //lbTotalParqueAscensor.Text = totalEquipoParqueAscensores();
            //lbTotalParqueAscensorPorcentaje.Text = totalEquipoParqueAscensoresPorcentaje();
            //lbTotalEquipo.Text = totalEquipo();

            if (!IsPostBack)
            {
                cargarddlAnioSeguimiento();

                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ingresado a Estadistica de Parque de Ascensores");
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

        private void cargarddlAnioSeguimiento()
        {
            NEstadisticaParqueAscensores parqueAscensores = new NEstadisticaParqueAscensores();
            ddlAnio.DataSource = parqueAscensores.listarAniosSeguimiento();
            //ddlAnio.DataValueField = "codigo";
            ddlAnio.DataTextField = "years";
            ddlAnio.Items.Add(new ListItem(""));
            ddlAnio.AppendDataBoundItems = true;
           // ddlAnio.SelectedIndex = -1;
            ddlAnio.DataBind();
        }

        private void listarEstadisticaParqueAscensor() 
        {
            NEstadisticaParqueAscensores parqueAscensores = new NEstadisticaParqueAscensores();
            string anio = ddlAnio.SelectedValue;
           if(anio!=""){
               DataSet lista = parqueAscensores.obtenerEstadisticaAnioMantenimiento(anio);
               GridView1.DataSource = lista;
               GridView1.DataBind();
           }

        }

        private void listarEquipoPorEstado() 
        {
            NEstadisticaParqueAscensores parqueAscensores = new NEstadisticaParqueAscensores();
          //  lbDatosEquipo.Visible = true;
            String nombreEstado = GridView1.SelectedRow.Cells[1].Text;
            string anio = ddlAnio.SelectedValue;
            DataSet listaEquipoPorEstado = parqueAscensores.obtenerDatosEstadisticaMantenimientoPorEstado(nombreEstado, anio);
            GridView2.DataSource = listaEquipoPorEstado;
            GridView2.DataBind(); 
        }

        private string totalEquipo() 
        {
            NEstadisticaParqueAscensores parqueAscensores = new NEstadisticaParqueAscensores();
            int total = parqueAscensores.totalEquipo();
            return Convert.ToString(total);
        }

        private string totalEquipoParqueAscensores() 
        {
            NEstadisticaParqueAscensores parqueAscensores = new NEstadisticaParqueAscensores();
            string anio = ddlAnio.SelectedValue;
            int total = parqueAscensores.totalEquipoParqueAscensores(anio);
            return Convert.ToString(total);
        }

        private string totalEquipoParqueAscensoresPorcentaje()
        {
            NEstadisticaParqueAscensores parqueAscensores = new NEstadisticaParqueAscensores();
            string anio = ddlAnio.SelectedValue;
            float total = parqueAscensores.totalEquipoParqueAscensoresPorcentaje(anio);
            return Convert.ToString(total);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView2.Visible = true;
            listarEquipoPorEstado();
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            listarEquipoPorEstado();
           // GridView1.DataBind();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            NEstadisticaParqueAscensores parqueAscensores = new NEstadisticaParqueAscensores();
            lbTotalEquipo.Visible= true;
            lbTotalEquipo1.Visible = true;
            lbTotalParqueAscensor.Visible = true;
            lbTotalParqueAscensor1.Visible = true;
            lbTotalParqueAscensorPorcentaje.Visible = true;
            lbTotalParqueAscensorPorcentaje1.Visible = true;
            
            //lbDatosEquipo.Visible = false;

           string anio = ddlAnio.SelectedValue;
           
           if (anio != "")
           {               
               lbTotalParqueAscensor.Text = totalEquipoParqueAscensores();
               lbTotalParqueAscensorPorcentaje.Text = totalEquipoParqueAscensoresPorcentaje();
               listarEstadisticaParqueAscensor();
               NEstadisticaInstalacion estadisticaInstalacion = new NEstadisticaInstalacion();
               lb_faltantesMantenimiento.Text = parqueAscensores.totalFaltantesMantenimiento(anio).ToString();
              // lb_TotalEquiposPorFuncionar.Text = Convert.ToString(estadisticaInstalacion.totalEquipoPorFuncionarMantenimiento(Convert.ToInt32(anio)));                              
               lbTotalEquipo.Text = totalEquipo();

               int totalEquiposAux = Convert.ToInt32(lbTotalEquipo.Text);
               int totalfuncionandoAux = Convert.ToInt32(lbTotalParqueAscensor.Text);
               int faltanteMantenimientoAux = Convert.ToInt32(lb_faltantesMantenimiento.Text);
               int equiposPorFuncionar = totalEquiposAux - totalfuncionandoAux - faltanteMantenimientoAux;
               lb_TotalEquiposPorFuncionar.Text = equiposPorFuncionar.ToString();

               GridView2.Visible = false;
           }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            NEstadisticaParqueAscensores parqueAscensores = new NEstadisticaParqueAscensores();
            String nombreEstado = GridView1.SelectedRow.Cells[1].Text;
            string anio = ddlAnio.SelectedValue;
            DataSet listaEquipoPorEstado = parqueAscensores.obtenerDatosEstadisticaMantenimientoPorEstado(nombreEstado, anio);
            
            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombreDB = Session["BaseDatos"].ToString();
            string nombre = "Mantenimiento Equipo " + nombreEstado + anio + "_"+nombreDB;
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

        protected void lkb_faltantesMantenimiento_Click(object sender, EventArgs e)
        {
            exportarAexcel();
        }

        private void exportarAexcel()
        {
            NEstadisticaParqueAscensores parqueAscensores = new NEstadisticaParqueAscensores();
            
            string anio = ddlAnio.SelectedValue;
            DataSet listaEquipoPorEstado = parqueAscensores.totalFaltantesMantenimiento2(anio);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombreDB = Session["BaseDatos"].ToString();
            string nombre = "Mantenimiento Equipo Faltantes " + anio + "_" + nombreDB;
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