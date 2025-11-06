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
        <div class="card shadow-sm">
            <div class="card-header bg-success text-white fw-bold">
                Solicitud Producto
            </div>

            <ul class="list-group list-group-flush">
                <li class="list-group-item">
                    <!-- Fila principal -->
                    <div class="row g-3">
                        <!-- PRODUCTO -->
                        <div class="col-12">
                            <asp:Label runat="server" AssociatedControlID="tx_producto" CssClass="form-label">Producto</asp:Label>
                            <asp:TextBox ID="tx_producto" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <asp:AutoCompleteExtender 
                                ID="tx_producto_AutoCompleteExtender" 
                                runat="server"
                                TargetControlID="tx_producto"
                                CompletionSetCount="12"
                                MinimumPrefixLength="1" 
                                ServiceMethod="GetlistaProductos"
                                UseContextKey="True"
                                CompletionListCssClass="CompletionList"
                                CompletionListItemCssClass="CompletionlistItem"
                                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem"
                                CompletionInterval="10">
                            </asp:AutoCompleteExtender>
                        </div>

                        <!-- CANTIDAD -->
                        <div class="col-12 col-md-6 col-lg-4">
                            <asp:Label runat="server" CssClass="form-label">Cantidad</asp:Label>
                            <asp:TextBox 
                                ID="tx_cantidadProducto" 
                                runat="server" 
                                CssClass="form-control form-control-sm"
                                oninput="convertdotcomma(event)">
                            </asp:TextBox>
                        </div>

                        <!-- TIPO SOLICITUD -->
                        <div class="col-12 col-md-6 col-lg-4">
                            <asp:Label runat="server" CssClass="form-label">Tipo Solicitud</asp:Label>
                            <asp:DropDownList 
                                ID="dd_tipoSolicitud" 
                                runat="server" 
                                CssClass="form-select form-select-sm">
                                <asp:ListItem>VENTA</asp:ListItem>
                                <asp:ListItem>DEGUSTACION</asp:ListItem>
                                <asp:ListItem>MUESTRA</asp:ListItem>
                                <asp:ListItem>OTROS</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <!-- ITEM PACK FERIAL -->
                        <div class="col-12 col-md-6 col-lg-4 d-flex align-items-center mt-4">
                            <div class="form-check mb-0">
                                <asp:CheckBox ID="cb_itemPackFerial" runat="server" CssClass="form-check-input" />
                                <asp:Label runat="server" CssClass="form-check-label ms-1">Item Pack Ferial</asp:Label>
                            </div>
                        </div>
                    </div>

                    <!-- Fila de botones -->
                    <div class="row mt-3">
                        <div class="col-12 d-flex flex-wrap justify-content-start align-items-center gap-2">
                            <asp:Button ID="bt_buscar" runat="server" CssClass="btn btn-primary btn-sm" Text="Buscar" OnClick="bt_buscar_Click" />
                            <asp:Button ID="bt_adicionar" runat="server" CssClass="btn btn-success btn-sm" Text="Adicionar" OnClick="bt_adicionar_Click" />
                            <asp:Button ID="bt_limpiar" runat="server" CssClass="btn btn-secondary btn-sm" Text="Limpiar" OnClick="bt_limpiar_Click" />
                        </div>
                    </div>
                </li>

                <!-- GRIDVIEW -->
                <li class="list-group-item">
                    <div class="table-responsive">
                        <asp:GridView 
                            ID="gv_Productos" 
                            runat="server"
                            CssClass="table table-striped table-bordered table-sm text-center align-middle"
                            AutoGenerateColumns="False"
                            DataKeyNames="StockParcialAlmacen"
                            Font-Size="Small"
                            GridLines="None">
                            <Columns>
                                <asp:TemplateField HeaderText="Asignar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="codigo" HeaderText="Código" />
                                <asp:BoundField DataField="producto" HeaderText="Producto" />
                                <asp:BoundField DataField="medida" HeaderText="Medida" />
                                <asp:BoundField DataField="precio" HeaderText="Precio" />
                                <asp:BoundField DataField="StockParcialAlmacen" HeaderText="Stock Parcial" />
                                <asp:BoundField DataField="stockAlmacen" HeaderText="Stock Almacén" />
                                <asp:BoundField DataField="StockPackFerial" HeaderText="Stock Pack Ferial" />
                            </Columns>
                            <HeaderStyle CssClass="bg-dark text-white" />
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </div>
    </div>
