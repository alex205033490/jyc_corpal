<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" Async="true" CodeBehind="FCorpal_APIInventarioEgresos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIInventarioEgresos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-10" style="padding-top: 1em;">
        <div class="row">
            <div class="col-lg-12 col-md-offset-1">
                <div class="panel panel-success class">

                    <!------------------------          API GET INVENTARIO EGRESO CON DETALLES          ------------------------------>
                    <div class="container-GETIEgresoDet p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Informe Detallado de Egresos de Inventario</h3>
                        </div>

                        <asp:Panel runat="server" DefaultButton="BuscarEgresoInventarioDetalle">
                            <div class="container_input row mb-4">

                                <div class="col-sm-8 col-md-6 col-lg-4 mb-2">
                                    <label class="form-label" for="TextBox1">Número de Egreso:</label>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Ingrese un número de egreso" AutoComplete="off"></asp:TextBox>
                                </div>
                                <div class="container_btn col-sm-4 col-md-6 col-lg-6 d-flex align-items-end">
                                    <asp:Button ID="BuscarEgresoInventarioDetalle" autocomplete="off" runat="server" Text="Buscar Registro" CssClass="btn btn-dark" OnClick="BuscarEgresoInventarioDetalle_Click" title="Deje el campo vacío para mostrar todos los registros o ingrese un número" />
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:UpdatePanel ID="updatePanelGet_IED" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="container_gv4 col-sm-12 col-md-12 col-lg-9">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="NumeroEgreso" HeaderText="Numero Egreso" SortExpression="numEgr" />
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                            <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                            <asp:BoundField DataField="CodigoAlmacen" HeaderText="Codigo Almacen" SortExpression="cAlm" />
                                            <asp:BoundField DataField="MotivoMovimiento" HeaderText="Motivo Movimiento" SortExpression="mMov" />
                                            <asp:BoundField DataField="ItemAnalisis" HeaderText="Item Analisis" SortExpression="iAnali" />
                                            <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="glo" />
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


                                <div class="container_gv1 col-sm-12 col-md-10 col-lg-8">
                                    <asp:GridView ID="GridView2" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Item" HeaderText="item" SortExpression="item" />
                                            <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo Producto" SortExpression="codProd" />
                                            <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" SortExpression="uMed" />
                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="cant" />
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
                                <asp:AsyncPostBackTrigger ControlID="BuscarEgresoInventarioDetalle" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <br />

                    <!------------------------          API GET INVENTARIO EGRESO C/S CRITERIO        ------------------------------>
                    <div class="container-GETIEgreso p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Informe de Egresos de Inventario</h3>
                        </div>
                        <asp:Panel runat="server" DefaultButton="btn_BuscarEgresoInventario">
                            <div class="container_input row mb-4">
                                <div class="col-sm-8 col-md-6 col-lg-4 mb-2">
                                    <label class="form-label" for="TextBox2">Número de transacción:</label>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Ingrese un código o deje vacio." AutoComplete="off"></asp:TextBox>
                                </div>
                                <div class="container_btn col-sm-4 col-md-6 col-lg-6 d-flex align-items-end">
                                    <asp:Button ID="btn_BuscarEgresoInventario" runat="server" Text="Buscar" CssClass="btn btn-dark" OnClick="BuscarEgresoInventario_Click" />
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:UpdatePanel ID="updatePanelGet_IE" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="container_gv3 col-xs-12 col-sm-12 col-md-12 col-lg-9">
                                    <asp:GridView ID="GridView3" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="NumeroTransaccion" HeaderText="Numero de Transacción" SortExpression="numTrans" />
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                            <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                            <asp:BoundField DataField="Almacen" HeaderText="Almacen" SortExpression="alm" />
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
                                <asp:AsyncPostBackTrigger ControlID="btn_BuscarEgresoInventario" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <br />

                    <!------------------------          API POST INVENTARIO EGRESO            ------------------------------>

                    <div class="container-POSTIEgreso p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Registro de Egresos en Inventario</h3>
                        </div>

                        <asp:Panel runat="server" DefaultButton="btn_InventarioEgresoPost2">
                            <asp:UpdatePanel ID="updatePanelPost_IE" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row mb-3 col-xs-12 col-sm-12 col-ms-12 col-lg-12">

                                        <div class="col-6 col-sm-6 col-md-4 col-lg-4">
                                            <label class="form-label">Referencia:</label>
                                            <asp:TextBox ID="TextBoxReferencia" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Opcional"></asp:TextBox>
                                        </div>
                                        <div class="col-6 col-sm-6 col-md-3 col-lg-4">
                                            <label class="form-label">Almacen:</label>
                                            <asp:DropDownList ID="dd_codAlmacenIEgreso" runat="server" CssClass="form-select dd_fsmall"></asp:DropDownList>
                                        </div>

                                        <div class="col-6 col-sm-6 col-md-3 col-lg-3">
                                            <label class="form-label">Motivo Movimiento:</label>
                                            <asp:DropDownList ID="dd_motMoviIEgreso" runat="server" CssClass="form-select dd_fsmall"></asp:DropDownList>
                                        </div>
                                        <div class="col-6 col-sm-6 col-md-3 col-lg-3">
                                            <label class="form-label">Ítem Análisis:</label>
                                            <asp:TextBox ID="TextBoxItemAnalisis" runat="server" CssClass="form-control" Text="0" AutoComplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-6 col-sm-6 col-md-4 col-lg-3">
                                            <label class="form-label">Glosa:</label>
                                            <asp:TextBox ID="TextBoxGlosa" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Opcional"></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btn_InventarioEgresoPost2" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>


                            <!-- DETALLE PRODUCTO -->

                            <div class="form_detproducto col-md-12 col-lg-10">
                                <h3 class="form-label">Detalle Productos</h3>

                                <asp:UpdatePanel ID="updatePanelPost_IEDetProd" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" DefaultButton="btn_addProd">
                                            <div class="form_addProducto row mb-3">
                                                <div class="input_producto col-6">
                                                    <asp:Label runat="server"> Producto:</asp:Label>
                                                    <asp:TextBox ID="txt_producto" runat="server" OnTextChanged="txt_producto_TextChanged" AutoPostBack="true" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                                </div>

                                                <div class="input_cantidad col-3">
                                                    <asp:Label runat="server"> Cantidad:</asp:Label>
                                                    <asp:TextBox ID="txt_cantProducto" runat="server" CssClass="form-control" AutoComplete="off" oninput="convertCommaToDot(event);"></asp:TextBox>
                                                </div>
                                                <div class="container_btnAddProd col-3 d-flex align-items-end">
                                                    <asp:Button runat="server" ID="btn_addProd" Text="Agregar" CssClass="btn btn-success" OnClick="btn_addProd_Click" />
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:GridView ID="gv_listProdIngresos" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClass="table table-bordered" OnSelectedIndexChanged="gv_listProdIngresos_SelectedIndexChanged">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Producto" HtmlEncode="false" />
                                                <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" />
                                                <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="UM" />

                                            </Columns>
                                        </asp:GridView>

                                        <div class="container_gvProdAddII mb-3">
                                            <asp:GridView ID="gv_productAgregadosIE" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClss="table table-bordered" OnRowCommand="gv_productAgregados">
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
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_InventarioEgresoPost2" EventName="Click" />
                                    </Triggers>

                                </asp:UpdatePanel>

                            </div>

                            <div class="form_btn col-lg-6 align-content-center">
                                <asp:Button ID="btn_InventarioEgresoPost2" runat="server" Text="Registrar Egreso" CssClass="btn btn-success" OnClick="btn_InventarioEgresoPost2_Click" />
                            </div>
                        </asp:Panel>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../js/jsApi.js" type="text/javascript"></script>
</asp:Content>
