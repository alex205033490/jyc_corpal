<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_SeguimientosMorosos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_SeguimientosMorosos" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_SeguimientosMorosos.css" rel="stylesheet" type="text/css" />
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
            width: 115px;
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
  <h3>  <asp:Label ID="lbTitulo" runat="server" Text="Mantenimientos Morosos"></asp:Label> </h3>
</div>

<table>
<tr>
<td>
    <div class="sc1">
        <table>
            <tr>
            <td class="style1"></td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Exbo:" Font-Size="Small"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="tx_exbo" runat="server" Font-Size="Small" Height="25px" 
                    Width="160px"></asp:TextBox>
                </td>
            <td></td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Edificio:" Font-Size="Small"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="tx_edificio" runat="server" Font-Size="Small" Height="25px" 
                    Width="160px"></asp:TextBox>
                <asp:AutoCompleteExtender ID="tx_edificio_AutoCompleteExtender" runat="server" 
                    TargetControlID="tx_edificio"
                    CompletionSetCount="12" 
                    MinimumPrefixLength="1" ServiceMethod="GetlistaProyectos3" 
                    UseContextKey="True"
                    CompletionListCssClass="CompletionList" 
                    CompletionListItemCssClass="CompletionlistItem" 
                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                    >
                </asp:AutoCompleteExtender>
                </td>
            <td></td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Año:" Font-Size="Small"></asp:Label>
                </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:DropDownList ID="dd_anio" runat="server" Font-Size="Small" 
                    Height="25px" Width="160px">
                </asp:DropDownList>
                </td>
            <td>
                &nbsp;</td>
            
            </tr>

            <tr>
            <td class="style1"></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <asp:Label ID="lb_BaseDatos" runat="server" Text="Dpto:" 
                    Font-Size="Small"></asp:Label>
                </td>
            <td></td>
            <td>
                <asp:DropDownList ID="dd_BaseDatos" runat="server" Height="25px" Width="160px" 
                    AutoPostBack="True" onselectedindexchanged="dd_BaseDatos_SelectedIndexChanged" >
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
                      <asp:ListItem>Prueba</asp:ListItem>
                </asp:DropDownList>
                </td>
            <td></td>
            </tr>

            <tr>
            <td class="style1"></td>
            <td></td>
            <td>
                <asp:Button ID="bt_buscar" runat="server" Text="Buscar" Width="90px" 
                    onclick="bt_buscar_Click" Height="25px" />
                </td>
            <td></td>
            <td></td>
            <td>
                <asp:Button ID="bt_Excel" runat="server" Text="Excel" Width="90px" 
                    onclick="bt_Excel_Click" Height="25px" />
                </td>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <asp:Label ID="lb_deudas" runat="server" BackColor="Lime" Font-Size="Small" 
                    ForeColor="Black"></asp:Label>
                </td>
            <td></td>
            </tr>
        </table>
    </div>
</td>
</tr>

<tr>
<td>
<div class="sc2">
    <asp:GridView ID="gv_seguimientoMoroso" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="Small" ForeColor="White">
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="#FF3300" />
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
    <asp:Label ID="Label6" runat="server" Text="Cantidad Equipos :"></asp:Label>
    <asp:TextBox ID="tx_cantidadEquipos" runat="server" Enabled="False"></asp:TextBox>
    </td>
</tr>
<tr>
<td></td>
</tr>

</table>

</div>

</asp:Content>
