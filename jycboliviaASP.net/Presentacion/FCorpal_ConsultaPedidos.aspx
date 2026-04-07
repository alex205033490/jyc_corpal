<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" CodeBehind="FCorpal_ConsultaPedidos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_ConsultaPedidos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" 
    namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_EntregaProductosACamion.css" rel="stylesheet" type="text/css" />

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        function onResponsableSelected(sender, e) {
            const valor = e.get_value();
            const partes = valor.split(" - ");
            if (partes.length >= 2) {
                document.getElementById("<%= hf_codConductor.ClientID %>").value = partes[0];
                document.getElementById("<%= tx_nomConductor.ClientID %>").value = partes[1];
            }
        }

    </script>

    <style type="text/css">
        .container_title{
            background-color: darkorange;
            font-size: small;
            color: black !important;
        }
        .container-main{
            box-shadow: 1px 1px 6px 0px black;
        }
        .container_input{
            background-color: bisque;
            padding: 0.8rem;
            font-size: small;
        }
        .card-header{
            background-color: darkorange;
            color: black !important;
        }
        .container_reporte{
            background-color: white;
        }
        .container_form{
            margin-left: 0.1rem;
            padding: 0.25rem;
            box-shadow: 0px 0px 3px 0px black;
            background-color: antiquewhite;
        }
    </style>

</asp:content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container-main card">
    <div class="card-header">Reporte Pedidos</div>
    <div class="container_body">

        <div class="container_input">
            
            <div class="container_ddbusqueda mb-2">
                <asp:Label runat="server" Text="Consulta:"></asp:Label>
                <asp:DropDownList ID="dd_consulta" CssClass="btn btn-secondary dropworn-toggle" runat="server" 
                     style="font-size: small;">
                    <asp:ListItem>Seleccione un opción</asp:ListItem>
                    <asp:ListItem>Productos sobrantes de despachos</asp:ListItem>
                    <asp:ListItem>Reporte Tiempo de Tardanza Entrega de Productos (Despacho-Orden de Entrega)</asp:ListItem>
                </asp:DropDownList>

                <asp:Button ID="btn_buscarReporte" runat="server" CssClass="btn btn-success" Text="Buscar" Style="font-size: small;" OnClick="btn_buscarReporte_Click" />
            </div>

            <div class="container_form row col-5 mb-2">

                <div class="col-6">

                    <asp:Label runat="server" Text="Desde:"></asp:Label>
                        <asp:TextBox ID="tx_fdesde" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender ID="tx_fdesde_calendarExtender" runat="server"
                             TargetControlID="tx_fdesde"/>

                    <asp:HiddenField id="hf_codConductor" runat="server" />
                    <asp:Label runat="server" Text="Conductor"></asp:Label>
                    <asp:TextBox ID="tx_nomConductor" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="tx_conductor_AutoCompleteExtender" runat="server"
                         TargetControlID="tx_nomConductor" CompletionSetCount="12" MinimumPrefixLength="2"
                         ServiceMethod="getListResponsable" UseContextKey="true" CompletionListCssClass="CompletionList" 
                         CompletionListItemCssClass="CompletionlistItem" CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" 
                         CompletionInterval="10" OnClientItemSelected="onResponsableSelected"></asp:AutoCompleteExtender>

                </div>

                <div class="col-6">
                    <asp:Label runat="server" Text="Hasta"></asp:Label>
                    <asp:TextBox ID="tx_fhasta" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender ID="tx_fhasta_calendarEnteder" runat="server"
                        TargetControlID="tx_fhasta"/>

                </div>

            </div>
            
            <div class="container_reporte col-10">
                <rsweb:ReportViewer ID="rw_consultaPedidos" runat="server" Font-Names="Verdana"
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
                    WaitMessageFont-Size="14pt" Width="800px" Height="645px" style="margin-right: 0px;" ProcessingMode ="Local">
                    <LocalReport ReportPath="Reportes\Report_ConsultaProductosSobrantes_ordenEntrega.rdlc" 
                         EnableExternalImages="true">

                    </LocalReport>

                </rsweb:ReportViewer>
            </div>
            
        </div>


    </div>
</div>

</asp:Content>





















