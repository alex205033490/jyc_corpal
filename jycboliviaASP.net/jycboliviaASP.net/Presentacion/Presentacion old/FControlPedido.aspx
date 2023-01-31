<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FControlPedido.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FControlPedido" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ControlPedido.css" rel="stylesheet" type="text/css" />

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
                        
        .style1
        {
            width: 128px;
        }
        .style3
        {
            width: 127px;
        }
        .style4
        {
            width: 58px;
        }
                        
    .style7
    {
        width: 86px;
    }
    .style8
    {
        width: 75px;
    }
    .style9
    {
        width: 84px;
    }
                        
        .style10
        {
            width: 113px;
        }
                        
        .style11
        {
            width: 47px;
        }
        .style12
        {
            width: 184px;
        }
                        
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>
       <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

<div class="Centrar">
<table>
<tr>
<td>
<div class="titulo">
    <h3>
        <asp:Label ID="lb_titulo" runat="server" Text="Control de Pedido"></asp:Label></h3>
</div>
</td>
</tr>


<tr>
<td>
<div class="GCP1">
<table style="height: 188px; width: 998px; margin-left:5px; margin-top: 0px; margin-right: 0px;">
<tr>
<td class="style8">
    <asp:Label ID="Label1" runat="server" Text="Proyecto:" Font-Size="X-Small"></asp:Label>
</td>
<td class="style1"> 
        <asp:TextBox ID="tx_Proyecto" runat="server" Font-Size="Small" Width="120px" ></asp:TextBox>
        <asp:AutoCompleteExtender ID="tx_Proyecto_AutoCompleteExtender" runat="server" 
             CompletionSetCount="12" MinimumPrefixLength="1" ServiceMethod="getListaProyecto" 
            TargetControlID="tx_Proyecto"
            UseContextKey="True"
            CompletionListCssClass="CompletionList" 
            CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
            >
        </asp:AutoCompleteExtender>
</td>
   
    <td class="style4">
    <asp:Label ID="Label2" runat="server" Text="Exbo:" Font-Size="X-Small"></asp:Label>
    </td>
    <td class="style1">
        <asp:TextBox ID="txbExbo" runat="server" Font-Size="Small" 
            Width="120px"></asp:TextBox>
        <asp:AutoCompleteExtender ID="txbExbo_AutoCompleteExtender" runat="server" 
            CompletionSetCount="12" MinimumPrefixLength="1" ServiceMethod="getListaEquipo" 
            TargetControlID="txbExbo"  UseContextKey="True"
            CompletionListCssClass="CompletionList" 
            CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
            >
        </asp:AutoCompleteExtender>
    </td>
   

    <td class="style7">
    <asp:Label ID="Label8" runat="server" Text="Cliente:" Font-Size="X-Small"></asp:Label>
    </td><td class="style1">
        <asp:TextBox ID="txbCliente" runat="server" Font-Size="Small" 
            Width="120px" style="margin-left: 0px"></asp:TextBox>
            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" 
            TargetControlID="txbCliente" MinimumPrefixLength="1" 
            ServiceMethod="getListaEncargadoPago"
             CompletionListCssClass="CompletionList" 
            CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
            >
        </asp:AutoCompleteExtender>

    </td>
        
    <td class="style10">
        <asp:CheckBox ID="cbxr110" runat="server" Font-Size="X-Small" Text="R-110" />
       </td>
       <td>
        <asp:CheckBox ID="cbxr148" runat="server" Font-Size="X-Small" Text="R-148" />
        </td>
       <td>
        <asp:CheckBox ID="cbxr106" runat="server" Font-Size="X-Small" Text="R-106" />
       </td>
       <td></td>
</tr>
<tr>

