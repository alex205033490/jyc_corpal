<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_GestionarSeguimientoInstalacion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_GestionarSeguimientoInstalacion" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_GestionSeguimientoInstalacion.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 148px;
        }
        .style2
        {
            width: 40px;
        }
        .style3
        {
            width: 209px;
        }
        .style4
        {
            width: 207px;
        }
        .style8
        {
            width: 130px;
        }
        .style14
        {
            width: 117px;
        }
        .style15
        {
            width: 122px;
        }
        .style16
        {
            width: 167px;
        }
        .style17
        {
            width: 120px;
        }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>


    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <br />


    <div class="Centrar">
    <div class="titulo">
    <h1>Seguimiento de Instalacion</h1>
    </div>
    
<div class = "GSI2" >
<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label44" runat="server" Text="Exbo / Serie :"></asp:Label>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_ExboSerie" runat="server"></asp:TextBox>
</td>
<td></td>
<td>
    <asp:Label ID="Label45" runat="server" Text="Nombre Edificio :"></asp:Label>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_edificiobuscar" runat="server"></asp:TextBox>
</td>
<td></td>
<td>
    <asp:Button ID="Button1" runat="server" Text="Buscar" onclick="Button1_Click" />
</td>
</tr>
</table>
</div>


<div class = "GSI1" >
<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label1" runat="server" Text="Exbo/Serie" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Proyecto" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label3" runat="server" Text="Tipologia" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Une" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label5" runat="server" Text="Estado Actual" Font-Size="X-Small"></asp:Label></td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:TextBox ID="tx_Exbo" runat="server"></asp:TextBox></td>
<td></td>
<td>
    <asp:TextBox ID="tx_proyecto" runat="server"></asp:TextBox></td>
<td></td>
<td>
    <asp:TextBox ID="tx_tipologia" runat="server"></asp:TextBox> </td>
<td></td>
<td>
    <asp:TextBox ID="tx_Une" runat="server"></asp:TextBox></td>
<td></td>
<td>
    <asp:DropDownList ID="dd_estadoInstalacion" runat="server" Width="150px">
    </asp:DropDownList>
    </td>
<td></td>
</tr>
</table>
<table>
<tr>
<td class="style2"></td>
<td>
 <asp:Label ID="Label6" runat="server" Text="Datos Obra :" Font-Bold="True"></asp:Label>
</td>
</tr>
</table>



<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label7" runat="server" Text="Dir. de Obra" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label8" runat="server" Text="Supervisor" Font-Size="X-Small"></asp:Label>
</td>
<td></td>
<td>
    <asp:Label ID="Label9" runat="server" Text="Email" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label10" runat="server" Text="Telefono" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td></td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:TextBox ID="tx_dirObra" runat="server"></asp:TextBox></td>
<td></td>
<td>
    <asp:TextBox ID="tx_Supervisor" runat="server"></asp:TextBox></td>
<td></td>
<td>
    <asp:TextBox ID="tx_email" runat="server"></asp:TextBox></td>
<td></td>
<td>
    <asp:TextBox ID="tx_Telefono" runat="server"></asp:TextBox></td>
<td></td>
<td></td>
<td></td>
</tr>
</table>
<table>
<tr>
<td class="style2"></td>
<td>
    <asp:Label ID="Label11" runat="server" Text="Confirmación Documento de Fabrica :" 
        Font-Bold="True"></asp:Label>
</td>
</tr>
</table>


<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label12" runat="server" Text="AOI" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label13" runat="server" Text="Plano" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label14" runat="server" Text="Otros" Font-Size="X-Small"></asp:Label></td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:DropDownList ID="dd_aoi" runat="server" Width="100px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Aprobado</asp:ListItem>
        <asp:ListItem>Pendiente</asp:ListItem>
    </asp:DropDownList>
</td>
<td></td>
<td>
    <asp:DropDownList ID="dd_plano" runat="server" Width="100px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Pendiente</asp:ListItem>
        <asp:ListItem>Aprobado</asp:ListItem>
    </asp:DropDownList>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_otros_PendientesFabrica" runat="server"></asp:TextBox></td>
