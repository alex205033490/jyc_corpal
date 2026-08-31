using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_BoletasGarantia
    {
        private DCorpal_boletaGarantia datos = new DCorpal_boletaGarantia();

        internal bool set_guardarBoletaGarantia(DateTime fecha, decimal monto, string tipoBoleta,
                                                    string cliente, string estado, int codresp)
        {
            return datos.set_guardarBoletaGarantia(fecha, monto, tipoBoleta, cliente, estado, codresp);
        }

        internal DataSet get_getBoletasGarantia()
        {
            return datos.get_getBoletasGarantia();
        }

        internal bool update_datosBoletaGarantia(decimal monto, string tipoBoleta, string cliente,
                                                    string estado, int id)
        {
            return datos.update_datosBoletaGarantia(monto, tipoBoleta, cliente, estado, id);
        }

    }
}