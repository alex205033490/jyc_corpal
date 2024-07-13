<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_movimientoCheques.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_movimientoCheques" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_movimientoCheques.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 

    <div class="Centrar">
    <div class="titulo"><h3>
        <asp:Label ID="lb_titulo" runat="server" Text="Movimientos de Cheques"></asp:Label></h3></div>
    
    <table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label17" runat="server" Text="Base de Datos:"></asp:Label>
        </td>
    <td>
        <asp:DropDownList ID="dd_baseDatos" runat="server" Width="200px" 
            AutoPostBack="True" onselectedindexchanged="dd_baseDatos_SelectedIndexChanged">
        <asp:ListItem>Ninguno</asp:ListItem>
                                              <asp:ListItem>Santa Cruz</asp:ListItem>
                                              <asp:ListItem>Cochabamba</asp:ListItem>
                                              <asp:ListItem>La Paz</asp:ListItem>
                                              <asp:ListItem>Sucre</asp:ListItem>
                                              <asp:ListItem>Oruro</asp:ListItem>
                                              <asp:ListItem>Potosi</asp:ListItem>
                                              <asp:ListItem>Tarija</asp:ListItem>
                                              <asp:ListItem>Yacuiba</asp:ListItem>
                                              <asp:ListItem>Villamontes</asp:ListItem>
                                              <asp:ListItem>Asuncion-Paraguay</asp:ListItem>
                                              <asp:ListItem>JyC Srl</asp:ListItem>
            <asp:ListItem>JyCIA Srl</asp:ListItem>
            <asp:ListItem>Imven</asp:ListItem>
                                              <asp:ListItem>Prueba</asp:ListItem>
        </asp:DropDownList>
        </td>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
    
    </tr>

    <tr>
    <td></td><td>
        <asp:Label ID="Label9" runat="server" Text="Banco :"></asp:Label>
        </td><td>
            <asp:DropDownList ID="dd_banco" runat="server" Width="200px" 
                AutoPostBack="true" onselectedindexchanged="dd_banco_SelectedIndexChanged">
            </asp:DropDownList>
        </td><td></td><td>Cuenta:</td><td>
        <asp:DropDownList ID="dd_CuentaBancaria" runat="server" Width="200px" 
            onselectedindexchanged="dd_CuentaBancaria_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
        </td>
        <td></td>
    </tr>
    
   

    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label13" runat="server" Text="Cuenta:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_Dolares" runat="server" Width="150px"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label14" runat="server" Text="Tipo :"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_tipoCuenta" runat="server" Width="150px"></asp:TextBox>
        </td>
    <td></td>
    </tr>

     <tr>
    <td></td>
    <td>
        <asp:Label ID="Label15" runat="server" Text="Fecha:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_fecha" runat="server"></asp:TextBox>
        <asp:CalendarExtender ID="tx_fecha_CalendarExtender" runat="server" 
            TargetControlID="tx_fecha">
        </asp:CalendarExtender>
        </td>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
    </tr>
    
    </table>
    
    <table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label16" runat="server" Text="Titular Cheque:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_titular" runat="server" Width="400px"></asp:TextBox>
        </td>
    <td></td>
    </tr>
    </table>

    
    <table>
    <tr>
    <td></td><td>
        <asp:Label ID="Label10" runat="server" Text="Nro Cheque:"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_nroCheque" runat="server"></asp:TextBox>
        </td><td></td><td>
        <asp:Label ID="Label11" runat="server" Text="Monto:"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_monto" runat="server">0</asp:TextBox>
        </td><td></td><td>
        <asp:Button ID="bt_buscar" runat="server" onclick="bt_buscar_Click" 
            Text="Buscar" />
        </td>
    </tr>
    <tr>
    <td></td><td></td><td>
        <asp:CheckBox ID="cb_tranferencia" runat="server" Text="Transferencia" />
        </td><td></td><td>
        <asp:Label ID="Label12" runat="server" Text="Destino:"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_destino" runat="server"></asp:TextBox>
        </td><td></td><td>
        &nbsp;</td>
    </tr>
    </table>

    <table>
    <tr>
    <td></td><td>Detalle :</td>
    <td>&nbsp;</td>
    </tr>
    <tr>
    <td></td>
    <td>    
        <asp:TextBox ID="tx_detalle" runat="server" Height="86px" TextMode="MultiLine" 
            Width="493px"></asp:TextBox>
    </td>
    <td></td>
    </tr>
    </table>

    <table>
    <tr>
    <td></td>
    <td>
        <asp:Button ID="bt_limpiar" runat="server" onclick="bt_limpiar_Click" 
            Text="Limpiar" />
        </td>
    <td></td>
    <td>
        <asp:Button ID="bt_guardar" runat="server" Text="Guardar" 
            onclick="bt_guardar_Click" />
        </td>
    <td></td>
    <td>
        <asp:Button ID="bt_modificar" runat="server" onclick="bt_modificar_Click" 
            Text="Modificar" />
        </td>
    <td></td>
    <td>
        <asp:Button ID="bt_eliminar" runat="server" onclick="bt_eliminar_Click" 
            Text="Eliminar" />
        </td>
    <td></td>
    <td></td>
    </tr>
    </table>

    <table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label18" runat="server" Font-Size="Small" Text="Banco :"></asp:Label>
        </td>
    <td>
        <asp:DropDownList ID="dd_bancoCheque" runat="server" Width="150px">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>BNB</asp:ListItem>
            <asp:ListItem>Bisa</asp:ListItem>
        </asp:DropDownList>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label19" runat="server" Font-Size="Small" Text="Moneda:"></asp:Label>
        </td>
    <td>
        <asp:DropDownList ID="dd_monedaCheque" runat="server" Width="120px">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Dólares Americanos</asp:ListItem>
            <asp:ListItem>Bolivianos</asp:ListItem>
        </asp:DropDownList>
        </td>
    <td></td>
    
    <td>
        <asp:Button ID="bt_impresionCheque" runat="server" 
            onclick="bt_impresionCheque_Click" Text="Impresion Cheque" />
        </td>  
    </tr>
    </table>

    <div class="MovimientosCheques">
    
        <asp:GridView ID="gv_movimientosCheques" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
            onselectedindexchanged="gv_movimientosCheques_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#99CC00" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    
    </div>


    <div class="blanco">
        <asp:LinkButton ID="lkb_excel" runat="server" onclick="lkb_excel_Click">Excel</asp:LinkButton>
        </div>

    </div>


</asp:Content>
