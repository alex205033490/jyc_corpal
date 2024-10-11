<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_GCotizacionRepuesto.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_GCotizacionRepuesto" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_GcotizacionRepuesto.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 52px;
        }
        .style2
        {
            width: 92px;
        }
        .style3
        {
            height: 25px;
        }
        .style4
        {
            width: 92px;
            height: 25px;
        }
        .style5
        {
            width: 52px;
            height: 25px;
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
<div class="titulo"><h3>Cotizaciones de Repuestos</h3></div>

<table>
<tr>
<td>
<div class="menu1">
<table>
<tr>
<td class="style3"></td>
<td class="style4">
    <asp:Label ID="Label2" runat="server" Text="Codigo Cotizacion:" 
        Font-Size="Small"></asp:Label>
    </td>
<td class="style3">
    <asp:TextBox ID="tx_codigo" runat="server" Font-Size="Small"></asp:TextBox>
    </td>
<td class="style3">
    <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="Edificio:"></asp:Label>
    </td>
<td class="style5">
    <asp:TextBox ID="tx_edificio" runat="server" Width="200px"></asp:TextBox>
    </td>
</tr>
<tr>
<td></td>
<td class="style2">
    <asp:Label ID="Label1" runat="server" Text="Cite:" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_cite" runat="server" Width="100px"></asp:TextBox>
    </td>
<td>
    <asp:Label ID="Label5" runat="server" Font-Size="Small" Text="Estado Coti:"></asp:Label>
    </td>
<td class="style1">
    <asp:DropDownList ID="dd_estadoCoti" runat="server" Width="120px">
        <asp:ListItem>Abierto</asp:ListItem>
        <asp:ListItem>Cerrado</asp:ListItem>
        <asp:ListItem>Todos</asp:ListItem>
    </asp:DropDownList>
    </td>

</tr>

<tr>
<td></td>
<td>
    <asp:CheckBox ID="ckb_vendido" runat="server" Text="Vendido" 
        Font-Size="Small" />
    </td>
<td>
    <asp:CheckBox ID="ckb_rechazado" runat="server" Text="Rechazado" 
        Font-Size="Small" />
    </td>
<td></td>
<td>&nbsp;</td>

</tr>

<tr>
<td></td>
<td>
    <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
        onclick="bt_buscar_Click" />
    </td>
<td>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="MostrarCoti" Width="120px" />
    </td>
<td>
    <asp:Button ID="bt_R144" runat="server" onclick="bt_R144_Click" Text="R-144" 
        Width="100px" />
    </td>
<td>&nbsp;</td>

</tr>
<tr>
<td></td>
<td></td>
<td></td>
<td></td>
<td></td>

</tr>

</table>
</div>

</td>
<td>

<div class="vista1">

    <asp:GridView ID="gv_cotizacionRepuesto" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
        onselectedindexchanged="gv_cotizacionRepuesto_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#669900" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>  
</div>
   <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Excel</asp:LinkButton>
</td>
</tr>

<tr>
<td>

<div class = "medio">
<table>
<tr>
<td></td><td>
   
    <asp:Label ID="Label6" runat="server" Font-Size="Small" 
        Text="Contacto Con Cliente:"></asp:Label>
   
    </td><td>
    <asp:TextBox ID="tx_fechaContactoCliente" runat="server" Font-Size="Small"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaContactoCliente_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaContactoCliente">
    </asp:CalendarExtender>
    </td>

</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label7" runat="server" Font-Size="Small" 
        Text="Detalle Contacto Cliente:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_detalleContactoCliente" runat="server" Font-Size="Small" Height="50px" 
        TextMode="MultiLine" Width="350px"></asp:TextBox>
    </td>

</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label4" runat="server" Font-Size="X-Small" 
        Text="Detalle Cierre Cotizacion :"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_cierreCotizacion" runat="server" Height="50px" 
        Width="350px" TextMode="MultiLine"></asp:TextBox>
    </td>

</tr>

<tr>
<td></td>
<td>

    <asp:Button ID="bt_cerrarCotizacion" runat="server" Text="Cerrar Cotizacion" 
        onclick="bt_cerrarCotizacion_Click" />

    </td>
<td>

    &nbsp;</td>


</tr>
</table>
</div>

</td>
<td>

<div class="vista2">

    <asp:GridView ID="gv_repuestosAdicionados" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        onrowcreated="gv_repuestosAdicionados_RowCreated">
            
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>

</div>

</td>
</tr>

<tr>
<td></td>
<td></td>
</tr>

<tr>
<td></td>
<td></td>
</tr>
</table>


<div class="blanco">

</div>

</div>





</asp:Content>
