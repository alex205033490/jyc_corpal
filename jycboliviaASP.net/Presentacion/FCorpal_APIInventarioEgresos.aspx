<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" Async="true" CodeBehind="FCorpal_APIInventarioEgresos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIInventarioEgresos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <script src="../js/jsUpon.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">

                    <!------------------------          API GET INVENTARIO EGRESO CON DETALLES          ------------------------------>
                    <div class="tb_getInventarioEgresoDet p-4 bg-light border rounded">
                        <h5 class="text-warning">Ver Inventario Egresos Detalle</h5>

                        <div class="mb-3">
                            <label class="form-label" for="TextBox2">Numero de Egreso:</label>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <asp:Button ID="BuscarEgresoInventarioDetalle" runat="server" Text="Buscar Egreso" CssClass="btn btn-warning" OnClick="BuscarEgresoInventarioDetalle_Click" />

                        <div class="mt-4">

                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                CellSpacing="2" Font-Size="X-Small" ForeColor="Black">

                                <Columns>
                                    <asp:BoundField DataField="NumeroEgreso" HeaderText="NumEgreso" SortExpression="numEgr" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                    <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                    <asp:BoundField DataField="CodigoAlmacen" HeaderText="CodAlmacen" SortExpression="cAlm" />
                                    <asp:BoundField DataField="MotivoMovimiento" HeaderText="MotMovimiento" SortExpression="mMov" />
                                    <asp:BoundField DataField="ItemAnalisis" HeaderText="ItemAnalisis" SortExpression="iAnali" />
                                    <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="glo" />
                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="usu" />
                                </Columns>

                                <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                <RowStyle BackColor="white" />
                                <AlternatingRowStyle BackColor="#f8f9fa" />
                            </asp:GridView>
                        </div>
                        <div class="mt-4">

                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                CellSpacing="2" Font-Size="X-Small" ForeColor="Black">

                                <Columns>
                                    <asp:BoundField DataField="Item" HeaderText="item" SortExpression="item" />
                                    <asp:BoundField DataField="CodigoProducto" HeaderText="codProducto" SortExpression="codProd" />
                                    <asp:BoundField DataField="UnidadMedida" HeaderText="uMedida" SortExpression="uMed" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="cantidad" SortExpression="cant" />
                                </Columns>

                                <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                <RowStyle BackColor="white" />
                                <AlternatingRowStyle BackColor="#f8f9fa" />
                            </asp:GridView>
                        </div>
                    </div>
                    <br>

                    <!------------------------          API GET INVENTARIO EGRESO            ------------------------------>
                    <div class="tb_getInventarioEgreso p-4 bg-light border rounded">
                        <h5 class="text-warning">Ver Inventario Egresos</h5>

                        <div class="mb-3">
                            <label class="form-label" for="TextBox2">Numero de transacción:</label>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <asp:Button ID="btn_BuscarEgresoInventario" runat="server" Text="Buscar Transaccion" CssClass="btn btn-warning" OnClick="BuscarEgresoInventario_Click" />

                        <div class="mt-4">

                            <asp:GridView ID="GridView3" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                CellSpacing="2" Font-Size="X-Small" ForeColor="Black">

                                <Columns>
                                    <asp:BoundField DataField="NumeroTransaccion" HeaderText="numTransacción" SortExpression="numTrans" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                    <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                    <asp:BoundField DataField="Almacen" HeaderText="Almacen" SortExpression="alm" />
                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="usu" />
                                </Columns>

                                <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                <RowStyle BackColor="white" />
                                <AlternatingRowStyle BackColor="#f8f9fa" />
                            </asp:GridView>
                        </div>
                    </div>
                    <br>

                    <!------------------------          API POST INVENTARIO EGRESO            ------------------------------>

                    <div class="tb_postInventarioEgreso p-4 bg-light border rounded">
                        <h5 class="text-warning">Registro Inventario Egresos</h5>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="form-label">Referencia:</label>
                                <asp:TextBox ID="TextBoxReferencia" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Codigo Almacen:</label>
                                <asp:TextBox ID="TextBoxCodigoAlmacen" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="form-label">Motivo Movimiento:</label>
                                <asp:TextBox ID="TextBoxMotivoMovimiento" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Item Analisis:</label>
                                <asp:TextBox ID="TextBoxItemAnalisis" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="form-label">Glosa:</label>
                                <asp:TextBox ID="TextBoxGlosa" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                   
                    <div>
                        <br />
                        <div class="column">
                            <label>Detalle Productos</label>
                            <table id="tblDetalleProductosEgresos">
                                <tr>
                                    <th>Numero Item</th>
                                    <th>Codigo Producto</th>
                                    <th>Unidad Medida</th>
                                    <th>Cantidad</th>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="number" class="inputNumber" name="item0" /></td>
                                    <td>
                                        <input type="text" class="inputText" name="codigoProducto0" /></td>
                                    <td>
                                        <input type="number" class="inputNumber" name="unidadMedida0" /></td>
                                    <td>
                                        <input type="number" class="inputNumber" name="cantidad0" /></td>
                                </tr>
                            </table>
                            <br />
                            <asp:Button ID="btnAddRow" runat="server" Text="Agregar Fila" CssClass="btn btn-warning" OnClientClick="addRowEgreso(); return false;" />
                            <asp:Button ID="btn_InventarioEgresoPost2" runat="server" Text="Registrar Egreso" CssClass="btn btn-warning" OnClick="btn_InventarioEgresoPost2_Click" />
                        </div>
                        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                    </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
