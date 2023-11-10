using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_ActivosJyC
    {
        DA_ActivosJyC dactivos = new DA_ActivosJyC();
        public NA_ActivosJyC() { }

        public bool update_Activos(int codigoActivo, int codUserRespAsignado, int codUser, int CodestadoActivo, bool noAplica, float montoValornoAplica, float tipocambio, string ubicacion_une, bool bajaactivo, string observaciones)
        {
            bool bandera = dactivos.update_Activos(codigoActivo, codUserRespAsignado, codUser, CodestadoActivo, noAplica, montoValornoAplica, tipocambio, ubicacion_une, bajaactivo, observaciones);
            return bandera;
        }

        public DataSet get_allActivos(string detalleActivo, string comprobante, string NombreCustodio, string responsableAsignado, string estadoActivo, string EstadoValorActual)
        {
            DataSet tuplas = dactivos.get_allActivos(detalleActivo, comprobante, NombreCustodio, responsableAsignado, estadoActivo, EstadoValorActual);
            return tuplas;
        }

        internal DataSet get_estadosActivos()
        {
            DataSet tuplas = dactivos.get_estadosActivos();
            return tuplas;
        }

        internal int get_codigoEstadoActivo(string estadoActivo)
        {
            DataSet tuplas = dactivos.get_EstadoActivo(estadoActivo);
            if (tuplas.Tables[0].Rows.Count > 0)
            {
                int codigo;
                int.TryParse(tuplas.Tables[0].Rows[0][0].ToString(), out codigo);
                return codigo;
            }
            else
                return -1;
        }

        internal DataSet get_estadosValorActual()
        {
            return dactivos.get_estadosValorActual();
        }

        internal DataSet mostrarResponsableSimec(string nombreResponsable)
        {
            return dactivos.mostrarResponsableSimec(nombreResponsable);
        }
    }
}