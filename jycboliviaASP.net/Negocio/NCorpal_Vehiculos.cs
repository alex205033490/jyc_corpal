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

        internal bool anular_registro(List<int> codigo)
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
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener datos. " + ex.Message);
            }
        }

        internal DataSet get_showRutasVehiculosDespachos(int codCar)
        {
            try
            {
                return datos.get_showRutasVehiculosDespachos(codCar);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener datos. " + ex.Message);
            }
        }

        internal int post_NewRegistroRutaEntrega_Asignacion(int codCar, string car)
        {
            try
            {
                return datos.post_NewRegistroRutaEntrega_Asignacion(codCar, car);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado registrar la ruta. " + ex.Message);
            }
        }

        internal bool post_NewRegistroRutaEntregaPuntos_Asignacion(int orden, int codRuta, int codCliente, string cliente,
                                            string lat, string lng)
        {
            try
            {
                return datos.post_NewRegistroRutaEntregaPuntos_Asignacion(orden, codRuta, codCliente, cliente,
                                                                            lat, lng);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al registrar los puntos. " + ex.Message);
            }

        }

        internal bool update_ordenRutaEntrega_asignacion(int codCar, int orden, int codCli)
        {
            try
            {
                return datos.update_ordenRutaEntrega_asignacion(codCar, orden, codCli);
            }
            catch(Exception ex)
            {
                throw new Exception("Error inesperado al actualizar los datos. " + ex.Message);
            }
        }


        }
}