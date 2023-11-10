using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;
using System.Globalization;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Recibo_IngresoEgreso
    {
        DA_Recibo_IngresoEgreso nrr = new DA_Recibo_IngresoEgreso();
        public NA_Recibo_IngresoEgreso() { }

        public bool insertarReciboIngreso(string cliente, float monto, string moneda, string chequenro, string concepto, string detalle, int codrespgra, string responsable, string facturanro, string nrorecibo, string fecharecibo)
        {
            return nrr.insertarReciboIngreso(cliente, monto, moneda, chequenro, concepto, detalle, codrespgra, responsable, facturanro, nrorecibo,  fecharecibo);
        }

        public bool insertarReciboEgreso(string pagadoha, float monto, string moneda, string chequenro, string concepto, string detalle, int codrespgra, string responsable, string banco, bool efectivo, float porcentajeretencioniue, float porcentajeretencionit, float retencioniuebs, float retencionitbs, float totalapagar, string facturanro, string nrorecibo, string fechaegreso)
        {
            return nrr.insertarReciboEgreso(pagadoha, monto, moneda, chequenro, concepto, detalle, codrespgra, responsable, banco, efectivo, porcentajeretencioniue, porcentajeretencionit, retencioniuebs, retencionitbs, totalapagar, facturanro, nrorecibo, fechaegreso);
        }


        public bool modificarReciboIngreso(int codigo, string cliente, float monto, string moneda, string chequenro, string concepto, string detalle, int codrespgra, string responsable, string facturanro, string fecharecibo)
        {
            return nrr.modificarReciboIngreso(codigo, cliente, monto, moneda, chequenro, concepto, detalle, codrespgra, responsable, facturanro, fecharecibo);
        }

        public bool modificarReciboEgreso(int codigo, string pagadoha, float monto, string moneda, string chequenro, string concepto, string detalle, int codrespgra, string responsable, string banco, bool efectivo, float porcentajeretencioniue, float porcentajeretencionit, float retencioniuebs, float retencionitbs, float totalapagar, string facturanro, string fechaegreso)
        {
            return nrr.modificarReciboEgreso(codigo, pagadoha, monto, moneda, chequenro, concepto, detalle, codrespgra, responsable, banco, efectivo, porcentajeretencioniue, porcentajeretencionit, retencioniuebs, retencionitbs, totalapagar, facturanro, fechaegreso);
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

        internal DataSet get_allreciboIngreso(string fecha1, string fecha2, string responsable)
        {
            return nrr.get_allreciboIngreso(fecha1, fecha2, responsable);
        }

        internal DataSet get_allreciboEgreso(string fecha1, string fecha2, string responsable)
        { 
            return nrr.get_allreciboEgreso( fecha1,  fecha2, responsable);
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

        internal bool insertconciliacionBancaria(string saldoanterior, string extractobancario, int codcuentaBancaria, int CodUser)
        {
            return nrr.insertconciliacionBancaria( saldoanterior,  extractobancario,  codcuentaBancaria,  CodUser);
        }

        internal bool eliminarIngresobancarizacion(string fecha, int cuentaBanco, int coduser, float montoRestar)
        {
            return nrr.eliminarIngresobancarizacion( fecha,  cuentaBanco,  coduser,  montoRestar);
        }

        internal DataTable get_allrecibosIngresoVsEgreso(string fechadesde, string fechahasta, double SaldoInicial, string responsable)
        {
           // return nrr.get_allrecibosIngresoVsEgreso( fechadesde,  fechahasta);

            DataSet Datos = nrr.get_allrecibosIngresoVsEgreso(fechadesde, fechahasta, responsable);

            DataTable TablaR = Datos.Tables[0];
            TablaR.Columns.Add("Saldo", typeof(double));

            double SaldoInicialAux = SaldoInicial;
            for (int i = 0; i < TablaR.Rows.Count; i++)
            {
                float montoIngreso;
                float.TryParse(TablaR.Rows[i][6].ToString(), out montoIngreso);
                float montoEgreso;
                float.TryParse(TablaR.Rows[i][7].ToString(), out montoEgreso);

                double saldoR = (SaldoInicialAux + montoIngreso) - montoEgreso;
                TablaR.Rows[i]["Saldo"] = saldoR;
                SaldoInicialAux = saldoR;
            }
            return TablaR;
        }

        internal double get_SaldoInicial_IngresoEgreso(string fechadesde)
        {
            DataSet datos = nrr.get_SaldoInicial_IngresoEgreso( fechadesde);            
            string aux = datos.Tables[0].Rows[0][2].ToString();
            double saldo;            
            double.TryParse(aux, out saldo);
            return saldo;
        }

        internal DataSet get_SaldosInicialesResponsable(string fechadesde, string responsable)
        {
            return nrr.get_SaldosInicialesResponsable(fechadesde, responsable);
        }

        internal DataTable get_allrecibosIngresoVsEgreso2(string fechadesde, string fechahasta, double SaldoInicial, string nameResp)
        {

            DataSet Datos = nrr.get_allrecibosIngresoVsEgreso(fechadesde, fechahasta, nameResp);

            DataTable TablaR = Datos.Tables[0];
            TablaR.Columns.Add("Saldo", typeof(double));
            TablaR.Columns.Add("SaldoInicial", typeof(double));
            TablaR.Columns.Add("fechadesde", typeof(string));
            TablaR.Columns.Add("fechahasta", typeof(string));
            TablaR.Columns.Add("RespComprobante", typeof(string));

            double SaldoInicialAux = SaldoInicial;
            for (int i = 0; i < TablaR.Rows.Count; i++)
            {
                float montoIngreso;
                float.TryParse(TablaR.Rows[i][6].ToString(), out montoIngreso);
                float montoEgreso;
                float.TryParse(TablaR.Rows[i][7].ToString(), out montoEgreso);

                double saldoR = (SaldoInicialAux + montoIngreso) - montoEgreso;
                TablaR.Rows[i]["Saldo"] = saldoR;
                SaldoInicialAux = saldoR;

                TablaR.Rows[i]["SaldoInicial"] = SaldoInicial;
                TablaR.Rows[i]["fechadesde"] = fechadesde;
                TablaR.Rows[i]["fechahasta"] = fechahasta;
                TablaR.Rows[i]["RespComprobante"] = nameResp;
            }

            if (TablaR.Rows.Count == 0)
            {
                DataRow filanueva = TablaR.NewRow();
                filanueva["Saldo"] = SaldoInicial;
                filanueva["SaldoInicial"] = SaldoInicial;
                filanueva["fechadesde"] = fechadesde;
                filanueva["fechahasta"] = fechahasta;
                filanueva["RespComprobante"] = nameResp;
                TablaR.Rows.Add(filanueva);
            }
            
            return TablaR;
        }
    }
}