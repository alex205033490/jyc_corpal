using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_DetalleTecnicoAsignado
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_DetalleTecnicoAsignado() {}

        public bool insertar(int codEquipo, int codResponsable, int codCargo, string fecha, string hora)
        {
            try
            {
                string consulta = "REPLACE into tb_detalle_tecnico_asignado (codeq, codresp, codcargo, fecha_asignado, hora_asignado, estado) values(" + codEquipo + "," + codResponsable + "," + codCargo + ",'" + fecha + "','" + hora + "', 1)";
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

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


        public bool eliminarAntiguos(int codEquipo, int CodCargo)
        {

            try
            {
                string consulta = "update tb_detalle_tecnico_asignado set tb_detalle_tecnico_asignado.estado = 0 " +
                                  " where tb_detalle_tecnico_asignado.codeq = " + codEquipo + " and tb_detalle_tecnico_asignado.codcargo = " + CodCargo;
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}