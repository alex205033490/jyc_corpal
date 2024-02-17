<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_VistaResponsableProyecto.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_VistaResponsableProyecto" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_VistaResponsableProyecto.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

<div class="centrar">
<div class="titulo">
<h3> 
    <asp:Label ID="Label1" runat="server" Text="Responsables de Proyectos"></asp:Label> </h3>
</div>

<table style="margin: 0 auto;">
<tr>
<td>
    <table>
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Exbo:" Font-Size="Small"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_Exbo" runat="server" Height="25px" Width="120px"></asp:TextBox>
            </td>
        <td></td>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Edificio:" Font-Size="Small"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_Edificio" runat="server" Height="25px" Width="200px"></asp:TextBox>
            </td>
        <td></td>
        <td>
            <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="VariableSimec:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_variableSimec" runat="server"></asp:TextBox>
            </td>
        <td></td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td></td>
        <td>
            <asp:Button ID="bt_Buscar" runat="server" Text="Buscar" 
                onclick="bt_Buscar_Click1" />
            </td>
        <td></td>
        </tr>
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label4" runat="server" Text="Resp. Proyecto :" Font-Size="Small"></asp:Label>
            </td>
        <td>
            <asp:DropDownList ID="dd_RespProyecto" runat="server" Height="25px" 
                Width="120px">
            </asp:DropDownList>
            </td>
        <td></td>
        <td>
            <asp:Label ID="Label5" runat="server" Font-Size="Small" 
                Text="Tecnico Instalador:"></asp:Label>
            </td>
        <td>
            <asp:DropDownList ID="dd_TecnicoInstalador" runat="server" Height="25px" 
                Width="200px">
            </asp:DropDownList>
            </td>
        <td></td>
        <td>
            <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Estado Equipo:"></asp:Label>
            </td>
        <td>
            <asp:DropDownList ID="ddlEstadoEquipo" runat="server" Height="25px" 
                Width="120px">
            </asp:DropDownList>
            </td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        </tr>

        <tr>
        <td></td>
        <td></td>
        <td>
            <asp:CheckBox ID="cb_polizaSeguro" runat="server" Font-Size="Small" 
                Text="Poliza de Seguro" />
            </td>
        <td></td>
        <td>
            <asp:CheckBox ID="cb_boletaBancaria" runat="server" Font-Size="Small" 
                Text="Boleta Bancaria" />
            </td>
        <td>
            <asp:CheckBox ID="cb_letraCambio" runat="server" Font-Size="Small" 
                Text="Letra de Cambio" />
            </td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        
        </tr>


    </table>
</td>
</tr>

<tr>
<td>
<div class="tabla">

    <asp:GridView ID="gv_tabla" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
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

    <asp:Label ID="Label7" runat="server" Font-Size="Small" 
        Text="Cantidad de Equipos:"></asp:Label>
    <asp:TextBox ID="tx_cantidadEquipo" runat="server" Enabled="False"></asp:TextBox>

</td>
</tr>

<tr>
<td>

    <asp:Button ID="bt_excel" runat="server" Height="25px" onclick="bt_excel_Click" 
        style="margin-left: 24px" Text="Excel" Width="120px" />

</td>
</tr>
<tr>
<td>

</td>
</tr>

</table>

</div>

</asp:Content>
