﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_EntregaSolicitudProducto.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_EntregaSolicitudProducto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Styles/Style_EntregaProductosACamion.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>

        var scrollPositicon = 0;
        function saveScroll() {
            var container = document.querySelector('.container-gvRegistros');
            if (container) {
                scrollPos = container.scrollTop;
            }
        }
        function restoreScroll() {
            var container = document.querySelector('.container-gvRegistros');
            if (container) {
                container.scrollTop = scrollPos;
            }
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
            saveScroll();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            restoreScroll();
        });
        $(document).ready(function () {
            var table = $(".sticky-table");
            if (table.find("thead").length === 0) {
                table.prepend("<thead>" + table.find("tr:first").html() + "</thead>");
                table.find("tr:first").remove();
            }
        })



    </script>

    <div class="card">
        <div class="card-header  text-black">Entrega de Solicitud Productos</div>

        <div class="container-form">

            <asp:UpdatePanel ID="updatePanelListaSolicitudes" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

            <div class="container-lista1">
                <div>
                    <h3>| LISTA DE SOLICITUDES |
                    </h3>
                </div>
                <!-- LISTA DE SOLICITUDES DE PRODUCTO -->
                <div class="container-gvRegistros table-responsive mb-2" data-clientid="<%= gv_solicitudesProductos.ClientID %>">

                    <asp:GridView ID="gv_solicitudesProductos" runat="server"
                        CssClass="table table-striped sticky-table gv_solicitudesProductos" AutoGenerateColumns="false"
                        Style="background-color: white !important;">
                        <Columns>
                            <asp:TemplateField HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="chkSelect" AutoPostBack="true" OnCheckedChanged="chk_seleccionar_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="codigo" HeaderText="Número de Pedido" />

                            <asp:BoundField DataField="nroboleta" HeaderText="Nro Boleta" HtmlEncode="false" />

                            <asp:BoundField DataField="codproducto" HeaderText="Codigo Producto" HtmlEncode="false" />

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
                                    <asp:CheckBox runat="server" ID="chktipoEntrega" />
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
                    <div class="lista_despachos col-lg-6">

                                <asp:GridView ID="gv_despachoProductos" runat="server" CssClass="gv_despachoProductos table table-striped"
                                    AutoGenerateColumns="false" Visible="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Número de solicitud">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodigoSolicitud" runat="server" Text='<%# Bind("codigoSolicitud") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nro Boleta">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNroBoleta" runat="server" Text='<%# Bind("nroboleta") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Producto">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProducto" runat="server" Text='<%# Bind("producto") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                    </div>
                </div>

                <!-- DETALLE VEHICULO Y DETALLE-->

                <div class="form_buscarCar col-sm-12 col-md-12 col-lg-12 mb-2 row">

                    <div class="mb-3 col-lg-3 col-md-4 col-sm-6">
                        <asp:Label runat="server" Font-Size="Small" Text="Encargado de entrega"></asp:Label>
                        <asp:TextBox ID="tx_entregoSolicitud" class="form-control" Style="background-color: #7080903b;" runat="server" ReadOnly="true"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="Vehiculo:"></asp:Label>
                        <asp:DropDownList ID="dd_listVehiculo" Font-Size="Small" runat="server" CssClass="form-select ddVehiculo" AutoPostBack="true" OnSelectedIndexChanged="dd_listVehiculo_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:UpdatePanel ID="updatePanelGVdetCar" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gv_detCar" runat="server" CssClass="table table-striped gv_detCar" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="conductor" HeaderText="Conductor" />
                                        <asp:BoundField DataField="cargacajas" HeaderText="Capacidad Cajas" />
                                        <asp:BoundField DataField="capacidad" HeaderText="Capacidad (Tn)" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dd_listVehiculo" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <div class="mb-3 container_detalle col-lg-3 col-md-4 col-sm-6">
                        <asp:Label ID="lb_detalleRegistro" runat="server" Font-Size="Small" Text="Detalle:"></asp:Label>
                        <asp:TextBox ID="txt_detalleRegistro" runat="server" CssClass="form-control txtdetalle" TextMode="MultiLine"></asp:TextBox>
                    </div>

                    <div class="mb-3 container_btnRegistro d-flex flex-column gap-3 col-lg-3 col-md-4 col-sm-8">
                        <asp:Button ID="btn_registrarDespacho" runat="server" CssClass="btn btn-success" Text="Registrar Despacho" OnClick="btn_registrarDespacho_Click" />
                        <asp:Button ID="bt_limpiar" runat="server" class="btn btn-primary" Text="Limpiar" OnClick="bt_limpiar_Click" />
                        <asp:Button ID="bt_verRecibo" runat="server" class="btn btn-warning"
                            Text="Ver Recibo" OnClick="bt_verRecibo_Click" />
                    </div>
                </div>
            </div>
                </ContentTemplate>
</asp:UpdatePanel>

            <br />



        </div>
    </div>
    <script src="../js/mainCorpal.js"></script>
</asp:Content>
