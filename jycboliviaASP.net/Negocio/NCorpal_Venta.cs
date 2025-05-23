﻿using jycboliviaASP.net.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_Venta
    {
        DCorpal_Venta dv = new DCorpal_Venta();
        public NCorpal_Venta() { }

        internal bool anularVendidoVaciadoUpon(int codigoVendido, bool bandera)
        {
            return dv.anularVendidoVaciadoUpon( codigoVendido,  bandera);
        }

        internal bool crearVenta(int codClient, string cliente, string correoCliente, string municipio, string telefono, string direccion, string numeroFactura, 
            string nombreRazonSocial, string numeroDocumento, int codigoMetodoPago, decimal montoTotal, int codigoMoneda, decimal tipoCambio, 
            decimal montoTotalMoneda, decimal descuentoAdicional, string leyendaF, int codresp, string responsable, bool factura, string fechaentrega, int codsolicitudentregaproducto)
        {
            return dv.crearVenta( codClient,  cliente,  correoCliente,  municipio,  telefono,  direccion,  numeroFactura,  nombreRazonSocial,  numeroDocumento,  codigoMetodoPago,  montoTotal,  codigoMoneda,  tipoCambio,  montoTotalMoneda,  descuentoAdicional,  leyendaF,  codresp,  responsable,  factura,  fechaentrega , codsolicitudentregaproducto);
        }
        
        internal bool crearVentas3(int codClient, string cliente, int codSolicitud, string correoCliente, string municipio, string telefono, string numeroFactura, string direccion,
            string nombreRazonSocial, string numeroDocumento, int codigoMetodoPago, decimal montoTotal, int codigoMoneda, decimal tipocambio, decimal montoTotalMoneda,
            decimal descuentoAdicional, string leyendaF, int codresp, string responsable, int factura , string fechaentrega, int codsolicitudentregaproducto)
        {
            return dv.crearVentas3(codClient, cliente, codSolicitud, correoCliente, municipio, telefono, numeroFactura, direccion, 
                nombreRazonSocial, numeroDocumento, codigoMetodoPago, montoTotal, codigoMoneda, tipocambio, montoTotalMoneda, 
                descuentoAdicional, leyendaF, codresp, responsable, factura, fechaentrega,codsolicitudentregaproducto);
        }

        internal DataSet get_allVentasParaVaciarUpon(string cliente)
        {
            return dv.get_allVentasParaVaciarUpon( cliente);
        }

        internal int get_codigoVentaUltimoInsertado(string cliente, string nombreRazonSocial, string nombreResponsable)
        {
            DataSet tupla = dv.get_codigoVentaUltimoInsertado( cliente,  nombreRazonSocial,  nombreResponsable);
            int codigo = 0;
            if (tupla.Tables[0].Rows.Count > 0) {
                int.TryParse(tupla.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            } else
                return codigo;
        }

        internal DataSet get_ItemVendidos(int codigoVendido)
        {
            DataSet tuplasDI = dv.get_ItemVendidos( codigoVendido);
            return tuplasDI;
        }

        internal DataSet get_ventaRealizadaparaVaciar(int codigoVendido)
        {
            DataSet tuplaV = dv.get_ventaRealizadaparaVaciar(codigoVendido);
            return tuplaV;
        }

        internal bool insertarTodoslosProductosAVenta(int codigoVenta, int codigoSolicitud)
        {
            return dv.insertarTodoslosProductosAVenta( codigoVenta,  codigoSolicitud);
        }

        internal bool insertarTodoslosProductosAVenta3( int codigoSolicitud)
        {
            return dv.insertarTodoslosProducosAVenta3( codigoSolicitud);
        }

        internal DataSet get_productoCodProducto(int codigo)
        {
            DataSet tuplaV = dv.get_productoCodProducto(codigo);
                return tuplaV;

        }
    }
}