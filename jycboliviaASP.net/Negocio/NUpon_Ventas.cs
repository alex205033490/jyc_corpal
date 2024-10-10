using jycboliviaASP.net.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    public class NUpon_Ventas
    {
        DUpon_Ventas dupon = new DUpon_Ventas();
        public NUpon_Ventas() { }

        public bool insertar()
        {
            return dupon.insertar();
        }

        public bool modificar()
        {
            return dupon.modificar();
        }

        public bool eliminar()
        {
            return dupon.eliminar();
        }

        public bool truncarTabla()
        {
            return dupon.truncarTabla();
        }

        public bool insertarVentasRealizadas(string Archivo)
        {
            return dupon.insertarVentasRealizadas(Archivo);
        }

        public DataSet mostrarVentasRealizadas(string glosa)
        {
            string consulta = "select " +
                "  codigo,  NumeroVenta,  NumeroPedido,   Fecha,  CodigoCliente,  Referencia,  Glosa,    EmitirFactura,    ImporteProductos,  ImporteDescuentos,  ImporteTotal,  Cobros_TotalEfectivo,  Cobros_TotalDeposito,  Factura_TipoDocumentoIdentidad,  Factura_NIT_CI,  Factura_Complemento,  Factura_RazonSocial,  Factura_Telefono,  Factura_Email,  Factura_MetodoPago,  DetProd_NumeroItem,   DetProd_CodigoProducto,  DetProd_Cantidad,  DetProd_CodigoUnidadMedida,  DetProd_PrecioUnitario,  DetProd_ImporteDescuento,  DetProd_ImporteTotal,  DetProd_NumeroItemOrigen,  Usuario,  estado,  vaciadoupon" +
                " from tbupon_ventas pp where pp.glosa like '%" + glosa + "%' and pp.estado = 1 and pp.vaciadoupon = false";
            DataSet resultado = dupon.getDatos(consulta);
            return resultado;
        }



        public DataSet mostrarBusqueda(string dprod_NumeroItem)
        {
            string consulta = "select " +
                "  codigo,  NumeroVenta,  NumeroPedido,   Fecha,  CodigoCliente,  Referencia,  Glosa,    EmitirFactura,    ImporteProductos,  ImporteDescuentos,  ImporteTotal,  Cobros_TotalEfectivo,  Cobros_TotalDeposito,  Factura_TipoDocumentoIdentidad,  Factura_NIT_CI,  Factura_Complemento,  Factura_RazonSocial,  Factura_Telefono,  Factura_Email,  Factura_MetodoPago,  DetProd_NumeroItem,   DetProd_CodigoProducto,  DetProd_Cantidad,  DetProd_CodigoUnidadMedida,  DetProd_PrecioUnitario,  DetProd_ImporteDescuento,  DetProd_ImporteTotal,  DetProd_NumeroItemOrigen,  Usuario,  estado,  vaciadoupon" +
                " from tbupon_ventas ex where ex.dprod_NumeroItem like '" + dprod_NumeroItem + "'";
            DataSet resultado = dupon.getDatos(consulta);
            return resultado;
        }

        internal bool eliminarVaciadoVentas(int codigoEliminar)
        {
            return dupon.eliminarVaciadoVentas(codigoEliminar);
        }

        internal DataSet GetDatoParaVaciar(int codigoVaciar)
        {
            DataSet datos = dupon.GetDatoParaVaciar(codigoVaciar);
            return datos;
        }

        internal bool updateVaciadoOk(int codigoVaciar)
        {
            return dupon.updateVaciadoOk(codigoVaciar);
        }
    }
}