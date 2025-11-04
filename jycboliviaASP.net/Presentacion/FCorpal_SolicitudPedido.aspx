<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_SolicitudPedido.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_SolicitudPedido" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_adicionarRepuesto.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .CompletionList {
            padding: 5px 0;
            margin: 2px 0 0;
            /*  position:absolute;  */
            height: 150px;
            width: 200px;
            background-color: White;
            cursor: pointer;
            border: solid;
            border-width: 1px;
            font-size: x-small;
            overflow: auto;
        }

        .CompletionlistItem {
            font-size: x-small;
        }

        .CompletionListMighlightedItem {
            background-color: Green;
            color: White;
            /* color: Lime;
           padding: 3px 20px;
            text-decoration: none;           
            background-repeat: repeat-x;
            outline: 0;*/
        }

        .style1 {
            height: 26px;
        }

        .container_gvProductos {
            height: 150px;
            overflow-y: auto;
            margin: 2px;
        }

        .table-sticky th {
            position: sticky !important;
            top: 0 !important;
            background-color: #6b6f6d;
            color: white !important;
            z-index: 100 !important;
            border: 1px solid white !important;
        }

        .container_card2{
            margin: 0.5rem;
            padding: 0.5rem;
        }

        .gv_adicionados th{
            background-color: #acacac;
            color: white;
            border: 1px solid black;
            padding: 0.5rem;
        }
        .gv_adicionados td {
            padding: 10px;
        }

        .container_gvListProductos{
            height: 320px;
        }

        .gv_Productos td:nth-child(6) {
            background-color: #9aff98;
            font-weight: bold;
        }

        .gv_adicionados td:nth-child(8){
            background-color: #5d5c5d4a;
            font-weight: bold;
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

                        <div class="row col-lg-8 col-md-10 mb-2" style="font-size: small;">
                            <div class="col-lg-5 col-md-6 col-sm-6 col-6">
                                <div>
                                    <asp:Label runat="server" for="inputName5" class="form-label">Producto</asp:Label>
                                    <asp:TextBox ID="tx_producto" runat="server" class="form-control mb-2" Font-Size="Small"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="tx_producto_AutoCompleteExtender" runat="server"
                                        TargetControlID="tx_producto"
                                        CompletionSetCount="12"
                                        MinimumPrefixLength="1" ServiceMethod="GetlistaProductos"
                                        UseContextKey="True"
                                        CompletionListCssClass="CompletionList"
                                        CompletionListItemCssClass="CompletionlistItem"
                                        CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                                    </asp:AutoCompleteExtender>

                                </div>
                                
                                <div class="row col-lg-12">
                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                        <asp:Label runat="server" class="form-label">Cantidad</asp:Label>
                                        <asp:TextBox ID="tx_cantidadProducto" runat="server" class="form-control mb-2" Font-Size="Small"></asp:TextBox>

                                        <asp:CheckBox ID="cb_itemPackFerial" runat="server" />
                                        <asp:Label runat="server" class="form-label">Item Pack Ferial</asp:Label>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-sm-6" style="padding=0.5rem;">
                                        <asp:Label runat="server" class="form-label">Tipo Solicitud</asp:Label>
                                        <asp:DropDownList ID="dd_tipoSolicitud" class="form-select mb-2" Font-Size="X-Small" runat="server">
                                            <asp:ListItem>VENTA</asp:ListItem>
                                            <asp:ListItem>DEGUSTACION</asp:ListItem>
                                            <asp:ListItem>MUESTRA</asp:ListItem>
                                            <asp:ListItem>OTROS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                
                            </div>



                            <div class="col-3 col-md-3 col-sm-4 col-6 d-flex flex-column align-items-center">
                                <asp:Button ID="bt_buscar" runat="server" class="btn btn-primary mb-1 w-100" OnClick="bt_buscar_Click" Text="Buscar" />
                                <asp:Button ID="bt_adicionar" runat="server" class="btn btn-success mb-1 w-100" OnClick="bt_adicionar_Click" Text="Adicionar" />
                                <asp:Button ID="bt_prueba" runat="server" class="btn btn-primary mb-1 w-100" OnClick="bt_prueba_Click" Text="Prueba" />
                                <asp:Button ID="bt_limpiar" runat="server" class="btn btn-secondary mb-1 w-100" OnClick="bt_limpiar_Click" Text="Limpiar" />
                            </div>

                        </div>

                        <div class="text-center col-lg-8 col-md-8 col-sm-10 col-12">
                            

                        </div>
                    </li>

                    <!--------    GRIDVIEW BUSQUEDA PRODUCTO    ------->
                    <li class="list-group-item">

                        <div class="container_gvProductos table-responsive col-lg-7">
                            <asp:GridView ID="gv_Productos" runat="server" BackColor="White"
                                BorderColor="#b4b4b4" BorderStyle="Ridge" BorderWidth="1px" CellPadding="7"
                                Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="false"
                                CssClass="gv_Productos table table-striped table-sticky" DataKeyNames="StockParcialAlmacen">
                                <Columns>
                                    <asp:TemplateField HeaderText="Asignar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="codigo" HeaderText="Codigo" HtmlEncode="false" />
                                    <asp:BoundField DataField="producto" HeaderText="Producto" HtmlEncode="false" />
                                    <asp:BoundField DataField="medida" HeaderText="Medida" HtmlEncode="false" />
                                    <asp:BoundField DataField="precio" HeaderText="Precio" HtmlEncode="false" />
                                    <asp:BoundField DataField="StockParcialAlmacen" HeaderText="Stock Parcial" />
                                    <asp:BoundField DataField="stockAlmacen" HeaderText="Stock Almacen" HtmlEncode="false" />
                                    <asp:BoundField DataField="StockPackFerial" HeaderText="Stock Pack Ferial" HtmlEncode="false" />
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

                    </li>
                </ul>

            </div>
        </div>
    </div>

    <!-------------------------------------------------------- CARD 2 ------------------------------------------------------------------>

    <div class="card col-lg-12">
        <div class="card-header bg-success text-white">
            Adición de Producto
        </div>
        <div class="container_card2">
            <div class="container_adicionProductos">

                <div class="container_columns row col-lg-12">

                    <div class="container_column1 row col-lg-5" style="font-size: small;">

                        <div class="column1 col-lg-6 col-md-3 col-sm-4 col-6">
                            <asp:label runat="server" class="form-label" for="tx_nrodocumento">Nro</asp:label>
                            <asp:TextBox ID="tx_nrodocumento" runat="server" class="form-control mb-2"></asp:TextBox>

                            <asp:label runat="server" class="form-label" for="tx_fechaEntrega">Fecha Entrega</asp:label>
                            <asp:TextBox ID="tx_fechaEntrega" runat="server" class="form-control mb-2"></asp:TextBox>
                            <asp:CalendarExtender ID="tx_fechaEntrega_CalendarExtender" runat="server"
                                TargetControlID="tx_fechaEntrega"></asp:CalendarExtender>

                            <div class="group_verificar mb-2" style="box-shadow: 1px 0px 2px 0px #7f7d7d;padding: 3px; border: 1px solid #00000026;">
                                <asp:label runat="server" class="form-label" for="tx_cliente">Tienda:</asp:label>
                                <asp:TextBox ID="tx_cliente" runat="server" class="form-control mb-1" style="font-size: smaller;"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="tx_cliente_AutoCompleteExtender" runat="server"
                                    TargetControlID="tx_cliente"
                                    CompletionSetCount="12"
                                    MinimumPrefixLength="1" ServiceMethod="GetlistaClientes222"
                                    UseContextKey="True"
                                    CompletionListCssClass="CompletionList"
                                    CompletionListItemCssClass="CompletionlistItem"
                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                                </asp:AutoCompleteExtender> 

                                <asp:CheckBox ID="cb_actualizarCliente" for="bt_verificar" Text="Actualizar Tienda" runat="server" />
                                <asp:Button ID="bt_verificar" CssClass="btn btn-info mb-2" runat="server" Text="Verificar" Font-Size="Smaller" OnClick="bt_verificar_Click" />
                            </div>

                           
                            <asp:Label ID="Label2" for="tx_razonSocial" runat="server" Text="Razon Social:"></asp:Label>
                            <asp:TextBox ID="tx_razonSocial" CssClass="form-control mb-2" Font-Size="Smaller" runat="server"></asp:TextBox>
                        </div>

                        <div class="column2 col-lg-6 col-md-3 col-sm-4 col-6">
                            <asp:label class="form-label" runat="server" for="tx_solicitante">Solicitante</asp:label>
                            <asp:TextBox ID="tx_solicitante" runat="server" class="form-control mb-2" Font-Size="Smaller"></asp:TextBox>

                            <asp:label runat="server" class="form-label" for="tx_horaEntrega">Hora Entrega</asp:label>
                            <asp:TextBox ID="tx_horaEntrega" runat="server" class="form-control mb-2"></asp:TextBox>

                            <asp:Label ID="Label1" for="tx_propietario" runat="server" Text="Propietario:"></asp:Label>
                            <asp:TextBox ID="tx_propietario" CssClass="form-control mb-2" runat="server" Font-Size="Smaller"></asp:TextBox>

                            <asp:Label runat="server" class="form-label">Metodo de Pago</asp:Label>
                            <asp:DropDownList ID="dd_metodoPago" class="form-select mb-2" Font-Size="Small" runat="server">
                            </asp:DropDownList>

                            <asp:Label ID="Label3" for="tx_nit" runat="server" Text="Nit:"></asp:Label>
                            <asp:TextBox ID="tx_nit" CssClass="form-control mb-2" runat="server" Font-Size="Smaller"></asp:TextBox>
                        
                        </div>
                        <div class="col-lg-12">
                            <asp:Button ID="bt_guardar" runat="server" class="btn btn-success" Text="Guardar" OnClick="bt_guardar_Click" />
                        </div>


                    </div>

                    <div class="container_column2 col-lg-7 col-md-7 col-sm-10 col-12">
                        <div class="container_gvListProductos table-responsive">
                            <asp:GridView ID="gv_adicionados" runat="server" BackColor="White"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                Font-Size="X-Small" ForeColor="Black" GridLines="Vertical"
                                OnRowCancelingEdit="gv_adicionados_RowCancelingEdit"
                                OnRowDeleting="gv_adicionados_RowDeleting"
                                OnRowEditing="gv_adicionados_RowEditing" AutoGenerateColumns="false"
                                OnRowUpdating="gv_adicionados_RowUpdating" CssClass="gv_adicionados table-sticky">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />

                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" HtmlEncode="false"/>
                                    <asp:BoundField DataField="Producto" HeaderText="Producto" HtmlEncode="false"/>
                                    <asp:BoundField DataField="Medida" HeaderText="Medida" HtmlEncode="false"/>
                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" HtmlEncode="false"/>
                                    <asp:BoundField DataField="Precio" HeaderText="Precio" HtmlEncode="false"/>
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" HtmlEncode="false"/>
                                    <asp:BoundField DataField="PrecioTotal" HeaderText="Precio Total" HtmlEncode="false"/>
                                    <asp:BoundField DataField="ItemPackFerial" HeaderText="Item Pack Ferial" HtmlEncode="false"/>

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
                </div>






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
                            <asp:TextBox ID="txt_nomCliente" runat="server" CssClass="form-control" placeholder="Ingrese un nombre" autocomplete="off" ></asp:TextBox>

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
                                <asp:GridView ID="gvPedidoClientes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" >
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
                            <asp:TextBox ID="txt_nomProducto" runat="server" class="form-control mb-1" AutoPostBack="true" placeholder="Ingrese un nombre" AutoComplete="off" ></asp:TextBox>
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
                        <asp:GridView ID="gv_PedidoGetProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" >
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
