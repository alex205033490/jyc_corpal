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
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ActivosJYC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(123) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                tx_montoValorJYC.Enabled = false;
                tx_tipocambio.Enabled = false;
                buscarActivosJYC("", "", "", "", null, null);
                rellenarEstadosSimec();
                rellenarEstadoValorActual();
            }
        }


        private void rellenarEstadoValorActual()
        {
            NA_ActivosJyC nactivos = new NA_ActivosJyC();
            DataSet tuplas = nactivos.get_estadosValorActual();
            dd_tipoValorActual.DataSource = tuplas;
            dd_tipoValorActual.DataValueField = "nombre";
            dd_tipoValorActual.DataTextField = "nombre";
            dd_tipoValorActual.Items.Add(new ListItem("Sin Estado"));
            dd_tipoValorActual.AppendDataBoundItems = true;
            dd_tipoValorActual.SelectedIndex = -1;
            dd_tipoValorActual.DataBind();
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


        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaResponsableSimec(string prefixText, int count)
        {
            string nombreResponsable = prefixText;

            NA_ActivosJyC Nrespon = new NA_ActivosJyC();
            DataSet tuplas = Nrespon.mostrarResponsableSimec(nombreResponsable);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }

        private void rellenarEstadosSimec()
        {
            NA_ActivosJyC nactivos = new NA_ActivosJyC();
            DataSet tuplas = nactivos.get_estadosActivos();
            dd_estadoSimec.DataSource = tuplas;
            dd_estadoSimec.DataValueField = "nombre";
            dd_estadoSimec.DataTextField = "nombre";
            dd_estadoSimec.Items.Add(new ListItem("Sin Estado"));
            dd_estadoSimec.AppendDataBoundItems = true;
            dd_estadoSimec.SelectedIndex = -1;
            dd_estadoSimec.DataBind();
        }

        private void buscarActivosJYC(string detalleActivo, string comprobante, string custodio, string responsableAsignado, string estadoActivo, string EstadoValorActual)
        {
            NA_ActivosJyC nactivos = new NA_ActivosJyC();
            DataSet tuplas = nactivos.get_allActivos(detalleActivo, comprobante, custodio, responsableAsignado, estadoActivo, EstadoValorActual);
            gv_activosJyC.DataSource = tuplas;
            gv_activosJyC.DataBind();
        }

        protected void bt_Actualizar_Click(object sender, EventArgs e)
        {
            string detalleActivo = tx_detalleActivo.Text;
            string comprobante = tx_comprobante.Text;
            string custodio = tx_custodio.Text;
            string responsableAsignado = tx_asignarCustodio.Text;
            string estadoActivo = dd_estadoSimec.SelectedItem.Text;
            string EstadoValorActual = dd_tipoValorActual.SelectedItem.Text;
            buscarActivosJYC(detalleActivo, comprobante, custodio, responsableAsignado, estadoActivo, EstadoValorActual);
        }

        protected void gv_activosJyC_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarDatos();
        }

        private void seleccionarDatos()
        {
            if (gv_activosJyC.SelectedIndex >= 0)
            {
                tx_detalleActivo.Text = HttpUtility.HtmlDecode(gv_activosJyC.SelectedRow.Cells[4].Text);
                tx_comprobante.Text = HttpUtility.HtmlDecode(gv_activosJyC.SelectedRow.Cells[5].Text);
                tx_custodio.Text = HttpUtility.HtmlDecode(gv_activosJyC.SelectedRow.Cells[10].Text);
                tx_montoValorJYC.Text = HttpUtility.HtmlDecode(gv_activosJyC.SelectedRow.Cells[20].Text);

                if (!string.IsNullOrEmpty(gv_activosJyC.SelectedRow.Cells[15].Text) && !(gv_activosJyC.SelectedRow.Cells[15].Text.Equals("&nbsp;")))
                {
                    tx_asignarCustodio.Text = HttpUtility.HtmlDecode(gv_activosJyC.SelectedRow.Cells[15].Text);
                }
                else
                    tx_asignarCustodio.Text = "";

                string EstadoActivo = gv_activosJyC.SelectedRow.Cells[2].Text;
                dd_estadoSimec.SelectedValue = EstadoActivo;


                if (!string.IsNullOrEmpty(gv_activosJyC.SelectedRow.Cells[18].Text) && !(gv_activosJyC.SelectedRow.Cells[18].Text.Equals("&nbsp;")))
                {
                    tx_valorSimec.Text = HttpUtility.HtmlDecode(gv_activosJyC.SelectedRow.Cells[18].Text);
                }
                else
                    tx_valorSimec.Text = "0";

                string TipoActualValos = gv_activosJyC.SelectedRow.Cells[21].Text;
                dd_tipoValorActual.SelectedValue = TipoActualValos;

                string ubicacion = gv_activosJyC.SelectedRow.Cells[22].Text;
                if (!string.IsNullOrEmpty(ubicacion) && !(ubicacion.Equals("&nbsp;")))
                {
                    dd_ubicacionUNE.SelectedValue = ubicacion;
                }
                else
                    dd_ubicacionUNE.SelectedIndex = -1;
                

                bool dadobaja = (gv_activosJyC.SelectedRow.Cells[23].Controls[0] as CheckBox).Checked;
                cb_dadodebaja.Checked = dadobaja;

                tx_observacion.Text = HttpUtility.HtmlDecode(gv_activosJyC.SelectedRow.Cells[24].Text);

                bool noaplica = (gv_activosJyC.SelectedRow.Cells[25].Controls[0] as CheckBox).Checked;
                cbx_noAplica.Checked = noaplica;
                if (noaplica == true)
                {
                    tx_montoValorJYC.Enabled = true;
                    tx_tipocambio.Enabled = true;
                }
                else
                {
                    tx_montoValorJYC.Enabled = false;
                    tx_tipocambio.Enabled = false;
                }

            }
        }

        protected void bt_actualizar_Click1(object sender, EventArgs e)
        {
            actualizardatos();
        }

        private void actualizardatos()
        {
            if (gv_activosJyC.SelectedIndex >= 0)
            {
                int codigoActivo;
                int.TryParse(gv_activosJyC.SelectedRow.Cells[1].Text, out codigoActivo);
                string detalleActivo = tx_detalleActivo.Text;
                string comprobante = tx_comprobante.Text;
                string custodio = tx_custodio.Text;
                string responsableAsignado = tx_asignarCustodio.Text;
                string estadoActivo = dd_estadoSimec.SelectedItem.Text;
                ///------------usuario-----------------
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                //-------------------------------------
                int codUserRespAsignado = Nresp.getCodigo_NombreResponsable(responsableAsignado);
                NA_ActivosJyC nactivos = new NA_ActivosJyC();
                int CodestadoActivo = nactivos.get_codigoEstadoActivo(estadoActivo);
                bool noAplica = cbx_noAplica.Checked;
                float montoValornoAplica;
                float.TryParse(tx_montoValorJYC.Text.Trim().Replace('.', ','), out montoValornoAplica);
                float tipocambio;
                float.TryParse(tx_tipocambio.Text.Replace('.', ','), out tipocambio);

                string ubicacion_une = dd_ubicacionUNE.SelectedItem.Text;

                bool bajaactivo = cb_dadodebaja.Checked;
                string observaciones = tx_observacion.Text;

                bool bandera = nactivos.update_Activos(codigoActivo, codUserRespAsignado, codUser, CodestadoActivo, noAplica, montoValornoAplica, tipocambio, ubicacion_une, bajaactivo, observaciones);
                if (bandera == true)
                {
                    limpiarDatos();
                    buscarActivosJYC(detalleActivo, comprobante.Trim(), custodio.Trim(), "", "", "");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Guardado') </script>");

            }
        }

        private void limpiarDatos()
        {
            tx_detalleActivo.Text = "";
            tx_comprobante.Text = "";
            tx_custodio.Text = "";
            tx_asignarCustodio.Text = "";
            dd_estadoSimec.SelectedIndex = -1;
            cbx_noAplica.Checked = false;
            tx_montoValorJYC.Text = "0";
            dd_tipoValorActual.SelectedIndex = -1;
            dd_ubicacionUNE.SelectedIndex = -1;
            cb_dadodebaja.Checked = false;
            tx_observacion.Text = "";
            tx_valorSimec.Text = "";
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        protected void cbx_noAplica_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_noAplica.Checked == true)
            {
                tx_montoValorJYC.Enabled = true;
                tx_tipocambio.Enabled = true;
            }
            else
            {
                tx_montoValorJYC.Enabled = false;
                tx_tipocambio.Enabled = false;
            }
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {
            exportarExcelDatos();
        }

        private void exportarExcelDatos()
        {
            string detalleActivo = tx_detalleActivo.Text;
            string comprobante = tx_comprobante.Text;
            string custodio = tx_custodio.Text;
            string responsableAsignado = tx_asignarCustodio.Text;
            string estadoActivo = dd_estadoSimec.SelectedItem.Text;
            string EstadoValorActual = dd_tipoValorActual.SelectedItem.Text;

            NA_ActivosJyC nactivos = new NA_ActivosJyC();
            DataSet tuplas = nactivos.get_allActivos(detalleActivo, comprobante, custodio, responsableAsignado, estadoActivo, EstadoValorActual);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Activos JyC - " + Session["BaseDatos"].ToString();
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



    }
}