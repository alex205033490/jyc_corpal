<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" Async="true" CodeBehind="FCorpal_APIInventarioIngresos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIInventarioIngresos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-10" style="padding-top: 1em;">
        <div class="row">
            <div class="col-lg-12 col-md-offset-1">
                <div class="panel panel-success class">

                    <!------------------------          API GET INVENTARIO INGRESOS CON DETALLES          ------------------------------>
                    <div class="container-GETIIngresoDet p-4 rounded">

                        <div class="container_tittle">
                            <h3 class="text_tittle p-3">Reporte Detallado de Ingresos en Inventario</h3>
                        </div>
                        <asp:Panel runat="server" DefaultButton="btn_InvIngresoGET">
                            <div class="container_input row mb-2">
                                <div class="col-9 col-sm-4 col-md-3 col-lg-3 mb-2">
                                    <label class="form-label" for="TextBox1">Número de ingreso:</label>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                                <div class="container_btn col-3 col-sm-2 col-md-2 col-lg-2 d-flex align-items-end">
                                    <asp:Button ID="btn_InvIngresoGET" runat="server" Text="Buscar" CssClass="btn btn-dark btn-sm" OnClick="btn_InvIngresoGET_Click" />
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:UpdatePanel ID="updatePanelGet_IID" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="container_gv4 col-sm-12 col-md-12 col-lg-9 mb-1">
                                    <asp:GridView ID="gv_Inventario" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="NumeroIngreso" HeaderText="Número Ingreso" SortExpression="numIng" />
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                            <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                            <asp:BoundField DataField="CodigoMoneda" HeaderText="Código Moneda" SortExpression="codMon" />
                                            <asp:BoundField DataField="CodigoAlmacen" HeaderText="Código Almacén" SortExpression="codAlm" />
                                            <asp:BoundField DataField="MotivoMovimiento" HeaderText="Motivo Movimiento" SortExpression="motMov" />
                                            <asp:BoundField DataField="ItemAnalisis" HeaderText="Ítem Análisis" SortExpression="itAnalis" />
                                            <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="glosa" />
                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usu" />
                                        </Columns>
                                        <AlternatingRowStyle CssClass="alternating-row" />
                                        <FooterStyle CssClass="footer" />
                                        <HeaderStyle CssClass="header" />
                                        <PagerStyle CssClass="pager" />
                                        <SelectedRowStyle CssClass="selected-row" />
                                        <SortedAscendingCellStyle CssClass="sorted-asc-cell" />
                                        <SortedAscendingHeaderStyle CssClass="sorted-asc-header" />
                                        <SortedDescendingCellStyle CssClass="sorted-desc-cell" />
                                        <SortedDescendingHeaderStyle CssClass="sorted-desc-header" />
                                    </asp:GridView>
                                </div>

                                <div class="container_gv1 col-sm-12 col-md-10 col-lg-8">
                                    <asp:GridView ID="gv_DetalleProductos" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Item" HeaderText="Ítem" SortExpression="item" />
                                            <asp:BoundField DataField="CodigoProducto" HeaderText="Código Producto" SortExpression="codProd" />
                                            <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" SortExpression="uMed" />
                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="cant" />
                                            <asp:BoundField DataField="CostoUnitario" HeaderText="Costo Unitario" SortExpression="costUnit" />
                                            <asp:BoundField DataField="CostoTotal" HeaderText="Costo Total" SortExpression="costTotal" />
                                        </Columns>

                                        <AlternatingRowStyle CssClass="alternating-row" />
                                        <FooterStyle CssClass="footer" />
                                        <HeaderStyle CssClass="header" />
                                        <PagerStyle CssClass="pager" />
                                        <SelectedRowStyle CssClass="selected-row" />
                                        <SortedAscendingCellStyle CssClass="sorted-asc-cell" />
                                        <SortedAscendingHeaderStyle CssClass="sorted-asc-header" />
                                        <SortedDescendingCellStyle CssClass="sorted-desc-cell" />
                                        <SortedDescendingHeaderStyle CssClass="sorted-desc-header" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_InvIngresoGET" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                    <br />


                    <!------------------------          API GET INVENTARIO INGRESOS C/S CRITERIO           ------------------------------>
                    <div class="container-GETIIngreso p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Reporte de Ingresos en Inventario</h3>
                        </div>
                        <asp:Panel runat="server" DefaultButton="btn_invIngreso2">
                            <div class="container_input row mb-1">
                                <div class="col-9 col-sm-5 col-md-4 col-lg-4 mb-2">
                                    <label class="form-label" for="TextBox2">Número de transacción:</label>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Ingrese un código o deje vacío para mostrar todo." AutoComplete="off"></asp:TextBox>

                                </div>
                                <div class="container_btn col-3 col-sm-2 col-md-2 col-lg-1 d-flex align-items-end">
                                    <asp:Button ID="btn_invIngreso2" runat="server" Text="Buscar" CssClass="btn btn-dark btn-sm" OnClick="btn_invIngreso2_Click" title="Ingrese un código o deje vacío para mostrar todos los registros" />
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:UpdatePanel ID="updatePanelGet_II" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="container_gv4 col-xs-12 col-sm-12 col-md-12 col-lg-9 ">
                                    <asp:GridView ID="gv_invIngresos2" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="NumeroTransaccion" HeaderText="Número de Transacción" SortExpression="numTrans" />
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                            <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                            <asp:BoundField DataField="Almacen" HeaderText="Almacén" SortExpression="alm" />
                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="usu" />
                                        </Columns>
                                        <AlternatingRowStyle CssClass="alternating-row" />
                                        <FooterStyle CssClass="footer" />
                                        <HeaderStyle CssClass="header" />
                                        <PagerStyle CssClass="pager" />
                                        <SelectedRowStyle CssClass="selected-row" />
                                        <SortedAscendingCellStyle CssClass="sorted-asc-cell" />
                                        <SortedAscendingHeaderStyle CssClass="sorted-asc-header" />
                                        <SortedDescendingCellStyle CssClass="sorted-desc-cell" />
                                        <SortedDescendingHeaderStyle CssClass="sorted-desc-header" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_invIngreso2" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <br />

                    <!------------------------          API POST INVENTARIO INGRESOS            ------------------------------>

                    <div class="container-POSTIIngreso p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Registro de Ingresos en Inventario</h3>
                        </div>

                        <asp:UpdatePanel ID="updatePanelPost_II" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel runat="server" DefaultButton="btn_registrarIngreso">
                                    <div class="row mb-1 col-xs-12 col-sm-12 col-md-12 col-lg-12">

                                        <div class="col-6 col-sm-6 col-md-4 col-lg-4">
                                            <label class="form-label">Referencia:</label>
                                            <asp:TextBox ID="txt_Referencia" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Opcional"></asp:TextBox>

                                            <label class="form-label">Tipo de moneda:</label>
                                            <asp:DropDownList ID="dd_codMoneda" runat="server" CssClass="custom-dropdown dd_fsmall">
                                                <asp:ListItem Text="Bolivianos" Value="1" />
                                                <asp:ListItem Text="Dólares" Value="2" />
                                                <asp:ListItem Text="Unidad Fomento Vivienda" Value="3"/>
                                            </asp:DropDownList>
                                            <label class="form-label">Código Almacén:</label>
                                            <asp:DropDownList ID="dd_CodAlmacenIIngreso" runat="server" CssClass="form-select dd_fsmall"></asp:DropDownList>
                                        </div>

                                        <div class="col-6 col-sm-6 col-md-4 col-lg-4">
                                            <label class="form-label" for="txt_motMovimiento">Motivo Movimiento:</label>
                                            <asp:DropDownList ID="dd_motMovI" runat="server" CssClass="form-select dd_fsmall"></asp:DropDownList>
                                            <label class="form-label" for="txt_itemAnalisis">Ítem Análisis:</label>
                                            <asp:TextBox ID="txt_itemAnalisis" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            <label class="form-label" for="txt_glosa">Glosa:</label>
                                            <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control" autocomplete="off" placeholder="Opcional"></asp:TextBox>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_registrarIngreso" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />

                        <!-- DETALLE PRODUCTO -->

                        <div class="form_detproducto col-md-12 col-lg-10">
                            <h3 class="form-label">Detalle Productos</h3>

                            <asp:UpdatePanel ID="updatePanelPost_IIDetProd" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel runat="server" DefaultButton="btn_addProd">
                                        <div class="form_addProducto row mb-3">


                                            <div class="input_producto col-5">
                                                <asp:Label runat="server"> Nombre del producto:</asp:Label>
                                                <asp:TextBox ID="txt_producto" runat="server" OnTextChanged="txt_producto_TextChanged" AutoPostBack="true" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>


                                            <div class="input_cantidad col-3">
                                                <asp:Label runat="server"> Cantidad:</asp:Label>
                                                <asp:TextBox ID="txt_cantProducto" runat="server" CssClass="form-control" AutoComplete="off" oninput="convertdotcomma(event);"></asp:TextBox>
                                            </div>
                                            <div class="container_btnAddProd col-3 d-flex align-items-end">
                                                <asp:Button runat="server" ID="btn_addProd" Text="Agregar" CssClass="btn btn-success" OnClick="btn_addProd_Click" />
                                            </div>
                                        </div>
                                    </asp:Panel>


                                    <asp:GridView ID="gv_listProdIngresos" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClass="table table-bordered" OnSelectedIndexChanged="gv_listProdIngresos_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" HtmlEncode="false"/>
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
                                        <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                                        <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad medida" />
                                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                        <asp:BoundField DataField="CostoUnitario" HeaderText="Costo Unitario" />
                                        <asp:BoundField DataField="CostoTotal" HeaderText="Costo Total" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                                    </ContentTemplate>
                                    <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btn_registrarIngreso" EventName="Click" />
                                </Triggers>

                            </asp:UpdatePanel>

                            <div>
                                <asp:Button ID="btn_registrarIngreso" runat="server" Text="Registrar Ingreso" CssClass="btn btn-success" OnClick="btn_registrarIngreso_Click" />

                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
    <script src="../js/jsApi.js" type="text/javascript"></script>
</asp:Content>

