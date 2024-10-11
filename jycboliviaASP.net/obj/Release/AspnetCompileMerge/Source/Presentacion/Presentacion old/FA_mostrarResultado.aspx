<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_mostrarResultado.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_mostrarResultado" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_Consultas.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

 <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
    </div>

     
    <div class="Centrar">
    <div class="titulo"><h3>
        <asp:Label ID="lb_titulo" runat="server" Text="Label"></asp:Label></h3></div>
    
      <div class="consulta">
          <asp:GridView ID="gvConsultas" runat="server" BackColor="White" 
              BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
              Font-Size="Small" ForeColor="Black" GridLines="Vertical">
              <AlternatingRowStyle BackColor="#CCCCCC" />
              <FooterStyle BackColor="#CCCCCC" />
              <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
              <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
              <SortedAscendingCellStyle BackColor="#F1F1F1" />
              <SortedAscendingHeaderStyle BackColor="#808080" />
              <SortedDescendingCellStyle BackColor="#CAC9C9" />
              <SortedDescendingHeaderStyle BackColor="#383838" />
          </asp:GridView>
      </div>
    
    
    </div>


</asp:Content>
