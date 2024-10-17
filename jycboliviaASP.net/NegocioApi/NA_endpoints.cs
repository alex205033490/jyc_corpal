
using Clases.ApiRest;
using jycboliviaASP.net.Negocio;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Web;

namespace jycboliviaASP.net.NegocioApi
{
    public class NA_endpoints
    {
        DBApi api = new DBApi();
        public NA_endpoints() { }

        public string get_TokenUsuario(string usuario , string password ) {
         //   NA_Responsables nres = new NA_Responsables();
         //   DataSet datosUpon = nres.get_datosUpon(usuario, password);
            string usuarioUpon = usuario;
            string passwordUpon = password;
        /*
            if (datosUpon.Tables[1].Rows.Count > 0)
            {
                usuarioUpon = datosUpon.Tables[1].Rows[0][0].ToString();
                passwordUpon = datosUpon.Tables[1].Rows[0][1].ToString();
            }
          */              
            Persona datoP = new Persona
            {
                Username = usuarioUpon,
                Password = passwordUpon
            };

            string json = JsonConvert.SerializeObject(datoP);
            dynamic respuesta = api.Post("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", json);
            string token = respuesta.Resultado.Token.ToString();
            return token;
        }

        public int get_codigoUsuarioVendedor(string usuario, string password) {
            NA_Responsables nres = new NA_Responsables();
            DataSet datosUpon = nres.get_datosUpon(usuario, password);
            string usuarioUpon = usuario;
            string passwordUpon = password;

            if (datosUpon.Tables[1].Rows.Count > 0)
            {
                usuarioUpon = datosUpon.Tables[1].Rows[0][0].ToString();
                passwordUpon = datosUpon.Tables[1].Rows[0][1].ToString();
            }

            Persona datoP = new Persona
            {
                Username = usuarioUpon,
                Password = passwordUpon
            };

            string json = JsonConvert.SerializeObject(datoP);
            string url = "http://192.168.11.62/ServcioUponApi/api/v1/auth/login";
            dynamic respuesta = api.Post(url, json);
            string codigo = respuesta.Resultado.CodVendedor.ToString();
            return int.Parse(codigo);
        }

        internal dynamic get_productoAlmacen(string nombreProducto, string usuario, string password)
        {
            string token = get_TokenUsuario(usuario, password);
            string parametro = "criterio=" + nombreProducto + "&usuario=" + usuario;

            string url = "http://192.168.11.62/ServcioUponApi/api/v1/almacenes/productos/Busqueda?";
            url = url + parametro;
            dynamic respuesta = api.Get_2(url, token);
            return  respuesta;
        }

        internal bool insertarCompras(string Token,int numeroCompra, DateTime fecha, string referencia, decimal importeProductos, decimal importeDescuento, decimal importeTotal, int codigoMoneda, int codigoProveedor, int codigoDistribucionGastos, decimal pagos_TotalEfectivo, decimal pagos_TotalCredito, decimal pagos_TotalCheques, decimal pagos_TotalDeposito, bool facturaPosterior, string factura_NIT_CI, string factura_RazonSocial, string factura_NumeroFactura, string factura_CodigoAutorizacion, string factura_CodigoControl, decimal factura_ImporteTotal, decimal factura_ImporteDescuento, decimal factura_ImporteGift, decimal factura_ImporteNeto, bool factura_AplicaCredictoFiscal, string glosa, int dprod_NumeroItem, string dprod_CodigoProducto, int dprod_Cantidad, int dprod_CodigoUnidadMedida, decimal dprod_PrecioUnitario, decimal dprod_ImporteDescuento, decimal dprod_PorcentajeGasto, decimal dprod_ImporteTotal, string usuario, bool estado, bool vaciadoupon)
        {
            J_Compras compra = new J_Compras {
                NumeroCompra = numeroCompra,
                Fecha = fecha,
                Referencia = referencia,
                ImporteProductos = importeProductos,
                ImporteDescuento = importeDescuento,
                ImporteTotal = importeTotal,
                CodigoMoneda = codigoMoneda,
                CodigoProveedor = codigoProveedor,
                CodigoDistribucionGastos = codigoDistribucionGastos,
                Pagos = new Pagos
                {
                    TotalEfectivo = pagos_TotalEfectivo,
                    TotalCredito = pagos_TotalCredito,
                    TotalCheques = pagos_TotalCheques,
                    TotalDeposito = pagos_TotalDeposito
                },
                FacturaPosterior = facturaPosterior,
                Factura = new Factura
                {
                    NIT_CI = factura_NIT_CI,
                    RazonSocial = factura_RazonSocial,
                    NumeroFactura = factura_NumeroFactura,
                    CodigoAutorizacion = factura_CodigoAutorizacion,
                    CodigoControl = factura_CodigoControl,
                    ImporteTotal = factura_ImporteTotal,
                    ImporteDescuento = factura_ImporteDescuento,
                    ImporteGift = factura_ImporteGift,
                    ImporteNeto = factura_ImporteNeto,
                    AplicaCredictoFiscal = factura_AplicaCredictoFiscal
                },
                Glosa = glosa,
                DetalleProductos = new List<DetalleProducto>
            {
                new DetalleProducto
                {
                    NumeroItem = dprod_NumeroItem,
                    CodigoProducto = dprod_CodigoProducto,
                    Cantidad = dprod_Cantidad,
                    CodigoUnidadMedida = dprod_CodigoUnidadMedida,
                    PrecioUnitario = dprod_PrecioUnitario,
                    ImporteDescuento = dprod_ImporteDescuento,
                    PorcentajeGasto = dprod_PorcentajeGasto,
                    ImporteTotal = dprod_ImporteTotal
                }
            },
                Usuario = usuario
            };

            string json = JsonConvert.SerializeObject(compra);
            string url = "http://192.168.11.62/ServcioUponApi/api/v1/compras";
            dynamic respuesta = api.Post(url, json, Token);
            string EsValido = respuesta.EsValido.ToString();
            bool EsValido_bandera = Convert.ToBoolean(EsValido);           

            return EsValido_bandera;

        }

