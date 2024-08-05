using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using jycboliviaASP.net.Reportes;
using System.Configuration;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ReporteCotizacionRepuesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (!IsPostBack)
            {
                int codigoCotizacion = Convert.ToInt32(Session["CodigoCotizacion"].ToString());
                generarCotizacion(codigoCotizacion);
            }
        }



        public string[] getCuentasDeRepuesto( string UNE, string BaseDatos) {
            string[] vector = new string[3];
            string almacen="XXXXXXXX";
            string CuentaBolivianos = "XXXXXXXX";
            string CuentaDorales = "XXXXXXXX";

            if (UNE.Equals("JYC"))
            {
                almacen = "JyC SRL.";
                CuentaBolivianos = "Banco JYC - Caja de ahorro Moneda Nacional(Bs) Nº XXXXXXXXXXXX";
                CuentaDorales = "Banco JYC - Caja de ahorro Moneda Extranjera ($us) Nº XXXXXXXXXXXX";
            }
            
            if (UNE.Equals("JYCIA")){
                    almacen = "JyCIA SRL.";
                    if (BaseDatos.Equals("Santa Cruz") || BaseDatos.Equals("Beni") || BaseDatos.Equals("Pando") || BaseDatos.Equals("Yacuiba"))
                    {
                       // CuentaBolivianos = "Banco Fassil - Cuenta Corriente Moneda Nacional(Bs) Nº 843110";
                        CuentaBolivianos = "";
                        CuentaDorales = "Banco Fassil - Cuenta Corriente Moneda Extranjera ($us) Nº 843220";                       
                    }else
                        if (BaseDatos.Equals("Cochabamba"))
                        {
                          //  CuentaBolivianos = "Banco Fassil - Cuenta Corriente Moneda Nacional(Bs) Nº 843310";
                          //  CuentaDorales = "Banco Fassil - Cuenta Corriente Moneda Extranjera ($us) Nº 843420";
                            CuentaBolivianos = "";
                            CuentaDorales = "Banco Fassil - Cuenta Corriente Moneda Extranjera ($us) Nº 843420";
                        }else
                            if (BaseDatos.Equals("La Paz"))
                            { 
                               // CuentaBolivianos = "Banco Fassil - Cuenta Corriente Moneda Nacional(Bs) Nº 842910";
                               // CuentaDorales = "Banco Fassil - Cuenta Corriente Moneda Extranjera ($us) Nº 843020";
                                CuentaBolivianos = "";
                                CuentaDorales = "Banco Fassil - Cuenta Corriente Moneda Extranjera ($us) Nº 843020";
                            }
                            else
                            { 
                                CuentaBolivianos = "Banco XXX - Caja de ahorro Moneda Nacional(Bs) Nº XXXXXXXXXXXX";
                                CuentaDorales = "Banco XXX - Caja de ahorro Moneda Extranjera ($us) Nº XXXXXXXXXXXX";
                            }
            }

            if (UNE.Equals("Local") || UNE.Equals(""))
            {
                almacen = Session["ID_UNE"].ToString();
                if (BaseDatos.Equals("Santa Cruz") || BaseDatos.Equals("Beni") || BaseDatos.Equals("Pando") || BaseDatos.Equals("Yacuiba"))
                    {                        
                       // CuentaBolivianos = "Banco Fassil - Caja Ahorro Moneda Nacional(Bs) Nº 3848351";
                       // CuentaDorales = "Banco Fassil - Caja Ahorro Moneda Extranjera ($us) Nº 3591822";
                        CuentaBolivianos = "";
                        CuentaDorales = "Banco Fassil - Caja Ahorro Moneda Extranjera ($us) Nº 3591822";
                    }else
                    if (BaseDatos.Equals("Cochabamba") || BaseDatos.Equals("Prueba Cochabamba"))
                        {
                           // CuentaBolivianos = "Banco Fassil - Caja Ahorro Moneda Nacional(Bs) Nº 3876251";
                           // CuentaDorales = "Banco Fassil - Caja Ahorro Moneda Extranjera ($us) Nº 3876262";
                            CuentaBolivianos = "";
                            CuentaDorales = "Banco Nacional de Bolivia - Cuenta Corriente Moneda Extranjera ($us) Nº 2400101577";
                        }else
                            if (BaseDatos.Equals("La Paz"))
                            { 
                               // CuentaBolivianos = "Banco BNB - Caja Ahorro Moneda Nacional(Bs) Nº 250-1000763";
                               // CuentaDorales = "Banco BNB - Caja Ahorro Moneda Extranjera ($us) Nº 290-1052916";
                                CuentaBolivianos = "";
                                CuentaDorales = "Banco BNB - Caja Ahorro Moneda Extranjera ($us) Nº 290-1052916";
                            }
                            else
                            { 
                                CuentaBolivianos = "Banco XXX - Caja de ahorro Moneda Nacional(Bs) Nº XXXXXXXXXXXX";
                                CuentaDorales = "Banco XXX - Caja de ahorro Moneda Extranjera ($us) Nº XXXXXXXXXXXX";
                            }
        } 
            vector[0]= almacen;
            vector[1]=CuentaBolivianos;
            vector[2]=CuentaDorales;
           return vector;
        }


        public void generarCotizacion(int nroCotizacion) {

            NA_Repuesto nrepu = new NA_Repuesto();
            DataSet datoResult = nrepu.getCotiR144(nroCotizacion);

            string edificioAux = datoResult.Tables[0].Rows[0][3].ToString();
            string numeroCotiAux = datoResult.Tables[0].Rows[0][5].ToString();
            string fechaAux = datoResult.Tables[0].Rows[0][1].ToString();
            N_numLetra nl = new N_numLetra();
            string precioTotalAux = "Son : " + nl.Convertir(datoResult.Tables[0].Rows[0][6].ToString(), true, "Dólares Americanos");

            string ellosAux = "el ascensor";
            string instaladosAux = "instalado";
            string codcotiRepuesto = datoResult.Tables[0].Rows[0][0].ToString();
            
            NA_Repuesto nrepuesto = new NA_Repuesto();
            DataSet tuplasDataSet = nrepuesto.getDetallesRepuestoR144(nroCotizacion);
            DataTable filasConsulta = tuplasDataSet.Tables[0];


            DataTable datoRepuesto = new DataTable();
            datoRepuesto.Columns.Add("Codigo", typeof(string));
            datoRepuesto.Columns.Add("numeracion", typeof(string));
            datoRepuesto.Columns.Add("Detalle", typeof(string));
            datoRepuesto.Columns.Add("Precio", typeof(string));
            datoRepuesto.Columns.Add("Cantidad", typeof(string));
            datoRepuesto.Columns.Add("PrecioTotal", typeof(string));

            for (int i = 0; i < filasConsulta.Rows.Count; i++)
            {
                DataRow row = filasConsulta.Rows[i];
                DataRow tupla = datoRepuesto.NewRow();
                tupla["Codigo"] = row[0].ToString();
                tupla["numeracion"] = row[1].ToString();
                tupla["Detalle"] = row[2].ToString();
                tupla["Cantidad"] = row[3].ToString();
                tupla["Precio"] = row[4].ToString();
                tupla["PrecioTotal"] = row[5].ToString();
                datoRepuesto.Rows.Add(tupla);
            }

            DataRow tupla1 = datoRepuesto.NewRow();
            tupla1["Codigo"] = "";
            tupla1["Detalle"] = "";
            tupla1["Cantidad"] = "";
            tupla1["Precio"] = "Total";
            tupla1["PrecioTotal"] = datoResult.Tables[0].Rows[0][6].ToString();
            datoRepuesto.Rows.Add(tupla1);

            // Se carga la ruta física de la carpeta temp del sitio            
            string ruta = ConfigurationManager.AppSettings["qr_code"] + Session["BaseDatos"].ToString();
            string montoTotalRepuestos = datoResult.Tables[0].Rows[0][6].ToString();
            // Si el directorio no existe, crearlo

            string UneLocal = datoResult.Tables[0].Rows[0][8].ToString();
            string Une = "";
            string CuentaBolivianos = "";
            string CuentaDorales = "";

            string[] vdatosCuentas;
            vdatosCuentas = getCuentasDeRepuesto(UneLocal, Session["BaseDatos"].ToString());
            Une = vdatosCuentas[0].ToString();
            CuentaBolivianos = vdatosCuentas[1].ToString();
            CuentaDorales = vdatosCuentas[2].ToString();


            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            string nombreArchivo = codcotiRepuesto + "_" + edificioAux;
            string contendidoQR = codcotiRepuesto + "|" + numeroCotiAux + "|" + edificioAux + "|" + montoTotalRepuestos + "|" + DateTime.Now.ToString("dd/MM/yyyy");
            string DirArchivo = ruta + "/" + nombreArchivo;

            NA_QRCodeNet qr = new NA_QRCodeNet();
            qr.CrearImagenQR(DirArchivo, contendidoQR, 5);

            ReportParameter imagen = new ReportParameter("p_pathimagen", @"file:\" + DirArchivo + ".jpg");
            ReportParameter fechacoti = new ReportParameter("p_fechaCoti", fechaAux);
            ReportParameter numerocarta = new ReportParameter("numerocarta", numeroCotiAux);
            ReportParameter edificio = new ReportParameter("p_edificio", edificioAux);
            ReportParameter precioTotal = new ReportParameter("p_precioTotal", precioTotalAux);
            ReportParameter ellos = new ReportParameter("p_ellos", ellosAux);
            ReportDataSource DSRepuesto = new ReportDataSource("DSRepuesto", datoRepuesto);
            ReportParameter instaladosR = new ReportParameter("p_instalados", instaladosAux);
            ReportParameter cotiRepuestoR = new ReportParameter("p_codcotizacion", codcotiRepuesto);
            ReportParameter p_almacen = new ReportParameter("p_Almacen", Une);
            ReportParameter p_CuentaBolivianos = new ReportParameter("p_CuentaBolivianos", CuentaBolivianos);
            ReportParameter p_CuentaDolares = new ReportParameter("p_CuentaDolares", CuentaDorales);
            

            ReportViewer1.LocalReport.SetParameters(fechacoti);
            ReportViewer1.LocalReport.SetParameters(numerocarta);
            ReportViewer1.LocalReport.SetParameters(edificio);
            ReportViewer1.LocalReport.SetParameters(precioTotal);
            ReportViewer1.LocalReport.SetParameters(ellos);
            ReportViewer1.LocalReport.SetParameters(instaladosR);
            ReportViewer1.LocalReport.SetParameters(cotiRepuestoR);
            ReportViewer1.LocalReport.SetParameters(p_almacen);
            ReportViewer1.LocalReport.SetParameters(p_CuentaBolivianos);
            ReportViewer1.LocalReport.SetParameters(p_CuentaDolares);


            ReportViewer1.LocalReport.SetParameters(imagen);

            ReportViewer1.LocalReport.DataSources.Add(DSRepuesto);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

    }
}