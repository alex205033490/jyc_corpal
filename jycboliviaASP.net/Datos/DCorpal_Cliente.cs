using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_Cliente
    {
        private conexionMySql conexion = new conexionMySql();
        public DCorpal_Cliente() { 
        }
        

        internal DataSet listarTiendas(string nombreTiendas)
        {
            string consulta = "Select "+
                             " codigo, "+                                      
                             " tiendaname,  tiendadir,  tiendatelefono, "+
                             " tiendadepartamento,  tiendazona,  propietarioname, "+
                             " propietarioci,  propietariodir,  propietariocelular, "+                       
                             " propietarionit,  propietariocorreo, facturar_a, "+                    
                             " facturar_nit,  facturar_correo, observacion "+     
                             " from tbcorpal_cliente tt "+
                            "  where tt.tiendaname like '%"+nombreTiendas+"%'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal bool eliminarTienda(int codigo)
        {
            string consulta = "delete from tbcorpal_cliente where tbcorpal_cliente.codigo = " + codigo;            
            return conexion.ejecutarMySql(consulta);
        }

        internal bool guardarDatosTienda(string tiendaname, string tiendadir, string tiendatelefono, string tiendadepartamento, string tiendazona, string propietarioname, string propietarioci, string propietariodir, string propietariocelular, string propietarionit, string propietariocorreo, string facturar_a, string facturar_nit, string facturar_correo, string observacion, int codUserGra)
        {
            string consulta = "insert into tbcorpal_cliente(  fechagra,horagra, "+
                               " tiendaname,  tiendadir,  tiendatelefono, "+
                               " tiendadepartamento,  tiendazona,  propietarioname, "+
                               " propietarioci,  propietariodir,  propietariocelular, "+                       
                               " propietarionit,  propietariocorreo, facturar_a, "+                 
                               " facturar_nit,  facturar_correo, observacion, codrespgra) "+  
                               " values( "+
                               " current_date(), current_time(), "+
                               " '"+tiendaname+"',  '"+tiendadir+"',  '"+tiendatelefono+"', "+
                               " '"+tiendadepartamento+"',  '"+tiendazona+"',  '"+propietarioname+"', "+
                               " '"+propietarioci+"',  '"+propietariodir+"',  '"+propietariocelular+"', "+                       
                               " '"+propietarionit+"',  '"+propietariocorreo+"', '"+facturar_a+"', "+
                               " '" + facturar_nit + "',  '" + facturar_correo + "', '" + observacion + "', " + codUserGra + ")";
            return conexion.ejecutarMySql(consulta);
        }

        internal bool updateDatosTienda(int codigo, string tiendaname, string tiendadir, string tiendatelefono, string tiendadepartamento, string tiendazona, string propietarioname, string propietarioci, string propietariodir, string propietariocelular, string propietarionit, string propietariocorreo, string facturar_a, string facturar_nit, string facturar_correo, string observacion, int codUserGra)
        {
            string consulta = "update tbcorpal_cliente set "+                     
                                 " tbcorpal_cliente.tiendaname = '"+tiendaname+"', "+ 
                                 " tbcorpal_cliente.tiendadir = '"+tiendadir+"', "+ 
                                 " tbcorpal_cliente.tiendatelefono = '"+tiendatelefono+"', "+
                                 " tbcorpal_cliente.tiendadepartamento = '"+tiendadepartamento+"', "+ 
                                 " tbcorpal_cliente.tiendazona = '"+tiendazona+"', "+ 
                                 " tbcorpal_cliente.propietarioname = '"+propietarioname+"', "+
                                 " tbcorpal_cliente.propietarioci = '"+propietarioci+"', "+ 
                                 " tbcorpal_cliente.propietariodir = '"+propietariodir+"', "+ 
                                 " tbcorpal_cliente.propietariocelular = '"+propietariocelular+"', "+                       
                                 " tbcorpal_cliente.propietarionit = '"+propietarionit+"', "+ 
                                 " tbcorpal_cliente.propietariocorreo = '"+propietariocorreo+"', "+ 
                                 " tbcorpal_cliente.facturar_a = '"+facturar_a+"', "+                 
                                 " tbcorpal_cliente.facturar_nit = '"+facturar_nit+"', "+  
                                 " tbcorpal_cliente.facturar_correo = '"+facturar_correo+"', "+
                                 " tbcorpal_cliente.observacion = '" + observacion + "', " +
                                 " tbcorpal_cliente.codrespgra = "+ codUserGra+
                                 " where "+
                                 " tbcorpal_cliente.codigo = "+codigo;
            return conexion.ejecutarMySql(consulta);
        }

        internal DataSet get_tienda(int codigo)
        {
            string consulta = "Select " +
                            " codigo, " +
                            " tiendaname,  tiendadir,  tiendatelefono, " +
                            " tiendadepartamento,  tiendazona,  propietarioname, " +
                            " propietarioci,  propietariodir,  propietariocelular, " +
                            " propietarionit,  propietariocorreo, facturar_a, " +
                            " facturar_nit,  facturar_correo, observacion " +
                            " from tbcorpal_cliente tt " +
                           "  where tt.codigo = '" + codigo + "'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet buscarPropietario(string nombre)
        {
            string consulta = "Select "+
                             " codigo, "+                      
                             " propietarioname, "+
                             " propietarioci, "+
                             " propietariodir, "+
                             " propietariocelular, "+                        
                             " propietarionit, "+
                             " propietariocorreo "+ 
                             " from "+
                             " tbcorpal_cliente cc "+
                             " where cc.propietarioname like '%"+nombre+"%' ";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_ClienteNombre(string tiendanombre)
        {
            string consulta = "Select " +
                            " codigo, " +
                            " tiendaname,  tiendadir,  tiendatelefono, " +
                            " tiendadepartamento,  tiendazona,  propietarioname, " +
                            " propietarioci,  propietariodir,  propietariocelular, " +
                            " propietarionit,  propietariocorreo, facturar_a, " +
                            " facturar_nit,  facturar_correo, observacion " +
                            " from tbcorpal_cliente tt " +
                           "  where tt.tiendaname like '%" + tiendanombre + "%'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_ClienteNombreEspecifico(string tiendanombre)
        {
            string consulta = "Select " +
                            " codigo, " +
                            " tiendaname,  tiendadir,  tiendatelefono, " +
                            " tiendadepartamento,  tiendazona,  propietarioname, " +
                            " propietarioci,  propietariodir,  propietariocelular, " +
                            " propietarionit,  propietariocorreo, facturar_a, " +
                            " facturar_nit,  facturar_correo, observacion " +
                            " from tbcorpal_cliente tt " +
                           "  where tt.tiendaname = '" + tiendanombre + "'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }
        internal DataSet get_ClienteCodCliente(int codigo)
        {
            string consulta = "Select " +
                            " codigo, " +
                            " tiendaname,  tiendadir,  tiendatelefono, " +
                            " tiendadepartamento,  tiendazona,  propietarioname, " +
                            " propietarioci,  propietariodir,  propietariocelular, " +
                            " propietarionit,  propietariocorreo, facturar_a, " +
                            " facturar_nit, observacion " +
                            " from tbcorpal_cliente tt " +
                           "  where tt.codigo = " + codigo + "";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_clienteUltimoIngresado(string cliente, string propietario, string razonSocial, string nit)
        {
            string consulta = "select max(cc.codigo) from tbcorpal_cliente cc  " +
                " where  cc.tiendaname = '"+cliente+"' and cc.propietarioname = '"+propietario+"' " +
                " and cc.facturar_a = '"+razonSocial+"' and cc.facturar_nit = '"+nit+"'";
            return conexion.consultaMySql(consulta);
        }

        internal bool updateDatosTiendaSolicitud(int codigCliente, string cliente, string propietario, string razonsocial,string nit, int codpersolicitante)
        {
            string consulta = "update tbcorpal_cliente set  " +
                 " tbcorpal_cliente.tiendaname = '" + cliente + "' , " +
                 " tbcorpal_cliente.propietarioname = '" + propietario + "' , " +
                 " tbcorpal_cliente.facturar_a = '" + razonsocial + "', " +
                 " tbcorpal_cliente.facturar_nit = '" + nit + "' " +
                 " where tbcorpal_cliente.codigo = " + codigCliente;
            return conexion.ejecutarMySql(consulta);    
        }
    }
}