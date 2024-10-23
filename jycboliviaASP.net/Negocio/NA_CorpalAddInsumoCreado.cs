using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using jycboliviaASP.net.Datos;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace jycboliviaASP.net.Negocio
{
    public class NA_CorpalAddInsumoCreado
    {
        DACorpal_InsumoCreado insumC = new DACorpal_InsumoCreado();

        // textbox Insumos autocompletar
        public DataSet mostrarTodos_AutoComplitInsumo(string nombreInsumo)
        {
            string consulta = "select ins.Medida, ins.codigo, ins.nombre from tbcorpal_insumo ins where ins.estado = 1 " +
                                " and ins.nombre like '%" + nombreInsumo + "%'";
            DataSet datosI = insumC.getDatos(consulta);
            return datosI;
        }

        // INSERT tbcorpal_insumocreado
        public int insertarInsumoCreado(string nombre, string medida)
        {
            return insumC.insertarInsumoCreado(nombre, medida);

        }
            
        public bool InsertarInsumoYDetalles(string nombre, string medida, GridView gv_insumoCreado)
        {
            try
            {
                int codinsumocreado = insumC.insertarInsumoCreado(nombre, medida);

                if (codinsumocreado > 0 )
                {
                    foreach (GridViewRow row in gv_insumoCreado.Rows)
                    {
                        int codInsumo = int.Parse(row.Cells[0].Text);
                        string cantidad = row.Cells[2].Text;
                        string medidaDetalle = row.Cells[3].Text;

                        bool resultado = insumC.insertarDetInsumoCreado(codInsumo, codinsumocreado, cantidad, medidaDetalle);


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



    }
}