<td class="style8">
    <asp:Label ID="Label4" runat="server" Text="Pasajero:" Font-Size="X-Small"></asp:Label>
    </td>
    <td class="style11">
    <asp:TextBox ID="txbPasajero" runat="server" Font-Size="Small" 
            Width="120px"></asp:TextBox>
    </td>
    
    <td class="style4">
    <asp:Label ID="Label7" runat="server" Text="Parada:" Font-Size="X-Small"></asp:Label>
    </td>
    <td class="style3">
    <asp:TextBox ID="txbParada" runat="server" Font-Size="Small" 
            Width="120px"></asp:TextBox>
    </td>
    
       <td class="style7">
    <asp:Label ID="Label5" runat="server" Text="Modelo:" Font-Size="X-Small"></asp:Label>
    </td>
          <td class="style15">
    <asp:TextBox ID="txbModelo" runat="server" Font-Size="Small" 
                  Width="120px"></asp:TextBox>
    </td>
       <td class="style10">
                 <asp:CheckBox ID="cbxr107" runat="server" Font-Size="X-Small" Text="R-107" />
    </td>
     <td>
                 <asp:CheckBox ID="cbxr109" runat="server" Font-Size="X-Small" Text="R-109" />
       </td>
       <td>
       <asp:CheckBox ID="cbxr113" runat="server" Font-Size="X-Small" Text="R-113" />
    </td>
       <td></td>
</tr>
<tr>
    <td class="style8">
                    <asp:Label ID="Label10" runat="server" Text="Velocidad:" 
        Font-Size="X-Small"></asp:Label>
        </td>
    <td class="style11">
                       <asp:TextBox ID="tx_velocidad" runat="server" Font-Size="Small" 
                           Width="120px"></asp:TextBox>
    </td>
 
    <td class="style4">
    <asp:Label ID="Label3" runat="server" Text="Estado:" Font-Size="X-Small"></asp:Label>
    </td>
        <td>
    <asp:DropDownList ID="dd_Estado" runat="server" Width="120px" Font-Size="Small">
    </asp:DropDownList>
    </td>
  
   <td class="style7">
        <asp:Label ID="Label11" runat="server" Font-Size="X-Small" 
            Text="Pago Contrato:"></asp:Label>
    </td>
   <td class="style15">
    <asp:TextBox ID="txbPagoContrato" runat="server" Font-Size="Small" 
           Width="120px">0</asp:TextBox>
    </td>
    
   <td class="style10">
       <asp:CheckBox ID="cbxprimerpago" runat="server" Font-Size="X-Small" 
           Text="Primer Pago" />
    </td>
     <td></td>
       <td></td>
       <td></td>
</tr>
<tr>
<td class="style8">
                    <asp:Label ID="Label18" runat="server" Font-Size="X-Small" Text="VC:"></asp:Label>
</td>
<td>
                       <asp:TextBox ID="tx_vc" runat="server" Width="120px"></asp:TextBox>
    </td>

<td class="style4">
    <asp:Label ID="Label13" runat="server" Font-Size="X-Small" Text="Fichero:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_fichero" runat="server" Width="120px" 
        Font-Size="Small"></asp:TextBox>
    </td>

<td class="style7">
    <asp:Label ID="Label14" runat="server" Font-Size="X-Small" Text="Fecha Venta:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_fechaContrato" runat="server" Font-Size="Small" 
        Width="120px"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaContrato_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaContrato">
    </asp:CalendarExtender>
    </td>

<td class="style10">
    <asp:CheckBox ID="ChxventaContrato" runat="server" Font-Size="X-Small" 
        Text="Venta Contrato" />
    </td>
     <td></td>
       <td></td>
       <td></td>
</tr>

<tr>
<td class="style8">
    <asp:Label ID="Label24" runat="server" Font-Size="X-Small" 
        Text="Valor Transporte Maritimo:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_valorTransporteMaritimo" runat="server" 
        Width="120px">0</asp:TextBox>
    </td>

<td class="style4">
    <asp:Label ID="Label23" runat="server" Font-Size="X-Small" 
        Text="Fecha Aprox. Embarque:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_fechaAproxEmbarque" runat="server" 
        Width="120px"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaAproxEmbarque_CalendarExtender" 
        runat="server" TargetControlID="tx_fechaAproxEmbarque">
    </asp:CalendarExtender>
    </td>


<td class="style7">
    <asp:Label ID="Label25" runat="server" Font-Size="X-Small" 
        Text="Fecha Pago de Embarque:"></asp:Label>
    </td>
<td>
        <asp:TextBox ID="tx_fechaPagoEmbarque" runat="server" Width="120px"></asp:TextBox>
        <asp:CalendarExtender ID="tx_fechaPagoEmbarque_CalendarExtender" runat="server" 
            TargetControlID="tx_fechaPagoEmbarque">
        </asp:CalendarExtender>
    </td>


    <td class="style10">
        <asp:CheckBox ID="cb_contratoFirmado" runat="server" Font-Size="X-Small" 
            Text="Contrato Firmado" />
        <br />
    </td>
     <td></td>
       <td></td>
       <td></td>
