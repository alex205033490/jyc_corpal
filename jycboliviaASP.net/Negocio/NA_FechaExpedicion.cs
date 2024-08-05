using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_FechaExpedicion
    {

        DA_FechaExpedicion DfechaExp = new DA_FechaExpedicion();
        public NA_FechaExpedicion() { }

        public bool insertar()
        {
            return DfechaExp.insertar();
        }

        public bool modificar()
        {
            return DfechaExp.modificar();
        }

        public bool eliminar()
        {
            return DfechaExp.eliminar();
        }

        public bool truncarTabla()
        {
            return DfechaExp.truncarTabla();
        }

        public bool insertarFechaExpedicion(string Archivo) {
            return DfechaExp.insertarFechaExpedicion(Archivo);
        }

        public DataSet mostrarFechaExpedicion() {
            string consulta = "select * from tb_expedicion";
            DataSet resultado = DfechaExp.getDatos(consulta);
            return resultado;
        }

        public DataSet mostrarAllDepartamentos()
        {
            string consulta = "select ex.codigo,ex.poblacionobra as 'nombre' from tb_expedicion ex group by ex.poblacionobra";
            DataSet resultado = DfechaExp.getDatos(consulta);
            return resultado;
        }

        public DataSet mostrarBusqueda(string dpto) {
            string consulta = "select * from tb_expedicion ex where ex.poblacionobra like '"+dpto+"'";
            DataSet resultado = DfechaExp.getDatos(consulta);
            return resultado;
        }

    }
}