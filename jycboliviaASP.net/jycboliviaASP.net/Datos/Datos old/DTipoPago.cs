using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DTipoPago
    {
        private conexionMySql conexion = new conexionMySql();

        public bool insertarTipoPago(string nombre, int estado)
        {
            try
            {
                string consulta = "insert into tb_tipopago(nombre, estado) values('"+nombre+"', "+estado+")";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool modificarTipoPago(int codigo, string nombre) 
        {
            try
            {
                string consulta = "update tb_tipopago set tb_tipopago.nombre= '" + nombre + "' where tb_tipopago.codigo= " + codigo + ";";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool eliminarTipoPago(int codigo) 
        {
            try
            {
                string consulta = "update tb_tipopago set tb_tipopago.estado =" + 0 + " where tb_tipopago.codigo= " + codigo + ";";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataSet listarTipoPago()
        {
            string consulta = "select  tb_tipopago.codigo, tb_tipopago.nombre, tb_tipopago.estado from tb_tipopago where tb_tipopago.estado!=0";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscarTipoPago(string nombre) 
        {
            string consulta = "select select  tb_tipopago.codigo, tb_tipopago.nombre, tb_tipopago.estado from tb_tipopago tp where tp.nombre like '%" + nombre + "%' ;";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }
    }
    
}