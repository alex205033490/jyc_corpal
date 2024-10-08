using jycboliviaASP.net.Negocio;
using jycboliviaASP.net.NegocioApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FUpon_CargaVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(132) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                buscar("");
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


        private void buscar(string glosa)
        {
            NUpon_Ventas NUpon = new NUpon_Ventas();
            DataSet resultMostrar = NUpon.mostrarVentasRealizadas(glosa);
            gv_ventasCargadas.DataSource = resultMostrar;
            gv_ventasCargadas.DataBind();
        }

        private void GuardarArchivo(HttpPostedFile file)
        {
            // Se carga la ruta física de la carpeta temp del sitio            
            string ruta = ConfigurationManager.AppSettings["ruta_ArchivoVenta"];
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
                    // Se verifica que la extensión sea de un formato válido
                    string ext = FileUpload1.PostedFile.FileName;
                    ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
                    //string[] formatos = new string[] { "xlsx", "xls","csv"};
                    string[] formatos = new string[] { "csv" };
                    if (Array.IndexOf(formatos, ext) < 0)
                        //    MensajeError("Formato de imagen inválido.");
                        Response.Write("<script type='text/javascript'> alert('Formato del Archivo inválido.') </script>");
                    else
                    {
                        //Response.Write("<script type='text/javascript'> alert('entro a guardar') </script>");
                        GuardarArchivo(FileUpload1.PostedFile);
                        // Response.Write("<script type='text/javascript'> alert('guardo el Archivo') </script>");
                        NUpon_Ventas Nupon = new NUpon_Ventas();
                        string ruta = ConfigurationManager.AppSettings["ruta_ArchivoVenta"] + FileUpload1.PostedFile.FileName;
                        bool bandera = Nupon.insertarVentasRealizadas(ruta);
                        if (bandera)
                        {
                            NA_Historial nhistorial = new NA_Historial();
                            int codUser = Convert.ToInt32(Session["coduser"].ToString());
                            nhistorial.insertar(codUser, "Se ha Cargado Ventas Realizadas archivo" + FileUpload1.PostedFile.FileName);
                            Response.Write("<script type='text/javascript'> alert('guardo la Base de datos') </script>");
                            cargarTablaCompras("");
                        }

                        //  Response.Write("<script type='text/javascript'> alert('inserto =   nombre archivo = "+ruta+"') </script>");
                    }
                    //  GuardarArchivo(fileUploader1.PostedFile);

                    //else
                    //  GuardarBD(fileUploader1.PostedFile);
                }
                else
                    Response.Write("<script type='text/javascript'> alert('Seleccione un archivo del disco duro') </script>");
                //MensajeError("Seleccione un archivo del disco duro.");
            }
            catch (Exception)
            {
                //MensajeError(ex.Message);
                Response.Write("<script type='text/javascript'> alert('Error') </script>");
            }
        }

        private void cargarTablaCompras(string glosa)
        {
            NUpon_Ventas NUpon = new NUpon_Ventas();
            DataSet resultMostrar = NUpon.mostrarVentasRealizadas(glosa);
            gv_ventasCargadas.DataSource = resultMostrar;
            gv_ventasCargadas.DataBind();
        }

        private void vaciarUpon_seleccionado()
        {
            NUpon_Ventas nupon = new NUpon_Ventas();
            NA_endpoints napi = new NA_endpoints();
            string admin = tx_usuario.Text;
            string pass = tx_passUsuario.Text;
            bool banderaAut = napi.get_AutenticarUsuario(admin, pass);
            if (banderaAut)
            {
                string token = napi.get_TokenUsuario(admin, pass);

                foreach (GridViewRow row in gv_ventasCargadas.Rows)
                {
                    CheckBox cbokeliminar = row.Cells[1].FindControl("cbk_VaciarUpon") as CheckBox;
                    if (cbokeliminar.Checked)
                    {
                        int codigoVaciar = int.Parse(row.Cells[2].Text);
                        DataSet Datos = nupon.GetDatoParaVaciar(codigoVaciar);
                        int codigo = codigoVaciar;
                        int NumeroVenta;
                        int.TryParse(Datos.Tables[0].Rows[0][1].ToString(), out NumeroVenta);
                        int NumeroPedido;
                        int.TryParse(Datos.Tables[0].Rows[0][2].ToString(), out NumeroPedido);
                        DateTime Fecha;
                        DateTime.TryParse(Datos.Tables[0].Rows[0][3].ToString(), out Fecha);
                        int CodigoCliente;
                        int.TryParse(Datos.Tables[0].Rows[0][4].ToString(), out CodigoCliente);
                        string Referencia = Datos.Tables[0].Rows[0][5].ToString();
                        string Glosa = Datos.Tables[0].Rows[0][6].ToString();
                        bool EmitirFactura = Convert.ToBoolean(Datos.Tables[0].Rows[0][7].ToString());
                        decimal ImporteProductos;
                        decimal.TryParse(Datos.Tables[0].Rows[0][8].ToString().Replace(".", ","), out ImporteProductos);
                        decimal ImporteDescuentos;
                        decimal.TryParse(Datos.Tables[0].Rows[0][9].ToString().Replace(".", ","), out ImporteDescuentos);
                        decimal ImporteTotal;
                        decimal.TryParse(Datos.Tables[0].Rows[0][10].ToString().Replace(".", ","), out ImporteTotal);
                        decimal Cobros_TotalEfectivo;
                        decimal.TryParse(Datos.Tables[0].Rows[0][11].ToString().Replace(".", ","), out Cobros_TotalEfectivo);
                        decimal Cobros_TotalDeposito;
                        decimal.TryParse(Datos.Tables[0].Rows[0][12].ToString().Replace(".", ","), out Cobros_TotalDeposito);
                        int Factura_TipoDocumentoIdentidad;
                        int.TryParse(Datos.Tables[0].Rows[0][13].ToString(), out Factura_TipoDocumentoIdentidad);
                        string Factura_NIT_CI = Datos.Tables[0].Rows[0][14].ToString();
                        string Factura_Complemento = Datos.Tables[0].Rows[0][15].ToString();
                        string Factura_RazonSocial = Datos.Tables[0].Rows[0][16].ToString();
                        string Factura_Telefono = Datos.Tables[0].Rows[0][17].ToString();
                        string Factura_Email = Datos.Tables[0].Rows[0][18].ToString();
                        int Factura_MetodoPago;
                        int.TryParse(Datos.Tables[0].Rows[0][19].ToString(), out Factura_MetodoPago);
                        int DetProd_NumeroItem;
                        int.TryParse(Datos.Tables[0].Rows[0][20].ToString(), out DetProd_NumeroItem);
                        string DetProd_CodigoProducto = Datos.Tables[0].Rows[0][21].ToString();
                        int DetProd_Cantidad;
                        int.TryParse(Datos.Tables[0].Rows[0][22].ToString(), out DetProd_Cantidad);
                        int DetProd_CodigoUnidadMedida;
                        int.TryParse(Datos.Tables[0].Rows[0][23].ToString(), out DetProd_CodigoUnidadMedida);
                        decimal DetProd_PrecioUnitario;
                        decimal.TryParse(Datos.Tables[0].Rows[0][24].ToString().Replace(".", ","), out DetProd_PrecioUnitario);
                        decimal DetProd_ImporteDescuento;
                        decimal.TryParse(Datos.Tables[0].Rows[0][25].ToString().Replace(".", ","), out DetProd_ImporteDescuento);
                        decimal DetProd_ImporteTotal;
                        decimal.TryParse(Datos.Tables[0].Rows[0][26].ToString().Replace(".", ","), out DetProd_ImporteTotal);
                        int DetProd_NumeroItemOrigen;
                        int.TryParse(Datos.Tables[0].Rows[0][27].ToString(), out DetProd_NumeroItemOrigen);
                        //string Usuario = Datos.Tables[0].Rows[0][34].ToString();
                        string Usuario = admin;
                        
                        bool banderaUpon = napi.insertarVentas(token, NumeroVenta, NumeroPedido, Fecha, CodigoCliente,
                            Referencia, Glosa, EmitirFactura, ImporteProductos, ImporteDescuentos, ImporteTotal, Cobros_TotalEfectivo,
                            Cobros_TotalDeposito, Factura_TipoDocumentoIdentidad, Factura_NIT_CI,  Factura_Complemento, Factura_RazonSocial,
                            Factura_Telefono, Factura_Email, Factura_MetodoPago, DetProd_NumeroItem, DetProd_CodigoProducto, DetProd_Cantidad,
                            DetProd_CodigoUnidadMedida, DetProd_PrecioUnitario, DetProd_ImporteDescuento, DetProd_ImporteTotal, DetProd_NumeroItemOrigen, Usuario);
                        if (banderaUpon == true)
                        {
                            bool bandera = nupon.updateVaciadoOk(codigoVaciar);
                        }
                    }
                }
                buscar("");
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Autenticacion') </script>");

        }

        private void eliminarSolicitud()
        {
            foreach (GridViewRow row in gv_ventasCargadas.Rows)
            {
                CheckBox cbokeliminar = row.Cells[0].FindControl("cbk_Eliminar") as CheckBox;
                if (cbokeliminar.Checked)
                {
                    int codigoEliminar = int.Parse(row.Cells[2].Text);
                    NUpon_Ventas nupon = new NUpon_Ventas();
                    bool bandera = nupon.eliminarVaciadoVentas(codigoEliminar);
                }
            }
            buscar("");
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            string dato = tx_glosaCompra.Text;
            buscar(dato);
        }

        protected void bt_cargarDocumentos_Click(object sender, EventArgs e)
        {
            cargarDocumentos();
            //  leerDocumentos();
            gv_ventasCargadas.SelectedIndex = -1;
        }

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarSolicitud();
        }

        protected void bt_vaciarUpon_Click(object sender, EventArgs e)
        {
            vaciarUpon_seleccionado();
        }
    }
}