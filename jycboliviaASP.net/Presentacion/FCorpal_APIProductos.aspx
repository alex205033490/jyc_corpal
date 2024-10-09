<%@ Page Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_APIProductos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIProductos" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Container" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">
                    <div>
                        <asp:Label runat="server" Text="Label">API Productos</asp:Label><br />
                        <br />
                    </div>

                    <!-- GET PRODUCTOS/BUSCAR -->
                    <div class="get_productosNombre p-4 bg-light border rounded">
                        <h5 class="text-warning">Buscar Producto por Nombre</h5>

                        <div class="mb-3">
                            <label class="form-label" for="txt_nomProducto">Nombre del Producto:</label>
                            <asp:TextBox ID="txt_nomProducto" runat="server" CssClass="form-control" placeholder="Ingrese el nombre del producto"></asp:TextBox>
                        </div>

                        <asp:Button ID="btn_nomProducto" runat="server" Text="Buscar Producto" CssClass="btn btn-warning" OnClick="btn_buscarProdNombre_Click" />

                        <div class="mt-4">
                            <asp:GridView ID="gv_prodNombre" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
                                
                                <Columns>
                                    <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" SortExpression="cod" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                    <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                    <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="CodMedida" SortExpression="codUniMedida" />
                                    <asp:BoundField DataField="UnidadMedida" HeaderText="UnidadMedida" SortExpression="UniMedida" />
                                    <asp:BoundField DataField="UnidadMedidaAbreviatura" HeaderText="Abreviatura" SortExpression="Abrev" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="PrecioUnitario" SortExpression="PrecUnitario" />
                                    <asp:BoundField DataField="DescuentosPermitido" HeaderText="Descuento" SortExpression="Descuento"/>
                                    <asp:BoundField DataField="CostoUnitario" HeaderText="CostoUnitario" SortExpression="CosUnitario"/>
                                </Columns>

                                <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                <RowStyle BackColor="white" />
                                <AlternatingRowStyle BackColor="#f8f9fa" />
                            </asp:GridView>
                        </div>
                    </div><br />
                    <!-- GET PRODUCTOS/VENTAS/BUSCAR -->
                    <div class="get_productosVentas p-4 bg-light border rounded">
                        <h5 class="text-warning">Ventas de Productos</h5>

                        <div class="mb-3">
                            <label class="form-label" for="txt_ventProducto">Nombre del Producto:</label>
                            <asp:TextBox ID="txt_ventProducto" runat="server" CssClass="form-control" placeholder="Ingrese el nombre del producto"></asp:TextBox>
                        </div>

                        <asp:Button ID="btn_ventProducto" runat="server" Text="Buscar Ventas" CssClass="btn btn-warning" OnClick="btn_BuscarventProducto_Click" />

                        <div class="mt-4">
                            <asp:GridView ID="gv_prodVenta" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
                                <Columns>
                                    <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" SortExpression="cod" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                    <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                    <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="CodMedida" SortExpression="codUniMedida" />
                                    <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" SortExpression="UniMedida" />
                                    <asp:BoundField DataField="UnidadMedidaAbreviatura" HeaderText="Abreviatura" SortExpression="Abrev" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" SortExpression="PrecUnitario" />
                                    <asp:BoundField DataField="DescuentosPermitido" HeaderText="Descuento" SortExpression="Descuento"/>
                                    <asp:BoundField DataField="CostoUnitario" HeaderText="CostoUnitario" SortExpression="CosUnitario"/>
                                </Columns>

                                <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                <RowStyle BackColor="white" />
                                <AlternatingRowStyle BackColor="#f8f9fa" />
                            </asp:GridView>
                        </div>

                    </div><br />
                    <!-- GET PRODUCTOS/COMPRAS/BUSCAR -->

                    <div class="get_productosCompras p-4 bg-light border rounded">
                        <h5 class="text-warning">Compras de Productos</h5>

                        <div class="mb-2">
                            <label class="form-label" for="txt_codProductoComp">Codigo del producto:</label>
                            <asp:TextBox ID="txt_codProductoComp" runat="server" CssClass="form-control" placeholder="Ingrese el codigo del producto"></asp:TextBox>
                            
                            <label class="form-label" for="txt_codProveedorComp">Codigo del Proveedor:</label>
                            <asp:TextBox ID="txt_codProveedorComp" runat="server" CssClass="form-control" placeholder="Ingrese el codigo del proveedor"></asp:TextBox>
                        </div>

                        <asp:Button ID="btn_buscarCompras" runat="server" Text="Buscar Compras" CssClass="btn btn-warning" OnClick="btn_buscarCompras_Click" />

                        <div class="mt-4">
                            <asp:GridView ID="gv_prodCompras" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
                                <Columns>
                                    <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" SortExpression="cod" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                    <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                    <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="CodMedida" SortExpression="codUniMedida" />
                                    <asp:BoundField DataField="UnidadMedida" HeaderText="UnidadMedida" SortExpression="UniMedida" />
                                    <asp:BoundField DataField="UnidadMedidaAbreviatura" HeaderText="Abreviatura" SortExpression="Abrev" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="PrecioUnitario" SortExpression="PrecUnitario" />
                                    <asp:BoundField DataField="DescuentosPermitido" HeaderText="Descuento" SortExpression="Descuento"/>
                                    <asp:BoundField DataField="CostoUnitario" HeaderText="CostoUnitario" SortExpression="CosUnitario"/>
                                </Columns>

                                <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                <RowStyle BackColor="white" />
                                <AlternatingRowStyle BackColor="#f8f9fa" />
                            </asp:GridView>
                        </div>
                    </div><br />


                    <!-- GET PRODUCTOS/codProducto  -->
                    <div class="get_productosCodigo p-4 bg-light border rounded">
                        <h5 class="text-warning">Buscar Productos por Código</h5>

                        <div class="mb-3">
                            <label class="form-label" for="txt_codProducto">Código del Producto:</label>
                            <asp:TextBox ID="txt_codProducto" runat="server" CssClass="form-control" placeholder="Ingrese el código del producto"></asp:TextBox>
                        </div>

                        <asp:Button ID="btn_BuscarcodProducto" runat="server" Text="Buscar Producto" CssClass="btn btn-warning" OnClick="btn_BuscarcodProducto_Click" />

                        <div class=" tablaProyecto" >
                            <asp:GridView ID="gv_prodCod" runat="server" CssClass="table table-striped table-hover"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
                                <Columns>
                                    <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" SortExpression="CodProd" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descrip" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="PrecioUnitario" SortExpression="PrecUnit" />
                                    <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                    <asp:BoundField DataField="DescuentosPermitido" HeaderText="DescPermitido" SortExpression="DescPermitido" />
                                    <asp:BoundField DataField="CodigoMoneda" HeaderText="CodMoneda" SortExpression="CodMoneda" />
                                    <asp:BoundField DataField="EsFraccionado" HeaderText="EsFraccionado" SortExpression="EsFracc"/>
                                    <asp:BoundField DataField="Categoria" HeaderText="Categoria" SortExpression="Categ"/>

                                    <asp:BoundField DataField="Grupo" HeaderText="Grupo" SortExpression="Grupo" />
                                    <asp:BoundField DataField="SubGrupo" HeaderText="SubGrupo" SortExpression="SGrupo" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca"/>
                                    <asp:BoundField DataField="UnidadMedida" HeaderText="UnidadMedida" SortExpression="UMedida"/>

                                </Columns>

                                <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                <RowStyle BackColor="white" />
                                <AlternatingRowStyle BackColor="#f8f9fa" />
                            </asp:GridView>
                        </div>

                        <div class="mt-4">
                            <asp:GridView ID="gv_prodCodDet" runat="server" CssClass="table table-striped table-hover"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
                                <Columns>
                                    <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="codMedida" SortExpression="cod" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Nombre" />
                                    <asp:BoundField DataField="Abreviatura" HeaderText="Abreviatura" SortExpression="Stock" />
                                    <asp:BoundField DataField="CantidadRelacion" HeaderText="CantidadRelacion" SortExpression="codUniMedida" />
                                </Columns>


                                <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                <RowStyle BackColor="white" />
                                <AlternatingRowStyle BackColor="#f8f9fa" />
                            </asp:GridView>
                        </div>
                    </div><br />

                </div>
            </div>
        </div>
    </div>
</asp:Content>
