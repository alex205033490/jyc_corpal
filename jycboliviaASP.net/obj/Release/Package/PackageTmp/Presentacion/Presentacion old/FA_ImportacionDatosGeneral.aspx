<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ImportacionDatosGeneral.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ImportacionDatosGeneral" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Styles/Style_ImportacionJYCIAGeneral.css" rel="stylesheet" type="text/css" />

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
                        
        .style7
    {
        width: 86px;
    }
    .style9
    {
        width: 84px;
    }
                        
        .style17
        {
            width: 68px;
        }
                        
        .style18
        {
            width: 48px;
        }
                                
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class = "menu">  
       <inmoinfo:menu ID="Menu1" runat="server"/>
   </div>
       <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

<div class="Centrar">
<table>
<tr>
<td>
<!--
<div class="titulo">
    <h3>
        <asp:Label ID="lb_titulo" runat="server" Text="Datos General Importacion"></asp:Label></h3>
</div>
-->
</td>
</tr>


<tr>
<td>
<div class="GCP1">
<div >
<table style="height: auto; width: auto; margin-left:5px; margin-top: 0px; margin-right: 0px;">
<tr>
<td>
    <asp:Label ID="Label28" runat="server" Text="DUI:" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_dui" runat="server" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Label ID="Label29" runat="server" Font-Size="Small" Text="Contenedor:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_contenedor" runat="server" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Label ID="Label25" runat="server" Font-Size="Small" Text="Edificio:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_edificio" runat="server" Width="250px" Font-Size="Small"></asp:TextBox>
    </td>
<td></td>
<td class="style18">
    <asp:Label ID="Label26" runat="server" Font-Size="Small" Text="Exbo:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_exbo" runat="server" Width="120px" Font-Size="Small"></asp:TextBox>
    </td>
<td>
    &nbsp;</td>
<td> 
        <asp:Label ID="Label27" runat="server" Text="Estado:" Font-Size="Small"></asp:Label>
    </td>
        <td>
            <asp:DropDownList ID="dd_estado" runat="server" Width="250px">
            </asp:DropDownList>
    </td>
        <td></td>
<td> 
        <asp:Button ID="bt_buscar"  runat="server"  Text="Buscar" Height="25px" 
            onclick="bt_buscar_Click" />
    </td>
<td>
    &nbsp;</td>
<td> 
        <asp:Button ID="bt_Limpiar" runat="server" Height="25px" Text="Limpiar" 
            onclick="bt_Limpiar_Click" />
    </td>
</tr>

</table>
 </div>
</div>

</td>
</tr>


