using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_tareasTecnicos
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DA_tareasTecnicos() { }

        public bool insertTareas(string detalleTarea,int codUserInicio, int codEdificio, string nombreEdificio)
        {
            string codigoEdificio = codEdificio.ToString();
            if (codEdificio == -1)
                codigoEdificio = "NULL";

            string consulta = "insert into tb_tareastecnico "+
                               " (tb_tareastecnico.fecha,tb_tareastecnico.hora,tb_tareastecnico.detalle,tb_tareastecnico.coduserinicio,tb_tareastecnico.estado, tb_tareastecnico.codedi, tb_tareastecnico.nombreEdificio) " +
                               " values "+
                               " (now(),now(),'"+detalleTarea+"',"+codUserInicio+",true,"+codigoEdificio+",'"+nombreEdificio+"')";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool deleteTareas(int codTarea)
        {
            string consulta = "delete from tb_tareastecnico where tb_tareastecnico.codigo = "+codTarea;                               
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool updateTarea(int codTarea, string detalleTarea)
        {
            string consulta = "update tb_tareastecnico set tb_tareastecnico.detalle = '"+detalleTarea+"' where tb_tareastecnico.codigo = "+codTarea;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool updateTareaTecnicoAsignado(int codTarea, int codUserAsignado)
        {
            string consulta = "update tb_tareastecnico set tb_tareastecnico.coduserultimoasignado = " + codUserAsignado + " where tb_tareastecnico.codigo = " + codTarea;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool insertarDetalleTareaTecnico(int codTarea, int codTecnico) {
            string consulta = "insert into tb_detalle_tareatecnico " +
                               " (tb_detalle_tareatecnico.codres, tb_detalle_tareatecnico.codtar, tb_detalle_tareatecnico.fechaasig, tb_detalle_tareatecnico.horaasig) " +
                               " values" +
                               "(" + codTecnico + "," + codTarea + ",now(),now())";
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool updateObservacionTecnico(int codtarea, int codtecnico, string detalle) {
            string consulta = "update tb_detalle_tareatecnico set tb_detalle_tareatecnico.detalle = '"+detalle+"' "+
                               " where  tb_detalle_tareatecnico.codres = "+codtecnico+" and "+
                               " tb_detalle_tareatecnico.codtar = "+codtarea; 
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool updateCierreTareaTecnico(int codTarea, string detalleCierre, int codUserCierre) {
            string consulta = "update tb_tareastecnico "+
                               " set tb_tareastecnico.detallecierre = '"+detalleCierre+"', "+
                               " tb_tareastecnico.codusercierre = "+codUserCierre+", "+
                               " tb_tareastecnico.fechacierre = now(), "+
                               " tb_tareastecnico.horacierre = now(), "+
                               " tb_tareastecnico.estado = false"+
                               " where tb_tareastecnico.codigo ="+codTarea;
            return ConecRes.ejecutarMySql(consulta);
        }


        public bool updateDetalleTareaCoti(int codTarea, int codCoti)
        {
            string consulta = "insert into tb_detalle_tareatec_coti( "+
                               " tb_detalle_tareatec_coti.codtareatec, "+
                               " tb_detalle_tareatec_coti.codcoti, "+
                               " tb_detalle_tareatec_coti.fecha, "+
                               " tb_detalle_tareatec_coti.hora "+
                               " ) "+
                               " values( "+codTarea+", "+codCoti+", now() ,now());";
            return ConecRes.ejecutarMySql(consulta);
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        //------------------nuevo-------
        public bool insertTareasConEventosCallCenter(string detalleTarea, int codUserInicio, int codEdificio, string nombreEdificio, int cod_detalletecnicoevento)
        {
            string codigoEdificio = codEdificio.ToString();
            if (codEdificio == -1)
                codigoEdificio = "NULL";

            string consulta = "insert into tb_tareastecnico " +
                               " (tb_tareastecnico.fecha, tb_tareastecnico.hora, tb_tareastecnico.detalle, tb_tareastecnico.coduserinicio, tb_tareastecnico.estado, tb_tareastecnico.codedi, tb_tareastecnico.nombreEdificio, tb_tareastecnico.cod_detalletecnicoevento) " +
                               " values " +
                               " (now(),now(),'" + detalleTarea + "'," + codUserInicio + ",true," + codigoEdificio + ",'" + nombreEdificio + "', " + cod_detalletecnicoevento + ")";
            return ConecRes.ejecutarMySql(consulta);
        }

    }
}