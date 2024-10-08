using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Datos
{
    public class DUpon_Ventas
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DUpon_Ventas() { }

        public bool insertar()
        {
                string consulta = "";
                return  ConecRes.ejecutarMySql(consulta);
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
                string consulta = "truncate table tbupon_ventas";
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

        public bool insertarVentasRealizadas(string Archivo)
        {
            try
            {
                string consulta = "LOAD DATA LOCAL INFILE '" + Archivo + "' " +
                                  "INTO TABLE tbupon_ventas " +
                                  "FIELDS TERMINATED BY ';' " +
                                  "LINES TERMINATED BY '\n' " +
                                  "IGNORE 1 LINES " +
                                  "( NumeroVenta,  NumeroPedido,   Fecha,  CodigoCliente,  Referencia,  Glosa,  EmitirFactura,    ImporteProductos,  ImporteDescuentos,  ImporteTotal,  Cobros_TotalEfectivo,  Cobros_TotalDeposito,  Factura_TipoDocumentoIdentidad,  Factura_NIT_CI,  Factura_Complemento,  Factura_RazonSocial,  Factura_Telefono,  Factura_Email,  Factura_MetodoPago,  DetProd_NumeroItem,   DetProd_CodigoProducto,  DetProd_Cantidad,  DetProd_CodigoUnidadMedida,  DetProd_PrecioUnitario,  DetProd_ImporteDescuento,  DetProd_ImporteTotal,  DetProd_NumeroItemOrigen,  Usuario,  estado,  vaciadoupon);";
                return ConecRes.ejecutarMySql(consulta);
            }
            catch (Exception)
            {

                return false;
            }

        }

        internal bool eliminarVaciadoVentas(int codigoEliminar)
        {
            string consulta = "update tbupon_ventas set " +
                              " tbupon_ventas.estado = 0, " +
                              " tbupon_ventas.vaciadoupon = 0 " +
                              " where tbupon_ventas.codigo =" + codigoEliminar;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal DataSet GetDatoParaVaciar(int codigoVaciar)
        {
            string consulta = "select   codigo,  NumeroVenta,  NumeroPedido,   Fecha,  CodigoCliente,  Referencia,  Glosa,    EmitirFactura,    ImporteProductos,  ImporteDescuentos,  ImporteTotal,  Cobros_TotalEfectivo,  Cobros_TotalDeposito,  Factura_TipoDocumentoIdentidad,  Factura_NIT_CI,  Factura_Complemento,  Factura_RazonSocial,  Factura_Telefono,  Factura_Email,  Factura_MetodoPago,  DetProd_NumeroItem,   DetProd_CodigoProducto,  DetProd_Cantidad,  DetProd_CodigoUnidadMedida,  DetProd_PrecioUnitario,  DetProd_ImporteDescuento,  DetProd_ImporteTotal,  DetProd_NumeroItemOrigen,  Usuario,  estado,  vaciadoupon" +
                " from tbupon_ventas uu where uu.codigo = " + codigoVaciar;
            return ConecRes.consultaMySql(consulta);
        }

        internal bool updateVaciadoOk(int codigoVaciar)
        {
            string consulta = "update tbupon_ventas set " +
                               " tbupon_ventas.vaciadoupon = 1 " +
                               " where tbupon_ventas.codigo =" + codigoVaciar;
            return ConecRes.ejecutarMySql(consulta);
        }

    }
}