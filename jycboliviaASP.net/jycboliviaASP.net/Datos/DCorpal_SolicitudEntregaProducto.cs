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
                             " codpersolicitante,   estado) " +
                             " values( "+
                             " '"+nroboleta+"', current_date(), current_time(), "+fechaentrega+", '"+horaentrega+"',  '"+personalsolicitud+"',"+
                              codpersolicitante + ", " + estado + " )";
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
                           " values(" + ultimoinsertado + "," + codProducto + "," + cantidad + "," + preciocompra + "," + total + ",'" + Tipo + "','" + Medida + "')";
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
                                " codigo, nroboleta, fechaGRA, horaGRA, fechaentrega, "+
                                " horaentrega, personalsolicitud, montototal "+
                                " from  tbcorpal_solicitudentregaproducto pp "+
                                " where "+
                                " pp.nroboleta like '%"+nroSolicitud+"%' and pp.`personalsolicitud` like '%"+solicitante+"%'";
            return conexion.consultaMySql(consulta);
        }
    }
}