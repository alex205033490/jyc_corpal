using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using static jycboliviaASP.net.Negocio.NA_APIcompras;

namespace jycboliviaASP.net.Datos
{
    
    public class DCorpal_Venta
    {
        private conexionMySql cnx = new conexionMySql();
        public DCorpal_Venta() { }

        internal bool anularVendidoVaciadoUpon(int codigoVendido, bool bandera)
        {
            string consulta = " update tbcorpal_venta set  " +
                " tbcorpal_venta.vaciadoupon = "+bandera+"  " +
                " where  tbcorpal_venta.codigo = "+codigoVendido;
            return cnx.ejecutarMySql(consulta);
        }

        internal bool crearVenta(int codClient, string cliente, string correoCliente, string municipio, string telefono, string direccion, string numeroFactura, string nombreRazonSocial, string numeroDocumento, int codigoMetodoPago, decimal montoTotal, int codigoMoneda, decimal tipoCambio, decimal montoTotalMoneda, decimal descuentoAdicional, string leyendaF, int codresp, string responsable, bool factura, string fechaentrega, int codsolicitudentregaproducto)
        {
            string consulta = "insert into tbcorpal_venta( " +
                " fechagra,horagra,codigoCliente, cliente, correoCliente, " +
                " municipio,telefono,numeroFactura, direccion, " +
                " nombreRazonSocial, numeroDocumento,codigoMetodoPago, " +
                " montoTotal,codigoMoneda, " +
                " tipoCambio,montoTotalMoneda,descuentoAdicional, " +
                " leyendaF,codresp, " +
                " responsable,factura,fechaentrega,estadoventa,estado,cicliente,vaciadoupon,codsolicitudentregaproducto) " +
                " values( current_date(), current_time(), "+ codClient + ", '"+cliente+"', '"+correoCliente+"', " +
                "'"+municipio+"','"+telefono+"','"+numeroFactura+"', '"+direccion+"', " +
                "'"+nombreRazonSocial+"', '"+numeroDocumento+"',"+ codigoMetodoPago + "," +
                "'"+ montoTotal.ToString().Replace(',','.') + "',"+ codigoMoneda + ", " +
                "'"+tipoCambio.ToString().Replace(',', '.')+"','"+ montoTotalMoneda.ToString().Replace(',','.')+ "','"+ descuentoAdicional.ToString().Replace(',','.') + "', " +
                "'"+leyendaF+"',"+ codresp + "," +
                "'"+ responsable + "',"+factura+", "+fechaentrega+",'Abierto',1,'"+ numeroDocumento + "',false,"+ codsolicitudentregaproducto + ");";
            return cnx.ejecutarMySql(consulta);
        }

        internal DataSet get_allVentasParaVaciarUpon(string cliente)
        {
            string consulta = "select  vv.codigo, date_format(vv.fechaentrega,'%d/%m/%Y') as 'Fecha_Entrega', " +
                " vv.cliente,  vv.direccion,  vv.cicliente,  vv.telefono,  vv.razonSocialEmisor,  " +
                " vv.nitEmisor,  vv.correoCliente,  vv.montoTotal,  vv.tipoCambio  from tbcorpal_venta vv " +
                " where  vv.cliente like '%"+cliente+"%' and vv.vaciadoupon = false and vv.estado = 1 and vv.estadoventa = 'Abierto'  " +
                " order by vv.codigo desc ";
            return cnx.consultaMySql(consulta);
        }

        internal DataSet get_codigoVentaUltimoInsertado(string cliente, string nombreRazonSocial, string nombreResponsable)
        {
            string consulta = "select max(vv.codigo) " +
                " from tbcorpal_venta vv where " +
                " vv.cliente = '" + cliente + "' and " +
                " vv.nombreRazonSocial = '" + nombreRazonSocial + "' and " +
                " vv.responsable = '" + nombreResponsable + "'";
            return cnx.consultaMySql(consulta);
        }

        internal DataSet get_ItemVendidos(int codigoVendido)
        {
            string consulta = "select     " +
                " dv.codventa,  dv.codprod,  dv.descripcion,  dv.cantidad,   " +
                " dv.codmedida,  dv.medida,  dv.precioUnitario,  dv.montoDescuento,   " +
                " dv.precioTotal, pp.codupon, pp.codumupon   " +
                " from tbcorpal_detalleventasproducto dv , tbcorpal_producto pp where   " +
                " dv.codprod = pp.codigo and "+
                " dv.codventa = " +codigoVendido;
            return cnx.consultaMySql(consulta);
        }

        internal DataSet get_ventaRealizadaparaVaciar(int codigoVendido)
        {
            string consulta = "SELECT    " +
                " vv.codigo,   date_format(vv.fechagra,'%d/%m/%Y') as 'fecha_Gra',   " +
                " vv.horagra,  vv.codigoCliente,  vv.cliente,  vv.correoCliente,   " +
                " vv.nitEmisor,  vv.razonSocialEmisor,  vv.municipio,  vv.telefono,   " +
                " vv.numeroFactura,  vv.cuf,  vv.cufd,  vv.codigoSucursal,   " +
                " vv.direccion,  vv.codigoPuntoVenta,  vv.fechaEmision,   " +
                " vv.nombreRazonSocial,  vv.codigoTipoDocumentoIdentidad,   " +
                " vv.numeroDocumento,  vv.codigoMetodoPago,  vv.numeroTarjeta,   " +
                " vv.montoTotal,  vv.montoTotalSujetoIva,  vv.codigoMoneda,   " +
                " vv.tipoCambio,  vv.montoTotalMoneda,  vv.descuentoAdicional,   " +
                " vv.leyendaF,  vv.codresp,  vv.responsable,  vv.factura,   " +
                " vv.fechaentrega,  vv.codcliente,  vv.cicliente,   " +
                " vv.estado,  vv.codsolicitudentregaproducto,   " +
                " vv.estadoventa,  vv.vaciadoupon,  vv.codusercierre,   " +
                " vv.responsablecierre   " +
                " from tbcorpal_venta vv   " +
                " where vv.codigo = "+codigoVendido;
            return cnx.consultaMySql(consulta);
        }

        internal bool insertarTodoslosProductosAVenta(int codigoVenta, int codigoSolicitud)
        {
            string consulta = "insert into tbcorpal_detalleventasproducto(   " +
                " codventa, codprod,   descripcion, cantidad,   codmedida,  medida,   " +
                " precioUnitario, montoDescuento, precioTotal) "+
                "select   " +codigoVenta+", dp.codproducto, pp.producto, " +
                " ifnull(dp.cantentregada,0), null as 'codmedida', " +
                " pp.medida, pp.precio, 0 , ifnull((dp.cantentregada * pp.precio),0) " +
                " from tbcorpal_detalle_solicitudproducto dp , " +
                " tbcorpal_producto pp " +
                " where dp.codproducto = pp.codigo and dp.codsolicitud = "+codigoSolicitud;
            return cnx.ejecutarMySql(consulta);
        }
    }
}