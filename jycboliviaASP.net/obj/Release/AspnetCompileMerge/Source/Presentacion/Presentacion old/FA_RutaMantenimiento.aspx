<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_RutaMantenimiento.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_RutaMantenimiento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_RutasMantenimiento.css" rel="stylesheet" type="text/css" />
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
        
        
         .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: auto;
    }
        
         .style1
         {
             width: 6px;
         }
         .style2
         {
             width: 69px;
         }
        
         .style3
         {
             width: 57px;
         }
        
         </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
    </div>



<div class="RM_central" >
    
<table style="margin: 0 auto;">
<tr>
<td>
<div class="RM_Grutas">
<br />
<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label1" runat="server" Text="Nro. Ruta:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_numeroRuta" runat="server" Width="47px"></asp:TextBox>
    </td>
<td>
    <asp:Button ID="bt_buscarRuta" runat="server" Text="Buscar" 
        onclick="Button1_Click" />
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Detalle:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_detalleRuta" runat="server" Height="66px" Width="600px" 
        TextMode="MultiLine"></asp:TextBox>
    </td>
<td></td>
<td></td>

</tr>
</table>

<table>
<tr>
<td></td>
<td>
    <asp:Button ID="bt_insertarRuta" runat="server" onclick="bt_insertarRuta_Click" 
        Text="Insertar" />
    </td>
<td></td>
<td>
    <asp:Button ID="bt_modificarRuta" runat="server" Text="Modificar" 
        onclick="bt_modificarRuta_Click" />
    </td>
<td></td>
<td class="style2">
    <asp:Button ID="bt_eliminarRuta" runat="server" Text="Eliminar" 
        onclick="bt_eliminarRuta_Click" />
    </td>
<td class="style1"></td>
<td class="style1">
    <asp:Button ID="bt_generar" runat="server" onclick="bt_generar_Click" 
        Text="Copiar Rutas Anterior" />
    </td>
<td class="style1"></td>
<td>
    <asp:Button ID="bt_eliminarTodaRuta1" runat="server" 
        onclick="bt_eliminarTodaRuta1_Click" Text="Eliminar Toda la Ruta" />
    </td>
</tr>
</table>
</div>
</td>
</tr>

<tr>
<td>
<div class="RM_rutas">

    <asp:GridView ID="gv_rutas" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        onselectedindexchanged="gv_rutas_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#33CC33" Font-Bold="True" ForeColor="White" />
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
    <asp:Label ID="Label7" runat="server" Text="Cantidad:"></asp:Label>
    <asp:Label ID="lb_ruta" runat="server" Text="0"></asp:Label>
</td>
</tr>


<tr>
<td>
<div class="RM_buscarEdificio">
<table>
<tr>
<td></td>
<td>
    <asp:CheckBox ID="chbx_verfaltantesderuta" runat="server" 
        Text="Faltante de Ruta" Font-Size="XX-Small" />
    </td>
<td>
    &nbsp;</td>
<td>&nbsp;</td>
<td>
    &nbsp;</td>
<td>&nbsp;</td>
<td class="style3"></td>
<td>
    &nbsp;</td>
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
<div class="RM_AsigndorEdificioRutas">
<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Tienda:" Font-Size="Small"></asp:Label>
    <asp:TextBox ID="tx_edificioBuscar" runat="server" Width="200px"></asp:TextBox>
    <asp:AutoCompleteExtender ID="tx_edificioBuscar_AutoCompleteExtender" 
        runat="server" TargetControlID="tx_edificioBuscar"
        CompletionSetCount="12" 
        MinimumPrefixLength="1" ServiceMethod="GetlistaProyectos2" 
        UseContextKey="True"
        CompletionListCssClass="CompletionList" 
        CompletionListItemCssClass="CompletionlistItem" 
        CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
        >
    </asp:AutoCompleteExtender>
    <asp:Button ID="bt_buscarEdificios" runat="server" Text="Buscar" 
        onclick="bt_buscarEdificios_Click" />
    </td>
<td>&nbsp;</td>
<td></td>
<td></td>
<td>
    <asp:Label ID="Label5" runat="server" Text="Tiendas Asignadas:" 
        Font-Size="Small"></asp:Label>
    <asp:TextBox ID="tx_buscarEquiposAsignados" runat="server" Width="200px"></asp:TextBox>
    <asp:Button ID="bt_buscarEquiposAsignados" runat="server" Text="Buscar" 
        onclick="bt_buscarEquiposAsignados_Click" />
    </td>
