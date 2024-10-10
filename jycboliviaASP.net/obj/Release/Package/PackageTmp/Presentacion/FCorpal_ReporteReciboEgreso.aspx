<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_ReporteReciboEgreso.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ReporteReciboEgreso" %>
<<<<<<< HEAD
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
    
=======
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
>>>>>>> origin/modulo3

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/Style_ReporteCotizacionRepuesto.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div class="Centrar">
       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
           Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
           WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="800px" 
           Height="645px" style="margin-right: 0px">
           <LocalReport ReportPath="Reportes\Report_ReciboEgreso.rdlc" 
               EnableExternalImages="True">
           </LocalReport>
       </rsweb:ReportViewer>
    </div>

</asp:Content>
