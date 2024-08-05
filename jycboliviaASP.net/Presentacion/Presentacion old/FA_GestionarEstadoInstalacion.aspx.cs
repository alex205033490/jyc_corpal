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
using System.Net.Mail;
using System.Net;


namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_GestionarEstadoInstalacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(24) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            lb_Titulo.Text = "Gestionar Estado de Instalacion " + Session["BaseDatos"].ToString();
            if(!IsPostBack){
            verEquiposEstados("","",-1);
            cargarddlEstadoEquipo();
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

        private void cargarddlEstadoEquipo()
        {
            NEstadoEquipo estadoEquipo = new NEstadoEquipo();
            dd_estadoEquipo.DataSource = estadoEquipo.listar();
            dd_estadoEquipo.DataValueField = "codigo";
            dd_estadoEquipo.DataTextField = "nombre";
            dd_estadoEquipo.Items.Add(new ListItem("--Ninguno--", "-1"));            
            dd_estadoEquipo.AppendDataBoundItems = true;
            dd_estadoEquipo.SelectedIndex = -1;
            dd_estadoEquipo.DataBind();
        }

        private void verEquiposEstados(string exbo, string nombreEdificio,int codEstadoEquipo)
        {
            if(codEstadoEquipo == -1){
                NA_Equipo nequipo = new NA_Equipo();
                DataSet resultado = nequipo.getEquiposProyectosEstadosTodos(exbo, nombreEdificio);
                gv_tablaEquipos.DataSource = resultado;
                gv_tablaEquipos.DataBind();
            }else{
                NA_Equipo nequipo = new NA_Equipo();
                DataSet resultado = nequipo.getEquiposProyectosEstados(exbo, nombreEdificio, codEstadoEquipo);
                gv_tablaEquipos.DataSource = resultado;
                gv_tablaEquipos.DataBind();
            }

            

        }


        private void buscarEquipo()
        {
            string nombreEquipo = tx_nombreEdificio.Text;
            string exbo = tx_exbo.Text;
            int codEstadoEquipo = Convert.ToInt32(dd_estadoEquipo.SelectedValue);
            verEquiposEstados(exbo,nombreEquipo,codEstadoEquipo);       
        }

   
        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            buscarEquipo();
        }

    
        protected void gv_tablaEquipos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_tablaEquipos.EditIndex = e.NewEditIndex;
            int codEstadoEquipo = Convert.ToInt32(dd_estadoEquipo.SelectedValue);
            verEquiposEstados(tx_exbo.Text, tx_nombreEdificio.Text,codEstadoEquipo);

        }
              

        protected void gv_tablaEquipos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_tablaEquipos.EditIndex = -1;
            int codEstadoEquipo = Convert.ToInt32(dd_estadoEquipo.SelectedValue);
            verEquiposEstados(tx_exbo.Text, tx_nombreEdificio.Text,codEstadoEquipo);
        }

        protected void gv_tablaEquipos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gv_tablaEquipos.EditIndex == e.Row.RowIndex)
            {
                DropDownList listaDesplegable = (DropDownList)e.Row.FindControl("DropDownList1");
                if (listaDesplegable != null)
                {
                                       
                    listaDesplegable.DataValueField = "codigo";
                    listaDesplegable.DataTextField = "nombre";

                    NEstadoEquipo estadoEquipo = new NEstadoEquipo();
                    DataSet resultado = estadoEquipo.listar();
                    listaDesplegable.DataSource = resultado;

                    /*
                    ListItem oItem1 = new ListItem("Fabricacion", "11");
                    ListItem oItem2 = new ListItem("Transporte Maritimo", "12");
                    ListItem oItem3 = new ListItem("Transporte Terrestre", "14");
                    ListItem oItem4 = new ListItem("En Puerto", "13");
                    ListItem oItem5 = new ListItem("Recinto Aduanero Doc. Pendiente", "15");
                    ListItem oItem6 = new ListItem("Recinto Aduanero Doc. Completa", "16");
                    ListItem oItem7 = new ListItem("Nacionalizado", "18");
                    ListItem oItem8 = new ListItem("Equipo en Obra", "4");
                    ListItem oItem9 = new ListItem("Equipo en Oficina", "5");
                    ListItem oItem10 = new ListItem("Semana Cero", "17");

                    listaDesplegable.Items.Add(oItem1);
                    listaDesplegable.Items.Add(oItem2);
                    listaDesplegable.Items.Add(oItem3);
                    listaDesplegable.Items.Add(oItem4);
                    listaDesplegable.Items.Add(oItem5);
                    listaDesplegable.Items.Add(oItem6);
                    listaDesplegable.Items.Add(oItem7);
                    listaDesplegable.Items.Add(oItem8);
                    listaDesplegable.Items.Add(oItem9);
                    listaDesplegable.Items.Add(oItem10);                    
                     */
                    string datoR = (e.Row.FindControl("TextBox1") as TextBox).Text;
                    int codigoEstado = estadoEquipo.getCodigoEstadoEquipo(datoR);
                    NEquipo nequipo1 = new NEquipo();
                   // if(nequipo1.estaPermitidoEstadoImportacion(codigoEstado)){
                        listaDesplegable.SelectedValue = codigoEstado.ToString();
                   // }

                        listaDesplegable.AppendDataBoundItems = true;
                        listaDesplegable.DataBind();
                    
                }
            }

        }

        protected void gv_tablaEquipos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;
            DropDownList combo = gv_tablaEquipos.Rows[index].FindControl("DropDownList1") as DropDownList;
            string estado = combo.SelectedValue;
            TextBox CodigoEquipo_Text = (TextBox)gv_tablaEquipos.Rows[index].Cells[1].Controls[0];
            int codigoEquipo = Convert.ToInt32(CodigoEquipo_Text.Text);
            NEquipo equipo = new NEquipo();

           // int codUser = Convert.ToInt32(Session["coduser"].ToString());
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                    
            int CodestadoEquipo = Convert.ToInt32(estado);            
            int codEstadoActual = equipo.getCodigoEstadoActual(codigoEquipo);
            bool permisodeImportacion = equipo.estaPermitidoEstadoImportacion(CodestadoEquipo);
            bool permisodeProyectoEstadoNuevo = equipo.estaPermitidoEstadoProyecto(CodestadoEquipo);
            bool permisodeProyectoEstadoActual = equipo.estaPermitidoEstadoProyecto(codEstadoActual);
            if(
                (permisodeImportacion==true &&
                permisodeProyectoEstadoNuevo==false &&
                permisodeProyectoEstadoActual==false)
                ||
                (CodestadoEquipo == 4 && 
                permisodeProyectoEstadoActual == false)
                ){

                NA_Equipo nequipo = new NA_Equipo();
                int codFechaEstadoUltimoInsertado = nequipo.getCodigoFechaEstadoEquipo(codigoEquipo);

                if(codEstadoActual != CodestadoEquipo){
                    NA_FechaEstadoEquipo fechaEstadoEq = new NA_FechaEstadoEquipo();
                    fechaEstadoEq.insertar(codigoEquipo, CodestadoEquipo,codUser);
                    codFechaEstadoUltimoInsertado = fechaEstadoEq.ultimoinsertado();
                }

                TextBox fechalimiteplanosAprovacionAux = (TextBox)gv_tablaEquipos.Rows[index].FindControl("tx_fechalimitePlanos_GV");
                string fechalimiteplanosAprovacion = equipo.aFecha(fechalimiteplanosAprovacionAux.Text);
                TextBox Exbo_Text = (TextBox)gv_tablaEquipos.Rows[index].Cells[2].Controls[0];
                string fechaAproxPuertoBD = equipo.getFechaAproxArriboPuerto(Exbo_Text.Text);
                TextBox fechaAproxPuertoTexto = (TextBox)gv_tablaEquipos.Rows[index].FindControl("tx_fechaAproxPuerto_GV");
                string fechaAproxPuertoActual = equipo.aFecha(fechaAproxPuertoTexto.Text);                
                equipo.ModificarFechaEstadoEquipo2(codigoEquipo, codFechaEstadoUltimoInsertado, fechalimiteplanosAprovacion, fechaAproxPuertoActual);
                ///--------------------------------------------
                NA_Historial nhistorial = new NA_Historial();
               // int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha Modificado el equipo " + codigoEquipo + " del Exbo " + Exbo_Text.Text);
                ///--------------------------------------------
                ///
                TextBox NombreEdificio_text = (TextBox)gv_tablaEquipos.Rows[index].Cells[3].Controls[0];
                string NombreEdificio = NombreEdificio_text.Text;
                if (CodestadoEquipo == 13)
                {
                    string asunto = "(" + Session["BaseDatos"].ToString() + ")" + " Cambio Estado en Puerto del Edificio = " + NombreEdificio + " del Exbo=" + Exbo_Text.Text;
                    string cuerpo = "Correo Automatico. <br><br>" +
                                    "El Edifico " + NombreEdificio + " con la referencia XBO " + Exbo_Text.Text + " <br>" +
                                    "ARRIBO AL PUERTO DE ARICA <br><br><br>"+
                                    "Fin de Mensaje.";
                    NA_EnvioCorreo ncorreo = new NA_EnvioCorreo();
                    string baseDatos = Session["BaseDatos"].ToString();
                    ncorreo.enviar_Correo_gestionarEstadoInstalacion(asunto, cuerpo, baseDatos);
                }
                 
                if (!fechaAproxPuertoBD.Equals(fechaAproxPuertoTexto.Text))
                {
                    string asunto = "(" + Session["BaseDatos"].ToString() + ")" + " Cambio fecha Aproximada Arribo a Puerto del Edificio = " + NombreEdificio + " del Exbo=" + Exbo_Text.Text;
                    string cuerpo = "Correo Automatico. <br><br>" +
                                    "El Edifico " + NombreEdificio + " con la referencia XBO " + Exbo_Text.Text + " <br>" +
                                    "Ha cambiado la fecha de Aproximada de Arribo a Puerto: <br>"+
                                    "Fecha Aprox Arribo a Puerto = "+ fechaAproxPuertoTexto.Text + "<br>"+
                                    "<br><br><br>" +
                                    "Fin de Mensaje.";
                    NA_EnvioCorreo ncorreo = new NA_EnvioCorreo();
                    string baseDatos = Session["BaseDatos"].ToString();
                    ncorreo.enviar_Correo_gestionarEstadoInstalacion(asunto, cuerpo, baseDatos);
                }


                gv_tablaEquipos.EditIndex = -1;
                int codEstadoEquipo = Convert.ToInt32(dd_estadoEquipo.SelectedValue);
                verEquiposEstados(tx_exbo.Text, tx_nombreEdificio.Text, codEstadoEquipo);

            }else
                Response.Write("<script type='text/javascript'> alert('ERROR : No puede instroducir este Estado') </script>");            
        }

        protected void tb_excel_Click(object sender, EventArgs e)
        {
            string nombreEdificio = tx_nombreEdificio.Text;
            string exbo = tx_exbo.Text;
            int codEstadoEquipo = Convert.ToInt32(dd_estadoEquipo.SelectedValue);

            DataSet resultado = null;

            if (codEstadoEquipo == -1)
            {
                NA_Equipo nequipo = new NA_Equipo();
                resultado = nequipo.getEquiposProyectosEstadosTodos(exbo, nombreEdificio);
               
            }
            else
            {
                NA_Equipo nequipo = new NA_Equipo();
                resultado = nequipo.getEquiposProyectosEstados(exbo, nombreEdificio, codEstadoEquipo);                
            }


            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Estados Equipos " + Session["BaseDatos"].ToString(); ;
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