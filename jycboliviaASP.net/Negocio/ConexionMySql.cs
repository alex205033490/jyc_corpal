using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.Entity.Core.Metadata.Edm;
using jycboliviaASP.net.Presentacion;



namespace jycboliviaASP.net.Negocio
{
    public class conexionMySql
    {
        // Declaramos las variables:
        public MySqlConnection MySqlConexion = new MySqlConnection();
        public MySqlCommand MySqlComando = new MySqlCommand();
        public MySqlDataReader MySqlLectura;        
        //public string CadenaDeConexion = ConfigurationManager.ConnectionStrings["MySqlCuadrosXXX"].ConnectionString;
        public string CadenaDeConexion = "";
        public conexionMySql()
        {
           // string _nombreDB = NA_Global.NombreBaseDatos;
            string _nombreDB = System.Web.HttpContext.Current.Session["NombreBaseDatos"].ToString();
            switch (_nombreDB)
            {
                case "db_prueba":                    
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["MySqlCuadrosXXX"].ConnectionString;
                    break;
                case "db_SantaCruz":                    
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_SantaCruz"].ConnectionString;
                    break;
                case "db_Cochabamba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Cochabamba"].ConnectionString;
                    break;
                case "db_LaPaz":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_LaPaz"].ConnectionString;
                    break;
                case "db_Sucre":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Sucre"].ConnectionString;
                    break;
                case "db_Oruro":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Oruro"].ConnectionString;
                    break;
                case "db_Potosi":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Potosi"].ConnectionString;
                    break;
                case "db_Tarija":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Tarija"].ConnectionString;
                    break;
                case "db_Yacuiba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Yacuiba"].ConnectionString;
                    break;
                case "db_Villamontes":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Villamontes"].ConnectionString;
                    break;
                case "db_Paraguay":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Paraguay"].ConnectionString;
                    break;
                case "db_ParaguayNuevo":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_ParaguayNuevo"].ConnectionString;
                    break;
                case "db_jycsrl":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_jycsrl"].ConnectionString;
                    break;
                case "db_jyciasrl":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_jyciasrl"].ConnectionString;
                    break;
                case "db_imven":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_imven"].ConnectionString;
                    break;
                case "db_beni":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_beni"].ConnectionString;
                    break;
                case "db_pando":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_pando"].ConnectionString;
                    break;
                case "db_equipostock":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_equipostock"].ConnectionString;
                    break;
                case "db_corpal":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_corpal"].ConnectionString;
                    break;
                    

