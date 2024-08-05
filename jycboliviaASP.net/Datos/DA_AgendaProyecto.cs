using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DA_AgendaProyecto
    {
        private conexionMySql cnx = new conexionMySql();
        public DA_AgendaProyecto() { }

        public bool insertarAgendaProyecto(int codRespInicio,string fechaasignacion, string horaasignacion, string personalasignado, int codResponsableAsignado, string detalle,  string estado, int codproy, string edificio,string horaexpiracion, string fechaexpiracion) {
            string codigoProyecto = "null";
            if(codproy > 0){
                codigoProyecto = Convert.ToString(codproy);
            }
            string consulta = "insert into tb_agenda_proyecto( " +
                               " tb_agenda_proyecto.fecha, " +
                               " tb_agenda_proyecto.hora, " +
                               " tb_agenda_proyecto.fechaasignacion, " +
                               " tb_agenda_proyecto.horaasignacion, " +
                               " tb_agenda_proyecto.codrespinicio, " +
                               " tb_agenda_proyecto.detalle, " +
                               " tb_agenda_proyecto.personalasignado, " +
                               " tb_agenda_proyecto.codpersonalasignado, " +
                               " tb_agenda_proyecto.estado, " +
                               " tb_agenda_proyecto.fechaexpiracion, " +
                               " tb_agenda_proyecto.horaexpiracion, " +
                               " tb_agenda_proyecto.edificio, " +
                               " tb_agenda_proyecto.codproy) " +
                               " values " +
                               " (current_date(), " +
                               " current_time(), " +
                               fechaasignacion + ", " +
                               " '" + horaasignacion + "', " +
                                codRespInicio + ", " +
                               " '" + detalle + "', " +
                               " '" + personalasignado + "', " +
                               codResponsableAsignado + ", " +
                               " '" + estado + "', " +
                               fechaexpiracion + ", " +
                               " '" + horaexpiracion + "', " +
                               " '" + edificio + "', " +
                               codproy + ")";
            return cnx.ejecutarMySql(consulta); 
        }

        public bool updateAgendaProyecto(int codigoAgenda, int codrespinicio, string fechaasignacion, string horaasignacion, string detalle, string personalasignado, int codpersonalasignado, string estado, int codproy, string edificio, string horaexpiracion, string fechaexpiracion, string detalleCierre, int codrespcierre)
        {
            string consulta = "update tb_agenda_proyecto set " +
                               " tb_agenda_proyecto.codrespinicio= " + codrespinicio + "," +
                               " tb_agenda_proyecto.fechaasignacion = " + fechaasignacion + ", " +
                               " tb_agenda_proyecto.horaasignacion = '" + horaasignacion + "'," +
                               " tb_agenda_proyecto.detalle = '" + detalle + "', " +
                               " tb_agenda_proyecto.personalasignado = '" + personalasignado + "', " +
                               " tb_agenda_proyecto.codpersonalasignado = " + codpersonalasignado + "," +
                               " tb_agenda_proyecto.estado = '" + estado + "', " +
                               " tb_agenda_proyecto.edificio = '" + edificio + "', " +
                               " tb_agenda_proyecto.horaexpiracion = '" + horaexpiracion + "', " +
                               " tb_agenda_proyecto.fechaexpiracion = " + fechaexpiracion + ", " +
                               " tb_agenda_proyecto.codproy =  " + codproy;
            if(estado.Equals("Cerrado")){
                consulta = consulta + ",tb_agenda_proyecto.observacioncierre = '" + detalleCierre + "'" +
                                      ",tb_agenda_proyecto.codrespcierre = " + codrespcierre +
                                      ",tb_agenda_proyecto.horacierre = current_time()" +
                                      ",tb_agenda_proyecto.fechacierre = current_date()";
            }
            consulta = consulta+" where "+
                               " tb_agenda_proyecto.codigo = "+codigoAgenda;
            return cnx.ejecutarMySql(consulta);
        }

        public bool eliminarAgenda(int codigoAgenda)
        {
            string consulta = "delete from tb_agenda_proyecto where tb_agenda_proyecto.codigo ="+codigoAgenda;
            return cnx.ejecutarMySql(consulta);
        }

        public DataSet getAgendaporCodigo(int codigoAgenda)
        {
            string consulta = "select "+
                               " ane.codigo, "+ 
                               " DATE_FORMAT(ane.fechaasignacion,'%d/%m/%Y') as 'Fecha_Asignacion', "+
                               " ane.horaasignacion as 'HoraAsignacion', "+
                               " ane.edificio, "+
                               " ane.detalle as 'Objetivo', "+
                               " ane.personalasignado as 'Persona_Asignada', "+
                               " ane.estado, "+
                               " ane.observacioncierre as 'observacion_Cierre', "+
                               " res2.nombre as 'PersonalCierre', "+
                               " DATE_FORMAT(ane.fechaexpiracion,'%d/%m/%Y') as 'Fecha_Expiracion', "+
                               " ane.horaexpiracion, "+
                               " res1.nombre as 'PersonalInicio' "+
                               " FROM "+
                               " tb_agenda_proyecto ane "+
                               " LEFT JOIN tb_responsable res1 ON ane.codrespinicio = res1.codigo "+
                               " LEFT JOIN tb_responsable res2 ON ane.codrespcierre = res2.codigo "+
                               " where  "+
                               " ane.codigo = " + codigoAgenda; 
            return cnx.consultaMySql(consulta);
        }

        public DataSet mostrarAgendaProyecto(string personal, string edificio, string estado, string fechaDesde, string fechahasta)
        {
            string consulta = "select "+
                               " ane.codigo, "+
                               " DATE_FORMAT(ane.fechaasignacion,'%d/%m/%Y') as 'Fecha_Asignacion', "+
                               " ane.horaasignacion as 'HoraAsignacion', "+
                               " ane.edificio, "+
                               " ane.detalle as 'Objetivo', "+
                               " ane.personalasignado as 'Persona_Asignada', "+
                               " ane.estado, "+                               
                               " DATE_FORMAT(ane.fechaexpiracion,'%d/%m/%Y') as 'Fecha_Expiracion', "+
                               " ane.horaexpiracion, "+
                               " res1.nombre as 'PersonalInicio', "+
                               " ane.horacierre,"+
                               " DATE_FORMAT(ane.fechacierre,'%d/%m/%Y') as 'Fecha_Cierre',"+
                               " ane.observacioncierre as 'observacion_Cierre', " +
                               " res2.nombre as 'PersonalCierre' " +
                               " FROM "+
                               " tb_agenda_proyecto ane "+
                               " LEFT JOIN tb_responsable res1 ON ane.codrespinicio = res1.codigo "+
                               " LEFT JOIN tb_responsable res2 ON ane.codrespcierre = res2.codigo "+
                               " where "+
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