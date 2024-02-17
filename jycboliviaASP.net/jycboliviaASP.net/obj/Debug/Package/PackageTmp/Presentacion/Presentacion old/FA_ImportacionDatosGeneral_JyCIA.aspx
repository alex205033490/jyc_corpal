<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ImportacionDatosGeneral_JyCIA.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ImportacionDatosGeneral_JyCIA" %>
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
    &nbsp;</td>
<td>
    &nbsp;</td>
<td></td>
<td>
    <asp:Label ID="Label30" runat="server" Font-Size="Small" 
        Text="Semana Expedicion:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_sem_Exp" runat="server"></asp:TextBox>
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
        ForeColor="Black" AutoGenerateColumns="False" >

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
            <asp:BoundField DataField="Estado1" HeaderText="Estado" 
                SortExpression="Estado1" />
            <asp:BoundField DataField="fechaaproximadoarribopuerto" 
                HeaderText="Aproximado Arribo Puerto" 
                SortExpression="fechaaproximadoarribopuerto" />
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
            <asp:BoundField DataField="ValorTransMaritimoPagado" 
                HeaderText="Valor Trans. Maritimo Pagado" 
                SortExpression="ValorTransMaritimoPagado" />
            <asp:BoundField DataField="Consignatario" HeaderText="Consignatario" 
                SortExpression="Consignatario" />
            <asp:BoundField DataField="fechafacturaproveedor" 
                HeaderText="Fecha Factura Proveedor" SortExpression="fechafacturaproveedor" />
            <asp:BoundField DataField="nrofacturaproveedor" 
                HeaderText="Nro Factura Proveedor" SortExpression="nrofacturaproveedor" />
            <asp:BoundField DataField="montofacturaproveedor" 
                HeaderText="Monto Factura Proveedor" SortExpression="montofacturaproveedor" />
            <asp:TemplateField HeaderText="Sem_Exp" SortExpression="semana_expedicion">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("semana_expedicion") %>'></asp:Label>
                    <asp:TextBox ID="tx_semanaExpadicion1" runat="server" 
                        Text='<%# Bind("semana_expedicion") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 1 Nro Proforma" 
                SortExpression="giros1_nroproforma">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("giros1_nroproforma") %>'></asp:Label>
                    <asp:TextBox ID="tx_giro_nroProforma" runat="server" 
                        Text='<%# Bind("giros1_nroproforma") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 1 Nro Operacion" 
                SortExpression="giros1_nrooperacion">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" 
                        Text='<%# Bind("giros1_nrooperacion") %>'></asp:Label>
                    <asp:TextBox ID="tx_giro1_nrooperacion" runat="server" 
                        Text='<%# Bind("giros1_nrooperacion") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 1 Fecha" 
                SortExpression="fechaGiro1">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" 
                        Text='<%# Bind("fechaGiro1") %>'></asp:Label>
                    <asp:TextBox ID="tx_giro1_fecha" runat="server" 
                        Text='<%# Bind("fechaGiro1") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="tx_giro1_fecha_CalendarExtender" runat="server" 
                        TargetControlID="tx_giro1_fecha">
                    </asp:CalendarExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 1 Euros CP" 
                SortExpression="giros1_euros_cp">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" 
                        Text='<%# Bind("giros1_euros_cp") %>'></asp:Label>
                    <asp:TextBox ID="tx_giro1_euros_cp" runat="server" 
                        Text='<%# Bind("giros1_euros_cp") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 1 TC Orona" 
                SortExpression="giros1_tc_orona">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("giros1_tc_orona") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros1_tc_orona" runat="server" 
                        Text='<%# Bind("giros1_tc_orona") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 1 Dolares" 
                SortExpression="giros1_dolares">
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("giros1_dolares") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros1_dolares" runat="server" 
                        Text='<%# Bind("giros1_dolares") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 2 Nro Proforma" 
                SortExpression="giros2_nroproforma">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("giros2_nroproforma") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros2_nroproforma" runat="server" 
                        Text='<%# Bind("giros2_nroproforma") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 2 Nro Operacion" 
                SortExpression="giros2_nrooperacion">
                <ItemTemplate>
                    <asp:Label ID="Label14" runat="server" 
                        Text='<%# Bind("giros2_nrooperacion") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros2_nrooperacion" runat="server" 
                        Text='<%# Bind("giros2_nrooperacion") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 2 Fecha" 
                SortExpression="fechaGiro2">
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server" Text='<%# Bind("fechaGiro2") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros2_fecha" runat="server" 
                        Text='<%# Bind("fechaGiro2") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="tx_giros2_fecha_CalendarExtender" runat="server" 
                        TargetControlID="tx_giros2_fecha">
                    </asp:CalendarExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 2 Euros CP" 
                SortExpression="giros2_euros_cp">
                <ItemTemplate>
                    <asp:Label ID="Label16" runat="server" Text='<%# Bind("giros2_euros_cp") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros2_euros_cp" runat="server" 
                        Text='<%# Bind("giros2_euros_cp") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 2 TC Orona" 
                SortExpression="giros2_tc_orona">
                <ItemTemplate>
                    <asp:Label ID="Label17" runat="server" Text='<%# Bind("giros2_tc_orona") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros2_tc_orona" runat="server" 
                        Text='<%# Bind("giros2_tc_orona") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 2 Dolares" 
                SortExpression="giros2_dolares">
                <ItemTemplate>
                    <asp:Label ID="Label18" runat="server" 
                        Text='<%# Bind("giros2_dolares") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros2_dolares" runat="server" 
                        Text='<%# Bind("giros2_dolares") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 3 Nro Proforma" 
                SortExpression="giros3_nroproforma">
                <ItemTemplate>
                    <asp:Label ID="Label21" runat="server" Text='<%# Bind("giros3_nroproforma") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros3_nroproforma" runat="server" 
                        Text='<%# Bind("giros3_nroproforma") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 3 Nro Operacion" 
                SortExpression="giros3_nrooperacion">
                <ItemTemplate>
                    <asp:Label ID="Label25" runat="server" 
                        Text='<%# Bind("giros3_nrooperacion") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros3_nrooperacion" runat="server" 
                        Text='<%# Bind("giros3_nrooperacion") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 3 Fecha" 
                SortExpression="fechaGiro3">
                <ItemTemplate>
                    <asp:Label ID="Label26" runat="server" Text='<%# Bind("fechaGiro3") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros3_fecha" runat="server" 
                        Text='<%# Bind("fechaGiro3") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="tx_giros3_fecha_CalendarExtender" runat="server" 
                        TargetControlID="tx_giros3_fecha">
                    </asp:CalendarExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 3 Euros CP" 
                SortExpression="giros3_euros_cp">
                <ItemTemplate>
                    <asp:Label ID="Label27" runat="server" Text='<%# Bind("giros3_euros_cp") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros3_euros_cp" runat="server" 
                        Text='<%# Bind("giros3_euros_cp") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 3 TC Orona" 
                SortExpression="giros3_tc_orona">
                <ItemTemplate>
                    <asp:Label ID="Label28" runat="server" Text='<%# Bind("giros3_tc_orona") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros3_tc_orona" runat="server" 
                        Text='<%# Bind("giros3_tc_orona") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Giro 3 Dolares" 
                SortExpression="giros3_dolares">
                <ItemTemplate>
                    <asp:Label ID="Label29" runat="server" 
                        Text='<%# Bind("giros3_dolares") %>'></asp:Label>
                    <asp:TextBox ID="tx_giros3_dolares" runat="server" 
                        Text='<%# Bind("giros3_dolares") %>'></asp:TextBox>
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
