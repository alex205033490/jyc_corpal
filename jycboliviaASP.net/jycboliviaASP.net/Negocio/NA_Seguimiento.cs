using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;
using jycboliviaASP.net.DatosSimec;



namespace jycboliviaASP.net.Negocio
{
    public class NA_Seguimiento
    {
        private DA_Seguimiento Dsegui = new DA_Seguimiento();
        public NA_Seguimiento() { }

        public bool insertar(string detalle, string hora_Cobro, string dia_cobro, string lugarPago, string fechaContrato, int mesesGratis, string mg_ini, string mg_fin, int codTipoPago, int codEquipo, int year)
        {
            return Dsegui.insertar(detalle, hora_Cobro, dia_cobro, lugarPago, fechaContrato, mesesGratis, mg_ini, mg_fin, codTipoPago, codEquipo, 1, year);
        }

        public bool modificar(int codigo, string detalle, string hora_Cobro, string dia_cobro, string lugarPago, string fechaContrato, int mesesGratis, string mg_ini, string mg_fin, int codTipoPago, int year, int codFechaEstadoMan)
        {
            return Dsegui.modificar(codigo, detalle, hora_Cobro, dia_cobro, lugarPago, fechaContrato, mesesGratis, mg_ini, mg_fin, codTipoPago, year, codFechaEstadoMan);
        }

        public bool modificarFechaEstadoMan(int codSeguimiento, int codfechaEstadoMan) {
            return Dsegui.modificarFechaEstadoMan(codSeguimiento, codfechaEstadoMan);        
        }

        public bool eliminar(int codigo)
        {
            return Dsegui.eliminar(codigo, 0);
        }

        public DataSet mostrarAllDatos()
        {
            string consulta = "select segui.* from tb_seguimiento segui where segui.estado = 1";
            return Dsegui.getDatos(consulta);
        }

        public DataSet mostrarSeguimiento(int codigoEquipoExbo) {
            /*string consulta = "select segui.codigo,segui.years ,segui.Detalle as 'Detalle_del_Seguimiento',segui.hora_cobro,"+
                            " DATE_FORMAT(segui.dia_cobro,'%d/%m/%Y') as 'dia_cobro',segui.lugar_pago as 'Lugar_de_Pago',"+                            
                            " DATE_FORMAT(segui.fecha_contrato,'%d/%m/%Y') as 'fecha_contrato',segui.mes_gratis,"+
                            " DATE_FORMAT(segui.mg_ini,'%d/%m/%Y') as 'Mes Gratis Inicial',"+
                            " DATE_FORMAT(segui.mg_fin,'%d/%m/%Y') as 'Mes Gratis Fin',tp.nombre as 'Tipo_de_Pago', "+
                            " eman.nombre as 'Estado_Mantenimiento' " +
                            " from tb_seguimiento segui, tb_tipopago tp, tb_fechaestadomantenimiento fechaman, tb_estado_mantenimiento eman  " +
                            " where segui.codfechaestadoman = fechaman.codigo and fechaman.codEstadoMan = eman.codigo and "+ 
                            " segui.cod_tipopago = tp.codigo and segui.estado = 1  and segui.cod_equipo = " + codigoEquipoExbo + " order by segui.years desc";*/
            string consulta = "select "+
                               " segui.codigo,segui.years ,segui.Detalle as 'Detalle_del_Seguimiento', "+
                               " segui.lugar_pago as 'Lugar_de_Pago', "+                             
                               " DATE_FORMAT(segui.fecha_contrato,'%d/%m/%Y') as 'fecha_contrato', "+
                               " eman.nombre as 'Estado_Mantenimiento' "+
                               " from tb_seguimiento segui, "+
                               " tb_fechaestadomantenimiento fechaman, "+
                               " tb_estado_mantenimiento eman "+ 
                               " where "+
                               " segui.codfechaestadoman = fechaman.codigo and "+
                               " fechaman.codEstadoMan = eman.codigo and "+
                               " segui.estado = 1  and segui.cod_equipo = "+codigoEquipoExbo+
                               " order by segui.years desc";
            return Dsegui.getDatos(consulta);
        }


        public DataSet getOnlySeguimiento(int codSeguimiento) {
            string consulta = "select segui.codigo,segui.years,segui.hora_cobro,DATE_FORMAT(segui.dia_cobro,'%d/%m/%Y') as 'dia_cobro'," +
                                "segui.cod_tipopago,segui.lugar_pago," +                                
                                "DATE_FORMAT(segui.fecha_contrato,'%d/%m/%Y') as 'Fecha_Contrato'," +
                                "segui.mes_gratis," +
                                "DATE_FORMAT(segui.mg_ini,'%d/%m/%Y') as 'Mes_Gratis Inicial'," +
                                "DATE_FORMAT(segui.mg_fin,'%d/%m/%Y') as 'Mes_Gratis Fin'," +
                                "segui.Detalle, fechaman.codEstadoMan "+  
                                " from tb_seguimiento segui, tb_fechaestadomantenimiento fechaman "+
                                "where segui.codfechaestadoman = fechaman.codigo and segui.codigo = " + codSeguimiento;
            return Dsegui.getDatos(consulta);
        }

        public int ultimoinsertado() {
            try
            {
                string consulta = "SELECT MAX(segui.codigo) FROM  tb_seguimiento segui";
                DataSet datoResul = Dsegui.getDatos(consulta);
                int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codUltimo;
            }
            catch (Exception ) {
                return -1;
            }

        }

        public DataSet GetCuadrosXXX(int year, string Exbo, string nombreProyecto) {
           
            string consulta = " select proy.nombre as 'Edificio',  eq.exbo,  epago.nombre as 'Nombre EncargadoPago',  "+
                               " epago.celular,  act.nombre as 'Actualizacion',  "+
                               " proy.direccion,  eeq.nombre as 'EstadoEquipo',  "+
                               " segui.Detalle as 'Seguimiento',  segui.hora_cobro, "+  
                               " segui.dia_cobro,   tp.nombre as 'Plan Pago',  segui.lugar_pago,  "+
                               " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y') as 'Fecha Acta Provicional',   "+
                               " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') as 'Fecha Acta Tecnico',  "+
                               " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') as 'Fecha Acta Definitiva',  "+
                               " segui.mes_gratis,  DATE_FORMAT(segui.mg_ini,'%d/%m/%Y') as 'Mes Gratis Inicial', "+  
                               " DATE_FORMAT(segui.mg_fin,'%d/%m/%Y') as 'Mes Gratis Final'  "+
                               " from tb_proyecto proy  left join tb_encargado_pago epago on (proy.codEncargado = epago.codigo),  "+
                               " tb_equipo eq  "+
                               " left join tb_actualizacion act on (eq.codActualizacion = act.codigo)  "+
                               " left join (  select feqAUX.codigo,eqAux.nombre from tb_fechaestadoequipo feqAUX, tb_estado_equipo eqAux where feqAUX.codEstadoEquipo = eqAux.codigo  ) as eeq on (eq.codfechaestadoequipo = eeq.codigo),  "+
                               " tb_seguimiento segui  "+
                               " left join tb_tipopago tp on (segui.cod_tipopago = tp.codigo )   "+
                               " where segui.cod_equipo = eq.codigo and  eq.cod_proyecto = proy.codigo "+
                               " and  segui.years = "+year+" and eq.exbo like '%"+Exbo+"%' and proy.nombre like '%"+nombreProyecto+"%' "; 
            return Dsegui.getDatos(consulta);
        }

        public DataSet getCuadrosXXX_2(int year, string Exbo, string nombreProyecto)
        {
            DataSet tabla = Dsegui.getCuadrosXXX_Mantenimiento(year, Exbo, nombreProyecto);
            return tabla;
           // return Dsegui.RellenarCuadrosXXX_SIN_Mantenimiento(tabla, year, Exbo, nombreProyecto);
        }


        public DataSet getAllyearSeguimiento() {
            string consulta = "select segui.codigo,segui.years from tb_seguimiento segui group by segui.years desc";
            return Dsegui.getDatos(consulta);
        }

        public int getCodigoSeguimiento(int codEquipo, int year) {
            try
            {
                string consulta = "select segui.codigo from tb_seguimiento segui where segui.estado = 1 and segui.cod_equipo = "+codEquipo+" and segui.years = "+year;
                DataSet datoResul = Dsegui.getDatos(consulta);
                int codigo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codigo;
            }
            catch (Exception) {
                return -1;
            }        
        }

