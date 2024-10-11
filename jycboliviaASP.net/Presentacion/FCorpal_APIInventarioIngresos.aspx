<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" Async="true" CodeBehind="FCorpal_APIInventarioIngresos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIInventarioIngresos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <script src="../js/jsUpon.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">

                    <!------------------------          API GET INVENTARIO INGRESOS CON DETALLES          ------------------------------>
                    <div class="get_inventarioIngreso p-4 bg-light border rounded">
                        <h5 class="text-warning">Ver Inventario Ingresos Detalle</h5>

                        <div class="mb-3">
                            <label class="form-label" for="TextBox1">Numero de ingreso:</label>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Ingrese numero de ingreso"></asp:TextBox>
                        </div>

                        <asp:Button ID="btn_InvIngresoGET" runat="server" Text="Buscar registro de ingreso" CssClass="btn btn-warning" OnClick="btn_InvIngresoGET_Click" />

                        <div class="mt-4">

                            <asp:GridView ID="gv_Inventario" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                CellSpacing="2" Font-Size="X-Small" ForeColor="Black">

                                <Columns>
                                    <asp:BoundField DataField="NumeroIngreso" HeaderText="NumIngreso" SortExpression="numIng" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                    <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                    <asp:BoundField DataField="CodigoMoneda" HeaderText="CodMoneda" SortExpression="codMon" />
                                    <asp:BoundField DataField="CodigoAlmacen" HeaderText="CodAlmacen" SortExpression="codAlm" />
                                    <asp:BoundField DataField="MotivoMovimiento" HeaderText="MotMovimiento" SortExpression="motMov" />
                                    <asp:BoundField DataField="ItemAnalisis" HeaderText="ItemAnalisis" SortExpression="itAnalis" />
                                    <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="glosa" />
                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usu" />
                                </Columns>

                                <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                <RowStyle BackColor="white" />
                                <AlternatingRowStyle BackColor="#f8f9fa" />
                            </asp:GridView>
                        </div>

                        <div class="mt-4">

                            <asp:GridView ID="gv_DetalleProductos" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                CellSpacing="2" Font-Size="X-Small" ForeColor="Black">

                                <Columns>
                                    <asp:BoundField DataField="Item" HeaderText="item" SortExpression="item" />
                                    <asp:BoundField DataField="CodigoProducto" HeaderText="codProducto" SortExpression="codProd" />
                                    <asp:BoundField DataField="UnidadMedida" HeaderText="uMedida" SortExpression="uMed" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="cantidad" SortExpression="cant" />
                                    <asp:BoundField DataField="CostoUnitario" HeaderText="costoUnitario" SortExpression="costUnit" />
                                    <asp:BoundField DataField="CostoTotal" HeaderText="costoTotal" SortExpression="costTotal" />

                                </Columns>

                                <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                <RowStyle BackColor="white" />
                                <AlternatingRowStyle BackColor="#f8f9fa" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <br>


                <!------------------------          API GET INVENTARIO INGRESOS            ------------------------------>
                <div class="get_inventarioIngreso p-4 bg-light border rounded">
                    <h5 class="text-warning">Ver Inventario Ingresos General</h5>

                    <div class="mb-3">
                        <label class="form-label" for="TextBox2">Numero de transacción:</label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Ingrese numero de transacción"></asp:TextBox>
                    </div>

                    <asp:Button ID="btn_invIngreso2" runat="server" Text="Buscar transaccion" CssClass="btn btn-warning" OnClick="btn_invIngreso2_Click" />

                    <div class="mt-4">

                        <asp:GridView ID="gv_invIngresos2" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
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

                <!------------------------          API POST INVENTARIO INGRESOS            ------------------------------>
                <script>
                    function addRowIngreso() {
                        var table = document.getElementById("tblDetalleProductosIngresos");
                        var rowCount = table.rows.length;
                        var row = table.insertRow(rowCount);
                        row.innerHTML =
                            `<td><input type='number' class="inputNumber" name='item${rowCount}' /></td>
                            <td><input type='text' class="inputText" name='codigoProducto${rowCount}' /></td>
                            <td><input type='number' class="inputNumber" name='unidadMedida${rowCount}' /></td>
                            <td><input type='number' class="inputNumber" name='cantidad${rowCount}' /></td>
                            <td><input type='number' class="inputNumber" name='costoUnitario${rowCount}' /></td>`;
                                    }
                </script>


                <div class="POST_inventarioIngreso p-4 bg-light border rounded">
                    <h5 class="text-warning">Registro de Inventario Ingreso</h5>
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="form-label">Referencia:</label>
                            <asp:TextBox ID="txt_Referencia" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Tipo de moneda:</label>
                            <asp:DropDownList ID="dd_codMoneda" runat="server">
                                <asp:ListItem Text="Bs" Value="1" />
                                <asp:ListItem Text="$" Value="2" />
                            </asp:DropDownList>
                        </div>

                    </div>

                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="form-label">Código Almacen:</label>
                            <asp:TextBox ID="txt_codAlmacen" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label" for="txt_motMovimiento">Motivo Movimiento:</label>
                            <asp:TextBox ID="txt_motMovimiento" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="form-label" for="txt_itemAnalisis">Item Análisis:</label>
                            <asp:TextBox ID="txt_itemAnalisis" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label" for="txt_glosa">Glosa:</label>
                            <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="column">
                        <label>Detalle Productos</label>
                        <table id="tblDetalleProductosIngresos">
                            <tr>
                                <th>Numero Item</th>
                                <th>Codigo Producto</th>
                                <th>Unidad Medida</th>
                                <th>Cantidad</th>
                                <th>Costo Unitario</th>
                
                            </tr>
                            <tr>
                                <td>
                                    <input class="inputNumber" type="number" name="item0" /></td>
                                <td>
                                    <input class="inputText" type="text" name="codigoProducto0" /></td>
                                <td>
                                    <input class="inputNumber" type="number" name="unidadMedida0" /></td>
                                <td>
                                    <input class="inputNumber" type="number" name="cantidad0" step="0.01"/></td>
                                <td>
                                    <input class="inputNumber" type="number" name="costoUnitario0" step="0.01"/></td>
                              
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnAddRow" runat="server" Text="Agregar Fila" CssClass="btn btn-warning" OnClientClick="addRowIngreso(); return false;" />
                        <asp:Button ID="btn_registrarIngreso" runat="server" Text="Registrar Ingreso" CssClass="btn btn-warning" OnClick="btn_registrarIngreso_Click" />
                    </div>
                    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                </div>

                <br>
                <br>
            </div>
        </div>
    </div>
</asp:Content>

