<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_Consulta_Importacion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_Consulta_Importacion" %>
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
    <asp:Label ID="Label2" runat="server" Text="Consulta:"></asp:Label>
    </td>
<td>
    <asp:DropDownList ID="dd_consulta" runat="server" Width="350px">
        <asp:ListItem></asp:ListItem>
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
    <asp:Label ID="Label3" runat="server" Text="Nro DUI:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_Dui" runat="server"></asp:TextBox>
    
    </td>
<td></td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Razon Social:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_RazonSocial" runat="server" Width="300px"></asp:TextBox>
    
    </td>
<td></td>
<td></td>
</tr>

</table>

<table>
<tr>
<td></td><td>
    <asp:Label ID="Label5" runat="server" Text="Fecha Desde:"></asp:Label>
    </td><td>
        <asp:TextBox ID="tx_fechadesde" runat="server"></asp:TextBox>
        <asp:CalendarExtender ID="tx_fechadesde_CalendarExtender" runat="server" 
            TargetControlID="tx_fechadesde">
        </asp:CalendarExtender>
    </td><td></td><td>
    <asp:Label ID="Label6" runat="server" Text="Fecha Hasta:"></asp:Label>
    </td><td>
        <asp:TextBox ID="tx_fechahasta" runat="server"></asp:TextBox>
        <asp:CalendarExtender ID="tx_fechahasta_CalendarExtender" runat="server" 
            TargetControlID="tx_fechahasta">
        </asp:CalendarExtender>
    </td>
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
        WaitMessageFont-Size="14pt">
        
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