<td></td>
</tr>
</table>
<table>
<tr>
<td class="style2"></td>
<td>
<asp:Label ID="Label15" runat="server" Text="Documentos Enviar a JYC :" 
        Font-Bold="True"></asp:Label>
</td>
</tr>
</table>

<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label16" runat="server" Text="C-01 (R-192)" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label17" runat="server" Text="C-05 (R-188)" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label18" runat="server" Text="FAI (R-184)" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label19" runat="server" Text="FAII (R-194)" Font-Size="X-Small"></asp:Label></td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:DropDownList ID="dd_C01" runat="server" Width="100px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Enviado</asp:ListItem>
        <asp:ListItem>Pendiente</asp:ListItem>
    </asp:DropDownList>
</td>
<td></td>
<td>
    <asp:DropDownList ID="dd_c05" runat="server" Width="100px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Enviado</asp:ListItem>
        <asp:ListItem>Pendiente</asp:ListItem>
    </asp:DropDownList>
</td>
<td></td>
<td>
    <asp:DropDownList ID="dd_FAI" runat="server" Width="100px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Enviado</asp:ListItem>
        <asp:ListItem>Pendiente</asp:ListItem>
    </asp:DropDownList>
</td>
<td></td>
<td>
    <asp:DropDownList ID="dd_FAII" runat="server" Width="100px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Enviado</asp:ListItem>
        <asp:ListItem>Pendiente</asp:ListItem>
    </asp:DropDownList>
</td>
<td></td>
</tr>
</table>
<table>
<tr>
<td class="style2"></td>
<td>
    <asp:Label ID="Label20" runat="server" Text="Estado de Obra :" Font-Bold="True"></asp:Label>
</td>
</tr>
</table>

<table>
<tr>
<td></td>
<td><asp:Label ID="Label21" runat="server" Text="Estado de Obra Adecuado" 
        Font-Size="X-Small"></asp:Label></td>    
<td></td>
<td>
    <asp:Label ID="Label22" runat="server" Text="Observaciones" Font-Size="X-Small"></asp:Label></td>
    <td></td>
<td>
    <asp:Label ID="Label23" runat="server" Text="Electricidad" Font-Size="X-Small"></asp:Label></td>
    <td></td>
<td class="style1">
    <asp:Label ID="Label24" runat="server" 
        Text="Otros,Aplicaciones, Modificaciones,Seguridad " Font-Size="X-Small"></asp:Label></td>
    <td></td>
<td>
    <asp:Label ID="Label25" runat="server" Text="Cumplimientos de Requisitos" 
        Font-Size="X-Small"></asp:Label></td>
    <td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:DropDownList ID="dd_EstadoObraAdecuado" runat="server" Width="100px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Si</asp:ListItem>
        <asp:ListItem>No</asp:ListItem>
    </asp:DropDownList>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_ObservacionesEstadoObra" runat="server"></asp:TextBox>
</td>
<td></td>
<td>
    <asp:DropDownList ID="dd_Electricidad" runat="server" Width="100px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Si</asp:ListItem>
        <asp:ListItem>No</asp:ListItem>
    </asp:DropDownList>
</td>
<td></td>
<td>
    <asp:TextBox ID="tx_otrosApliModSegu" runat="server"></asp:TextBox></td>
<td></td>
<td>
    <asp:DropDownList ID="dd_CumplimientoRequisitos" runat="server" Width="100px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Si</asp:ListItem>
        <asp:ListItem>No</asp:ListItem>
    </asp:DropDownList>
</td>
<td></td>
</tr>
</table>
<table>
<tr>
<td class="style2"></td>
<td>
    <asp:Label ID="Label26" runat="server" Text="Informacion Critica :" 
        Font-Bold="True"></asp:Label>
</td>
</tr>
</table>


<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label27" runat="server" Text="Fecha de Expedicion" 
        Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label28" runat="server" Text="Equipo en Obra" 
        Font-Size="X-Small"></asp:Label></td>
