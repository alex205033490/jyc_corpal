<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" CodeBehind="FCorpal_AsignacionRuta_Camion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_AsignacionRuta_Camion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_EntregaProductosACamion.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <div class="card">
        <div class="card-header  text-black">Entrega de Solicitud Productos</div>

        <div class="container-form">


            <div class="container-lista1">
                <div>
                    <h3>| ASIGNACIÓN DE RUTAS |
                    </h3>
                </div>

                <div class="container_contenido row ">
                    <div class="col-lg-4">
                        <asp:Label runat="server" Font-Size="Small" Text="Vehiculo:"></asp:Label>
                        <asp:DropDownList ID="dd_listVehiculo" Font-Size="Small" runat="server" CssClass="form-select ddVehiculo" AutoPostBack="true">
                        </asp:DropDownList>
                        <br />

                        <asp:Label runat="server" Text="Latitud: "></asp:Label>
                        <asp:TextBox ID="tx_latitud" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Label runat="server" Text="longitud: "></asp:Label>
                        <asp:TextBox ID="tx_longitud" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:Button ID="btn_addCoordenada" runat="server" CssClass="btn btn-success" Text="Agregar ruta"
                                 OnClientClick="addPointFromTextBox(); return false;" />

                    </div>
                    <div class="col-lg8">

                        <div class="container_maps">
                            <div id="divmappp" style="width:100%; height: 420px; border: 1px solid #ccc;"></div>
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
                travelMode: 'DRIVING'
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
    </script>

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBadNUlLiF0DBBKZse7AFtt-v2p4Oz1Vp0&callback=initMap" async defer></script>

</asp:Content>























