using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.IO;


namespace jycboliviaASP.net.Datos
{
    public class DA_Evento
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DA_Evento() { }

        public bool insertar(int semana, string hora, string fecha, string cliente, string telefono, string celular, string nombreEdificio, string dirEdificio, string ascensor, string estadoEvento, string Observacion, int codtipoevento, int codEdificio, int ascensorParado, int personasatrapadas, int prioridad, int UserInicio, bool cambioRepuesto, bool areacotirepuesto, bool solicitudrepuestobandera, bool areacallcenter, bool arearin, bool arearcc, bool areacliente)
        {  
                string codEdificio_aux = "null";
                if(codEdificio > -1){
                    codEdificio_aux = codEdificio.ToString();
                }

                string consulta = "insert into tb_evento(semana, hora, fecha, cliente, telefono, celular, nombreEdificio, dirEdificio, ascensor, estadoEvento, Observacion, codtipoevento, codEdificio, ascensorparado, personasatrapadas, prioridad, inicioeventouser, cambiorepuesto , areacotirepuesto, solicitudrepuestobandera, areacallcenter, arearin, arearcc, areacliente) " +
                                    " values(" + semana + ", '" + hora + "', '" + fecha + "', '" + cliente + "', '" + telefono + "', '" + celular + "', '" + nombreEdificio + "', '" + dirEdificio + "', '" + ascensor + "', '" + estadoEvento + "', '" + Observacion + "', " + codtipoevento + ", " + codEdificio_aux + "," + ascensorParado + "," + personasatrapadas + "," + prioridad + " , " + UserInicio + ","+cambioRepuesto+"," + areacotirepuesto + ", "+solicitudrepuestobandera+", "+areacallcenter+" , "+ arearin+" , "+arearcc+" , "+areacliente+" ) ";
               return ConecRes.ejecutarMySql(consulta);
        }

