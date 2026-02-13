<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_GestionarCliente.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_GestionarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
        /* Definimos el Verde estilo Orona */
        :root {
            --orona-green: #84bd00; /* Puedes ajustar este hex si lo quieres más claro u oscuro */
        }

        /* Clase para el fondo de los paneles */
        .bg-orona {
            background-color: var(--orona-green) !important;
            color: #ffffff !important; /* Texto blanco */
        }

        /* Clase para los títulos de las secciones del formulario */
        .form-section-title {
            font-weight: bold;
            color: var(--orona-green); /* Texto verde */
            border-bottom: 2px solid var(--orona-green); /* Línea inferior verde */
            margin-bottom: 10px;
            padding-bottom: 5px;
        }

        /* Ajuste de botones */
        .espacio-botones .btn {
            margin-right: 5px;
            margin-bottom: 5px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="updPanel_Clientes" runat="server">
        <ContentTemplate>
            
            <div class="card" style="margin: 10px;">
                <div class="card-header bg-orona text-white">
                    <h4><i class="fa fa-users"></i> Gestión de Clientes Corpal</h4>
                </div>
                
                <div class="card-body">
                    
                    <div class="row mb-3 p-3 bg-light rounded border">
                        <div class="col-md-6">
                             <asp:Label ID="lblBuscar" runat="server" Text="Buscar Cliente (Nombre/Tienda):" Font-Bold="true"></asp:Label>
                             <div class="input-group">
                                <asp:TextBox ID="tx_buscar" CssClass="form-control" runat="server" placeholder="Ingrese criterio de búsqueda..."></asp:TextBox>
                                <div class="input-group-append">
                                    <asp:Button ID="bt_buscar" class="btn bg-orona" runat="server" Text="Buscar" OnClick="bt_buscar_Click" />
                                </div>
                             </div>
                        </div>
                    </div>

                    <div class="row">
                        
                        <div class="col-lg-4 col-md-12">
                            <h5 class="form-section-title">Datos de la Tienda</h5>
                            
                            <div class="form-group mb-2">
                                <label>Nombre Tienda:</label>
                                <asp:TextBox ID="tx_tiendaname" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-2">
                                <label>Dirección:</label>
                                <asp:TextBox ID="tx_tiendadir" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                            <div class="form-group mb-2">
                                <label>Teléfono Tienda:</label>
                                <asp:TextBox ID="tx_tiendatelefono" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="row">
                                <div class="col-6">
                                    <label>Departamento:</label>
                                    <asp:TextBox ID="tx_tiendadepartamento" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    <label>Zona:</label>
                                    <asp:TextBox ID="tx_tiendazona" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-4 col-md-12">
                            <h5 class="form-section-title">Datos del Propietario</h5>
                            
                            <div class="form-group mb-2">
                                <label>Nombre Propietario:</label>
                                <asp:TextBox ID="tx_propietarioname" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="row mb-2">
                                <div class="col-6">
                                    <label>CI:</label>
                                    <asp:TextBox ID="tx_propietarioci" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    <label>NIT:</label>
                                    <asp:TextBox ID="tx_propietarionit" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group mb-2">
                                <label>Celular:</label>
                                <asp:TextBox ID="tx_propietariocelular" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-2">
                                <label>Correo:</label>
                                <asp:TextBox ID="tx_propietariocorreo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-2">
                                <label>Dirección Propietario:</label>
                                <asp:TextBox ID="tx_propietariodir" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-4 col-md-12">
                            <h5 class="form-section-title">Facturación y Configuración</h5>
                            
                            <div class="form-group mb-2">
                                <label>Facturar a:</label>
                                <asp:TextBox ID="tx_facturar_a" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="row mb-2">
                                <div class="col-6">
                                    <label>NIT Factura:</label>
                                    <asp:TextBox ID="tx_facturar_nit" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    <label>Correo Factura:</label>
                                    <asp:TextBox ID="tx_facturar_correo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            
                            <hr />
                            
                            <div class="form-group mb-2 bg-warning p-2 rounded bg-opacity-10">
                                <label class="fw-bold">Tipo de Cliente:</label>
                                <asp:DropDownList ID="ddl_tipocliente" runat="server" CssClass="form-select form-control">
                                    <%-- Se llena desde el Backend (tbcorpal_tipocliente) --%>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group mb-2 bg-info p-2 rounded bg-opacity-10">
                                <label class="fw-bold">Lista de Precios:</label>
                                <asp:DropDownList ID="ddl_listaprecio" runat="server" CssClass="form-select form-control">
                                    <%-- Se llena desde el Backend (tbcorpal_listaprecio) --%>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group mb-2">
                                <label>Observación:</label>
                                <asp:TextBox ID="tx_observacion" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <hr />

                    <div class="row text-center espacio-botones">
                        <div class="col-12">
                            <asp:Button ID="bt_limpiar" class="btn btn-secondary" runat="server" Text="Limpiar / Nuevo" OnClick="bt_limpiar_Click" />
                            <asp:Button ID="bt_insertar" class="btn btn-success" runat="server" Text="Guardar Cliente" OnClick="bt_insertar_Click" />
                            <asp:Button ID="bt_modificar" class="btn btn-warning" runat="server" Text="Actualizar Datos" OnClick="bt_modificar_Click" />
                            <asp:Button ID="bt_eliminar" class="btn btn-danger" runat="server" Text="Eliminar Cliente" OnClick="bt_eliminar_Click" OnClientClick="return confirm('¿Está seguro de eliminar este cliente?');" />
                        </div>
                        <div class="col-12 mt-2">
                            <asp:Label ID="lbl_mensaje" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                        </div>
                    </div>

                </div> </div> <div class="card" style="margin: 10px;">
                <div class="card-header bg-dark text-white">
                    <h5>Listado de Clientes</h5>
                </div>
                <div class="card-body table-responsive">
                    <asp:GridView ID="gv_Clientes" runat="server" 
                        AutoGenerateColumns="False" 
                        DataKeyNames="codigo"
                        CssClass="table table-striped table-hover table-bordered"
                        GridLines="None"
                        OnSelectedIndexChanged="gv_Clientes_SelectedIndexChanged"
                        AllowPaging="True" PageSize="10" OnPageIndexChanging="gv_Clientes_PageIndexChanging">
    
                        <HeaderStyle CssClass="bg-orona text-white" /> <%-- Usando tu estilo verde --%>
    
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" SelectText="<i class='fa fa-edit'></i> Seleccionar" ControlStyle-CssClass="btn btn-sm btn-info" />
        
                            <asp:BoundField DataField="codigo" HeaderText="Cód" />
                            <asp:BoundField DataField="tiendaname" HeaderText="Tienda" />
                            <asp:BoundField DataField="tiendadir" HeaderText="Dirección" />
                            <asp:BoundField DataField="tiendatelefono" HeaderText="Teléfono" />
        
                            <asp:BoundField DataField="propietarioname" HeaderText="Propietario" />
                            <asp:BoundField DataField="propietariocelular" HeaderText="Celular Prop." />
        
                            <%-- Alias exactos de tu SQL --%>
                            <asp:BoundField DataField="NombreTipoCliente" HeaderText="Tipo Cliente" />
                            <asp:BoundField DataField="NombreListaPrecio" HeaderText="Lista Precio" />
                        </Columns>
                    </asp:GridView>
                    
                    <div class="mt-2 text-right">
                        <asp:Button ID="bt_excel" class="btn btn-success" runat="server" Text="Exportar a Excel" OnClick="bt_excel_Click" />
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
             <asp:PostBackTrigger ControlID="bt_excel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
