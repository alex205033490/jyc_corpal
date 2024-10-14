<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APICuentasCobranza.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APICuentasCobranza" Async="true" MasterPageFile="~/PlantillaNew.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .CompletionList {
            padding: 5px 0;
            margin: 2px 0 0;
            /*  position:absolute;  */
            height: 150px;
            width: 200px;
            background-color: White;
            cursor: pointer;
            border: solid;
            border-width: 1px;
            font-size: x-small;
            overflow: auto;
        }

        .CompletionlistItem {
            font-size: x-small;
        }

        .CompletionListMighlightedItem {
            background-color: Green;
            color: White;
            /* color: Lime;
   padding: 3px 20px;
    text-decoration: none;           
    background-repeat: repeat-x;
    outline: 0;*/
        }

        .style1 {
            height: 26px;
        }

    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">
                    <div>
                        <asp:Label runat="server" Text="Label">API INVENTARIO INGRESOS</asp:Label><br />
                        <br />
                    </div>


                    <!------------------------          API GET CUENTAS         ------------------------------>
                    <div class="get_cuentas p-4 bg-light border rounded">
                        <h5 class="text-warning">Ver Cuentas</h5>

                        <div class="mb-3">
                            <asp:Button ID="btn_getCuentas" runat="server" Text="Ver Cuentas" CssClass="btn btn-warning" OnClick="btn_getCuentas_Click" />
                        </div>
                        <div class="row">

                            <div class="Gcuentas">
                                <div class="grid-container">

                                    <asp:GridView ID="gv_Cuentas" runat="server" BackColor="White"
                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                                        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical">
                                        <Columns>

                                            <asp:BoundField DataField="NumeroCuenta" HeaderText="numCuenta" SortExpression="nCuenta" />
                                            <asp:BoundField DataField="ContactoContacto" HeaderText="Contacto" SortExpression="contac" />
                                            <asp:BoundField DataField="ImporteTotal" HeaderText="Importe Total" SortExpression="iTotal" />
                                            <asp:BoundField DataField="ImporteSaldo" HeaderText="Importe Saldo" SortExpression="iSaldo" />
                                            <asp:BoundField DataField="ImporteVencido" HeaderText="Importe Vencido" SortExpression="iVenc" />
                                            <asp:BoundField DataField="CodigoMoneda" HeaderText="codMoneda" SortExpression="cMoneda" />
                                            <asp:BoundField DataField="CodigoModulo" HeaderText="codModulo" SortExpression="cModulo" />
                                            <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="Glos" />
                                            <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha Vencimiento" SortExpression="fVenc" />
                                        </Columns>



                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                   
                    <br />

                    <!------------------------          API GET CUENTAS COBRANZAS         ------------------------------>
                    <div class="panel-body">
                        <br>
                        <asp:Label runat="server" Text="GET - CONSULTA CUENTAS COBRANZA" ID="Label2"></asp:Label><br>
                        <br />
                        <asp:Label runat="server" Text="Ingrese un numero de cobranza: " ID="Label3"></asp:Label>
                        <asp:TextBox ID="txt_numCobranza" runat="server" placeholder=""></asp:TextBox>
                        <asp:Button ID="btn_buscarCobranza" runat="server" Text="Buscar" OnClick="btn_buscarCobranza_Click"></asp:Button>&nbsp;
                        <asp:GridView ID="gv_cuentaCobranza" runat="server">
                        </asp:GridView>
                    </div>
                    <br />
                    <!------------------------          API POST CUENTAS/COBRANZA            ------------------------------>
                    <script type="text/javascript">
                        function addRowCobranza() {
                            var table = document.getElementById("tblDetalleCuentas");
                            var rowCount = table.rows.length;
                            var row = table.insertRow(rowCount);
                            row.innerHTML =
                                `<td><input type='number' name='numeroCuenta${rowCount}' /></td>
                                 <td><input type='number' name='importeCapital${rowCount}' /></td>`;
                        }
                    </script>
                    <div class="panel-body">
                    <label>POST - CUENTAS/COBRANZA</label>
                        <br />

                        <asp:TextBox ID="txt_Referencia" runat="server" placeholder="Referencia"></asp:TextBox>
                        <asp:TextBox ID="txt_glosa" runat="server" placeholder="Glosa"></asp:TextBox><br>
                        <asp:TextBox ID="txt_codContacto" runat="server" placeholder="Codigo contacto"></asp:TextBox><br />
                        <asp:TextBox ID="txt_impTotal" runat="server" placeholder="Importe Total"></asp:TextBox>
                        <asp:Label runat="server" Text="lblcodigoMoneda">Tipo de moneda: </asp:Label>
                        <asp:DropDownList ID="dd_codMoneda" runat="server">
                            <asp:ListItem Text="Bolivianos" Value="1" />
                            <asp:ListItem Text="Dolares" Value="2" />
                        </asp:DropDownList><br />

                        <asp:Label runat="server"> Cobros </asp:Label><br />
                        <asp:TextBox ID="txt_totalEfectivo" runat="server" placeholder="Total Efectivo"></asp:TextBox>
                        <asp:TextBox ID="txt_totalDeposito" runat="server" placeholder="Total Deposito"></asp:TextBox><br />

                        <asp:Label runat="server">  - Deposito </asp:Label><br />
                        <asp:Label runat="server" Text="lblcodigoBanco">Banco: </asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem Text="Banco Visa" Value="1" />
                            <asp:ListItem Text="Banco Economico" Value="2" />
                            <asp:ListItem Text="Banco Union" Value="3" />
                        </asp:DropDownList><br />
                        <asp:TextBox ID="txt_numCuenta" runat="server" placeholder="Numero de Cuenta"></asp:TextBox>
                        <asp:TextBox ID="txt_referenciaDepos" runat="server" placeholder="Ref Desposito"></asp:TextBox><br />

                        <label>Detalle Cuentas</label>
                        <br>
                        <table id="tblDetalleCuentas" border="1">
                            <tr>
                                <th>NumeroCuenta</th>
                                <th>Importe Capital</th>
                            </tr>
                            <tr>
                                <td>
                                    <input type="number" name="numeroCuenta0" /></td>
                                <td>
                                    <input type="number" name="importeCapital0" /></td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnAddRow" runat="server" Text="Agregar Fila" OnClientClick="addRowCobranza(); return false;" />
                        <asp:Button ID="btn_PostCobranza" runat="server" Text="Insertar Cobranza" OnClick="btn_PostCobranza_Click" />
                        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
