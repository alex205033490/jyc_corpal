<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master"
    AutoEventWireup="true"
    CodeBehind="FCorpal_SolicitudPedido.aspx.cs"
    Inherits="jycboliviaASP.net.Presentacion.FCorpal_SolicitudPedido"
    Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="../Styles/Style_adicionarRepuesto.css" rel="stylesheet" />

<style>
.CompletionList{
    padding:5px 0;
    margin:2px 0 0;
    height:150px;
    width:200px;
    background:white;
    cursor:pointer;
    border:1px solid;
    font-size:x-small;
    overflow:auto;
}

.CompletionListMighlightedItem{
    background:green;
    color:white;
}

.container_gvProductos{
    height:150px;
    overflow-y:auto;
    margin:2px;
}

.table-sticky th{
    position:sticky;
    top:0;
    background:#6b6f6d;
    color:white;
    z-index:100;
}

.container_gvListProductos{
    height:320px;
}
</style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<!-- ===================== CARD SOLICITUD ===================== -->

<div class="row">
<div class="col-12">

<div class="card shadow-sm">

<div class="card-header bg-success text-white fw-bold">
Solicitud Producto
</div>

<ul class="list-group list-group-flush">
<li class="list-group-item">

<asp:UpdatePanel ID="updatePanel_Producto" runat="server">
<ContentTemplate>

<div class="row g-3">

<div class="col-12">
<asp:Label runat="server" AssociatedControlID="tx_producto" CssClass="form-label">Producto</asp:Label>

<asp:TextBox ID="tx_producto" runat="server" CssClass="form-control form-control-sm" />

<asp:AutoCompleteExtender
    ID="autoProducto"
    runat="server"
    TargetControlID="tx_producto"
    ServiceMethod="GetlistaProductos"
    MinimumPrefixLength="1"
    CompletionSetCount="12"
    CompletionListCssClass="CompletionList"
    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem"/>
</div>

<div class="col-12 col-md-6 col-lg-4">
<asp:Label runat="server" CssClass="form-label">Cantidad</asp:Label>
<asp:TextBox ID="tx_cantidadProducto" runat="server"
    CssClass="form-control form-control-sm"
    oninput="convertdotcomma(event)" />
</div>

<div class="col-12 col-md-6 col-lg-4">
<asp:Label runat="server" CssClass="form-label">Tipo Solicitud</asp:Label>

<asp:DropDownList ID="dd_tipoSolicitud" runat="server"
    CssClass="form-select form-select-sm">

<asp:ListItem>VENTA</asp:ListItem>
<asp:ListItem>DEGUSTACION</asp:ListItem>
<asp:ListItem>MUESTRA</asp:ListItem>
<asp:ListItem>OTROS</asp:ListItem>

</asp:DropDownList>
</div>

<div class="col-12 col-md-6 col-lg-4 d-flex align-items-center mt-4">
<asp:CheckBox ID="cb_itemPackFerial" runat="server" CssClass="form-check-input" />
<asp:Label runat="server" AssociatedControlID="cb_itemPackFerial"
    CssClass="form-check-label ms-2">
Item Pack Ferial
</asp:Label>
</div>

</div>

<div class="mt-3 d-flex gap-2">
<asp:Button ID="bt_buscar" runat="server"
    CssClass="btn btn-primary btn-sm"
    Text="Buscar"
    OnClick="bt_buscar_Click"/>

<asp:Button ID="bt_adicionar" runat="server"
    CssClass="btn btn-success btn-sm"
    Text="Adicionar"
    OnClick="bt_adicionar_Click"/>

<asp:Button ID="bt_limpiar" runat="server"
    CssClass="btn btn-secondary btn-sm"
    Text="Limpiar"
    OnClick="bt_limpiar_Click"/>
</div>

</ContentTemplate>
</asp:UpdatePanel>

</li>


<!-- GRID PRODUCTOS -->

<li class="list-group-item">

<div class="table-responsive container_gvProductos">

<asp:GridView ID="gv_Productos"
    runat="server"
    CssClass="table table-striped table-bordered table-sm table-sticky"
    AutoGenerateColumns="False"
    DataKeyNames="StockParcialAlmacen">

<Columns>

<asp:TemplateField HeaderText="Asignar">
<ItemTemplate>
<asp:CheckBox ID="CheckBox1" runat="server"/>
</ItemTemplate>
</asp:TemplateField>

