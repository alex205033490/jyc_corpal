<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_EntregaSolicitudProducto.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_EntregaSolicitudProducto" %>

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

        .container_detCar {
            margin-top: 0.3rem;
            font-size: 0.8rem;
        }
    </style>

    <div class="card">
        <div class="card-header bg-warning text-black">Entrega de Solicitud Productos</div>
        <div class="container-form">

            <asp:UpdatePanel ID="updatePanelLimpiarForm" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:UpdatePanel ID="updatePanelDDdetalleCar" runat="server">
                        <ContentTemplate>
                            <div class="form_buscarCar col-sm-6 col-md-5 col-lg-8 mb-2 row">
                                <div class="col-lg-5">
                                    <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="Vehiculo:"></asp:Label>
                                    <asp:DropDownList ID="dd_listVehiculo" Font-Size="Small" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="dd_listVehiculo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div class="container_detCar col-lg-5">
                                    <asp:GridView ID="gv_detCar" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="conductor" HeaderText="Conductor" />
                                            <asp:BoundField DataField="cargacajas" HeaderText="Capacidad Cajas" />
                                            <asp:BoundField DataField="capacidad" HeaderText="Capacidad (Tn)" />
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dd_listVehiculo" EventName="SelectedIndexChanged" />

                        </Triggers>
                    </asp:UpdatePanel>

                    <div class="row mb-3 col-lg-6">

                        <div class="form_encargadoEntrega col-6 col-lg-6">
                            <asp:Label runat="server" Font-Size="Small" Text="Encargado de entrega"></asp:Label>
                            <asp:TextBox ID="tx_entregoSolicitud" class="form-control" Style="background-color: #7080903b;" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>


                        <div class="col-sm-6 col-md-5 col-lg-5">
                        </div>

                    </div>

                    <!--  CONTAINER BOTONES  -->
                    <div class="container-btns col-sm-8 mb-3 col-md-7">
                        <div class="form_boton">
                            <asp:Button ID="btn_RegistrarSolicitud" runat="server" class="btn btn-success" Text="Guardar" Width="100px"
                                OnClick="btn_RegistrarSolicitud_Click" />
                            <asp:Button ID="bt_limpiar" runat="server" class="btn btn-primary" Text="Limpiar"
                                OnClick="bt_limpiar_Click" />
                            <asp:Button ID="bt_verRecibo" runat="server" class="btn btn-warning"
                                Text="Ver Recibo" OnClick="bt_verRecibo_Click" />
                            <asp:Button ID="btn_anularSolicitud" runat="server" class="btn btn-danger" Text="Retirar Solicitud (X)"
                                OnClick="btn_anularSolicitud_click" />
                        </div>

                    </div>


                            <div class="container-lista1">
                                <div>
                                    <h3>| LISTA DE SOLICITUDES |
                                    </h3>
                                </div>
                                <!-- LISTA DE SOLICITUDES DE PRODUCTO -->
                                <div class="container-gvRegistros table-responsive mb-2" data-clientid="<%= gv_solicitudesProductos.ClientID %>">

                                    <asp:GridView ID="gv_solicitudesProductos" runat="server"
                                        CssClass="table table-striped sticky-table gv_solicitudesProductos" AutoGenerateColumns="false" Style="background-color: white !important;"
                                        OnRowDataBound="gv_solicitudesProductos_RowDataBound" OnRowCommand="gv_solProductos_OnRowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:linkButton runat="server" id="id_seleccionar" style="background-color: bisque;" CommandName="SeleccionarProducto" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>SELECCIONAR</asp:linkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="nroboleta" HeaderText="Nro Boleta" HtmlEncode="false" />

                                            <asp:BoundField DataField="producto" HeaderText="Producto" HtmlEncode="false" />

                                            <asp:BoundField DataField="cantSolicitada" HeaderText="Cantidad Solicitada" />

                                            <asp:TemplateField HeaderText="Cantidad Entregada" SortExpression="Cantidad Entregada">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_cantentregada" runat="server" Text='<%# Bind("cantEntregada") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cantidad a Entregar">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tx_cantidadEntregarOK" runat="server" BackColor="Yellow" Width="90px" autoComplete="off" onInput="convertdotcomma(event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Entrega Parcial">
                                                <ItemTemplate>
                                                    <asp:CheckBox id="chk_entregaParcial" runat="server" CssClass="c"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="fechaentrega" HeaderText="Fecha Entrega" />
                                            <asp:BoundField DataField="personalsolicitud" HeaderText="Personal Solicitante" HtmlEncode="false" />
                                            <asp:BoundField DataField="codCliente" HeaderText="Codigo Cliente" />
                                            <asp:BoundField DataField="tiendaname" HeaderText="Cliente" HtmlEncode="false" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>


                    <div class="container-lista2">
                            <div>
                                <h3>| DESPACHO DE PRODUCTOS |</h3>
                            </div>
                        <div class="container_despachoDProductos border-2">
                            <div class="lista_despachos">
                                <asp:GridView ID="gv_despachoProductos" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" Visible="true">
                                    <Columns>
                                        <asp:BoundField DataField="nroboleta" HeaderText="Nro Boleta" SortExpression="Nro Boleta"/>
                                        <asp:BoundField DataField="producto" HeaderText="Producto" SortExpression="Producto"/>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>
                    </div>


                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="bt_limpiar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btn_RegistrarSolicitud" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

    </div>
    <script src="../js/mainCorpal.js"></script>
</asp:Content>