        public bool tieneDeudasPendientes_mayorA(string exbo, int nroMeses, int anio ) {
            try{

                string consultaAux = "select  YEAR(current_date()) ; ";
                DataSet datoResul = Dsegui.getDatos(consultaAux);
                int anioActual = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                string consulta = "";
                if (anio == anioActual)
                {
                    consulta = "select det.codSeg, seg.years, eq.exbo, proy.nombre as 'Edificio', count(det.pago) as 'MesesAtrazado', sum(det.monto_pagar) as 'Deuda' " +
                                       " from tb_detalle_segme det, tb_seguimiento seg, tb_equipo eq, tb_proyecto proy " +
                                       " where " +
                                       " proy.codigo = eq.cod_proyecto and " +
                                       " eq.codigo = seg.cod_equipo and " +
                                       " eq.exbo = '" + exbo + "' and " +                                       
                                       " seg.codigo = det.codSeg and " +
                                       " seg.years = " + anio + " and " +
                                       " det.pago = 'NO' and det.codMe <= (month(current_date())-1) " +
                                       " group by det.codSeg  having count(det.pago) > " + nroMeses;

                }
                else
                {

                    consulta = "select det.codSeg, seg.years, eq.exbo, proy.nombre as 'Edificio', count(det.pago) as 'MesesAtrazado', sum(det.monto_pagar) as 'Deuda' " +
                                           " from tb_detalle_segme det, tb_seguimiento seg, tb_equipo eq, tb_proyecto proy " +
                                           " where " +
                                           " proy.codigo = eq.cod_proyecto and " +
                                           " eq.codigo = seg.cod_equipo and " +
                                           " eq.exbo = '" + exbo + "' and " +                                           
                                           " seg.codigo = det.codSeg and " +
                                           " seg.years = " + anio + " and " +
                                           " det.pago = 'NO' and det.codMe <= 12 " +
                                           " group by det.codSeg  having count(det.pago) > " + 0;


                }


            datoResul = Dsegui.getDatos(consulta);

            int cantidad = datoResul.Tables[0].Rows.Count;
            if (cantidad > 0)
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


        /** todas las posibles tuplas con morosos que comiencen con el nombre del edificio */
        public DataSet getEquiposMantenimientoMorosos(string exbo, string edificio, int meses, int anio) {
            string consultaAux = "select  YEAR(current_date()) ; ";
            DataSet datoResul = Dsegui.getDatos(consultaAux);
            int anioActual = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
            string consulta = "";
            if (anio == anioActual)
            {
                consulta = "select det.codSeg, seg.years, eq.exbo, proy.nombre as 'Edificio', count(det.pago) as 'MesesAtrazado',  (sum(det.monto_pagar)-sum(det.monto_pago)) as 'Deuda' " +
                                   " , ep.nombre as 'EncargadoDelPago', ep.celular,ep.telefono "+
                                   " from tb_detalle_segme det, tb_seguimiento seg, tb_equipo eq, " +
                                   " tb_proyecto proy left join tb_encargado_pago ep on (proy.codEncargado = ep.codigo) "+
                                   " where " +
                                   " proy.codigo = eq.cod_proyecto and " +
                                   " eq.codigo = seg.cod_equipo and " +
                                   " eq.estado = 1 and "+
                                   " eq.exbo like '%" + exbo + "%' and " +
                                   " proy.nombre like '%" + edificio + "%' and " +
                                   " seg.codigo = det.codSeg and " +
                                   " seg.years = " + anio + " and " +
                                   " det.pago = 'NO' and det.codMe <= (month(current_date())-1) " +
                                   " group by det.codSeg  having count(det.pago) >= " + meses;

            }
            else
            {

                consulta = "select det.codSeg, seg.years, eq.exbo, proy.nombre as 'Edificio', count(det.pago) as 'MesesAtrazado',  (sum(det.monto_pagar)-sum(det.monto_pago)) as 'Deuda' " +
                                           " , ep.nombre as 'EncargadoDelPago', ep.celular,ep.telefono " +
                                           " from tb_detalle_segme det, tb_seguimiento seg, tb_equipo eq, " +
                                           " tb_proyecto proy left join tb_encargado_pago ep on (proy.codEncargado = ep.codigo) " +
                                           " where " +
                                           " proy.codigo = eq.cod_proyecto and " +
                                           " eq.codigo = seg.cod_equipo and " +
                                           " eq.estado = 1 and " +
                                           " eq.exbo like '%" + exbo + "%' and " +
                                           " proy.nombre like '%" + edificio + "%' and " +
                                           " seg.codigo = det.codSeg and " +
                                           " seg.years = " + anio + " and " +
                                           " det.pago = 'NO' and det.codMe <= 12 " +
                                           " group by det.codSeg  having count(det.pago) > 0";

              
            }        
            
            return Dsegui.getDatos(consulta);
        }

        /** todas las posibles tuplas con morosos que comiencen con el nombre del edificio */
        public DataSet getTodosEquiposMantenimientoMorosos(string exbo, string edificio, int mesesLimitePermitido)
        {
            string consulAux = "select  YEAR(current_date()) ; ";
            DataSet datoResul = Dsegui.getDatos(consulAux);
            int anioActual = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());

            string consulta = "SELECT t1.cod_equipo,'Todos' as 'Anios' ,t1.exbo, t1.nombre as 'Edificio', "+
                               " sum(t1.MesesAtrazado) as 'MesesAtrazado', sum(t1.DeudaTotal) as 'DeudaTotal' "+
                               " , t1.nombre as 'EncargadoDelPago', t1.celular, t1.telefono "+   
                               " FROM "+
                               " ( "+
                               " select seg.cod_equipo,eq.exbo, proy.nombre, count(*) as MesesAtrazado,  "+
                               " sum(sm.monto_pagar) as DeudaTotal "+
                               " , ep.nombre as 'EncargadoDelPago', ep.celular,ep.telefono "+	   
                               " from tb_equipo eq, "+ 
                               " tb_seguimiento seg, tb_detalle_segme sm, "+
                               " tb_proyecto proy  "+
                               " left join tb_encargado_pago ep on (proy.codEncargado = ep.codigo) "+
                               " where  eq.codigo = seg.cod_equipo and "+
                               " eq.estado = 1 and " +
                               " eq.cod_proyecto = proy.codigo and "+ 
                               " sm.codSeg = seg.codigo and "+ 
                               " proy.nombre like '%"+edificio+"%' "+
                               " and  eq.exbo like '%"+exbo+"%' "+
                               " and   sm.pago = 'No' "+
                               " and   seg.years = "+anioActual+
                               " and  sm.codMe <= (month(current_date())-1) "+ 
                               " GROUP by eq.exbo "+
                               " union all "+
                               " select seg.cod_equipo,eq.exbo, proy.nombre, count(*) as MesesAtrazado, "+
                               " sum(sm.monto_pagar) as DeudaTotal "+
                               " , ep.nombre as 'EncargadoDelPago', ep.celular,ep.telefono "+
                               " from tb_equipo eq, "+
                               " tb_seguimiento seg, tb_detalle_segme sm, "+
                               " tb_proyecto proy "+ 
                               " left join tb_encargado_pago ep on (proy.codEncargado = ep.codigo) "+
                               " where  eq.codigo = seg.cod_equipo and "+
                               " eq.estado = 1 and " +
                               " eq.cod_proyecto = proy.codigo and "+ 
                               " sm.codSeg = seg.codigo and  proy.nombre like '%"+edificio+"%' and "+ 
                               " eq.exbo like '%"+exbo+"%' and   sm.pago = 'No' and  seg.years < "+anioActual+
                               " GROUP by eq.exbo "+
                               " ) as t1 "+
                               " GROUP BY t1.exbo "+
                               " having sum(t1.MesesAtrazado) >= " + mesesLimitePermitido;
            return Dsegui.getDatos(consulta);
        }



        
      /*  public DataSet getTodosEquiposMantenimientoMorosos(string exbo, string edificio , int meses)
        {
            string consulAux = "select  YEAR(current_date()) ; ";
            DataSet datoResul = Dsegui.getDatos(consulAux);
            int anioActual = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());

            string consulta = "";
            DataSet Vanios = getAllyearSeguimiento();
            for (int i = 0; i < Vanios.Tables[0].Rows.Count; i++ )
            {
                int anio = Convert.ToInt32(Vanios.Tables[0].Rows[i][1].ToString());
                if(anio <= anioActual){
                    string consultaAux = "select seg.cod_equipo, det.codSeg, seg.years, eq.exbo, proy.nombre as 'Edificio', count(det.pago) as 'MesesAtrazado',  (sum(det.monto_pagar)-sum(det.monto_pago)) as 'Deuda' " +
                                               " , ep.nombre as 'EncargadoDelPago', ep.celular,ep.telefono " +
                                               " from tb_detalle_segme det, tb_seguimiento seg, tb_equipo eq, " +
                                               " tb_proyecto proy left join tb_encargado_pago ep on (proy.codEncargado = ep.codigo) " +
                                               " where " +
                                               " proy.codigo = eq.cod_proyecto and " +
                                               " eq.codigo = seg.cod_equipo and " +
                                               " eq.exbo like '%" + exbo + "%' and " +
                                               " proy.nombre like '%" + edificio + "%' and " +
                                               " seg.codigo = det.codSeg and " +
                                               " seg.years = " + anio + " and ";
                            if (anio == anioActual)
                            {
                                consultaAux = consultaAux + " det.pago = 'NO' and det.codMe <= (month(current_date())-1) " +
                                               " group by det.codSeg  having count(det.pago) >= " + meses;
                            }
                            else
                            {
                                consultaAux = consultaAux + " det.pago = 'NO' and det.codMe <= 12 " +
                                                          " group by det.codSeg  having count(det.pago) > 0 ";
                            }
                            consulta = consulta + consultaAux;

                            if (i < Vanios.Tables[0].Rows.Count - 1)
                            {
                                consulta = consulta + " union ";
                            }
                    }
            }

            if (!consulta.Equals(""))
            {
                consulta = "select t9.cod_equipo, 'Todos' as 'Anios', t9.exbo, t9.Edificio, t9.MesesAtrazado, sum(t9.Deuda) as 'DeudaTotal'" +
                                               " , t9.EncargadoDelPago, t9.celular, t9.telefono " +                                               
                                               " from (" + consulta + ") as t9 " +
                                               " group by t9.cod_equipo";

            }

            return Dsegui.getDatos(consulta);
        }
        */


        /** todas las posibles tuplas con morosos con el nombre del edificio */
        public DataSet getEquiposMantenimientoMorososEspecifico(string exbo, string edificio, int meses, int anio)
        {
            string consultaAux = "select  YEAR(current_date()) ; ";
            DataSet datoResul = Dsegui.getDatos(consultaAux);
            int anioActual = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
            string consulta = "";
            if (anio == anioActual)
            {
                consulta = "select det.codSeg, seg.years, eq.exbo, proy.nombre as 'Edificio', count(det.pago) as 'MesesAtrazado', (sum(det.monto_pagar)-sum(det.monto_pago)) as 'Deuda' " +
                                   " , ep.nombre as 'EncargadoDelPago', ep.celular,ep.telefono " +
                                   " from tb_detalle_segme det, tb_seguimiento seg, tb_equipo eq, " +
                                   " tb_proyecto proy left join tb_encargado_pago ep on (proy.codEncargado = ep.codigo) " +
                                   " where " +
                                   " proy.codigo = eq.cod_proyecto and " +
                                   " eq.codigo = seg.cod_equipo and " +
                                   " eq.exbo like '%" + exbo + "%' and " +
                                   " proy.nombre = '" + edificio + "' and " +
                                   " seg.codigo = det.codSeg and " +
                                   " seg.years = " + anio + " and " +
                                   " det.pago = 'NO' and det.codMe <= (month(current_date())-1) " +
                                   " group by det.codSeg  having count(det.pago) > " + meses;

            }
            else
            {

                consulta = "select det.codSeg, seg.years, eq.exbo, proy.nombre as 'Edificio', count(det.pago) as 'MesesAtrazado',  (sum(det.monto_pagar)-sum(det.monto_pago)) as 'Deuda' " +
                                       " , ep.nombre as 'EncargadoDelPago', ep.celular,ep.telefono " +
                                       " from tb_detalle_segme det, tb_seguimiento seg, tb_equipo eq, " +
                                       " tb_proyecto proy left join tb_encargado_pago ep on (proy.codEncargado = ep.codigo) " +
                                       " where " +
                                       " proy.codigo = eq.cod_proyecto and " +
                                       " eq.codigo = seg.cod_equipo and " +
                                       " eq.exbo like '%" + exbo + "%' and " +
                                       " proy.nombre = '" + edificio + "' and " +
                                       " seg.codigo = det.codSeg and " +
                                       " seg.years = " + anio + " and " +
                                       " det.pago = 'NO' and det.codMe <= 12 " +
                                       " group by det.codSeg  having count(det.pago) > 0";


            }

            return Dsegui.getDatos(consulta);
        }

        
        /** todas las posibles tuplas con morosos con el nombre del edificio */
        public DataSet getTodosEquiposMantenimientoMorososEspecifico(string exbo, string edificio, int mesesLimitePermitido)
        {
            string consulAux = "select  YEAR(current_date()) ; ";
            DataSet datoResul = Dsegui.getDatos(consulAux);
            int anioActual = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());

            string consulta = "SELECT t1.cod_equipo,'Todos' as 'Anios' ,t1.exbo, t1.nombre as 'Edificio', " +
                               " sum(t1.MesesAtrazado) as 'MesesAtrazado', sum(t1.DeudaTotal) as 'DeudaTotal' " +
                               " , t1.nombre as 'EncargadoDelPago', t1.celular, t1.telefono " +
                               " FROM " +
                               " ( " +
                               " select seg.cod_equipo,eq.exbo, proy.nombre, count(*) as MesesAtrazado,  " +
                               " sum(sm.monto_pagar) as DeudaTotal " +
                               " , ep.nombre as 'EncargadoDelPago', ep.celular,ep.telefono " +
                               " from tb_equipo eq, " +
                               " tb_seguimiento seg, tb_detalle_segme sm, " +
                               " tb_proyecto proy  " +
                               " left join tb_encargado_pago ep on (proy.codEncargado = ep.codigo) " +
                               " where  eq.codigo = seg.cod_equipo and " +
                               " eq.cod_proyecto = proy.codigo and " +
                               " sm.codSeg = seg.codigo and " +
                               " proy.nombre = '" + edificio + "' " +
                               " and  eq.exbo like '%" + exbo + "%' " +
                               " and   sm.pago = 'No' " +
                               " and   seg.years = " + anioActual +
                               " and  sm.codMe <= (month(current_date())-1) " +
                               " GROUP by eq.exbo " +
                               " union all " +
                               " select seg.cod_equipo,eq.exbo, proy.nombre, count(*) as MesesAtrazado, " +
                               " sum(sm.monto_pagar) as DeudaTotal " +
                               " , ep.nombre as 'EncargadoDelPago', ep.celular,ep.telefono " +
                               " from tb_equipo eq, " +
                               " tb_seguimiento seg, tb_detalle_segme sm, " +
                               " tb_proyecto proy " +
                               " left join tb_encargado_pago ep on (proy.codEncargado = ep.codigo) " +
                               " where  eq.codigo = seg.cod_equipo and " +
                               " eq.cod_proyecto = proy.codigo and " +
                               " sm.codSeg = seg.codigo and  proy.nombre = '" + edificio + "' and " +
                               " eq.exbo like '%" + exbo + "%' and   sm.pago = 'No' and  seg.years < " + anioActual +
                               " GROUP by eq.exbo " +
                               " ) as t1 " +
                               " GROUP BY t1.exbo " +
                               " having sum(t1.MesesAtrazado) >= " + mesesLimitePermitido;
            return Dsegui.getDatos(consulta);

        }

