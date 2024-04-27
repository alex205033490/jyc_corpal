using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_ActivosJyC
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DA_ActivosJyC() { }



        internal bool update_Activos(int codigoActivo, int codUserRespAsignado, int codUser, int CodestadoActivo, bool noAplica, float montoValornoAplica, float tipocambio, string ubicacion_une, bool bajaactivo, string observaciones)
        {
            string codigoEstadoAux = "null";
            if (CodestadoActivo > 0)
            {
                codigoEstadoAux = CodestadoActivo.ToString();
            }

            string codUserRespAsignadoAux = "null";
            if (codUserRespAsignado > 0)
            {
                codUserRespAsignadoAux = codUserRespAsignado.ToString();
            }

            string consulta = "";
            if (noAplica == false)
            {
                consulta = "update tb_activosjyc, tb_estadoactivos_simec set " +
                               " tb_activosjyc.coduser_custodio = " + codUserRespAsignadoAux + " , " +
                               " tb_activosjyc.fechaasig = current_date(), " +
                               " tb_activosjyc.horaasig = current_time(), " +
                               " tb_activosjyc.coduser_asigpor =  " + codUser + " , " +
                               " tb_activosjyc.cod_estado = " + codigoEstadoAux + ", " +
                               " tb_activosjyc.ubicacion_une = '" + ubicacion_une + "', " +
                               " tb_activosjyc.ValorActivo_BS = TRUNCATE(ifnull((tb_activosjyc.valoractualbs * (tb_estadoactivos_simec.valorporcentual/100)),0),2), " +
                               " tb_activosjyc.ValorActivo_SuS = TRUNCATE(ifnull((tb_activosjyc.valoractualsus * (tb_estadoactivos_simec.valorporcentual/100)),0),2)," +
                               " tb_activosjyc.bajaactivo = " + bajaactivo + " , " +
                               " tb_activosjyc.observaciones = '" + observaciones + "' " +
                               " where  " +
                               " tb_estadoactivos_simec.codigo = " + CodestadoActivo + " and " +
                               " tb_activosjyc.codigo = " + codigoActivo;
            }
            else
            {
                float montoValornoAplicaBS = (montoValornoAplica * tipocambio);
                consulta = "update tb_activosjyc set " +
                               " tb_activosjyc.coduser_custodio = " + codUserRespAsignadoAux + " , " +
                               " tb_activosjyc.coduser_asig = " + codUserRespAsignadoAux + " , " +
                               " tb_activosjyc.fechaasig = current_date(), " +
                               " tb_activosjyc.horaasig = current_time(), " +
                               " tb_activosjyc.coduser_asigpor =  " + codUser + " , " +
                               " tb_activosjyc.cod_estado = null, " +
                               " tb_activosjyc.ubicacion_une = '" + ubicacion_une + "', " +
                               " tb_activosjyc.noaplica = 1 , " +
                               " tb_activosjyc.ValorActivo_BS = '" + montoValornoAplicaBS.ToString().Replace(',', '.') + "', " +
                               " tb_activosjyc.ValorActivo_SuS = '" + montoValornoAplica.ToString().Replace(',', '.') + "', " +
                               " tb_activosjyc.bajaactivo = " + bajaactivo + " , " +
                               " tb_activosjyc.observaciones = '" + observaciones + "' " +
                               " where  " +
                               " tb_activosjyc.codigo = " + codigoActivo;
            }

            return ConecRes.ejecutarMySql(consulta);
        }

        internal DataSet get_allActivos(string detalleActivo, string comprobante, string NombreCustodio, string responsableAsignado, string estadoActivo, string EstadoValorActual)
        {
            string consulta = "select "+
                               " aa.codigo, "+ 
                               " ifnull(ee.detalle,'Sin Estado') as 'Estado1', "+
                               " aa.cuenta,aa.descripcion,aa.comprobante, date_format(aa.fechacompra,'%d/%m/%Y') as 'fecha_Compra', "+
                               " aa.tipo,aa.ubicacion,aa.custodio,aa.nombrecustodio,aa.var1,aa.Proyecto,aa.var2,aa.financia, "+
                               " res.nombre as 'Personal_Custodio', "+
                               " aa.vida,aa.valoractualbs as 'valorActualbs_Simec',aa.valoractualsus as 'Valoractualsus_Simec', "+
                               " aa.ValorActivo_BS, aa.ValorActivo_SuS, "+
                               " aa.Tipo_ValorActual, aa.ubicacion_une, "+
                               " aa.bajaactivo , aa.observaciones, aa.noaplica, aa.insertardatos " +
                              // " TRUNCATE(ifnull((aa.valoractualbs * (ee.valorporcentual/100)),0),2) as 'ValorActivo_BS', "+
                              // " TRUNCATE(ifnull((aa.valoractualsus * (ee.valorporcentual/100)),0),2) as 'ValorActivo_SuS' "+
                               " ,date_format(aa.fechabaja,'%d/%m/%Y') as 'fecha_baja' " +
                               " ,aa.codigoactivo,if(aa.bajacodigo ='s','Baja', 'Vigente') as 'EstadoBaja' " +
                               " from tb_activosjyc aa "+                               
                               " left join  tb_estadoactivos_simec ee on (aa.cod_estado = ee.codigo) "+
                               " left join tb_responsable res on (aa.coduser_custodio = res.codigo) " +
                               " where "+
                               " aa.descripcion like '%"+detalleActivo+"%' ";
            if(string.IsNullOrEmpty(comprobante) == false){
                consulta = consulta + " and aa.comprobante like '%"+comprobante+"%'";
            }
            if (string.IsNullOrEmpty(NombreCustodio) == false)
            {
                consulta = consulta + " and aa.nombrecustodio like '%" + NombreCustodio + "%'";
            }
             if (string.IsNullOrEmpty(estadoActivo) == false && !estadoActivo.Equals("Sin Estado"))
             {
                consulta = consulta + " and ee.detalle like '%"+estadoActivo+"%' ";
            }                  
             
             if(string.IsNullOrEmpty(responsableAsignado) == false){
                consulta = consulta + " and res.nombre like '%"+responsableAsignado+"%'";
            }

             if (string.IsNullOrEmpty(EstadoValorActual) == false && !EstadoValorActual.Equals("Sin Estado"))
             {
                 consulta = consulta + " and aa.Tipo_ValorActual like '%" + EstadoValorActual + "%' ";
             }  
                              
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_estadosActivos()
        {
            string consulta = "select ee.codigo, ee.detalle as 'nombre' from tb_estadoactivos_simec ee where ee.estado = 1;";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_EstadoActivo(string estadoActivo)
        {
            string consulta = "select ee.codigo, ee.detalle, ee.valorporcentual, ee.estado from tb_estadoactivos_simec ee " +
                               " where ee.estado = 1 and ee.detalle = '" + estadoActivo + "'";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_estadosValorActual()
        {
            string consulta = "select aa.Tipo_ValorActual as 'nombre' from tb_activosjyc aa group by aa.Tipo_ValorActual";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet mostrarResponsableSimec(string nombreResponsable)
        {
            string consulta = "select aa.nombrecustodio as 'nombre' from tb_activosjyc aa where aa.nombrecustodio like '%" + nombreResponsable + "%' group by aa.nombrecustodio";
            return ConecRes.consultaMySql(consulta);
        }

        internal bool generarCodigosQR(string baseDatos)
        {
            string consulta = "update tb_activosjyc set " +
                               " tb_activosjyc.qr_activo = concat(ifnull(tb_activosjyc.codigoactivo,'Ninguno'),'|', " +
                               " tb_activosjyc.descripcion,'|', " +
                               " '" + baseDatos + "','|'," +
                               " tb_activosjyc.codigo)";
            return ConecRes.ejecutarMySql(consulta);
        }

        internal DataSet get_datos_QR(string activo)
        {
            string consulta = "select " +
                             " aa.codigo, " +
                             " aa.descripcion, " +
                             " aa.qr_activo ," +
                             " ifnull(aa.codigoactivo,'Ninguno') as 'codSimecActivo' " +
                             " from tb_activosjyc aa " +
                             " where " +
                             " aa.descripcion like '%" + activo + "%'";
            return ConecRes.consultaMySql(consulta);
        }

        internal DataSet get_direccionQRActivos(string ruta)
        {
            string consulta = "select " +
                                " aa.codigoactivo as 'codigo', " +
                                " aa.descripcion, " +
                                " aa.Tipo_ValorActual, " +
                                " CAST(concat('" + ruta + "',aa.codigo,'_',ifnull(aa.codigoactivo,'Ninguno'),'.jpg') AS CHAR) as 'DireccionArchivo' " +
                                " ,aa.codigo as 'Codigo2' "+
                                " from tb_activosjyc aa";
            return ConecRes.consultaMySql(consulta);
        }
    }
}