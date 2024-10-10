<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_EstadoSeguimientoGeneral.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_EstadoSeguimientoGeneral" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_EstadoSeguimientoGeneral.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>

<div class="Centrar">
<div class="titulo">
<h3>Estados de Mantenimientos Funcionando</h3>    
</div>

<div class = "ESG1">
<table>
<tr>
<td></td>
<td> 
    <asp:Label ID="Label1" runat="server" Text="De : "></asp:Label>
</td>
<td></td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Hasta :"></asp:Label>
</td>
<td></td>
<td></td>
</tr>
<tr>
<td></td>
<td>
    <asp:TextBox ID="tx_fecha1" runat="server"></asp:TextBox> 
</td>
<td>
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tx_fecha1">
    </asp:CalendarExtender>
</td>
<td>
    <asp:TextBox ID="tx_fecha2" runat="server"></asp:TextBox>
</td>
<td>
    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID = "tx_fecha2">
    </asp:CalendarExtender>
</td>
<td>
    <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
        onclick="bt_buscar_Click" />
</td>
</tr>
</table>
</div>

<div class = "ESG11"><h3>Estado General Instalacion</h3></div>
<div class = "botonLink">
    <asp:LinkButton ID="lk_excelInstalacion" runat="server" 
        onclick="LinkButton1_Click" Font-Size="X-Small">Excel Instalacion</asp:LinkButton>
</div>

<div class = "ESG2">
    <asp:GridView ID="gv_EstadoInstalacion" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#99CC00" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="Gray" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
 <Columns>
   <asp:TemplateField>
      <ItemTemplate>
         <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="False"></asp:CheckBox>
      </ItemTemplate>
   </asp:TemplateField>
</Columns>
    </asp:GridView>
    </div>
    
<div class="ESG21"> <h3>Estado General Mantenimiento</h3> </div>
<div class="botonLink">
    <asp:LinkButton ID="lk_excelMantenimiento" runat="server" Font-Size="X-Small" 
        onclick="lk_excelMantenimiento_Click">Excel Mantenimiento</asp:LinkButton>
</div>

<div class = "ESG3">
    <asp:GridView ID="gv_EstadoMantenimiento" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
 <Columns>
   <asp:TemplateField>
      <ItemTemplate>
         <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="False"></asp:CheckBox>
      </ItemTemplate>
   </asp:TemplateField>
</Columns>
        
    </asp:GridView>
    </div>
<div class = "ESG4">
    <asp:Button ID="bt_EstadisticaSelect" runat="server" Text="Estadistica Select" 
        Height="30px" onclick="bt_EstadisticaSelect_Click" 
        style="margin-left: 0px" />
</div>
<div class = "ESG5">
  <asp:Chart ID="Chart1" runat="server" Height="400px" Width="900px">
        <Series>
            <asp:Series Name="Series1">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>

</div>

<div class = "ESG6"></div>



</div>








</asp:Content>
