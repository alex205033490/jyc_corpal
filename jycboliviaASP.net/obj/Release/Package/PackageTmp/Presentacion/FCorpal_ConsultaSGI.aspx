<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_ConsultaSGI.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_ConsultaSGI" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
        
        
         .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: auto;
    }
        
         .style1
         {
             width: 6px;
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
<td class="style1"></td>
<td class="style1">
    <asp:Label ID="Label2" runat="server" Text="Consulta:"></asp:Label>
    </td>
<td class="style1">
    <asp:DropDownList ID="dd_consulta" runat="server" Width="350px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Actividades Personal</asp:ListItem>
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
</table>
<table>
    <tr>
        <td></td>
        <td>
            <asp:Label ID="Label9" runat="server" Text="Personal:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_personal" runat="server" Width="350px"></asp:TextBox>
            <asp:AutoCompleteExtender ID="tx_personal_AutoCompleteExtender0" runat="server" 
                TargetControlID="tx_personal"
                CompletionSetCount="12" 
                MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2" 
                UseContextKey="True"
                CompletionListCssClass="CompletionList" 
                CompletionListItemCssClass="CompletionlistItem" 
                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                >
            </asp:AutoCompleteExtender>
        </td>
        <td></td>
    </tr>
</table>

</div>
</td>
</tr>

<tr>
<td>

<div class="CR_datos">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="900px" 
        Width="900px">
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
