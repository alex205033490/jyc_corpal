using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_VaciadoUponPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(140) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                buscarDatosParaCargarAUpon("");
                string baseDeDatos = Session["BaseDatos"].ToString();
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ha ingresado a Vaciado Pedido Upon");
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

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaClienteP(string prefixText, int count)
        {
            string cliente = prefixText;

            NCorpal_Cliente cc = new NCorpal_Cliente();
            //      DataSet tuplas = Nrespon.mostrarSoloAutorizados_AutoComplit(nombreResponsable,"2,6,7,9,10,11,13");
            DataSet tuplas = cc.get_ClienteNombre(cliente);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }

            return lista;
        }

        private void buscarDatosParaCargarAUpon(string cliente)
        {
            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = nss.get_allPedidosParaVaciarUpon(cliente);
            lb_cantDatos.Text = Convert.ToString(tuplas.Tables[0].Rows.Count);
            gv_datosCobros.DataSource = tuplas;
            gv_datosCobros.DataBind();
        }

        private void anularPedido()
        {
            foreach (GridViewRow row in gv_datosCobros.Rows)
            {
                NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();

                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.RowState = DataControlRowState.Edit;
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked == true)
                    {
                        int CodigoPedido = int.Parse(row.Cells[1].Text);
                        bool bandera = nss.anularPedidoVaciadoUpon(CodigoPedido, isChecked);
                    }
                }
            }
            buscarDatosParaCargarAUpon("");
        }
        protected void bt_anularPago_Click(object sender, EventArgs e)
        {
            anularPedido();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            exceldelaTabla();
        }

        private void exceldelaTabla()
        {
            string cliente =tx_cliente.Text;
            NCorpal_SolicitudEntregaProducto nss = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = nss.get_allPedidosParaVaciarUpon(cliente);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Vaciado Upon Pedido - " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = tuplas;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }

        }
        protected void bt_vaciarAlSimec_Click(object sender, EventArgs e)
        {

        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string cliente = tx_cliente.Text;
            buscarDatosParaCargarAUpon(cliente);
        }
    }
}
    
