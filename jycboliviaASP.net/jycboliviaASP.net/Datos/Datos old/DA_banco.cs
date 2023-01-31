using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{     
    public class DA_banco
    {
        private conexionMySql ConecRes = new conexionMySql();
      
        public DA_banco() { }

        public bool insertBanco(string nombre) 
        {
            string consulta = "insert into tb_banco(tb_banco.nombre,tb_banco.fecha) values('"+nombre+"', now())";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool insertarCuentaBancaria(string cuenta, string moneda, int banco, int tipoCuenta) {
            string consulta = "insert into tb_cuentabancaria(tb_cuentabancaria.fecha,tb_cuentabancaria.nrocuenta,tb_cuentabancaria.moneda,tb_cuentabancaria.codbanco, tb_cuentabancaria.codtipocuenta) "+
                               " values(now(),'" + cuenta + "','" + moneda + "'," + banco + "," + tipoCuenta + ")";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool insertconciliacionBancaria(string saldoAnterior, string extractoBancario, int codCuentaBanco,int coduser) {
            string consulta = "insert into tb_conciliacionbancaria "+
                               " (fecha,saldoAnterior,extractobancario,codcuentabanco,coduser) "+
                               " values(now(),'" +  saldoAnterior.ToString() + "','" + extractoBancario.ToString() + "'," + codCuentaBanco + "," + coduser + ")";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool insertChequeCirculacion(string fecha, string nrocheque, string monto, int codConciliacionBanco) {
            string consulta = "insert into tb_chequesencirculacion "+
                               " (fecha,nrocheque,monto,codconciliacionbanco) "+
                               " values (" + fecha + ",'" + nrocheque + "','" + monto + "'," + codConciliacionBanco + ")";
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CuentaBancaria"> es el numero de cuenta y banco</param>
        /// <returns> retorna la cuenta bancaria y el banco</returns>
        public DataSet getCuentaBancaria(string CuentaBancaria){
            string consulta = "select t1.codigo, t1.cuenta "+
                               " from  "+
                               " ( "+
                               " select cc.codigo, concat(cc.nrocuenta,' ',bb.nombre) as 'cuenta' "+
                               " from tb_cuentabancaria cc, tb_banco bb "+
                               " where "+
                               " cc.codbanco = bb.codigo "+
                               " ) as t1 "+
                               " where t1.cuenta like '%"+CuentaBancaria+"%'";
            return ConecRes.consultaMySql(consulta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuentaBancoAux">parametro de cuenta + banco</param>
        /// <returns>codigo especifico de lo solicitado</returns>
        internal DataSet get_CodigoCuentaBancaria(string cuentaBancoAux)
        {
            string consulta = "select t1.codigo, t1.cuenta, t1.col_subtitulosimec , t1.col_titulosimec, t1.banco " +
                                " from  " +
                                " ( " +
                                " select cc.codigo, concat(cc.nrocuenta,' ',bb.nombre) as 'cuenta' , cc.col_subtitulosimec, cc.col_titulosimec, bb.nombre as 'banco' " +
                                " from tb_cuentabancaria cc, tb_banco bb " +
                                " where " +
                                " cc.codbanco = bb.codigo " +
                                " ) as t1 " +
                                " where t1.cuenta = '" + cuentaBancoAux + "'";
            return ConecRes.consultaMySql(consulta);
        }
    }
}