using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DA_FechaExpedicion
    {
        private conexionMySql ConecRes = new conexionMySql();

        public DA_FechaExpedicion() { }

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

        public bool truncarTabla() {
            try
            {
                string consulta = "truncate table tb_expedicion";
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception ) {

                return false;
            }
        
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        public bool insertarFechaExpedicion(string Archivo)
        {
            try
            {
                string consulta = "LOAD DATA LOCAL INFILE '" + Archivo + "' " +
                                  "INTO TABLE tb_expedicion "+
                                  "FIELDS TERMINATED BY ';' "+ 
                                  "LINES TERMINATED BY '\n' "+
                                  "STARTING BY 'XBO' "+
                                  "IGNORE 4 LINES ;";
                ConecRes.ejecutarMySql(consulta);
                return true;
            }
            catch (Exception )
            {

                return false;
            }

        }


    }
}