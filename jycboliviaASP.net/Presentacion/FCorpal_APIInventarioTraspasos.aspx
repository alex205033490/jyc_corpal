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
                    </div>
                    <br />


                    <!------------------------          API POST INVENTARIO TRASPASOS            ------------------------------>
                    <div class="panel-body">
                    <label>Formulario Registro Traspasos en Inventario - INVENTARIO TRASPASOS (F)</label>
                        <br/>
                        <!--
                        <asp:TextBox ID="txt_referencia" runat="server" placeholder="Referencia"></asp:TextBox>
                        <asp:TextBox ID="txt_codAlmacenDest" runat="server" placeholder="Codigo Almacen Dest" TextMode="Number"></asp:TextBox>
                        <asp:TextBox ID="txt_motMovimiento" runat="server" placeholder="Motivo Movimiento"></asp:TextBox>
                        <asp:TextBox ID="txt_itAnalisis" runat="server" placeholder="Item Analisis" TextMode="Number"></asp:TextBox>
                        <asp:TextBox ID="txt_glosa" runat="server" placeholder="Glosa"></asp:TextBox><br>
                        <label>- Detalle Productos -</label>
                        <br>
                        <asp:TextBox ID="txt_item" runat="server" placeholder="Item" TextMode="Number"></asp:TextBox>
                        <asp:TextBox ID="txt_codProducto" runat="server" placeholder="Codigo Producto"></asp:TextBox>
                        <asp:TextBox ID="txt_unMedida" runat="server" placeholder="Unidad Medida" TextMode="Number"></asp:TextBox>
                        <asp:TextBox ID="txt_cantidad" runat="server" placeholder="Cantidad" TextMode="Number"></asp:TextBox>
                        <br>

                        <asp:Button ID="btn_registrarTraspaso" runat="server" Text="Registrar Traspaso" />
                        <asp:Label ID="lblresult" runat="server" Text="Label"></asp:Label>
                                -->
                    </div>
                    <br>
                    <br>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
