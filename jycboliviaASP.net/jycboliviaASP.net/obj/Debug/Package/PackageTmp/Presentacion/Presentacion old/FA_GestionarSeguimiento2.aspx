<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_GestionarSeguimiento2.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_GestionarSeguimiento2" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 144px;
        }
        .style2
        {
            width: 38px;
        }
        .style3
        {
            width: 72px;
        }
        .style4
        {
            width: 32px;
        }
    </style>
    <link href="../Styles/Style_GSeguimiento.css" rel="stylesheet" type="text/css" />    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

   <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">    </asp:ScriptManager>

   
   <table>
   <tr>
   <td>
   <div class="GS2_1">    
<p>Mantenimiento</p>
<table style="height: 250px">    
    <tr>
    <td></td>
    <td >
        <asp:Label ID="lb_anio" runat="server" Font-Size="Small" Text="Año:"></asp:Label>
        </td>
    <td><asp:TextBox ID="tx_year" runat="server" Font-Size="Small" Width="125px"></asp:TextBox></td>    
    <td class="style2"></td>    
    </tr>
    <tr>
    <td></td>
    <td >
        <asp:Label ID="lb_horaCobro" runat="server" Font-Size="Small" 
            Text="Hora Cobro:"></asp:Label>
        </td>
    <td>        <asp:TextBox ID="tx_horaCobro" runat="server" Font-Size="Small" 
            Width="125px"></asp:TextBox>        </td>
    <td >
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="tx_horaCobro" ErrorMessage="Error de Hora" 
            ForeColor="#FF3300" 
            ValidationExpression="^(0[1-9]|1\d|2[0-3]):([0-5]\d):([0-5]\d)$">Error</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
    <td></td>
        <td class="style1">
            <asp:Label ID="lb_diaCobroPlanificado" runat="server" Font-Size="Small" 
                Text="Dia Cobro:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_fechaCobroPlanificado" runat="server" Font-Size="Small" 
                Width="125px"></asp:TextBox>
        </td>
        <td>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tx_fechaCobroPlanificado" >
            </asp:CalendarExtender>
        </td>
    </tr>
    <tr>
    <td></td>
        <td >
            <asp:Label ID="lb_PlanPago" runat="server" Font-Size="Small" Text="Plan Pago:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="dd_PlanPago" runat="server" Height="20px" Width="125px" 
                Font-Size="Small">
            </asp:DropDownList>
        </td>
        <td></td>
    </tr>
     <tr>
     <td></td>
        <td >
            <asp:Label ID="lb_estadoMantenimiento" runat="server" Font-Size="Small" 
                Text="Estado Mantenim:"></asp:Label>
         </td>
        <td>
            <asp:DropDownList ID="dd_estadoMantenimiento" runat="server" Height="20px" 
                Width="125px" Font-Size="Small">
            </asp:DropDownList>
        </td>
        <td></td>
    </tr>

    <tr>
    <td></td>
        <td class="style1">
            <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="Lugar Pago:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_lugarPago" runat="server" Font-Size="Small" Width="125px"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
        
    <tr>
    <td></td>
        <td class="style1">
            <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="Fecha Contrato :"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_FechaContrato" runat="server" Font-Size="Small" 
                Width="125px"></asp:TextBox>
            <asp:CalendarExtender ID="tx_FechaContrato_CalendarExtender" runat="server" 
                TargetControlID="tx_FechaContrato">
            </asp:CalendarExtender>
        </td>
        <td> 
            &nbsp;</td>
    </tr>   
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="Meses Gratuitos:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_MesesGratuitos" runat="server" Font-Size="Small" 
            Width="125px">0</asp:TextBox>
        </td>
    <td></td>
    </tr>
     <tr>
    <td></td>
    <td>
        <asp:Label ID="Label4" runat="server" Font-Size="Small" Text="Mes G. Inicial:"></asp:Label>
         </td>
    <td>
        <asp:TextBox ID="tx_MesInicial" runat="server" Font-Size="Small" Width="125px"></asp:TextBox>
         <asp:CalendarExtender ID="tx_MesInicial_CalendarExtender" runat="server" 
            TargetControlID="tx_MesInicial">
        </asp:CalendarExtender>
         </td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label5" runat="server" Font-Size="Small" Text="Mes G. Final:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_MesFinal" runat="server" Enabled="False" Font-Size="Small" 
            Width="125px"></asp:TextBox>
        </td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Detalle:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_Detalle" runat="server" Height="69px" TextMode="MultiLine" 
            Width="125px" Font-Size="Small"></asp:TextBox>
        </td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="lb_mensualidad" runat="server" Font-Size="Small" 
            Text="Mensualidad:"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_Mensualidad" runat="server" Font-Size="Small" Width="125px">0</asp:TextBox>
        </td>
    <td></td>
    </tr>
