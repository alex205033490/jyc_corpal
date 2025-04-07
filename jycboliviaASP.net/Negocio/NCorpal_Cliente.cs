using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Datos;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_Cliente
    {
        DCorpal_Cliente dtienda = new DCorpal_Cliente();

        public NCorpal_Cliente() { }

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
            return dtienda.guardarDatosTienda(tiendaname, tiendadir, tiendatelefono, tiendadepartamento, tiendazona, propietarioname, propietarioci, propietariodir, propietariocelular, propietarionit, propietariocorreo, facturar_a, facturar_nit, facturar_correo, observacion, codUserGra);
        }

        internal bool updateDatosTienda(int codigo, string tiendaname, string tiendadir, string tiendatelefono, string tiendadepartamento, string tiendazona, string propietarioname, string propietarioci, string propietariodir, string propietariocelular, string propietarionit, string propietariocorreo, string facturar_a, string facturar_nit, string facturar_correo, string observacion, int codUserGra)
        {
            return dtienda.updateDatosTienda(codigo, tiendaname, tiendadir, tiendatelefono, tiendadepartamento, tiendazona, propietarioname, propietarioci, propietariodir, propietariocelular, propietarionit, propietariocorreo, facturar_a, facturar_nit, facturar_correo, observacion, codUserGra);
        }

        internal DataSet get_tienda(int codigo)
        {
            return dtienda.get_tienda(codigo);
        }

        internal DataSet buscarPropietario(string nombre)
        {
            return dtienda.buscarPropietario(nombre);
        }

        internal DataSet get_ClienteNombre(string tiendanombre)
        {
            return dtienda.get_ClienteNombre(tiendanombre);
        }

        internal DataSet get_ClienteNombreEspecifico(string tiendanombre)
        {
            return dtienda.get_ClienteNombreEspecifico(tiendanombre);
        }
        internal DataSet get_ClienteCodigo(int codigo)
        {
            return dtienda.get_ClienteCodCliente(codigo);
        }

        internal int get_CodigoCliente(string cliente)
        {
            int codigo = 0;
            DataSet tabla = dtienda.get_ClienteNombreEspecifico(cliente);
            if (tabla.Tables[0].Rows.Count > 0)
            {
                int.TryParse(tabla.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            }
            else
                return codigo;
        }

        internal bool set_clienteSolicitud(string cliente, string propietario, string razonSocial, string nit,  int codUser)
        {
            return dtienda.guardarDatosTienda(cliente, "", "", "", "", propietario, "", "","","","",razonSocial,nit,"","",codUser);
        }

        internal int get_clienteUltimoIngresado(string cliente, string propietario, string razonSocial, string nit)
        {
            DataSet tupla = dtienda.get_clienteUltimoIngresado(cliente,  propietario, razonSocial,  nit);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                int codigo = 0;
                int.TryParse(tupla.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            }
            else
                return 0;
        }

        internal bool updateDatosTiendaSolicitud(int codigCliente, string cliente, string propietario, string razonsocial,string nit, int codpersolicitante)
        {
            return dtienda.updateDatosTiendaSolicitud( codigCliente,  cliente, razonsocial, propietario,  nit,  codpersolicitante);
        }
    }
}
    