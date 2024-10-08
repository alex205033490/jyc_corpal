<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_SGI_Actividades.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_SGI_Actividades" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_GActividades.css" rel="stylesheet" type="text/css" />

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
        
        
         .style8
         {
             width: 110px;
         }
        </style>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class = "Centrar">    
    <table>
    <tr>
    <td>
    <div class = "titulo"><h1>Gestionar Actividades</h1></div>
    </td>        
    </tr>

    <tr>
    <td>
    <div class = "BuscarEdificio">
        <table>
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label8" runat="server" Font-Size="Small" 
                Text="Fecha Expiración:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_FechaExpiracion" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="tx_FechaExpiracion_CalendarExtender" runat="server" 
                TargetControlID="tx_FechaExpiracion">
            </asp:CalendarExtender>
            </td>
        <td></td>
        <td>
            <asp:DropDownList ID="dd_estado" runat="server" Height="25px" Width="100px">
                <asp:ListItem Value="1">Abierto</asp:ListItem>
                <asp:ListItem Value="0">Cerrado</asp:ListItem>
            </asp:DropDownList>
            </td>
        <td></td>
        <td>
            <asp:Button ID="bt_buscar" runat="server" Height="25px" Text="Buscar" 
                Width="100px" onclick="bt_buscar_Click" />
            </td>
        </tr>

        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label9" runat="server" Font-Size="Small" Text="Fecha Ejecucion:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_fechaEjecucion" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaEjecucion_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaEjecucion">
            </asp:CalendarExtender>
            </td>
        <td></td>
        <td>
            &nbsp;</td>
        <td></td>
        <td>
            &nbsp;</td>
        </tr>

           <tr>
        <td></td>
        <td>
            <asp:Label ID="Label10" runat="server" Font-Size="Small" Text="Hora Ejecucion:"></asp:Label>
               </td>
        <td>
            <asp:TextBox ID="tx_horaejecucion" runat="server"></asp:TextBox>
               </td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        </tr>
        


        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="Actividad:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_Actividad" runat="server" Height="25px" Width="200px"></asp:TextBox>
            </td>
        <td></td>
        <td>
            <asp:Button ID="bt_crear" runat="server" Height="25px" Text="Crear Actividad" 
                Width="120px" onclick="bt_crear_Click" />
            </td>
        <td></td>
        <td>&nbsp;</td>
        </tr>

     
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Personal:" Font-Size="Small"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_PersonalAsignado" runat="server" Height="25px" 
                Width="200px"></asp:TextBox>
            <asp:AutoCompleteExtender ID="tx_PersonalAsignado_AutoCompleteExtender" runat="server" 
                TargetControlID="tx_PersonalAsignado"
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
            <asp:Button ID="bt_asignarTecnico" runat="server" Height="25px" Text="Asignar" 
                Width="100px" onclick="bt_asignarTecnico_Click" />
            </td>
        <td></td>
        <td></td>
        </tr>

        </table>
    </div>
    </td>
    </tr>

    <tr>
    <td>
    <div class="TablaEdificios">
        <asp:GridView ID="gv_tablaTarea" runat="server" BackColor="#CCCCCC" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
            CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
            onselectedindexchanged="gv_tablaTarea_SelectedIndexChanged">
                   
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
                   
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
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
        <asp:LinkButton ID="lk_buton" runat="server" onclick="lk_buton_Click" 
            Font-Bold="True" ForeColor="#0066FF">Excel</asp:LinkButton>
    </td>
    </tr>

    <tr>
    <td>
    <table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label4" runat="server" Text="Personal Asignado:" 
            Font-Size="Small"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:DropDownList ID="dd_tecnico" runat="server" Width="250px"  
            AutoPostBack="True" onselectedindexchanged="dd_tecnico_SelectedIndexChanged">
        </asp:DropDownList>
        </td>
    <td></td>
    <td></td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label5" runat="server" Text="Fecha de Asignacion:" 
            Font-Size="Small"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:TextBox ID="tx_fechaTecnico" runat="server" Enabled="False"></asp:TextBox>
        </td>
    <td></td>
    <td></td>
    <td></td>
    </tr>

    <tr>
     <td></td>
    <td>
        <asp:Label ID="Label6" runat="server" Text="Hora de Asignacion:" 
            Font-Size="Small"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:TextBox ID="tx_horaTecnico" runat="server" Enabled="False"></asp:TextBox>
        </td>
    <td></td>
     <td></td>
    <td></td>
    </tr>

    <tr>
     <td></td>
    <td>
        <asp:Label ID="Label7" runat="server" Font-Size="Small" Text="observacion:"></asp:Label>
        </td>
     <td></td>
    <td>
        <asp:TextBox ID="tx_observacionTecnico" runat="server" Height="100px" 
            Width="255px" TextMode="MultiLine"></asp:TextBox>
        </td>
     <td></td>
    <td>
        <asp:Button ID="bt_actualizar" runat="server" onclick="bt_actualizar_Click" 
            Text="Actualizar" />
        </td>
     <td></td>

    </tr>


    </table>
    </td>
    </tr>

    <tr>
    <td>
        <div>
            <table>
            <tr>
            <td></td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Detalle Cierre"></asp:Label>
                </td>
            <td></td>
            <td class="style8"></td>
            
            
            </tr>

            <tr>
            <td></td>
            <td>
                <asp:TextBox ID="tx_cierreTarea" runat="server" Height="90px" Width="450px" 
                    TextMode="MultiLine"></asp:TextBox>
                </td>
            <td></td>
            
            <td class="style8">
                <asp:Button ID="bt_CerrarActividad" runat="server" Text="Cerrar Actividad" 
                    Height="25px" Width="100px" onclick="bt_CerrarActividad_Click" />
                </td>
            
            </tr>
            </table>
        </div>
    </td>
    </tr>


    </table>


</div>


</asp:Content>
