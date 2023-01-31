<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FEquipo.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FEquipo" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../Styles/Style_GEdificio.css" rel="stylesheet" type="text/css" />
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
        
        .style1
        {
            height: 26px;
            width: 18px;
        }
        .style3
        {
            height: 26px;
            width: 111px;
        }
        .style4
        {
            width: 111px;
            height: 30px;
        }
        .style5
        {
            height: 30px;
            width: 18px;
        }
        .style13
        {
            height: 30px;
            width: 77px;
        }
        .style14
        {
            height: 26px;
            width: 77px;
        }
        .style15
        {
            width: 77px;
            height: 35px;
        }
        .style18
        {
            width: 18px;
            height: 29px;
        }
        .style20
        {
            width: 49px;
            height: 29px;
        }
        .style21
        {
            width: 43px;
            height: 29px;
        }
        .style22
        {
            width: 26px;
            height: 29px;
        }
        .style26
        {
            height: 30px;
            width: 10px;
        }
        .style27
        {
            height: 26px;
            width: 10px;
        }
        .style28
        {
            height: 35px;
            width: 10px;
        }
        .style29
        {
            width: 111px;
            height: 35px;
        }
        .style33
        {
            height: 30px;
            width: 4px;
        }
        .style34
        {
            height: 26px;
            width: 4px;
        }
        .style35
        {
            height: 35px;
            width: 4px;
        }
        .style36
        {
            height: 30px;
            width: 160px;
        }
        .style37
        {
            height: 26px;
            width: 160px;
        }
        .style38
        {
            height: 35px;
            width: 160px;
        }
        .style39
        {
            height: 26px;
            width: 187px;
        }
        .style41
        {
            height: 30px;
            width: 187px;
        }
        .style42
        {
            height: 30px;
            width: 103px;
        }
        .style43
        {
            height: 26px;
            width: 103px;
        }
        .style45
        {
            height: 30px;
            width: 91px;
        }
        .style46
        {
            height: 26px;
            width: 91px;
        }
        .style47
        {
            height: 35px;
        }
        .style48
        {
            height: 35px;
            width: 187px;
        }
        .style49
        {
            width: 187px;
        }
        .style50
        {
            width: 111px;
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
<div class="titulo">
<h3>GESTIONAR EQUIPO</h3>
</div>

<table>
<tr>
<td>
<div class ="GE1">
 <table border="0" 
        style="width: 836px; margin-left: 22px; height: 110px; margin-top: 9px;">
        <tr>
            <td class="style4">
                <asp:Label ID="Label2" runat="server" Text="Exbo :" 
                    Font-Size = "X-Small" Width="100px" ></asp:Label></td>
            <td class="style36">
                <asp:TextBox ID="txtExbo" runat="server" Width="145px" 
                    Font-Size="X-Small" />
                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                 CompletionSetCount="12" MinimumPrefixLength="1" ServiceMethod="getListaEquipo" 
            TargetControlID="txtExbo"  UseContextKey="True"
            CompletionListCssClass="CompletionList" 
            CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                >
                </asp:AutoCompleteExtender>
            </td>
            <td class="style26"  ></td>
            <td class="style41">
                <asp:Label ID="Label8" runat="server" Font-Size="X-Small" 
                    Text="Acta Provisional :" Width="130px"></asp:Label>
                 </td>
            <td class="style45">
                <asp:TextBox ID="txtFechaActaProvisional" runat="server" 
                    style="margin-bottom: 0px" Width="118px" 
                    Font-Size="X-Small"/>
                <asp:CalendarExtender ID="calendarFechaActaProvisional" runat="server" 
                    TargetControlID="txtFechaActaProvisional">
                </asp:CalendarExtender>   
                </td>
            <td class="style5">
                        </td>
                        <td class="style42">
                            
              <asp:Label ID="Label15" runat="server" Text="Equipo Entregado Segun Contrato :" 
                  Font-Size="X-Small"></asp:Label>
                
                 </td>
            <td class="style13">
              <asp:TextBox ID="tx_FechaEquipoEntregado" runat="server" Width="118px" 
                  Font-Size="X-Small"></asp:TextBox>
              <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                    TargetControlID="tx_FechaEquipoEntregado">
                </asp:CalendarExtender>
                 </td>
            <td class="style33">  
                </td>
        </tr>
        <tr>
            <td class="style3">
                <asp:Label ID="Label7" runat="server" Text="Edificio :" Font-Size="X-Small" 
                    Width="100px"></asp:Label>
            </td>
            <td class="style37">
                <asp:TextBox ID="tx_NombreProyecto" runat="server" Width="145px" 
                    Font-Size="X-Small"></asp:TextBox>                 
                <asp:AutoCompleteExtender ID="tx_NombreProyecto_AutoCompleteExtender" runat="server"  
                    TargetControlID="tx_NombreProyecto"
                    Enabled="True" BehaviorID="AutoCompleteEx" CompletionInterval="200"
                    ServicePath="" servicemethod="GetlistaProyectos"
                    minimumprefixlength="2"  DelimiterCharacters="" enablecaching="true"                     
                    completionsetcount="30" 
                    ShowOnlyCurrentWordInCompletionListItem="True" 
                    CompletionListCssClass="CompletionList" 
            CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem"
                    >
                </asp:AutoCompleteExtender>
                </td>
            <td class="style27">
                &nbsp;</td>
            <td class="style39" dir="ltr">
                <asp:Label ID="Label6" runat="server" Text="Acta Definitiva :" 
                    Font-Size= "X-Small" Width="130px"></asp:Label>
            </td>
            <td class="style46">
                <asp:TextBox ID="tx_FechaActaDefinitiva" runat="server" Width="118px" 
                    Font-Size="X-Small" />
                <asp:CalendarExtender ID="calendarFechaActaDefinitiva" runat="server" 
                    TargetControlID="tx_FechaActaDefinitiva">
                </asp:CalendarExtender>
            </td>
            <td class="style1">                    
                        </td>
            <td>
                            
                            <asp:Label ID="Label21" runat="server" Font-Size="X-Small" 
                                Text="Aprobacion de Plano :"></asp:Label>
                
              </td>
            <td class="style14">
                <asp:TextBox ID="tx_aprobacionPlano" runat="server" Font-Size="X-Small" 
                    Width="118px"></asp:TextBox>
                <asp:CalendarExtender ID="tx_aprobacionPlano_CalendarExtender" runat="server" 
                    TargetControlID="tx_aprobacionPlano">
                </asp:CalendarExtender>
              </td>
            <td class="style34">
                        &nbsp;</td>

        </tr>
        <tr>
            <td class="style3">
                
                                <asp:Label ID="Label5" runat="server" Font-Size="X-Small" 
                                    Text="Estado del Equipo" Width="100px"></asp:Label>
                            
                </td>
            <td class="style37">
                <asp:DropDownList ID="ddlEstadoEquipo" runat="server" Font-Size="Small" 
                     Width="145px">
                </asp:DropDownList>
            </td>
            <td class="style27"></td>
            <td class="style39">
                <asp:Label ID="Label3" runat="server" Text="Acta Tecnico : " Width="130px" 
                    Font-Size = "X-Small"></asp:Label>
          </td>
            <td class="style46">
                <asp:TextBox ID="txtFechaActaTecnica" runat="server" Width="118px" 
                    style="margin-bottom: 0px" Font-Size="X-Small"/>
                <asp:CalendarExtender ID="calendarFechaActaTecnica" runat="server" 
                    TargetControlID="txtFechaActaTecnica">
                </asp:CalendarExtender>
                        </td>
             <td class="style1">
                    
            </td>
             <td class="style43">
              <asp:Label ID="Label19" runat="server" Font-Size="X-Small" 
                  Text="Aprobacion Limite de Planos a Fabrica :"></asp:Label>
            </td>
             <td class="style14">
              <asp:TextBox ID="tx_fechalimiteAprobacionPlanosFabrica" runat="server" 
                    Width="118px" Font-Size="X-Small"></asp:TextBox>
              <asp:CalendarExtender ID="tx_fechalimiteAprobacionPlanosFabrica_CalendarExtender" runat="server" 
                  TargetControlID="tx_fechalimiteAprobacionPlanosFabrica">
              </asp:CalendarExtender>
            </td>
             <td class="style34"></td>
             
        </tr>
         <tr>
                            <td class="style29">
              <asp:Label ID="Label16" runat="server" Text="Tipo Equipo :" Font-Size="X-Small"></asp:Label>
            </td>
             <td class="style38">
                 
              <asp:DropDownList ID="dd_tipoEquipo" runat="server" Width="145px">
              </asp:DropDownList>
                 
                 </td>
             <td class="style28">
                
             </td>
             <td class="style48">
                 <asp:Label ID="Label24" runat="server" Font-Size="X-Small" 
                     Text="Fecha Aprox. Embarque:"></asp:Label>
                 </td>
             <td class="style47">
                
                 <asp:TextBox ID="tx_fechaAproxEmbarque" runat="server" Width="118px" 
                     Font-Size="X-Small"></asp:TextBox>
                 <asp:CalendarExtender ID="tx_fechaAproxEmbarque_CalendarExtender" 
                     runat="server" TargetControlID="tx_fechaAproxEmbarque">
                 </asp:CalendarExtender>
                
                 </td>
             <td>                        
                            </td>
             <td>
                 <asp:Label ID="Label22" runat="server" Font-Size="X-Small" 
                     Text="Entrega Cliente :"></asp:Label>
                            </td>
             <td class="style15">
                
                 <asp:TextBox ID="tx_entregaCliente" runat="server" Font-Size="X-Small" 
                     Width="118px"></asp:TextBox>
                 <asp:CalendarExtender ID="tx_entregaCliente_CalendarExtender" runat="server" 
                     TargetControlID="tx_entregaCliente">
                 </asp:CalendarExtender>
                            </td>
             <td class="style35"></td>
          </tr>
          <tr>
          <td class="style50">
              <asp:Label ID="Label17" runat="server" Text="Marca :" Font-Size="X-Small"></asp:Label>
          </td>
          <td>
              <asp:DropDownList ID="dd_Marca" runat="server" Width="145px">
              </asp:DropDownList>
          </td>
          <td></td>
          <td class="style49">
              <asp:Label ID="Label25" runat="server" Font-Size="X-Small" 
                  Text="Fecha Pago Embarque Segun Contrato:"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="tx_fechaPagoEmbarque" runat="server" Width="118px" 
                  Font-Size="X-Small"></asp:TextBox>
              <asp:CalendarExtender ID="tx_fechaPagoEmbarque_CalendarExtender" runat="server" 
                  TargetControlID="tx_fechaPagoEmbarque">
              </asp:CalendarExtender>
          </td>
          <td></td>
          <td>
                            
                 <asp:Label ID="Label23" runat="server" Font-Size="X-Small" 
                     Text="Habilitacion del Equipo :"></asp:Label>
                
                 </td>
          <td>
                
                 <asp:TextBox ID="tx_habilitacionEquipo" runat="server" Font-Size="X-Small" 
                     Width="118px"></asp:TextBox>
                 <asp:CalendarExtender ID="tx_habilitacionEquipo_CalendarExtender" 
                     runat="server" TargetControlID="tx_habilitacionEquipo">
                 </asp:CalendarExtender>
                 </td>
          <td></td>
          </tr>
          <tr>
          <td class="style50">
              <asp:Label ID="Label27" runat="server" Text="CODCLIsimec:" Font-Size="X-Small"></asp:Label>
              </td>
          <td>
              <asp:TextBox ID="tx_CodCli_simec" runat="server" Font-Size="X-Small" 
                  Width="145px"></asp:TextBox>
              </td>
          <td></td>
          <td>
                <asp:Label ID="Label26" runat="server" Font-Size="X-Small" 
                    Text="Fecha Confirmación de Pago Embarque:"></asp:Label>
                 </td>
          <td>
                
                <asp:TextBox ID="tx_fechaConfirmacionPagoEmbarque" runat="server" 
                    Font-Size="X-Small" Width="118px"></asp:TextBox>
                <asp:CalendarExtender ID="tx_fechaConfirmacionPagoEmbarque_CalendarExtender" 
                    runat="server" TargetControlID="tx_fechaConfirmacionPagoEmbarque">
                </asp:CalendarExtender>
                
                 </td>
          <td></td>
          <td>
                            
                <asp:Label ID="Label9" runat="server" Text="Tecnico Instalador" 
                    Font-Size="X-Small" Width="135px"></asp:Label>
                
              </td>
          <td>
                <asp:TextBox ID="tx_tecInstalador" runat="server" Font-Size="X-Small"  Width="118px"></asp:TextBox>
                     <asp:AutoCompleteExtender ID="tx_tecInstalador_AutoCompleteExtender" runat="server" 
                                            TargetControlID="tx_tecInstalador"          
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
          </tr>
           
          <tr>
          <td class="style50">
              <asp:Label ID="Label28" runat="server" Text="MonedaSimec:" Font-Size="X-Small"></asp:Label>
              </td>
          <td>
              <asp:DropDownList ID="dd_monedaSimec" runat="server" Width="120px">
                  <asp:ListItem Value="0">Ninguno</asp:ListItem>
                  <asp:ListItem Value="2">Dolares</asp:ListItem>
                  <asp:ListItem Value="1">Bolivianos</asp:ListItem>
              </asp:DropDownList>
              </td>
          <td></td>
          <td>
                <asp:Label ID="lbFechaEquipoObra" runat="server" Text="Equipo en Obra :" 
                     Font-Size="X-Small" Width="130px"></asp:Label>
              </td>
          <td>
                
                <asp:TextBox ID="txtFechaEquipoObra" runat="server" Width="118px" 
                    style="margin-bottom: 0px" Font-Size="X-Small"/>
                 <asp:CalendarExtender ID="calendarFechaEquipoObra" runat="server" 
                     TargetControlID="txtFechaEquipoObra">
                 </asp:CalendarExtender>
                
              </td>
          <td></td>
          <td>
                 <asp:Label ID="Label18" runat="server" Font-Size="X-Small" 
                     Text="Fiscal de Proyecto"></asp:Label>
              </td>
          <td>
                 <asp:TextBox ID="tx_fiscalProyecto" runat="server" Font-Size="X-Small"  Width="118px"></asp:TextBox>
                 <asp:AutoCompleteExtender ID="tx_fiscalProyecto_AutoCompleteExtender2" runat="server" 
                                            TargetControlID="tx_fiscalProyecto"          
                                            CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
        
                                        </asp:AutoCompleteExtender>
              </td>
          <td></td>
          </tr>         
           
           <tr>
           <td>
               <asp:Label ID="Label33" runat="server" Font-Size="X-Small" 
                   Text="Variable Simec"></asp:Label>
               </td>
           <td>
               <asp:TextBox ID="tx_variableSimec" runat="server" Width="145px"></asp:TextBox>
               </td>
           <td></td>
           <td></td>
           <td></td>
           <td></td>
           <td></td>
           <td></td>
           <td></td>
           </tr>
           
          </table>

          <!-- Responsables de Equipos -->
          <table>
          <tr>
          <td></td>
          <td>
              <asp:Label ID="Label29" runat="server" Text="RIN:" Font-Size="Small"></asp:Label>
              </td>
          <td>
              <asp:TextBox ID="tx_rin" runat="server" Width="300px" Font-Size="Small"></asp:TextBox>
              <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" 
                                            TargetControlID="tx_rin"          
                                            CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
        
                                        </asp:AutoCompleteExtender>
              </td>
          <td></td>
          <td>
              <asp:Label ID="Label30" runat="server" Text="RCC:" Font-Size="Small"></asp:Label>
              </td>
          <td>
              <asp:TextBox ID="tx_rcc" runat="server" Width="300px" Font-Size="Small"></asp:TextBox>
              <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" 
                                            TargetControlID="tx_rcc"          
                                            CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
        
                                        </asp:AutoCompleteExtender>
              </td>
          <td></td>
          </tr>

          <tr>
          <td></td>
          <td>
              <asp:Label ID="Label31" runat="server" Font-Size="Small" Text="TecMant:"></asp:Label>

              </td>
          <td>
              <asp:TextBox ID="tx_tecmantenimiento" runat="server" Font-Size="Small" 
                  Width="300px"></asp:TextBox>
               <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" 
                                            TargetControlID="tx_tecmantenimiento"          
                                            CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
        
                                        </asp:AutoCompleteExtender>
              </td>
          <td></td>
          <td>
              <asp:Label ID="Label32" runat="server" Font-Size="Small" Text="Supervisor:"></asp:Label>
              </td>
          <td>
              <asp:TextBox ID="tx_supervisor" runat="server" Font-Size="Small" Width="300px"></asp:TextBox>
               <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" 
                                            TargetControlID="tx_supervisor"          
                                            CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
        
                                        </asp:AutoCompleteExtender>
              </td>
          <td></td>      
          
          </tr>

          </table>

          <!-- tipologia -->
                 <table>
                 <tr>
                 <td></td>
                 <td>
                     <asp:Label ID="Label20" runat="server" Text="Modelo:" Font-Size="X-Small"></asp:Label>
                     </td>
                 <td>
                     <asp:TextBox ID="tx_modelo" runat="server" Font-Size="X-Small" 
                         Width="100px"></asp:TextBox></td>
                 <td>
                     <asp:Label ID="Label4" runat="server" Text="Pasajero:" Font-Size="X-Small"></asp:Label></td>
                 <td>
                     <asp:TextBox ID="tx_pasajero" runat="server" Font-Size="X-Small" 
                         Width="50px"></asp:TextBox></td>
                 <td>
                     <asp:Label ID="Label10" runat="server" Text="Parada:" Font-Size="X-Small"></asp:Label></td>
                 <td>
                     <asp:TextBox ID="tx_parada" runat="server" Font-Size="X-Small" 
                         Width="50px"></asp:TextBox></td>
                 <td>
                     <asp:Label ID="Label11" runat="server" Text="Velocidad:" Font-Size="X-Small"></asp:Label></td>
                <td> 
                    <asp:TextBox ID="tx_velocidad" runat="server" Font-Size="X-Small" 
                        Width="100px"></asp:TextBox> </td>
                <td>
                    <asp:Label ID="Label34" runat="server" Font-Size="Small" 
                        Text="Identificador Ascensor:"></asp:Label>
                     </td>
                <td>
                    <asp:TextBox ID="tx_identificacionAscensor" runat="server" Width="100px"></asp:TextBox>
                     </td>
                <td></td>
                <td></td>
                 </tr>
                 </table>


          <table style="width: 497px; margin-top: 15px; margin-left: 231px;">
          <tr>
          <td class="style18">
                <asp:Button ID="btnNuevo" runat="server" Text="Limpiar" Width="65px" 
                     onclick="btnNuevo_Click" Font-Size="Small"/>
              </td>
          <td class="style20">
                <asp:Button ID="btnModificar" runat="server" Text="Actualizar" Width="75px" 
                     onclick="btnModificar_Click" 
                    style="margin-bottom: 0px; margin-left: 0px;" 
                    Font-Size="Small" />
              </td>
          <td class="style21">
              <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Font-Size="Small" 
                  Width="65px" onclick="btnBuscar_Click" />
                
              </td>
          <td class="style22">
              &nbsp;</td>
          </tr>
    </table>
 </div>
</td>
</tr>

<tr>
<td>
<div class ="GE2" >
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
            onselectedindexchanged="GridView1_SelectedIndexChanged1">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#99CC00" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#99CC00" Font-Bold="True" ForeColor="White" />
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
    <asp:Label ID="Label1" runat="server" Text="Cantidad de Equipos:" 
        Font-Size="Small"></asp:Label>
    <asp:TextBox ID="tx_cantidadEquipos" runat="server" Enabled="False" 
        style="margin-left: 13px">0</asp:TextBox>
</td>
</tr>
<tr>
<td></td>
</tr>

<tr>
<td>
    <div class="GE3">
        <asp:Button ID="btExportarExcel" runat="server" onclick="btExportarExcel_Click" 
            Text="Exportar Excel" style="margin-left: 44px" />
    </div>

</td>
</tr>


</table>


</div>








</asp:Content>
