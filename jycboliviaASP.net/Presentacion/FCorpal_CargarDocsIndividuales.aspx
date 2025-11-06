<%@ Page Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_CargarDocsIndividuales.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_CargarDocsIndividuales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Styles/Style_EntregaProductosACamion.css" rel="stylesheet" type="text/css" />
    <style>
        .container-gvRegistros {
            background-color: white;
            height: 250px;
            border: 2px solid black;
            font-size: 12px;
            overflow-y: auto;
            margin: 10px;
            padding: 0px;
            width: 700px;
        }
        .container-form{
            padding: 0 1rem;
        }
        .container_cargarDoc{
            padding: 0rem;
            margin: 0.5rem 0;
        }

        .container-gvListProductos {
            height: 250px;
            margin: 10px 0;
            font-size: 0.7rem;
            padding: 0;
        }
        .container_buscador{
            border: 1px solid black;
            border-radius: 0.5rem;
            padding: 0.2rem;
            margin: 0.5rem 0rem;
        }
        .Container_input{
            margin: 0.5rem;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>

        var scrollPositicon = 0;
        function saveScroll() {
            var container = document.querySelector('.container-gvRegistros');
            if (container) {
                scrollPos = container.scrollTop;
            }
        }
        function restoreScroll() {
            var container = document.querySelector('.container-gvRegistros');
            if (container) {
                container.scrollTop = scrollPos;
            }
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
            saveScroll();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            restoreScroll();
        });
        $(document).ready(function () {
            var table = $(".sticky-table");
            if (table.find("thead").length === 0) {
                table.prepend("<thead>" + table.find("tr:first").html() + "</thead>");
                table.find("tr:first").remove();
            }
        })

    </script>



    <div class="card">
        <div class="card-header text-black">Carga/Descarga de Documentos </div>

        <div class="container-form">

            <!-- FORMULARIO CARGA DE DOCUMENTOS -->
            <div class="container_formInput">

                <div class="form_input">

                    <div class="Container_input row col-lg-8 col-md-6">
                        <div class="container_buscador col-lg-6 d-flex gap-2">
                            <asp:TextBox ID="tx_nomArchivo" runat="server" CssClass="form-control flex-grow-1" placeholder="Ingrese un Nombre"></asp:TextBox>
                            <asp:Button ID="btn_buscarDoc" runat="server" Text="Buscar Doc" OnClick="btn_buscarDoc_Click" CssClass="btn btn-success"/>
                        </div>
                        
                        
                        <div class="container_cargarDoc col-lg-8 col-md-8 d-flex gap-2" style="align-content:center">
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="500px" BackColor="SeaGreen" CssClass="form-control flex-grow-1" />
                            <asp:Button ID="btn_subirDoc" runat="server" CssClass="btn btn-dark" Text="Subir Documento" OnClick="btn_subirDoc_Click" />
                        </div>
                        
                        
                    </div>

                </div>

            </div>


                    <div class="container_listDocPersonales row col-lg-12 col-md-12 col-sm-12 col-12">

                        <div class="container-gvRegistros table-responsive mb-2 col-lg-8" data-clientid="<%= gv_docPersonales.ClientID %>">
                            <asp:GridView ID="gv_docPersonales" runat="server"
                                CssClass="table table-striped sticky-table gv_solicitudesProductos"
                                Style="background-color: white !important;" OnSelectedIndexChanged="gv_docPersonales_SelectedIndexChanged">
                               <Columns>
                                   <asp:CommandField ShowSelectButton="true" SelectText="Descargar" />
                               </Columns>
                            </asp:GridView>
                        </div>


                    </div>

            <br />

        </div>
    </div>
    <script src="../js/mainCorpal.js"></script>
</asp:Content>
