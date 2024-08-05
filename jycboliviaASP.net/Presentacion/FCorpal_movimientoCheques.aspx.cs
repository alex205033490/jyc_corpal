using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.IO;
using System.Globalization;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_movimientoCheques : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(42) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            lb_titulo.Text = "Movimientos de Cheques " + Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {               
                cargarBancos();
                mostrarMovimientoCheques("");
                negartodo();
                habilitarBotones();
             //   cargarBancosImpresion();
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


        private void habilitarBotones()
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
                        if(codPermiso == 43){
                            bt_eliminar.Enabled = true;
                            bt_modificar.Enabled = true;
                            bt_guardar.Enabled = true;
                        }
                    }
             }           
        }

        private void negartodo()
        {
            bt_eliminar.Enabled = false;
            bt_modificar.Enabled = false;
            bt_guardar.Enabled = false;
            tx_Dolares.Enabled = false;
            tx_tipoCuenta.Enabled = false;
        }

        public string aFecha(string fecha)
        {
            if (fecha == "" || fecha == "&nbsp;")
            {
                return fecha = "null";
            }

            else
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = "'" + anio + "/" + mes + "/" + dia + "'";
                return _fecha;
            }

        }

        public void mostrarMovimientoCheques(string NroCuenta) {
            NA_movimientoCheques Nmovi = new NA_movimientoCheques();
            DataSet datos = Nmovi.mostrarMovimientosCheques(NroCuenta);
            gv_movimientosCheques.DataSource = datos;
            gv_movimientosCheques.DataBind();
        }

        private void cargarBancos()
        {
            NA_banco Nbanco = new NA_banco();
            dd_banco.DataSource = Nbanco.mostrarBancos();
            dd_banco.DataValueField = "codigo";
            dd_banco.DataTextField = "nombre";
            dd_banco.Items.Add(new ListItem("", "-1"));
            dd_banco.AppendDataBoundItems = true;
            dd_banco.SelectedIndex = -1;
            dd_banco.DataBind();
        }


        private void cargarBancosImpresion()
        {
            NA_banco Nbanco = new NA_banco();
            dd_bancoCheque.DataSource = Nbanco.mostrarBancos();
            dd_bancoCheque.DataValueField = "codigo";
            dd_bancoCheque.DataTextField = "nombre";
            dd_bancoCheque.Items.Add(new ListItem("", "-1"));
            dd_bancoCheque.AppendDataBoundItems = true;
            dd_bancoCheque.SelectedIndex = -1;
            dd_bancoCheque.DataBind();
        }

        protected void bt_guardar_Click(object sender, EventArgs e)
        {
            guardarMovimientoCheques();
            mostrarMovimientoCheques("");
            limpiar();
        }

        private void guardarMovimientoCheques()
        {
            string nroCheque = tx_nroCheque.Text;
            string monto = tx_monto.Text.Replace(',', '.');
            bool transferencia = cb_tranferencia.Checked;
            string detalle = tx_detalle.Text;
            string destino = tx_destino.Text;
            int codcuentaBancaria = Convert.ToInt32(dd_CuentaBancaria.SelectedValue);
           
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            string fecha = aFecha(tx_fecha.Text);
            string titular = tx_titular.Text;

            NA_movimientoCheques Nmovi = new NA_movimientoCheques();
            if (Nmovi.insertarMovimientoCheques(fecha, nroCheque, monto, detalle, transferencia, destino, codcuentaBancaria, CodUser, titular))
            {
                //----------------historial-------------
                NA_Historial nhistorial = new NA_Historial();
                nhistorial.insertar(CodUser, "Se ha insertado un movimiento de cheque con el nro " + nroCheque);
                //--------------------------------------
                Response.Write("<script type='text/javascript'> alert('El Dato Ha sido Guardado Correctamente') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Al Guardar, codigoUser= "+CodUser+"') </script>");

        }

        protected void dd_banco_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codigoBanco = Convert.ToInt32(dd_banco.SelectedValue);
            cargarCuentasBancarias(codigoBanco);
        }

        private void cargarCuentasBancarias(int banco)
        {
            dd_CuentaBancaria.Items.Clear();

            NA_banco Nbanco = new NA_banco();
            dd_CuentaBancaria.DataSource = Nbanco.mostrarCuentasBancarias(banco);
            dd_CuentaBancaria.DataValueField = "codigo";
            dd_CuentaBancaria.DataTextField = "nombre";
            dd_CuentaBancaria.Items.Add(new ListItem("", "-1"));
            dd_CuentaBancaria.AppendDataBoundItems = true;
            dd_CuentaBancaria.SelectedIndex = -1;
            dd_CuentaBancaria.DataBind();
        }

        protected void dd_CuentaBancaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            vertiposcuentas();
        }

        private void vertiposcuentas()
        {
            NA_banco Nbanco = new NA_banco();
            int codcuentaBancaria = Convert.ToInt32(dd_CuentaBancaria.SelectedValue); 
            DataSet dato = Nbanco.mostrarCuentasBancarias2(codcuentaBancaria);
           if(dato.Tables[0].Rows.Count > 0){
               tx_Dolares.Text = dato.Tables[0].Rows[0][3].ToString();
               tx_tipoCuenta.Text = dato.Tables[0].Rows[0][4].ToString();
           }
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            mostrarMovimientoCheques(dd_CuentaBancaria.SelectedValue); 
        }

        protected void gv_movimientosCheques_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarDatos();
        }

        private void seleccionarDatos()
        {
            NA_banco nbanco = new NA_banco();
            NumberFormatInfo nfi = new CultureInfo("es-BO", false).NumberFormat;

            if (!gv_movimientosCheques.SelectedRow.Cells[2].Text.Equals("&nbsp;"))
            {
                tx_fecha.Text = gv_movimientosCheques.SelectedRow.Cells[2].Text;
            }
            else
                tx_fecha.Text = "";

            if (!gv_movimientosCheques.SelectedRow.Cells[3].Text.Equals("&nbsp;"))
            {
                tx_nroCheque.Text = gv_movimientosCheques.SelectedRow.Cells[3].Text;
            }
            else
                tx_nroCheque.Text = "";

            if (!gv_movimientosCheques.SelectedRow.Cells[4].Text.Equals("&nbsp;"))
            {
                tx_monto.Text = Convert.ToSingle(gv_movimientosCheques.SelectedRow.Cells[4].Text).ToString("0.00");
            }
            else 
                tx_monto.Text = "";

            if (!gv_movimientosCheques.SelectedRow.Cells[5].Text.Equals("&nbsp;"))
            {
                tx_detalle.Text = HttpUtility.HtmlDecode(gv_movimientosCheques.SelectedRow.Cells[5].Text);
            }
            else
                tx_detalle.Text = "";

            
         //   cb_tranferencia.Checked = Convert.ToBoolean(gv_movimientosCheques.SelectedRow.Cells[6].Text);

            if (!gv_movimientosCheques.SelectedRow.Cells[7].Text.Equals("&nbsp;"))
            {
                tx_destino.Text = gv_movimientosCheques.SelectedRow.Cells[7].Text;
            }
            else
                tx_destino.Text = "";
            
            string banco = gv_movimientosCheques.SelectedRow.Cells[9].Text;
            dd_banco.SelectedValue = nbanco.getcodigoBanco(banco).ToString();
            cargarCuentasBancarias(nbanco.getcodigoBanco(banco));

            dd_CuentaBancaria.SelectedValue = nbanco.getcodigoCuenta(gv_movimientosCheques.SelectedRow.Cells[8].Text).ToString();

            if (!gv_movimientosCheques.SelectedRow.Cells[10].Text.Equals("&nbsp;"))
            {
                tx_Dolares.Text = gv_movimientosCheques.SelectedRow.Cells[10].Text;
            }
            else
                tx_Dolares.Text = "";


            if (!gv_movimientosCheques.SelectedRow.Cells[11].Text.Equals("&nbsp;"))
            {
                tx_tipoCuenta.Text = gv_movimientosCheques.SelectedRow.Cells[11].Text;
            }
            else
                tx_tipoCuenta.Text = "";

            if (!gv_movimientosCheques.SelectedRow.Cells[12].Text.Equals("&nbsp;"))
            {
                tx_titular.Text = HttpUtility.HtmlDecode(gv_movimientosCheques.SelectedRow.Cells[12].Text);
            }
            else
                tx_titular.Text = "";

        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            tx_nroCheque.Text = "";
            tx_monto.Text = "0";
            tx_detalle.Text = "";
            cb_tranferencia.Checked = false;
            tx_destino.Text = "";

            dd_banco.SelectedIndex = -1;
            dd_CuentaBancaria.SelectedIndex = -1;

            tx_Dolares.Text = "";
            tx_tipoCuenta.Text = "";
            tx_fecha.Text = "";
            tx_titular.Text = "";
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarDato();
        }

        private void eliminarDato()
        {
           if(gv_movimientosCheques.SelectedIndex > -1){
               int codigoMovimientoCheque = Convert.ToInt32(gv_movimientosCheques.SelectedRow.Cells[1].Text);
               NA_movimientoCheques movi = new NA_movimientoCheques();
               movi.eliminarMovimientoCheques(codigoMovimientoCheque);
               mostrarMovimientoCheques("");
               //----------------historial-------------
               NA_Responsables Nresp = new NA_Responsables();
               string usuarioAux = Session["NameUser"].ToString();
               string passwordAux = Session["passworuser"].ToString();
               int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
               NA_Historial nhistorial = new NA_Historial();
               nhistorial.insertar(CodUser, "Se ha eliminado un movimiento de cheque con el codigo " + codigoMovimientoCheque+ " y nro de cheque "+gv_movimientosCheques.SelectedRow.Cells[3].Text);
               //--------------------------------------
           }
        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            modificarDatos();
            limpiar();
            mostrarMovimientoCheques("");

        }

        private void modificarDatos()
        {
           if(gv_movimientosCheques.SelectedIndex > -1){
               int codigoMovimientoCheque = Convert.ToInt32(gv_movimientosCheques.SelectedRow.Cells[1].Text);
               string nroCheque = tx_nroCheque.Text;
               string monto = tx_monto.Text.Replace(',', '.');
               bool transferencia = cb_tranferencia.Checked;
               string detalle = tx_detalle.Text;
               string destino = tx_destino.Text;
               int codcuentaBancaria = Convert.ToInt32(dd_CuentaBancaria.SelectedValue);

               NA_Responsables Nresp = new NA_Responsables();
               string usuarioAux = Session["NameUser"].ToString();
               string passwordAux = Session["passworuser"].ToString();
               int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
               string fecha = aFecha(tx_fecha.Text);
               string titular = tx_titular.Text;

               NA_movimientoCheques Nmovi = new NA_movimientoCheques();
               Nmovi.modificarmovimientoCheques(codigoMovimientoCheque, fecha, nroCheque, monto, detalle, transferencia, destino, codcuentaBancaria, CodUser,titular);
               //----------------historial-------------
               NA_Historial nhistorial = new NA_Historial();
               nhistorial.insertar(CodUser, "Se ha modificado un movimiento de cheque con el nro " + nroCheque);
               //--------------------------------------
           }
        }

        protected void lkb_excel_Click(object sender, EventArgs e)
        {
            exportarExcel();
        }

        private void exportarExcel()
        {


            NA_movimientoCheques Nmovi = new NA_movimientoCheques();
            DataSet resultado = Nmovi.mostrarMovimientosCheques("");

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Movimientos Cheques - " + Session["BaseDatos"].ToString();
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

        protected void dd_baseDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dpto = dd_baseDatos.SelectedValue.ToString();
            lb_titulo.Text = "Movimientos de Cheques " + dpto;
            //Response.Write("<script type='text/javascript'> alert('Error: " + dpto + "') </script>");
            // Response.Write("<script type='text/javascript'> alert('Error: usuario') </script>");
            cambiarBaseDeDatos(dpto);
            Response.Redirect("../Presentacion/FA_movimientoCheques.aspx");    
        }

        public void cambiarBaseDeDatos(string departamento)
        {
            string BaseDatos = departamento;
            switch (BaseDatos)
            {
                case "Prueba":
                    Session["NombreBaseDatos"] = "db_prueba";
                    Session["BaseDatos"] = "Prueba";
                    break;
                case "Santa Cruz":
                    Session["NombreBaseDatos"] = "db_SantaCruz";
                    Session["BaseDatos"] = "Santa Cruz";
                    break;
                case "La Paz":
                    Session["NombreBaseDatos"] = "db_LaPaz";
                    Session["BaseDatos"] = "La Paz";
                    break;
                case "Cochabamba":
                    Session["NombreBaseDatos"] = "db_Cochabamba";
                    Session["BaseDatos"] = "Cochabamba";
                    break;
                case "Sucre":
                    Session["NombreBaseDatos"] = "db_Sucre";
                    Session["BaseDatos"] = "Sucre";
                    break;
                case "Oruro":
                    Session["NombreBaseDatos"] = "db_Oruro";
                    Session["BaseDatos"] = "Oruro";
                    break;
                case "Potosi":
                    Session["NombreBaseDatos"] = "db_Potosi";
                    Session["BaseDatos"] = "Potosi";
                    break;
                case "Tarija":
                    Session["NombreBaseDatos"] = "db_Tarija";
                    Session["BaseDatos"] = "Tarija";
                    break;
                case "Yacuiba":
                    Session["NombreBaseDatos"] = "db_Yacuiba";
                    Session["BaseDatos"] = "Yacuiba";
                    break;
                case "Villamontes":
                    Session["NombreBaseDatos"] = "db_Villamontes";
                    Session["BaseDatos"] = "Villamontes";
                    break;
                case "Asuncion-Paraguay":
                    Session["NombreBaseDatos"] = "db_Paraguay";
                    Session["BaseDatos"] = "Asuncion-Paraguay";
                    break;
                case "JyC Srl":
                    Session["NombreBaseDatos"] = "db_jycsrl";
                    Session["BaseDatos"] = "JyC Srl";
                    break;
                case "JyCIA Srl":
                    Session["NombreBaseDatos"] = "db_jyciasrl";
                    Session["BaseDatos"] = "JyCIA Srl";
                    break;
                case "Imven":
                    Session["NombreBaseDatos"] = "db_imven";
                    Session["BaseDatos"] = "Imven";
                    break; 
                    
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

        protected void bt_impresionCheque_Click(object sender, EventArgs e)
        {
            imprimirCheques();
        }

        private void imprimirCheques()
        {
            
            if(dd_bancoCheque.SelectedIndex > 0 && dd_monedaCheque.SelectedIndex > 0){
                string banco = dd_bancoCheque.SelectedItem.Text;
                string moneda = dd_monedaCheque.SelectedItem.Text;
               
                Session["montoCheque"] = "**" + tx_monto.Text + "**";
                //Session["numeroCotiRepuesto"] = tx_numeroCoti.Text + "/" + DateTime.Now.ToString("yyyy");
                DateTime fecha = Convert.ToDateTime(tx_fecha.Text);

                Session["lugarfechaCheque"] = "Santa Cruz" + ", " + fecha.ToString("dd") + " de " + fecha.ToString("MMMM") + " de " + fecha.ToString("yyyy");
                N_numLetra nl = new N_numLetra();
                Session["PageseACheque"] = tx_titular.Text;
                //Session["SumaletrasCheque"] = nl.Convertir(tx_monto.Text, false, moneda);
                Session["SumaletrasCheque"] = nl.Convertir(tx_monto.Text, false, "");
                Session["Relleno"] = "******************************";
                Session["departamentoCheque"] = "Santa Cruz";
                Session["diaCheque"] = fecha.ToString("dd");
                Session["mesCheque"] = fecha.ToString("MMMM");
                Session["anioCheque"] = fecha.ToString("yyyy");

                if (banco.Equals("BNB"))
                {
                    Response.Redirect("../Presentacion/FA_ReporteChequeBNB.aspx");
                }
                else {
                    if (banco.Equals("Bisa"))
                        Response.Redirect("../Presentacion/FA_ReporteChequeBisa.aspx");
                    {
                
                }
                
                }


        }
       }
    }
}