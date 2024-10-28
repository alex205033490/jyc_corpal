using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Datos;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Net;

namespace jycboliviaASP.net.Negocio
{
    public class NA_CorpalAddInsumoCreado
    {
        DACorpal_InsumoCreado DatosIC = new DACorpal_InsumoCreado();

        //  TEXTBOX INSUMOS AUTOCOMPLETAR
        public DataSet mostrarTodos_AutoComplitInsumo(string nombreInsumo)
        {
            string consulta = "select ins.Medida, ins.codigo, ins.nombre from tbcorpal_insumo ins where ins.estado = 1 " +
                                " and ins.nombre like '%" + nombreInsumo + "%'";
            DataSet datosI = DatosIC.getDatos(consulta);
            return datosI;
        }
      
        //  INSERTAR INSUMOCREADO tbcorpal_insumoscreados, tbcorpal_detinsumocreado (2 tablas)    
        public bool InsertarInsumoYDetalles(string nombre, string medida, GridView gv_insumoCreado)
        {
            try
            {
                int codinsumocreado = DatosIC.insertarInsumoCreado(nombre, medida);

                if (codinsumocreado > 0 )
                {
                    foreach (GridViewRow row in gv_insumoCreado.Rows)
                    {
                        int codInsumo = int.Parse(row.Cells[0].Text);
                         
                        string cantidad = (row.Cells[2].Text);
                        string medidaDetalle = row.Cells[3].Text;

                        bool resultado = DatosIC.insertarDetInsumoCreado(codInsumo, codinsumocreado, cantidad, medidaDetalle);

                        if (!resultado)
                        {
                            Console.WriteLine("Error al insertar detalle: " + codinsumocreado);
                        }
                    }           
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en InsertarInsumoYDetalles: " + ex.Message);
                return false;
            }   
        }
        // INSERTAR NUEVO INSUMO EN INSUMOCREADO EXISTENTE
        public bool InsertarInsumoNuevoIC(int codinsumo, int codinsumocreado, string cantidad, string medida)
        {
            try
            {
                DatosIC.insertarDetInsumoCreado(codinsumo, codinsumocreado, cantidad, medida);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Insertar nuevo Insumo: " + ex.Message);
                return false;
            }
        }


        // SELECT INSUMO CREADO
        public DataSet mostrarInsumoCreado_Autocomplit(string insumoCreado)
        {
            string consulta = "SELECT ic.`nombre` FROM tbcorpal_insumoscreados as ic WHERE ic.nombre like '%" + insumoCreado + "%'";

            DataSet datosI = DatosIC.getDatos(consulta);
            return datosI;
        }
        // SELECT MOSTRAR LISTA INSUMOS DE UN INSUMOCREADO
        public DataSet mostrarDetInsumoCreado(int codigo)
        {
            string consulta = "SELECT ins.codigo, ins.`nombre`, dic.cantidad, dic.medida " +
                "FROM tbcorpal_detinsumocreado as dic INNER JOIN tbcorpal_insumo as ins " +
                "ON dic.`codinsumo` = ins.`codigo` WHERE dic.codinsumocreado = "+ codigo +";";

            DataSet datosI = DatosIC.getDatos(consulta);
            return datosI;
        }

        // SELECT InsumoCreado, DetInsumoCreado, Insumo
        public DataSet MostrarDetInsumoCreado(string InsumoCreado)
        {
            string consulta = "SELECT ic.codigo as 'CodigoIC', ic.nombre as 'NombreIC', ic.medida as 'MedidaIC',ins.`codigo` ,ins.`nombre`, dic.`cantidad`, dic.`medida`" +
                "FROM tbcorpal_insumoscreados as ic " +
                "INNER JOIN tbcorpal_detinsumocreado as dic ON ic.codigo = dic.codinsumocreado " +
                "INNER JOIN tbcorpal_insumo as ins ON dic.`codinsumo` = ins.`codigo` " +
                "WHERE ic.`nombre` = '"+InsumoCreado+"'";

            DataSet datosI = DatosIC.getDatos(consulta);
            return datosI;
        }

        // MODIFICAR DetInsumoCreado
        public bool ModificarDetInsumoCreado(int codInsumoCreado, string cantidadInsumo, int codInsumo)
        {
            

            return DatosIC.ModificarDetInsumoCreado(codInsumoCreado, cantidadInsumo, codInsumo);
        }


    }
}
