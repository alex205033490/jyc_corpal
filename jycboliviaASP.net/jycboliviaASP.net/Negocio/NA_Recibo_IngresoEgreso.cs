using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Recibo_IngresoEgreso
    {
        DA_Recibo_IngresoEgreso nrr = new DA_Recibo_IngresoEgreso();
        public NA_Recibo_IngresoEgreso() { }

        public bool insertarReciboIngreso(string cliente, float monto, string moneda, string chequenro, string concepto, string detalle, int codrespgra, string responsable, string facturanro, string nrorecibo)
        {
            return nrr.insertarReciboIngreso(cliente, monto, moneda, chequenro, concepto, detalle, codrespgra, responsable, facturanro, nrorecibo);
        }

        public bool insertarReciboEgreso(string pagadoha, float monto, string moneda, string chequenro, string concepto, string detalle, int codrespgra, string responsable, string banco, bool efectivo, float porcentajeretencioniue, float porcentajeretencionit, float retencioniuebs, float retencionitbs, float totalapagar, string facturanro, string nrorecibo)
        {
            return nrr.insertarReciboEgreso(pagadoha, monto, moneda, chequenro, concepto, detalle, codrespgra, responsable, banco, efectivo, porcentajeretencioniue, porcentajeretencionit, retencioniuebs, retencionitbs, totalapagar, facturanro, nrorecibo);
        }


        public bool modificarReciboIngreso(int codigo, string cliente, float monto, string moneda, string chequenro, string concepto, string detalle, int codrespgra, string responsable, string facturanro)
        {
            return nrr.modificarReciboIngreso(codigo, cliente, monto, moneda, chequenro, concepto, detalle, codrespgra, responsable, facturanro);
        }

        public bool modificarReciboEgreso(int codigo, string pagadoha, float monto, string moneda, string chequenro, string concepto, string detalle, int codrespgra, string responsable, string banco, bool efectivo, float porcentajeretencioniue, float porcentajeretencionit, float retencioniuebs, float retencionitbs, float totalapagar, string facturanro)
        {
            return nrr.modificarReciboEgreso(codigo, pagadoha, monto, moneda, chequenro, concepto, detalle, codrespgra, responsable, banco, efectivo, porcentajeretencioniue, porcentajeretencionit, retencioniuebs, retencionitbs, totalapagar, facturanro);
        }

        public bool eliminarIngreso(int codigo)
        {
            return nrr.eliminarIngreso(codigo);
        }

        public bool eliminarEgreso(int codigo)
        { 
            return nrr.eliminarEgreso(codigo);
        }

        internal DataSet mostrarReciboIngreso(string cliente, int codUser)
        {
            return nrr.mostrarReciboIngreso(cliente, codUser);
        }

        internal DataSet mostrarReciboEgreso(string pagadoha, int codUser)
        { 
            return nrr.mostrarReciboEgreso( pagadoha,  codUser);
        }



        internal DataSet getDatosReciboIngreso(int codigoReciboIngreso)
        {
           return nrr.getDatosReciboIngreso(codigoReciboIngreso);
        }

        internal int get_codigoUltimoInsertado(string cliente)
        {
            DataSet dato = nrr.get_codigoUltimoInsertado(cliente);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return -1;
        }

        internal int get_codigoUltimoInsertadoEgreso(string pagadoha)
        {
            DataSet dato = nrr.get_codigoUltimoInsertadoEgreso(pagadoha);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return -1;
        }

        internal DataSet getDatosReciboEgreso(int codigoReciboEgreso)
        {
            return nrr.getDatosReciboEgreso( codigoReciboEgreso);
        }

        internal DataSet get_allreciboIngreso(string fecha1, string fecha2)
        {
            return nrr.get_allreciboIngreso(fecha1, fecha2);
        }

        internal DataSet get_allreciboEgreso(string fecha1, string fecha2)
        { 
            return nrr.get_allreciboEgreso( fecha1,  fecha2);
        }


        internal string get_nroRegistroIngresoSiguiente(int codUser)
        {
            DataSet dato = nrr.get_nroRegistroIngresoSiguiente(codUser);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return dato.Tables[0].Rows[0][0].ToString();
            }
            else
                return "Ninguno";
        }

        internal string get_nroRegistroEgresoSiguiente(int codUser)
        {
            DataSet dato = nrr.get_nroRegistroEgresoSiguiente(codUser);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return dato.Tables[0].Rows[0][0].ToString();
            }
            else
                return "Ninguno";
        }
    }
}