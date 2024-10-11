<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CallCenter_AreaCotiRepuesto.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CallCenter_AreaCotiRepuesto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menuIzquierdo" Src="MenuIzquierdo.ascx" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="../Styles/Style_CallCenter.css" rel="stylesheet" type="text/css" />

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
        
        
        .style3
        {
            width: 10px;
        }
        .style4
        {
            width: 90px;
        }
        .style5
        {
            width: 11px;
        }
        .style8
        {
            width: 203px;
        }
        .style9
        {
            width: 177px;
        }
        .style10
        {
            width: 419px;
        }
        .style11
        {
            width: 162px;
        }
        .style12
        {
            width: 67px;
        }
        .style14
        {
            width: 167px;
        }
        .style15
        {
            width: 109px;
        }
        .style16
        {
            width: 176px;
        }
        </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">    
    </asp:ScriptManager>
  
  <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>


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
    <tr><td style="height:500px;"></td></tr>
    <tr><td style="height:500px;"></td></tr>
    
    </table>
        
</td>

<td>
    <table>
    <tr>
    <td>
                            <div class = "Centrar">
                            <table style=" margin:auto;">
                            <tr>
                            <td>
                            <div class = "titulo"><h3>
                                <asp:Label ID="lb_eventos" runat="server" Text="Eventos"></asp:Label></h3></div>
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
                                    Width="130px" onselectedindexchanged="dd_baseDeDatos_SelectedIndexChanged" 
                                    AutoPostBack="True">
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
                                      <asp:ListItem>Asuncion-Paraguay</asp:ListItem>
                                      <asp:ListItem>Prueba</asp:ListItem>
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
                            <td></td>
                            <td>
                                <asp:Label ID="Label45" runat="server" Font-Size="Small" Text="Tiket:"></asp:Label>
                                </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="tx_tiket" runat="server" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td></td>
                            <td>&nbsp;</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            </tr>


                            <tr>
                            <td class="style3"></td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Une :" Font-Size="Small"></asp:Label>
                                </td>
                            <td></td>
    
                            <td class="style4">
                                <asp:TextBox ID="tx_BaseDeDatos" runat="server" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Edificio :" Font-Size="Small"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="tx_nombreEdificioBusqueda" runat="server" Width="200px" 
                                    Font-Size="Small"></asp:TextBox></td>
                                <asp:AutoCompleteExtender ID="tx_edificio_AutoCompleteExtender" runat="server" 
                                            TargetControlID="tx_nombreEdificioBusqueda"
                                            CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaProyectos5" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                    
                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                                            >
                                        </asp:AutoCompleteExtender>
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
                                <asp:TextBox ID="tx_SemanaBusqueda" runat="server" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td>
                                <asp:Label ID="Label36" runat="server" Text="LLamada :" Font-Size="Small"></asp:Label>
                                </td>
                            <td>
                                <asp:DropDownList ID="dd_tipoEvento1" runat="server" 
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
                                <asp:TextBox ID="tx_desdeBusqueda" runat="server" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="tx_desdeBusqueda_CalendarExtender" runat="server" 
                                    TargetControlID="tx_desdeBusqueda">
                                </asp:CalendarExtender>
                                </td>
                            <td>
                                <asp:Label ID="Label39" runat="server" Font-Size="Small" Text="Hasta :"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="tx_hastaBusqueda" runat="server" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="tx_hastaBusqueda_CalendarExtender" runat="server" 
                                    TargetControlID="tx_hastaBusqueda">
                                </asp:CalendarExtender>
                                </td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="dd_evento" runat="server" Height="25px" Width="100px">
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
                                <asp:GridView ID="gv_tablaEventosAreaCotiRepuesto" runat="server" BackColor="#CCCCCC" 
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
                                    <SelectedRowStyle BackColor="#33CC33" Font-Bold="True" ForeColor="White" 
                                        BorderColor="Black" BorderStyle="Double" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="Gray" />
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
                                <asp:Label ID="Label43" runat="server" Text="Cantidad de Eventos:"></asp:Label>
                                <asp:TextBox ID="tx_cantidadEventos" runat="server"></asp:TextBox>
                            </td>
                            </tr>



                            <tr>
                            <td>
                            <div class="cc0">
                            <table>
                            <tr>
                            <td ></td>
                            <td >
                            <asp:Label ID="Label2" runat="server" Text="Ticket :" Font-Size="Small"></asp:Label></td>
                            <td  >
                            <asp:Label ID="lb_numeroEvento" runat="server" Text="0"></asp:Label></td>
                            <td class="style5" ></td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Regional :" Font-Size="Small"></asp:Label></td>
                            <td >
                                <asp:TextBox ID="tx_Regional" runat="server" Width="150px" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td class="style5" ></td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="LLamada :" Font-Size="Small"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="tx_tipoEvento" runat="server" Font-Size="Small" Width="150px"></asp:TextBox>
                            </td>
                            <td></td>
                            </tr>
                            <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Nro Semana : " Font-Size="Small"></asp:Label></td>
                            <td class="style3">
                                <asp:Label ID="lb_numeroSemana" runat="server" Text="0"></asp:Label></td>
                            <td class="style5"></td>
                            <td>
                                <asp:Label ID="Label25" runat="server" Font-Size="Small" Text="Fecha :"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="tx_fechaEvento" runat="server" Width="150px" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="tx_fechaEvento_CalendarExtender" runat="server" 
                                    TargetControlID="tx_fechaEvento">
                                </asp:CalendarExtender>
                                </td>
                            <td class="style5"></td>
                            <td>
                                <asp:Label ID="Label26" runat="server" Font-Size="Small" Text="Hora :"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="tx_horaEvento" runat="server" Width="150px" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td></td>
                            </tr>
                            </table>
                        </div>

                            </td>
                            </tr>


                            <tr>
                            <td>
                            <div class="llamada1">
                        <table>
                        <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="Label27" runat="server" Font-Size="Small" Text="Cliente:"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="tx_Cliente" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
                            </td>
                        <td></td>
                        <td>
                            <asp:Label ID="Label41" runat="server" Text="Telefono :" Font-Size="Small"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="tx_telefono" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
                            </td>
                        <td></td>
                        </tr>


                        <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Edificio:" Font-Size="Small"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="tx_edificio" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
                            </td>
                        <td></td>
                        <td>
                            <asp:Label ID="Label42" runat="server" Text="Celular :
                            " Font-Size="Small"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="tx_celular" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
                            </td>
                        <td></td>
                        </tr>

                        <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Direccion:" Font-Size="Small"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="tx_Direccion" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
                            </td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="CheckBox1_AscensorParado" runat="server" Text="Ascensor Parado" 
                                Font-Size="Small" />
                            </td>
                        <td></td>
                        </tr>

                        <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Ascensores:" Font-Size="Small"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="tx_Ascensores" runat="server" Width="200px" 
                                Font-Size="Small"></asp:TextBox>
                            </td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="CheckBox2_PersonasAtrapadas" runat="server" Font-Size="Small" 
                                Text="Personas Atrapadas" />
                            </td>
                        <td></td>
                        </tr>
                                          
                        </table>



                        <table>
                        <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Solicitud de Servicio o atencion :" 
                                Font-Size="Small"></asp:Label></td>
                            <td></td>
                        <td>
                            <asp:TextBox ID="tx_observacion"  TextMode = "MultiLine" runat="server" Height="80px" 
                                Width="400px" Font-Size="Small"></asp:TextBox></td>
                        <td></td>
                        </tr>
                        </table> 
        



                        </div>

                            </td>
                            </tr>
    

                            <tr>
                            <td>
                            <div class = "AsignarSupervisor">
                                <asp:Label ID="Label29" runat="server" Text="Asignar Supervisor :"></asp:Label>
                                <asp:Label ID="tx_codDetalleTecnico" runat="server"></asp:Label>
                            <table>
                            <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="Label30" runat="server" Text="Supervisor :" Font-Size="Small"></asp:Label>
                                </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="tx_SupervisorAsignado" runat="server" Width="300px" 
                                    Font-Size="Small"></asp:TextBox>        
                                <asp:AutoCompleteExtender ID="tx_SupervisorAsignado_AutoCompleteExtender" 
                                    runat="server" TargetControlID="tx_SupervisorAsignado"
                                    ServiceMethod="GetlistaResponsable2" MinimumPrefixLength="1"
                                    UseContextKey="True"
                                    CompletionListCssClass="CompletionList" 
                                    CompletionListItemCssClass="CompletionlistItem" 
                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                                    >
                                </asp:AutoCompleteExtender>
        
                                </td>
                            <td></td>
                            <td> 
                             </td>
                                <td>
                                <asp:Button ID="bt_AsignarSupervisor" runat="server" Text="Asignar" 
                                        onclick="bt_AsignarSupervisor_Click" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="Label31" runat="server" Text="Fecha :" Font-Size="Small"></asp:Label>
                                </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="tx_fechaSupervisorAsignado" runat="server" Font-Size="Small" 
                                    Width="150px"></asp:TextBox>
                                <asp:CalendarExtender ID="tx_fechaSupervisorAsignado_CalendarExtender" 
                                    runat="server" TargetControlID="tx_fechaSupervisorAsignado">
                                </asp:CalendarExtender>
                                </td>
                            <td></td>
                            <td>
                                <asp:Label ID="Label32" runat="server" Text="Hora :" Font-Size="Small"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tx_horaSupervisorAsignado" runat="server" Font-Size="Small" 
                                        Width="150px"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            </table>
                            </div>
    
                            </td>
                            </tr>


                            <tr>
                            <td>    
                            <div class="AsignarTecnico">
                                <asp:Label ID="Label8" runat="server" Text="Asignar Tecnico :"></asp:Label>
                            <table>
                            <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="Label28" runat="server" Font-Size="Small" Text="Tecnico :"></asp:Label>
                                </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="tx_tecnicoAsignado" runat="server" Width="300px" 
                                    Font-Size="Small"></asp:TextBox>       
                                <asp:AutoCompleteExtender ID="tx_tecnicoAsignado_AutoCompleteExtender" 
                                    runat="server" TargetControlID="tx_tecnicoAsignado"
                                    ServiceMethod="GetlistaResponsable2" MinimumPrefixLength="1"
                                    UseContextKey="True"
                                    CompletionListCssClass="CompletionList" 
                                    CompletionListItemCssClass="CompletionlistItem" 
                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                                    >
                                </asp:AutoCompleteExtender>
                                </td>
                            <td>
        
                            </td>
                            <td>
                                <asp:Button ID="bt_AsignarTecnico" runat="server" 
                                    onclick="bt_AsignarTecnico_Click" Text="Asignar" />
                                </td>
                            </tr>    
                            </table>
                            </div>    
                            </td>
                            </tr>

   
    
                            <tr>
                            <td>
                            <div class = "e3">
                            <table>
                            <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Tecnico :" 
                                    Font-Size="Small"></asp:Label></td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="dd_tecnicosAsignados" AutoPostBack="True" runat="server" 
                                    Width="300px" 
                                    onselectedindexchanged="dd_tecnicosAsignados_SelectedIndexChanged">
                                </asp:DropDownList>
                                </td>
                            <td></td>
                            </tr>
                            </table>
                            <table>
                            <tr>
                            <td></td>
                            <td class="style9">
                                <asp:Label ID="Label44" runat="server" Text="Nro. Boleta :" Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style11">
                                <asp:TextBox ID="tx_nroboleta" runat="server" Font-Size="Small" Width="150px"></asp:TextBox>
                                </td>
                            <td>
                                <asp:CheckBox ID="cbx_trabajoprogramado" runat="server" Font-Size="Small" 
                                    Text="trab. prog." />
                                </td>
                            </tr>

                            <tr>
                            <td></td>
                            <td class="style9">
                                <asp:Label ID="Label34" runat="server" Font-Size="Small" 
                                    Text="Hora de Asignacion :"></asp:Label>
                                </td>
                            <td class="style11">
                                <asp:TextBox ID="tx_HoraTecnicoAsignacion" runat="server" Width="150px" 
                                    Font-Size="Small"></asp:TextBox>
                                </td>
                            <td>
                                <asp:Label ID="Label33" runat="server" Font-Size="Small" 
                                    Text="Fecha de Asignacion :"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="tx_FechaTecnicoAsignacion" runat="server" Width="150px" 
                                    Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="tx_FechaTecnicoAsignacion_CalendarExtender" 
                                    runat="server" TargetControlID="tx_FechaTecnicoAsignacion">
                                </asp:CalendarExtender>
                                </td>
                            </tr>

                            <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="Label49" runat="server" Font-Size="Small" Text="Hora Reporte :"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="tx_horaReporte" runat="server" Width="150px" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td>
                                <asp:Label ID="Label50" runat="server" Font-Size="Small" Text="Fecha Reporte :"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="tx_fechaReporte" runat="server" Width="150px" 
                                    Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="tx_fechaReporte_CalendarExtender" runat="server" 
                                    TargetControlID="tx_fechaReporte">
                                </asp:CalendarExtender>
                                </td>
                            </tr>

                            <tr>
                            <td></td>
                            <td class="style9">
                                <asp:Label ID="Label13" runat="server" Text="Hora LLegada al Edificio :" 
                                    Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style11">
                                <asp:TextBox ID="tx_horallegadaAlEdificioTecnico" runat="server" Width="150px" 
                                    Font-Size="Small"></asp:TextBox>
                                </td>
                            <td>
                                <asp:Label ID="Label47" runat="server" Font-Size="Small" 
                                    Text="Fecha LLegada al Edificio :"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="tx_fechahorallegada" runat="server" Width="150px" 
                                    Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="tx_fechahorallegada_CalendarExtender" runat="server" 
                                    TargetControlID="tx_fechahorallegada">
                                </asp:CalendarExtender>
                                </td>
                            </tr>

                            <tr>
                            <td></td>
                            <td class="style9">
                                <asp:Label ID="Label14" runat="server" Text="Hora Salida del Edificio :" 
                                    Font-Size="Small"></asp:Label></td>
                            <td class="style11">
                                <asp:TextBox ID="tx_horaSalidadelEdificioTecnico" runat="server" Width="150px" 
                                    Font-Size="Small"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="Label48" runat="server" Font-Size="Small" 
                                    Text="Fecha Salida al Edificio :"></asp:Label>
                                </td>
                                <td>
                                
                                <asp:TextBox ID="tx_fechahorasalida" runat="server" Width="150px" Font-Size="Small"></asp:TextBox>
                                
                                    <asp:CalendarExtender ID="tx_fechahorasalida_CalendarExtender" runat="server" 
                                        TargetControlID="tx_fechahorasalida">
                                    </asp:CalendarExtender>
                                
                                </td>
                            </tr>
                            <tr>
                            <td></td>
                            <td class="style9">
                                <asp:Label ID="Label15" runat="server" 
                                    Text="Estado del Equipo en la LLegada y Salida :" Font-Size="Small"></asp:Label></td>
                            <td class="style11">
                                <asp:DropDownList ID="dd_EstadoLLegadaSalidaTecnico" runat="server" 
                                    Height="25px" Width="130px">
                                    <asp:ListItem>FF</asp:ListItem>
                                    <asp:ListItem>FP</asp:ListItem>
                                    <asp:ListItem>PF</asp:ListItem>
                                    <asp:ListItem>PP</asp:ListItem>
                                </asp:DropDownList>
                                </td>
                            <td>
                                <asp:Label ID="Label46" runat="server" Font-Size="Small" 
                                    Text="Ascensor Reparado :"></asp:Label>
                                </td>
                                <td>
                                <asp:TextBox ID="tx_ascensorReparado" runat="server" Width="150px" 
                                        Font-Size="Small"></asp:TextBox>
                                </td>
                            </tr>


                          
                            
                            </table>

                            <table>
                            
                            <tr>
                            <td></td>
                            <td class="style8">
                                <asp:Label ID="Label16" runat="server" Text="Observacion :" Font-Size="Small"></asp:Label></td>
                            <td class="style10">
                                <asp:TextBox ID="tx_observacionTecnico"  TextMode = "MultiLine" runat="server" 
                                    Height="70px" Width="400px" Font-Size="Small"></asp:TextBox></td>
                            <td></td>
                            </tr>

                            <tr>
                            <td></td>
                            <td class="style8">
                                <asp:Button ID="bt_datosTecnicos" runat="server" 
                                    onclick="bt_datosTecnicos_Click" Text="Guardar Datos Tecnico" />
                                </td>
                            <td class="style10"></td>
                            <td></td>
                            </tr>
                            </table>

    
                            </div>
                            </td>
                            </tr>

   
                            <tr>
                            <td>
                                
                            <div class = "e4RIN">
                            <table>
                            <tr>
                            <td></td>
                            <td class="style14">
                                <asp:Label ID="Label17" runat="server" Text="Observacion Evento :" 
                                    Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style8">
                                <asp:TextBox ID="tx_observacionEvento" runat="server" Height="80px" 
                                    style="margin-left: 0px" TextMode="MultiLine" Width="450px" 
                                    Font-Size="Small"></asp:TextBox>
                                </td>
                            <td></td>
                           
                            </tr>
                            </table>
                            <table>
                            <tr>
                            <td></td>
                            <td class="style15">
                                <asp:Label ID="Label18" runat="server" Text="Defecto Constatado:" 
                                    Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style8">
                                <asp:TextBox ID="tx_defectoConstatadoEvento" runat="server" Width="200px" 
                                    Enabled="False" Font-Size="Small" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            <td></td>
                            <td class="style12">
                                <asp:Label ID="Label19" runat="server" 
                                    Text="Observacion Necesidad de Repuesto:" Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style6">
                                <asp:TextBox ID="tx_observacionNecesidadRepuestoEvento" runat="server" 
                                    Width="200px" Enabled="False" Font-Size="Small" Height="80px" 
                                    TextMode="MultiLine"></asp:TextBox>
                                </td>
                            <td></td>
                            </tr>
                            </table>
                            <table>
                            
                            <tr>
                            <td></td>
                            <td class="style16">
                                <asp:Label ID="Label20" runat="server" Text="Solicitud de Repuesto:" 
                                    Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style8">
                                <asp:TextBox ID="tx_solicitudRepuestoEvento" runat="server" Width="200px" 
                                    Enabled="False" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td></td>
                            <td class="style12">
                                <asp:Label ID="Label51" runat="server" Font-Size="Small" Text="Prioridad:"></asp:Label>
                                </td>
                            <td>
                                <asp:DropDownList ID="dd_prioridad" runat="server" Width="120px">
                                </asp:DropDownList>
                                </td>
                            <td></td>
                            </tr>
                            <tr>
                            <td></td>
                            <td class="style16">
                                <asp:Label ID="Label21" runat="server" Text="Envio de Proforma:" 
                                    Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style8">
                                <asp:TextBox ID="tx_envioProformaEvento" runat="server" Width="200px" 
                                    Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="tx_envioProformaEvento_CalendarExtender" 
                                    runat="server" TargetControlID="tx_envioProformaEvento">
                                </asp:CalendarExtender>
                                </td>
                            <td></td>
                            <td>
                                <asp:Label ID="Label24" runat="server" Text="Estado:" Font-Size="Small"></asp:Label>
                                </td>
                            <td>
                                <asp:DropDownList ID="dd_estadoEvento" runat="server" Width="120px">
                                    <asp:ListItem>Abierto</asp:ListItem>
                                    <asp:ListItem>Cerrado</asp:ListItem>
                                </asp:DropDownList>
                                </td>
                            <td></td>
                            </tr>
                            <tr>
                            <td></td>
                            <td class="style16">
                                <asp:Label ID="Label22" runat="server" Text="Aceptacion Proforma:" 
                                    Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style8">
                                <asp:TextBox ID="tx_AceptacionProformaEvento" runat="server" Width="200px" 
                                    Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="tx_AceptacionProformaEvento_CalendarExtender" 
                                    runat="server" TargetControlID="tx_AceptacionProformaEvento">
                                </asp:CalendarExtender>
                                </td>
                            <td></td>
                            <td></td>
                            <td>
                            <asp:CheckBox ID="ckb_cambioRespuesto" runat="server" Font-Size="Small" 
                                    Text="Cambio Repuesto" Enabled="False" />
                                </td>
                            <td></td>
                            </tr>
                            <tr>
                            <td></td>
                            <td class="style16">
                                <asp:Label ID="Label23" runat="server" Text="Verificacion de Cambio:" 
                                    Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style8">
                                <asp:TextBox ID="tx_verificacionCambioEvento" runat="server" Width="200px" 
                                    Enabled="False" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            </tr>

                            </table>   
    
                            </div>
                                                         
                            </td>
                            </tr>



                            <tr>
                            <td>
                            <div class = "cc2">
                                <asp:Button ID="bt_guardarDatosEvento" runat="server" Text="Guardar Datos Evento" 
                                    onclick="bt_guardarDatosEvento_Click" />
                                </div>    
                            </td>
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
