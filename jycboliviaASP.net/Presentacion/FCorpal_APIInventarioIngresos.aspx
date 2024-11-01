<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" Async="true" CodeBehind="FCorpal_APIInventarioIngresos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIInventarioIngresos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-11 col-md-offset-1">
                <div class="panel panel-success class">

                    <!------------------------          API GET INVENTARIO INGRESOS CON DETALLES          ------------------------------>
                    <div class="container-GETIIngreso p-4 rounded">

                        <div class="container_tittle">
                            <h3 class="text_tittle p-3">Vista Detalles de Inventario Ingresos</h3>
                        </div>

                        <div class="container_input row mb-2">
                            <div class="col-sm-8 col-md-6 col-lg-4 mb-2">
                                <label class="form-label" for="TextBox1">Numero de ingreso:</label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Ingrese un numero de ingreso"></asp:TextBox>
                            </div>
                            <div class="container_btn col-sm-4 col-md-6 col-lg-6 d-flex align-items-end">
                                <asp:Button ID="btn_InvIngresoGET" runat="server" Text="Buscar registro" CssClass="btn btn-info" OnClick="btn_InvIngresoGET_Click" />
                            </div>
                        </div>

                        <div class="container_gv1 col-sm-12 col-md-12 col-lg-9 mb-4">
                            <asp:GridView ID="gv_Inventario" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="NumeroIngreso" HeaderText="Numero Ingreso" SortExpression="numIng" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                    <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                    <asp:BoundField DataField="CodigoMoneda" HeaderText="Codigo Moneda" SortExpression="codMon" />
                                    <asp:BoundField DataField="CodigoAlmacen" HeaderText="Codigo Almacen" SortExpression="codAlm" />
                                    <asp:BoundField DataField="MotivoMovimiento" HeaderText="Motivo Movimiento" SortExpression="motMov" />
                                    <asp:BoundField DataField="ItemAnalisis" HeaderText="Item Analisis" SortExpression="itAnalis" />
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

                        <div class="container_gv2 col-sm-12 col-md-10 col-lg-8">
                            <asp:GridView ID="gv_DetalleProductos" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="Item" HeaderText="item" SortExpression="item" />
                                    <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo Producto" SortExpression="codProd" />
                                    <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" SortExpression="uMed" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="cantidad" SortExpression="cant" />
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
                    </div>



                    <br>

                    <!------------------------          API GET INVENTARIO INGRESOS C/S CRITERIO           ------------------------------>
                    <div class="form_getInventarioIngreso p-4 bg-light border rounded">
                        <h2 class="text_tittle2">Ver Inventario Ingresos General</h2>

                        <div class="container_input row mb-4">
                            <div class="col-sm-8 col-md-6 col-lg-6 mb-2">
                                <label class="form-label" for="TextBox2">Numero de transacción:</label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Ingrese un código o deje vacío para mostrar todos los registros."></asp:TextBox>
                            </div>
                            <div class="container_btn col-sm-4 col-md-6 col-lg-6 d-flex align-items-end">
                                <asp:Button ID="btn_invIngreso2" runat="server" Text="Buscar" CssClass="btn btn-warning" OnClick="btn_invIngreso2_Click" />
                            </div>

                        </div>

                        <div class="container_gv3 col-xs-12 col-sm-12 col-md-12 col-lg-9 ">

                            <asp:GridView ID="gv_invIngresos2" runat="server" CssClass="gridview" AutoGenerateColumns="false">
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
                    </div>
                    <br>

                    <!------------------------          API POST INVENTARIO INGRESOS            ------------------------------>


                    <div class="POST_inventarioIngreso p-4 bg-light border rounded">
                        <h5 class="text_tittle2">Registro de Inventario Ingreso</h5>

                        <div class="row mb-3 col-xs-12 col-sm-12 col-md-12 col-lg-12">

                            <div class="col-sm-6 col-md-5">
                                <label class="form-label">Referencia:</label>
                                <asp:TextBox ID="txt_Referencia" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                            </div>

                            <div class="col-sm-6 col-md-3">
                                <label class="form-label">Tipo de moneda:</label>
                                <asp:DropDownList ID="dd_codMoneda" runat="server" CssClass="custom-dropdown">
                                    <asp:ListItem Text="Bolivianos" Value="1" />
                                    <asp:ListItem Text="Dolares" Value="2" />
                                </asp:DropDownList>
                            </div>

                            <div class="col-sm-6 col-md-3">
                                <label class="form-label">Código Almacen:</label>
                                <asp:TextBox ID="txt_codAlmacen" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                            </div>

                            <div class="col-sm-6 col-md-4">
                                <label class="form-label" for="txt_motMovimiento">Motivo Movimiento:</label>
                                <asp:TextBox ID="txt_motMovimiento" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                            </div>

                            <div class="col-sm-6 col-md-3">
                                <label class="form-label" for="txt_itemAnalisis">Item Análisis:</label>
                                <asp:TextBox ID="txt_itemAnalisis" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>

                            <div class="col-sm-6 col-md-5">
                                <label class="form-label" for="txt_glosa">Glosa:</label>
                                <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>

                        <!-- DETALLE PRODUCTO -->

                        <br>
                        <div class="form_detproducto col-md-12">
                            <h3 class="form-label">Detalle Productos</h3>
                            <div class="table-detProducto">
                                <table id="tblDetalleProductosIngresos" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Codigo Producto</th>
                                            <th>Unidad Medida</th>
                                            <th>Cantidad</th>
                                            <th>Costo Unitario</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <input class="form-control" type="text" name="codigoProducto0" /></td>
                                            <td>
                                                <input class="form-control" type="number" name="unidadMedida0" /></td>
                                            <td>
                                                <input class="form-control" type="number" name="cantidad0" step="0.01" /></td>
                                            <td>
                                                <input class="form-control" type="number" name="costoUnitario0" step="0.01" /></td>
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

                <br>
                <br>
            </div>
        </div>
    </div>
    <script src="../js/jsApi.js" type="text/javascript"></script>
</asp:Content>

