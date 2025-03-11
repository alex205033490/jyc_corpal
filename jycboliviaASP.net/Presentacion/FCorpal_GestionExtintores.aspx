<%@ Page Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_GestionExtintores.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_GestionExtintores" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1" >
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            var table = $(".table-sticky");

            if (table.find("thead").length === 0) {
                table.prepend("<thead>" + table.find("tr:first").html() + "</thead>");
                table.find("tr:first").remove();
            }

        })

        function validarDecimal(input) {
            var valor = input.value;
            var regex = /^\d*(?:[.,]\d{0,2})?$/;

            if (!regex.test(valor)) {
                input.setCustomValidity("Solo se permiten números con hasta 2 decimales.")
            } else {
                input.setCustomValidity("");
            } 
        }

        function validarAnioMin(input) {
            var valor = input.value.trim();
            var regex = /^\d+$/;

            var anioActual = new Date().getFullYear();

            if (!regex.test(valor)) {
                input.setCustomValidity("Solo se permiten números enteros.");
            } else if (parseInt(valor) < anioActual) {
                input.setCustomValidity("El año debe ser mayor o igual a " + anioActual + ".");
            } else {
                input.setCustomValidity("");
            }
        }
    </script>

    <style>
        .container-registro{
            padding-left:20px;
            padding-right:20px;
            font-size:14px;
           
        }
        .columna1, .columna2, .columna3{
            margin: 5px;
            display:flex;
            flex-direction:column;
        }
        input[type=text], input[type="number"], select {
            padding: 3px;
            margin-bottom: 7px;
            width: 80%;
            border-radius: 6px;
            border:1px solid #0000005c;
        }
        input[type=text]:hover, input[type="number"]:hover, select:hover {
            background-color:#00000038;
        }
        input[type=text]:focus, input[type="number"]:focus, select:focus {
            background-color:#0034ff1c ;
            box-shadow: 2px 2px 10px black;
        }

        .btn-success:hover{
            box-shadow:2px 2px 15px green;
            
        }
        .btn-danger:hover{
            box-shadow:2px 2px 15px red;
    
        }   

        .container-listaRegistrosExtintores{
            padding:10px;
            height:350px;
        }

        .gv_registrosExtintores{
            overflow-y:auto;
            overflow-x:auto;
            font-size:small;
            border:2px solid black;
        }
        
        .gv_registrosExtintores th{
            background-color:orange;
            padding:6px;
            border:2px solid black;
        }
        .gv_registrosExtintores td{
            background-color:white;
            padding:6px; 
        }
        .table-sticky th{
            position: sticky !important;
            top:-10px !important;
            z-index:100 !important;
            border:1px solid black;
        }
        .form-registro{
            background-color:#ffa93e47;
        }

        

    </style>
    
    <div class="card">
        <div class="card-header bg-warning"> Gestión de Extintores </div>
        <div class="container-form">

            <div class="container-registro bg-white"> Registro de Extintores
                <div class="form-registro row">
                    <div class="columna1 col-lg-3">
                    <label>Area:</label>
                    <asp:DropDownList ID="dd_area" CssClass="dd_area" runat="server">
                        <asp:ListItem> Selecciona un área:</asp:ListItem>
                        <asp:ListItem> Almacén de materia prima 1 </asp:ListItem>
                        <asp:ListItem> Almacén de materia prima 2 </asp:ListItem>
                        <asp:ListItem> Almacén de materia prima 3 </asp:ListItem>
                        <asp:ListItem> Almacén de producto terminado </asp:ListItem>
                        <asp:ListItem> Almacén maestro </asp:ListItem>
                        <asp:ListItem> Alta tensión </asp:ListItem>
                        <asp:ListItem> Batido de semilla </asp:ListItem>
                        <asp:ListItem> Compresores </asp:ListItem>
                        <asp:ListItem> Container </asp:ListItem>
                        <asp:ListItem> envolsado de semilla </asp:ListItem>
                        <asp:ListItem> Evaporadores de gas </asp:ListItem>
                        <asp:ListItem> Galpon 1 - Galletas </asp:ListItem>
                        <asp:ListItem> Galpon 2 - Papas </asp:ListItem>
                        <asp:ListItem> Galpon 3 - Nachos</asp:ListItem>
                        <asp:ListItem> Mantenimiento </asp:ListItem>
                        <asp:ListItem> Oficina Administración</asp:ListItem>
                        <asp:ListItem> Pasillo sazón de ají </asp:ListItem>
                        <asp:ListItem> Tasques de gas </asp:ListItem>
                        <asp:ListItem> Tostado de semilla </asp:ListItem>
                    </asp:DropDownList>

                    <label>Marca:</label>
                    <asp:TextBox ID="txt_marca" runat="server" autocomplete="off"></asp:TextBox>

                    <label>Fecha de Carga:</label>
                    <asp:TextBox ID="txt_fechadCarga" runat="server" autocomplete="off" ></asp:TextBox>
                    <asp:CalendarExtender id="txt_fechadCarga_CalendarExtender" runat="server" TargetControlID="txt_fechadCarga"/>

                    <label>Próxima año de Prueba Hidrostatica:</label>
                    <asp:TextBox ID="txt_fechaProximaPrueba" runat="server" autocomplete="off" onInput="validarAnioMin(this);"></asp:TextBox>
                    </div>

                    <div class="columna2 col-lg-3">
                    <label>Detalle:</label>
                    <asp:TextBox ID="txt_detalle" runat="server" autocomplete="off"></asp:TextBox>

                    <label>Capacidad (Kg):</label>
                    <asp:TextBox ID="txt_capacidad" runat="server" autocomplete="off" onInput="validarDecimal(this);" ></asp:TextBox>


                    <label>Fecha Proxima Carga:</label>
                    <asp:TextBox ID="txt_fechaProximaCarga" runat="server" autocomplete="off"></asp:TextBox>
                    <asp:CalendarExtender id="txt_fechaProximaCarga_calendarExtender" runat="server" TargetControlID="txt_fechaProximaCarga"/>

                    <asp:Button ID="btn_guardarForm" runat="server" Text="Registrar" CssClass="btn btn-success" Style="width: 80%;" OnClick="btn_guardarForm_Click" />
                    </div>

                    <div class="columna3 col-lg-3">
                    <label>Agente Extintor:</label>
                    <asp:TextBox ID="txt_agenteExtintor" runat="server" autocomplete="off"></asp:TextBox>

                    <label>Codigo Sistema:</label>
                    <asp:TextBox ID="txt_codSistema" runat="server" autocomplete="off"></asp:TextBox>

                    <label>Estado Extintor:</label>
                    <asp:DropDownList ID="dd_estadoExtintor" runat="server">
                        <asp:ListItem>Selecciona un estado:</asp:ListItem>
                        <asp:ListItem>Recarga</asp:ListItem>
                        <asp:ListItem>Mantenimiento</asp:ListItem>
                        <asp:ListItem>Nuevo</asp:ListItem>
                    </asp:DropDownList>


                    </div>


                </div>
            </div>

            <br />
            <div class="container-btns row col-lg-6">
                <div class="col-lg-6">
                    <asp:Button id="btn_anularRegistro" runat="server" Text="Anular" CssClass="btn btn-danger" style="width:80%;" OnClick="btn_anularRegistro_Click"/>
                </div>

                <div class="col-lg-6">
                    <asp:Button ID="btn_updateRegistro" runat="server" Text="Actualizar" CssClass="btn btn-success" Style="width: 80%;" OnClick="btn_updateRegistro_Click" />
                </div>
            </div>
            <div class="container-listaRegistrosExtintores table-responsive col-lg-11"> Lista de registros 
                <asp:GridView ID="gv_registrosExtintores" runat="server" DataKeyNames="codigo" CssClass="table table-sticky table-striped table-responsive gv_registrosExtintores" 
                    AutoGenerateColumns="false" OnRowDataBound="gv_registrosExtintores_row_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" CssClass="chkSelect"  runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="codigo" HeaderText="Código"/>
                        <asp:BoundField DataField="fechagra" HeaderText="Fecha Creación" />
                        <asp:BoundField DataField="horagra" HeaderText="Hora Creación" />
                        <asp:TemplateField HeaderText="Detalle">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_detalle" runat="server" Width="160px" BackColor="yellow" autoComplete="off"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Área">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_area" runat="server" Width="100px" BackColor="yellow" autoComplete="off"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Agente extintor">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_aextintor" runat="server" Width="120px" BackColor="yellow" autoComplete="off"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Marca">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_marca" runat="server" Width="90px" BackColor="yellow" autoComplete="off"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Capacidad">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_capacidad" runat="server" BackColor="yellow" autoComplete="off"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>                        

                        <asp:TemplateField HeaderText="Código Sistema">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_codsistema" width="70px" runat="server" BackColor="yellow" autoComplete="off"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fecha de Carga">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_fdcarga" runat="server" Width="90px" BackColor="yellow" autoComplete="off"></asp:TextBox>
                                <asp:CalendarExtender id="txt_fdcarga_calendarExtender" runat="server" TargetControlID="txt_fdcarga"/>
                            </ItemTemplate>
                        </asp:TemplateField>                        

                        <asp:TemplateField HeaderText="Fecha Proxima Carga">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_fproximacarga" runat="server" Width="90px" BackColor="yellow" autoComplete="off"></asp:TextBox>
                                <asp:CalendarExtender id="txt_fproximacarga_calendarExtender" runat="server" TargetControlID="txt_fproximacarga"/>

                            </ItemTemplate>
                        </asp:TemplateField>

                       
                        <asp:TemplateField HeaderText="Estado Extintor">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_estadoextintor" runat="server" Width="100px" BackColor="yellow" autoComplete="off"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Próxima prueba hidrostatica">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_pPruebaHidrostatica" runat="server" BackColor="Yellow" AutoComplete="off"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="nombreresp" HeaderText="Responsable" HtmlEncode="false"/>

                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </div>



</asp:Content>