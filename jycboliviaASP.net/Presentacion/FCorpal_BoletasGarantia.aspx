<%@ Page Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_BoletasGarantia.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_BoletasGarantia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_EntregaProductosACamion.css" rel="stylesheet" type="text/css" />


    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <style type="text/css">
        .control_gv{
            background-color: yellow;
            padding: 0.2rem;
            border-radius: 0.3rem;
            font-size: 0.75rem;
            width: 100%;
        }
        .controlselect_gv{
            background-color: yellow;
            padding: 0.2rem;
            border-radius: 0.3rem;
        }
        .texto_chico{
            font-size: 0.7rem;
        }

    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="card">
        <div class="card-header  text-black">Solicitud De Documentos De Garantia</div>

        <div class="container-form">

            <div class="container_camposBoletaGarantia mb-2 col-md-12 col-lg-12 row">

                <div class="row col-lg-4 column1" style="font-size: smaller; ">

                    <div class="col-lg-6">
                        <asp:Label runat="server">Tipo Boleta:</asp:Label>
                        <asp:TextBox ID="tx_tipoBoletaGarantia" runat="server" CssClass="form-control texto_chico"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender
                            id="ace_tipoboletagarantia" runat="server" TargetControlID="tx_tipoBoletaGarantia" 
                             ServiceMethod="obtenerTiposBoleta" MinimumPrefixLength="1" CompletionInterval="100" 
                             EnableCaching="true" CompletionSetCount="10" UseContextKey="true" CompletionListCssClass="CompletionList"
                             CompletionListItemCssClass="CompletionlistItem" CompletionListHighlightedItemCssClass="CompletionListMighlightedItem">
                        </ajaxToolkit:AutoCompleteExtender>

                    </div>
                    <div class="col-lg-6">
                        <asp:Label runat="server">Fecha:</asp:Label>
                        <asp:TextBox ID="tx_fecha" runat="server" CssClass="form-control texto_chico"></asp:TextBox>
                        <asp:CalendarExtender ID="ce_fecha" runat="server" TargetControlID="tx_fecha" Format="dd/MM/yyyy" />
                    </div>

                    <div class="col-lg-6">
                        <asp:Label runat="server">Cliente:</asp:Label>
                        <asp:TextBox ID="tx_cliente" runat="server" CssClass="form-control texto_chico"></asp:TextBox>
                    </div>

                    <div class="col-lg-6">
                        <asp:Label runat="server">Beneficiario:</asp:Label>
                        <asp:TextBox ID="tx_beneficiario" runat="server" CssClass="form-control texto_chico"></asp:TextBox>
                    </div>

                    <div class="col-lg-6">
                        <asp:Label runat="server">Estado:</asp:Label>
                        <asp:DropDownList ID="dd_estadoBoleta" runat="server" CssClass="form-select texto_chico" >
                            <asp:ListItem Text="Abierto" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Cerrado" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>


                <div class="col-lg-4 column2 row" style="font-size: smaller;" >
                    <div class="col-lg-6">
                        <asp:Label runat="server">Moneda:</asp:Label>
                        <asp:TextBox ID="tx_moneda" runat="server" CssClass="form-control texto_chico"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender
                            id="ace_tipoMoneda" runat="server" TargetControlID="tx_moneda" 
                             ServiceMethod="obtenerTipoMoneda" MinimumPrefixLength="1" CompletionInterval="100" 
                             EnableCaching="true" CompletionSetCount="10" UseContextKey="true" CompletionListCssClass="CompletionList"
                             CompletionListItemCssClass="CompletionlistItem" CompletionListHighlightedItemCssClass="CompletionListMighlightedItem">
                        </ajaxToolkit:AutoCompleteExtender>
                    </div>

                    <div class="col-lg-6">
                        <asp:Label runat="server">Monto Numeral:</asp:Label>
                        <asp:TextBox ID="tx_Monto" runat="server" CssClass="form-control texto_chico" 
                                oninput="this.value = this.value.replace(/\./g, ',');" ></asp:TextBox>
                    </div>

                    <div class="col-lg-6">
                        <asp:Label runat="server">Fecha De Inicio:</asp:Label>
                        <asp:TextBox ID="tx_fechainicio" runat="server" CssClass="form-control texto_chico"></asp:TextBox>
                        <asp:CalendarExtender ID="ce_fechainicio" runat="server" TargetControlID="tx_fechainicio" Format="dd/MM/yyyy" />
                    </div>

                    <div class="col-lg-6">
                        <asp:Label runat="server">Fecha Vencimiento:</asp:Label>
                        <asp:TextBox ID="tx_fechaVencimiento" runat="server" CssClass="form-control texto_chico"></asp:TextBox>
                        <asp:CalendarExtender ID="ce_fechavencimiento" runat="server" TargetControlID="tx_fechaVencimiento" Format="dd/MM/yyyy" />
                    </div>

                    <div class="col-lg-6">
                        <asp:Label runat="server">Objeto:</asp:Label>
                        <asp:TextBox ID="tx_objeto" runat="server" CssClass="form-control texto_chico"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender
                                id="ace_objetoGarantia" runat="server" TargetControlID="tx_objeto" 
                                 ServiceMethod="obtenerTipoObjeto" MinimumPrefixLength="1" CompletionInterval="100" 
                                 EnableCaching="true" CompletionSetCount="10" UseContextKey="true" CompletionListCssClass="CompletionList"
                             CompletionListItemCssClass="CompletionlistItem" CompletionListHighlightedItemCssClass="CompletionListMighlightedItem">
                            </ajaxToolkit:AutoCompleteExtender>
                    </div>

                    <div class="col-lg-6">
                        <asp:Label runat="server">Extencion Del Objeto:</asp:Label>
                        <asp:TextBox ID="tx_extenciondObjeto" runat="server" CssClass="form-control" style="font-size: 0.6rem; height: 55px;"
                             TextMode="MultiLine"></asp:TextBox>
                    </div>

                </div>

            </div>
            <div class="col-lg-7" style="align-content: end;">
                <asp:Button ID="btn_registrarBoletaGarantia" runat="server" CssClass="btn btn-success"
                    Text="Registrar" OnClick="btn_registrarBoletaGarantia_Click" />
                <asp:Button ID="btn_limpiarForm" runat="server" CssClass="btn btn-info" Text="Limpiar" OnClick="btn_limpiarForm_Click" />
            </div>


                    <div class="container-lista1">
                        <!-- LISTA DE BOLETAS GARANTIA  -->
                        <div class="container-gvRegistros table-responsive mb-2" data-clientid="<%= gv_boletasGarantia.ClientID %>">

                            <asp:GridView ID="gv_boletasGarantia" runat="server"
                                CssClass="table table-striped sticky-table gv_boletasGarantia" AutoGenerateColumns="false"
                                Style="background-color: white !important;" DataKeyNames="id" OnRowEditing="gv_boletasGarantia_RowEditing" OnRowCancelingEdit="gv_boletasGarantia_RowCancelingEdit" OnRowUpdating="gv_boletasGarantia_RowUpdating">
                                <Columns>

                                    <asp:CommandField HeaderText="Acción"
                                    ShowEditButton="true"
                                    EditText="Editar"
                                    UpdateText="Guardar"
                                    CancelText="Cancelar" />

                                    <asp:BoundField
                                    DataField="id"
                                    HeaderText="ID"
                                    ReadOnly="true" />

                                    <asp:BoundField DataField="fechagra" 
                                        HeaderText="Fecha Registro" ReadOnly="true" />
                                    
                                    <asp:BoundField DataField="horagra" 
                                        HeaderText="Hora Registro" ReadOnly="true" />

                                    <asp:TemplateField HeaderText="Moneda">
                                        <ItemTemplate>
                                            <%# Eval("moneda") %>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:TextBox
                                                ID="tx_monedaEditar"
                                                runat="server"
                                                CssClass="control_gv" style="width: 80px;"
                                                Text='<%# Bind("moneda") %>'>
                                            </asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Monto">
                                    <ItemTemplate>
                                        <%# Eval("montonumeral") %>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="tx_montoEditar"
                                            runat="server"
                                            oninput="this.value = this.value.replace(/\./g, ',');"
                                            CssClass="control_gv" style="width: 80px;"
                                            Text='<%# Bind("montonumeral") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tipo_boleta">
                                    <ItemTemplate>
                                        <%# Eval("tipoboleta") %>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="tx_tipoBoletaEditar"
                                            runat="server"
                                            textmode="MultiLine" 
                                            CssClass="control_gv" style="width: 200px; height: 80px;"
                                            Text='<%# Bind("tipoboleta") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Beneficiario">
                                    <ItemTemplate>
                                        <%# Eval("beneficiario") %>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="tx_beneficiarioEditar"
                                            runat="server"
                                            CssClass="control_gv"
                                            Text='<%# Bind("beneficiario") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Cliente">
                                    <ItemTemplate>
                                        <%# Eval("cliente") %>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="tx_clienteEditar"
                                            runat="server"
                                            textmode="MultiLine"
                                            CssClass="control_gv" style="width: 100px; height: 80px;"
                                            Text='<%# Bind("cliente") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <%# Eval("estado") %>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:DropDownList
                                            ID="dd_estadoEditar"
                                            runat="server"
                                            CssClass="controlselect_gv"
                                            SelectedValue='<%# Bind("estado") %>'>

                                            <asp:ListItem Text="Abierto" Value="Abierto"></asp:ListItem>
                                            <asp:ListItem Text="Cerrado" Value="Cerrado"></asp:ListItem>

                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Fecha Inicio">
                                    <ItemTemplate>
                                        <%# Eval("fechainicio") %>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="tx_fechainicioEditar"
                                            runat="server"
                                            CssClass="control_gv" style="width: 80px;"
                                            Text='<%# Bind("fechainicio") %>'>
                                        </asp:TextBox>
                                        <asp:CalendarExtender ID="ce_fechainicioeditar" runat="server" TargetControlID="tx_fechainicioEditar" Format="dd/MM/yyyy" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Fecha Vencimiento">
                                    <ItemTemplate>
                                        <%# Eval("fechavencimiento") %>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="tx_fechavencimientoEditar"
                                            runat="server"
                                            CssClass="control_gv"
                                            Text='<%# Bind("fechavencimiento") %>'>
                                        </asp:TextBox>
                                        <asp:CalendarExtender ID="ce_fechavencimientoeditar" runat="server" TargetControlID="tx_fechaVencimientoEditar" Format="dd/MM/yyyy" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Objeto">
                                    <ItemTemplate>
                                        <%# Eval("objeto") %>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="tx_objetoEditar"
                                            runat="server"
                                            CssClass="control_gv" style="width: 160px;"
                                            Text='<%# Bind("objeto") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Extencion Objeto">
                                    <ItemTemplate>
                                        <%# Eval("extencionobjeto") %>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="tx_extencionobjetoEditar"
                                            runat="server"
                                            CssClass="control_gv"  style="width: 250px;"
                                            Text='<%# Bind("extencionobjeto") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                </Columns>

                            </asp:GridView>

                        </div>
                    </div>

            <br />


        </div>
    </div>
    <script src="../js/mainCorpal.js"></script>


</asp:Content>



























