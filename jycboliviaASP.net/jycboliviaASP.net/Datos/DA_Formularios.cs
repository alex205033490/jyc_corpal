using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DA_Formularios
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DA_Formularios() { }

        public bool insertar(string nombre, int estado)
        {

            try
            {
                string consulta = "insert into tb_formulario (nombre,estado) values('"+nombre+"',"+estado+");";
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool modificar(int codigo,string nombre, int estado)
        {
            try
            {
                string consulta = "update tb_formulario set nombre='" + nombre + "',estado=" + estado + " where codigo=" + codigo;
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception ) {
                return false;
            }
        }

        public bool eliminar(int codigo,int estado)
        {
            try {
                string consulta = "update tb_formulario set estado=" + estado + " where codigo=" + codigo;
                ConecRes.ejecutarMySql(consulta);
                return true;
            }catch(Exception ){
            return false;
            }
            
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }
    }
}