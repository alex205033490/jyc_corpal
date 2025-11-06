<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" CodeBehind="FCorpal_ConsultaStockVendedores.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCopal_ConsultaStockVendedores" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_EntregaProductosACamion.css" rel="stylesheet" type="text/css" />

    <style>
        .CompletionList {
            padding: 5px;
            margin: 2px 0 0;
            /*  position:absolute;  */
            height: 150px;
            width: 200px;
            background-color: White;
            cursor: pointer;
            border: solid;
            border-width: 1px;
            font-size: small;
            overflow: auto;
        }
        .CompletionlistItem {
            font-size: small;
        }
        .container-gvRegistros{
            border: 0px ;
            height: 200px;
        }
        .CompletionListMighlightedItem {
            background-color: darkorange;
            color: White;
        }
        .container-gvRegistros td{
            text-align: center;
        }


    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script type="text/javascript">


        $(document).ready(function () {
            var table = $(".sticky-table");
            if (table.find("thead").length === 0) {
                table.prepend("<thead>" + table.find("tr:first").html() + "</thead>");
                table.find("tr:first").remove();
            }
        })


        function onResponsableSelected(sender, e) {
            const valor = e.get_value();
            const partes = valor.split(" - ");
            if (partes.length >= 2) {
                document.getElementById("<%= hf_codResponsable.ClientID %>").value = partes[0];
                document.getElementById("<%= tx_responsable.ClientID %>").value = partes[1];
            }
        }


        

    </script>

    <div class="card">
        <div class="card-header text-black">Stock de Productos Por Vendedor</div>

        <div class="container-form">

            <asp:UpdatePanel ID="updatePanelAlmacenDinamico" runat="server" UpdateMode="Conditional">
                <contenttemplate>

                    <div class="container_main row col-lg-12">
                        
                        <!-- INPUT -->
                        <div class="container_filtroBusqueda col-lg-12">

                            <div class="form_buscarResponsable col-lg-5 col-md-8 mb-2 row mb-2 row">

                                    <div class="container_input col-lg-8 col-md-6 col-sm-6 col-7">
                                        <asp:HiddenField ID="hf_codResponsable" runat="server"/>
                                        <asp:Label runat="server" Font-Size="Small" Text="Personal:"></asp:Label>
                                        <asp:TextBox ID="tx_responsable" class="form-control" Style="background-color: #7080903b; font-size: 0.8rem;" runat="server"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="tx_responsable_AutoCompleteExtender" runat="server"
                                             TargetControlID="tx_responsable" CompletionSetCount="12" MinimumPrefixLength="2"
                                             ServiceMethod="getListResponsable" UseContextKey="true" CompletionListCssClass="CompletionList" 
                                             CompletionListItemCssClass="CompletionlistItem" CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" 
                                             CompletionInterval="10" OnClientItemSelected="onResponsableSelected">
                                        </asp:AutoCompleteExtender>
                                    </div>
                     
                                    <div class="mb-3 container_btn d-flex flex-column gap-3 col-lg-4 col-md-4 col-sm-4 col-5">
                                        <asp:Button ID="btn_registrarAprobacion" runat="server" CssClass="btn btn-success" Text="Buscar" OnClick="btn_registrarAprobacion_Click" />
                                        <asp:Button ID="bt_limpiar" runat="server" class="btn btn-primary" Text="Limpiar" OnClick="bt_limpiar_Click" />
                                    </div>
                            </div>
                        </div>



                        <!-- LISTA STOCK DE PRODUCTO -->
                        <div class="container-gvRegistros table-responsive mb-2 col-lg-7 col-md-7 col-11" data-clientid="<%= gv_stockProductos.ClientID %>">
                            
                            <asp:GridView ID="gv_stockProductos" runat="server"
                                CssClass="table table-striped sticky-table gv_solicitudesProductos"
                                Style="background-color: white !important;" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="codproducto" HeaderText="Codigo" />
                                    <asp:BoundField DataField="producto" HeaderText="Producto" />
                                    <asp:BoundField DataField="cantidad" HeaderText="Stock" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>

                </contenttemplate>
            </asp:UpdatePanel>

            <br />

        </div>
    </div>
    <script src="../js/mainCorpal.js"></script>
   
</asp:Content>


















