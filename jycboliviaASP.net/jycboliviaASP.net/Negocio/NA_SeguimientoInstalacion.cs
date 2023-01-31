using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Datos;

namespace jycboliviaASP.net.Negocio
{
    public class NA_SeguimientoInstalacion
    {
       private DA_SeguimientoInstalacion dseguiins = new DA_SeguimientoInstalacion();

        public NA_SeguimientoInstalacion() { }

        public bool insertar( string supervidor, string email,
                              string telefono, string pf_aoi, string pf_plano,
                              string pf_otros, string pjyc_c01, string pjyc_c05,
                              string pjyc_fa1, string pjyc_fa2, string eo_adecuado,
                              string eo_observaciones, string eo_electricidad, string eo_apli_modif_seguri,
                              string eo_cumpliotrorequisito, string ic_fechaexpedicion, 
                              int ic_semanaentregarequerida,                               
                              int semanasestimadasintacion, string contratoinstalacion,
                              int semanaacumuladaportecnicoasignado) {

      return dseguiins.insertar( supervidor,  email,
   telefono, pf_aoi,  pf_plano,
   pf_otros, pjyc_c01, pjyc_c05,
   pjyc_fa1,  pjyc_fa2, eo_adecuado,
   eo_observaciones,  eo_electricidad,  eo_apli_modif_seguri,
   eo_cumpliotrorequisito,  ic_fechaexpedicion,
   ic_semanaentregarequerida, 
   semanasestimadasintacion, contratoinstalacion,
   semanaacumuladaportecnicoasignado);
        }



        public bool modificar(int codSeguiInst, string supervidor, string email,
                              string telefono, string pf_aoi, string pf_plano,
                              string pf_otros, string pjyc_c01, string pjyc_c05,
                              string pjyc_fa1, string pjyc_fa2, string eo_adecuado,
                              string eo_observaciones, string eo_electricidad, string eo_apli_modif_seguri,
                              string eo_cumpliotrorequisito, string ic_fechaexpedicion,  
                              int ic_semanaentregarequerida,                              
                              int semanasestimadasintacion, string contratoinstalacion,
                              int semanaacumuladaportecnicoasignado)
        {

         return dseguiins.modificar(codSeguiInst, supervidor, email,
         telefono, pf_aoi, pf_plano,
         pf_otros, pjyc_c01, pjyc_c05,
         pjyc_fa1, pjyc_fa2, eo_adecuado,
         eo_observaciones, eo_electricidad, eo_apli_modif_seguri,
         eo_cumpliotrorequisito, ic_fechaexpedicion, 
         ic_semanaentregarequerida, 
         semanasestimadasintacion, contratoinstalacion,
         semanaacumuladaportecnicoasignado);
        }




