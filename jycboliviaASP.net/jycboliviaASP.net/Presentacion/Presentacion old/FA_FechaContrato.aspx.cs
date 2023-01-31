using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Globalization;
using System.IO;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_FechaContrato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();


            if (tienePermisoDeIngreso(28) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            if (!IsPostBack)
            {
                limpiar();                
                Buscar("","");                
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


        private void Buscar(string nombreEdificio, string exbo)
        {
            NA_Equipo equipo1 = new NA_Equipo();
            DataSet tuplaRes = equipo1.getEquiposContratosFirmados1(nombreEdificio,exbo);
            gv_contratosFirmados.DataSource = tuplaRes;
            gv_contratosFirmados.DataBind();
            tx_cantidadEquipos.Text = gv_contratosFirmados.Rows.Count.ToString(); 
        }

        private void limpiar()
        {
            tx_contratoFirmado.Text = "";
            tx_edificio.Text = "";
            tx_exbo.Text = "";
            tx_fechaActaDefinitiva.Text = "";
            tx_fechaContratoInicio.Text = "";
            tx_fechaEquipoEntregado.Text = "";
            tx_fechaFinManGratuito.Text = "";
            tx_InicioManGratuito.Text = "";
            tx_marca.Text = "";
            tx_mesesManGratuito.Text = "";
            tx_montoContrato.Text = "";
            tx_nroContrato.Text = "";
            tx_paradas.Text = "";
            tx_pasajeros.Text = "";
            tx_tipoEquipo.Text = "";
            tx_mesesContrato.Text = "";
            tx_fechaContratoFin.Text = "";
            tx_velocidad.Text = "";
            tx_modelo.Text = ""; 
            tx_habilitacionEquipo.Text = "";
        }

        public string convertidorFecha(string fecha)
        {
            if (fecha != "" || fecha != null)
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

        public string aumentarFecha_conMes(string fecha, int meses)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha, new CultureInfo("es-ES"));
               fecha_ = fecha_.AddMonths(meses);
                string _fecha = fecha_.ToString("dd/MM/yyyy");
                return _fecha ;
            }
            else
                return "null";
        }

        private void cargarDatoSeleccionado()
        {
            tx_exbo.Text = gv_contratosFirmados.SelectedRow.Cells[2].Text;
            tx_edificio.Text = HttpUtility.HtmlDecode(gv_contratosFirmados.SelectedRow.Cells[3].Text);
            
            if (gv_contratosFirmados.SelectedRow.Cells[4].Text != "&nbsp;")
            {
                tx_tipoEquipo.Text = gv_contratosFirmados.SelectedRow.Cells[4].Text;
            }
            else
                tx_tipoEquipo.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[5].Text != "&nbsp;")
            {
                tx_marca.Text = gv_contratosFirmados.SelectedRow.Cells[5].Text;
            }
            else
                tx_marca.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[6].Text != "&nbsp;")
            {
                tx_paradas.Text = gv_contratosFirmados.SelectedRow.Cells[6].Text;
            }
            else
                tx_paradas.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[7].Text != "&nbsp;") {
                tx_pasajeros.Text = gv_contratosFirmados.SelectedRow.Cells[7].Text;
            }else
                tx_pasajeros.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[8].Text != "&nbsp;")
            {
                tx_velocidad.Text = gv_contratosFirmados.SelectedRow.Cells[8].Text;
            }
            else
                tx_velocidad.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[9].Text != "&nbsp;")
            {
                tx_modelo.Text = gv_contratosFirmados.SelectedRow.Cells[9].Text;
            }
            else
                tx_modelo.Text = "";


            if (gv_contratosFirmados.SelectedRow.Cells[10].Text != "&nbsp;")
            {
                tx_fechaActaDefinitiva.Text = gv_contratosFirmados.SelectedRow.Cells[10].Text;
            }else
                tx_fechaActaDefinitiva.Text = "";

            
            if (gv_contratosFirmados.SelectedRow.Cells[11].Text != "&nbsp;") {
                tx_fechaEquipoEntregado.Text = gv_contratosFirmados.SelectedRow.Cells[11].Text;
            }else
                tx_fechaEquipoEntregado.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[12].Text != "&nbsp;") {
                tx_InicioManGratuito.Text = gv_contratosFirmados.SelectedRow.Cells[12].Text;
            }else
                tx_InicioManGratuito.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[13].Text != "&nbsp;")
            {
                tx_mesesManGratuito.Text = gv_contratosFirmados.SelectedRow.Cells[13].Text;
            }else
                tx_mesesManGratuito.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[14].Text != "&nbsp;") {
                tx_fechaFinManGratuito.Text = gv_contratosFirmados.SelectedRow.Cells[14].Text;
            }else
                tx_fechaFinManGratuito.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[15].Text != "&nbsp;") {
                tx_contratoFirmado.Text = gv_contratosFirmados.SelectedRow.Cells[15].Text;
            }else
                tx_contratoFirmado.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[16].Text != "&nbsp;") {
                tx_nroContrato.Text = gv_contratosFirmados.SelectedRow.Cells[16].Text;
            }else
                tx_nroContrato.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[17].Text != "&nbsp;") {
                tx_montoContrato.Text = gv_contratosFirmados.SelectedRow.Cells[17].Text;
            }else
                tx_montoContrato.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[18].Text != "&nbsp;") {
                tx_fechaContratoInicio.Text = gv_contratosFirmados.SelectedRow.Cells[18].Text;
            }else
                tx_fechaContratoInicio.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[19].Text != "&nbsp;") {
                tx_mesesContrato.Text = gv_contratosFirmados.SelectedRow.Cells[19].Text;
            }else
                tx_mesesContrato.Text = "";

            if (gv_contratosFirmados.SelectedRow.Cells[20].Text != "&nbsp;") {
                tx_fechaContratoFin.Text = gv_contratosFirmados.SelectedRow.Cells[20].Text;
            }else
                tx_fechaContratoFin.Text = "";


            if (gv_contratosFirmados.SelectedRow.Cells[21].Text != "&nbsp;")
            {
                tx_habilitacionEquipo.Text = gv_contratosFirmados.SelectedRow.Cells[21].Text;
            }
            else
                tx_habilitacionEquipo.Text = "";
            
            
            
        }

        protected void bt_actualizar_Click(object sender, EventArgs e)
        {
            actualizarDatos();
            Buscar(HttpUtility.HtmlDecode(tx_edificio.Text), HttpUtility.HtmlDecode(tx_exbo.Text));
            limpiar();
        }

        private void actualizarDatos()
        {
            NA_Equipo equipo = new NA_Equipo();
           if(gv_contratosFirmados.SelectedIndex > -1){
               int codigoEquipo = Convert.ToInt32(gv_contratosFirmados.SelectedRow.Cells[1].Text);

               if (tx_InicioManGratuito.Text != "")
               {
                
                       string fechaInicioManGratuito = convertidorFecha(tx_InicioManGratuito.Text);
                       int mesesGratuitos = 0;
                       if (tx_mesesManGratuito.Text != "")
                       {
                           mesesGratuitos = Convert.ToInt32(tx_mesesManGratuito.Text);                                
                           }
                
                       tx_fechaFinManGratuito.Text = aumentarFecha_conMes(tx_InicioManGratuito.Text, mesesGratuitos);
                       string fechaFinManGratuito = convertidorFecha(tx_fechaFinManGratuito.Text);
                       equipo.actualizarMantenimientoGratuito(codigoEquipo, fechaInicioManGratuito, mesesGratuitos,fechaFinManGratuito);

              }

          /*     int paradas = 0;
               if (tx_paradas.Text != "")
               {
                   paradas = Convert.ToInt32(tx_paradas.Text);
               }


               NEquipo Neq = new NEquipo();
               Neq.modificarEquipo(codigoEquipo, paradas, tx_pasajeros.Text, tx_velocidad.Text, tx_modelo.Text); 
            */       

               if (tx_contratoFirmado.Text != "" && tx_fechaContratoInicio.Text != "")
               {
               string firmaContrato = convertidorFecha(tx_contratoFirmado.Text);
               string nroContrato = tx_nroContrato.Text;

               string monto = "0";
               if (tx_montoContrato.Text != "")
               {
                monto = tx_montoContrato.Text.Replace(",", ".");
                }

               string fechaContratoInicio = convertidorFecha(tx_fechaContratoInicio.Text);
               int mesesContrato = 0;
               if (tx_mesesContrato.Text != "")
               {
                   mesesContrato = Convert.ToInt32(tx_mesesContrato.Text);
               }

               
               tx_fechaContratoFin.Text = aumentarFecha_conMes(tx_fechaContratoInicio.Text,mesesContrato);
               string fechaContratoFin = convertidorFecha(tx_fechaContratoFin.Text);
               NA_fechaContrato nfecha = new NA_fechaContrato();
               nfecha.insertar(firmaContrato, fechaContratoInicio, fechaContratoFin, mesesContrato, codigoEquipo, monto,nroContrato);
               int codigoUltimoInsertado = nfecha.getCodigoUltimoInsertado();
               bool bandera = equipo.actualizarFechaContrato(codigoEquipo,codigoUltimoInsertado);
                   if(bandera == true){
                        Response.Write("<script type='text/javascript'> alert('OK, Datos Actualizados') </script>");    
                   }else
                       Response.Write("<script type='text/javascript'> alert('Error, Datos Erroneos') </script>");    
               
               }else
                   Response.Write("<script type='text/javascript'> alert('La fecha de Contrato Firmado o fecha Contrato Inicio NO ES VALIDA') </script>");
              
           }else
               Response.Write("<script type='text/javascript'> alert('No ha Seleccionado Equipo') </script>");
        }

        protected void gv_contratosFirmados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatoSeleccionado();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            Buscar(HttpUtility.HtmlDecode(tx_edificio.Text), HttpUtility.HtmlDecode(tx_exbo.Text));
        }

        protected void bt_exportarExcel_Click(object sender, EventArgs e)
        {
            NA_Equipo equipo1 = new NA_Equipo();
            DataSet tuplaRes = equipo1.getEquiposContratosFirmados1(HttpUtility.HtmlDecode(tx_edificio.Text), HttpUtility.HtmlDecode(tx_exbo.Text));

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombreDB = Session["BaseDatos"].ToString();
            string nombre = "Fechas de Contratos " + nombreDB;
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = tuplaRes;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }

            }
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

    }
}