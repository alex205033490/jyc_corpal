using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{     
    public class DA_facturacion
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_facturacion() { }

        public bool insertFacturacion(string fecha, string nrofactura, string nroAutorizacion, string nombrefactura, string nitci, string montototal, string codigocontrol, string fechalimite, string detalle)
        {
            string consulta = "insert into tb_facturacion(fecha,nrofactura,nroAutorizacion,nombrefactura, "+
                               " nitci,montototal,codigocontrol,fechalimite,detalle) "+
                               " values "+
                               " ("+fecha+",'"+nrofactura+"','"+nroAutorizacion+"','"+nombrefactura+"', "+
                               " '"+nitci+"','"+montototal+"','"+codigocontrol+"',"+fechalimite+",'"+detalle+"')";
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool modificar(int codigo,string fecha, string nrofactura, string nroAutorizacion, string nombrefactura, string nitci, string montototal, string codigocontrol, string fechalimite, string detalle)
        {
            string consulta = "update tb_facturacion set tb_facturacion.fecha = "+fecha+", "+
                               " tb_facturacion.nrofactura = '"+nrofactura+"', "+
                               " tb_facturacion.nroAutorizacion = '"+nroAutorizacion+"', "+
                               " tb_facturacion.nombrefactura = '"+nombrefactura+"', "+
                               " tb_facturacion.nitci = '"+nitci+"', "+
                               " tb_facturacion.montototal = '"+montototal.Replace(',','.')+"', "+
                               " tb_facturacion.codigocontrol = '"+codigocontrol+"', "+
                               " tb_facturacion.fechalimite = "+fechalimite+", "+
                               " tb_facturacion.detalle = '"+detalle+"' "+
                               " where tb_facturacion.codigo = "+codigo;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool delete(int codigo) {
            string consulta = "delete from tb_facturacion where tb_facturacion.codigo = "+codigo;
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }
    }
}