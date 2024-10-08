<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ConsultaMantenimiento.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ConsultaMantenimiento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ConsultaRutas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .CompletionList
        {
            padding: 5px 0 ;
            margin: 2px 0 0;            
          /*  position:absolute;  */
            height:150px;
            width:200px;
            background-color: White;
            cursor: pointer;
            border: solid ;  
            border-width: 1px;    
            font-size:x-small;
            overflow: auto;
                        }
                        
           .CompletionlistItem
           {
               font-size:x-small;           
            }             
                        
        .CompletionListMighlightedItem
        {
             background-color: Green;
             color: White;
            /* color: Lime;
           padding: 3px 20px;
            text-decoration: none;           
            background-repeat: repeat-x;
            outline: 0;*/            
            } 
        
        .style3
        {
            width: 11px;
        }
        .style5
        {
            width: 50px;
        }
        .style6
        {
            width: 205px;
        }
        .style7
        {
            width: 146px;
        }
        .style8
        {
            width: 174px;
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
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Consulta:"></asp:Label>
    </td>
<td>
    <asp:DropDownList ID="dd_consulta" runat="server" Width="350px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Libro Diario Cobranza</asp:ListItem>
        <asp:ListItem>Encuestas Mantenimiento Realizadas</asp:ListItem>
        <asp:ListItem>Boleta por Nro</asp:ListItem>
        <asp:ListItem>Boleta Emergencia</asp:ListItem>
        <asp:ListItem>Boletas Preventiva/Emergencia</asp:ListItem>
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
    <asp:Label ID="Label6" runat="server" Text="Nro Boleta:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_nroBoleta" runat="server"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Label ID="Label7" runat="server" Text="Edifico:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_edificio" runat="server" Width="250px"></asp:TextBox>
    <asp:AutoCompleteExtender ID="tx_edificio_AutoCompleteExtender" runat="server" 
        TargetControlID="tx_edificio" CompletionSetCount="12" 
        MinimumPrefixLength="1" ServiceMethod="GetlistaProyectos2" 
        UseContextKey="True"
        CompletionListCssClass="CompletionList" 
        CompletionListItemCssClass="CompletionlistItem" 
        CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
        >
    </asp:AutoCompleteExtender>
    </td>
</tr>
</table>

<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label3" runat="server" Text="Desde:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_desdeFecha" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_desdeFecha_CalendarExtender" runat="server" 
        TargetControlID="tx_desdeFecha">
    </asp:CalendarExtender>
    </td>
<td></td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Hasta:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_hastaFecha" runat="server"></asp:TextBox>
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

<table>
<tr>
<td></td><td>
    <asp:Label ID="Label5" runat="server" Text="Cobrador:"></asp:Label>
    </td><td>
        <asp:DropDownList ID="dd_cobrador" runat="server" Width="250px">
        </asp:DropDownList>
    </td><td></td><td></td><td></td>
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
