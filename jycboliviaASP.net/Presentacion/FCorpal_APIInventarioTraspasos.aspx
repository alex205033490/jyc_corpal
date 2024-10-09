<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" Async="true" CodeBehind="FCorpal_APIInventarioTraspasos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIInventarioTraspasos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_SimecModificar.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">
                    <div>
                        <asp:Label runat="server" Text="Label">API INVENTARIO TRASPASO</asp:Label><br />
                        <br />
                    </div>



                    <!------------------------          API GET INVENTARIO TRASPASO            ------------------------------>
                    <div class="tb_getInventarioTraspaso p-4 bg-light border rounded">
                        <h5 class="text-warning">Ver Inventario Traspasos</h5>

                        <div class="mb-3">
                            <label class="form-label">Numero de transacción:</label>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <asp:Button ID="btn_GetinvTraspaso" runat="server" Text="Buscar Registro" CssClass="btn btn-warning" OnClick="btn_GetinvTraspaso_Click" />

                     <div class="mt-4">

                            <asp:GridView ID="gv_invTraspaso" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
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


                    <!------------------------          API GET INVENTARIO TRASPASO DETALLE           ------------------------------>
                    <div class="tb_getInventarioTraspasoDet p-4 bg-light border rounded">
                        <h5 class="text-warning">Ver Inventario Traspaso Detalle</h5>

                        <div class="mb-3">
                            <label class="form-label" for="TextBox2">Numero de Traspaso:</label>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <asp:Button ID="btn_GetinvTraspasoDet" runat="server" Text="Buscar Registro" CssClass="btn btn-warning" OnClick="btn_GetinvTraspasoDet_Click" />

                          <div class="mt-4">

                                    <asp:GridView ID="gv_invTraspasoDet" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                                        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">

                                        <Columns>
                                            <asp:BoundField DataField="NumeroTraspasos" HeaderText="NumTraspaso" SortExpression="numTra" />
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                            <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                            <asp:BoundField DataField="CodigoAlmacenDestino" HeaderText="CodAlmacenDestino" SortExpression="cAlmDest" />
                                            <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="glo" />
                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="usu" />
                                        </Columns>

                                        <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                                        <RowStyle BackColor="white" />
                                        <AlternatingRowStyle BackColor="#f8f9fa" />
                                    </asp:GridView>
                                </div>
                                <div class="mt-4">

                                    <asp:GridView ID="gv_invTraspasoDet2" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
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
                        



<!------------------------          API POST INVENTARIO TRASPASOS            ------------------------------>
                    <div class="panel-body">
                        &nbsp;
                    <label> POST - INVENTARIO TRASPASOS (F)</label>
                        <br>
                        <br>
     

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

                        <asp:Button ID="btn_registrarTraspaso" runat="server" Text="Registrar Traspaso"
                            
                            />
                        <asp:Label ID="lblresult" runat="server" Text="Label"></asp:Label>

                    </div>
                    <br>
                    <br>
                </div>
            </div>
        </div>
    </div>
</asp:Content>