using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.IO;
using System.Drawing;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ViewSeguimientoMantenimientoXXX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(27) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            tx_titulo.Text = "Seguimiento Mantenimiento " + Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                cargarYearSeguimiento();
                verEquiposEstados("", "", -1);

                cargarddlEstadoEquipo();
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


          private void cargarddlEstadoEquipo()
        {
            NEstadoEquipo estadoEquipo = new NEstadoEquipo();
            dd_estadoEquipo.DataSource = estadoEquipo.listar();
            dd_estadoEquipo.DataValueField = "codigo";
            dd_estadoEquipo.DataTextField = "nombre";
            dd_estadoEquipo.Items.Add(new ListItem("--Ninguno--", "-1"));            
            dd_estadoEquipo.AppendDataBoundItems = true;
            dd_estadoEquipo.SelectedIndex = -1;
            dd_estadoEquipo.DataBind();
        }

         private void buscarEquipo()
        {
            string nombreEquipo = tx_nombreEdificio.Text;
            string exbo = tx_exbo2.Text;
            int codEstadoEquipo = Convert.ToInt32(dd_estadoEquipo.SelectedValue);
            verEquiposEstados(exbo,nombreEquipo,codEstadoEquipo);       
        }

         private void verEquiposEstados(string exbo, string nombreEdificio, int codEstadoEquipo)
         {
             if (codEstadoEquipo == -1)
             {
                 NA_Equipo nequipo = new NA_Equipo();
                 DataSet resultado = nequipo.getEquiposProyectosEstadosTodos(exbo, nombreEdificio);
                 gv_tablaEquipos.DataSource = resultado;
                 gv_tablaEquipos.DataBind();
             }
             else
             {
                 NA_Equipo nequipo = new NA_Equipo();
                 DataSet resultado = nequipo.getEquiposProyectosEstados(exbo, nombreEdificio, codEstadoEquipo);
                 gv_tablaEquipos.DataSource = resultado;
                 gv_tablaEquipos.DataBind();
             }
         }

        private void cargarYearSeguimiento()
        {
            NA_Seguimiento Nsegui = new NA_Seguimiento();
            DataSet datosCargar = Nsegui.getAllyearSeguimiento();
            dd_anio.DataSource = datosCargar;
            dd_anio.DataValueField = "codigo";
            dd_anio.DataTextField = "years";
            dd_anio.Items.Add(new ListItem("", "-1"));
            dd_anio.AppendDataBoundItems = true;
            dd_anio.SelectedIndex = -1;
            dd_anio.DataBind();
        }


        protected void bt_Buscar_Click(object sender, EventArgs e)
        {
            Buscar_CuadroXXX();          
        }

        private void Buscar_CuadroXXX()
        {
            gv_tablaDatos.SelectedIndex = -1;
            if (dd_anio.SelectedIndex > -1)
            {
                    string year = dd_anio.SelectedItem.Text;
                    if (year != "")
                    {
                        int datoYear = Convert.ToInt32(year);
                        string nombreProyecto = tx_Edificio.Text;
                        string exbo = tx_Exbo.Text;
                        NA_Seguimiento Nsegui = new NA_Seguimiento();
                        DataSet tuplas = Nsegui.getCuadrosXXX_2(datoYear, exbo, nombreProyecto);
                        gv_tablaDatos.DataSource = tuplas;
                        gv_tablaDatos.DataBind();

                        ponerColoresAequiposConDeuda(gv_tablaDatos);
                    }else
                        Response.Write("<script type='text/javascript'> alert('Error: no seleccionono el Año') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: no seleccionono el Año') </script>");
            
        }

        public void ponerColoresAequiposConDeuda(GridView gv_tablaDatosAux) {
            NA_Seguimiento nseg = new NA_Seguimiento();
            int anio = Convert.ToInt32(dd_anio.SelectedItem.Text);

            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++ )
            {
                string exbo = gv_tablaDatosAux.Rows[i].Cells[1].Text;
                string nombreProyecto = gv_tablaDatosAux.Rows[i].Cells[2].Text;
                string critico = gv_tablaDatosAux.Rows[i].Cells[14].Text;
                // if(nseg.tieneDeudasPendientes_mayorA(exbo,2,anio) || nseg.tieneDeudasAnterioresGestionesPendientes(exbo,nombreProyecto,anio)){
                if (critico.Equals("Critico") || critico.Equals("Perdido"))
                {  
                    gv_tablaDatosAux.Rows[i].BackColor = Color.Red;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.White;    
                 }
                 if (critico.Equals("Mantenimiento Fuera de Control"))
                 {  
                    gv_tablaDatosAux.Rows[i].BackColor = Color.Orange;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.Black;    
                 }
                
            }        
        
        }

       

        protected void Button1_Click(object sender, EventArgs e)
        {
            exportarEn_Excel2();
       
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        private void exportarEn_Excel2()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string nombreArchivo = "Seguimiento Mantenimiento " + Session["BaseDatos"].ToString()+" " +DateTime.Now ;
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombreArchivo + ".xls" + "\"");
           
            GridView dg = gv_tablaDatos;
            dg.GridLines = GridLines.Both;
            dg.HeaderStyle.Font.Bold = true;
            dg.Columns[0].Visible = false;
            dg.RenderControl(htmltextwrtter);
                              
            Response.Write(strwritter.ToString());
            Response.End();  
        }




        private void exportarEn_Excel()
        {
            string year = dd_anio.SelectedItem.Text;
            //    if (year != "")
            //     {
            int datoYear = Convert.ToInt32(year);
            string nombreProyecto = tx_Edificio.Text;
            string exbo = tx_Exbo.Text;
            NA_Seguimiento Nsegui = new NA_Seguimiento();
            DataSet tuplas = Nsegui.getCuadrosXXX_2(datoYear, exbo, nombreProyecto);
            //    } 

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Seguimiento Mantenimiento " + Session["BaseDatos"].ToString(); 
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

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            buscarEquipo();
        }

        protected void bt_DescargarInstalacion_Click(object sender, EventArgs e)
        {

            string nombreEquipo = tx_nombreEdificio.Text;
            string exbo = tx_exbo2.Text;
            int codEstadoEquipo = Convert.ToInt32(dd_estadoEquipo.SelectedValue);
            DataSet tuplas;

            if (codEstadoEquipo == -1)
            {
                NA_Equipo nequipo = new NA_Equipo();
                tuplas = nequipo.getEquiposProyectosEstadosTodos(exbo, nombreEquipo);
               
            }
            else
            {
                NA_Equipo nequipo = new NA_Equipo();
                tuplas = nequipo.getEquiposProyectosEstados(exbo, nombreEquipo, codEstadoEquipo);
               
            }

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Estado Instalacion " + Session["BaseDatos"].ToString(); 
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