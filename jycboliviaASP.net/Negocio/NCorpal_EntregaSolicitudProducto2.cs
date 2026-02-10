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
        internal bool update_CierreAutSolicitudProd(int codSolicitud, int codPer, string personal)
        {
            return datos.update_CierreAutSolicitudProd(codSolicitud, codPer, personal);
        }

        internal DataSet get_EntregasProductoaCamion(int codigoCamion)
        {
            return datos.get_EntregasProductoaCamion(codigoCamion);
        }


        internal DataSet get_despachosdeCamiones(string fechadesde, string fechahasta, string estado, int codVehiculo)
        {
            return datos.get_despachosdeCamiones(fechadesde, fechahasta, estado, codVehiculo);
        }

        internal bool update_despachodeproductosCamiones(int codigo, string estado, int codresp)
        {
            return datos.update_despachodeproductosCamiones(codigo, estado, codresp);
        }

        internal DataSet get_DespachoProductoaCamion(int codigoDespacho)
        {
            return datos.get_DespachoProductoaCamion(codigoDespacho);
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

        public int POST_INSERTdespachoRetornoID(string detalle, int codvehiculo, int codrespinicio, int codconductor, string conductor)
        {
            return datos.POST_INSERTdespachoRetornoID(detalle, codvehiculo, codrespinicio, codconductor, conductor);
        }
        internal bool POST_INSERTdetalleDespacho(int coddespacho, int codpedido, int codprod, float cantidad, int codcli)
        {
            try
            {
                return datos.POST_INSERTdetalleDespacho(coddespacho, codpedido, codprod, cantidad, codcli);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en capa negocio (POST_INSERTdetalleDespacho):" + ex.Message);
                throw;
            }
        }

        /* POST DETALLE SOLICITUD PEDIDO */
        internal bool UPDATE_camposDetalleSolicitudPedido(int codigoSolicitud, int codigoProducto, float cantidadEntregado, string estadoProducto, float restarStock,
                                                            int coduser, int codVehiculo)
        {
            try
            {
                return datos.UPDATE_camposDetalleSolicitudPedido(codigoSolicitud, codigoProducto, cantidadEntregado, estadoProducto, restarStock,
                                                                    coduser, codVehiculo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado: " + ex.Message);
            }
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
        internal bool POST_rechazarSolCredito(int codResp, int codSol, string nroBoleta)
        {
            try
            {
                return datos.POST_rechazarSolCredito(codResp, codSol, nroBoleta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al rechazar la solicitud de credito. " + ex.Message);
            }
        }
        internal bool POST_RegistroAsignacionChoferAVehiculo(int codCar, int codChofer, int codUserGra, string userGra)
        {
            try
            {
                return datos.POST_RegistroAsignacionChoferAVehiculo(codCar, codChofer, codUserGra, userGra);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la asignacion de chofer. " + ex.Message);
            }
        }

        internal DataSet GET_obtener_UltConductorVehiculo(int codVehiculo)
        {
            try
            {
                return datos.GET_obtener_UltConductorVehiculo(codVehiculo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los datos del conductor. " + ex.Message);
            }
        }

        /*  REGISTRO RUTAS DESPACHO  */
        internal int post_RegistroRutaEntrega_despacho(int codCar, string car, int codChofer, string chofer)
        {
            try
            {
                return datos.post_RegistroRutaEntrega_despacho(codCar, car, codChofer, chofer);

            }
            catch (Exception ex)
            {
                throw new Exception("Error la registrar ruta entrega. " + ex.Message);
            }
        }

        internal bool post_RegistroRutaEntregaPuntos_despacho(int orden, int codRuta, int codCliente, string cliente,
                                            int codDespacho, string descripcion, string lat, string lng)
        {
            try
            {
                return datos.post_RegistroRutaEntregaPuntos_despacho(orden, codRuta, codCliente, cliente,
                                    codDespacho, descripcion, lat, lng);
            }
            catch (Exception ex)
            {
                throw new Exception("Error la registrar puntos de la ruta. " + ex.Message);
            }
        }

        internal DataSet GET_obtenerDatosClienteDespacho(int codDespacho)
        {
            try
            {
                return datos.GET_obtenerDatosClienteDespacho(codDespacho);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos del Cliente. " + ex.Message);
            }
        }

        internal DataSet GET_ReportSolicitudEntregaProducto(DateTime fechaIni, DateTime fechaFin, string vendedor, string cliente)
        {
            try
            {
                return datos.GET_ReportSolicitudEntregaProducto(fechaIni, fechaFin, vendedor, cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos de la solicitud. " + ex.Message);
            }
        }



    }
}