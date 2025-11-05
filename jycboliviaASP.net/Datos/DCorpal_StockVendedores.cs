using jycboliviaASP.net.Negocio;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_StockVendedores
    {
        private conexionMySql cnx = new conexionMySql();


        internal bool POST_registroEntradaStock(int codVendedor, int codProducto, string producto, decimal cantidad)
        {
            try
            {
                string consulta = @"insert into tbcorpal_stockvendedores 
                    (codvendedor, codproducto, producto, cantidad) 
                    values(@codVendedor, @codProd, @prod, @cantidad) 
                    ON DUPLICATE KEY UPDATE 
                    cantidad = cantidad + VALUES(cantidad)";
                using (MySqlCommand cmd = new MySqlCommand(consulta))
                {
                    cmd.Parameters.AddWithValue("@codVendedor", codVendedor);
                    cmd.Parameters.AddWithValue("@codProd", codProducto);
                    cmd.Parameters.AddWithValue("@prod", producto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);

                    bool result = cnx.ejecutarMySql2(cmd);
                    return result;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error en la consulta Registro Stock" + ex.Message);
                return false;
            }
        }

        internal DataSet GET_verStockProductosVendedor (int codigo)
        {
            try
            {
                string consulta = @"select sv.codproducto, sv.producto, 
                                sv.cantidad, sv.codvendedor from 
                                tbcorpal_stockvendedores sv where sv.codvendedor = @cod group by sv.producto";
                var parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("@cod", codigo)
                };
                DataSet ds = cnx.consultaMySqlParametros(consulta, parametros);
                return ds;
            }
            catch(Exception ex)
            {
                throw new Exception("Error en la consulta al obtener datos Del Stock. " + ex.Message);
            }
        }


    }
}