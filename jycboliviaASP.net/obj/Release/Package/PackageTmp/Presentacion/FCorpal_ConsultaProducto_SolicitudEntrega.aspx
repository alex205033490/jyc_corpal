﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_ConsultaProducto_SolicitudEntrega.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_ConsultaProducto_SolicitudEntrega" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<<<<<<< HEAD
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
=======
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
>>>>>>> origin/modulo3

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
    
        </style>
<<<<<<< HEAD

=======
>>>>>>> origin/modulo3
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<<<<<<< HEAD
<div class="card">
  <div class="card-header bg-success text-white"> Consulta </div>
  <ul class="list-group list-group-flush">
    <li class="list-group-item">
        <div class="row">
            <div class="col-md-12">
                <div class="CR_consulta">

<table>
<tr>
<td><asp:Label ID="Label2" runat="server" Text="Consulta:"></asp:Label></td>
<td>
    <asp:DropDownList ID="dd_consulta" class="btn btn-secondary dropdown-toggle" runat="server" Width="350px">
=======

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
    <asp:DropDownList ID="dd_consulta" class="form-control" runat="server" Width="350px">
>>>>>>> origin/modulo3
        <asp:ListItem>Detalle de Datos</asp:ListItem>
        <asp:ListItem>Productos Solicitados Vs Entregados</asp:ListItem>
        <asp:ListItem>Productos Entregados y Solicitados por Persona</asp:ListItem>
        <asp:ListItem>Entrega Produccion</asp:ListItem>
        <asp:ListItem>Stock Producto</asp:ListItem>
        <asp:ListItem>Detalle Entrega Productos</asp:ListItem>
    </asp:DropDownList>
    </td>
<<<<<<< HEAD
<td><asp:Button ID="bt_buscar" runat="server" class="btn btn-success" Text="Buscar"  onclick="bt_buscar_Click"  />    </td>
</tr>
<tr>
 <td><asp:Label ID="Label5" runat="server" Text="Responsable:"></asp:Label></td>
 <td><asp:TextBox ID="tx_responsable" class="form-control" runat="server"  Width="400px"></asp:TextBox>
=======
<td></td>
<td>
    <asp:Button ID="bt_buscar" runat="server" class="btn btn-success" Text="Buscar" 
        onclick="bt_buscar_Click" style="height: 26px" />
    </td>
<td></td>
</tr>
<tr>
    <td></td>
    <td>
        <asp:Label ID="Label5" runat="server" Text="Responsable:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tx_responsable" class="form-control" runat="server" 
            Width="400px"></asp:TextBox>
>>>>>>> origin/modulo3
        <asp:AutoCompleteExtender ID="tx_responsable_AutoCompleteExtender" 
            runat="server" TargetControlID="tx_responsable"
             MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2"
             CompletionListCssClass="CompletionList"  CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
<<<<<<< HEAD
        </asp:AutoCompleteExtender> </td>
</tr>
<tr>    
    <td> <asp:Label ID="Label6" runat="server" Text="Producto:"></asp:Label> </td>
    <td> <asp:TextBox ID="tx_producto" class="form-control" runat="server" Width="400px"></asp:TextBox>
=======
        </asp:AutoCompleteExtender>
    </td>
    <td></td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td></td>
    <td>
        <asp:Label ID="Label6" runat="server" Text="Producto:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tx_producto" class="form-control" runat="server" Width="400px"></asp:TextBox>
>>>>>>> origin/modulo3
        <asp:AutoCompleteExtender ID="tx_producto_AutoCompleteExtender" runat="server" 
            TargetControlID="tx_producto"
            CompletionSetCount="12" 
                    MinimumPrefixLength="1" ServiceMethod="GetlistaProductos" 
                    UseContextKey="True"
                    CompletionListCssClass="CompletionList" 
                    CompletionListItemCssClass="CompletionlistItem" 
                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
<<<<<<< HEAD
            ></asp:AutoCompleteExtender> </td>    
=======
            >
        </asp:AutoCompleteExtender>
    </td>
    <td></td>
    <td></td>
    <td></td>
>>>>>>> origin/modulo3
</tr>
</table>

<table>
<tr>
<<<<<<< HEAD
<td> <asp:Label ID="Label3" runat="server" Text="Desde:"></asp:Label> </td>
<td><asp:TextBox ID="tx_desdeFecha" class="form-control" runat="server" 
        Width="150px"></asp:TextBox>
    <asp:CalendarExtender ID="tx_desdeFecha_CalendarExtender" runat="server" 
        TargetControlID="tx_desdeFecha">
    </asp:CalendarExtender> </td>
<td> <asp:Label ID="Label4" runat="server" Text="Hasta:"></asp:Label> </td>
<td> <asp:TextBox ID="tx_hastaFecha" class="form-control" runat="server" Width="150px"></asp:TextBox>
    <asp:CalendarExtender ID="tx_hastaFecha_CalendarExtender" runat="server" 
        TargetControlID="tx_hastaFecha">
    </asp:CalendarExtender> </td>
=======
<td></td>
<td>
    <asp:Label ID="Label3" runat="server" Text="Desde:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_desdeFecha" class="form-control" runat="server" 
        Width="150px"></asp:TextBox>
    <asp:CalendarExtender ID="tx_desdeFecha_CalendarExtender" runat="server" 
        TargetControlID="tx_desdeFecha">
    </asp:CalendarExtender>
    </td>
<td></td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Hasta:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_hastaFecha" class="form-control" runat="server" 
        Width="150px"></asp:TextBox>
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
    &nbsp;</td><td>
        &nbsp;</td><td></td><td></td><td></td>
>>>>>>> origin/modulo3
</tr>
</table>

</div>
<<<<<<< HEAD
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
    <li class="list-group-item">JYC</li>
  </ul>
</div>


=======
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
>>>>>>> origin/modulo3

</asp:Content>
