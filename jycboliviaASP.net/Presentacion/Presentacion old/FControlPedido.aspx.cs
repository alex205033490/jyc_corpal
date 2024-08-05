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
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FControlPedido : System.Web.UI.Page
    {
        
        
       
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(22) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            lb_titulo.Text = "Control de Pedido " + Session["BaseDatos"].ToString();
            
            if (!IsPostBack)
            {
                limpiar();
                cargarEstadosEquipos();
                BuscarControl("","","","","","","","","","");
                string baseDatos = Session["BaseDatos"].ToString();
                ddlCiudad.SelectedValue = baseDatos;
                cargarddTipoEquipo();
                cargarddMarca();
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


        private void cargarddTipoEquipo()
        {
            NA_TipoEquipo NtipoEquipo = new NA_TipoEquipo();
            dd_tipoEquipo.DataSource = NtipoEquipo.mostrarAllDatos();
            dd_tipoEquipo.DataValueField = "codigo";
            dd_tipoEquipo.DataTextField = "nombre";
            dd_tipoEquipo.Items.Add(new ListItem("--Ninguno--", "-1"));
            dd_tipoEquipo.AppendDataBoundItems = true;
            dd_tipoEquipo.SelectedIndex = -1;
            dd_tipoEquipo.DataBind();
        }

        private void cargarddMarca()
        {
            NA_Marca Nmarca = new NA_Marca();
            dd_marca.DataSource = Nmarca.mostrarAllDatos();
            dd_marca.DataValueField = "codigo";
            dd_marca.DataTextField = "nombre";
            dd_marca.Items.Add(new ListItem("--Ninguno--", "-1"));
            dd_marca.AppendDataBoundItems = true;
            dd_marca.SelectedIndex = -1;
            dd_marca.DataBind();
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

        public void cargarEstadosEquipos() {
         //   NEstadoEquipo estadoEquipo = new NEstadoEquipo();
            dd_Estado.DataValueField = "codigo";
            dd_Estado.DataTextField = "nombre";
          //  DataSet dato = estadoEquipo.listar();
            //dd_Estado.DataSource = dato;            
            ListItem oItem = new ListItem("Codificado", "19");
            ListItem oItem1 = new ListItem("Vendidos", "1");
            ListItem oItem2 = new ListItem("Pedidos", "2");
            ListItem oItem3 = new ListItem("Pendiente de Contrato", "20");
            dd_Estado.Items.Add(oItem);
            dd_Estado.Items.Add(oItem1);
            dd_Estado.Items.Add(oItem2);
            dd_Estado.Items.Add(oItem3);
            dd_Estado.SelectedIndex = 1;
            dd_Estado.DataBind();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            registrar();
            BuscarControl(tx_Proyecto.Text,txbExbo.Text,txbCliente.Text,txbPasajero.Text,txbParada.Text,txbModelo.Text,tx_velocidad.Text,"","",tx_fichero.Text);
            gvControlPedido.SelectedIndex = -1;
        }



        private void registrar()
        {
            NProyecto proyecto = new NProyecto();
            NEquipo equipo = new NEquipo();
            NA_Propietario encargadoPago = new NA_Propietario();       

            string nombreProyecto = tx_Proyecto.Text;
            string exbo = txbExbo.Text;
            string ciudad = ddlCiudad.Text;
            string ciudadVenta = dd_ciudadVenta.Text;
            string pasajero = txbPasajero.Text;            
            string modelo = txbModelo.Text;
            string velocidad = tx_velocidad.Text;
            float vc;
            float.TryParse(tx_vc.Text.Replace('.', ','), out vc);


            string cliente = txbCliente.Text;
            float primerPago;
            float.TryParse(txbPagoContrato.Text.Replace('.', ','), out primerPago);

            int parada ;
            int.TryParse(txbParada.Text, out parada);


            string nombreEncargadoPago = txbCliente.Text;
            string fichero = tx_fichero.Text;
            string fecha = equipo.obtenerFechaActual();
            string tipologia = pasajero + "/" + parada + "/" + modelo + "/" + velocidad;

           
            bool r110 = cbxr110.Checked;
            bool r148 = cbxr148.Checked;
            bool r106 = cbxr106.Checked;
            bool r107 = cbxr107.Checked;
            bool r109 = cbxr109.Checked;
            bool r113 = cbxr113.Checked;
            bool inventario = chbx_stock.Checked;
            bool ventaSistema = cbxprimerpago.Checked;
            bool ventaSistema2 = ChxventaContrato.Checked;
            string fechaContrato = convertidorFecha(tx_fechaContrato.Text);

            if(nombreProyecto != ""){
            if( !equipo.existeEquipo(exbo) || exbo == "" ){
            int codigoEncargadoPago = -1;
            if (!encargadoPago.existe(cliente))
            {
                encargadoPago.registrar1(cliente, 1);
                codigoEncargadoPago = encargadoPago.obtenerUltimoCodigo();
            }
            else
            {
                codigoEncargadoPago = encargadoPago.obtenerCodigoEncargadoPago(nombreEncargadoPago);
            }

            string empresacontratoproyecto = dd_empresacontratoproyecto.SelectedItem.Text;

            int codigoProyecto = -1;
            if (!proyecto.existe(nombreProyecto))
            {
                proyecto.registrar1(nombreProyecto, nombreProyecto, fecha, -1, 1, ciudad, codigoEncargadoPago, empresacontratoproyecto);
                codigoProyecto = proyecto.obtenerUltimoCodigo();
            }
            else
            {
                codigoProyecto = proyecto.getCodigoProyect(nombreProyecto);
            }

            int codTipoEquipo = Convert.ToInt32(dd_tipoEquipo.SelectedValue);
            int codmarca = Convert.ToInt32(dd_marca.SelectedValue);

            float valorTransporteMaritimo = Convert.ToSingle(tx_valorTransporteMaritimo.Text.Replace('.', ','));
            string fechaAproxEmbarque = convertidorFecha(tx_fechaAproxEmbarque.Text);
            string fechaPagoEmbarque = convertidorFecha(tx_fechaPagoEmbarque.Text);

            string equipoEntregadoSegunContrato = convertidorFecha(tx_equipoEntregadoSegunContrato.Text);
            string codigoContrato = tx_CodigoContrato.Text;
            bool contratoFirmado = cb_contratoFirmado.Checked;
            string consignatario = dd_consignatario.Text;
            string idAscensor = tx_idAscensor.Text;

            equipo.registrarEquipoControlPedido(exbo, fecha, codigoProyecto, r110, r148, r106, r107, r109, r113, ventaSistema, primerPago, tipologia, fichero, fechaContrato, ventaSistema2, modelo, parada, pasajero, velocidad, vc, codTipoEquipo, codmarca, ciudadVenta, ciudad, fechaAproxEmbarque, valorTransporteMaritimo, inventario, fechaPagoEmbarque, equipoEntregadoSegunContrato, codigoContrato, consignatario, contratoFirmado, idAscensor, empresacontratoproyecto);
            int codEquipoUltimoInsertado = equipo.ultimoinsertado();
             //int codUser = Convert.ToInt32(Session["coduser"].ToString());

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            int CodestadoEquipo = Convert.ToInt32(dd_Estado.SelectedValue);
            NA_FechaEstadoEquipo fechaEstadoEquipo = new NA_FechaEstadoEquipo();
            fechaEstadoEquipo.insertar(codEquipoUltimoInsertado, CodestadoEquipo,codUser);
            int codFechaEstadoEquipoUltimoInsertado = fechaEstadoEquipo.ultimoinsertado();
            
            equipo.ModificarFechaEstadoEquipo(codEquipoUltimoInsertado, codFechaEstadoEquipoUltimoInsertado);            
            NA_EnvioCorreo ncorreo = new NA_EnvioCorreo();
                if(CodestadoEquipo == 1){
                    string baseDatos = Session["BaseDatos"].ToString();
                    string asunto1 = "Equipo Vendido exbo= " + exbo + " del Edificio = " + nombreProyecto + "  (" + baseDatos + ")";
                    string cuerpo2 = "Se ha colocado el estado Vendido el equipo con Exbo "+exbo+" del Edificio "+
                        nombreProyecto+" de la UNE "+baseDatos;
                    ncorreo.Enviar_CorreoAContable(asunto1, cuerpo2, baseDatos);
                }
            //-----------------------historial
            NA_Historial nhistorial = new NA_Historial();
            nhistorial.insertar(codUser, "Ha insertado el Equipo del Proyecto " + nombreProyecto + " con el codigo " + codigoProyecto + " exboEquipo " + exbo + " codigoEquipo = " + codEquipoUltimoInsertado);
            //----------------- historial----      
            string baseDeDatos = Session["BaseDatos"].ToString();
                //NA_EnvioCorreo ncorreo = new NA_EnvioCorreo();
                string asunto = "Se ha Registrado el Edificio " + nombreProyecto;
                string Cuerpo = "Se ha registrado el Edificio " + nombreProyecto +
                                " con el exbo " + exbo + " <br/> <br/> " +
                                "Valor Compra = " + vc + " <br/>" +
                                "Valor CFR Transporte Maritimo = " + valorTransporteMaritimo + "<br/><br/><br/>";
                ncorreo.envioCorreoJanSfi(asunto, Cuerpo, baseDeDatos);            

           //------------------------------------------------------------------------------

            }else
                Response.Write("<script type='text/javascript'> alert('Equipo ya existen. Si desea actualizar los datos click en Modificar') </script>");

          }else
                Response.Write("<script type='text/javascript'> alert('El Nombre del Proyecto Esta Vacio') </script>");

        }

        private void modificar() 
        {
            NProyecto proyecto = new NProyecto();
            NEquipo equipo = new NEquipo();
            NA_Propietario encargadoPago = new NA_Propietario();
       
            string nombreProyecto = tx_Proyecto.Text;
            string exbo = txbExbo.Text;
            string ciudadInstalacion = ddlCiudad.SelectedValue;
            string ciudadVenta = dd_ciudadVenta.Text;
            string pasajero = txbPasajero.Text;            
            string modelo = txbModelo.Text;
            string velocidad = tx_velocidad.Text;
            float vc;
            float.TryParse(tx_vc.Text.Replace('.',','), out vc);
            string cliente = txbCliente.Text;
            float primerPago;
            float.TryParse(txbPagoContrato.Text.Replace('.', ',') , out primerPago);
            
            string fichero = tx_fichero.Text;
            string fecha = equipo.obtenerFechaActual();
           

            int parada;
            int.TryParse(txbParada.Text, out parada);

            string tipologia = pasajero + "/" + parada + "/" + modelo + "/" + velocidad;
 
            bool r110 = cbxr110.Checked;
            bool r148 = cbxr148.Checked;
            bool r106 = cbxr106.Checked;
            bool r107 = cbxr107.Checked;
            bool r109 = cbxr109.Checked;
            bool r113 = cbxr113.Checked;
            bool inventario = chbx_stock.Checked;
            bool ventaSistema = cbxprimerpago.Checked;
            bool ventaSistema2 = ChxventaContrato.Checked;
            string fechaContrato = convertidorFecha(tx_fechaContrato.Text);


            int indexAux = gvControlPedido.SelectedIndex;

            if (indexAux > -1 && (!equipo.existeEquipo(exbo) || exbo == "" || exbo == gvControlPedido.SelectedRow.Cells[3].Text))
            {
                int codigoEncargadoPago = -1;
                if (!encargadoPago.existe(cliente))
                {
                    encargadoPago.registrar1(cliente, 1);
                    codigoEncargadoPago = encargadoPago.obtenerUltimoCodigo();
                }
                else
                {
                    codigoEncargadoPago = encargadoPago.obtenerCodigoEncargadoPago(cliente);
                }
                // aki esta la clave
                int codigoEquipo = Convert.ToInt32(gvControlPedido.SelectedRow.Cells[18].Text);
                int codProyecto = equipo.getcodigoProyecto(codigoEquipo);

                string empresacontratoproyecto = dd_empresacontratoproyecto.SelectedItem.Text;
                if (!proyecto.existe(nombreProyecto))
                {
                   // proyecto.registrar1(nombreProyecto, nombreProyecto, fecha, -1, 1, ciudadInstalacion, codigoEncargadoPago);
                    proyecto.modificarProyecto1(codProyecto, nombreProyecto, fecha, 1, ciudadInstalacion, empresacontratoproyecto);
                   // codigoProyecto = proyecto.obtenerUltimoCodigo();
                }
                else
                {
                    codProyecto = proyecto.getCodigoProyect(nombreProyecto);
                    proyecto.modificarProyecto3(codProyecto, empresacontratoproyecto);
                }

                int codTipoEquipo = Convert.ToInt32(dd_tipoEquipo.SelectedValue);
                int codmarca = Convert.ToInt32(dd_marca.SelectedValue);

                float valorTransporteMaritimo = Convert.ToSingle(tx_valorTransporteMaritimo.Text.Replace('.', ','));
                string fechaAproxEmbarque = convertidorFecha(tx_fechaAproxEmbarque.Text);

                int estado = 1;
                string baseDatos = Session["BaseDatos"].ToString();
                if(baseDatos.Equals("Sucre")){
                    baseDatos = "Chuquisaca";
                }

                if(!ciudadInstalacion.Equals(baseDatos)){
                    estado = 0;
                }

                string fechapagoembarque = convertidorFecha(tx_fechaPagoEmbarque.Text);
                //----------------------------datos para el envio de correo --------------------
                DataSet tuplaAnterior = equipo.getequipoControlPedido(codigoEquipo);
                string VC_Anterior = tuplaAnterior.Tables[0].Rows[0][2].ToString();
                string valorcfrtransportemaritimo_Anterior = tuplaAnterior.Tables[0].Rows[0][3].ToString();
                if(!VC_Anterior.Equals(vc) || !valorcfrtransportemaritimo_Anterior.Equals(valorTransporteMaritimo)){
                    NA_EnvioCorreo ncorreo = new NA_EnvioCorreo();
                    string asunto = "Se Modifico los datos del Edificio " + nombreProyecto;
                    string Cuerpo = "Se Modifico los datos del Edificio " + nombreProyecto +
                                    " con el exbo " + exbo + " <br/> <br/> " +
                                    "Valor Compra = "+vc+" <br/>"+
                                    "Valor CFR Transporte Maritimo = " + valorTransporteMaritimo + "<br/><br/><br/>";
                    ncorreo.envioCorreoJanSfi(asunto, Cuerpo, baseDatos);
                }
                //------------------------------------------------------------------------------
                string equipoEntregadoSegunContrato = convertidorFecha(tx_equipoEntregadoSegunContrato.Text);
                string codigoContrato = tx_CodigoContrato.Text;
                bool contratoFirmado = cb_contratoFirmado.Checked;
                string consignatario = dd_consignatario.Text;
                string idAscensor = tx_idAscensor.Text;


                bool bandera = equipo.modificarEquipoControlPedido(codigoEquipo, exbo, tipologia, r110, r148, r106, r107, r109, r113, ventaSistema, primerPago, codProyecto, fichero, fechaContrato, ventaSistema2, modelo, parada, pasajero, velocidad, vc, codTipoEquipo, codmarca, ciudadVenta, ciudadInstalacion, fechaAproxEmbarque, valorTransporteMaritimo, inventario, estado, fechapagoembarque, equipoEntregadoSegunContrato, codigoContrato, consignatario, contratoFirmado, idAscensor, empresacontratoproyecto);
                proyecto.cambiarEncargadoPago1(codigoEncargadoPago, codProyecto);
                proyecto.cambiarDepartamento(codProyecto, ciudadInstalacion);
                
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);


              //-------------------dato nuevo cambio estadoAlbaro------------------------------
                int CodestadoEquipo = Convert.ToInt32(dd_Estado.SelectedValue);
                int codEstadoActual = equipo.getCodigoEstadoActual(codigoEquipo);

                //poder modificar el estado ( son diferentes los estados antes y actual)
              if (codEstadoActual != CodestadoEquipo && !equipo.estaPermitidoEstadoImportacion(CodestadoEquipo) && !equipo.estaPermitidoEstadoProyecto(CodestadoEquipo)
                  && !equipo.estaPermitidoEstadoImportacion(codEstadoActual) && !equipo.estaPermitidoEstadoProyecto(codEstadoActual))
              {
                  int codestadoEquipo = Convert.ToInt32(dd_Estado.SelectedValue);
                  NA_FechaEstadoEquipo fechaEstadoEquipo = new NA_FechaEstadoEquipo();
                  fechaEstadoEquipo.insertar(codigoEquipo, codestadoEquipo, codUser);
                  int codFechaEstadoEquipoUltimoInsertado = fechaEstadoEquipo.ultimoinsertado();
                  equipo.ModificarFechaEstadoEquipo(codigoEquipo, codFechaEstadoEquipoUltimoInsertado);  
                  //-------------------envio de correo -------------------
                  NA_EnvioCorreo ncorreo = new NA_EnvioCorreo();
                  if (CodestadoEquipo == 1)
                  {
                      string asunto1 = "Equipo Vendido exbo= " + exbo + " del Edificio = " + nombreProyecto + "  (" +baseDatos+ ")";
                      string cuerpo2 = "Se ha colocado el estado Vendido el equipo con Exbo " + exbo + " del Edificio " +
                          nombreProyecto + " de la UNE " + baseDatos;
                      ncorreo.Enviar_CorreoAContable(asunto1, cuerpo2, baseDatos);
                  }
                  //--------------------fin envio para frozen ----------
                  //-------------------historial --------------
                  NA_Historial nhistorial = new NA_Historial();              
                  nhistorial.insertar(codUser, "Modifico estado = "+dd_Estado.SelectedItem.Text+" en la vista FControlPedido.aspx un Equipo con exbo " + exbo);
                  //-------------------------------------------                
              }       
            //---------------------fin dato nuevo alvaro------------------------
                //----------------------dato nuevo----------------------------

               if(bandera == true){
                Response.Write("<script type='text/javascript'> alert('El Dato ha sido Modificado Correctamente') </script>");
                //-------------------- historial ------------------
                NA_Historial nhistorial1 = new NA_Historial();
                nhistorial1.insertar(codUser, "Ha Modificado Datos del Equipo del Proyecto " + nombreProyecto + " con el codigo " + codProyecto + " exboEquipo " + exbo + " codigoEquipo=" + codigoEquipo + " en la vista FControlPedido.aspx ");
                //------------------------------------------------
               }else
                   Response.Write("<script type='text/javascript'> alert('Error al Modificar') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('No ha seleccionado el Dato ha Modificar o el Equipo ya Existe') </script>");

            }
        
 
        protected void txbProyecto_TextChanged(object sender, EventArgs e)
        {
            Server.Transfer("../Presentacion/FProyecto.aspx", true);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            gvControlPedido.SelectedIndex = -1;
        }

        [WebMethod]
        [ScriptMethod]
        public static string[] getListaProyecto(string prefixText, int count)
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

        [WebMethod]
        [ScriptMethod]
        public static string[] getListaEquipo(string prefixText, int count)
        {
            string nombreEquipo = prefixText;

            NEquipo equipoN = new NEquipo();
            DataSet tuplas = equipoN.buscadorEquipo(nombreEquipo);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }
            return lista;
        }

        [WebMethod]
        [ScriptMethod]
        public static string[] getListaEncargadoPago(string prefixText, int count)
        {
            string nombreEncargado = prefixText;
            NA_Propietario encargadoPago = new NA_Propietario();
            DataSet tuplas = encargadoPago.buscador(nombreEncargado);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }
            return lista;
        }

        protected void btnRegistrarProyecto_Click(object sender, EventArgs e)
        {
            NProyecto proyecto = new NProyecto();
       
            if (!proyecto.existe(tx_Proyecto.Text))
            {
                string proy = tx_Proyecto.Text;
                Session.Add("proyectos", proy);
                Server.Transfer("../Presentacion/FProyecto.aspx", true);
            }
            else {
                Response.Write("<script type='text/javascript'> alert('ERROR : El Proyecto ya Existe') </script>");
            }
        }

         protected void btnModificar_Click(object sender, EventArgs e)
         {
             modificar();
             BuscarControl(tx_Proyecto.Text, txbExbo.Text, txbCliente.Text, txbPasajero.Text, txbParada.Text, txbModelo.Text, tx_velocidad.Text,"","",tx_fichero.Text);
             gvControlPedido.SelectedIndex = -1;
         }

         protected void gvControlPedido_SelectedIndexChanged(object sender, EventArgs e)
         {
             limpiar();
             cargarDatoSeleccionado();
         }

         private void cargarDatoSeleccionado() 
         {
             tx_Proyecto.Text = HttpUtility.HtmlDecode(gvControlPedido.SelectedRow.Cells[2].Text);


             if (gvControlPedido.SelectedRow.Cells[3].Text != "&nbsp;")
             {
                 txbExbo.Text = gvControlPedido.SelectedRow.Cells[3].Text;
             }
             else
             {
                 txbExbo.Text = "";
             }

             NEquipo equipo = new NEquipo();
             NEstadoEquipo estadoEquipo = new NEstadoEquipo();
             int codigoEstado = estadoEquipo.getCodigoEstadoEquipo(gvControlPedido.SelectedRow.Cells[4].Text);
             if(!equipo.estaPermitidoEstadoImportacion(codigoEstado) && !equipo.estaPermitidoEstadoProyecto(codigoEstado)){
             dd_Estado.SelectedValue = Convert.ToString(codigoEstado);
             }

             ddlCiudad.SelectedValue = gvControlPedido.SelectedRow.Cells[7].Text;
             
             if (gvControlPedido.SelectedRow.Cells[5].Text != "&nbsp;")
             {
                 txbCliente.Text = HttpUtility.HtmlDecode(gvControlPedido.SelectedRow.Cells[5].Text);
             }
             else
                 txbCliente.Text = "";
             
             
             if (gvControlPedido.SelectedRow.Cells[15].Text != "&nbsp;")
             {
                 txbPagoContrato.Text = gvControlPedido.SelectedRow.Cells[15].Text;
             }
             else
                 txbPagoContrato.Text = "";

             if (gvControlPedido.SelectedRow.Cells[16].Text != "&nbsp;")
             {
                 tx_fichero.Text = gvControlPedido.SelectedRow.Cells[16].Text;
             }
             else
                 tx_fichero.Text = "";

             if (gvControlPedido.SelectedRow.Cells[1].Text != "&nbsp;")
             {
                 tx_fechaContrato.Text = gvControlPedido.SelectedRow.Cells[1].Text;
             }
             else
                 tx_fechaContrato.Text = "";

             if (gvControlPedido.SelectedRow.Cells[19].Text != "&nbsp;")
             {
                 txbModelo.Text = gvControlPedido.SelectedRow.Cells[19].Text;
             }
             else
                 txbModelo.Text = "";

             if (gvControlPedido.SelectedRow.Cells[20].Text != "&nbsp;")
             {
                 txbParada.Text = gvControlPedido.SelectedRow.Cells[20].Text;
             }
             else
                 txbParada.Text = "";

             if (gvControlPedido.SelectedRow.Cells[21].Text != "&nbsp;")
             {
                 txbPasajero.Text = gvControlPedido.SelectedRow.Cells[21].Text;
             }
             else
                 txbPasajero.Text = "";

             if (gvControlPedido.SelectedRow.Cells[22].Text != "&nbsp;")
             {
                 tx_velocidad.Text = gvControlPedido.SelectedRow.Cells[22].Text;
             }
             else
                 tx_velocidad.Text = "";

             
            if (gvControlPedido.SelectedRow.Cells[23].Text != "&nbsp;")
             {
                tx_vc.Text = gvControlPedido.SelectedRow.Cells[23].Text;
             }
             else
                 tx_vc.Text = "";            

             if (gvControlPedido.SelectedRow.Cells[24].Text != "&nbsp;")
             {
                 NA_TipoEquipo ntipo = new NA_TipoEquipo();
                 string tipoEquipo = gvControlPedido.SelectedRow.Cells[24].Text;
                 int codtipo = ntipo.getCodigoTipoEquipo(tipoEquipo);
                 dd_tipoEquipo.SelectedValue = Convert.ToString(codtipo);
             }
             else
                 dd_tipoEquipo.SelectedIndex = -1;

             if (gvControlPedido.SelectedRow.Cells[25].Text != "&nbsp;")
             {
                 NA_Marca nmarca = new NA_Marca();
                 int codmarca = nmarca.getCodigoMarca(gvControlPedido.SelectedRow.Cells[25].Text);
                 dd_marca.SelectedValue = Convert.ToString(codmarca);
             }
             else
                dd_marca.SelectedIndex = -1;

             if (gvControlPedido.SelectedRow.Cells[26].Text != "&nbsp;")
             {
                 ddlCiudad.SelectedValue = gvControlPedido.SelectedRow.Cells[26].Text;
             }
             else
                 ddlCiudad.SelectedValue = "Ninguno";

             if (gvControlPedido.SelectedRow.Cells[27].Text != "&nbsp;")
             {
                 dd_ciudadVenta.SelectedValue = gvControlPedido.SelectedRow.Cells[27].Text;
             }
             else
                 dd_ciudadVenta.SelectedValue = "Ninguno";

             
             if (gvControlPedido.SelectedRow.Cells[28].Text != "&nbsp;")
             {
                 tx_valorTransporteMaritimo.Text = gvControlPedido.SelectedRow.Cells[28].Text;
             }
             else
                 tx_valorTransporteMaritimo.Text = "0";

             if (gvControlPedido.SelectedRow.Cells[29].Text != "&nbsp;")
             {
                 tx_fechaAproxEmbarque.Text = gvControlPedido.SelectedRow.Cells[29].Text;
             }
             else
                 tx_fechaAproxEmbarque.Text = "";

             
             if (gvControlPedido.SelectedRow.Cells[31].Text != "&nbsp;")
             {
                 tx_fechaPagoEmbarque.Text = gvControlPedido.SelectedRow.Cells[31].Text;
             }
             else
                 tx_fechaPagoEmbarque.Text = "";

             if (gvControlPedido.SelectedRow.Cells[32].Text != "&nbsp;")
             {
                 tx_equipoEntregadoSegunContrato.Text = gvControlPedido.SelectedRow.Cells[32].Text;
             }
             else
                 tx_equipoEntregadoSegunContrato.Text = "";
             

             if (gvControlPedido.SelectedRow.Cells[34].Text != "&nbsp;")
             {
                 tx_CodigoContrato.Text = gvControlPedido.SelectedRow.Cells[34].Text;
             }
             else
                 tx_CodigoContrato.Text = "";

             dd_consignatario.SelectedValue = gvControlPedido.SelectedRow.Cells[35].Text;



             if (gvControlPedido.SelectedRow.Cells[36].Text != "&nbsp;")
             {
                 tx_idAscensor.Text = gvControlPedido.SelectedRow.Cells[36].Text;
             }
             else
                 tx_idAscensor.Text = "";

             dd_empresacontratoproyecto.SelectedValue = gvControlPedido.SelectedRow.Cells[37].Text;

             cbxSeleccionado();
         }

         private void cbxSeleccionado() {
       
             NEquipo equipo = new NEquipo();
             int codigoEquipo = Convert.ToInt32(gvControlPedido.SelectedRow.Cells[18].Text);

             cbxr110.Checked = equipo.obtenerValor(codigoEquipo, 1);

             cbxr148.Checked = equipo.obtenerValor(codigoEquipo, 2);

             cbxr106.Checked = equipo.obtenerValor(codigoEquipo, 3);

             cbxr107.Checked = equipo.obtenerValor(codigoEquipo, 4);

             cbxr109.Checked = equipo.obtenerValor(codigoEquipo, 5);

             cbxr113.Checked = equipo.obtenerValor(codigoEquipo, 6);

             cbxprimerpago.Checked = equipo.obtenerValor(codigoEquipo, 7);

             ChxventaContrato.Checked = equipo.obtenerValor(codigoEquipo, 8);

             chbx_stock.Checked = equipo.obtenerValor(codigoEquipo, 9);

             cb_contratoFirmado.Checked = equipo.obtenerValor(codigoEquipo, 10);
         }

         protected void gvControl(object sender, GridViewDeleteEventArgs e)
         {
             NProyecto proyecto = new NProyecto();
             NEquipo equipo = new NEquipo();
             NA_Propietario encargadoPago = new NA_Propietario();
       
             int posicion = e.RowIndex;

             string codigo = gvControlPedido.DataKeys[posicion].Value.ToString();
             proyecto.eliminar(Convert.ToInt32(codigo));
             equipo.eliminar1(Convert.ToInt32(codigo));
             encargadoPago.eliminar(Convert.ToInt32(codigo));
             Response.Write("<script type='text/javascript'> alert('Se ha eliminado correctamente') </script>");
             BuscarControl(tx_Proyecto.Text, txbExbo.Text, txbCliente.Text, txbPasajero.Text, txbParada.Text, txbModelo.Text, tx_velocidad.Text, "" ,"",tx_fichero.Text);
         }

        private void limpiar()
        {
            tx_Proyecto.Text = "";
            txbExbo.Text = "";
            txbModelo.Text = "";
            txbParada.Text = "";
            txbPasajero.Text = "";
            tx_velocidad.Text = "";
            txbPagoContrato.Text = "0";
            txbCliente.Text = "";
            ddlCiudad.SelectedValue = "-1";
            cbxr106.Checked = false;
            cbxr107.Checked = false;
            cbxr109.Checked = false;
            cbxr110.Checked = false;
            cbxr113.Checked = false;
            cbxr148.Checked = false;
            chbx_stock.Checked = false;
            cbxprimerpago.Checked = false;
            ChxventaContrato.Checked = false;
            cb_contratoFirmado.Checked = false;
            tx_fechaContrato.Text = "";
            tx_fichero.Text = "";
            tx_FechaDesde.Text = "";
            tx_fechaHasta.Text = "";
            tx_vc.Text = "";
            dd_marca.SelectedValue = "-1";
            dd_tipoEquipo.SelectedValue = "-1";
          //  dd_Estado.SelectedValue = "1";
            dd_ciudadVenta.SelectedValue = "-1";
            ddlCiudad.SelectedValue = "-1";
            tx_valorTransporteMaritimo.Text = "0";
            tx_fechaAproxEmbarque.Text = "";
            tx_equipoEntregadoSegunContrato.Text = "";
            tx_CodigoContrato.Text = "";
            dd_empresacontratoproyecto.SelectedValue = "-1";
        }


        public void BuscarControl(string nombreProyecto, string exbo, string nombrePropietario, string pasajero, string parada, string modelo, string velocidad, string fechaDesde, string fechaHasta, string fichero)
        {      
                NEquipo equipo1 = new NEquipo();
                string fechadesdeAux = convertidorFecha(fechaDesde);
                string fechahastaAux = convertidorFecha(fechaHasta);
                DataSet tuplaRes = equipo1.BuscarControlEquipos2(nombreProyecto, exbo, nombrePropietario, pasajero, parada, modelo, velocidad,fechadesdeAux,fechahastaAux,fichero);
                gvControlPedido.DataSource = tuplaRes;
                gvControlPedido.DataBind();          
                lb_cantidad.Text = gvControlPedido.Rows.Count.ToString();
                gvControlPedido.SelectedIndex = -1;
        }
        
        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string nombreProyecto = tx_Proyecto.Text;
            string exbo = txbExbo.Text;
            string fichero = tx_fichero.Text;
            BuscarControl(tx_Proyecto.Text,txbExbo.Text,txbCliente.Text,txbPasajero.Text,txbParada.Text,txbModelo.Text,tx_velocidad.Text, tx_FechaDesde.Text, tx_fechaHasta.Text,fichero);
        }

        protected void bt_Eliminar_Click(object sender, EventArgs e)
        {
            Eliminar_Procedimiento();
            gvControlPedido.SelectedIndex = -1;
        }

        private void Eliminar_Procedimiento()
        {
            NProyecto proyecto = new NProyecto();
            NEquipo equipo = new NEquipo();
            NA_Propietario encargadoPago = new NA_Propietario();
            string Exbo = gvControlPedido.SelectedRow.Cells[3].Text;
            int codigoExbo = Convert.ToInt32(equipo.getEquipo2(Exbo).Tables[0].Rows[0][0].ToString());
            string nombreproyecto = gvControlPedido.SelectedRow.Cells[2].Text;
            if (equipo.eliminar1(codigoExbo)) {

                NA_Historial nhistorial = new NA_Historial();                
                //int codUser = Convert.ToInt32(Session["coduser"].ToString());
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                nhistorial.insertar(codUser, "Ha eliminado el Equipo " + codigoExbo+" con exbo= "+Exbo+", proyecto = "+nombreproyecto);
                Response.Write("<script type='text/javascript'> alert('Se ha eliminado correctamente') </script>");            

            }else
                Response.Write("<script type='text/javascript'> alert('NO se ha eliminado, el equipo esta en mantenimiento ') </script>");


            BuscarControl(tx_Proyecto.Text, txbExbo.Text, txbCliente.Text, txbPasajero.Text, txbParada.Text, txbModelo.Text, tx_velocidad.Text,"","",tx_fichero.Text);
        }

        protected void bt_Exportar_Click(object sender, EventArgs e)
        {
            NEquipo equipo1 = new NEquipo();
            DataSet tuplaRes = equipo1.BuscarControlEquipos2(tx_Proyecto.Text, txbExbo.Text, txbCliente.Text, txbPasajero.Text, txbParada.Text, txbModelo.Text, tx_velocidad.Text,convertidorFecha(tx_FechaDesde.Text),convertidorFecha(tx_fechaHasta.Text),tx_fichero.Text); 

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombreDB = Session["BaseDatos"].ToString();
            string nombre = "Control de Pedido "+nombreDB;
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = tuplaRes;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }

        }



        protected void bt_Exportar_ListaMaestra_Click(object sender, EventArgs e)
        {
            NEquipo equipo1 = new NEquipo();
            DataSet tuplaRes = equipo1.getListaMaestraEquipos();

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombreDB = Session["BaseDatos"].ToString();
            string nombre = "Lista Maestra " + nombreDB;
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = tuplaRes;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            bt_Exportar_ListaMaestra_Click(sender, e);
        }

       
      
    }
}