using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Marca
    {
        private DA_Marca Dmarca = new DA_Marca();
        public NA_Marca() { }

        public bool insertar()
        {

            try
            {

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

        public DataSet mostrarAllDatos()
        {
            string consulta = "select c.codigo, c.nombre from tb_marca c";
            return Dmarca.getDatos(consulta);
        }

        public int getCodigoMarca(string nombre)
        {
            string consulta = "select c.codigo, c.nombre from tb_marca c where c.nombre = '"+nombre+"'";
            try
            {
                DataSet dato = Dmarca.getDatos(consulta);
                if(dato.Tables[0].Rows.Count > 0){
                 return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
                }else
                    return -1;                
            }
            catch (Exception) { 
            return  -1;
            }
            
            
        }

    }
}