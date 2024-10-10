<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_AprobacionDevolucionProductoTerminado.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_AprobacionDevolucionProductoTerminado" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

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
            <h3 class="panel-title">Busqueda:</h3>
            </div>
            <div class="panel-body">
            
       <table>      
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label30" runat="server" Font-Size="Small" 
                Text="Fecha Devolucion:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_fechaDevolucion" class="form-control" runat="server" 
                Font-Size="Small" Width="150px"></asp:TextBox>
            </td>       
            
            <td>
                <asp:Label ID="Label1" runat="server" Text="Producto :" Font-Size="Small"></asp:Label></td>   
            <td>
                <asp:TextBox ID="tx_producto" runat="server" class="form-control" Width="300px" 
                    Font-Size="Small" ></asp:TextBox>
                <asp:AutoCompleteExtender ID="tx_producto_AutoCompleteExtender" runat="server" 
                    TargetControlID="tx_producto"
                    CompletionSetCount="12" 
                    MinimumPrefixLength="1" ServiceMethod="GetlistaProyectos" 
                    UseContextKey="True"
                    CompletionListCssClass="CompletionList" 
                    CompletionListItemCssClass="CompletionlistItem" 
                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                    >
                </asp:AutoCompleteExtender>
            </td>   

            <td>
            <asp:Button ID="bt_buscar" class="btn btn-success" runat="server" 
                Text="Buscar" onclick="bt_buscar_Click" />
            </td>            
        </tr>    
    </table>
    <table>
    <tr>
        <td></td>
        <td><asp:Button ID="bt_limpiar" class="btn btn-success" runat="server"   
                    onclick="bt_limpiar_Click" Text="Limpiar" /></td>
        <td></td>
        <td>
            <asp:Button ID="bt_aprobar" runat="server" class="btn btn-success" 
                Text="Aprobar" onclick="bt_marcar_Click" />
        </td>
        <td></td>
        <td>
            &nbsp;</td>
        <td>&nbsp;</td>
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
                <asp:GridView ID="gv_produccionProducto" 
<<<<<<< HEAD
                     runat="server" BackColor="White" 
=======
                    CssClass="table table-responsive table-striped" runat="server" BackColor="White" 
>>>>>>> origin/modulo3
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                    ForeColor="Black" GridLines="Vertical" Font-Size="Small" 
                    AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="#CCCCCC" />                   
                    <Columns>
                        <asp:TemplateField HeaderText="Autorizado" 
                            SortExpression="autorizado">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("autorizado") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("autorizado") %>' 
                                    Visible="False"></asp:Label>
                                <asp:CheckBox ID="CheckBox1"  runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="codigo" 
                            SortExpression="codigo" />
                        <asp:BoundField DataField="fecha_devolucion" HeaderText="fecha_devolucion" 
                            SortExpression="fecha_devolucion" />
                        <asp:BoundField DataField="vendedor" HeaderText="vendedor" 
                            SortExpression="vendedor" />
                        <asp:BoundField DataField="producto" HeaderText="producto" 
                            SortExpression="producto" />
                        <asp:BoundField DataField="cantidad" HeaderText="cantidad" 
                            SortExpression="cantidad" />
                        <asp:BoundField DataField="medida" HeaderText="medida" 
                            SortExpression="medida" />
                        <asp:BoundField DataField="almacenerorecibe" HeaderText="almacenerorecibe" 
                            SortExpression="almacenerorecibe" />
                        <asp:BoundField DataField="motivodevolucion" HeaderText="motivodevolucion" 
                            SortExpression="motivodevolucion" />
                        <asp:BoundField DataField="seenviaa" HeaderText="seenviaa" 
                            SortExpression="seenviaa" />
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
            <asp:Button ID="bt_excel" class="btn btn-success mr-2" runat="server" 
                    Text="Excel" onclick="bt_excel_Click" />
            </div>
        </div>
    </div>
</div>

</div>


</asp:Content>
