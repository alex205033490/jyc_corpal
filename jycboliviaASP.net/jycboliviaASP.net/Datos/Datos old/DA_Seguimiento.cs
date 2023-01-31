using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_Seguimiento
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DA_Seguimiento() { }

        public bool insertar(string detalle, string hora_Cobro, string dia_cobro, string lugarPago, string fechaContrato, int mesesGratis, string mg_ini, string mg_fin, int codTipoPago, int codEquipo, int estado, int year)
        {
           
                string consulta = "insert into tb_seguimiento(detalle,hora_cobro,dia_cobro,lugar_pago,fecha_contrato,mes_gratis,mg_ini,mg_fin,cod_tipopago,cod_equipo,estado,years) values('" + detalle + "'," + hora_Cobro + "," + dia_cobro + ",'" + lugarPago + "'," + fechaContrato + "," + mesesGratis + "," + mg_ini + "," + mg_fin + "," + codTipoPago + "," + codEquipo + "," + estado + "," + year + ")";
                return ConecRes.ejecutarMySql(consulta);
                
           
        }

        public bool modificar(int codigo, string detalle, string hora_Cobro, string dia_cobro, string lugarPago, string fechaContrato, int mesesGratis, string mg_ini, string mg_fin, int codTipoPago, int year, int codfechaEstadoMan)
        {
           
                string consulta = "update tb_seguimiento set " +
                                "Detalle = '" + detalle + "'," +
                                "hora_cobro = " + hora_Cobro + "," +
                                "dia_cobro  = " + dia_cobro + "," +
                                "lugar_pago = '" + lugarPago + "'," +                                
                                "fecha_contrato = " + fechaContrato + "," +
                                "mes_gratis =  " + mesesGratis + "," +
                                "mg_ini  =  " + mg_ini + "," +
                                "mg_fin  =  " + mg_fin + "," +
                                "cod_tipopago = " + codTipoPago + "," +
                                "years = " + year + ","+
                                "codfechaestadoman = " + codfechaEstadoMan +
                                " where codigo = " + codigo;
                return ConecRes.ejecutarMySql(consulta);
          
            
        }

        public bool modificarFechaEstadoMan(int codSeguimiento, int codfechaEstadoMan) {
           
                string consulta = "update tb_seguimiento set " +                               
                               "codfechaestadoman = " + codfechaEstadoMan +
                               " where codigo = " + codSeguimiento;
                return ConecRes.ejecutarMySql(consulta);
                              
        }

        public bool eliminar(int codigo, int estado)
        {
                string consulta = "update tb_seguimiento set estado="+estado+" where codigo="+codigo;
                return ConecRes.ejecutarMySql(consulta);
            
            
        }

        public DataSet getDatos(string consulta)
        {            
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        public DataSet getTablaMySqlRellenada(DataSet TablaMySql, string consulta)
        {
            DataSet datosR = ConecRes.RellenarConConsulta(TablaMySql,consulta);
            return datosR;
        }

        public DataSet getCuadrosXXX_Mantenimiento(int year, string Exbo, string nombreProyecto)
        {
            string consulta = "select eq1.exbo, proy1.nombre as 'Edificio', eq1.parada, eq1.pasajero, eq1.velocidad ,se.years , rr.nombre as 'Cobrador' , " +                                                                
                                " rrin.nombre as 'Rin', "+
                                " rrcc.nombre as 'RCC', "+
                                " rtec.nombre as 'TecMantenimiento', "+
                                " rsup.nombre as 'Supervisor' , "+
                                " date_format(fechacon.fechafirmacontrato,'%d/%m/%Y') as 'FechaContratoFirmado' ," +
                                " date_format(eq1.fechahabilitacionequipo,'%d/%m/%Y') as 'Habilitacion del Equipo', "+
                                 "if(" +
                                 "(" +
                                 ///---- esto nos marca si tiene por lomenos una deuda nos da critico
                                " select sum(t1.deuda) " +
                                 " from tb_equipo eq, tb_proyecto proy, " +
                                 " (select  " +
                                 " segme.codSeg,seg.cod_equipo, sum(segme.monto_pagar)-sum(segme.monto_pago) as 'deuda' " +
                                 " from tb_detalle_segme segme, tb_seguimiento seg " +                                 
                                 " where  " +
                                 " segme.codSeg = seg.codigo and " +
                                 " seg.years < " + year +
                                 " group by segme.codSeg) as t1 " +
                                 " where  " +                                 
                                 " eq.cod_proyecto = proy.codigo and " +
                                 " eq.codigo = t1.cod_equipo and " +
                                 " eq.codigo = eq1.codigo " +
                                 " group by eq.codigo  " +  

                              ///-------------------nuevo---------------
                                 /*   " select count(t1.deuda) "+
                                    " from tb_equipo eq, tb_proyecto proy, "+
                                    " ( select   "+
                                    " segme.codSeg,seg.cod_equipo, segme.codMe, segme.monto_pagar-segme.monto_pago as 'deuda'  "+
                                    " from tb_detalle_segme segme, tb_seguimiento seg  "+
                                    " where  "+
                                    " segme.codSeg = seg.codigo and "+
                                    " seg.years < "+year+
                                    " having (deuda) > 0 ) as t1 "+
                                    " where "+               
                                    " eq.cod_proyecto = proy.codigo and "+
                                    " eq.codigo = t1.cod_equipo and "+
                                    " eq.codigo = eq1.codigo " +
                                    " group by eq.codigo "+   */
                              ///------------------fin------------------

                                  " )" +
                                 " > 0  && em.nombre!='Perdido'," +
                                 "'Critico', em.nombre) as 'EstadoMantenimiento'," +

                                 // se modifico para eliminar los null
                                 "IFNULL((" +                                 
                                 " select sum(t1.deuda) " +
                                 " from tb_equipo eq, tb_proyecto proy, " +
                                 " (select  " +
                                 " segme.codSeg,seg.cod_equipo, sum(segme.monto_pagar)-sum(segme.monto_pago) as 'deuda' " +
                                 " from tb_detalle_segme segme, tb_seguimiento seg " +
                                 " where  " +
                                 " segme.codSeg = seg.codigo and " +
                                 " seg.years < " + year +
                                 " group by segme.codSeg) as t1 " +
                                 " where  " +
                                 " eq.cod_proyecto = proy.codigo and " +
                                 " eq.codigo = t1.cod_equipo and " +
                                 " eq.codigo = eq1.codigo " +
                                 " group by eq.codigo  " +                                
                                  " ) ,0) as 'DeudaGestionAnterior'," +
                                  /// fin de deuda anterior
                                 " enero.monto_pagar as 'EneroPagar', enero.monto_pago as 'EneroPago', " +
                                 " febrero.monto_pagar as 'FebreroPagar', febrero.monto_pago as 'FebreroPago', " +
                                 " marzo.monto_pagar as 'MarzoPagar', marzo.monto_pago as 'MarzoPago', " +
                                 " abril.monto_pagar as 'AbrilPagar', abril.monto_pago as 'AbrilPago', " +
                                 " mayo.monto_pagar as 'MayoPagar', mayo.monto_pago as 'MayoPago', " +
                                 " junio.monto_pagar as 'JunioPagar', junio.monto_pago as 'JunioPago', " +
                                 " julio.monto_pagar as 'JulioPagar', julio.monto_pago as 'JulioPago', " +
                                 " agosto.monto_pagar as 'AgostoPagar', agosto.monto_pago as 'AgostoPago', " +
                                 " septiembre.monto_pagar as 'SeptiembrePagar', septiembre.monto_pago as 'SeptiembrePago', " +
                                 " octubre.monto_pagar as 'OctubrePagar', octubre.monto_pago as 'OctubrePago', " +
                                 " noviembre.monto_pagar as 'NoviembrePagar', noviembre.monto_pago as 'NoviembrePago', " +
                                 " diciembre.monto_pagar as 'DiciembrePagar', diciembre.monto_pago as 'DiciembrePago', " +
                                 " (enero.monto_pagar + " +
                                 " febrero.monto_pagar + " +
                                 " marzo.monto_pagar  + " +
                                 " abril.monto_pagar + " +
                                 " mayo.monto_pagar + " +
                                 " junio.monto_pagar + " +
                                 " julio.monto_pagar + " +
                                 " agosto.monto_pagar + " +
                                 " septiembre.monto_pagar + " +
                                 " octubre.monto_pagar + " +
                                 " noviembre.monto_pagar + " +
                                 " diciembre.monto_pagar )  " +
                                 " - " +
                                 " (enero.monto_pago + " +
                                 " febrero.monto_pago + " +
                                 " marzo.monto_pago  + " +
                                 " abril.monto_pago + " +
                                 " mayo.monto_pago + " +
                                 " junio.monto_pago + " +
                                 " julio.monto_pago + " +
                                 " agosto.monto_pago + " +
                                 " septiembre.monto_pago + " +
                                 " octubre.monto_pago + " +
                                 " noviembre.monto_pago + " +
                                 " diciembre.monto_pago ) "+
                                 "+"+
                                //------------suma la deuda anterior 
                                 "IFNULL((" +                                 
                                 " select sum(t1.deuda) " +
                                 " from tb_equipo eq, tb_proyecto proy, " +
                                 " (select  " +
                                 " segme.codSeg,seg.cod_equipo, sum(segme.monto_pagar)-sum(segme.monto_pago) as 'deuda' " +
                                 " from tb_detalle_segme segme, tb_seguimiento seg " +
                                 " where  " +
                                 " segme.codSeg = seg.codigo and " +
                                 " seg.years < " + year +
                                 " group by segme.codSeg) as t1 " +
                                 " where  " +
                                 " eq.cod_proyecto = proy.codigo and " +
                                 " eq.codigo = t1.cod_equipo and " +
                                 " eq.codigo = eq1.codigo " +
                                 " group by eq.codigo  " +                                
                                  " ) ,0) "+
                                //------------fin de la suma de deuda anterior                                  
                                 " as Deuda " +
                                 //---------- el from donde actualizar
                                 " from tb_seguimiento se, tb_fechaestadomantenimiento fm , tb_estado_mantenimiento em, " +
                                 " tb_equipo eq1 " +
                                 " left join tb_responsable rrin on (eq1.cod_rin = rrin.codigo) "+
                                 " left join tb_responsable rrcc on (eq1.cod_rcc = rrcc.codigo) "+
                                 " left join tb_responsable rsup on (eq1.cod_supervisor = rsup.codigo) "+
                                 " left join tb_responsable rtec on (eq1.cod_tecmantenimiento  = rtec.codigo) "+
                                 " left join tb_fechacontrato_firmado fechacon ON fechacon.codigo = eq1.codfechacontratofirmado , " +
                                 " tb_detalle_segme enero, " +
                                 " tb_detalle_segme febrero, " +
                                 " tb_detalle_segme marzo, " +
                                 " tb_detalle_segme abril, " +
                                 " tb_detalle_segme mayo, " +
                                 " tb_detalle_segme junio, " +
                                 " tb_detalle_segme julio, " +
                                 " tb_detalle_segme agosto, " +
                                 " tb_detalle_segme septiembre, " +
                                 " tb_detalle_segme octubre, " +
                                 " tb_detalle_segme noviembre, " +
                                 " tb_detalle_segme diciembre, " +
                                 " tb_proyecto proy1 " +
                                 " left join tb_responsable rr on proy1.codcobradorasignado = rr.codigo " +
                                 " where se.codfechaestadoman = fm.codigo and " +
                                 " fm.codEstadoMan = em.codigo and " +
                                 " se.cod_equipo = eq1.codigo and " +
                                 " eq1.estado = 1 and "+
                                 " eq1.cod_proyecto = proy1.codigo and " +
                                 " se.codigo = enero.codSeg and enero.codMe = 1 and " +
                                 " se.codigo = febrero.codSeg and febrero.codMe = 2 and " +
                                 " se.codigo = marzo.codSeg and marzo.codMe = 3 and " +
                                 " se.codigo = abril.codSeg and abril.codMe = 4 and " +
                                 " se.codigo = mayo.codSeg and mayo.codMe = 5 and " +
                                 " se.codigo = junio.codSeg and junio.codMe = 6 and " +
                                 " se.codigo = julio.codSeg and julio.codMe = 7 and " +
                                 " se.codigo = agosto.codSeg and agosto.codMe = 8 and " +
                                 " se.codigo = septiembre.codSeg and septiembre.codMe = 9 and " +
                                 " se.codigo = octubre.codSeg and octubre.codMe = 10 and " +
                                 " se.codigo = noviembre.codSeg and noviembre.codMe = 11 and " +
                                 " se.codigo = diciembre.codSeg and diciembre.codMe = 12 and " +
                                 " se.years = " + year + " and proy1.nombre like '%" + nombreProyecto + "%' and eq1.exbo like '%" + Exbo + "%' ";
            return getDatos(consulta);        
        }

        public DataSet RellenarCuadrosXXX_SIN_Mantenimiento(DataSet tablaMySql,int year, string Exbo, string nombreProyecto)
        {
            string consulta = "select "+
                               " eq.exbo, "+ 
                               " pp.nombre as 'Edificio', "+
                               " eq.parada, "+
                               " eq.pasajero, "+
                               " eq.velocidad, "+
                               " '0' as 'years',  "+
                               " res.nombre as 'Cobrador', "+
                               //" ee.nombre as 'EstadoMantenimiento', "+
                               " 'Sin Prevision' as 'EstadoMantenimiento', " +
                               " '0'  as 'DeudaGestionAnterior', " +
                               " '0'  as 'EneroPagar', '0'  as 'EneroPago', "+
                               " '0'  as 'FebreroPagar', '0'  as 'FebreroPago', "+
                               " '0'  as 'MarzoPagar', '0'  as 'MarzoPago',  "+
                               " '0'  as 'AbrilPagar', '0'  as 'AbrilPago',  "+
                               " '0'  as 'MayoPagar', '0'  as 'MayoPago',  "+
                               " '0'  as 'JunioPagar', '0'  as 'JunioPago',  "+
                               " '0'  as 'JulioPagar', '0'  as 'JulioPago',  "+
                               " '0'  as 'AgostoPagar', '0'  as 'AgostoPago',  "+
                               " '0'  as 'SeptiembrePagar', '0'  as 'SeptiembrePago',  "+
                               " '0'  as 'OctubrePagar', '0'  as 'OctubrePago',  "+
                               " '0'  as 'NoviembrePagar', '0'  as 'NoviembrePago', "+ 
                               " '0'  as 'DiciembrePagar', '0'  as 'DiciembrePago', "+
                               " '0' as 'Deuda' "+
                               " from tb_equipo eq, tb_fechaestadoequipo ff, tb_estado_equipo ee, "+
                               " tb_proyecto pp "+
                               " left join tb_responsable res on pp.codcobradorasignado = res.codigo  "+
                               " where "+
                               " eq.estado = 1 and "+
                               " eq.codfechaestadoequipo = ff.codigo and "+
                               " ff.codEstadoEquipo = ee.codigo and "+
                               " eq.cod_proyecto = pp.codigo and "+
                               " pp.nombre like '%"+nombreProyecto+"%' and "+
                               " eq.exbo like '%"+Exbo+"%' and "+
                               " ee.nombre = 'Parado por el Cliente' and "+
                               " eq.codigo not in  "+
                               " ( "+
                               " select seg.cod_equipo  "+
                               " from tb_seguimiento seg "+
                               " where "+
                               " seg.years = "+year+
                               " group by seg.cod_equipo "+
                               " )";
            return getTablaMySqlRellenada(tablaMySql, consulta);
        }

        public DataSet getDatosSeguimientoMantenimiento(int codSeguimiento)
        {
            string consulta = "select "+
                               " seg.codigo, seg.Detalle, seg.cod_equipo,seg.years, seg.codfechaestadoman, "+
                               " festado.codEstadoMan "+
                               " from tb_seguimiento seg, tb_fechaestadomantenimiento festado "+
                               " where "+
                               " seg.codfechaestadoman = festado.codigo and "+
                               " seg.codigo = "+codSeguimiento;
            return ConecRes.consultaMySql(consulta);
        }

        public DataSet getSeguimientosConDeudaAnterior(int codSeguimiento, int anioSeg)
        {
            string consulta = " select "+
                              " seg1.codigo as 'codSeg' "+
                              " from "+
                              " tb_seguimiento seg1, "+
                              " ( "+
                              "/*-------saca todo codigo de equipo que deba alguna gestion---*/ "+               
                              " select seg.cod_equipo "+
                              " from tb_detalle_segme det, tb_seguimiento seg "+
                              " ,tb_fechaestadomantenimiento fm  "+
                              " where  "+
                              " seg.codigo = det.codSeg and "+
                              " det.pago = 'NO' and seg.years < "+anioSeg+
                              " and det.codMe <= 12 "+
                              " and seg.codfechaestadoman = fm.codigo "+
                              " /* and fm.codEstadoMan not in (1,3,4,6,7,8) */  "+
                              " group by seg.cod_equipo  having count(det.pago) > 0 "+
                              " /*---------------------*/ "+
                              " ) as t1 "+
                              " where  "+
                              " seg1.cod_equipo = t1.cod_equipo and "+
                              " seg1.years = "+anioSeg+" and "+
                              " seg1.codigo = "+codSeguimiento;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet getDatosaPagar_duranteTodaslasGestiones(string exbo)
        {
            string consulta = "select "+
                               " seg.codigo as 'codseg', "+
                               " mes.codigo as 'codmes', "+
                               " seg.years ,  "+
                               " mes.nombre,  "+
                               " segme.monto_pagar as 'Deuda', "+
                               " segme.monto_pago as 'Pagado', "+                                                          
                               " '0' as 'Pagar' , "+                               
                               " 'Ninguno' as 'Cheque', "+
                               " 'Ninguno' as 'Factura', "+
                               " 'Ninguno' as 'Recibo' "+
                               " from "+
                               " tb_equipo eq, "+
                               " tb_seguimiento seg, "+
                               " tb_detalle_segme segme, "+
                               " tb_mes mes "+
                               " where "+
                               " eq.codigo = seg.cod_equipo and "+
                               " seg.codigo = segme.codSeg and "+
                               " segme.codMe = mes.codigo and "+
                               " segme.pago in ('No', 'Error') and "+
                               " eq.exbo = '"+exbo+"' "+
                               " order by seg.years,mes.codigo asc";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_TipoUltimoTipoCambio()
        {
            string consulta = "select " +
                                " tc.codigo, " +
                                " DATE_FORMAT(tc.fecha,'%d/%m/%Y') as fecha ,  " +
                                " tc.hora as 'hora1', tc.TC " +
                                " from tb_tipocambiodolar tc  " +
                                " order by tc.CODIGO desc limit 1";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_moneda()
        {
            string consulta = "select mm.CODIGO, mm.MONEDA, mm.ABREVIATURA, mm.SIMBOLO from tb_moneda mm";
            return ConecRes.consultaMySql(consulta);
        }

        internal bool generar_Cobranza(string DOCUM, string GLOSA, bool ANULADA, double tipocambio, int codmoneda, int coduser, string clicodigo, string  codvendedor, string fechaPago)
        {
            string consulta = "insert into tb_cobranza_recibio( " +
                               " tb_cobranza_recibio.FECHA, " +
                               " tb_cobranza_recibio.HORAGRA, " +
                               " tb_cobranza_recibio.FECHAGRA, " +
                               " tb_cobranza_recibio.DOCUM, " +
                               " tb_cobranza_recibio.GLOSA, " +
                               " tb_cobranza_recibio.ANULADA, " +
                               " tb_cobranza_recibio.tipocambio, " +
                               " tb_cobranza_recibio.codmoneda, " +
                               " tb_cobranza_recibio.coduser,"+
                               " tb_cobranza_recibio.clicodigo,"+
                               " tb_cobranza_recibio.codvendedor,"+
                               " tb_cobranza_recibio.vaciarsimec "+
                               " ) " +
                               " values(" + fechaPago + ", current_time() ,current_date(), " +
                                "'"+ DOCUM +"', " +
                               " '"+GLOSA +"', " +
                                ANULADA+", " +
                               "'"+tipocambio.ToString().Replace(',','.')+"', " +
                                codmoneda+"," +
                                coduser+","+
                                "'"+clicodigo+"',"+
                                "'"+codvendedor+"',false)";
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet get_ultimocobroIngresado() {
            string consulta = "select "+
                               " cc.codigo, "+
                               " cc.fecha, "+
                               " cc.docum, "+
                               " cc.glosa, "+
                               " cc.anulada, "+
                               " cc.horagra, "+
                               " cc.fechagra, "+
                               " cc.tipocambio, "+
                               " cc.codmoneda, "+
                               " cc.coduser "+
                               " from tb_cobranza_recibio cc "+
                               " order by cc.CODIGO desc limit 1;";
            return ConecRes.consultaMySql(consulta);
        }


        internal DataSet get_cobranzadeldia(int codigoCobranza)
        {
            string consulta = "select "+
                               " cr.codigo, "+
                               " DATE_FORMAT(cr.fecha,'%d/%m/%Y') as fecha, " +
                               " cr.docum, "+
                               " cr.tipocambio, "+
                               " mm.MONEDA, "+
                               " cr.glosa, "+
                               " res.nombre as 'Cobrado por'  "+
                               " from tb_cobranza_recibio cr, tb_moneda mm, tb_responsable res "+
                               " where  "+
                               " cr.codmoneda = mm.CODIGO and "+
                               " cr.coduser = res.codigo and "+
                               " cr.codigo = "+codigoCobranza;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_Recibo_cobranzadeldia(int codigoCobranza)
        {
            string consulta = "select "+
                               " eq.clicodigo as 'codigo_cliente', "+
                               " concat(pp.nombre,'_',eq.exbo) as 'edificio_exbo', "+
                               " re.recibo, "+
                               " DATE_FORMAT(re.fecha,'%d/%m/%Y') as 'fecha_pago', " +
                               " re.banco, "+
                               " re.nrocheque, "+
                               " re.factura, "+
                               " re.pago as 'importe', "+
                               " re.pagobs as 'importe_Bs', "+
                               " re.detalle as 'observaciones' "+
                               " from  "+
                               " tb_recibopago re, tb_moneda mm, "+
                               " tb_seguimiento seg, tb_equipo eq, tb_proyecto pp "+
                               " where "+
                               " re.codmoneda = mm.CODIGO and "+
                               " re.codseg = seg.codigo and "+
                               " seg.cod_equipo = eq.codigo and "+
                               " eq.cod_proyecto = pp.codigo and "+
                               " re.codcobranza = "+codigoCobranza;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet CobrosGeneralesRecibo()
        {
           /* string consulta = "select "+
                               " cr.codigo, "+
                               " DATE_FORMAT(cr.fecha,'%d/%m/%Y') as 'fecha', "+
                               " cr.docum, "+
                               " cr.glosa, "+
                               " cr.anulada, "+
                               " DATE_FORMAT(cr.fechagra,'%d/%m/%Y') as 'fechaGra', "+
                               " cr.horagra, "+
                               " cr.tipocambio, "+
                               " mm.MONEDA, "+
                               " cr.coduser "+
                               " from  "+
                               " tb_cobranza_recibio cr, tb_moneda mm "+
                               " where "+
                               " cr.codmoneda = mm.CODIGO and "+
                               " (cr.vaciarsimec = false or cr.vaciarsimec is null);";  */
            string consulta = "select "+
                               " cr.codigo, "+
                               " DATE_FORMAT(cr.fecha,'%d/%m/%Y') as 'fecha', "+
                               " cr.docum, "+
                               " cr.glosa, "+
                               " t1.Edificio, "+
                               " t1.exbo  , "+
                               " cr.anulada, "+
                               " DATE_FORMAT(cr.fechagra,'%d/%m/%Y') as 'fechaGra', "+
                               " cr.horagra, "+
                               " cr.tipocambio, "+ 
                               " mm.MONEDA, "+
                               " (select Sum(re.pago) from tb_recibopago re where re.codcobranza = cr.codigo) as 'Dolares', "+
                               " (select Sum(re1.pagobs) from tb_recibopago re1 where re1.codcobranza = cr.codigo) as 'Bolivianos', "+
                               " (select count(*) from tb_recibopago re2 where re2.codcobranza = cr.codigo) as 'Meses', "+
                               " res.nombre as 'Cobrador'  "+
                               " from  "+
                               " tb_moneda mm , "+
                               " tb_cobranza_recibio cr "+
                               " LEFT JOIN tb_responsable res ON (cr.coduser = res.codigo) "+
                               " LEFT JOIN "+
                               " ( "+
                               " select "+
                               " re.codcobranza, eq.exbo, pp.nombre as 'Edificio' "+
                               " from "+
                               " tb_recibopago re, tb_seguimiento seg, tb_equipo eq , tb_proyecto pp "+
                               " where "+
                               " eq.cod_proyecto = pp.codigo and "+
                               " seg.cod_equipo = eq.codigo and "+
                               " re.codseg = seg.codigo and "+
                               " re.codcobranza > 0 "+
                               " group by re.codcobranza "+
                               " ) AS t1 ON (t1.codcobranza = cr.codigo) "+
                               " where "+
                               " cr.codmoneda = mm.CODIGO and "+
                               " (cr.vaciarsimec = false or cr.vaciarsimec is null) order by cr.codigo asc";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet fechasNoVaciadasAlSimec()
        {
            string consulta = "select  "+
                               " DATE_FORMAT(cr.fecha,'%d/%m/%Y') as 'fecha' " +
                               " from  "+
                               " tb_cobranza_recibio cr, tb_moneda mm "+
                               " where "+
                               " cr.codmoneda = mm.CODIGO and "+
                               " (cr.vaciarsimec = false or cr.vaciarsimec is null) "+
                               " group by cr.fecha";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet CobrosGeneralesRecibo_porDocumPorFecha(string DOCUM,string fechaDeterminada) {
            string consulta = "select "+
                                " cr.codigo, "+
                                " cr.glosa, "+
                                " cr.coduser, "+
                                " cr.clicodigo, "+
                                " res.codvendedor, "+
                                " re.codigo as 'codRecibo', "+
                                " re.detalle, "+
                                " re.fecha, "+
                                " re.hora, "+
                                " re.codseg, "+
                                " re.codmes, "+
                                " re.codresp, "+
                                " re.efectivo, "+
                                " re.deposito, "+
                                " re.nrocheque, "+
                                " re.banco, "+
                                " re.recibo, "+
                                " re.factura, "+
                                " re.codcobranza, "+
                                " re.codmoneda, "+
                                " re.tipocambio, "+
                                " re.pago, "+
                                " re.pagobs, "+
                                " seg.years, re.transferencia  " +
                                " from   "+
                                " tb_cobranza_recibio cr, tb_recibopago re "+
                                " , tb_seguimiento seg "+
                                " , tb_responsable res "+
                                " where "+
                                " re.codseg = seg.codigo and "+
                                " re.codcobranza = cr.codigo and "+
                                " cr.coduser = res.codigo and "+
                                " cr.docum = '"+DOCUM+"' and "+
                                " cr.fecha = " + fechaDeterminada;
            return ConecRes.consultaMySql(consulta);
        }


        internal DataSet CobrosGeneralesRecibo_porDocumPorClienteSimecPorFecha(string DOCUM, string CodClienteSimec, string fechaDeterminada)
        {
            string consulta = "";
            return ConecRes.consultaMySql(consulta);
        }


        internal DataSet todosCobrosRecibo_porFecha(string DOCUM,string fechaDeterminada)
        {
            string consulta = "select  "+
                               " cr.codigo,  "+
                               " DATE_FORMAT(cr.fecha,'%d/%m/%Y') as 'fecha', "+
                               " cr.docum,  "+
                               " cr.glosa, "+
                               " cr.anulada,  "+
                               " DATE_FORMAT(cr.fechagra,'%d/%m/%Y') as 'fechagra', "+
                               " cr.horagra, "+
                               " cr.tipocambio, "+  
                               " mm.MONEDA,  "+
                               " cr.coduser,  "+
                               " cr.clicodigo,  "+
                               " cr.codvendedor  "+
                               " from   "+
                               " tb_cobranza_recibio cr, tb_moneda mm  "+
                               " where  "+
                               " cr.codmoneda = mm.CODIGO and "+
                               " cr.fecha = "+fechaDeterminada+" and "+
                               " cr.docum = '"+DOCUM+"'";
            return ConecRes.consultaMySql(consulta);
        }

    
     /*     
        /// <summary>
        /// este procedimiento actualiza todos los cobros con el Docum en una fecha determinada
        /// siempre y cuando no haya sido vaciado antes
        /// </summary>
        /// <param name="fechaDeterminada"> fecha en la cual se van actuazlizar todos los registros que no hayan sido vaciados a simec</param>
        /// <param name="DOCUM">la variable de simec que van a compartir en un solo registro</param>
        /// <returns></returns>
        internal bool updateTodosDOCUM_porFecha(string fechaDeterminada, string DOCUM)
        {
            string consulta = "update tb_cobranza_recibio set tb_cobranza_recibio.docum = '"+DOCUM+"'"+
                               ", tb_cobranza_recibio.vaciarsimec = true "+
                               " where  "+
                               " (tb_cobranza_recibio.vaciarsimec = false  "+
                               " or  "+
                               " tb_cobranza_recibio.vaciarsimec is null) and "+
                               " tb_cobranza_recibio.fecha = "+fechaDeterminada;
            return ConecRes.ejecutarMySql(consulta);
        }
        */

        internal bool updateDOCUM(int codigoCobroRecibo, string DOCUM)
        {
            string consulta = "update tb_cobranza_recibio set tb_cobranza_recibio.docum = '"+DOCUM+"'"+
                               " where tb_cobranza_recibio.codigo = "+codigoCobroRecibo;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal DataSet get_recibosCobradosDelaCobranza(int codCobranzaRealizada)
        {
            string consulta = "select "+
                               " re.codigo, "+
                               " re.detalle, "+
                               " re.fecha, "+
                               " re.hora, "+
                               " re.codseg, "+
                               " re.codmes, "+
                               " re.codresp, "+
                               " re.efectivo, "+
                               " re.deposito, "+
                               " re.nrocheque, "+
                               " re.banco, "+
                               " re.recibo, "+
                               " re.factura, "+
                               " re.codcobranza, "+
                               " re.codmoneda, "+
                               " re.tipocambio, "+
                               " re.pagobs "+
                               " from tb_recibopago re "+
                               " where re.codcobranza = "+codCobranzaRealizada;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet SumaTodoslosRecibosdelCobro(int codigoCobro)
        {
            string consulta = "select "+
                                " re.codcobranza, "+
                                " re.banco, "+
                                " re.codmoneda, "+
                                " re.tipocambio, "+
                                " sum(re.pago) as 'SumaDolares', "+
                                " sum(re.pagobs) as 'SumaBolivianos' "+
                                " from  "+ 
                                " tb_recibopago re "+
                                " where "+  
                                " re.codcobranza = "+codigoCobro+
                                " group by re.codcobranza ";
            return ConecRes.consultaMySql(consulta);
        }

        internal string get_detalleMttoCobro(int codigoCobro)
        {
            string consulta = "select  re.codcobranza, "+
                               " re.banco,  re.codmoneda, "+ 
                               " re.tipocambio,  re.pago,  "+
                               " re.pagobs, "+  
                               " m.nombre as mes, "+
                               " seg.years "+ 
                               " from   tb_recibopago re, tb_mes m, tb_seguimiento seg "+
                               " where   re.codmes = m.codigo and  re.codseg = seg.codigo "+
                               " and  re.codcobranza = "+codigoCobro;
           DataSet detalleFilas = ConecRes.consultaMySql(consulta);
           string detalle = "";
            if(detalleFilas.Tables[0].Rows.Count > 0){
                detalle = "(";
                for (int i = 0; i < detalleFilas.Tables[0].Rows.Count; i++)
                {
                    detalle = detalle + detalleFilas.Tables[0].Rows[i][6].ToString() + "-" + detalleFilas.Tables[0].Rows[i][7].ToString() + ",";
                }                
                detalle = detalle + ")";
            }else
                detalle = "Ninguno";
            return detalle;
        }

        internal DataSet tuplas_TodosRecibosdelCobroRealizado(int codigoCobro)
        {
            string consulta = "select "+
                                " re.codigo as 'codRecibo', "+
                                " re.detalle, "+
                                " re.fecha, "+
                                " re.hora, "+
                                " re.codseg, "+
                                " re.codmes, "+
                                " re.codresp, "+
                                " re.efectivo, "+
                                " re.deposito, "+
                                " re.nrocheque, "+
                                " re.banco, "+
                                " re.recibo, "+
                                " re.factura, "+
                                " re.codcobranza, "+
                                " re.codmoneda, "+
                                " re.tipocambio, "+
                                " re.pago, "+
                                " re.pagobs  "+
                                " from  "+
                                " tb_recibopago re "+
                                " where "+
                                " re.codcobranza = "+codigoCobro;
            return ConecRes.consultaMySql(consulta);
           }

        //--------------------por cliente Simect 

        /// <summary>
        /// este procedimiento actualiza todos los cobros con el Docum en una fecha determinada
        /// siempre y cuando no haya sido vaciado antes
        /// </summary>
        /// <param name="fechaDeterminada"> fecha en la cual se van actuazlizar todos los registros que no hayan sido vaciados a simec</param>
        /// <param name="DOCUM">la variable de simec que van a compartir en un solo registro</param>
        /// <returns></returns>
        internal bool updateTodosDOCUM_porFecha2(string fechaDeterminada, string DOCUM,int CodMonedaCliente)
        {
            string consulta = " update tb_cobranza_recibio, tb_equipo " +
                               " set tb_cobranza_recibio.docum = '" + DOCUM + "', tb_cobranza_recibio.vaciarsimec = true " +
                               " where " +
                               " (tb_cobranza_recibio.vaciarsimec = false or tb_cobranza_recibio.vaciarsimec is null) and " +
                               " tb_cobranza_recibio.clicodigo = tb_equipo.clicodigo and " +
                               " tb_equipo.monedaprevision_simec = "+CodMonedaCliente+" and " +
                               " tb_cobranza_recibio.fecha = " + fechaDeterminada;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal bool get_hayCobranzadeCliente_conLaMonedaDeterminada(string fechaDeterminada, int CodMonedaCliente)
        {
            string consulta = "select co.* from tb_cobranza_recibio co, tb_equipo eq " +
                               " where " +
                               " co.clicodigo = eq.clicodigo and " +
                               " (co.vaciarsimec = false or co.vaciarsimec is null) and " +
                               " eq.monedaprevision_simec = "+CodMonedaCliente+" and " +
                               " co.fecha = " + fechaDeterminada;
            DataSet filas = ConecRes.consultaMySql(consulta);
             if (filas.Tables[0].Rows.Count > 0)
             {
                 return true;
             }
             else
                 return false;

        }


        internal bool anularCobroReciboGeneral(int CodigoCobroRecibo, bool anulado)
        {
            string consulta = "update tb_cobranza_recibio set "+
                               " tb_cobranza_recibio.vaciarsimec = "+anulado+", "+
                               " tb_cobranza_recibio.anulada = "+anulado+
                               " where  "+
                               " tb_cobranza_recibio.codigo = "+CodigoCobroRecibo;
            return ConecRes.ejecutarMySql(consulta);
        }


        internal DataSet getAllEncuestaMantenimientoRealizas()
        {
            string consulta = "select "+
                               " pp.nombre as 'Edificio', "+
                               " pp.direccion, "+
                               " date_format(en.fecha,'%d/%m/%Y') as 'fecha', "+
                               " en.hora, "+
                               " en.cumplimientofechasplanificadasmantenimiento, "+
                               " en.funcionamientodelosequipos, "+
                               " en.rapidezdelasreparaciones, "+
                               " en.resolucionefectivadelacausadereparacion, "+
                               " en.asesoramientoyrapidezenlaentregadecotizacionesinformes, "+
                               " en.tiempoderespuestaanteunaemergencia, "+
                               " en.resolucionefectivadelasemergencias, "+
                               " en.cordialidadyatenciondelpersonaldecobranza, "+
                               " en.tratoyatenciondelpersonalasministrativo, "+
                               " en.cordialidadyatenciondelpersonaltecnico, "+
                               " en.tratoyatenciondelpersonaldeingenieria, "+
                               " en.tratoatencionyrespuestadelpersonaldecallcenter, "+
                               " en.sugerenciademejora "+
                               " , res.nombre as 'ResponsableEncuesta'"+
                               " from  "+
                               " tb_encuestamantenimiento en "+
                               " LEFT JOIN tb_proyecto pp ON en.codproyecto = pp.codigo "+
                               " LEFT JOIN tb_responsable res ON en.codresp = res.codigo ";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet getBoletaSolaNro(string nroBoleta)
        {
            string consulta = "SELECT "+
                               " vv.codigo, "+
                               " vv.boleta, "+
                               " pp.nombre as 'edificio', "+
                               " DATE_FORMAT(vv.fechaboleta,'%d/%m/%Y') as 'fechaboleta', "+
                               " eq.exbo, "+
                               " IF(vv.eq_ascensorelectrico=TRUE,'X','') , "+
                               " IF(vv.eq_ascensorhidraulico=TRUE,'X','') , "+
                               " IF(vv.eq_escaleramecanica=TRUE,'X','') , "+
                               " IF(vv.eq_plataforma=TRUE,'X','') , "+
                               " IF(vv.eq_montacoches=TRUE,'X','') , "+
                               " IF(vv.eq_minicarga=TRUE,'X','') , "+
                               " IF(vv.ee_ff=TRUE,'X','') , "+
                               " IF(vv.ee_fp=TRUE,'X','') , "+
                               " IF(vv.ee_pf=TRUE,'X','') , "+
                               " IF(vv.ee_pp=TRUE,'X','') , "+
                               " IF(vv.ee_pec=TRUE,'X','') , "+
                               " IF(vv.sc_motor=TRUE,'X','') , "+
                               " IF(vv.sc_poleas=TRUE,'X','') , "+
                               " IF(vv.sc_aceitemotor=TRUE,'X','') , "+
                               " IF(vv.sc_cabletraccion=TRUE,'X','') , "+
                               " IF(vv.sc_ventilador=TRUE,'X','') , "+
                               " IF(vv.sc_freno=TRUE,'X','') , "+
                               " IF(vv.sc_bobina=TRUE,'X','') , "+
                               " IF(vv.sc_lvelocidad=TRUE,'X','') , "+
                               " IF(vv.sc_reduccionjuego=TRUE,'X','') , "+
                               " IF(vv.sc_cpu=TRUE,'X','') , "+
                               " IF(vv.sc_tarjetas=TRUE,'X','') , "+
                               " IF(vv.sc_conectores=TRUE,'X','') , "+
                               " IF(vv.sc_auxiliares=TRUE,'X','') , "+
                               " IF(vv.sc_aelectrica=TRUE,'X','') , "+
                               " IF(vv.sc_reguladordevelocidad=TRUE,'X','') , "+
                               " IF(vv.sc_unidadhidraulica=TRUE,'X','') , "+
                               " IF(vv.sc_valvulahidraulica=TRUE,'X','') , "+
                               " IF(vv.sc_cadenaprincipal=TRUE,'X','') , "+
                               " IF(vv.sc_sistemalubricacion=TRUE,'X','') , "+
                               " IF(vv.sc_contyseriedeseguridad=TRUE,'X','') , "+
                               " IF(vv.sc_accesos=TRUE,'X','') , "+
                               " IF(vv.sc_limpieza=TRUE,'X','') , "+
                               " IF(vv.c_botonera=TRUE,'X','') , "+
                               " IF(vv.c_indicadores=TRUE,'X','') , "+
                               " IF(vv.c_iluminacion=TRUE,'X','') , "+
                               " IF(vv.c_puertacabina=TRUE,'X','') , "+
                               " IF(vv.c_ajusteenviaje=TRUE,'X','') , "+
                               " IF(vv.c_ventilador=TRUE,'X','') , "+
                               " IF(vv.c_barrerafotoelec=TRUE,'X','') , "+
                               " IF(vv.c_holguradecab=TRUE,'X','') , "+
                               " IF(vv.c_guias=TRUE,'X','')  , "+
                               " IF(vv.c_vidrioespejopaneles=TRUE,'X','') , "+
                               " IF(vv.c_operadordepuertas=TRUE,'X','') , "+
                               " IF(vv.c_contyseriedeseguridad=TRUE,'X','') , "+
                               " IF(vv.c_pasamanos=TRUE,'X','')  , "+
                               " IF(vv.c_limpieza=TRUE,'X','')  , "+
                               " IF(vv.a_botonera=TRUE,'X','') , "+
                               " IF(vv.a_indicadores=TRUE,'X','') , "+
                               " IF(vv.a_puerta=TRUE,'X','') , "+
                               " IF(vv.a_guiapatines=TRUE,'X','') , "+
                               " IF(vv.a_cerrojos=TRUE,'X','') , "+
                               " IF(vv.a_padeenclavamiento=TRUE,'X','') , "+
                               " IF(vv.a_sensores=TRUE,'X','') , "+
                               " IF(vv.a_peines=TRUE,'X','') , "+
                               " IF(vv.a_peldanosfaldon=TRUE,'X','') , "+
                               " IF(vv.a_demarcaciones=TRUE,'X','') , "+
                               " IF(vv.a_botondeemergencia=TRUE,'X','') , "+
                               " IF(vv.a_contyseriedeseguridad=TRUE,'X','') , "+
                               " IF(vv.a_senales=TRUE,'X','') , "+
                               " IF(vv.a_limpieza=TRUE,'X','') , "+
                               " IF(vv.f_cablesdetraccion=TRUE,'X','') , "+
                               " IF(vv.f_cablelimitador=TRUE,'X','') , "+
                               " IF(vv.f_cableviajero=TRUE,'X','') , "+
                               " IF(vv.f_contrapeso=TRUE,'X','') , "+
                               " IF(vv.f_fdecarrerasuperior=TRUE,'X','') , "+
                               " IF(vv.f_fdecarrerainferior=TRUE,'X','') , "+
                               " IF(vv.f_paracaidas=TRUE,'X','') , "+
                               " IF(vv.f_topespistones=TRUE,'X','') , "+
                               " IF(vv.f_poleatensora=TRUE,'X','') , "+
                               " IF(vv.f_poleas=TRUE,'X','') , "+
                               " IF(vv.f_rieles=TRUE,'X','') , "+
                               " IF(vv.f_aceiteras=TRUE,'X','') , "+
                               " IF(vv.f_stopdefosa=TRUE,'X','') , "+
                               " IF(vv.f_resortes=TRUE,'X','') , "+
                               " IF(vv.f_tensiondecadena=TRUE,'X','') , "+
                               " IF(vv.f_contyseriedeseguridad=TRUE,'X','') , "+
                               " IF(vv.f_mordazas=TRUE,'X','') , "+
                               " IF(vv.f_limpieza=TRUE,'X','') , "+
                               " vv.materialesyrepuesto, "+
                               " vv.observacion, "+
                               " IF(vv.i_fusiblecontactos=TRUE,'X','') , "+
                               " IF(vv.i_botoneradepisoencorte=TRUE,'X','') , "+
                               " IF(vv.i_limites=TRUE,'X','') , "+
                               " IF(vv.i_reguladordevelocidad=TRUE,'X','') , "+
                               " IF(vv.i_frenobalataselectroiman=TRUE,'X','') , "+
                               " IF(vv.i_motordetraccion=TRUE,'X','') , "+
                               " IF(vv.i_poleas=TRUE,'X','') , "+
                               " IF(vv.i_filtraciondeaguaensalademaquinas=TRUE,'X','') , "+
                               " IF(vv.i_accesoirregularasalademaquinas=TRUE,'X','') , "+
                               " IF(vv.i_corteensenalizadoropulsadordepiso=TRUE,'X','') , "+
                               " IF(vv.i_ruidooajusteenpuertaspisocabina=TRUE,'X','') , "+
                               " IF(vv.i_iluminaciondecabina=TRUE,'X','') , "+
                               " IF(vv.i_operadordepuertas=TRUE,'X','') , "+
                               " IF(vv.i_motordeoperador=TRUE,'X','') , "+
                               " IF(vv.i_ventiladordecabina=TRUE,'X','') , "+
                               " IF(vv.i_cerrojo=TRUE,'X','') , "+
                               " IF(vv.i_sensordecabinabarrerafotocelula=TRUE,'X','') , "+
                               " IF(vv.i_filtraciondeaguaenhuecoyfoso=TRUE,'X','') , "+
                               " IF(vv.i_bajasocortedetension=TRUE,'X','') , "+
                               " IF(vv.i_sensores=TRUE,'X','') , "+
                               " IF(vv.i_malusoporusuario=TRUE,'X','') , "+
                               " IF(vv.i_iluminacionirregularensalademaquinasyfoso=TRUE,'X','') , "+
                               " IF(vv.i_otros=TRUE,'X','') , "+
                               " res.nombre as 'tecnico', "+
                               " res.codigo, "+
                               " vv.horallegada, "+
                               " vv.horasalida, "+
                               " vv.recepcion, "+
                               " vv.receptor_ci, "+
                               " vv.receptor_cargo "+
                               " ,vv.tipoboleta "+
                               " from tb_visitadetallerutaequipo vv, tb_equipo eq,tb_proyecto pp, tb_responsable res "+
                               " WHERE "+
                               " vv.codequipo = eq.codigo and "+
                               " eq.cod_proyecto = pp.codigo and "+
                               " vv.codtecnico = res.codigo and "+
                               " vv.boleta ='"+nroBoleta+"'";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet getBoletaEmergencia(string nroBoleta)
        {
            string consulta = "select "+
                               " bb.codigo, "+
                               " bb.boleta, "+
                               " date_format(bb.fechagra,'%d/%m/%Y') as 'Fecha1', "+
                               " even.nombreEdificio as 'Edificio', "+
                               " even.telefono, "+
                               " bb.ascensor, "+
                               " bb.estadoboleta, "+
                               " bb.tipoboleta, "+
                               " IF(bb.i_fusiblecontactos=TRUE,'X','') , " +
                               " IF(bb.i_botoneradepisoencorte=TRUE,'X','') , " +
                               " IF(bb.i_limites=TRUE,'X','') , " +
                               " IF(bb.i_reguladordevelocidad=TRUE,'X','') , " +
                               " IF(bb.i_frenobalataselectroiman=TRUE,'X','') , " +
                               " IF(bb.i_motordetraccion=TRUE,'X','') , " +
                               " IF(bb.i_poleas=TRUE,'X','') , " +
                               " IF(bb.i_filtraciondeaguaensalademaquinas=TRUE,'X','') , " +
                               " IF(bb.i_accesoirregularasalademaquinas=TRUE,'X','') , " +
                               " IF(bb.i_corteensenalizadoropulsadordepiso=TRUE,'X','') , " +
                               " IF(bb.i_ruidooajusteenpuertaspisocabina=TRUE,'X','') , " +
                               " IF(bb.i_iluminaciondecabina=TRUE,'X','') , " +
                               " IF(bb.i_operadordepuertas=TRUE,'X','') , " +
                               " IF(bb.i_motordeoperador=TRUE,'X','') , " +
                               " IF(bb.i_ventiladordecabina=TRUE,'X','') , " +
                               " IF(bb.i_cerrojo=TRUE,'X','') , " +
                               " IF(bb.i_sensordecabinabarrerafotocelula=TRUE,'X','') , " +
                               " IF(bb.i_filtraciondeaguaenhuecoyfoso=TRUE,'X','') , " +
                               " IF(bb.i_bajasocortedetension=TRUE,'X','') , " +
                               " IF(bb.i_sensores=TRUE,'X','') , " +
                               " IF(bb.i_malusoporusuario=TRUE,'X','') , " +
                               " IF(bb.i_iluminacionirregularensalademaquinasyfoso=TRUE,'X','') , " +
                               " IF(bb.i_otros=TRUE,'X','') , " +
                               " bb.observacioncierre, "+
                               " res.nombre as 'tecnico', "+
                               " res.codigo, "+
                               " deven.hora_llegadaEdificio, "+
                               " deven.hora_salidaEdificio, "+
                               " bb.recepcion, "+
                               " bb.receptor_ci, "+
                               " bb.receptor_cargo "+
                               " from tb_boletaemergenciacallcenter bb, tb_detalle_teceven deven, "+
                               " tb_evento even, tb_responsable res " +
                               " where "+
                               " deven.codeven = even.codigo and "+
                               " deven.codigo = bb.cod_detalleEventoCallcenter and "+
                               " bb.boleta like '%"+nroBoleta+"%'"; 
            return ConecRes.consultaMySql(consulta);
        }

        public DataSet getBoletasMantenimientoPreventivo(string edificio, string fechadesde, string fechahasta) {
            string consulta = "select " +
                               " vv.codigo, " +
                               " pp.nombre as 'Edificio', " +
                               " vv.boleta, " +
                               " vv.tipoboleta, " +
                               " date_format(vv.fechaboleta,'%d/%m/%Y') as 'fecha1', " +
                               " vv.horallegada, " +
                               " res.nombre as 'Tecnico' " +
                               " ,vv.costotransporte "+
                               " ,vv.observacion "+
                               " from  " +
                               " tb_equipo eq, tb_proyecto pp, " +
                               " tb_visitadetallerutaequipo vv " +
                               " LEFT JOIN tb_responsable res ON (vv.codtecnico = res.codigo) " +
                               " where  " +
                               " vv.codequipo = eq.codigo and " +
                               " eq.cod_proyecto = pp.codigo and " +
                               " vv.tipoboleta = 'Mantenimiento Preventivo' and " +
                               " pp.nombre like '%" + edificio + "%' and " +
                               " vv.fecha between " + fechadesde + " and " + fechahasta+
                               " order by res.nombre ASC, TIMESTAMP(vv.fechaboleta,vv.horallegada) ASC";
            return ConecRes.consultaMySql(consulta);
        }

        public DataSet getBoletasMantenimientoEmergenciayOtros(string edificio, string fechadesde, string fechahasta)
        {
            string consulta = "select " +
                               " vv.codigo, " +
                               " pp.nombre as 'Edificio', " +
                               " vv.boleta, " +
                               " vv.tipoboleta, " +
                               " date_format(vv.fechaboleta,'%d/%m/%Y') as 'fecha1', " +
                               " vv.horallegada, " +
                               " res.nombre as 'Tecnico' " +
                               " ,vv.costotransporte " +
                               " ,vv.observacion " +
                               " from  " +
                               " tb_equipo eq, tb_proyecto pp, " +
                               " tb_visitadetallerutaequipo vv " +
                               " LEFT JOIN tb_responsable res ON (vv.codtecnico = res.codigo) " +
                               " where  " +
                               " vv.codequipo = eq.codigo and " +
                               " eq.cod_proyecto = pp.codigo and " +
                               " vv.tipoboleta <> 'Mantenimiento Preventivo' and " +
                               " pp.nombre like '%" + edificio + "%' and " +
                               " vv.fecha between " + fechadesde + " and " + fechahasta+
                               " order by res.nombre ASC, TIMESTAMP(vv.fechaboleta,vv.horallegada) ASC";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet getmontoAdeudado_montoReferencia(int codigoEquipo)
        {
            string consulta = "SELECT T1.cod_equipo, SUM(T1.Deuda), T1.monto_pagar "+
                               " FROM "+
                               " (select "+ 
                               " seg.cod_equipo, "+
                               " sum(dseg.monto_pagar - dseg.monto_pago) as 'Deuda', "+
                               " monto_pagar "+
                               " from "+
                               " tb_seguimiento seg, tb_detalle_segme dseg "+
                               " where "+
                               " seg.codigo = dseg.codSeg and "+
                               " (dseg.monto_pagar - dseg.monto_pago) > 0 and "+
                               " seg.years = year(current_date()) and "+
                               " dseg.codMe < month(current_date()) and "+
                               " seg.cod_equipo = '"+codigoEquipo+"' "+
                               " union all "+
                               " select "+ 
                               " seg.cod_equipo, "+
                               " sum(dseg.monto_pagar - dseg.monto_pago) as 'Deuda', "+
                               " monto_pagar "+
                               " from "+
                               " tb_seguimiento seg, tb_detalle_segme dseg "+
                               " where "+
                               " seg.codigo = dseg.codSeg and "+
                               " (dseg.monto_pagar - dseg.monto_pago) > 0 and "+
                               " seg.years < year(current_date()) and "+
                               " seg.cod_equipo = '"+codigoEquipo+"') AS T1 "+
                               " GROUP BY T1.cod_equipo";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_montoReferencia(int codigoEquipo)
        {
            string consulta = "select  "+
                                " seg.cod_equipo, "+
                                " monto_pagar  "+
                                " from  "+
                                " tb_seguimiento seg, tb_detalle_segme dseg  "+
                                " where  "+
                                " seg.codigo = dseg.codSeg and  "+
                                " seg.years = year(current_date()) and  "+
                                " dseg.codMe = month(current_date()) and "+
                                " seg.cod_equipo = '"+codigoEquipo+"' ";
            return ConecRes.consultaMySql(consulta);
        }


        internal DataSet buscar_R148(bool anulado, bool vaciadoSimec, string proyecto)
        {
            string consulta = "SELECT " +
                               " r148.codigo, " +
                               " date_format(r148.fecha, '%d/%m/%Y') as 'fecha-R148', " +
                               " r148.hora, " +
                               " r148.proyecto, " +
                               " r148.cantEquipos, " +
                               " r148.ciudadventa, " +
                               " r148.ciudadinstalacion, " +
                               " r148.direccioninstalacion, " +
                               " r148.zona, " +
                               " r148.NroContrato, " +
                               " r148.ValorContrato, " +
                               " r148.moneda, " +
                               " date_format(r148.fechaContrato, '%d/%m/%Y') as 'fecha-Contrato', " +
                               " date_format(r148.fechaPedido, '%d/%m/%Y') as 'fecha-Pedigo', " +
                               " date_format(r148.fechaEmbarque, '%d/%m/%Y') as 'fecha-Embarque', " +
                               " r148.resp_une, " +
                               " r148.resp_nacional, " +
                               " r148.resp_gun, " +
                               " r148.resp_jpr, " +
                               " r148.resp_fpr, " +
                               " r148.resp_flo, " +
                               " r148.observaciones, " +
                               " r148.cod_varSimec "+ 
                               " FROM  " +
                               " tb_planpago_r148 r148 " +
                               " WHERE " +
                               " r148.anulado = " + anulado + " and " +
                               " r148.vaciarsimec = " + vaciadoSimec;
                               if(!proyecto.Equals("")){
                                 consulta = consulta +  " and r148.proyecto like '%" + proyecto + "%'";
                               }
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_R148(int codigoR148)
        {
            string consulta = "SELECT " +
                               " r148.codigo, " +
                               " date_format(r148.fecha, '%d/%m/%Y') as 'fecha-R148', " +
                               " r148.hora, " +
                               " r148.proyecto, " +
                               " r148.cantEquipos, " +
                               " r148.ciudadventa, " +
                               " r148.ciudadinstalacion, " +
                               " r148.direccioninstalacion, " +
                               " r148.zona, " +
                               " r148.NroContrato, " +
                               " r148.ValorContrato, " +
                               " r148.moneda, " +
                               " date_format(r148.fechaContrato, '%d/%m/%Y') as 'fecha-Contrato', " +
                               " date_format(r148.fechaPedido, '%d/%m/%Y') as 'fecha-Pedigo', " +
                               " date_format(r148.fechaEmbarque, '%d/%m/%Y') as 'fecha-Embarque', " +
                               " r148.resp_une, " +
                               " r148.resp_nacional, " +
                               " r148.resp_gun, " +
                               " r148.resp_jpr, " +
                               " r148.resp_fpr, " +
                               " r148.resp_flo, " +
                               " r148.observaciones, " +
                               " r148.cod_varSimec "+
                               " FROM  " +
                               " tb_planpago_r148 r148 " +
                               " WHERE " +
                               " r148.codigo ="+codigoR148;
            
            return ConecRes.consultaMySql(consulta);
        }

        internal bool anularR148(int CodigoR148, bool isChecked)
        {
            string consulta = "update tb_planpago_r148 set tb_planpago_r148.anulado = "+isChecked+
                              " where tb_planpago_r148.codigo = "+CodigoR148 ;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal DataSet get_cuotasPlanPagosR148(int CodigoPlanPago)
        {
            string consulta = "select "+
                               " cc.codigo, "+
                               " date_format(cc.fecha,'%d/%m/%Y') as 'fecha1', "+
                               " cc.hora, "+
                               " cc.descripcion, "+
                               " cc.monto, "+
                               " cc.porcentaje, "+
                               " cc.moneda, "+
                               " date_format(cc.`fechaPago`,'%d/%m/%Y') as 'fechaPago1', "+
                               " cc.cantdias, "+
                               " cc.cod_r148  "+ 
                               "  from "+
                               "  tb_cuota_r148 cc "+
                               "  where "+
                               "  cc.cod_r148 = "+CodigoPlanPago;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_exbosTotalEquiposR148(int CodigoPlanPago)
        {
            string consulta = "select  "+
                               "  CAST(group_concat(eq.exbo) AS CHAR) as 'Exbos',"+
                               "  count(*)  as 'CantEquipos', "+
                               "  CAST(group_concat(eq.parada) AS CHAR) AS 'Paradas' , "+
                               "  CAST(group_concat(eq.pasajero) AS CHAR) as 'Pasajeros' "+
                               " from tb_equipo eq "+
                               " where eq.cod_r148 = " + CodigoPlanPago;
            return ConecRes.consultaMySql(consulta);
        }

        internal bool marcarVaciadoR148(int CodigoR148)
        {
            string consulta = "update tb_planpago_r148 set tb_planpago_r148.vaciarsimec = true "+
                               " where tb_planpago_r148.codigo = "+CodigoR148;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal DataSet get_EquiposR148_2(int CodigoPlanPago)
        {
            string consulta = "select  " +
                               " eq.exbo, " +                               
                               " eq.parada, " +
                               " eq.pasajero " +
                               " from tb_equipo eq " +
                               " where eq.cod_r148 = " + CodigoPlanPago;
            return ConecRes.consultaMySql(consulta);
        }
    }
}