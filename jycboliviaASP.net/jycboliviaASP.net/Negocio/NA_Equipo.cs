using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio

{
    public class NA_Equipo
    {
        DA_Equipo Dequipo = new DA_Equipo();

        public NA_Equipo() { }

        public bool insertar(string nombre, string detalle, int estado)
        {
            return false;
        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar()
        {
            return false;
        }

        public DataSet MostrarAllEquipoSeguimiento(string Exbo,string nombreProyecto) {
        
            string consulta = "select eq.codigo,eq.exbo, proy.nombre as 'Nombre Proyecto', "+
                               " DATE_FORMAT(eq.fecha,'%d/%m/%Y') as fecha,  "+
                               " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y') AS 'Acta Provicional',  "+
                               " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') AS 'Tecnico Ingeniero', "+
                               " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') AS 'Acta Definitiva',  "+
                               " eeq.nombre as Estado1, eq.vendidoenciudad, eq.instaladoenciudad,  "+
                               " rr.nombre as 'Cobrador' "+
                               " from tb_proyecto proy  "+
                               " left join tb_responsable rr on proy.codcobradorasignado = rr.codigo "+
                               " ,tb_equipo eq "+
                               " left join (select feq.codigo,eq.nombre,feq.codEstadoEquipo from tb_fechaestadoequipo feq, tb_estado_equipo eq " +
                               " where feq.codEstadoEquipo = eq.codigo) as eeq on (eq.codfechaestadoequipo= eeq.codigo) "+   
                               " where eq.estado=1  and eq.cod_proyecto= proy.codigo "+
                               " and eeq.codEstadoEquipo in (4,5,8,9,10) " +
                               " and eq.exbo like '%"+Exbo+"%' and proy.nombre like '%"+nombreProyecto+"%' "+
                               " order by proy.nombre asc ;";
            return Dequipo.getDatos(consulta);                    
        }

        public DataSet MostrarAllEquipoSeguimiento2(string Exbo, string nombreProyecto)
        {

            string consulta = "select eq.codigo,eq.exbo, proy.nombre as 'Nombre Proyecto', " +
                               " DATE_FORMAT(eq.fecha,'%d/%m/%Y') as fecha,  " +
                               " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y') AS 'Acta Provicional',  " +
                               " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') AS 'Tecnico Ingeniero', " +
                               " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') AS 'Acta Definitiva',  " +
                               " eeq.nombre as Estado1, eq.vendidoenciudad, eq.instaladoenciudad,  " +
                               " rr.nombre as 'Cobrador' " +
                               " from tb_proyecto proy  " +
                               " left join tb_responsable rr on proy.codcobradorasignado = rr.codigo " +
                               " ,tb_equipo eq " +
                               " left join (select feq.codigo,eq.nombre,feq.codEstadoEquipo from tb_fechaestadoequipo feq, tb_estado_equipo eq " +
                               " where feq.codEstadoEquipo = eq.codigo) as eeq on (eq.codfechaestadoequipo= eeq.codigo) " +
                               " where eq.estado=1  and eq.cod_proyecto= proy.codigo " +
                               " and eeq.codEstadoEquipo in (10) " +
                               " and eq.exbo like '%" + Exbo + "%' and proy.nombre like '%" + nombreProyecto + "%' " +
                               " order by proy.nombre asc ;";
            return Dequipo.getDatos(consulta);
        }

        public int getCodigoEquipo(string exbo) {            
                string consulta = "select eq.codigo from tb_equipo eq where eq.estado = 1 and eq.exbo = '" + exbo + "'";
                DataSet datoResul = Dequipo.getDatos(consulta);
                if (datoResul.Tables[0].Rows.Count > 0) {
                    int res;
                    int.TryParse(datoResul.Tables[0].Rows[0][0].ToString(), out res);
                    return res;
                }else
                    return -1;            
        }

        public DataSet getEncargadosResponsables(int codEquipo,int cargo) {
            string consulta = "select resp.codigo,resp.nombre from tb_detalle_tecnico_asignado dta, tb_responsable resp " +
                              " where dta.codresp = resp.codigo and dta.estado = 1 and dta.codeq = " + codEquipo + " and dta.codcargo = " + cargo;
                              
            return Dequipo.getDatos(consulta);
        }



        public bool tieneSeguimientoInstalacion(int codigoEquipo) {
            try {
                string consulta = "select eq.codseginstalacion from tb_equipo eq where eq.codigo = " + codigoEquipo;
                DataSet tuplaRes = Dequipo.getDatos(consulta);
                int codigoSeguimientoInstacion = Convert.ToInt32(tuplaRes.Tables[0].Rows[0][0].ToString());
                if (codigoSeguimientoInstacion > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
                
            }catch(Exception){
             return false;
            }

        
        }

        

        public bool modificarSeguimientoInstalacion(int codEquipo, int codSeguimientoInstalacion, string fechaEquipoObra) {
            return Dequipo.modificarSeguimientoInstalacion(codEquipo,codSeguimientoInstalacion, fechaEquipoObra);
        }

        public bool modificarSeguimientoInstalacionFechaEquipoObra(int codEquipo, string fechaEquipoObra)
        {
            return Dequipo.modificarSeguimientoInstalacionFechaEquipoObra(codEquipo,  fechaEquipoObra);
        }

        public int getCodigoSeguimientoEquipo(int codigoEquipo) {
            try
            {
                string consulta = "select eq.codseginstalacion from tb_equipo eq where eq.codigo = "+codigoEquipo;
                DataSet tupla = Dequipo.getDatos(consulta);
                int codigo = Convert.ToInt32(tupla.Tables[0].Rows[0][0].ToString());
                return codigo;
            }
            catch (Exception) {
                return -1;
            }
        
        }


        public DataSet getEquiposProyectosEstadosTodos(string exbo, string nombreEdificio)
        {
            string consulta = "select eq.codigo as 'Codigo',eq.exbo as 'Exbo', proy.nombre as 'Nombre Edificio', eq.parada, eq.pasajero, eq.velocidad, eseq.nombre as Estado1, " +
                                " DATE_FORMAT(eq.fechaaprobacionlimite_planos,'%d/%m/%Y') as 'FechaLimitePlanosFabrica', " +
                                " DATE_FORMAT(fe.fecha,'%d/%m/%Y') as 'FechaCambioEstado' ," +
                                " date_format(fe.hora,'%H:%i:%s') as 'HoraCambioEstado'"+
                                " ,res.nombre as 'Responsable_Cambio' "+
                                " , date_format(eq.fechaaproximadoarribopuerto,'%d/%m/%Y') as 'fechaAproxPuerto'  " +
                                " , eq.vendidoenciudad,eq.instaladoenciudad "+
                                " from tb_proyecto proy, tb_equipo eq, tb_estado_equipo eseq, tb_fechaestadoequipo fe "+
                                " LEFT JOIN tb_responsable res ON res.codigo = fe.codusercambio "+ 
                                " where proy.codigo = eq.cod_proyecto " +
                                " and eq.codfechaestadoequipo = fe.codigo and fe.codEstadoEquipo = eseq.codigo " +                                
                                " and proy.nombre like '%" + nombreEdificio + "%' and eq.exbo like '%" + exbo + "%' "+
                               // " and eq.estado = 1 "+
                                " order by proy.nombre asc";
            return Dequipo.getDatos(consulta);
        }


        public DataSet getEquiposProyectosEstados(string exbo, string nombreEdificio,int estadoEquipo) {
            string consulta = "select eq.codigo as 'Codigo',eq.exbo as 'Exbo', proy.nombre as 'Nombre Edificio', eq.parada, eq.pasajero, eq.velocidad, eseq.nombre as Estado1 ," +
                                " DATE_FORMAT(eq.fechaaprobacionlimite_planos,'%d/%m/%Y') as 'FechaLimitePlanosFabrica', " +
                                " DATE_FORMAT(fe.fecha,'%d/%m/%Y') as 'FechaCambioEstado' ," +
                                " date_format(fe.hora,'%H:%i:%s') as 'HoraCambioEstado' " +
                                " ,res.nombre as 'Responsable_Cambio' "+
                                " ,date_format(eq.fechaaproximadoarribopuerto,'%d/%m/%Y') as 'fechaAproxPuerto'  " +
                                " , eq.vendidoenciudad,eq.instaladoenciudad "+
                                " from tb_proyecto proy,tb_equipo eq, tb_estado_equipo eseq, tb_fechaestadoequipo fe " +
                                " LEFT JOIN tb_responsable res ON res.codigo = fe.codusercambio " + 
                                " where proy.codigo = eq.cod_proyecto "+
                                " and eq.codfechaestadoequipo = fe.codigo and fe.codEstadoEquipo = eseq.codigo "+
                                " and res.codigo = fe.codusercambio "+
                                " and proy.nombre like '%" + nombreEdificio + "%' and eq.exbo like '%" + exbo + "%' and fe.codEstadoEquipo = " +estadoEquipo+
                             //   " and eq.estado = 1 "+
                                " order by proy.nombre asc";
            return Dequipo.getDatos(consulta);
        }


        public DataSet getEquiposContratosFirmados1(string nombreEdificio, string exbo) {
            string consulta = "select eq.codigo,eq.exbo, proy.nombre as 'Edificio', tipo.nombre as 'TipoEquipo', marca.nombre as 'Marca', "+
                               " eq.parada,eq.pasajero, eq.velocidad, eq.modelo, " +
                               " date_format(eq.fecha_acta_definitiva,'%d/%m/%Y') as 'ActaDefinitiva', "+
                               " date_format(eq.fecha_equipo_entregado,'%d/%m/%Y') as 'EquipoEntregado', "+
                               " date_format(eq.mantenimientogratuito_inicio,'%d/%m/%Y') as 'MantenimientoGratuitoIncio', "+
                               " eq.mesesgratuitos, "+
                               " date_format(eq.mantenimientogratuito_fin,'%d/%m/%Y') as 'MantenimientoGratuitoFin', "+
                               " date_format(fechacon.fechafirmacontrato,'%d/%m/%Y') as 'ContratoFirmado', "+
                               " fechacon.codcontrato as 'NroContrato', fechacon.monto as 'MontoContrato', "+
                               " date_format(fechacon.fechainicio,'%d/%m/%Y') as 'InicioContrato', "+
                               " fechacon.mesescontrato as 'MesesContrato', "+
                               " date_format(fechacon.fechafin,'%d/%m/%Y') as 'FinContrato' "+
                               " ,date_format(eq.fechahabilitacionequipo,'%d/%m/%Y') as 'EquipoHabilitado' "+
                               " from tb_proyecto proy, tb_equipo eq   "+
                               " left join tb_tipoequipo tipo ON tipo.codigo = eq.codtipoequipo  "+
                               " left join tb_marca marca ON marca.codigo = eq.codmarca "+
                               " left join tb_fechacontrato_firmado fechacon ON fechacon.codigo = eq.codfechacontratofirmado "+
                               " where  eq.cod_proyecto = proy.codigo "+
                               " and proy.nombre like '%"+nombreEdificio+"%' and eq.exbo like '%"+exbo+"%'";

            return Dequipo.getDatos(consulta);
        }

        public bool actualizarMantenimientoGratuito(int codEquipo, string fechaInicio, int meses, string fechaFin) {
            return Dequipo.actualizarMantenimientoGratuito(codEquipo,fechaInicio,meses,fechaFin);
        }

        public bool actualizarFechaContrato(int codEquipo, int codFechaContrato) {
            return Dequipo.actualizarFechaContrato(codEquipo, codFechaContrato);
        }

        public int get_Cant_AllResponsableProyecto(string exbo, string edificio, string nameRespEdificio, string nameTecnicoInstalador, string NombreEstado, bool polizaSeguro, bool boletaBancaria, bool letraCambio)
        {
            string consulta = " select " +
                               " count(*)" +
                               " from tb_proyecto proy, " +
                               " tb_equipo eq " +
                               " left join (  select feqAUX.codigo,eqAux.nombre from tb_fechaestadoequipo feqAUX, tb_estado_equipo eqAux where feqAUX.codEstadoEquipo = eqAux.codigo  ) as eeq on (eq.codfechaestadoequipo = eeq.codigo)  " +
                               " left join tb_responsable res on (eq.codresponsable = res.codigo) " +
                               " left join (select dta.codeq,resp.codigo,resp.nombre from tb_detalle_tecnico_asignado dta, tb_responsable resp  " +
                               " where  dta.codresp = resp.codigo and dta.estado = 1 and dta.codcargo = 3) as t1 on (t1.codeq = eq.codigo) " +
                               " where eq.cod_proyecto = proy.codigo and  eq.exbo like '%" + exbo + "%' and proy.nombre like '%" + edificio + "%' and eeq.nombre like '%" + NombreEstado + "%'  "+
                               " and eq.estado = 1 ";
            if (nameRespEdificio != "")
            {
                consulta = consulta + " and res.nombre like '%" + nameRespEdificio + "%' ";
            }
            if (nameTecnicoInstalador != "")
            {
                consulta = consulta + " and t1.nombre like '%" + nameTecnicoInstalador + "%' ";
            }
            if (polizaSeguro)
            {
                consulta = consulta + " and proy.polizaseguro = true ";
            }

            if (boletaBancaria)
            {
                consulta = consulta + " and proy.boletabancaria = true ";
            }

            if (letraCambio)
            {
                consulta = consulta + " and proy.letracambio = true ";
            }            
                        
            DataSet tuplas = Dequipo.getDatos(consulta);

            int resultInt;
            bool sw = int.TryParse(tuplas.Tables[0].Rows[0][0].ToString(), out resultInt);
            if (sw)
            {
                return resultInt;
            }
            else
                return 0;

        }

        public DataSet getAllResponsableProyecto(string exbo, string edificio, string nameRespEdificio, string nameTecnicoInstalador, string NombreEstado, bool polizaSeguro, bool boletaBancaria, bool letraCambio, bool exportar, string codvariablesimec)
        {
            string consulta = " select "+                               
                               " proy.polizaseguro,  "+
                               " proy.boletabancaria,  "+
                               " proy.letracambio,   "+
                               " eq.codvariablesimec," +
                               " eq.exbo,  "+
                               " proy.nombre as 'Edificio', "+
                               " eq.vendidoenciudad, eq.instaladoenciudad, "+  
                               " eq.modelo , "+
                               " eq.parada,  "+
                               " eq.pasajero,  "+
                               " eq.velocidad, "+
                               " proy.direccion, "+
                               " eeq.nombre as 'EstadoEquipo', "+
                               " DATE_FORMAT(eq.fecha_equipo_obra,'%d/%m/%Y') as 'Equipo en Obra (R-114)', "+
                               " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y') as 'Acta Provicional (R-115)', "+
                               " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') as 'Acta Tecnico (R-117)', "+
                               " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') as 'Acta Definitiva (R-118.1)' , "+ 
                               " DATE_FORMAT(eq.fechahabilitacionequipo,'%d/%m/%Y') as 'Acta Habilitacion Equipo (R-118.2)', "+
                               " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y') as 'Fecha Entrega segun Contrato', "+
                               " DATE_FORMAT(eq.fechaaprobacionlimite_planos,'%d/%m/%Y') as 'Aprobacion Limite Plano', "+
                               " DATE_FORMAT(eq.fechaaproximadaembarque,'%d/%m/%Y') as 'Fecha Aprox. Embarque', "+
                               " DATE_FORMAT(eq.fechapagoembarque,'%d/%m/%Y') as 'Fecha Pago Embarque Segun Contrato', "+
                               " DATE_FORMAT(eq.fechaconfirmacionpagoembarque,'%d/%m/%Y') as 'Fecha Confirmacion Pago Embarque', "+
                               " date_format(eq.fechafase1,'%d/%m/%Y') as 'Fase 1', "+
                               " date_format(eq.fechafase2,'%d/%m/%Y') as 'Fase 2', "+
                               " res.nombre as 'ResponsableEquipo', "+
                               " t1.nombre as 'Tecnico Instalador', "+
                               " rrin.nombre as 'Rin', "+
                               " rrcc.nombre as 'RCC', "+
                               " rtec.nombre as 'TecMantenimiento', "+
                               " rsup.nombre as 'Supervisor' "+
                               " from "+
                               " tb_proyecto proy,  "+
                               " tb_equipo eq "+
                               " left join tb_responsable rrin on (eq.cod_rin = rrin.codigo) "+
                               " left join tb_responsable rrcc on (eq.cod_rcc = rrcc.codigo) "+
                               " left join tb_responsable rsup on (eq.cod_supervisor = rsup.codigo) "+
                               " left join tb_responsable rtec on (eq.cod_tecmantenimiento  = rtec.codigo)  "+
                               " left join (  select feqAUX.codigo,eqAux.nombre from tb_fechaestadoequipo feqAUX, tb_estado_equipo eqAux where feqAUX.codEstadoEquipo = eqAux.codigo  ) as eeq on (eq.codfechaestadoequipo = eeq.codigo)   "+
                               " left join tb_responsable res on (eq.codresponsable = res.codigo) "+
                               " left join (select dta.codeq,resp.codigo,resp.nombre from tb_detalle_tecnico_asignado dta, tb_responsable resp  "+
                               " where  dta.codresp = resp.codigo and dta.estado = 1 and dta.codcargo = 3) as t1 on (t1.codeq = eq.codigo) "+
                               " where eq.cod_proyecto = proy.codigo and  eq.exbo like '%"+exbo+"%' and proy.nombre like '%"+edificio+"%' and eeq.nombre like '%"+NombreEstado+"%' "+  
                               " and eq.estado = 1";
            if (nameRespEdificio != "" )
            {
                consulta = consulta + " and res.nombre like '%" + nameRespEdificio + "%' ";
            }
            if (nameTecnicoInstalador != "" )
            {
                consulta = consulta + " and t1.nombre like '%" + nameTecnicoInstalador + "%' ";
            }

            if (codvariablesimec != "")
            {
                consulta = consulta + " and eq.codvariablesimec like '%" + codvariablesimec + "%' ";
            }

            if(polizaSeguro){
                consulta = consulta + " and proy.polizaseguro = true ";
            }

            if (boletaBancaria)
            {
                consulta = consulta + " and proy.boletabancaria = true ";
            }

            if (letraCambio)
            {
                consulta = consulta + " and proy.letracambio = true ";
            }

            consulta = consulta + " order by proy.nombre asc";

            if(exportar == false){
                consulta = consulta + " Limit 250 ";
            }
            return Dequipo.getDatos(consulta);
        }

        public DataSet getAllResponsableProyecto_Responsable()
        {
            string consulta = "select res.nombre  " +
                               " from tb_proyecto proy, " +
                               " tb_equipo eq " +
                               " left join (select feqAUX.codigo,eqAux.nombre from tb_fechaestadoequipo feqAUX, tb_estado_equipo eqAux where feqAUX.codEstadoEquipo = eqAux.codigo  ) as eeq on (eq.codfechaestadoequipo = eeq.codigo)  " +
                               " left join tb_responsable res on (eq.codresponsable = res.codigo) " +
                               " where eq.cod_proyecto = proy.codigo  and not(res.nombre = '') " +
                               " group by res.nombre ";
            return Dequipo.getDatos(consulta);
        }

        public DataSet getAllResponsableProyecto_TecnicoInstalador()
        {
            string consulta = "select t1.nombre "+
                               " from tb_proyecto proy, "+
                               " tb_equipo eq "+
                               " left join (  select feqAUX.codigo,eqAux.nombre from tb_fechaestadoequipo feqAUX, tb_estado_equipo eqAux where feqAUX.codEstadoEquipo = eqAux.codigo  ) as eeq on (eq.codfechaestadoequipo = eeq.codigo)   "+
                               " left join tb_responsable res on (eq.codresponsable = res.codigo) "+
                               " left join (select dta.codeq,resp.codigo,resp.nombre from tb_detalle_tecnico_asignado dta, tb_responsable resp  "+
                               " where dta.codresp = resp.codigo and dta.estado = 1 and dta.codcargo = 3) as t1 on (t1.codeq = eq.codigo) "+
                               " where eq.cod_proyecto = proy.codigo and not(t1.nombre = '') "+
                               " group by t1.nombre ";
            return Dequipo.getDatos(consulta);
        }


          
        /*  aki programe  FechaCronograma -> fechaFaseI  */
        public DataSet getEquiposCronogramaTecnico(string Edificio , string exbo, string estado, string nombreResponsableProyecto, string nombreTecnicoInstalador) {
            string consulta = "select eq.codigo as 'CodEquipo', " +
                               " eq.exbo, proy.nombre as 'Edificio',  " +
                               " eq.parada as 'Paradas', eq.modelo, eq.pasajero, eq.velocidad,  " +
                               " DATE_FORMAT(eq.fechafase1,'%d/%m/%Y') as 'fechaFaseI', " +
                               " 'cantDiasFaseI', " +
                               " 'fechaConclusionFecha1', " +
                               " DATE_FORMAT(eq.fechafase2,'%d/%m/%Y') as 'fechaFase2', " +
                               " 'cantDiasFaseII', " +
                               " 'fechaConclusionFecha2', " +
                               " est.nombre as 'Estado1', " +
                               " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y') as 'Entrega Provisional (R-115)', " +
                               " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') as 'Acta Tecnico (R-117)', " +
                               " DATE_FORMAT(eq.fecha_acta_definitiva ,'%d/%m/%Y') as 'fecha Definitiva (R-118.1)', " +
                               " DATE_FORMAT(eq.fechahabilitacionequipo ,'%d/%m/%Y') as 'fecha Habilitacion Equipo (R-118.2)', " +
                               " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y') as 'fecha EntregaSegun Contrato', " +
                               " res.nombre as 'ResponsableEquipo',  " +
                               " t1.nombre as 'TecnicoInstalador' ," +
                               " eq.codvariablesimec "+
                               " from tb_proyecto proy, tb_fechaestadoequipo deq, tb_estado_equipo est,  " +
                               " tb_equipo eq " +
                               " left join tb_responsable res on eq.codresponsable = res.codigo " +
                               " left join (select dta.codeq,resp.codigo,resp.nombre from tb_detalle_tecnico_asignado dta, tb_responsable resp  " +
                               " where dta.codresp = resp.codigo and dta.estado = 1 and dta.codcargo = 3) as t1 on (t1.codeq = eq.codigo) " +
                               " where eq.cod_proyecto = proy.codigo and " +
                               " proy.nombre like '%" + Edificio + "%' and eq.exbo like '%" + exbo + "%' " +
                               " and eq.codfechaestadoequipo = deq.codigo " +
                               " and eq.estado = 1 " +
                               " and deq.codEstadoEquipo = est.codigo " +
                               " and est.nombre like '%" + estado + "%'  ";
                              // " and deq.codEstadoEquipo <> 10 ";
            if (nombreResponsableProyecto != "")
            {
                consulta = consulta + " and res.nombre like '%" + nombreResponsableProyecto + "%' ";
            }
            if (nombreTecnicoInstalador != "")
            {
                consulta = consulta + " and t1.nombre like '%" + nombreTecnicoInstalador + "%' ";
            }

            consulta = consulta + " order by proy.nombre asc";
                              
            return Dequipo.getDatos(consulta);

        }

      public bool modificarCronogramaTecnico(int codEquipo,int paradas, string pasajero, string modelo, string velocidad, string fechaCronograma,string fechaFase2){
          return Dequipo.modificarCronogramaTecnico(codEquipo, paradas,  pasajero,  modelo, velocidad,fechaCronograma,fechaFase2);        
        }

      public DataSet getEquiposCronogramaTecnico2(int codigoEquipo)
      {
          string consulta = "select eq.codigo as 'CodEquipo', " +
                               " eq.exbo, proy.nombre as 'Edificio', " +
                               " eq.parada as 'Paradas', eq.modelo, eq.pasajero, eq.velocidad, " +
                               " DATE_FORMAT(eq.fechafase1,'%d/%m/%Y') as 'fechafase1', " +
                               " DATE_FORMAT(eq.fechafase2,'%d/%m/%Y') as 'fechafase2' "+
                               " from tb_equipo eq, tb_proyecto proy " +
                               " where eq.cod_proyecto = proy.codigo and " +
                               " eq.codigo = " + codigoEquipo+
                               " order by proy.nombre asc";
          return Dequipo.getDatos(consulta);
      }

      public bool actualizarLetraID_equipo(int codigoEquipo, string LetraID) {
          return Dequipo.actualizarLetraID_equipo(codigoEquipo,LetraID);
      }

      public int getCodigoFechaEstadoEquipo(int codigoEquipo) {
          string consulta = " select "+
                            " eq.codfechaestadoequipo "+
                            " from tb_equipo eq "+
                            " where eq.codigo = "+codigoEquipo;
          DataSet dato = Dequipo.getDatos(consulta);
          if (dato.Tables[0].Rows.Count > 0)
          {
              return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
          }
          else
              return -1;

      
      }

    }
}