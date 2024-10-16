using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Configuration;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Reporting.WebForms;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_OrdenProduccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(121) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {                
                llenarProductosNax();
                ponerMedidadelProducto();
                ponernumeroCorrelativo();
                buscarDatos("");
            }
            
        }

        private void ponernumeroCorrelativo()
        {
            NCorpal_Produccion npp = new NCorpal_Produccion();
            int codigoOrden = npp.get_codigoCorrelativoOrdenProduccion();
            tx_nroproduccion.Text = codigoOrden.ToString();
        }

        private void ponerMedidadelProducto()
        {
            string producto = dd_productosNax.SelectedItem.Text;
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos(producto);
            string medida = tuplas.Tables[0].Rows[0][2].ToString();
            tx_medida.Text = medida;
        }

        private void llenarProductosNax()
        {
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos("");

            dd_productosNax.DataSource = tuplas;
            dd_productosNax.DataValueField = "codigo";
            dd_productosNax.DataTextField = "producto";
            dd_productosNax.AppendDataBoundItems = true;
            dd_productosNax.SelectedIndex = 1;
            dd_productosNax.DataBind();
        }

        private void buscarDatos(string Producto)
        {
            NCorpal_Produccion npro = new NCorpal_Produccion();
            DataSet tupla = npro.get_datosOrdenProduccion(Producto);
            gv_OrdendeProduccion.DataSource = tupla;
            gv_OrdendeProduccion.DataBind();
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

        private void limpiarDatos()
        {
            tx_nroproduccion.Text = "";            
            tx_detalle.Text = "";            
            tx_medida.Text = "";
            tx_fechaProduccion.Text = "";
            tx_cantTurnoDia.Text = "";
            tx_cantTurnoTarde.Text = "";
            tx_cantTurnoNoche.Text = "";
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string producto = dd_productosNax.SelectedItem.Text;
            buscarDatos(producto);
        }

        protected void dd_productosNax_SelectedIndexChanged(object sender, EventArgs e)
        {
            ponerMedidadelProducto();
        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            insertarDatosOrdenProduccion();
        }

        private void insertarDatosOrdenProduccion()
        {
           string nroProduccion = tx_nroproduccion.Text;           
           string detalleProduccion = tx_detalle.Text;
           string medidaProduccion = tx_medida.Text;
           string fechaProduccion = convertidorFecha(tx_fechaProduccion.Text);           
           int codProducto;
           int.TryParse(dd_productosNax.SelectedValue.ToString(), out codProducto);
           string producto = dd_productosNax.SelectedItem.Text;

           NA_Responsables Nresp = new NA_Responsables();
           string usuarioAux = Session["NameUser"].ToString();
           string passwordAux = Session["passworuser"].ToString();
           int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
           string responsable = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();

            float cantTurnoDia ;
            float.TryParse(tx_cantTurnoDia.Text.Replace(".",","), out cantTurnoDia);
            float cantTurnoTarde ;
            float.TryParse(tx_cantTurnoTarde.Text.Replace(".", ","), out cantTurnoTarde);
            float cantTurnoNoche ;
            float.TryParse(tx_cantTurnoNoche.Text.Replace(".", ","), out cantTurnoNoche);

          //  if (cantTurnoDia > 0 && cantTurnoTarde > 0 && cantTurnoNoche > 0) {
                NCorpal_Produccion nproduccion = new NCorpal_Produccion();
                float cantcajas;
                cantcajas = cantTurnoDia + cantTurnoTarde + cantTurnoNoche;

                bool bandera = nproduccion.insertarOrdenProduccion(fechaProduccion, codProducto, producto, cantcajas, medidaProduccion, detalleProduccion, codUser, responsable, cantTurnoDia,cantTurnoTarde,cantTurnoNoche);
                if (bandera == true)
                {
                int codigoOrden = nproduccion.get_codigoOrdenProduccionUltimoInsertado(codProducto, codUser);
                Session["codigoOrdenProduccion"] = codigoOrden;
                Session["ReporteGeneral"] = "CalcularInsumosPorTurnoDia";
                Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");
                //buscarDatos("");                
                //limpiarDatos();
                //Response.Write("<script type='text/javascript'> alert('Guardado: OK!') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Error Insertar') </script>");
           /* }else
                Response.Write("<script type='text/javascript'> alert('Error: Cantidad Turno Vacio') </script>");
           */


        }

        

            private string convertidorFecha(string fecha)
        {
            if (fecha == "" || fecha == "&nbsp;")
            {
                return fecha = "null";
            }

            else
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = "'" + anio + "/" + mes + "/" + dia + "'";
                return _fecha;
            }
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarDatosOrdenProduccion();
        }

        private void eliminarDatosOrdenProduccion()
        {
            if(gv_OrdendeProduccion.SelectedIndex >= 0 ){
                int codigoOrden;
                int.TryParse(gv_OrdendeProduccion.SelectedRow.Cells[1].Text , out codigoOrden);
                NCorpal_Produccion nproduccion = new NCorpal_Produccion();
                bool bandera = nproduccion.eliminar_ordenProduccion(codigoOrden);
                if (bandera == true)
                {
                    buscarDatos("");
                    Response.Write("<script type='text/javascript'> alert('Eliminado: OK!') </script>");
                }else
                    Response.Write("<script type='text/javascript'> alert('Error: Error Eliminacion') </script>");
            }else                
                Response.Write("<script type='text/javascript'> alert('Error: Seleccione una Orden para Modificar') </script>");
        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            if(gv_OrdendeProduccion.SelectedIndex >= 0){
                modificarDatosOrdenProduccion();            
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Seleccione una Orden para Modificar') </script>");
        }

        private void modificarDatosOrdenProduccion()
        {
            int codigoOrden;
            int.TryParse(gv_OrdendeProduccion.SelectedRow.Cells[1].Text, out codigoOrden);

            string nroProduccion = tx_nroproduccion.Text;            
            string detalleProduccion = tx_detalle.Text;
            string medidaProduccion = tx_medida.Text;
            string fechaProduccion = convertidorFecha(tx_fechaProduccion.Text);
            int codProducto;
            int.TryParse(dd_productosNax.SelectedValue.ToString(), out codProducto);
            string producto = dd_productosNax.SelectedItem.Text;

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            string responsable = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();

            float cantTurnoDia;
            float.TryParse(tx_cantTurnoDia.Text.Replace(".", ","), out cantTurnoDia);
            float cantTurnoTarde;
            float.TryParse(tx_cantTurnoTarde.Text.Replace(".", ","), out cantTurnoTarde);
            float cantTurnoNoche;
            float.TryParse(tx_cantTurnoNoche.Text.Replace(".", ","), out cantTurnoNoche);

          /*  if (cantTurnoDia > 0 && cantTurnoTarde > 0 && cantTurnoNoche > 0)
            { */
                NCorpal_Produccion nproduccion = new NCorpal_Produccion();
                float cantcajas;
                cantcajas = cantTurnoDia + cantTurnoTarde + cantTurnoNoche;
                bool bandera = nproduccion.modificarOrdenProduccion(codigoOrden, fechaProduccion, codProducto, producto, cantcajas, medidaProduccion, detalleProduccion, codUser, responsable,cantTurnoDia,cantTurnoTarde,cantTurnoNoche);
                if (bandera == true)
                {
                    buscarDatos("");
                    Response.Write("<script type='text/javascript'> alert('Modificado: OK!') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error: Error Modificacion') </script>");
          /*  }else
                Response.Write("<script type='text/javascript'> alert('Error: Cantidad Turno Vacio') </script>");
            */
        }

        protected void gv_reciboIngresoEgreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionarDatosOrdenProduccion();
        }

        private void seleccionarDatosOrdenProduccion()
        {
            if(gv_OrdendeProduccion.SelectedIndex >= 0){
                int codigoOrden;
                int.TryParse(gv_OrdendeProduccion.SelectedRow.Cells[1].Text, out codigoOrden);
                tx_nroproduccion.Text = codigoOrden.ToString();
                /*
                float cantcajas;
                float.TryParse(gv_OrdendeProduccion.SelectedRow.Cells[5].Text, out cantcajas);
                tx_cantcajas.Text = cantcajas.ToString();
                */
                tx_detalle.Text = HttpUtility.HtmlDecode(gv_OrdendeProduccion.SelectedRow.Cells[7].Text);
                tx_medida.Text = HttpUtility.HtmlDecode(gv_OrdendeProduccion.SelectedRow.Cells[6].Text);
                tx_fechaProduccion.Text = HttpUtility.HtmlDecode(gv_OrdendeProduccion.SelectedRow.Cells[2].Text);

                float cantturnodia;
                float.TryParse(gv_OrdendeProduccion.SelectedRow.Cells[9].Text, out cantturnodia);
                tx_cantTurnoDia.Text = cantturnodia.ToString();

                float cantturnotarde;
                float.TryParse(gv_OrdendeProduccion.SelectedRow.Cells[10].Text, out cantturnotarde);
                tx_cantTurnoTarde.Text = cantturnotarde.ToString();

                float cantturnonoche;
                float.TryParse(gv_OrdendeProduccion.SelectedRow.Cells[11].Text, out cantturnonoche);
                tx_cantTurnoNoche.Text = cantturnonoche.ToString();

                NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
                int codProducto = pp.get_CodigoProductos(HttpUtility.HtmlDecode(gv_OrdendeProduccion.SelectedRow.Cells[4].Text));
                dd_productosNax.SelectedValue = codProducto.ToString();
                
            }
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {
            descargarExcel();
        }

        private void descargarExcel()
        {
            string producto = dd_productosNax.SelectedItem.Text; ;
            NCorpal_Produccion npro = new NCorpal_Produccion();
            DataSet tupla = npro.get_datosOrdenProduccion(producto);


            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Orden de Produccion - " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = tupla;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }   
        }

     
    }
}