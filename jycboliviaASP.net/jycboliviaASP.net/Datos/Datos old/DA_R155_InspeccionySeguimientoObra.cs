using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace JyC_Exterior.Datos
{
    public class DA_R155_InspeccionySeguimientoObra
    {

        private conexionMySql ConecRes = new conexionMySql();
        public DA_R155_InspeccionySeguimientoObra() { }

        public bool insertar(string edificio ,
                                                   string direccion,
                                                   string faseinstalacion ,
                                                   string fosa_libredeescombros ,
                                                   string fosa_seca ,
                                                   string fosa_alturadeacuerdoalplanodemontaje ,
                                                   string hueco_retirodeelementosajenosalascensor ,
                                                   string hueco_ganchosdemontaje ,
                                                   string hueco_impermeabilizacioncubierta ,
                                                   string hueco_orificiosdesdehuecoalagaveta ,
                                                   string hueco_espacioparalagavetadecontrol ,
                                                   string hueco_huecosparafijaciondevigas ,
                                                   string hueco_pasoanivel ,
                                                   string hueco_puertaconchapainterior ,
                                                   string hueco_trampillaconcandadoochapa ,
                                                   string hueco_puntodeiluminacion ,
                                                   string hueco_reboqueinterior_1 ,
                                                   string hueco_barandillasdeseguridad ,
                                                   string hueco_huecosforjados ,
                                                   string hueco_escalerasiexistedesnivel ,
                                                   string hueco_empotradodecajadetermicos ,
                                                   string hueco_machones ,
                                                   string hueco_reboqueinterior_2 ,
                                                   string hueco_pieldevidrio ,
                                                   string hueco_pinturainteriordepuertasdepiso ,
                                                   string accesos_libredeelementosajenosalasc ,
                                                   string accesos_detalleyentodoslospisos ,
                                                   string accesos_niveldepisoterminado ,
                                                   string accesos_vigaparafijaciondepuertas ,
                                                   string accesos_ensotanogradaenaccesoascensor ,
                                                   string accesos_rematedepuertas ,
                                                   string accesos_rematedepisos ,
                                                   string accesos_pinturadepuertas ,
                                                   string accesos_fijaciondecajasparabotonera ,
                                                   string otros_depositoparaascensores ,
                                                   string otros_cuartomontadores ,
                                                   string otros_materialeselectricos ,
                                                   string otros_colocadopisoencabina ,
                                                   string otros_tensiondefuerzadefinitiva ,
                                                   string otros_aterramiento ,
                                                   string otros_murodeseparacionhuecoscontiguos ,
                                                   string presenciadeaguaenfosaycuartodemaquinas ,
                                                   string presenciadecablesoelementosajenosdentroelhueco ,
                                                   string observaciones ,
                                                   int codproy )
        {
            string codigoProyecto;
            if (codproy > 0)
            {
                codigoProyecto = codproy.ToString();
            }
            else
                codigoProyecto = "null";
            string consulta = "insert into tb_r155inspeccionyseguimientoobra(" +
                                "fechagra ," +
                                "horagra ," +
                                "fecha ," +
                                "hora ," +
                                "edificio ," +
                                "direccion ," +
                                "faseinstalacion ," +
                                "fosa_libredeescombros ," +
                                "fosa_seca ," +
                                "fosa_alturadeacuerdoalplanodemontaje ," +
                                "hueco_retirodeelementosajenosalascensor ," +
                                "hueco_ganchosdemontaje ," +
                                "hueco_impermeabilizacioncubierta ," +
                                "hueco_orificiosdesdehuecoalagaveta ," +
                                "hueco_espacioparalagavetadecontrol ," +
                                "hueco_huecosparafijaciondevigas ," +
                                "hueco_pasoanivel ," +
                                "hueco_puertaconchapainterior ," +
                                "hueco_trampillaconcandadoochapa ," +
                                "hueco_puntodeiluminacion ," +
                                "hueco_reboqueinterior_1 ," +
                                "hueco_barandillasdeseguridad ," +
                                "hueco_huecosforjados ," +
                                "hueco_escalerasiexistedesnivel ," +
                                "hueco_empotradodecajadetermicos ," +
                                "hueco_machones ," +
                                "hueco_reboqueinterior_2 ," +
                                "hueco_pieldevidrio ," +
                                "hueco_pinturainteriordepuertasdepiso ," +
                                "accesos_libredeelementosajenosalasc ," +
                                "accesos_detalleyentodoslospisos ," +
                                "accesos_niveldepisoterminado ," +
                                "accesos_vigaparafijaciondepuertas ," +
                                "accesos_ensotanogradaenaccesoascensor ," +
                                "accesos_rematedepuertas ," +
                                "accesos_rematedepisos ," +
                                "accesos_pinturadepuertas ," +
                                "accesos_fijaciondecajasparabotonera ," +
                                "otros_depositoparaascensores ," +
                                "otros_cuartomontadores ," +
                                "otros_materialeselectricos ," +
                                "otros_colocadopisoencabina ," +
                                "otros_tensiondefuerzadefinitiva ," +
                                "otros_aterramiento ," +
                                "otros_murodeseparacionhuecoscontiguos ," +
                                "presenciadeaguaenfosaycuartodemaquinas ," +
                                "presenciadecablesoelementosajenosdentroelhueco ," +
                                "observaciones ," +
                                "codproy " +
                                ") values(" +
                                "current_date() ," +
                                "current_time() ," +
                                "current_date() ," +
                                "current_time() ," +
                                "'"+edificio+"'," +
                                "'"+direccion+"'," +
                                "'"+faseinstalacion+"' ," +
                                "'"+fosa_libredeescombros+"' ," +
                                "'"+fosa_seca+"' ," +
                                "'"+fosa_alturadeacuerdoalplanodemontaje+"'," +
                                "'"+hueco_retirodeelementosajenosalascensor+"'," +
                                "'"+hueco_ganchosdemontaje+"'," +
                                "'"+hueco_impermeabilizacioncubierta+"'," +
                                "'"+hueco_orificiosdesdehuecoalagaveta+"'," +
                                "'"+hueco_espacioparalagavetadecontrol+"'," +
                                "'"+hueco_huecosparafijaciondevigas+"' ," +
                                "'"+hueco_pasoanivel+"' ," +
                                "'"+hueco_puertaconchapainterior+"' ," +
                                "'"+hueco_trampillaconcandadoochapa+"' ," +
                                "'"+hueco_puntodeiluminacion+"' ," +
                                "'"+hueco_reboqueinterior_1+"' ," +
                                "'"+hueco_barandillasdeseguridad+"' ," +
                                "'"+hueco_huecosforjados+"' ," +
                                "'"+hueco_escalerasiexistedesnivel+"' ," +
                                "'"+hueco_empotradodecajadetermicos+"' ," +
                                "'"+hueco_machones+"' ," +
                                "'"+hueco_reboqueinterior_2+"' ," +
                                "'"+hueco_pieldevidrio+"' ," +
                                "'"+hueco_pinturainteriordepuertasdepiso+"' ," +
                                "'"+accesos_libredeelementosajenosalasc+"' ," +
                                "'"+accesos_detalleyentodoslospisos+"' ," +
                                "'"+accesos_niveldepisoterminado+"' ," +
                                "'"+accesos_vigaparafijaciondepuertas+"' ," +
                                "'"+accesos_ensotanogradaenaccesoascensor+"' ," +
                                "'"+accesos_rematedepuertas+"' ," +
                                "'"+accesos_rematedepisos+"' ," +
                                "'"+accesos_pinturadepuertas+"' ," +
                                "'"+accesos_fijaciondecajasparabotonera+"' ," +
                                "'"+otros_depositoparaascensores+"' ," +
                                "'"+otros_cuartomontadores+"' ," +
                                "'"+otros_materialeselectricos+"' ," +
                                "'"+otros_colocadopisoencabina+"' ," +
                                "'"+otros_tensiondefuerzadefinitiva+"' ," +
                                "'"+otros_aterramiento+"' ," +
                                "'"+otros_murodeseparacionhuecoscontiguos+"' ," +
                                presenciadeaguaenfosaycuartodemaquinas + " ," +
                                presenciadecablesoelementosajenosdentroelhueco + " ," +
                                "'"+observaciones+"' ," +
                                codigoProyecto+")";
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar()
        {
            return false;
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        internal DataSet get_mostrarR155Realizadas(string edificio, string fechaDesde, string fechaHasta)
        {
            string consulta = "SELECT r155.codigo as 'boleta', "+
                           " date_format(r155.fecha,'%d/%m/%Y') as 'fecha',r155.hora , "+
                           " r155.edificio ,r155.direccion ,r155.faseinstalacion , "+
                           " r155.fosa_libredeescombros , "+
                           " r155.fosa_seca ,  "+
                           " r155.fosa_alturadeacuerdoalplanodemontaje , "+
                           " r155.hueco_retirodeelementosajenosalascensor , "+ 
                           " r155.hueco_ganchosdemontaje , "+
                           " r155.hueco_impermeabilizacioncubierta , "+
                           " r155.hueco_orificiosdesdehuecoalagaveta ,  "+
                           " r155.hueco_espacioparalagavetadecontrol , "+
                           " r155.hueco_huecosparafijaciondevigas , "+
                           " r155.hueco_pasoanivel ,  "+
                           " r155.hueco_puertaconchapainterior , "+
                           " r155.hueco_trampillaconcandadoochapa , "+
                           " r155.hueco_puntodeiluminacion ,  "+
                           " r155.hueco_reboqueinterior_1 , "+
                           " r155.hueco_barandillasdeseguridad , "+
                           " r155.hueco_huecosforjados ,  "+
                           " r155.hueco_escalerasiexistedesnivel , "+
                           " r155.hueco_empotradodecajadetermicos , "+
                           " r155.hueco_machones ,  "+
                           " r155.hueco_reboqueinterior_2 , "+
                           " r155.hueco_pieldevidrio , "+
                           " r155.hueco_pinturainteriordepuertasdepiso ,  "+
                           " r155.accesos_libredeelementosajenosalasc , "+
                           " r155.accesos_detalleyentodoslospisos , "+
                           " r155.accesos_niveldepisoterminado , "+
                           " r155.accesos_vigaparafijaciondepuertas , "+
                           " r155.accesos_ensotanogradaenaccesoascensor ,  "+
                           " r155.accesos_rematedepuertas , "+
                           " r155.accesos_rematedepisos , "+
                           " r155.accesos_pinturadepuertas , "+ 
                           " r155.accesos_fijaciondecajasparabotonera , "+
                           " r155.otros_depositoparaascensores , "+
                           " r155.otros_cuartomontadores , "+
                           " r155.otros_materialeselectricos , "+
                           " r155.otros_colocadopisoencabina , "+
                           " r155.otros_tensiondefuerzadefinitiva ,  "+
                           " r155.otros_aterramiento , "+
                           " r155.otros_murodeseparacionhuecoscontiguos , "+
                           " r155.presenciadeaguaenfosaycuartodemaquinas , "+
                           " r155.presenciadecablesoelementosajenosdentroelhueco , "+
                           " r155.observaciones "+
                           " ,r155.recibidonombre "+
                           " ,r155.recibidoci "+
                           " ,r155.recibidocargo "+
                           " ,res.nombre as 'Responsable'  "+
                           " FROM tb_r155inspeccionyseguimientoobra r155, tb_responsable res  "+
                           " where  "+
                           " r155.codres = res.codigo and "+
                           " r155.edificio like '%"+edificio+"%' and "+
                           " r155.fecha between "+fechaDesde+" and "+fechaHasta;
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        internal DataSet get_R155porCodigo(string codigo)
        {
          string  consulta = "SELECT r155.codigo as 'boleta', " +
                            " date_format(r155.fecha,'%d/%m/%Y') as 'fecha',r155.hora , " +
                            " r155.edificio ,r155.direccion ,r155.faseinstalacion , " +
                            " r155.fosa_libredeescombros , " +
                            " r155.fosa_seca ,  " +
                            " r155.fosa_alturadeacuerdoalplanodemontaje , " +
                            " r155.accesos_libredeelementosajenosalasc , " +
                            " r155.accesos_detalleyentodoslospisos , " +
                            " r155.accesos_niveldepisoterminado , " +
                            " r155.accesos_vigaparafijaciondepuertas , " +
                            " r155.accesos_ensotanogradaenaccesoascensor ,  " +
                            " r155.accesos_rematedepuertas , " +
                            " r155.accesos_rematedepisos , " +
                            " r155.accesos_pinturadepuertas , " +
                            " r155.accesos_fijaciondecajasparabotonera , " +
                            " r155.otros_depositoparaascensores , " +
                            " r155.otros_cuartomontadores , " +
                            " r155.otros_materialeselectricos , " +
                            " r155.otros_colocadopisoencabina , " +
                            " r155.otros_tensiondefuerzadefinitiva ,  " +
                            " r155.otros_aterramiento , " +
                            " r155.otros_murodeseparacionhuecoscontiguos , " +
                            " r155.hueco_retirodeelementosajenosalascensor , " +
                            " r155.hueco_ganchosdemontaje , " +
                            " r155.hueco_impermeabilizacioncubierta , " +
                            " r155.hueco_orificiosdesdehuecoalagaveta ,  " +
                            " r155.hueco_espacioparalagavetadecontrol , " +
                            " r155.hueco_huecosparafijaciondevigas , " +
                            " r155.hueco_pasoanivel ,  " +
                            " r155.hueco_puertaconchapainterior , " +
                            " r155.hueco_trampillaconcandadoochapa , " +
                            " r155.hueco_puntodeiluminacion ,  " +
                            " r155.hueco_reboqueinterior_1 , " +
                            " r155.hueco_barandillasdeseguridad , " +
                            " r155.hueco_huecosforjados ,  " +
                            " r155.hueco_escalerasiexistedesnivel , " +
                            " r155.hueco_empotradodecajadetermicos , " +
                            " r155.hueco_machones ,  " +
                            " r155.hueco_reboqueinterior_2 , " +
                            " r155.hueco_pieldevidrio , " +
                            " r155.hueco_pinturainteriordepuertasdepiso ,  " +                            
                            " if(r155.presenciadeaguaenfosaycuartodemaquinas=true,1,0) , " +
                            " if(r155.presenciadecablesoelementosajenosdentroelhueco=true,1,0) , " +
                            " r155.observaciones " +
                            " ,r155.recibidonombre " +
                            " ,r155.recibidoci " +
                            " ,r155.recibidocargo " +
                            " ,res.nombre as 'Responsable'  " +
                            " ,cc.nombre as 'cargo'"+
                            " FROM tb_r155inspeccionyseguimientoobra r155, tb_responsable res, tb_cargo cc  " +
                            " where  " +
                            " res.cargoc = cc.codigo and "+
                            " r155.codres = res.codigo and " +
                            " r155.codigo = " + codigo;
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }
    }
}