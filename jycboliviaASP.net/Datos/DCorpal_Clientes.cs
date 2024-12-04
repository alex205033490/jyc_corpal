using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos.Datos_old
{
    public class DCorpal_Clientes
    {
        private conexionMySql conexion = new conexionMySql();
        internal bool insert_vaciadoCliente(int CodigoContacto, string NombreCompleto, string NumeroDocumentoIdentidad, string correo, string telefono)
        {
            string consulta = "insert fechagra, horagra, cod_clienteupon, propietarioname, propietarioci, propietariocorreo, propietariocelular " +
                "values (" +
                " current_date(), current_time(), " +CodigoContacto+ ", '" +NombreCompleto+ "', '" +NumeroDocumentoIdentidad+"', '" +correo+ "', '" +telefono+ "')";
            return conexion.ejecutarMySql(consulta);
        }

    }
}