<td></td>
</tr>

<tr>
<td></td>
<td>
 <div class="RM_equipos">
    <asp:GridView ID="gv_equipo" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>              
               
            <asp:CommandField ShowSelectButton="True" />
               
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#33CC33" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
 </div>
    </td>
<td></td>
<td>
    <table style="text-align:center;">
    <tr>
    <td>
    <asp:Button ID="bt_asignar" runat="server" Text="Agregar" />
        <asp:ModalPopupExtender ID="bt_asignar_ModalPopupExtender" runat="server" 
            TargetControlID="bt_asignar" PopupControlID="Panel1"            
            CancelControlID="bt_popupcancel"            
            BackgroundCssClass="modalBackground"
            popupdraghandlecontrolid="PopupHeader" 	drag="true"
            >
        </asp:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup"  style = "display:none">
            Ingresar Datos:<br />
            <table>
            <tr>            
            <td><asp:Label ID="Label12" runat="server" Text="Hora Ingreso:" Font-Size="Small"></asp:Label></td>
            <td><asp:TextBox ID="tx_popuhoraIngreso" runat="server" Font-Size="Small" Width="100px"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                    TargetControlID="tx_popuhoraIngreso" 
                    AcceptAMPM = "false"
                    MaskType="Time" 
                    Mask="99:99"
                    AutoComplete = "false" 
                                       
                ></asp:MaskedEditExtender>
            </td>
            <td></td>
            </tr>
            <tr>
            <td><asp:Label ID="Label13" runat="server" Text="Hora Salida:" Font-Size="Small" ></asp:Label></td>
            <td><asp:TextBox ID="tx_popuhorasalida" runat="server" Font-Size="Small" Width="100px" ></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                    TargetControlID="tx_popuhorasalida" 
                    AcceptAMPM = "false"
                    MaskType="Time" 
                    Mask="99:99"
                    AutoComplete = "false"                                        
                ></asp:MaskedEditExtender>
            </td>
            <td></td>            
            </tr>
            <tr>
            <td><asp:Label ID="Label14" runat="server" Text="Dia:" Font-Size="Small"></asp:Label></td>
            <td><asp:DropDownList ID="dd_diapopu" runat="server" Font-Size="Small">
                <asp:ListItem>Domingo</asp:ListItem>
                <asp:ListItem>Lunes</asp:ListItem>
                <asp:ListItem>Martes</asp:ListItem>
                <asp:ListItem>Miercoles</asp:ListItem>
                <asp:ListItem>Jueves</asp:ListItem>
                <asp:ListItem>Viernes</asp:ListItem>
                <asp:ListItem>Sabado</asp:ListItem>        
                </asp:DropDownList> </td>
            <td></td>
            </tr>
             <tr>
            <td>
                <asp:Label ID="Label15" runat="server" Text="CantVisita:"  Font-Size="Small"></asp:Label></td>
            <td>   
                <asp:TextBox ID="tx_popucantidadVistas" runat="server"  Font-Size="Small" Width="50px" >0</asp:TextBox>         </td>
            <td></td>
            </tr>
            </table>

            <table>
            <tr>
            <td></td>
            <td><asp:CheckBox ID="chx_s1" runat="server"  Text="Semana1"   Font-Size="Small"/></td>                       
            <td><asp:TextBox ID="tx_popuFechaSemana1" runat="server" Font-Size="Small" Width="100px"></asp:TextBox>
            <asp:CalendarExtender ID="tx_popuFechaSemana1_CalendarExtender" 
                                    runat="server" TargetControlID="tx_popuFechaSemana1">
              </asp:CalendarExtender>
            </td>
            <td><asp:Button ID="bt_popuCalcular" runat="server" Text="Calcular" 
                    Font-Size="X-Small" onclick="bt_popuCalcular_Click"/></td>
            <td></td>
            </tr>
            <tr>
            <td></td>
            <td><asp:CheckBox ID="chx_s2" runat="server"  Text="Semana2"   Font-Size="Small"/></td>                       
            <td><asp:TextBox ID="tx_popuFechaSemana2" runat="server" Font-Size="Small" Width="100px"></asp:TextBox>
            <asp:CalendarExtender ID="tx_popuFechaSemana2_CalendarExtender1" 
                                    runat="server" TargetControlID="tx_popuFechaSemana2">
              </asp:CalendarExtender>
            </td>
            <td></td>
            <td></td>
            </tr>
            <tr>
            <td></td>
            <td><asp:CheckBox ID="chx_s3" runat="server"  Text="Semana3"   Font-Size="Small"/></td>           
            <td><asp:TextBox ID="tx_popuFechaSemana3" runat="server" Font-Size="Small" Width="100px"></asp:TextBox>
            <asp:CalendarExtender ID="tx_popuFechaSemana3_CalendarExtender1" 
                                    runat="server" TargetControlID="tx_popuFechaSemana3">
              </asp:CalendarExtender>
            </td>
            <td></td>
            <td></td>
            </tr>
            <tr>
            <td></td>
            <td><asp:CheckBox ID="chx_s4" runat="server"  Text="Semana4"   Font-Size="Small"/></td>           
            <td><asp:TextBox ID="tx_popuFechaSemana4" runat="server" Font-Size="Small" Width="100px"></asp:TextBox>
            <asp:CalendarExtender ID="tx_popuFechaSemana4_CalendarExtender1" 
                                    runat="server" TargetControlID="tx_popuFechaSemana4">
              </asp:CalendarExtender>
            </td>
            <td></td>
            <td></td>
            </tr>
            <tr>
            <td></td>
            <td><asp:Label ID="Label20" runat="server"  Font-Size="Small" Text="Pasaje:"></asp:Label></td>
            <td>
                <asp:TextBox ID="tx_popuPasaje" runat="server"  Font-Size="Small" Width="100px" Text="0"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                    TargetControlID="tx_popuPasaje" 
                    AcceptNegative="Left" InputDirection="RightToLeft" 
                    MaskType="Number" 
                    Mask="99.99"
                    AutoComplete = "false" 
                >
                </asp:MaskedEditExtender>
            </td>
            <td></td>
            <td></td>
            <td></td>
            </tr>    
                 
            <tr>
            <td></td>
            <td><asp:Label ID="Label22" runat="server"  Font-Size="Small" Text="Ascensor:"></asp:Label></td>
            <td>
                <asp:TextBox ID="tx_popuAscensor" runat="server"  Font-Size="Small" Width="100px" ></asp:TextBox>
            </td>
            <td></td>
            <td></td>
            <td></td>
            </tr>
                                
            </table>

            <table>
            <tr>
            <td></td>
            <td><asp:Button ID="bt_popupok" runat="server" Text="Aceptar" 
                    onclick="bt_popupok_Click"  Font-Size="Small"/></td>
            <td><asp:Button ID="bt_popupcancel" runat="server" Text="cancel"  Font-Size="Small" /></td>
            <td></td>
            </tr>
            </table>
                        
        </asp:Panel>

    </td>
    </tr>
    <tr>
    <td>
    <asp:Button ID="bt_eliminar" runat="server" Text="Quitar" 
            onclick="bt_eliminar_Click" />
    </td>
    </tr>

    <tr>
    <td>
        <asp:Button ID="bt_modificarPopu" runat="server" Text="Modificar" />
        <asp:ModalPopupExtender ID="bt_modificarPopu_ModalPopupExtender" runat="server" 
            TargetControlID="bt_modificarPopu" PopupControlID="Panel3"            
            CancelControlID="bt_popupcancelModificar" 
            BackgroundCssClass="modalBackground"
            popupdraghandlecontrolid="PopupHeader" 	drag="true" >           
        </asp:ModalPopupExtender>

         <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup"  style = "display:none">
            Modificar Datos:<br />
            <table>
            <tr>            
            <td><asp:Label ID="Label8" runat="server" Text="Hora Ingreso:" Font-Size="Small"></asp:Label></td>
            <td><asp:TextBox ID="tx_popuHoraIngresoModificar" runat="server" Font-Size="Small" Width="100px"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                    TargetControlID="tx_popuHoraIngresoModificar" 
                    AcceptAMPM = "false"
                    MaskType="Time" 
                    Mask="99:99"
                    AutoComplete = "false" 
                                       
                ></asp:MaskedEditExtender>
            </td>
            <td></td>
            </tr>
            <tr>
            <td><asp:Label ID="Label9" runat="server" Text="Hora Salida:" Font-Size="Small" ></asp:Label></td>
            <td><asp:TextBox ID="tx_popuHoraSalidaModificar" runat="server" Font-Size="Small" Width="100px" ></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                    TargetControlID="tx_popuHoraSalidaModificar" 
                    AcceptAMPM = "false"
                    MaskType="Time" 
                    Mask="99:99"
                    AutoComplete = "false"                                      
                ></asp:MaskedEditExtender>
            </td>
            <td></td>            
            </tr>
            <tr>
            <td><asp:Label ID="Label10" runat="server" Text="Dia:" Font-Size="Small"></asp:Label></td>
            <td><asp:DropDownList ID="dd_popuDiamodificar" runat="server" Font-Size="Small">
                <asp:ListItem>Domingo</asp:ListItem>
                <asp:ListItem>Lunes</asp:ListItem>
                <asp:ListItem>Martes</asp:ListItem>
                <asp:ListItem>Miercoles</asp:ListItem>
                <asp:ListItem>Jueves</asp:ListItem>
                <asp:ListItem>Viernes</asp:ListItem>
                <asp:ListItem>Sabado</asp:ListItem>        
                </asp:DropDownList> </td>
            <td></td>
            </tr>
             <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="CantVisita:"  Font-Size="Small"></asp:Label></td>
            <td>   
                <asp:TextBox ID="tx_popuCantVisitaMoficar" runat="server"  Font-Size="Small" Width="50px" >0</asp:TextBox>         </td>
            <td></td>
            </tr>
            </table>

            <table>
            <tr>
            <td></td>
            <td><asp:CheckBox ID="chx_popuSemana1modificar" runat="server"  Text="Semana1"   Font-Size="Small"/></td>                       
            <td><asp:TextBox ID="tx_popuFechaSemana1modificar" runat="server" Font-Size="Small" Width="100px"></asp:TextBox>
             <asp:CalendarExtender ID="CalendarExtender1" 
                                    runat="server" TargetControlID="tx_popuFechaSemana1modificar">
              </asp:CalendarExtender>
            </td>
            <td><asp:Button ID="bt_popuCalcularMoficar" runat="server" Text="calcular" 
                    onclick="bt_popuCalcularMoficar_Click" Font-Size="X-Small" /></td>
            <td></td>
            </tr>
            <tr>
            <td></td>
            <td><asp:CheckBox ID="chx_popuSemana2modificar" runat="server"  Text="Semana2"   Font-Size="Small"/></td>                       
            <td><asp:TextBox ID="tx_popuFechaSemana2Modificar" runat="server" Font-Size="Small" Width="100px"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender2" 
                                    runat="server" TargetControlID="tx_popuFechaSemana2Modificar">
              </asp:CalendarExtender>
            </td>
            <td></td>
            <td></td>
            </tr>
            <tr>
            <td></td>
            <td><asp:CheckBox ID="chx_popuSemana3modificar" runat="server"  Text="Semana3"   Font-Size="Small"/></td>           
            <td><asp:TextBox ID="tx_popuFechaSemana3Modificar" runat="server" Font-Size="Small" Width="100px"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender3" 
                                    runat="server" TargetControlID="tx_popuFechaSemana3Modificar">
              </asp:CalendarExtender>
            </td>
            <td></td>
            <td></td>
            </tr>
            <tr>
            <td></td>
            <td><asp:CheckBox ID="chx_popuSemana4modificar" runat="server"  Text="Semana4"   Font-Size="Small"/></td>           
            <td><asp:TextBox ID="tx_popuFechaSemana4modificar" runat="server" Font-Size="Small" Width="100px"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender4" 
                                    runat="server" TargetControlID="tx_popuFechaSemana4modificar">
              </asp:CalendarExtender>
            </td>
            <td></td>
            <td></td>
            </tr>
            
            <tr>
            <td></td>
            <td>
                <asp:Label ID="Label21" runat="server" Font-Size="Small" Text="Pasaje:"></asp:Label></td>
            <td>
                <asp:TextBox ID="tx_popuModificarPasaje" runat="server" Text="0" Font-Size="Small" Width="100px"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                    TargetControlID="tx_popuModificarPasaje" 
                    AcceptNegative="Left" InputDirection="RightToLeft" 
                    MaskType="Number" 
                    Mask="99.99"
                    AutoComplete = "false" 
                >
                </asp:MaskedEditExtender>
            </td>
            <td></td>
            <td></td>
            <td></td>
            </tr>

             <tr>
            <td></td>
            <td><asp:Label ID="Label23" runat="server"  Font-Size="Small" Text="Ascensor:"></asp:Label></td>
            <td>
                <asp:TextBox ID="tx_popuModificarAscensor" runat="server"  Font-Size="Small" Width="100px" ></asp:TextBox>
            </td>
            <td></td>
            <td></td>
            <td></td>
            </tr>

            </table>

            
            <table>
            <tr>
            <td></td>
            <td><asp:Button ID="bt_popuok_modificar" runat="server" Text="Aceptar" 
                      Font-Size="Small" onclick="bt_popuok_modificar_Click1" /></td>
            <td><asp:Button ID="bt_popupcancelModificar" runat="server" Text="cancel"  Font-Size="Small" /></td>
            <td></td>
            </tr>
            </table>
                        
        </asp:Panel>

    </td>
    </tr>

    </table>    
    </td>
