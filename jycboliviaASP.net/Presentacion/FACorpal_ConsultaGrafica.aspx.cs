using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Windows.Controls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FACorpal_ConsultaGrafica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(132) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            this.Title = Session["BaseDatos"].ToString();
            colocarIframeCorrecto();
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

        private void colocarIframeCorrecto()
        {
            string baseDatos = Session["BaseDatos"].ToString();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            buscarConsultas();
        }

        private void buscarConsultas()
        {
            int index = dd_consulta.SelectedIndex;
            /* if(index == 1){
                 consultaHorasExtrasTrabajadas();
             }else*/
            if (index == 1)
            {
                consulta_PorcentajesProduccionVsVentas();
            }
          
        }

        private void consultaDeudaVSPagosRealizados()
        {
            string baseDatos = Session["BaseDatos"].ToString();
            if (baseDatos.Equals("Santa Cruz"))
            {
                //dvFrame.InnerHtml = "<iframe width='1140' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiMTc5YzcxNTItMWVjOS00NzgxLThmYjYtNjNmNDJlOWQxYTc3IiwidCI6ImE1ZDk5ZmJjLTE5YmYtNDNkMi05Y2FiLWVhNDM0YzZkYjYwZiJ9&pageName=ReportSection' frameborder='0' allowFullScreen='true'></iframe>";                
                dvFrame.InnerHtml = "<iframe width='1140' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiNTQ3NmFmYjUtZDc2Yy00MDkxLWFlY2QtMWYzNWIzYzJjYWZhIiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";
                //dvFrame.InnerHtml =  "<iframe src='https://www.google.com/maps/d/embed?mid=1WBzjIpbENEwzPWxhWoNQUR5HieI&hl=es-419&ehbc=2E312F' width='640' height='480'></iframe> ";


            }
            else
                if (baseDatos.Equals("Cochabamba"))
            {
                //dvFrame.InnerHtml = "<iframe width='1140' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiNjg1NGQ4ZjktNTM4ZC00MDE3LWJiOTUtNzJlZDU4Nzk4YjI1IiwidCI6ImE1ZDk5ZmJjLTE5YmYtNDNkMi05Y2FiLWVhNDM0YzZkYjYwZiJ9' frameborder='0' allowFullScreen='true'></iframe>";
                dvFrame.InnerHtml = "<iframe width='1140' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiMzQwNTE0OTMtNzQwZC00ZTM2LTk0MzItYWM2N2JhNzliNTQ1IiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";

            }
            else
                    if (baseDatos.Equals("La Paz"))
            {
                //dvFrame.InnerHtml = "<iframe width='1140' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiNWFiM2RkZGYtMzVkYy00MTA4LTgwNjYtOWJhOWQyOWJhZWI1IiwidCI6ImE1ZDk5ZmJjLTE5YmYtNDNkMi05Y2FiLWVhNDM0YzZkYjYwZiJ9' frameborder='0' allowFullScreen='true'></iframe>";
                dvFrame.InnerHtml = "<iframe width='1140' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiYjUzOThhYWItMGI3OS00YjRkLTliZDQtOGZjYjRiZWM3ZDdjIiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";

            }
            else
                        if (baseDatos.Equals("Sucre"))
            {
                //dvFrame.InnerHtml = "<iframe width='1140' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiNWFiM2RkZGYtMzVkYy00MTA4LTgwNjYtOWJhOWQyOWJhZWI1IiwidCI6ImE1ZDk5ZmJjLTE5YmYtNDNkMi05Y2FiLWVhNDM0YzZkYjYwZiJ9' frameborder='0' allowFullScreen='true'></iframe>";
                dvFrame.InnerHtml = "<iframe title='Report Section' width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiNzBlMWU0NDEtMTUwYy00ZDgwLTk4YmYtMTVmOGQ2YTQxMjM2IiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";

            }
            else
                            if (baseDatos.Equals("Asuncion-Nuevo"))
            {
                dvFrame.InnerHtml = "<iframe title='Report Section' width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiNGFhYmU5ZDEtNjk3NC00ZTViLTg4OTItYTBiZjg0MDcyOTk0IiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";

            }
            else
                                if (baseDatos.Equals("Potosi"))
            {
                dvFrame.InnerHtml = "<iframe title='Report Section' width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiOWFiZjUwN2MtYWMwYi00NWU5LTk3MmUtY2E1ZGU4YWVkNzY5IiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";

            }
            else
                                    if (baseDatos.Equals("Tarija"))
            {
                dvFrame.InnerHtml = "<iframe title='Report Section' width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiNzYyZWU2YzktOWUxMi00YmQ0LWI5OTgtOGNmNTgzZTFiNDRmIiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";

            }
            else
                                        if (baseDatos.Equals("Oruro"))
            {
                dvFrame.InnerHtml = "<iframe title='Report Section' width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiNTU2OWZiZjItYjE2Mi00ZGI0LWFiZGUtNDRiYjM5MmJiM2RiIiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";

            }


        }

        private void consultaEstadodeEstionesdeMantenimiento()
        {

            string baseDatos = Session["BaseDatos"].ToString();
            if (baseDatos.Equals("Santa Cruz"))
            {
                // dvFrame.InnerHtml = "<iframe width='1140' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiNjg0OTE4MzktYWQ0Ni00ZjEzLWFmMTctZmU1NWM0NDc1ZDRmIiwidCI6ImE1ZDk5ZmJjLTE5YmYtNDNkMi05Y2FiLWVhNDM0YzZkYjYwZiJ9' frameborder='0' allowFullScreen='true'></iframe>";
                dvFrame.InnerHtml = "<iframe title='Mantenimientos_SCZ - Página 1' width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiMDhjZDk1ZGEtOWQxYy00ZDg2LTlhNzktMmE3MDlhNTViM2Y2IiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";
            }
            else
                if (baseDatos.Equals("Cochabamba"))
            {
                dvFrame.InnerHtml = "<iframe title='Mantenimientos_Cbba' width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiNDFkYmJkZTItNmVhOS00ZDFiLTllNmMtNzgwYTRkZmE0OTlkIiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";
            }
            else
                    if (baseDatos.Equals("La Paz"))
            {
                dvFrame.InnerHtml = "<iframe title='Mantenimientos_LPZ' width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiMDJmMzk2MzQtZTIwOC00MzM0LWI1ZDItMmQ5ODIzOTkyNjc2IiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";
            }
            else
                        if (baseDatos.Equals("Sucre"))
            {


            }
            else
                            if (baseDatos.Equals("Asuncion-Nuevo"))
            {


            }
            else
                                if (baseDatos.Equals("Potosi"))
            {


            }
            else
                                    if (baseDatos.Equals("Tarija"))
            {


            }
            else
                                        if (baseDatos.Equals("Oruro"))
            {


            }
        }

        /*
        private void consultaRecaudacionesProgramadasRutasyCobros()
        {
            string baseDatos = Session["BaseDatos"].ToString();
            if (baseDatos.Equals("Santa Cruz"))
            {
                dvFrame.InnerHtml = "<iframe width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiMzM1YWM1YTctZGI4NS00M2FlLThkNWYtNTViNGM1ODQ2OGJmIiwidCI6ImE1ZDk5ZmJjLTE5YmYtNDNkMi05Y2FiLWVhNDM0YzZkYjYwZiJ9' frameborder='0' allowFullScreen='true'></iframe>";
            }
        }*/

        private void consulta_PorcentajesProduccionVsVentas()
        {
            string baseDatos = Session["BaseDatos"].ToString();
            if (baseDatos.Equals("Corpal"))
            {
                //dvFrame.InnerHtml = "<iframe width='1140' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiMTQwNjg5YmQtNjgzZC00Yzc5LTg0ZGMtZWYzMTc5MTUyNDYwIiwidCI6ImE1ZDk5ZmJjLTE5YmYtNDNkMi05Y2FiLWVhNDM0YzZkYjYwZiJ9' frameborder='0' allowFullScreen='true'></iframe>";                
                dvFrame.InnerHtml = "<iframe title='PorcentajesObjetivoVSVentas' width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiZjlhZGQzNDAtZDNhZS00OTc4LTlhMTEtOTFkODBmOGE3M2UwIiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";
            }
            /*  else
                  if (baseDatos.Equals("Cochabamba"))
              {
                  dvFrame.InnerHtml = "<iframe title='Horas Trabajadas Tipo Boletas Cbba' width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiNDliZDgzMjUtMzZlZS00MTFlLWE3ZTctNjhjODJiMGZjMWIwIiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";

              }
              else
                      if (baseDatos.Equals("La Paz"))
              {

                  dvFrame.InnerHtml = "<iframe title='Horas Trabajadas Tipo Boletas Lpz' width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiZDBiMGFkMWEtMzg3YS00NDY1LTgzNjktOTMxZjM4YjkwMTcwIiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9' frameborder='0' allowFullScreen='true'></iframe>";

              }
              else
                          if (baseDatos.Equals("Sucre"))
              {


              }
              else
                              if (baseDatos.Equals("Asuncion-Nuevo"))
              {


              }
              else
                                  if (baseDatos.Equals("Potosi"))
              {


              }
              else
                                      if (baseDatos.Equals("Tarija"))
              {


              }
              else
                                          if (baseDatos.Equals("Oruro"))
              {


              }*/
        }

        private void consultaHorasExtrasTrabajadas()
        {
            string baseDatos = Session["BaseDatos"].ToString();
            if (baseDatos.Equals("Santa Cruz"))
            {
                //dvFrame.InnerHtml = "<iframe width='1140' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiZmE4YmQ2MmQtMmQzNC00OTYwLTkxOWYtZjU2MjE4ZDYwMmRhIiwidCI6ImE1ZDk5ZmJjLTE5YmYtNDNkMi05Y2FiLWVhNDM0YzZkYjYwZiJ9' frameborder='0' allowFullScreen='true'></iframe>";
                dvFrame.InnerHtml = "<iframe title='HorasExtrasTrabajadas_SantaCruz' width='1024' height='612' src='https://app.powerbi.com/view?r=eyJrIjoiNmRjNWYyNDMtMzdkZS00OWRkLTlkMDQtMGY1YWYzOGQyNzlmIiwidCI6IjQ1Yjc0YjExLTg2ZWQtNDQ0My05MTY2LTNhZGJmMGFhMmIxNSJ9&pageName=ReportSection' frameborder='0' allowFullScreen='true'></iframe>";
            }
        }


    }
}

