using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.IO;
using System.Text;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_SGI_Actividades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(83) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            
            if (!IsPostBack)
            {
                dd_estado.SelectedIndex = 0;
                buscarActividades("", "", true);
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

        public void limpiar()
        {
            tx_Actividad.Text = "";
            tx_PersonalAsignado.Text = "";
            tx_fechaTecnico.Text = "";
            tx_horaTecnico.Text = "";
            tx_observacionTecnico.Text = "";
            tx_cierreTarea.Text = "";
            tx_fechaEjecucion.Text = "";
            tx_horaejecucion.Text = "";
            tx_FechaExpiracion.Text = "";
        }


    
        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaResponsable2(string prefixText, int count)
        {
            string nombreResponsable = prefixText;

            NA_Responsables Nrespon = new NA_Responsables();
            DataSet tuplas = Nrespon.mostrarTodos_AutoComplit(nombreResponsable);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }


        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string ActividadPersonal = tx_Actividad.Text;
            string nombreTecnico = tx_PersonalAsignado.Text;            
            int bandera = Convert.ToInt32(dd_estado.SelectedValue.ToString());
            bool estado = Convert.ToBoolean(bandera);
            buscarActividades(ActividadPersonal, nombreTecnico,  estado);
        }


        public void buscarActividades(string detalleTarea, string nombreTecnico,  bool estado)
        {
            NA_ActividadesPersonal ntarea = new NA_ActividadesPersonal();
            DataSet resultado = ntarea.mostrarActividades(detalleTarea, nombreTecnico,  estado);
            gv_tablaTarea.DataSource = resultado;
            gv_tablaTarea.DataBind();
            gv_tablaTarea.SelectedIndex = -1;
        }


        protected void bt_crear_Click(object sender, EventArgs e)
        {
            int bandera = Convert.ToInt32(dd_estado.SelectedValue.ToString());
            bool estado = Convert.ToBoolean(bandera);
            if (estado)
            {
                crearActividadPersonal();
                buscarActividades("", "",  true);
                limpiar();
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Estado Cerrado') </script>");
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
                return "'" + _fecha + "'";
            }
            else
                return "null";
        }

        private void crearActividadPersonal()
        {
            try
            {
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                string tarea = tx_Actividad.Text;
                string nombreTecnico = tx_PersonalAsignado.Text;
                string fechaExpiracion = convertidorFecha(tx_FechaExpiracion.Text);

                NA_ActividadesPersonal ntareas = new NA_ActividadesPersonal();
                ntareas.insertActividades(tarea, CodUser,fechaExpiracion);


                if (!tx_PersonalAsignado.Text.Equals(""))
                {
                    int codTecnico = Convert.ToInt32(Nresp.getResponsable_SinExepcion(nombreTecnico).Tables[0].Rows[0][0].ToString());
                    int codActividad = ntareas.codultimaActividadInsertada();
                    string fechaEjecucion = convertidorFecha(tx_fechaEjecucion.Text);
                    string horaEjecucion = tx_horaejecucion.Text;
                    ntareas.insertarDetalleActividadPersonal(codActividad, codTecnico,fechaEjecucion,horaEjecucion);
                    ntareas.updateActividadAsignado(codActividad, codTecnico);
                }

            }
            catch
            {
                Response.Write("<script type='text/javascript'> alert('Error: el nombre del tecnico no Existe') </script>");
            }

        }

        protected void bt_asignarTecnico_Click(object sender, EventArgs e)
        {
            int bandera = Convert.ToInt32(dd_estado.SelectedValue.ToString());
            bool estado = Convert.ToBoolean(bandera);
            if (estado)
            {
                asignarTecnicosolo();
                buscarActividades("", "",  true);
                limpiar();
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Tarea Cerrada') </script>");
        }

        private void asignarTecnicosolo()
        {
            NA_ActividadesPersonal ntareas = new NA_ActividadesPersonal();
            string nombreTecnico = tx_PersonalAsignado.Text;
            NA_Responsables Nresp = new NA_Responsables();
            int codigoTarea = Convert.ToInt32(gv_tablaTarea.SelectedRow.Cells[1].Text);
            if (!tx_PersonalAsignado.Text.Equals(""))
               {
                    int codTecnico = Convert.ToInt32(Nresp.getResponsable_SinExepcion(nombreTecnico).Tables[0].Rows[0][0].ToString());
                        string fechaEjecucion = convertidorFecha(tx_fechaEjecucion.Text);
                        string horaEjecucion = tx_horaejecucion.Text;
                        ntareas.updateActividadAsignado(codigoTarea, codTecnico);
                        ntareas.insertarDetalleActividadPersonal(codigoTarea, codTecnico,fechaEjecucion,horaEjecucion);
                    }

                

        }

        protected void gv_tablaTarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarTecnicosAsignados();
            limpiar();
            cargarDetalleCierre();
        }

        private void cargarDetalleCierre()
        {
            if (!gv_tablaTarea.SelectedRow.Cells[8].Text.Equals("&nbsp;"))
            {
                string detalleCierre = gv_tablaTarea.SelectedRow.Cells[8].Text;
                tx_cierreTarea.Text = detalleCierre;
            }
            else
                tx_cierreTarea.Text = "";
        }

        private void cargarTecnicosAsignados()
        {

            int codtarea = Convert.ToInt32(gv_tablaTarea.SelectedRow.Cells[1].Text);
            NA_ActividadesPersonal ntareas = new NA_ActividadesPersonal();
            DataSet tuplasTecnicos = ntareas.getPersonalAsignados(codtarea);

            dd_tecnico.Items.Clear();
            dd_tecnico.DataSource = tuplasTecnicos;
            dd_tecnico.DataValueField = "codigo";
            dd_tecnico.DataTextField = "nombre";
            dd_tecnico.Items.Add(new ListItem(""));
            dd_tecnico.AppendDataBoundItems = true;
            dd_tecnico.SelectedIndex = -1;
            dd_tecnico.DataBind();
        }

        protected void dd_tecnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarDatosTecnicoSeleccionado();
        }

        private void llenarDatosTecnicoSeleccionado()
        {
            int codtarea = Convert.ToInt32(gv_tablaTarea.SelectedRow.Cells[1].Text);

            int codTecnico = Convert.ToInt32(dd_tecnico.SelectedValue.ToString());
            NA_ActividadesPersonal ntareas = new NA_ActividadesPersonal();
         //   DataSet tuplasTecnicos = ntareas.getPersonalAsignados(codtarea);

            DataSet resultado = ntareas.getPersonalAsignado(codtarea, codTecnico);
            tx_fechaTecnico.Text = resultado.Tables[0].Rows[0][0].ToString();
            tx_horaTecnico.Text = resultado.Tables[0].Rows[0][1].ToString();
            tx_observacionTecnico.Text = resultado.Tables[0].Rows[0][2].ToString();
        }

        protected void bt_actualizar_Click(object sender, EventArgs e)
        {
            int bandera = Convert.ToInt32(dd_estado.SelectedValue.ToString());
            bool estado = Convert.ToBoolean(bandera);
            if (estado)
            {
                actualizarObservacionTecnico();
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Guardar Detalle Tecnico') </script>");
        }


        private void actualizarObservacionTecnico()
        {

            int codtarea = Convert.ToInt32(gv_tablaTarea.SelectedRow.Cells[1].Text);
            int codTecnico = Convert.ToInt32(dd_tecnico.SelectedValue.ToString());
            string observacion = tx_observacionTecnico.Text;
            NA_ActividadesPersonal ntareas = new NA_ActividadesPersonal();
            ntareas.updateObservacionTecnico(codtarea, codTecnico, observacion);
        }

        protected void bt_CerrarActividad_Click(object sender, EventArgs e)
        {
            int bandera = Convert.ToInt32(dd_estado.SelectedValue.ToString());
            bool estado = Convert.ToBoolean(bandera);
            if (estado)
            {
                cerrarActividadTareas();
                buscarActividades("", "",  true);
                limpiar();
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Tarea Cerrada') </script>");
        }


        private void cerrarActividadTareas()
        {
            int codtarea = Convert.ToInt32(gv_tablaTarea.SelectedRow.Cells[1].Text);
            string observacionTarea = tx_cierreTarea.Text;
            NA_ActividadesPersonal ntareas = new NA_ActividadesPersonal();

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

           // DataSet tuplasTecnicos = ntareas.getPersonalAsignados(codtarea);
            ntareas.updateCierreTareaTecnico(codtarea, observacionTarea, CodUser);
        }

        protected void lk_buton_Click(object sender, EventArgs e)
        {
            exportarExcel();
        }


        private void exportarEn_Excel2()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string nombreArchivo = "Consultas " + Session["BaseDatos"].ToString() + " " + DateTime.Now;
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombreArchivo + ".xls" + "\"");

            GridView dg = gv_tablaTarea;
            dg.GridLines = GridLines.Both;
            dg.HeaderStyle.Font.Bold = true;
            //dg.Columns[0].Visible = false;
            dg.RenderControl(htmltextwrtter);

            Response.Write(strwritter.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


        private void exportarExcel()
        {

            string tareaTecnico = tx_Actividad.Text;
            string nombreTecnico = tx_PersonalAsignado.Text;            
            int bandera = Convert.ToInt32(dd_estado.SelectedValue.ToString());
            bool estado = Convert.ToBoolean(bandera);
            NA_ActividadesPersonal ntarea = new NA_ActividadesPersonal();
            DataSet resultado = ntarea.mostrarActividades(tareaTecnico, nombreTecnico, estado);


            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "ActividadesAsignadas_" + nombreTecnico + "-" + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.EnableViewState = false;
                    dg.AllowPaging = false;
                    dg.DataSource = resultado;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }




    }
}