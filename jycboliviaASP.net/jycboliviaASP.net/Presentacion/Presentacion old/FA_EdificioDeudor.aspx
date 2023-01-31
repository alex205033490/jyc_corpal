<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_EdificioDeudor.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_EdificioDeudor" EnableEventValidation = "false"  %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_EdificioDeudor.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

<div class = "Centrar">    
    <table>
    <tr>
    <td>
    <div class = "titulo">
     <h1>Gestionar Deuda Edificio</h1>
    </div>
    </td>        
    </tr>

    <tr>
    <td>
    <div class = "BuscarEdificio">
        <table>
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Size="Medium" Text="Edificio :"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_edificio" runat="server" Height="25px" Width="200px"></asp:TextBox>
            </td>
        <td></td>
        <td>
            <asp:Button ID="bt_buscar" runat="server" Height="25px" Text="Buscar" 
                Width="100px" onclick="bt_buscar_Click" />
            </td>
        <td></td>
        </tr>
        </table>
    </div>
    </td>
    </tr>

    <tr>
    <td>
    <div class="TablaEdificios">
        <asp:GridView ID="gv_tablaEdificios" runat="server" BackColor="#CCCCCC" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
            CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
            <Columns>
                <asp:TemplateField HeaderText="Instalacion">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="Repuesto">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox2" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
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
        <div>
            <table>
            <tr>
            <td></td>
            <td>
                <asp:Button ID="bt_marcados" runat="server" Text="Marcar" Height="25px" 
                    onclick="bt_marcados_Click" Width="100px" />
                </td>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Excel</asp:LinkButton>
                </td>
            </tr>
            </table>
        </div>
    </td>
    </tr>


    </table>


</div>

</asp:Content>
