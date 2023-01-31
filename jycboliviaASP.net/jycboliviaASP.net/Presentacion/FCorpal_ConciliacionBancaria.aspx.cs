using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using jycboliviaASP.net.Negocio;
using System.Configuration;


namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ConciliacionBancaria : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(41) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if (!IsPostBack) {
                DataTable tablaCheque = new DataTable();
                tablaCheque.Columns.Add("Fecha", typeof(string));
                tablaCheque.Columns.Add("Nro. Cheques", typeof(string));
                tablaCheque.Columns.Add("Monto", typeof(string));

                gv_chequesCirculacion.DataSource = tablaCheque;
                gv_chequesCirculacion.DataBind();
                Session["listacheques"] = tablaCheque;

                tx_cuentaview.Enabled = false;
                tx_tipoCuentaview.Enabled = false;

                cargarBancos();
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
                string _fecha = "'"+anio + "/" + mes + "/" + dia+"'";
                return _fecha;
            }

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


        protected void tx_Adicionar_Click(object sender, EventArgs e)
        {
            DataTable tablaCheque = Session["listacheques"] as DataTable;
           
            DataRow fila = tablaCheque.NewRow();
            fila["Fecha"] = tx_FechaCheque.Text;
            fila["Nro. Cheques"] = tx_nrocheque.Text;
            fila["Monto"] = tx_monto.Text.Replace(".", ",");
            tablaCheque.Rows.Add(fila);
            tablaCheque.AcceptChanges();
            gv_chequesCirculacion.DataSource = tablaCheque;
            gv_chequesCirculacion.DataBind();
            limpiar();
            calcular();
            
        }

        public void calcular() {
            DataTable tablaCheque = Session["listacheques"] as DataTable;
            int cantfilas = tablaCheque.Rows.Count;
            float sumaTotal = Convert.ToSingle("0.0"); 

            for (int i = 0; i < cantfilas; i++)
            {
                float monto = Convert.ToSingle(tablaCheque.Rows[i]["Monto"].ToString());
                sumaTotal = sumaTotal + monto;
            }

            DataTable tablaSuma = new DataTable();
            tablaSuma.Columns.Add("X", typeof(string));
            tablaSuma.Columns.Add("Debe_JyC", typeof(float));
            tablaSuma.Columns.Add("Haber_JyC", typeof(float));
            tablaSuma.Columns.Add("Debe_Banco", typeof(float));
            tablaSuma.Columns.Add("Haber_Banco", typeof(float));
            
            DataRow fila = tablaSuma.NewRow();
            fila["X"] = "SUMAS";
            fila["Debe_JyC"] = Convert.ToSingle(tx_saldoAnterior.Text.Replace(".",","));
            fila["Haber_JyC"] = Convert.ToSingle("0");
            fila["Debe_Banco"] = Convert.ToSingle(sumaTotal);
            fila["Haber_Banco"] = Convert.ToSingle(tx_extractoBancario.Text.Replace(".", ","));
            tablaSuma.Rows.Add(fila);
            tablaSuma.AcceptChanges();

            DataRow fila2 = tablaSuma.NewRow();
            fila2["X"] = "SALDO CORRECTO";
            fila2["Debe_JyC"] = Convert.ToSingle("0");
            fila2["Haber_JyC"] = Convert.ToSingle(fila["Debe_JyC"]) - Convert.ToSingle(fila["Haber_JyC"]);
            fila2["Debe_Banco"] = Convert.ToSingle(fila["Haber_Banco"]) - Convert.ToSingle(fila["Debe_Banco"]);
            fila2["Haber_Banco"] = Convert.ToSingle("0");
            tablaSuma.Rows.Add(fila2);
            tablaSuma.AcceptChanges();

            DataRow fila3 = tablaSuma.NewRow();
            fila3["X"] = "SUMAS IGUALES";
            fila3["Debe_JyC"] = Convert.ToSingle(fila["Debe_JyC"]) + Convert.ToSingle(fila2["Debe_JyC"]);
            fila3["Haber_JyC"] = Convert.ToSingle(fila["Haber_JyC"]) + Convert.ToSingle(fila2["Haber_JyC"]);
            fila3["Debe_Banco"] = Convert.ToSingle(fila["Debe_Banco"]) + Convert.ToSingle(fila2["Debe_Banco"]);
            fila3["Haber_Banco"] = Convert.ToSingle(fila["Haber_Banco"]) + Convert.ToSingle(fila2["Haber_Banco"]);
            tablaSuma.Rows.Add(fila3);
            tablaSuma.AcceptChanges();

            gv_calculos.DataSource = tablaSuma;
            gv_calculos.DataBind();
        }

        private void limpiar()
        {
            tx_FechaCheque.Text = "";
            tx_nrocheque.Text = "";
            tx_monto.Text = "";
        }

        private void limpiarTodo() {
            tx_FechaCheque.Text = "";
            tx_nrocheque.Text = "";
            tx_monto.Text = "";

            tx_saldoAnterior.Text = "";
            tx_extractoBancario.Text = "";

            tx_tipoCuentaview.Text = "";
            tx_cuentaview.Text = "";

            gv_chequesCirculacion.DataSource = null;
            gv_chequesCirculacion.DataBind();
        }

        protected void bt_calcular_Click(object sender, EventArgs e)
        {
            calcular();
        }

        protected void gv_chequesCirculacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {            
            int index = e.RowIndex;

            DataTable tablaCheque = Session["listacheques"] as DataTable;
            tablaCheque.Rows[index].Delete();
            tablaCheque.AcceptChanges();

            gv_chequesCirculacion.EditIndex = -1;

            gv_chequesCirculacion.DataSource = tablaCheque;
            gv_chequesCirculacion.DataBind();
            Session["listacheques"] = tablaCheque;

            calcular();
         
        }

        protected void dd_banco_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codigoBanco = Convert.ToInt32(dd_banco.SelectedValue);
            cargarCuentasBancarias(codigoBanco);
        }

        protected void bt_guardar_Click(object sender, EventArgs e)
        {
            guardarConciliacion();
           // limpiarTodo();
        }



        private void guardarConciliacion()
        {
            string saldoAnterior = tx_saldoAnterior.Text.Replace(',', '.').Replace(" ", "");
            string extractoBancario = tx_extractoBancario.Text.Replace(',', '.').Replace(" ", ""); 
            if (dd_CuentaBancaria.SelectedIndex > -1 && dd_banco.SelectedIndex > -1)
            {
                int codcuentaBancaria = Convert.ToInt32(dd_CuentaBancaria.SelectedValue);
                float n;               

                if (float.TryParse(saldoAnterior,out n) == true)
                {
                    if (float.TryParse(extractoBancario,out n) == true)
                    {
                            NA_Responsables Nresp = new NA_Responsables();
                            string usuarioAux = Session["NameUser"].ToString();
                            string passwordAux = Session["passworuser"].ToString();
                            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                        NA_banco Nbanco = new NA_banco();
                       if(!Nbanco.tieneConciliacionBancaria(codcuentaBancaria)){
                           Nbanco.insertconciliacionBancaria(saldoAnterior, extractoBancario, codcuentaBancaria, CodUser);
                           int codultimaConciliacionBancaria = Nbanco.getultimaConciliacion();

                           DataTable tablaCheque = Session["listacheques"] as DataTable;
                           int cantfilas = tablaCheque.Rows.Count;
                           for (int i = 0; i < cantfilas; i++)
                           {
                               string fechaCheque = aFecha(tablaCheque.Rows[i]["Fecha"].ToString());
                               string NroCheque = tablaCheque.Rows[i]["Nro. Cheques"].ToString();
                               string montoCheque = tablaCheque.Rows[i]["Monto"].ToString().Replace(',', '.').Replace(" ", "");
                               Nbanco.insertChequeCirculacion(fechaCheque, NroCheque, montoCheque, codultimaConciliacionBancaria);
                           }
                           //----------------historial-------------
                           NA_Historial nhistorial = new NA_Historial();
                           nhistorial.insertar(CodUser, "Se ha creado una Conciliacion Bancaria con codigo " + codultimaConciliacionBancaria);
                           //--------------------------------------
                           Response.Write("<script type='text/javascript'> alert('Se ha Guardado Correctamente') </script>");

                           string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                           Response.Redirect(ruta + "/Presentacion/FA_ConciliacionBancaria.aspx");
                       }else
                           Response.Write("<script type='text/javascript'> alert('Error: Esta Cuenta ya tiene Conciliacion en esta fecha') </script>");
                    }else
                        Response.Write("<script type='text/javascript'> alert('Error: No tiene Extracto Bancario') </script>");
                }else
                    Response.Write("<script type='text/javascript'> alert('Error: No tiene Saldo Anterior') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: No ha seleccionado Cuenta Bancaria') </script>");


        }

        protected void dd_CuentaBancaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            vertiposcuentas();
            llenarCheques();
            calcular();
        }

        private void llenarCheques()
        {
            NA_banco Nbanco = new NA_banco();
            int codcuentaBancaria = Convert.ToInt32(dd_CuentaBancaria.SelectedValue);
            DataTable tablaCheque = Session["listacheques"] as DataTable;
            tablaCheque.Clear();
            tablaCheque.AcceptChanges();
            
            DataSet dato = Nbanco.obtenerChequesAnteriores(codcuentaBancaria);
            gv_chequesCirculacion.DataSource = null;
            gv_chequesCirculacion.DataBind();



            if (dato.Tables[0].Rows.Count > 0)
            {  
                for (int i = 0; i < dato.Tables[0].Rows.Count; i++)
                {
                    DataRow fila = tablaCheque.NewRow();
                    fila["Fecha"] = dato.Tables[0].Rows[i][0].ToString();
                    fila["Nro. Cheques"] = dato.Tables[0].Rows[i][1].ToString();
                    fila["Monto"] = dato.Tables[0].Rows[i][2].ToString().Replace(".", ",");
                    tablaCheque.Rows.Add(fila);
                    tablaCheque.AcceptChanges();
                }

                gv_chequesCirculacion.DataSource = tablaCheque;
                gv_chequesCirculacion.DataBind();

            }
        }

        private void vertiposcuentas()
        {
            NA_banco Nbanco = new NA_banco();
            int codcuentaBancaria = Convert.ToInt32(dd_CuentaBancaria.SelectedValue);
            DataSet dato = Nbanco.mostrarCuentasBancarias2(codcuentaBancaria);
            if (dato.Tables[0].Rows.Count > 0)
            {
                tx_cuentaview.Text = dato.Tables[0].Rows[0][3].ToString();
                tx_tipoCuentaview.Text = dato.Tables[0].Rows[0][4].ToString();
            }
        }

      
    }
}