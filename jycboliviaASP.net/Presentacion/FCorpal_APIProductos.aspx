<%@ Page Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_APIProductos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIProductos" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Container" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-11 col-md-offset-1">
                <div class="panel panel-success class">
                    

                    <!------------------------- GET PRODUCTOS/BUSCAR ------------------------->
                    <div class="container-GETProductosNombre p-4 rounded">
                        
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Búsqueda De Productos </h3>
                        </div>

                        <asp:Panel id="panel_busquedaProducto" runat="server" DefaultButton="btn_nomProducto">
                        <div class="mb-3 row">

                            <div class="col-7 col-sm-5 col-md-4 col-lg-3">
                                <label class="form-label" for="txt_nomProducto">Nombre del Producto:</label>
                                <asp:TextBox ID="txt_nomProducto" runat="server" CssClass="form-control" placeholder="Ingrese el nombre del producto" AutoComplete="off"></asp:TextBox>
                            </div>

                            <div class="col-3 col-sm-3 col-md-2 col-lg-2 d-flex align-items-end">
                                <asp:Button ID="btn_nomProducto" runat="server" Text="Buscar" CssClass="btn btn-dark" OnClick="btn_buscarProdNombre_Click" />
                            </div>

                        </div>
                            </asp:Panel>


                        <div class="container_gv2">
                            <asp:GridView ID="gv_prodNombre" runat="server" CssClass="gridview table-hover" AutoGenerateColumns="false">
                                
                                <Columns>
                                    <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" SortExpression="cod" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                    <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                    <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="Cod Unidad Medida" SortExpression="codUniMedida" />
                                    <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" SortExpression="UniMedida" />
                                    <asp:BoundField DataField="UnidadMedidaAbreviatura" HeaderText="Abreviatura" SortExpression="Abrev" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" SortExpression="PrecUnitario" />
                                    <asp:BoundField DataField="DescuentosPermitido" HeaderText="Descuento Permitido" SortExpression="Descuento"/>
                                    <asp:BoundField DataField="CostoUnitario" HeaderText="Costo Unitario" SortExpression="CosUnitario"/>
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
                    </div>
                    <br />

                    <!------------------------- GET PRODUCTOS/VENTAS/BUSCAR ------------------------->
                    <div class="container-GETProductosVentas p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Vista Ventas de Productos</h3>
                        </div>

                        <asp:Panel id="panel_vwVentasProductos" runat="server" DefaultButton="btn_nomProducto">
                            

                        <div class="mb-3 row col-lg-12">
                            <div class="col-8 col-sm-6 col-md-4 col-lg-4">
                                <label class="form-label" for="txt_ventProducto">Nombre del Producto:</label>
                                <asp:TextBox ID="txt_ventProducto" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Ingrese el nombre del producto"></asp:TextBox>
                            </div>
                            <div class="col-4 col-sm-4 col-md-3 col-lg-3 d-flex align-items-end">
                                <asp:Button ID="btn_ventProducto" runat="server" Text="Buscar" CssClass="btn btn-dark" OnClick="btn_BuscarventProducto_Click" />
                            </div>
                            <input type="image" class="close-button banner-element" name="close-button" alt="text" src="http://localhost:58557/49dc35553eb24a4b92bbcb4af4f83b26/images/CloseButtonHover_Dark">
                        </div>
                            </asp:Panel>


                        <div class="container_gv2">
                            <asp:GridView ID="gv_prodVenta" runat="server" CssClass="gridview table-hover" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" SortExpression="cod" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                    <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                    <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="Cod Unidad Medida" SortExpression="codUniMedida" />
                                    <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" SortExpression="UniMedida" />
                                    <asp:BoundField DataField="UnidadMedidaAbreviatura" HeaderText="Abreviatura" SortExpression="Abrev" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" SortExpression="PrecUnitario" />
                                    <asp:BoundField DataField="DescuentosPermitido" HeaderText="Descuento Permitido" SortExpression="Descuento"/>
                                    <asp:BoundField DataField="CostoUnitario" HeaderText="Costo Unitario" SortExpression="CosUnitario"/>
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

                    </div><br />

                    <!------------------------- GET PRODUCTOS/COMPRAS/BUSCAR ------------------------->
                    <div class="container-GETProductosCompras p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Vista Compras de Productos</h3>
                        </div>

                        <asp:Panel id="panel_vwcComprasProductos" runat="server" DefaultButton="btn_buscarCompras">

                        <div class="mb-2 row col-lg-12">
                            <div class="col-sm-5 col-md-4 col-lg-3">
                                <label class="form-label" for="txt_codProductoComp">Codigo del producto:</label>
                                <asp:TextBox ID="txt_codProductoComp" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Ingrese codigo de producto"></asp:TextBox>
                            </div>

                            <div class=" col-sm-5 col-md-4 col-lg-3 mb-1">
                                <label class="form-label" for="txt_codProveedorComp">Codigo del Proveedor:</label>
                                <asp:TextBox ID="txt_codProveedorComp" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Ingrese codigo de proveedor"></asp:TextBox>
                            </div>

                            <div class="col-sm-2 col-md-3 col-lg-2 d-flex align-items-end">
                                <asp:Button ID="btn_buscarCompras" runat="server" Text="Buscar" CssClass="btn btn-dark" OnClick="btn_buscarCompras_Click" />
                            </div>
                        </div>
                            </asp:Panel>

                        <div class="container_gv1">
                            <asp:GridView ID="gv_prodCompras" runat="server" CssClass="gridview table-hover" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" SortExpression="cod" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                    <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                    <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="Codigo Medida" SortExpression="codUniMedida" />
                                    <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" SortExpression="UniMedida" />
                                    <asp:BoundField DataField="UnidadMedidaAbreviatura" HeaderText="Abreviatura" SortExpression="Abrev" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" SortExpression="PrecUnitario" />
                                    <asp:BoundField DataField="DescuentosPermitido" HeaderText="Descuento" SortExpression="Descuento"/>
                                    <asp:BoundField DataField="CostoUnitario" HeaderText="Costo Unitario" SortExpression="CosUnitario"/>
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
                    </div><br />


                    <!-- GET PRODUCTOS/codProducto  -->
                    <div class="container-GETProductosCodigo p-4 rounded">
                        <div class="container_tittle rounded mb-3">
                            <h3 class="text-tittle p-3">Búsqueda de Productos por Codigo</h3>
                        </div>

                        <asp:Panel id="panel_vwCodProductos" runat="server" DefaultButton="btn_BuscarcodProducto">
                        <div class="mb-3 row col-lg-10">
                            <div class="col-sm-5 col-md-4 col-lg-4">
                                <label class="form-label" for="txt_codProducto">Codigo del Producto:</label>
                                <asp:TextBox ID="txt_codProducto" runat="server" CssClass="form-control" placeholder="Ingrese codigo producto" AutoComplete="off"></asp:TextBox>
                            </div>

                            <div class="col-sm-4 col-md-3 col-lg-3 d-flex align-items-end">
                                <asp:Button ID="btn_BuscarcodProducto" runat="server" Text="Buscar Producto" CssClass="btn btn-dark" OnClick="btn_BuscarcodProducto_Click" />
                            </div>
                        </div>
                            </asp:Panel>


                        <div class="container_gv1" >
                            <asp:GridView ID="gv_prodCod" runat="server" CssClass="gridview table-hover" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" SortExpression="CodProd" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descrip" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" SortExpression="PrecUnit" />
                                    <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                    <asp:BoundField DataField="DescuentosPermitido" HeaderText="Descuento Permitido" SortExpression="DescPermitido" />
                                    <asp:BoundField DataField="CodigoMoneda" HeaderText="Codigo Moneda" SortExpression="CodMoneda" />
                                    <asp:BoundField DataField="EsFraccionado" HeaderText="EsFraccionado" SortExpression="EsFracc"/>
                                    <asp:BoundField DataField="Categoria" HeaderText="Categoria" SortExpression="Categ"/>
                                    <asp:BoundField DataField="Grupo" HeaderText="Grupo" SortExpression="Grupo" />
                                    <asp:BoundField DataField="SubGrupo" HeaderText="SubGrupo" SortExpression="SGrupo" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca"/>
                                    <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" SortExpression="UMedida"/>

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
                            <asp:GridView ID="gv_prodCodDet" runat="server" CssClass="gridview table-hover" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="codigo Medida" SortExpression="cod" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Nombre" />
                                    <asp:BoundField DataField="Abreviatura" HeaderText="Abreviatura" SortExpression="Stock" />
                                    <asp:BoundField DataField="CantidadRelacion" HeaderText="Cantidad Relacion" SortExpression="codUniMedida" />
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
                    </div><br />

                </div>
            </div>
        </div>
    </div>
</asp:Content>
