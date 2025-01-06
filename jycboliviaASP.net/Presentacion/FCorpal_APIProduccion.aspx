<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCorpal_APIProduccion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_APIProduccion" Async="true" MasterPageFile="~/PlantillaNew.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    
</asp:Content>

<asp:Content ID="Contentn2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="" style="padding-top: 1em;">
        <div class="">
            <div class="col-md-11 col-md-offset-1">
                <div class="panel panel-success class">

                    <!--------------------------------          API POST PRODUCCION PARTEproduccion  (registra datos vacios - La cadena de entrada no tiene el formato correcto.)       ----------------------->

                    <div class="container-POSTProduccion p-4 rounded">
                        <div class="container_tittle rounded">
                            <h3 class="text_tittle p-3">Registro Producción (F)</h3>
                        </div>

                        <!--  FORMULARIO PARA REGISTRAR PRODUCCION  -->
                        <div class="form_col1 row">
                            <div class="mb-3 col-12 col-xs-6 col-sm-6 col-md-5 col-lg-4">
                                <div class="col-9 col-sm-12 col-md-12 col-lg-12">
                                    <label class="form-label">Referencia:</label>
                                    <asp:TextBox ID="txt_referencia" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-10 col-sm-12 col-md-12 col-lg-12">
    <label class="form-label">Glosa:</label>
    <asp:TextBox ID="txt_glosa" runat="server" CssClass="form-control"></asp:TextBox>
