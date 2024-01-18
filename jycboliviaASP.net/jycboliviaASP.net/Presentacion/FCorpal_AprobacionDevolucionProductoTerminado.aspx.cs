using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_AprobacionDevolucionProductoTerminado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

           /* if (tienePermisoDeIngreso(115) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            */
            if (!IsPostBack)
            {
                buscarDatos("","");
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


        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string fechaDevolucion = convertidorFecha(tx_fechaDevolucion.Text);
            string Producto = tx_producto.Text;
            buscarDatos(fechaDevolucion, Producto);
        }

        private void buscarDatos(string fechaDevolucion, string Producto)
        {
            NCorpal_Produccion pp = new NCorpal_Produccion();
            DataSet tuplas = pp.get_devolucionProductosNoAprovados(fechaDevolucion, Producto);
            gv_produccionProducto.DataSource = tuplas;
            gv_produccionProducto.DataBind();

        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {

        }

        protected void bt_marcar_Click(object sender, EventArgs e)
        {
            ActutorizarDevolucionProductos();
        }

        private void ActutorizarDevolucionProductos()
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NCorpal_Produccion pp = new NCorpal_Produccion();
            for (int i = 0; i < gv_produccionProducto.Rows.Count; i++)
            {
                int codigoDevolucion = Convert.ToInt32(gv_produccionProducto.Rows[i].Cells[1].Text);
                CheckBox cb = (CheckBox)gv_produccionProducto.Rows[i].Cells[0].FindControl("CheckBox1");                
                bool marcado = cb.Checked;
                if(marcado){
                  int codUserAutorizado = codUser;
                  string nombreAutorizacion = Nresp.get_responsable(codUserAutorizado).Tables[0].Rows[0][1].ToString();
                  bool bandera = pp.set_AutorizadoDevolucionProducto(codigoDevolucion, marcado,  codUserAutorizado,  nombreAutorizacion);
                }
            }
            buscarDatos("","");           
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {

        }
    }
}