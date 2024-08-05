<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ViewSeguimientoMantenimientoXXX.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ViewSeguimientoMantenimientoXXX"  EnableEventValidation = "false" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ViewSeguimientoMantenimientoXXX.css" rel="stylesheet"
        type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

<div class="Centrar">
<div class="titulo">
<h3> 
    <asp:Label ID="tx_titulo" runat="server" Text="Seguimiento Mantenimiento"></asp:Label></h3>
</div>

<table>

<tr>
<td>
<div class="busqueda">

<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label1" runat="server" Text="Exbo :" Font-Size="Small" 
        Width="50px"></asp:Label>
    </td>
<td></td>
<td>
    <asp:TextBox ID="tx_Exbo" runat="server" Font-Size="Small" Height="25px" 
        Width="100px"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Edificio :" Font-Size="Small" 
        Width="70px"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tx_Edificio" runat="server" Font-Size="Small" Height="25px" 
            Width="120px"></asp:TextBox>
    </td>
    <td></td>
    <td>
        <asp:Label ID="Label3" runat="server" Text="Año :" Font-Size="Small" 
            Width="40px"></asp:Label>
    </td>
    <td></td>
    <td>
        <asp:DropDownList ID="dd_anio" runat="server" Font-Size="Small" Height="25px" 
            Width="70px">
        </asp:DropDownList>
    </td>
    <td></td>
    <td>
        <asp:Button ID="bt_Buscar" runat="server" Text="Buscar" 
            onclick="bt_Buscar_Click" />
    </td>
    <td></td>
</tr>

</table>

</div>
</td>
</tr>

<tr>
<td>
<div class="TablaVista">
    <asp:GridView ID="gv_tablaDatos" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
        <Columns>
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#669900" Font-Bold="True" ForeColor="White" />
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
<div class="pie1">
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Exportar Excel" />
</div>
</td>
</tr>

<tr>
<td>
<div class="titulo">
<h3>Estado Instalacion</h3>
</div>
</td>
</tr>

<tr>
<td>
<div class ="busqueda2">
<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Exbo:"></asp:Label>
    </td>
<td></td>
<td>
    <asp:TextBox ID="tx_exbo2" runat="server"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Label ID="Label5" runat="server" Text="Nombre Edificio :"></asp:Label>
    </td>
<td></td>
<td>
    <asp:TextBox ID="tx_nombreEdificio" runat="server" Width="200px"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Button ID="Button2" runat="server" Text="Buscar" Width="100px" 
        onclick="bt_buscar_Click" />
    </td>
<td></td>
</tr>
<tr>
<td></td>
<td>&nbsp;</td>
<td></td>
<td></td>
<td></td>
<td>
    <asp:Label ID="Label6" runat="server" Text="Estado :"></asp:Label>
    </td>
<td></td>
<td>
    <asp:DropDownList ID="dd_estadoEquipo" runat="server" Width="200px">
    </asp:DropDownList>
    </td>
<td></td>
<td></td>
<td></td>
</tr>     
</table>
</div>
</td>
</tr>

<tr>
<td>
<div class="TablaVista">
    <asp:GridView ID="gv_tablaEquipos" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
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
<td>
<div class="pie1">
    <asp:Button ID="bt_DescargarInstalacion" runat="server" Text="Exportar Excel" 
        onclick="bt_DescargarInstalacion_Click" />
</div>
</td>
</tr>

</table>



</div>   

</asp:Content>
