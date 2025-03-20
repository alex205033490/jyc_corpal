<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FACorpal_ConsultaGrafica.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FACorpal_ConsultaGrafica" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- <link href="../Styles/Style_ConsultaRutas.css" rel="stylesheet" type="text/css" /> -->    
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
        
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
  <div class="CR_central">

<table>

<tr>
<td>
    
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
    <asp:DropDownList ID="dd_consulta" class="btn btn-secondary dropdown-toggle" runat="server">
        <asp:ListItem>Ninguno</asp:ListItem>
        <asp:ListItem>Porcentajes Produccion Vs Porcentajes Ventas</asp:ListItem>        
    </asp:DropDownList>
    </td>
<td></td>
<td>
    <asp:Button ID="bt_buscar" CssClass="btn btn-info" runat="server" Text="Buscar" 
        onclick="bt_buscar_Click"  />
    </td>
<td></td>
</tr>
</table>
</div>
</td>
</tr>

<tr>
<td>

 <div runat = "server" id = "dvFrame"> </div>

</td>
</tr>

<tr>
<td></td>
</tr>

</table>

</div>


</asp:Content>