<td></td>
<td class="style16">
    <asp:Label ID="Label31" runat="server" Text="Fecha Equipo Entregado Segun Contrato:" 
        Font-Size="X-Small"></asp:Label></td>
<td></td>
<td class="style15">
    <asp:Label ID="Label30" runat="server" Text="Semana Entrega Segun Contrato:" 
        Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    &nbsp;</td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:TextBox ID="tx_IC_fechaExpedicion" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="tx_IC_fechaExpedicion_CalendarExtender" 
        runat="server" TargetControlID="tx_IC_fechaExpedicion">
    </asp:CalendarExtender>
    </td>
<td></td>
<td>
    <asp:TextBox ID="tx_IC_EstimadoEquipoObraSemana" runat="server" Enabled="False"></asp:TextBox>
    <asp:CalendarExtender ID="tx_IC_EstimadoEquipoObraSemana_CalendarExtender" 
        runat="server" TargetControlID="tx_IC_EstimadoEquipoObraSemana">
    </asp:CalendarExtender>
    </td>
<td></td>
<td class="style16">
    <asp:TextBox ID="tx_IC_FechaEntregaRequerida" runat="server" Enabled="False"></asp:TextBox>
    <asp:CalendarExtender ID="tx_IC_FechaEntregaRequerida_CalendarExtender" 
        runat="server" TargetControlID="tx_IC_FechaEntregaRequerida">
    </asp:CalendarExtender>
    </td>
<td></td>
<td class="style15">
    <asp:TextBox ID="tx_IC_SemanaEntregaRequerida" runat="server" Enabled="False">0</asp:TextBox></td>
<td></td>
<td>
    &nbsp;</td>
<td></td>
</tr>
</table>
<table>
<tr>
<td class="style2"></td>
<td>
    <asp:Label ID="Label32" runat="server" Text="Entrega :" Font-Bold="True"></asp:Label>
</td>
</tr>
</table>
   
 <table>
    <tr>
    <td></td>
    <td class="style14">
        <asp:Label ID="Label46" runat="server" 
            Text="Fecha Inicio de Instalacion (Fase I)" Font-Size="X-Small"></asp:Label>
    </td>
    <td></td>
    <td class="style14">
        <asp:Label ID="Label47" runat="server" 
            Text="Fecha de Entrega Instalacion (Fase I)" Font-Size="X-Small"></asp:Label>
        </td>
    <td></td>
    <td class="style17">
        <asp:Label ID="Label48" runat="server" 
            Text="Fecha Inicio de Instalacion (Fase II)" Font-Size="X-Small"></asp:Label>
        </td>
    <td></td>
    <td class="style8">
        <asp:Label ID="Label49" runat="server" 
            Text="Fecha de Entrega Instalacion (Fase II)" Font-Size="X-Small"></asp:Label>
        </td>
    <td></td>
    
    </tr>
    <tr>
    <td></td>
    <td class="style14">
        <asp:TextBox ID="tx_entrega_inicioFase1" runat="server" Enabled="False"></asp:TextBox>
        <asp:CalendarExtender ID="tx_entrega_inicioFase1_CalendarExtender" 
            runat="server" TargetControlID="tx_entrega_inicioFase1">
        </asp:CalendarExtender>
        </td>
    <td></td>
    <td class="style14">
        <asp:TextBox ID="tx_entrega_entregaFase1" runat="server" Enabled="False"></asp:TextBox>
        <asp:CalendarExtender ID="tx_entrega_entregaFase1_CalendarExtender" 
            runat="server" TargetControlID="tx_entrega_entregaFase1">
        </asp:CalendarExtender>
        </td>
    <td></td>
    <td class="style17">
        <asp:TextBox ID="tx_entrega_inicioFase2" runat="server" Enabled="False"></asp:TextBox>
        <asp:CalendarExtender ID="tx_entrega_inicioFase2_CalendarExtender" 
            runat="server" TargetControlID="tx_entrega_inicioFase2">
        </asp:CalendarExtender>
        </td>
    <td></td>
    <td class="style8">
        <asp:TextBox ID="tx_entrega_entregaFase2" runat="server" Enabled="False"></asp:TextBox>
        <asp:CalendarExtender ID="tx_entrega_entregaFase2_CalendarExtender" 
            runat="server" TargetControlID="tx_entrega_entregaFase2">
        </asp:CalendarExtender>
        </td>
    <td></td>
    
    </tr>
    </table>
