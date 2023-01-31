using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_CostosPolizaImportacion
    {
        private conexionMySql cnx = new conexionMySql();

        public DA_CostosPolizaImportacion() { }

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


            //-----------calculos----------                                           
            float pocentaje13 = float.Parse("0,13");
            impbaseparacreditofiscal = creditofiscal / pocentaje13;
            iva_cf_planillaaduanera = importeFactura * pocentaje13;

            iva_cf_poliza = (creditofiscal + iva_cf_planillaaduanera);
            valornetoplanillaaduanera = (planillaaduanera - iva_cf_planillaaduanera);
            dif_enpago_pa = (planillaaduanera - pagoplanillaaduanera);


            float SumaTotalGiroExterior = SumaTotalGiroAlExterior(nrodui);
            float SumaTotalSeguroDeEquipos = SumaTotalSeguroEquipo(nrodui);

            mercaderiaentransito = (SumaTotalGiroExterior + pagoplanillaaduanera + SumaTotalSeguroDeEquipos +
               prorrateodecostos_transporte_internacional + prorrateodecostos_transporte_nacional +
               prorrateodecostos_logisticaparatransporte + prorrateodecostos_mscltda +
               prorrateodecostos_nrofactmsc + prorrateodecostos_aspb);

            totalcostopoliza = (SumaTotalGiroExterior + planillaaduanera + SumaTotalSeguroDeEquipos +
               prorrateodecostos_transporte_internacional + prorrateodecostos_transporte_nacional +
               prorrateodecostos_logisticaparatransporte + prorrateodecostos_mscltda +
               prorrateodecostos_nrofactmsc + prorrateodecostos_aspb - iva_cf_poliza);

            //--------------------------------------------------------------

            string consulta = "insert into tb_polizas_costos ( " +
                               " nrodui , " +
                               " fechagra , " +
                               " horagra , " +
                               " fechafactura , " +
                               " nitproveedor ," +
                               " nombrerazonsocialproveedor , " +
                               " impbaseparacreditofiscal , " +
                               " creditofiscal , " +
                               " iva_cf_poliza , " +
                               " planillaaduanera , " +
                               " pagoplanillaaduanera , " +
                               " iva_cf_planillaaduanera , " +
                               " valornetoplanillaaduanera , " +
                               " dif_enpago_pa , " +
                               " prorrateodecostos_transporte_internacional , " +
                               " prorrateodecostos_transporte_nacional , " +
                               " prorrateodecostos_nrofacttransptnaceinter , " +
                               " prorrateodecostos_logisticaparatransporte , " +
                               " prorrateodecostos_nrofactlogistica , " +
                               " prorrateodecostos_mscltda , " +
                               " prorrateodecostos_nrofactmsc , " +
                               " prorrateodecostos_aspb , " +
                               " prorrateodecostos_nrodepositooplanillaaspb , " +
                               " mercaderiaentransito , " +
                               " totalcostopoliza , " +
                               " cantidad , " +
                               " descripciondelproducto , " +
                               " transexpresadobolivianos_total , " +
                               " observaciones, " +
                               " nrofacturaagencia, " +
                               " importe_fac," +
                               " importe_almacenera, " +
                               " nrofactura_almacenera)" +
                               " values( " +
                               "'" + nrodui + "'," +
                               " current_date, " +
                               " current_time, " +
                               fechafactura + "," +
                               "'" + nitproveedor + "'," +
                               "'" + nombrerazonsocialproveedor + "'," +
                               "'" + impbaseparacreditofiscal.ToString().Replace(',', '.') + "'," +
                               "'" + creditofiscal.ToString().Replace(',', '.') + "'," +
                               "'" + iva_cf_poliza.ToString().Replace(',', '.') + "'," +
                               "'" + planillaaduanera.ToString().Replace(',', '.') + "'," +
                               "'" + pagoplanillaaduanera.ToString().Replace(',', '.') + "'," +
                               "'" + iva_cf_planillaaduanera.ToString().Replace(',', '.') + "'," +
                               "'" + valornetoplanillaaduanera.ToString().Replace(',', '.') + "'," +
                               "'" + dif_enpago_pa.ToString().Replace(',', '.') + "'," +
                               "'" + prorrateodecostos_transporte_internacional.ToString().Replace(',', '.') + "'," +
                               "'" + prorrateodecostos_transporte_nacional.ToString().Replace(',', '.') + "'," +
                               "'" + prorrateodecostos_nrofacttransptnaceinter.ToString().Replace(',', '.') + "'," +
                               "'" + prorrateodecostos_logisticaparatransporte.ToString().Replace(',', '.') + "'," +
                               "'" + prorrateodecostos_nrofactlogistica.ToString().Replace(',', '.') + "'," +
                               "'" + prorrateodecostos_mscltda.ToString().Replace(',', '.') + "'," +
                               "'" + prorrateodecostos_nrofactmsc.ToString().Replace(',', '.') + "'," +
                               "'" + prorrateodecostos_aspb.ToString().Replace(',', '.') + "'," +
                               "'" + prorrateodecostos_nrodepositooplanillaaspb + "'," +
                               "'" + mercaderiaentransito.ToString().Replace(',', '.') + "'," +
                               "'" + totalcostopoliza.ToString().Replace(',', '.') + "'," +
                               "'" + cantidad.ToString().Replace(',', '.') + "'," +
                               "'" + descripciondelproducto + "'," +
                               "'" + transexpresadobolivianos_total.ToString().Replace(',', '.') + "'," +
                               "'" + observaciones + "'," +
                               "'" + nroFacturaAgencia + "'," +
                               "'" + importeFactura.ToString().Replace(',', '.') + "'" +
                               "'" + importe_almacenera.ToString().Replace(',', '.') + "', " +
                               "'" + nrofactura_almacenera + "')";
            return cnx.ejecutarMySql(consulta);
        }


        public float SumaTotalGiroAlExterior(string nrodui)
        {
            string consulta = "select sum(ff.giroalexterior) from tb_facturaproveedorescomercial ff " +
                                " where " +
                                " ff.codnrodui = '" + nrodui + "'";
            DataSet tupla = cnx.consultaMySql(consulta);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                float suma;
                float.TryParse(tupla.Tables[0].Rows[0][0].ToString().Replace('.', ','), out suma);
                return suma;
            }
            else
                return 0;
        }

        public float SumaTotalSeguroEquipo(string nrodui)
        {
            string consulta = "select sum(ff.seguro) from tb_detalle_facturaseguroequipo ff " +
                                " where " +
                                " ff.codnrodui =  '" + nrodui + "'";
            DataSet tupla = cnx.consultaMySql(consulta);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                float suma;
                float.TryParse(tupla.Tables[0].Rows[0][0].ToString().Replace('.', ','), out suma);
                return suma;
            }
            else
                return 0;
        }

        public bool updatecostospoliza(string nrodui, string fechafactura, string nitproveedor, string nombrerazonsocialproveedor, float impbaseparacreditofiscal,
                                       float creditofiscal,
                                       float iva_cf_poliza, float planillaaduanera,
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

            //-----------calculos----------
            float pocentaje13 = float.Parse("0,13");
            impbaseparacreditofiscal = creditofiscal / pocentaje13;
            iva_cf_planillaaduanera = importeFactura * pocentaje13;

            iva_cf_poliza = (creditofiscal + iva_cf_planillaaduanera);
            valornetoplanillaaduanera = (planillaaduanera - iva_cf_planillaaduanera);
            dif_enpago_pa = (planillaaduanera - pagoplanillaaduanera);



            float SumaTotalGiroExterior = SumaTotalGiroAlExterior(nrodui);
            float SumaTotalSeguroDeEquipos = SumaTotalSeguroEquipo(nrodui);

            mercaderiaentransito = (SumaTotalGiroExterior + pagoplanillaaduanera + SumaTotalSeguroDeEquipos +
               prorrateodecostos_transporte_internacional + prorrateodecostos_transporte_nacional +
               prorrateodecostos_logisticaparatransporte + prorrateodecostos_mscltda + prorrateodecostos_aspb);

            totalcostopoliza = (SumaTotalGiroExterior + planillaaduanera + SumaTotalSeguroDeEquipos +
               prorrateodecostos_transporte_internacional + prorrateodecostos_transporte_nacional +
               prorrateodecostos_logisticaparatransporte + prorrateodecostos_mscltda +
               prorrateodecostos_aspb - iva_cf_poliza);

            //--------------------------------------------------------------
            string consulta = "update tb_polizas_costos set " +
                               " fechafactura = " + fechafactura + " , " +
                               " nitproveedor = '" + nitproveedor + "'," +
                               " nombrerazonsocialproveedor = '" + nombrerazonsocialproveedor + "', " +
                               " impbaseparacreditofiscal = '" + impbaseparacreditofiscal.ToString().Replace(',', '.') + "', " +
                               " creditofiscal = '" + creditofiscal.ToString().Replace(',', '.') + "', " +
                               " iva_cf_poliza = '" + iva_cf_poliza.ToString().Replace(',', '.') + "', " +
                               " planillaaduanera = '" + planillaaduanera.ToString().Replace(',', '.') + "' , " +
                               " pagoplanillaaduanera = '" + pagoplanillaaduanera.ToString().Replace(',', '.') + "' , " +
                               " iva_cf_planillaaduanera = '" + iva_cf_planillaaduanera.ToString().Replace(',', '.') + "' , " +
                               " valornetoplanillaaduanera = '" + valornetoplanillaaduanera.ToString().Replace(',', '.') + "' , " +
                               " dif_enpago_pa = '" + dif_enpago_pa.ToString().Replace(',', '.') + "' , " +
                               " prorrateodecostos_transporte_internacional = '" + prorrateodecostos_transporte_internacional.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_transporte_nacional = '" + prorrateodecostos_transporte_nacional.ToString().Replace(',', '.') + "' , " +
                               " prorrateodecostos_nrofacttransptnaceinter = '" + prorrateodecostos_nrofacttransptnaceinter.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_logisticaparatransporte = '" + prorrateodecostos_logisticaparatransporte.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_nrofactlogistica = '" + prorrateodecostos_nrofactlogistica.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_mscltda = '" + prorrateodecostos_mscltda.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_nrofactmsc = '" + prorrateodecostos_nrofactmsc.ToString().Replace(',', '.') + "' , " +
                               " prorrateodecostos_aspb = '" + prorrateodecostos_aspb.ToString().Replace(',', '.') + "', " +
                               " prorrateodecostos_nrodepositooplanillaaspb = '" + prorrateodecostos_nrodepositooplanillaaspb + "', " +
                               " mercaderiaentransito = '" + mercaderiaentransito.ToString().Replace(',', '.') + "', " +
                               " totalcostopoliza = '" + totalcostopoliza.ToString().Replace(',', '.') + "' , " +
                               " cantidad = '" + cantidad.ToString().Replace(',', '.') + "', " +
                               " descripciondelproducto = '" + descripciondelproducto + "', " +
                               " transexpresadobolivianos_total = '" + transexpresadobolivianos_total.ToString().Replace(',', '.') + "', " +
                               " observaciones = '" + observaciones + "', " +
                               " nrofacturaagencia = '" + nroFacturaAgencia + "', " +
                               " importe_fac= '" + importeFactura.ToString().Replace(',', '.') + "', " +
                               " importe_almacenera = '" + importe_almacenera.ToString().Replace(',', '.') + "'," +
                               " nrofactura_almacenera = '" + nrofactura_almacenera + "'" +
                               " where nrodui = '" + nrodui + "'";
            return cnx.ejecutarMySql(consulta);
        }

        public DataSet get_PolizasCostos(string NroDUI, string baseDatos)
        {
            String consulta = "select " +
                               " nrodui, " +
                               " date_format(fechagra,'%d/%m/%Y') as 'fechagra' , " +
                               " horagra , " +
                               " date_format(fechafactura,'%d/%m/%Y') as 'fechafactura' , " +
                               " nitproveedor, " +
                               " nombrerazonsocialproveedor , impbaseparacreditofiscal , creditofiscal , " +
                               " iva_cf_poliza , planillaaduanera , pagoplanillaaduanera , " +
                               " iva_cf_planillaaduanera , valornetoplanillaaduanera , dif_enpago_pa , " +
                               " prorrateodecostos_transporte_internacional , prorrateodecostos_transporte_nacional , " +
                               " prorrateodecostos_nrofacttransptnaceinter , prorrateodecostos_logisticaparatransporte , " +
                               " prorrateodecostos_nrofactlogistica , prorrateodecostos_mscltda , prorrateodecostos_nrofactmsc , " +
                               " prorrateodecostos_aspb , prorrateodecostos_nrodepositooplanillaaspb , mercaderiaentransito , " +
                               " totalcostopoliza , cantidad , descripciondelproducto , " +
                               " transexpresadobolivianos_total , " +
                               " observaciones,  " +
                               " nrofacturaagencia," +
                               " importe_fac," +
                               " importe_almacenera," +
                               " nrofactura_almacenera" +
                               " from  " +
                               " tb_polizas_costos pp " +
                               " where  " +
                               " pp.nrodui = '" + NroDUI + "'";
            conexionMySql cnx = new conexionMySql(baseDatos);
            return cnx.consultaMySql(consulta);
        }


        public DataSet get_ALLPolizasCostos(string NroDUI)
        {
            String consulta = "select " +
                               " nrodui, " +
                               " date_format(fechagra,'%d/%m/%Y') as 'fechagra' , " +
                               " horagra , " +
                               " date_format(fechafactura,'%d/%m/%Y') as 'fechafactura' , " +
                               " nitproveedor, " +
                               " nombrerazonsocialproveedor , impbaseparacreditofiscal , creditofiscal , " +
                               " iva_cf_poliza , planillaaduanera , pagoplanillaaduanera , " +
                               " iva_cf_planillaaduanera , valornetoplanillaaduanera , dif_enpago_pa , " +
                               " prorrateodecostos_transporte_internacional , prorrateodecostos_transporte_nacional , " +
                               " prorrateodecostos_nrofacttransptnaceinter , prorrateodecostos_logisticaparatransporte , " +
                               " prorrateodecostos_nrofactlogistica , prorrateodecostos_mscltda , prorrateodecostos_nrofactmsc , " +
                               " prorrateodecostos_aspb , prorrateodecostos_nrodepositooplanillaaspb , mercaderiaentransito , " +
                               " totalcostopoliza , cantidad , descripciondelproducto , " +
                               " transexpresadobolivianos_total , " +
                               " observaciones  " +
                               " from  " +
                               " tb_polizas_costos pp " +
                               " where  " +
                               " pp.nrodui LIKE '%" + NroDUI + "%'";
            return cnx.consultaMySql(consulta);
        }


        public DataSet get_ALLPolizasCostos_Mejorado(string NroDUI, string nombrerazonsocialproveedor)
        {
            String consulta = "select " +
                               " date_format(pp.fechafactura,'%d/%m/%Y') as 'fechafactura' , " +
                               " pp.nitproveedor , " +
                               " pp.nombrerazonsocialproveedor , " +
                               " pp.nrodui ,  " +
                               " pp.impbaseparacreditofiscal , " +
                               " pp.creditofiscal , " +
                               " pp.iva_cf_poliza ,  " +
                               " pp.planillaaduanera ,  " +
                               " pp.pagoplanillaaduanera , " +
                               " pp.nrofacturaagencia, " +
                               " pp.importe_fac, " +
                               " pp.iva_cf_planillaaduanera , " +
                               " pp.valornetoplanillaaduanera ,  " +
                               " pp.dif_enpago_pa , " +
                               " pp.prorrateodecostos_transporte_internacional , " +
                               " pp.prorrateodecostos_transporte_nacional ,  " +
                               " pp.prorrateodecostos_nrofacttransptnaceinter , " +
                               " pp.prorrateodecostos_logisticaparatransporte ,  " +
                               " pp.prorrateodecostos_nrofactlogistica , " +
                               " pp.prorrateodecostos_mscltda , " +
                               " pp.prorrateodecostos_nrofactmsc , " +
                               " pp.prorrateodecostos_aspb , " +
                               " pp.prorrateodecostos_nrodepositooplanillaaspb , " +
                               " pp.mercaderiaentransito , " +
                               " pp.totalcostopoliza , " +
                               " pp.cantidad , " +
                               " pp.descripciondelproducto , " +
                               " pp.transexpresadobolivianos_total , " +
                               " pp.observaciones  " +
                               " from  " +
                               " tb_polizas_costos pp " +
                               " where " +
                               " pp.nrodui LIKE '%" + NroDUI + "%' and " +
                               " pp.nombrerazonsocialproveedor like '%" + nombrerazonsocialproveedor + "%'";
            return cnx.consultaMySql(consulta);
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
            string consulta = "insert into tb_facturaproveedorescomercial( " +
                                " fecha , " +
                                " hora , " +
                                " facturacomercial , " +
                                " giroalexterior , " +
                                " proveedoresporpagar , " +
                                " transexpbolivianos_moneda , " +
                                " transexpresadobolivianos_girofacturacomercial , " +
                                
                                " transexpresadobolivianos_tcdolares_girofacturacomercial,"+
                                " transexpresadobolivianos_girofacturacomercial_euro,"+
                                " transexpresadobolivianos_tceuro_girofacturacomercial,"+

                                " transexpresadobolivianos_comision , " +
                                " transexpresadobolivianos_itf , " +
                                " transexpresadobolivianos_difdecambio , " +
                                " codnrodui) " +
                                " values( " +
                                " current_date(), " +
                                " current_time(), " +
                                " '" + facturacomercial + "', " +
                                " '" + giroalexterior.ToString().Replace(',', '.') + "', " +
                                " '" + proveedoresporpagar + "', " +
                                " '" + transexpbolivianos_moneda + "', " +
                                " '" + transexpresadobolivianos_girofacturacomercial.ToString().Replace(',', '.') + "', " +

                                " '" + transexpresadobolivianos_tcdolares_girofacturacomercial.ToString().Replace(',', '.') + "'," +
                                " '" + transexpresadobolivianos_girofacturacomercial_euro.ToString().Replace(',', '.') + "'," +
                                " '" + transexpresadobolivianos_tceuro_girofacturacomercial.ToString().Replace(',', '.') + "'," +

                                " '" + transexpresadobolivianos_comision.ToString().Replace(',', '.') + "', " +
                                " '" + transexpresadobolivianos_itf.ToString().Replace(',', '.') + "', " +
                                " '" + transexpresadobolivianos_difdecambio.ToString().Replace(',', '.') + "', " +
                                " '" + codnrodui + "')";

            return cnx.ejecutarMySql(consulta);
        }

        internal DataSet getFacturasDui(string NroDUI)
        {
            string consulta = "select " +
                               " codigo ," +
                               " codnrodui, " +
                               " facturacomercial , " +
                               " giroalexterior , " +
                               " proveedoresporpagar , " +
                               " transexpbolivianos_moneda , " +
                               " transexpresadobolivianos_girofacturacomercial , " +
                               " transexpresadobolivianos_tcdolares_girofacturacomercial, "+
                               " transexpresadobolivianos_girofacturacomercial_euro, "+
                               " transexpresadobolivianos_tceuro_girofacturacomercial, "+
                               " transexpresadobolivianos_comision , " +
                               " transexpresadobolivianos_itf , " +
                               " transexpresadobolivianos_difdecambio,  " +
                               " (transexpresadobolivianos_girofacturacomercial+transexpresadobolivianos_comision+transexpresadobolivianos_itf) as 'Total_Importe_DOLARES', "+
                               " (transexpresadobolivianos_girofacturacomercial_euro+transexpresadobolivianos_comision+transexpresadobolivianos_itf) as 'Total_Importe_EUROS' " +
                               " from  " +
                               " tb_facturaproveedorescomercial ff " +
                               " where  " +
                               " ff.codnrodui = '" + NroDUI + "'";
            return cnx.consultaMySql(consulta);
        }

        internal bool eliminarFacturasProveedores(int codigoFactura)
        {
            string consulta = "delete from tb_facturaproveedorescomercial " +
                               " where " +
                               " tb_facturaproveedorescomercial.codigo = " + codigoFactura;
            return cnx.ejecutarMySql(consulta);
        }

        internal bool eliminarSeguroEquipo(string codnroDui, int codEquipo)
        {
            string consulta = "delete from tb_detalle_facturaseguroequipo  " +
                                " where  " +
                                " tb_detalle_facturaseguroequipo.codnrodui = '" + codnroDui + "' and " +
                                " tb_detalle_facturaseguroequipo.codequipo = " + codEquipo;
            return cnx.ejecutarMySql(consulta);
        }

        internal bool agrearEquiposADui(string codnrodui, int codequipo, string exbo, string nrofactura, float seguro)
        {
            string codEquipoAux = "null";
            if (codequipo > -1)
            {
                codEquipoAux = codequipo.ToString();
            }
            string consulta = "insert into tb_detalle_facturaseguroequipo( codnrodui, codequipo, exbo, fecha, " +
                              " hora, nrofactura, seguro) " +
                              " values( '" + codnrodui + "', " + codEquipoAux + ", '" + exbo + "' ,current_date(), current_time(), '" + nrofactura + "', '" + seguro.ToString().Replace(',', '.') + "')";
            return cnx.ejecutarMySql(consulta);
        }


        internal bool agrearEquiposADuiGeneral(string codnrodui, string exbo, string nrofactura, float seguro, string fechaFacturaSeguro, string nitSeguro, string BaseDeDatos)
        {

            string consulta = "update tb_importacion_datos set " +
                                " tb_importacion_datos.nrofactura_seguro = '" + nrofactura + "', " +
                                " tb_importacion_datos.fechafactura_seguro = " + fechaFacturaSeguro + " , " +
                                " tb_importacion_datos.nitoci_seguro = '" + nitSeguro + "', " +
                                " tb_importacion_datos.codnrodui  = '" + codnrodui + "'," +
                                " tb_importacion_datos.monto_seguro = '" + seguro.ToString().Replace(',', '.') + "' " +
                                " where  " +
                                " tb_importacion_datos.exbo ='" + exbo + "'";
            conexionMySql cnxaux = new conexionMySql(BaseDeDatos);
            return cnxaux.ejecutarMySql(consulta);
        }


        internal DataSet mostrarEquiposDui(string codnrodui)
        {
            string consulta = "select  " +
                               " dd.codnrodui,  " +
                               " dd.exbo, " +
                               " date_format(dd.fecha, '%d/%m/%Y') as 'fechaGrabacion', " +
                               " dd.hora as 'horaGrabacion',  " +
                               " dd.nrofactura,  " +
                               " dd.seguro " +
                               " from " +
                               " tb_detalle_facturaseguroequipo dd " +
                               " where " +
                               " dd.codnrodui = '" + codnrodui + "'";
            return cnx.consultaMySql(consulta);
        }

        internal bool actualizarEquiposSeguro_FacturaProveedores(string codnrodui)
        {
            float SumaTotalGiroExterior = SumaTotalGiroAlExterior(codnrodui);
            float SumaTotalSeguroDeEquipos = SumaTotalSeguroEquipo(codnrodui);
            string consulta = "update tb_polizas_costos " +
                               " set " +
                               " tb_polizas_costos.mercaderiaentransito =  " +
                               " '" + SumaTotalGiroExterior.ToString().Replace(',', '.') + "' + tb_polizas_costos.pagoplanillaaduanera +  " +
                               " '" + SumaTotalSeguroDeEquipos.ToString().Replace(',', '.') + "' + tb_polizas_costos.prorrateodecostos_transporte_internacional +  " +
                               " tb_polizas_costos.prorrateodecostos_transporte_nacional + tb_polizas_costos.prorrateodecostos_logisticaparatransporte +  " +
                               " tb_polizas_costos.prorrateodecostos_mscltda + tb_polizas_costos.prorrateodecostos_aspb , " +
                               " tb_polizas_costos.totalcostopoliza =  " +
                               " '" + SumaTotalGiroExterior.ToString().Replace(',', '.') + "' + tb_polizas_costos.planillaaduanera +  " +
                               " '" + SumaTotalSeguroDeEquipos.ToString().Replace(',', '.') + "' + tb_polizas_costos.prorrateodecostos_transporte_internacional +  " +
                               " tb_polizas_costos.prorrateodecostos_transporte_nacional + tb_polizas_costos.prorrateodecostos_logisticaparatransporte +  " +
                               " tb_polizas_costos.prorrateodecostos_mscltda + " +
                               " tb_polizas_costos.prorrateodecostos_aspb - tb_polizas_costos.iva_cf_poliza " +
                               " where " +
                               " tb_polizas_costos.nrodui = '" + codnrodui + "'";
            return cnx.ejecutarMySql(consulta);
        }

        internal DataSet get_detalleFacturaProovedoresComercial_DUI(string NroDUI)
        {
            string consulta = "select " +
                               " fc.codnrodui, " +
                               " date_format(fc.fecha, '%d/%m/%Y') as 'fecha', " +
                               " fc.hora, " +
                               " fc.facturacomercial, " +
                               " fc.giroalexterior, " +
                               " fc.proveedoresporpagar, " +
                               " fc.transexpbolivianos_moneda, " +
                               " fc.transexpresadobolivianos_girofacturacomercial, " +
                               " fc.transexpresadobolivianos_comision, " +
                               " fc.transexpresadobolivianos_itf, " +
                               " fc.transexpresadobolivianos_difdecambio " +
                               " from  " +
                               " tb_facturaproveedorescomercial fc " +
                               " where " +
                               " fc.codnrodui = '" + NroDUI + "'";
            return cnx.consultaMySql(consulta);

        }



        internal DataSet get_detalleSeguroEquipo(string codnrodui)
        {
            string consulta = "select  " +
                               " date_format(dd.fecha, '%d/%m/%Y') as 'fecha', " +
                               " dd.hora as 'hora', " +
                               " dd.exbo, " +
                               " dd.nrofactura, " +
                               " dd.seguro, " +
                               " dd.codnrodui " +
                               " from " +
                               " tb_detalle_facturaseguroequipo dd " +
                               " where " +
                               " dd.codnrodui = '" + codnrodui + "'";
            return cnx.consultaMySql(consulta);
        }


        internal DataSet buscar_EquiposDUIGeneral(string nroDUI)
        {
            List<string> listBaseDatos = NA_VariablesGlobales.listBaseDatos;
            string consulta = "";
            for (int i = 0; i < listBaseDatos.Count; i++)
            {
                string baseDatos = listBaseDatos[i];
                string UNE;
                string Ciudad;
                if (baseDatos.Equals("db_seguimientoscz_jyc"))
                {
                    UNE = "Intelogi";
                    Ciudad = "SCZ";
                }
                else
                    if (baseDatos.Equals("db_seguimientocbba_jyc"))
                    {
                        UNE = "Melevar";
                        Ciudad = "CBBA";
                    }
                    else
                        if (baseDatos.Equals("db_seguimientolpz_jyc"))
                        {
                            UNE = "Elevamerica";
                            Ciudad = "LPZ";
                        }
                        else
                            if (baseDatos.Equals("db_seguimientosucre_jyc"))
                            {
                                UNE = "JYC";
                                Ciudad = "Sucre";
                            }
                            else
                                if (baseDatos.Equals("db_seguimientooruro_jyc"))
                                {
                                    UNE = "JYC";
                                    Ciudad = "Oruro";
                                }
                                else
                                    if (baseDatos.Equals("db_seguimientobeni_jyc"))
                                    {
                                        UNE = "JYC";
                                        Ciudad = "Beni";
                                    }
                                    else
                                        if (baseDatos.Equals("db_seguimientopando_jyc"))
                                        {
                                            UNE = "JYC";
                                            Ciudad = "Pando";
                                        }
                                        else
                                            if (baseDatos.Equals("db_seguimientotarija_jyc"))
                                            {
                                                UNE = "JYC";
                                                Ciudad = "Tarija";
                                            }
                                            else
                                                if (baseDatos.Equals("db_seguimientoyacuiba_jyc"))
                                                {
                                                    UNE = "JYC";
                                                    Ciudad = "Yacuiba";
                                                }
                                                else
                                                    if (baseDatos.Equals("db_seguimientopotosi_jyc"))
                                                    {
                                                        UNE = "JYC";
                                                        Ciudad = "Potosi";
                                                    }
                                                    else
                                                        if (baseDatos.Equals("db_seguimientovillamontes_jyc"))
                                                        {
                                                            UNE = "JYC";
                                                            Ciudad = "Villamontes";
                                                        }
                                                        else
                                                            if (baseDatos.Equals("db_seguimientoparaguay_nuevo"))
                                                            {
                                                                UNE = "JYC";
                                                                Ciudad = "Paraguay";
                                                            }
                                                            else
                                                            {
                                                                UNE = "JYC";
                                                                Ciudad = "Otro";
                                                            }

                if (i > 0)
                    consulta = consulta + " UNION ";


                consulta = consulta + "select " +
                                " eq.codigo,  " +
                                " eq.exbo, " +
                                " pp.nombre as 'Edificio',  " +
                                " eq.vendidoenciudad as 'Vendido',  " +
                                " eq.instaladoenciudad as 'Instalado', " +
                                " '" + UNE + "' AS 'UNE',  " +
                                " '" + Ciudad + "' as 'CIUDAD',  " +
                                " eeq.nombre as 'Estado1' , " +
                                " id.codnrodui as 'NroDui', " +
                                " id.codnrocontenedor as 'Contenedor', " +
                                " id.consignatario as 'Consignatario', " +
                                " date_format(id.fechafactura_seguro,'%d/%m/%Y') as 'FechaFactura', " +
                                " id.nitoci_seguro as 'NIT', " +
                                " id.nrofactura_seguro as 'NroFactura', " +
                                " id.monto_seguro as 'MontoSeguro', " +
                                " id.nroaplicaciondelseguro" +
                                " from  " +
                                baseDatos + ".tb_proyecto pp, " +
                                baseDatos + ".tb_equipo eq " +
                                " LEFT JOIN " + baseDatos + ".tb_importacion_datos id ON (eq.codimportacion = id.codigo) , " +
                                baseDatos + ".tb_fechaestadoequipo fe, " + baseDatos + ".tb_estado_equipo eeq " +
                                " where  " +
                                " eq.cod_proyecto = pp.codigo and " +
                                " eq.codfechaestadoequipo = fe.codigo and " +
                                " fe.codEstadoEquipo = eeq.codigo " +
                                " and id.codnrodui = '" + nroDUI + "'";

            }

            return cnx.consultaMySql(consulta);
        }


        internal bool eliminarEquiposADuiGeneral(string codnrodui, string exbo, string baseDatos)
        {
            string consulta = "update tb_importacion_datos set " +
                             " tb_importacion_datos.codnrodui  = ''" +
                             " where  " +
                             " tb_importacion_datos.exbo ='" + exbo + "'" +
                             " and tb_importacion_datos.codnrodui = '" + codnrodui + "'";
            conexionMySql cnxaux = new conexionMySql(baseDatos);
            return cnxaux.ejecutarMySql(consulta);
        }

        internal bool insertarDui(string NroDui, string baseDatos)
        {
            string consulta = "insert into tb_polizas_costos ( " +
                             " nrodui , " +
                             " fechagra , " +
                             " horagra )" +
                             " values( " +
                             "'" + NroDui + "'," +
                             " current_date, " +
                             " current_time)";
            conexionMySql cnxaux = new conexionMySql(baseDatos);
            return cnxaux.ejecutarMySql(consulta);
        }
    }
}