</tr>

<tr>
<td>
    <asp:Label ID="Label19" runat="server" Font-Size="X-Small" Text="Tipo Equipo:"></asp:Label>
    </td>
<td>
    <asp:DropDownList ID="dd_tipoEquipo" runat="server" Width="120px">
    </asp:DropDownList>
    </td>
<td>
    <asp:Label ID="Label20" runat="server" Font-Size="X-Small" Text="Marca:"></asp:Label>
    </td>
<td>
    <asp:DropDownList ID="dd_marca" runat="server" Width="120px">
    </asp:DropDownList>
    </td>
<td>
    <asp:Label ID="Label21" runat="server" Font-Size="X-Small" Text="Venta:"></asp:Label>
    </td>
<td>
        <asp:DropDownList ID="dd_ciudadVenta" runat="server" 
        Width="120px">
            <asp:ListItem>Ninguno</asp:ListItem>
        <asp:ListItem>Santa Cruz</asp:ListItem>
            <asp:ListItem>Cochabamba</asp:ListItem>
            <asp:ListItem Value="Chuquisaca">Chuquisaca</asp:ListItem>
            <asp:ListItem>La Paz</asp:ListItem>
            <asp:ListItem>Oruro</asp:ListItem>
            <asp:ListItem>Potosi</asp:ListItem>
            <asp:ListItem>Beni</asp:ListItem>
            <asp:ListItem>Tarija</asp:ListItem>
            <asp:ListItem>Pando</asp:ListItem>
            <asp:ListItem>Asuncion-Paraguay</asp:ListItem>
        </asp:DropDownList>
    </td>
<td class="style10">
        <asp:CheckBox ID="chbx_stock" runat="server" Font-Size="X-Small" 
            Text="Inventario" />
        </td>
 <td></td>
       <td></td>
       <td></td>
</tr>

<tr>
<td class="style8">
    <asp:Label ID="Label26" runat="server" Font-Size="X-Small" 
        Text="Equipo Entregado Segun Contrato:"></asp:Label>
    </td>



<td>
    <asp:TextBox ID="tx_equipoEntregadoSegunContrato" runat="server" Width="120px"></asp:TextBox>
    <asp:CalendarExtender ID="tx_equipoEntregadoSegunContrato_CalendarExtender" 
        runat="server" TargetControlID="tx_equipoEntregadoSegunContrato">
    </asp:CalendarExtender>
    </td>
<td class="style4">
    <asp:Label ID="Label27" runat="server" Font-Size="X-Small" 
        Text="Codigo de Contrato:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_CodigoContrato" runat="server" Width="120px"></asp:TextBox>
    </td>

<td class="style7">
    <asp:Label ID="Label22" runat="server" Font-Size="X-Small" Text="Instalacion:"></asp:Label>
    </td>
<td>
        <asp:DropDownList ID="ddlCiudad" runat="server" Font-Size="Small" Width="120px">
            <asp:ListItem>Ninguno</asp:ListItem>
            <asp:ListItem>Santa Cruz</asp:ListItem>
            <asp:ListItem>Cochabamba</asp:ListItem>
            <asp:ListItem>Chuquisaca</asp:ListItem>
            <asp:ListItem>La Paz</asp:ListItem>
            <asp:ListItem>Oruro</asp:ListItem>
            <asp:ListItem>Potosi</asp:ListItem>
            <asp:ListItem>Beni</asp:ListItem>
            <asp:ListItem>Tarija</asp:ListItem>
            <asp:ListItem>Pando</asp:ListItem>
            <asp:ListItem>Asuncion-Paraguay</asp:ListItem>
        </asp:DropDownList>
    </td>

<td class="style10">
    <asp:Label ID="Label31" runat="server" Font-Size="X-Small" 
        Text="Identificador Ascensor:"></asp:Label>
    </td>
 <td>
     <asp:TextBox ID="tx_idAscensor" runat="server"></asp:TextBox>
    </td>
       <td></td>
       <td></td>
</tr>

