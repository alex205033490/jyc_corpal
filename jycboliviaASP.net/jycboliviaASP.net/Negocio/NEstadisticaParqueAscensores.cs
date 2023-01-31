using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NEstadisticaParqueAscensores
    {
        DEstadisticaParqueAscensores estadisticaParqueAscensores = new DEstadisticaParqueAscensores();

        public DataSet listaParqueAscensores(string mes ,string anio)
        {
            DataSet lista = estadisticaParqueAscensores.obtenerEstadisticaParqueAscensores(mes,anio);
            return lista;
        }

        public DataSet obtenerEstadisticaAnioMantenimiento(string anio)
        {
           // DataSet lista = estadisticaParqueAscensores.obtenerEstadisticaAnioMantenimiento(anio);
            DataSet lista = listaParqueAscensores("12", anio);
            return lista;
        }


        public DataSet obtenerEstadosEquiposFuncionandoEntreFechas(string fecha1, string fecha2, string gestion) {

            return estadisticaParqueAscensores.obtenerEstadosEquiposFuncionandoEntreFechas(fecha1,fecha2,gestion);
        }


        public DataSet listaEquipoPorEstado1(string nombreEstado, string mes,string anio)
        {
            DataSet lista = estadisticaParqueAscensores.obtenerEquipoPorEstado(nombreEstado,mes, anio);
            return lista;
        }

        public DataSet obtenerDatosEstadisticaMantenimientoPorEstado(string NombreEstado, string anio) {
            DataSet lista = estadisticaParqueAscensores.obtenerDatosEstadisticaMantenimientoPorEstado(NombreEstado, anio);
            return lista;
        }


        public int totalEquipo()
        {
            int total = estadisticaParqueAscensores.totalEquipos();
            return total;
        }

        public int totalEquipoParqueAscensores(string anio) 
        {
         //   int total = estadisticaParqueAscensores.totalParqueAscensor(anio);
            DataSet dato = listaParqueAscensores("12", anio);
            int total = 0;
            for (int i = 0; i < dato.Tables[0].Rows.Count; i++ )
            {
                total = total + Convert.ToInt32(dato.Tables[0].Rows[i][1].ToString());
            }
            return total;
        }

        public float totalEquipoParqueAscensoresPorcentaje(string anio)
        {
         //   float total = estadisticaParqueAscensores.totalParqueAscensoresPorcentaje(anio);
            DataSet dato = listaParqueAscensores("12", anio);
            float total = 0;
            for (int i = 0; i < dato.Tables[0].Rows.Count; i++)
            {
                total = total + Convert.ToSingle(dato.Tables[0].Rows[i][2].ToString());
            }            
            return total;
        }

        public DataSet listarAniosSeguimiento() 
        {
            DataSet lista = estadisticaParqueAscensores.listarAniosSeguimiento();
            return lista;
        }

    /*    public int candidadEquipo(string mes, string anio)
        {
            return estadisticaParqueAscensores.candidadEquipo(mes, anio);        
        }  */

        public int candidadEquipoEntreFechas(string fecha1, string fecha2, string gestion) {
            return estadisticaParqueAscensores.candidadEquipoEntreFechas(fecha1,fecha2,gestion);
        }

        public float porcentajeEquipoEntreFechas(string fecha1, string fecha2, string gestion)
        {
            return estadisticaParqueAscensores.porcentajeEquipoEntreFechas(fecha1,fecha2,gestion);
        }

        public DataSet VistaGeneralEstado(string fecha1, string fecha2) { 
         
            DateTime fecha1_aux = Convert.ToDateTime(fecha1);
            DateTime fecha2_aux = Convert.ToDateTime(fecha2);                       
            int mes1 = fecha1_aux.Month;
            int anio1 = fecha1_aux.Year;
            int mes2 = fecha2_aux.Month;
            int anio2 = fecha2_aux.Year;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);

            NA_EstadoMantenimiento estadoM = new NA_EstadoMantenimiento();
            DataSet datosEstado = estadoM.mostrarAllDatos();
            dt.Columns.Add("Estados");
            for (int k = 0; k < datosEstado.Tables[0].Rows.Count; k++ )
            {
                dt.Rows.Add(datosEstado.Tables[0].Rows[k][1].ToString());
            }

            int col = 1;
            
            for (int i = anio1; i <= anio2; i++ ) {
                if (i == anio2)
                {
                    for (int j = mes1; j <= mes2; j++)
                    {
                        string columna = "("+j+","+i+")";
                        dt.Columns.Add(columna, typeof(string));
                        DataSet tuplasEstados = listaParqueAscensores(Convert.ToString(j), Convert.ToString(i));
                        for (int l = 0; l < tuplasEstados.Tables[0].Rows.Count; l++)
                        {
                            dt.Rows[l][col] = tuplasEstados.Tables[0].Rows[l][1].ToString();
                        }
                        col++;
                    }
                    mes1 = 1;
                }
                else {
                    for (int j = mes1; j <= 12; j++)
                    {
                        string columna = "("+j+","+i+")";
                        dt.Columns.Add(columna, typeof(string));
                        DataSet tuplasEstados = listaParqueAscensores(Convert.ToString(j),Convert.ToString(i));
                        for (int l = 0; l < tuplasEstados.Tables[0].Rows.Count; l++ )
                        {
                            dt.Rows[l][col] = tuplasEstados.Tables[0].Rows[l][1].ToString();
                        }
                        col++;
                    }
                    mes1 = 1;
                }   
                
            }
            return ds;
        }


        public int totalFaltantesMantenimiento(string anio) {
            return estadisticaParqueAscensores.totalFaltantesMantenimiento(anio);
        }

        public DataSet totalFaltantesMantenimiento2(string anio)
        {
            return estadisticaParqueAscensores.totalFaltantesMantenimiento2(anio);
        }
    }
}