using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_SolicitudEntregaProducto
    {
        DCorpal_SolicitudEntregaProducto dsp = new DCorpal_SolicitudEntregaProducto();

        public NCorpal_SolicitudEntregaProducto() { }

        public DataSet get_mostrarProductos(string producto)
        {            
            return dsp.get_mostrarProductos(producto);
        }

        public int get_CodigoProductos(string producto)
        {
            DataSet tupla = dsp.get_CodigoProductos(producto);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                int codigo;
                int.TryParse(tupla.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            }
            else
                return -1;
        }

        internal bool set_guardarSolicitud(string nroboleta, string fechaentrega, string horaentrega, string personalsolicitud, int codpersolicitante, bool estado)
        {
            return dsp.set_guardarSolicitud( nroboleta,  fechaentrega,  horaentrega,  personalsolicitud,  codpersolicitante,  estado);
        }

        internal string get_siguentenumeroRecibo(int codUser)
        {
            DataSet dato = dsp.get_siguentenumeroRecibo(codUser);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return dato.Tables[0].Rows[0][0].ToString();
            }
            else
                return "Ninguno";
        }

        internal int getultimaSolicitudproductoInsertado(int codpersolicitante)
        {
            DataSet dato = dsp.getultimaSolicitudproductoInsertado( codpersolicitante);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return -1;
        }

        internal bool insertarDetalleSolicitudProducto(int ultimoinsertado, int codProducto, double cantidad, double preciocompra, double total, string Tipo, string Medida)
        {
           return dsp.insertarDetalleSolicitudProducto( ultimoinsertado,  codProducto,  cantidad,  preciocompra,  total,  Tipo,  Medida);
        }

        internal bool actualizarmontoTotal(int ultimoinsertado, double montoTotal)
        {
            return dsp.actualizarmontoTotal( ultimoinsertado,  montoTotal);
        }

        internal DataSet get_solicitudesRealizadasProductos(string nroSolicitud, string solicitante)
        {
            return dsp.get_solicitudesRealizadasProductos( nroSolicitud,  solicitante);
        }

        internal DataSet get_datosSolicitudProductos(int codigoSolicitud)
        {
            return dsp.get_datosSolicitudProductos( codigoSolicitud);
        }

        internal bool eliminarSolicitud(int codigoSolicitud)
        {
            return dsp.eliminarSolicitud( codigoSolicitud);
        }

        internal bool update_cantProductosEntregados(int codigoSolicitud, int codigoP, float cantEntregado)
        {
            return dsp.update_cantProductosEntregados(codigoSolicitud, codigoP, cantEntregado);
        }

        internal bool update_cerrarSolicitud(int codigoSolicitud, int codresponsable, string nombreResponsable)
        {
            return dsp.update_cerrarSolicitud( codigoSolicitud,  codresponsable,  nombreResponsable);
        }

        internal DataSet get_entregaSolicitudProductos(int codigoEntregaSolicitudProducto)
        {
            return dsp.get_entregaSolicitudProductos( codigoEntregaSolicitudProducto);
        }

        internal DataSet get_productosEngregaSolicitudProducto(int codigoEntregaSolicitudProducto)
        {
            return dsp.get_productosEngregaSolicitudProducto( codigoEntregaSolicitudProducto);
        }

        internal DataSet get_productosSolicitudProducto(int codigoSolicitudProducto)
        {
            return dsp.get_productosSolicitudProducto( codigoSolicitudProducto);
        }

        internal DataSet get_alldetalleProductoSolicitudEntregado(string fechadesde, string fechahasta, string personalsolicitud)
        {
            return dsp.get_alldetalleProductoSolicitudEntregado( fechadesde,  fechahasta,  personalsolicitud);
        }

        internal DataSet get_alldetalleProductoSolicitud_VS_Entregado(string fechadesde, string fechahasta)
        {
            return dsp.get_alldetalleProductoSolicitud_VS_Entregado( fechadesde,  fechahasta);
        }

        internal DataSet get_alldetalleProductoSolicitadosyEntregadosporpersona(string fechadesde, string fechahasta, string Responsable)
        {
            return dsp.get_alldetalleProductoSolicitadosyEntregadosporpersona(fechadesde, fechahasta, Responsable);
        }

      
    }
}