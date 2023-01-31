using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DEstadoEquipo
    {
        private conexionMySql conexion = new conexionMySql();

        public bool insertarEstadoEquipo(string nombre, int estado)
        {
            try
            {
                string consulta = "insert into tb_estado_equipo(nombre, estado) values('"+nombre+"', "+estado+")";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool modificarEstadoEquipo(int codigo, string nombre) 
        {
            try
            {
                string consulta = "update tb_estado_equipo set tb_estado_equipo.nombre= '" + nombre + "' where tb_estado_equipo.codigo= " + codigo + ";";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool eliminarEstadoEquipo(int codigo) 
        {
            try
            {
                string consulta = "update tb_estado_equipo set tb_estado_equipo.estado =" + 0 + " where tb_estado_equipo.codigo= " + codigo + ";";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataSet listarEstadoEquipo()
        {
            string consulta = "select  tb_estado_equipo.codigo, tb_estado_equipo.nombre, tb_estado_equipo.estado from tb_estado_equipo where tb_estado_equipo.estado!=0";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscarEstadoEquipo(string nombre) 
        {
            string consulta = "select eq.codigo, eq.nombre, eq.estado from tb_estado_equipo eq where eq.nombre like '%" + nombre + "%' ;";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public int getCodigoEstadoEquipo(string estadoEquipo) {
            try
            {
                string consulta = "select estadoe.codigo from tb_estado_equipo estadoe where estadoe.nombre = '"+estadoEquipo+"'";
                DataSet lista = conexion.consultaMySql(consulta);
                return Convert.ToInt32(lista.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception) {
                return -1;
            }
        
        }
    }
}