<td></td>
<td>
   <div class="RM_equiposAsignados">
    <asp:GridView ID="gv_equiposAsignados" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
           onselectedindexchanged="gv_equiposAsignados_SelectedIndexChanged1">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
         
            <asp:CommandField ShowSelectButton="True" />
         
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#33CC33" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
   </div>
    </td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:Label ID="Label17" runat="server" Text="Cantidad:"></asp:Label>
    <asp:Label ID="lb_equipos" runat="server" Text="0"></asp:Label>
    </td>
<td></td>
<td></td>
<td></td>
<td>
    <asp:Label ID="Label18" runat="server" Text="Cantidad:"></asp:Label>
    <asp:Label ID="lb_cantequipoAsignados" runat="server" Text="0"></asp:Label>
    </td>
<td></td>
</tr>
</table>
</div>
</td>
</tr>

<tr>
<td>
<div class ="RM_GTecnicos">
<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label6" runat="server" Text="Asignar:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_tecnico" runat="server" Width="250px"></asp:TextBox>
    <asp:AutoCompleteExtender ID="tx_tecnico_AutoCompleteExtender" runat="server" 
        TargetControlID="tx_tecnico"          
        CompletionSetCount="12" 
        MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2" 
        UseContextKey="True"
        CompletionListCssClass="CompletionList" 
        CompletionListItemCssClass="CompletionlistItem" 
        CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
        >
        
    </asp:AutoCompleteExtender>
    </td>
