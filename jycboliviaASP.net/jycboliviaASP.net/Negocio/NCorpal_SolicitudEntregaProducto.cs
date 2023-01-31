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
    }
}