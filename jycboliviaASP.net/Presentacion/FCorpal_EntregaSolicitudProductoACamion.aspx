<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true" MasterPageFile="~/PlantillaNew.Master" CodeBehind="FCorpal_EntregaSolicitudProductoACamion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_EntregaSolicitudProductoACamion_" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_CorpalEntregaProdCamion.css" rel="stylesheet" type="text/css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            var table = $(".sticky-table");

            if (table.find("thead").length === 0) {
                table.prepend("<thead>" + table.find("tr:first").html() + "</thead>");
                table.find("tr:first").remove();
            }
        })
    </script>

    <div class="card">
        <div class="card-header bg-warning text-black">
            <h1>Entrega Solicitud de Productos a Camión</h1>
        </div>

        <div class="list-group list-group-flush">
            <div class="container_DatosSolicitud List-group-item">
                <!-- Cuadro busqueda Registro -->
                <asp:UpdatePanel ID="UpdatePanelContainer_F1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>


                        <div class="container_form1 row">

                            <div class="container_buscarRegistro row col-lg-12" style="margin: 10px">
                                <div class="col-lg-4 col-md-5 col-sm-6 col-11 mb-3">
                                    <div class="container_buscar mb-3">


                                        <div class="col-lg-5 mb-2">
                                            <label>Nro de boleta:</label>
                                            <asp:TextBox ID="txt_nroSolicitud" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txt_nroSolicitud_AutoCompleteExtender" runat="server" TargetControlID="txt_nroSolicitud"
                                                CompletionSetCount="12"
                                                MinimumPrefixLength="1" ServiceMethod="getListNroBoletas"
                                                UseContextKey="True"
                                                CompletionListCssClass="CompletionList"
                                                CompletionListItemCssClass="CompletionlistItem"
                                                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                                            </asp:AutoCompleteExtender>
                                        </div>

                                        <div class=" col-lg-9 mb-2">
                                            <label>Vendedor</label>
                                            <asp:TextBox ID="txt_SolicitanteProducto" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txt_SolicitanteProducto_AutoCompleteExtender" runat="server"
                                                TargetControlID="txt_SolicitanteProducto"
                                                CompletionSetCount="12"
                                                MinimumPrefixLength="1" ServiceMethod="getListPersonalSolicitante"
                                                UseContextKey="True"
                                                CompletionListCssClass="CompletionList"
                                                CompletionListItemCssClass="CompletionlistItem"
                                                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                                            </asp:AutoCompleteExtender>
                                        </div>

                                        <div class="col-lg-12 d-flex align-items-end">
                                            <div class="col-lg-5">
                                                <asp:Button ID="btn_buscarRegistro" runat="server" CssClass="btn btn-dark" Text="Buscar" OnClick="btn_buscarRegistro_Click" />
                                            </div>
                                            <div class="col-lg-5">
                                                <asp:Button ID="btn_Limpiar" runat="server" CssClass="btn btn-info" Text="Limpiar Campos" OnClick="btn_Limpiar_Click" />
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-lg-7">
                                        <label>Encargado de Asignación:</label>
                                        <asp:TextBox ID="txt_entregoProducto" ReadOnly="true" runat="server" BackColor="lightgray" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <!-- COL2 -->
                                <div class="container-vehiculos col-8 col-sm-5 col-md-6 col-lg-4">
                                    <div class="tittle_principal">
                                        <h3>Lista de Vehiculos</h3>
                                    </div>
                                    <div class="add_vehiculos col-lg-12 row">
                                        <div class="lista_vehiculos col-lg-8 mb-2">
                                            <asp:DropDownList ID="dd_vehiculos" Font-Size="Small" runat="server" CssClass="form-select" OnSelectedIndexChanged="dd_vehiculos_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="class_btn col-lg-4">
                                            <asp:Button ID="btn_registrar" runat="server" CssClass="btn btn-success" Text="Agregar vehiculo" OnClick="btn_registrar_Click" />
                                        </div>
                                    </div>

                                    <asp:UpdatePanel ID="updatePanelDDdetCar" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="container_detCar">
                                                <asp:GridView ID="gv_detCar" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="capacidad" HeaderText="Capacidad" />
                                                        <asp:BoundField DataField="medida" HeaderText="Medida" />
                                                        <asp:BoundField DataField="cargacajas" HeaderText="Capacidad Cajas" />
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dd_vehiculos" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </div>

                            </div>

                            <!-- Cuadro Datos d Solicitud -->


                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gv_listRegistros" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                <!--CONTAINER BTN-->

                <!-- GV LISTAS DE REGISTOS -->
                <asp:UpdatePanel ID="updatePanelBtnLimpiar" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>



                <asp:UpdatePanel ID="updatePanelBtnRegistrar" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="updatePanelBtnBuscar" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div class="container_listRegistros table-responsive col-lg-11" data-clientid="<%= gv_listRegistros.ClientID %>">
                                    <asp:GridView ID="gv_listRegistros" runat="server" ShowHeader="true" EnableViewState="true" AutoGenerateColumns="false" 
                                        CssClass="table table-striped sticky-table" OnSelectedIndexChanged="gv_listRegistros_SelectedIndexChanged">
                                        
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="codRegistro" HeaderText="Codigo Registro" />
                                            <asp:BoundField DataField="nroboleta" HeaderText="Nro Boleta" HtmlEncode="false" />
                                            <asp:BoundField DataField="FechaGra" HeaderText="Fecha Gra" />
                                            <asp:BoundField DataField="horaGRA" HeaderText="Hora Gra" />
                                            <asp:BoundField DataField="personalsolicitud" HeaderText="Personal Solicitante" HtmlEncode="false" />
                                            <asp:BoundField DataField="estadosolicitud" HeaderText="Estado" HtmlEncode="false" />
                                            <asp:BoundField DataField="codProducto" HeaderText="Codigo Producto" HtmlEncode="false" />
                                            <asp:BoundField DataField="producto" HeaderText="Producto" HtmlEncode="false" />
                                            <asp:BoundField DataField="cant" HeaderText="Cantidad" />
                                            <asp:BoundField DataField="cantEntregada" HeaderText="Cantidad Entregada" />
                                            <asp:BoundField DataField="tiposolicitud" HeaderText="Tipo de Solicitud" HtmlEncode="false" />
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_buscarRegistro" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_registrar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                                            </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_Limpiar" EventName="Click"/>
                    </Triggers>
                </asp:UpdatePanel>
                <br />

            </div>
        </div>

    </div>
    <script src="../js/mainCorpal.js"></script>
</asp:Content>
