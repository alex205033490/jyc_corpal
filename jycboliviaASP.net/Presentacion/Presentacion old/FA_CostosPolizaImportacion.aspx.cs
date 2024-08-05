using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_CostosPolizaImportacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(71) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {

            }
            //  habilitar();
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


        [WebMethod]
        [ScriptMethod]
        public static string[] getListaContenedor(string prefixText, int count)
        {
            string nombreEquipo = prefixText;

            NEquipo equipoN = new NEquipo();
            DataSet tuplas = equipoN.buscadorEquipo_GeneralTotal(nombreEquipo);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }
            return lista;
        }


        protected void bt_verificar_Click(object sender, EventArgs e)
        {
            verificarSiExisteelDUI();
        }

        private void verificarSiExisteelDUI()
        {

            //-------------------cambio de base de datos------------------
            string _nombreDBOriginal = Session["NombreBaseDatos"].ToString();
            NA_VariablesGlobales nvar = new NA_VariablesGlobales();
            string baseDeDatosTemporal = nvar.getBasedeDatosTemporal(NA_VariablesGlobales.CiudadParaIngresarDui);
            Session["NombreBaseDatos"] = baseDeDatosTemporal;
            //-------------------fin de cambio de datos-------------------
            string NroDUI = tx_nrodui.Text;
            NA_CostosPolizaImportacion npoliza = new NA_CostosPolizaImportacion();
            if (npoliza.existeNroDUI(NroDUI))
            {
                mostrarTodoslosDatosNroDUI(NroDUI);
                mostrarFacturasProveedor(NroDUI);
                //-------------------volver la base dedatos base de datos------------------                            
                Session["NombreBaseDatos"] = _nombreDBOriginal;
                //-------------------fin de volver base de datos-------------------
                mostrarEquiposDui(NroDUI);
                mostrarProrrateoCostos(NroDUI);                
            }
            else{
                //-------------------volver la base dedatos base de datos------------------                            
                Session["NombreBaseDatos"] = _nombreDBOriginal;
                //-------------------fin de volver base de datos-------------------
                Response.Write("<script type='text/javascript'> alert('ERROR: No Existe el NroDUI') </script>");
            }
        }

        private void mostrarTodoslosDatosNroDUI(string NroDUI)
        {
            string BaseDui = NA_VariablesGlobales.baseDedatosDui;
            NA_CostosPolizaImportacion npoliza = new NA_CostosPolizaImportacion();
            DataSet tuplas = npoliza.get_PolizasCostos(NroDUI, BaseDui);
            tx_nrodui.Text = tuplas.Tables[0].Rows[0][0].ToString();
            tx_fechafactura.Text = tuplas.Tables[0].Rows[0][3].ToString();
            tx_nitproveedor.Text = tuplas.Tables[0].Rows[0][4].ToString();
            tx_nombrerazonsocialproveedor.Text = tuplas.Tables[0].Rows[0][5].ToString();
            tx_impbaseparacreditofiscal.Text = tuplas.Tables[0].Rows[0][6].ToString();
            tx_creditofiscal.Text = tuplas.Tables[0].Rows[0][7].ToString();            
            tx_iva_cf_poliza.Text = tuplas.Tables[0].Rows[0][8].ToString();
            tx_planillaaduanera.Text = tuplas.Tables[0].Rows[0][9].ToString();
            tx_pagoplanillaaduanera.Text = tuplas.Tables[0].Rows[0][10].ToString();
            tx_iva_cf_planillaaduanera.Text = tuplas.Tables[0].Rows[0][11].ToString();
            tx_valornetoplanillaaduanera.Text = tuplas.Tables[0].Rows[0][12].ToString();
            tx_dif_enpago_pa.Text = tuplas.Tables[0].Rows[0][13].ToString();            
            tx_prorrateodecostos_transporte_internacional.Text = tuplas.Tables[0].Rows[0][14].ToString();
            tx_prorrateodecostos_transporte_nacional.Text = tuplas.Tables[0].Rows[0][15].ToString();
            tx_prorrateodecostos_nrofacttransptnaceinter.Text = tuplas.Tables[0].Rows[0][16].ToString();
            tx_prorrateodecostos_logisticaparatransporte.Text = tuplas.Tables[0].Rows[0][17].ToString();
            tx_prorrateodecostos_nrofactlogistica.Text = tuplas.Tables[0].Rows[0][18].ToString();
            tx_prorrateodecostos_mscltda.Text = tuplas.Tables[0].Rows[0][19].ToString();
            tx_prorrateodecostos_nrofactmsc.Text = tuplas.Tables[0].Rows[0][20].ToString();
            tx_prorrateodecostos_aspb.Text = tuplas.Tables[0].Rows[0][21].ToString();
            tx_prorrateodecostos_nrodepositooplanillaaspb.Text = tuplas.Tables[0].Rows[0][22].ToString(); 
            tx_mercaderiaentransito.Text = tuplas.Tables[0].Rows[0][23].ToString();
            tx_totalcostopoliza.Text = tuplas.Tables[0].Rows[0][24].ToString();
            tx_cantidad_equiposDui.Text = tuplas.Tables[0].Rows[0][25].ToString();
            tx_descripciondelproducto.Text = tuplas.Tables[0].Rows[0][26].ToString();            
       //     tx_transexpresadobolivianos_total.Text = tuplas.Tables[0].Rows[0][27].ToString();
            tx_observaciones.Text = tuplas.Tables[0].Rows[0][28].ToString();
            tx_nrofacturaAgencia.Text = tuplas.Tables[0].Rows[0][29].ToString();
            tx_importeFac.Text = tuplas.Tables[0].Rows[0][30].ToString();
            tx_importeAlmacenera.Text = tuplas.Tables[0].Rows[0][31].ToString();
            tx_nroFacturaAlmacenera.Text = tuplas.Tables[0].Rows[0][32].ToString();
        }

        public void mostrarProrrateoCostos(string NroDui) {
            NEquipo equipo1 = new NEquipo();
            DataSet tuplaRes = equipo1.buscar_ProrrateoCostosGeneralTransporte_JyCIA(false, "", "", "", NroDui, "");
            gv_prorrateoCostos.DataSource = tuplaRes;
            gv_prorrateoCostos.DataBind();

        }

        public void limpiar() {
            tx_nrodui.Text = "";
            tx_fechafactura.Text = "";
            tx_nitproveedor.Text = "";
            tx_nombrerazonsocialproveedor.Text = "";
            tx_impbaseparacreditofiscal.Text = "0";
            tx_creditofiscal.Text = "0";
            tx_iva_cf_poliza.Text = "0";
            tx_planillaaduanera.Text = "0";
            tx_pagoplanillaaduanera.Text = "0"; 
            tx_iva_cf_planillaaduanera.Text = "0";
            tx_valornetoplanillaaduanera.Text = "0";
            tx_dif_enpago_pa.Text = "0";
            tx_prorrateodecostos_transporte_internacional.Text = "0";
            tx_prorrateodecostos_transporte_nacional.Text = "0";
            tx_prorrateodecostos_nrofacttransptnaceinter.Text = "0"; 
            tx_prorrateodecostos_logisticaparatransporte.Text = "0";
            tx_prorrateodecostos_nrofactlogistica.Text = "0";
            tx_prorrateodecostos_mscltda.Text = "0";
            tx_prorrateodecostos_nrofactmsc.Text = "0";
            tx_prorrateodecostos_aspb.Text = "";
            tx_prorrateodecostos_nrodepositooplanillaaspb.Text = "0";
            tx_mercaderiaentransito.Text = "0";
            tx_totalcostopoliza.Text = "0";
            tx_cantidad_equiposDui.Text = "0";
            tx_descripciondelproducto.Text = "";
         //   tx_transexpresadobolivianos_total.Text = "0";
            tx_observaciones.Text = "";
            tx_nrofacturaAgencia.Text = "";
            tx_importeFac.Text = "0";

            gv_facturasProveedores.DataSource = null;
            gv_facturasProveedores.DataBind();
            gv_SegurosdeEquipos.DataSource = null;
            gv_SegurosdeEquipos.DataBind();
        }


        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaNroDUI(string prefixText, int count)
        {
            string NroDUI = prefixText;
            //-------------------cambio de base de datos------------------
            string _nombreDBOriginal = System.Web.HttpContext.Current.Session["NombreBaseDatos"].ToString();
            NA_VariablesGlobales nvar = new NA_VariablesGlobales();
            string baseDeDatosTemporal = nvar.getBasedeDatosTemporal(NA_VariablesGlobales.CiudadParaIngresarDui);
            System.Web.HttpContext.Current.Session["NombreBaseDatos"] = baseDeDatosTemporal;
            //-------------------fin de cambio de datos-------------------
            NA_CostosPolizaImportacion npoliza = new NA_CostosPolizaImportacion();
            DataSet tuplas = npoliza.get_ALLPolizasCostos(NroDUI);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            //-------------------volver la base dedatos base de datos------------------                            
            System.Web.HttpContext.Current.Session["NombreBaseDatos"] = _nombreDBOriginal;
            //-------------------fin de volver base de datos-------------------
            return lista;
        }

        protected void bt_actualizar_Click(object sender, EventArgs e)
        {
            ingresar_ActualizarDatos();
        }

        public string ConvertidorFecha(string fecha)
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
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'"+_fecha+"'";
            }

        }

        private void ingresar_ActualizarDatos()
        {
            bool bandera = false;
            string NroDUI = tx_nrodui.Text;

            string fechafactura = ConvertidorFecha(tx_fechafactura.Text);
            string nitproveedor = tx_nitproveedor.Text;
            string nombrerazonsocialproveedor = tx_nombrerazonsocialproveedor.Text;            

            float creditofiscal;
            float.TryParse(tx_creditofiscal.Text.Replace('.', ','), out creditofiscal);

            float impbaseparacreditofiscal;
            float.TryParse(tx_impbaseparacreditofiscal.Text.Replace('.', ','), out impbaseparacreditofiscal);

            float iva_cf_poliza;
            float.TryParse(tx_iva_cf_poliza.Text.Replace('.', ','), out iva_cf_poliza);

            float planillaaduanera;
            float.TryParse(tx_planillaaduanera.Text.Replace('.', ','), out planillaaduanera);

            float pagoplanillaaduanera;
            float.TryParse(tx_pagoplanillaaduanera.Text.Replace('.', ','), out pagoplanillaaduanera);

            float iva_cf_planillaaduanera;
            float.TryParse(tx_iva_cf_planillaaduanera.Text.Replace('.', ','), out iva_cf_planillaaduanera);

            float valornetoplanillaaduanera;
            float.TryParse(tx_valornetoplanillaaduanera.Text.Replace('.', ','), out valornetoplanillaaduanera);

            float dif_enpago_pa;
            float.TryParse(tx_dif_enpago_pa.Text.Replace('.', ','), out dif_enpago_pa);

         
            float prorrateodecostos_transporte_internacional;
            float.TryParse(tx_prorrateodecostos_transporte_internacional.Text.Replace('.', ','), out prorrateodecostos_transporte_internacional);

            float prorrateodecostos_transporte_nacional;
            float.TryParse(tx_prorrateodecostos_transporte_nacional.Text.Replace('.', ','), out prorrateodecostos_transporte_nacional);

            float prorrateodecostos_nrofacttransptnaceinter;
            float.TryParse(tx_prorrateodecostos_nrofacttransptnaceinter.Text.Replace('.', ','), out prorrateodecostos_nrofacttransptnaceinter);

            float prorrateodecostos_logisticaparatransporte;
            float.TryParse(tx_prorrateodecostos_logisticaparatransporte.Text.Replace('.', ','), out prorrateodecostos_logisticaparatransporte);

            float prorrateodecostos_nrofactlogistica;
            float.TryParse(tx_prorrateodecostos_nrofactlogistica.Text.Replace('.', ','), out prorrateodecostos_nrofactlogistica);

            float prorrateodecostos_mscltda;
            float.TryParse(tx_prorrateodecostos_mscltda.Text.Replace('.', ','), out prorrateodecostos_mscltda);

            float prorrateodecostos_nrofactmsc;
            float.TryParse(tx_prorrateodecostos_nrofactmsc.Text.Replace('.', ','), out prorrateodecostos_nrofactmsc);

            float prorrateodecostos_aspb;
            float.TryParse(tx_prorrateodecostos_aspb.Text.Replace('.', ','), out prorrateodecostos_aspb);

            string prorrateodecostos_nrodepositooplanillaaspb = tx_prorrateodecostos_nrodepositooplanillaaspb.Text;
            //float.TryParse(tx_prorrateodecostos_nrodepositooplanillaaspb.Text.Replace('.', ','), out prorrateodecostos_nrodepositooplanillaaspb);

            float mercaderiaentransito;
            float.TryParse(tx_mercaderiaentransito.Text.Replace('.', ','), out mercaderiaentransito);

            float totalcostopoliza;
            float.TryParse(tx_totalcostopoliza.Text.Replace('.', ','), out totalcostopoliza);

            float cantidad;
            float.TryParse(tx_cantidad_equiposDui.Text.Replace('.', ','), out cantidad);

            string descripciondelproducto = tx_descripciondelproducto.Text;
                      
         //   float transexpresadobolivianos_total;
         //   float.TryParse(tx_transexpresadobolivianos_total.Text.Replace('.', ','), out transexpresadobolivianos_total);
            float transexpresadobolivianos_total = 0;

            string observaciones = tx_observaciones.Text;

            string nroFacturaAgencia = tx_nrofacturaAgencia.Text;
            float importeFactura;
            float.TryParse(tx_importeFac.Text.Replace('.', ','), out importeFactura);
            string nrofactura_almacenera = tx_nroFacturaAlmacenera.Text;
            float importe_almacenera;
            float.TryParse(tx_importeAlmacenera.Text.Replace('.', ','), out importe_almacenera);


            //-------------------cambio de base de datos------------------
            string _nombreDBOriginal = Session["NombreBaseDatos"].ToString();
            NA_VariablesGlobales nvar = new NA_VariablesGlobales();
            string baseDeDatosTemporal = nvar.getBasedeDatosTemporal(NA_VariablesGlobales.CiudadParaIngresarDui);
            Session["NombreBaseDatos"] = baseDeDatosTemporal;
            //-------------------fin de cambio de datos-------------------

            NA_CostosPolizaImportacion npoliza = new NA_CostosPolizaImportacion();
            if (npoliza.existeNroDUI(NroDUI))
            {
                bandera = npoliza.updatecostospoliza(NroDUI, fechafactura, nitproveedor,
                                                        nombrerazonsocialproveedor, impbaseparacreditofiscal, creditofiscal,                                                        
                                                        iva_cf_poliza, planillaaduanera, pagoplanillaaduanera,
                                                        iva_cf_planillaaduanera, valornetoplanillaaduanera, dif_enpago_pa,
                                                        prorrateodecostos_transporte_internacional, prorrateodecostos_transporte_nacional,
                                                        prorrateodecostos_nrofacttransptnaceinter, prorrateodecostos_logisticaparatransporte,
                                                        prorrateodecostos_nrofactlogistica, prorrateodecostos_mscltda, prorrateodecostos_nrofactmsc,
                                                        prorrateodecostos_aspb, prorrateodecostos_nrodepositooplanillaaspb, mercaderiaentransito,
                                                        totalcostopoliza, cantidad, descripciondelproducto,
                                                        transexpresadobolivianos_total, observaciones, nroFacturaAgencia, importeFactura,  nrofactura_almacenera,  importe_almacenera);
            }
            else
            {
                bandera = npoliza.insertcostospoliza(NroDUI, fechafactura, nitproveedor,
                                                        nombrerazonsocialproveedor, impbaseparacreditofiscal, creditofiscal,                                                        
                                                        iva_cf_poliza, planillaaduanera, pagoplanillaaduanera,
                                                        iva_cf_planillaaduanera, valornetoplanillaaduanera, dif_enpago_pa,
                                                        prorrateodecostos_transporte_internacional, prorrateodecostos_transporte_nacional,
                                                        prorrateodecostos_nrofacttransptnaceinter, prorrateodecostos_logisticaparatransporte,
                                                        prorrateodecostos_nrofactlogistica, prorrateodecostos_mscltda, prorrateodecostos_nrofactmsc,
                                                        prorrateodecostos_aspb, prorrateodecostos_nrodepositooplanillaaspb, mercaderiaentransito,
                                                        totalcostopoliza, cantidad, descripciondelproducto,
                                                        transexpresadobolivianos_total, observaciones, nroFacturaAgencia, importeFactura, nrofactura_almacenera,  importe_almacenera);
            }

            if (bandera == true)
            {
                mostrarFacturasProveedor(NroDUI);
                mostrarEquiposDui(NroDUI);
                mostrarTodoslosDatosNroDUI(NroDUI);

                //-------------------volver la base dedatos base de datos------------------                            
                Session["NombreBaseDatos"] = _nombreDBOriginal;
                //-------------------fin de volver base de datos-------------------
                Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
            }
            else
            {
                //-------------------volver la base dedatos base de datos------------------                            
                Session["NombreBaseDatos"] = _nombreDBOriginal;
                //-------------------fin de volver base de datos-------------------
                Response.Write("<script type='text/javascript'> alert('Guardado: ERROR') </script>");
            }
        }

        protected void bt_limpiza_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void bt_facProv_agregarFac_Click(object sender, EventArgs e)
        {
            agregarFacturasProveedores();
            limpiarFacturaProveedores();
        }

        private void limpiarFacturaProveedores()
        {
            tx_facProv_FacComercial.Text = "0";
            tx_facProv_GiroalExterior.Text = "0";            
            tx_facProv_ProveedoresporPagar.Text = "0";
            tx_facProv_moneda.Text = "";
            tx_facProv_giroFacturaComercial.Text = "0";
            tx_facProv_comision.Text = "0";
            tx_facProv_itf.Text = "0";
            tx_facProv_difdeCambio.Text = "0";
            tx_tcgirofactuacomercialalexterior.Text = "0";
            tx_girofacturaComercial_giroalexteriorEuro.Text = "0";
            tx_girofacturacomercialExterior_Euro.Text = "0";
        }

        private void agregarFacturasProveedores()
        {
            string facturaComercial = tx_facProv_FacComercial.Text;
            float giroExterior;
            float.TryParse(tx_facProv_GiroalExterior.Text.Replace('.', ','), out giroExterior);
            string proveedoresporPagar = tx_facProv_ProveedoresporPagar.Text;
            string moneda = tx_facProv_moneda.Text;
            float giroFacturaComercial;
            float.TryParse(tx_facProv_giroFacturaComercial.Text.Replace('.', ','), out giroFacturaComercial);
            float comision;
            float.TryParse(tx_facProv_comision.Text.Replace('.', ','), out comision);
            float itf;
            float.TryParse(tx_facProv_itf.Text.Replace('.', ','), out itf);           

            float transexpresadobolivianos_tcdolares_girofacturacomercial;
            float.TryParse(tx_tcgirofactuacomercialalexterior.Text.Replace('.', ','), out transexpresadobolivianos_tcdolares_girofacturacomercial);

            float transexpresadobolivianos_girofacturacomercial_euro;
            float.TryParse(tx_girofacturaComercial_giroalexteriorEuro.Text.Replace('.', ','), out transexpresadobolivianos_girofacturacomercial_euro);

            float transexpresadobolivianos_tceuro_girofacturacomercial;
            float.TryParse(tx_girofacturacomercialExterior_Euro.Text.Replace('.', ','), out transexpresadobolivianos_tceuro_girofacturacomercial);

            bool bandera = false;
            string NroDUI = tx_nrodui.Text;

            float difdecambio;
            //float.TryParse(tx_facProv_difdeCambio.Text.Replace('.', ','), out difdecambio);

            difdecambio = (giroExterior - transexpresadobolivianos_girofacturacomercial_euro);

            //-------------------cambio de base de datos------------------
            string _nombreDBOriginal = Session["NombreBaseDatos"].ToString();
            NA_VariablesGlobales nvar = new NA_VariablesGlobales();
            string baseDeDatosTemporal = nvar.getBasedeDatosTemporal(NA_VariablesGlobales.CiudadParaIngresarDui);
            Session["NombreBaseDatos"] = baseDeDatosTemporal;
            //-------------------fin de cambio de datos-------------------

            NA_CostosPolizaImportacion npoliza = new NA_CostosPolizaImportacion();
            if (npoliza.existeNroDUI(NroDUI))
            {
                mostrarTodoslosDatosNroDUI(NroDUI);
                bandera = npoliza.ingresarFacturasProveedor(NroDUI, facturaComercial, giroExterior, proveedoresporPagar,
                                                  moneda, giroFacturaComercial, comision, itf, difdecambio,
                                                  transexpresadobolivianos_tcdolares_girofacturacomercial,
                                                 transexpresadobolivianos_girofacturacomercial_euro,
                                                 transexpresadobolivianos_tceuro_girofacturacomercial
                                                  );
                mostrarFacturasProveedor(NroDUI);
                mostrarEquiposDui(NroDUI);
                mostrarTodoslosDatosNroDUI(NroDUI);
                //-------------------volver la base dedatos base de datos------------------                            
                Session["NombreBaseDatos"] = _nombreDBOriginal;
                //-------------------fin de volver base de datos-------------------
            }
            else
            {
                //-------------------volver la base dedatos base de datos------------------                            
                Session["NombreBaseDatos"] = _nombreDBOriginal;
                //-------------------fin de volver base de datos-------------------
                Response.Write("<script type='text/javascript'> alert('Error: NO Se ha Guardado') </script>");
            }



        }

        private void mostrarFacturasProveedor(string NroDUI)
        {
            //-------------------cambio de base de datos------------------
            string _nombreDBOriginal = Session["NombreBaseDatos"].ToString();
            NA_VariablesGlobales nvar = new NA_VariablesGlobales();
            string baseDeDatosTemporal = nvar.getBasedeDatosTemporal(NA_VariablesGlobales.CiudadParaIngresarDui);
            Session["NombreBaseDatos"] = baseDeDatosTemporal;
            //-------------------fin de cambio de datos-------------------
            NA_CostosPolizaImportacion npoliza = new NA_CostosPolizaImportacion();
            DataSet tuplas = npoliza.getFacturasDui(NroDUI);
            gv_facturasProveedores.DataSource = tuplas;
            gv_facturasProveedores.DataBind();
            //-------------------volver la base dedatos base de datos------------------                            
            Session["NombreBaseDatos"] = _nombreDBOriginal;
            //-------------------fin de volver base de datos-------------------
        }


        private void eliminarFacturasProveedores(GridViewDeleteEventArgs e)
        {
            //-------------------cambio de base de datos------------------
            string _nombreDBOriginal = Session["NombreBaseDatos"].ToString();
            NA_VariablesGlobales nvar = new NA_VariablesGlobales();
            string baseDeDatosTemporal = nvar.getBasedeDatosTemporal(NA_VariablesGlobales.CiudadParaIngresarDui);
            Session["NombreBaseDatos"] = baseDeDatosTemporal;
            //-------------------fin de cambio de datos-------------------
            if (e.RowIndex > -1)
            {
                int codigoFactura = Convert.ToInt32(gv_facturasProveedores.Rows[e.RowIndex].Cells[1].Text);
                string NroDui = tx_nrodui.Text;
                NA_CostosPolizaImportacion npoliza = new NA_CostosPolizaImportacion();
                bool bandera = npoliza.eliminarFacturasProveedores(codigoFactura, NroDui);
                Response.Write("<script type='text/javascript'> alert('Eliminado OK') </script>");
                mostrarEquiposDui(NroDui);
                mostrarFacturasProveedor(NroDui);
                mostrarTodoslosDatosNroDUI(NroDui);
            }
            else
            {
                Response.Write("<script type='text/javascript'> alert('Error: NO Se ha eliminado') </script>");
                //-------------------volver la base dedatos base de datos------------------                            
                Session["NombreBaseDatos"] = _nombreDBOriginal;
                //-------------------fin de volver base de datos-------------------
            }
        }

        protected void gv_facturasProveedores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            eliminarFacturasProveedores(e);
        }

        protected void bt_SegEq_AgregarEquipo_Click(object sender, EventArgs e)
        {
            agregarEquiposAlDui();
        }

        private void agregarEquiposAlDui()
        {
            //-------------------cambio de base de datos------------------
            string _nombreDBOriginal = Session["NombreBaseDatos"].ToString();
            NA_VariablesGlobales nvar = new NA_VariablesGlobales();
            string baseDeDatosTemporal = nvar.getBasedeDatosTemporal(NA_VariablesGlobales.CiudadParaIngresarDui);
            Session["NombreBaseDatos"] = baseDeDatosTemporal;
            //-------------------fin de cambio de datos-------------------

            string codnrodui = tx_nrodui.Text;
            string exbo = tx_SegEq_Exbo.Text;
            NA_Equipo neq = new NA_Equipo();
            int codequipo = neq.getCodigoEquipo(exbo);
            string nrofactura = tx_SegEq_NroFactura.Text;
            float seguro;
            float.TryParse(tx_SegEq_Seguro.Text.Replace('.', ','), out seguro);

            string fechaFacturaSeguro = ConvertidorFecha(tx_fechaFacturaSeguro.Text);
            string nitSeguro = tx_nitSeguro.Text;

            agrearEquiposADui(codnrodui, exbo, nrofactura, seguro, fechaFacturaSeguro, nitSeguro);
            mostrarEquiposDui(codnrodui);
            mostrarFacturasProveedor(codnrodui);
            mostrarTodoslosDatosNroDUI(codnrodui);

            //-------------------volver la base dedatos base de datos------------------                            
            Session["NombreBaseDatos"] = _nombreDBOriginal;
            //-------------------fin de volver base de datos-------------------
        }

        private void mostrarEquiposDui(string codnrodui)
        {
            NA_CostosPolizaImportacion npoliza = new NA_CostosPolizaImportacion();
            DataSet tuplas = npoliza.buscar_EquiposDUIGeneral(codnrodui);
            tx_cantidad_equiposDui.Text = tuplas.Tables[0].Rows.Count.ToString();
            gv_SegurosdeEquipos.DataSource = tuplas;            
            gv_SegurosdeEquipos.DataBind();

            
        }

        private void agrearEquiposADui(string codnrodui, string exbo, string nrofactura, float seguro, string fechaFacturaSeguro, string NitSeguro)
        {
            NA_CostosPolizaImportacion ndui = new NA_CostosPolizaImportacion();
            if (ndui.existeNroDUI(codnrodui))
            {
                bool bandera = ndui.agrearEquiposADuiGeneral(codnrodui, exbo, nrofactura, seguro, fechaFacturaSeguro, NitSeguro);
                limpiarEquiposAsignados();

                mostrarEquiposDui(codnrodui);
                mostrarFacturasProveedor(codnrodui);
                mostrarTodoslosDatosNroDUI(codnrodui);

            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: NO Se ha Guardado') </script>");
        }

        private void limpiarEquiposAsignados()
        {
            tx_SegEq_NroFactura.Text = "";
            tx_SegEq_Seguro.Text = "";
            tx_fechaFacturaSeguro.Text = "";
            tx_nitSeguro.Text = "";
        }

        protected void gv_SegurosdeEquipos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string codnrodui = tx_nrodui.Text;
                string exbo = gv_SegurosdeEquipos.Rows[e.RowIndex].Cells[2].Text;
                //NA_Equipo neq = new NA_Equipo();
                //int codequipo = neq.getCodigoEquipo(exbo);

                NA_CostosPolizaImportacion npoliza = new NA_CostosPolizaImportacion();
                bool bandera = npoliza.eliminarEquiposADuiGeneral(codnrodui, exbo); 
                Response.Write("<script type='text/javascript'> alert('Eliminado OK') </script>");                
                mostrarEquiposDui(codnrodui);
                mostrarFacturasProveedor(codnrodui);
                mostrarTodoslosDatosNroDUI(codnrodui);
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: NO Se ha eliminado') </script>");
        }

        protected void gv_prorrateoCostos_SelectedIndexChanged(object sender, EventArgs e)
        {
           // seleccionarDatosdeProrrateoCostos();
        }

        private void seleccionarDatosdeProrrateoCostos()
        {
            if(gv_prorrateoCostos.SelectedIndex > -1){
            int codigoEquipo = Convert.ToInt32(gv_prorrateoCostos.SelectedRow.Cells[1].Text);
            string NroDui = gv_prorrateoCostos.SelectedRow.Cells[9].Text;

            NEquipo equipo1 = new NEquipo();
            DataSet tuplaRes = equipo1.buscar_ProrrateoCostosGeneral_Polizas(NroDui, codigoEquipo);

            tx_prorrateodecostos_transporte_internacional.Text = tuplaRes.Tables[0].Rows[0][11].ToString();
            tx_prorrateodecostos_transporte_nacional.Text = tuplaRes.Tables[0].Rows[0][12].ToString();
            tx_prorrateodecostos_nrofacttransptnaceinter.Text = tuplaRes.Tables[0].Rows[0][13].ToString();
            tx_prorrateodecostos_logisticaparatransporte.Text = tuplaRes.Tables[0].Rows[0][14].ToString();
            tx_prorrateodecostos_nrofactlogistica.Text = tuplaRes.Tables[0].Rows[0][15].ToString();
            tx_prorrateodecostos_mscltda.Text = tuplaRes.Tables[0].Rows[0][16].ToString();
            tx_prorrateodecostos_nrofactmsc.Text = tuplaRes.Tables[0].Rows[0][17].ToString();
            tx_prorrateodecostos_aspb.Text = tuplaRes.Tables[0].Rows[0][18].ToString();
            tx_prorrateodecostos_nrodepositooplanillaaspb.Text = tuplaRes.Tables[0].Rows[0][19].ToString();

            tx_mercaderiaentransito.Text = tuplaRes.Tables[0].Rows[0][20].ToString();
            tx_totalcostopoliza.Text = tuplaRes.Tables[0].Rows[0][21].ToString();
            }
            
        }

        /*
        private void modificarDatosdeProrrateoCostos()
        {
            int codigoEquipo = Convert.ToInt32(gv_prorrateoCostos.SelectedRow.Cells[1].Text);
            string Ciudad = gv_prorrateoCostos.SelectedRow.Cells[7].Text;
            string codnrodui = gv_prorrateoCostos.SelectedRow.Cells[9].Text;
            NEquipo equipo1 = new NEquipo();
            DataSet tuplaRes = equipo1.buscar_ProrrateoCostosGeneral_Polizas(codnrodui, codigoEquipo);

            string codnrocontenedor = tuplaRes.Tables[0].Rows[0][9].ToString();
            string semana_expedicion = tuplaRes.Tables[0].Rows[0][10].ToString();

            float pc_importefactura_tramointernacional;
            float.TryParse(tx_prorrateodecostos_transporte_internacional.Text.Replace('.', ','), out pc_importefactura_tramointernacional);

            float pc_importefactura_tramonacional;
            float.TryParse(tx_prorrateodecostos_transporte_nacional.Text.Replace('.', ','), out pc_importefactura_tramonacional);

            string pc_nrofacturatransporte_nacionalinternacional = tx_prorrateodecostos_nrofacttransptnaceinter.Text;

            float pc_importefacturalogisticatransporte;
            float.TryParse(tx_prorrateodecostos_logisticaparatransporte.Text.Replace('.', ','), out pc_importefacturalogisticatransporte);

            string pc_nrofacturalogisticatransporte = tx_prorrateodecostos_nrofactlogistica.Text;

            float pc_importefactura_mscltda;
            float.TryParse(tx_prorrateodecostos_mscltda.Text.Replace('.', ','), out pc_importefactura_mscltda);

            string pc_nrofacturamsc = tx_prorrateodecostos_nrofactmsc.Text;

            float pc_importedeplantilla_aspb;
            float.TryParse(tx_prorrateodecostos_aspb.Text.Replace('.', ','), out pc_importedeplantilla_aspb);

            string pc_nrodepositoplantilla_aspb = tx_prorrateodecostos_nrodepositooplanillaaspb.Text;

            float pc_importemercaderia_transito;
            float.TryParse(tx_mercaderiaentransito.Text.Replace('.', ','), out pc_importemercaderia_transito);

            float pc_totalcosto_poliza;
            float.TryParse(tx_totalcostopoliza.Text.Replace('.', ','), out pc_totalcosto_poliza);
                        

            NEquipo equipo = new NEquipo();
            bool actualizado = equipo.actualizar_ProrrateoCostos_General(codigoEquipo, Ciudad,
                                codnrodui, codnrocontenedor, semana_expedicion, pc_importefactura_tramointernacional,
                                pc_importefactura_tramonacional, pc_nrofacturatransporte_nacionalinternacional, pc_importefacturalogisticatransporte,
                                pc_nrofacturalogisticatransporte, pc_importefactura_mscltda, pc_nrofacturamsc, pc_importedeplantilla_aspb,
                                pc_nrodepositoplantilla_aspb, pc_importemercaderia_transito, pc_totalcosto_poliza);
            ///---------------modificar Estado pero ay k cambiar la base de datos-----------------------------------------------------                                                                       

            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            ///--------------------------------------------
            NA_Historial nhistorial = new NA_Historial();
            // int codUser = Convert.ToInt32(Session["coduser"].ToString());
            nhistorial.insertar(codUser, "Ha Datos de Prorrateo de Costo del equipo " + codigoEquipo + " del Exbo " + gv_prorrateoCostos.SelectedRow.Cells[3].Text);
            ///--------------------------------------------
            ///----------------------------------fin de modificar estado-----------------------------------------------------

            mostrarProrrateoCostos(codnrodui);
        }
        */ 
        protected void bt_modificarProrrateoCosto_Click(object sender, EventArgs e)
        {
           // modificarDatosdeProrrateoCostos();
        }

    }
}