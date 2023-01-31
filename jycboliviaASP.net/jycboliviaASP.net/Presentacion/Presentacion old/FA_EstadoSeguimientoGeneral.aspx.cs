using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.IO;
using System.Configuration;


namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_EstadoSeguimientoGeneral : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // ponerDatosChart();
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(19) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
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


        public void ponerDatosChart()
        {
            /*   Series serie = new Series("column");
               serie.ChartType = SeriesChartType.Line;
               int[] yValues = {2,4,6,8,10};
               string[] xValues = {"enero","febrero","marzo","abril","mayo"};
               serie.Points.DataBindXY(xValues, yValues);

               Series serie2 = new Series("column2");
               serie2.ChartType = SeriesChartType.Line;
               int[] yValues2 = {1,4,1,6,2};            
               serie2.Points.DataBindXY(xValues, yValues2);
            
               Chart1.Series.Add(serie);
               Chart1.Series.Add(serie2); */

            //Chart1.ChartAreas.Add("chtArea");
            //Chart1.ChartAreas[0].AxisX.Title = "Category Name";
            //Chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana",11,System.Drawing.FontStyle.Bold);
            //Chart1.ChartAreas[0].AxisY.Title = "UniPrice";
            //Chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);

            // Chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            //Chart1.ChartAreas[0].BorderWidth = 2;

            Chart1.Legends.Add("UniPrice");
            Series serie = new Series("UniPrice");
            serie.ChartType = SeriesChartType.Line;
            int[] yValues = { 20, 40, 60, 80, 150 };
            string[] xValues = { "enero", "febrero", "marzo", "abril", "mayo" };
            serie.Points.DataBindXY(xValues, yValues);
            serie.BorderWidth = 3;

            Chart1.Legends.Add("UniPrice2");
            Series serie2 = new Series("UniPrice2");
            serie2.ChartType = SeriesChartType.Line;
            int[] yValues2 = { 10, 40, 10, 60, 20 };
            serie2.Points.DataBindXY(xValues, yValues2);
            serie2.BorderWidth = 3;
            Chart1.Series.Add(serie);
            Chart1.Series.Add(serie2);

        }

        protected void CargarEstadistica()
        {
            NEstadisticaParqueAscensores estadosSeguiN = new NEstadisticaParqueAscensores();
            DataSet tuplas = estadosSeguiN.VistaGeneralEstado(tx_fecha1.Text, tx_fecha2.Text);
            int columnaTuplas = tuplas.Tables[0].Columns.Count - 1;
            string[] xValues = new string[columnaTuplas];

            for (int i = 1; i <= columnaTuplas; i++)
            {
                string nombreColumna = tuplas.Tables[0].Columns[i].ColumnName;
                xValues[i - 1] = nombreColumna;
            }

            int CantColumnas = gv_EstadoInstalacion.Rows[0].Cells.Count;
            //   Chart1.ChartAreas.Add("chtArea");
            //  Chart1.ChartAreas[0].AxisX.Title = "(Mes,Año)";
            // Chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);
            //Chart1.ChartAreas[0].AxisY.Title = "Nro Equipos";
            //Chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);

            CheckBox cb = null;
            for (int i = 0; i < gv_EstadoInstalacion.Rows.Count; i++)
            {
                cb = (CheckBox)gv_EstadoInstalacion.Rows[i].Cells[1].FindControl("CheckBox1");
                if (cb != null && cb.Checked)
                {
                    int[] listdatosInstalacion = new int[CantColumnas - 2];
                    string nombreEstado = gv_EstadoInstalacion.Rows[i].Cells[1].Text;
                    for (int j = 2; j < CantColumnas; j++)
                    {
                        int dato = Convert.ToInt32(gv_EstadoInstalacion.Rows[i].Cells[j].Text);
                        listdatosInstalacion[j - 2] = dato;
                    }
                    Chart1.Legends.Add(nombreEstado);
                    Series serie = new Series(nombreEstado);
                    serie.ChartType = SeriesChartType.Line;
                    int[] yValues2 = listdatosInstalacion;
                    serie.Points.DataBindXY(xValues, yValues2);
                    serie.BorderWidth = 3;
                    //Chart1.Series[0] = serie;
                    serie.IsValueShownAsLabel = true;
                    serie.ToolTip = "Data Point y Value: #VALY{G}";
                    Chart1.Series.Add(serie);

                }
            }


            for (int i = 0; i < gv_EstadoMantenimiento.Rows.Count; i++)
            {
                cb = (CheckBox)gv_EstadoMantenimiento.Rows[i].Cells[1].FindControl("CheckBox2");
                if (cb != null && cb.Checked)
                {
                    int[] listdatosMantenimiento = new int[CantColumnas - 2];
                    string nombreEstado = gv_EstadoMantenimiento.Rows[i].Cells[1].Text;
                    for (int j = 2; j < CantColumnas; j++)
                    {
                        int dato = Convert.ToInt32(gv_EstadoMantenimiento.Rows[i].Cells[j].Text);
                        listdatosMantenimiento[j - 2] = dato;
                    }
                    Chart1.Legends.Add(nombreEstado);
                    Series serie = new Series(nombreEstado);
                    serie.ChartType = SeriesChartType.Line;
                    int[] yValues2 = listdatosMantenimiento;
                    serie.Points.DataBindXY(xValues, yValues2);
                    serie.BorderWidth = 3;
                    //Chart1.Series[0] = serie;
                    serie.IsValueShownAsLabel = true;
                    serie.ToolTip = "Data Point y Value: #VALY{G}";
                    Chart1.Series.Add(serie);
                }
            }

            // Chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            // Chart1.BorderlineColor = System.Drawing.Color.FromArgb(26, 59, 105);
            Chart1.BorderlineWidth = 3;
            Chart1.BackColor = Color.NavajoWhite;
        }



        public void VerEstadisticaSeguimiento()
        {

            if (tx_fecha1.Text != "" && tx_fecha2.Text != "")
            {
                NEstadisticaInstalacion estadosInstalacionN = new NEstadisticaInstalacion();
                DataSet tuplasInstalacion = estadosInstalacionN.VistaGeneralEstadoInstalacion(tx_fecha1.Text, tx_fecha2.Text);
                gv_EstadoInstalacion.DataSource = tuplasInstalacion;
                gv_EstadoInstalacion.DataBind();

                NEstadisticaParqueAscensores estadosSeguiN = new NEstadisticaParqueAscensores();
                DataSet tuplas = estadosSeguiN.VistaGeneralEstado(tx_fecha1.Text, tx_fecha2.Text);
                gv_EstadoMantenimiento.DataSource = tuplas;
                gv_EstadoMantenimiento.DataBind();
                gv_EstadoMantenimiento.Rows[0].BackColor = System.Drawing.Color.Yellow;
                gv_EstadoMantenimiento.Rows[1].BackColor = System.Drawing.Color.Red;
                gv_EstadoMantenimiento.Rows[2].BackColor = System.Drawing.Color.Orange;
                //gv_EstadoSeguimiento2.Rows[3].BackColor = System.Drawing.Color.Pink;
                gv_EstadoMantenimiento.Rows[4].BackColor = System.Drawing.Color.Blue;
                gv_EstadoMantenimiento.Rows[5].BackColor = System.Drawing.Color.GreenYellow;
                gv_EstadoMantenimiento.Rows[6].BackColor = System.Drawing.Color.Pink;
            }
            else {
                Response.Write("<script type='text/javascript'> alert('Error: Fechas1 , Fechas2') </script>");
            }

        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            VerEstadisticaSeguimiento();
        }

        protected void bt_EstadisticaSelect_Click(object sender, EventArgs e)
        {
            CargarEstadistica();
        }






        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            if (tx_fecha1.Text != "" && tx_fecha2.Text != "")
            {
                NEstadisticaInstalacion estadosInstalacionN = new NEstadisticaInstalacion();
                DataSet tuplasInstalacion = estadosInstalacionN.VistaGeneralEstadoInstalacion(tx_fecha1.Text, tx_fecha2.Text);

                //// Creacion del Excel
                HttpResponse response = HttpContext.Current.Response;
                // first let's clean up the response.object
                response.Clear();
                response.Charset = "";
                // set the response mime type for excel
                response.ContentType = "application/vnd.ms-excel";
                string nombre = "Estado General _Instalacion";
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

                // create a string writer
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        // instantiate a datagrid
                        DataGrid dg = new DataGrid();
                        dg.DataSource = tuplasInstalacion;
                        dg.DataBind();
                        dg.RenderControl(htw);
                        response.Write(sw.ToString());
                        response.End();
                    }
                }
            }
        }

        protected void lk_excelMantenimiento_Click(object sender, EventArgs e)
        {

            if (tx_fecha1.Text != "" && tx_fecha2.Text != "")
            {
                NEstadisticaParqueAscensores estadosSeguiN = new NEstadisticaParqueAscensores();
                DataSet tuplas = estadosSeguiN.VistaGeneralEstado(tx_fecha1.Text, tx_fecha2.Text);
                //// Creacion del Excel
                HttpResponse response = HttpContext.Current.Response;
                // first let's clean up the response.object
                response.Clear();
                response.Charset = "";
                // set the response mime type for excel
                response.ContentType = "application/vnd.ms-excel";
                string nombre = "Estado General Mantenimiento";
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