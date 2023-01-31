using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_RutaMantenimiento
    {
        private conexionMySql ConecRes = new conexionMySql();
        
        public DA_RutaMantenimiento() { }

        public DataSet getallRutas(int mes, int anio) {
            string consulta = "select "+
                               " ru.codigo, "+
                               " ru.numero, "+
                               " ru.detalle, "+
                               " ru.fecha, "+
                               " ru.hora, "+
                               " ru.mes, "+
                               " ru.anio "+
                               " from tb_ruta ru "+
                               " where  "+
                               " ru.mes = "+mes+" and "+
                               " ru.anio = "+anio;
            return ConecRes.consultaMySql(consulta);
        }


        public DataSet getallEquiposProyectoRutaMantenimiento(string edificio)
        {
            string consulta = "select "+
                               " eq.codigo, "+
                               " eq.exbo, "+
                               " proy.nombre as 'Edificio', "+
                               " te.nombre as 'Tipo', "+
                               " m.nombre as 'Marca' "+
                               " from  tb_proyecto proy, "+
                               " tb_fechaestadoequipo ff,  "+
                               " tb_equipo eq  "+
                               " left join tb_marca m on eq.codmarca = m.codigo "+
                               " left join tb_tipoequipo te on eq.codtipoequipo = te.codigo "+
                               " where  "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " eq.codfechaestadoequipo = ff.codigo and " +
                               " ff.codEstadoEquipo = 10 and "+
                               " proy.nombre like '%"+edificio+"%'";
            return ConecRes.consultaMySql(consulta);
        }

        public DataSet getallEquiposProyecto(string edificio) {
            string consulta = "select "+
                               " eq.codigo, "+
                               " eq.exbo, "+
                               " proy.nombre as 'Edificio', "+
                               " te.nombre as 'Tipo', "+
                               " m.nombre as 'Marca' "+
                               " from  tb_proyecto proy, "+
                               " tb_equipo eq  "+
                               " left join tb_marca m on eq.codmarca = m.codigo "+
                               " left join tb_tipoequipo te on eq.codtipoequipo = te.codigo "+
                               " where  "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " proy.nombre like '%"+edificio+"%'";
            return ConecRes.consultaMySql(consulta);
        }


        public DataSet getAllFaltantesEquiposSinRutas(string Edificio, int mes, int anio) {
            string consulta = "select "+
                               " eq.codigo, "+
                               " eq.exbo, "+
                               " proy.nombre as 'Edificio', "+
                               " te.nombre as 'Tipo', "+
                               " m.nombre as 'Marca' "+
                               " from  tb_proyecto proy, "+
                               " tb_seguimiento seg, "+
                               " tb_fechaestadoequipo feq, "+
                               " tb_equipo eq   "+
                               " left join tb_marca m on eq.codmarca = m.codigo "+
                               " left join tb_tipoequipo te on eq.codtipoequipo = te.codigo "+
                               " left join "+
                               " ( "+
                               " select re.codeq from tb_cronogramavisitarutamanteminieto re  "+
                               " where re.codmes = "+mes+" and re.anio = "+anio+
                               " group by re.codeq "+
                               " ) as t1  "+
                               " ON eq.codigo = t1.codeq "+
                               " where "+
                               " t1.codeq is null and "+
                               " eq.codigo = seg.cod_equipo and "+
                               " seg.years = "+anio+" and "+
                               " eq.estado = 1 and "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " eq.codfechaestadoequipo = feq.codigo and "+
                               " feq.codEstadoEquipo = 10 and "+
                               " proy.nombre like '%"+Edificio+"%' "+
                               " order by eq.codigo asc";
            return ConecRes.consultaMySql(consulta);
        }


        public bool insertarRutaMantenimiento(int nro, string detalle, int mes, int anio)
        {
            string consulta = "insert into tb_ruta(tb_ruta.nro,tb_ruta.detalle,tb_ruta.fecha,tb_ruta.hora,tb_ruta.estado,tb_ruta.mes,tb_ruta.anio) " +
                               " values("+nro+",'"+detalle+"',now(),now(),1,"+mes+","+anio+");";
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getallRutaMantenimiento(string nombre, int mes , int anio) {
            string consulta = "select "+ 
                               " ru.codigo, "+
                               " ru.nro as 'NRO', " +
                               " ru.detalle, "+
                               " date_format(ru.fecha,'%d/%m/%Y') as 'FechaCreacion', "+
                               " ru.hora as 'HoraCreacion' "+
                               " from tb_ruta ru "+
                               " where ru.detalle like '%"+nombre+"%' "+
                               " and ru.mes = "+mes+ " and ru.anio= "+anio;
            return ConecRes.consultaMySql(consulta);
        }


        public DataSet getallRutaMantenimiento2(string codigo, string nombre, int mes, int anio)
        {
            string consulta = "select " +
                               " ru.codigo, " +
                               " ru.detalle, " +
                               " date_format(ru.fecha,'%d/%m/%Y') as 'FechaCreacion', " +
                               " ru.hora as 'HoraCreacion', " +
                               " ru.mes, ru.anio "+
                               " from tb_ruta ru where ru.detalle like '%" + nombre + "%' and "+
                               " ru.codigo like '%"+codigo+"' and "+
                               " ru.mes = " + mes + " and ru.anio= " + anio; 
            return ConecRes.consultaMySql(consulta);
        }


         public DataSet getallEquiposRutasAsignadas(int codRuta, string nombreEdificio)
        {
            string consulta = "select   "+
                               " eq.codigo,  "+
                               " proy.nombre as 'Edificio', "+
                               " eq.exbo,  "+
                               " eq.ascensor, "+ 
                               " te.nombre as 'Tipo',   "+
                               " m.nombre as 'Marca', "+
                               " dre.horaentrada,  "+
                               " dre.horasalida,  "+
                               " TIMEDIFF(dre.horasalida,dre.horaentrada) as 'TiempoTotal', "+
                               " dre.nrodia, "+ 
                               " dre.diasemana, "+  
                               " dre.nrovisita, "+
                               " dre.pasaje, "+
                               " dre.semana1, "+
                               " dre.semana2, "+
                               " dre.semana3, "+
                               " dre.semana4 "+
                               " from tb_cronogramavisitarutamanteminieto dre, tb_proyecto proy,   "+
                               " tb_equipo eq "+
                               " left join tb_marca m on eq.codmarca = m.codigo  "+
                               " left join tb_tipoequipo te on eq.codtipoequipo = te.codigo  "+
                               " where "+
                               " dre.codeq = eq.codigo and "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " dre.codruta = "+codRuta+" and "+
                               " proy.nombre like '%"+nombreEdificio+"%'"+ 
                               " order by dre.nrodia, dre.horaentrada asc";                                 
            return ConecRes.consultaMySql(consulta);
        }


         public DataSet getallEquiposRutasAsignadas2(string exbo, string edificio, int mes , int anio)
         {
             string consulta =     "select "+
                                   " eq.codigo, "+
                                   " eq.exbo, "+
                                   " proy.nombre as 'Edificio', "+
                                   " te.nombre as 'Tipo',   "+
                                   " m.nombre as 'Marca',  "+
                                   " dre.horaentrada,  "+
                                   " dre.horasalida,  "+
                                   " TIMEDIFF(dre.horasalida,dre.horaentrada) as 'TiempoTotal', "+
                                   " dre.diasemana, "+
                                   " dre.nrovisita "+
                                   " from tb_cronogramavisitarutamanteminieto dre, tb_proyecto proy, "+
                                   " tb_equipo eq  "+
                                   " left join tb_marca m on eq.codmarca = m.codigo  "+
                                   " left join tb_tipoequipo te on eq.codtipoequipo = te.codigo  "+
                                   " where  "+
                                   " dre.codeq = eq.codigo and "+
                                   " eq.cod_proyecto = proy.codigo and  "+                                   
                                   " dre.codmes = "+mes+" and "+
                                   " dre.anio = "+anio+
                                   " and eq.exbo like '%"+exbo+"%' and  "+
                                   " proy.nombre like '%"+edificio+"%' "+
                                   " order by dre.nrodia,dre.horaentrada asc ";
             return ConecRes.consultaMySql(consulta);
         }


         public bool modificarRutaMantenimiento(int codigo, int nro , string detalle, int mes, int anio)
         {
             string consulta = "update tb_ruta set tb_ruta.detalle = '"+detalle+"',"+
                                " tb_ruta.nro = "+nro+        
                                " where tb_ruta.codigo = "+codigo+
                                " and tb_ruta.mes= "+mes+
                                " and tb_ruta.anio="+anio;
             return ConecRes.ejecutarMySql(consulta);
         }

   /*      public bool insertarEquipoRuta(int codRuta, int codEquipo, string horaEntrada, string horasalida, string dia, int cantvisitas, int nrodia, bool semana1, bool semana2, bool semana3, bool semana4, float pasaje)
         {
             string consulta = "insert tb_cronogramavisitarutamanteminieto( "+
                                " tb_cronogramavisitarutamanteminieto.codruta, "+
                                " tb_cronogramavisitarutamanteminieto.codequipo, "+
                                " tb_cronogramavisitarutamanteminieto.horaentrada, "+
                                " tb_cronogramavisitarutamanteminieto.horasalida, "+
                                " tb_cronogramavisitarutamanteminieto.diasemana, "+
                                " tb_cronogramavisitarutamanteminieto.cantvisita, "+
                                " tb_cronogramavisitarutamanteminieto.fecha, "+
                                " tb_cronogramavisitarutamanteminieto.hora, "+
                                " tb_cronogramavisitarutamanteminieto.nrodia, "+
                                " tb_cronogramavisitarutamanteminieto.semana1, "+
                                " tb_cronogramavisitarutamanteminieto.semana2, "+
                                " tb_cronogramavisitarutamanteminieto.semana3, "+
                                " tb_cronogramavisitarutamanteminieto.semana4, "+
                                " tb_cronogramavisitarutamanteminieto.pasaje " +
                                " ) values "+
                                " ( "+
                                codRuta+" , "+
                                codEquipo+" , "+
                                "'"+horaEntrada+"', "+
                                "'"+horasalida+"' , "+
                                "'"+dia+"' , "+
                                cantvisitas+" , "+
                                " now(), "+
                                " now(), "+
                                nrodia+ " , "+
                                semana1+ ", "+
                                semana2+", "+
                                semana3+", "+
                                semana4+", "+
                                "'"+pasaje.ToString().Replace(',','.')+"'"+
                                ")";
             return ConecRes.ejecutarMySql(consulta);
         }*/


      /*   public bool ModificarEquipoRuta(int codRuta, int codEquipo, string horaEntrada, string horasalida, string dia, int cantvisitas, int nrodia, bool semana1, bool semana2, bool semana3, bool semana4, float pasaje)
         {
             string consulta = "update tb_cronogramavisitarutamanteminieto " +
                                " set " +
                                " tb_cronogramavisitarutamanteminieto.horaentrada = '" + horaEntrada + "', " +
                                " tb_cronogramavisitarutamanteminieto.horasalida = '" + horasalida + "', " +
                                " tb_cronogramavisitarutamanteminieto.diasemana = '" + dia + "', " +
                                " tb_cronogramavisitarutamanteminieto.cantvisita = " + cantvisitas + ", " +
                                " tb_cronogramavisitarutamanteminieto.nrodia = " + nrodia +" , "+
                                " tb_cronogramavisitarutamanteminieto.semana1 = "+semana1+" , " +
                                " tb_cronogramavisitarutamanteminieto.semana2 = "+semana2+", " +
                                " tb_cronogramavisitarutamanteminieto.semana3 = "+semana3+", " +
                                " tb_cronogramavisitarutamanteminieto.semana4 = "+semana4+", "+
                                " tb_cronogramavisitarutamanteminieto.pasaje = '" + pasaje.ToString().Replace(',','.') + "' " +
                                " where " +
                                " tb_cronogramavisitarutamanteminieto.codruta = " + codRuta +
                                " and tb_cronogramavisitarutamanteminieto.codequipo = " + codEquipo;
             return ConecRes.ejecutarMySql(consulta);
         }*/


         public bool insertarTecnicoRuta(int codRuta, int codTecnico, string supervisor, int mes, int anio)
         {
             string consulta = "insert into tb_detalle_rutatecnicom "+
                               " ( "+
                               " tb_detalle_rutatecnicom.codruta, "+
                               " tb_detalle_rutatecnicom.codtec, "+
                               " tb_detalle_rutatecnicom.supervisor, "+                               
                               " tb_detalle_rutatecnicom.fecha, "+
                               " tb_detalle_rutatecnicom.hora, "+
                               " tb_detalle_rutatecnicom.estado, "+
                               " tb_detalle_rutatecnicom.mes, " +
                               " tb_detalle_rutatecnicom.anio " +
                               " ) values "+
                               " ( "+
                               codRuta+" , "+
                               codTecnico+" , "+
                               "'"+ supervisor+"' , "+                              
                               " now(), "+
                               " now(), "+
                               " 1 ,"+
                               + mes+ ","+
                               anio+
                               " )";
             return ConecRes.ejecutarMySql(consulta);
         }


         public DataSet mostrarTecnicoRuta(int codRuta)
         {
             string consulta = "select res.codigo, res.nombre, " +
                               " date_format(dt.fecha,'%d/%m/%Y') as 'FechaAsignacion', " +
                               " dt.hora as 'HoraAsignacion', " +
                               " dt.supervisor " +
                               " from tb_detalle_rutatecnicom dt, tb_responsable res " +
                               " where  " +
                               " dt.codtec = res.codigo and " +
                               " dt.codruta = " + codRuta;
             return ConecRes.consultaMySql(consulta);
         }

         public DataSet mostrarTecnicoRuta(int mes, int anio)
         {
             string consulta = "select res.codigo, res.nombre, "+
                               " date_format(dt.fecha,'%d/%m/%Y') as 'FechaAsignacion', "+
                               " dt.hora as 'HoraAsignacion', "+
                               " dt.supervisor "+
                               " from tb_detalle_rutatecnicom dt, tb_responsable res "+
                               " where  "+
                               " dt.codtec = res.codigo and "+
                               " dt.mes = "+mes+" and "+
                               " dt.anio = "+anio;
             return ConecRes.consultaMySql(consulta);
         }

         public DataSet mostrarTecnicoRuta2(string nombre, int mes, int anio)
         {
             string consulta = "select "+
                               " T1.codigo, T1.nombre, T1.FechaAsignacion, T1.HoraAsignacion, "+
                               " T1.supervisor as 'Tipo'  "+
                               " from "+
                               " ( "+
                               " select res.codigo, res.nombre, "+
                               " date_format(dt.fecha,'%d/%m/%Y') as 'FechaAsignacion', "+
                               " dt.hora as 'HoraAsignacion', "+
                               " dt.supervisor  "+
                               " from tb_detalle_rutatecnicom dt, tb_responsable res  "+
                               " where  "+
                               " dt.codtec = res.codigo and "+                               
                               " dt.mes = "+mes+" and "+
                               " dt.anio = "+anio+" and "+
                               " res.nombre like '%"+nombre+"%' "+
                               " union "+
                               " select res.codigo, res.nombre, "+
                               " '' as 'FechaAsignacion', "+
                               " '' as 'HoraAsignacion',  "+
                               " '' as 'supervisor'  "+
                               " from  tb_responsable res "+
                               " where  "+
                               " res.nombre like '%"+nombre+"%' "+
                               " ) as T1 "+
                               " group by T1.codigo";
             return ConecRes.consultaMySql(consulta);
         }

         public bool eliminarRuta(int codRuta, int mes, int anio)
         {
             string consulta = "delete from tb_ruta "+
                               " where tb_ruta.codigo = " + codRuta+
                               " and tb_ruta.mes ="+mes+ " and tb_ruta.anio="+anio;
             return ConecRes.ejecutarMySql(consulta);
         }

         public DataSet tecnicoAsignadosRuta(int codRuta) {
             string consulta = "select * from tb_detalle_rutatecnicom dt where dt.codruta = " + codRuta;
             return ConecRes.consultaMySql(consulta);
         }

         public DataSet equiposAsignadoRuta(int codRuta) {
             string consulta = "select * from tb_cronogramavisitarutamanteminieto dt where dt.codruta =" + codRuta;
             return ConecRes.consultaMySql(consulta);
         }

         public bool eliminarEquipoRutaMantemiento(int codRuta,int codEquipo)
         {
             string consulta = "delete from tb_cronogramavisitarutamanteminieto " +
                               " where "+
                               " tb_cronogramavisitarutamanteminieto.codruta = " + codRuta + " and " +
                               " tb_cronogramavisitarutamanteminieto.codeq = " + codEquipo;
             return ConecRes.ejecutarMySql(consulta);
         }

         public bool eliminarTecnicoRutaMantemiento(int codRuta, int codtecnico)
         {
             string consulta = "delete from tb_detalle_rutatecnicom  " +
                                  " where " +
                                  " tb_detalle_rutatecnicom.codruta = " + codRuta + " and " +
                                  " tb_detalle_rutatecnicom.codtec = " + codtecnico;
             return ConecRes.ejecutarMySql(consulta);         
         }


         public bool insertarBoletaMantenimiento(int codEquipo, int codTecnico, string boleta, string detalle, bool cambiorepuesto, string fechaboleta, string horallegada, string horasalida, string recepcion, bool banderaArreglo, string tipoBoleta, bool siningresoedificio)
         {
             string consulta = "insert into tb_visitadetallerutaequipo( "+
                               " tb_visitadetallerutaequipo.fecha, "+
                               " tb_visitadetallerutaequipo.hora, "+
                             //  " tb_visitadetallerutaequipo.codruta, "+
                               " tb_visitadetallerutaequipo.codequipo, "+
                               " tb_visitadetallerutaequipo.codtecnico, "+
                               " tb_visitadetallerutaequipo.boleta, "+
                               " tb_visitadetallerutaequipo.observacion, "+
                               " tb_visitadetallerutaequipo.cambiorepuesto, " +
                               " tb_visitadetallerutaequipo.fechaboleta, " +
                               " tb_visitadetallerutaequipo.horallegada, " +
                               " tb_visitadetallerutaequipo.horasalida, " +
                               " tb_visitadetallerutaequipo.recepcion, " +
                               " tb_visitadetallerutaequipo.arreglo, " +
                               " tb_visitadetallerutaequipo.tipoboleta, " +
                               " tb_visitadetallerutaequipo.siningresoedificio, "+
                               " tb_visitadetallerutaequipo.mes, "+
                               " tb_visitadetallerutaequipo.anio "+
                               " ) values( "+
                               " now(), "+
                               " now(), "+
                          //     codRuta+" , "+
                               codEquipo+" , "+
                               codTecnico+" , "+
                               "'"+boleta+"' , "+
                               "'"+detalle+"',"+
                               cambiorepuesto +" , "+
                               fechaboleta+" , "+
                               horallegada+" , "+
                               horasalida+" , "+
                               "'"+ recepcion+"',"+
                               banderaArreglo+" , "+
                               "'"+tipoBoleta+"',"+
                               siningresoedificio+","+
                               "month("+fechaboleta+"),"+
                               "year("+fechaboleta+")"+
                               ")";
             return ConecRes.ejecutarMySql(consulta);
         }

         public bool eliminarBoletaMantenimiento(int codigoBoleta) {
             string consulta = "delete from tb_visitadetallerutaequipo "+
                               " where tb_visitadetallerutaequipo.codigo = "+codigoBoleta;

             return ConecRes.ejecutarMySql(consulta);
         }


         public DataSet mostrarBoletasMantenimiento(int codequipo, int codtecnico) { 
            string consulta = "select "+
                               " ve.codigo, "+
                               " date_format(ve.fecha,'%d/%m/%Y') as 'Fecha', " +
                               " ve.hora as 'Hora', "+                               
                               " ve.boleta, "+
                               " date_format(ve.fechaboleta,'%d/%m/%Y') as 'Fecha boleta',"+
                               " ve.horallegada,"+
                               " ve.horasalida,"+
                               " ve.recepcion," +
                               " ve.cambiorepuesto," +
                               " ve.arreglo," +
                               " ve.siningresoedificio," +
                               " ve.observacion "+
                               " from tb_visitadetallerutaequipo ve "+
                               " where "+
                              // " ve.codruta = "+codruta+" and "+
                               " ve.codequipo = "+codequipo+" and "+
                               " ve.codtecnico = "+codtecnico;
            return ConecRes.consultaMySql(consulta);
         }

         public DataSet mostrarALLPersonalAsignadoRuta(int mes, int anio)
         {
             string consulta = "select " +
                               " res.nombre as 'Nombre', " +
                               " date_format(dr.fecha, '%d/%m/%Y') as 'Fecha_Asignacion', " +
                               " dr.hora as 'Hora_Asignacion', " +
                               " ru.nro as 'NroRuta', " +
                               " ru.detalle as 'Detalle', " +
                               " dr.supervisor as 'Cargo' " +
                               " from " +
                               " tb_detalle_rutatecnicom dr, tb_ruta ru, tb_responsable res " +
                               " where " +
                               " dr.codruta = ru.codigo and " +
                               " dr.codtec = res.codigo and " +
                               " dr.mes = "+mes+" and "+
                               " dr.anio = "+anio+"  "+
                               " order by ru.codigo asc";
             return ConecRes.consultaMySql(consulta);
         }


         public DataSet mostrarALLEquiposAsignadosRutas(int mes , int anio)
         {
             string consulta = "select "+
                               " ru.nro as 'NroRuta', " +
                               " re.diasemana, "+
                               " re.horaentrada as 'Hora_Entrada', "+
                               " re.horasalida as 'Hora_Salida', "+
                               " timediff(re.horasalida , re.horaentrada) as 'Total_Hora', "+
                               " proy.nombre as 'Edificio', "+
                               " eq.exbo, "+
                               " re.nrovisita , "+ 
                               " re.nrodia "+
                               " from  "+
                               " tb_cronogramavisitarutamanteminieto re, "+
                               " tb_ruta ru, "+
                               " tb_equipo eq, "+
                               " tb_proyecto proy "+ 
                               " where  "+
                               " re.codruta = ru.codigo and "+
                               " re.codeq = eq.codigo and "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " re.codmes = "+mes+" and "+
                               " re.anio = "+anio+
                               " order by ru.codigo,re.nrodia asc";
             return ConecRes.consultaMySql(consulta);
         }


         public DataSet mostrarALLEquiposAsignadosRutasFechas(int mes, int anio)
         {
             string consulta = "select  " +
                               " ru.nro as 'NroRuta', " +
                               " cc.diasemana, " +
                               " cc.horaentrada as 'Hora_Entrada', " +
                               " cc.horasalida as 'Hora_Salida', " +
                               " timediff(cc.horasalida,cc.horaentrada) as 'Total_Hora', " +
                               " proy.nombre as 'Edificio', " +
                               " eq.exbo, " +
                               " cc.nrovisita as 'cantvisita',  " +
                               " cc.pasaje ," +
                               " cc.nrodia, " +
                               " date_format(cc.fechas1, '%d/%m/%Y')  as 'Semana1', " +
                               " date_format(cc.fechas2, '%d/%m/%Y')  as 'Semana2', " +
                               " date_format(cc.fechas3, '%d/%m/%Y')  as 'Semana3', " +
                               " date_format(cc.fechas4, '%d/%m/%Y')  as 'Semana4' " +
                               " from  " +
                               " tb_ruta ru, " +
                               " tb_proyecto proy,  " +
                               " tb_equipo eq,   " +
                               " tb_cronogramavisitarutamanteminieto cc  " +
                               " where  " +
                               " eq.codigo = cc.codeq and " +
                               " ru.codigo = cc.codruta and " +
                               " eq.cod_proyecto = proy.codigo and " +
                               " cc.codmes = " + mes + " and cc.anio = " + anio +
                               " order by ru.codigo,cc.nrodia,cc.horaentrada asc";
             return ConecRes.consultaMySql(consulta);
         }


         public DataSet mostrarALLEquiposAsignadosRutasFechas_Reporte(int mes, int anio)
         {
             string consulta = "select  "+
                               " ru.codigo, "+
                               " ru.nro as 'NroRuta', "+
                               " cc.diasemana, "+
                               " cc.horaentrada as 'Hora_Entrada', "+
                               " cc.horasalida as 'Hora_Salida', "+
                               " timediff(cc.horasalida,cc.horaentrada) as 'Total_Hora', "+
                               " proy.nombre as 'Edificio', "+
                               " eq.exbo, "+
                               " cc.nrovisita as 'cantvisita',  " +
                               " cc.pasaje ,"+
                               " cc.nrodia, "+
                               " date_format(cc.fechas1, '%d/%m/%Y')  as 'Semana1', "+
                               " date_format(cc.fechas2, '%d/%m/%Y')  as 'Semana2', "+
                               " date_format(cc.fechas3, '%d/%m/%Y')  as 'Semana3', "+
                               " date_format(cc.fechas4, '%d/%m/%Y')  as 'Semana4' "+
                               " from  "+
                               " tb_ruta ru, "+
                               " tb_proyecto proy,  "+
                               " tb_equipo eq,   "+
                               " tb_cronogramavisitarutamanteminieto cc  "+
                               " where  "+
                               " eq.codigo = cc.codeq and "+
                               " ru.codigo = cc.codruta and "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " cc.codmes = "+mes+" and cc.anio = "+anio+
                               " order by ru.codigo,cc.nrodia,cc.horaentrada asc";
             return ConecRes.consultaMySql(consulta);
         }

         public DataSet mostrarALLEquiposAsignadosRutasFechas_consulta(int mes, int anio,string codRuta, string exbo, string edificio)
         {            
             string consulta = "select  " +
                               " ru.codigo as 'NroRuta', " +
                               " cc.diasemana, " +
                               " cc.horaentrada as 'Hora_Entrada', " +
                               " cc.horasalida as 'Hora_Salida', " +
                               " timediff(cc.horasalida,cc.horaentrada) as 'Total_Hora', " +
                               " proy.nombre as 'Edificio', " +
                               " eq.exbo, " +
                               " cc.nrovisita as 'cantvisita',  " +
                               " cc.nrodia, " +
                               " date_format(cc.fechas1, '%d/%m/%Y')  as 'Semana1', " +
                               " date_format(cc.fechas2, '%d/%m/%Y')  as 'Semana2', " +
                               " date_format(cc.fechas3, '%d/%m/%Y')  as 'Semana3', " +
                               " date_format(cc.fechas4, '%d/%m/%Y')  as 'Semana4' " +
                               " from  " +
                               " tb_ruta ru, " +
                               " tb_proyecto proy,  " +
                               " tb_equipo eq,   " +
                               " tb_cronogramavisitarutamanteminieto cc  " +
                               " where  " +
                               " eq.codigo = cc.codeq and " +
                               " ru.codigo = cc.codruta and " +
                               " eq.cod_proyecto = proy.codigo and " +
                               " proy.nombre like '%"+edificio+"%' and "+
                               " eq.exbo like '%"+exbo+"%' and "+
                               " ru.codigo like '%"+codRuta+"%' and "+ 
                               " cc.codmes = " + mes + " and cc.anio = " + anio +
                               " order by ru.codigo,cc.nrodia,cc.horaentrada asc";
             return ConecRes.consultaMySql(consulta);
         }

         public DataSet getcronogramaMesAnioRuta(int mes, int anio) {
             string consulta = "select * from tb_cronogramavisitarutamanteminieto crovi "+
                               " where crovi.codmes = "+mes+" and "+
                               " crovi.anio = "+anio;
             return ConecRes.consultaMySql(consulta);     
         }

         public DataSet getcronogramaMesAnioRuta_porNroVisita(int mes, int anio,int nrovisita)
         {
             string consulta = "select * from tb_cronogramavisitarutamanteminieto crovi " +
                               " where crovi.codmes = " + mes + " and " +
                               " crovi.anio = " + anio + " and crovi.nrovisita = "+nrovisita;
             return ConecRes.consultaMySql(consulta);
         }

       /*  public bool modificarCronogramaEquipoRutaMantemiento(int codRuta, int codEquipo, int codCronograma)
         {
             string consulta = "update tb_cronogramavisitarutamanteminieto set tb_cronogramavisitarutamanteminieto.codcrono = "+codCronograma+
                               " where  "+
                               " tb_cronogramavisitarutamanteminieto.codruta = "+codRuta+" and "+
                               " tb_cronogramavisitarutamanteminieto.codequipo = "+codEquipo;
             return ConecRes.ejecutarMySql(consulta);
         }*/


     /*    public int getCodigoCronoRutaMantenimiento(int codRuta, int codEquipo)
         {
             string consulta = "select "+
                               " rr.codcrono "+
                               " from tb_cronogramavisitarutamanteminieto rr "+
                               " where "+
                               " rr.codruta = "+codRuta+" and "+
                               " rr.codequipo = "+codEquipo;
             DataSet dato = ConecRes.consultaMySql(consulta);
             int numero = 0;
             string numeroSTR = dato.Tables[0].Rows[0][0].ToString();
             bool siEsNumero = int.TryParse(numeroSTR, out numero);
             if (siEsNumero == true)
             {
                 return numero;
             }
             else
                 return -1;
         }
        */

         public DataSet getdetalleRutaEquipoCrono(int codRuta, int codEquipo)
         {
             string consulta = "select "+
                               " dd.codruta, "+
                               " dd.codequipo, "+
                               " dd.horaentrada, "+
                               " dd.horasalida, "+
                               " dd.nrodia, "+
                               " dd.cantvisita, "+
                               " dd.semana1, "+
                               " dd.semana2, "+
                               " dd.semana3, "+
                               " dd.semana4, "+
                               " cc.fechas1, "+
                               " cc.fechas2, "+
                               " cc.fechas3, "+
                               " cc.fechas4  "+
                               " from tb_cronogramavisitarutamanteminieto dd "+
                               " left join tb_cronogramavisitarutamanteminieto cc on dd.codcrono = cc.codigo "+
                               " where "+
                               " dd.codruta = " + codRuta + " and " +
                               " dd.codequipo = " + codEquipo;
             return ConecRes.consultaMySql(consulta);
         }

 public DataSet getdetalleRutaEquipoTodos(int mes, int anio)
         {
             string consulta = "select "+
                               " dd.codruta,  "+
                               " dd.codeq, "+
                               " dd.horaentrada, "+
                               " dd.horasalida, "+
                               " dd.diasemana, "+
                               " dd.nrodia, "+
                               " dd.nrovisita, "+ 
                               " dd.semana1, "+
                               " dd.semana2, "+
                               " dd.semana3, "+
                               " dd.semana4, "+
                               " dd.codigo, "+
                               " dd.pasaje "+
                               " from tb_cronogramavisitarutamanteminieto dd "+
                               " where "+
                               " dd.codmes = "+mes+" and "+
                               " dd.anio = "+anio;
                               
             return ConecRes.consultaMySql(consulta);
         }  

         public int getcantidadColumnasFechasTotalRutas(int mes, int anio) {
             string consulta = "select "+
                               " max(t1.cantvisita) "+
                               " from "+
                               " ( "+
                               " select "+
                               " count(*) as 'cantvisita' "+
                               " from "+
                               " tb_visitadetallerutaequipo vv "+
                               " where  "+
                               " month(vv.fechaboleta) = "+mes+" and "+
                               " year(vv.fechaboleta) = "+anio+
                               " group by "+
                               " (vv.codequipo) "+
                               " ) as t1";
             DataSet dato = ConecRes.consultaMySql(consulta);
             if (dato.Tables[0].Rows.Count > 0)
             {
                 return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
             }
             else
                 return 0;
         }

         public DataSet getFechasBoletasRutas(int codRuta, int codEquipo, int mes, int anio, string tipoBoleta)
         {

             string consulta = "select " +
                               " Concat(date_format(vv.fechaboleta, '%d/%m/%Y'),' ',res.nombre,' ',if(vv.siningresoedificio=1,' No_Ingreso','')) as 'fecha_boleta' " +
                               " from " +
                               " tb_visitadetallerutaequipo vv " +
                               " , tb_responsable res "+
                               " where  " +
                               " vv.codtecnico = res.codigo and "+
                            //   " vv.codruta = " + codRuta + " and " +
                               " vv.codequipo = " + codEquipo + " and " +
                               " vv.tipoboleta = '" + tipoBoleta + "' and " +
                               " month(vv.fechaboleta) = " + mes + " and " +
                               " year(vv.fechaboleta) = " + anio +
                               " order by (vv.fechaboleta) asc ";
             DataSet dato = ConecRes.consultaMySql(consulta);
             return dato;
         }


         public DataSet  getFechasBastones(int codEquipo, int mes, int anio, string tipoBoleta)
         {

             string consulta = "select " +
                               " CAST(CONCAT(date_format(bb.fechabaston, '%d/%m/%Y'),' ',bb.horabaston,' ',res.nombre) AS CHAR) as fecha_boleta " +
                               " from " +
                               " tb_bastones bb, tb_responsable res, tb_equipo eq " +
                               " where " +
                               " bb.codequipo = eq.codigo and " +
                               " bb.codtecnico = res.codigo and " +
                               " bb.horabaston <> 0  and " +
                               " bb.codequipo = " + codEquipo + " and " +
                               " month(bb.fechabaston) = " + mes + " and " +
                               " year(bb.fechabaston) = " + anio +
                               " order by TIMESTAMP(bb.fechabaston,bb.horabaston) asc ";
                               
                               
             DataSet dato = ConecRes.consultaMySql(consulta);
             return dato;
         }


         public DataSet mostrarConsultaEquiposAsignadosRutasFechas(string codRuta, string exbo, int mes, int anio, string nombreProyecto, string personalAsignado)
         {
             string consulta = "select  "+
                                   " ru.codigo as 'NroRuta', "+
                                   " re.diasemana, "+
                                   " re.horaentrada as 'Hora_Entrada', "+
                                   " re.horasalida as 'Hora_Salida', "+
                                   " timediff(re.horasalida,re.horaentrada) as 'Total_Hora', "+
                                   " proy.nombre as 'Edificio', "+
                                   " eq.exbo, "+
                                   " re.cantvisita,  "+
                                   " date_format(cc.fechas1, '%d/%m/%Y')  as 'Semana1', "+
                                   " date_format(cc.fechas2, '%d/%m/%Y')  as 'Semana2', "+
                                   " date_format(cc.fechas3, '%d/%m/%Y')  as 'Semana3', "+
                                   " date_format(cc.fechas4, '%d/%m/%Y')  as 'Semana4', "+
                                   " T1.Supervisor as 'Personal Asignado', "+
                                   " T1.tipo "+
                                   " from  "+
                                   " tb_proyecto proy, "+
                                   " tb_cronogramavisitarutamanteminieto re, "+
                                   " tb_equipo eq   "+
                                   " left join tb_cronogramavisitarutamanteminieto cc on (cc.codeq = eq.codigo), "+
                                   " tb_ruta ru "+
                                   " LEFT JOIN "+
                                   " ( "+
                                   " select rt.codruta, res.nombre as 'Supervisor', rt.supervisor as 'tipo' "+
                                   " from tb_detalle_rutatecnicom rt, tb_responsable res "+
                                   " where "+
                                   " rt.codtec = res.codigo "+                                   
                                   " )AS T1 ON (T1.codruta = ru.codigo) "+                                   
                                   " where  "+
                                   " re.codruta = ru.codigo and "+
                                   " re.codequipo = eq.codigo and "+
                                   " eq.cod_proyecto = proy.codigo and "+
                                   " eq.exbo like '%"+exbo+"%' and "+
                                   " ru.codigo like '%"+codRuta+"%' and "+
                                   " proy.nombre like '%"+nombreProyecto+"%' and "+
                                   " T1.Supervisor like '%"+personalAsignado+"%' and "+                                   
                                   " cc.codmes = "+mes+" and cc.anio = "+anio+                               
                                   " order by ru.codigo,re.nrodia asc ";
             return ConecRes.consultaMySql(consulta);
         }


          public DataSet getAllPersonalAsignadoRuta() {
              string consulta = "select " +
                               " res.codigo, " +
                               " res.nombre  " +
                               " from  " +
                               " tb_detalle_rutatecnicom dr, tb_responsable res " +
                               " where " +
                               " dr.codtec = res.codigo  " +
                               " group by res.codigo " +
                               " order by res.codigo asc";
              return ConecRes.consultaMySql(consulta);
          }


          public bool existeEquipoRuta(int codRuta, int codEquipo)
          {
              string consulta = "select * from tb_cronogramavisitarutamanteminieto ee "+
                                   " where "+
                                   " ee.codruta = "+codRuta+" and "+
                                   " ee.codeq = "+codEquipo;
              DataSet dato = ConecRes.consultaMySql(consulta);
              if (dato.Tables[0].Rows.Count > 0)
              {
                  return true;
              }
              else
                  return false;
          }

          public bool insertarExcelBastones(string direccionRuta, string nombreArchivo)
          {
              string consulta = "truncate tb_cargarbastones_aux;";
              bool bandera1 = ConecRes.ejecutarMySql(consulta);
              consulta = "LOAD DATA LOCAL INFILE '"+direccionRuta+"/"+nombreArchivo+"' "+
                           " INTO TABLE tb_cargarbastones_aux "+
                           " FIELDS TERMINATED BY ';'  "+                           
                           " LINES TERMINATED BY '\r\n' "+
                           " IGNORE 1 LINES "+
                           " (codtecnico, codequipo, hora, fecha, tipoevento);";
              bool bandera2 = ConecRes.ejecutarMySql(consulta);

              consulta = "delete from tb_cargarbastones_aux where tb_cargarbastones_aux.codtecnico = 0;";
              bool bandera3 = ConecRes.ejecutarMySql(consulta);
              consulta = "insert into tb_bastones( "+
                           " tb_bastones.fecha, "+
                           " tb_bastones.hora, "+
                           " tb_bastones.codtecnico, "+
                           " tb_bastones.codequipo, "+
                           " tb_bastones.horabaston, "+
                           " tb_bastones.fechabaston, "+
                           " tb_bastones.tipoevento "+
                           " )  "+
                           " select "+
                           " now(), "+
                           " now(), "+
                           " tb_cargarbastones_aux.codtecnico, "+
                           " tb_cargarbastones_aux.codequipo, "+
                           " tb_cargarbastones_aux.hora, "+
                           " tb_cargarbastones_aux.fecha, "+
                           " tb_cargarbastones_aux.tipoevento "+
                           " from tb_cargarbastones_aux;";
              bool bandera4 = ConecRes.ejecutarMySql(consulta);

              if (bandera1 == true && bandera2 == true && bandera3 == true && bandera4 == true)
              {
                  return true;
              }
              else
                  return false;              
          }


       

          public bool insert_generarRutasMantenimiento(int mes, int anio, int coduser, int mesCopiar, int anioCopiar)
          {
              string delete1 = "delete from tb_cronogramavisitarutamanteminieto where tb_cronogramavisitarutamanteminieto.codmes = "+mes+" and tb_cronogramavisitarutamanteminieto.anio = "+anio;
              string delete2 = "delete from tb_detalle_rutatecnicom where tb_detalle_rutatecnicom.mes = "+mes+" and tb_detalle_rutatecnicom.anio = "+anio;
              string delete3 = "delete from tb_ruta where tb_ruta.mes = " + mes + " and tb_ruta.anio = " + anio;

              bool banderadelete1 = ConecRes.ejecutarMySql(delete1);
              bool banderadelete2 = ConecRes.ejecutarMySql(delete2);
              bool banderadelete3 = ConecRes.ejecutarMySql(delete3);

              string consulta1 = "insert into tb_ruta( "+
                                   " tb_ruta.nro, "+
                                   " tb_ruta.detalle, "+
                                   " tb_ruta.fecha, "+
                                   " tb_ruta.hora, "+
                                   " tb_ruta.estado, "+
                                   " tb_ruta.mes, "+
                                   " tb_ruta.anio) "+
                                   " select  "+
                                   " ru.nro, "+
                                   " ru.detalle, "+
                                   " now(), "+
                                   " now(), "+
                                   " ru.estado, "+
                                     mes+" , "+
                                     anio+
                                   " from tb_ruta ru "+
                                   " where  "+
                                   " ru.mes = "+mesCopiar+" and ru.anio = "+anioCopiar;
              
              bool bandera1 = ConecRes.ejecutarMySql(consulta1);


              string consulta2 = "insert into tb_cronogramavisitarutamanteminieto( "+
                                   " tb_cronogramavisitarutamanteminieto.codeq, "+
                                   " tb_cronogramavisitarutamanteminieto.codruta, "+
                                   " tb_cronogramavisitarutamanteminieto.codmes, "+
                                   " tb_cronogramavisitarutamanteminieto.anio, "+
                                   " tb_cronogramavisitarutamanteminieto.fecha, "+
                                   " tb_cronogramavisitarutamanteminieto.hora, "+
                                   " tb_cronogramavisitarutamanteminieto.nrovisita, "+
                                   " tb_cronogramavisitarutamanteminieto.semana1, "+
                                   " tb_cronogramavisitarutamanteminieto.semana2, "+
                                   " tb_cronogramavisitarutamanteminieto.semana3, "+
                                   " tb_cronogramavisitarutamanteminieto.semana4, "+
                                   " tb_cronogramavisitarutamanteminieto.fechas1, "+
                                   " tb_cronogramavisitarutamanteminieto.fechas2, "+
                                   " tb_cronogramavisitarutamanteminieto.fechas3, "+
                                   " tb_cronogramavisitarutamanteminieto.fechas4, "+
                                   " tb_cronogramavisitarutamanteminieto.codresp, "+
                                   " tb_cronogramavisitarutamanteminieto.horaentrada , "+
                                   " tb_cronogramavisitarutamanteminieto.diasemana, "+
                                   " tb_cronogramavisitarutamanteminieto.horasalida, "+
                                   " tb_cronogramavisitarutamanteminieto.nrodia, "+
                                   " tb_cronogramavisitarutamanteminieto.pasaje  "+
                                   " ) "+
                                   " select  "+
                                   " cc.codeq, "+
                                   " ru2.codigo, "+
                                     mes+" , "+
                                     anio+" , "+
                                   " now(), "+
                                   " now(), "+
                                   " cc.nrovisita, "+
                                   " cc.semana1, "+
                                   " cc.semana2, "+
                                   " cc.semana3, "+
                                   " cc.semana4, "+
                                   " cc.fechas1, "+
                                   " cc.fechas2, "+
                                   " cc.fechas3, "+
                                   " cc.fechas4, "+
                                   coduser+" , "+
                                   " cc.horaentrada, "+
                                   " cc.diasemana, "+
                                   " cc.horasalida, "+
                                   " cc.nrodia, "+                                   
                                   " cc.pasaje "+ 
                                   " from "+
                                   " tb_cronogramavisitarutamanteminieto cc, "+
                                   " tb_ruta ru1, tb_ruta ru2 "+
                                   " where "+
                                   " ru1.mes = "+mesCopiar+" and ru1.anio = "+anioCopiar+" and "+
                                   " ru1.nro = ru2.nro and "+
                                   " ru2.mes = "+mes+" and ru2.anio = "+anio+" and "+
                                   " cc.codruta = ru1.codigo and  "+
                                   " cc.codmes = "+mesCopiar+" and cc.anio = "+anioCopiar;  
                                   
              bool bandera2 = ConecRes.ejecutarMySql(consulta2);

              string consulta3 = "insert into tb_detalle_rutatecnicom( "+
                                   " tb_detalle_rutatecnicom.codruta, "+
                                   " tb_detalle_rutatecnicom.codtec, "+
                                   " tb_detalle_rutatecnicom.fecha, "+
                                   " tb_detalle_rutatecnicom.hora, "+
                                   " tb_detalle_rutatecnicom.estado, "+
                                   " tb_detalle_rutatecnicom.supervisor, "+
                                   " tb_detalle_rutatecnicom.mes, "+
                                   " tb_detalle_rutatecnicom.anio "+ 
                                   " ) "+
                                   " select  "+
                                   " r2.codigo, "+
                                   " dd.codtec, "+
                                   " now(), "+
                                   " now(), " +
                                   " dd.estado, "+
                                   " dd.supervisor, "+
                                   " month(now()), "+
                                   " year(now()) "+
                                   " from "+
                                   " tb_detalle_rutatecnicom dd, "+
                                   " tb_ruta r1, "+
                                   " tb_ruta r2 "+
                                   " where "+
                                   " r1.mes = "+mesCopiar+" and "+
                                   " r1.anio = "+anioCopiar+" and "+
                                   " r1.nro = r2.nro and "+
                                   " r2.mes = "+mes+" and "+
                                   " r2.anio = "+anio+" and "+
                                   " dd.codruta = r1.codigo and "+
                                   " dd.mes = "+mesCopiar+" and dd.anio = "+anio;

              bool bandera3 = ConecRes.ejecutarMySql(consulta3);

              if (banderadelete1&&banderadelete2&&banderadelete3&&bandera1 && bandera2 && bandera3)
              {
                  return true;
              }
              else
                  return false;
          }


          public bool update_generarRutasMantenimiento(int mes, int anio, int coduser, int mesCopiar, int anioCopiar)
          {
              string delete1 = "delete from tb_cronogramavisitarutamanteminieto where tb_cronogramavisitarutamanteminieto.codmes = " + mes + " and tb_cronogramavisitarutamanteminieto.anio = " + anio;
              string delete2 = "delete from tb_detalle_rutatecnicom where tb_detalle_rutatecnicom.mes = " + mes + " and tb_detalle_rutatecnicom.anio = " + anio;              

              bool banderadelete1 = ConecRes.ejecutarMySql(delete1);
              bool banderadelete2 = ConecRes.ejecutarMySql(delete2);              

              string consulta2 = "insert into tb_cronogramavisitarutamanteminieto( " +
                                   " tb_cronogramavisitarutamanteminieto.codeq, " +
                                   " tb_cronogramavisitarutamanteminieto.codruta, " +
                                   " tb_cronogramavisitarutamanteminieto.codmes, " +
                                   " tb_cronogramavisitarutamanteminieto.anio, " +
                                   " tb_cronogramavisitarutamanteminieto.fecha, " +
                                   " tb_cronogramavisitarutamanteminieto.hora, " +
                                   " tb_cronogramavisitarutamanteminieto.nrovisita, " +
                                   " tb_cronogramavisitarutamanteminieto.semana1, " +
                                   " tb_cronogramavisitarutamanteminieto.semana2, " +
                                   " tb_cronogramavisitarutamanteminieto.semana3, " +
                                   " tb_cronogramavisitarutamanteminieto.semana4, " +
                                   " tb_cronogramavisitarutamanteminieto.fechas1, " +
                                   " tb_cronogramavisitarutamanteminieto.fechas2, " +
                                   " tb_cronogramavisitarutamanteminieto.fechas3, " +
                                   " tb_cronogramavisitarutamanteminieto.fechas4, " +
                                   " tb_cronogramavisitarutamanteminieto.codresp, " +
                                   " tb_cronogramavisitarutamanteminieto.horaentrada , " +
                                   " tb_cronogramavisitarutamanteminieto.diasemana, " +
                                   " tb_cronogramavisitarutamanteminieto.horasalida, " +
                                   " tb_cronogramavisitarutamanteminieto.nrodia, " +
                                   " tb_cronogramavisitarutamanteminieto.pasaje  " +
                                   " ) " +
                                   " select  " +
                                   " cc.codeq, " +
                                   " ru2.codigo, " +
                                     mes + " , " +
                                     anio + " , " +
                                   " now(), " +
                                   " now(), " +
                                   " cc.nrovisita, " +
                                   " cc.semana1, " +
                                   " cc.semana2, " +
                                   " cc.semana3, " +
                                   " cc.semana4, " +
                                   " cc.fechas1, " +
                                   " cc.fechas2, " +
                                   " cc.fechas3, " +
                                   " cc.fechas4, " +
                                   coduser + " , " +
                                   " cc.horaentrada, " +
                                   " cc.diasemana, " +
                                   " cc.horasalida, " +
                                   " cc.nrodia, " +
                                   " cc.pasaje " +
                                   " from " +
                                   " tb_cronogramavisitarutamanteminieto cc, " +
                                   " tb_ruta ru1, tb_ruta ru2 " +
                                   " where " +
                                   " ru1.mes = " + mesCopiar + " and ru1.anio = " + anioCopiar + " and " +
                                   " ru1.nro = ru2.nro and " +
                                   " ru2.mes = " + mes + " and ru2.anio = " + anio + " and " +
                                   " cc.codruta = ru1.codigo and  " +
                                   " cc.codmes = " + mesCopiar + " and cc.anio = " + anioCopiar;

              bool bandera2 = ConecRes.ejecutarMySql(consulta2);

              string consulta3 = "insert into tb_detalle_rutatecnicom( " +
                                   " tb_detalle_rutatecnicom.codruta, " +
                                   " tb_detalle_rutatecnicom.codtec, " +
                                   " tb_detalle_rutatecnicom.fecha, " +
                                   " tb_detalle_rutatecnicom.hora, " +
                                   " tb_detalle_rutatecnicom.estado, " +
                                   " tb_detalle_rutatecnicom.supervisor, " +
                                   " tb_detalle_rutatecnicom.mes, " +
                                   " tb_detalle_rutatecnicom.anio " +
                                   " ) " +
                                   " select  " +
                                   " r2.codigo, " +
                                   " dd.codtec, " +
                                   " now(), " +
                                   " now(), " +
                                   " dd.estado, " +
                                   " dd.supervisor, " +
                                   " month(now()), " +
                                   " year(now()) " +
                                   " from " +
                                   " tb_detalle_rutatecnicom dd, " +
                                   " tb_ruta r1, " +
                                   " tb_ruta r2 " +
                                   " where " +
                                   " r1.mes = " + mesCopiar + " and " +
                                   " r1.anio = " + anioCopiar + " and " +
                                   " r1.nro = r2.nro and " +
                                   " r2.mes = " + mes + " and " +
                                   " r2.anio = " + anio + " and " +
                                   " dd.codruta = r1.codigo and " +
                                   " dd.mes = " + mesCopiar + " and dd.anio = " + anio;

              bool bandera3 = ConecRes.ejecutarMySql(consulta3);

              if (banderadelete1 && banderadelete2 && bandera2 && bandera3)
              {
                  return true;
              }
              else
                  return false;


          }

          internal bool ponerEnCerolasFechasNOcumplidas()
          {
              string consulta1 = "update tb_cronogramavisitarutamanteminieto set tb_cronogramavisitarutamanteminieto.fecha1cumplida = 0 where tb_cronogramavisitarutamanteminieto.fecha1cumplida is null";
              string consulta2 = "update tb_cronogramavisitarutamanteminieto set tb_cronogramavisitarutamanteminieto.fecha2cumplida = 0 where tb_cronogramavisitarutamanteminieto.fecha2cumplida is null";
              string consulta3 = "update tb_cronogramavisitarutamanteminieto set tb_cronogramavisitarutamanteminieto.fecha3cumplida = 0 where tb_cronogramavisitarutamanteminieto.fecha3cumplida is null";
              string consulta4 = "update tb_cronogramavisitarutamanteminieto set tb_cronogramavisitarutamanteminieto.fecha4cumplida = 0 where tb_cronogramavisitarutamanteminieto.fecha4cumplida is null";
              bool bandera1 = ConecRes.ejecutarMySql(consulta1);
              bool bandera2 = ConecRes.ejecutarMySql(consulta2);
              bool bandera3 = ConecRes.ejecutarMySql(consulta3);
              bool bandera4 = ConecRes.ejecutarMySql(consulta4);
              if (bandera1 && bandera2 && bandera3 && bandera4)
                  return true;
              else
                  return false;
          }

          internal bool eliminartodosequiposyrutademantenimiento(int codigoRuta)
          {
              string consulta = "delete from tb_cronogramavisitarutamanteminieto "+
                                " where tb_cronogramavisitarutamanteminieto.codruta = "+codigoRuta;
              bool bandera1 = ConecRes.ejecutarMySql(consulta);

              consulta = "delete from tb_detalle_rutatecnicom where tb_detalle_rutatecnicom.codruta = " + codigoRuta;
              bool bandera2 = ConecRes.ejecutarMySql(consulta);

              consulta = "delete from tb_ruta where tb_ruta.codigo = "+codigoRuta;
              bool bandera3 = ConecRes.ejecutarMySql(consulta);
              if (bandera1 == bandera2 == bandera3 == true)
              {
                  return true;
              }
              else
                  return false;

          }
    }
}