</div>

    
    <!-------------------------------------------------------- CARD 2 ------------------------------------------------------------------>


  <div class="row">
  <div class="col-12">
      <div class="card shadow-sm">
            <div class="card-header bg-success text-white fw-bold">
                Adición de Producto
            </div>

      <ul class="list-group list-group-flush">
      <li class="list-group-item">
            <!-- Fila principal -->
            <div class="row g-3">
             <!-- NRO DOCUMENTO -->
                 <div class="col-12 col-md-6">
                     <asp:Label runat="server" AssociatedControlID="tx_nrodocumento" CssClass="form-label">Nro</asp:Label>
                     <asp:TextBox ID="tx_nrodocumento" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                 </div>

                 <!-- FECHA ENTREGA -->
                 <div class="col-12 col-md-6">
                     <asp:Label runat="server" AssociatedControlID="tx_fechaEntrega" CssClass="form-label">Fecha Entrega</asp:Label>
                     <asp:TextBox ID="tx_fechaEntrega" runat="server" CssClass="form-control form-control-sm" AutoComplete="off"></asp:TextBox>
                     <asp:CalendarExtender ID="tx_fechaEntrega_CalendarExtender" runat="server" TargetControlID="tx_fechaEntrega"></asp:CalendarExtender>
                 </div>

                 <!-- TIENDA -->
                 <div class="col-12">
                     <div class="p-2 border rounded" style="background-color:#f8f9fa;">
                         <asp:Label runat="server" AssociatedControlID="tx_cliente" CssClass="form-label">Tienda</asp:Label>
                         <asp:TextBox ID="tx_cliente" runat="server" CssClass="form-control form-control-sm mb-2"></asp:TextBox>
                         <asp:AutoCompleteExtender 
                             ID="tx_cliente_AutoCompleteExtender" 
                             runat="server"
                             TargetControlID="tx_cliente"
                             CompletionSetCount="12"
                             MinimumPrefixLength="1" 
                             ServiceMethod="GetlistaClientes222"
                             UseContextKey="True"
                             CompletionListCssClass="CompletionList"
                             CompletionListItemCssClass="CompletionlistItem"
                             CompletionListHighlightedItemCssClass="CompletionListMighlightedItem"
                             CompletionInterval="10">
                         </asp:AutoCompleteExtender>

                         <div class="d-flex align-items-center flex-wrap gap-2">
                             <asp:CheckBox ID="cb_actualizarCliente" runat="server" CssClass="form-check-input" />
                             <asp:Label runat="server" AssociatedControlID="cb_actualizarCliente" CssClass="form-check-label me-3">Actualizar Tienda</asp:Label>
                             <asp:Button ID="bt_verificar" runat="server" CssClass="btn btn-info btn-sm" Text="Verificar" Font-Size="Smaller" OnClick="bt_verificar_Click" />
                         </div>
                     </div>
                 </div>

                 <!-- RAZÓN SOCIAL -->
                 <div class="col-12">
                     <asp:Label runat="server" AssociatedControlID="tx_razonSocial" CssClass="form-label">Razón Social</asp:Label>
                     <asp:TextBox ID="tx_razonSocial" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                 </div>

                 <!-- SOLICITANTE -->
                 <div class="col-12 col-md-6">
                     <asp:Label runat="server" AssociatedControlID="tx_solicitante" CssClass="form-label">Solicitante</asp:Label>
                     <asp:TextBox ID="tx_solicitante" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                 </div>

                 <!-- HORA ENTREGA -->
                 <div class="col-12 col-md-6">
                     <asp:Label runat="server" AssociatedControlID="tx_horaEntrega" CssClass="form-label">Hora Entrega</asp:Label>
                     <asp:TextBox ID="tx_horaEntrega" runat="server" CssClass="form-control form-control-sm" type="time" AutoComplete="off"></asp:TextBox>
                 </div>

                 <!-- PROPIETARIO -->
                 <div class="col-12 col-md-6">
                     <asp:Label runat="server" AssociatedControlID="tx_propietario" CssClass="form-label">Propietario</asp:Label>
                     <asp:TextBox ID="tx_propietario" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                 </div>

                 <!-- MÉTODO DE PAGO -->
                 <div class="col-12 col-md-6">
                     <asp:Label runat="server" AssociatedControlID="dd_metodoPago" CssClass="form-label">Método de Pago</asp:Label>
                     <asp:DropDownList ID="dd_metodoPago" runat="server" CssClass="form-select form-select-sm"></asp:DropDownList>
                 </div>

                 <!-- NIT -->
                 <div class="col-12 col-md-6">
                     <asp:Label runat="server" AssociatedControlID="tx_nit" CssClass="form-label">NIT</asp:Label>
                     <asp:TextBox ID="tx_nit" runat="server" CssClass="form-control form-control-sm" AutoComplete="off"></asp:TextBox>
                 </div>

                 <!-- BOTÓN GUARDAR -->
                 <div class="col-12 ">
                     <asp:Button ID="bt_guardar" runat="server" CssClass="btn btn-success btn-sm px-4" Text="Guardar" OnClick="bt_guardar_Click" />
                 </div>
            </div>
        </li>
          <li class="list-group-item">
              <div class="row g-3">
                    <!-- COLUMNA DERECHA -->
  <div class="col-12 col-lg-12">
      <div class="table-responsive">
          <asp:GridView 
              ID="gv_adicionados" 
              runat="server"
              CssClass="table table-striped table-bordered table-sm text-center align-middle"
              Font-Size="Small"
              AutoGenerateColumns="False"
              OnRowCancelingEdit="gv_adicionados_RowCancelingEdit"
              OnRowDeleting="gv_adicionados_RowDeleting"
              OnRowEditing="gv_adicionados_RowEditing"
              OnRowUpdating="gv_adicionados_RowUpdating"
              DataKeyNames="Codigo">
              <Columns>
                  <asp:CommandField ShowEditButton="True" />
                  <asp:CommandField ShowDeleteButton="True" />
                  <asp:BoundField DataField="Codigo" HeaderText="Código" />
                  <asp:BoundField DataField="Producto" HeaderText="Producto" />
                  <asp:BoundField DataField="Medida" HeaderText="Medida" />
                  <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                  <asp:BoundField DataField="Precio" HeaderText="Precio" />
                  <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                  <asp:BoundField DataField="PrecioTotal" HeaderText="Precio Total" />
                  <asp:BoundField DataField="ItemPackFerial" HeaderText="Item Pack Ferial" />
              </Columns>
              <HeaderStyle CssClass="bg-dark text-white" />
          </asp:GridView>
      </div>
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
<script type="text/javascript" src="../js/mainCorpal.js"></script>
</asp:Content>


