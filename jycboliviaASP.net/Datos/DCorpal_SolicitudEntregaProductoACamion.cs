using jycboliviaASP.net.Negocio;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        internal DataSet get_ShowNroBoleta(string nroBoleta)
        {
            string consulta = "select sp.nroboleta from tbcorpal_solicitudentregaproducto sp " +
                "left join tbcorpal_detalle_solicitudproducto ds on sp.codigo = ds.codsolicitud " +
                "where sp.estado = 1 and sp.estadosolicitud = 'Abierto' " +
                "and ds.codvehiculo is null and sp.nroboleta like '%" + nroBoleta + "%' " +
                "group by sp.nroboleta  ";

            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_ShowPersonalSolicitud(string nombre)
        {
            string consulta = "select sp.personalsolicitud from tbcorpal_solicitudentregaproducto sp " +
                "where sp.estado = 1 and sp.estadosolicitud = 'Abierto' and " +
                "sp.personalsolicitud like '%" + nombre + "%' group by sp.personalsolicitud ";
            return conexion.consultaMySql(consulta);
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
        internal DataSet get_detVehiculo(int codigo)
        {
            string consulta = "select v.codigo, v.cargacajas, v.capacidad, v.medida " +
                "from tbcorpal_vehiculos v " +
                "where v.codigo = "+codigo+" ";
            return conexion.consultaMySql(consulta);
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
        
    }
}