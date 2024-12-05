using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_VariablesGlobales
    {
        public static int meseslimitesdeAtrazadosPermitidosMantenimiento = 2;
        //"db_seguimientoscz_jyc", "db_seguimientocbba_jyc", "db_seguimientolpz_jyc", "db_seguimientosucre_jyc", "db_seguimientotarija_jyc", "db_seguimientooruro_jyc", "db_seguimientopotosi_jyc", "db_seguimientobeni_jyc", "db_seguimientopando_jyc", "db_seguimientoyacuiba_jyc", "db_seguimientovillamontes_jyc", "db_seguimientoparaguay_nuevo" 
        //public static List<string> listBaseDatos = new List<string> { "db_seguimientoprueba_jyc"};
       
        public static List<string> listBaseDatos = new List<string> { "db_seguimientoscz_jyc", "db_seguimientocbba_jyc", "db_seguimientolpz_jyc", "db_seguimientosucre_jyc", "db_seguimientotarija_jyc", "db_seguimientooruro_jyc", "db_seguimientopotosi_jyc", "db_seguimientobeni_jyc", "db_seguimientopando_jyc", "db_seguimientoyacuiba_jyc", "db_seguimientovillamontes_jyc", "db_seguimientoparaguay_nuevo"};
        public static List<string> listNombreBaseDatos = new List<string> { "Santa Cruz", "Cochabamba", "La Paz", "Sucre", "Tarija", "Oruro", "Potosi", "Beni", "Pando", "Yacuiba", "Villamontes", "Asuncion-Paraguay" };
       // public static List<string> listBaseDatos = new List<string> { "db_seguimientoscz_jyc", "db_seguimientocbba_jyc", "db_seguimientolpz_jyc", "db_seguimientoprueba_jyc" };
       // public static List<string> listNombreBaseDatos = new List<string> { "Santa Cruz", "Cochabamba", "La Paz", "Prueba" };

        public static string CiudadParaIngresarDui = "CBBA";
        public static string baseDedatosDui = "db_Cochabamba";
        public static string baseDedatosContenedor = "db_Cochabamba";
        public static string baseDedatosImportacion = "db_jyciasrl";
        //public static string fechaInicialProduccion = "'2023-12-01'";  Saldo Inicial 1
        public static string fechaInicialProduccion = "'2024-03-11'";
        public static string fechaInicialentregaProducto = "'2024-03-12'";

        public int get_VCAJAenbasedeDatosActual(string baseDatos)
        {
            int Vcaja = 0;
            if (baseDatos.Equals("Santa Cruz") || baseDatos.Equals("Prueba Santa Cruz"))
            {
                Vcaja = 30;
            }
            else
                if (baseDatos.Equals("Cochabamba") || baseDatos.Equals("Prueba Cochabamba"))
                {
                    Vcaja = 20;
                }
                else
                    if (baseDatos.Equals("La Paz") || baseDatos.Equals("Prueba La Paz"))
                    {
                        Vcaja = 10;
                    }
                    else
                        if (baseDatos.Equals("Asuncion-Nuevo") || baseDatos.Equals("Asuncion-Paraguay"))
                        {
                            Vcaja = 0;
                        }
                        else
                            if (baseDatos.Equals("Oruro"))
                            {
                                Vcaja = 40;
                            }else
                                if (baseDatos.Equals("Sucre"))
                                {
                                    Vcaja = 50;
                                }
                                else
                                    if (baseDatos.Equals("Potosi"))
                                    {
                                        Vcaja = 60;
                                    }
                                    else
                                        if (baseDatos.Equals("Tarija"))
                                        {
                                            Vcaja = 70;
                                        }
                                        else
                                            if (baseDatos.Equals("Prueba"))
                                            {
                                                Vcaja = 30;
                                            }
            return Vcaja;
        }


        public string get_VNO_CUENTA_enbasedeDatosActual(string baseDatos)
        {
            string Vcuenta = "Ninguno";
            if (baseDatos.Equals("Santa Cruz") || baseDatos.Equals("Prueba Santa Cruz"))
            {
                Vcuenta = "1142*";
            }
            else
                if (baseDatos.Equals("Cochabamba") || baseDatos.Equals("Prueba Cochabamba"))
                {
                    Vcuenta = "1141*";
                }
                else
                    if (baseDatos.Equals("La Paz") || baseDatos.Equals("Prueba La Paz"))
                    {
                        Vcuenta = "1140*";
                    }
                    else
                        if (baseDatos.Equals("Asuncion-Nuevo") || baseDatos.Equals("Asuncion-Paraguay"))
                        {
                            Vcuenta = "Ninguno";
                        }
                        else
                            if (baseDatos.Equals("Oruro"))
                            {
                                Vcuenta = "1143*";
                            }
                            else
                                if (baseDatos.Equals("Sucre"))
                                {
                                    Vcuenta = "1144*";
                                }
                                else
                                    if (baseDatos.Equals("Potosi"))
                                    {
                                        Vcuenta = "1145*";
                                    }
                                    else
                                        if (baseDatos.Equals("Tarija"))
                                        {
                                            Vcuenta = "1146*";
                                        }
                                        else
                                            if (baseDatos.Equals("Prueba"))
                                            {
                                                Vcuenta = "1142*";
                                            }
            return Vcuenta;
        }


        public string getBasedeDatosTemporal(string Ciudad)
        {
            string baseDatos = "";


            if (Ciudad.Equals("SCZ"))
            {
                baseDatos = "db_SantaCruz";
            }
            else
                if (Ciudad.Equals("CBBA"))
                {
                    baseDatos = "db_Cochabamba";
                }
                else
                    if (Ciudad.Equals("LPZ"))
                    {
                        baseDatos = "db_LaPaz";
                    }
                    else
                        if (Ciudad.Equals("Sucre"))
                        {
                            baseDatos = "db_Sucre";
                        }
                        else
                            if (Ciudad.Equals("Oruro"))
                            {
                                baseDatos = "db_Oruro";
                            }
                            else
                                if (Ciudad.Equals("Beni"))
                                {
                                    baseDatos = "db_beni";
                                }
                                else
                                    if (Ciudad.Equals("Pando"))
                                    {
                                        baseDatos = "db_pando";
                                    }
                                    else
                                        if (Ciudad.Equals("Tarija"))
                                        {
                                            baseDatos = "db_Tarija";
                                        }
                                        else
                                            if (Ciudad.Equals("Yacuiba"))
                                            {
                                                baseDatos = "db_Yacuiba";
                                            }
                                            else
                                                if (Ciudad.Equals("Potosi"))
                                                {
                                                    baseDatos = "db_Potosi";
                                                }
                                                else
                                                    if (Ciudad.Equals("Villamontes"))
                                                    {
                                                        baseDatos = "db_Villamontes";
                                                    }
                                                    else
                                                        if (Ciudad.Equals("Paraguay"))
                                                        {
                                                            baseDatos = "db_ParaguayNuevo";
                                                        }
                                                        else
                                                        {
                                                            baseDatos = "db_prueba";
                                                        }
            return baseDatos;
        }

      
        internal string get_VNO_CUENTA_enbasedeDatosActual_Debe(string baseDatos)
        {
            string Vcuenta = "Ninguno";
            if (baseDatos.Equals("Santa Cruz") || baseDatos.Equals("Prueba Santa Cruz"))
            {
                Vcuenta = "2142*";
            }
            else
                if (baseDatos.Equals("Cochabamba") || baseDatos.Equals("Prueba Cochabamba"))
                {
                    Vcuenta = "2141*";
                }
                else
                    if (baseDatos.Equals("La Paz") || baseDatos.Equals("Prueba La Paz"))
                    {
                        Vcuenta = "2140*";
                    }
                    else
                        if (baseDatos.Equals("Asuncion-Nuevo") || baseDatos.Equals("Asuncion-Paraguay"))
                        {
                            Vcuenta = "Ninguno";
                        }
                        else
                            if (baseDatos.Equals("Oruro"))
                            {
                                Vcuenta = "2143*";
                            }
                            else
                                if (baseDatos.Equals("Sucre"))
                                {
                                    Vcuenta = "2144*";
                                }
                                else
                                    if (baseDatos.Equals("Potosi"))
                                    {
                                        Vcuenta = "2145*";
                                    }
                                    else
                                        if (baseDatos.Equals("Tarija"))
                                        {
                                            Vcuenta = "2146*";
                                        }
                                        else
                                            if (baseDatos.Equals("Prueba"))
                                            {
                                                Vcuenta = "2142*";
                                            }
            return Vcuenta;
        }

        internal double get_MontoEstablecido_enbaseaparadas(int paradas)
        {
            conexionMySql cnx = new conexionMySql();
            string consulta = "select "+
                               " ic.paradas, "+
                               " ic.orona, "+
                               " ic.blt_hundai "+
                               " from tb_instalacion_comision ic where ic.paradas = '"+paradas+"'";
            DataSet tuplas = cnx.consultaMySql(consulta);            
                double comision;
                double.TryParse(tuplas.Tables[0].Rows[0][1].ToString(), out comision);
                return comision;
        }

        internal DataSet get_DatosNombreResponsableSimec(string resp_une)
        {
            conexionMySql cnx = new conexionMySql();
            string consulta = "select "+ 
                               " resp.codigo, "+
                               " resp.une, "+
                               " resp.cargo, "+
                               " resp.nombreresponsable, "+
                               " resp.nombreresponsable_simec, "+
                               " resp.monto "+
                               " from tb_resprevision resp "+
                               " where "+
                               " resp.nombreresponsable = '"+resp_une+"'";
            DataSet tuplas = cnx.consultaMySql(consulta);
            return tuplas;
        }

        internal string get_NombreResponsableSimec(string resp_une)
        {
            DataSet tuplas = get_DatosNombreResponsableSimec(resp_une);
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                return tuplas.Tables[0].Rows[0][4].ToString();
            }
            else
               return "Ninguno";
        }

        internal string get_consultaStockProductosActual() {
            /* string consultaStock = "SELECT pp.codigo, pp.producto, pp.medida, ifnull(t1.ingreso,0) as 'Ingreso1', ifnull(t2.salida,0) as 'Salida1', " +
                                       " (ifnull(t1.ingreso,0) - ifnull(t2.salida,0)) as 'StockAlmacen' " +
                                       " FROM tbcorpal_producto pp " +
                                       " LEFT JOIN " +
                                       " ( " +
                                       " select " +
                                       " oo.codProductonax, sum(oo.cantcajas) as 'ingreso', " +
                                       " sum(oo.pack_ferial) as 'itempackferial' " +
                                       " from tbcorpal_entregasordenproduccion oo " +
                                       " where " +
                                       " oo.estado = 1 and " +
                                       " oo.fechagra between " + NA_VariablesGlobales.fechaInicialProduccion + " and current_date() " +
                                       " group by oo.codProductonax " +
                                       " ) as t1  ON pp.codigo = t1.codProductonax " +
                                       " LEFT JOIN " +
                                       " ( " +
                                       " select dss.codproducto, sum(dss.cantentregada) as 'salida' " +                                      
                                       " from tbcorpal_solicitudentregaproducto ss, " +
                                       " tbcorpal_detalle_solicitudproducto dss " +
                                       " where " +
                                       " ss.codigo = dss.codsolicitud and " +
                                       " ss.estado = 1 and " +
                                       " ss.estadosolicitud = 'Cerrado' and " +
                                       " dss.itempackferial is not true and " +
                                       " ss.fechaentrega between " + NA_VariablesGlobales.fechaInicialProduccion + " and current_date() " +
                                       " group by dss.codproducto " +
                                       " ) as t2 ON pp.codigo = t2.codproducto " +
                                       " WHERE " +
                                       " pp.estado = 1"; */
            string consultaStock = "SELECT pp.codigo, pp.producto, pp.medida, ifnull(t1.ingreso,0) as 'Ingreso1', ifnull(t2.salida,0) as 'Salida1',   " +
                " (ifnull(t1.ingreso,0) - ifnull(t2.salidaCajas,0)) as 'StockAlmacen',   " +
                " (ifnull(t1.ingresopackferial,0) - ifnull(t2.salidaPackFerial,0)) as 'StockPackFerial'  " +
                " FROM tbcorpal_producto pp   " +
                " LEFT JOIN    " +
                " (   " +
                " select   " +
                " oo.codProductonax, sum(oo.cantcajas) as 'ingreso',   " +
                " sum(oo.pack_ferial) as 'ingresopackferial'   " +
                " from tbcorpal_entregasordenproduccion oo   " +
                " where   " +
                " oo.estado = 1 and   " +
                " oo.fechagra between "+
                " TIMESTAMP("+ NA_VariablesGlobales.fechaInicialProduccion + ", '07:00:00') and "+
                " TIMESTAMP(DATE_SUB(current_date(), INTERVAL - 1 DAY), '06:00:00') "+
               // + NA_VariablesGlobales.fechaInicialProduccion + " and current_date()   " +
                " group by oo.codProductonax   " +
                " ) as t1  ON pp.codigo = t1.codProductonax   " +
                " LEFT JOIN   " +
                " (   " +
                " select dss.codproducto,  " +
                " sum(dss.cantentregada) as 'salida',  " +
                " sum(  " +
                " case    " +
                " when dss.tiposolicitud <> 'ITEM PACK FERIAL' then dss.cantentregada  " +
                " else 0  " +
                " end)   " +
                " as 'salidaCajas' ,  " +
                " sum(  " +
                " case dss.tiposolicitud   " +
                " when 'ITEM PACK FERIAL' then dss.cantentregada  " +
                " else 0  " +
                " end)   " +
                " as 'salidaPackFerial'                                          " +
                " from tbcorpal_solicitudentregaproducto ss,   " +
                " tbcorpal_detalle_solicitudproducto dss   " +
                " where   " +
                " ss.codigo = dss.codsolicitud and   " +
                " ss.estado = 1 and   " +
                " ss.estadosolicitud = 'Cerrado' and  " +
                " ss.fechaentrega between "+
                " TIMESTAMP(" + NA_VariablesGlobales.fechaInicialProduccion + ", '07:00:00') and " +
                " TIMESTAMP(DATE_SUB(current_date(), INTERVAL - 1 DAY), '06:00:00') " +
                // NA_VariablesGlobales.fechaInicialProduccion + " and current_date()   " +
                " group by dss.codproducto   " +
                " ) as t2 ON pp.codigo = t2.codproducto   " +
                " WHERE   " +
                " pp.estado = 1 ";

            return consultaStock;
        }

        internal string get_consultaStockProductosActual_fecha(string fechaHasta)
        {
            string consultaStock = "SELECT pp.codigo, pp.producto, pp.medida, ifnull(t1.ingreso,0) as 'Ingreso1', ifnull(t2.salida,0) as 'Salida1',   " +
                    " (ifnull(t1.ingreso,0) - ifnull(t2.salidaCajas,0)) as 'StockAlmacen',   " +
                    " (ifnull(t1.ingresopackferial,0) - ifnull(t2.salidaPackFerial,0)) as 'StockPackFerial'  " +
                    " FROM tbcorpal_producto pp   " +
                    " LEFT JOIN    " +
                    " (   " +
                    " select   " +
                    " oo.codProductonax, sum(oo.cantcajas) as 'ingreso',   " +
                    " sum(oo.pack_ferial) as 'ingresopackferial'   " +
                    " from tbcorpal_entregasordenproduccion oo   " +
                    " where   " +
                    " oo.estado = 1 and   " +
                    " oo.fechagra between " +
                    " TIMESTAMP(" + NA_VariablesGlobales.fechaInicialProduccion + ", '07:00:00') and " +
                    " TIMESTAMP(DATE_SUB("+ fechaHasta + ", INTERVAL - 1 DAY), '06:00:00') " +
                    //NA_VariablesGlobales.fechaInicialProduccion + " and " + fechaHasta+
                    " group by oo.codProductonax   " +
                    " ) as t1  ON pp.codigo = t1.codProductonax   " +
                    " LEFT JOIN   " +
                    " (   " +
                    " select dss.codproducto,  " +
                    " sum(dss.cantentregada) as 'salida',  " +
                    " sum(  " +
                    " case    " +
                    " when dss.tiposolicitud <> 'ITEM PACK FERIAL' then dss.cantentregada  " +
                    " else 0  " +
                    " end)   " +
                    " as 'salidaCajas' ,  " +
                    " sum(  " +
                    " case dss.tiposolicitud   " +
                    " when 'ITEM PACK FERIAL' then dss.cantentregada  " +
                    " else 0  " +
                    " end)   " +
                    " as 'salidaPackFerial'                                          " +
                    " from tbcorpal_solicitudentregaproducto ss,   " +
                    " tbcorpal_detalle_solicitudproducto dss   " +
                    " where   " +
                    " ss.codigo = dss.codsolicitud and   " +
                    " ss.estado = 1 and   " +
                    " ss.estadosolicitud = 'Cerrado' and  " +
                    " ss.fechaentrega between " +
                    " TIMESTAMP(" + NA_VariablesGlobales.fechaInicialProduccion + ", '07:00:00') and " +
                    " TIMESTAMP(DATE_SUB(" + fechaHasta + ", INTERVAL - 1 DAY), '06:00:00') " +
                    // NA_VariablesGlobales.fechaInicialProduccion + " and " + fechaHasta+
                    " group by dss.codproducto   " +
                    " ) as t2 ON pp.codigo = t2.codproducto   " +
                    " WHERE   " +
                    " pp.estado = 1 ";

            return consultaStock;
        }
        internal string get_consultaStockProductosActual(int codigoProducto)
        {
            string consultaStock = "SELECT pp.codigo, pp.producto, pp.medida, ifnull(t1.ingreso,0) as 'Ingreso1', ifnull(t2.salida,0) as 'Salida1',   " +
                    " (ifnull(t1.ingreso,0) - ifnull(t2.salidaCajas,0)) as 'StockAlmacen',   " +
                    " (ifnull(t1.ingresopackferial,0) - ifnull(t2.salidaPackFerial,0)) as 'StockPackFerial'  " +
                    " FROM tbcorpal_producto pp   " +
                    " LEFT JOIN    " +
                    " (   " +
                    " select   " +
                    " oo.codProductonax, sum(oo.cantcajas) as 'ingreso',   " +
                    " sum(oo.pack_ferial) as 'ingresopackferial'   " +
                    " from tbcorpal_entregasordenproduccion oo   " +
                    " where   " +
                    " oo.estado = 1 and   " +
                    " oo.fechagra between " +
                    " TIMESTAMP(" + NA_VariablesGlobales.fechaInicialProduccion + ", '07:00:00') and " +
                    " TIMESTAMP(DATE_SUB(current_date(), INTERVAL - 1 DAY), '06:00:00') " +
                    // NA_VariablesGlobales.fechaInicialProduccion + " and current_date()   " +
                    " group by oo.codProductonax   " +
                    " ) as t1  ON pp.codigo = t1.codProductonax   " +
                    " LEFT JOIN   " +
                    " (   " +
                    " select dss.codproducto,  " +
                    " sum(dss.cantentregada) as 'salida',  " +
                    " sum(  " +
                    " case    " +
                    " when dss.tiposolicitud <> 'ITEM PACK FERIAL' then dss.cantentregada  " +
                    " else 0  " +
                    " end)   " +
                    " as 'salidaCajas' ,  " +
                    " sum(  " +
                    " case dss.tiposolicitud   " +
                    " when 'ITEM PACK FERIAL' then dss.cantentregada  " +
                    " else 0  " +
                    " end)   " +
                    " as 'salidaPackFerial'                                          " +
                    " from tbcorpal_solicitudentregaproducto ss,   " +
                    " tbcorpal_detalle_solicitudproducto dss   " +
                    " where   " +
                    " ss.codigo = dss.codsolicitud and   " +
                    " ss.estado = 1 and   " +
                    " ss.estadosolicitud = 'Cerrado' and  " +
                    " ss.fechaentrega between " +
                    " TIMESTAMP(" + NA_VariablesGlobales.fechaInicialProduccion + ", '07:00:00') and " +
                    " TIMESTAMP(DATE_SUB(current_date(), INTERVAL - 1 DAY), '06:00:00') " +
                    //NA_VariablesGlobales.fechaInicialProduccion + " and current_date()   " +
                    " group by dss.codproducto   " +
                    " ) as t2 ON pp.codigo = t2.codproducto   " +
                    " WHERE   " +
                    " pp.estado = 1 "+
                    " and pp.codigo = "+codigoProducto;

            return consultaStock;
        }
        internal double get_montoComisionResponsable(string resp_une)
        {
            DataSet tuplas = get_DatosNombreResponsableSimec(resp_une);
            double monto;
            double.TryParse(tuplas.Tables[0].Rows[0][5].ToString(), out monto);
            return monto;
        }
    }
}