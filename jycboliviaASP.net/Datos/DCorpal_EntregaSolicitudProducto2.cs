using jycboliviaASP.net.Negocio;
using MySql.Data.MySqlClient;
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
                "v.placa, v.conductor, sep.codigo, sep.nroboleta, sep.personalsolicitud, dsp.codproducto, p.producto, cc.codigo as 'cliente', " +
                "date_format(sep.fechaentrega, '%d/%m/%Y') as 'fechaentrega', sep.horaentrega, sep.estadosolicitud, dsp.tiposolicitud, " +
                "dsp.cant as 'cantSolicitada', ifnull(dsp.cantentregada, 0) as 'cantEntregada', " +
                "CASE dsp.tiposolicitud WHEN 'ITEM PACK FERIAL' THEN ifnull(pp.StockPackFerial, 0) " +
                "ELSE ifnull(pp.StockAlmacen, 0) END AS 'StockAlmacen' " +
                "from tbcorpal_solicitudentregaproducto sep " +
                "left join tbcorpal_detalle_solicitudproducto dsp ON sep.codigo = dsp.codsolicitud " +
                "left join tbcorpal_vehiculos v ON dsp.codvehiculo = v.codigo " +
                "left join tbcorpal_producto p ON dsp.codproducto = p.codigo " +
                "left join (" +consultaStock+ ") as pp on dsp.codproducto = pp.codigo " +
                "left join tbcorpal_cliente cc ON sep.codcliente = cc.codigo " +
                "WHERE sep.estadosolicitud = '"+estadoSolicitud+"' and sep.estado = true " +
                "AND dsp.codvehiculo is not null AND dsp.cantentregada is null order by v.marca, v.modelo asc";

            return conexion.consultaMySql(consulta);
        }

        // LISTA DE REGISTROS POR VEHICULO
        internal DataSet get_VWRegistrosEntregaSolicitudProductoXCamion(string estadoSolicitud, int codVehiculo)
        {
            NA_VariablesGlobales negocio = new NA_VariablesGlobales();
            string consultaStock = negocio.get_consultaStockProductosActual();

            string consulta = "SELECT CONCAT(COALESCE(v.marca, ''), ' ', COALESCE(v.modelo, '')) AS 'Vehiculo', " +
                "v.placa, v.conductor, sep.codigo, sep.nroboleta, sep.personalsolicitud, dsp.codproducto, p.producto, cc.codigo as 'cliente', " +
                "date_format(sep.fechaentrega, '%d/%m/%Y') as 'fechaentrega', sep.horaentrega, sep.estadosolicitud, dsp.tiposolicitud, " +
                "dsp.cant as 'cantSolicitada', ifnull(dsp.cantentregada, 0) as 'cantEntregada', " +
                "CASE dsp.tiposolicitud WHEN 'ITEM PACK FERIAL' THEN ifnull(pp.StockPackFerial, 0) " +
                "ELSE ifnull(pp.StockAlmacen, 0) END AS 'StockAlmacen' " +
                "from tbcorpal_solicitudentregaproducto sep " +
                "left join tbcorpal_detalle_solicitudproducto dsp ON sep.codigo = dsp.codsolicitud " +
                "left join tbcorpal_vehiculos v ON dsp.codvehiculo = v.codigo " +
                "left join tbcorpal_producto p ON dsp.codproducto = p.codigo " +
                "left join (" +consultaStock+ ") as pp on dsp.codproducto = pp.codigo " +
                "left join tbcorpal_cliente cc ON sep.codcliente = cc.codigo " +
                "WHERE sep.estadosolicitud = '" + estadoSolicitud + "' and sep.estado = true " +
                "AND dsp.codvehiculo = "+codVehiculo+" AND dsp.cantentregada is null order by v.marca, v.modelo asc";

            List<MySqlParameter> parametros = new List<MySqlParameter>
            {
                new MySqlParameter("@consultaStock", MySqlDbType.VarChar){Value = consultaStock},
                new MySqlParameter("@estadoSolicitud", MySqlDbType.VarChar){ Value = estadoSolicitud},
                new MySqlParameter("@codVehiculo", MySqlDbType.VarChar){Value = codVehiculo}

            };
            return conexion.consultaMySqlParametros(consulta, parametros);
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
                " WHERE dsp.codvehiculo is not null AND dsp.cantentregada is null GROUP BY dsp.codvehiculo";
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_Stock(int codProducto)
        {
            NA_VariablesGlobales negocio = new NA_VariablesGlobales();
            string consulta = negocio.get_consultaStockProductosActual(codProducto);
            return conexion.consultaMySql(consulta);
        }

        internal bool update_cantProductosEntregados(int codigoSolicitud, int codigoProducto, float cantEntregado, float restarStock, int coduser)
        {
                bool banderaResultado = false;
                string consulta0 = "UPDATE tbcorpal_producto set tbcorpal_producto.stock = tbcorpal_producto.stock - " +
                    "CAST('" + restarStock.ToString().Replace(',', '.') + "' AS DECIMAL(10, 2)) " +
                    "WHERE tbcorpal_producto.codigo = " + codigoProducto + " ";

                bool bandera0 = conexion.ejecutarMySql(consulta0);

                if (bandera0)
                {
                    string consulta = "update tbcorpal_detalle_solicitudproducto dsp" +
                        " set dsp.cantentregada = '" + cantEntregado.ToString().Replace(',', '.') + "', " +
                        " dsp.fechaentrega_car = current_date(), " +
                        " dsp.horaentrega_car = current_time(), " +
                        " dsp.coduserentrega_car = "+coduser+" "+
                        " where dsp.codsolicitud = " +codigoSolicitud+ " and " +
                        " dsp.codproducto = " + codigoProducto + " ";
                    banderaResultado = conexion.ejecutarMySql(consulta);
                }
            
            return banderaResultado;
        }


        //UPDATE ANULAR SOLICITUD
        internal bool update_RetirarSolicitud(List<int> codSolicitud, List<int>codProducto)
        {
            string codSolicitudStr = string.Join(",", codSolicitud);
            string codProductoStr = string.Join(",", codProducto);

            if(string.IsNullOrEmpty(codSolicitudStr) || string.IsNullOrEmpty(codProductoStr))
            {
                throw new ArgumentException("Las listas de códigos no deben estar vacias.");
            }

            string consulta = "UPDATE tbcorpal_detalle_solicitudproducto dsp " +
            "set dsp.codvehiculo = null, dsp.fechaasignacion_car = null, " +
            "dsp.horaasignacion_car = null, dsp.coduserasignacion_car = null " +
            "where dsp.codsolicitud in ("+ codSolicitudStr +") " +
            "and dsp.codProducto in ("+ codProductoStr +") ;";

            using (MySqlCommand comand = new MySqlCommand(consulta))
            {
                try
                {
                    return conexion.ejecutarMySql2(comand);
                }
                catch(Exception ex)
                {
                    throw new Exception("Error al ejecutar la consulta: " + ex.Message);
                }
            }
        }
        internal bool update_RetirarSolicitud2(List<int> codSolicitud, List<int> codProducto)
        {
            string codSolicitudStr = string.Join(",", codSolicitud);
            string codProductoStr = string.Join(",", codProducto);

            if(string.IsNullOrEmpty(codSolicitudStr) || string.IsNullOrEmpty(codProductoStr))
            {
                throw new ArgumentException("Las listas de códigos no deben estar vacias.");
            }
            string consulta = "UPDATE tbcorpal_detalle_solicitudproducto dsp " +
                "set dsp.codvehiculo = null, dsp.fechaasignacion_car = null, " +
                "dsp.horaasignacion_car = null, dsp.coduserasignacion_car = null " +
                "where dsp.codsolicitud in (" + codSolicitudStr + ") " +
                "and dsp.codProducto in (" + codProductoStr + ") ;";
            using (MySqlCommand comand = new MySqlCommand(consulta))
            {
                try
                {
                    comand.Parameters.AddWithValue("@codSolicitudStr", codSolicitud);
                    comand.Parameters.AddWithValue("@codProductoStr", codProducto);
                    return conexion.ejecutarMySql2(comand);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al ejecutar la consulta: " + ex.Message);
                }
            }


        }









        internal bool update_CierreAutSolicitudProd(int codSolicitud, int codper,string personal)
        {
            /*string consulta = "UPDATE tbcorpal_detalle_solicitudProducto dd " +
                "JOIN ( " +
                "SELECT codsolicitud " +
                "FROM tbcorpal_detalle_solicitudProducto " +
                "WHERE codsolicitud = @codsolicitud " +
                "GROUP BY codsolicitud " +
                "HAVING SUM(CASE WHEN cantentregada IS NULL OR cantentregada < 0 THEN 1 ELSE 0 END) = 0 " +
                ") valid_check ON dd.codsolicitud = valid_check.codsolicitud " +
                "SET dd.fechaentrega_car = CURRENT_DATE(), " +
                "dd.horaentrega_car = CURRENT_TIME(), " +
                "dd.coduserentrega_car = @coduser " +
                "WHERE dd.codsolicitud = @codsolicitud;";
            */
            string consulta = "UPDATE tbcorpal_solicitudentregaproducto sep " +
                "SET sep.fechacierre = current_date(), " +
                "sep.horacierre = current_time(), " +
                "sep.codperentregoproducto = @codper, " +
                "sep.personalentregoproducto = @personal, " +
                "sep.estadosolicitud = 'Cerrado' " +
                "WHERE sep.codigo = @codigo " +
                "AND NOT EXISTS ( " +
                "SELECT 1 FROM tbcorpal_detalle_solicitudproducto dsp " +
                "WHERE dsp.codsolicitud = @codigo AND dsp.cantentregada IS NULL); ";

            using (MySqlCommand comand = new MySqlCommand(consulta))
            {
                comand.Parameters.AddWithValue("@codigo", codSolicitud);
                comand.Parameters.AddWithValue("@codper", codper);
                comand.Parameters.AddWithValue("@personal", personal);

                bool success = conexion.ejecutarMySql2(comand);
                return success;
            }
        }

        internal DataSet get_EntregasProductoaCamion(int codigoCamion)
        {
            string consulta = "SELECT CONCAT(COALESCE(v.marca, ''), ' ', COALESCE(v.modelo, '')) AS 'Vehiculo', " +
                                " v.placa, v.conductor, sep.codigo, sep.nroboleta, sep.personalsolicitud, dsp.codproducto, p.producto, " +
                                " date_format(dsp.fechaentrega_car, '%d/%m/%Y') as 'fechaentrega_car', dsp.horaentrega_car, sep.estadosolicitud, " +
                                " dsp.tiposolicitud, dsp.cant as 'cantSolicitada', ifnull(dsp.cantentregada, 0) as 'cantEntregada' " +
                                " from tbcorpal_solicitudentregaproducto sep " +
                                " left join tbcorpal_detalle_solicitudproducto dsp ON sep.codigo = dsp.codsolicitud " +
                                " left join tbcorpal_vehiculos v ON dsp.codvehiculo = v.codigo " +
                                " left join tbcorpal_producto p ON dsp.codproducto = p.codigo " +
                                " WHERE " +
                                " dsp.fechaentrega_car = current_date()  and sep.estado = true ";
                                if (codigoCamion > 0) {
                                    consulta = consulta + "  AND dsp.codvehiculo = " + codigoCamion;
                                }
                    consulta +=  " order by v.marca, v.modelo asc";
            return conexion.consultaMySql(consulta);
        }
    }
}