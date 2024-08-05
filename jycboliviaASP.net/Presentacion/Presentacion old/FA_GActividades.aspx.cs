using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using System.Text;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_GActividades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(50) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if(!IsPostBack){
                dd_estado.SelectedIndex = 0;
                buscarTareas("", "", "", true);
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

        public void limpiar() {
            tx_tareaTecnico.Text = "";
            tx_tecnicoNombre.Text = "";
            tx_edificio.Text = "";
            tx_fechaTecnico.Text = "";
            tx_horaTecnico.Text = "";
            tx_observacionTecnico.Text = "";
            tx_cierreTarea.Text = "";
        }


        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProyectos2(string prefixText, int count)
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


        public void buscarTareas(string detalleTarea, string nombreTecnico, string nombreEdificio, bool estado)
        {
            NA_tareasTecnico ntarea = new NA_tareasTecnico();
            DataSet resultado = ntarea.mostrarTareas(detalleTarea, nombreTecnico, nombreEdificio, estado);
            gv_tablaTarea.DataSource = resultado; 
            gv_tablaTarea.DataBind();
            gv_tablaTarea.SelectedIndex = -1;
        }


     
        protected void bt_crear_Click(object sender, EventArgs e)
        {
            int bandera = Convert.ToInt32(dd_estado.SelectedValue.ToString());
            bool estado = Convert.ToBoolean(bandera);
            if(estado){
                crearTareaTecnico();
                buscarTareas("", "" ,"", true);
                limpiar();
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Estado Cerrado') </script>");
        }

        private void crearTareaTecnico()
        {
            try
            {
             NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            string tarea = tx_tareaTecnico.Text;
            string nombreTecnico = tx_tecnicoNombre.Text;
            string edificio = tx_edificio.Text;
                       
            NProyecto Nproy = new NProyecto();
            int codproy = -1;
            if (Nproy.buscar(edificio).Tables[0].Rows.Count > 0)
            {
                codproy = Convert.ToInt32(Nproy.buscar(edificio).Tables[0].Rows[0][0].ToString());
            }


            NA_tareasTecnico ntareas = new NA_tareasTecnico();
            ntareas.insertTareas(tarea, CodUser,codproy, edificio);


                if(!tx_tecnicoNombre.Text.Equals("")){
                    int codTecnico = Convert.ToInt32(Nresp.getResponsable_SinExepcion(nombreTecnico).Tables[0].Rows[0][0].ToString());
                    int codTarea = ntareas.codultimaTareaInsertada();
                    ntareas.insertarDetalleTareaTecnico(codTarea, codTecnico);
                    ntareas.updateTareaTecnicoAsignado(codTarea, codTecnico);
                }
                
            }
            catch {
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
                buscarTareas("", "", "", true);
                limpiar();
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Tarea Cerrada') </script>");
        }

        private void asignarTecnicosolo()
        {
            NA_tareasTecnico ntareas = new NA_tareasTecnico();
            string nombreTecnico = tx_tecnicoNombre.Text;
            NA_Responsables Nresp = new NA_Responsables();   
         if(gv_tablaTarea.SelectedIndex >- 1){

             try
             {
                 int codigoTarea = Convert.ToInt32(gv_tablaTarea.SelectedRow.Cells[1].Text);
                 if (!tx_tecnicoNombre.Text.Equals(""))
                 {
                     int codTecnico = Convert.ToInt32(Nresp.getResponsable_SinExepcion(nombreTecnico).Tables[0].Rows[0][0].ToString());
                     
                     ntareas.updateTareaTecnicoAsignado(codigoTarea, codTecnico);
                     ntareas.insertarDetalleTareaTecnico(codigoTarea, codTecnico);
                 }

             }
             catch
             {
                 Response.Write("<script type='text/javascript'> alert('Error: el nombre del tecnico no Existe') </script>");
             }
         }else
             Response.Write("<script type='text/javascript'> alert('Error: No selecciono Tarea') </script>");



        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string tareaTecnico = tx_tareaTecnico.Text;
            string nombreTecnico = tx_tecnicoNombre.Text;
            string nombreEdificio = tx_edificio.Text;
            int bandera = Convert.ToInt32(dd_estado.SelectedValue.ToString());
            bool estado = Convert.ToBoolean(bandera);
            buscarTareas(tareaTecnico, nombreTecnico, nombreEdificio,estado);
        }

        protected void gv_tablaTarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarTecnicosAsignados();
            limpiar();
        }

        private void cargarTecnicosAsignados()
        {
            
            int codtarea = Convert.ToInt32(gv_tablaTarea.SelectedRow.Cells[1].Text);
            NA_tareasTecnico ntareas = new NA_tareasTecnico();
            DataSet tuplasTecnicos = ntareas.getTecnicosAsignados(codtarea);

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
            NA_tareasTecnico ntareas = new NA_tareasTecnico();
            DataSet tuplasTecnicos = ntareas.getTecnicosAsignados(codtarea);
            
            DataSet resultado = ntareas.getTecnicoAsignado(codtarea, codTecnico);
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
              }else
                Response.Write("<script type='text/javascript'> alert('Error: Tarea Cerrada') </script>");
        }

        private void actualizarObservacionTecnico()
        {

            int codtarea = Convert.ToInt32(gv_tablaTarea.SelectedRow.Cells[1].Text);
            int codTecnico = Convert.ToInt32(dd_tecnico.SelectedValue.ToString());
            string observacion = tx_observacionTecnico.Text;
            NA_tareasTecnico ntareas = new NA_tareasTecnico();            
            ntareas.updateObservacionTecnico(codtarea, codTecnico, observacion);
        }

        protected void bt_CerrarActividad_Click(object sender, EventArgs e)
        {
            int bandera = Convert.ToInt32(dd_estado.SelectedValue.ToString());
            bool estado = Convert.ToBoolean(bandera);
            if (estado)
            {
                cerrarActividadTareas();
                buscarTareas("", "", "",true);
                limpiar();
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Tarea Cerrada') </script>");
        }

        private void cerrarActividadTareas()
        {
            int codtarea = Convert.ToInt32(gv_tablaTarea.SelectedRow.Cells[1].Text);
            string observacionTarea = tx_cierreTarea.Text;
            NA_tareasTecnico ntareas = new NA_tareasTecnico();

             NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            DataSet tuplasTecnicos = ntareas.getTecnicosAsignados(codtarea);
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

            string tareaTecnico = tx_tareaTecnico.Text;
            string nombreTecnico = tx_tecnicoNombre.Text;
            string nombreEdificio = tx_edificio.Text;
            int bandera = Convert.ToInt32(dd_estado.SelectedValue.ToString());
            bool estado = Convert.ToBoolean(bandera);
            NA_tareasTecnico ntarea = new NA_tareasTecnico();
            DataSet resultado = ntarea.mostrarTareas(tareaTecnico, nombreTecnico, nombreEdificio, estado);
           

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "TareasAsignadas_"+nombreTecnico+"-" + Session["BaseDatos"].ToString();
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

        protected void bt_crearCoti_Click(object sender, EventArgs e)
        {
            if(gv_tablaTarea.SelectedIndex > -1){
                Session["codTarea"] = gv_tablaTarea.SelectedRow.Cells[1].Text;
                Session["nombreEdificioTarea"] = gv_tablaTarea.SelectedRow.Cells[2].Text;
                Session["banderatarea"] = true;
                Response.Redirect("../Presentacion/FA_AdicionarCotizacionRepuesto.aspx");            
            }else
                 Response.Write("<script type='text/javascript'> alert('Error: No ha seleccionado Tarea') </script>");
        }

    }
}