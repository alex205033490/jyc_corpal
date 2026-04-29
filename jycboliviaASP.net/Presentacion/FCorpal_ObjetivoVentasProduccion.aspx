<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_ObjetivoVentasProduccion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_ObjetivoProduccion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_SimecModificar.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .CompletionList {
            padding: 5px 0 ;
            margin: 2px 0 0;            
            height:150px;
            width:200px;
            background-color: White;
            cursor: pointer;
            border: solid ;  
            border-width: 1px;    
            font-size:x-small;
            overflow: auto;
        }
        .CompletionlistItem {
            font-size:x-small;            
        }              
        .CompletionListMighlightedItem {
             background-color: Green;
             color: White;
        }      
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card" >
      <div class="card-header bg-warning text-black font-weight-bold">
        Gestión de Objetivos de Ventas Producción
      </div>
      
      <div class="card-body">

        <asp:Panel ID="pn_CargaMasiva" runat="server" Visible="true">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <h5 class="text-primary m-0">Carga Masiva de Productos</h5>
                <asp:Button ID="bt_insertarMasivo" class="btn btn-primary" runat="server" Text="Guardar Seleccionados" onclick="bt_insertarMasivo_Click" />
            </div>
            
            <div style="height: 380px; overflow-y: auto; border: 1px solid #ccc; border-radius: 5px;" class="mb-4">
                <asp:GridView ID="gv_cargaMasiva" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-sm table-striped table-hover mb-0" DataKeyNames="codigo">
                    <HeaderStyle BackColor="#343a40" ForeColor="White" CssClass="position-sticky top-0" />
                    <Columns>
                        <asp:TemplateField HeaderText="✔">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSeleccionar" runat="server" CssClass="form-check-input" style="position: relative; margin-left: 0;" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="codigo" HeaderText="Cód." ReadOnly="True" Visible="false" />
                        <asp:BoundField DataField="producto" HeaderText="Producto" ReadOnly="True" />
                        
                        <asp:TemplateField HeaderText="Fecha Límite">
                            <ItemTemplate>
                                <asp:TextBox ID="tx_fechaGrid" runat="server" CssClass="form-control form-control-sm" Width="120px"></asp:TextBox>
                                <asp:CalendarExtender ID="ce_fechaGrid" runat="server" TargetControlID="tx_fechaGrid"></asp:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <asp:TextBox ID="tx_cantGrid" runat="server" CssClass="form-control form-control-sm" Width="80px" Text="0"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="medida" HeaderText="Medida" ReadOnly="True" />

                        <asp:TemplateField HeaderText="Detalle">
                            <ItemTemplate>
                                <asp:TextBox ID="tx_detalleGrid" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="1" Width="100%"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pn_Edicion" runat="server" Visible="false" CssClass="bg-light p-3 border rounded mb-4">
            <h5 class="text-secondary mb-3 border-bottom pb-2">Modificar o Eliminar Registro</h5>
            <div class="row">
                <div class="col-md-8">
                    <table class="mb-2">   
                        <tr>
                            <td><asp:Label ID="Label36" runat="server" Text="Fecha Limite :" CssClass="font-weight-bold mr-2"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="tx_fechalimite" class="form-control" runat="server" Width="150px" ></asp:TextBox>
                                <asp:CalendarExtender ID="tx_fechalimite_CalendarExtender" runat="server" TargetControlID="tx_fechalimite"></asp:CalendarExtender>
                            </td>
                            <td><asp:Button ID="bt_buscar" class="btn btn-success ml-2" runat="server" onclick="bt_buscar_Click" Text="Buscar" /></td>
                        </tr>  
                        <tr>
                            <td class="pt-2"><asp:Label ID="Label40" runat="server" Text="Producto Nax :" CssClass="font-weight-bold mr-2"></asp:Label></td>
                            <td colspan="2" class="pt-2">
                                <asp:DropDownList ID="dd_productosNax" class="btn btn-secondary dropdown-toggle text-left" runat="server" 
                                    Width="100%" onselectedindexchanged="dd_productosNax_SelectedIndexChanged" AutoPostBack="True" Enabled="False">
                                </asp:DropDownList>
                                <small class="text-muted d-block">El producto no se puede cambiar en edición.</small>
                            </td>
                        </tr>
                    </table>

                    <table class="mb-2 mt-3">        
                        <tr>
                            <td><asp:Label ID="Label31" runat="server" Text="Cantidad:" CssClass="font-weight-bold mr-2"></asp:Label></td>
                            <td><asp:TextBox ID="tx_cantcajas" class="form-control" runat="server" Width="120px"></asp:TextBox></td>
                            <td><asp:Label ID="Label32" runat="server" Text="Medida:" CssClass="font-weight-bold ml-3 mr-2"></asp:Label></td>
                            <td><asp:TextBox ID="tx_medida" class="form-control" runat="server" Width="120px" Enabled="False"></asp:TextBox></td>            
                        </tr>
                    </table>

                    <table class="w-100 mt-3">
                        <tr>
                            <td style="vertical-align: top; width: 100px;"><asp:Label ID="Label35" runat="server" Text="Detalle :" CssClass="font-weight-bold"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="tx_detalle" class="form-control" runat="server" Height="80px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="mt-4 border-top pt-3">
                <asp:Button ID="bt_modificar" class="btn btn-warning" runat="server" Text="Guardar Cambios" onclick="bt_modificar_Click" />
                <asp:Button ID="bt_eliminar" class="btn btn-danger ml-2" runat="server" Text="Eliminar Registro" onclick="bt_eliminar_Click" />
                <asp:Button ID="bt_limpiar" class="btn btn-light ml-2 border" runat="server" Text="Limpiar" onclick="bt_limpiar_Click" />
                <asp:Button ID="bt_cancelarEdicion" class="btn btn-secondary float-right" runat="server" Text="Cancelar / Volver" onclick="bt_cancelarEdicion_Click" />
            </div>
        </asp:Panel>

        <h5 class="mt-4 border-bottom pb-2">Registros de Objetivos Actuales</h5>
        <div class="DatosProyecto mb-3" style="overflow-x: auto;">
             <asp:GridView ID="gv_objetivoProduccion" 
                  runat="server" BackColor="White" 
                 BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                 Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
                 onselectedindexchanged="gv_reciboIngresoEgreso_SelectedIndexChanged">
                 <AlternatingRowStyle BackColor="#CCCCCC" />
                 <Columns>
                     <asp:CommandField ShowSelectButton="True" SelectText="Editar" />
                 </Columns>
                 <FooterStyle BackColor="#CCCCCC" />
                 <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                 <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                 <SelectedRowStyle BackColor="#669900" Font-Bold="True" ForeColor="White" />
                 <SortedAscendingCellStyle BackColor="#F1F1F1" />
                 <SortedAscendingHeaderStyle BackColor="#808080" />
                 <SortedDescendingCellStyle BackColor="#CAC9C9" />
                 <SortedDescendingHeaderStyle BackColor="#383838" />
             </asp:GridView>
        </div>

        <asp:Button ID="bt_excel" class="btn btn-success" runat="server" Text="Descargar Excel" onclick="bt_excel_Click" />

      </div>
    </div>

</asp:Content>