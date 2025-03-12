using jycboliviaASP.net.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_Objetivos
    {
        DCorpal_Objetivos dobjetivo = new DCorpal_Objetivos();
        public NCorpal_Objetivos() { }

        internal DataSet get_ListaObjetivoProduccionyVentasAnual(int gestion)
        {
            return dobjetivo.get_ListaObjetivoProduccionyVentasAnual( gestion);
        }

        internal DataSet get_montosTotalObjetivoProduccionyVentasAnual()
        {
            DataSet dato = dobjetivo.montototalAnualObjetivosVentasProgramado();
            return dato;
        }

            internal float montototalAnualObjetivosVentasProgramado()
        {
            DataSet dato = dobjetivo.montototalAnualObjetivosVentasProgramado();
            if (dato.Tables[0].Rows.Count>0) {
                float enero;
                float.TryParse(dato.Tables[0].Rows[0][0].ToString(), out enero);
                float febrero;
                float.TryParse(dato.Tables[0].Rows[0][3].ToString(), out febrero);
                float marzo;
                float.TryParse(dato.Tables[0].Rows[0][6].ToString(), out marzo);
                float abril;
                float.TryParse(dato.Tables[0].Rows[0][9].ToString(), out abril);
                float mayo;
                float.TryParse(dato.Tables[0].Rows[0][12].ToString(), out mayo);
                float junio;
                float.TryParse(dato.Tables[0].Rows[0][15].ToString(), out junio);
                float julio;
                float.TryParse(dato.Tables[0].Rows[0][18].ToString(), out julio);
                float agosto;
                float.TryParse(dato.Tables[0].Rows[0][21].ToString(), out agosto);
                float septiembre;
                float.TryParse(dato.Tables[0].Rows[0][24].ToString(), out septiembre);
                float octubre;
                float.TryParse(dato.Tables[0].Rows[0][27].ToString(), out octubre);
                float noviembre;
                float.TryParse(dato.Tables[0].Rows[0][30].ToString(), out noviembre);
                float diciembre;
                float.TryParse(dato.Tables[0].Rows[0][33].ToString(), out diciembre);

                float monto = enero + febrero + marzo + abril + mayo + junio + julio + agosto + septiembre + octubre + noviembre + diciembre;
                return monto;
            }else
                return 0;
        }

        internal float montototalAnualOrdenProduccion()
        {
            DataSet dato = dobjetivo.montototalAnualObjetivosVentasProgramado();
            if (dato.Tables[0].Rows.Count > 0)
            {
                float enero;
                float.TryParse(dato.Tables[0].Rows[0][1].ToString(), out enero);
                float febrero;
                float.TryParse(dato.Tables[0].Rows[0][4].ToString(), out febrero);
                float marzo;
                float.TryParse(dato.Tables[0].Rows[0][7].ToString(), out marzo);
                float abril;
                float.TryParse(dato.Tables[0].Rows[0][10].ToString(), out abril);
                float mayo;
                float.TryParse(dato.Tables[0].Rows[0][13].ToString(), out mayo);
                float junio;
                float.TryParse(dato.Tables[0].Rows[0][16].ToString(), out junio);
                float julio;
                float.TryParse(dato.Tables[0].Rows[0][19].ToString(), out julio);
                float agosto;
                float.TryParse(dato.Tables[0].Rows[0][22].ToString(), out agosto);
                float septiembre;
                float.TryParse(dato.Tables[0].Rows[0][25].ToString(), out septiembre);
                float octubre;
                float.TryParse(dato.Tables[0].Rows[0][28].ToString(), out octubre);
                float noviembre;
                float.TryParse(dato.Tables[0].Rows[0][31].ToString(), out noviembre);
                float diciembre;
                float.TryParse(dato.Tables[0].Rows[0][34].ToString(), out diciembre);

                float monto = enero + febrero + marzo + abril + mayo + junio + julio + agosto + septiembre + octubre + noviembre + diciembre;
                return monto;
            }
            else
                return 0;
        }

        internal float montototalAnualSalidasAlmacen()
        {
            DataSet dato = dobjetivo.montototalAnualObjetivosVentasProgramado();
            if (dato.Tables[0].Rows.Count > 0)
            {
                float enero;
                float.TryParse(dato.Tables[0].Rows[0][2].ToString(), out enero);
                float febrero;
                float.TryParse(dato.Tables[0].Rows[0][5].ToString(), out febrero);
                float marzo;
                float.TryParse(dato.Tables[0].Rows[0][8].ToString(), out marzo);
                float abril;
                float.TryParse(dato.Tables[0].Rows[0][11].ToString(), out abril);
                float mayo;
                float.TryParse(dato.Tables[0].Rows[0][14].ToString(), out mayo);
                float junio;
                float.TryParse(dato.Tables[0].Rows[0][17].ToString(), out junio);
                float julio;
                float.TryParse(dato.Tables[0].Rows[0][20].ToString(), out julio);
                float agosto;
                float.TryParse(dato.Tables[0].Rows[0][23].ToString(), out agosto);
                float septiembre;
                float.TryParse(dato.Tables[0].Rows[0][26].ToString(), out septiembre);
                float octubre;
                float.TryParse(dato.Tables[0].Rows[0][29].ToString(), out octubre);
                float noviembre;
                float.TryParse(dato.Tables[0].Rows[0][32].ToString(), out noviembre);
                float diciembre;
                float.TryParse(dato.Tables[0].Rows[0][35].ToString(), out diciembre);

                float monto = enero + febrero + marzo + abril + mayo + junio + julio + agosto + septiembre + octubre + noviembre + diciembre;
                return monto;
            }
            else
                return 0;
        }
    }
}