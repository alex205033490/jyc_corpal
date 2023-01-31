using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.DatosSimec
{
    public class DA_contabilidad
    {
        public DA_contabilidad() { }

        public bool insertarCobranza_Glosa(string NUMERO, string GLOSA, string OBSERVACIONES, string USUARIO, float TipoCambio, string fechaPago)
        {
            string consulta = "insert into glosa( " +
                                 " NUMERO, GLOSA , FECHA,  " +
                                 " OBSERVACIONES, USUARIO ,  " +
                                 " FECHAGRA, HORAGRA , FACTOR) " +
                                 " values( " +
                                 " '" + NUMERO + "', '" + GLOSA + "' , " + fechaPago + ", " +
                                 " '" + OBSERVACIONES + "', '" + USUARIO + "',  " +
                                 " current_date, current_time , '" + TipoCambio.ToString().Replace(",", ".") + "')";
            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_SimecConta();
            return cnx.ejecutarMySql(consulta);
        }


        public bool insertarCobranza_Glosa_JYC(string NUMERO, string GLOSA, string OBSERVACIONES, string USUARIO, float TipoCambio, string fechaPago)
        {
            string consulta = "insert into glosa( " +
                                 " NUMERO, GLOSA , FECHA,  " +
                                 " OBSERVACIONES, USUARIO ,  " +
                                 " FECHAGRA, HORAGRA , FACTOR) " +
                                 " values( " +
                                 " '" + NUMERO + "', '" + GLOSA + "' , " + fechaPago + ", " +
                                 " '" + OBSERVACIONES + "', '" + USUARIO + "',  " +
                                 " current_date, current_time , '" + TipoCambio.ToString().Replace(",", ".") + "')";
            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_ConvSimecJYCIA();
            return cnx.ejecutarMySql(consulta);
        }

        public string get_UltimoNumContabilidad_Glosa(string baseDatos)
        {
            string abreviatura = "";
            string consulta = "";
            if (baseDatos.Equals("Santa Cruz") || baseDatos.Equals("Prueba Santa Cruz"))
            {
                abreviatura = "SC";
                consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(current_date, '%y%m'),'I')";
            }
            else
                if (baseDatos.Equals("Cochabamba") || baseDatos.Equals("Prueba Cochabamba"))
                {
                    abreviatura = "CB";
                    consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(current_date, '%y%m'),'I')";
                }
                else
                    if (baseDatos.Equals("La Paz") || baseDatos.Equals("Prueba La Paz"))
                    {
                        abreviatura = "LP";
                        consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(current_date, '%y%m'),'I')";
                    }
                    else
                        if (baseDatos.Equals("Asuncion-Nuevo") || baseDatos.Equals("Asuncion-Paraguay"))
                        {
                            abreviatura = "PY";
                            consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(current_date, '%y%m'),'I')";
                        }
                        else

                        if (baseDatos.Equals("Prueba"))
                        {
                            abreviatura = "SC";
                            consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(current_date, '%y%m'),'I')";
                        }


            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_SimecConta();
            DataSet tupla = cnx.consultaMySql(consulta);
            string formatoNum = "";
            string numStr = "";
            if (tupla.Tables[0].Rows.Count > 0)
            {
                formatoNum = tupla.Tables[0].Rows[0][0].ToString();
               /* string consulta2 = "select max(substring(gg.NUMERO,-4,4)) from glosa gg " +
                                   " where " +
                                   " gg.NUMERO like '%" + formatoNum + "%'";
                */
                string consulta2 = "select max(substring(gg.NUMERO,-3,3)) from glosa gg " +
                                   " where " +
                                   " gg.NUMERO like '%" + formatoNum + "%'";
                
                DataSet tupla2 = cnx.consultaMySql(consulta2);
                numStr = tupla2.Tables[0].Rows[0][0].ToString();
            }

            if (!numStr.Equals(""))
            {
                return (formatoNum + numStr);
            }
            else
                return (formatoNum + "0000");


        }


        public string getNumCorrelativoContabilidad_Glosa(string baseDatos, string fechaPago)
        {

            string abreviatura = "";
            string consulta = "";
            if (baseDatos.Equals("Santa Cruz") || baseDatos.Equals("Prueba Santa Cruz"))
            {
                abreviatura = "SC";
                consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(" + fechaPago + ", '%y%m'),'I')";
            }
            else
                if (baseDatos.Equals("Cochabamba") || baseDatos.Equals("Prueba Cochabamba"))
                {
                    abreviatura = "CB";
                    consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(" + fechaPago + ", '%y%m'),'I')";
                }
                else
                    if (baseDatos.Equals("La Paz") || baseDatos.Equals("Prueba La Paz"))
                    {
                        abreviatura = "LP";
                        consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(" + fechaPago + ", '%y%m'),'I')";
                    }
                    else
                        if (baseDatos.Equals("Asuncion-Nuevo") || baseDatos.Equals("Asuncion-Paraguay"))
                        {
                            abreviatura = "PY";
                            consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(current_date, '%y%m'),'I')";
                        }
                        else
                        if (baseDatos.Equals("Prueba"))
                        {
                            abreviatura = "SC";
                            consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(" + fechaPago + ", '%y%m'),'I')";
                        }

            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_SimecConta();
            DataSet tupla = cnx.consultaMySql(consulta);
            string formatoNum = "";
            string numStr = "";
            if (tupla.Tables[0].Rows.Count > 0)
            {
                formatoNum = tupla.Tables[0].Rows[0][0].ToString();
               /* string consulta2 = "select max(substring(gg.NUMERO,-4,4)) from glosa gg " +
                                   " where " +
                                   " gg.NUMERO like '%" + formatoNum + "%'";
                */
                string consulta2 = "select max(substring(gg.NUMERO,-3,3)) from glosa gg " +
                                 " where " +
                                 " gg.NUMERO like '%" + formatoNum + "%'";

            /*    if (baseDatos.Equals("Cochabamba") || baseDatos.Equals("Prueba Cochabamba")||)
                {
                    consulta2 = "select max(substring(gg.NUMERO,-3,3)) from glosa gg " +
                                       " where " +
                                       " gg.NUMERO like '%" + formatoNum + "%'";
                }
                */
                DataSet tupla2 = cnx.consultaMySql(consulta2);
                numStr = tupla2.Tables[0].Rows[0][0].ToString();
            }
            int numCorrelativo = 0;
            // if(!numStr.Equals("")){
            int.TryParse(numStr, out numCorrelativo);
            numCorrelativo = numCorrelativo + 1;
            // }

            string resultado = numCorrelativo.ToString();
            //--------------solo por cocha
            int cantidadDigitos = 3;
       /*     int cantidadDigitos = 4;
            if (baseDatos.Equals("Cochabamba") || baseDatos.Equals("Prueba Cochabamba"))
            {
                cantidadDigitos = 3;
            }*/
            //------------------------

            if (numCorrelativo.ToString().Length < cantidadDigitos)
            {
                for (int i = numCorrelativo.ToString().Length; i < cantidadDigitos; i++)
                {
                    resultado = "0" + resultado;
                }
            }
            return (formatoNum + resultado);
        }


        public string getNumCorrelativoContabilidad_Glosa_JYC(string baseDatos, string fechaPago)
        {

            string abreviatura = "";
            string consulta = "";
            if (baseDatos.Equals("Santa Cruz") || baseDatos.Equals("Prueba Santa Cruz"))
            {
                abreviatura = "SC";
                consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(" + fechaPago + ", '%y%m'),'T')";
            }
            else
                if (baseDatos.Equals("Cochabamba") || baseDatos.Equals("Prueba Cochabamba"))
                {
                    abreviatura = "CB";
                    consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(" + fechaPago + ", '%y%m'),'T')";
                }
                else
                    if (baseDatos.Equals("La Paz") || baseDatos.Equals("Prueba La Paz"))
                    {
                        abreviatura = "LP";
                        consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(" + fechaPago + ", '%y%m'),'T')";
                    }
                    else
                        if (baseDatos.Equals("Asuncion-Nuevo") || baseDatos.Equals("Asuncion-Paraguay"))
                        {
                            abreviatura = "PY";
                            consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(current_date, '%y%m'),'T')";
                        }
                        else
                            if (baseDatos.Equals("Prueba"))
                            {
                                abreviatura = "SC";
                                consulta = "select  concat('" + abreviatura + "',DATE_FORMAT(" + fechaPago + ", '%y%m'),'T')";
                            }

            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_ConvSimecJYCIA();
            DataSet tupla = cnx.consultaMySql(consulta);
            string formatoNum = "";
            string numStr = "";
            if (tupla.Tables[0].Rows.Count > 0)
            {
                formatoNum = tupla.Tables[0].Rows[0][0].ToString();
                /* string consulta2 = "select max(substring(gg.NUMERO,-4,4)) from glosa gg " +
                                    " where " +
                                    " gg.NUMERO like '%" + formatoNum + "%'";
                 */
                string consulta2 = "select max(substring(gg.NUMERO,-3,3)) from glosa gg " +
                                 " where " +
                                 " gg.NUMERO like '%" + formatoNum + "%'";

                /*    if (baseDatos.Equals("Cochabamba") || baseDatos.Equals("Prueba Cochabamba")||)
                    {
                        consulta2 = "select max(substring(gg.NUMERO,-3,3)) from glosa gg " +
                                           " where " +
                                           " gg.NUMERO like '%" + formatoNum + "%'";
                    }
                    */
                DataSet tupla2 = cnx.consultaMySql(consulta2);
                numStr = tupla2.Tables[0].Rows[0][0].ToString();
            }
            int numCorrelativo = 0;
            // if(!numStr.Equals("")){
            int.TryParse(numStr, out numCorrelativo);
            numCorrelativo = numCorrelativo + 1;
            // }

            string resultado = numCorrelativo.ToString();
            //--------------solo por cocha
            int cantidadDigitos = 3;
            /*     int cantidadDigitos = 4;
                 if (baseDatos.Equals("Cochabamba") || baseDatos.Equals("Prueba Cochabamba"))
                 {
                     cantidadDigitos = 3;
                 }*/
            //------------------------

            if (numCorrelativo.ToString().Length < cantidadDigitos)
            {
                for (int i = numCorrelativo.ToString().Length; i < cantidadDigitos; i++)
                {
                    resultado = "0" + resultado;
                }
            }
            return (formatoNum + resultado);
        }

        internal bool insertCobranza_Conta(string CBTE, string NCTA, string DETALLE, double FACTOR, int ORDEN, int tipoMoneda, double monto_pago, bool Debe_bool, string baseDatos, string fechaPago)
        {
            double monto_pagoBS = 0;
            double monto_pagoSus = 0;
            string regionalProyecto = "";
            NA_VariablesGlobales vg = new NA_VariablesGlobales();
            regionalProyecto = vg.get_VCAJAenbasedeDatosActual(baseDatos).ToString();


            if (tipoMoneda == 1)    // cuando el monto es en bolivianos
            {
                monto_pagoSus = (double)Math.Round(((double)monto_pago / (double)FACTOR), 2);
                monto_pagoBS = (double)Math.Round((double)monto_pago, 2);
            }
            else
                if (tipoMoneda == 2)
                {
                    monto_pagoSus = (double)Math.Round((double)monto_pago, 2);
                    monto_pagoBS = (double)Math.Round(((double)monto_pago * (double)FACTOR), 2);
                }



            string DebeBS = "0";
            string DebeSus = "0";
            string HaberBS = "0";
            string HaberSus = "0";

            if (Debe_bool == true)
            {
                DebeBS = monto_pagoBS.ToString();
                DebeSus = monto_pagoSus.ToString();
            }

            if (Debe_bool == false)
            {
                HaberBS = monto_pagoBS.ToString();
                HaberSus = monto_pagoSus.ToString();
            }
            
            string consulta = "insert into conta( " +
                              " CBTE, FECHA, NCTA, " +
                              " DETALLE, DEBE, HABER, " +
                              " FACTOR , PROYECTO,  " +
                              " ORDEN, DEBEUS, HABERUS " +
                              " ) values( " +
                              " '" + CBTE + "', " + fechaPago + ", '" + NCTA + "', " +
                              " '" + DETALLE + "', '" + DebeBS.Replace(",", ".") + "', '" + HaberBS.Replace(",", ".") + "', " +
                              " '" + Math.Round(FACTOR, 2).ToString().Replace(",", ".") + "' , '" + regionalProyecto + "', " +
                              ORDEN + " , '" + DebeSus.Replace(",", ".") + "', '" + HaberSus.Replace(",", ".") + "')";
            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_SimecConta();
            return cnx.ejecutarMySql(consulta);
        }

        internal bool insertCobranza_Conta_JYC(string CBTE, string NCTA, string DETALLE, double FACTOR, int ORDEN, int tipoMoneda, double monto_pago, bool Debe_bool, string baseDatos, string fechaPago, string FINANCIA, string ACTIVIDAD)
        {
            double monto_pagoBS = 0;
            double monto_pagoSus = 0;
            string regionalProyecto = "";
            NA_VariablesGlobales vg = new NA_VariablesGlobales();
            regionalProyecto = vg.get_VCAJAenbasedeDatosActual(baseDatos).ToString();
            

            if (tipoMoneda == 1)    // cuando el monto es en bolivianos
            {
                monto_pagoSus = (double)Math.Round(((double)monto_pago / (double)FACTOR), 2);
                monto_pagoBS = (double)Math.Round((double)monto_pago, 2);
            }
            else
                if (tipoMoneda == 2)
                {
                    monto_pagoSus = (double) Math.Round((double)monto_pago, 2);
                    monto_pagoBS = (double)Math.Round(((double)monto_pago * (double)FACTOR),2);
                }



            string DebeBS = "0";
            string DebeSus = "0";
            string HaberBS = "0";
            string HaberSus = "0";

            if (Debe_bool == true)
            {
                DebeBS = monto_pagoBS.ToString();
                DebeSus = monto_pagoSus.ToString();
            }

            if (Debe_bool == false)
            {
                HaberBS = monto_pagoBS.ToString();
                HaberSus = monto_pagoSus.ToString();
            }




            string consulta = "insert into conta( " +
                              " CBTE, FECHA, NCTA, " +
                              " DETALLE, DEBE, HABER, " +
                              " FACTOR , PROYECTO,  " +
                              " ORDEN, DEBEUS, HABERUS, FINANCIA, ACTIVIDAD " +
                              " ) values( " +
                              " '" + CBTE + "', " + fechaPago + ", '" + NCTA + "', " +
                              " '" + DETALLE + "', '" + DebeBS.Replace(",", ".") + "', '" + HaberBS.Replace(",", ".") + "', " +
                              " '" + Math.Round(FACTOR,2).ToString().Replace(",", ".") + "' , '" + regionalProyecto + "', " +
                              ORDEN + " , '" + DebeSus.Replace(",", ".") + "', '" + HaberSus.Replace(",", ".") + "', '"+FINANCIA+"', '"+ACTIVIDAD+"')";
            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_ConvSimecJYCIA();
            return cnx.ejecutarMySql(consulta);
        }
    }



    
}