using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;
using System.IO;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Evento
    {
        private DA_Evento Devento = new DA_Evento();

        public NA_Evento() { }

        public bool insertar(int semana, string cliente, string telefono, string celular, string nombreEdificio, string dirEdificio, string ascensor, string estadoEvento, string Observacion, int codtipoevento, int codEdificio, int ascensorParado, int personasatrapadas, int prioridad, int inicioUser, bool cambioRepuesto, bool areacotirepuesto, bool solicitudrepuestobandera, bool areacallcenter, bool arearin, bool arearcc, bool areacliente)
        {
            return Devento.insertar(semana, DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"), cliente, telefono, celular, nombreEdificio, dirEdificio, ascensor, estadoEvento, Observacion, codtipoevento, codEdificio, ascensorParado, personasatrapadas, prioridad, inicioUser, cambioRepuesto, areacotirepuesto, solicitudrepuestobandera, areacallcenter, arearin, arearcc, areacliente);           
        }

        public bool modificar(int codigoEvento, string cliente, string telefono, string celular, string nombreEdificio, string dirEdificio, string ascensor, string Observacion,  int ascensorParado, int personasatrapadas)
        {
            return Devento.modificar(codigoEvento, cliente, telefono, celular, nombreEdificio, dirEdificio, ascensor, Observacion, ascensorParado, personasatrapadas);
        }

        public bool eliminar()
        {
            return false;
        }

        public DataSet mostrarAllDatos()
        {
            string consulta = "select c.codigo, c.nombre, c.detalle from tb_cargo c";
            return Devento.getDatos(consulta);
        }

        public DataSet getAllEventos(int tiket,string nombreEdificio, string nombreTipoEvento, string NroSemana, string estadoEvento, int cambioRepuesto, string fechaDesde, string fechaHasta)
        {
            string consulta = "select even.codigo as Ticket, " +
                               " even.semana, " +
                               " even.hora, " +
                               " date_format(even.fecha,'%d/%m/%y') as Fecha, " +
                               " even.cliente, " +
                               " even.nombreEdificio, even.ascensorparado as 'Ascensor Parado',even.personasatrapadas as 'Personas Atrapadas' ," +
                               " tv.nombre as 'TipoEvento', " +
                               " p.nombre as 'Prioridad ', " +
                               " resp.nombre as 'EventoAbierto' " +
                               " ,even.Observacion as 'Solicitud de Servicio o Atencion'" +
                               " ,even.observacion_evento as 'Observacion_Cierre_Evento' " +                               
                               " from  tb_tipoevento tv, tb_prioridad p, tb_responsable resp, " +
                               " tb_evento even "+                               
                               " where "+                               
                               " even.estadoEvento = '" + estadoEvento + "' and " +
                               " even.cambiorepuesto = " + cambioRepuesto + " and " +
                               " even.prioridad = p.codigo and " +
                               " even.codtipoevento = tv.codigo and " +
                               " even.inicioeventouser = resp.codigo and " +
                               "  even.areacallcenter = 1 and " +
                               " (even.arearcc is null or even.arearcc<>1) and " +
                               " (even.arearin is null or even.arearin<>1) and " +                               
                               " (even.areacliente is null or even.areacliente<>1) ";

                               if(!nombreEdificio.Equals("")){
                                   consulta = consulta + " and even.nombreEdificio like '%" + nombreEdificio + "%' ";
                               }
                                
                               if(!NroSemana.Equals("")){
                                    consulta = consulta + " and even.semana like '%" + NroSemana + "%' ";
                                }

                               if(!nombreTipoEvento.Equals("")){
                                   consulta = consulta + " and tv.nombre like '%" + nombreTipoEvento + "%' ";
                               }
                               
                               if(tiket > 0){
                                   consulta = consulta + " and even.codigo = " + tiket;
                               }


            if (!fechaDesde.Equals("") && !fechaHasta.Equals(""))
            {
                consulta = consulta + " and even.fecha between " + fechaDesde + " and " + fechaHasta + " ";
            }

            consulta = consulta + " group by even.codigo ";
            consulta = consulta + " order by (even.codigo) desc";
            return Devento.getDatos(consulta);
        }

        /// <summary>
        /// modificar akiiii
        /// 
        /// </summary>
        /// <param name="tiket"></param>
        /// <param name="nombreEdificio"></param>
        /// <param name="nombreTipoEvento"></param>
        /// <param name="NroSemana"></param>
        /// <param name="estadoEvento"></param>
        /// <param name="cambioRepuesto"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        public DataSet getReporteAlexanderEventos(int tiket, string nombreEdificio, string nombreTipoEvento, string NroSemana, string estadoEvento, int cambioRepuesto, string fechaDesde, string fechaHasta)
        {
            string consulta = "select even.codigo as Ticket, " +
                               " even.semana, " +
                               " even.hora, " +
                               " date_format(even.fecha,'%d/%m/%y') as Fecha, " +
                               " even.cliente, " +
                               " even.nombreEdificio, even.ascensorparado as 'Ascensor Parado',even.personasatrapadas as 'Personas Atrapadas' ," +
                               " tv.nombre as 'TipoEvento', " +
                               " p.nombre as 'Prioridad ', " +
                               " resp.nombre as 'EventoAbierto' " +
                               " , res.nombre as 'Tecnico', "+
                               " IF(te.supervisor = 1, 'Supervisor', 'Tecnico') as 'Supervisor_Tecnico', "+
                               " te.estadoEquipo_llegadasalida, "+
                               " te.ascensorreparado, "+
                               " te.observacion as 'Observacion_Tecnico', "+
                               " even.observacion_evento as 'Observacion_Cierre_Evento'" +
                               " from "+
                               " tb_tipoevento tv, "+
                               " tb_prioridad p, "+
                               " tb_responsable resp, "+
                               " tb_evento even "+
                               " left join tb_detalle_teceven te on even.codigo = te.codeven "+
                               " left join tb_responsable res on   res.codigo = te.codtec "+                               
                               " where "+                               
                               " even.estadoEvento = '" + estadoEvento + "' and " +
                               " even.cambiorepuesto = " + cambioRepuesto + " and " +
                               " even.prioridad = p.codigo and " +
                               " even.codtipoevento = tv.codigo and " +
                               " even.inicioeventouser = resp.codigo and " +
                               " (even.areacliente is null or even.areacliente<>1) ";

            if (!nombreEdificio.Equals(""))
            {
                consulta = consulta + " and even.nombreEdificio like '%" + nombreEdificio + "%' ";
            }

            if (!NroSemana.Equals(""))
            {
                consulta = consulta + " and even.semana like '%" + NroSemana + "%' ";
            }

            if (!nombreTipoEvento.Equals(""))
            {
                consulta = consulta + " and tv.nombre like '%" + nombreTipoEvento + "%' ";
            }

            if (tiket > 0)
            {
                consulta = consulta + " and even.codigo = " + tiket;
            }


            if (!fechaDesde.Equals("") && !fechaHasta.Equals(""))
            {
                consulta = consulta + " and even.fecha between " + fechaDesde + " and " + fechaHasta + " ";
            }
            consulta = consulta + " order by (even.codigo) desc";
            return Devento.getDatos(consulta);
        }



        public DataSet getAllEventoAreaCliente(int tiket, string nombreEdificio, string nombreTipoEvento, string NroSemana, string estadoEvento, string fechaDesde, string fechaHasta)
        {
            string consulta = "select even.codigo as Ticket, even.semana, even.hora, date_format(even.fecha,'%d/%m/%y') as Fecha, even.cliente, " +
                               " even.nombreEdificio, even.ascensorparado as 'Ascensor Parado', even.personasatrapadas as 'Personas Atrapadas' ," +
                               " tv.nombre as 'TipoEvento', " +
                               " p.nombre as 'Prioridad ', " +
                               " resp.nombre as 'EventoAbierto' " +
                               " ,even.Observacion as 'Solicitud de Servicio o Atencion'" +
                               " , even.observacion_evento, " +
                               " DATE_FORMAT(even.solicitudRepuesto,'%d/%m/%Y') as 'Solicitud_Repuesto', " +
                               " DATE_FORMAT(even.envioproforma,'%d/%m/%Y') as 'Envio_Proforma', " +
                               " DATE_FORMAT(even.aceptacionproforma,'%d/%m/%Y') as 'Aceptacion_Proforma', " +
                               " even.verificacion_cambio, " +
                               " even.observacion_necesidadrepuesto " +                               
                               " from tb_tipoevento tv, tb_prioridad p, tb_responsable resp, " +
                               " tb_evento even "+                               
                               " where  "+                               
                               " even.estadoEvento = '" + estadoEvento + "' and " +
                               " even.prioridad = p.codigo and " +
                               " even.codtipoevento = tv.codigo and " +
                               " even.inicioeventouser = resp.codigo and " +
                               " even.areacliente = 1 and " +
                               " (even.arearcc is null or even.arearcc<>1) and " +
                               " (even.arearin is null or even.arearin<>1) and " +
                               " (even.areacallcenter is null or even.areacallcenter<>1) ";

            if (!nombreEdificio.Equals(""))
            {
                consulta = consulta + " and even.nombreEdificio like '%" + nombreEdificio + "%' ";
            }

            if (!NroSemana.Equals(""))
            {
                consulta = consulta + " and even.semana like '%" + NroSemana + "%' ";
            }

            if (!nombreTipoEvento.Equals(""))
            {
                consulta = consulta + " and tv.nombre like '%" + nombreTipoEvento + "%' ";
            }

            if (tiket > 0)
            {
                consulta = consulta + " and even.codigo = " + tiket;
            }


            if (!fechaDesde.Equals("") && !fechaHasta.Equals(""))
            {
                consulta = consulta + " and even.fecha between " + fechaDesde + " and " + fechaHasta + " ";
            }

            consulta = consulta + " group by even.codigo ";
            consulta = consulta + " order by (even.codigo) desc";
            return Devento.getDatos(consulta);
        }


        public DataSet getAllEventoAreaCortizacionRepuesto(int tiket, string nombreEdificio, string nombreTipoEvento, string NroSemana, string estadoEvento, string fechaDesde, string fechaHasta)
        {
            string consulta = "select even.codigo as Ticket, even.semana, even.hora, date_format(even.fecha,'%d/%m/%y') as Fecha, even.cliente, " +
                               " even.nombreEdificio, even.ascensorparado as 'Ascensor Parado', even.personasatrapadas as 'Personas Atrapadas' ," +
                               " tv.nombre as 'TipoEvento', " +
                               " p.nombre as 'Prioridad ', " +
                               " resp.nombre as 'EventoAbierto' " +
                               " , even.observacion_evento, " +
                               " DATE_FORMAT(even.solicitudRepuesto,'%d/%m/%Y') as 'Solicitud_Repuesto', " +
                               " DATE_FORMAT(even.envioproforma,'%d/%m/%Y') as 'Envio_Proforma', " +
                               " DATE_FORMAT(even.aceptacionproforma,'%d/%m/%Y') as 'Aceptacion_Proforma', " +
                               " even.verificacion_cambio, " +
                               " even.observacion_necesidadrepuesto " +
                               " from tb_evento even, tb_tipoevento tv, tb_prioridad p, tb_responsable resp " +
                               " where even.estadoEvento = '" + estadoEvento + "' and " +
                               " even.prioridad = p.codigo and " +
                               " even.codtipoevento = tv.codigo and " +
                               " even.inicioeventouser = resp.codigo and " +
                               " even.areacotirepuesto = 1 ";

            if (!nombreEdificio.Equals(""))
            {
                consulta = consulta + " and even.nombreEdificio like '%" + nombreEdificio + "%' ";
            }

            if (!NroSemana.Equals(""))
            {
                consulta = consulta + " and even.semana like '%" + NroSemana + "%' ";
            }

            if (!nombreTipoEvento.Equals(""))
            {
                consulta = consulta + " and tv.nombre like '%" + nombreTipoEvento + "%' ";
            }

            if (tiket > 0)
            {
                consulta = consulta + " and even.codigo = " + tiket;
            }


            if (!fechaDesde.Equals("") && !fechaHasta.Equals(""))
            {
                consulta = consulta + " and even.fecha between " + fechaDesde + " and " + fechaHasta + " ";
            }
            consulta = consulta + " order by (even.codigo) desc";
            return Devento.getDatos(consulta);
        }

        public DataSet getEvento(int codigo)
        {
            string consulta = "select "+
                               " even.codigo, "+
                               " even.semana, "+
                               " even.hora, "+
                               " DATE_FORMAT(even.fecha,'%d/%m/%Y') as 'Fecha', "+
                               " even.cliente, "+
                               " even.telefono, "+
                               " even.celular, "+
                               " even.nombreEdificio, "+
                               " even.dirEdificio, "+
                               " even.ascensor, "+
                               " even.estadoEvento, "+
                               " even.Observacion, "+
                               " even.defectoconstatado, "+
                               " even.observacion_necesidadrepuesto, "+
                               " DATE_FORMAT(even.solicitudRepuesto,'%d/%m/%Y') as 'solicitudRepuesto', "+
                               " DATE_FORMAT(even.envioproforma,'%d/%m/%Y') as 'envioProforma', "+
                               " DATE_FORMAT(even.aceptacionproforma,'%d/%m/%Y') as 'aceptacionProforma', "+
                               " even.verificacion_cambio, "+
                               " even.codtipoevento, "+
                               " even.codEdificio, "+
                               " even.ascensorparado, "+
                               " even.personasatrapadas, "+
                               " even.observacion_evento, "+
                               " even.prioridad, "+
                               " even.cambiorepuesto, "+
                               " even.inicioeventouser, "+
                               " even.cierreeventouser, "+
                               " DATE_FORMAT(even.fechacierreevento,'%d/%m/%Y') as 'fechaCierreEvento', "+
                               " even.horacierreevento, "+
                               " even.solicitudrepuestobandera, " +
                               " even.areacliente, "+
                               " DATE_FORMAT(even.fechacontactocliente,'%d/%m/%Y') as 'ContactoCliente',"+
                               " even.detalle_contactocliente "+
                               " from tb_evento even where even.codigo = "+codigo;
            return Devento.getDatos(consulta);

        }

        public DataSet getCodigoEdificioEvento(int codigo)
        {
            string consulta = "select " +                               
                               " even.codEdificio " +                               
                               " from tb_evento even where even.codigo = " + codigo;
            return Devento.getDatos(consulta);

        }        

        public int getTikect() { 
            int numeroTicket = 0 ;
            string consulta = "select Max(even.codigo) from tb_evento even ";
            DataSet resultado = Devento.getDatos(consulta);
            string contenido = resultado.Tables[0].Rows[0][0].ToString();
            if (resultado.Tables[0].Rows.Count > 0 && contenido != "")
            {
                numeroTicket = Convert.ToInt32(resultado.Tables[0].Rows[0][0].ToString());
            }
            return numeroTicket;
        }

        public bool modificarDatosEvento(int codEvento, string estadoEvento, string observacion, string defectoConstatado, string observacionNecesidadRepuesto, string solicitudRepuesto, string envioProforma, string aceptacionProforma, string verificacionCambio, bool cambioRepuesto, int codUserCierre, string fechaEven, string horaEven, bool solicitudRepuestoBandera, bool areaCliente, bool areaCallcenter, bool areaRin, bool areaRcc, int prioridad)
        {
            return Devento.modificarDatosEvento(codEvento, estadoEvento, observacion, defectoConstatado, observacionNecesidadRepuesto, solicitudRepuesto, envioProforma, aceptacionProforma, verificacionCambio, cambioRepuesto, codUserCierre, fechaEven, horaEven, solicitudRepuestoBandera, areaCliente,areaCallcenter,areaRin,areaRcc,prioridad);
        }

        public bool modificarDatosEvento2(int codEvento, string estadoEvento, string observacion, string defectoConstatado, string observacionNecesidadRepuesto, string solicitudRepuesto, string envioProforma, string aceptacionProforma, string verificacionCambio, bool cambioRepuesto, int codUserCierre, string fechaEven, string horaEven, bool solicitudRepuestoBandera)
        {
            return Devento.modificarDatosEvento2(codEvento, estadoEvento, observacion, defectoConstatado, observacionNecesidadRepuesto, solicitudRepuesto, envioProforma, aceptacionProforma, verificacionCambio, cambioRepuesto, codUserCierre, fechaEven, horaEven, solicitudRepuestoBandera);
        }

        public bool modificarDatosEvento_FechaContactoCliente(int codEvento, string fechaContactoCliente, string detalleContactoCliente)
        {         
            return Devento.modificarDatosEvento_FechaContactoCliente(codEvento, fechaContactoCliente, detalleContactoCliente);
        }

        public DataSet getAllEventosRCC(int tiket, string nombreEdificio, string nombreTipoEvento, string NroSemana, string estadoEvento, int cambioRepuesto, int solicitudRepuestoBandera, string fechaDesde, string fechaHasta)
        {
            string consulta = "select even.codigo as Ticket, even.semana, even.hora, date_format(even.fecha,'%d/%m/%y') as Fecha, even.cliente, " +
                               " even.nombreEdificio, even.ascensorparado as 'Ascensor Parado',even.personasatrapadas as 'Personas Atrapadas' ," +
                               " tv.nombre as 'TipoEvento', " +
                               " p.nombre as 'Prioridad ' " +
                               " ,even.Observacion as 'Solicitud de Servicio o Atencion'" +
                               " , even.observacion_evento, " +
                               " DATE_FORMAT(even.solicitudRepuesto,'%d/%m/%Y') as 'Solicitud_Repuesto', " +
                               " DATE_FORMAT(even.envioproforma,'%d/%m/%Y') as 'Envio_Proforma', " +
                               " DATE_FORMAT(even.aceptacionproforma,'%d/%m/%Y') as 'Aceptacion_Proforma', " +
                               " even.verificacion_cambio, " +
                               " even.observacion_necesidadrepuesto " +                               
                               " from tb_tipoevento tv, tb_prioridad p, " +
                               " tb_evento even "+                               
                               " where even.estadoEvento = '" + estadoEvento + "' and " +
                               " even.cambiorepuesto = " + cambioRepuesto + " and " +
                               " even.solicitudrepuestobandera = " + solicitudRepuestoBandera + " and " +
                               " even.prioridad = p.codigo and " +
                               " even.codtipoevento = tv.codigo and " +
                               " even.arearcc = 1 and " +
                           //    " even.solicitudRepuesto is not null and " +
                           //    " even.aceptacionproforma is null and " +
                               " (even.areacliente is null or even.areacliente<>1) and " +
                               " (even.arearin is null or even.arearin<>1) and " +
                               " (even.areacallcenter is null or even.areacallcenter<>1) "; 
                               
                               
                              if(!nombreEdificio.Equals("")){
                                  consulta = consulta + " and even.nombreEdificio like '%" + nombreEdificio + "%' ";
                               }

                              if (!NroSemana.Equals("")) {
                                  consulta = consulta + " and even.semana like '%" + NroSemana + "%' ";
                              }

                              if (!nombreTipoEvento.Equals("")) {
                                  consulta = consulta + " and tv.nombre like '%" + nombreTipoEvento + "%' ";
                              } 
                              
                              if(tiket > 0){
                                  consulta = consulta + " and even.codigo = " + tiket;
                              }

                              if (!fechaDesde.Equals("") && !fechaHasta.Equals(""))
                              {
                                  consulta = consulta + " and even.fecha between " + fechaDesde + " and " + fechaHasta + " ";
                              }

                              consulta = consulta + " group by even.codigo ";                
                              consulta = consulta + " order by (even.codigo) desc";
            return Devento.getDatos(consulta);

        }

        public DataSet getAllEventosRIN(int tiket,string nombreEdificio, string nombreTipoEvento, string NroSemana, string estadoEvento, int cambioRepuesto, string fechaDesde, string fechaHasta)
        {
            string consulta = "select even.codigo as Ticket, even.semana, even.hora, date_format(even.fecha,'%d/%m/%y') as Fecha, even.cliente, " +
                               " even.nombreEdificio, even.ascensorparado as 'Ascensor Parado',even.personasatrapadas as 'Personas Atrapadas' ," +
                               " tv.nombre as 'TipoEvento', " +
                               " p.nombre as 'Prioridad ' " +
                               " ,even.Observacion as 'Solicitud de Servicio o Atencion'" +
                               " , even.observacion_evento, " +
                               " DATE_FORMAT(even.solicitudRepuesto,'%d/%m/%Y') as 'Solicitud_Repuesto', " +
                               " DATE_FORMAT(even.envioproforma,'%d/%m/%Y') as 'Envio_Proforma', " +
                               " DATE_FORMAT(even.aceptacionproforma,'%d/%m/%Y') as 'Aceptacion_Proforma', " +
                               " even.verificacion_cambio, " +
                               " even.observacion_necesidadrepuesto " +                               
                               " from  tb_tipoevento tv, tb_prioridad p, " +
                               " tb_evento even "+                               
                               " where even.estadoEvento = '" + estadoEvento + "' and " +
                               " even.cambiorepuesto = " + cambioRepuesto + " and " +                               
                               " even.prioridad = p.codigo and " +
                               " even.codtipoevento = tv.codigo and " +
                               " even.arearin = 1 and " +
                               " (even.solicitudRepuesto is null or even.aceptacionproforma is not null) and " +
                               " (even.areacliente is null or even.areacliente<>1) and " +
                               " (even.arearcc is null or even.arearcc<>1) and " +
                               " (even.areacallcenter is null or even.areacallcenter<>1) ";
                               
                                if (!nombreEdificio.Equals(""))
                                {
                                    consulta = consulta + " and even.nombreEdificio like '%" + nombreEdificio + "%' ";
                                }

                                if (!NroSemana.Equals(""))
                                {
                                    consulta = consulta + " and even.semana like '%" + NroSemana + "%' ";
                                }

                                if (!nombreTipoEvento.Equals(""))
                                {
                                    consulta = consulta + " and tv.nombre like '%" + nombreTipoEvento + "%' ";
                                }
                               
                                if(tiket > 0){
                                    consulta = consulta + " and even.codigo = " + tiket;
                                }
                               
            if (!fechaDesde.Equals("") && !fechaHasta.Equals(""))
            {
                consulta = consulta + " and even.fecha between " + fechaDesde + " and " + fechaHasta + " ";
            }

            consulta = consulta + " group by even.codigo ";   
            consulta = consulta + " order by (even.codigo) desc";                               
            return Devento.getDatos(consulta);

        }


        public DataSet estadisticaNormalCallCenter(string nombreEdificio, string nombreTipoEvento, string NroSemana, string estadoEvento, string fechaDesde, string fechaHasta, bool exportar)
        {
            string consulta = "select even.codigo as 'Ticket', even.semana, " +
                               " even.cliente, even.nombreEdificio as 'Nombre_del_edificio', " +
                               " teven.nombre as 'TipoEvento', " +
                               " pri.nombre as 'Prioridad1', " +
                               " even.hora as 'HoraEvento', " +
                               " DATE_FORMAT(even.fecha,'%d/%m/%Y') as 'FechaEvento', " +
                               " even.horacierreevento as 'HoraCierre', " +
                               " DATE_FORMAT(even.fechacierreevento,'%d/%m/%Y') as 'FechaCierre', " +
                               " DATEDIFF(even.fechacierreevento,even.fecha) as 'Dias En Cerrar Evento', " +
                               " TIMEDIFF(concat(even.fechacierreevento,' ',even.horacierreevento),concat(even.fecha,' ',even.hora)) as 'Horas', " +
                               " even.ascensorparado, even.personasatrapadas, " +
                               " res.nombre as 'Evento Cerrado Por' " +
                               " from  tb_tipoevento teven, tb_prioridad pri , " +
                               " tb_evento even LEFT JOIN tb_responsable res ON even.cierreeventouser = res.codigo " +
                               " where " +
                               " even.codtipoevento = teven.codigo and even.prioridad = pri.codigo " +
                               " and even.nombreEdificio like '%" + nombreEdificio + "%' " +
                               " and even.semana like '%" + NroSemana + "%' " +
                               " and teven.nombre like '%" + nombreTipoEvento + "%' " +
                               " and even.estadoEvento like '%" + estadoEvento + "%' ";
                               
            if(!fechaDesde.Equals("") && !fechaHasta.Equals("")){
                consulta = consulta + " and even.fecha between '"+fechaDesde+"' and '"+fechaHasta+"' ";
            }

            consulta = consulta + " order by (even.codigo) desc";

            if(exportar == false){
                consulta = consulta + " LIMIT 250 ";
            }

            return Devento.getDatos(consulta);        
        }

        public DataSet estadisticaCallCenterNormalTecnicosEvento(int codEvento) {
            string consulta = "select teceven.codeven, teceven.codtec, "+
                               " teceven.hora_asignacion, teceven.hora_llegadaEdificio, teceven.hora_salidaEdificio, "+
                               " TIMEDIFF(teceven.hora_llegadaEdificio, teceven.hora_asignacion) AS 'Tiempo Llega', "+
                               " TIMEDIFF(teceven.hora_salidaEdificio, teceven.hora_llegadaEdificio) AS 'Tiempo Solucion', "+
                               " teceven.estadoEquipo_llegadasalida as 'EstadoLLegadaSalida', "+
                               " teceven.observacion as 'Observacion_del_Tecnico_al_Problema', "+
                               " res.nombre as 'Nombre_del_Tecnico_Asignado', "+
                               " if(teceven.supervisor = 0,'Tecnico','Supervisor') as 'Cargo' "+
                               " from tb_detalle_teceven teceven, tb_responsable res "+ 
                               " where teceven.codtec = res.codigo "+
                               " and teceven.codeven = "+codEvento;            
            return Devento.getDatos(consulta);        
        }


        public DataSet getDatosEventoCallCenter() {
            string consulta = "select t1.nombre as 'Responsable', IFNULL(t1.Abiertos,0) AS 'Pendientes', IFNULL(t2.Cerrados,0) as 'Cerrados' "+
                               " FROM "+
                               " ( "+
                               " select resp.codigo,resp.nombre, count(*) as 'Abiertos' "+
                               " from tb_evento even, tb_responsable resp "+
                               " where even.inicioeventouser = resp.codigo "+
                               " and even.cierreeventouser IS NULL "+
                               " and year(even.fecha) = year(now()) "+
                               " group by even.inicioeventouser "+
                               " ) as t1 "+
                               " LEFT JOIN "+
                               " ( "+
                               " select resp.codigo,resp.nombre, count(*) as 'Cerrados' "+
                               " from tb_evento even, tb_responsable resp "+
                               " where even.cierreeventouser = resp.codigo " +
                               " and year(even.fecha) = year(now()) "+
                               " group by even.cierreeventouser "+
                               " ) as t2 "+
                               " ON "+
                               " t1.codigo = t2.codigo";
            return Devento.getDatos(consulta);        
        }


        public DataSet getEventoConAscensorParado(string fecha1, string fecha2)
        {
            string consulta = "select "+
                               " even.semana, "+
                               " date_format(even.fecha,'%d/%m/%Y') as 'FechaEvento', " +
                               " even.nombreEdificio, even.dirEdificio, " +
                               " count(*) as 'cantparadoDia' "+
                               " from tb_evento even "+ 
                               " where "+
                               " even.ascensorparado = true and "+
                               " even.fecha between "+fecha1+" and "+fecha2+" "+
                               " group by even.fecha,even.nombreEdificio "+
                               " having count(*) > 1";
            return Devento.getDatos(consulta);   
        }

        public DataSet getEventoConAscensorParado2(string fecha1, string fecha2, string unidadNegocio)
        {
            string consulta = "select "+
                               " '"+unidadNegocio+"' as 'Unidad_Negocio', "+
                               " t1.cantparadoDia, "+
                               " even.nombreEdificio as 'Edificio', "+
                               " even.dirEdificio, "+
                               " even.semana,   "+
                               " date_format(even.fecha,'%d/%m/%Y') as 'FechaEvento', "+
                               " even.codigo as 'Ticket',   "+
                               " even.hora, "+
                               " even.cliente, "+
                               " even.ascensor, "+
                               " even.ascensorparado, "+
                               " even.Observacion as 'Observacion_Evento', "+
                               " even.observacion_evento 'Observacion_CierreEvento', "+
                               " tt.nombre as 'TipoEvento', "+
                               " even.estadoEvento, "+
                               " res1.nombre as 'InicioEvento', "+
                               " res2.nombre as 'CierreEvento' "+
                               " from  tb_tipoevento tt ,  "+
                               " tb_evento even "+
                               " LEFT JOIN "+
                               " tb_responsable res1 ON (even.inicioeventouser = res1.codigo) "+
                               " LEFT JOIN  "+
                               " tb_responsable res2 ON  (even.cierreeventouser = res2.codigo)  "+
                               " LEFT JOIN  "+
                               " ( "+
                               " select  "+
                               " even.codEdificio, "+
                               " even.fecha, "+
                               " count(*) as 'cantparadoDia'  "+
                               " from tb_evento even  "+
                               " where "+
                               " even.ascensorparado = true and  "+
                               " even.fecha between "+fecha1+" and "+fecha2+
                               " group by even.fecha,even.nombreEdificio  "+
                               " having count(*) > 1 "+
                               " )t1 ON (even.codEdificio = t1.codEdificio) "+
                               " where "+
                               " even.fecha = t1.fecha and "+
                               " even.codtipoevento = tt.codigo and "+
                               " even.ascensorparado = true and "+
                               " even.fecha between "+fecha1+" and "+fecha2+
                               " order by even.fecha asc ";
            return Devento.getDatos(consulta);
        }


        public DataSet getInformeDeAtencionCallCenter(string fecha1, string fecha2, string baseDeDatos, string Edificio)
        {
            string consulta = "select "+
                                   " even.codigo as 'Ticket', "+
                                   " even.semana, '"+baseDeDatos+"' as Unidad_de_Negocio, "+
                                   " date_format(even.fecha,'%d/%m/%Y') as 'FechaEvento', "+
                                   " even.hora, "+
                                   " even.cliente as 'nombre_cliente', "+
                                   " even.nombreEdificio as 'nombre_edificio', even.ascensor as 'Ascensor Parado', "+
                                   " dtv.ascensorreparado as 'Ascensor Reparado Tecnico', "+
                                   " IF(even.ascensorparado=1,'Si','No') as 'Ascensor_Parado', "+
                                   " IF(even.personasatrapadas=1,'Si','No') as 'Persona_Parada',  "+
                                   " even.Observacion as '__________Observacion_Evento__________', "+
                                   //---------------nuevo-----------------
                                   " t1.nombre as 'Tec_Asignado_Sis', "+
                                   " t2.nombre as 'Supervisor_Sis', "+
                                   " t3.nombre as 'Rin_Sis', "+
                                   //-------------end nuevo---------------
                                   " resp.nombre as '___Tecnico_Asignado___', "+
                                   " date_format(dtv.fecha_asignacion,'%d/%m/%Y') as 'fecha_Asignacion', "+
                                   " dtv.hora_asignacion, "+
                                   " date_format(dtv.fecha_llegadaedificio,'%d/%m/%Y') as 'fecha_llegadaEdificio', "+
                                   " dtv.hora_llegadaEdificio, "+
                                   " date_format(dtv.fecha_salidaedificio,'%d/%m/%Y') as 'fecha_salidaEdificio', "+
                                   " dtv.hora_salidaEdificio, "+
                                   " date_format(dtv.fecha_horareporte,'%d/%m/%Y') as 'fecha_horareporte', "+
                                   " dtv.hora_reporte, "+
                                   " TIMEDIFF(TIMESTAMP(dtv.fecha_llegadaedificio,dtv.hora_llegadaEdificio),TIMESTAMP(dtv.fecha_asignacion, dtv.hora_asignacion)) AS 'Tiempo_LLegada', "+
                                   " TIMEDIFF(TIMESTAMP(dtv.fecha_salidaedificio,dtv.hora_salidaEdificio),TIMESTAMP(dtv.fecha_llegadaedificio, dtv.hora_llegadaEdificio)) AS 'Tiempo_Solucion', "+
                                   " TIMEDIFF(TIMESTAMP(dtv.fecha_horareporte,dtv.hora_reporte),TIMESTAMP(dtv.fecha_salidaedificio, dtv.hora_salidaEdificio)) AS 'Tiempo_Reporte', "+
                                   " dtv.nroboleta, "+
                                   " dtv.estadoEquipo_llegadasalida as 'Estado_LLegada_Salida', "+
                                   " dtv.observacion as '__________Observacion_del_Tecnico_al_Problema__________', "+
                                   " IF(dtv.supervisor = 1, 'Supervisor', 'Tecnico') as 'Supervisor_Tecnico', "+
                                   " even.observacion_evento as '__________Observacion_Cierre_Evento__________', "+ 
                                   " tipo.nombre as 'TipoEvento', "+
                                   " even.estadoEvento, "+
                                   " res1.nombre as 'InicioEvento', "+
                                   " res2.nombre as 'CierreEvento', "+
                                   " '1' as 'Total' "+
                                   " from  "+
                                   " tb_tipoevento tipo, "+
                                   " tb_evento even "+
                                   " left join tb_detalle_teceven dtv on even.codigo = dtv.codeven "+
                                   " left join tb_responsable resp on dtv.codtec = resp.codigo "+
                                   " left join tb_responsable res1 on (even.inicioeventouser = res1.codigo) "+
                                   " left join tb_responsable res2 on (even.cierreeventouser = res2.codigo) "+
                                   //----------------nuevo-------------------
                                   " LEFT JOIN "+
                                   " ( "+
                                   " select eqaux.cod_proyecto, resaux.nombre  "+
                                   " from tb_equipo eqaux, tb_responsable resaux "+
                                   " where  "+
                                   " eqaux.cod_tecmantenimiento = resaux.codigo "+
                                   " group by eqaux.cod_proyecto "+
                                   " ) AS t1 ON (t1.cod_proyecto = even.codEdificio ) "+
                                   " LEFT JOIN "+
                                   " ( "+
                                   " select eqaux1.cod_proyecto, resaux1.nombre "+
                                   " from tb_equipo eqaux1, tb_responsable resaux1 "+
                                   " where  "+
                                   " eqaux1.cod_supervisor = resaux1.codigo "+
                                   " group by eqaux1.cod_proyecto "+
                                   " ) AS t2 ON (t2.cod_proyecto = even.codEdificio) "+
                                   " LEFT JOIN "+
                                   " ( "+
                                   " select eqaux2.cod_proyecto, resaux2.nombre "+
                                   " from tb_equipo eqaux2, tb_responsable resaux2 "+
                                   " where  "+
                                   " eqaux2.cod_rin = resaux2.codigo "+
                                   " group by eqaux2.cod_proyecto "+
                                   " ) AS t3 ON (t3.cod_proyecto = even.codEdificio) "+
                                   //----------------end nuevo---------------
                                   " where "+
                                   " even.codtipoevento = tipo.codigo and "+
                                   " even.nombreEdificio like '%"+Edificio+"%' and "+
                                   " even.fecha between " + fecha1 + " and " + fecha2 +
                                   " order by even.nombreEdificio,even.fecha desc ;";
                                   
            return Devento.getDatos(consulta);
        }


        public DataSet getInformeDeAtencionCallCenterConsulta1(string baseDeDatos,string fechafija, string nombreEdificio)
        {
            string consulta = "select "+
                               " even.codigo as 'Ticket', "+
                               " even.semana, '" +baseDeDatos+"' as Unidad_de_Negocio, "+
                               " date_format(even.fecha,'%d/%m/%Y') as 'FechaEvento', "+
                               " even.hora,  "+
                               " even.cliente as 'nombre_cliente', "+
                               " even.nombreEdificio as 'nombre_edificio', even.ascensor, even.ascensorparado,  " +
                               " even.Observacion as '__________Observacion_Evento__________', "+ 
                               " even.observacion_evento as '__________Observacion_Cierre_Evento__________', "+
                               " tipo.nombre as 'TipoEvento', "+
                               " even.estadoEvento, "+
                               " res1.nombre as 'InicioEvento', "+
                               " res2.nombre as 'CierreEvento', "+
                               " '1' as 'Total' "+
                               " from  "+
                               " tb_tipoevento tipo, "+
                               " tb_evento even "+
                               " left join tb_responsable res1 on (even.inicioeventouser = res1.codigo) "+
                               " left join tb_responsable res2 on (even.cierreeventouser = res2.codigo) "+
                               " where "+
                               " even.codtipoevento = tipo.codigo and "+
                               " even.ascensorparado = 1 and "+
                               " even.fecha = '"+fechafija+"' and "+
                               " even.nombreEdificio = '"+nombreEdificio+"' ";

            return Devento.getDatos(consulta);
        }


        public int get_CantidadEventosMesAnioBaseDatos(string fechaInicio, string fechaFin, string TiposEventos, string BaseDatos) {
            string consulta = "select count(*) "+
                               " from "+BaseDatos+"tb_evento even "+
                               " where "+
                               " even.codtipoevento in ("+TiposEventos+") and "+
                               " even.fecha between '"+fechaInicio+"' and '"+fechaFin+"' ";
            DataSet dato = Devento.getDatos(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return 0;
        }

        public DataSet getDatos_CantidadEventosMesAnioBaseDatos(string fechaInicio, string fechaFin, string TiposEventos, string BaseDatos)
        {
            string consulta = "select " +
                               " even.codigo as 'Ticket', " +
                                   " even.semana, " +
                                   " date_format(even.fecha,'%d/%m/%Y') as 'FechaEvento', " +
                                   " even.hora, " +
                                   " even.cliente as 'nombre_cliente', " +
                                   " even.nombreEdificio as 'nombre_edificio', " +
                                   " even.Observacion as '__________Observacion_Evento__________', " +
                                   " even.observacion_evento as '__________Observacion_Cierre_Evento__________', " +
                                   " tipo.nombre as 'TipoEvento', " +
                                   " even.estadoEvento, " +
                                   " res1.nombre as 'InicioEvento', " +
                                   " res2.nombre as 'CierreEvento' " +
                                   " from  " +                                   
                                   BaseDatos + "tb_tipoevento tipo, " +
                                   BaseDatos + "tb_evento even " +
                                   " left join "+BaseDatos+"tb_responsable res1 on (even.inicioeventouser = res1.codigo) " +
                                   " left join "+BaseDatos+"tb_responsable res2 on (even.cierreeventouser = res2.codigo) " +
                               " where " +                               
                               " even.codtipoevento = tipo.codigo and " +
                               " even.codtipoevento in (" + TiposEventos + ") and " +
                               " even.fecha between '" + fechaInicio + "' and '" + fechaFin + "' ";
            DataSet dato = Devento.getDatos(consulta);            
            return dato;
        }

        



        public int get_CantidadEventosCerradoMesAnioBaseDatos(string fechaInicio, string fechafin, string TipoEvento, string baseDatos)
        {
            string consulta = "select count(*) "+
                               " from "+baseDatos+"tb_evento even  "+
                               " where  "+
                               " even.estadoEvento = 'Cerrado' and "+
                               " even.codtipoevento in (" + TipoEvento + ") and " +
                               " even.fecha between '"+fechaInicio+"' and '"+fechafin+"'";
            DataSet dato = Devento.getDatos(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return 0;
        }

        public DataSet getDatos_CantidadEventosCerradoMesAnioBaseDatos(string fechaInicio, string fechafin, string TipoEvento, string baseDatos)
        {
            string consulta = "select " +
                               " even.codigo as 'Ticket', " +
                                   " even.semana, " +
                                   " date_format(even.fecha,'%d/%m/%Y') as 'FechaEvento', " +
                                   " even.hora, " +
                                   " even.cliente as 'nombre_cliente', " +
                                   " even.nombreEdificio as 'nombre_edificio', " +
                                   " even.Observacion as '__________Observacion_Evento__________', " +                                   
                                   " even.observacion_evento as '__________Observacion_Cierre_Evento__________', " +
                                   " tipo.nombre as 'TipoEvento', " +
                                   " even.estadoEvento, " +
                                   " res1.nombre as 'InicioEvento', " +
                                   " res2.nombre as 'CierreEvento' " +
                                   " from  " +                                   
                                   baseDatos + "tb_tipoevento tipo, " +
                                   baseDatos + "tb_evento even " +
                                   " left join " + baseDatos + "tb_responsable res1 on (even.inicioeventouser = res1.codigo) " +
                                   " left join " + baseDatos + "tb_responsable res2 on (even.cierreeventouser = res2.codigo) " +
                               " where  " +                              
                               " even.codtipoevento = tipo.codigo and " +
                               " even.estadoEvento = 'Cerrado' and " +
                               " even.codtipoevento in (" + TipoEvento + ") and " +
                               " even.fecha between '" + fechaInicio + "' and '" + fechafin + "'";
            DataSet dato = Devento.getDatos(consulta);            
                return dato;
        }

        public int get_CantidadEventosAbiertoMesAnioBaseDatos(string fechaInicio, string fechafin, string TipoEvento, string baseDatos)
        {
            string consulta = "select count(*) " +
                               " from " + baseDatos + "tb_evento even  " +
                               " where  " +
                               " even.estadoEvento = 'Abierto' and " +
                               " even.codtipoevento in (" + TipoEvento + ") and " +
                               " even.fecha between '" + fechaInicio + "' and '" + fechafin + "'";
            DataSet dato = Devento.getDatos(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return 0;
        }

        public DataSet getDatos_CantidadEventosAbiertoMesAnioBaseDatos(string fechaInicio, string fechafin, string TipoEvento, string baseDatos)
        {
            string consulta = "select " +
                               " even.codigo as 'Ticket', " +
                                   " even.semana, " +
                                   " date_format(even.fecha,'%d/%m/%Y') as 'FechaEvento', " +
                                   " even.hora, " +
                                   " even.cliente as 'nombre_cliente', " +
                                   " even.nombreEdificio as 'nombre_edificio', " +
                                   " even.Observacion as '__________Observacion_Evento__________', " +                                                                      
                                   " even.observacion_evento as '__________Observacion_Cierre_Evento__________', " +
                                   " tipo.nombre as 'TipoEvento', " +
                                   " even.estadoEvento, " +
                                   " res1.nombre as 'InicioEvento', " +
                                   " res2.nombre as 'CierreEvento' " +
                                   " from  " +                                   
                                   baseDatos + "tb_tipoevento tipo, " +
                                   baseDatos + "tb_evento even " +
                                   " left join " + baseDatos + "tb_responsable res1 on (even.inicioeventouser = res1.codigo) " +
                                   " left join " + baseDatos + "tb_responsable res2 on (even.cierreeventouser = res2.codigo) " +
                               " where  " +                               
                               " even.codtipoevento = tipo.codigo and " +
                               " even.estadoEvento = 'Abierto' and " +
                               " even.codtipoevento in (" + TipoEvento + ") and " +
                               " even.fecha between '" + fechaInicio + "' and '" + fechafin + "'";
            DataSet dato = Devento.getDatos(consulta);
            return dato;
        }



        public int get_cantidadEventosEstadoBaseDatos_RQS(int mes, int anio,string estado, string DB) {
            string consulta = "select count(*)  "+
                               " from "+DB+"tb_evento even "+
                               " where "+
                               " month(even.fecha) = "+mes+" and "+
                               " year(even.fecha) = "+anio+" and "+
                               " even.estadoEvento = '"+estado+"' and "+
                               " even.codtipoevento in (6,2,3,4)";
            DataSet dato = Devento.getDatos(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return 0;
        }

        public int get_cantidadEstadoBaseDatos_REX(int mes, int anio, string estado, string DB)
        {
            string consulta = "select count(*)  " +
                               " from " + DB + "tb_evento even " +
                               " where " +
                               " month(even.fecha) = " + mes + " and " +
                               " year(even.fecha) = " + anio + " and " +
                               " even.estadoEvento = '" + estado + "' and " +
                               " even.codtipoevento = 5 ";
            DataSet dato = Devento.getDatos(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return 0;
        }


       public int get_cantidadEventosTotalBaseDatos_RQS(int mes, int anio, string DB)
        {
            string consulta = "select count(*)  " +
                               " from " + DB + "tb_evento even " +
                               " where " +
                               " month(even.fecha) = " + mes + " and " +
                               " year(even.fecha) = " + anio + " and " +                               
                               " even.codtipoevento in (6,2,3,4)";
            DataSet dato = Devento.getDatos(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return 0;
        }


       public int get_cantidadTotalBaseDatos_REX(int mes, int anio, string DB)
       {
           string consulta = "select count(*)  " +
                              " from " + DB + "tb_evento even " +
                              " where " +
                              " month(even.fecha) = " + mes + " and " +
                              " year(even.fecha) = " + anio + " and " +
                              " even.codtipoevento = 5";
           DataSet dato = Devento.getDatos(consulta);
           if (dato.Tables[0].Rows.Count > 0)
           {
               return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
           }
           else
               return 0;
       }


       private string get_PromedioAtencionEmergenciaBaseDatos(int mes, int anio, string DB)
       {
         /*  string consulta = "select "+
                               " sec_to_time(avg(time_to_sec(TIMEDIFF(dtv.hora_llegadaEdificio,dtv.hora_asignacion)))) "+
                               " from "+DB+"tb_evento even, "+
                               " "+DB+"tb_detalle_teceven dtv "+
                               " where "+
                               " even.codigo = dtv.codeven and "+
                               " even.codtipoevento = 1 and "+
                               " even.cambiorepuesto = 0 and "+
                               " even.solicitudrepuestobandera = 0 and "+
                               " month(even.fecha) = "+mes+" and "+
                               " year(even.fecha) = "+anio;   */
           string consulta = "select sec_to_time(avg(time_to_sec(TIMEDIFF(TIMESTAMP(dtv.fecha_llegadaedificio,dtv.hora_llegadaEdificio),TIMESTAMP(dtv.fecha_asignacion, dtv.hora_asignacion))))) "+
                              " from " + DB + "tb_evento even, " +
                               " " + DB + "tb_detalle_teceven dtv " +
                               " where "+
                               " even.codigo = dtv.codeven and "+
                               " even.codtipoevento = 1 and "+                               
                               " dtv.supervisor = 0 and "+
                               " dtv.trabajoprogramado = 0 and "+
                               " dtv.hora_asignacion IS not NULL and " +
                               " dtv.hora_asignacion > '08:00:00' and dtv.hora_asignacion < '18:00:00' and "+
                               " month(even.fecha) = " + mes + " and " +
                               " year(even.fecha) = " + anio;   
           DataSet dato = Devento.getDatos(consulta);
           if (dato.Tables[0].Rows.Count > 0)
           {
               if (!dato.Tables[0].Rows[0][0].ToString().Equals(""))
               {
                   return dato.Tables[0].Rows[0][0].ToString();
               }else
                   return "00:00:00";
           }
           else
               return "00:00:00";
       }



       private string get_PromedioAtencionEmergenciaBaseDatos_NOCHE(int mes, int anio, string DB)
       {        
           string consulta = "SELECT "+
                               " sec_to_time(avg(time_to_sec(T1.diferencia))) "+
                               " FROM "+
                               " ( "+
                               " select "+
                               " TIMEDIFF(TIMESTAMP(dtv.fecha_llegadaedificio,dtv.hora_llegadaEdificio),TIMESTAMP(dtv.fecha_asignacion, dtv.hora_asignacion)) as diferencia "+
                               " from " + DB + "tb_evento even, " +
                               " " + DB + "tb_detalle_teceven dtv " +
                               " where  "+
                               " even.codigo = dtv.codeven and  "+
                               " even.codtipoevento = 1 and  "+
                               " dtv.supervisor = 0 and "+
                               " dtv.trabajoprogramado = 0 and "+
                               " dtv.hora_asignacion IS not NULL and "+
                               " dtv.hora_asignacion > '18:00:00' and dtv.hora_asignacion < '23:59:59' and "+
                               " month(even.fecha) = " + mes + " and " +
                               " year(even.fecha) = " + anio+ 
                               " UNION "+
                               " select  "+
                               " TIMEDIFF(TIMESTAMP(dtv.fecha_llegadaedificio,dtv.hora_llegadaEdificio),TIMESTAMP(dtv.fecha_asignacion, dtv.hora_asignacion)) as diferencia "+
                                " from " + DB + "tb_evento even, " +
                               " " + DB + "tb_detalle_teceven dtv " +
                               " where "+
                               " even.codigo = dtv.codeven and "+
                               " even.codtipoevento = 1 and "+
                               " dtv.supervisor = 0 and "+
                               " dtv.trabajoprogramado = 0 and "+
                               " dtv.hora_asignacion IS not NULL and "+
                               " dtv.hora_asignacion > '00:00:01' and dtv.hora_asignacion < '08:00:00' and "+
                               " month(even.fecha) = " + mes + " and " +
                               " year(even.fecha) = " + anio+
                               " ) AS T1";
           DataSet dato = Devento.getDatos(consulta);
           if (dato.Tables[0].Rows.Count > 0)
           {
               if (!dato.Tables[0].Rows[0][0].ToString().Equals(""))
               {
                   return dato.Tables[0].Rows[0][0].ToString();
               }
               else
                   return "00:00:00";
           }
           else
               return "00:00:00";
       }


       private string get_PromedioAtencionEmergenciaBaseDatos_AscensorParado(int mes, int anio, string DB)
       {
         /*  string consulta = "select " +
                               " sec_to_time(avg(time_to_sec(TIMEDIFF(dtv.hora_salidaEdificio,dtv.hora_llegadaEdificio)))) " +
                               " from " + DB + "tb_evento even, " +
                               " " + DB + "tb_detalle_teceven dtv " +
                               " where " +
                               " even.codigo = dtv.codeven and " +
                               " even.codtipoevento = 1 and " +
                               " even.cambiorepuesto = 0 and " +
                               " even.solicitudrepuestobandera = 0 and " +
                               " even.ascensorparado = 1 and "+
                               " month(even.fecha) = " + mes + " and " +
                               " year(even.fecha) = " + anio;   */
           string consulta = "select "+
                               " sec_to_time(avg(time_to_sec(TIMEDIFF(TIMESTAMP(dtv.fecha_salidaedificio,dtv.hora_salidaEdificio),TIMESTAMP(dtv.fecha_llegadaedificio, dtv.hora_llegadaEdificio))))) "+
                               " from " + DB + "tb_evento even, " +
                               " " + DB + "tb_detalle_teceven dtv " +
                               " where  "+
                               " even.codigo = dtv.codeven and  "+
                               " even.codtipoevento = 1 and  "+
                               " dtv.supervisor = 0 and "+
                               " dtv.trabajoprogramado = 0 and "+
                               " dtv.hora_llegadaEdificio IS not NULL and "+
                               " dtv.hora_llegadaEdificio > '08:00:00' and dtv.hora_llegadaEdificio < '18:00:00' and "+
                               " month(even.fecha) = "+mes+" and  "+
                               " year(even.fecha) = "+anio;
           DataSet dato = Devento.getDatos(consulta);
           if (dato.Tables[0].Rows.Count > 0)
           {
               if (!dato.Tables[0].Rows[0][0].ToString().Equals(""))
                   return dato.Tables[0].Rows[0][0].ToString();
               else
                   return "00:00:00";
           }
           else
               return "00:00:00";
       }



       private string get_PromedioAtencionEmergenciaBaseDatos_AscensorParado_NOCHE(int mes, int anio, string DB)
       {
         
          /* string consulta = "select " +
                               " sec_to_time(avg(time_to_sec(TIMEDIFF(TIMESTAMP(dtv.fecha_salidaedificio,dtv.hora_salidaEdificio),TIMESTAMP(dtv.fecha_llegadaedificio, dtv.hora_llegadaEdificio))))) " +
                               " from " + DB + "tb_evento even, " +
                               " " + DB + "tb_detalle_teceven dtv " +
                               " where  " +
                               " even.codigo = dtv.codeven and  " +
                               " even.codtipoevento = 1 and  " +
                               " dtv.supervisor = 0 and " +
                               " dtv.trabajoprogramado = 0 and " +
                               " dtv.hora_llegadaEdificio IS not NULL and " +
                               " dtv.hora_llegadaEdificio > '08:00:00' and dtv.hora_llegadaEdificio < '18:00:00' and " +
                               " month(even.fecha) = " + mes + " and  " +
                               " year(even.fecha) = " + anio;   */
           string consulta = "SELECT "+
                               " sec_to_time(avg(time_to_sec(T1.diferencia))) "+
                               " FROM "+
                               " ( "+
                               " select  "+
                               " TIMEDIFF(TIMESTAMP(dtv.fecha_salidaedificio,dtv.hora_salidaEdificio),TIMESTAMP(dtv.fecha_llegadaedificio, dtv.hora_llegadaEdificio)) as diferencia "+
                               " from " + DB + "tb_evento even, " +
                               " " + DB + "tb_detalle_teceven dtv " +
                               " where "+
                               " even.codigo = dtv.codeven and "+
                               " even.codtipoevento = 1 and "+
                               " dtv.supervisor = 0 and "+
                               " dtv.trabajoprogramado = 0 and "+
                               " dtv.hora_llegadaEdificio IS not NULL and "+
                               " dtv.hora_llegadaEdificio > '18:00:00' and dtv.hora_llegadaEdificio < '23:59:59' and "+
                               " month(even.fecha) = " + mes + " and  " +
                               " year(even.fecha) = " + anio+
                               " UNION "+
                               " select "+
                               " TIMEDIFF(TIMESTAMP(dtv.fecha_salidaedificio,dtv.hora_salidaEdificio),TIMESTAMP(dtv.fecha_llegadaedificio, dtv.hora_llegadaEdificio)) as diferencia "+
                               " from " + DB + "tb_evento even, " +
                               " " + DB + "tb_detalle_teceven dtv " +
                               " where "+
                               " even.codigo = dtv.codeven and "+
                               " even.codtipoevento = 1 and "+
                               " dtv.supervisor = 0 and "+
                               " dtv.trabajoprogramado = 0 and "+
                               " dtv.hora_llegadaEdificio IS not NULL and "+
                               " dtv.hora_llegadaEdificio > '00:00:01' and dtv.hora_llegadaEdificio < '08:00:00' and "+
                               " month(even.fecha) = " + mes + " and  " +
                               " year(even.fecha) = " + anio+
                               " ) AS T1";
           DataSet dato = Devento.getDatos(consulta);
           if (dato.Tables[0].Rows.Count > 0)
           {
               if (!dato.Tables[0].Rows[0][0].ToString().Equals(""))
                   return dato.Tables[0].Rows[0][0].ToString();
               else
                   return "00:00:00";
           }
           else
               return "00:00:00";
       }


        public DataSet get_Trimestral_Eventos(string fecha1, string fecha2)
        {

            DateTime fecha1_aux = Convert.ToDateTime(fecha1);
            DateTime fecha2_aux = Convert.ToDateTime(fecha2);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);

            dt.Columns.Add("Mes");
            dt.Columns.Add("Nro_RQS");
            dt.Columns.Add("Reclamos");
            dt.Columns.Add("Quejas");
            dt.Columns.Add("Sugerencia");
            dt.Columns.Add("Otras_Solicitudes");
            dt.Columns.Add("Cerrado");
            dt.Columns.Add("Abierto");
            dt.Columns.Add("Grado_Cumplimiento");

            int diaDesde = fecha1_aux.Day;
            int mesDesde = fecha1_aux.Month;
            int anioDesde = fecha1_aux.Year;

            int diaHasta = fecha2_aux.Day;
            int mesHasta = fecha2_aux.Month;
            int anioHasta = fecha2_aux.Year;


            //-------------------Santa Cruz--------------
            DataRow filaS = dt.NewRow();
            filaS["Mes"] = "Santa Cruz";
            filaS["Nro_RQS"] = "";
            filaS["Reclamos"] = "";
            filaS["Quejas"] = "";
            filaS["Sugerencia"] = "";
            filaS["Otras_Solicitudes"] = "";
            filaS["Cerrado"] = "";
            filaS["Abierto"] = "";
            dt.Rows.Add(filaS);

            bool primeraVes = true;  ////para activar la primera ves que del primer mes

            for (int i = anioDesde; i <= anioHasta; i++)
            {
                if (i == anioHasta)
                {
                 mesHasta = fecha2_aux.Month;
                }else
                    mesHasta = 12;
                primeraVes = true;
                    for (int j = mesDesde; j <= mesHasta; j++)
                    {
                        ///-----------------busqueda Inicio y Fin---------------------
                        string FechaInicio = i+"-"+j;
                        if(primeraVes){
                            FechaInicio = FechaInicio + "-"+ diaDesde;
                            primeraVes = false;
                        }else
                            FechaInicio = FechaInicio + "-01";

                        string datoF = i + "-" + j  + "-01";
                        DateTime fechaConver = Convert.ToDateTime(datoF);
                        fechaConver = fechaConver.AddMonths(1);
                        DateTime fechaFinAux =  fechaConver.AddDays(-1);
                        
                        string fechaFin = i + "-" + j;
                        if (j == mesHasta && i == anioHasta)
                        {
                            fechaFin = fechaFin + "-" + diaHasta;
                        }
                        else
                            fechaFin = fechaFinAux.ToString("yyyy-MM-dd");
                       
                        //----------------------------------------------


                        DateTime faux = new DateTime(anioHasta, j, 1);
                        DataRow fila = dt.NewRow();
                        fila["Mes"] = faux.ToString("MMMM")+"-"+i;
                        int cant_eventos = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientoscz_jyc.");
                        fila["Nro_RQS"] = cant_eventos;

                        fila["Reclamos"] = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "2", "db_seguimientoscz_jyc.");
                        fila["Quejas"] = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "3", "db_seguimientoscz_jyc.");
                        fila["Sugerencia"] = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "4", "db_seguimientoscz_jyc.");
                        fila["Otras_Solicitudes"] = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "6", "db_seguimientoscz_jyc.");
                        
                        int cant_eventoCerrado = get_CantidadEventosCerradoMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientoscz_jyc.");
                        fila["Cerrado"] = cant_eventoCerrado;
                        int cant_eventoAbierto = get_CantidadEventosAbiertoMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientoscz_jyc.");
                        fila["Abierto"] = cant_eventoAbierto;
                        if (cant_eventos == 0)
                        {
                            fila["Grado_Cumplimiento"] = "0 %";
                        }
                        else
                            fila["Grado_Cumplimiento"] = (cant_eventoCerrado * 100) / cant_eventos + " %";
                        dt.Rows.Add(fila);

                    }
                    mesDesde = 1;
            }
            //-----------------end Santa Cruz
            //------------------begin cochabamba

             mesDesde = fecha1_aux.Month;
             anioDesde = fecha1_aux.Year;
             mesHasta = fecha2_aux.Month;
             anioHasta = fecha2_aux.Year;

            DataRow filaC = dt.NewRow();
            filaC["Mes"] = "Cochabamba";
            filaC["Nro_RQS"] = "";
            filaC["Reclamos"] = "";
            filaC["Quejas"] = "";
            filaC["Sugerencia"] = "";
            filaC["Otras_Solicitudes"] = "";
            filaC["Cerrado"] = "";
            filaC["Abierto"] = "";
            dt.Rows.Add(filaC);

            for (int i = anioDesde; i <= anioHasta; i++)
            {
               if (i == anioHasta)
                {
                 mesHasta = fecha2_aux.Month;
                }else
                    mesHasta = 12;
               primeraVes = true;
                    for (int j = mesDesde; j <= mesHasta; j++)
                    {

                        ///-----------------busqueda Inicio y Fin---------------------
                        string FechaInicio = i + "-" + j;
                        if (primeraVes)
                        {
                            FechaInicio = FechaInicio + "-" + diaDesde;
                            primeraVes = false;
                        }
                        else
                            FechaInicio = FechaInicio + "-01";

                        string datoF = i + "-" + j + "-01";
                        DateTime fechaConver = Convert.ToDateTime(datoF);
                        fechaConver = fechaConver.AddMonths(1);
                        DateTime fechaFinAux = fechaConver.AddDays(-1);

                        string fechaFin = i + "-" + j;
                        if (j == mesHasta && i == anioHasta)
                        {
                            fechaFin = fechaFin + "-" + diaHasta;
                        }
                        else
                            fechaFin = fechaFinAux.ToString("yyyy-MM-dd");

                        //----------------------------------------------

                        DateTime faux = new DateTime(anioHasta, j, 1);
                        DataRow fila = dt.NewRow();
                        fila["Mes"] = faux.ToString("MMMM") + "-" + i;
                        int cant_eventos = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientocbba_jyc.");
                        fila["Nro_RQS"] = cant_eventos;
                       
                        fila["Reclamos"] = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "2", "db_seguimientocbba_jyc.");
                        fila["Quejas"] = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "3", "db_seguimientocbba_jyc.");
                        fila["Sugerencia"] = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "4", "db_seguimientocbba_jyc.");
                        fila["Otras_Solicitudes"] = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "6", "db_seguimientocbba_jyc.");
                        
                        int cant_eventoCerrado = get_CantidadEventosCerradoMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientocbba_jyc.");
                        fila["Cerrado"] = cant_eventoCerrado;
                        int cant_eventoAbierto = get_CantidadEventosAbiertoMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientocbba_jyc.");
                        fila["Abierto"] = cant_eventoAbierto;
                        if (cant_eventos == 0)
                        {
                            fila["Grado_Cumplimiento"] = "0 %";
                        }
                        else
                            fila["Grado_Cumplimiento"] = (cant_eventoCerrado * 100) / cant_eventos + " %";
                        dt.Rows.Add(fila);

                    }
                    mesDesde = 1;                
            }

            //------------------end cochabamba
            //------------------begin la paz
            mesDesde = fecha1_aux.Month;
            anioDesde = fecha1_aux.Year;
            mesHasta = fecha2_aux.Month;
            anioHasta = fecha2_aux.Year;

            DataRow filaL = dt.NewRow();
            filaL["Mes"] = "La Paz";
            filaL["Nro_RQS"] = "";
            filaL["Reclamos"] = "";
            filaL["Quejas"] = "";
            filaL["Sugerencia"] = "";
            filaL["Otras_Solicitudes"] = "";
            filaL["Cerrado"] = "";
            filaL["Abierto"] = "";
            dt.Rows.Add(filaL);

            for (int i = anioDesde; i <= anioHasta; i++)
            {
                if (i == anioHasta)
                {
                    mesHasta = fecha2_aux.Month;
                }
                else
                    mesHasta = 12;
                primeraVes = true;
              for (int j = mesDesde; j <= mesHasta; j++)
                    {

                        ///-----------------busqueda Inicio y Fin---------------------
                        string FechaInicio = i + "-" + j;
                        if (primeraVes)
                        {
                            FechaInicio = FechaInicio + "-" + diaDesde;
                            primeraVes = false;
                        }
                        else
                            FechaInicio = FechaInicio + "-01";

                        string datoF = i + "-" + j + "-01";
                        DateTime fechaConver = Convert.ToDateTime(datoF);
                        fechaConver = fechaConver.AddMonths(1);
                        DateTime fechaFinAux = fechaConver.AddDays(-1);

                        string fechaFin = i + "-" + j;
                        if (j == mesHasta && i == anioHasta)
                        {
                            fechaFin = fechaFin + "-" + diaHasta;
                        }
                        else
                            fechaFin = fechaFinAux.ToString("yyyy-MM-dd");

                        //----------------------------------------------

                        DateTime faux = new DateTime(anioHasta, j, 1);
                        DataRow fila = dt.NewRow();
                        fila["Mes"] = faux.ToString("MMMM") + "-" + i;
                        int cant_eventos = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientolpz_jyc.");
                        fila["Nro_RQS"] = cant_eventos;
                        
                        fila["Reclamos"] = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "2", "db_seguimientolpz_jyc.");
                        fila["Quejas"] = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "3", "db_seguimientolpz_jyc.");
                        fila["Sugerencia"] = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "4", "db_seguimientolpz_jyc.");
                        fila["Otras_Solicitudes"] = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "6", "db_seguimientolpz_jyc.");
                  
                        int cant_eventoCerrado = get_CantidadEventosCerradoMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientolpz_jyc.");
                        fila["Cerrado"] = cant_eventoCerrado;
                        int cant_eventoAbierto = get_CantidadEventosAbiertoMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientolpz_jyc.");
                        fila["Abierto"] = cant_eventoAbierto;
                        if (cant_eventos == 0)
                        {
                            fila["Grado_Cumplimiento"] = "0 %";
                        }
                        else
                            fila["Grado_Cumplimiento"] = (cant_eventoCerrado * 100) / cant_eventos + " %";
                        dt.Rows.Add(fila);

                    }
                    mesDesde = 1;
                }             
            
            //------------------end la paz
                        
            return ds;
        }


        public int get_NroEdificiosConMasDe3llamadasEn1Mes_baseDatos(int mes, int anio, string baseDatos) {
            string consulta = "select count(*) "+
                               " from "+
                               " (select  "+
                               " even.nombreEdificio, "+
                               " count(even.nombreEdificio) as 'cantEventoPorMes' "+
                               " from "+baseDatos+"tb_evento even  "+
                               " where  "+
                               " even.codtipoevento = 1 and "+
                               " month(even.fecha) = "+mes+" and "+
                               " year(even.fecha) = "+anio+
                               " group by even.nombreEdificio "+
                               " having count(even.nombreEdificio)>3) as t1";
            DataSet dato = Devento.getDatos(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return 0;

        }


        public DataSet get_CuadroIndicadores(string fecha1, string fecha2)
        {
            DateTime fecha1_aux = Convert.ToDateTime(fecha1);
            DateTime fecha2_aux = Convert.ToDateTime(fecha2);
            
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);

            dt.Columns.Add("Tipo_Informacion");

            int diaDesde = fecha1_aux.Day;
            int mesDesde = fecha1_aux.Month;
            int anioDesde = fecha1_aux.Year;
            int diaHasta = fecha2_aux.Day;
            int mesHasta = fecha2_aux.Month;
            int anioHasta = fecha2_aux.Year;

            //----------------------- reporte RQS ----------------
            DataRow filaS = dt.NewRow();
            filaS["Tipo_Informacion"] = "Nro RQS al Cliente Atendidos // Nro RQS";
            dt.Rows.Add(filaS);
            dt.Rows.Add("INTERLOGY");
            dt.Rows.Add("MELEVAR");
            dt.Rows.Add("ELEVAMERICA");

            int Interlogy_totalRQSCerrado = 0;
            int Interlogy_totalRQS = 0;
            int Melevar_totalRQSCerrado = 0;
            int Melevar_totalRQS = 0;
            int Elevamerica_totalRQSCerrado = 0;
            int Elevamerica_totalRQS = 0;
            //------------------ NUMERO DE REX ------------------
            dt.Rows.Add("NUMERO DE REX");
            dt.Rows.Add("INTERLOGY");
            dt.Rows.Add("MELEVAR");
            dt.Rows.Add("ELEVAMERICA");

            int Interlogy_totalREXCerrado = 0;
            int Interlogy_totalREX = 0;
            int Melevar_totalREXCerrado = 0;
            int Melevar_totalREX = 0;
            int Elevamerica_totalREXCerrado = 0;
            int Elevamerica_totalREX = 0;

            //-------------------- PROMEDIO DE ATENCION DE EMERGENCIA (Dia) ------------
            dt.Rows.Add("PROMEDIO DE TIEMPO DE ATENCION DE LAS EMERGENCIAS (Dia)");
            dt.Rows.Add("INTERLOGY");
            dt.Rows.Add("MELEVAR");
            dt.Rows.Add("ELEVAMERICA");
            TimeSpan sumaHorasAtencionInterlogy = new TimeSpan(0,0,0);
            TimeSpan sumaHorasAtencionMelevar = new TimeSpan(0, 0, 0);
            TimeSpan sumaHorasAtencionElevamerica = new TimeSpan(0, 0, 0);

            //------------------- PROMEDIO DE TIEMPO DE EQUIPO PARADO POR EMERGENCIA (Dia) ------------
            dt.Rows.Add("PROMEDIO DE TIEMPO DE EQUIPO PARADO POR EMERGENCIA (Dia)");
            dt.Rows.Add("INTERLOGY");
            dt.Rows.Add("MELEVAR");
            dt.Rows.Add("ELEVAMERICA");
            TimeSpan sumaHorasEquipoParadoInterlogy = new TimeSpan(0, 0, 0);
            TimeSpan sumaHorasEquipoParadoMelevar = new TimeSpan(0, 0, 0);
            TimeSpan sumaHorasEquipoParadoElevamerica = new TimeSpan(0, 0, 0);

            //------------------- NUMERO DE ETV'S CON MAS DE TRES EN UN MES ------------
            dt.Rows.Add("NUMERO DE ETV'S CON MAS DE TRES EN UN MES");
            dt.Rows.Add("INTERLOGY");
            dt.Rows.Add("MELEVAR");
            dt.Rows.Add("ELEVAMERICA");
            int sumaInterlogy = 0;
            int sumaMelevar = 0;
            int sumaElevamerica = 0;
            int cantInterlogy = 0;
            int cantMelevar = 0;
            int cantElevamerica = 0;
            bool primeraVes = true;

            //-------------------- PROMEDIO DE ATENCION DE EMERGENCIA (Noche) ------------
            dt.Rows.Add("PROMEDIO DE TIEMPO DE ATENCION DE LAS EMERGENCIAS (Noche)");
            dt.Rows.Add("INTERLOGY");
            dt.Rows.Add("MELEVAR");
            dt.Rows.Add("ELEVAMERICA");
            TimeSpan sumaHorasAtencionInterlogyNoche = new TimeSpan(0, 0, 0);
            TimeSpan sumaHorasAtencionMelevarNoche = new TimeSpan(0, 0, 0);
            TimeSpan sumaHorasAtencionElevamericaNoche = new TimeSpan(0, 0, 0);

            //------------------- PROMEDIO DE TIEMPO DE EQUIPO PARADO POR EMERGENCIA (Noche) ------------
            dt.Rows.Add("PROMEDIO DE TIEMPO DE EQUIPO PARADO POR EMERGENCIA (Noche)");
            dt.Rows.Add("INTERLOGY");
            dt.Rows.Add("MELEVAR");
            dt.Rows.Add("ELEVAMERICA");
            TimeSpan sumaHorasEquipoParadoInterlogyNoche = new TimeSpan(0, 0, 0);
            TimeSpan sumaHorasEquipoParadoMelevarNoche = new TimeSpan(0, 0, 0);
            TimeSpan sumaHorasEquipoParadoElevamericaNoche = new TimeSpan(0, 0, 0);



            for (int i = anioDesde; i <= anioHasta; i++)
            {
                if (i == anioHasta)
                {
                    mesHasta = fecha2_aux.Month;
                }
                else
                mesHasta = 12;
                primeraVes = true;

                    for (int j = mesDesde; j <= mesHasta; j++)
                    {
                        DateTime faux = new DateTime(anioHasta, j, 1);
                        dt.Columns.Add(faux.ToString("MMMM") + "-" + i);
                        
                        ///-----------------busqueda Inicio y Fin---------------------
                        string FechaInicio = i + "-" + j;
                        if (primeraVes)
                        {
                            FechaInicio = FechaInicio + "-" + diaDesde;
                            primeraVes = false;
                        }
                        else
                            FechaInicio = FechaInicio + "-01";

                        string datoF = i + "-" + j + "-01";
                        DateTime fechaConver = Convert.ToDateTime(datoF);
                        fechaConver = fechaConver.AddMonths(1);
                        DateTime fechaFinAux = fechaConver.AddDays(-1);

                        string fechaFin = i + "-" + j;
                        if (j == mesHasta && i == anioHasta)
                        {
                            fechaFin = fechaFin + "-" + diaHasta;
                        }
                        else
                            fechaFin = fechaFinAux.ToString("yyyy-MM-dd");

                        //----------------------------------------------
                        
                        //-----------interlogy RQS
                        // int cant_eventoCerrado = get_cantidadEventosEstadoBaseDatos_RQS(j, i,"Cerrado","db_seguimientoscz_jyc.");
                        //int cant_eventoCerrado = get_cantidadTotalBaseDatos_RQS(j, i, "Cerrado", "db_seguimientoscz_jyc.");
                        int cant_eventoCerrado = get_CantidadEventosCerradoMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientoscz_jyc.");
                        int cant_eventoTotal = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientoscz_jyc.");

                        Interlogy_totalRQS = Interlogy_totalRQS + cant_eventoTotal;
                        Interlogy_totalRQSCerrado = Interlogy_totalRQSCerrado + cant_eventoCerrado;

                        if (cant_eventoTotal == 0)
                        {
                            dt.Rows[1][faux.ToString("MMMM") + "-" + i] = "0//0";
                        }
                        else
                            dt.Rows[1][faux.ToString("MMMM") + "-" + i] = cant_eventoCerrado + "//" + cant_eventoTotal; 

                        //----------------------- interlogy numero de Rex ---------------
                       // int cant_RexCerrado = get_cantidadEstadoBaseDatos_REX(j, i, "Cerrado", "db_seguimientoscz_jyc.");
                       // int cant_RexTotal = get_cantidadTotalBaseDatos_REX(j, i, "db_seguimientoscz_jyc.");

                        int cant_RexCerrado = get_CantidadEventosCerradoMesAnioBaseDatos(FechaInicio, fechaFin, "5", "db_seguimientoscz_jyc.");
                        int cant_RexTotal = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "5", "db_seguimientoscz_jyc.");

                        Interlogy_totalREX = Interlogy_totalREX + cant_RexTotal;
                        Interlogy_totalREXCerrado = Interlogy_totalREXCerrado + cant_RexCerrado;
                        if (cant_eventoTotal == 0)
                        {
                            dt.Rows[5][faux.ToString("MMMM") + "-" + i] = "0//0";
                        }
                        else
                            dt.Rows[5][faux.ToString("MMMM") + "-" + i] = cant_RexCerrado + "//" + cant_RexTotal; 


                        //----------------------- Interlogy Promedio de Atencion Emergencia ---------
                        string PromedioAtencion = get_PromedioAtencionEmergenciaBaseDatos(j, i, "db_seguimientoscz_jyc.");
                        dt.Rows[9][faux.ToString("MMMM") + "-" + i] = PromedioAtencion;
                        string[] desarmar = PromedioAtencion.Split(':');
                        TimeSpan resultadoHora = new TimeSpan(Convert.ToInt32(desarmar[0]), Convert.ToInt32(desarmar[1]), Convert.ToInt32(desarmar[2]));
                        sumaHorasAtencionInterlogy = sumaHorasAtencionInterlogy + resultadoHora;
                        
                        //----------------------- Interlogy Promedio de Equipo Parado ---------
                        string PromedioEquipoParado = get_PromedioAtencionEmergenciaBaseDatos_AscensorParado(j, i, "db_seguimientoscz_jyc.");
                        dt.Rows[13][faux.ToString("MMMM") + "-" + i] = PromedioEquipoParado;
                        desarmar = PromedioEquipoParado.Split(':');
                        resultadoHora = new TimeSpan(Convert.ToInt32(desarmar[0]), Convert.ToInt32(desarmar[1]), Convert.ToInt32(desarmar[2]));
                        sumaHorasEquipoParadoInterlogy = sumaHorasEquipoParadoInterlogy + resultadoHora;

                        //----------------------- Interlogy Numero ETV con mas de 3 llamadas en un mes -----------
                        int nroETV = get_NroEdificiosConMasDe3llamadasEn1Mes_baseDatos(j,i,"db_seguimientoscz_jyc.");
                        dt.Rows[17][faux.ToString("MMMM") + "-" + i] = nroETV;
                        sumaInterlogy = sumaInterlogy + nroETV;
                        cantInterlogy++;

                        //----------------------- Interlogy Promedio de Atencion Emergencia NOCHE ---------
                        string PromedioAtencionNOCHE = get_PromedioAtencionEmergenciaBaseDatos_NOCHE(j, i, "db_seguimientoscz_jyc.");
                        dt.Rows[21][faux.ToString("MMMM") + "-" + i] = PromedioAtencionNOCHE;
                        desarmar = PromedioAtencionNOCHE.Split(':');
                        resultadoHora = new TimeSpan(Convert.ToInt32(desarmar[0]), Convert.ToInt32(desarmar[1]), Convert.ToInt32(desarmar[2]));
                        sumaHorasAtencionInterlogyNoche = sumaHorasAtencionInterlogyNoche + resultadoHora;

                        //----------------------- Interlogy Promedio de Equipo Parado NOCHE ---------
                        string PromedioEquipoParadoNOCHE = get_PromedioAtencionEmergenciaBaseDatos_AscensorParado_NOCHE(j, i, "db_seguimientoscz_jyc.");
                        dt.Rows[25][faux.ToString("MMMM") + "-" + i] = PromedioEquipoParadoNOCHE;
                        desarmar = PromedioEquipoParadoNOCHE.Split(':');
                        resultadoHora = new TimeSpan(Convert.ToInt32(desarmar[0]), Convert.ToInt32(desarmar[1]), Convert.ToInt32(desarmar[2]));
                        sumaHorasEquipoParadoInterlogyNoche = sumaHorasEquipoParadoInterlogyNoche + resultadoHora;


                        //----------end interlogy
                        //----------begin Melevar
                        //int cant_eventoCerrado = get_CantidadEventosCerradoMesAnioBaseDatos(fecha1, fecha2, "6,2,3,4", "db_seguimientoscz_jyc.");
                        //int cant_eventoTotal = get_CantidadEventosMesAnioBaseDatos(fecha1, fecha2, "6,2,3,4", "db_seguimientoscz_jyc.");

                        cant_eventoCerrado = get_CantidadEventosCerradoMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientocbba_jyc.");
                        cant_eventoTotal = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientocbba_jyc.");

                        Melevar_totalRQS = Melevar_totalRQS + cant_eventoTotal;
                        Melevar_totalRQSCerrado = Melevar_totalRQSCerrado + cant_eventoCerrado;

                        if (cant_eventoTotal == 0)
                        {
                            dt.Rows[2][faux.ToString("MMMM") + "-" + i] = "0//0";
                        }
                        else
                            dt.Rows[2][faux.ToString("MMMM") + "-" + i] = cant_eventoCerrado + "//" + cant_eventoTotal; 
                        //----------------------- melevar Numero de Rex
                        //cant_RexCerrado = get_cantidadEstadoBaseDatos_REX(j, i, "Cerrado", "db_seguimientocbba_jyc.");
                        //cant_RexTotal = get_cantidadTotalBaseDatos_REX(j, i, "db_seguimientocbba_jyc.");
                        cant_RexCerrado = get_CantidadEventosCerradoMesAnioBaseDatos(FechaInicio, fechaFin, "5", "db_seguimientocbba_jyc.");
                        cant_RexTotal = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "5", "db_seguimientocbba_jyc.");

                        Melevar_totalREX = Melevar_totalREX + cant_RexTotal;
                        Melevar_totalREXCerrado = Melevar_totalREXCerrado + cant_RexCerrado;
                        if (cant_eventoTotal == 0)
                        {
                            dt.Rows[6][faux.ToString("MMMM") + "-" + i] = "0//0";
                        }
                        else
                            dt.Rows[6][faux.ToString("MMMM") + "-" + i] = cant_RexCerrado + "//" + cant_RexTotal;
                        //----------------------- Melevar Promedio de Atencion Emergencia ---------
                        PromedioAtencion = get_PromedioAtencionEmergenciaBaseDatos(j, i, "db_seguimientocbba_jyc.");
                        dt.Rows[10][faux.ToString("MMMM") + "-" + i] = PromedioAtencion;
                        desarmar = PromedioAtencion.Split(':');
                        resultadoHora = new TimeSpan(Convert.ToInt32(desarmar[0]), Convert.ToInt32(desarmar[1]), Convert.ToInt32(desarmar[2]));
                        sumaHorasAtencionMelevar = sumaHorasAtencionMelevar + resultadoHora;

                        //----------------------- Melevar Promedio de Equipo Parado ---------
                        PromedioEquipoParado = get_PromedioAtencionEmergenciaBaseDatos_AscensorParado(j, i, "db_seguimientocbba_jyc.");
                        dt.Rows[14][faux.ToString("MMMM") + "-" + i] = PromedioEquipoParado;
                        desarmar = PromedioEquipoParado.Split(':');
                        resultadoHora = new TimeSpan(Convert.ToInt32(desarmar[0]), Convert.ToInt32(desarmar[1]), Convert.ToInt32(desarmar[2]));
                        sumaHorasEquipoParadoMelevar = sumaHorasEquipoParadoMelevar + resultadoHora;

                        //----------------------- Melevar Numero ETV con mas de 3 llamadas en un mes -----------
                        nroETV = get_NroEdificiosConMasDe3llamadasEn1Mes_baseDatos(j, i, "db_seguimientocbba_jyc.");
                        dt.Rows[18][faux.ToString("MMMM") + "-" + i] = nroETV;
                        sumaMelevar = sumaMelevar + nroETV;
                        cantMelevar++;
                        //----------------------- Melevar Promedio de Atencion Emergencia NOCHE ---------
                        PromedioAtencionNOCHE = get_PromedioAtencionEmergenciaBaseDatos_NOCHE(j, i, "db_seguimientocbba_jyc.");
                        dt.Rows[22][faux.ToString("MMMM") + "-" + i] = PromedioAtencionNOCHE;
                        desarmar = PromedioAtencionNOCHE.Split(':');
                        resultadoHora = new TimeSpan(Convert.ToInt32(desarmar[0]), Convert.ToInt32(desarmar[1]), Convert.ToInt32(desarmar[2]));
                        sumaHorasAtencionMelevarNoche = sumaHorasAtencionMelevarNoche + resultadoHora;

                        //----------------------- Melevar Promedio de Equipo Parado NOCHE---------
                        PromedioEquipoParado = get_PromedioAtencionEmergenciaBaseDatos_AscensorParado_NOCHE(j, i, "db_seguimientocbba_jyc.");
                        dt.Rows[26][faux.ToString("MMMM") + "-" + i] = PromedioEquipoParado;
                        desarmar = PromedioEquipoParado.Split(':');
                        resultadoHora = new TimeSpan(Convert.ToInt32(desarmar[0]), Convert.ToInt32(desarmar[1]), Convert.ToInt32(desarmar[2]));
                        sumaHorasEquipoParadoMelevarNoche = sumaHorasEquipoParadoMelevarNoche + resultadoHora;

                        //--------------end melevar
                        //----------------begin Elevamerica
                        //cant_eventoCerrado = get_cantidadEventosEstadoBaseDatos_RQS(j, i, "Cerrado", "db_seguimientolpz_jyc.");
                        //cant_eventoTotal = get_cantidadEventosTotalBaseDatos_RQS(j, i, "db_seguimientolpz_jyc.");
                        cant_eventoCerrado = get_CantidadEventosCerradoMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientolpz_jyc.");
                        cant_eventoTotal = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "6,2,3,4", "db_seguimientolpz_jyc.");

                        Elevamerica_totalRQS = Elevamerica_totalRQS + cant_eventoTotal;
                        Elevamerica_totalRQSCerrado = Elevamerica_totalRQSCerrado + cant_eventoCerrado;

                        if (cant_eventoTotal == 0)
                        {
                            dt.Rows[3][faux.ToString("MMMM") + "-" + i] = "0//0";
                        }
                        else
                            dt.Rows[3][faux.ToString("MMMM") + "-" + i] = cant_eventoCerrado + "//" + cant_eventoTotal; 

                        //---------------------------- elevamerica Numero de REX -------------------------
                        //cant_RexCerrado = get_cantidadEstadoBaseDatos_REX(j, i, "Cerrado", "db_seguimientolpz_jyc.");
                        //cant_RexTotal = get_cantidadTotalBaseDatos_REX(j, i, "db_seguimientolpz_jyc.");
                        cant_RexCerrado = get_CantidadEventosCerradoMesAnioBaseDatos(FechaInicio, fechaFin, "5", "db_seguimientolpz_jyc.");
                        cant_RexTotal = get_CantidadEventosMesAnioBaseDatos(FechaInicio, fechaFin, "5", "db_seguimientolpz_jyc.");


                        Elevamerica_totalREX = Elevamerica_totalREX + cant_RexTotal;
                        Elevamerica_totalREXCerrado = Elevamerica_totalREXCerrado + cant_RexCerrado;
                        if (cant_eventoTotal == 0)
                        {
                            dt.Rows[7][faux.ToString("MMMM") + "-" + i] = "0//0";
                        }
                        else
                            dt.Rows[7][faux.ToString("MMMM") + "-" + i] = cant_RexCerrado + "//" + cant_RexTotal;
                        //----------------------- Elevamerica Promedio de Atencion Emergencia ---------
                        PromedioAtencion = get_PromedioAtencionEmergenciaBaseDatos(j, i, "db_seguimientolpz_jyc.");
                        dt.Rows[11][faux.ToString("MMMM") + "-" + i] = PromedioAtencion;
                        desarmar = PromedioAtencion.Split(':');
                        resultadoHora = new TimeSpan(Convert.ToInt32(desarmar[0]), Convert.ToInt32(desarmar[1]), Convert.ToInt32(desarmar[2]));
                        sumaHorasAtencionElevamerica = sumaHorasAtencionElevamerica + resultadoHora;

                        //----------------------- Elevamerica Promedio de Equipo Parado ---------
                        PromedioEquipoParado = get_PromedioAtencionEmergenciaBaseDatos_AscensorParado(j, i, "db_seguimientolpz_jyc.");
                        dt.Rows[15][faux.ToString("MMMM") + "-" + i] = PromedioEquipoParado;
                        desarmar = PromedioEquipoParado.Split(':');
                        resultadoHora = new TimeSpan(Convert.ToInt32(desarmar[0]), Convert.ToInt32(desarmar[1]), Convert.ToInt32(desarmar[2]));
                        sumaHorasEquipoParadoElevamerica = sumaHorasEquipoParadoElevamerica + resultadoHora;

                        //----------------------- Elevamerica Numero ETV con mas de 3 llamadas en un mes -----------
                        nroETV = get_NroEdificiosConMasDe3llamadasEn1Mes_baseDatos(j, i, "db_seguimientolpz_jyc.");
                        dt.Rows[19][faux.ToString("MMMM") + "-" + i] = nroETV;
                        sumaElevamerica = sumaElevamerica + nroETV;
                        cantElevamerica++;
                        //----------------------- Melevar Promedio de Atencion Emergencia NOCHE ---------
                        PromedioAtencionNOCHE = get_PromedioAtencionEmergenciaBaseDatos_NOCHE(j, i, "db_seguimientolpz_jyc.");
                        dt.Rows[23][faux.ToString("MMMM") + "-" + i] = PromedioAtencionNOCHE;
                        desarmar = PromedioAtencionNOCHE.Split(':');
                        resultadoHora = new TimeSpan(Convert.ToInt32(desarmar[0]), Convert.ToInt32(desarmar[1]), Convert.ToInt32(desarmar[2]));
                        sumaHorasAtencionElevamericaNoche = sumaHorasAtencionElevamericaNoche + resultadoHora;

                        //----------------------- Elevamerica Promedio de Equipo Parado NOCHE---------
                        PromedioEquipoParado = get_PromedioAtencionEmergenciaBaseDatos_AscensorParado_NOCHE(j, i, "db_seguimientolpz_jyc.");
                        dt.Rows[27][faux.ToString("MMMM") + "-" + i] = PromedioEquipoParado;
                        desarmar = PromedioEquipoParado.Split(':');
                        resultadoHora = new TimeSpan(Convert.ToInt32(desarmar[0]), Convert.ToInt32(desarmar[1]), Convert.ToInt32(desarmar[2]));
                        sumaHorasEquipoParadoElevamericaNoche = sumaHorasEquipoParadoElevamericaNoche + resultadoHora;


                        //-----------------end elevamerica                    
                }
                    mesDesde = 1;
            }
            dt.Columns.Add("Total");
            dt.Rows[1]["Total"] = Interlogy_totalRQSCerrado + "//" + Interlogy_totalRQS;
            dt.Rows[2]["Total"] = Melevar_totalRQSCerrado + "//" + Melevar_totalRQS;
            dt.Rows[3]["Total"] = Elevamerica_totalRQSCerrado + "//" + Elevamerica_totalRQS;
            
            dt.Rows[5]["Total"] = Interlogy_totalREXCerrado + "//" + Interlogy_totalREX;
            dt.Rows[6]["Total"] = Melevar_totalREXCerrado + "//" + Melevar_totalREX;
            dt.Rows[7]["Total"] = Elevamerica_totalREXCerrado + "//" + Elevamerica_totalREX;

            TimeSpan promedioAtencion = new TimeSpan(sumaHorasAtencionInterlogy.Ticks / cantInterlogy);
            // el dd/. --> dia
            //dt.Rows[9]["Total"] = promedioAtencion.ToString(@"dd\.hh\:mm\:ss");
            dt.Rows[9]["Total"] = promedioAtencion.ToString(@"hh\:mm\:ss");
            promedioAtencion = new TimeSpan(sumaHorasAtencionMelevar.Ticks / cantMelevar);
            dt.Rows[10]["Total"] = promedioAtencion.ToString(@"hh\:mm\:ss");
            promedioAtencion = new TimeSpan(sumaHorasAtencionElevamerica.Ticks / cantElevamerica);
            dt.Rows[11]["Total"] = promedioAtencion.ToString(@"hh\:mm\:ss");

            promedioAtencion = new TimeSpan(sumaHorasEquipoParadoInterlogy.Ticks / cantInterlogy);
            dt.Rows[13]["Total"] = promedioAtencion.ToString(@"hh\:mm\:ss");
            promedioAtencion = new TimeSpan(sumaHorasEquipoParadoMelevar.Ticks / cantMelevar);
            dt.Rows[14]["Total"] = promedioAtencion.ToString(@"hh\:mm\:ss");
            promedioAtencion = new TimeSpan(sumaHorasEquipoParadoElevamerica.Ticks / cantElevamerica);
            dt.Rows[15]["Total"] = promedioAtencion.ToString(@"hh\:mm\:ss");

            dt.Rows[17]["Total"] = sumaInterlogy / cantInterlogy;
            dt.Rows[18]["Total"] = sumaMelevar / cantMelevar;
            dt.Rows[19]["Total"] = sumaElevamerica / cantElevamerica;
            //--------------------TOTAL ULTIMO INSERTADO
            promedioAtencion = new TimeSpan(sumaHorasAtencionInterlogyNoche.Ticks / cantInterlogy);
            dt.Rows[21]["Total"] = promedioAtencion.ToString(@"hh\:mm\:ss");
            promedioAtencion = new TimeSpan(sumaHorasAtencionMelevarNoche.Ticks / cantMelevar);
            dt.Rows[22]["Total"] = promedioAtencion.ToString(@"hh\:mm\:ss");
            promedioAtencion = new TimeSpan(sumaHorasAtencionElevamericaNoche.Ticks / cantElevamerica);
            dt.Rows[23]["Total"] = promedioAtencion.ToString(@"hh\:mm\:ss");

            promedioAtencion = new TimeSpan(sumaHorasEquipoParadoInterlogyNoche.Ticks / cantInterlogy);
            dt.Rows[25]["Total"] = promedioAtencion.ToString(@"hh\:mm\:ss");
            promedioAtencion = new TimeSpan(sumaHorasEquipoParadoMelevarNoche.Ticks / cantMelevar);
            dt.Rows[26]["Total"] = promedioAtencion.ToString(@"hh\:mm\:ss");
            promedioAtencion = new TimeSpan(sumaHorasEquipoParadoElevamericaNoche.Ticks / cantElevamerica);
            dt.Rows[27]["Total"] = promedioAtencion.ToString(@"hh\:mm\:ss");
           
            //-------------------- end Reporte RQS       
            
            return ds;
        }

        public DataSet get_CuadroIndicadoresRCC(string fecha1, string fecha2, string baseDeDatos)
        {
            string consulta = "select " +
                                   " even.codigo as 'Ticket', " +
                                   " even.semana, " +
                                   "'" + baseDeDatos + "' as 'Unidad_Negocio', " +
                                   " date_format(even.fecha,'%d/%m/%Y') as 'Fecha_Evento', " +
                                   " even.cliente, " +
                                   " even.nombreEdificio, " +
                                   " teven.nombre as 'Tipo_Evento', " +
                                   " even.Observacion as 'Solicitud_o_Servicio', " +
                                   " even.defectoconstatado, " +
                                   " even.observacion_necesidadrepuesto as 'Observacion_necesidad_repuesto', " +
                                   " even.solicitudRepuesto, " +
                                   " even.envioproforma, " +
                                   " even.aceptacionproforma, " +
                                   " even.verificacion_cambio, " +
                                   " even.cambiorepuesto, " +
                                   " even.observacion_evento as 'Detalle_cierre_evento', " +
                                   " res1.nombre as 'InicioEvento', " +
                                   " res2.nombre as 'CierreEvento', " +
                                   " 1 as 'Total' " +
                                   " from "+
                                   " tb_tipoevento teven,  "+
                                   " tb_evento even "+
                                   " left join tb_responsable res1 on (even.inicioeventouser = res1.codigo) "+
                                   " left join tb_responsable res2 on (even.cierreeventouser = res2.codigo) "+
                                   " where  "+
                                   " even.codtipoevento = teven.codigo and " +                                   
                                   " even.cambiorepuesto = 1 and " +
                                   " even.solicitudrepuestobandera = 1 and " +
                                   " even.aceptacionproforma is null ";
            if (!fecha1.Equals("") && !fecha2.Equals(""))
            {
                consulta = consulta + "and even.fecha between " + fecha1 + " and " + fecha2;
            }

                                   
            return Devento.getDatos(consulta);  
        }

        public DataSet get_CuadroIndicadoresRIN(string fecha1, string fecha2, string baseDeDatos)
        {
            string consulta = "select " +
                                   " even.codigo as 'Ticket', " +
                                   " even.semana, " +
                                   "'" + baseDeDatos + "' as 'Unidad_Negocio', " +
                                   " date_format(even.fecha,'%d/%m/%Y') as 'Fecha_Evento', " +
                                   " even.cliente, " +
                                   " even.nombreEdificio, " +
                                   " teven.nombre as 'Tipo_Evento', " +
                                   " even.Observacion as 'Solicitud_o_Servicio', " +
                                   " even.defectoconstatado, " +
                                   " even.observacion_necesidadrepuesto as 'Observacion_necesidad_repuesto', " +
                                   " even.solicitudRepuesto, " +
                                   " even.envioproforma, " +
                                   " even.aceptacionproforma, " +
                                   " even.verificacion_cambio, " +
                                   " even.cambiorepuesto, " +
                                   " even.observacion_evento as 'Detalle_cierre_evento', " +
                                   " res1.nombre as 'InicioEvento', " +
                                   " res2.nombre as 'CierreEvento', " +
                                   " 1 as 'Total' " +
                                   " from "+
                                   " tb_tipoevento teven,   "+
                                   " tb_evento even "+
                                   " left join tb_responsable res1 on (even.inicioeventouser = res1.codigo) "+
                                   " left join tb_responsable res2 on (even.cierreeventouser = res2.codigo) "+
                                   " where  "+
                                   " even.codtipoevento = teven.codigo and " +                                   
                                   " even.cambiorepuesto = 1 and " +
                                   " even.solicitudrepuestobandera = 0 ";
                                   
            if (!fecha1.Equals("") && !fecha2.Equals(""))
            {
                consulta = consulta + "and even.fecha between " + fecha1 + " and " + fecha2;
            }


            return Devento.getDatos(consulta);
        }

        public bool updateEventoCallcenter_CotiRepuesto(int codEvento, int CodCoti, int codPrioridad)
        {
            return Devento.updateEventoCallcenter_CotiRepuesto(codEvento, CodCoti,  codPrioridad);        
        }

        public bool updatefechaEnvioProformaEvento(int codEvento)
        {
            return Devento.updatefechaEnvioProformaEvento(codEvento);
        }

        public bool updatefechaAceptacionProformaEvento(int codEvento)
        {
            return Devento.updatefechaAceptacionProformaEvento(codEvento);
        }

        public bool pasarEventoalAreaRIN(int codEvento)
        {
            return Devento.pasarEventoalAreaRIN(codEvento);
        }

        public bool updateCerrarEventoCotizacion(int codEvento, int codUser, string detalleCierre)
        {
            return Devento.updateCerrarEventoCotizacion(codEvento, codUser, detalleCierre);
        }


        public bool updateCerrarEventoCotizacionAreaCliente(int codEvento,  string detalleCierre)
        {
            return Devento.updateCerrarEventoCotizacionAreaCliente(codEvento, detalleCierre);
        }


        public bool estaCerradoelEvento(int codEvento) {
            string consulta = "select * from tb_evento even "+
                               " where  "+
                               " even.estadoEvento = 'Cerrado' and "+
                               " even.codigo = "+codEvento;
            DataSet datos = Devento.getDatos(consulta);
            if (datos.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }


        public int getultimoInsertado() {
            string consulta = "select max(even.codigo) from tb_evento even";
            DataSet datos = Devento.getDatos(consulta);
            int resultado = -1;

            try
            {
                return Convert.ToInt32(datos.Tables[0].Rows[0][0].ToString());
            }
            catch (IOException e) {
                return resultado;            
            }
                    
        }

        public bool modificarDatosEventoSolicitudRepuesto(int codEvento, string defectoConstatado, string observacionNecesidadRepuesto, string solicitudRepuesto, bool cambioRepuesto, bool solicitudRepuestobandera)
        {
            return Devento.modificarDatosEventoSolicitudRepuesto(codEvento, defectoConstatado, observacionNecesidadRepuesto, solicitudRepuesto, cambioRepuesto, solicitudRepuestobandera);                
        }



        public int getCantCoticonElMismoEvento(int codEvento)
        {
            DataSet dato = Devento.getCantCoticonElMismoEvento(codEvento);
            int cantidad;
            bool bandera = int.TryParse(dato.Tables[0].Rows[0][0].ToString(),out cantidad);
            if (bandera)
            {
                if (cantidad > 0)
                {
                    return cantidad;
                }
                else
                    return -1;
            }
            else
                return -1;
        }

        public int getCantCoticonElMismoEvento_estadoCerrado(int codEvento)
        {
            DataSet dato = Devento.getCantCoticonElMismoEvento_estadoCerrado(codEvento);
            int cantidad;
            bool bandera = int.TryParse(dato.Tables[0].Rows[0][0].ToString(), out cantidad);
            if (bandera)
            {
                if (cantidad > 0)
                {
                    return cantidad;
                }
                else
                    return 0;
            }
            else
                return 0;
        }
    }
}