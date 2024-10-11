<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ConsultaRuta.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ConsultaRuta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ConsultaRutas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
    </style>
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
<td class="style1"></td>
<td class="style1">
    <asp:Label ID="Label2" runat="server" Text="Consulta:"></asp:Label>
    </td>
<td class="style1">
    <asp:DropDownList ID="dd_consulta" runat="server" Width="350px">
        <asp:ListItem>Datos General</asp:ListItem>
        <asp:ListItem>Cronograma Ruta Mantenimiento</asp:ListItem>
        <asp:ListItem>Mantenimiento Preventivo</asp:ListItem>
        <asp:ListItem>Mantenimiento Correctivo/Emergencia</asp:ListItem>
        <asp:ListItem>Instruccion de Trabajo</asp:ListItem>
        <asp:ListItem>Inspeccion</asp:ListItem>
        <asp:ListItem>Otros</asp:ListItem>
        <asp:ListItem>Boleta por Nro</asp:ListItem>
        <asp:ListItem>Boleta Emergencia</asp:ListItem>
        <asp:ListItem>Boleta Preventiva/Emergencia</asp:ListItem>
    </asp:DropDownList>
    </td>
<td class="style1"></td>
<td class="style1">
    <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
        onclick="bt_buscar_Click" style="height: 26px" />
    </td>
<td class="style1"></td>
</tr>
</table>

<table>
<tr>
    <td></td>
    <td>
        <asp:Label ID="Label5" runat="server" Text="Nro Boleta:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tx_nroBoleta" runat="server" Width="100px"></asp:TextBox>
    </td>
    <td></td>
    <td>
        <asp:Label ID="Label6" runat="server" Text="Edificio:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tx_edificio" runat="server" Width="300px"></asp:TextBox>
    </td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td></td>
    <td>
        <asp:Label ID="Label7" runat="server" Text="Fecha Desde:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tx_desdeFecha" runat="server"></asp:TextBox>
        <asp:CalendarExtender ID="tx_desdeFecha_CalendarExtender" runat="server" 
            TargetControlID="tx_desdeFecha">
        </asp:CalendarExtender>
    </td>
    <td></td>
    <td>
        <asp:Label ID="Label8" runat="server" Text="Hasta:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tx_hastaFecha" runat="server"></asp:TextBox>
        <asp:CalendarExtender ID="tx_hastaFecha_CalendarExtender" runat="server" 
            TargetControlID="tx_hastaFecha">
        </asp:CalendarExtender>
    </td>
    <td></td>
    <td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:Label ID="Label3" runat="server" Text="Mes:"></asp:Label>
    </td>
<td>
    <asp:DropDownList ID="dd_mes" runat="server">
        <asp:ListItem>Enero</asp:ListItem>
        <asp:ListItem>Febrero</asp:ListItem>
        <asp:ListItem>Marzo</asp:ListItem>
        <asp:ListItem>Abril</asp:ListItem>
        <asp:ListItem>Mayo</asp:ListItem>
        <asp:ListItem>Junio</asp:ListItem>
        <asp:ListItem>Julio</asp:ListItem>
        <asp:ListItem>Agosto</asp:ListItem>
        <asp:ListItem>Septiembre</asp:ListItem>
        <asp:ListItem>Octubre</asp:ListItem>
        <asp:ListItem>Noviembre</asp:ListItem>
        <asp:ListItem>Diciembre</asp:ListItem>
    </asp:DropDownList>
    </td>
<td></td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Año:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_anio" runat="server" Font-Size="X-Small" Width="80px"></asp:TextBox>

    </td>
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
