<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_DevoluciondeProductoTerminado.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_DevoluciondeProductoTerminado" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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


    <div class="container" style="padding-top: 1em;">
   


<div class="row">
    <div class="col-md-10 col-md-offset-1">
        <div class="panel panel-success class">
            <div class="panel-heading">
            <h3 class="panel-title">Devolucion Produccion:</h3>
            </div>
            <div class="panel-body">
            
       <table>   
       <tr>
        <td>
            <asp:Label ID="Label41" runat="server" Text="Fecha:"></asp:Label>
           </td>
        <td>
            <asp:TextBox ID="tx_fechaDevolucion" class="form-control"  runat="server" 
                Width="150px"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaDevolucion_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaDevolucion">
            </asp:CalendarExtender>
           </td>
        <td></td>
       </tr>
       <tr>
        <td>
            <asp:Label ID="Label36" runat="server" Text="Nombre Vendedor :"></asp:Label>
           </td>
        <td>
            <asp:TextBox ID="tx_nombreVendedor" class="form-control" runat="server" 
                Width="350px" ></asp:TextBox>
           </td>
        <td><asp:Button ID="bt_buscar" class="btn btn-success" runat="server"   
                    onclick="bt_buscar_Click" Text="Buscar" /></td>
       </tr>  
       
       <tr>
       <td>
                <asp:Label ID="Label40" runat="server"  Text="Producto Nax :"></asp:Label>
           </td>
       <td>
                <asp:DropDownList ID="dd_productosNax" class="form-control" runat="server" 
                    Width="350px"  onselectedindexchanged="dd_productosNax_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
           </td>
       <td></td>       
       </tr> 

       <tr>
        <td>
                <asp:Label ID="Label42" runat="server" Text="Medida:"></asp:Label>
            </td>
        <td>
                <asp:TextBox ID="tx_medida" class="form-control" runat="server" Width="150px"></asp:TextBox>
            </td>
        <td></td>        
       </tr>

       <tr>
            <td>
                <asp:Label ID="Label31" runat="server" Text="Cantidad:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_cantcajas" class="form-control" runat="server" 
                    Width="150px"></asp:TextBox>
            </td>
            <td></td>
       </tr>

        <tr>
        <td>
            <asp:Label ID="Label30" runat="server" Font-Size="Small" 
                Text="Almacenero que Recibe:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_almaceneroquerecibe" class="form-control" runat="server" 
                Font-Size="Small" Width="400px"></asp:TextBox>           
            <asp:AutoCompleteExtender ID="tx_almaceneroquerecibe_AutoCompleteExtender" 
                runat="server" TargetControlID="tx_almaceneroquerecibe"
                MinimumPrefixLength="1" ServiceMethod="GetlistaResponsable2"
             CompletionListCssClass="CompletionList"  CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" 
                CompletionInterval="10">
            </asp:AutoCompleteExtender>
           
            </td>                    
            <td>&nbsp;</td>   
            
        </tr>       
        <tr>
        <td>
                <asp:Label ID="Label35" runat="server" Text="Motivo de la Devolucion:"></asp:Label>
            </td>
        <td>
                <asp:TextBox ID="tx_MotivoDevolucion" class="form-control" runat="server" 
                    Height="100px" TextMode="MultiLine" 
                    Width="500px"></asp:TextBox>
            </td>                    
            <td>&nbsp;</td>   
            
        </tr>       
        <tr>
            <td>
            <asp:Label ID="Label1" runat="server" Font-Size="Small" 
                Text="Se enviara a:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="dd_seenviaraa" class="form-control"  runat="server" 
                    Width="350px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Destrucción</asp:ListItem>
                    <asp:ListItem>Otros</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>
            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label32" runat="server" Text="Observaciones:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_observacionesDevolucion" class="form-control" runat="server" 
                    Height="100px" TextMode="MultiLine" 
                    Width="500px"></asp:TextBox>
            </td>
            <td></td>
            
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
                    Text="Insertar" onclick="bt_insertar_Click" />
            </td>
            <td>
                <asp:Button ID="bt_verRecibo" runat="server" class="btn btn-light" 
                    Text="Ver Recibo" onclick="bt_verRecibo_Click" />
                <asp:Button ID="bt_modificar" class="btn btn-warning" runat="server" 
                    Text="Modificar" onclick="bt_modificar_Click" />
            </td>

            <td>
                <asp:Button ID="bt_eliminar" class="btn btn-danger" runat="server" 
                    Text="Eliminar" onclick="bt_eliminar_Click" />
            </td>

            <td>
                &nbsp;</td>
            
        </tr>
    </table>

            </div>
        </div>
     </div>
</div>


<div class="row">
    <div class="col-md-10 col-md-offset-1">
        <div class="panel panel-success class">
            <div class="panel-heading">
            <h3 class="panel-title">Datos:</h3>
            </div>
            <div class="panel-body">
            <div class="DatosProyecto">
                

                <asp:GridView ID="gv_DevoluciondelProduccion" 
                    CssClass="table table-responsive table-striped" runat="server" BackColor="White" 
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                    Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
                    onselectedindexchanged="gv_DevoluciondelProduccion_SelectedIndexChanged">
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
            <asp:Button ID="bt_excel" class="btn btn-success mr-2" runat="server" 
                    Text="Excel" onclick="bt_excel_Click" />
            </div>
        </div>
    </div>
</div>


</div>

</asp:Content>
