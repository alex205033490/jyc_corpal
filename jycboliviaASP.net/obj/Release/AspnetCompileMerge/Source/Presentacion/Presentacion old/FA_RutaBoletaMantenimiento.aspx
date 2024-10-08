<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_RutaBoletaMantenimiento.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_RutaBoletaMantenimiento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_RutaBoletaMantenimiento.css" rel="stylesheet" type="text/css" />
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
        
         </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
    </div>



<div class="RB_central">

<table style="margin: 0 auto;">

<tr>
<td>
    <asp:Label ID="Label20" runat="server" Text="Equipo:"></asp:Label>
    </td>
</tr>

<tr>
<td>

<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label18" runat="server" Font-Size="Small" Text="mes:"></asp:Label>
    </td>
<td>
    <asp:DropDownList ID="dd_mesRuta" runat="server">
        <asp:ListItem>Enero</asp:ListItem>
        <asp:ListItem>Febrero</asp:ListItem>
        <asp:ListItem>Marzo</asp:ListItem>
        <asp:ListItem>Abril</asp:ListItem>
        <asp:ListItem>Mayo</asp:ListItem>
        <asp:ListItem>Junio</asp:ListItem>
        <asp:ListItem>Julio</asp:ListItem>
        <asp:ListItem>Agosto</asp:ListItem>
        <asp:ListItem>Septiembre</asp:ListItem>
        <asp:ListItem>Octubre</asp:ListItem>
        <asp:ListItem>Noviembre</asp:ListItem>
        <asp:ListItem>Diciembre</asp:ListItem>
    </asp:DropDownList>
    </td>
<td></td>
<td>
    <asp:Label ID="Label19" runat="server" Font-Size="Small" Text="año:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_anioRuta" runat="server"></asp:TextBox>
    </td>
<td></td>
<td></td>
<td></td>
</tr>


</table>

</td>
</tr>



<tr>
<td>

<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Exbo:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_exbo" runat="server" Width="100px"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Label ID="Label7" runat="server" Text="Edificio:" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_edificio" runat="server" Width="200px"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Button ID="bt_buscarEquipo" runat="server" Text="Buscar" 
        onclick="bt_buscarEquipo_Click" />
    </td>
<td></td>
</tr>
</table>

</td>
</tr>

<tr>
<td>    
<div class="RB_EquiposAsignados">
    <asp:GridView ID="gv_equipoAsignado" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        onselectedindexchanged="gv_equipoAsignado_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#009900" Font-Bold="True" ForeColor="White" />
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
<asp:Label ID="Label3" runat="server" Text="Personal Asignado"></asp:Label>
</td>
</tr>

<tr>
<td>

<table>
<tr>
<td></td>
<td>
    <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="Nombre:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_namePersonal" runat="server" Width="250px"></asp:TextBox>
    <asp:AutoCompleteExtender ID="tx_namePersonal_AutoCompleteExtender" runat="server" 
                                            TargetControlID="tx_namePersonal"          
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
    <asp:Button ID="bt_PersonalAsignado" runat="server" Text="Buscar" 
        onclick="bt_PersonalAsignado_Click" />
    </td>
