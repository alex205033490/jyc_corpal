<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APIProduccion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIProduccion" Async="true" MasterPageFile="~/PlantillaNew.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <script src="../js/jsUpon.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Contentn2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">
                    <div>
                        <asp:Label runat="server" Text="Label"> API PRODUCCION PARTE PRODUCCION </asp:Label><br /><br />
                    </div>

<!--------------------------------          API POST PRODUCCION PARTEproduccion  (registra datos vacios - La cadena de entrada no tiene el formato correcto.)       ----------------------->
                    
                        <div class="tb_postInventarioEgreso p-4 bg-light border rounded">
                            <h5 class="text-warning">Registro Produccion (F)</h5>

                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <label class="form-label">Referencia:</label>
                                    <asp:TextBox ID="txt_referencia" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Codigo Responsable:</label>
                                    <asp:TextBox ID="txt_codResponsable" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <label class="form-label">Item Analisis:</label>
                                    <asp:TextBox ID="txt_itemAnalisis" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Linea Produccion:</label>
                                    <asp:TextBox ID="txt_lineaProduccion" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <label class="form-label">Realizar Descarga:</label>
                                    <asp:DropDownList ID="dd_realDescarga" runat="server">
                                        <asp:ListItem Text="Si" Value="true" />
                                        <asp:ListItem Text="No" Value="false" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Glosa:</label>
                                    <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>


                    <div>
                        <asp:Label runat="server" Text=" - Detalle - "></asp:Label><br />

                        <table id="tblDetalleProductoProduccion" border="1">
                            <tr>
                                <th>Numero Item</th>
                                <th>Codigo Producto</th>
                                <th>Cantidad</th>
                                <th>Unidad Medida</th>
                                <th>Codigo Receta</th>
                            </tr>
                            <tr>
                                <td><input class= "inputNumber" type="number" name="item0"/></td>
                                <td><input class= "inputText" type="text" name="codigoProducto0"/></td>
                                <td><input class= "inputNumber" type="number" name="cantidad0"/></td>
                                <td><input class= "inputNumber" type="number" name="unidadMedida0"/></td>
                                <td><input class= "inputText" type="text" name="codigoReceta0"/></td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnAddRow" runat="server" Text="Agregar Fila" CssClass="btn btn-warning" onClientClick="addRowProduccion(); return false"  />
                        <asp:Button ID="btn_Prod_parteProd" runat="server" Text="Registrar Produccion" CssClass="btn btn-warning" OnClick="btn_Insert_parteProd_Click" />
                                            
                        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">&nbsp;</div>
    </div>

    <!---------------------------------------       API GET PRODUCCION DETALLE  (Muestra datos vacios)  ------------------------------------------------->
    <div class="tb_getProduccionDet p-4 bg-light border rounded">
        <h5 class="text-warning">Ver Produccion Detalle (F)</h5>

        <div class="mb-3">
            <label class="form-label">Numero Parte de Produccion:</label>
            <asp:TextBox ID="txt_numProduccion1" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <asp:Button ID="btn_buscProduccion" runat="server" Text="Buscar Produccion" CssClass="btn btn-warning" OnClick="btn_buscProduccion_Click" />


            <div class="mt-4">

        <asp:GridView ID="gv_produccion" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                    CellSpacing="2" Font-Size="X-Small" ForeColor="Black">

                    <Columns>
                        <asp:BoundField DataField="NumeroParteProduccion" HeaderText="numPartProduccion" SortExpression="nPartProd" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                        <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                        <asp:BoundField DataField="CodigoResponsable" HeaderText="codResponsable" SortExpression="cResp" />
                        <asp:BoundField DataField="ItemAnalisis" HeaderText="ItemAnalisis" SortExpression="iAnali" />
                        <asp:BoundField DataField="LineaProduccion" HeaderText="LineaProduccion" SortExpression="lProdc" />
                        <asp:BoundField DataField="RealizaDescarga" HeaderText="realizaDescarga" SortExpression="rDescarg" />
                        <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="glo" />
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="usu" />
                    </Columns>

                    <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                    <RowStyle BackColor="white" />
                    <AlternatingRowStyle BackColor="#f8f9fa" />
                </asp:GridView>
            </div>
            <div class="mt-4">

                <asp:GridView ID="gv_detalle" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
                    CellSpacing="2" Font-Size="X-Small" ForeColor="Black">

                    <Columns>
                        <asp:BoundField DataField="Item" HeaderText="item" SortExpression="item" />
                        <asp:BoundField DataField="CodigoProducto" HeaderText="codProducto" SortExpression="codProd" />
                        <asp:BoundField DataField="Cantidad" HeaderText="cantidad" SortExpression="cant" />
                        <asp:BoundField DataField="UnidadMedida" HeaderText="uMedida" SortExpression="uMed" />
                        <asp:BoundField DataField="CodigoReceta" HeaderText="codReceta" SortExpression="cRecet" />
                    </Columns>

                    <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                    <RowStyle BackColor="white" />
                    <AlternatingRowStyle BackColor="#f8f9fa" />
                </asp:GridView>
            </div>
        </div>
        <br/>


</asp:Content>
