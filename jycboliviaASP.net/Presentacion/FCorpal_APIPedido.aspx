<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APIPedido.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIPedido" Async="true" MasterPageFile="~/PlantillaNew.Master"%>

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
                    <div>
                        <asp:Label runat="server" Text="Label">API PEDIDO</asp:Label><br />
                        <br />
                    </div>


<!------------------------          API GET PEDIDO CON CRITERIO DETALLE (numero pedido)          ------------------------------>

                    <div class="get_pedidoDet p-4 bg-light border rounded">
                        <h5 class="text-warning">Ver Pedido con Detalle</h5>

                        <div class="mb-3">
                            <label class="form-label" for="TextBox1">Numero de Pedido:</label>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Ingrese numero de pedido"></asp:TextBox>
                        </div>

                        <asp:Button ID="btn_buscarPedidoCriterio" runat="server" Text="Buscar registro" CssClass="btn btn-warning" OnClick="btn_buscarPedidoCriterio_Click" />

                                <div class="mt-4">

                                    <asp:GridView ID="gv_pedidoCriterio" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">

                                        <Columns>
                                            <asp:BoundField DataField="NumeroPedido" HeaderText="numPedido" SortExpression="nPed" />
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                            <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                            <asp:BoundField DataField="CodigoCliente" HeaderText="CodCliente" SortExpression="cCli" />
                                            <asp:BoundField DataField="ImporteProductos" HeaderText="impProductos" SortExpression="iProd" />
                                            <asp:BoundField DataField="ImporteDescuentos" HeaderText="impDescuentos" SortExpression="iDesc" />
                                            <asp:BoundField DataField="ImporteTotal" HeaderText="impTotal" SortExpression="iTotal" />
                                            <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="glo" />
                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usu" />
                                        </Columns>

                                        <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                        <RowStyle BackColor="white" />
                                        <AlternatingRowStyle BackColor="#f8f9fa" />
                                    </asp:GridView>
                                </div>

                                <div class="mt-4">

                                    <asp:GridView ID="gv_detalleProd" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">

                                        <Columns>
                                            <asp:BoundField DataField="NumeroItem" HeaderText="item" SortExpression="item" />
                                            <asp:BoundField DataField="CodigoProducto" HeaderText="codProducto" SortExpression="codProd" />
                                            <asp:BoundField DataField="Cantidad" HeaderText="cantidad" SortExpression="cant" />
                                            <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="codUMedida" SortExpression="cUMed" />
                                            <asp:BoundField DataField="PrecioUnitario" HeaderText="precUnitario" SortExpression="pUni" />
                                            <asp:BoundField DataField="ImporteDescuento" HeaderText="impDescuento" SortExpression="iDesc" />
                                            <asp:BoundField DataField="ImporteTotal" HeaderText="impTotal" SortExpression="iTot" />

                                        </Columns>

                                        <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                        <RowStyle BackColor="white" />
                                        <AlternatingRowStyle BackColor="#f8f9fa" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <br>


<!------------------------          API GET PEDIDO C/S CRITERIO          ------------------------------>
                <div class="get_pedido p-4 bg-light border rounded">
                    <h5 class="text-warning">Ver Pedido General</h5>

                    <div class="mb-3">
                        <label class="form-label" for="TextBox2">Numero de transacción:</label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Ingrese numero de pedido"></asp:TextBox>
                    </div>

                    <asp:Button ID="btn_buscarPedido" runat="server" Text="Buscar transaccion" CssClass="btn btn-warning" OnClick="btn_buscarPedido_Click" />

                        <div class="mt-4">

                            <asp:GridView ID="gv_pedido" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                CellSpacing="2" Font-Size="X-Small" ForeColor="Black">

                                <Columns>
                                    <asp:BoundField DataField="NumeroPedido" HeaderText="numPedido" SortExpression="nPed" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="cli" />
                                    <asp:BoundField DataField="CodigoCliente" HeaderText="codCliente" SortExpression="cCl" />
                                    <asp:BoundField DataField="ImporteTotal" HeaderText="impTotal" SortExpression="iTot" />
                                </Columns>

                                <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                <RowStyle BackColor="white" />
                                <AlternatingRowStyle BackColor="#f8f9fa" />
                            </asp:GridView>
                        </div>
                    </div>
                    <br>


<!------------------------          API POST PEDIDO           ------------------------------>
                <div class="POST_inventarioIngreso p-4 bg-light border rounded">
                    <h5 class="text-warning">Registro de Pedido</h5>

                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="form-label">Referencia:</label>
                            <asp:TextBox ID="txt_Referencia" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Codigo Cliente:</label>
                            <asp:TextBox ID="txt_codCliente" runat="server"></asp:TextBox><br />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="form-label">Importe Productos:</label>
                            <asp:TextBox ID="txt_impProductos" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label" >Importe Descuentos:</label>
                            <asp:TextBox ID="txt_impDescuentos" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="form-label">Importe Total:</label>
                            <asp:TextBox ID="txt_impTotal" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label" ">Glosa:</label>
                            <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="column">
                        <label>Detalle Productos</label>
                        <table id="tblDetalleProductosPedido">
                            <tr>
                                <th>Numero Item</th>
                                <th>Codigo Producto</th>
                                <th>Cantidad</th>
                                <th>Codigo Unidad Medida</th>
                                <th>Precio Unitario</th>
                                <th>Importe Descuento</th>
                                <th>ImporteTotal</th>
                            </tr>
                            <tr>
                                <td>
                                    <input class="inputNumber" type="number" name="numeroItem0" /></td>
                                <td>
                                    <input class="inputText" type="text" name="codigoProducto0" /></td>
                                <td>
                                    <input class="inputNumber" type="number" name="cantidad0" /></td>
                                <td>
                                    <input class="inputNumber" type="number" name="codigoUnidadMedida0" /></td>
                                <td>
                                    <input class="inputNumber" type="number" name="precioUnitario0" /></td>
                                <td>
                                    <input class="inputNumber" type="number" name="importeDescuento0" /></td>
                                <td>
                                    <input class="inputNumber" type="number" name="importeTotal0" /></td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="Button1" runat="server" Text="Agregar Fila" CssClass="btn btn-warning" OnClientClick="addRowPedido(); return false;" />
                        <asp:Button ID="btn_PostPedido" runat="server" Text="Registrar Ingreso" CssClass="btn btn-warning" OnClick="btn_PostPedido_Click" />
                    </div>
                    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                </div>


                    <br>
                    <br>
                </div>
            </div>
        </div>

</asp:Content>