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
                "and area like '%" + area+ "%';";

            return conexion.consultaMySql(consulta);
        }

        internal bool anular_RegistroExtintor(List<int> codigo)
        {
            if (codigo == null || codigo.Count == 0)
                return false;

            List<string> parametros = new List<string>();
            for(int i = 0; i < codigo.Count; i++)
            {
                parametros.Add("@codigo" + i);
            }

            string consulta = "UPDATE tbcorpal_extintores ex " +
                "set ex.estado = 0 where ex.codigo in (" + string.Join(",", parametros) + ");";

            MySqlCommand comando = new MySqlCommand(consulta);

            for(int i = 0; i<codigo.Count; i++)
            {                                          
                comando.Parameters.AddWithValue(parametros[i], codigo[i]);
            }
            return conexion.ejecutarMySql2(comando);
        }

        internal bool update_RegistrosExtintor(List<int> codigos, Dictionary<int, Dictionary<string, object>> parametrosPorCodigo)
        {
            if (codigos == null || codigos.Count == 0)
                return false;

            List<String> parametros = new List<string>();
            for(int i = 0; i < codigos.Count; i++)
            {
                parametros.Add("@codigo" + i);
            }
            string consulta = "update tbcorpal_extintores ex set ex.detalle = @detalle, ex.area = @area, ex.agenteextintor = @aextintor, " +
                    "ex.marca = @marca, ex.capacidad = @capacidad, ex.codSistema = @codsistema, ex.fechadecarga = @fechadecarga, " +
                    "ex.fechaproximacarga = @fechaProximaCarga, ex.estadoextintor = @estadoExtintor, ex.anioproximapruebahidrostatica = @anioProxPruebaH " +
                    "where ex.codigo IN ("+string.Join(",", parametros)+") AND ex.estado = 1";

            MySqlCommand comando = new MySqlCommand(consulta);

            for (int i = 0; i < codigos.Count; i++)
            {
                comando.Parameters.AddWithValue("@codigo" + i, codigos[i]);

                if (parametrosPorCodigo.ContainsKey(codigos[i]))
                {
                    var parametrosCodigo = parametrosPorCodigo[codigos[i]];

                    comando.Parameters.AddWithValue("@detalle",parametrosCodigo["detalle"]);
                    comando.Parameters.AddWithValue("@area", parametrosCodigo["area"] );
                    comando.Parameters.AddWithValue("@aextintor", parametrosCodigo["agenteextintor"] );
                    comando.Parameters.AddWithValue("@marca", parametrosCodigo["marca"] );
                    comando.Parameters.AddWithValue("@capacidad", parametrosCodigo["capacidad"] );
                    comando.Parameters.AddWithValue("@codSistema", parametrosCodigo["codSistema"] );
                    comando.Parameters.AddWithValue("@fechadecarga", parametrosCodigo["fechadecarga"] );
                    comando.Parameters.AddWithValue("@fechaProximaCarga", parametrosCodigo["fechaproximacarga"] );
                    comando.Parameters.AddWithValue("@estadoExtintor", parametrosCodigo["estadoextintor"] );
                    comando.Parameters.AddWithValue("@anioProxPruebaH", parametrosCodigo["anioproximapruebahidrostatica"] );
                }
            }
            try
            {
                return conexion.ejecutarMySql2(comando);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}

