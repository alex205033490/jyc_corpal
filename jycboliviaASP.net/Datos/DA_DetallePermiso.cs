using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_DetallePermiso
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DA_DetallePermiso() { }


        public bool insertar(int codFormulario, int codResponsable, string fecha, string hora)
        {

            try
            {
                string consulta = "insert into tb_detalle_permisos(cod_permiso,cod_responsable,fecha_creado,hora_creado) values ("+codFormulario+","+codResponsable+",'"+fecha+"','"+hora+"')";
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception )
            {
                return false;
            }

        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar(int codFormulario,int codReponsable)
        {
            try{
                string consulta = "delete from tb_detalle_permisos where COD_PERMISO = "+codFormulario+" and COD_RESPONSABLE = "+codReponsable;
                ConecRes.ejecutarMySql(consulta);
                return true;
            }catch(Exception ){
            return false;
            }
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

    }
}