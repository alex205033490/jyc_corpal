<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ConsultaProyectosInstalacion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ConsultaProyectosInstalacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="../Styles/Style_ConsultaRutas.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
    </div>



<div class="CR_central">

<table>

<tr>
<td>
    <asp:Label ID="Label1" runat="server" Text="Consulta :"></asp:Label>
</td>
</tr>

<tr>
<td>
<div class="CR_consulta">

<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Consulta:" Font-Size="X-Small"></asp:Label>
    </td>
<td>
    <asp:DropDownList ID="dd_consulta" runat="server" Width="350px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>General R-155 Inspeccion y Seguimiento de la Obra</asp:ListItem>
        <asp:ListItem>Get R-155 por codigo</asp:ListItem>
    </asp:DropDownList>
    </td>
<td></td>
<td>
    <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
        onclick="bt_buscar_Click" style="height: 26px" />
    </td>
<td></td>
</tr>
</table>

<table>
<tr>
    <td></td>
    <td>
        <asp:Label ID="Label5" runat="server" Font-Size="X-Small" Text="Edificio:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tx_edificio" runat="server" Font-Size="X-Small" Width="300px"></asp:TextBox>
    </td>
    <td></td>
    <td>
        <asp:Label ID="Label6" runat="server" Font-Size="X-Small" Text="Codigo:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tx_codigo" runat="server" Font-Size="X-Small" Width="120px"></asp:TextBox>
    </td>
</tr>
</table>

<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label3" runat="server" Text="Desde:" Font-Size="X-Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_desdeFecha" runat="server" Font-Size="X-Small" 
        Width="120px"></asp:TextBox>
    <asp:CalendarExtender ID="tx_desdeFecha_CalendarExtender" runat="server" 
        TargetControlID="tx_desdeFecha">
    </asp:CalendarExtender>
    </td>
<td></td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Hasta:" Font-Size="X-Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_hastaFecha" runat="server" Font-Size="X-Small" 
        Width="120px"></asp:TextBox>
    <asp:CalendarExtender ID="tx_hastaFecha_CalendarExtender" runat="server" 
        TargetControlID="tx_hastaFecha">
    </asp:CalendarExtender>

    </td>
<td></td>
<td></td>
<td>
    &nbsp;</td>
<td></td>
<td></td>
</tr>

</table>


</div>
</td>
</tr>

<tr>
<td>

<div class="CR_datos">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="900px" 
        Height="900px" Font-Names="Verdana" Font-Size="8pt" 
        InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt"
         EnableExternalImages="True"
        >
        
    </rsweb:ReportViewer>
</div>

</td>
</tr>

<tr>
<td></td>
</tr>

</table>

</div>

</asp:Content>
