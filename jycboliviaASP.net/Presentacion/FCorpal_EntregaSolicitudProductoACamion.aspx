<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true" MasterPageFile="~/PlantillaNew.Master" CodeBehind="FCorpal_EntregaSolicitudProductoACamion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_EntregaSolicitudProductoACamion_" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_CorpalEntregaProdCamion.css" rel="stylesheet" type="text/css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

                            <div class="container_buscarRegistro row col-lg-7">
                                <div class="col-lg-6">
                                    <label>Nro Solicitud:</label>
                                    <asp:TextBox ID="txt_nroSolicitud" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txt_nroSolicitud_AutoCompleteExtender" runat="server" TargetControlID="txt_nroSolicitud"
                                        CompletionSetCount="12"
                                        MinimumPrefixLength="1" ServiceMethod="getListNroBoletas"
                                        UseContextKey="True"
                                        CompletionListCssClass="CompletionList"
                                        CompletionListItemCssClass="CompletionlistItem"
                                        CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                                    </asp:AutoCompleteExtender>

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

                                <div class="col-lg-4">
                                    <asp:Button ID="btn_buscarRegistro" runat="server" CssClass="btn btn-dark" Text="Buscar" OnClick="btn_buscarRegistro_Click" />
                                    <asp:Button ID="btn_Limpiar" runat="server" CssClass="btn btn-info" Text="Limpiar Campos" OnClick="btn_Limpiar_Click" />
                                </div>

                            </div>

                            <!-- Cuadro Datos d Solicitud -->

                            <div class="row container_datosFactura mb-2 col-lg-4">

                                <!-- COL1 -->
                                <div class="container_EAsignacion col-lg-10">

                                    <label>Encargado de Asignación:</label>
                                    <asp:TextBox ID="txt_entregoProducto" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>


                                



                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gv_listRegistros" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                <!--CONTAINER BTN-->
                <div class="container_btn row mb-2 col-lg-6">
                    <div class="col-lg-6 btn_guardar">
                    </div>
                
                </div>

                <!-- GV LISTAS DE REGISTOS -->
                <asp:UpdatePanel ID="updatePanelBtnBuscar" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>


                <div class="container_listRegistros table-responsive col-lg-12">
                    <asp:GridView ID="gv_listRegistros" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClass="table table-striped" OnSelectedIndexChanged="gv_listRegistros_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="codRegistro" HeaderText="CodigoRegistro" />
                            <asp:BoundField DataField="nroboleta" HeaderText="Nro Boleta" HtmlEncode="false" />
                            <asp:BoundField DataField="FechaGra" HeaderText="Fecha Gra" />
                            <asp:BoundField DataField="horaGRA" HeaderText="Hora Gra" />
                            <asp:BoundField DataField="personalsolicitud" HeaderText="Personal Solicitante" HtmlEncode="false" />
                            <asp:BoundField DataField="estadosolicitud" HeaderText="Estado" HtmlEncode="false" />
                            <asp:BoundField DataField="producto" HeaderText="Producto" HtmlEncode="false" />
                            <asp:BoundField DataField="cant" HeaderText="Cantidad" />
                            <asp:BoundField DataField="cantEntregada" HeaderText="Cantidad Entregada" />
                            <asp:BoundField DataField="tiposolicitud" HeaderText="Tipo de Solicitud" HtmlEncode="false" />
                        </Columns>
                    </asp:GridView>
                </div>

                                            </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_buscarRegistro" EventName="Click"/>
                    </Triggers>
                </asp:UpdatePanel>
                <br />

                <!-- CONTAINER VEHICULOS -->
                <div class="container-vehiculos col-lg-10">
                    <div class="tittle_principal">
                        <h3>Lista de Vehiculos</h3>
                    </div>
                    <div class="add_vehiculos col-lg-8 row">
                        <div class="lista_vehiculos col-lg-8">
                            <label>Vehiculos</label>
                            <asp:DropDownList ID="dd_vehiculos" runat="server" CssClass="form-select" OnSelectedIndexChanged="dd_vehiculos_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="class_btn col-lg-4">
                            <asp:Button ID="btn_registrar" runat="server" CssClass="btn btn-success" Text="Agregar vehiculo a pedido" />
                        </div>
                    </div>
                </div>



                <!--DETALLE VEHICULO-->
                <asp:UpdatePanel ID="updatePanelVehiculo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="container_detalleVehiculo table-responsive col-lg-10">
                            <asp:GridView ID="gv_detVehiculo" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
                                <Columns>
                                    <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                                    <asp:BoundField DataField="producto" HeaderText="Producto" HtmlEncode="false" />
                                    <asp:BoundField DataField="cantSolici" HeaderText="Cantidad Solicitada" />
                                    <asp:BoundField DataField="tiposolicitud" HeaderText="Tipo Solicitud" HtmlEncode="false" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gv_listRegistros" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>


            </div>
        </div>







    </div>
</asp:Content>
