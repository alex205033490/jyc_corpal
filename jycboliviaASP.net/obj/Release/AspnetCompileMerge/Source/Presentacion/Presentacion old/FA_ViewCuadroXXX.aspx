<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ViewCuadroXXX.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ViewCuadroXXX" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ViewCuadroXXX.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

<div class = "Centrar">
<div class = ""> <h1> CuadroXXX </h1> </div>

<div class = "MenuArriba">
<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label7" runat="server" Text="Exbo:"></asp:Label>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_exbo" runat="server" Height="25px" Width="100px"></asp:TextBox>
</td>
<td></td>
<td>
    <asp:Label ID="Label8" runat="server" Text="Edificio:"></asp:Label>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_nombreProyecto" runat="server" Height="25px" Width="125px"></asp:TextBox>
</td>
<td></td>
<td>
    <asp:Label ID="Label1" runat="server" Text="Año del Seguimiento :"></asp:Label>
</td>
<td></td>
<td>
    <asp:DropDownList ID="dd_yearSeguimiento" runat="server" Height="25px" 
        Width="100px">
    </asp:DropDownList>
</td>
<td></td>
<td>
    <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
        onclick="bt_buscar_Click" />
</td>
</tr>
</table>
</div>


<div class = "CuadroXXX">
    <asp:GridView ID="gv_CuadroXXX" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onselectedindexchanged="gv_CuadroXXX_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#669900" Font-Bold="True" Font-Size="X-Small" 
            ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
</div>



<div class ="extraerExcel" >
    <asp:Button ID="bt_Excel" runat="server" Text="Descargar Excel" 
        onclick="bt_Excel_Click" />
</div>


<div class="doble">
<table>
<tr>
<td>
<div class = "Encargados">
<h3>Encargados</h3>
<table>
<tr>
<td>
    <asp:Label ID="Label2" runat="server" Text="Tecnico Mantenimiento :"></asp:Label>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_TecnicoMantenimiento" runat="server" Height="22px" 
        Width="170px"></asp:TextBox>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label3" runat="server" Text="Supervisor Tecnico :"></asp:Label>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_SupervisorTecnico" runat="server" Width="170px"></asp:TextBox>
</td>
</tr>

<tr>
<td>
    <asp:Label ID="Label4" runat="server" Text="Tecnico Instalador :"></asp:Label>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_TecnicoInstalador" runat="server" Width="170px"></asp:TextBox>
</td>
</tr>

<tr>
<td>
    <asp:Label ID="Label5" runat="server" Text="Cobrador :"></asp:Label>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_Cobrador" runat="server" Width="170px"></asp:TextBox>
</td>
</tr>

<tr>
<td>
    <asp:Label ID="Label6" runat="server" Text="Encargado Cobranza :"></asp:Label>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_EncargadoCobranza" runat="server" Width="170px"></asp:TextBox>
</td>
</tr>

</table>
</div>
</td>


<td>
<div class = "HistorialEstados">
<h3>Historial de Estado Seguimiento</h3>
    <asp:GridView ID="gv_historialEstados" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#669900" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
</div>
</td>
</tr>



</table>


</div>








<div class = "Pie1"></div>








</div>





</asp:Content>
