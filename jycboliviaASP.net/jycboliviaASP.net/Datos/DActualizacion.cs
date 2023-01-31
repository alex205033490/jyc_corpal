using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DActualizacion
    {
        private conexionMySql conexion = new conexionMySql();

        public bool insertarActualizacion(string nombre, int estado)
        {
            try
            {
                string consulta = "insert into tb_actualizacion(nombre, estado) values('"+nombre+"', "+estado+")";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool modificarActualizacion(int codigo, string nombre) 
        {
            try
            {
                string consulta = "update tb_actualizacion set tb_actualizacion.nombre= '" + nombre + "' where tb_actualizacion.codigo= " + codigo + ";";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool eliminarActualizacion(int codigo) 
        {
            try
            {
                string consulta = "update tb_actualizacion set tb_actualizacion.estado =" + 0 + " where tb_actualizacion.codigo= " + codigo + ";";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataSet listarActualizacion()
        {
            try
            {
                string consulta = "select  tb_actualizacion.codigo, tb_actualizacion.nombre, tb_actualizacion.estado from tb_actualizacion where tb_actualizacion.estado!=0";
                DataSet lista = conexion.consultaMySql(consulta);
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Error al Listar Actualizacion ", e); ;
            }
        }

        public DataSet buscarActualizacion(string nombre) 
        {
             try
            {
                string consulta = "select ac.codigo, ac.nombre, ac.Estado from tb_actualizacion ac where ac.nombre='"+nombre+"'";
                DataSet lista = conexion.consultaMySql(consulta);
                return lista;
            }
             catch (Exception e)
             {
                 throw new Exception("Error al Buscar Actualizacion ", e); ;
             }
        }
    }
    
}