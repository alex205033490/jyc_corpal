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
using System.Windows.Input;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_EntregaSolicitudProducto2
    {
        private conexionMySql conexion = new conexionMySql();

        // List principal gv registros
        internal DataSet get_VWRegistrosEntregaSolicitudProductos(string estadoSolicitud)
        {
            NA_VariablesGlobales negocio = new NA_VariablesGlobales();
            string consultaStock = negocio.get_consultaStockProductosActual();

            string consulta = "SELECT " +
                "sep.codigo, " +
                "sep.nroboleta, " +
                "sep.personalsolicitud, " +
                "dsp.codproducto, " +
                "p.producto, " +
                "cc.codigo as 'codCliente', " +
                "cc.tiendaname, " +
                "date_format(sep.fechaentrega, '%d/%m/%Y') as 'fechaentrega', " +
                "sep.horaentrega, " +
                "sep.estadosolicitud, " +
                "dsp.tiposolicitud, " +
                "dsp.cant as 'cantSolicitada', " +
                "ifnull(dsp.cantentregada, 0) as 'cantEntregada', " +
                "CASE dsp.tiposolicitud WHEN 'ITEM PACK FERIAL' THEN ifnull(pp.StockPackFerial, 0) " +
                "ELSE ifnull(pp.StockAlmacen, 0) END AS 'StockAlmacen' " +
                "from tbcorpal_solicitudentregaproducto sep " +
                "left join tbcorpal_detalle_solicitudproducto dsp ON sep.codigo = dsp.codsolicitud " +
                "left join tbcorpal_producto p ON dsp.codproducto = p.codigo " +
                "left join (" +consultaStock+ ") as pp on dsp.codproducto = pp.codigo " +
                "left join tbcorpal_cliente cc ON sep.codcliente = cc.codigo " +
                "WHERE sep.estadosolicitud = '"+estadoSolicitud+"' " +
                "and sep.estado = true " +
                "and sep.fechaGRA >= CURDATE() - INTERVAL 3 WEEK " +
                "and dsp.estadoprodsolicitud <> 'total' or dsp.estadoprodsolicitud is null " +
                "order by sep.fechaGRA desc, sep.nroboleta desc";

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

        

        internal DataSet get_EntregasProductoaCamion(int codigoCamion)
        {
            string consulta = "SELECT CONCAT(COALESCE(v.marca, ''), ' ', COALESCE(v.modelo, '')) AS 'Vehiculo', " +
                                " v.placa, v.conductor, sep.codigo, sep.nroboleta, sep.personalsolicitud, dsp.codproducto, p.producto, " +
                                " date_format(dsp.fechaentrega_car, '%d/%m/%Y') as 'fechaentrega_car', dsp.horaentrega_car, sep.estadosolicitud, " +
                                " dsp.tiposolicitud, dsp.cant as 'cantSolicitada', ifnull(dsp.cantentregada, 0) as 'cantEntregada' " +
                                " , cli.tiendaname as 'cliente' "+
                                " from tbcorpal_solicitudentregaproducto sep " +
                                " left join tbcorpal_cliente cli on sep.codcliente = cli.codigo "+
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

        //mostrar vehiculo en dd
        internal DataSet get_showVehiculoDD()
        {
            string consulta = "select v.codigo, v.marca, v.modelo, v.placa, v.conductor, " +
                "CONCAT(COALESCE(v.marca, ''), ' - ', " +
                "COALESCE(v.`modelo`, ''), " +
                "' placa: ', " +
                "COALESCE(v.placa, ''))" +
                "as 'detalle' " +
                "from tbcorpal_vehiculos v " +
                "order by v.marca asc";
            return conexion.consultaMySql(consulta);
        }
        // ver detalle de vehiculo en GV
        internal DataSet GET_detalleVehiculoGV(int codigo)
        {
            string consulta = "select v.codigo, v.cargacajas, v.capacidad, v.medida, v.conductor " +
                "from tbcorpal_vehiculos v where v.codigo = @codigo";
            MySqlParameter paramCodigo = new MySqlParameter("@codigo", MySqlDbType.Int64);
            paramCodigo.Value = codigo;
            return conexion.consultaMySqlParametros(consulta, new List<MySqlParameter> { paramCodigo });
        }

        //asignar vehiculo a pedido
        internal bool UPDATE_ADDvehiculoAPedido(int codVehiculo, int codUser, int codSolicitud, int codProducto)
        {
            string consulta = "UPDATE tbcorpal_detalle_solicitudproducto ds " +
                "SET ds.codvehiculo = @codVehiculo, ds.fechaasignacion_car = current_date(), " +
                "ds.horaasignacion_car = current_time(), ds.coduserasignacion_car = @codUser " +
                "WHERE ds.codsolicitud = @codSolicitud and ds.codProducto = @codProducto";

            using (MySqlCommand comand = new MySqlCommand(consulta))
            {
                comand.Parameters.AddWithValue("@codVehiculo", codVehiculo);
                comand.Parameters.AddWithValue("@codUser", codUser);
                comand.Parameters.AddWithValue("@codSolicitud", codSolicitud);
                comand.Parameters.AddWithValue("@codProducto", codProducto);

                return conexion.ejecutarMySql2(comand);
            }
        }
        /* OBTENER CODIGO PRODUCTO */
        internal DataSet GET_obtenerCodProducto(string producto)
        {
            string consulta = "SELECT codigo from tbcorpal_producto " +
                "where LOWER(producto) = LOWER(@producto) ";
            MySqlParameter paramNombre = new MySqlParameter("@producto", MySqlDbType.VarChar);
            paramNombre.Value = producto;
            return conexion.consultaMySqlParametros(consulta, new List<MySqlParameter> { paramNombre });
        }

        /*  ----    DESPACHO Y DETALLE DESPACHO     ----*/
        /* POST DESPACHO */
        public int POST_INSERTdespachoRetornoID(string detalle, int codvehiculo, int codrespinicio)
        {
            try
            {
                string consulta01 = "INSERT INTO tbcorpal_despachovehiculo(fechagra, horagra, detalle, codvehiculo, codrespinicio, " +
                    "estado, estadodespacho) values (current_date(), current_time(), @detalle, @codvehiculo, @codrespinicio, 1, 'abierto'); " +
                    "SELECT LAST_INSERT_ID();";

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@detalle", detalle),
                    new MySqlParameter("@codvehiculo", codvehiculo),
                    new MySqlParameter("@codrespinicio", codrespinicio)
                };
                return conexion.ejecutarScalarMySql(consulta01, parametros);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error en Post_INSERT_despachoRetornnoID: " + ex.Message);
                return -1;
            }
        }
        /* POST DETALLE DESPACHO */
        internal bool POST_INSERTdetalleDespacho(int coddespacho, int codpedido, int codprod, float cantidad)
        {
            try
            {
                string consulta = "INSERT INTO tbcorpal_detalleproddespacho(coddespacho, codpedido, codprod, cantentregada) " +
                        "values (@cdespacho, @cpedido, @cproducto, @cant);";

                MySqlCommand comando = new MySqlCommand(consulta);
                comando.Parameters.AddWithValue("@cdespacho", coddespacho);
                comando.Parameters.AddWithValue("@cpedido", codpedido);
                comando.Parameters.AddWithValue("@cproducto", codprod);
                comando.Parameters.AddWithValue("@cant", cantidad);

                return conexion.ejecutarMySql2(comando);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error en POST_INSERTdetalleDespacho: " + ex.Message);
                return false;
            }
        }

        /* POST INSERTAR DETALLES DE SOLICITUD PEDIDO*/
        internal bool UPDATE_camposDetalleSolicitudPedido(int codigoSolicitud, int codigoProducto, float cantEntregado, string estadoProducto,float restarStock, int coduser, int codVehiculo)
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
                    " dsp.fechaasignacion_car = current_date(), " +
                    " dsp.horaentrega_car = current_time(), " +
                    " dsp.horaasignacion_car = current_time(), " +
                    " dsp.coduserentrega_car = " +coduser+ ", " +
                    " dsp.coduserasignacion_car = "+coduser+", " +
                    " dsp.codvehiculo = "+codVehiculo+", " +
                    " dsp.estadoprodsolicitud = '"+estadoProducto+"' " +
                    " where dsp.codsolicitud = " + codigoSolicitud + " and " +
                    " dsp.codproducto = " + codigoProducto + " ";
                banderaResultado = conexion.ejecutarMySql(consulta);
            }
            return banderaResultado;
        }

        internal bool update_CierreAutSolicitudProd(int codSolicitud, int codper, string personal)
        {

            string consulta = "UPDATE tbcorpal_solicitudentregaproducto sep " +
                "SET sep.fechacierre = current_date(), " +
                "sep.horacierre = current_time(), " +
                "sep.codperentregoproducto = @codper, " +
                "sep.personalentregoproducto = @personal, " +
                "sep.estadosolicitud = 'Cerrado' " +
                "WHERE sep.codigo = @codigo " +
                "AND NOT EXISTS ( " +
                "SELECT 1 FROM tbcorpal_detalle_solicitudproducto dsp " +
                "WHERE dsp.codsolicitud = @codigo " +
                "AND dsp.estadoprodsolicitud != 'total'); ";

            using (MySqlCommand comand = new MySqlCommand(consulta))
            {
                comand.Parameters.AddWithValue("@codigo", codSolicitud);
                comand.Parameters.AddWithValue("@codper", codper);
                comand.Parameters.AddWithValue("@personal", personal);

                bool success = conexion.ejecutarMySql2(comand);
                return success;
            }
        }


    }
}