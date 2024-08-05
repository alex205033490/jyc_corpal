using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_agendanegociacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(80) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            
            if (!IsPostBack)
            {
             //   ponerResponsablePorDefecto();
                buscarProyecto("");
                cargarAgenda("", "", "", "", "Abierto");
                desactivarBotonesGestionar();
            }
            
        }

        private void desactivarBotonesGestionar()
        {
            if (tienePermisoDeIngreso(81) == false)
            {
                bt_eliminarAgenda.Enabled = false;
                bt_modificarAgenda.Enabled = false;
                bt_nuevoAgenda.Enabled = false;
            }
            else {
                bt_eliminarAgenda.Enabled = true;
                bt_modificarAgenda.Enabled = true;
                bt_nuevoAgenda.Enabled = true;
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

        private void ponerResponsablePorDefecto()
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            DataSet datoResp = Nresp.get_responsable(codUser);
            tx_responsableAsignado.Text = datoResp.Tables[0].Rows[0][1].ToString();
        }

        protected void bt_buscarProyecto_Click(object sender, EventArgs e)
        {
            string nombreProyecto = tx_Proyeto.Text;
            buscarProyecto(nombreProyecto);
        }

        private void buscarProyecto(string nombreProyecto)
        {
            NProyecto pp = new NProyecto();
            DataSet dato = pp.getProyectoTodos_like(nombreProyecto);
            gv_Proyecto.DataSource = dato;
            gv_Proyecto.DataBind();
        }

        private void cargarAgenda(string proyecto, string responsable, string fechadesde, string fechahasta, string estado)
        {
            NA_AgendaNegociacion nagenda = new NA_AgendaNegociacion();
            DataSet datosEquipo = nagenda.mostrarAgendaNegociacion(responsable, proyecto, estado, fechadesde, fechahasta);
            gv_AgendaAsignacion.DataSource = datosEquipo;
            gv_AgendaAsignacion.DataBind();
        }

        protected void bt_agendaAsignacion_Click(object sender, EventArgs e)
        {
            string responsable = tx_responsableBusqueda.Text;
            string edificio = tx_Proyeto.Text;
            string estado = dd_estadobusqueda.SelectedItem.Text;
            string fechaDesde = tx_fechadesdeBusqueda.Text;
            string fechaHasta = tx_fechahastaBusqueda.Text;
            cargarAgenda(edificio, responsable, fechaDesde, fechaHasta, estado);
        }

      
        protected void bt_limpiarDatos_Click(object sender, EventArgs e)
        {
            tx_horaAsignacion.Text = "";
            tx_fechaAsignacion.Text = "";
            tx_horaexpiracion.Text = "";
            tx_fechaExpiracion.Text = "";
            tx_edificioAgenda.Text = "";
            tx_responsableAsignado.Text = "";
            tx_objetivo.Text = "";
            tx_detalleCierre.Text = "";
        }

        protected void bt_nuevoAgenda_Click(object sender, EventArgs e)
        {
            insertarNuevoAgenda();
        }

        private void insertarNuevoAgenda()
        {
            int codProyAux = -1;
            if(gv_Proyecto.SelectedIndex > -1){
                codProyAux = Convert.ToInt32(gv_Proyecto.SelectedRow.Cells[1].Text);
            }
           string horaAsignacion = tx_horaAsignacion.Text ;
           string fechaAsignacion = tx_fechaAsignacion.Text ;
           string horaExpiracion = tx_horaexpiracion.Text ;
           string fechaExpiracion = tx_fechaExpiracion.Text;
           string edificio = tx_edificioAgenda.Text;
           string responsable = tx_responsableAsignado.Text;
           string objetivo = tx_objetivo.Text;
           string estado = dd_estado.SelectedItem.Text;

           NProyecto nproy = new NProyecto();
           codProyAux = nproy.getCodigoProyect(edificio);

           NA_AgendaNegociacion nagenda = new NA_AgendaNegociacion();
           bool bandera = nagenda.insertarAgenda(fechaAsignacion, horaAsignacion, objetivo, responsable, estado, codProyAux, edificio,horaExpiracion,fechaExpiracion);
            if(bandera){
                cargarAgenda(edificio, responsable, "", "", estado);
                Response.Write("<script type='text/javascript'> alert('Guardado: Ok') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Guardar') </script>"); 
        }


        private void actualizarDatosAgenda()
        {
           if(gv_AgendaAsignacion.SelectedIndex > -1){
               int codigoAgenda = Convert.ToInt32(gv_AgendaAsignacion.SelectedRow.Cells[1].Text);
               string horaAsignacion = tx_horaAsignacion.Text;
               string fechaAsignacion = tx_fechaAsignacion.Text;
               string horaExpiracion = tx_horaexpiracion.Text;
               string fechaExpiracion = tx_fechaExpiracion.Text;
               string edificio = tx_edificioAgenda.Text;
               string responsable = tx_responsableAsignado.Text;
               string objetivo = tx_objetivo.Text;
               string estado = dd_estado.SelectedItem.Text;
               string detalleCierre = tx_detalleCierre.Text;
               NA_AgendaNegociacion nagenda = new NA_AgendaNegociacion();
               bool bandera = nagenda.updateAgenda(codigoAgenda,fechaAsignacion, horaAsignacion, objetivo, responsable, estado, 1, edificio, horaExpiracion, fechaExpiracion,detalleCierre);
               if (bandera)
               {
                   cargarAgenda("", "", "", "", dd_estadobusqueda.SelectedItem.Text);
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

        protected void bt_eliminarAgenda_Click(object sender, EventArgs e)
        {
            eliminarDatos();
        }

        private void eliminarDatos()
        {
            if(gv_AgendaAsignacion.SelectedIndex > -1){
                int codigoAgenda = Convert.ToInt32(gv_AgendaAsignacion.SelectedRow.Cells[1].Text);
                NA_AgendaNegociacion nagenda = new NA_AgendaNegociacion();
                bool bandera = nagenda.eliminarAgenda(codigoAgenda);
                if(bandera){

                    cargarAgenda("", "", "", "", "Abierto");
                    Response.Write("<script type='text/javascript'> alert('Eliminado: OK') </script>");
                }else
                    Response.Write("<script type='text/javascript'> alert('Eliminado: Error') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccione Dato Agenda') </script>");
        }

        protected void gv_AgendaAsignacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarDatosAgenda();
        }

        private void seleccionarDatosAgenda()
        {
            if(gv_AgendaAsignacion.SelectedIndex > -1){
                NA_AgendaNegociacion nagenda = new NA_AgendaNegociacion();
                int codigoAgenda = Convert.ToInt32(gv_AgendaAsignacion.SelectedRow.Cells[1].Text);
                DataSet dato =  nagenda.getAgendaporCodigo(codigoAgenda);
                tx_horaAsignacion.Text = dato.Tables[0].Rows[0][2].ToString();
                tx_fechaAsignacion.Text = dato.Tables[0].Rows[0][1].ToString();
                tx_horaexpiracion.Text = dato.Tables[0].Rows[0][14].ToString();
                tx_fechaExpiracion.Text = dato.Tables[0].Rows[0][15].ToString();
                tx_edificioAgenda.Text = dato.Tables[0].Rows[0][3].ToString();
                tx_responsableAsignado.Text = dato.Tables[0].Rows[0][11].ToString();
                tx_objetivo.Text = dato.Tables[0].Rows[0][10].ToString();
                tx_detalleCierre.Text = dato.Tables[0].Rows[0][13].ToString();
            }
            
        }

        protected void gv_Proyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarProyecto();
        }

        private void seleccionarProyecto()
        {
           if(gv_Proyecto.SelectedIndex > -1){
               tx_edificioAgenda.Text = gv_Proyecto.SelectedRow.Cells[3].Text;
           }
        }

        protected void lk_exportarExcel_Click(object sender, EventArgs e)
        {
            exportarAExcel();
        }

        private void exportarAExcel()
        {
            string responsable = tx_responsableBusqueda.Text;
            string edificio = tx_Proyeto.Text;
            string estado = dd_estadobusqueda.SelectedItem.Text;
            string fechaDesde = tx_fechadesdeBusqueda.Text;
            string fechaHasta = tx_fechahastaBusqueda.Text;

            NA_AgendaNegociacion nagenda = new NA_AgendaNegociacion();
            DataSet datosEquipo = nagenda.mostrarAgendaNegociacion(responsable, edificio, estado, fechaDesde, fechaHasta);
           

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Agenda Negociacion " + Session["BaseDatos"].ToString();
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


        

    }
}