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
<div class="titulo"><h3>Entrega de Solicitud Repuestos</h3>
    <p>&nbsp;</p></div>
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
    <asp:Button ID="bt_actualizar" runat="server" Text="Actualizar" Width="100px" />
    </td>
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
</div>


<div class="vista1">

    <asp:GridView ID="gv_solicitudesProductos" runat="server" BackColor="White" 
        CssClass="table table-responsive table-striped" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical"
       
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
                        <asp:CheckBox ID="CheckBox1" runat="server"  />
                    </ItemTemplate>
                </asp:TemplateField>
     </Columns>

    </asp:GridView>

</div>

<div>
<h3>Detalle Producto</h3>
</div>

<div class="Grepuesto">

    <asp:GridView ID="gv_detallesolicitud" runat="server" BackColor="White" 
        CssClass="table table-responsive table-striped" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical">
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
