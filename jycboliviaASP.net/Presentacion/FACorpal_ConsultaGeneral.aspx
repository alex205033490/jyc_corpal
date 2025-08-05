<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FACorpal_ConsultaGeneral.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ConsultaGeneral" %>
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
                <div class="CR_consulta">

<table>
<tr>
<td><asp:Label ID="Label2" runat="server" Text="Consulta:"></asp:Label></td>
<td>
    <asp:DropDownList ID="dd_consulta" class="btn btn-secondary dropdown-toggle" runat="server">        
        <asp:ListItem>Corresponde Almuerzo de 5 a 6am Marcacion</asp:ListItem>                
        <asp:ListItem>Marcacion Personal</asp:ListItem>   
    </asp:DropDownList>
    </td>
<td><asp:Button ID="bt_buscar" runat="server" class="btn btn-success" Text="Buscar"  onclick="bt_buscar_Click"  />    </td>
</tr>
</table>

<table>
<tr>
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
</tr>
<tr>
<td> <asp:Label ID="Label1" runat="server" Text="Hora Desde:"></asp:Label> </td>
<td><asp:TextBox ID="txtHoradesde" runat="server" class="form-control"  MaxLength="5" placeholder="HH:mm" />
<asp:RegularExpressionValidator ID="revHora" runat="server"
    ControlToValidate="txtHoradesde"
    ValidationExpression="^([01]?[0-9]|2[0-3]):[0-5][0-9]$"
    ErrorMessage="Hora inválida"
    ForeColor="Red" /></td>
<td> <asp:Label ID="Label5" runat="server" Text="Hora Hasta:"></asp:Label> </td>
<td><asp:TextBox ID="txtHorahasta" runat="server" class="form-control"  MaxLength="5" placeholder="HH:mm" />
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
    ControlToValidate="txtHorahasta"
    ValidationExpression="^([01]?[0-9]|2[0-3]):[0-5][0-9]$"
    ErrorMessage="Hora inválida"
    ForeColor="Red" /></td>
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
    <li class="list-group-item">JYC</li>
  </ul>
</div>



</asp:Content>
