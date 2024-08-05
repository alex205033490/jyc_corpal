using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_FechaEstadoEquipo
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_FechaEstadoEquipo() { }

        public bool insertar(string fecha, string hora, int codEquipo, int codEstadoEquipo, int codUserCambio)
        {  
                string consulta = "insert into tb_fechaestadoequipo (fecha,hora,codequipo,codestadoequipo,codusercambio) values('" + fecha + "','" + hora + "'," + codEquipo + "," + codEstadoEquipo + "," + codUserCambio + ") ";
                return ConecRes.ejecutarMySql(consulta);
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