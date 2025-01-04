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
                            <h2 class="text_tittle p-3">Registro de Compras </h2>
                        </div>

                        <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="Panel_POSTCompra" runat="server" DefaultButton="btn_registrarCompra">
                                    <div class="form_CompraP1 col-lg-12 row">

                                        <!-- COL 1 -->

                                        <div class="form_datos col-sm-6 col-md-6 col-lg-7 mx-auto mt-2 mb-2">
                                            <div class="text-center">
                                                <h2>Datos Compra</h2>
                                            </div>

                                            <div class="DcompraDTO col-lg-12 row">
                                                <!-- col 1 -->
                                                <div class="col-sm-12 col-md-6 col-lg-6">
                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                                        <asp:Label ID="lblreferencia" runat="server" Text="Referencia:" />
                                                        <asp:TextBox ID="txt_referencia" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Opcional" />
                                                    </div>
                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                                        <asp:Label ID="lblimporteDescuentos" runat="server" Text="Importe Descuento:" />
                                                        <asp:TextBox ID="txt_importe_Descuentos" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Opcional" />
                                                    </div>
                                                    <did class="col-12 col-sm-12 col-md-12 col-lg-12">
                                                        <asp:Label ID="lblcodMoneda" runat="server" Text="Tipo de moneda:" />
                                                        <asp:DropDownList ID="dd_codMoneda" runat="server" CssClass="form-select dd_fsmall">
                                                            <asp:ListItem Text="Bolivianos" Value="1" />
                                                            <asp:ListItem Text="Dólares Americanos" Value="2" />
                                                            <asp:ListItem Text="Unidad Fomento Vivienda" Value="3" />
                                                        </asp:DropDownList>
                                                    </did>
                                                </div>

                                                <!-- col 2 -->
                                                <div class="col-sm-12 col-md-6 col-lg-6">

                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                                        <asp:Label ID="lblcodProveedor" runat="server" Text="Proveedor: " />
                                                        <asp:DropDownList ID="dd_codProveedor" runat="server" CssClass="form-select dd_fsmall">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                                        <asp:Label ID="lblGlosa" runat="server" Text="Glosa:" />
                                                        <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Opcional" />
                                                    </div>

                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                                        <asp:Label ID="lblcodDistrGastos" runat="server" Text="Dist. Gastos"></asp:Label>
                                                        <asp:DropDownList ID="dd_codDistGastos" runat="server" CssClass="form-select dd_fsmall">
                                                            <asp:ListItem Text="Sin Seleccionar" Value="0" />
                                                            <asp:ListItem Text="En función al costo" Value="1" />
                                                            <asp:ListItem Text="En función a la cantidad" Value="2" />
                                                            <asp:ListItem Text="Manual" Value="3" />
                                                            <asp:ListItem Text="Por volumen" Value="4" />
                                                            <asp:ListItem Text="Por peso" Value="5" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <!-- COL 2 -->
                                        <!-- DATOS FACTURA -->
                                        <div class="form_pagosDTO col-sm-6 col-md-6 col-lg-5 border rounded mx-auto mt-2 mb-2 p-2">
                                            <div class="text-center">
                                                <h5>Datos Factura</h5>
                                            </div>

                                            <div class="facturaDTO col-lg-12 row">
                                                <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                                                    <asp:Label ID="lbl_nit" runat="server" Text="NIT/CI:" />
                                                    <asp:TextBox ID="txt_nit" runat="server" CssClass="form-control" AutoComplete="off" Placeholder="Opcional" />
                                                    <asp:Label ID="lbl_razonsocial" runat="server" Text="Razón social:" />
                                                    <asp:TextBox ID="txt_razonSocial" runat="server" CssClass="form-control" AutoComplete="off" />
                                                    <asp:Label ID="lbl_nFactura" runat="server" Text="Número factura:" />
                                                    <asp:TextBox ID="txt_nFactura" runat="server" CssClass="form-control" AutoComplete="off" />

                                                </div>
                                                <div class="col-6 col-sm-6 col-md-6 col-lg-6">
                                                    <asp:Label ID="lblcodAutorizacion" runat="server" Text="Código autorización: " />
                                                    <asp:TextBox ID="txt_codAutorizacion" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Opcional" />
                                                    <asp:Label ID="lblcodControl" runat="server" Text="Código control: " />
                                                    <asp:TextBox ID="txt_codControl" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Opcional" />
                                                    <asp:Label runat="server" Text="lblapCredFiscal">Aplica crédito fiscal: </asp:Label>
                                                    <asp:DropDownList ID="dd_apCredFiscal" runat="server" CssClass="form-select">
                                                        <asp:ListItem Text="Si" Value="true" />
                                                        <asp:ListItem Text="No" Value="false" />
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_registrarCompra" EventName="Click" />
                            </Triggers>

                        </asp:UpdatePanel>

                        <br />
                        <!-- DETALLE PRODUCTO -->

                        <asp:UpdatePanel ID="updatePanelPost_PostCompra" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="form_detproducto col-md-12 col-lg-10">
                                    <h3 class="form-label">Detalle Productos</h3>


                                    <div class="form_addProducto row mb-3">


                                        <div class="input_producto col-4">
                                            <asp:Label runat="server">Producto:</asp:Label>
                                            <asp:TextBox ID="txt_producto" runat="server" AutoPostBack="true" CssClass="form-control" AutoComplete="off" OnTextChanged="txt_producto_TextChanged"></asp:TextBox>
                                        </div>


                                        <div class="input_cantidad col-2">
                                            <asp:Label runat="server"> Cantidad:</asp:Label>
                                            <asp:TextBox ID="txt_cantProducto" runat="server" CssClass="form-control" AutoComplete="off" oninput="convertdotcomma(event);"></asp:TextBox>
                                        </div>

                                        <div class="input_impDescuento col-2">
                                            <asp:Label runat="server">Descuento</asp:Label>
                                            <asp:TextBox ID="txt_impDescProd" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Opcional" oninput="convertdotcomma(event);"></asp:TextBox>
                                        </div>

                                        <div class="input_porcenGasto col-2">
                                            <asp:Label runat="server">Gastos (%)</asp:Label>
                                            <asp:TextBox ID="txt_porceGastos" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Opcional" oninput="convertdotcomma(event);"></asp:TextBox>
                                        </div>

                                        <div class="container_btnAddProd col-2 d-flex align-items-end">
                                            <asp:Button runat="server" ID="btn_addProd" Text="Agregar" CssClass="btn btn-success" OnClick="btn_addProd_Click" />
                                        </div>

                                    </div>



                                    <asp:GridView ID="gv_listProdCompras" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClass="table table-bordered" OnSelectedIndexChanged="gv_listProdCompras_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Producto" HtmlEncode="false" />
                                            <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" />
                                            <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="UM" />
                                            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio" />
                                        </Columns>
                                    </asp:GridView>


                                    <div class="container_gvProdAddII mb-3">
                                        <asp:GridView ID="gv_productAgregados" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClss="table table-bordered" OnRowCommand="gv_productAgregados_RowCommand">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEliminarFila" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("CodigoProducto") %>' CssClass="btn btn-danger btn-sm" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="codigoProducto" HeaderText="Codigo" />
                                                <asp:BoundField DataField="nombre" HeaderText="Producto" />
                                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                                <asp:BoundField DataField="codigoUnidadMedida" HeaderText="Cod Unidad Medida" />
                                                <asp:BoundField DataField="precioUnitario" HeaderText="Precio" />
                                                <asp:BoundField DataField="importeDescuento" HeaderText="Descuento" />
                                                <asp:BoundField DataField="porcentajeGasto" HeaderText="Gastos %" />
                                                <asp:BoundField DataField="importeTotal" HeaderText="Importe Total" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>


                                    <div>
                                        <asp:Button ID="btn_registrarCompra" runat="server" Text="Registrar Compra" CssClass="btn btn-success" OnClick="btn_registrarCompra_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_addProd" EventName="Click" />
                            </Triggers>

                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <br />
    <script src="../js/jsApi.js" type="text/javascript"></script>

</asp:Content>
