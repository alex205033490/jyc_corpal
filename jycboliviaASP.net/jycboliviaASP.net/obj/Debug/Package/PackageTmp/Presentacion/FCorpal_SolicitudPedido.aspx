<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FCorpal_SolicitudPedido.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_SolicitudPedido" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_adicionarRepuesto.css" rel="stylesheet" type="text/css" />
<style type="text/css">
           .CompletionList
        {
            padding: 5px 0 ;
            margin: 2px 0 0;            
          /*  position:absolute;  */
            height:150px;
            width:200px;
            background-color: White;
            cursor: pointer;
            border: solid ;  
            border-width: 1px;    
            font-size:x-small;
            overflow: auto;
                        }
                        
           .CompletionlistItem
           {
               font-size:x-small;           
            }             
                        
        .CompletionListMighlightedItem
        {
             background-color: Green;
             color: White;
            /* color: Lime;
           padding: 3px 20px;
            text-decoration: none;           
            background-repeat: repeat-x;
            outline: 0;*/            
            } 
        
        .style1
        {
            height: 26px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 
<div class="Centrar">
<div class="titulo">
<h3>Solicitud de Productos</h3>
</div>

<div class="vista1">
<table>
<tr>
<td>
    <table>
    <tr>
    <td class="style1">
        <asp:Label ID="Label2" runat="server" Text="Producto :" Font-Size="Small"></asp:Label>
        </td>
    <td class="style1">
        <asp:TextBox ID="tx_producto" runat="server" Width="350px" Font-Size="Small"></asp:TextBox>
        <asp:AutoCompleteExtender ID="tx_producto_AutoCompleteExtender" runat="server" 
            TargetControlID="tx_producto"
            CompletionSetCount="12" 
            MinimumPrefixLength="1" ServiceMethod="GetlistaProductos" 
            UseContextKey="True"
            CompletionListCssClass="CompletionList" 
            CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
            >
        </asp:AutoCompleteExtender>
        </td>    
    <td class="style1">
        <asp:Label ID="Label16" runat="server" Text="Cantidad:" Font-Size="Small"></asp:Label>
        </td>    
    <td class="style1">
        <asp:TextBox ID="tx_cantidadProducto" runat="server" Font-Size="Small"></asp:TextBox>
        </td>
    <td class="style1"></td>
    <td class="style1">
        <asp:Button ID="bt_buscar" runat="server" onclick="bt_buscar_Click" 
            Text="Buscar" />
        </td>
    <td class="style1"></td>    
    </tr>
    <tr>
    <td>
        &nbsp;</td>
    <td>
        &nbsp;</td>
    <td>
        <asp:Label ID="Label22" runat="server" Text="Tipo Solicitud :" 
            Font-Size="Small"></asp:Label>
        </td>
    <td>
        <asp:DropDownList ID="dd_tipoSolicitud" runat="server" Width="100px">
            <asp:ListItem>VENTA</asp:ListItem>
            <asp:ListItem>DEGUSTACION</asp:ListItem>
            <asp:ListItem>MUESTRA</asp:ListItem>
            <asp:ListItem>OTROS</asp:ListItem>
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
                <asp:Button ID="bt_adicionar" runat="server" onclick="bt_adicionar_Click" 
            Text="Adicionar" />
        
        </td>
        <td>&nbsp;</td>
        <td>
            <asp:Button ID="bt_limpiar" runat="server" onclick="bt_limpiar_Click" 
                Text="Limpiar" />
        </td>
        <td></td>
    </tr>

    </table>

</td>
</tr>

<tr>
<td>

<div class="Grepuesto">
    <asp:GridView ID="gv_Productos" runat="server" BackColor="White" 
        CssClass="table table-responsive table-striped" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical">
        <Columns>
                <asp:TemplateField HeaderText="Asignar">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
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

</td>
</tr>


</table>
</div>
<div class = "vista2">
<table>
<tr>
<td>
    <asp:Label ID="Label4" runat="server" Text="Adicion de Producto" 
        Font-Size="Small"></asp:Label>
    </td>
</tr>

<tr>
<td>    
    <table>
        <tr>
            <td>
                <asp:Label ID="Label21" runat="server" Text="Nro:" Font-Size="Small"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_nrodocumento" runat="server"></asp:TextBox>
            </td>
            <td></td>
        
        </tr>

        <tr>
            <td><asp:Label ID="Label10" runat="server" Font-Size="Small" 
                    Text="Solicitante :"></asp:Label></td>
            <td>
            <asp:TextBox ID="tx_solicitante" runat="server" Width="350px" Font-Size="Small"></asp:TextBox>
            </td>
            <td></td>
        </tr>   
    </table>
    <Table>
        <tr>
            <td>
                <asp:Label ID="Label19" runat="server" Text="Fecha Entrega :" Font-Size="Small"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_fechaEntrega" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="tx_fechaEntrega_CalendarExtender" runat="server" 
                    TargetControlID="tx_fechaEntrega">
                </asp:CalendarExtender>
            </td>
            <td>
                <asp:Label ID="Label20" runat="server" Text="Hora Entrega:" Font-Size="Small"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_horaEntrega" runat="server"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="bt_guardar" runat="server" Text="Guardar" 
                    onclick="bt_guardar_Click" />
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </Table>

</td>
</tr>

<tr>
<td>
<div class="Gcotizacion">

    <asp:GridView ID="gv_adicionados" runat="server" BackColor="White" 
        CssClass="table table-responsive table-striped" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        onrowcancelingedit="gv_adicionados_RowCancelingEdit" 
        onrowdeleting="gv_adicionados_RowDeleting" 
        onrowediting="gv_adicionados_RowEditing" 
        onrowupdating="gv_adicionados_RowUpdating">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
      
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

</td>
</tr>



</table>
</div>

</div>



</asp:Content>
