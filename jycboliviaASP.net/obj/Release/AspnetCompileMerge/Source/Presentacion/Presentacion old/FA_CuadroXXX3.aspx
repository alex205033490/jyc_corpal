<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CuadroXXX3.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CuadroXXX3" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
<link href="../Styles/Style_CargaCuadrosXXX.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class = "menu">  
       <inmoInfo:menu runat="server"/>
   </div>
       
<div class="Centrar">
<div class="titulo"><h3>Carga de Cuadros de las XXX</h3></div>

 <div class="CargaArchivoXXX"> 
      <asp:FileUpload ID="FileUpload1" runat="server" Height="25px" Width="479px" />
      <asp:Button ID="bt_CargarCuadroXXX" runat="server" Text="Carga Cuadros XXX" 
          Height="25px" onclick="bt_CargarCuadroXXX_Click" />
  </div>

     <div class="CFE1XXX">     
         <asp:GridView ID="gv_CuadrosXXX" runat="server" BackColor="#CCCCCC" 
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

  <div class="CFE2XXX">
      <asp:Label ID="Label1" runat="server" Text="Total Equipos :"></asp:Label>
      <asp:TextBox ID="tx_TotalEquiposXXX" runat="server"></asp:TextBox>
  </div>



</div>

 
 
 
</asp:Content>