      /*  public DataSet getTodosEquiposMantenimientoMorososEspecifico(string exbo, string edificio, int meses)
        {
            string consulAux = "select  YEAR(current_date()) ; ";
            DataSet datoResul = Dsegui.getDatos(consulAux);
            int anioActual = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());

            string consulta = "";
            DataSet Vanios = getAllyearSeguimiento();
            for (int i = 0; i < Vanios.Tables[0].Rows.Count; i++)
            {
                int anio = Convert.ToInt32(Vanios.Tables[0].Rows[i][1].ToString());
                if (anio <= anioActual)
                {
                    string consultaAux = "select seg.cod_equipo,det.codSeg, seg.years, eq.exbo, proy.nombre as 'Edificio', count(det.pago) as 'MesesAtrazado',  (sum(det.monto_pagar)-sum(det.monto_pago)) as 'Deuda' " +
                                       " , ep.nombre as 'EncargadoDelPago', ep.celular,ep.telefono " +
                                       " from tb_detalle_segme det, tb_seguimiento seg, tb_equipo eq, " +
                                       " tb_proyecto proy left join tb_encargado_pago ep on (proy.codEncargado = ep.codigo) " +
                                       " where " +
                                       " proy.codigo = eq.cod_proyecto and " +
                                       " eq.codigo = seg.cod_equipo and " +
                                       " eq.exbo like '%" + exbo + "%' and " +
                                       " proy.nombre = '" + edificio + "' and " +
                                       " seg.codigo = det.codSeg and " +
                                       " seg.years = " + anio + " and ";
                    if (anio == anioActual)
                    {
                        consultaAux = consultaAux + " det.pago = 'NO' and det.codMe <= (month(current_date())-1) " +
                                       " group by det.codSeg  having count(det.pago) >= " + meses;
                    }
                    else
                    {
                        consultaAux = consultaAux + " det.pago = 'NO' and det.codMe <= 12 " +
                                                  " group by det.codSeg  having count(det.pago) > 0 ";
                    }
                    consulta = consulta + consultaAux;

                    if (i < Vanios.Tables[0].Rows.Count - 1)
                    {
                        consulta = consulta + " union ";
                    }


                }
            }

            if (consulta.Equals(""))
            {
                return null;
            }
            else
            {
                if (!consulta.Equals(""))
                {
                    consulta = "select t9.cod_equipo, 'Todos' as 'Anios', t9.exbo, t9.Edificio, t9.MesesAtrazado, sum(t9.Deuda) as 'DeudaTotal' " +
                                                   " , t9.EncargadoDelPago, t9.celular, t9.telefono " +
                                                   " from (" + consulta + ") as t9 " +
                                                   " group by t9.cod_equipo";

                }
                return Dsegui.getDatos(consulta);
            }
        }
        */
   
        //----------------- modificado

