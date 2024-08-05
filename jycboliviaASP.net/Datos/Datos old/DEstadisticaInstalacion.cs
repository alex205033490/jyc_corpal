using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DEstadisticaInstalacion
    {
        private conexionMySql conexion = new conexionMySql();

        public DataSet obtenerEstadisticaInstalacion()
        {
            string consulta = "select estadoEq.nombre, ifnull(t1.cantidad,0) as cantidad, "+
                               " (ifnull(t1.cantidad,0)/(select count(*) from tb_equipo) * 100 ) as Porcentaje "+         
                               " from tb_estado_equipo estadoEq "+
                               " left join "+
                               " (select estado.codigo,estado.nombre, count(*) as cantidad "+
                               " from tb_equipo eq, tb_fechaestadoequipo feeq ,tb_estado_equipo estado "+
                               " where "+
                               " eq.codfechaestadoequipo = feeq.codigo and "+
                               " feeq.codEstadoEquipo = estado.codigo and "+
                               " eq.estado = 1 "+
                               " group by estado.codigo) "+
                               " as t1 on estadoEq.codigo = t1.codigo"; 
            DataSet estadisticaInstalacion = conexion.consultaMySql(consulta);
            return estadisticaInstalacion;
        }

        public DataSet obtenerEstadisticaInstalacionPorMesAnio(string mes, string anio) {
            string consulta = "select eman.nombre as EstadoMantenimiento, ifnull(t3.Cantidad,0) as Cantidad, "+
                              " ((ifnull(t3.Cantidad, 0) / (SELECT count(*) FROM tb_equipo e WHERE e.estado = 1)) * 100) AS Porcentaje "+
                              " from tb_estado_equipo eman "+
                              " left join "+
                              " ( "+
                              " select t2.codigo, t2.nombre, count(t2.codigo) as Cantidad "+ 
                              " from "+
                              " (select t1.codigo, t1.nombre, t1.fecha, t1.hora, t1.codEquipo "+
                              " from "+
                              " (select em.codigo,em.nombre, fem.fecha , fem.hora, fem.codEquipo "+
                              " from  tb_fechaestadoequipo fem, tb_estado_equipo em  "+
                              " where fem.codEstadoEquipo = em.codigo and "+
                              " year(fem.fecha) = "+anio+" and month(fem.fecha) <= "+mes+
                              " order by fem.fecha desc, fem.hora desc) as t1 "+
                              " group by t1.codEquipo)as t2 , tb_equipo equipo "+
                              " where t2.codEquipo = equipo.codigo and equipo.estado = 1 "+
                              " group by t2.codigo) as t3 on t3.codigo = eman.codigo";
            DataSet estadisticaInstalacion = conexion.consultaMySql(consulta);
            return estadisticaInstalacion;
        }



        public DataSet obtenerEquipoPorEstado(string nombreEstado)
        {
            string consulta = "select eq.codigo, eq.exbo as 'Chasis', pro.nombre as 'NombreProyecto', pro.direccion as 'Direccion_Edificio', "+
                              " estadoeq.nombre as 'EstadoEquipo', ac.nombre as 'Actualizacion', "+
                              " eq.tipologia, teq.nombre as 'TipoEquipo', m.nombre as 'MarcaEquipo' , "+
                              " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y') as 'FechaActaProvicional', "+
                              " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') as 'FechaActaTecnico', "+
                              " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') as 'FechaActaDefinitiva', "+
                              " DATE_FORMAT(eq.fecha_equipo_obra,'%d/%m/%Y') as 'FechaEquipoenObra', "+
                              " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y') as 'FechaEquipoEntregado' "+
                              " FROM  tb_proyecto pro,  tb_equipo eq "+
                              " left join tb_tipoequipo teq on (eq.codtipoequipo = teq.codigo) "+
                              " left join tb_marca m on (eq.codmarca = m.codigo) "+
                              " left join tb_actualizacion ac on (eq.codActualizacion = ac.codigo) , "+
                              " tb_fechaestadoequipo feeq, tb_estado_equipo estadoeq "+
                              " where eq.cod_proyecto = pro.codigo and  eq.estado = 1 and eq.codfechaestadoequipo = feeq.codigo and "+
                              " feeq.codEstadoEquipo = estadoeq.codigo and estadoeq.nombre = '"+nombreEstado+"'  "+
                              " order by pro.nombre asc";
            DataSet estadisticaInstalacion = conexion.consultaMySql(consulta);
            return estadisticaInstalacion;
        }

        public int totalEquipoPorFuncionar()  
        {
            try
            {
                string consulta = "select count(*) as cantidad " +
                                  " from tb_equipo eq, tb_estado_equipo estado , tb_fechaestadoequipo feeq " +
                                  " where "+
                                  " eq.estado = 1 and "+
                                  " eq.codfechaestadoequipo = feeq.codigo and " +
                                  " feeq.codEstadoEquipo = estado.codigo and " +
                                  " estado.nombre <> 'Habilitado' "+
                                  " and eq.codigo not in  "+
                                  " ( "+
                                  " select eq.codigo "+
                                   " from tb_equipo eq,  tb_seguimiento seg, tb_fechaestadoequipo feq "+
                                   " where  "+
                                   " seg.cod_equipo = eq.codigo "+
                                   " and eq.codfechaestadoequipo = feq.codigo "+
                                   //" and feq.codEstadoEquipo not in (8,9) "+                                  
                                   " and feq.codEstadoEquipo = 10 " +                                  
                                   " and eq.estado = 1 ) ";
                DataSet totalFuncionar = conexion.consultaMySql(consulta);
                int total = Convert.ToInt32(totalFuncionar.Tables[0].Rows[0][0].ToString());
                return total;
            }
            catch(Exception) {
                return -1;
            }
        }

        public int totalEquipoPorFuncionarMantenimiento(int anio)
        {
            try
            {
                string consulta = "select count(*) as cantidad " +
                                  " from tb_equipo eq, tb_estado_equipo estado , tb_fechaestadoequipo feeq " +
                                  " where eq.codfechaestadoequipo = feeq.codigo and " +
                                  " feeq.codEstadoEquipo = estado.codigo and " +
                                  " estado.nombre <> 'Habilitado' "+
                                  " and eq.codigo not in  "+
                                  " ( "+
                                  "  select eq.codigo "+
                                  "  from tb_equipo eq,  tb_seguimiento seg, tb_fechaestadoequipo feq "+
                                  "  where  "+
                                  "  seg.cod_equipo = eq.codigo "+
                                  "  and eq.codfechaestadoequipo = feq.codigo "+
                                  // "  and feq.codEstadoEquipo not in (8,9) "+
                                  "  and feq.codEstadoEquipo = 10 " +
                                  "  and seg.years = "+anio+
                                   " ) ";
                DataSet totalFuncionar = conexion.consultaMySql(consulta);
                int total = Convert.ToInt32(totalFuncionar.Tables[0].Rows[0][0].ToString());
                return total;
            }
            catch (Exception)
            {
                return -1;
            }
        }



        public float totalEquipoPorFuncionarPorcentaje() 
        {
            try
            {
                string consulta = "select SUM(ifnull(t1.cantidad,0)/(select count(*) from tb_equipo) * 100 ) as Porcentaje "+
                                    " from tb_estado_equipo estadoEq "+
                                    " left join (select estado.codigo,estado.nombre, count(*) as cantidad  "+
                                    " from tb_equipo eq, tb_estado_equipo estado , tb_fechaestadoequipo feeq "+
                                    " where "+
                                    " eq.estado = 1 and "+
                                    " eq.codfechaestadoequipo = feeq.codigo and feeq.codEstadoEquipo = estado.codigo "+
                                    " group by estado.codigo) as t1 on estadoEq.codigo = t1.codigo and estadoEq.nombre <>'Habilitado'";
                DataSet totalPorcentaje = conexion.consultaMySql(consulta);
                float total = float.Parse(totalPorcentaje.Tables[0].Rows[0][0].ToString());
                return total;
            }
            catch(Exception){
                return -1;
            }
        }
    }
}