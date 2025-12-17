<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" CodeBehind="FCorpal_AsignacionRuta_Camion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_AsignacionRuta_Camion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_EntregaProductosACamion.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .container_newpunto{
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

                                <asp:Button ID="btn_dibujarPuntos" runat="server" Text="Dibujar Puntos"
                                    CssClass="btn btn-success mt-2" OnClick="btn_dibujarPuntos_Click"/>                            
                            </div>

                            <div class="col-lg-4 container_newpunto" 
                                    style="border: 1px solid #a9a9a93d; padding: 0.5rem 0 0;"> 
                                
                                    <asp:HiddenField ID="hf_codCliente" runat="server"/>

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
                                    runat="server" Text="Agregar Nuevo Punto" OnClick="btn_registrarNuevoPunto_Click" />
                                
                            </div>
                            

                        </div>
                        

                        <div class="row col-lg-12">
                            <div class="col-lg-4">
                                
                            <asp:UpdatePanel ID="updatePanel_listRutasCamiones" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    
                                        <asp:GridView ID="gv_listaRutasDespacho" CssClass="table table-striped" runat="server"
                                             AutoGenerateColumns="false" style="font-size: 0.75rem;">
                                            <Columns>
                                                <asp:BoundField DataField="orden" HeaderText="Orden"/>
                                                <asp:BoundField DataField="cliente" HeaderText="Cliente"/>
                                                <asp:BoundField DataField="lat" HeaderText="Lat"/>
                                                <asp:BoundField DataField="lng" HeaderText="lng"/>
                                            </Columns>
                                        </asp:GridView>
                                    
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dd_listVehiculo" EventName="SelectedIndexChanged"/>
                                </Triggers>
                            </asp:UpdatePanel>
                                
                            </div>

                        <div class="col-lg-8">

                            <div class="container_maps">
                                <div id="divmappp" style="width:100%; height: 420px; border: 1px solid #ccc;"></div>
                            </div>

                        </div>
                        </div>
                        

                    </div>


                </div>


            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        
        let map;
        let marker = null;
        let markers = [];
        let directionsService;
        let directionsRenderer;

        function initMap() {
            const defaultLocation = { lat: -17.751882, lng: -63.132911 };

            map = new google.maps.Map(document.getElementById("divmappp"), {
                zoom: 13,
                center: defaultLocation
            });

            directionsService = new google.maps.DirectionsService();
            directionsRenderer = new google.maps.DirectionsRenderer();
            directionsRenderer.setMap(map);

            // EVENTO CLICK
            map.addListener("click", function (event) {
                addMarker(event.latLng);
            });
        }

        // dibujar puntos desde ASP.NET
        function dibujarPuntosMapa(puntos) {

            limpiarMarkers();

            if (!puntos || puntos.length === 0) return;

            let bounds = new google.maps.LatLngBounds();

            puntos.forEach((p, index) => {

                let marker = new google.maps.Marker({
                    position: {
                        lat: parseFloat(p.lat),
                        lng: parseFloat(p.lng)
                    },
                    map: map,
                    label: (index + 1).toString()
                });

                markers.push(marker);
                bounds.extend(marker.getPosition());
            });
            maps.fitBounds(bounds);
        }

        // limpiar markers anteriores
        function limpiarMarkers() {
            markers.forEach(m => m.setMap(null));
            markers = [];
        }

        
        function addMarker(location) {
            let marker = new google.maps.Marker({
                position: location,
                map: map
            });

            markers.push(marker);

            if (markers.length >= 2) {
                calculateOptimizedRoute();
            } 
        }

        function calculateOptimizedRoute() {

            if (markers.length < 2) return;

            let origin = markers[0].getPosition();
            let destination = markers[markers.length - 1].getPosition();

            let waypoints = [];

            for (let i = 1; i < markers.length - 1; i++) {
                waypoints.push({
                    location: markers[i].getPosition(),
                    stopover: true
                });
            }

            let request = {
                origin: origin,
                destination: destination,
                waypoints: waypoints,
                optimizeWaypoints: true,
                travelMode: google.maps.TravelMode.DRIVING
            };

            directionsService.route(request, function (result, status) {
                if (status == 'OK') {
                    directionsRenderer.setDirections(result);

                    console.log("Nuevo Orden: ", result.routes[0].waypoint_order);
                } else {
                    console.error(status);
                }
            });
        }

        /* SEPARADOR AUTOCOMPLETE */
        function clientSeleccionado(sender, args) {
            let valor = args.get_value();

            let partes = valor.split(' - ');

            let codigo = partes[0];
            let tienda = partes.slice(1).join(' - ');

            document.getElementById('<%= hf_codCliente.ClientID %>').value = codigo;
            document.getElementById('<%= tx_newCliente.ClientID %>').value = tienda;
        }



    </script>

   
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBadNUlLiF0DBBKZse7AFtt-v2p4Oz1Vp0" async defer></script>
   

</asp:Content>