</div>
                                
                            </div>

                            <div class="mb-3 col-12 col-xs-6 col-sm-6 col-md-4 col-lg-4">
                                <div class="col-9 col-sm-12 col-md-10 col-lg-10">
                                    <label class="form-label">Linea Producción:</label>
                                    <asp:DropDownList ID="dd_lineaProduccion" runat="server" CssClass="form-select dd_fsmall">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                
                                
                                <div class="col-8 col-sm-9 col-md-10 col-lg-10">
                                    <label class="form-label">Realizar Descarga:</label>
                                    <asp:DropDownList ID="dd_realDescarga" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Si" Value="true" />
                                        <asp:ListItem Text="No" Value="false" />
                                    </asp:DropDownList>
                                </div>
                                
                            </div>
                            <div class="mb-3 col-12 col-xs-6 col-sm-6 col-md-4 col-lg-4">
                                <div class="col-10 col-sm-12 col-md-10 col-lg-12">
                                    <label class="form-label">Responsable:</label>
                                    <asp:TextBox ID="txt_codResponsable" runat="server" CssClass="form-control" OnTextChanged="txt_codResponsable_TextChanged"></asp:TextBox>
                                    <asp:GridView ID="gv_listResponsables" runat="server" EnableViewState="true" AutoGenerateColumns="false" CssClass="gv_listResponsables table table-bordered small" OnSelectedIndexChanged="gv_listResponsables_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
                                            <asp:BoundField DataField="CodigoContacto" HeaderText="Codigo"/>
                                            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" HtmlEncode="false"/>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                
                            <div class="col-6 col-sm-10 col-md-10 col-lg-8">
                                <label class="form-label">Ítem Análisis:</label>
                                <asp:TextBox ID="txt_itemAnalisis" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            
                            </div>

                        </div>







                        <!--  LISTA DETALLE PRODUCCION  -->
                        <div class="container_formADDProducto col-lg-12">
                            <div class="container_subtittle mb-2">
                                
                                <h5 CssClass="text_tittle" runat="server">Detalle Producto</h5>
                            </div>

    
                            <div class="row col-lg-12">
                                <!--  columna 1  -->
                                <div class="table-responsive col-sm-8 col-md-8 col-lg-8">
                                    <table id="tblDetalleProductoProduccion" border="1" class="table_ADDProd table-bordered table-striped col-sm-12 col-md-10 col-lg-12">
                                        <thead class="table_tr">
                                            <tr>
                                                <th class="th_cprod txt-center">Codigo Producto</th>
                                                <th class="th_cant txt-center">Cantidad</th>
                                                <th class="th_umed txt-center">Unidad Medida</th>
                                                <th class="th_crec txt-center">Codigo Receta</th>
                                            </tr>
                                        </thead>
                                        <tr>
                                            <td>
                                                <input class="inputText" type="text" name="codigoProducto0" /></td>
                                            <td>
                                                <input class="inputNumber" type="number" name="cantidad0" /></td>
                                            <td>
                                                <input class="inputNumber" type="number" name="unidadMedida0" /></td>
                                            <td>
                                                <input class="inputText" type="text" name="codigoReceta0" /></td>
                                        </tr>
                                    </table>
                                </div>
                                <!--  columna 2  -->
                                <div class="container_btn col-sm-4 col-md-4 col-lg-4 d-flex flex-column align-items-center">
                                    <asp:Button ID="btnAddRow" runat="server" Text="Agregar Fila" CssClass="btn btn-secondary mb-2 btn-sm" OnClientClick="addRowProduccion(); return false" />
                                    <asp:Button ID="btn_Prod_parteProd" runat="server" Text="Registrar Produccion" CssClass="btn btn-success btn-sm" OnClick="btn_Insert_parteProd_Click" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <br />
        <!---------------------------------------       API GET PRODUCCION DETALLE  (Muestra datos vacios)  ------------------------------------------------->
        <div class="container-GETProduccionDet p-4 rounded">
            <div class="container_tittle rounded">
                <h3 class="text_tittle p-3">Vista Produccion Detalle (F)</h3>
            </div>

            <div class="container_input mb-3 row">
                <div class="container_textbox col-6 col-sm-4 col-md-4 col-lg-4">
                    <label class="form-label">Numero Parte de Produccion:</label>
                    <asp:TextBox ID="txt_numProduccion1" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-4 col-sm-4 col-md-2 align-items-end d-flex">
                    <asp:Button ID="btn_buscProduccion" runat="server" Text="Buscar Produccion" CssClass="btn btn-dark btn-sm" OnClick="btn_buscProduccion_Click" />
                </div>
            </div>



            <div class="container_gv1">
                <asp:GridView ID="gv_produccion" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="NumeroParteProduccion" HeaderText="numPartProduccion" SortExpression="nPartProd" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fech" />
                        <asp:BoundField DataField="Referencia" HeaderText="Referencia" SortExpression="ref" />
                        <asp:BoundField DataField="CodigoResponsable" HeaderText="codResponsable" SortExpression="cResp" />
                        <asp:BoundField DataField="ItemAnalisis" HeaderText="ItemAnalisis" SortExpression="iAnali" />
                        <asp:BoundField DataField="LineaProduccion" HeaderText="LineaProduccion" SortExpression="lProdc" />
                        <asp:BoundField DataField="RealizaDescarga" HeaderText="realizaDescarga" SortExpression="rDescarg" />
                        <asp:BoundField DataField="Glosa" HeaderText="Glosa" SortExpression="glo" />
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="usu" />
                    </Columns>
                    <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                    <RowStyle BackColor="white" />
                    <AlternatingRowStyle BackColor="#f8f9fa" />
                </asp:GridView>
            </div>

            <div class="container_gv2">
                <asp:GridView ID="gv_detalle" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false">

                    <Columns>
                        <asp:BoundField DataField="Item" HeaderText="item" SortExpression="item" />
                        <asp:BoundField DataField="CodigoProducto" HeaderText="codProducto" SortExpression="codProd" />
                        <asp:BoundField DataField="Cantidad" HeaderText="cantidad" SortExpression="cant" />
                        <asp:BoundField DataField="UnidadMedida" HeaderText="uMedida" SortExpression="uMed" />
                        <asp:BoundField DataField="CodigoReceta" HeaderText="codReceta" SortExpression="cRecet" />
                    </Columns>

                    <HeaderStyle BackColor="#ffcc00" ForeColor="black" />
                    <RowStyle BackColor="white" />
                    <AlternatingRowStyle BackColor="#f8f9fa" />
                </asp:GridView>
            </div>
        </div>
            </div>
        </div>

        </div>
        <br />
        <script src="../js/jsApi.js" type="text/javascript"></script>
</asp:Content>
