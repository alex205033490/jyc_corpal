<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FACorpal_GestionListaPrecio.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FACorpal_GestionListaPrecio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .panel-gestion { margin-top: 20px; }
        .margen-top { margin-top: 30px; }
        .fila-seleccionada { background-color: #d9edf7 !important; font-weight: bold; }
        .link-seleccion { text-decoration: none; font-weight: bold; color: #0056b3; }
        .link-seleccion:hover { text-decoration: underline; color: #003d82; }
    </style>

    

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div class="container panel-gestion">
        <h2>Gestión de Listas de Precios</h2>
        <hr />
        
        <asp:Panel ID="panelFormularioLista" runat="server" Visible="false" CssClass="card" style="margin-bottom: 20px; border: 1px solid #ccc; padding: 15px; background-color: #f9f9f9;">
            <h4 style="color: #337ab7;">Registro de Lista de Precio</h4>
            <div class="row">
                <div class="col-md-5">
                    <div class="form-group">
                        <label>Nombre de la Lista (*)</label>
                        <asp:TextBox ID="txtNombreLista" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="form-group">
                        <label>Descripción</label>
                        <asp:TextBox ID="txtDescripcionLista" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>% Dcto. Gral.</label>
                        <asp:TextBox ID="txtDescuentoGral" runat="server" CssClass="form-control" TextMode="Number" step="0.01" Text="0.00"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 15px;">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btnGuardarLista" runat="server" Text="Guardar Lista" CssClass="btn btn-primary" OnClick="btnGuardarLista_Click" />
                    <asp:Button ID="btnCancelarLista" runat="server" Text="Cancelar" CssClass="btn btn-default" OnClick="btnCancelarLista_Click" />
                </div>
            </div>
        </asp:Panel>



        <div class="row mb-3" style="margin-bottom: 15px;">
            <div class="col-md-8">
                <div class="input-group">
                    <asp:TextBox ID="txtBuscarLista" runat="server" CssClass="form-control" Placeholder="Buscar lista por nombre..."></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Button ID="btnBuscarLista" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscarLista_Click" />
                    </span>
                </div>
            </div>
            <div class="col-md-4 text-right">
                <asp:Button ID="btnNuevaLista" runat="server" Text="+ Nueva Lista" CssClass="btn btn-success" OnClick="btnNuevaLista_Click" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
 

                <asp:GridView ID="gvListasPrecio" runat="server" AutoGenerateColumns="False" 
            CssClass="table table-striped table-bordered table-hover"
            DataKeyNames="codigo" 
            OnSelectedIndexChanged="gvListasPrecio_SelectedIndexChanged"
            OnRowEditing="gvListasPrecio_RowEditing"
            OnRowDeleting="gvListasPrecio_RowDeleting"
            OnRowUpdating="gvListasPrecio_RowUpdating" 
            OnRowCancelingEdit="gvListasPrecio_RowCancelingEdit">
            <Columns>
                <asp:BoundField DataField="codigo" HeaderText="ID" ItemStyle-Width="50px" ReadOnly="true" />
                
                <%-- 1. NOMBRE DE LA LISTA --%>
                <asp:TemplateField HeaderText="Nombre de la Lista">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkNombreLista" runat="server" CommandName="Select" 
                            Text='<%# Eval("nombre") %>' CssClass="link-seleccion" 
                            ToolTip="Haga clic para ver los productos de esta lista">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditNombre" runat="server" Text='<%# Bind("nombre") %>' CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <%-- 2. DESCRIPCIÓN --%>
                <asp:TemplateField HeaderText="Descripción">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("descripcion") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditDescripcion" runat="server" Text='<%# Bind("descripcion") %>' CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                
                <%-- 3. % DCTO GRAL --%>
                <asp:TemplateField HeaderText="% Dcto. Gral.">
                    <ItemTemplate>
                        <asp:Label ID="lblDctoGral" runat="server" Text='<%# Eval("descuentogral_porcentaje", "{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditDctoGral" runat="server" 
                            Text='<%# Eval("descuentogral_porcentaje").ToString().Replace(",", ".") %>' 
                            CssClass="form-control" TextMode="Number" step="0.01">
                        </asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                
                <%-- 4. BOTONES DE ACCIÓN --%>
                <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Edit" CssClass="btn btn-warning btn-sm" ToolTip="Editar Lista">
                            Editar
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Delete" CssClass="btn btn-danger btn-sm" ToolTip="Eliminar Lista"
                            OnClientClick="return confirm('¿Está seguro de eliminar esta lista de precios?');">
                            Eliminar
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkGuardar" runat="server" CommandName="Update" CssClass="btn btn-success btn-sm" ToolTip="Guardar Cambios">
                            Guardar
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-default btn-sm" ToolTip="Cancelar Edición">
                            Cancelar
                        </asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <SelectedRowStyle CssClass="fila-seleccionada" />
        </asp:GridView>


            </div>
        </div>

        <br />

        <div class="row margen-top" id="panelProductos" runat="server" visible="false">
            <div class="col-md-12">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <h4>Productos en: <asp:Label ID="lblNombreListaSeleccionada" runat="server" ForeColor="#d9534f"></asp:Label></h4>
                    <asp:Button ID="btnAbrirAgregar" runat="server" Text="+ Agregar Producto a la Lista" 
                        CssClass="btn btn-success" OnClick="btnAbrirAgregar_Click" />
                </div>

               <%-- PANEL AGREGAR PRODUCTO --%>

<asp:Panel ID="panelAgregarProducto" runat="server" Visible="false" CssClass="card" style="margin-bottom: 20px; border: 1px solid #5cb85c; padding: 15px; background-color: #f0fdf4;">
    <h5 style="color: #4cae4c; font-weight: bold;">Agregar Producto a la Lista</h5>
    
    <div class="row" style="margin-bottom: 10px;">
        <div class="col-md-4">
            <div class="form-group">
                <label>Producto (*)</label>
                <asp:TextBox ID="txtBuscarProducto" runat="server" CssClass="form-control" 
                    list="miListaDeProductos" AutoPostBack="true" 
                    OnTextChanged="txtBuscarProducto_TextChanged" 
                    placeholder="Escriba para buscar...">
                </asp:TextBox>
                
                <datalist id="miListaDeProductos">
                    <asp:Repeater ID="rptProductos" runat="server">
                        <ItemTemplate>
                            <%-- Mostraremos el código y el nombre. Ej: "15 - Tubo PVC" --%>
                            <option value='<%# Eval("codigo") + " - " + Eval("producto") %>'></option>
                        </ItemTemplate>
                    </asp:Repeater>
                </datalist>
            </div>
        </div>
        <div class="col-md-2">
            <div class="form-group">
                <label>Precio Base</label>
                <asp:TextBox ID="txtPrecioAgregar" runat="server" CssClass="form-control" ReadOnly="true" BackColor="#e9ecef"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-2">
            <div class="form-group">
                <label>Unidad</label>
                <asp:TextBox ID="txtUnidadAgregar" runat="server" CssClass="form-control" ReadOnly="true" BackColor="#e9ecef"></asp:TextBox>
            </div>
        </div>
<div class="col-md-2">
            <div class="form-group">
                <label>% Dcto. Lista</label>
                <asp:TextBox ID="txtDctoAgregar" runat="server" CssClass="form-control" TextMode="Number" step="0.01" Text="0.00" 
                    onkeyup="calcularPrecioFinal()" onchange="calcularPrecioFinal()"></asp:TextBox>
            </div>
        </div>
        
        <div class="col-md-2">
            <div class="form-group">
                <label>Precio Final</label>
                <asp:TextBox ID="txtPrecioEspecialAgregar" runat="server" CssClass="form-control" TextMode="Number" step="0.01" Text="0.00" ReadOnly="true" BackColor="#e9ecef"></asp:TextBox>
            </div>
        </div>

    <div class="row">
        <%-- Ocultamos la columna de Cant. Desde --%>
        <div class="col-md-2" style="display: none;">
            <div class="form-group">
                <label>Cant. Desde</label>
                <asp:TextBox ID="txtCantidadDesdeAgregar" runat="server" CssClass="form-control" TextMode="Number" step="0.01" Text="1.00"></asp:TextBox>
            </div>
        </div>
<%-- Ocultamos la columna de Cant. Mínima --%>
        <div class="col-md-2" style="display: none;">
            <div class="form-group">
                <label>Cant. Mínima</label>
                <asp:TextBox ID="txtCantidadMinimaAgregar" runat="server" CssClass="form-control" TextMode="Number" Text="1"></asp:TextBox>
            </div>
        </div>
        </div>
        
        <div class="col-md-2">
            <div class="form-group">
                <label>% Aumento</label>
                <asp:TextBox ID="txtAumentoAgregar" runat="server" CssClass="form-control" TextMode="Number" step="0.01" Text="0.00" 
                    onkeyup="calcularPrecioFinal()" onchange="calcularPrecioFinal()"></asp:TextBox>
            </div>
        </div>
        
        <div class="col-md-6 text-right">
            <div class="form-group" style="margin-top: 25px;">
                <asp:Button ID="btnGuardarProducto" runat="server" Text="Guardar Producto" CssClass="btn btn-success" OnClick="btnGuardarProducto_Click" />
                <asp:Button ID="btnCancelarProducto" runat="server" Text="Cancelar" CssClass="btn btn-default" OnClick="btnCancelarProducto_Click" />
            </div>
        </div>
    </div>
</asp:Panel>





                <%-- IMPORTANTE: DataKeyNames="codigo" guarda el dlp.codigo de forma oculta --%>
                
                <asp:GridView ID="gvProductosLista" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-bordered table-hover" 
                    EmptyDataText="No hay productos registrados en esta lista actualmente."
                    DataKeyNames="codigo"
                    OnRowEditing="gvProductosLista_RowEditing"
                    OnRowCancelingEdit="gvProductosLista_RowCancelingEdit"
                    OnRowUpdating="gvProductosLista_RowUpdating">
                    
<Columns>
                        <%-- 1. Producto (Bloqueado) --%>
                        <asp:BoundField DataField="producto" HeaderText="Producto" ReadOnly="True" />
                        
                        <%-- 2. Medida (Bloqueado) --%>
                        <asp:BoundField DataField="medida" HeaderText="Medida" ReadOnly="True" />
                        
<%-- 3. % Dcto. (EDITABLE) --%>
                        <asp:TemplateField HeaderText="% Dcto.">
                            <ItemTemplate>
                                <asp:Label ID="lblDctoProd" runat="server" Text='<%# Eval("porcentaje_descuento", "{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
<asp:TextBox ID="txtEditDctoProd" runat="server" 
    Text='<%# Eval("porcentaje_descuento").ToString().Replace(",", ".") %>' 
    CssClass="form-control dcto-input" 
    TextMode="Number" step="0.01" Width="80px"
    oninput="calcularPrecioFinalEdicion(this)">
</asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <%-- 4. % Aumento (EDITABLE) --%>
                        <asp:TemplateField HeaderText="% Aumento">
                            <ItemTemplate>
                                <asp:Label ID="lblAumentoProd" runat="server" Text='<%# Eval("porcentaje_aumento", "{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
<asp:TextBox ID="txtEditAumentoProd" runat="server" 
    Text='<%# Eval("porcentaje_aumento").ToString().Replace(",", ".") %>' 
    CssClass="form-control aumento-input" 
    TextMode="Number" step="0.01" Width="80px"
    oninput="calcularPrecioFinalEdicion(this)">
</asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <%-- 5. Precio Base --%>
                        <asp:TemplateField HeaderText="Precio Base">
                            <ItemTemplate>
                                <asp:Label ID="lblPrecioBase" runat="server" Text='<%# Eval("precio_base", "{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblEditPrecioBase" runat="server" Text='<%# Eval("precio_base", "{0:N2}") %>' CssClass="precio-base-label"></asp:Label>

                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <%-- 6. Precio Final (AHORA ES TEMPLATE PARA PODER EDITARLO EN VIVO) --%>
                        <asp:TemplateField HeaderText="Precio Final" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0056b3">
                            <ItemTemplate>
                                <asp:Label ID="lblPrecioFinal" runat="server" Text='<%# Eval("precio_final", "{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblEditPrecioFinal" runat="server" 
                                    Text='<%# Eval("precio_final", "{0:N2}") %>' 
                                    CssClass="precio-final-label"></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <%-- 7. Acciones (Se mantiene igual que el paso anterior) --%>
                        <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEditarProducto" runat="server" CommandName="Edit" CssClass="btn btn-warning btn-sm">Editar</asp:LinkButton>
                                <asp:LinkButton ID="lnkQuitar" runat="server" CommandName="Delete" CssClass="btn btn-danger btn-sm" CommandArgument='<%# Eval("codigo") %>' OnClick="btnQuitar_Click" OnClientClick="return confirm('¿Está seguro de quitar este producto de la lista?');">Quitar</asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkGuardarProducto" runat="server" CommandName="Update" CssClass="btn btn-success btn-sm">Guardar</asp:LinkButton>
                                <asp:LinkButton ID="lnkCancelarProducto" runat="server" CommandName="Cancel" CssClass="btn btn-default btn-sm">Cancelar</asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <HeaderStyle BackColor="#5cb85c" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                
            </div>
        </div>
    </div>


        <HeaderStyle BackColor="#5cb85c" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                
            </div>
        </div>
    </div> 
    

    <script type="text/javascript">

        // =========================================================
        // 1. FUNCIÓN PARA EL PANEL DE "AGREGAR NUEVO PRODUCTO"
        // =========================================================
        function calcularPrecioFinalAgregar() {
            // Encontramos las cajas exactas usando los IDs reales de ASP.NET
            var cajaPrecioBase = document.getElementById('<%= txtPrecioAgregar.ClientID %>');
            var cajaDescuento = document.getElementById('<%= txtDctoAgregar.ClientID %>');
            var cajaAumento = document.getElementById('<%= txtAumentoAgregar.ClientID %>');
            var cajaPrecioEspecial = document.getElementById('<%= txtPrecioEspecialAgregar.ClientID %>');

            // Verificamos que las cajas existan en la pantalla
            if (cajaPrecioBase && cajaDescuento && cajaAumento && cajaPrecioEspecial) {
                // Extraemos los números (cambiando comas por puntos por si acaso)
                var precioBase = parseFloat(cajaPrecioBase.value.replace(',', '.')) || 0;
                var descuento = parseFloat(cajaDescuento.value.replace(',', '.')) || 0;
                var aumento = parseFloat(cajaAumento.value.replace(',', '.')) || 0;

                // Hacemos la matemática
                var montoDescuento = precioBase * (descuento / 100);
                var montoAumento = precioBase * (aumento / 100);
                var precioFinal = precioBase - montoDescuento + montoAumento;

                // Evitamos negativos
                if (precioFinal < 0) precioFinal = 0;

                // Escribimos el resultado con 2 decimales
                cajaPrecioEspecial.value = precioFinal.toFixed(2);
            }
        }

        // =========================================================
        // 2. FUNCIÓN PARA LA GRILLA DE "EDITAR PRODUCTO" (Inline)
        // =========================================================
        function calcularPrecioFinalEdicion(input) {
            // Encontramos la fila (TR) en la que el usuario está escribiendo
            var fila = input.closest('tr');

            // Buscamos los controles de esa fila específica
            var txtDcto = fila.querySelector('.dcto-input');
            var txtAumento = fila.querySelector('.aumento-input');
            var lblPrecioBase = fila.querySelector('.precio-base-label');
            var lblPrecioFinal = fila.querySelector('.precio-final-label');

            if (txtDcto && txtAumento && lblPrecioBase && lblPrecioFinal) {
                // Extraemos los valores. Si dejan la caja en blanco, asumimos 0
                var dcto = parseFloat(txtDcto.value) || 0;
                var aumento = parseFloat(txtAumento.value) || 0;

                // Limpiamos el texto del precio base (quitamos puntos de miles y cambiamos coma por punto decimal)
                var precioBaseTexto = lblPrecioBase.innerText || lblPrecioBase.textContent;
                precioBaseTexto = precioBaseTexto.replace(/\./g, '').replace(',', '.');
                var precioBase = parseFloat(precioBaseTexto) || 0;

                // MATEMÁTICA: Precio Base + (Monto Aumento) - (Monto Descuento)
                var montoAumento = precioBase * (aumento / 100);
                var montoDcto = precioBase * (dcto / 100);
                var precioFinal = precioBase + montoAumento - montoDcto;

                // Evitamos que el precio quede en negativo por accidente
                if (precioFinal < 0) precioFinal = 0;

                // Mostramos el resultado formateado a 2 decimales y con coma
                lblPrecioFinal.innerText = precioFinal.toFixed(2).replace('.', ',');
            }
        }

    </script>
    
    </asp:Content>


