using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;


namespace jycboliviaASP.net.Negocio
{
    public class NMeses
    {
        DMeses dmes = new DMeses();
        public NMeses() { }

        public DataSet getMes(int codMes) {

            string consulta = "select * from tb_mes m where m.codigo = "+codMes;
            return dmes.getDatos(consulta);
        }
    }
}