using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Zona
    {
        private DA_Zona Dzona = new DA_Zona();

        public NA_Zona() { }

        public bool insertar(string nombre, string detalle, int estado)
        {
            return Dzona.insertar(nombre);
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
            string consulta = "select c.codigo, c.nombre from tb_zona c";
            return Dzona.getDatos(consulta);
        }

    }
}