<tr>
<td>
<div class = "GCP3">
   <asp:GridView ID="gv_datos" runat="server" Height="97px" 
        style="margin-top: 0px" Width="322px" Font-Size="X-Small" 
        BackColor="#CCCCCC" BorderColor="#999999" 
        BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" 
        ForeColor="Black" AutoGenerateColumns="False" 
        onrowdatabound="gv_datos_RowDataBound" >

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
            <asp:BoundField DataField="Vendido" HeaderText="Vendido" 
                SortExpression="Vendido" />
            <asp:BoundField DataField="Instalado" HeaderText="Instalado" 
                SortExpression="Instalado" />
            <asp:BoundField DataField="UNE" HeaderText="UNE" 
                SortExpression="UNE" />
            <asp:BoundField DataField="CIUDAD" HeaderText="CIUDAD" 
                SortExpression="CIUDAD" />
            <asp:TemplateField HeaderText="Estado" SortExpression="Estado1">
                 <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("Estado1") %>'></asp:Label>
                    <asp:TextBox ID="tx_estado1" runat="server" Text='<%# Bind("Estado1") %>'></asp:TextBox>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="200px">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Aprobacion limite planos" 
                SortExpression="fechaaprobacionlimite_planos">
                <ItemTemplate>
                    <asp:Label ID="Label19" runat="server" 
                        Text='<%# Bind("fechaaprobacionlimite_planos") %>'></asp:Label>
                    <asp:TextBox ID="tx_fechaaprobacionlimiteplanos1" runat="server" 
                        Text='<%# Bind("fechaaprobacionlimite_planos") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="tx_fechaaprobacionlimiteplanos1_CalendarExtender" 
                        runat="server" TargetControlID="tx_fechaaprobacionlimiteplanos1">
                    </asp:CalendarExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Aproximado Arribo Puerto" 
                SortExpression="fechaaproximadoarribopuerto">
                <ItemTemplate>
                    <asp:Label ID="Label20" runat="server" 
                        Text='<%# Bind("fechaaproximadoarribopuerto") %>'></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" 
                        Text='<%# Bind("fechaaproximadoarribopuerto") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" 
                        TargetControlID="TextBox2">
                    </asp:CalendarExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FechaAproxEmbarque" 
                HeaderText="Aprox Embarque" 
                SortExpression="FechaAproxEmbarque" />
            <asp:BoundField DataField="FechaPagoEmbarque" HeaderText="Pago Embarque" 
                SortExpression="FechaPagoEmbarque" />
            <asp:BoundField DataField="ValorFOB" HeaderText="Valor FOB" 
                SortExpression="ValorFOB" />
            <asp:BoundField DataField="ValorDelGiroAlProovedor" HeaderText="Valor Del Giro Al Proovedor" 
                SortExpression="ValorDelGiroAlProovedor" />
            <asp:BoundField DataField="ValorTransMaritimo" HeaderText="Valor Trans. Maritimo" 
                SortExpression="ValorTransMaritimo" />
            <asp:TemplateField HeaderText="Valor Trans. Maritimo Pagado" 
                SortExpression="ValorTransMaritimoPagado">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# Bind("ValorTransMaritimoPagado") %>'></asp:Label>
                    <asp:TextBox ID="tx_valorTransMaritimoPagado" runat="server" 
                        Text='<%# Bind("ValorTransMaritimoPagado") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Valor Girado a Proovedor Dolares" 
                SortExpression="valorgiradoaproovedor_dolares">
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" 
                        Text='<%# Bind("valorgiradoaproovedor_dolares") %>'></asp:Label>
                    <asp:TextBox ID="tx_valorgiradoaproovedores_dolares1" runat="server" 
                        Text='<%# Bind("valorgiradoaproovedor_dolares") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Valor Girado a Proovedor Euros" 
                SortExpression="valorgiradoaproovedor_euros">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" 
                        Text='<%# Bind("valorgiradoaproovedor_euros") %>'></asp:Label>
                    <asp:TextBox ID="tx_valorgiradoaproovedores_euros1" runat="server" 
                        Text='<%# Bind("valorgiradoaproovedor_euros") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NroDui" SortExpression="NroDui">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("NroDui") %>'></asp:Label>
                    <asp:TextBox ID="tx_nroDui" runat="server" Text='<%# Bind("NroDui") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contenedor" SortExpression="Contenedor">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Contenedor") %>'></asp:Label>
                    <asp:TextBox ID="tx_contenedor" runat="server" Text='<%# Bind("Contenedor") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Consignatario" SortExpression="Consignatario">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Consignatario") %>'></asp:Label>
                    <asp:TextBox ID="tx_consignatario1" runat="server" 
                        Text='<%# Bind("Consignatario") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha Factura Proveedor" 
                SortExpression="fechafacturaproveedor">
                <ItemTemplate>
                    <asp:Label ID="Label22" runat="server" 
                        Text='<%# Bind("fechafacturaproveedor") %>'></asp:Label>
                    <asp:TextBox ID="tx_fechafacturaProveedor" runat="server" 
                        Text='<%# Bind("fechafacturaproveedor") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="tx_fechafacturaProveedor_CalendarExtender" 
                        runat="server" TargetControlID="tx_fechafacturaProveedor">
                    </asp:CalendarExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nro Factura Proveedor" 
                SortExpression="nrofacturaproveedor">
                <ItemTemplate>
                    <asp:Label ID="Label23" runat="server" 
                        Text='<%# Bind("nrofacturaproveedor") %>'></asp:Label>
                    <asp:TextBox ID="tx_nrofacturaProveedor" runat="server" 
                        Text='<%# Bind("nrofacturaproveedor") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Monto Factura Proveedor" 
                SortExpression="montofacturaproveedor">
                <ItemTemplate>
                    <asp:Label ID="Label24" runat="server" 
                        Text='<%# Bind("montofacturaproveedor") %>'></asp:Label>
                    <asp:TextBox ID="tx_montofacturaProveedor" runat="server" 
                        Text='<%# Bind("montofacturaproveedor") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha Factura Seguro" 
                SortExpression="FechaFactura">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("FechaFactura") %>'></asp:Label>
                    <asp:TextBox ID="tx_fechaFacturaSeguro1" runat="server" 
                        Text='<%# Bind("FechaFactura") %>'></asp:TextBox>
                    
                    <asp:CalendarExtender ID="tx_fechaFacturaSeguro1_CalendarExtender" runat="server" 
                        TargetControlID="tx_fechaFacturaSeguro1">
                    </asp:CalendarExtender>
                    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NIT Seguro" SortExpression="NIT">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("NIT") %>'></asp:Label>
                    <asp:TextBox ID="tx_nitSeguro" runat="server" Text='<%# Bind("NIT") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nro Factura Seguro" SortExpression="NroFactura">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("NroFactura") %>'></asp:Label>
                    <asp:TextBox ID="tx_nroFacturaSeguro" runat="server" 
                        Text='<%# Bind("NroFactura") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Monto Seguro" SortExpression="MontoSeguro">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("MontoSeguro") %>'></asp:Label>
                    <asp:TextBox ID="tx_montoseguro1" runat="server" 
                        Text='<%# Bind("MontoSeguro") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nro Aplicacion del Seguro" 
                SortExpression="nroaplicaciondelseguro">
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" 
                        Text='<%# Bind("nroaplicaciondelseguro") %>'></asp:Label>
                    <asp:TextBox ID="tx_nroaplicacionseguro1" runat="server" 
                        Text='<%# Bind("nroaplicaciondelseguro") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Valor Costo Prima" 
                SortExpression="valorcostoprima">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("valorcostoprima") %>'></asp:Label>
                    <asp:TextBox ID="tx_valorCostoPrima1" runat="server" 
                        Text='<%# Bind("valorcostoprima") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Arribo a Puerto" 
                SortExpression="fechaarriboapuerto">
                <ItemTemplate>
                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("fechaarriboapuerto") %>'></asp:Label>
                    <asp:TextBox ID="tx_arriboapuerto1" runat="server" 
                        Text='<%# Bind("fechaarriboapuerto") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="tx_arriboapuerto1_CalendarExtender" runat="server" 
                        TargetControlID="tx_arriboapuerto1">
                    </asp:CalendarExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Arribo Aduanero" 
                SortExpression="fechaarriboaduanero">
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server" 
                        Text='<%# Bind("fechaarriboaduanero") %>'></asp:Label>
                    <asp:TextBox ID="tx_arriboaduanero1" runat="server" 
                        Text='<%# Bind("fechaarriboaduanero") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="tx_arriboaduanero1_CalendarExtender" runat="server" 
                        TargetControlID="tx_arriboaduanero1">
                    </asp:CalendarExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Arribo Obra" SortExpression="fechaarriboobra">
                <ItemTemplate>
                    <asp:Label ID="Label16" runat="server" Text='<%# Bind("fechaarriboobra") %>'></asp:Label>
                    <asp:TextBox ID="tx_arriboObra1" runat="server" 
                        Text='<%# Bind("fechaarriboobra") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="tx_arriboObra1_CalendarExtender" runat="server" 
                        TargetControlID="tx_arriboObra1">
                    </asp:CalendarExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cruce Frontera" 
                SortExpression="fechacrucefrontera">
                <ItemTemplate>
                    <asp:Label ID="Label17" runat="server" Text='<%# Bind("fechacrucefrontera") %>'></asp:Label>
                    <asp:TextBox ID="tx_cruceFrontera1" runat="server" 
                        Text='<%# Bind("fechacrucefrontera") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="tx_cruceFrontera1_CalendarExtender" runat="server" 
                        TargetControlID="tx_cruceFrontera1">
                    </asp:CalendarExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Color Canal" SortExpression="colorcanal">
                <ItemTemplate>
                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("colorcanal") %>'></asp:Label>
                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("colorcanal") %>'></asp:TextBox>
                    <asp:DropDownList ID="dd_colorCanal1" runat="server" Width="120px">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Rojo</asp:ListItem>
                        <asp:ListItem>Amarillo</asp:ListItem>
                        <asp:ListItem>Verde</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nro B/L" SortExpression="nro_bl">
                <ItemTemplate>
                    <asp:Label ID="Label21" runat="server" Text='<%# Bind("nro_bl") %>'></asp:Label>
                    <asp:TextBox ID="tx_nrobl1" runat="server" Text='<%# Bind("nro_bl") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#99CC00" Font-Bold="True" ForeColor="White" />
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
<div class = "GCP_otro">
<table>
<tr>
<td>
    <asp:Button ID="bt_Update" runat="server" Text="Actualizar" 
        onclick="bt_Update_Click" />
    </td>
<td></td>
<td></td>
<td></td>
</tr>


<tr>
<td>
<asp:Button ID="bt_Exportar" runat="server" 
        Text="Exportar Excel" onclick="bt_Exportar_Click" />
</td>
<td></td>
<td>
    <asp:Label ID="Label15" runat="server" Text="Cantidad Equipos :"></asp:Label>
    </td>
<td></td>
<td>
    <asp:Label ID="lb_cantidad" runat="server" Text="0"></asp:Label>
    </td>
</tr>
</table>
    
    </div>

</td>
</tr>


</table>







</div>



</asp:Content>
