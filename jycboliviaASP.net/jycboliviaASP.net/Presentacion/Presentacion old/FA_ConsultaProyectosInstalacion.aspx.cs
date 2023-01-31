using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using JyC_Exterior.Negocio;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ConsultaProyectosInstalacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(70) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
           /* if (!IsPostBack)
            {
                cargarCobrador();
            }*/ 
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
            ReportViewer1.LocalReport.EnableExternalImages = true;

            string fechadesde = convertidorFecha(tx_desdeFecha.Text);
            string fechahasta = convertidorFecha(tx_hastaFecha.Text);            
            string BaseDatosS= Session["BaseDatos"].ToString();

            if (dd_consulta.SelectedIndex == 1)
            {
                consultaGeneralR155(tx_edificio.Text,fechadesde,fechahasta, BaseDatosS);                
            }
            if (dd_consulta.SelectedIndex == 2)
            {
                string codigo = tx_codigo.Text;
                consultaR155porCodigo(codigo,BaseDatosS);
            }
            if (dd_consulta.SelectedIndex == 3)
            {
               // get_BoletaSola();
            } 
            
        }


        public string devolverMarcacion(string marcacion) {
            if (marcacion.Equals("1"))
            {
                return "X";
            }
            else
                return " ";
        }
    

        private void consultaR155porCodigo(string codigo, string baseDatos)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_R-155porCodigo.rdlc";

            NA_R155_InspeccionySeguimientoObra nr155 = new NA_R155_InspeccionySeguimientoObra();
            DataSet tuplas = nr155.get_R155porCodigo(codigo);

            if(tuplas.Tables[0].Rows.Count > 0){

            string ruta = ConfigurationManager.AppSettings["image_logo"];
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

            string direccionImagen = ruta + nombreImagen;

            ReportParameter p_logo = new ReportParameter("p_logo", @"file:\" + direccionImagen + ".jpg");            
            ReportParameter p_fechainspeccion = new ReportParameter("p_fechainspeccion", tuplas.Tables[0].Rows[0][1].ToString());
            ReportParameter p_edificio = new ReportParameter("p_edificio", tuplas.Tables[0].Rows[0][3].ToString());
            ReportParameter p_direccion = new ReportParameter("p_direccion", tuplas.Tables[0].Rows[0][4].ToString());
            ReportParameter p_ciudad = new ReportParameter("p_ciudad", baseDatos);
            ReportParameter p_fasedeinstalacion = new ReportParameter("p_fasedeinstalacion", tuplas.Tables[0].Rows[0][5].ToString());

            ReportParameter fosa_11 = new ReportParameter("fosa_11", devolverMarcacion(tuplas.Tables[0].Rows[0][6].ToString().Substring(0, 1)));
            ReportParameter fosa_12 = new ReportParameter("fosa_12", devolverMarcacion(tuplas.Tables[0].Rows[0][6].ToString().Substring(1, 1)));
            ReportParameter fosa_13 = new ReportParameter("fosa_13", devolverMarcacion(tuplas.Tables[0].Rows[0][6].ToString().Substring(2, 1)));
            ReportParameter fosa_14 = new ReportParameter("fosa_14", devolverMarcacion(tuplas.Tables[0].Rows[0][6].ToString().Substring(3, 1)));
            ReportParameter fosa_15 = new ReportParameter("fosa_15", devolverMarcacion(tuplas.Tables[0].Rows[0][6].ToString().Substring(4, 1)));
            ReportParameter fosa_21 = new ReportParameter("fosa_21", devolverMarcacion(tuplas.Tables[0].Rows[0][7].ToString().Substring(0, 1)));
            ReportParameter fosa_22 = new ReportParameter("fosa_22", devolverMarcacion(tuplas.Tables[0].Rows[0][7].ToString().Substring(1, 1)));
            ReportParameter fosa_23 = new ReportParameter("fosa_23", devolverMarcacion(tuplas.Tables[0].Rows[0][7].ToString().Substring(2, 1)));
            ReportParameter fosa_24 = new ReportParameter("fosa_24", devolverMarcacion(tuplas.Tables[0].Rows[0][7].ToString().Substring(3, 1)));
            ReportParameter fosa_25 = new ReportParameter("fosa_25", devolverMarcacion(tuplas.Tables[0].Rows[0][7].ToString().Substring(4, 1)));
            ReportParameter fosa_31 = new ReportParameter("fosa_31", devolverMarcacion(tuplas.Tables[0].Rows[0][8].ToString().Substring(0, 1)));
            ReportParameter fosa_32 = new ReportParameter("fosa_32", devolverMarcacion(tuplas.Tables[0].Rows[0][8].ToString().Substring(1, 1)));
            ReportParameter fosa_33 = new ReportParameter("fosa_33", devolverMarcacion(tuplas.Tables[0].Rows[0][8].ToString().Substring(2, 1)));
            ReportParameter fosa_34 = new ReportParameter("fosa_34", devolverMarcacion(tuplas.Tables[0].Rows[0][8].ToString().Substring(3, 1)));
            ReportParameter fosa_35 = new ReportParameter("fosa_35", devolverMarcacion(tuplas.Tables[0].Rows[0][8].ToString().Substring(4, 1)));

            ReportParameter accesos_11 = new ReportParameter("accesos_11", devolverMarcacion(tuplas.Tables[0].Rows[0][9].ToString().Substring(0, 1)));
            ReportParameter accesos_12 = new ReportParameter("accesos_12", devolverMarcacion(tuplas.Tables[0].Rows[0][9].ToString().Substring(1, 1)));
            ReportParameter accesos_13 = new ReportParameter("accesos_13", devolverMarcacion(tuplas.Tables[0].Rows[0][9].ToString().Substring(2, 1)));
            ReportParameter accesos_14 = new ReportParameter("accesos_14", devolverMarcacion(tuplas.Tables[0].Rows[0][9].ToString().Substring(3, 1)));
            ReportParameter accesos_15 = new ReportParameter("accesos_15", devolverMarcacion(tuplas.Tables[0].Rows[0][9].ToString().Substring(4, 1)));
            ReportParameter accesos_21 = new ReportParameter("accesos_21", devolverMarcacion(tuplas.Tables[0].Rows[0][10].ToString().Substring(0, 1)));
            ReportParameter accesos_22 = new ReportParameter("accesos_22", devolverMarcacion(tuplas.Tables[0].Rows[0][10].ToString().Substring(1, 1)));
            ReportParameter accesos_23 = new ReportParameter("accesos_23", devolverMarcacion(tuplas.Tables[0].Rows[0][10].ToString().Substring(2, 1)));
            ReportParameter accesos_24 = new ReportParameter("accesos_24", devolverMarcacion(tuplas.Tables[0].Rows[0][10].ToString().Substring(3, 1)));
            ReportParameter accesos_25 = new ReportParameter("accesos_25", devolverMarcacion(tuplas.Tables[0].Rows[0][10].ToString().Substring(4, 1)));
            ReportParameter accesos_31 = new ReportParameter("accesos_31", devolverMarcacion(tuplas.Tables[0].Rows[0][11].ToString().Substring(0, 1)));
            ReportParameter accesos_32 = new ReportParameter("accesos_32", devolverMarcacion(tuplas.Tables[0].Rows[0][11].ToString().Substring(1, 1)));
            ReportParameter accesos_33 = new ReportParameter("accesos_33", devolverMarcacion(tuplas.Tables[0].Rows[0][11].ToString().Substring(2, 1)));
            ReportParameter accesos_34 = new ReportParameter("accesos_34", devolverMarcacion(tuplas.Tables[0].Rows[0][11].ToString().Substring(3, 1)));
            ReportParameter accesos_35 = new ReportParameter("accesos_35", devolverMarcacion(tuplas.Tables[0].Rows[0][11].ToString().Substring(4, 1)));
            ReportParameter accesos_41 = new ReportParameter("accesos_41", devolverMarcacion(tuplas.Tables[0].Rows[0][12].ToString().Substring(0, 1)));
            ReportParameter accesos_42 = new ReportParameter("accesos_42", devolverMarcacion(tuplas.Tables[0].Rows[0][12].ToString().Substring(1, 1)));
            ReportParameter accesos_43 = new ReportParameter("accesos_43", devolverMarcacion(tuplas.Tables[0].Rows[0][12].ToString().Substring(2, 1)));
            ReportParameter accesos_44 = new ReportParameter("accesos_44", devolverMarcacion(tuplas.Tables[0].Rows[0][12].ToString().Substring(3, 1)));
            ReportParameter accesos_45 = new ReportParameter("accesos_45", devolverMarcacion(tuplas.Tables[0].Rows[0][12].ToString().Substring(4, 1)));
            ReportParameter accesos_51 = new ReportParameter("accesos_51", devolverMarcacion(tuplas.Tables[0].Rows[0][13].ToString().Substring(0, 1)));
            ReportParameter accesos_52 = new ReportParameter("accesos_52", devolverMarcacion(tuplas.Tables[0].Rows[0][13].ToString().Substring(1, 1)));
            ReportParameter accesos_53 = new ReportParameter("accesos_53", devolverMarcacion(tuplas.Tables[0].Rows[0][13].ToString().Substring(2, 1)));
            ReportParameter accesos_54 = new ReportParameter("accesos_54", devolverMarcacion(tuplas.Tables[0].Rows[0][13].ToString().Substring(3, 1)));
            ReportParameter accesos_55 = new ReportParameter("accesos_55", devolverMarcacion(tuplas.Tables[0].Rows[0][13].ToString().Substring(4, 1)));
            ReportParameter accesos_61 = new ReportParameter("accesos_61", devolverMarcacion(tuplas.Tables[0].Rows[0][14].ToString().Substring(0, 1)));
            ReportParameter accesos_62 = new ReportParameter("accesos_62", devolverMarcacion(tuplas.Tables[0].Rows[0][14].ToString().Substring(1, 1)));
            ReportParameter accesos_63 = new ReportParameter("accesos_63", devolverMarcacion(tuplas.Tables[0].Rows[0][14].ToString().Substring(2, 1)));
            ReportParameter accesos_64 = new ReportParameter("accesos_64", devolverMarcacion(tuplas.Tables[0].Rows[0][14].ToString().Substring(3, 1)));
            ReportParameter accesos_65 = new ReportParameter("accesos_65", devolverMarcacion(tuplas.Tables[0].Rows[0][14].ToString().Substring(4, 1)));
            ReportParameter accesos_71 = new ReportParameter("accesos_71", devolverMarcacion(tuplas.Tables[0].Rows[0][15].ToString().Substring(0, 1)));
            ReportParameter accesos_72 = new ReportParameter("accesos_72", devolverMarcacion(tuplas.Tables[0].Rows[0][15].ToString().Substring(1, 1)));
            ReportParameter accesos_73 = new ReportParameter("accesos_73", devolverMarcacion(tuplas.Tables[0].Rows[0][15].ToString().Substring(2, 1)));
            ReportParameter accesos_74 = new ReportParameter("accesos_74", devolverMarcacion(tuplas.Tables[0].Rows[0][15].ToString().Substring(3, 1)));
            ReportParameter accesos_75 = new ReportParameter("accesos_75", devolverMarcacion(tuplas.Tables[0].Rows[0][15].ToString().Substring(4, 1)));
            ReportParameter accesos_81 = new ReportParameter("accesos_81", devolverMarcacion(tuplas.Tables[0].Rows[0][16].ToString().Substring(0, 1)));
            ReportParameter accesos_82 = new ReportParameter("accesos_82", devolverMarcacion(tuplas.Tables[0].Rows[0][16].ToString().Substring(1, 1)));
            ReportParameter accesos_83 = new ReportParameter("accesos_83", devolverMarcacion(tuplas.Tables[0].Rows[0][16].ToString().Substring(2, 1)));
            ReportParameter accesos_84 = new ReportParameter("accesos_84", devolverMarcacion(tuplas.Tables[0].Rows[0][16].ToString().Substring(3, 1)));
            ReportParameter accesos_85 = new ReportParameter("accesos_85", devolverMarcacion(tuplas.Tables[0].Rows[0][16].ToString().Substring(4, 1)));
            ReportParameter accesos_91 = new ReportParameter("accesos_91", devolverMarcacion(tuplas.Tables[0].Rows[0][17].ToString().Substring(0, 1)));
            ReportParameter accesos_92 = new ReportParameter("accesos_92", devolverMarcacion(tuplas.Tables[0].Rows[0][17].ToString().Substring(1, 1)));
            ReportParameter accesos_93 = new ReportParameter("accesos_93", devolverMarcacion(tuplas.Tables[0].Rows[0][17].ToString().Substring(2, 1)));
            ReportParameter accesos_94 = new ReportParameter("accesos_94", devolverMarcacion(tuplas.Tables[0].Rows[0][17].ToString().Substring(3, 1)));
            ReportParameter accesos_95 = new ReportParameter("accesos_95", devolverMarcacion(tuplas.Tables[0].Rows[0][17].ToString().Substring(4, 1)));

            ReportParameter otros_11 = new ReportParameter("otros_11", devolverMarcacion(tuplas.Tables[0].Rows[0][18].ToString().Substring(0, 1)));
            ReportParameter otros_12 = new ReportParameter("otros_12", devolverMarcacion(tuplas.Tables[0].Rows[0][18].ToString().Substring(1, 1)));
            ReportParameter otros_13 = new ReportParameter("otros_13", devolverMarcacion(tuplas.Tables[0].Rows[0][18].ToString().Substring(2, 1)));
            ReportParameter otros_14 = new ReportParameter("otros_14", devolverMarcacion(tuplas.Tables[0].Rows[0][18].ToString().Substring(3, 1)));
            ReportParameter otros_15 = new ReportParameter("otros_15", devolverMarcacion(tuplas.Tables[0].Rows[0][18].ToString().Substring(4, 1)));
            ReportParameter otros_21 = new ReportParameter("otros_21", devolverMarcacion(tuplas.Tables[0].Rows[0][19].ToString().Substring(0, 1)));
            ReportParameter otros_22 = new ReportParameter("otros_22", devolverMarcacion(tuplas.Tables[0].Rows[0][19].ToString().Substring(0, 1)));
            ReportParameter otros_23 = new ReportParameter("otros_23", devolverMarcacion(tuplas.Tables[0].Rows[0][19].ToString().Substring(1, 1)));
            ReportParameter otros_24 = new ReportParameter("otros_24", devolverMarcacion(tuplas.Tables[0].Rows[0][19].ToString().Substring(2, 1)));
            ReportParameter otros_25 = new ReportParameter("otros_25", devolverMarcacion(tuplas.Tables[0].Rows[0][19].ToString().Substring(3, 1)));
            ReportParameter otros_31 = new ReportParameter("otros_31", devolverMarcacion(tuplas.Tables[0].Rows[0][20].ToString().Substring(0, 1)));
            ReportParameter otros_32 = new ReportParameter("otros_32", devolverMarcacion(tuplas.Tables[0].Rows[0][20].ToString().Substring(1, 1)));
            ReportParameter otros_33 = new ReportParameter("otros_33", devolverMarcacion(tuplas.Tables[0].Rows[0][20].ToString().Substring(2, 1)));
            ReportParameter otros_34 = new ReportParameter("otros_34", devolverMarcacion(tuplas.Tables[0].Rows[0][20].ToString().Substring(3, 1)));
            ReportParameter otros_35 = new ReportParameter("otros_35", devolverMarcacion(tuplas.Tables[0].Rows[0][20].ToString().Substring(4, 1)));
            ReportParameter otros_41 = new ReportParameter("otros_41", devolverMarcacion(tuplas.Tables[0].Rows[0][21].ToString().Substring(0, 1)));
            ReportParameter otros_42 = new ReportParameter("otros_42", devolverMarcacion(tuplas.Tables[0].Rows[0][21].ToString().Substring(1, 1)));
            ReportParameter otros_43 = new ReportParameter("otros_43", devolverMarcacion(tuplas.Tables[0].Rows[0][21].ToString().Substring(2, 1)));
            ReportParameter otros_44 = new ReportParameter("otros_44", devolverMarcacion(tuplas.Tables[0].Rows[0][21].ToString().Substring(3, 1)));
            ReportParameter otros_45 = new ReportParameter("otros_45", devolverMarcacion(tuplas.Tables[0].Rows[0][21].ToString().Substring(4, 1)));
            ReportParameter otros_51 = new ReportParameter("otros_51", devolverMarcacion(tuplas.Tables[0].Rows[0][22].ToString().Substring(0, 1)));
            ReportParameter otros_52 = new ReportParameter("otros_52", devolverMarcacion(tuplas.Tables[0].Rows[0][22].ToString().Substring(1, 1)));
            ReportParameter otros_53 = new ReportParameter("otros_53", devolverMarcacion(tuplas.Tables[0].Rows[0][22].ToString().Substring(2, 1)));
            ReportParameter otros_54 = new ReportParameter("otros_54", devolverMarcacion(tuplas.Tables[0].Rows[0][22].ToString().Substring(3, 1)));
            ReportParameter otros_55 = new ReportParameter("otros_55", devolverMarcacion(tuplas.Tables[0].Rows[0][22].ToString().Substring(4, 1)));
            ReportParameter otros_61 = new ReportParameter("otros_61", devolverMarcacion(tuplas.Tables[0].Rows[0][23].ToString().Substring(0, 1)));
            ReportParameter otros_62 = new ReportParameter("otros_62", devolverMarcacion(tuplas.Tables[0].Rows[0][23].ToString().Substring(1, 1)));
            ReportParameter otros_63 = new ReportParameter("otros_63", devolverMarcacion(tuplas.Tables[0].Rows[0][23].ToString().Substring(2, 1)));
            ReportParameter otros_64 = new ReportParameter("otros_64", devolverMarcacion(tuplas.Tables[0].Rows[0][23].ToString().Substring(3, 1)));
            ReportParameter otros_65 = new ReportParameter("otros_65", devolverMarcacion(tuplas.Tables[0].Rows[0][23].ToString().Substring(4, 1)));
            ReportParameter otros_71 = new ReportParameter("otros_71", devolverMarcacion(tuplas.Tables[0].Rows[0][24].ToString().Substring(0, 1)));
            ReportParameter otros_72 = new ReportParameter("otros_72", devolverMarcacion(tuplas.Tables[0].Rows[0][24].ToString().Substring(1, 1)));
            ReportParameter otros_73 = new ReportParameter("otros_73", devolverMarcacion(tuplas.Tables[0].Rows[0][24].ToString().Substring(2, 1)));
            ReportParameter otros_74 = new ReportParameter("otros_74", devolverMarcacion(tuplas.Tables[0].Rows[0][24].ToString().Substring(3, 1)));
            ReportParameter otros_75 = new ReportParameter("otros_75", devolverMarcacion(tuplas.Tables[0].Rows[0][24].ToString().Substring(4, 1)));
            ReportParameter hueco_11 = new ReportParameter("hueco_11", devolverMarcacion(tuplas.Tables[0].Rows[0][25].ToString().Substring(0, 1)));
            ReportParameter hueco_12 = new ReportParameter("hueco_12", devolverMarcacion(tuplas.Tables[0].Rows[0][25].ToString().Substring(1, 1)));
            ReportParameter hueco_13 = new ReportParameter("hueco_13", devolverMarcacion(tuplas.Tables[0].Rows[0][25].ToString().Substring(2, 1)));
            ReportParameter hueco_14 = new ReportParameter("hueco_14", devolverMarcacion(tuplas.Tables[0].Rows[0][25].ToString().Substring(3, 1)));
            ReportParameter hueco_15 = new ReportParameter("hueco_15", devolverMarcacion(tuplas.Tables[0].Rows[0][25].ToString().Substring(4, 1)));
            ReportParameter hueco_21 = new ReportParameter("hueco_21", devolverMarcacion(tuplas.Tables[0].Rows[0][26].ToString().Substring(0, 1)));
            ReportParameter hueco_22 = new ReportParameter("hueco_22", devolverMarcacion(tuplas.Tables[0].Rows[0][26].ToString().Substring(1, 1)));
            ReportParameter hueco_23 = new ReportParameter("hueco_23", devolverMarcacion(tuplas.Tables[0].Rows[0][26].ToString().Substring(2, 1)));
            ReportParameter hueco_24 = new ReportParameter("hueco_24", devolverMarcacion(tuplas.Tables[0].Rows[0][26].ToString().Substring(3, 1)));
            ReportParameter hueco_25 = new ReportParameter("hueco_25", devolverMarcacion(tuplas.Tables[0].Rows[0][26].ToString().Substring(4, 1)));
            ReportParameter hueco_31 = new ReportParameter("hueco_31", devolverMarcacion(tuplas.Tables[0].Rows[0][27].ToString().Substring(0, 1)));
            ReportParameter hueco_32 = new ReportParameter("hueco_32", devolverMarcacion(tuplas.Tables[0].Rows[0][27].ToString().Substring(1, 1)));
            ReportParameter hueco_33 = new ReportParameter("hueco_33", devolverMarcacion(tuplas.Tables[0].Rows[0][27].ToString().Substring(2, 1)));
            ReportParameter hueco_34 = new ReportParameter("hueco_34", devolverMarcacion(tuplas.Tables[0].Rows[0][27].ToString().Substring(3, 1)));
            ReportParameter hueco_35 = new ReportParameter("hueco_35", devolverMarcacion(tuplas.Tables[0].Rows[0][27].ToString().Substring(4, 1)));

            ReportParameter hueco_41 = new ReportParameter("hueco_41", devolverMarcacion(tuplas.Tables[0].Rows[0][28].ToString().Substring(0, 1)));
            ReportParameter hueco_42 = new ReportParameter("hueco_42", devolverMarcacion(tuplas.Tables[0].Rows[0][28].ToString().Substring(1, 1)));
            ReportParameter hueco_43 = new ReportParameter("hueco_43", devolverMarcacion(tuplas.Tables[0].Rows[0][28].ToString().Substring(2, 1)));
            ReportParameter hueco_44 = new ReportParameter("hueco_44", devolverMarcacion(tuplas.Tables[0].Rows[0][28].ToString().Substring(3, 1)));
            ReportParameter hueco_45 = new ReportParameter("hueco_45", devolverMarcacion(tuplas.Tables[0].Rows[0][28].ToString().Substring(4, 1)));
            ReportParameter hueco_51 = new ReportParameter("hueco_51", devolverMarcacion(tuplas.Tables[0].Rows[0][29].ToString().Substring(0, 1)));
            ReportParameter hueco_52 = new ReportParameter("hueco_52", devolverMarcacion(tuplas.Tables[0].Rows[0][29].ToString().Substring(1, 1)));
            ReportParameter hueco_53 = new ReportParameter("hueco_53", devolverMarcacion(tuplas.Tables[0].Rows[0][29].ToString().Substring(2, 1)));
            ReportParameter hueco_54 = new ReportParameter("hueco_54", devolverMarcacion(tuplas.Tables[0].Rows[0][29].ToString().Substring(3, 1)));
            ReportParameter hueco_55 = new ReportParameter("hueco_55", devolverMarcacion(tuplas.Tables[0].Rows[0][29].ToString().Substring(4, 1)));
            ReportParameter hueco_61 = new ReportParameter("hueco_61", devolverMarcacion(tuplas.Tables[0].Rows[0][30].ToString().Substring(0, 1)));
            ReportParameter hueco_62 = new ReportParameter("hueco_62", devolverMarcacion(tuplas.Tables[0].Rows[0][30].ToString().Substring(1, 1)));
            ReportParameter hueco_63 = new ReportParameter("hueco_63", devolverMarcacion(tuplas.Tables[0].Rows[0][30].ToString().Substring(2, 1)));
            ReportParameter hueco_64 = new ReportParameter("hueco_64", devolverMarcacion(tuplas.Tables[0].Rows[0][30].ToString().Substring(3, 1)));
            ReportParameter hueco_65 = new ReportParameter("hueco_65", devolverMarcacion(tuplas.Tables[0].Rows[0][30].ToString().Substring(4, 1)));
            ReportParameter hueco_71 = new ReportParameter("hueco_71", devolverMarcacion(tuplas.Tables[0].Rows[0][31].ToString().Substring(0, 1)));
            ReportParameter hueco_72 = new ReportParameter("hueco_72", devolverMarcacion(tuplas.Tables[0].Rows[0][31].ToString().Substring(1, 1)));
            ReportParameter hueco_73 = new ReportParameter("hueco_73", devolverMarcacion(tuplas.Tables[0].Rows[0][31].ToString().Substring(2, 1)));
            ReportParameter hueco_74 = new ReportParameter("hueco_74", devolverMarcacion(tuplas.Tables[0].Rows[0][31].ToString().Substring(3, 1)));
            ReportParameter hueco_75 = new ReportParameter("hueco_75", devolverMarcacion(tuplas.Tables[0].Rows[0][31].ToString().Substring(4, 1)));
            ReportParameter hueco_81 = new ReportParameter("hueco_81", devolverMarcacion(tuplas.Tables[0].Rows[0][32].ToString().Substring(0, 1)));
            ReportParameter hueco_82 = new ReportParameter("hueco_82", devolverMarcacion(tuplas.Tables[0].Rows[0][32].ToString().Substring(1, 1)));
            ReportParameter hueco_83 = new ReportParameter("hueco_83", devolverMarcacion(tuplas.Tables[0].Rows[0][32].ToString().Substring(2, 1)));
            ReportParameter hueco_84 = new ReportParameter("hueco_84", devolverMarcacion(tuplas.Tables[0].Rows[0][32].ToString().Substring(3, 1)));
            ReportParameter hueco_85 = new ReportParameter("hueco_85", devolverMarcacion(tuplas.Tables[0].Rows[0][32].ToString().Substring(4, 1)));
            ReportParameter hueco_91 = new ReportParameter("hueco_91", devolverMarcacion(tuplas.Tables[0].Rows[0][33].ToString().Substring(0, 1)));
            ReportParameter hueco_92 = new ReportParameter("hueco_92", devolverMarcacion(tuplas.Tables[0].Rows[0][33].ToString().Substring(1, 1)));
            ReportParameter hueco_93 = new ReportParameter("hueco_93", devolverMarcacion(tuplas.Tables[0].Rows[0][33].ToString().Substring(2, 1)));
            ReportParameter hueco_94 = new ReportParameter("hueco_94", devolverMarcacion(tuplas.Tables[0].Rows[0][33].ToString().Substring(3, 1)));
            ReportParameter hueco_95 = new ReportParameter("hueco_95", devolverMarcacion(tuplas.Tables[0].Rows[0][33].ToString().Substring(4, 1)));
            ReportParameter hueco_101 = new ReportParameter("hueco_101", devolverMarcacion(tuplas.Tables[0].Rows[0][34].ToString().Substring(0, 1)));
            ReportParameter hueco_102 = new ReportParameter("hueco_102", devolverMarcacion(tuplas.Tables[0].Rows[0][34].ToString().Substring(1, 1)));
            ReportParameter hueco_103 = new ReportParameter("hueco_103", devolverMarcacion(tuplas.Tables[0].Rows[0][34].ToString().Substring(2, 1)));
            ReportParameter hueco_104 = new ReportParameter("hueco_104", devolverMarcacion(tuplas.Tables[0].Rows[0][34].ToString().Substring(3, 1)));
            ReportParameter hueco_105 = new ReportParameter("hueco_105", devolverMarcacion(tuplas.Tables[0].Rows[0][34].ToString().Substring(4, 1)));
            ReportParameter hueco_111 = new ReportParameter("hueco_111", devolverMarcacion(tuplas.Tables[0].Rows[0][35].ToString().Substring(0, 1)));
            ReportParameter hueco_112 = new ReportParameter("hueco_112", devolverMarcacion(tuplas.Tables[0].Rows[0][35].ToString().Substring(1, 1)));
            ReportParameter hueco_113 = new ReportParameter("hueco_113", devolverMarcacion(tuplas.Tables[0].Rows[0][35].ToString().Substring(2, 1)));
            ReportParameter hueco_114 = new ReportParameter("hueco_114", devolverMarcacion(tuplas.Tables[0].Rows[0][35].ToString().Substring(3, 1)));
            ReportParameter hueco_115 = new ReportParameter("hueco_115", devolverMarcacion(tuplas.Tables[0].Rows[0][35].ToString().Substring(4, 1)));
            ReportParameter hueco_121 = new ReportParameter("hueco_121", devolverMarcacion(tuplas.Tables[0].Rows[0][36].ToString().Substring(0, 1)));
            ReportParameter hueco_122 = new ReportParameter("hueco_122", devolverMarcacion(tuplas.Tables[0].Rows[0][36].ToString().Substring(1, 1)));
            ReportParameter hueco_123 = new ReportParameter("hueco_123", devolverMarcacion(tuplas.Tables[0].Rows[0][36].ToString().Substring(2, 1)));
            ReportParameter hueco_124 = new ReportParameter("hueco_124", devolverMarcacion(tuplas.Tables[0].Rows[0][36].ToString().Substring(3, 1)));
            ReportParameter hueco_125 = new ReportParameter("hueco_125", devolverMarcacion(tuplas.Tables[0].Rows[0][36].ToString().Substring(4, 1)));
            ReportParameter hueco_131 = new ReportParameter("hueco_131", devolverMarcacion(tuplas.Tables[0].Rows[0][37].ToString().Substring(0, 1)));
            ReportParameter hueco_132 = new ReportParameter("hueco_132", devolverMarcacion(tuplas.Tables[0].Rows[0][37].ToString().Substring(1, 1)));
            ReportParameter hueco_133 = new ReportParameter("hueco_133", devolverMarcacion(tuplas.Tables[0].Rows[0][37].ToString().Substring(2, 1)));
            ReportParameter hueco_134 = new ReportParameter("hueco_134", devolverMarcacion(tuplas.Tables[0].Rows[0][37].ToString().Substring(3, 1)));
            ReportParameter hueco_135 = new ReportParameter("hueco_135", devolverMarcacion(tuplas.Tables[0].Rows[0][37].ToString().Substring(4, 1)));
            ReportParameter hueco_141 = new ReportParameter("hueco_141", devolverMarcacion(tuplas.Tables[0].Rows[0][38].ToString().Substring(0, 1)));
            ReportParameter hueco_142 = new ReportParameter("hueco_142", devolverMarcacion(tuplas.Tables[0].Rows[0][38].ToString().Substring(1, 1)));
            ReportParameter hueco_143 = new ReportParameter("hueco_143", devolverMarcacion(tuplas.Tables[0].Rows[0][38].ToString().Substring(2, 1)));
            ReportParameter hueco_144 = new ReportParameter("hueco_144", devolverMarcacion(tuplas.Tables[0].Rows[0][38].ToString().Substring(3, 1)));
            ReportParameter hueco_145 = new ReportParameter("hueco_145", devolverMarcacion(tuplas.Tables[0].Rows[0][38].ToString().Substring(4, 1)));
            ReportParameter hueco_151 = new ReportParameter("hueco_151", devolverMarcacion(tuplas.Tables[0].Rows[0][39].ToString().Substring(0, 1)));
            ReportParameter hueco_152 = new ReportParameter("hueco_152", devolverMarcacion(tuplas.Tables[0].Rows[0][39].ToString().Substring(1, 1)));
            ReportParameter hueco_153 = new ReportParameter("hueco_153", devolverMarcacion(tuplas.Tables[0].Rows[0][39].ToString().Substring(2, 1)));
            ReportParameter hueco_154 = new ReportParameter("hueco_154", devolverMarcacion(tuplas.Tables[0].Rows[0][39].ToString().Substring(3, 1)));
            ReportParameter hueco_155 = new ReportParameter("hueco_155", devolverMarcacion(tuplas.Tables[0].Rows[0][39].ToString().Substring(4, 1)));
            ReportParameter hueco_161 = new ReportParameter("hueco_161", devolverMarcacion(tuplas.Tables[0].Rows[0][40].ToString().Substring(0, 1)));
            ReportParameter hueco_162 = new ReportParameter("hueco_162", devolverMarcacion(tuplas.Tables[0].Rows[0][40].ToString().Substring(1, 1)));
            ReportParameter hueco_163 = new ReportParameter("hueco_163", devolverMarcacion(tuplas.Tables[0].Rows[0][40].ToString().Substring(2, 1)));
            ReportParameter hueco_164 = new ReportParameter("hueco_164", devolverMarcacion(tuplas.Tables[0].Rows[0][40].ToString().Substring(3, 1)));
            ReportParameter hueco_165 = new ReportParameter("hueco_165", devolverMarcacion(tuplas.Tables[0].Rows[0][40].ToString().Substring(4, 1)));
            ReportParameter hueco_171 = new ReportParameter("hueco_171", devolverMarcacion(tuplas.Tables[0].Rows[0][41].ToString().Substring(0, 1)));
            ReportParameter hueco_172 = new ReportParameter("hueco_172", devolverMarcacion(tuplas.Tables[0].Rows[0][41].ToString().Substring(1, 1)));
            ReportParameter hueco_173 = new ReportParameter("hueco_173", devolverMarcacion(tuplas.Tables[0].Rows[0][41].ToString().Substring(2, 1)));
            ReportParameter hueco_174 = new ReportParameter("hueco_174", devolverMarcacion(tuplas.Tables[0].Rows[0][41].ToString().Substring(3, 1)));
            ReportParameter hueco_175 = new ReportParameter("hueco_175", devolverMarcacion(tuplas.Tables[0].Rows[0][41].ToString().Substring(4, 1)));
            ReportParameter hueco_181 = new ReportParameter("hueco_181", devolverMarcacion(tuplas.Tables[0].Rows[0][42].ToString().Substring(0, 1)));
            ReportParameter hueco_182 = new ReportParameter("hueco_182", devolverMarcacion(tuplas.Tables[0].Rows[0][42].ToString().Substring(1, 1)));
            ReportParameter hueco_183 = new ReportParameter("hueco_183", devolverMarcacion(tuplas.Tables[0].Rows[0][42].ToString().Substring(2, 1)));
            ReportParameter hueco_184 = new ReportParameter("hueco_184", devolverMarcacion(tuplas.Tables[0].Rows[0][42].ToString().Substring(3, 1)));
            ReportParameter hueco_185 = new ReportParameter("hueco_185", devolverMarcacion(tuplas.Tables[0].Rows[0][42].ToString().Substring(4, 1)));
            ReportParameter hueco_191 = new ReportParameter("hueco_191", devolverMarcacion(tuplas.Tables[0].Rows[0][43].ToString().Substring(0, 1)));
            ReportParameter hueco_192 = new ReportParameter("hueco_192", devolverMarcacion(tuplas.Tables[0].Rows[0][43].ToString().Substring(1, 1)));
            ReportParameter hueco_193 = new ReportParameter("hueco_193", devolverMarcacion(tuplas.Tables[0].Rows[0][43].ToString().Substring(2, 1)));
            ReportParameter hueco_194 = new ReportParameter("hueco_194", devolverMarcacion(tuplas.Tables[0].Rows[0][43].ToString().Substring(3, 1)));
            ReportParameter hueco_195 = new ReportParameter("hueco_195", devolverMarcacion(tuplas.Tables[0].Rows[0][43].ToString().Substring(4, 1)));

            ReportParameter p_codigo = new ReportParameter("p_codigo", tuplas.Tables[0].Rows[0][0].ToString());
            ReportParameter p_presenciadeaguaenfosaycuartodemaquinas = new ReportParameter("p_presenciadeaguaenfosaycuartodemaquinas",devolverMarcacion(tuplas.Tables[0].Rows[0][44].ToString()));
            ReportParameter p_presenciadecablesoelementosajenosdentroelhueco = new ReportParameter("p_presenciadecablesoelementosajenosdentroelhueco", devolverMarcacion(tuplas.Tables[0].Rows[0][45].ToString()));
            ReportParameter p_observaciones = new ReportParameter("p_observaciones", tuplas.Tables[0].Rows[0][46].ToString());

            ReportParameter p_nombreEdificio = new ReportParameter("p_nombreEdificio", tuplas.Tables[0].Rows[0][47].ToString());
            ReportParameter p_ciEdificio = new ReportParameter("p_ciEdificio", tuplas.Tables[0].Rows[0][48].ToString());
            ReportParameter p_cargoEdificio = new ReportParameter("p_cargoEdificio", tuplas.Tables[0].Rows[0][49].ToString());
            ReportParameter p_nombreRes = new ReportParameter("p_nombreRes", tuplas.Tables[0].Rows[0][50].ToString());
            ReportParameter p_cargoRes = new ReportParameter("p_cargoRes", tuplas.Tables[0].Rows[0][51].ToString());
            


            ReportViewer1.LocalReport.SetParameters(p_nombreRes);
            ReportViewer1.LocalReport.SetParameters(p_cargoRes);
            ReportViewer1.LocalReport.SetParameters(p_nombreEdificio);
            ReportViewer1.LocalReport.SetParameters(p_ciEdificio);
            ReportViewer1.LocalReport.SetParameters(p_cargoEdificio);


            ReportViewer1.LocalReport.SetParameters(p_codigo);
            ReportViewer1.LocalReport.SetParameters(p_presenciadeaguaenfosaycuartodemaquinas);
            ReportViewer1.LocalReport.SetParameters(p_presenciadecablesoelementosajenosdentroelhueco);
            ReportViewer1.LocalReport.SetParameters(p_observaciones);


            ReportViewer1.LocalReport.SetParameters(p_logo);
            ReportViewer1.LocalReport.SetParameters(p_fechainspeccion);
            ReportViewer1.LocalReport.SetParameters(p_edificio);
            ReportViewer1.LocalReport.SetParameters(p_direccion);
            ReportViewer1.LocalReport.SetParameters(p_ciudad);
            ReportViewer1.LocalReport.SetParameters(p_fasedeinstalacion);

            ReportViewer1.LocalReport.SetParameters(fosa_11);
            ReportViewer1.LocalReport.SetParameters(fosa_12);
            ReportViewer1.LocalReport.SetParameters(fosa_13);
            ReportViewer1.LocalReport.SetParameters(fosa_14);
            ReportViewer1.LocalReport.SetParameters(fosa_15);
            ReportViewer1.LocalReport.SetParameters(fosa_21);
            ReportViewer1.LocalReport.SetParameters(fosa_22);
            ReportViewer1.LocalReport.SetParameters(fosa_23);
            ReportViewer1.LocalReport.SetParameters(fosa_24);
            ReportViewer1.LocalReport.SetParameters(fosa_25);
            ReportViewer1.LocalReport.SetParameters(fosa_31);
            ReportViewer1.LocalReport.SetParameters(fosa_32);
            ReportViewer1.LocalReport.SetParameters(fosa_33);
            ReportViewer1.LocalReport.SetParameters(fosa_34);
            ReportViewer1.LocalReport.SetParameters(fosa_35);

            ReportViewer1.LocalReport.SetParameters(accesos_11);
            ReportViewer1.LocalReport.SetParameters(accesos_12);
            ReportViewer1.LocalReport.SetParameters(accesos_13);
            ReportViewer1.LocalReport.SetParameters(accesos_14);
            ReportViewer1.LocalReport.SetParameters(accesos_15);
            ReportViewer1.LocalReport.SetParameters(accesos_21);
            ReportViewer1.LocalReport.SetParameters(accesos_22);
            ReportViewer1.LocalReport.SetParameters(accesos_23);
            ReportViewer1.LocalReport.SetParameters(accesos_24);
            ReportViewer1.LocalReport.SetParameters(accesos_25);
            ReportViewer1.LocalReport.SetParameters(accesos_31);
            ReportViewer1.LocalReport.SetParameters(accesos_32);
            ReportViewer1.LocalReport.SetParameters(accesos_33);
            ReportViewer1.LocalReport.SetParameters(accesos_34);
            ReportViewer1.LocalReport.SetParameters(accesos_35);
            ReportViewer1.LocalReport.SetParameters(accesos_41);
            ReportViewer1.LocalReport.SetParameters(accesos_42);
            ReportViewer1.LocalReport.SetParameters(accesos_43);
            ReportViewer1.LocalReport.SetParameters(accesos_44);
            ReportViewer1.LocalReport.SetParameters(accesos_45);
            ReportViewer1.LocalReport.SetParameters(accesos_51);
            ReportViewer1.LocalReport.SetParameters(accesos_52);
            ReportViewer1.LocalReport.SetParameters(accesos_53);
            ReportViewer1.LocalReport.SetParameters(accesos_54);
            ReportViewer1.LocalReport.SetParameters(accesos_55);
            ReportViewer1.LocalReport.SetParameters(accesos_61);
            ReportViewer1.LocalReport.SetParameters(accesos_62);
            ReportViewer1.LocalReport.SetParameters(accesos_63);
            ReportViewer1.LocalReport.SetParameters(accesos_64);
            ReportViewer1.LocalReport.SetParameters(accesos_65 );
            ReportViewer1.LocalReport.SetParameters(accesos_71 );
            ReportViewer1.LocalReport.SetParameters(accesos_72 );
            ReportViewer1.LocalReport.SetParameters(accesos_73 );
            ReportViewer1.LocalReport.SetParameters(accesos_74 );
            ReportViewer1.LocalReport.SetParameters(accesos_75 );
            ReportViewer1.LocalReport.SetParameters(accesos_81 );
            ReportViewer1.LocalReport.SetParameters(accesos_82 );
            ReportViewer1.LocalReport.SetParameters( accesos_83 );
            ReportViewer1.LocalReport.SetParameters( accesos_84 );
            ReportViewer1.LocalReport.SetParameters( accesos_85 );
            ReportViewer1.LocalReport.SetParameters( accesos_91 );
            ReportViewer1.LocalReport.SetParameters( accesos_92 );
            ReportViewer1.LocalReport.SetParameters( accesos_93 );
            ReportViewer1.LocalReport.SetParameters( accesos_94 );
            ReportViewer1.LocalReport.SetParameters( accesos_95 );

            ReportViewer1.LocalReport.SetParameters( otros_11 );
            ReportViewer1.LocalReport.SetParameters( otros_12 );
            ReportViewer1.LocalReport.SetParameters( otros_13 );
            ReportViewer1.LocalReport.SetParameters( otros_14 );
            ReportViewer1.LocalReport.SetParameters( otros_15 );
            ReportViewer1.LocalReport.SetParameters( otros_21 );
            ReportViewer1.LocalReport.SetParameters( otros_22 );
            ReportViewer1.LocalReport.SetParameters( otros_23 );
            ReportViewer1.LocalReport.SetParameters( otros_24 );
            ReportViewer1.LocalReport.SetParameters( otros_25 );
            ReportViewer1.LocalReport.SetParameters( otros_31 );
            ReportViewer1.LocalReport.SetParameters( otros_32 );
            ReportViewer1.LocalReport.SetParameters( otros_33 );
            ReportViewer1.LocalReport.SetParameters( otros_34 );
            ReportViewer1.LocalReport.SetParameters( otros_35 );
            ReportViewer1.LocalReport.SetParameters( otros_41 );
            ReportViewer1.LocalReport.SetParameters( otros_42 );
            ReportViewer1.LocalReport.SetParameters( otros_43 );
            ReportViewer1.LocalReport.SetParameters( otros_44 );
            ReportViewer1.LocalReport.SetParameters( otros_45 );
            ReportViewer1.LocalReport.SetParameters( otros_51 );
            ReportViewer1.LocalReport.SetParameters( otros_52 );
            ReportViewer1.LocalReport.SetParameters( otros_53 );
            ReportViewer1.LocalReport.SetParameters( otros_54 );
            ReportViewer1.LocalReport.SetParameters( otros_55 );
            ReportViewer1.LocalReport.SetParameters( otros_61 );
            ReportViewer1.LocalReport.SetParameters( otros_62 );
            ReportViewer1.LocalReport.SetParameters( otros_63 );
            ReportViewer1.LocalReport.SetParameters( otros_64 );
            ReportViewer1.LocalReport.SetParameters( otros_65 );
            ReportViewer1.LocalReport.SetParameters( otros_71 );
            ReportViewer1.LocalReport.SetParameters( otros_72 );
            ReportViewer1.LocalReport.SetParameters( otros_73 );
            ReportViewer1.LocalReport.SetParameters( otros_74 );
            ReportViewer1.LocalReport.SetParameters( otros_75 );
            ReportViewer1.LocalReport.SetParameters( hueco_11 );
            ReportViewer1.LocalReport.SetParameters( hueco_12 );
            ReportViewer1.LocalReport.SetParameters( hueco_13 );
            ReportViewer1.LocalReport.SetParameters( hueco_14 );
            ReportViewer1.LocalReport.SetParameters( hueco_15 );
            ReportViewer1.LocalReport.SetParameters( hueco_21 );
            ReportViewer1.LocalReport.SetParameters( hueco_22 );
            ReportViewer1.LocalReport.SetParameters( hueco_23 );
            ReportViewer1.LocalReport.SetParameters( hueco_24 );
            ReportViewer1.LocalReport.SetParameters( hueco_25 );
            ReportViewer1.LocalReport.SetParameters( hueco_31 );
            ReportViewer1.LocalReport.SetParameters( hueco_32 );
            ReportViewer1.LocalReport.SetParameters( hueco_33 );
            ReportViewer1.LocalReport.SetParameters( hueco_34 );
            ReportViewer1.LocalReport.SetParameters( hueco_35 );

            ReportViewer1.LocalReport.SetParameters( hueco_41);
            ReportViewer1.LocalReport.SetParameters( hueco_42 );
            ReportViewer1.LocalReport.SetParameters( hueco_43 );
            ReportViewer1.LocalReport.SetParameters( hueco_44 );
            ReportViewer1.LocalReport.SetParameters( hueco_45 );
            ReportViewer1.LocalReport.SetParameters( hueco_51 );
            ReportViewer1.LocalReport.SetParameters( hueco_52 );
            ReportViewer1.LocalReport.SetParameters( hueco_53 );
            ReportViewer1.LocalReport.SetParameters( hueco_54 );
            ReportViewer1.LocalReport.SetParameters( hueco_55 );
            ReportViewer1.LocalReport.SetParameters( hueco_61 );
            ReportViewer1.LocalReport.SetParameters( hueco_62 );
            ReportViewer1.LocalReport.SetParameters( hueco_63 );
            ReportViewer1.LocalReport.SetParameters( hueco_64 );
            ReportViewer1.LocalReport.SetParameters( hueco_65 );
            ReportViewer1.LocalReport.SetParameters( hueco_71 );
            ReportViewer1.LocalReport.SetParameters( hueco_72 );
            ReportViewer1.LocalReport.SetParameters( hueco_73 );
            ReportViewer1.LocalReport.SetParameters( hueco_74 );
            ReportViewer1.LocalReport.SetParameters( hueco_75 );
            ReportViewer1.LocalReport.SetParameters( hueco_81 );
            ReportViewer1.LocalReport.SetParameters( hueco_82 );
            ReportViewer1.LocalReport.SetParameters( hueco_83 );
            ReportViewer1.LocalReport.SetParameters( hueco_84 );
            ReportViewer1.LocalReport.SetParameters( hueco_85 );
            ReportViewer1.LocalReport.SetParameters( hueco_91 );
            ReportViewer1.LocalReport.SetParameters( hueco_92 );
            ReportViewer1.LocalReport.SetParameters( hueco_93 );
            ReportViewer1.LocalReport.SetParameters( hueco_94 );
            ReportViewer1.LocalReport.SetParameters( hueco_95 );
            ReportViewer1.LocalReport.SetParameters( hueco_101 );
            ReportViewer1.LocalReport.SetParameters( hueco_102 );
            ReportViewer1.LocalReport.SetParameters( hueco_103 );
            ReportViewer1.LocalReport.SetParameters( hueco_104 );
            ReportViewer1.LocalReport.SetParameters( hueco_105 );
            ReportViewer1.LocalReport.SetParameters( hueco_111 );
            ReportViewer1.LocalReport.SetParameters( hueco_112 );
            ReportViewer1.LocalReport.SetParameters( hueco_113 );
            ReportViewer1.LocalReport.SetParameters( hueco_114 );
            ReportViewer1.LocalReport.SetParameters( hueco_115 );
            ReportViewer1.LocalReport.SetParameters( hueco_121 );
            ReportViewer1.LocalReport.SetParameters( hueco_122 );
            ReportViewer1.LocalReport.SetParameters( hueco_123 );
            ReportViewer1.LocalReport.SetParameters( hueco_124 );
            ReportViewer1.LocalReport.SetParameters( hueco_125 );
            ReportViewer1.LocalReport.SetParameters( hueco_131 );
            ReportViewer1.LocalReport.SetParameters( hueco_132 );
            ReportViewer1.LocalReport.SetParameters( hueco_133 );
            ReportViewer1.LocalReport.SetParameters( hueco_134 );
            ReportViewer1.LocalReport.SetParameters( hueco_135 );
            ReportViewer1.LocalReport.SetParameters( hueco_141 );
            ReportViewer1.LocalReport.SetParameters( hueco_142 );
            ReportViewer1.LocalReport.SetParameters( hueco_143 );
            ReportViewer1.LocalReport.SetParameters( hueco_144 );
            ReportViewer1.LocalReport.SetParameters( hueco_145 );
            ReportViewer1.LocalReport.SetParameters( hueco_151 );
            ReportViewer1.LocalReport.SetParameters( hueco_152 );
            ReportViewer1.LocalReport.SetParameters( hueco_153 );
            ReportViewer1.LocalReport.SetParameters( hueco_154 );
            ReportViewer1.LocalReport.SetParameters( hueco_155 );
            ReportViewer1.LocalReport.SetParameters( hueco_161 );
            ReportViewer1.LocalReport.SetParameters( hueco_162 );
            ReportViewer1.LocalReport.SetParameters( hueco_163 );
            ReportViewer1.LocalReport.SetParameters( hueco_164 );
            ReportViewer1.LocalReport.SetParameters( hueco_165 );
            ReportViewer1.LocalReport.SetParameters( hueco_171 );
            ReportViewer1.LocalReport.SetParameters( hueco_172 );
            ReportViewer1.LocalReport.SetParameters( hueco_173 );
            ReportViewer1.LocalReport.SetParameters( hueco_174 );
            ReportViewer1.LocalReport.SetParameters( hueco_175 );
            ReportViewer1.LocalReport.SetParameters( hueco_181 );
            ReportViewer1.LocalReport.SetParameters( hueco_182 );
            ReportViewer1.LocalReport.SetParameters( hueco_183 );
            ReportViewer1.LocalReport.SetParameters( hueco_184 );
            ReportViewer1.LocalReport.SetParameters( hueco_185 );
            ReportViewer1.LocalReport.SetParameters( hueco_191 );
            ReportViewer1.LocalReport.SetParameters( hueco_192 );
            ReportViewer1.LocalReport.SetParameters( hueco_193 );
            ReportViewer1.LocalReport.SetParameters( hueco_194 );
            ReportViewer1.LocalReport.SetParameters(hueco_195);

            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
            }

        }

        private void consultaGeneralR155(string edificio, string fechadesde, string fechahasta, string BaseDatos)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_R155InspeccionySeguimiento.rdlc";

            NA_R155_InspeccionySeguimientoObra nr155 = new NA_R155_InspeccionySeguimientoObra();
            DataSet tuplas = nr155.get_mostrarR155Realizadas(edificio, fechadesde, fechahasta);
            DataTable DSconsulta = tuplas.Tables[0];

            ReportParameter p_DB = new ReportParameter("DB_Datos", BaseDatos);
            ReportParameter p_fechadesde = new ReportParameter("fechadesde", tx_desdeFecha.Text);
            ReportParameter p_fechahasta = new ReportParameter("fechahasta", tx_hastaFecha.Text);
            ReportDataSource DSR155InspeccionySeguimiento = new ReportDataSource("DS_R155InspeccionySeguimiento", DSconsulta);

            ReportViewer1.LocalReport.SetParameters(p_DB);
            ReportViewer1.LocalReport.SetParameters(p_fechadesde);
            ReportViewer1.LocalReport.SetParameters(p_fechahasta);
            ReportViewer1.LocalReport.DataSources.Add(DSR155InspeccionySeguimiento);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }


    }
}