using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_ActividadesPersonal
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DA_ActividadesPersonal() { }

        public bool insertActividades(string detalleActividad, int codUserInicio,  string fechaExpiracion)
        {
            string consulta = "insert into tb_ActividadPersonal "+
                               " (tb_ActividadPersonal.fecha,tb_ActividadPersonal.hora,tb_ActividadPersonal.fechaexpiracion, "+
                               " tb_ActividadPersonal.detalle,tb_ActividadPersonal.coduserinicio, "+
                               " tb_ActividadPersonal.estado) "+
                               " values "+
                               " (now(),now(),"+fechaExpiracion+", "+
                              " ' "+ detalleActividad+ "' , "+codUserInicio+" ,1);";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool deleteActividad(int codActividad)
        {
            string consulta = "delete from tb_ActividadPersonal where tb_ActividadPersonal.codigo = " + codActividad;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool updateActividad(int codActividad, string detalleActividad, string fechaExpiracion)
        {
            string consulta = "update tb_ActividadPersonal set tb_ActividadPersonal.detalle = '" + detalleActividad + "' , tb_ActividadPersonal.fechaexpiracion = "+fechaExpiracion+
                             "' where tb_ActividadPersonal.codigo = " + codActividad;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool updateActividadAsignado(int codActividad, int codUserAsignado)
        {
            string consulta = "update tb_ActividadPersonal set tb_ActividadPersonal.coduserultimoasignado = " + codUserAsignado + " where tb_ActividadPersonal.codigo = " + codActividad;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool insertarDetalleActividadPersonal(int codactividad, int codResponsable, string fechaEjecucion, string horaEjecucion)
        {
            if(horaEjecucion.Equals("")){
                horaEjecucion = "00:00:00";
            }
            string consulta = "insert into tb_detalle_ActiviadPersonal "+
                               " (tb_detalle_ActiviadPersonal.codres, tb_detalle_ActiviadPersonal.codact, "+
                               " tb_detalle_ActiviadPersonal.fechaasig, tb_detalle_ActiviadPersonal.horaasig, "+
                               " tb_detalle_ActiviadPersonal.fechaejecucion,tb_detalle_ActiviadPersonal.horaejecucion) "+
                               " values "+
                               " ("+codResponsable+", "+codactividad+", "+
                               " now(), now(), "+
                               fechaEjecucion+" , '"+horaEjecucion+"')"; 
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool updateObservacionPersonalAsignado(int codactividad, int codPersonal, string detalle)
        {
            string consulta = "update tb_detalle_ActiviadPersonal set tb_detalle_ActiviadPersonal.detalle = '" + detalle + "' " +
                               " where  tb_detalle_ActiviadPersonal.codres = " + codPersonal + " and " +
                               " tb_detalle_ActiviadPersonal.codact = " + codactividad;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool updateCierreActividadPersonal(int codactividad, string detalleCierre, int codUserCierre)
        {
            string consulta = "update tb_ActividadPersonal " +
                               " set tb_ActividadPersonal.detallecierre = '" + detalleCierre + "', " +
                               " tb_ActividadPersonal.codusercierre = " + codUserCierre + ", " +
                               " tb_ActividadPersonal.fechacierre = now(), " +
                               " tb_ActividadPersonal.horacierre = now(), " +
                               " tb_ActividadPersonal.estado = false" +
                               " where tb_ActividadPersonal.codigo =" + codactividad;
            return ConecRes.ejecutarMySql(consulta);
        }


        

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        //----------------------------nuevo


        public bool insertActividadesConEvento(string detalleActividad, int codUserInicio, string fechaExpiracion, int codDetalleTecnicoEvento)
        {
            string consulta = "insert into tb_ActividadPersonal " +
                               " (tb_ActividadPersonal.fecha,tb_ActividadPersonal.hora,tb_ActividadPersonal.fechaexpiracion, " +
                               " tb_ActividadPersonal.detalle,tb_ActividadPersonal.coduserinicio, " +
                               " tb_ActividadPersonal.estado,cod_detalletecnicoevento) " +
                               " values " +
                               " (now(),now()," + fechaExpiracion + ", " +
                              " ' " + detalleActividad + "' , " + codUserInicio + " ,1," + codDetalleTecnicoEvento + ");";
            return ConecRes.ejecutarMySql(consulta);
        }
        
    }
}