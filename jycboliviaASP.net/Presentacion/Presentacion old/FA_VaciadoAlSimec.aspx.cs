using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_VaciadoAlSimec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(74) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
           
            if (!IsPostBack)
            {
                cargarTipoCambio();
            //    cargarMonedas();
                buscarDatosParaCargarASimec();
               // dd_moneda.SelectedValue = "2";
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha ingresado a Vaciado Simec");
            }
            
        }

      /*  private void cargarMonedas()
        {
            NA_Seguimiento seg = new NA_Seguimiento();
            DataSet datos = seg.get_Monedas();
            dd_moneda.DataSource = datos;
            dd_moneda.DataValueField = "CODIGO";
            dd_moneda.DataTextField = "MONEDA";
            dd_moneda.AppendDataBoundItems = true;
            dd_moneda.DataBind();
        }
        */
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
            buscarDatosParaCargarASimec();
        }

        private void buscarDatosParaCargarASimec()
        {
            NA_Seguimiento nseg = new NA_Seguimiento();
            DataSet tuplas = nseg.CobrosGeneralesRecibo();
            lb_cantDatos.Text = Convert.ToString(tuplas.Tables[0].Rows.Count);
            gv_datosCobros.DataSource = tuplas;
            gv_datosCobros.DataBind();
        }

    
        protected void bt_vaciarAlSimec_Click(object sender, EventArgs e)
        {
            vaciarPordiaAlSimec();
            buscarDatosParaCargarASimec();
        }

        private void vaciarPordiaAlSimec()
        {
          //  int codMoneda = int.Parse(dd_moneda.SelectedValue.ToString());
            float TC = float.Parse(tx_tipoCambio.Text.Replace('.', ','));            
            string VCAJA = tx_vcaja.Text;

            NA_Responsables Nresp = new NA_Responsables();
            string usuario = Session["NameUser"].ToString();
            string password = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuario, password);

            DataSet tupla = Nresp.get_responsable(codUser);
            string usuarioCobrador = tupla.Tables[0].Rows[0][1].ToString();
            
            string BasedeDatos = Session["BaseDatos"].ToString();            
            string usuarioAux = "JYC";
            string GLOSA_GENERAL = tx_glosa.Text + " Realizado por "+usuarioCobrador;

            NA_Seguimiento nseg = new NA_Seguimiento();
            nseg.vaciarAlSistemaSimecporDia( TC, GLOSA_GENERAL, usuarioAux, VCAJA, BasedeDatos);
        }

        protected void bt_anularPago_Click(object sender, EventArgs e)
        {
            anularPagoCobro();
        }

        private void anularPagoCobro()
        {
            foreach (GridViewRow row in gv_datosCobros.Rows)
            {
                NA_Seguimiento nseg = new NA_Seguimiento();

                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.RowState = DataControlRowState.Edit;
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked == true)
                    {
                        int CodigoCobroRecibo = int.Parse(row.Cells[1].Text);
                       bool bandera = nseg.anularCobroReciboGeneral(CodigoCobroRecibo, isChecked);        
                    }
                }
            }
            buscarDatosParaCargarASimec();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            exceldelaTabla();
        }

        private void exceldelaTabla()
        {

            NA_Seguimiento nseg = new NA_Seguimiento();
            DataSet resultado = nseg.CobrosGeneralesRecibo();            

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Vaciado Simec - " + Session["BaseDatos"].ToString();
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

      
     
      
    }
}