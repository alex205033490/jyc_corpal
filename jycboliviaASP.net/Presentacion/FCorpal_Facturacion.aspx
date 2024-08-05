<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_Facturacion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_Facturacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_facturacion.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="Centrar">
    <div class="titulo"><h3>
        <asp:Label ID="lb_titulo" runat="server" Text="Movimientos de Facturas"></asp:Label></h3></div>
    
    <table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label15" runat="server" Text="Fecha Factura:" 
            Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_fechaFacturacion" runat="server" Font-Size="X-Small" 
            Height="20px" Width="150px"></asp:TextBox>
        <asp:CalendarExtender ID="tx_fechaFacturacion_CalendarExtender" runat="server" 
            TargetControlID="tx_fechaFacturacion">
        </asp:CalendarExtender>
        </td>
    <td></td>
    <td></td>
    <td>
        <asp:Button ID="bt_buscar" runat="server" onclick="bt_buscar_Click" 
            Text="Buscar" />
        </td>
    <td></td>
    
    </tr>

    <tr>
    <td></td><td>
        <asp:Label ID="lb_nrofactura" runat="server" Text="Nro. Factura:" 
            Font-Size="X-Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_nroFactura" runat="server" Font-Size="X-Small" 
                Height="20px" Width="150px"></asp:TextBox>
        </td><td></td><td>
        <asp:Label ID="Label20" runat="server" Text="Nro. Autorizacion:" 
            Font-Size="X-Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_nroAutorizacion" runat="server" Font-Size="X-Small" 
                Height="20px" Width="150px"></asp:TextBox>
        </td>
        <td></td>
    </tr>
    
   

    
    
    </table>
    
    <table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label16" runat="server" Text="Señor(es):" Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_nombreFactura" runat="server" Width="400px" 
            Font-Size="X-Small" Height="20px"></asp:TextBox>
        </td>
    <td></td>
    </tr>
    </table>

    
    <table>
    <tr>
    <td></td><td>
        <asp:Label ID="Label10" runat="server" Text="Nit/Ci:" Font-Size="X-Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_nit_ci" runat="server" Width="150px" Font-Size="X-Small" 
                Height="20px"></asp:TextBox>
        </td><td></td><td>
        <asp:Label ID="Label11" runat="server" Text="Monto Total:" Font-Size="X-Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_montoTotal" runat="server" Font-Size="X-Small" 
                Height="20px" Width="150px">0</asp:TextBox>
        </td><td></td><td>
        &nbsp;</td>
    </tr>
    <tr>
    <td></td><td>
        <asp:Label ID="Label21" runat="server" Text="Codigo Control:" 
            Font-Size="X-Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_codigoControl" runat="server" Width="150px" 
                Font-Size="X-Small" Height="20px"></asp:TextBox>
        </td><td></td><td>
        <asp:Label ID="Label22" runat="server" Text="Fecha Limite:" Font-Size="X-Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_fechalimite" runat="server" Font-Size="X-Small" 
                Height="20px" Width="150px"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechalimite_CalendarExtender" runat="server" 
                TargetControlID="tx_fechalimite">
            </asp:CalendarExtender>
        </td><td></td><td>
        &nbsp;</td>
    </tr>
    </table>

    <table>
    <tr>
    <td></td><td>
        <asp:Label ID="Label24" runat="server" Font-Size="X-Small" Text="Detalle:"></asp:Label>
        </td>
    <td>&nbsp;</td>
    </tr>
    <tr>
    <td></td>
    <td>    
        <asp:TextBox ID="tx_detalle" runat="server" Height="86px" TextMode="MultiLine" 
            Width="493px" Font-Size="X-Small"></asp:TextBox>
    </td>
    <td></td>
    </tr>
    </table>


    <table>
    <tr>
    <td></td>
    
    <td>
        <asp:FileUpload ID="FileUpload1" runat="server" Width="500px" />
        </td>
    <td></td>
    <td>
        <asp:Button ID="bt_adicionar" runat="server" Text="Adicionar" 
            onclick="bt_adicionar_Click" />
        </td>
    <td></td>
    </tr>
    </table>


    <table>
    <tr>
    <td></td>
    <td>
        <asp:Button ID="bt_limpiar" runat="server" 
            Text="Limpiar" onclick="bt_limpiar_Click" />
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
   
    </table>

    <div class="MovimientosCheques">
    
        <asp:GridView ID="gv_movimientosFacturas" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
            onselectedindexchanged="gv_movimientosFacturas_SelectedIndexChanged">
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

    <div class="AdjuntosFacturas">
        <asp:GridView ID="gv_adjuntofactura" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
            onselectedindexchanged="gv_adjuntofactura_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:CommandField HeaderText="descarga" SelectText="descargar" 
                    ShowSelectButton="True" />
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
        <asp:LinkButton ID="lkb_excel" runat="server">Excel</asp:LinkButton>
        </div>

    </div>


</asp:Content>
