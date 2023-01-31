using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DA_Historial
    {

        private conexionMySql ConecRes = new conexionMySql();

        public DA_Historial() { }


        public bool insertar(string fecha, string hora, string detalle, int codResponsable)
        {

            try
            {
                string consulta = "insert into tb_historial(fecha,hora,detalle,codResp) values('" + fecha + "','" + hora + "','" + detalle + "'," + codResponsable + "); ";    
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

    }
}