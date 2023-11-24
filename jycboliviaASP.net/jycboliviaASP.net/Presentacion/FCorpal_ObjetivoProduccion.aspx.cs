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

namespace jycboliviaASP.net.Presentacion
{
    public partial class FCorpal_ObjetivoProduccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(127) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {
                /*NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                tx_responsableEntrega.Text = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();                
                ponerMedidadelProducto();
                */
                llenarProductosNax();
                buscarDatos("","");
            }            
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

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string fechalimite = convertirFecha(tx_fechalimite.Text);
            string producto = dd_productosNax.SelectedItem.Text;
            buscarDatos(fechalimite, producto);
        }

        private void buscarDatos(string fechalimite, string producto)
        {
            /*string fechalimite = convertirFecha(tx_fechalimite.Text);            
            string producto = dd_productosNax.SelectedItem.Text;
            */
            
            NCorpal_Produccion np = new NCorpal_Produccion();
            DataSet datos = np.get_objetivosDeProduccion(fechalimite,producto);
            gv_objetivoProduccion.DataSource = datos;
            gv_objetivoProduccion.DataBind();
            
        }

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            insertardatos();
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


        private void insertardatos()
        {
            string fechalimite = convertirFecha(tx_fechalimite.Text);

            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            string producto = dd_productosNax.SelectedItem.Text;
            int codprod = pp.get_CodigoProductos(producto);    

            float cantidadprod;
            float.TryParse(tx_cantcajas.Text.Replace('.',','), out cantidadprod );                    
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
            bool bandera = cp.set_objetivoProduccion(fechalimite, codprod, producto, cantidadprod, medida, detalle, codusergra, respgra);
            if (bandera)
            {
                buscarDatos("","");
                Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('ERROR: No Ingreso Datos') </script>");
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            tx_cantcajas.Text = "0";
            tx_detalle.Text = "";
            tx_fechalimite.Text = "0";
            tx_medida.Text = "";
            dd_productosNax.SelectedIndex = 0;

        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            actualizarDatosSistema();
        }

        private void actualizarDatosSistema()
        {
            if (gv_objetivoProduccion.SelectedIndex >= 0) {
                int codigo;
                int.TryParse(gv_objetivoProduccion.SelectedRow.Cells[1].Text, out codigo);

                string fechalimite = convertirFecha(tx_fechalimite.Text);

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
                bool bandera = cp.update_objetivoProduccion(codigo, fechalimite, codprod, producto, cantidadprod, medida, detalle, codusergra, respgra);
                if (bandera)
                {
                    buscarDatos("",""); 
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

                string fechalimite = convertirFecha(tx_fechalimite.Text);                                
                //---------------------------------------
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                //---------------------------------------                

                NCorpal_Produccion cp = new NCorpal_Produccion();
                bool bandera = cp.delete_objetivoProduccion(codigo, codUser);
                if (bandera)
                {
                    buscarDatos("","");
                    Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('ERROR: No Ingreso Datos') </script>");
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

        protected void gv_reciboIngresoEgreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecciondeDatosparaModificar();
        }

        private void selecciondeDatosparaModificar()
        {
            tx_fechalimite.Text = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[2].Text);
            int codProducto;
            int.TryParse(gv_objetivoProduccion.SelectedRow.Cells[3].Text, out codProducto);

            if (codProducto >= 0)
            {
                dd_productosNax.SelectedValue = codProducto.ToString();
            }
            tx_cantcajas.Text = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[5].Text);
            tx_medida.Text = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[8].Text);
            tx_detalle.Text = HttpUtility.HtmlDecode(gv_objetivoProduccion.SelectedRow.Cells[9].Text);                        
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {
            descargarExcel();
        }

        private void descargarExcel()
        {
            string fechalimite = convertirFecha(tx_fechalimite.Text);
            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            string producto = dd_productosNax.SelectedItem.Text;

            NCorpal_Produccion np = new NCorpal_Produccion();
            DataSet datos = np.get_objetivosDeProduccion(fechalimite, producto);

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Objetivos de Produccion - " + Session["BaseDatos"].ToString();
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