        public bool modificar(int codigoEvento,string cliente, string telefono, string celular, string nombreEdificio, string dirEdificio, string ascensor, string Observacion,  int ascensorParado, int personasatrapadas )
        {
           

            string consulta = "update tb_evento set cliente = '" + cliente + "', telefono = '" + telefono + "', celular = '" + celular + "', nombreEdificio = '" + nombreEdificio + "', dirEdificio ='" + dirEdificio + "', ascensor = '" + ascensor + "', Observacion = '" + Observacion + "', ascensorparado = " + ascensorParado + ", personasatrapadas = " + personasatrapadas + " where tb_evento.codigo = " + codigoEvento;
                                    
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool eliminar()
        {
            return false;
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        public bool modificarDatosEvento(int codEvento, string estadoEvento, string observacion, string defectoConstatado, string observacionNecesidadRepuesto, string solicitudRepuesto, string envioProforma, string aceptacionProforma, string verificacionCambio, bool cambioRepuesto, int codCierreUser, string fechaEven, string HoraEven, bool solicitudRepuestobandera, bool areaCliente, bool areaCallcenter, bool areaRin, bool areaRcc, int prioridad)
        {
            string auxTexto = "";
            if(estadoEvento.Equals("Cerrado")){
                auxTexto = " , tb_evento.cierreeventouser = " + codCierreUser + ", tb_evento.fechacierreevento = now() , tb_evento.horacierreevento = now() ";
            }

            string consulta = "update tb_evento set tb_evento.estadoEvento = '"+estadoEvento+"' ,tb_evento.observacion_evento = '"+observacion+"', tb_evento.defectoconstatado = '"+defectoConstatado+"', tb_evento.observacion_necesidadrepuesto = '"+observacionNecesidadRepuesto+"', "+
                               " tb_evento.solicitudRepuesto = "+solicitudRepuesto+", tb_evento.envioproforma = "+envioProforma+", tb_evento.aceptacionproforma = "+aceptacionProforma+", "+
                               " tb_evento.verificacion_cambio = '" + verificacionCambio + "', tb_evento.cambiorepuesto = " + cambioRepuesto +" " + auxTexto +  
                               " ,tb_evento.fecha = "+fechaEven+" , tb_evento.hora = "+HoraEven+" , tb_evento.solicitudrepuestobandera = "+solicitudRepuestobandera+
                               " ,tb_evento.arearin = "+areaRin+", "+
                               " tb_evento.arearcc = "+areaRcc+", "+
                               " tb_evento.areacliente = "+areaCliente+", "+
                               " tb_evento.areacallcenter = "+areaCallcenter+" , "+
                               " tb_evento.prioridad = "+prioridad+
                               " where tb_evento.codigo = "+codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool modificarDatosEvento_FechaContactoCliente(int codEvento, string fechaContactoCliente, string detalleContactoCliente)
        {
            string consulta = "update tb_evento set tb_evento.fechacontactocliente = " + fechaContactoCliente + ", " +
                               " tb_evento.detalle_contactocliente = '" + detalleContactoCliente + "', " +
                               " tb_evento.horacontactocliente = now() " +
                               " where tb_evento.codigo = " + codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool modificarDatosEvento2(int codEvento, string estadoEvento, string observacion, string defectoConstatado, string observacionNecesidadRepuesto, string solicitudRepuesto, string envioProforma, string aceptacionProforma, string verificacionCambio, bool cambioRepuesto, int codCierreUser, string fechaEven, string HoraEven, bool solicitudRepuestobandera)
        {
            string auxTexto = "";
            if (estadoEvento.Equals("Cerrado"))
            {
                auxTexto = " , tb_evento.cierreeventouser = " + codCierreUser + ", tb_evento.fechacierreevento = '" + DateTime.Now.ToString("yyyy-MM-dd") + "', tb_evento.horacierreevento = '" + DateTime.Now.ToString("HH:mm:ss") + "' ";
            }

            string consulta = "update tb_evento set tb_evento.estadoEvento = '" + estadoEvento + "' ,tb_evento.observacion_evento = '" + observacion + "', tb_evento.defectoconstatado = '" + defectoConstatado + "', tb_evento.observacion_necesidadrepuesto = '" + observacionNecesidadRepuesto + "', " +
                              " tb_evento.solicitudRepuesto = " + solicitudRepuesto + ", tb_evento.envioproforma = " + envioProforma + ", tb_evento.aceptacionproforma = " + aceptacionProforma + ", " +
                              " tb_evento.verificacion_cambio = '" + verificacionCambio + "', tb_evento.cambiorepuesto = " + cambioRepuesto + " " + auxTexto +
                              " ,tb_evento.fecha = " + fechaEven + " , tb_evento.hora = " + HoraEven + " , tb_evento.solicitudrepuestobandera = " + solicitudRepuestobandera +
                               " where tb_evento.codigo = " + codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }

        //---------------------nuevo detalle de callcenter y cotizaciones enlace
        public bool updateEventoCallcenter_CotiRepuesto(int codEvento, int CodCoti, int codPrioridad)
        {
            string consulta = "update tb_cotizacionrepuesto set tb_cotizacionrepuesto.codevento = "+codEvento+" , "+
                              " tb_cotizacionrepuesto.prioridad =  "+codPrioridad+ 
                              " where "+
                              " tb_cotizacionrepuesto.codigo = "+CodCoti;

            return ConecRes.ejecutarMySql(consulta);
        }

        public bool updatefechaEnvioProformaEvento(int codEvento)
        {
            string consulta = "update tb_evento set tb_evento.envioproforma = now() "+
                               " where tb_evento.codigo = "+codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool updatefechaAceptacionProformaEvento(int codEvento)
        {
            string consulta = "update tb_evento "+
                               " set tb_evento.envioproforma = now(), "+
                               " tb_evento.aceptacionproforma = now(), "+
                               " tb_evento.solicitudrepuestobandera = 0, "+
                               " tb_evento.areacotirepuesto = 1, "+
                               " tb_evento.areacallcenter = 0, "+
                               " tb_evento.areacliente = 0, "+
                               " tb_evento.arearcc = 0, "+
                               " tb_evento.arearin = 0, "+
                               " tb_evento.areaera = 1 "+
                               " where tb_evento.codigo =" + codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool pasarEventoalAreaRIN(int codEvento)
        {
            string consulta = "update tb_evento set "+
                               " tb_evento.areacotirepuesto = 0, "+
                               " tb_evento.areaera = 0, "+
                               " tb_evento.arearin = 1 " +
                               " where tb_evento.codigo = " + codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool updateCerrarEventoCotizacion(int codEvento, int codUser, string detalleCierre)
        {
            string aux1 = "select even.observacion_evento "+
                           " from tb_evento even  "+
                           " where even.codigo = "+codEvento;
            DataSet dato = ConecRes.consultaMySql(aux1);
          
                string observacion = dato.Tables[0].Rows[0][0].ToString();

                string consulta = "";
                if (!observacion.Equals(""))
                {
                    consulta = "update tb_evento set tb_evento.observacion_evento = concat(tb_evento.observacion_evento,' ','" + detalleCierre + "') , " +
                                       " tb_evento.estadoEvento = 'Cerrado', tb_evento.fechacierreevento = now(), tb_evento.horacierreevento = now(), " +
                                       " tb_evento.cierreeventouser = " + codUser +
                                       " where tb_evento.codigo = " + codEvento;
                }
                else
                {
                    consulta = "update tb_evento set tb_evento.observacion_evento = '" + detalleCierre + "' , " +
                                           " tb_evento.estadoEvento = 'Cerrado', tb_evento.fechacierreevento = now(), tb_evento.horacierreevento = now(), " +
                                           " tb_evento.cierreeventouser = " + codUser +
                                           " where tb_evento.codigo = " + codEvento;
                }

                return ConecRes.ejecutarMySql(consulta);            

        }


        public bool updateCerrarEventoCotizacionAreaCliente(int codEvento, string detalleCierre)
        {
            string consulta = "update tb_evento set tb_evento.observacion_evento = concat(tb_evento.observacion_evento,' ','" + detalleCierre + "') , " +
                               " tb_evento.estadoEvento = 'Abierto' , "+
                               " tb_evento.solicitudrepuestobandera = 0, " +
                               " tb_evento.areacotirepuesto = 1, " +
                               " tb_evento.areacallcenter = 0, " +
                               " tb_evento.areacliente = 1, " +
                               " tb_evento.arearcc = 0, " +
                               " tb_evento.arearin = 0 " +
                               " where tb_evento.codigo = " + codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool modificarDatosEventoSolicitudRepuesto(int codEvento, string defectoConstatado, string observacionNecesidadRepuesto, string solicitudRepuesto, bool cambioRepuesto, bool solicitudRepuestobandera)
        {
         

            string consulta = "update tb_evento set  tb_evento.defectoconstatado = '" + defectoConstatado + "', tb_evento.observacion_necesidadrepuesto = '" + observacionNecesidadRepuesto + "', " +
                              " tb_evento.solicitudRepuesto = " + solicitudRepuesto + ", " +
                              " tb_evento.cambiorepuesto = " + cambioRepuesto + " , "+
                              " tb_evento.solicitudrepuestobandera = " + solicitudRepuestobandera +                              
                               " where tb_evento.codigo = " + codEvento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public DataSet getCantCoticonElMismoEvento(int codEvento)
        {
            string consulta = "select count(*) as 'cant' from tb_cotizacionrepuesto coti " +
                               " where "+
                               " coti.codr144_padre is not null and "+
                               " coti.codevento = "+codEvento;
            return ConecRes.consultaMySql(consulta);
        }

        public DataSet getCantCoticonElMismoEvento_estadoCerrado(int codEvento)
        {
            string consulta = "select count(*) as 'cant' from tb_cotizacionrepuesto coti " +
                               " where "+
                               " coti.codr144_padre is not null and "+
                               " coti.estadocoti = 'Cerrado' and "+
                               " coti.codevento = "+codEvento;
            return ConecRes.consultaMySql(consulta);
        }
    }
}