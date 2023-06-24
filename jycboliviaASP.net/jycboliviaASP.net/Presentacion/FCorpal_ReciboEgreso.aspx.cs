using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ReciboEgreso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(116) == false)
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
                buscarDatos("",codUser);
            }

            NA_Recibo_IngresoEgreso nre = new NA_Recibo_IngresoEgreso();
            string nroRecibo = nre.get_nroRegistroEgresoSiguiente(codUser);
            tx_nrorecibo.Text = nroRecibo;
        }

        private void buscarDatos(string pagadoHA, int coduser)
        {
            NA_Recibo_IngresoEgreso nrr = new NA_Recibo_IngresoEgreso();
            DataSet dato = nrr.mostrarReciboEgreso(pagadoHA, coduser);
            gv_reciboIngresoEgreso.DataSource = dato;
            gv_reciboIngresoEgreso.DataBind();
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

            if (gv_reciboIngresoEgreso.SelectedIndex > -1)
            {
                int codigo;
                int.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[1].Text, out codigo);
                NA_Recibo_IngresoEgreso nrr = new NA_Recibo_IngresoEgreso();
                bool bandera = nrr.eliminarEgreso(codigo);
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

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            string pagadoHA = tx_pagadoha.Text;
            buscarDatos(pagadoHA, codUser);
        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            insertarDAtos();
        }

        private void insertarDAtos()
        {
            string pagadoha = tx_pagadoha.Text;
            float monto;
            float.TryParse(tx_montototal.Text.Replace('.', ','), out monto);
            string moneda = dd_moneda.SelectedItem.Text;
            string chequenro = tx_chequenro.Text;
            string banco = tx_banco.Text;
            bool efectivo = cb_efectivo.Checked;
            string concepto = tx_concepto.Text;
            string detalle = "Ninguno";
            string fechaEgreso = convertidorFecha(tx_fechaegreso.Text);

            float porcentajeretencioniue;
            float.TryParse(tx_retencionIUEporcentaje.Text.Replace('.', ','), out porcentajeretencioniue);
            float porcentajeretencionit;
            float.TryParse(tx_retencionITporcentaje.Text.Replace('.', ','), out porcentajeretencionit);
            float retencioniuebs;
            float.TryParse(tx_retencionIUEBS.Text.Replace('.', ','), out retencioniuebs);
            float retencionitbs;
            float.TryParse(tx_retencionITBS.Text.Replace('.', ','), out retencionitbs);
            float totalapagar;
            float.TryParse(tx_totalaPagar.Text.Replace('.', ','), out totalapagar);            

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            int codrespgra = codUser;
            string responsable = Nresp.get_responsable(codrespgra).Tables[0].Rows[0][1].ToString();

            string facturanro = tx_facturanro.Text;

            NA_Recibo_IngresoEgreso nrr = new NA_Recibo_IngresoEgreso();

            NA_Recibo_IngresoEgreso nre = new NA_Recibo_IngresoEgreso();
            string nroRecibo = nre.get_nroRegistroEgresoSiguiente(codUser);

            bool bandera = nrr.insertarReciboEgreso(pagadoha, monto, moneda, chequenro, concepto, detalle, codrespgra, responsable, banco, efectivo, porcentajeretencioniue, porcentajeretencionit, retencioniuebs, retencionitbs, totalapagar, facturanro, nroRecibo, fechaEgreso);
            if (bandera)
            {
                limpiarDatos();
                buscarDatos("", codUser);
                int ultimoinsertado = nrr.get_codigoUltimoInsertadoEgreso(pagadoha);
                Session["codigoReciboEgreso"] = ultimoinsertado;
                Response.Redirect("../Presentacion/FCorpal_ReporteReciboEgreso.aspx");
                //Response.Write("<script type='text/javascript'> alert('GUARDADO: OK!!') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('ERROR: Guardado') </script>");
        }

        private void limpiarDatos()
        {
            tx_pagadoha.Text = "";            
            tx_montototal.Text = "";
            dd_moneda.SelectedIndex = 0;
            tx_chequenro.Text = "";
            tx_banco.Text = "";
            cb_efectivo.Checked = false;
            tx_concepto.Text = "";

            tx_retencionIUEporcentaje.Text = "";
            tx_retencionITporcentaje.Text = "";
            tx_retencionIUEBS.Text = "";
            tx_retencionITBS.Text = "";
            tx_totalaPagar.Text = "";
            tx_facturanro.Text = "";
            tx_fechaegreso.Text = "";
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            modificarDatos();
        }

        private void modificarDatos()
        {
            if (gv_reciboIngresoEgreso.SelectedIndex > -1)
            {
                int codigo;
                int.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[1].Text, out codigo);
                string pagadoha = tx_pagadoha.Text;
                float monto;
                float.TryParse(tx_montototal.Text.Replace('.', ','), out monto);
                string moneda = dd_moneda.SelectedItem.Text;
                string chequenro = tx_chequenro.Text;
                string banco = tx_banco.Text;
                bool efectivo = cb_efectivo.Checked;
                string concepto = tx_concepto.Text;
                string detalle = "Ninguno";
                string fechaegreso = convertidorFecha(tx_fechaegreso.Text);

                float porcentajeretencioniue;
                float.TryParse(tx_retencionIUEporcentaje.Text.Replace('.', ','), out porcentajeretencioniue);
                float porcentajeretencionit;
                float.TryParse(tx_retencionITporcentaje.Text.Replace('.', ','), out porcentajeretencionit);
                float retencioniuebs;
                float.TryParse(tx_retencionIUEBS.Text.Replace('.', ','), out retencioniuebs);
                float retencionitbs;
                float.TryParse(tx_retencionITBS.Text.Replace('.', ','), out retencionitbs);
                float totalapagar;
                float.TryParse(tx_totalaPagar.Text.Replace('.', ','), out totalapagar);
                

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                int codrespgra = codUser;
                string responsable = Nresp.get_responsable(codrespgra).Tables[0].Rows[0][1].ToString();

                string facturanro = tx_facturanro.Text;

                NA_Recibo_IngresoEgreso nrr = new NA_Recibo_IngresoEgreso();
                bool bandera = nrr.modificarReciboEgreso(codigo, pagadoha, monto, moneda, chequenro, concepto, detalle, codrespgra, responsable, banco, efectivo, porcentajeretencioniue, porcentajeretencionit, retencioniuebs, retencionitbs, totalapagar, facturanro, fechaegreso);
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

        protected void gv_reciboIngresoEgreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarDatos();
        }

        private void seleccionarDatos()
        {
            tx_pagadoha.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[4].Text;
            float monto;
            float.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[5].Text, out monto);
            tx_montototal.Text = monto.ToString().Replace('.',',');
            dd_moneda.SelectedValue = gv_reciboIngresoEgreso.SelectedRow.Cells[6].Text;
            tx_chequenro.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[7].Text;
            tx_banco.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[8].Text;            
            cb_efectivo.Checked = (gv_reciboIngresoEgreso.SelectedRow.Cells[9].Controls[0] as CheckBox).Checked;
            tx_concepto.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[10].Text;
            
            float porcentajeretencioniue;
            float.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[13].Text, out porcentajeretencioniue);
            float porcentajeretencionit;
            float.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[14].Text, out porcentajeretencionit);
            float retencioniuebs;
            float.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[15].Text, out retencioniuebs);
            float retencionitbs;
            float.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[16].Text, out retencionitbs);
            float totalapagar;
            float.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[17].Text, out totalapagar);

            tx_facturanro.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[18].Text;
            tx_nrorecibo.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[19].Text;
            tx_fechaegreso.Text = gv_reciboIngresoEgreso.SelectedRow.Cells[20].Text;
                        
            tx_retencionIUEporcentaje.Text = porcentajeretencioniue.ToString();            
            tx_retencionITporcentaje.Text= porcentajeretencionit.ToString();            
            tx_retencionIUEBS.Text = retencioniuebs.ToString();            
            tx_retencionITBS.Text = retencionitbs.ToString();            
            tx_totalaPagar.Text = totalapagar.ToString();
        }

        protected void bt_verEgreso_Click(object sender, EventArgs e)
        {
            verEgresoDatos();
        }

        private void verEgresoDatos()
        {
            if(gv_reciboIngresoEgreso.SelectedIndex > -1){
                int ultimoinsertado;
                int.TryParse(gv_reciboIngresoEgreso.SelectedRow.Cells[1].Text, out ultimoinsertado);
                Session["codigoReciboEgreso"] = ultimoinsertado;
                Response.Redirect("../Presentacion/FCorpal_ReporteReciboEgreso.aspx");
            }
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {

        }

    }
}