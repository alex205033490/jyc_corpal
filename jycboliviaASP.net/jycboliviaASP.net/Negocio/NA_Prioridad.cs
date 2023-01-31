using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{

    public class NA_Prioridad
    {
        private DA_Prioridad dprioridad = new DA_Prioridad();

        public NA_Prioridad() { }

        public DataSet getAllPrioridad()
        {           
            return dprioridad.getAllPrioridad();
        }



    }
}