                case "db_SantaCruzprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_SantaCruzprueba"].ConnectionString;
                    break;
                case "db_Cochabambaprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Cochabambaprueba"].ConnectionString;
                    break;
                case "db_LaPazprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_LaPazprueba"].ConnectionString;
                    break;
                case "db_alquileres":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_alquileres"].ConnectionString;
                    break;

                default:
                    Console.WriteLine("Default case");
                    break;
            }
            MySqlConexion.ConnectionString = CadenaDeConexion;
        }


        
        public void conexionMySql_Simec()
        {
            // string _nombreDB = NA_Global.NombreBaseDatos;
            string _nombreDB = System.Web.HttpContext.Current.Session["NombreBaseDatos"].ToString();
            switch (_nombreDB)
            {
                case "db_prueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinv_prueba"].ConnectionString;
                    break;
                case "db_SantaCruz":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinvinterlogi_scz"].ConnectionString;
                    break;
                case "db_Cochabamba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinvmelevar_cbba"].ConnectionString;
                    break;
                case "db_LaPaz":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinvelevamerica_lpz"].ConnectionString;
                    break;
                case "db_ParaguayNuevo":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinv2020jycparaguay"].ConnectionString;
                    break;
                case "db_SantaCruzprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinvinterlogi_sczp"].ConnectionString;
                    break;
                case "db_Cochabambaprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinvmelevar_cbbap"].ConnectionString;
                    break;
                case "db_LaPazprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinvelevamerica_lpzp"].ConnectionString;
                    break;   
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            MySqlConexion.ConnectionString = CadenaDeConexion;
        }

        public void conexionMySql_SimecConta()
        {
            // string _nombreDB = NA_Global.NombreBaseDatos;
            string _nombreDB = System.Web.HttpContext.Current.Session["NombreBaseDatos"].ToString();
            switch (_nombreDB)
            {
                case "db_prueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["siscon_prueba"].ConnectionString;
                    break;
                case "db_SantaCruz":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisconinterlogi_scz"].ConnectionString;
                    break;
                case "db_Cochabamba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisconmelevar_cbba"].ConnectionString;
                    break;
                case "db_LaPaz":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisconelevamerica_lpz"].ConnectionString;
                    break;

                case "db_ParaguayNuevo":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["siscon2020jycparaguay"].ConnectionString;
                    break;
                case "db_SantaCruzprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisconinterlogi_sczp"].ConnectionString;
                    break;
                case "db_Cochabambaprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisconmelevar_cbbap"].ConnectionString;
                    break;
                case "db_LaPazprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisconelevamerica_lpzp"].ConnectionString;
                    break;

                default:
                    Console.WriteLine("Default case");
                    break;
            }
            MySqlConexion.ConnectionString = CadenaDeConexion;
        }

        
            public void conexionMySql_ConvSimecJYCIA()
        {
            // string _nombreDB = NA_Global.NombreBaseDatos;
            string _nombreDB = System.Web.HttpContext.Current.Session["NombreBaseDatos"].ToString();
            switch (_nombreDB)
            {
                case "db_prueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["siscon2020jyciaP2"].ConnectionString;
                    break;
                case "db_SantaCruzprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["siscon2020jyciaP2"].ConnectionString;
                    break;
                case "db_Cochabambaprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["siscon2020jyciaP2"].ConnectionString;
                    break;
                case "db_LaPazprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["siscon2020jyciaP2"].ConnectionString;
                    break;
                case "db_SantaCruz":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["siscon2020jycia"].ConnectionString;
                    break;
                case "db_Cochabamba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["siscon2020jycia"].ConnectionString;
                    break;
                case "db_LaPaz":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["siscon2020jycia"].ConnectionString;
                    break;
                case "db_ParaguayNuevo":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["siscon2020jycia"].ConnectionString;
                    break;
                
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            MySqlConexion.ConnectionString = CadenaDeConexion;
        }

        public void conexionMySql_InvSimecJYCIA()
        {
            // string _nombreDB = NA_Global.NombreBaseDatos;
            string _nombreDB = System.Web.HttpContext.Current.Session["NombreBaseDatos"].ToString();
            switch (_nombreDB)
            {
                case "db_prueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinv2020jyciaP2"].ConnectionString;
                    break;
                case "db_SantaCruzprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinv2020jyciaP2"].ConnectionString;
                    break;
                case "db_Cochabambaprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinv2020jyciaP2"].ConnectionString;
                    break;
                case "db_LaPazprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinv2020jyciaP2"].ConnectionString;
                    break;
                case "db_SantaCruz":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinv2020jycia"].ConnectionString;
                    break;
                case "db_Cochabamba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinv2020jycia"].ConnectionString;
                    break;
                case "db_LaPaz":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinv2020jycia"].ConnectionString;
                    break;
                case "db_ParaguayNuevo":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["sisinv2020jycia"].ConnectionString;
                    break;
                
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            MySqlConexion.ConnectionString = CadenaDeConexion;
        }


        public bool Prueba_Conexion()
        {
            MySqlConnection mysql_conexion = new MySqlConnection(CadenaDeConexion);
            try
            {                
                mysql_conexion.Open();
                mysql_conexion.Close();
                return true;
            }
            catch (Exception)
            {
                mysql_conexion.Close();
                return false;
            }
        }
        
        public DataSet consultaMySql(string consulta)
        {
            MySqlComando = new MySqlCommand(consulta);
            MySqlComando.Connection = MySqlConexion;
            MySqlDataAdapter mdatos = new MySqlDataAdapter(MySqlComando);
            MySqlConexion.Open();
            DataSet datosMysql = new DataSet();
            mdatos.Fill(datosMysql);
            MySqlConexion.Close();
            return datosMysql;
        }
        public DataSet consultaMySqlParametros(string consulta, List<MySqlParameter> parametros)
        {
            try
            {
                MySqlComando = new MySqlCommand(consulta, MySqlConexion);

                foreach(var parametro in parametros)
                {
                    MySqlComando.Parameters.Add(parametro);
                }
                MySqlDataAdapter mdatos = new MySqlDataAdapter(MySqlComando);

                MySqlConexion.Open();
                DataSet datosMysql = new DataSet();
                mdatos.Fill(datosMysql);
                return datosMysql;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error en consulta Mysql: {ex.Message}");
                return null;
            }
            finally
            {
                if (MySqlConexion.State == ConnectionState.Open)
                    MySqlConexion.Close();
            }
        }



        public DataSet RellenarConConsulta(DataSet tablaMySql,string consulta)
        {
            MySqlComando = new MySqlCommand(consulta);
            MySqlComando.Connection = MySqlConexion;
            MySqlDataAdapter mdatos = new MySqlDataAdapter(MySqlComando);
            MySqlConexion.Open();
            mdatos.Fill(tablaMySql);
            MySqlConexion.Close();
            return tablaMySql;
        }
        public Boolean ejecutarMySql2(MySqlCommand comando)
        {
            try
            {
                comando.Connection = MySqlConexion; // Asignar la conexión al comando
                MySqlConexion.Open();
                comando.ExecuteNonQuery();
                MySqlConexion.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                MySqlConexion.Close(); // Asegúrate de cerrar la conexión
            }
        }
        public object ejecutarScalarObject(MySqlCommand comando)
        {
            try
            {
                comando.Connection = MySqlConexion;
                MySqlConexion.Open();
                var result = comando.ExecuteScalar();
                return result;
            }
            finally
            {
                MySqlConexion.Close();
            }
        }

        public Boolean ejecutarMySql2arg(string consulta, List<MySqlParameter> parametros)
        {
            try
            {
                MySqlComando = new MySqlCommand(consulta, MySqlConexion);

                // Agregar los parámetros de forma dinámica
                foreach (var parametro in parametros)
                {
                    MySqlComando.Parameters.Add(parametro);
                }

                // Abrir la conexión, ejecutar la consulta y cerrarla
                MySqlConexion.Open();
                MySqlComando.ExecuteNonQuery();
                MySqlConexion.Close();
                return true;
            }
            catch (Exception)
            {
                // En caso de error, cerrar la conexión
                MySqlConexion.Close();
                return false;
            }
        }




        public Boolean ejecutarMySql(string consulta)
        {
            try
            {                
                MySqlComando = new MySqlCommand(consulta);
                MySqlComando.Connection = MySqlConexion;
                MySqlConexion.Open();
                MySqlComando.ExecuteNonQuery();
                MySqlConexion.Close();
                return true;
            }
            catch (Exception) 
            {
                MySqlConexion.Close();
                return false;
            }
        }

        public int ejecutarScalarMySql(string consulta, List<MySqlParameter> parametros)
        {
            try
            {
                int result = -1;
                using (MySqlCommand cmd = new MySqlCommand(consulta, MySqlConexion))
                {
                    if (MySqlConexion.State != ConnectionState.Open)
                        MySqlConexion.Open();

                    foreach(var parametro in parametros)
                    {
                        cmd.Parameters.Add(parametro);
                    }

                    object scalar = cmd.ExecuteScalar();
                    if(scalar != null)
                    {
                        result = Convert.ToInt32(scalar);
                    }
                }
                MySqlConexion.Close();
                return result;
            }
            catch(Exception )
            {
                MySqlConexion.Close();
                return -1;
            }
        }


        public conexionMySql(string baseDeDatos)
        {
            string _nombreDB = baseDeDatos;
            switch (_nombreDB)
            {
                case "db_prueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["MySqlCuadrosXXX"].ConnectionString;
                    break;
                case "db_SantaCruz":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_SantaCruz"].ConnectionString;
                    break;
                case "db_Cochabamba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Cochabamba"].ConnectionString;
                    break;
                case "db_LaPaz":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_LaPaz"].ConnectionString;
                    break;
                case "db_Sucre":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Sucre"].ConnectionString;
                    break;
                case "db_Oruro":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Oruro"].ConnectionString;
                    break;
                case "db_Potosi":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Potosi"].ConnectionString;
                    break;
                case "db_Tarija":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Tarija"].ConnectionString;
                    break;
                case "db_Yacuiba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Yacuiba"].ConnectionString;
                    break;
                case "db_Villamontes":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Villamontes"].ConnectionString;
                    break;
                case "db_Paraguay":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Paraguay"].ConnectionString;
                    break;
                case "db_ParaguayNuevo":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_ParaguayNuevo"].ConnectionString;
                    break;
                case "db_jycsrl":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_jycsrl"].ConnectionString;
                    break;
                case "db_jyciasrl":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_jyciasrl"].ConnectionString;
                    break;
                case "db_imven":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_imven"].ConnectionString;
                    break;
                case "db_beni":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_beni"].ConnectionString;
                    break;
                case "db_pando":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_pando"].ConnectionString;
                    break;
                case "db_equipostock":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_equipostock"].ConnectionString;
                    break;


                case "db_SantaCruzprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_SantaCruzprueba"].ConnectionString;
                    break;
                case "db_Cochabambaprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_Cochabambaprueba"].ConnectionString;
                    break;
                case "db_LaPazprueba":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_LaPazprueba"].ConnectionString;
                    break;
                case "db_alquileres":
                    CadenaDeConexion = ConfigurationManager.ConnectionStrings["db_alquileres"].ConnectionString;
                    break;

                default:
                    Console.WriteLine("Default case");
                    break;
            }
            MySqlConexion.ConnectionString = CadenaDeConexion;
        }
    

    }
}