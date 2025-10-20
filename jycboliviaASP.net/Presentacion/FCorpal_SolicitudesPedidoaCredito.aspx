<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_SolicitudesPedidoaCredito.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_SolicitudesPedidoaCredito" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Styles/Style_EntregaProductosACamion.css" rel="stylesheet" type="text/css" />
    <style>
        .container-gvRegistros {
            background-color: white;
            height: 250px;
            border: 2px solid black;
            font-size: 12px;
            overflow-y: auto;
            margin: 10px;
            padding: 0px;
            width: 700px;
        }
        .container-gvListProductos{
            height: 250px;
            margin: 10px 0;
            font-size: 0.7rem;
            padding: 0;
        }


    </style>
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
        <div class="card-header text-black">Solicitudes de Productos a Crédito</div>

        <div class="container-form">

            <asp:UpdatePanel ID="updatePanelListaSolicitudes" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <div class="container-lista1 row col-lg-12">
                        <div>
                            <h3>| LISTA DE PEDIDOS AL CRÉDITO|
                            </h3>
                        </div>
                        <!-- LISTA DE SOLICITUDES DE PRODUCTO -->
                        <div class="container-gvRegistros table-responsive mb-2 col-lg-8" data-clientid="<%= gv_solicitudesProductos.ClientID %>">
                            <asp:GridView ID="gv_solicitudesProductos" runat="server"
                                CssClass="table table-striped sticky-table gv_solicitudesProductos" AutoGenerateColumns="false"
                                Style="background-color: white !important;" DataKeyNames="codigo, nroboleta">
                                <Columns>
                                    <asp:TemplateField HeaderText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSolicitud" runat="server" CssClass="chkSelect" AutoPostBack="true" OnCheckedChanged="chkSolicitud_CheckedChanged"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="fechaHoraGRA" HeaderText="Fecha Solicitud" />

                                    <asp:BoundField DataField="codigo" HeaderText="Nro de Pedido" />

                                    <asp:BoundField DataField="nroboleta" HeaderText="Nro Boleta" HtmlEncode="false" />

                                    <asp:BoundField DataField="fechaentrega" HeaderText="Fecha Entrega" />
                                    <asp:BoundField DataField="personalsolicitud" HeaderText="Vendedor" HtmlEncode="false" />
                                    <asp:BoundField DataField="tiendaname" HeaderText="Cliente" HtmlEncode="false" />
                                </Columns>
                            </asp:GridView>
                        </div>


                        <!--  Lista productos  -->
                        <div class="container-gvListProductos table-responsive mb-2 col-lg-4" data-clientid="<%= gv_listProductos.ClientID %>">
                            <asp:GridView ID="gv_listProductos" runat="server"
                                CssClass="table table-striped sticky-table gv_listProductos" AutoGenerateColumns="false"
                                Style="background-color: white !important;">
                                <Columns>
                                    <asp:BoundField DataField="nroboleta" HeaderText="Nro de Boleta" />
                                    <asp:BoundField DataField="producto" HeaderText="Producto" HtmlEncode="false" />
                                    <asp:BoundField DataField="cantSolicitada" HeaderText="Cantidad Solicitada" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>


                    <div class="container-lista2">


                        <div class="form_buscarCar col-sm-12 col-md-12 col-lg-12 mb-2 row">

                            <div class="mb-3 col-lg-3 col-md-4 col-sm-6">
                                <asp:Label runat="server" Font-Size="Small" Text="Responsable:"></asp:Label>
                                <asp:TextBox ID="tx_responsable" class="form-control" Style="background-color: #7080903b;" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>


                            <div class="mb-3 container_btnRegistro d-flex flex-column gap-3 col-lg-3 col-md-4 col-sm-8">
                                <asp:Button ID="btn_registrarAprobacion" runat="server" CssClass="btn btn-success" Text="Aprobar Credito" OnClick="btn_registrarAprobacion_Click" />
                                <asp:Button ID="bt_limpiar" runat="server" class="btn btn-primary" Text="Limpiar" OnClick="bt_limpiar_Click" />
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
