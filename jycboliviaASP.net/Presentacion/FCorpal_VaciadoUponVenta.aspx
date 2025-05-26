<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_VaciadoUponVenta.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_VaciadoUponVenta" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_SeguimientosMorosos.css" rel="stylesheet" />
    <link href="../Styles/Style_VaciadoUponCompraVentas.css" rel="stylesheet"/>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $(".table-sticky");

            if (table.find("thead").length === 0) {
                table.prepend("<thead>" + table.find("tr:first").html() + "</thead>");
                table.find("tr:first").remove();
            }
        })

        $(document).ready(function () {
            function addCheckboxChangeListener() {
                var gridViewID = $(".container_datosUponVentas").data("clientid");

                $("#" + gridViewID + " input[type='checkbox']").change(function () {
                    var row = $(this).closest("tr");

                    if ($(this).is(":checked")) {
                        row.addClass("highlighted");
                    } else {
                        row.removeClass("highlighted");
                    }
                });
            }
            addCheckboxChangeListener();
            Sys.Application.add_load(function () {
                addCheckboxChangeListener();
            });
        });


    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card">
        <div class="card-header bg-success text-white">Vaciar al Upon las Ventas de JyC</div>
        <div class="card-body">
            <div class="list-group list-group-flush">
                <div class="list-group-item">
                    <div class="row">
                        
                        <div class="col-md-4 mb-2">
                            <asp:Label ID="Label3" for="tx_cliente" runat="server" Text="Cliente:" Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="tx_cliente" CssClass="form-control" runat="server" Font-Size="Small"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="tx_cliente_AutoCompleteExtender" runat="server"
                                TargetControlID="tx_cliente"
                                CompletionSetCount="12"
                                MinimumPrefixLength="1" ServiceMethod="GetlistaClienteV"
                                UseContextKey="True"
                                CompletionListCssClass="CompletionList"
                                CompletionListItemCssClass="CompletionlistItem"
                                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                            </asp:AutoCompleteExtender>
                        </div>
                        <div class="col-md-2">
                            <br />
                            <asp:Button ID="bt_buscar" CssClass="btn btn-info" runat="server" Text="Buscar" OnClick="bt_buscar_Click" />
                        </div>
                        <div class="col-md-12 mb-3">
                            <asp:CheckBox ID="cb_selecciontodo" Text="Seleccionar Todo" runat="server" AutoPostBack="true" OnCheckedChanged="cb_selecciontodo_CheckedChanged" />
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <asp:Button ID="bt_anularPago" CssClass="btn btn-danger" runat="server" OnClick="bt_anularPago_Click" Text="Anular" />
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="bt_vaciarAlUpon" CssClass="btn btn-success" runat="server" OnClick="bt_vaciarAlSimec_Click" Text="Vaciar al Upon" />
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" OnClick="Button1_Click" Text="Excel" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="list-group-item">

                <div class="container_datosUponVentas" data-clientid="<%= gv_datosCobros.ClientID %>"">
                    <asp:GridView ID="gv_datosCobros" runat="server" AutoGenerateColumns="false"
                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="7"
                        Font-Size="Small" ForeColor="Black" GridLines="Vertical" CssClass="table-sticky gv_datosCobros">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />
                        <Columns>
                            <asp:TemplateField HeaderText="Anular">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" CssClass="chkSelect"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="codigo" HeaderText="Codigo" SortExpression="codigo" />
                            <asp:BoundField DataField="Fecha_Entrega" HeaderText="Fecha Entrega" SortExpression="fe" />
                            <asp:BoundField DataField="cliente" HeaderText="Cliente" SortExpression="cli" />
                            <asp:BoundField DataField="direccion" HeaderText="Direccion" SortExpression="Dir" />
                            <asp:BoundField DataField="cicliente" HeaderText="CI cliente" SortExpression="ci" />
                            <asp:BoundField DataField="telefono" HeaderText="Telefono" SortExpression="tel" />
                            <asp:BoundField DataField="razonSocialEmisor" HeaderText="Razon social" SortExpression="rs" />
                            <asp:BoundField DataField="nitEmisor" HeaderText="NIT" SortExpression="nt" />
                            <asp:BoundField DataField="correoCliente" HeaderText="Correo Cliente" SortExpression="corr" />
                            <asp:BoundField DataField="montoTotal" HeaderText="Monto Total" SortExpression="mtotal" />
                            <asp:BoundField DataField="tipoCambio" HeaderText="Tipo Cambio" SortExpression="tcambio" />


                        </Columns>
                    </asp:GridView>
                </div>
           
        </div>
        <div class="list-group-item cant_equiposVentas">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label6" runat="server" Text="Cantidad Equipos :"></asp:Label>
                    <asp:Label ID="lb_cantDatos" runat="server" Text="0"></asp:Label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
