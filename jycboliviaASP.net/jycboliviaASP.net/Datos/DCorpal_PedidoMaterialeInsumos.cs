using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class DCorpal_PedidoMaterialeInsumos
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DCorpal_PedidoMaterialeInsumos() { }

        public bool insertarSolicitudMaterialInsumos( int codSolicitante, string solicitante, string fechaEstimadaEntrega, float montototal)
        {
            string consulta = "insert into tbcorpal_pedidomateriaprimaeinsumos( fechaGRA, horaGRA, fechasolicitud, fechaesimadaentrega, personalsolicitud, " +
                             " codpersolicitante, estado, montototal, estadosolicitud) " +
                             " values(  current_date(), current_time(), current_date(), " + fechaEstimadaEntrega + ", '" + solicitante + "', " +
                              codSolicitante + ", 1, '"+montototal.ToString().Replace(',','.')+"', 'Abierto')";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool insertarItemMaterialInsumos(int codpedido, string proveedor, string  item, string unidadmedida, float cantidad,  float montototal, string factura, float retencion)
        {
            string consulta = "insert into tbcorpal_itempedidomateriaprimaeinsumos( "+
                             " proveedor,  item,  unidadmedida,  cantidad,  montounidad,  montototal,  factura, retencion,  codpedido "+
                             " ) values('"+proveedor+"', '"+item+"', '"+unidadmedida+"', '"+cantidad.ToString().Replace(',','.')+"', '"+(montototal/cantidad).ToString().Replace(',','.')+"' ,  '"+montototal.ToString().Replace(',','.')+"', '"+factura+"', "+
                             " '"+retencion.ToString().Replace(',','.')+"', "+codpedido+")";
            return ConecRes.ejecutarMySql(consulta);
        }


        internal DataSet get_ultimoinsertadoSolicitudMaterialInsumos(int codSolicitante, string Solicitante)
        {
            string consulta = "select max(codigo) from tbcorpal_pedidomateriaprimaeinsumos pp where pp.personalsolicitud = '"+Solicitante+"' and pp.codpersolicitante ="+codSolicitante;
            return ConecRes.consultaMySql(consulta);
        }

        internal bool updateSolicitudMontoTotal(int codSolicitud, float montoTotalSolicitud)
        {
            string consulta = "update tbcorpal_pedidomateriaprimaeinsumos set "+
                              " tbcorpal_pedidomateriaprimaeinsumos.montototal= '"+montoTotalSolicitud.ToString().Replace(',','.')+"', "+
                              " tbcorpal_pedidomateriaprimaeinsumos.nroboleta = '"+codSolicitud+"' "+
                              " where tbcorpal_pedidomateriaprimaeinsumos.codigo ="+codSolicitud;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal DataSet get_CorrelativoSolicitudMaterialInsumos()
        {
            string consulta = "select max(codigo) from tbcorpal_pedidomateriaprimaeinsumos pp ";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_solicitudesMaterialeseInsumos(string responsableSolicitud, string Estado)
        {
            string consulta = "select "+ 
                               " pp.codigo,  date_format(pp.fechasolicitud,'%d/%m/%Y') as 'Fecha_Solicitud', "+ 
                               " date_format(pp.fechaesimadaentrega,'%d/%m/%Y') as 'Fecha_EstimadaEntrega', "+ 
                               " pp.personalsolicitud, pp.estadosolicitud "+
                               " from tbcorpal_pedidomateriaprimaeinsumos pp where "+
                               " pp.estadosolicitud = '"+Estado+"'"+
            " and pp.personalsolicitud like '%" + responsableSolicitud + "%' ";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_todosItemInsumosPedidos(int codigoPedido)
        {
            string consulta = "select "+
                               " ii.codigo, ii.proveedor, ii.item, ii.unidadmedida, "+ 
                               " ii.cantidad, ii.cantidadcomprada, "+
                               " ii.montototalcomprado, ii.factura, ii.retencion, ii.tipocompra "+
                               " from "+
                               " tbcorpal_itempedidomateriaprimaeinsumos ii "+
                               " where "+
                               " ii.codpedido = "+codigoPedido;
            return ConecRes.consultaMySql(consulta);
        }

        internal bool update_CompradeInsumos(int codigoItem, float cantidadComprado, float montoComprado, string factura, string retencion, string tipocompra)
        {
            string consulta = "update tbcorpal_itempedidomateriaprimaeinsumos set "+
                               " tbcorpal_itempedidomateriaprimaeinsumos.cantidadcomprada = '"+cantidadComprado.ToString().Replace(',','.')+"', "+
                               " tbcorpal_itempedidomateriaprimaeinsumos.montototalcomprado = '"+montoComprado.ToString().Replace(',','.')+"', "+
                               " tbcorpal_itempedidomateriaprimaeinsumos.factura = '" + factura + "', " +
                               " tbcorpal_itempedidomateriaprimaeinsumos.retencion = '" + retencion + "', " +
                               " tbcorpal_itempedidomateriaprimaeinsumos.tipocompra = '" + tipocompra + "' " +
                               " where " +
                               " tbcorpal_itempedidomateriaprimaeinsumos.codigo = "+codigoItem;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal bool update_cerrarCompraMaterialInsumos(int codigoPedido, string estadoCompra, float montoTotalPedidoComprado, int codresponsableCompra, string ResponsableCompra)
        {
            string consulta = "update tbcorpal_pedidomateriaprimaeinsumos set "+
                               " tbcorpal_pedidomateriaprimaeinsumos.codpercompra = "+codresponsableCompra+", "+
                               " tbcorpal_pedidomateriaprimaeinsumos.personalcompra = '"+ResponsableCompra+"', "+
                               " tbcorpal_pedidomateriaprimaeinsumos.horacierre = current_time(), "+
                               " tbcorpal_pedidomateriaprimaeinsumos.fechacierre = current_date(), "+
                               " tbcorpal_pedidomateriaprimaeinsumos.montototalcomprado = '"+montoTotalPedidoComprado.ToString().Replace(',','.')+"', "+
                               " tbcorpal_pedidomateriaprimaeinsumos.estadosolicitud = '"+estadoCompra+"' "+
                               " where "+
                               " tbcorpal_pedidomateriaprimaeinsumos.codigo = "+codigoPedido;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal DataSet get_todosItemInsumosComprados(int codigoPedido)
        {
            string consulta = "select " +
                               " ii.codigo, ii.proveedor, ii.item, ii.unidadmedida, " +
                               " ii.cantidad, ii.cantidadcomprada, " +
                               " ii.cantidadrecibida, '0' as 'montorecibido', ii.tipocompra " +
                               " from " +
                               " tbcorpal_itempedidomateriaprimaeinsumos ii " +
                               " where " +
                               " ii.codpedido = " + codigoPedido;
            return ConecRes.consultaMySql(consulta);
        }

        internal bool update_RecibirInsumosMaterial(int codigoItem, float cantidadRecibido)
        {
            string consulta = "update tbcorpal_itempedidomateriaprimaeinsumos set " +
                                " tbcorpal_itempedidomateriaprimaeinsumos.cantidadrecibida = '" + cantidadRecibido.ToString().Replace(',', '.') + "' " +                                
                                " where " +
                                " tbcorpal_itempedidomateriaprimaeinsumos.codigo = " + codigoItem;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal bool update_cerrarMaterialInsumosRecibido(int codigoPedido, string estadoCompra, int codresponsableRecibido, string ResponsableRecibido)
        {
            string consulta = "update tbcorpal_pedidomateriaprimaeinsumos set " +
                               " tbcorpal_pedidomateriaprimaeinsumos.codpersonalrecibido = " + codresponsableRecibido + ", " +
                               " tbcorpal_pedidomateriaprimaeinsumos.personalrecibido = '" + ResponsableRecibido + "', " +
                               " tbcorpal_pedidomateriaprimaeinsumos.horacierre = current_time(), " +
                               " tbcorpal_pedidomateriaprimaeinsumos.fechacierre = current_date(), " +                               
                               " tbcorpal_pedidomateriaprimaeinsumos.estadosolicitud = '" + estadoCompra + "' " +
                               " where " +
                               " tbcorpal_pedidomateriaprimaeinsumos.codigo = " + codigoPedido;
            return ConecRes.ejecutarMySql(consulta);
        }

        internal DataSet get_DatosSolicitudMaterialeInsumos(int codigoSMI)
        {
            string consulta = "select " +
                               " pp.codigo,  date_format(pp.fechasolicitud,'%d/%m/%Y') as 'Fecha_Solicitud', " +
                               " date_format(pp.fechaesimadaentrega,'%d/%m/%Y') as 'Fecha_EstimadaEntrega', " +
                               " pp.personalsolicitud,  format(pp.montototal,2) as 'Total',  pp.estadosolicitud, " +
                               " pp.personalcompra "+
                               " from tbcorpal_pedidomateriaprimaeinsumos pp where " +
                               " pp.codigo =" + codigoSMI;
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_DatosSolicitudMaterialeInsumos_item(int codigoSMI)
        {
            throw new System.NotImplementedException();
        }
    }
}