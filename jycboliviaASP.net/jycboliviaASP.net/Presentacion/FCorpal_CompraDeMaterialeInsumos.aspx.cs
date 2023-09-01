using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Configuration;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CompraDeMaterialeInsumos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(125) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                mostarlasSolicitudesdeCompradeInsumos("","Abierto");
            }
           /* NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NCorpal_SolicitudEntregaProducto ncc = new NCorpal_SolicitudEntregaProducto();
            tx_responsableSolicitud.Text = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
            */
            
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

        private void mostarlasSolicitudesdeCompradeInsumos(string responsableSolicitud, string estadoSolicitud)
        {
            NCorpal_PedidoMaterialeInsumos np = new NCorpal_PedidoMaterialeInsumos();
            DataSet tuplas = np.get_solicitudesMaterialeseInsumos(responsableSolicitud,estadoSolicitud);
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

        protected void gv_MaterialSolicitado_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarDatosdePedidodeMaterialeInsumos();
        }

        private void seleccionarDatosdePedidodeMaterialeInsumos()
        {
            if(gv_MaterialSolicitado.SelectedIndex > -1){
                int codigoPedido;
                int.TryParse(gv_MaterialSolicitado.SelectedRow.Cells[1].Text, out codigoPedido);
                NCorpal_PedidoMaterialeInsumos np = new NCorpal_PedidoMaterialeInsumos();
                DataSet tuplasItem = np.get_todosItemInsumosPedidos(codigoPedido);
                gv_DatosItem.DataSource = tuplasItem;
                gv_DatosItem.DataBind();

                string responsableSolicitud = gv_MaterialSolicitado.SelectedRow.Cells[4].Text;
                string estadoSolicitud = gv_MaterialSolicitado.SelectedRow.Cells[6].Text;
                tx_responsableSolicitud.Text = responsableSolicitud;
                dd_estadoSolicitud.SelectedValue = estadoSolicitud;
            }
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_responsableSolicitud.Text = "";
            dd_estadoSolicitud.SelectedIndex = -1;
            gv_MaterialSolicitado.SelectedIndex= -1;
            gv_DatosItem.DataSource = null;
            gv_DatosItem.DataBind();

        }

        protected void bt_actualizar_Click(object sender, EventArgs e)
        {
            ActualizarDatosInsumos();
        }

        private void ActualizarDatosInsumos()
        {
            
            if (gv_MaterialSolicitado.SelectedIndex > -1 )
            {
                NCorpal_PedidoMaterialeInsumos np = new NCorpal_PedidoMaterialeInsumos();
                int codigoPedido;
                int.TryParse(gv_MaterialSolicitado.SelectedRow.Cells[1].Text, out codigoPedido);
                float montoTotalPedidoComprado = 0;

                foreach (GridViewRow row in gv_DatosItem.Rows)
                {
                    int codigoItem = int.Parse(row.Cells[0].Text);
                    float cantidadComprado;                    
                    TextBox tx_cantComprado = row.Cells[5].FindControl("tx_cantidadComprada") as TextBox;
                    float.TryParse(tx_cantComprado.Text.Replace('.',','), out cantidadComprado);
                    float montoComprado;
                    TextBox tx_montoComprado = row.Cells[7].FindControl("tx_montototalcomprado") as TextBox;
                    float.TryParse(tx_montoComprado.Text.Replace('.', ','), out montoComprado);
                    bool bb = np.update_CompradeInsumos(codigoItem,cantidadComprado,montoComprado);
                    montoTotalPedidoComprado = montoTotalPedidoComprado + montoComprado;
                }

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codresponsableCompra = Nresp.getCodUsuario(usuarioAux, passwordAux);
                string ResponsableCompra = Nresp.get_responsable(codresponsableCompra).Tables[0].Rows[0][1].ToString();
                
                string estadoCompra = dd_estadoSolicitud.SelectedItem.Text;                    
                if (estadoCompra.Equals("Abierto"))
                {
                    estadoCompra = "Comprado";
                }
                bool cerrado = np.update_cerrarCompraMaterialInsumos(codigoPedido, estadoCompra, montoTotalPedidoComprado, codresponsableCompra, ResponsableCompra);
                if (cerrado == true)
                {                    
                    mostarlasSolicitudesdeCompradeInsumos("","Abierto");
                    limpiarDatos();
                    Session["ReporteGeneral"] = "Reporte_CompraMaterialInsumos";
                    Session["codigoCompraMaterialeInsumos"] = codigoPedido;
                    Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");

                   // Session["codigoEntregaSolicitudProducto"] = codigoSolicitud;
                   // Response.Redirect("../Presentacion/FCorpal_ReporteEntregaSolicitudProducto.aspx");
                }else
                    Response.Write("<script type='text/javascript'> alert('Error: Guardado') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccionar Solicitud') </script>");
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string responsableSolicitud = tx_responsableSolicitud.Text;
            string estadoSolicitud = dd_estadoSolicitud.SelectedItem.Text;
            mostarlasSolicitudesdeCompradeInsumos(responsableSolicitud, estadoSolicitud);
        }

        

       

    }
}