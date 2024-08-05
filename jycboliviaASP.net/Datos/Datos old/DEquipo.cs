using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DEquipo
    {
        private conexionMySql conexion = new conexionMySql();      

        public DEquipo() { }

        public bool insertarEquipo(string exbo, string fecha, string fechaActaProvisional, string fechaActaTecnico, string fechaActaDefinitiva,  int codigoActualizacion, int codigoProyecto, string fechaEquipoObra, string fechaEquipoEntregado, string tipologia, int codTipoEquipo, int codmarca, string codFiscalProyecto, string fechaAprobacionLimitePlanos,string modelo,string pasajero,int parada,string velocidad, string fechaAprobacionPlano, string FechaEntregaCliente, string fechaHabilitacionEquipo, string fechaaproximadaembarque, string fechapagoembarque)
        {
            
                if (fechaActaProvisional != "null")
                {
                    fechaActaProvisional = "'" + fechaActaProvisional + "'";
                }

                if (fechaActaTecnico != "null")
                {
                    fechaActaTecnico = "'" + fechaActaTecnico + "'";
                }

                if(fechaActaDefinitiva != "null"){
                fechaActaDefinitiva = "'"+fechaActaDefinitiva+"'";
                }

             
                string codigoActualizacionAux = "null";
                if (codigoActualizacion != -1)
                {
                    codigoActualizacionAux = Convert.ToString(codigoActualizacion);
                }

                if (fechaEquipoObra != "null")
                {
                    fechaEquipoObra = "'"+fechaEquipoObra+"'";
                }

                if (fechaEquipoEntregado != "null")
                {
                    fechaEquipoEntregado = "'"+fechaEquipoEntregado+"'";
                }
                                
                if (fechaAprobacionLimitePlanos != "null")
                {
                    fechaAprobacionLimitePlanos = "'"+fechaAprobacionLimitePlanos+"'";
                }

                string codTipoEquipoAux = "null";
                if(codTipoEquipo != -1){
                    codTipoEquipoAux = Convert.ToString(codTipoEquipo);
                }

                string codMarcaAux = "null";
                if (codmarca != -1)
                {
                    codMarcaAux = Convert.ToString(codmarca);
                }

            if(fechaAprobacionPlano != "null"){
                fechaAprobacionPlano = "'"+fechaAprobacionPlano+"'";
            }

            if(FechaEntregaCliente  != "null"){
                FechaEntregaCliente = "'"+FechaEntregaCliente +"'";
            }

            if (fechaHabilitacionEquipo != "null")               
            {
                fechaHabilitacionEquipo = "'" + fechaHabilitacionEquipo + "'";
            }


                string consulta = "insert into tb_equipo(exbo,fecha,fecha_acta_provicional,fecha_acta_tecnico_ing, "+
                                  " fecha_acta_definitiva,codActualizacion,cod_proyecto,estado, "+
                                  " fecha_equipo_obra,fecha_equipo_entregado,tipologia,codtipoequipo,codmarca,codresponsable,fechaaprobacionlimite_planos,modelo,pasajero,parada,velocidad, fechaaprobacionplano, fechaentregacliente ,  fechahabilitacionequipo, fechaaproximadaembarque, fechapagoembarque) " +
                                  " values ('"+exbo+"','"+fecha+"',"+fechaActaProvisional+","+fechaActaTecnico+", "+
                                  fechaActaDefinitiva+","+codigoActualizacionAux+","+codigoProyecto+",1, "+
                                   fechaEquipoObra + "," + fechaEquipoEntregado + ",'" + tipologia + "'," + codTipoEquipoAux + "," + codMarcaAux + "," + codFiscalProyecto + "," + fechaAprobacionLimitePlanos + ",'" + modelo + "','" + pasajero + "'," + parada + ",'" + velocidad + "'," + fechaAprobacionPlano + "," + FechaEntregaCliente + "," + fechaHabilitacionEquipo + ","+fechaaproximadaembarque+" , "+fechapagoembarque+") ";

                return conexion.ejecutarMySql(consulta);        
         }



        public bool modificarEquipo(int codigo, string fechaActaProvisional, string fechaActaTecnico, string fechaActaDefinitiva, int codigoActualizacion, string fechaEquipoObra, string fechaEquipoEntregado, string tipologia, int codTipoEquipo, int codmarca, string codFiscalProyecto, string fechalimiteAprobacionPlanos, string modelo, string pasajero, int parada, string velocidad, string fechaAprobacionPlano, string fechaEntregaCliente, string fechaHabilitacionEquipo, string fechaaproximadaembarque, string fechapagoembarque, string fechaConfirmacionPagoEmbarque, string CLICODSIMEC, int monedaSimecEquipo, string codRin, string codRCC, string codTecmant, string codSupervisor, string codvariableSimec, string IdentificadorAscensor) 
        {
            
                if (fechaActaProvisional != "null")
                {
                    fechaActaProvisional = "'" + fechaActaProvisional + "'";
                }
                if (fechaConfirmacionPagoEmbarque != "null") {
                    fechaConfirmacionPagoEmbarque = "'" + fechaConfirmacionPagoEmbarque + "'";
                }

                if (fechaActaTecnico != "null")
                {
                    fechaActaTecnico = "'" + fechaActaTecnico + "'";
                }

                if (fechaActaDefinitiva != "null")
                {
                    fechaActaDefinitiva = "'" + fechaActaDefinitiva + "'";
                }

           
                string codigoActualizacionAux = "null";
                if (codigoActualizacion != -1)
                {
                    codigoActualizacionAux = Convert.ToString(codigoActualizacion);
                }

                if (fechaEquipoObra != "null")
                {
                    fechaEquipoObra = "'" + fechaEquipoObra + "'";
                }

                if (fechaEquipoEntregado != "null")
                {
                    fechaEquipoEntregado = "'" + fechaEquipoEntregado + "'";
                }

                if (fechalimiteAprobacionPlanos != "null")
                {
                    fechalimiteAprobacionPlanos = "'" + fechalimiteAprobacionPlanos + "'";
                }

                string codTipoEquipoAux = "null";
                if (codTipoEquipo != -1)
                {
                    codTipoEquipoAux = Convert.ToString(codTipoEquipo);
                }

                string codMarcaAux = "null";
                if (codmarca != -1)
                {
                    codMarcaAux = Convert.ToString(codmarca);
                }


                if (fechaAprobacionPlano != "null")
                {
                    fechaAprobacionPlano = "'" + fechaAprobacionPlano + "'";
                }

                if (fechaEntregaCliente != "null")
                {
                    fechaEntregaCliente = "'" + fechaEntregaCliente + "'";
                }

                if (fechaHabilitacionEquipo != "null")
                {
                    fechaHabilitacionEquipo = "'" + fechaHabilitacionEquipo + "'";
                }

                if (fechaaproximadaembarque != "null")
                {
                    fechaaproximadaembarque = "'" + fechaaproximadaembarque + "'";
                }

                if (fechapagoembarque != "null")
                {
                    fechapagoembarque = "'" + fechapagoembarque + "'";
                }
                string consulta = "update tb_equipo set  tb_equipo.fecha_acta_provicional = "+fechaActaProvisional+", "+ 
                                  " tb_equipo.fecha_acta_tecnico_ing = "+fechaActaTecnico+", tb_equipo.fecha_acta_definitiva = "+fechaActaDefinitiva+","+
                                  " tb_equipo.fecha_equipo_obra = "+fechaEquipoObra+", tb_equipo.fecha_equipo_entregado = "+fechaEquipoEntregado+", "+
                                  " tb_equipo.codActualizacion = "+codigoActualizacionAux+", "+
                                  " tb_equipo.tipologia = '"+tipologia+"', "+
                                  " tb_equipo.codtipoequipo = "+codTipoEquipoAux+", tb_equipo.codmarca = "+codMarcaAux+
                                  " , tb_equipo.codresponsable = "+codFiscalProyecto+
                                  " , tb_equipo.fechaaprobacionlimite_planos = " + fechalimiteAprobacionPlanos +
                                  " , tb_equipo.modelo='" + modelo + "' " +
                                  " , tb_equipo.pasajero='" + pasajero + "' " +
                                  " , tb_equipo.parada=" + parada +
                                  " , tb_equipo.velocidad='" + velocidad + "' " +
                                  " , tb_equipo.fechaaprobacionplano= "+fechaAprobacionPlano+
                                  " , tb_equipo.fechaentregacliente= "+fechaEntregaCliente+
                                  " , tb_equipo.fechahabilitacionequipo="+fechaHabilitacionEquipo+
                                  " , tb_equipo.fechaaproximadaembarque=" + fechaaproximadaembarque +
                                  " , tb_equipo.fechapagoembarque=" + fechapagoembarque +
                                  " , tb_equipo.fechaconfirmacionpagoembarque="+fechaConfirmacionPagoEmbarque+
                                  " , tb_equipo.clicodigo = '"+CLICODSIMEC+"'"+
                                  " , tb_equipo.monedaprevision_simec = "+monedaSimecEquipo+
                                  " , tb_equipo.cod_rcc = "+codRCC+
                                  " , tb_equipo.cod_rin = "+codRin+
                                  " , tb_equipo.cod_supervisor = "+codSupervisor+
                                  " , tb_equipo.cod_tecmantenimiento = "+codTecmant+
                                  " ,tb_equipo.codvariablesimec = '"+codvariableSimec+"'"+
                                  " ,tb_equipo.ascensor = '"+IdentificadorAscensor+"'"+
                                  " where tb_equipo.codigo = "+codigo;
           return conexion.ejecutarMySql(consulta);
        }
  
        public bool eliminarEquipo1(int codigo) 
        {           
                string consulta = "delete from tb_equipo where tb_equipo.codigo= " + codigo + ";";
                return conexion.ejecutarMySql(consulta);
        }

        public bool ModificarFechaEstadoEquipo(int codEquipo, int CodFechaEstadoEquipo)
        {
            string consulta = "update tb_equipo set tb_equipo.codfechaestadoequipo = " + CodFechaEstadoEquipo +                                
                                " where tb_equipo.codigo= " + codEquipo;
            return conexion.ejecutarMySql(consulta);
        }

        public bool ModificarFechaEstadoEquipo2(int codEquipo, int CodFechaEstadoEquipo, string fechalimiteplanosAprovacion, string fechaAproximadaArriboPuerto)
        {
                if(fechalimiteplanosAprovacion != "null"){
                    fechalimiteplanosAprovacion = "'"+fechalimiteplanosAprovacion+"'";
                }

                if (fechaAproximadaArriboPuerto != "null")
                {
                    fechaAproximadaArriboPuerto = "'" + fechaAproximadaArriboPuerto + "'";
                }
            
                string consulta = "update tb_equipo set tb_equipo.codfechaestadoequipo = " + CodFechaEstadoEquipo + ", "+
                                   " tb_equipo.fechaaprobacionlimite_planos = "+fechalimiteplanosAprovacion+
                                   ",tb_equipo.fechaaproximadoarribopuerto = " + fechaAproximadaArriboPuerto +
                                    " where tb_equipo.codigo= " + codEquipo;
                return conexion.ejecutarMySql(consulta);                          
        }



        public bool actualizar_importacionJYCIA(int codigo, string nrofactura, string fechafactura, float montofactura, string fechagiro, float montogiro1, float montogiro2, float montogiro3, float montogiro4, float montogiro5, float valorfob, float valortransportemaritimo2, string nrocontenedor, string fechagiro2, string fechagiro3 , string fechagiro4, string fechagiro5, bool primerpago,bool segundopago, bool tercerpago){
            string montoFacturaAux = montofactura.ToString().Replace(',', '.');
            string montoGiro1Aux = montogiro1.ToString().Replace(',', '.');
            string montoGiro2Aux = montogiro2.ToString().Replace(',', '.');
            string montoGiro3Aux = montogiro3.ToString().Replace(',', '.');
            string montoGiro4Aux = montogiro4.ToString().Replace(',', '.');
            string montoGiro5Aux = montogiro5.ToString().Replace(',', '.');
            string valorFobAux = valorfob.ToString().Replace(',', '.');
            string valortransportemaritimo2Aux = valortransportemaritimo2.ToString().Replace(',', '.');

            
            string consulta = "update "+
                               " tb_equipo "+
                               " set "+
                               " tb_equipo.nrofactura = '"+nrofactura+"', "+
                               " tb_equipo.fechafactura = "+fechafactura+", "+
                               " tb_equipo.montofactura = "+montoFacturaAux+", "+
                               " tb_equipo.fechagiro = "+fechagiro+", "+
                               " tb_equipo.montogiro1 = "+montoGiro1Aux+", "+
                               " tb_equipo.montogiro2 = "+montoGiro2Aux+", "+
                               " tb_equipo.montogiro3 = "+montoGiro3Aux+", "+ 
                               " tb_equipo.montogiro4 = "+montoGiro4Aux+", "+
                               " tb_equipo.montogiro5 = " + montoGiro5Aux + ", " +
                               " tb_equipo.valorfob = " + valorFobAux+" , "+
                               " tb_equipo.valortransportemaritimo2 = "+ valortransportemaritimo2Aux+ " , "+
                               " tb_equipo.nrocontenedor = '"+ nrocontenedor+ "' , "+
                               " tb_equipo.fechagiro2 = "+fechagiro2+ " , "+
                               " tb_equipo.fechagiro3 = "+fechagiro3+ " , "+
                               " tb_equipo.fechagiro4 = "+fechagiro4+ " , "+
                               " tb_equipo.fechagiro5 = "+fechagiro5+ " , "+
                               " tb_equipo.1erpago = "+primerpago+ " , "+
                               " tb_equipo.2dopago = "+segundopago+" , "+
                               " tb_equipo.3erpago = "+tercerpago+
                               " where  "+
                               " tb_equipo.codigo = "+codigo;
            return conexion.ejecutarMySql(consulta);  
        }



        public DataSet listarEquipo()
        {
           string consulta = "select eq.codigo, eq.exbo as 'Chasis', pro.nombre as 'NombreProyecto', "+
                             " eq.tipologia, teq.nombre as 'TipoEquipo', m.nombre as 'MarcaEquipo' , "+
                             " eeq.nombre as 'EstadoEquipo', eq.codresponsable"+
                             " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y') as 'Fecha Acta Provicional', "+
                             " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') as 'Fecha Acta Tecnico', "+
                             " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') as 'Fecha Acta Definitiva', "+
                             " DATE_FORMAT(eq.fecha_equipo_obra,'%d/%m/%Y') as 'Fecha Equipo en Obra', "+
                             " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y') as 'Fecha Equipo Entregado' "+
                             " FROM  tb_proyecto pro, tb_equipo eq "+
                             " left join tb_tipoequipo teq on (eq.codtipoequipo = teq.codigo) "+
                             " left join tb_marca m on (eq.codmarca = m.codigo) ,"+                             
                             " tb_fechaestadoequipo feeq, tb_estado_equipo eeq "+
                             " where eq.cod_proyecto = pro.codigo and  eq.estado = 1 and eq.codfechaestadoequipo = feeq.codigo and "+
                             " feeq.codEstadoEquipo = eeq.codigo order by pro.nombre asc";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet cantiListaEquipo2(string exbo, string proyecto, string NombreEstado)
        {
            string consulta = "select " +
                                  "count(*) " +
                                  " FROM  tb_proyecto pro, tb_equipo eq " +
                                  " left join tb_tipoequipo teq on (eq.codtipoequipo = teq.codigo) " +
                                  " left join tb_marca m on (eq.codmarca = m.codigo) ," +
                                  " tb_fechaestadoequipo feeq, tb_estado_equipo eeq " +
                                  " where eq.cod_proyecto = pro.codigo and  eq.estado = 1 and eq.codfechaestadoequipo = feeq.codigo and " +
                                  " feeq.codEstadoEquipo = eeq.codigo " +
                                  " and eq.exbo like '%" + exbo + "%' and pro.nombre like '%" + proyecto + "%' and eeq.nombre like '%" + NombreEstado + "%' ";                                  

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listarEquipo2(string exbo, string proyecto, string NombreEstado, bool exportar)
        {
            string consulta = "select "+
                               " eq.codigo, "+
                               " eq.exbo as 'Chasis', "+
                               " eq.ascensor, "+
                               " pro.nombre as 'NombreProyecto', "+
                               " eq.tipologia as 'tipologia' , "+
                               " eq.modelo, "+
                               " eq.parada, "+
                               " eq.pasajero, "+
                               " eq.velocidad, "+
                               " teq.nombre as 'TipoEquipo', "+
                               " m.nombre as 'MarcaEquipo' ,  "+
                               " eeq.nombre as 'EstadoEquipo', "+
                               " DATE_FORMAT(eq.fecha_equipo_obra,'%d/%m/%Y') as 'Fecha Equipo en Obra (R-114)', "+
                               " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y') as 'Fecha Acta Provicional (R-115)', "+
                               " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') as 'Fecha Acta Tecnico (R-117)',  "+
                               " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') as 'Fecha Acta Definitiva (R-118.1)', "+ 
                               " DATE_FORMAT(eq.fechahabilitacionequipo, '%d/%m/%Y') as 'Fecha Habilitacion Equipo (R-118.2)' , "+
                               " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y') as 'Fecha Equipo Entregado Segun Contrato', "+
                               " DATE_FORMAT(id.fechaaprobacionlimite_planos,'%d/%m/%Y') as 'FechaLimitePlanosFabrica', "+
                               " DATE_FORMAT(eq.fechaaprobacionplano,'%d/%m/%Y') as 'Fecha Aprobacion Plano', "+
                               " DATE_FORMAT(eq.fechaentregacliente , '%d/%m/%Y') as 'Fecha Entrega al Cliente', "+
                               " DATE_FORMAT(id.fechaaproxembarque , '%d/%m/%Y') as 'Fecha Aprox. Embarque',  "+
                               " DATE_FORMAT(id.fechapagoembarque , '%d/%m/%Y') as 'FechaPagoEmbarque Segun Contrato', "+
                               " DATE_FORMAT(eq.fechaconfirmacionpagoembarque , '%d/%m/%Y') as 'Fecha Confirmacion Pago Embarque', " + 
                               " eq.codresponsable, "+
                               " rrin.nombre as 'Rin', "+
                               " rrcc.nombre as 'RCC', "+
                               " rtec.nombre as 'TecMantenimiento', "+
                               " rsup.nombre as 'Supervisor'  "+
                               " ,eq.clicodigo as 'CLICODIGO_SIMEC' "+
                               " ,eq.monedaprevision_simec "+
                               " ,eq.codvariablesimec "+
                               " FROM  tb_proyecto pro, tb_equipo eq " +
                               " left join tb_responsable rrin on (eq.cod_rin = rrin.codigo) "+
                               " left join tb_responsable rrcc on (eq.cod_rcc = rrcc.codigo) "+
                               " left join tb_responsable rsup on (eq.cod_supervisor = rsup.codigo) "+
                               " left join tb_responsable rtec on (eq.cod_tecmantenimiento  = rtec.codigo) "+
                              " left join tb_tipoequipo teq on (eq.codtipoequipo = teq.codigo) " +
                              " left join tb_marca m on (eq.codmarca = m.codigo) " +
                              " left join tb_importacion_datos id on (eq.codimportacion = id.codigo), "+
                              " tb_fechaestadoequipo feeq, tb_estado_equipo eeq " +                              
                              " where eq.cod_proyecto = pro.codigo and  eq.estado = 1 and eq.codfechaestadoequipo = feeq.codigo and "+
                              " feeq.codEstadoEquipo = eeq.codigo "+
                              " and eq.exbo like '%" + exbo + "%' and pro.nombre like '%" + proyecto + "%' and eeq.nombre like '%"+NombreEstado+"%' " +
                              " order by pro.nombre asc";
            if(exportar == false){
                consulta = consulta + " LIMIT 50";
            }

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listarEquipo2ConFiscalProyecto(string exbo, string proyecto, int codFiscal, string NombreEstado, bool exportar)
        {
            string consulta = "select "+
                               " eq.codigo,  "+
                               " eq.exbo as 'Chasis',  "+
                               " eq.ascensor, " +
                               " pro.nombre as 'NombreProyecto',  "+
                               " eq.tipologia as 'tipologia' , "+
                               " eq.modelo,  "+
                               " eq.parada,  "+
                               " eq.pasajero,  "+
                               " eq.velocidad, "+
                               " teq.nombre as 'TipoEquipo',  "+
                               " m.nombre as 'MarcaEquipo' , "+
                               " eeq.nombre as 'EstadoEquipo', "+
                               " DATE_FORMAT(eq.fecha_equipo_obra,'%d/%m/%Y') as 'Fecha Equipo en Obra (R-114)', "+
                               " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y') as 'Fecha Acta Provicional (R-115)', "+
                               " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y') as 'Fecha Acta Tecnico (R-117)', "+
                               " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y') as 'Fecha Acta Definitiva (R-118.1)', "+ 
                               " DATE_FORMAT(eq.fechahabilitacionequipo, '%d/%m/%Y') as 'Fecha Habilitacion Equipo (R-118.2)' , "+
                               " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y') as 'Fecha Equipo Entregado Segun Contrato', "+
                               " DATE_FORMAT(id.fechaaprobacionlimite_planos,'%d/%m/%Y') as 'FechaLimitePlanosFabrica', "+
                               " DATE_FORMAT(eq.fechaaprobacionplano,'%d/%m/%Y') as 'Fecha Aprobacion Plano', "+
                               " DATE_FORMAT(eq.fechaentregacliente , '%d/%m/%Y') as 'Fecha Entrega al Cliente', "+
                               " DATE_FORMAT(id.fechaaproxembarque , '%d/%m/%Y') as 'Fecha Aprox. Embarque', "+
                               " DATE_FORMAT(id.fechapagoembarque , '%d/%m/%Y') as 'FechaPagoEmbarque Segun Contrato',"+
                               " DATE_FORMAT(eq.fechaconfirmacionpagoembarque , '%d/%m/%Y') as 'Fecha Confirmacion Pago Embarque', " + 
                               " eq.codresponsable, " +                                                              
                               " rrin.nombre as 'Rin', "+
                               " rrcc.nombre as 'RCC', "+
                               " rtec.nombre as 'TecMantenimiento', "+
                               " rsup.nombre as 'Supervisor'  "+
                               " ,eq.clicodigo as 'CLICODIGO_SIMEC' " +
                               " ,eq.monedaprevision_simec "+
                               " ,eq.codvariablesimec " +
                              " FROM  tb_proyecto pro, tb_equipo eq " +
                               " left join tb_responsable rrin on (eq.cod_rin = rrin.codigo) "+
                               " left join tb_responsable rrcc on (eq.cod_rcc = rrcc.codigo) "+
                               " left join tb_responsable rsup on (eq.cod_supervisor = rsup.codigo) "+
                               " left join tb_responsable rtec on (eq.cod_tecmantenimiento  = rtec.codigo)  "+
                              " left join tb_tipoequipo teq on (eq.codtipoequipo = teq.codigo) " +
                              " left join tb_marca m on (eq.codmarca = m.codigo) " +
                              " left join tb_importacion_datos id on (eq.codimportacion = id.codigo), "+
                              " tb_fechaestadoequipo feeq, tb_estado_equipo eeq " +
                              " where eq.cod_proyecto = pro.codigo and  eq.estado = 1 and eq.codfechaestadoequipo = feeq.codigo and " +
                              " feeq.codEstadoEquipo = eeq.codigo and eq.codresponsable = "+codFiscal+
                              " and eq.exbo like '%" + exbo + "%' and pro.nombre like '%" + proyecto + "%' and eeq.nombre like '%"+NombreEstado+"%' " +
                              " order by pro.nombre asc";

            if (exportar == false)
            {
                consulta = consulta + " LIMIT 50";
            }
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscarEquipo(string exbo) 
        {
            string consulta = "select tb_equipo.codigo, tb_equipo.exbo,  DATE_FORMAT(tb_equipo.fecha,'%d/%m/%Y') as fecha, DATE_FORMAT(tb_equipo.fecha_acta_provicional,'%d/%m/%Y') as fechaActaProvisional, DATE_FORMAT(tb_equipo.fecha_acta_tecnico_ing,'%d/%m/%Y') as fechaActaTecnico, DATE_FORMAT(tb_equipo.fecha_acta_definitiva,'%d/%m/%Y') as fechaActaDefinitiva, tb_equipo.codEstado, tb_equipo.codActualizacion, tb_equipo.cod_proyecto from tb_equipo where tb_equipo.exbo like '12345';";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listarTecnicoManteniento() 
        {
            string consulta = "SELECT * from tb_responsable resp where resp.cargoc = 5 and resp.estado = 1";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }
        
        public DataSet listarTecnicoInstalador()
        {
            string consulta = "SELECT * from tb_responsable resp where resp.cargoc = 6 and resp.estado = 1";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listarSupervisorTecnico()
        {
            string consulta = "SELECT * from tb_responsable resp where resp.cargoc = 7 and resp.estado = 1";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listarCobrador() 
        {
            string consulta = "SELECT * from tb_responsable resp where resp.cargoc = 8 and resp.estado = 1";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet listarEncargadoCobro()
        {
            string consulta = "SELECT * from tb_responsable resp where resp.cargoc = 9 and resp.estado = 1";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet getEquipo(int codigoEquipo) {
            string consulta = "select eq.codigo,eq.exbo, eq.tipologia, "+
                               " DATE_FORMAT(eq.fecha,'%d/%m/%Y'), "+
                               " DATE_FORMAT(eq.fecha_acta_provicional,'%d/%m/%Y'), "+
                               " DATE_FORMAT(eq.fecha_acta_tecnico_ing,'%d/%m/%Y'), "+
                               " DATE_FORMAT(eq.fecha_acta_definitiva,'%d/%m/%Y'), "+
                               " DATE_FORMAT(eq.fecha_equipo_obra,'%d/%m/%Y'), "+
                               " DATE_FORMAT(eq.fecha_equipo_entregado,'%d/%m/%Y'), "+
                               " feq.codEstadoEquipo, eq.codActualizacion, eq.cod_proyecto, "+
                               " eq.codtipoequipo, eq.codmarca , eq.codresponsable , "+
                               " DATE_FORMAT(id.fechaaprobacionlimite_planos,'%d/%m/%Y'), " +
                               " eq.modelo,eq.pasajero,eq.parada,eq.velocidad ,"+
                               " DATE_FORMAT(eq.fechaaprobacionplano,'%d/%m/%Y') as 'Fecha Aprobacion Plano', " +
                               " DATE_FORMAT(eq.fechaentregacliente , '%d/%m/%Y') as 'Fecha Entrega al Cliente', " +
                               " DATE_FORMAT(eq.fechahabilitacionequipo, '%d/%m/%Y') as 'Fecha Habilitacion Equipo', " +
                               " DATE_FORMAT(id.fechaaproxembarque, '%d/%m/%Y') as 'Fecha Aprox. Embarque', " +
                               " DATE_FORMAT(id.fechapagoembarque, '%d/%m/%Y') as 'FechaPagoEmbarque', " +
                               " DATE_FORMAT(eq.fechaconfirmacionpagoembarque, '%d/%m/%Y') as 'Fecha Confirmacion Pago Embarque', "+
                               " eq.clicodigo, eq.monedaprevision_simec " +
                               " ,eq.codvariablesimec " +
                               " ,eq.ascensor " +
                               " from tb_equipo eq "+
                               " left join tb_importacion_datos id on (eq.codimportacion = id.codigo), "+
                               " tb_fechaestadoequipo feq where "+
                               " eq.codfechaestadoequipo = feq.codigo and "+
                               " eq.codigo = "+codigoEquipo;
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public int ultimoinsertado()
        {
            try
            {
                string consulta = "SELECT MAX(segui.codigo) FROM  tb_equipo segui";
                DataSet datoResul = conexion.consultaMySql(consulta);
                int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codUltimo;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public int getCodigoEstadoEquipo_FechaEstado(int codigoFechaEstadoEquipo)
        {
            try
            {
            string consulta = " select feeq.codEstadoEquipo from tb_fechaestadoequipo feeq where feeq.codigo = "+ codigoFechaEstadoEquipo;
            DataSet datoResul = conexion.consultaMySql(consulta);
            int codUltimo = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
            return codUltimo;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = conexion.consultaMySql(consulta);
            return datosR;
        }

        ////-------------------------------------------

        public DataSet listarControlPedido()
        {
            string consulta = "SELECT date_format(e.fechaventa, '%d/%m/%Y') as fechaventa,p.nombre as Proyecto, e.exbo as Exbo, estadoe.nombre as 'Estado Equipo' , " +
                               " ep.nombre as EncargadoPago, "+
                               " e.tipologia, p.departamento, e.r110, e.r148, e.r106, e.r107, e.r109, "+
                               " e.r113, e.primerpago, e.pagocontrato, e.fichero, e.ventacontrato " +
                               " FROM tb_proyecto p  "+
                               " left join tb_equipo e ON e.cod_proyecto = p.codigo "+
                               " left join tb_encargado_pago ep ON ep.codigo = p.codEncargado "+
                               " left join tb_fechaestadoequipo feq on e.codfechaestadoequipo = feq.codigo "+
                               " left join tb_estado_equipo estadoe on feq.codEstadoEquipo = estadoe.codigo "+
                               " ORDER BY e.fechaventa desc, p.nombre asc";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public bool insertarEquipoControlPedido(string exbo, string fecha, int codigoProyecto, bool r110, bool r148, bool r106, bool r107, bool r109, bool r113, bool ventaSistema, float primerPago, string tipologia, string fichero, string fechaventa, bool ventacontrato, string modelo, int parada, string pasajero, string velocidad, float vc, int codTipoEquipo, int codmarca, string ciudadVenta, string ciudadinstalacion, string fechaAproxEmbarque, float valorTransporteMaritimo, bool inventario, string fechapagoembarque, string fechaEquipoSegunContrato, string codigoContrato, string consignatario, bool contratofirmado, string ascensor, string empresacontratoproyecto)
        {
                string primerPagoaux = primerPago.ToString().Replace(',', '.');
                string valorTransporteMaritimoAux = valorTransporteMaritimo.ToString().Replace(',', '.');
                
                string codTipoEquipoAux = "null";
                if (codTipoEquipo != -1)
                {
                    codTipoEquipoAux = Convert.ToString(codTipoEquipo);
                }

                string codMarcaAux = "null";
                if (codmarca != -1)
                {
                    codMarcaAux = Convert.ToString(codmarca);
                }
                string consulta = "insert into tb_equipo(exbo,fecha, cod_proyecto, estado, " +
                                      "r110, r148, r106, r107, r109, r113, primerpago, pagocontrato, tipologia, fichero,fechaventa,ventacontrato,modelo,parada,pasajero,velocidad,vc,codtipoequipo,codmarca,vendidoenciudad,instaladoenciudad, "+
                                      " fechaaproximadaembarque, valorcfrtransportemaritimo, inventario, fechapagoembarque, fecha_equipo_entregado, codigocontrato, consignatario, contratofirmado,ascensor , empresacontratoproyecto) " +
                                      " values ('" + exbo + "', '" + fecha + "', " + codigoProyecto + ", " + 1 + ", " + r110 + ", " + r148 + ", " + r106 + ", " + r107 + ", " + r109 + ", " + r113 + ", " + ventaSistema + ", " + primerPagoaux + ",'" + tipologia + "','" + fichero + "'," + fechaventa + "," + ventacontrato + ",'" + modelo + "'," + parada + ",'" + pasajero + "','" + velocidad + "','" + vc.ToString().Replace(',','.') + "'," + codTipoEquipoAux + "," + codMarcaAux + ",'" + ciudadVenta + "','" + ciudadinstalacion + "'," + fechaAproxEmbarque + "," + valorTransporteMaritimoAux + "," + inventario + "," + fechapagoembarque + ", " + fechaEquipoSegunContrato + " , '" + codigoContrato + "' , '" + consignatario + "' , " + contratofirmado + ",'" + ascensor + "', '" + empresacontratoproyecto + "' ) ";
                return conexion.ejecutarMySql(consulta);
        }

        public int primerEstadoInsertado()
        {
            try
            {
                string consulta = "SELECT min(p.`codigo`) FROM `tb_proyecto` p WHERE p.`estado`=1";
                DataSet datoResul = conexion.consultaMySql(consulta);
                int codPrimero = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codPrimero;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int getCodigoEquipo(string exbo)
        {
            try
            {
                string consulta = " select e.codigo from tb_equipo e where e.exbo = '" + exbo + "' ";
                DataSet datoResul = conexion.consultaMySql(consulta);
                int codPrimero = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
                return codPrimero;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool modificarEquipoControlPedido(int codigo, string exbo, string tipologia, bool r110, bool r148, bool r106, bool r107, bool r109, bool r113, bool ventaSistema, float primerPago, int codigoProyecto, string fichero, string fechaventa, bool ventacontrato, string modelo, int parada, string pasajero, string velocidad, float vc, int codTipoEquipo, int codmarca, string ciudadVenta, string ciudadinstalacion, string fechaAproxEmbarque, float valorTransporteMaritimo, bool inventario, int estado, string fechapagoembarque, string fechaEquipoSegunContrato, string codigoContrato, string consignatario, bool contratofirmado, string ascensor, string empresacontratoproyecto)
        {
            string codTipoEquipoAux = "null";
            if (codTipoEquipo != -1)
            {
                codTipoEquipoAux = Convert.ToString(codTipoEquipo);
            }

            string codMarcaAux = "null";
            if (codmarca != -1)
            {
                codMarcaAux = Convert.ToString(codmarca);
            }
                string primerPagoaux = primerPago.ToString().Replace(',', '.');
                string valorTransporteMaritimoAux = valorTransporteMaritimo.ToString().Replace(',', '.');

                string consulta = " update tb_equipo set tb_equipo.exbo= '" + exbo + "', " +
                                  " tb_equipo.tipologia = '" + tipologia + "', " +
                                  " tb_equipo.r110 = " + r110 + ", tb_equipo.r148 = " + r148 + ", " +
                                  " tb_equipo.r106 = " + r106 + ", tb_equipo.r107 = " + r107 + ", " +
                                  " tb_equipo.r109 = " + r109 + ", tb_equipo.r113 = " + r113 + ", " +
                                  " tb_equipo.primerpago = " + ventaSistema + ", tb_equipo.pagocontrato = " + primerPagoaux + ", " +
                                  " tb_equipo.cod_proyecto = " + codigoProyecto + " , fichero = '" + fichero + "' ," +
                                  " tb_equipo.fechaventa = " + fechaventa + ", tb_equipo.ventacontrato = " + ventacontrato + ", " +
                                  " tb_equipo.modelo = '" + modelo + "', tb_equipo.parada= " + parada + ", tb_equipo.pasajero='" + pasajero + "', tb_equipo.velocidad='" + velocidad + "' " +
                                  " ,tb_equipo.vc = '"+vc.ToString().Replace(',','.')+"'"+
                                  " ,tb_equipo.codtipoequipo = " + codTipoEquipoAux + " , tb_equipo.codmarca = " + codMarcaAux + 
                                  " ,tb_equipo.vendidoenciudad = '"+ciudadVenta+"' , tb_equipo.instaladoenciudad = '"+ciudadinstalacion+"' "+
                                  " ,tb_equipo.fechaaproximadaembarque = " + fechaAproxEmbarque + " , tb_equipo.valorcfrtransportemaritimo = " + valorTransporteMaritimoAux + 
                                  " , tb_equipo.inventario = "+inventario+
                                  " , tb_equipo.estado = "+estado+
                                  " , tb_equipo.fechapagoembarque = " + fechapagoembarque +
                                  " ,tb_equipo.fecha_equipo_entregado = "+fechaEquipoSegunContrato+
                                  " ,tb_equipo.contratofirmado = "+contratofirmado+ 
                                  " ,tb_equipo.codigocontrato = '"+codigoContrato+"'"+
                                  " ,tb_equipo.consignatario = '"+consignatario+"'"+
                                  " ,tb_equipo.ascensor = '" + ascensor + "'" +
                                  " ,tb_equipo.empresacontratoproyecto = '"+empresacontratoproyecto+"'"+
                                  " where tb_equipo.codigo = " + codigo;
                return conexion.ejecutarMySql(consulta);
        }


        public DataSet buscador(string exbo)
        {
            string consulta = "SELECT e.exbo FROM tb_equipo e WHERE e.estado = 1 AND e.exbo LIKE '%" + exbo + "%' ORDER BY e.exbo ASC";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscadorNombreExbo(string exbo, string edificio)
        {
            string consulta = "SELECT e.codigo, concat(e.exbo,' - ',e.ascensor) as 'exbo' "+
                               " FROM tb_equipo e, tb_proyecto pp "+
                               " WHERE "+
                               " e.cod_proyecto = pp.codigo and "+
                               " e.estado = 1 AND "+
                               " e.exbo LIKE '%"+exbo+"%' and "+
                               " pp.nombre like '%"+edificio+"%' "+
                               " ORDER BY e.exbo ASC";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public DataSet buscadorNombreExbo2(string edificio)
        {
            string consulta = "SELECT e.codigo, concat(e.exbo,' - ',ifnull(e.ascensor,'')) as 'exbo' " +
                               " FROM tb_equipo e, tb_proyecto pp " +
                               " WHERE " +
                               " e.cod_proyecto = pp.codigo and " +
                               " e.estado = 1 AND " +                               
                               " pp.nombre = '" + edificio + "' " +
                               " ORDER BY e.exbo ASC";
            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        }

        public int obtenerValor(int codigoEquipo, int nro)
        {
            string consulta = "";
            try
            {
                switch (nro)
                {
                    case 1:
                        consulta = "SELECT e.`r110` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo  ;
                        break;
                    case 2:
                        consulta = "SELECT e.`r148` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 3:
                        consulta = "SELECT e.`r106` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 4:
                        consulta = "SELECT e.`r107` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 5:
                        consulta = "SELECT e.`r109` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 6:
                        consulta = "SELECT e.`r113` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 7:
                        consulta = "SELECT e.`primerpago` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 8:
                        consulta = "SELECT e.`ventacontrato` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo ;
                        break;
                    case 9:
                        consulta = "SELECT e.`inventario` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo;
                        break;
                    case 10:
                        consulta = "SELECT e.`contratofirmado` FROM `tb_equipo` e WHERE e.codigo =" + codigoEquipo;
                        break;
                }

                DataSet datoResul = conexion.consultaMySql(consulta);
                int lista = Convert.ToInt32(datoResul.Tables[0].Rows[0][0]);
                return lista;
            }
            catch (Exception)
            {
                return -1;
            }

        }
/*
        public DataSet BuscarControlEquipos(string nombreProyecto, string exbo, string nombrePropietario, string pasajero, string parada, string modelo, string velocidad) {
           string consulta = "SELECT date_format(e.fechaventa, '%d/%m/%Y') as fechaventa,p.nombre as Proyecto, e.exbo as Exbo, estadoe.nombre as 'Estado Equipo' , " +
                              " ep.nombre as Propietario, " +
                              " e.tipologia, p.departamento, e.r110, e.r148, e.r106, e.r107, e.r109, " +
                              " e.r113, e.primerpago, e.pagocontrato, e.fichero, e.ventacontrato, e.codigo as CodEstado, " +
                              " e.modelo,e.parada,e.pasajero,e.velocidad "+
                              " FROM tb_proyecto p  " +
                              " left join tb_equipo e ON e.cod_proyecto = p.codigo " +
                              " left join tb_propietario ep ON ep.codigo = p.codpropietario " +
                              " left join tb_fechaestadoequipo feq on e.codfechaestadoequipo = feq.codigo " +
                              " left join tb_estado_equipo estadoe on feq.codEstadoEquipo = estadoe.codigo " +
                              " where p.nombre like '%"+nombreProyecto+"%' and e.exbo like '%"+exbo+"%' " +
                              " ORDER BY e.fechaventa desc, p.nombre asc";
           

            string consulta = "SELECT date_format(e.fechaventa, '%d/%m/%Y') as fechaventa,p.nombre as Proyecto, e.exbo as Exbo, estadoe.nombre as 'Estado Equipo' , " +
                               " ep.nombre as Propietario, " +
                               " e.tipologia, p.departamento, e.r110, e.r148, e.r106, e.r107, e.r109, " +
                               " e.r113, e.primerpago, e.pagocontrato, e.fichero, e.ventacontrato, e.codigo as CodEstado, " +
                               " e.modelo,e.parada,e.pasajero,e.velocidad " +
                               " FROM tb_proyecto p  " +
                               " left join tb_equipo e ON e.cod_proyecto = p.codigo " +
                               " left join tb_propietario ep ON ep.codigo = p.codpropietario " +
                               " left join tb_fechaestadoequipo feq on e.codfechaestadoequipo = feq.codigo " +
                               " left join tb_estado_equipo estadoe on feq.codEstadoEquipo = estadoe.codigo " +
                               " where p.nombre like '%"+nombreProyecto+"%' and e.exbo like '%"+exbo+"%' and ep.nombre like '%"+nombrePropietario+"%' and " +
                               " e.pasajero like '%"+pasajero+"%' and e.parada like '%"+parada+"%' and e.modelo like '%"+modelo+"%' and e.velocidad like '%"+velocidad+"%' " +
                               " ORDER BY e.fechaventa desc, p.nombre asc;";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;
        
        }*/

       public DataSet BuscarControlEquipos2(string nombreProyecto, string exbo, string nombrePropietario, string pasajero, string parada, string modelo, string velocidad, string fechaDesde, string fechahasta, string fichero)
        {

            string consulta = "SELECT date_format(e.fechaventa, '%d/%m/%Y') as fechaventa,p.nombre as Proyecto, e.exbo as Exbo, estadoe.nombre as 'Estado Equipo' , " +
                               " ep.nombre as Propietario, " +
                               " e.tipologia, p.departamento, e.r110, e.r148, e.r106, e.r107, e.r109, " +
                               " e.r113, e.primerpago, e.pagocontrato, e.fichero, e.ventacontrato, e.codigo as CodEstado, " +
                               " e.modelo,e.parada,e.pasajero,e.velocidad, id.valorfob " +
                               " ,tie.nombre as 'TipoEquipo', "+
                               " m.nombre as 'Marca' "+
                               " ,e.instaladoenciudad,e.vendidoenciudad,"+
                               " id.valorcfrtransportemaritimo , " +
                               " date_format(id.fechaaproxembarque, '%d/%m/%Y') as fechaAproxEmbarque " +    
                               " ,e.inventario "+
                               " , date_format(id.fechapagoembarque, '%d/%m/%Y') as fechaPagoEmbarque" +
                               " ,date_format(e.fecha_equipo_entregado, '%d/%m/%Y') as 'Equipo_Entregado_Segun_Contrato', " +
                               "  e.contratofirmado, e.codigocontrato, e.consignatario, e.ascensor as 'IdAscensor', e.empresacontratoproyecto " +
                               " FROM tb_equipo e "+
                               " left join tb_tipoequipo tie on e.codtipoequipo = tie.codigo "+
                               " left join tb_marca m on e.codmarca = m.codigo "+
                               " left join tb_importacion_datos id on (e.codimportacion = id.codigo) "+
                               " ,tb_fechaestadoequipo feq, tb_estado_equipo estadoe, tb_proyecto p "+
                               " left join tb_propietario ep ON ep.codigo = p.codpropietario  "+
                               " where "+
                               " e.cod_proyecto = p.codigo and "+
                               " e.codfechaestadoequipo = feq.codigo and "+
                               " feq.codEstadoEquipo = estadoe.codigo and "+
                               " p.nombre like '%"+nombreProyecto+"%'" ;
                                
           if(exbo != ""){
           consulta = consulta + " and e.exbo like '%" + exbo + "%' ";
           }

            if (fichero != "")
            {
           consulta = consulta + " and e.fichero like '%"+fichero+"%' ";
           }

           if(nombrePropietario != ""){
           consulta = consulta + " and ep.nombre like '%" + nombrePropietario + "%' ";
           }
           if(pasajero != ""){
               consulta = consulta + "  and e.pasajero like '%" + pasajero + "%' ";
           }
           if(parada != ""){
               consulta = consulta + " and e.parada like '%" + parada + "%' ";
           }
           if(modelo != ""){
               consulta = consulta + " and e.modelo like '%" + modelo + "%' ";       
           }
           if(velocidad != ""){
               consulta = consulta + " and e.velocidad like '%" + velocidad + "%' "; 
           }
           if (fechaDesde != "null" && fechahasta != "null") {
               consulta = consulta + " and e.fechaventa BETWEEN " + fechaDesde + " and " + fechahasta + " ";
           }

           consulta = consulta + " ORDER BY e.fechaventa desc, p.nombre asc;";

            DataSet lista = conexion.consultaMySql(consulta);
            return lista;

        }

       public DataSet getEquipoJYC_pagos(int codigoEquipo)
       {
           string consulta = "select eq.1erpago, eq.2dopago, eq.3erpago from tb_equipo eq where eq.codigo = " + codigoEquipo;
           DataSet lista = conexion.consultaMySql(consulta);
           return lista;
       }

       public DataSet Buscar_ImportacionJYCIA(string Edificio, string exbo, string nrofactura, string fechafactura, string montofactura, string fechagiro, string montogiro1, string montogiro2, string montogiro3, string montogiro4, string montogiro5)
       {
           string consulta = "select eq.codigo, "+
                               " proy.nombre as 'Edificio',  "+
                               " eq.exbo, "+
                               " ee.nombre as 'tipoEquipo', "+
                               " mm.nombre as 'Marca', "+
                               " eq.parada, "+
                               " eq.pasajero, "+
                               " eq.modelo, "+
                               " eq.velocidad, "+
                               " eq.vc as 'Valor FOB (Alvaro)', " +
                               " eq.valorcfrtransportemaritimo as 'Transporte Maritimo (Alvaro)', "+
                               " date_format(eq.fechaaproximadaembarque,'%d/%m/%Y') as 'Fecha Aprox. Embarque', "+
                               " date_format(eq.fecha,'%d/%m/%Y') as 'Fecha Equipo',   " +
                               " eq.nrofactura, "+                               
                               " date_format(eq.fechafactura, '%d/%m/%Y') as 'Fecha Factura',  "+
                               " eq.montofactura,  "+
                               " date_format(eq.fechagiro, '%d/%m/%Y') as 'Fecha Giro1',  "+
                               " eq.montogiro1,  "+
                               " date_format(eq.fechagiro2, '%d/%m/%Y') as 'Fecha Giro2'," +
                               " eq.montogiro2, "+
                               " date_format(eq.fechagiro3, '%d/%m/%Y') as 'Fecha Giro3'," +
                               " eq.montogiro3,  "+
                               " date_format(eq.fechagiro4, '%d/%m/%Y') as 'Fecha Giro4'," +
                               " eq.montogiro4,  "+
                               " date_format(eq.fechagiro5, '%d/%m/%Y') as 'Fecha Giro5'," +
                               " eq.montogiro5, "+
                               " eq.valorfob, "+
                               " eq.valortransportemaritimo2, "+
                               " eq.nrocontenedor, "+
                               " eq.1erpago, "+
                               " eq.2dopago, "+
                               " eq.3erpago "+
                               ", (eq.montogiro1+ eq.montogiro2+eq.montogiro3+eq.montogiro4+eq.montogiro5) as 'TOTAL GIROS' " +
                               ", ((eq.vc + eq.valorcfrtransportemaritimo) - (eq.montogiro1+ eq.montogiro2+eq.montogiro3+eq.montogiro4+eq.montogiro5)) AS 'DIFERENCIA VALOR REAL / VRS GIRO' " +
                               ", ((eq.montogiro1+ eq.montogiro2+eq.montogiro3+eq.montogiro4+eq.montogiro5) - eq.montofactura ) as 'DIFERENCIA VALOR REAL VRS FACTURA' " +
                               " from  tb_proyecto proy, "+
                               " tb_equipo eq "+
                               " left join tb_tipoequipo ee on eq.codtipoequipo = ee.codigo  "+
                               " left join tb_marca mm on eq.codmarca = mm.codigo "+
                               " where   "+
                               " eq.cod_proyecto = proy.codigo  "; 
           
           if(!Edificio.Equals("")){
               consulta = consulta + " and proy.nombre like '%" + Edificio + "%'";
           }
           if(!exbo.Equals("")){
               consulta = consulta + " and eq.exbo like '%" + exbo + "%'";
           }
           if (!nrofactura.Equals("") && !nrofactura.Equals("0"))
           {
               consulta = consulta + " and eq.nrofactura = '"+nrofactura+"'";
           }
           if (!fechafactura.Equals("") && !fechafactura.Equals("null"))
           {
               consulta = consulta + " and eq.fechafactura = " + fechafactura;
           }
           if (!montofactura.Equals("") && !montofactura.Equals("0"))
           {
               consulta = consulta + " and eq.montofactura = " + montofactura;
           }
           if (!fechagiro.Equals("") && !fechagiro.Equals("null"))
           {
               consulta = consulta + " and eq.fechagiro = " + fechagiro;
           }
           if (!montogiro1.Equals("") && !montogiro1.Equals("0"))
           {
            consulta = consulta + " and eq.montogiro1 = "+montogiro1;
           }
           if (!montogiro2.Equals("") && !montogiro2.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro2 = " + montogiro2;
           }
           if (!montogiro3.Equals("") && !montogiro3.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro3 = " + montogiro3;
           }
           if (!montogiro4.Equals("") && !montogiro4.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro4 = " + montogiro4;
           }
           if (!montogiro5.Equals("") && !montogiro5.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro5 = " + montogiro5;
           }

           consulta = consulta + " order by eq.fecha desc";

           DataSet lista = conexion.consultaMySql(consulta);
           return lista;
       }


       public DataSet Buscar_ImportacionJYCIA_2(string Edificio, string exbo, string nrofactura, string fechafactura, string montofactura, string fechagiro, string montogiro1, string montogiro2, string montogiro3, string montogiro4, string montogiro5)
       {
           string consulta = "select eq.codigo, "+
                               " proy.nombre as 'Edificio', "+
                               " eq.exbo, "+
                               " ee.nombre as 'tipoEquipo', "+
                               " mm.nombre as 'Marca', "+
                               " eq.parada, "+
                               " eq.pasajero, "+
                               " eq.modelo, "+
                               " eq.velocidad, "+ 
                               " eq.vc as 'Valor FOB (Alvaro)', "+
                               " eq.valorcfrtransportemaritimo as 'Transporte Maritimo (Alvaro)', "+
                               " (eq.vc + eq.valorcfrtransportemaritimo) as 'VALOR TOTAL A PAGAR A FABRICA', "+
                               " date_format(eq.fechaaproximadaembarque,'%d/%m/%Y') as 'Fecha Aprox. Embarque', "+ 
                               " date_format(eq.fecha,'%d/%m/%Y') as 'Fecha Equipo', "+  
                               " eq.nrofactura, "+
                               " date_format(eq.fechafactura, '%d/%m/%Y') as 'Fecha Factura', "+ 
                               " eq.montofactura, "+ 
                               " date_format(eq.fechagiro, '%d/%m/%Y') as 'Fecha Giro1', "+ 
                               " eq.montogiro1, "+ 
                               " date_format(eq.fechagiro2, '%d/%m/%Y') as 'Fecha Giro2', "+
                               " eq.montogiro2, "+
                               " date_format(eq.fechagiro3, '%d/%m/%Y') as 'Fecha Giro3', "+
                               " eq.montogiro3, "+ 
                               " date_format(eq.fechagiro4, '%d/%m/%Y') as 'Fecha Giro4', "+
                               " eq.montogiro4, "+ 
                               " date_format(eq.fechagiro5, '%d/%m/%Y') as 'Fecha Giro5', "+
                               " eq.montogiro5, "+
                               " (eq.montogiro1+ eq.montogiro2+eq.montogiro3+eq.montogiro4+eq.montogiro5) as 'TOTAL GIROS', "+
                               " eq.valorfob, "+
                               " eq.valortransportemaritimo2, "+
                               " eq.nrocontenedor, "+
                               " eq.1erpago, "+
                               " eq.2dopago, "+
                               " eq.3erpago, "+
                               " ((eq.vc + eq.valorcfrtransportemaritimo) - (eq.montogiro1+ eq.montogiro2+eq.montogiro3+eq.montogiro4+eq.montogiro5)) AS 'DIFERENCIA VALOR REAL / VRS GIRO', "+
                               " ((eq.montogiro1+ eq.montogiro2+eq.montogiro3+eq.montogiro4+eq.montogiro5) - eq.montofactura ) as 'DIFERENCIA VALOR REAL VRS FACTURA' "+
                               "  from  tb_proyecto proy, "+
                               " tb_equipo eq "+
                               " left join tb_tipoequipo ee on eq.codtipoequipo = ee.codigo  "+
                               " left join tb_marca mm on eq.codmarca = mm.codigo "+
                               " where   "+
                               " eq.cod_proyecto = proy.codigo";

           if (!Edificio.Equals(""))
           {
               consulta = consulta + " and proy.nombre like '%" + Edificio + "%'";
           }
           if (!exbo.Equals(""))
           {
               consulta = consulta + " and eq.exbo like '%" + exbo + "%'";
           }
           if (!nrofactura.Equals("") && !nrofactura.Equals("0"))
           {
               consulta = consulta + " and eq.nrofactura = '" + nrofactura + "'";
           }
           if (!fechafactura.Equals("") && !fechafactura.Equals("null"))
           {
               consulta = consulta + " and eq.fechafactura = " + fechafactura;
           }
           if (!montofactura.Equals("") && !montofactura.Equals("0"))
           {
               consulta = consulta + " and eq.montofactura = " + montofactura;
           }
           if (!fechagiro.Equals("") && !fechagiro.Equals("null"))
           {
               consulta = consulta + " and eq.fechagiro = " + fechagiro;
           }
           if (!montogiro1.Equals("") && !montogiro1.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro1 = " + montogiro1;
           }
           if (!montogiro2.Equals("") && !montogiro2.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro2 = " + montogiro2;
           }
           if (!montogiro3.Equals("") && !montogiro3.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro3 = " + montogiro3;
           }
           if (!montogiro4.Equals("") && !montogiro4.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro4 = " + montogiro4;
           }
           if (!montogiro5.Equals("") && !montogiro5.Equals("0"))
           {
               consulta = consulta + " and eq.montogiro5 = " + montogiro5;
           }

           consulta = consulta + " order by eq.fecha desc";

           DataSet lista = conexion.consultaMySql(consulta);
           return lista;
       } 

       public int getCodigoEstado_Actual(int codEquipo)
       {
           try
           {
               string consulta = "  select  "+
                                    " ff.codEstadoEquipo "+
                                    " from tb_equipo eq , tb_fechaestadoequipo ff "+
                                    " where  "+
                                    " eq.codfechaestadoequipo = ff.codigo and "+
                                    " eq.codigo = " + codEquipo;
               DataSet datoResul = conexion.consultaMySql(consulta);
               int codEstado = Convert.ToInt32(datoResul.Tables[0].Rows[0][0].ToString());
               return codEstado;
           }
           catch (Exception )
           {
               return -1;
           }

       }



       internal bool modificarEquipo(int codEquipo, int parada, string pasajeros, string velocidad, string modelo)
       {
           string consulta = "update tb_equipo set tb_equipo.parada = "+parada+", tb_equipo.pasajero = '"+pasajeros+"', tb_equipo.velocidad = '"+velocidad+"', tb_equipo.modelo = '"+modelo+"' where tb_equipo.codigo = "+codEquipo;
           return conexion.ejecutarMySql(consulta);
       }

       public string getFechaAproxArriboPuerto(string exbo) {
           string consulta = "select date_format(eq.fechaaproximadoarribopuerto,'%d/%m/%Y') as fechaAproxPuerto from tb_equipo eq where eq.exbo = '" + exbo + "'";
           DataSet dato = conexion.consultaMySql(consulta);
           string fecha = "";
           if(dato.Tables[0].Rows.Count > 0){
               fecha = dato.Tables[0].Rows[0][0].ToString();
           }
           return fecha;
       }

       public DataSet getListaMaestraEquipos() {
           string consulta = "select "+
                           " proy.nombre as 'Edificio', "+
                           " eq.parada, proy.direccion, 'Santa Cruz' as Ciudad "+
                           " from db_seguimientoscz_jyc.tb_proyecto proy, db_seguimientoscz_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto and "+
                           " eq.instaladoenciudad = 'Santa Cruz' "+
                           " union  "+
                           " select "+
                           " proy.nombre as 'Edificio',  "+
                           " eq.parada, proy.direccion, 'Cochabamba' as Ciudad "+
                           " from db_seguimientocbba_jyc.tb_proyecto proy, db_seguimientocbba_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Cochabamba' "+
                           " union "+
                           " select "+
                           " proy.nombre as 'Edificio', "+
                           " eq.parada, proy.direccion, 'La Paz' as Ciudad "+
                           " from db_seguimientolpz_jyc.tb_proyecto proy, db_seguimientolpz_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'La Paz' "+
                           " union "+
                           " select "+
                           " proy.nombre as 'Edificio', "+
                           " eq.parada, proy.direccion, 'Sucre' as Ciudad "+
                           " from db_seguimientosucre_jyc.tb_proyecto proy, db_seguimientosucre_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Sucre' "+
                           " union  "+
                           " select  "+
                           " proy.nombre as 'Edificio',  "+
                           " eq.parada, proy.direccion, 'Tarija' as Ciudad "+
                           " from db_seguimientotarija_jyc.tb_proyecto proy, db_seguimientotarija_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Tarija' "+
                           " union "+
                           " select "+
                           " proy.nombre as 'Edificio', "+
                           " eq.parada, proy.direccion, 'Beni' as Ciudad "+
                           " from db_seguimientobeni_jyc.tb_proyecto proy, db_seguimientobeni_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Beni' "+
                           " union  "+
                           " select "+
                           " proy.nombre as 'Edificio',  "+
                           " eq.parada, proy.direccion, 'Potosi' as Ciudad "+
                           " from db_seguimientopotosi_jyc.tb_proyecto proy, db_seguimientopotosi_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto  "+
                           " and eq.instaladoenciudad = 'Potosi' "+
                           " union  "+
                           " select "+
                           " proy.nombre as 'Edificio',  "+
                           " eq.parada, proy.direccion, 'Oruro' as Ciudad "+
                           " from db_seguimientooruro_jyc.tb_proyecto proy, db_seguimientooruro_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Oruro' "+
                           " union "+
                           " select "+
                           " proy.nombre as 'Edificio', "+
                           " eq.parada, proy.direccion, 'Pando' as Ciudad "+
                           " from db_seguimientopando_jyc.tb_proyecto proy, db_seguimientopando_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Pando' "+
                           " union "+
                           " select "+
                           " proy.nombre as 'Edificio',  "+
                           " eq.parada, proy.direccion, 'Villamontes' as Ciudad "+
                           " from db_seguimientovillamontes_jyc.tb_proyecto proy, db_seguimientovillamontes_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Villamontes' "+
                           " union "+
                           " select "+
                           " proy.nombre as 'Edificio',  "+
                           " eq.parada, proy.direccion, 'Yacuiba' as Ciudad "+
                           " from db_seguimientoyacuiba_jyc.tb_proyecto proy, db_seguimientoyacuiba_jyc.tb_equipo eq "+
                           " where "+
                           " proy.codigo = eq.cod_proyecto "+
                           " and eq.instaladoenciudad = 'Yacuiba'";
           return conexion.consultaMySql(consulta);
       }


       public DataSet getConsultaCodigoDeAutenticacion(string exbo, string edificio)
       {
           string consulta = "select  " +
                               " proy.nombre as 'Edificio', " +
                               " proy.direccion, " +
                               " concat(eq.exbo,'-',ifnull(eq.ascensor,'')) as 'Exbo', " +
                               " eq.vendidoenciudad as 'Vendido', " +
                               " eq.instaladoenciudad as 'Instalado', " +
                               " mm.nombre as 'Marca', " +
                               " teq.nombre as 'Tipo', " +
                               " eq.parada, " +
                               " eq.pasajero, " +
                               " eq.velocidad, " +
                               " eq.modelo " +
                               " from tb_proyecto proy, " +
                               " tb_equipo eq  " +
                               " LEFT JOIN tb_marca mm ON (eq.codmarca = mm.codigo) " +
                               " LEFT JOIN tb_tipoequipo teq ON (eq.codtipoequipo = teq.codigo) " +
                               " where " +
                               " proy.codigo = eq.cod_proyecto and " +
                               " eq.estado = 1 and " +
                               " proy.nombre like '%" + edificio + "%' and " +
                               " eq.exbo like '%" + exbo + "%'";
           return conexion.consultaMySql(consulta);
       }


       public DataSet getConsultaCodigoDeAutenticacion_QR(string exbo, string edificio, string dirArchivo)
       {
           string consulta = "select  " +
                               " proy.nombre as 'Edificio', " +
                               " proy.direccion, " +
                               " concat(eq.exbo,'-',ifnull(eq.ascensor,'')) as 'Exbo', " +
                               " eq.vendidoenciudad as 'Vendido', " +
                               " eq.instaladoenciudad as 'Instalado', " +
                               " mm.nombre as 'Marca', " +
                               " teq.nombre as 'Tipo', " +
                               " eq.parada, " +
                               " eq.pasajero, " +
                               " eq.velocidad, " +
                               " eq.modelo , " +
                               " eq.qr_equipo, " +
                               " CAST(concat(eq.codigo,'_',eq.exbo,'_',proy.nombre) AS CHAR) as 'QR_nombreArchivo', " +
                               " CAST(concat('" + dirArchivo + "',eq.codigo,'_',eq.exbo,'_',proy.nombre,'.jpg') AS CHAR) as 'QR_DirNombreArchivo' " +
                               " from tb_proyecto proy, " +
                               " tb_equipo eq  " +
                               " LEFT JOIN tb_marca mm ON (eq.codmarca = mm.codigo) " +
                               " LEFT JOIN tb_tipoequipo teq ON (eq.codtipoequipo = teq.codigo) " +
                               " where " +
                               " proy.codigo = eq.cod_proyecto and " +
                               " eq.estado = 1 and " +
                               " proy.nombre like '%" + edificio + "%' and " +
                               " eq.exbo like '%" + exbo + "%'";
                               
           return conexion.consultaMySql(consulta);
        }


       public DataSet getConsultaCodigoDeAutenticacion_QR_ParaPegarEnAscensor(string exbo, string edificio, string dirArchivo)
       {
           string consulta = "select "+
                               " CAST(concat(proy.nombre,' - [Ruta ',ifnull(t1.nro,'No Asignado'),']') AS CHAR) as 'Glosa', " +
                               " proy.nombre as 'edificio',  "+
                               " eq.exbo, "+
                               " t1.nro, t1.detalle, "+
                               " t1.mes, t1.anio, "+
                               " eq.qr_equipo, "+ 
                               " CAST(concat(eq.codigo,'_',eq.exbo,'_',proy.nombre) AS CHAR) as 'QR_nombreArchivo',  "+
                               " CAST(concat('" + dirArchivo + "',eq.codigo,'_',eq.exbo,'_',proy.nombre,'.jpg') AS CHAR) as 'QR_DirNombreArchivo' "+
                               " from "+ 
                               " tb_proyecto proy, "+
                               " tb_equipo eq "+
                               " LEFT JOIN "+ 
                               " ( "+
                               " select "+ 
                               " cv.codeq, ru.nro, ru.detalle, ru.mes, ru.anio "+
                               " from tb_cronogramavisitarutamanteminieto cv, tb_ruta ru "+
                               " where "+
                               " cv.codruta = ru.codigo and "+
                               " ru.mes = month(current_date) and ru.anio = year(current_date) "+
                               " ) as t1 "+
                               " ON eq.codigo = t1.codeq "+
                               " where "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " eq.estado = 1 and "+
                               " eq.exbo <> '' and "+
                               " eq.exbo like '%"+exbo+"%' and "+
                               " proy.nombre like '%"+edificio+"%' "+
                               " order by t1.nro asc";
           return conexion.consultaMySql(consulta);
       }


       public DataSet getequipoControlPedido(int codigoEquipo)
       {
           string consulta = "select  "+
                               " proy.nombre as Edificio, "+
                               " eq.exbo , "+
                               " eq.vc, "+
                               " eq.valorcfrtransportemaritimo "+
                               " from tb_equipo eq, tb_proyecto proy "+
                               " where  "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " eq.codigo = "+codigoEquipo;
           return conexion.consultaMySql(consulta);
       }

       internal string get_codigoClienteSimec(string exbo)
       {
           string consulta = "select "+
                               " eq.clicodigo "+
                               " from tb_equipo eq "+
                               " where "+
                               " eq.exbo = '"+exbo+"'";
          DataSet dato = conexion.consultaMySql(consulta);
          if (dato.Tables[0].Rows.Count > 0)
          {
              return dato.Tables[0].Rows[0][0].ToString();
          }
          else
              return "";
       }

       internal DataSet get_cantidadEquiposdeEdificio(string edificio)
       {
           string consulta = "select "+
                               " eq.codigo, "+
                               " eq.exbo, "+
                               " proy.nombre "+
                               " from tb_equipo eq, tb_proyecto proy, tb_fechaestadoequipo feq "+
                               " where "+
                               " eq.cod_proyecto = proy.codigo and "+
                               " eq.estado = 1 and "+
                               " eq.codfechaestadoequipo = feq.codigo and "+
                               " feq.codEstadoEquipo = 10 and "+
                               " proy.nombre = '"+edificio+"' ";
           return conexion.consultaMySql(consulta);
       }

       internal bool actulizarCodigosQREquipos(string baseDatos)
       {
           string consulta = "update  tb_proyecto, tb_equipo "+
                               " LEFT JOIN tb_marca ON (tb_equipo.codmarca = tb_marca.codigo) "+
                               " SET "+
                               " tb_equipo.qr_equipo = "+
                               " concat(ifnull(tb_equipo.codigo,''),'|',  "+
                               " ifnull(tb_proyecto.nombre,''),'|', "+
                               " '"+baseDatos+"','|',  "+
                               " ifnull(tb_marca.nombre,'')) "+
                               " where "+
                               " tb_equipo.cod_proyecto = tb_proyecto.codigo "; 
           return conexion.ejecutarMySql(consulta);
       }

       internal DataSet buscar_ImportacionDatosGeneral(string Edificio, string exbo , string estado, string Dui, string Contenedor, bool todosLosDatos)
       {
           List<string> listBaseDatos = NA_VariablesGlobales.listBaseDatos;
           string consulta = "";
           for (int i = 0; i < listBaseDatos.Count; i++)
           {
               string baseDatos = listBaseDatos[i];
               string UNE;
               string Ciudad;
               if(baseDatos.Equals("db_seguimientoscz_jyc")){
                   UNE = "Interlogi"; 
                   Ciudad = "SCZ";
               }else
                   if(baseDatos.Equals("db_seguimientocbba_jyc")){
                    UNE = "Melevar"; 
                    Ciudad = "CBBA";
                   }else
                       if(baseDatos.Equals("db_seguimientolpz_jyc")){
                        UNE = "Elevamerica"; 
                        Ciudad = "LPZ";
                       }else
                            if(baseDatos.Equals("db_seguimientosucre_jyc")){
                                UNE = "JYC"; 
                                Ciudad = "Sucre";
                               }else
                                if(baseDatos.Equals("db_seguimientooruro_jyc")){
                                    UNE = "JYC"; 
                                    Ciudad = "Oruro";
                                   }else
                                    if(baseDatos.Equals("db_seguimientobeni_jyc")){
                                        UNE = "JYC"; 
                                        Ciudad = "Beni";
                                       }else
                                        if(baseDatos.Equals("db_seguimientopando_jyc")){
                                            UNE = "JYC"; 
                                            Ciudad = "Pando";
                                           }else
                                            if(baseDatos.Equals("db_seguimientotarija_jyc")){
                                                UNE = "JYC"; 
                                                Ciudad = "Tarija";
                                               }else
                                                if(baseDatos.Equals("db_seguimientoyacuiba_jyc")){
                                                    UNE = "JYC"; 
                                                    Ciudad = "Yacuiba";
                                                   }else
                                                    if(baseDatos.Equals("db_seguimientopotosi_jyc")){
                                                        UNE = "JYC"; 
                                                        Ciudad = "Potosi";
                                                       }else
                                                        if(baseDatos.Equals("db_seguimientovillamontes_jyc")){
                                                            UNE = "JYC"; 
                                                            Ciudad = "Villamontes";
                                                           }else
                                                            if(baseDatos.Equals("db_seguimientoparaguay_nuevo")){
                                                                UNE = "JYC"; 
                                                                Ciudad = "Paraguay";
                                                               }else
                                                            {
                                                                UNE = "JYC"; 
                                                                Ciudad = "Otro";
                                                            }
               if (i > 0)
                        consulta = consulta + " UNION ";


               consulta = consulta + "select " +
                               " eq.codigo,  " +
                               " eq.exbo, " +
                               " pp.nombre as 'Edificio',  " +
                               " eq.vendidoenciudad as 'Vendido',  " +
                               " eq.instaladoenciudad as 'Instalado', " +
                               " '" + UNE + "' AS 'UNE',  " +
                               " '" + Ciudad + "' as 'CIUDAD',  " +
                               " eeq.nombre as 'Estado1' , " +
                               " date_format(id.fechaaprobacionlimite_planos,'%d/%m/%Y') as 'fechaaprobacionlimite_planos', " +
                               " date_format(id.fechaaproximadoarribopuerto,'%d/%m/%Y') as 'fechaaproximadoarribopuerto',  " +
                               " date_format(id.fechaaproxembarque,'%d/%m/%Y') as 'FechaAproxEmbarque', " +
                               " date_format(id.fechapagoembarque,'%d/%m/%Y') as 'FechaPagoEmbarque',  " +
                               " id.valorfob as 'ValorFOB',  " +
                               " id.valorfob_pagado as 'ValorDelGiroAlProovedor',  " +
                               " id.valorcfrtransportemaritimo as 'ValorTransMaritimo',  " +
                               " id.valorcfrtransportemaritimo_pagado as 'ValorTransMaritimoPagado',  " +
                               " (id.giros1_dolares + id.giros2_dolares + id.giros3_dolares) as valorgiradoaproovedor_dolares," +
                               " (id.giros1_euros_cp + id.giros2_euros_cp + id.giros3_euros_cp) as 'valorgiradoaproovedor_euros'," +
                               " id.codnrodui as 'NroDui', " +
                               " id.codnrocontenedor as 'Contenedor', " +
                               " id.consignatario as 'Consignatario', " +
                               " date_format(id.fechafacturaproveedor,'%d/%m/%Y') as 'fechafacturaproveedor', " +
                               " id.nrofacturaproveedor, " +
                               " id.montofacturaproveedor, " +
                               " date_format(id.fechafactura_seguro,'%d/%m/%Y') as 'FechaFactura', " +
                               " id.nitoci_seguro as 'NIT', " +
                               " id.nrofactura_seguro as 'NroFactura', " +
                               " id.monto_seguro as 'MontoSeguro', " +
                               " id.nroaplicaciondelseguro," +
                               " id.valorcostoprima," +
                               " date_format(id.fechaarriboapuerto,'%d/%m/%Y') as 'fechaarriboapuerto'," +
                               " date_format(id.fechaarriboaduanero,'%d/%m/%Y') as 'fechaarriboaduanero'," +
                               " date_format(id.fechaarriboobra,'%d/%m/%Y') as 'fechaarriboobra'," +
                               " date_format(id.fechacrucefrontera,'%d/%m/%Y') as 'fechacrucefrontera'," +
                               " id.colorcanal," +
                               " id.nro_bl " +
                               " from  " +
                               baseDatos + ".tb_proyecto pp, " +
                               baseDatos + ".tb_equipo eq " +
                               " LEFT JOIN " + baseDatos + ".tb_importacion_datos id ON (eq.codimportacion = id.codigo) , " +
                               baseDatos + ".tb_fechaestadoequipo fe, " + baseDatos + ".tb_estado_equipo eeq " +
                               " where  " +
                               " eq.cod_proyecto = pp.codigo and " +
                               " eq.codfechaestadoequipo = fe.codigo and " +
                               " fe.codEstadoEquipo = eeq.codigo ";
                             //  " and fe.codEstadoEquipo not in (6,7,8,9,10,21) ";
                               if (!Edificio.Equals(""))
                               {
                                   consulta = consulta + " and pp.nombre like '%" + Edificio + "%' ";
                               }
                               if (!exbo.Equals(""))
                               {
                                   consulta = consulta + " and eq.exbo like '%" + exbo + "%' ";
                               }
                               if (!estado.Equals(""))
                               {
                                   consulta = consulta + " and eeq.nombre like '%" + estado + "%'";
                               }
                               if (!Dui.Equals(""))
                               {
                                   consulta = consulta + " and id.codnrodui like '%" + Dui + "%'";
                               }
                               if (!Contenedor.Equals(""))
                               {
                                   consulta = consulta + " and id.codnrocontenedor like '%" + Contenedor + "%'";
                               }
               if(todosLosDatos ==false){
               consulta = consulta + " limit 100";
               }

           }           
           return conexion.consultaMySql(consulta);
       }

       internal int cant_ImportacionDatosGeneral(string Edificio, string exbo, string estado, string Dui, string Contenedor)
       {
           DataSet tuplas = buscar_ImportacionDatosGeneral( Edificio,  exbo,  estado,  Dui,  Contenedor, true);
           return tuplas.Tables[0].Rows.Count;
       }

       internal bool insertarimportacionDatos(int codequipo, string exbo, string fechaaproxembarque, string fechapagoembarque,
                                               float valorfob,  float  valorcfrtransportemaritimo)
       {
           string consulta = "insert into tb_importacion_datos( " +
                               " tb_importacion_datos.fecha, " +
                               " tb_importacion_datos.hora, " +
                               " tb_importacion_datos.codequipo, " +
                               " tb_importacion_datos.exbo, " +
                               " tb_importacion_datos.fechaaproxembarque, " +
                               " tb_importacion_datos.fechapagoembarque, " +
                               " tb_importacion_datos.valorfob, " +
                               " tb_importacion_datos.valorcfrtransportemaritimo) " +
                               " values " +
                               " (current_date(), " +
                               " current_time(), " +
                               codequipo + ", " +
                               " '" + exbo + "', " +
                               fechaaproxembarque + ", " +
                               fechapagoembarque + ", " +
                               " '" + valorfob.ToString().Replace(',','.') + "', " +
                               " '" + valorcfrtransportemaritimo.ToString().Replace(',', '.') + "')";
           return conexion.ejecutarMySql(consulta);
       }

       public DataSet get_ultimoInsertadoImportacionDatos() {
           string consulta = "select max(ii.codigo) from tb_importacion_datos ii";
           return conexion.consultaMySql(consulta);
       }

       internal bool Actulizar_ImportacionDatos(int codEquipo, int codImportacion)
       {
           string consulta = "update tb_equipo set "+
                               " tb_equipo.codimportacion = "+codImportacion+
                                " where  tb_equipo.codigo = "+codEquipo ;
           return conexion.ejecutarMySql(consulta);
       }

       internal int get_CodigoImportacionDatos(int codigoEquipo)
       {
           string consulta = "select cc.codimportacion from tb_equipo cc where cc.codigo =" + codigoEquipo;
           DataSet resultImp = conexion.consultaMySql(consulta);
           if (resultImp.Tables[0].Rows.Count > 0)
           {
               int codigoImp;
               int.TryParse(resultImp.Tables[0].Rows[0][0].ToString(), out codigoImp);
               return codigoImp;
           }
           else
               return -1;
       }

       internal bool modificar_ImportacionDatos(int codimportacion, int codequipo, string exbo, string fechaaproxembarque, string fechapagoembarque,
                                               float valorfob, float valorcfrtransportemaritimo)
       {         

           string consulta = "update tb_importacion_datos set tb_importacion_datos.codequipo = " + codequipo + ", " +
                                " tb_importacion_datos.exbo = '" + exbo + "', " +
                                " tb_importacion_datos.fechaaproxembarque = " + fechaaproxembarque + ", " +
                                " tb_importacion_datos.fechapagoembarque = " + fechapagoembarque + ", " +
                                " tb_importacion_datos.valorfob = '" + valorfob.ToString().Replace(',', '.') + "', " +
                                " tb_importacion_datos.valorcfrtransportemaritimo = '" + valorcfrtransportemaritimo.ToString().Replace(',', '.') + "' " +
                                " where " +
                                " tb_importacion_datos.codigo = " + codimportacion;
           return conexion.ejecutarMySql(consulta);
       }

       internal bool modificar_ImportacionDatos2(int codimportacion, int codequipo, string fechaaproxembarque, string fechapagoembarque, string fechaaprobacionlimite_planos)
       {

           string consulta = "update tb_importacion_datos set tb_importacion_datos.codequipo = " + codequipo + ", " +
                                " tb_importacion_datos.fechaaprobacionlimite_planos = " + fechaaprobacionlimite_planos + ", " +
                                " tb_importacion_datos.fechaaproxembarque = " + fechaaproxembarque + ", " +
                                " tb_importacion_datos.fechapagoembarque = " + fechapagoembarque + 
                                " where " +
                                " tb_importacion_datos.codigo = " + codimportacion;
           return conexion.ejecutarMySql(consulta);
       }


       internal bool actualizar_importacionJYCIA_General(int codigoEquipo, string Ciudad, float ValorTransMaritimoPagado, string NroDui, string Contenedor, string Consignatario, string FechaFactura, string NIT, string NroFactura, float MontoSeguro,
           string fechaaprobacionlimite_planos, string fechaaproximadoarribopuerto, float valorcostoprima,
           string fechaarriboapuerto, string fechaarriboaduanero, string fechaarriboobra, string fechacrucefrontera,
           string colorcanal, string nroaplicaciondelseguro, float valorgiradoaproovedor_dolares, float valorgiradoaproovedor_euros, string nroBl,
           string fechafacturaproveedor, string nrofacturaproveedor, float montofacturaproveedor)
       {
           string baseDatos = "";

           if (Ciudad.Equals("SCZ"))
           {
               baseDatos = "db_SantaCruz";              
           }
           else
               if (Ciudad.Equals("CBBA"))
               {
                   baseDatos = "db_Cochabamba";                   
               }
               else
                   if (Ciudad.Equals("LPZ"))
                   {
                       baseDatos = "db_LaPaz";                       
                   }
                   else
                       if (Ciudad.Equals("Sucre"))
                       {
                           baseDatos = "db_Sucre";                           
                       }
                       else
                           if (Ciudad.Equals("Oruro"))
                           {
                               baseDatos = "db_Oruro";                               
                           }
                           else
                               if (Ciudad.Equals("Beni"))
                               {
                                   baseDatos = "db_beni";                                   
                               }
                               else
                                   if (Ciudad.Equals("Pando"))
                                   {
                                       baseDatos = "db_pando";                                       
                                   }
                                   else
                                       if (Ciudad.Equals("Tarija"))
                                       {
                                           baseDatos = "db_Tarija";                                           
                                       }
                                       else
                                           if (Ciudad.Equals("Yacuiba"))
                                           {
                                               baseDatos = "db_Yacuiba";                                               
                                           }
                                           else
                                               if (Ciudad.Equals("Potosi"))
                                               {
                                                   baseDatos = "db_Potosi";                                                  
                                               }
                                               else
                                                   if (Ciudad.Equals("Villamontes"))
                                                   {
                                                       baseDatos = "db_Villamontes";                                                       
                                                   }
                                                   else
                                                       if (Ciudad.Equals("Paraguay"))
                                                       {
                                                           baseDatos = "db_ParaguayNuevo";                                                           
                                                       }
                                                       else
                                                       {
                                                           baseDatos = "db_prueba";                                                           
                                                       }
           conexionMySql conx = new conexionMySql(baseDatos);
           float porcentaje;
           float.TryParse("0,0040", out porcentaje);
           valorcostoprima = (MontoSeguro * porcentaje);

           string consulta = "update tb_importacion_datos set "+
                               " tb_importacion_datos.valorcfrtransportemaritimo_pagado = '" + ValorTransMaritimoPagado.ToString().Replace(',', '.') + "', " +
                               " tb_importacion_datos.codnrodui = '"+ NroDui +"', "+
                               " tb_importacion_datos.codnrocontenedor = '"+ Contenedor+"', "+
                               " tb_importacion_datos.consignatario = '"+Consignatario+"', "+
                               " tb_importacion_datos.fechafactura_seguro = "+ FechaFactura+", "+
                               " tb_importacion_datos.nitoci_seguro = '"+NIT+"', "+
                               " tb_importacion_datos.nrofactura_seguro = '"+NroFactura+"', "+
                               " tb_importacion_datos.monto_seguro = '"+MontoSeguro.ToString().Replace(',','.')+"',"+
                               " tb_importacion_datos.fechaaprobacionlimite_planos = " +fechaaprobacionlimite_planos+ ","+
                               " tb_importacion_datos.fechaaproximadoarribopuerto = " + fechaaproximadoarribopuerto+","+
                               " tb_importacion_datos.valorcostoprima = '" +valorcostoprima.ToString().Replace(',','.')+"',"+
                               " tb_importacion_datos.fechaarriboapuerto = " + fechaarriboapuerto + "," +
                               " tb_importacion_datos.fechaarriboaduanero = " + fechaarriboaduanero + "," +
                               " tb_importacion_datos.fechaarriboobra = " + fechaarriboobra + "," +
                               " tb_importacion_datos.fechacrucefrontera = " + fechacrucefrontera + "," +
                               " tb_importacion_datos.colorcanal = '" + colorcanal + "'," +
                               " tb_importacion_datos.nroaplicaciondelseguro = '" + nroaplicaciondelseguro + "'," +
                               " tb_importacion_datos.valorgiradoaproovedor_dolares = '" + valorgiradoaproovedor_dolares.ToString().Replace(',', '.') + "'," +
                               " tb_importacion_datos.valorgiradoaproovedor_euros = '" + valorgiradoaproovedor_euros.ToString().Replace(',', '.') + "'," +
                               " tb_importacion_datos.nro_bl = '"+nroBl+"', "+
                               " tb_importacion_datos.fechafacturaproveedor = " + fechafacturaproveedor + ", " +
                               " tb_importacion_datos.nrofacturaproveedor = '" + nrofacturaproveedor + "', " +
                               " tb_importacion_datos.montofacturaproveedor = '" + montofacturaproveedor.ToString().Replace(',', '.') + "'" +
                               " where "+
                               " tb_importacion_datos.codequipo = "+codigoEquipo;
           bool bandera = conx.ejecutarMySql(consulta);
           return bandera;
       }

       public DataSet buscadorEquipo_GeneralTotal(string exbo){
           List<string> listBaseDatos = NA_VariablesGlobales.listBaseDatos;
           string consulta = "";
           for (int i = 0; i < listBaseDatos.Count; i++)
           {
               string baseDatos = listBaseDatos[i];
               string UNE;
               string Ciudad;
               if (baseDatos.Equals("db_seguimientoscz_jyc"))
               {
                   UNE = "Interlogi";
                   Ciudad = "SCZ";
               }
               else
                   if (baseDatos.Equals("db_seguimientocbba_jyc"))
                   {
                       UNE = "Melevar";
                       Ciudad = "CBBA";
                   }
                   else
                       if (baseDatos.Equals("db_seguimientolpz_jyc"))
                       {
                           UNE = "Elevamerica";
                           Ciudad = "LPZ";
                       }
                       else
                           if (baseDatos.Equals("db_seguimientosucre_jyc"))
                           {
                               UNE = "JYC";
                               Ciudad = "Sucre";
                           }
                           else
                               if (baseDatos.Equals("db_seguimientooruro_jyc"))
                               {
                                   UNE = "JYC";
                                   Ciudad = "Oruro";
                               }
                               else
                                   if (baseDatos.Equals("db_seguimientobeni_jyc"))
                                   {
                                       UNE = "JYC";
                                       Ciudad = "Beni";
                                   }
                                   else
                                       if (baseDatos.Equals("db_seguimientopando_jyc"))
                                       {
                                           UNE = "JYC";
                                           Ciudad = "Pando";
                                       }
                                       else
                                           if (baseDatos.Equals("db_seguimientotarija_jyc"))
                                           {
                                               UNE = "JYC";
                                               Ciudad = "Tarija";
                                           }
                                           else
                                               if (baseDatos.Equals("db_seguimientoyacuiba_jyc"))
                                               {
                                                   UNE = "JYC";
                                                   Ciudad = "Yacuiba";
                                               }
                                               else
                                                   if (baseDatos.Equals("db_seguimientopotosi_jyc"))
                                                   {
                                                       UNE = "JYC";
                                                       Ciudad = "Potosi";
                                                   }
                                                   else
                                                       if (baseDatos.Equals("db_seguimientovillamontes_jyc"))
                                                       {
                                                           UNE = "JYC";
                                                           Ciudad = "Villamontes";
                                                       }
                                                       else
                                                           if (baseDatos.Equals("db_seguimientoparaguay_nuevo"))
                                                           {
                                                               UNE = "JYC";
                                                               Ciudad = "Paraguay";
                                                           }
                                                           else
                                                           {
                                                               UNE = "JYC";
                                                               Ciudad = "Otro";
                                                           }
               if (i > 0)
                   consulta = consulta + " UNION ";

               consulta = consulta + "select " +
                               " eq.codigo,  " +
                               " eq.exbo, " +
                               " pp.nombre as 'Edificio',  " +
                               " eq.vendidoenciudad as 'Vendido',  " +
                               " eq.instaladoenciudad as 'Instalado', " +
                               " '" + UNE + "' AS 'UNE',  " +
                               " '" + Ciudad + "' as 'CIUDAD'  " +
                               " from  " +
                               baseDatos + ".tb_proyecto pp, " +
                               baseDatos + ".tb_equipo eq " +
                               " where  " +
                               " eq.cod_proyecto = pp.codigo "+
                               " and eq.exbo = '" + exbo + "'";                               
               
           }
           return conexion.consultaMySql(consulta);
       }


       public DataSet buscar_DatosGeneral_JYCIA(string Edificio, string exbo, string estado, bool todosLosDatos, string semana_expedicion)
       {
           List<string> listBaseDatos = NA_VariablesGlobales.listBaseDatos;
           string consulta = "";
           for (int i = 0; i < listBaseDatos.Count; i++)
           {
               string baseDatos = listBaseDatos[i];
               string UNE;
               string Ciudad;
               if (baseDatos.Equals("db_seguimientoscz_jyc"))
               {
                   UNE = "Interlogi";
                   Ciudad = "SCZ";
               }
               else
                   if (baseDatos.Equals("db_seguimientocbba_jyc"))
                   {
                       UNE = "Melevar";
                       Ciudad = "CBBA";
                   }
                   else
                       if (baseDatos.Equals("db_seguimientolpz_jyc"))
                       {
                           UNE = "Elevamerica";
                           Ciudad = "LPZ";
                       }
                       else
                           if (baseDatos.Equals("db_seguimientosucre_jyc"))
                           {
                               UNE = "JYC";
                               Ciudad = "Sucre";
                           }
                           else
                               if (baseDatos.Equals("db_seguimientooruro_jyc"))
                               {
                                   UNE = "JYC";
                                   Ciudad = "Oruro";
                               }
                               else
                                   if (baseDatos.Equals("db_seguimientobeni_jyc"))
                                   {
                                       UNE = "JYC";
                                       Ciudad = "Beni";
                                   }
                                   else
                                       if (baseDatos.Equals("db_seguimientopando_jyc"))
                                       {
                                           UNE = "JYC";
                                           Ciudad = "Pando";
                                       }
                                       else
                                           if (baseDatos.Equals("db_seguimientotarija_jyc"))
                                           {
                                               UNE = "JYC";
                                               Ciudad = "Tarija";
                                           }
                                           else
                                               if (baseDatos.Equals("db_seguimientoyacuiba_jyc"))
                                               {
                                                   UNE = "JYC";
                                                   Ciudad = "Yacuiba";
                                               }
                                               else
                                                   if (baseDatos.Equals("db_seguimientopotosi_jyc"))
                                                   {
                                                       UNE = "JYC";
                                                       Ciudad = "Potosi";
                                                   }
                                                   else
                                                       if (baseDatos.Equals("db_seguimientovillamontes_jyc"))
                                                       {
                                                           UNE = "JYC";
                                                           Ciudad = "Villamontes";
                                                       }
                                                       else
                                                           if (baseDatos.Equals("db_seguimientoparaguay_nuevo"))
                                                           {
                                                               UNE = "JYC";
                                                               Ciudad = "Paraguay";
                                                           }
                                                           else
                                                           {
                                                               UNE = "JYC";
                                                               Ciudad = "Otro";
                                                           }
               if (i > 0)
                   consulta = consulta + " UNION ";


               consulta = consulta + "select " +
                               " eq.codigo,  " +
                               " eq.exbo, " +
                               " pp.nombre as 'Edificio',  " +
                               " eq.vendidoenciudad as 'Vendido',  " +
                               " eq.instaladoenciudad as 'Instalado', " +
                               " '" + UNE + "' AS 'UNE',  " +
                               " '" + Ciudad + "' as 'CIUDAD',  " +
                               " eeq.nombre as 'Estado1' , " +                               
                               " date_format(id.fechaaproximadoarribopuerto,'%d/%m/%Y') as 'fechaaproximadoarribopuerto',  " +
                               " date_format(id.fechaaproxembarque,'%d/%m/%Y') as 'FechaAproxEmbarque', " +
                               " date_format(id.fechapagoembarque,'%d/%m/%Y') as 'FechaPagoEmbarque',  " +
                               " id.valorfob as 'ValorFOB',  " +
                               " id.valorfob_pagado as 'ValorDelGiroAlProovedor',  " +
                               " id.valorcfrtransportemaritimo as 'ValorTransMaritimo',  " +
                               " id.valorcfrtransportemaritimo_pagado as 'ValorTransMaritimoPagado',  " +
                               " id.consignatario as 'Consignatario', " +
                               " date_format(id.fechafacturaproveedor,'%d/%m/%Y') as 'fechafacturaproveedor', " +
                               " id.nrofacturaproveedor, " +
                               " id.montofacturaproveedor, " +
                               " id.semana_expedicion, "+
                               " id.giros1_nroproforma, " +
                               " id.giros1_nrooperacion, " +
                               " date_format(id.giro1_fecha ,'%d/%m/%Y') as 'fechaGiro1', " +
                               " id.giros1_euros_cp, " +
                               " id.giros1_tc_orona, " +
                               " id.giros1_dolares, " +
                               " id.giros2_nroproforma, " +
                               " id.giros2_nrooperacion, " +
                               " date_format(id.giro2_fecha ,'%d/%m/%Y') as 'fechaGiro2', " +
                               " id.giros2_euros_cp, " +
                               " id.giros2_tc_orona, " +
                               " id.giros2_dolares, " +
                               " id.giros3_nroproforma, " +
                               " id.giros3_nrooperacion, " +
                               " date_format(id.giro3_fecha ,'%d/%m/%Y') as 'fechaGiro3', " +
                               " id.giros3_euros_cp, " +
                               " id.giros3_tc_orona, " +
                               " id.giros3_dolares " +
                               " from  " +
                               baseDatos + ".tb_proyecto pp, " +
                               baseDatos + ".tb_equipo eq " +
                               " LEFT JOIN " + baseDatos + ".tb_importacion_datos id ON (eq.codimportacion = id.codigo) , " +
                               baseDatos + ".tb_fechaestadoequipo fe, " + baseDatos + ".tb_estado_equipo eeq " +
                               " where  " +
                               " eq.cod_proyecto = pp.codigo and " +
                               " eq.codfechaestadoequipo = fe.codigo and " +
                               " fe.codEstadoEquipo = eeq.codigo ";
                               //" and fe.codEstadoEquipo not in (6,7,8,9,10,21) ";
               if (!Edificio.Equals(""))
               {
                   consulta = consulta + " and pp.nombre like '%" + Edificio + "%' ";
               }
               if (!exbo.Equals(""))
               {
                   consulta = consulta + " and eq.exbo like '%" + exbo + "%' ";
               }
               if (!estado.Equals(""))
               {
                   consulta = consulta + " and eeq.nombre like '%" + estado + "%'";
               }
               if (!semana_expedicion.Equals(""))
               {
                   consulta = consulta + " and id.semana_expedicion like '%" + semana_expedicion + "%'";
               }


               if(todosLosDatos == false){
                   consulta = consulta + " limit 100";
               }
               
           }
           return conexion.consultaMySql(consulta);
       }

       internal int cant_DatosGeneral_JYCIA(string Edificio, string exbo, string estado, string semana_expedicion)
       {
           DataSet tuplas = buscar_DatosGeneral_JYCIA(Edificio, exbo, estado, true, semana_expedicion);
           return tuplas.Tables[0].Rows.Count;
       }     
       



       internal bool actualizar_DatosGeneralJYCIA_General(int codigoEquipo, string Ciudad,
           string giros1_nroproforma, string giros1_nrooperacion, string fechaGiro1, float giros1_euros_cp, float giros1_tc_orona, float giros1_dolares,
           string giros2_nroproforma, string giros2_nrooperacion, string fechaGiro2, float giros2_euros_cp, float giros2_tc_orona, float giros2_dolares,
           string giros3_nroproforma, string giros3_nrooperacion, string fechaGiro3, float giros3_euros_cp, float giros3_tc_orona, float giros3_dolares,
           string semana_expedicion)
       {
           string baseDatos = "";

           if (Ciudad.Equals("SCZ"))
           {
               baseDatos = "db_SantaCruz";
           }
           else
               if (Ciudad.Equals("CBBA"))
               {
                   baseDatos = "db_Cochabamba";
               }
               else
                   if (Ciudad.Equals("LPZ"))
                   {
                       baseDatos = "db_LaPaz";
                   }
                   else
                       if (Ciudad.Equals("Sucre"))
                       {
                           baseDatos = "db_Sucre";
                       }
                       else
                           if (Ciudad.Equals("Oruro"))
                           {
                               baseDatos = "db_Oruro";
                           }
                           else
                               if (Ciudad.Equals("Beni"))
                               {
                                   baseDatos = "db_beni";
                               }
                               else
                                   if (Ciudad.Equals("Pando"))
                                   {
                                       baseDatos = "db_pando";
                                   }
                                   else
                                       if (Ciudad.Equals("Tarija"))
                                       {
                                           baseDatos = "db_Tarija";
                                       }
                                       else
                                           if (Ciudad.Equals("Yacuiba"))
                                           {
                                               baseDatos = "db_Yacuiba";
                                           }
                                           else
                                               if (Ciudad.Equals("Potosi"))
                                               {
                                                   baseDatos = "db_Potosi";
                                               }
                                               else
                                                   if (Ciudad.Equals("Villamontes"))
                                                   {
                                                       baseDatos = "db_Villamontes";
                                                   }
                                                   else
                                                       if (Ciudad.Equals("Paraguay"))
                                                       {
                                                           baseDatos = "db_ParaguayNuevo";
                                                       }
                                                       else
                                                       {
                                                           baseDatos = "db_prueba";
                                                       }
           conexionMySql conx = new conexionMySql(baseDatos);

           giros1_dolares = (giros1_euros_cp * giros1_tc_orona);
           giros2_dolares = (giros2_euros_cp * giros2_tc_orona);
           giros3_dolares = (giros3_euros_cp * giros3_tc_orona);

           string consulta = "update tb_importacion_datos set " +
                               " tb_importacion_datos.semana_expedicion = '" + semana_expedicion + "', " +
                               " tb_importacion_datos.giros1_nroproforma = '" + giros1_nroproforma + "', " +
                               " tb_importacion_datos.giros1_nrooperacion = '" + giros1_nrooperacion + "', " +
                               " tb_importacion_datos.giro1_fecha = " + fechaGiro1 + ", " +
                               " tb_importacion_datos.giros1_euros_cp = '" + giros1_euros_cp.ToString().Replace(',', '.') + "', " +
                               " tb_importacion_datos.giros1_tc_orona = '" + giros1_tc_orona.ToString().Replace(',', '.') + "', " +
                               " tb_importacion_datos.giros1_dolares = '" + giros1_dolares.ToString().Replace(',', '.') + "', " +

                               " tb_importacion_datos.giros2_nroproforma = '" + giros2_nroproforma + "', " +
                               " tb_importacion_datos.giros2_nrooperacion = '" + giros2_nrooperacion + "', " +
                               " tb_importacion_datos.giro2_fecha = " + fechaGiro2 + ", " +
                               " tb_importacion_datos.giros2_euros_cp = '" + giros2_euros_cp.ToString().Replace(',', '.') + "', " +
                               " tb_importacion_datos.giros2_tc_orona = '" + giros2_tc_orona.ToString().Replace(',', '.') + "', " +
                               " tb_importacion_datos.giros2_dolares = '" + giros2_dolares.ToString().Replace(',', '.') + "', " +

                               " tb_importacion_datos.giros3_nroproforma = '" + giros3_nroproforma + "', " +
                               " tb_importacion_datos.giros3_nrooperacion = '" + giros3_nrooperacion + "', " +
                               " tb_importacion_datos.giro3_fecha = " + fechaGiro3 + ", " +
                               " tb_importacion_datos.giros3_euros_cp = '" + giros3_euros_cp.ToString().Replace(',', '.') + "', " +
                               " tb_importacion_datos.giros3_tc_orona = '" + giros3_tc_orona.ToString().Replace(',', '.') + "', " +
                               " tb_importacion_datos.giros3_dolares = '" + giros3_dolares.ToString().Replace(',', '.') + "' " +                               
                               " where " +
                               " tb_importacion_datos.codequipo = " + codigoEquipo;
           bool bandera = conx.ejecutarMySql(consulta);
           return bandera;
       }


       public DataSet buscar_ProrrateoCostosGeneral_JYCIA(string Edificio, string exbo, string estado, bool todosLosDatos, string semana_expedicion, string Dui, string Contenedor)
       {
           List<string> listBaseDatos = NA_VariablesGlobales.listBaseDatos;
           string consulta = "";
           for (int i = 0; i < listBaseDatos.Count; i++)
           {
               string baseDatos = listBaseDatos[i];
               string UNE;
               string Ciudad;
               if (baseDatos.Equals("db_seguimientoscz_jyc"))
               {
                   UNE = "Interlogi";
                   Ciudad = "SCZ";
               }
               else
                   if (baseDatos.Equals("db_seguimientocbba_jyc"))
                   {
                       UNE = "Melevar";
                       Ciudad = "CBBA";
                   }
                   else
                       if (baseDatos.Equals("db_seguimientolpz_jyc"))
                       {
                           UNE = "Elevamerica";
                           Ciudad = "LPZ";
                       }
                       else
                           if (baseDatos.Equals("db_seguimientosucre_jyc"))
                           {
                               UNE = "JYC";
                               Ciudad = "Sucre";
                           }
                           else
                               if (baseDatos.Equals("db_seguimientooruro_jyc"))
                               {
                                   UNE = "JYC";
                                   Ciudad = "Oruro";
                               }
                               else
                                   if (baseDatos.Equals("db_seguimientobeni_jyc"))
                                   {
                                       UNE = "JYC";
                                       Ciudad = "Beni";
                                   }
                                   else
                                       if (baseDatos.Equals("db_seguimientopando_jyc"))
                                       {
                                           UNE = "JYC";
                                           Ciudad = "Pando";
                                       }
                                       else
                                           if (baseDatos.Equals("db_seguimientotarija_jyc"))
                                           {
                                               UNE = "JYC";
                                               Ciudad = "Tarija";
                                           }
                                           else
                                               if (baseDatos.Equals("db_seguimientoyacuiba_jyc"))
                                               {
                                                   UNE = "JYC";
                                                   Ciudad = "Yacuiba";
                                               }
                                               else
                                                   if (baseDatos.Equals("db_seguimientopotosi_jyc"))
                                                   {
                                                       UNE = "JYC";
                                                       Ciudad = "Potosi";
                                                   }
                                                   else
                                                       if (baseDatos.Equals("db_seguimientovillamontes_jyc"))
                                                       {
                                                           UNE = "JYC";
                                                           Ciudad = "Villamontes";
                                                       }
                                                       else
                                                           if (baseDatos.Equals("db_seguimientoparaguay_nuevo"))
                                                           {
                                                               UNE = "JYC";
                                                               Ciudad = "Paraguay";
                                                           }
                                                           else
                                                           {
                                                               UNE = "JYC";
                                                               Ciudad = "Otro";
                                                           }
               if (i > 0)
                   consulta = consulta + " UNION ";


               consulta = consulta + "select " +
                               " eq.codigo,  " +
                               " eq.exbo, " +
                               " pp.nombre as 'Edificio',  " +
                               " eq.vendidoenciudad as 'Vendido',  " +
                               " eq.instaladoenciudad as 'Instalado', " +
                               " '" + UNE + "' AS 'UNE',  " +
                               " '" + Ciudad + "' as 'CIUDAD',  " +
                               " eeq.nombre as 'Estado1' , " +
                               " date_format(id.fechaaproximadoarribopuerto,'%d/%m/%Y') as 'fechaaproximadoarribopuerto',  " +
                               " id.consignatario as 'Consignatario', " +                               
                               " id.semana_expedicion, "+
                               " id.codnrodui, "+
                               " id.codnrocontenedor, "+
                               " id.pct_tamanio_contenedor, "+
                               " id.nro_bl, "+
                               " id.pct_peso, "+
                               " ( "+
                               " id.pct_costo_transporte "+
                               " * "+
                               " ( "+
                               " (id.pct_peso * 100) "+
                               " /  "+
                               " (select sum(id1.pct_peso) from tb_importacion_datos id1 where id1.codnrocontenedor = id.codnrocontenedor) "+
                               "  )/100 "+
                               "  ) "+
                               "  as 'montodolares_x_aplicarproyecto', "+
                               " (id.giros1_dolares+id.giros2_dolares+id.giros3_dolares) as 'valorgiradodelequipo', "+
                               " ( "+
                               " ((id.giros1_dolares+id.giros2_dolares+id.giros3_dolares)*100) "+
                               " / "+
                               " (select  "+
                               " sum(id2.giros1_dolares+id2.giros2_dolares+id2.giros3_dolares)  "+
                               " from tb_importacion_datos id2 where id2.codnrocontenedor = id.codnrocontenedor) "+
                               " ) "+
                               " as 'porcentajeImportedegirodelequipo', "+
                               " ( "+
                               " id.pct_costo_transporte "+
                               " * "+
                               " (( "+
                               " ((id.giros1_dolares+id.giros2_dolares+id.giros3_dolares)*100) "+
                               " / "+
                               " (select  "+
                               " sum(id3.giros1_dolares+id3.giros2_dolares+id3.giros3_dolares)  "+
                               " from tb_importacion_datos id3 where id3.codnrocontenedor = id.codnrocontenedor) "+
                               " )/100) "+
                               " ) "+
                               "  as 'prorrateodetransporte', "+
                               " id.pct_costo_transporte, "+
                               " id.pct_costo_internacional, "+
                               " id.pct_costo_nacional, "+
                               " date_format(id.pct_fechapagarproveedor,'%d/%m/%Y') as 'pct_fechapagarproveedor', "+
                               " id.pct_proveedor, "+
                               " id.pct_nrofactura " +
                               " from  " +
                               baseDatos + ".tb_proyecto pp, " +
                               baseDatos + ".tb_equipo eq " +
                               " LEFT JOIN " + baseDatos + ".tb_importacion_datos id ON (eq.codimportacion = id.codigo) , " +
                               baseDatos + ".tb_fechaestadoequipo fe, " + baseDatos + ".tb_estado_equipo eeq " +
                               " where  " +
                               " eq.cod_proyecto = pp.codigo and " +
                               " eq.codfechaestadoequipo = fe.codigo and " +
                               " fe.codEstadoEquipo = eeq.codigo ";
               //" and fe.codEstadoEquipo not in (6,7,8,9,10,21) ";
               if (!Edificio.Equals(""))
               {
                   consulta = consulta + " and pp.nombre like '%" + Edificio + "%' ";
               }
               if (!exbo.Equals(""))
               {
                   consulta = consulta + " and eq.exbo like '%" + exbo + "%' ";
               }
               if (!estado.Equals(""))
               {
                   consulta = consulta + " and eeq.nombre = '" + estado + "'";
               }
               if (!semana_expedicion.Equals(""))
               {
                   consulta = consulta + " and id.semana_expedicion = '" + semana_expedicion + "'";
               }

               if (!Dui.Equals(""))
               {
                   consulta = consulta + " and id.codnrodui = '" + Dui + "'";
               }
               if (!Contenedor.Equals(""))
               {
                   consulta = consulta + " and id.codnrocontenedor = '" + Contenedor + "'";
               }

               if (todosLosDatos == false)
               {
                   consulta = consulta + " limit 100";
               }

           }
           return conexion.consultaMySql(consulta);
       }


       internal bool actualizar_ProrrateoCostos_General(int codigoEquipo, string Ciudad,
           string codnrodui, string codnrocontenedor , string semana_expedicion,
           string pct_tamanio_contenedor, float pct_peso, float pct_costo_transporte, float pct_costo_internacional,
           float pct_costo_nacional, string pct_fechapagarproveedor,string nro_Hbl)
       {
           string baseDatos = "";

           if (Ciudad.Equals("SCZ"))
           {
               baseDatos = "db_SantaCruz";
           }
           else
               if (Ciudad.Equals("CBBA"))
               {
                   baseDatos = "db_Cochabamba";
               }
               else
                   if (Ciudad.Equals("LPZ"))
                   {
                       baseDatos = "db_LaPaz";
                   }
                   else
                       if (Ciudad.Equals("Sucre"))
                       {
                           baseDatos = "db_Sucre";
                       }
                       else
                           if (Ciudad.Equals("Oruro"))
                           {
                               baseDatos = "db_Oruro";
                           }
                           else
                               if (Ciudad.Equals("Beni"))
                               {
                                   baseDatos = "db_beni";
                               }
                               else
                                   if (Ciudad.Equals("Pando"))
                                   {
                                       baseDatos = "db_pando";
                                   }
                                   else
                                       if (Ciudad.Equals("Tarija"))
                                       {
                                           baseDatos = "db_Tarija";
                                       }
                                       else
                                           if (Ciudad.Equals("Yacuiba"))
                                           {
                                               baseDatos = "db_Yacuiba";
                                           }
                                           else
                                               if (Ciudad.Equals("Potosi"))
                                               {
                                                   baseDatos = "db_Potosi";
                                               }
                                               else
                                                   if (Ciudad.Equals("Villamontes"))
                                                   {
                                                       baseDatos = "db_Villamontes";
                                                   }
                                                   else
                                                       if (Ciudad.Equals("Paraguay"))
                                                       {
                                                           baseDatos = "db_ParaguayNuevo";
                                                       }
                                                       else
                                                       {
                                                           baseDatos = "db_prueba";
                                                       }
           conexionMySql conx = new conexionMySql(baseDatos);

 
           string consulta = "update tb_importacion_datos set " +
                               " tb_importacion_datos.codnrodui = '" + codnrodui + "', " +
                               " tb_importacion_datos.codnrocontenedor = '" + codnrocontenedor + "', " +
                               " tb_importacion_datos.semana_expedicion = '" + semana_expedicion + "', " +
                               " tb_importacion_datos.pct_tamanio_contenedor = '" + pct_tamanio_contenedor + "', " +
                               " tb_importacion_datos.pct_peso = '" + pct_peso.ToString().Replace(',', '.') + "', " +
                               " tb_importacion_datos.pct_costo_transporte = '" + pct_costo_transporte.ToString().Replace(',', '.') + "', " +
                               " tb_importacion_datos.pct_costo_internacional = '" + pct_costo_internacional.ToString().Replace(',', '.') + "', " +
                               " tb_importacion_datos.pct_costo_nacional = '" + pct_costo_nacional.ToString().Replace(',', '.') + "', " +
                               " tb_importacion_datos.pct_fechapagarproveedor = " + pct_fechapagarproveedor +","+                         
                               " tb_importacion_datos.nro_bl = '"+nro_Hbl+"'"+
                               " where " +
                               " tb_importacion_datos.codequipo = " + codigoEquipo;
           bool bandera = conx.ejecutarMySql(consulta);
           return bandera;
       }


       public DataSet buscar_ProrrateoCostosGeneral_Polizas(bool todosLosDatos, string Dui, int codEquipo)
       {
           List<string> listBaseDatos = NA_VariablesGlobales.listBaseDatos;
           string consulta = "";
           for (int i = 0; i < listBaseDatos.Count; i++)
           {
               string baseDatos = listBaseDatos[i];
               string UNE;
               string Ciudad;
               if (baseDatos.Equals("db_seguimientoscz_jyc"))
               {
                   UNE = "Interlogi";
                   Ciudad = "SCZ";
               }
               else
                   if (baseDatos.Equals("db_seguimientocbba_jyc"))
                   {
                       UNE = "Melevar";
                       Ciudad = "CBBA";
                   }
                   else
                       if (baseDatos.Equals("db_seguimientolpz_jyc"))
                       {
                           UNE = "Elevamerica";
                           Ciudad = "LPZ";
                       }
                       else
                           if (baseDatos.Equals("db_seguimientosucre_jyc"))
                           {
                               UNE = "JYC";
                               Ciudad = "Sucre";
                           }
                           else
                               if (baseDatos.Equals("db_seguimientooruro_jyc"))
                               {
                                   UNE = "JYC";
                                   Ciudad = "Oruro";
                               }
                               else
                                   if (baseDatos.Equals("db_seguimientobeni_jyc"))
                                   {
                                       UNE = "JYC";
                                       Ciudad = "Beni";
                                   }
                                   else
                                       if (baseDatos.Equals("db_seguimientopando_jyc"))
                                       {
                                           UNE = "JYC";
                                           Ciudad = "Pando";
                                       }
                                       else
                                           if (baseDatos.Equals("db_seguimientotarija_jyc"))
                                           {
                                               UNE = "JYC";
                                               Ciudad = "Tarija";
                                           }
                                           else
                                               if (baseDatos.Equals("db_seguimientoyacuiba_jyc"))
                                               {
                                                   UNE = "JYC";
                                                   Ciudad = "Yacuiba";
                                               }
                                               else
                                                   if (baseDatos.Equals("db_seguimientopotosi_jyc"))
                                                   {
                                                       UNE = "JYC";
                                                       Ciudad = "Potosi";
                                                   }
                                                   else
                                                       if (baseDatos.Equals("db_seguimientovillamontes_jyc"))
                                                       {
                                                           UNE = "JYC";
                                                           Ciudad = "Villamontes";
                                                       }
                                                       else
                                                           if (baseDatos.Equals("db_seguimientoparaguay_nuevo"))
                                                           {
                                                               UNE = "JYC";
                                                               Ciudad = "Paraguay";
                                                           }
                                                           else
                                                           {
                                                               UNE = "JYC";
                                                               Ciudad = "Otro";
                                                           }
               if (i > 0)
                   consulta = consulta + " UNION ";


               consulta = consulta + "select " +
                                " eq.codigo,  " +
                               " eq.exbo, " +
                               " pp.nombre as 'Edificio',  " +
                               " eq.vendidoenciudad as 'Vendido',  " +
                               " eq.instaladoenciudad as 'Instalado', " +
                               " '" + UNE + "' AS 'UNE',  " +
                               " '" + Ciudad + "' as 'CIUDAD',  " +
                               " eeq.nombre as 'Estado1' , " +
                               " date_format(id.fechaaproximadoarribopuerto,'%d/%m/%Y') as 'fechaaproximadoarribopuerto',  " +
                               " id.consignatario as 'Consignatario', " +
                               " id.semana_expedicion, " +
                               " id.codnrodui, " +
                               " id.codnrocontenedor, " +
                               " id.pct_tamanio_contenedor, " +
                               " id.nro_bl, " +
                               " id.pct_peso, " +
                               " ( " +
                               " id.pct_costo_transporte " +
                               " * " +
                               " ( " +
                               " (id.pct_peso * 100) " +
                               " /  " +
                               " (select sum(id1.pct_peso) from " + baseDatos + ".tb_importacion_datos id1 where id1.codnrocontenedor = id.codnrocontenedor) " +
                               "  )/100 " +
                               "  ) " +
                               "  as 'montodolares_x_aplicarproyecto', " +
                               " (id.giros1_dolares+id.giros2_dolares+id.giros3_dolares) as 'valorgiradodelequipo', " +
                               " ( " +
                               " ((id.giros1_dolares+id.giros2_dolares+id.giros3_dolares)*100) " +
                               " / " +
                               " (select  " +
                               " sum(id2.giros1_dolares+id2.giros2_dolares+id2.giros3_dolares)  " +
                               " from " + baseDatos + ".tb_importacion_datos id2 where id2.codnrocontenedor = id.codnrocontenedor) " +
                               " ) " +
                               " as 'porcentajeImportedegirodelequipo', " +
                               " ( " +
                               " id.pct_costo_transporte " +
                               " * " +
                               " (( " +
                               " ((id.giros1_dolares+id.giros2_dolares+id.giros3_dolares)*100) " +
                               " / " +
                               " (select  " +
                               " sum(id3.giros1_dolares+id3.giros2_dolares+id3.giros3_dolares)  " +
                               " from " + baseDatos + ".tb_importacion_datos id3 where id3.codnrocontenedor = id.codnrocontenedor) " +
                               " )/100) " +
                               " ) " +
                               "  as 'prorrateodetransporte', " +
                               " id.pct_costo_transporte, " +
                               " id.pct_costo_internacional, " +
                               " id.pct_costo_nacional, " +
                               " date_format(id.pct_fechapagarproveedor,'%d/%m/%Y') as 'pct_fechapagarproveedor', " +
                               " id.pct_proveedor, " +
                               " id.pct_nrofactura " +
                               " from  " +
                               baseDatos + ".tb_proyecto pp, " +
                               baseDatos + ".tb_equipo eq " +
                               " LEFT JOIN " + baseDatos + ".tb_importacion_datos id ON (eq.codimportacion = id.codigo) , " +
                               baseDatos + ".tb_fechaestadoequipo fe, " + baseDatos + ".tb_estado_equipo eeq " +
                               " where  " +
                               " eq.cod_proyecto = pp.codigo and " +
                               " eq.codfechaestadoequipo = fe.codigo and " +
                               " fe.codEstadoEquipo = eeq.codigo ";
               //" and fe.codEstadoEquipo not in (6,7,8,9,10,21) ";

               if (!Dui.Equals(""))
               {
                   consulta = consulta + " and id.codnrodui like '%" + Dui + "%'";
               } 
               if(codEquipo > 0){
                   consulta = consulta + " and eq.codigo = '" + codEquipo + "'";
               }

               if (todosLosDatos == false)
               {
                   consulta = consulta + " limit 100";
               }

           }
           return conexion.consultaMySql(consulta);
       }



       public DataSet buscar_ProrrateoCostosGeneralTransporte_JyCIA(bool todosLosDatos, string Edificio, string exbo, string semanaExp, string Dui, string Contenedor)
       {
           NA_Importacion nimp = new NA_Importacion();
           DataSet datosTotal = nimp.get_datoImportacionContenedorTotal( todosLosDatos,  Edificio,  exbo,  semanaExp ,  Dui,  Contenedor);
           DataSet ds = new DataSet();
           DataTable dt = new DataTable();
           ds.Tables.Add(dt);
           dt.Columns.Add("codigo", typeof(string));
           dt.Columns.Add("exbo", typeof(string));
           dt.Columns.Add("Edificio", typeof(string));
           dt.Columns.Add("Vendido", typeof(string));
           dt.Columns.Add("Instalado", typeof(string));
           dt.Columns.Add("UNE", typeof(string));
           dt.Columns.Add("CIUDAD", typeof(string));
           dt.Columns.Add("Estado1", typeof(string));
           dt.Columns.Add("fechaaproximadoarribopuerto", typeof(string));
           dt.Columns.Add("Consignatario", typeof(string));
           dt.Columns.Add("semana_expedicion", typeof(string));
           dt.Columns.Add("codnrodui", typeof(string));
           dt.Columns.Add("codnrocontenedor", typeof(string));
           dt.Columns.Add("pct_tamanio_contenedor", typeof(string));
           dt.Columns.Add("nro_bl", typeof(string));
           dt.Columns.Add("pct_peso", typeof(string));
           dt.Columns.Add("montodolares_x_aplicarproyecto", typeof(string));
           dt.Columns.Add("valorgiradodelequipo", typeof(string));
           dt.Columns.Add("porcentajeImportedegirodelequipo", typeof(string));
           dt.Columns.Add("prorrateodetransporte", typeof(string));
           dt.Columns.Add("pct_costo_transporte", typeof(string));
           dt.Columns.Add("pct_costo_internacional", typeof(string));
           dt.Columns.Add("pct_costo_nacional", typeof(string));
           dt.Columns.Add("pct_fechapagarproveedor", typeof(string));
           
           for (int i = 0; i < datosTotal.Tables[0].Rows.Count; i++ )
           {
                   DataRow dtRow = dt.NewRow();

                   dtRow["codigo"] = datosTotal.Tables[0].Rows[i][0].ToString();
                   dtRow["exbo"] = datosTotal.Tables[0].Rows[i][1].ToString();
                   dtRow["Edificio"] = datosTotal.Tables[0].Rows[i][2].ToString();
                   dtRow["Vendido"] = datosTotal.Tables[0].Rows[i][3].ToString();
                   dtRow["Instalado"] = datosTotal.Tables[0].Rows[i][4].ToString();
                   dtRow["UNE"] = datosTotal.Tables[0].Rows[i][5].ToString();
                   dtRow["CIUDAD"] = datosTotal.Tables[0].Rows[i][6].ToString();
                   dtRow["Estado1"] = datosTotal.Tables[0].Rows[i][7].ToString();
                   dtRow["fechaaproximadoarribopuerto"] = datosTotal.Tables[0].Rows[i][8].ToString();
                   dtRow["Consignatario"] = datosTotal.Tables[0].Rows[i][9].ToString();
                   dtRow["semana_expedicion"] = datosTotal.Tables[0].Rows[i][10].ToString();
                   dtRow["codnrodui"] = datosTotal.Tables[0].Rows[i][11].ToString();
                   dtRow["codnrocontenedor"] = datosTotal.Tables[0].Rows[i][12].ToString();
                   dtRow["pct_tamanio_contenedor"] = datosTotal.Tables[0].Rows[i][13].ToString();
                   dtRow["nro_bl"] = datosTotal.Tables[0].Rows[i][14].ToString();
                   dtRow["pct_peso"] = datosTotal.Tables[0].Rows[i][15].ToString();
                   dtRow["valorgiradodelequipo"] = datosTotal.Tables[0].Rows[i][16].ToString();
                   dtRow["pct_costo_transporte"] = datosTotal.Tables[0].Rows[i][17].ToString();
                   dtRow["pct_costo_internacional"] = datosTotal.Tables[0].Rows[i][18].ToString();
                   dtRow["pct_costo_nacional"] = datosTotal.Tables[0].Rows[i][19].ToString();
                   dtRow["pct_fechapagarproveedor"] = datosTotal.Tables[0].Rows[i][20].ToString();
                   
                   string contenedorAux = datosTotal.Tables[0].Rows[i][12].ToString();

                   if (!contenedorAux.Equals(""))
                   {
                       float pesoTotal = nimp.get_PesoTotalContenedor(contenedorAux);
                       float pesoEquipo;
                       float.TryParse(datosTotal.Tables[0].Rows[i][15].ToString(), out pesoEquipo);
                       float porcentajePesodelContendor = ((pesoEquipo * 100) / (pesoTotal));

                       float costoTransporte;
                       float.TryParse(datosTotal.Tables[0].Rows[i][17].ToString(), out costoTransporte);

                       float montoSUSxAplicarProyecto = ((porcentajePesodelContendor / 100) * (costoTransporte));

                       dtRow["montodolares_x_aplicarproyecto"] = montoSUSxAplicarProyecto.ToString();

                       float valorGiradoEquipo;
                       float.TryParse(datosTotal.Tables[0].Rows[i][16].ToString(), out valorGiradoEquipo);
                       float TotalGirosContenedor = nimp.get_TotalGirosContenedor(contenedorAux);

                       float porcentajeImportedeGiroEquipo = ((valorGiradoEquipo * 100) / (TotalGirosContenedor));
                       dtRow["porcentajeImportedegirodelequipo"] = porcentajeImportedeGiroEquipo.ToString();

                       float prorrateoCostoTransporte = ((porcentajeImportedeGiroEquipo / 100) * costoTransporte);
                       dtRow["prorrateodetransporte"] = prorrateoCostoTransporte.ToString();
                   }
                   else {
                       dtRow["montodolares_x_aplicarproyecto"] = "0";
                       dtRow["porcentajeImportedegirodelequipo"] = "0";
                       dtRow["prorrateodetransporte"] = "0";
                   }

                   

                    dt.Rows.Add(dtRow);
           }

           return ds;
       }

       public int cant_ProrrateoCostosGeneralTransporte_JyCIA(bool todosLosDatos, string Edificio, string exbo, string semanaExp, string Dui, string Contenedor)
       {           
           DataSet datosTotal = buscar_ProrrateoCostosGeneralTransporte_JyCIA( todosLosDatos,  Edificio,  exbo,  semanaExp,  Dui,  Contenedor);
           return datosTotal.Tables[0].Rows.Count;
       }


       public DataSet buscar_FacturaContenedorGeneral_JyCIA(bool todosLosDatos, string Edificio, string exbo, string semanaExp, string Dui, string Contenedor)
       {
           NA_Importacion nimp = new NA_Importacion();
           DataSet datosTotal = nimp.get_FacturaContenedorGeneral(todosLosDatos, Edificio, exbo, semanaExp, Dui, Contenedor);
           DataSet ds = new DataSet();
           DataTable dt = new DataTable();
           ds.Tables.Add(dt);
           dt.Columns.Add("Codigo", typeof(string));
           dt.Columns.Add("Exbo", typeof(string));
           dt.Columns.Add("Edificio", typeof(string));
           dt.Columns.Add("Vendido", typeof(string));
           dt.Columns.Add("Instalado", typeof(string));
           dt.Columns.Add("UNE", typeof(string));
           dt.Columns.Add("Ciudad", typeof(string));
           dt.Columns.Add("Estado", typeof(string));           
           dt.Columns.Add("Consignatario", typeof(string));
           dt.Columns.Add("Semana Expedicion", typeof(string));
           dt.Columns.Add("DUI", typeof(string));
           dt.Columns.Add("Contenedor", typeof(string));
           dt.Columns.Add("Importe USD", typeof(string));   
           dt.Columns.Add("Porcentaje", typeof(string));   
           dt.Columns.Add("MSC", typeof(string));   
           dt.Columns.Add("ASP-B BS", typeof(string));   
           dt.Columns.Add("THC Destino Khuene+Negel USD", typeof(string));
           dt.Columns.Add("# Fact MSC", typeof(string));

           dt.Columns.Add("Costo MSC 87%", typeof(string));
           dt.Columns.Add("Prorrateo por Equipo MSC", typeof(string));
           dt.Columns.Add("Prorrateo por Equipo ASPB", typeof(string));
           dt.Columns.Add("Prorrateo por Equipo THC", typeof(string));   

           for (int i = 0; i < datosTotal.Tables[0].Rows.Count; i++)
           {
               DataRow dtRow = dt.NewRow();

               dtRow["Codigo"] = datosTotal.Tables[0].Rows[i][0].ToString();
               dtRow["Exbo"] = datosTotal.Tables[0].Rows[i][1].ToString();
               dtRow["Edificio"] = datosTotal.Tables[0].Rows[i][2].ToString();
               dtRow["Vendido"] = datosTotal.Tables[0].Rows[i][3].ToString();
               dtRow["Instalado"] = datosTotal.Tables[0].Rows[i][4].ToString();
               dtRow["UNE"] = datosTotal.Tables[0].Rows[i][5].ToString();
               dtRow["Ciudad"] = datosTotal.Tables[0].Rows[i][6].ToString();
               dtRow["Estado"] = datosTotal.Tables[0].Rows[i][7].ToString();               
               dtRow["Consignatario"] = datosTotal.Tables[0].Rows[i][8].ToString();
               dtRow["Semana Expedicion"] = datosTotal.Tables[0].Rows[i][9].ToString();
               dtRow["DUI"] = datosTotal.Tables[0].Rows[i][10].ToString();
               dtRow["Contenedor"] = datosTotal.Tables[0].Rows[i][11].ToString();

               dtRow["Importe USD"] = datosTotal.Tables[0].Rows[i][12].ToString();
               
               dtRow["MSC"] = datosTotal.Tables[0].Rows[i][13].ToString();
               dtRow["ASP-B BS"] = datosTotal.Tables[0].Rows[i][14].ToString();
               dtRow["THC Destino Khuene+Negel USD"] = datosTotal.Tables[0].Rows[i][15].ToString();
               dtRow["# Fact MSC"] = datosTotal.Tables[0].Rows[i][16].ToString();              


               string contenedorAux = datosTotal.Tables[0].Rows[i][11].ToString();

               if (!contenedorAux.Equals(""))
               {
                   float valorGiradoEquipo;
                   float.TryParse(datosTotal.Tables[0].Rows[i][12].ToString(), out valorGiradoEquipo);
                   float TotalGirosContenedor = nimp.get_TotalGirosContenedor(contenedorAux);

                   float porcentajeImportedeGiroEquipo = ((valorGiradoEquipo * 100) / (TotalGirosContenedor));
                   dtRow["Porcentaje"] = porcentajeImportedeGiroEquipo.ToString();    
                   
                   float valorfacturaMSC;
                   float.TryParse(datosTotal.Tables[0].Rows[i][13].ToString().Replace('.', ','), out valorfacturaMSC);
                   float porcentaje87 = float.Parse("0,87");
                   float costoMSC87 = (valorfacturaMSC * porcentaje87);
                   dtRow["Costo MSC 87%"] = costoMSC87.ToString();

                   float prorrateo_por_equipo_MSC = (costoMSC87 * (porcentajeImportedeGiroEquipo/100));
                   dtRow["Prorrateo por Equipo MSC"] = prorrateo_por_equipo_MSC.ToString();

                   float montoASPB;
                   float.TryParse(datosTotal.Tables[0].Rows[i][14].ToString(), out montoASPB);
                   float prorrateo_por_equipo_ASPB = (montoASPB * (porcentajeImportedeGiroEquipo/100));
                   dtRow["Prorrateo por Equipo ASPB"] = prorrateo_por_equipo_ASPB.ToString();

                   float montoTHC;
                   float.TryParse(datosTotal.Tables[0].Rows[i][15].ToString(), out montoTHC);
                   float prorrateo_por_equipo_THC = (montoTHC * (porcentajeImportedeGiroEquipo/100));
                   dtRow["Prorrateo por Equipo THC"] = prorrateo_por_equipo_THC.ToString();                                      
               }
               else
               {
                   dtRow["Porcentaje"] = "0";
                   dtRow["Costo MSC 87%"] = "0";
                   dtRow["Prorrateo por Equipo MSC"] = "0";
                   dtRow["Prorrateo por Equipo ASPB"] = "0";
                   dtRow["Prorrateo por Equipo THC"] = "0";                   
               }



               dt.Rows.Add(dtRow);
           }

           return ds;
       }



       internal DataSet getClienteCodSimec(int codigoCobro)
       {
           string consulta = "select re.codcobranza, eq.clicodigo, eq.monedaprevision_simec " +
                               " from tb_recibopago re, tb_seguimiento seg, tb_equipo eq "+
                               " where "+ 
                               " re.codseg = seg.codigo and "+
                               " seg.cod_equipo = eq.codigo and "+
                               " re.codcobranza > 0 and "+
                               " re.codcobranza = '"+codigoCobro+"'";
           return conexion.consultaMySql(consulta);
       }
    }
}