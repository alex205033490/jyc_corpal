using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Configuration;
using System.Data;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ReciboPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
           if(!IsPostBack){
            desactivarEdit();
            llenarLosCampos();
           }
        }

        public void desactivarEdit() { 
            tx_numeroRegistro.Enabled = false;
            tx_Responsable.Enabled = false;
            tx_ResponsablePago.Enabled = false;
            tx_montoPagado.Enabled = false;
            tx_NombreProyecto.Enabled = false;
            tx_MesCancelado.Enabled = false;

            tx_numeroRegistro0.Enabled = false;
            tx_Responsable0.Enabled = false;
            tx_ResponsablePago0.Enabled = false;
            tx_montoPagado0.Enabled = false;
            tx_NombreProyecto0.Enabled = false;
            tx_MesCancelado0.Enabled = false;

            CheckBox_Efectivo.Checked = false;
            CheckBox_Efectivo0.Checked = false;
            CheckBox_Deposito0.Checked = false;
            CheckBox_Deposito.Checked = false;

            tx_NroCheques.Text = "0";
            tx_NroCheques0.Text = "0";

            tx_Detalle.Text = "";
            tx_Detalle0.Text = "";

        }

        public void llenarLosCampos() {
            tx_numeroRegistro.Text = Session["reciboNumero"].ToString() ;
            tx_Responsable.Text =  Session["reciboResponsable"].ToString() ;
            lb_reponsable.Text = tx_Responsable.Text;
            tx_ResponsablePago.Text = Session["reciboEncargadoPago"].ToString() ;
            tx_montoPagado.Text = Session["reciboMontoCancelado"].ToString();
            tx_NombreProyecto.Text = Session["reciboProyecto"].ToString();
            tx_FechaRecibo.Text = DateTime.Now.ToString("dd-MM-yyyy");
            int codMes = Convert.ToInt32(Session["reciboCodMes"].ToString());
            NMeses MesN = new NMeses();
            DataSet resultado = MesN.getMes(codMes);
            tx_MesCancelado.Text = resultado.Tables[0].Rows[0][1].ToString();


            tx_numeroRegistro0.Text = Session["reciboNumero"].ToString();
            tx_Responsable0.Text = Session["reciboResponsable"].ToString();
            lb_reponsable0.Text = tx_Responsable.Text;
            tx_ResponsablePago0.Text = Session["reciboEncargadoPago"].ToString();
            tx_montoPagado0.Text = Session["reciboMontoCancelado"].ToString();
            tx_NombreProyecto0.Text = Session["reciboProyecto"].ToString();
            tx_FechaRecibo0.Text = DateTime.Now.ToString("dd-MM-yyyy");
            tx_MesCancelado0.Text = tx_MesCancelado.Text;

           

            NA_DetalleSeguimiento Nseguimiento = new NA_DetalleSeguimiento();
            int codSeguimiento = Convert.ToInt32(Session["reciboCodSeguimiento"].ToString()) ;

            string reciboCodigo = Session["reciboCodigo"].ToString();
            int codrecibo = -1;
            if(reciboCodigo != ""){
                codrecibo = Convert.ToInt32(reciboCodigo);
            }
            
            if(codrecibo > -1 ){
                bool bandera = Nseguimiento.obtenerValorEfectivoDeposito(codSeguimiento, codMes, codrecibo, 1);
                if (bandera == true)
                {
                    CheckBox_Efectivo.Checked = true;
                    CheckBox_Efectivo0.Checked = true;
                }

                bandera = Nseguimiento.obtenerValorEfectivoDeposito(codSeguimiento, codMes, codrecibo, 2);
                if (bandera == true)
                {
                    CheckBox_Deposito.Checked = true;
                    CheckBox_Deposito0.Checked = true;
                }             
            
            }

            
            string cheque = Session["nroCheque"].ToString();
            if (cheque == "&nbsp;")
            {
                tx_NroCheques.Text = "0";
                tx_NroCheques0.Text = "0";
            }
            else {
                tx_NroCheques.Text = cheque;
                tx_NroCheques0.Text = cheque;
            
            }

            string detalleaux = Session["detalle"].ToString();
            if (detalleaux == "&nbsp;")
            {
            tx_Detalle.Text = "";
            tx_Detalle0.Text = "";
            }else{
            tx_Detalle.Text = detalleaux;
            tx_Detalle0.Text = detalleaux;
            }
            
            
            
             
        }


        public void actualizar() {
           int codRecibo = Convert.ToInt32(Session["reciboNumero"].ToString());
           int codSeguimiento = Convert.ToInt32(Session["reciboCodSeguimiento"].ToString()) ;
           int codMes = Convert.ToInt32(Session["reciboCodMes"].ToString()) ;
           int codUser = Convert.ToInt32(Session["reciboCodResponsable"].ToString()) ;
           string detalle = tx_Detalle.Text;
           bool efectivo = CheckBox_Efectivo.Checked;
           bool deposito = CheckBox_Deposito.Checked;
           string nroCheque = tx_NroCheques.Text;

           NA_ReciboPago reciboN = new NA_ReciboPago();
           reciboN.modificar(codRecibo,codSeguimiento,codMes,codUser,detalle, efectivo, deposito, nroCheque);
        }

        protected void bt_imprimir_Click(object sender, EventArgs e)
        {
            actualizar();
            copiartodo();
            Response.Write("<script type='text/javascript'> window.print()</script>");
        }

        private void copiartodo()
        {
            CheckBox_Efectivo0.Checked = CheckBox_Efectivo.Checked;
            CheckBox_Deposito0.Checked = CheckBox_Deposito.Checked;
            tx_NroCheques0.Text = tx_NroCheques.Text;
            tx_Detalle0.Text = tx_Detalle.Text;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
            Response.Redirect(ruta + "/Presentacion/FA_PagoSeguimiento.aspx");
        }

      

       
    }
}