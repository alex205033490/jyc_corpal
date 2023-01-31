using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_FechaEstadoMan
    {
        private DA_FechaEstadoMan DfechaEstadoMan = new DA_FechaEstadoMan();

        public NA_FechaEstadoMan() { }

        public bool insertar(int codSeguimiento, int codEstadoMan, int codUser)
        {

            return DfechaEstadoMan.insertar(codSeguimiento,codEstadoMan,codUser);
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
                string consulta = "SELECT MAX(segui.codigo) FROM  tb_fechaestadomantenimiento segui";
                DataSet datoResul = DfechaEstadoMan.getDatos(consulta);
                int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codUltimo;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public DataSet historial_fechas(int codSeguimiento) {
            string consulta = "select eman.nombre as 'Estado Mantenimiento', "+                               
                               " DATE_FORMAT(fm.fecha,'%d/%m/%Y') as 'Fecha Insertado', "+
                               " fm.hora from tb_fechaestadomantenimiento fm, tb_estado_mantenimiento eman "+
                               " where fm.codEstadoMan = eman.codigo and fm.codSeguimiento = "+codSeguimiento+" order by fm.codigo desc";

            return DfechaEstadoMan.getDatos(consulta);
        }

    }
}