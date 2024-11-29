<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_VaciadoUponPedido.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_VaciadoUponPedido" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="../Styles/Style_SeguimientosMorosos.css" rel="stylesheet" type="text/css" />
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
     
     
     
     .style1
     {
         width: 20px;
     }
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card">
    <div class="card-header bg-success text-white">Vaciar al Upon los Pedidos de JyC</div>
    <div class="card-body">
        <div class="list-group list-group-flush">
            <div class="list-group-item">
                <div class="row">                    
                        <div class="col-md-9">
                            <asp:Label ID="Label3" for="tx_cliente" runat="server" Text="Cliente:" Font-Size="Small"></asp:Label>            
                            <asp:TextBox ID="tx_cliente"  CssClass="form-control" runat="server" Font-Size="Small"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="tx_cliente_AutoCompleteExtender" runat="server" 
                                TargetControlID="tx_cliente"
                                CompletionSetCount="12" 
                                MinimumPrefixLength="1" ServiceMethod="GetlistaClienteP" 
                                UseContextKey="True"
                                CompletionListCssClass="CompletionList" 
                                CompletionListItemCssClass="CompletionlistItem" 
                                CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                                >
                            </asp:AutoCompleteExtender>
                        </div>
                        <div class="col-md-3">
                            <br />
                            <asp:Button ID="bt_buscar" CssClass="btn btn-info" runat="server" Text="Buscar" OnClick="bt_buscar_Click" />
                        </div>
                    </div>
                <div class="row">
                    <div class="col-md-3">
                    <asp:Button ID="bt_anularPago" CssClass="btn btn-danger" runat="server" onclick="bt_anularPago_Click"  Text="Anular" />
                        </div>
                    <div class="col-md-3">
                    <asp:Button ID="bt_vaciarAlUpon" CssClass="btn btn-success" runat="server" onclick="bt_vaciarAlSimec_Click" Text="Vaciar al Upon" /> 
                    </div>
                    <div class="col-md-3">
                    <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" onclick="Button1_Click" Text="Excel" /> 
                    </div>                    
                </div>
   
            </div>
          </div>
     </div>            
            <div class="list-group-item">
                <div class="row">
                    <div class="col-md-12">
                        <div class="sc2">
                            <asp:GridView ID="gv_datosCobros" runat="server" BackColor="White"                                 
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                                Font-Size="Small" ForeColor="Black" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                <Columns>
                                <asp:TemplateField HeaderText="Anular">
                                    <ItemTemplate>
                                           <asp:CheckBox ID="chkAll" runat="server"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </div>
                    </div>
                </div>
            </div>
            <div class="list-group-item">
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="Label6" runat="server" Text="Cantidad Equipos :"></asp:Label>
                        <asp:Label ID="lb_cantDatos" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
   


</asp:Content>
