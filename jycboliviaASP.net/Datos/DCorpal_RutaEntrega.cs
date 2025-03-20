using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
   public class DCorpal_RutaEntrega
    {
       private conexionMySql conexion = new conexionMySql();
       public DCorpal_RutaEntrega() { }

        public DataSet BuscarRutasEntrega(string nombreTienda, string PersonalAsignado)
        {
            string consulta = "select "+
                                 " codigo, "+
                                 " date_format(fechagra,'%d/%m/%Y') as 'FechaGra', "+
                                 " horagra, "+
                                 " date_format(fechacobro,'%d/%m/%Y') as 'Fecha_entrega', "+
                                 " horacobro as 'Hora_entrega', "+
                                 " tiendanombre, " +
                                 " detalle, "+                                 
                                 " personalasignado, "+
                                 " estadoEntrega "+
                                 " from "+
                                 " tbcorpal_rutaentrega cc "+
                                 " where cc.tiendanombre like '%"+nombreTienda+"%' and cc.personalasignado like '%"+PersonalAsignado+"%' ";
            return conexion.consultaMySql(consulta);
        }

        internal bool eliminarRutaEntrega(int codigo)
        {
            string consulta = "delete from tbcorpal_rutaentrega where tbcorpal_rutaentrega.codigo = "+codigo;
            return conexion.ejecutarMySql(consulta);
        }

        internal bool guardarDatosRutasEntrega(string fechacobro, string horacobro, string detalle, string tiendanombre, int codtienda, int coduserasignado, string personalasignado, int coduserinicio, string estadoEntrega)
        {
            string consulta = "insert into tbcorpal_rutaentrega( "+
                             " fechagra,  horagra,  fechacobro,  horacobro, "+
                             " detalle,  tiendanombre,  codtienda, "+
                             " coduserasignado,  personalasignado, "+
                             " coduserinicio,estadoEntrega,  estado,  entregacancelada) " +
                             " values(  current_date(),  current_time(),  "+fechacobro+",  '"+horacobro+"', "+
                             " '"+detalle+"',  '"+tiendanombre+"',  "+codtienda+", "+
                             coduserasignado+",  '"+personalasignado+"', "+
                             coduserinicio + ",'" + estadoEntrega + "',  1,  false )";
            return conexion.ejecutarMySql(consulta);
        }

        internal bool updateDatosRutasEntrega(int codigo, string fechacobro, string horacobro, string detalle, string tiendanombre, int codtienda, int coduserasignado, string personalasignado, int coduserinicio, string estadoEntrega)
        {
            string consulta = "update tbcorpal_rutaentrega set "+
                             " tbcorpal_rutaentrega.fechacobro =   "+fechacobro+", "+
                             " tbcorpal_rutaentrega.horacobro = '"+horacobro+"', "+
                             " tbcorpal_rutaentrega.detalle = '"+detalle+"', "+
                             " tbcorpal_rutaentrega.tiendanombre = '"+tiendanombre+"', "+
                             " tbcorpal_rutaentrega.codtienda = "+codtienda+", "+
                             " tbcorpal_rutaentrega.coduserasignado = "+coduserasignado+", "+
                             " tbcorpal_rutaentrega.personalasignado = '"+personalasignado+"', "+
                             " tbcorpal_rutaentrega.coduserinicio = "+coduserinicio+", "+
                             " tbcorpal_rutaentrega.estadoEntrega = '"+estadoEntrega+"' "+
                             " where "+
                             " tbcorpal_rutaentrega.codigo ="+codigo;
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_RutaEntrega(int codigo)
        {
            string consulta = "select "+
                                 " codigo, "+
                                 " date_format(fechagra,'%d/%m/%Y') as 'FechaGra', "+
                                 " horagra, "+
                                 " date_format(fechacobro,'%d/%m/%Y') as 'Fecha_Cobro', "+
                                 " horacobro, "+
                                 " detalle, "+
                                 " codtienda, "+
                                 " tiendanombre, "+
                                 " coduserasignado, "+
                                 " personalasignado, "+
                                 " estadoEntrega "+
                                 " from "+
                                 " tbcorpal_rutaentrega cc "+
                                 " where cc.codigo = "+codigo;
            return conexion.consultaMySql(consulta);
        }

        internal DataSet get_UltimaRutaIngresada(string tiendanombre)
        {
            string consulta = "select "+
                             " codigo, "+
                             " date_format(fechagra,'%d/%m/%Y') as 'FechaGra', "+
                             " horagra, "+
                             " date_format(fechacobro,'%d/%m/%Y') as 'Fecha_Cobro', "+
                             " horacobro, "+
                             " detalle, "+
                             " codtienda, "+
                             " tiendanombre, "+
                             " coduserasignado, "+
                             " personalasignado, "+
                             " estadoEntrega "+
                             " from "+
                             " tbcorpal_rutaentrega cc "+
                             " where cc.tiendanombre = '"+tiendanombre+"' "+
                             " order by cc.codigo desc "+
                             " limit 1";
            return conexion.consultaMySql(consulta);
        }

        internal bool insertarProductos(int codigoRutaIngresada, string Producto, double Cantidad)
        {
            string consulta = "insert into tbcorpal_productosentrega( "+
                                 " fechagra,  horagra,  producto,  cantidad, codrutaentrega) "+
                                 " values(current_date(),  current_time(), "+ 
                                 " '"+Producto+"',  '"+Cantidad.ToString().Replace(',','.')+"',"+codigoRutaIngresada+" )";
            return conexion.ejecutarMySql(consulta);
        }

        internal bool eliminarProductosRutaEntreta(int codigo)
        {
            string consulta = "delete from tbcorpal_productosentrega where tbcorpal_productosentrega.codrutaentrega =" + codigo;
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_productosAdicionados(int codigoRuta)
        {
            string consulta = "select cc.producto, cc.cantidad from tbcorpal_productosentrega cc where "+
                               " cc.codrutaentrega = "+codigoRuta;
            return conexion.consultaMySql(consulta);
        }
    }
}
