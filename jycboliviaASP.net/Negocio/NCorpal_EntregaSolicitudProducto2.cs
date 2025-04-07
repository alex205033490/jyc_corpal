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

        internal bool update_cantProductosEntregados(int codigoSolicitud, int codigoProducto, float cantidadEntregado, float restarStock, int coduser)
        {
            return datos.update_cantProductosEntregados(codigoSolicitud, codigoProducto, cantidadEntregado, restarStock, coduser);
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
    }
}