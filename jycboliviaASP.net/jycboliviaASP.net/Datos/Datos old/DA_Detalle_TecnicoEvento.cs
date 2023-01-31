using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_Detalle_TecnicoEvento
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DA_Detalle_TecnicoEvento() { }


        public bool insertar(int codEvento, int codTecnico, string fechaAsignacion, string horaAsignacion,int supervisor)
        {
            string consulta = "insert into tb_detalle_teceven(codeven,codtec,fecha_asignacion,hora_asignacion,supervisor,cod_boletaemergenciacallcenter) values (" + codEvento + "," + codTecnico + ",'" + fechaAsignacion + "','" + horaAsignacion + "'," + supervisor + ",0)";
                return ConecRes.ejecutarMySql(consulta);         
        }

        public bool ModifiarDatosSupervisor(int codEvento, int codTecnico, string fechaAsignacion, string horaAsignacion, int supervisor)
        {
            string consulta = "update tb_detalle_teceven "+
                               " set "+
                               " tb_detalle_teceven.fecha_asignacion = "+fechaAsignacion+", "+
                               " tb_detalle_teceven.hora_asignacion = "+horaAsignacion+", "+
                               " tb_detalle_teceven.supervisor = " +supervisor+
                               " where  "+
                               " tb_detalle_teceven.codeven = "+codEvento+
                               " and tb_detalle_teceven.codtec = "+codTecnico;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool ModifiarDatosSupervisor2(int codEvento, int codDetalleSupervisor, string fechaAsignacion, string horaAsignacion, int supervisor)
        {
            string consulta = "update tb_detalle_teceven " +
                               " set " +
                               " tb_detalle_teceven.fecha_asignacion = " + fechaAsignacion + ", " +
                               " tb_detalle_teceven.hora_asignacion = " + horaAsignacion + ", " +
                               " tb_detalle_teceven.supervisor = " + supervisor +
                               " where  " +
                               " tb_detalle_teceven.codeven = " + codEvento +
                               " and tb_detalle_teceven.codigo = " + codDetalleSupervisor;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool modificarDatosTecnico(int codEvento, int codTecnico,string horallegada,string horaSalida,string estadoEquipoLLegadaSalida,string Observacion, string fechaAsignacion, string horaAsignacion)
        {
            string consulta = "update tb_detalle_teceven set tb_detalle_teceven.hora_llegadaEdificio='" + horallegada + "' , tb_detalle_teceven.hora_salidaEdificio= '" + horaSalida + "', " +
                              " tb_detalle_teceven.estadoEquipo_llegadasalida = '" + estadoEquipoLLegadaSalida + "',tb_detalle_teceven.observacion ='" + Observacion + "' " +
                              " ,tb_detalle_teceven.fecha_asignacion = "+fechaAsignacion+", tb_detalle_teceven.hora_asignacion = "+horaAsignacion+
                              " where tb_detalle_teceven.codeven = " + codEvento + " and tb_detalle_teceven.codtec = " + codTecnico;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool tienehoradellegada(int codEvento, int codigoDetalleTecnicoEvento)
        {
            string consulta = " select te.hora_llegadaEdificio from tb_detalle_teceven te " +
                               " where te.codeven = " + codEvento + " and te.codigo = " + codigoDetalleTecnicoEvento;
            DataSet tupla = ConecRes.consultaMySql(consulta);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }


        public bool eslamismahoradellegada(int codEvento, int codigoDetalleTecnicoEvento, string horallegada)
        {
            string consulta = " select te.hora_llegadaEdificio from tb_detalle_teceven te " +
                               " where te.codeven = " + codEvento + " and te.codigo = " + codigoDetalleTecnicoEvento;
            DataSet tupla = ConecRes.consultaMySql(consulta);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                string horaobtenida = tupla.Tables[0].Rows[0][0].ToString();
                if(horaobtenida.Equals(horallegada)){
                    return true;
                }else
                    return false;
            }
            else
                return false;
        }


        public bool tienehoradeSalida(int codEvento, int codigoDetalleTecnicoEvento)
        {
            string consulta = " select te.hora_salidaEdificio from tb_detalle_teceven te " +
                               " where te.codeven = " + codEvento + " and te.codigo = " + codigoDetalleTecnicoEvento;
            DataSet tupla = ConecRes.consultaMySql(consulta);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }


    /*    public bool eslamismahoradeSalida(int codEvento, int codigoDetalleTecnicoEvento, string horaSalida)
        {
            string consulta = " select te.hora_salidaEdificio from tb_detalle_teceven te " +
                               " where te.codeven = " + codEvento + " and te.codigo = " + codigoDetalleTecnicoEvento;
            DataSet tupla = ConecRes.consultaMySql(consulta);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                string horasalidaobtenida = tupla.Tables[0].Rows[0][0].ToString();
                if(){
                return true;
                }
                
            }
            else
                return false;
        }  */
        public bool modificarDatosTecnico2(int codEvento, int codigoDetalleTecnicoEvento, string horallegada, string horaSalida, string estadoEquipoLLegadaSalida, string Observacion, string fechaAsignacion, string horaAsignacion, string nroboleta, bool trabajoProgramado, string ascensorReparado,string fechahorallegadaEdificio,string fechahoraSalidaEdificio, string fechaReporte, string horaReporte)
        {
            if (horaAsignacion.Equals(""))
            {
                horaAsignacion = "null";
            }
            else
                horaAsignacion = "'" + horaAsignacion + "'";

            if (horallegada.Equals(""))
            {
                horallegada = "null";
            }
            else
                horallegada = "'"+horallegada+"'";

            if (horaReporte.Equals(""))
            {
                horaReporte = "null";
            }
            else
                horaReporte = "'" + horaReporte + "'";

            if (horaSalida.Equals(""))
            {
                horaSalida = "null";
            }
            else
                horaSalida = "'" + horaSalida + "'";

            string consulta = "update tb_detalle_teceven set tb_detalle_teceven.hora_llegadaEdificio= " + horallegada + " , tb_detalle_teceven.fecha_llegadaedificio = " + fechahorallegadaEdificio + " ," +
                              " tb_detalle_teceven.hora_salidaEdificio= " + horaSalida + ", tb_detalle_teceven.fecha_salidaedificio =  " +fechahoraSalidaEdificio+ " , "+
                              " tb_detalle_teceven.estadoEquipo_llegadasalida = '" + estadoEquipoLLegadaSalida + "',tb_detalle_teceven.observacion ='" + Observacion + "' " +
                              " ,tb_detalle_teceven.hora_reporte = "+horaReporte+" , tb_detalle_teceven.fecha_horareporte = " +fechaReporte+
                              " ,tb_detalle_teceven.fecha_asignacion = " + fechaAsignacion + ", tb_detalle_teceven.hora_asignacion = " + horaAsignacion +
                              " ,tb_detalle_teceven.nroboleta = '"+nroboleta+"' "+
                              " ,tb_detalle_teceven.trabajoprogramado = "+trabajoProgramado+
                              " ,tb_detalle_teceven.ascensorreparado = '"+ascensorReparado+"' "+
                              " where tb_detalle_teceven.codeven = " + codEvento + " and tb_detalle_teceven.codigo = " + codigoDetalleTecnicoEvento;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool eliminar(int codEvento, int codTecnico)
        {
               string consulta = "delete from tb_detalle_teceven where codeven = " + codEvento + " and codtec = " + codTecnico;
               return  ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }


    }
}