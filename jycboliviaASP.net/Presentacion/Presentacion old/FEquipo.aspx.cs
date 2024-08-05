using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Data.SqlTypes;
using System.Web.Script.Services;
using System.Web.Services;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Globalization;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FEquipo : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
              if (tienePermisoDeIngreso(9)==false)
                {
                    string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                    Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
                }        
            
            if(!IsPostBack)
            {
                listar1("","",-1,"");               
                cargarddlEstadoEquipo();                
                //cargarddlTecnicoInstalador();
                cargarddTipoEquipo();
                cargarddMarca();
             //   cargardfiscalesProyectos();               
            }
          //  habilitar();
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
        


        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(permiso, codUser);
        }


        private void limpiar()
        {
            txtExbo.Text = "";
            tx_FechaActaDefinitiva.Text = "";
            txtFechaActaProvisional.Text = "";
            txtFechaActaTecnica.Text = "";
            tx_FechaEquipoEntregado.Text = "";
            txtFechaEquipoObra.Text = "";
            tx_fechalimiteAprobacionPlanosFabrica.Text = "";
            tx_modelo.Text = "";
            tx_parada.Text = "";
            tx_pasajero.Text = "";
            tx_velocidad.Text = "";
            dd_Marca.SelectedValue = "-1";
            dd_tipoEquipo.SelectedValue = "-1";          
           // ddlTecnicoInstalador.SelectedValue = "-1";
           // dd_FiscalProy.SelectedValue = "-1";
            ddlEstadoEquipo.SelectedIndex = -1;
            tx_NombreProyecto.Text = "";
            tx_fechaAproxEmbarque.Text = "";
            tx_fechaPagoEmbarque.Text = "";
            tx_fechaConfirmacionPagoEmbarque.Text = "";
            tx_CodCli_simec.Text = "";

            tx_tecInstalador.Text = "";
            tx_fiscalProyecto.Text = "";
            tx_rin.Text = "";
            tx_rcc.Text = "";
            tx_tecmantenimiento.Text = "";
            tx_supervisor.Text = "";
            tx_variableSimec.Text = "";
            tx_identificacionAscensor.Text = "";
        }
   
        private void habilitar()
        {
            txtExbo.Enabled = true;
            tx_FechaActaDefinitiva.Enabled = true;
            txtFechaActaProvisional.Enabled = true;
            txtFechaActaTecnica.Enabled = true;                               
            tx_FechaEquipoEntregado.Enabled = true;
            ddlEstadoEquipo.Enabled = true;    
            tx_modelo.Enabled = true;
            tx_parada.Enabled = true;
            tx_pasajero.Enabled = true;
            tx_velocidad.Enabled = true;
            txtFechaEquipoObra.Enabled = true;
            tx_NombreProyecto.Enabled = true;
            tx_aprobacionPlano.Enabled = true;
            tx_fechalimiteAprobacionPlanosFabrica.Enabled = true;
            tx_entregaCliente.Enabled = true;
            tx_habilitacionEquipo.Enabled = true;
            tx_fechaConfirmacionPagoEmbarque.Enabled = true;
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

      /*  private void cargardfiscalesProyectos()
        {
            NA_Responsables NResponsables = new NA_Responsables();
            dd_FiscalProy.DataSource = NResponsables.get_AllFiscalesProy();
            dd_FiscalProy.DataValueField = "codigo";
            dd_FiscalProy.DataTextField = "nombre";
            dd_FiscalProy.Items.Add(new ListItem("--Ninguno--", "-1"));
            dd_FiscalProy.AppendDataBoundItems = true;
            dd_FiscalProy.SelectedIndex = -1;
            dd_FiscalProy.DataBind();
        }  */

        private void cargarddMarca()
        {
            NA_Marca Nmarca = new NA_Marca();
            dd_Marca.DataSource = Nmarca.mostrarAllDatos();
            dd_Marca.DataValueField = "codigo";
            dd_Marca.DataTextField = "nombre";
            dd_Marca.Items.Add(new ListItem("--Ninguno--", "-1"));
            dd_Marca.AppendDataBoundItems = true;
            dd_Marca.SelectedIndex = -1;
            dd_Marca.DataBind();
        }

        
    
        private void cargarddlEstadoEquipo()
        {
            NEstadoEquipo estadoEquipo = new NEstadoEquipo();      
            
            ddlEstadoEquipo.DataSource = estadoEquipo.listar();
            ddlEstadoEquipo.DataValueField = "codigo";
            ddlEstadoEquipo.DataTextField = "nombre";
            ddlEstadoEquipo.Items.Add(new ListItem("", "-1"));
            ddlEstadoEquipo.AppendDataBoundItems = true;
            ddlEstadoEquipo.SelectedIndex = -1;
            ddlEstadoEquipo.DataBind();
        }

        /*
        private void cargarddlTecnicoInstalador()
        {
            NEquipo equipo = new NEquipo();
      
            ddlTecnicoInstalador.DataSource = equipo.listaTecnicoInstalador();
            ddlTecnicoInstalador.DataValueField = "codigo";
            ddlTecnicoInstalador.DataTextField = "nombre";
            ddlTecnicoInstalador.Items.Add(new ListItem("--Ninguno--", "-1"));
            ddlTecnicoInstalador.AppendDataBoundItems = true;
            ddlTecnicoInstalador.SelectedIndex = -1;
            ddlTecnicoInstalador.DataBind();
        }
        */
       

        private void listar1(string edificio, string exbo, int fiscalProyecto, string nombreEstado)
        {
            NEquipo equipo = new NEquipo();
            if (fiscalProyecto == -1){
                DataSet lista = equipo.listarEquipo2(exbo,edificio,nombreEstado, false);
                int cantidad = equipo.cantiListaEquipo2(exbo, edificio, nombreEstado);
                GridView1.DataSource = lista;
                GridView1.DataBind();
                tx_cantidadEquipos.Text = cantidad.ToString();
            }
            else {
                DataSet lista = equipo.listarEquipo2ConFiscalProyecto(exbo,edificio,fiscalProyecto,nombreEstado,false);
                int cantidad = equipo.cantiListaEquipo2(exbo, edificio, nombreEstado);
                GridView1.DataSource = lista;
                GridView1.DataBind();
                tx_cantidadEquipos.Text = cantidad.ToString();
            }

        }


        private void cargarDatosSeleccion()
        {
            NEquipo equipo = new NEquipo();
            NProyecto proyecto = new NProyecto();
            NActualizacion actualizacion = new NActualizacion();
            NEstadoEquipo estadoEquipo = new NEstadoEquipo();
      
            int codEquipo = Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);
            NEquipo Neq = new NEquipo();
            DataSet tuplaSelec = Neq.getEquipo(codEquipo);

            txtExbo.Text = tuplaSelec.Tables[0].Rows[0][1].ToString();
           // tx_tipologia.Text = tuplaSelec.Tables[0].Rows[0][2].ToString();

            if (tuplaSelec.Tables[0].Rows[0][4].ToString() == "&nbsp;" || tuplaSelec.Tables[0].Rows[0][4].ToString() == "")
            {
                txtFechaActaProvisional.Text = "";
                txtFechaActaProvisional.Enabled = true;
            }
            else {
                txtFechaActaProvisional.Text = tuplaSelec.Tables[0].Rows[0][4].ToString();
                txtFechaActaProvisional.Enabled = false;
            }

            if (tuplaSelec.Tables[0].Rows[0][5].ToString() == "&nbsp;" || tuplaSelec.Tables[0].Rows[0][5].ToString() == "")
            {
                txtFechaActaTecnica.Text = "";
                txtFechaActaTecnica.Enabled = true;
            }
            else
            {
                txtFechaActaTecnica.Text = tuplaSelec.Tables[0].Rows[0][5].ToString();
                txtFechaActaTecnica.Enabled = false;
            }

            if (tuplaSelec.Tables[0].Rows[0][6].ToString() == "&nbsp;" || tuplaSelec.Tables[0].Rows[0][6].ToString() == "")
            {
                tx_FechaActaDefinitiva.Text = "";
                tx_FechaActaDefinitiva.Enabled = true;
            }
            else {
                tx_FechaActaDefinitiva.Text = tuplaSelec.Tables[0].Rows[0][6].ToString();
                tx_FechaActaDefinitiva.Enabled = false;
            }

            if (tuplaSelec.Tables[0].Rows[0][7].ToString() == "&nbsp;" || tuplaSelec.Tables[0].Rows[0][7].ToString() == "")
            {
                txtFechaEquipoObra.Text = "";
                txtFechaEquipoObra.Enabled = true;
            }
            else
            {
                txtFechaEquipoObra.Text = tuplaSelec.Tables[0].Rows[0][7].ToString();
                txtFechaEquipoObra.Enabled = false;
            }

            if (tuplaSelec.Tables[0].Rows[0][8].ToString() == "&nbsp;" || tuplaSelec.Tables[0].Rows[0][8].ToString() == "")
            {
                tx_FechaEquipoEntregado.Text = "";
                tx_FechaEquipoEntregado.Enabled = true;
            }
            else
            {
                tx_FechaEquipoEntregado.Text = tuplaSelec.Tables[0].Rows[0][8].ToString();
                tx_FechaEquipoEntregado.Enabled = false;
            }


            if (tuplaSelec.Tables[0].Rows[0][9].ToString() == "")
            {
               ddlEstadoEquipo.SelectedValue = "-1";
            }
            else
            {                
                int codigoEstadoEquipo = Convert.ToInt32(tuplaSelec.Tables[0].Rows[0][9].ToString());
                ddlEstadoEquipo.SelectedValue = Convert.ToString(codigoEstadoEquipo);
            }

                             
            int codigoProyecto = Convert.ToInt32(tuplaSelec.Tables[0].Rows[0][11].ToString());
            DataSet tuplaProyecto = proyecto.getProyect(codigoProyecto);
            tx_NombreProyecto.Text = HttpUtility.HtmlDecode(tuplaProyecto.Tables[0].Rows[0][1].ToString());

            if (tuplaSelec.Tables[0].Rows[0][12].ToString() == "")
            {
                dd_tipoEquipo.SelectedValue = "-1";
            }
            else
            {
                dd_tipoEquipo.SelectedValue = tuplaSelec.Tables[0].Rows[0][12].ToString();
            }

            if (tuplaSelec.Tables[0].Rows[0][13].ToString() == "")
            {
                dd_Marca.SelectedValue = "-1";
            }
            else
            {
                dd_Marca.SelectedValue = tuplaSelec.Tables[0].Rows[0][13].ToString();
            }

              NA_Responsables nres = new NA_Responsables();

       /*     if (tuplaSelec.Tables[0].Rows[0][14].ToString() == "")
            {
                dd_FiscalProy.SelectedValue = "-1";
            }
            else
            {
                dd_FiscalProy.SelectedValue = tuplaSelec.Tables[0].Rows[0][14].ToString();
            }
            */

            if (tuplaSelec.Tables[0].Rows[0][14].ToString() == "")
            {
                tx_fiscalProyecto.Text = "";
            }
            else
            {
                int codigo = int.Parse(tuplaSelec.Tables[0].Rows[0][14].ToString());
                DataSet tupla = nres.get_responsable(codigo);
                if(tupla.Tables[0].Rows.Count > 0){
                    tx_fiscalProyecto.Text = HttpUtility.HtmlDecode(tupla.Tables[0].Rows[0][1].ToString());
                }
            }

            if (tuplaSelec.Tables[0].Rows[0][15].ToString() == "&nbsp;" || tuplaSelec.Tables[0].Rows[0][15].ToString() == "")
            {
                tx_fechalimiteAprobacionPlanosFabrica.Text = "";
                tx_fechalimiteAprobacionPlanosFabrica.Enabled = true;
            }
            else
            {
                tx_fechalimiteAprobacionPlanosFabrica.Text = tuplaSelec.Tables[0].Rows[0][15].ToString();
             //   tx_fechalimiteAprobacionPlanosFabrica.Enabled = false;
            }

            if (tuplaSelec.Tables[0].Rows[0][16].ToString() == "&nbsp;")
            {
                tx_modelo.Text = "";
            }
            else
            {
                tx_modelo.Text = tuplaSelec.Tables[0].Rows[0][16].ToString();
            }

            if (tuplaSelec.Tables[0].Rows[0][17].ToString() == "&nbsp;")
            {
                tx_pasajero.Text = "";
            }
            else
            {
                tx_pasajero.Text = tuplaSelec.Tables[0].Rows[0][17].ToString();
            }

            if (tuplaSelec.Tables[0].Rows[0][18].ToString() == "&nbsp;")
            {
                tx_parada.Text = "";
            }
            else
            {
                tx_parada.Text = tuplaSelec.Tables[0].Rows[0][18].ToString();
            }

            if (tuplaSelec.Tables[0].Rows[0][19].ToString() == "&nbsp;")
            {
                tx_velocidad.Text = "";
            }
            else
            {
                tx_velocidad.Text = tuplaSelec.Tables[0].Rows[0][19].ToString();
            }

            if (tuplaSelec.Tables[0].Rows[0][20].ToString() == "")
            {
                tx_aprobacionPlano.Text = "";
                tx_aprobacionPlano.Enabled = true;
            }
            else
            {
                tx_aprobacionPlano.Text = tuplaSelec.Tables[0].Rows[0][20].ToString();
                tx_aprobacionPlano.Enabled = false;
            }

            if (tuplaSelec.Tables[0].Rows[0][21].ToString() == "")
            {
                tx_entregaCliente.Text = "";
                tx_entregaCliente.Enabled = true;
            }
            else
            {
                tx_entregaCliente.Text = tuplaSelec.Tables[0].Rows[0][21].ToString();
                tx_entregaCliente.Enabled = false;
            }


            if (tuplaSelec.Tables[0].Rows[0][22].ToString() == "")
            {
                tx_habilitacionEquipo.Text = "";
                tx_habilitacionEquipo.Enabled = true;
            }
            else
            {
                tx_habilitacionEquipo.Text = tuplaSelec.Tables[0].Rows[0][22].ToString();
                tx_habilitacionEquipo.Enabled = false;
            }

            if (tuplaSelec.Tables[0].Rows[0][23].ToString() == "")
            {
                tx_fechaAproxEmbarque.Text = "";
                tx_fechaAproxEmbarque.Enabled = true;
            }
            else
            {
                tx_fechaAproxEmbarque.Text = tuplaSelec.Tables[0].Rows[0][23].ToString();
            //    tx_fechaAproxEmbarque.Enabled = false;
            }


            if (tuplaSelec.Tables[0].Rows[0][24].ToString() == "")
            {
                tx_fechaPagoEmbarque.Text = "";
                tx_fechaPagoEmbarque.Enabled = true;
            }
            else
            {
                tx_fechaPagoEmbarque.Text = tuplaSelec.Tables[0].Rows[0][24].ToString();
              //  tx_fechaPagoEmbarque.Enabled = false;
            }

            if (tuplaSelec.Tables[0].Rows[0][25].ToString() == "")
            {
                tx_fechaConfirmacionPagoEmbarque.Text = "";
                tx_fechaConfirmacionPagoEmbarque.Enabled = true;
            }
            else
            {
                tx_fechaConfirmacionPagoEmbarque.Text = tuplaSelec.Tables[0].Rows[0][25].ToString();
             //   tx_fechaConfirmacionPagoEmbarque.Enabled = false;
            }


            if (tuplaSelec.Tables[0].Rows[0][26].ToString() == "")
            {
                tx_CodCli_simec.Text = "";               
            }
            else
            {
                tx_CodCli_simec.Text = tuplaSelec.Tables[0].Rows[0][26].ToString();               
            }

            if (tuplaSelec.Tables[0].Rows[0][27].ToString() == "")
            {
                dd_monedaSimec.SelectedValue = "0";
            }
            else
            {
                dd_monedaSimec.SelectedValue = tuplaSelec.Tables[0].Rows[0][27].ToString();
            }

            if (tuplaSelec.Tables[0].Rows[0][28].ToString() == "")
            {
                tx_variableSimec.Text = "";
            }
            else
            {
                tx_variableSimec.Text = tuplaSelec.Tables[0].Rows[0][28].ToString();
            }

            if (tuplaSelec.Tables[0].Rows[0][29].ToString() == "")
            {
                tx_identificacionAscensor.Text = "";
            }
            else
            {
                tx_identificacionAscensor.Text = tuplaSelec.Tables[0].Rows[0][29].ToString();
            }

            if (GridView1.SelectedRow.Cells[26].Text == "&nbsp;" || GridView1.SelectedRow.Cells[26].Text == "")
            {
                tx_rin.Text = "";
            }
            else
            {
                tx_rin.Text = HttpUtility.HtmlDecode(GridView1.SelectedRow.Cells[26].Text);
            }

            if (GridView1.SelectedRow.Cells[27].Text == "&nbsp;" || GridView1.SelectedRow.Cells[27].Text == "")
            {
                tx_rcc.Text = "";
            }
            else
            {
                tx_rcc.Text = HttpUtility.HtmlDecode(GridView1.SelectedRow.Cells[27].Text);
            }

            if (GridView1.SelectedRow.Cells[28].Text == "&nbsp;" || GridView1.SelectedRow.Cells[28].Text == "")
            {
                tx_tecmantenimiento.Text = "";
            }
            else
            {
                tx_tecmantenimiento.Text = HttpUtility.HtmlDecode(GridView1.SelectedRow.Cells[28].Text);
            }

            if (GridView1.SelectedRow.Cells[29].Text == "&nbsp;" || GridView1.SelectedRow.Cells[29].Text == "")
            {
                tx_supervisor.Text = "";
            }
            else
            {
                tx_supervisor.Text = HttpUtility.HtmlDecode(GridView1.SelectedRow.Cells[29].Text);
            }
            


            NA_DetalleTecnicoAsignado Ndta = new NA_DetalleTecnicoAsignado();
            DataSet TuplaResponsable = Ndta.getDetalleTecnicoAsignado(codEquipo, 3);          
            
            if (TuplaResponsable.Tables[0].Rows.Count > 0)
            {
                string codtecnicoInstalador = TuplaResponsable.Tables[0].Rows[0][1].ToString();
                int codigo = int.Parse(codtecnicoInstalador);
                DataSet tuplaData = nres.get_responsable(codigo);
                tx_tecInstalador.Text = HttpUtility.HtmlDecode(tuplaData.Tables[0].Rows[0][1].ToString());
            }
            

        }

        private void registrar() 
        {
            NEquipo equipo = new NEquipo();
            int CodestadoEquipo = Convert.ToInt32(ddlEstadoEquipo.SelectedValue);
            
            if(equipo.estaPermitidoEstadoProyecto(CodestadoEquipo)){
                string exbo = txtExbo.Text;            
            NProyecto proyectoN = new NProyecto();            
            int CodProy = proyectoN.getCodigoProyect(tx_NombreProyecto.Text);

            string tipologia = tx_modelo.Text + "/" + tx_pasajero.Text + "/" + tx_parada.Text + "/" + tx_velocidad.Text;
            int codtipoEquipo = Convert.ToInt32(dd_tipoEquipo.SelectedValue);
            int codmarca = Convert.ToInt32(dd_Marca.SelectedValue);

            string fecha = equipo.obtenerFechaActual();
            string fechaActaProvisional = equipo.aFecha(txtFechaActaProvisional.Text);
            string fechaActaTecnica = equipo.aFecha(txtFechaActaTecnica.Text);
            string fechaActaDefinitiva = equipo.aFecha(tx_FechaActaDefinitiva.Text);
            string fechaEquipoObra = equipo.aFecha(txtFechaEquipoObra.Text);
            string fechaEquipoEntregado = equipo.aFecha(tx_FechaEquipoEntregado.Text);
            string fechaLimiteAprobacionFabrica = equipo.aFecha(tx_fechalimiteAprobacionPlanosFabrica.Text);

            NA_Responsables nresp = new NA_Responsables();
            int codtecnicoInstalador = nresp.getCodigo_NombreResponsable(tx_tecInstalador.Text);
            string codFiscalProy = nresp.getCodigo_NombreResponsable(tx_fiscalProyecto.Text).ToString();

            string fechaAprobacionPlano = equipo.aFecha(tx_aprobacionPlano.Text);
            string fechaEntregaCliente = equipo.aFecha(tx_entregaCliente.Text);
            string fechaHabilitacionEquipo = equipo.aFecha(tx_habilitacionEquipo.Text);
            string fechaAproxEmbarque = equipo.aFecha(tx_fechaAproxEmbarque.Text);
            string fechaPagoEmbarque = equipo.aFecha(tx_fechaPagoEmbarque.Text);

            if(codFiscalProy == "-1"){
                codFiscalProy = "null";
            }

            string modelo = tx_modelo.Text;
            string pasajero = tx_pasajero.Text;
            int parada = 0;
                if(!tx_parada.Text.Equals("")){
                    parada = Convert.ToInt32(tx_parada.Text);
                }
                string velocidad = tx_velocidad.Text;


            if (!equipo.existeEquipo(exbo))
            {
                equipo.registrar(exbo, fecha, fechaActaProvisional, fechaActaTecnica, fechaActaDefinitiva, 1, CodProy, fechaEquipoObra, fechaEquipoEntregado, tipologia, codtipoEquipo, codmarca, codFiscalProy, fechaLimiteAprobacionFabrica, modelo, pasajero, parada, velocidad, fechaAprobacionPlano, fechaEntregaCliente, fechaHabilitacionEquipo,fechaAproxEmbarque,fechaPagoEmbarque );
                int codEquipoUltimoInsertado = equipo.ultimoinsertado();

               // int codUser = Convert.ToInt32(Session["coduser"].ToString());
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                NA_FechaEstadoEquipo fechaEstadoEquipo = new NA_FechaEstadoEquipo();
                fechaEstadoEquipo.insertar(codEquipoUltimoInsertado,CodestadoEquipo,codUser);
                int codFechaEstadoEquipoUltimoInsertado = fechaEstadoEquipo.ultimoinsertado();

                equipo.ModificarFechaEstadoEquipo(codEquipoUltimoInsertado,codFechaEstadoEquipoUltimoInsertado);

                int codEquipoInsertado = equipo.ultimoinsertado();
                NA_DetalleTecnicoAsignado Ndta = new NA_DetalleTecnicoAsignado();
                
                if (codtecnicoInstalador != 1)
                {
                    Ndta.insertar(codEquipoInsertado, codtecnicoInstalador, 3);
                }
                //--------------- historial -----------------
                NA_Historial nhistorial = new NA_Historial();
                nhistorial.insertar(codUser, "Se ha registrado un Equipo con exbo " + exbo);
                //----------------------------------------
                Response.Write("<script type='text/javascript'> alert('Se ha registrado correctamente') </script>");
            }
            else {
                Response.Write("<script type='text/javascript'> alert('ERROR : El Equipo YA Existe') </script>");
            }
            }else
                Response.Write("<script type='text/javascript'> alert('ERROR : No puede instroducir este Estado (Solo Importacion)') </script>");
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


        public string aumentarFecha_conMes(string fecha, int meses)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha, new CultureInfo("es-ES"));
                fecha_ = fecha_.AddMonths(meses);
                string _fecha = fecha_.ToString("yyyy/MM/dd");
                return "'" + _fecha + "'";
            }
            else
                return "null";
        }


        private void insertarSeguimientoPorDefecto(int diaInicio, int mesInicio, int year, int codEquipo, string fechaInicialGratuita)
        {
            DateTime fechaActual = DateTime.Today;
            int yearActual = fechaActual.Year;
            for (int i = year; i <= yearActual; i++ )
            {
                insertarSeguimientoPorDefecto_porGestion(diaInicio, mesInicio, i, codEquipo, fechaInicialGratuita);
                diaInicio = 1;
                mesInicio = 1;
            }
            if(year > yearActual){
                insertarSeguimientoPorDefecto_porGestion_Vacio(yearActual, codEquipo, fechaInicialGratuita);
            }
        }


        private void insertarSeguimientoPorDefecto_porGestion(int diaInicio,int mesInicio,int year,int codEquipo, string fechaInicioGratuito)
        {            
            string horaCobro = "'10:00:00'";
            int mesesGratuitos = 3;            
            string fechaCobroPlanificado = "null";
            int codPlanPago = 2;
            string lugarPago = "";
            string fechaContrato = "null";
            string mesInicial = fechaInicioGratuito ;
            string mesFinal = "null";
            string detalle = "Seg. Automatico";
            int codigoEquipoExbo = codEquipo;
            NA_Seguimiento Nsegui = new NA_Seguimiento();
            // si no tiene seguimiento se crea
            if (Nsegui.getCodigoSeguimiento(codigoEquipoExbo, year) == -1)
            {
                //-------------------------------------------------
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                //----------------------------------------------------------

                bool bandera = Nsegui.insertar(detalle, horaCobro, fechaCobroPlanificado, lugarPago, fechaContrato, mesesGratuitos, mesInicial, mesFinal, codPlanPago, codigoEquipoExbo, year);
                int codultimoInsertado = Nsegui.ultimoinsertado();
                int codestadoMan = 1;
                NA_FechaEstadoMan NfechaEstadoMan = new NA_FechaEstadoMan();
                NfechaEstadoMan.insertar(codultimoInsertado, codestadoMan, codUser);
                int ultimaFechaEstadoMan_insertada = NfechaEstadoMan.ultimoinsertado();
                Nsegui.modificarFechaEstadoMan(codultimoInsertado, ultimaFechaEstadoMan_insertada);

                //----------------------------------------------
                NA_Historial nhistorial = new NA_Historial();
               // int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha Insertado un nuevo Seguimiento con codigo " + codultimoInsertado);
                //---------------------------------------
                NA_DetalleSeguimiento NdetSeguimiento = new NA_DetalleSeguimiento();             
                
                if(diaInicio >= 21){
                    mesInicio = mesInicio + 1;
                }

                if (codultimoInsertado > -1)
                {
                
                for (int i = 1; i <= 12; i++)
                    {
                        if (i >= mesInicio)
                        {
                            if(i==mesInicio && diaInicio > 15 && diaInicio < 21){
                                NdetSeguimiento.insertar(codultimoInsertado, i, 50, 0, "No", codUser);
                            }else
                                NdetSeguimiento.insertar(codultimoInsertado, i, 100, 0, "No", codUser);

                        }
                        else
                            NdetSeguimiento.insertar(codultimoInsertado, i, 0, 0, "No", codUser);
                    }                
                }

              //  Response.Write("<script type='text/javascript'> alert(' El dato ha sido insertado Correctamente') </script>");
            }
            else
            {
             //   Response.Write("<script type='text/javascript'> alert(' La Prevision ya Existe ') </script>");
            }

        }


        private void insertarSeguimientoPorDefecto_porGestion_Vacio(int year, int codEquipo, string fechaInicioGratuito)
        {
            string horaCobro = "'10:00:00'";
            int mesesGratuitos = 3;
            string fechaCobroPlanificado = "null";
            int codPlanPago = 2;
            string lugarPago = "";
            string fechaContrato = "null";
            string mesInicial = fechaInicioGratuito;
            string mesFinal = "null";
            string detalle = "Seg. Automatico";
            int codigoEquipoExbo = codEquipo;
            NA_Seguimiento Nsegui = new NA_Seguimiento();
            // si no tiene seguimiento se crea
            if (Nsegui.getCodigoSeguimiento(codigoEquipoExbo, year) == -1)
            {
                //-------------------------------------------------
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                //----------------------------------------------------------

                bool bandera = Nsegui.insertar(detalle, horaCobro, fechaCobroPlanificado, lugarPago, fechaContrato, mesesGratuitos, mesInicial, mesFinal, codPlanPago, codigoEquipoExbo, year);
                int codultimoInsertado = Nsegui.ultimoinsertado();
                int codestadoMan = 1;
                NA_FechaEstadoMan NfechaEstadoMan = new NA_FechaEstadoMan();
                NfechaEstadoMan.insertar(codultimoInsertado, codestadoMan, codUser);
                int ultimaFechaEstadoMan_insertada = NfechaEstadoMan.ultimoinsertado();
                Nsegui.modificarFechaEstadoMan(codultimoInsertado, ultimaFechaEstadoMan_insertada);

                //----------------------------------------------
                NA_Historial nhistorial = new NA_Historial();
                // int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha Insertado un nuevo Seguimiento con codigo " + codultimoInsertado);
                //---------------------------------------
                NA_DetalleSeguimiento NdetSeguimiento = new NA_DetalleSeguimiento();
                if (codultimoInsertado > -1)
                {
                    for (int i = 1; i <= 12; i++)
                    {   
                       NdetSeguimiento.insertar(codultimoInsertado, i, 0, 0, "Si", codUser);                                             
                    }
                }               
            }
        }



        private void modificar()
        {
            NEquipo equipo = new NEquipo();
            int CodestadoEquipo = Convert.ToInt32(ddlEstadoEquipo.SelectedValue);
            int codigoEquipo = Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);
            int codEstadoActual = equipo.getCodigoEstadoActual(codigoEquipo);
            
          //------------------------------------------------              
              NProyecto proyecto = new NProyecto();
              string exbo = txtExbo.Text;
              string tipologia = tx_modelo.Text+"/"+tx_pasajero.Text+"/"+tx_parada.Text+"/"+tx_velocidad.Text;
              string fechaActaProvisional = equipo.aFecha(txtFechaActaProvisional.Text);
              string fechaActaTecnica = equipo.aFecha(txtFechaActaTecnica.Text);
              string fechaActaDefinitiva = equipo.aFecha(tx_FechaActaDefinitiva.Text);
              string fechaEquipoObra = equipo.aFecha(txtFechaEquipoObra.Text);
              string fechaEquipoEntregado = equipo.aFecha(tx_FechaEquipoEntregado.Text);
              string fechalimiteAprobacionPlanos = equipo.aFecha(tx_fechalimiteAprobacionPlanosFabrica.Text);

              string fechaAprobacionPlano = equipo.aFecha(tx_aprobacionPlano.Text);
              string fechaEntregaCliente = equipo.aFecha(tx_entregaCliente.Text);
              string fechaHabilitacionEquipo = equipo.aFecha(tx_habilitacionEquipo.Text);
              string fechaAproxEmbarque = equipo.aFecha(tx_fechaAproxEmbarque.Text);
              string fechaPagoEmbarque = equipo.aFecha(tx_fechaPagoEmbarque.Text);
              string fechaConfirmacionPagoEmbarque = equipo.aFecha(tx_fechaConfirmacionPagoEmbarque.Text);

              string nombreProyecto = HttpUtility.HtmlDecode(tx_NombreProyecto.Text);
              int Codproyecto = Convert.ToInt32(proyecto.getCodigoProyect(nombreProyecto));

              int codTipoEquipo = Convert.ToInt32(dd_tipoEquipo.SelectedValue);
              int codmarca = Convert.ToInt32(dd_Marca.SelectedValue);

              string codVariableSimec = tx_variableSimec.Text;

              NA_Responsables nres = new NA_Responsables();
              string codFiscalProy = nres.getCodigo_NombreResponsable(tx_fiscalProyecto.Text).ToString();
              if (codFiscalProy == "-1")
              {
                  codFiscalProy = "null";
              }

              string modelo = tx_modelo.Text;
              string pasajero = tx_pasajero.Text;
              int parada = 0;
              if (!tx_parada.Text.Equals(""))
              {
                  parada = Convert.ToInt32(tx_parada.Text);
              }
              string velocidad = tx_velocidad.Text;

            //---------------------------------------------------
              NA_Responsables Nresp = new NA_Responsables();
              string usuarioAux = Session["NameUser"].ToString();
              string passwordAux = Session["passworuser"].ToString();
              int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
              //---------------------------------------------------
                        
            bool permisodeProyecto = equipo.estaPermitidoEstadoProyecto(CodestadoEquipo);
            bool permisodeImportacionEstadoNuevo = equipo.estaPermitidoEstadoImportacion(CodestadoEquipo);            
            bool permisodeImportacionEstadoActual = equipo.estaPermitidoEstadoImportacion(codEstadoActual);
            if (
                (permisodeProyecto == true &&
                permisodeImportacionEstadoNuevo == false &&
                permisodeImportacionEstadoActual == false)
                ||
                (codEstadoActual == 4 &&
                permisodeImportacionEstadoNuevo == false)
                )
            {
                //// ---------- reglas para los fpr de no cambiar los estados de importacion exepto equipo en obra

                //poder modificar el estado (si es estado permitido FPR y son diferentes los estados antes y actual)
                if (equipo.estaPermitidoEstadoProyecto(CodestadoEquipo) && (codEstadoActual != CodestadoEquipo))
                {
                    // si el equipo tiene otro estado y pasa habilitado
                    if (CodestadoEquipo == 10)
                    {
                        // si tiene fecha de habilitacion
                        if (!fechaHabilitacionEquipo.Equals("null"))
                        {
                            NA_FechaEstadoEquipo fechaEstadoEq = new NA_FechaEstadoEquipo();
                            fechaEstadoEq.insertar(codigoEquipo, CodestadoEquipo, codUser);
                            int codFechaEstadoUltimoInsertado = fechaEstadoEq.ultimoinsertado();
                            equipo.ModificarFechaEstadoEquipo(codigoEquipo, codFechaEstadoUltimoInsertado);
                            //-------------------historial --------------
                            NA_Historial nhistorial = new NA_Historial();
                            nhistorial.insertar(codUser, "Modifico estado = " + ddlEstadoEquipo.SelectedItem.Text + " en la vista FEquipo.aspx un Equipo con exbo " + exbo);
                            //-------------------------------------------

                            string fecha = tx_habilitacionEquipo.Text;
                            DateTime fecha_ = Convert.ToDateTime(fecha, new CultureInfo("es-ES"));
                            fecha_ = fecha_.AddMonths(3);
                            int mesInicio = fecha_.Month;
                            int year = fecha_.Year;
                            int diaInicio = fecha_.Day;
                            string fechaInicialGratuita = convertidorFecha(fecha);
                            insertarSeguimientoPorDefecto(diaInicio,mesInicio, year, codigoEquipo,fechaInicialGratuita);

                            //-----------------envio de correo---------
                            string asunto = "Equipo habilitado " + exbo + "(" + Session["BaseDatos"].ToString()+")";
                            string Cuerpo = "El Exbo " + exbo + " del proyecto " + tx_NombreProyecto.Text + " ha sido Habilitado";
                            NA_EnvioCorreo nenvio = new NA_EnvioCorreo();
                            string baseDatos = Session["BaseDatos"].ToString();
                           bool correoOK = nenvio.Enviar_Correo_HabilitacionEquipo_Equipo(asunto, Cuerpo, baseDatos);
                            //-------------------------------------
                            if(correoOK ==false){
                                Response.Write("<script type='text/javascript'> alert('Error: Envio Correo') </script>");
                            }
                        }
                        else
                            Response.Write("<script type='text/javascript'> alert('Error: No se puede cambiar Habilitado sin fecha de Habilitacion') </script>");
                    }
                    else
                    {
                        // si es diferente al estado habilitado cambia nomas el estado                  
                        NA_FechaEstadoEquipo fechaEstadoEq = new NA_FechaEstadoEquipo();
                        fechaEstadoEq.insertar(codigoEquipo, CodestadoEquipo, codUser);
                        int codFechaEstadoUltimoInsertado = fechaEstadoEq.ultimoinsertado();
                        equipo.ModificarFechaEstadoEquipo(codigoEquipo, codFechaEstadoUltimoInsertado);
                        //-------------------historial --------------
                        NA_Historial nhistorial = new NA_Historial();
                        nhistorial.insertar(codUser, "Modifico estado = " + ddlEstadoEquipo.SelectedItem.Text + " en la vista FEquipo.aspx un Equipo con exbo " + exbo);
                        //-------------------------------------------
                    }

                }

                //// ----------FIN reglas para los fpr de no cambiar los estados de importacion exepto equipo en obra
            }
          //  else {
           //     Response.Write("<script type='text/javascript'> alert('Error: No se pudo Cambiar el Estado') </script>");
           // }

            string codRin = nres.getCodigo_NombreResponsable(tx_rin.Text).ToString();
            if (codRin == "-1")
            {
                codRin = "null";
            }

            string codRCC = nres.getCodigo_NombreResponsable(tx_rcc.Text).ToString();
            if (codRCC == "-1")
            {
                codRCC = "null";
            }

            string codtecmant = nres.getCodigo_NombreResponsable(tx_tecmantenimiento.Text).ToString();
            if (codtecmant == "-1")
            {
                codtecmant = "null";
            }

            string codSupervisor = nres.getCodigo_NombreResponsable(tx_supervisor.Text).ToString();
            if (codSupervisor == "-1")
            {
                codSupervisor = "null";
            }

            string IdentificadorAscensor = tx_identificacionAscensor.Text;
            string CODCLID = tx_CodCli_simec.Text;
            int monedaSimecEquipo;            
            int.TryParse(dd_monedaSimec.SelectedValue.ToString(), out monedaSimecEquipo);
            // solo actuliza los datos sin cambiar los estados                            
            equipo.modificar(codigoEquipo, fechaActaProvisional, fechaActaTecnica, fechaActaDefinitiva, 1, fechaEquipoObra, fechaEquipoEntregado, tipologia, codTipoEquipo, codmarca, codFiscalProy, fechalimiteAprobacionPlanos, modelo, pasajero, parada, velocidad, fechaAprobacionPlano, fechaEntregaCliente, fechaHabilitacionEquipo, fechaAproxEmbarque, fechaPagoEmbarque, fechaConfirmacionPagoEmbarque, CODCLID, monedaSimecEquipo, codRin, codRCC, codtecmant, codSupervisor, codVariableSimec, IdentificadorAscensor);
            enviarCorreodeFechasModificadas();

              string tecnicoInstalador = HttpUtility.HtmlDecode(tx_tecInstalador.Text);
              int codtecnicoInstalador = Convert.ToInt32(nres.getCodigo_NombreResponsable(tecnicoInstalador));
              NA_DetalleTecnicoAsignado Ndta = new NA_DetalleTecnicoAsignado();
              if (codtecnicoInstalador != -1)
              {
                  Ndta.eliminarAntiguos(codigoEquipo, 3);
                  Ndta.insertar(codigoEquipo, codtecnicoInstalador, 3);
              }
              
              //-------------------historial --------------
              NA_Historial nhistorial1 = new NA_Historial();              
              nhistorial1.insertar(codUser, "Modifico en la vista FEquipo.aspx un Equipo con exbo " + exbo);
              //-------------------------------------------
              string exboAux = txtExbo.Text;
              string edificioAux = tx_NombreProyecto.Text;

              string FiscalProyecto = HttpUtility.HtmlDecode(tx_fiscalProyecto.Text);
              int CodfiscalProy = nres.getCodigo_NombreResponsable(FiscalProyecto);
              string nombreEstado = ddlEstadoEquipo.SelectedItem.Text;
              listar1(edificioAux, exboAux, CodfiscalProy, nombreEstado);
              GridView1.SelectedIndex = -1; 
        }

        private void enviarCorreodeFechasModificadas()
        {
            string edificio = tx_NombreProyecto.Text;
            string exbo = txtExbo.Text;
            string fecha_ActaDefinitiva = tx_FechaActaDefinitiva.Text;
            string fecha_EntregaCliente = tx_entregaCliente.Text;
            string fecha_HabilitacionEquipo = tx_habilitacionEquipo.Text;
            string cuerpo = "";

            if (!fecha_ActaDefinitiva.Equals("") && tx_FechaActaDefinitiva.Enabled == true)
            {
                cuerpo = cuerpo + "Fecha Acta Definitiva = " + fecha_ActaDefinitiva + "<br>";
            }

            if (!fecha_EntregaCliente.Equals("") && tx_entregaCliente.Enabled == true) {
                cuerpo = cuerpo + "Fecha Entrega al Cliente = " + fecha_ActaDefinitiva + "<br>";
            }

            if (!fecha_HabilitacionEquipo.Equals("") && tx_habilitacionEquipo.Enabled == true)
            {
                cuerpo = cuerpo + "Fecha Habilitacion del Equipo = " + fecha_HabilitacionEquipo + "<br>";
            }

            if(!cuerpo.Equals("")){
                string asunto = "(" + Session["BaseDatos"].ToString() + ") Correo Automatico de cambio de Fechas del proyecto " + edificio + " con exbo " + exbo;
                string datoEnviar = "Envio Automatico del sistema <br> Se ha modificado del Edificio "+edificio+" con numero de Exbo "+exbo+" las siguientes Fechas :<br><br><br>"+cuerpo;
                NA_EnvioCorreo nenvio = new NA_EnvioCorreo();
                string baseDatos = Session["BaseDatos"].ToString();
                bool correoOK = nenvio.Enviar_Correo_Equipo(asunto,datoEnviar, baseDatos);
                if(correoOK == false){
                    Response.Write("<script type='text/javascript'> alert('Error: Envio Correo') </script>");
                }
            }

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            habilitar();
            limpiar();          
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
           // registrar();
           // string exboAux = txtExbo.Text;
           // string edificioAux = tx_NombreProyecto.Text;
           // int CodfiscalProy = Convert.ToInt32(dd_FiscalProy.SelectedValue);
           // string nombreEstado = ddlEstadoEquipo.SelectedItem.Text;
           // listar1(edificioAux,exboAux,CodfiscalProy,nombreEstado);
           // GridView1.SelectedIndex = -1;
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            DataBind();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiar();
           // habilitar();            
            cargarDatosSeleccion();
           // btnRegistrar.Enabled = false;
          //  btnModificar.Enabled = true;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            modificar(); 
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            NEquipo equipo = new NEquipo();
      
            int posicion = e.RowIndex;
            string codigo = GridView1.DataKeys[posicion].Value.ToString();
            equipo.eliminar1(Convert.ToInt32(codigo));
            Response.Write("<script type='text/javascript'> alert('Se ha eliminado correctamente') </script>");
            string exboAux = txtExbo.Text;
            string edificioAux = tx_NombreProyecto.Text;
            NA_Responsables nres = new NA_Responsables();
            int CodfiscalProy = nres.getCodigo_NombreResponsable(tx_fiscalProyecto.Text);
            string nombreEstado = ddlEstadoEquipo.SelectedItem.Text;
            listar1(edificioAux, exboAux, CodfiscalProy,nombreEstado);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            string exboAux = txtExbo.Text;
            string edificioAux = tx_NombreProyecto.Text;
            NA_Responsables nres = new NA_Responsables();
            int CodfiscalProy = nres.getCodigo_NombreResponsable(tx_fiscalProyecto.Text);
            string nombreEstado = ddlEstadoEquipo.SelectedItem.Text;
            listar1(edificioAux, exboAux, CodfiscalProy,nombreEstado);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string exboAux = txtExbo.Text;
            string edificioAux = tx_NombreProyecto.Text;
            NA_Responsables nres = new NA_Responsables();
            int CodfiscalProy = nres.getCodigo_NombreResponsable(tx_fiscalProyecto.Text);
            string nombreEstado = ddlEstadoEquipo.SelectedItem.Text;
            listar1(edificioAux, exboAux, CodfiscalProy,nombreEstado);
            GridView1.SelectedIndex = -1;
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

        protected void btExportarExcel_Click(object sender, EventArgs e)
        {
             string exboAux = txtExbo.Text;
            string edificioAux = tx_NombreProyecto.Text;
            NA_Responsables nres = new NA_Responsables();
            int CodfiscalProy = nres.getCodigo_NombreResponsable(tx_fiscalProyecto.Text);
            string nombreEstado = ddlEstadoEquipo.SelectedItem.Text;
            
            DataSet lista = null;
             NEquipo equipo = new NEquipo();

            if (CodfiscalProy == -1){
                 lista = equipo.listarEquipo2(exboAux,edificioAux,nombreEstado, true);
                GridView1.DataSource = lista;
                GridView1.DataBind();
            }
            else {
                lista = equipo.listarEquipo2ConFiscalProyecto(exboAux,edificioAux,CodfiscalProy,nombreEstado, true);
                GridView1.DataSource = lista;
                GridView1.DataBind();
            }


              //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Equipo " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = lista;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            limpiar();
            // habilitar();            
            cargarDatosSeleccion();
            // btnRegistrar.Enabled = false;
            //  btnModificar.Enabled = true;
        }

      
      
        //-----------------------envio de correo--------------
      

        }

    }