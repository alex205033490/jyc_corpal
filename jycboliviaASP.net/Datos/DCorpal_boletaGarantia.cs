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





        internal bool set_guardarBoletaGarantia(DateTime fecha, string moneda, decimal montonumeral, string tipoBoleta, 
                                                    string beneficiario, string cliente, string estado, int codresp, 
                                                    DateTime finicio, DateTime fvencimiento, string obj, string extencionobj)
        {
            try
            {
                string consulta = @"insert into tbcorpal_boletagarantia (
                                    fechagra, horagra, fecha, moneda, montonumeral, tipoboleta, 
                                    beneficiario, cliente, estado, codrespgra, 
                                    fechainicio, fechavencimiento, objeto, extencionobjeto) 
                                    values (current_date, current_time, @fecha, @moneda, @montonumeral, @tipoboleta,
                                    @beneficiario, @cliente, @estado, @codrespgra, 
                                    @fechainicio, @fechavencimiento, @objeto, @extencionobjeto)";

                using (MySqlCommand cmd = new MySqlCommand(consulta))
                {
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@moneda", moneda);
                    cmd.Parameters.AddWithValue("@montonumeral", montonumeral);
                    cmd.Parameters.AddWithValue("@tipoboleta", tipoBoleta);
                    cmd.Parameters.AddWithValue("@beneficiario", beneficiario);
                    cmd.Parameters.AddWithValue("@cliente", cliente);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@codrespgra", codresp);
                    cmd.Parameters.AddWithValue("@fechainicio", finicio);
                    cmd.Parameters.AddWithValue("@fechavencimiento", fvencimiento);
                    cmd.Parameters.AddWithValue("@objeto", obj);
                    cmd.Parameters.AddWithValue("@extencionobjeto", extencionobj);


                    return cnx.ejecutarMySql2(cmd);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error en la consulta. " + ex.Message);
            }
        }

        internal bool update_datosBoletaGarantia(string moneda, decimal monto, string tipoBoleta, 
                                                string beneficiario,string cliente, string estado, 
                                                DateTime finicio, DateTime fvencimiento, string obj, 
                                                string extencionobj, int id)
        {
            try
            {
                string consulta = @"update tbcorpal_boletagarantia 
                                   set 
                                   moneda = @moneda, montonumeral = @monto, tipoboleta = @tipoBoleta,
                                   beneficiario = @beneficiario, cliente = @cliente, estado = @estado,
                                   fechainicio = @finicio, fechavencimiento = @fvencimiento, 
                                   objeto = @objeto, extencionobjeto = @extencionobjeto 
                                   where id = @id;";
                using (MySqlCommand cmd = new MySqlCommand(consulta))
                {
                    cmd.Parameters.AddWithValue("@moneda", moneda);
                    cmd.Parameters.AddWithValue("@monto", monto);
                    cmd.Parameters.AddWithValue("@tipoboleta", tipoBoleta);
                    cmd.Parameters.AddWithValue("@beneficiario", beneficiario);
                    cmd.Parameters.AddWithValue("@cliente", cliente);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@finicio", finicio);
                    cmd.Parameters.AddWithValue("@fvencimiento", fvencimiento);
                    cmd.Parameters.AddWithValue("@objeto", obj);
                    cmd.Parameters.AddWithValue("@extencionobjeto", extencionobj);
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
                            id, DATE_FORMAT(fechagra, '%d/%m/%Y') as fechagra, TIME_FORMAT(horagra, '%H:%i:%s') as horagra,
                            fecha, moneda, montonumeral, 
                            tipoboleta, beneficiario, cliente,
                            estado, codrespgra, date_format(fechainicio, '%d/%m/%Y') as fechainicio, 
                            date_format(fechavencimiento, '%d/%m/%Y') as fechavencimiento, objeto, extencionobjeto 
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