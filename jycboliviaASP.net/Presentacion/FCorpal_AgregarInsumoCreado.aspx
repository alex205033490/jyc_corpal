<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PlantillaNew.Master" CodeBehind="FCorpal_AgregarInsumoCreado.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_AgregarInsumoCreado" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_APIUpon.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
      
      
      .CompletionList
      {
          padding: 5px 0 ;
          margin: 2px 0 0;            
        /*  position:absolute;  */
          height:150px;
          width:200px;
          background-color: White;
          cursor: pointer;
          border: solid ;  
          border-width: 1px;    
          font-size:x-small;
          overflow: auto;
                      }
                      
         .CompletionlistItem
         {
             font-size:x-small;           
          }             
                      
      .CompletionListMighlightedItem
      {
           background-color: Green;
           color: White;
          /* color: Lime;
          padding: 3px 20px;
          text-decoration: none;           
          background-repeat: repeat-x;
          outline: 0;*/            
          } 
      
      
       </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="" style="padding-top: 1em;">
        <div class="row">
            <div class="col-md-11 col-md-offset-1">
                <div class="panel panel-success class">



                    <!------------------------          Form agregar nuevo insumo creado          ------------------------------>
                    <div class="POST_inventarioIngreso p-4 bg-light border rounded">
                        <h2 class="text_tittle2">Registro Nuevo Insumo Creado</h2>

                        <div class="row mb-3 col-xs-12 col-sm-12 col-md-12 col-lg-12">

                            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5">
                                <label class="form-label">Nombre Insumo Creado:</label>
                                <asp:TextBox ID="txt_nomInsumoCreado" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Ingrese el nombre del nuevo insumo creado"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5 col-md-3 col-lg-3">
                                <label class="form-label">Medida:</label>
                                <asp:TextBox ID="txt_medidaInsumoCreado" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Ingrese la medida"></asp:TextBox>
                            </div>

                        </div>
                        <br />
                        <!-- JS -->
                        <script>
                            function onItemSelected(sender, args) {
                                console.log("Item selected");
                                var dataItem = args.get_value();
                                console.log("dataItem");
                                var parts = dataItem.split("|");

                                if (parts.length > 1) {
                                    var codigo = parts[0];
                                    var nombre = parts[1];
                                    var medida = parts[2];

                                    document.getElementById('<%= txt_Nombre.ClientID %>').value = nombre;
                                    document.getElementById('<%= txt_codInsumo.ClientID %>').value = codigo;
                                    document.getElementById('<%= txt_Medida.ClientID %>').value = medida;
                                }
                            }
                        </script>


                        <!-- Lista Insumos  -->
                        <div class="form_insumo col-xs-12 col-sm-8 col-md-8 col-lg-8">
                            <h5 class="form-label">Lista Insumos</h5>
                            <div class="container_insumos">
                                <table id="tblAddInsumo" class="table table-bordered table-striped col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                    <thead>
                                        <tr>
                                            <th>CodInsumo</th>
                                            <th>Nombre</th>
                                            <th>Cantidad</th>
                                            <th>Medida</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txt_codInsumo" runat="server" CssClass="form-control" Font-Size="Small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_Nombre" runat="server"  CssClass="form-control" Font-Size="Small" Width="200px"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                                 TargetControlID="txt_Nombre" CompletionSetCount="12" MinimumPrefixLength="1" 
                                                 ServiceMethod="GetListaInsumo" UseContextKey="True" CompletionListCssClass="CompletionList" 
                                                 CompletionListItemCssClass="CompletionlistItem" 
                                                 CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" 
                                                 CompletionInterval="10" OnClientItemSelected ="onItemSelected">
                                                </asp:AutoCompleteExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_Cantidad" runat="server" CssClass="form-control" Font-Size="Small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_Medida" runat="server" CssClass="form-control" type="text" Font-Size="Small"></asp:TextBox></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Button ID="btn_ADD" runat="server" Text=" + " CssClass="btn btn-warning" OnClick="btn_ADD_Click"/>
                            </div>
                        </div>
                        <br />

                        <div class="container_gv col-md-8 col-lg-8">
                            <asp:GridView runat="server" ID="gv_insumoCreado" CssClass="gridview" AutoGenerateColumns="false" OnRowCommand="gv_insumoCreado_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="CodInsumo" HeaderText="CodInsumo" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" DataFormatString="{0:F2}"/>
                                    <asp:BoundField DataField="Medida" HeaderText="Medida" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminarFila" runat="server" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' Text="Eliminar" CssClass="btn btn-danger" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:Button ID="btn_registrarICreado2" runat="server" Text="Registrar Nuevo InsumoCreado " CssClass="btn btn-warning" OnClick="btn_registrarICreado2_Click" />
                        </div>
                        <br />
                    </div>


                    <!------------------------          Form modificar insumo creado          ------------------------------>
                    <div class="POST_inventarioIngreso p-4 bg-light border rounded">
                        <h2 class="text_tittle2">Modificar Insumo Creado</h2>

                        <div class="row mb-3 col-xs-12 col-sm-12 col-md-12 col-lg-12">

                            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5">
                                <label class="form-label">Nombre Del Insumo Creado:</label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Ingrese el nombre del nuevo insumo creado"></asp:TextBox>
                            </div>

                        </div>
                        <br />

                        <!-- Lista Insumos  -->
                        <div class="form_insumo col-xs-12 col-sm-8 col-md-8 col-lg-8">
                            <h5 class="form-label">Lista Insumos</h5>
                            <div class="container_insumos">
                                <table id="tblAddInsumo" class="table table-bordered table-striped col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                    <thead>
                                        <tr>
                                            <th>CodInsumo</th>
                                            <th>Nombre</th>
                                            <th>Cantidad</th>
                                            <th>Medida</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Font-Size="Small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="TextBox4" runat="server"  CssClass="form-control" Font-Size="Small" Width="200px"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" 
                                                 TargetControlID="txt_Nombre" CompletionSetCount="12" MinimumPrefixLength="1" 
                                                 ServiceMethod="GetListaInsumo" UseContextKey="True" CompletionListCssClass="CompletionList" 
                                                 CompletionListItemCssClass="CompletionlistItem" 
                                                 CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" 
                                                 CompletionInterval="10" OnClientItemSelected ="onItemSelected">
                                                </asp:AutoCompleteExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" Font-Size="Small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" type="text" Font-Size="Small"></asp:TextBox></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Button ID="Button1" runat="server" Text=" + " CssClass="btn btn-warning" OnClick="btn_ADD_Click"/>
                            </div>
                        </div>
                        <br />

                        <div class="container_gv col-md-8 col-lg-8">
                            <asp:GridView runat="server" ID="GridView1" CssClass="gridview" AutoGenerateColumns="false" OnRowCommand="gv_insumoCreado_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="CodInsumo" HeaderText="CodInsumo" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" DataFormatString="{0:F2}"/>
                                    <asp:BoundField DataField="Medida" HeaderText="Medida" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminarFila" runat="server" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' Text="Eliminar" CssClass="btn btn-danger" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:Button ID="Button2" runat="server" Text="Registrar Nuevo InsumoCreado " CssClass="btn btn-warning" OnClick="btn_registrarICreado2_Click" />
                        </div>
                        <br />
                    </div>



                </div>
                <br>
                <br>
            </div>
        </div>
    </div>
    <script src="../js/jsApi.js" type="text/javascript"></script>
</asp:Content>
