<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CargaFechaExpedicion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CargaFechaExpedicion" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_CargaFechaExpedicion.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

<div class="Centrar">
<div class="titulo">
<h3>Carga de Fecha de Expedicion</h3>
</div>
<div class="CargaArchivo">
    <asp:FileUpload ID="FileUpload1" runat="server" BackColor="#CCCCCC" 
        Font-Size="Medium" Height="24px" Width="522px" />
    <asp:Button ID="bt_CargaFechaExp" runat="server" Text="CargarFechaExpedicion" 
        onclick="Button1_Click" Height="30px" />
</div>

<div class="OpcionesBusqueda">
<table>
<tr>
    <td>
    <asp:Label ID="Label2" runat="server" Text="Departamento :"></asp:Label>
    </td>
    <td></td>
    <td>
        <asp:DropDownList ID="dd_Departamento" runat="server" Height="20px" 
            Width="180px">  </asp:DropDownList>  
    </td>
    <td></td>
    <td>
        <asp:Button ID="bt_buscar" runat="server" Text="Buscar" Height="25px" 
            onclick="bt_buscar_Click" Width="90px" />
    </td>
</tr>
</table>
    
</div>
<div class="CFE1">    
    <asp:GridView ID="gv_fechaExpedicion" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
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

<div class="CFE2">
    <asp:Label ID="Label1" runat="server" Text="Total de Equipos :"></asp:Label>
    <asp:TextBox ID="tx_totalEquipos" runat="server"></asp:TextBox>
</div>





</div>



    



</asp:Content>
