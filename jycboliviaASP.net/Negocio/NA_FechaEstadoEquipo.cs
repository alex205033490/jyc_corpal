using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_FechaEstadoEquipo
    {
        private DA_FechaEstadoEquipo DfechaEstadoEquipo = new DA_FechaEstadoEquipo();

        public NA_FechaEstadoEquipo() { }

        public bool insertar(int codEquipo, int codEstadoEquipo, int codUserCambio)
        {
            return DfechaEstadoEquipo.insertar(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), codEquipo, codEstadoEquipo,codUserCambio);
        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar()
        {
            return false;
        }

        public int ultimoinsertado()
        {
            try
            {
                string consulta = "SELECT MAX(segui.codigo) FROM  tb_fechaestadoequipo segui";
                DataSet datoResul = DfechaEstadoEquipo.getDatos(consulta);
                int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codUltimo;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public DataSet historial_fechas(int codEquipo)
        {
            string consulta = "select eman.nombre as 'Estado Mantenimiento', " +
                               " DATE_FORMAT(fm.fecha,'%d/%m/%Y') as 'Fecha Insertado', " +
                               " fm.hora from tb_fechaestadoequipo fm, tb_estado_equipo eman " +
                               " where fm.codEstadoEquipo = eman.codigo and fm.codEquipo = " + codEquipo + " order by fm.codigo desc";

            return DfechaEstadoEquipo.getDatos(consulta);
        }


    }
}