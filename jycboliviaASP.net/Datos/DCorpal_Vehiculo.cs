using jycboliviaASP.net.Negocio;
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
    }
}