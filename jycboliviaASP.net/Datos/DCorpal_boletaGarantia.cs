using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_boletaGarantia
    {
        private conexionMySql cnx = new conexionMySql();





        internal bool set_guardarBoletaGarantia(DateTime fecha, decimal monto, string tipoBoleta, 
                                                    string cliente, string estado, int codresp)
        {
            try
            {
                string consulta = @"insert into tbcorpal_boletagarantia (
                                    fechagra, horagra, fecha, monto, tipoboleta, cliente, estado, codrespgra) 
                                     values (current_date, current_time, @fecha, @monto, @tipoboleta, 
                                    @cliente, @estado, @codrespgra)";

                using (MySqlCommand cmd = new MySqlCommand(consulta))
                {
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@monto", monto);
                    cmd.Parameters.AddWithValue("@tipoboleta", tipoBoleta);
                    cmd.Parameters.AddWithValue("@cliente", cliente);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@codrespgra", codresp);

                    return cnx.ejecutarMySql2(cmd);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error en la consulta. " + ex.Message);
            }
        }

        internal bool update_datosBoletaGarantia(decimal monto, string tipoBoleta, string cliente, 
                                                    string estado, int id)
        {
            try
            {
                string consulta = @"update tbcorpal_boletagarantia 
                                   set monto = @monto,
                                   tipoboleta = @tipoBoleta,
                                   cliente = @cliente,
                                   estado = @estado 
                                   where id = @id;";
                using (MySqlCommand cmd = new MySqlCommand(consulta))
                {
                    cmd.Parameters.AddWithValue("@monto", monto);
                    cmd.Parameters.AddWithValue("@tipoboleta", tipoBoleta);
                    cmd.Parameters.AddWithValue("@cliente", cliente);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@id", id);

                    return cnx.ejecutarMySql2(cmd);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error al ejecutar la actualizacion. " + ex.Message);
            }
        }



        internal DataSet get_getBoletasGarantia()
        {
            try
            {
                string consulta = @"
                        SELECT
                            id,
                            DATE_FORMAT(fechagra, '%d/%m/%Y') as fechagra,
                            TIME_FORMAT(horagra, '%H:%i:%s') as horagra,
                            fecha,
                            monto,
                            tipoboleta,
                            cliente,
                            estado,
                            codrespgra
                        FROM tbcorpal_boletagarantia
                        ORDER BY id DESC;";

                return cnx.consultaMySql(consulta);

            }
                catch(Exception ex)
            {
                throw new Exception("Error al obtener datos. " + ex.Message);
            }
        }




    }
}