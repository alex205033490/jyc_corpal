using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NEstadoEquipo
    {
               DEstadoEquipo estadoEquipo = new DEstadoEquipo();

         public NEstadoEquipo() {
            
         }
                
        public bool registrar(String nombre, int estado )
        {
            return estadoEquipo.insertarEstadoEquipo(nombre, estado);
        }

        public bool modificar(int codigo, string nombre)
        {
            return estadoEquipo.modificarEstadoEquipo(codigo, nombre);
        }

        public void eliminar(int codigo)
        {
            estadoEquipo.eliminarEstadoEquipo(codigo);
        }

        public DataSet buscar(string nombre) 
        {
            DataSet lista = estadoEquipo.buscarEstadoEquipo(nombre);
            return lista;
        }

        public DataSet listar() 
        {
            DataSet lista = estadoEquipo.listarEstadoEquipo();
            return lista;
        }

        public int getCodigoEstadoEquipo(string NombreEstadoEquipo) {
            return estadoEquipo.getCodigoEstadoEquipo(NombreEstadoEquipo);
        }
    }
    
}