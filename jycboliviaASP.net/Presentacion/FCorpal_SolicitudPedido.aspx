<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_SolicitudPedido.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FCorpal_SolicitudPedido" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/Style_adicionarRepuesto.css" rel="stylesheet" type="text/css" />
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
            height: 26px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
           

    <div class="row">        
          <div class="col-12">
          <div class="card">
             <div class="card-header bg-success text-white">
                Solicitud Producto
              </div>

               <ul class="list-group list-group-flush">
                <li class="list-group-item">
                    <div class="row">
                          <div class="col-12">
                            <label for="inputName5" class="form-label">Producto</label>                  
                              <asp:TextBox ID="tx_producto" runat="server" class="form-control" ></asp:TextBox>
                              <asp:AutoCompleteExtender ID="tx_producto_AutoCompleteExtender" runat="server" 
                                  TargetControlID="tx_producto"
                                  CompletionSetCount="12" 
                                  MinimumPrefixLength="1" ServiceMethod="GetlistaProductos" 
                                  UseContextKey="True"
                                  CompletionListCssClass="CompletionList" 
                                  CompletionListItemCssClass="CompletionlistItem" 
                                  CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10" >
                              </asp:AutoCompleteExtender>  
                          </div>
                 
                          <div class="col-6">
                            <label  class="form-label">Cantidad</label>                  
                            <asp:TextBox ID="tx_cantidadProducto" runat="server" class="form-control" type="text" Font-Size="Small"></asp:TextBox>
                          </div>

                          <div class="col-6">
                            <label  class="form-label">Tipo Solicitud</label>                  
                              <asp:DropDownList ID="dd_tipoSolicitud" class="btn btn-secondary dropdown-toggle" runat="server"  >
                               <asp:ListItem>VENTA</asp:ListItem>
                               <asp:ListItem>DEGUSTACION</asp:ListItem>
                               <asp:ListItem>MUESTRA</asp:ListItem>
                               <asp:ListItem>OTROS</asp:ListItem>
                           </asp:DropDownList>
                          </div>      
                         <div class="col-6">
                           <asp:CheckBox ID="cb_itemPackFerial" runat="server" />
                             <label  class="form-label">Item Pack Ferial</label> 
                         </div>

                          <div class="text-center">
                              <asp:Button ID="bt_limpiar" runat="server" class="btn btn-secondary" onclick="bt_limpiar_Click"  Text="Limpiar" />
                              <asp:Button ID="bt_adicionar" runat="server" class="btn btn-success"  onclick="bt_adicionar_Click"  Text="Adicionar" />
                              <asp:Button ID="bt_buscar" runat="server" class="btn btn-primary" onclick="bt_buscar_Click"  Text="Buscar" />
                              <asp:Button ID="bt_prueba" runat="server" class="btn btn-primary" onclick="bt_prueba_Click"  Text="Prueba" />
      
                          </div>
                        </div>
                </li>
                <li class="list-group-item">
                     <div class="row">
        <div class="col-12">              
              <div class="Grepuesto">
                  <asp:GridView ID="gv_Productos" runat="server" BackColor="White"
                      
                      BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                      Font-Size="X-Small" ForeColor="Black" GridLines="Vertical">
                      <Columns>
                              <asp:TemplateField HeaderText="Asignar">
                                  <ItemTemplate>
                                      <asp:CheckBox ID="CheckBox1" runat="server" />
                                  </ItemTemplate>
                              </asp:TemplateField>
                          </Columns>
                      <AlternatingRowStyle BackColor="#CCCCCC" />
                      <FooterStyle BackColor="#CCCCCC" />
                      <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                      <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                      <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                      <SortedAscendingCellStyle BackColor="#F1F1F1" />
                      <SortedAscendingHeaderStyle BackColor="#808080" />
                      <SortedDescendingCellStyle BackColor="#CAC9C9" />
                      <SortedDescendingHeaderStyle BackColor="#383838" />
                  </asp:GridView>
              </div>
        </div>
    </div>     
                </li>                
              </ul>            
          </div>
        </div>        
    </div>        
    <div class="row">
     <div class="col-12">
        <div class="card" >
          <div class="card-header bg-success text-white">
            Adicion de Producto
          </div>
          <ul class="list-group list-group-flush">
            <li class="list-group-item">
                <div class="row">
                    <div class="col-md-3">
                         <label class="form-label" for="tx_nrodocumento">Nro</label>
                        <asp:TextBox ID="tx_nrodocumento" runat="server" class="form-control"></asp:TextBox>
                    </div>
            <div class="col">
                 <label class="form-label" for="tx_solicitante">Solicitante</label>
                <asp:TextBox ID="tx_solicitante" runat="server" class="form-control" ></asp:TextBox>
            </div>
        </div>
                <div class="row">
                <div class="col-6">
                     <label class="form-label" for="tx_fechaEntrega">Fecha Entrega</label>
                    <asp:TextBox ID="tx_fechaEntrega" runat="server" class="form-control"></asp:TextBox>
                    <asp:CalendarExtender ID="tx_fechaEntrega_CalendarExtender" runat="server" 
                        TargetControlID="tx_fechaEntrega">
                    </asp:CalendarExtender>
                </div>
                <div class="col-6">
                    <label class="form-label" for="tx_horaEntrega">Hora Entrega</label>
                    <asp:TextBox ID="tx_horaEntrega" runat="server" class="form-control"></asp:TextBox>
                </div>
                </div>
                <div class="row">
                    <div class="col">
                        <label class="form-label" for="tx_cliente">Cliente:</label>
                        <asp:TextBox ID="tx_cliente" runat="server" class="form-control"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="tx_cliente_AutoCompleteExtender" runat="server" 
                            TargetControlID="tx_cliente"
                            CompletionSetCount="12" 
                            MinimumPrefixLength="1" ServiceMethod="GetlistaClientes222" 
                            UseContextKey="True"
                            CompletionListCssClass="CompletionList" 
                            CompletionListItemCssClass="CompletionlistItem" 
                            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10" >
                        </asp:AutoCompleteExtender>  
                    </div>
                </div>

            <div class="row">
                <div class="col-3">
                    <asp:Button ID="bt_guardar" runat="server" class="btn btn-success" Text="Guardar"   onclick="bt_guardar_Click" />
                </div>
                
            </div>
            </li>
    
    
            <li class="list-group-item">
                <div class="row">
                    <div class="Gcotizacion">
                        <asp:GridView ID="gv_adicionados" runat="server" BackColor="White" 
            
                            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                            Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" 
                            onrowcancelingedit="gv_adicionados_RowCancelingEdit" 
                            onrowdeleting="gv_adicionados_RowDeleting" 
                            onrowediting="gv_adicionados_RowEditing" 
                            onrowupdating="gv_adicionados_RowUpdating">
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:CommandField ShowEditButton="True" />
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
      
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </asp:GridView>
                    </div>
                </div>
            </li>   
              
          </ul>
        </div>
      </div>
    </div>

       

</asp:Content>
