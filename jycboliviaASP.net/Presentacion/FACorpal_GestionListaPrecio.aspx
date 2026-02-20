<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FACorpal_GestionListaPrecio.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FACorpal_GestionListaPrecio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
        .panel-gestion { margin-top: 20px; }
        .margen-top { margin-top: 30px; }
        .fila-seleccionada { background-color: #d9edf7 !important; font-weight: bold; }
        .link-seleccion { text-decoration: none; font-weight: bold; color: #0056b3; }
        .link-seleccion:hover { text-decoration: underline; color: #003d82; }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div class="container panel-gestion">
        <h2>Gestión de Listas de Precios</h2>
        <hr />

        <div class="row mb-3" style="margin-bottom: 15px;">
            <div class="col-md-8">
                <div class="input-group">
                    <asp:TextBox ID="txtBuscarLista" runat="server" CssClass="form-control" Placeholder="Buscar lista por nombre..."></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Button ID="btnBuscarLista" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscarLista_Click" />
                    </span>
                </div>
            </div>
            <div class="col-md-4 text-right">
                <asp:Button ID="btnNuevaLista" runat="server" Text="+ Nueva Lista" CssClass="btn btn-success" OnClick="btnNuevaLista_Click" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="gvListasPrecio" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-bordered table-hover"
                    DataKeyNames="codigo" 
                    OnSelectedIndexChanged="gvListasPrecio_SelectedIndexChanged"
                    OnRowEditing="gvListasPrecio_RowEditing"
                    OnRowDeleting="gvListasPrecio_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="codigo" HeaderText="ID" ItemStyle-Width="50px" />
                        
                        <%-- Hacemos que el Nombre sea clicable para seleccionar la lista --%>
                        <asp:TemplateField HeaderText="Nombre de la Lista">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkNombreLista" runat="server" CommandName="Select" 
                                    Text='<%# Eval("nombre") %>' CssClass="link-seleccion" 
                                    ToolTip="Haga clic para ver los productos de esta lista">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="descuentogral_porcentaje" HeaderText="% Dcto. Gral." DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                        
                        <%-- Botones de Acción: Editar y Eliminar --%>
                        <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Edit" CssClass="btn btn-warning btn-sm" ToolTip="Editar Lista">
                                    Editar
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Delete" CssClass="btn btn-danger btn-sm" ToolTip="Eliminar Lista"
                                    OnClientClick="return confirm('¿Está seguro de eliminar esta lista de precios?');">
                                    Eliminar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                    <SelectedRowStyle CssClass="fila-seleccionada" />
                </asp:GridView>
            </div>
        </div>

        <br />

        <div class="row margen-top" id="panelProductos" runat="server" visible="false">
            <div class="col-md-12">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <h4>Productos en: <asp:Label ID="lblNombreListaSeleccionada" runat="server" ForeColor="#d9534f"></asp:Label></h4>
                    <asp:Button ID="btnAbrirAgregar" runat="server" Text="+ Agregar Producto a la Lista" 
                        CssClass="btn btn-success" OnClick="btnAbrirAgregar_Click" />
                </div>
                
                <asp:GridView ID="gvProductosLista" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-bordered table-hover" 
                    EmptyDataText="No hay productos registrados en esta lista actualmente."
                    DataKeyNames="id_detalle"> 
                    <Columns>
                        <asp:BoundField DataField="id_producto" HeaderText="Cód." ItemStyle-Width="60px" />
                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                        <asp:BoundField DataField="medida_producto" HeaderText="Medida" />
                        <asp:BoundField DataField="precio_base" HeaderText="Precio Base" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="precio_asignado" HeaderText="Precio en Lista" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="porcentaje_descuento" HeaderText="% Dcto." DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                        
                        <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkQuitar" runat="server" CommandName="Delete" CssClass="btn btn-danger btn-sm" 
                                    CommandArgument='<%# Eval("id_detalle") %>' OnClick="btnQuitar_Click" 
                                    OnClientClick="return confirm('¿Está seguro de quitar este producto de la lista?');">
                                    Quitar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#5cb85c" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
