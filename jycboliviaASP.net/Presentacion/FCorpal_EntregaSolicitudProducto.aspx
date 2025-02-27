<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_EntregaSolicitudProducto.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_EntregaSolicitudProducto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Styles/Style_EntregaProductosACamion.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card">
        <div class="card-header bg-warning text-black">Entrega de Solicitud Productos</div>
        <div class="container-form">

            <div class="form_buscarCar col-sm-6 col-md-5 col-lg-4 mb-2">
                <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="Vehiculo:"></asp:Label>
                <asp:DropDownList ID="dd_listVehiculo" Font-Size="Small" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="dd_listVehiculo_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="row mb-3">
                <div class="col-sm-6 col-md-5 col-lg-5">
                    <div class="row col-lg-12 mb-2">
                        <div class="form_fentrega col-lg-5">
                            <asp:Label ID="Label2" runat="server" Text="FechaEntrega :" Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="tx_fechaEngrega" class="form-control" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="tx_fechaEngrega_CalendarExtender" runat="server"
                                TargetControlID="tx_fechaEngrega"></asp:CalendarExtender>
                        </div>

                        <div class="form_hentrega col-lg-5">
                            <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Hora Entrega :"></asp:Label>
                            <asp:TextBox ID="tx_horaentrega" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row col-lg-12">
                        <div class="form_estado col-5 col-xs-5 col-sm-5 col-md-5 col-lg-4">
                            <asp:Label ID="Label1" runat="server" Font-Size="Small" CssClass="d-block" Text="Estado:"></asp:Label>
                            <asp:DropDownList ID="dd_estadoCierre" class="btn btn-secondary dropdown-toggle" runat="server">
                                <asp:ListItem>Abierto</asp:ListItem>
                                <asp:ListItem>Cerrado</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="form_motivoCierre col-5 col-xs-5 col-sm-5 col-md-5 col-lg-5">
                            <asp:Label ID="Label31" runat="server" Font-Size="Small" CssClass="d-block" Text="Motivo Cierre:"></asp:Label>
                            <asp:DropDownList ID="dd_motivoCierre" class="btn btn-secondary dropdown-toggle" runat="server">
                                <asp:ListItem>Ninguno</asp:ListItem>
                                <asp:ListItem>Vendedor Sin Espacio</asp:ListItem>
                                <asp:ListItem>Vendedor Redujo Solicitud</asp:ListItem>
                                <asp:ListItem>Sin Stock en Almacen</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6 col-md-5 col-lg-5">
                    <div class="form_solicitante col-8 col-lg-6">
                        <asp:Label ID="Label10" runat="server" Font-Size="Small"
                            Text="Solicitante del Producto:"></asp:Label>
                        <asp:TextBox ID="tx_SolicitanteProducto" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="form_encargadoEntrega col-8 col-lg-6">
                        <asp:Label runat="server" Font-Size="Small" Text="Encargado de entrega"></asp:Label>
                        <asp:TextBox ID="tx_entregoSolicitud" class="form-control" Style="background-color: #7080903b;" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>

            </div>

            <!-- CONTAINER BOTONES  -->
            <div class="container-btns col-sm-8 mb-3 col-md-7">
                <div class="form_boton">
                    <asp:Button ID="bt_actualizar" runat="server" class="btn btn-success" Text="Guardar" Width="100px"
                        OnClick="bt_actualizar_Click" />
                    <asp:Button ID="bt_limpiar" runat="server" class="btn btn-primary" Text="Limpiar"
                        OnClick="bt_limpiar_Click" />
                    <asp:Button ID="bt_verRecibo" runat="server" class="btn btn-warning"
                        Text="Ver Recibo" OnClick="bt_verRecibo_Click" />
                    <asp:Button ID="bt_eliminar" runat="server" class="btn btn-danger" Text="Anular"
                        OnClick="bt_eliminar_Click" />
                </div>

            </div>

            <asp:UpdatePanel ID="updatePanelDDUpdate" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

            <asp:UpdatePanel id="UpdatePanel1" runat="server">
                <ContentTemplate>


                    <div class="container-gvRegistros table-responsive mb-2">
                        <asp:GridView ID="gv_solicitudesProductos" runat="server" CssClass="table table-striped" AutoGenerateColumns="false"
                            DataKeyNames="codigo" OnRowCommand="gv_solicitudesProductos_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btn_addCantidad" runat="server" CssClass="btn btn-success" Text="Guardar"
                                            Font-Size="11px" CommandName="GuardarCantidad" CommandArgument="<%# Container.DataItemIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Anular">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbk_eliminar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" HtmlEncode="false" />
                                <asp:BoundField DataField="placa" HeaderText="Placa" HtmlEncode="false" />
                                <asp:BoundField DataField="conductor" HeaderText="Conductor" HtmlEncode="false" />
                                <asp:BoundField DataField="codigo" HeaderText="Codigo Registro" />
                                <asp:BoundField DataField="nroboleta" HeaderText="Nro Boleta" HtmlEncode="false" />
                                <asp:BoundField DataField="codproducto" HeaderText="Codigo Producto" />
                                <asp:BoundField DataField="producto" HeaderText="Producto" HtmlEncode="false" />

                                <asp:BoundField DataField="cantSolicitada" HeaderText="Cantidad Solicitada" />

                                <asp:TemplateField HeaderText="Cantidad Entregada" SortExpression="Cantidad Entregada">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_cantentregada" runat="server" Text='<%# Bind("cantEntregada") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cantidad a Entregar">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tx_cantidadEntregarOK" runat="server" BackColor="Yellow" Width="90px" autoComplete="off"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Stock Almacen">
                                    <ItemTemplate>
                                        <asp:Label id="lb_stockAlmacen" runat="server" Text='<%# Bind("StockAlmacen")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="fechaentrega" HeaderText="Fecha Entrega" />
                                <asp:BoundField DataField="horaentrega" HeaderText="Hora Entrega" />
                                <asp:BoundField DataField="personalsolicitud" HeaderText="Personal Solicitante" HtmlEncode="false" />
                                <asp:BoundField DataField="tiposolicitud" HeaderText="Tipo Solicitud" HtmlEncode="false" />
                                <asp:BoundField DataField="estadosolicitud" HeaderText="Estado Solicitud" HtmlEncode="false" />
                            </Columns>
                        </asp:GridView>
                    </div>
                                    </ContentTemplate>
            </asp:UpdatePanel>

                                    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dd_listVehiculo" EventName="SelectedIndexChanged"/>
                </Triggers>
            </asp:UpdatePanel>

        </div>

    </div>

</asp:Content>
