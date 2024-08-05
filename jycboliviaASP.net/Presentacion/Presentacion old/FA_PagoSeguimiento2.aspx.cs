using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
using jycboliviaASP.net.DatosSimec;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_PagoSeguimiento2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(73) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                cargarTipoCambio();
                cargarMonedas();
                cargarCuentasBancos();
                dd_moneda.SelectedValue = "2";
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha ingresado a Gestionar Seguimiento");
                DateTime hoy = DateTime.Now;
                tx_FechaGeneral.Text = hoy.ToString("dd/MM/yyyy");
            }
        }

        private void cargarCuentasBancos()
        {
            NA_banco banco = new NA_banco();
            DataSet tuplas = banco.getCuentaBancaria("");

            dd_cuentaBanco.DataSource = tuplas;
            dd_cuentaBanco.DataValueField = "codigo";
            dd_cuentaBanco.DataTextField = "cuenta";
            dd_cuentaBanco.Items.Add(new ListItem("--Ninguno--", "0"));
            dd_cuentaBanco.AppendDataBoundItems = true;
            dd_cuentaBanco.SelectedIndex = -1;
            dd_cuentaBanco.DataBind();
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


        [WebMethod]
        [ScriptMethod]
        public static string[] getListaCuentaBancos(string prefixText, int count)
        {
            string nombreProyecto = prefixText;
            NA_banco banco = new NA_banco();
            DataSet tuplas = banco.getCuentaBancaria(prefixText);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;
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



        private void cargarMonedas()
        {
            NA_Seguimiento seg = new NA_Seguimiento();
            DataSet datos = seg.get_Monedas();
            dd_moneda.DataSource = datos;
            dd_moneda.DataValueField = "CODIGO";
            dd_moneda.DataTextField = "MONEDA";
            dd_moneda.AppendDataBoundItems = true;
            dd_moneda.DataBind();
        }
                

        private void cargarTipoCambio()
        {
            NA_Seguimiento seg = new NA_Seguimiento();
            float TC = seg.get_TipoUltimoTipoCambio();
            tx_tipoCambio.Text = TC.ToString();
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

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string exbo = tx_Exbo.Text;
            string edificio = tx_nombreProyecto.Text;
            buscarProyecto(exbo, edificio);
        }

        private void buscarProyecto(string exbo, string edificio)
        {
            NA_Equipo Nequipo = new NA_Equipo();
            DataSet datosEquipo = Nequipo.MostrarAllEquipoSeguimiento2(exbo, edificio);
            gv_equipos.DataSource = datosEquipo;
            gv_equipos.DataBind();
        }

        protected void gv_equipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string exbo = gv_equipos.SelectedRow.Cells[2].Text;
            mostrarDatosAPagar_duranteTodalaGestion(exbo);
            OnCheckedChanged_seleccionTodo();
        }

        private void mostrarDatosAPagar_duranteTodalaGestion(string exbo)
        {
            NA_Seguimiento seg = new NA_Seguimiento();
            DataSet tuplas = seg.getDatosaPagar_duranteTodaslasGestiones(exbo);
            gv_seguiMes.DataSource = tuplas;
            gv_seguiMes.DataBind();
        }

        protected void OnCheckedChanged_seleccionTodo()
        {
            //Loop through all rows in GridView
            foreach (GridViewRow row in gv_seguiMes.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 8; i<row.Cells.Count; i++ )
                    {                    
                    row.RowState = DataControlRowState.Edit;
                    row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Visible = false;
                    string monto = row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Text;
                    row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Visible = true;
                    row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().BackColor = Color.Yellow;
                    row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Text = monto;
                    }
                }
            }            
        }

        /*
        private void cargarSeguimientoEquipo(int codigoEquipoExbo)
        {
            gv_seguiMes.SelectedIndex = -1;
            NA_Seguimiento Nsegui = new NA_Seguimiento();
            DataSet mostrarTabla = Nsegui.mostrarSeguimiento(codigoEquipoExbo);
            gv_seguiMes.DataSource = mostrarTabla;
            gv_seguiMes.DataBind();

            gv_seguiMes.DataSource = null;
            gv_seguiMes.DataBind();

            gv_recibos.DataSource = null;
            gv_recibos.DataBind();
        }
        */
        private void cargarSeguimientoMes(int codSeguimientoAux)
        {
            gv_seguiMes.SelectedIndex = -1;
            NA_DetalleSeguimiento NdetalleSegui = new NA_DetalleSeguimiento();
            DataSet datosTabla = NdetalleSegui.MostrarAllDatos(codSeguimientoAux);
            gv_seguiMes.DataSource = datosTabla;
            gv_seguiMes.DataBind();

           // gv_recibos.DataSource = null;
           // gv_recibos.DataBind();
        }

        protected void bt_pagar_Click(object sender, EventArgs e)
        {
            hacerElPagodelEquipo();
        }

        private void hacerElPagodelEquipo()
        {            
            int codMoneda = int.Parse(dd_moneda.SelectedValue.ToString());
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            string codUserVendedor = Nresp.getCodUsuarioVendedor(codUser);
            string fechaPago = convertidorFecha(tx_FechaGeneral.Text);
            pagarTodoenBolivianosODolares(codMoneda, codUser, usuarioAux, codUserVendedor, fechaPago);

        }

        protected bool tieneAlgunPagoSeleccionado()
        {
            bool bandera = false;
            foreach (GridViewRow row in gv_seguiMes.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {                  
                        row.RowState = DataControlRowState.Edit;
                         bool isChecked = row.Cells[1].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                         if (isChecked == true) {
                             bandera = true;
                             break;
                         }                        
                }
                if (bandera == true)
                    break;
            }
            return bandera;
        }

        protected String get_detallesdelosmesesSeleccionados()
        {
            string detalle = "(";
            foreach (GridViewRow row in gv_seguiMes.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.RowState = DataControlRowState.Edit;                    
                         bool isChecked = row.Cells[1].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                         if (isChecked == true) {
                             string mes = row.Cells[5].Text;
                             string year = row.Cells[4].Text;
                             string factura = row.Cells[10].Controls.OfType<TextBox>().FirstOrDefault().Text;
                             detalle = detalle + mes + "/" + year + " FAC" + factura + ",";
                         }
                }
                
            }
            detalle = detalle + ")";
            return detalle;
        }

        protected String get_detallesdelosmesesSeleccionados2()
        {
            string detalle = "(";
            foreach (GridViewRow row in gv_seguiMes.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.RowState = DataControlRowState.Edit;
                    bool isChecked = row.Cells[1].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked == true)
                    {
                        string mes = row.Cells[5].Text;
                        string year = row.Cells[4].Text;
                       // string factura = row.Cells[10].Controls.OfType<TextBox>().FirstOrDefault().Text;
                       // detalle = detalle + mes + "/" + year + " FAC" + factura + ",";
                        detalle = detalle + mes + "/" + year +",";
                    }
                }

            }
            detalle = detalle + ")";
            return detalle;
        }

        private void pagarTodoenBolivianosODolares(int tipoMoneda ,int codUser, string nombreUser, string codVendedor, string fechaPago)
        {
            string baseDatos = Session["BaseDatos"].ToString();
            NA_Seguimiento nseg = new NA_Seguimiento();
            if(tieneAlgunPagoSeleccionado() && dd_cuentaBanco.SelectedIndex > 0){
                NEquipo neq = new NEquipo();
                
                string edificio = gv_equipos.SelectedRow.Cells[3].Text;
                string exbo = gv_equipos.SelectedRow.Cells[2].Text;
                string CLIENTECOD = neq.get_codigoClienteSimec(exbo);
                //----------------Nombre Cliente Simec--------------
                DA_inventario inv = new DA_inventario();
                string NombreClienteSimec = inv.getNombreClienteSimec(CLIENTECOD);
                //--------------------------------------------------
               
                // string GLOSA = edificio + "_" + exbo + "_MTTO" + get_detallesdelosmesesSeleccionados();                
                string GLOSA = NombreClienteSimec + " MTTO_" + get_detallesdelosmesesSeleccionados2();                
                double tipoCambio = (double) Math.Round((double)double.Parse(tx_tipoCambio.Text),2);
                int codMoneda = int.Parse(dd_moneda.SelectedValue.ToString());
                bool bcobranza = nseg.generar_Cobranza(GLOSA, false, tipoCambio, codMoneda, codUser, nombreUser, CLIENTECOD, codVendedor, fechaPago);
                int codCobranza =nseg.get_ultimocobroIngresado();
                ///--------simec dato---------
            //    string DOCUM = nseg.getDocumUltimoInsertado();
            //    string CBTE = nseg.get_UltimoNumContabilidad_Glosa(baseDatos);                                
                //--------hacer los recibos de cada uno de los pagos
                int codSeguimiento = 0;
                double montoTotal = 0;
                int i = 0;
                if(bcobranza == true){                    
                    foreach (GridViewRow row in gv_seguiMes.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            row.RowState = DataControlRowState.Edit;
                            bool isChecked = row.Cells[1].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                            if (isChecked == true)
                            {
                                i++;
                                string detalle = NombreClienteSimec+" MTTO ";
                                int codSeg = int.Parse(row.Cells[2].Text);
                                int codMe = int.Parse(row.Cells[3].Text);
                                string mes = row.Cells[5].Text;
                                string year = row.Cells[4].Text;

                                double montoPago = double.Parse(row.Cells[8].Controls.OfType<TextBox>().FirstOrDefault().Text.Replace('.', ','));
                                string cuentaBancoAux = dd_cuentaBanco.SelectedItem.Text;
                                NA_banco bb = new NA_banco();
                                string codigoCuentaBancaria = bb.get_CodigoCuentaBancaria_Haber(cuentaBancoAux);
                                string banco = cuentaBancoAux;
                                string nrocheque = row.Cells[9].Controls.OfType<TextBox>().FirstOrDefault().Text;
                                string factura = row.Cells[10].Controls.OfType<TextBox>().FirstOrDefault().Text;
                                string recibo = row.Cells[11].Controls.OfType<TextBox>().FirstOrDefault().Text;

                                detalle = detalle + mes + "/" + year + " FAC" + factura;
                                bool efectivo = false;
                                if (dd_tipoPago.SelectedValue.ToString() == "EF")
                                    efectivo = true;

                                bool deposito = false;
                                if (dd_tipoPago.SelectedValue.ToString() == "CH")
                                    deposito = true;

                                bool transferencia = false;
                                if (dd_tipoPago.SelectedValue.ToString() == "TF")
                                    transferencia = true;

                                string TipoPago = dd_tipoPago.SelectedValue.ToString();
                                bool debe_bool = false;
                                bool modificado = nseg.modificarMotoPago_DolaresBolivianos(codSeg, codMe, "DOCUM", "CBTE", montoPago, tipoMoneda, tipoCambio, codUser, codCobranza, detalle, efectivo, deposito, nrocheque, banco, factura, recibo, TipoPago, CLIENTECOD, codVendedor, i, debe_bool, baseDatos, codigoCuentaBancaria, transferencia, fechaPago);
                                codSeguimiento = codSeg;
                                montoTotal = montoTotal + montoPago;
                            }
                        }
                    }

                }

                if(codCobranza > 0){
                    int meseslimitePermitidodeRetrazo = NA_VariablesGlobales.meseslimitesdeAtrazadosPermitidosMantenimiento;
                    i++;
                    string cuentaBancoAux = dd_cuentaBanco.SelectedItem.Text;
                   /* NA_banco bb = new NA_banco();
                    string codigoCuentaBancaria = bb.get_CodigoCuentaBancaria_debe(cuentaBancoAux);
                    bool banderaSW2 = nseg.insertCobranza_Conta(CBTE, codigoCuentaBancaria, GLOSA, tipoCambio, i, tipoMoneda, montoTotal, true, baseDatos); */
                    bool banderaSW = nseg.modificarEstadoMantenimiento_CriticoMantenimiento_MantenimientoCritico(codSeguimiento, exbo, edificio, meseslimitePermitidodeRetrazo, codUser);                    
                    Session["CodigoCobranza"] = codCobranza.ToString();
                    string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                    Response.Redirect(ruta + "/Presentacion/FA_ReporteCobranza2.aspx");                
                }
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccione el pago o  la cuenta de banco') </script>");
        }

      

           
    }
}