        public bool eliminar()
        {
            return false;
        }

      
        public DataSet BuscarDatos(string exbo , string NombreProyecto) {
            string consulta = "select eq.exbo,proy.nombre as 'Proyecto', proy.direccion as 'Direccion Obra', "+
                                   " eeq.nombre as 'Estado1', t1.supervidor as 'Supervisor (Encargado Obra)', t1.email, "+
                                   " t1.telefono, t1.pf_aoi as 'Pendiente Fabrica (AOI)' , "+
                                   " t1.pf_plano as 'Pendiente Fabrica (Plano)', "+
                                   " t1.pf_otros as 'Pendiente Fabrica (Otros)', "+
                                   " t1.pjyc_c01 as 'Pendiente JyC (C-01)', "+
                                   " t1.pjyc_c05 as 'Pendiente JyC (C-05)', "+
                                   " t1.pjyc_fa1 as 'Pendiente JyC (FAI)', "+
                                   " t1.pjyc_fa2 as 'Pendiente JyC (FAII)', "+
                                   " t1.eo_adecuado as 'Estado Obra (Adecuado)', "+
                                   " t1.eo_observaciones as 'Estado Obra (Observaciones)', "+
                                   " t1.eo_electricidad as 'Estado Obra (Electricidad)', "+
                                   " t1.eo_apli_modif_seguri as 'Estado Obra (otros, aplicaciones, modificaciones, seguridad)', "+
                                   " t1.eo_cumpliotrorequisito as 'Estado Obra (Cumplimiento de Requisitos)', "+
                                   " DATE_FORMAT(t1.ic_fechaexpedicion,'%d/%m/%Y') as 'Info. Critica (Fecha Expedicion)', "+
                                   " DATE_FORMAT(eq.fecha_equipo_obra,'%d/%m/%Y') as 'Info. Critica (Equipo en Obra)',"+
                                   " t1.ic_semanaentregarequerida 'Info. Critica (Semana de Entrega Segun Contrato)', "+
                                   " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y') as 'Info. Critica (Fecha Equipo Entregado Segun Contrato)', " +

                                   " DATE_FORMAT(eq.fechafase1,'%d/%m/%Y') as 'Inico Instalacion (Fase I)', " +
                                   " DATE_FORMAT(eq.fechafase2,'%d/%m/%Y') as 'Inico Instalacion (Fase II)', " +

                                   " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') as 'Fecha de Acta de Entrega y Conclusion Conformidad', " +
                                   " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') as 'Fecha de Entrega y Certificacion Habilitado R-118', " +
                                   " t1.semanasestimadasintacion as 'Semana Estimada de Instalacion', "+
                                   " t1.contratoinstalacion as 'Contrato Instalacion', "+
                                   " t1.semanaacumuladaportecnicoasignado as 'Semana Acumulada Por Tecnico Asignado', "+
                                   " t2.nombre as 'Tecnico Instalador' "+
                                   " from tb_proyecto proy, tb_fechaestadoequipo feq, tb_estado_equipo eeq ,tb_equipo eq "+
                                   " left join ( "+
                                   " select seguin.codigo, "+
                                   " seguin.supervidor, seguin.email, "+
                                   " seguin.telefono, seguin.pf_aoi, seguin.pf_plano, seguin.pf_otros, "+
                                   " seguin.pjyc_c01, seguin.pjyc_c05, seguin.pjyc_fa1, seguin.pjyc_fa2, "+
                                   " seguin.eo_adecuado, seguin.eo_observaciones, seguin.eo_electricidad, "+
                                   " seguin.eo_apli_modif_seguri, seguin.eo_cumpliotrorequisito, "+
                                   " seguin.ic_fechaexpedicion, "+
                                   " seguin.ic_semanaentregarequerida, " +
                                   " seguin.semanasestimadasintacion, "+
                                   " seguin.contratoinstalacion, seguin.semanaacumuladaportecnicoasignado "+
                                   " from tb_seguimientoinstalacion seguin "+
                                   " ) as t1  on (eq.codseginstalacion = t1.codigo) "+
                                   " left join ( "+
                                   " select dtec.codeq,resp.nombre from tb_detalle_tecnico_asignado dtec, tb_responsable resp "+
                                   " where dtec.codresp = resp.codigo and dtec.codcargo = 3 group by dtec.codeq "+
                                   " ) as t2 on (eq.codigo = t2.codeq) "+
                                   " where eq.cod_proyecto = proy.codigo "+
                                   " and eq.codfechaestadoequipo = feq.codigo and feq.codEstadoEquipo = eeq.codigo "+
                                   " and eq.exbo like '%"+exbo+"%' and proy.nombre like '%"+NombreProyecto+"%' "+
                                   " order by proy.nombre asc";
            DataSet TuplasTabla = dseguiins.getDatos(consulta);
            return TuplasTabla;
        }

        public DataSet mostrarEstadosInstalacion() {
            string consulta = "select ei.codigo, ei.nombre from tb_estado_equipo ei";
            DataSet TuplasTabla = dseguiins.getDatos(consulta);
            return TuplasTabla;
        }


        public int getCodigoEstadoInstalacion(string nombre)
        {
            try
            {
                string consulta = "select ei.codigo from tb_estado_equipo ei where ei.nombre = '"+nombre+"'";
                DataSet tupla = dseguiins.getDatos(consulta);
                return Convert.ToInt32(tupla.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception) {
                return -1;
            }

        }

        public int ultimoinsertado()
        {
            try
            {
                string consulta = "SELECT MAX(segui.codigo) FROM  tb_seguimientoinstalacion segui";
                DataSet datoResul = dseguiins.getDatos(consulta);
                int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codUltimo;
            }
            catch (Exception)
            {
                return -1;
            }

        }

    }
}