<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_GestionarEstadoEquipo.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_GestionarEstadoEquipo" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_GestionarEstadoMantenimiento.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

<div class="Centrar">
<div class="titulo">
<h1>Gestionar Estado Equipo</h1>
</div>


<table>
<tr>
<td>
<div class="GEE1">
  <table>
      <tr>
      <td></td>
      <td>
          <asp:Label ID="Label1" runat="server" Text="Exbo :"></asp:Label>
      </td>
      <td>
          <asp:TextBox ID="tx_Exbo" runat="server"></asp:TextBox>
      </td>
      <td></td>
      <td>
          <asp:Label ID="Label2" runat="server" Text="Nombre Proyecto:"></asp:Label>
      </td>
      <td>
          <asp:TextBox ID="tx_nombreProyecto" runat="server"></asp:TextBox>
      </td>
      <td></td>
      <td>
          <asp:Button ID="bt_Buscar" runat="server" Text="Buscar" 
              onclick="bt_Buscar_Click" />
      </td>               
      </tr>
  </table>
</div>

</td>
</tr>

<tr>
<td>
<div class="titulo2" ><h3>Equipos</h3></div>
</td>
</tr>


<tr>
<td>
<div class="GEE2">
    <asp:GridView ID="gv_equipos" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onselectedindexchanged="gv_equipos_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
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
    <br />
</div>

</td>
</tr>


<tr>
<td>
<div class="titulo3"><h3>Seguimiento</h3></div>
</td>
</tr>

<tr>
<td>
<div class="GEE11">
    <asp:GridView ID="gv_SeguiMantenimiento" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onselectedindexchanged="gv_SeguiMantenimiento_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" Font-Size="X-Small" />
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
<div class="GEE_tabla">
<table>
<tr>
<td><h3> Cambiar Estado <br />
 Equipo Equipo </h3></td>
<td>
<h3>Historial de Estados Seguimiento</h3>
</td>
</tr>
<tr>
<td>
<div class="GEE3">
<table>
<tr>
<td>
    <asp:Label ID="Label3" runat="server" Text="Exbo :"></asp:Label>
</td>
<td>
    <asp:TextBox ID="tx_exboView" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label4" runat="server" Text="Nombre Proyecto :"></asp:Label>
</td>
<td>
    <asp:TextBox ID="tx_NombreProyectoView" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>Estado Anterior :</td>
<td>
    <asp:TextBox ID="tx_EstadoAnteriorView" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>Nuevo Estado :</td>
<td>
    <asp:DropDownList ID="dd_nuevoEstado" runat="server" Height="17px" 
        Width="128px">
    </asp:DropDownList>
</td>
</tr>
<tr>
<td></td>
<td>
    <asp:Button ID="bt_ActualizarEstado" runat="server" Text="Actualizar Estado" 
        onclick="bt_ActualizarEstado_Click" />
</td>
</tr>

</table>
</div>
</td>

<td>
<div class="GEE4">
    <asp:GridView ID="gv_historialSeguimiento" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#669900" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="Gray" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
</div>

</td>
</tr>

</table>
</div>


</td>
</tr>


<tr>
<td>

<div class="GEE5">
</div>

</td>
</tr>


</table>






</div>

</asp:Content>
