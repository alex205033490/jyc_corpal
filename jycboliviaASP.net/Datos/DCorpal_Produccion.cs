﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;
using jycboliviaASP.net.Presentacion;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_Produccion
    {
        private conexionMySql Conx = new conexionMySql();
        public DCorpal_Produccion() { }



        internal bool insertarEntregaProduccion(string nroorden, string turno, int codusuario, string respEntrega, float cantcajas, float unidadsuelta, float kgrdesperdicio, float kgrparamix, string detalleentrega, int codProdNax, string productoNax, int codresprecepcion, string resp_recepcion, int cod_respgra, float kgrdesperdicio_conaceite, float kgrdesperdicio_sinaceite, float pack_ferial, string medidaentregada, string medidapackferial, decimal kgrdesperdiciobobina)
        {
            string consulta = "insert into tbcorpal_entregasordenproduccion( "+
                               " fechagra,horagra,turno,resp_entrega, "+
                               " cantcajas,unidadsuelta,kgrdesperdicio,kgrparamix, "+
                               " codorden,codrespentrega,detalleentrega,nroorden, "+
                               " productoNax,codProductonax, estado,codresprecepcion,resp_recepcion,cod_respgra, kgrdesperdicio_conaceite, kgrdesperdicio_sinaceite , pack_ferial," +
                               " medidaentregada, medidapackferial, kgrdesperdiciobobina " +
                               " ) values( "+
                               " current_date(), current_time(), '" + turno + "', '" + respEntrega + "', " +
                               " '"+cantcajas.ToString().Replace(',','.')+"', '"+unidadsuelta.ToString().Replace(',','.')+"', '"+kgrdesperdicio.ToString().Replace(',','.')+"', '"+kgrparamix.ToString().Replace(',','.')+"', "+
                               " null, " + codusuario + ", '" + detalleentrega + "', '" + nroorden + "', " +
                               " '" + productoNax + "', " + codProdNax + ", 1, " + codresprecepcion + ",'" + resp_recepcion + "'," + cod_respgra + ", '" + kgrdesperdicio_conaceite.ToString().Replace(',', '.') + "', '" + kgrdesperdicio_sinaceite.ToString().Replace(',', '.') + "','"+ pack_ferial.ToString().Replace(',', '.') + "'," +
                               "'"+medidaentregada+"', '"+medidapackferial+"', '"+ kgrdesperdiciobobina.ToString().Replace(',', '.') + "')";
            return Conx.ejecutarMySql(consulta);
        }

        internal bool modificarEntregaProduccion(int codigo, string nroorden, string turno, int codusuario, string respEntrega, float cantcajas, float unidadsuelta, float kgrdesperdicio, float kgrparamix, string detalleentrega, int codProdNax, string productoNax, int codresprecepcion, string resp_recepcion, int cod_respgra, float kgrdesperdicio_sinaceite, float kgrdesperdicio_conaceite, float pack_ferial, string medidaentregada, string medidapackferial, decimal kgrdesperdiciobobina)
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
                               " tbcorpal_entregasordenproduccion.cod_respgra = " + cod_respgra +", "+
                               " tbcorpal_entregasordenproduccion.kgrdesperdicio_sinaceite = '"+ kgrdesperdicio_sinaceite.ToString().Replace(',', '.') + "', " +
                               " tbcorpal_entregasordenproduccion.kgrdesperdicio_conaceite = '" + kgrdesperdicio_conaceite.ToString().Replace(',', '.') + "', " +
                               " tbcorpal_entregasordenproduccion.pack_ferial = '" + pack_ferial.ToString().Replace(',', '.') + "', " +
                               " tbcorpal_entregasordenproduccion.medidaentregada = '"+ medidaentregada + "', " +
                               " tbcorpal_entregasordenproduccion.medidapackferial = '"+ medidapackferial + "', " +
                               " tbcorpal_entregasordenproduccion.kgrdesperdiciobobina = '"+ kgrdesperdiciobobina.ToString().Replace(',', '.') + "' "+
                               " where " +
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
                               " aa.productoNax, aa.codProductonax, aa.codresprecepcion, aa.resp_recepcion, " +
                               " aa.kgrdesperdicio_conaceite, aa.kgrdesperdicio_sinaceite, aa.pack_ferial, aa.kgrdesperdiciobobina " +
                               " from "+
                               " tbcorpal_entregasordenproduccion aa "+
                               " where "+
                               " aa.estado = 1 and "+
                               " aa.turno like '%"+turno+"%' and aa.resp_entrega like '%"+respEntrega+"%' "+
                               " order by aa.codigo desc ";
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
                               "  END AS 'Dia', " +
                               " CASE " +
                               " WHEN HOUR(ee.horagra) BETWEEN 0 AND 6 THEN date_format(date_add(ee.fechagra, INTERVAL -1 DAY),'%d/%m/%Y') " +
                               " ELSE date_format(ee.fechagra,'%d/%m/%Y') " +
                               " END AS 'FechaSistema', " +
                               " CASE " +
                               " WHEN HOUR(ee.horagra) BETWEEN 7 AND 15 THEN 'Mañana' " +
                               " WHEN HOUR(ee.horagra) BETWEEN 16 AND 23 THEN 'Tarde' " +
                               " WHEN HOUR(ee.horagra) BETWEEN 0 AND 6 THEN 'Noche' " +
                               " ELSE 'Noche' " +
                               " END AS 'TurnoSistema' "+

                               " from tbcorpal_entregasordenproduccion ee " +
                               " left join tbcorpal_producto cc on (ee.codProductonax = cc.codigo) "+
                               " where "+
                               " cc.estado = 1 and "+
                               " ee.estado = 1 and "+
                               " ee.resp_entrega like '%"+Responsable+"%' and "+
                               " ee.productoNax like '%"+producto+"%' and "+
                               " ee.fechagra between "+fechadesde+" and "+fechahasta +
                               " order by TIMESTAMP (ee.fechagra,ee.horagra) desc";
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
                               " ee.codresprecepcion, " +
                               " ee.medidaentregada,"+
                               " ee.kgrdesperdicio_conaceite,"+                               
                               " ee.kgrdesperdicio_sinaceite,"+
                               " ee.pack_ferial "+
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
            string consultaStock = "SELECT pp.codigo, pp.producto, pp.medida, "+
                                   " ifnull(t1.ingreso,0) as 'Ingreso1', "+
                                   " ifnull(t2.salida,0) as 'Salida1', "+
                                   " (ifnull(t1.ingreso,0) - ifnull(t2.salida,0)) as 'StockAlmacen' "+ 
                                   " FROM tbcorpal_producto pp "+
                                   " LEFT JOIN "+
                                   " ( "+
                                   " select "+
                                   " oo.codProductonax, sum(oo.cantcajas) as 'ingreso' "+   
                                   " from tbcorpal_entregasordenproduccion oo "+
                                   " where "+
                                   " oo.estado = 1 and "+
                                   " oo.fechagra between "+NA_VariablesGlobales.fechaInicialProduccion+" and current_date() "+
                                   " group by oo.codProductonax "+
                                   " ) as t1  ON pp.codigo = t1.codProductonax "+
                                   " LEFT JOIN "+
                                   " ( "+
                                   " select dss.codproducto, sum(dss.cantentregada) as 'salida' "+
                                   " from tbcorpal_solicitudentregaproducto ss, "+
                                   " tbcorpal_detalle_solicitudproducto dss "+
                                   " where "+
                                   " ss.codigo = dss.codsolicitud and "+
                                   " ss.estado = 1 and "+
                                   " ss.estadosolicitud = 'Cerrado' and "+
                                   " ss.fechacierre between "+NA_VariablesGlobales.fechaInicialProduccion+" and current_date() "+
                                   " group by dss.codproducto "+
                                   " ) as t2 ON pp.codigo = t2.codproducto "+
                                   " WHERE "+
                                   " pp.estado = 1 ";

            string consulta = "Select "+ 
                               " op.codigo, "+
                               " date_format(op.fechalimite, '%d/%m/%Y') as 'fechaLimite', "+
                               " op.codprod, "+
                               " op.producto, "+
                               " op.cantidadprod, "+
                               " pp.StockAlmacen as 'stock_Almacen', " +
                               " if( "+
                               " (ifnull(pp.StockAlmacen,0) - ifnull(op.cantidadprod,0)) <= 0, " +
                               " ((ifnull(pp.StockAlmacen,0) - ifnull(op.cantidadprod,0)) * -1), 0 " +
                               " ) as 'cant_CompletarObjetivo', " +
                               " op.medida, "+
                               " op.detalle "+
                               " from tbcorpal_objetivosproduccion op, ("+consultaStock+") as pp "+
                               " where "+
                               " op.estado = 1 and "+
                               " op.codprod = pp.codigo and "+
                               " op.producto like '%"+producto+"%'";

            if(string.IsNullOrEmpty(fechalimite) == false){
                consulta = consulta + " and op.fechalimite <= " + fechalimite;              
            }            

            return Conx.consultaMySql(consulta);
        }

        internal DataSet get_devolucionProductos(string vendedor, string producto)
        {
            string consulta = "select "+
                               " dd.codigo, "+
                               " date_format(dd.fechadevolucion,'%d/%m/%Y') as 'fecha_devolucion', dd.vendedor, "+
                               " dd.producto, dd.medida, dd.cantidad, dd.almacenerorecibe, dd.motivodevolucion, dd.seenviaa, "+
                               " dd.observacionesdevolucion "+ 
                               " from tbcorpal_devolucionproducto dd "+
                               " where "+
                               " dd.estado = true and "+
                               " dd.vendedor like '%"+vendedor+"%' and "+
                               " dd.producto like '%"+producto+"%'";
            return Conx.consultaMySql(consulta);
        }

        internal bool insertarDevolucionProduccion(string fechadevolucion, string vendedor, int codvendedor, string producto, int codproducto, float cantidad, string almacenerorecibe, string motivodevolucion, string seenviaa, string observacionesdevolucion, string medida)
        {
            string consulta = "insert into tbcorpal_devolucionproducto( "+
                               " fechagra, horagra, estado, " +
                               " fechadevolucion,vendedor,codvendedor, "+
                               " producto, codproducto,cantidad, almacenerorecibe, motivodevolucion, seenviaa, "+
                               " observacionesdevolucion, medida) " +
                               " VALUES (current_date(), current_time(), 1, " + fechadevolucion + ",'" + vendedor + "'," + codvendedor + ", " +
                               " '"+producto+"', "+codproducto+",'"+cantidad.ToString().Replace(',','.')+"', '"+almacenerorecibe+"', '"+motivodevolucion+"', '"+seenviaa+"', "+
                               " '" + observacionesdevolucion + "', '"+medida+"')";
            return Conx.ejecutarMySql(consulta);
        }

        internal bool modificarDevolucionProduccion(int codigoD, string fechadevolucion, string vendedor, int codvendedor, string producto, int codproducto, float cantidad, string almacenerorecibe, string motivodevolucion, string seenviaa, string observacionesdevolucion, string medida)
        {
            string consulta = "update tbcorpal_devolucionproducto set " +
                              " tbcorpal_devolucionproducto.fechadevolucion = "+fechadevolucion+" , "+
                              " tbcorpal_devolucionproducto.vendedor = '"+vendedor+"', "+
                              " tbcorpal_devolucionproducto.codvendedor = "+codvendedor+" , " +
                              " tbcorpal_devolucionproducto.producto = '"+producto+"', "+
                              " tbcorpal_devolucionproducto.codproducto = "+codproducto+", "+
                              " tbcorpal_devolucionproducto.cantidad = '"+cantidad.ToString().Replace(',','.')+"', "+
                              " tbcorpal_devolucionproducto.almacenerorecibe = '"+almacenerorecibe+"' , "+
                              " tbcorpal_devolucionproducto.motivodevolucion = '"+motivodevolucion+"', "+
                              " tbcorpal_devolucionproducto.seenviaa = '"+seenviaa+"' ," +
                              " tbcorpal_devolucionproducto.medida = '" + medida + "' ," +
                              " tbcorpal_devolucionproducto.observacionesdevolucion = '"+observacionesdevolucion+"'" +
                              " where tbcorpal_devolucionproducto.codigo = "+codigoD;
            return Conx.ejecutarMySql(consulta);
        }

        internal bool eliminarDevolucionProduccion(int codigoD)
        {
            string consulta = "update tbcorpal_devolucionproducto set " +
                             " tbcorpal_devolucionproducto.estado = false " +                             
                             " where tbcorpal_devolucionproducto.codigo = " + codigoD;
            return Conx.ejecutarMySql(consulta);
        }

        internal DataSet get_devolucionProductosNoAprovados(string fechaDevolucion, string Producto)
        {
            string consulta = "select "+
                               " dd.autorizado," +
                               " dd.codigo, "+ 
                               " date_format(dd.fechadevolucion,'%d%/%m/%Y') as 'fecha_devolucion', "+
                               " dd.vendedor, "+
                               " dd.producto, "+
                               " dd.cantidad, "+
                               " dd.medida, "+
                               " dd.almacenerorecibe, "+
                               " dd.motivodevolucion, "+
                               " dd.seenviaa "+
                               " from "+
                               " tbcorpal_devolucionproducto dd "+
                               " where "+
                               " dd.codrespautoriza is null and " +
                               " dd.autorizadopor is null and "+
                               " dd.autorizado is null and "+
                               " dd.producto like '%"+Producto+"%' ";
            if(!string.IsNullOrEmpty(fechaDevolucion) && !fechaDevolucion.Equals("null")){
                consulta = consulta + " and dd.fechadevolucion = " + fechaDevolucion;
            }

            return Conx.consultaMySql(consulta);
        }

        internal bool set_objetivoProduccion(int codigoDevolucion, bool marcado, int codUserAutorizado, string nombreAutorizacion)
        {
            string consulta = "update tbcorpal_devolucionproducto set " +
                              " tbcorpal_devolucionproducto.codrespautoriza = " + codUserAutorizado + " , " +
                              " tbcorpal_devolucionproducto.autorizadopor = '" + nombreAutorizacion + "', " +
                              " tbcorpal_devolucionproducto.autorizado = " + marcado +
                              " where tbcorpal_devolucionproducto.codigo = " + codigoDevolucion;
            return Conx.ejecutarMySql(consulta);
        }

        internal DataSet get_objetivosDeProduccionMensual(int Mes, int anio, string producto)
        {
            string consulta = "select "+
                                " oo.codigo, "+
                                " date_format(oo.fechagra,'%d/%m/%Y') as 'Fecha_Gra', "+ 
                                " oo.horagra, "+
                                " oo.mes_texto as 'mes', "+
                                " oo.anio, "+
                                " oo.producto, "+
                                " oo.cantidadprod, "+
                                " oo.medida, "+
                                " oo.detalle, "+
                                " oo.respgra as 'Resp.Grabacion', "+
                                " oo.estado "+
                                " from "+ 
                                " tbcorpal_objetivosproduccionmensual oo "+
                                " where "+                               
                                " oo.producto like '%"+producto+"%' and oo.estado = 1 ";
            if (Mes > 0 && anio > 0) {
                consulta = consulta + " and oo.mes = " + Mes + " and  oo.anio = " + anio;
            }
            return Conx.consultaMySql(consulta);
        }

        internal bool update_objetivoProduccionMensual(int  codigo,int codMes, string mesTexto, int anio, int codprod, string producto, float cantidadprod, string medida, string detalle, int codusergra, string respgra)
        {
            string consulta = "update " +
                               " tbcorpal_objetivosproduccionmensual set " +
                               " tbcorpal_objetivosproduccionmensual.fechagra = current_date(), " +
                               " tbcorpal_objetivosproduccionmensual.horagra = current_time(), " +
                               " tbcorpal_objetivosproduccionmensual.mes = " + codMes + ", " +
                               " tbcorpal_objetivosproduccionmensual.anio = " + anio + ", " +
                               " tbcorpal_objetivosproduccionmensual.codprod = " + codprod + ", " +
                               " tbcorpal_objetivosproduccionmensual.producto = '" + producto + "', " +
                               " tbcorpal_objetivosproduccionmensual.cantidadprod = " + cantidadprod + ", " +
                               " tbcorpal_objetivosproduccionmensual.medida = '" + medida + "', " +
                               " tbcorpal_objetivosproduccionmensual.detalle = '" + detalle + "', " +
                               " tbcorpal_objetivosproduccionmensual.codusergra = " + codusergra + ", " +
                               " tbcorpal_objetivosproduccionmensual.respgra = '" + respgra + "', " +
                               " tbcorpal_objetivosproduccionmensual.estado = 1, " +
                               " tbcorpal_objetivosproduccionmensual.mes_texto = '" + mesTexto + "' " +
                               " where " +
                               " tbcorpal_objetivosproduccionmensual.codigo = " + codigo;
            return Conx.ejecutarMySql(consulta);
        }

        internal bool set_objetivoProduccionMensual( int codMes, string mesTexto, int anio, int codprod, string producto, float cantidadprod, string medida, string detalle, int codusergra, string respgra)
        {
            string consulta = "insert into tbcorpal_objetivosproduccionmensual( "+
                               " fechagra,horagra,mes,anio,codprod,producto,cantidadprod,medida, "+
                               " detalle,codusergra,respgra,estado,mes_texto) "+
                               " values(current_date(),current_time(),"+codMes+","+anio+","+codprod+",'"+producto+"',"+cantidadprod+",'"+medida+"', " +
                               " '"+detalle+"',"+codusergra+",'"+respgra+"',1,'"+mesTexto+"')";
            return Conx.ejecutarMySql(consulta);
        }

        internal bool delete_objetivoProduccionMensual(int codigo, int codUser)
        {
            string consulta = "update tbcorpal_objetivosproduccionmensual set " +
                          " tbcorpal_objetivosproduccionmensual.codusergra = "+codUser+"," +
                          " tbcorpal_objetivosproduccionmensual.estado = false " +
                          " where tbcorpal_objetivosproduccionmensual.codigo = " + codigo;
            return Conx.ejecutarMySql(consulta);
        }

        internal DataSet get_datosOrdenProduccion(string Producto)
        {
            string consulta = "select "+
                              " oo.codigo, "+  
                              " date_format(oo.fechaproduccion,'%d/%m/%Y') as 'fecha_produccion', "+
                              " oo.horaproduccion, "+
                              " oo.productoNax, "+
                              " oo.cantcajasproduccion, "+
                              " oo.medida, "+  
                              " oo.detalleproduccion, "+
                              " oo.responsable, "+
                              " oo.cantturnodia," +
                              " oo.cantturnotarde," +
                              " oo.cantturnonoche " +
                              " from " +
                              " tbcorpal_ordenproduccion oo "+
                              " where "+
                              " oo.estado = 1 and "+
                              " oo.productoNax like '%"+Producto+"%' order by oo.codigo desc";
            return Conx.consultaMySql(consulta);
        }

        internal DataSet get_datosOrdenProduccion(int codigoOrden)
        {
            string consulta = "select " +
                              " oo.codigo, " +
                              " date_format(oo.fechaproduccion,'%d/%m/%Y') as 'fecha_produccion', " +
                              " oo.horaproduccion, " +
                              " oo.productoNax, " +
                              " oo.cantcajasproduccion, " +
                              " oo.medida, " +
                              " oo.detalleproduccion, " +
                              " oo.responsable, " +
                              " oo.cantturnodia," +
                              " oo.cantturnotarde," +
                              " oo.cantturnonoche, " +
                              " oo.codProductonax "+
                              " from " +
                              " tbcorpal_ordenproduccion oo " +
                              " where " +
                              " oo.estado = 1 and " +
                              " oo.codigo = "+codigoOrden+" order by oo.codigo desc";
            return Conx.consultaMySql(consulta);
        }

        internal bool insertarOrdenProduccion( string fechaproduccion,int codProductonax,string productoNax,float cantcajasproduccion,
                                               string medida,string detalleproduccion,int cod_respgra,string responsable,
                                               float cantturnodia, float cantturnotarde, float cantturnonoche)
        {
            string consulta = "insert into tbcorpal_ordenproduccion( "+
                                " fechagra, "+
                                " horagra, "+
                                " fechaproduccion, "+
                                " horaproduccion, "+
                                " codProductonax, "+
                                " productoNax, "+
                                " cantcajasproduccion, "+
                                " medida, "+  
                                " detalleproduccion, "+
                                " estado, "+
                                " estadoorden, "+
                                " cod_respgra, "+
                                " responsable, " +
                                " cantturnodia, "+
                                " cantturnotarde, "+
                                " cantturnonoche) " +
                                " values( "+
                                " current_date(), "+
                                " current_time(), "+
                                 fechaproduccion+", "+
                                " current_time(), "+
                                 codProductonax+", "+
                                " '"+productoNax+"', "+
                                 cantcajasproduccion.ToString().Replace(',','.')+", "+
                                " '"+medida+"',  "+
                                " '"+detalleproduccion+"', "+
                                " 1, "+
                                " 'Abierto', "+
                                 cod_respgra+", "+
                                " '"+responsable+"',"+
                                "'"+cantturnodia.ToString().Replace(',', '.') + "', "+
                                "'"+cantturnotarde.ToString().Replace(',', '.') + "', "+
                                "'"+cantturnonoche.ToString().Replace(',', '.') + "')";
            return Conx.ejecutarMySql (consulta);
        }

        internal bool eliminar_ordenProduccion(int codigoOrden)
        {
            string consulta = "update tbcorpal_ordenproduccion set " +
                               " tbcorpal_ordenproduccion.estado = 0, " +
                               " tbcorpal_ordenproduccion.estadoorden = 'Eliminado' " +
                               " where tbcorpal_ordenproduccion.codigo = "+codigoOrden;
            return Conx.ejecutarMySql(consulta);
        }

        internal bool modificarOrdenProduccion(int codigoOrden, string fechaProduccion, int codProducto, string producto, float cantcajas, string medidaProduccion, string detalleProduccion, int codUser, string responsable, float cantturnodia, float cantturnotarde, float cantturnonoche)
        {
            string consulta = "update tbcorpal_ordenproduccion set " +
                                " tbcorpal_ordenproduccion.fechaproduccion = "+fechaProduccion+", " +
                                " tbcorpal_ordenproduccion.codProductonax = "+codProducto+", " +
                                " tbcorpal_ordenproduccion.productoNax = '"+producto+"', " +
                                " tbcorpal_ordenproduccion.cantcajasproduccion = "+cantcajas+", " +
                                " tbcorpal_ordenproduccion.medida = '"+medidaProduccion+"', " +
                                " tbcorpal_ordenproduccion.detalleproduccion = '"+detalleProduccion+"', " +
                                " tbcorpal_ordenproduccion.cod_respgra = "+codUser+", " +
                                " tbcorpal_ordenproduccion.responsable= '"+responsable+"', " +
                                " tbcorpal_ordenproduccion.cantturnodia= '"+ cantturnodia.ToString().Replace(',', '.') + "', "+
                                " tbcorpal_ordenproduccion.cantturnotarde= '"+ cantturnotarde.ToString().Replace(',', '.') + "', "+ 
                                " tbcorpal_ordenproduccion.cantturnonoche= '"+ cantturnonoche.ToString().Replace(',', '.') + "' "+
                                " where tbcorpal_ordenproduccion.codigo = " +codigoOrden;
                                
            return Conx.ejecutarMySql(consulta);
        }

        internal DataSet get_codigoCorrelativoOrdenProduccion()
        {
            string consulta = "select " +
                            " max(oo.codigo) " +                          
                             " from " +
                             " tbcorpal_ordenproduccion oo " ;
            return Conx.consultaMySql(consulta);
        }

        internal DataSet get_datosEntregaProduccionFechaTurno(string fechadesde, string fechahasta, string producto)
        {
            string consulta = "select pp.codigo, pp.producto, " +
                "date_format(t1.FechaSistema,'%d/%m/%Y') as 'FechaTurno', " +
                "pp.medida,  " +
               " sum(ifnull(t1.TurnoDia, 0)) as 'TurnoDia1', "+
               " sum(ifnull(t1.TurnoTarde, 0)) as 'TurnoTarde1', "+
               " sum(ifnull(t1.TurnoNoche, 0)) as 'TurnoNoche1', "+
               " sum(t1.CantCajas1) as 'CantCajas1' , "+
               " sum(t1.CantSuelta) as 'CantSuelta', "+
               " FORMAT(sum(t1.PackFerial), 2) AS 'PackFerial1', "+
               " FORMAT( "+
               " ((sum(t1.CantCajas1) * pp.pesoporcajakgr) + (sum(t1.CantSuelta) * (pp.pesounidadgr/1000)) + (sum(t1.PackFerial) * (pp.pesounidadgr/1000))+ t1.kgrdesperdicio_conaceite+t1.kgrdesperdicio_sinaceite) " +
               " , 2) as 'Total_Kgr', "+
               " sum(t1.kgrdesperdicio_conaceite) as 'kgrdesperdicioconaceite' , sum(t1.kgrdesperdicio_sinaceite) as 'kgrdesperdiciosinaceite' "+
                "FROM tbcorpal_producto pp " +
                "LEFT JOIN ( " +
                "select " +
                "eo.codProductonax, eo.productoNax,  " +
                "eo.fechagra, " +
                "CASE  " +
                "WHEN HOUR(eo.horagra) BETWEEN 0 AND 6 THEN date_add(eo.fechagra, INTERVAL -1 DAY)  " +
                "ELSE eo.fechagra  " +
                "END AS 'FechaSistema', eo.horagra, " +
                "CASE    " +
                "WHEN HOUR(eo.horagra) BETWEEN 7 AND 15 THEN sum(ifnull(eo.cantcajas,0))   " +
                "END AS 'TurnoDia', " +
                "CASE   " +
                "WHEN HOUR(eo.horagra) BETWEEN 16 AND 23 THEN sum(ifnull(eo.cantcajas,0)) " +
                "END AS 'TurnoTarde', " +
                "CASE   " +
                "WHEN HOUR(eo.horagra) BETWEEN 0 AND 6 THEN sum(ifnull(eo.cantcajas,0)) " +
                "END as 'TurnoNoche', " +
                "sum(ifnull(eo.cantcajas,0)) as 'CantCajas1',  " +
                "sum(ifnull(eo.unidadsuelta,0)) as 'CantSuelta',  " +
                "sum(ifnull(eo.pack_ferial,0)) as 'PackFerial' " +
                ", sum(eo.kgrdesperdicio_conaceite) as 'kgrdesperdicio_conaceite' " +
                ", sum(eo.kgrdesperdicio_sinaceite) as 'kgrdesperdicio_sinaceite' "+
                "from  " +
                "tbcorpal_entregasordenproduccion eo " +
                "where " +
                "eo.estado = 1 and " +
                "eo.fechagra between "+fechadesde+" and " + fechahasta +
                "group by eo.fechagra, eo.codProductonax ) as t1 ON pp.codigo = t1.codProductonax " +
                "WHERE " +                
                "pp.estado = 1 " +
                " and t1.FechaSistema is not null "+
                "and pp.producto like '%" +producto+"%' " +
                " group by  t1.FechaSistema, pp.codigo " +
                "order by t1.FechaSistema, pp.codigo asc";
            return Conx.consultaMySql(consulta);
        }

        internal DataSet get_codigoOrdenProduccionUltimoInsertado(int codProducto, int codUser)
        {
            string consulta = "select " +
                            " max(oo.codigo) " +                            
                            " from " +
                            " tbcorpal_ordenproduccion oo " +
                            " where " +
                            " oo.estado = 1 and " +
                            " oo.codProductonax = " + codProducto + " and oo.cod_respgra = "+ codUser;
            return Conx.consultaMySql(consulta);
        }

        internal DataSet get_insumosporProductoNormal(int codNax, string nameProd, decimal cantidadNecesitada)
        {
            string consulta = " select ii.codigo,ii.nombre as 'insumo'," +
                            " (di.cantidad * '" + cantidadNecesitada.ToString().Replace(',', '.') + "') as 'cantidad'," +
                            " ii.medida,'" + codNax + "' as 'codigonax'," +
                            " '" + nameProd + "' as 'productonax' " +
                            " from tbcorpal_receta re, " +
                            " tbcorpal_detingredienteinsumocreado di," +
                            " tbcorpal_insumoscreados ii " +
                            " where re.codigo = di.codreceta and " +
                            " di.codinsumocreado = ii.codigo and " +
                            " ii.estado = 1  and " +
                            " re.estado = 1 and " +
                            " re.codproducto = " + codNax +
                            " UNION " +
                            "select ii.codigo,ii.nombre as 'insumo', " +
                            "(di.cantidad * '" + cantidadNecesitada.ToString().Replace(',', '.') + "') as 'cantidad'," +
                            " ii.Medida,'" + codNax + "' as 'codigonax'," +
                            " '" + nameProd + "' as 'productonax' " +
                            " from tbcorpal_receta re, " +
                            " tbcorpal_detingredienteinsumo di," +
                            " tbcorpal_insumo ii " +
                            " where re.codigo = di.codreceta and " +
                            " di.codinsumo = ii.codigo and " +
                            " ii.estado = 1  and re.estado = 1 and " +
                            " re.codproducto = " + codNax;                            
                            
            return Conx.consultaMySql(consulta);
        }

        internal DataSet get_insumosCreadosporProducto(int codNax, decimal cantidadNecesitada)
        {
            string consulta = "select ic.codigo,ic.nombre," +
                " (dic.cantidad * '"+cantidadNecesitada.ToString().Replace(',','.')+"') as 'cantidad'" +
                " from tbcorpal_receta re,tbcorpal_detingredienteinsumocreado dic, " +
                " tbcorpal_insumoscreados ic" +
                " where re.codigo = dic.codreceta and " +
                " dic.codinsumocreado = ic.codigo and re.estado = 1 and " +
                " re.codproducto = " + codNax;
            return Conx.consultaMySql(consulta);
        }

        public DataSet get_consultaMySql(string consulta) {            
            return Conx.consultaMySql(consulta);
        }

        internal DataSet get_tieneRecetaProducto(int codProducto)
        {
            string consulta = "select * from tbcorpal_receta re where re.codproducto = "+codProducto;
            return Conx.consultaMySql(consulta);
        }
    }
}