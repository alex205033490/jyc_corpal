<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_AgendaTelefonica.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_AgendaTelefonica" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_AgendaTelefonica.css" rel="stylesheet" type="text/css" />
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
        
        .style3
        {
            width: 11px;
        }
        .style5
        {
            width: 50px;
        }
        .style6
        {
            width: 237px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

<div class="Centrar">
<div class="titulo">
<h3>Agenda Telefonica</h3>
</div>

<table>
<tr>
<td>

<table>

<tr>
<td></td>
<td>
    <asp:Label ID="Label15" runat="server" Font-Size="Small" Text="Codigo :"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_codigo" runat="server" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label1" runat="server" Text="Nombre :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_nombreCliente" runat="server" Width="200px" 
        Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Direccion :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_direccion" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label3" runat="server" Text="Telefono :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_telefono" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Celular 1 :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_celular1" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label5" runat="server" Text="Celular 2 :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_celular2" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label6" runat="server" Text="Celular 3 :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_celular3" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label7" runat="server" Text="Celular 4 :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_celular4" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label8" runat="server" Text="Email 1 :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_email1" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label9" runat="server" Text="Email 2 :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_email2" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label10" runat="server" Text="Email 3 :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_mail3" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label11" runat="server" Text="Email 4 :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_email4" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label12" runat="server" Text="Fax :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_fax" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label13" runat="server" Text="Nota :" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_nota" runat="server" Height="141px" TextMode="MultiLine" 
        Width="200px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Button ID="bt_limpiar" runat="server" onclick="bt_limpiar_Click" 
        Text="Limpiar" />
    </td>
<td></td>
<td></td>
</tr>
</table>


<table>
<tr>
<td></td>
<td>
    <asp:Button ID="bt_adicionar" runat="server" Text="Adicionar" 
        onclick="bt_adicionar_Click" />
    </td>
<td>
    &nbsp;</td>
<td>
    <asp:Button ID="bt_modificar" runat="server" Text="Modificar" 
        onclick="bt_modificar_Click" />
    </td>
<td>
    &nbsp;</td>
<td>
    <asp:Button ID="bt_eliminar" runat="server" Text="Eliminar" 
        onclick="bt_eliminar_Click" />
    </td>
<td></td>
</tr>
</table>

<div class="blanco"></div>

</td>

<td></td>


<td>

<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label14" runat="server" Text="Nombre :"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_nombrebusqueda" runat="server" Font-Size="Small" 
        Width="250px"></asp:TextBox>
    <asp:AutoCompleteExtender ID="tx_nombrebusqueda_AutoCompleteExtender" 
        runat="server" TargetControlID="tx_nombrebusqueda"
        CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaProyectosAgenda" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
        >
    </asp:AutoCompleteExtender>
    </td>
<td></td>
<td>
    <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
        onclick="bt_buscar_Click" />
    </td>
<td></td>
</tr>
</table>
<div class="tablaDatos">
    <asp:GridView ID="gv_datos" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        onselectedindexchanged="gv_datos_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#33CC33" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>

</div>
<div class="blanco">
    <asp:LinkButton ID="lb_botondescargar" runat="server" 
        onclick="LinkButton1_Click">Excel</asp:LinkButton>
    </div>

</td>
</tr>
</table>




</div>


</asp:Content>
