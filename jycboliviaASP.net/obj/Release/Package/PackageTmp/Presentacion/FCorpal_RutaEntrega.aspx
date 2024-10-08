<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_RutaEntrega.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_RutaCobrador" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_RutaCobrador.css" rel="stylesheet" type="text/css" />
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
        
        
         </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div class = "menu">  
 

<div class = "Centrar">    
    <table>
    <tr>
    <td>
    <div class = "titulo"><h1>Crear Ruta Entrega</h1></div>
    </td>        
    </tr>

    <tr>
    <td>
    <div class = "BuscarEdificio">
        <table>
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="Tienda:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_tiendanombre" runat="server" Width="300px" 
                Font-Size="Small"></asp:TextBox>
            <asp:AutoCompleteExtender ID="tx_tiendanombre_AutoCompleteExtender" 
                runat="server"  TargetControlID="tx_tiendanombre"
             CompletionSetCount="12" 
                                            MinimumPrefixLength="1" ServiceMethod="GetlistaTienda2" 
                                            UseContextKey="True"
                                            CompletionListCssClass="CompletionList" 
                                            CompletionListItemCssClass="CompletionlistItem" 
                                            
                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
            >
            </asp:AutoCompleteExtender>
            </td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="bt_buscar" runat="server" Font-Size="Small" Text="Buscar" 
                onclick="bt_buscar_Click" />
            </td>
        <td></td>
        <td>
            &nbsp;</td>
        </tr>

        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Asignar Personal:" 
                Font-Size="Small"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_personalAsignado" runat="server" Width="300px" 
                Font-Size="Small"></asp:TextBox>
            <asp:AutoCompleteExtender ID="tx_personalAsignado_AutoCompleteExtender" runat="server" 
                TargetControlID="tx_personalAsignado"
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
            &nbsp;</td>
        <td></td>
        <td>
            &nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td>
            <asp:Label ID="Label14" runat="server" Font-Size="Small" Text="Objetivo:"></asp:Label>
            </td>
            <td>
            <asp:TextBox ID="tx_detalleRuta" runat="server" 
                Width="450px"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        </table>
        <table>
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label9" runat="server" Text="Fecha:" Font-Size="Small"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_fechaEntrega" runat="server" Font-Size="Small"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaEntrega_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaEntrega">
            </asp:CalendarExtender>
            </td>
        <td>
            <asp:Label ID="Label10" runat="server" Text="hora:" Font-Size="Small"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_horaEntrega" runat="server" Font-Size="Small"></asp:TextBox>
            </td>
        <td></td>
        <td>
            <asp:Label ID="Label13" runat="server" Font-Size="Small" Text="Estado:"></asp:Label>
            </td>
        <td>
            <asp:DropDownList ID="dd_estado" runat="server" Font-Size="Small" Width="100px">
                <asp:ListItem>Abierto</asp:ListItem>
                <asp:ListItem>Cerrado</asp:ListItem>
            </asp:DropDownList>
            </td>
        <td></td>
        <td></td>
        </tr>

   
        
        </table>
        <table>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="bt_limpiar" runat="server" Text="Limpiar" 
                        onclick="bt_limpiar_Click" Width="100px" />
                </td>
                <td></td>
                <td>&nbsp;</td>
                <td>
            <asp:Button ID="bt_grabarRutaEntrega" runat="server" Font-Size="Small" 
                Text="Grabar" onclick="bt_grabarcobro_Click" Width="100px" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="bt_modificarEntrega" runat="server" Text="Modificar" 
                        onclick="bt_modificarEntrega_Click" Width="100px" />
                </td>
                <td></td>
                <td>
                    <asp:Button ID="bt_eliminarEntrega" runat="server" Text="Eliminar" 
                        onclick="bt_eliminarEntrega_Click" Width="100px" />
                </td>
            </tr>
        </table>
    </div>
    </td>
    </tr>
    <tr>
        <td>
            <div class="Productos">
                <table>
                    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Productos"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                     <tr>
        <td></td>
        <td>
            <asp:Label ID="Label15" runat="server" Font-Size="Small" Text="Producto:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_producto" runat="server"></asp:TextBox>
            </td>
        <td>
            <asp:Label ID="Label16" runat="server" Font-Size="Small" Text="Cant:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_cantidadProducto" runat="server">0</asp:TextBox>
            </td>
        <td></td>
        <td>
            <asp:Button ID="bt_agregarProducto" runat="server" Text="Agregar" 
                onclick="bt_agregarProducto_Click" />
                         </td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="bt_eliminarProducto" runat="server" Text="Eliminar" 
                onclick="bt_eliminarProducto_Click" />
                         </td>
            <td></td>
        </tr>   
            </table>
        </td>
    </tr>
    <tr>
    <td>
        <table>
                    <tr>
                        <td>
                            <div class="ProductoDetalle">
                                <asp:GridView ID="gv_productos" runat="server" BackColor="White" 
                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                                    Font-Size="Small" ForeColor="Black" GridLines="Vertical">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Anular">
                                            <ItemTemplate>
                                                   <asp:CheckBox ID="chkAll" runat="server"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
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
    </td>
    </tr>

                </table>           
            </div>
        </td>    
    </tr>

    <tr>
        <td>
            <div class="TablaEdificios">
                <table>
                    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Ruta"></asp:Label>
        </td>
    </tr>
    <tr>
    <td>
    <div class="RutaCobroDetalle">
        <asp:GridView ID="gv_rutaEntrega" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
            CellPadding="3" Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
            onselectedindexchanged="gv_rutaEntrega_SelectedIndexChanged">
                   
            <AlternatingRowStyle BackColor="#CCCCCC" />
                   
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
                   
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#669900" Font-Bold="True" ForeColor="White" />
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
        </td>
    </tr>
    



  
  
    
    <tr>
    <td>
    </td>
    </tr>

    <tr>
    <td> 
    </td>
    </tr>

    <tr>
    <td>
  
    </td>
    </tr>


    </table>


</div>


</asp:Content>
