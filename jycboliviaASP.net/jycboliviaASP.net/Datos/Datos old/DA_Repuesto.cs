using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_Repuesto
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_Repuesto() {
        }

        public DataSet mostrarRepuestos(string codigo, string detalle)
        {
            string consulta = "select p.codigo as 'Numeracion', p.numeracion as 'Codigo', p.denominacion as 'Nombre_Repuesto', p.precio "+
                               " from tb_preciorepuesto p "+
                               " where p.estado = 1 and "+
                               " p.numeracion like '%" + codigo + "%' and " +
                               " p.denominacion like '%"+detalle+"%'";
            return ConecRes.consultaMySql(consulta);
        }


        public bool insertarCotizacionRepuesto(string monto, string edificio, string cite, string ascensores, int codrespSolicitante,int codEvento, int codequipo)
        {
            string codigoEventoAux = "null";
            if(codEvento > -1){
                codigoEventoAux = codEvento.ToString();
            }

            string codigoEquipoAux = "null";
            if(codequipo > -1){
                codigoEquipoAux = codequipo.ToString();
            }


            string consulta = "insert into tb_cotizacionrepuesto( "+
                               " tb_cotizacionrepuesto.fecha, "+
                               " tb_cotizacionrepuesto.hora, "+
                               " tb_cotizacionrepuesto.monto, "+
                               " tb_cotizacionrepuesto.edificio, "+
                               " tb_cotizacionrepuesto.cite, "+
                               " tb_cotizacionrepuesto.ascensor, "+
                               " tb_cotizacionrepuesto.estadocoti, "+
                               " tb_cotizacionrepuesto.codrespsolicitante, "+
                               " tb_cotizacionrepuesto.horasolicitud, "+
                               " tb_cotizacionrepuesto.fechasolicitud, "+
                               " tb_cotizacionrepuesto.codevento, "+
                               " tb_cotizacionrepuesto.codequipo "+
                               " )values(  "+
                               " now(),  "+
                               " now(), "+
                               " '"+monto+"', "+
                               " '"+edificio+"', "+
                               " '"+cite+"', "+
                               " '"+ascensores+"', " + 
                               " 'Abierto', "+
                               codrespSolicitante+" , "+
                               " now(), "+
                               " now(), "+
                               codigoEventoAux+" , "+
                               codigoEquipoAux+" )";
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool insertarDetalleCotizacionRepuesto(int codcoti, int codrepuesto, double cantidad, double preciocompra) {
            string consulta = "insert into tb_detalle_cotirepuesto( "+
                               " tb_detalle_cotirepuesto.codcoti, "+
                               " tb_detalle_cotirepuesto.codrepuesto, "+
                               " tb_detalle_cotirepuesto.cantidad, "+                               
                               " tb_detalle_cotirepuesto.preciocompra "+
                               " )values( " + codcoti + ", " + codrepuesto + ", '" + cantidad.ToString().Replace(",", ".") + "','" + preciocompra.ToString().Replace(",", ".") + "')";
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool updateCotizacionRepuesto(int codigoCoti,  string estadoCoti, string detalleCierreCoti, bool vendido, bool rechazado, bool areacliente, int codResponsabeCierre, bool areaera)
        {
            string consulta = "update tb_cotizacionrepuesto "+
                               " set tb_cotizacionrepuesto.estadocoti = '"+estadoCoti+"' , "+
                               " tb_cotizacionrepuesto.detallecierrecotizacion = '"+detalleCierreCoti+"', "+
                               " tb_cotizacionrepuesto.vendido = "+vendido+", "+
                               " tb_cotizacionrepuesto.rechazado = "+rechazado+ " , "+
                               " tb_cotizacionrepuesto.areacliente = "+areacliente+" , "+
                               " tb_cotizacionrepuesto.horacierre = now(), "+ 
                               " tb_cotizacionrepuesto.fechacierre = now(), "+
                               " tb_cotizacionrepuesto.codrespcierre = "+ codResponsabeCierre+", "+
                               " tb_cotizacionrepuesto.areaera = "+areaera+
                               " where tb_cotizacionrepuesto.codigo = "+codigoCoti;
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

      /*  public bool actualizarAlmacenLocal(int codcotizacion, int codRepuesto, bool bandera) {
            string consulta = "update tb_detalle_cotirepuesto set  "+
                               " tb_detalle_cotirepuesto.almacenlocal = "+bandera+" , "+
                               " tb_detalle_cotirepuesto.fechapedidorepuestolocal = now(), " +
                               " tb_detalle_cotirepuesto.horapedidorepuestolocal = now() " +
                               " where tb_detalle_cotirepuesto.codcoti = "+codcotizacion+
                               " and tb_detalle_cotirepuesto.codrepuesto = " + codRepuesto;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool actualizarAlmacenJYC(int codcotizacion, int codRepuesto, bool bandera)
        {
            string consulta = "update tb_detalle_cotirepuesto set "+
                               " tb_detalle_cotirepuesto.almacenjyc = "+bandera+", "+
                               " tb_detalle_cotirepuesto.fechapedidorepuestojyc = now(), " +
                               " tb_detalle_cotirepuesto.horapedidorepuestojyc = now() " +
                               " where tb_detalle_cotirepuesto.codcoti = "+codcotizacion+
                               " and tb_detalle_cotirepuesto.codrepuesto = " + codRepuesto;
            return ConecRes.ejecutarMySql(consulta);
        }

    
        public bool actualizarCompraEnProceso(int codcotizacion, int codRepuesto, bool bandera)
        {
            string consulta = "update tb_detalle_cotirepuesto set  "+
                               " tb_detalle_cotirepuesto.gcompra = "+bandera+", "+
                               " tb_detalle_cotirepuesto.fechagcompra = now(), "+
                               " tb_detalle_cotirepuesto.horagcompra = now() "+
                               " where tb_detalle_cotirepuesto.codcoti =  "+codcotizacion+
                               " and tb_detalle_cotirepuesto.codrepuesto = "+codRepuesto;
            return ConecRes.ejecutarMySql(consulta);
        }
        

        public bool actualizarEntregaRepuestoJYC(int codcotizacion, int codRepuesto, bool bandera)
        {
            string consulta = "update tb_detalle_cotirepuesto set "+
                               " tb_detalle_cotirepuesto.gentregajyc = "+bandera+", "+
                               " tb_detalle_cotirepuesto.fechagentregajyc = now(), "+
                               " tb_detalle_cotirepuesto.horagentregajyc = now() "+
                               " where tb_detalle_cotirepuesto.codcoti = "+codcotizacion+
                               " and tb_detalle_cotirepuesto.codrepuesto = "+codRepuesto ;
            return ConecRes.ejecutarMySql(consulta);
        }
        

        public bool actualizarEntregaRepuestoJYCIA(int codcotizacion, int codRepuesto, bool bandera)
        {
            string consulta = "update tb_detalle_cotirepuesto set " +
                               " tb_detalle_cotirepuesto.gentregajycia = " + bandera + ", " +
                               " tb_detalle_cotirepuesto.fechagentregajycia = now(), " +
                               " tb_detalle_cotirepuesto.horagentregajycia = now() " +
                               " where tb_detalle_cotirepuesto.codcoti = " + codcotizacion +
                               " and tb_detalle_cotirepuesto.codrepuesto = " + codRepuesto;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool actualizarEntregadoRepuestoLocal(int codcotizacion, int codRepuesto, bool bandera)
        {
            string consulta = "update tb_detalle_cotirepuesto set " +
                               " tb_detalle_cotirepuesto.gentregalocal = " + bandera + ", " +
                               " tb_detalle_cotirepuesto.fechagentregalocal = now(), " +
                               " tb_detalle_cotirepuesto.horagentregalocal = now() " +
                               " where tb_detalle_cotirepuesto.codcoti = " + codcotizacion +
                               " and tb_detalle_cotirepuesto.codrepuesto = " + codRepuesto;
            return ConecRes.ejecutarMySql(consulta);
        }*/
        
        public bool updateCiteCotizacion(int codcoti, string cite)
        {
            string consulta = "update tb_cotizacionrepuesto set tb_cotizacionrepuesto.cite = '"+cite+"' "+
                               " where tb_cotizacionrepuesto.codigo = "+codcoti;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool updateFechaEventoCotizacion(int codevento, string fechaEnvioProforma)
        {
            string consulta = "update tb_evento set tb_evento.envioproforma = "+fechaEnvioProforma+
                               " where tb_evento.codigo ="+codevento;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool updateAlmacenesR144(int codCoti, int codRepuesto, string almacen_Local, string almacen_JyC, string almacen_JyCIA)
        {
            string consulta = "update tb_detalle_cotirepuesto set " +
                               " tb_detalle_cotirepuesto.cant_almacenlocal = '"+almacen_Local+"', " +
                               " tb_detalle_cotirepuesto.cant_almacenjyc = '"+almacen_JyC+"', " +
                               " tb_detalle_cotirepuesto.cant_almacenjycia =  '"+almacen_JyCIA+"'" +
                               " where " +
                               " tb_detalle_cotirepuesto.codcoti = "+codCoti+" and " +
                               " tb_detalle_cotirepuesto.codrepuesto = "+codRepuesto;
            return ConecRes.ejecutarMySql(consulta);
        }
        



        public bool crearCotizacionSeparadaAlmacenLocal(int codR144)
        {
            string consulta = "call 9_crearcotizacionesseparada_local("+codR144+");";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool crearCotizacionSeparadaAlmacenJYC(int codR144)
        {
            string consulta = "call 10_crearcotizacionesseparada_jyc(" + codR144 + ");";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool crearCotizacionSeparadaAlmacenJYCIA(int codR144)
        {
            string consulta = "call 11_crearcotizacionesseparada_jycia(" + codR144 + ");";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool cerrarR144(int codR144, int codResponsable, string detalle)
        {
            string consulta = "update tb_cotizacionrepuesto set " +
                               " tb_cotizacionrepuesto.estadocoti = 'Cerrado', "+
                               " tb_cotizacionrepuesto.horacierre = now(), "+
                               " tb_cotizacionrepuesto.fechacierre = now(), "+
                               " tb_cotizacionrepuesto.detallecierrecotizacion = '"+detalle+"', "+
                               " tb_cotizacionrepuesto.codrespcierre =  "+codResponsable+
                               " where "+
                               " tb_cotizacionrepuesto.codigo = "+codR144;
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getDetallesRepuestoR144(int codR144)
        {
            string consulta = "select "+
                               " rep.codigo, "+
                               " rep.numeracion, "+
                               " rep.denominacion, "+
                               " dcoti.cantidad, "+
                               " dcoti.preciocompra, "+
                               " (dcoti.cantidad * dcoti.preciocompra) as 'Total' "+
                               " from  "+
                               " tb_cotizacionrepuesto coti, "+
                               " tb_detalle_cotirepuesto dcoti, "+
                               " tb_preciorepuesto rep "+
                               " where "+
                               " coti.codigo = dcoti.codcoti and "+
                               " dcoti.codrepuesto = rep.codigo and "+ 
                               " coti.codigo = "+codR144;
            return ConecRes.consultaMySql(consulta);
        }

        public bool actualizar_DatosEra(int codigoCoti, int codigoRepuesto, string fechaentrega_ERA, string nroSerial, string nroFactura)
        {
            string consulta = "update tb_detalle_cotirepuesto set " +
                               " tb_detalle_cotirepuesto.fechaentregaera = " + fechaentrega_ERA + ", " +
                               " tb_detalle_cotirepuesto.nroserial = '" + nroSerial + "', " +
                               " tb_detalle_cotirepuesto.nrofactura = '" + nroFactura + "' " +
                               " where tb_detalle_cotirepuesto.codcoti = " + codigoCoti + " and " +
                               " tb_detalle_cotirepuesto.codrepuesto = " + codigoRepuesto;
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getCantidadRepuestodeCotizacion(int codCoti)
        {
            string consulta = " select ifnull(count(*),0) as 'cantRepuesto' from tb_detalle_cotirepuesto dc "+
                               " where dc.codcoti = "+codCoti;
            return ConecRes.consultaMySql(consulta);
        }

        public DataSet getCantidadRepuesto_Entregados(int codCoti)
        {
            string consulta = "  select ifnull(count(*),0) as 'cantRepuesto' from tb_detalle_cotirepuesto dc "+
                                " where dc.codcoti = "+codCoti+" and "+
                                " dc.fechaentregaera is not null ";
            return ConecRes.consultaMySql(consulta);
        }

       /* public bool pasarEventoalRIN(int codCoti)
        {
            string consulta = "update tb_cotizacionrepuesto "+
                               " set tb_cotizacionrepuesto.areaera = true "+
                               " where tb_cotizacionrepuesto.codigo = "+codCoti;
            return ConecRes.ejecutarMySql(consulta);
        }*/

        public DataSet geteventoCotizacion(int codCoti)
        {
            string consulta = "select  "+
                               " even.*  "+
                               " from tb_evento even , tb_cotizacionrepuesto coti "+
                               " where  "+
                               " coti.codevento = even.codigo and "+
                               " coti.codigo =" + codCoti;
            return ConecRes.consultaMySql(consulta);
        }

        public bool cerrarCotizacion_era(int codCoti)
        {
            string consulta = "update tb_cotizacionrepuesto set "+
                               " tb_cotizacionrepuesto.areaera = 0 "+
                               " where  "+
                               " tb_cotizacionrepuesto.codigo ="+ codCoti;
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getRepuestosSolicitadosporEvento(int codEvento)
        {
            string consulta = "select "+
                               " pr.codigo as 'Numeracion',  "+
                               " pr.numeracion as 'CodigoRepuesto', pr.denominacion as 'Denominacion', "+
                               " dc.cantidad, "+
                               " if(coti.vendido,'Aceptado por el Cliente','Ninguno') as 'Estado Repuesto', "+
                               " coti.estadocoti as 'Estado Cotizacion', "+
                               " date_format(dc.fechaentregaera,'%d/%m/%Y') as 'fechaentrega_era', "+
                               " dc.nroserial, "+
                               " dc.nrofactura   "+
                               " from "+
                               " tb_detalle_cotirepuesto dc, tb_preciorepuesto pr, tb_cotizacionrepuesto coti  "+
                               " where "+
                               " dc.codcoti = coti.codigo and  "+
                               " dc.codrepuesto = pr.codigo and "+
                               " coti.codr144_padre is not null and "+
                               " coti.codevento = "+codEvento;
            return ConecRes.consultaMySql(consulta);
        }

        /// <summary>
        /// </summary>
        /// <param name="edificio"> nombre del edificio </param>
        /// <returns>
        /// da como resultado el promedo del primera cotizacion vendida a la ultima, la fecha de hoy, 
        /// y la diferencia del ultimo cotizado y vendido al dia de hoy
        /// </returns>         
        public DataSet get_diferenciaDiasDesdeLaAprobacionDeLasCotizaciones(string edificio)
        {
            string consulta = "select "+
                               " date_format(avg(dcoti.fechaentregaera),'%d/%m/%y') AS 'PROMEDIO_Dias', "+
                               " current_date as 'Hoy_dia', "+
                               " TIMESTAMPDIFF(DAY, max(coti.fechasolicitud), current_date ) as 'Diferencias_Dias' " +
                               " from "+
                               " tb_cotizacionrepuesto coti, tb_detalle_cotirepuesto dcoti "+
                               " where "+
                               " coti.codigo = dcoti.codcoti and "+
                              // " coti.codr144_padre is not null and "+
                               " coti.vendido = true and "+
                               " coti.edificio = '"+edificio+"'";
            return ConecRes.consultaMySql(consulta);
                                
        }

        internal DataSet get_CotizacionesRepuesto(string fecha1, string fecha2)
        {
            string consulta = "select cr.codigo as 'CodigoCoti', "+
                               " date_format(cr.fecha,'%d/%m/%Y') as 'Fecha1' , "+
                               " cr.hora, "+
                               " cr.edificio as 'NombreEdificio', "+
                               " concat(eq.exbo,'-',ifnull(eq.ascensor,'')) as 'Exbo', "+
                               " cr.cite, "+
                               " cr.monto,  "+
                               " cr.une as 'Almacen', "+
                               " date_format(cr.fechasolicitud,'%d/%m/%Y') as 'FechaSolicitud1', "+
                               " cr.horasolicitud, "+
                               " date_format(cr.fechacierre,'%d/%m/%Y') as 'FechaCierre1', "+
                               " cr.horacierre, "+
                               " cr.detallecierrecotizacion, "+                               
                               " cr.vendido, cr.rechazado, "+
                               " cr.estadocoti, "+
                               " pp.nombre as 'Prioridad', "+
                               " cr.codevento as 'Tiket_CallCenter', "+
                               " cr.codr144_padre as 'R_144',"+                               
                               " res.nombre as 'ReponsableCierre' " +
                               " ,even.arearin " +
                               " ,even.arearcc " +
                               " ,even.areaera " +
                               " ,even.areacallcenter " +
                               " ,even.Observacion as 'Observacion_Tiket', " +
                               " even.observacion_evento, " +
                               " even.observacion_necesidadrepuesto " +
                               " from "+
                               " tb_cotizacionrepuesto cr "+
                               " left join tb_equipo eq on (cr.codequipo = eq.codigo)  "+
                               " left join tb_prioridad pp on cr.prioridad = pp.codigo "+
                               " left join tb_responsable res on (cr.codrespcierre = res.codigo) "+
                               " left join tb_evento even on (cr.codevento = even.codigo) "+
                               " where  "+
                               " not(cr.cite = '') and "+
                               " cr.fecha between "+fecha1+" and "+fecha2+" ";
            return ConecRes.consultaMySql(consulta);
                                
        }
    }
}