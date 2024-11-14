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
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Ingrese un código o deje vacío para mostrar todo."></asp:TextBox>
                                
                            </div>
                            <div class="container_btn col-3 col-sm-2 col-md-2 col-lg-1 d-flex align-items-end">
                                <asp:Button ID="btn_invIngreso2" runat="server" Text="Buscar" CssClass="btn btn-dark btn-sm" OnClick="btn_invIngreso2_Click"  title="Ingrese un código o deje vacío para mostrar todos los registros" />
                            </div>
                        </div>
                            </asp:Panel>

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
                    </div>
                    <br />

                    <!------------------------          API POST INVENTARIO INGRESOS            ------------------------------>

                    <div class="container-POSTIIngreso p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Registro de Ingresos en Inventario</h3>
                        </div>

                        <asp:Panel runat="server" DefaultButton="btn_registrarIngreso">
                        <div class="row mb-1 col-xs-12 col-sm-12 col-md-12 col-lg-12">

                            <div class="col-6 col-sm-6 col-md-4 col-lg-4">
                                <label class="form-label">Referencia:</label>
                                <asp:TextBox ID="txt_Referencia" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Opcional"></asp:TextBox>

                                <label class="form-label">Tipo de moneda:</label>
                                <asp:DropDownList ID="dd_codMoneda" runat="server" CssClass="custom-dropdown">
                                    <asp:ListItem Text="Bolivianos" Value="1" />
                                    <asp:ListItem Text="Dólares" Value="2" />
                                </asp:DropDownList>
                                <label class="form-label">Código Almacén:</label>
                                <asp:TextBox ID="txt_codAlmacen" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                            </div>

                            <div class="col-6 col-sm-6 col-md-4 col-lg-4">
                                <label class="form-label" for="txt_motMovimiento">Motivo Movimiento:</label>
                                <asp:TextBox ID="txt_motMovimiento" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                <label class="form-label" for="txt_itemAnalisis">Ítem Análisis:</label>
                                <asp:TextBox ID="txt_itemAnalisis" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                <label class="form-label" for="txt_glosa">Glosa:</label>
                                <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control" autocomplete="off" placeholder="Opcional"></asp:TextBox>
                            </div>

                        </div>
                            </asp:Panel>
                        <br />

                        <!-- DETALLE PRODUCTO -->

                        <div class="form_detproducto col-md-12 col-lg-10">
                            <h3 class="form-label">Detalle Productos</h3>

                            <asp:Panel runat="server" DefaultButton="btnAddRow">
                            <div class="table-detProducto">
                                <table id="tblDetalleProductosIngresos" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Código Producto</th>
                                            <th>Unidad Medida</th>
                                            <th>Cantidad</th>
                                            <th>Costo Unitario</th>
                                            <th>Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <input class="form-control" type="text" name="codigoProducto0" AutoComplete="off"/></td>
                                            <td>
                                                <input class="form-control" type="number" name="unidadMedida0" /></td>
                                            <td>
                                                <input class="form-control" type="number" name="cantidad0" step="0.01"/></td>
                                            <td>
                                                <input class="form-control" type="number" name="costoUnitario0" step="0.01"/></td>
                                            <td>
                                                <asp:Button ID="btnAddRow" runat="server" Text="Agregar Fila" CssClass="btn btn-success" OnClientClick="addRowIngreso(); return false;" /></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                                </asp:Panel>
                            <br />
                            <asp:Button ID="btn_registrarIngreso" runat="server" Text="Registrar Ingreso" CssClass="btn btn-success" OnClick="btn_registrarIngreso_Click" />
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
    <script src="../js/jsApi.js" type="text/javascript"></script>
</asp:Content>

