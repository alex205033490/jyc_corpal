<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APICompras.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APICompras" MasterPageFile="~/PlantillaNew.Master" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-lg-12" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-11 col-md-offset-1">
                <div class="panel panel-success class">

                    <!---------------- FORMULARIO COMPRAS -------------->
                    <div class="container-POSTCompras p-4 rounded col-lg-12">
                        <div class="container_tittle rounded">
                            <h2 class="text_tittle p-3">Registro de Compras</h2>
                        </div>
                        <div class="form_CompraP1 col-lg-12 row">

                        <!-- COL 1 -->
                            <div class="form_datos col-sm-6 col-md-6 col-lg-6 mx-auto mt-2 mb-2">
                                <div class="text-center">
                                    <h2>Datos Compra</h2>
                                </div>
                                <div class="DcompraDTO col-lg-12 row">
                                <!-- col 1 -->
                                <div class="col-sm-12 col-md-6 col-lg-5">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                        <asp:Label ID="lblreferencia" runat="server" Text="Referencia:" />
                                        <asp:TextBox ID="txt_referencia" runat="server" CssClass="form-control" AutoComplete="off" />
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                        <asp:Label ID="lblimporteDescuentos" runat="server" Text="Importe descuentos:" />
                                        <asp:TextBox ID="txt_importe_Descuentos" runat="server" CssClass="form-control" AutoComplete="off" />
                                    </div>
                                    <did class="col-12 col-sm-12 col-md-12 col-lg-12">
                                        <asp:Label runat="server" Text="lblcodigoMoneda">Tipo de moneda: </asp:Label>
                                        <asp:DropDownList ID="dd_codMoneda" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="Bolivianos" Value="1" />
                                            <asp:ListItem Text="Dolares" Value="2" />
                                        </asp:DropDownList>
                                    </did>
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                        <asp:Label ID="lblcodProveedor" runat="server" Text="Codigo proveedor: " />
                                        <asp:TextBox ID="txt_codProveedor" runat="server" CssClass="form-control" AutoComplete="off" />
                                    </div>
                                </div>

                                <!-- col 2 -->
                                <div class="col-sm-12 col-md-6 col-lg-6">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                        <asp:Label ID="lblcodDistribucionGastos" runat="server" Text="Cod distribución gastos: " />
                                        <asp:TextBox ID="txt_codDistribucionGastos" runat="server" CssClass="form-control" AutoComplete="off" />
                                    </div>

                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                        <asp:Label ID="lblFacturaPosterior" runat="server" Text="Factura posterior: " />
                                        <asp:DropDownList ID="dd_fPosterior" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="Si" Value="true" />
                                            <asp:ListItem Text="No" Value="false" />
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                        <asp:Label ID="lblGlosa" runat="server" Text="Glosa:" />
                                        <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control" AutoComplete="off" />
                                    </div>
                                </div>
                                    </div>
                                <!-- Pagos -->
                            </div>

                            <!-- COL 2 -->
                            <!-- DATOS FACTURA -->
                            <div class="form_pagosDTO col-sm-6 col-md-6 col-lg-6 border rounded mx-auto mt-2 mb-2 p-2">
                                <div class="text-center">
                                    <h5>Datos Factura</h5>
                                </div>
                                <div class="facturaDTO col-lg-12 row">
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                                        <asp:Label ID="lbl_nit" runat="server" Text="NIT/CI:" />
                                        <asp:TextBox ID="txt_nit" runat="server" CssClass="form-control" AutoComplete="off" />
                                        <asp:Label ID="lbl_razonsocial" runat="server" Text="Razón social:" />
                                        <asp:TextBox ID="txt_razonSocial" runat="server" CssClass="form-control" AutoComplete="off" />
                                        <asp:Label ID="lbl_nFactura" runat="server" Text="Número factura:" />
                                        <asp:TextBox ID="txt_nFactura" runat="server" CssClass="form-control" AutoComplete="off" />
                                        <asp:Label ID="lblcodAutorizacion" runat="server" Text="Código autorización: " />
                                        <asp:TextBox ID="txt_codAutorizacion" runat="server" CssClass="form-control" AutoComplete="off" />
                                        <asp:Label ID="lblcodControl" runat="server" Text="Código control: " />
                                        <asp:TextBox ID="txt_codControl" runat="server" CssClass="form-control" AutoComplete="off" />

                                    </div>
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                                        <asp:Label ID="lblimpTotal" runat="server" Text="Importe total:" />
                                        <asp:TextBox ID="txt_imptotal" runat="server" CssClass="form-control" AutoComplete="off" />
                                        <asp:Label ID="lblimpDescuento" runat="server" Text="Importe descuento: " />
                                        <asp:TextBox ID="txt_impDescuento" runat="server" CssClass="form-control" AutoComplete="off" />
                                        <asp:Label ID="lblimpGift" runat="server" Text="Importe gift: " />
                                        <asp:TextBox ID="txt_impGift" runat="server" CssClass="form-control" AutoComplete="off" />
                                        <asp:Label ID="lblimpNeto" runat="server" Text="Importe neto: " />
                                        <asp:TextBox ID="txt_impNeto" runat="server" CssClass="form-control" AutoComplete="off" />
                                        <asp:Label runat="server" Text="lblapCredFiscal">Aplica crédito fiscal: </asp:Label>
                                        <asp:DropDownList ID="dd_apCredFiscal" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="Si" Value="true" />
                                            <asp:ListItem Text="No" Value="false" />
                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                        </div>

                        </div>
                    <br />
                        <!--  FORMULARIO PAGOS  -->
                    
                            <!-- DetalleProductos -->

                            <div class="form_ADDdetProducto col-12 col-sm-12 col-md-12 p-4 rounded">
                                <div class="col-12 text-center">
                                    <h4 class="form-label">Detalle  del Producto</h4>
                                </div>
                                <div class="table-detProducto">
                                    <table id="tblDetalleProductosCompra" class="table table-bordered table-striped">
                                        <thead>
                                            <tr>
                                                <th>Código de producto</th>
                                                <th>Cantidad</th>
                                                <th>Código unidad medida</th>
                                                <th>Precio unitario</th>
                                                <th>Importe descuento</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <input class="form-control" type="text" name="codigoProducto0" /></td>
                                                <td>
                                                    <input class="form-control" type="number" name="cantidad0" step="0.01" /></td>
                                                <td>
                                                    <input class="form-control" type="number" name="codUnidadMedida0" step="0.01" /></td>
                                                <td>
                                                    <input class="form-control" type="number" name="precioUnitario0" /></td>
                                                <td>
                                                    <input class="form-control" type="number" name="importeDescuento0" step="0.01" /></td>
                                                <td>
                                                    <asp:Button ID="btnAddRow" runat="server" Text="+" CssClass="btn btn-success" OnClientClick="addRowCompra(); return false;" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                                <asp:Button ID="btn_registrarCompra" runat="server" Text="Registrar Compra" CssClass="btn btn-success" OnClick="btn_registrarCompra_Click" />

                            </div>
                        </div>
             
                </div>
            </div>
        </div>
        <br />

    <br />
    <script src="../js/jsApi.js" type="text/javascript"></script>



</asp:Content>
