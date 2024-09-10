<%@ Page Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_APIClientes.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIClientes" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_SimecModificar.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="panel panel-success class">

            
                            <div>
                                <asp:Label runat="server" Text="Label">API Clientes</asp:Label> <br />
                            </div>
                            <!-- POST clientesPersona -->
                            <div class="Cpersona">
                                <asp:Label runat="server" Text="Label">API-POST Agregar persona</asp:Label><br>
                                <asp:TextBox ID="TextBox1" runat="server" placeholder="Nombres"></asp:TextBox>
                                <asp:TextBox ID="TextBox2" runat="server" placeholder="Apellido Paterno"></asp:TextBox>
                                <asp:TextBox ID="TextBox3" runat="server" placeholder="Apellido Materno (Opcional) "></asp:TextBox><br>
                                <asp:TextBox ID="TextBox4" runat="server" placeholder="Apellido Casado (Opcional) "></asp:TextBox>
                                <asp:Label runat="server" Text="Label">Tipo de documento de identidad: </asp:Label>
                                <asp:DropDownList ID="ddlNumbers" runat="server">
                                    <asp:ListItem Text="CI" Value="1" />
                                    <asp:ListItem Text="Pasaporte" Value="2" />
                                </asp:DropDownList>
                                <asp:TextBox ID="TextBox5" runat="server" placeholder="Numero Documento"></asp:TextBox><br>
                                <asp:TextBox ID="TextBox6" runat="server" placeholder="Complemento(Opcional)"></asp:TextBox>
                                <asp:TextBox ID="TextBox7" runat="server" placeholder="Telefono(Opcional)" TextMode="Number"></asp:TextBox>
                                <asp:TextBox ID="TextBox8" runat="server" placeholder="Correo(Opcional)"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" Text="Registrar" OnClick="Btn_RegistrarCliente_Click" />

                            </div>
                            <!-- POST clientes/empresas -->
                            <div class="Cempresas">
                                <asp:Label runat="server" Text="Label">API-POST Agregar Empresas</asp:Label><br>
                                <asp:TextBox ID="TextBox9" runat="server" placeholder="Nombre Legal"></asp:TextBox>
                                <asp:TextBox ID="TextBox10" runat="server" placeholder="Nombre Comercial"></asp:TextBox>
                                <asp:TextBox ID="TextBox11" runat="server" placeholder="Dirección"></asp:TextBox><br>
                                <asp:TextBox ID="TextBox12" runat="server" placeholder="Telefono" TextMode="Number"></asp:TextBox>
                                <asp:TextBox ID="TextBox13" runat="server" placeholder="Nit" TextMode="Number"></asp:TextBox>
                                <asp:TextBox ID="TextBox14" runat="server" placeholder="Correo Electronico(Opcional)"></asp:TextBox><br>
                                <asp:Label runat="server" Text="Label">EsSucursal: </asp:Label>
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                    <asp:ListItem Text="True" Value="true" />
                                    <asp:ListItem Text="False" Value="false" />
                                </asp:DropDownList>
                                <asp:Button ID="Button2" runat="server" Text="Registrar" OnClick="Btn_RegistrarEmpresa_Click"/>
                            </div>
                            <!-- GET clientes/buscar -->
                            <div class="Cbuscar">
                                <asp:Label runat="server" Text="Label">API-GET Buscar clientes</asp:Label><br>
                                <asp:Label runat="server" Text="Label">Ingrese un nombre: </asp:Label>
                                <asp:TextBox ID="TextBox15" runat="server" placeholder="Ingrese un valor"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Text="Buscar" OnClick="Btn_BuscarPersona_Click" /><br>
                                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                            </div>
                      
                </div>
            </div>
        </div>
</asp:Content>
