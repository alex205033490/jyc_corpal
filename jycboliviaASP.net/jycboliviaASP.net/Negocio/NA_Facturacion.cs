using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Facturacion
    {
        DA_facturacion Dfacturacion = new DA_facturacion();

        public NA_Facturacion() { }

        public bool insertFacturacion(string fecha, string nrofactura, string nroAutorizacion, string nombrefactura, string nitci, string montototal, string codigocontrol, string fechalimite, string detalle)
        {
            return Dfacturacion.insertFacturacion(fecha, nrofactura, nroAutorizacion, nombrefactura, nitci, montototal, codigocontrol, fechalimite, detalle);
        }

        public bool modificar(int codigo, string fecha, string nrofactura, string nroAutorizacion, string nombrefactura, string nitci, string montototal, string codigocontrol, string fechalimite, string detalle)
        {
            return Dfacturacion.modificar(codigo, fecha, nrofactura, nroAutorizacion, nombrefactura, nitci, montototal, codigocontrol, fechalimite, detalle);
        }

        public bool eliminar(int codigo) {
            return Dfacturacion.delete(codigo);
        }

        public DataSet mostrarfacturas(string nrofactura)
        {
            string consulta = "select "+
                               " fa.codigo, "+
                               " date_format(fa.fecha,'%d/%m/%Y') as 'FechaFactura',  "+
                               " fa.nrofactura, "+
                               " fa.nroAutorizacion, "+
                               " fa.nombrefactura, "+
                               " fa.nitci, "+
                               " fa.montototal, "+
                               " fa.codigocontrol, "+
                               " date_format(fa.fechalimite,'%d/%m/%Y') as 'FechaLimite', "+
                               " fa.detalle  "+
                               " from tb_facturacion fa "+
                               " where fa.nrofactura like '%"+nrofactura+"%'";
                               
            return Dfacturacion.getDatos(consulta);
        }

    }
}