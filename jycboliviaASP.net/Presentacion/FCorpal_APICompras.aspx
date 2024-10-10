<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APICompras.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APICompras" MasterPageFile="~/PlantillaNew.Master" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_SimecModificar.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">
                    <div>
                        <asp:Label runat="server" Text="Label">API Compras</asp:Label><br />
                        <br />
                    </div>


                    <div>
                        <!-- COMPRADTO -->
                        <div class="compraDTP">
                        <asp:Label ID="lblreferencia" runat="server" Text="Referencia: " />
                        <asp:TextBox ID="txt_referencia" runat="server" />
                        <asp:Label ID="lblimporteDescuentos" runat="server" Text="Importe Descuentos: " />
                        <asp:TextBox ID="txt_importe_Descuentos" runat="server"/>                        
                        <br />
                        <asp:Label runat="server" Text="lblcodigoMoneda">Tipo de moneda: </asp:Label>
                        <asp:DropDownList ID="dd_codMoneda" runat="server">
                            <asp:ListItem Text="Bolivianos" Value="1" />
                            <asp:ListItem Text="Dolares" Value="2" />
                        </asp:DropDownList>
                        <asp:Label ID="lblcodProveedor" runat="server" Text="Codigo Proveedor: " />
                        <asp:TextBox ID="txt_codProveedor" runat="server"/>
                        <br />
                        <asp:Label ID="lblcodDistribucionGastos" runat="server" Text="Codigo Distribucion Gastos: " />
                        <asp:TextBox ID="txt_codDistribucionGastos" runat="server"/><br/>
                            </div>
                        <br /><br /><br>
                        
                        
                        <div class="pagosDTO"> 
                        <!-- PAGOS {     }-->
                        <asp:Label ID="lblPAGOS" runat="server" Text="--PAGOS--" /><br>
                            <asp:Label ID="lbltotalEfect" runat="server" Text="Total Efectivo: "/>
                        <asp:TextBox ID="txt_totalEfectivo" runat="server"/>
                            <asp:Label ID="lbltotalCredito" runat="server" Text="Total credito: " />
                        <asp:TextBox ID="txt_totalCredito" runat="server"/><br>
                            <asp:Label ID="lbltotalCheque" runat="server" Text="total cheque: " />
                        <asp:TextBox ID="txt_totalCheques" runat="server" />
                            <asp:Label ID="lbltotalDeposito" runat="server" Text="total deposito: " />
                        <asp:TextBox ID="txt_totalDeposito" runat="server" /><br />
                            <!-- {Sub credito} -->
                        <asp:Label ID="lblSubcredito" runat="server" Text="PAGOS: " />
                        <asp:TextBox ID="txt_tipoCuenta" runat="server" placeholder="tipo cuenta"/>
                        <asp:TextBox ID="txt_DiasCredito" runat="server" placeholder="dias credito" />
                        <br />
                            <!-- {Sub cheque} -->
                        <asp:Label ID="lblSubcheque" runat="server" Text="CHEQUES: " />
                        <asp:TextBox ID="txt_codigoBanco" runat="server" placeholder="codigo banco"/>
                        <asp:TextBox ID="txt_numeroCuenta" runat="server" placeholder="numero cuenta" />
                        <asp:TextBox ID="txt_codigoCheque" runat="server" placeholder="codigo cheque"/>
                        <asp:TextBox ID="txt_numeroCheque" runat="server" placeholder="numero cheque" />
                        <br />
                            <!-- {Sub Deposito} -->
                        <asp:Label ID="lblDeposito" runat="server" Text="Deposito: " />
                        <asp:TextBox ID="txt_codigoBanco2" runat="server" placeholder="codigo banco"/>
                        <asp:TextBox ID="txt_numeroCuenta2" runat="server" placeholder="numero cuenta" />
                        <asp:TextBox ID="txt_referenciaDeposito" runat="server" placeholder="referencia"/>
                        <br />
                        </div>
                        <br /><br /><br>

                        <!--GASTOS [{}] -->
                        <div class="gastosDTO">
                        <asp:Label ID="lblgastos" runat="server" Text="--GASTOS--" /><br />
                        <asp:Label ID="lblcodigogasto" runat="server" Text="codigo gasto: " />
                        <asp:TextBox ID="txt_codGasto" runat="server"/>
                        <asp:Label ID="lblimporte" runat="server" Text="importe: " />
                        <asp:TextBox ID="txt_importe" runat="server" /><br />
                        <asp:Label runat="server" Text="lblcodigoMonedaGastos">Tipo de moneda: </asp:Label>
                        <asp:DropDownList ID="dd_codMonedaGastos" runat="server">
                            <asp:ListItem Text="Bolivianos" Value="1" />
                            <asp:ListItem Text="Dolares" Value="2" />
                        </asp:DropDownList>                        
                        <asp:Label runat="server" Text="lblaplicaIVA">Aplica IVA: </asp:Label>
                        <asp:DropDownList ID="dd_aplicaIVA" runat="server">
                            <asp:ListItem Text="Si" Value="true" />
                            <asp:ListItem Text="No" Value="false" />
                        </asp:DropDownList>
                        </div>
                        <br /><br /><br />

                        
                        <asp:Label runat="server" Text="lblfacturaPosterior">Factura Posterior: </asp:Label>
                        <asp:DropDownList ID="dd_facturaPost" runat="server">
                            <asp:ListItem Text="Si" Value="true" />
                            <asp:ListItem Text="No" Value="false" />
                        </asp:DropDownList>
                        <br /><br />
                        

                        <!-- FACTURA {} -->
                        <div class="facturaDTO">
                        <asp:Label ID="lblfactura" runat="server" Text="--FACTURA--" /><br/>
                        <asp:Label ID="lbl_nit" runat="server" Text="NIT_CI: " />
                        <asp:TextBox ID="txt_nit" runat="server" />
                        <asp:Label ID="lbl_razonsocial" runat="server" Text="Razon Social: " />
                        <asp:TextBox ID="txt_razonSocial" runat="server"/>
                        <br />                   
                        <asp:Label ID="lbl_nFactura" runat="server" Text="numero factura: " />
                        <asp:TextBox ID="txt_nFactura" runat="server" />
                        <asp:Label ID="lblcodAutorizacion" runat="server" Text="codigo Autorizacion: " />
                        <asp:TextBox ID="txt_codAutorizacion" runat="server"/>
                        <br />
                        <asp:Label ID="lblcodControl" runat="server" Text="codigo control: " />
                        <asp:TextBox ID="txt_codControl" runat="server" />
                        <asp:Label ID="lblimpTotal" runat="server" Text="importe total: " />
                        <asp:TextBox ID="txt_imptotal" runat="server"/>
                        <br />
                        <asp:Label ID="lblimpDescuento" runat="server" Text="importe descuento: " />
                        <asp:TextBox ID="txt_impDescuento" runat="server" />
                        <asp:Label ID="lblimpGift" runat="server" Text="ImporteGift: " />
                        <asp:TextBox ID="txt_impGift" runat="server"/>
                        <br />
                        <asp:Label ID="lblimpNeto" runat="server" Text="importe neto: " />
                        <asp:TextBox ID="txt_impNeto" runat="server" />
                        <asp:Label runat="server" Text="lblapCredFiscal">Aplica Credito Fiscal: </asp:Label>
                        <asp:DropDownList ID="dd_apCredFiscal" runat="server">
                            <asp:ListItem Text="Si" Value="true" />
                            <asp:ListItem Text="No" Value="false" />
                        </asp:DropDownList>
                        </div>
                        <br /><br /><br />


                        <asp:Label ID="lblGlosa" runat="server" Text="Glosa: " />
                        <asp:TextBox ID="txt_glosa" runat="server" />
                        <br /><br />


                        <!-- DetalleProductos [{}] -->
                        <div class="detalleProductoDTO">
                        <asp:Label ID="lblDetalleProducto" runat="server" Text="--Detalle Productos--"/><br />
                        <asp:Label ID="lblnumItem" runat="server" Text="Numero Item: "/>
                        <asp:TextBox ID="txt_numItem" runat="server" />
                        <asp:Label ID="lblCodigoProducto" runat="server" Text="Codigo Producto: "/>
                        <asp:TextBox ID="txt_codProducto" runat="server" /> <br />
                        <asp:Label ID="lblcantidad" runat="server" Text="Cantidad: "/>
                        <asp:TextBox ID="txt_cantidad" runat="server" />
                        <asp:Label ID="lblcodUnidadMedida" runat="server" Text="Codigo unidad medida: "/>
                        <asp:TextBox ID="txt_codUnidadMedida" runat="server" /><br />
                        <asp:Label ID="lblprecioUnitario" runat="server" Text="Precio unitario: "/>
                        <asp:TextBox ID="txt_precUnitario" runat="server" />
                        <asp:Label ID="lblimpDescuento2" runat="server" Text="Importe descuento: "/>
                        <asp:TextBox ID="txt_impDescuento2" runat="server" /><br />
                        <asp:Label ID="lblporcGasto" runat="server" Text="Porcentaje Gasto: "/>
                        <asp:TextBox ID="txt_PorcGasto" runat="server" />
                        </div>

                        <asp:Button ID="btn_registrarCompra" runat="server" Text="Enviar" OnClick="btn_registrarCompra_Click" /> <br /><br />

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
