using jycboliviaASP.net.Negocio;
using MaterialDesignThemes.Wpf.Converters;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_Vehiculo
    {
        private conexionMySql conexion = new conexionMySql();

        //cargar datos en GV
        internal DataSet get_mostrarVehiculosGV()
        {
            string consulta = "select v.codigo, v.modelo, v.marca, v.placa, " +
                "v.codconductor, v.conductor, v.capacidad, v.cargacajas, v.detalle " +
                "from tbcorpal_vehiculos v where v.estado = 1 order by v.marca asc";
            return conexion.consultaMySql(consulta);
        }

        //registrar vehiculo
        internal bool post_registrarVehiculo(string placa, string marca, string modelo, string detalle, string conductor,
            decimal capacidad, int cargacajas)
        {
            string consulta = "INSERT INTO tbcorpal_vehiculos (fechagra, horagra, placa, marca, modelo, " +
                "detalle, codconductor, conductor, capacidad, medida, cargacajas, estado) VALUES " +
                " (current_date(), current_time(), @placa, @marca, @modelo, @detalle, null, @conductor, @capacidad, 'Tn', @cargacajas, 1)";

            MySqlCommand comando = new MySqlCommand(consulta);

            comando.Parameters.AddWithValue("@placa",placa);
            comando.Parameters.AddWithValue("@marca",marca);
            comando.Parameters.AddWithValue("@modelo",modelo);
            comando.Parameters.AddWithValue("@detalle",detalle);
            comando.Parameters.AddWithValue("@conductor",conductor);
            comando.Parameters.AddWithValue("@capacidad",capacidad);
            comando.Parameters.AddWithValue("@cargacajas",cargacajas);

            return conexion.ejecutarMySql2(comando);
        }

        //anular registro
        internal bool anular_RegistroVehiculo(List<int> codigo)
        {
            if (codigo == null || codigo.Count == 0)
                return false;

            List<string> parametros = new List<string>();
            for (int i = 0; i < codigo.Count; i++)
            {
                parametros.Add("@codigo" + i);
            }

            string consulta = "UPDATE tbcorpal_vehiculos v " +
                "set v.estado = 0 where v.codigo in (" + string.Join(",", parametros) + "); ";

            MySqlCommand comando = new MySqlCommand(consulta);

            for(int i = 0; i < codigo.Count; i++)
            {
                comando.Parameters.AddWithValue(parametros[i], codigo[i]);
            }
            return conexion.ejecutarMySql2(comando);
        }

        //actualizar registros
        internal bool post_updateRegistroVehiculos(int codigo ,string placa, string marca, string modelo, string detalle, string conductor,
            float capacidad, int cargacajas)
        {
            try
            {
                string consulta = "UPDATE tbcorpal_vehiculos v set " +
                    "v.placa = @placa, v.marca = @marca, v.modelo = @modelo, v.detalle = @detalle, " +
                    "v.conductor = @conductor, v.capacidad = @capacidad, v.cargacajas = @cargacajas " +
                    "WHERE v.codigo = @codigo AND v.estado = 1";

                MySqlCommand cmd = new MySqlCommand(consulta);

                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@placa", placa);
                cmd.Parameters.AddWithValue("@marca", marca);
                cmd.Parameters.AddWithValue("@modelo", modelo);
                cmd.Parameters.AddWithValue("@detalle", detalle);
                cmd.Parameters.AddWithValue("@conductor", conductor);
                cmd.Parameters.AddWithValue("@capacidad", capacidad);
                cmd.Parameters.AddWithValue("@cargacajas", cargacajas);

                return conexion.ejecutarMySql2(cmd);

            }
            catch(MySqlException mysqlEx)
            {
                Console.WriteLine($"Error en mysql {mysqlEx.Message}. stacktrace: {mysqlEx.Message}");
                return false;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}. stacktrace: {ex.Message}");
                return false;
            }
        }

        internal DataSet get_showVehiculoDD()
        {
            try
            {
                String consulta = @"select 
                                    v.`codigo`,
                                    concat(v.`marca`, ' ','Placa: ', v.`placa` ) as 'detalle' 
                                    from tbcorpal_vehiculos v 
                                    left join tbcorpal_despachovehiculo dv ON v.`codigo` = dv.`codvehiculo` 
                                    group by dv.`codvehiculo` 
                                    order by v.`marca` asc";
                return conexion.consultaMySql(consulta);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los datos del vehiculo" + ex.Message);
            }
        }

        internal DataSet get_showRutasVehiculosDespachos(int codCar)
        {
            try
            {
                string consulta = @"select 
                                    rp.`cliente`, 
                                    rp.`orden`,
                                    rp.`lat`,
                                    rp.`lng`,
                                    rp.codcliente 
                                    from tbcorpal_rutasentrega re 
                                    left join tbcorpal_rutapuntos rp ON re.`codigo` = rp.`codruta` 
                                    where re.`fecharuta` = current_date() 
                                    and re.`estado` = 1 
                                    and re.`estadoruta` = 'PENDIENTE' 
                                    and rp.`estado` = 1 
                                    and rp.`estadopunta` = 'PENDIENTE' 
                                    and re.`codvehiculo` = @codCar 
                                    group by rp.`cliente` 
                                    order by rp.`orden` asc";
                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@codCar", codCar)
                };
                return conexion.consultaMySqlParametros(consulta, parametros);

            } catch(Exception ex)
            {
                throw new Exception($"Erro al obtener las rutas de despacho.  {ex.Message}");
            }
        }

        /*  REGISTRO RUTAA  */
        internal int post_NewRegistroRutaEntrega_Asignacion(int codCar, string car)
        {
            try
            {
                string consulta = @"insert into tbcorpal_rutasentrega(fechagra, horagra, fecharuta, horaruta, 
                    codvehiculo, vehiculo, nombre_ruta, estado) values 
                    (current_date(), current_time(), current_date, current_time(), 
                    @codCar, @car, 'Nueva ruta creado desde el formulario', 1); SELECT LAST_INSERT_ID();";

                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@codCar", codCar),
                    new MySqlParameter("@car", car)
                };
                return conexion.ejecutarScalarMySql(consulta, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar la ruta de entrega. " + ex.Message);
                return -1;
            }
        }

        internal bool post_NewRegistroRutaEntregaPuntos_Asignacion(int orden, int codRuta, int codCliente, string cliente,
                                            string lat, string lng)
        {
            try
            {
                string consulta = @"insert into tbcorpal_rutapuntos(orden, codruta, codcliente, cliente, 
                            descripcion, estado, lat, lng) values 
                            (@orden, @codRuta, @codCliente, @cliente, 'Nuevo punto creado desde el formulario.', 1, @lat, @lng )";

                MySqlCommand cmd = new MySqlCommand(consulta);
                cmd.Parameters.AddWithValue("@orden", orden);
                cmd.Parameters.AddWithValue("@codRuta", codRuta);
                cmd.Parameters.AddWithValue("@codCliente", codCliente);
                cmd.Parameters.AddWithValue("@cliente", cliente);
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

        internal bool update_ordenRutaEntrega_asignacion (int codCar, int orden, int codCli)
        {

            try
            {
                string consulta = @"update tbcorpal_rutasentrega re 
                        inner join tbcorpal_rutapuntos rp ON re.codigo = rp.codruta 
                        set rp.orden = @nroOrden 
                        where re.estado = 1 and re.codvehiculo = @codCar
                        and re.fecharuta = current_date() and re.estadoruta = 'PENDIENTE' 
                        and rp.estado = 1 and rp.estadopunta = 'PENDIENTE' 
                        and rp.codcliente = @codCli";

                MySqlCommand cmd = new MySqlCommand(consulta);

                cmd.Parameters.AddWithValue("@nroOrden", orden);
                cmd.Parameters.AddWithValue("@codCar", codCar);
                cmd.Parameters.AddWithValue("@codCli", codCli);

                return conexion.ejecutarMySql2(cmd);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al actualizar los datos. " + ex.Message);
                return false;
            }
        }


    }
}