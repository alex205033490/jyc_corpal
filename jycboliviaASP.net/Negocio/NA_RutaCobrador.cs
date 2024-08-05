using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_RutaCobrador
    {
        private DA_RutaCobrador drutac = new DA_RutaCobrador();
        public NA_RutaCobrador() { }

        public bool insertarRutaCobrador(int codEquipo,string edificio, string exbo, string cobrador, string fechacobro, string horacobro, string detalle, float montocobrar, string estadoCobro, int coduserInicio)
        {
            NProyecto nproy = new NProyecto();
            int codEdificio = 0;
            int.TryParse(nproy.getCodigoProyect(edificio).ToString(), out codEdificio);
            
            NA_Responsables res = new NA_Responsables();
            int codcobrador = 0;
            int.TryParse(res.getCodigo_NombreResponsable(cobrador).ToString(), out codcobrador);
                
            DataSet tupla = nproy.getProyect(codEdificio);
            int codEncargado = 0;
            int.TryParse(tupla.Tables[0].Rows[0][5].ToString(), out codEncargado);
            

            return drutac.insertarRutaCobrador(codEquipo, edificio, exbo, codcobrador, codEncargado, cobrador, fechacobro, horacobro, detalle, montocobrar, estadoCobro, coduserInicio);
        }

        public bool eliminar(int codRutaCobro)
        {
            return drutac.eliminar(codRutaCobro);
        }

        public bool modificarRutaCobro(int codRutaCobro, int codEquipo, string edificio, string exbo, int codcobrador, string cobrador, string fechacobro, string horacobro, float montocobrar)
        {
            return false;
        }

        internal DataSet getrutasAsignadas(string edificio, string cobradornombre)
        {
          return drutac.getrutasAsignadas(edificio, cobradornombre);
        }
    }
}