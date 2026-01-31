<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_ConsutaProduccion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_ConsutaProduccion" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="card">
  <div class="card-header bg-success text-white"> Consulta </div>
  <ul class="list-group list-group-flush">
    <li class="list-group-item">
        <div class="row">
            <div class="col-md-12">
                <div class="CR_consulta" style="padding: 0.3rem; font-size: 0.8rem;">

                    <div class="container_campos col-lg-6">
                        <div class="filtroBusqueda mb-2">
                            <asp:Label ID="Label2" runat="server" Text="Consulta:"></asp:Label><br />
                            
                                <asp:DropDownList ID="dd_consulta" class="btn btn-secondary dropdown-toggle" Width="70%" runat="server">        
                                    <asp:ListItem>Entrega Produccion</asp:ListItem>        
                                    <asp:ListItem>Entrega Produccion Fecha Turno</asp:ListItem>
                                    <asp:ListItem>Objetivo Produccion vs Entrega Produccion con salida almacen</asp:ListItem>
                                    <asp:ListItem>Reporte de Ventas y Cumplimiento de Objetivos por Producto</asp:ListItem>
                                </asp:DropDownList>
                            <asp:Button ID="bt_buscar" runat="server" class="btn btn-success" Text="Buscar"  onclick="bt_buscar_Click"  />
                        </div>

                         <asp:Label ID="Label5" runat="server" Text="Responsable:"></asp:Label>
                         <asp:TextBox ID="tx_responsable" class="form-control mb-2" runat="server"  Width="300px"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="tx_responsable_AutoCompleteExtender" 
                                    runat="server" TargetControlID="tx_responsable"
                                     MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2"
                                     CompletionListCssClass="CompletionList"  CompletionListItemCssClass="CompletionlistItem" 
                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                                </asp:AutoCompleteExtender>
                        
                            <asp:Label ID="Label6" runat="server" Text="Producto:"></asp:Label> 
                            <asp:TextBox ID="tx_producto" class="form-control mb-2" runat="server" Width="300px"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="tx_producto_AutoCompleteExtender" runat="server" 
                                    TargetControlID="tx_producto"
                                    CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaProductos" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                                    ></asp:AutoCompleteExtender> 

                    </div>

                    <div class=" col-lg-5 row">
                        <div class="col-lg-6 col-md-4 col-5">
                            <asp:Label ID="Label3" runat="server" Text="Desde:"></asp:Label> 
                            <asp:TextBox ID="tx_desdeFecha" class="form-control" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="tx_desdeFecha_CalendarExtender" runat="server" 
                            TargetControlID="tx_desdeFecha">
                        </asp:CalendarExtender> 
                        </div>
                        <div class="col-lg-6 col-md-4 col-5">
                            <asp:Label ID="Label4" runat="server" Text="Hasta:"></asp:Label>
                            <asp:TextBox ID="tx_hastaFecha" class="form-control" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="tx_hastaFecha_CalendarExtender" runat="server" 
                                    TargetControlID="tx_hastaFecha">
                                </asp:CalendarExtender>
                        </div>

                    </div>



</div>
            </div>
        </div>
    </li>
    <li class="list-group-item">
        <div class="row">
            <div class="col-md-12">
                <div class="CR_datos">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1200px" 
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


</asp:Content>
