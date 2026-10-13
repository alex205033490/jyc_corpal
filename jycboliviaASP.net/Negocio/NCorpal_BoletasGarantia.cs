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

        internal bool set_guardarBoletaGarantia(DateTime fecha, string moneda, decimal montonumeral, string tipoBoleta,
                                                    string beneficiario, string cliente, string estado, int codresp,
                                                    DateTime finicio, DateTime fvencimiento, string obj, string extencionobj)
        {
            return datos.set_guardarBoletaGarantia(fecha, moneda, montonumeral, tipoBoleta, 
                                                   beneficiario, cliente, estado, codresp, 
                                                   finicio, fvencimiento, obj, extencionobj);
        }

        internal DataSet get_getBoletasGarantia()
        {
            return datos.get_getBoletasGarantia();
        }

        internal bool update_datosBoletaGarantia(string moneda, decimal monto, string tipoBoleta,
                                                string beneficiario, string cliente, string estado,
                                                DateTime finicio, DateTime fvencimiento, string obj,
                                                string extencionobj, int id)
        {
            return datos.update_datosBoletaGarantia(moneda, monto, tipoBoleta,
                                                beneficiario, cliente, estado,
                                                finicio, fvencimiento, obj,
                                                extencionobj, id);
        }

    }
}