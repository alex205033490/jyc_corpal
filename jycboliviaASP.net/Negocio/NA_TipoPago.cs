using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_TipoPago
    {
        DA_TipoPago DtipoPago = new DA_TipoPago();

        public NA_TipoPago() { }

        public bool insertar()
        {

            try
            {

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

        public DataSet mostrarAllDatos()
        {
            string consulta = "select tipoP.codigo, tipoP.nombre from tb_tipopago tipoP where tipoP.estado = 1";
            return DtipoPago.getDatos(consulta);        
        }

    }
}