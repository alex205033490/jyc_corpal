<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FCorpal_CompraDeMaterialeInsumos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CompraDeMaterialeInsumos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

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
            <h3 class="panel-title">Compra Materia Prima e Insumos:</h3>
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
                <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
            </td>   
            <td>
                <asp:DropDownList ID="dd_estadoSolicitud" class="form-control" runat="server" 
                    Width="120px">
                    <asp:ListItem>Abierto</asp:ListItem>
                    <asp:ListItem>Cerrado</asp:ListItem>
                    <asp:ListItem>Comprado</asp:ListItem>
                    <asp:ListItem>Rechazado</asp:ListItem>
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
            <h3 class="panel-title">Solicitud:</h3>
            </div>
            <div class="panel-body">
            <div class="DatosProyecto">
                <asp:GridView ID="gv_MaterialSolicitado" 
                    CssClass="table table-responsive table-striped" runat="server" BackColor="White" 
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
            <h3 class="panel-title">Datos:</h3>
            </div>
            <div class="panel-body">
            <div class="DatosItem">
                <asp:GridView ID="gv_DatosItem" 
                    CssClass="table table-responsive table-striped" runat="server" BackColor="White" 
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
                        <asp:TemplateField HeaderText="cantidadcomprada" 
                            SortExpression="cantidadcomprada">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("cantidadcomprada") %>' 
                                    Visible="False"></asp:Label>
                                <asp:TextBox ID="tx_cantidadComprada" runat="server" BackColor="Yellow" 
                                    Text='<%# Bind("cantidadcomprada") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="montototalcomprado (Bs)" 
                            SortExpression="montototalcomprado">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("montototalcomprado") %>' 
                                    Visible="False"></asp:Label>
                                <asp:TextBox ID="tx_montototalcomprado" runat="server" BackColor="Yellow" 
                                    Text='<%# Bind("montototalcomprado") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="factura" SortExpression="factura">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("factura") %>' 
                                    Visible="False"></asp:Label>
                                <asp:DropDownList ID="dd_factura" runat="server" BackColor="Yellow" 
                                    Width="100px">
                                    <asp:ListItem>Si</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("factura") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="retencion" SortExpression="retencion">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("retencion") %>' 
                                    Visible="False"></asp:Label>
                                <asp:DropDownList ID="dd_retencion" runat="server" BackColor="Yellow" 
                                    Width="100px">
                                    <asp:ListItem>Si</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("retencion") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="tipocompra" SortExpression="tipocompra">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("tipocompra") %>' 
                                    Visible="False"></asp:Label>
                                <asp:DropDownList ID="dd_tipocompra" runat="server" BackColor="Yellow" 
                                    Width="150px">
                                    <asp:ListItem>Contado</asp:ListItem>
                                    <asp:ListItem>Credito</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("tipocompra") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
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
