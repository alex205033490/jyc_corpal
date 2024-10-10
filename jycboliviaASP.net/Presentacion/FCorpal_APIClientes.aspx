<%@ Page Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_APIClientes.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIClientes" Async="true" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-6 col-md-6 col-6" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">
                            
                            <!-- POST clientesPersona -->
                            <div class="POST_clientePersona p-4 bg-light border rounded ">
                                <h5 class="text-success">Registrar Cliente</h5>

                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label class="form-label">Nombre:</label>
                                        <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label">Apellido Paterno:</label>
                                        <asp:TextBox ID="txt_paterno" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label class="form-label">Apellido Materno:</label>
                                        <asp:TextBox ID="txt_materno" runat="server" CssClass="form-control" placeholder="(Opcional)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label">Apellido Casado:</label>
                                        <asp:TextBox ID="txt_casado" runat="server" CssClass="form-control" placeholder="(Opcional)"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label class="form-label">Tipo de documento de identidad:</label>
                                        <asp:DropDownList ID="dd_tdocumento" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="CI" Value="1" />
                                            <asp:ListItem Text="Pasaporte" Value="2" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label">Número Documento:</label>
                                        <asp:TextBox ID="txt_numdocumento" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label class="form-label">Complemento:</label>
                                        <asp:TextBox ID="txt_complemento" runat="server" CssClass="form-control" placeholder="(Opcional)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label">Teléfono:</label>
                                        <asp:TextBox ID="txt_telefono" runat="server" CssClass="form-control" placeholder="(Opcional)"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label class="form-label">Correo:</label>
                                        <asp:TextBox ID="txt_correo" runat="server" CssClass="form-control" placeholder="(Opcional)"></asp:TextBox>
                                    </div>
                                </div>

                                <asp:Button ID="btn_registrar_cliente" runat="server" Text="Registrar Cliente" CssClass="btn btn-success" OnClick="Btn_RegistrarCliente_Click" />
                            </div><br />

                            
                            <!-- POST clientes/empresas -->
                            <div class="POST_clienteEmpresa p-4 bg-light border rounded">
                                <h5 class="text-success">Registrar Empresa</h5>
    
                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label class="form-label">Nombre Legal:</label>
                                        <asp:TextBox ID="txt_nomlegal" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label">Nombre Comercial:</label>
                                        <asp:TextBox ID="txt_nomcomercial" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
    
                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label class="form-label">Dirección:</label>
                                        <asp:TextBox ID="txt_direccion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label">Teléfono:</label>
                                        <asp:TextBox ID="txt_telefono2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
    
                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label class="form-label">NIT:</label>
                                        <asp:TextBox ID="txt_nit" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label">Correo Electrónico:</label>
                                        <asp:TextBox ID="txt_correo2" runat="server" CssClass="form-control" placeholder="(Opcional)"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label class="form-label">Es Sucursal:</label>
                                        <asp:DropDownList ID="dd_EsSucursal" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="True" Value="true" />
                                            <asp:ListItem Text="False" Value="false" />
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <asp:Button ID="btn_registrarEmpresa" runat="server" Text="Registrar Empresa" CssClass="btn btn-success" OnClick="btn_registrarEmpresa_Click"/>
                            </div>
                            <br />

                            <!-- GET clientes/buscar --> 
                                <div class="tb_ClienteEmpresa p-3 bg-light">
                                <h5 >Ver Clientes - Proveedores</h5>
                                    <div class="mb-3">
                                        <label class =form-label>Ingrese un Valor: </label>
                                        <asp:TextBox ID="txt_filtroBusqueda" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                <asp:Button ID="btn_buscar_CliEmpr" runat="server" Text="Buscar" OnClick="btn_buscar_CliEmpr_Click" />

                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="CodigoContacto" HeaderText="CodContacto" SortExpression="ClienteID" />
                                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" SortExpression="Nombre" />
                                        <asp:BoundField DataField="CodigoDocumentoIdentidad" HeaderText="Cod Documento Identidad" SortExpression="codDocIdentidad" />
                                        <asp:BoundField DataField="NumeroDocumentoIdentidad" HeaderText="Numero de Identidad" SortExpression="numDocIdentidad" />
                                        <asp:BoundField DataField="Complemento" HeaderText="Complemento" SortExpression="complemento" />
                                        <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
                                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" />
                                    </Columns>
                                    <HeaderStyle BackColor="#28a745" ForeColor="white" />
                                    <RowStyle BackColor="white"/>
                                    <AlternatingRowStyle BackColor="#f8f9fa"/>

                                </asp:GridView>
                            </div>
                      
                </div>
            </div>
        </div>
        </div>
</asp:Content>
