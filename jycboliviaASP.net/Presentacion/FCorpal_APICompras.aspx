<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APICompras.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APICompras" MasterPageFile="~/PlantillaNew.Master" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-lg-12" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-11 col-md-offset-1">
                <div class="panel panel-success class">
                    
                   <!---------------- COMPRADTO -------------->
                    <div class="container-POSTCompras p-4 rounded col-lg-12">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3"> Formulario Compras</h3>
                        </div>

                        <div class="form_CompraP1 row">
                            <div class="col-lg-4">
                                <asp:Label ID="lblreferencia" runat="server" Text="Referencia: " />
                                <asp:TextBox ID="txt_referencia" runat="server" CssClass="form-control" AutoComplete="off"/>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label ID="lblimporteDescuentos" runat="server" Text="Importe Descuentos: " />
                                <asp:TextBox ID="txt_importe_Descuentos" runat="server" CssClass="form-control" AutoComplete="off" />
                            </div>
                            <did class="col-lg-3">
                                <asp:Label runat="server" Text="lblcodigoMoneda">Tipo de moneda: </asp:Label>
                                <asp:DropDownList ID="dd_codMoneda" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Bolivianos" Value="1" />
                                    <asp:ListItem Text="Dolares" Value="2" />
                                </asp:DropDownList>
                            </did>
                            <div class="col-lg-3">
                                <asp:Label ID="lblcodProveedor" runat="server" Text="Codigo Proveedor: " />
                                <asp:TextBox ID="txt_codProveedor" runat="server" CssClass="form-control" AutoComplete="off"/>
                            </div>

                            <div class="col-lg-3">
                                <asp:Label ID="lblcodDistribucionGastos" runat="server" Text="Codigo Distribucion Gastos: " />
                                <asp:TextBox ID="txt_codDistribucionGastos" runat="server" CssClass="form-control" AutoComplete="off"/>
                            </div>

                            <div class="col-lg-2">
                                <asp:Label ID="lblFacturaPosterior" runat="server" Text="Factura Posterior: " />
                                <asp:DropDownList ID="dd_fPosterior" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Si" Value="true" />
                                    <asp:ListItem Text="No" Value="false" />
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-3">
                                <asp:Label ID="lblGlosa" runat="server" Text="Glosa: " />
                                <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control" AutoComplete="off"/>
                            </div>


                            <!-- PAGOS -->
                            <div class="form_pagosDTO col-lg-10 border row mx-auto mt-2 mb-2">
                                <div class="container_tittle rounded">
                                    <asp:Label ID="lblPAGOS"  runat="server" Text="PAGOS" /><br>
                                </div>

                                <div class="col-lg-3">
                                    <asp:Label ID="lbltotalEfect" runat="server" Text="Total Efectivo: " />
                                    <asp:TextBox ID="txt_totalEfectivo" runat="server" CssClass="form-control" AutoComplete="off" />
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lbltotalCredito" runat="server" Text="Total credito: " />
                                    <asp:TextBox ID="txt_totalCredito" runat="server" CssClass="form-control" AutoComplete="off" /><br>
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lbltotalCheque" runat="server" Text="total cheque: " />
                                    <asp:TextBox ID="txt_totalCheques" runat="server" CssClass="form-control" AutoComplete="off" />
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="lbltotalDeposito" runat="server" Text="total deposito: " />
                                    <asp:TextBox ID="txt_totalDeposito" runat="server" CssClass="form-control" AutoComplete="off" />
                                </div>
                                <!-- {Sub credito} -->
                                <div class="form_creditoDTO border mx-auto border col-lg-6 row">
                                    <div class="container_tittle rounded mb-1">
                                        <asp:Label ID="lbl_tittleCredit" runat="server" Text="CREDITO" />
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label runat="server" >Tipo de cuenta:</asp:Label>
                                        <asp:TextBox ID="txt_tipoCuenta" runat="server" placeholder="" CssClass="form-control" AutoComplete="off" />
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label runat="server">Dias Credito:</asp:Label>
                                        <asp:TextBox ID="txt_DiasCredito" runat="server" placeholder="" CssClass="form-control" AutoComplete="off" />
                                    </div>
                                </div>
                            </div>
                            <!-- DATOS FACTURA -->
                            <div class="form_pagosDTO col-lg-10 border mx-auto mt-2 mb-2">
                                <div class="container_tittle rounded">
                                    <asp:Label ID="lblfacturacion"  runat="server" Text="Datos Factura" />
                                </div>
                                    <div class="facturaDTO mx-auto col-lg-6 row">
                                        <div class="col-lg-6" >
                                            <asp:Label ID="lbl_nit" runat="server" Text="NIT_CI: " />
                                            <asp:TextBox ID="txt_nit" runat="server" CssClass="form-control" AutoComplete="off"/>
                                            <asp:Label ID="lbl_razonsocial" runat="server" Text="Razon Social: " />
                                            <asp:TextBox ID="txt_razonSocial" runat="server" CssClass="form-control" AutoComplete="off"/>
                                            <asp:Label ID="lbl_nFactura" runat="server" Text="numero factura: " />
                                            <asp:TextBox ID="txt_nFactura" runat="server" CssClass="form-control" AutoComplete="off"/>
                                            <asp:Label ID="lblcodAutorizacion" runat="server" Text="codigo Autorizacion: " />
                                            <asp:TextBox ID="txt_codAutorizacion" runat="server" CssClass="form-control" AutoComplete="off"/>
                                            <asp:Label ID="lblcodControl" runat="server" Text="codigo control: " />
                                            <asp:TextBox ID="txt_codControl" runat="server" CssClass="form-control" AutoComplete="off"/>
                                            <br />                   
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Label ID="lblimpTotal" runat="server" Text="importe total: " />
                                            <asp:TextBox ID="txt_imptotal" runat="server" CssClass="form-control" AutoComplete="off"/>
                                            <asp:Label ID="lblimpDescuento" runat="server" Text="importe descuento: " />
                                            <asp:TextBox ID="txt_impDescuento" runat="server" CssClass="form-control" AutoComplete="off"/>
                                            <asp:Label ID="lblimpGift" runat="server" Text="ImporteGift: " />
                                            <asp:TextBox ID="txt_impGift" runat="server" CssClass="form-control" AutoComplete="off"/>
                                            <asp:Label ID="lblimpNeto" runat="server" Text="importe neto: " />
                                            <asp:TextBox ID="txt_impNeto" runat="server" CssClass="form-control" AutoComplete="off"/>
                                            <asp:Label runat="server" Text="lblapCredFiscal">Aplica Credito Fiscal: </asp:Label>
                                            <asp:DropDownList ID="dd_apCredFiscal" runat="server" CssClass="form-select">
                                                <asp:ListItem Text="Si" Value="true" />
                                                <asp:ListItem Text="No" Value="false" />
                                            </asp:DropDownList>

                                        </div>
                                      </div>
                            </div>
                            <!-- DetalleProductos -->
                            <div class="">

                                <div class="form_detproducto col-md-12">
                                    <h3 class="form-label">Detalle Productos</h3>
                                    <div class="table-detProducto">
                                        <table id="tblDetalleProductosIngresos" class="table table-bordered table-striped">
                                            <thead>
                                                <tr>
                                                    <th>Numero Item</th>
                                                    <th>Codigo Producto</th>
                                                    <th>Cantidad</th>
                                                    <th>Cod Unidad Medida</th>
                                                    <th>Precio Unitario</th>
                                                    <th>Importe Descuento</th>
                                                    <th>Porcentaje de Gasto</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <input class="form-control" type="text" name="numeroItem0" /></td>
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
                                    <br />
                                    <asp:Button ID="btnAddRow" runat="server" Text="Agregar Fila" CssClass="btn btn-warning" OnClientClick="addRowIngreso(); return false;" />
                                    <asp:Button ID="btn_registrarIngreso" runat="server" Text="Registrar Ingreso" CssClass="btn btn-warning" OnClick="btn_registrarIngreso_Click" />
                                    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                                </div>



                            </div>
                        </div>
                        </div>
   

                        <asp:Button ID="btn_registrarCompra" runat="server" Text="Enviar" OnClick="btn_registrarCompra_Click" /> <br /><br />

                    </div>
                </div>
            </div>
        </div>

</asp:Content>
