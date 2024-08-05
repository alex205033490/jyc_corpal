using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jycboliviaASP.net.Negocio;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Configuration;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_Importacion_CredinForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = Session["BaseDatos"].ToString();

            if (tienePermisoDeIngreso(95) == false)
            {
                string ruta = ConfigurationManager.AppSettings["NombreCarpetaContenedora"];
                Response.Redirect(ruta + "/Presentacion/FA_Login.aspx");
            }

            if (!IsPostBack)
            {
                limpiar();
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

        protected void bt_verificar_Click(object sender, EventArgs e)
        {
            cargarDatosdelExbo();
        }

        private void cargarDatosdelExbo()
        {
            string exbo = tx_nroChasis.Text;
            NA_Importacion nimp = new NA_Importacion();
            DataSet tuplas = nimp.get_TodoslosEquiposdelasBasesdedatos(exbo);
            string edificio = tuplas.Tables[0].Rows[0][2].ToString();
            string consignatario = tuplas.Tables[0].Rows[0][3].ToString();
            string Ciudad = tuplas.Tables[0].Rows[0][4].ToString();            
            string valorFobAux = tuplas.Tables[0].Rows[0][7].ToString();

            tx_nombreProyecto.Text = edificio;
            tx_consignatario.Text = consignatario;
            tx_ciudad.Text = Ciudad;
            tx_valorFobOriginal.Text = valorFobAux;

            float tipoCambio;
            float.TryParse(tx_tipocambio.Text.Replace('.', ','), out tipoCambio);
            float fobOriginalEuros;
            float.TryParse(tx_valorFobOriginal.Text.Replace('.', ','), out fobOriginalEuros);
            float fobEurosDolares = fobOriginalEuros * tipoCambio;
            tx_valorFob.Text = fobEurosDolares.ToString();
            //------------------------------------------------------
        }

        [WebMethod]
        [ScriptMethod]
        public static string[] getListaEquipo_Total(string prefixText, int count)
        {
            string exbo = prefixText;
            NA_Importacion nimp = new NA_Importacion();
            DataSet tuplas = nimp.get_TodoslosEquiposdelasBasesdedatos2(exbo);

            string[] lista = new string[tuplas.Tables[0].Rows.Count];
            int fin = tuplas.Tables[0].Rows.Count;
            for (int i = 0; i < fin; i++)
            {
                lista[i] = tuplas.Tables[0].Rows[i][1].ToString();
            }
            return lista;
        }

        protected void bt_calcular_Click(object sender, EventArgs e)
        {
            calcularDatos();
        }

        public void limpiar() {
            tx_valorFob.Text = "0";
            tx_valorFobResultado.Text = "0";
            tx_transpMaritimo.Text = "0";
            tx_transpMaritimoResultado.Text = "0";
            tx_transpTerrestre.Text = "0";
            tx_transpTerrestreResultado.Text = "0";
            tx_nombreProyecto.Text = "";
            tx_nroChasis.Text = "";
            tx_nrodeAplicacion.Text = "";            
            tx_totalValorSeguro.Text = "0";
            tx_nroEquiposContenedor.Text = "0";
            tx_fechaPago.Text = "";
            tx_fechaSolicitud.Text = "";
            tx_baseImponibleParaelSeguro.Text = "0";
            tx_contenedor.Text = "";
            tx_consignatario.Text = "";
            tx_ciudad.Text = "";
            tx_valorFobOriginal.Text = "0";
        
        }

        private void calcularDatos()
        {
            float tipoCambio;
            float.TryParse(tx_tipocambio.Text.Replace('.', ','), out tipoCambio);
            float fobOriginalEuros;
            float.TryParse(tx_valorFobOriginal.Text.Replace('.', ','), out fobOriginalEuros);
            float fobEurosDolares = fobOriginalEuros * tipoCambio;
            tx_valorFob.Text = fobEurosDolares.ToString();
            //------------------------------------------------------

            float porcentaje;
            float.TryParse(tx_valorPorcentaje.Text.Replace('.',',') ,out porcentaje);
            porcentaje = (porcentaje / 100);

            float valorFob;
            float.TryParse(tx_valorFob.Text.Replace('.', ','), out valorFob);

            float valorCFR;
            float.TryParse(tx_valorCFR.Text.Replace('.', ','), out valorCFR);

            float transpMaritimo;
            float.TryParse(tx_transpMaritimo.Text.Replace('.', ','), out transpMaritimo);

            float transpTerrestre;
            float.TryParse(tx_transpTerrestre.Text.Replace('.', ','), out transpTerrestre);

            float nroEquiposContenedor;
            float.TryParse(tx_nroEquiposContenedor.Text.Replace('.', ','), out nroEquiposContenedor);

            //-----------------------
            float valorFobResult = 0;
            float valorCRFResult = 0;
            if (valorFob > 0)
            {
                valorCFR = 0;
                tx_valorCFR.Text = "0";
                tx_valorCFR_resultado.Text = "0";
                valorFobResult = valorFob;
                tx_valorFobResultado.Text = valorFob.ToString("#,###.##");
            }
            else {
                transpMaritimo = 0;
                tx_transpMaritimo.Text = "0";
                tx_transpMaritimoResultado.Text = "0";
                valorCRFResult = valorCFR;
                tx_valorCFR_resultado.Text = valorCFR.ToString("#,###.##");
                tx_valorFobResultado.Text = "0";
                tx_valorFob.Text = "0";
                valorFob = 0;
                valorFobResult = 0;
            }


            float transpMaritimoResultado;
            if (transpMaritimo> 0 )
            {
            transpMaritimoResultado = (transpMaritimo / nroEquiposContenedor);
            tx_transpMaritimoResultado.Text = transpMaritimoResultado.ToString("#,###.##");
            }else{
            transpMaritimoResultado = 0;
            tx_transpMaritimoResultado.Text = "0";
            }

            

            float transpTerrestreResultado = (transpTerrestre / nroEquiposContenedor);
            tx_transpTerrestreResultado.Text = transpTerrestreResultado.ToString("#,###.##");

            float valorMultiplicar = 0;
            float.TryParse("1,05", out valorMultiplicar);
            float baseImponibleSeguro = (valorFobResult + valorCRFResult + transpMaritimoResultado + transpTerrestreResultado) * valorMultiplicar;
            tx_baseImponibleParaelSeguro.Text = baseImponibleSeguro.ToString("#,###.##");

            float totalValorSeguro = (baseImponibleSeguro * porcentaje);
            tx_totalValorSeguro.Text = totalValorSeguro.ToString("#,###.##");


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

        protected void bt_actualizar_Click(object sender, EventArgs e)
        {
            float tipoCambio;
            float.TryParse(tx_tipocambio.Text.Replace('.', ','), out tipoCambio);
            float fobOriginalEuros;
            float.TryParse(tx_valorFobOriginal.Text.Replace('.', ','), out fobOriginalEuros);
            float fobEurosDolares = fobOriginalEuros * tipoCambio;
            tx_valorFob.Text = fobEurosDolares.ToString();
            //------------------------------------------------------

            NA_Importacion nimp = new NA_Importacion();
            string exbo = tx_nroChasis.Text;
            if(nimp.estacargadodatosdelSegurodelExbo(exbo) == false){

               

            float porcentaje;
            float.TryParse(tx_valorPorcentaje.Text.Replace('.', ','), out porcentaje);
            porcentaje = (porcentaje / 100);

            float valorFob;
            float.TryParse(tx_valorFob.Text.Replace('.', ','), out valorFob);

            float valorCFR;
            float.TryParse(tx_valorCFR.Text.Replace('.', ','), out valorCFR);

            float transpMaritimo;
            float.TryParse(tx_transpMaritimo.Text.Replace('.', ','), out transpMaritimo);

            float transpTerrestre;
            float.TryParse(tx_transpTerrestre.Text.Replace('.', ','), out transpTerrestre);

            float nroEquiposContenedor;
            float.TryParse(tx_nroEquiposContenedor.Text.Replace('.', ','), out nroEquiposContenedor);

            //-----------------------
            float valorFobResult = 0;
            float valorCRFResult = 0;
            if (valorFob > 0)
            {
                valorCFR = 0;
                tx_valorCFR.Text = "0";
                tx_valorCFR_resultado.Text = "0";
                valorFobResult = valorFob;
                tx_valorFobResultado.Text = valorFob.ToString("#,###.##");
            }
            else
            {
                transpMaritimo = 0;
                tx_transpMaritimo.Text = "0";
                tx_transpMaritimoResultado.Text = "0";
                valorCRFResult = valorCFR;
                tx_valorCFR_resultado.Text = valorCFR.ToString("#,###.##");
                tx_valorFobResultado.Text = "0";
                tx_valorFob.Text = "0";
                valorFob = 0;
                valorFobResult = 0;
            }


            float transpMaritimoResultado;
            if (transpMaritimo > 0)
            {
                transpMaritimoResultado = (transpMaritimo / nroEquiposContenedor);
                tx_transpMaritimoResultado.Text = transpMaritimoResultado.ToString("#,###.##");
            }
            else
            {
                transpMaritimoResultado = 0;
                tx_transpMaritimoResultado.Text = "0";
            }

            float transpTerrestreResultado = (transpTerrestre / nroEquiposContenedor);
            tx_transpTerrestreResultado.Text = transpTerrestreResultado.ToString("#,###.##");

            float valorMultiplicar = 0;
            float.TryParse("1,05", out valorMultiplicar);
            float baseImponibleSeguro = (valorFobResult + valorCRFResult + transpMaritimoResultado + transpTerrestreResultado) * valorMultiplicar;
            tx_baseImponibleParaelSeguro.Text = baseImponibleSeguro.ToString("#,###.##");

            float totalValorSeguro = (baseImponibleSeguro * porcentaje);
            tx_totalValorSeguro.Text = totalValorSeguro.ToString("#,###.##");

            
            string edificio = tx_nombreProyecto.Text;
            string consignatario = tx_consignatario.Text;
            string Ciudad = tx_ciudad.Text;
            string nroAplicacion = tx_nrodeAplicacion.Text;            
            string contenedor = tx_contenedor.Text;
            string fechaPago = convertidorFecha(tx_fechaPago.Text);
            string fechaSolicitud = convertidorFecha(tx_fechaSolicitud.Text);

            DataSet tuplas = nimp.get_TodoslosEquiposdelasBasesdedatos(exbo);
            int Cod_Equipo;
            int.TryParse(tuplas.Tables[0].Rows[0][0].ToString(), out Cod_Equipo);
            string DB = tuplas.Tables[0].Rows[0][5].ToString();
            string variableSimec = tuplas.Tables[0].Rows[0][6].ToString();            
                                    
            bool bandera = nimp.actualizarDatosCredinForm(exbo, edificio, consignatario, Ciudad, nroAplicacion, contenedor,
                fechaPago, fechaSolicitud, porcentaje, valorFob, valorFobResult, valorCFR, valorCRFResult, 
                transpMaritimo, transpMaritimoResultado, transpTerrestre, transpTerrestreResultado, nroEquiposContenedor,
                baseImponibleSeguro, totalValorSeguro , DB, variableSimec, Cod_Equipo);
            if(bandera == true){
                limpiar();
                Response.Write("<script type='text/javascript'> alert('Guardado: OK') </script>");
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Guardado') </script>");
            
            }else
                Response.Write("<script type='text/javascript'> alert('Error: Chasis ya Existe') </script>");
        }

        protected void bt_limpiza_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}