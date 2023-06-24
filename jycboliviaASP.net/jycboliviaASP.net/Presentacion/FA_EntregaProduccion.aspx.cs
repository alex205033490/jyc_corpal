using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_OrdendeProduccion : System.Web.UI.Page
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
                buscarDatos("","");
            }

            permisodemodificaryeliminar();

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            tx_responsableEntrega.Text = Nresp.get_responsable(codUser).Tables[0].Rows[0][1].ToString();
        }

        private void permisodemodificaryeliminar()
        {
            if (tienePermisoDeIngreso(122) == true)
            {
                bt_modificar.Visible = true;
                bt_eliminar.Visible = true;
            }
            else {
                bt_modificar.Visible = false;
                bt_eliminar.Visible = false;
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


        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaResponsable2(string prefixText, int count)
        {
            string nombreResponsable = prefixText;

            NA_Responsables Nrespon = new NA_Responsables();
            DataSet tuplas = Nrespon.mostrarTodos_AutoComplit(nombreResponsable);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
        }

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProductos(string prefixText, int count)
        {
            string nombreProducto = prefixText;

            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            DataSet tuplas = pp.get_mostrarProductos(nombreProducto);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;
        }

        public string aFecha(string fecha)
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

        protected void bt_insertar_Click(object sender, EventArgs e)
        {
            insertarDatosEntregaProducto();
        }

        private void insertarDatosEntregaProducto()
        {           
            string turno = dd_turno.SelectedItem.Text;            
            float cantcajas;
            float.TryParse(tx_cantcajas.Text.Replace('.',','), out cantcajas );
            float unidadsuelta;
            float.TryParse(tx_unidadsuelta.Text.Replace('.',','), out unidadsuelta);
            float kgrdesperdicio;
            float.TryParse(tx_kgrdesperdicio.Text.Replace('.',','), out kgrdesperdicio);
            float kgrparamix;
            float.TryParse(tx_kgrparamix.Text.Replace('.',','), out kgrparamix);
            string nroorden = tx_nroOrden.Text;
            string detalleentrega = tx_detalle.Text;
            //---------------------------------------
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
            //---------------------------------------
            string respEntrega = tx_responsableEntrega.Text;
            int codrespEntrega = Nresp.getCodigo_NombreResponsable(respEntrega);
            string respRecepcion = tx_recepcionProduccion.Text;
            int codRespRecepcionProduccion = Nresp.getCodigo_NombreResponsable(respRecepcion);


            NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
            string productoNAX = tx_productoNax.Text;
            int codigoProdNax = pp.get_CodigoProductos(productoNAX);

            NCorpal_Produccion npro = new NCorpal_Produccion();
            bool bandera = npro.insertarEntregaProduccion(nroorden, turno, codrespEntrega, respEntrega, cantcajas, unidadsuelta, kgrdesperdicio, kgrparamix, detalleentrega, codigoProdNax, productoNAX, codRespRecepcionProduccion, respRecepcion, codUser);
            
            if(bandera){
                int codigoEntregaProduccion = npro.get_ultimoInsertadoEntregaProduccion(respEntrega);
                limpiarDatos();
                Session["codigoEntregaProduccion"] = codigoEntregaProduccion;
                Session["ReporteGeneral"] = "Reporte_Entrega_Produccion";
                Response.Redirect("../Presentacion/FCorpal_ReporteGeneral.aspx");
                //buscarDatos(turno,respEntrega);
            }else
                Response.Write("<script type='text/javascript'> alert('ERROR: No Ingreso Datos') </script>");
        }

        private void buscarDatos(string turno, string respEntrega)
        {
            NCorpal_Produccion npro = new NCorpal_Produccion();
            DataSet tupla = npro.mostrarEmpregasProduccion(turno, respEntrega);
            gv_EntregasdeProduccion.DataSource = tupla;
            gv_EntregasdeProduccion.DataBind();

        }

        private void limpiarDatos()
        {
            dd_turno.SelectedIndex = -1;
            tx_responsableEntrega.Text = "";
            tx_cantcajas.Text = "";
            tx_unidadsuelta.Text = "";
            tx_kgrdesperdicio.Text = "";
            tx_kgrparamix.Text = "";
            tx_nroOrden.Text = "";
            tx_detalle.Text = "";
            tx_productoNax.Text = "";
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
            if(gv_EntregasdeProduccion.SelectedIndex >= 0){
                int codigo;
                int.TryParse(gv_EntregasdeProduccion.SelectedRow.Cells[1].Text , out codigo);
                string turno = dd_turno.SelectedItem.Text;                
                float cantcajas;
                float.TryParse(tx_cantcajas.Text.Replace('.', ','), out cantcajas);
                float unidadsuelta;
                float.TryParse(tx_unidadsuelta.Text.Replace('.', ','), out unidadsuelta);
                float kgrdesperdicio;
                float.TryParse(tx_kgrdesperdicio.Text.Replace('.', ','), out kgrdesperdicio);
                float kgrparamix;
                float.TryParse(tx_kgrparamix.Text.Replace('.', ','), out kgrparamix);
                string nroorden = tx_nroOrden.Text;
                string detalleentrega = tx_detalle.Text;

                //---------------------------------------
                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);
                //---------------------------------------
                NCorpal_SolicitudEntregaProducto pp = new NCorpal_SolicitudEntregaProducto();
                string productoNAX = tx_productoNax.Text;
                int codigoProdNax = pp.get_CodigoProductos(productoNAX);

                string respEntrega = tx_responsableEntrega.Text;
                int codrespEntrega = Nresp.getCodigo_NombreResponsable(respEntrega);
                string respRecepcion = tx_recepcionProduccion.Text;
                int codRespRecepcionProduccion = Nresp.getCodigo_NombreResponsable(respRecepcion);


                NCorpal_Produccion npro = new NCorpal_Produccion();
                bool bandera = npro.modificarEntregaProduccion(codigo, nroorden, turno, codrespEntrega, respEntrega, cantcajas, unidadsuelta, kgrdesperdicio, kgrparamix, detalleentrega, codigoProdNax, productoNAX, codRespRecepcionProduccion, respRecepcion, codUser);
                if (bandera)
                {
                    limpiarDatos();
                    buscarDatos(turno, respEntrega);
                }
                else
                    Response.Write("<script type='text/javascript'> alert('ERROR: No Ingreso Datos') </script>");            
            }else
                Response.Write("<script type='text/javascript'> alert('ERROR: Seleccione datos') </script>");
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarDatos();
        }

        private void eliminarDatos()
        {
            if (gv_EntregasdeProduccion.SelectedIndex >= 0)
            {
                int codigo;
                int.TryParse(gv_EntregasdeProduccion.SelectedRow.Cells[1].Text, out codigo);

                NCorpal_Produccion npro = new NCorpal_Produccion();
                bool bandera = npro.eliminarEntregaProduccion(codigo);
                if (bandera)
                {
                    limpiarDatos();
                    buscarDatos("", "");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('ERROR: Error Guardado') </script>");
            }
            else
                Response.Write("<script type='text/javascript'> alert('ERROR: Seleccione datos') </script>");
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            buscarDatos("","");
        }

        protected void gv_reciboIngresoEgreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionDatosProduccion();
        }

        private void SeleccionDatosProduccion()
        {
            dd_turno.SelectedValue = HttpUtility.HtmlDecode(gv_EntregasdeProduccion.SelectedRow.Cells[2].Text);
            tx_responsableEntrega.Text =  HttpUtility.HtmlDecode(gv_EntregasdeProduccion.SelectedRow.Cells[5].Text);            
            tx_cantcajas.Text = HttpUtility.HtmlDecode(gv_EntregasdeProduccion.SelectedRow.Cells[6].Text);           
            tx_unidadsuelta.Text =  HttpUtility.HtmlDecode(gv_EntregasdeProduccion.SelectedRow.Cells[7].Text);           
            tx_kgrdesperdicio.Text = HttpUtility.HtmlDecode(gv_EntregasdeProduccion.SelectedRow.Cells[8].Text);           
            tx_kgrparamix.Text = HttpUtility.HtmlDecode(gv_EntregasdeProduccion.SelectedRow.Cells[9].Text);            
            tx_detalle.Text =  HttpUtility.HtmlDecode(gv_EntregasdeProduccion.SelectedRow.Cells[12].Text);
            tx_nroOrden.Text = HttpUtility.HtmlDecode(gv_EntregasdeProduccion.SelectedRow.Cells[13].Text);
            tx_productoNax.Text = HttpUtility.HtmlDecode(gv_EntregasdeProduccion.SelectedRow.Cells[14].Text);
            tx_recepcionProduccion.Text = HttpUtility.HtmlDecode(gv_EntregasdeProduccion.SelectedRow.Cells[17].Text);
        }

        protected void bt_excel_Click(object sender, EventArgs e)
        {
            descargarExcelDatos();
        }

        private void descargarExcelDatos()
        {
            string turno = dd_turno.SelectedItem.Text;
            string respEntrega = tx_responsableEntrega.Text;

            NCorpal_Produccion npro = new NCorpal_Produccion();
            DataSet tupla = npro.mostrarEmpregasProduccion("", respEntrega);
            

            //// Creacion del Excel
            HttpResponse response = HttpContext.Current.Response;
            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            string nombre = "Entrega Produccion - " + Session["BaseDatos"].ToString();
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