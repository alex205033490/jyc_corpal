using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_DetalleSeguimiento
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_DetalleSeguimiento() { }

        public bool insertar(int codSeguimiento, int codMes, float montoPagar, float montoPago, string Pago, int codUser)
        {
                string montoPagar_aux = montoPagar.ToString();
                montoPagar_aux = montoPagar_aux.Replace(',','.');

                string montoPago_aux = montoPago.ToString();
                montoPago_aux = montoPago_aux.Replace(',', '.');

                string consulta = "insert into tb_detalle_segme(codSeg,codme,monto_pagar,monto_pago,pago,estado,coduser) values("+codSeguimiento+","+codMes+",'"+montoPagar_aux+"','"+montoPago_aux+"','"+Pago+"',1,"+codUser+");";
                return ConecRes.ejecutarMySql(consulta);

        }

        public bool modificar(int codSeguimiento, int codMes, float montoPagar)
        {
            string montoPagar_aux = montoPagar.ToString();
            montoPagar_aux = montoPagar_aux.Replace(',', '.');
            string consulta = "update tb_detalle_segme set tb_detalle_segme.monto_pagar = '"+montoPagar_aux+"' where tb_detalle_segme.codSeg = "+codSeguimiento+" and tb_detalle_segme.codMe = "+ codMes;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool eliminar()
        {
            return false;
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        

        public bool modificarMotoPagar(int codSegui, int codmes, float montoPagar) { 
            
                string montoPagar_aux = montoPagar.ToString();
                montoPagar_aux = montoPagar_aux.Replace(',', '.');
                string consulta = "update tb_detalle_segme set monto_pagar = '"+montoPagar_aux+"' where codseg = "+codSegui+" and codMe = "+codmes;
               return ConecRes.ejecutarMySql(consulta);
               
            
        }


        public bool modificarMotoPago(int codSegui, int codmes, float montoPago,string pago,int coduser)
        {            
                string montoPago_aux = montoPago.ToString();
                montoPago_aux = montoPago_aux.Replace(',', '.');
                string consulta = "update tb_detalle_segme set monto_pago = '"+montoPago_aux+"', pago = '"+pago+"', coduser="+coduser+" where codseg = " + codSegui + " and codMe = " + codmes;
                return  ConecRes.ejecutarMySql(consulta);        
         }

        internal bool update_recibodeCobranza(int codSeg, int codMe, double monto_pago, double monto_pago_bs, double tipocambio, string pago,  int coduser)
        {
            string consulta = "update tb_detalle_segme set " +                               
                               " tb_detalle_segme.monto_pago = '" + monto_pago.ToString().Replace(',', '.') + "', " +
                               " tb_detalle_segme.monto_pago_bs = '" + monto_pago_bs.ToString().Replace(',', '.') + "', " +
                               " tb_detalle_segme.tipocambio = '" + tipocambio.ToString().Replace(',', '.') + "', " +
                               " tb_detalle_segme.pago = '" + pago + "', " +
                               " tb_detalle_segme.estado = 1, " +
                               " tb_detalle_segme.coduser = " + coduser+
                               " where "+
                               " tb_detalle_segme.codSeg = " + codSeg + " and " +
                               " tb_detalle_segme.codMe = " + codMe ;
            return ConecRes.ejecutarMySql(consulta);
        }

    }
}