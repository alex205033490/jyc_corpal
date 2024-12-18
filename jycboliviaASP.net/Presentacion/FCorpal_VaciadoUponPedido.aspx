<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_VaciadoUponPedido.aspx.cs" async="true" Inherits="jycboliviaASP.net.Presentacion.FCorpal_VaciadoUponPedido" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
     .Centrar
    {        
        margin: 0 auto;            
        width:950px;
        }

    .titulo
    {        
        height:40px;    
        margin-top: 20px;
        }
    
     .sc1
     { 
       
       float: left;   
       height: 120px;  
       width: 940px;
     
         }   
     
      .sc2
     { 
         
       float: left;   
       height: 300px;  
       width: 840px;
       overflow:auto;
         } 
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card">
    <div class="card-header bg-success text-white">Vaciar al Upon los Pedidos de JyC</div>
    <div class="card-body">
        <div class="list-group list-group-flush">
            <div class="list-group-item">
                <div class="row mb-2">                    
                        <div class="col-9 col-sm-8 col-md-8">
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
                        <div class="col-3 col-sm-3 col-md-3">
                            <br />
                            <asp:Button ID="bt_buscar" CssClass="btn btn-info" runat="server" Text="Buscar" OnClick="bt_buscar_Click" />
                        </div>
                    </div>
                <div class="row">
                    <div class="col-4 col-sm-4 col-md-3">
                    <asp:Button ID="bt_anularPago" CssClass="btn btn-danger" runat="server" onclick="bt_anularPago_Click"  Text="Anular" />
                        </div>
                    <div class="col-4 col-sm-4 col-md-3">
                    <asp:Button ID="bt_vaciarAlUpon" CssClass="btn btn-success" runat="server" onclick="bt_vaciarAlSimec_Click" Text="Vaciar al Upon" /> 
                    </div>
                    <div class="col-4 col-sm-4 col-md-3">
                    <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" onclick="Button1_Click" Text="Excel" /> 
                    </div>                    
                </div>
   
            </div>
          </div>
     </div>            
            <div class="card-body list-group-item col-11 col-sm-11 col-md-8">
                <div class="row col-md-12">
                        <div class="sc2 col-md-1">
                            <asp:GridView ID="gv_datosCobros" runat="server" BackColor="White" 
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="2px" CellPadding="6"
                                Font-Size="Small" ForeColor="black" GridLines="Vertical" AutoGenerateColumns="false">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="green" Font-Bold="true" ForeColor="white" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="right" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                <Columns>
                                <asp:TemplateField HeaderText="Seleccionar">
                                    <ItemTemplate>
                                           <asp:CheckBox ID="chkAll" runat="server"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:BoundField DataField="codigo" HeaderText ="Codigo Solicitud" SortExpression="codigo"/>
                                    <asp:BoundField DataField="nroboleta" HeaderText ="Nro Boleta" SortExpression="nroboleta"/>
                                    <asp:BoundField DataField="fecha_entrega" HeaderText ="Fecha Entrega" SortExpression="fecha_entrega"/>
                                    <asp:BoundField DataField="horaentrega" HeaderText ="Hora Entrega" SortExpression="horaentrega"/>
                                    <asp:BoundField DataField="CodClienteUpon" HeaderText ="Codigo Cliente" SortExpression="CodClienteUpon"/> 
                                    <asp:BoundField DataField="Cliente" HeaderText ="Cliente" SortExpression="Cliente"/>
                                    <asp:BoundField DataField="ImporteProductos" HeaderText ="Importe Total" SortExpression="ImporteProductos"/>
                                    
                                </Columns>
                            </asp:GridView>
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
