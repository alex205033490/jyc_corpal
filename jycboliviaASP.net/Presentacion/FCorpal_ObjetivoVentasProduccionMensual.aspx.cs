using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ObjetivoVentasProduccionMensual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(128) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {
               
                llenarProductosNax();
                seleccionarMesyAnioActual();
                buscarDatos(-1, -1, "");
            }   
        }

        private void seleccionarMesyAnioActual()
        {
            // Get the current date.
            DateTime thisDay = DateTime.Today;
            int mes = thisDay.Month-1;
            int anio = thisDay.Year;
            dd_mes.SelectedIndex = mes;
            tx_anio.Text = anio.ToString();
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

        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(permiso, codUser);
        }

        public string convertirFecha(string fecha)
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

        protected void dd_productosNax_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatosSeleccionado();
        }

        private void cargarDatosSeleccionado()
        {
            string producto = dd_productosNax.SelectedItem.Text;
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos(producto);
            string medida = tuplas.Tables[0].Rows[0][2].ToString();
            tx_medida.Text = medida;
        }

        private void buscarDatos(int Mes, int anio, string producto)
        {            
                NCorpal_Produccion np = new NCorpal_Produccion();
                DataSet datos = np.get_objetivosDeProduccionMensual(Mes, anio, producto);
                gv_objetivoProduccion.DataSource = datos;
                gv_objetivoProduccion.DataBind();  
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_cantcajas.Text = "0";
            tx_detalle.Text = "";
            dd_mes.SelectedIndex = 0;
            tx_anio.Text = "0";
            tx_medida.Text = "";
            dd_productosNax.SelectedIndex = 0;

        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            insertardatos();
        }

        private void insertardatos()
        {
            int codMes;
            int.TryParse(dd_mes.SelectedIndex.ToString(), out codMes);
            codMes = codMes + 1;
            string mesTexto = dd_mes.SelectedItem.Text;
            int anio;
            int.TryParse(tx_anio.Text, out anio);

            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            string producto = dd_productosNax.SelectedItem.Text;
            int codprod = pp.get_CodigoProductos(producto);

            float cantidadprod;
            float.TryParse(tx_cantcajas.Text.Replace('.', ','), out cantidadprod);
            string medida = tx_medida.Text;
            string detalle = tx_detalle.Text;
            //---------------------------------------
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            //---------------------------------------
            int codusergra = codUser;
            string respgra = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();

            NCorpal_Produccion cpp = new NCorpal_Produccion();
            bool bandera = cpp.set_objetivoProduccionMensual(codMes, mesTexto, anio, codprod, producto, cantidadprod, medida, detalle, codusergra, respgra);
            if (bandera)
            {
                //-----------------envio de correo---------
                string asunto = "(Corpal) Objetivo de Ventas Mensual Agregado por " + respgra;
                string Cuerpo = "Se ha incorporado objetivo de Ventas <br><br>" +
                    "Producto = " + producto + "; <br>" +
                    "Medida = " + medida + "; <br>" +
                    "Mes = " + mesTexto + "; <br>" +
                    "Año = " + anio + "; <br>" +
                    "Detalle = " + detalle + "; <br>" +
                    "Responsable = " + respgra;
                NA_EnvioCorreo nenvio = new NA_EnvioCorreo();
                string baseDatos = Session["BaseDatos"].ToString();
                bool correoOK = nenvio.Enviar_Correo_objetivosVentas(asunto, Cuerpo);
                //-------------------------------------
                buscarDatos(codMes, anio, "");
                Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('ERROR: No Ingreso Datos') </script>");
        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            actualizarDatosSistema();
        }

        private void actualizarDatosSistema()
        {
            if (gv_objetivoProduccion.SelectedIndex >= 0)
            {
                int codigo;
                int.TryParse(gv_objetivoProduccion.SelectedRow.Cells[1].Text, out codigo);

                int codMes;
                int.TryParse(dd_mes.SelectedIndex.ToString(), out codMes);
                codMes = codMes + 1;
                string mesTexto = dd_mes.SelectedItem.Text;
                int anio;
                int.TryParse(tx_anio.Text, out anio);

                NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
                string producto = dd_productosNax.SelectedItem.Text;
                int codprod = pp.get_CodigoProductos(producto);

                float cantidadprod;
                float.TryParse(tx_cantcajas.Text.Replace('.', ','), out cantidadprod);
                string medida = tx_medida.Text;
                string detalle = tx_detalle.Text;
                //---------------------------------------
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                //---------------------------------------
                int codusergra = codUser;
                string respgra = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();

                NCorpal_Produccion cp = new NCorpal_Produccion();
                bool bandera = cp.update_objetivoProduccionMensual(codigo, codMes, mesTexto, anio, codprod, producto, cantidadprod, medida, detalle, codusergra, respgra);
                if (bandera)
                {
                    //-----------------envio de correo---------
                    string asunto = "(Corpal) Modificado-Objetivo de Ventas Mensual por " + respgra;
                    string Cuerpo = "Se ha Modificado el objetivo de Ventas Mensual <br><br>" +
                        "Codigo = " + codigo + "; <br>" +
                        "Producto = " + producto + "; <br>" +
                        "Medida = " + medida + "; <br>" +
                        "Mes = " + mesTexto + "; <br>" +
                        "Año = " + anio + "; <br>" +
                        "Responsable = " + respgra;
                    NA_EnvioCorreo nenvio = new NA_EnvioCorreo();
                    string baseDatos = Session["BaseDatos"].ToString();
                    bool correoOK = nenvio.Enviar_Correo_objetivosVentas(asunto, Cuerpo);
                    //-------------------------------------
                    buscarDatos(codMes,anio, "");
                    Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('ERROR: No Ingreso Datos') </script>");
            }
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarDatos();
        }

        private void eliminarDatos()
        {
            if (gv_objetivoProduccion.SelectedIndex >= 0)
            {
                int codigo;
                int.TryParse(gv_objetivoProduccion.SelectedRow.Cells[1].Text, out codigo);
                               
                //---------------------------------------
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                //---------------------------------------         
                int codMes;
                int.TryParse(dd_mes.SelectedIndex.ToString(), out codMes);
                codMes = codMes + 1;
                string mesTexto = dd_mes.SelectedItem.Text;
                int anio;
                int.TryParse(tx_anio.Text, out anio);

                NCorpal_Produccion cp = new NCorpal_Produccion();
                bool bandera = cp.delete_objetivoProduccionMensual(codigo, codUser);
                if (bandera)
                {
                    buscarDatos(codMes,anio, "");
                    Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('ERROR: No Ingreso Datos') </script>");
            }
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            int codMes;
            int.TryParse(dd_mes.SelectedIndex.ToString(), out codMes);
            codMes = codMes + 1;
            string mesTexto = dd_mes.SelectedItem.Text;
            int anio;
            int.TryParse(tx_anio.Text, out anio);
            string producto = dd_productosNax.SelectedItem.Text;

            buscarDatos(codMes, anio, "");
        }

        protected void gv_reciboIngresoEgreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecciondeDatosparaModificar();
        }

        private void selecciondeDatosparaModificar()
        {
            tx_anio.Text = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[5].Text);
            dd_mes.SelectedValue = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[4].Text);
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            string producto = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[6].Text);
            int codprod = pp.get_CodigoProductos(producto);

            if (codprod >= 0)
            {
                dd_productosNax.SelectedValue = codprod.ToString();
            }
            tx_cantcajas.Text = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[7].Text);
            tx_medida.Text = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[8].Text);
            tx_detalle.Text = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[9].Text);
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {
            descargarExcel();
        }

        private void descargarExcel()
        {
            int codMes;
            int.TryParse(dd_mes.SelectedIndex.ToString(), out codMes);
            codMes = codMes + 1;
            string mesTexto = dd_mes.SelectedItem.Text;
            int anio;
            int.TryParse(tx_anio.Text, out anio);
            string producto = dd_productosNax.SelectedItem.Text;

            NCorpal_Produccion np = new NCorpal_Produccion();
            
            if (! (gv_objetivoProduccion.SelectedIndex >= 0))

            {
                codMes = 0;
                anio = 0;
                producto = "";
            }
            DataSet datos = np.get_objetivosDeProduccionMensual(codMes,anio, producto);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Objetivos de Produccion Mensual - " + Session["BaseDatos"].ToString();
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombre + ".xls" + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = datos;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }
    }
}
