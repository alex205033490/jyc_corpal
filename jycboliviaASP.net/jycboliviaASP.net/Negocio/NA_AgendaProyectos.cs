using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Datos;

namespace jycboliviaASP.net.Negocio
{
    public class NA_AgendaProyecto
    {
        DA_AgendaProyecto nagenda = new DA_AgendaProyecto();
        public NA_AgendaProyecto() { }

        public bool eliminarAgenda(int codigoAgenda)
        {
            return nagenda.eliminarAgenda(codigoAgenda);
        }

        public bool updateAgendaProyecto(int codigoAgenda, int codrespinicio, string fechaasignacion, string horaasignacion, string detalle, string personalasignado, int codpersonalasignado, string estado, int codproy, string edificio, string horaexpiracion, string fechaexpiracion, string detalleCierre, int codrespcierre)
        {
            string fechaAsignacionAux = convertidorFecha(fechaasignacion);
            string fechaExpiracionAux = convertidorFecha(fechaexpiracion);
            if(horaasignacion.Equals("")){
                horaasignacion = "'00:00:00'";
            }

            if (horaexpiracion.Equals(""))
            {
                horaexpiracion = "'00:00:00'";
            }

            return nagenda.updateAgendaProyecto(codigoAgenda, codrespinicio, fechaAsignacionAux, horaasignacion, detalle, personalasignado, codpersonalasignado, estado, codproy, edificio, horaexpiracion, fechaExpiracionAux, detalleCierre, codrespcierre);
        }

        public bool insertarAgendaProyecto(int codRespInicio, string fechaasignacion, string horaasignacion, string personalasignado, int codResponsableAsignado, string detalle, string estado, int codproy, string edificio, string horaexpiracion, string fechaexpiracion)
        {
            string fechaAsignacionAux = convertidorFecha(fechaasignacion);
            string fechaExpiracionAux = convertidorFecha(fechaexpiracion);
            if (horaasignacion.Equals(""))
            {
                horaasignacion = "00:00:00";
            }

            if (horaexpiracion.Equals(""))
            {
                horaexpiracion = "00:00:00";
            }
            return nagenda.insertarAgendaProyecto(  codRespInicio, fechaAsignacionAux,  horaasignacion,  personalasignado,  codResponsableAsignado,  detalle,   estado,  codproy,  edificio, horaexpiracion,  fechaExpiracionAux);
        }

        public string convertidorFecha(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;                
                return "'" + _fecha + "'";
            }
            else
                return "null";
        }

        public DataSet mostrarAgendaProyecto(string personal, string edificio, string estado, string fechaDesde, string fechaHasta) {
            string fechaDesdeAux = convertidorFecha(fechaDesde);
            string fechaHastaAux = convertidorFecha(fechaHasta);            
            return nagenda.mostrarAgendaProyecto( personal,  edificio,  estado, fechaDesdeAux, fechaHastaAux); 
        }

        public DataSet getAgendaporCodigo(int codigoAgenda)
        {
            return nagenda.getAgendaporCodigo(codigoAgenda);
        }
        
    }
}