<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_GestionarSeguimiento.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_GestionarSeguimiento" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/Style_GSeguimiento.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

   <div class = "Centrar">
   <div class = "titulo">
    <h3>Gestionar Seguimiento</h3>
   </div>

<div class="GSbuscador">
<table>
<tr>
    <td> 
        <asp:Label ID="Label1" runat="server" Text="Exbo :"></asp:Label>
        <asp:TextBox ID="tx_Exbo" runat="server"></asp:TextBox>
    </td>
    <td></td>
    <td>
        <asp:Label ID="Label2" runat="server" Text="Nombre Proyecto:"></asp:Label>
        <asp:TextBox ID="tx_nombreProyecto" runat="server"></asp:TextBox>
    </td>
    <td></td>
    <td>
        <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
            onclick="bt_buscar_Click" Height="35px" />
    </td>
</tr>
</table>

</div>
    
    <div class="GS1">
    <asp:GridView ID="gv_equipos" runat="server" AllowPaging="True" 
        BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
        CellPadding="4" CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onpageindexchanging="gv_equipos_PageIndexChanging" 
        onselectedindexchanged="gv_equipos_SelectedIndexChanged" PageSize="7">
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
</div>
<div class="GS2"></div>




   </div>
 




</asp:Content>
