<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_VaciadoR148.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_VaciadoR148" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
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
    .style2
    {
        width: 22px;
    }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


<div class="Centrar">
<div class="titulo">
  <h3>  
      <asp:Label ID="lbTitulo" runat="server" 
          Text="Vaciar R-148 Al Simec "></asp:Label> </h3>
</div>

<table>
<tr>
<td>
    <div class="sc1">
        <table>
        <tr>
        <td class="style2"></td>
        <td>
            <asp:Label ID="Label7" runat="server" Text="Edificio:"></asp:Label>
            </td>
        <td></td>
        <td>
            <asp:TextBox ID="tx_proyecto" runat="server" Width="300px"></asp:TextBox>
            </td>
        <td></td>
        <td>
            <asp:Button ID="bt_buscar" runat="server" onclick="bt_buscar_Click" 
                Text="Buscar" />
            </td>
        </tr>
        </table>
        
        <table>
         <tr>
            <td class="style1"></td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="TC:"></asp:Label>
             </td>
            <td>
                <asp:TextBox ID="tx_tipoCambio" runat="server" Width="70px"></asp:TextBox>
             </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            </tr>
        </table>

        
        <table>     
            <tr>
            <td class="style1"></td>
            <td></td>
            <td>
                <asp:Button ID="bt_anularR148" runat="server" onclick="bt_anularR148_Click" 
                    Text="Anular" />
                </td>
            <td></td>
            <td></td>
            <td>
                <asp:Button ID="bt_vaciarAlSimec" runat="server" Text="Vaciar R-148 al Simec" 
                    onclick="bt_vaciarAlSimec_Click" />
                </td>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <asp:Button ID="bt_excel" runat="server" onclick="bt_excel_Click" 
                    Text="Excel" />
                </td>
            <td></td>
            </tr>
        </table>
    </div>
</td>
</tr>

<tr>
<td>
<div class="sc2">
    <asp:GridView ID="gv_r148_vaciarSimec" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
        AutoGenerateColumns="False">
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
            <asp:BoundField DataField="codigo" HeaderText="codigo" 
                SortExpression="codigo" />
            <asp:BoundField DataField="fecha-R148" HeaderText="fecha-R148" 
                SortExpression="fecha-R148" />
            <asp:BoundField DataField="hora" HeaderText="hora" SortExpression="hora" />
            <asp:BoundField DataField="proyecto" HeaderText="proyecto" 
                SortExpression="proyecto" />
            <asp:BoundField DataField="cantEquipos" HeaderText="cantEquipos" 
                SortExpression="cantEquipos" />
            <asp:BoundField DataField="ciudadventa" HeaderText="ciudadventa" 
                SortExpression="ciudadventa" />
            <asp:BoundField DataField="ciudadinstalacion" HeaderText="ciudadinstalacion" 
                SortExpression="ciudadinstalacion" />
            <asp:BoundField DataField="direccioninstalacion" 
                HeaderText="direccioninstalacion" SortExpression="direccioninstalacion" />
            <asp:BoundField DataField="zona" HeaderText="zona" SortExpression="zona" />
            <asp:BoundField DataField="NroContrato" HeaderText="NroContrato" 
                SortExpression="NroContrato" />
            <asp:BoundField DataField="ValorContrato" HeaderText="ValorContrato" 
                SortExpression="ValorContrato" />
            <asp:BoundField DataField="moneda" HeaderText="moneda" 
                SortExpression="moneda" />
            <asp:BoundField DataField="fecha-Contrato" HeaderText="fecha-Contrato" 
                SortExpression="fecha-Contrato" />
            <asp:BoundField DataField="fecha-Pedigo" HeaderText="fecha-Pedigo" 
                SortExpression="fecha-Pedigo" />
            <asp:BoundField DataField="fecha-Embarque" HeaderText="fecha-Embarque" 
                SortExpression="fecha-Embarque" />
            <asp:BoundField DataField="resp_une" HeaderText="resp_une" 
                SortExpression="resp_une" />
            <asp:BoundField DataField="resp_nacional" HeaderText="resp_nacional" 
                SortExpression="resp_nacional" />
            <asp:BoundField DataField="resp_gun" HeaderText="resp_gun" 
                SortExpression="resp_gun" />
            <asp:BoundField DataField="resp_jpr" HeaderText="resp_jpr" 
                SortExpression="resp_jpr" />
            <asp:BoundField DataField="resp_fpr" HeaderText="resp_fpr" 
                SortExpression="resp_fpr" />
            <asp:BoundField DataField="resp_flo" HeaderText="resp_flo" 
                SortExpression="resp_flo" />
            <asp:BoundField DataField="observaciones" HeaderText="observaciones" 
                SortExpression="observaciones" />
            <asp:TemplateField HeaderText="cod_varSimec" SortExpression="cod_varSimec">
                <ItemTemplate>
                    <asp:Label ID="lb_variableSimec" runat="server" 
                        Text='<%# Bind("cod_varSimec") %>' Visible="False"></asp:Label>
                    <asp:TextBox ID="tx_variableSimec" runat="server" BackColor="Yellow" 
                        Text='<%# Bind("cod_varSimec") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
&nbsp;
</td>
</tr>

<tr>
<td>
    <asp:Label ID="Label6" runat="server" Text="Cantidad:"></asp:Label>
    <asp:Label ID="lb_cantDatos" runat="server" Text="0"></asp:Label>
    </td>
</tr>
<tr>
<td></td>
</tr>

</table>

</div>


</asp:Content>
