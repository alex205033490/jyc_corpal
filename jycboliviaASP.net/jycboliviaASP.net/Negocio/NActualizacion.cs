using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NActualizacion
    {
                
        DActualizacion actualizacion = new DActualizacion();

        public NActualizacion() {
            
         }
                
        public bool registrar(String nombre, int estado )
        {
                return actualizacion.insertarActualizacion(nombre, estado);
        }

        public bool modificar(int codigo, string nombre)
        {
            return actualizacion.modificarActualizacion(codigo, nombre);
        }

        public void eliminar(int codigo)
        {
            actualizacion.eliminarActualizacion(codigo);
        }

        public DataSet buscar(string nombre)
        {
            DataSet lista = actualizacion.buscarActualizacion(nombre);
            return lista;
        }

        public DataSet listar()
        {
            DataSet lista = actualizacion.listarActualizacion();
            return lista;
        }
    }
}