</table>

<table>
<tr>
<td class="style3"></td>
<td>
    <asp:CheckBox ID="cb_enero" runat="server" Text="Enero" Font-Size="Small" />
    </td>
<td>
    <asp:CheckBox ID="cb_julio" runat="server" Text="Julio" Font-Size="Small" />
    </td>
<td></td>
</tr>
<tr>
<td class="style3"></td>
<td>
    <asp:CheckBox ID="cb_febrero" runat="server" Text="Febrero" Font-Size="Small" />
    </td>
<td>
    <asp:CheckBox ID="cb_agosto" runat="server" Text="Agosto" Font-Size="Small" />
    </td>
<td></td>
</tr>
<tr>
<td class="style3"></td>
<td>
    <asp:CheckBox ID="cb_marzo" runat="server" Text="Marzo" Font-Size="Small" />
    </td>
<td>
    <asp:CheckBox ID="cb_septiembre" runat="server" Text="Septiembre" 
        Font-Size="Small" />
    </td>
<td></td>
</tr>
<tr>
<td class="style3"></td>
<td>
    <asp:CheckBox ID="cb_abril" runat="server" Text="Abril" Font-Size="Small" />
    </td>
<td>
    <asp:CheckBox ID="cb_octubre" runat="server" Text="Octrubre" 
        Font-Size="Small" />
    </td>
<td></td>
</tr>
<tr>
<td class="style3"></td>
<td>
    <asp:CheckBox ID="cb_mayo" runat="server" Text="Mayo" Font-Size="Small" />
    </td>
<td>
    <asp:CheckBox ID="cb_noviembre" runat="server" Text="Noviembre" 
        Font-Size="Small" />
    </td>
<td></td>
</tr>
<tr>
<td class="style3"></td>
<td>
    <asp:CheckBox ID="cb_junio" runat="server" Text="Junio" Font-Size="Small" />
    </td>
<td>
    <asp:CheckBox ID="cb_diciembre" runat="server" Text="Diciembre" 
        Font-Size="Small" />
    </td>
<td></td>
</tr>
</table>

<table>
<tr>
<td class="style4"></td>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td></td>
<td></td>
</tr>
<tr>
<td class="style4"></td>
<td>     
            <asp:Button ID="bt_insertarSeguimiento" runat="server" Text="Insertar" 
                Height="25px" onclick="bt_insertarSeguimiento_Click" 
        Width="80px" />
            </td>
<td>
            <asp:Button ID="bt_Modificar" runat="server" Text="Modificar" 
                Height="25px" onclick="bt_Modificar_Click" Width="80px" />
    </td>
<td>
            <asp:Button ID="bt_limpiar" runat="server" Text="Limpiar" Height="25px" 
                onclick="tx_limpiar_Click" Width="80px" />
            </td>
<td></td>
</tr>
</table>


</div>
   </td>



   <td>
    <div class="GS2_2">
<p>Seguimiento de Mantenimiento    </p>
<asp:GridView ID="gv_SeguimientoMantenimiento" runat="server" 
        BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
        CellPadding="4" CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        PageSize="7" 
        onselectedindexchanged="gv_SeguimientoMantenimiento_SelectedIndexChanged">
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
<div class="GS2_3">
<p>Meses Pago</p>
<asp:GridView ID="gv_seguiMes" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onrowcancelingedit="gv_seguiMes_RowCancelingEdit" 
        onrowediting="gv_seguiMes_RowEditing" 
        onrowupdating="gv_seguiMes_RowUpdating" 
        onrowdatabound="gv_seguiMes_RowDataBound">
    <Columns>
        <asp:CommandField ShowEditButton="True" />
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
   
   </table>
   
   
<div class="GS2_4">     
</div>


<div class="GS2_5"></div>
    </asp:Content>
