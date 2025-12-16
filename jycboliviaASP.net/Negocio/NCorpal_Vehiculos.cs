using jycboliviaASP.net.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_Vehiculos
    {
        DCorpal_Vehiculo datos = new DCorpal_Vehiculo();

        public DataSet get_mostrarVehiculosGV()
        {
            return datos.get_mostrarVehiculosGV();
        }

        internal bool post_registrarVehiculo(string placa, string marca, string modelo, string detalle, string conductor,
            decimal capacidad, int cargacajas)
        {
            return datos.post_registrarVehiculo(placa, marca, modelo, detalle, conductor, capacidad, cargacajas);

        }

        internal bool anular_registro (List<int> codigo)
        {
            return datos.anular_RegistroVehiculo(codigo);
        }

        public bool post_updateRegistroVehiculo(int codigo, string placa, string marca, string modelo, string detalle, string conductor,
            float capacidad, int cargacajas)
        {
            return datos.post_updateRegistroVehiculos(codigo, placa, marca, modelo, detalle, conductor,
             capacidad, cargacajas);
        }

        internal DataSet get_showVehiculoDD()
        {
            try
            {
                return datos.get_showVehiculoDD();
            } catch(Exception ex)
            {
                throw new Exception("Error inesperado al obtener datos. " + ex.Message);
            }
        }
    }
}