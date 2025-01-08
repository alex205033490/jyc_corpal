<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APIProduccion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIProduccion" Async="true" MasterPageFile="~/PlantillaNew.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Contentn2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="" style="padding-top: 1em;">
        <div class="">
            <div class="col-md-11 col-md-offset-1">
                <div class="panel panel-success class">

                    <!--------------------------------          API POST PRODUCCION PARTEproduccion  (registra datos vacios - La cadena de entrada no tiene el formato correcto.)       ----------------------->

                    <div class="container-POSTProduccion p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Registro de Producción</h3>
                        </div>

                        <!--  FORMULARIO PARA REGISTRAR PRODUCCION  -->
                        <asp:UpdatePanel ID="updatePanel_RProduccion" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                            
                        <div class="form_col1 row">
                            <div class="mb-3 col-12 col-xs-6 col-sm-6 col-md-5 col-lg-4">
                                <div class="col-9 col-sm-12 col-md-12 col-lg-12">
                                    <label class="form-label">Referencia:</label>
                                    <asp:TextBox ID="txt_referencia" runat="server" CssClass="form-control" placeholder="Opcional" AutoComplete="off"></asp:TextBox>
                                </div>
                                <div class="col-10 col-sm-12 col-md-12 col-lg-12">
                                    <label class="form-label">Glosa:</label>
                                    <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control" Placeholder="Opcional" AutoComplete="off"></asp:TextBox>
                                </div>

                            </div>

                            <div class="mb-3 col-12 col-xs-6 col-sm-6 col-md-4 col-lg-4">
                                <div class="col-9 col-sm-12 col-md-10 col-lg-10">
                                    <label class="form-label">Linea Producción:</label>
                                    <asp:DropDownList ID="dd_lineaProduccion" runat="server" CssClass="form-select dd_fsmall">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>


                                <div class="col-8 col-sm-9 col-md-10 col-lg-10">
                                    <label class="form-label">Realizar Descarga:</label>
                                    <asp:DropDownList ID="dd_realDescarga" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="No" Value="false" />
                                        <asp:ListItem Text="Si" Value="true" />
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="mb-3 col-12 col-xs-6 col-sm-6 col-md-4 col-lg-4">
                                <div class="col-10 col-sm-12 col-md-10 col-lg-12">
                                    <label class="form-label">Responsable:</label>
                                    <asp:TextBox ID="txt_codResponsable" runat="server" AutoPostBack="true" CssClass="form-control" AutoComplete="off" OnTextChanged="txt_codResponsable_TextChanged"></asp:TextBox>
                                    <asp:GridView ID="gv_listResponsables" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClass="gv_listResponsables table table-bordered small" OnSelectedIndexChanged="gv_listResponsables_SelectedIndexChanged">
                                        <columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                            <asp:BoundField DataField="CodigoContacto" HeaderText="Codigo" />
                                            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" HtmlEncode="false" />
                                        </columns>
                                    </asp:GridView>
                                </div>

                                <div class="col-6 col-sm-10 col-md-10 col-lg-8">
                                    <label class="form-label">Ítem Análisis:</label>
                                    <asp:TextBox ID="txt_itemAnalisis" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>

                            </div>

                        </div>
                                </ContentTemplate>
                            <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btn_registrarProduccion" EventName="Click" />
                            </Triggers>
                            </asp:UpdatePanel>



                        <!--  LISTA DETALLE PRODUCCION  -->
                        <div class="form_detproducto col-md-12 col-lg-12">
                            <h3 class="form-label">Detalle Productos</h3>

                            <asp:UpdatePanel ID="updatePanelPost_IIDetProd" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel runat="server" DefaultButton="btn_addProd">
                                        <div class="form_addProducto row mb-3">


                                            <div class="input_producto col-4">
                                                <asp:Label runat="server"> Nombre del producto:</asp:Label>
                                                <asp:TextBox ID="txt_producto" runat="server" AutoPostBack="true" CssClass="form-control dd_fsmall" AutoComplete="off" OnTextChanged="txt_producto_TextChanged"></asp:TextBox>
                                            </div>

                                            <div class="input_cantidad col-2">
                                                <asp:Label runat="server"> Cantidad:</asp:Label>
                                                <asp:TextBox ID="txt_cantProducto" runat="server" CssClass="form-control" AutoComplete="off" oninput="convertdotcomma(event);"></asp:TextBox>
                                            </div>

                                            <div class="input_codReceta col-4">
                                                <asp:Label runat="server">Receta:</asp:Label>
                                                <asp:DropDownList ID="dd_recetas" runat="server" CssClass="form-select dd_fsmall" ></asp:DropDownList>
                                            </div>

                                            <div class="container_btnAddProd col-2 d-flex align-items-end">
                                                <asp:Button runat="server" ID="btn_addProd" Text="Agregar" CssClass="btn btn-success" OnClick="btn_addProd_Click" />
                                            </div>
                                        </div>
                                    </asp:Panel>


                                    <asp:GridView ID="gv_listProdProduccion" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClass="table table-bordered" OnSelectedIndexChanged="gv_listProdProduccion_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                            <asp:BoundField DataField="CodigoProducto" HeaderText="Codigo" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" HtmlEncode="false"/>
                                            <asp:BoundField DataField="CodigoUnidadMedida" HeaderText="UM" />
                                        </Columns>
                                    </asp:GridView>


                            <div class="container_gvProdAddII mb-3">
                                <asp:GridView ID="gv_productAgregados" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClss="table table-bordered" OnRowCommand="gv_productAgregados_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEliminarFila" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("CodigoProducto") %>' CssClass="btn btn-danger btn-sm" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="CodigoProducto" HeaderText="CodProducto" />
                                        <asp:BoundField DataField="CodUnidadMedida" HeaderText="Unidad Medida" />
                                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                        <asp:BoundField DataField="CodReceta" HeaderText="CodReceta" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                                    </ContentTemplate>
                                    <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btn_registrarProduccion" EventName="Click" />
                                </Triggers>

                            </asp:UpdatePanel>

                            <div>
                                <asp:Button ID="btn_registrarProduccion" runat="server" Text="Registrar Producción" CssClass="btn btn-success" OnClick="btn_registrarProduccion_Click" />
                            </div>
                        </div>

                    </div>
                </div>
                <br />



                <!---------------------------------------       API GET PRODUCCION DETALLE  (Muestra datos vacios)  ------------------------------------------------->
                <div class="container-GETProduccionDet p-4 rounded">
                    <div class="container_tittle rounded">
                        <h3 class="text_tittle p-3">Vista Produccion Detalle (F)</h3>
                    </div>

                    <div class="container_input mb-3 row">
                        <div class="container_textbox col-6 col-sm-4 col-md-4 col-lg-4">
                            <label class="form-label">Numero Parte de Produccion:</label>
                            <asp:TextBox ID="txt_numProduccion1" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-4 col-sm-4 col-md-2 align-items-end d-flex">
                            <asp:Button ID="btn_buscProduccion" runat="server" Text="Buscar Produccion" CssClass="btn btn-dark btn-sm" OnClick="btn_buscProduccion_Click" />
                        </div>
                    </div>

                    <asp:UpdatePanel ID="updatePanel_GETProduccionDet" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                    <div class="container_gv1">
                        <asp:GridView ID="gv_produccion" runat="server" AutoGenerateColumns="false">
                            <columns>
                                <asp:BoundField DataField="NumeroParteProduccion" HeaderText="numPartProduccion" SortExpression="nPartProd" />
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                                <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                                <asp:BoundField DataField="CodigoResponsable" HeaderText="codResponsable" SortExpression="cResp" />
                                <asp:BoundField DataField="ItemAnalisis" HeaderText="ItemAnalisis" SortExpression="iAnali" />
                                <asp:BoundField DataField="LineaProduccion" HeaderText="LineaProduccion" SortExpression="lProdc" />
                                <asp:BoundField DataField="RealizaDescarga" HeaderText="realizaDescarga" SortExpression="rDescarg" />
                                <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="glo" />
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="usu" />
                            </columns>
                            <headerstyle backcolor="#ffcc00" forecolor="black" />
                            <rowstyle backcolor="white" />
                            <alternatingrowstyle backcolor="#f8f9fa" />
                        </asp:GridView>
                    </div>

                    <div class="container_gv2">
                        <asp:GridView ID="gv_detalle" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false">

                            <columns>
                                <asp:BoundField DataField="Item" HeaderText="item" SortExpression="item" />
                                <asp:BoundField DataField="CodigoProducto" HeaderText="codProducto" SortExpression="codProd" />
                                <asp:BoundField DataField="Cantidad" HeaderText="cantidad" SortExpression="cant" />
                                <asp:BoundField DataField="UnidadMedida" HeaderText="uMedida" SortExpression="uMed" />
                                <asp:BoundField DataField="CodigoReceta" HeaderText="codReceta" SortExpression="cRecet" />
                            </columns>

                            <headerstyle backcolor="#ffcc00" forecolor="black" />
                            <rowstyle backcolor="white" />
                            <alternatingrowstyle backcolor="#f8f9fa" />
                        </asp:GridView>
                    </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_buscProduccion" EventName="Click"/>
                        </Triggers>
                        </asp:UpdatePanel>
                </div>
            </div>
        </div>

    </div>
    <br />
    <script src="../js/jsApi.js" type="text/javascript"></script>
</asp:Content>
