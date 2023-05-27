using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_Recibo_IngresoEgreso
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_Recibo_IngresoEgreso() { }

        public bool insertarReciboIngreso(string cliente, float monto, string moneda, string chequenro, string concepto, string detalle, int codrespgra, string responsable, string facturanro, string nrorecibo, string fecharecibo)
        {
            string consulta = "insert into tbcorpal_reciboingreso ( " +
                             " fechagra,  horagra,  cliente,  monto, "+
                             " moneda,  chequenro,  concepto,  detalle, "+
                             " fechamod,  horamod,  codrespgra,  responsable,tipo,estadoingreso,facturanro,nrorecibo,fecharecibo " + 
                              "  ) values( "+
                             " current_date,  current_time,  '" + cliente + "',  '" + monto.ToString().Replace(',', '.') + "', " +
                             " '"+moneda+"',  '"+chequenro+"',  '"+concepto+"',  '"+detalle+"', "+
                             " current_date,  current_time,  " + codrespgra + ",  '" + responsable + "','Ingreso',1,'" + facturanro + "', '" + nrorecibo + "' ," + fecharecibo + ")";
                return ConecRes.ejecutarMySql(consulta);
        }

        public bool insertarReciboEgreso(string pagadoha, float monto, string moneda, string chequenro, string concepto, string detalle, int codrespgra, string responsable, string banco, bool efectivo, float porcentajeretencioniue, float porcentajeretencionit, float retencioniuebs, float retencionitbs, float totalapagar, string facturanro, string nrorecibo, string fechaegreso)
        {
            string consulta = "insert into tbcorpal_reciboegreso ( " +
                             " fechagra,  horagra,  pagadoha,  monto, " +
                             " moneda,  chequenro,  concepto,  detalle, " +
                             " fechamod,  horamod,  codrespgra,  responsable, tipo, estadoegreso, banco, efectivo,porcentajeretencioniue,porcentajeretencionit,retencioniuebs,retencionitbs,totalapagar, facturanro, nrorecibo, fechaegreso " +
                              "  ) values( " +
                             " current_date,  current_time,  '" + pagadoha + "',  '" + monto.ToString().Replace(',', '.') + "', " +
                             " '" + moneda + "',  '" + chequenro + "',  '" + concepto + "',  '" + detalle + "', " +
                             " current_date,  current_time,  "+codrespgra+",  '" + responsable + "','Egreso',1 , '" + banco + "', "+efectivo+", "+
                             " '" + porcentajeretencioniue.ToString().Replace(',', '.') + "','" + porcentajeretencionit.ToString().Replace(',', '.') + "','" + retencioniuebs.ToString().Replace(',', '.') + "','" + retencionitbs.ToString().Replace(',', '.') + "','" + totalapagar.ToString().Replace(',', '.') + "', '" + facturanro + "', '" + nrorecibo + "', " + fechaegreso + " )";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool modificarReciboIngreso(int codigo, string cliente, float monto, string moneda, string chequenro, string concepto, string detalle, int codrespgra, string responsable, string facturanro, string fecharecibo)
        {
            string consulta = "update tbcorpal_reciboingreso set " +
                                     " tbcorpal_reciboingreso.cliente = '" + cliente + "', " +
                                     " tbcorpal_reciboingreso.monto = '" + monto.ToString().Replace(',', '.') + "', " +
                                     " tbcorpal_reciboingreso.moneda = '" + moneda + "', " +
                                     " tbcorpal_reciboingreso.chequenro = '" + chequenro + "', " +
                                     " tbcorpal_reciboingreso.concepto = '" + concepto + "', " +
                                     " tbcorpal_reciboingreso.detalle = '" + detalle + "', " +
                                     " tbcorpal_reciboingreso.fechamod = current_date, " +
                                     " tbcorpal_reciboingreso.horamod = current_time, " +
                                     " tbcorpal_reciboingreso.codrespgra = " + codrespgra + ", " +
                                     " tbcorpal_reciboingreso.responsable = '" + responsable + "', " +
                                     " tbcorpal_reciboingreso.facturanro='" + facturanro + "',  " +
                                     " tbcorpal_reciboingreso.fecharecibo=" + fecharecibo + 
                                     " where "+
                                     " tbcorpal_reciboingreso.codigo = " + codigo;

                     return ConecRes.ejecutarMySql(consulta);           
        }

        public bool modificarReciboEgreso(int codigo, string pagadoha, float monto, string moneda, string chequenro, string concepto, string detalle, int codrespgra, string responsable, string banco, bool efectivo, float porcentajeretencioniue, float porcentajeretencionit, float retencioniuebs, float retencionitbs, float totalapagar, string facturanro, string fechaegreso)
        {
            string consulta = "update tbcorpal_reciboegreso set "+
                                " tbcorpal_reciboegreso.fechamod = current_date(), "+ 
                                " tbcorpal_reciboegreso.horamod = current_time(), "+ 
                                " tbcorpal_reciboegreso.pagadoha = '"+pagadoha+"', "+ 
                                " tbcorpal_reciboegreso.monto = '"+monto.ToString().Replace(',','.')+"', "+
                                " tbcorpal_reciboegreso.moneda = '"+moneda+"', "+  
                                " tbcorpal_reciboegreso.chequenro = '"+chequenro+"', "+ 
                                " tbcorpal_reciboegreso.concepto = '"+concepto+"', "+ 
                                " tbcorpal_reciboegreso.detalle = '"+detalle+"', "+
                                " tbcorpal_reciboegreso.codrespgra = "+codrespgra+", "+ 
                                " tbcorpal_reciboegreso.responsable = '"+responsable+"', "+                                
                                " tbcorpal_reciboegreso.banco = '"+banco+"', "+
                                " tbcorpal_reciboegreso.efectivo = "+efectivo+", "+
                                " tbcorpal_reciboegreso.porcentajeretencioniue = '" + porcentajeretencioniue.ToString().Replace(',', '.') + "', " +
                                " tbcorpal_reciboegreso.porcentajeretencionit = '" + porcentajeretencionit.ToString().Replace(',', '.') + "', " +
                                " tbcorpal_reciboegreso.retencioniuebs = '" + retencioniuebs.ToString().Replace(',', '.') + "', " +
                                " tbcorpal_reciboegreso.retencionitbs = '" + retencionitbs.ToString().Replace(',', '.') + "', " +
                                " tbcorpal_reciboegreso.totalapagar = '" + totalapagar.ToString().Replace(',', '.') + "', " +
                                " tbcorpal_reciboegreso.facturanro = '" + facturanro + "', " +
                                " tbcorpal_reciboegreso.fechaegreso = " + fechaegreso +
                                " where "+
                                " tbcorpal_reciboegreso.codigo = "+codigo;

            return ConecRes.ejecutarMySql(consulta);
        }

        public bool eliminarIngreso(int codigo)
        {
            string consulta = "update tbcorpal_reciboingreso set " +
                            " tbcorpal_reciboingreso.estadoingreso = false " +
                            " where "+
                            " tbcorpal_reciboingreso.codigo = " + codigo;
            return ConecRes.ejecutarMySql(consulta);

        }

        public bool eliminarEgreso(int codigo)
        {
            string consulta = "update tbcorpal_reciboegreso set " +
                            " tbcorpal_reciboegreso.estadoegreso = false " +
                            " where " +
                            " tbcorpal_reciboegreso.codigo = " + codigo;
            return ConecRes.ejecutarMySql(consulta);

        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        
        internal DataSet mostrarReciboIngreso(string cliente, int codUser)
        {
            string consulta = " select  " +
                            " codigo,  date_format(fechagra,'%d/%m/%Y') as 'Fecha_Gra', horagra,  cliente, " +
                            " monto, moneda,  chequenro,  concepto,  detalle, responsable, facturanro, nrorecibo, "+
                            " date_format(fecharecibo,'%d/%m/%Y') as 'Fecha Recibo' " +
                            " from tbcorpal_reciboingreso rr " +
                            " where "+
                            " rr.estadoingreso = true and "+                                                       
                            " rr.cliente like '%"+cliente+"%' "+
                            " and rr.codrespgra = " + codUser;            
                             
            return ConecRes.consultaMySql(consulta);
        }


        internal DataSet mostrarReciboEgreso(string pagadoha, int codUser)
        {
            string consulta = "select "+ 
                                " codigo,  date_format(fechagra,'%d/%m/%Y') as 'Fecha_Gra', horagra,  pagadoha, "+ 
                                " monto, moneda,  chequenro, banco, efectivo, "+
                                " concepto,  detalle, responsable,porcentajeretencioniue,porcentajeretencionit,retencioniuebs,retencionitbs,totalapagar,facturanro, nroRecibo, " + 
                                " date_format(fechaegreso,'%d/%m/%Y') as 'Fecha Egreso' "+
                                " from tbcorpal_reciboegreso rr "+
                                " where "+
                                " rr.estadoegreso = true and "+
                                " rr.pagadoha like '%" + pagadoha + "%'"+
                                " and rr.codrespgra = " + codUser; 
                                
            return ConecRes.consultaMySql(consulta);
        }


        internal DataSet getDatosReciboIngreso(int codigoReciboIngreso)
        {
            string consulta = " select  " +
                            " codigo,  date_format(fechagra,'%d/%m/%Y') as 'Fecha_Gra', horagra,  cliente, " +
                            " monto, moneda,  chequenro,  concepto,  detalle, responsable, facturanro, nrorecibo, fecharecibo " +
                            " from tbcorpal_reciboingreso rr " +
                            " where " +
                            " rr.estadoingreso = true and " +
                            " rr.codigo = "+codigoReciboIngreso;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet getDatosReciboEgreso(int codigoReciboEgreso)
        {
            string consulta = "select " +
                                " codigo,  date_format(fechagra,'%d/%m/%Y') as 'Fecha_Gra', horagra,  pagadoha, " +
                                " monto, moneda,  chequenro, banco, efectivo, " +
                                " concepto,  detalle, responsable, porcentajeretencioniue, " +
                                " porcentajeretencionit,retencioniuebs,retencionitbs,totalapagar,facturanro,nrorecibo, " +
                                " date_format(fechaegreso,'%d/%m/%Y') as 'Fecha Egreso' "+
                                " from tbcorpal_reciboegreso rr " +
                                " where " +
                                " rr.estadoegreso = true and " +
                                " rr.codigo = " + codigoReciboEgreso;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_codigoUltimoInsertado(string cliente)
        {
            string consulta = "select "+ 
                               " max(codigo) "+ 
                               " from tbcorpal_reciboingreso rr "+
                               " where "+
                               " rr.estadoingreso = true and "+
                               " rr.cliente = '"+cliente+"'";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_codigoUltimoInsertadoEgreso(string pagadoha)
        {
            string consulta = "select " +
                               " max(codigo) " +
                               " from tbcorpal_reciboegreso rr " +
                               " where " +
                               " rr.estadoegreso = true and " +
                               " rr.pagadoha = '" + pagadoha + "'";
                                

            return ConecRes.consultaMySql(consulta);
        }
        

        internal DataSet get_allreciboIngreso(string fecha1, string fecha2, string responsable)
        {
            string consulta = "select  " +
                            " codigo,  date_format(fechagra,'%d/%m/%Y') as 'Fecha_Gra', horagra,  cliente, " +
                            " monto, moneda,  chequenro,  concepto,  detalle, responsable,nrorecibo, " +
                            " date_format(rr.fecharecibo,'%d/%m/%Y') as 'Fecha_Recibo' "+
                            " from tbcorpal_reciboingreso rr " +
                            " where " +
                            " rr.estadoingreso = true " +
                            " and rr.responsable like '%"+responsable+"%' "+
                            " and rr.fecharecibo between " + fecha1 + " and " + fecha2;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_allreciboEgreso(string fecha1, string fecha2, string responsable)
        {
            string consulta = "select " +
                                " codigo,  date_format(fechagra,'%d/%m/%Y') as 'Fecha_Gra', horagra,  pagadoha, " +
                                " monto, moneda,  chequenro, banco, efectivo, " +
                                " concepto,  detalle, responsable, porcentajeretencioniue, " +
                                " porcentajeretencionit,retencioniuebs,retencionitbs,totalapagar," +
                                " nrorecibo, responsable as 'realizadopor', "+
                                " date_format(rr.fechaegreso ,'%d/%m/%Y') as 'Fecha_Recibo' "+
                                " from tbcorpal_reciboegreso rr " +
                                " where " +
                                " rr.estadoegreso = true " +
                                " and rr.responsable like '%" + responsable + "%' " +
                                " and rr.fechagra between " + fecha1 + " and " + fecha2;
            return ConecRes.consultaMySql(consulta);
        }


        internal DataSet get_nroRegistroIngresoSiguiente(int codUser)
        {
            string consulta = "select CONCAT( "+
                               " substring(res.nombre,1,1), "+
                               " substring(REVERSE(LEFT(REVERSE(res.nombre), locate(' ', REVERSE(res.nombre))-1 )),1,1),'_', "+
                               " count(ii.codigo) + 1 "+
                               " ) as 'NroRecibo' "+
                               " from tbcorpal_reciboingreso ii, tb_responsable res "+
                               " where "+ 
                               " ii.codrespgra = res.codigo and "+
                               " ii.codrespgra = "+codUser;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_nroRegistroEgresoSiguiente(int codUser)
        {
            string consulta = "select CONCAT( " +
                               " substring(res.nombre,1,1), " +
                               " substring(REVERSE(LEFT(REVERSE(res.nombre), locate(' ', REVERSE(res.nombre))-1 )),1,1),'_', " +
                               " count(ii.codigo) + 1 " +
                               " ) as 'NroRecibo' " +
                               " from tbcorpal_reciboegreso ii, tb_responsable res " +
                               " where " +
                               " ii.codrespgra = res.codigo and " +
                               " ii.codrespgra = " + codUser;
            return ConecRes.consultaMySql(consulta);
        }
    }
}