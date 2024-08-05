using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Drawing;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_PagoSeguimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(11) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            lb_titulo.Text = "Gestion de Pago del Seguimiento " + Session["BaseDatos"].ToString();

            if (!IsPostBack)
            {
                cargarEquipos();
                NA_Historial nhistorial = new NA_Historial();
                int codUser = Convert.ToInt32(Session["coduser"].ToString());
                nhistorial.insertar(codUser, "Ingresado a Pago de Seguimiento");
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

        private void cargarEquipos()
        {
            NA_Equipo Nequipo = new NA_Equipo();
            DataSet datosEquipo = Nequipo.MostrarAllEquipoSeguimiento("", "");
            gv_equipos.DataSource = datosEquipo;
            gv_equipos.DataBind();
        }

        private void cargarEquipos(string exbo, string nombreProyecto)
        {
            NA_Equipo Nequipo = new NA_Equipo();
            DataSet datosEquipo = Nequipo.MostrarAllEquipoSeguimiento(exbo, nombreProyecto);
            gv_equipos.DataSource = datosEquipo;
            gv_equipos.DataBind();
        }

        private void cargarSeguimientoEquipo(int codigoEquipoExbo)
        {
            gv_SeguimientoMantenimiento.SelectedIndex = -1;
            NA_Seguimiento Nsegui = new NA_Seguimiento();
            DataSet mostrarTabla = Nsegui.mostrarSeguimiento(codigoEquipoExbo);
            gv_SeguimientoMantenimiento.DataSource = mostrarTabla;
            gv_SeguimientoMantenimiento.DataBind();

            gv_seguiMes.DataSource = null;
            gv_seguiMes.DataBind();

            gv_recibos.DataSource = null;
            gv_recibos.DataBind();
        }

        private void cargarSeguimientoMes(int codSeguimientoAux)
        {
            gv_seguiMes.SelectedIndex = -1;
            NA_DetalleSeguimiento NdetalleSegui = new NA_DetalleSeguimiento();
            DataSet datosTabla = NdetalleSegui.MostrarAllDatos(codSeguimientoAux);
            gv_seguiMes.DataSource = datosTabla;
            gv_seguiMes.DataBind();

            gv_recibos.DataSource = null;
            gv_recibos.DataBind();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string exboAux = tx_Exbo.Text;
            string nombreProyectoAux = tx_nombreProyecto.Text;
            cargarEquipos(exboAux, nombreProyectoAux);
        }

        protected void gv_equipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codigoEquipo = Convert.ToInt32(gv_equipos.SelectedRow.Cells[1].Text);
            string codigoExbo = gv_equipos.SelectedRow.Cells[2].Text;
            cargarSeguimientoEquipo(codigoEquipo);
        }

        protected void gv_SeguimientoMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codSeguimiento = Convert.ToInt32(gv_SeguimientoMantenimiento.SelectedRow.Cells[1].Text);
            cargarSeguimientoMes(codSeguimiento);            
        }

        protected void gv_seguiMes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_seguiMes.EditIndex = -1;
            int codSeguimiento = Convert.ToInt32(gv_SeguimientoMantenimiento.SelectedRow.Cells[1].Text);
            cargarSeguimientoMes(codSeguimiento);  
        }

        protected void gv_seguiMes_RowEditing(object sender, GridViewEditEventArgs e)
        {          

            gv_seguiMes.EditIndex = e.NewEditIndex;
            // Response.Write("<script type='text/javascript'> alert('el index es " + e.NewEditIndex + "') </script>");
            int codSeguimiento = Convert.ToInt32(gv_SeguimientoMantenimiento.SelectedRow.Cells[1].Text);
            NA_DetalleSeguimiento NdetalleSegui = new NA_DetalleSeguimiento();
            DataSet datosTabla = NdetalleSegui.MostrarAllDatos(codSeguimiento);
            gv_seguiMes.DataSource = datosTabla;
            gv_seguiMes.DataBind();

            int index = gv_seguiMes.EditIndex;
            GridViewRow row = gv_seguiMes.Rows[index];
            TextBox auxtexto = (TextBox)row.Cells[2].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto = (TextBox)row.Cells[3].Controls[0];
            auxtexto.ReadOnly = true;

            TextBox MontoPago = (TextBox)row.Cells[5].Controls[0];
            MontoPago.BackColor = Color.Yellow;
            MontoPago.Text = "0";

            auxtexto = (TextBox)row.Cells[4].Controls[0];
            auxtexto.ReadOnly = true;
            auxtexto = (TextBox)row.Cells[6].Controls[0];
            auxtexto.ReadOnly = true;

            gv_recibos.DataSource = null;
            gv_recibos.DataBind();
                  
        }

        protected void gv_seguiMes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int codUser = Convert.ToInt32(Session["coduser"].ToString());

            int index = gv_seguiMes.EditIndex;
            GridViewRow row = gv_seguiMes.Rows[index];
            // string MontoModificar = row.Cells[3].Text;
            TextBox MontoModificar = (TextBox)row.Cells[5].Controls[0];
            gv_seguiMes.EditIndex = -1;
            //  Response.Write("<script type='text/javascript'> alert(' se modifico " + MontoModificar.Text + "') </script>");
            int codSeguimiento = Convert.ToInt32(gv_SeguimientoMantenimiento.SelectedRow.Cells[1].Text);
            TextBox auxMex = (TextBox)row.Cells[2].Controls[0];
            int codMes = Convert.ToInt32(auxMex.Text);
            NA_DetalleSeguimiento Ndetsegui = new NA_DetalleSeguimiento();
            bool bandera = Ndetsegui.modificarMotoPago(codSeguimiento, codMes, Convert.ToSingle(MontoModificar.Text.Replace(".",",")),codUser);


            string exbo = gv_equipos.SelectedRow.Cells[2].Text;
            string edificio = gv_equipos.SelectedRow.Cells[3].Text;
            NA_Seguimiento Nsegui = new NA_Seguimiento();
            int meseslimitePermitidodeRetrazo = NA_VariablesGlobales.meseslimitesdeAtrazadosPermitidosMantenimiento;
            bool banderaSW = Nsegui.modificarEstadoMantenimiento_CriticoMantenimiento_MantenimientoCritico(codSeguimiento, exbo, edificio, meseslimitePermitidodeRetrazo, codUser);

                      
            if (bandera==true){
                int CodResp = Convert.ToInt32(Session["coduser"]);
                NA_ReciboPago reciboPagoN = new NA_ReciboPago();
                reciboPagoN.insertar("", Convert.ToSingle(MontoModificar.Text.Replace(".", ",")), codSeguimiento, codMes, CodResp);
                //--------historial
                NA_Historial nhistorial = new NA_Historial();              
                nhistorial.insertar(codUser, "Ha Modificado seguimiento " + codSeguimiento + " del mes " + codMes + " por el monto Pago " + MontoModificar.Text);
                //------------
                Session["reciboNumero"] = reciboPagoN.ultimoinsertado();
                NA_Responsables responsablesN = new NA_Responsables();
                DataSet tupla = responsablesN.get_responsable(CodResp);
                
                NEncargadoPago encargadoPagoN = new NEncargadoPago();
                int codEncargadoPago = encargadoPagoN.obtenerCodigoEncargadoPagoProyecto(gv_equipos.SelectedRow.Cells[3].Text);
                string nombreEncargadoPAgo = encargadoPagoN.getEncargadoPago(codEncargadoPago);

                Session["reciboResponsable"] = tupla.Tables[0].Rows[0][1].ToString();
                Session["reciboProyecto"] = gv_equipos.SelectedRow.Cells[3].Text;
                Session["reciboEncargadoPago"] = nombreEncargadoPAgo;
                Session["reciboMontoCancelado"] = MontoModificar.Text; 
               
                Session["reciboCodSeguimiento"] = codSeguimiento;
                Session["reciboCodMes"] = codMes;
                Session["reciboCodResponsable"] = codUser;

                Session["detalle"] = "";                
                Session["nroCheque"] = "0";
                Session["reciboCodigo"] = "";

                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_ReciboPago.aspx");
               //Response.Write("<script type='text/javascript'>window.open('" + ruta + "/Presentacion/FA_ReciboPago.aspx','cal','width=400,height=250,left=270,top=180');</script>");
               

            }else{
                Response.Write("<script type='text/javascript'> alert('Error: Saldo Mayor') </script>");
            }
            cargarSeguimientoMes(codSeguimiento); 
        }

        protected void gv_seguiMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codSeguimiento = Convert.ToInt32(gv_SeguimientoMantenimiento.SelectedRow.Cells[1].Text);
            int codMes = Convert.ToInt32(gv_seguiMes.SelectedRow.Cells[2].Text);
            NA_DetalleSeguimiento Ndetsegui = new NA_DetalleSeguimiento();
            DataSet tuplasResult = Ndetsegui.getAllRecibos(codSeguimiento,codMes);
            gv_recibos.DataSource = tuplasResult;
            gv_recibos.DataBind(); 
        }

        protected void gv_recibos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["reciboNumero"] = Convert.ToInt32(gv_recibos.SelectedRow.Cells[1].Text);
            NA_Responsables responsablesN = new NA_Responsables();
            int CodResp = Convert.ToInt32(Session["coduser"]);
            DataSet tupla = responsablesN.get_responsable(CodResp);

            NEncargadoPago encargadoPagoN = new NEncargadoPago();
            int codEncargadoPago = encargadoPagoN.obtenerCodigoEncargadoPagoProyecto(gv_equipos.SelectedRow.Cells[3].Text);
            string nombreEncargadoPAgo = encargadoPagoN.getEncargadoPago(codEncargadoPago);

            Session["reciboResponsable"] = tupla.Tables[0].Rows[0][1].ToString();
            Session["reciboProyecto"] = gv_equipos.SelectedRow.Cells[3].Text;
            Session["reciboEncargadoPago"] = nombreEncargadoPAgo;
            Session["reciboMontoCancelado"] = gv_recibos.SelectedRow.Cells[5].Text;

            int codSeguimiento = Convert.ToInt32(gv_SeguimientoMantenimiento.SelectedRow.Cells[1].Text);
            int codMes = Convert.ToInt32(gv_seguiMes.SelectedRow.Cells[2].Text);
            int codUser = Convert.ToInt32(Session["coduser"].ToString());
            Session["reciboCodSeguimiento"] = codSeguimiento;
            Session["reciboCodMes"] = codMes;
            Session["reciboCodResponsable"] = codUser;
            Session["reciboCodigo"] = gv_recibos.SelectedRow.Cells[1].Text;

            Session["detalle"] = gv_recibos.SelectedRow.Cells[4].Text;
            Session["nroCheque"] = gv_recibos.SelectedRow.Cells[8].Text;
            

            string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
            Response.Redirect(ruta + "/Presentacion/FA_ReciboPago.aspx");
        }
    }
}