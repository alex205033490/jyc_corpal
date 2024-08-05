using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Globalization;
using System.Configuration;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Drawing;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_GCotizacionRepuesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(47) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if(!IsPostBack){                
                negarBotones();
              //  ponernegativocheckbox();
                habilitarBotonesRCC_almacen();                
                buscarCotizacion("", "", "", false,false,false);
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

      /*  private void ponernegativocheckbox()
        {
            ckb_areaCliente.Enabled = false;
        }
        */
        private void negarBotones()
        {           
            bt_cerrarCotizacion.Enabled = false;
        }


      private void habilitarBotonesRCC_almacen() {
          NA_Responsables Nresp = new NA_Responsables();
          string usuarioAux = Session["NameUser"].ToString();
          string passwordAux = Session["passworuser"].ToString();
          int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

          if (codUser != -1)
          {
              List<int> listaPermisos = Nresp.getPermisoUsuario(codUser);
              for (int i = 0; i < listaPermisos.Count; i++)
              {
                  int codPermiso = Convert.ToInt32(listaPermisos[i].ToString());
                  if (codPermiso == 48)
                  {                     
                      bt_cerrarCotizacion.Enabled = true;                      
                  }
                  
                /*  if(codPermiso == 67){
                      ckb_areaCliente.Enabled = true;
                  } */
              }
          }  
        }

    

      

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string codigo = tx_codigo.Text;
            string edificio = tx_edificio.Text;
            string cite = tx_cite.Text;
            bool vendido = ckb_vendido.Checked;
            bool rechazado = ckb_rechazado.Checked;
           // bool areacliente = ckb_areaCliente.Checked;
            bool areacliente = false;
            buscarCotizacion(codigo,edificio,cite,vendido,rechazado,areacliente);
        }

        private void buscarCotizacion(string codigoCotiR, string edificio, string cite, bool vendido, bool rechazado, bool areacliente)
        {
            NA_Repuesto nrepuesto = new NA_Repuesto();
            DataSet dato ;
            string estadoCoti = dd_estadoCoti.SelectedItem.Text;            

            dato = nrepuesto.getCotizacionesRepuestoRCC(codigoCotiR, edificio, cite, estadoCoti,vendido,rechazado,areacliente, false);                
         
            gv_cotizacionRepuesto.DataSource = dato;
            gv_cotizacionRepuesto.DataBind();
            ponerColoresAequiposConPrioridad(gv_cotizacionRepuesto);

            gv_cotizacionRepuesto.SelectedIndex = -1;
            gv_repuestosAdicionados.SelectedIndex = -1;
        }


        public void ponerColoresAequiposConPrioridad(GridView gv_tablaDatosAux)
        {
            for (int i = 0; i < gv_tablaDatosAux.Rows.Count; i++)
            {
                string Prioridad = gv_tablaDatosAux.Rows[i].Cells[16].Text;
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

        protected void gv_cotizacionRepuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarCotizacionRepuesto();
            if (gv_cotizacionRepuesto.SelectedIndex > -1)
            {
            gv_cotizacionRepuesto.SelectedRow.BackColor = Color.Lime;
            }
          //  ponercheckbox();
        }

        private void seleccionarCotizacionRepuesto()
        {
            int codigoCotizacion = Convert.ToInt32(gv_cotizacionRepuesto.SelectedRow.Cells[1].Text);            
            NA_Repuesto nrepuesto = new NA_Repuesto();
            DataSet dato = nrepuesto.getRepuestoCotizacionDetalle_RCC(codigoCotizacion);
            gv_repuestosAdicionados.DataSource = dato;
            gv_repuestosAdicionados.DataBind();
            

            if (HttpUtility.HtmlDecode(gv_cotizacionRepuesto.SelectedRow.Cells[7].Text).Equals("&nbsp;"))
            {
            tx_cierreCotizacion.Text = "";
            }else
                tx_cierreCotizacion.Text = HttpUtility.HtmlDecode(gv_cotizacionRepuesto.SelectedRow.Cells[7].Text);


            if (nrepuesto.tieneEventoCallCenterAsignado(codigoCotizacion))
            {
                int codEvento = nrepuesto.getCodigoEventoCallCenterAsignado(codigoCotizacion);
                NA_Evento neven = new NA_Evento();
                DataSet datoEven = neven.getEvento(codEvento);

                string fechaContactoCliente = datoEven.Tables[0].Rows[0][31].ToString();
                tx_fechaContactoCliente.Text = fechaContactoCliente;

                string detalleContactoCliente = datoEven.Tables[0].Rows[0][32].ToString();
                tx_detalleContactoCliente.Text = detalleContactoCliente;
            }

        }


    /*    public void ponercheckbox()
        {
            NA_Repuesto nrepuesto = new NA_Repuesto();

            for (int i = 0; i < gv_repuestosAdicionados.Rows.Count; i++)
            {
                int codRepuesto = Convert.ToInt32(gv_repuestosAdicionados.Rows[i].Cells[6].Text);
                int codCotiRepuesto = Convert.ToInt32(gv_cotizacionRepuesto.SelectedRow.Cells[1].Text);
                if (nrepuesto.estaenAlmacenLocal(codCotiRepuesto,codRepuesto))
                {                    
                    CheckBox cb = (CheckBox)gv_repuestosAdicionados.Rows[i].Cells[0].FindControl("CheckBox1");
                    cb.Checked = true;
                }

                if (nrepuesto.estaenAlmacenJyC(codCotiRepuesto,codRepuesto))
                {
                    CheckBox cb = (CheckBox)gv_repuestosAdicionados.Rows[i].Cells[1].FindControl("CheckBox2");
                    cb.Checked = true;
                }


                if (nrepuesto.estaEntregadoelRepuestoLocal(codCotiRepuesto, codRepuesto))
                {
                    CheckBox cb = (CheckBox)gv_repuestosAdicionados.Rows[i].Cells[2].FindControl("CheckBox3");
                    cb.Checked = true;
                }


                if (nrepuesto.estaEnProcesodeCompraRepuesto(codCotiRepuesto, codRepuesto))
                {
                    CheckBox cb = (CheckBox)gv_repuestosAdicionados.Rows[i].Cells[3].FindControl("CheckBox4");
                    cb.Checked = true;
                }


                if (nrepuesto.estaEntregadoelRepuestoJyC(codCotiRepuesto, codRepuesto))
                {
                    CheckBox cb = (CheckBox)gv_repuestosAdicionados.Rows[i].Cells[4].FindControl("CheckBox5");
                    cb.Checked = true;
                }

                if (nrepuesto.estaEntregadoelRepuestoJyCIA(codCotiRepuesto, codRepuesto))
                {
                    CheckBox cb = (CheckBox)gv_repuestosAdicionados.Rows[i].Cells[5].FindControl("CheckBox6");
                    cb.Checked = true;
                }
             

            }

        }  
     
     */



/*
        protected void bt_pedir_Click(object sender, EventArgs e)
        {
            pedirRepuesto();
            ponercheckbox();
        }

        
        private void pedirRepuesto()
        {    
            NA_Repuesto nrepuesto = new NA_Repuesto();
            
            for (int i = 0; i < gv_repuestosAdicionados.Rows.Count; i++)
            {
                int codRepuesto = Convert.ToInt32(gv_repuestosAdicionados.Rows[i].Cells[6].Text);
                int codCotiRepuesto = Convert.ToInt32(gv_cotizacionRepuesto.SelectedRow.Cells[1].Text);
              
                    CheckBox cb = (CheckBox)gv_repuestosAdicionados.Rows[i].Cells[0].FindControl("CheckBox1");
                    CheckBox cb2 = (CheckBox)gv_repuestosAdicionados.Rows[i].Cells[1].FindControl("CheckBox2");
                    CheckBox cb3 = (CheckBox)gv_repuestosAdicionados.Rows[i].Cells[2].FindControl("CheckBox3");

                    if (cb.Checked == true && cb2.Checked == true)
                    {
                        Response.Write("<script type='text/javascript'> alert('El repuesto " + codRepuesto + " no puede estar en local y JyC') </script>");
                        break;
                    }
                    else {
                        if (cb.Checked == true && nrepuesto.estaenAlmacenLocal(codCotiRepuesto, codRepuesto) == false)
                        {
                            nrepuesto.actualizarAlmacenLocal(codCotiRepuesto,codRepuesto,true);
                        }

                        if (cb2.Checked == true && nrepuesto.estaenAlmacenJyC(codCotiRepuesto, codRepuesto) == false)
                        {
                            nrepuesto.actualizarAlmacenJYC(codCotiRepuesto, codRepuesto, true);
                        }

                        if (cb3.Checked == true && nrepuesto.estaEntregadoelRepuestoLocal(codCotiRepuesto, codRepuesto) == false)
                        {
                            nrepuesto.actualizarEntregadoRepuestoLocal(codCotiRepuesto, codRepuesto, true);
                        }
                    
                    }
            }
        }



        private void ActualizarPedido()
        {
            NA_Repuesto nrepuesto = new NA_Repuesto();
            int codigoCoti = Convert.ToInt32(gv_cotizacionRepuesto.SelectedRow.Cells[1].Text);
            string Edificio = gv_cotizacionRepuesto.SelectedRow.Cells[4].Text;
            string cuerpoDelMensaje = "";
            string saltolinea = "<br/>";


            for (int i = 0; i < gv_repuestosAdicionados.Rows.Count; i++)
            {
                int codRepuesto = Convert.ToInt32(gv_repuestosAdicionados.Rows[i].Cells[6].Text);
                int codCotiRepuesto = Convert.ToInt32(gv_cotizacionRepuesto.SelectedRow.Cells[1].Text);
                

                CheckBox cb = (CheckBox)gv_repuestosAdicionados.Rows[i].Cells[3].FindControl("CheckBox4");
                CheckBox cb2 = (CheckBox)gv_repuestosAdicionados.Rows[i].Cells[4].FindControl("CheckBox5");
                CheckBox cb3 = (CheckBox)gv_repuestosAdicionados.Rows[i].Cells[5].FindControl("CheckBox6");

                if (cb2.Checked == true && cb3.Checked == true)
                {
                        Response.Write("<script type='text/javascript'> alert('El repuesto " + codRepuesto + " no puede ser entregado por en JyC y JyCIA') </script>");
                        break;
                }
                else {
                    if (cb.Checked == true && (nrepuesto.estaEnProcesodeCompraRepuesto(codCotiRepuesto, codRepuesto) == false))
                    {
                        nrepuesto.actualizarCompraEnProceso(codCotiRepuesto, codRepuesto, true);
                    }

                    if (cb2.Checked == true && (nrepuesto.estaEntregadoelRepuestoJyC(codCotiRepuesto, codRepuesto) == false))
                    {
                        nrepuesto.actualizarEntregaRepuestoJYC(codCotiRepuesto, codRepuesto, true);
                        string numeroR = gv_repuestosAdicionados.Rows[i].Cells[7].Text;
                        string detalle = gv_repuestosAdicionados.Rows[i].Cells[8].Text;
                        cuerpoDelMensaje = cuerpoDelMensaje + " * Puede Pasar a Recojer el Repuesto " + numeroR +
                                           " " + detalle + saltolinea;
                    }

                    if (cb3.Checked == true && (nrepuesto.estaEntregadoelRepuestoJyCIA(codCotiRepuesto, codRepuesto) == false))
                    {
                        nrepuesto.actualizarEntregaRepuestoJYCIA(codCotiRepuesto, codRepuesto, true);
                        string numeroR = gv_repuestosAdicionados.Rows[i].Cells[7].Text;
                        string detalle = gv_repuestosAdicionados.Rows[i].Cells[8].Text;
                        cuerpoDelMensaje = cuerpoDelMensaje + " * Puede Pasar a Recojer el Repuesto " + numeroR +
                                           " " + detalle + saltolinea;
                    }                
                
                }

                }

            if(!cuerpoDelMensaje.Equals("")){
                string Mensaje = "Se ha realizado la Entrega de la Cotizacion " + codigoCoti + " del Edificio " + Edificio + saltolinea+saltolinea;
                Mensaje = Mensaje + cuerpoDelMensaje;
                Enviar_Correo(Mensaje);
            }

            
            }
       
        protected void bt_actualizar_Click(object sender, EventArgs e)
        {
            ActualizarPedido();
            ponercheckbox();
        }
        */
        protected void gv_repuestosAdicionados_RowCreated(object sender, GridViewRowEventArgs e)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            if (codUser != -1)
            {
                List<int> listaPermisos = Nresp.getPermisoUsuario(codUser);
                for (int i = 0; i < listaPermisos.Count; i++)
                {
                    int codPermiso = Convert.ToInt32(listaPermisos[i].ToString());
                    if (codPermiso == 48)
                    {
                        e.Row.Cells[3].Enabled = false;
                        e.Row.Cells[4].Enabled = false;
                        e.Row.Cells[5].Enabled = false;       
                    }

                    if (codPermiso == 49)
                    {
                        e.Row.Cells[0].Enabled = false;
                        e.Row.Cells[1].Enabled = false;
                        e.Row.Cells[2].Enabled = false;                        
                    }
                }
            }
   
            }



        protected void bt_cerrarCotizacion_Click(object sender, EventArgs e)
        {
            cerrarCotizacion_Evento();
            bool vendido = false;
            bool rechazado = false;          
            bool areaCliente = false;
            ckb_rechazado.Checked = rechazado;
            ckb_vendido.Checked = vendido;
         
            buscarCotizacion(tx_codigo.Text, tx_edificio.Text, tx_cite.Text,vendido,rechazado,areaCliente);
        }

        
        private void cerrarCotizacion_Evento()
        {
           if(gv_cotizacionRepuesto.SelectedIndex > -1){
               int codigoCoti = Convert.ToInt32(gv_cotizacionRepuesto.SelectedRow.Cells[1].Text);
               NA_Repuesto nrepuesto = new NA_Repuesto();
               string cierreCotizacion = tx_cierreCotizacion.Text;
               bool vendido = ckb_vendido.Checked;
               bool rechazado = ckb_rechazado.Checked;
               //bool areaCliente = ckb_areaCliente.Checked;
               bool areaCliente = false;
               bool areaEra = false;
               string estadoCoti = dd_estadoCoti.SelectedItem.Text;

               string fechaContactoCliente = convertidorFecha(tx_fechaContactoCliente.Text);
               string detalleContactoCliente = tx_detalleContactoCliente.Text;

               NA_Evento evento = new NA_Evento();
               ///--------------------------------------------------------
               NA_Responsables Nresp = new NA_Responsables();
               string usuarioAux = Session["NameUser"].ToString();
               string passwordAux = Session["passworuser"].ToString();
               int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
               //---------------------------------------------------------                 
               
               if(vendido == true){
                   estadoCoti = "Cerrado";
                   areaEra = true;
                       if (nrepuesto.tieneEventoCallCenterAsignado(codigoCoti))
                       {
                           int codEvento = nrepuesto.getCodigoEventoCallCenterAsignado(codigoCoti);                           
                           evento.updatefechaAceptacionProformaEvento(codEvento);                           
                         /*  DataSet dato = evento.getCodigoEdificioEvento(codEvento);
                           int codigoEdificio;
                           bool isnumero = int.TryParse(dato.Tables[0].Rows[0][0].ToString(), out codigoEdificio);                           
                               if(isnumero == true && codigoEdificio > 0){                                   
                                   NProyecto proy = new NProyecto();
                                   proy.modificarDeudaRepuestoProyeto(codigoEdificio, true);  /// problema cuando no existe el equipo
                               }  */
                        }
                   //--------------envio de correo----------
                       string baseDatos = Session["BaseDatos"].ToString();
                       string edificio = gv_cotizacionRepuesto.SelectedRow.Cells[4].Text;
                       string codTicket = gv_cotizacionRepuesto.SelectedRow.Cells[17].Text;
                       string codR144 = gv_cotizacionRepuesto.SelectedRow.Cells[18].Text;
                       NA_EnvioCorreo ncorreo = new NA_EnvioCorreo();
                       string Asunto = "(" + baseDatos + ") APROBACION R-138 COTIZACION DE REPUESTO DEL EDIFICIO "+edificio; 
                       string Cuerpo ="Correo Automatico. <br><br>" +
                                    "El Edifico " + edificio + " acepto la Cotizacion R-138 con el codigo " +codigoCoti + " <br>" +
                                    "El codigo de solicitud R-144 es " + codR144 + " <br>" +
                                    "El codigo de Ticket de CallCenter es " + codTicket + " <br>" +
                                    "<br><br><br>"+
                                    "Fin de Mensaje.";
                       ncorreo.enviar_Correo_CotizacionesRepuesto(Asunto, Cuerpo, baseDatos); 

                   }

                   if(rechazado == true){
                       estadoCoti = "Cerrado";
                       if (nrepuesto.tieneEventoCallCenterAsignado(codigoCoti))
                       {
                           int codEvento = nrepuesto.getCodigoEventoCallCenterAsignado(codigoCoti);                           
                           string detalleCierre = tx_cierreCotizacion.Text;                           
                           int cantCoticonElMismoEvento = evento.getCantCoticonElMismoEvento(codEvento);
                           int cantCoticonElMismoEvento_Cerrado = evento.getCantCoticonElMismoEvento_estadoCerrado(codEvento);
                           // si la cantidad de cotizaciones es igual a las cotizaciones cerradas , se cierra el evento
                           if(cantCoticonElMismoEvento == (cantCoticonElMismoEvento_Cerrado + 1)){
                               if (!evento.updateCerrarEventoCotizacion(codEvento, codUser, detalleCierre))
                               {
                                   Response.Write("<script type='text/javascript'> alert('Error: no se pudo Cerrar el evento') </script>");
                               }
                           }
                           
                       }
                   }

               /*
                   if (areaCliente == true)
                   {
                       if (nrepuesto.tieneEventoCallCenterAsignado(codigoCoti))
                       {
                           int codEvento = nrepuesto.getCodigoEventoCallCenterAsignado(codigoCoti);                           
                           string detalleCierre = tx_cierreCotizacion.Text;
                           estadoCoti = "Cerrado";                           
                           if (!evento.updateCerrarEventoCotizacionAreaCliente(codEvento, detalleCierre))
                           {
                               Response.Write("<script type='text/javascript'> alert('Error: no se pudo Cerrar el evento') </script>");
                           }
                       }
                   }*/


                   if (nrepuesto.tieneEventoCallCenterAsignado(codigoCoti))
                   {
                       int codEvento = nrepuesto.getCodigoEventoCallCenterAsignado(codigoCoti);
                       evento.modificarDatosEvento_FechaContactoCliente(codEvento, fechaContactoCliente, detalleContactoCliente);
                   }

                   bool bandera = nrepuesto.modificarCotizacionRepuesto(codigoCoti, estadoCoti, cierreCotizacion, vendido, rechazado, areaCliente, codUser, areaEra);
                   if (bandera == true)
                   {
                       Response.Write("<script type='text/javascript'> alert('Cotizacion Modificada') </script>");
                   }
                   else
                       Response.Write("<script type='text/javascript'> alert('Error') </script>"); 

             }else
              Response.Write("<script type='text/javascript'> alert('Error: No Selecciono Cotizacion Repuesto') </script>"); 
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            mostrarCoti();
        }

        private void mostrarCoti()
        {
           
            /*
                Session["EdificioRepuesto"] = gv_cotizacionRepuesto.SelectedRow.Cells[4].Text;
                Session["numeroCotiRepuesto"] = gv_cotizacionRepuesto.SelectedRow.Cells[5].Text;
                DateTime dt = DateTime.ParseExact(gv_cotizacionRepuesto.SelectedRow.Cells[2].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                Session["fechaRepuesto"] = Session["BaseDatos"].ToString() + ", " + dt.ToString("dd") + " de " + dt.ToString("MMMM") + " de " + dt.ToString("yyyy");
                Session["TotalRepuesto"] = gv_cotizacionRepuesto.SelectedRow.Cells[6].Text;

                N_numLetra nl = new N_numLetra();
                Session["PrecioLetras"] = "Son : " + nl.Convertir(gv_cotizacionRepuesto.SelectedRow.Cells[6].Text, true, "Dólares Americanos");


              //  if (cb_el.Checked)
             //   {
                    Session["ellosRepuesto"] = "el ascensor";
                    Session["instalados"] = "instalado";
              /*  }
                else
                {
                    Session["ellosRepuesto"] = "los ascensores";
                    Session["instalados"] = "instalados";
                }  */

             /*   NA_Repuesto nrepuesto = new NA_Repuesto();
                int codCoti = Convert.ToInt32(gv_cotizacionRepuesto.SelectedRow.Cells[1].Text);
                DataSet datosItemCoti = nrepuesto.getItemCoti(codCoti);

              


                DataTable datoRepuesto = new DataTable();
                datoRepuesto.Columns.Add("Codigo", typeof(string));
                datoRepuesto.Columns.Add("numeracion", typeof(string));
                datoRepuesto.Columns.Add("Detalle", typeof(string));
                datoRepuesto.Columns.Add("Precio", typeof(string));
                datoRepuesto.Columns.Add("Cantidad", typeof(string));
                datoRepuesto.Columns.Add("PrecioTotal", typeof(string));

                int cant = datosItemCoti.Tables[0].Rows.Count;
                for (int i = 0; i < cant; i++)
                {
                    DataRow tupla = datoRepuesto.NewRow();

                    tupla["Codigo"] = datosItemCoti.Tables[0].Rows[i][0].ToString();
                    tupla["numeracion"] = datosItemCoti.Tables[0].Rows[i][1].ToString();
                    tupla["Detalle"] = datosItemCoti.Tables[0].Rows[i][2].ToString();
                    tupla["Precio"] = datosItemCoti.Tables[0].Rows[i][3].ToString();
                    tupla["Cantidad"] = datosItemCoti.Tables[0].Rows[i][4].ToString();
                    tupla["PrecioTotal"] = datosItemCoti.Tables[0].Rows[i][5].ToString();

                    datoRepuesto.Rows.Add(tupla);
                }
            
                Session["listaRepuesto"] = datoRepuesto;
                Session["codcotiRepuesto"] = gv_cotizacionRepuesto.SelectedRow.Cells[1].Text;
            */
                Session["CodigoCotizacion"] = gv_cotizacionRepuesto.SelectedRow.Cells[1].Text;

                //---------------------- si no tiene premiso no ingresa a crear la carta
                Response.Redirect("../Presentacion/FA_ReporteCotizacionRepuesto.aspx");
                //---------------------------------------------------------------------

               
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            excel();
        }

        private void excel()
        {

            string codigoCotiR = tx_codigo.Text;
            string edificio = tx_edificio.Text;
            string cite = tx_cite.Text;
            bool vendido = ckb_vendido.Checked;
            bool rechazado = ckb_rechazado.Checked;
            //bool areacliente = ckb_areaCliente.Checked;
            bool areacliente = false;
            NA_Repuesto nrepuesto = new NA_Repuesto();
            DataSet dato;
            string estadoCoti = dd_estadoCoti.SelectedItem.Text;

            dato = nrepuesto.getCotizacionesRepuestoRCC(codigoCotiR, edificio, cite, estadoCoti, vendido, rechazado, areacliente, true);
            
            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Cotizaciones - " + Session["BaseDatos"].ToString();
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

        protected void bt_R144_Click(object sender, EventArgs e)
        {
            crearR144();
        }


        private void crearR144()
        {
            if (gv_cotizacionRepuesto.SelectedIndex > -1)
            {
                int number;
                string dato = gv_cotizacionRepuesto.SelectedRow.Cells[15].Text;
                bool esNumero = Int32.TryParse(dato, out number);
                int codigoCoti = 0;
                if (esNumero)
                {
                    codigoCoti = number;
                }
                else
                    codigoCoti = Convert.ToInt32(gv_cotizacionRepuesto.SelectedRow.Cells[1].Text);

                Session["CodigoR144"] = codigoCoti;
                Response.Redirect("../Presentacion/FA_Reporte_R144.aspx");
            }
            else
                Response.Write("<script type='text/javascript'> alert('ERROR: No Selecciono Cotizacion') </script>");                       
        }
 

        private void descargarArchivo(string RutaArchivo, string nombreArchivo)
        {
            try
            {
                if (!Directory.Exists(RutaArchivo))
                    Directory.CreateDirectory(RutaArchivo);

                RutaArchivo = RutaArchivo + "/" + nombreArchivo;

                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                string nombreAux = nombreArchivo.Replace(" ", "_");
                Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreAux);
                // Escribimos el fichero a enviar 
                Response.WriteFile(RutaArchivo);
                // volcamos el stream 
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
            }
            catch (Exception)
            {
                //  Response.Write("<script type='text/javascript'> alert('ERROR: " + ex.Message + "') </script>");
            }
        }


    }
}