using jycboliviaASP.net.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    public class NA_SolicitudEntregaProductoACamion
    {
        DCorpal_SolicitudEntregaProductoACamion datos = new DCorpal_SolicitudEntregaProductoACamion();

        internal DataSet get_SolicitudesRealizadasProductos(string nroSolicitud, string solicitante, string estadoSolicitante)
        {
            return datos.get_SolicitudesRealizadasProductos(nroSolicitud, solicitante, estadoSolicitante);
        }

        internal DataSet get_RegistrosSolicitudPedidos(string nroSolicitud, string solicitante, string estadoSolicitante)
        {
            return datos.get_RegistrosSolicitudPedidos(nroSolicitud, solicitante, estadoSolicitante);
        }
        internal DataSet get_RegistrosSolicitudPedidos2(string nroSolicitud, string solicitante, string estadoSolicitud)
        {
            return datos.get_RegistrosSolicitudPedidos2(nroSolicitud, solicitante, estadoSolicitud);
        }

        internal DataSet get_DetSolicitudesRealizadas(int codigoSolicitud)
        {
            return datos.get_detalleSolicitudProducto(codigoSolicitud);
        }

        internal DataSet get_showNroBoleta (string nroBoleta)
        {
            return datos.get_ShowNroBoleta(nroBoleta);
        }

        internal DataSet get_showPersonalSolicitante(string nombre)
        {
            return datos.get_ShowPersonalSolicitud(nombre);
        }

        internal DataSet get_ShowVehiculos()
        {
            return datos.get_ShowVehiculo();
        }

        internal DataSet get_detVehiculo(int codigo)
        {
            return datos.get_detVehiculo(codigo);
        }

        public bool UpdateADDVehiculoAPedido(int codVehiculo, int codUser, int codSolicitud, int codProducto)
        {
            return datos.update_ADDVehiculoAPedido(codVehiculo, codUser, codSolicitud, codProducto);
        }

        internal DataSet get_AsignacionProductoaCamion(int codigoCamion)
        {
            return datos.get_AsignacionProductoaCamion(codigoCamion);
        }
    }
}