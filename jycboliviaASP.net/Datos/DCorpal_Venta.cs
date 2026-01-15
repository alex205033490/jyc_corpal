using jycboliviaASP.net.Negocio;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure.Interception;
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

        internal bool crearVenta(int codClient, string cliente, string correoCliente, string municipio, string telefono, string direccion, string numeroFactura,
    string nombreRazonSocial, string numeroDocumento, int codigoMetodoPago, decimal montoTotal, int codigoMoneda, decimal tipoCambio, decimal montoTotalMoneda,
    decimal descuentoAdicional, string leyendaF, int codresp, string responsable, bool factura, string fechaentrega, int codsolicitudentregaproducto)
        {



            string consulta = "insert into tbcorpal_venta( " +
                " fechagra,horagra,codigoCliente, cliente, correoCliente, " +
                " municipio,telefono,numeroFactura, direccion, " +
                " nombreRazonSocial, numeroDocumento,codigoMetodoPago, " +
                " montoTotal,codigoMoneda, " +
                " tipoCambio,montoTotalMoneda,descuentoAdicional, " +
                " leyendaF,codresp, " +
                " responsable,factura,fechaentrega,estadoventa,estado,cicliente,vaciadoupon,codsolicitudentregaproducto) " +
                " values( current_date(), current_time(), " + codClient + ", '" + cliente + "', '" + correoCliente + "', " +
                "'" + municipio + "','" + telefono + "','" + numeroFactura + "', '" + direccion + "', " +
                "'" + nombreRazonSocial + "', '" + numeroDocumento + "'," + codigoMetodoPago + "," +
                "'" + montoTotal.ToString().Replace(',', '.') + "'," + codigoMoneda + ", " +
                "'" + tipoCambio.ToString().Replace(',', '.') + "','" + montoTotalMoneda.ToString().Replace(',', '.') + "','" + descuentoAdicional.ToString().Replace(',', '.') + "', " +
                "'" + leyendaF + "'," + codresp + "," +
                "'" + responsable + "'," + factura + ", " + fechaentrega + ",'Abierto',1,'" + numeroDocumento + "',false," + codsolicitudentregaproducto + ");";
            return cnx.ejecutarMySql(consulta);
        }
        
        ////////// VENTAS OPCIONAL uso adm
        
        internal bool crearVentas3(int codClient, string cliente, int codigoSolicitud, string correoCliente, string municipio, string telefono, string numeroFactura, string direccion, 
            string nombreRazonSocial, string numeroDocumento, int codigoMetodoPago, decimal montoTotal, int codigoMoneda, decimal tipocambio, decimal montoTotalMoneda, 
            decimal descuentoAdicional, string leyendaF, int codresp, string responsable, int factura, string fechaentrega, int codsolicitudentregaproducto)
        {
            try
            {
                string consulta = "INSERT INTO tbcorpal_venta (fechagra, horagra, codigoCliente, cliente, correoCliente, municipio, telefono, numeroFactura, direccion, " +
                    "nombreRazonSocial, numeroDocumento, codigoMetodoPago, montoTotal, codigoMoneda, tipoCambio, montoTotalMoneda, " +
                    "descuentoAdicional, leyendaF, codresp, responsable, factura, fechaentrega, codsolicitudentregaproducto, estadoventa, estado, vaciadoupon, cicliente) " +
                                  "SELECT current_date(), current_time(), @codClient, @cliente, @correoCliente, @municipio, @telefono, @numeroFactura, @direccion, " +
                                  "@nombreRazonSocial, @numeroDocumento, @codigoMetodoPago, @montoTotal, @codigoMoneda, @tipoCambio, @montoTotalMoneda, " +
                                  "@descuentoAdicional, @leyendaF, @codresp, @responsable, @factura, @fechaentrega, @codsolicitudentregaproducto, 'Abierto', 1, false, @numeroDocumento " +
                                  "FROM tbcorpal_solicitudentregaproducto sep " +
                                  "WHERE sep.codigo = @codigoSolicitud " +
                                  "AND sep.fechacierre IS NOT NULL " +
                                  "AND sep.horacierre IS NOT NULL " +
                                  "AND sep.estadosolicitud = 'Cerrado';";

                Console.WriteLine("CONSULTA GENERADA: " + consulta);

                // Usar parámetros en lugar de concatenación directa
                var parametros = new List<MySqlParameter>
        {
            new MySqlParameter("@codClient", codClient),
            new MySqlParameter("@cliente", cliente),
            new MySqlParameter("@correoCliente", correoCliente),
            new MySqlParameter("@municipio", municipio),
            new MySqlParameter("@telefono", telefono),
            new MySqlParameter("@numeroFactura", numeroFactura),
            new MySqlParameter("@direccion", direccion),

            new MySqlParameter("@nombreRazonSocial", nombreRazonSocial),
            new MySqlParameter("@numeroDocumento", numeroDocumento),
            new MySqlParameter("@codigoMetodoPago", codigoMetodoPago),
            new MySqlParameter("@montoTotal", montoTotal),
            new MySqlParameter("@codigoMoneda", codigoMoneda),
            new MySqlParameter("@tipoCambio", tipocambio),
            new MySqlParameter("@montoTotalMoneda", montoTotalMoneda),

            new MySqlParameter("@descuentoAdicional", descuentoAdicional),
            new MySqlParameter("@leyendaF", leyendaF),
            new MySqlParameter("@codresp", codresp),
            new MySqlParameter("@responsable", responsable),
            new MySqlParameter("@factura", factura),
            new MySqlParameter("@fechaentrega", fechaentrega),
            new MySqlParameter("@codsolicitudentregaproducto", codsolicitudentregaproducto),

            new MySqlParameter("@codigoSolicitud", codigoSolicitud)
        };
                return cnx.ejecutarMySql2arg(consulta, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar la venta: " + ex.Message);
                return false;
            }
        }

        internal DataSet get_allVentasParaVaciarUpon(string cliente)
        {
            string consulta = "select  vv.codigo, date_format(vv.fechaentrega,'%d/%m/%Y') as 'Fecha_Entrega', " +
                " vv.cliente,  vv.direccion,  vv.cicliente,  vv.telefono,  vv.razonSocialEmisor,  " +
                " vv.nitEmisor,  vv.correoCliente,  vv.montoTotal,  vv.tipoCambio  from tbcorpal_venta vv " +
                " where  vv.cliente like '%"+cliente+"%' and " +
                " vv.vaciadoupon = false and vv.estado = 1 and " +
                " vv.estadoventa = 'Cerrado'  " +
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
            string consulta = "select " +
                " dv.codventa,  dv.codprod,  dv.descripcion,  dv.cantidad,   " +
                " dv.codmedida,  dv.medida,  dv.precioUnitario,  dv.montoDescuento,   " +
                " dv.precioTotal, pp.codupon, pp.codumupon   " +
                " from tbcorpal_detalleventasproducto dv " +
                " left join tbcorpal_producto pp on dv.codprod = pp.codigo " +
                " where   " +                
                " dv.codventa = " +codigoVendido;
            return cnx.consultaMySql(consulta);
        }

        internal DataSet get_ventaRealizadaparaVaciar(int codigoVendido)
        {
            string consulta = "SELECT " +
                " vv.codigo, date_format(vv.fechagra,'%d/%m/%Y') as 'fecha_Gra',   " +
                " vv.horagra, ifnull(cc.cod_clienteupon,0) as 'codigoCliente', vv.cliente, vv.correoCliente,   " +
                " vv.nitEmisor, vv.razonSocialEmisor, vv.municipio, vv.telefono,   " +
                " vv.numeroFactura, vv.cuf, vv.cufd, vv.codigoSucursal, " +
                " vv.direccion, vv.codigoPuntoVenta, vv.fechaEmision, " +
                " vv.nombreRazonSocial, vv.codigoTipoDocumentoIdentidad, " +
                " vv.numeroDocumento, vv.codigoMetodoPago, vv.numeroTarjeta, " +
                " vv.montoTotal, vv.montoTotalSujetoIva, vv.codigoMoneda, " +
                " vv.tipoCambio, vv.montoTotalMoneda, vv.descuentoAdicional, " +
                " vv.leyendaF, vv.codresp, vv.responsable, vv.factura, " +
                " vv.fechaentrega, vv.codcliente, vv.cicliente, " +
                " vv.estado, vv.codsolicitudentregaproducto, " +
                " vv.estadoventa, vv.vaciadoupon, vv.codusercierre,   " +
                " vv.responsablecierre " +
                " from tbcorpal_venta vv " +
                " left join  tbcorpal_cliente cc on vv.codigoCliente = cc.codigo "+
                " where vv.codigo = " +codigoVendido;
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
                " tbcorpal_producto pp" +
                " where dp.codproducto = pp.codigo and dp.codsolicitud = "+codigoSolicitud;
            return cnx.ejecutarMySql(consulta);
        }

        ////////// POST productos a venta uso adm
        internal bool insertarTodoslosProducosAVenta3(int codigoSolicitud)
        {
            try
            {
                string consulta = "insert into tbcorpal_detalleventasproducto ( " +
                        "codventa, codprod, descripcion, cantidad, codmedida, medida, " +
                        "precioUnitario, montoDescuento, precioTotal) " +
                        "select v.codigo, dsp.codproducto, p.producto, " +
                        "ifnull(dsp.cantentregada ,0), null, p.medida, p.precio, 0, " +
                        "ifnull((dsp.cantentregada * p.precio), 0) " +
                        "from tbcorpal_venta v " +
                        "join tbcorpal_solicitudentregaproducto sep on sep.codigo = v.codsolicitudentregaproducto " +
                        "join tbcorpal_detalle_solicitudproducto dsp on dsp.codsolicitud = sep.codigo " +
                        "join tbcorpal_producto p on p.codigo = dsp.codproducto " +
                        "where dsp.codsolicitud = @codigoSolicitud " +
                        "and v.estado = 1 and v.estadoventa = 'Abierto' " +
                        "and sep.estadosolicitud = 'Cerrado' and sep.estado = 1 ";

                Console.WriteLine("Consulta generada: " + consulta);

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@codigoSolicitud", codigoSolicitud)
                };
                return cnx.ejecutarMySql2arg(consulta, parametros);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error al insertar los productos a la venta123123: " + ex.Message);
                return false;
            }
        }

        // obtener datos del producto 
        internal DataSet get_productoCodProducto(int codigo)
        {
            string consulta = "Select p.medida, p.precio, " +
                "from tbcorpal_producto p where p.codigo = " + codigo + "";

            return cnx.consultaMySql(consulta);
        }

        internal DataSet GET_reportVentasObjVentasProductos(DateTime fechaini, DateTime fechafin)
        {
            try
            {
                string consulta = @"
                select 
                venta1.codprod,
                venta1.descripcion,
                ifnull(venta2.domingo, 0) as domingo,
                ifnull(venta2.lunes, 0) as lunes,
                ifnull(venta2.martes, 0) as martes,
                ifnull(venta2.miercoles, 0) as miercoles,
                ifnull(venta2.jueves, 0) as jueves,
                ifnull(venta2.viernes, 0) as viernes,
                ifnull(venta2.sabado, 0) as sabado,
                venta1.cantidad_total_vendida,
                objventa2.cantidadprod as 'obj_ventas',
                round((venta1.cantidad_total_vendida / nullif(objventa2.cantidadprod, 0)) * 100, 2) as 'cumplimiento_%', 
                objventa2.fechalimite

                from (
                     select
                     dv.`codprod`,
                     dv.`descripcion`,
                     SUM(dv.`cantidad`) AS cantidad_total_vendida 
                     from tbcorpal_venta v 
                     inner join tbcorpal_detalleventasproducto dv ON v.codigo = dv.`codventa` 
                     where 
                           v.`fechaEmision` >= @fechaini and v.`fechaEmision` <= @fechafin
                           and v.estado = 1 and v.`estadoventa` = 'Cerrado' 
                     group by dv.`codprod` 
                ) venta1 

                left join (
                     select dv.codprod,
                     SUM(CASE WHEN DAYOFWEEK(v.`fechaEmision`) = 1 THEN dv.`cantidad` ELSE 0 END) AS 'domingo',
                     SUM(CASE WHEN DAYOFWEEK(v.`fechaEmision`) = 2 THEN dv.`cantidad` ELSE 0 END) AS 'lunes',
                     SUM(CASE WHEN DAYOFWEEK(v.`fechaEmision`) = 3 THEN dv.`cantidad` ELSE 0 END) AS 'martes',
                     SUM(CASE WHEN DAYOFWEEK(v.`fechaEmision`) = 4 THEN dv.`cantidad` ELSE 0 END) AS 'miercoles',
                     SUM(CASE WHEN DAYOFWEEK(v.`fechaEmision`) = 5 THEN dv.`cantidad` ELSE 0 END) AS 'jueves',
                     SUM(CASE WHEN DAYOFWEEK(v.`fechaEmision`) = 6 THEN dv.`cantidad` ELSE 0 END) AS 'viernes',
                     SUM(CASE WHEN DAYOFWEEK(v.`fechaEmision`) = 7 THEN dv.`cantidad` ELSE 0 END) AS 'sabado' 
                     from tbcorpal_venta v 
                     inner join tbcorpal_detalleventasproducto dv ON v.codigo = dv.`codventa` 
                     where 
                     v.`estado` = 1 
                     and v.`estadoventa` = 'Cerrado' 
                     and week(v.`fechaEmision`, 1) = week(current_date(), 1) 
                     and year(v.`fechaEmision`) = year(current_date()) 
     
                     group by dv.`codprod` 
                ) venta2 ON venta2.codprod = venta1.codprod 

                left join ( 
                     select  
                      op.codprod,
                       max(op.`fechalimite`) as 'fechaMax' 
                      from tbcorpal_objetivosproduccion op 
                      where op.`estado` = 1 
                       and op.`fechalimite` <= @fechafin 
                      group by op.`codprod` 
                      ) objmax on objmax.codprod = venta1.codprod 

                left join tbcorpal_objetivosproduccion objventa2 
                     on objventa2.codprod = objmax.codprod 
                     and objventa2.fechalimite = objmax.fechamax 
                     and objventa2.estado = 1 GROUP BY objventa2.codprod";

                var parametros = new List<MySqlParameter>
            {
                new MySqlParameter("@fechaini", (fechaini)),
                new MySqlParameter("@fechafin", (fechafin))
            };
                return cnx.consultaMySqlParametros(consulta, parametros);

            }
            catch(Exception ex)
            {
                throw new Exception("Error al obtener datos. " + ex.Message);
            }
            
        } 


    }
}