using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_GestionarSeguimiento2 : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(96) == false)
            {
                desabilitarBotones();
            }


            if(!IsPostBack){                
                 cargarSeguimientoEquipo();
                 cargarTipoPago();
                 cargarEstadoMantenimiento();

                 NA_Historial nhistorial = new NA_Historial();
                 int codUser = Convert.ToInt32(Session["coduser"].ToString());
                 nhistorial.insertar(codUser, "Ha ingresado a Gestionar Seguimiento2");
            }
        }

        private void desabilitarBotones()
        {
            bt_Modificar.Enabled = false;
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

        private void cargarTipoPago(){
        NA_TipoPago NtipoPago = new NA_TipoPago();
        DataSet datosCargar = NtipoPago.mostrarAllDatos();
        dd_PlanPago.DataSource = datosCargar;
        dd_PlanPago.DataValueField = "codigo";
        dd_PlanPago.DataTextField = "nombre";
        dd_PlanPago.Items.Add(new ListItem("", "-1"));
        dd_PlanPago.AppendDataBoundItems = true;
        dd_PlanPago.SelectedIndex = -1;
        dd_PlanPago.DataBind(); 
        }

        private void cargarEstadoMantenimiento() {
            NA_EstadoMantenimiento Nman = new NA_EstadoMantenimiento();
            DataSet datosCargar = Nman.mostrarAllDatos();
            dd_estadoMantenimiento.DataSource = datosCargar;
            dd_estadoMantenimiento.DataValueField = "codigo";
            dd_estadoMantenimiento.DataTextField = "nombre";
            dd_estadoMantenimiento.Items.Add(new ListItem("", "-1"));
            dd_estadoMantenimiento.AppendDataBoundItems = true;
            dd_estadoMantenimiento.SelectedIndex = -1;
            dd_estadoMantenimiento.DataBind();         
        }

        

        private void cargarSeguimientoEquipo() {
            NA_Seguimiento Nsegui = new NA_Seguimiento();
           int codigoEquipoExbo = Convert.ToInt32(Session["codEquipo"].ToString());
            DataSet mostrarTabla = Nsegui.mostrarSeguimiento(codigoEquipoExbo);
            gv_SeguimientoMantenimiento.DataSource = mostrarTabla;
            gv_SeguimientoMantenimiento.DataBind();
        }

        private void cargarSeguimientoMes(int codSeguimientoAux) {
            NA_DetalleSeguimiento NdetalleSegui = new NA_DetalleSeguimiento();
            DataSet datosTabla = NdetalleSegui.MostrarAllDatos(codSeguimientoAux);
            gv_seguiMes.DataSource = datosTabla;
            gv_seguiMes.DataBind(); 
        }

        private void cargarSeguimientoModificar(int codSeguimientoAux)
        {
            NA_Seguimiento segui = new NA_Seguimiento();
            DataSet datosRellenar = segui.getOnlySeguimiento(codSeguimientoAux);
            tx_year.Text = datosRellenar.Tables[0].Rows[0][1].ToString();
            tx_horaCobro.Text = datosRellenar.Tables[0].Rows[0][2].ToString();
            tx_fechaCobroPlanificado.Text = datosRellenar.Tables[0].Rows[0][3].ToString();
            dd_PlanPago.SelectedValue = datosRellenar.Tables[0].Rows[0][4].ToString();
            tx_lugarPago.Text = datosRellenar.Tables[0].Rows[0][5].ToString();           
            tx_FechaContrato.Text = datosRellenar.Tables[0].Rows[0][6].ToString();
            tx_MesesGratuitos.Text = datosRellenar.Tables[0].Rows[0][7].ToString();
            tx_MesInicial.Text = datosRellenar.Tables[0].Rows[0][8].ToString();
            tx_MesFinal.Text = datosRellenar.Tables[0].Rows[0][9].ToString();
            tx_Detalle.Text = datosRellenar.Tables[0].Rows[0][10].ToString();
            dd_estadoMantenimiento.SelectedValue = datosRellenar.Tables[0].Rows[0][11].ToString();
            NA_DetalleSeguimiento NdetalleSegui = new NA_DetalleSeguimiento();
            tx_Mensualidad.Text = NdetalleSegui.getMaximoPagoSegMes(codSeguimientoAux);

                int mes = Convert.ToInt32(gv_seguiMes.Rows[0].Cells[1].Text);
                float pago = Convert.ToSingle(gv_seguiMes.Rows[0].Cells[3].Text);
                if (mes == 1 && pago >0) { cb_enero.Checked = true; } else cb_enero.Checked = false;
                    mes  = Convert.ToInt32(gv_seguiMes.Rows[1].Cells[1].Text);
                    pago = Convert.ToSingle(gv_seguiMes.Rows[1].Cells[3].Text);
                if (mes == 2 && pago > 0) { cb_febrero.Checked = true; } else cb_febrero.Checked = false;
                    mes  = Convert.ToInt32(gv_seguiMes.Rows[2].Cells[1].Text);
                    pago = Convert.ToSingle(gv_seguiMes.Rows[2].Cells[3].Text);
                if (mes == 3 && pago > 0) { cb_marzo.Checked = true; } else cb_marzo.Checked = false;
                    mes = Convert.ToInt32(gv_seguiMes.Rows[3].Cells[1].Text);
                    pago = Convert.ToSingle(gv_seguiMes.Rows[3].Cells[3].Text);
                if (mes == 4 && pago > 0) { cb_abril.Checked = true; } else cb_abril.Checked = false;
                    mes = Convert.ToInt32(gv_seguiMes.Rows[4].Cells[1].Text);
                    pago = Convert.ToSingle(gv_seguiMes.Rows[4].Cells[3].Text);
                if (mes == 5 && pago > 0) { cb_mayo.Checked = true; } else cb_mayo.Checked = false;
                    mes = Convert.ToInt32(gv_seguiMes.Rows[5].Cells[1].Text);
                    pago = Convert.ToSingle(gv_seguiMes.Rows[5].Cells[3].Text);
                if (mes == 6 && pago > 0) { cb_junio.Checked = true; } else cb_junio.Checked = false;
                    mes = Convert.ToInt32(gv_seguiMes.Rows[6].Cells[1].Text);
                    pago = Convert.ToSingle(gv_seguiMes.Rows[6].Cells[3].Text);
                if (mes == 7 && pago > 0) { cb_julio.Checked = true; } else cb_julio.Checked = false;
                    mes = Convert.ToInt32(gv_seguiMes.Rows[7].Cells[1].Text);
                    pago = Convert.ToSingle(gv_seguiMes.Rows[7].Cells[3].Text);
                if (mes == 8 && pago > 0) { cb_agosto.Checked = true; } else cb_agosto.Checked = false;
                    mes = Convert.ToInt32(gv_seguiMes.Rows[8].Cells[1].Text);
                    pago = Convert.ToSingle(gv_seguiMes.Rows[8].Cells[3].Text);
                if (mes == 9 && pago > 0) { cb_septiembre.Checked = true; } else cb_septiembre.Checked = false;
                    mes = Convert.ToInt32(gv_seguiMes.Rows[9].Cells[1].Text);
                    pago = Convert.ToSingle(gv_seguiMes.Rows[9].Cells[3].Text);
                if (mes == 10 && pago > 0) { cb_octubre.Checked = true; } else cb_octubre.Checked = false;
                    mes = Convert.ToInt32(gv_seguiMes.Rows[10].Cells[1].Text);
                    pago = Convert.ToSingle(gv_seguiMes.Rows[10].Cells[3].Text);
                if (mes == 11 && pago > 0) { cb_noviembre.Checked = true; } else cb_noviembre.Checked = false;
                    mes = Convert.ToInt32(gv_seguiMes.Rows[11].Cells[1].Text);
                    pago = Convert.ToSingle(gv_seguiMes.Rows[11].Cells[3].Text);
                if (mes == 12 && pago > 0) { cb_diciembre.Checked = true; } else cb_diciembre.Checked = false;
            

        }

        protected void gv_SeguimientoMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codSeguimiento = Convert.ToInt32(gv_SeguimientoMantenimiento.SelectedRow.Cells[1].Text);
            cargarSeguimientoMes(codSeguimiento);
            cargarSeguimientoModificar(codSeguimiento);
        }

      

        protected void gv_seguiMes_RowEditing(object sender, GridViewEditEventArgs e)
        {
           
            gv_seguiMes.EditIndex = e.NewEditIndex;
           // Response.Write("<script type='text/javascript'> alert('el index es " + e.NewEditIndex + "') </script>");
            int codSeguimiento = Convert.ToInt32(gv_SeguimientoMantenimiento.SelectedRow.Cells[1].Text);
            NA_DetalleSeguimiento NdetalleSegui = new NA_DetalleSeguimiento();
            DataSet datosTabla = NdetalleSegui.MostrarAllDatos(codSeguimiento);
            gv_seguiMes.DataSource = datosTabla;           
            gv_seguiMes.DataBind();


            int index = gv_seguiMes.EditIndex;
            GridViewRow row = gv_seguiMes.Rows[index];
            TextBox auxtexto = (TextBox)row.Cells[1].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto = (TextBox)row.Cells[2].Controls[0];
            auxtexto.ReadOnly = true;

            TextBox MontoModificar = (TextBox)row.Cells[3].Controls[0];
            MontoModificar.BackColor = Color.Yellow;

            auxtexto = (TextBox)row.Cells[4].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto = (TextBox)row.Cells[5].Controls[0];
            auxtexto.ReadOnly = true;
           
                  
        }

        protected void gv_seguiMes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_seguiMes.EditIndex = -1;
            int codSeguimiento = Convert.ToInt32(gv_SeguimientoMantenimiento.SelectedRow.Cells[1].Text);
            cargarSeguimientoMes(codSeguimiento);   
        }

        protected void gv_seguiMes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        
            int index = gv_seguiMes.EditIndex;
            GridViewRow row = gv_seguiMes.Rows[index];
           // string MontoModificar = row.Cells[3].Text;
            TextBox MontoModificar = (TextBox)row.Cells[3].Controls[0];
            gv_seguiMes.EditIndex = -1;            
          //  Response.Write("<script type='text/javascript'> alert(' se modifico " + MontoModificar.Text + "') </script>");
            int codSeguimiento = Convert.ToInt32(gv_SeguimientoMantenimiento.SelectedRow.Cells[1].Text);
            TextBox auxMex = (TextBox)row.Cells[1].Controls[0];
            int codMes = Convert.ToInt32(auxMex.Text);
            NA_DetalleSeguimiento Ndetsegui = new NA_DetalleSeguimiento();
            float montoPagar_Aux = Convert.ToSingle(MontoModificar.Text);
            Ndetsegui.modificarMotoPagar(codSeguimiento,codMes,montoPagar_Aux);

            NA_Historial nhistorial = new NA_Historial();
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Ha Modificado seguimiento " + codSeguimiento + " del mes " + codMes + " por el monto a Pagar " + MontoModificar.Text);
            

            cargarSeguimientoMes(codSeguimiento);   

        }


        private void limpiarTodosDatos() {
            tx_Detalle.Text = "";
            tx_fechaCobroPlanificado.Text = "";
            tx_FechaContrato.Text = "";            
            tx_horaCobro.Text = "";
            tx_lugarPago.Text = "";
            tx_MesesGratuitos.Text = "0";
            tx_MesFinal.Text = "";
            tx_MesInicial.Text = "";
            tx_year.Text = "";
            tx_Mensualidad.Text = "0";
            dd_PlanPago.SelectedIndex = -1;
            dd_estadoMantenimiento.SelectedIndex = -1;

            cb_enero.Checked = false;
            cb_febrero.Checked = false;
            cb_marzo.Checked = false;
            cb_abril.Checked = false;
            cb_mayo.Checked = false;
            cb_junio.Checked = false;
            cb_julio.Checked = false;
            cb_agosto.Checked = false;
            cb_septiembre.Checked = false;
            cb_octubre.Checked = false;
            cb_noviembre.Checked = false;
            cb_diciembre.Checked = false;
        }

        protected void tx_limpiar_Click(object sender, EventArgs e)
        {
            limpiarTodosDatos();
        }


        public string aumentarFecha_conMes(string fecha, int meses)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha, new CultureInfo("es-ES"));
                fecha_ = fecha_.AddMonths(meses);
                string _fecha = fecha_.ToString("yyyy/MM/dd");
                return  "'"+_fecha+"'";
            }
            else
                return "null";
        }



        private void insertarSeguimiento() {
            int year = Convert.ToInt32(tx_year.Text);
            string horaCobro = "";
            if (tx_horaCobro.Text == "")
            {
                horaCobro = "null";
            }
            else
                horaCobro = "'" + tx_horaCobro.Text + "'";

            int mesesGratuitos = 0;
            if (tx_MesesGratuitos.Text != "")
            {
             mesesGratuitos = Convert.ToInt32(tx_MesesGratuitos.Text);
            }


            string fechaCobroPlanificado = convertidorFecha(tx_fechaCobroPlanificado.Text);
            int codPlanPago =  Convert.ToInt32(dd_PlanPago.SelectedValue);
            string lugarPago = tx_lugarPago.Text;
            string fechaContrato = convertidorFecha(tx_FechaContrato.Text);           
            string mesInicial = convertidorFecha(tx_MesInicial.Text);
            string mesFinal = aumentarFecha_conMes(tx_MesInicial.Text, mesesGratuitos);
            string detalle = tx_Detalle.Text;
            int codigoEquipoExbo = Convert.ToInt32(Session["codEquipo"].ToString());
            NA_Seguimiento Nsegui = new NA_Seguimiento();

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

                int codestadoMan = Convert.ToInt32(dd_estadoMantenimiento.SelectedValue);
                NA_FechaEstadoMan NfechaEstadoMan = new NA_FechaEstadoMan();
                NfechaEstadoMan.insertar(codultimoInsertado, codestadoMan, codUser);
                int ultimaFechaEstadoMan_insertada = NfechaEstadoMan.ultimoinsertado();
                Nsegui.modificarFechaEstadoMan(codultimoInsertado, ultimaFechaEstadoMan_insertada);

                NA_Historial nhistorial = new NA_Historial();                
                nhistorial.insertar(codUser, "Ha Insertado un nuevo Seguimiento con codigo " + codultimoInsertado);

                NA_DetalleSeguimiento NdetSeguimiento = new NA_DetalleSeguimiento();
                if (codultimoInsertado > -1)
                {
                    if(cb_enero.Checked == true){
                        NdetSeguimiento.insertar(codultimoInsertado, 1, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")), 0, "No",codUser);
                    }else
                        NdetSeguimiento.insertar(codultimoInsertado, 1, 0, 0, "No", codUser);

                    if (cb_febrero.Checked == true)
                    {
                        NdetSeguimiento.insertar(codultimoInsertado, 2, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")), 0, "No", codUser);
                    }else
                        NdetSeguimiento.insertar(codultimoInsertado, 2, 0, 0, "No", codUser);

                    if (cb_marzo.Checked == true)
                    {
                        NdetSeguimiento.insertar(codultimoInsertado, 3, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")), 0, "No", codUser);
                    }else
                        NdetSeguimiento.insertar(codultimoInsertado, 3, 0, 0, "No", codUser);

                    if (cb_abril.Checked == true)
                    {
                        NdetSeguimiento.insertar(codultimoInsertado, 4, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")), 0, "No", codUser);
                    }else
                        NdetSeguimiento.insertar(codultimoInsertado, 4, 0, 0, "No", codUser);

                    if (cb_mayo.Checked == true)
                    {
                        NdetSeguimiento.insertar(codultimoInsertado, 5, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")), 0, "No", codUser);
                    }else
                        NdetSeguimiento.insertar(codultimoInsertado, 5, 0, 0, "No", codUser);

                    if (cb_junio.Checked == true)
                    {
                        NdetSeguimiento.insertar(codultimoInsertado, 6, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")), 0, "No", codUser);
                    }else
                        NdetSeguimiento.insertar(codultimoInsertado, 6, 0, 0, "No", codUser);
                    
                    if (cb_julio.Checked == true)
                    {
                        NdetSeguimiento.insertar(codultimoInsertado, 7, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")), 0, "No", codUser);
                    }else
                        NdetSeguimiento.insertar(codultimoInsertado, 7, 0, 0, "No", codUser);

                    if (cb_agosto.Checked == true)
                    {
                        NdetSeguimiento.insertar(codultimoInsertado, 8, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")), 0, "No", codUser);
                    }else
                        NdetSeguimiento.insertar(codultimoInsertado, 8, 0, 0, "No", codUser);

                    if (cb_septiembre.Checked == true)
                    {
                        NdetSeguimiento.insertar(codultimoInsertado, 9, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")), 0, "No", codUser);
                    }else
                        NdetSeguimiento.insertar(codultimoInsertado, 9, 0, 0, "No", codUser);

                    if (cb_octubre.Checked == true)
                    {
                        NdetSeguimiento.insertar(codultimoInsertado, 10, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")), 0, "No", codUser);
                    }else
                        NdetSeguimiento.insertar(codultimoInsertado, 10, 0, 0, "No", codUser);

                    if (cb_noviembre.Checked == true)
                    {
                        NdetSeguimiento.insertar(codultimoInsertado, 11, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")), 0, "No", codUser);
                    }else
                        NdetSeguimiento.insertar(codultimoInsertado, 11, 0, 0, "No", codUser);

                    if (cb_diciembre.Checked == true)
                    {
                        NdetSeguimiento.insertar(codultimoInsertado, 12, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")), 0, "No", codUser);
                    }else
                        NdetSeguimiento.insertar(codultimoInsertado, 12, 0, 0, "No", codUser);
                        
                    
                }
                Response.Write("<script type='text/javascript'> alert(' El dato ha sido insertado Correctamente') </script>");

            }
            else
            {
                Response.Write("<script type='text/javascript'> alert(' La Prevision ya Existe ') </script>");
            }
            
        }

        private void modificarDatos()
        {
            int codSeguimiento = Convert.ToInt32(gv_SeguimientoMantenimiento.SelectedRow.Cells[1].Text);
            int year = Convert.ToInt32(tx_year.Text);
            string horaCobro = "";
            if (tx_horaCobro.Text == "")
            {
                horaCobro = "null";
            }else
                horaCobro = "'"+tx_horaCobro.Text+"'";


            string fechaCobroPlanificado = convertidorFecha(tx_fechaCobroPlanificado.Text);
            int codPlanPago = Convert.ToInt32(dd_PlanPago.SelectedValue);
            string lugarPago = tx_lugarPago.Text;            
            string fechaContrato = convertidorFecha(tx_FechaContrato.Text);
            int mesesGratuitos = Convert.ToInt32(tx_MesesGratuitos.Text);
            string mesInicial = convertidorFecha(tx_MesInicial.Text);
            string mesFinal = convertidorFecha(tx_MesFinal.Text);            
            string detalle = tx_Detalle.Text;

            //-------------------------------------------------
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            //----------------------------------------------------------

            int codestadoMan = Convert.ToInt32(dd_estadoMantenimiento.SelectedValue);
            NA_FechaEstadoMan NfechaEstadoMan = new NA_FechaEstadoMan();
            NfechaEstadoMan.insertar(codSeguimiento, codestadoMan, codUser);
            int ultimaFechaEstadoMan_insertada = NfechaEstadoMan.ultimoinsertado();           
            
            NA_Seguimiento Nsegui = new NA_Seguimiento();
            bool bandera = Nsegui.modificar(codSeguimiento,detalle,horaCobro,fechaCobroPlanificado,lugarPago,fechaContrato,mesesGratuitos,mesInicial,mesFinal,codPlanPago,year,ultimaFechaEstadoMan_insertada);
          //  Response.Write("<script type='text/javascript'> alert(' el dato ha sido modificado = " + bandera + "') </script>");

            NA_DetalleSeguimiento NdetSeguimiento = new NA_DetalleSeguimiento();
            if (cb_enero.Checked == true)
            {
                NdetSeguimiento.modificar(codSeguimiento, 1, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")));
            }
            else
                NdetSeguimiento.modificar(codSeguimiento, 1, 0);

            if (cb_febrero.Checked == true)
            {
                NdetSeguimiento.modificar(codSeguimiento, 2, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")));
            }
            else
                NdetSeguimiento.modificar(codSeguimiento, 2, 0);

            if (cb_marzo.Checked == true)
            {
                NdetSeguimiento.modificar(codSeguimiento, 3, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")));
            }
            else
                NdetSeguimiento.modificar(codSeguimiento, 3, 0);

            if (cb_abril.Checked == true)
            {
                NdetSeguimiento.modificar(codSeguimiento, 4, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")));
            }
            else
                NdetSeguimiento.modificar(codSeguimiento, 4, 0);

            if (cb_mayo.Checked == true)
            {
                NdetSeguimiento.modificar(codSeguimiento, 5, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")));
            }
            else
                NdetSeguimiento.modificar(codSeguimiento, 5, 0);

            if (cb_junio.Checked == true)
            {
                NdetSeguimiento.modificar(codSeguimiento, 6, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")));
            }
            else
                NdetSeguimiento.modificar(codSeguimiento, 6, 0);

            if (cb_julio.Checked == true)
            {
                NdetSeguimiento.modificar(codSeguimiento, 7, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")));
            }
            else
                NdetSeguimiento.modificar(codSeguimiento, 7, 0);

            if (cb_agosto.Checked == true)
            {
                NdetSeguimiento.modificar(codSeguimiento, 8, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")));
            }
            else
                NdetSeguimiento.modificar(codSeguimiento, 8, 0);

            if (cb_septiembre.Checked == true)
            {
                NdetSeguimiento.modificar(codSeguimiento, 9, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")));
            }
            else
                NdetSeguimiento.modificar(codSeguimiento, 9, 0);

            if (cb_octubre.Checked == true)
            {
                NdetSeguimiento.modificar(codSeguimiento, 10, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")));
            }
            else
                NdetSeguimiento.modificar(codSeguimiento, 10, 0);

            if (cb_noviembre.Checked == true)
            {
                NdetSeguimiento.modificar(codSeguimiento, 11, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")));
            }
            else
                NdetSeguimiento.modificar(codSeguimiento, 11, 0);

            if (cb_diciembre.Checked == true)
            {
                NdetSeguimiento.modificar(codSeguimiento, 12, Convert.ToSingle(tx_Mensualidad.Text.Replace(".", ",")));
            }
            else
                NdetSeguimiento.modificar(codSeguimiento, 12, 0);


            cargarSeguimientoMes(codSeguimiento);

            NA_Historial nhistorial = new NA_Historial();            
            nhistorial.insertar(codUser, "Ha Modificado el  Seguimiento con codigo " + codSeguimiento);
            
        }

        protected void bt_insertarSeguimiento_Click(object sender, EventArgs e)
        {
            if(tx_year.Text != ""){
            insertarSeguimiento();
            cargarSeguimientoEquipo();
            }else
                Response.Write("<script type='text/javascript'> alert('No tiene Año') </script>");
            
         }

        protected void bt_Modificar_Click(object sender, EventArgs e)
        {
            if (gv_SeguimientoMantenimiento.SelectedIndex > -1) {
                modificarDatos();
                cargarSeguimientoEquipo();
                
            }else
                Response.Write("<script type='text/javascript'> alert('No ha seleccionado Prevision ') </script>");
            
        }

        protected void gv_seguiMes_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (tienePermisoDeIngreso(96) == false)
            {
                e.Row.Cells[0].Visible = false;
            }
                      
        }

      
      
    }
}