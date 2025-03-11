using jycboliviaASP.net.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_Extintor
    {
        DCorpal_Extintor datos = new DCorpal_Extintor(); 
        internal bool Insert_RegistroExtintor(string detalle, string area, string agenteextintor, string marca, float capacidad,
            string codSistema, string fechaCarga, string fechaProximaCarga, string estadoExtintor, int añoProximaPrueba, int codres, string nombreRes)
        {
            return datos.insert_RegistroExtintor(detalle, area, agenteextintor, marca, capacidad,
             codSistema, fechaCarga, fechaProximaCarga, estadoExtintor, añoProximaPrueba, codres, nombreRes);
        }

        internal DataSet mostrar_registrosExtintores(string area)
        {
            return datos.mostrar_registrosExtintores(area);
        }

        public bool anular_Registro(List<int> codigo)
        {
            return datos.anular_RegistroExtintor(codigo);
        }

        public bool Actualizar_RegistrosSeleccionados(List<int> codigosSeleccionados, Dictionary<int, Dictionary<string, object>> parametrosPorCodigo)
        {
            if (codigosSeleccionados == null || codigosSeleccionados.Count == 0)
            {
                throw new ArgumentException("No se han seleccionado registros para actualizar.");
            }
            if(parametrosPorCodigo == null || parametrosPorCodigo.Count == 0)
            {
                throw new ArgumentException("No se han proporcionado parametros de actualizacion.");
            }
            bool resultado = datos.update_RegistrosExtintor(codigosSeleccionados, parametrosPorCodigo);

            return resultado;
        }
    }
}