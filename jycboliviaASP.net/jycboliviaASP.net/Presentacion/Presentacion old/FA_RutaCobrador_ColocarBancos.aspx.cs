using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_RutaCobrador_ColocarBancos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(92) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
           }

            if (!IsPostBack)
            {   
                cargarCuentasBancos();
                mostrarRecibosPagadosSinBanco("","");
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha ingresado a Gestionar Seguimiento");
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

        private void mostrarRecibosPagadosSinBanco(string edificio, string exbo)
        {
            NA_ReciboPago re = new NA_ReciboPago();
            DataSet datos = re.mostrarReciboPagosSinBancos(edificio, exbo);
            gv_recibosPagos.DataSource = datos;
            gv_recibosPagos.DataBind();
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

        protected void bt_colocarBancos_Click(object sender, EventArgs e)
        {
            ponerBancosEnRecibosMoviles();
            string edificio = tx_edificio1.Text;
            string exbo = tx_exbo1.Text;
            mostrarRecibosPagadosSinBanco(edificio,exbo);
        }

        private void ponerBancosEnRecibosMoviles()
        {
            foreach (GridViewRow row in gv_recibosPagos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.RowState = DataControlRowState.Edit;
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked == true)
                    {
                        int codigoRecibo = int.Parse(row.Cells[1].Text);
                        string cuentaBancoAux = dd_cuentaBanco.SelectedItem.Text;
                        NA_banco bb = new NA_banco();
                        string codigoCuentaBancaria = bb.get_CodigoCuentaBancaria_Haber(cuentaBancoAux);
                        string banco = cuentaBancoAux;

                        NA_ReciboPago nrecibo = new NA_ReciboPago();
                        bool bandera = nrecibo.updateBancosRecibo(codigoRecibo,banco);

                    }
                }
            }
        }

        protected void tb_buscar_Click(object sender, EventArgs e)
        {
            string edificio = tx_edificio1.Text;
            string exbo = tx_exbo1.Text;
            mostrarRecibosPagadosSinBanco(edificio, exbo);
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarTEXT();
        }

        private void limpiarTEXT()
        {
            tx_edificio1.Text = "";
            tx_exbo1.Text = "";
            dd_cuentaBanco.SelectedIndex = -1;
        }

       
    }
}