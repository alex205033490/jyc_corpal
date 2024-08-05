using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_RutaCobrador
    {
        private conexionMySql sql = new conexionMySql();
        public DA_RutaCobrador() { }

        public bool insertarRutaCobrador(int codEquipo, string edificio, string exbo, int codcobrador, int codcliente, string cobrador, string fechacobro, string horacobro, string detalle, float montocobrar,string estadoCobro,int coduserInicio)
        {
            string codEquipo_aux = "NULL";
            string codcliente_aux = "NULL";
            string codcobrador_aux = "NULL";
            if (codEquipo > 0) {
                codEquipo_aux = codEquipo.ToString();
            }

            if (codcliente > 0)
            {
                codcliente_aux = codcliente.ToString();
            }

            if(codcobrador > 0){
                codcobrador_aux = codcobrador.ToString();
            }


            string consulta = "insert into tb_rutacobrador( "+
                               " tb_rutacobrador.codcliente, tb_rutacobrador.codusercobrador, "+
                               " tb_rutacobrador.codequipo, tb_rutacobrador.edificio, "+
                               " tb_rutacobrador.fechagra, tb_rutacobrador.horagra, "+
                               " tb_rutacobrador.fechacobro, tb_rutacobrador.horacobro, "+ 
                               " tb_rutacobrador.exbo, tb_rutacobrador.detalle, "+
                               " tb_rutacobrador.coduserinicio, tb_rutacobrador.estadoCobro, "+
                               " tb_rutacobrador.estado, tb_rutacobrador.nrorecibo, "+
                               " tb_rutacobrador.montocobrar  " +
                               " ) values( "+
                               codcliente_aux + " , " + codcobrador_aux + ", " +
                               codEquipo_aux+" , '"+edificio+"', "+
                               " current_date(), current_time(), "+
                               fechacobro+" , '"+horacobro+"',  "+
                               " '"+exbo+"', '"+detalle+"',  "+
                               coduserInicio + ", '" + estadoCobro + "', " +
                               " 1, concat( year(current_date()), month(current_date()), (select IFNULL(max(vv.codigo),0)+1 from tb_rutacobrador vv)  ), " +
                               " '"+montocobrar.ToString().Replace(',','.')+"' )";
            return sql.ejecutarMySql(consulta);

        }

        public bool eliminar(int codRutaCobro) {
            string consulta = "delete from tb_rutacobrador where tb_rutacobrador.codigo =" + codRutaCobro;
            return sql.ejecutarMySql(consulta);
        }

        public bool modificarRutaCobro(int codRutaCobro, int codEquipo, string edificio, string exbo, int codcobrador, string cobrador, string fechacobro, string horacobro, float montocobrar) {
            return false;
        }

        internal DataSet getrutasAsignadas(string edificio, string cobradornombre)
        {
            string consulta = "select " +
                               " rr.codigo, " +
                               " rr.nrorecibo, " +
                               " rr.edificio, " +
                               " rr.exbo, " +
                               " rr.detalle, " +
                               " ep.nombre as 'Cliente', " +
                               " date_format(rr.fechacobro,'%d/%m/%Y') as 'fecha_Cobro', " +
                               " rr.horacobro, " +
                               " rr.estadoCobro, " +
                               " rr.montocobrar, " +
                               " res.nombre as 'Cobrador' " +
                               " from tb_rutacobrador rr " +
                               " LEFT JOIN tb_encargado_pago ep ON rr.codcliente = ep.codigo, "+
                               " tb_responsable res" +
                               " where " +
                               " rr.codusercobrador = res.codigo ";
                               
                               if(!edificio.Equals("")){
                                   consulta = consulta + " and rr.edificio like '%" + edificio + "%' "; 
                               }

                               if (!cobradornombre.Equals(""))
                               {
                                   consulta = consulta + " and res.nombre like '%" + cobradornombre + "%'";
                               }
                               
            return sql.consultaMySql(consulta);
        }
    }
}