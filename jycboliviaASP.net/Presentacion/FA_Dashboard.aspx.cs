using jycboliviaASP.net.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Presentacion
{
    public partial class FA_Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                montoTotalObjVentas();
                montoTotalOrdenProduccion();
                montoTotalObjSalidasAlmacen();
            }            
        }

        private void montoTotalObjSalidasAlmacen()
        {
            NCorpal_Objetivos nobj = new NCorpal_Objetivos();
            float montoTotalsalidasAlmacen = nobj.montototalAnualSalidasAlmacen();
            lb_totalSalidaAlmacen.Text = montoTotalsalidasAlmacen.ToString();
        }

        private void montoTotalOrdenProduccion()
        {
            NCorpal_Objetivos nobj = new NCorpal_Objetivos();
            float montototalAnualOrdenProduccion = nobj.montototalAnualOrdenProduccion();
            lb_totalOrdenProduccion.Text = montototalAnualOrdenProduccion.ToString();
        }

        private void montoTotalObjVentas()
        {
           NCorpal_Objetivos nobj = new NCorpal_Objetivos();
            float montoTotalObjetivo = nobj.montototalAnualObjetivosVentasProgramado();
            lb_objVentasAnual.Text= montoTotalObjetivo.ToString();
        }

        // Método WebMethod que devuelve datos en JSON
        [WebMethod]
        public static string GetChartData()
        {
            int anioActual = DateTime.Now.Year;
            NCorpal_Objetivos nobj = new NCorpal_Objetivos();
            DataSet montoTotalObjetivo = nobj.get_ListaObjetivoProduccionyVentasAnual(anioActual);
           
            // Definir los 12 meses del año
            string[] allMonths = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            // Datos de ejemplo con valores en algunos meses y ceros en los demás
           // int[] salesData = { 50, 60, 70, 0, 0, 90, 0, 85, 0, 100, 120, 0 };
            int[] salesData = new int[12];
            int[] revenueData = { 20, 30, 40, 0, 0, 45, 0, 50, 0, 60, 70, 0 };
            int[] customersData = { 10, 20, 30, 0, 0, 35, 0, 40, 0, 50, 60, 0 };
            int j = 0;
            for (int i=0; i< 12; i++) {
                int aux;
                int.TryParse(montoTotalObjetivo.Tables[0].Rows[0][j].ToString(), out aux);
                salesData[i] = aux;
                j=i+3;
            }


            // Crear el objeto con los datos
            var data = new
            {
                Categories = allMonths, // Se incluyen los 12 meses
                Sales = salesData,
                Revenue = revenueData,
                Customers = customersData
            };

            // Serializar el objeto a JSON y devolverlo
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(data);
        }

    }
}