<td></td>
<td>
    <asp:Button ID="bt_boleta" runat="server" Text="Boleta"  />
    <asp:ModalPopupExtender ID="bt_boleta_ModalPopupExtender" runat="server" 
        TargetControlID="bt_boleta"
        PopupControlID="Panel1"            
        CancelControlID="bt_popupcancel" 
        BackgroundCssClass="modalBackground"
        popupdraghandlecontrolid="PopupHeader" 	drag="false">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" 
        style = "display:none" Width="400px">
            Ingresar Datos:<br />
            <table>
            <tr>
            <td>
            <asp:Label ID="Label17" runat="server" Text="Tipo Boleta:" Font-Size="Small"></asp:Label>
            </td>
            <td>
             <asp:DropDownList ID="dd_tipoBoleta" runat="server" Font-Size="Small" >
            <asp:ListItem>Mantenimiento Preventivo</asp:ListItem>
            <asp:ListItem>Mantenimiento Correctivo/Emergencia</asp:ListItem>            
            <asp:ListItem>Instruccion de Trabajo</asp:ListItem>
            <asp:ListItem>Inspeccion</asp:ListItem>    
            <asp:ListItem>Otros</asp:ListItem>          
        </asp:DropDownList>            
            </td>
            
            <td></td>
            </tr>


            <tr>            
            <td><asp:Label ID="Label12" runat="server" Text="NroBoleta:" Font-Size="Small"></asp:Label></td>
            <td><asp:TextBox ID="tx_NroBoleta" runat="server" Font-Size="Small"></asp:TextBox></td>
            <td></td>
            </tr>
            
            <tr>
            <td><asp:Label ID="Label13" runat="server" Text="Detalle:" Font-Size="Small"></asp:Label></td>
            <td><asp:TextBox ID="tx_DetalleRutapopu" runat="server" Font-Size="Small" TextMode="MultiLine" Height="100px"></asp:TextBox></td>
            <td></td>            
            </tr>

            <tr>
            <td><asp:Label ID="Label11" runat="server" Text="fecha boleta:" Font-Size="Small"></asp:Label></td>
            <td><asp:TextBox ID="tx_popfechaboleta" runat="server"></asp:TextBox>
              <asp:CalendarExtender ID="tx_popfechaboleta_CalendarExtender" 
                                    runat="server" TargetControlID="tx_popfechaboleta">
              </asp:CalendarExtender>
            </td>
            <td></td>
            </tr>

            <tr>
            <td><asp:Label ID="Label15" runat="server" Text="Hora llegada:" Font-Size="Small"></asp:Label></td>
            <td><asp:TextBox ID="tx_pophorallegada" runat="server"></asp:TextBox>
             <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                    TargetControlID="tx_pophorallegada" 
                    AcceptAMPM = "false"
                    MaskType="Time" 
                    Mask="99:99"
                    AutoComplete = "false"                                        
                ></asp:MaskedEditExtender>
            </td>
            <td></td>
            </tr>

            <tr>
            <td><asp:Label ID="Label16" runat="server" Text="Hora Salida:" Font-Size="Small"></asp:Label></td>
            <td><asp:TextBox ID="tx_popHoraSalida" runat="server"></asp:TextBox>
            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                    TargetControlID="tx_popHoraSalida" 
                    AcceptAMPM = "false"
                    MaskType="Time" 
                    Mask="99:99"
                    AutoComplete = "false"                                        
                ></asp:MaskedEditExtender>
            </td>
            <td></td>
            </tr>

            <tr>
            <td><asp:Label ID="Label14" runat="server" Text="Recepcion:" Font-Size="Small" ></asp:Label></td>
            <td><asp:TextBox ID="tx_poppersonaconforme" runat="server" Font-Size="Small" ></asp:TextBox></td>
            <td></td>
            </tr>

            <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="necesidad:" Font-Size="Small"></asp:Label>
            </td>
            <td><asp:CheckBox ID="chx_popCambioRepuesto" Text="Cambio de Repuesto" runat="server" Font-Size="Small" /></td>
            <td></td>
            </tr>

            <tr>
            <td></td>
            <td><asp:CheckBox ID="chx_popArreglo" Text="Arreglo" runat="server" Font-Size="Small" /></td>
            <td></td>
            </tr>

            <tr>
            <td></td>
            <td><asp:CheckBox ID="chx_popSiningresoEdificio" Text="Sin Ingreso a Edificio" runat="server" Font-Size="Small" /></td>
            <td></td>
            </tr>

            </table>
            <table>
            <tr>
            <td></td>
            <td><asp:Button ID="bt_popupok" runat="server" Text="Aceptar" 
                    onclick="bt_popupok_Click" /></td>
            <td><asp:Button ID="bt_popupcancel" runat="server" Text="cancel" /></td>
            <td></td>
            </tr>
            </table>
                        
        </asp:Panel>
  
    </td>
<td></td>
<td></td>
</tr>
</table>

</td>
</tr>

<tr>
<td>
<div class="RB_TecnicoAsignados">    
    <asp:GridView ID="gv_PersonalAsignado" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        onselectedindexchanged="gv_PersonalAsignado_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#009900" Font-Bold="True" ForeColor="White" />
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
    <asp:Label ID="Label9" runat="server" Text="Boletas"></asp:Label>
</td>
</tr>

<tr>
<td>
<div class = "RB_Boleta">
    <asp:GridView ID="gv_boletas" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        onrowdeleting="gv_boletas_RowDeleting">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#009900" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
    <br />
</div>
</td>
</tr>

</table>

</div>



</asp:Content>
