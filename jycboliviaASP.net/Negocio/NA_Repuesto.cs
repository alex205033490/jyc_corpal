using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;
using System.IO;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Repuesto
    {
        private DA_Repuesto Drepuesto = new DA_Repuesto();
        public NA_Repuesto(){}

        public bool insertarCotizacionRepuesto(string monto, string edificio, string cite, string ascensores, int codrespSolicitante, int codEvento, int codequipo)
        {
            return Drepuesto.insertarCotizacionRepuesto(  monto,  edificio,  cite,  ascensores,  codrespSolicitante, codEvento,  codequipo);
        }

        public bool insertarDetalleCotizacionRepuesto(int codcoti, int codrepuesto, double cantidad, double preciocompra) {
            return Drepuesto.insertarDetalleCotizacionRepuesto(codcoti, codrepuesto, cantidad,preciocompra);
        }

        public bool modificarCotizacionRepuesto(int codigoCoti, string estadoCoti, string detalleCierreCoti, bool vendido, bool rechazado, bool areacliente, int codResponsabeCierre, bool areaera)
        {
            return Drepuesto.updateCotizacionRepuesto(codigoCoti, estadoCoti, detalleCierreCoti, vendido, rechazado, areacliente, codResponsabeCierre, areaera);
        }

        public bool eliminar()
        {
            return false;
        }


        public DataSet getCotizacionesRepuesto_EraEntrega(string codigoCoti, string edificio, string cite)
        {
            
            string consulta = "select cr.codigo as 'Codigo Coti', "+
                               " date_format(cr.fecha,'%d/%m/%Y') as 'fecha' , "+
                               " cr.hora, "+
                               " cr.edificio as 'Nombre del Edificio', "+
                               " concat(eq.exbo,'-',ifnull(eq.ascensor,'')) as 'Exbo', " +
                               " cr.cite, "+
                               " cr.monto, "+
                               " cr.une as 'Almacen', "+
                               " cr.detallecierrecotizacion, "+
                               " cr.vendido, cr.rechazado, cr.areacliente, "+
                               " cr.estadocoti, "+
                               " cr.codevento as 'Tiket_CallCenter', "+
                               " cr.codr144_padre as 'R-144', "+
                               " pp.nombre as Prioridad "+
                               " from "+
                               " tb_cotizacionrepuesto cr "+
                               " left join tb_equipo eq on (cr.codequipo = eq.codigo)  "+
                               " left join tb_prioridad pp on (cr.prioridad = pp.codigo) "+
                               " where  "+
                               " cr.areaera = 1";

            if (!edificio.Equals(""))
            {
                consulta = consulta + " and cr.edificio like '%" + edificio + "%' ";
            }

            if (!cite.Equals(""))
            {
                consulta = consulta + " and cr.cite like '%" + cite + "%' ";
            }

            if (!codigoCoti.Equals(""))
            {
                consulta = consulta + " and cr.codigo like '%" + codigoCoti + "%' ";
            }

            consulta = consulta + " order by timestamp(cr.fecha, cr.hora) desc ";

            return Drepuesto.getDatos(consulta);
        }

        public DataSet getCotizacionesRepuestoRCC(string codigoCoti, string edificio, string cite, string estado, bool vendido, bool rechazado, bool areacliente, bool excel)
        {         
            string consulta = "select cr.codigo as 'Codigo Coti', " +
                               " date_format(cr.fecha,'%d/%m/%Y') as 'fecha' , " +
                               " cr.hora, " +
                               " cr.edificio as 'Nombre del Edificio', " +
                               " concat(eq.exbo,'-',ifnull(eq.ascensor,'')) as 'Exbo', " +
                               " cr.cite, " +
                               " cr.monto,  " +
                               " cr.une as 'Almacen'," +
                               " cr.detallecierrecotizacion, " +
                               " even.Observacion as 'Observacion de Tiket', " +
                               " even.observacion_evento, " +
                               " even.observacion_necesidadrepuesto, " +
                               " cr.vendido, cr.rechazado, cr.areacliente, " +
                               " cr.estadocoti, " +
                               " cr.codevento as 'Tiket_CallCenter', " +
                               " cr.codr144_padre as 'R-144' " +
                               " , pp.nombre as 'Prioridad' " +
                               " ,even.arearin " +
                               " ,even.arearcc " +
                               " ,even.areaera " +
                               " ,even.areacallcenter " +
                               " ,res.nombre as 'Solicitante Cotizacion'"+
                               " from " +
                               " tb_cotizacionrepuesto cr " +
                               " left join tb_equipo eq on (cr.codequipo = eq.codigo)  " +
                               " left join tb_prioridad pp on (cr.prioridad = pp.codigo) " +
                               " left join tb_evento even on (cr.codevento = even.codigo) " +
                               " left join tb_responsable res on (cr.codrespsolicitante = res.codigo)"+
                               " where  " +
                               " not(cr.cite = '') ";
                         //      " and cr.codr144_padre is not null ";
                             if (!estado.Equals("Todos"))
                               {
                                   consulta = consulta + " and cr.estadocoti = '" + estado + "' ";
                               }
                               
         consulta = consulta + " and cr.codigo not in "+
                               " ( "+
                                   " select t1.codigo from ( "+
                                   " select co.codigo from tb_evento even, tb_cotizacionrepuesto co "+
                                   " where "+
                                   " even.codigo = co.codevento and "+
                                   " even.estadoEvento = 'Abierto' and "+  
                                   " even.envioproforma is null and "+ 
                                   " even.arearcc = 1 "+
                                   " union all "+
                                   " select coti.codr144_padre as 'Codigo' from tb_cotizacionrepuesto coti "+
                                   " where "+
                                   " coti.codr144_padre is not null "+
                                   " ) as t1 "+
                                   " group by "+ 
                                   " t1.codigo "+
                                ")";

                                if(!edificio.Equals("")){
                                consulta = consulta + " and cr.edificio like '%"+edificio+"%' ";
                                }

                                if(!cite.Equals("")){
                                consulta = consulta + " and cr.cite like '%"+cite+"%' ";
                                }
                               
                                if(!codigoCoti.Equals("")){
                                    consulta = consulta + " and cr.codigo like '%" + codigoCoti + "%' ";
                                }
                               
                                if(vendido == true){
                                    consulta = consulta + " and cr.vendido = "+vendido;
                                }

                                if (rechazado == true)
                                {
                                    consulta = consulta + " and cr.rechazado = " + rechazado;
                                }

                                if (areacliente == true)
                                {
                                    consulta = consulta + " and cr.areacliente = " + areacliente;
                                }

                                consulta = consulta + " order by timestamp(cr.fecha, cr.hora) desc " ;
            if(excel == false){
                consulta = consulta + " limit 200  ";
            }       

            return Drepuesto.getDatos(consulta);
        }

        public DataSet getCotiR144(int R144) {
            string consulta = "select coti.codigo as 'CodigoCoti',  " +
                               " date_format(coti.fecha,'%d/%m/%Y') as 'fecha1' ,  " +
                               " coti.hora,  " +
                               " coti.edificio as 'Nombre_del_Edificio',  " +
                               " concat(eq.exbo,'-',eq.ascensor) as 'Exbo', " +
                               " coti.cite,  " +
                               " coti.monto,  " +
                               " coti.codevento as 'Tiket_CallCenter' , " +
                               " coti.une as 'Almacen' "+
                               " ,res.nombre as 'responsable'"+
                               " from  " +
                               " tb_cotizacionrepuesto coti  " +
                               " left join tb_equipo eq on (coti.codequipo = eq.codigo)  " +
                               " left join tb_responsable res on (coti.codrespsolicitante = res.codigo) "+
                               " where  " +
                               " coti.codigo = " + R144;
            return Drepuesto.getDatos(consulta);
        }

        public DataSet getCotiRepuestoRCC2(string codigoCoti, string edificio)
        {           
           /* string consulta = "select cr.codigo as 'Codigo Coti', "+
                               " date_format(cr.fecha,'%d/%m/%Y') as 'fecha' , "+
                               " cr.hora, "+
                               " cr.edificio as 'Nombre del Edificio', "+
                               " cr.cite, "+
                               " cr.monto,  "+
                               " dc.codcallcenter as 'Tiket_CallCenter' "+
                               " from "+
                               " tb_evento even, "+
                               " tb_detalle_callcenter_cotirepuesto dc, "+
                               " tb_cotizacionrepuesto cr "+
                               " where  "+
                               " even.codigo = dc.codcallcenter and "+
                               " cr.codigo = dc.codcotirepuesto and "+
                               " cr.estadocoti = 'Abierto' and "+
                               " cr.edificio like '%"+edificio+"%' and "+
                               " cr.codigo like '%"+codigoCoti+"%' and "+
                               " even.envioproforma is null and "+
                               " even.arearcc = 1 "+
                               " order by cr.fecha desc"; 
            
            string consulta = "select coti.codigo as 'CodigoCoti', "+
                               " date_format(coti.fecha,'%d/%m/%Y') as 'fecha' , "+
                               " coti.hora, "+
                               " coti.edificio as 'Nombre_del_Edificio', "+
                               " concat(eq.exbo,'-',eq.ascensor) as 'Exbo', "+
                               " coti.cite, "+
                               " coti.monto,  "+
                               " coti.codevento as 'Tiket_CallCenter' "+
                               " from  "+
                               " tb_evento even, "+
                               " tb_cotizacionrepuesto coti "+
                               " left join tb_equipo eq on (coti.codequipo = eq.codigo) "+
                               " where "+
                               " coti.estadocoti = 'Abierto' and  "+
                               " coti.edificio like '%"+edificio+"%' and "+
                               " coti.codigo like '%"+codigoCoti+"%'  and "+
                               " coti.codevento = even.codigo and "+
                               " even.envioproforma is null and "+
                               " even.arearcc = 1 and "+
                               " even.estadoEvento = 'Abierto' "+
                               " order by coti.codigo desc"; */
            string consulta = "select "+
                               " t1.CodigoCoti,  "+
                               " t1.fecha , "+
                               " t1.hora, "+
                               " t1.Nombre_del_Edificio, "+
                               " t1.Exbo, "+
                               " t1.cite, "+
                               " t1.monto, "+ 
                               " t1.Tiket_CallCenter "+
                               " ,pp.nombre as 'Prioridad' " +
                               " from "+
                               " ( "+
                               " select coti.codigo as 'CodigoCoti', "+
                               " date_format(coti.fecha,'%d/%m/%Y') as 'fecha' , "+
                               " coti.hora, "+
                               " coti.edificio as 'Nombre_del_Edificio', "+
                               " concat(eq.exbo,'-',ifnull(eq.ascensor,'')) as 'Exbo', " + 
                               " coti.cite, "+
                               " coti.monto, "+ 
                               " coti.codevento as 'Tiket_CallCenter' "+
                               " ,coti.prioridad "+
                               " from  "+
                               " tb_evento even, "+
                               " tb_cotizacionrepuesto coti "+
                               " left join tb_equipo eq on (coti.codequipo = eq.codigo) "+
                               " where "+
                               " coti.estadocoti = 'Abierto' and  "+
                               " coti.edificio like '%"+edificio+"%' and "+
                               " coti.codigo like '%"+codigoCoti+"%'  and "+
                               " coti.codevento = even.codigo and "+
                               " even.envioproforma is null and "+
                               " even.arearcc = 1 and "+
                               " even.estadoEvento = 'Abierto' "+
                               " union "+
                               "select "+
                               " coti.codigo as 'CodigoCoti', "+
                               " date_format(coti.fecha,'%d/%m/%Y') as 'fecha' , "+
                               " coti.hora, "+
                               " coti.edificio as 'Nombre_del_Edificio', "+
                               " concat(eq.exbo,'-',eq.ascensor) as 'Exbo', "+  
                               " coti.cite,  "+
                               " coti.monto, "+  
                               " coti.codevento as 'Tiket_CallCenter' "+
                               " ,coti.prioridad " +
                               " from  "+
                               " tb_cotizacionrepuesto coti "+
                               " left join tb_equipo eq on (coti.codequipo = eq.codigo) "+
                               " where "+
                               " coti.estadocoti = 'Abierto' and "+ 
                               " coti.edificio like '%%' and "+
                               " coti.codigo like '%%'  and "+
                               " coti.codevento is null and "+
                               " coti.codr144_padre is null and "+
                               " coti.une is null and "+
                               " coti.fecha > '2018-06-15' and "+
                               " coti.codigo not in "+
                               " (select bb.codr144_padre "+
                               " from tb_cotizacionrepuesto bb "+
                               " where bb.codr144_padre is not null "+
                               " group by bb.codr144_padre) "+                               
                               " ) AS t1 "+
                               "LEFT JOIN tb_prioridad pp "+
                               " ON t1.prioridad = pp.codigo "+
                               " order by t1.CodigoCoti desc";
            return Drepuesto.getDatos(consulta);
        }

   /*     public DataSet getCotizacionesRepuestoJyC_otro(string codigoCoti, string edificio, string cite, string estadoCoti, bool vendido, bool rechazado, bool areacliente)
        {
            string consulta = "select coti.codigo as 'Codigo Coti', "+
                               " date_format(coti.fecha,'%d/%m/%Y') as 'fecha' , "+
                               " coti.hora, "+
                               " coti.edificio as 'Nombre_Edificio',  "+
                               " coti.cite,  "+
                               " coti.monto, "+
                               " coti.detallecierrecotizacion, "+
                               " coti.vendido, coti.rechazado, coti.areacliente, " +
                               " coti.estadocoti," +
                               " coti.codevento as 'Tiket_CallCenter' " +
                               " from tb_detalle_cotirepuesto dc, tb_cotizacionrepuesto coti "+                               
                               " where  "+
                               " coti.codigo = dc.codcoti and "+
                               " dc.almacenjyc = true  and " +
                               " not(coti.cite = '') and "+
                               " coti.estadocoti = '"+estadoCoti+"'" ;
                                if(!codigoCoti.Equals("")){
                                    consulta = consulta + " and dc.codcoti like '%" + codigoCoti + "%' ";
                                 }

                                if(!edificio.Equals("")){
                                    consulta = consulta + " and coti.edificio like '%" + edificio + "%' ";
                                }
                               
                               if(!cite.Equals("")){
                                   consulta = consulta + " and coti.cite like '%" + cite + "%' ";
                               }

                               if (vendido == true)
                               {
                                   consulta = consulta + " and coti.vendido = " + vendido;
                               }

                               if (rechazado == true)
                               {
                                   consulta = consulta + " and coti.rechazado = " + rechazado;
                               }

                               if (areacliente == true)
                               {
                                   consulta = consulta + " and coti.areacliente = " + areacliente;
                               } 


                    consulta = consulta +" group by coti.codigo";
            return Drepuesto.getDatos(consulta);
        }

        */

        public DataSet getRepuestoCotizacionDetalle(int codigoCotizacion)
        {
              string consulta = "select "+
                                 " pr.codigo as 'Numeracion',  "+
                                 " pr.numeracion as 'CodigoRepuesto', pr.denominacion as 'Denominacion', "+
                                 " dc.cantidad, "+
                                 "  date_format(dc.fechaentregaera,'%d/%m/%Y') as 'fechaentrega_era', "+
                                 " dc.nroserial, "+
                                 " dc.nrofactura   "+
                                 " from "+
                                 " tb_detalle_cotirepuesto dc, tb_preciorepuesto pr  "+
                                 " where  "+
                                 " dc.codrepuesto = pr.codigo and "+
                                 " dc.codcoti = "+codigoCotizacion;
           
            return Drepuesto.getDatos(consulta);
        }

        public DataSet getRepuestoCotizacionDetalle_RCC(int codigoCotizacion) {
          
            string consulta = "select " +
                              " pr.codigo as 'Numeracion',  " +
                              " pr.numeracion as 'CodigoRepuesto', pr.denominacion as '________________Denominacion________________'," +
                              " dc.cantidad, " +
                              " dc.preciocompra as 'precioUnitario',  " +
                              " (dc.preciocompra * dc.cantidad)as precioTotal " +                              
                              " from " +
                              " tb_detalle_cotirepuesto dc, tb_preciorepuesto pr " +
                              " where " +
                              " dc.codrepuesto = pr.codigo and " +
                              " dc.codcoti = " + codigoCotizacion;
            return Drepuesto.getDatos(consulta);         
        }

        public DataSet getRepuestoCotizacionDetalleR114(int codigoCotizacion)
        {
            string consulta = "select " +
                               " pr.codigo as 'Numeracion',  " +
                               " pr.numeracion as 'CodigoRepuesto', pr.denominacion as '________________Denominacion________________'," +
                               " dc.cantidad, " +
                               " dc.preciocompra as 'precioUnitario',  " +
                               " (dc.preciocompra * dc.cantidad)as precioTotal, " +
                               " ifnull(dc.cant_almacenlocal,0) as 'cant_almacenlocal', "+
                               " ifnull(dc.cant_almacenjyc,0) as 'cant_almacenjyc', "+
                               " ifnull(dc.cant_almacenjycia,0) as 'cant_almacenjycia' "+
                               " from " +
                               " tb_detalle_cotirepuesto dc, tb_preciorepuesto pr " +
                               " where " +
                               " dc.codrepuesto = pr.codigo and " +
                               " dc.codcoti = " + codigoCotizacion;
            return Drepuesto.getDatos(consulta);
        }


        public int getultimaCotizacionRepuestoSiguiente() {
            try {
                string consulta = "select max(cr.codigo)+1 as 'coti' from tb_cotizacionrepuesto cr";
                DataSet tupla = Drepuesto.getDatos(consulta);
                int codigo = Convert.ToInt32(tupla.Tables[0].Rows[0][0].ToString());
                return codigo;
            }
            catch (Exception)
            {
                return 1;
            }            
        }

        public int getultimaCotizacionRepuestoInsertado()
        {
            try
            {
                string consulta = "select max(cr.codigo) as 'coti' from tb_cotizacionrepuesto cr";
                DataSet tupla = Drepuesto.getDatos(consulta);
                int codigo = Convert.ToInt32(tupla.Tables[0].Rows[0][0].ToString());
                return codigo;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public DataSet mostrarRepuestos(string numeracion, string detalle)
        {
            return Drepuesto.mostrarRepuestos(numeracion, detalle);
        }

  
    /*   public bool estaEntregadoelRepuestoJyC(int codCotizacionRepuesto, int codRepuesto)
        {
            try
            {
                string consulta = "select " +
                                   " dc.gentregajyc  " +
                                   " from tb_detalle_cotirepuesto dc " +
                                   " where dc.codcoti = " + codCotizacionRepuesto + " and " +
                                   " dc.codrepuesto = " + codRepuesto;
                DataSet resultado = Drepuesto.getDatos(consulta);
                bool dato = Convert.ToBoolean(resultado.Tables[0].Rows[0][0].ToString());
                
                    return dato;
                
            }
            catch (IOException e) {
                return false;
            
            }

        }
        

        public bool estaEntregadoelRepuestoJyCIA(int codCotizacionRepuesto, int codRepuesto)
        {
            try
            {
                string consulta = "select " +
                                   " dc.gentregajycia  " +
                                   " from tb_detalle_cotirepuesto dc " +
                                   " where dc.codcoti = " + codCotizacionRepuesto + " and " +
                                   " dc.codrepuesto = " + codRepuesto;
                DataSet resultado = Drepuesto.getDatos(consulta);
                bool dato = Convert.ToBoolean(resultado.Tables[0].Rows[0][0].ToString());

                return dato;

            }
            catch (IOException e)
            {
                return false;

            }

        }


        public bool estaEntregadoelRepuestoLocal(int codCotizacionRepuesto, int codRepuesto)
        {
            try
            {
                string consulta = "select " +
                                   " dc.gentregalocal  " +
                                   " from tb_detalle_cotirepuesto dc " +
                                   " where dc.codcoti = " + codCotizacionRepuesto + " and " +
                                   " dc.codrepuesto = " + codRepuesto;
                DataSet resultado = Drepuesto.getDatos(consulta);
                bool dato = Convert.ToBoolean(resultado.Tables[0].Rows[0][0].ToString());
                return dato;
            }
            catch (IOException e)
            {
                return false;

            }

        }
        


        public bool estaEnProcesodeCompraRepuesto(int codCotizacionRepuesto, int codRepuesto)
        {
            try
            {
                string consulta = "select " +
                                   " dc.gcompra  " +
                                   " from tb_detalle_cotirepuesto dc " +
                                   " where dc.codcoti = " + codCotizacionRepuesto + " and " +
                                   " dc.codrepuesto = " + codRepuesto;
                DataSet resultado = Drepuesto.getDatos(consulta);
                bool dato = Convert.ToBoolean(resultado.Tables[0].Rows[0][0].ToString());
                return dato;
            }
            catch (IOException e)
            {
                return false;

            }

        }


        public bool actualizarAlmacenLocal(int codcotizacion, int codRepuesto, bool bandera)
        {
            return Drepuesto.actualizarAlmacenLocal(codcotizacion, codRepuesto, bandera);
        }

        public bool estaenAlmacenLocal(int codcotizacion, int codRepuesto) {
            try
            {
                string consulta = "select " +
                                   " dc.almacenlocal  " +
                                   " from tb_detalle_cotirepuesto dc " +
                                   " where dc.codcoti = " + codcotizacion + " and " +
                                   " dc.codrepuesto = " + codRepuesto;
                DataSet resultado = Drepuesto.getDatos(consulta);
                bool dato = Convert.ToBoolean(resultado.Tables[0].Rows[0][0].ToString());
                return dato;
            }
            catch (IOException e)
            {
                return false;

            }
        
        }

        public bool actualizarAlmacenJYC(int codcotizacion, int codRepuesto, bool bandera)
        {
            return Drepuesto.actualizarAlmacenJYC(codcotizacion, codRepuesto, bandera);
        }


        public bool estaenAlmacenJyC(int codcotizacion, int codRepuesto)
        {
            try
            {
                string consulta = "select " +
                                   " dc.almacenjyc  " +
                                   " from tb_detalle_cotirepuesto dc " +
                                   " where dc.codcoti = " + codcotizacion + " and " +
                                   " dc.codrepuesto = " + codRepuesto;
                DataSet resultado = Drepuesto.getDatos(consulta);
                bool dato = Convert.ToBoolean(resultado.Tables[0].Rows[0][0].ToString());
                return dato;
            }
            catch (IOException e)
            {
                return false;

            }

        }

    
        public bool actualizarCompraEnProceso(int codcotizacion, int codRepuesto, bool bandera)
        {
            return Drepuesto.actualizarCompraEnProceso(codcotizacion, codRepuesto, bandera);
        }
        

        public bool actualizarEntregaRepuestoJYC(int codcotizacion, int codRepuesto, bool bandera)
        {
            return Drepuesto.actualizarEntregaRepuestoJYC(codcotizacion, codRepuesto, bandera);
        }
        

        public bool actualizarEntregadoRepuestoLocal(int codcotizacion, int codRepuesto, bool bandera)
        {
            return Drepuesto.actualizarEntregadoRepuestoLocal(codcotizacion, codRepuesto, bandera);
        }


         public bool actualizarEntregaRepuestoJYCIA(int codcotizacion, int codRepuesto, bool bandera)
        {
            return Drepuesto.actualizarEntregaRepuestoJYCIA(codcotizacion, codRepuesto, bandera);
         }
        */
         public DataSet getNumeracionRepuesto(string Numeracion)
         {
             string consulta = "select pre.numeracion from tb_preciorepuesto pre where "+
             " pre.estado = 1 and "+
             " pre.numeracion like '%"+Numeracion+"%'";
             return Drepuesto.getDatos(consulta);
         }


         public DataSet getItemCoti(int codCoti) {
             string consulta = "select  "+
                               " dc.codrepuesto, "+
                               " p.numeracion, "+
                               " p.denominacion as 'Detalle', "+
                               " dc.preciocompra as 'P.Unit.', "+
                               " dc.cantidad as 'Cantidad',  "+
                               " (dc.cantidad * dc.preciocompra) as 'Precio Total' "+
                               " from tb_detalle_cotirepuesto dc ,tb_preciorepuesto p "+
                               " where "+
                               " p.codigo = dc.codrepuesto and "+
                               " dc.codcoti = "+codCoti;
                               
             return Drepuesto.getDatos(consulta);         
         }

         public bool updateCiteCotizacion1(int codcoti, string cite) {
             return Drepuesto.updateCiteCotizacion(codcoti, cite);
         }

         public bool updateFechaEventoCotizacion(int codevento, string fechaEnvioProforma)
         {
             return Drepuesto.updateFechaEventoCotizacion(codevento, fechaEnvioProforma);
         }


        public static bool IsNumeric(string Expression)
            {
                double retNum;
                bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
                return isNum;
            }
         
         public bool tieneEventoCallCenterAsignado(int codCoti) {
            /* string consulta = "select * from tb_detalle_callcenter_cotirepuesto cr "+
                                   " where cr.codcotirepuesto = "+codCoti; */
             string consulta = "select coti.codevento from tb_cotizacionrepuesto coti " +
                               " where coti.codigo = "+codCoti;
             DataSet datos = Drepuesto.getDatos(consulta);   
             string codigoEvento = datos.Tables[0].Rows[0][0].ToString();
           //  if(datos.Tables[0].Rows.Count > 0 ){
               if(IsNumeric(codigoEvento)){
                 return true;
             }else
                 return false;
         }


        
         public int getCodigoEventoCallCenterAsignado(int codCoti)
         {
            /* string consulta = "select * from tb_detalle_callcenter_cotirepuesto cr " +
                                   " where cr.codcotirepuesto = " + codCoti; */
             string consulta = "select coti.codevento from tb_cotizacionrepuesto coti " +
                               " where coti.codigo = "+codCoti;
             DataSet datos = Drepuesto.getDatos(consulta);
             string codigoEvento = datos.Tables[0].Rows[0][0].ToString();

             //if (datos.Tables[0].Rows.Count > 0)
             if(IsNumeric(codigoEvento))
             {
                 return Convert.ToInt32(datos.Tables[0].Rows[0][0].ToString());
             }
             else
                 return -1;
         }

         public int getCantidadCotizacionesRepuesto(int codigoProyecto) {
            /* string consulta = "select  "+
                               " count(even.codEdificio) "+
                               " from tb_detalle_callcenter_cotirepuesto dcoti, tb_evento even, tb_cotizacionrepuesto coti "+
                               " where dcoti.codcallcenter = even.codigo and "+
                               " dcoti.codcotirepuesto = coti.codigo and "+
                               " even.estadoEvento = 'Abierto'  and "+
                               " even.codEdificio = "+codigoProyecto+
                               " group by even.codEdificio";  */
             string consulta = "select "+ 
                                " count(even.codEdificio)  "+
                                " from  "+
                                " tb_evento even, tb_cotizacionrepuesto coti "+
                                " where "+
                                " coti.codevento = even.codigo and "+
                                " even.estadoEvento = 'Abierto'  and "+
                                " coti.codr144_padre is not null and "+
                                " even.codEdificio = "+codigoProyecto +
                                " group by even.codEdificio";
             DataSet datos = Drepuesto.getDatos(consulta);
             if (datos.Tables[0].Rows.Count > 0)
             {
                 return Convert.ToInt32(datos.Tables[0].Rows[0][0].ToString());
             }
             else
                 return 0;
         }

         

         public float getTotalR144_Cotizacion(int codR144)
         {
             try
             {
                 string consulta = "select "+
                                    " sum(dc.cantidad) as total "+
                                    " from   "+
                                    " tb_detalle_cotirepuesto dc, tb_preciorepuesto pr  "+
                                    " where  "+
                                    " dc.codrepuesto = pr.codigo and "+
                                    " dc.codcoti = "+codR144;
                 DataSet tupla = Drepuesto.getDatos(consulta);
                 float codigo = Convert.ToSingle(tupla.Tables[0].Rows[0][0].ToString());
                 return codigo;
             }
             catch (Exception)
             {
                 return -1;
             }
         }


         public bool updateAlmacenesR144(int codCoti, int codRepuesto, string almacen_Local, string almacen_JyC, string almacen_JyCIA)
         {
             return Drepuesto.updateAlmacenesR144(codCoti, codRepuesto, almacen_Local, almacen_JyC, almacen_JyCIA);
         }
        
         public bool tieneRepuestosLocal(int codR144)
         {
             try
             {
                 string consulta = "select sum(tb_detalle_cotirepuesto.cant_almacenlocal) as total "+
                                   " from tb_detalle_cotirepuesto "+
                                   " where "+
                                   " tb_detalle_cotirepuesto.codcoti = " + codR144;
                 DataSet tupla = Drepuesto.getDatos(consulta);
                 float total = Convert.ToSingle(tupla.Tables[0].Rows[0][0].ToString().Replace('.',','));
                 if (total > 0)
                 {
                     return true;
                 }
                 else
                     return false;
             }
             catch (Exception)
             {
                 return false;
             }
         }

         public bool tieneRepuestosJyC(int codR144)
         {
             try
             {
                 string consulta = "select sum(tb_detalle_cotirepuesto.cant_almacenjyc) as total " +
                                   " from tb_detalle_cotirepuesto " +
                                   " where " +
                                   " tb_detalle_cotirepuesto.codcoti = " + codR144;
                 DataSet tupla = Drepuesto.getDatos(consulta);
                 float total = Convert.ToSingle(tupla.Tables[0].Rows[0][0].ToString().Replace('.', ','));
                 if (total > 0)
                 {
                     return true;
                 }
                 else
                     return false;
             }
             catch (Exception)
             {
                 return false;
             }
         }

         public bool tieneRepuestosJyCIA(int codR144)
         {
             try
             {
                 string consulta = "select sum(tb_detalle_cotirepuesto.cant_almacenjycia) as total " +
                                   " from tb_detalle_cotirepuesto " +
                                   " where " +
                                   " tb_detalle_cotirepuesto.codcoti = " + codR144;
                 DataSet tupla = Drepuesto.getDatos(consulta);
                 float total = Convert.ToSingle(tupla.Tables[0].Rows[0][0].ToString().Replace('.', ','));
                 if (total > 0)
                 {
                     return true;
                 }
                 else
                     return false;
             }
             catch (Exception)
             {
                 return false;
             }
         }

         public bool crearCotizacionSeparadaAlmacenLocal(int codR144)
         {
             return Drepuesto.crearCotizacionSeparadaAlmacenLocal(codR144);     
         }

         public bool crearCotizacionSeparadaAlmacenJYC(int codR144)
         {
             return Drepuesto.crearCotizacionSeparadaAlmacenJYC(codR144);
         }

         public bool crearCotizacionSeparadaAlmacenJYCIA(int codR144)
         {
             return Drepuesto.crearCotizacionSeparadaAlmacenJYCIA(codR144);
         }


         public bool cerrarR144(int codR144, int codResponsable, string detalle)
         {
             return Drepuesto.cerrarR144(codR144,codResponsable, detalle);
         }

         public DataSet getDetallesRepuestoR144(int codR144)
         {
             return Drepuesto.getDetallesRepuestoR144(codR144);
         }

         public bool actualizar_DatosEra(int codigoCoti, int codigoRepuesto, string fechaentrega_ERA, string nroSerial, string nroFactura)
         {
            return Drepuesto.actualizar_DatosEra( codigoCoti,  codigoRepuesto,  fechaentrega_ERA,  nroSerial,  nroFactura);
         }

         public int getCantidadRepuestodeCotizacion(int codCoti)
         {
             DataSet tuplas = Drepuesto.getCantidadRepuestodeCotizacion(codCoti);
             if (tuplas.Tables[0].Rows.Count > 0)
             {
                 int cantidad = Convert.ToInt32(tuplas.Tables[0].Rows[0][0].ToString());
                 return cantidad;
             }
             else
                 return -1;
         }

         public int getCantidadRepuesto_Entregados(int codCoti)
         {
             DataSet tuplas = Drepuesto.getCantidadRepuesto_Entregados(codCoti);
             if (tuplas.Tables[0].Rows.Count > 0)
             {
                 int cantidad = Convert.ToInt32(tuplas.Tables[0].Rows[0][0].ToString());
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

         public bool pasarEventoalRIN_evento(int codCoti)
         {             
             DataSet tuplaEvento = Drepuesto.geteventoCotizacion(codCoti);
             int codevento;
             bool esnumero = int.TryParse(tuplaEvento.Tables[0].Rows[0][0].ToString(), out codevento);
             if (esnumero)
             {
                 NA_Evento evento = new NA_Evento();
                 bool sw2 = evento.pasarEventoalAreaRIN(codevento);
                 return sw2;
             }
             else
                 return false;
             
         }


         public bool cerrarCotizacion_era2(int codCoti)
         {
             bool bandera = Drepuesto.cerrarCotizacion_era(codCoti);
             return bandera;
         }



         public DataSet getRepuestosSolicitadosporEvento(int codEvento)
         {
             DataSet tuplas = Drepuesto.getRepuestosSolicitadosporEvento(codEvento);
             return tuplas;
         }

         /// <summary>
         /// </summary>
         /// <param name="edificio"> nombre del edificio </param>
         /// <returns>
         /// da como resultado la diferencia de dias del ultimo cotizado y vendido al dia de hoy
         /// si no tiene cotizaciones da como resultado 0
         /// </returns>
         public int get_diferenciaDiasDesdeLaAprobacionDeLasCotizaciones(int codProyecto, string edificio) {
             NProyecto nproy = new NProyecto();
             if (nproy.tieneDeudasRepuestoPendientesProyecto(codProyecto))
             {
                 DataSet tuplas = Drepuesto.get_diferenciaDiasDesdeLaAprobacionDeLasCotizaciones(edificio);
                 if (tuplas.Tables[0].Rows.Count > 0)
                 {
                     int diferencia;
                     int.TryParse(tuplas.Tables[0].Rows[0][2].ToString(), out diferencia);
                     return diferencia;
                 }
                 else
                     return 0;
             }
             else
                 return 0;
         }

         internal DataSet get_CotizacionesRepuesto(string fecha1, string fecha2)
         {
             return Drepuesto.get_CotizacionesRepuesto(fecha1,fecha2);
         }
    }
}