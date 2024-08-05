<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_VaciadoAlSimec.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_VaciadoAlSimec" %>
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
            width: 20px;
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
  <h3>  <asp:Label ID="lbTitulo" runat="server" 
          Text="Vaciar Al Simec los Pagos de JyC"></asp:Label> </h3>
</div>

<table>
<tr>
<td>
    <div class="sc1">
        <table>
            <tr>
            <td class="style1"></td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="TC:" Font-Size="Small"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="tx_tipoCambio" runat="server" Font-Size="Small" Height="25px" 
                    Width="70px">6,96</asp:TextBox>
                </td>
            <td></td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Glosa:" Font-Size="Small"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="tx_glosa" runat="server" Font-Size="Small" Height="25px" 
                    Width="350px"></asp:TextBox>
                <asp:AutoCompleteExtender ID="tx_glosa_AutoCompleteExtender" runat="server" 
                    TargetControlID="tx_glosa"
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
                <asp:Label ID="Label7" runat="server" Font-Size="Small" Text="VCAJA:"></asp:Label>
                </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="tx_vcaja" runat="server" Font-Size="Small" Width="80px">0P</asp:TextBox>
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
                &nbsp;</td>
            <td></td>
            <td>
                &nbsp;</td>
            <td></td>
            </tr>

            <tr>
            <td class="style1"></td>
            <td></td>
            <td>
                <asp:Button ID="bt_anularPago" runat="server" onclick="bt_anularPago_Click" 
                    Text="Anular" />
                </td>
            <td></td>
            <td></td>
            <td>
                <asp:Button ID="bt_vaciarAlSimec" runat="server" 
                    onclick="bt_vaciarAlSimec_Click" Text="Vaciar al Simec" />
                </td>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Excel" />
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
    <asp:GridView ID="gv_datosCobros" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="Small" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
        <Columns>
        <asp:TemplateField HeaderText="Anular">
            <ItemTemplate>
                   <asp:CheckBox ID="chkAll" runat="server"  />
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
</td>
</tr>

<tr>
<td>
    <asp:Label ID="Label6" runat="server" Text="Cantidad Equipos :"></asp:Label>
    <asp:Label ID="lb_cantDatos" runat="server" Text="0"></asp:Label>
    </td>
</tr>
<tr>
<td></td>
</tr>

</table>

</div>


</asp:Content>
