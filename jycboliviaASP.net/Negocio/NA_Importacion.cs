using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jycboliviaASP.net.Datos;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NA_Importacion
    {
        DA_Importacion dimp = new DA_Importacion();

        public NA_Importacion() { }

        public DataSet get_datoImportacionContenedorTotal(bool todosLosDatos, string Edificio, string exbo, string semanaExp, string Dui, string Contenedor)
        {
            return dimp.get_datoImportacionContenedorTotal( todosLosDatos,  Edificio,  exbo,  semanaExp ,  Dui,  Contenedor);
        }

        public float get_PesoTotalContenedor(string Contenedor)
        {
            float dato;
            DataSet tupla = dimp.get_PesoTotalContenedor(Contenedor);
            if (tupla.Tables[0].Rows.Count > 0)
            {
            float.TryParse(tupla.Tables[0].Rows[0][0].ToString(), out dato);
            return dato;        
            }else
                return 0;
        }

        public float get_TotalGirosContenedor(string Contenedor)
        {
            float dato;
            DataSet tupla = dimp.get_TotalGirosContenedor(Contenedor);
            if(tupla.Tables[0].Rows.Count > 0){
            float.TryParse(tupla.Tables[0].Rows[0][0].ToString(), out dato);
            return dato;
            }else
                return 0;
        }


        internal bool existeContenedor(string nroContenedor)
        {
            DataSet dato = dimp.existeContenedor(nroContenedor);
            if (dato.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        internal DataSet getFacturasContenedor(string nroContenedor)
        {
            return dimp.getFacturasContenedor(nroContenedor);
        }

        internal bool actualizarDatoscontenedor(string nroContenedor, string fechaASP, string nroASPB, float montoASPB, string fechaMSC, string nroMSC, float montoMSC, string fechaTHC, string nroTHC, float montoTHC)
        {
            return dimp.actualizarDatoscontenedor( nroContenedor,  fechaASP,  nroASPB,  montoASPB,  fechaMSC,  nroMSC,  montoMSC,  fechaTHC,  nroTHC,  montoTHC);
        }

        internal bool ingresarNuevoContendor(string codnrocontenedor, string fechaASP, string nroASPB, float montoASPB, string fechaMSC, string nroMSC, float montoMSC, string fechaTHC, string nroTHC, float montoTHC)
        {
            return dimp.ingresarNuevoContendor( codnrocontenedor, fechaASP,  nroASPB,  montoASPB,  fechaMSC,  nroMSC,  montoMSC,  fechaTHC,  nroTHC,  montoTHC);
        }

        internal DataSet getTodoslosContenedores(string contenedor)
        {
            return dimp.getTodoslosContenedores( contenedor);
        }

        public DataSet get_FacturaContenedorGeneral(bool todosLosDatos, string Edificio, string exbo, string semanaExp, string Dui, string Contenedor)
        { 
            return dimp.get_FacturaContenedorGeneral( todosLosDatos,  Edificio,  exbo,  semanaExp,  Dui,  Contenedor);
        }


        internal DataSet get_TodoslosEquiposdelasBasesdedatos(string exbo)
        {
            DataSet tuplas = dimp.get_TodoslosEquiposdelasBasesdedatos(exbo);
            return tuplas;
        }

        internal DataSet get_TodoslosEquiposdelasBasesdedatos2(string exbo)
        {
            DataSet tuplas = dimp.get_TodoslosEquiposdelasBasesdedatos2(exbo);
            return tuplas;
        }


        internal bool actualizarDatosCredinForm(string exbo, string edificio, string consignatario, string Ciudad, string nroAplicacion,  string contenedor, string fechaPago, string fechaSolicitud, float porcentaje, float valorFob, float valorFobResult, float valorCFR, float valorCRFResult, float transpMaritimo, float transpMaritimoResultado, float transpTerrestre, float transpTerrestreResultado, float nroEquiposContenedor, float baseImponibleSeguro, float totalValorSeguro,string BasedeDatos, string variablesimec, int cod_Equipo)
        {
            bool bandera = dimp.actualizarDatosCredinForm( exbo,  edificio,  consignatario,  Ciudad,  nroAplicacion,  contenedor,  fechaPago,  fechaSolicitud,  porcentaje,  valorFob,  valorFobResult,  valorCFR,  valorCRFResult,  transpMaritimo,  transpMaritimoResultado,  transpTerrestre,  transpTerrestreResultado,  nroEquiposContenedor,  baseImponibleSeguro,  totalValorSeguro, BasedeDatos, variablesimec, cod_Equipo);
            return bandera;
        }

        internal DataSet get_SegurosCredinForm(string fechadesde, string fechahasta)
        {
            DataSet filas = dimp.get_SegurosCredinForm(fechadesde, fechahasta);
            return filas;
        }

        internal DataSet get_SegurosCredinForm2(string fechadesde, string fechahasta)
        {
            DataSet filas = dimp.get_SegurosCredinForm2(fechadesde, fechahasta);
            return filas;
        }

        internal bool estacargadodatosdelSegurodelExbo(string exbo)
        {
            DataSet filas = dimp.estacargadodatosdelSegurodelExbo(exbo);
            if (filas.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}