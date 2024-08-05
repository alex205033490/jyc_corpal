using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Datos;

namespace jycboliviaASP.net.Negocio
{
    public class NEstadisticaInstalacion
    {
        DEstadisticaInstalacion estadisticaInstalacion = new DEstadisticaInstalacion();

        public DataSet listaEstadisticaInstalacion()
        {
            DataSet lista = estadisticaInstalacion.obtenerEstadisticaInstalacion();
            return lista;
        }

        public int totalEquipoPorFuncionar()
        {
            int total = estadisticaInstalacion.totalEquipoPorFuncionar();
            return total;
        }

        public float totalEquipoPorFuncionarPorcentaje() 
        {
            float total = estadisticaInstalacion.totalEquipoPorFuncionarPorcentaje();
            return total;
        }

        public DataSet listarEquipoPorEstado(string nombreEstado) 
        {
            DataSet lista = estadisticaInstalacion.obtenerEquipoPorEstado(nombreEstado);
            return lista;
        }

        public DataSet obtenerEstadisticaInstalacionPorMesAnio(string mes, string anio) {
            DataSet lista = estadisticaInstalacion.obtenerEstadisticaInstalacionPorMesAnio(mes,anio);
            return lista;
        }

        public DataSet VistaGeneralEstadoInstalacion(string fecha1, string fecha2)
        {

            DateTime fecha1_aux = Convert.ToDateTime(fecha1);
            DateTime fecha2_aux = Convert.ToDateTime(fecha2);
            int mes1 = fecha1_aux.Month;
            int anio1 = fecha1_aux.Year;
            int mes2 = fecha2_aux.Month;
            int anio2 = fecha2_aux.Year;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);

            NEstadoEquipo estadoM = new NEstadoEquipo();
            DataSet datosEstado = estadoM.listar();
            dt.Columns.Add("Estados");
            for (int k = 0; k < datosEstado.Tables[0].Rows.Count; k++)
            {
                dt.Rows.Add(datosEstado.Tables[0].Rows[k][1].ToString());
            }

            int col = 1;

            for (int i = anio1; i <= anio2; i++)
            {
                if (i == anio2)
                {
                    for (int j = mes1; j <= mes2; j++)
                    {
                        string columna = "(" + j + "," + i + ")";
                        dt.Columns.Add(columna, typeof(string));
                        DataSet tuplasEstados = obtenerEstadisticaInstalacionPorMesAnio(Convert.ToString(j), Convert.ToString(i));
                        for (int l = 0; l < tuplasEstados.Tables[0].Rows.Count; l++)
                        {
                            dt.Rows[l][col] = tuplasEstados.Tables[0].Rows[l][1].ToString();
                        }
                        col++;
                    }
                    mes1 = 1;
                }
                else
                {
                    for (int j = mes1; j <= 12; j++)
                    {
                        string columna = "(" + j + "," + i + ")";
                        dt.Columns.Add(columna, typeof(string));
                        DataSet tuplasEstados = obtenerEstadisticaInstalacionPorMesAnio(Convert.ToString(j), Convert.ToString(i));
                        for (int l = 0; l < tuplasEstados.Tables[0].Rows.Count; l++)
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



        public int totalEquipoPorFuncionarMantenimiento(int anio)
        {
            return estadisticaInstalacion.totalEquipoPorFuncionarMantenimiento(anio);
        }

    }
}