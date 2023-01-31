using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Datos;

namespace jycboliviaASP.net.Negocio
{
    public class NA_DetalleSeguimiento
    {
        private DA_DetalleSeguimiento DdetalleSegui = new DA_DetalleSeguimiento();

        public NA_DetalleSeguimiento() { }

        public bool insertar(int codSeguimiento, int codMes, float montoPagar, float montoPago, string Pago, int coduser)
        {
                return DdetalleSegui.insertar(codSeguimiento,codMes,montoPagar,montoPago,Pago, coduser);
         }

        public bool modificar(int codSeguimiento, int codMes, float montoPagar)
        {
            return DdetalleSegui.modificar(codSeguimiento,codMes,montoPagar);
        }

        public bool eliminar()
        {
            return DdetalleSegui.eliminar();
        }

        public bool modificarMotoPagar(int codSegui, int codmes, float montoPagar) {
            return DdetalleSegui.modificarMotoPagar(codSegui, codmes, montoPagar);
        }



        public bool modificarMotoPago(int codSegui, int codmes, float montoPago, int coduser) {            
           DataSet tuplaSegui = get_seguimiento(codSegui,codmes);
           float montoPagar = Convert.ToSingle(tuplaSegui.Tables[0].Rows[0][2].ToString());
           float montoPagoOld = Convert.ToSingle(tuplaSegui.Tables[0].Rows[0][3].ToString());
           montoPago = montoPagoOld + montoPago;
           if (montoPagar == montoPago)
           {
               return DdetalleSegui.modificarMotoPago(codSegui, codmes, montoPago, "Si",coduser);
           }
           else { 
                if(montoPago < montoPagar){
                    return DdetalleSegui.modificarMotoPago(codSegui, codmes, montoPago,"No",coduser);
                }
           }
           return false;
        }

        public DataSet getAllRecibos(int codSegui, int codmes)
        {
            string consulta = " select r.codigo, date_format(r.fecha, '%d/%m/%Y') as fechaRecibo, r.hora, r.detalle, r.pago, r.efectivo, r.deposito, r.nrocheque "+
            " from tb_recibopago r where r.codseg = "+codSegui+" and r.codmes = "+codmes;
            DataSet datosR = DdetalleSegui.getDatos(consulta);
            return datosR;

        }


        public DataSet MostrarAllDatos(int codSeguimiento)
        {
            string consulta = "select mes.codigo, mes.nombre, segme.monto_pagar, segme.monto_pago, segme.pago "+                              
                              "from tb_detalle_segme segme, tb_mes mes "+
                              "where segme.codMe = mes.codigo and segme.codSeg="+codSeguimiento;
            DataSet datosR = DdetalleSegui.getDatos(consulta);
            return datosR;
        }

        public string getMaximoPagoSegMes(int codSeguimiento)
        {
            string consulta = "select max(segme.monto_pagar) " +
                                  "from tb_detalle_segme segme, tb_mes mes " +
                                  "where segme.codMe = mes.codigo and segme.codSeg=" + codSeguimiento;
            DataSet datosR = DdetalleSegui.getDatos(consulta);
            return datosR.Tables[0].Rows[0][0].ToString();
        
        }

        public DataSet get_seguimiento(int codSeg, int codMes) {
            string consulta = "select "+
                               " seg.codSeg, "+
                               " seg.codMe, "+
                               " seg.monto_pagar, "+
                               " seg.monto_pago, "+
                               " seg.pago, "+
                               " seg.estado, "+
                               " seg.coduser, "+
                               " seg.monto_pagar_bs, "+
                               " seg.monto_pago_bs, "+
                               " seg.tipocambio "+
                               " from tb_detalle_segme seg where seg.estado = 1 and seg.codSeg = "+codSeg+" and seg.codMe = "+codMes;
            DataSet datosR = DdetalleSegui.getDatos(consulta);
            return datosR;
        }

        public bool obtenerValorEfectivoDeposito(int codseguimiento, int codmes, int codigoRecibo, int numero)
        {
            int i = obtenerValor(codseguimiento, codmes,codigoRecibo, numero);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int obtenerValor(int codseguimiento, int codmes,int codigoRecibo,int numero)
        {
            string consulta = "";
            try
            {
                switch (numero)
                {
                    case 1:
                        consulta = "select  r.efectivo from tb_recibopago r where r.codseg = " + codseguimiento + " and r.codmes = " + codmes + " and r.codigo = " + codigoRecibo;
                        break;
                    case 2:
                        consulta = "select  r.deposito from tb_recibopago r where r.codseg = " + codseguimiento + " and r.codmes = " + codmes + " and r.codigo = " + codigoRecibo;
                        break;
                    
                }

                DataSet datoResul = DdetalleSegui.getDatos(consulta);
                int lista = Convert.ToInt32(datoResul.Tables[0].Rows[0][0]);
                return lista;
            }
            catch (Exception)
            {
                return -1;
            }

        }


        public bool modificarMotoPago_DolaresBolivianos(int codSeg, int codMe, double monto_pago, int tipoMoneda, double tipocambio, int coduser)
        {
            double monto_pagoSus = 0;
            if (tipoMoneda == 1)    // cuando el monto es en bolivianos
            {
                monto_pagoSus = Math.Round(((double)monto_pago / (double)tipocambio), 2);
            }
            else
                if (tipoMoneda == 2)
                    monto_pagoSus = Math.Round((double)monto_pago, 2);



            DataSet tuplaSegui = get_seguimiento(codSeg, codMe);
            double montoPagar = 0;
            double.TryParse(tuplaSegui.Tables[0].Rows[0][2].ToString(), out montoPagar);

            double montoPagoOld = 0;
            double.TryParse(tuplaSegui.Tables[0].Rows[0][3].ToString(), out montoPagoOld);

            double montoPagoOldBs = 0;
            double.TryParse(tuplaSegui.Tables[0].Rows[0][8].ToString(), out montoPagoOldBs);

            monto_pagoSus = (double)Math.Round((double)montoPagoOld + (double)monto_pagoSus, 2);
            montoPagar = (double) Math.Round((double)montoPagar,2);

            if (montoPagar == monto_pagoSus)
            {
                if (tipoMoneda == 1) //es bolivianos
                {
                    montoPagoOldBs = (double)Math.Round((double)montoPagoOldBs + ((double)monto_pagoSus * (double)tipocambio), 2);
                }else
                    montoPagoOldBs = (double)Math.Round((double)montoPagoOldBs + ((double)monto_pagoSus * (double)tipocambio), 2);
                
                return DdetalleSegui.update_recibodeCobranza(codSeg, codMe, monto_pagoSus, montoPagoOldBs, tipocambio, "Si", coduser);                 
            }
            else
            {
                if (monto_pagoSus < montoPagar)
                {
                    if (tipoMoneda == 1) //es bolivianos
                    {
                        montoPagoOldBs = (double)Math.Round((double)montoPagoOldBs + ((double)monto_pagoSus * (double)tipocambio), 2);
                    }else
                        montoPagoOldBs = (double)Math.Round((double)montoPagoOldBs + ((double)monto_pagoSus * (double)tipocambio), 2);

                    return DdetalleSegui.update_recibodeCobranza(codSeg, codMe, monto_pagoSus, montoPagoOldBs, tipocambio, "No", coduser);                 
                }else
                    return false;
            }
            
        }
       
    }
}