<td></td>
<td>    
    <asp:Label ID="Label16" runat="server" Text="Tipo:"></asp:Label>
    </td>
<td>
    <asp:DropDownList ID="dd_supervisorRuta" runat="server" Width="150px">
        <asp:ListItem>Tecnico Mantenimiento</asp:ListItem>
        <asp:ListItem>Supervisor</asp:ListItem>
        <asp:ListItem>Ejecutiva de cuenta</asp:ListItem>
        <asp:ListItem>AAI - Auxiliar Administrativo de Ingenieria</asp:ListItem>
        <asp:ListItem>Ayudante Tecnico</asp:ListItem>
        <asp:ListItem>Mecanico</asp:ListItem>
    </asp:DropDownList>
    </td>
    <td></td>
    <td>
    <asp:Button ID="bt_asignarTecnico" runat="server" Text="Asignar" 
        onclick="bt_asignarTecnico_Click" />
    </td>
    <td></td>
</tr>

</table>


</div>
</td>
</tr>

<tr>
<td>
<div class="RM_tecnicosAsignados">
    <asp:GridView ID="gv_tecnicosAsignados" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        onrowdeleting="gv_tecnicosAsignados_RowDeleting">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#33CC33" Font-Bold="True" ForeColor="White" />
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
    <asp:Label ID="Label19" runat="server" Text="Cantidad:"></asp:Label>
    <asp:Label ID="lb_canttecnicoAsignado" runat="server" Text="0"></asp:Label>
    </td>
</tr>

<tr>
<td>
<div class="RM_Gsuervisor">
<table>
<tr>
<td></td>
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

</table>

<table>
<tr>
<td></td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
<td></td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
<td>&nbsp;</td>
<td>
    &nbsp;</td>
<td></td>
</tr>
</table>
</div>
</td>
</tr>

<tr>
<td>
    &nbsp;</td>
</tr>


</table>

</div>

</asp:Content>
