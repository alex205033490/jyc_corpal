using jycboliviaASP.net.Negocio;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Web;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_Extintor
    {
        private conexionMySql conexion = new conexionMySql();

        internal bool insert_RegistroExtintor(string detalle, string area, string agenteextintor, string marca, float capacidad,
            string codSistema, string fechaCarga, string fechaProximaCarga, string estadoExtintor, int añoProximaPrueba, int codres, string nombreRes)
        {
            string consulta = "INSERT INTO tbcorpal_extintores " +
                "(fechagra, horagra, detalle, area, agenteextintor, marca, capacidad, codSistema, " +
                "fechadecarga, fechaproximacarga, estadoextintor, estado, anioproximapruebahidrostatica, codres, nombreresp) " +
                "VALUES (current_date(), current_time(), @detalle, @area, @agenteextintor, @marca, @capacidad, @codSistema, " +
                "@fechaCarga, @fechaProximaCarga, @estadoExtintor, 1, @anioProximaPrueba, @codres, @nombreRes);";

            MySqlCommand comando = new MySqlCommand(consulta);

            comando.Parameters.AddWithValue("@detalle", detalle);
            comando.Parameters.AddWithValue("@area", area);
            comando.Parameters.AddWithValue("@agenteextintor", agenteextintor);
            comando.Parameters.AddWithValue("@marca", marca);
            comando.Parameters.AddWithValue("@capacidad", capacidad);
            comando.Parameters.AddWithValue("@codSistema", codSistema);
            comando.Parameters.AddWithValue("@fechaCarga", fechaCarga);
            comando.Parameters.AddWithValue("@fechaProximaCarga", fechaProximaCarga);
            comando.Parameters.AddWithValue("@estadoExtintor", estadoExtintor);
            comando.Parameters.AddWithValue("@anioProximaPrueba", añoProximaPrueba);
            comando.Parameters.AddWithValue("@codres", codres);
            comando.Parameters.AddWithValue("@nombreRes", nombreRes);

            return conexion.ejecutarMySql2(comando);
        }

        internal DataSet mostrar_registrosExtintores(string area)
        {
            string consulta = "Select codigo, fechagra, horagra, detalle, area, agenteextintor, marca, " +
                "capacidad, codSistema, fechadecarga, fechaproximacarga, estadoextintor, " +
                "anioproximapruebahidrostatica, nombreresp from tbcorpal_extintores where estado = 1 " +
                "and area like '%" + area + "%';";

            return conexion.consultaMySql(consulta);
        }

        internal bool anular_RegistroExtintor(List<int> codigo)
        {
            if (codigo == null || codigo.Count == 0)
                return false;

            List<string> parametros = new List<string>();
            for (int i = 0; i < codigo.Count; i++)
            {
                parametros.Add("@codigo" + i);
            }

            string consulta = "UPDATE tbcorpal_extintores ex " +
                "set ex.estado = 0 where ex.codigo in (" + string.Join(",", parametros) + ");";

            MySqlCommand comando = new MySqlCommand(consulta);

            for (int i = 0; i < codigo.Count; i++)
            {
                comando.Parameters.AddWithValue(parametros[i], codigo[i]);
            }
            return conexion.ejecutarMySql2(comando);
        }

        internal bool ActualizarRegistrosExtintor(int codigo, string detalle, string area, string agenteextintor, string marca, float capacidad,
            string codSistema, string estadoextintor, int anioProxPruebaH, string fechadecarga, string fechaproximacarga)
        {
            try
            {
                string consulta = "UPDATE tbcorpal_extintores e SET e.detalle = @detalle , e.area = @area, e.agenteextintor = @aextintor, " +
                       "e.marca = @marca, e.capacidad = @capacidad, e.codSistema = @codsistema, " +
                       "e.estadoextintor = @estadoextintor, e.anioproximapruebahidrostatica = @anioProxPruebaH, e.fechadecarga = @fechadecarga, " +
                       "e.fechaproximacarga = @fechaproximacarga WHERE e.codigo = @codigo AND e.estado = 1";

                MySqlCommand cmd = new MySqlCommand(consulta);

                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@detalle",detalle);
                cmd.Parameters.AddWithValue("@area", area);
                cmd.Parameters.AddWithValue("@aextintor", agenteextintor);
                cmd.Parameters.AddWithValue("@marca", marca);
                cmd.Parameters.AddWithValue("@capacidad", capacidad);
                cmd.Parameters.AddWithValue("@codsistema", codSistema);
                cmd.Parameters.AddWithValue("@estadoextintor", estadoextintor);
                cmd.Parameters.AddWithValue("@anioProxPruebaH", anioProxPruebaH);
                cmd.Parameters.AddWithValue("@fechadecarga", fechadecarga);
                cmd.Parameters.AddWithValue("@fechaproximacarga", fechaproximacarga);


                return conexion.ejecutarMySql2(cmd);

            }
            catch (MySqlException mysqlEx)
            {
                Console.WriteLine($"Error en Mysql {mysqlEx.Message}. stacktrace: {mysqlEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}. stacktrace: {ex.Message}");
                return false;
            }
        }
    }
}

