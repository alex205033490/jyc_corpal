using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_ReciboPago
    {

        DA_ReciboPago reciboPagoD = new DA_ReciboPago();

        public NA_ReciboPago() { }

        public bool insertar(string detalle, float pago, int codSeguimiento, int codMes, int codresp)
        {
            return reciboPagoD.insertar(detalle, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"),pago,codSeguimiento,codMes, codresp);          
        }

        public bool modificar(int codigoRecibo, int codSeguimiento, int codmes, int codresponsable, string detalle, bool efectivo, bool deposito, string nroCheque)
        {
            return reciboPagoD.modificar(codigoRecibo, codSeguimiento, codmes, codresponsable,  detalle, efectivo, deposito , nroCheque);
        }

        public bool eliminar()
        {
            return false;
        }

        public int ultimoinsertado()
        {
            try
            {
                string consulta = "SELECT MAX(recibo.codigo) FROM  tb_recibopago recibo";
                DataSet datoResul = reciboPagoD.getDatos(consulta);
                int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codUltimo;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public bool insertarReciboMTTO(int codseg, int codmes, string detalle, double pago, bool efectivo, bool deposito, string nrocheque, string banco, string factura, string recibo, int codresp, int codcobranza, double tipoCambio, int codmoneda, bool transferencia, string fechaPago)
        {
            return reciboPagoD.insertarReciboMTTO(codseg, codmes, detalle, pago, efectivo, deposito, nrocheque, banco, factura, recibo, codresp, codcobranza, tipoCambio, codmoneda, transferencia, fechaPago);
        }


        internal DataSet mostrarReciboPagosSinBancos(string edificio, string exbo)
        {
            return reciboPagoD.mostrarReciboPagosSinBancos(edificio, exbo);
        }

        internal bool updateBancosRecibo(int codigoRecibo, string banco)
        {
           return reciboPagoD.updateBancosRecibo(codigoRecibo,  banco);
        }
    }
}