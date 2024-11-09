<%@ Page Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_APIClientes.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIClientes" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 col-lg-10" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-12 col-md-offset-1">
                <div class="panel panel-success class">
                    <!---------------- POST clientesPersona ---------------->
                    <div class="container-POSTPersona p-4 rounded col-lg-12">

                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Formulario Registro de Clientes</h3>
                        </div>

                        <asp:Panel runat="server" DefaultButton="btn_registrar_cliente">
                        <div class="row mb-3 col-sm-12 col-md-12 col-lg-12">
                            <div class="col-6 col-sm-4 col-md-3">
                                <label class="form-label">Nombre:</label>
                                <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-6 col-sm-4 col-md-3">
                                <label class="form-label">Apellido Paterno:</label>
                                <asp:TextBox ID="txt_paterno" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-6 col-sm-4 col-md-3">
                                <label class="form-label">Apellido Materno:</label>
                                <asp:TextBox ID="txt_materno" runat="server" CssClass="form-control" placeholder="(Opcional)" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-6 col-sm-4 col-md-3">
                                <label class="form-label">Apellido Casado:</label>
                                <asp:TextBox ID="txt_casado" runat="server" CssClass="form-control" placeholder="(Opcional)" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-6 col-sm-4 col-md-2">
                                <label class="form-label">Tipo de documento:</label>
                                <asp:DropDownList ID="dd_tdocumento" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="CI" Value="1" />
                                    <asp:ListItem Text="Pasaporte" Value="2" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-6 col-sm-4 col-md-3">
                                <label class="form-label">Número Documento:</label>
                                <asp:TextBox ID="txt_numdocumento" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-6 col-sm-3 col-md-3">
                                <label class="form-label">Complemento:</label>
                                <asp:TextBox ID="txt_complemento" runat="server" CssClass="form-control" placeholder="(Opcional)" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-6 col-sm-3 col-md-3">
                                <label class="form-label">Teléfono:</label>
                                <asp:TextBox ID="txt_telefono" runat="server" CssClass="form-control" placeholder="(Opcional)" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-6 col-sm-4 col-md-3">
                                <label class="form-label">Correo:</label>
                                <asp:TextBox ID="txt_correo" runat="server" CssClass="form-control" placeholder="(Opcional)" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-4 col-sm-2 col-md-3 align-items-end d-flex">
                                <asp:Button ID="btn_registrar_cliente" runat="server" Text="Registrar" CssClass="btn btn-success" OnClick="Btn_RegistrarCliente_Click" />
                            </div>
                        </div>
                            </asp:Panel>
                    </div>
                    <br />

                    <!---------------- POST clientes/empresas ---------------->
                    <div class="container-POSTEmpresa p-4 rounded col-lg-12">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Formulario Registro de Proveedores</h3>
                        </div>

                        <!-- formulario -->
                        <asp:Panel runat="server" DefaultButton="btn_registrarEmpresa">
                        <div class="form-POSTEmpresa row">

                            <div class="row mb-3 col-sm-12 col-md-6 col-lg-6">
                                <div class="col-6 col-sm-6 col-md-6">
                                    <label class="form-label">Nombre Legal:</label>
                                    <asp:TextBox ID="txt_nomlegal" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                                <div class="col-6 col-sm-6 col-md-6">
                                    <label class="form-label">Nombre Comercial:</label>
                                    <asp:TextBox ID="txt_nomcomercial" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-3 col-sm-12 col-md-6 col-lg-6">
                                <div class="col-6 col-sm-6 col-md-6">
                                    <label class="form-label">Dirección:</label>
                                    <asp:TextBox ID="txt_direccion" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                                <div class="col-6 col-sm-6 col-md-6">
                                    <label class="form-label">Teléfono:</label>
                                    <asp:TextBox ID="txt_telefono2" runat="server" CssClass="form-control" AutoComplete="off" placeholder="(Opcional)"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-3 col-sm-12 col-md-6 col-lg-5">
                                <div class="col-6 col-sm-6 col-md-6">
                                    <label class="form-label">NIT:</label>
                                    <asp:TextBox ID="txt_nit" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                                <div class="col-6 col-sm-6 col-md-6">
                                    <label class="form-label">Correo Electrónico:</label>
                                    <asp:TextBox ID="txt_correo2" runat="server" CssClass="form-control" placeholder="(Opcional)" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-3 col-sm-6 col-md-6 col-lg-5">
                                <div class="col-6 col-sm-7 col-md-4 mb-1">
                                    <label class="form-label">Es Sucursal:</label>
                                    <asp:DropDownList ID="dd_EsSucursal" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Si" Value="true" />
                                        <asp:ListItem Text="No" Value="false" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-6 col-sm-5 col-md-4 d-flex align-items-end">
                                    <asp:Button ID="btn_registrarEmpresa" runat="server" Text="Registrar" CssClass="btn btn-success" OnClick="btn_registrarEmpresa_Click" />
                                </div>
                            </div>

                        </div>
                            </asp:Panel>
                    </div>
                    <br />
                    <!--------------- GET clientes/buscar con filtro --------------->
                    <div class="container-GETClientes p-4 rounded col-lg-12">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Vista de Clientes/Proveedores</h3>
                        </div>

                        <asp:Panel runat="server" DefaultButton="btn_buscar_CliEmpr">
                        <div class="mb-3 col-lg-5 row">
                            <div class="col-8 col-sm-5 col-md-3 col-lg-8 mb-1">
                                <label class="form-label">Ingrese un valor: </label>
                                <asp:TextBox ID="txt_filtroBusqueda" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-4 col-sm-2 col-md-2 d-flex align-items-end">
                                <asp:Button ID="btn_buscar_CliEmpr" runat="server" Text="Buscar" CssClass="btn btn-dark" OnClick="btn_buscar_CliEmpr_Click" />
                            </div>
                        </div>
                            </asp:Panel>
                        <div class="container_gv2 col-md-9 col-lg-8">
                            <asp:GridView ID="GridView1" runat="server" CssClass="gridview table-hover" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="CodigoContacto" HeaderText="CodContacto" SortExpression="ClienteID" />
                                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" SortExpression="Nombre" />
                                    <asp:BoundField DataField="CodigoDocumentoIdentidad" HeaderText="CodDocumento Identidad" SortExpression="codDocIdentidad" />
                                    <asp:BoundField DataField="NumeroDocumentoIdentidad" HeaderText="Número de Identidad" SortExpression="numDocIdentidad" />
                                    <asp:BoundField DataField="Complemento" HeaderText="Complemento" SortExpression="complemento" />
                                    <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
                                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Teléfono" />
                                </Columns>
                                <AlternatingRowStyle CssClass="alternating-row" />
                                <FooterStyle CssClass="footer" />
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <SelectedRowStyle CssClass="selected-row" />
                                <SortedAscendingCellStyle CssClass="sorted-asc-cell" />
                                <SortedAscendingHeaderStyle CssClass="sorted-asc-header" />
                                <SortedDescendingCellStyle CssClass="sorted-desc-cell" />
                                <SortedDescendingHeaderStyle CssClass="sorted-desc-header" />
                            </asp:GridView>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
