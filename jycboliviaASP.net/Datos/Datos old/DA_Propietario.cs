using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using MySql.Data;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_Propietario 
    {
        
        private int codigo;
        private String nombre;
        private string ci;
        private string telefono;
        private string celular;
        private string direccion;
        private string email;
        private string facturar_A;
        private int nit;
        private conexionMySql conexion = new conexionMySql();

        public DA_Propietario() { }

        public DA_Propietario(int codigo, string nombre, string ci, string telefono, string celular, string direccion, string email, string facturar_A, int nit) {
            this.codigo = codigo;
            this.nombre = nombre;
            this.ci = ci;
            this.telefono = telefono;
            this.celular = celular;
            this.direccion = direccion;
            this.email = email;
            this.facturar_A = facturar_A;
            this.nit = nit;
        }

        public bool insertarEncargadoPago(string nombre, string ci, string telefono, string celular, string direccion, string email, string facturar_A, string nit, int estado, string banco, string observacion)
        {
            try
            {
                string consulta = "insert into tb_propietario(nombre, ci, telefono, celular, direccion, email, facturar_A, nit, estado, banco, observacion) values ('" + nombre + "', '" + ci + "', '" + telefono + "', '" + celular + "', '" + direccion + "','" + email + "', '" + facturar_A + "', '" + nit + "', 1, '"+banco+"', '"+observacion+"') ;";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public bool modificarEncargadoPago(int codigo, String nombre, String ci, string telefono, string celular, string direccion, string email, string facturar_A, string nit, string banco, string observacion)
        {
            try
            {
                string consulta = "update tb_propietario set tb_propietario.nombre= '"+nombre+"', tb_propietario.ci= '"+ci+"', tb_propietario.telefono='"+telefono+"', tb_propietario.celular='"+celular+"', tb_propietario.direccion='"+direccion+"', tb_propietario.email='"+email+"', tb_propietario.facturar_A ='"+facturar_A+"', tb_propietario.Nit = '"+nit+"', tb_propietario.estado ="+1+", "
                                    + "tb_propietario.banco = '" + banco + "', tb_propietario.observacion = '" + observacion+"' " 
                                        +" where tb_propietario.codigo= " + codigo + ";";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataSet listar()
        {
                string consulta = "select codigo, nombre, ci, telefono, celular, direccion, email, facturar_A, Nit from tb_propietario where tb_propietario.estado!=0";
                DataSet lista = conexion.consultaMySql(consulta);
                return lista;
        }

        public bool eliminarEncargadoPago(int codigo)
        {
            try
            {
                string consulta = "update tb_propietario set tb_propietario.estado =" + 0 + " where tb_propietario.codigo= " + codigo + ";";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataSet buscarEncargadoPago(string nombre,string ci) {
            string consulta = "select ep.codigo, ep.nombre as 'NombreEncargadoPago', ep.ci, ep.telefono, ep.celular, ep.direccion, ep.email, facturar_A, Nit from tb_propietario ep where ep.ci like '%" + ci + "%' and ep.nombre like '%" + nombre + "%' ;";
                DataSet lista = conexion.consultaMySql(consulta);
                return lista;
        }

        ///---------------------------------------------------------
        public DataSet existeEncargadoPago(string nombreEncargadoPago)
        {
            string consulta = "SELECT *FROM tb_propietario ep WHERE ep.`nombre`='" + nombreEncargadoPago + "'";
            DataSet resultado = conexion.consultaMySql(consulta);
            return resultado;
        }


        public int getUltimoCodigo()
        {
            try
            {
                string consulta = "SELECT MAX(codigo) FROM tb_propietario";
                DataSet datoResul = conexion.consultaMySql(consulta);
                int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codUltimo;
            }
            catch (Exception)
            {
                return -1;
            }
        }


        public int getCodigoEncargadoPago(string nombreEncargadoPago)
        {
            string consulta = "SELECT ep.`codigo` FROM `tb_propietario` ep WHERE ep.`nombre`='" + nombreEncargadoPago + "'";
            DataSet datoResul = conexion.consultaMySql(consulta);

            if (datoResul.Tables[0].Rows.Count > 0)
            {
                int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codUltimo;
            }
            else
                return -1;
           
        }


        public int getCodigoEncargadoPagoProyecto(string nombreProyecto)
        {
            string consulta = "SELECT ep.codigo FROM `tb_proyecto` p, `tb_propietario` ep WHERE p.`codEncargado` = ep.`codigo` AND p.`nombre` = '" + nombreProyecto + "'";
            DataSet datoResul = conexion.consultaMySql(consulta);

            int codUltimo = -1;
            if(datoResul.Tables[0].Rows.Count > 0){
                codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
            }
            
            return codUltimo;
        }


        public DataSet buscador(string nombre)
        {
            string consulta = "SELECT ep.`nombre` FROM `tb_propietario` ep WHERE ep.`estado`=1 AND ep.`nombre` LIKE '%" + nombre + "%' ORDER BY ep.`nombre` ASC";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public string getEncargadoPago(int codigo)
        {
            string consulta = "SELECT ep.nombre FROM tb_propietario ep WHERE ep.estado=1 AND ep.codigo =  "+ codigo;
            DataSet lista = conexion.consultaMySql(consulta);

            if (lista.Tables[0].Rows.Count > 0)
            {
                return lista.Tables[0].Rows[0][0].ToString();
            }
            else
                return "ninguno";

            
        }


        public DataSet buscarEncargadoPago2(int codigo)
        {
            string consulta = "select ep.codigo, ep.nombre as 'NombreEncargadoPago', ep.ci, ep.telefono, ep.celular, ep.direccion, ep.email, facturar_A, Nit, ep.banco, ep.observacion from tb_propietario ep where ep.codigo = "+codigo;
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

    }
}