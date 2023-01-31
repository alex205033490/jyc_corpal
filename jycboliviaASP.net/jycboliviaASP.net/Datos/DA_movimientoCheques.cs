using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_movimientoCheques
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_movimientoCheques() { }


        public bool insertarMovimientoCheques(string fecha,string nrocheque,string monto,string detalle,bool transferencia,string destino,int codcuentabanco,int coduser,string titular) {

            string consulta = "insert into tb_movimientocheques "+
                                " (tb_movimientocheques.fecha,tb_movimientocheques.nrocheque,tb_movimientocheques.monto, "+
                                " tb_movimientocheques.detalle,tb_movimientocheques.transferencia,tb_movimientocheques.destino, "+
                                " tb_movimientocheques.codcuentabanco,tb_movimientocheques.coduser,tb_movimientocheques.titular) " +
                                " values("+fecha+",'"+nrocheque+"','"+monto+"','"+detalle+"',"+transferencia+",'"+destino+"',"+codcuentabanco+","+coduser+",'"+titular+"')";
            return ConecRes.ejecutarMySql(consulta);

        }

        public bool modificarmovimientoCheques(int codigoMovimiento,string fecha,string nrocheque,string monto,string detalle,bool transferencia,string destino,int codcuentabanco,int coduser, string titular) {
            string consulta = "update  tb_movimientocheques set " +
                               " tb_movimientocheques.fecha = "+fecha+",tb_movimientocheques.nrocheque = '"+nrocheque+"',tb_movimientocheques.monto = '"+monto+"', " +
                               " tb_movimientocheques.detalle = '"+detalle+"',tb_movimientocheques.transferencia = "+transferencia+",tb_movimientocheques.destino = '"+destino+"', " +
                               " tb_movimientocheques.codcuentabanco = "+codcuentabanco+",tb_movimientocheques.coduser =  " +coduser+" , "+
                               " tb_movimientocheques.titular = '" + titular + "' " +
                               " where tb_movimientocheques.codigo = "+codigoMovimiento;

            return ConecRes.ejecutarMySql(consulta);
        }

        public bool eliminarMovimientoCheques(int codigoMovi)
        {
            string consulta = "delete from tb_movimientocheques where tb_movimientocheques.codigo = " + codigoMovi;
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

    }
}