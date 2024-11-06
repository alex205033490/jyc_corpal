<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" CodeBehind="FCorpal_AgregarInsumoCreado.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_AgregarInsumoCreado" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="" style="padding-top: 0em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">
                    <script>
                        // selected 1
                        function onItemSelectedInsumoC1(sender, args) {
                            console.log("Item selected");
                            var dataItem = args.get_value();
                            console.log("dataItem");
                            var parts = dataItem.split("|");

                            if (parts.length > 1) {
                                var codigo = parts[0];
                                var nombre = parts[1];
                                var medida = parts[2];

                                document.getElementById('<%= txt_Nombre.ClientID %>').value = nombre;
                                document.getElementById('<%= txt_codInsumo.ClientID %>').value = codigo;
                                document.getElementById('<%= txt_Medida.ClientID %>').value = medida;

                            }
                        }
                        // selected 2
                        function onItemSelectedInsumoC2(sender, args) {
                            console.log("Item selected");
                            var dataItem = args.get_value();
                            console.log("dataItem");
                            var parts = dataItem.split("|");

                            if (parts.length > 1) {
                                var codigo = parts[0];
                                var nombre = parts[1];
                                var medida = parts[2];

                                document.getElementById('<%= txt_MInsumoNombre.ClientID %>').value = nombre;
                                document.getElementById('<%= txt_MInsumoCodigo.ClientID %>').value = codigo;
                                document.getElementById('<%= txt_MInsumoMedida.ClientID %>').value = medida;
                            }
                        }
                    </script>

                    <!------------------------          FORMULARIO INSERTAR INSUMOCREADO       ------------------------------>
                    <div class="container-ICreadoADD p-2 bg-light rounded ">
                        <div class="container_tittle col-md-12 col-lg-12 mb-4 rounded">
                            <h2 class="text_tittle2 rounded p-3">Registro Nuevo InsumoCreado</h2>
                        </div>
                        
                        <asp:Panel ID="Panel_INSERTIC" runat="server" DefaultButton="btn_registrarICreado2">
                        <div class="row mb-3 col-xs-12 col-sm-12 col-md-12 col-lg-12 row">

                            <div class="col-xs-6 col-sm-5 col-md-5 col-lg-3">
                                <label class="form-label">Nombre Insumo:</label>
                                <asp:TextBox ID="txt_nomInsumoCreado" Font-Size="Small" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Ingrese un nombre" ></asp:TextBox>
                            </div>
                            <div class="col-xs-6 col-sm-4 col-md-3 col-lg-2">
                                <label class="form-label">Medida:</label>
                                <asp:TextBox ID="txt_medidaInsumoCreado" Font-Size="Small" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Ingrese una medida"></asp:TextBox>
                            </div>
                            <div class="container-btn d-flex col-xs-12 col-sm-3 col-md-3 col-lg-2 align-items-end">
                                <asp:Button ID="btn_registrarICreado2" runat="server" Text="Registrar" CssClass="btn btn-success" OnClick="btn_registrarICreado2_Click" />
                            </div>
                        </div>
                            </asp:Panel>
                        <!-- ADD formulario Insumos  -->
                        <asp:Panel ID="Panel_ADDInsumo2" runat="server" DefaultButton="btn_ADD">

                        <div class="form_insumo col-xs-12 col-sm-11 col-md-8 col-lg-7">
                            <h5 class="form-label">Lista de Insumos</h5>
                            <div class="container_insumos">
                                <table id="tblAddInsumo" class="table table-bordered table-striped col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                    <thead>
                                        <tr>
                                            <th class="custom-width-number">Codigo</th>
                                            <th class="custom-width-text">Nombre</th>
                                            <th class="custom-width-number">Cantidad</th>
                                            <th class="custom-width-number">Medida</th>
                                            <th class="custom-width-number">Accion</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txt_codInsumo" runat="server" CssClass="form-control" Font-Size="Small" AutoComplete="off" Wrap="False"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_Nombre" runat="server" CssClass="form-control" Placeholder="Ingreso un nombre" Font-Size="Small" OnTextChanged="txt_Nombre_TextChanged"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                    TargetControlID="txt_Nombre" CompletionSetCount="12" MinimumPrefixLength="1"
                                                    ServiceMethod="GetListaInsumo" UseContextKey="True" CompletionListCssClass="CompletionList"
                                                    CompletionListItemCssClass="CompletionlistItem"
                                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem"
                                                    CompletionInterval="10" OnClientItemSelected="onItemSelectedInsumoC1">
                                                </asp:AutoCompleteExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_Cantidad" runat="server" CssClass="form-control" Font-Size="Small" TextMode="Number" step="0.0001"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_Medida" runat="server" CssClass="form-control" Font-Size="Small" AutoComplete="off"></asp:TextBox></td>
                                            <td>
                                                <asp:Button ID="btn_ADD" runat="server" Text="añadir" CssClass="btn btn-success" OnClick="btn_ADD_Click" />

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                            </asp:Panel>

                        <!-- Lista GRIDVIEW-->

                        <div class="container_gv col-xs-12 col-sm-10 col-md-9 col-lg-8">
                            <asp:GridView runat="server" ID="gv_insumoCreado" CssClass="gridview" AutoGenerateColumns="false" Font-Size="Small" OnRowCommand="gv_insumoCreado_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="CodInsumo" HeaderText="CodInsumo" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" DataFormatString="{0:F2}" />
                                    <asp:BoundField DataField="Medida" HeaderText="Medida" />
                                    <asp:TemplateField HeaderText="Accion">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminarFila" runat="server" Font-Size="Small" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' Text="Eliminar" CssClass="btn btn-danger" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                    <br />


                    <!------------------------          FORMULARIO MODIFICAR INSUMO CREADO          ------------------------------>
                    <div class="container-ICreadoUpdate p-4 bg-light rounded">
                        <div class="container_tittle col-md-12 col-lg-12 mb-4 rounded">
                            <h2 class="text_tittle2 rounded p-3 rounded">Modificar Insumo Creado</h2>
                        </div>

                        <div class="container_form mb-3 col-sm-12 col-md-12 col-lg-12">
                            
                            <!-- Buscar InsumoCreado -->
                                <asp:Panel ID="Panel_BuscarIC" runat="server" DefaultButton="btn_BuscarInsumoCreado">
                            <div class="form_buscar col-md-12 col-lg-5 mb-3 d-flex align-items-center">
                                    <asp:TextBox ID="txt_MInsumoCreado" runat="server" CssClass="form-control custom-width-textBuscardor me-2" placeholder="Ingrese un nombre de insumoCreado" AutoComplete="off" class="col-3 me-2"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                        TargetControlID="txt_MInsumoCreado" CompletionSetCount="12" MinimumPrefixLength="1"
                                        ServiceMethod="GetListaInsumoCreado" UseContextKey="True" CompletionListCssClass="CompletionList"
                                        CompletionListItemCssClass="CompletionlistItem"
                                        CompletionListHighlightedItemCssClass="CompletionListMighlightedItem"
                                        CompletionInterval="10">
                                    </asp:AutoCompleteExtender>
                                    <asp:Button ID="btn_BuscarInsumoCreado" runat="server" Text="Buscar" CssClass="btn btn-dark me-2" OnClick="btn_BuscarInsumoCreado_Click" />
                                    <asp:Button ID="btn_limpiar" runat="server" Text="Limpiar" CssClass="btn btn-info" OnClick="btn_limpiar_Click" />
                            </div>
                                </asp:Panel>

                            <!--  formulario para agregar insumo nuevo  -->
                            <asp:Panel ID="Panel_ADDINuevo" runat="server" DefaultButton="btn_InsertarInsumo">
                            <div class="container-ICreadoModf1 col-xs-12 col-sm-11 col-md-8 col-lg-7">
                                <h5 class="form-label">Formulario para agregar nuevo insumo</h5>
                                <div class="container_insumos">
                                    <table id="tblAddInsumo2" class="table table-bordered table-striped ">
                                        <thead>
                                            <tr>
                                                <th class="custom-width-number">Codigo</th>
                                                <th class="custom-width-text">Nombre</th>
                                                <th class="custom-width-number">Cantidad</th>
                                                <th class="custom-width-number">Medida</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txt_MInsumoCodigo" runat="server" CssClass="form-control" Font-Size="Small" AutoComplete="off"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_MInsumoNombre" runat="server" CssClass="form-control" AutoComplete="off" Font-Size="Small"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                                        TargetControlID="txt_MInsumoNombre" CompletionSetCount="12" MinimumPrefixLength="1"
                                                        ServiceMethod="GetListaInsumo" UseContextKey="True" CompletionListCssClass="CompletionList"
                                                        CompletionListItemCssClass="CompletionlistItem"
                                                        CompletionListHighlightedItemCssClass="CompletionListMighlightedItem"
                                                        CompletionInterval="10" OnClientItemSelected="onItemSelectedInsumoC2">
                                                    </asp:AutoCompleteExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_MInsumoCantidad" runat="server" CssClass="form-control" Font-Size="Small" TextMode="Number" step="0.0001"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txt_MInsumoMedida" runat="server" CssClass="form-control" Font-Size="Small" AutoComplete="off"></asp:TextBox></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="container-btn mb-2">
                                    <asp:Button ID="btn_InsertarInsumo" runat="server" Text="Añadir Insumo" CssClass="btn btn-success mt-2 " OnClick="btn_InsertarInsumo_Click" />
                                </div>
                            </div>
                                </asp:Panel>

                            <!-- Datos Insumo Creado -->
                            <div class="container-ICreadoModf2 col-12 col-md-12 col-lg-12 mt-3 row">
                                <div class="ICreadoModC1 col-sm-8 col-md-7 col-lg-6">
                                    <h5 class="form-label">Datos del Insumo Creado</h5>
                                    <table id="tblModInsumoCreado" class="table table-bordered gridview" Font-Size="Small">
                                        <thead>
                                            <tr>
                                                <th class="col-3">Codigo</th>
                                                <th>Nombre</th>
                                                <th class="col-3">Medida</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txt_MCodICreado" runat="server" CssClass="form-control" Enabled="False" Font-Size="Small" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_MNombre" runat="server" CssClass="form-control" Enabled="False" Font-Size="Small" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_MMedida" runat="server" CssClass="form-control" Enabled="False" Font-Size="Small" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div class="btn-container mb-3 col-lg-6">
                                        <asp:Button ID="btn_DeleteIC" runat="server" Text="Eliminar Insumo Creado" CssClass="btn btn-danger mt-2 mb-3 align-items-center" OnClick="btn_DeleteIC_Click" />
                                    </div>
                                </div>
                                    <div class="ICreadoModC2 col-sm-10 col-md-10 col-lg-6">
                                <asp:Panel ID="Panel_ICUpdate" runat="server" DefaultButton="btn_ModificarInsumoCreado">
                                        <h5 class="form-label">Lista de Insumos</h5>
                                        <asp:GridView runat="server" ID="gv_MODInsumoCreado" CssClass="table table-bordered gridview" Font-Size="Small" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_NewCantidad" runat="server" Text='<%# Eval("cantidad") %>' Font-Size="Small" CssClass="form-control" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="medida" HeaderText="Medida" />
                                            </Columns>
                                        </asp:GridView>
                                        <div class="btn-container">
                                            <asp:Button ID="btn_ModificarInsumoCreado" runat="server" Text="Actualizar Datos" CssClass="btn btn-info mt-2 mb-3" OnClick="btn_ModificarInsumoCreado_Click" />
                                        </div>
                                </asp:Panel>
                                    </div>
                                <!-- GV Lista Insumos -->
                            </div>
                        </div>

                    </div>
                    <br />
                </div>
            </div>
            <br>
            <br>
        </div>
    </div>
    <script src="../js/jsApi.js" type="text/javascript"></script>
</asp:Content>
