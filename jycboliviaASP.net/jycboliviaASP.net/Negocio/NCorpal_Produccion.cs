using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    
    public class NCorpal_Produccion    
    {
        DCorpal_Produccion dproduccion = new DCorpal_Produccion();
        internal bool insertarEntregaProduccion(string nroorden, string turno, int codusuario, string respEntrega, float cantcajas, float unidadsuelta, float kgrdesperdicio, float kgrparamix, string detalleentrega, int codProdNax, string productoNax, int codresprecepcion, string resp_recepcion, int cod_respgra, float kgrdesperdicio_conaceite, float kgrdesperdicio_sinaceite)
        {
            return dproduccion.insertarEntregaProduccion( nroorden,  turno,  codusuario,  respEntrega,  cantcajas,  unidadsuelta,  kgrdesperdicio,  kgrparamix,  detalleentrega,  codProdNax,  productoNax,  codresprecepcion,  resp_recepcion,  cod_respgra,  kgrdesperdicio_conaceite,  kgrdesperdicio_sinaceite);
        }

        internal bool modificarEntregaProduccion(int codigo, string nroorden, string turno, int codusuario, string respEntrega, float cantcajas, float unidadsuelta, float kgrdesperdicio, float kgrparamix, string detalleentrega, int codProdNax, string productoNax, int codresprecepcion, string resp_recepcion, int cod_respgra)
        {
            return dproduccion.modificarEntregaProduccion( codigo,  nroorden,  turno,  codusuario,  respEntrega,  cantcajas,  unidadsuelta,  kgrdesperdicio,  kgrparamix,  detalleentrega,  codProdNax,  productoNax ,  codresprecepcion,  resp_recepcion,  cod_respgra);
        }

        internal bool eliminarEntregaProduccion(int codigo)
        {
           return dproduccion.eliminarEntregaProduccion(codigo);
        }

        internal DataSet mostrarEmpregasProduccion(string turno, string respEntrega)
        {
            return dproduccion.mostrarEmpregasProduccion( turno,  respEntrega);
        }

        internal DataSet get_entregasProduccion(string fechadesde, string fechahasta, string Responsable, string producto)
        {
           return dproduccion.get_entregasProduccion( fechadesde,  fechahasta, Responsable,  producto);
        }

        internal int get_ultimoInsertadoEntregaProduccion(string respEntrega)
        {
            DataSet ultimo = dproduccion.get_ultimoInsertadoEntregaProduccion( respEntrega);
            if (ultimo.Tables[0].Rows.Count > 0)
            {
                int codigo;
                int.TryParse(ultimo.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            }
            else
                return -1;
        }

        internal DataSet get_DatosEntregaProduccion(int codigoEntregaProduccion)
        {
            DataSet tuplas = dproduccion.get_DatosEntregaProduccion(codigoEntregaProduccion);
            return tuplas;
        }

        internal bool set_objetivoProduccion(string fechalimite, int codprod, string producto, float cantidadprod,
                                           string medida, string detalle, int codusergra, string respgra)
        {
            return dproduccion.set_objetivoProduccion( fechalimite,  codprod,  producto,  cantidadprod, medida,  detalle,  codusergra,  respgra);
        }

        internal DataSet get_objetivosDeProduccion(string fechalimite, string producto)
        {
            return dproduccion.get_objetivosDeProduccion( fechalimite,  producto);
        }

        internal bool update_objetivoProduccion(int codigo, string fechalimite, int codprod, string producto, float cantidadprod, string medida, string detalle, int codusergra, string respgra)
        {
            return dproduccion.update_objetivoProduccion(codigo, fechalimite, codprod, producto, cantidadprod, medida, detalle, codusergra, respgra);
        }

        internal bool delete_objetivoProduccion(int codigo, int codUser)
        {
            return dproduccion.delete_objetivoProduccion( codigo,  codUser);
        }

        internal DataSet get_devolucionProductos(string vendedor, string producto)
        {
            return dproduccion.get_devolucionProductos( vendedor,  producto);
        }

        internal bool insertarDevolucionProduccion(string fechadevolucion, string vendedor, int codvendedor, string producto, int codproducto, float cantidad, string almacenerorecibe, string motivodevolucion, string seenviaa, string observacionesdevolucion, string medida)
        {
            return dproduccion.insertarDevolucionProduccion( fechadevolucion, vendedor, codvendedor, producto, codproducto, cantidad, almacenerorecibe, motivodevolucion, seenviaa, observacionesdevolucion, medida);
        }

        internal bool modificarDevolucionProduccion(int codigoD, string fechadevolucion, string vendedor, int codvendedor, string producto, int codproducto, float cantidad, string almacenerorecibe, string motivodevolucion, string seenviaa, string observacionesdevolucion, string medida)
        {
            return dproduccion.modificarDevolucionProduccion(codigoD, fechadevolucion, vendedor, codvendedor, producto, codproducto, cantidad, almacenerorecibe, motivodevolucion, seenviaa, observacionesdevolucion, medida);
        }

        internal bool eliminarDevolucionProduccion(int codigoD)
        {
            return dproduccion.eliminarDevolucionProduccion(codigoD);
        }

        internal DataSet get_devolucionProductosNoAprovados(string fechaDevolucion, string Producto)
        {
           return dproduccion.get_devolucionProductosNoAprovados( fechaDevolucion,  Producto);
        }

        internal bool set_AutorizadoDevolucionProducto(int codigoDevolucion, bool marcado, int codUserAutorizado, string nombreAutorizacion)
        {
            return dproduccion.set_objetivoProduccion( codigoDevolucion,  marcado,  codUserAutorizado,  nombreAutorizacion);
        }

        internal DataSet get_objetivosDeProduccionMensual(int Mes, int anio, string producto)
        {
           return dproduccion.get_objetivosDeProduccionMensual( Mes,  anio,  producto);
        }

        internal bool update_objetivoProduccionMensual(int codigo, int codMes, string mesTexto, int anio, int codprod, string producto, float cantidadprod, string medida, string detalle, int codusergra, string respgra)
        { 
            return dproduccion.update_objetivoProduccionMensual( codigo,  codMes,  mesTexto,  anio,  codprod,  producto,  cantidadprod,  medida,  detalle,  codusergra,  respgra);
        }

        internal bool set_objetivoProduccionMensual(int codMes, string mesTexto, int anio, int codprod, string producto, float cantidadprod, string medida, string detalle, int codusergra, string respgra)
        {
           return dproduccion.set_objetivoProduccionMensual( codMes,  mesTexto,  anio,  codprod,  producto,  cantidadprod,  medida,  detalle,  codusergra,  respgra);
        }

        internal bool delete_objetivoProduccionMensual(int codigo, int codUser)
        {
            return dproduccion.delete_objetivoProduccionMensual( codigo,  codUser);
        }
    }
}