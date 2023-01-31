using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DCorpal_Tienda
    {
        private conexionMySql conexion = new conexionMySql();
        public DCorpal_Tienda() { 
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
                             " from tbcorpal_tienda tt "+
                            "  where tt.tiendaname like '%"+nombreTiendas+"%'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal bool eliminarTienda(int codigo)
        {
            string consulta = "delete from tbcorpal_tienda where tbcorpal_tienda.codigo = " + codigo;            
            return conexion.ejecutarMySql(consulta);
        }

        internal bool guardarDatosTienda(string tiendaname, string tiendadir, string tiendatelefono, string tiendadepartamento, string tiendazona, string propietarioname, string propietarioci, string propietariodir, string propietariocelular, string propietarionit, string propietariocorreo, string facturar_a, string facturar_nit, string facturar_correo, string observacion, int codUserGra)
        {
            string consulta = "insert into tbcorpal_tienda(  fechagra,horagra, "+
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
            string consulta = "update tbcorpal_tienda set "+                     
                                 " tbcorpal_tienda.tiendaname = '"+tiendaname+"', "+ 
                                 " tbcorpal_tienda.tiendadir = '"+tiendadir+"', "+ 
                                 " tbcorpal_tienda.tiendatelefono = '"+tiendatelefono+"', "+
                                 " tbcorpal_tienda.tiendadepartamento = '"+tiendadepartamento+"', "+ 
                                 " tbcorpal_tienda.tiendazona = '"+tiendazona+"', "+ 
                                 " tbcorpal_tienda.propietarioname = '"+propietarioname+"', "+
                                 " tbcorpal_tienda.propietarioci = '"+propietarioci+"', "+ 
                                 " tbcorpal_tienda.propietariodir = '"+propietariodir+"', "+ 
                                 " tbcorpal_tienda.propietariocelular = '"+propietariocelular+"', "+                       
                                 " tbcorpal_tienda.propietarionit = '"+propietarionit+"', "+ 
                                 " tbcorpal_tienda.propietariocorreo = '"+propietariocorreo+"', "+ 
                                 " tbcorpal_tienda.facturar_a = '"+facturar_a+"', "+                 
                                 " tbcorpal_tienda.facturar_nit = '"+facturar_nit+"', "+  
                                 " tbcorpal_tienda.facturar_correo = '"+facturar_correo+"', "+
                                 " tbcorpal_tienda.observacion = '" + observacion + "', " +
                                 " tbcorpal_tienda.codrespgra = "+ codUserGra+
                                 " where "+
                                 " tbcorpal_tienda.codigo = "+codigo;
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
                            " from tbcorpal_tienda tt " +
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
                             " tbcorpal_tienda cc "+
                             " where cc.propietarioname like '%"+nombre+"%' ";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        internal DataSet get_tiendaNombre(string tiendanombre)
        {
            string consulta = "Select " +
                            " codigo, " +
                            " tiendaname,  tiendadir,  tiendatelefono, " +
                            " tiendadepartamento,  tiendazona,  propietarioname, " +
                            " propietarioci,  propietariodir,  propietariocelular, " +
                            " propietarionit,  propietariocorreo, facturar_a, " +
                            " facturar_nit,  facturar_correo, observacion " +
                            " from tbcorpal_tienda tt " +
                           "  where tt.tiendaname = '" + tiendanombre + "'";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }
    }
}