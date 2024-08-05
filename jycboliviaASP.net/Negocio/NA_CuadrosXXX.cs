using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_CuadrosXXX
    {
        DA_CuadrosXXX DcuadroXXX = new DA_CuadrosXXX();

        public NA_CuadrosXXX() { }

        public bool insertar()
        {
            return DcuadroXXX.insertar();
        }

        public bool modificar()
        {
            return DcuadroXXX.modificar();
        }

        public bool eliminar()
        {
            return DcuadroXXX.eliminar();
        }

        public bool truncarTabla()
        {
            return DcuadroXXX.truncarTabla();
        }

        public bool insertarCuadrosXXX(string Archivo)
        {
            return DcuadroXXX.insertarCuadrosXXX(Archivo);
        }

        public DataSet mostrarCuadrosXXX()
        {
            string consulta = "select * from tb_seguimiento_aux";
            DataSet resultado = DcuadroXXX.getDatos(consulta);
            return resultado;
        }

    /*    public DataSet mostrarAllDepartamentos()
        {
            string consulta = "select ex.codigo,ex.poblacionobra as 'nombre' from tb_expedicion ex group by ex.poblacionobra";
            DataSet resultado = DfechaExp.getDatos(consulta);
            return resultado;
        }

        public DataSet mostrarBusqueda(string dpto)
        {
            string consulta = "select * from tb_expedicion ex where ex.poblacionobra like '" + dpto + "'";
            DataSet resultado = DfechaExp.getDatos(consulta);
            return resultado;
        }
     */ 
     }
}