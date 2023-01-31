<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_FechaContrato.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_FechaContrato" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_fechaContratos.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>


<div class="Centrar">
<div class="titulo">
  <h3>  <asp:Label ID="Label1" runat="server" Text="Fechas Contrato"></asp:Label> </h3>
</div>

<table>
<tr>
<td>
<div class="fc1">
    <table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label2" runat="server" Text="Exbo :" Font-Size="X-Small"></asp:Label></td>
    <td>
        <asp:TextBox ID="tx_exbo" runat="server" Width="160px" Font-Size="Small" 
            Height="25px"></asp:TextBox></td>
    <td>
        <asp:Label ID="Label7" runat="server" Text="Acta Definitiva :" 
            Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_fechaActaDefinitiva" runat="server" Width="160px" 
            Font-Size="Small" Height="25px" Enabled="False"></asp:TextBox>
        </td>
    <td>
        &nbsp;</td>
    <td>
        <asp:Label ID="Label9" runat="server" Text="Firma Contrato :" 
            Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_contratoFirmado" runat="server" Font-Size="Small" 
            Height="25px" Width="160px"></asp:TextBox>
        <asp:CalendarExtender ID="tx_contratoFirmado_CalendarExtender" runat="server" 
            TargetControlID="tx_contratoFirmado">
        </asp:CalendarExtender>
        </td>
    </tr>
    
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label3" runat="server" Text="Edificio:" Font-Size="X-Small"></asp:Label></td>
    <td>
        <asp:TextBox ID="tx_edificio" runat="server" Width="160px" Font-Size="Small" 
            Height="25px"></asp:TextBox></td>
    <td>
        <asp:Label ID="Label8" runat="server" Text="Equipo Entregado :" 
            Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_fechaEquipoEntregado" runat="server" Width="160px" 
            Font-Size="Small" Height="25px" Enabled="False"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label13" runat="server" Text="Nro. de Contrato:" 
            Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_nroContrato" runat="server" Font-Size="Small" 
            Height="25px" Width="160px">0</asp:TextBox>
        </td>
    </tr>
    
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label4" runat="server" Text="Tipo Equipo:" Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_tipoEquipo" runat="server" Width="160px" 
            Font-Size="Small" Height="25px" Enabled="False"></asp:TextBox>
        </td>
    <td>
        <asp:Label ID="Label22" runat="server" Font-Size="X-Small" 
            Text="Habilitacion del Equipo:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_habilitacionEquipo" runat="server" Enabled="False" 
            Font-Size="Small" Height="25px" Width="160px"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label14" runat="server" Text="Monto Contrato:" 
            Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_montoContrato" runat="server" Font-Size="Small" 
            Height="25px" Width="160px">0</asp:TextBox>
        </td>
    </tr>

    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label5" runat="server" Text="Marca :" Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_marca" runat="server" Width="160px" Font-Size="Small" 
            Height="25px" Enabled="False"></asp:TextBox>
        </td>
    <td>
        &nbsp;</td>
    <td>
        &nbsp;</td>
    <td></td>
    <td>
        &nbsp;</td>
    <td>
        &nbsp;</td>
    </tr>

    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label6" runat="server" Text="Paradas :" Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_paradas" runat="server" Width="160px" Font-Size="Small" 
            Height="25px" Enabled="False"></asp:TextBox>
        </td>
    <td>
        <asp:Label ID="Label10" runat="server" Text="Inicio Man. Gratuito:" 
            Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_InicioManGratuito" runat="server" Font-Size="Small" 
            Height="25px" Width="160px"></asp:TextBox>
        <asp:CalendarExtender ID="tx_InicioManGratuito_CalendarExtender" runat="server" 
            TargetControlID="tx_InicioManGratuito">
        </asp:CalendarExtender>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label16" runat="server" Font-Size="X-Small" 
            Text="Contrato Inicio:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_fechaContratoInicio" runat="server" Height="25px" 
            Width="160px" Font-Size="Small"></asp:TextBox>
        <asp:CalendarExtender ID="tx_fechaContratoInicio_CalendarExtender" 
            runat="server" TargetControlID="tx_fechaContratoInicio">
        </asp:CalendarExtender>
        </td>
    </tr>

    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label15" runat="server" Text="Pasajeros:" Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_pasajeros" runat="server" Width="160px" Font-Size="Small" 
            Height="25px" Enabled="False"></asp:TextBox>
        </td>
    <td>
        <asp:Label ID="Label11" runat="server" Text="Meses Man. Gratuito:" 
            Font-Size="X-Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_mesesManGratuito" runat="server" Font-Size="Small" 
            Height="25px" Width="160px"  >0</asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label19" runat="server" Font-Size="X-Small" 
            Text="Meses Contrato:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_mesesContrato" runat="server" Height="25px" Width="160px" 
            Font-Size="Small"></asp:TextBox>
        </td>
    </tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label20" runat="server" Font-Size="X-Small" Text="Velocidad :"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_velocidad" runat="server" Font-Size="Small" Height="25px" 
        Width="160px" Enabled="False"></asp:TextBox>
    </td>
<td>
        <asp:Label ID="Label12" runat="server" Text="Fin Man. Gratuito:" 
            Font-Size="X-Small"></asp:Label>
        </td>
<td>
        <asp:TextBox ID="tx_fechaFinManGratuito" runat="server" Font-Size="Small" 
            Height="25px" Width="160px" Enabled="False"></asp:TextBox>
        </td>
<td></td>
<td>
        <asp:Label ID="Label17" runat="server" Font-Size="X-Small" Text="Contrato Fin:"></asp:Label>
        </td>
<td>
        <asp:TextBox ID="tx_fechaContratoFin" runat="server" Height="25px" 
            Width="160px" Enabled="False" Font-Size="Small"></asp:TextBox>
        </td>
</tr>


<tr>
<td></td>
<td>
    <asp:Label ID="Label21" runat="server" Font-Size="X-Small" Text="Modelo :"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_modelo" runat="server" Font-Size="Small" Height="25px" 
        Width="160px" Enabled="False"></asp:TextBox>
    </td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td></td>
<td>&nbsp;</td>
<td>&nbsp;</td>
</tr>


    
<tr>
<td></td>
<td></td>
<td>
    <asp:Button ID="bt_limpiar" runat="server" Height="25px" 
        onclick="bt_limpiar_Click" Text="Limpiar" Width="100px" />
    </td>
<td>
    &nbsp;</td>
<td>
    <asp:Button ID="bt_actualizar" runat="server" Text="Actualizar" 
        onclick="bt_actualizar_Click" Height="25px" Width="100px" />
    </td>
<td></td>
<td>&nbsp;</td>
<td>
    <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
        onclick="bt_buscar_Click" Height="25px" Width="100px" />
    </td>
</tr>

    </table>
</div>
</td>
</tr>


<tr>
<td>
<div class="fc2">
    <asp:GridView ID="gv_contratosFirmados" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onselectedindexchanged="gv_contratosFirmados_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
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
<tr>
<td>
    <asp:Label ID="Label18" runat="server" Text="Cantidad Equipos: "></asp:Label>
    <asp:TextBox ID="tx_cantidadEquipos" runat="server" Enabled="False"></asp:TextBox>
</td>
</tr>
<tr>
<td>
    <asp:Button ID="bt_exportarExcel" runat="server" Text="Exportar Excel" 
        onclick="bt_exportarExcel_Click" /></td>
</tr>
<tr>
<td></td>
</tr>



</table>


</div>

</asp:Content>
