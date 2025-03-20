using jycboliviaASP.net.Negocio;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services.Protocols;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_SolicitudEntregaProductoACamion
    {
        private conexionMySql conexion = new conexionMySql();
    internal DataSet get_SolicitudesRealizadasProductos(string NroSolicitud, string solicitud, string estadoSolicitud)
        {
        string consulta = "select pp.codigo, pp.nroboleta,  date_format(pp.fechaGRA,'%d/%m/%Y') as 'FechaGrabacion', " +
                                " pp.horaGRA,  date_format(pp.fechaentrega,'%d/%m/%Y') as 'FechaEntrega', " +
                                " pp.horaentrega,  pp.personalsolicitud, pp.montototal,  pp.estadosolicitud, " +
                                " cc.tiendaname as 'Cliente' " +
                                " from  tbcorpal_solicitudentregaproducto pp " +
                                " left join tbcorpal_cliente cc on pp.codcliente = cc.codigo " +
                                " where " +
                                " pp.estadosolicitud = '" + estadoSolicitud + "' and " +
                                " pp.estado = true and " +
                                " pp.nroboleta like '%" + NroSolicitud + "%'";

            if (!string.IsNullOrEmpty(solicitud))
            {
                consulta = consulta + " and pp.personalsolicitud like '%" + solicitud + "%' ";
            }
            return conexion.consultaMySql(consulta);
        }
       
        /*Error*/
        internal DataSet get_detalleSolicitudProducto(int codigoSolicitud)
        {
            NA_VariablesGlobales nv = new NA_VariablesGlobales();
            string consultaStock = nv.get_consultaStockProductosActual();
            string consulta = " select pp.codigo, pp.producto, dse.medida, dse.cant as 'cantSolici', " +
                "dse.tiposolicitud, ifnull(dse.cantentregada, 0) as 'cantEntregada', CASE dse.tiposolicitud " +
                "WHEN 'ITEM PACK FERIAL' THEN ifnull(pp.StockPackFerial, 0) ELSE ifnull(pp.StockAlmacen, 0) " +
                "END AS 'stock_Almacen' " +
                "from tbcorpal_solicitudentregaproducto se, tbcorpal_detalle_solicitudproducto dse " +
                "left join (" + consultaStock + ") as pp on dse.codproducto = pp.codigo " +
                "where se.codigo = dse.codsolicitud and se.codigo = " + codigoSolicitud;

            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_RegistrosSolicitudPedidos (string NroSolicitud, string solicitud, string estadoSolicitud)
        {
            NA_VariablesGlobales nv = new NA_VariablesGlobales();
            string consultaStock = nv.get_consultaStockProductosActual();
            string consulta = "select pp.codigo as 'codRegistro', pp.nroboleta,  date_format(pp.fechaGRA,'%d/%m/%Y') as 'FechaGra', " +
                                " pp.horaGRA,  date_format(pp.fechaentrega,'%d/%m/%Y') as 'FechaEntrega', " +
                                " pp.horaentrega, pp.personalsolicitud, pp.montototal,  pp.estadosolicitud, " +
                                " cc.tiendaname as 'Cliente', ds.cant, ds.tiposolicitud, ifnull(ds.cantentregada,0) as 'cantEntregada', " +
                                " st.codigo as 'codProducto', st.producto " +
                                " from tbcorpal_solicitudentregaproducto pp " +
                                " left join tbcorpal_detalle_solicitudproducto ds on pp.codigo = ds.codsolicitud " +
                                " left join tbcorpal_cliente cc on pp.codcliente = cc.codigo " +
                                " left join ("+consultaStock+") as st on ds.codproducto = st.codigo " +
                                " where " +
                                " pp.estadosolicitud = '" + estadoSolicitud + "' and " +
                                " pp.estado = true and " +
                                " ds.codvehiculo is null and" +
                                " pp.nroboleta like '%" + NroSolicitud + "%'"+
                                " order by pp.codigo desc";

            if (!string.IsNullOrEmpty(solicitud))
            {
                consulta = consulta + " and pp.personalsolicitud like '%" + solicitud + "%' ";
            }
            return conexion.consultaMySql(consulta);
        }
        public DataSet get_RegistrosSolicitudPedidos2(string NroSolicitud, string solicitud, string estadoSolicitud)
        {
            NA_VariablesGlobales nv = new NA_VariablesGlobales();
            string consultaStock = nv.get_consultaStockProductosActual();
            string consulta = "select pp.codigo as 'codRegistro', pp.nroboleta,  date_format(pp.fechaGRA,'%d/%m/%Y') as 'FechaGra', " +
                                " pp.horaGRA,  date_format(pp.fechaentrega,'%d/%m/%Y') as 'FechaEntrega', " +
                                " pp.horaentrega, pp.personalsolicitud, pp.montototal,  pp.estadosolicitud, " +
                                " cc.tiendaname as 'Cliente', ds.cant, ds.tiposolicitud, ifnull(ds.cantentregada,0) as 'cantEntregada', " +
                                " st.codigo as 'codProducto', st.producto " +
                                " from tbcorpal_solicitudentregaproducto pp " +
                                " left join tbcorpal_detalle_solicitudproducto ds on pp.codigo = ds.codsolicitud " +
                                " left join tbcorpal_cliente cc on pp.codcliente = cc.codigo " +
                                " left join (@consultaStock) as st on ds.codproducto = st.codigo " +
                                " where " +
                                " pp.estadosolicitud = @estadoSolicitud and " +
                                " pp.estado = true and " +
                                " ds.codvehiculo is null and" +
                                " pp.nroboleta like @NroSolicitud";
            List<MySqlParameter> parametros = new List<MySqlParameter>
            {
                new MySqlParameter("@consultaStock", MySqlDbType.VarChar){ Value = consultaStock},
                new MySqlParameter("@estadoSolicitud", MySqlDbType.VarChar){ Value = estadoSolicitud},
                new MySqlParameter("@NroSolicitud", MySqlDbType.VarChar){ Value = "%" +NroSolicitud+ "%"}
            };

            if (!string.IsNullOrEmpty(solicitud))
            {
                consulta += " AND pp.personalsolicitud LIKE @solicitud";
                parametros.Add(new MySqlParameter("@solicitud", MySqlDbType.VarChar) { Value = "%" + solicitud + "%" });
            }
            return conexion.consultaMySqlParametros(consulta, parametros);
        }

        /*internal DataSet get_ShowNroBoleta2(string nroBoleta)
        {
            string consulta = "select sp.nroboleta from tbcorpal_solicitudentregaproducto sp " +
                "left join tbcorpal_detalle_solicitudproducto ds on sp.codigo = ds.codsolicitud " +
                "where sp.estado = 1 and sp.estadosolicitud = 'Abierto' " +
                "and ds.codvehiculo is null and sp.nroboleta like '%" + nroBoleta + "%' " +
                "group by sp.nroboleta  ";

            return conexion.consultaMySql(consulta);
        }*/
        internal DataSet get_ShowNroBoleta(string nroBoleta) 
        {
            string consulta = "select sp.nroboleta from tbcorpal_solicitudentregaproducto sp " +
                      "left join tbcorpal_detalle_solicitudproducto ds on sp.codigo = ds.codsolicitud " +
                      "where sp.estado = 1 and sp.estadosolicitud = 'Abierto' " +
                      "and ds.codvehiculo is null and sp.nroboleta like @nroBoleta " +
                      "group by sp.nroboleta";

            MySqlParameter paramNroBoleta = new MySqlParameter("@nroBoleta", MySqlDbType.VarChar);
            paramNroBoleta.Value = "%" + nroBoleta + "%";

            return conexion.consultaMySqlParametros(consulta, new List<MySqlParameter> { paramNroBoleta });
        }

        /*internal DataSet get_ShowPersonalSolicitud2(string nombre)
        {
            string consulta = "select sp.personalsolicitud from tbcorpal_solicitudentregaproducto sp " +
                "where sp.estado = 1 and sp.estadosolicitud = 'Abierto' and " +
                "sp.personalsolicitud like '%" + nombre + "%' group by sp.personalsolicitud ";
            return conexion.consultaMySql(consulta);
        }*/
        internal DataSet get_ShowPersonalSolicitud(string nombre)
        {
            string consulta = "select sp.personalsolicitud from tbcorpal_solicitudentregaproducto sp " +
                "where sp.estado = 1 and sp.estadosolicitud = 'Abierto' and " +
                "sp.personalsolicitud like @nombre group by sp.personalsolicitud;";

            MySqlParameter paramNombre = new MySqlParameter("@nombre", MySqlDbType.VarChar);
            paramNombre.Value = "%" + nombre + "%";

            return conexion.consultaMySqlParametros(consulta, new List<MySqlParameter> { paramNombre });
        }

        internal DataSet get_ShowVehiculo()
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
        /*internal DataSet get_detVehiculo2(int codigo)
        {
            string consulta = "select v.codigo, v.cargacajas, v.capacidad, v.medida " +
                "from tbcorpal_vehiculos v " +
                "where v.codigo = "+codigo+" ";
            return conexion.consultaMySql(consulta);
        }*/
        internal DataSet get_detVehiculo(int codigo)
        {
            string consulta = "select v.codigo, v.cargacajas, v.capacidad, v.medida " +
                "from tbcorpal_vehiculos v where v.codigo = @codigo ";

            MySqlParameter paramCodigo = new MySqlParameter("@codigo", MySqlDbType.Int64);
            paramCodigo.Value = codigo;

            return conexion.consultaMySqlParametros(consulta, new List<MySqlParameter> { paramCodigo });
        }

        // UPDATE ASIGNAR VEHICULO
        internal bool update_ADDVehiculoAPedido(int codVehiculo, int codUser, int codSolicitud, int codProducto)
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

        internal DataSet get_AsignacionProductoaCamion(int codigoCamion)
        {
            string consulta = "SELECT CONCAT(COALESCE(v.marca, ''), ' ', COALESCE(v.modelo, '')) AS 'Vehiculo', "+
                           " v.placa, v.conductor, sep.codigo, sep.nroboleta, sep.personalsolicitud, dsp.codproducto, p.producto, "+
                           " date_format(sep.fechaentrega, '%d/%m/%Y') as 'fechaentrega', sep.horaentrega, sep.estadosolicitud, dsp.tiposolicitud, "+
                           " dsp.cant as 'cantSolicitada' "+
                           " from tbcorpal_solicitudentregaproducto sep "+
                           " left join tbcorpal_detalle_solicitudproducto dsp ON sep.codigo = dsp.codsolicitud "+
                           " left join tbcorpal_vehiculos v ON dsp.codvehiculo = v.codigo "+
                           " left join tbcorpal_producto p ON dsp.codproducto = p.codigo "+
                           " WHERE "+
                           " dsp.fechaasignacion_car = current_date() and "+
                           " sep.estadosolicitud = 'Abierto' and sep.estado = true "+
                           " AND dsp.codvehiculo = "+codigoCamion+" AND dsp.cantentregada is null "+
                           " order by v.marca, v.modelo asc";
            return conexion.consultaMySql(consulta);
        }
    }
}