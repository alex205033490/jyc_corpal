using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Drawing;
using System.IO;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CallCenterEstadisticaNormal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(55) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            lb_eventos.Text = "Estadistica Call Center " + Session["BaseDatos"].ToString();
            tx_BaseDeDatos.Text = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                llenarTipoEvento();
                buscarEventos("", "", "", "", "", "");
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

        public void llenarTipoEvento()
        {
            NA_TipoEvento NtipoEvento = new NA_TipoEvento();
            dd_tipoEvento1.Items.Add(new ListItem(""));
            dd_tipoEvento1.SelectedIndex = -1;
            dd_tipoEvento1.DataSource = NtipoEvento.mostrarAllDatos();
            dd_tipoEvento1.DataValueField = "codigo";
            dd_tipoEvento1.DataTextField = "nombre";            
            dd_tipoEvento1.AppendDataBoundItems = true;            
            dd_tipoEvento1.DataBind();

        }

        public void buscarEventos(string nombreEdificio, string nombretipoLLamada, string semana, string estado,string fechaDesde,string fechaHasta)
        {
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = Nevento.estadisticaNormalCallCenter(nombreEdificio, nombretipoLLamada, semana, estado,fechaDesde,fechaHasta,false);
            gv_tablaEventos.DataSource = resultado;
            gv_tablaEventos.DataBind();
            ponerColoresAequiposConPrioridad(gv_tablaEventos);
            tx_cantidadEventos.Text = gv_tablaEventos.Rows.Count.ToString();
        }

        public void mostrarEventos(DataSet listaEventos) {
            gv_tablaEventos.DataSource = listaEventos;
            gv_tablaEventos.DataBind();
            ponerColoresAequiposConPrioridad(gv_tablaEventos);
            tx_cantidadEventos.Text = gv_tablaEventos.Rows.Count.ToString();
        }

        public void mostrarTecnicos(DataSet listaEventos)
        {
            gv_Tecnicos.DataSource = listaEventos;
            gv_Tecnicos.DataBind();                       
        }

        public void ponerColoresAequiposConPrioridad(GridView gv_tablaDatosAux)
        {
            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++)
            {
                string Prioridad = gv_tablaEventos.Rows[i].Cells[6].Text;
                if (Prioridad.Equals("Alta"))
                {
                    gv_tablaDatosAux.Rows[i].BackColor = Color.Red;
                    gv_tablaDatosAux.Rows[i].ForeColor = Color.White;
                }
                else
                    if (Prioridad.Equals("Media"))
                    {
                        gv_tablaDatosAux.Rows[i].BackColor = Color.Yellow;
                        gv_tablaDatosAux.Rows[i].ForeColor = Color.Black;
                    }
                    else
                        if (Prioridad.Equals("Baja"))
                        {
                            gv_tablaDatosAux.Rows[i].BackColor = Color.Green;
                            gv_tablaDatosAux.Rows[i].ForeColor = Color.Black;
                        }
                gv_tablaDatosAux.Rows[i].Cells[0].BackColor = Color.White;
                gv_tablaDatosAux.Rows[i].Cells[0].ForeColor = Color.Black;
            }
        }

        protected void bt_Buscar_Click(object sender, EventArgs e)
        {
            buscarEventos();
            mostrarTecnicos(null);
        }

        public string convertidorFecha(string fecha)
        {
            if (fecha != "" || fecha != null)
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return  _fecha ;
            }
            else
                return "null";
        }

        private void buscarEventos()
        {
            string fechaDesde = tx_desdeBusqueda.Text;
            string fechaHasta = tx_hastaBusqueda.Text;
            NA_Evento Nevento = new NA_Evento();

            if (!fechaDesde.Equals("") && !fechaHasta.Equals(""))
            {
                string fechaDesdeAux = convertidorFecha(fechaDesde);
                string fechaHastaAux = convertidorFecha(fechaHasta);
                DataSet resultado = Nevento.estadisticaNormalCallCenter(tx_nombreEdificioBusqueda.Text, dd_tipoEvento1.SelectedItem.Text, tx_SemanaBusqueda.Text, dd_evento.SelectedItem.Text, fechaDesdeAux, fechaHastaAux,false);
                mostrarEventos(resultado);
            }
            else {
                DataSet resultado = Nevento.estadisticaNormalCallCenter(tx_nombreEdificioBusqueda.Text, dd_tipoEvento1.SelectedItem.Text, tx_SemanaBusqueda.Text, dd_evento.SelectedItem.Text, "", "",false);
                mostrarEventos(resultado);
            }
               

        }

        public void cambiarBaseDeDatos(string departamento)
        {
            string BaseDatos = departamento;
            switch (BaseDatos)
            {
                case "Prueba":
                    Session["NombreBaseDatos"] = "db_prueba";
                    Session["BaseDatos"] = "Prueba";
                    break;
                case "Santa Cruz":
                    Session["NombreBaseDatos"] = "db_SantaCruz";
                    Session["BaseDatos"] = "Santa Cruz";
                    break;
                case "La Paz":
                    Session["NombreBaseDatos"] = "db_LaPaz";
                    Session["BaseDatos"] = "La Paz";
                    break;
                case "Cochabamba":
                    Session["NombreBaseDatos"] = "db_Cochabamba";
                    Session["BaseDatos"] = "Cochabamba";
                    break;
                case "Sucre":
                    Session["NombreBaseDatos"] = "db_Sucre";
                    Session["BaseDatos"] = "Sucre";
                    break;
                case "Oruro":
                    Session["NombreBaseDatos"] = "db_Oruro";
                    Session["BaseDatos"] = "Oruro";
                    break;
                case "Potosi":
                    Session["NombreBaseDatos"] = "db_Potosi";
                    Session["BaseDatos"] = "Potosi";
                    break;
                case "Tarija":
                    Session["NombreBaseDatos"] = "db_Tarija";
                    Session["BaseDatos"] = "Tarija";
                    break;
                case "Yacuiba":
                    Session["NombreBaseDatos"] = "db_Yacuiba";
                    Session["BaseDatos"] = "Yacuiba";
                    break;
                case "Villamontes":
                    Session["NombreBaseDatos"] = "db_Villamontes";
                    Session["BaseDatos"] = "Villamontes";
                    break;
                case "Asuncion-Paraguay":
                    Session["NombreBaseDatos"] = "db_Paraguay";
                    Session["BaseDatos"] = "Asuncion-Paraguay";
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

        protected void dd_baseDeDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dpto = dd_baseDeDatos.SelectedValue.ToString();
            lb_eventos.Text = "Eventos " + dpto;
            //Response.Write("<script type='text/javascript'> alert('Error: " + dpto + "') </script>");
            // Response.Write("<script type='text/javascript'> alert('Error: usuario') </script>");
            cambiarBaseDeDatos(dpto);
            Response.Redirect("../Presentacion/FA_CallCenterEstadisticaNormal.aspx");  
        }

        protected void gv_tablaEventos_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarEvento();
        }

        private void seleccionarEvento()
        {
            int index = gv_tablaEventos.SelectedIndex;
            if(index > -1){
                int codEvento = Convert.ToInt32(gv_tablaEventos.SelectedRow.Cells[1].Text);
                 NA_Evento Nevento = new NA_Evento();
                 DataSet resultado = Nevento.estadisticaCallCenterNormalTecnicosEvento(codEvento);
                 mostrarTecnicos(resultado);
            }
        }

        protected void lk_excelEventos_Click(object sender, EventArgs e)
        {

            string fechaDesde = tx_desdeBusqueda.Text;
            string fechaHasta = tx_hastaBusqueda.Text;
            NA_Evento Nevento = new NA_Evento();
            DataSet resultado = null;
            if (!fechaDesde.Equals("") && !fechaHasta.Equals(""))
            {
                string fechaDesdeAux = convertidorFecha(fechaDesde);
                string fechaHastaAux = convertidorFecha(fechaHasta);
                resultado = Nevento.estadisticaNormalCallCenter(tx_nombreEdificioBusqueda.Text, dd_tipoEvento1.SelectedItem.Text, tx_SemanaBusqueda.Text, dd_evento.SelectedItem.Text, fechaDesdeAux, fechaHastaAux, true);               
            }
            else
            {
                resultado = Nevento.estadisticaNormalCallCenter(tx_nombreEdificioBusqueda.Text, dd_tipoEvento1.SelectedItem.Text, tx_SemanaBusqueda.Text, dd_evento.SelectedItem.Text, "", "", true);               
            }



            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Eventos Estadistica Normal - " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = resultado;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }     
        }

        protected void lk_exceltecnicos_Click(object sender, EventArgs e)
        {

            int index = gv_tablaEventos.SelectedIndex;
            DataSet resultado = null;
            if (index > -1)
            {
                int codEvento = Convert.ToInt32(gv_tablaEventos.SelectedRow.Cells[1].Text);
                NA_Evento Nevento = new NA_Evento();
                resultado = Nevento.estadisticaCallCenterNormalTecnicosEvento(codEvento);               
            }

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Eventos Estadistica Tecnico - " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
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