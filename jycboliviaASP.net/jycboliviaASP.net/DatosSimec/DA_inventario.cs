using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.DatosSimec
{
    public class DA_inventario
    {
        public DA_inventario() { }

        public bool insertarCobranza(int MONEDA,float TC,string GLOSA, string USUARIO, string fechaPago){
            string consulta = "insert into cobranza( "+
                               " cobranza.DOCUM,cobranza.MONEDA,cobranza.FECHA,cobranza.TC,cobranza.GLOSA, "+
                               " cobranza.USUARIO,cobranza.HORAGRA,cobranza.FECHAGRA) "+
                               " select max(cobranza.DOCUM)+1 , "+MONEDA+", "+fechaPago+" ,'"+TC.ToString().Replace(',','.')+"','"+GLOSA+"','"+USUARIO+"', current_time , current_date "+
                               " from cobranza";
            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_Simec();
            return cnx.ejecutarMySql(consulta);            
        }

        public bool insertarDetalleCobranza1(string DOCUM,string CLICODIGO, string RECIBO, string BANCO, string CHEQUE, 
                               float IMPORTE, string Observacion, string codvendedor, int MONEDACLIENTE, float TC, int MONEDACOB,
                               string FACTURADIF, string TIPO_PAGO, string VCAJA, string fechaPago)
        {
            string consulta = "insert into cobranza1 "+
                               " (cobranza1.DOCUM,cobranza1.CLICODIGO,cobranza1.RECIBO,cobranza1.FECHA, "+
                               " cobranza1.VDOCUM, cobranza1.VCAJA, cobranza1.BANCO, cobranza1.CHEQUE, "+
                               " cobranza1.IMPORTE, cobranza1.OBS, cobranza1.VENCODIGO, cobranza1.RECHAZADO, "+
                               " cobranza1.DESCUENTOS, cobranza1.MONEDA, cobranza1.TC, cobranza1.MONEDACOB, "+
                               " cobranza1.INTERESES, cobranza1.MULTAS, cobranza1.SEGUROS, cobranza1.FACTURADIF, "+
                               " cobranza1.TIPO_PAGO) "+
                               " values "+
                               " ('"+DOCUM+"','"+CLICODIGO+"','"+RECIBO+"', "+fechaPago+", "+
                               " '00000000', '"+VCAJA+"', '"+BANCO+"', '"+CHEQUE+"', "+
                               " '"+IMPORTE.ToString().Replace(',','.')+"', '"+Observacion+"', '"+codvendedor+"', 0, "+
                               " 0, '"+MONEDACLIENTE+"', '"+TC.ToString().Replace(',','.')+"', "+MONEDACOB+", "+
                               " 0, 0, 0, '"+FACTURADIF+"', "+
                               " '"+TIPO_PAGO+"');";
            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_Simec();
            return cnx.ejecutarMySql(consulta);
        }

        internal bool insertarDetalleCobranza_CLIKAR(string DOCUM, string CLICODIGO, string RECIBO, string DETALLE, float HABER, int CLIMONEDA, string VENCODIGO, string VCAJA, string fechaPago){
            string consulta = "insert into clikar(clikar.DOCUM, clikar.CLICODIGO, clikar.VDOCUM,clikar.VCAJA, "+
                               " clikar.RECIBO,clikar.FECHA, clikar.DETALLE, clikar.DEBE, clikar.HABER, clikar.SALDO, "+
                               " clikar.CLIMONEDA, clikar.VENCODIGO, clikar.INTERESES, "+
                               " clikar.MULTAS, clikar.SEGUROS, clikar.ANTICIPOS, clikar.TIPOMOV) " +
                               " values "+
                               " ('" + DOCUM + "','" + CLICODIGO + "', '00000000','" + VCAJA + "', " +
                              "'" + RECIBO + "', " + fechaPago + ", '" + DETALLE + "', 0, '" + HABER.ToString().Replace(',', '.') + "', 0, " +
                               CLIMONEDA + ", '" + VENCODIGO + "', 0, 0, 0, 0, 'CCO')";
            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_Simec();
            return cnx.ejecutarMySql(consulta);
        }


        internal string getUltimoDocumInsertado()
        {
            string consulta = "select max(cobranza.DOCUM)  from cobranza";
            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_Simec();
            DataSet dato = cnx.consultaMySql(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return dato.Tables[0].Rows[0][0].ToString();
            }
            else
                return "0";
        }

        internal int getMonedaClienteSimecInv(string codCliente)
        {
            string consulta = "select cc.CLIMONEDA from clientes cc where cc.CLICODIGO = '"+codCliente+"';";
            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_Simec();
            DataSet dato = cnx.consultaMySql(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dato.Tables[0].Rows[0][0].ToString());
            }
            else
                return 2;
        }

        internal string getNombreClienteSimec(string CLIENTECOD)
        {
            string consulta = "select cc.CLICODIGO, cc.CLINOMBRES  "+
                               " from clientes cc where cc.CLICODIGO = '"+CLIENTECOD+"'";

            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_Simec();
            DataSet dato = cnx.consultaMySql(consulta);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return dato.Tables[0].Rows[0][1].ToString();
            }
            else
                return "Ninguno";
        }


        public bool insert_PlanPagoInv_Ventas_JYCIA(string VDOCUM, string VDOCUMA, string VFECHA,	
                                string VNOMBRE,string VRUC, int VCAJA,	
                                string VTRANS,float	VTC, string VMONEDA,	
                                string VUSUARIO,string AGECODIGO,string CLICODIGO,	
                                string VENCODIGO,int VTIPRE,string VGLOSA,	
                                int VDIAS,string VENCIMIENTO,float TOTALFAC,	
                                string TOTALLIT,string XENTREGAR,string NO_CUENTA) {
            string consulta = "INSERT INTO ventas("+
                               " VDOCUM,VDOCUMA,VFECHA,"+
                               " VNOMBRE,VRUC,VCAJA,"+	
                               " VTRANS,VTC,VMONEDA,"+	
                               " VUSUARIO,AGECODIGO,CLICODIGO,"+	
                               " VENCODIGO,VTIPRE, VGLOSA,"+
                               " VDIAS,VENCIMIENTO,TOTALFAC,"+	
                               " TOTALLIT,XENTREGAR,HORAGRA,"+
                               " FECHAGRA, NO_CUENTA, "+
                               " DESCODIGO,VDESCUENTOS,PROFORMA,ICE,DESCUENTOS,OTROS,DESC1,DESC2,DESC3,ENTREGADOBS,ENTREGADOUS,CAMBIO,HOJA,DESC4)" +
                               " values("+
                               " '"+VDOCUM+"','"+VDOCUMA+"',"+VFECHA+","+
                               " '"+VNOMBRE+"','"+VRUC+"',"+VCAJA+","+	
                               " '"+VTRANS+"','"+VTC.ToString().Replace(',','.')+"','"+VMONEDA+"',"+	
                               " '"+VUSUARIO+"','"+AGECODIGO+"','"+CLICODIGO+"', "+	
                               " '"+VENCODIGO+"', "+VTIPRE+", '"+VGLOSA+"', "+
                               VDIAS+", '"+VENCIMIENTO+"', '"+TOTALFAC.ToString().Replace(',','.')+"', "+
                               " '"+TOTALLIT+"', '"+XENTREGAR+"', current_time, " +
                               " current_date(), '"+NO_CUENTA+"',1,0,0,0,0,0,0,0,0,0,0,0,0,0)";

            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_InvSimecJYCIA();
           return cnx.ejecutarMySql(consulta);
           
        }

        public bool insert_PlanPagoInv_Ventas1_JYCIA(string VDOCUM, string VDOCUMA, string AGECODIGO, string CLICODIGO,
                                                   string VENCODIGO, string VFECHA, float VTC, int VCAJA,
                                                   string VMONEDA, string CODIGO, int VCANTIDAD, float VPREUNIL,
                                                   float VPREUNI, int VTIPRE, string XENTREGAR, string CLOTE,
                                                   string VTRANS, float TOTAL)
        {
            string consulta = "INSERT INTO ventas1( " +
                               " VDOCUM,VDOCUMA,AGECODIGO,CLICODIGO, " +
                               " VENCODIGO,VFECHA,VTC,VCAJA, " +
                               " VMONEDA,CODIGO,VCANTIDAD,VPREUNIL," +
                               " VPREUNI,VTIPRE,XENTREGAR,CLOTE," +
                               " VTRANS,TOTAL,VDESCUENTO,VCANTIDAD1,ICE,PROFORMA,ICEADICIONAL,VCANTIDADP) VALUES(" +
                               " '" + VDOCUM + "','" + VDOCUMA + "','" + AGECODIGO + "','" + CLICODIGO + "'," +
                               " '" + VENCODIGO + "'," + VFECHA + ",'" + VTC.ToString().Replace(',','.') + "'," + VCAJA + "," +
                               VMONEDA + ",'" + CODIGO + "'," + VCANTIDAD + ",'" + VPREUNIL.ToString().Replace(',', '.') + "'," +
                               " '" + VPREUNI.ToString().Replace(',', '.') + "'," + VTIPRE + ",'" + XENTREGAR + "','" + CLOTE + "'," +
                               " '" + VTRANS + "','" + TOTAL.ToString().Replace(',', '.') + "',0,0,0,0,0,0)";

            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_InvSimecJYCIA();
            return cnx.ejecutarMySql(consulta);
        }

        public bool insert_PlanPagoInv_PlanPagos_JYCIA(string TABLA,string NCTA,string DOCUM,string CAJA,
                                                       string CODIGO,string MONEDA,string NO_CUOTA,string FECHA,
                                                       string IMPORTE, string SALDO, string CUOTA,string GLOSA)
        {
            string consulta = "INSERT INTO PLANPAGOS "+
                               " (TABLA,NCTA,DOCUM,CAJA, "+
                               " CODIGO,MONEDA,NO_CUOTA,FECHA, "+
                               " IMPORTE,SALDO,CUOTA,GLOSA) "+
                               " VALUES( "+
                               " '"+TABLA+"','"+NCTA+"','"+DOCUM+"','"+CAJA+"', "+
                               " '"+CODIGO+"','"+MONEDA+"','"+NO_CUOTA+"',"+FECHA+", "+
                               " '"+IMPORTE+"','"+SALDO+"','"+CUOTA+"','"+GLOSA+"')";

            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_InvSimecJYCIA();
            return cnx.ejecutarMySql(consulta);
        }

        
        public DataSet get_DocumyDocumaINV_JYCIA(int vcaja){
        string consulta = "select  max(v.vdocum)+1 as 'numsiguiente', "+
                           " concat("+vcaja+" div 10, max(v.vdocum))+1 as 'vdocuma' " +
                           " from ventas v "+
                           " where "+
                           " v.VCAJA = "+vcaja;
            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_InvSimecJYCIA();
           return cnx.consultaMySql(consulta);
        }

        public DataSet get_DocumyDocumaINV_JYCIA1(int vcaja)
        {
            string consulta = "select  max(v.vdocum)+1 as 'numsiguiente', " +
                               " concat(" + vcaja + " div 10, max(v.vdocum))+1 as 'vdocuma' " +
                               " from ventas v " +
                               " where " +
                               " v.VCAJA = " + vcaja;
            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_InvSimecJYCIA();
            return cnx.consultaMySql(consulta);
        }

        public string get_correlativoDocumINV_JYCIA(int vcaja) {
            DataSet tupla = get_DocumyDocumaINV_JYCIA1(vcaja);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                string numeroCorrelativo = tupla.Tables[0].Rows[0][0].ToString();
                int cantletras = numeroCorrelativo.Length;
                int cantCero = 8 - cantletras;
                string ceros_Inicio = "";
                for (int i = 0; i < cantCero; i++)
                {
                    ceros_Inicio = ceros_Inicio + "0";
                }
                string final = ceros_Inicio + numeroCorrelativo;
                return final;
            }
            else
                return "00000001";
        }

        public string get_correlativoDocuMAINV_JYCIA(int vcaja)
        {
            DataSet tupla = get_DocumyDocumaINV_JYCIA(vcaja);
            if (tupla.Tables[0].Rows.Count > 0)
            {
                string numeroCorrelativo = tupla.Tables[0].Rows[0][1].ToString();
                return numeroCorrelativo;
            }
            else
                return  vcaja.ToString()+"0000001";
        }

        public DataSet get_datosClienteInv_JYCIA(string clicodigo) {
            string consulta = "select CLICODIGO,  CLINOMBRES,  CLITITULAR, " +
                           " CLIDIRECCION,  CLITELEFONO, CLIRUC, "+
                           " CLIFECHA_APER,  CLIMONEDA,  ESTADO, "+ 
                           " CLINOMBRESFAC,  APER_FECHA,  APER_USUARIO, "+ 
                           " MODI_FECHA,  MODI_USUARIO,  NITTIPO, NCTA "+ 
                           " from clientes cc "+
                           " where "+
                           " cc.CLICODIGO = '"+clicodigo+"'";
            conexionMySql cnx = new conexionMySql();
            cnx.conexionMySql_InvSimecJYCIA();
            return cnx.consultaMySql(consulta);        
        }


    }
}