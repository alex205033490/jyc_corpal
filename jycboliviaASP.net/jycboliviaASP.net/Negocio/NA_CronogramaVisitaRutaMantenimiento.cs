using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_CronogramaVisitaRutaMantenimiento
    {
        DA_CronogramaVisitaRutaManteminieto cv = new DA_CronogramaVisitaRutaManteminieto();
        public NA_CronogramaVisitaRutaMantenimiento() { }

        public bool insertarCronogramaVisitaRutaM(int codRuta, int codEquipo, int nrovisita, int codmes, int anio, bool semana1, bool semana2, bool semana3, bool semana4, string fechaS1, string fechaS2, string fechaS3, string fechaS4, int codResp, string horaEntrada, string horasalida, string dia, int nrodia, float pasaje)
        {           
            return cv.insertarCronogramaVisitaRutaM( codRuta, codEquipo, nrovisita, codmes, anio, semana1, semana2, semana3, semana4,  fechaS1,  fechaS2,  fechaS3,  fechaS4,  codResp, horaEntrada,  horasalida,  dia,  nrodia,  pasaje);
        }

        public bool UpdateCronogramaVisitaRutaM(int codigoCronograma, int nrovisita, bool semana1, bool semana2, bool semana3, bool semana4, string fechaS1, string fechaS2, string fechaS3, string fechaS4, int codResp, string horaEntrada, string horasalida, string dia, int nrodia, float pasaje)
        { 
        return cv.UpdateCronogramaVisitaRutaM( codigoCronograma,  nrovisita,  semana1,  semana2,  semana3,  semana4,  fechaS1,  fechaS2,  fechaS3,  fechaS4,  codResp, horaEntrada,  horasalida,  dia,  nrodia,  pasaje);
        }

        public bool eliminar(int nrovisita, int codmes, int anio)
        {          
            return cv.eliminar( nrovisita,  codmes,  anio);
        }

        public DataSet getAllDatos(string nrovisita, string codmes, string anio)
        {            
            return cv.getAllDatos( nrovisita,  codmes,  anio);
        }

        public DataSet getUltimoIngresado(int codRuta, int codEquipo, int mes, int anio)
        {
            return cv.getUltimoIngresado( codRuta,  codEquipo,  mes,  anio);
        }

        public DataSet getcronoVisitaRutaEquipo(int codRuta, int codEquipo, int codmes, int anio)
        { 
        return cv.getcronoVisitaRutaEquipo( codRuta,  codEquipo,  codmes,  anio);
        }

        public int getCodigoCronoVisitaRutaEquipo(int codRuta, int codEquipo, int codmes, int anio)
        {
            DataSet dato = cv.getcronoVisitaRutaEquipo(codRuta, codEquipo, codmes, anio);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return -1;
        }

        public bool existecronoVisitaRutaEquipo(int codRuta, int codEquipo, int codmes, int anio)
        {
            DataSet dato = cv.getcronoVisitaRutaEquipo(codRuta, codEquipo, codmes, anio);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;

        }

    }
}