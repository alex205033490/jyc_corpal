<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FCorpal_EntregaSolicitudProducto.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_EntregaSolicitudProducto" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

  <link href="../Styles/Style_cotiRcc.css" rel="stylesheet" type="text/css" />
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
        
        </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div class="Centrar">
<div class="titulo"><h3>Entrega de Solicitud Repuestos</h3></div>
<div class="busqueda">
<table>
<tr>
    <td></td>
    <td>
        <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="NroSolicitud:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tx_nrosolicitud" runat="server"></asp:TextBox>
    </td>
    <td>
        &nbsp;</td>
    <td>
        &nbsp;</td>
    <td>
    <asp:Button ID="bt_buscar" runat="server" Text="Buscar" Width="100px" 
            onclick="bt_buscar_Click" />
    </td>
    <td></td>
    <td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="FechaEntrega :" 
        Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_fechaEngrega" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaEngrega_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaEngrega">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Hora Entrega :"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_horaentrega" runat="server"></asp:TextBox>
    
    </td>
<td>
    &nbsp;</td>
<td></td>
<td>
    &nbsp;</td>

</tr>

</table>

<table>
    <tr>
        <td></td>
        <td>
            <asp:Label ID="Label10" runat="server" Font-Size="Small" 
                Text="Solicitante del Producto:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_SolicitanteProducto" runat="server" 
                Width="500px"></asp:TextBox>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Label ID="Label11" runat="server" Font-Size="Small" 
                Text="Engrego Producto:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_entregoSolicitud" runat="server" Enabled="False" 
                Width="500px"></asp:TextBox>
        </td>
        <td></td>
        <td></td>
    </tr>
</table>
<table>
    <tr>
        <td></td>
        <td><asp:Button ID="bt_limpiar" runat="server" Text="Limpiar" 
                onclick="bt_limpiar_Click" /></td>
        <td><asp:Button ID="bt_actualizar" runat="server" Text="Guardar" Width="100px" 
                onclick="bt_actualizar_Click" /> </td>
        <td><asp:Button ID="bt_eliminar" runat="server" Text="Eliminar" 
                onclick="bt_eliminar_Click" /></td>
        <td></td>
    </tr>
</table>
</div>


<div class="vista1">

    <asp:GridView ID="gv_solicitudesProductos" runat="server" BackColor="White" 
        CssClass="table table-responsive table-striped" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" onselectedindexchanged="gv_solicitudesProductos_SelectedIndexChanged"
       
        >
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>

        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#669900" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    
     <Columns>
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbk_eliminar" runat="server"  />
                    </ItemTemplate>
                </asp:TemplateField>
     </Columns>

    </asp:GridView>

</div>

<div>
<h3>Detalle Producto
</div>

<div class="Grepuesto">

    <asp:GridView ID="gv_detallesolicitud" runat="server" BackColor="White" 
        CssClass="table table-responsive table-striped" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="codigo" HeaderText="codigo" 
                SortExpression="codigo" />
            <asp:BoundField DataField="producto" HeaderText="producto" 
                SortExpression="producto" />
            <asp:BoundField DataField="medida" HeaderText="medida" 
                SortExpression="medida" />
            <asp:BoundField DataField="cantSolicitada" HeaderText="cantSolicitada" 
                SortExpression="cantSolicitada" />
            <asp:BoundField DataField="tiposolicitud" HeaderText="tiposolicitud" 
                SortExpression="tiposolicitud" />
            <asp:TemplateField HeaderText="Cant_Entregada" SortExpression="Cant_Entregada">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Cant_Entregada") %>' 
                        Visible="False"></asp:Label>
                    <asp:TextBox ID="tx_cantentregado" runat="server" 
                        Text='<%# Bind("Cant_Entregada") %>' BackColor="Yellow"></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    
                </EditItemTemplate>
            </asp:TemplateField>
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


<div class = "medio">
<table>
<tr>
<td></td>
<td>
    &nbsp;</td>
<td></td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>

</tr>

</table>
</div>


<div class="blanco">
<table>
<tr>
<td></td>
<td>
    &nbsp;</td>
<td></td>
<td>
    &nbsp;</td>
<td></td>
<td>
    &nbsp;</td>
</tr>
</table>
</div>

</div>


</asp:Content>
