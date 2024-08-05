using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_EstadoMantenimiento
    {

        DA_EstadoMantenimiento DestadoMantenimiento = new DA_EstadoMantenimiento();

        public NA_EstadoMantenimiento() { }

        public bool insertar()
        {
            return DestadoMantenimiento.insertar();
        }

        public bool modificar()
        {
            return DestadoMantenimiento.modificar();
        }

        public bool eliminar()
        {
            return DestadoMantenimiento.eliminar();
        }

        public DataSet mostrarAllDatos() {
            string consulta = "select eman.codigo,eman.nombre from tb_estado_mantenimiento eman order by eman.codigo asc";
            DataSet tuplaRes = DestadoMantenimiento.getDatos(consulta);
            return tuplaRes;
        }
       
    }
}