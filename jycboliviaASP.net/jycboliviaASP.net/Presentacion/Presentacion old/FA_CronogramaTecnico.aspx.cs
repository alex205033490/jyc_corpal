using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using jycboliviaASP.net.Negocio;
using System.Drawing;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CronogramaTecnico : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(36) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

          //  lb_Titulo.Text = "Gestionar Estado de Instalacion " + Session["BaseDatos"].ToString();
           if(!IsPostBack){            
            cargarddlEstadoEquipo();
            mostrarEquipos("", "","","");
            cargarResponsableEdificio();
            cargarTecnicoInstalador();
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


        private void cargarResponsableEdificio()
        {
            NA_Equipo Nequipo = new NA_Equipo();
            DataSet datosCargar = Nequipo.getAllResponsableProyecto_Responsable();
            dd_RespProyecto.DataSource = datosCargar;
            // dd_RespProyecto.DataValueField = "codigo";
            dd_RespProyecto.DataTextField = "nombre";
            dd_RespProyecto.Items.Add(new ListItem("", "-1"));
            dd_RespProyecto.AppendDataBoundItems = true;
            dd_RespProyecto.SelectedIndex = -1;
            dd_RespProyecto.DataBind();
        }

        private void cargarTecnicoInstalador()
        {
            NA_Equipo Nequipo = new NA_Equipo();
            DataSet datosCargar = Nequipo.getAllResponsableProyecto_TecnicoInstalador();
            dd_TecnicoInstalador.DataSource = datosCargar;
            // dd_TecnicoInstalador.DataValueField = "codigo";
            dd_TecnicoInstalador.DataTextField = "nombre";
            dd_TecnicoInstalador.Items.Add(new ListItem("", "-1"));
            dd_TecnicoInstalador.AppendDataBoundItems = true;
            dd_TecnicoInstalador.SelectedIndex = -1;
            dd_TecnicoInstalador.DataBind();
        }


        private bool cargarPermisoCambioFecha()
        {
            int codigoUser = Convert.ToInt32(Session["coduser"].ToString());
            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(37,codigoUser);
        }

        private void cargarddlEstadoEquipo()
        {
            NEstadoEquipo estadoEquipo = new NEstadoEquipo();

            dd_estado.DataSource = estadoEquipo.listar();
            dd_estado.DataValueField = "codigo";
            dd_estado.DataTextField = "nombre";
            dd_estado.Items.Add(new ListItem("", "-1"));
            dd_estado.AppendDataBoundItems = true;
            dd_estado.SelectedIndex = -1;
            dd_estado.DataBind();
        }


        public void mostrarEquipos(string edificio, string exbo,string nombreEncargadoProyecto, string nombreTecnicoInstalador) {
            string NombreEstado = dd_estado.SelectedItem.Text;

            NA_Equipo neq = new NA_Equipo();
            DataSet dato = neq.getEquiposCronogramaTecnico(edificio, exbo, NombreEstado,nombreEncargadoProyecto,nombreTecnicoInstalador );
            gv_equipos.DataSource = dato;
            gv_equipos.DataBind();
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

        protected void bt_calcular_Click(object sender, EventArgs e)
        {
           
          //  int cantdias = Convert.ToInt32(tx_cantidadDias.Text);
          //  gv_tablaCronograma.DataSource = calcularFecha2(cantdias);
           // gv_tablaCronograma.DataBind();
            //calcularCronogramaTecnico();
            buscarEquipos();
        }

        public void buscarEquipos() {
            mostrarEquipos(tx_Edificio.Text, tx_exbo.Text, dd_RespProyecto.SelectedItem.Text, dd_TecnicoInstalador.SelectedItem.Text);
        }


     public void calcularCronogramaTecnico_faceI(string fechaIni,string paradasAux) {

            if (!fechaIni.Equals(""))
            {
            if (gv_equipos.SelectedIndex > -1)
            {
                if (!paradasAux.Equals(""))
                {
                    int paradas = Convert.ToInt32(paradasAux);
                    NA_CronogramaTecnico crono = new NA_CronogramaTecnico();
                    DataSet dato = crono.calcularCronogramaTecnico_faceI(Convert.ToDateTime(fechaIni), paradas);
                    gv_tablaCronograma.DataSource = dato;
                    gv_tablaCronograma.DataBind();
                }else
                    Response.Write("<script type='text/javascript'> alert('Error: No tiene Numero Paradas') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: No selecciono Equipo') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: No Tiene Fecha Asignada') </script>");
        }


        public void calcularCronogramaTecnico_faceII(string fechaIni, string paradasAux)
        {

            if (!fechaIni.Equals(""))
            {
                if (gv_equipos.SelectedIndex > -1)
                {
                    if (!paradasAux.Equals(""))
                    {
                        int paradas = Convert.ToInt32(paradasAux);
                        NA_CronogramaTecnico crono = new NA_CronogramaTecnico();
                        DataSet dato = crono.calcularCronogramaTecnico_faceII(Convert.ToDateTime(fechaIni), paradas);
                        gv_tablaCronograma.DataSource = dato;
                        gv_tablaCronograma.DataBind();
                    }
                    else
                        Response.Write("<script type='text/javascript'> alert('Error: No tiene Numero Paradas') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: No selecciono Equipo') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: No Tiene Fecha Asignada') </script>");
        }



        protected void gv_equipos_RowEditing(object sender, GridViewEditEventArgs e)
        {
             gv_equipos.EditIndex = e.NewEditIndex;
             buscarEquipos();

             gv_equipos.Rows[e.NewEditIndex].Cells[2].Enabled = false;
             gv_equipos.Rows[e.NewEditIndex].Cells[3].Enabled = false;
             gv_equipos.Rows[e.NewEditIndex].Cells[4].Enabled = false;
             gv_equipos.Rows[e.NewEditIndex].Cells[10].Enabled = false;
             gv_equipos.Rows[e.NewEditIndex].Cells[12].Enabled = false;

          //   gv_equipos.Rows[e.NewEditIndex].Cells[9].Enabled = false;
          //   gv_equipos.Rows[e.NewEditIndex].Cells[11].Enabled = false;

             TextBox prueba = (TextBox)gv_equipos.Rows[e.NewEditIndex].Cells[9].FindControl("tx_fechaCronoTec");
             prueba.BackColor = Color.Yellow;
             TextBox prueba1 = (TextBox)gv_equipos.Rows[e.NewEditIndex].Cells[5].Controls[0];
             prueba1.BackColor = Color.Yellow;
             TextBox prueba2 = (TextBox)gv_equipos.Rows[e.NewEditIndex].Cells[6].Controls[0];
             prueba2.BackColor = Color.Yellow;
             TextBox prueba3 = (TextBox)gv_equipos.Rows[e.NewEditIndex].Cells[7].Controls[0];
             prueba3.BackColor = Color.Yellow;
             TextBox prueba5 = (TextBox)gv_equipos.Rows[e.NewEditIndex].Cells[8].Controls[0];
             prueba5.BackColor = Color.Yellow;
             TextBox prueba4 = (TextBox)gv_equipos.Rows[e.NewEditIndex].Cells[12].FindControl("TextBox1");
             prueba4.BackColor = Color.Yellow;


             NA_Equipo equipoA = new NA_Equipo();
             TextBox CodigoEquipo = (TextBox)gv_equipos.Rows[e.NewEditIndex].Cells[2].Controls[0];
             int codEquipo = Convert.ToInt32(CodigoEquipo.Text);
             DataSet datoEquipoCrono = equipoA.getEquiposCronogramaTecnico2(codEquipo);
             string auxFase1 = datoEquipoCrono.Tables[0].Rows[0][7].ToString();
             string auxFase2 = datoEquipoCrono.Tables[0].Rows[0][8].ToString();
             if(!auxFase1.Equals("")){
                 gv_equipos.Rows[e.NewEditIndex].Cells[9].Enabled = false;                 
             }else
                 gv_equipos.Rows[e.NewEditIndex].Cells[9].Enabled = true;                 

            if(!auxFase2.Equals("")){
                gv_equipos.Rows[e.NewEditIndex].Cells[12].Enabled = false;  
            }else
                gv_equipos.Rows[e.NewEditIndex].Cells[12].Enabled = true;  

            bool cambioFechaCrono = cargarPermisoCambioFecha();
            if(cambioFechaCrono == true){
                gv_equipos.Rows[e.NewEditIndex].Cells[9].Enabled = true;
                gv_equipos.Rows[e.NewEditIndex].Cells[12].Enabled = true;  
            }

        }

        protected void gv_equipos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_equipos.EditIndex = -1;
            buscarEquipos();            
        }

        protected void gv_equipos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                NA_CronogramaTecnico ncrono = new NA_CronogramaTecnico();
                
                int parada = 0;
                try{
                Label fechafaseI = (Label)e.Row.FindControl("Label1");
                DateTime fechaIni = DateTime.ParseExact(fechafaseI.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture);
                parada = Convert.ToInt32(e.Row.Cells[5].Text);
                int NroParadas = ncrono.getParadasExacta(parada);
                int cantDias = ncrono.getDias_segunParadas_FaseI(NroParadas);
                int cantidadSabados = ncrono.calcularSabadosenFecha(fechaIni, cantDias);                
                int cantDiasAux = cantDias + cantidadSabados;
                e.Row.Cells[10].Text = Convert.ToString(cantDiasAux);
                DateTime fechafin = ncrono.calcularFechaFinalizacionCronograma(fechaIni, cantDiasAux);
                e.Row.Cells[11].Text = fechafin.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
               // Label diaskTarda = new Label();
               // diaskTarda.ID = "lb_diasG";
               // diaskTarda.Text = Convert.ToString(cantDiasAux);
               // e.Row.Cells[10].Controls.Add(diaskTarda);
                

                }catch(Exception){
                parada = 0;
                e.Row.Cells[10].Text = "0";
                e.Row.Cells[11].Text = "";
                }


                int parada2 = 0;
                try{
                    Label fechafase2 = (Label)e.Row.FindControl("Label2");
                    DateTime fechaIni2 = DateTime.ParseExact(fechafase2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    parada2 = Convert.ToInt32(e.Row.Cells[5].Text);
                    int NroParadas2 = ncrono.getParadasExacta(parada2);
                    int cantDias2 = ncrono.getDias_segunParadas_FaseII(NroParadas2);
                    int cantidadSabados2 = ncrono.calcularSabadosenFecha(fechaIni2, cantDias2);
                    int cantDiasAux2 = cantDias2 + cantidadSabados2;
                    e.Row.Cells[13].Text = Convert.ToString(cantDiasAux2);
                    DateTime fechafin2 = ncrono.calcularFechaFinalizacionCronograma(fechaIni2, cantDiasAux2);
                    e.Row.Cells[14].Text = fechafin2.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                }catch(Exception){
                parada2 = 0;
                e.Row.Cells[13].Text = "0";
                e.Row.Cells[14].Text = "";
                }
                                    
            }
        }

        protected void gv_equipos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox CodigoEquipo = (TextBox)gv_equipos.Rows[e.RowIndex].Cells[2].Controls[0];
            int codigoEquipo = Convert.ToInt32(CodigoEquipo.Text);
            
            TextBox fechacronogramaAux = (TextBox)gv_equipos.Rows[e.RowIndex].Cells[9].Controls[1];            
            TextBox fechaFase2aux = (TextBox)gv_equipos.Rows[e.RowIndex].Cells[12].Controls[1];

            TextBox paradaaux = (TextBox)gv_equipos.Rows[e.RowIndex].Cells[5].Controls[0];
            TextBox modeloaux = (TextBox)gv_equipos.Rows[e.RowIndex].Cells[6].Controls[0];
            TextBox pasajeroaux = (TextBox)gv_equipos.Rows[e.RowIndex].Cells[7].Controls[0];
            TextBox velocidadaux = (TextBox)gv_equipos.Rows[e.RowIndex].Cells[8].Controls[0];
            NEquipo nequipoA = new NEquipo();
            string fechaCronograma = nequipoA.aFecha(fechacronogramaAux.Text);
            string fechaFase2 = nequipoA.aFecha(fechaFase2aux.Text);
            
                if (!fechaCronograma.Equals("null"))
                {
                    fechaCronograma = "'" + fechaCronograma + "'";
                }
            
                if (!fechaFase2.Equals("null"))
                {
                    fechaFase2 = "'" + fechaFase2 + "'";
                }
                        

            int parada = Convert.ToInt32(paradaaux.Text);
            string pasajero = pasajeroaux.Text;
            string modelo = modeloaux.Text;
            string velocidad = velocidadaux.Text;

            NA_Equipo equipoAlex = new NA_Equipo();
            equipoAlex.modificarCronogramaTecnico(codigoEquipo,parada,pasajero,modelo, velocidad,fechaCronograma,fechaFase2);
            //------------------------------------------------
            TextBox txexbo = (TextBox)gv_equipos.Rows[e.RowIndex].Cells[3].Controls[0];
            string exbo = txexbo.Text;
            TextBox txEdificio = (TextBox)gv_equipos.Rows[e.RowIndex].Cells[4].Controls[0];
            string edificio = txEdificio.Text;
            string fase1 = fechacronogramaAux.Text;
            string fase2 = fechaFase2aux.Text;
            string baseDeDatos =  Session["BaseDatos"].ToString();
            string VariableSimec = ((TextBox)gv_equipos.Rows[e.RowIndex].Cells[23].Controls[0]).Text;

            enviarCorreodeFechasModificadas(edificio, exbo, fase1, fase2, baseDeDatos,VariableSimec);
            //------------------------------------------------
            gv_equipos.EditIndex = -1;
            buscarEquipos();
            ///--------------------------------------------
            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Ha Modificado(cronograma Tecnico) el equipo " + codigoEquipo + " del Exbo " + CodigoEquipo.Text+" faseI = "+fechaCronograma+", faseII = "+fechaFase2);
        }

        protected void gv_equipos_SelectedIndexChanged(object sender, EventArgs e)
        {            
           /* int codigoEquipo = Convert.ToInt32(gv_equipos.SelectedRow.Cells[2].Text);
            NA_Equipo equipoA = new NA_Equipo();
            DataSet datoEquipoCrono = equipoA.getEquiposCronogramaTecnico2(codigoEquipo);
            string fechaAux = datoEquipoCrono.Tables[0].Rows[0][7].ToString();
            string paradasAux = datoEquipoCrono.Tables[0].Rows[0][3].ToString();  
            calcularCronogramaTecnico_faceI(fechaAux,paradasAux); */
        }

        protected void bt_faseI_Click(object sender, EventArgs e)
        {
            if (gv_equipos.SelectedIndex > -1) {
                int codigoEquipo = Convert.ToInt32(gv_equipos.SelectedRow.Cells[2].Text);
                NA_Equipo equipoA = new NA_Equipo();
                DataSet datoEquipoCrono = equipoA.getEquiposCronogramaTecnico2(codigoEquipo);
                string fechaAux = datoEquipoCrono.Tables[0].Rows[0][7].ToString();
                string paradasAux = datoEquipoCrono.Tables[0].Rows[0][3].ToString();
                calcularCronogramaTecnico_faceI(fechaAux, paradasAux);
            }else
                Response.Write("<script type='text/javascript'> alert('Error: No selecciono Equipo') </script>");
        }


        protected void bt_faseII_Click(object sender, EventArgs e)
        {
            if (gv_equipos.SelectedIndex > -1) {
                int codigoEquipo = Convert.ToInt32(gv_equipos.SelectedRow.Cells[2].Text);
                NA_Equipo equipoA = new NA_Equipo();
                DataSet datoEquipoCrono = equipoA.getEquiposCronogramaTecnico2(codigoEquipo);
                string fechaAux = datoEquipoCrono.Tables[0].Rows[0][8].ToString();
                string paradasAux = datoEquipoCrono.Tables[0].Rows[0][3].ToString();
                calcularCronogramaTecnico_faceII(fechaAux, paradasAux);
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: No selecciono Equipo') </script>");
            
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {
            exportarEn_Excel2();
        }


        public void exportar_excelEquipos()
        {
            string nombreEdificio = tx_Edificio.Text;
            string exbo = tx_exbo.Text;            

            string NombreEstado = dd_estado.SelectedItem.Text;
            NA_Equipo neq = new NA_Equipo();
            DataSet dato = neq.getEquiposCronogramaTecnico(nombreEdificio, exbo, NombreEstado, dd_RespProyecto.SelectedItem.Text, dd_TecnicoInstalador.SelectedItem.Text);
           // gv_equipos.DataSource = dato;
           // gv_equipos.DataBind();            


            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Cronograma Tecnico "+nombreEdificio+" "+exbo+" "+ Session["BaseDatos"].ToString(); ;
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = dato;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }
        }

        protected void bt_excelFaseI_Click(object sender, EventArgs e)
        {
            if (gv_equipos.SelectedIndex > -1) {
                int codigoEquipo = Convert.ToInt32(gv_equipos.SelectedRow.Cells[2].Text);
                NA_Equipo equipoA = new NA_Equipo();
                DataSet datoEquipoCrono = equipoA.getEquiposCronogramaTecnico2(codigoEquipo);
                string fechaAux = datoEquipoCrono.Tables[0].Rows[0][7].ToString();
                string paradasAux = datoEquipoCrono.Tables[0].Rows[0][3].ToString();
                exportar_excelfaseI(fechaAux, paradasAux);
            }else
                Response.Write("<script type='text/javascript'> alert('Error: No selecciono Equipo') </script>");
        }



        public void exportar_excelfaseI(string fechaIni, string paradasAux)
        {

            if (!fechaIni.Equals(""))
            {
                if (gv_equipos.SelectedIndex > -1)
                {
                    if (!paradasAux.Equals(""))
                    {
                        string nombreEdificio = tx_Edificio.Text;
                        string exbo = tx_exbo.Text;     
                        int paradas = Convert.ToInt32(paradasAux);
                        NA_CronogramaTecnico crono = new NA_CronogramaTecnico();
                        DataSet dato = crono.calcularCronogramaTecnico_faceI(Convert.ToDateTime(fechaIni), paradas);


                        //// Creacion del Excel
                        HttpResponse response = HttpContext.Current.Response;
                        // first let's clean up the response.object
                        response.Clear();
                        response.Charset = "";
                        // set the response mime type for excel
                        response.ContentType = "application/vnd.ms-excel";
                        string nombre = "Cronograma Tecnico Fase I de "+nombreEdificio+" "+exbo+" " + Session["BaseDatos"].ToString(); ;
                        response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

                        // create a string writer
                        using (StringWriter sw = new StringWriter())
                        {
                            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                            {
                                // instantiate a datagrid
                                DataGrid dg = new DataGrid();
                                dg.DataSource = dato;
                                dg.DataBind();
                                dg.RenderControl(htw);
                                response.Write(sw.ToString());
                                response.End();
                            }

                        }

                    }
                    else
                        Response.Write("<script type='text/javascript'> alert('Error: No tiene Numero Paradas') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: No selecciono Equipo') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: No Tiene Fecha Asignada') </script>");

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
           
            GridView dg = gv_equipos;
            dg.GridLines = GridLines.Both;
            dg.HeaderStyle.Font.Bold = true;
            dg.Columns[0].Visible = false;
            dg.RenderControl(htmltextwrtter);
                              
            Response.Write(strwritter.ToString());
            Response.End();  
        }


        protected void bt_excelFaseII_Click(object sender, EventArgs e)
        {
            if (gv_equipos.SelectedIndex > -1) {
                int codigoEquipo = Convert.ToInt32(gv_equipos.SelectedRow.Cells[2].Text);
                NA_Equipo equipoA = new NA_Equipo();
                DataSet datoEquipoCrono = equipoA.getEquiposCronogramaTecnico2(codigoEquipo);
                string fechaAux = datoEquipoCrono.Tables[0].Rows[0][8].ToString();
                string paradasAux = datoEquipoCrono.Tables[0].Rows[0][3].ToString();
                calcularCronogramaTecnico_faceII(fechaAux, paradasAux);
                exportarExcel_faceII(fechaAux, paradasAux);
            }else
            Response.Write("<script type='text/javascript'> alert('Error: No selecciono Equipo') </script>");
        }



        public void exportarExcel_faceII(string fechaIni, string paradasAux)
        {

            if (!fechaIni.Equals(""))
            {
                if (gv_equipos.SelectedIndex > -1)
                {
                    if (!paradasAux.Equals(""))
                    {
                        string nombreEdificio = tx_Edificio.Text;
                        string exbo = tx_exbo.Text;  
                        int paradas = Convert.ToInt32(paradasAux);
                        NA_CronogramaTecnico crono = new NA_CronogramaTecnico();
                        DataSet dato = crono.calcularCronogramaTecnico_faceII(Convert.ToDateTime(fechaIni), paradas);
                       // gv_tablaCronograma.DataSource = dato;
                       // gv_tablaCronograma.DataBind();

                        //// Creacion del Excel
                        HttpResponse response = HttpContext.Current.Response;
                        // first let's clean up the response.object
                        response.Clear();
                        response.Charset = "";
                        // set the response mime type for excel
                        response.ContentType = "application/vnd.ms-excel";
                        string nombre = "Cronograma Tecnico Fase II de" + nombreEdificio + " " + exbo + " " + Session["BaseDatos"].ToString(); ;
                        response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

                        // create a string writer
                        using (StringWriter sw = new StringWriter())
                        {
                            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                            {
                                // instantiate a datagrid
                                DataGrid dg = new DataGrid();
                                dg.DataSource = dato;
                                dg.DataBind();
                                dg.RenderControl(htw);
                                response.Write(sw.ToString());
                                response.End();
                            }

                        }

                    }
                    else
                        Response.Write("<script type='text/javascript'> alert('Error: No tiene Numero Paradas') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: No selecciono Equipo') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: No Tiene Fecha Asignada') </script>");
        }


      //----------------------------------------------envio de correo--------------------------

        private void enviarCorreodeFechasModificadas(string edificio, string exbo, string fechaFase1, string fechaFase2, string baseDeDatos, string variableSimec)
        {            
            string cuerpo = "";

            if (!fechaFase1.Equals(""))
            {
                cuerpo = cuerpo + "Fecha Fase 1 = " + fechaFase1 + "<br>";
            }

            if (!fechaFase2.Equals("") )
            {
                cuerpo = cuerpo + "Fecha Fase 2 = " + fechaFase2 + "<br>";
            }

            cuerpo = cuerpo + "Variable Simec = " + variableSimec + "<br>";
            
            if (!cuerpo.Equals(""))
            {
                string asunto = "Correo Automatico de cambio de Fechas del proyecto " + edificio + " con exbo ";
                string datoEnviar = "Envio Automatico del sistema <br> Se ha modificado del Edificio " + edificio + " con numero de Exbo " + exbo + " las siguientes Fechas :<br><br><br>" + cuerpo;
                NA_EnvioCorreo ncorreo = new NA_EnvioCorreo();
                ncorreo.Enviar_Correo_CronogramaTecnicoFaseIFaseII(asunto, datoEnviar, baseDeDatos);
            }

        }





        

  

    }
}