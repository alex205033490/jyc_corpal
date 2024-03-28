<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FCorpal_GestionarPermisos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_GestionarPermisos" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/Style_GPermisos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 106px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div class="Centrar">
   <div class="titulo"><h1>Gestionar Permisos</h1></div>
   <div class="tituloGP1" ><h3>Responsables</h3>
   <table>
<tr>
<td class="style1"></td>
<td><asp:Label ID="Label1" runat="server" Text="Nombre Responsable :"></asp:Label></td>
<td></td>
<td>
    <asp:TextBox ID="tx_nombreResponsable" runat="server" Height="18px" 
        Width="251px"></asp:TextBox></td>
<td></td>
<td>
    <asp:Button ID="Button1" runat="server" Text="Buscar" onclick="Button1_Click" /></td>
</tr>
</table>
   </div>

<div class="GP1">
    <asp:GridView ID="GridView1" runat="server" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" 
        BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
        CellPadding="4" CellSpacing="2" Font-Size="Small" ForeColor="Black">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#33CC33" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
</div>

<table>
<tr>
<td>
<div class="tituloGP2" ><h3>Accesos de Responsable</h3></div>
    </td>
<td>
<div class="tituloGP3" ><h3>Acceso de Sistema</h3></div>

    </td>
</tr>
<tr>
<td><div class="GP2">
    <asp:GridView ID="gv_PermisosResp" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="Small" ForeColor="Black">
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
<Columns>
   <asp:TemplateField>
      <ItemTemplate>
         <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="False"></asp:CheckBox>
      </ItemTemplate>
   </asp:TemplateField>
</Columns>
    </asp:GridView>    
</div>
</td>
<td>

<div class="GP3">
    <asp:GridView ID="gv_AccesosSistema" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="Small" ForeColor="Black">
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
<Columns>
   <asp:TemplateField>
      <ItemTemplate>
         <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="False"></asp:CheckBox>
      </ItemTemplate>
   </asp:TemplateField>
</Columns>
    </asp:GridView>   
</div>
</td>
</tr>
<tr>
<td>








<div class="botonAccesosResp"><asp:Button ID="bt_eliminarAccesos" runat="server" 
        Text=" Eliminar Accesos Seleccionado" Height="35px" 
        onclick="bt_eliminarAccesos_Click" /></div>
    </td>
<td>
<div class="botonAccesosSistema"> <asp:Button ID="bt_addAccesos" runat="server" 
        Text="Adicionar Accesos Seleccionado" Height="35px" 
        onclick="bt_addAccesos_Click" /></div>
    </td>
</tr>
<tr>
<td></td>
<td></td>
</tr>
</table>


   </div>



















<div class="GP4"></div>

</asp:Content>
