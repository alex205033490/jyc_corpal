<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APICompras.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APICompras" MasterPageFile="~/PlantillaNew.Master" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-lg-12" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">

                    <!---------------- FORMULARIO COMPRAS -------------->
                    <div class="container-POSTCompras p-4 rounded col-lg-12">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Formulario compras</h3>
                        </div>

                        <div class="form_CompraP1 row">
                            <div class="col-6 col-sm-4 col-md-4 col-lg-4">
                                <asp:Label ID="lblreferencia" runat="server" Text="Referencia:" />
                                <asp:TextBox ID="txt_referencia" runat="server" CssClass="form-control" AutoComplete="off" />
                            </div>
                            <div class="col-6 col-sm-4 col-md-3 col-lg-3">
                                <asp:Label ID="lblimporteDescuentos" runat="server" Text="Importe descuentos:" />
                                <asp:TextBox ID="txt_importe_Descuentos" runat="server" CssClass="form-control" AutoComplete="off" />
                            </div>
                            <did class="col-6 col-sm-4 col-md-4 col-lg-3">
                                <asp:Label runat="server" Text="lblcodigoMoneda">Tipo de moneda: </asp:Label>
                                <asp:DropDownList ID="dd_codMoneda" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Bolivianos" Value="1" />
                                    <asp:ListItem Text="Dolares" Value="2" />
                                </asp:DropDownList>
                            </did>
                            <div class="col-6 col-sm-4 col-md-3 col-lg-3">
                                <asp:Label ID="lblcodProveedor" runat="server" Text="Codigo proveedor: " />
                                <asp:TextBox ID="txt_codProveedor" runat="server" CssClass="form-control" AutoComplete="off" />
                            </div>

                            <div class="col-6 col-sm-4 col-md-4 col-lg-3">
                                <asp:Label ID="lblcodDistribucionGastos" runat="server" Text="Cod distribución gastos: " />
                                <asp:TextBox ID="txt_codDistribucionGastos" runat="server" CssClass="form-control" AutoComplete="off" />
                            </div>

                            <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                                <asp:Label ID="lblFacturaPosterior" runat="server" Text="Factura posterior: " />
                                <asp:DropDownList ID="dd_fPosterior" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Si" Value="true" />
                                    <asp:ListItem Text="No" Value="false" />
                                </asp:DropDownList>
                            </div>

                            <div class="col-7 col-sm-5 col-md-4 col-lg-3">
                                <asp:Label ID="lblGlosa" runat="server" Text="Glosa:" />
                                <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control" AutoComplete="off" />
                            </div>
                        </div>


                        <!--  FORMULARIO PAGOS  -->
                        <div class="col-lg-12 row">
                            <!-- Col 1 -->
                            <div class="form_pagosDTO col-sm-6 col-md-5 col-lg-5 border mx-auto mt-2 rounded">
                                <div class="text-center">
                                    <h5>Pagos </h5>
                                </div>
                                <div class="row col-sm-12 col-md-12">
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                                        <asp:Label ID="lbltotalEfect" runat="server" Text="Total efectivo: " />
                                        <asp:TextBox ID="txt_totalEfectivo" runat="server" CssClass="form-control" AutoComplete="off" />
                                        <asp:Label ID="lbltotalCheque" runat="server" Text="Total cheque: " />
                                        <asp:TextBox ID="txt_totalCheques" runat="server" CssClass="form-control" AutoComplete="off" />
                                    </div>
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                                        <asp:Label ID="lbltotalCredito" runat="server" Text="Total credito: " />
                                        <asp:TextBox ID="txt_totalCredito" runat="server" CssClass="form-control" AutoComplete="off" />
                                        <asp:Label ID="lbltotalDeposito" runat="server" Text="Total deposito: " />
                                        <asp:TextBox ID="txt_totalDeposito" runat="server" CssClass="form-control" AutoComplete="off" />
                                    </div>
                                </div>

                                <!-- {Sub credito} -->
                                <div class="form_creditoDTO border border col-sm-12 col-md-12 col-lg-12 mx-auto row mt-3">
                                    <div class="text-center">
                                        <h5>Credito</h5>
                                    </div>
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                                        <asp:Label runat="server">Tipo de cuenta:</asp:Label>
                                        <asp:TextBox ID="txt_tipoCuenta" runat="server" placeholder="" CssClass="form-control" AutoComplete="off" />
                                    </div>
                                    <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                                        <asp:Label runat="server">Dias credito:</asp:Label>
                                        <asp:TextBox ID="txt_DiasCredito" runat="server" placeholder="" CssClass="form-control" AutoComplete="off" />
                                    </div>
                                </div>
                            </div>


                            <!-- COL 2 -->
                            <!-- DATOS FACTURA -->
                            <div class="form_pagosDTO col-sm-6 col-md-6 col-lg-6 border rounded mx-auto mt-2 mb-2">
                                <div class="text-center">
                                    <h5>Datos factura</h5>
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

                            <!-- DetalleProductos -->

                            <div class="form_detproducto col-12 col-sm-12 col-md-12">
                                <h4 class="form-label">Detalle Productos</h4>
                                <div class="table-detProducto">
                                    <table id="tblDetalleProductosIngresos" class="table table-bordered table-striped">
                                        <thead>
                                            <tr>
                                                <th>Código producto</th>
                                                <th>Cantidad</th>
                                                <th>Código unidad medida</th>
                                                <th>Precio unitario</th>
                                                <th>Importe descuento</th>
                                                <th>Porcentaje de gasto</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <input class="form-control" type="number" name="codigoProducto0" /></td>
                                                <td>
                                                    <input class="form-control" type="number" name="cantidad0" step="0.01" /></td>
                                                <td>
                                                    <input class="form-control" type="number" name="codUnidadMedida0" step="0.01" /></td>
                                                <td>
                                                    <input class="form-control" type="number" name="precioUnitario0" /></td>
                                                <td>
                                                    <input class="form-control" type="number" name="importeDescuento0" step="0.01" /></td>
                                                <td>
                                                    <input class="form-control" type="number" name="porcentajeGasto0" step="0.01" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <asp:Button ID="btnAddRow" runat="server" Text="Agregar Fila" CssClass="btn btn-success" OnClientClick="addRowIngreso(); return false;" />
                                <asp:Button ID="btn_registrarIngreso" runat="server" Text="Registrar Ingreso" CssClass="btn btn-success" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>

    <br />
</asp:Content>
