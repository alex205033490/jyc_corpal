using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_movimientoCheques
    {
        DA_movimientoCheques Dmovi = new DA_movimientoCheques();

        public NA_movimientoCheques() {         
        }

        public bool insertarMovimientoCheques(string fecha,string nrocheque, string monto, string detalle, bool transferencia, string destino, int codcuentabanco, int coduser, string titular) {
          return  Dmovi.insertarMovimientoCheques( fecha,nrocheque,  monto,  detalle,  transferencia,  destino,  codcuentabanco,  coduser,titular);
        }

        public bool eliminarMovimientoCheques(int codigoMovi)
        {
            return Dmovi.eliminarMovimientoCheques(codigoMovi);
        }

        public DataSet mostrarMovimientosCheques(string nroCuenta) {
            string consulta = "select movi.codigo,date_format(movi.fecha,'%d/%m/%Y') as Fecha, "+
                               " movi.nrocheque,movi.monto,movi.detalle as 'Detalle_movimiento_bancario', "+
                               " movi.transferencia, movi.destino, "+
                               " cu.nrocuenta,ba.nombre as 'Banco', cu.moneda, ti.nombre as 'TipoCuenta', "+
                               " movi.titular as 'TitularCheque' "+
                               " from tb_movimientocheques movi, tb_cuentabancaria cu, tb_banco ba, tb_tipocuenta ti "+
                               " where "+
                               " movi.codcuentabanco = cu.codigo and "+
                               " cu.codbanco = ba.codigo and "+
                               " cu.codtipocuenta = ti.codigo and "+
                               " cu.nrocuenta like '%"+nroCuenta+"%' order by movi.codigo desc";
            return Dmovi.getDatos(consulta);
        }

        public bool modificarmovimientoCheques(int codigoMovimiento, string fecha, string nrocheque, string monto, string detalle, bool transferencia, string destino, int codcuentabanco, int coduser, string titular) { 
            return Dmovi.modificarmovimientoCheques( codigoMovimiento,  fecha,  nrocheque,  monto,  detalle,  transferencia,  destino,  codcuentabanco,  coduser, titular);
        }
    }
}