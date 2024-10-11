<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_ImportacionJYCIA.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ImportacionJYCIA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ImportacionJYCIA.css" rel="stylesheet" type="text/css" />

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
            width: 112px;
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
<div class="titulo">
    <h3>
        <asp:Label ID="lb_titulo" runat="server" Text="Importacion & JYCIA"></asp:Label></h3>
</div>
</td>
</tr>


<tr>
<td>
<div class="GCP1">
<div style="width: 818px">
<table style="height: auto; width: auto; margin-left:5px; margin-top: 0px; margin-right: 0px;">
<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="Label25" runat="server" Font-Size="X-Small" Text="Edificio:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_edificio" runat="server" Width="250px"></asp:TextBox>
    </td>
<td class="style18">
    <asp:Label ID="Label26" runat="server" Font-Size="X-Small" Text="Exbo:"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="tx_exbo" runat="server" Width="120px"></asp:TextBox>
    </td>
<td>
    &nbsp;</td>
<td> </td>
<td> 
    <asp:Label ID="lb_basedatos" runat="server" Font-Size="X-Small" 
        Text="Base de Datos:"></asp:Label>
    </td>
<td>
    <asp:DropDownList ID="dd_BaseDatos" runat="server" Width="120px" 
        Font-Size="X-Small" onselectedindexchanged="dd_BaseDatos_SelectedIndexChanged" AutoPostBack="True">
                                              <asp:ListItem>Ninguno</asp:ListItem>
                                              <asp:ListItem>Santa Cruz</asp:ListItem>
                                              <asp:ListItem>Cochabamba</asp:ListItem>
                                              <asp:ListItem>La Paz</asp:ListItem>
                                              <asp:ListItem>Sucre</asp:ListItem>
                                              <asp:ListItem>Oruro</asp:ListItem>
                                              <asp:ListItem>Potosi</asp:ListItem>
                                              <asp:ListItem>Tarija</asp:ListItem>
                                              <asp:ListItem>Yacuiba</asp:ListItem>
                                              <asp:ListItem>Villamontes</asp:ListItem>
                                              <asp:ListItem>Asuncion-Paraguay</asp:ListItem>
                                              <asp:ListItem>Prueba</asp:ListItem>
    </asp:DropDownList>
</td>
<td> </td>
</tr>

</table>
 </div>
 <div style="width: 401px; margin-left: 214px">
