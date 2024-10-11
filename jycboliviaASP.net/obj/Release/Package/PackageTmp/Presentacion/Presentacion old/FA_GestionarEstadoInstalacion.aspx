<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_GestionarEstadoInstalacion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_GestionarEstadoInstalacion" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_GestionarEstadoInstalacion.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
</div>

 <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

<div class = "Centrar">
<div class = "titulo"> 
    <h3> 
        <asp:Label ID="lb_Titulo" runat="server" Text="Gestionar Estado de Instalacion"></asp:Label> </h3>
</div>

<div class ="buscador">
<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label1" runat="server" Text="Exbo:"></asp:Label>
    </td>
<td></td>
<td>
    <asp:TextBox ID="tx_exbo" runat="server"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Nombre Edificio :"></asp:Label>
    </td>
<td></td>
<td>
    <asp:TextBox ID="tx_nombreEdificio" runat="server" Width="200px"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Button ID="bt_buscar" runat="server" Text="Buscar" Width="100px" 
        onclick="bt_buscar_Click" />
    </td>
<td></td>
</tr>
<tr>
<td></td>
<td>&nbsp;</td>
<td></td>
<td></td>
<td></td>
<td>
    <asp:Label ID="Label3" runat="server" Text="Estado :"></asp:Label>
    </td>
<td></td>
<td>
    <asp:DropDownList ID="dd_estadoEquipo" runat="server" Width="200px">
    </asp:DropDownList>
    </td>
<td></td>
<td></td>
<td></td>
</tr>

</table>
</div>

<div class = "EquiposEstados">
    <asp:GridView ID="gv_tablaEquipos" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onrowcancelingedit="gv_tablaEquipos_RowCancelingEdit" 
        onrowediting="gv_tablaEquipos_RowEditing" 
        onrowupdating="gv_tablaEquipos_RowUpdating" AutoGenerateColumns="False" 
        onrowdatabound="gv_tablaEquipos_RowDataBound">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="Codigo" HeaderText="Codigo" 
                SortExpression="Codigo" />
            <asp:BoundField DataField="Exbo" HeaderText="Exbo" SortExpression="Exbo" />
            <asp:BoundField DataField="Nombre Edificio" HeaderText="Nombre Edificio" 
                SortExpression="Nombre Edificio" />
            <asp:TemplateField HeaderText="Estado1" SortExpression="Estado1">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Estado1") %>' 
                        Visible="False"></asp:TextBox>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Estado1") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FechaLimitePlanosFabrica" 
                SortExpression="FechaLimitePlanosFabrica">
                <EditItemTemplate>
                    <asp:TextBox ID="tx_fechalimitePlanos_GV" runat="server" 
                        Text='<%# Bind("FechaLimitePlanosFabrica") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="tx_fechalimitePlanos_GV_CalendarExtender" runat="server" 
                        TargetControlID="tx_fechalimitePlanos_GV">
                    </asp:CalendarExtender>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# Bind("FechaLimitePlanosFabrica") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FechaCambioEstado" HeaderText="FechaCambioEstado" 
                SortExpression="FechaCambioEstado" />
            <asp:BoundField DataField="HoraCambioEstado" HeaderText="HoraCambioEstado" 
                SortExpression="HoraCambioEstado" />
            <asp:BoundField DataField="Responsable_Cambio" HeaderText="Responsable_Cambio" 
                SortExpression="Responsable_Cambio" />
            <asp:TemplateField HeaderText="fechaAproxPuerto" 
                SortExpression="fechaAproxPuerto">
                <EditItemTemplate>
                    <asp:TextBox ID="tx_fechaAproxPuerto_GV" runat="server" 
                        Text='<%# Bind("fechaAproxPuerto") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="tx_fechaAproxPuerto_GV_CalendarExtender" 
                        runat="server" TargetControlID="tx_fechaAproxPuerto_GV">
                    </asp:CalendarExtender>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("fechaAproxPuerto") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="vendidoenciudad" HeaderText="vendidoenciudad" 
                SortExpression="vendidoenciudad" />
            <asp:BoundField DataField="instaladoenciudad" HeaderText="instaladoenciudad" 
                SortExpression="instaladoenciudad" />
            <asp:BoundField DataField="fechaaprobacionlimite_planos" 
                HeaderText="fechaaprobacionlimite_planos" 
                SortExpression="fechaaprobacionlimite_planos" />
            <asp:BoundField DataField="fechaaproximadoarribopuerto" 
                HeaderText="fechaaproximadoarribopuerto" 
                SortExpression="fechaaproximadoarribopuerto" />
        </Columns>
        <EditRowStyle BackColor="#99CC00" />
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

<div class="pieEstado">
    <asp:Button ID="tb_excel" runat="server" onclick="tb_excel_Click" 
        Text="Excel" />
    </div>

</div>





</asp:Content>

