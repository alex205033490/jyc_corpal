using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;
using MySql.Data.MySqlClient;

namespace jycboliviaASP.net.Datos.Datos_old
{
    public class DCorpal_Clientes
    {
        private conexionMySql conexion = new conexionMySql();
        internal bool insert_vaciadoCliente(int CodigoContacto, string NombreCompleto, string NumeroDocumentoIdentidad, string correo, string telefono)
        {
            string consulta = "INSERT INTO tbcorpal_cliente(fechagra, horagra, cod_clienteupon, propietarioname, propietarioci, propietariocorreo, propietariocelular) " +
                "SELECT current_date(), current_time(), " + CodigoContacto + ", '" + NombreCompleto + "', '" + NumeroDocumentoIdentidad + "', '" + correo + "', '" + telefono + "' " +
                " from dual " +
                " where not exists (select 1 from tbcorpal_cliente where cod_clienteupon = " + CodigoContacto + ")";

                return conexion.ejecutarMySql(consulta);
        }
    }
}