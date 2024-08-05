using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_CostosPolizaImportacion
    {
        DA_CostosPolizaImportacion dcosto = new DA_CostosPolizaImportacion();

        public NA_CostosPolizaImportacion() { }

        public bool insertcostospoliza(string nrodui, string fechafactura, string nitproveedor, string nombrerazonsocialproveedor, float impbaseparacreditofiscal,
                                       float creditofiscal, float iva_cf_poliza, float planillaaduanera,
                                       float pagoplanillaaduanera, float iva_cf_planillaaduanera, float valornetoplanillaaduanera,
                                       float dif_enpago_pa, float prorrateodecostos_transporte_internacional,
                                       float prorrateodecostos_transporte_nacional, float prorrateodecostos_nrofacttransptnaceinter,
                                       float prorrateodecostos_logisticaparatransporte, float prorrateodecostos_nrofactlogistica,
                                       float prorrateodecostos_mscltda, float prorrateodecostos_nrofactmsc, float prorrateodecostos_aspb,
                                       string prorrateodecostos_nrodepositooplanillaaspb, float mercaderiaentransito, float totalcostopoliza,
                                       float cantidad, string descripciondelproducto, 
                                       float transexpresadobolivianos_total, string observaciones,
                                        string nroFacturaAgencia, float importeFactura, string nrofactura_almacenera, float importe_almacenera)
        {
            return dcosto.insertcostospoliza(nrodui, fechafactura, nitproveedor, nombrerazonsocialproveedor, impbaseparacreditofiscal,
                                        creditofiscal,   iva_cf_poliza,  planillaaduanera,
                                        pagoplanillaaduanera,  iva_cf_planillaaduanera,  valornetoplanillaaduanera,
                                        dif_enpago_pa,  prorrateodecostos_transporte_internacional,
                                        prorrateodecostos_transporte_nacional,  prorrateodecostos_nrofacttransptnaceinter,
                                        prorrateodecostos_logisticaparatransporte,  prorrateodecostos_nrofactlogistica,
                                        prorrateodecostos_mscltda,  prorrateodecostos_nrofactmsc,  prorrateodecostos_aspb,
                                        prorrateodecostos_nrodepositooplanillaaspb,  mercaderiaentransito,  totalcostopoliza,
                                        cantidad,  descripciondelproducto, 
                                        transexpresadobolivianos_total,  observaciones, nroFacturaAgencia, importeFactura, nrofactura_almacenera, importe_almacenera);
        }



        public bool updatecostospoliza(string nrodui, string fechafactura, string nitproveedor, string nombrerazonsocialproveedor, float impbaseparacreditofiscal,
                                      float creditofiscal, float iva_cf_poliza, float planillaaduanera,
                                      float pagoplanillaaduanera, float iva_cf_planillaaduanera, float valornetoplanillaaduanera,
                                      float dif_enpago_pa, float prorrateodecostos_transporte_internacional,
                                      float prorrateodecostos_transporte_nacional, float prorrateodecostos_nrofacttransptnaceinter,
                                      float prorrateodecostos_logisticaparatransporte, float prorrateodecostos_nrofactlogistica,
                                      float prorrateodecostos_mscltda, float prorrateodecostos_nrofactmsc, float prorrateodecostos_aspb,
                                      string prorrateodecostos_nrodepositooplanillaaspb, float mercaderiaentransito, float totalcostopoliza,
                                      float cantidad, string descripciondelproducto, 
                                      float transexpresadobolivianos_total, string observaciones,
                                      string nroFacturaAgencia, float importeFactura, string nrofactura_almacenera, float importe_almacenera)
        {
            return dcosto.updatecostospoliza(nrodui, fechafactura, nitproveedor, nombrerazonsocialproveedor, impbaseparacreditofiscal,
                                       creditofiscal,  iva_cf_poliza,  planillaaduanera,
                                       pagoplanillaaduanera,  iva_cf_planillaaduanera,  valornetoplanillaaduanera,
                                       dif_enpago_pa,  prorrateodecostos_transporte_internacional,
                                       prorrateodecostos_transporte_nacional,  prorrateodecostos_nrofacttransptnaceinter,
                                       prorrateodecostos_logisticaparatransporte,  prorrateodecostos_nrofactlogistica,
                                       prorrateodecostos_mscltda,  prorrateodecostos_nrofactmsc,  prorrateodecostos_aspb,
                                       prorrateodecostos_nrodepositooplanillaaspb,  mercaderiaentransito,  totalcostopoliza,
                                       cantidad,  descripciondelproducto,  
                                       transexpresadobolivianos_total,  observaciones,
                                       nroFacturaAgencia, importeFactura,  nrofactura_almacenera,  importe_almacenera);
        }


        public DataSet get_PolizasCostos(string NroDUI, string basedatos) {
            return dcosto.get_PolizasCostos(NroDUI, basedatos);
        }

        public DataSet get_ALLPolizasCostos(string NroDUI)
        {
            return dcosto.get_ALLPolizasCostos(NroDUI);
        }

        public DataSet get_ALLPolizasCostos_Mejorado(string NroDUI, string nombrerazonsocialproveedor)
        {
            return dcosto.get_ALLPolizasCostos_Mejorado(NroDUI, nombrerazonsocialproveedor);
        }

        public DataSet get_detalleFacturaProovedoresComercial_DUI(string NroDUI)
        {
            return dcosto.get_detalleFacturaProovedoresComercial_DUI(NroDUI);
        }

        internal bool existeNroDUI(string NroDUI)
        {
            string baseDeDatos = NA_VariablesGlobales.baseDedatosDui;
            DataSet tuplas = get_PolizasCostos(NroDUI, baseDeDatos);
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        internal bool ingresarFacturasProveedor(string codnrodui, string facturacomercial,
                                                float giroalexterior,
                                                string proveedoresporpagar,
                                                string transexpbolivianos_moneda,
                                                float transexpresadobolivianos_girofacturacomercial,
                                                float transexpresadobolivianos_comision,
                                                float transexpresadobolivianos_itf,
                                                float transexpresadobolivianos_difdecambio,
                                                float transexpresadobolivianos_tcdolares_girofacturacomercial,
                                                float transexpresadobolivianos_girofacturacomercial_euro,
                                                float transexpresadobolivianos_tceuro_girofacturacomercial
                                                )
        {
            bool bandera =  dcosto.ingresarFacturasProveedor( codnrodui, facturacomercial ,
                                                 giroalexterior ,
                                                 proveedoresporpagar ,
                                                 transexpbolivianos_moneda ,
                                                 transexpresadobolivianos_girofacturacomercial ,
                                                 transexpresadobolivianos_comision ,
                                                 transexpresadobolivianos_itf ,
                                                 transexpresadobolivianos_difdecambio,
                                                 transexpresadobolivianos_tcdolares_girofacturacomercial,
                                                 transexpresadobolivianos_girofacturacomercial_euro,
                                                 transexpresadobolivianos_tceuro_girofacturacomercial
                                                 );
            actualizarEquiposSeguro_FacturaProveedores(codnrodui);            
            return bandera;
        }

        internal DataSet getFacturasDui(string NroDUI)
        {
            return dcosto.getFacturasDui(NroDUI);
        }

        internal bool eliminarFacturasProveedores(int codigoFactura, string NroDui)
        {
           bool bandera = dcosto.eliminarFacturasProveedores(codigoFactura);
           actualizarEquiposSeguro_FacturaProveedores(NroDui);
           return bandera;
        }

        internal bool agrearEquiposADui(string codnrodui, int codequipo, string exbo, string nrofactura, float seguro)
        {
            bool bandera = dcosto.agrearEquiposADui( codnrodui, codequipo, exbo,nrofactura, seguro);
            actualizarEquiposSeguro_FacturaProveedores(codnrodui);
            return bandera;
        }

        internal bool agrearEquiposADuiGeneral(string codnrodui, string exbo, string nrofactura, float seguro, string fechaFacturaSeguro, string nitSeguro)
        {
            NA_VariablesGlobales vg = new NA_VariablesGlobales();
            NEquipo eq = new NEquipo();
            DataSet tuplas = eq.buscadorEquipo_GeneralTotal(exbo);
            for (int i = 0; i < tuplas.Tables[0].Rows.Count; i++ )
            {
                string ciudad = tuplas.Tables[0].Rows[i][6].ToString();
                string baseDatos = vg.getBasedeDatosTemporal(ciudad);
                dcosto.agrearEquiposADuiGeneral( codnrodui,  exbo,  nrofactura,  seguro,  fechaFacturaSeguro,  nitSeguro,baseDatos);
            }
            return true;
        }

        internal bool eliminarEquiposADuiGeneral(string codnrodui, string exbo)
        {
            NA_VariablesGlobales vg = new NA_VariablesGlobales();
            NEquipo eq = new NEquipo();
            DataSet tuplas = eq.buscadorEquipo_GeneralTotal(exbo);
            for (int i = 0; i < tuplas.Tables[0].Rows.Count; i++)
            {
                string ciudad = tuplas.Tables[0].Rows[i][6].ToString();
                string baseDatos = vg.getBasedeDatosTemporal(ciudad);
                dcosto.eliminarEquiposADuiGeneral(codnrodui, exbo, baseDatos);
            }
            return true;
        }


        private bool actualizarEquiposSeguro_FacturaProveedores(string codnrodui)
        {
           return  dcosto.actualizarEquiposSeguro_FacturaProveedores(codnrodui);
        }

        internal bool eliminarSeguroEquipo(string codnroDui, int codEquipo)
        {
            bool bandera = dcosto.eliminarSeguroEquipo(codnroDui, codEquipo);
            actualizarEquiposSeguro_FacturaProveedores(codnroDui);
            return bandera;
        }

        internal DataSet mostrarEquiposDui(string codnrodui)
        {
            return dcosto.mostrarEquiposDui(codnrodui);
        }

        internal DataSet buscar_EquiposDUIGeneral(string nroDui)
        {
            return dcosto.buscar_EquiposDUIGeneral(nroDui);
        }



        internal DataSet get_detalleSeguroEquipo(string codnrodui)
        {
            return dcosto.get_detalleSeguroEquipo(codnrodui);
        }

        internal bool insertarDui(string NroDui)
        {
            string baseDatosDui = NA_VariablesGlobales.baseDedatosDui;
           return dcosto.insertarDui(NroDui,baseDatosDui);
        }
    }
}