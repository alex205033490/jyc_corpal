<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APIVentas.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIVentas" Async="true" MasterPageFile="~/PlantillaNew.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ApiUpon.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">                    
                    <!------------------------          API GET VENTAS        ------------------------------>
                    <div class="container-GETVentas p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Reporte de Ventas</h3>
                        </div>
                        <asp:Panel runat="server" DefaultButton="btn_getVentas">
                        <div class="container_input row mb-1">
                            <div class="col-9 col-sm-5 col-md-4 col-lg-4 mb-2">
                                <label class="form-label" for="TextBox2">Número de Venta:</label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Ingrese un código o deje vacío para mostrar todo." AutoComplete="off"></asp:TextBox>
            
                            </div>
                            <div class="container_btn col-3 col-sm-2 col-md-2 col-lg-1 d-flex align-items-end">
                                <asp:Button ID="btn_getVentas" runat="server" Text="Buscar" CssClass="btn btn-dark btn-sm" title="Ingrese un código o deje vacío para mostrar todos los registros" />
                            </div>
                        </div>
                            </asp:Panel>

                        <div class="container_gv4 col-xs-12 col-sm-12 col-md-12 col-lg-9 ">
                            <asp:GridView ID="gv_getVentas" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="NumeroVenta" HeaderText="Número Venta" SortExpression="numTrans" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="ref" />
                                    <asp:BoundField DataField="CodigoCliente" HeaderText="Codigo Cliente" SortExpression="alm" />
                                    <asp:BoundField DataField="ImporteTotal" HeaderText="Importe Total" SortExpression="usu" />
                                    <asp:BoundField DataField="NumeroFactura" HeaderText="Número Factura" SortExpression="usu" />
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


                    <!------------------------          API GET VENTAS DET       ------------------------------>
                    <div class="container-GETVentasDet p-4 rounded">

                        <div class="container_tittle">
                            <h3 class="text_tittle p-3">Reporte Detallado de Ventas</h3>
                        </div>
                        <asp:Panel runat="server" DefaultButton="btn_GETVentasDet">
                        <div class="container_input row mb-2">
                            <div class="col-9 col-sm-4 col-md-3 col-lg-3 mb-2">
                                <label class="form-label" for="TextBox1">Número de Venta:</label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="container_btn col-3 col-sm-2 col-md-2 col-lg-2 d-flex align-items-end">
                                <asp:Button ID="btn_GETVentasDet" runat="server" Text="Buscar" CssClass="btn btn-dark btn-sm"/>
                            </div>
                        </div>
                            </asp:Panel>

                        <div class="container_gv4 col-sm-12 col-md-12 col-lg-9 mb-1">
                            <asp:GridView ID="gv_GETVentaDet" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="NumeroVenta" HeaderText="Número Venta" SortExpression="numVenta" />
                                    <asp:BoundField DataField="NumeroPedido" HeaderText="Número Pedido" SortExpression="numPedido" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fecha" />
                                    <asp:BoundField DataField="CodigoCliente" HeaderText="Código Cliente" SortExpression="codCliente" />
                                    <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="refe" />
                                    <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="glosa" />
                                    <asp:BoundField DataField="EmitirFactura" HeaderText="Emitir Factura" SortExpression="emiFactura" />
                                    <asp:BoundField DataField="ImporteProductos" HeaderText="Importe Productos" SortExpression="impProductos" />
                                    <asp:BoundField DataField="ImporteDescuentos" HeaderText="Importe Descuentos" SortExpression="impDescuento" />
                                    <asp:BoundField DataField="ImporteTotal" HeaderText="Importe Total" SortExpression="impTotal" />
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
                            <asp:GridView ID="gv_DetalleProductos" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="NumeroItem" HeaderText="Numero Item" SortExpression="numItem" />
                                    <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo Producto" SortExpression="codProd" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="cant" />
                                    <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="Codigo Unidad Medida" SortExpression="codUM" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" SortExpression="precU" />
                                    <asp:BoundField DataField="ImporteDescuento" HeaderText="Importe Descuento" SortExpression="impDescuento" />
                                    <asp:BoundField DataField="ImporteTotal" HeaderText="Importe Total" SortExpression="impTotal" />
                                    <asp:BoundField DataField="NumeroItemOrigen" HeaderText="Numero Item Origen" SortExpression="numIOrigen" />
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
                   









































                                        
                    <br/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
