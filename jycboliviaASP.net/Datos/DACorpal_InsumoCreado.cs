using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using static AjaxControlToolkit.AsyncFileUpload.Constants;
using System.Globalization;

namespace jycboliviaASP.net.Datos
{
    public class DACorpal_InsumoCreado
    {
        private conexionMySql ConecIns = new conexionMySql();

        public DACorpal_InsumoCreado(){}

        // INSERT INSUMOCREADO tbcorpal_insumoscreado
        public int insertarInsumoCreado(string nombre, string medida)
        {
            try
            {
                string consulta = "INSERT INTO tbcorpal_insumoscreados (nombre, fecha, hora, medida, estado) " +
                              "VALUES (@nombre, current_date(), current_time(), @medida, 1)" ;

                var comando = new MySqlCommand(consulta);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@medida", medida);

                if (ConecIns.ejecutarMySql2(comando))
                {
                    string consultaId = "SELECT LAST_INSERT_ID();";
                    DataSet ds = ConecIns.consultaMySql(consultaId);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]); // Retornar el ID generado
                }
                }
                return -1; // Indicar error
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1; // Indicar error
            }
        }

        // INSERT DETALLES INSUMOCREADO tbcorpal_detinsumocreado
        public bool insertarDetInsumoCreado(int codinsumo, int codinsumocreado, string cantidad, string medida)
        {
            try
            {
                string consulta = "INSERT INTO tbcorpal_detinsumocreado (codinsumo, codinsumocreado, cantidad, medida) " +
                              "VALUES (@codinsumo, @codinsumocreado, @cantidad, @medida);";
                
                var comando = new MySqlCommand(consulta);
                comando.Parameters.AddWithValue("@codinsumo", codinsumo);
                comando.Parameters.AddWithValue("@codinsumocreado", codinsumocreado);
                comando.Parameters.AddWithValue("@cantidad", cantidad);
                comando.Parameters.AddWithValue("@medida", medida);

                return ConecIns.ejecutarMySql2(comando);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        // CONSULTA 
        public DataSet getDatos(string consulta)
        {
            DataSet datosI = ConecIns.consultaMySql(consulta);
            return datosI;
        }

        public bool UpdateEstadoInsumoCreado(int codInsumoCreado)
        {
            string update = "UPDATE tbcorpal_insumoscreados as ic set ic.estado = 0 where ic.codigo = " + codInsumoCreado + ";";
            MySqlCommand comando = new MySqlCommand(update);
            return ConecIns.ejecutarMySql2(comando);
        }

        public bool ModificarDetInsumoCreado(int codInsumoCreado, string cantidadInsumo, int codInsumo)
        {
            if (!decimal.TryParse(cantidadInsumo, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cantidadDecimal))
            {
                throw new ArgumentException("Cantidad no es un valor decimal válido.");
            }

                string update = "CALL UpdateCantidadInsumo(" + codInsumoCreado + ", " + cantidadInsumo + ", " + codInsumo + ");";
            MySqlCommand comando = new MySqlCommand(update);
            return ConecIns.ejecutarMySql2(comando);
            /*
             DELIMITER //
                CREATE PROCEDURE UpdateCantidadInsumo(
                    IN p_codICreado INT,
                    IN p_nuevaCantidad DECIMAL(10, 6),
                    IN p_codInsumo INT
                )
                BEGIN
                    UPDATE tbcorpal_insumo as ins
                    INNER JOIN tbcorpal_detinsumocreado AS dic ON ins.codigo = dic.codinsumo
                    INNER JOIN tbcorpal_insumoscreados AS ic ON ic.codigo = dic.codinsumocreado
                    SET dic.cantidad = p_nuevaCantidad
                    WHERE ic.codigo = p_codICreado
                    AND ins.codigo = p_codInsumo;
                END //
                DELIMITER ;
            */

        }


    }
}