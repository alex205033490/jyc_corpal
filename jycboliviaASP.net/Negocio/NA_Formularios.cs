using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;


namespace jycboliviaASP.net.Negocio
{
    public class NA_Formularios
    {
        private DA_Formularios Dformularios = new DA_Formularios();
        
        public NA_Formularios() { }
        public bool insertar(string nombre, int estado)
        {
            return Dformularios.insertar(nombre, estado);
        }

        public bool modificar(int codigo, string nombre, int estado)
        {
            return Dformularios.modificar(codigo,nombre,estado);
        }

        public bool eliminar(int codigo)
        {
            return Dformularios.eliminar(codigo, 0);
        }

        public DataSet mostrarAllDatos()
        {
            string consulta = "select f.CODIGO, f.NOMBRE from tb_formulario f";
            return Dformularios.getDatos(consulta);
        }

        public DataSet buscarFormularios(string nombre)
        {
            string consulta = "select form.CODIGO, form.NOMBRE from tb_formulario form where form.nombre like '%" + nombre + "%'";
            return Dformularios.getDatos(consulta);
        }
        
    }
}