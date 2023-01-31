using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Negocio;
using System.Data;

namespace jycboliviaASP.net.Datos
{
    public class DA_CuadrosXXX
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_CuadrosXXX() { }

        public bool insertar()
        {

            try
            {
                string consulta = "";
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar()
        {
            return false;
        }

        public bool truncarTabla()
        {
            try
            {
                string consulta = "truncate table tb_seguimiento_aux";
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        public bool insertarCuadrosXXX(string Archivo)
        {
            try
            {
                string consulta = "LOAD DATA LOCAL INFILE '" + Archivo + "' " +
                                  "INTO TABLE tb_seguimiento_aux " +
                                  "FIELDS TERMINATED BY ';' " +
                                  "LINES TERMINATED BY '\n' " +
                                  "IGNORE 22 LINES ;";
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

    }
}