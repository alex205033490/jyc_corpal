<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_ReporteDespachoProductos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_ReporteDespachoProductos" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">


    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container_main">
        <div class="container_reporte">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"
                Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
                WaitMessageFont-Size="14pt" Width="800px" Height="645px" style="margin-right: 0px;">
                <LocalReport ReportPath="Reportes\Report_ReciboDespachoProductos.rdlc" 
                     EnableExternalImages="true">


                </LocalReport>

            </rsweb:ReportViewer>
        </div>
    </div>


</asp:Content>