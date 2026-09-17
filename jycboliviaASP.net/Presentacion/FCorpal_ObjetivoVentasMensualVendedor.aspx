<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" CodeBehind="FCorpal_ObjetivoVentasMensualVendedor.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_ObjetivoVentasMensualVendedor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_EntregaProductosACamion.css" rel="stylesheet" type="text/css" />

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            var table = $(".sticky-table");
            if (table.find("thead").length === 0) {
                table.prepend("<thead>" + table.find("tr:first").html() + "</thead>");
                table.find("tr:first").remove();
            }
        })

        $(document).ready(function () {
            function addCheckboxChangeListener() {
                var gridViewId = $(".container-gvRegistros").data("clientid");

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

    </script>

    <style type="text/css">
        .error-message {
            color: red;
            font-size: 12px;
            margin-top: 5px;
        }

        .gv_despachos th {
            border: 1px solid white;
        }

        .gv_despachos td {
            text-align: center;
        }

        .table-sticky th {
            position: sticky !important;
            top: 0 !important;
            color: white !important;
            z-index: 100 !important;
            border: 1px solid white !important;
            text-align: center;
        }

        .table-sticky th:first-child,
        .table-sticky td:first-child{
            position: sticky !important;
            left: 0 !important;
            z-index: 101 !important;
            background-color: #e4e4e4;
        }
        .table-sticky th:first-child{
            z-index: 102 !important;
        }

        .container_gv{
            height: 580px;
        }
        .card_principal{
            background-color: darkorange;
        }
        .columnaProducto{
            font-size: 0.53rem;
        }
    </style>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card">
        <div class="card-header text-black card_principal">OBJETIVO MENSUAL VENTAS </div>
        <div class="container-form col-lg-12">

            <div class="container_acciones row col-lg-6 mb-2">

                <div class="col-lg-4 col-md-3 col-3">
                    <asp:Label runat="server" Text="Mes"></asp:Label>
                    <asp:DropDownList ID="dd_mes" runat="server" CssClass="form-select" >
                        <asp:ListItem Value="1" Text="Enero" ></asp:ListItem>
                        <asp:ListItem Value="2" Text="Febrero" ></asp:ListItem>
                        <asp:ListItem Value="3" Text="Marzo" ></asp:ListItem>
                        <asp:ListItem Value="4" Text="Abril" ></asp:ListItem>
                        <asp:ListItem Value="5" Text="Mayo" ></asp:ListItem>
                        <asp:ListItem Value="6" Text="Junio" ></asp:ListItem>
                        <asp:ListItem Value="7" Text="Julio" ></asp:ListItem>
                        <asp:ListItem Value="8" Text="Agosto" ></asp:ListItem>
                        <asp:ListItem Value="9" Text="Septiembre" ></asp:ListItem>
                        <asp:ListItem Value="10" Text="Octubre" ></asp:ListItem>
                        <asp:ListItem Value="11" Text="Noviembre" ></asp:ListItem>
                        <asp:ListItem Value="12" Text="Diciembre" ></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-lg-4 col-md-2 col-3">
                    <asp:Label runat="server" Text="Año" ></asp:Label>
                    <asp:DropDownList ID="dd_anio" runat="server" CssClass="form-control" Width="80%"></asp:DropDownList>
                </div>

                <div class="col-lg-4 col-md-3 col-4 d-grid gap-1">
                    <asp:Button ID="btn_buscar" runat="server" CssClass="btn btn-primary" Text="Buscar" OnClick="btn_buscar_Click" />
                    <asp:Button ID="btn_registrar" runat="server" CssClass="btn btn-success" Text="Registrar/Actualizar" 
                                    OnClick="btn_registrar_Click" OnClientClick="return validarTextBoxObjetivos();"/>
                    <asp:Button ID="btn_limpiarForm" runat="server" CssClass="btn btn-danger" Text="Limpiar" OnClick="btn_limpiarForm_Click" />
                </div>
                
            </div>


            <div class="container_gv table-responsive col-lg-12">
                <asp:GridView ID="gv_vendedorproductoObj" runat="server" BackColor="White"
                        BorderColor="#b4b4b4" BorderStyle="Ridge" BorderWidth="1px" CellPadding="7"
                        Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="false"
                        CssClass="table table-striped table-sticky" DataKeyNames="codigo">
                        <Columns>
                        
                        </Columns>
                    </asp:GridView>
            </div>




            

        </div>

    </div>


    <script type="text/javascript">
        function validarTextBoxObjetivos() {
            var textboxs = document.querySelectorAll(
                '#<%= gv_vendedorproductoObj.ClientID %> input[type="text"]'
            );

            for (var i = 0; i < textboxs.length; i++) {

                var valor = textboxs[i].value.trim();

                if (valor === "") {
                    continue;
                }

                if (!/^[0-9]+([.,][0-9]+)?$/.test(valor)) {
                    alert("El valor ingresado no es válido. \n" + 
                        "Solo se permiten números.");
                    textboxs[i].focus();

                    return false;
                }
            }
            return true;
        }

    </script>

</asp:Content>

















