using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FACorpal_RecibioMaterialInsumos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(126) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                mostarlasSolicitudesdeCompradeInsumos("","Comprado");
            }

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

        private void mostarlasSolicitudesdeCompradeInsumos(string responsableSolicitud, string estado)
        {
            NCorpal_PedidoMaterialeInsumos np = new NCorpal_PedidoMaterialeInsumos();
            DataSet tuplas = np.get_solicitudesMaterialeseInsumos(responsableSolicitud, estado);
            gv_MaterialSolicitado.DataSource = tuplas;
            gv_MaterialSolicitado.DataBind();
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
            string responsable = tx_responsableSolicitud.Text;
            string estadoSolicitud = dd_estadoSolicitud.SelectedItem.Text;
            mostarlasSolicitudesdeCompradeInsumos(responsable, estadoSolicitud);
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_responsableSolicitud.Text = "";
            gv_MaterialSolicitado.SelectedIndex = -1;
            gv_DatosItem.DataSource = null;
            gv_DatosItem.DataBind();
        }

        protected void gv_MaterialSolicitado_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecciondeDatosComprado();
        }

        private void selecciondeDatosComprado()
        {
            if (gv_MaterialSolicitado.SelectedIndex > -1)
            {
                int codigoPedido;
                int.TryParse(gv_MaterialSolicitado.SelectedRow.Cells[1].Text, out codigoPedido);
                NCorpal_PedidoMaterialeInsumos np = new NCorpal_PedidoMaterialeInsumos();
                DataSet tuplasItem = np.get_todosItemInsumosComprados(codigoPedido);
                gv_DatosItem.DataSource = tuplasItem;
                gv_DatosItem.DataBind();

                string responsableSolicitud = gv_MaterialSolicitado.SelectedRow.Cells[4].Text;
                //string estadoSolicitud = gv_MaterialSolicitado.SelectedRow.Cells[6].Text;
                tx_responsableSolicitud.Text = responsableSolicitud;
                //dd_estadoSolicitud.SelectedValue = estadoSolicitud;
            }
        }

        protected void bt_actualizar_Click(object sender, EventArgs e)
        {
            actualizarDatosCantidadRecibidas();
        }

        private void actualizarDatosCantidadRecibidas()
        {
            if (gv_MaterialSolicitado.SelectedIndex > -1)
            {
                NCorpal_PedidoMaterialeInsumos np = new NCorpal_PedidoMaterialeInsumos();
                int codigoPedido;
                int.TryParse(gv_MaterialSolicitado.SelectedRow.Cells[1].Text, out codigoPedido);
                float cantidadCompradaTotal = 0;
                float cantidadRecibidaTotal = 0;

                foreach (GridViewRow row in gv_DatosItem.Rows)
                {
                    int codigoItem = int.Parse(row.Cells[0].Text);

                    float cantidadComprada;
                    float.TryParse(row.Cells[5].Text.Replace('.', ','), out cantidadComprada);

                    float cantidadRecibido;
                    TextBox tx_cantidadRecibida = row.Cells[6].FindControl("tx_cantidadRecibida") as TextBox;
                    float.TryParse(tx_cantidadRecibida.Text.Replace('.', ','), out cantidadRecibido);

                    cantidadCompradaTotal = cantidadCompradaTotal + cantidadComprada;
                    cantidadRecibidaTotal = cantidadRecibidaTotal + cantidadRecibido;

                    bool bb = np.update_RecibirInsumosMaterial(codigoItem, cantidadRecibido);                    
                }

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codresponsableRecibido = Nresp.getCodUsuario(usuarioAux, passwordAux);
                string ResponsableRecibido = Nresp.get_responsable(codresponsableRecibido).Tables[0].Rows[0][1].ToString();

                string estadoCompra = "Cerrado"; 
                if(cantidadRecibidaTotal < cantidadCompradaTotal){
                    estadoCompra = "Entrega Parcial";
                }
                bool cerrado = np.update_cerrarMaterialInsumosRecibido(codigoPedido, estadoCompra, codresponsableRecibido, ResponsableRecibido);
                if (cerrado == true)
                {
                    mostarlasSolicitudesdeCompradeInsumos("",estadoCompra);
                    limpiarDatos();
                    Session["ReporteGeneral"] = "Reporte_RecibidoMaterialInsumos";
                    Session["codigoRecibidoMaterialeInsumos"] = codigoPedido;
                    Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");
                    // Session["codigoEntregaSolicitudProducto"] = codigoSolicitud;
                    // Response.Redirect("../Presentacion/FCorpal_ReporteEntregaSolicitudProducto.aspx");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Guardado') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccionar Solicitud') </script>");
        }
    }
}