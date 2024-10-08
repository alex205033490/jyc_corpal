﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_ConsultaIngresoEgreso.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ConsultaRepuesto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>



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
        
        
         .style8
         {
             width: 110px;
         }
        </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 

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
        <asp:ListItem>Libro Diario Ingreso</asp:ListItem>
        <asp:ListItem>Libro Diario Egreso</asp:ListItem>
        <asp:ListItem>Ingreso Vs Egreso con Saldo Inicial</asp:ListItem>
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
</tr>

</table>

<table>
<tr>
<td></td><td>
    <asp:Label ID="Label5" runat="server" Text="Responsable:"></asp:Label>
    </td><td>
        <asp:TextBox ID="tx_responsable" runat="server" Width="350px"></asp:TextBox>
        <asp:AutoCompleteExtender ID="tx_responsable_AutoCompleteExtender" 
            runat="server" TargetControlID="tx_responsable"
             ServiceMethod="GetlistaResponsable2" MinimumPrefixLength="1"
                                    UseContextKey="True"
                                    CompletionListCssClass="CompletionList" 
                                    CompletionListItemCssClass="CompletionlistItem"                                     
                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">

        </asp:AutoCompleteExtender>
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
