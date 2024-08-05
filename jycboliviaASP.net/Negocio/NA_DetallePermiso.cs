using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_DetallePermiso
    {
        DA_DetallePermiso Dpermisos = new DA_DetallePermiso();

        public NA_DetallePermiso() {}

        public bool insertar(int codFormulario, int codResponsable, string fecha, string hora)
        {
            return Dpermisos.insertar(codFormulario,codResponsable,fecha,hora);
        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar(int codFormulario, int codReponsable)
        {
            return Dpermisos.eliminar(codFormulario, codReponsable);
        }

        public DataSet mostrarPermiso_responsable(int codResponsable)
        {
            string consulta = "select f.* from tb_formulario f ,tb_detalle_permisos per, tb_responsable resp where resp.codigo =  per.COD_RESPONSABLE and f.CODIGO = per.COD_PERMISO and resp.codigo = "+codResponsable;
            return Dpermisos.getDatos(consulta);
        }
        
        public DataSet mostrarNOPermiso_responsable(int codResponsable)
        {
            string consulta = "select fm.* from tb_formulario fm where fm.CODIGO not in (select f.CODIGO from tb_formulario f ,tb_detalle_permisos per, tb_responsable resp where resp.codigo =  per.COD_RESPONSABLE and f.CODIGO = per.COD_PERMISO and resp.codigo = "+codResponsable+")";
            return Dpermisos.getDatos(consulta);
        }

        public bool tienePermisoResponsable(int permiso, int codResponsable) {
            string consulta = "select * from tb_detalle_permisos dp where dp.COD_RESPONSABLE = "+codResponsable+" and dp.COD_PERMISO = " + permiso;
            DataSet datos = Dpermisos.getDatos(consulta);
            bool permisoRes = false; 
            if(datos.Tables[0].Rows.Count > 0){
                permisoRes = true;
            }
            return permisoRes;
        }
    }

}