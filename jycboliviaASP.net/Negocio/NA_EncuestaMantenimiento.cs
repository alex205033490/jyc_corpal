using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;

namespace jycboliviaASP.net.Negocio
{
    public class NA_EncuestaMantenimiento
    {
        private DA_EncuestaMantenimiento dencuesta = new DA_EncuestaMantenimiento();

        public NA_EncuestaMantenimiento() { }

        public bool insertarEncuestaMantenimiento(int codproyecto, int cumplimientofechasplanificadasmantenimiento,
                                                    int funcionamientodelosequipos,
                                                    int rapidezdelasreparaciones,
                                                    int resolucionefectivadelacausadereparacion,
                                                    int asesoramientoyrapidezenlaentregadecotizacionesinformes,
                                                    int tiempoderespuestaanteunaemergencia,
                                                    int resolucionefectivadelasemergencias,
                                                    int cordialidadyatenciondelpersonaldecobranza,
                                                    int tratoyatenciondelpersonalasministrativo,
                                                    int cordialidadyatenciondelpersonaltecnico,
                                                    int tratoyatenciondelpersonaldeingenieria,
                                                    int tratoatencionyrespuestadelpersonaldecallcenter){

            return dencuesta.insertarEncuestaMantenimiento( codproyecto,  cumplimientofechasplanificadasmantenimiento,
                                                     funcionamientodelosequipos,
                                                     rapidezdelasreparaciones,
                                                     resolucionefectivadelacausadereparacion,
                                                     asesoramientoyrapidezenlaentregadecotizacionesinformes,
                                                     tiempoderespuestaanteunaemergencia,
                                                     resolucionefectivadelasemergencias,
                                                     cordialidadyatenciondelpersonaldecobranza,
                                                     tratoyatenciondelpersonalasministrativo,
                                                     cordialidadyatenciondelpersonaltecnico,
                                                     tratoyatenciondelpersonaldeingenieria,
                                                     tratoatencionyrespuestadelpersonaldecallcenter);

        }

    }
}