<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_saldosCuentasGeneral.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_saldosCuentasGeneral" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_saldosCuentasGeneral.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <div class="Centrar">
    <div class="titulo"><h3>Saldos Cuentas General</h3></div>
    <table>
    <tr>
    <td></td><td>
        <asp:Label ID="Label1" runat="server" Text="Desde:"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_fechaDesde" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaDesde_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaDesde">
            </asp:CalendarExtender>
        </td><td></td><td></td><td>
        <asp:Label ID="Label2" runat="server" Text="Hasta:"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_fechahasta" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechahasta_CalendarExtender" runat="server" 
                TargetControlID="tx_fechahasta">
            </asp:CalendarExtender>
        </td><td>
            <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
                onclick="bt_buscar_Click" />
        </td>
    </tr>
    <tr>
    <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
    </tr>  
    </table>

    <div class="SaldosCuentas">
        <asp:GridView ID="gv_saldosCuentasGeneral" runat="server" BackColor="White" 
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

    <div class="blanco">
        <asp:LinkButton ID="linkb_excel" runat="server" onclick="linkb_excel_Click">Excel</asp:LinkButton>
    </div>

    </div>

</asp:Content>
