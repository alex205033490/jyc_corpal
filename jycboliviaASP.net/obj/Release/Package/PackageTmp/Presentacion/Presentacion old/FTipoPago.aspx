<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FTipoPago.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FTipoPago" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_GTipoPago.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style1
        {
            width: 4px;
        }
        .style2
        {
            width: 90px;
        }
        .style3
        {
            height: 30px;
        }
        .style4
        {
            width: 90px;
            height: 30px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>


<div class="Centrar">
<div class="titulo"><h3>GESTIONAR TIPO DE PAGO</h3></div>

<div class ="GTP1">
 
<table style="margin-left: 18px; width: 285px;">
        <tr>
        <td class="style1">
            <asp:Label ID="Label1" runat="server" Text="Nombre" Font-Size="Small"></asp:Label>
        </td>
        <td class="style1">
            <asp:TextBox ID="txtNombre" runat="server" Width="199px"></asp:TextBox>
        </td>
        <td class="style1"></td>
        </tr>
        
        </table>
<table style="width: 290px; margin-left: 14px">
<tr>
<td class="style3">
            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" 
                onclick="btnNuevo_Click" Width="80px" />
            </td>
<td class="style3">
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" 
                onclick="btnRegistrar_Click" />
            </td>
<td class="style4">
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" 
                onclick="btnModificar_Click" />
            </td>
</tr>
</table>

</div>

<div class="GTP2">
    <asp:GridView ID="GridView1" runat="server" 
        onrowdeleting="GridView1_RowDeleting1" DataKeyNames="codigo" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" AllowPaging="True" 
        Height="84px" style="margin-left: 0px; margin-top: 1px" Width="351px" 
        BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
        CellPadding="4" CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
        <Columns>
            <asp:CommandField ShowSelectButton="True" >
            <FooterStyle ForeColor="Black" />
            </asp:CommandField>
<asp:TemplateField><ItemTemplate>
                 <asp:LinkButton ID="lkEliminar" CommandName="Delete" runat="server" 
                     Text = "Eliminar" OnClientClick = "return confirm('Esta seguro de eliminar?')" 
                     CausesValidation="False" ForeColor="Black"> </asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
        </Columns>

        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />

        <SelectedRowStyle BackColor="#00CC00" Font-Bold="True" ForeColor="White" />

        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />

    </asp:GridView>
</div>

<div class = "GTP3"></div>

</div>







</asp:Content>
