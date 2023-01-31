using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DA_Importacion
    {
        private conexionMySql conexion = new conexionMySql();      
        public DA_Importacion()
        { }


        public DataSet get_datoImportacionContenedorTotal(bool todosLosDatos, string Edificio, string exbo, string semanaExp , string Dui, string Contenedor)
        {
            List<string> listBaseDatos = NA_VariablesGlobales.listBaseDatos;
            string consulta = "";
            for (int i = 0; i < listBaseDatos.Count; i++)
            {
                string baseDatos = listBaseDatos[i];
                string UNE;
                string Ciudad;
                if (baseDatos.Equals("db_seguimientoscz_jyc"))
                {
                    UNE = "Interlogi";
                    Ciudad = "SCZ";
                }
                else
                    if (baseDatos.Equals("db_seguimientocbba_jyc"))
                    {
                        UNE = "Melevar";
                        Ciudad = "CBBA";
                    }
                    else
                        if (baseDatos.Equals("db_seguimientolpz_jyc"))
                        {
                            UNE = "Elevamerica";
                            Ciudad = "LPZ";
                        }
                        else
                            if (baseDatos.Equals("db_seguimientosucre_jyc"))
                            {
                                UNE = "JYC";
                                Ciudad = "Sucre";
                            }
                            else
                                if (baseDatos.Equals("db_seguimientooruro_jyc"))
                                {
                                    UNE = "JYC";
                                    Ciudad = "Oruro";
                                }
                                else
                                    if (baseDatos.Equals("db_seguimientobeni_jyc"))
                                    {
                                        UNE = "JYC";
                                        Ciudad = "Beni";
                                    }
                                    else
                                        if (baseDatos.Equals("db_seguimientopando_jyc"))
                                        {
                                            UNE = "JYC";
                                            Ciudad = "Pando";
                                        }
                                        else
                                            if (baseDatos.Equals("db_seguimientotarija_jyc"))
                                            {
                                                UNE = "JYC";
                                                Ciudad = "Tarija";
                                            }
                                            else
                                                if (baseDatos.Equals("db_seguimientoyacuiba_jyc"))
                                                {
                                                    UNE = "JYC";
                                                    Ciudad = "Yacuiba";
                                                }
                                                else
                                                    if (baseDatos.Equals("db_seguimientopotosi_jyc"))
                                                    {
                                                        UNE = "JYC";
                                                        Ciudad = "Potosi";
                                                    }
                                                    else
                                                        if (baseDatos.Equals("db_seguimientovillamontes_jyc"))
                                                        {
                                                            UNE = "JYC";
                                                            Ciudad = "Villamontes";
                                                        }
                                                        else
                                                            if (baseDatos.Equals("db_seguimientoparaguay_nuevo"))
                                                            {
                                                                UNE = "JYC";
                                                                Ciudad = "Paraguay";
                                                            }
                                                            else
                                                            {
                                                                UNE = "JYC";
                                                                Ciudad = "Otro";
                                                            }
                if (i > 0)
                    consulta = consulta + " UNION ";


                consulta = consulta + "select " +
                                 " eq.codigo,  " +
                                " eq.exbo, " +
                                " pp.nombre as 'Edificio',  " +
                                " eq.vendidoenciudad as 'Vendido',  " +
                                " eq.instaladoenciudad as 'Instalado', " +
                                " '" + UNE + "' AS 'UNE',  " +
                                " '" + Ciudad + "' as 'CIUDAD',  " +
                                " eeq.nombre as 'Estado1' , " +
                                " date_format(id.fechaaproximadoarribopuerto,'%d/%m/%Y') as 'fechaaproximadoarribopuerto',  " +
                                " id.consignatario as 'Consignatario', " +
                                " id.semana_expedicion, " +
                                " id.codnrodui, " +
                                " id.codnrocontenedor, " +
                                " id.pct_tamanio_contenedor, " +
                                " id.nro_bl, " +
                                " id.pct_peso, " +                              
                                " (id.giros1_dolares+id.giros2_dolares+id.giros3_dolares) as 'valorgiradodelequipo', " +                                
                                " id.pct_costo_transporte, " +
                                " id.pct_costo_internacional, " +
                                " id.pct_costo_nacional, " +
                                " date_format(id.pct_fechapagarproveedor,'%d/%m/%Y') as 'pct_fechapagarproveedor' " +                                
                                " from  " +
                                baseDatos + ".tb_proyecto pp, " +
                                baseDatos + ".tb_equipo eq " +
                                " LEFT JOIN " + baseDatos + ".tb_importacion_datos id ON (eq.codimportacion = id.codigo) , " +
                                baseDatos + ".tb_fechaestadoequipo fe, " + baseDatos + ".tb_estado_equipo eeq " +
                                " where  " +
                                " eq.cod_proyecto = pp.codigo and " +
                                " eq.codfechaestadoequipo = fe.codigo and " +
                                " fe.codEstadoEquipo = eeq.codigo ";
                //" and fe.codEstadoEquipo not in (6,7,8,9,10,21) ";
                               

                if (!Edificio.Equals(""))
                {
                    consulta = consulta + " and pp.nombre = '" + Edificio + "'";
                }
               
                if (!exbo.Equals(""))
                {
                    consulta = consulta + " and eq.exbo = '" + exbo + "'";
                }

                if (!semanaExp.Equals(""))
                {
                    consulta = consulta + " and id.semana_expedicion = '" + semanaExp + "'";
                }

                if (!Dui.Equals(""))
                {
                    consulta = consulta + " and id.codnrodui = '" + Dui + "'";
                }

                if (!Contenedor.Equals(""))
                {
                    consulta = consulta + " and id.codnrocontenedor = '" + Contenedor + "'";
                }

                if (todosLosDatos == false)
                {
                    consulta = consulta + " limit 100";
                }

            }
            return conexion.consultaMySql(consulta);
        }


        public DataSet get_PesoTotalContenedor(string Contenedor)
        {
            List<string> listBaseDatos = NA_VariablesGlobales.listBaseDatos;
            string consulta = "";
            for (int i = 0; i < listBaseDatos.Count; i++)
            {
                string baseDatos = listBaseDatos[i];               
                if (i > 0)
                    consulta = consulta + " UNION ALL ";


                consulta = consulta + " select "+
                                       " sum(id.pct_peso) AS 'PESO', id.codnrocontenedor " +
                                       " from "+
                                       " tb_importacion_datos id "+
                                       " where "+
                                       " id.codnrocontenedor = '"+Contenedor+"'"+
                                       " group by id.codnrocontenedor";
              
                if (!Contenedor.Equals(""))
                {
                    consulta = consulta + " and id.codnrocontenedor = '" + Contenedor + "'";
                }                              

            }

            consulta = "SELECT SUM(T1.PESO) FROM(" + consulta + ") AS T1 GROUP BY T1.codnrocontenedor";

            return conexion.consultaMySql(consulta);
        }


        public DataSet get_TotalGirosContenedor(string Contenedor)
        {
            List<string> listBaseDatos = NA_VariablesGlobales.listBaseDatos;
            string consulta = "";
            for (int i = 0; i < listBaseDatos.Count; i++)
            {
                string baseDatos = listBaseDatos[i];
                if (i > 0)
                    consulta = consulta + " UNION ALL ";


                consulta = consulta + " select " +
                                       " sum(id.giros1_dolares+id.giros2_dolares+id.giros3_dolares) AS 'totalGiro', id.codnrocontenedor  " +
                                       " from " +
                                       " tb_importacion_datos id " +
                                       " where " +
                                       " id.codnrocontenedor = '" + Contenedor +"'"+
                                       " group by id.codnrocontenedor";

                if (!Contenedor.Equals(""))
                {
                    consulta = consulta + " and id.codnrocontenedor = '" + Contenedor + "'";
                }

            }

            consulta = "SELECT SUM(T1.totalGiro) FROM(" + consulta + ") AS T1 GROUP BY T1.codnrocontenedor";

            return conexion.consultaMySql(consulta);
        }


        internal DataSet existeContenedor(string nroContenedor)
        {
            string consulta = "select * from tb_importacioncontenedor cc "+
                               " where cc.codnrocontenedor = '"+nroContenedor+"';";
            return conexion.consultaMySql(consulta);
        }

        internal DataSet getFacturasContenedor(string nroContenedor)
        {
            string consulta = "select "+
                               " codnrocontenedor, "+
                               " date_format(fechafactura_msc,'%d/%m/%Y') as 'fechafactura_msc', " +
                               " nrofactura_msc, "+
                               " montofactura_msc, "+
                               " date_format(fechafactura_aspb,'%d/%m/%Y') as 'fechafactura_aspb', " +
                               " nrofactura_aspb, "+
                               " montofactura_aspb, "+
                               " date_format(fechafactura_thc,'%d/%m/%Y') as 'fechafactura_thc', " +
                               " nrofactura_thc,  "+
                               " montofactura_thc  " +
                               " from tb_importacioncontenedor imp "+
                               " where  "+
                               " imp.codnrocontenedor = '"+nroContenedor+"'";
            return conexion.consultaMySql(consulta);
        }

        internal bool actualizarDatoscontenedor(string nroContenedor, string fechaASP, string nroASPB, float montoASPB, string fechaMSC, string nroMSC, float montoMSC, string fechaTHC, string nroTHC, float montoTHC)
        {
            string consulta = "update tb_importacioncontenedor set "+
                               " tb_importacioncontenedor.fechafactura_msc = "+fechaMSC+", "+
                               " tb_importacioncontenedor.nrofactura_msc = '"+nroMSC+"', "+
                               " tb_importacioncontenedor.montofactura_msc = '" + montoMSC.ToString().Replace(',','.') +"', " +
                                  " tb_importacioncontenedor.fechafactura_aspb = "+fechaASP+", " +
                                  " tb_importacioncontenedor.nrofactura_aspb = '"+nroASPB+"', " +
                                  " tb_importacioncontenedor.montofactura_aspb = '"+montoASPB.ToString().Replace(',','.')+"', " +
                                  " tb_importacioncontenedor.fechafactura_thc = "+fechaTHC+", " +
                                  " tb_importacioncontenedor.nrofactura_thc = '"+nroTHC+"', " +
                                  " tb_importacioncontenedor.montofactura_thc = '"+montoTHC.ToString().Replace(',','.')+"' " +
                                  " where " +
                                  " tb_importacioncontenedor.codnrocontenedor = '"+nroContenedor+"' ";
            return conexion.ejecutarMySql(consulta);
        }

        internal bool ingresarNuevoContendor(string codnrocontenedor,string fechaASP, string nroASPB, float montoASPB, string fechaMSC, string nroMSC, float montoMSC, string fechaTHC, string nroTHC, float montoTHC)
        {
            string consulta = "insert into tb_importacioncontenedor(" +
                               " codnrocontenedor," +
                               " fechafactura_msc," +
                               " nrofactura_msc," +
                               " montofactura_msc," +
                               " fechafactura_aspb," +
                               " nrofactura_aspb," +
                               " montofactura_aspb," +
                               " fechafactura_thc," +
                               " nrofactura_thc," +
                               " montofactura_thc" +
                               " ) values(" +
                               "'" + codnrocontenedor + "'," +
                                fechaMSC + "," +
                               "'"+nroMSC +"'," +
                               "'"+montoMSC.ToString().Replace(',','.') + "'," +
                                fechaASP + "," +
                               "'"+nroASPB+"'," +
                               "'"+montoASPB.ToString().Replace(',','.')+"'," +
                                fechaTHC + "," +
                               "'"+nroTHC+"'," +
                               "'"+montoTHC.ToString().Replace(',','.')+"')";
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet getTodoslosContenedores(string contenedor)
        {
            string consulta = "select " +
                              " codnrocontenedor, " +
                              " date_format(fechafactura_msc,'%d/%m/%Y') as 'fechafactura_msc', " +
                              " nrofactura_msc, " +
                              " montofactura_msc, " +
                              " date_format(fechafactura_aspb,'%d/%m/%Y') as 'fechafactura_aspb', " +
                              " nrofactura_aspb, " +
                              " montofactura_aspb, " +
                              " date_format(fechafactura_thc,'%d/%m/%Y') as 'fechafactura_thc', " +
                              " nrofactura_thc,  " +
                              " montofactura_thc  " +
                              " from tb_importacioncontenedor imp " +
                              " where  " +
                              " imp.codnrocontenedor like '%" + contenedor + "%'";
            return conexion.consultaMySql(consulta);
        }

        public DataSet get_FacturaContenedorGeneral(bool todosLosDatos, string Edificio, string exbo, string semanaExp, string Dui, string Contenedor)
        {
            List<string> listBaseDatos = NA_VariablesGlobales.listBaseDatos;
            string consulta = "";
            for (int i = 0; i < listBaseDatos.Count; i++)
            {
                string baseDatos = listBaseDatos[i];
                string UNE;
                string Ciudad;
                if (baseDatos.Equals("db_seguimientoscz_jyc"))
                {
                    UNE = "Interlogi";
                    Ciudad = "SCZ";
                }
                else
                    if (baseDatos.Equals("db_seguimientocbba_jyc"))
                    {
                        UNE = "Melevar";
                        Ciudad = "CBBA";
                    }
                    else
                        if (baseDatos.Equals("db_seguimientolpz_jyc"))
                        {
                            UNE = "Elevamerica";
                            Ciudad = "LPZ";
                        }
                        else
                            if (baseDatos.Equals("db_seguimientosucre_jyc"))
                            {
                                UNE = "JYC";
                                Ciudad = "Sucre";
                            }
                            else
                                if (baseDatos.Equals("db_seguimientooruro_jyc"))
                                {
                                    UNE = "JYC";
                                    Ciudad = "Oruro";
                                }
                                else
                                    if (baseDatos.Equals("db_seguimientobeni_jyc"))
                                    {
                                        UNE = "JYC";
                                        Ciudad = "Beni";
                                    }
                                    else
                                        if (baseDatos.Equals("db_seguimientopando_jyc"))
                                        {
                                            UNE = "JYC";
                                            Ciudad = "Pando";
                                        }
                                        else
                                            if (baseDatos.Equals("db_seguimientotarija_jyc"))
                                            {
                                                UNE = "JYC";
                                                Ciudad = "Tarija";
                                            }
                                            else
                                                if (baseDatos.Equals("db_seguimientoyacuiba_jyc"))
                                                {
                                                    UNE = "JYC";
                                                    Ciudad = "Yacuiba";
                                                }
                                                else
                                                    if (baseDatos.Equals("db_seguimientopotosi_jyc"))
                                                    {
                                                        UNE = "JYC";
                                                        Ciudad = "Potosi";
                                                    }
                                                    else
                                                        if (baseDatos.Equals("db_seguimientovillamontes_jyc"))
                                                        {
                                                            UNE = "JYC";
                                                            Ciudad = "Villamontes";
                                                        }
                                                        else
                                                            if (baseDatos.Equals("db_seguimientoparaguay_nuevo"))
                                                            {
                                                                UNE = "JYC";
                                                                Ciudad = "Paraguay";
                                                            }
                                                            else
                                                            {
                                                                UNE = "JYC";
                                                                Ciudad = "Otro";
                                                            }
                if (i > 0)
                    consulta = consulta + " UNION ";


                consulta = consulta + "select " +
                                 " eq.codigo,  " +
                                " eq.exbo, " +
                                " pp.nombre as 'Edificio',  " +
                                " eq.vendidoenciudad as 'Vendido',  " +
                                " eq.instaladoenciudad as 'Instalado', " +
                                " '" + UNE + "' AS 'UNE',  " +
                                " '" + Ciudad + "' as 'CIUDAD',  " +
                                " eeq.nombre as 'Estado1' , " +                                
                                " id.consignatario as 'Consignatario', " +
                                " id.semana_expedicion, " +
                                " id.codnrodui, " +
                                " id.codnrocontenedor, " +                                
                                " (id.giros1_dolares+id.giros2_dolares+id.giros3_dolares) as 'valorgiradodelequipo', " +
                                " ic.montofactura_msc, "+
                                " ic.montofactura_aspb, "+
                                " ic.montofactura_thc, "+
                                " ic.nrofactura_msc "+
                                " from  " +
                                baseDatos + ".tb_proyecto pp, " +
                                baseDatos + ".tb_equipo eq " +
                                " LEFT JOIN " + baseDatos + ".tb_importacion_datos id ON (eq.codimportacion = id.codigo)  "+
                                " left join " + baseDatos + ".tb_importacioncontenedor ic on (id.codnrocontenedor = ic.codnrocontenedor) , " +
                                baseDatos + ".tb_fechaestadoequipo fe, " + baseDatos + ".tb_estado_equipo eeq " +
                                " where  " +
                                " eq.cod_proyecto = pp.codigo and " +
                                " eq.codfechaestadoequipo = fe.codigo and " +
                                " fe.codEstadoEquipo = eeq.codigo ";
                //" and fe.codEstadoEquipo not in (6,7,8,9,10,21) ";


                if (!Edificio.Equals(""))
                {
                    consulta = consulta + " and pp.nombre = '" + Edificio + "'";
                }

                if (!exbo.Equals(""))
                {
                    consulta = consulta + " and eq.exbo = '" + exbo + "'";
                }

                if (!semanaExp.Equals(""))
                {
                    consulta = consulta + " and id.semana_expedicion = '" + semanaExp + "'";
                }

                if (!Dui.Equals(""))
                {
                    consulta = consulta + " and id.codnrodui = '" + Dui + "'";
                }

                if (!Contenedor.Equals(""))
                {
                    consulta = consulta + " and id.codnrocontenedor = '" + Contenedor + "'";
                }

                if (todosLosDatos == false)
                {
                    consulta = consulta + " limit 100";
                }

            }
            return conexion.consultaMySql(consulta);
        }


        internal DataSet get_TodoslosEquiposdelasBasesdedatos(string exbo)
        {
            List<string> listBaseDatos = NA_VariablesGlobales.listBaseDatos;
            List<string> listNombreBaseDatos = NA_VariablesGlobales.listNombreBaseDatos;
            string consulta = "";
            for (int i = 0; i < listBaseDatos.Count; i++ )
            {
                consulta = consulta +
                           "select " +
                           " eq.codigo, eq.exbo, " +
                           " proy.nombre as 'Edificio', " +
                           " eq.consignatario,  '" + listNombreBaseDatos[i] + "' as 'BaseDatos', '" + listBaseDatos[i] + "' as DB " +
                           " , eq.codvariablesimec "+
                           " , eq.vc "+
                           " from " + listBaseDatos[i] + ".tb_equipo eq, " + listBaseDatos[i] + ".tb_proyecto proy " +
                           " where " +
                           " eq.cod_proyecto = proy.codigo and " +
                           " eq.exbo = '"+exbo+"' and "+
                           " eq.instaladoenciudad = '" + listNombreBaseDatos[i] + "'";

                if ((i+1) < listBaseDatos.Count)
                {
                    consulta = consulta + " UNION ";
                }                           
            }
            DataSet datosResult = conexion.consultaMySql(consulta);
            return datosResult;

        }


        internal DataSet get_TodoslosEquiposdelasBasesdedatos2(string exbo)
        {
            List<string> listBaseDatos = NA_VariablesGlobales.listBaseDatos;
            List<string> listNombreBaseDatos = NA_VariablesGlobales.listNombreBaseDatos;
            string consulta = "";
            for (int i = 0; i < listBaseDatos.Count; i++)
            {
                consulta = consulta +
                           "select " +
                           " eq.codigo, eq.exbo, " +
                           " proy.nombre as 'Edificio', " +
                           " eq.consignatario,  '" + listNombreBaseDatos[i] + "' as 'BaseDatos', '" + listBaseDatos[i] + "' as DB " +
                           " , eq.codvariablesimec " +
                           " from " + listBaseDatos[i] + ".tb_equipo eq, " + listBaseDatos[i] + ".tb_proyecto proy " +
                           " where " +
                           " eq.cod_proyecto = proy.codigo and " +
                           " eq.exbo like '%" + exbo + "%' and " +
                           " eq.instaladoenciudad = '" + listNombreBaseDatos[i] + "'";

                if ((i + 1) < listBaseDatos.Count)
                {
                    consulta = consulta + " UNION ";
                }
            }
            DataSet datosResult = conexion.consultaMySql(consulta);
            return datosResult;

        }

        internal bool actualizarDatosCredinForm(string exbo, string edificio, string consignatario, string Ciudad, string nroAplicacion,  string contenedor, string fechaPago, string fechaSolicitud, float porcentaje, float valorFob, float valorFobResult, float valorCFR, float valorCRFResult, float transpMaritimo, float transpMaritimoResultado, float transpTerrestre, float transpTerrestreResultado, float nroEquiposContenedor, float baseImponibleSeguro, float totalValorSeguro, string BasedeDatos, string variablesimec, int cod_Equipo)
        {
            
            string basedeImportacion = NA_VariablesGlobales.baseDedatosImportacion;
            conexionMySql cnxImportacion = new conexionMySql(basedeImportacion);            
            string consulta = "insert into tb_importacion_segurocredinform( "+
                           " fecha, hora, "+    
                           " exbo, proyecto, consignatario, "+
                           " variablesimec , nroaplicacion, fechapago, "+     
                           " fechasolitud, valorfob_sus, valorfob_sus_result, "+ 
                           " transpmaritimo_sus, transpmaritimo_sus_result, "+      
                           " nro_equiposcontenedor, baseimponibleparaelseguro, "+      
                           " totalvalorseguro, contenedor, cod_equipo, ciudad, "+
                           " basedatos,transpterrestre_sus,transpterrestre_sus_result) values( " +
                           " current_date, current_time, "+
                           " '"+exbo+"', '"+edificio+"', '"+consignatario+"', "+ 
                           " '"+variablesimec+"' , '"+nroAplicacion+"', "+fechaPago+", "+
                            fechaSolicitud + ", '" + valorFob.ToString().Replace(',', '.') + "', '" + valorFobResult.ToString().Replace(',', '.') + "', " +
                           " '" + transpMaritimo.ToString().Replace(',', '.') + "', '" + transpMaritimoResultado.ToString().Replace(',', '.') + "', " +
                            nroEquiposContenedor.ToString().Replace(',', '.') + ", '" + baseImponibleSeguro.ToString().Replace(',', '.') + "', " +
                           " '" + totalValorSeguro.ToString().Replace(',', '.') + "', '" + contenedor + "',  " + cod_Equipo + ", '" + Ciudad + "', " +
                           " '" + BasedeDatos + "','" + transpTerrestre.ToString().Replace(',', '.') + "','" + transpTerrestreResultado.ToString().Replace(',', '.') + "')";
            bool bandera = cnxImportacion.ejecutarMySql(consulta);
            return bandera;
        }

        internal DataSet get_SegurosCredinForm(string fechadesde, string fechahasta)
        {
            string basedeImportacion = NA_VariablesGlobales.baseDedatosImportacion;
            conexionMySql cnxImportacion = new conexionMySql(basedeImportacion);
            string consulta = "Select "+
                               " exbo ,variablesimec ,nroaplicacion ,proyecto ,consignatario , "+
                               " totalvalorseguro ,date_format(fechapago,'%d/%m/%Y') as 'fechapago' ,date_format(fechasolitud,'%d/%m/%Y') as 'fechasolitud',ciudad " +
                               " from tb_importacion_segurocredinform seg "+
                               " where "+
                               " seg.fechapago between "+fechadesde+" and "+fechahasta;
            DataSet filas = cnxImportacion.consultaMySql(consulta);
            return filas;
        }

        internal DataSet get_SegurosCredinForm2(string fechadesde, string fechahasta)
        {
            string basedeImportacion = NA_VariablesGlobales.baseDedatosImportacion;
            conexionMySql cnxImportacion = new conexionMySql(basedeImportacion);
            string consulta = "Select "+
                               " seg.exbo , "+
                               " seg.nroaplicacion, "+
                               " seg.proyecto, "+
                               " seg.consignatario, "+
                               " seg.baseimponibleparaelseguro, "+
                               " seg.totalvalorseguro,  "+
                               " date_format(seg.fechapago,'%d/%m/%Y') as 'fechapago' , "+
                               " date_format(seg.fechasolitud,'%d/%m/%Y') as 'fechasolitud',ciudad "+
                               " from tb_importacion_segurocredinform seg  "+
                               " where "+
                               " seg.fechapago between " + fechadesde + " and " + fechahasta;
            DataSet filas = cnxImportacion.consultaMySql(consulta);
            return filas;
        }


        internal DataSet estacargadodatosdelSegurodelExbo(string exbo)
        {
            string basedeImportacion = NA_VariablesGlobales.baseDedatosImportacion;
            conexionMySql cnxImportacion = new conexionMySql(basedeImportacion);
            string consulta = "Select  exbo ,variablesimec ,nroaplicacion ,proyecto ,consignatario , "+ 
                               " totalvalorseguro ,fechapago ,fechasolitud ,ciudad "+ 
                               " from tb_importacion_segurocredinform seg "+
                               " where "+
                               " seg.exbo = '"+exbo+"'";
            DataSet filas = cnxImportacion.consultaMySql(consulta);
            return filas;
        }
    }
}