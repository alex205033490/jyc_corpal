<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CotiEra.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CotiEra" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="../Styles/Style_CotiEra.css" rel="stylesheet" type="text/css" />

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
                        
        .style7
    {
        width: 86px;
    }
    .style9
    {
        width: 84px;
    }
                        
        .style17
        {
            width: 68px;
        }
                        
        </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>
       <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

    <div class="Centrar">
<table>
<tr>
<td>
<div class="titulo">
    <h3>
        <asp:Label ID="lb_titulo" runat="server">Repuesto Cotizaciones</asp:Label></h3>
</div>
</td>
</tr>


<tr>
<td>
<div class="datosIntroducir">
<div style="width: 818px">
<table>
<tr>
<td>
    <asp:Label ID="Label20" runat="server" Font-Size="Small" Text="Codigo Coti:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_codigoCoti" runat="server" Font-Size="Small"></asp:TextBox>
    </td>
<td>
    <asp:Label ID="Label21" runat="server" Text="Edificio" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_edificio" runat="server" Width="250px" Font-Size="Small"></asp:TextBox>
    </td>
<td>
    <asp:Label ID="Label22" runat="server" Text="Cite:" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_cite" runat="server" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
</tr>

</table>
 </div>
 <div style="width: 401px; margin-left: 214px">
<table style="width: 405px">
<tr>
    <td class="style17">
        <asp:Button ID="bt_limpiar" runat="server" Text="Limpiar" 
            onclick="bt_limpiar_Click" />
    </td>
    <td class="style9">
        &nbsp;</td>
    <td class="style7">
        <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
            onclick="bt_buscar_Click" />
    </td>

    
  
</tr>
</table>
</div> 
</div>

</td>
</tr>

<tr>
<td>
<div class="vistaCoti">
    <asp:GridView ID="gv_vistaCoti" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
        onselectedindexchanged="gv_vistaCoti_SelectedIndexChanged1">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
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
<div class = "vistaItem">
   <asp:GridView ID="gv_datos" runat="server" Height="97px" 
        style="margin-top: 0px" Width="322px" Font-Size="X-Small" 
        onselectedindexchanged="gv_datos_SelectedIndexChanged" BackColor="#CCCCCC" BorderColor="#999999" 
        BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" 
        ForeColor="Black" AutoGenerateColumns="False" >

        <Columns>
         <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="OnCheckedChanged" />
                    </ItemTemplate>
         </asp:TemplateField>

            <asp:BoundField DataField="Numeracion" HeaderText="Numeracion" 
                SortExpression="Numeracion" />
            <asp:BoundField DataField="CodigoRepuesto" HeaderText="CodigoRepuesto" 
                SortExpression="CodigoRepuesto" />
            <asp:BoundField DataField="Denominacion" HeaderText="Denominacion" SortExpression="Denominacion" />
            <asp:BoundField DataField="cantidad" HeaderText="cantidad" 
                SortExpression="cantidad" />
                   

            <asp:TemplateField HeaderText="fechaentrega_era" 
                SortExpression="fechaentrega_era">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        Text='<%# Bind("fechaentrega_era") %>' Visible="False" Width="100px"></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
                        TargetControlID="TextBox1">
                    </asp:CalendarExtender>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("fechaentrega_era") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="nroserial" SortExpression="nroserial">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("nroserial") %>' 
                        Visible="False" Width="100px"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("nroserial") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="nrofactura" SortExpression="nrofactura">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("nrofactura") %>' 
                        Visible="False" Width="100px"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("nrofactura") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                   

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

</td>
</tr>


<tr>
<td>
<div class = "GCP_otro">
<table>
<tr>
<td>
    <asp:Button ID="bt_Update" runat="server" Text="Actualizar" 
        onclick="bt_Update_Click" />
    </td>
<td></td>
<td></td>
<td></td>
</tr>


<tr>
<td>
<asp:Button ID="bt_Exportar" runat="server" 
        Text="Exportar Excel" onclick="bt_Exportar_Click" />
</td>
<td></td>
<td>
    <asp:Label ID="Label15" runat="server" Text="Cantidad Equipos :"></asp:Label>
    </td>
<td></td>
<td>
    <asp:Label ID="lb_cantidad" runat="server" Text="0"></asp:Label>
    </td>
</tr>
</table>
    
    </div>

</td>
</tr>


</table>

</div>

</asp:Content>
