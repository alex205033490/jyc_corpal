using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DEstadisticaParqueAscensores
    {
        private conexionMySql conexion = new conexionMySql();
        //concat(  ,' ','%')


       public DataSet obtenerEstadisticaParqueAscensores(string mes,string anio) 
        {     
             
     /* string consulta = "select eman.nombre as 'EstadoMantenimiento', ifnull(t4.cantidad,0) as Cantidad, "+
                       " ((ifnull(t4.Cantidad, 0) / (SELECT count(*) FROM tb_equipo e WHERE e.estado = 1)) * 100) AS Porcentaje "+
                       " from tb_estado_mantenimiento eman "+
                       " left join  "+
                       " ( "+
                       " select t3.codigo, t3.nombre, count(t3.codigo) as 'cantidad'  "+
                       " from  "+
                       " ( "+
                       " select t2.codigo, t2.nombre, t2.fecha, t2.hora, t2.codSeguimiento  "+
                       " from  "+
                       " ( "+
                       " select t1.codigo, t1.nombre, t1.fecha, t1.hora, t1.codSeguimiento  "+
                       " from  "+
                       " ( "+
                       " select em.codigo,em.nombre, fem.fecha , fem.hora, fem.codSeguimiento  "+
                       " from  tb_fechaestadomantenimiento fem, tb_estado_mantenimiento em  "+
                       " where fem.codEstadoMan = em.codigo and  "+
                       " year(fem.fecha) = "+anio+" and month(fem.fecha) <= "+mes+
                       " order by timestamp(fem.fecha, fem.hora) desc "+
                       " ) as t1 "+
                       " group by t1.codSeguimiento "+
                       " union "+
                       " select t1.codigo, t1.nombre, t1.fecha, t1.hora, t1.codSeguimiento  "+
                       " from  "+
                       " ( "+
                       " select em.codigo,em.nombre, fem.fecha , fem.hora, fem.codSeguimiento  "+
                       " from  tb_fechaestadomantenimiento fem, tb_estado_mantenimiento em  "+
                       " where fem.codEstadoMan = em.codigo and  "+
                       " year(fem.fecha) < "+anio+  
                       " order by timestamp(fem.fecha, fem.hora) desc "+
                       " ) as t1 "+
                       " group by t1.codSeguimiento "+
                       " )as t2 , tb_seguimiento segui, tb_equipo eq, tb_fechaestadoequipo feq "+
                       " where t2.codSeguimiento = segui.codigo and segui.estado = 1 and segui.years = "+anio+
                       " and eq.codfechaestadoequipo = feq.codigo "+
                       " and feq.codEstadoEquipo = 10 "+
                       " and segui.cod_equipo = eq.codigo "+
                       " group by t2.codSeguimiento "+
                       " ) as t3 "+
                       " group by t3.codigo "+
                       " ) as t4 "+
                       " on t4.codigo = eman.codigo";  */
            string fecha = "01/" + mes + "/" + anio  ;
            DateTime fecha_ = Convert.ToDateTime(fecha);
            fecha_ = fecha_.AddMonths(1);
            string fechaAux = fecha_.ToString("yyyy-MM-dd");

            string consulta = "select eman.nombre as 'EstadoMantenimiento', ifnull(t4.cantidad,0) as Cantidad, "+
                               " ((ifnull(t4.Cantidad, 0) / (SELECT count(*) FROM tb_equipo e WHERE e.estado = 1)) * 100) AS Porcentaje "+
                               " from tb_estado_mantenimiento eman "+
                               " left join  "+
                               " ( "+
                               " select t3.codigo, t3.nombre, count(t3.codigo) as 'cantidad'  "+
                               " from  "+
                               " (  "+
                               " select em.codigo,em.nombre, fem.fecha , fem.hora, fem.codSeguimiento  "+
                               " from  "+
                               " tb_seguimiento seg, tb_fechaestadomantenimiento fem, tb_estado_mantenimiento em, "+
                               " tb_equipo eq, tb_fechaestadoequipo feq  "+
                               " where "+
                               " seg.cod_equipo = eq.codigo and "+
                               " eq.codfechaestadoequipo = feq.codigo and "+
                               " feq.codEstadoEquipo = 10 and "+
                               " seg.codfechaestadoman = fem.codigo and "+
                               " fem.codEstadoMan = em.codigo and "+  
                               " seg.estado = 1 and "+
                               " seg.years = "+anio+" and "+
                               " fem.fecha < '" +fechaAux+ "' " +
                               " group by fem.codSeguimiento "+
                               " order by timestamp(fem.fecha, fem.hora) desc "+
                               " ) as t3 "+
                               " group by t3.codigo "+
                               " ) as t4 "+
                               " on t4.codigo = eman.codigo";
                DataSet lista = conexion.consultaMySql(consulta);
                return lista;            
        }

        /// <summary>
        /// muestra los datos de los estados de mantenimiento siempre y cuando no se encuentre en los estados
        /// 8,9 parado por el cliente y por nosotros
        /// </summary>
        /// <param name="anio"></param>
        /// <returns></returns>
       public DataSet obtenerEstadisticaAnioMantenimiento(string anio) {
           string consulta = "select "+
                               " estadom.nombre AS 'Estado Mantenimiento', "+
                               " IFNULL(t1.Cantidad,0) as 'Cantidad', "+
                               " IFNULL(t1.Porcentaje,0) as 'Porcentaje' "+ 
                               " from tb_estado_mantenimiento estadom "+
                               " left join "+
                               " ( "+
                               " select esman.nombre as 'EstadoEquipo', count(*) as 'Cantidad', (count(*) * 100) / (Select count(*) from tb_equipo) as 'Porcentaje' "+
                               " from tb_equipo eq,  tb_seguimiento seg, "+
                               " tb_fechaestadomantenimiento feq, tb_estado_mantenimiento esman , tb_fechaestadoequipo festaeq " +
                               " where "+
                               " seg.cod_equipo = eq.codigo "+
                               " and seg.codfechaestadoman = feq.codigo "+
                               " and feq.codEstadoMan = esman.codigo "+
                               " and eq.codfechaestadoequipo = festaeq.codigo "+
                               //" and festaeq.codEstadoEquipo not in (8,9) "+
                               " and festaeq.codEstadoEquipo = 10 " +
                               " and seg.years = "+anio+
                               " group by esman.nombre) as t1 "+
                               " ON t1.EstadoEquipo = estadom.nombre";
           DataSet lista = conexion.consultaMySql(consulta);
           return lista;    

       }



        public DataSet obtenerEstadosEquiposFuncionandoEntreFechas(string fecha1,string fecha2,string gestion) {

            string consulta = "select eman.nombre as EstadoMantenimiento, ifnull(t3.Cantidad,0) as Cantidad, "+
                                " ((ifnull(t3.Cantidad, 0) / (SELECT count(*) FROM tb_equipo e WHERE e.estado = 1)) * 100) AS Porcentaje "+
                                " from tb_estado_mantenimiento eman "+
                                " left join "+
                                " ( "+
                                " select t2.codigo, t2.nombre, count(t2.codigo) as Cantidad "+
                                " from "+
                                " (select t1.codigo, t1.nombre, t1.fecha, t1.hora, t1.codSeguimiento "+
                                " from "+
                                " (select em.codigo,em.nombre, fem.fecha , fem.hora, fem.codSeguimiento "+
                                " from  tb_fechaestadomantenimiento fem, tb_estado_mantenimiento em  "+
                                " where fem.codEstadoMan = em.codigo and "+
                                " Date(fem.fecha) between '"+fecha1+"' and '"+fecha2+"'"+
                                " order by fem.fecha desc, fem.hora desc) as t1 "+
                                " group by t1.codSeguimiento)as t2 , tb_seguimiento segui "+
                                " where t2.codSeguimiento = segui.codigo and segui.estado = 1 and segui.years = "+gestion+
                                " group by t2.codigo) as t3 on t3.codigo = eman.codigo";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;  
        }


        public DataSet obtenerEquipoPorEstado(string nombreEstado, string mes,string anio)
        {
         
                string consulta = "select  eq.exbo as 'Chasis', proy.nombre as 'NombreProyecto', "+
                                  " t2.nombre as 'EstadoMantenimienot', "+ 
                                 // " t2.fecha as 'FechaEstado', "+
                                  " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') as FechaActaDefinitiva, "+
                                  " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y') as FechaEquipoEntregado, "+
                                  " t2.hora as 'HoraEstado', "+
                                  " epago.nombre as 'EncargadoPago', epago.celular, segui.lugar_pago "+
                                  " from "+
                                  " (select t1.codigo, t1.nombre, t1.fecha, t1.hora, t1.codSeguimiento "+
                                  " from "+
                                  " (select em.codigo,em.nombre, fem.fecha , fem.hora, fem.codSeguimiento "+
                                  " from  tb_fechaestadomantenimiento fem, tb_estado_mantenimiento em "+
                                  " where fem.codEstadoMan = em.codigo and "+
                                  " year(fem.fecha) = "+anio+" and month(fem.fecha) <= "+mes+
                                  " order by fem.fecha desc, fem.hora desc) as t1 "+
                                  " group by t1.codSeguimiento) as t2 , tb_seguimiento segui, tb_equipo eq, tb_proyecto proy "+
                                  " left join tb_encargado_pago epago on  (proy.codencargado = epago.codigo) "+
                                  " where t2.codSeguimiento = segui.codigo and segui.estado = 1 and segui.years = "+anio+
                                  " and eq.codigo = segui.cod_equipo and proy.codigo = eq.cod_proyecto and "+
                                  " t2.nombre = '"+nombreEstado+"'";
                DataSet lista = conexion.consultaMySql(consulta);
                return lista;            
        }


        /// <summary>
        /// muestra los datos de los estados de mantenimiento siempre y cuando no se encuentre en los estados
        /// 8,9 parado por el cliente y por nosotros
        /// </summary>
        /// <param name="anio"></param>
        /// <returns></returns>
        public DataSet obtenerDatosEstadisticaMantenimientoPorEstado(string NombreEstado, string anio) {
            string consulta = "select eq.exbo, proy.nombre as 'Nombre_Proyecto', esman.nombre as 'Estado_Mantenimiento', "+
                               " DATE_FORMAT(eq.fecha_acta_definitiva, '%d/%m/%Y') as 'Fecha_Acta_definitiva', "+
                               " DATE_FORMAT(eq.fecha_equipo_entregado, '%d/%m/%Y') as 'Fecha_Equipo_Entregado', "+
                               " enp.nombre as Encargado_Pago, enp.celular as 'Nro_Celular', seg.lugar_pago "+
                               " from  tb_equipo eq , tb_seguimiento seg,  "+
                               " tb_fechaestadomantenimiento feq, tb_estado_mantenimiento esman, tb_fechaestadoequipo festaeq , " +
                               " tb_proyecto proy "+
                               " LEFT JOIN tb_encargado_pago enp ON enp.codigo = proy.codEncargado "+
                               " where "+
                               " eq.cod_proyecto = proy.codigo "+
                               " and seg.cod_equipo = eq.codigo "+
                               " and seg.codfechaestadoman = feq.codigo "+
                               " and feq.codEstadoMan = esman.codigo "+
                               " and eq.codfechaestadoequipo = festaeq.codigo "+
                               //" and festaeq.codEstadoEquipo not in (8,9) "+
                               " and festaeq.codEstadoEquipo = 10 " +
                               " and esman.nombre = '"+NombreEstado+"' "+
                               " and seg.years = "+anio;
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;                    
        }


        public int totalEquipos() 
        {
            string consulta = "select count(*) from tb_equipo e where e.estado=1";
            DataSet totalEquipo = conexion.consultaMySql(consulta);
            int total = Convert.ToInt32(totalEquipo.Tables[0].Rows[0][0].ToString());
            return total;
        }
       

        /// <summary>
        /// este metodo devuelve todos los equipos que tienen prevision sin contar los que tienen
        /// el estado Parado por el cliente, y parados por nosotros
        /// </summary>
        /// <param name="anio"></param>
        /// <returns></returns>
         

        public int totalParqueAscensor(string anio) 
        {
            string consulta = "select count(*) as 'TotalEquiposFuncionando' " +
                               " from tb_equipo eq,  tb_seguimiento seg , tb_fechaestadoequipo feq " +
                               " where  " +
                               " seg.cod_equipo = eq.codigo " +
                               " and eq.codfechaestadoequipo = feq.codigo "+
                               // " and feq.codEstadoEquipo not in (8,9) " +
                               " and feq.codEstadoEquipo = 10 " +
                               " and seg.years = " + anio;

                DataSet totalParqueAscensores = conexion.consultaMySql(consulta);
                int total = Convert.ToInt32(totalParqueAscensores.Tables[0].Rows[0][0].ToString());
                return total;
           
        }

          public int totalFaltantesMantenimiento(string anio) 
        {
        
                string consulta = " select count(*) as cantidad "+ 
                                  " from tb_equipo eq, tb_estado_equipo estado , tb_fechaestadoequipo feeq "+ 
                                  " where eq.codfechaestadoequipo = feeq.codigo "+
                                  " and  feeq.codEstadoEquipo = estado.codigo "+
                                  " and  estado.nombre = 'Habilitado' "+ 
                                  " and eq.codigo not in "+
                                  " (select se.cod_equipo "+
                                  " from tb_seguimiento se where se.years = "+anio+" GROUP by se.cod_equipo)";
                DataSet totalParqueAscensores = conexion.consultaMySql(consulta);
                int total = Convert.ToInt32(totalParqueAscensores.Tables[0].Rows[0][0].ToString());
                return total;
           
        }


          public DataSet totalFaltantesMantenimiento2(string anio)
          {

             string consulta = "select eq.exbo, proy.nombre as 'Nombre_Proyecto', eeq.nombre as 'EstadoEquipo', "+
                               " date_format(eq.fecha_acta_definitiva,'%d/%m/%y') as 'fecha_actaDefinitiva', "+
                               " date_format(eq.fecha_equipo_entregado,'%d/%m/%y') as 'fecha_EquipoEntregado'  "+
                               " from tb_equipo eq, tb_estado_equipo estado , tb_fechaestadoequipo feeq , "+
                               " tb_proyecto proy , tb_estado_equipo eeq "+
                               " where eq.codfechaestadoequipo = feeq.codigo  "+
                               " and  feeq.codEstadoEquipo = estado.codigo  "+
                               " and  estado.nombre = 'Habilitado' "+
                               " and proy.codigo = eq.cod_proyecto "+
                               " and feeq.codEstadoEquipo =  eeq.codigo "+
                               " and eq.codigo not in  "+
                               " (select se.cod_equipo  "+
                               " from tb_seguimiento se where se.years = "+anio+" GROUP by se.cod_equipo)";
              DataSet totalParqueAscensores = conexion.consultaMySql(consulta);

              return totalParqueAscensores;

          }

       
        public float totalParqueAscensoresPorcentaje(string anio) 
        {          
               // string consulta = "SELECT sum(suma.Porcentaje) AS Suma FROM(SELECT em.`nombre`, ifnull(t2.amount, 0) AS Cantidad,  concat(((ifnull(t2.amount, 0) / (SELECT count(*) FROM `tb_equipo` e WHERE e.`estado` = 1)) * 100),' ','%') AS `Porcentaje` FROM `tb_estado_mantenimiento` em left join(select em.`nombre` name, count(t1.code)as amount FROM `tb_estado_mantenimiento` em left join( SELECT fem.`codEstadoMan` AS code FROM `tb_seguimiento` s, `tb_fechaestadomantenimiento` fem WHERE s.`codfechaestadoman` = fem.`codigo` AND s.`years`="+Convert.ToInt32(anio)+")t1 ON t1.code=em.codigo GROUP BY t1.code)t2 on em.`nombre`=t2.name)suma";

            string consulta = "select (count(*) * 100) / (Select count(*) from tb_equipo) as 'PorcentajeEquipoFuncionando' " +
                               " from tb_equipo eq,tb_seguimiento seg , tb_fechaestadoequipo feq " +
                               " where  " +
                               " seg.cod_equipo = eq.codigo " +
                               " and eq.codfechaestadoequipo = feq.codigo "+
                              // " and feq.codEstadoEquipo not in (8,9) " +
                               " and feq.codEstadoEquipo = 10 " +
                               " and seg.years = " + anio;
                DataSet totalParqueAscensorPorcentaje = conexion.consultaMySql(consulta);
                float total = float.Parse(totalParqueAscensorPorcentaje.Tables[0].Rows[0][0].ToString());
                return total;          
        }

        public DataSet listarAniosSeguimiento() 
        {
            string consulta = " select DISTINCT(s.`years`) from `tb_seguimiento` s where s.`years` is not null order by s.`years` ";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

    /*   public int candidadEquipo(string mes, string anio) {
            string consulta = "select  COUNT(*) " +
                              " from " +
                              " (select t1.codigo, t1.nombre, t1.fecha, t1.hora, t1.codSeguimiento " +
                                " from " +
                                " (select em.codigo,em.nombre, fem.fecha , fem.hora, fem.codSeguimiento " +
                                " from  tb_fechaestadomantenimiento fem, tb_estado_mantenimiento em " +
                                " where fem.codEstadoMan = em.codigo and " +
                                " year(fem.fecha) = " + anio + " and month(fem.fecha) <= " + mes +
                                " order by fem.fecha desc, fem.hora desc) as t1 " +
                                " group by t1.codSeguimiento) as t2 , tb_seguimiento segui, tb_equipo eq, tb_proyecto proy " +
                                " left join tb_encargado_pago epago on  (proy.codencargado = epago.codigo) " +
                                " where t2.codSeguimiento = segui.codigo and segui.estado = 1 and segui.years = " + anio +
                                " and eq.codigo = segui.cod_equipo and proy.codigo = eq.cod_proyecto ";

            DataSet datoResul = conexion.consultaMySql(consulta);
            int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
            return codUltimo;
        }
        */

        public int candidadEquipoEntreFechas(string fecha1, string fecha2, string gestion) {

            string consulta = "select count(*) " +
                                "from " +
                                "(select t1.codigo, t1.nombre, t1.fecha, t1.hora, t1.codSeguimiento  " +
                                "from " +
                                "(select em.codigo,em.nombre, fem.fecha , fem.hora, fem.codSeguimiento " +
                                "from  tb_fechaestadomantenimiento fem, tb_estado_mantenimiento em  " +
                                "where fem.codEstadoMan = em.codigo and " +
                                "Date(fem.fecha) between '" + fecha1 + "' and '" + fecha2 + "' " +
                                "order by fem.fecha desc, fem.hora desc) as t1  " +
                                "group by t1.codSeguimiento)as t2 , tb_seguimiento segui " +
                                "where t2.codSeguimiento = segui.codigo and segui.estado = 1 and segui.years = " + gestion;
            DataSet datoResul = conexion.consultaMySql(consulta);
            int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
            return codUltimo;
        }

        public float porcentajeEquipoEntreFechas(string fecha1, string fecha2, string gestion)
        {
            string consulta = "select SUM((ifnull(t3.Cantidad, 0) / (SELECT count(*) FROM tb_equipo e WHERE e.estado = 1)) * 100) AS Porcentaje "+
                                " from tb_estado_mantenimiento eman "+
                                " left join "+
                                " ( "+
                                " select t2.codigo, t2.nombre, count(t2.codigo) as Cantidad "+
                                " from "+
                                " (select t1.codigo, t1.nombre, t1.fecha, t1.hora, t1.codSeguimiento "+
                                " from "+
                                " (select em.codigo,em.nombre, fem.fecha , fem.hora, fem.codSeguimiento "+
                                " from  tb_fechaestadomantenimiento fem, tb_estado_mantenimiento em  "+
                                " where fem.codEstadoMan = em.codigo and "+
                                " Date(fem.fecha) between '"+fecha1+"' and '"+fecha2+"' "+
                                " order by fem.fecha desc, fem.hora desc) as t1 "+
                                " group by t1.codSeguimiento)as t2 , tb_seguimiento segui "+
                                " where t2.codSeguimiento = segui.codigo and segui.estado = 1 and segui.years = "+gestion+
                                " group by t2.codigo) as t3 on t3.codigo = eman.codigo ";
            DataSet totalParqueAscensorPorcentaje = conexion.consultaMySql(consulta);
            float total = float.Parse(totalParqueAscensorPorcentaje.Tables[0].Rows[0][0].ToString());
            return total;
        }

    }
}