using jycboliviaASP.net.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_EntregaSolicitudProducto2
    {
        DCorpal_EntregaSolicitudProducto2 datos = new DCorpal_EntregaSolicitudProducto2();

        internal DataSet get_VWRegistrosEntregaSolicitudProductos(string estadoSolicitud)
        {
            return datos.get_VWRegistrosEntregaSolicitudProductos(estadoSolicitud);
        }

        internal DataSet get_VWRegistrosEntregaSolicitudProductoXCamion(string estadoSolicitud, int codVehiculo)
        {
            return datos.get_VWRegistrosEntregaSolicitudProductoXCamion(estadoSolicitud, codVehiculo);
        } 

        /*internal DataSet get_detSolicitudProducto(int codSolicitud, int codProducto)
        {
            return datos.get_detSolicitudProductos(codSolicitud, codProducto);
        }*/

        internal DataSet get_showCarDD()
        {
            return datos.get_listVehiculosDRegistro();
        }

        internal float get_Stock(int codProducto, string tipoSolicitud)
        {
            DataSet dato = datos.get_Stock(codProducto);
            if (dato.Tables[0].Rows.Count > 0)
            {
                float stock;
                if (tipoSolicitud.Equals("ITEM PACK FERIAL"))
                {
                    float.TryParse(dato.Tables[0].Rows[0][6].ToString(), out stock);
                }
                else
                {
                    float.TryParse(dato.Tables[0].Rows[0][5].ToString(), out stock);
                }
                return stock;
            }
            else
                return 0;
        }

        
        internal bool update_RetirarSolicitud(List<int> codSolicitud, List<int> codProducto)
        {
            return datos.update_RetirarSolicitud(codSolicitud, codProducto);
        }
        internal bool update_CierreAutSolicitudProd(int codSolicitud, int codPer,string personal)
        {
            return datos.update_CierreAutSolicitudProd(codSolicitud, codPer, personal);
        }

        internal DataSet get_EntregasProductoaCamion(int codigoCamion)
        {
            return datos.get_EntregasProductoaCamion(codigoCamion);
        }


        internal DataSet get_despachosdeCamiones(string fechadesde, string fechahasta, string estado, int codVehiculo)
        {
            return datos.get_despachosdeCamiones( fechadesde,  fechahasta,  estado, codVehiculo);
        }

        internal bool update_despachodeproductosCamiones(int codigo, string estado, int codresp)
        {
            return datos.update_despachodeproductosCamiones( codigo,  estado, codresp);
        }

        internal DataSet get_DespachoProductoaCamion(int codigoDespacho)
        {
            return datos.get_DespachoProductoaCamion( codigoDespacho);
        }

        internal DataSet get_DespachoBoletasProdEntrega(int codigoDespacho)
        {
            return datos.get_DespachoBoletasProdEntrega(codigoDespacho);
        }

        internal DataSet get_showVehiculosDD()
        {
            return datos.get_showVehiculoDD();
        }
        internal DataSet get_detVehiculoGV(int codigo)
        {
            return datos.GET_detalleVehiculoGV(codigo);
        }
        public bool UpdateADDVehiculoPedido(int codVehiculo, int codUser, int codSolicitud, int codProducto)
        {
            return datos.UPDATE_ADDvehiculoAPedido(codVehiculo, codUser, codSolicitud, codProducto);

        }
        internal DataSet get_obtenerCodigoProducto(string nombre)
        {
            return datos.GET_obtenerCodProducto(nombre);
        }
        /*  DESPACHO - DETALLE DESPACHO*/

        public int POST_INSERTdespachoRetornoID(string detalle, int codvehiculo, int codrespinicio)
        {
            return datos.POST_INSERTdespachoRetornoID(detalle, codvehiculo, codrespinicio);
        }
        internal bool POST_INSERTdetalleDespacho(int coddespacho, int codpedido, int codprod, float cantidad)
        {
            try
            {
                return datos.POST_INSERTdetalleDespacho(coddespacho, codpedido, codprod, cantidad);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error en capa negocio (POST_INSERTdetalleDespacho):" + ex.Message);
                throw;
            }
        }

        /* POST DETALLE SOLICITUD PEDIDO */
        internal bool UPDATE_camposDetalleSolicitudPedido(int codigoSolicitud, int codigoProducto, float cantidadEntregado, string estadoProducto, float restarStock, int coduser, int codVehiculo)
        {
            return datos.UPDATE_camposDetalleSolicitudPedido(codigoSolicitud, codigoProducto, cantidadEntregado, estadoProducto, restarStock, coduser, codVehiculo);
        }

        /* SOLICITUDES PEDIDOS A CREDITO */
        internal DataSet get_listaPedidosACredito()
        {
            return datos.get_listaPedidosACredito();

        }
        internal DataSet get_listDetallePedidoaCredito(int codigo)
        {
            return datos.get_listDetallePedidoaCredito(codigo);
        }

        internal bool POST_aprobacionSolCredito(int codResp, int codSol, string nroBoleta)
        {
            return datos.POST_aprobacionSolCredito(codResp, codSol, nroBoleta);
        }

        internal int ObtenerCodVendedor_EntregaSolProductos(int cod)
        {
            return datos.ObtenerCodVendedor_EntregaSolProductos(cod);
        }

        internal int Obtener_codMetodoPagoSolicitud(int cod)
        {
            return datos.Obtener_codMetodoPagoSolicitud(cod);
        }
    }
}