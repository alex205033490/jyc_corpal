using jycboliviaASP.net.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
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

        public bool update_registros (int codigos, string detalle, string area, string agenteextintor, string marca, float capacidad,
            string codSistema, string estadoextintor, int anioPruebaHidrostatica)
        {
            return datos.ActualizarRegistrosExtintor(codigos, detalle, area, agenteextintor, marca, capacidad,
                codSistema, estadoextintor, anioPruebaHidrostatica);
        }
    }
}