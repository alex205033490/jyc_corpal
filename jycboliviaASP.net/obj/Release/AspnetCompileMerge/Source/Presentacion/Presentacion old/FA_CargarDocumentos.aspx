<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CargarDocumentos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CargarDocumentos" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_CargarDocumentos.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

   
<table style="margin: 0 auto;">
<tr>
<td> 
<div class="titulo"><h3>Carga de Docuementos</h3></div>
</td>
</tr>

<tr>
<td>
<div class="datosEdificio">
<table>
<tr>
<td></td>
<td>
    &nbsp;</td>
<td>
    <asp:Label ID="Label1" runat="server" Text="Edificio :" Font-Size="Small"></asp:Label>
    <asp:TextBox ID="tx_edificio" runat="server" Width="400px"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
        onclick="bt_buscar_Click" />
    </td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    &nbsp;</td>
<td>    
   <div class = "fakefile"> <asp:FileUpload ID="FileUpload1" runat="server" 
           Width="500px" BackColor="#CCCCCC"   /> </div>
    </td>
<td></td>
<td>
    <asp:Button ID="bt_cargarDocumentos" runat="server" 
        onclick="bt_cargarDocumentos_Click" Text="Cargar" />
    </td>
<td></td>
</tr>
</table>

</div>
</td>
</tr>

<tr>
<td>
<div class="edificios">
    <asp:GridView ID="gv_proyecto" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="XX-Small" ForeColor="Black" 
        onselectedindexchanged="gv_proyecto_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <EditRowStyle BackColor="#33CC33" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#33CC33" Font-Bold="True" ForeColor="White" />
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
<div class="documentos">
    <asp:GridView ID="gv_documentos" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onrowdeleting="gv_documentos_RowDeleting1" 
        onselectedindexchanged="gv_documentos_SelectedIndexChanged1">
        <Columns>
            <asp:CommandField HeaderText="Descarga" SelectText="Descargar" 
                ShowSelectButton="True" />
            <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="True" />
        </Columns>
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
<div class="pie"></div>
</td>
</tr>
</table>

  
</asp:Content>
