<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APICuentasCobranza.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APICuentasCobranza" Async="true" MasterPageFile="~/PlantillaNew.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding-top: 1em;">
        <div class="">
            <div class="col-md-11 col-md-offset-1">
                <div class="panel panel-success class">

                    <!------------------------          API GET CUENTAS         ------------------------------>
                    <div class="container-GETCCobranzaDet p-4 rounded col-md-12">
                        <div class="container_tittle rounded">
                            <h5 class="text_tittle p-3">Consulta De Cuentas</h5>
                        </div>

                        <div class="row col-sm-10 col-md-12">
                            <div class="col-sm-6 col-md-3 col-lg-2 mb-2">
                                <asp:Button ID="btn_getCuentas" runat="server" Text="Consultar Cuentas" CssClass="btn btn-dark btn-sm" OnClick="btn_getCuentas_Click" />
                            </div>

                            <asp:UpdatePanel ID="updatePanelGet_CGet" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="container_gv1 col-12 col-xs-12 col-sm-12 col-md-12 col-lg-10">
                                        <asp:GridView ID="gv_Cuentas" runat="server" AutoGenerateColumns="false" CssClass="gridview">
                                            <Columns>
                                                <asp:BoundField DataField="NumeroCuenta" HeaderText="Numero Cuenta" SortExpression="nCuenta" />
                                                <asp:BoundField DataField="ContactoContacto" HeaderText="Contacto" SortExpression="contac" />
                                                <asp:BoundField DataField="ImporteTotal" HeaderText="Importe Total" SortExpression="iTotal" />
                                                <asp:BoundField DataField="ImporteSaldo" HeaderText="Importe Saldo" SortExpression="iSaldo" />
                                                <asp:BoundField DataField="ImporteVencido" HeaderText="Importe Vencido" SortExpression="iVenc" />
                                                <asp:BoundField DataField="CodigoMoneda" HeaderText="Codigo Moneda" SortExpression="cMoneda" />
                                                <asp:BoundField DataField="CodigoModulo" HeaderText="Codigo Modulo" SortExpression="cModulo" />
                                                <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="Glos" />
                                                <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha Vencimiento" SortExpression="fVenc" />
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
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btn_getCuentas" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <br />

                    <!------------------------          API GET CUENTAS COBRANZAS         ------------------------------>
                    <div class="container-GETCCobranzas p-4 rounded">
                        <div class="container_tittle rounded">
                            <h5 class="text_tittle p-3">CONSULTA COBRANZA (F)</h5>
                        </div>
                        <div class="form_busqueda row mb-3">
                            <div class="col-md-4 col-6 col-md-6 col-lg-5">
                                <asp:Label runat="server" Text="Numero de cobranza:"></asp:Label>
                                <asp:TextBox ID="txt_numCobranza" runat="server" placeholder="Ingrese un numero de cobranza" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-btn col-md-2 d-flex align-items-end col-6 col-md-3 col-lg-3">
                                <asp:Button ID="btn_buscarCobranza" runat="server" Text="Buscar" OnClick="btn_buscarCobranza_Click" CssClass="btn btn-dark "></asp:Button>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="container_gv1">
                                    <asp:GridView ID="gv_cuentaCobranza" runat="server" CssClass="gridview">
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
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>


                    </div>
                    <br />
                    <!------------------------          API POST CUENTAS/COBRANZA            ------------------------------>

                    <div class="container-POSTCCobranza p-4 rounded">
                        <div class="container_tittle rounded">
                            <h5 class="text_tittle p-3">FORMULARIO REGISTRO COBRANZA (F)</h5>
                        </div>

                        <div class="container-formulario container">

                            <div class="row">
                                <div class="col-md-3 col-sm-6 mb-2">
                                    <asp:Label runat="server">Referencia: </asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txt_Referencia" runat="server" placeholder=""></asp:TextBox>
                                </div>

                                <div class="col-md-3 col-sm-6 mb-2">
                                    <asp:Label runat="server">Glosa:</asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txt_glosa" runat="server" placeholder=""></asp:TextBox><br>
                                </div>

                                <div class="col-md-3 col-sm-6 mb-2">
                                    <asp:Label runat="server">Codigo Contacto:</asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txt_codContacto" runat="server" placeholder=""></asp:TextBox><br />
                                </div>

                                <div class="col-md-3 col-sm-6 mb-2">
                                    <asp:Label runat="server"> Importe Total:</asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txt_impTotal" runat="server" placeholder=""></asp:TextBox>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-3 col-sm-6 mb-2">
                                    <asp:Label runat="server" Text="lblcodigoMoneda">Tipo de moneda:</asp:Label>
                                    <asp:DropDownList ID="dd_codMoneda" runat="server" class="form-select">
                                        <asp:ListItem Text="Bolivianos" Value="1" />
                                        <asp:ListItem Text="Dolares" Value="2" />
                                    </asp:DropDownList><br />
                                </div>

                                <div class="col-md-4 col-sm-6 mb-2">
                                    <div class="border p-2 bg-light rounded shadow-sm">
                                        <asp:Label runat="server">COBROS</asp:Label><br />
                                        <asp:TextBox ID="txt_totalEfectivo" runat="server" placeholder="Total Efectivo" AutoComplete="off" CssClass="form-control mb-1"></asp:TextBox>
                                        <asp:TextBox ID="txt_totalDeposito" runat="server" placeholder="Total Deposito" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-5 col-sm-12 mb-2">
                                    <div class="border p-2 bg-light rounded shadow-sm">
                                        <asp:Label runat="server">DEPOSITO</asp:Label><br />
                                        <asp:Label runat="server" Text="lblcodigoBanco">Banco: </asp:Label>
                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-select mb-1">
                                            <asp:ListItem Text="Banco Visa" Value="1" />
                                            <asp:ListItem Text="Banco Economico" Value="2" />
                                            <asp:ListItem Text="Banco Union" Value="3" />
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txt_numCuenta" runat="server" placeholder="Numero de Cuenta" CssClass="form-control mb-1"></asp:TextBox>
                                        <asp:TextBox ID="txt_referenciaDepos" runat="server" placeholder="Referencia Desposito" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="col-md-12 row mb-2">
                            <div class="col-sm-9 col-md-9 col-lg-9 mb-2">
                                <h5>Detalle Cuentas</h5>

                                <asp:Panel runat="server" DefaultButton="btnAddRow">
                                    <div class="form_addCuenta row">
                                        <div class="input_numCuentaDet col-6">
                                            <asp:Label runat="server">Número de Cuenta</asp:Label>
                                            <asp:TextBox ID="txt_numCuentaDet" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                        </div>

                                        <div class="input_inporteCapital col-6">
                                            <asp:Label runat="server">Importe Capital</asp:Label>
                                            <asp:TextBox ID="txt_importeCapital" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>


                            </div>
                            <div class="col-sm-3 col-md-3 d-flex align-items-end mb-2">
                                <asp:Button ID="btnAddRow" runat="server" Text="Agregar" CssClass="btn btn-success" OnClientClick="addRowCobranza(); return false;" />
                            </div>

                        </div>
                        <div class="container_gv_Cuentas">
                            <asp:GridView ID="gv_productosAgregados" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClass="table table-bordered">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminarFila" runat="server" Text="eliminar" CommandName="Eliminar" CommandArgument='<% Eval("") %>' CssClass="btn btn-danger btn-sm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="NumeroCuenta" HeaderText="Número cuenta" />
                                    <asp:BoundField DataField="ImporteCapital" HeaderText="Imporye" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <asp:Button ID="btn_PostCobranza" runat="server" Text="Registrar Cobranza" CssClass="btn btn-success btn-sm" OnClick="btn_PostCobranza_Click" />
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
    <script src="../js/jsApi.js" type="text/javascript"></script>
</asp:Content>