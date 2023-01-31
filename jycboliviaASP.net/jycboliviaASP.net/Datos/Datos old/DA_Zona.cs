using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{


    public class DA_Zona
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_Zona() { }


        public bool insertar(string nombre)
        {

            try
            {
                string consulta = "insert into tb_zona (nombre) values('" + nombre + "');";
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

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