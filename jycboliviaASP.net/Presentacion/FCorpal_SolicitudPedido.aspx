<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_SolicitudPedido.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_SolicitudPedido" Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/Style_adicionarRepuesto.css" rel="stylesheet" type="text/css" />
<link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
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
            height: 26px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
      <div class="row">        
          <div class="col-12">
          <div class="card">
             <div class="card-header bg-success text-white">
                Solicitud Producto
              </div>

               <ul class="list-group list-group-flush">
                <li class="list-group-item">
                    <div class="row">
                          <div class="col-12">
                            <label for="inputName5" class="form-label">Producto</label>                  
                              <asp:TextBox ID="tx_producto" runat="server" class="form-control" ></asp:TextBox>
                              <asp:AutoCompleteExtender ID="tx_producto_AutoCompleteExtender" runat="server" 
                                  TargetControlID="tx_producto"
                                  CompletionSetCount="12" 
                                  MinimumPrefixLength="1" ServiceMethod="GetlistaProductos" 
                                  UseContextKey="True"
                                  CompletionListCssClass="CompletionList" 
                                  CompletionListItemCssClass="CompletionlistItem" 
                                  CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10" >
                              </asp:AutoCompleteExtender>  
                          </div>
                 
                          <div class="col-6">
                            <label  class="form-label">Cantidad</label>                  
                            <asp:TextBox ID="tx_cantidadProducto" runat="server" class="form-control" type="text" Font-Size="Small"></asp:TextBox>
                          </div>

                          <div class="col-6">
                            <label  class="form-label">Tipo Solicitud</label>                  
                              <asp:DropDownList ID="dd_tipoSolicitud" class="btn btn-secondary dropdown-toggle" runat="server"  >
                               <asp:ListItem>VENTA</asp:ListItem>
                               <asp:ListItem>DEGUSTACION</asp:ListItem>
                               <asp:ListItem>MUESTRA</asp:ListItem>
                               <asp:ListItem>OTROS</asp:ListItem>
                           </asp:DropDownList>
                          </div>      
                         <div class="col-6">
                           <asp:CheckBox ID="cb_itemPackFerial" runat="server" />
                             <label  class="form-label">Item Pack Ferial</label> 
                         </div>

                          <div class="text-center">
                              <asp:Button ID="bt_limpiar" runat="server" class="btn btn-secondary" onclick="bt_limpiar_Click"  Text="Limpiar" />
                              <asp:Button ID="bt_adicionar" runat="server" class="btn btn-success"  onclick="bt_adicionar_Click"  Text="Adicionar" />
                              <asp:Button ID="bt_buscar" runat="server" class="btn btn-primary" onclick="bt_buscar_Click"  Text="Buscar" />
                              <asp:Button ID="bt_prueba" runat="server" class="btn btn-primary" onclick="bt_prueba_Click"  Text="Prueba" />
      
                          </div>
                        </div>
                </li>
                <li class="list-group-item">
                     <div class="row">
        <div class="col-12">              
              <div class="Grepuesto">
                  <asp:GridView ID="gv_Productos" runat="server" BackColor="White"
                      
                      BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                      Font-Size="X-Small" ForeColor="Black" GridLines="Vertical">
                      <Columns>
                              <asp:TemplateField HeaderText="Asignar">
                                  <ItemTemplate>
                                      <asp:CheckBox ID="CheckBox1" runat="server" />
                                  </ItemTemplate>
                              </asp:TemplateField>
                          </Columns>
                      <AlternatingRowStyle BackColor="#CCCCCC" />
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
        </div>
    </div>     
                </li>                
              </ul>            
          </div>
        </div>        
    </div>        
    <div class="row">
     <div class="col-12">
        <div class="card" >
          <div class="card-header bg-success text-white">
            Adicion de Producto
          </div>
          <ul class="list-group list-group-flush">
            <li class="list-group-item">
                <div class="row">
        <div class="col-3">
             <label class="form-label">Nro</label>
            <asp:TextBox ID="tx_nrodocumento" runat="server" class="form-control"></asp:TextBox>
        </div>
        <div class="col-12">
             <label class="form-label">Solicitante</label>
            <asp:TextBox ID="tx_solicitante" runat="server" class="form-control" ></asp:TextBox>
        </div>
        <div class="col-6">
             <label class="form-label">Fecha Entrega</label>
            <asp:TextBox ID="tx_fechaEntrega" runat="server" class="form-control"></asp:TextBox>
            <asp:CalendarExtender ID="tx_fechaEntrega_CalendarExtender" runat="server" 
                TargetControlID="tx_fechaEntrega">
            </asp:CalendarExtender>
        </div>
        <div class="col-6">
             <label class="form-label">Hora Entrega</label>
            <asp:TextBox ID="tx_horaEntrega" runat="server" class="form-control"></asp:TextBox>
        </div>

        <div class="text-left">
            <asp:Button ID="bt_guardar" runat="server" class="btn btn-success" Text="Guardar"   onclick="bt_guardar_Click" />
        </div>

    </div>
            </li>
            <li class="list-group-item">
                <div class="row">
        <div class="Gcotizacion">
            <asp:GridView ID="gv_adicionados" runat="server" BackColor="White" 
            
                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
                onrowcancelingedit="gv_adicionados_RowCancelingEdit" 
                onrowdeleting="gv_adicionados_RowDeleting" 
                onrowediting="gv_adicionados_RowEditing" 
                onrowupdating="gv_adicionados_RowUpdating">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />
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
    </div>
            </li>        
          </ul>
        </div>
      </div>
    </div>




















    <!--    Solicitud Pedido UPON    -->
    <!--
    <div class="row">
        <div class="col-md-12">
            <div class ="container-POSTPedido card">
                <div class="card-header bg-success text-white">
                  Solicitud de Pedido
                </div>
               
                <div class="row">
                    <!-- col 1 -->
                    <!--
                    <div class="col1-solicitudPedido col-lg-6 row">
                        <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                            <label class="form-label">Referencia:</label>
                            <asp:TextBox ID="txt_Referencia" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        </div>

                        <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                            <label class="form-label">Cliente:</label>
                            <asp:TextBox ID="txt_nomCliente" runat="server" CssClass="form-control" placeholder="Ingrese un nombre" autocomplete="off" OnTextChanged="txt_nomCliente_TextChanged"></asp:TextBox>

                        </div>

                        <div class="col-12 col-sm-6 col-md-6">
                            <label class="form-label">Telefono:</label>
                            <asp:TextBox ID="txt_telefono" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-md-6">
                            <label class="form-label">Correo Electronico:</label>
                            <asp:TextBox ID="txt_correoE" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    
                    <!-- COL 2 -->
    <!--
                    <div class="col2-solicitudPedido col-lg-6 row mb-2">

                        <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                            <label class="form-label">NIT:</label>
                            <asp:TextBox ID="txt_nit" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-md-6">
                            <label class="form-label">Razón social:</label>
                            <asp:TextBox ID="txt_razonsocial" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        </div>

                        <div class="col-12 col-sm-6 col-md-6">
                            <label class="form-label">Importe Descuentos:</label>
                            <asp:TextBox ID="txt_impDescuentos" runat="server" CssClass="form-control" Text="0,00" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-6 col-md-6">
                            <label class="form-label">Glosa:</label>
                            <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        </div>

                    </div>
                    <div class="container_gvClientesPedidos col-lg-6">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvPedidoClientes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnSelectedIndexChanged="gv_Clientes_SelectedIndexChanged">
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="true" HeaderText="Selecciona un cliente" SelectText="Seleccionar" />
                                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" SortExpression="NombreCompleto" />
                                        <asp:BoundField DataField="CodigoContacto" HeaderText="Codigo" SortExpression="CodigoContacto" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                              
                <br />
           
                <div class="container_addProductos border rounded">
                    <h5 runat="server"> Formulario de Productos </h5>
                    <div class="row mb-1 col-lg-12">
                        <div class="item_nomProducto col-lg-3">
                            <p class="item-name mb-1">Producto:</p>
                            <asp:TextBox ID="txt_nomProducto" runat="server" class="form-control mb-1" AutoPostBack="true" placeholder="Ingrese un nombre" AutoComplete="off" OnTextChanged="txt_nomProducto_TextChanged"></asp:TextBox>
                        </div>
                        <div class="item_cantidad col-lg-2">
                            <p class="item-name mb-1">Cantidad:</p>
                            <asp:TextBox ID="txt_cantProducto" runat="server" class="form-control" oninput="replaceDotWithComma(this)" AutoComplete="off" placeholder="Ingrese una cantidad"></asp:TextBox>
                        </div>
                        <div class="item_precioU col-lg-2">
                            <p class="item-name mb-1">Precio:</p>
                            <asp:TextBox ID="txt_precProducto" runat="server" class="form-control" aria-label="Card Holder" aria-describedby="basic-addon1" oninput="replaceDotWithComma(this)" AutoComplete="off"></asp:TextBox>
                        </div>
                        <div class="item_impDescProd col-lg-2">
                            <p class="item-name mb-1">Descuento:</p>
                            <asp:TextBox ID="txt_descProducto" runat="server" class="form-control" aria-label="Card Holder" aria-describedby="basic-addon1" Text="0,00" oninput="replaceDotWithComma(this)"></asp:TextBox>
                        </div>
                        <div class="item-btnAddProducto col-lg-3 align-items-center d-flex">
                            <asp:Button ID="btn_ADDproducto" runat="server" Text="Agregar Producto" CssClass="btn btn-dark btn-sm" />
                        </div>
                    </div>
                    <div class="table-responsive col-lg-8">
                        <asp:GridView ID="gv_PedidoGetProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnSelectedIndexChanged="gv_PedidoGetProductos_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" SortExpression="CodProd" />
                                <asp:BoundField DataField="Nombre" HeaderText="Producto" SortExpression="NomProd" />
                                <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="CUM" SortExpression="CodUM" />
                                <asp:BoundField DataField="CostoUnitario" HeaderText="Precio" SortExpression="Prec" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
          
                <div class="container_ListaProductosPedido">

                    <asp:GridView ID="gv_pedidoListaProductos" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" >
                        <Columns>
                          <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                          <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" SortExpression="CodigoProducto" />
                          <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" />
                          <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="CUMedida" SortExpression="CodigoUnidadMedida" />
                          <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio" SortExpression="PrecioUnitario" />
                          <asp:BoundField DataField="ImporteDescuento" HeaderText="Descuento" SortExpression="ImporteDescuento" />
                          <asp:BoundField DataField="ImporteTotal" HeaderText="Total" SortExpression="ImporteTotal" />
              
                          <asp:TemplateField>
                              <ItemTemplate>
                                  <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument ='<%# Eval("CodigoProducto") %>' CssClass="btn btn-danger btn-sm" />
                              </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <asp:Button ID="btn_PostPedido" runat="server" Text="Registrar Pedido " CssClass="btn btn-success btn-sm"/>
            </div>
                
                </div>

                </div>

            </div>
    
    -->
</asp:Content>
