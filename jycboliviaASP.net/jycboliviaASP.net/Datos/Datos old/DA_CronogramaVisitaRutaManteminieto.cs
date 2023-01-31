using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_CronogramaVisitaRutaManteminieto
    {
        private conexionMySql ConecRes = new conexionMySql();
        
        public DA_CronogramaVisitaRutaManteminieto() { }
    
        public bool insertarCronogramaVisitaRutaM(int codRuta,int codEquipo,int nrovisita,int codmes,int anio,bool semana1,bool semana2,bool semana3,bool semana4, string fechaS1, string fechaS2, string fechaS3, string fechaS4, int codResp,string horaEntrada, string horasalida, string dia, int nrodia, float pasaje){
         string consulta = "insert into tb_cronogramavisitarutamanteminieto "+
                           " ( "+
                           " tb_cronogramavisitarutamanteminieto.fecha, "+
                           " tb_cronogramavisitarutamanteminieto.hora, "+
                           " tb_cronogramavisitarutamanteminieto.codeq, "+
                           " tb_cronogramavisitarutamanteminieto.codruta, "+
                           " tb_cronogramavisitarutamanteminieto.nrovisita, "+
                           " tb_cronogramavisitarutamanteminieto.codmes, "+ 
                           " tb_cronogramavisitarutamanteminieto.anio, "+
                           " tb_cronogramavisitarutamanteminieto.semana1, "+ 
                           " tb_cronogramavisitarutamanteminieto.semana2, "+
                           " tb_cronogramavisitarutamanteminieto.semana3, "+
                           " tb_cronogramavisitarutamanteminieto.semana4, "+
                           " tb_cronogramavisitarutamanteminieto.fechas1, "+
                           " tb_cronogramavisitarutamanteminieto.fechas2, "+
                           " tb_cronogramavisitarutamanteminieto.fechas3, "+
                           " tb_cronogramavisitarutamanteminieto.fechas4, "+
                           " tb_cronogramavisitarutamanteminieto.codresp, "+
                           " tb_cronogramavisitarutamanteminieto.horaentrada, " +
                           " tb_cronogramavisitarutamanteminieto.horasalida, " +
                           " tb_cronogramavisitarutamanteminieto.diasemana, " +
                           " tb_cronogramavisitarutamanteminieto.nrodia, " +
                           " tb_cronogramavisitarutamanteminieto.pasaje " +
                           " ) "+
                           " values "+
                           " ( "+
                           " now(), "+
                           " now(), "+
                           codEquipo+", "+
                           codRuta+", "+
                           nrovisita+", "+
                           codmes+", "+
                           anio+", "+
                           semana1+", "+
                           semana2+", "+
                           semana3+", "+
                           semana4+", "+
                           fechaS1+", "+
                           fechaS2+", "+
                           fechaS3+", "+
                           fechaS4+", "+
                           codResp+", "+
                           "'"+horaEntrada+"', "+
                           "'"+horasalida+"', "+
                           "'"+dia+"', "+
                           nrodia+","+
                           "'"+pasaje.ToString().Replace(',','.')+"')";
         return ConecRes.ejecutarMySql(consulta);
        }

        public bool UpdateCronogramaVisitaRutaM(int codigoCronograma, int nrovisita, bool semana1, bool semana2, bool semana3, bool semana4, string fechaS1, string fechaS2, string fechaS3, string fechaS4, int codResp, string horaEntrada, string horasalida, string dia, int nrodia, float pasaje)
        {
            string consulta = "update tb_cronogramavisitarutamanteminieto " +
                              " set " +
                              " tb_cronogramavisitarutamanteminieto.fecha = now(), " +
                              " tb_cronogramavisitarutamanteminieto.hora = now(), " +
                              " tb_cronogramavisitarutamanteminieto.nrovisita = " + nrovisita + " ," +
                              " tb_cronogramavisitarutamanteminieto.semana1 = " + semana1 + ", " +
                              " tb_cronogramavisitarutamanteminieto.semana2 = " + semana2 + ", " +
                              " tb_cronogramavisitarutamanteminieto.semana3 = " + semana3 + ", " +
                              " tb_cronogramavisitarutamanteminieto.semana4 = " + semana4 + ", " +
                              " tb_cronogramavisitarutamanteminieto.fechas1 = " + fechaS1 + ", " +
                              " tb_cronogramavisitarutamanteminieto.fechas2 = " + fechaS2 + ", " +
                              " tb_cronogramavisitarutamanteminieto.fechas3 = " + fechaS3 + ", " +
                              " tb_cronogramavisitarutamanteminieto.fechas4 = " + fechaS4 + ", " +
                              " tb_cronogramavisitarutamanteminieto.codresp = " + codResp + ", "+
                              " tb_cronogramavisitarutamanteminieto.horaentrada = '" + horaEntrada + "', " +
                              " tb_cronogramavisitarutamanteminieto.horasalida = '" + horasalida + "', " +
                              " tb_cronogramavisitarutamanteminieto.diasemana = '" + dia + "', " +
                              " tb_cronogramavisitarutamanteminieto.nrodia = " + nrodia + ", " +
                              " tb_cronogramavisitarutamanteminieto.pasaje = '" + pasaje.ToString().Replace(',','.') + "' " +
                              " where " +
                              " tb_cronogramavisitarutamanteminieto.codigo = " + codigoCronograma;

            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getUltimoIngresado(int codRuta, int codEquipo, int mes, int anio)
        {
            string consulta = "select max(cc.codigo) from tb_cronogramavisitarutamanteminieto cc "+
                               " where  "+
                               " cc.codruta = "+codRuta+" and " +
                               " cc.codeq = "+codEquipo+" and "+
                               " cc.codmes = "+mes+" and "+
                               " cc.anio = "+anio;
            return ConecRes.consultaMySql(consulta);
        }

        public bool eliminar(int nrovisita, int codmes, int anio)
        {
            string consulta = " delete from tb_cronogramavisitarutamanteminieto  "+
                                " where tb_cronogramavisitarutamanteminieto.nrovisita = "+nrovisita+" and "+
                                " tb_cronogramavisitarutamanteminieto.codmes = "+codmes+" and "+
                                " tb_cronogramavisitarutamanteminieto.anio = "+anio;
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getAllDatos(string nrovisita, string codmes, string anio) {
            string consulta = "  select "+
                                " crono.nrovisita, "+
                                " mes.nombre as 'mes', "+
                                " crono.anio, "+
                                " crono.semana1, "+
                                " crono.semana2, "+
                                " crono.semana3, "+
                                " crono.semana4  "+
                                " from tb_cronogramavisitarutamanteminieto crono , tb_mes mes "+
                                " where  "+
                                " crono.codmes = mes.codigo and "+
                                " crono.nrovisita like '%"+nrovisita+"%' and "+
                                " crono.codmes like '%"+codmes+"%' and "+
                                " crono.anio like '%"+anio+"%'";
            return ConecRes.consultaMySql(consulta);
        }

        public DataSet getcronoVisitaRutaEquipo(int codRuta, int codEquipo, int codmes, int anio)
        {
            string consulta = "select "+
                               " cc.codigo "+
                               " from tb_cronogramavisitarutamanteminieto cc "+
                               " where "+
                               " cc.codruta = "+codRuta+" and "+
                               " cc.codeq = "+codEquipo+" and "+
                               " cc.codmes = "+codmes+" and "+
                               " cc.anio = "+anio;
            return ConecRes.consultaMySql(consulta);
        }



    }


}