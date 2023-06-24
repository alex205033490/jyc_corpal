using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ReciboIngreso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(115) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            if (!IsPostBack)
            {                
                buscarDatos("", codUser);
            }
            NA_Recibo_IngresoEgreso nr = new NA_Recibo_IngresoEgreso();
            string NroRecibo = nr.get_nroRegistroIngresoSiguiente(codUser);
            tx_nroRecibo.Text = NroRecibo;
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

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            insertarDAtos();
        }

        private void insertarDAtos()
        {
            string cliente = tx_cliente.Text; 
            float monto;
            float.TryParse(tx_montototal.Text.Replace('.',','), out monto);
            string  moneda = dd_moneda.SelectedItem.Text; 
            string chequenro = tx_chequenro.Text; 
            string concepto = tx_concepto.Text;
            string detalle = tx_detalle.Text;
            string fechaRecibo = convertidorFecha(tx_fechaingreso.Text);

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            
            string responsable = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
            string facturanro = tx_nroFactura.Text;
            

            NA_Recibo_IngresoEgreso nrr = new NA_Recibo_IngresoEgreso();
            string NroReciboAux = nrr.get_nroRegistroIngresoSiguiente(codUser);
            string nroRecibo = NroReciboAux;
            bool bandera = nrr.insertarReciboIngreso(cliente, monto, moneda, chequenro, concepto, detalle, codUser, responsable, facturanro, nroRecibo, fechaRecibo);
            if(bandera){
                limpiarDatos();
                buscarDatos("", codUser);
                int ultimoinsertado = nrr.get_codigoUltimoInsertado(cliente);
                Session["codigoReciboIngreso"] = ultimoinsertado;
                Response.Redirect("../Presentacion/FCorpal_ReporteReciboIngreso.aspx");
                //Response.Write("<script type='text/javascript'> alert('GUARDADO: OK!!') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('ERROR: Guardado') </script>");
        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            modificarDatos();
        }

        private void modificarDatos()
        {
            if(gv_reciboIngresoEgreso.SelectedIndex > -1){
                int codigo;
                int.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[1].Text, out codigo);

                string cliente = tx_cliente.Text;
                float monto;
                float.TryParse(tx_montototal.Text.Replace('.', ','), out monto);
                string moneda = dd_moneda.SelectedItem.Text;
                string chequenro = tx_chequenro.Text;
                string concepto = tx_concepto.Text;
                string detalle = tx_detalle.Text;
                string fecharecibo = convertidorFecha(tx_fechaingreso.Text);

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                int codrespgra = codUser;
                string responsable = Nresp.get_responsable(codrespgra).Tables[0].Rows[0][1].ToString();
                string facturanro = tx_nroFactura.Text;

                NA_Recibo_IngresoEgreso nrr = new NA_Recibo_IngresoEgreso();
                bool bandera = nrr.modificarReciboIngreso(codigo, cliente, monto, moneda, chequenro, concepto, detalle, codrespgra, responsable, facturanro, fecharecibo);
                if (bandera)
                {
                    limpiarDatos();
                    buscarDatos("", codUser);
                    Response.Write("<script type='text/javascript'> alert('MODIFICADO: OK!!') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('ERROR: MODIFICADO') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('ERROR: Seleccione un Recibo') </script>");
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarDatos();
        }

        private void eliminarDatos()
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            if(gv_reciboIngresoEgreso.SelectedIndex > -1){
                int codigo;
                int.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[1].Text, out codigo);
                NA_Recibo_IngresoEgreso nrr = new NA_Recibo_IngresoEgreso();
                bool bandera = nrr.eliminarIngreso(codigo);
                if (bandera)
                {                    
                    limpiarDatos();
                    buscarDatos("", codUser);
                    Response.Write("<script type='text/javascript'> alert('ELIMINADO: OK!!') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('ERROR: ELIMINADO') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('ERROR: Seleccione un recibo') </script>");
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_cliente.Text = "";
            tx_montototal.Text = "";
            dd_moneda.SelectedIndex = 0;
            tx_chequenro.Text = "";
            tx_concepto.Text = "";
            tx_detalle.Text = "";
            tx_nroFactura.Text = "";
            tx_fechaingreso.Text = "";
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            string cliente = tx_cliente.Text;
            buscarDatos(cliente, codUser);
        }

        private void buscarDatos(string cliente, int codUser)
        {
            NA_Recibo_IngresoEgreso nrr = new NA_Recibo_IngresoEgreso();
            DataSet dato = nrr.mostrarReciboIngreso(cliente, codUser);
            gv_reciboIngresoEgreso.DataSource = dato;
            gv_reciboIngresoEgreso.DataBind();
        }

        protected void gv_reciboIngresoEgreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecciondeDatos();
        }

        private void selecciondeDatos()
        {
            if(gv_reciboIngresoEgreso.SelectedIndex > -1){

                tx_cliente.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[4].Text;
                float monto ;
                float.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[5].Text.Replace('.',','), out monto);
                tx_montototal.Text = monto.ToString();
                dd_moneda.SelectedValue = gv_reciboIngresoEgreso.SelectedRow.Cells[6].Text;
                tx_chequenro.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[7].Text;
                tx_concepto.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[8].Text;
                tx_detalle.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[9].Text;
                tx_nroFactura.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[11].Text;
                tx_nroRecibo.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[12].Text;
                tx_fechaingreso.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[13].Text;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            VerRecibo();
        }

        private void VerRecibo()
        {
            if(gv_reciboIngresoEgreso.SelectedIndex > -1){
                int codigo;
                int.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[1].Text ,out codigo);
                Session["codigoReciboIngreso"] = codigo;
                Response.Redirect("../Presentacion/FCorpal_ReporteReciboIngreso.aspx");            
            }
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {

        }

       
    }
}