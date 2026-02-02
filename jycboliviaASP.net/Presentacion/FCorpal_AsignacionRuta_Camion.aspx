<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" CodeBehind="FCorpal_AsignacionRuta_Camion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_AsignacionRuta_Camion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_EntregaProductosACamion.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .container_newpunto {
            border: 1px solid #a9a9a93d;
            padding: 0.5rem;
            margin: 0 1rem;
            background-color: #75757552;
            border-radius: 1rem;
            font-size: 0.8rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <div class="card">
        <div class="card-header  text-black">Gestión de Rutas de Camiones</div>

        <div class="container-form">

            <div class="container-lista1">
                <div>
                    <h3>| ASIGNACIÓN DE RUTAS |
                    </h3>
                </div>

                <div class="container_contenido row ">

                    <div class="col-lg-12">

                        <div class="row col-lg-12 mb-2">

                            <div class="col-lg-3">

                                <asp:Label runat="server" Font-Size="Small" Text="Vehiculo:"></asp:Label>
                                <asp:DropDownList ID="dd_listVehiculo" Font-Size="Small" runat="server" CssClass="form-select ddVehiculo" AutoPostBack="true" OnSelectedIndexChanged="dd_listVehiculo_selectedIndexChanged">
                                </asp:DropDownList>

                                <asp:Button ID="btn_dibujarPuntosGV" runat="server" Text="Dibujar Rutas"
                                    CssClass="btn btn-success mt-2" OnClick="btn_dibujarPuntosGV_Click" />

                                <asp:Button ID="btn_dibujarPuntos" runat="server" Text="Reordenar Rutas"
                                    CssClass="btn mt-2 btn-info" OnClick="btn_dibujarPuntos_Click" OnClientClick="optimizarRutasGoogle(); return false;"/>

                            </div>

                            <div class="col-lg-4 container_newpunto"
                                style="border: 1px solid #a9a9a93d; padding: 0.5rem;">

                                <asp:HiddenField ID="hf_codCliente" runat="server" />

                                <asp:Label ID="lb_cliente" runat="server"> Cliente:</asp:Label>
                                <asp:TextBox ID="tx_newCliente" runat="server" CssClass="form-control mb-1">
                                </asp:TextBox>

                                <asp:AutoCompleteExtender ID="tx_newcliente_AutoCompleteExtender" runat="server"
                                    TargetControlID="tx_newcliente"
                                    CompletionSetCount="12"
                                    MinimumPrefixLength="1" ServiceMethod="GetlistaClientes"
                                    UseContextKey="True" OnClientItemSelected="clientSeleccionado"
                                    CompletionListCssClass="CompletionList"
                                    CompletionListItemCssClass="CompletionlistItem"
                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                                </asp:AutoCompleteExtender>

                                <asp:Button ID="btn_registrarNuevoPunto" CssClass="btn btn-success"
                                    runat="server" Text="Agregar Ruta" OnClick="btn_registrarNuevoPunto_Click" />

                            </div>


                        </div>
                        <!--  #####################################################################################################################################################  -->
                        <!--  ###########   GRIDVIEW LISTA DE RUTAS   ###########  -->
                        <!--  #####################################################################################################################################################  -->
                        <div class="row col-lg-12">
                            <div class="col-lg-4">

                                <asp:UpdatePanel ID="updatePanel_listRutasCamiones" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <asp:GridView ID="gv_listaRutasDespacho" CssClass="table table-striped" runat="server"
                                            AutoGenerateColumns="false" Style="font-size: 0.75rem;" DataKeyNames="codcliente">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Orden">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOrden" runat="server"
                                                            Text='<%# Eval("Orden") %>' CssClass="form-control text-center txtOrdenGV"
                                                            Width="60px">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="cliente" HeaderText="Cliente" />
                                                <asp:BoundField DataField="lat" HeaderText="Lat" />
                                                <asp:BoundField DataField="lng" HeaderText="lng" />
                                                <asp:BoundField DataField="codcliente" HeaderText="codCli" Visible="false"/>
                                            </Columns>
                                        </asp:GridView>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="dd_listVehiculo" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="btn_dibujarPuntosGV" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btn_guardarOrden" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btn_registrarNuevoPunto" EventName="Click"/>
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="col-lg-8">
                                <asp:UpdatePanel ID="updatePanel_containerMaps" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="container_maps">
                                            <div id="divmappp" style="width: 100%; height: 420px; border: 1px solid #ccc;"></div>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_dibujarPuntos" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            

                        </div>

                        <div class="col-lg-12">
                            <div class="col-lg-4">
                                <asp:Button ID="btn_guardarOrden" runat="server" CssClass="btn btn-success" Text="Guardar Orden" OnClick="btn_guardarOrden_Click" />

                                <asp:Button ID="btn_limpiarMaps" runat="server" Text="Limpiar Mapa"
                                    CssClass="btn btn-danger" OnClientClick="limpiarMapa(); return false;" OnClick="btn_limpiarMaps_Click" />
                            </div>

                        </div>

                    </div>

                </div>

            </div>
        </div>
    </div>

    <script type="text/javascript">

        let map;
        let markerReferencia;
        let markers = [];

        let directionsService;
        let directionsRenderer;

        function initMap() {

            //Punto referencia MAP
            const LocationFabrica = {
                lat: -17.752107,
                lng: -63.132962
            };

            //Crear mapa
            map = new google.maps.Map(document.getElementById("divmappp"), {
                zoom: 13,
                center: LocationFabrica
            });

            // ICONO FABRICA 
            const iconoFabrica = {
                path: "M3 21H21V10L15 6V10L9 6V10L3 10Z M6 21V15H9V21 M11 21V15H14V21 M16 21V15H19V21",
                fillColor: "#2c3e50",      
                fillOpacity: 1,
                strokeColor: "#f0871f",
                strokeWeight: 1,
                scale: 1.2,                
                anchor: new google.maps.Point(12, 21) 
            };

            // MARKER UNICO
            markerReferencia = new google.maps.Marker({
                position: LocationFabrica,
                map: map,
                title: "Fabrica Corpal",
                icon: iconoFabrica,
                label: "Corpal"
            });

            directionsService = new google.maps.DirectionsService();
            directionsRenderer = new google.maps.DirectionsRenderer({
                suppressMarkers: true
            });
            directionsRenderer.setMap(map);
        }

        // ############################################################################## 
        // FUNCION DIBUJAR PUNTOS DESDE GV
        function dibujarPuntosDesdeGv(puntos) {

            markers.forEach(m => m.setMap(null));
            markers = [];

            if (!puntos || puntos.length === 0) return;

            let bounds = new google.maps.LatLngBounds();

            puntos.forEach(p => {

                let marker = new google.maps.Marker({
                    position: {
                        lat: parseFloat(p.lat),
                        lng: parseFloat(p.lng),
                    },
                    map: map,
                    title: p.cliente,
                    label: p.orden.toString()

                });

                markers.push(marker);
                bounds.extend(marker.getPosition());
            });
            map.fitBounds(bounds);
        }


        // ##############################################################################
        // FUNCION DIBUJAR PUNTOS OPTIMIZADO 
        function optimizarRutasGoogle() {
            if (markers.length < 2) {
                alert("Se requieren al menos 2 puntos");
                return;
            }
            const origin = markerReferencia.getPosition();
            const destination = origin;

            const waypoints = markers.map(m => ({
                location: m.getPosition(),
                stopover: true
            }));

            directionsService.route({
                origin: origin,
                destination: destination,
                waypoints: waypoints,
                optimizeWaypoints: true,
                travelMode: google.maps.TravelMode.DRIVING
            }, function (result, status) {
                if (status === "OK") {

                    directionsRenderer.setDirections(result);

                    const ordenGoogle = result.routes[0].waypoint_order;

                    ordenGoogle.forEach((idx, nuevoOrden) => {
                        markers[idx].setLabel((nuevoOrden + 1).toString());
                    });

                    actualizarOrdenGridView(ordenGoogle);

                } else {
                    alert("Error al optimizar las rutas. " + status);
                }
            });
        }



        // ##############################################################################
        // LIMPIAR MAPS
        function limpiarMapa() {
            markersGrid.forEach(m => m.setMap(null));
            markersManual.forEach(m => m.setMap(null));

            directionsRenderer.set('directions', null);
        }


        // ###############################################################################
        /* SEPARADOR AUTOCOMPLETE CLIENTE */
        function clientSeleccionado(sender, args) {
            let valor = args.get_value();

            let partes = valor.split(' - ');

            let codigo = partes[0];
            let tienda = partes.slice(1).join(' - ');

            document.getElementById('<%= hf_codCliente.ClientID %>').value = codigo;
            document.getElementById('<%= tx_newCliente.ClientID %>').value = tienda;
        }

        // ###############################################################################
        function actualizarOrdenGridView(ordenGoogle) {
            const txtOrdenes = document.querySelectorAll(".txtOrdenGV");

            ordenGoogle.forEach((idx, nuevoOrden) => {
                if (txtOrdenes[idx]) {
                    txtOrdenes[idx].value = nuevoOrden + 1;
                }
            });
        }

    </script>

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBadNUlLiF0DBBKZse7AFtt-v2p4Oz1Vp0&callback=initMap" async defer></script>

</asp:Content>























