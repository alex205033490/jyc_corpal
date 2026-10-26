using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace jycboliviaASP.net.Negocio
{

    public class TipoCambio
    {
        public decimal? compra { get; set; }
        public decimal? venta { get; set; }
    }
    public class DolarPY
    {
        public TipoCambio bcp { get; set; }
    }
    public class TipoCambioPY
    {
        public DolarPY dolarpy { get; set; }
        public string updated { get; set; }
    }



    public class NA_TipoCambioService
    {

        private string DescargarJson(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("User-Agent", "Mozilla/5.0");
                client.Encoding = System.Text.Encoding.UTF8;
                return client.DownloadString(url);
            }
        }

        private TipoCambio DeserializarJson(string json)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<TipoCambio>(json);
        }
        private TipoCambioPY DeserializarJsonPY(string json)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<TipoCambioPY>(json);
        }


        public TipoCambio ObtenerTipoCambioOficial()
        {
            string url = "https://bo.dolarapi.com/v1/dolares/oficial";
            string json = DescargarJson(url);
            return DeserializarJson(json);
        }

        public TipoCambio ObtenerTipoCambioBlue()
        {
            string url = "https://bo.dolarapi.com/v1/dolares/binance";
            string json = DescargarJson(url);
            return DeserializarJson(json);
        }

        public TipoCambioPY ObtenerTipoCambioOficialPY()
        {
            string url = "https://dolar.melizeche.com/api/1.0/";
            string json = DescargarJson(url);
            return DeserializarJsonPY(json);
        }

    }
}