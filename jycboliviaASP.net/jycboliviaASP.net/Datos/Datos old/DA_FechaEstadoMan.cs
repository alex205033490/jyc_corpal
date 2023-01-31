using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_FechaEstadoMan
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DA_FechaEstadoMan() { }


        public bool insertar(int codSeguimiento, int codEstadoMan, int codUser)
        {           
                string consulta = "insert into tb_fechaestadomantenimiento (fecha,hora,codseguimiento,codestadoman,codusercambio) values(now(),now(),"+codSeguimiento+","+codEstadoMan+", "+codUser+") ";
                ConecRes.ejecutarMySql(consulta);
                return true;
        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar()
        {
            return false;
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

    }
}