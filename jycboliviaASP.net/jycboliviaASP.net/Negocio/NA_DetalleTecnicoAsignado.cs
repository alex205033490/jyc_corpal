using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;


namespace jycboliviaASP.net.Negocio
{
    public class NA_DetalleTecnicoAsignado
    {        
        public NA_DetalleTecnicoAsignado() { }

        private DA_DetalleTecnicoAsignado DtecnicoA = new DA_DetalleTecnicoAsignado();

        public bool insertar(int codEquipo, int codResponsable, int codCargo)
        {
            return DtecnicoA.insertar(codEquipo, codResponsable, codCargo, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"));
        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar()
        {
            return false;
        }

        public DataSet mostrarAllDatos()
        {
            string consulta = "select dta.codeq, dta.codresp, dta.codcargo, DATE_FORMAT(dta.fecha_asignado,'%d/%m/%Y'),dta.hora_asignado from tb_detalle_tecnico_asignado dta";
            return DtecnicoA.getDatos(consulta);
        }

        public DataSet getDetalleTecnicoAsignado(int codEquipo, int codCargo)
        {
            string consulta = "select dta.codeq, dta.codresp, dta.codcargo, DATE_FORMAT(dta.fecha_asignado,'%d/%m/%Y'),dta.hora_asignado "+
                              " from tb_detalle_tecnico_asignado dta "+
                              " where dta.codeq = "+codEquipo+" and dta.codcargo = "+codCargo+" and dta.estado = 1";
            return DtecnicoA.getDatos(consulta);
        }

        public bool eliminarAntiguos(int codEquipo, int CodCargo) {
            return DtecnicoA.eliminarAntiguos(codEquipo,CodCargo);
        }

    }
}