        internal bool get_AutenticarUsuario(string admin, string pass)
        {        
            string usuarioUpon = admin;
            string passwordUpon = pass;
            
            Persona datoP = new Persona
            {
                Username = usuarioUpon,
                Password = passwordUpon
            };
            string json = JsonConvert.SerializeObject(datoP);
            dynamic respuesta = api.Post("http://192.168.11.62/ServcioUponApi/api/v1/auth/login", json);
            string dato = respuesta.EsValido.ToString();
            return Convert.ToBoolean(dato);
        }

        internal bool insertarVentas(string token,        
        int NumeroVenta,
        int NumeroPedido,        
        DateTime Fecha,        
        int CodigoCliente,        
        string Referencia,
        string Glosa,
        bool EmitirFactura,
        decimal ImporteProductos,
        decimal ImporteDescuentos,        
        decimal ImporteTotal,        
        decimal Cobros_TotalEfectivo,        
        decimal Cobros_TotalDeposito,
        int Factura_TipoDocumentoIdentidad,        
        string Factura_NIT_CI,
        string Factura_Complemento,
        string Factura_RazonSocial,
        string Factura_Telefono,
        string Factura_Email,
        int Factura_MetodoPago,
        int DetProd_NumeroItem,
        string DetProd_CodigoProducto,
        int DetProd_Cantidad,
        int DetProd_CodigoUnidadMedida,
        decimal DetProd_PrecioUnitario,        
        decimal DetProd_ImporteDescuento,        
        decimal DetProd_ImporteTotal,
        int DetProd_NumeroItemOrigen,                
        string Usuario)
        {

            J_Ventas venta1 = new J_Ventas
            {
                 NumeroVenta = NumeroVenta,
                 NumeroPedido= NumeroPedido,
                 Fecha = Fecha,
                 CodigoCliente = CodigoCliente,
                 Referencia = Referencia,
                 Glosa = Glosa,
                 EmitirFactura = EmitirFactura,
                 ImporteProductos = ImporteProductos,
                 ImporteDescuentos = ImporteDescuentos,
                 ImporteTotal = ImporteTotal,

                Cobros = new Cobros
                {
                    TotalEfectivo = Cobros_TotalEfectivo,
                    TotalDeposito = Cobros_TotalDeposito
                },
                 
                Factura = new FacturaV {
                    TipoDocumentoIdentidad = Factura_TipoDocumentoIdentidad,
                    NIT_CI = Factura_NIT_CI,
                    Complemento = Factura_Complemento,
                    RazonSocial = Factura_RazonSocial,
                    Telefono = Factura_Telefono,
                    Email = Factura_Email,
                    MetodoPago = Factura_MetodoPago
                },
                            
                DetalleProductos = new List<DetalleProductoV>
            {
                new DetalleProductoV
                {
                     NumeroItem = DetProd_NumeroItem,
                     CodigoProducto = DetProd_CodigoProducto,
                     Cantidad = DetProd_Cantidad,
                     CodigoUnidadMedida = DetProd_CodigoUnidadMedida,
                     PrecioUnitario = DetProd_PrecioUnitario,
                     ImporteDescuento = DetProd_ImporteDescuento,
                     ImporteTotal = DetProd_ImporteTotal,
                     NumeroItemOrigen = DetProd_NumeroItemOrigen
                }
            },
                Usuario = Usuario                
            };

            string json1 = JsonConvert.SerializeObject(venta1);
            string url = "http://192.168.11.62/ServcioUponApi/api/v1/ventas";
            dynamic respuesta = api.Post(url, json1, token);
            string EsValido = respuesta.EsValido.ToString();
            bool EsValido_bandera = Convert.ToBoolean(EsValido);

            return EsValido_bandera;

        }
    }
}


public class Persona
{
    public string Username { get; set; }
    public string Password { get; set; }

}



