<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" 
    CodeBehind="FCorpal_ConsultaProducto_SolicitudEntrega.aspx.cs" 
    Inherits="jycboliviaASP.net.Presentacion.FCorpal_ConsultaProducto_SolicitudEntrega" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" 
    namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ConsultaRutas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .CompletionList {
            padding: 5px 0;
            margin: 2px 0 0;
            height: 150px;
            width: 200px;
            background-color: White;
            cursor: pointer;
            border: solid 1px;
            font-size: x-small;
            overflow: auto;
        }
        .CompletionlistItem {
            font-size: x-small;
        }
        .CompletionListMighlightedItem {
            background-color: Green;
            color: White;
        }
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: auto;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="card">
    <div class="card-header bg-success text-white">Consulta</div>
    <ul class="list-group list-group-flush">
        <!-- Sección de filtros -->
        <li class="list-group-item">
            <div class="row">
                <div class="col-md-12">
                    <div class="CR_consulta">
                        <div class="group_campos col-lg-7 row p-2">

                            <!-- Consulta y botón buscar -->
                            <div class="col-lg-12 mb-3 d-flex align-items-center gap-2">
                                <asp:Label ID="Label2" runat="server" Text="Consulta:"></asp:Label>
                                <asp:DropDownList ID="dd_consulta" class="btn btn-secondary dropdown-toggle" runat="server" Width="420px">
                                    <asp:ListItem>Detalle de Datos</asp:ListItem>
                                    <asp:ListItem>Productos Solicitados Vs Entregados</asp:ListItem>
                                    <asp:ListItem>Productos Entregados y Solicitados por Persona</asp:ListItem>
                                    <asp:ListItem>Entrega Produccion</asp:ListItem>
                                    <asp:ListItem>Stock Producto</asp:ListItem>
                                    <asp:ListItem>Detalle Entrega Productos</asp:ListItem>
                                    <asp:ListItem>Calidad Proceso Nacho Remojado y Lavado</asp:ListItem>
                                    <asp:ListItem>Calidad Proceso Nacho Molinos</asp:ListItem>
                                    <asp:ListItem>Calidad Proceso Nacho Formadora</asp:ListItem>
                                    <asp:ListItem>Calidad Proceso Nacho Fritadora</asp:ListItem>
                                    <asp:ListItem>Calidad Proceso Nacho Sazonadora-Control Sensorial</asp:ListItem>
                                    <asp:ListItem>Calidad Proceso Nacho Envasadora</asp:ListItem>
                                    <asp:ListItem>Productos Solicitado y Entregado</asp:ListItem>
                                </asp:DropDownList>

                                <asp:Button ID="bt_buscar" runat="server" class="btn btn-success" Text="Buscar" onclick="bt_buscar_Click" />
                            </div>

                            <!-- Responsable y Producto -->
                            <div class="col-lg-5 col-md-5 col-6">
                                <asp:Label ID="Label5" runat="server" Text="Responsable:"></asp:Label>
                                <asp:TextBox ID="tx_responsable" class="form-control" runat="server"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="tx_responsable_AutoCompleteExtender" 
                                    runat="server" TargetControlID="tx_responsable"
                                    MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2"
                                    CompletionListCssClass="CompletionList"  
                                    CompletionListItemCssClass="CompletionlistItem" 
                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" 
                                    CompletionInterval="10">
                                </asp:AutoCompleteExtender>

                                <asp:Label ID="Label6" runat="server" Text="Producto:"></asp:Label>
                                <asp:TextBox ID="tx_producto" class="form-control" runat="server"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="tx_producto_AutoCompleteExtender" runat="server" 
                                    TargetControlID="tx_producto"
                                    CompletionSetCount="12" 
                                    MinimumPrefixLength="1" ServiceMethod="GetlistaProductos" 
                                    UseContextKey="True"
                                    CompletionListCssClass="CompletionList" 
                                    CompletionListItemCssClass="CompletionlistItem" 
                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" 
                                    CompletionInterval="10">
                                </asp:AutoCompleteExtender> 
                            </div>

                            <!-- Cliente -->
                            <div class="col-lg-5 col-md-5 col-6">
                                <asp:Label runat="server" Text="Cliente:"></asp:Label>
                                <asp:TextBox ID="tx_cliente" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="tx_cliente_AutoCompleteExtender" runat="server" 
                                    TargetControlID="tx_cliente"
                                    CompletionSetCount="12" 
                                    MinimumPrefixLength="1" ServiceMethod="GetListClientes" 
                                    UseContextKey="True"
                                    CompletionListCssClass="CompletionList" 
                                    CompletionListItemCssClass="CompletionlistItem" 
                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" 
                                    CompletionInterval="10">
                                </asp:AutoCompleteExtender> 
                            </div>

                            <!-- Fechas -->
                            <div class="group_fechas row col-lg-8">
                                <div class="col-lg-6 col-md-4 col-6">
                                    <asp:Label ID="Label3" runat="server" Text="Desde:"></asp:Label>
                                    <asp:TextBox ID="tx_desdeFecha" class="form-control" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="tx_desdeFecha_CalendarExtender" runat="server" 
                                        TargetControlID="tx_desdeFecha">
                                    </asp:CalendarExtender>
                                </div>
                                <div class="col-lg-6 col-md-4 col-6">
                                    <asp:Label ID="Label4" runat="server" Text="Hasta:"></asp:Label>
                                    <asp:TextBox ID="tx_hastaFecha" class="form-control" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="tx_hastaFecha_CalendarExtender" runat="server" 
                                        TargetControlID="tx_hastaFecha">
                                    </asp:CalendarExtender> 
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </li>

        <!-- Sección ReportViewer -->
        <li class="list-group-item">
            <div class="row">
                <div class="col-md-12">
                    <div class="CR_datos">
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" 
                            Height="900px" Font-Names="Verdana" Font-Size="8pt" 
                            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
                            WaitMessageFont-Size="14pt">
                        </rsweb:ReportViewer>
                    </div>
                </div>
            </div>
        </li>

        <li class="list-group-item">JYC</li>
    </ul>
</div>

</asp:Content>
