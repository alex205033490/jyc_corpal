using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_GestionarInsumos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(134) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {
                buscarInsumos("");
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

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_Insumo.Text = "";
            tx_detalle.Text = "";
            tx_codigoGrupo.Text = "";
            tx_codigoUpon.Text = "";
            tx_medida.Text = "";
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string insumo = tx_Insumo.Text;
            buscarInsumos(insumo);
        }

        private void buscarInsumos(string insumo)
        {
            NCorpal_Produccion npp = new NCorpal_Produccion();
            DataSet tuplas = npp.get_insumosNormal(insumo);
            gv_Insumos.DataSource = tuplas; 
            gv_Insumos.DataBind();
        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            insertarDatosInsumos();
        }

        private void insertarDatosInsumos()
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            string insumos = tx_Insumo.Text;
            string codigoGrupo = tx_codigoGrupo.Text;
            string codigoUpon = tx_codigoUpon.Text;
            string medida=tx_medida.Text;
            string detalle = tx_detalle.Text;
            NCorpal_Produccion npp = new NCorpal_Produccion();
            bool bandera = npp.insertarInsumoSolo(insumos,codigoGrupo,codigoUpon,medida, detalle, codUser);
            if (bandera) {
                Response.Write("<script type='text/javascript'> alert('Guardado: Ok!!') </script>");
                buscarInsumos("");
            }else
                Response.Write("<script type='text/javascript'> alert('Guardado: ERROR') </script>");
        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            if (gv_Insumos.SelectedIndex >= 0) {
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                int codigo;
                int.TryParse(gv_Insumos.SelectedRow.Cells[1].Text, out codigo);

                string insumos = tx_Insumo.Text;
                string codigoGrupo = tx_codigoGrupo.Text;
                string codigoUpon = tx_codigoUpon.Text;
                string medida = tx_medida.Text;
                string detalle = tx_detalle.Text;
                NCorpal_Produccion npp = new NCorpal_Produccion();
                bool bandera = npp.modificarInsumoSolo(codigo,insumos, codigoGrupo, codigoUpon, medida, detalle, codUser);
                if (bandera)
                {
                    Response.Write("<script type='text/javascript'> alert('Guardado: Ok!!') </script>");
                    buscarInsumos("");
                }else
                    Response.Write("<script type='text/javascript'> alert('Guardado: ERROR!!') </script>");

            }else
                Response.Write("<script type='text/javascript'> alert('Seleccion: Seleccionar Dato!!') </script>");

        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarDatosInsumos();
        }

        private void eliminarDatosInsumos()
        {

            if (gv_Insumos.SelectedIndex >= 0)
            {
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                int codigo;
                int.TryParse(gv_Insumos.SelectedRow.Cells[1].Text, out codigo);
                                
                NCorpal_Produccion npp = new NCorpal_Produccion();
                bool bandera = npp.eliminarInsumoSolo(codigo, codUser);
                if (bandera)
                {
                    Response.Write("<script type='text/javascript'> alert('Eliminado: Ok!!') </script>");
                    buscarInsumos("");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Eliminado: ERROR!!') </script>");

            }
            else
                Response.Write("<script type='text/javascript'> alert('Seleccion: Seleccionar Dato!!') </script>");
        }

        protected void gv_reciboIngresoEgreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarDatosInsumos();
        }

        private void seleccionarDatosInsumos()
        {
            if (gv_Insumos.SelectedIndex >= 0) {
                int codigo;
                int.TryParse(gv_Insumos.SelectedRow.Cells[1].Text, out codigo);

                NCorpal_Produccion npp = new NCorpal_Produccion();
                DataSet tuplas = npp.get_insumosCodigo(codigo);
                tx_Insumo.Text = tuplas.Tables[0].Rows[0][1].ToString();
                tx_detalle.Text = tuplas.Tables[0].Rows[0][2].ToString();
                tx_medida.Text = tuplas.Tables[0].Rows[0][3].ToString();
                tx_codigoGrupo.Text = tuplas.Tables[0].Rows[0][4].ToString();
                tx_codigoUpon.Text = tuplas.Tables[0].Rows[0][5].ToString();
            }
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {
            exceldatos();
        }

        private void exceldatos()
        {
            throw new NotImplementedException();
        }
    }
}