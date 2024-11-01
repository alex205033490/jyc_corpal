<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APIPedido.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIPedido" Async="true" MasterPageFile="~/PlantillaNew.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-12 col-md-offset-1">
                <div class="panel panel-success class">
                    <div>
                        <asp:Label CssClass="text_tittle1" runat="server" Text="Label"> PEDIDOS </asp:Label><br />
                        <br />
                    </div>

<!------------------------          API GET PEDIDO CON CRITERIO DETALLE (numero pedido)          ------------------------------>
                    
                    <div class="form_getPedidoDet p-4 bg-light border rounded col-md-12 col-lg-12" >
                        <h2 class="text_tittle2">Detalles del Pedido</h2>
                        <div class="container_pedidos col-md-12 col-lg-12">

                        <div class="container_input row mb-4">

                            <div class="col-md-6 col-lg-6 mb-2">
                                <label class="form-label" for="TextBox1">Numero de Pedido:</label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Ingrese un numero de pedido"></asp:TextBox>
                            </div>

                            <div class="container_btn col-md-6 col-lg-6 d-flex align-items-end">
                                <asp:Button ID="btn_buscarPedidoCriterio" runat="server" Text="Buscar Pedido" CssClass="btn btn-warning" OnClick="btn_buscarPedidoCriterio_Click" />
                            </div>

                        </div>

                                <div class="container_gv1 col-sm-12 col-md-12 col-lg-12 mb-2">
                                    <asp:GridView ID="gv_pedidoCriterio" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="NumeroPedido" HeaderText="Numero Pedido" SortExpression="nPed" />
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                            <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                            <asp:BoundField DataField="CodigoCliente" HeaderText="Codigo Cliente" SortExpression="cCli" />
                                            <asp:BoundField DataField="ImporteProductos" HeaderText="Importe Productos" SortExpression="iProd" />
                                            <asp:BoundField DataField="ImporteDescuentos" HeaderText="Importe Descuentos" SortExpression="iDesc" />
                                            <asp:BoundField DataField="ImporteTotal" HeaderText="Importe Total" SortExpression="iTotal" />
                                            <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="glo" />
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

                                <div class="container_gv2 col-sm-12 col-md-10 col-lg-12">
                                    <asp:GridView ID="gv_detalleProd" runat="server" CssClass="gridview" AutoGenerateColumns="false">

                                        <Columns>
                                            <asp:BoundField DataField="NumeroItem" HeaderText="Item" SortExpression="item" />
                                            <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo Producto" SortExpression="codProd" />
                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="cant" />
                                            <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="Codigo Unidad Medida" SortExpression="cUMed" />
                                            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" SortExpression="pUni" />
                                            <asp:BoundField DataField="ImporteDescuento" HeaderText="Importe Descuento" SortExpression="iDesc" />
                                            <asp:BoundField DataField="ImporteTotal" HeaderText="Importe Total" SortExpression="iTot" />

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
                            </div>
                        
                        <br>


<!------------------------          API GET PEDIDO C/S CRITERIO          ------------------------------>
               
                <div class="form_getPedido p-4 bg-light border rounded col-md-12 col-lg-12">
                    <h5 class="text_tittle2">Ver Pedidos</h5>

                    <div class="container_input row mb-4">
                    <div class="col-md-6 col-lg-6 mb-2"> 
                        <label class="form-label" for="TextBox2">Numero de Pedido:</label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Ingrese un código o deje vacío para mostrar todos los registros."></asp:TextBox>
                        <small class="form-text text-muted"></small>
                    </div>

                    <div class="container_btn col-md-6 d-flex col-lg-6 align-items-end">
                        <asp:Button ID="btn_buscarPedido" runat="server" Text="Buscar" CssClass="btn btn-warning" OnClick="btn_buscarPedido_Click" />
                    </div>

                        </div>
                        <div class="container_gv3">

                            <asp:GridView ID="gv_pedido" runat="server" CssClass="gridview" AutoGenerateColumns="false">

                                <Columns>
                                    <asp:BoundField DataField="NumeroPedido" HeaderText="Numero Pedido" SortExpression="nPed" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="cli" />
                                    <asp:BoundField DataField="CodigoCliente" HeaderText="Codigo Cliente" SortExpression="cCl" />
                                    <asp:BoundField DataField="ImporteTotal" HeaderText="Importe Total" SortExpression="iTot" />
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


<!------------------------          API POST PEDIDO           ------------------------------>
                    <script>
                        function addRowPedido() {
                            var table = document.getElementById("tblDetalleProductosPedido");
                            var rowCount = table.rows.length;
                            var row = table.insertRow(rowCount);
                            row.innerHTML =
                                `
                                <td><input class="form-control" type='text' name='codigoProducto${rowCount}' /></td>
                                 <td><input class="form-control" type='number' step="0.01" name='cantidad${rowCount}' /></td>
                                 <td><input class="form-control" type='number' name='codigoUnidadMedida${rowCount}' /></td>
                                 <td><input class="form-control" type='number' step="0.01" name='precioUnitario${rowCount}' /></td>
                                 <td><input class="form-control" type='number' step="0.01" name='importeDescuento${rowCount}' /></td>
                                 `;
                        }
                    </script>

                <div class="POST_inventarioIngreso p-4 bg-light border rounded">
                    <h5 class="text_tittle2">Formulario Registro De Pedido</h5>
                    

                    <div class="row mb-3 col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <div class="col-md-4">
                            <label class="form-label">Referencia:</label>
                            <asp:TextBox ID="txt_Referencia" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Codigo Cliente:</label>
                            <asp:TextBox ID="txt_codCliente" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3 col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <div class="col-md-4">
                            <label class="form-label" >Importe Descuentos:</label>
                            <asp:TextBox ID="txt_impDescuentos" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label" ">Glosa:</label>
                            <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form_detalleProducto col-md-12">
                        <label class="form-label">Detalle Producto</label>

                        <div class="table-detProducto">
                            <table id="tblDetalleProductosPedido" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Codigo Producto</th>
                                        <th>Cantidad</th>
                                        <th>CodUnidad Medida</th>
                                        <th>Precio Unitario</th>
                                        <th>Importe Descuento</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <input class="form-control " type="text" name="codigoProducto0" /></td>
                                        <td>
                                            <input class="form-control " type="number" name="cantidad0" step="0.01"/></td>
                                        <td>
                                            <input class="form-control " type="number" name="codigoUnidadMedida0" /></td>
                                        <td>
                                            <input class="form-control " type="number" name="precioUnitario0" step="0.01"/></td>
                                        <td>
                                            <input class="form-control " type="number" name="importeDescuento0" step="0.01"/></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <br />
                        <asp:Button ID="Button1" runat="server" Text="Agregar Fila" CssClass="btn btn-warning" OnClientClick="addRowPedido(); return false;" />
                        <asp:Button ID="btn_PostPedido" runat="server" Text="Registrar Pedido " CssClass="btn btn-warning" OnClick="btn_PostPedido_Click" />
                        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                    </div>

                    
                </div>
                    <br>
                    <br>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
