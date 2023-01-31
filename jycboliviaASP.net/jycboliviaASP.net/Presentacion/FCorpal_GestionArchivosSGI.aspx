<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FCorpal_GestionArchivosSGI.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_GestionArchivosSGI" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_GestionArchivosSGI.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 

<div class="GA" >
 <table >
 <tr>
 <td>
 <div class="GA_titulo">
     <asp:Label ID="lb_titulo" runat="server" Text="Gestion Archivos SGI" 
         Font-Bold="True" Font-Size="X-Large"></asp:Label></div>
 </td>
 </tr>

 <tr>
 <td> 
 <div class="Buscador">
 <table>
  <tr>
 <td></td>
 <td><asp:Label ID="Label1" runat="server" Text="Nombre Doc:"></asp:Label></td>
 <td><asp:TextBox ID="tx_nombre" runat="server" Height="25px" Width="250px"></asp:TextBox></td>
 <td></td>
 <td><asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
         onclick="bt_buscar_Click" Height="25px" Width="100px" /></td>
 <td>
     <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
         Text="Mostrar Exporador" Height="25px" Width="150px" />
     </td>
 </tr>
 </table>
 </div>

 </td></tr>

 <tr>
 <td>
<div class="GA_tabla">
    
     <asp:GridView ID="gv_tabla" runat="server" BackColor="#CCCCCC" 
         BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
         CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
         onselectedindexchanged="gv_tabla_SelectedIndexChanged" 
         onrowdatabound="gv_tabla_RowDataBound">
         <Columns>
             <asp:CommandField ShowSelectButton="True" SelectText="Descargar" />
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

 <tr><td>
 <table>
 <tr><td>
 <div class="GA_Arbol">
 
     <asp:TreeView ID="TreeView1" runat="server" 
         CollapseImageUrl="~/Images/Good-mark.png" ExpandImageUrl="~/Images/Folder.png" 
         ImageSet="XPFileExplorer" NodeIndent="15" NoExpandImageUrl="~/Images/Text.png" 
         onselectednodechanged="TreeView1_SelectedNodeChanged">
         <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
         <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" 
             HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
         <ParentNodeStyle Font-Bold="False" />
         <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
             HorizontalPadding="0px" VerticalPadding="0px" />
     </asp:TreeView>
 </div>
     </td>
 </tr>
 </table>
 </td></tr>
 </table>
   

</div>
</asp:Content>



