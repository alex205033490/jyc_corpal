using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jycboliviaASP.net.NegocioApi
{
    public class J_Ventas
    {
        public int NumeroVenta { get; set; }
        public int NumeroPedido { get; set; }
        public DateTime Fecha {  get; set; }
        public int CodigoCliente {  get; set; }
        public string Referencia { get; set; }
        public string Glosa {get; set; }
        public bool EmitirFactura { get; set; }
        public decimal ImporteProductos { get; set; }
        public decimal ImporteDescuentos { get; set; }
        public decimal ImporteTotal {  get; set; }        
        public Cobros Cobros { get; set; }
        public FacturaV Factura { get; set; }
        public List<DetalleProductoV> DetalleProductos { get; set; }
        public string Usuario { get; set; }
    }
}

public class Cobros
{
    public decimal TotalEfectivo { get; set; }
    public decimal TotalDeposito { get; set; }    
}

public class FacturaV
{
    public int TipoDocumentoIdentidad { get; set; }
    public string NIT_CI { get; set; }
    public string Complemento { get; set; }
    public string RazonSocial { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public int MetodoPago { get; set; }    
}

public class DetalleProductoV
{
    public int NumeroItem { get; set; }
    public string CodigoProducto { get; set; }
    public int Cantidad { get; set; }
    public int CodigoUnidadMedida { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal ImporteDescuento { get; set; }
    public decimal ImporteTotal { get; set; }
    public int NumeroItemOrigen { get; set; }
}