using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Negocio;

namespace jycboliviaASP.net.Datos
{
    public class DA_Equipo
    {
        private conexionMySql ConecRes = new conexionMySql();
        public DA_Equipo() { }

        public bool insertar(string nombre, string detalle, int estado)
        {
                return false;
        }

        public bool modificar()
        {
            return false;
        }

        public bool eliminar()
        {
            return false;
        }

        public DataSet getDatos(string consulta)
        {
            DataSet datosR = ConecRes.consultaMySql(consulta);
            return datosR;
        }

        public bool modificarSeguimientoInstalacion(int codEquipo, int codSeguimientoInstalacion,string fechaEquipoObra)
        {
            string consulta = "update tb_equipo set tb_equipo.codseginstalacion = " + codSeguimientoInstalacion + ", tb_equipo.fecha_equipo_obra = "+fechaEquipoObra+"  where tb_equipo.codigo = " + codEquipo;
               return  ConecRes.ejecutarMySql(consulta);                
        }

        public bool modificarSeguimientoInstalacionFechaEquipoObra(int codEquipo, string fechaEquipoObra)
        {
            string consulta = "update tb_equipo set  tb_equipo.fecha_equipo_obra = " + fechaEquipoObra + "  where tb_equipo.codigo = " + codEquipo;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool actualizarMantenimientoGratuito(int codEquipo, string fechaInicio, int meses, string fechaFin)
        {
            string consulta = "update tb_equipo set tb_equipo.mantenimientogratuito_inicio = " +fechaInicio+ 
                              ", tb_equipo.mantenimientogratuito_fin = "+fechaFin+", tb_equipo.mesesgratuitos =  "+meses+ 
                              " where tb_equipo.codigo = " + codEquipo;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool actualizarFechaContrato(int codEquipo, int codFechaContrato) {
            string consulta = "update tb_equipo set tb_equipo.codfechacontratofirmado = " + codFechaContrato +                                 
                                 " where tb_equipo.codigo = " + codEquipo;
            return ConecRes.ejecutarMySql(consulta);
        }

        public bool modificarCronogramaTecnico(int codEquipo, int paradas, string pasajero, string modelo,string velocidad,string fechaCronograma, string fechaFase2)
        {

            string consulta = "update tb_equipo set "+
                               " tb_equipo.fechafase1 = " + fechaCronograma +", "+ 
                               " tb_equipo.fechafase2 = "+fechaFase2+", "+
                               " tb_equipo.parada = "+paradas+", "+
                               " tb_equipo.pasajero = '"+pasajero+"', "+
                               " tb_equipo.modelo = '"+modelo+"', "+
                               " tb_equipo.velocidad = '"+velocidad+"' "+
                               " where tb_equipo.codigo = " + codEquipo;
            return ConecRes.ejecutarMySql(consulta);

        }

        public bool actualizarLetraID_equipo(int codigoEquipo, string LetraID)
        {
            string consulta = "UPDATE tb_equipo SET tb_equipo.ascensor = '"+LetraID+"' WHERE  tb_equipo.codigo = "+codigoEquipo;
            return ConecRes.ejecutarMySql(consulta);
        }

    }
}