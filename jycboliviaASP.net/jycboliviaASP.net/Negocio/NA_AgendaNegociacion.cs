using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Datos;

namespace jycboliviaASP.net.Negocio
{
    public class NA_AgendaNegociacion
    {
        DA_AgendaNegociacion nagenda = new DA_AgendaNegociacion();
        public NA_AgendaNegociacion() { }

        public bool eliminarAgenda(int codigoAgenda)
        {
            return nagenda.eliminarAgenda(codigoAgenda);
        }

        public bool updateAgenda(int codigoAgenda, string fechaasignacion, string horaasignacion, string detalle, string personalasignado, string estado, int codproy, string edificio, string horaexpiracion, string fechaexpiracion, string detalleCierre)
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
            
            return nagenda.updateAgenda(codigoAgenda, fechaAsignacionAux, horaasignacion, detalle, personalasignado, estado, codproy, edificio, horaexpiracion, fechaExpiracionAux, detalleCierre);
        }

        public bool insertarAgenda(string fechaasignacion, string horaasignacion, string detalle, string personalasignado, string estado, int codproy, string edificio, string horaexpiracion, string fechaexpiracion)
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
            return nagenda.insertarAgenda( fechaAsignacionAux,  horaasignacion,  detalle,  personalasignado,  estado,  codproy,  edificio, horaexpiracion, fechaExpiracionAux);
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

        public DataSet mostrarAgendaNegociacion(string personal, string edificio, string estado, string fechaDesde, string fechaHasta) {
            string fechaDesdeAux = convertidorFecha(fechaDesde);
            string fechaHastaAux = convertidorFecha(fechaHasta);            
            return nagenda.mostrarAgendaNegociacion( personal,  edificio,  estado, fechaDesdeAux, fechaHastaAux); 
        }

        public DataSet getAgendaporCodigo(int codigoAgenda)
        {
            return nagenda.getAgendaporCodigo(codigoAgenda);
        }
        
    }
}