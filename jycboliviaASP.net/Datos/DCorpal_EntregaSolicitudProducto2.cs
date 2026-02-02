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
                "pp.StockAlmacen, " +
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
                "left join (" + consultaStock + ") as pp on dsp.codproducto = pp.codigo " +
                "left join tbcorpal_cliente cc ON sep.codcliente = cc.codigo " +

                "WHERE sep.estadosolicitud = '" + estadoSolicitud + "' " +
                "and sep.estado = true " +
                "and sep.fechaGRA >= CURDATE() - INTERVAL 5 DAY " +
                "and (dsp.estadoprodsolicitud <> 'total' or dsp.estadoprodsolicitud is null) " +
                "and (sep.cod_modcobranza !=2 " +
                "OR (sep.cod_modcobranza = 2 AND sep.estado_aprobarcredito = 1) OR sep.cod_modcobranza is null) " +
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
                "left join (" + consultaStock + ") as pp on dsp.codproducto = pp.codigo " +
                "left join tbcorpal_cliente cc ON sep.codcliente = cc.codigo " +
                "WHERE sep.estadosolicitud = '" + estadoSolicitud + "' and sep.estado = true " +
                "AND dsp.codvehiculo = " + codVehiculo + " AND dsp.cantentregada is null order by v.marca, v.modelo asc";

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
        internal bool update_RetirarSolicitud(List<int> codSolicitud, List<int> codProducto)
        {
            string codSolicitudStr = string.Join(",", codSolicitud);
            string codProductoStr = string.Join(",", codProducto);

            if (string.IsNullOrEmpty(codSolicitudStr) || string.IsNullOrEmpty(codProductoStr))
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
                    return conexion.ejecutarMySql2(comand);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al ejecutar la consulta: " + ex.Message);
                }
            }
        }
        internal bool update_RetirarSolicitud2(List<int> codSolicitud, List<int> codProducto)
        {
            string codSolicitudStr = string.Join(",", codSolicitud);
            string codProductoStr = string.Join(",", codProducto);

            if (string.IsNullOrEmpty(codSolicitudStr) || string.IsNullOrEmpty(codProductoStr))
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
                                " , cli.tiendaname as 'cliente' " +
                                " from tbcorpal_solicitudentregaproducto sep " +
                                " left join tbcorpal_cliente cli on sep.codcliente = cli.codigo " +
                                " left join tbcorpal_detalle_solicitudproducto dsp ON sep.codigo = dsp.codsolicitud " +
                                " left join tbcorpal_vehiculos v ON dsp.codvehiculo = v.codigo " +
                                " left join tbcorpal_producto p ON dsp.codproducto = p.codigo " +
                                " WHERE " +
                                " dsp.fechaentrega_car = current_date()  and sep.estado = true ";
            if (codigoCamion > 0) {
                consulta = consulta + "  AND dsp.codvehiculo = " + codigoCamion;
            }
            consulta += " order by v.marca, v.modelo asc";
            return conexion.consultaMySql(consulta);
        }


        internal DataSet get_despachosdeCamiones(string fechadesde, string fechahasta, string estado, int codVehiculo)
        {
            string consulta = "select  " +
                               " dd.codigo, " +
                               " date_format(dd.fechagra,'%d/%m/%Y') as  'fecha', " +
                               " dd.horagra, dd.detalle , " +
                               " concat(vv.placa,'_',vv.marca) as 'Vehiculo', " +
                               " dd.conductor, " +
                               " dd.estadodespacho " +
                               " from tbcorpal_despachovehiculo dd, " +
                               " tbcorpal_vehiculos vv " +
                               " where " +
                               " dd.fechagra >= CURDATE() - INTERVAL 5 DAY and " +
                               " dd.codvehiculo = vv.codigo and " +
                               " dd.estado = 1 and " +
                               " dd.estadodespacho = '" + estado + "'";
            if (codVehiculo > 0) {
                consulta = consulta + " and dd.codvehiculo = " + codVehiculo;
            }

            if (!string.IsNullOrEmpty(fechadesde) && !string.IsNullOrEmpty(fechahasta) &&
                !fechadesde.Equals("null") && !fechahasta.Equals("null"))
            {
                consulta = consulta + " and dd.fechagra between " + fechadesde + " and " + fechahasta; ;
            }
            consulta += " order by dd.codigo asc";
            return conexion.consultaMySql(consulta);
        }

        internal bool update_despachodeproductosCamiones(int codigo, string estado, int codResp)
        {
            string consulta = "update tbcorpal_despachovehiculo set " +
                               " tbcorpal_despachovehiculo.estadodespacho = '" + estado + "', " +
                               " tbcorpal_despachovehiculo.fechacierre = current_date(), " +
                               " tbcorpal_despachovehiculo.horacierre = current_time(), " +
                               " tbcorpal_despachovehiculo.codrespcierre = " + codResp +
                               " where " +
                               " tbcorpal_despachovehiculo.codigo = " + codigo;
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_DespachoProductoaCamion(int codigoDespacho)
        {
            string consulta = "select  " +
                               " dd.codigo, " +
                               " date_format(dd.fechagra,'%d/%m/%Y') as  'fecha', " +
                               " dd.horagra, dd.detalle , " +
                               " vv.marca as 'Vehiculo', " +
                               " res.nombre as 'Conductor', " +
                               " pp.codigo as 'CodProd' , " +
                               " pp.producto, " +
                               " sum(dv.cantentregada) as 'CantEntregar', " +
                               " vv.placa, vv.codigo as 'codVehiculo', dd.codconductor as 'codConductor' " +
                               " from tbcorpal_despachovehiculo dd " +
                               " inner join tbcorpal_detalleproddespacho dv ON dd.codigo = dv.coddespacho " +
                               " left join tbcorpal_producto pp ON dv.codprod = pp.codigo " +
                               " left join tbcorpal_vehiculos vv on dd.codvehiculo = vv.codigo " +
                               " left join tb_responsable res ON dd.codconductor = res.codigo" +
                               " where " +
                               " dd.estado = 1 and " +
                               " dd.codigo = " + codigoDespacho +
                               " group by dd.codigo, dv.codprod";
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_DespachoBoletasProdEntrega(int codigoDespacho)
        {
            try
            {
                string consulta = @"select  
                    dp.codigo as 'codSolicitud',
                    dp.nroboleta,  
                    dp.personalsolicitud,
                    cc.tiendaname as 'Cliente', 
                    dd.codigo as 'codDespacho', 
                    date_format(dd.fechagra,'%d/%m/%Y') as  'fecha', 
                    dd.horagra, 
                    dd.detalle , 
                    vv.marca as 'Vehiculo', 
                    res.nombre as 'Conductor', 
                    pp.codigo as 'CodProd' , 
                    pp.producto, 
                    dv.cantentregada, 
                    vv.placa 
                    from 
                    tbcorpal_solicitudentregaproducto dp 
                    left join tbcorpal_cliente cc on dp.codcliente = cc.codigo
                    left join tbcorpal_detalleproddespacho dv on dp.`codigo` = dv.`codpedido`
                    inner join tbcorpal_despachovehiculo dd on dv.`coddespacho` = dd.`codigo`  
                    left join tbcorpal_producto pp on dv.`codprod` = pp.`codigo`
                    left join tbcorpal_vehiculos vv on dd.`codvehiculo` = vv.`codigo`
                    left join tb_responsable res on dd.codconductor = res.codigo  
                    where  
                    dp.estado = 1 and
                    dd.estado = 1 and 
                    dd.codigo = @codDespacho ";

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@codDespacho", codigoDespacho)
                };
                return conexion.consultaMySqlParametros(consulta, parametros);

            } catch(Exception ex)
            {
                throw new Exception("Error al obtener datos del despacho. " + ex.Message);
            }
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
        public int POST_INSERTdespachoRetornoID(string detalle, int codvehiculo, int codrespinicio, int codconductor, string conductor)
        {
            try
            {
                string consulta01 = "INSERT INTO tbcorpal_despachovehiculo(fechagra, horagra, detalle, codvehiculo, codrespinicio, " +
                    "estado, estadodespacho, codconductor, conductor) values (current_date(), current_time(), @detalle, @codvehiculo, @codrespinicio, 1, 'abierto', " +
                    "@codconductor, @conductor); " +
                    "SELECT LAST_INSERT_ID();";

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@detalle", detalle),
                    new MySqlParameter("@codvehiculo", codvehiculo),
                    new MySqlParameter("@codrespinicio", codrespinicio),
                    new MySqlParameter("@codconductor", codconductor),
                    new MySqlParameter("@conductor", conductor),
                };
                return conexion.ejecutarScalarMySql(consulta01, parametros);
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                Console.WriteLine("Error en POST_INSERTdetalleDespacho: " + ex.Message);
                return false;
            }
        }

        /* POST INSERTAR DETALLES DE SOLICITUD PEDIDO*/
        internal bool UPDATE_camposDetalleSolicitudPedido(int codigoSolicitud, int codigoProducto, float cantEntregado, string estadoProducto, float restarStock,
                                                            int coduser, int codVehiculo)
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
                    " dsp.coduserentrega_car = " + coduser + ", " +
                    " dsp.coduserasignacion_car = " + coduser + ", " +
                    " dsp.codvehiculo = " + codVehiculo + ", " +
                    " dsp.estadoprodsolicitud = '" + estadoProducto + "' " +
                    " where dsp.codsolicitud = " + codigoSolicitud + " and " +
                    " dsp.codproducto = " + codigoProducto + " ";
                banderaResultado = conexion.ejecutarMySql(consulta);
            }
            return banderaResultado;
        }

        internal bool update_CierreAutSolicitudProd(int codSolicitud, int codper, string personal)
        {

            /*string consulta = "UPDATE tbcorpal_solicitudentregaproducto sep " +
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
            */

            string consulta = @"UPDATE tbcorpal_solicitudentregaproducto sep " +
                "SET sep.fechacierre = current_date(), " +
                "sep.horacierre = current_time(), " +
                "sep.codperentregoproducto = @codper, " +
                "sep.personalentregoproducto = @personal, " +
                "sep.estadosolicitud = 'Cerrado' " +
                "WHERE sep.codigo = @codigo " +
                "AND ( " +
                "SELECT COUNT(*) FROM tbcorpal_detalle_solicitudproducto dsp WHERE " +
                "dsp.codsolicitud = @codigo) = ( " +
                "SELECT COUNT(*) FROM tbcorpal_detalle_solicitudproducto dsp " +
                "WHERE dsp.codsolicitud = @codigo " +
                "AND dsp.estadoprodsolicitud = 'total');";

            using (MySqlCommand comand = new MySqlCommand(consulta))
            {
                comand.Parameters.AddWithValue("@codigo", codSolicitud);
                comand.Parameters.AddWithValue("@codper", codper);
                comand.Parameters.AddWithValue("@personal", personal);

                bool success = conexion.ejecutarMySql2(comand);
                return success;
            }
        }


        /************   PEDIDO A CREDITO   ************/
        internal DataSet get_listaPedidosACredito()
        {
            NA_VariablesGlobales negocio = new NA_VariablesGlobales();
            string consultaStock = negocio.get_consultaStockProductosActual();

            string consulta = "SELECT " +
                "sep.codigo, " +
                "CONCAT(date_format(sep.fechaGRA, '%d-%m-%Y'), ' - ', TIME_FORMAT(sep.horaGRA, '%H:%i:%s')) AS 'fechaHoraGRA', " +
                "sep.nroboleta, " +
                "sep.personalsolicitud, " +
                "cc.tiendaname, " +
                "date_format(sep.fechaentrega, '%d/%m/%Y') as 'fechaentrega', " +
                "sep.horaentrega " +
                "from tbcorpal_solicitudentregaproducto sep " +
                "left join tbcorpal_cliente cc ON sep.codcliente = cc.codigo " +
                "WHERE sep.estadosolicitud = 'Abierto' " +
                "and sep.cod_modcobranza = 2 " +
                "and sep.estado = true " +
                "and sep.estado_aprobarcredito is null " +
                "order by sep.fechaGRA asc, sep.nroboleta asc";

            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_listDetallePedidoaCredito(int codigo)
        {
            try
            {
                string consulta = @"SELECT  
                            sep.nroboleta,  
                            p.producto,  
                            dsp.cant as 'cantSolicitada' 
                            from tbcorpal_solicitudentregaproducto sep 
                            left join tbcorpal_detalle_solicitudproducto dsp ON sep.codigo = dsp.codsolicitud 
                            left join tbcorpal_producto p ON dsp.codproducto = p.codigo 
                            WHERE sep.estadosolicitud = 'Abierto' 
                            and sep.cod_modcobranza = 2 
                            and sep.estado = true 
                            and sep.`codigo` = @codigo 
                            and sep.estado_aprobarcredito is null
                            order by sep.fechaGRA asc, sep.nroboleta asc;";

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@codigo", codigo)
                };
                return conexion.consultaMySqlParametros(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la consulta de datos. Detalle de solicitud." + ex.Message);
            }
        }

        internal bool POST_aprobacionSolCredito(int codResp, int codSol, string nroBoleta)
        {
            try
            {
                string consulta = @"UPDATE tbcorpal_solicitudentregaproducto sep 
                                SET sep.resp_aprobarcredito = @codResp, 
                                sep.fecha_aprobarcredito = current_date(), 
                                sep.hora_aprobarcredito = current_time(), 
                                sep.estado_aprobarcredito = 1 
                                WHERE sep.estado = 1 and sep.codigo = @codSol and sep.nroboleta = @nroBoleta;";

                using (MySqlCommand cmd = new MySqlCommand(consulta))
                {
                    cmd.Parameters.AddWithValue("@codResp", codResp);
                    cmd.Parameters.AddWithValue("@codSol", codSol);
                    cmd.Parameters.AddWithValue("@nroBoleta", nroBoleta);

                    bool result = conexion.ejecutarMySql2(cmd);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error en la consulta de aprobación de credito." + ex.Message);
            }
        }


        internal int ObtenerCodVendedor_EntregaSolProductos(int cod)
        {
            try
            {
                string consulta = @"select 
                                    sol.`codpersolicitante`
                                    from tbcorpal_solicitudentregaproducto sol
                                    where sol.`estado` = 1
                                    and sol.`codigo` = @cod
                                    ";
                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@cod", cod)

                };
                DataSet ds = conexion.consultaMySqlParametros(consulta, parametros);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0]["codpersolicitante"]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la consulta al obtener el codVendedor. " + ex.Message);
            }
        }

        internal int Obtener_codMetodoPagoSolicitud(int cod)
        {
            try
            {
                string consulta = @"select sol.cod_modcobranza from 
                                tbcorpal_solicitudentregaproducto sol 
                                where sol.estado = 1 and sol.codigo = @cod";

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@cod", cod)
                };
                DataSet ds = conexion.consultaMySqlParametros(consulta, parametros);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0]["cod_modcobranza"]);
                }
                else
                {
                    return 0;
                }
            } catch (Exception ex)
            {
                throw new Exception("Error en la consulta al obtener el codigoMetodoPago. " + ex.Message);
            }
        }

        internal bool POST_rechazarSolCredito(int codResp, int codSol, string nroBoleta)
        {
            try
            {
                string consulta = @"UPDATE tbcorpal_solicitudentregaproducto sep 
                                SET sep.resp_aprobarcredito = @codResp, 
                                sep.fecha_aprobarcredito = current_date(), 
                                sep.hora_aprobarcredito = current_time(), 
                                sep.estado_aprobarcredito = 0 
                                WHERE sep.estado = 1 and sep.codigo = @codSol and sep.nroboleta = @nroBoleta;";

                using (MySqlCommand cmd = new MySqlCommand(consulta))
                {
                    cmd.Parameters.AddWithValue("@codResp", codResp);
                    cmd.Parameters.AddWithValue("@codSol", codSol);
                    cmd.Parameters.AddWithValue("@nroBoleta", nroBoleta);

                    bool result = conexion.ejecutarMySql2(cmd);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error en la consulta de aprobación de credito." + ex.Message);
            }
        }

        internal bool POST_RegistroAsignacionChoferAVehiculo(int codCar, int codChofer, int codUserGra, string userGra)
        {
            try
            {
                string consulta = @"INSERT INTO tbcorpal_asignacion_conductor_vehiculo 
                    (codvehiculo, codconductor, fecha_asignacion, hora_asignacion, 
                    estado, coduser_gra, usuario_gra, fecha_gra, hora_gra) values (
                    @codvehiculo, @codconductor, current_date(), current_time(), 
                    1, @coduser_gra, @user_gra, current_date(), current_time())";

                MySqlCommand cmd = new MySqlCommand(consulta);
                cmd.Parameters.AddWithValue("@codvehiculo", codCar);
                cmd.Parameters.AddWithValue("@codconductor", codChofer);
                cmd.Parameters.AddWithValue("@coduser_gra", codUserGra);
                cmd.Parameters.AddWithValue("@user_gra", userGra);

                return conexion.ejecutarMySql2(cmd);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la Consulta RegistroAsignacionChofer. " + ex.Message);
                return false;
            }
        }

        internal DataSet GET_obtener_UltConductorVehiculo(int codVehiculo)
        {
            try
            {
                string consulta = @"select ac.fecha_asignacion, ac.hora_asignacion, r.codigo, 
                                r.nombre, ac.codvehiculo from tb_responsable r inner join 
                                tbcorpal_asignacion_conductor_vehiculo ac ON r.codigo = ac.codconductor 
                                where ac.codvehiculo = @codcar order by ac.codigo desc limit 1 ";

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@codcar", codVehiculo)
                };
                return conexion.consultaMySqlParametros(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obetener datos del conductor. " + ex.Message);
            }
        }

        /*  REGISTRO RUTAA  */
        internal int post_RegistroRutaEntrega_despacho(int codCar, string car, int codChofer, string chofer)
        {
            try
            {
                string consulta = @"insert into tbcorpal_rutasentrega(fechagra, horagra, fecharuta, horaruta, 
                    codvehiculo, vehiculo, codchofer, chofer, estado) values 
                    (current_date(), current_time(), current_date, current_time(), 
                    @codCar, @car, @codChofer, @chofer, 1); SELECT LAST_INSERT_ID();";

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@codCar", codCar),
                    new MySqlParameter("@car", car),
                    new MySqlParameter("@codChofer", codChofer),
                    new MySqlParameter("@chofer", chofer),
                };
                return conexion.ejecutarScalarMySql(consulta, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar la ruta de entrega. " + ex.Message);
                return -1;
            }
        }

        internal bool post_RegistroRutaEntregaPuntos_despacho(int orden, int codRuta, int codCliente, string cliente,
                                            int codDespacho, string descripcion, string lat, string lng)
        {
            try
            {
                string consulta = @"insert into tbcorpal_rutapuntos(orden, codruta, codcliente, cliente, coddespacho, 
                            descripcion, estado, lat, lng) values 
                            (@orden, @codRuta, @codCliente, @cliente, @codDespacho, @descripcion, 1, @lat, @lng )";

                MySqlCommand cmd = new MySqlCommand(consulta);
                cmd.Parameters.AddWithValue("@orden", orden);
                cmd.Parameters.AddWithValue("@codRuta", codRuta);
                cmd.Parameters.AddWithValue("@codCliente", codCliente);
                cmd.Parameters.AddWithValue("@cliente", cliente);
                cmd.Parameters.AddWithValue("@codDespacho", codDespacho);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@lat", lat);
                cmd.Parameters.AddWithValue("@lng", lng);

                return conexion.ejecutarMySql2(cmd);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar los puntos. " + ex.Message);
                return false;
            }
        }

        /*  DATOS cliente   */
        internal DataSet GET_obtenerDatosClienteDespacho(int codDespacho)
        {
            try
            {
                string consulta = @"select 
                                    cli.`codigo` as 'codCli', cli.`tiendaname`,
                                    cli.`direccion_lat`, cli.`direccion_lng` 
                                    from tbcorpal_despachovehiculo dv 
                                    inner join tbcorpal_detalleproddespacho dpd ON dv.`codigo` = dpd.`coddespacho` 
                                    inner join tbcorpal_solicitudentregaproducto sol ON dpd.`codpedido` = sol.`codigo` 
                                    inner join tbcorpal_cliente cli ON sol.`codcliente` = cli.`codigo` 
                                    where dv.`codigo` = @codDespacho ";

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@codDespacho", codDespacho)
                };
                return conexion.consultaMySqlParametros(consulta, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos del cliente. " + ex.Message);
            }
        }

        internal DataSet GET_ReportSolicitudEntregaProducto( DateTime fechaIni, DateTime fechaFin,
                                                        string vendedor, string cliente)
        {
            try
            {
                string consulta = @"select 
                                    dv.`codigo` as 'codDespacho',
                                    ddv.`codpedido` as 'codSolicitud',
                                    v2.`codigo` as codVenta,
                                    concat(car.`placa`, ' ', car.`marca`) as 'vehiculo',
                                    dv.`conductor` as 'conductor',

                                    v2.`cliente` as 'cliente',
                                    v2.`responsable` as 'vendedor',

                                    p.`producto` as 'producto',
                                    ddv.`cantentregada` as 'cantidadRecibida',
                                    dvp.`cantidad` as 'cantidadEntregada', 
                                    CASE
                                        when dvp.cantidad is null or dvp.cantidad = 0 
                                            then ddv.cantentregada 
                                        ELSE ddv.cantentregada - dvp.cantidad 
                                    END as cantidadSobrante, 
                                    DATE(v.fechaEmision) as 'fechaEntregaProductosCliente',

                                    v.`estadoventa` as 'estadoventa' 

                                    from  
                                    tbcorpal_despachovehiculo dv 
                                    inner join tbcorpal_vehiculos car ON dv.`codvehiculo` = car.`codigo` 

                                    left join tbcorpal_detalleproddespacho ddv ON dv.`codigo` = ddv.`coddespacho` 
                                    inner join tbcorpal_producto p ON ddv.`codprod` = p.`codigo` 

                                    left join tbcorpal_venta v2 on ddv.`codpedido` = v2.`codsolicitudentregaproducto` 

                                    left join tbcorpal_venta v 
                                         ON ddv.`codpedido` = v.`codsolicitudentregaproducto` 
                                         and v.`estado` = 1 
                                         and v.`estadoventa` = 'Cerrado' 
                                    left join tbcorpal_detalleventasproducto dvp ON v.`codigo` = dvp.`codventa` 

                                    where 
                                    dv.`estado` = 1 and dv.`estadodespacho` = 'Cerrado' 
                                    and dv.`fechagra` between @fechaIni and @fechaFin ";

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@fechaIni", fechaIni),
                    new MySqlParameter("@fechaFin", fechaFin)
                };
                
                if (!string.IsNullOrEmpty(cliente))
                {
                    consulta += " and v2.cliente = @cliente ";
                    parametros.Add(new MySqlParameter("@cliente", cliente));
                }

                if (!string.IsNullOrEmpty(vendedor))
                {
                    consulta += " and v2.responsable = @vendedor ";
                    parametros.Add(new MySqlParameter("@vendedor", vendedor));
                }
                
                consulta += "order by dv.codigo asc ";

                
                return conexion.consultaMySqlParametros(consulta, parametros);

            } catch(Exception ex)
            {
                throw new Exception("Error al obtener datos. " + ex.Message);
            }
        }

    
    
    }
}