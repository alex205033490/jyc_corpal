using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Datos
{
    public class DUpon_compras
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DUpon_compras() { }

        public bool insertar()
        {

            try
            {
                string consulta = "";
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar()
        {
            return false;
        }

        public bool truncarTabla()
        {
            try
            {
                string consulta = "truncate table tbupon_compras";
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        public bool insertarComprasRealizadas(string Archivo)
        {
            try
            {
                string consulta = "LOAD DATA LOCAL INFILE '" + Archivo + "' " +
                                  "INTO TABLE tbupon_compras " +
                                  "FIELDS TERMINATED BY ';' " +
                                  "LINES TERMINATED BY '\n' " +                                  
                                  "IGNORE 1 LINES "+
                                  "(NumeroCompra,Fecha,Referencia,ImporteProductos,ImporteDescuento,ImporteTotal,CodigoMoneda,CodigoProveedor,CodigoDistribucionGastos,pagos_TotalEfectivo,pagos_TotalCredito,pagos_TotalCheques,pagos_TotalDeposito,FacturaPosterior,factura_NIT_CI,factura_RazonSocial,factura_NumeroFactura,factura_CodigoAutorizacion,factura_CodigoControl,factura_ImporteTotal,factura_ImporteDescuento,factura_ImporteGift,factura_ImporteNeto,factura_AplicaCredictoFiscal,Glosa,dprod_NumeroItem,dprod_CodigoProducto,dprod_Cantidad,dprod_CodigoUnidadMedida,dprod_PrecioUnitario,dprod_ImporteDescuento,dprod_PorcentajeGasto,dprod_ImporteTotal,Usuario,estado,vaciadoupon);";
                return ConecRes.ejecutarMySql(consulta);                
            }
            catch (Exception)
            {

                return false;
            }

        }

        internal bool eliminarVaciadoCompras(int codigoEliminar)
        {
            string consulta = "update tbupon_compras set " +
                              " tbupon_compras.estado = 0, " +
                              " tbupon_compras.vaciadoupon = 0 " +
                              " where tbupon_compras.codigo =" + codigoEliminar;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal DataSet GetDatoParaVaciar(int codigoVaciar)
        {
            string consulta = "select codigo,NumeroCompra,Fecha,Referencia,ImporteProductos,ImporteDescuento,ImporteTotal," +
                " CodigoMoneda,CodigoProveedor,CodigoDistribucionGastos,pagos_TotalEfectivo,pagos_TotalCredito," +
                " pagos_TotalCheques,pagos_TotalDeposito,FacturaPosterior,factura_NIT_CI,factura_RazonSocial," +
                " factura_NumeroFactura,factura_CodigoAutorizacion,factura_CodigoControl,factura_ImporteTotal," +
                " factura_ImporteDescuento,factura_ImporteGift,factura_ImporteNeto,factura_AplicaCredictoFiscal," +
                " Glosa,dprod_NumeroItem,dprod_CodigoProducto,dprod_Cantidad,dprod_CodigoUnidadMedida," +
                " dprod_PrecioUnitario,dprod_ImporteDescuento,dprod_PorcentajeGasto,dprod_ImporteTotal," +
                " Usuario,estado,vaciadoupon" +
                " from tbupon_compras uu where uu.codigo = "+codigoVaciar;
            return ConecRes.consultaMySql(consulta);
        }

        internal bool updateVaciadoOk(int codigoVaciar)
        {
            string consulta = "update tbupon_compras set " +                               
                               " tbupon_compras.vaciadoupon = 1 " +
                               " where tbupon_compras.codigo =" + codigoVaciar;
            return ConecRes.ejecutarMySql(consulta);
        }

    }
}