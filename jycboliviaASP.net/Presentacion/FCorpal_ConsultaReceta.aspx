<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_ConsultaReceta.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_ConsultaReceta" %>
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

<div class="card">
  <div class="card-header bg-warning text-dark">
    Consultas
  </div>
  <ul class="list-group list-group-flush">
    <li class="list-group-item">
        <div class="row">
            <div class="col-md-12">
                <div class="CR_consulta">

<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Consulta:"></asp:Label>
    </td>
<td>
    <asp:DropDownList ID="dd_consulta" class="btn btn-secondary dropdown-toggle" runat="server" Width="350px">
        <asp:ListItem>Recetas</asp:ListItem>
        <asp:ListItem>Ninguno</asp:ListItem>        
    </asp:DropDownList>
    </td>
<td></td>
<td>
    <asp:Button ID="bt_buscar" CssClass="btn btn-info" runat="server" Text="Buscar" 
        onclick="bt_buscar_Click" />
    </td>
<td></td>
</tr>
</table>

</div>
            </div>
        </div>
    </li>

    <li class="list-group-item">
        <div class="row">
            <div class="col-md-12">
                <div class="CR_datos">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="900px" 
        Height="900px" Font-Names="Verdana" Font-Size="8pt" 
        InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt">
        
    </rsweb:ReportViewer>
</div>
            </div>
        </div>
    </li>

    <li class="list-group-item">

    </li>
  </ul>
</div>


</asp:Content>
