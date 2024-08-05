using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_tareasTecnico
    {
        private DA_tareasTecnicos Dtecnico = new DA_tareasTecnicos();
        public NA_tareasTecnico() { }

        public bool insertTareas(string detalleTarea, int codUserInicio, int codEdificio, string nombreEdificio)
        {
            return Dtecnico.insertTareas(detalleTarea, codUserInicio, codEdificio,  nombreEdificio);
        }

        public bool deleteTareas(int codTarea)
        {
            return Dtecnico.deleteTareas(codTarea);
        }

        public bool updateTarea(int codTarea, string detalleTarea)
        {
            return Dtecnico.updateTarea(codTarea, detalleTarea);
        }

        public bool updateTareaTecnicoAsignado(int codTarea, int codUserAsignado)
        {
            return Dtecnico.updateTareaTecnicoAsignado(codTarea, codUserAsignado);
        }

        public bool insertarDetalleTareaTecnico(int codTarea, int codresponsable)
        {
            return Dtecnico.insertarDetalleTareaTecnico(codTarea, codresponsable);
        }


        public int codultimaTareaInsertada()
        {
            try
            {
                string consulta = "select max(t.codigo) from tb_tareastecnico t";
                DataSet dato = Dtecnico.getDatos(consulta);
                int resultado = Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
                return resultado;
            }
            catch {
                return -1;
            }
        }


        public DataSet mostrarTareas(string detalleTarea, string nombreTecnico, string nombreEdificio, bool estado) {
           /* string consulta = "select t.codigo,t.nombreEdificio,date_format(t.fecha,'%d/%m/%y') as 'fecha',t.hora, t.detalle, res.nombre as 'TecnicoAsignado' " +
                               " from  " +
                               " tb_tareastecnico t " +
                               " left join tb_responsable res  on t.coduserultimoasignado = res.codigo " +
                               " where  " +
                               " t.estado = "+estado+" and "+                               
                               " t.detalle like '%" + detalleTarea + "%' "; */
            string consulta = "select t.codigo,t.nombreEdificio,date_format(t.fecha,'%d/%m/%y') as 'fecha',t.hora, t.detalle, " +
                               " res.nombre as 'PersonalAsignado' , " +
                               " res1.nombre as 'InicioTarea',  " +
                               " t.detallecierre, " +
                               " res2.nombre as 'CierreTarea', " +
                               " date_format(t.fechacierre,'%d/%m/%y') as 'CierreFecha', " +
                               " t.horacierre, " +
                               " t1.nombre as 'Tec_Asignado_Sis', "+
                               " t2.nombre as 'Supervisor_Sis', "+
                               " t3.nombre as 'Rin_Sis' , "+
                               " t4.nombre as 'Rcc_Sis'  "+
                               " from    " +
                               " tb_tareastecnico t  " +
                               " left join tb_responsable res  on t.coduserultimoasignado = res.codigo  " +
                               " left join tb_responsable res1  on t.coduserinicio = res1.codigo " +
                               " left join tb_responsable res2  on t.codusercierre = res2.codigo " +
                               " LEFT JOIN "+
                               " ( "+
                               " select eqaux.cod_proyecto, resaux.nombre  "+
                               " from tb_equipo eqaux, tb_responsable resaux "+
                               " where  "+
                               " eqaux.cod_tecmantenimiento = resaux.codigo "+
                               " group by eqaux.cod_proyecto "+
                               " ) AS t1 ON (t1.cod_proyecto = t.codedi ) "+
                               " LEFT JOIN "+
                               " ( "+
                               " select eqaux1.cod_proyecto, resaux1.nombre  "+
                               " from tb_equipo eqaux1, tb_responsable resaux1 "+
                               " where  "+
                               " eqaux1.cod_supervisor = resaux1.codigo "+
                               " group by eqaux1.cod_proyecto "+
                               " ) AS t2 ON (t2.cod_proyecto = t.codedi) "+
                               " LEFT JOIN "+
                               " ( "+
                               " select eqaux2.cod_proyecto, resaux2.nombre  "+
                               " from tb_equipo eqaux2, tb_responsable resaux2 "+
                               " where  "+
                               " eqaux2.cod_rin = resaux2.codigo "+
                               " group by eqaux2.cod_proyecto "+
                               " ) AS t3 ON (t3.cod_proyecto = t.codedi) "+
                               " LEFT JOIN "+
                               " ( "+
                               " select eqaux3.cod_proyecto, resaux3.nombre  "+
                               " from tb_equipo eqaux3, tb_responsable resaux3 "+
                               " where  "+
                               " eqaux3.cod_rcc = resaux3.codigo "+
                               " group by eqaux3.cod_proyecto "+
                               " ) AS t4 ON (t4.cod_proyecto = t.codedi) "+
                               " where " +
                               " t.estado = " + estado + " and " +
                               " t.detalle like '%" + detalleTarea + "%' ";
                               
            if(!nombreTecnico.Equals("")){
                consulta = consulta + " and res.nombre like '%" + nombreTecnico + "%'";
            }

            if (!nombreEdificio.Equals("")) {
               consulta = consulta + "and t.nombreEdificio like '%" + nombreEdificio + "%' ";
            }
                               
            return Dtecnico.getDatos(consulta);
        }


        public DataSet getTecnicosAsignados(int codTarea) {
            string consulta = "select res.codigo, res.nombre " +
                               " from tb_detalle_tareatecnico tec, tb_responsable res " +
                               " where  " +
                               " tec.codres = res.codigo and " +
                               " tec.codtar = " + codTarea +
                               " order by TIMESTAMP(tec.fechaasig,tec.horaasig) desc";
              return Dtecnico.getDatos(consulta);
        }

        public DataSet getTecnicoAsignado(int codTarea, int codTecnico)
        {
            string consulta = "select date_format(tec.fechaasig,'%d/%m/%Y'),tec.horaasig, tec.detalle "+
                               " from tb_detalle_tareatecnico tec "+
                               " where "+
                               " tec.codres = "+codTecnico+" and "+
                               " tec.codtar = "+codTarea;
            return Dtecnico.getDatos(consulta);
        }


        public bool updateObservacionTecnico(int codtarea, int codtecnico, string detalle)
        {
            return Dtecnico.updateObservacionTecnico(codtarea, codtecnico, detalle);
        }

        public bool updateCierreTareaTecnico(int codTarea, string detalleCierre, int codUserCierre)
        {

            return Dtecnico.updateCierreTareaTecnico(codTarea, detalleCierre, codUserCierre);
        }


        public bool updateDetalleTareaCoti(int codTarea, int codCoti)
        {
            return Dtecnico.updateDetalleTareaCoti( codTarea,  codCoti);
        }

  /*      public bool insertTareasConEventosCallCenter(string detalleTarea, int codUserInicio, int codEdificio, string nombreEdificio, int cod_detalletecnicoevento, int codTecnico)
        {
            bool bandera = Dtecnico.insertTareasConEventosCallCenter( detalleTarea,  codUserInicio,  codEdificio,  nombreEdificio,  cod_detalletecnicoevento);
            int codigoTarea = codultimaTareaInsertada();
            bool bandera2 =  updateTareaTecnicoAsignado(codigoTarea, codTecnico);
            bool bandera3 = insertarDetalleTareaTecnico(codigoTarea, codTecnico);
            if(bandera == true && bandera2 == true && bandera3 == true){
            return true;
            }else
                return false;           
        }

        */
    }
}