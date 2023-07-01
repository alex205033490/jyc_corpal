using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_Produccion
    {
        private conexionMySql Conx = new conexionMySql();
        public DCorpal_Produccion() { }



        internal bool insertarEntregaProduccion(string nroorden, string turno, int codusuario, string respEntrega, float cantcajas, float unidadsuelta, float kgrdesperdicio, float kgrparamix, string detalleentrega, int codProdNax, string productoNax, int codresprecepcion, string resp_recepcion, int cod_respgra)
        {
            string consulta = "insert into tbcorpal_entregasordenproduccion( "+
                               " fechagra,horagra,turno,resp_entrega, "+
                               " cantcajas,unidadsuelta,kgrdesperdicio,kgrparamix, "+
                               " codorden,codrespentrega,detalleentrega,nroorden, "+
                               " productoNax,codProductonax, estado,codresprecepcion,resp_recepcion,cod_respgra " +
                               " ) values( "+
                               " current_date(), current_time(), '" + turno + "', '" + respEntrega + "', " +
                               " '"+cantcajas.ToString().Replace(',','.')+"', '"+unidadsuelta.ToString().Replace(',','.')+"', '"+kgrdesperdicio.ToString().Replace(',','.')+"', '"+kgrparamix.ToString().Replace(',','.')+"', "+
                               " null, " + codusuario + ", '" + detalleentrega + "', '" + nroorden + "', " +
                               " '" + productoNax + "', " + codProdNax + ", 1, "+codresprecepcion+",'"+resp_recepcion+"',"+cod_respgra+")";
            return Conx.ejecutarMySql(consulta);
        }

        internal bool modificarEntregaProduccion(int codigo, string nroorden, string turno, int codusuario, string respEntrega, float cantcajas, float unidadsuelta, float kgrdesperdicio, float kgrparamix, string detalleentrega, int codProdNax, string productoNax, int codresprecepcion, string resp_recepcion, int cod_respgra)
        {
            string consulta = "update tbcorpal_entregasordenproduccion set "+ 
                               " tbcorpal_entregasordenproduccion.turno = '"+turno+"', "+
                               " tbcorpal_entregasordenproduccion.resp_entrega = '" + respEntrega + "', " +
                               " tbcorpal_entregasordenproduccion.cantcajas = '"+cantcajas.ToString().Replace(',','.')+"', "+
                               " tbcorpal_entregasordenproduccion.unidadsuelta = '" + unidadsuelta.ToString().Replace(',', '.') + "', " +
                               " tbcorpal_entregasordenproduccion.kgrdesperdicio = '"+kgrdesperdicio.ToString().Replace(',','.')+"', " +
                               " tbcorpal_entregasordenproduccion.kgrparamix = '"+kgrparamix.ToString().Replace(',','.')+"', " +
                               " tbcorpal_entregasordenproduccion.codrespentrega = '" + codusuario + "', " +
                               " tbcorpal_entregasordenproduccion.detalleentrega = '"+detalleentrega+"', "+
                               " tbcorpal_entregasordenproduccion.nroorden = '"+nroorden+"', "+
                               " tbcorpal_entregasordenproduccion.productoNax = '"+productoNax+"', "+
                               " tbcorpal_entregasordenproduccion.codProductonax = " + codProdNax + " , "+
                               " tbcorpal_entregasordenproduccion.codresprecepcion = " + codresprecepcion + " , " +
                               " tbcorpal_entregasordenproduccion.resp_recepcion = '" + resp_recepcion + "' , " +
                               " tbcorpal_entregasordenproduccion.cod_respgra = " + cod_respgra +
                               " where "+
                               " tbcorpal_entregasordenproduccion.codigo ="+ codigo;
            return Conx.ejecutarMySql(consulta);
        }

        internal DataSet mostrarEmpregasProduccion(string turno, string respEntrega)
        {
            string consulta = "select "+
                               " aa.codigo, aa.turno, "+ 
                               " date_format( aa.fechagra, '%d/%m/%Y') as 'fecha' , "+
                               " aa.horagra as 'hora', "+
                               " aa.resp_entrega, aa.cantcajas, aa.unidadsuelta, aa.kgrdesperdicio, aa.kgrparamix, "+
                               " aa.codorden, aa.codrespentrega, aa.detalleentrega, aa.nroorden, "+
                               " aa.productoNax, aa.codProductonax, aa.codresprecepcion, aa.resp_recepcion " +
                               " from "+
                               " tbcorpal_entregasordenproduccion aa "+
                               " where "+
                               " aa.estado = 1 and "+
                               " aa.turno like '%"+turno+"%' and aa.resp_entrega like '%"+respEntrega+"%'";
            return Conx.consultaMySql(consulta);
        }

        internal bool eliminarEntregaProduccion(int codigo)
        {
            string consulta = "update tbcorpal_entregasordenproduccion set " +
                                " tbcorpal_entregasordenproduccion.estado = 0 "+
                                " where " +
                                " tbcorpal_entregasordenproduccion.codigo =" + codigo;
            return Conx.ejecutarMySql(consulta);
        }

        internal DataSet get_entregasProduccion(string fechadesde, string fechahasta, string Responsable, string producto)
        {
            string consulta = "select "+
                               " ee.codigo, "+
                               " ee.turno, "+
                               " ee.resp_entrega, "+
                               " ee.resp_recepcion, "+
                               " ee.nroorden, "+
                               " ee.productoNax, "+
                               " format(ee.cantcajas,2) as 'cantcajas', "+
                               " format(ee.unidadsuelta,2) as 'unidadsuelta', "+
                               " format(ee.kgrdesperdicio,2) as 'kgrdesperdicio', "+
                               " format(ee.kgrparamix, 2) as 'kgrparamix', "+
                               " format(( "+
                               " (ifnull(ee.cantcajas,0)* ifnull(cc.pesoporcajakgr,0)) + "+
                               " (ifnull(ee.unidadsuelta,0)*ifnull(cc.pesounidadgr,0)) "+
                               " ),2) as 'PesoTotal', "+
                               " 'Objetivo' as 'ObjetivoProduccion', "+
                               " 'Cantidad' as 'CantdeAcuerdoaPlanProduccionUnidades', "+
                               " date_format(ee.fechagra, '%d/%m/%Y') as 'fecha', "+
                               " ee.horagra as 'hora' "+
                               " from tbcorpal_entregasordenproduccion ee "+
                               " left join tbcorpal_producto cc on (ee.codProductonax = cc.codigo) "+
                               " where "+
                               " ee.estado = 1 and "+
                               " ee.resp_entrega like '%"+Responsable+"%' and "+
                               " ee.productoNax like '%"+producto+"%' and "+
                               " ee.fechagra between "+fechadesde+" and "+fechahasta;
            return Conx.consultaMySql(consulta);
        }

        internal DataSet get_ultimoInsertadoEntregaProduccion(string respEntrega)
        {
            string consulta = "select "+
                               " ee.codigo, "+
                               " date_format(ee.fechagra,'%d/%m/%Y') as 'fecha', "+
                               " ee.horagra, "+
                               " ee.turno, "+
                               " ee.resp_entrega, "+
                               " ee.resp_recepcion, "+
                               " ee.nroorden, "+
                               " ee.productoNax, "+
                               " ee.cantcajas, "+
                               " ee.unidadsuelta, "+
                               " ee.kgrdesperdicio, "+
                               " ee.kgrparamix "+
                               " from tbcorpal_entregasordenproduccion ee "+
                               " where "+
                               " ee.estado = 1 and "+
                               " ee.resp_entrega ='"+respEntrega+"' "+
                               " order by ee.codigo desc";
            return Conx.consultaMySql(consulta);
        }

        internal DataSet get_DatosEntregaProduccion(int codigoEntregaProduccion)
        {

            string consulta = "select " +
                               " ee.codigo, " +
                               " date_format(ee.fechagra,'%d/%m/%Y') as 'fecha_gra', " +
                               " ee.horagra, " +
                               " ee.turno, " +
                               " ee.resp_entrega, " +
                               " ee.resp_recepcion, " +
                               " ee.nroorden, " +
                               " ee.productoNax, " +
                               " ee.cantcajas, " +
                               " ee.unidadsuelta, " +
                               " ee.kgrdesperdicio, " +
                               " ee.kgrparamix, " +
                               " ee.codresprecepcion " +
                               " from tbcorpal_entregasordenproduccion ee " +
                               " where " +
                               " ee.estado = 1 and " +
                               " ee.codigo = " + codigoEntregaProduccion;                               
            return Conx.consultaMySql(consulta);
        }
    }
}