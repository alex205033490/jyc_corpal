using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DEncargadoPago 
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

        public DEncargadoPago() { }

        public DEncargadoPago(int codigo, string nombre, string ci, string telefono, string celular, string direccion, string email, string facturar_A, int nit) {
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
                string consulta = "insert into tb_encargado_pago(nombre, ci, telefono, celular, direccion, email, facturar_A, nit, estado, banco, observacion) values ('" + nombre + "', '" + ci + "', '" + telefono + "', '" + celular + "', '" + direccion + "','" + email + "', '" + facturar_A + "', '" + nit + "', 1, '"+banco+"', '"+observacion+"') ;";
               bool bandera = conexion.ejecutarMySql(consulta);
                return bandera;            
        }

        public bool modificarEncargadoPago(int codigo, String nombre, String ci, string telefono, string celular, string direccion, string email, string facturar_A, string nit, string banco, string observacion)
        {

                string consulta = "update tb_encargado_pago set tb_encargado_pago.nombre= '"+nombre+"', tb_encargado_pago.ci= '"+ci+"', tb_encargado_pago.telefono='"+telefono+"', tb_encargado_pago.celular='"+celular+"', tb_encargado_pago.direccion='"+direccion+"', tb_encargado_pago.email='"+email+"', tb_encargado_pago.facturar_A ='"+facturar_A+"', tb_encargado_pago.Nit = '"+nit+"', tb_encargado_pago.estado ="+1+", "
                                    + "tb_encargado_pago.banco = '" + banco + "', tb_encargado_pago.observacion = '" + observacion+"' " 
                                        +" where tb_encargado_pago.codigo= " + codigo + ";";
                return conexion.ejecutarMySql(consulta);
               
        }

        public DataSet listar()
        {
                string consulta = "select codigo, nombre, ci, telefono, celular, direccion, email, facturar_A, Nit from tb_encargado_pago where tb_encargado_pago.estado!=0";
                DataSet lista = conexion.consultaMySql(consulta);
                return lista;
        }

        public bool eliminarEncargadoPago(int codigo)
        {
         
                string consulta = "update tb_encargado_pago set tb_encargado_pago.estado =" + 0 + " where tb_encargado_pago.codigo= " + codigo + ";";
                return conexion.ejecutarMySql(consulta);
          
         
        }

        public DataSet buscarEncargadoPago(string nombre,string ci) {
            string consulta = "select ep.codigo, ep.nombre as 'NombreEncargadoPago', ep.ci, ep.telefono, ep.celular, ep.direccion, ep.email, facturar_A, Nit from tb_encargado_pago ep where ep.ci like '%" + ci + "%' and ep.nombre like '%" + nombre + "%' ;";
                DataSet lista = conexion.consultaMySql(consulta);
                return lista;
        }

        ///---------------------------------------------------------
        public DataSet existeEncargadoPago(string nombreEncargadoPago)
        {
            string consulta = "SELECT *FROM tb_encargado_pago ep WHERE ep.`nombre`='" + nombreEncargadoPago + "'";
            DataSet resultado = conexion.consultaMySql(consulta);
            return resultado;
        }

      /*  public bool insertarEncargadoPago1(String nombre, int estado)
        {
            try
            {
                string consulta = "insert into tb_encargado_pago(nombre, 1) ;";
                conexion.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }*/


        public int getUltimoCodigo_insertado()
        {
            try
            {
                string consulta = "SELECT MAX(codigo) FROM tb_encargado_pago";
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
            string consulta = "SELECT ep.codigo FROM tb_encargado_pago ep WHERE ep.nombre='" + nombreEncargadoPago + "'";
            DataSet datoResul = conexion.consultaMySql(consulta);
            if(datoResul.Tables[0].Rows.Count > 0){
                int result;
                bool isnumero = int.TryParse(datoResul.Tables[0].Rows[0][0].ToString(), out result);            
                if (isnumero){                    
                    return result;
                }
                else
                    return -1;
            }else
                return -1;           
        }


        public int getCodigoEncargadoPagoProyecto(string nombreProyecto)
        {
            string consulta = "SELECT ep.codigo FROM `tb_proyecto` p, `tb_encargado_pago` ep WHERE p.`codEncargado` = ep.`codigo` AND p.`nombre` = '" + nombreProyecto + "'";
            DataSet datoResul = conexion.consultaMySql(consulta);

            int codUltimo = -1;
            if(datoResul.Tables[0].Rows.Count > 0){
                codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
            }
            
            return codUltimo;
        }


        public DataSet buscador(string nombre)
        {
            string consulta = "SELECT ep.`nombre` FROM `tb_encargado_pago` ep WHERE ep.`estado`=1 AND ep.`nombre` LIKE '%" + nombre + "%' ORDER BY ep.`nombre` ASC";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public string getEncargadoPago(int codigo)
        {
            string consulta = "SELECT ep.nombre FROM tb_encargado_pago ep WHERE ep.estado=1 AND ep.codigo =  "+ codigo;
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
            string consulta = "select ep.codigo, ep.nombre as 'NombreEncargadoPago', ep.ci, ep.telefono, ep.celular, ep.direccion, ep.email, facturar_A, Nit, ep.banco, ep.observacion from tb_encargado_pago ep where ep.codigo = "+codigo;
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

    }
}