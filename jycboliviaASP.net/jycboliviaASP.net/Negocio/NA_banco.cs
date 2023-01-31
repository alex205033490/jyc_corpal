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

            //----------- santa Cruz
            DataRow filaScz = dt.NewRow();
            filaScz["Banco"] = "Santa Cruz";
            filaScz["TipoCuenta"] = "";
            filaScz["Moneda"] = "";
            filaScz["NroCuenta"] = "";            
            dt.Rows.Add(filaScz);

            DataSet datosEstadoSCZ = listarCuentasBancariasSantaCruz();
            for (int k = 0; k < datosEstadoSCZ.Tables[0].Rows.Count; k++)
            {
                DataRow fila = dt.NewRow();
                fila["Banco"] = datosEstadoSCZ.Tables[0].Rows[k][0].ToString();
                fila["TipoCuenta"] = datosEstadoSCZ.Tables[0].Rows[k][1].ToString();
                fila["Moneda"] = datosEstadoSCZ.Tables[0].Rows[k][2].ToString();
                fila["NroCuenta"] = datosEstadoSCZ.Tables[0].Rows[k][3].ToString();
                dt.Rows.Add(fila);
            }

            //---------end santa cruz
            //---------cochabamba
            DataRow filacbba = dt.NewRow();
            filacbba["Banco"] = "Cochabamba";
            filacbba["TipoCuenta"] = "";
            filacbba["Moneda"] = "";
            filacbba["NroCuenta"] = "";
            dt.Rows.Add(filacbba);
            DataSet datosEstadoCbba = listarCuentasBancariasCochabamba();
            for (int k = 0; k < datosEstadoCbba.Tables[0].Rows.Count; k++)
            {
                DataRow fila = dt.NewRow();
                fila["Banco"] = datosEstadoCbba.Tables[0].Rows[k][0].ToString();
                fila["TipoCuenta"] = datosEstadoCbba.Tables[0].Rows[k][1].ToString();
                fila["Moneda"] = datosEstadoCbba.Tables[0].Rows[k][2].ToString();
                fila["NroCuenta"] = datosEstadoCbba.Tables[0].Rows[k][3].ToString();
                dt.Rows.Add(fila);
            }
            //---------end Cochabamba
            //---------- La Paz
            DataRow filalpz = dt.NewRow();
            filalpz["Banco"] = "La Paz";
            filalpz["TipoCuenta"] = "";
            filalpz["Moneda"] = "";
            filalpz["NroCuenta"] = "";            
            dt.Rows.Add(filalpz);
            DataSet datosEstadoLpz = listarCuentasBancariasLaPaz();
            for (int k = 0; k < datosEstadoLpz.Tables[0].Rows.Count; k++)
            {
                DataRow fila = dt.NewRow();
                fila["Banco"] = datosEstadoLpz.Tables[0].Rows[k][0].ToString();
                fila["TipoCuenta"] = datosEstadoLpz.Tables[0].Rows[k][1].ToString();
                fila["Moneda"] = datosEstadoLpz.Tables[0].Rows[k][2].ToString();
                fila["NroCuenta"] = datosEstadoLpz.Tables[0].Rows[k][3].ToString();
                dt.Rows.Add(fila);
            }

            //---------en la paz
            //----------JYC SRL
            DataRow filajycSrl = dt.NewRow();
            filajycSrl["Banco"] = "JyC SRL";
            filajycSrl["TipoCuenta"] = "";
            filajycSrl["Moneda"] = "";
            filajycSrl["NroCuenta"] = "";
            dt.Rows.Add(filajycSrl);
            DataSet datosEstadoJYCSRL = listarCuentasBancariasJYCSRL();
            for (int k = 0; k < datosEstadoJYCSRL.Tables[0].Rows.Count; k++)
            {
                DataRow fila = dt.NewRow();
                fila["Banco"] = datosEstadoJYCSRL.Tables[0].Rows[k][0].ToString();
                fila["TipoCuenta"] = datosEstadoJYCSRL.Tables[0].Rows[k][1].ToString();
                fila["Moneda"] = datosEstadoJYCSRL.Tables[0].Rows[k][2].ToString();
                fila["NroCuenta"] = datosEstadoJYCSRL.Tables[0].Rows[k][3].ToString();
                dt.Rows.Add(fila);
            }
            //----------end JYCSRL
            //---------- JYCIA SRL
            DataRow filajyciaSrl = dt.NewRow();
            filajyciaSrl["Banco"] = "JyCIA SRL";
            filajyciaSrl["TipoCuenta"] = "";
            filajyciaSrl["Moneda"] = "";
            filajyciaSrl["NroCuenta"] = "";
            dt.Rows.Add(filajyciaSrl);
            DataSet datosEstadoJYCIASRL = listarCuentasBancariasJYCIASRL();
            for (int k = 0; k < datosEstadoJYCIASRL.Tables[0].Rows.Count; k++)
            {
                DataRow fila = dt.NewRow();
                fila["Banco"] = datosEstadoJYCIASRL.Tables[0].Rows[k][0].ToString();
                fila["TipoCuenta"] = datosEstadoJYCIASRL.Tables[0].Rows[k][1].ToString();
                fila["Moneda"] = datosEstadoJYCIASRL.Tables[0].Rows[k][2].ToString();
                fila["NroCuenta"] = datosEstadoJYCIASRL.Tables[0].Rows[k][3].ToString();
                dt.Rows.Add(fila);
            }

            //----------end JYCIA SRL
            //----------Imven
            DataRow filaImven = dt.NewRow();
            filaImven["Banco"] = "Imven";
            filaImven["TipoCuenta"] = "";
            filaImven["Moneda"] = "";
            filaImven["NroCuenta"] = "";
            dt.Rows.Add(filaImven);
            DataSet datosEstadoImven = listarCuentasBancariasImven();
            for (int k = 0; k < datosEstadoImven.Tables[0].Rows.Count; k++)
            {
                DataRow fila = dt.NewRow();
                fila["Banco"] = datosEstadoImven.Tables[0].Rows[k][0].ToString();
                fila["TipoCuenta"] = datosEstadoImven.Tables[0].Rows[k][1].ToString();
                fila["Moneda"] = datosEstadoImven.Tables[0].Rows[k][2].ToString();
                fila["NroCuenta"] = datosEstadoImven.Tables[0].Rows[k][3].ToString();
                dt.Rows.Add(fila);
            }

            //-----------end Imven


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


                //---------- santa cruz
                DataSet datoSCZ = listarSaldosCuentasBancariasSantaCruz(fechaActual);                
                int cantSCZ = datoSCZ.Tables[0].Rows.Count;
                for (int j = 0; j < cantSCZ; j++)
                {
                    string tupla = datoSCZ.Tables[0].Rows[j][4].ToString();
                    dt.Rows[j + 1][nombreColumna] = Convert.ToSingle(tupla).ToString("C", nfi).Replace(nfi.CurrencySymbol,"");
                }
                //-----------end santa cruz
                //----------- cochabamba
                DataSet datocbba = listarSaldosCuentasBancariasCochabamba(fechaActual);
                int cantcbba = datocbba.Tables[0].Rows.Count;
                for (int j = 0; j < cantcbba; j++)
                {
                    string tupla = datocbba.Tables[0].Rows[j][4].ToString();
                    int filaktoca = j + (2 + cantSCZ);
                    dt.Rows[filaktoca][nombreColumna] = Convert.ToSingle(tupla).ToString("C", nfi).Replace(nfi.CurrencySymbol, "");
                }
                //---------- end cochabamba
                //---------- La PAz
                DataSet datolpz = listarSaldosCuentasBancariasLaPaz(fechaActual);
                int cantlpz = datolpz.Tables[0].Rows.Count;
                for (int j = 0; j < cantlpz; j++)
                {
                    string tupla = datolpz.Tables[0].Rows[j][4].ToString();
                    int filaktoca = j + (3 + cantSCZ+cantcbba);
                    dt.Rows[filaktoca][nombreColumna] = Convert.ToSingle(tupla).ToString("C", nfi).Replace(nfi.CurrencySymbol, "");
                }
                ///--------------end la paz
                ///--------------JYC SRL
                DataSet datoJYCSRL = listarSaldosCuentasBancariasJYCSRL(fechaActual);
                int cantJYCSRL = datoJYCSRL.Tables[0].Rows.Count;
                for (int j = 0; j < cantJYCSRL; j++)
                {
                    string tupla = datoJYCSRL.Tables[0].Rows[j][4].ToString();
                    int filaktoca = j + (4 + cantSCZ + cantcbba + cantlpz);
                    dt.Rows[filaktoca][nombreColumna] = Convert.ToSingle(tupla).ToString("C", nfi).Replace(nfi.CurrencySymbol, "");
                }
                ///-------------End JYC SRL
                ///-------------JYCIA SRL
                DataSet datoJYCIASRL = listarSaldosCuentasBancariasJYCIASRL(fechaActual);
                int cantJYCIASRL = datoJYCIASRL.Tables[0].Rows.Count;
                for (int j = 0; j < cantJYCIASRL; j++)
                {
                    string tupla = datoJYCIASRL.Tables[0].Rows[j][4].ToString();
                    int filaktoca = j + (5 + cantSCZ + cantcbba + cantlpz + cantJYCSRL);
                    dt.Rows[filaktoca][nombreColumna] = Convert.ToSingle(tupla).ToString("C", nfi).Replace(nfi.CurrencySymbol, "");
                }
                ///---------------end JYCIA SRL
                ///--------------Imven
                DataSet datoImven = listarSaldosCuentasBancariasIMVEN(fechaActual);
                int cantImven = datoImven.Tables[0].Rows.Count;
                for (int j = 0; j < cantImven; j++)
                {
                    string tupla = datoImven.Tables[0].Rows[j][4].ToString();
                    int filaktoca = j + (6 + cantSCZ + cantcbba + cantlpz + cantJYCSRL + cantJYCIASRL);
                    dt.Rows[filaktoca][nombreColumna] = Convert.ToSingle(tupla).ToString("C", nfi).Replace(nfi.CurrencySymbol, "");
                }
                ///-------------- end Imven


            }

            return ds;
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

    }
}