<table style="width: 405px">
<tr>
    <td class="style17">
        <asp:Button ID="bt_Limpiar" runat="server" Height="25px" Text="Limpiar" 
            onclick="bt_Limpiar_Click" />
    </td>
    <td class="style9">
        &nbsp;</td>
    <td class="style7">
        <asp:Button ID="bt_buscar"  runat="server"  Text="Buscar" Height="25px" 
            onclick="bt_buscar_Click" />
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
        onselectedindexchanged="gv_datos_SelectedIndexChanged" BackColor="#CCCCCC" BorderColor="#999999" 
        BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" 
        ForeColor="Black" AutoGenerateColumns="False" >

        <Columns>
         <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="OnCheckedChanged" />
                    </ItemTemplate>
         </asp:TemplateField>

            <asp:BoundField DataField="codigo" HeaderText="codigo" 
                SortExpression="codigo" />
            <asp:BoundField DataField="Edificio" HeaderText="Edificio" 
                SortExpression="Edificio" />
            <asp:BoundField DataField="exbo" HeaderText="exbo" SortExpression="exbo" />
            <asp:BoundField DataField="tipoEquipo" HeaderText="tipoEquipo" 
                SortExpression="tipoEquipo" />
            <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
            <asp:BoundField DataField="parada" HeaderText="parada" 
                SortExpression="parada" />
            <asp:BoundField DataField="pasajero" HeaderText="pasajero" 
                SortExpression="pasajero" />
            <asp:BoundField DataField="modelo" HeaderText="modelo" 
                SortExpression="modelo" />
            <asp:BoundField DataField="velocidad" HeaderText="velocidad" 
                SortExpression="velocidad" />
            <asp:BoundField DataField="Valor Fob (Alvaro)" HeaderText="Valor Fob (Alvaro)" 
                SortExpression="Valor Fob (Alvaro)" />
            <asp:BoundField DataField="Transporte Maritimo (Alvaro)" 
                HeaderText="Transporte Maritimo (Alvaro)" 
                SortExpression="Transporte Maritimo (Alvaro)" />
            <asp:BoundField DataField="Fecha Aprox. Embarque" 
                HeaderText="Fecha Aprox. Embarque" SortExpression="Fecha Aprox. Embarque" />
            <asp:BoundField DataField="Fecha Equipo" HeaderText="Fecha Equipo" 
                SortExpression="Fecha Equipo" />

            <asp:TemplateField HeaderText="nrofactura" SortExpression="nrofactura">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("nrofactura") %>' Visible ="false" ></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("nrofactura") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha Factura" SortExpression="Fecha Factura">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("[Fecha Factura]") %>' Visible ="false" ></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" 
                        TargetControlID="TextBox2">
                    </asp:CalendarExtender>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("[Fecha Factura]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="montofactura" SortExpression="montofactura">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("montofactura") %>' Visible ="false" ></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("montofactura") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha Giro1" SortExpression="Fecha Giro1">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("[Fecha Giro1]") %>' Visible ="false" ></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox4_CalendarExtender" runat="server" 
                        TargetControlID="TextBox4">
                    </asp:CalendarExtender>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("[Fecha Giro1]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="montogiro1" SortExpression="montogiro1">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("montogiro1") %>' Visible ="false" ></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("montogiro1") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha Giro2" SortExpression="Fecha Giro2">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("[Fecha Giro2]") %>' Visible ="false" ></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox6_CalendarExtender" runat="server" 
                        TargetControlID="TextBox6">
                    </asp:CalendarExtender>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("[Fecha Giro2]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="montogiro2" SortExpression="montogiro2">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("montogiro2") %>' Visible ="false" ></asp:TextBox>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("montogiro2") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha Giro3" SortExpression="Fecha Giro3">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("[Fecha Giro3]") %>' Visible ="false" ></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox8_CalendarExtender" runat="server" 
                        TargetControlID="TextBox8">
                    </asp:CalendarExtender>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("[Fecha Giro3]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="montogiro3" SortExpression="montogiro3">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("montogiro3") %>' Visible ="false" ></asp:TextBox>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("montogiro3") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha Giro4" SortExpression="Fecha Giro4">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("[Fecha Giro4]") %>' Visible ="false" ></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox10_CalendarExtender" runat="server" 
                        TargetControlID="TextBox10">
                    </asp:CalendarExtender>
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("[Fecha Giro4]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="montogiro4" SortExpression="montogiro4">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("montogiro4") %>' Visible ="false" ></asp:TextBox>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("montogiro4") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha Giro5" SortExpression="Fecha Giro5">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("[Fecha Giro5]") %>' Visible ="false" ></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox12_CalendarExtender" runat="server" 
                        TargetControlID="TextBox12">
                    </asp:CalendarExtender>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("[Fecha Giro5]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="montogiro5" SortExpression="montogiro5">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("montogiro5") %>' Visible ="false" ></asp:TextBox>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("montogiro5") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="valorfob" SortExpression="valorfob">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("valorfob") %>' Visible ="false" ></asp:TextBox>
                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("valorfob") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="valortransportemaritimo2" 
                SortExpression="valortransportemaritimo2">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("valortransportemaritimo2") %>' Visible ="false" ></asp:TextBox>
                    <asp:Label ID="Label15" runat="server"  Text='<%# Bind("valortransportemaritimo2") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="nrocontenedor" SortExpression="nrocontenedor">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("nrocontenedor") %>' Visible ="false" ></asp:TextBox>
                    <asp:Label ID="Label16" runat="server" Text='<%# Bind("nrocontenedor") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="1erpago" SortExpression="1erpago">
                <ItemTemplate>
                <!--    <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("1erpago") %>' Visible ="false" ></asp:TextBox>  -->
                    <asp:Label ID="Label17" runat="server" Text='<%# Bind("1erpago") %>'></asp:Label>                     
                    <asp:CheckBox ID="CheckBox1" runat="server" Visible ="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="2dopago" SortExpression="2dopago">
                <ItemTemplate>
                <!--    <asp:TextBox ID="TextBox18" runat="server" Text='<%# Bind("2dopago") %>' Visible ="false" ></asp:TextBox>  -->
                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("2dopago") %>'></asp:Label>
                    <asp:CheckBox ID="CheckBox2" runat="server" Visible ="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="3erpago" SortExpression="3erpago">
                <ItemTemplate>
                <!--    <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("3erpago") %>' Visible ="false" ></asp:TextBox>  -->
                    <asp:Label ID="Label19" runat="server" Text='<%# Bind("3erpago") %>'></asp:Label>
                    <asp:CheckBox ID="CheckBox3" runat="server" Visible ="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TOTAL GIROS" HeaderText="TOTAL GIROS" 
                SortExpression="TOTAL GIROS" />
            <asp:BoundField DataField="DIFERENCIA VALOR REAL / VRS GIRO" 
                HeaderText="DIFERENCIA VALOR REAL / VRS GIRO" 
                SortExpression="DIFERENCIA VALOR REAL / VRS GIRO" />
            <asp:BoundField DataField="DIFERENCIA VALOR REAL VRS FACTURA" 
                HeaderText="DIFERENCIA VALOR REAL VRS FACTURA" 
                SortExpression="DIFERENCIA VALOR REAL VRS FACTURA" />
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
