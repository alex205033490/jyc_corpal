using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using jycboliviaASP.net.Negocio;
using System.Configuration;
using System.IO;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_Facturacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(45) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            } 

            lb_titulo.Text = "Movimientos de Facturas " + Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                cargarfacturas("");
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

        private void cargarfacturas(string nroFactura)
        {
            NA_Facturacion Nfacturacion = new NA_Facturacion();
            DataSet datos = Nfacturacion.mostrarfacturas(nroFactura);
            gv_movimientosFacturas.DataSource = datos;
            gv_movimientosFacturas.DataBind();
        }

        public void limpiar()
        {            
            tx_codigoControl.Text = "";
            tx_detalle.Text = "";
            tx_fechaFacturacion.Text = "";
            tx_fechalimite.Text = "";
            tx_montoTotal.Text = "";
            tx_nit_ci.Text = "";
            tx_nombreFactura.Text = "";
            tx_nroAutorizacion.Text = "";
            tx_nroFactura.Text = "";

        }


        private void GuardarArchivo(HttpPostedFile file)
        {
            // Se carga la ruta física de la carpeta temp del sitio            
            string ruta = ConfigurationManager.AppSettings["RutaCuadroXXX"] + Session["BaseDatos"].ToString();
            string codProy = gv_movimientosFacturas.SelectedRow.Cells[1].Text;
            string nombreFactura = gv_movimientosFacturas.SelectedRow.Cells[5].Text;
            ruta = ruta + "/" + codProy + "_" + nombreFactura;
            // Si el directorio no existe, crearlo
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            string archivo = String.Format("{0}\\{1}", ruta, file.FileName);
            file.SaveAs(archivo);
        }


        public void cargarDocumentos()
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    if (gv_movimientosFacturas.SelectedIndex > -1)
                    {
                        GuardarArchivo(FileUpload1.PostedFile);

                        NA_Historial nhistorial = new NA_Historial();
                        int codUser = Convert.ToInt32(Session["coduser"].ToString());
                        nhistorial.insertar(codUser, "Se ha Cargado el archivo" + FileUpload1.PostedFile.FileName);
                        Response.Write("<script type='text/javascript'> alert('Archivo Subido') </script>");
                    }
                    else
                        Response.Write("<script type='text/javascript'> alert('ERROR: Seleccione un Proyecto') </script>");

                }
                else
                    Response.Write("<script type='text/javascript'> alert('ERROR: Seleccione un archivo del disco duro') </script>");
            }
            catch (Exception)
            {
                Response.Write("<script type='text/javascript'> alert('Error'+ex ) </script>");
            }
        }


        protected void bt_adicionar_Click(object sender, EventArgs e)
        {
            cargarDocumentos();
        }

        protected void bt_guardar_Click(object sender, EventArgs e)
        {
            string fechaFactura = aFecha(tx_fechaFacturacion.Text);
            string nroFactura = tx_nroFactura.Text;
            string nroAutorizacion = tx_nroAutorizacion.Text;
            string nombreFactura = tx_nombreFactura.Text;
            string nitci = tx_nit_ci.Text;
            string montototal = tx_montoTotal.Text;
            string codigoControl = tx_codigoControl.Text;
            string fechalimite = aFecha(tx_fechalimite.Text);
            string detalle = tx_detalle.Text;

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_Facturacion nfacturacion = new NA_Facturacion();
            if (nfacturacion.insertFacturacion(fechaFactura, nroFactura, nroAutorizacion, nombreFactura, nitci, montototal, codigoControl, fechalimite, detalle))
            {
                //----------------historial-------------
                NA_Historial nhistorial = new NA_Historial();
                nhistorial.insertar(CodUser, "Se ha insertado una factura " + nroFactura);
                //--------------------------------------
                Response.Write("<script type='text/javascript'> alert('El Dato Ha sido Guardado Correctamente') </script>");
                cargarfacturas("");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error') </script>");
        }

        protected void bt_modificar_Click(object sender, EventArgs e)
        {
            if (gv_movimientosFacturas.SelectedIndex > -1)
            {
                int codigo = Convert.ToInt32(gv_movimientosFacturas.SelectedRow.Cells[1].Text);
                string fechaFactura = aFecha(tx_fechaFacturacion.Text);
                string nroFactura = tx_nroFactura.Text;
                string nroAutorizacion = tx_nroAutorizacion.Text;
                string nombreFactura = tx_nombreFactura.Text;
                string nitci = tx_nit_ci.Text;
                string montototal = tx_montoTotal.Text;
                string codigoControl = tx_codigoControl.Text;
                string fechalimite = aFecha(tx_fechalimite.Text);
                string detalle = tx_detalle.Text;

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                NA_Facturacion nfacturacion = new NA_Facturacion();
                if (nfacturacion.modificar(codigo, fechaFactura, nroFactura, nroAutorizacion, nombreFactura, nitci, montototal, codigoControl, fechalimite, detalle))
                {
                    //----------------historial-------------
                    NA_Historial nhistorial = new NA_Historial();
                    nhistorial.insertar(CodUser, "Se ha modificado la factura " + nroFactura);
                    //--------------------------------------
                    Response.Write("<script type='text/javascript'> alert('El Dato Ha sido Guardado Correctamente') </script>");
                    cargarfacturas("");
                }else
                    Response.Write("<script type='text/javascript'> alert('Error') </script>");
            }
        }

        protected void gv_movimientosFacturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionFacturas();
            leerDocumentos();
        }

        private void seleccionFacturas()
        {
            tx_fechaFacturacion.Text = gv_movimientosFacturas.SelectedRow.Cells[2].Text;
            tx_nroFactura.Text = gv_movimientosFacturas.SelectedRow.Cells[3].Text;
            tx_nroAutorizacion.Text = gv_movimientosFacturas.SelectedRow.Cells[4].Text;
            tx_nombreFactura.Text = gv_movimientosFacturas.SelectedRow.Cells[5].Text;
            tx_nit_ci.Text = gv_movimientosFacturas.SelectedRow.Cells[6].Text;
            tx_montoTotal.Text = gv_movimientosFacturas.SelectedRow.Cells[7].Text; 
            tx_codigoControl.Text = gv_movimientosFacturas.SelectedRow.Cells[8].Text;
            tx_fechalimite.Text = gv_movimientosFacturas.SelectedRow.Cells[9].Text;
            tx_detalle.Text = gv_movimientosFacturas.SelectedRow.Cells[10].Text;
        }

        private void leerDocumentos()
        {
            if (gv_movimientosFacturas.SelectedIndex > -1)
            {
                // Se carga la ruta física de la carpeta temp del sitio            
                string ruta = ConfigurationManager.AppSettings["RutaCuadroXXX"] + Session["BaseDatos"].ToString();
                string codProy = gv_movimientosFacturas.SelectedRow.Cells[1].Text;
                string nombreFactura = gv_movimientosFacturas.SelectedRow.Cells[5].Text;
                ruta = ruta + "/" + codProy + "_" + nombreFactura;
                // Si el directorio no existe, crearlo
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                string[] files = Directory.GetFiles(ruta);
                string[] documentos = new string[files.Length];
                for (int i = 0; i < documentos.Length; i++)
                {
                    string aux = files[i].ToString().Replace("\\", "/");
                    int inicio = aux.LastIndexOf("/");
                    documentos[i] = aux.Substring(inicio + 1);
                }
                gv_adjuntofactura.DataSource = documentos;
                gv_adjuntofactura.DataBind();
            }
        }


        private void descargarArchivo()
        {
            try
            {
                string ruta = ConfigurationManager.AppSettings["RutaCuadroXXX"] + Session["BaseDatos"].ToString();
                string codProy = gv_movimientosFacturas.SelectedRow.Cells[1].Text;
                string nombreFactura = gv_movimientosFacturas.SelectedRow.Cells[5].Text;
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                ruta = ruta + "/" + codProy + "_" + nombreFactura + "/" + gv_adjuntofactura.SelectedRow.Cells[1].Text;
                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=" + gv_adjuntofactura.SelectedRow.Cells[1].Text);
                // Escribimos el fichero a enviar 
                Response.WriteFile(ruta);
                // volcamos el stream 
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write("<script type='text/javascript'> alert('ERROR: " + ex.Message + "') </script>");

            }
        }


        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            if (gv_movimientosFacturas.SelectedIndex > -1)
            {
                int codigo = Convert.ToInt32(gv_movimientosFacturas.SelectedRow.Cells[1].Text);

                NA_Responsables Nresp = new NA_Responsables();
                string usuarioAux = Session["NameUser"].ToString();
                string passwordAux = Session["passworuser"].ToString();
                int CodUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

                NA_Facturacion nfacturacion = new NA_Facturacion();
                if (nfacturacion.eliminar(codigo))
                {
                    //----------------historial-------------
                    NA_Historial nhistorial = new NA_Historial();
                    nhistorial.insertar(CodUser, "Se ha eliminado la factura " + codigo);
                    //--------------------------------------
                    Response.Write("<script type='text/javascript'> alert('El Dato Ha sido eliminado Correctamente') </script>");
                    cargarfacturas("");
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Error') </script>");
            }
        }

        protected void gv_adjuntofactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            descargarArchivo();
        }

        protected void bt_limpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            cargarfacturas(tx_nroFactura.Text);
        }
    }
}