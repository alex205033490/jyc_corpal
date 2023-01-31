<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CallCenterEstadisticaNormal.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CallCenterEstadisticaNormal"  EnableEventValidation = "false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menuIzquierdo" Src="MenuIzquierdo.ascx" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_CallCenterEstadisticaNormal.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>
   
    <asp:ScriptManager ID="ScriptManager1" runat="server"  EnableScriptGlobalization="True">
    </asp:ScriptManager>

<table>
<tr>
<td>
    <table>
    <tr>
    <td>
        <inmoInfo:menuIzquierdo ID="MenuIzquierdo1"  runat="server"/>
    </td>
    </tr>    

    <tr><td style="height:500px;"></td></tr>
    
    </table>
        
</td>

<td>
    <table>
    <tr>
    <td>

    <div class = "Centrar">
<table>
<tr>
<td>
<div class = "titulo"><h3>
        <asp:Label ID="lb_eventos" runat="server" Text="Estadistica Call Center"></asp:Label></h3></div>    
</td>
</tr>


<tr>
<td>
<div class = "titulo">
    <table>
    <tr>
    <td>
        <asp:Label ID="Label35" runat="server" Text="Base de Datos:"></asp:Label></td>
    <td>
    
        <asp:DropDownList ID="dd_baseDeDatos" runat="server" Height="25px" 
            Width="130px" 
            AutoPostBack="True" 
            onselectedindexchanged="dd_baseDeDatos_SelectedIndexChanged">
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
            <asp:ListItem>Asuncion-Paraguay</asp:ListItem>
        </asp:DropDownList>    
    </td>
    </tr>
    </table>
    </div>
</td>
</tr>

<tr>
<td>
<div class="e1">
    <table>
    <tr>
    <td class="style3"></td>
    <td>
        <asp:Label ID="Label11" runat="server" Text="Une :" Font-Size="Small"></asp:Label>
        </td>
    <td></td>
    
    <td class="style4">
        <asp:TextBox ID="tx_BaseDeDatos" runat="server" Height="25px"></asp:TextBox>
        </td>
    <td>
        <asp:Label ID="Label12" runat="server" Text="Edificio :" Font-Size="Small"></asp:Label></td>
    <td>
        <asp:TextBox ID="tx_nombreEdificioBusqueda" runat="server" Width="200px" 
            Height="25px"></asp:TextBox></td>
    <td></td>
    <td>
        <asp:Button ID="bt_Buscar" runat="server" Text="Buscar" 
            onclick="bt_Buscar_Click" Height="25px" Width="100px" /></td>
    <td></td>
    </tr>

    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label37" runat="server" Text="Semana :" Font-Size="Small"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:TextBox ID="tx_SemanaBusqueda" runat="server" Height="25px"></asp:TextBox>
        </td>
    <td>
        <asp:Label ID="Label36" runat="server" Text="LLamada :" Font-Size="Small"></asp:Label>
        </td>
    <td>
        <asp:DropDownList ID="dd_tipoEvento1" runat="server" Height="25px" 
            Width="125px">
        </asp:DropDownList>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label40" runat="server" Font-Size="Small" Text="Evento :"></asp:Label>
        </td>
    <td></td>
    
    </tr>

    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label38" runat="server" Font-Size="Small" Text="desde :"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:TextBox ID="tx_desdeBusqueda" runat="server" Height="25px"></asp:TextBox>
        <asp:CalendarExtender ID="tx_desdeBusqueda_CalendarExtender" runat="server" 
            TargetControlID="tx_desdeBusqueda">
        </asp:CalendarExtender>
        </td>
    <td>
        <asp:Label ID="Label39" runat="server" Font-Size="Small" Text="Hasta :"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_hastaBusqueda" runat="server" Height="25px"></asp:TextBox>
        <asp:CalendarExtender ID="tx_hastaBusqueda_CalendarExtender" runat="server" 
            TargetControlID="tx_hastaBusqueda">
        </asp:CalendarExtender>
        </td>
    <td></td>
    <td>
        <asp:DropDownList ID="dd_evento" runat="server" Height="25px" Width="100px">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Abierto</asp:ListItem>
            <asp:ListItem>Cerrado</asp:ListItem>
        </asp:DropDownList>
        </td>
    <td></td>
    </tr>

    </table>
    </div>   
</td>
</tr>

<tr>
<td>
<div class="e2">
        <asp:GridView ID="gv_tablaEventos" runat="server" BackColor="#CCCCCC" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
            CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
            onselectedindexchanged="gv_tablaEventos_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" 
                BorderColor="Black" BorderStyle="Double" />
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
                            <asp:LinkButton ID="lk_excelEventos" runat="server" 
                            onclick="lk_excelEventos_Click" Font-Size="X-Small">Excel Eventos</asp:LinkButton>
                            </td>
                            </tr>

<tr>
<td>
    <asp:Label ID="Label1" runat="server" Text="Cant. Eventos :"></asp:Label>
    <asp:TextBox ID="tx_cantidadEventos" runat="server"></asp:TextBox>
</td>
</tr>

<tr>
<td>
<div class="e3">
    <asp:GridView ID="gv_Tecnicos" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
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
<asp:LinkButton ID="lb_tecnicos" runat="server" 
   onclick="lk_exceltecnicos_Click" Font-Size="X-Small">Excel Tecnicos</asp:LinkButton>
</td>
</tr>

<tr>
<td></td>
</tr>

</table>
</div>

    </td>
    </tr>
    </table>
</td>
</tr>
</table>



</asp:Content>
