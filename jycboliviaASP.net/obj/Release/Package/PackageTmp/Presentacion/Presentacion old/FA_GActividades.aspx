<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_GActividades.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_GActividades"  EnableEventValidation = "false" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_AsignarTareas.css" rel="stylesheet" type="text/css" />

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

<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

<div class = "Centrar">    
    <table>
    <tr>
    <td>
    <div class = "titulo"><h1>Gestionar Tareas</h1></div>
    </td>        
    </tr>

    <tr>
    <td>
    <div class = "BuscarEdificio">
        <table>
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="Edificio:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_edificio" runat="server" Width="300px"></asp:TextBox>
            <asp:AutoCompleteExtender ID="tx_edificio_AutoCompleteExtender" runat="server"  TargetControlID="tx_edificio"
             CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaProyectos2" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
            >
            </asp:AutoCompleteExtender>
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
            <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="Tarea nueva:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_tareaTecnico" runat="server" Width="200px"></asp:TextBox>
            </td>
        <td></td>
        <td>
            <asp:Button ID="bt_crear" runat="server" Height="25px" Text="Crear Tarea" 
                Width="100px" onclick="bt_crear_Click" />
            </td>
        <td></td>
        <td>
            <asp:Button ID="bt_crearCoti" runat="server" onclick="bt_crearCoti_Click" 
                Text="Crear Coti" Height="25px" Width="100px" Visible="False" />
            </td>
        </tr>

        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Tecnico:" Font-Size="Small"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_tecnicoNombre" runat="server" Width="300px"></asp:TextBox>
            <asp:AutoCompleteExtender ID="tx_tecnicoNombre_AutoCompleteExtender" runat="server" 
                TargetControlID="tx_tecnicoNombre"
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
        <asp:Label ID="Label4" runat="server" Text="Tecnico Asignado:" 
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
