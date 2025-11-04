<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_DespachoCamiones.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_DespachoCamiones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_EntregaProductosACamion.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            var table = $(".sticky-table");
            if (table.find("thead").length === 0) {
                table.prepend("<thead>" + table.find("tr:first").html() + "</thead>");
                table.find("tr:first").remove();
            }
        })

        $(document).ready(function () {
            function addCheckboxChangeListener() {
                var gridViewId = $(".container-gvRegistros").data("clientid");

                $("#" + gridViewId + " input[type='checkbox']").change(function () {
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

    <style>
        .error-message {
            color: red;
            font-size: 12px;
            margin-top: 5px;
        }
        
        .gv_despachos th {
            
            border: 1px solid white;
        }
        .gv_despachos td{
            text-align:center;
        }
        .sticky-table th {
            position: sticky !important;
            top: 0 !important;
            background-color: #ff8800 !important;
            color: white !important;
            z-index: 100 !important;
            border:1px solid white !important;
            text-align: center;
        }
    </style>


    <div class="card">
        <div class="card-header bg-warning text-black">Entrega de Productos a Camion</div>
        <div class="container-form">

            <div class="form_buscarCar col-6 col-sm-6 col-md-4 col-lg-3 mb-2">
                <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="Vehiculo:"></asp:Label>
                <asp:DropDownList ID="dd_listVehiculo" Font-Size="Small" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="dd_listVehiculo_SelectedIndexChanged">
                </asp:DropDownList>
            </div>


            <div class="row mb-3 col-lg-10">
                <div class=" col-sm-6 col-md-5 col-lg-6">
                    <div class="row col-lg-12 mb-2">
                        <div class="form_fentrega col-lg-6 col-md-6 col-sm-6 col-5">
                            <asp:Label ID="Label2" runat="server" Text="Fecha Desde :" Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="tx_fechaDesdeDespacho" class="form-control" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="tx_fechaDesdeDespacho_CalendarExtender" runat="server"
                                TargetControlID="tx_fechaDesdeDespacho"></asp:CalendarExtender>
                        </div>

                        <div class="form_hentrega col-lg-6 col-md-6 col-sm-6 col-5">
                            <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Fecha hasta :"></asp:Label>
                            <asp:TextBox ID="tx_fechaHastaDespacho" class="form-control" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="tx_fechaHastaDespacho_CalendarExtender" runat="server"
                                TargetControlID="tx_fechaHastaDespacho"></asp:CalendarExtender>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6 col-md-5 col-lg-5">
                    <div class="form_solicitante col-lg-6 col-6">
                        <asp:Label ID="Label10" runat="server" Font-Size="Small"
                            Text="Solicitante del Producto:"></asp:Label>
                        <asp:TextBox ID="tx_SolicitanteProducto" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form_estado col-5 col-xs-5 col-sm-5 col-md-5 col-lg-4">
                        <asp:Label ID="Label1" runat="server" Font-Size="Small" CssClass="d-block" Text="Estado:"></asp:Label>
                        <asp:DropDownList ID="dd_estadoCierre" class="btn btn-secondary dropdown-toggle" runat="server">
                            <asp:ListItem>Abierto</asp:ListItem>
                            <asp:ListItem>Cerrado</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>

            <!-- CONTAINER BOTONES  -->
            <div class="container-btns col-sm-8 mb-12 col-md-12" style="margin: 0.5rem 0rem;">
                <div class="form_boton">
                    <asp:Button ID="bt_limpiar1" runat="server" class="btn btn-primary" Text="Limpiar" OnClick="bt_limpiar_Click" />
                    <asp:Button ID="bt_buscar1" runat="server" class="btn btn-info" Text="Buscar" Width="100px" OnClick="bt_buscar_Click" />
                    <asp:Button ID="bt_actualizar1" runat="server" class="btn btn-success" Text="Entregado" Width="100px" OnClick="bt_actualizar_Click" />
                    <asp:Button ID="bt_verRecibo1" runat="server" class="btn btn-warning" Text="Ver Recibo" OnClick="bt_verRecibo_Click" />
                </div>

            </div>


            <asp:UpdatePanel ID="updatePanelDDUpdate" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                            <div class="vista1">
                                <asp:GridView ID="gv_despachos" CssClass="gv_despachos sticky-table"
                                    runat="server" BackColor="White" AutoGenerateColumns="false"
                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="10"
                                    Font-Size="Small" ForeColor="Black" GridLines="Vertical">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:BoundField DataField="codigo" HeaderText="Codigo" HtmlEncode="false" />
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" HtmlEncode="false" />
                                        <asp:BoundField DataField="horagra" HeaderText="Hora" HtmlEncode="false" />
                                        <asp:BoundField DataField="detalle" HeaderText="Detalle" HtmlEncode="false" />
                                        <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" HtmlEncode="false" />
                                        <asp:BoundField DataField="Conductor" HeaderText="Conductor" HtmlEncode="false" />
                                        <asp:BoundField DataField="estadodespacho" HeaderText="Estado Despacho" HtmlEncode="false" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#669900" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                            </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dd_listVehiculo" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

        </div>

    </div>

</asp:Content>
