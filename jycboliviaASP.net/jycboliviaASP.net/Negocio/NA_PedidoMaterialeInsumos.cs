using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_PedidoMaterialeInsumos
    {
        DA_PedidoMaterialeInsumos dpedido = new DA_PedidoMaterialeInsumos();
        public NA_PedidoMaterialeInsumos() { }

        public bool insertarSolicitudMaterialInsumos(int codSolicitante, string solicitante, string fechaEstimadaEntrega, float montototal)
        {
            return dpedido.insertarSolicitudMaterialInsumos(  codSolicitante,  solicitante,  fechaEstimadaEntrega,  montototal);
        }

        public bool insertarItemMaterialInsumos(int codpedido, string proveedor, string item, string unidadmedida, float cantidad, float montototal, string factura, float retencion)
        {            
            return dpedido.insertarItemMaterialInsumos( codpedido,  proveedor,  item,  unidadmedida,  cantidad,  montototal,  factura,  retencion);
        }

        internal int get_ultimoinsertadoSolicitudMaterialInsumos(int codSolicitante, string Solicitante)
        {
            DataSet tupla = dpedido.get_ultimoinsertadoSolicitudMaterialInsumos( codSolicitante,  Solicitante);
            if(tupla.Tables[0].Rows.Count > 0){
                int codigo;
                int.TryParse(tupla.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            }else
            return -1;
        }

        internal bool updateSolicitudMontoTotal(int codSolicitud, float montoTotalSolicitud)
        {
            return dpedido.updateSolicitudMontoTotal( codSolicitud,  montoTotalSolicitud);
        }

        internal int get_CorrelativoSolicitudMaterialInsumos()
        {
            DataSet tupla = dpedido.get_CorrelativoSolicitudMaterialInsumos();         
            if (tupla.Tables[0].Rows.Count > 0)
            {
                int codigo1;
                int.TryParse(tupla.Tables[0].Rows[0][0].ToString(), out codigo1);
                return codigo1;
            }
            else
                return -1;
        }

        internal DataSet get_solicitudesMaterialeseInsumos(string responsableSolicitud, string EstadoSolicitud)
        {
            return dpedido.get_solicitudesMaterialeseInsumos(responsableSolicitud, EstadoSolicitud);
        }

        internal DataSet get_todosItemInsumosPedidos(int codigoPedido)
        {
            return dpedido.get_todosItemInsumosPedidos(codigoPedido);
        }

        internal bool update_CompradeInsumos(int codigoItem, float cantidadComprado, float montoComprado)
        {
            return dpedido.update_CompradeInsumos( codigoItem,  cantidadComprado,  montoComprado);
        }

        internal bool update_cerrarCompraMaterialInsumos(int codigoPedido, string estadoCompra, float montoTotalPedidoComprado, int codresponsableCompra, string ResponsableCompra)
        {
           return dpedido.update_cerrarCompraMaterialInsumos(codigoPedido,  estadoCompra,  montoTotalPedidoComprado,  codresponsableCompra,  ResponsableCompra);
        }

        internal DataSet get_todosItemInsumosComprados(int codigoPedido)
        {
           return dpedido.get_todosItemInsumosComprados(codigoPedido);
        }

        internal bool update_RecibirInsumosMaterial(int codigoItem, float cantidadRecibido)
        {
            return dpedido.update_RecibirInsumosMaterial(codigoItem, cantidadRecibido);
        }

        internal bool update_cerrarMaterialInsumosRecibido(int codigoPedido, string estadoCompra, int codresponsableRecibido, string ResponsableRecibido)
        {
            return dpedido.update_cerrarMaterialInsumosRecibido( codigoPedido,  estadoCompra,  codresponsableRecibido,  ResponsableRecibido);
        }
    }
}