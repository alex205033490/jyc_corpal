using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{    
    public class NA_TipoEquipo
    {
        private DA_TipoEquipo DtipoEquipo = new DA_TipoEquipo();

        public NA_TipoEquipo() { }

        public bool insertar()
        {
            return DtipoEquipo.insertar();          
        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar()
        {
            return false;
        }

        public  DataSet mostrarAllDatos() {
            string consulta = "select c.codigo, c.nombre from tb_tipoequipo c";
            return DtipoEquipo.getDatos(consulta);        
        }

        public int getCodigoTipoEquipo(string nombre)
        {
            string consulta = "select c.codigo, c.nombre from tb_tipoequipo c where c.nombre = '"+nombre+"'";
            try
            {
               DataSet dato = DtipoEquipo.getDatos(consulta);
               if (dato.Tables[0].Rows.Count > 0)
               {
                   return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
               }
               else
                   return -1;            
            }
            catch (Exception) {
                return -1;
            }
            
        }

    }
}