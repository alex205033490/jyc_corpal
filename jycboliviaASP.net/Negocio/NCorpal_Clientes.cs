using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Datos;
using jycboliviaASP.net.Datos.Datos_old;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_Clientes
    {
        DCorpal_Clientes dcc = new DCorpal_Clientes();
        internal bool insert_vaciadocliente(int CodigoContacto, string NombreCompleto, string NumeroDocumentoIdentidad, string correo, string telefono)
        {
            return dcc.insert_vaciadoCliente(CodigoContacto, NombreCompleto, NumeroDocumentoIdentidad, correo, telefono);
        }
    }
}