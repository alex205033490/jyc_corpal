using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ConsultasCallCenter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(56) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            lb_titulo.Text = "Consultas Call Center - " + Session["BaseDatos"].ToString();
            if(!IsPostBack){
            llenarConsultas();                                
            }            
            buscarPorConsulta();
            ponercolor();
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
            tx_edificio.Text = "";
            tx_fechaDesde.Text = "";
            tx_fechahasta.Text = "";
        }
             

        private void llenarConsultas()
        {   
            dd_consulta.DataValueField = "codigo";
            dd_consulta.DataTextField = "nombre";
            dd_consulta.Items.Add(new ListItem("", "0"));
            dd_consulta.Items.Add(new ListItem("Ascensores Parados", "1"));
            dd_consulta.Items.Add(new ListItem("Informe de Atencion", "2"));
            dd_consulta.Items.Add(new ListItem("Trimestral", "3"));
            dd_consulta.Items.Add(new ListItem("Cuadro de Indicadores", "4"));
            dd_consulta.Items.Add(new ListItem("Consulta RCC", "5"));
            dd_consulta.Items.Add(new ListItem("Consulta RIN", "6"));
            dd_consulta.AppendDataBoundItems = true;
            dd_consulta.SelectedIndex = -1;
            dd_consulta.DataBind();        
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            
            buscarPorConsulta();
            ponercolor();
        }

        private void ponercolor()
        {

             int IdConsulta = dd_consulta.SelectedIndex;
             if (IdConsulta == 3) {
                 for (int i = 0; i < gv_consulta.Rows.Count; i++)
                 {
                     string banco = gv_consulta.Rows[i].Cells[0].Text;
                     if (banco.Equals("Santa Cruz") || banco.Equals("Cochabamba")
                         || banco.Equals("La Paz") || banco.Equals("JyC SRL") ||
                         banco.Equals("JyCIA SRL") || banco.Equals("Imven"))
                     {
                         gv_consulta.Rows[i].BackColor = Color.Red;
                         gv_consulta.Rows[i].ForeColor = Color.White;
                     }
                 } 
             }

             if (IdConsulta == 4) {
                 int UnidadNegocio = 3;
                 gv_consulta.Rows[0].BackColor = Color.Red;
                 gv_consulta.Rows[0].ForeColor = Color.White;

                 gv_consulta.Rows[UnidadNegocio+1].BackColor = Color.Red;
                 gv_consulta.Rows[UnidadNegocio+1].ForeColor = Color.White;

                 gv_consulta.Rows[(UnidadNegocio * 2) + 2].BackColor = Color.Red;
                 gv_consulta.Rows[(UnidadNegocio * 2) + 2].ForeColor = Color.White;

                 gv_consulta.Rows[(UnidadNegocio * 3) + 3].BackColor = Color.Red;
                 gv_consulta.Rows[(UnidadNegocio * 3) + 3].ForeColor = Color.White;

                 gv_consulta.Rows[(UnidadNegocio * 4) + 4].BackColor = Color.Red;
                 gv_consulta.Rows[(UnidadNegocio * 4) + 4].ForeColor = Color.White;

                 gv_consulta.Rows[(UnidadNegocio * 5) + 5].BackColor = Color.Red;
                 gv_consulta.Rows[(UnidadNegocio * 5) + 5].ForeColor = Color.White;

                 gv_consulta.Rows[(UnidadNegocio * 6) + 6].BackColor = Color.Red;
                 gv_consulta.Rows[(UnidadNegocio * 6) + 6].ForeColor = Color.White; 
             }

            
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

        private void buscarPorConsulta()
        {
            if ( !tx_fechaDesde.Text.Equals("") && !tx_fechahasta.Text.Equals("") )
            {  
            int IdConsulta = dd_consulta.SelectedIndex;
            NA_Evento evento = new NA_Evento();
            DataSet tuplas = null;
            
            if (IdConsulta == 1)
                tuplas = evento.getEventoConAscensorParado(convertidorFecha(tx_fechaDesde.Text), convertidorFecha(tx_fechahasta.Text));
            if(IdConsulta == 2)
                tuplas = evento.getInformeDeAtencionCallCenter(convertidorFecha(tx_fechaDesde.Text), convertidorFecha(tx_fechahasta.Text), Session["BaseDatos"].ToString(), tx_edificio.Text);
            if(IdConsulta == 3)
                tuplas = evento.get_Trimestral_Eventos(tx_fechaDesde.Text, tx_fechahasta.Text);
            if (IdConsulta == 4)
                tuplas = evento.get_CuadroIndicadores(tx_fechaDesde.Text, tx_fechahasta.Text);
            if (IdConsulta == 5)
                tuplas = evento.get_CuadroIndicadoresRCC(convertidorFecha(tx_fechaDesde.Text), convertidorFecha(tx_fechahasta.Text), Session["BaseDatos"].ToString());
            if (IdConsulta == 6)
                tuplas = evento.get_CuadroIndicadoresRIN(convertidorFecha(tx_fechaDesde.Text), convertidorFecha(tx_fechahasta.Text), Session["BaseDatos"].ToString());

            gv_consulta.DataSource = tuplas;   
            
            gv_consulta.DataBind();
         }
        }

        protected void linkb_excel_Click(object sender, EventArgs e)
        {
            exportarEn_Excel2();
        }


        private void exportarExcel()
        {

            int IdConsulta = dd_consulta.SelectedIndex;
            NA_Evento evento = new NA_Evento();
            DataSet tuplas = null;

            if (IdConsulta == 1)
                tuplas = evento.getEventoConAscensorParado(convertidorFecha(tx_fechaDesde.Text), convertidorFecha(tx_fechahasta.Text));
            if (IdConsulta == 2)
                tuplas = evento.getInformeDeAtencionCallCenter(convertidorFecha(tx_fechaDesde.Text), convertidorFecha(tx_fechahasta.Text), Session["BaseDatos"].ToString(), tx_edificio.Text);
            if (IdConsulta == 3)
                tuplas = evento.get_Trimestral_Eventos(tx_fechaDesde.Text, tx_fechahasta.Text);
            if (IdConsulta == 4)
                tuplas = evento.get_CuadroIndicadores(tx_fechaDesde.Text, tx_fechahasta.Text);
            if (IdConsulta == 5)
                tuplas = evento.get_CuadroIndicadoresRCC(convertidorFecha(tx_fechaDesde.Text), convertidorFecha(tx_fechahasta.Text), Session["BaseDatos"].ToString());
            if (IdConsulta == 6)
                tuplas = evento.get_CuadroIndicadoresRIN(convertidorFecha(tx_fechaDesde.Text), convertidorFecha(tx_fechahasta.Text), Session["BaseDatos"].ToString());

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "ConsultaCallCenter - " + Session["BaseDatos"].ToString();
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
                    dg.DataSource = tuplas;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
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

            GridView dg = gv_consulta;
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



        public int CalcularMesesDeDiferencia(string fechaDesde1, string fechaHasta2)
        {
            DateTime fechaDesde = Convert.ToDateTime(fechaDesde1);
            DateTime fechaHasta = Convert.ToDateTime(fechaHasta2);
            return Math.Abs((fechaDesde.Month - fechaHasta.Month) + 12 * (fechaDesde.Year - fechaHasta.Year));
        }


        protected void gv_consulta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int IdConsulta = dd_consulta.SelectedIndex;
                
                DateTime fecha1_aux = Convert.ToDateTime(tx_fechaDesde.Text);
                DateTime fecha2_aux = Convert.ToDateTime(tx_fechahasta.Text);
                int diaDesde = fecha1_aux.Day;
                int mesDesde = fecha1_aux.Month;
                int anioDesde = fecha1_aux.Year;
                int diaHasta = fecha2_aux.Day;
                int mesHasta = fecha2_aux.Month;
                int anioHasta = fecha2_aux.Year;

                //-------------------------consulta 1-------------------
                if(IdConsulta == 1){
                   
                    string nombreEdificioc1 = e.Row.Cells[2].Text;
                    string fecharow = e.Row.Cells[1].Text;
                    DateTime fechac1 = Convert.ToDateTime(fecharow);
                    string fechac1aux = fechac1.ToString("yyyy-MM-dd");
                    string db = Session["DB"].ToString();
                    string datoGuardar = db + ";" + fechac1aux + ";" + nombreEdificioc1;

                    LinkButton lb = new LinkButton();
                    lb.ID = datoGuardar+";";
                    lb.Text = e.Row.Cells[4].Text;
                    lb.Click += new EventHandler(onLinkClickConsulta1);
                    e.Row.Cells[4].Controls.Add(lb);
                }
                //-------------------------end consulta 1---------------

                ///---------------------begin consulta 3------------------------
                if(IdConsulta == 3){
                        for (int j = 1; j < e.Row.Cells.Count-1; j++)
                        {
                            string dato = e.Row.Cells[j].Text;                            
                            if (!dato.Equals("&nbsp;"))
                            {
                                int meses = CalcularMesesDeDiferencia(tx_fechaDesde.Text,tx_fechahasta.Text) +1;
                                int countSCZ = 0;
                                int countCbba = countSCZ + meses + 1;
                                int countLpz = countCbba + meses + 1;
                                string datoGuardar = "";
                                string BaseDatos = "db_seguimientoscz_jyc.";

                                int fila = e.Row.RowIndex;
                                
                               //---------------- santa Cruz
                                if (fila > countCbba && fila < countLpz)
                                {
                                    int aux = countSCZ + meses + 1;
                                    fila = fila - aux;
                                    BaseDatos = "db_seguimientocbba_jyc.";
                                }
                                else
                                    if (fila > countLpz)
                                    {
                                        int aux = countCbba + meses + 1;
                                        fila = fila - aux;
                                        BaseDatos = "db_seguimientolpz_jyc.";
                                    }

                                       
                                                string fecha1 = "";
                                                if (fila == 1)
                                                {
                                                    fecha1 = fecha1_aux.ToString("yyyy-MM-dd");
                                                }
                                                else {
                                                    string auxFecha = anioDesde + "-" + mesDesde + "-01";
                                                    DateTime fechaConver = Convert.ToDateTime(auxFecha);
                                                    fechaConver = fechaConver.AddMonths(fila - 1);
                                                    fecha1 = fechaConver.ToString("yyyy-MM-dd");                                    
                                                }   
                                                    string fecha2 = "";
                                                    if (fila == (countCbba - 1))
                                                    {
                                                        DateTime fechaConver2 = Convert.ToDateTime(fecha1);
                                                        fechaConver2 = fechaConver2.AddMonths(1);  
                                                        fechaConver2 = Convert.ToDateTime(fecha2_aux);
                                                        fecha2 = fechaConver2.ToString("yyyy-MM-dd");                                           
                                                    }
                                                    else {
                                                        string auxFecha = anioDesde + "-" + mesDesde + "-01";
                                                        DateTime fechaConver = Convert.ToDateTime(auxFecha);
                                                        fechaConver = fechaConver.AddMonths(fila);
                                                        DateTime fechaFinAux = fechaConver.AddDays(-1);
                                                        fecha2 = fechaFinAux.ToString("yyyy-MM-dd");                                           
                                                    }
                                                    datoGuardar = fecha1 + ";" + fecha2+";"+BaseDatos;                                        
                                           
                                //------------------end santa cruz
                            
                                LinkButton lb = new LinkButton();
                                lb.ID = datoGuardar+";"+j;
                                lb.Text = e.Row.Cells[j].Text;
                                lb.Click += new EventHandler(onLinkClick);
                                e.Row.Cells[j].Controls.Add(lb);

                                }

                            }
                      }    
                ///-----------------------------end consulta 3-------------------------------------                
                }
            }


        protected void onLinkClickConsulta1(object sender, EventArgs e)
        {
            LinkButton clickedControl = (LinkButton)sender;
            string dato = clickedControl.ID;
            string[] desarmar = dato.Split(';');            
            string baseDatos = desarmar[0].ToString();
            string fechaC1 = desarmar[1].ToString();
            string nombreEdificioc1 = desarmar[2].ToString();                       

            NA_Evento nevento = new NA_Evento();
            DataSet datos = null;
            datos = nevento.getInformeDeAtencionCallCenterConsulta1(Session["BaseDatos"].ToString(), fechaC1, nombreEdificioc1);
            
         
            Session["datoMostrar"] = datos;
            string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
            Response.Redirect(ruta + "/Presentacion/FA_mostrarResultado.aspx");

        }

       protected void onLinkClick(object sender, EventArgs e) {
            LinkButton clickedControl = (LinkButton)sender;
            string dato = clickedControl.ID;
            string[] desarmar = dato.Split(';');
            string fecha1 = desarmar[0].ToString();
            string fecha2 = desarmar[1].ToString();
            string baseDatos =  desarmar[2].ToString();
            int columna = Convert.ToInt32(desarmar[3].ToString());

            NA_Evento nevento = new NA_Evento();
            DataSet datos = null;
           if(columna == 1)
             datos = nevento.getDatos_CantidadEventosMesAnioBaseDatos(fecha1, fecha2, "6,2,3,4",baseDatos);
           else
               if(columna == 2)
                   datos = nevento.getDatos_CantidadEventosMesAnioBaseDatos(fecha1, fecha2, "2", baseDatos);
               else
                    if(columna == 3)
                        datos = nevento.getDatos_CantidadEventosMesAnioBaseDatos(fecha1, fecha2, "3", baseDatos);
                    else
                        if(columna == 4)
                            datos = nevento.getDatos_CantidadEventosMesAnioBaseDatos(fecha1, fecha2, "4", baseDatos);
                        else
                            if(columna == 5)
                                datos = nevento.getDatos_CantidadEventosMesAnioBaseDatos(fecha1, fecha2, "6", baseDatos);
                            else
                                if (columna == 6)
                                    datos = nevento.getDatos_CantidadEventosCerradoMesAnioBaseDatos(fecha1, fecha2, "6,2,3,4", baseDatos);
                                else
                                    if(columna == 7){
                                        datos = nevento.getDatos_CantidadEventosAbiertoMesAnioBaseDatos(fecha1, fecha2, "6,2,3,4", baseDatos);
                                    }


        //    Response.Write("<script type='text/javascript'> alert('boton: " + clickedControl.ID + "') </script>");
           // NA_Evento even = new NA_Evento();
          //  DataSet dato = even.getEvento(550);
            Session["datoMostrar"] = datos;
            string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
            Response.Redirect(ruta + "/Presentacion/FA_mostrarResultado.aspx");
          
        }

       

        


    }
}