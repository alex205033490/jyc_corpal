using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using jycboliviaASP.net.Negocio;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CronoRuta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (tienePermisoDeIngreso(64) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if(!IsPostBack){
             vercuadro();
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



        private void vercuadro()
        {
            DataTable datoRepuesto = new DataTable();
            datoRepuesto.Columns.Add("Cant. Visita", typeof(string));
            datoRepuesto.Columns.Add("Semana 1", typeof(string));
            datoRepuesto.Columns.Add("Semana 2", typeof(string));
            datoRepuesto.Columns.Add("Semana 3", typeof(string));
            datoRepuesto.Columns.Add("Semana 4", typeof(string)); 
            
            DataRow tupla = datoRepuesto.NewRow();                           
            datoRepuesto.Rows.Add(tupla);
            DataRow tupla2 = datoRepuesto.NewRow();
            datoRepuesto.Rows.Add(tupla2);
            DataRow tupla3 = datoRepuesto.NewRow();
            datoRepuesto.Rows.Add(tupla3);
            DataRow tupla4 = datoRepuesto.NewRow();
            datoRepuesto.Rows.Add(tupla4);

            gv_cronogramaVisita.DataSource = datoRepuesto;
            gv_cronogramaVisita.DataBind();

            Label dato = (Label)gv_cronogramaVisita.Rows[0].Cells[0].FindControl("Label1");
            Label dato1 = (Label)gv_cronogramaVisita.Rows[1].Cells[0].FindControl("Label1");
            Label dato2 = (Label)gv_cronogramaVisita.Rows[2].Cells[0].FindControl("Label1");
            Label dato3 = (Label)gv_cronogramaVisita.Rows[3].Cells[0].FindControl("Label1");
            dato.Text = "1";
            dato1.Text = "2";
            dato2.Text = "3";
            dato3.Text = "4";

            dd_anio.SelectedIndex = 0;
            dd_mes.SelectedIndex = 0;
        }

        protected void bt_AgregarCrono_Click(object sender, EventArgs e)
        {
            agregarCronograma();
        }

        private void agregarCronograma()
        {
            int mes = dd_mes.SelectedIndex + 1;
            int anio = Convert.ToInt32(dd_anio.SelectedItem.Text);
            NA_CronogramaVisitaRutaMantenimiento cv = new NA_CronogramaVisitaRutaMantenimiento();
            bool verificacion = verifiartablaCronogramaCorrecto();   
            if(verificacion == true){
                insertarCuadroCronogramaVisita(mes,anio);
                NA_CronogramaVisitaRutaMantenimiento ncrono =  new NA_CronogramaVisitaRutaMantenimiento();
                DataSet dato = ncrono.getAllDatos("", mes.ToString(), anio.ToString());
                gv_datosCronogramaVisita.DataSource = dato;
                gv_datosCronogramaVisita.DataBind();
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Datos Cronograma No son Correctos') </script>");

        }

        private void insertarCuadroCronogramaVisita(int mes, int anio)
        {
            for (int i = 0; i < gv_cronogramaVisita.Rows.Count; i++)
            {
                Label dato = (Label)gv_cronogramaVisita.Rows[i].Cells[0].FindControl("Label1");
                int nrovisita = Convert.ToInt32(dato.Text);
                CheckBox semana1 = (CheckBox)gv_cronogramaVisita.Rows[i].Cells[1].FindControl("CheckBox1");
                CheckBox semana2 = (CheckBox)gv_cronogramaVisita.Rows[i].Cells[2].FindControl("CheckBox2");
                CheckBox semana3 = (CheckBox)gv_cronogramaVisita.Rows[i].Cells[3].FindControl("CheckBox3");
                CheckBox semana4 = (CheckBox)gv_cronogramaVisita.Rows[i].Cells[4].FindControl("CheckBox4");
                NA_CronogramaVisitaRutaMantenimiento ncrono = new NA_CronogramaVisitaRutaMantenimiento();
         //       ncrono.insertarCronogramaVisitaRutaM(nrovisita, mes, anio, semana1.Checked, semana2.Checked, semana3.Checked, semana4.Checked);               
            }
        }

        private bool verifiartablaCronogramaCorrecto()
        {
            int cant = 0;

            for (int i = 0; i < gv_cronogramaVisita.Rows.Count; i++ )
            {
                int nroCheckbox = 1;
                for (int j = 1; j < gv_cronogramaVisita.Columns.Count; j++ )
                {
                    CheckBox cb = (CheckBox)gv_cronogramaVisita.Rows[i].Cells[j].FindControl("CheckBox" + nroCheckbox);
                    if (cb.Checked == true)
                    {
                        cant = cant + 1;
                    }
                    nroCheckbox = nroCheckbox + 1;
                }
            }

            if (cant == 10)
            {
                return true;
            }
            else
                return false;

          
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            buscarCronograma();
        }

        private void buscarCronograma()
        {
            NA_CronogramaVisitaRutaMantenimiento ncrono = new NA_CronogramaVisitaRutaMantenimiento();
            int mes = dd_mes.SelectedIndex+1;
            int anio = Convert.ToInt32(dd_anio.SelectedItem.Text);
            DataSet dato = ncrono.getAllDatos("",mes.ToString(),anio.ToString());
            gv_datosCronogramaVisita.DataSource = dato;
            gv_datosCronogramaVisita.DataBind();
        }

    }
}