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
                            " codigo,  date_format(fechagra,'%d/%m/%Y') as 'Fecha_Gra', " +
                            " horagra,  cliente, " +
                            " cast(monto as Decimal(10,2)) as 'monto', moneda,  chequenro,  concepto,  detalle, responsable,nrorecibo, " +
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
                                " cast(monto as Decimal(10,2)) as 'monto', moneda,  chequenro, banco, efectivo, " +
                                " concepto,  detalle, responsable, porcentajeretencioniue, " +
                                " porcentajeretencionit,retencioniuebs,retencionitbs, cast(totalapagar as Decimal(10,2)) as 'totalapagar' ," +
                                " nrorecibo, responsable as 'realizadopor', "+
                                " date_format(rr.fechaegreso , '%d/%m/%Y') as 'Fecha_Recibo' " +
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

        internal bool insertconciliacionBancaria(string saldoAnterior, string extractoBancario, int codCuentaBanco, int coduser)
        {
            string consulta = "insert into tb_conciliacionbancaria " +
                              " (fecha,saldoAnterior,extractobancario,codcuentabanco,coduser) " +
                              " values(now(),'" + saldoAnterior.ToString() + "','" + extractoBancario.ToString() + "'," + codCuentaBanco + "," + coduser + ")";
            return ConecRes.ejecutarMySql(consulta);
        }

        internal bool eliminarIngresobancarizacion(string fecha, int cuentaBanco, int coduser, float montoRestar)
        {
            string consulta = "select codigo,extractobancario "+
                               " from tb_conciliacionbancaria "+ 
                               " where  tb_conciliacionbancaria.fecha = "+fecha+
                               " and  tb_conciliacionbancaria. codcuentabanco = "+cuentaBanco+
                               " and  tb_conciliacionbancaria. coduser =  "+coduser;
            DataSet dato = ConecRes.consultaMySql(consulta);

            if (dato.Tables[0].Rows.Count > 0)
            {
                float montoActual;
                float.TryParse(dato.Tables[0].Rows[0][1].ToString().Replace('.', ','), out montoActual);
                float montoResultado;
                if (montoRestar <= montoActual)
                {
                    montoResultado = montoActual - montoRestar;
                    string consultaR = "update tb_conciliacionbancaria set "+ 
                                        " tb_conciliacionbancaria.extractobancario = format('"+montoResultado+"',2) "+ 
                                        " where  tb_conciliacionbancaria.fecha = current_date() "+
                                        " and  tb_conciliacionbancaria. codcuentabanco = "+cuentaBanco+ 
                                        " and  tb_conciliacionbancaria. coduser =  "+coduser;
                    return ConecRes.ejecutarMySql(consultaR);
                }
                else
                    return false;

            }
            else
                return false;
            
        }

        internal DataSet get_allrecibosIngresoVsEgreso(string fechadesde, string fechahasta, string responsable)
        {
            string consulta = "SELECT "+
                            " t1.codigo, "+
                            " t1.Tipo, "+
                            " date_format(t1.Fecha_Gra,'%d/%m/%Y') as 'Fecha_Gra', " +
                            " t1.horagra, "+ 
                            " t1.ClienteIngreso, "+
                            " t1.pagadoha_Egreso, "+
                            " cast(t1.MontoIngreso as decimal(10,2)) as 'MontoIngreso', "+ 
                            " cast(t1.MontoEgreso as decimal(10,2)) as 'MontoEgreso', "+                           
                            " t1.moneda, "+ 
                            " t1.chequenro, "+  
                            " t1.concepto, "+ 
                            " t1.detalle, "+
                            " res.nombre as 'responsable', "+
                            " t1.nrorecibo, "+
                            " date_format(t1.Fecha_Recibo,'%d/%m/%Y') as 'Fecha_Recibo', " +
                            " t1.banco, "+
                            " t1.efectivo, "+ 
                            " t1.porcentajeretencioniue, "+
                            " t1.porcentajeretencionit, "+
                            " t1.retencioniuebs, "+
                            " t1.retencionitbs, "+
                            " t1.totalapagar "+
                            " FROM "+
                            " ( "+
                            " select "+ 
                            " ri.codigo, "+
                            " 'Ingreso' as 'Tipo', "+ 
                            " ri.fechagra as 'Fecha_Gra', "+
                            " ri.horagra, "+ 
                            " ri.cliente as 'ClienteIngreso', "+
                            " '' as 'pagadoha_Egreso', "+
                            " ri.monto as 'MontoIngreso', "+ 
                            " '0' as 'MontoEgreso', "+
                            " ri.moneda, "+ 
                            " ri.chequenro, "+  
                            " ri.concepto, "+ 
                            " ri.detalle, "+
                            " ri.codrespgra, "+
                            " ri.nrorecibo, "+
                            " ri.fecharecibo as 'Fecha_Recibo', "+
                            " '' as 'banco', "+
                            " '' as 'efectivo', "+  
                            " '' as 'porcentajeretencioniue', "+
                            " '' as 'porcentajeretencionit', "+
                            " '' as 'retencioniuebs', "+
                            " '' as 'retencionitbs', "+
                            " '' as 'totalapagar'  "+
                            " from tbcorpal_reciboingreso ri  "+
                            " where "+
                            " ri.estadoingreso = true "+
                            " and ri.fecharecibo between "+fechadesde+" and  "+ fechahasta+
                            " UNION all "+
                            " select "+
                            " re.codigo, "+ 
                            " 'Egreso' as 'Egreso', "+
                            " re.fechagra as 'Fecha_Gra', "+
                            " re.horagra, "+ 
                            " '' as 'ClienteIngreso', "+
                            " re.pagadoha as 'pagadoha_Egreso', "+
                            " '0' as 'MontoIngreso', "+
                            " re.monto as 'MontoEgreso', "+ 
                            " re.moneda, "+ 
                            " re.chequenro, "+ 
                            " re.concepto, "+
                            " re.detalle, "+
                            " re.codrespgra, "+
                            " re.nrorecibo, "+
                            " re.fechaegreso as 'Fecha_Recibo', "+
                            " re.banco, "+
                            " re.efectivo, "+  
                            " re.porcentajeretencioniue, "+
                            " re.porcentajeretencionit, "+
                            " re.retencioniuebs, "+
                            " re.retencionitbs, "+
                            " re.totalapagar "+
                            " from tbcorpal_reciboegreso re "+
                            " where "+
                            " re.estadoegreso = true "+
                            " and re.fechaegreso between "+fechadesde+" and "+fechahasta+
                            " ) AS t1, tb_responsable res "+
                            " where "+
                            " t1.codrespgra = res.codigo and "+
                            " res.nombre = '"+responsable+"' "+
                            " order by t1.Fecha_Recibo asc";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_SaldoInicial_IngresoEgreso(string fechadesde)
        {
            string consulta = "select "+
                                " format(sum(ri.monto),2) as 'ingreso', "+
                                " format(sum(re.monto),2) as 'egreso', "+
                                " (sum(ri.monto)-sum(re.monto)) as 'SALDO' "+
                                " from tbcorpal_reciboingreso ri , tbcorpal_reciboegreso re "+
                                " where "+
                                " ri.fecharecibo = re.fechaegreso and "+
                                " ri.estadoingreso = 1 and "+
                                " re.estadoegreso = 1 and "+
                                " ri.fecharecibo < "+fechadesde;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_SaldosInicialesResponsable(string fechadesde, string responsable)
        {
          /*  string consulta = " SELECT "+
                               " t1.codrespgra, "+
                               " res.nombre as 'responsable', "+
                               " SUM(t1.ingreso) AS 'Total_ingreso', "+
                               " SUM(t1.egreso) AS 'Total_egreso', "+
                               " SUM(t1.ingreso) - SUM(t1.egreso) AS 'SALDO' "+
                               " FROM ( "+
                               " SELECT "+
                               " ri.codrespgra, "+
                               " SUM(ri.monto) AS ingreso, "+
                               " '0' AS egreso "+ 
                               " FROM tbcorpal_reciboingreso ri "+
                               " WHERE ri.estadoingreso = 1  and "+
                               " fecharecibo < "+fechadesde+
                               " GROUP BY ri.codrespgra "+
                               " UNION ALL "+
                               " SELECT "+
                               " re.codrespgra, "+
                               " '0' AS ingreso, "+
                               " SUM(re.monto) AS egreso "+    
                               " FROM tbcorpal_reciboegreso re "+
                               " WHERE re.estadoegreso = 1 AND "+
                               " re.fechaegreso < "+fechadesde+
                               " GROUP BY re.codrespgra "+
                               " ) AS t1, tb_responsable res "+
                               " where "+
                               " t1.codrespgra = res.codigo and "+      
                               " res.nombre like '%"+responsable+"%' "+
                               " GROUP BY t1.codrespgra"; */
            string consulta = "SELECT "+
                                " t1.codrespgra, "+
                                " t1.nombre as 'responsable', "+
                                " ifnull(t2.Total_ingreso,0) as 'Total_ingreso', "+ 
                                " ifnull(t2.Total_egreso,0) as 'Total_egreso', "+
                                " ifnull(t2.SALDO,0) as 'SALDO' "+
                                "  FROM  "+
                                " ( "+
                                " SELECT "+
                                " ri.codrespgra, "+
                                " res.nombre "+
                                " FROM tbcorpal_reciboingreso ri, tb_responsable res "+
                                " WHERE ri.estadoingreso = 1  and "+
                                " ri.codrespgra = res.codigo "+
                                " GROUP BY ri.codrespgra "+ 
                                " UNION ALL "+ 
                                " SELECT "+
                                " re.codrespgra, "+
                                " resp.nombre "+
                                "  FROM tbcorpal_reciboegreso re, tb_responsable resp "+
                                "  WHERE re.estadoegreso = 1 and "+
                                "  re.codrespgra = resp.codigo "+
                                "  GROUP BY re.codrespgra) AS t1 "+
                                "  LEFT JOIN "+
                                "  ( "+
                                "  SELECT "+
                                " tt.codrespgra, "+
                                " SUM(tt.ingreso) AS 'Total_ingreso', "+
                                " SUM(tt.egreso) AS 'Total_egreso', "+
                                " SUM(tt.ingreso) - SUM(tt.egreso) AS 'SALDO' "+
                                " FROM ( "+
                                " SELECT "+
                                " ri.codrespgra, "+
                                " SUM(ri.monto) AS ingreso, "+
                                " '0' AS egreso "+
                                " FROM tbcorpal_reciboingreso ri "+
                                " WHERE ri.estadoingreso = 1  and "+
                                " fecharecibo < "+fechadesde+
                                " GROUP BY ri.codrespgra "+
                                " UNION ALL "+
                                " SELECT "+
                                " re.codrespgra, "+
                                " '0' AS ingreso, "+
                                " SUM(re.monto) AS egreso "+    
                                " FROM tbcorpal_reciboegreso re "+
                                " WHERE re.estadoegreso = 1 AND "+
                                " re.fechaegreso < "+fechadesde+
                                " GROUP BY re.codrespgra "+
                                " ) AS tt "+
                                " GROUP BY tt.codrespgra "+
                                " ) AS t2   ON t1.codrespgra = t2.codrespgra "+
                                " WHERE "+
                                " t1.nombre like '%"+responsable+"%' "+
                                " GROUP BY t1.codrespgra";

            return ConecRes.consultaMySql(consulta);
        }
    }
}