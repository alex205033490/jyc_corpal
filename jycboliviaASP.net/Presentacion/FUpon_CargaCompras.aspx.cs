using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using jycboliviaASP.net.Negocio;
using System.IO;
using System.Data;
using System.Net;
using jycboliviaASP.net.Datos;
using Clases.ApiRest;
using jycboliviaASP.net.NegocioApi;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.Xml;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CargarDocumentos : System.Web.UI.Page
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
            NUpon_compras NUpon = new NUpon_compras();
            DataSet resultMostrar = NUpon.mostrarComprasRealizadas(glosa);
            gv_comprasCargadas.DataSource = resultMostrar;
            gv_comprasCargadas.DataBind();
        }
   
        private void GuardarArchivo(HttpPostedFile file)
        {
            // Se carga la ruta física de la carpeta temp del sitio            
            string ruta = ConfigurationManager.AppSettings["ruta_ArchivoCompra"] ;                        
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
                        NUpon_compras Nupon = new NUpon_compras();
                        string ruta = ConfigurationManager.AppSettings["ruta_ArchivoCompra"] + FileUpload1.PostedFile.FileName;
                        bool bandera = Nupon.insertarComprasRealizadas(ruta);
                        if (bandera) {
                            NA_Historial nhistorial = new NA_Historial();
                            int codUser = Convert.ToInt32(Session["coduser"].ToString());
                            nhistorial.insertar(codUser, "Se ha Cargado Compras Realizadas archivo" + FileUpload1.PostedFile.FileName);
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
            NUpon_compras NUpon = new NUpon_compras();
            DataSet resultMostrar = NUpon.mostrarComprasRealizadas(glosa);
            gv_comprasCargadas.DataSource = resultMostrar;
            gv_comprasCargadas.DataBind();            
        }
        protected void bt_cargarDocumentos_Click(object sender, EventArgs e)
        {
            cargarDocumentos();            
          //  leerDocumentos();
            gv_comprasCargadas.SelectedIndex = -1;
        }


        private void eliminarSolicitud()
        {
            foreach (GridViewRow row in gv_comprasCargadas.Rows)
            {
                CheckBox cbokeliminar = row.Cells[0].FindControl("cbk_Eliminar") as CheckBox;
                if (cbokeliminar.Checked)
                {
                    int codigoEliminar = int.Parse(row.Cells[2].Text);
                    NUpon_compras nupon = new NUpon_compras();
                    bool bandera = nupon.eliminarVaciadoCompras(codigoEliminar);
                }
            }
            buscar("");
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            buscar(tx_glosaCompra.Text);            
        }

        protected void bt_vaciarUpon_Click(object sender, EventArgs e)
        {
            vaciarUpon_seleccionado();
        }

        private void vaciarUpon_seleccionado()
        {
            NUpon_compras nupon = new NUpon_compras();
            NA_endpoints napi = new NA_endpoints();
            string admin = tx_usuario.Text;
            string pass = tx_passUsuario.Text;
            bool banderaAut = napi.get_AutenticarUsuario(admin, pass);
            if (banderaAut) {
                string token = napi.get_TokenUsuario(admin, pass);

                foreach (GridViewRow row in gv_comprasCargadas.Rows)
                {
                    CheckBox cbokeliminar = row.Cells[1].FindControl("cbk_VaciarUpon") as CheckBox;
                    if (cbokeliminar.Checked)
                    {
                        int codigoVaciar = int.Parse(row.Cells[2].Text);
                        DataSet Datos = nupon.GetDatoParaVaciar(codigoVaciar);
                        int codigo = codigoVaciar;
                        int NumeroCompra;
                        int.TryParse(Datos.Tables[0].Rows[0][1].ToString(), out NumeroCompra);
                        DateTime Fecha;
                        DateTime.TryParse(Datos.Tables[0].Rows[0][2].ToString(), out Fecha);
                        string Referencia = Datos.Tables[0].Rows[0][3].ToString();
                        decimal ImporteProductos;
                        decimal.TryParse(Datos.Tables[0].Rows[0][4].ToString().Replace(".", ","), out ImporteProductos);
                        decimal ImporteDescuento;
                        decimal.TryParse(Datos.Tables[0].Rows[0][5].ToString().Replace(".", ","), out ImporteDescuento);
                        decimal ImporteTotal;
                        decimal.TryParse(Datos.Tables[0].Rows[0][6].ToString().Replace(".", ","), out ImporteTotal);
                        int CodigoMoneda;
                        int.TryParse(Datos.Tables[0].Rows[0][7].ToString(), out CodigoMoneda);
                        int CodigoProveedor;
                        int.TryParse(Datos.Tables[0].Rows[0][8].ToString(), out CodigoProveedor);
                        int CodigoDistribucionGastos;
                        int.TryParse(Datos.Tables[0].Rows[0][9].ToString(), out CodigoDistribucionGastos);

                        decimal pagos_TotalEfectivo;
                        decimal.TryParse(Datos.Tables[0].Rows[0][10].ToString().Replace(".", ","), out pagos_TotalEfectivo);
                        decimal pagos_TotalCredito;
                        decimal.TryParse(Datos.Tables[0].Rows[0][11].ToString().Replace(".", ","), out pagos_TotalCredito);
                        decimal pagos_TotalCheques;
                        decimal.TryParse(Datos.Tables[0].Rows[0][12].ToString().Replace(".", ","), out pagos_TotalCheques);
                        decimal pagos_TotalDeposito;
                        decimal.TryParse(Datos.Tables[0].Rows[0][13].ToString().Replace(".", ","), out pagos_TotalDeposito);
                        bool FacturaPosterior = Convert.ToBoolean(Datos.Tables[0].Rows[0][14].ToString());
                        string factura_NIT_CI = Datos.Tables[0].Rows[0][15].ToString();
                        string factura_RazonSocial = Datos.Tables[0].Rows[0][16].ToString();
                        string factura_NumeroFactura = Datos.Tables[0].Rows[0][17].ToString();
                        string factura_CodigoAutorizacion = Datos.Tables[0].Rows[0][18].ToString();
                        string factura_CodigoControl = Datos.Tables[0].Rows[0][19].ToString();
                        decimal factura_ImporteTotal;
                        decimal.TryParse(Datos.Tables[0].Rows[0][20].ToString().Replace(".", ","), out factura_ImporteTotal);
                        decimal factura_ImporteDescuento;
                        decimal.TryParse(Datos.Tables[0].Rows[0][21].ToString().Replace(".", ","), out factura_ImporteDescuento);
                        decimal factura_ImporteGift;
                        decimal.TryParse(Datos.Tables[0].Rows[0][22].ToString().Replace(".", ","), out factura_ImporteGift);
                        decimal factura_ImporteNeto;
                        decimal.TryParse(Datos.Tables[0].Rows[0][23].ToString().Replace(".", ","), out factura_ImporteNeto);
                        bool factura_AplicaCredictoFiscal = Convert.ToBoolean(Datos.Tables[0].Rows[0][24].ToString());
                        string Glosa = Datos.Tables[0].Rows[0][25].ToString();
                        int dprod_NumeroItem;
                        int.TryParse(Datos.Tables[0].Rows[0][26].ToString(), out dprod_NumeroItem);
                        string dprod_CodigoProducto = Datos.Tables[0].Rows[0][27].ToString();

                        decimal dprod_Cantidad_aux;
                        decimal.TryParse(Datos.Tables[0].Rows[0][28].ToString().Replace(".", ","), out dprod_Cantidad_aux);
                        int dprod_Cantidad = decimal.ToInt32(dprod_Cantidad_aux);
                        decimal dprod_CodigoUnidadMedida_aux;
                        decimal.TryParse(Datos.Tables[0].Rows[0][29].ToString().Replace(".", ","), out dprod_CodigoUnidadMedida_aux);
                        int dprod_CodigoUnidadMedida = decimal.ToInt32(dprod_CodigoUnidadMedida_aux);
                        decimal dprod_PrecioUnitario;
                        decimal.TryParse(Datos.Tables[0].Rows[0][30].ToString().Replace(".", ","), out dprod_PrecioUnitario);
                        decimal dprod_ImporteDescuento;
                        decimal.TryParse(Datos.Tables[0].Rows[0][31].ToString().Replace(".", ","), out dprod_ImporteDescuento);
                        decimal dprod_PorcentajeGasto;
                        decimal.TryParse(Datos.Tables[0].Rows[0][32].ToString().Replace(".", ","), out dprod_PorcentajeGasto);
                        decimal dprod_ImporteTotal;
                        decimal.TryParse(Datos.Tables[0].Rows[0][33].ToString().Replace(".", ","), out dprod_ImporteTotal);
                        //string Usuario = Datos.Tables[0].Rows[0][34].ToString();
                        string Usuario = admin;
                        bool estado = Convert.ToBoolean(Datos.Tables[0].Rows[0][35].ToString());
                        bool vaciadoupon = Convert.ToBoolean(Datos.Tables[0].Rows[0][36].ToString());

                        bool banderaUpon = napi.insertarCompras(token, NumeroCompra, Fecha, Referencia, ImporteProductos, ImporteDescuento, ImporteTotal, CodigoMoneda, CodigoProveedor, CodigoDistribucionGastos, pagos_TotalEfectivo, pagos_TotalCredito, pagos_TotalCheques, pagos_TotalDeposito, FacturaPosterior, factura_NIT_CI, factura_RazonSocial, factura_NumeroFactura, factura_CodigoAutorizacion, factura_CodigoControl, factura_ImporteTotal, factura_ImporteDescuento, factura_ImporteGift, factura_ImporteNeto, factura_AplicaCredictoFiscal, Glosa, dprod_NumeroItem, dprod_CodigoProducto, dprod_Cantidad, dprod_CodigoUnidadMedida, dprod_PrecioUnitario, dprod_ImporteDescuento, dprod_PorcentajeGasto, dprod_ImporteTotal, Usuario, estado, vaciadoupon);
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

        protected void bt_eliminar_Click(object sender, EventArgs e)
        {
            eliminarSolicitud();
        }
    }
}