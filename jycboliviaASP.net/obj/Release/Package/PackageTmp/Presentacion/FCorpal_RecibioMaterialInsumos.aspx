<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_RecibioMaterialInsumos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FACorpal_RecibioMaterialInsumos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<<<<<<< HEAD

=======
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
>>>>>>> origin/modulo3

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="../Styles/Style_CompradeMaterialeInsumos.css" rel="stylesheet" type="text/css" />
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
            <h3 class="panel-title">Materia Prima e Insumos Comprados:</h3>
            </div>
                     
            <div class="panel-body">
            
       <table>   
       
        <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Font-Size="Small" 
                Text="Responsable Solicitud :"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_responsableSolicitud" class="form-control" runat="server" 
                Font-Size="Small" Width="400px"></asp:TextBox> 
             <asp:AutoCompleteExtender ID="tx_responsableSolicitud_AutoCompleteExtender" 
                    runat="server" TargetControlID="tx_responsableSolicitud"
                    ServiceMethod="GetlistaResponsable2" MinimumPrefixLength="1"
                                    UseContextKey="True"
                                    CompletionListCssClass="CompletionList" 
                                    CompletionListItemCssClass="CompletionlistItem" 
                                    
                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" 
                    CompletionInterval="10">
                </asp:AutoCompleteExtender>
            </td>                    
            <td>
                <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="Estado:"></asp:Label>
            </td>   
            <td>
                <asp:DropDownList ID="dd_estadoSolicitud" runat="server" Width="150px">
                    <asp:ListItem>Comprado</asp:ListItem>
                    <asp:ListItem>Entrega Parcial</asp:ListItem>
                    <asp:ListItem>Cerrado</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>       
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
     
    </table>
       
    
    <table>
        <tr>
            <td>
                <asp:Button ID="bt_limpiar" class="btn btn-light" runat="server" Text="Limpiar" 
                    onclick="bt_limpiar_Click" />
            </td>
            <td>
                <asp:Button ID="bt_actualizar" class="btn btn-success" runat="server" 
                    Text="Actualizar" onclick="bt_actualizar_Click" />
            </td>
            <td>
                <asp:Button ID="bt_buscar" class="btn btn-light" runat="server" onclick="bt_buscar_Click" 
                    Text="Buscar" />
            </td>

            <td>
                &nbsp;</td>

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
            <h3 class="panel-title">Material Comprado:</h3>
            </div>
            <div class="panel-body">
            <div class="DatosProyecto">
                <asp:GridView ID="gv_MaterialSolicitado" 
<<<<<<< HEAD
                    runat="server" BackColor="White" 
=======
                    CssClass="table table-responsive table-striped" runat="server" BackColor="White" 
>>>>>>> origin/modulo3
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                    Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
                    onselectedindexchanged="gv_MaterialSolicitado_SelectedIndexChanged">
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
    </div>
</div>

<div class="row">
    <div class="col-md-10 col-md-offset-1">
        <div class="panel panel-success class">
            <div class="panel-heading">
            <h3 class="panel-title">Insumos Recibidos:</h3>
            </div>
            <div class="panel-body">
            <div class="DatosItem">
                <asp:GridView ID="gv_DatosItem" 
<<<<<<< HEAD
                    runat="server" BackColor="White" 
=======
                    CssClass="table table-responsive table-striped" runat="server" BackColor="White" 
>>>>>>> origin/modulo3
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                    Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
                    AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:BoundField DataField="codigo" HeaderText="codigo" 
                            SortExpression="codigo" />
                        <asp:BoundField DataField="proveedor" HeaderText="proveedor" 
                            SortExpression="proveedor" />
                        <asp:BoundField DataField="item" HeaderText="item" SortExpression="item" />
                        <asp:BoundField DataField="unidadmedida" HeaderText="unidadmedida" 
                            SortExpression="unidadmedida" />
                        <asp:BoundField DataField="cantidad" HeaderText="cantidad Solicitada" 
                            SortExpression="cantidad" />
                        <asp:BoundField DataField="cantidadcomprada" HeaderText="cantidadcomprada" 
                            SortExpression="cantidadcomprada" />
                        <asp:TemplateField HeaderText="cantidadrecibida" 
                            SortExpression="cantidadrecibida">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("cantidadrecibida") %>' 
                                    Visible="False"></asp:Label>
                                <asp:TextBox ID="tx_cantidadRecibida" runat="server" 
                                    Text='<%# Bind("cantidadrecibida") %>' BackColor="Yellow"></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="tipocompra" HeaderText="tipocompra" 
                            SortExpression="tipocompra" />
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
    </div>
</div>
</div>
</asp:Content>
