using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;


namespace jycboliviaASP.net.Negocio
{
    public class NA_Propietario
    {

        DA_Propietario encargadoPago = new DA_Propietario();

        public NA_Propietario()
        {
            
         }

         public bool registrar(String nombre, String ci, string telefono, string celular, string direccion, string email, string facturar_A, string nit, int estado, string banco, string observacion)
         {
            
           return encargadoPago.insertarEncargadoPago(nombre, ci, telefono, celular, direccion, email,facturar_A, nit,estado,banco, observacion);
            
        }
                

        public bool modificar(int codigo, String nombre, String ci, string telefono, string celular, string direccion, string email, string facturar_A, string nit, string banco, string observacion)
        {
            return encargadoPago.modificarEncargadoPago(codigo, nombre, ci, telefono, celular, direccion, email, facturar_A, nit, banco, observacion);
        }

        public bool eliminar(int codigo) 
        {
            return encargadoPago.eliminarEncargadoPago(codigo);
        }

        public DataSet listar() 
        {
            DataSet lista = encargadoPago.listar();
            return lista;
        }

        public DataSet buscar(string nombre,string ci) 
        {
            DataSet lista = encargadoPago.buscarEncargadoPago(nombre,ci);
            return lista;
        }

        ///-------------------------------------------------------
        public bool existe(string nombreEncargadoPago)
        {
            DataSet tuplaUsuario = encargadoPago.existeEncargadoPago(nombreEncargadoPago);
            if (tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool registrar1(String nombre, int estado)
        {
            return encargadoPago.insertarEncargadoPago(nombre,"","","","","","","",1,"","");
        }

        public int obtenerUltimoCodigo()
        {
            return encargadoPago.getUltimoCodigo();
        }

        public int obtenerCodigoEncargadoPago(string nombreEncargadoPago)
        {
            return encargadoPago.getCodigoEncargadoPago(nombreEncargadoPago);
        }

        public int obtenerCodigoEncargadoPagoProyecto(string nombreProyecto)
        {
            return encargadoPago.getCodigoEncargadoPagoProyecto(nombreProyecto);
        }

        public DataSet buscador(string nombre)
        {
            DataSet lista = encargadoPago.buscador(nombre);
            return lista;
        }

        public string getEncargadoPago(int codigo)
        {
            return encargadoPago.getEncargadoPago(codigo);
        }

        public DataSet buscarEncargadoPago2(int codigo)
        {
            return encargadoPago.buscarEncargadoPago2(codigo); 
        }

    }
}