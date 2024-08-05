using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JyC_Exterior.Datos;
using System.Data;

namespace JyC_Exterior.Negocio
{
    public class NA_R155_InspeccionySeguimientoObra
    {
        private DA_R155_InspeccionySeguimientoObra dr155 = new DA_R155_InspeccionySeguimientoObra();
        public NA_R155_InspeccionySeguimientoObra() { }

        public bool insertar(string edificio,
                                                  string direccion,
                                                  string faseinstalacion,
                                                  string fosa_libredeescombros,
                                                  string fosa_seca,
                                                  string fosa_alturadeacuerdoalplanodemontaje,
                                                  string hueco_retirodeelementosajenosalascensor,
                                                  string hueco_ganchosdemontaje,
                                                  string hueco_impermeabilizacioncubierta,
                                                  string hueco_orificiosdesdehuecoalagaveta,
                                                  string hueco_espacioparalagavetadecontrol,
                                                  string hueco_huecosparafijaciondevigas,
                                                  string hueco_pasoanivel,
                                                  string hueco_puertaconchapainterior,
                                                  string hueco_trampillaconcandadoochapa,
                                                  string hueco_puntodeiluminacion,
                                                  string hueco_reboqueinterior_1,
                                                  string hueco_barandillasdeseguridad,
                                                  string hueco_huecosforjados,
                                                  string hueco_escalerasiexistedesnivel,
                                                  string hueco_empotradodecajadetermicos,
                                                  string hueco_machones,
                                                  string hueco_reboqueinterior_2,
                                                  string hueco_pieldevidrio,
                                                  string hueco_pinturainteriordepuertasdepiso,
                                                  string accesos_libredeelementosajenosalasc,
                                                  string accesos_detalleyentodoslospisos,
                                                  string accesos_niveldepisoterminado,
                                                  string accesos_vigaparafijaciondepuertas,
                                                  string accesos_ensotanogradaenaccesoascensor,
                                                  string accesos_rematedepuertas,
                                                  string accesos_rematedepisos,
                                                  string accesos_pinturadepuertas,
                                                  string accesos_fijaciondecajasparabotonera,
                                                  string otros_depositoparaascensores,
                                                  string otros_cuartomontadores,
                                                  string otros_materialeselectricos,
                                                  string otros_colocadopisoencabina,
                                                  string otros_tensiondefuerzadefinitiva,
                                                  string otros_aterramiento,
                                                  string otros_murodeseparacionhuecoscontiguos,
                                                  string presenciadeaguaenfosaycuartodemaquinas,
                                                  string presenciadecablesoelementosajenosdentroelhueco,
                                                  string observaciones,
                                                  int codproy)
        {

            return dr155.insertar( edificio,
                                                   direccion,
                                                   faseinstalacion,
                                                   fosa_libredeescombros,
                                                   fosa_seca,
                                                   fosa_alturadeacuerdoalplanodemontaje,
                                                   hueco_retirodeelementosajenosalascensor,
                                                   hueco_ganchosdemontaje,
                                                   hueco_impermeabilizacioncubierta,
                                                   hueco_orificiosdesdehuecoalagaveta,
                                                   hueco_espacioparalagavetadecontrol,
                                                   hueco_huecosparafijaciondevigas,
                                                   hueco_pasoanivel,
                                                   hueco_puertaconchapainterior,
                                                   hueco_trampillaconcandadoochapa,
                                                   hueco_puntodeiluminacion,
                                                   hueco_reboqueinterior_1,
                                                   hueco_barandillasdeseguridad,
                                                   hueco_huecosforjados,
                                                   hueco_escalerasiexistedesnivel,
                                                   hueco_empotradodecajadetermicos,
                                                   hueco_machones,
                                                   hueco_reboqueinterior_2,
                                                   hueco_pieldevidrio,
                                                   hueco_pinturainteriordepuertasdepiso,
                                                   accesos_libredeelementosajenosalasc,
                                                   accesos_detalleyentodoslospisos,
                                                   accesos_niveldepisoterminado,
                                                   accesos_vigaparafijaciondepuertas,
                                                   accesos_ensotanogradaenaccesoascensor,
                                                   accesos_rematedepuertas,
                                                   accesos_rematedepisos,
                                                   accesos_pinturadepuertas,
                                                   accesos_fijaciondecajasparabotonera,
                                                   otros_depositoparaascensores,
                                                   otros_cuartomontadores,
                                                   otros_materialeselectricos,
                                                   otros_colocadopisoencabina,
                                                   otros_tensiondefuerzadefinitiva,
                                                   otros_aterramiento,
                                                   otros_murodeseparacionhuecoscontiguos,
                                                   presenciadeaguaenfosaycuartodemaquinas,
                                                   presenciadecablesoelementosajenosdentroelhueco,
                                                   observaciones,
                                                   codproy);
        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar()
        {
            return false;
        }

        public DataSet get_mostrarR155Realizadas(string edificio, string fechaDesde, string fechaHasta) { 
            return  dr155.get_mostrarR155Realizadas( edificio,  fechaDesde,  fechaHasta);
        }


        internal DataSet get_R155porCodigo(string codigo)
        {
            return dr155.get_R155porCodigo(codigo);
        }
    }
}