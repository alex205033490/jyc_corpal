using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_SolicitudEntregaProducto
    {
        private conexionMySql conexion = new conexionMySql();
        public DCorpal_SolicitudEntregaProducto() { }

        internal DataSet get_mostrarProductos(string producto)
        {
            string consulta = "select codigo, producto, medida, precio "+ 
                               " from tbcorpal_producto pp where pp.`producto` like '%"+producto+"%'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal bool set_guardarSolicitud(string nroboleta, string fechaentrega, string horaentrega, string personalsolicitud, int codpersolicitante, bool estado)
        {
            string consulta = "insert into tbcorpal_solicitudentregaproducto ( "+
                             " nroboleta, fechaGRA, horaGRA, fechaentrega, horaentrega,  personalsolicitud, "+
                             " codpersolicitante,   estado, estadosolicitud) " +
                             " values( "+
                             " '"+nroboleta+"', current_date(), current_time(), "+fechaentrega+", '"+horaentrega+"',  '"+personalsolicitud+"',"+
                              codpersolicitante + ", " + estado + ", 'Abierto' )";
            return conexion.ejecutarMySql(consulta);
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

        internal bool insertarDetalleSolicitudProducto(int ultimoinsertado, int codProducto, double cantidad, double preciocompra, double total, string Tipo, string Medida)
        {
            string consulta = "insert into tbcorpal_detalle_solicitudproducto( "+
                           " codsolicitud,codproducto,cant,precio,precioTotal,tiposolicitud,medida) "+
                           " values(" + ultimoinsertado + "," + codProducto + ",'" + cantidad.ToString().Replace(',', '.') + "','" + preciocompra.ToString().Replace(',', '.') + "','" + total.ToString().Replace(',', '.') + "','" + Tipo + "','" + Medida + "')";
            return conexion.ejecutarMySql(consulta);
        }

        internal bool actualizarmontoTotal(int ultimoinsertado, double montoTotal)
        {
            string consulta = "update tbcorpal_solicitudentregaproducto "+
                               " set tbcorpal_solicitudentregaproducto.montototal = '"+montoTotal.ToString().Replace(',','.')+"'"+  
                               " where tbcorpal_solicitudentregaproducto.codigo = "+ultimoinsertado;
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_solicitudesRealizadasProductos(string nroSolicitud, string solicitante)
        {
            string consulta = "select "+
                                " codigo, nroboleta, "+
                                " date_format(fechaGRA,'%d/%m/%Y') as 'Fecha Grabacion', horaGRA, "+
                                " date_format(fechaentrega,'%d/%m/%Y') as 'Fecha Entrega', horaentrega, "+
                                " personalsolicitud, montototal "+
                                " from  tbcorpal_solicitudentregaproducto pp "+
                                " where "+
                                " pp.estadosolicitud = 'Abierto' and " +
                                " pp.estado = true and " +
                                " pp.nroboleta like '%"+nroSolicitud+"%' and pp.`personalsolicitud` like '%"+solicitante+"%'";
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_datosSolicitudProductos(int codigoSolicitud)
        {
            string consulta = " select "+
                                " pp.codigo, pp.producto, pp.medida , dse.cant as 'cantSolicitada', "+
                                " dse.tiposolicitud, "+
                                " ifnull(dse.cantentregada,0) as 'Cant_Entregada' "+
                                " from tbcorpal_solicitudentregaproducto se , "+
                                " tbcorpal_detalle_solicitudproducto dse, tbcorpal_producto pp "+
                                " where "+                                
                                " se.codigo = dse.codsolicitud and "+
                                " dse.codproducto = pp.codigo and "+
                                " se.codigo = "+codigoSolicitud;
            return conexion.consultaMySql(consulta);
        }

        internal bool eliminarSolicitud(int codigoSolicitud)
        {
            string consulta = "update tbcorpal_solicitudentregaproducto " +
                              " set tbcorpal_solicitudentregaproducto.estado = false, " +
                              "  tbcorpal_solicitudentregaproducto.estadosolicitud = 'Cerrado' " +
                              " where tbcorpal_solicitudentregaproducto.codigo = " + codigoSolicitud;
            return conexion.ejecutarMySql(consulta);
        }

        internal bool update_cantProductosEntregados(int codigoSolicitud, int codigoP, float cantEntregado)
        {
            string consulta = "update tbcorpal_detalle_solicitudproducto "+
                                " set tbcorpal_detalle_solicitudproducto.cantentregada = '"+cantEntregado.ToString().Replace(',','.')+"'"+
                                " where "+
                                " tbcorpal_detalle_solicitudproducto.codsolicitud = "+codigoSolicitud+" and "+
                                " tbcorpal_detalle_solicitudproducto.codproducto ="+codigoP;
            return conexion.ejecutarMySql(consulta);
        }

        internal bool update_cerrarSolicitud(int codigoSolicitud, int codresponsable, string nombreResponsable)
        {
            string consulta = "update tbcorpal_solicitudentregaproducto "+
                                " set tbcorpal_solicitudentregaproducto.estadosolicitud = 'Cerrado' , "+
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
                                " from  tbcorpal_solicitudentregaproducto pp "+
                                " where "+
                                " pp.codigo = "+codigoEntregaSolicitudProducto;
            return conexion.consultaMySql(consulta);
        }

        

        internal DataSet get_productosEngregaSolicitudProducto(int codigoEntregaSolicitudProducto)
        {
            string consulta = "select "+
                                " pp.producto, pp.medida , "+
                                " dse.cantentregada as 'cantidad', "+
                                " dse.tiposolicitud "+
                                " from tbcorpal_solicitudentregaproducto se , "+
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
                                " pp.producto, pp.medida , " +
                                " dse.cant as 'cantidad', " +
                                " dse.tiposolicitud " +
                                " from tbcorpal_solicitudentregaproducto se , " +
                                " tbcorpal_detalle_solicitudproducto dse, tbcorpal_producto pp " +
                                " where " +
                                " se.codigo = dse.codsolicitud and " +
                                " dse.codproducto = pp.codigo and " +
                                " se.codigo = " + codigoSolicitudProducto;
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_alldetalleProductoSolicitudEntregado(string fechadesde, string fechahasta)
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
                                " se.fechaGRA BETWEEN "+fechadesde+" and "+fechahasta;
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_alldetalleProductoSolicitud_VS_Entregado(string fechadesde, string fechahasta)
        {
            string consulta = "select "+ 
                               " pp.codigo, "+
                               " pp.producto, "+
                               " pp.medida, "+
                               " ifnull(t1.cantidadsolicitado,0) as 'CantidadSolicitada', "+
                               " ifnull(t1.cantidadEntregada,0) as 'Cantidad_Entregada', "+
                               " ifnull(t1.precio,0) as 'PrecioUnidad', "+
                               " ifnull((t1.cantidadsolicitado * t1.precio),0) as 'MontoSolicitado', "+
                               " ifnull((t1.cantidadEntregada * t1.precio),0) as 'MontoEntregado', "+
                               " (ifnull((t1.cantidadsolicitado * t1.precio),0) "+
                               " - "+
                               " ifnull((t1.cantidadEntregada * t1.precio),0)) as 'Perdida' "+
                               " from tbcorpal_producto pp "+ 
                               " LEFT JOIN "+
                               " ( "+
                               " select "+  
                               " dse.codproducto, "+
                               " sum(dse.cant) as 'cantidadsolicitado' , "+
                               " sum(dse.precio)  as 'precio', "+
                               " sum(dse.cantentregada) as 'cantidadEntregada' "+
                               " from tbcorpal_solicitudentregaproducto se, tbcorpal_detalle_solicitudproducto dse "+
                               " where "+ 
                               " se.codigo = dse.codsolicitud and "+
                               " se.estadosolicitud = 'Cerrado' and "+
                               " se.fechaentrega BETWEEN "+fechadesde+" and "+fechahasta+
                               " group by dse.codproducto "+
                               " )AS t1 "+
                               " ON  (pp.codigo = t1.codproducto)";
            return conexion.consultaMySql(consulta);
        }
    }
}