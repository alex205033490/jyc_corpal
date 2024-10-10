<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_PagoSeguimiento2.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_PagoSeguimiento2" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Styles_GPagoSeguimiento2.css" rel="stylesheet" type="text/css" />

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
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>
   
<div class="Centrar">
 <div class="titulo">
    <h3><asp:Label ID="lb_titulo" runat="server" Text="Gestion de Pago del Seguimiento"></asp:Label> </h3> 
 </div>
 
 <div class="contenedor">
     <table>
        <tr>
            <td>
                <div class="PS1_busqueda">
                    <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Exbo:" Font-Size="X-Small"></asp:Label>
                        </td>
                        <td>    
                            <asp:TextBox ID="tx_Exbo" runat="server" Font-Size="X-Small" Width="80px"></asp:TextBox>
                             <asp:AutoCompleteExtender ID="tx_Exbo_AutoCompleteExtender" runat="server" 
                                    CompletionSetCount="12" MinimumPrefixLength="1" ServiceMethod="getListaEquipo" 
                                    TargetControlID="tx_Exbo"  UseContextKey="True"
                                    CompletionListCssClass="CompletionList" 
                                    CompletionListItemCssClass="CompletionlistItem" 
                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                                    ></asp:AutoCompleteExtender>

                        </td>
                        <td>
                        <asp:Label ID="Label2" runat="server" Text="Proyecto:" Font-Size="X-Small"></asp:Label>
                        </td>
                        <td>        
                            <asp:TextBox ID="tx_nombreProyecto" runat="server" Font-Size="X-Small" 
                                Width="250px"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="tx_nombreProyecto_AutoCompleteExtender" runat="server" 
                                         CompletionSetCount="12" MinimumPrefixLength="1" ServiceMethod="getListaProyecto" 
                                        TargetControlID="tx_nombreProyecto"
                                        UseContextKey="True"
                                        CompletionListCssClass="CompletionList" 
                                        CompletionListItemCssClass="CompletionlistItem" 
                                        CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                                        </asp:AutoCompleteExtender>

                        </td>
                        <td></td>
                        <td>
                            <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
                                onclick="bt_buscar_Click" Font-Size="Small" />
                        </td>
                    </tr>
                    </table>
               </div>
            </td>
        </tr>

        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="Equipos:" Font-Size="Small"></asp:Label></td>
        </tr>
 
        <tr>
            <td>
                <div class="PS1">
                    <asp:GridView ID="gv_equipos" runat="server" BackColor="#CCCCCC" 
                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
                        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
                        onselectedindexchanged="gv_equipos_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#669900" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />
                    </asp:GridView>
                </div>
            </td>
        </tr>

        <!--- primera parte ----------------------------->
        
        <tr>
                 <td>
                    <table>
                    <tr>
                    <td>  <asp:Label ID="Label5" runat="server" Text="Pagos a Realizar:" 
                            Font-Size="Small"></asp:Label>  </td>
                    </tr>
                    <tr>
                    <td>
                        <div class ="monedaTipoCambio">
                            <table>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Banco:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="dd_cuentaBanco" runat="server" Font-Size="Small" 
                                        Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td></td>                               
                                <td>
                                    <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Tipo Cambio:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tx_tipoCambio" runat="server" Font-Size="Small" Width="50px"></asp:TextBox>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Font-Size="Small" Text="Moneda:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="dd_moneda" runat="server" Width="100px">
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="dd_tipoPago" runat="server" Width="120px">
                                        <asp:ListItem Value="EF">Efectivo</asp:ListItem>
                                        <asp:ListItem Value="CH">Cheque</asp:ListItem>
                                        <asp:ListItem Value="TF">Transferencia</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Font-Size="Small" Text="Fecha:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tx_FechaGeneral" runat="server" Font-Size="Small"></asp:TextBox>
                                      <asp:CalendarExtender ID="calendarFechaGeneral" runat="server" 
                                    TargetControlID="tx_FechaGeneral">  </asp:CalendarExtender>   
                                </td>
                            </tr>
                            </table>
                        </div>
                    </td>
                    </tr>

                        <tr>
                            <td>
                                <div class="PS3">
                                    <asp:GridView ID="gv_seguiMes" runat="server" BackColor="#CCCCCC" 
                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
                                        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
                                        AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:CommandField EditText="Recibos" SelectText="Recibos" 
                                                ShowSelectButton="True" />
                            
                                            <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server"  />
                                                    </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="codseg" HeaderText="codseg" 
                                                SortExpression="codseg" />
                                            <asp:BoundField DataField="codmes" HeaderText="codmes" 
                                                SortExpression="codmes" />
                                            <asp:BoundField DataField="years" HeaderText="years" SortExpression="years" />
                                            <asp:BoundField DataField="nombre" HeaderText="nombre" 
                                                SortExpression="nombre" />
                                            <asp:BoundField DataField="Deuda" HeaderText="Deuda" 
                                                SortExpression="Deuda" />
                                            <asp:BoundField DataField="Pagado" HeaderText="Pagado" 
                                                SortExpression="Pagado" />
                                            <asp:TemplateField HeaderText="Pagar" SortExpression="Pagar">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Pagar") %>'></asp:Label>
                                                    <asp:TextBox ID="TextBox2" runat="server" Font-Size="X-Small" Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Pagar") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque" SortExpression="Cheque">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Cheque") %>'></asp:Label>
                                                    <asp:TextBox ID="TextBox7" runat="server" Font-Size="X-Small" Width="100px"></asp:TextBox>
                                                                                                        
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Cheque") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Factura" SortExpression="Factura">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Factura") %>'></asp:Label>
                                                    <asp:TextBox ID="TextBox8" runat="server" Font-Size="X-Small" Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Factura") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Recibo" SortExpression="Recibo">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Recibo") %>'></asp:Label>
                                                    <asp:TextBox ID="TextBox9" runat="server" Font-Size="X-Small" Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Recibo") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                        <RowStyle BackColor="White" />
                                        <SelectedRowStyle BackColor="#669900" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>

                        <tr>
                        <td>
                            <asp:Button ID="bt_pagar" runat="server" Text="Pagar" 
                                onclick="bt_pagar_Click" />
                            </td>
                        </tr>
                        </table>
                 </td>
                 </tr>

     </table>

 </div>



<div class="PS5"></div>

 </div>



</asp:Content>
