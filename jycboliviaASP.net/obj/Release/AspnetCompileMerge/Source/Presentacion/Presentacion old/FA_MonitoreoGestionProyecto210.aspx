<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_MonitoreoGestionProyecto210.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_MonitoreoGestionProyecto210" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_MonitoreoGestionProyecto.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style1
        {
            width: 169px;
        }
        .style2
        {
            width: 156px;
        }
        .style3
        {
            width: 162px;
        }
        .style4
        {
            width: 165px;
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
    <div class= "titulo">
    <h1>Monitoreo Gestion de Proyectos</h1>    
    </div>

    <div class = "MGP1">
<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label22" runat="server" Text="Nombre Proyecto" Font-Bold="True"></asp:Label>
    </td>
<td></td>
<td></td>
<td></td>
<td></td>
<td></td>
</tr>


<tr>
<td></td>
<td>
    <asp:TextBox ID="tx_nombreProyecto" runat="server" Height="24px" Width="164px"></asp:TextBox>
    </td>
<td></td>
<td></td>
<td></td>
<td></td>
<td></td>
</tr>

<tr>
<td></td>
<td class="style1">
    <asp:Label ID="Label1" runat="server" Text="Apertura" Font-Size="Small"></asp:Label>
</td>
<td class="style2">
    <asp:Label ID="Label2" runat="server" Text="Sabida Fab" Font-Size="Small"></asp:Label>
</td>
<td class="style3">
    <asp:Label ID="Label3" runat="server" Text="Entrega Contrato" Font-Size="Small"></asp:Label>
</td>
<td class="style4">
    <asp:Label ID="Label4" runat="server" Text="Entrega Acordada" Font-Size="Small"></asp:Label>
</td>
<td>
    <asp:Label ID="Label5" runat="server" Text="Cobro ADM" Font-Size="Small"></asp:Label>
</td>
<td></td>
</tr>

<tr>
<td></td>
<td class="style1">
    <asp:TextBox ID="tx_fechaApertura" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaApertura_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaApertura">
    </asp:CalendarExtender>
</td>
<td class="style2">
    <asp:TextBox ID="tx_fechaSalidaFab" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaSalidaFab_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaSalidaFab">
    </asp:CalendarExtender>
    </td>
<td class="style3">
    <asp:TextBox ID="tx_fechaEntregaContrato" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaEntregaContrato_CalendarExtender" 
        runat="server" TargetControlID="tx_fechaEntregaContrato">
    </asp:CalendarExtender>
    </td>
<td class="style4">
    <asp:TextBox ID="tx_fechaEntregaAcordada" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaEntregaAcordada_CalendarExtender" 
        runat="server" TargetControlID="tx_fechaEntregaAcordada">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:TextBox ID="tx_fechaCobroAdm" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaCobroAdm_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaCobroAdm">
    </asp:CalendarExtender>
</td>
<td></td>

</tr>

<tr>
<td></td>
<td class="style1">
    <asp:Label ID="Label6" runat="server" Text="Dpt. Administrativo" 
        Font-Bold="True"></asp:Label>
</td>
<td class="style2"></td>
<td class="style3"></td>
<td class="style4"></td>
<td></td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label7" runat="server" Text="R-189" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:Label ID="Label8" runat="server" Text="R-190" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:Label ID="Label9" runat="server" Text="R-182" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:Label ID="Label10" runat="server" Text="R-197" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:Label ID="Label11" runat="server" Text="R-198" Font-Size="Small"></asp:Label>
    </td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:TextBox ID="tx_fechaR189" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR189_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR189">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:TextBox ID="tx_fechaR190" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR190_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR190">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:TextBox ID="tx_fechaR182" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR182_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR182">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:TextBox ID="tx_fechaR197" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR197_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR197">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:TextBox ID="tx_fechaR198" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR198_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR198">
    </asp:CalendarExtender>
    </td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:Label ID="Label12" runat="server" Text="R-199" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:Label ID="Label13" runat="server" Text="R-200" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:Label ID="Label14" runat="server" Text="R-201" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:Label ID="Label15" runat="server" Text="R-202" Font-Size="Small"></asp:Label>
    </td>
<td></td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:TextBox ID="tx_fechaR199" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR199_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR199">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:TextBox ID="tx_fechaR200" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR200_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR200">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:TextBox ID="tx_fechaR201" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR201_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR201">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:TextBox ID="tx_fechaR202" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR202_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR202">
    </asp:CalendarExtender>
    </td>
<td></td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label16" runat="server" Text="Dpto. Proyectos" Font-Bold="True"></asp:Label>
    </td>
<td></td>
<td></td>
<td></td>
<td></td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:Label ID="Label17" runat="server" Text="R-188" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:Label ID="Label18" runat="server" Text="R-183" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:Label ID="Label19" runat="server" Text="R-184" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:Label ID="Label20" runat="server" Text="R-194" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:Label ID="Label21" runat="server" Text="R-186" Font-Size="Small"></asp:Label>
    </td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:TextBox ID="tx_fechaR188" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR188_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR188">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:TextBox ID="tx_fechaR183" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR183_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR183">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:TextBox ID="tx_fechaR184" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR184_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR184">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:TextBox ID="tx_fechaR194" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR194_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR194">
    </asp:CalendarExtender>
    </td>
<td>
    <asp:TextBox ID="tx_fechaR186" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_fechaR186_CalendarExtender" runat="server" 
        TargetControlID="tx_fechaR186">
    </asp:CalendarExtender>
    </td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:Button ID="bt_Actualizar_datos" runat="server" Text="Actualizar" 
        Height="32px" onclick="bt_Actualizar_datos_Click" Width="127px" 
        style="margin-top: 14px" />
    </td>
<td>
    <asp:Button ID="bt_Buscar" runat="server" Height="32px" Text="Buscar" 
        Width="127px" onclick="bt_Buscar_Click" style="margin-top: 14px" />
    </td>
<td></td>
<td></td>
<td></td>
<td></td>
</tr>

</table>

</div>



<div class = "MGP2">

    <asp:GridView ID="gv_MonitoreoGestionProyecto" runat="server" 
        BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
        CellPadding="4" CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onselectedindexchanged="gv_MonitoreoGestionProyecto_SelectedIndexChanged">
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


 </div>

<div class = "MGP3">
</div>




</asp:Content>
