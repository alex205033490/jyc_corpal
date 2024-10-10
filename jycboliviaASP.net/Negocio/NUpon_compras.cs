using jycboliviaASP.net.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    public class NUpon_compras
    {
        public NUpon_compras() { }

        DUpon_compras dupon = new DUpon_compras();
        public bool insertar()
        {
            return dupon.insertar();
        }

        public bool modificar()
        {
            return dupon.modificar();
        }

        public bool eliminar()
        {
            return dupon.eliminar();
        }

        public bool truncarTabla()
        {
            return dupon.truncarTabla();
        }

        public bool insertarComprasRealizadas(string Archivo)
        {
            return dupon.insertarComprasRealizadas(Archivo);
        }

        public DataSet mostrarComprasRealizadas(string glosa)
        {
            string consulta = "select * from tbupon_compras pp where pp.glosa like '%"+glosa+ "%' and pp.estado = 1 and pp.vaciadoupon = false";
            DataSet resultado = dupon.getDatos(consulta);
            return resultado;
        }

       

        public DataSet mostrarBusqueda(string dprod_NumeroItem)
        {
            string consulta = "select * from tbupon_compras ex where ex.dprod_NumeroItem like '" + dprod_NumeroItem + "'";
            DataSet resultado = dupon.getDatos(consulta);
            return resultado;
        }

        internal bool eliminarVaciadoCompras(int codigoEliminar)
        {   
            return  dupon.eliminarVaciadoCompras(codigoEliminar);
        }

        internal DataSet GetDatoParaVaciar(int codigoVaciar)
        {
            DataSet datos = dupon.GetDatoParaVaciar(codigoVaciar);
            return datos;
        }

        internal bool updateVaciadoOk(int codigoVaciar)
        {
            return dupon.updateVaciadoOk(codigoVaciar);
        }
    }
}