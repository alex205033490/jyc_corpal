using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_Cargo
    {

        private conexionMySql ConecRes = new conexionMySql();
        public DA_Cargo() { 
        
        }

        public bool insertar(string nombre, string detalle, int estado)
        {

                string consulta = "insert into tb_cargo (nombre,detalle,estado) values('"+nombre+"','"+detalle+"',"+estado+");";
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