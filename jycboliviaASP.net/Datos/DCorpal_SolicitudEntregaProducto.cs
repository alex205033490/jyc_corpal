using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;
using static System.Data.Entity.Infrastructure.Design.Executor;
using static jycboliviaASP.net.Presentacion.FCorpal_APIProduccion;
using MySql.Data.MySqlClient;
using ZstdSharp.Unsafe;
using Microsoft.ReportingServices.Interfaces;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_SolicitudEntregaProducto
    {
        private conexionMySql conexion = new conexionMySql();
        public DCorpal_SolicitudEntregaProducto() { }

        internal DataSet get_mostrarProductosClienteLista(string producto, int codCliente)
        {
            try
            {
                NA_VariablesGlobales vlocal = new NA_VariablesGlobales();
                string consultaStockActual = vlocal.get_consultaStockProductosActual();

                string consulta = $@"select 
                                    pp.`codigo`,
                                    pp.`producto`,
                                    pp.medida,
                                    dlp.`precio`,
                                    t1.StockAlmacen,
                                    t1.StockParcialAlmacen
                                    
                                    from 
                                    tbcorpal_cliente cl
                                    inner join tbcorpal_listaprecio lp on cl.`id_listaprecio` = lp.`codigo`
                                    left join tbcorpal_detallelistaprecio dlp on lp.`codigo` = dlp.`id_listaprecio`
                                    inner join tbcorpal_producto pp on dlp.`id_producto` = pp.`codigo`
                                    left join ({consultaStockActual}) as t1 on t1.codigo = pp.codigo 
                                    where pp.`estado` = 1 and lp.`estado` = 1 and dlp.`estado` = 1 
                                    and cl.`codigo` = @codcliente
                                    and pp.`producto` like @producto ";

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@codcliente", codCliente),
                    new MySqlParameter("@producto", "%"+producto+"%")
                };
                return conexion.consultaMySqlParametros(consulta, parametros);
            }
            catch (Exception ex)            
            {
                throw new Exception("Error al consultar datos. " + ex.Message);
            }
        }

        internal DataSet get_mostrarProductos(string producto)
        {
            NA_VariablesGlobales vlocal = new NA_VariablesGlobales();
            string consultaStockActual = vlocal.get_consultaStockProductosActual();

            string consulta = @"select pp.codigo, pp.producto from  
                                tbcorpal_producto pp 
                                where pp.estado = 1 
                                and pp.producto like @producto";
            var parametros = new List<MySqlParameter>
            {
                new MySqlParameter("@producto", "%" +producto+ "%")
            };
            return conexion.consultaMySqlParametros(consulta, parametros);
        }

        internal bool set_guardarSolicitud(string nroboleta, string fechaentrega, string horaentrega, string personalsolicitud, 
                                    int codpersolicitante, bool estado, int codcliente, int codModPago)
        {
            try
            {
                string consulta = @"insert into tbcorpal_solicitudentregaproducto ( 
                              nroboleta, fechaGRA, horaGRA, fechaentrega, horaentrega,  personalsolicitud, 
                              codpersolicitante,   estado, estadosolicitud, vaciadoupon, codcliente, cod_modcobranza) 
                              values( @nroboleta, current_date(), current_time(), @fechaentrega, @horaentrega, @personalsolicitud, @codpersolicitante, 
                              @estado, 'Abierto', false, @codcliente, @cod_modpago)";

                using (MySqlCommand cmd = new MySqlCommand(consulta))
                {
                    cmd.Parameters.AddWithValue("@nroboleta", nroboleta);
                    cmd.Parameters.AddWithValue("@fechaentrega", fechaentrega);
                    cmd.Parameters.AddWithValue("@horaentrega", horaentrega);
                    cmd.Parameters.AddWithValue("@personalsolicitud", personalsolicitud);
                    cmd.Parameters.AddWithValue("@codpersolicitante", codpersolicitante);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@codcliente", codcliente);
                    cmd.Parameters.AddWithValue("@cod_modpago", codModPago);

                    return conexion.ejecutarMySql2(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurro un error inesperado. " + ex.Message);
            }
            /*
            string consulta = "insert into tbcorpal_solicitudentregaproducto ( "+
                             " nroboleta, fechaGRA, horaGRA, fechaentrega, horaentrega,  personalsolicitud, "+
                             " codpersolicitante,   estado, estadosolicitud, vaciadoupon, codcliente, cod_modcobranza) " +
                             " values( "+
                             " '"+nroboleta+"', current_date(), current_time(), "+fechaentrega+", '"+horaentrega+"',  '"+personalsolicitud+"',"+
                              codpersolicitante + ", " + estado + ", 'Abierto', false, "+codcliente+" )";
            return conexion.ejecutarMySql(consulta);
            */
        }


        internal DataSet get_siguentenumeroRecibo(int codUser)
        {
            string consulta = "select CONCAT( "+
                                " substring(res.nombre,1,1), "+
                                " substring(REVERSE(LEFT(REVERSE(res.nombre), locate(' ', REVERSE(res.nombre))-1 )),1,1),'_', "+
                                " count(ss.codigo) + 1 "+ 
                                " ) as 'NroRecibo' "+ 
                                " from tbcorpal_solicitudentregaproducto ss, tb_responsable res "+
                                " where "+
                                " ss.codpersolicitante = res.codigo and "+
                                " ss.codpersolicitante = "+codUser;
            return conexion.consultaMySql(consulta);
        }

        internal DataSet getultimaSolicitudproductoInsertado(int codpersolicitante)
        {
            string consulta = "select max(pp.codigo) from tbcorpal_solicitudentregaproducto pp where "+
                              " pp.codpersolicitante = "+codpersolicitante;
            return conexion.consultaMySql(consulta);
        }

        internal bool insertarDetalleSolicitudProducto(int ultimoinsertado, int codProducto, decimal cantidad, 
                                                decimal preciocompra, decimal total, string Tipo, string Medida)
        {
            try
            {
                /*
                double porcentajeDescuento = 0;
                string consultaDescuento = @"select pr.porcentaje_descuento 
                                        from tbcorpal_producto_descuento pr where 
                                        pr.producto_codigo = @codProd and @cantidad between 
                                        pr.cantidad_min and pr.cantidad_max limit 1";
                using (MySqlCommand cmd = new MySqlCommand(consultaDescuento))
                {
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@codProd", codProducto);
                    var result = conexion.ejecutarScalarObject(cmd);

                    if (result != null)
                    {
                        porcentajeDescuento = Convert.ToDouble(result);
                    }
                    if (porcentajeDescuento > 0)
                    {
                        total = total - (total * porcentajeDescuento / 100);
                    }
                }
                */
                string consultaGeneral = @"insert into tbcorpal_detalle_solicitudproducto 
                                    (codsolicitud, codproducto, cant, precio, precioTotal, tiposolicitud, medida) values 
                                    (@ultInsertado, @codProducto, @cantidad, @precio, @total, @tipo, @medida);";

                using (MySqlCommand cmd = new MySqlCommand(consultaGeneral))
                {
                    cmd.Parameters.AddWithValue("@ultInsertado", ultimoinsertado);
                    cmd.Parameters.AddWithValue("@codProducto", codProducto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@precio", preciocompra);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@tipo", Tipo);
                    cmd.Parameters.AddWithValue("@medida", Medida);

                    return conexion.ejecutarMySql2(cmd);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error en la consulta insertar Detalle Solicitud Producto. " + ex.Message);
            }
        }

        internal bool actualizarmontoTotal(int ultimoinsertado)
        {
            try
            {
                string consulta = @"update tbcorpal_solicitudentregaproducto s 
                        set s.montoTotal = (
                        select ifnull(sum(d.precioTotal), 0) 
                        from tbcorpal_detalle_solicitudproducto d 
                        where d.codsolicitud = @codigo 
                        ) where s.codigo = @codigo";
                using (MySqlCommand cmd = new MySqlCommand(consulta))
                {
                    cmd.Parameters.AddWithValue("@codigo", ultimoinsertado);
                    return conexion.ejecutarMySql2(cmd);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("error al actualizar el monto Total. " + ex.Message);
            }
        }


        internal DataSet get_datosSolicitudProductos(int codigoSolicitud)
        {
            /* string consultaStock = "SELECT pp.codigo, pp.producto, pp.medida, ifnull(t1.ingreso,0) as 'Ingreso1', ifnull(t2.salida,0) as 'Salida1', "+
                                    " (ifnull(t1.ingreso,0) - ifnull(t2.salida,0)) as 'StockAlmacen' "+ 
                                    " FROM tbcorpal_producto pp "+
                                    " LEFT JOIN "+ 
                                    " ( "+
                                    " select "+ 
                                    " oo.codProductonax, sum(oo.cantcajas) as 'ingreso' "+                                   
                                    " from tbcorpal_entregasordenproduccion oo " +
                                    " where "+
                                    " oo.estado = 1 and "+
                                    " oo.fechagra between "+NA_VariablesGlobales.fechaInicialProduccion+" and current_date() "+
                                    " group by oo.codProductonax "+
                                    " ) as t1  ON pp.codigo = t1.codProductonax "+
                                    " LEFT JOIN "+
                                    " ( "+
                                    " select dss.codproducto, sum(dss.cantentregada) as 'salida' "+                                   
                                    " from tbcorpal_solicitudentregaproducto ss, " +  
                                    " tbcorpal_detalle_solicitudproducto dss "+
                                    " where "+ 
                                    " ss.codigo = dss.codsolicitud and "+
                                    " ss.estado = 1 and "+
                                    " ss.estadosolicitud = 'Cerrado' and "+
                                    " dss.itempackferial is not true and "+
                                    " ss.fechaentrega between " + NA_VariablesGlobales.fechaInicialProduccion+" and current_date() " +
                                    " group by dss.codproducto "+
                                    " ) as t2 ON pp.codigo = t2.codproducto "+
                                    " WHERE "+
                                    " pp.estado = 1";  */
            NA_VariablesGlobales nv = new NA_VariablesGlobales();
            string consultaStock = nv.get_consultaStockProductosActual();

            string consulta = " select "+
                                " pp.codigo, pp.producto, dse.medida , dse.cant as 'cantSolicitada', " +
                                " dse.tiposolicitud, "+
                                " ifnull(dse.cantentregada,0) as 'Cant_Entregada', " +
                                " CASE dse.tiposolicitud "+
                                " WHEN 'ITEM PACK FERIAL' THEN ifnull(pp.StockPackFerial,0) " +
                                " ELSE ifnull(pp.StockAlmacen,0) "+
                                " END AS 'stock_Almacen' " +                                
                                " from tbcorpal_solicitudentregaproducto se , "+
                                " tbcorpal_detalle_solicitudproducto dse "+
                                " left join (" + consultaStock + ") as pp on dse.codproducto = pp.codigo " +
                                " where "+                                
                                " se.codigo = dse.codsolicitud and "+                                
                                " se.codigo = "+codigoSolicitud;
            return conexion.consultaMySql(consulta);
        }

        internal bool eliminarSolicitud(int codigoSolicitud)
        {
            bool banderaResultado = false;
            string consulta0 = "update tbcorpal_producto , tbcorpal_detalle_solicitudproducto "+
                               " set tbcorpal_producto.stock = (tbcorpal_producto.stock - tbcorpal_detalle_solicitudproducto.cantentregada) " +
                               " where "+
                               " tbcorpal_producto.codigo = tbcorpal_detalle_solicitudproducto.codproducto and " +
                               " tbcorpal_detalle_solicitudproducto.codsolicitud ="+ codigoSolicitud;
            bool bandera0 = conexion.ejecutarMySql(consulta0);

            if (bandera0)
            {
                string consulta = "update tbcorpal_solicitudentregaproducto " +
                              " set tbcorpal_solicitudentregaproducto.estado = false, " +
                              "  tbcorpal_solicitudentregaproducto.estadosolicitud = 'Cerrado' " +
                              " where tbcorpal_solicitudentregaproducto.codigo = " + codigoSolicitud;
                banderaResultado = conexion.ejecutarMySql(consulta);
            }

            return banderaResultado;
            
        }

        internal bool update_cantProductosEntregados(int codigoSolicitud, int codigoP, float cantEntregado, float restarStock)
        {
            bool banderaResultado = false;
            string consulta0 = "update tbcorpal_producto "+
                               " set tbcorpal_producto.stock = tbcorpal_producto.stock - CAST('" + restarStock.ToString().Replace(',', '.') + "' AS DECIMAL(10,2))" +
                               " where "+
                               " tbcorpal_producto.codigo = "+codigoP;

            bool bandera0 = conexion.ejecutarMySql(consulta0);

            if (bandera0) {
                string consulta = "update tbcorpal_detalle_solicitudproducto " +
                                    " set tbcorpal_detalle_solicitudproducto.cantentregada = '" + cantEntregado.ToString().Replace(',', '.') + "'" +
                                    " where " +
                                    " tbcorpal_detalle_solicitudproducto.codsolicitud = " + codigoSolicitud + " and " +
                                    " tbcorpal_detalle_solicitudproducto.codproducto =" + codigoP;
                banderaResultado = conexion.ejecutarMySql(consulta);            
            }

            return banderaResultado;
        }

        internal bool update_cerrarSolicitud(int codigoSolicitud, int codresponsable, string nombreResponsable, string estadoCierre, string motivoCierre, string fechaEntrega, string horaEntrega)
        {
            string consulta = "update tbcorpal_solicitudentregaproducto "+
                                " set tbcorpal_solicitudentregaproducto.estadosolicitud = '"+estadoCierre+"' , "+
                                " tbcorpal_solicitudentregaproducto.detallecierre = '"+motivoCierre+"', "+
                                " tbcorpal_solicitudentregaproducto.fechaentrega = " + fechaEntrega + ", " +
                                " tbcorpal_solicitudentregaproducto.horaEntrega = '" + horaEntrega + "', " +
                                " tbcorpal_solicitudentregaproducto.fechacierre = current_date(), "+
                                " tbcorpal_solicitudentregaproducto.horacierre = current_time(), "+
                                " tbcorpal_solicitudentregaproducto.codperentregoproducto = "+codresponsable+", "+
                                " tbcorpal_solicitudentregaproducto.personalentregoproducto = '"+nombreResponsable+"' "+
                                " where tbcorpal_solicitudentregaproducto.codigo =  "+codigoSolicitud ;
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_entregaSolicitudProductos(int codigoEntregaSolicitudProducto)
        {
            string consulta = "select "+
                                " pp.codigo, pp.nroboleta, "+
                                " date_format(pp.fechaentrega,'%d/%m/%Y') as 'Fecha Entrega', pp.horaentrega, "+
                                " pp.personalsolicitud, pp.personalentregoproducto "+
                                " , cc.tiendaname as 'Cliente' "+
                                " from tbcorpal_solicitudentregaproducto pp "+
                                " left join tbcorpal_cliente cc on pp.codcliente = cc.codigo "+   
                                " where " +
                                " pp.codigo = "+codigoEntregaSolicitudProducto;
            return conexion.consultaMySql(consulta);
        }

        

        internal DataSet get_productosEngregaSolicitudProducto(int codigoEntregaSolicitudProducto)
        {
            string consulta = "select "+
                                " pp.producto, pp.medida , "+
                                " dse.cantentregada as 'cantidad', "+
                                " dse.tiposolicitud "+
                                " , pp. codupon "+
                                " from tbcorpal_solicitudentregaproducto se , " +
                                " tbcorpal_detalle_solicitudproducto dse, tbcorpal_producto pp "+
                                " where "+
                                " se.codigo = dse.codsolicitud and "+
                                " dse.codproducto = pp.codigo and "+
                                " se.codigo = " + codigoEntregaSolicitudProducto;
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_productosSolicitudProducto(int codigoSolicitudProducto)
        {
            string consulta = "select " +
                                " pp.producto, dse.medida , " +
                                " dse.cant as 'cantidad', " +
                                " dse.tiposolicitud, pp.codupon " +
                                " from tbcorpal_solicitudentregaproducto se , " +
                                " tbcorpal_detalle_solicitudproducto dse, tbcorpal_producto pp " +
                                " where " +
                                " se.codigo = dse.codsolicitud and " +
                                " dse.codproducto = pp.codigo and " +
                                " se.codigo = " + codigoSolicitudProducto;
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_alldetalleProductoSolicitudEntregado(string fechadesde, string fechahasta, string personalsolicitud)
        {
            string consulta = " select "+
                                " se.codigo, "+
                                " se.nroboleta, "+
                                " date_format(se.fechaGRA,'%d/%m/%Y') as 'fecha_Gra', "+
                                " se.horaGRA, "+
                                " date_format(se.fechaentrega,'%d/%m/%Y') as 'fecha_entrega', "+
                                " se.horaentrega, "+
                                " se.personalsolicitud, "+ 
                                " pp.producto, pp.medida , "+ 
                                " dse.cant as 'cantidad', "+                                 
                                " dse.tiposolicitud, "+
                                " se.personalentregoproducto, "+
                                " date_format(se.fechacierre,'%d/%m/%Y') as 'fecha_Cierre', "+
                                " se.horacierre, "+
                                " se.estadosolicitud, "+
                                " dse.cantentregada " +
                                " from tbcorpal_solicitudentregaproducto se , "+ 
                                " tbcorpal_detalle_solicitudproducto dse, tbcorpal_producto pp "+
                                " where "+
                                " se.codigo = dse.codsolicitud and "+
                                " dse.codproducto = pp.codigo and "+
                                " se.estado = 1 and "+
                                " se.personalsolicitud like '%" + personalsolicitud + "%' and " +
                                " se.fechaGRA BETWEEN "+fechadesde+" and "+fechahasta;
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_alldetalleProductoSolicitud_VS_Entregado(string fechadesde, string fechahasta)
        {
            string consulta = "select "+ 
                               " pp.codigo, "+
                               " pp.producto, "+
                               " pp.medida, "+
                               " format(ifnull(t1.cantidadsolicitado,0),2) as 'CantidadSolicitada', "+
                               " format(ifnull(t1.cantidadEntregada,0),2) as 'Cantidad_Entregada', "+
                               "  format(ifnull(t1.precio,0),2) as 'PrecioUnidad', "+
                               "  format(ifnull((t1.cantidadsolicitado * t1.precio),0),2) as 'MontoSolicitado', "+
                               "  format(ifnull((t1.cantidadEntregada * t1.precio),0),2) as 'MontoEntregado', "+
                               "  format((ifnull((t1.cantidadsolicitado * t1.precio),0) "+
                               "  -  "+
                               "  ifnull((t1.cantidadEntregada * t1.precio),0)),2) as 'Perdida' "+
                               " from tbcorpal_producto pp "+ 
                               " LEFT JOIN "+
                               " ( "+
                               " select "+  
                               " dse.codproducto, "+
                               " sum(dse.cant) as 'cantidadsolicitado' , "+
                               " dse.precio  as 'precio', "+
                               " sum(dse.cantentregada) as 'cantidadEntregada' "+
                               " from tbcorpal_solicitudentregaproducto se, tbcorpal_detalle_solicitudproducto dse "+
                               " where "+ 
                               " se.estado = 1 and "+
                               " se.codigo = dse.codsolicitud and "+
                               " se.estadosolicitud = 'Cerrado' and "+
                               " se.fechaentrega BETWEEN "+fechadesde+" and "+fechahasta+
                               " group by dse.codproducto "+
                               " )AS t1 "+
                               " ON  (pp.codigo = t1.codproducto) "+
                               " where pp.estado = 1;";
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_CodigoProductos(string producto)
        {
            string consulta = "select codigo, producto, medida, precio " +
                              " from tbcorpal_producto pp where pp.producto = '" + producto + "'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_alldetalleProductoSolicitadosyEntregadosporpersona(string fechadesde, string fechahasta, string Responsable)
        {
            string consulta = "SELECT SF.personalsolicitud, SF.producto, SF.total_cantSolicitada, SF.total_cantentregada "+
                               " FROM "+
                               " ( "+
                               " SELECT personalsolicitud, producto, SUM(total_cant) AS 'total_cantSolicitada', SUM(total_cantentregada) AS 'total_cantentregada' "+
                               " FROM ( "+
                               " SELECT s.personalsolicitud, p.producto, COALESCE(SUM(d.cant), 0) AS total_cant, COALESCE(SUM(d.cantentregada), 0) AS total_cantentregada "+
                               " FROM tbcorpal_solicitudentregaproducto s "+
                               " RIGHT JOIN tbcorpal_detalle_solicitudproducto d ON s.codigo = d.codsolicitud "+
                               " RIGHT JOIN tbcorpal_producto p ON p.codigo = d.codproducto "+
                               " WHERE s.estado = 1 and p.estado = 1 and s.fechaGRA BETWEEN "+fechadesde+" AND "+fechahasta+
                               " GROUP BY s.personalsolicitud, p.producto "+
                               " UNION "+
                               " SELECT s.personalsolicitud, p.producto, 0 AS total_cant, 0 AS total_cantentregada "+
                               " FROM tbcorpal_producto p "+
                               " CROSS JOIN ( "+
                               " SELECT personalsolicitud "+
                               " FROM tbcorpal_solicitudentregaproducto "+
                               " GROUP BY personalsolicitud "+
                               " ) as s "+
                               " LEFT JOIN tbcorpal_detalle_solicitudproducto d ON p.codigo = d.codproducto "+
                               " LEFT JOIN tbcorpal_solicitudentregaproducto se ON se.codigo = d.codsolicitud AND se.personalsolicitud = s.personalsolicitud "+
                               " WHERE p.estado = 1 and se.codigo IS NULL "+
                               " ) AS result "+
                               " GROUP BY personalsolicitud, producto "+
                               " UNION "+
                               " SELECT res1.nombre AS personalsolicitud, pro1.producto, 0 AS 'total_cantSolicitada', 0 AS 'total_cantentregada' "+
                               " FROM tb_responsable res1 "+
                               " CROSS JOIN tbcorpal_producto pro1 "+
                               " WHERE pro1.estado = 1 and res1.codigo NOT IN ( " +
                               " SELECT sol1.codpersolicitante "+
                               " FROM tbcorpal_solicitudentregaproducto sol1 "+
                               " WHERE sol1.estado = 1 and sol1.fechaGRA BETWEEN " + fechadesde + " AND " + fechahasta + 
                               " ) "+
                               " ORDER BY personalsolicitud, producto "+
                               " ) AS SF "+
                               " WHERE "+
                               " SF.personalsolicitud LIKE '%"+Responsable+"%'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal bool sumarStockenProducto(int codigoProdNax, float cantcajas)
        {
            string consulta = " update tbcorpal_producto set "+
                                " tbcorpal_producto.stock = tbcorpal_producto.stock + '"+cantcajas.ToString().Replace(',','.')+"'"+
                                " where "+
                                " tbcorpal_producto.codigo ="+codigoProdNax;
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_SumStockTotal(int codigoSolicitud)
        {
            string consulta = "select "+
                            " sum(pp.stock ) "+
                            " from tbcorpal_solicitudentregaproducto se , "+ 
                            " tbcorpal_detalle_solicitudproducto dse, tbcorpal_producto pp  "+
                            " where "+ 
                            " se.codigo = dse.codsolicitud and "+
                            " dse.codproducto = pp.codigo and "+
                            " se.codigo = "+codigoSolicitud;
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_StockProducctos(string fechaHasta)
        {         
           NA_VariablesGlobales nv = new NA_VariablesGlobales();
            string consulta = nv.get_consultaStockProductosActual_fecha(fechaHasta);
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_Stock(int codProducto)
        {
            /* string consulta = "select " +
                                " pp.codigo, pp.producto, pp.medida, ifnull(pp.stock,0) as 'stock' " +
                                " from tbcorpal_producto pp " +
                                " where pp.estado = 1 and pp.codigo = "+codProducto; 
            string consulta = "SELECT pp.codigo, pp.producto, pp.medida, " +
                               " ifnull(t1.ingreso,0) as 'Ingreso1', " +
                               " ifnull(t2.salida,0) as 'Salida1', " +
                               " (ifnull(t1.ingreso,0) - ifnull(t2.salida,0)) as 'StockAlmacen' " +
                               " FROM tbcorpal_producto pp " +
                               " LEFT JOIN " +
                               " ( " +
                               " select " +
                               " oo.codProductonax, sum(oo.cantcajas) as 'ingreso' " +
                               " from tbcorpal_entregasordenproduccion oo " +
                               " where " +
                               " oo.estado = 1 and " +
                               " oo.fechagra between " + NA_VariablesGlobales.fechaInicialProduccion + " and current_date() " +
                               " group by oo.codProductonax " +
                               " ) as t1  ON pp.codigo = t1.codProductonax " +
                               " LEFT JOIN " +
                               " ( " +
                               " select dss.codproducto, sum(dss.cantentregada) as 'salida' " +
                               " from tbcorpal_solicitudentregaproducto ss, " +
                               " tbcorpal_detalle_solicitudproducto dss " +
                               " where " +
                               " ss.codigo = dss.codsolicitud and " +
                               " ss.estado = 1 and " +
                               " ss.estadosolicitud = 'Cerrado' and " +
                               " ss.fechaentrega between " + NA_VariablesGlobales.fechaInicialProduccion + " and current_date() " +
                               " group by dss.codproducto " +
                               " ) as t2 ON pp.codigo = t2.codproducto " +
                               " WHERE " +
                               " pp.estado = 1 and " +
                               " pp.codigo = " + codProducto; */
            NA_VariablesGlobales nv = new NA_VariablesGlobales();
            string consulta = nv.get_consultaStockProductosActual(codProducto);
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_detalleEntregaSolicitudProductos(string fechadesde, string fechahasta)
        {
            string consulta = "select " +
                               " ss.codigo, ss.nroboleta, " +
                               " date_format(ss.fechaentrega,'%d/%m/%Y') as 'fecha_entrega', " +
                               " ss.horaentrega, " +
                               " ss.personalsolicitud, " +
                               " pp.producto, " +
                               " dss.cant as 'cant_solicitada', " +
                               " ifnull(dss.cantentregada,0) as 'cant_entregada', " +
                               " ss.estadosolicitud, " +
                               " date_format(ss.fechacierre,'%d/%m/%Y') as 'fecha_cierre', " +
                               " ss.horacierre, " +
                               " ss.personalentregoproducto, " +
                               " ss.detallecierre  " +
                               " ,pp.codupon "+
                               " from tbcorpal_solicitudentregaproducto ss, " +
                               " tbcorpal_detalle_solicitudproducto dss, " +
                               " tbcorpal_producto pp " +
                               " where " +
                               " ss.codigo = dss.codsolicitud and " +
                               " dss.codproducto = pp.codigo and " +
                               " ss.estado = 1 and " +
                               " pp.estado = 1 and "+
                               " ss.fechaentrega between " + fechadesde + " and " + fechahasta;
                               
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_allPedidosParaVaciarUpon(string cliente)
        {
            string consulta = "select   ss.codigo, ss.nroboleta,   " +
                " date_format(ss.fechaentrega,'%d/%m/%Y') as 'fecha_entrega',   " +
                " ss.horaentrega,   ss.personalsolicitud, " +                
                " ss.estadosolicitud,   date_format(ss.fechacierre,'%d/%m/%Y') as 'fecha_cierre', " +
                " ss.horacierre,   ss.personalentregoproducto,   cc.tiendaname as 'Cliente'  " +
                " from tbcorpal_solicitudentregaproducto ss  left join tbcorpal_cliente cc  on ss.codcliente = cc.codigo   " +                
                " where  " +
                " ss.estado = 1 and  ss.estadosolicitud = 'Cerrado' and  " +
                " ss.vaciadoupon = false  and "+
                " cc.tiendaname like '%"+cliente+"%' "+
                " order by ss.codigo desc ";
            return conexion.consultaMySql(consulta);
        }


        //----------------------------------------- P2        
        internal DataSet get_allPedidosVWParaVaciado(string cliente)
        {
            string consulta = "select ss.codigo, ss.nroboleta, date_format(ss.fechaentrega, '%d/%m/%Y') as 'fecha_entrega', " +
                " ss.horaentrega, cc.tiendaname AS 'Cliente', cc.cod_clienteupon as CodClienteUpon, SUM(dsp.preciototal) as 'ImporteProductos' from tbcorpal_solicitudentregaproducto ss " +
                " left join tbcorpal_cliente cc ON ss.codcliente = cc.codigo " +
                " inner join tbcorpal_detalle_solicitudproducto dsp ON ss.codigo = dsp.codsolicitud " +
                " where ss.estado = 1 and ss.estadosolicitud = 'Cerrado' and ss.vaciadoupon = false " +
                " and cc.tiendaname LIKE '%" + cliente + "%' group by ss.codigo order by ss.codigo desc";
            return conexion.consultaMySql(consulta);
        }
        internal DataSet get_ObtenerDetalleProductoAsync(int codsolicitud)
        {
            string consulta = "SELECT pr.codupon as 'CodigoProducto', dsp.cant as 'Cantidad', pr.codumupon as 'CodigoUnidadMedida', dsp.precio as 'PrecioUnitario', " +
                              " dsp.precioTotal as 'ImporteTotal' " +
                              " FROM tbcorpal_solicitudentregaproducto sep  " +
                              " INNER JOIN tbcorpal_detalle_solicitudproducto dsp ON sep.codigo = dsp.codsolicitud " +
                              " INNER JOIN tbcorpal_producto pr ON pr.codigo = dsp.codproducto " +
                              " WHERE dsp.`codsolicitud`= "+ codsolicitud +" AND sep.estado = 1 AND sep.estadosolicitud = 'Cerrado' " +
                              " AND sep.vaciadoupon = 0;";
            return conexion.consultaMySql(consulta);
        }

        internal bool anularPedidoVaciadoUpon(int codigoPedido, object bandera)
        {
            string consulta = " update tbcorpal_solicitudentregaproducto set " +
                " tbcorpal_solicitudentregaproducto.vaciadoupon = " + bandera +
                " where  tbcorpal_solicitudentregaproducto.codigo = " + codigoPedido;
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_mostrarProductos_quesoloestenvigente2(string producto)
        {
            string consulta = "select "+
                                 " pp.codigo,"+
                                 " concat('(', ifnull(pp.codupon,'No Codigo') ,') ', pp.producto) as 'producto' "+
                                 " , pp.medida "+
                                 " FROM tbcorpal_producto pp " +
                                 " where "+ 
                                 " pp.producto like '%"+producto+"%' and "+
                                 " pp.estado = 1 ";
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_Producto(int codprod)
        {
            string consulta = "select " +
                                 " pp.codigo,pp.producto, pp.medida " +
                                 " FROM tbcorpal_producto pp " +
                                 " where " +
                                 " pp.codigo = "+codprod +" and "+
                                 " pp.estado = 1 ";
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_obtenerModalidadPago()
        {
            try
            {
                string consulta = @"select mc.codigo, mc.nombre 
                                    from tbcorpal_modalidadcobranza mc 
                                    where mc.estado = 1 order by mc.nombre asc";

                return conexion.consultaMySql(consulta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar datos Modalidad Pago" + ex.Message);
            }
        }

        internal decimal obtenerPorcDescuentoCliNormal(int codProducto, decimal cantidad)
        {
            try
            {
                decimal porcentaje = 0;

                string consulta = @"SELECT pr.porcentaje_descuento
                                    FROM tbcorpal_producto_descuento pr
                                    WHERE pr.producto_codigo = @codProd
                                    AND @cantidad BETWEEN pr.cantidad_min AND pr.cantidad_max
                                    LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(consulta))
                {
                    cmd.Parameters.AddWithValue("@codProd", codProducto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);

                    var result = conexion.ejecutarScalarObject(cmd);

                    if (result != null && result != DBNull.Value)
                    {
                        porcentaje = Convert.ToDecimal(result);
                    }
                }
                return porcentaje;
            }
            catch(Exception ex)
            {
                throw new Exception("Error al obtener el descuento. " + ex.Message);
            }
        }

        internal int identificarTipoCliente (int codCli)
        {
            try
            {
                int tipoCli = 0;
                string consulta = @"select c.id_tipocliente from tbcorpal_cliente c 
                                    where c.codigo = @cod";
                using (MySqlCommand cmd = new MySqlCommand(consulta))
                {
                    cmd.Parameters.AddWithValue("@cod", codCli);
                    var result = conexion.ejecutarScalarObject(cmd);

                    if (result != null && result != DBNull.Value)
                    {
                        tipoCli = Convert.ToInt32(result);
                    }
                }
                return tipoCli;
            }
            catch(Exception ex)
            {
                throw new Exception("Error al identificar al tipo cliente. " +ex.Message);
            }
        }

    }
}