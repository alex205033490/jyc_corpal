<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FCorpal_GestionarTiendaPropietario.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_GestionarProyEncargadoPago" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_GestionarProyEncargadoPago.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
   
    <div class="Centrar">
    <div class="titulo"><h3>GESTIONAR TIENDA</h3></div>
    <div class="DatosProyecto">
    <h3> Datos de la Tienda :</h3>
    <table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label1" runat="server" Text="Nombre Tienda:" 
            Font-Size="Small"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label2" runat="server" Text="Direccion :" Font-Size="Small"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label3" runat="server" Text="Departamento :" Font-Size="Small"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label4" runat="server" Font-Size="Small" Text="Zona :"></asp:Label>
        </td>
    <td></td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td>
        <asp:TextBox ID="tx_nombreTienda" runat="server" Width="200px" 
            Font-Size="Small"></asp:TextBox>
            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
        TargetControlID = "tx_nombreTienda" CompletionSetCount = "12" 
        MinimumPrefixLength = "1" ServiceMethod = "getListaTienda"
        UseContextKey = "true" >
        </asp:AutoCompleteExtender> 
        </td>
    <td></td>
    <td>
        <asp:TextBox ID="tx_direcciontienda" runat="server" Width="150px" 
            Font-Size="Small"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:DropDownList ID="dd_departamentoTienda" runat="server" Width="150px">
            <asp:ListItem Value="Santa Cruz"></asp:ListItem>
            <asp:ListItem Value="Cochabamba"></asp:ListItem>
            <asp:ListItem Value="La Paz"></asp:ListItem>
            <asp:ListItem Value="Chuquisaca"></asp:ListItem>
            <asp:ListItem Value="Tarija"></asp:ListItem>
            <asp:ListItem Value="Pando"></asp:ListItem>
            <asp:ListItem Value="Oruro"></asp:ListItem>
            <asp:ListItem Value="Potosi"></asp:ListItem>
            <asp:ListItem Value="Beni"></asp:ListItem>
            <asp:ListItem>Asuncion-Paraguay</asp:ListItem>
        </asp:DropDownList>
        </td>
    <td></td>
    <td>
        <asp:TextBox ID="tx_zonaTienda" runat="server" Font-Size="Small"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
            onclick="bt_buscar_Click" />
        </td>
    </tr>
     <tr>
    <td></td>
    <td>
        <asp:Label ID="Label16" runat="server" Font-Size="Small" 
            Text="Telefono :"></asp:Label>
         </td>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
    </tr>

    <tr>
    <td></td>
    <td>
        <asp:TextBox ID="tx_telefonoTienda" runat="server" Font-Size="Small" 
            Width="200px"></asp:TextBox>
        </td>
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
   
   <h3>Datos Propietario Tienda :</h3>
   <table>
   
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label5" runat="server" Font-Size="Small" Text="Nombre :"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Carnet :"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label12" runat="server" Font-Size="Small" Text="Nit :"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="Celular :"></asp:Label>
        </td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td>
        <asp:TextBox ID="tx_nombrePropietario" runat="server" Width="200px" 
            Font-Size="Small"></asp:TextBox>
        </td>
    <td>
        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
        TargetControlID = "tx_nombrePropietario" CompletionSetCount = "12" 
        MinimumPrefixLength = "1" ServiceMethod = "getListaPropietario"
        UseContextKey = "true" >
        </asp:AutoCompleteExtender> 
    </td>
    <td>
        <asp:TextBox ID="tx_ciPropietario" runat="server" Width="150px" 
            Font-Size="Small"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:TextBox ID="tx_nitPropietario" runat="server" Width="150px" 
            Font-Size="Small"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:TextBox ID="tx_celularPropietario" runat="server" Width="150px" 
            Font-Size="Small"></asp:TextBox>
        </td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label9" runat="server" Font-Size="Small" Text="Direccion :"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label10" runat="server" Font-Size="Small" Text="Mail :"></asp:Label>
        </td>
    <td></td>
    <td>
        &nbsp;</td>
    <td></td>
    <td>
        &nbsp;</td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td>
        <asp:TextBox ID="tx_direccionPropietario" runat="server" Width="200px" 
            Font-Size="Small"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:TextBox ID="tx_correoPropietario" runat="server" Width="150px" 
            Font-Size="Small"></asp:TextBox>
        </td>
    <td></td>
    <td>
        &nbsp;</td>
    <td></td>
    <td>
        &nbsp;</td>
    <td></td>
    </tr>
 
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label11" runat="server" Font-Size="Small" Text="Facturar A :"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label17" runat="server" Font-Size="Small" Text="Facturar Nit:"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label18" runat="server" Font-Size="Small" 
            Text="Facturar Correo:"></asp:Label>
        </td>
    <td></td>
    <td>&nbsp;</td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td>
        <asp:TextBox ID="tx_FacturarA" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:TextBox ID="tx_facturarNit" runat="server" Font-Size="Small" Width="150px"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:TextBox ID="tx_facturarCorreo" runat="server" Font-Size="Small" 
            Width="150px"></asp:TextBox>
        </td>
    <td></td>
    <td>
        &nbsp;</td>
    <td></td>
    </tr>
</table>
<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label14" runat="server" Font-Size="Small" Text="Observaciones :"></asp:Label>
    </td>
</tr>
<tr>
<td></td>
<td>
    <asp:TextBox ID="tx_observacionTienda" runat="server" Height="50px" Width="600px" 
        TextMode="MultiLine" ></asp:TextBox>
    </td>
</tr>
</table>

<table>
   <tr>
    <td></td>
    <td>
        <asp:Button ID="bt_limpiar" runat="server" Text="Limpiar" Width="100px" 
            onclick="bt_limpiar_Click" />
       </td>
    <td></td>
    <td>
        <asp:Button ID="bt_guardar" runat="server" Text="Guardar" Width="100px" 
            onclick="bt_guardar_Click" Height="26px" />
       </td>
    <td></td>
    <td>
        <asp:Button ID="bt_modificar" runat="server" Text="Modificar" Width="100px" 
            onclick="bt_modificar_Click" />
       </td>
    <td></td>
    <td></td>
    <td>
        <asp:Button ID="bt_eliminar" runat="server" Text="Eliminar" Width="100px" 
            onclick="bt_eliminar_Click" />
       </td>
    </tr>

   </table>
    
    </div>

    <div class = "tablaProyecto">
        <asp:GridView ID="gv_tablaTienda" runat="server" BackColor="#CCCCCC" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
            CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
            onselectedindexchanged="gv_tablaProyecto_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#99CC00" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </div>


    <div class="pie">
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">excel</asp:LinkButton>
    </div>


    </div>

</asp:Content>
