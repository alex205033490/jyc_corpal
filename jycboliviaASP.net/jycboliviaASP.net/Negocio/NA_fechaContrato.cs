using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_fechaContrato
    {
        DA_fechaContrato dfechaContrato = new DA_fechaContrato();

        public NA_fechaContrato() { }

        public bool insertar(string fechaContratoFirma, string fechaContratoInicio, string fechaContratoFin, int mesesContrato, int codigoEquipo, string monto, string NroContrato)
        {
            return dfechaContrato.insertar(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), fechaContratoFirma, fechaContratoInicio, fechaContratoFin, mesesContrato, codigoEquipo, monto, NroContrato);
        }

        public bool Actualizar(int codigo, string fechaContratoFirma, string fechaContratoInicio, string fechaContratoFin, int mesesContrato, int codigoEquipo, float monto)
        {
            return dfechaContrato.Actualizar(codigo, fechaContratoFirma, fechaContratoInicio, fechaContratoFin, mesesContrato, codigoEquipo, monto);
        }

        public int getCodigoUltimoInsertado() {
            string consulta = "select Max(f.codigo) from tb_fechacontrato_firmado f ;";
            DataSet resp = dfechaContrato.getDatos(consulta);
            return Convert.ToInt32(resp.Tables[0].Rows[0][0].ToString());
        }

    }
}