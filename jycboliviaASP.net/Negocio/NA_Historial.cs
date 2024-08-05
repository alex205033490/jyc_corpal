using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Historial
    {
        DA_Historial Dhistorial = new DA_Historial();
        public NA_Historial() { }

        public bool insertar(int codResponsable , string detalle )
        {
           return Dhistorial.insertar(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"),detalle,codResponsable);
        }

        public bool modificar()
        {
            return Dhistorial.modificar();
        }

        public bool eliminar()
        {
            return Dhistorial.eliminar();
        }

        

    }
}