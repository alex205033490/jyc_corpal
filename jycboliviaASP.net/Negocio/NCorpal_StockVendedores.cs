using jycboliviaASP.net.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace jycboliviaASP.net.Negocio
{
    public class NCorpal_StockVendedores
    {
        DCorpal_StockVendedores nego = new DCorpal_StockVendedores();


        internal bool POST_registroEntradaStock(int codVendedor, int codProducto, string producto, int cantidad)
        {
            return nego.POST_registroEntradaStock(codVendedor, codProducto, producto, cantidad);
        }

        internal DataSet GET_verStockProductosVendedor(int codigo)
        {
            return nego.GET_verStockProductosVendedor(codigo);
        }

    }
}