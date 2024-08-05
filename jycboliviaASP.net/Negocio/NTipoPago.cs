using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NTipoPago
    {
        DTipoPago tipoPago = new DTipoPago();
        public NTipoPago() {
            
         }
                
        public bool registrar(String nombre, int estado )
        {
            return tipoPago.insertarTipoPago(nombre, estado);
        }

        public bool modificar(int codigo, string nombre)
        {
            return tipoPago.modificarTipoPago(codigo, nombre);
        }

        public void eliminar(int codigo)
        {
            tipoPago.eliminarTipoPago(codigo);
        }

        public DataSet buscar(string nombre)
        {
            DataSet lista = tipoPago.buscarTipoPago(nombre);
            return lista;
        }

        public DataSet listar()
        {
            DataSet lista = tipoPago.listarTipoPago();
            return lista;
        }
    }
}