<table>
<tr>
<td></td>
<td class="style3">
    <asp:Label ID="Label37" runat="server" 
        Text="Fecha Acta de Entrega, Conclusion y Conformidad" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td class="style4">
    <asp:Label ID="Label38" runat="server" 
        Text="Fecha de Entrega y Certificacion de Habilitacion de Equipo R-118" 
        Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label39" runat="server" Text="Semanas Estimadas de Instalacion" 
        Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    &nbsp;</td>
<td></td>
<td>
    &nbsp;</td>
<td></td>
</tr>
<tr>
<td></td>
<td class="style3">
    <asp:TextBox ID="tx_Entrega_FechaActaEntrega" runat="server" Enabled="False"></asp:TextBox>
    <asp:CalendarExtender ID="tx_Entrega_FechaActaEntrega_CalendarExtender" 
        runat="server" TargetControlID="tx_Entrega_FechaActaEntrega">
    </asp:CalendarExtender>
    </td>
<td></td>
<td class="style4">
    <asp:TextBox ID="tx_Entrega_FechaEntregaYCertificacionR118" runat="server" 
        Enabled="False"></asp:TextBox>
    <asp:CalendarExtender ID="tx_Entrega_FechaEntregaYCertificacionR118_CalendarExtender" 
        runat="server" TargetControlID="tx_Entrega_FechaEntregaYCertificacionR118">
    </asp:CalendarExtender>
    </td>
<td></td>
<td>
    <asp:TextBox ID="tx_Entrega_SemanasEstimadaInstalacion" runat="server">0</asp:TextBox>
    </td>
<td></td>
<td>
    &nbsp;</td>
<td></td>
<td>
    &nbsp;</td>
<td></td>

</tr>
</table>
<table>
<tr>
<td class="style2"></td>
<td>
    <asp:Label ID="Label40" runat="server" Text="Instalador :" Font-Bold="True"></asp:Label>
</td>
</tr>
</table>

<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label41" runat="server" Text="Contrato de Instalacion" 
        Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label42" runat="server" Text="Tecnico Asignado" 
        Font-Size="X-Small"></asp:Label></td>
<td></td>
<td>
    <asp:Label ID="Label43" runat="server" 
        Text="Semanas Acumuladas por Tecnico Asignado" Font-Size="X-Small"></asp:Label></td>
<td></td>
<td></td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:TextBox ID="tx_Instalador_ContratoInstalador" runat="server"></asp:TextBox></td>
<td></td>
<td>
    <asp:TextBox ID="tx_Instalador_TecnicoAsignado" runat="server"></asp:TextBox></td>
<td></td>
<td>
    <asp:TextBox ID="tx_Instalador_SemanasAcumuladas" runat="server">0</asp:TextBox></td>
<td>
    <asp:Button ID="bt_Actualizar" runat="server" onclick="bt_Actualizar_Click" 
        Text="Actualizar" Width="148px" />
    </td>
    <td></td>
    <td>
        <asp:Button ID="bt_Limpiar" runat="server" Text="Limpiar" 
            onclick="bt_Limpiar_Click" Width="100px" />
    </td>

</tr>
</table>

</div>



<div class = "botonLink">
    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Excel</asp:LinkButton>
</div>



<div class = "GSI3" >
    <asp:GridView ID="gv_SeguiInstalacion" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
        onselectedindexchanged="gv_SeguiInstalacion_SelectedIndexChanged">
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
<div class = "GSI5" ></div>


    </div>









</asp:Content>