        public bool tieneDeudasAnterioresGestionesPendientes(string exbo, string nombreProyecto, int anio)
        {
            try
            {

                string consultaAux = "select sum(t1.deuda) "+
                                       " from tb_equipo eq, tb_proyecto proy, "+
                                       " (select  "+
                                       " segme.codSeg,seg.cod_equipo, sum(segme.monto_pagar)-sum(segme.monto_pago) as 'deuda' "+
                                       " from tb_detalle_segme segme, tb_seguimiento seg "+
                                       " where  "+
                                       " segme.codSeg = seg.codigo and "+
                                       " seg.years < "+anio+
                                       " group by segme.codSeg) as t1 "+
                                       " where  "+
                                       " eq.cod_proyecto = proy.codigo and "+
                                       " eq.codigo = t1.cod_equipo and "+
                                       " eq.exbo = '"+exbo+"' and "+
                                       " proy.nombre = '"+nombreProyecto+"' "+
                                       " group by eq.codigo ";

                DataSet datoResul = Dsegui.getDatos(consultaAux);
                float cantidad = Convert.ToSingle(datoResul.Tables[0].Rows[0][0].ToString());
                if (cantidad > 0)
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



        public DataSet getLibroDiarioCobranza(string desde, string hasta, int codcobrador)
        {
            string consulta = "select  " +
                               " DATE_FORMAT(re.fecha,'%d/%m/%Y') as 'fecha',  " +
                               " re.hora,  " +
                               " proy.nombre as 'Edificio',  " +
                               " eq.exbo,  " +
                               " eq.ascensor, " +
                               " ROUND(re.pago,2) as 'pagoDolares',  "+
                               " ROUND(re.pagobs,2) as 'pagoBolivianos', "+
                               " ROUND(re.tipocambio,2) as 'tipocambio', "+
                               " seg.years as 'year',  " +
                               " m.nombre as 'mes',  " +
                               " re.efectivo,  " +
                               " re.deposito,  " +                               
                               " re.nrocheque, " +
                               " re.banco, " +
                               " re.factura, " +
                               " re.detalle, " +
                               " res.nombre as 'Cobrador', " +
                               " '' as Recibido,  " +
                               " '' as Entregado,  " +
                               " re.recibo " +
                               " from tb_mes m, tb_seguimiento seg,   " +
                               " tb_equipo eq, tb_proyecto proy , " +
                               " tb_recibopago re  " +
                               " left join tb_responsable res on (re.codresp = res.codigo)  " +
                               " where " +
                               " proy.codigo = eq.cod_proyecto and " +
                               " eq.codigo = seg.cod_equipo and " +
                               " seg.codigo = re.codseg and " +
                               " re.codmes = m.codigo ";
            if(codcobrador > 0){
                consulta = consulta + " and re.codresp = " + codcobrador;
            }
                               consulta = consulta +" and re.fecha between " + desde + " and " + hasta + " " +
                               " order by TIMESTAMP(re.fecha, re.hora) asc ";
            return Dsegui.getDatos(consulta);
        }
        
     
        /// <summary>
        /// Procedimiento que verifica si es que deudaAnterior lo pone en critico directo,
        /// si no tiene deuda anterior y tiene deuda en el maximo mes lo pone en critico,
        /// si no tiene deuda anterior y esta al dia lo pone en mantenimiento.
        /// </summary>
        /// <param name="codSeguimiento">Codigo de Seguimiento de la gestion seleccionada</param>
        /// <param name="exbo">Codigo del Equipo</param>
        /// <param name="edificio">Nombre del Edificio</param>
        /// <param name="maxMesesRetrazo">MesMaximo de Retrato</param>
        /// <param name="anio">Año de la Gestion del Seguimiento</param>
        /// <returns>Retorna si se realizo algun cambio</returns>
        public bool modificarEstadoMantenimiento_CriticoMantenimiento_MantenimientoCritico(int codSeguimiento, string exbo, string edificio, int maxMesesRetrazo, int CodUserCambio)
        {            
            int codEstadoMantenimiento = getCodigoEstaMantenimiento(codSeguimiento);
            int anioGestionSeg = getYearSeguimiento(codSeguimiento);
            bool tieneDeudaAtrazada = tieneDeudasAnterioresGestionesPendientes(exbo, edificio, anioGestionSeg);

            if (tieneDeudaAtrazada && codEstadoMantenimiento != 2)
            {   // Si tiene deuda Atrazada y no esta con el estado de Critico --> directo Critico
                bool bandera = modificarEstadoMantenimiento(codSeguimiento, 2, CodUserCambio);
                return bandera;
            }
            else {
                DataSet tuplaRes = getTodosEquiposMantenimientoMorososEspecifico(exbo, edificio, maxMesesRetrazo);
                if (tuplaRes.Tables[0].Rows.Count > 0 && tieneDeudaAtrazada == false && codEstadoMantenimiento != 2)
                {   //si tiene deuda acumulada y no tiene deudaAtrazada y es diferente de critico --> colocar critico
                    bool bandera = modificarEstadoMantenimiento(codSeguimiento, 2, CodUserCambio);
                    return bandera;
                }
                else {
                    if (tuplaRes.Tables[0].Rows.Count == 0 && tieneDeudaAtrazada == false && codEstadoMantenimiento != 6)
                    {  //si no tiene deuda mayor al mesMaximo y no tiene deuda Atrazada y el estado diferente de Mantenimiento  --> colocar en Mantenimiento
                        bool bandera = modificarEstadoMantenimiento(codSeguimiento, 6, CodUserCambio);
                        return bandera;
                    }
                    else
                       return false;
                }            
            }

        }

        public bool modificarEstadoMantenimiento(int codSeguimiento, int codEstadoMantenimiento, int CodUser)
        {
            NA_FechaEstadoMan nfechaEstadoMan = new NA_FechaEstadoMan();
            bool bandera = nfechaEstadoMan.insertar(codSeguimiento, codEstadoMantenimiento, CodUser);
            if (bandera)
            {
                int codfechaUltimo = nfechaEstadoMan.ultimoinsertado();
                bool bandera2 = modificarFechaEstadoMan(codSeguimiento, codfechaUltimo);
                if (bandera2)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool getTieneDeudaAnterior(int codSeguimiento, int anioSeg)
        {
            DataSet tuplaSeguimientoConDeudaAnterior = Dsegui.getSeguimientosConDeudaAnterior(codSeguimiento,anioSeg);
            if (tuplaSeguimientoConDeudaAnterior.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public int getYearSeguimiento(int codSeguimiento)
        {
            DataSet tuplas = Dsegui.getDatosSeguimientoMantenimiento(codSeguimiento);
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                int codigo;
                bool bandera = int.TryParse(tuplas.Tables[0].Rows[0][3].ToString(), out codigo);
                if (bandera)
                {
                    return codigo;
                }
                else
                    return -1;
            }
            else
                return -1;
        }

        public int getCodigoEstaMantenimiento(int codSeguimiento)
        {
            DataSet tuplas = Dsegui.getDatosSeguimientoMantenimiento(codSeguimiento);
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                int codigo;
                bool bandera = int.TryParse(tuplas.Tables[0].Rows[0][5].ToString(),out codigo);
                if (bandera)
                {
                    return codigo;
                }
                else
                    return -1;
            }
            else
                return -1;
        }

        //--------------------------------------------------------------------------------------

        internal DataSet getDatosaPagar_duranteTodaslasGestiones(string exbo)
        {
           return Dsegui.getDatosaPagar_duranteTodaslasGestiones(exbo);
        }

        internal float get_TipoUltimoTipoCambio()
        {
            DataSet dato = Dsegui.get_TipoUltimoTipoCambio();
            if (dato.Tables[0].Rows.Count > 0)
            {
                float tc;
                float.TryParse(dato.Tables[0].Rows[0][3].ToString(), out tc);
                return tc;
            }
            else
                return 0;
        }

        internal DataSet get_Monedas()
        {
            DataSet dato = Dsegui.get_moneda();
            return dato;
        }

        public string get_UltimoNumContabilidad_Glosa1(string baseDatos)
        {
            DA_contabilidad cont = new DA_contabilidad();
            string NUMCorrelativo = cont.get_UltimoNumContabilidad_Glosa(baseDatos);
            return NUMCorrelativo;
        }

        internal bool generar_Cobranza(string GLOSA, bool ANULADA, double tipocambio, int codmoneda, int coduser, string nombreUsuario, string clicodigo, string codvendedor, string fechaPago)
        {
         /*  //------------inventario simec------------
            DA_inventario inv = new DA_inventario();
            inv.insertarCobranza(codmoneda, tipocambio, GLOSA, nombreUsuario);
            string DOCUM = inv.getUltimoDocumInsertado();
            //-------------fin inventario ------------
            //----------contavilidad simec
            string _nombreDB = System.Web.HttpContext.Current.Session["BaseDatos"].ToString();
            DA_contabilidad cont = new DA_contabilidad();
            string NUMCorrelativo = cont.getNumCorrelativoContabilidad_Glosa(_nombreDB);
            bool bandera3 = cont.insertarCobranza_Glosa(NUMCorrelativo, GLOSA, "observaciones automatica", nombreUsuario, tipocambio);            
            //------------fin conta     */
            bool bandera = Dsegui.generar_Cobranza( "DOCUM",  GLOSA,  ANULADA,  tipocambio,  codmoneda,  coduser, clicodigo, codvendedor, fechaPago);
            return bandera;
        }

        public bool modificarMotoPago_DolaresBolivianos(int codSeg, int codMe, string DOCUM, string CBTE, double monto_pago, int tipoMoneda, double tipocambio, int coduser, int codcobranza, string GLOSA, bool efectivo, bool deposito, string nrocheque, string banco, string factura, string recibo, string tipoPago, string CLICODIGO, string CODVENDEDOR, int orden, bool debe_bool, string baseDatos, string codigoCuentaBancaria, bool transferencia, string fechaPago)
        { 
            NA_DetalleSeguimiento ndseg = new NA_DetalleSeguimiento();
            bool bandera = ndseg.modificarMotoPago_DolaresBolivianos( codSeg,  codMe,  monto_pago,  tipoMoneda,  tipocambio, coduser) ;            
            bool bandera2 = false;
            if (bandera)
            {
               NA_ReciboPago nrecibo = new NA_ReciboPago();
               bandera2 = nrecibo.insertarReciboMTTO(codSeg, codMe, GLOSA, monto_pago, efectivo, deposito, nrocheque, banco, factura, recibo, coduser, codcobranza, tipocambio, tipoMoneda, transferencia, fechaPago);
             /*  DA_inventario inv = new DA_inventario();
               NA_banco bb = new NA_banco();
               string bancoNombre = bb.get_CodigoCuentaBancaria_banco(banco);

               bool sw =  inv.insertarDetalleCobranza1(DOCUM, CLICODIGO, recibo, bancoNombre, nrocheque, monto_pago, GLOSA, CODVENDEDOR, tipoMoneda, tipocambio, tipoMoneda, factura, tipoPago);
               bool sw2 = inv.insertarDetalleCobranza_CLIKAR(DOCUM, CLICODIGO, recibo, GLOSA, monto_pago, tipoMoneda, CODVENDEDOR, tipoMoneda);
               
               DA_contabilidad cont = new DA_contabilidad();
               bool sw3 = cont.insertCobranza_Conta(CBTE, codigoCuentaBancaria, GLOSA, tipocambio, orden, tipoMoneda, monto_pago, debe_bool, baseDatos);
               */ 
              return bandera2;
            }
            else
                return bandera;            
        }


        internal bool insertCobranza_Conta(string CBTE, string NCTA, string DETALLE, double FACTOR, int ORDEN, int tipoMoneda, double monto_pago, bool Debe_bool, string baseDatos, string fechaPago)
       {
           DA_contabilidad cont = new DA_contabilidad();
           bool sw3 = cont.insertCobranza_Conta(CBTE, NCTA, DETALLE, FACTOR, ORDEN, tipoMoneda, monto_pago, Debe_bool, baseDatos, fechaPago);
           return sw3;
       }


        public int get_ultimocobroIngresado() {
            DataSet dato = Dsegui.get_ultimocobroIngresado();
            if (dato.Tables[0].Rows.Count > 0)
            {
                int codigo;
                int.TryParse(dato.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            }
            else
                return 0;
        }

       
        internal DataSet get_cobranzadeldia(int codigoCobranza)
        {
            DataSet tupla = Dsegui.get_cobranzadeldia(codigoCobranza);
            return tupla;
        }

        internal DataSet get_Recibo_cobranzadeldia(int codigoCobranza)
        {
            DataSet tupla = Dsegui.get_Recibo_cobranzadeldia(codigoCobranza);
            return tupla;
        }

        internal string getDocumUltimoInsertado() { 
            DA_inventario inv = new DA_inventario();            
            string DOCUM = inv.getUltimoDocumInsertado();
            return DOCUM;
        }

        //----------------------21-01-2019---------------------

        public DataSet get_TodosEquiposMantenimientoMorososEspecifico_CallCenter(string exbo, string edificio, int meses)
        {
            string consulAux = "select  YEAR(current_date()) ; ";
            DataSet datoResul = Dsegui.getDatos(consulAux);
            int anioActual = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());

            string consulta = "SELECT t1.exbo, t1.nombre, sum(t1.mesesRetrazados) as 'mesesRetrazo', sum(t1.sumaPago) as 'SumaTotal' "+  
                               " FROM "+
                               " ( "+
                               " select eq.exbo, pp.nombre, count(*) as mesesRetrazados, "+
                               " sum(sm.monto_pagar) as sumaPago  "+
                               " from tb_proyecto pp, tb_equipo eq, "+ 
                               " tb_seguimiento seg, tb_detalle_segme sm  "+
                               " where  eq.codigo = seg.cod_equipo and  "+
                               " eq.cod_proyecto = pp.codigo and  "+
                               " sm.codSeg = seg.codigo and  "+
                               " pp.nombre = '"+edificio+"' "+
                               " and  eq.exbo like '%"+exbo+"%' "+
                               " and   sm.pago = 'No' "+
                               " and   seg.years = "+anioActual+
                               " and  sm.codMe <= (month(current_date())-1) "+ 
                               " GROUP by eq.exbo "+
                               " union all "+
                               " select eq.exbo, pp.nombre, count(*) as mesesRetrazados, "+
                               " sum(sm.monto_pagar) as sumaPago  "+
                               " from tb_proyecto pp, tb_equipo eq, "+
                               " tb_seguimiento seg, tb_detalle_segme sm  "+
                               " where  eq.codigo = seg.cod_equipo and "+ 
                               " eq.cod_proyecto = pp.codigo and  "+
                               " sm.codSeg = seg.codigo and  pp.nombre = '"+edificio+"' and  "+
                               " eq.exbo like '%"+exbo+"%' and   sm.pago = 'No' and  seg.years < "+anioActual +
                               " GROUP by eq.exbo "+
                               " ) as t1 "+
                               " GROUP BY t1.exbo "+
                               " having sum(t1.mesesRetrazados) >= "+meses;
            return Dsegui.getDatos(consulta);
        }


        internal DataSet CobrosGeneralesRecibo()
        {
            return Dsegui.CobrosGeneralesRecibo();
        }

        public string convertidorFecha(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'" + _fecha + "'";
            }
            else
                return "null";
        }

      /*  internal void vaciarAlSistemaSimecporDia(int codMonedaUsuario, float tipoCambioUsuario, string GlosaUsuario, string nombreUsuario, string codVendedorUsuario, string VCAJA, string baseDatos)
        {
           DataSet tuplasfechas = Dsegui.fechasNoVaciadasAlSimec();
            if(tuplasfechas.Tables[0].Rows.Count > 0){
                for (int i = 0; i < tuplasfechas.Tables[0].Rows.Count; i++)
                {
                    string fechaAux = tuplasfechas.Tables[0].Rows[i][0].ToString();
                    string fechaDeterminada = convertidorFecha(fechaAux);                    
                    //-------- Simec Inventario y Contavilidad --------------------------
                    DA_inventario inv = new DA_inventario();
                    string GlosaGeneral_titulo = GlosaUsuario + " " + fechaAux;
                    inv.insertarCobranza(codMonedaUsuario, tipoCambioUsuario, GlosaGeneral_titulo, nombreUsuario);
                    string DOCUM = inv.getUltimoDocumInsertado();
                    string CBTE = get_UltimoNumContabilidad_Glosa(baseDatos);

                    DA_contabilidad cont = new DA_contabilidad();
                    string NUMCorrelativo = cont.getNumCorrelativoContabilidad_Glosa(baseDatos);
                    bool bandera3 = cont.insertarCobranza_Glosa(NUMCorrelativo, GlosaGeneral_titulo, "observaciones automatica", nombreUsuario, tipoCambioUsuario);            
                    //------------------------------------------------------            
                    Dsegui.updateTodosDOCUM_porFecha(fechaDeterminada, DOCUM);
                    DataSet tuplas_TodosCobroReciboporFecha = Dsegui.todosCobrosRecibo_porFecha(DOCUM, fechaDeterminada);
                    int orden = 0;
                    for (int j = 0; j < tuplas_TodosCobroReciboporFecha.Tables[0].Rows.Count; j++)
                    {
                        orden++;
                        int codigoCobro =  int.Parse(tuplas_TodosCobroReciboporFecha.Tables[0].Rows[j][0].ToString());
                       // string fecha = tuplas_TodosCobroReciboporFecha.Tables[0].Rows[j][1].ToString();
                       // string docum = tuplas_TodosCobroReciboporFecha.Tables[0].Rows[j][2].ToString() ;
                       // string glosa = tuplas_TodosCobroReciboporFecha.Tables[0].Rows[j][3].ToString();
                       // string anulada = tuplas_TodosCobroReciboporFecha.Tables[0].Rows[j][4].ToString();
                       // string fechagra = tuplas_TodosCobroReciboporFecha.Tables[0].Rows[j][5].ToString();
                       // string horagra = tuplas_TodosCobroReciboporFecha.Tables[0].Rows[j][6].ToString();
                        float tipocambio =  float.Parse(tuplas_TodosCobroReciboporFecha.Tables[0].Rows[j][7].ToString());
                        string MONEDAnombre = tuplas_TodosCobroReciboporFecha.Tables[0].Rows[j][8].ToString();
                        int coduser = int.Parse(tuplas_TodosCobroReciboporFecha.Tables[0].Rows[j][9].ToString());
                        string CLIENTECOD = tuplas_TodosCobroReciboporFecha.Tables[0].Rows[j][10].ToString();
                        string codvendedor  = tuplas_TodosCobroReciboporFecha.Tables[0].Rows[j][11].ToString();

                        //-----------------simec-------------------
                            DataSet tuplaSumadelCobro = Dsegui.SumaTodoslosRecibosdelCobro(codigoCobro);
                            int codcobranza = int.Parse(tuplaSumadelCobro.Tables[0].Rows[0][0].ToString());
                            string banco = tuplaSumadelCobro.Tables[0].Rows[0][1].ToString();
                            int codmoneda = int.Parse(tuplaSumadelCobro.Tables[0].Rows[0][2].ToString());
                           // float tipocambio = float.Parse(tuplaSumadelCobro.Tables[0].Rows[0][3].ToString());
                            float SumaDolares = float.Parse(tuplaSumadelCobro.Tables[0].Rows[0][4].ToString());
                            float SumaBolivianos = float.Parse(tuplaSumadelCobro.Tables[0].Rows[0][5].ToString());

                            NA_banco bb = new NA_banco();
                            string codigoCuentaBancariaDebe = bb.get_CodigoCuentaBancaria_debe(banco);                            
                            string NombreClienteSimec = inv.getNombreClienteSimec(CLIENTECOD);
                            string DetalleMTTO_cobro = Dsegui.get_detalleMttoCobro(codigoCobro);
                            string GlosaDelosCobrosRecibo = NombreClienteSimec+" MTTO_"+ DetalleMTTO_cobro;    
                            float montoTotal = 0;
                            if(codmoneda == 2){
                                montoTotal = SumaDolares;
                            }else
                                montoTotal = SumaBolivianos;

                            bool debe_bool = true;
                            bool banderaSW2 = insertCobranza_Conta(CBTE, codigoCuentaBancariaDebe, GlosaDelosCobrosRecibo, tipocambio, orden, codmoneda, montoTotal, debe_bool, baseDatos);
                        //-----------------------------------------
                        
                        DataSet tuplas_TodosRecibosdelCobroRealizado =  Dsegui.tuplas_TodosRecibosdelCobroRealizado(codigoCobro);

                        for (int k = 0; k < tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows.Count; k++)
                        {
                            orden++;
                            int codigoRecibo = int.Parse(tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][0].ToString());
                            string detalleRecibo = tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][1].ToString();
                            string fechaRecibo = tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][2].ToString();
                            string horaRecibo = tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][3].ToString();
                            int codsegRecibo = int.Parse(tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][4].ToString());
                            int codmesRecibo = int.Parse(tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][5].ToString());
                            int codrespRecibo = int.Parse(tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][6].ToString());
                            bool efectivoRecibo = bool.Parse(tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][7].ToString());
                            bool depositoRecibo = bool.Parse(tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][8].ToString());
                            string nrochequeRecibo = tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][9].ToString();
                            string bancoRecibo = tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][10].ToString();
                            string reciboRecibo = tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][11].ToString();
                            string facturaRecibo = tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][12].ToString();
                            int codcobranzaRecibo = int.Parse(tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][13].ToString());
                            int codmonedaRecibo = int.Parse(tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][14].ToString());
                            float tipocambioRecibo = float.Parse(tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][15].ToString());
                            float pagoSusRecibo = float.Parse(tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][16].ToString());
                            float pagobsRecibo = float.Parse(tuplas_TodosRecibosdelCobroRealizado.Tables[0].Rows[k][17].ToString());
                           //-------------------------SIMEC---------------------------------------                             
                           string GLOSA_Recibo = detalleRecibo;
                           string TipoPago = "";
                           if (efectivoRecibo == true)
                               TipoPago = "EF";
                           if (depositoRecibo == true)
                               TipoPago = "CH";

                           float montoRecibo = 0;
                           if (codmonedaRecibo == 2)
                           {
                               montoRecibo = pagoSusRecibo;
                           }
                           else
                               montoRecibo = pagobsRecibo;

                           NA_banco nbancoRecibo = new NA_banco();
                           string NombreBancoRecibo = nbancoRecibo.get_NombreBanco_CuentaBancaria_banco(bancoRecibo);

                           bool sw = inv.insertarDetalleCobranza1(DOCUM, CLIENTECOD, reciboRecibo, NombreBancoRecibo, nrochequeRecibo, montoRecibo, GLOSA_Recibo, codvendedor, codmonedaRecibo, tipocambioRecibo, codmoneda, facturaRecibo, TipoPago, VCAJA);
                           bool sw2 = inv.insertarDetalleCobranza_CLIKAR(DOCUM, CLIENTECOD, reciboRecibo, GLOSA_Recibo, montoRecibo, codmonedaRecibo, codvendedor, codmonedaRecibo, VCAJA);

                           
                           string codigoCuentaBancariaHaber = bb.get_CodigoCuentaBancaria_haber(banco);
                           bool debe_bool_Haber = false;                                                 
                           bool sw3 = cont.insertCobranza_Conta(CBTE, codigoCuentaBancariaHaber, GLOSA_Recibo, tipocambioRecibo, orden, codmonedaRecibo, montoRecibo, debe_bool_Haber, baseDatos);
                           //----------------------------------------------------------------------
                        }  
                            
                    }
                }
               
            }

        }

        */

      
     /*   internal void vaciarAlSistemaSimecporDia(int codMonedaUsuario, float tipoCambioUsuario, string GlosaUsuario, string nombreUsuario,  string VCAJA, string baseDatos)
        {
            DataSet tuplasfechas = Dsegui.fechasNoVaciadasAlSimec();
            if (tuplasfechas.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < tuplasfechas.Tables[0].Rows.Count; i++)
                {
                    string fechaAux = tuplasfechas.Tables[0].Rows[i][0].ToString();
                    string fechaDeterminada = convertidorFecha(fechaAux);
                    //-------- Simec Inventario y Contavilidad General---------------------
                    DA_inventario inv = new DA_inventario();
                    string GlosaGeneral_titulo = GlosaUsuario + " " + fechaAux;
                    inv.insertarCobranza(codMonedaUsuario, tipoCambioUsuario, GlosaGeneral_titulo, nombreUsuario);
                    string DOCUM = inv.getUltimoDocumInsertado();
                 //  string CBTE = get_UltimoNumContabilidad_Glosa(baseDatos);

                    DA_contabilidad cont = new DA_contabilidad();
                    string CBTE_Correlativo = cont.getNumCorrelativoContabilidad_Glosa(baseDatos);
                    bool bandera3 = cont.insertarCobranza_Glosa(CBTE_Correlativo, GlosaGeneral_titulo, "observaciones automatica", nombreUsuario, tipoCambioUsuario);
                    //------------------------------------------------------            
                    Dsegui.updateTodosDOCUM_porFecha(fechaDeterminada, DOCUM);
                    DataSet tupla_CobrosGeneralesRecibo_porDocumPorFecha = Dsegui.CobrosGeneralesRecibo_porDocumPorFecha(DOCUM, fechaDeterminada);
                    int orden = 0;
                        for (int k = 0; k < tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows.Count; k++)
                        {
                            orden++;
                            int codigoCobro = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][0].ToString());
                            string GlosaCobro = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][1].ToString();
                            int coduserCobro = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][2].ToString());
                            string CLIENTECOD = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][3].ToString();
                            string codvendedor = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][4].ToString();                            
                            int codigoRecibo = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][5].ToString());
                            string detalleRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][6].ToString();
                            string fechaRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][7].ToString();
                            string horaRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][8].ToString();
                            int codsegRecibo = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][9].ToString());
                            int codmesRecibo = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][10].ToString());
                            int codrespRecibo = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][11].ToString());
                            bool efectivoRecibo = bool.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][12].ToString());
                            bool depositoRecibo = bool.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][13].ToString());
                            string nrochequeRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][14].ToString();
                            string bancoRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][15].ToString();
                            string reciboRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][16].ToString();
                            string facturaRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][17].ToString();
                            int codcobranzaRecibo = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][18].ToString());
                            int codmonedaRecibo = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][19].ToString());
                            float tipocambioRecibo = float.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][20].ToString());
                            float pagoSusRecibo = float.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][21].ToString());
                            float pagobsRecibo = float.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][22].ToString());
                            string anioPagoRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][23].ToString();
                            bool transferencia = bool.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][24].ToString());
                            //-------------------------SIMEC---------------------------------------  
                            int MonedaCliente = inv.getMonedaClienteSimecInv(CLIENTECOD);
                           
                            string TipoPago = "";
                            if (efectivoRecibo == true)
                                TipoPago = "EF";

                            if (depositoRecibo == true)
                                TipoPago = "CH";

                            if (transferencia == true)
                                TipoPago = "TF";

                            float montoReciboINV = 0;
                            if (MonedaCliente == 2)
                            {
                                montoReciboINV = pagoSusRecibo;
                            }
                            else
                                montoReciboINV = pagobsRecibo;


                            float montoRecibo = 0;
                            if (codmonedaRecibo == 2)
                            {
                                montoRecibo = pagoSusRecibo;
                            }
                            else
                                montoRecibo = pagobsRecibo;

                            NA_banco nbancoRecibo = new NA_banco();
                            string NombreBancoRecibo = nbancoRecibo.get_NombreBanco_CuentaBancaria_banco(bancoRecibo);
                            string NombreClienteSimec = inv.getNombreClienteSimec(CLIENTECOD);                            

                            string GLOSA_INVENTARIO = " MTTO (" + codmesRecibo + "/" + anioPagoRecibo + ") FAC"+facturaRecibo;
                            string GLOSA_CONTABILIDAD = NombreClienteSimec + " MTTO (" + codmesRecibo + "/" + anioPagoRecibo + ") FAC" + facturaRecibo;

                            bool sw = inv.insertarDetalleCobranza1(DOCUM, CLIENTECOD, reciboRecibo, NombreBancoRecibo, nrochequeRecibo, montoReciboINV, GLOSA_INVENTARIO, codvendedor, MonedaCliente, tipocambioRecibo, codmonedaRecibo, facturaRecibo, TipoPago, VCAJA);
                            bool sw2 = inv.insertarDetalleCobranza_CLIKAR(DOCUM, CLIENTECOD, reciboRecibo, GLOSA_INVENTARIO, montoReciboINV, codmonedaRecibo, codvendedor, codmonedaRecibo, VCAJA);

                            string codigoCuentaBancariaDebe = nbancoRecibo.get_CodigoCuentaBancaria_Debe(bancoRecibo);
                            bool debe_bool = true;
                            bool sw3 = cont.insertCobranza_Conta(CBTE_Correlativo, codigoCuentaBancariaDebe, GLOSA_CONTABILIDAD, tipocambioRecibo, orden, codmonedaRecibo, montoRecibo, debe_bool, baseDatos);
                            
                            orden++;
                            string codigoCuentaBancariaHaber = nbancoRecibo.get_CodigoCuentaBancaria_Haber(bancoRecibo);
                            debe_bool = false;
                            bool sw4 = cont.insertCobranza_Conta(CBTE_Correlativo, codigoCuentaBancariaHaber, GLOSA_CONTABILIDAD, tipocambioRecibo, orden, codmonedaRecibo, montoRecibo, debe_bool, baseDatos);
                            //----------------------------------------------------------------------
                        }

                    }
                
            }

        }    */



        //----------------------------------nuevo-----------------------


        internal void vaciadoportipodeMonedaClienteSimec(string GlosaUsuario, string fechaDeterminada, string fechaAux, int codMonedaUsuario, float tipoCambioUsuario, string nombreUsuario, string VCAJA, string baseDatos)
        {
            //-------- Simec Inventario y Contavilidad General---------------------
            DA_inventario inv = new DA_inventario();
            string GlosaGeneral_titulo = GlosaUsuario + " " + fechaAux;
            inv.insertarCobranza(codMonedaUsuario, tipoCambioUsuario, GlosaGeneral_titulo, nombreUsuario, fechaDeterminada);
            string DOCUM = inv.getUltimoDocumInsertado();            

            DA_contabilidad cont = new DA_contabilidad();
            string CBTE_Correlativo = cont.getNumCorrelativoContabilidad_Glosa(baseDatos, fechaDeterminada);
            bool bandera3 = cont.insertarCobranza_Glosa(CBTE_Correlativo, GlosaGeneral_titulo, "observaciones automatica", nombreUsuario, tipoCambioUsuario, fechaDeterminada);
            //------------------------------------------------------   
         
            Dsegui.updateTodosDOCUM_porFecha2(fechaDeterminada, DOCUM, codMonedaUsuario);
            //saca los que ya estan marcados
            DataSet tupla_CobrosGeneralesRecibo_porDocumPorFecha = Dsegui.CobrosGeneralesRecibo_porDocumPorFecha(DOCUM, fechaDeterminada);
            int orden = 0;
            for (int k = 0; k < tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows.Count; k++)
            {
                orden++;
                int codigoCobro = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][0].ToString());
                string GlosaCobro = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][1].ToString();
                int coduserCobro = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][2].ToString());
                //string CLIENTECOD = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][3].ToString();
                string codvendedor = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][4].ToString();
                int codigoRecibo = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][5].ToString());
                string detalleRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][6].ToString();
                string fechaRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][7].ToString();
                string horaRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][8].ToString();
                int codsegRecibo = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][9].ToString());
                int codmesRecibo = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][10].ToString());
                int codrespRecibo = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][11].ToString());
                bool efectivoRecibo = bool.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][12].ToString());
                bool depositoRecibo = bool.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][13].ToString());
                string nrochequeRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][14].ToString();
                string bancoRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][15].ToString();
                string reciboRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][16].ToString();
                string facturaRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][17].ToString();
                int codcobranzaRecibo = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][18].ToString());
                int codmonedaRecibo = int.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][19].ToString());
                float tipocambioRecibo = float.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][20].ToString());
                float pagoSusRecibo = float.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][21].ToString());
                float pagobsRecibo = float.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][22].ToString());
                string anioPagoRecibo = tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][23].ToString();
                bool transferencia = bool.Parse(tupla_CobrosGeneralesRecibo_porDocumPorFecha.Tables[0].Rows[k][24].ToString());


                NEquipo neq = new NEquipo();
                string CLIENTECOD = neq.getClienteCodSimec(codigoCobro);
                
                //-------------------------SIMEC---------------------------------------  
                //  int MonedaCliente = inv.getMonedaClienteSimecInv(CLIENTECOD);
                //----------------Nombre Cliente Simec--------------              
                int index = GlosaCobro.IndexOf(' ', 0, GlosaCobro.Length);
                string nombreClienteBorrar = GlosaCobro.Substring(0, index);
                string restoGlosa = GlosaCobro.Remove(0, nombreClienteBorrar.Length);
                string NombreClienteSimec = inv.getNombreClienteSimec(CLIENTECOD);
                string GlosaNueva = NombreClienteSimec + restoGlosa;
                GlosaCobro = GlosaNueva;

                int index2 = detalleRecibo.IndexOf(' ', 0, detalleRecibo.Length);
                nombreClienteBorrar = detalleRecibo.Substring(0, index2);
                restoGlosa = detalleRecibo.Remove(0, nombreClienteBorrar.Length);
                NombreClienteSimec = inv.getNombreClienteSimec(CLIENTECOD);
                GlosaNueva = NombreClienteSimec + restoGlosa;
                detalleRecibo = GlosaNueva;
                //--------------------------------------------------


                string TipoPago = "";
                if (efectivoRecibo == true)
                    TipoPago = "EF";

                if (depositoRecibo == true)
                    TipoPago = "CH";

                if (transferencia == true)
                    TipoPago = "TF";

                float montoReciboINV = 0;
                if (codMonedaUsuario == 2)
                {
                    montoReciboINV = pagoSusRecibo;
                }
                else
                    montoReciboINV = pagobsRecibo;


                float montoRecibo = 0;
                if (codmonedaRecibo == 2)
                {
                    montoRecibo = pagoSusRecibo;
                }
                else
                    montoRecibo = pagobsRecibo;

                NA_banco nbancoRecibo = new NA_banco();
                string NombreBancoRecibo = nbancoRecibo.get_NombreBanco_CuentaBancaria_banco(bancoRecibo);
              //  string NombreClienteSimec = inv.getNombreClienteSimec(CLIENTECOD);

                string GLOSA_INVENTARIO = "MTTO (" + codmesRecibo + "/" + anioPagoRecibo + ") FAC" + facturaRecibo;
                string GLOSA_CONTABILIDAD =  NombreClienteSimec + " MTTO (" + codmesRecibo + "/" + anioPagoRecibo + ") FAC" + facturaRecibo;

                bool sw = inv.insertarDetalleCobranza1(DOCUM, CLIENTECOD, reciboRecibo, NombreBancoRecibo, nrochequeRecibo, montoReciboINV, GLOSA_INVENTARIO, codvendedor, codMonedaUsuario, tipocambioRecibo, codMonedaUsuario, facturaRecibo, TipoPago, VCAJA, fechaDeterminada);
                bool sw2 = inv.insertarDetalleCobranza_CLIKAR(DOCUM, CLIENTECOD, reciboRecibo, GLOSA_INVENTARIO, montoReciboINV, codMonedaUsuario, codvendedor, VCAJA, fechaDeterminada);

                string codigoCuentaBancariaDebe = nbancoRecibo.get_CodigoCuentaBancaria_Debe(bancoRecibo);
                bool debe_bool = true;
                bool sw3 = cont.insertCobranza_Conta(CBTE_Correlativo, codigoCuentaBancariaDebe, GLOSA_CONTABILIDAD, tipocambioRecibo, orden, codmonedaRecibo, montoRecibo, debe_bool, baseDatos, fechaDeterminada);

                orden++;
                string codigoCuentaBancariaHaber = nbancoRecibo.get_CodigoCuentaBancaria_Haber(bancoRecibo);
                debe_bool = false;
                bool sw4 = cont.insertCobranza_Conta(CBTE_Correlativo, codigoCuentaBancariaHaber, GLOSA_CONTABILIDAD, tipocambioRecibo, orden, codmonedaRecibo, montoRecibo, debe_bool, baseDatos, fechaDeterminada);
                //----------------------------------------------------------------------
            }
       
        }




        internal void vaciarAlSistemaSimecporDia(float tipoCambioUsuario, string GlosaUsuario, string nombreUsuario, string VCAJA, string baseDatos)
        {
            DataSet tuplasfechas = Dsegui.fechasNoVaciadasAlSimec();
            if (tuplasfechas.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < tuplasfechas.Tables[0].Rows.Count; i++)
                {
                    string fechaAux = tuplasfechas.Tables[0].Rows[i][0].ToString();
                    string fechaDeterminada = convertidorFecha(fechaAux);
                    int codmonedaClienteBolivianos = 1;
                    int codmonedaClienteDolares = 2;                    

                    bool ClienteDolares = Dsegui.get_hayCobranzadeCliente_conLaMonedaDeterminada(fechaDeterminada, codmonedaClienteDolares);
                    if(ClienteDolares == true){
                       string GlosaUsuario1 = GlosaUsuario + " Dolares";
                       vaciadoportipodeMonedaClienteSimec(GlosaUsuario1, fechaDeterminada, fechaAux, codmonedaClienteDolares, tipoCambioUsuario, nombreUsuario, VCAJA, baseDatos);
                    }

                    bool ClienteBolivianos = Dsegui.get_hayCobranzadeCliente_conLaMonedaDeterminada(fechaDeterminada, codmonedaClienteBolivianos);
                    if (ClienteBolivianos == true)
                    {
                       string GlosaUsuario2 = GlosaUsuario + " Bolivianos";
                       vaciadoportipodeMonedaClienteSimec(GlosaUsuario2, fechaDeterminada, fechaAux, codmonedaClienteBolivianos, tipoCambioUsuario, nombreUsuario, VCAJA, baseDatos);
                    }
                    
                }

            }

        }  


        //---------------------------akimodificado------------------------------------
        internal bool vaciadoSimecR148_JYCIA(int CodigoPlanPago, float VTC, string VUSUARIO, string VENCODIGO, string BaseDeDatos)
        {
            DataSet DatosR148 = get_R148(CodigoPlanPago);
            string VFECHA = convertidorFecha(DatosR148.Tables[0].Rows[0][12].ToString());	
            NA_VariablesGlobales nv = new NA_VariablesGlobales();
            int VCAJA = nv.get_VCAJAenbasedeDatosActual(BaseDeDatos);
            string NO_CUENTA = nv.get_VNO_CUENTA_enbasedeDatosActual(BaseDeDatos);

            string CLICODIGO = 	DatosR148.Tables[0].Rows[0][22].ToString();
            float TOTALFAC = float.Parse(DatosR148.Tables[0].Rows[0][10].ToString());
            N_numLetra nletra = new N_numLetra();
            string TOTALLIT = nletra.Convertir(TOTALFAC.ToString(), false, "Dolares");


            DA_inventario ninv = new DA_inventario();
            string VDOCUM = ninv.get_correlativoDocumINV_JYCIA(VCAJA);
            string VDOCUMA = ninv.get_correlativoDocuMAINV_JYCIA(VCAJA);
            DataSet tuplaCliente = ninv.get_datosClienteInv_JYCIA(CLICODIGO);
            string VNOMBRE = tuplaCliente.Tables[0].Rows[0][1].ToString();
            string VRUC = tuplaCliente.Tables[0].Rows[0][5].ToString();
            string VTRANS = "P";
            string VMONEDA = "2";
            string AGECODIGO = "Z30";
            int VTIPRE = 1;
            string VGLOSA = VNOMBRE + " SALDO INICIAL ACUMULADO";
            int VDIAS = 30;
            DateTime fecha = DateTime.Now;
            fecha.AddDays(VDIAS);
            string fechaSumada = fecha.ToString("yyyy/MM/dd");
            string VENCIMIENTO = fechaSumada;
            string XENTREGAR = "N";

            bool bandera1 = ninv.insert_PlanPagoInv_Ventas_JYCIA(VDOCUM, VDOCUMA, VFECHA,
                                 VNOMBRE, VRUC, VCAJA,
                                 VTRANS, VTC, VMONEDA,
                                 VUSUARIO, AGECODIGO, CLICODIGO,
                                 VENCODIGO, VTIPRE, VGLOSA,
                                 VDIAS, VENCIMIENTO, TOTALFAC,
                                 TOTALLIT, XENTREGAR, NO_CUENTA);

            string CODIGO = "Z11";
            //int VCANTIDAD = int.Parse(DatosR148.Tables[0].Rows[0][4].ToString());
            int VCANTIDAD = 1;
            float VPREUNIL = TOTALFAC;
            float VPREUNI = TOTALFAC;
            string ExbosTotalEquipos = get_exbosTotalEquiposR148(CodigoPlanPago);
            string CLOTE = ExbosTotalEquipos;
            if(CLOTE.Length > 12){
            CLOTE = ExbosTotalEquipos.Substring(0,12);
            }

            
            //string CLOTE = "12Char";
            float TOTAL = TOTALFAC;

            bool bandera2 = ninv.insert_PlanPagoInv_Ventas1_JYCIA( VDOCUM,  VDOCUMA,  AGECODIGO,  CLICODIGO,
                                                    VENCODIGO,  VFECHA,  VTC,  VCAJA,
                                                    VMONEDA,  CODIGO,  VCANTIDAD,  VPREUNIL,
                                                    VPREUNI,  VTIPRE,  XENTREGAR,  CLOTE,
                                                    VTRANS,  TOTAL);

            
           DataSet datosCuotas = get_cuotasPlanPagosR148(CodigoPlanPago);

           for (int i = 0; i < datosCuotas.Tables[0].Rows.Count; i++ )
           {   
               string TABLA = "ventas";
               string NCTA =tuplaCliente.Tables[0].Rows[0][15].ToString();
               string DOCUM = VDOCUMA;
               string CAJA = VCAJA.ToString();                                                      
               string CODIGOP = CLICODIGO;
               string MONEDA = VMONEDA;               
               string FECHA = convertidorFecha(datosCuotas.Tables[0].Rows[i][7].ToString());
               string IMPORTE = datosCuotas.Tables[0].Rows[i][4].ToString(); 
               string SALDO = "0";
               int NO_CUOTA = i + 1;
               string CUOTA = "0";
               string GLOSA = datosCuotas.Tables[0].Rows[i][3].ToString();
               if(GLOSA.Length > 50){
               GLOSA = datosCuotas.Tables[0].Rows[i][3].ToString().Substring(0,50);
               }

               //string GLOSA = "char(50) cuota "+NO_CUOTA;

               bool banderaAux = ninv.insert_PlanPagoInv_PlanPagos_JYCIA( TABLA, NCTA, DOCUM, CAJA,
                                                        CODIGOP, MONEDA, NO_CUOTA.ToString(), FECHA,
                                                        IMPORTE,  SALDO,  CUOTA, GLOSA);
           }


            //--------------------Contavilidad------------------------
           //---------traspaso
           DA_contabilidad cont = new DA_contabilidad();
           string CBTE_Correlativo2 = cont.getNumCorrelativoContabilidad_Glosa_JYC(BaseDeDatos, "current_date()");
           DataSet tuplas = get_exbosTotalEquiposR148_2(CodigoPlanPago);
           string GlosaGeneral_titulo = VNOMBRE+" "+ tuplas.Tables[0].Rows[0][1].ToString() + " ASC ORONA XBO=" + tuplas.Tables[0].Rows[0][0].ToString() + " PAR=" + tuplas.Tables[0].Rows[0][2].ToString() + " PER= " + tuplas.Tables[0].Rows[0][3].ToString();
           bool bandera3 = cont.insertarCobranza_Glosa_JYC(CBTE_Correlativo2, GlosaGeneral_titulo, "observaciones automatica", VUSUARIO, VTC, "current_date()");

           string DETALLE = GlosaGeneral_titulo;
           double monto_pago = TOTALFAC;
           double FACTOR = VTC;
           int ORDEN = 1000;
           int tipoMoneda = 2;
           bool Debe_bool = false;
           string fechaPago = "current_date()";
           string FINANCIA = CLICODIGO;
           string ACTIVIDAD = "C3";
           //string NCTA1 = nv.get_VNO_CUENTA_enbasedeDatosActual_Debe(BaseDeDatos);
           string NCTA1 = NO_CUENTA;
           Debe_bool = true;
           bool sw3 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo2, NCTA1, DETALLE, FACTOR, ORDEN, tipoMoneda, monto_pago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
           Debe_bool = false;
           NCTA1 = nv.get_VNO_CUENTA_enbasedeDatosActual_Debe(BaseDeDatos);
           ORDEN = ORDEN + 1;
           bool sw4 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo2, NCTA1, DETALLE, FACTOR, ORDEN, tipoMoneda, monto_pago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);            
           //-------fin de traspaso 
            
           //------comisiones 
           string CBTE_Correlativo = cont.getNumCorrelativoContabilidad_Glosa_JYC(BaseDeDatos, "current_date()");
           tuplas = get_exbosTotalEquiposR148_2(CodigoPlanPago);
           GlosaGeneral_titulo = GlosaGeneral_titulo+" (COMISIONES)" ;
           bool bandera4 = cont.insertarCobranza_Glosa_JYC(CBTE_Correlativo, GlosaGeneral_titulo, "observaciones automatica", VUSUARIO, VTC, "current_date()");

           ORDEN = 1000;
           DataSet tuplasEquipos = get_EquiposR148_2(CodigoPlanPago); 
            for(int i = 0; i < tuplasEquipos.Tables[0].Rows.Count ; i++){
                DETALLE = "INSTALACION PREV 1ASC ORONA XBO=" + tuplasEquipos.Tables[0].Rows[i][0].ToString() + " PAR=" + tuplasEquipos.Tables[0].Rows[i][1].ToString() + " PER= " + tuplasEquipos.Tables[0].Rows[i][2].ToString();
                int paradas = int.Parse(tuplasEquipos.Tables[0].Rows[i][1].ToString());
                monto_pago = nv.get_MontoEstablecido_enbaseaparadas(paradas);
                FACTOR = VTC;
                ORDEN = ORDEN + 1;
                tipoMoneda = 2;
                Debe_bool = false; 
                fechaPago = "current_date()";
                FINANCIA = CLICODIGO;
                ACTIVIDAD = "C3";
               // string  NCTA2 = "2127*";
                string NCTA2 = "3331001*"; 
                Debe_bool = true;
                bool sw6 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, DETALLE, FACTOR, ORDEN, tipoMoneda, monto_pago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
                Debe_bool = false;
                NCTA2 = "2127*";
                ORDEN = ORDEN + 1;
                bool sw5 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, DETALLE, FACTOR, ORDEN, tipoMoneda, monto_pago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
            }
            //-------------------------comision Personal------------------------------
            string resp_une = DatosR148.Tables[0].Rows[0][15].ToString();
            string resp_nacional = DatosR148.Tables[0].Rows[0][16].ToString();
            string resp_gun = DatosR148.Tables[0].Rows[0][17].ToString();
            string resp_jpr = DatosR148.Tables[0].Rows[0][18].ToString();
            string resp_fpr = DatosR148.Tables[0].Rows[0][19].ToString();
            string resp_flo = DatosR148.Tables[0].Rows[0][20].ToString();

            string datoRellenarDetalle = " PREV " + tuplas.Tables[0].Rows[0][1].ToString() + " ASC ORONA XBO=" + tuplas.Tables[0].Rows[0][0].ToString() + " PAR=" + tuplas.Tables[0].Rows[0][2].ToString() + " PER= " + tuplas.Tables[0].Rows[0][3].ToString();
            FACTOR = VTC;
            tipoMoneda = 2;
            FINANCIA = CLICODIGO;
            ACTIVIDAD = "C3";
            fechaPago = "current_date()";
            int CantidadEquipos = int.Parse(DatosR148.Tables[0].Rows[0][4].ToString()); 

            if (!resp_une.Equals(""))
            {
                ORDEN = ORDEN + 1;
                string nombreRespSimec = nv.get_NombreResponsableSimec(resp_une);
                string detalleComision = nombreRespSimec + datoRellenarDetalle;
                double montoPago = nv.get_montoComisionResponsable(resp_une);
                monto_pago = monto_pago * CantidadEquipos;
                string NCTA2 = "3331002*";
                Debe_bool = true;
                bool sw6 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, detalleComision, FACTOR, ORDEN, tipoMoneda, montoPago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
                Debe_bool = false;
                NCTA2 = "2127*";
                ORDEN = ORDEN + 1;
                bool sw5 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, detalleComision, FACTOR, ORDEN, tipoMoneda, montoPago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
            }
                if (!resp_nacional.Equals(""))
                {
                    ORDEN = ORDEN + 1;
                    string nombreRespSimec = nv.get_NombreResponsableSimec(resp_nacional);
                    string detalleComision = nombreRespSimec + datoRellenarDetalle;
                    double montoPago = nv.get_montoComisionResponsable(resp_nacional);
                    monto_pago = monto_pago * CantidadEquipos;
                    string NCTA2 = "3331002*";
                    Debe_bool = true;
                    bool sw6 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, detalleComision, FACTOR, ORDEN, tipoMoneda, montoPago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
                    Debe_bool = false;
                    NCTA2 = "2127*";
                    ORDEN = ORDEN + 1;
                    bool sw5 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, detalleComision, FACTOR, ORDEN, tipoMoneda, montoPago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
                }
                
                    if (!resp_gun.Equals(""))
                    {
                        ORDEN = ORDEN + 1;
                        string nombreRespSimec = nv.get_NombreResponsableSimec(resp_gun);
                        string detalleComision = nombreRespSimec + datoRellenarDetalle;
                        double montoPago = nv.get_montoComisionResponsable(resp_gun);
                        monto_pago = monto_pago * CantidadEquipos;
                        string NCTA2 = "3331002*";
                        Debe_bool = true;
                        bool sw6 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, detalleComision, FACTOR, ORDEN, tipoMoneda, montoPago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
                        Debe_bool = false;
                        NCTA2 = "2127*";
                        ORDEN = ORDEN + 1;
                        bool sw5 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, detalleComision, FACTOR, ORDEN, tipoMoneda, montoPago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
                    }
                    
                        if (!resp_jpr.Equals(""))
                        {
                            ORDEN = ORDEN + 1;
                            string nombreRespSimec = nv.get_NombreResponsableSimec(resp_jpr);
                            string detalleComision = nombreRespSimec + datoRellenarDetalle;
                            double montoPago = nv.get_montoComisionResponsable(resp_jpr);
                            monto_pago = monto_pago * CantidadEquipos;
                            string NCTA2 = "3331004*";
                            Debe_bool = true;
                            bool sw6 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, detalleComision, FACTOR, ORDEN, tipoMoneda, montoPago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
                            Debe_bool = false;
                            NCTA2 = "2127*";
                            ORDEN = ORDEN + 1;
                            bool sw5 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, detalleComision, FACTOR, ORDEN, tipoMoneda, montoPago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
                        }
                        
                            if (!resp_fpr.Equals(""))
                            {
                                ORDEN = ORDEN + 1;
                                string nombreRespSimec = nv.get_NombreResponsableSimec(resp_fpr);
                                string detalleComision = nombreRespSimec + datoRellenarDetalle;
                                double montoPago = nv.get_montoComisionResponsable(resp_fpr);
                                monto_pago = monto_pago * CantidadEquipos;
                                string NCTA2 = "3331003*";
                                Debe_bool = true;
                                bool sw6 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, detalleComision, FACTOR, ORDEN, tipoMoneda, montoPago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
                                Debe_bool = false;
                                NCTA2 = "2127*";
                                ORDEN = ORDEN + 1;
                                bool sw5 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, detalleComision, FACTOR, ORDEN, tipoMoneda, montoPago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
                            }
                            
                                if (!resp_flo.Equals(""))
                                {
                                    ORDEN = ORDEN + 1;
                                    string nombreRespSimec = nv.get_NombreResponsableSimec(resp_flo);
                                    string detalleComision = nombreRespSimec + datoRellenarDetalle;
                                    double montoPago = nv.get_montoComisionResponsable(resp_flo);
                                    monto_pago = monto_pago * CantidadEquipos;
                                    string NCTA2 = "3331005*";
                                    Debe_bool = true;
                                    bool sw6 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, detalleComision, FACTOR, ORDEN, tipoMoneda, montoPago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
                                    Debe_bool = false;
                                    NCTA2 = "2127*";
                                    ORDEN = ORDEN + 1;
                                    bool sw5 = cont.insertCobranza_Conta_JYC(CBTE_Correlativo, NCTA2, detalleComision, FACTOR, ORDEN, tipoMoneda, montoPago, Debe_bool, BaseDeDatos, fechaPago, FINANCIA, ACTIVIDAD);
                                }
            
            //-------------------------fin comision Personal------------------------------
            
           if (bandera1 == true && bandera2 == true)
           {
               return true;
           }
           else
               return false;
        
        }

        private string get_exbosTotalEquiposR148(int CodigoPlanPago)
        {
            DataSet datos = Dsegui.get_exbosTotalEquiposR148(CodigoPlanPago);
            if (datos.Tables[0].Rows.Count > 0)
            {
                string exbosTotal = datos.Tables[0].Rows[0][0].ToString();
                return exbosTotal;
            }
            else
                return "Ninguno";

        }

        private DataSet get_exbosTotalEquiposR148_2(int CodigoPlanPago)
        {
            DataSet datos = Dsegui.get_exbosTotalEquiposR148(CodigoPlanPago);
            return datos;
        }

        private DataSet get_EquiposR148_2(int CodigoPlanPago)
        {
            DataSet datos = Dsegui.get_EquiposR148_2(CodigoPlanPago);
            return datos;
        }


        private DataSet get_cuotasPlanPagosR148(int CodigoPlanPago)
        {
           return Dsegui.get_cuotasPlanPagosR148(CodigoPlanPago);
        }

      

        //---------------------------------------------------------------


        internal bool anularCobroReciboGeneral(int CodigoCobroRecibo, bool anulado)
        {
            return Dsegui.anularCobroReciboGeneral(CodigoCobroRecibo, anulado);
        }

        public DataSet getAllEncuestaMantenimientoRealizas() {
            return Dsegui.getAllEncuestaMantenimientoRealizas();
        }

        public DataSet getBoletaSolaNro(string nroBoleta) { 
            return Dsegui.getBoletaSolaNro(nroBoleta);
        }

        public DataSet getBoletaEmergencia(string nroBoleta) { 
            return Dsegui.getBoletaEmergencia(nroBoleta);
        }


        internal float getmontoAdeudado(int codigoEquipo)
        {
            DataSet dato = Dsegui.getmontoAdeudado_montoReferencia(codigoEquipo);
            if (dato.Tables[0].Rows.Count > 0)
            {
                float deuda;
                float.TryParse(dato.Tables[0].Rows[0][1].ToString(), out deuda);
                return deuda;
            }
            else
                return 0;
        }

        internal float getmontoReferencia(int codigoEquipo)
        {
            DataSet dato = Dsegui.getmontoAdeudado_montoReferencia(codigoEquipo);
            if (dato.Tables[0].Rows.Count > 0)
            {
                float deuda;
                float.TryParse(dato.Tables[0].Rows[0][2].ToString(), out deuda);
                return deuda;
            }
            else
                return 0;
        }

        internal float get_montoReferencia(int codigoEquipo)
        {
            DataSet dato = Dsegui.get_montoReferencia(codigoEquipo);
            if (dato.Tables[0].Rows.Count > 0)
            {
                float deuda;
                float.TryParse(dato.Tables[0].Rows[0][1].ToString(), out deuda);
                return deuda;
            }
            else
                return 0;
        
        }

        public DataSet getBoletasMantenimientoPreventivo(string edificio, string fechadesde, string fechahasta) { 
            return Dsegui.getBoletasMantenimientoPreventivo( edificio,  fechadesde,  fechahasta);
        }

        public DataSet getBoletasMantenimientoEmergenciayOtros(string edificio, string fechadesde, string fechahasta)
        { 
            return Dsegui.getBoletasMantenimientoEmergenciayOtros( edificio,  fechadesde,  fechahasta);
        }


        internal DataSet buscar_R148(bool anulado, bool vaciadoSimec, string proyecto)
        {
            return Dsegui.buscar_R148(anulado, vaciadoSimec, proyecto);
        }

        internal bool anularR148(int CodigoR148, bool isChecked)
        {
            return Dsegui.anularR148(CodigoR148, isChecked);
        }

        internal DataSet get_R148(int codigoR148)
        {
            return Dsegui.get_R148(codigoR148);
        }

        internal bool marcarVaciadoR148(int CodigoR148)
        {
           return Dsegui.marcarVaciadoR148(CodigoR148);
        }
    }
}
