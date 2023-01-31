using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Datos;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_Tienda
    {
        DCorpal_Tienda dtienda = new DCorpal_Tienda();

        public NCorpal_Tienda() { }

        public DataSet listarTiendas(string nombreTiendas)
        {
            DataSet lista = dtienda.listarTiendas(nombreTiendas);
            return lista;
        }

        internal bool eliminarTienda(int codigo)
        {
            return dtienda.eliminarTienda(codigo);
        }

        internal bool guardarDatosTienda(string tiendaname, string tiendadir, string tiendatelefono, string tiendadepartamento, string tiendazona, string propietarioname, string propietarioci, string propietariodir, string propietariocelular, string propietarionit, string propietariocorreo, string facturar_a, string facturar_nit, string facturar_correo, string observacion, int codUserGra)
        {
            return dtienda.guardarDatosTienda( tiendaname,  tiendadir,  tiendatelefono,  tiendadepartamento,  tiendazona,  propietarioname,  propietarioci,  propietariodir,  propietariocelular,  propietarionit,  propietariocorreo,  facturar_a,  facturar_nit,  facturar_correo,  observacion,  codUserGra);
        }

        internal bool updateDatosTienda(int codigo, string tiendaname, string tiendadir, string tiendatelefono, string tiendadepartamento, string tiendazona, string propietarioname, string propietarioci, string propietariodir, string propietariocelular, string propietarionit, string propietariocorreo, string facturar_a, string facturar_nit, string facturar_correo, string observacion, int codUserGra)
        {
            return dtienda.updateDatosTienda( codigo,  tiendaname,  tiendadir,  tiendatelefono,  tiendadepartamento,  tiendazona,  propietarioname,  propietarioci,  propietariodir,  propietariocelular,  propietarionit,  propietariocorreo,  facturar_a,  facturar_nit,  facturar_correo,  observacion,  codUserGra);
        }

        internal DataSet get_tienda(int codigo)
        {
            return dtienda.get_tienda(codigo);
        }

        internal DataSet buscarPropietario(string nombre)
        {
           return dtienda.buscarPropietario( nombre);
        }

        internal DataSet get_tiendaNombre(string tiendanombre)
        {
            return dtienda.get_tiendaNombre(tiendanombre);
        }
    }
}