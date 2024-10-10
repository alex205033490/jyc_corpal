<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_viewEstadisticaMantenimiento.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_viewEstadisticaMantenimiento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ViewEstadoMantenimiento.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>
  

<div class="Centrar">
<div class="titulo">
<h3>Estados de Mantemientos</h3>
</div>

<table>
<tr>
<td>
<div class="em2">
<table>
<tr>
<td>
    <asp:Label ID="Label1" runat="server" Text="De :"></asp:Label>
</td>
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Hasta :"></asp:Label>
</td>
<td></td>
<td>
    <asp:Label ID="Label7" runat="server" Text="Gestion :"></asp:Label>
    </td>
<td></td>
</tr>
<tr>
<td>    
    <asp:TextBox ID="tx_Fecha11" runat="server" Height="20px" Width="90px"></asp:TextBox>
    </td>
<td>
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tx_Fecha11">
    </asp:CalendarExtender>
</td>

<td>    
    <asp:TextBox ID="tx_Fecha12" runat="server" Height="20px" Width="90px"></asp:TextBox>
    </td>
<td>
    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tx_Fecha12" >
    </asp:CalendarExtender>

</td>
<td>
    <asp:DropDownList ID="dd_Gestion1" runat="server" Height="25px">
    </asp:DropDownList>
    </td>
<td>
    <asp:Button ID="bt_buscar1" runat="server" Text="Buscar" Height="25px" 
        onclick="bt_buscar1_Click" />
</td>
</tr>
</table>
</div>

</td>
<td>
<div class="em3">
<table>
<tr>
<td>
    <asp:Label ID="Label3" runat="server" Text="De :"></asp:Label>
</td>
<td></td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Hasta :"></asp:Label>
</td>
<td></td>
<td>
    <asp:Label ID="Label8" runat="server" Text="Gestion :"></asp:Label>
    </td>
<td></td>

</tr>
<tr>
<td>    
    <asp:TextBox ID="tx_Fecha21" runat="server" Height="20px" Width="90px"></asp:TextBox>
    </td>
<td>
    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="tx_Fecha21">
    </asp:CalendarExtender>
</td>

<td id="tx_f">
    <asp:TextBox ID="tx_Fecha22" runat="server" Height="20px" Width="90px"></asp:TextBox>
</td>
<td>
    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tx_Fecha22">
    </asp:CalendarExtender>
</td>

<td>
    <asp:DropDownList ID="dd_Gestion2" runat="server" Height="25px">
    </asp:DropDownList>
    </td>
<td>
<asp:Button ID="bt_buscar2" runat="server" Text="Buscar" Height="25px" 
        onclick="bt_buscar2_Click" />
</td>

</tr>

</table>
</div>

</td>
</tr>

<tr>
<td>
<div class="em4">

    <asp:GridView ID="gv_estadoM1" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
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
<td>
<div class="em5">

    <asp:GridView ID="gv_EstadoM2" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
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
<div class="em6">
<table>
<tr>
<td>
    <asp:Label ID="Label5" runat="server" Text="Equipos Funcionando : "></asp:Label>
</td>
<td></td>
<td>
    <asp:Label ID="lb_cantidadEquipo1" runat="server" Text="0"></asp:Label>
    </td>
</tr>
<tr>
<td>
    <asp:Label ID="Label9" runat="server" Text="Equipos Funcionando % :"></asp:Label>
</td>
<td></td>
<td>
    <asp:Label ID="lb_EquiposFuncionandoPorcentaje1" runat="server" Text="0"></asp:Label>
</td>

</tr>

<tr>
<td>
    <asp:Label ID="Label10" runat="server" Text="Total Equipos"></asp:Label>
</td>
<td></td>
<td>
    <asp:Label ID="lb_TotalEquipos1" runat="server" Text="0"></asp:Label>
</td>

</tr>

</table>
</div>
</td>
<td>
<div class="em7">
<table>
<tr>
<td>
    <asp:Label ID="Label6" runat="server" Text="Equipos Funcionando :"></asp:Label>
</td>
<td></td>
<td>
    <asp:Label ID="lb_cantidadEquipo2" runat="server" Text="0"></asp:Label>
    </td>
</tr>
<tr>
<td>
    <asp:Label ID="Label11" runat="server" Text="Equipos Funcionando % :"></asp:Label>
</td>
<td></td>
<td>
    <asp:Label ID="lb_EquipoPorcentaje2" runat="server" Text="0"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label12" runat="server" Text="Total Equipos :"></asp:Label>
</td>
<td></td>
<td>
    <asp:Label ID="lb_TotalEquipos2" runat="server" Text="0"></asp:Label>
</td>
</tr>
</table>
</div>
</td>
</tr>
</table>

<div class="em8"></div>

</div>


</asp:Content>
