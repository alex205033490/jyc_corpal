<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FCorpal_agendanegociacion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_agendanegociacion" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_AgendaNegociacion.css" rel="stylesheet" type="text/css" />

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
            width: 428px;
        }
        
        .style2
       {
           width: 97px;
       }
        
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    

<div class="Centrar">
<div class="titulo"></div>

<table>
<tr>
<td class="style1">
<div class="DatosProyecto">

    <table>
        <tr>
            <td></td>
            <td>
                <asp:Label ID="Label28" runat="server" style="font-weight: 700" Text="Datos:"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>            
        </tr>
        <tr>
        <td></td>
        <td>
                <asp:Label ID="Label2" runat="server" Text="Proyecto:" 
                    Font-Size="X-Small"></asp:Label>
            </td>
        <td>
                <asp:TextBox ID="tx_Proyeto" runat="server" Width="250px" 
                    Font-Size="X-Small"></asp:TextBox>
                <asp:AutoCompleteExtender ID="tx_Proyeto_AutoCompleteExtender" runat="server" 
                    TargetControlID="tx_Proyeto"
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
        <td>
                <asp:Button ID="bt_buscarProyecto" runat="server" 
                    onclick="bt_buscarProyecto_Click" Text="Buscar Proyecto" />
            </td>
        </tr>

        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label29" runat="server" Font-Size="X-Small" Text="Responsable:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_responsableBusqueda" runat="server" Width="250px" 
                Font-Size="X-Small"></asp:TextBox>
            <asp:AutoCompleteExtender ID="tx_responsableBusqueda_AutoCompleteExtender" 
                runat="server" TargetControlID="tx_responsableBusqueda"
               CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                >
            </asp:AutoCompleteExtender>
            </td>
        <td>
            <asp:Button ID="bt_agendaAsignacion" runat="server" Text="Buscar Agenda" 
                onclick="bt_agendaAsignacion_Click" />
            </td>
        </tr>

     <tr>
     <td></td>
     <td>
         <asp:Label ID="Label30" runat="server" Font-Size="X-Small" Text="desde:"></asp:Label>
         </td>
     <td>
         <asp:TextBox ID="tx_fechadesdeBusqueda" runat="server" Font-Size="X-Small" 
             Width="150px"></asp:TextBox>
         <asp:CalendarExtender ID="tx_fechadesdeBusqueda_CalendarExtender" runat="server" 
             TargetControlID="tx_fechadesdeBusqueda">
         </asp:CalendarExtender>
         </td>
     <td>
         <asp:Label ID="Label37" runat="server" Font-Size="Small" Text="Estado:"></asp:Label>
         </td>

     </tr>

      <tr>
     <td></td>
     <td>
         <asp:Label ID="Label31" runat="server" Font-Size="X-Small" Text="hasta:"></asp:Label>
          </td>
     <td>
         <asp:TextBox ID="tx_fechahastaBusqueda" runat="server" Font-Size="X-Small" 
             Width="150px"></asp:TextBox>
         <asp:CalendarExtender ID="tx_fechahastaBusqueda_CalendarExtender" runat="server" 
             TargetControlID="tx_fechahastaBusqueda">
         </asp:CalendarExtender>
          </td>
     <td>
            <asp:DropDownList ID="dd_estadobusqueda" runat="server" Font-Size="X-Small" 
                Width="120px">
                <asp:ListItem>Abierto</asp:ListItem>
                <asp:ListItem>Cerrado</asp:ListItem>
            </asp:DropDownList>
            </td>

     </tr>






    </table>
           

</div>
</td>

