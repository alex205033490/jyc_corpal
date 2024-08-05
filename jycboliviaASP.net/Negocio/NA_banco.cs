using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;
using System.Globalization;

namespace jycboliviaASP.net.Negocio
{
    public class NA_banco
    {
        DA_banco Dbanco = new DA_banco();

        public NA_banco() { }

        public bool insertBanco(string nombre)
        {
            return Dbanco.insertBanco(nombre);
        }

        public bool insertarCuentaBancaria(string cuenta, string moneda, int banco, int tipoCuenta) {
            return Dbanco.insertarCuentaBancaria(cuenta,moneda,banco,tipoCuenta);
        }

        public DataSet mostrarBancos() {
            string consulta = "select ba.codigo, ba.nombre from tb_banco ba";
            return Dbanco.getDatos(consulta);
        }

        public bool insertconciliacionBancaria(string saldoAnterior, string extractoBancario, int codCuentaBanco, int coduser) {
            return Dbanco.insertconciliacionBancaria(saldoAnterior,extractoBancario,codCuentaBanco,coduser);
        }

        public bool insertChequeCirculacion(string fecha, string nrocheque, string monto, int codConciliacionBanco)
        {
            return Dbanco.insertChequeCirculacion(fecha, nrocheque, monto, codConciliacionBanco);
        }

        public int getultimaConciliacion(){
            string consulta = "select max(cu.codigo) from tb_conciliacionbancaria cu";
            DataSet dato = Dbanco.getDatos(consulta);
            if(dato.Tables[0].Rows.Count > 0){
                return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
            }
            return -1;

        }

        public int getcodigoBanco(string nombreBanco)
        {
            string consulta = "select ba.codigo from tb_banco ba where ba.nombre = '"+nombreBanco+"'";
            DataSet dato = Dbanco.getDatos(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
            }
            return -1;

        }

