<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" CodeBehind="FCorpal_GestionarVehiculos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_GestionarVehiculos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <link href="../Styles/Style_gestionarVehiculo.css" rel="stylesheet" type="text/css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            var table = $(".table-sticky");

            if (table.find("thead").length === 0) {
                table.prepend("<thead>" + table.find("tr:first").html() + "</thead>");
                table.find("tr:first").remove();
            }
        })

        $(document).ready(function () {
            function addCheckboxChangeListener() {
                var gridViewId = $(".container_listaVehiculos").data("clientid");

                $("#" + gridViewId + " input[type='checkbox']").change(function () {
                    var row = $(this).closest("tr");

                    if ($(this).is(":checked")) {
                        row.addClass("highlighted");
                    } else {
                        row.removeClass("highlighted");
                    }
                });
            }
            addCheckboxChangeListener();
            Sys.Application.add_load(function () {
                addCheckboxChangeListener();
            });
        });

        function convertdotcomma(event) {
            var input = event.target;

            input.value = input.value.replace('.', ',');
        }
    </script>

    <div class="card">
        <div class="card-header bg-warning text-black">
            <h1>Gestionar Vehiculos</h1>
        </div>

        <div class="list-group list-group-flush">
            <div class="container_RegistroVehiculo">
                <h2 class="p-3">Registro de Vehiculo</h2>
                <div class="form-registro p-3 m-3 row" >
                    <div class="form-p1 p-2 col-lg-3 col-md-6 col-sm-6">
                        <label>Marca:</label>
                        <asp:TextBox ID="txt_marca" runat="server" autocomplete="off" CssClass="form-control mb-2" Width="80%"></asp:TextBox>

                        <label>Modelo:</label>
                        <asp:TextBox ID="txt_modelo" runat="server" autocomplete="off"  CssClass="form-control mb-2"  Width="80%"></asp:TextBox>

                        <label>Placa:</label>
                        <asp:TextBox ID="txt_placa" runat="server" autocomplete="off" CssClass="form-control mb-2"  Width="80%"></asp:TextBox>

                    </div>

                    <div class="form-p2 p-2 col-lg-3 col-md-6 col-sm-6">
                        <label>Conductor:</label>
                        <asp:TextBox ID="txt_conductor" runat="server" autocomplete="off"  CssClass="form-control mb-2"  Width="80%"></asp:TextBox>

                        <label>Capacidad (Tn):</label>
                        <asp:TextBox ID="txt_capacidad" runat="server" autocomplete="off"  CssClass="form-control mb-2" Width="80%" oninput="convertdotcomma(event)"></asp:TextBox>

                        <label>Capacidad Cajas:</label>
                        <asp:TextBox ID="txt_capCajas" runat="server" autocomplete="off" CssClass="form-control mb-2"  Width="80%"></asp:TextBox>

                        <label>Detalle:</label>
                        <asp:TextBox ID="txt_detalle" runat="server" autocomplete="off"  CssClass="form-control mb-2"></asp:TextBox>
                    </div>

                    <div class="p-2 col-lg-3">
                        <asp:Button ID="btn_registrarForm" runat="server" CssClass="btn btn-success" Font-Size="20px" Text="Registrar" OnClick="btn_registrarForm_Click" />
                    </div>


                </div>
            </div>

            <div class="container_btns m-3">
                <asp:Button ID="btn_update" Text="Actualizar" runat="server" CssClass="btn btn-info" Font-Size="20px" style="margin-right:20px;" OnClick="btn_update_Click" />
                <asp:Button ID="btn_anular" Text="Quitar" runat="server" CssClass="btn btn-danger" Font-Size="20px" OnClick="btn_anular_Click" />

            </div>

            <div class="container_listaVehiculos table-responsive" data-clientid="<%= gv_listaVehiculos.ClientID %>"">
                <asp:GridView ID="gv_listaVehiculos" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-striped table-sticky gv_listaVehiculos" DataKeyNames="codigo">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" CssClass="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="codigo" HeaderText="Codigo Vehiculo" SortExpression="codigo"/>

                        <asp:TemplateField HeaderText="Marca">
                            <ItemTemplate>
                                <asp:TextBox ID="tx_marcaCar" runat="server" BackColor="Yellow" AutoComplete="off" Width="90%" Text='<%# Bind("marca") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Modelo">
                            <ItemTemplate>
                                <asp:TextBox ID="tx_modeloCar" runat="server" BackColor="Yellow" autoComplete="off" Width="80%" Text='<%# Bind("modelo") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Placa">
                            <ItemTemplate>
                                <asp:TextBox ID="tx_placaCar" runat="server" BackColor="Yellow" AutoComplete="off" Width="90%" Text='<%# Bind("placa") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Conductor">
                            <ItemTemplate>
                                <asp:TextBox ID="tx_conductorCar" runat="server" BackColor="Yellow" AutoComplete="off" Text='<%# Bind("conductor") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Capacidad (Tn)">
                            <ItemTemplate>
                                <asp:TextBox ID="tx_capacidadCar" runat="server" BackColor="Yellow" AutoComplete="off" Width="80%" Text='<%# Bind("capacidad") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Capacidad Cajas">
                            <ItemTemplate>
                                <asp:TextBox ID="tx_cargacajasCar" runat="server" BackColor="Yellow" AutoComplete="off" Width="80%" Text='<%# Bind("cargacajas") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Detalles">
                            <ItemTemplate>
                                <asp:TextBox ID="tx_detalleCar" runat="server" BackColor="Yellow" AutoComplete="off" Text='<%# Bind("detalle") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>












