<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_AdicionarCotizacionRepuesto.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_AdicionarCotizacionRepuesto" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

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
    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

<div class="Centrar">
<div class="titulo">
<h3>Cotizacion de Repuesto</h3>
</div>

<div class="vista1">
<table>
<tr>
<td>
    <table>
    <tr>
    <td class="style1"></td>
    <td class="style1">
        <asp:Label ID="Label1" runat="server" Text="Codigo:"></asp:Label>
        </td>
    <td class="style1">
        <asp:TextBox ID="tx_codigo" runat="server"></asp:TextBox>
        <asp:AutoCompleteExtender ID="tx_codigo_AutoCompleteExtender" runat="server" 
             CompletionSetCount="12" MinimumPrefixLength="1" ServiceMethod="GetlistaCodigosRepuesto" 
            TargetControlID="tx_codigo"
            UseContextKey="True"
            CompletionListCssClass="CompletionList" 
            CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
            >
        </asp:AutoCompleteExtender>
        </td>
    <td class="style1"></td>
    <td class="style1">
        <asp:Label ID="Label2" runat="server" Text="Repuesto :"></asp:Label>
        </td>
    <td class="style1">
        <asp:TextBox ID="tx_nameRepuesto" runat="server"></asp:TextBox>
        </td>
    <td class="style1"></td>
    <td class="style1">
        <asp:Button ID="bt_buscar" runat="server" onclick="bt_buscar_Click" 
            Text="Buscar" />
        </td>
    <td class="style1"></td>    
    </tr>
    <tr>
    <td></td>
    <td></td>
    <td>
        <asp:Button ID="bt_adicionar" runat="server" onclick="bt_adicionar_Click" 
            Text="Adicionar" />
        </td>
    <td></td>
    </tr>

    </table>

</td>
</tr>

<tr>
<td>

<div class="Grepuesto">
    <asp:GridView ID="gv_repuesto" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="Small" ForeColor="Black" GridLines="Vertical">
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

<tr>
<td>
    <asp:Label ID="Label3" runat="server" Font-Size="Small" 
        Text="Cantidad Repuesto :"></asp:Label>
    <asp:TextBox ID="tx_cantRepuesto" runat="server"></asp:TextBox>
    </td>
</tr>
</table>
</div>
<div class = "vista2">
<table>
<tr>
<td>
    <asp:Label ID="Label4" runat="server" Text="Adicion de Repuesto"></asp:Label>
    </td>
</tr>

<tr>
<td>
    <table>

    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="Codigo:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_codigoCotizacion" runat="server" Font-Size="Small" 
            Enabled="False"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label9" runat="server" Font-Size="Small" Text="Exbo:"></asp:Label>
        </td>
    <td>
        <asp:DropDownList ID="dd_exbos" runat="server" Width="150px">
        </asp:DropDownList>
        </td>
    <td></td>
    <td>
        <asp:Button ID="bt_verificar" runat="server" 
            Text="Verificar Equipos" onclick="bt_verificar_Click" />
        </td>
    <td>
        &nbsp;</td>
    <td></td>
    <td></td>
    </tr>

    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label6" runat="server" Text="Edificio:" Font-Size="Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_edificio" runat="server"></asp:TextBox>
        <asp:AutoCompleteExtender ID="tx_edificio_AutoCompleteExtender" runat="server" 
            TargetControlID="tx_edificio"
            CompletionSetCount="12" 
            MinimumPrefixLength="1" ServiceMethod="GetlistaProyectos" 
            UseContextKey="True"
            CompletionListCssClass="CompletionList" 
            CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
            >
        </asp:AutoCompleteExtender>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label7" runat="server" Text="Cite :" Font-Size="Small"></asp:Label>
        </td>
    <td>
    <asp:TextBox ID="tx_numeroCoti" runat="server"></asp:TextBox>
    </td>
    <td></td>
    <td>
        <asp:Button ID="Button1" runat="server" Text="Crear R-144" 
            onclick="Button1_Click" Width="150px" />
        </td>
    <td></td>
    <td></td>
    <td></td>
    </tr>
    </table>
</td>
</tr>

<tr>
<td>
    <table>
    <tr>
    <td class="style1"></td>
    <td class="style1">
        <asp:CheckBox ID="cb_el" runat="server" Text="Ascensor"  AutoPostBack = "true"
            oncheckedchanged="cb_el_CheckedChanged" />
        </td>
    <td class="style1"></td>
    <td class="style1">
        <asp:CheckBox ID="cb_ellos" runat="server" Text="Ascensores" AutoPostBack = "true"
            oncheckedchanged="cb_ellos_CheckedChanged" />
        </td>
    <td class="style1"></td>
    </tr>
    </table>
</td>
</tr>

<tr>
<td>
<div class="Gcotizacion">

    <asp:GridView ID="gv_adicionados" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
        onrowcancelingedit="gv_adicionados_RowCancelingEdit" 
        onrowdeleting="gv_adicionados_RowDeleting" 
        onrowediting="gv_adicionados_RowEditing" 
        onrowupdating="gv_adicionados_RowUpdating" 
        onselectedindexchanged="gv_adicionados_SelectedIndexChanged">
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

<tr>
<td>
    <asp:Label ID="Label5" runat="server" Text="Total :"></asp:Label>
    <asp:TextBox ID="tx_precioTotal" runat="server"></asp:TextBox>
    </td>
</tr>

</table>
</div>

</div>

</asp:Content>
