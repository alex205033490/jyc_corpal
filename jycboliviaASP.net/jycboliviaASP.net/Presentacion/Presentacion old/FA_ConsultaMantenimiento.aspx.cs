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
using System.Web.Services;
using System.Web.Script.Services;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_ConsultaMantenimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();
            if (tienePermisoDeIngreso(68) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }
            if (!IsPostBack)
            {
                cargarCobrador();
            } 
        }

        // webservice que me permite la autocompletacion
        [WebMethod]
        [ScriptMethod]
        // se devuelve un arreglo con la informacion
        public static string[] GetlistaProyectos2(string prefixText, int count)
        {
            string nombreProyecto = prefixText;

            NProyecto proyectoN = new NProyecto();
            DataSet tuplas = proyectoN.buscadorCallCenter(nombreProyecto);
            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;

            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][0].ToString();
            }

            return lista;
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

        private void cargarCobrador()
        {
            NA_Responsables Nresp = new NA_Responsables();
            dd_cobrador.DataSource = Nresp.getResponsableCobrador(-1,"");
            dd_cobrador.DataValueField = "codigo";
            dd_cobrador.DataTextField = "nombre";
            dd_cobrador.Items.Add(new ListItem("--Todos--", "-1"));
            dd_cobrador.AppendDataBoundItems = true;
            dd_cobrador.SelectedIndex = -1;
            dd_cobrador.DataBind();
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


        private void get_libroDiarioCobranza(string fecha1, string fecha2, int codCobrador, string nameCobrador)
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_LibroDiarioCobranza.rdlc";

            NA_Seguimiento nseg = new NA_Seguimiento();
            DataSet consulta1 = nseg.getLibroDiarioCobranza(fecha1, fecha2, codCobrador);            
            DataTable DSconsulta = consulta1.Tables[0];

            ReportParameter nombreCobrador = new ReportParameter("p_nameCobrador", nameCobrador);
            ReportDataSource DSLibroDiarioCobranzas = new ReportDataSource("DS_librodiariocobranza", DSconsulta);

            ReportViewer1.LocalReport.SetParameters(nombreCobrador);
            ReportViewer1.LocalReport.DataSources.Add(DSLibroDiarioCobranzas);            
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }


        private void consultadedatos()
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;

            string fechadesde = convertidorFecha(tx_desdeFecha.Text);
            string fechahasta = convertidorFecha(tx_hastaFecha.Text);
            int codigoCobrador = Convert.ToInt32(dd_cobrador.SelectedValue.ToString());
            string nombreCobrador = dd_cobrador.SelectedItem.Text;

               if (dd_consulta.SelectedIndex == 1)
                {
                    if (dd_consulta.SelectedIndex > -1 && !fechadesde.Equals("null") && !fechahasta.Equals("null"))
                    { 
                     get_libroDiarioCobranza(fechadesde, fechahasta, codigoCobrador, nombreCobrador);
                    }
                    else
                        Response.Write("<script type='text/javascript'> alert('Error: Datos incorrectos') </script>");
                }
               if (dd_consulta.SelectedIndex == 2)
               {
                   getEncuestasMantenimientoRealizado();
                }
               if (dd_consulta.SelectedIndex == 3)
               {
                   get_BoletaSola();
               }
               if (dd_consulta.SelectedIndex == 4)
               {
                   get_BoletaEmergencia();
               } 
                if(dd_consulta.SelectedIndex == 5){
                    if (dd_consulta.SelectedIndex > -1 && !fechadesde.Equals("null") && !fechahasta.Equals("null"))
                    {
                        get_BoletasPreventiva_Emergencia();
                    }else
                        Response.Write("<script type='text/javascript'> alert('Error: Datos incorrectos') </script>");                    
                }
        }

        private void get_BoletasPreventiva_Emergencia()
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_BoletasMantenimiento.rdlc";
            string edificio = tx_edificio.Text;
            string fechadesde = convertidorFecha(tx_desdeFecha.Text);
            string fechahasta = convertidorFecha(tx_hastaFecha.Text);
            
            if (!fechadesde.Equals("null") && !fechahasta.Equals("null"))
            {
                NA_Seguimiento nseg = new NA_Seguimiento();
                DataSet consulta1 = nseg.getBoletasMantenimientoPreventivo(edificio, fechadesde, fechahasta);
                DataSet consulta2 = nseg.getBoletasMantenimientoEmergenciayOtros(edificio, fechadesde, fechahasta);

                DataTable DSconsulta1 = consulta1.Tables[0];
                DataTable DSconsulta2 = consulta2.Tables[0];

                ReportDataSource DS_BoletasMantenimiento = new ReportDataSource("DS_BoletasMantenimiento", DSconsulta1);
                ReportDataSource DS_BoletasEmergencia = new ReportDataSource("DS_BoletasEmergencia", DSconsulta2);


                ReportViewer1.LocalReport.DataSources.Add(DS_BoletasMantenimiento);
                ReportViewer1.LocalReport.DataSources.Add(DS_BoletasEmergencia);
                
                this.ReportViewer1.LocalReport.Refresh();
                this.ReportViewer1.DataBind();
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: No hay Datos') </script>");



        }

        private void get_BoletaEmergencia()
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_ReporteEmergenciaCallCenter.rdlc";
            string nroBoleta = tx_nroBoleta.Text;
            NA_Seguimiento nseg = new NA_Seguimiento();
            DataSet consulta1 = nseg.getBoletaEmergencia(nroBoleta);
            if (consulta1.Tables[0].Rows.Count > 0)
            {
                string ruta = ConfigurationManager.AppSettings["image_logo"];
                string nombreImagen = "jyc";
                string baseDatos = Session["BaseDatos"].ToString();

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

                ReportParameter imagen = new ReportParameter("p_logo", @"file:\" + direccionImagen + ".jpg");

                ReportParameter boleta = new ReportParameter("boleta", consulta1.Tables[0].Rows[0][1].ToString());
                ReportParameter p_fecha = new ReportParameter("p_fecha", consulta1.Tables[0].Rows[0][2].ToString());                
                ReportParameter p_edificio = new ReportParameter("p_edificio", consulta1.Tables[0].Rows[0][3].ToString());
                ReportParameter p_telefono = new ReportParameter("p_telefono", consulta1.Tables[0].Rows[0][4].ToString());
                ReportParameter p_ciudad = new ReportParameter("p_ciudad", Session["BaseDatos"].ToString());
                ReportParameter p_exbo = new ReportParameter("p_exbo", consulta1.Tables[0].Rows[0][5].ToString());
                ReportParameter ee_estadoequipo = new ReportParameter("ee_estadoequipo", consulta1.Tables[0].Rows[0][6].ToString());
                ReportParameter p_tipodeboleta = new ReportParameter("p_tipodeboleta", consulta1.Tables[0].Rows[0][7].ToString());                

                ReportParameter i_fusiblecontactos = new ReportParameter("i_fusiblecontactos", consulta1.Tables[0].Rows[0][8].ToString());
                ReportParameter i_botoneradepisoencorte = new ReportParameter("i_botoneradepisoencorte", consulta1.Tables[0].Rows[0][9].ToString());
                ReportParameter i_limites = new ReportParameter("i_limites", consulta1.Tables[0].Rows[0][10].ToString());
                ReportParameter i_reguladordevelocidad = new ReportParameter("i_reguladordevelocidad", consulta1.Tables[0].Rows[0][11].ToString());
                ReportParameter i_frenobalataselectroiman = new ReportParameter("i_frenobalataselectroiman", consulta1.Tables[0].Rows[0][12].ToString());
                ReportParameter i_motordetraccion = new ReportParameter("i_motordetraccion", consulta1.Tables[0].Rows[0][13].ToString());
                ReportParameter i_poleas = new ReportParameter("i_poleas", consulta1.Tables[0].Rows[0][14].ToString());
                ReportParameter i_filtraciondeaguaensalademaquinas = new ReportParameter("i_filtraciondeaguaensalademaquinas", consulta1.Tables[0].Rows[0][15].ToString());
                ReportParameter i_accesoirregularasalademaquinas = new ReportParameter("i_accesoirregularasalademaquinas", consulta1.Tables[0].Rows[0][16].ToString());
                ReportParameter i_corteensenalizadoropulsadordepiso = new ReportParameter("i_corteensenalizadoropulsadordepiso", consulta1.Tables[0].Rows[0][17].ToString());
                ReportParameter i_ruidooajusteenpuertaspisocabina = new ReportParameter("i_ruidooajusteenpuertaspisocabina", consulta1.Tables[0].Rows[0][18].ToString());
                ReportParameter i_iluminaciondecabina = new ReportParameter("i_iluminaciondecabina", consulta1.Tables[0].Rows[0][19].ToString());
                ReportParameter i_operadordepuertas = new ReportParameter("i_operadordepuertas", consulta1.Tables[0].Rows[0][20].ToString());
                ReportParameter i_motordeoperador = new ReportParameter("i_motordeoperador", consulta1.Tables[0].Rows[0][21].ToString());
                ReportParameter i_ventiladordecabina = new ReportParameter("i_ventiladordecabina", consulta1.Tables[0].Rows[0][22].ToString());
                ReportParameter i_cerrojo = new ReportParameter("i_cerrojo", consulta1.Tables[0].Rows[0][23].ToString());
                ReportParameter i_sensordecabinabarrerafotocelula = new ReportParameter("i_sensordecabinabarrerafotocelula", consulta1.Tables[0].Rows[0][24].ToString());
                ReportParameter i_filtraciondeaguaenhuecoyfoso = new ReportParameter("i_filtraciondeaguaenhuecoyfoso", consulta1.Tables[0].Rows[0][25].ToString());
                ReportParameter i_bajasocortedetension = new ReportParameter("i_bajasocortedetension", consulta1.Tables[0].Rows[0][26].ToString());
                ReportParameter i_sensores = new ReportParameter("i_sensores", consulta1.Tables[0].Rows[0][27].ToString());
                ReportParameter i_malusoporusuario = new ReportParameter("i_malusoporusuario", consulta1.Tables[0].Rows[0][28].ToString());
                ReportParameter i_iluminacionirregularensalademaquinasyfoso = new ReportParameter("i_iluminacionirregularensalademaquinasyfoso", consulta1.Tables[0].Rows[0][29].ToString());
                ReportParameter i_otros = new ReportParameter("i_otros", consulta1.Tables[0].Rows[0][30].ToString());
                ReportParameter observaciones = new ReportParameter("observaciones", consulta1.Tables[0].Rows[0][31].ToString());
                
                ReportParameter tec_nombre = new ReportParameter("tec_nombre", consulta1.Tables[0].Rows[0][32].ToString());
                ReportParameter tec_codigo = new ReportParameter("tec_codigo", consulta1.Tables[0].Rows[0][33].ToString());
                ReportParameter tec_horallegada = new ReportParameter("tec_horallegada", consulta1.Tables[0].Rows[0][34].ToString());
                ReportParameter tec_horasalida = new ReportParameter("tec_horasalida", consulta1.Tables[0].Rows[0][35].ToString());

                ReportParameter cli_nombre = new ReportParameter("cli_nombre", consulta1.Tables[0].Rows[0][36].ToString());
                ReportParameter cli_ci = new ReportParameter("cli_ci", consulta1.Tables[0].Rows[0][37].ToString());
                ReportParameter cli_cargo = new ReportParameter("cli_cargo", consulta1.Tables[0].Rows[0][38].ToString());
                

                ///-----------------------------------------------------------------------------------------------
                ReportViewer1.LocalReport.SetParameters(p_tipodeboleta);
                ReportViewer1.LocalReport.SetParameters(imagen);
                ReportViewer1.LocalReport.SetParameters(boleta);
                ReportViewer1.LocalReport.SetParameters(p_edificio);
                ReportViewer1.LocalReport.SetParameters(p_fecha);
                ReportViewer1.LocalReport.SetParameters(p_ciudad);
                ReportViewer1.LocalReport.SetParameters(p_telefono);                
                ReportViewer1.LocalReport.SetParameters(p_exbo);
                ReportViewer1.LocalReport.SetParameters(ee_estadoequipo);
                ReportViewer1.LocalReport.SetParameters(observaciones);
                ReportViewer1.LocalReport.SetParameters(i_fusiblecontactos);
                ReportViewer1.LocalReport.SetParameters(i_botoneradepisoencorte);
                ReportViewer1.LocalReport.SetParameters(i_limites);
                ReportViewer1.LocalReport.SetParameters(i_reguladordevelocidad);
                ReportViewer1.LocalReport.SetParameters(i_frenobalataselectroiman);
                ReportViewer1.LocalReport.SetParameters(i_motordetraccion);
                ReportViewer1.LocalReport.SetParameters(i_poleas);
                ReportViewer1.LocalReport.SetParameters(i_filtraciondeaguaensalademaquinas);
                ReportViewer1.LocalReport.SetParameters(i_accesoirregularasalademaquinas);
                ReportViewer1.LocalReport.SetParameters(i_corteensenalizadoropulsadordepiso);
                ReportViewer1.LocalReport.SetParameters(i_ruidooajusteenpuertaspisocabina);
                ReportViewer1.LocalReport.SetParameters(i_iluminaciondecabina);
                ReportViewer1.LocalReport.SetParameters(i_operadordepuertas);
                ReportViewer1.LocalReport.SetParameters(i_motordeoperador);
                ReportViewer1.LocalReport.SetParameters(i_ventiladordecabina);
                ReportViewer1.LocalReport.SetParameters(i_cerrojo);
                ReportViewer1.LocalReport.SetParameters(i_sensordecabinabarrerafotocelula);
                ReportViewer1.LocalReport.SetParameters(i_filtraciondeaguaenhuecoyfoso);
                ReportViewer1.LocalReport.SetParameters(i_bajasocortedetension);
                ReportViewer1.LocalReport.SetParameters(i_sensores);
                ReportViewer1.LocalReport.SetParameters(i_malusoporusuario);
                ReportViewer1.LocalReport.SetParameters(i_iluminacionirregularensalademaquinasyfoso);
                ReportViewer1.LocalReport.SetParameters(i_otros);
                ReportViewer1.LocalReport.SetParameters(tec_nombre);
                ReportViewer1.LocalReport.SetParameters(tec_codigo);
                ReportViewer1.LocalReport.SetParameters(tec_horallegada);
                ReportViewer1.LocalReport.SetParameters(tec_horasalida);
                ReportViewer1.LocalReport.SetParameters(cli_nombre);
                ReportViewer1.LocalReport.SetParameters(cli_ci);
                ReportViewer1.LocalReport.SetParameters(cli_cargo);

                this.ReportViewer1.LocalReport.Refresh();
                this.ReportViewer1.DataBind();
            }
            else
                Response.Write("<script type='text/javascript'> alert('Error: No hay Datos') </script>");
        }

        private void get_BoletaSola()
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_ReporteMantenimiento_R124.rdlc";            
            string nroBoleta = tx_nroBoleta.Text;
            NA_Seguimiento nseg = new NA_Seguimiento();
            DataSet consulta1 = nseg.getBoletaSolaNro(nroBoleta);
            if(consulta1.Tables[0].Rows.Count > 0){
                string ruta = ConfigurationManager.AppSettings["image_logo"];
                string nombreImagen = "jyc";
                string baseDatos = Session["BaseDatos"].ToString();

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

                ReportParameter imagen = new ReportParameter("p_logo", @"file:\" + direccionImagen + ".jpg");

                ReportParameter boleta = new ReportParameter("boleta", consulta1.Tables[0].Rows[0][1].ToString());
                ReportParameter p_edificio = new ReportParameter("p_edificio", consulta1.Tables[0].Rows[0][2].ToString());
                ReportParameter p_fecha = new ReportParameter("p_fecha", consulta1.Tables[0].Rows[0][3].ToString());
                ReportParameter p_ciudad = new ReportParameter("p_ciudad", Session["BaseDatos"].ToString());
                ReportParameter p_telefono = new ReportParameter("p_telefono", "");
                ReportParameter p_email = new ReportParameter("p_email", "");
                ReportParameter p_exbo = new ReportParameter("p_exbo", consulta1.Tables[0].Rows[0][4].ToString());
                
                ReportParameter eq_ascensorelectrico = new ReportParameter("eq_ascensorelectrico", consulta1.Tables[0].Rows[0][5].ToString());
                ReportParameter eq_ascensorhidraulico = new ReportParameter("eq_ascensorhidraulico", consulta1.Tables[0].Rows[0][6].ToString());
                ReportParameter eq_escaleramecanica = new ReportParameter("eq_escaleramecanica", consulta1.Tables[0].Rows[0][7].ToString());
                ReportParameter eq_plataforma = new ReportParameter("eq_plataforma", consulta1.Tables[0].Rows[0][8].ToString());
                ReportParameter eq_montacoches = new ReportParameter("eq_montacoches", consulta1.Tables[0].Rows[0][9].ToString());
                ReportParameter eq_minicarga = new ReportParameter("eq_minicarga", consulta1.Tables[0].Rows[0][10].ToString());

                ReportParameter ee_ff = new ReportParameter("ee_ff", consulta1.Tables[0].Rows[0][11].ToString());
                ReportParameter ee_fp = new ReportParameter("ee_fp", consulta1.Tables[0].Rows[0][12].ToString());
                ReportParameter ee_pf = new ReportParameter("ee_pf", consulta1.Tables[0].Rows[0][13].ToString());
                ReportParameter ee_pp = new ReportParameter("ee_pp", consulta1.Tables[0].Rows[0][14].ToString());
                ReportParameter ee_pec = new ReportParameter("ee_pec", consulta1.Tables[0].Rows[0][15].ToString());

                ReportParameter sc_motor = new ReportParameter("sc_motor", consulta1.Tables[0].Rows[0][16].ToString());
                ReportParameter sc_poleas = new ReportParameter("sc_poleas", consulta1.Tables[0].Rows[0][17].ToString());
                ReportParameter sc_aceitemotor = new ReportParameter("sc_aceitemotor", consulta1.Tables[0].Rows[0][18].ToString());
                ReportParameter sc_cabletraccion = new ReportParameter("sc_cabletraccion", consulta1.Tables[0].Rows[0][19].ToString());
                ReportParameter sc_ventilador = new ReportParameter("sc_ventilador", consulta1.Tables[0].Rows[0][20].ToString());
                ReportParameter sc_freno = new ReportParameter("sc_freno", consulta1.Tables[0].Rows[0][21].ToString());
                ReportParameter sc_bobina = new ReportParameter("sc_bobina", consulta1.Tables[0].Rows[0][22].ToString());
                ReportParameter sc_lvelocidad = new ReportParameter("sc_lvelocidad", consulta1.Tables[0].Rows[0][23].ToString());
                ReportParameter sc_reduccionjuego = new ReportParameter("sc_reduccionjuego", consulta1.Tables[0].Rows[0][24].ToString());
                ReportParameter sc_cpu = new ReportParameter("sc_cpu", consulta1.Tables[0].Rows[0][25].ToString());
                ReportParameter sc_tarjetas = new ReportParameter("sc_tarjetas", consulta1.Tables[0].Rows[0][26].ToString());
                ReportParameter sc_conectores = new ReportParameter("sc_conectores", consulta1.Tables[0].Rows[0][27].ToString());
                ReportParameter sc_auxiliares = new ReportParameter("sc_auxiliares", consulta1.Tables[0].Rows[0][28].ToString());
                ReportParameter sc_aelectrica = new ReportParameter("sc_aelectrica", consulta1.Tables[0].Rows[0][29].ToString());
                ReportParameter sc_reguladordevelocidad = new ReportParameter("sc_reguladordevelocidad", consulta1.Tables[0].Rows[0][30].ToString());
                ReportParameter sc_unidadhidraulica = new ReportParameter("sc_unidadhidraulica", consulta1.Tables[0].Rows[0][31].ToString());
                ReportParameter sc_valvulahidraulica = new ReportParameter("sc_valvulahidraulica", consulta1.Tables[0].Rows[0][32].ToString());
                ReportParameter sc_cadenaprincipal = new ReportParameter("sc_cadenaprincipal", consulta1.Tables[0].Rows[0][33].ToString());
                ReportParameter sc_sistemalubricacion = new ReportParameter("sc_sistemalubricacion", consulta1.Tables[0].Rows[0][34].ToString());
                ReportParameter sc_contyseriedeseguridad = new ReportParameter("sc_contyseriedeseguridad", consulta1.Tables[0].Rows[0][35].ToString());
                ReportParameter sc_accesos = new ReportParameter("sc_accesos", consulta1.Tables[0].Rows[0][36].ToString());
                ReportParameter sc_limpieza = new ReportParameter("sc_limpieza", consulta1.Tables[0].Rows[0][37].ToString());

                ReportParameter c_botonera = new ReportParameter("c_botonera", consulta1.Tables[0].Rows[0][38].ToString());
                ReportParameter c_indicadores = new ReportParameter("c_indicadores", consulta1.Tables[0].Rows[0][39].ToString());
                ReportParameter c_iluminacion = new ReportParameter("c_iluminacion", consulta1.Tables[0].Rows[0][40].ToString());
                ReportParameter c_puertacabina = new ReportParameter("c_puertacabina", consulta1.Tables[0].Rows[0][41].ToString());
                ReportParameter c_ajusteenviaje = new ReportParameter("c_ajusteenviaje", consulta1.Tables[0].Rows[0][42].ToString());
                ReportParameter c_ventilador = new ReportParameter("c_ventilador", consulta1.Tables[0].Rows[0][43].ToString());
                ReportParameter c_barrerafotoelec = new ReportParameter("c_barrerafotoelec", consulta1.Tables[0].Rows[0][44].ToString());
                ReportParameter c_holguradecab = new ReportParameter("c_holguradecab", consulta1.Tables[0].Rows[0][45].ToString());
                ReportParameter c_guias = new ReportParameter("c_guias", consulta1.Tables[0].Rows[0][46].ToString());
                ReportParameter c_vidrioespejopaneles = new ReportParameter("c_vidrioespejopaneles", consulta1.Tables[0].Rows[0][47].ToString());
                ReportParameter c_operadordepuertas = new ReportParameter("c_operadordepuertas", consulta1.Tables[0].Rows[0][48].ToString());
                ReportParameter c_contyseriedeseguridad = new ReportParameter("c_contyseriedeseguridad", consulta1.Tables[0].Rows[0][49].ToString());
                ReportParameter c_pasamanos = new ReportParameter("c_pasamanos", consulta1.Tables[0].Rows[0][50].ToString());
                ReportParameter c_limpieza = new ReportParameter("c_limpieza", consulta1.Tables[0].Rows[0][51].ToString());

                ReportParameter a_botonera = new ReportParameter("a_botonera", consulta1.Tables[0].Rows[0][52].ToString());
                ReportParameter a_indicadores = new ReportParameter("a_indicadores", consulta1.Tables[0].Rows[0][53].ToString());
                ReportParameter a_puerta = new ReportParameter("a_puerta", consulta1.Tables[0].Rows[0][54].ToString());
                ReportParameter a_guiapatines = new ReportParameter("a_guiapatines", consulta1.Tables[0].Rows[0][55].ToString());
                ReportParameter a_cerrojos = new ReportParameter("a_cerrojos", consulta1.Tables[0].Rows[0][56].ToString());
                ReportParameter a_padeenclavamiento = new ReportParameter("a_padeenclavamiento", consulta1.Tables[0].Rows[0][57].ToString());
                ReportParameter a_sensores = new ReportParameter("a_sensores", consulta1.Tables[0].Rows[0][58].ToString());
                ReportParameter a_peines = new ReportParameter("a_peines", consulta1.Tables[0].Rows[0][59].ToString());
                ReportParameter a_peldanosfaldon = new ReportParameter("a_peldanosfaldon", consulta1.Tables[0].Rows[0][60].ToString());
                ReportParameter a_demarcaciones = new ReportParameter("a_demarcaciones", consulta1.Tables[0].Rows[0][61].ToString());
                ReportParameter a_botondeemergencia = new ReportParameter("a_botondeemergencia", consulta1.Tables[0].Rows[0][62].ToString());
                ReportParameter a_contyseriedeseguridad = new ReportParameter("a_contyseriedeseguridad", consulta1.Tables[0].Rows[0][63].ToString());
                ReportParameter a_senales = new ReportParameter("a_senales", consulta1.Tables[0].Rows[0][64].ToString());
                ReportParameter a_limpieza = new ReportParameter("a_limpieza", consulta1.Tables[0].Rows[0][65].ToString());


                ReportParameter f_cablesdetraccion = new ReportParameter("f_cablesdetraccion", consulta1.Tables[0].Rows[0][66].ToString());
                ReportParameter f_cablelimitador = new ReportParameter("f_cablelimitador", consulta1.Tables[0].Rows[0][67].ToString());
                ReportParameter f_cableviajero = new ReportParameter("f_cableviajero", consulta1.Tables[0].Rows[0][68].ToString());
                ReportParameter f_contrapeso = new ReportParameter("f_contrapeso", consulta1.Tables[0].Rows[0][69].ToString());
                ReportParameter f_fdecarrerasuperior = new ReportParameter("f_fdecarrerasuperior", consulta1.Tables[0].Rows[0][70].ToString());
                ReportParameter f_fdecarrerainferior = new ReportParameter("f_fdecarrerainferior", consulta1.Tables[0].Rows[0][71].ToString());
                ReportParameter f_paracaidas = new ReportParameter("f_paracaidas", consulta1.Tables[0].Rows[0][72].ToString());
                ReportParameter f_topespistones = new ReportParameter("f_topespistones", consulta1.Tables[0].Rows[0][73].ToString());
                ReportParameter f_poleatensora = new ReportParameter("f_poleatensora", consulta1.Tables[0].Rows[0][74].ToString());
                ReportParameter f_poleas = new ReportParameter("f_poleas", consulta1.Tables[0].Rows[0][75].ToString());
                ReportParameter f_rieles = new ReportParameter("f_rieles", consulta1.Tables[0].Rows[0][76].ToString());
                ReportParameter f_aceiteras = new ReportParameter("f_aceiteras", consulta1.Tables[0].Rows[0][77].ToString());
                ReportParameter f_stopdefosa = new ReportParameter("f_stopdefosa", consulta1.Tables[0].Rows[0][78].ToString());
                ReportParameter f_resortes = new ReportParameter("f_resortes", consulta1.Tables[0].Rows[0][79].ToString());
                ReportParameter f_tensiondecadena = new ReportParameter("f_tensiondecadena", consulta1.Tables[0].Rows[0][80].ToString());
                ReportParameter f_contyseriedeseguridad = new ReportParameter("f_contyseriedeseguridad", consulta1.Tables[0].Rows[0][81].ToString());
                ReportParameter f_mordazas = new ReportParameter("f_mordazas", consulta1.Tables[0].Rows[0][82].ToString());
                ReportParameter f_limpieza = new ReportParameter("f_limpieza", consulta1.Tables[0].Rows[0][83].ToString());

                ReportParameter materialesyrepuesto = new ReportParameter("materialesyrepuesto", consulta1.Tables[0].Rows[0][84].ToString());
                ReportParameter observacion = new ReportParameter("observaciones", consulta1.Tables[0].Rows[0][85].ToString());

                ReportParameter i_fusiblecontactos = new ReportParameter("i_fusiblecontactos", consulta1.Tables[0].Rows[0][86].ToString());
                ReportParameter i_botoneradepisoencorte = new ReportParameter("i_botoneradepisoencorte", consulta1.Tables[0].Rows[0][87].ToString());
                ReportParameter i_limites = new ReportParameter("i_limites", consulta1.Tables[0].Rows[0][88].ToString());
                ReportParameter i_reguladordevelocidad = new ReportParameter("i_reguladordevelocidad", consulta1.Tables[0].Rows[0][89].ToString());
                ReportParameter i_frenobalataselectroiman = new ReportParameter("i_frenobalataselectroiman", consulta1.Tables[0].Rows[0][90].ToString());
                ReportParameter i_motordetraccion = new ReportParameter("i_motordetraccion", consulta1.Tables[0].Rows[0][91].ToString());
                ReportParameter i_poleas = new ReportParameter("i_poleas", consulta1.Tables[0].Rows[0][92].ToString());
                ReportParameter i_filtraciondeaguaensalademaquinas = new ReportParameter("i_filtraciondeaguaensalademaquinas", consulta1.Tables[0].Rows[0][93].ToString());
                ReportParameter i_accesoirregularasalademaquinas = new ReportParameter("i_accesoirregularasalademaquinas", consulta1.Tables[0].Rows[0][94].ToString());
                ReportParameter i_corteensenalizadoropulsadordepiso = new ReportParameter("i_corteensenalizadoropulsadordepiso", consulta1.Tables[0].Rows[0][95].ToString());
                ReportParameter i_ruidooajusteenpuertaspisocabina = new ReportParameter("i_ruidooajusteenpuertaspisocabina", consulta1.Tables[0].Rows[0][96].ToString());
                ReportParameter i_iluminaciondecabina = new ReportParameter("i_iluminaciondecabina", consulta1.Tables[0].Rows[0][97].ToString());
                ReportParameter i_operadordepuertas = new ReportParameter("i_operadordepuertas", consulta1.Tables[0].Rows[0][98].ToString());
                ReportParameter i_motordeoperador = new ReportParameter("i_motordeoperador", consulta1.Tables[0].Rows[0][99].ToString());
                ReportParameter i_ventiladordecabina = new ReportParameter("i_ventiladordecabina", consulta1.Tables[0].Rows[0][100].ToString());
                ReportParameter i_cerrojo = new ReportParameter("i_cerrojo", consulta1.Tables[0].Rows[0][101].ToString());
                ReportParameter i_sensordecabinabarrerafotocelula = new ReportParameter("i_sensordecabinabarrerafotocelula", consulta1.Tables[0].Rows[0][102].ToString());
                ReportParameter i_filtraciondeaguaenhuecoyfoso = new ReportParameter("i_filtraciondeaguaenhuecoyfoso", consulta1.Tables[0].Rows[0][103].ToString());
                ReportParameter i_bajasocortedetension = new ReportParameter("i_bajasocortedetension", consulta1.Tables[0].Rows[0][104].ToString());
                ReportParameter i_sensores = new ReportParameter("i_sensores", consulta1.Tables[0].Rows[0][105].ToString());
                ReportParameter i_malusoporusuario = new ReportParameter("i_malusoporusuario", consulta1.Tables[0].Rows[0][106].ToString());
                ReportParameter i_iluminacionirregularensalademaquinasyfoso = new ReportParameter("i_iluminacionirregularensalademaquinasyfoso", consulta1.Tables[0].Rows[0][107].ToString());
                ReportParameter i_otros = new ReportParameter("i_otros", consulta1.Tables[0].Rows[0][108].ToString());

                ReportParameter tec_nombre = new ReportParameter("tec_nombre", consulta1.Tables[0].Rows[0][109].ToString());
                ReportParameter tec_codigo = new ReportParameter("tec_codigo", consulta1.Tables[0].Rows[0][110].ToString());
                ReportParameter tec_horallegada = new ReportParameter("tec_horallegada", consulta1.Tables[0].Rows[0][111].ToString());
                ReportParameter tec_horasalida = new ReportParameter("tec_horasalida", consulta1.Tables[0].Rows[0][112].ToString());

                ReportParameter cli_nombre = new ReportParameter("cli_nombre", consulta1.Tables[0].Rows[0][113].ToString());
                ReportParameter cli_ci = new ReportParameter("cli_ci", consulta1.Tables[0].Rows[0][114].ToString());
                ReportParameter cli_cargo = new ReportParameter("cli_cargo", consulta1.Tables[0].Rows[0][115].ToString());
                ReportParameter p_tipodeboleta = new ReportParameter("p_tipodeboleta", consulta1.Tables[0].Rows[0][116].ToString());
                
                ///-----------------------------------------------------------------------------------------------
                ReportViewer1.LocalReport.SetParameters(p_tipodeboleta);
                ReportViewer1.LocalReport.SetParameters(imagen);
                ReportViewer1.LocalReport.SetParameters(boleta);
                ReportViewer1.LocalReport.SetParameters(p_edificio);
                ReportViewer1.LocalReport.SetParameters(p_fecha);
                ReportViewer1.LocalReport.SetParameters(p_ciudad);
                ReportViewer1.LocalReport.SetParameters(p_telefono);
                ReportViewer1.LocalReport.SetParameters(p_email);
                ReportViewer1.LocalReport.SetParameters(p_exbo);
                ReportViewer1.LocalReport.SetParameters(eq_ascensorelectrico);
                ReportViewer1.LocalReport.SetParameters(eq_ascensorhidraulico);
                ReportViewer1.LocalReport.SetParameters(eq_escaleramecanica);
                ReportViewer1.LocalReport.SetParameters(eq_plataforma);
                ReportViewer1.LocalReport.SetParameters(eq_montacoches);
                ReportViewer1.LocalReport.SetParameters(eq_minicarga);
                ReportViewer1.LocalReport.SetParameters(ee_ff);
                ReportViewer1.LocalReport.SetParameters(ee_fp);
                ReportViewer1.LocalReport.SetParameters(ee_pf);
                ReportViewer1.LocalReport.SetParameters(ee_pp);
                ReportViewer1.LocalReport.SetParameters(ee_pec);
                ReportViewer1.LocalReport.SetParameters(sc_motor);
                ReportViewer1.LocalReport.SetParameters(sc_poleas);
                ReportViewer1.LocalReport.SetParameters(sc_aceitemotor);
                ReportViewer1.LocalReport.SetParameters(sc_cabletraccion);
                ReportViewer1.LocalReport.SetParameters(sc_ventilador);
                ReportViewer1.LocalReport.SetParameters(sc_freno);
                ReportViewer1.LocalReport.SetParameters(sc_bobina);
                ReportViewer1.LocalReport.SetParameters(sc_lvelocidad);
                ReportViewer1.LocalReport.SetParameters(sc_reduccionjuego);
                ReportViewer1.LocalReport.SetParameters(sc_cpu);
                ReportViewer1.LocalReport.SetParameters(sc_tarjetas);
                ReportViewer1.LocalReport.SetParameters(sc_conectores);
                ReportViewer1.LocalReport.SetParameters(sc_auxiliares);
                ReportViewer1.LocalReport.SetParameters(sc_aelectrica);
                ReportViewer1.LocalReport.SetParameters(sc_reguladordevelocidad);
                ReportViewer1.LocalReport.SetParameters(sc_unidadhidraulica);
                ReportViewer1.LocalReport.SetParameters(sc_valvulahidraulica);
                ReportViewer1.LocalReport.SetParameters(sc_cadenaprincipal);
                ReportViewer1.LocalReport.SetParameters(sc_sistemalubricacion);
                ReportViewer1.LocalReport.SetParameters(sc_contyseriedeseguridad);
                ReportViewer1.LocalReport.SetParameters(sc_accesos);
                ReportViewer1.LocalReport.SetParameters(sc_limpieza);
                ReportViewer1.LocalReport.SetParameters(c_botonera);
                ReportViewer1.LocalReport.SetParameters(c_indicadores);
                ReportViewer1.LocalReport.SetParameters(c_iluminacion);
                ReportViewer1.LocalReport.SetParameters(c_puertacabina);
                ReportViewer1.LocalReport.SetParameters(c_ajusteenviaje);
                ReportViewer1.LocalReport.SetParameters(c_ventilador);
                ReportViewer1.LocalReport.SetParameters(c_barrerafotoelec);
                ReportViewer1.LocalReport.SetParameters(c_holguradecab);
                ReportViewer1.LocalReport.SetParameters(c_guias);
                ReportViewer1.LocalReport.SetParameters(c_vidrioespejopaneles);
                ReportViewer1.LocalReport.SetParameters(c_operadordepuertas);
                ReportViewer1.LocalReport.SetParameters(c_contyseriedeseguridad);
                ReportViewer1.LocalReport.SetParameters(c_pasamanos);
                ReportViewer1.LocalReport.SetParameters(c_limpieza);
                ReportViewer1.LocalReport.SetParameters(a_botonera);
                ReportViewer1.LocalReport.SetParameters(a_indicadores);
                ReportViewer1.LocalReport.SetParameters(a_puerta);
                ReportViewer1.LocalReport.SetParameters(a_guiapatines);
                ReportViewer1.LocalReport.SetParameters(a_cerrojos);
                ReportViewer1.LocalReport.SetParameters(a_padeenclavamiento);
                ReportViewer1.LocalReport.SetParameters(a_sensores);
                ReportViewer1.LocalReport.SetParameters(a_peines);
                ReportViewer1.LocalReport.SetParameters(a_peldanosfaldon);
                ReportViewer1.LocalReport.SetParameters(a_demarcaciones);
                ReportViewer1.LocalReport.SetParameters(a_botondeemergencia);
                ReportViewer1.LocalReport.SetParameters(a_contyseriedeseguridad);
                ReportViewer1.LocalReport.SetParameters(a_senales);
                ReportViewer1.LocalReport.SetParameters(a_limpieza);
                ReportViewer1.LocalReport.SetParameters(f_cablesdetraccion);
                ReportViewer1.LocalReport.SetParameters(f_cablelimitador);
                ReportViewer1.LocalReport.SetParameters(f_cableviajero);
                ReportViewer1.LocalReport.SetParameters(f_contrapeso);
                ReportViewer1.LocalReport.SetParameters(f_fdecarrerasuperior);
                ReportViewer1.LocalReport.SetParameters(f_fdecarrerainferior);
                ReportViewer1.LocalReport.SetParameters(f_paracaidas);
                ReportViewer1.LocalReport.SetParameters(f_topespistones);
                ReportViewer1.LocalReport.SetParameters(f_poleatensora);
                ReportViewer1.LocalReport.SetParameters(f_poleas);
                ReportViewer1.LocalReport.SetParameters(f_rieles);
                ReportViewer1.LocalReport.SetParameters(f_aceiteras);
                ReportViewer1.LocalReport.SetParameters(f_stopdefosa);
                ReportViewer1.LocalReport.SetParameters(f_resortes);
                ReportViewer1.LocalReport.SetParameters(f_tensiondecadena);
                ReportViewer1.LocalReport.SetParameters(f_contyseriedeseguridad);
                ReportViewer1.LocalReport.SetParameters(f_mordazas);
                ReportViewer1.LocalReport.SetParameters(f_limpieza);
                ReportViewer1.LocalReport.SetParameters(materialesyrepuesto);
                ReportViewer1.LocalReport.SetParameters(observacion);
                ReportViewer1.LocalReport.SetParameters(i_fusiblecontactos);
                ReportViewer1.LocalReport.SetParameters(i_botoneradepisoencorte);
                ReportViewer1.LocalReport.SetParameters(i_limites);
                ReportViewer1.LocalReport.SetParameters(i_reguladordevelocidad);
                ReportViewer1.LocalReport.SetParameters(i_frenobalataselectroiman);
                ReportViewer1.LocalReport.SetParameters(i_motordetraccion);
                ReportViewer1.LocalReport.SetParameters(i_poleas);
                ReportViewer1.LocalReport.SetParameters(i_filtraciondeaguaensalademaquinas);
                ReportViewer1.LocalReport.SetParameters(i_accesoirregularasalademaquinas);
                ReportViewer1.LocalReport.SetParameters(i_corteensenalizadoropulsadordepiso);
                ReportViewer1.LocalReport.SetParameters(i_ruidooajusteenpuertaspisocabina);
                ReportViewer1.LocalReport.SetParameters(i_iluminaciondecabina);
                ReportViewer1.LocalReport.SetParameters(i_operadordepuertas);
                ReportViewer1.LocalReport.SetParameters(i_motordeoperador);
                ReportViewer1.LocalReport.SetParameters(i_ventiladordecabina);
                ReportViewer1.LocalReport.SetParameters(i_cerrojo);
                ReportViewer1.LocalReport.SetParameters(i_sensordecabinabarrerafotocelula);
                ReportViewer1.LocalReport.SetParameters(i_filtraciondeaguaenhuecoyfoso);
                ReportViewer1.LocalReport.SetParameters(i_bajasocortedetension);
                ReportViewer1.LocalReport.SetParameters(i_sensores);
                ReportViewer1.LocalReport.SetParameters(i_malusoporusuario);
                ReportViewer1.LocalReport.SetParameters(i_iluminacionirregularensalademaquinasyfoso);
                ReportViewer1.LocalReport.SetParameters(i_otros);
                ReportViewer1.LocalReport.SetParameters(tec_nombre);
                ReportViewer1.LocalReport.SetParameters(tec_codigo);
                ReportViewer1.LocalReport.SetParameters(tec_horallegada);
                ReportViewer1.LocalReport.SetParameters(tec_horasalida);
                ReportViewer1.LocalReport.SetParameters(cli_nombre);
                ReportViewer1.LocalReport.SetParameters(cli_ci);
                ReportViewer1.LocalReport.SetParameters(cli_cargo);

                this.ReportViewer1.LocalReport.Refresh();
                this.ReportViewer1.DataBind();
            }else
                Response.Write("<script type='text/javascript'> alert('Error: No hay Datos') </script>");
            
        }

        private void getEncuestasMantenimientoRealizado()
        {
            LocalReport localreport = ReportViewer1.LocalReport;
            localreport.ReportPath = "Reportes/Report_EncuestasMantenimientoRealizadas.rdlc";

            NA_Seguimiento nseg = new NA_Seguimiento();
            DataSet consulta1 = nseg.getAllEncuestaMantenimientoRealizas();
            DataTable DSconsulta = consulta1.Tables[0];
                        
            ReportDataSource DSLibroDiarioCobranzas = new ReportDataSource("DS_EncuestaMantenimientoRealizado", DSconsulta);
                        
            ReportViewer1.LocalReport.DataSources.Add(DSLibroDiarioCobranzas);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();
        }

    }
}