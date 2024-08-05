using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CallCenter_EventoCotiRepuesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (tienePermisoDeIngreso(60) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            DateTime date = DateTime.Now;
            System.Globalization.CultureInfo norwCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es");
            System.Globalization.Calendar cal = norwCulture.Calendar;
            int weekNo = cal.GetWeekOfYear(date, norwCulture.DateTimeFormat.CalendarWeekRule, norwCulture.DateTimeFormat.FirstDayOfWeek);
            // Show the result
            lb_numeroSemana.Text = weekNo.ToString();
            this.Title = Session["BaseDatos"].ToString();
            NA_Evento Nevento = new NA_Evento();
            lb_numeroEvento.Text = Convert.ToString(Nevento.getTikect() + 1);
            lb_EventoNuevo.Text = "Evento Nuevo " + Session["BaseDatos"].ToString();

            tx_regional.Text = Session["BaseDatos"].ToString();
            ckb_solicitudRepuesto.Checked = true;
            //--------------------------------------------------------------------
           /* NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            DataSet dato = Nresp.get_responsable(CodUser);
            tx_observacion.Text ="cotizado por: "+ dato.Tables[0].Rows[0][1].ToString();
           */
            if (!IsPostBack)
            {
                llenarTipoEvento();
                llenarPrioridad();
                dd_prioridad.SelectedIndex = 1;
            }

           // dd_prioridad.SelectedIndex = 1;
            dd_tipoEvento.SelectedIndex = 6;
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

        public void llenarTipoEvento()
        {
            NA_TipoEvento NtipoEvento = new NA_TipoEvento();
            dd_tipoEvento.DataSource = NtipoEvento.mostrarAllDatos();
            dd_tipoEvento.DataValueField = "codigo";
            dd_tipoEvento.DataTextField = "nombre";
            // dd_tipoEvento.Items.Add(new ListItem(""));
            dd_tipoEvento.AppendDataBoundItems = true;
            // ddlAnio.SelectedIndex = -1;
            dd_tipoEvento.DataBind();

        }

        public void llenarPrioridad()
        {
            NA_Prioridad nprioridad = new NA_Prioridad();
            dd_prioridad.DataSource = nprioridad.getAllPrioridad();
            dd_prioridad.DataValueField = "codigo";
            dd_prioridad.DataTextField = "nombre";
            // dd_tipoEvento.Items.Add(new ListItem(""));
            dd_prioridad.AppendDataBoundItems = true;
            // ddlAnio.SelectedIndex = -1;
            dd_prioridad.DataBind();
        }



        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProyectos2(string prefixText, int count)
        {
            string nombreProyecto = prefixText;

            NProyecto proyectoN = new NProyecto();
            DataSet tuplas = proyectoN.buscadorCallCenter(nombreProyecto);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }



        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaResponsable(string prefixText, int count)
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


        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                insertarEvento();
                limpiarTodo();
            }
            catch (Exception ex)
            {
                Response.Write("<script type='text/javascript'> alert('Error: " + ex + "') </script>");
            }
        }


        private void limpiarTodo()
        {
            dd_tipoEvento.SelectedIndex = -1;
            tx_Edificio.Text = "";
            tx_direccion.Text = "";
            tx_nombreCliente.Text = "";
            tx_celular.Text = "";
            tx_telefono.Text = "";
            tx_ascensores.Text = "";
            CheckBox1_ascensorParado.Checked = false;
            CheckBox2_personasAtrapadas.Checked = false;
            tx_observacion.Text = "";
        }

        private void insertarEvento()
        {
            int CodTipoEvento1 = Convert.ToInt32(dd_tipoEvento.SelectedValue);
            int codPrioridad = Convert.ToInt32(dd_prioridad.SelectedValue);
            int semana = Convert.ToInt32(lb_numeroSemana.Text);
            string edificio = tx_Edificio.Text;
            string direccion = tx_direccion.Text;
            string nombreCliente = tx_nombreCliente.Text;
            string celular = tx_celular.Text;
            string telefono = tx_telefono.Text;
            string ascensores = tx_ascensores.Text;
            bool ascensorParado = CheckBox1_ascensorParado.Checked;
            int ascensorParado_aux = 0;

            
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            DataSet dato = Nresp.get_responsable(CodUser);
            
            if (ascensorParado)
            {
                ascensorParado_aux = 1;
            }

            bool personasAtrapadas = CheckBox2_personasAtrapadas.Checked;
            int personasAtrapadas_aux = 0;
            if (personasAtrapadas)
            {
                personasAtrapadas_aux = 1;
            }

            string observacion = tx_observacion.Text + "\r\n cotizado por: " + dato.Tables[0].Rows[0][1].ToString();


            NProyecto Nproy = new NProyecto();
            int codproy = -1;
            if (Nproy.buscar(edificio).Tables[0].Rows.Count > 0)
            {
                codproy = Convert.ToInt32(Nproy.buscar(edificio).Tables[0].Rows[0][0].ToString());
            }
            NA_Evento Nevento = new NA_Evento();


            bool solicitudRepuesto = true;
            bool cambioRepuesto = true;
            bool areaCotizacionRepuesto = true;
            bool areaCallcenter = false;            
            bool areaCliente = false;
            bool areaRin = false;
            bool areaRCC = true;

            bool ok = Nevento.insertar(semana, nombreCliente, telefono, celular, edificio, direccion, ascensores, "Abierto", observacion, CodTipoEvento1, codproy, ascensorParado_aux, personasAtrapadas_aux, codPrioridad, CodUser, cambioRepuesto, areaCotizacionRepuesto, solicitudRepuesto, areaCallcenter, areaRin, areaRCC, areaCliente);
            
            int codigoEvento = Nevento.getultimoInsertado();
            bool banderasolicitudRepuesto = ckb_solicitudRepuesto.Checked;
            bool ok2 = Nevento.modificarDatosEventoSolicitudRepuesto(codigoEvento, "Cambio de Repuesto", tx_observacion.Text, "now()", true, banderasolicitudRepuesto);
            
            //-----------------------historial
            NA_Historial nhistorial = new NA_Historial();
            nhistorial.insertar(CodUser, "CallCenter, Cotizacion Ha insertado una Cotizacion Evento tiket = " + lb_numeroEvento.Text + " del Edificio " + edificio);
            //----------------- historial----


            if (banderasolicitudRepuesto == true)
            {
                Session["codEvento"] = codigoEvento;
                Session["nombreEdificioEvento"] = tx_Edificio.Text;
                Session["banderaEvento"] = true;
                Response.Redirect("../Presentacion/FA_AdicionarCotizacionRepuesto.aspx");
            }

            if (ok)
            {               
                Response.Write("<script type='text/javascript'> alert('OK: El Evento fue insertado') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: El Evento NO ha sido insertado') </script>");

        }

        
             protected void bt_validarEdificio_Click(object sender, EventArgs e)
        {
            lb_verificar.Text = "";
            lb_verificar2.Text = "";
            buscarEspecifico();
            verificarDeudaEdificio();
            VerificarRepuestoDeuda();
            VerificarCotizacionesPendientes();
            verificarPlanpago();
            cuantoAscensoresParadoClientehay();
            cuantoAscensoresParadoNosotroshay();
        }


        private void cuantoAscensoresParadoClientehay()
        {
            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);
            int cantidad = nproyec.CuantosAscensorestieneParadoCliente(codigoEdificio);
            if (cantidad > 0)
            {
                lb_paradoCliente.Text = "Tiene " + cantidad + " Asc. Parado Cliente";
                lb_paradoCliente.BackColor = Color.Red;
                lb_paradoCliente.ForeColor = Color.White;
            }
            else
            {
                lb_paradoCliente.Text = "No tiene Asc. Parado Cliente";
                lb_paradoCliente.BackColor = Color.LimeGreen;
                lb_paradoCliente.ForeColor = Color.Black;

            }
        }

        private void cuantoAscensoresParadoNosotroshay()
        {
            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);
            int cantidad = nproyec.CuantosAscensorestieneParadoNosotros(codigoEdificio);
            if (cantidad > 0)
            {
                lb_paradoNosotros.Text = "Tiene " + cantidad + " Asc. Parado Nosotros";
                lb_paradoNosotros.BackColor = Color.Red;
                lb_paradoNosotros.ForeColor = Color.White;
            }
            else
            {
                lb_paradoNosotros.Text = "No tiene Asc. Parado Nosotros";
                lb_paradoNosotros.BackColor = Color.LimeGreen;
                lb_paradoNosotros.ForeColor = Color.Black;

            }
        }


        private void verificarPlanpago()
        {
            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);

            if (nproyec.tienePlanPago(codigoEdificio))
            {
                lb_planPago.Text = "Tiene Plan de Pagos";
                lb_planPago.BackColor = Color.Yellow;
                lb_planPago.ForeColor = Color.Black;
            }
            else
            {
                lb_planPago.Text = "No tiene Plan de Pagos";
                lb_planPago.BackColor = Color.Red;
                lb_planPago.ForeColor = Color.White;
            }
        }


        public void VerificarCotizacionesPendientes()
        {

            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);
            if (codigoEdificio > -1)
            {
                NA_Repuesto nrepuestos = new NA_Repuesto();
                int cantidadCoti = nrepuestos.getCantidadCotizacionesRepuesto(codigoEdificio);

                if (cantidadCoti > 0)
                {
                    lb_cotizacionRepuesto.Text = "Tiene " + cantidadCoti + " Cotizaciones Pendiente";
                    lb_cotizacionRepuesto.BackColor = Color.Red;
                    lb_cotizacionRepuesto.ForeColor = Color.White;
                }
                else
                {
                    lb_cotizacionRepuesto.Text = "No tiene Cotizaciones Pendientes";
                    lb_cotizacionRepuesto.BackColor = Color.LimeGreen;
                    lb_cotizacionRepuesto.ForeColor = Color.Black;
                }
            }

        }



        public void VerificarRepuestoDeuda()
        {

            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);

            if (nproyec.tieneDeudasRepuestoPendientesProyecto(codigoEdificio))
            {
                lb_repuesto.Text = "Tiene Deudas de Repuesto";
                lb_repuesto.BackColor = Color.Red;
                lb_repuesto.ForeColor = Color.White;
            }
            else
            {
                lb_repuesto.Text = "No tiene Deudas de Repuesto";
                lb_repuesto.BackColor = Color.LimeGreen;
                lb_repuesto.ForeColor = Color.Black;
            }
        }


        public void verificarDeudaEdificio()
        {

            NProyecto nproyec = new NProyecto();
            int codigoEdificio = nproyec.getCodigoProyect(tx_Edificio.Text);

            if (nproyec.tieneDeudasPendientesProyecto(codigoEdificio))
            {
                lb_verificar2.Text = "Tiene Deudas de Instalacion";
                lb_verificar2.BackColor = Color.Red;
                lb_verificar2.ForeColor = Color.White;
            }
            else
            {
                lb_verificar2.Text = "No tiene Deudas de Instalacion";
                lb_verificar2.BackColor = Color.LimeGreen;
                lb_verificar2.ForeColor = Color.Black;
            }


        }


        public void buscarEspecifico()
        {

            string year = "Todos";
            string edificio = tx_Edificio.Text;
            NProyecto nproy = new NProyecto();

            if (nproy.existeProyectoEspecifico(edificio))
            {

                if (year == "Todos")
                {
                    string exbo = "";
                    NA_Seguimiento segn = new NA_Seguimiento();
                    int meseslimiteAtrazadoPermitido = NA_VariablesGlobales.meseslimitesdeAtrazadosPermitidosMantenimiento;
                    DataSet tuplaRes = segn.getTodosEquiposMantenimientoMorososEspecifico(exbo, edificio, meseslimiteAtrazadoPermitido);
                    if (tuplaRes == null)
                    {
                        lb_verificar.Text = "No tiene Prevision realizada";
                        lb_verificar.BackColor = Color.Red;
                        lb_verificar.ForeColor = Color.White;
                    }
                    else
                        if (tuplaRes.Tables[0].Rows.Count == 0)
                        {
                            lb_verificar.Text = "No tiene Deudas Mantenimiento";
                            lb_verificar.BackColor = Color.LimeGreen;
                            lb_verificar.ForeColor = Color.Black;
                        }
                        else
                        {
                            lb_verificar.Text = "Tiene Deudas Mantenimiento";
                            lb_verificar.BackColor = Color.Red;
                            lb_verificar.ForeColor = Color.White;
                        }
                }

            }
            else
            {
                lb_verificar.Text = "El edificio no Existe";
                lb_verificar.BackColor = Color.Yellow;
            }

        }


        protected void tx_Edificio_TextChanged(object sender, EventArgs e)
        {
            ponerDireccion();
        }

        private void ponerDireccion()
        {
            string nombreEdificio = tx_Edificio.Text;
            if (!nombreEdificio.Equals(""))
            {
                NProyecto nproyect = new NProyecto();
                DataSet res_edificio = nproyect.getProyect2(nombreEdificio);
                if (res_edificio.Tables[0].Rows.Count > 0)
                {
                    tx_direccion.Text = res_edificio.Tables[0].Rows[0][3].ToString();
                }
            }
        }

    }
}