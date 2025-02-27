using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_EntregaSolicitudProducto2
    {
        private conexionMySql conexion = new conexionMySql();

        // LISTA DE TODOS LOS REGISTROS 
        internal DataSet get_VWRegistrosEntregaSolicitudProductos(string estadoSolicitud)
        {
            NA_VariablesGlobales negocio = new NA_VariablesGlobales();
            string consultaStock = negocio.get_consultaStockProductosActual();

            string consulta = "SELECT CONCAT(COALESCE(v.marca, ''), ' ', COALESCE(v.modelo, '')) AS 'Vehiculo', " +
                "v.placa, v.conductor, sep.codigo, sep.nroboleta, sep.personalsolicitud, dsp.codproducto, p.producto, " +
                "date_format(sep.fechaentrega, '%d/%m/%Y') as 'fechaentrega', sep.horaentrega, sep.estadosolicitud, dsp.tiposolicitud, " +
                "dsp.cant as 'cantSolicitada', ifnull(dsp.cantentregada, 0) as 'cantEntregada', " +
                "CASE dsp.tiposolicitud WHEN 'ITEM PACK FERIAL' THEN ifnull(pp.StockPackFerial, 0) " +
                "ELSE ifnull(pp.StockAlmacen, 0) END AS 'StockAlmacen' " +
                "from tbcorpal_solicitudentregaproducto sep " +
                "left join tbcorpal_detalle_solicitudproducto dsp ON sep.codigo = dsp.codsolicitud " +
                "left join tbcorpal_vehiculos v ON dsp.codvehiculo = v.codigo " +
                "left join tbcorpal_producto p ON dsp.codproducto = p.codigo " +
                "left join (" +consultaStock+ ") as pp on dsp.codproducto = pp.codigo " +
                "WHERE sep.estadosolicitud = '"+estadoSolicitud+"' and sep.estado = true " +
                "order by v.marca, v.modelo asc";

            return conexion.consultaMySql(consulta);
        }

        // LISTA DE REGISTROS POR VEHICULO
        internal DataSet get_VWRegistrosEntregaSolicitudProductoXCamion(string estadoSolicitud, int codVehiculo)
        {
            NA_VariablesGlobales negocio = new NA_VariablesGlobales();
            string consultaStock = negocio.get_consultaStockProductosActual();

            string consulta = "SELECT CONCAT(COALESCE(v.marca, ''), ' ', COALESCE(v.modelo, '')) AS 'Vehiculo', " +
                "v.placa, v.conductor, sep.codigo, sep.nroboleta, sep.personalsolicitud, dsp.codproducto, p.producto, " +
                "date_format(sep.fechaentrega, '%d/%m/%Y') as 'fechaentrega', sep.horaentrega, sep.estadosolicitud, dsp.tiposolicitud, " +
                "dsp.cant as 'cantSolicitada', ifnull(dsp.cantentregada, 0) as 'cantEntregada', " +
                "CASE dsp.tiposolicitud WHEN 'ITEM PACK FERIAL' THEN ifnull(pp.StockPackFerial, 0) " +
                "ELSE ifnull(pp.StockAlmacen, 0) END AS 'StockAlmacen' " +
                "from tbcorpal_solicitudentregaproducto sep " +
                "left join tbcorpal_detalle_solicitudproducto dsp ON sep.codigo = dsp.codsolicitud " +
                "left join tbcorpal_vehiculos v ON dsp.codvehiculo = v.codigo " +
                "left join tbcorpal_producto p ON dsp.codproducto = p.codigo " +
                "left join (" +consultaStock+ ") as pp on dsp.codproducto = pp.codigo " +
                "WHERE sep.estadosolicitud = '" + estadoSolicitud + "' and sep.estado = true " +
                "AND dsp.codvehiculo = "+codVehiculo+" order by v.marca, v.modelo asc";

            return conexion.consultaMySql(consulta);
        }
        /*
        internal DataSet get_detSolicitudProductos(int codigoSolicitud, int codProducto)
        {
            NA_VariablesGlobales negocio = new NA_VariablesGlobales();
            string consultaStock = negocio.get_consultaStockProductosActual();

            string consulta = " select " +
                                " pp.codigo, pp.producto, dse.medida , dse.cant as 'cantSolicitada', " +
                                " dse.tiposolicitud, " +
                                " ifnull(dse.cantentregada,0) as 'Cant_Entregada', " +
                                " CASE dse.tiposolicitud " +
                                " WHEN 'ITEM PACK FERIAL' THEN ifnull(pp.StockPackFerial,0) " +
                                " ELSE ifnull(pp.StockAlmacen,0) " +
                                " END AS 'stock_Almacen' " +
                                " from tbcorpal_solicitudentregaproducto se , " +
                                " tbcorpal_detalle_solicitudproducto dse " +
                                " left join (" + consultaStock + ") as pp on dse.codproducto = pp.codigo " +
                                " where " +
                                " se.codigo = dse.codsolicitud and " +
                                " se.codigo = " + codigoSolicitud +" " +
                                " and dse.codproducto = "+codProducto+"";
            return conexion.consultaMySql(consulta);
        }
        */

        // consulta List Vehiculos DD
        internal DataSet get_listVehiculosDRegistro()
        {
            string consulta = "select dsp.codvehiculo, CONCAT(COALESCE(v.`marca`,''), ' ', COALESCE(v.`modelo`,''), 'Placa: ',COALESCE(v.`placa`,'')) as 'Vehiculo' " +
                " from tbcorpal_detalle_solicitudproducto dsp " +
                " left join tbcorpal_vehiculos v ON dsp.codvehiculo = v.codigo " +
                " WHERE dsp.codvehiculo is not null GROUP BY dsp.codvehiculo";
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_Stock(int codProducto)
        {
            NA_VariablesGlobales negocio = new NA_VariablesGlobales();
            string consulta = negocio.get_consultaStockProductosActual(codProducto);
            return conexion.consultaMySql(consulta);
        }

        internal bool update_cantProductosEntregados(int codigoSolicitud, int codigoProducto, float cantEntregado, float restarStock)
        {
                bool banderaResultado = false;
                string consulta0 = "UPDATE tbcorpal_producto set tbcorpal_producto.stock = tbcorpal_producto.stock - " +
                    "CAST('" + restarStock.ToString().Replace(',', '.') + "' AS DECIMAL(10, 2)) " +
                    "WHERE tbcorpal_producto.codigo = " + codigoProducto + " ";

                bool bandera0 = conexion.ejecutarMySql(consulta0);

                if (bandera0)
                {
                    string consulta = "update tbcorpal_detalle_solicitudproducto " +
                        "set tbcorpal_detalle_solicitudproducto.cantentregada = '" + cantEntregado.ToString().Replace(',', '.') + "' " +
                        "where tbcorpal_detalle_solicitudproducto.codsolicitud = " + codigoSolicitud + " and " +
                        "tbcorpal_detalle_solicitudproducto.codproducto = " + codigoProducto + " ";
                    banderaResultado = conexion.ejecutarMySql(consulta);
                }
                    return banderaResultado;
        }

    }
}