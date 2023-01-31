using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Datos;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_RutaEntrega
    {
        DCorpal_RutaEntrega dRutaEntrega = new DCorpal_RutaEntrega();

        public NCorpal_RutaEntrega() { }

        public DataSet BuscarRutasEntrega(string nombreTienda, string PersonalAsignado)
        {
            DataSet lista = dRutaEntrega.BuscarRutasEntrega(nombreTienda, PersonalAsignado);
            return lista;
        }

        internal bool eliminarRutaEntrega(int codigo)
        {
            return dRutaEntrega.eliminarRutaEntrega(codigo);
        }

        internal bool guardarDatosRutasEntrega(string fechacobro, string  horacobro, string  detalle, string  tiendanombre, int  codtienda, int  coduserasignado, string  personalasignado, int  coduserinicio, string  estadoEntrega)
        {
            return dRutaEntrega.guardarDatosRutasEntrega( fechacobro,   horacobro,   detalle,   tiendanombre,   codtienda,   coduserasignado,   personalasignado,   coduserinicio,   estadoEntrega);
        }

        internal bool updateDatosRutasEntrega(int codigo ,string fechacobro, string horacobro, string detalle, string tiendanombre, int codtienda, int coduserasignado, string personalasignado, int coduserinicio, string estadoEntrega)
        {
            return dRutaEntrega.updateDatosRutasEntrega( codigo , fechacobro,  horacobro,  detalle,  tiendanombre,  codtienda,  coduserasignado,  personalasignado,  coduserinicio,  estadoEntrega);
        }

        internal DataSet get_RutaEntrega(int codigo)
        {
            return dRutaEntrega.get_RutaEntrega(codigo);
        }



        internal int get_UltimaRutaIngresada(string tiendanombre)
        {
            DataSet tupla = dRutaEntrega.get_UltimaRutaIngresada(tiendanombre);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                return int.Parse(tupla.Tables[0].Rows[0][0].ToString());
            }
            else
                return -1;
        }

        internal bool insertarProductos(int codigoRutaIngresada, string Producto, double Cantidad)
        {
            return dRutaEntrega.insertarProductos( codigoRutaIngresada, Producto, Cantidad);
        }

        internal bool eliminarProductosRutaEntreta(int codigo)
        {
            return dRutaEntrega.eliminarProductosRutaEntreta( codigo);
        }

        internal DataSet get_productosAdicionados(int codigoRuta)
        {
            return dRutaEntrega.get_productosAdicionados(codigoRuta);
        }
    }
}