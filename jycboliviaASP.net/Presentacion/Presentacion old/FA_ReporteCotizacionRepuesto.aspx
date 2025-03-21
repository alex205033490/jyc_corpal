﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ReporteCotizacionRepuesto.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ReporteCotizacionRepuesto" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ReporteCotizacionRepuesto.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

   <div class="Centrar">
       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
           Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
           WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="650px" 
           Height="645px" style="margin-right: 0px">
           <LocalReport ReportPath="Reportes\cotizacionReporte.rdlc" 
               EnableExternalImages="True">
           </LocalReport>
       </rsweb:ReportViewer>
    </div>
</asp:Content>
