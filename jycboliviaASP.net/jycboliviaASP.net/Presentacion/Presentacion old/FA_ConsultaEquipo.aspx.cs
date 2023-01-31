using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using jycboliviaASP.net.Negocio;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.IO;
using jycboliviaASP.net.Reportes;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ConsultaEquipo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(70) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {
               // cargarCobrador();
            } 
        }


        private bool tienePermisoDeIngreso(int permiso)
        {
            NA_Responsables Nresp = new NA_Responsables();
            string usuarioAux = Session["NameUser"].ToString();
            string passwordAux = Session["passworuser"].ToString();
            int codUser = Nresp.getCodUsuario(usuarioAux, passwordAux);

            NA_DetallePermiso npermiso = new NA_DetallePermiso();
            return npermiso.tienePermisoResponsable(permiso, codUser);
        }

        protected void bt_buscar_Click(object sender, EventArgs e)
        {
            consultadedatos();
        }

        public string convertidorFecha(string fecha)
        {
            if (fecha != "")
            {
                DateTime fecha_ = Convert.ToDateTime(fecha);
                int dia = fecha_.Day;
                int mes = fecha_.Month;
                int anio = fecha_.Year;
                string _fecha = anio + "/" + mes + "/" + dia;
                return "'" + _fecha + "'";
            }
            else
                return "null";
        }

        private void consultadedatos()
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            string exbo = tx_exbo.Text;
            string edificio = tx_edificio.Text;
            
            int caseSwitch = dd_consulta.SelectedIndex;
            switch(caseSwitch){
                case 1:
                    get_CodigoEquipoEdificio(exbo, edificio);
                    break;
                case 2:
                    get_CodigoEquipoEdificioParaPegarAlAscensor(exbo, edificio);
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            
            }


            if ( caseSwitch > -1 )
            {
                if (dd_consulta.SelectedIndex == 1)
                {
                    
                }

            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: Datos incorrectos') </script>");
        }


        private void get_CodigoEquipoEdificio(string exbo, string edificio)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_CodigosEquiposEdificio1.rdlc";

            string ruta = ConfigurationManager.AppSettings["qr_codeEquipo"] + Session["BaseDatos"].ToString()+"/";
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            string baseDatos = Session["BaseDatos"].ToString();
            NEquipo eq = new NEquipo();
            eq.actulizarCodigosQREquipos(baseDatos);
            DataSet tuplas = eq.getConsultaCodigoDeAutenticacion_QR(exbo, edificio, ruta);
            GenerarCodigosEquiposEdificio(tuplas);
            DataTable DSconsulta = tuplas.Tables[0];

            string rutaLogo = ConfigurationManager.AppSettings["image_logo"];
            string nombreImagen = "jyc";
           

            if (baseDatos.Equals("La Paz"))
            {
                nombreImagen = "elevamerica";
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    nombreImagen = "melevar";
                }
                else
                    if (baseDatos.Equals("Santa Cruz"))
                    {
                        nombreImagen = "interlogy";
                    }

            string direccionImagen = rutaLogo + nombreImagen;

            ReportParameter imagen = new ReportParameter("p_logo", @"file:\" + direccionImagen + ".jpg");

            ReportDataSource DSCodigosEquiposEdificios_QR = new ReportDataSource("DS_CodigoEquipoEdificio", DSconsulta);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.LocalReport.SetParameters(imagen);
            ReportViewer1.LocalReport.DataSources.Add(DSCodigosEquiposEdificios_QR);            
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }


        public void GenerarCodigosEquiposEdificio(DataSet Tuplas) {
            int cant = Tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < cant; i++)
            { 
                string contendidoQR = Tuplas.Tables[0].Rows[i][11].ToString();               
                string nombreArchivo = Tuplas.Tables[0].Rows[i][12].ToString();                
                string DirArchivo = Tuplas.Tables[0].Rows[i][13].ToString();
                DirArchivo = DirArchivo.Remove(DirArchivo.Length - 4);

                if (!System.IO.File.Exists(DirArchivo+".jpg"))
                {
                    NA_QRCodeNet qr = new NA_QRCodeNet();
                    qr.CrearImagenQR(DirArchivo, contendidoQR, 5);
                }

            }           
        
        }

        public void GenerarCodigosEquiposEdificio2(DataSet Tuplas)
        {
            int cant = Tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < cant; i++)
            {
                string contendidoQR = Tuplas.Tables[0].Rows[i][7].ToString();
                string nombreArchivo = Tuplas.Tables[0].Rows[i][8].ToString();
                string DirArchivo = Tuplas.Tables[0].Rows[i][9].ToString();
                DirArchivo = DirArchivo.Remove(DirArchivo.Length - 4);

                if (!System.IO.File.Exists(DirArchivo + ".jpg"))
                {
                    NA_QRCodeNet qr = new NA_QRCodeNet();
                    qr.CrearImagenQR(DirArchivo, contendidoQR, 5);
                }

            }

        }



        private void get_CodigoEquipoEdificioParaPegarAlAscensor(string exbo, string edificio)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_CodigosEquiposEdificio2.rdlc";

            string ruta = ConfigurationManager.AppSettings["qr_codeEquipo"] + Session["BaseDatos"].ToString() + "/";
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            string baseDatos = Session["BaseDatos"].ToString();
            NEquipo eq = new NEquipo();
            eq.actulizarCodigosQREquipos(baseDatos);
            DataSet tuplas = eq.getConsultaCodigoDeAutenticacion_QR_ParaPegarEnAscensor(exbo, edificio,ruta);      
            GenerarCodigosEquiposEdificio2(tuplas);
            DataTable DSconsulta = tuplas.Tables[0];

            string rutaLogo = ConfigurationManager.AppSettings["image_logo"];
            string nombreImagen = "jyc";


            if (baseDatos.Equals("La Paz"))
            {
                nombreImagen = "elevamerica";
            }
            else
                if (baseDatos.Equals("Cochabamba"))
                {
                    nombreImagen = "melevar";
                }
                else
                    if (baseDatos.Equals("Santa Cruz"))
                    {
                        nombreImagen = "interlogy";
                    }

            string direccionImagen = rutaLogo + nombreImagen;

            ReportParameter imagen = new ReportParameter("p_logo", @"file:\" + direccionImagen + ".jpg");

            ReportDataSource DSCodigosEquiposEdificios_QR = new ReportDataSource("DS_QR_Equipos", DSconsulta);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.LocalReport.SetParameters(imagen);
            ReportViewer1.LocalReport.DataSources.Add(DSCodigosEquiposEdificios_QR);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }


    }
}