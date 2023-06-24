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
        internal bool insertarEntregaProduccion(string nroorden, string turno, int codusuario, string respEntrega, float cantcajas, float unidadsuelta, float kgrdesperdicio, float kgrparamix, string detalleentrega, int codProdNax, string productoNax, int codresprecepcion, string resp_recepcion, int cod_respgra)
        {
            return dproduccion.insertarEntregaProduccion( nroorden,  turno,  codusuario,  respEntrega,  cantcajas,  unidadsuelta,  kgrdesperdicio,  kgrparamix,  detalleentrega,  codProdNax,  productoNax,  codresprecepcion,  resp_recepcion,  cod_respgra);
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
        }
    }
}