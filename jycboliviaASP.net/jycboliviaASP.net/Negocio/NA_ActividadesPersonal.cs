using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_ActividadesPersonal
    {
        DA_ActividadesPersonal Dactividad = new DA_ActividadesPersonal();

        public NA_ActividadesPersonal() {         
        }



        public bool insertActividades(string detalleActividad, int codUserInicio, string fechaExpiracion)
        {
               return Dactividad.insertActividades( detalleActividad,  codUserInicio,  fechaExpiracion);
        }

        public bool deleteActividad(int codActividad)
        {            
            return Dactividad.deleteActividad(codActividad);
        }

        public bool updateActividad(int codActividad, string detalleActividad, string fechaExpiracion)
        {
            return Dactividad.updateActividad(codActividad, detalleActividad, fechaExpiracion);
        }

        public bool updateActividadAsignado(int codActividad, int codUserAsignado)
        {            
            return Dactividad.updateActividadAsignado( codActividad,  codUserAsignado);
        }

        public bool insertarDetalleActividadPersonal(int codactividad, int codResponsable, string fechaEjecucion, string horaEjecucion)
        {            
            return Dactividad.insertarDetalleActividadPersonal( codactividad,  codResponsable, fechaEjecucion, horaEjecucion);
        }


        public bool updateObservacionPersonalAsignado(int codactividad, int codPersonal, string detalle)
        {
            return Dactividad.updateObservacionPersonalAsignado(codactividad, codPersonal, detalle);
        }


        public bool updateCierreActividadPersonal(int codactividad, string detalleCierre, int codUserCierre)
        {            
            return Dactividad.updateCierreActividadPersonal(codactividad, detalleCierre, codUserCierre);
        }

        
        public int codultimaActividadInsertada()
        {
            try
            {
                string consulta = "select max(t.codigo) from tb_ActividadPersonal t";
                DataSet dato = Dactividad.getDatos(consulta);
                int resultado = Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
                return resultado;
            }
            catch
            {
                return -1;
            }
        }


        public DataSet mostrarActividades(string detalleActividad, string nombreTecnico,  bool estado)
        {           
            string consulta = " select t.codigo,date_format(t.fecha,'%d/%m/%y') as 'fecha', "+
                                " t.hora, t.detalle,  date_format(da.fechaasig,'%d/%m/%y') as 'FechaAsignacion',  "+
                                " da.horaasig as 'HoraAsignacion',  "+
                                " date_format(da.fechaejecucion,'%d/%m/%y') as 'FechaEjecucion' , "+
                                " da.horaejecucion as 'HoraEjecucion',  da.detalle,  res.nombre as 'PersonalAsignado' , "+
                                " date_format(t.fechaexpiracion,'%d/%m/%y') as 'FechaLimite', "+  
                                " res1.nombre as 'InicioActividad',   t.detallecierre, "+
                                " res2.nombre as 'CierreActividad', "+
                                " date_format(t.fechacierre,'%d/%m/%y') as 'CierreFecha',  t.horacierre "+
                                " from  "+   
                                " tb_ActividadPersonal t "+
                                " left join tb_responsable res  on t.coduserultimoasignado = res.codigo "+
                                " left join tb_responsable res1  on t.coduserinicio = res1.codigo "+ 
                                " left join tb_responsable res2  on t.codusercierre = res2.codigo "+
                                " left join tb_detalle_activiadpersonal da on (da.codact = t.codigo and da.codres = t.coduserultimoasignado) "+
                                " where "+
                                " t.estado = True and "+
                                " t.detalle like '%"+detalleActividad+"%'  order by t.fecha desc";

            if (!nombreTecnico.Equals(""))
            {
                consulta = consulta + " and res.nombre like '%" + nombreTecnico + "%'";
            }

            
            return Dactividad.getDatos(consulta);
        }


        public DataSet getPersonalAsignados(int codActividad)
        {
            string consulta = "select res.codigo, res.nombre " +
                               " from tb_detalle_ActiviadPersonal tec, tb_responsable res " +
                               " where  " +
                               " tec.codres = res.codigo and " +
                               " tec.codact = " + codActividad +                               
                               " order by TIMESTAMP(tec.fechaasig,tec.horaasig) desc";
            return Dactividad.getDatos(consulta);
        }

        public DataSet getPersonalAsignado(int codActividad, int codPersonal)
        {
            string consulta = "select date_format(tec.fechaasig,'%d/%m/%Y'),tec.horaasig, tec.detalle " +
                               " from tb_detalle_ActiviadPersonal tec " +
                               " where " +
                               " tec.codres = " + codPersonal + " and " +
                               " tec.codact = " + codActividad;
            return Dactividad.getDatos(consulta);
        }


        public bool updateObservacionTecnico(int codactividad, int codPersonal, string detalle)
        {
            return Dactividad.updateObservacionPersonalAsignado(codactividad, codPersonal, detalle);
        }

        public bool updateCierreTareaTecnico(int codActividad, string detalleCierre, int codUserCierre)
        {
            return Dactividad.updateCierreActividadPersonal(codActividad, detalleCierre, codUserCierre);
        }

        //--------------nuevo
       

    }
}