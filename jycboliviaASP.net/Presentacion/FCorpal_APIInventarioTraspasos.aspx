<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" Async="true" CodeBehind="FCorpal_APIInventarioTraspasos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIInventarioTraspasos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-10" style="padding-top: 1em;">
        <div class="row">
            <div class="col-lg-12 col-md-offset-1">
                <div class="panel panel-success class">

                    <!------------------------          API GET INVENTARIO TRASPASO            ------------------------------>
                    <div class="container-GETITraspaso p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Reporte de Traspasos en Inventario</h3>
                        </div>

                        <div class="form_input row mb-3 col-lg-12">
                            <div class="col-6 col-sm-6 col-md-6 col-lg-4">
                                <label class="form-label">Número de transacción:</label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" autoComplete="off" placeholder="Ingrese un número de transacción o deje vacio"></asp:TextBox>
                            </div>
                            <div class="col-3 col-sm-2 col-md-2 col-lg-2 d-flex align-items-end">
                                <asp:Button ID="btn_GetinvTraspaso" runat="server" Text="Buscar" CssClass="btn btn-dark" OnClick="btn_GetinvTraspaso_Click" />
                            </div>
                        </div>

                        <asp:UpdatePanel ID="updatePanelGet_IT" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="container_gv1">
                                    <asp:GridView ID="gv_invTraspaso" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="NumeroTransaccion" HeaderText="Número De Transacción" SortExpression="numTrans" />
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
                                <asp:AsyncPostBackTrigger ControlID="btn_GetinvTraspaso" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <br />

                    <!------------------------          API GET INVENTARIO TRASPASO DETALLE           ------------------------------>
                    <div class="container-GETITraspasoDet p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Reporte Detallado de Traspasos en Inventario</h3>
                        </div>

                        <div class="form_input mb-3 row">
                            <div class="col-6 col-sm-6 col-md-6 col-lg-4">
                                <label class="form-label" for="TextBox2">Número de transacción:</label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Ingrese un número de transacción"></asp:TextBox>
                            </div>
                            <div class="col-3 col-sm-2 col-md-2 col-lg-3 d-flex align-items-end">
                                <asp:Button ID="btn_GetinvTraspasoDet" runat="server" Text="Buscar" CssClass="btn btn-dark" OnClick="btn_GetinvTraspasoDet_Click" />
                            </div>
                        </div>

                        <asp:UpdatePanel ID="updatePanelGet_ITDet" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="container_gv4">
                                    <asp:GridView ID="gv_invTraspasoDet" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="NumeroTraspasos" HeaderText="Número Traspaso" SortExpression="numTra" />
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                            <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                            <asp:BoundField DataField="CodigoAlmacenDestino" HeaderText="Código Almacen Destino" SortExpression="cAlmDest" />
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

                                <div class="container_gv2">
                                    <asp:GridView ID="gv_invTraspasoDet2" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Item" HeaderText="Ítem" SortExpression="item" />
                                            <asp:BoundField DataField="CodigoProducto" HeaderText="Código Producto" SortExpression="codProd" />
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
                                <asp:AsyncPostBackTrigger ControlID="btn_GetinvTraspasoDet" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <br />


                    <!------------------------          API POST INVENTARIO TRASPASOS            ------------------------------>
                    <div class="container-POSTITraspaso p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Traspaso de Inventario entre Almacenes (F)</h3>
                        </div>

                        <asp:UpdatePanel ID="updatePanelPost_IT" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel runat="server" DefaultButton="btn_registrarTraspaso">
                                    <div class="row mb-1 col-xs-12 col-sm-12 col-md-12 col-lg-12">
     
                                        <div class="col-6 col-sm-6 col-md-4">
                                            <label class="form-label">Referencia:</label>
                                            <asp:TextBox ID="txt_Referencia" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Opcional"></asp:TextBox>                                         
                                        </div>

                                        <div class="col-6 col-sm-6 col-md-4">
                                            <label class="form-label">Almacén Destino:</label>
                                            <asp:DropDownList ID="dd_codAlmacenDestino" runat="server" CssClass="custom-dropdown">
                                            </asp:DropDownList>
                                        </div>
                                        
                                        <div class="col-6 col-sm-6 col-md-4">
                                            <label class="form-label" for="txt_glosa">Glosa:</label>
                                            <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control" autocomplete="off" placeholder="Opcional"></asp:TextBox>                                       
                                        </div>

                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_registrarTraspaso" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />

                        <!-- DETALLE PRODUCTO -->

                        <div class="form_detproducto col-md-12 col-lg-10">
                            <h3 class="form-label">Detalle Productos</h3>

                            <asp:UpdatePanel ID="updatePanelPost_ITDetProd" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel runat="server" DefaultButton="btn_addProd">
                                        <div class="form_addProducto row mb-3">


                                            <div class="input_producto col-6 mb-2">
                                                <asp:Label runat="server">Producto:</asp:Label>
                                                <asp:TextBox ID="txt_producto" runat="server" OnTextChanged="txt_producto_TextChanged" AutoPostBack="true" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>


                                            <div class="input_cantidad col-3 mb-2">
                                                <asp:Label runat="server">Cantidad:</asp:Label>
                                                <asp:TextBox ID="txt_cantProducto" runat="server" CssClass="form-control" AutoComplete="off" oninput="convertdotcomma(event);"></asp:TextBox>
                                            </div>
                                            <div class="container_btnAddProd col-3 d-flex align-items-end mb-2">
                                                <asp:Button runat="server" ID="btn_addProd" Text="Agregar" CssClass="btn btn-success" OnClick="btn_addProd_Click" />
                                            </div>
                                        </div>
                                    </asp:Panel>


                                    <asp:GridView ID="gv_listProdTraspaso" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClass="table table-bordered" OnSelectedIndexChanged="gv_listProdTraspaso_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                            <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" HtmlEncode="false" />
                                            <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="Unidad Medida" />
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
                                                <asp:BoundField DataField="producto" HeaderText="Producto" />
                                                <asp:BoundField DataField="unidadMedida" HeaderText="Unidad medida" />
                                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btn_registrarTraspaso" EventName="Click" />
                                </Triggers>

                            </asp:UpdatePanel>

                            <div>
                                <asp:Button ID="btn_registrarTraspaso" runat="server" Text="Registrar Traspaso" CssClass="btn btn-success" OnClick="btn_registrarTraspaso_Click" />

                            </div>
                        </div>
                    </div>

                    <br>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
