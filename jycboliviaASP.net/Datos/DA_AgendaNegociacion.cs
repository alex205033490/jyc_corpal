using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DA_AgendaNegociacion
    {
        private conexionMySql cnx = new conexionMySql();
        public DA_AgendaNegociacion() { }

        public bool insertarAgenda(string fechaasignacion, string horaasignacion, string detalle, string personalasignado, string estado, int codproy, string edificio,string horaexpiracion, string fechaexpiracion) {
            string codigoProyecto = "null";
            if(codproy > 0){
                codigoProyecto = Convert.ToString(codproy);
            }
            string consulta = "insert into tb_agendanegociacion "+
                               " (tb_agendanegociacion.fecha,tb_agendanegociacion.hora, "+
                               " tb_agendanegociacion.fechaasignacion, tb_agendanegociacion.horaasignacion, "+
                               " tb_agendanegociacion.detalle,tb_agendanegociacion.personalasignado, "+
                               " tb_agendanegociacion.estado,tb_agendanegociacion.codproy,"+
                               "tb_agendanegociacion.horaexpiracion,tb_agendanegociacion.fechaexpiracion,"+
                               "tb_agendanegociacion.edificio ) " +
                               " values "+
                               " (now(), now(), "+
                               fechaasignacion+", '"+horaasignacion+"', "+
                               " '"+detalle+"', '"+personalasignado+"', "+
                               " '"+estado+"', "+codigoProyecto+","+
                               "'"+horaexpiracion+"',"+fechaexpiracion+", '"+edificio+"')";
            return cnx.ejecutarMySql(consulta); 
        }

        public bool updateAgenda(int codigoAgenda,string fechaasignacion, string horaasignacion, string detalle, string personalasignado, string estado, int codproy, string edificio, string horaexpiracion, string fechaexpiracion, string detalleCierre)
        {
            string consulta = "update tb_agendanegociacion set "+
                               " tb_agendanegociacion.fechaasignacion = "+fechaasignacion+", "+ 
                               " tb_agendanegociacion.horaasignacion = '"+horaasignacion+"',"+
                               " tb_agendanegociacion.detalle = '"+detalle+"', "+
                               " tb_agendanegociacion.personalasignado = '"+personalasignado+"', "+
                               " tb_agendanegociacion.estado = '"+estado+"', "+
                               " tb_agendanegociacion.edificio = '"+edificio+"', "+
                               " tb_agendanegociacion.horaexpiracion = '"+horaexpiracion+"', "+
                               " tb_agendanegociacion.fechaexpiracion = "+fechaexpiracion+", "+
                               " tb_agendanegociacion.observacioncierre = '"+detalleCierre+"', "+
                               " tb_agendanegociacion.codproy =  "+codproy+
                               " where "+
                               " tb_agendanegociacion.codigo = "+codigoAgenda;
            return cnx.ejecutarMySql(consulta);
        }

        public bool eliminarAgenda(int codigoAgenda)
        {
            string consulta = "delete from tb_agendanegociacion where tb_agendanegociacion.codigo ="+codigoAgenda;
            return cnx.ejecutarMySql(consulta);
        }

        public DataSet getAgendaporCodigo(int codigoAgenda)
        {
            string consulta = "select " +
                               " ane.codigo,  " +
                               " DATE_FORMAT(ane.fechaasignacion,'%d/%m/%Y') as 'Fecha_Asignacion', " +
                               " ane.horaasignacion as 'HoraAsignacion', " +
                               " ane.edificio, " +
                               " t1.DireccionEdificio, " +
                               " t1.EncagadoPago, " +
                               " t1.direccion, " +
                               " t1.telefono, " +
                               " t1.celular, " +
                               " t1.email, " +
                               " ane.detalle as 'Objetivo', " +
                               " ane.personalasignado as 'Persona_Asignada', " +
                               " ane.estado, " +
                               " ane.observacioncierre as 'observacion_Cierre', " +
                               " ane.horaexpiracion, " +
                               " DATE_FORMAT(ane.fechaexpiracion,'%d/%m/%Y') as 'Fecha_Expiracion' " +
                               " FROM " +
                               " tb_agendanegociacion ane " +
                               " LEFT JOIN " +
                               " ( " +
                               " select proy.codigo, proy.nombre as 'edificio', proy.direccion as 'DireccionEdificio', " +
                               " epago.nombre as 'EncagadoPago', epago.direccion ,epago.telefono, epago.celular, " +
                               " epago.email  " +
                               " from  " +
                               " tb_proyecto proy " +
                               " left join tb_encargado_pago epago on (proy.codEncargado = epago.codigo) " +
                               " ) AS t1 " +
                               " ON (ane.codproy = t1.codigo) " +
                               " where " +
                               " ane.codigo = " + codigoAgenda; 
            return cnx.consultaMySql(consulta);
        }

        public DataSet mostrarAgendaNegociacion(string personal, string edificio, string estado, string fechaDesde, string fechahasta) {
            string consulta = "select " +
                               " ane.codigo,  " +
                               " DATE_FORMAT(ane.fechaasignacion,'%d/%m/%Y') as 'Fecha_Asignacion', " +
                               " ane.horaasignacion as 'HoraAsignacion', " +
                               " ane.edificio, " +
                               " t1.DireccionEdificio, " +
                               " t1.EncagadoPago, " +
                               " t1.direccion, " +
                               " t1.telefono, " +
                               " t1.celular, " +
                               " t1.email, " +
                               " ane.detalle as 'Objetivo', " +
                               " ane.personalasignado as 'Persona_Asignada', " +
                               " ane.estado, " +
                               " ane.observacioncierre as 'observacion_Cierre', "+
                               " ane.horaexpiracion, "+
                               " DATE_FORMAT(ane.fechaexpiracion,'%d/%m/%Y') as 'Fecha_Expiracion' " +
                               " FROM " +
                               " tb_agendanegociacion ane " +
                               " LEFT JOIN " +
                               " ( " +
                               " select proy.codigo, proy.nombre as 'edificio', proy.direccion as 'DireccionEdificio', " +
                               " epago.nombre as 'EncagadoPago', epago.direccion ,epago.telefono, epago.celular, " +
                               " epago.email  " +
                               " from  " +
                               " tb_proyecto proy " +
                               " left join tb_encargado_pago epago on (proy.codEncargado = epago.codigo) " +
                               " ) AS t1 " +
                               " ON (ane.codproy = t1.codigo) " +
                               " where " +
                               " ane.estado = '" + estado + "' ";
            if(!personal.Equals("")){
            consulta = consulta +" and ane.personalasignado like '%" + personal + "%' " ; 
            }
                               
            if(!edificio.Equals("")){
                consulta = consulta + " and ane.edificio like '%" + edificio + "%' ";
            }
                               
            if(!fechaDesde.Equals("null") && !fechahasta.Equals("null")){
            consulta = consulta+ "and ane.fecha between " + fechaDesde + " and " + fechahasta ;
            }
            consulta = consulta + " order by ane.codigo desc ";
            return cnx.consultaMySql(consulta);        
        }
    }
}