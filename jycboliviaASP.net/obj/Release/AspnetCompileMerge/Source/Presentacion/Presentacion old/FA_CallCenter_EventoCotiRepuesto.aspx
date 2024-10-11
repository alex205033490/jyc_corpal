<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CallCenter_EventoCotiRepuesto.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CallCenter_EventoCotiRepuesto" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menuIzquierdo" Src="MenuIzquierdo.ascx" %>

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
            width: 11px;
        }
        .style5
        {
            width: 50px;
        }
        .style6
        {
            width: 237px;
        }
        .style7
        {
            width: 146px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

   <asp:ScriptManager ID="ScriptManager1" runat="server">
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
         
         <tr>
         <td style="height:100px;"></td>
         </tr>

           
     </table>
  </td>   
  <td>
     <table>
                 <tr>
                 <td>
                    
                                    <div class = "Centrar">
                                    <table >
                                    
                                    <tr>
                                    <td>
                                    <div class = "titulo" >
                                       <h3> <asp:Label ID="lb_EventoNuevo" runat="server" Text="Evento Nuevo"></asp:Label> </h3>
                                    </div>
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
                                        <asp:Label ID="Label3" runat="server" Text="Regional :" Font-Size="Small"  ></asp:Label></td>
                                    <td >
                                        &nbsp;</td>
                                    <td class="style5" >
                                        <asp:TextBox ID="tx_regional" runat="server" Width="120px"></asp:TextBox>
                                        </td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="LLamada :" Font-Size="Small"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="dd_tipoEvento" runat="server" Height="20px" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    </tr>
                                    <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Numero Semana : " Font-Size="Small"></asp:Label></td>
                                    <td class="style3">
                                        <asp:Label ID="lb_numeroSemana" runat="server" Text="0"></asp:Label></td>
                                    <td class="style5"></td>
                                    <td></td>
                                    <td></td>
                                    <td class="style5"></td>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Font-Size="Small" Text="Prioridad :"></asp:Label>
                                        </td>
                                    <td>
                                        <asp:DropDownList ID="dd_prioridad" runat="server" Height="20px" Width="150px">
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
                                    <div class="llamadanuevo">
                                <table>
                                <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Edificio :" Font-Size="Small"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="tx_Edificio" runat="server" Width="250px" 
                                        ontextchanged="tx_Edificio_TextChanged" Font-Size="Smaller"></asp:TextBox>
                                      <asp:AutoCompleteExtender ID="tx_Edificio_AutoCompleteExtender1" runat="server" 
                                            TargetControlID="tx_Edificio" CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaProyectos2" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                                            >
                                        </asp:AutoCompleteExtender> 
                                    </td>
                                <td>
  
                                </td>
                                <td class="style6">
                                    <asp:Button ID="bt_validarEdificio" runat="server" Height="25px" 
                                        onclick="bt_validarEdificio_Click" Text="Verificar" Width="120px" />
                                    </td>
                                <td></td>
                                <td></td>
                                </tr>



                                <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Text="Direccion :" 
                                        Font-Size="Small"></asp:Label>
                                    </td>
                                <td>
                                    <asp:TextBox ID="tx_direccion" runat="server" Width="200px" Font-Size="Smaller"></asp:TextBox>
                                    </td>
                                <td></td>
                                <td class="style6">
                                    <asp:Label ID="lb_verificar2" runat="server" Font-Size="Small"></asp:Label>
                                    </td>
                                <td></td>
                                <td></td>
                                </tr>


                                <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="Nombre Cliente :" Font-Size="Small"></asp:Label>
                                    </td>
                                <td>
                                    <asp:TextBox ID="tx_nombreCliente" runat="server" Width="200px" 
                                        Font-Size="Smaller"></asp:TextBox>
                                    </td>
                                <td></td>
                                <td class="style6">
                                    <asp:Label ID="lb_verificar" runat="server" Font-Size="Small"></asp:Label>
                                    </td>
                                <td></td>
                                <td></td>
                                </tr>



                                <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label13" runat="server" Font-Size="Small" Text="Celular :"></asp:Label>
                                    </td>
                                <td>
                                    <asp:TextBox ID="tx_celular" runat="server" Width="200px" Font-Size="Smaller"></asp:TextBox>
                                    </td>
                                <td></td>
                                <td class="style6">
                                    <asp:Label ID="lb_repuesto" runat="server" Font-Size="Small"></asp:Label>
                                    </td>
                                <td></td>
                                <td>
                                    &nbsp;</td>

                                </tr>
                                <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Font-Size="Small" Text="Telefono :"></asp:Label>
                                    </td>
                                <td>
                                    <asp:TextBox ID="tx_telefono" runat="server" Width="200px" Font-Size="Smaller"></asp:TextBox>
                                    </td>
                                <td></td>
                                <td class="style6">
                                    <asp:Label ID="lb_planPago" runat="server" Font-Size="Small"></asp:Label>
                                    </td>
                                <td></td>
                                <td></td>
                                </tr>

                                <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Ascensores :" Font-Size="Small"></asp:Label>
                                    </td>
                                <td>
                                    <asp:TextBox ID="tx_ascensores" runat="server" Width="200px" 
                                        Font-Size="Smaller"></asp:TextBox>
                                    </td>
                                <td></td>
                                <td class="style6">
                                    <asp:Label ID="lb_cotizacionRepuesto" runat="server" Font-Size="Small"></asp:Label>
                                    </td>
                                <td></td>
                                <td></td>
                                </tr>



                                <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:CheckBox ID="CheckBox1_ascensorParado" runat="server" Text="Ascensor Parado" 
                                        Font-Size="Small" />
                                    </td>
                                <td></td>
                                <td class="style6">
                                    <asp:Label ID="lb_paradoCliente" runat="server" Font-Size="Small"></asp:Label>
                                    </td>
                                <td>
                                    &nbsp;</td>
                                <td></td>
                                </tr>
                                <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:CheckBox ID="CheckBox2_personasAtrapadas" runat="server" Font-Size="Small" 
                                        Text="Personas Atrapadas" />
                                    </td>
                                <td></td>
                                <td class="style6">
                                    <asp:Label ID="lb_paradoNosotros" runat="server" Font-Size="Small"></asp:Label>
                                    </td>
                                <td></td>
                                <td></td>
                                </tr>


                                <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:CheckBox ID="ckb_solicitudRepuesto" runat="server" Font-Size="Small" 
                                        Text="Solicitud de Repuesto" Enabled="False" />
                                    </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                </tr>

                                </table>

                                <table>
                                <tr>
                                <td></td>
                                <td class="style7">
                                    <asp:Label ID="Label9" runat="server" Text="Solicitud de Servicio o atencion :" 
                                        Font-Size="Small"></asp:Label></td>
                                    <td></td>
                                <td>
                                    <asp:TextBox ID="tx_observacion"  TextMode = "MultiLine" runat="server" Height="78px" 
                                        Width="457px" Font-Size="Smaller"></asp:TextBox></td>
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
                                        <asp:Button ID="bt_GuardarDatos" runat="server" Text="Guardar Datos" 
                                            onclick="Button1_Click" />
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
