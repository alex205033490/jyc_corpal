<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ImportacionFacturasContenedor_Transporte.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ImportacionFacturasContenedor_Transporte" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ImportacionJYCIAGeneral.css" rel="stylesheet" type="text/css" />

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
                        
        .style18
        {
            width: 48px;
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
    <td></td>
    <td>
        <asp:Label ID="Label33" runat="server" Font-Size="Small" Text="Nro Contenedor:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_nroContenedor" runat="server"></asp:TextBox>
         <asp:AutoCompleteExtender ID="tx_nroContenedor_AutoCompleteExtender" runat="server" 
                TargetControlID="tx_nroContenedor"
                CompletionSetCount="12" MinimumPrefixLength="1" ServiceMethod="getListaContenedor" 
                UseContextKey="True" CompletionListCssClass="CompletionList" 
                CompletionListItemCssClass="CompletionlistItem" 
                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10" >

            </asp:AutoCompleteExtender>

        </td>
    <td></td>
    <td>
        <asp:Button ID="bt_VerificarContenedor" runat="server" Text="Verificar" 
            onclick="bt_VerificarContenedor_Click" />
        </td>
    <td>
        <asp:Button ID="bt_limpiar1" runat="server" Text="Limpiar" 
            onclick="bt_limpiar1_Click" />
        </td>
    <td>
        <asp:Button ID="bt_actualizarContenedor" runat="server" Text="Actualizar" 
            onclick="bt_actualizarContenedor_Click" />
        </td>
    </tr>
</table>
<table>
    <tr>
        <td></td>
        <td>
            <asp:Label ID="Label34" runat="server" Font-Size="Small" 
                Text="Fecha Factura MSC:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_fechaMSC" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaMSC_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaMSC">
            </asp:CalendarExtender>
        </td>
        <td></td>
        <td>
            <asp:Label ID="Label35" runat="server" Font-Size="Small" 
                Text="Nro Factura MSC:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_nroMSC" runat="server"></asp:TextBox>
        </td>
        <td></td>
        <td>
            <asp:Label ID="Label36" runat="server" Font-Size="Small" Text="Monto MSC:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_montoMSC" runat="server"></asp:TextBox>
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Label ID="Label37" runat="server" Font-Size="Small" 
                Text="Fecha Factura ASP:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_fechaASP" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaASP_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaASP">
            </asp:CalendarExtender>
        </td>
        <td></td>
        <td>
            <asp:Label ID="Label39" runat="server" Font-Size="Small" 
                Text="Nro Factura ASPB:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_nroASPB" runat="server"></asp:TextBox>
        </td>
        <td></td>
        <td>
            <asp:Label ID="Label41" runat="server" Font-Size="Small" Text="Monto ASPB:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_montoASPB" runat="server"></asp:TextBox>
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Label ID="Label38" runat="server" Font-Size="Small" 
                Text="Fecha Factura THC:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_fechaTHC" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaTHC_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaTHC">
            </asp:CalendarExtender>
        </td>
        <td></td>
        <td>
            <asp:Label ID="Label40" runat="server" Font-Size="Small" 
                Text="Nro Factura THC:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_nroTHC" runat="server"></asp:TextBox>
        </td>
        <td></td>
        <td>
            <asp:Label ID="Label42" runat="server" Font-Size="Small" Text="Monto THC:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_montoTHC" runat="server"></asp:TextBox>
        </td>
        <td></td>
    </tr>

</table>



</div>

<div class="Centrar">
<table>
<tr>
<td>
<!--
<div class="titulo">
    <h3>
        <asp:Label ID="lb_titulo" runat="server" Text="Datos General Importacion"></asp:Label></h3>
</div>
-->
</td>
</tr>


<tr>
<td>
<div class="GCP1">
<div >
<table style="height: auto; width: auto; margin-left:5px; margin-top: 0px; margin-right: 0px;">
<tr>
<td>
    <asp:Label ID="Label31" runat="server" Text="Dui:" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_nroDui" runat="server"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Label ID="Label32" runat="server" Font-Size="Small" Text="Contenedor:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_contenedorBusqueda" runat="server"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Label ID="Label30" runat="server" Font-Size="Small" 
        Text="Semana Expedicion:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_sem_Exp" runat="server"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Label ID="Label25" runat="server" Font-Size="Small" Text="Edificio:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_edificio" runat="server" Width="250px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
<td class="style18">
    <asp:Label ID="Label26" runat="server" Font-Size="Small" Text="Exbo:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_exbo" runat="server" Width="120px" Font-Size="Small"></asp:TextBox>
    </td>
<td>
    &nbsp;</td>
<td> 
        <asp:Label ID="Label27" runat="server" Text="Estado:" Font-Size="Small"></asp:Label>
    </td>
        <td>
            <asp:DropDownList ID="dd_estado" runat="server" Width="250px">
            </asp:DropDownList>
    </td>
        <td></td>
<td> 
        <asp:Button ID="bt_buscar"  runat="server"  Text="Buscar" Height="25px" 
            onclick="bt_buscar_Click" />
    </td>
<td>
    &nbsp;</td>
<td> 
        <asp:Button ID="bt_Limpiar" runat="server" Height="25px" Text="Limpiar" 
            onclick="bt_Limpiar_Click" />
    </td>
</tr>

</table>
 </div>
</div>

</td>
</tr>


<tr>
<td>
<div class = "GCP3">   
    <asp:GridView ID="gv_datos" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="Small" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="Gray" />
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
    <asp:Button ID="bt_Update" runat="server" Text="Actualizar" />
    </td>
<td></td>
<td></td>
<td></td>
</tr>


<tr>
<td>
<asp:Button ID="bt_Exportar" runat="server" 
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
