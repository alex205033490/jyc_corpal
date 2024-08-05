using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Web.Services;
using System.Web.Script.Services;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_RutaBoletaMantenimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(62) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if (!IsPostBack)
            {
                DateTime fechaNow = DateTime.Now;
                int mes = fechaNow.Month;
                int anio = fechaNow.Year;
                tx_popfechaboleta.Text = fechaNow.ToString("dd/MM/yyyy");

                dd_mesRuta.SelectedIndex = mes - 1;
                tx_anioRuta.Text = anio.ToString();
                verEquiposAsignadosRutas("", "", mes, anio);
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
            gv_equipoAsignado.DataSource = null;
            gv_equipoAsignado.DataBind();

            gv_PersonalAsignado.DataSource = null;
            gv_PersonalAsignado.DataBind();

            gv_boletas.DataSource = null;
            gv_boletas.DataBind();

        }

    
        private void verEquiposAsignadosRutas(string exbo, string edificio, int mes,int anio)
        {           
            NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
            DataSet dato = nruta.getallEquiposRutasAsignadas2(exbo, edificio, mes, anio);
            gv_equipoAsignado.DataSource = dato;
            gv_equipoAsignado.DataBind();
            
            DataSet dato1 = nruta.mostrarTecnicoRuta(mes, anio);
            gv_PersonalAsignado.DataSource = dato1;
            gv_PersonalAsignado.DataBind();
        }

        protected void bt_PersonalAsignado_Click(object sender, EventArgs e)
        {
            buscarPersonalAsignado();
        }

        private void buscarPersonalAsignado()
        {
            DateTime fechaNow = DateTime.Now;
            int mes = dd_mesRuta.SelectedIndex + 1;
            int anio = Convert.ToInt32(tx_anioRuta.Text);

            if(gv_equipoAsignado.SelectedIndex > -1){
            NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();            
            string nombre = tx_namePersonal.Text;
            DataSet dato = nruta.mostrarTecnicoRuta2(nombre,mes,anio);
            gv_PersonalAsignado.DataSource = dato;
            gv_PersonalAsignado.DataBind();
            }
            
        }

        protected void bt_popupok_Click(object sender, EventArgs e)
        {
            ingresarBoletaMantenimiento();
            seleccionadoTecnico();
           // NoSeleccionarTablas();
        }

        private void NoSeleccionarTablas()
        {
            gv_boletas.SelectedIndex = -1;
            gv_equipoAsignado.SelectedIndex = -1;
            gv_PersonalAsignado.SelectedIndex = -1;
        }

        private void ingresarBoletaMantenimiento()
        {
                if (gv_equipoAsignado.SelectedIndex > -1)
                {
                    if (gv_PersonalAsignado.SelectedIndex > -1)
                    {                       
                        int codEquipoAsignado = Convert.ToInt32(gv_equipoAsignado.SelectedRow.Cells[1].Text);
                        int codPersonalAsignado = Convert.ToInt32(gv_PersonalAsignado.SelectedRow.Cells[1].Text);
                        string boleta = tx_NroBoleta.Text;
                        string detalle = tx_DetalleRutapopu.Text;
                        bool cambiorepuesto = chx_popCambioRepuesto.Checked;
                        string fechaboleta = "null";
                        if (!tx_popfechaboleta.Text.Equals(""))
                        {
                            DateTime aux = Convert.ToDateTime(tx_popfechaboleta.Text);
                            fechaboleta = "'" + aux.ToString("yyyy/MM/dd") + "'";
                        }

                        string horallegada = "null";
                        if (!tx_pophorallegada.Text.Equals(""))
                        {
                            horallegada = "'" + tx_pophorallegada.Text + "'";
                        }

                        string horaSalida = "null";
                        if (!tx_popHoraSalida.Text.Equals(""))
                        {
                            horaSalida = "'" + tx_popHoraSalida.Text + "'";
                        }
                        string recepcion = tx_poppersonaconforme.Text;


                        bool banderaArreglo = chx_popArreglo.Checked;
                        string tipoBoleta = dd_tipoBoleta.SelectedItem.Text;
                        bool banderaSinEntradaEdificio = chx_popSiningresoEdificio.Checked;

                        NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
                        bool boletaOK = nruta.insertarBoletaMantenimiento(codEquipoAsignado, codPersonalAsignado, boleta, detalle, cambiorepuesto, fechaboleta, horallegada, horaSalida, recepcion, banderaArreglo, tipoBoleta, banderaSinEntradaEdificio);

                        //------------------historial--------------
                        NA_Responsables Nresp = new NA_Responsables();
                        string usuarioAux = Session["NameUser"].ToString();
                        string passwordAux = Session["passworuser"].ToString();
                        int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                        NA_Historial nhistorial = new NA_Historial();
                        nhistorial.insertar(codUser, "Ha Insertado una boleta Mantenimiento codigo=" + boleta + " equipo=" + codEquipoAsignado);
                        //-------------------------------------------
                        if (banderaArreglo == true)
                        {
                            NA_tareasTecnico ntarea = new NA_tareasTecnico();
                            NEquipo ee = new NEquipo();
                            int codProyecto = ee.getcodigoProyecto(codEquipoAsignado);
                            string nombreProyecto = gv_equipoAsignado.SelectedRow.Cells[3].Text;
                            ntarea.insertTareas(detalle, codUser, codProyecto, nombreProyecto);
                        }

                        if (!detalle.Equals(""))
                        {
                            string asunto = "(" + Session["BaseDatos"].ToString() + ")" + " boleta=" + boleta + ", edificio= " + gv_equipoAsignado.SelectedRow.Cells[3].Text + ", equipo=" + gv_equipoAsignado.SelectedRow.Cells[2].Text;
                            string cuerpo = "Correo Automatico de Boletas de Mantenimiento. <br>" +
                                            "<br>" +
                                            "La boleta de Mantenimiento con codigo=" + boleta + ", edificio= " + gv_equipoAsignado.SelectedRow.Cells[3].Text + " , equipo=" + gv_equipoAsignado.SelectedRow.Cells[2].Text + "<br>" +
                                            "Fecha de boleta= " + fechaboleta + "<br>" +
                                            "Hora LLegada= " + horallegada + "<br>" +
                                            "Hora Salida= " + horaSalida + "<br>" +
                                            "Recibida por= " + recepcion + "<br>" +
                                            "Cambio Repuesto= " + cambiorepuesto + "<br>" +
                                            "Arreglo= " + banderaArreglo + "<br>" +
                                            "Sin Ingreso a Edificio= " + banderaSinEntradaEdificio + "<br>" +
                                            "Responsable Boleta= " + gv_PersonalAsignado.SelectedRow.Cells[2].Text + "<br>" +
                                            "<br>" +
                                            "Tiene la siguiente observacion: <br>" +
                                            detalle + "<br>" +
                                            "<br>" +
                                            "<br>" +
                                            "Fin de Mensaje.";
                            string baseDatos = Session["BaseDatos"].ToString();
                            NA_EnvioCorreo nenvio = new NA_EnvioCorreo();
                            bool correoOK = nenvio.Enviar_Correo_BoletaConObservacion(asunto, cuerpo, baseDatos);

                            if (correoOK == false)
                                Response.Write("<script type='text/javascript'> alert('Error: Envio de Correo') </script>");

                            if (boletaOK)
                            {
                                Response.Write("<script type='text/javascript'> alert('OK: Guardado Boleta Correctamente') </script>");
                            }
                           else
                                Response.Write("<script type='text/javascript'> alert('Error: Guardado') </script>");

                          /*  DataSet dato = nruta.mostrarBoletasMantenimiento(codEquipoAsignado, codPersonalAsignado);
                            gv_boletas.DataSource = dato;
                            gv_boletas.DataBind();*/
                            limpiarPopu();

                            DateTime fechaNow = DateTime.Now;
                            tx_popfechaboleta.Text = fechaNow.ToString("dd/MM/yyyy");                            
                        }
                        // else
                        //    Response.Write("<script type='text/javascript'> alert('Error Envio: No tiene Detalle') </script>");
                    }
                    else
                        Response.Write("<script type='text/javascript'> alert('Error: No ha seleccionado Tecnico') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: No ha seleccionado Equipo') </script>");
            
        }

        protected void gv_PersonalAsignado_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionadoTecnico();
        }

        private void seleccionadoTecnico()
        {
            gv_boletas.SelectedIndex = -1;            
              if(gv_equipoAsignado.SelectedIndex > -1){
                  if(gv_PersonalAsignado.SelectedIndex > -1){                      
                      int codEquipoAsignado = Convert.ToInt32(gv_equipoAsignado.SelectedRow.Cells[1].Text);
                      int codPersonalAsignado = Convert.ToInt32(gv_PersonalAsignado.SelectedRow.Cells[1].Text);
                      string boleta = tx_NroBoleta.Text;                    
                      NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();                      
                      DataSet dato = nruta.mostrarBoletasMantenimiento(codEquipoAsignado,codPersonalAsignado);
                      gv_boletas.DataSource = dato;
                      gv_boletas.DataBind();                      
                  }
              }          
          
        }

        private void limpiarPopu()
        {
            tx_NroBoleta.Text = "";
            tx_DetalleRutapopu.Text = "";
            chx_popCambioRepuesto.Checked = false;
            chx_popArreglo.Checked = false;
            tx_popfechaboleta.Text = "";
            tx_pophorallegada.Text = "";
            tx_popHoraSalida.Text = "";
            tx_poppersonaconforme.Text = "";
        }

        protected void gv_equipoAsignado_SelectedIndexChanged(object sender, EventArgs e)
        {
            gv_PersonalAsignado.SelectedIndex = -1;
            gv_boletas.DataSource = null;
            gv_boletas.DataBind();

            gv_boletas.SelectedIndex = -1;
            seleccionadoTecnico();            
        }

        private void mostrarBoletasMantenimiento( int codEquipoAsignado, int codPersonalAsignado)
        {
            NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
            DataSet dato = nruta.mostrarBoletasMantenimiento( codEquipoAsignado, codPersonalAsignado);
            gv_boletas.DataSource = dato;
            gv_boletas.DataBind();
        }

        protected void gv_boletas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            eliminarBoletaMantenimiento(e);
        }

        private void eliminarBoletaMantenimiento(GridViewDeleteEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int codigoBoleta = Convert.ToInt32(gv_boletas.Rows[e.RowIndex].Cells[1].Text);                
                NA_RutaMantenimiento nruta = new NA_RutaMantenimiento();
                bool bandera = nruta.eliminarBoletaMantenimiento(codigoBoleta);
                Response.Write("<script type='text/javascript'> alert('Eliminado OK') </script>");
                               
                int codEquipoAsignado = Convert.ToInt32(gv_equipoAsignado.SelectedRow.Cells[1].Text);
                int codPersonalAsignado = Convert.ToInt32(gv_PersonalAsignado.SelectedRow.Cells[1].Text);                
                DataSet dato = nruta.mostrarBoletasMantenimiento(codEquipoAsignado, codPersonalAsignado);
                gv_boletas.DataSource = dato;
                gv_boletas.DataBind();                      
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: NO Tiene Ruta Seleccionada') </script>");

        }

        protected void bt_buscarEquipo_Click(object sender, EventArgs e)
        {
            int mes = Convert.ToInt32(dd_mesRuta.SelectedIndex + 1);
            int anio = Convert.ToInt32(tx_anioRuta.Text);
            string exbo = tx_exbo.Text;
            string edificio = tx_edificio.Text;
            verEquiposAsignadosRutas(exbo, edificio, mes, anio);
        }

        
      

     

       

    }
}