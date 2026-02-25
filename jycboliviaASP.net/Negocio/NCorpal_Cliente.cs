using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using static jycboliviaASP.net.Presentacion.FCorpal_APIProduccion;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_Cliente
    {
        DCorpal_Cliente dtienda = new DCorpal_Cliente();

        public NCorpal_Cliente() { }


        public bool eliminar_cliente(int codigo)
        {
            try
            {
                DCorpal_Cliente dtienda = new DCorpal_Cliente();
                return dtienda.eliminar_cliente(codigo);
            }
            catch (Exception ex)
            {
                throw ex; // Reenviamos el error para mostrarlo en la alerta
            }
        }

        public bool existeClienteParaModificar(string nombreCliente, int idActual)
        {
            DataSet tuplaUsuario = dtienda.get_clienteNombreExcluyendoID(nombreCliente, idActual);

            // Si devuelve filas, significa que OTRO cliente ya tiene ese nombre
            if (tuplaUsuario != null && tuplaUsuario.Tables.Count > 0 && tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true; // Existe un duplicado (conflicto)
            }
            else
            {
                return false; // No hay conflicto (o es el mismo registro, o es nombre nuevo)
            }
        }

        public bool modificar_cliente(
            int codigo, // <--- EL ID ES OBLIGATORIO AQUÍ
            string tiendaname, string tiendadir, string tiendatelefono, string tiendadepartamento, string tiendazona,
            string propietarioname, string propietarioci, string propietariodir, string propietariocelular, string propietarionit, string propietariocorreo,
            string facturar_a, string facturar_nit, string facturar_correo, string observacion,
            int codUser,  string latitud, string longitud,
            int id_tipocliente, int id_listaprecio)
        {
            try
            {
                return dtienda.modificar_cliente(
                    codigo, // Pasamos el ID a la capa de datos
                    tiendaname, tiendadir, tiendatelefono, tiendadepartamento, tiendazona,
                    propietarioname, propietarioci, propietariodir, propietariocelular, propietarionit, propietariocorreo,
                    facturar_a, facturar_nit, facturar_correo, observacion,
                    codUser,  latitud, longitud,
                    id_tipocliente, id_listaprecio
                );
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool eliminarListaPrecio(int idListaProducto) {
            try
            {
                // Llamamos directamente a la capa de datos y retornamos su resultado (true o false)
                return dtienda.eliminarListaPrecio(idListaProducto);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool insertarListaPrecio(string nombre, string descripcion, decimal descuentogral)
        {
            try
            {
                // Llamamos directamente a la capa de datos y retornamos su resultado (true o false)
                return dtienda.insertarListaPrecio(nombre, descripcion, descuentogral);
            }
            catch (Exception)
            {
                return false;
            }
        }


        
        public bool insertarDetalleLista(int idLista, int idProducto, decimal precioEspecial, decimal descuento, decimal precio, string unidad, decimal cantidadDesde, int cantidadMinima, decimal aumento)
        {
            try
            {
                // Llamamos a la capa de datos pasándole todos los parámetros correctos
                // (Asegúrate de que "dtienda" sea el nombre correcto de tu instancia de la capa de Datos)
                return dtienda.insertarDetalleLista(idLista, idProducto, precioEspecial, descuento, precio, unidad, cantidadDesde, cantidadMinima, aumento);
            }
            catch (Exception)
            {
                return false;
            }
        }






        public bool insertar_cliente(
                string tiendaname, string tiendadir, string tiendatelefono, string tiendadepartamento, string tiendazona,
                string propietarioname, string propietarioci, string propietariodir, string propietariocelular, string propietarionit, string propietariocorreo,
                string facturar_a, string facturar_nit, string facturar_correo, string observacion,

                int codUser, // Este es codrespgra
                string latitud,
                string longitud,
                int id_tipocliente,
                int id_listaprecio)
        {
            try
            {
                // CORRECCIÓN 1: Agregar "return" antes de llamar al método.
                // CORRECCIÓN 2: Agregar "null" (u 0) para el parámetro 'cod_clienteupon' que faltaba en medio.

                return dtienda.insertar_cliente(
                    tiendaname, tiendadir, tiendatelefono, tiendadepartamento, tiendazona,
                    propietarioname, propietarioci, propietariodir, propietariocelular, propietarionit, propietariocorreo,
                    facturar_a, facturar_nit, facturar_correo, observacion,

                    codUser, // codrespgra
                    
                    latitud,
                    longitud,

                    id_tipocliente, id_listaprecio
                );
            }
            catch (Exception)
            {
                return false;
            }
        }



        public bool existeCliente(string nombreCliente)
        {
            DataSet tuplaUsuario = dtienda.get_clienteNombre(nombreCliente);

            // VALIDACIÓN: Verificamos que no sea nulo Y que tenga filas
            if (tuplaUsuario != null && tuplaUsuario.Tables.Count > 0 && tuplaUsuario.Tables[0].Rows.Count > 0)
            {
                return true; // Ya existe
            }
            else
            {
                return false; // No existe, puedes guardar
            }
        }

        public DataSet mostrarListaPrecio()
        {
            DataSet lista = dtienda.mostrarListaPrecio();
            return lista;
        }
        
        public DataSet mostrarTipoCliente() {
            DataSet lista = dtienda.mostrarTipoCliente();
            return lista;
        }


        public DataSet get_cliente(int codigo)
        {
            DataSet lista = dtienda.get_cliente(codigo);
            return lista;
        }
        


        public DataSet listarTiendas2(string nombreTiendas)
        {
            DataSet lista = dtienda.listarTiendas2(nombreTiendas);
            return lista;
        }

        public DataSet listarListaProducto(string nombreLista)
        {
            DataSet lista = dtienda.listarListaProducto(nombreLista);
            return lista;
        }


        public DataSet obtenerDatosProducto(int idProducto)
        {
            // dprod sería la instancia de tu capa de datos de producto
            return dtienda.obtenerDatosProducto(idProducto);
        }


        public DataSet listarDetalleListaProducto(int idLista)
        {
            DataSet lista = dtienda.listarDetalleListaProducto(idLista);
            return lista;
        }

        public DataSet listarProductosActivos() {
            DataSet lista = dtienda.listarProductosActivos();
            return lista;



        }

        // EN TU CAPA DE NEGOCIO
        public bool eliminarDetalleLista(int codigoDetalle)
        {
            // Llamamos a la capa de datos (asegúrate de que 'dtienda' sea tu instancia correcta)
            return dtienda.eliminarDetalleLista(codigoDetalle);
        }

        // EN TU CAPA DE NEGOCIO
        public bool existeNombreLista(string nombreLista, int idListaActual = 0)
        {
            // Llamamos a la capa de datos
            return dtienda.existeNombreLista(nombreLista, idListaActual);
        }

        // EN TU CAPA DE NEGOCIO
        public bool actualizarListaPrecio(int codigoLista, string nombre, string descripcion, decimal descuentoGral)
        {
            return dtienda.actualizarListaPrecio(codigoLista, nombre, descripcion, descuentoGral);
        }


        // EN TU CAPA DE NEGOCIO
        public bool existeProductoEnLista(int idLista, int idProducto, int codigoDetalleActual = 0)
        {
            return dtienda.existeProductoEnLista(idLista, idProducto, codigoDetalleActual);
        }

        // EN TU CAPA DE NEGOCIO
        public bool actualizarDetalleListaProducto(int codigoDetalle, decimal porcentajeDcto, decimal porcentajeAumento, decimal precioFinalCalculado)
        {
            return dtienda.actualizarDetalleListaProducto(codigoDetalle, porcentajeDcto, porcentajeAumento, precioFinalCalculado);
        }



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
    