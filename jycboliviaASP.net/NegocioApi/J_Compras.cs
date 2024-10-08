using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.NegocioApi
{
    public class J_Compras
    {
        public int NumeroCompra { get; set; }
        public DateTime Fecha { get; set; }
        public string Referencia { get; set; }
        public decimal ImporteProductos { get; set; }
        public decimal ImporteDescuento { get; set; }
        public decimal ImporteTotal { get; set; }
        public int CodigoMoneda { get; set; }
        public int CodigoProveedor { get; set; }
        public int CodigoDistribucionGastos { get; set; }
        public Pagos Pagos { get; set; }
        public bool FacturaPosterior { get; set; }
        public Factura Factura { get; set; }
        public string Glosa { get; set; }
        public List<DetalleProducto> DetalleProductos { get; set; }
        public string Usuario { get; set; }
    }
}

public class Pagos
{
        public decimal TotalEfectivo { get; set; }
        public decimal TotalCredito { get; set; }
        public decimal TotalCheques { get; set; }
        public decimal TotalDeposito { get; set; }
    }

public class Factura
{
        public string NIT_CI { get; set; }
        public string RazonSocial { get; set; }
        public string NumeroFactura { get; set; }
        public string CodigoAutorizacion { get; set; }
        public string CodigoControl { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal ImporteDescuento { get; set; }
        public decimal ImporteGift { get; set; }
        public decimal ImporteNeto { get; set; }
        public bool AplicaCredictoFiscal { get; set; }
    }


public class DetalleProducto
    {
        public int NumeroItem { get; set; }
        public string CodigoProducto { get; set; }
        public int Cantidad { get; set; }
        public int CodigoUnidadMedida { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal ImporteDescuento { get; set; }
        public decimal PorcentajeGasto { get; set; }
        public decimal ImporteTotal { get; set; }
    }

