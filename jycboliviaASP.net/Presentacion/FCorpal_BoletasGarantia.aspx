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

    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="card">
        <div class="card-header  text-black">Boletas Garantia</div>

        <div class="container-form">

            <div class="container_camposBoletaGarantia mb-2 col-md-12 col-lg-12">

                <div class="row col-lg-8" style="font-size: smaller; ">

                    <div class="col-lg-3">
                        <asp:Label runat="server">Fecha:</asp:Label>
                        <asp:TextBox ID="tx_fecha" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender ID="ce_fecha" runat="server" TargetControlID="tx_fecha" Format="dd/MM/yyyy" />
                    </div>

                    <div class="col-lg-3">
                        <asp:Label runat="server">Monto:</asp:Label>
                        <asp:TextBox ID="tx_Monto" runat="server" CssClass="form-control" 
                                oninput="this.value = this.value.replace(/\./g, ',');" ></asp:TextBox>
                    </div>

                    <div class="col-lg-3">
                        <asp:Label runat="server">Tipo Boleta:</asp:Label>
                        <asp:TextBox ID="tx_tipoBoletaGarantia" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-lg-4">
                        <asp:Label runat="server">Cliente:</asp:Label>
                        <asp:TextBox ID="tx_cliente" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-lg-3">
                        <asp:Label runat="server">Estado:</asp:Label>
                        <asp:DropDownList ID="dd_estadoBoleta" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Abierto" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Cerrado" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-lg-3" style="align-content: end;">
                        <asp:Button ID="btn_registrarBoletaGarantia" runat="server" CssClass="btn btn-success"
                            Text="Registrar" OnClick="btn_registrarBoletaGarantia_Click" />
                    </div>
                </div>

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

                                <asp:TemplateField HeaderText="Monto">
                                    <ItemTemplate>
                                        <%# Eval("monto") %>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="tx_montoEditar"
                                            runat="server"
                                            oninput="this.value = this.value.replace(/\./g, ',');"
                                            CssClass="control_gv"
                                            Text='<%# Bind("monto") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tipo boleta">
                                    <ItemTemplate>
                                        <%# Eval("tipoboleta") %>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="tx_tipoBoletaEditar"
                                            runat="server"
                                            CssClass="control_gv"
                                            Text='<%# Bind("tipoboleta") %>'>
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
                                            CssClass="control_gv"
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



                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>



            <br />



        </div>
    </div>
    <script src="../js/mainCorpal.js"></script>






</asp:Content>



























