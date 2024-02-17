<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ImportacionProrrateoCostos_Transporte.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ImportacionProrrateoCostos_Transporte" %>
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
       <inmoInfo:menu ID="Menu1" runat="server"/>
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
    <asp:Label ID="Label31" runat="server" Text="Dui:" Font-Size="Small"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_nroDui" runat="server"></asp:TextBox>
    </td>
<td></td>
<td>
    <asp:Label ID="Label32" runat="server" Font-Size="Small" Text="Contenedor:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_contenedor" runat="server"></asp:TextBox>
    </td>
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
        <asp:Button ID="bt_Limpiar" runat="server" Height="25px" Text="Limpiar" />
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

            <asp:BoundField DataField="codigo" HeaderText="Codigo" 
                SortExpression="codigo" />
            <asp:BoundField DataField="exbo" HeaderText="Exbo" SortExpression="exbo" />
            <asp:BoundField DataField="Edificio" HeaderText="Edificio" 
                SortExpression="Edificio" />
            <asp:BoundField DataField="Vendido" HeaderText="Vendido" 
                SortExpression="Vendido" />
            <asp:BoundField DataField="Instalado" HeaderText="Instalado" 
                SortExpression="Instalado" />
            <asp:BoundField DataField="UNE" HeaderText="UNE" 
                SortExpression="UNE" />
            <asp:BoundField DataField="CIUDAD" HeaderText="Ciudad" 
                SortExpression="CIUDAD" />
            <asp:BoundField DataField="Estado1" HeaderText="Estado" 
                SortExpression="Estado1" />
            <asp:BoundField DataField="fechaaproximadoarribopuerto" 
                HeaderText="Aproximado Arribo Puerto" 
                SortExpression="fechaaproximadoarribopuerto" />
            <asp:BoundField DataField="Consignatario" HeaderText="Consignatario" 
                SortExpression="Consignatario" />
            <asp:TemplateField HeaderText="Semana Expedicion" 
                SortExpression="semana_expedicion">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("semana_expedicion") %>'></asp:Label>
                    <asp:TextBox ID="tx_semanaExpadicion1" runat="server" 
                        Text='<%# Bind("semana_expedicion") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nro DUI" SortExpression="codnrodui">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("codnrodui") %>'></asp:Label>
                    <asp:TextBox ID="tx_codnrodui" runat="server" Text='<%# Bind("codnrodui") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nro Contenedor" 
                SortExpression="codnrocontenedor">
                <ItemTemplate>
                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("codnrocontenedor") %>'></asp:Label>
                    <asp:TextBox ID="tx_codnrocontenedor" runat="server" 
                        Text='<%# Bind("codnrocontenedor") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tamaño Contenedor" 
                SortExpression="pct_tamanio_contenedor">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# Bind("pct_tamanio_contenedor") %>'></asp:Label>
                    <asp:TextBox ID="tx_pct_tamanio_contenedor" runat="server" 
                        Text='<%# Bind("pct_tamanio_contenedor") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nro de HB/L" SortExpression="nro_bl">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("nro_bl") %>'></asp:Label>
                    <asp:TextBox ID="tx_nro_bl" runat="server" Text='<%# Bind("nro_bl") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Peso" SortExpression="pct_peso">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("pct_peso") %>'></asp:Label>
                    <asp:TextBox ID="tx_pctpeso" runat="server" Text='<%# Bind("pct_peso") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Monto $us X Aplicar Proyecto" 
                SortExpression="montodolares_x_aplicarproyecto">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" 
                        Text='<%# Bind("montodolares_x_aplicarproyecto") %>'></asp:Label>
                    <asp:TextBox ID="TextBox4" runat="server" 
                        Text='<%# Bind("montodolares_x_aplicarproyecto") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Valor Girado del Equipo" 
                SortExpression="valorgiradodelequipo">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" 
                        Text='<%# Bind("valorgiradodelequipo") %>'></asp:Label>
                    <asp:TextBox ID="TextBox5" runat="server" 
                        Text='<%# Bind("valorgiradodelequipo") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="% segun Importe del Giro del Equipo" 
                SortExpression="porcentajeImportedegirodelequipo">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" 
                        Text='<%# Bind("porcentajeImportedegirodelequipo") %>'></asp:Label>
                    <asp:TextBox ID="TextBox6" runat="server" 
                        Text='<%# Bind("porcentajeImportedegirodelequipo") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Prorrateo de Transporte" 
                SortExpression="prorrateodetransporte">
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" 
                        Text='<%# Bind("prorrateodetransporte") %>'></asp:Label>
                    <asp:TextBox ID="TextBox7" runat="server" 
                        Text='<%# Bind("prorrateodetransporte") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Costo Transporte" 
                SortExpression="pct_costo_transporte">
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" 
                        Text='<%# Bind("pct_costo_transporte") %>'></asp:Label>
                    <asp:TextBox ID="tx_pct_costo_transporte" runat="server" 
                        Text='<%# Bind("pct_costo_transporte") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Costo Internacional" 
                SortExpression="pct_costo_internacional">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" 
                        Text='<%# Bind("pct_costo_internacional") %>'></asp:Label>
                    <asp:TextBox ID="tx_pct_costo_internacional" runat="server" 
                        Text='<%# Bind("pct_costo_internacional") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Costo Nacional" 
                SortExpression="pct_costo_nacional">
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("pct_costo_nacional") %>'></asp:Label>
                    <asp:TextBox ID="tx_pct_costo_nacional" runat="server" 
                        Text='<%# Bind("pct_costo_nacional") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha Pagar Proveedor" 
                SortExpression="pct_fechapagarproveedor">
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server" 
                        Text='<%# Bind("pct_fechapagarproveedor") %>'></asp:Label>
                    <asp:TextBox ID="tx_pct_fechapagarproveedor" runat="server" 
                        Text='<%# Bind("pct_fechapagarproveedor") %>'></asp:TextBox>
                    <asp:CalendarExtender ID="tx_pct_fechapagarproveedor_CalendarExtender" 
                        runat="server" TargetControlID="tx_pct_fechapagarproveedor">
                    </asp:CalendarExtender>
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
        Text="Exportar Excel" />
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
