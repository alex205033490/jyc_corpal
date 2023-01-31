using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    
    public class DA_fechaContrato
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_fechaContrato() { }

        public bool insertar(string fecha, string hora, string fechaContratoFirma, string fechaContratoInicio, string fechaContratoFin,int mesesContrato, int codigoEquipo, string monto, string NroContrato ) {
            
            string consulta = "insert into tb_fechacontrato_firmado (fecha,hora,fechafirmacontrato,fechainicio,fechafin,codequipo,monto,mesescontrato, codcontrato) "+
            "values('" + fecha + "','" + hora + "'," + fechaContratoFirma + ", "+fechaContratoInicio+" , "+fechaContratoFin+" , "+codigoEquipo+" , '"+monto+"' , "+mesesContrato+", '"+NroContrato+"');";
            return ConecRes.ejecutarMySql(consulta);        
        }

        public bool Actualizar(int codigo, string fechaContratoFirma, string fechaContratoInicio, string fechaContratoFin, int mesesContrato, int codigoEquipo, float monto)
        {
            string consulta = "update tb_fechacontrato_firmado set fechafirmacontrato = " + fechaContratoFirma + ",fechainicio = " + fechaContratoInicio + ",fechafin = " + fechaContratoFin + ",codequipo = " + codigoEquipo + ",monto = " + monto + ",mesescontrato = " + mesesContrato + " where tb_fechacontrato_firmado.codigo = "+codigo;
            return ConecRes.ejecutarMySql(consulta);
        }


        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

    }
}