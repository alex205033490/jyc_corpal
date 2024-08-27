<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_PagoSeguimiento.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_PagoSeguimiento" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/Styles_GPagoSeguimiento.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
  <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>
 
 <div class="Centrar">
 <div class="titulo">
<h3> 
    <asp:Label ID="lb_titulo" runat="server" Text="Gestion de Pago del Seguimiento"></asp:Label> </h3> 
 </div>

<div class="PS1_busqueda">
<table>
<tr>
    <td>
        <asp:Label ID="Label1" runat="server" Text="Exbo :"></asp:Label>
        <asp:TextBox ID="tx_Exbo" runat="server"></asp:TextBox>
    </td>
    <td></td>
    <td>
        <asp:Label ID="Label2" runat="server" Text="Nombre Proyecto :"></asp:Label>
        <asp:TextBox ID="tx_nombreProyecto" runat="server"></asp:TextBox>
    </td>
    <td></td>
    <td>
        <asp:Button ID="bt_buscar" runat="server" Text="Buscar" Height="35px" 
            onclick="bt_buscar_Click" />
    </td>
</tr>
</table>
</div>
<div class="PS1">
<p>Equipos</p>
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
</div>

<table>
<tr>
<td>
<div class="PS2">
<p>Seguimiento Equipo</p>
    <asp:GridView ID="gv_SeguimientoMantenimiento" runat="server" 
        BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
        CellPadding="4" CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onselectedindexchanged="gv_SeguimientoMantenimiento_SelectedIndexChanged">
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

</td>
<td>
<div class="PS3">
<p>Meses Pago</p>
    <asp:GridView ID="gv_seguiMes" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onrowcancelingedit="gv_seguiMes_RowCancelingEdit" 
        onrowediting="gv_seguiMes_RowEditing" 
        onrowupdating="gv_seguiMes_RowUpdating" 
        onselectedindexchanged="gv_seguiMes_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField EditText="Recibos" SelectText="Recibos" 
                ShowSelectButton="True" />
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
<td></td>
<td>&nbsp;</td>
</tr>

</table>

<div class="PS4">
    <asp:Label ID="Label3" runat="server" Text="Recibos"></asp:Label>
    <asp:GridView ID="gv_recibos" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onselectedindexchanged="gv_recibos_SelectedIndexChanged">
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
<div class="PS5"></div>

 </div>


</asp:Content>
