<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_VaciadoUponPedido.aspx.cs" Async="true" Inherits="jycboliviaASP.net.Presentacion.FCorpal_VaciadoUponPedido" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_SeguimientosMorosos.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Style_UponPedidoVaciado.css" rel="stylesheet" type="text/css" />

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            var table = $(".table-sticky");

            if (table.find("thead").length === 0) {
                table.prepend("<thead>" + table.find("tr:first").html() + "</thead>");
                table.find("tr:first").remove();
            }
        })

        $(document).ready(function () {
            function addCheckboxChangeListener() {
                var gridViewID = $(".container-listaPedidosVaciado").data("clientid");

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
        <div class="card-header bg-success text-white">Vaciar al Upon los Pedidos de JyC</div>
        <div class="card-body">
            <div class="list-group list-group-flush">
                <div class="list-group-item">
                    <div class="row mb-2">
                        <div class="col-9 col-sm-8 col-md-5">
                            <asp:Label ID="Label3" for="tx_cliente" runat="server" Text="Cliente:" Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="tx_cliente" CssClass="form-control" runat="server" Font-Size="Small"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="tx_cliente_AutoCompleteExtender" runat="server"
                                TargetControlID="tx_cliente"
                                CompletionSetCount="12"
                                MinimumPrefixLength="1" ServiceMethod="GetlistaClienteP"
                                UseContextKey="True"
                                CompletionListCssClass="CompletionList"
                                CompletionListItemCssClass="CompletionlistItem"
                                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                            </asp:AutoCompleteExtender>
                        </div>
                        <div class="col-3 col-sm-3 col-md-3">
                            <br />
                            <asp:Button ID="bt_buscar" CssClass="btn btn-info" runat="server" Text="Buscar" OnClick="bt_buscar_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-4 col-sm-4 col-md-3">
                            <asp:Button ID="bt_anularPago" CssClass="btn btn-danger" runat="server" OnClick="bt_anularPago_Click" Text="Anular" />
                        </div>
                        <div class="col-4 col-sm-4 col-md-3">
                            <asp:Button ID="bt_vaciarAlUpon" CssClass="btn btn-success" runat="server" OnClick="bt_vaciarAlSimec_Click" Text="Vaciar al Upon" />
                        </div>
                        <div class="col-4 col-sm-4 col-md-3">
                            <asp:Button ID="btn_Excel" CssClass="btn btn-primary" runat="server" OnClick="Button1_Click" Text="Excel" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="card-body list-group-item col-11 col-sm-11 col-md-11">

            <div class="container-listaPedidosVaciado col-md-12 " data-clientid="<%= gv_datosCobros.ClientID %>">
                <asp:GridView ID="gv_datosCobros" runat="server" BackColor="White" CssClass="table table-striped table-sticky gv_datosCobros"
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" CellPadding="6"
                    Font-Size="Small" ForeColor="black" GridLines="Vertical" AutoGenerateColumns="false">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="green" Font-Bold="true" ForeColor="white" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="right" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                    <Columns>
                        <asp:TemplateField HeaderText="Seleccionar">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAll" CssClass="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="Codigo Solicitud" SortExpression="codigo" />
                        <asp:BoundField DataField="nroboleta" HeaderText="Nro Boleta" SortExpression="nroboleta" />
                        <asp:BoundField DataField="fecha_entrega" HeaderText="Fecha Entrega" SortExpression="fecha_entrega" />
                        <asp:BoundField DataField="horaentrega" HeaderText="Hora Entrega" SortExpression="horaentrega" />
                        <asp:BoundField DataField="CodClienteUpon" HeaderText="Codigo Cliente" SortExpression="CodClienteUpon" />
                        <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente" />
                        <asp:BoundField DataField="ImporteProductos" HeaderText="Importe Total" SortExpression="ImporteProductos" />

                    </Columns>
                </asp:GridView>
            </div>

        </div>
        <div class="list-group-item" style="padding: 0px 20px 20px 20px;">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label6" runat="server" Text="Cantidad Registros :"></asp:Label>
                    <asp:Label ID="lb_cantDatos" runat="server" Text="0"></asp:Label>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
