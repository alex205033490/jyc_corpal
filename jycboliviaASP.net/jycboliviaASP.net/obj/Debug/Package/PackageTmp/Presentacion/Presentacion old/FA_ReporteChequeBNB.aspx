<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ReporteChequeBNB.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ReporteChequeBNB" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_chequeBNB.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="Centrar">
        <rsweb:ReportViewer ID="Report_ChequeBNB" runat="server" Height="10cm" 
            Width="20cm">
        <LocalReport ReportPath="Reportes\Report_chequeBNB_ok.rdlc"></LocalReport>
        </rsweb:ReportViewer>
         
    </div>

</asp:Content>
