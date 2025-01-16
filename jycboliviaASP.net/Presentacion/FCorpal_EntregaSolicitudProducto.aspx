<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_EntregaSolicitudProducto.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_EntregaSolicitudProducto" %>
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

<div class="card" >
  <div class="card-header bg-warning text-black">
    Entrega de Solicitud Productos
  </div>
  <ul class="list-group list-group-flush">
    <li class="list-group-item">
        <div class="col-12">
            <div class="busqueda">
<table>
<tr>
    <td></td>
    <td>
        <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="NroSolicitud:"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="tx_nrosolicitud" class="form-control" runat="server" Font-Size="Small"   Width="100px"></asp:TextBox>
    </td>
    <td>
        &nbsp;</td>
    <td>
        &nbsp;</td>
    <td>
    <asp:Button ID="bt_buscar" runat="server" class="btn btn-info" Text="Buscar" Width="100px" 
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
    <asp:TextBox ID="tx_fechaEngrega" class="form-control" runat="server" Font-Size="Small"      Width="100px"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaEngrega_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaEngrega">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Hora Entrega :"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_horaentrega" class="form-control" Font-Size="Small" runat="server" Width="100px"></asp:TextBox>    
    </td>
<td>
    <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>  </td>
<td>
    <asp:DropDownList ID="dd_estadoCierre" class="btn btn-secondary dropdown-toggle" runat="server"  >
        <asp:ListItem>Abierto</asp:ListItem>
        <asp:ListItem>Cerrado</asp:ListItem>
    </asp:DropDownList>

</td>
<td>
    &nbsp;</td>

</tr>

</table>

<table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label12" runat="server" Text="Motivo Cierre:"></asp:Label>
        </td>
    <td>
        <asp:DropDownList ID="dd_motivoCierre" class="btn btn-secondary dropdown-toggle" runat="server" >
            <asp:ListItem>Ninguno</asp:ListItem>
            <asp:ListItem>Vendedor Sin Espacio</asp:ListItem>
            <asp:ListItem>Vendedor Redujo Solicitud</asp:ListItem>
            <asp:ListItem>Sin Stock en Almacen</asp:ListItem>
        </asp:DropDownList>
        </td>

    </tr>

    <tr>
        <td></td>
        <td>
            <asp:Label ID="Label10" runat="server" Font-Size="Small" 
                Text="Solicitante del Producto:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_SolicitanteProducto" class="form-control" runat="server" 
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
            <asp:TextBox ID="tx_entregoSolicitud" class="form-control" runat="server" Enabled="False" 
                Width="500px"></asp:TextBox>
        </td>
        <td></td>
        <td></td>
    </tr>
</table>
<table>
    <tr>
        <td></td>
        <td><asp:Button ID="bt_limpiar" runat="server" class="btn btn-primary" Text="Limpiar" 
                onclick="bt_limpiar_Click" /></td>
        <td><asp:Button ID="bt_actualizar" runat="server" class="btn btn-success" Text="Guardar" Width="100px" 
                onclick="bt_actualizar_Click" /> </td>
        <td>
            <asp:Button ID="bt_verRecibo" runat="server" class="btn btn-warning" 
                Text="Ver Recibo" onclick="bt_verRecibo_Click" />
        </td>
        <td><asp:Button ID="bt_eliminar" runat="server" class="btn btn-danger" Text="Eliminar" 
                onclick="bt_eliminar_Click" /></td>
        
    </tr>
</table>
</div>
        </div>
    </li>
    <li class="list-group-item">
        <div class="col-12">
            <div class="vista1">

    <asp:GridView ID="gv_solicitudesProductos" runat="server" BackColor="White"         
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
        </div>
    </li>
    <li class="list-group-item">
        <div class="col-12">
            <h3>Detalle Producto</h3>
            <div class="Grepuesto">

    <asp:GridView ID="gv_detallesolicitud" runat="server" BackColor="White"        
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        AutoGenerateColumns="False" 
        onselectedindexchanged="gv_detallesolicitud_SelectedIndexChanged">
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
                    <asp:Label ID="lb_cantentregado" runat="server" Text='<%# Bind("Cant_Entregada") %>' 
                        ></asp:Label>
                    <asp:TextBox ID="tx_cantentregado" runat="server" 
                        Text='<%# Bind("Cant_Entregada") %>' BackColor="Yellow" Visible="False" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="cant_Entregar">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                    <asp:TextBox ID="tx_cantidadEntregarOK" runat="server" BackColor="Yellow"></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="stock_Almacen" HeaderText="stock_Almacen" 
                SortExpression="stock_Almacen" />
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
        </div>
    </li>
  </ul>
</div>




</asp:Content>
