<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_ReporteReciboIngreso.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ReporteReciboIngreso" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/Style_ReporteCotizacionRepuesto.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   

   <div class="Centrar">
       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
           Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
           WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="800px" 
           Height="645px" style="margin-right: 0px">
           <LocalReport ReportPath="Reportes\Report_ReciboIngreso.rdlc" 
               EnableExternalImages="True">
           </LocalReport>
       </rsweb:ReportViewer>
    </div>

</asp:Content>
