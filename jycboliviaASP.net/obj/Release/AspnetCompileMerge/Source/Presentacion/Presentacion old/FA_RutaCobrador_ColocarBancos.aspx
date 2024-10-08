<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_RutaCobrador_ColocarBancos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_RutaCobrador_ColocarBancos" %>
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
    <h3><asp:Label ID="lb_titulo" runat="server" 
            Text="Colocar Bancos a Pagos Movil Realizados"></asp:Label> </h3> 
 </div>
 
 <div class="contenedor">
     <table>        
     
        <!--- primera parte ----------------------------->
        
        <tr>
                 <td>
                    <table>
                    <tr>
                    <td>  
                        <asp:Label ID="Label5" runat="server" Text="Pagos a Realizados:" 
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
                            <asp:Button ID="bt_colocarBancos" runat="server" Text="Colocar Bancos" 
                                onclick="bt_colocarBancos_Click" />
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Exbo:" Font-Size="Small"></asp:Label>
                                </td>
                                
                                <td>
                                    <asp:TextBox ID="tx_exbo1" runat="server" Font-Size="Small"></asp:TextBox>
                                     <asp:AutoCompleteExtender ID="tx_exbo1_AutoCompleteExtender" runat="server" 
                                    CompletionSetCount="12" MinimumPrefixLength="1" ServiceMethod="getListaEquipo" 
                                    TargetControlID="tx_exbo1"  UseContextKey="True"
                                    CompletionListCssClass="CompletionList" 
                                    CompletionListItemCssClass="CompletionlistItem" 
                                    CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10"
                                    ></asp:AutoCompleteExtender>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="Edificio:" Font-Size="Small"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tx_edificio1" runat="server" Font-Size="Small" Width="250px"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="tx_edificio1_AutoCompleteExtender" runat="server" 
                                         CompletionSetCount="12" MinimumPrefixLength="1" ServiceMethod="getListaProyecto" 
                                        TargetControlID="tx_edificio1"
                                        UseContextKey="True"
                                        CompletionListCssClass="CompletionList" 
                                        CompletionListItemCssClass="CompletionlistItem" 
                                        CompletionListHighlightedItemCssClass="CompletionListMighlightedItem" CompletionInterval="10">
                                        </asp:AutoCompleteExtender>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Button ID="tb_buscar" runat="server" Text="Buscar" 
                                        onclick="tb_buscar_Click" />
                                </td>
                                <td></td>
                                <td>
                                    <asp:Button ID="bt_limpiar" runat="server" Text="Limpiar" 
                                        onclick="bt_limpiar_Click" /></td>
                            </tr>
                            </table>
                        </div>
                    </td>
                    </tr>

                        <tr>
                            <td>
                                <div class="PS3">
                                    <asp:GridView ID="gv_recibosPagos" runat="server" BackColor="#CCCCCC" 
                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
                                        CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
                                        AutoGenerateColumns="False">
                                        <Columns>
                            
                                            <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server"  />
                                                    </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="codigo" HeaderText="codigo" 
                                                SortExpression="codigo" />
                                            <asp:BoundField DataField="exbo" HeaderText="exbo" SortExpression="exbo" />
                                            <asp:BoundField DataField="Edificio" HeaderText="Edificio" 
                                                SortExpression="Edificio" />
                                            <asp:BoundField DataField="mes" HeaderText="mes" SortExpression="mes" />
                                            <asp:BoundField DataField="years" HeaderText="years" SortExpression="years" />
                                            <asp:BoundField DataField="Fecha1" HeaderText="Fecha1" 
                                                SortExpression="Fecha1" />
                                            <asp:BoundField DataField="hora" HeaderText="hora" SortExpression="hora" />
                                            <asp:BoundField DataField="Efectivo1" HeaderText="Efectivo1" 
                                                SortExpression="Efectivo1" />
                                            <asp:BoundField DataField="Deposito1" HeaderText="Deposito1" 
                                                SortExpression="Deposito1" />
                                            <asp:BoundField DataField="Transferencia1" HeaderText="Transferencia1" 
                                                SortExpression="Transferencia1" />
                                            <asp:BoundField DataField="nrocheque" HeaderText="nrocheque" 
                                                SortExpression="nrocheque" />
                                            <asp:BoundField DataField="factura" HeaderText="factura" 
                                                SortExpression="factura" />
                                            <asp:BoundField DataField="recibo" HeaderText="recibo" 
                                                SortExpression="recibo" />
                                            <asp:BoundField DataField="banco" HeaderText="banco" SortExpression="banco" />
                                            <asp:BoundField DataField="tipocambio" HeaderText="tipocambio" 
                                                SortExpression="tipocambio" />
                                            <asp:BoundField DataField="pago" HeaderText="pago" SortExpression="pago" />
                                            <asp:BoundField DataField="pagobs" HeaderText="pagobs" 
                                                SortExpression="pagobs" />
                                            <asp:BoundField DataField="Moneda" HeaderText="Moneda" 
                                                SortExpression="Moneda" />
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
                            &nbsp;</td>
                        </tr>
                        </table>
                 </td>
                 </tr>

     </table>

 </div>



<div class="PS5"></div>

 </div>


</asp:Content>