        public int getcodigoCuenta(string nroCuenta)
        {
            string consulta = "select cu.codigo from tb_cuentabancaria cu where cu.nrocuenta = '"+nroCuenta+"'";
            DataSet dato = Dbanco.getDatos(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dato.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public bool tieneConciliacionBancaria(int codCuentaBancaria) {
            string consulta = "select * from tb_conciliacionbancaria con " +
                              " where con.fecha = date_format(now(),'%Y/%m/%d')  and con.codcuentabanco = " + codCuentaBancaria;
            DataSet dato = Dbanco.getDatos(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        
        }

        public DataSet mostrarCuentasBancarias(int banco) {
            string consulta = "select cu.codigo,cu.nrocuenta as nombre from tb_cuentabancaria cu where cu.codbanco = " + banco;
            return Dbanco.getDatos(consulta);
        }

        public DataSet mostrarCuentasBancarias2(int codigoCuenta)
        {
            string consulta = "select cu.codigo,cu.fecha,cu.nrocuenta,cu.moneda,tipo.nombre "+
                               " from tb_cuentabancaria cu, tb_tipocuenta tipo "+
                               " where cu.codtipocuenta = tipo.codigo and "+
                               " cu.codigo =  "+codigoCuenta;
            return Dbanco.getDatos(consulta);
        }


        public DataSet listarCuentasBancariasCochabamba()
        {
            string consulta = "select b.nombre as 'banco',tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta "+
                               " from db_seguimientocbba_jyc.tb_banco b, db_seguimientocbba_jyc.tb_cuentabancaria cu , db_seguimientocbba_jyc.tb_tipocuenta tc "+
                               " where b.codigo = cu.codbanco and "+
                               " cu.codtipocuenta = tc.codigo" ;
                    return Dbanco.getDatos(consulta);
        }

        public DataSet listarCuentasBancariasSantaCruz()
        {
            string consulta = "select b.nombre as 'banco',tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta "+
                               " from db_seguimientoscz_jyc.tb_banco b, db_seguimientoscz_jyc.tb_cuentabancaria cu , db_seguimientoscz_jyc.tb_tipocuenta tc "+
                               " where b.codigo = cu.codbanco and "+
                               " cu.codtipocuenta = tc.codigo";
            return Dbanco.getDatos(consulta);
        }

        public DataSet listarCuentasBancariasLaPaz()
        {
            string consulta = "select b.nombre as 'banco',tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta "+
                               " from db_seguimientolpz_jyc.tb_banco b, db_seguimientolpz_jyc.tb_cuentabancaria cu , db_seguimientolpz_jyc.tb_tipocuenta tc "+
                               " where b.codigo = cu.codbanco and "+
                               " cu.codtipocuenta = tc.codigo";
            return Dbanco.getDatos(consulta);
        }

        public DataSet listarCuentasBancariasJYCSRL()
        {
            string consulta = "select b.nombre as 'banco',tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta "+
                               " from db_seguimientojycsrl_jyc.tb_banco b, db_seguimientojycsrl_jyc.tb_cuentabancaria cu , db_seguimientojycsrl_jyc.tb_tipocuenta tc "+
                               " where b.codigo = cu.codbanco and "+
                               " cu.codtipocuenta = tc.codigo";
            return Dbanco.getDatos(consulta);
        }

        public DataSet listarCuentasBancariasJYCIASRL()
        {
            string consulta = "select b.nombre as 'banco',tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta "+
                               " from db_seguimientojyciasrl_jyc.tb_banco b, db_seguimientojyciasrl_jyc.tb_cuentabancaria cu , db_seguimientojyciasrl_jyc.tb_tipocuenta tc "+
                               " where b.codigo = cu.codbanco and "+
                               " cu.codtipocuenta = tc.codigo";
            return Dbanco.getDatos(consulta);
        }


        public DataSet listarCuentasBancariasImven()
        {
            string consulta = "select b.nombre as 'banco',tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta "+
                               " from db_seguimientoimven_jyc.tb_banco b, db_seguimientoimven_jyc.tb_cuentabancaria cu , db_seguimientoimven_jyc.tb_tipocuenta tc "+
                               " where b.codigo = cu.codbanco and "+
                               " cu.codtipocuenta = tc.codigo";
            return Dbanco.getDatos(consulta);
        }

        public DataSet listarSaldosCuentasBancariasCochabamba(string fecha)
        {
            string consulta = "select "+
                               " b.nombre,tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta, "+
                               " IF(t2.resultado>0,t2.resultado,0) as 'resultado' " +
                               " from "+
                               " db_seguimientocbba_jyc.tb_banco b, db_seguimientocbba_jyc.tb_tipocuenta tc, db_seguimientocbba_jyc.tb_cuentabancaria cu "+
                               " left join "+
                               " ( "+
                               " select con.codcuentabanco,IFNULL((con.extractobancario-t1.montoCheque),con.extractobancario) as 'resultado'  " +
                               " from db_seguimientocbba_jyc.tb_conciliacionbancaria con "+
                               " left join  "+
                               " (select che.codconciliacionbanco,sum(che.monto)as 'montoCheque' from db_seguimientocbba_jyc.tb_chequesencirculacion "+
                               " che group by che.codconciliacionbanco) as t1 "+
                               " ON con.codigo = t1.codconciliacionbanco "+
                               " where "+
                               " con.fecha = '"+fecha+"' "+
                               " ) as t2 "+
                               " ON cu.codigo = t2.codcuentabanco "+
                               " where b.codigo = cu.codbanco and "+
                               " cu.codtipocuenta = tc.codigo ";
            return Dbanco.getDatos(consulta);
        }


        public DataSet listarSaldosCuentasBancariasLaPaz(string fecha)
        {
            string consulta = "select "+
                               " b.nombre,tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta, "+
                               " IF(t2.resultado>0,t2.resultado,0) as 'resultado' " +
                               " from  "+
                               " db_seguimientolpz_jyc.tb_banco b, db_seguimientolpz_jyc.tb_tipocuenta tc, db_seguimientolpz_jyc.tb_cuentabancaria cu "+
                               " left join "+
                               " ( "+
                               " select con.codcuentabanco,IFNULL((con.extractobancario-t1.montoCheque),con.extractobancario) as 'resultado'  " +
                               " from db_seguimientolpz_jyc.tb_conciliacionbancaria con "+
                               " left join "+
                               " (select che.codconciliacionbanco,sum(che.monto)as 'montoCheque' from db_seguimientolpz_jyc.tb_chequesencirculacion  "+
                               " che group by che.codconciliacionbanco) as t1 "+
                               " ON con.codigo = t1.codconciliacionbanco "+
                               " where "+
                               " con.fecha = '"+fecha+"' "+
                               " ) as t2 "+
                               " ON cu.codigo = t2.codcuentabanco "+
                               " where b.codigo = cu.codbanco and "+
                               " cu.codtipocuenta = tc.codigo ";
            return Dbanco.getDatos(consulta);
        }

        public DataSet listarSaldosCuentasBancariasSantaCruz(string fecha)
        {
            string consulta = "select "+
                                   " b.nombre,tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta, "+
                                   " IF(t2.resultado>0,t2.resultado,0) as 'resultado' " +
                                   " from  "+
                                   " db_seguimientoscz_jyc.tb_banco b, db_seguimientoscz_jyc.tb_tipocuenta tc, db_seguimientoscz_jyc.tb_cuentabancaria cu "+
                                   " left join "+
                                   " ( "+
                                // esto se cambio  = IFNULL(con.extractobancario,(con.extractobancario-t1.montoCheque)
                                   " select con.codcuentabanco,IFNULL((con.extractobancario-t1.montoCheque),con.extractobancario) as 'resultado' " +
                                   " from db_seguimientoscz_jyc.tb_conciliacionbancaria con  "+
                                   " left join "+
                                   " (select che.codconciliacionbanco,sum(che.monto)as 'montoCheque' from db_seguimientoscz_jyc.tb_chequesencirculacion "+
                                   " che group by che.codconciliacionbanco) as t1 "+
                                   " ON con.codigo = t1.codconciliacionbanco "+
                                   " where "+
                                   " con.fecha = '"+fecha+"' "+
                                   " ) as t2 "+
                                   " ON cu.codigo = t2.codcuentabanco "+
                                   " where b.codigo = cu.codbanco and "+
                                   " cu.codtipocuenta = tc.codigo ";
            return Dbanco.getDatos(consulta);
        }


        public DataSet listarSaldosCuentasBancariasJYCSRL(string fecha)
        {
            string consulta = "select  "+
                               " b.nombre,tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta, "+
                               " IF(t2.resultado>0,t2.resultado,0) as 'resultado' "+
                               " from  "+
                               " db_seguimientojycsrl_jyc.tb_banco b, db_seguimientojycsrl_jyc.tb_tipocuenta tc, db_seguimientojycsrl_jyc.tb_cuentabancaria cu "+
                               " left join "+
                               " ( "+
                               " select con.codcuentabanco,IFNULL((con.extractobancario-t1.montoCheque),con.extractobancario) as 'resultado' "+
                               " from db_seguimientojycsrl_jyc.tb_conciliacionbancaria con  "+
                               " left join "+
                               " (select che.codconciliacionbanco,sum(che.monto)as 'montoCheque' from db_seguimientojycsrl_jyc.tb_chequesencirculacion "+
                               " che group by che.codconciliacionbanco) as t1 "+
                               " ON con.codigo = t1.codconciliacionbanco "+
                               " where "+
                               " con.fecha = '"+fecha+"' "+
                               " ) as t2 "+
                               " ON cu.codigo = t2.codcuentabanco "+
                               " where b.codigo = cu.codbanco and "+
                               " cu.codtipocuenta = tc.codigo ";
            return Dbanco.getDatos(consulta);
        }


        public DataSet listarSaldosCuentasBancariasJYCIASRL(string fecha)
        {
            string consulta = "select "+
                               " b.nombre,tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta, "+
                               " IF(t2.resultado>0,t2.resultado,0) as 'resultado' "+
                               " from "+
                               " db_seguimientojyciasrl_jyc.tb_banco b, db_seguimientojyciasrl_jyc.tb_tipocuenta tc, db_seguimientojyciasrl_jyc.tb_cuentabancaria cu "+
                               " left join "+
                               " ( "+
                               " select con.codcuentabanco,IFNULL((con.extractobancario-t1.montoCheque),con.extractobancario) as 'resultado'  "+
                               " from db_seguimientojyciasrl_jyc.tb_conciliacionbancaria con  "+
                               " left join  "+
                               " (select che.codconciliacionbanco,sum(che.monto)as 'montoCheque' from db_seguimientojyciasrl_jyc.tb_chequesencirculacion  "+
                               " che group by che.codconciliacionbanco) as t1 "+
                               " ON con.codigo = t1.codconciliacionbanco "+
                               " where "+
                               " con.fecha = '"+fecha+"' "+
                               " ) as t2 "+
                               " ON cu.codigo = t2.codcuentabanco "+
                               " where b.codigo = cu.codbanco and "+
                               " cu.codtipocuenta = tc.codigo ";
            return Dbanco.getDatos(consulta);
        }


        public DataSet listarSaldosCuentasBancariasIMVEN(string fecha)
        {
            string consulta = "select "+ 
                               " b.nombre,tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta, "+
                               " IF(t2.resultado>0,t2.resultado,0) as 'resultado' "+
                               " from  "+
                               " db_seguimientoimven_jyc.tb_banco b, db_seguimientoimven_jyc.tb_tipocuenta tc, db_seguimientoimven_jyc.tb_cuentabancaria cu "+
                               " left join "+
                               " ( "+
                               " select con.codcuentabanco,IFNULL((con.extractobancario-t1.montoCheque),con.extractobancario) as 'resultado'  "+
                               " from db_seguimientoimven_jyc.tb_conciliacionbancaria con  "+
                               " left join "+
                               " (select che.codconciliacionbanco,sum(che.monto)as 'montoCheque' from db_seguimientoimven_jyc.tb_chequesencirculacion "+
                               " che group by che.codconciliacionbanco) as t1 "+
                               " ON con.codigo = t1.codconciliacionbanco "+
                               " where "+
                               " con.fecha = '"+fecha+"' "+
                               " ) as t2 "+
                               " ON cu.codigo = t2.codcuentabanco "+
                               " where b.codigo = cu.codbanco and "+
                               " cu.codtipocuenta = tc.codigo";
            return Dbanco.getDatos(consulta);
        }

        public DataSet obtenerChequesAnteriores(int codCuentaBancaria) {
            string consulta = "select "+ 
                               " date_format(che.fecha,'%d/%m/%Y') as 'fecha', "+
                               " che.nrocheque, che.monto  "+
                               " from tb_chequesencirculacion che "+
                               " where che.codconciliacionbanco = "+
                               " (select max(con.codigo) as codcon "+
                               " from tb_conciliacionbancaria con "+
                               " where con.codcuentabanco = "+codCuentaBancaria+")";
            return Dbanco.getDatos(consulta);
        }

        public DataSet VistaSaldosCuentaGeneral(string fecha1, string fecha2)
        {

            DateTime fecha1_aux = Convert.ToDateTime(fecha1);
            DateTime fecha2_aux = Convert.ToDateTime(fecha2);
            

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);

            dt.Columns.Add("Banco");
            dt.Columns.Add("TipoCuenta");
            dt.Columns.Add("Moneda");
            dt.Columns.Add("NroCuenta");


            //----------Corpal
            DataRow filaCorpal = dt.NewRow();
            filaCorpal["Banco"] = "Corpal";
            filaCorpal["TipoCuenta"] = "";
            filaCorpal["Moneda"] = "";
            filaCorpal["NroCuenta"] = "";
            dt.Rows.Add(filaCorpal);
            DataSet datosEstadoCorpal = listarCuentasBancariasCorpal();
            for (int k = 0; k < datosEstadoCorpal.Tables[0].Rows.Count; k++)
            {
                DataRow fila = dt.NewRow();
                fila["Banco"] = datosEstadoCorpal.Tables[0].Rows[k][0].ToString();
                fila["TipoCuenta"] = datosEstadoCorpal.Tables[0].Rows[k][1].ToString();
                fila["Moneda"] = datosEstadoCorpal.Tables[0].Rows[k][2].ToString();
                fila["NroCuenta"] = datosEstadoCorpal.Tables[0].Rows[k][3].ToString();
                dt.Rows.Add(fila);
            }
            //-----------end Corpal
            
            
            

            for (DateTime i = fecha1_aux; i <= fecha2_aux; i=i.AddDays(1))
            {
                int mes1 = i.Month;
                int anio1 = i.Year;
                int dia = i.Day;
                string fechaActual = anio1 + "-" + mes1 + "-" + dia;
                string nombreColumna = dia + "/" + mes1 + "/" + anio1;
                dt.Columns.Add(nombreColumna);

                //-----------------formato de numeros con miles y decimales ----------
                NumberFormatInfo nfi = new CultureInfo("es-BO", false).NumberFormat;
                
                //--------------------------------------------------------------------
                ///--------------Corpal
                DataSet datoCorpal = listarSaldosCuentasBancariasCORPAL(fechaActual);
                int cantCorpal = datoCorpal.Tables[0].Rows.Count;
                for (int j = 0; j < cantCorpal; j++)
                {
                    string tupla = datoCorpal.Tables[0].Rows[j][4].ToString();
                    dt.Rows[j + 1][nombreColumna] = Convert.ToSingle(tupla).ToString("C", nfi).Replace(nfi.CurrencySymbol, "");
                }
                ///-------------- end Corpal
                

            }

            return ds;
        }

        private DataSet listarSaldosCuentasBancariasCORPAL(string fechaActual)
        {
            string consulta = "select " +
                                " b.nombre,tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta, " +
                                " IF(t2.resultado>0,t2.resultado,0) as 'resultado' " +
                                " from  " +
                                " db_seguimientocorpal_jyc.tb_banco b, db_seguimientocorpal_jyc.tb_tipocuenta tc, db_seguimientocorpal_jyc.tb_cuentabancaria cu " +
                                " left join " +
                                " ( " +
                                " select con.codcuentabanco,IFNULL((con.extractobancario-t1.montoCheque),con.extractobancario) as 'resultado'  " +
                                " from db_seguimientocorpal_jyc.tb_conciliacionbancaria con  " +
                                " left join " +
                                " (select che.codconciliacionbanco,sum(che.monto)as 'montoCheque' from db_seguimientocorpal_jyc.tb_chequesencirculacion " +
                                " che group by che.codconciliacionbanco) as t1 " +
                                " ON con.codigo = t1.codconciliacionbanco " +
                                " where " +
                                " con.fecha = '" + fechaActual + "' " +
                                " ) as t2 " +
                                " ON cu.codigo = t2.codcuentabanco " +
                                " where b.codigo = cu.codbanco and " +
                                " cu.codtipocuenta = tc.codigo  and cu.estado = 1";
            return Dbanco.getDatos(consulta);
        }

        private DataSet listarCuentasBancariasCorpal()
        {
            string consulta = "select b.nombre as 'banco',tc.nombre as 'tipoCuenta',cu.moneda,cu.nrocuenta " +
                               " from db_seguimientocorpal_jyc.tb_banco b, db_seguimientocorpal_jyc.tb_cuentabancaria cu , db_seguimientocorpal_jyc.tb_tipocuenta tc " +
                               " where b.codigo = cu.codbanco and " +
                               " cu.codtipocuenta = tc.codigo  and cu.estado = 1";
            return Dbanco.getDatos(consulta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CuentaBancaria">dato de cuenta o banco</param>
        /// <returns>lista de del dato introducido ya sea la cuenta o banco </returns>
        public DataSet getCuentaBancaria(string CuentaBancaria) {
            return Dbanco.getCuentaBancaria(CuentaBancaria);
        }


        internal string get_CodigoCuentaBancaria_Debe(string cuentaBancoAux)
        {
            DataSet dato = Dbanco.get_CodigoCuentaBancaria(cuentaBancoAux);
            if(dato.Tables[0].Rows.Count > 0){
                return dato.Tables[0].Rows[0][2].ToString();
            }else
                return "Ninguno";
        }

        internal string get_CodigoCuentaBancaria_Haber(string cuentaBancoAux)
        {
            DataSet dato = Dbanco.get_CodigoCuentaBancaria(cuentaBancoAux);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return dato.Tables[0].Rows[0][3].ToString();
            }
            else
                return "Ninguno";
        }

        internal string get_NombreBanco_CuentaBancaria_banco(string cuentaBancoAux)
        {
            DataSet dato = Dbanco.get_CodigoCuentaBancaria(cuentaBancoAux);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return dato.Tables[0].Rows[0][4].ToString();
            }
            else
                return "Ninguno";
        }


        internal bool updateconciliacionBancaria(string saldoAnterior, string extractoBancario, int codcuentaBancaria, int CodUser)
        {
            return Dbanco.updateconciliacionBancaria( saldoAnterior,  extractoBancario,  codcuentaBancaria,  CodUser);
        }
    }
}