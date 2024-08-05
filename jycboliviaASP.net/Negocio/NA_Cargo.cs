using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Cargo
    {

        public NA_Cargo() { }
        private DA_Cargo Dcargo = new DA_Cargo();

        public bool insertar(string nombre, string detalle, int estado)
        {
            return Dcargo.insertar(nombre,detalle,estado);
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
            string consulta = "select c.codigo, c.nombre, c.detalle from tb_cargo c";
            return Dcargo.getDatos(consulta);
        }
    }
}