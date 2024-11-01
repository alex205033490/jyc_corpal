<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_Recetas.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_Recetas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/Style_SimecModificar.css" rel="stylesheet" type="text/css" />
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
  <div class="card">
  <div class="card-header bg-success text-white">Crear Recetas</div>
  <ul class="list-group list-group-flush">
    <li class="list-group-item">
     <div class="row">
            <div class="col-md-12 col-md-offset-1">        
                <h3>Receta</h3>
     <div class="gv_recetas">
         <asp:GridView ID="gv_recetasCreada" 
             runat="server" BackColor="White" 
             BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
             Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
             onselectedindexchanged="gv_reciboIngresoEgreso_SelectedIndexChanged">
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
     </div>  
            </div>        
    </li>

    <li class="list-group-item">
        <div class="row">
    <div class="col-md-12 col-md-offset-1">        
               <table>             
               <tr>        
                <td><asp:Label ID="Label46" runat="server" Text="Receta:"></asp:Label></td>                    
                <td><asp:TextBox ID="tx_nameReceta" class="form-control"  runat="server"  ></asp:TextBox>                   </td>   
                <td>
                    <asp:Button ID="bt_buscar" class="btn btn-info" runat="server" Text="Buscar" OnClick="bt_buscar_Click" /></td>
               </tr>       
                <tr>
                    <td>
                        <asp:Label ID="Label40" runat="server"  Text="Producto Asignar :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="dd_productosNax" class="btn btn-secondary dropdown-toggle" runat="server" 
                            Width="400px" >
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
               <table>                
                <tr>
                    <td><asp:Label ID="Label4" runat="server" Text="Cant. por Dia"></asp:Label></td>
                    <td><asp:TextBox ID="tx_cantidadPorDia" class="form-control" runat="server"></asp:TextBox></td>
                    <td><asp:Label ID="Label5" runat="server" Text="Condicionante"></asp:Label></td>
                    <td><asp:CheckBox ID="cbx_condicionante" runat="server" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label1" runat="server" Text="Insumos:"></asp:Label></td>
                    <td><asp:TextBox ID="tx_insumos" CssClass="form-control" runat="server" BackColor="Yellow"></asp:TextBox>                        
                        <asp:AutoCompleteExtender ID="tx_insumos_AutoCompleteExtender" runat="server" 
                            TargetControlID="tx_insumos"
                            CompletionSetCount="12" 
                            MinimumPrefixLength="1" ServiceMethod="GetlistaInsumos" 
                            UseContextKey="True"
                            CompletionListCssClass="CompletionList" 
                            CompletionListItemCssClass="CompletionlistItem" 
                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10" >
                        </asp:AutoCompleteExtender> 
                    </td>
                    <td><asp:Label ID="Label32" runat="server" Text="Cantidad:"></asp:Label></td>
                    <td><asp:TextBox ID="tx_cantInsumos" class="form-control" runat="server" Width="150px"></asp:TextBox></td>
                    <td><asp:Button ID="bt_adicionarInsumos" class="btn btn-success" runat="server" Text="Add" OnClick="bt_adicionarInsumos_Click" /></td>
                    <td>
                        <asp:Button ID="bt_updateInsumos" class="btn btn-warning" runat="server" Text="Update" OnClick="bt_updateInsumos_Click" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label2" runat="server" Text="Insumos Compuesto:"></asp:Label></td>
                    <td><asp:TextBox ID="tx_insumosCompuesto" CssClass="form-control" runat="server" BackColor="Yellow"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="tx_insumosCompuesto_AutoCompleteExtender" runat="server" 
                            TargetControlID="tx_insumosCompuesto"
                            CompletionSetCount="12" 
                            MinimumPrefixLength="1" ServiceMethod="GetlistaInsumosCompuesto" 
                            UseContextKey="True"
                            CompletionListCssClass="CompletionList" 
                            CompletionListItemCssClass="CompletionlistItem" 
                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10" >
                        </asp:AutoCompleteExtender> 

                    </td>
                    <td><asp:Label ID="Label3" runat="server" Text="Cantidad:"></asp:Label></td>
                    <td><asp:TextBox ID="tx_cantInsumosCompuesto" class="form-control" runat="server" Width="150px"></asp:TextBox></td>
                    <td><asp:Button ID="bt_adicionarInsumosCompuesto" class="btn btn-success" runat="server" Text="Add" OnClick="bt_adicionarInsumosCompuesto_Click" /></td>
                    <td>
                        <asp:Button ID="bt_InsumosCompuesto" class="btn btn-warning" runat="server" Text="Update" OnClick="bt_InsumosCompuesto_Click" /></td>
                </tr>             

            </table>
               <table>
                <tr>
                    <td>
                        <asp:Button ID="bt_limpiar" class="btn btn-light" runat="server" Text="Limpiar" 
                            onclick="bt_limpiar_Click" />
                    </td>
                    <td>
                        <asp:Button ID="bt_insertar" class="btn btn-success" runat="server" 
                            Text="Crear Receta" onclick="bt_insertar_Click" />
                    </td>
                    <td>                
                        <asp:Button ID="bt_modificar" class="btn btn-warning" runat="server"
                            Text="Modificar Receta" OnClick="bt_modificar_Click" />
                    </td>

                    <td>
                        <asp:Button ID="bt_eliminar" class="btn btn-danger" runat="server"
                            Text="Eliminar Receta" OnClick="bt_eliminar_Click" />
                    </td>

                    <td></td>
            
                </tr>
            </table>
            </div>        
</div>
    </li>

    <li class="list-group-item">
            <div class="row">
                <div class="col-md-12 col-md-offset-1">  
                    <h3>Insumos</h3>
                        <div class="DatosRecetaInsumos">                            
                            <asp:GridView ID="gv_Insumos"
                                runat="server" BackColor="White"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                Font-Size="Small" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="gv_Insumos_SelectedIndexChanged" OnRowDeleting="gv_Insumos_Deleted">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
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
                        </div>        
            </div>
    </li>

    <li class="list-group-item">
        <div class="row">
    <div class="col-md-12 col-md-offset-1">      
        <h3>Insumos Compuesto</h3>
            <div class="DatosRecetaInsumosCompuesto">
                <asp:GridView ID="gv_InsumosCompuesto"
                    runat="server" BackColor="White"
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Font-Size="Small" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="gv_InsumosCompuesto_SelectedIndexChanged" OnRowDeleting="gv_InsumosCompuesto_Deleted">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
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
            <asp:Button ID="Button1" class="btn btn-success mr-2" runat="server" 
                    Text="Excel" onclick="bt_excel_Click" />
            </div>        
    </div>
    </li>
  </ul>
</div>

</asp:Content>
