using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_Responsable
    {
        private conexionMySql ConecRes = new conexionMySql();
        
        public DA_Responsable() { 
                
        }

        public bool insertarResponsable(string nombre, string direccion, string telefono, string celular, string email, string dpto, string ciudad, int sueldo, string loggin, string PassWord, int cargo, int estado) {
            
            try
            {
                string consulta = "insert into tb_responsable (nombre,direccion,telefono,celular,email,departamento,ciudad,sueldo,loggin,Passw,cargoc,estado) values ('" + nombre + "','" + direccion + "','" + telefono + "', '" + celular + "', '" + email + "','" + dpto + "', '" + ciudad + "', " + sueldo + ",'" + loggin + "','" + PassWord + "'," + cargo + ",1);";
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception )
            {
                return false;            
            }

        }

        public bool modificarResponsable(int codigo,string nombre, string direccion, string telefono, string celular, string email, string dpto, string ciudad, int sueldo, string loggin, string PassWord, int cargo, int estado)
        {
            try{
                string consulta = "update tb_responsable set nombre='"+nombre+"',direccion='"+direccion+"',telefono='"+telefono+"',celular='"+celular+"',email='"+email+"',departamento='"+dpto+"',ciudad='"+ciudad+"',sueldo="+sueldo+",loggin='"+loggin+"',Passw='"+PassWord+"',cargoc="+cargo+" where codigo ="+codigo;
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch(Exception ){
                return false;
            }
        }

        public bool eliminarResponsable(int codigo, int estado) {
            try
            {
                string consulta = "update tb_responsable set estado="+estado+" where codigo =" + codigo;
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public DataSet getDatos(string consulta) {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;        
        }
    }
}