using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_ReciboPago
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_ReciboPago() { }

        public bool insertar(string detalle, string fecha, string hora, float pago, int codSeguimiento, int codMes, int codresp)
        {

            string pago_aux = pago.ToString();
                pago_aux = pago_aux.Replace(',', '.');
                string consulta = "insert into tb_recibopago(detalle,fecha,hora,pago,codseg,codmes,codresp) " +
                                    " values('" + detalle + "','" + fecha + "','" + hora + "'," + pago_aux + "," + codSeguimiento + "," + codMes + "," + codresp + "); ";

                return ConecRes.ejecutarMySql(consulta);
        }
                

        public bool modificar(int codigoRecibo, int codSeguimiento, int codmes, int codresponsable, string detalle, bool efectivo, bool deposito, string nroCheque)
        {  
                     string consulta = "update tb_recibopago set tb_recibopago.detalle = '"+detalle+"' "+ " , tb_recibopago.efectivo = "+efectivo+" , tb_recibopago.deposito = "+deposito+" , tb_recibopago.nrocheque = '"+nroCheque +"' "+
                                        " where codigo = "+codigoRecibo+"  and codseg = "+codSeguimiento+"  and codmes = "+codmes+"  and codresp = "+ codresponsable;

                     return ConecRes.ejecutarMySql(consulta);
           
        }

        
        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        public bool insertarReciboMTTO(int codseg, int codmes, string detalle, double pago, bool efectivo, bool deposito, string nrocheque, string banco, string factura, string recibo, int codresp, int codcobranza, double tipoCambio, int codmoneda, bool transferencia, string fechaPago)
        {
            double monto_pagoSus = 0;
            double montoPagoBs = 0;
            if (codmoneda == 1)    // cuando el monto es en bolivianos
            {
                monto_pagoSus = Math.Round(((double)pago / (double)tipoCambio), 2);
                montoPagoBs = Math.Round((double)pago, 2);
            }
            else
                if (codmoneda == 2)
                {
                    monto_pagoSus = Math.Round((double)pago, 2);
                    montoPagoBs = Math.Round(((double)pago * (double)tipoCambio), 2);
                }

             

            string consulta = "insert into tb_recibopago( "+
                               " tb_recibopago.codseg, "+
                               " tb_recibopago.codmes, "+
                               " tb_recibopago.fecha, "+
                               " tb_recibopago.hora, "+
                               " tb_recibopago.detalle, "+
                               " tb_recibopago.pago, "+
                               " tb_recibopago.efectivo, "+
                               " tb_recibopago.deposito, "+
                               " tb_recibopago.nrocheque, "+
                               " tb_recibopago.banco, "+
                               " tb_recibopago.factura, "+
                               " tb_recibopago.recibo, "+
                               " tb_recibopago.codresp, "+
                               " tb_recibopago.codcobranza, "+
                               " tb_recibopago.tipoCambio, " +
                               " tb_recibopago.codmoneda, " +
                               " tb_recibopago.pagobs, "+
                               " tb_recibopago.transferencia) "+
                               " values( "+
                               codseg +", "+
                                codmes+", "+
                               fechaPago + " , " +
                               " current_time(), "+
                               "'"+ detalle+"', "+
                               "'" + monto_pagoSus.ToString().Replace(',', '.') + "', " +
                                efectivo+", "+
                                deposito+", "+
                               "'"+nrocheque+"', "+
                               "'"+ banco+"', "+
                               "'"+ factura+"', "+
                               "'"+ recibo+"', "+
                                codresp+", "+
                               codcobranza+","+
                               "'"+ tipoCambio.ToString().Replace(',','.')+"', " +
                               +codmoneda+", " +
                              "'" + montoPagoBs.ToString().Replace(',', '.') + "'," +
                               transferencia+
                               ")";
            return ConecRes.ejecutarMySql(consulta);

        }


        internal DataSet mostrarReciboPagosSinBancos(string edificio, string exbo)
        {
            string consulta = "select " +
                               " re.codigo, " +
                               " eq.exbo, " +
                               " pp.nombre as 'Edificio', " +
                               " mm.nombre as 'mes', " +
                               " seg.years, " +
                               " date_format(re.fecha ,'%d/%m/%Y') as 'Fecha1', " +
                               " re.hora, " +
                               " IF(re.efectivo=1,'Si','No') as 'Efectivo1', " +
                               " IF(re.deposito=1,'Si','No') as 'Deposito1', " +
                               " IF(re.transferencia=1,'Si','No') as 'Transferencia1', " +
                               " re.nrocheque, " +
                               " re.factura, " +
                               " re.recibo, " +
                               " re.banco, " +
                               " re.tipocambio, " +
                               " re.pago, " +
                               " re.pagobs, " +
                               " IF(re.codmoneda = 1,'Bolivianos','Dolares') as  'Moneda' " +
                               " from tb_recibopago re, tb_mes mm , tb_responsable res, tb_seguimiento seg, " +
                               " tb_equipo eq , tb_proyecto pp " +
                               " where  " +
                               " re.codmes = mm.codigo and " +
                               " re.codresp = res.codigo and " +
                               " re.codseg = seg.codigo and " +
                               " seg.cod_equipo = eq.codigo and " +
                               " eq.cod_proyecto = pp.codigo and " +
                               " year(re.fecha) > 2019 and " +
                               " re.banco =''  "+
                               " and pp.nombre like '%"+edificio+"%' "+                              
                               " and eq.exbo like '%"+exbo+"%'";	                            
                               
            return ConecRes.consultaMySql(consulta);
        }

        internal bool updateBancosRecibo(int codigoRecibo, string banco)
        {
            string consulta = "update tb_recibopago set "+
                               " tb_recibopago.banco = '"+banco+"' "+
                               " where "+
                               " tb_recibopago.codigo = '"+codigoRecibo+"'";
            return ConecRes.ejecutarMySql(consulta);
        }
    }
}