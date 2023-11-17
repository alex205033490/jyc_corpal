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
            string consultaD = "select " +
                               " pp.codigo, pp.producto, pp.stock, ep.cantcajas " +
                               " from tbcorpal_producto pp, " +
                               " tbcorpal_entregasordenproduccion ep " +
                               " where " +
                               " ep.codProductonax = pp.codigo and " +
                               " ep.codigo = " + codigo;
            DataSet dato = Conx.consultaMySql(consultaD);

            bool banderaResultado = false;

            if (dato.Tables[0].Rows.Count > 0)
            {
                float stock;
                float.TryParse(dato.Tables[0].Rows[0][2].ToString().Replace('.', ','), out stock);

                float cantcajas;
                float.TryParse(dato.Tables[0].Rows[0][3].ToString().Replace('.', ','), out cantcajas);

                if (cantcajas <= stock)
                {
                    string consulta0 = "update tbcorpal_producto , tbcorpal_entregasordenproduccion " +
                                       " set tbcorpal_producto.stock = (tbcorpal_producto.stock - tbcorpal_entregasordenproduccion.cantcajas)" +
                                       " where " +
                                       " tbcorpal_producto.codigo = tbcorpal_entregasordenproduccion.codProductonax and " +
                        //" tbcorpal_entregasordenproduccion.cantcajas <= tbcorpal_producto.stock and "+
                                       " tbcorpal_entregasordenproduccion.codigo = " + codigo;
                    bool bandera = Conx.ejecutarMySql(consulta0);

                    if (bandera)
                    {
                        string consulta = "update tbcorpal_entregasordenproduccion set " +
                                        " tbcorpal_entregasordenproduccion.estado = 0 " +
                                        " where " +
                                        " tbcorpal_entregasordenproduccion.codigo =" + codigo;
                        banderaResultado = Conx.ejecutarMySql(consulta);
                    }
                    return banderaResultado;
                }
                else
                    return banderaResultado;

            }
            else
                return banderaResultado;


            
        }

        internal DataSet get_entregasProduccion(string fechadesde, string fechahasta, string Responsable, string producto)
        {
            string consulta = "select "+
                               " ee.codigo, "+
                               " ee.turno, "+
                               " ee.resp_entrega, "+
                               " ee.resp_recepcion, "+
                               " ee.codigo as 'nroorden', " +
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
                               " ee.horagra as 'hora', "+
                               " ee.detalleentrega, "+
                               " CASE DAYOFWEEK(ee.fechagra) "+
                               "    WHEN 1 THEN 'Domingo' "+
                               "    WHEN 2 THEN 'Lunes' "+
                               "    WHEN 3 THEN 'Martes' "+
                               "    WHEN 4 THEN 'Miércoles' "+
                               "    WHEN 5 THEN 'Jueves' "+
                               "    WHEN 6 THEN 'Viernes' "+
                               "    WHEN 7 THEN 'Sábado' "+
                               "  END AS 'Dia' " +
                               " from tbcorpal_entregasordenproduccion ee "+
                               " left join tbcorpal_producto cc on (ee.codProductonax = cc.codigo) "+
                               " where "+
                               " cc.estado = 1 and "+
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

        internal bool set_objetivoProduccion(string fechalimite, int codprod, string producto, float cantidadprod,
                                           string medida, string detalle, int codusergra, string respgra)
        {
            string consulta = "insert into tbcorpal_objetivosproduccion("+
                               " tbcorpal_objetivosproduccion.fechagra,tbcorpal_objetivosproduccion.horagra, "+
                               " tbcorpal_objetivosproduccion.fechalimite,tbcorpal_objetivosproduccion.codprod, "+
                               " tbcorpal_objetivosproduccion.producto,tbcorpal_objetivosproduccion.cantidadprod, "+
                               " tbcorpal_objetivosproduccion.medida,tbcorpal_objetivosproduccion.detalle, "+
                               " tbcorpal_objetivosproduccion.codusergra,tbcorpal_objetivosproduccion.respgra, tbcorpal_objetivosproduccion.estado) " +
                               " values( "+
                               " current_date(), current_time(), "+
                                fechalimite+", "+codprod+", "+
                               " '"+producto+"', "+cantidadprod.ToString().Replace(',','.')+", "+
                               " '"+medida+"','"+detalle+"', "+
                                codusergra+ " , '"+respgra+"',1)";
            return Conx.ejecutarMySql(consulta);
        }

        internal bool update_objetivoProduccion(int codigo, string fechalimite, int codprod, string producto, float cantidadprod, string medida, string detalle, int codusergra, string respgra)
        {
            string consulta = "update tbcorpal_objetivosproduccion "+
                               " set "+
                               " tbcorpal_objetivosproduccion.fechalimite = "+fechalimite+", "+
                               " tbcorpal_objetivosproduccion.codprod = "+codprod+", "+
                               " tbcorpal_objetivosproduccion.producto = '"+producto+"', "+
                               " tbcorpal_objetivosproduccion.cantidadprod = "+cantidadprod+", "+
                               " tbcorpal_objetivosproduccion.medida = '"+medida+"', "+
                               " tbcorpal_objetivosproduccion.detalle = '"+detalle+"', "+
                               " tbcorpal_objetivosproduccion.codusergra = "+codusergra+", "+
                               " tbcorpal_objetivosproduccion.respgra = '"+respgra +"' "+
                               " where "+
                               " tbcorpal_objetivosproduccion.codigo = " +codigo;
            return Conx.ejecutarMySql(consulta);
        }

        internal bool delete_objetivoProduccion(int codigo, int codUser)
        {
            string consulta = "update tbcorpal_objetivosproduccion "+
                               " set "+
                               " tbcorpal_objetivosproduccion.estado = false "+
                               " where "+
                               " tbcorpal_objetivosproduccion.codigo = " + codigo;
            return Conx.ejecutarMySql(consulta);
        }

        internal DataSet get_objetivosDeProduccion(string fechalimite, string producto)
        {
            string consulta = "Select "+ 
                               " op.codigo, "+
                               " date_format(op.fechalimite, '%d/%m/%Y') as 'fechaLimite', "+
                               " op.codprod, "+
                               " op.producto, "+
                               " op.cantidadprod, "+
                               " pp.stock as 'stock_Almacen', "+
                               " (ifnull(pp.stock,0) - ifnull(op.cantidadprod,0)) as 'cant_CompletarObjetivo', " +
                               " op.medida, "+
                               " op.detalle "+
                               " from tbcorpal_objetivosproduccion op, tbcorpal_producto pp "+
                               " where "+
                               " op.estado = 1 and "+
                               " op.codprod = pp.codigo and "+                               
                               " op.producto like '%%' ";

            if(string.IsNullOrEmpty(fechalimite) == false){
                consulta = consulta + " and op.fechalimite = " + fechalimite;
            }

            return Conx.consultaMySql(consulta);
        }
    }
}