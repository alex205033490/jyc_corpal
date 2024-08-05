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
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_AgendaProyectos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(84) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            
            if (!IsPostBack)
            {   
                cargarAgenda("", "", "", "", "Abierto");             
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

        private void cargarAgenda(string proyecto, string responsable, string fechadesde, string fechahasta, string estado)
        {
            NA_AgendaProyecto nagenda = new NA_AgendaProyecto();
            DataSet datosEquipo = nagenda.mostrarAgendaProyecto(responsable, proyecto, estado, fechadesde, fechahasta);
            gv_AgendaAsignacion.DataSource = datosEquipo;
            gv_AgendaAsignacion.DataBind();
        }

        protected void bt_agendaAsignacion_Click(object sender, EventArgs e)
        {
            string proyecto = tx_Proyeto_Busqueda.Text;
            string responsable = tx_responsableBusqueda.Text;
            string fechadesde = tx_fechadesdeBusqueda.Text;
            string fechahasta = tx_fechahastaBusqueda.Text;
            string estado = dd_estadobusqueda.SelectedItem.Text;
            cargarAgenda(proyecto, responsable, fechadesde, fechahasta, estado);
            limpiarTodo();
        }

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProyectos(string prefixText, int count)
        {
            string nombreProyecto = prefixText;

            NProyecto proyectoN = new NProyecto();
            DataSet tuplas = proyectoN.buscador2(nombreProyecto);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
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

        public void limpiarTodo() {
            tx_horaAsignacion.Text = "";
            tx_fechaAsignacion.Text = "";
            tx_horaexpiracion.Text = "";
            tx_fechaExpiracion.Text = "";
            tx_edificioAgenda.Text = "";
            tx_responsableAsignado.Text = "";
            tx_objetivo.Text = "";
            tx_detalleCierre.Text = "";
        }

        private void insertarNuevoAgenda()
        {
            int codProyAux = -1;
            if (gv_Proyecto.SelectedIndex > -1)
            {
                codProyAux = Convert.ToInt32(gv_Proyecto.SelectedRow.Cells[1].Text);
            }
            string horaAsignacion = tx_horaAsignacion.Text;
            string fechaAsignacion = tx_fechaAsignacion.Text;
            string horaExpiracion = tx_horaexpiracion.Text;
            string fechaExpiracion = tx_fechaExpiracion.Text;
            string edificio = tx_edificioAgenda.Text;
            string personalAsignado = tx_responsableAsignado.Text;
            string objetivoDetalle = tx_objetivo.Text;
            string estado = dd_estado.SelectedItem.Text;

            NProyecto nproy = new NProyecto();
            codProyAux = nproy.getCodigoProyect(edificio);

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUserInicio = Nresp.getCodUsuario(usuarioAux, passwordAux);

            int codRespAsignado = Nresp.getCodigo_NombreResponsable(personalAsignado);

            NA_AgendaProyecto nagenda = new NA_AgendaProyecto();
            bool bandera = nagenda.insertarAgendaProyecto(codUserInicio, fechaAsignacion, horaAsignacion, personalAsignado, codRespAsignado, objetivoDetalle, estado, codProyAux, edificio, horaExpiracion, fechaExpiracion);
            if (bandera)
            {
                cargarAgenda("", personalAsignado, "", "", estado);
                Response.Write("<script type='text/javascript'> alert('Guardado: Ok') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Guardar') </script>");
        }

        private void eliminarDatos()
        {
            if (gv_AgendaAsignacion.SelectedIndex > -1)
            {
                int codigoAgenda = Convert.ToInt32(gv_AgendaAsignacion.SelectedRow.Cells[1].Text);
                NA_AgendaProyecto nagenda = new NA_AgendaProyecto();
                bool bandera = nagenda.eliminarAgenda(codigoAgenda);
                if (bandera)
                {

                    cargarAgenda("", "", "", "", "Abierto");
                    Response.Write("<script type='text/javascript'> alert('Eliminado: OK') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Eliminado: Error') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccione Dato Agenda') </script>");
        }

        public void seleccionarDatos() {
            if (gv_AgendaAsignacion.SelectedIndex > -1)
            {
                NA_AgendaProyecto nagenda = new NA_AgendaProyecto();
                int codigoAgenda = Convert.ToInt32(gv_AgendaAsignacion.SelectedRow.Cells[1].Text);
                DataSet dato = nagenda.getAgendaporCodigo(codigoAgenda);
                tx_horaAsignacion.Text = dato.Tables[0].Rows[0][2].ToString();
                tx_fechaAsignacion.Text = dato.Tables[0].Rows[0][1].ToString();
                tx_horaexpiracion.Text = dato.Tables[0].Rows[0][10].ToString();
                tx_fechaExpiracion.Text = dato.Tables[0].Rows[0][9].ToString();
                tx_edificioAgenda.Text = dato.Tables[0].Rows[0][3].ToString();
                tx_responsableAsignado.Text = dato.Tables[0].Rows[0][5].ToString();
                tx_objetivo.Text = dato.Tables[0].Rows[0][4].ToString();
                tx_detalleCierre.Text = dato.Tables[0].Rows[0][7].ToString();
                dd_estado.SelectedValue = dato.Tables[0].Rows[0][6].ToString();
            }
        }

        private void actualizarDatosAgenda()
        {
            if (gv_AgendaAsignacion.SelectedIndex > -1)
            {
                int codigoAgenda = Convert.ToInt32(gv_AgendaAsignacion.SelectedRow.Cells[1].Text);
                string horaAsignacion = tx_horaAsignacion.Text;
                string fechaAsignacion = tx_fechaAsignacion.Text;
                string horaExpiracion = tx_horaexpiracion.Text;
                string fechaExpiracion = tx_fechaExpiracion.Text;
                string edificio = tx_edificioAgenda.Text;
                string responsableAsignado = tx_responsableAsignado.Text;
                string objetivo = tx_objetivo.Text;
                string estado = dd_estado.SelectedItem.Text;
                string detalleCierre = tx_detalleCierre.Text;
                
                NProyecto nproy = new NProyecto();
               int codProyAux = nproy.getCodigoProyect(edificio);

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUserInicio = Nresp.getCodUsuario(usuarioAux, passwordAux);

                int codRespAsignado = Nresp.getCodigo_NombreResponsable(responsableAsignado);
                
                NA_AgendaProyecto nagenda = new NA_AgendaProyecto();
                bool bandera = nagenda.updateAgendaProyecto(codigoAgenda, codUserInicio, fechaAsignacion, horaAsignacion, objetivo, responsableAsignado, codRespAsignado, estado, codProyAux, edificio, horaExpiracion, fechaExpiracion, detalleCierre, codUserInicio);
                if (bandera)
                {
                    cargarAgenda("", responsableAsignado, "", "", dd_estadobusqueda.SelectedItem.Text);
                    Response.Write("<script type='text/javascript'> alert('Guardado: Ok') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Guardar') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccione Dato Agenda') </script>");
        }

        protected void bt_modificarAgenda_Click(object sender, EventArgs e)
        {
            actualizarDatosAgenda();
        }

        protected void bt_nuevoAgenda_Click(object sender, EventArgs e)
        {
            insertarNuevoAgenda();
        }

        protected void bt_eliminarAgenda_Click(object sender, EventArgs e)
        {
            eliminarDatos();
        }

        protected void gv_AgendaAsignacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarDatos();
        }

        protected void bt_limpiarDatos_Click(object sender, EventArgs e)
        {
            limpiarTodo();
        }

        protected void gv_Proyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gv_Proyecto.SelectedIndex > -1)
            {
                tx_edificioAgenda.Text = gv_Proyecto.SelectedRow.Cells[3].Text;
            }
        }

        protected void lk_exportarExcel_Click(object sender, EventArgs e)
        {
            exportarEn_Excel2();
        }


        private void exportarAExcel()
        {
            string responsable = tx_responsableBusqueda.Text;
            string edificio = tx_Proyeto_Busqueda.Text;
            string estado = dd_estadobusqueda.SelectedItem.Text;
            string fechaDesde = tx_fechadesdeBusqueda.Text;
            string fechaHasta = tx_fechahastaBusqueda.Text;

            NA_AgendaProyecto nagenda = new NA_AgendaProyecto();
            DataSet datosEquipo = nagenda.mostrarAgendaProyecto(responsable, edificio, estado, fechaDesde, fechaHasta);


            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Agenda Proyecto " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = datosEquipo;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }
        }

        private void buscarProyecto(string nombreProyecto)
        {
            NProyecto pp = new NProyecto();
            DataSet dato = pp.getProyectoTodos_like(nombreProyecto);
            gv_Proyecto.DataSource = dato;
            gv_Proyecto.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string nombreProyecto = tx_Proyeto_Busqueda.Text;
            buscarProyecto(nombreProyecto);
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
            string nombreArchivo = "Agenda Proyecto " + Session["BaseDatos"].ToString() + " " + DateTime.Now;
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombreArchivo + ".xls" + "\"");

            GridView dg = gv_AgendaAsignacion;
            dg.GridLines = GridLines.Both;
            dg.HeaderStyle.Font.Bold = true;
            dg.Columns[0].Visible = false;
            dg.RenderControl(htmltextwrtter);

            Response.Write(strwritter.ToString());
            Response.End();
        }

    }
}