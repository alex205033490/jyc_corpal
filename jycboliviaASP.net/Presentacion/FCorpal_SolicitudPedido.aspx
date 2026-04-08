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
            background-color: White;
            width: 200px;
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

        .container_card2 {
            margin: 0.5rem;
            padding: 0.5rem;
        }

        .gv_adicionados th {
            background-color: #acacac;
            color: white;
            border: 1px solid black;
            padding: 0.5rem;
        }

        .gv_adicionados td {
            padding: 5px;
        }

        .container_gvListProductos {
            height: 320px;
        }

        .gv_Productos td:nth-child(7) {
            background-color: #9aff98;
            font-weight: bold;
        }

        .gv_adicionados td:nth-child(8) {
            background-color: #5d5c5d4a;
            font-weight: bold;
        }

        .group_cb{
            box-shadow: 1px 1px 3px 0px darkgreen;
            border-radius: 2px;
            padding: 3px;
            background-color: #70c77212;
            font-weight: 700;
            color: seagreen;
        }

        .container_total{
            box-shadow: 0px 0px 5px 0px darkseagreen;
            padding: 3px;
            border-radius: 0.5rem;
            background-color: darkseagreen;
        }

        .lb_itemFraccionado{
            color: red;
            background-color: antiquewhite;
            font-size: 0.7rem;
        }
    </style>
    <script type="text/javascript">

        document.addEventListener("DOMContentLoaded", function () {

            var txtCliente = document.getElementById("<%= tx_cliente.ClientID %>");
                var txtProducto = document.getElementById("<%= tx_producto.ClientID %>");

                txtProducto.addEventListener("input", function () {
                    if (txtCliente.value.trim() === "") {
                        alert("Primero ingresa un cliente válido");
                        txtCliente.focus();
                    }
                });

            });

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header bg-success text-white">
                    Solicitud Producto
                </div>

                <ul class="list-group list-group-flush">
                    <li class="list-group-item">

                        <asp:UpdatePanel ID="updatePanel_Producto" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row col-lg-8 col-md-10 mb-2" style="font-size: small;">

                                    <div class="col-lg-5 col-md-6 col-sm-6 col-6">
                                        <div>

                                            <asp:Label runat="server" for="inputName5" class="form-label">Producto</asp:Label>
                                            <asp:TextBox ID="tx_producto" runat="server" class="form-control mb-2" Font-Size="Small" onkeyup="setClienteContextKey()"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="tx_producto_AutoCompleteExtender" runat="server"
                                                TargetControlID="tx_producto"
                                                CompletionSetCount="12"
                                                MinimumPrefixLength="1" ServiceMethod="GetlistaProductos"
                                                UseContextKey="True"
                                                CompletionListCssClass="CompletionList"
                                                CompletionListItemCssClass="CompletionlistItem"
                                                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10" 
                                                onClientItemSelected="ProductoSeleccionado">
                                            </asp:AutoCompleteExtender>

                                        </div>

                                        <div class="row col-lg-12">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <asp:Label runat="server" class="form-label">Cantidad</asp:Label>
                                                <asp:TextBox ID="tx_cantidadProducto" runat="server" class="form-control mb-2" Font-Size="Small" oninput="convertdotcomma(event)"></asp:TextBox>

                                                <div class="group_cb">
                                                    <asp:CheckBox  ID="cb_precioFraccionado" runat="server" />
                                                    <asp:Label runat="server" CssClass="form-label">Fraccionado</asp:Label>
                                                </div>
                                            </div>

                                            <div class="col-lg-6 col-md-6 col-sm-6" style="padding=0.5rem;">
                                                <asp:Label runat="server" class="form-label">Tipo Solicitud</asp:Label>
                                                <asp:DropDownList ID="dd_tipoSolicitud" class="form-select mb-2" Font-Size="X-Small" runat="server">
                                                    <asp:ListItem>VENTA</asp:ListItem>
                                                    <asp:ListItem>DEGUSTACION</asp:ListItem>
                                                    <asp:ListItem>MUESTRA</asp:ListItem>
                                                    <asp:ListItem>OTROS</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:CheckBox ID="cb_itemPackFerial" runat="server" />
                                                <asp:Label runat="server" class="form-label">Item Pack Ferial</asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lb_itemFraccionado" runat="server" Text="" CssClass="lb_itemFraccionado"></asp:Label>
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
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="bt_adicionar" EventName="click" />
                                <asp:AsyncPostBackTrigger ControlID="bt_buscar" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </li>

                    <!--------    GRIDVIEW BUSQUEDA PRODUCTO    ------->

                    <div class="container_gvProductos table-responsive col-lg-7">
                        <asp:UpdatePanel ID="updatePanel_solicitudProd" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:GridView ID="gv_Productos" runat="server" BackColor="White"
                                    BorderColor="#b4b4b4" BorderStyle="Ridge" BorderWidth="1px" CellPadding="7"
                                    Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="false"
                                    CssClass="gv_Productos table table-striped table-sticky" DataKeyNames="codigo, StockParcialAlmacen">
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
                                        <asp:BoundField DataField="precioFracc" HeaderText="Precio Fraccionado"/>
                                        <asp:BoundField DataField="StockParcialAlmacen" HeaderText="Stock Parcial" />
                                        <asp:BoundField DataField="stockAlmacen" HeaderText="Stock Almacen" HtmlEncode="false" />
                                        <asp:BoundField DataField="codcategoriap" HeaderText="ID categoria" />
                                        <asp:BoundField DataField="codupon" HeaderText="Codigo Upon" />
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
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="bt_buscar" EventName="click" />
                                <asp:AsyncPostBackTrigger ControlID="bt_adicionar" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

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


                    <div class="container_column1 row col-lg-5">
                        <asp:UpdatePanel ID="updatePanel_datosFactura" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row" style="font-size: small;">



                                    <div class="column1 col-lg-6 col-md-3 col-sm-4 col-6">
                                        <asp:Label runat="server" class="form-label" for="tx_nrodocumento">Nro</asp:Label>
                                        <asp:TextBox ID="tx_nrodocumento" runat="server" class="form-control mb-2"></asp:TextBox>

                                        <asp:Label runat="server" class="form-label" for="tx_fechaEntrega">Fecha Entrega</asp:Label>
                                        <asp:TextBox ID="tx_fechaEntrega" runat="server" class="form-control mb-2" AutoComplete="off"></asp:TextBox>
                                        <asp:CalendarExtender ID="tx_fechaEntrega_CalendarExtender" runat="server"
                                            TargetControlID="tx_fechaEntrega"></asp:CalendarExtender>

                                        <div class="group_verificar mb-2" style="box-shadow: 1px 0px 2px 0px #7f7d7d; padding: 3px; border: 1px solid #00000026;">
                                            <asp:HiddenField ID="hf_tipoCliente" runat="server"/>

                                            <asp:Label runat="server" class="form-label" for="tx_cliente">Tienda:</asp:Label>
                                            <asp:TextBox ID="tx_cliente" runat="server" class="form-control mb-1" Style="font-size: smaller;"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="tx_cliente_AutoCompleteExtender" runat="server"
                                                TargetControlID="tx_cliente"
                                                CompletionSetCount="12"
                                                MinimumPrefixLength="1" ServiceMethod="GetlistaClientes222"
                                                UseContextKey="True"
                                                CompletionListCssClass="CompletionList"
                                                CompletionListItemCssClass="CompletionlistItem"
                                                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                                                OnClientItemSelected="ClienteSeleccionado">
                                            </asp:AutoCompleteExtender>

                                            <asp:CheckBox ID="cb_actualizarCliente" for="bt_verificar" Text="Actualizar Tienda" runat="server" />

                                            <asp:Button ID="bt_verificar" CssClass="btn btn-info mb-2" runat="server" Text="Verificar" Font-Size="Smaller" OnClick="bt_verificar_Click" />

                                        </div>


                                        <asp:Label ID="Label2" for="tx_razonSocial" runat="server" Text="Datos de la Factura:"></asp:Label>
                                        <asp:TextBox ID="tx_razonSocial" CssClass="form-control mb-2" Font-Size="Smaller" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="column2 col-lg-6 col-md-3 col-sm-4 col-6">
                                        <asp:Label class="form-label" runat="server" for="tx_solicitante">Solicitante</asp:Label>
                                        <asp:TextBox ID="tx_solicitante" runat="server" class="form-control mb-2" Font-Size="Smaller"></asp:TextBox>

                                        <asp:Label runat="server" class="form-label" for="tx_horaEntrega">Hora Entrega</asp:Label>
                                        <asp:TextBox ID="tx_horaEntrega" runat="server" class="form-control mb-2" type="time" AutoComplete="off"></asp:TextBox>

                                        <asp:Label ID="Label1" for="tx_propietario" runat="server" Text="Propietario:"></asp:Label>
                                        <asp:TextBox ID="tx_propietario" CssClass="form-control mb-2" runat="server" Font-Size="Smaller"></asp:TextBox>

                                        <asp:Label runat="server" class="form-label">Metodo de Pago</asp:Label>
                                        <asp:DropDownList ID="dd_metodoPago" class="form-select mb-2" Font-Size="Small" runat="server" 
                                            OnSelectedIndexChanged="dd_metodoPago_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>

                                        <asp:TextBox ID="tx_diasCredito" runat="server" CssClass="form-control" placeholder="Dias de Credito" Visible="false"></asp:TextBox>

                                        <asp:Label ID="Label3" for="tx_nit" runat="server" Text="Nit:"></asp:Label>
                                        <asp:TextBox ID="tx_nit" CssClass="form-control mb-2" runat="server" Font-Size="Smaller" AutoComplete="off"></asp:TextBox>

                                    </div>
                                    <div class="col-lg-12">
                                        <asp:Button ID="bt_guardar" runat="server" class="btn btn-success" Text="Guardar" OnClick="bt_guardar_Click" />
                                    </div>

                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="bt_guardar" EventName="click" />
                                <asp:AsyncPostBackTrigger ControlID="bt_verificar" EventName="click" />
                                <asp:AsyncPostBackTrigger ControlID="bt_buscar" EventName="click" />
                                <asp:AsyncPostBackTrigger ControlID="bt_adicionar" EventName="click" />
                                <asp:AsyncPostBackTrigger ControlID="dd_metodoPago" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <div class="container_column2 col-lg-7 col-md-7 col-sm-10 col-12">
                        <div class="container_gvListProductos table-responsive">

                            <asp:UpdatePanel ID="updatePanel_GVadicionados" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:GridView ID="gv_adicionados" runat="server" BackColor="White"
                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical"
                                        OnRowCancelingEdit="gv_adicionados_RowCancelingEdit"
                                        OnRowDeleting="gv_adicionados_RowDeleting"
                                        OnRowEditing="gv_adicionados_RowEditing" AutoGenerateColumns="false"
                                        OnRowUpdating="gv_adicionados_RowUpdating" CssClass="gv_adicionados table-sticky"
                                        DataKeyNames="Medida">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:CommandField ShowDeleteButton="True" />

                                            <asp:BoundField DataField="Codigo" HeaderText="Codigo" HtmlEncode="false" />
                                            <asp:BoundField DataField="codupon" HeaderText="Cod Upon" />
                                            <asp:BoundField DataField="Producto" HeaderText="Producto" HtmlEncode="false" />
                                            <asp:BoundField DataField="Medida" HeaderText="Medida" HtmlEncode="false" />
                                            <asp:BoundField DataField="idcategoriap" HeaderText="ID categoria" />
                                            


                                            <asp:BoundField DataField="Precio" HeaderText="Precio" HtmlEncode="false" />
                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" HtmlEncode="false" />
                                            <asp:BoundField DataField="Descuento" HeaderText="Descuento (%)" />
                                            <asp:BoundField DataField="PrecioTotal" HeaderText="Total" HtmlEncode="false" />

                                            <asp:TemplateField HeaderText="Item Fraccionado">
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="cb_itemFraccionado" Checked= '<%# Eval("cb_itemFraccionado") %>' 
                                                        onclick="return false;" CssClass="switch-input"/>
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
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="bt_adicionar" EventName="click" />
                                    <asp:AsyncPostBackTrigger ControlID="bt_guardar" EventName="click" />
                                    
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                        <!--  CAMPO Total  -->
                        <asp:UpdatePanel ID="updatePanel_TXmostrarTotal" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-12 d-flex flex-column align-items-end">
                                    <div class="container_total col-3">
                                        <asp:Label runat="server">Total: </asp:Label>
                                        <asp:TextBox ID="tx_total" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="bt_adicionar" EventName="click"/>
                                <asp:AsyncPostBackTrigger ControlID="bt_guardar" EventName="click"/>
                                <asp:AsyncPostBackTrigger ControlID="gv_adicionados" EventName="RowDeleting"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="../js/mainCorpal.js"></script>
    <script type="text/javascript"> 
        
        function ClienteSeleccionado(source, eventArgs) {
            var nomCliente = eventArgs.get_text();
            PageMethods.obtenerCliente(nomCliente, function (resultado) {
                document.getElementById("<%=tx_propietario.ClientID%>").value = resultado.propietario;
                document.getElementById("<%=tx_nit.ClientID%>").value = resultado.nit;
                document.getElementById("<%=tx_razonSocial.ClientID%>").value = resultado.razonsocial;

                document.getElementById("<%= hf_tipoCliente.ClientID %>").value = resultado.tipoCliente;

                aplicarReglaFraccionado(resultado.tipoCliente);
            });
        }

        function aplicarReglaFraccionado(tipoCliente) {
            var chkFraccionado = document.getElementById("<%= cb_precioFraccionado.ClientID %>");
            var lbl = document.getElementById("<%= lb_itemFraccionado.ClientID %>");

            if (!chkFraccionado) return;

            if (tipoCliente == 7) {
                chkFraccionado.disabled = false;
                if (lbl) lbl.innerText = "";
            } else {
                chkFraccionado.checked = false;
                chkFraccionado.disabled = true;
                if (lbl) lbl.innerText = "No se permite productos fraccionados";
            }
        }


        function setClienteContextKey() {
            var inputCliente = document.getElementById('<%= tx_cliente.ClientID %>');
    
            if (!inputCliente) return;

            var cliente = inputCliente.value;

            if (!cliente) return; 

                    var autoComplete = $find('<%= tx_producto_AutoCompleteExtender.ClientID %>');

                    if (autoComplete) {
                        autoComplete.set_contextKey(cliente);
                    }
        }

        function ProductoSeleccionado(source, eventArgs) {
            var producto = eventArgs.get_text();

            document.getElementById("<%= tx_producto.ClientID %>").value = producto;
            
            __doPostBack('<%= bt_buscar.UniqueID %>', '');
        }

        Sys.Application.add_load(function () {
            var tipoCliente = document.getElementById("<%= hf_tipoCliente.ClientID %>").value;

            if (tipoCliente) {
                aplicarReglaFraccionado(tipoCliente);
            }
        })
    </script>

</asp:Content>

