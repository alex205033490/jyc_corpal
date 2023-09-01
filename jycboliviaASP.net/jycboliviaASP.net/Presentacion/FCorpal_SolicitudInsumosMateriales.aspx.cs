using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_SolicitudInsumosMateriales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(124) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                DataTable datoRepuesto = new DataTable();                
                datoRepuesto.Columns.Add("Proveedor", typeof(string));
                datoRepuesto.Columns.Add("Descripcion", typeof(string));
                datoRepuesto.Columns.Add("Medida", typeof(string));
                datoRepuesto.Columns.Add("Cantidad", typeof(string));
                datoRepuesto.Columns.Add("Monto Total", typeof(string));
                datoRepuesto.Columns.Add("Factura", typeof(string));
                datoRepuesto.Columns.Add("Retencion", typeof(string));

                gv_MaterialSolicitado.DataSource = datoRepuesto;
                gv_MaterialSolicitado.DataBind();
                Session["listaSolicitudMaterial"] = datoRepuesto;
            }
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NCorpal_SolicitudEntregaProducto ncc = new NCorpal_SolicitudEntregaProducto();            
            tx_solicitante.Text = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();

            NCorpal_PedidoMaterialeInsumos pp = new NCorpal_PedidoMaterialeInsumos();
            int nroCorrelativo = pp.get_CorrelativoSolicitudMaterialInsumos();
            tx_nro.Text = (nroCorrelativo + 1).ToString();
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

        protected void bt_guardarSolicitud_Click(object sender, EventArgs e)
        {
            guardarSolicitudPedido();
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

        private void guardarSolicitudPedido()
        {
            string fechaEstimada = convertidorFecha(tx_fechaestimadaEntrega.Text);
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NCorpal_SolicitudEntregaProducto ncc = new NCorpal_SolicitudEntregaProducto();
            string Solicitante = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
            int codSolicitante = codUser;

            NCorpal_PedidoMaterialeInsumos np = new NCorpal_PedidoMaterialeInsumos();
            bool bandera = np.insertarSolicitudMaterialInsumos(codSolicitante, Solicitante, fechaEstimada, 0);
            int codSolicitud = np.get_ultimoinsertadoSolicitudMaterialInsumos(codSolicitante,Solicitante);
            DataTable datoSolicitud = Session["listaSolicitudMaterial"] as DataTable;
            float montoTotalSolicitud = 0;
            if(bandera == true && datoSolicitud.Rows.Count > 0){
                for (int i = 0; i < datoSolicitud.Rows.Count; i++)
                {
                    string proveedor = datoSolicitud.Rows[i]["Proveedor"].ToString();
                    string item = datoSolicitud.Rows[i]["Descripcion"].ToString();
                    string medida = datoSolicitud.Rows[i]["Medida"].ToString();
                    string factura = datoSolicitud.Rows[i]["Factura"].ToString();
                    float cantidad;
                    float.TryParse(datoSolicitud.Rows[i]["Cantidad"].ToString().Replace('.',','), out cantidad);
                    float montoTotalBS;
                    float.TryParse(datoSolicitud.Rows[i]["Monto Total"].ToString().Replace('.', ','), out montoTotalBS);
                    float retencion;
                    float.TryParse(datoSolicitud.Rows[i]["Retencion"].ToString().Replace('.', ','), out retencion);
                    bool bandera2 = np.insertarItemMaterialInsumos(codSolicitud,proveedor,item,medida,cantidad,montoTotalBS,factura,retencion);
                    montoTotalSolicitud = montoTotalSolicitud + montoTotalBS;
                }
            }
           bool bandera3 = np.updateSolicitudMontoTotal(codSolicitud,montoTotalSolicitud);
            if(bandera ==bandera3 == true){
                limpiarTodos();
                limpiarItem();
                Session["ReporteGeneral"] = "Reporte_SolicitudMaterialInsumos";
                Session["codigoSolicitudMaterialeInsumos"] = codSolicitud;                
                Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");
                //Response.Write("<script type='text/javascript'> alert('OK: Guardado') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Guardado') </script>");
        }

        private void limpiarTodos()
        {
            tx_nro.Text = "";
            tx_fechaestimadaEntrega.Text = "";

            NCorpal_PedidoMaterialeInsumos pp = new NCorpal_PedidoMaterialeInsumos();
            int nroCorrelativo = pp.get_CorrelativoSolicitudMaterialInsumos();
            tx_nro.Text = (nroCorrelativo + 1).ToString();

            DataTable datoRepuesto = new DataTable();
            datoRepuesto.Columns.Add("Proveedor", typeof(string));
            datoRepuesto.Columns.Add("Descripcion", typeof(string));
            datoRepuesto.Columns.Add("Medida", typeof(string));
            datoRepuesto.Columns.Add("Cantidad", typeof(string));
            datoRepuesto.Columns.Add("Monto Total", typeof(string));
            datoRepuesto.Columns.Add("Factura", typeof(string));
            datoRepuesto.Columns.Add("Retencion", typeof(string));

            gv_MaterialSolicitado.DataSource = datoRepuesto;
            gv_MaterialSolicitado.DataBind();
            Session["listaSolicitudMaterial"] = datoRepuesto;
        }

     

        private void limpiarItem() {
            tx_proveedor.Text = "";
            tx_descripcion.Text = "";
            tx_unidad.Text = "";
            tx_cantidad.Text = "";
            tx_montoTotalbs.Text = "";
            tx_factura.Text = "";
            tx_retencion.Text = "";
        }

        private void adicionar_productos()
        {
            string proveedor = tx_proveedor.Text;
            string item = tx_descripcion.Text;
            string medida = tx_unidad.Text;
            string factura = tx_factura.Text;
            float cantidad;
            float.TryParse(tx_cantidad.Text.Replace('.',','), out cantidad );

            float montoTotalBS;
            float.TryParse(tx_montoTotalbs.Text.Replace('.', ','), out montoTotalBS);

            float retencion;
            float.TryParse(tx_retencion.Text.Replace('.', ','), out retencion);
                        
            if (cantidad > 0)
            {
                DataTable datoRepuesto = Session["listaSolicitudMaterial"] as DataTable;
                DataRow tupla = datoRepuesto.NewRow();
                tupla["Proveedor"] = proveedor;
                tupla["Descripcion"] = item;
                tupla["Medida"] = medida;
                tupla["Cantidad"] = cantidad;
                tupla["Monto Total"] = montoTotalBS;
                tupla["Factura"] = factura;
                tupla["Retencion"] = retencion;
                datoRepuesto.Rows.Add(tupla);
                gv_MaterialSolicitado.DataSource = datoRepuesto;
                gv_MaterialSolicitado.DataBind();

                limpiarItem();
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Cantidad igual 0') </script>");

        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {

        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            adicionar_productos();
        }

        protected void bt_verRecibo_Click(object sender, EventArgs e)
        {

        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {

        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {

        }

        protected void gv_MaterialSolicitado_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex;

            DataTable datoRepuesto = Session["listaSolicitudMaterial"] as DataTable;
            datoRepuesto.Rows[index].Delete();
            datoRepuesto.AcceptChanges();
            gv_MaterialSolicitado.EditIndex = -1;
            Session["listaSolicitudProducto"] = datoRepuesto;
            gv_MaterialSolicitado.DataSource = datoRepuesto;
            gv_MaterialSolicitado.DataBind();       
        }

       
       
       
    }
}