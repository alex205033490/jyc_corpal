<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ViewSeguimientoInstalacion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ViewSeguimientoInstalacion" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_VistaSeguimientoInstalacion.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>


<div class = "Centrar">
<div class = "titulo"><h1>Vista Seguimiento Instalacion</h1></div>


<div class = "VSI1">
<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label1" runat="server" Text="Exbo / Serie :"></asp:Label>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_exbo" runat="server"></asp:TextBox>
</td>
<td></td>
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Nombre Edificio :"></asp:Label>
</td>
<td></td>
<td></td>
<td>
    <asp:TextBox ID="tx_nombreEdificio" runat="server"></asp:TextBox>
</td>
<td></td>
<td></td>
<td>
    <asp:Button ID="bt_Buscar" runat="server" Text="Buscar" 
        onclick="bt_Buscar_Click" />
</td>
<td></td>
<td></td>
</tr>
</table>
</div>



<div class = "vitaExcel">
    <asp:LinkButton ID="lk_excel" runat="server" onclick="lk_excel_Click">Excel</asp:LinkButton>
</div>

<div class = "VSI2">
    <asp:GridView ID="gv_seguimientoInstalacion" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <EditRowStyle Font-Size="X-Small" />
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
<div class = "VSI3"></div>



</div>







</asp:Content>