<tr>
<td class="style8">
    <asp:Label ID="Label16" runat="server" Font-Size="X-Small" Text="Desde:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_FechaDesde" runat="server" Width="120px"></asp:TextBox>
    <asp:CalendarExtender ID="tx_FechaDesde_CalendarExtender" runat="server" 
        TargetControlID="tx_FechaDesde">
    </asp:CalendarExtender>
    </td>

<td class="style4">
    <asp:Label ID="Label17" runat="server" Font-Size="X-Small" Text="Hasta :"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_fechaHasta" runat="server" Width="120px"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaHasta_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaHasta">
    </asp:CalendarExtender>
    </td>

<td class="style7">
    <asp:Label ID="Label30" runat="server" Font-Size="X-Small" 
        Text="Consignatario:"></asp:Label>
    </td>
<td>
        <asp:DropDownList ID="dd_consignatario" runat="server" Width="120px">
            <asp:ListItem>Ninguno</asp:ListItem>
            <asp:ListItem>Interlogy</asp:ListItem>
            <asp:ListItem>Melevar</asp:ListItem>
            <asp:ListItem>Elevamerica</asp:ListItem>
            <asp:ListItem>JYCIA</asp:ListItem>
            <asp:ListItem>JYC</asp:ListItem>
            <asp:ListItem>Cliente</asp:ListItem>
        </asp:DropDownList>
    </td>

<td class="style10">
    <asp:Label ID="Label32" runat="server" Font-Size="X-Small" 
        Text="Empresa Contrato:"></asp:Label>
    </td>

    <td>
        <asp:DropDownList ID="dd_empresacontratoproyecto" runat="server" Width="120px">
            <asp:ListItem>Ninguno</asp:ListItem>
            <asp:ListItem>JYCIA</asp:ListItem>
            <asp:ListItem>JYC</asp:ListItem>
            <asp:ListItem>Interlogi</asp:ListItem>
            <asp:ListItem>Melevar</asp:ListItem>
            <asp:ListItem>Elevamerica</asp:ListItem>
        </asp:DropDownList>
    </td>
       <td></td>
       <td></td>
</tr>
 <tr>
 <td>
     &nbsp;</td>
 <td>
     &nbsp;</td>
 <td>
     &nbsp;</td>
 <td>
     &nbsp;</td>
 <td></td>
 <td></td>
 <td></td>
 <td></td>
 <td></td>
 <td></td>
 </tr>


</table>
 
<table style="width: 505px">
<tr>
    <td>
        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" 
            onclick="btnLimpiar_Click" />
    </td>
    <td class="style9">
        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" 
            onclick="btnRegistrar_Click" />
    </td>
    <td>
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" 
            onclick="btnModificar_Click" />        
    </td>

    <td>    
            <asp:Button ID="bt_Eliminar" runat="server" Text="Eliminar" 
                onclick="bt_Eliminar_Click" />
    </td>
    <td>
        <asp:Button ID="bt_buscar"  runat="server"  Text="Buscar" 
            onclick="bt_buscar_Click" />
    </td>
  <td class="style12">
  
    <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="XX-Small" 
        onclick="LinkButton1_Click">Lista Maestra Equipos</asp:LinkButton>
  
  </td>

</tr>

</table>

</div>

</td>
</tr>


<tr>
<td>
<div class = "GCP3">
    <asp:GridView ID="gvControlPedido" runat="server" Height="97px" 
        style="margin-top: 0px" Width="322px" Font-Size="X-Small" 
        onselectedindexchanged="gvControlPedido_SelectedIndexChanged" 
        onrowdeleting="gvControl" BackColor="#CCCCCC" BorderColor="#999999" 
        BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" 
        ForeColor="Black" >
        <Columns>
            <asp:CommandField ShowSelectButton="True"  />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#99CC00" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
</div>

</td>
</tr>


<tr>
<td>
<div class = "GCP_otro">
<table>
<tr>
<td>
<asp:Button ID="bt_Exportar" runat="server" onclick="bt_Exportar_Click" 
        Text="Exportar Excel" />
</td>
<td></td>
<td>
    <asp:Label ID="Label15" runat="server" Text="Cantidad Equipos :"></asp:Label>
    </td>
<td></td>
<td>
    <asp:Label ID="lb_cantidad" runat="server" Text="0"></asp:Label>
    </td>
</tr>
</table>
    
    </div>

</td>
</tr>


</table>







</div>


</asp:Content>
