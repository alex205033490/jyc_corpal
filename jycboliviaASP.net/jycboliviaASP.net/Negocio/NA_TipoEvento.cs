using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_TipoEvento
    {
        private DA_TipoEvento Devento = new DA_TipoEvento();

        public DataSet mostrarAllDatos()
        {
            string consulta = "select * from tb_tipoevento";
            return Devento.getDatos(consulta);
        }
    }
}