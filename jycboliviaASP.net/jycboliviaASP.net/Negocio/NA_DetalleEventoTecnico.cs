using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_DetalleEventoTecnico
    {
        DA_Detalle_TecnicoEvento DDetalleTecEven = new DA_Detalle_TecnicoEvento();
        public NA_DetalleEventoTecnico() { }

        public bool insertar(int codEvento, int codTecnico, int supervisor )
        {
            return DDetalleTecEven.insertar(codEvento, codTecnico, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"),supervisor);
        }

        public bool ModifiarDatosSupervisor(int codEvento, int codTecnico, string fechaAsignacion, string horaAsignacion, int supervisor) {
            return DDetalleTecEven.ModifiarDatosSupervisor(codEvento, codTecnico, fechaAsignacion,  horaAsignacion,  supervisor);
        }


        public bool ModifiarDatosSupervisor2(int codEvento, int codDetalleSupervisor, string fechaAsignacion, string horaAsignacion, int supervisor)
        {
            return DDetalleTecEven.ModifiarDatosSupervisor2(codEvento, codDetalleSupervisor, fechaAsignacion, horaAsignacion, supervisor);
        }

        public bool eliminar(int codEvento, int codTecnico)
        {
            return DDetalleTecEven.eliminar(codEvento,codTecnico);
       }

        public DataSet getAllResponsablesAsignados(int codigoEvento,int supervisor) {
            string consulta = "select det.codigo, resp.nombre, det.hora_asignacion, date_format(det.fecha_asignacion,'%d/%m/%y') as fecha_Asignacion " +
                               " from tb_detalle_teceven det, tb_responsable resp "+
                               " where det.codtec = resp.codigo and det.codeven = "+codigoEvento+" and det.supervisor = "+supervisor;
            return DDetalleTecEven.getDatos(consulta);
        }

        public DataSet getAllResponsablesAsignados2(int codigoEvento,int codigoResponsable, int supervisor)
        {
            string consulta = "select resp.codigo, resp.nombre, det.hora_asignacion, date_format(det.fecha_asignacion,'%d/%m/%Y') as fecha_Asignacion, " +
                               " det.hora_llegadaEdificio, det.hora_salidaEdificio, det.estadoEquipo_llegadasalida, det.observacion, det.nroboleta "+
                               " ,det.ascensorreparado,det.trabajoprogramado, DATE_FORMAT(det.fecha_llegadaedificio,'%d/%m/%Y'), DATE_FORMAT(det.fecha_salidaedificio,'%d/%m/%Y') " +
                               " ,det.hora_reporte, DATE_FORMAT(det.fecha_horareporte ,'%d/%m/%Y') " +
                               " from tb_detalle_teceven det, tb_responsable resp " +
                               " where det.codtec = resp.codigo and det.codeven = " + codigoEvento + " and det.supervisor = " + supervisor+" and det.codigo = "+codigoResponsable;
            return DDetalleTecEven.getDatos(consulta);
        }

        public DataSet getAllResponsablesAsignados3(int codigoEvento, int codigoEventoTecnicoAsignado, int supervisor)
        {
            string consulta = "select resp.codigo, resp.nombre, det.hora_asignacion, date_format(det.fecha_asignacion,'%d/%m/%y') as fecha_Asignacion, " +
                               " det.hora_llegadaEdificio, det.hora_salidaEdificio, det.estadoEquipo_llegadasalida, det.observacion " +
                               " from tb_detalle_teceven det, tb_responsable resp " +
                               " where det.codtec = resp.codigo and det.codeven = " + codigoEvento + " and det.supervisor = " + supervisor + " and det.codigo = " + codigoEventoTecnicoAsignado;
            return DDetalleTecEven.getDatos(consulta);
        }

        public bool modificarDatosTecnico(int codEvento, int codTecnico, string horallegada, string horaSalida, string estadoEquipoLLegadaSalida, string Observacion, string fechaAsignacion, string horaAsignacion)
        {
            return DDetalleTecEven.modificarDatosTecnico(codEvento,codTecnico,horallegada,horaSalida,estadoEquipoLLegadaSalida,Observacion, fechaAsignacion, horaAsignacion);
        }




        public bool modificarDatosTecnico2(int codEvento, int codEventoTecnicoAsignado, string horallegada, string horaSalida, string estadoEquipoLLegadaSalida, string Observacion, string fechaAsignacion, string horaAsignacion, string nroboleta, bool trabajoProgramado, string ascensorReparado, string fechahorallegadaEdificio,string fechahoraSalidaEdificio, string fechaReporte, string horaReporte)
        {
            return DDetalleTecEven.modificarDatosTecnico2(codEvento, codEventoTecnicoAsignado, horallegada, horaSalida, estadoEquipoLLegadaSalida, Observacion, fechaAsignacion, horaAsignacion, nroboleta,  trabajoProgramado,  ascensorReparado, fechahorallegadaEdificio, fechahoraSalidaEdificio,  fechaReporte,  horaReporte);
        }

        public bool existeSupervisor(int codEvento) {
            string consulta = "select * from tb_detalle_teceven dte where dte.codeven = "+codEvento+" and dte.supervisor = 1";
            if (DDetalleTecEven.getDatos(consulta).Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

    }
}