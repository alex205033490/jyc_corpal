<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CallCenter_EventoRIN.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CallCenter_EventoAntiguo" %>
<%@ Register TagPrefix="inmoInfo" TagName="menuIzquierdo" Src="MenuIzquierdo.ascx" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_CallCenter_Rin.css" rel="stylesheet" type="text/css" />
 <style type="text/css">
     
      .style3
        {
            width: 10px;
        }
        .style4
        {
            width: 90px;
        }
        .style6
     {
         width: 174px;
     }
        .style5
        {
            width: 11px;
        }
        .style7
     {
         height: 82px;
     }
        .style8
     {
         width: 223px;
     }
        </style>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>
   <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

<table>
<tr>
<td>
    <table>
    <tr><td>
        <inmoInfo:menuIzquierdo ID="MenuIzquierdo1"  runat="server"/>
    </td></tr>  
    <tr><td style="height:500px;"></td></tr> 
    <tr><td style="height:500px;"></td></tr> 
    </table>
</td>

<td>
    <table>
    <tr><td>
                            <div class = "Centrar">
    
                            <table style=" margin:auto;">
                            <tr>
                            <td>
                            <div class = "titulo"><h3>
                                <asp:Label ID="lb_titulo" runat="server" Text="Eventos RIN"></asp:Label></h3></div>
                            </td>
                            </tr>

                            <tr>
                            <td>
                            <div class="ea1">    
                            <table>
                            <tr>
                            <td></td><td>
                                <asp:Label ID="Label43" runat="server" Font-Size="Small" Text="Tiket:"></asp:Label>
                                </td>
                            <td></td><td>
                                <asp:TextBox ID="tx_tiketRIN" runat="server" Font-Size="Small" Width="120px"></asp:TextBox>
                                </td>
                            <td></td><td></td>
                            <td></td><td></td><td></td>
                            </tr>

                            <tr>
                            <td class="style3"></td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Une:" Font-Size="Small"></asp:Label>
                                </td>
                            <td></td>
    
                            <td class="style4">
                                <asp:TextBox ID="tx_BaseDeDatos" runat="server" Font-Size="Small" 
                                    Width="120px"></asp:TextBox>
                                </td>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Edificio:" Font-Size="Small"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="tx_nombreEdificioBusqueda" runat="server" Width="250px" 
                                    Font-Size="Small"></asp:TextBox></td>
                            <td></td>
                            <td>
                                <asp:Button ID="bt_Buscar" runat="server" Text="Buscar" 
                                    onclick="bt_Buscar_Click" Height="25px" Width="100px" /></td>
                            <td></td>
                            </tr>

                            <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="Label37" runat="server" Text="Semana:" Font-Size="Small"></asp:Label>
                                </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="tx_SemanaBusqueda" runat="server" Font-Size="Small" 
                                    Width="120px"></asp:TextBox>
                                </td>
                            <td>
                                <asp:Label ID="Label36" runat="server" Text="LLamada:" Font-Size="Small"></asp:Label>
                                </td>
                            <td>
                                <asp:DropDownList ID="dd_tipoEvento1" runat="server" 
                                    Width="200px" Font-Size="Small">
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
                                <asp:Label ID="Label38" runat="server" Font-Size="Small" Text="desde:"></asp:Label>
                                </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="tx_desdeBusqueda" runat="server" Font-Size="Small" 
                                    Width="120px"></asp:TextBox>
                                <asp:CalendarExtender ID="tx_desdeBusqueda_CalendarExtender" runat="server" 
                                    TargetControlID="tx_desdeBusqueda">
                                </asp:CalendarExtender>
                                </td>
                            <td>
                                <asp:Label ID="Label39" runat="server" Font-Size="Small" Text="Hasta:"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="tx_hastaBusqueda" runat="server" Font-Size="Small" 
                                    Width="120px"></asp:TextBox>
                                <asp:CalendarExtender ID="tx_hastaBusqueda_CalendarExtender" runat="server" 
                                    TargetControlID="tx_hastaBusqueda">
                                </asp:CalendarExtender>
                                </td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="dd_evento" runat="server" Width="100px" Font-Size="Small">
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
                            <div class="eventosRin">
                                <asp:GridView ID="gv_datosEvento" runat="server" BackColor="#CCCCCC" 
                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
                                    CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
                                    onselectedindexchanged="gv_datosEvento_SelectedIndexChanged">
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                    <RowStyle BackColor="White" />
                                    <SelectedRowStyle BackColor="#009900" Font-Bold="True" ForeColor="White" 
                                        BorderStyle="Double" />
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
                                <asp:Label ID="Label8" runat="server" Text="Cant. Eventos:" Font-Size="Small"></asp:Label>
                                <asp:TextBox ID="tx_cantEventos" runat="server" Font-Size="X-Small"></asp:TextBox>
                            </td>                                                        
                            </tr>


                            <tr>
                            <td class="style7">
                            <div class="cc0_Rin">
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
                                <asp:TextBox ID="tx_Regional" runat="server" Width="120px" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td class="style5" ></td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="LLamada :" Font-Size="Small"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="tx_tipoEvento" runat="server" Font-Size="Small" 
                                    Width="120px"></asp:TextBox>
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
                                <asp:TextBox ID="tx_fechaEvento" runat="server" Width="120px" 
                                    Font-Size="Small"></asp:TextBox>
                                       </td>
                            <td class="style5"></td>
                            <td>
                                <asp:Label ID="Label26" runat="server" Font-Size="Small" Text="Hora :"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="tx_horaEvento" runat="server" Width="120px" 
                                    Font-Size="Small"></asp:TextBox>
                                </td>
                            <td></td>
                            </tr>
                            </table>
                        </div>

                            </td>
                            </tr>


                            <tr>
                            <td>
                        <div class="llamadaRIN">
                        <table>
                        <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="Label27" runat="server" Font-Size="Small" Text="Cliente :"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="tx_Cliente" runat="server" Width="250px" Font-Size="Small"></asp:TextBox>
                            </td>
                        <td></td>
                        <td>
                            <asp:Label ID="Label41" runat="server" Font-Size="Small" Text="Telefono:"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="tx_telefono" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
                            </td>
                        <td></td>
                        </tr>


                        <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Edificio :" Font-Size="Small"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="tx_edificio" runat="server" Width="250px" Font-Size="Small"></asp:TextBox>
                            </td>
                        <td></td>
                        <td>
                            <asp:Label ID="Label42" runat="server" Font-Size="Small" Text="Celular:"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="tx_celular" runat="server" Width="200px" Font-Size="Small"></asp:TextBox>
                            </td>
                        <td></td>
                        </tr>

                        <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Direccion :" Font-Size="Small"></asp:Label>
                            </td>
                        <td>
                            <asp:TextBox ID="tx_Direccion" runat="server" Width="250px" Font-Size="Small"></asp:TextBox>
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
                            <asp:Label ID="Label6" runat="server" Text="Ascensores :" Font-Size="Small"></asp:Label>
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
                            <asp:TextBox ID="tx_observacion"  TextMode = "MultiLine" runat="server" Height="76px" 
                                Width="380px" Font-Size="Small"></asp:TextBox></td>
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
                            <td class="style6">
                                <asp:Label ID="Label20" runat="server" Text="Solicitud de Repuesto :" 
                                    Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style8">
                                <asp:TextBox ID="tx_solicitudRepuestoEvento" runat="server" Width="200px" 
                                    Enabled="False" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td></td>
                            <td>
                                <asp:Label ID="Label44" runat="server" Font-Size="Small" Text="Prioridad:"></asp:Label>
                                </td>
                            <td>
                                <asp:DropDownList ID="dd_prioridad" runat="server" Width="120px">
                                </asp:DropDownList>
                                </td>
                            <td></td>
                            </tr>
                            <tr>
                            <td></td>
                            <td class="style6">
                                <asp:Label ID="Label21" runat="server" Text="Envio de Proforma :" 
                                    Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style8">
                                <asp:TextBox ID="tx_envioProformaEvento" runat="server" Width="200px" 
                                    Enabled="False" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td></td>
                            <td>
                                <asp:Label ID="Label24" runat="server" Text="Estado:" Font-Size="Small"></asp:Label>
                                </td>
                            <td>
                                <asp:DropDownList ID="dd_estadoEvento" runat="server" Height="25px" Width="120px">
                                    <asp:ListItem>Abierto</asp:ListItem>
                                    <asp:ListItem>Cerrado</asp:ListItem>
                                </asp:DropDownList>
                                </td>
                            <td></td>
                            </tr>
                            <tr>
                            <td></td>
                            <td class="style6">
                                <asp:Label ID="Label22" runat="server" Text="Aceptacion Proforma :" 
                                    Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style8">
                                <asp:TextBox ID="tx_AceptacionProformaEvento" runat="server" Width="200px" 
                                    Enabled="False" Font-Size="Small"></asp:TextBox>
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
                            <td class="style6">
                                <asp:Label ID="Label23" runat="server" Text="Verificacion de Cambio :" 
                                    Font-Size="Small"></asp:Label>
                                </td>
                            <td class="style8">
                                <asp:TextBox ID="tx_verificacionCambioEvento" runat="server" Width="200px" 
                                    Font-Size="Small"></asp:TextBox>
                                </td>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:CheckBox ID="Cbx_solicitudCambioRepuesto" runat="server" Font-Size="Small" 
                                    Text="Solicitud de Repuesto" />
                                </td>
                            <td></td>
                            </tr>
                            </table> 
                            <table>
                            <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="Label17" runat="server" Text="Observacion RIN:" 
                                    Font-Size="Small"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="tx_observacionEvento" runat="server" Height="85px" 
                                    style="margin-left: 0px" TextMode="MultiLine" Width="535px" 
                                    Font-Size="Small"></asp:TextBox>
                                </td>
                            <td></td>
                            </tr>                          
                            </table>  
                            <table>
                            <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="Label18" runat="server" Text="Defecto Constatado:" 
                                    Font-Size="X-Small"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="tx_defectoConstatadoEvento" runat="server" Width="250px" 
                                    Height="85px" TextMode="MultiLine" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td>
                                <asp:Label ID="Label19" runat="server" 
                                    Text="Observacion Necesidad de Repuesto:" Font-Size="X-Small"></asp:Label>
                                </td>
                            <td>
                                <asp:TextBox ID="tx_observacionNecesidadRepuestoEvento" runat="server" 
                                    Width="250px" Height="85px" TextMode="MultiLine" Font-Size="Small"></asp:TextBox>
                                </td>
                            <td></td>
                            
                            </tr>
                            </table>
    
                            </div>
                            </td>
                            </tr>



                            <tr>
                            <td>
                            <div class = "cc2">
                                <asp:Button ID="bt_guardarBoton" runat="server" Text="Guardar Datos" 
                                    onclick="bt_guardarBoton_Click" />
                                </div>    
                            </td>
                            </tr>

                            <tr>
                            <td>
                            <h2>Repuestos Solicitados</h2>
                            <div class="repuestoVista">
                            
                                <asp:GridView ID="gv_repuestoSolicitado" runat="server" BackColor="White" 
                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                                    Font-Size="X-Small" ForeColor="Black" GridLines="Vertical">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
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

                            </table>
       
                        </div>    
    </td></tr>    
    </table>
</td>
</tr>
</table>


</asp:Content>
