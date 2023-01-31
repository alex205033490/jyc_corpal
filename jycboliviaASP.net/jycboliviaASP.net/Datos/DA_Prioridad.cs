using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DA_Prioridad
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_Prioridad() { }

        public DataSet getAllPrioridad()
        {
            string consulta = "select tb_prioridad.codigo, tb_prioridad.nombre from tb_prioridad order by tb_prioridad.codigo asc";
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }


    }
}