<td>
<div class="ProyectosView">
    <asp:GridView ID="gv_Proyecto" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
        onselectedindexchanged="gv_Proyecto_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
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
<div class="Cliente">
        <table>
        <tr>
        <td></td>
        <td class="style2">
            <asp:Label ID="Label8" runat="server" Text="HoraAsignacion:" 
                Font-Size="X-Small"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_horaAsignacion" runat="server" Font-Size="X-Small" 
                Width="120px"></asp:TextBox>
            </td>
        <td>
            <asp:Label ID="Label15" runat="server" Text="FechaAsignacion:" 
                Font-Size="X-Small"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_fechaAsignacion" runat="server" Font-Size="X-Small" 
                Width="120px"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaAsignacion_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaAsignacion">
            </asp:CalendarExtender>
            </td>  
        </tr>
        <tr>
        <td></td>
        <td class="style2">
            <asp:Label ID="Label34" runat="server" Font-Size="X-Small" 
                Text="horaExpiracion:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_horaexpiracion" runat="server" Font-Size="X-Small" 
                Width="120px"></asp:TextBox>
            </td>
        <td>
            <asp:Label ID="Label35" runat="server" Font-Size="X-Small" 
                Text="FechaExpiracion:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_fechaExpiracion" runat="server" Font-Size="X-Small" 
                Width="120px"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaExpiracion_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaExpiracion">
            </asp:CalendarExtender>
            </td>
        </tr>

        <tr>
        <td></td>
        <td class="style2">
            <asp:Label ID="Label19" runat="server" Text="Estado:" Font-Size="X-Small"></asp:Label>
            </td>
        <td>
            <asp:DropDownList ID="dd_estado" runat="server" Font-Size="X-Small" 
                Width="120px">
                <asp:ListItem>Abierto</asp:ListItem>
                <asp:ListItem>Cerrado</asp:ListItem>
            </asp:DropDownList>
            </td>     
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
            <asp:Label ID="Label36" runat="server" Font-Size="X-Small" Text="Edificio:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_edificioAgenda" runat="server" Font-Size="X-Small" 
                Width="300px"></asp:TextBox>
           <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" 
                    TargetControlID="tx_edificioAgenda"
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
        </tr>

        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label33" runat="server" Font-Size="X-Small" Text="Responsable:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_responsableAsignado" runat="server" Font-Size="X-Small" 
                Width="300px"></asp:TextBox>
            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" 
                runat="server" TargetControlID="tx_responsableAsignado"
               CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                >
            </asp:AutoCompleteExtender>
            </td>
        </tr>

        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label26" runat="server" Font-Size="X-Small" Text="Objetivo:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_objetivo" runat="server" Height="67px" TextMode="MultiLine" 
                Width="327px" Font-Size="X-Small"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label32" runat="server" Font-Size="X-Small" 
                Text="Detalle de cierre:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_detalleCierre" runat="server" Height="67px" TextMode="MultiLine" 
                Width="327px" Font-Size="X-Small"></asp:TextBox>
            </td>
        </tr>

        </table>

        <table>
        <tr>
        <td></td>
        <td>
            <asp:Button ID="bt_limpiarDatos" runat="server" Text="Limpiar" 
                onclick="bt_limpiarDatos_Click" />
            </td>
        <td></td>
        <td>
            <asp:Button ID="bt_nuevoAgenda" runat="server" Text="Nuevo" 
                onclick="bt_nuevoAgenda_Click" />
            </td>
        <td></td>
        <td>
            <asp:Button ID="bt_modificarAgenda" runat="server" Text="Modificar" 
                onclick="bt_modificarAgenda_Click" />
            </td>
        <td></td>
        <td>
            <asp:Button ID="bt_eliminarAgenda" runat="server" 
                onclick="bt_eliminarAgenda_Click" Text="Eliminar" />
            </td>
        </tr>
        </table>

    </div>  
</td>

<td>
<div class="datosCliente">
        <asp:GridView ID="gv_AgendaAsignacion" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
            onselectedindexchanged="gv_AgendaAsignacion_SelectedIndexChanged">
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
</tr>



<tr>
<td class="style1">
    
    <asp:LinkButton ID="lk_exportarExcel" runat="server" 
        onclick="lk_exportarExcel_Click">Exportar a Excel</asp:LinkButton>
    
</td>
</tr>

<tr>
<td class="style1">

</td>
</tr>

<tr>
<td class="style1"></td>
</tr>
</table>



</div>




</asp:Content>