<asp:BoundField DataField="codigo" HeaderText="Código"/>
<asp:BoundField DataField="producto" HeaderText="Producto"/>
<asp:BoundField DataField="medida" HeaderText="Medida"/>
<asp:BoundField DataField="precio" HeaderText="Precio"/>
<asp:BoundField DataField="StockParcialAlmacen" HeaderText="Stock Parcial"/>
<asp:BoundField DataField="stockAlmacen" HeaderText="Stock Almacén"/>
<asp:BoundField DataField="StockPackFerial" HeaderText="Stock Pack Ferial"/>

</Columns>
</asp:GridView>

</div>

</li>
</ul>

</div>
</div>
</div>


<!-- ===================== CARD ADICION ===================== -->

<div class="row mt-3">
<div class="col-12">

<div class="card shadow-sm">

<div class="card-header bg-success text-white fw-bold">
Adición de Producto
</div>

<ul class="list-group list-group-flush">

<li class="list-group-item">

<div class="row g-3">

<div class="col-12 col-md-6">
<asp:Label runat="server" AssociatedControlID="tx_nrodocumento" CssClass="form-label">Nro</asp:Label>
<asp:TextBox ID="tx_nrodocumento" runat="server" CssClass="form-control form-control-sm"/>
</div>

<div class="col-12 col-md-6">
<asp:Label runat="server" AssociatedControlID="tx_fechaEntrega" CssClass="form-label">Fecha Entrega</asp:Label>

<asp:TextBox ID="tx_fechaEntrega" runat="server"
    CssClass="form-control form-control-sm"/>

<asp:CalendarExtender ID="calendarEntrega"
    runat="server"
    TargetControlID="tx_fechaEntrega"/>
</div>

<div class="col-12">
<asp:Label runat="server" AssociatedControlID="tx_cliente" CssClass="form-label">Tienda</asp:Label>

<asp:TextBox ID="tx_cliente" runat="server"
    CssClass="form-control form-control-sm mb-2"/>

<asp:AutoCompleteExtender
    ID="autoCliente"
    runat="server"
    TargetControlID="tx_cliente"
    ServiceMethod="GetlistaClientes222"
    MinimumPrefixLength="1"
    CompletionSetCount="12"
    CompletionListCssClass="CompletionList"
    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem"/>
</div>

<div class="col-12 col-md-6">
<asp:Label runat="server" AssociatedControlID="tx_solicitante" CssClass="form-label">Solicitante</asp:Label>
<asp:TextBox ID="tx_solicitante" runat="server" CssClass="form-control form-control-sm"/>
</div>

<div class="col-12 col-md-6">
<asp:Label runat="server" AssociatedControlID="tx_horaEntrega" CssClass="form-label">Hora Entrega</asp:Label>

<asp:TextBox ID="tx_horaEntrega"
    runat="server"
    CssClass="form-control form-control-sm"
    TextMode="Time"/>
</div>

<div class="col-12 col-md-6">
<asp:Label runat="server" AssociatedControlID="dd_metodoPago" CssClass="form-label">Método de Pago</asp:Label>
<asp:DropDownList ID="dd_metodoPago" runat="server" CssClass="form-select form-select-sm"/>
</div>

<div class="col-12">
<asp:Button ID="bt_guardar"
    runat="server"
    CssClass="btn btn-success btn-sm px-4"
    Text="Guardar"
    OnClick="bt_guardar_Click"/>
</div>

</div>

</li>

<li class="list-group-item">

<div class="table-responsive container_gvListProductos">

<asp:GridView ID="gv_adicionados"
    runat="server"
    CssClass="table table-striped table-bordered table-sm text-center"
    AutoGenerateColumns="False"
    DataKeyNames="Codigo"
    OnRowEditing="gv_adicionados_RowEditing"
    OnRowDeleting="gv_adicionados_RowDeleting"
    OnRowUpdating="gv_adicionados_RowUpdating"
    OnRowCancelingEdit="gv_adicionados_RowCancelingEdit">

<Columns>

<asp:CommandField ShowEditButton="True"/>
<asp:CommandField ShowDeleteButton="True"/>

<asp:BoundField DataField="Codigo" HeaderText="Código"/>
<asp:BoundField DataField="Producto" HeaderText="Producto"/>
<asp:BoundField DataField="Medida" HeaderText="Medida"/>
<asp:BoundField DataField="Tipo" HeaderText="Tipo"/>
<asp:BoundField DataField="Precio" HeaderText="Precio"/>
<asp:BoundField DataField="Cantidad" HeaderText="Cantidad"/>
<asp:BoundField DataField="PrecioTotal" HeaderText="Precio Total"/>
<asp:BoundField DataField="ItemPackFerial" HeaderText="Item Pack Ferial"/>

</Columns>
</asp:GridView>

</div>

</li>

</ul>

</div>
</div>
</div>

<script src="../js/mainCorpal.js"></script>

</asp:Content>
