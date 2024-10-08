<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CotiRCC.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CotiRCC" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_cotiRcc.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

<div class="Centrar">
<div class="titulo"><h3>Cotizaciones de Repuestos R-144</h3></div>
<div class="busqueda">
<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Codigo Cotizacion:" 
        Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_codigo" runat="server" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="Edificio:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_edificio" runat="server"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
        onclick="bt_buscar_Click" />
    </td>
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
    <asp:Label ID="Label4" runat="server" Text="Envio Proforma:" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_envioProforma" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_envioProforma_CalendarExtender" runat="server" 
        TargetControlID="tx_envioProforma">
    </asp:CalendarExtender>
    </td>
<td></td>
<td>&nbsp;</td>
<td>
    <asp:Button ID="bt_crearCoti" runat="server" onclick="bt_crearCoti_Click" 
        Text="Crear Cotizacion" Font-Size="Small" Width="150px" />
    </td>
<td></td>
<td>
    <asp:Button ID="bt_R144" runat="server" Font-Size="Small" 
        onclick="bt_R144_Click" Text="R-144" Width="150px" />
    </td>
<td></td>
<td></td>
<td></td>
<td></td>
</tr>

<tr>
<td></td>
<td>
        <asp:CheckBox ID="cb_el" runat="server" Text="Ascensor"  AutoPostBack = "true"
            oncheckedchanged="cb_el_CheckedChanged" />
        </td>
<td>
        <asp:CheckBox ID="cb_ellos" runat="server" Text="Ascensores" AutoPostBack = "true"
            oncheckedchanged="cb_ellos_CheckedChanged" />
        </td>
<td></td>
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
</div>


<div class="vista1">

    <asp:GridView ID="gv_cotizacionRepuesto" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        onselectedindexchanged="gv_cotizacionRepuesto_SelectedIndexChanged"
       
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
<h3>Repuesto</h3>
</div>

<div class="Grepuesto">

    <asp:GridView ID="gv_repuestoUne" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        AutoGenerateColumns="False" 
        onselectedindexchanged="gv_repuestoUne_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>

          

            <asp:BoundField DataField="Numeracion" HeaderText="Numeracion" 
                SortExpression="Numeracion" />
            <asp:BoundField DataField="CodigoRepuesto" HeaderText="CodigoRepuesto" 
                SortExpression="CodigoRepuesto" />
            <asp:BoundField DataField="________________Denominacion________________" 
                HeaderText="________________Denominacion________________" 
                SortExpression="________________Denominacion________________" />
            <asp:BoundField DataField="cantidad" HeaderText="cantidad" 
                SortExpression="cantidad" />
            <asp:BoundField DataField="precioUnitario" HeaderText="precioUnitario" 
                SortExpression="precioUnitario" />
            <asp:BoundField DataField="precioTotal" HeaderText="precioTotal" 
                SortExpression="precioTotal" />
            <asp:TemplateField HeaderText="cant_almacenlocal" 
                SortExpression="cant_almacenlocal">                
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("cant_almacenlocal") %>'></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("cant_almacenlocal") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="cant_almacenjyc" 
                SortExpression="cant_almacenjyc">                
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("cant_almacenjyc") %>'></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("cant_almacenjyc") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="cant_almacenjycia" 
                SortExpression="cant_almacenjycia">                
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("cant_almacenjycia") %>'></asp:Label>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("cant_almacenjycia") %>'></asp:TextBox>
                </ItemTemplate>
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
    <asp:Label ID="Label5" runat="server" Font-Size="Small" 
        Text="Detalle de Cierre:"></asp:Label>
    </td>
<td></td>
<td>
    <asp:TextBox ID="tx_cierre" runat="server" Height="85px" TextMode="MultiLine" 
        Width="464px"></asp:TextBox>
    </td>
<td>
    &nbsp;</td>
<td>
    <asp:Button ID="bt_cerrarCotizacion" runat="server" 
        onclick="bt_cerrarCotizacion_Click" Text="Cerrar Cotizacion" />
    </td>

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
