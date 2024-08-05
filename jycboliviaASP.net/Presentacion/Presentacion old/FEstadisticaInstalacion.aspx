<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FEstadisticaInstalacion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FDatosEquipo" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/Style_GEstadisticaInstalacion.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style1
        {
            height: 17px;
            width: 177px;
        }
        .style2
        {
            height: 30px;
            width: 177px;
        }
        .style3
        {
            width: 324px;
            height: 36px;
        }
        .style4
        {
            width: 341px;
            height: 39px;
        }
        .style5
        {
            height: 30px;
        }
        .style6
        {
            height: 17px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

<div class="Centrar">
<div class="titulo"></div>

<table>
<tr>
<td>
<div class="titulo2">
<asp:Label ID="Label3" runat="server" Font-Size="Small" Text=""><center>ESTADISTICA DE INSTALACION </center></asp:Label>
</div>
</td>
<td>
<div class="titulo3">
<asp:Label ID="lbDatosEquipo" runat="server" Text="" Font-Size="Small" 
        Visible="False"><center>DATOS DEL EQUIPO </center> </asp:Label>
</div>
</td>
</tr>

<tr>
<td>
<div class ="GEI1">
<asp:GridView ID="GridView1" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" ForeColor="Black" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" 
        Font-Size="X-Small" style="margin-left: 38px" Height="160px" 
        Width="147px">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#00CC00" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" Font-Size="X-Small" />
    </asp:GridView>
    <br />
    <br />
    <table style="width: 269px; margin-left: 34px; margin-top: 0px">
    <tr>
    <td class="style2"> 
        <asp:Label ID="Label1" runat="server" 
            Text="TOTAL POR FUNCIONAR: " Width="180px"></asp:Label></td>
    <td class="style5">  <asp:Label ID="lbTotalEquipoFuncionar" runat="server" 
            Width="30px">0</asp:Label>  </td>
    </tr>
    
    <tr>
    <td>
        <asp:Label ID="Label4" runat="server" Text="TOTAL EQUIPOS :"></asp:Label>
    </td>
    <td>
        <asp:Label ID="lb_CandidadEquipos" runat="server" Text="0"></asp:Label>
    </td>
    </tr>


    <tr>
    <td class="style1"> 
    <asp:Label ID="Label2" runat="server" Text="TOTAL POR FUNCIONAR (%): " 
            Width="185px"></asp:Label></td>
   <td class="style6"><asp:Label ID="lbTotalPorcentaje" runat="server" Width="70px">0</asp:Label> </td>
   </tr>
    </table>
</div>

</td>


<td>
<div class ="GEI2">
    <asp:GridView ID="GridView2" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" ForeColor="Black" AllowPaging="True" 
        onpageindexchanging="GridView2_PageIndexChanging" Font-Size="X-Small" 
        style="margin-left: 0px; margin-top: 0px;" Width="202px" Height="16px" 
        PageSize="12">

        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#00CC00" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
</div>

</td>
</tr>
</table>

<div class="GEI3">
    <asp:Button ID="bt_ExportarExel" runat="server" onclick="bt_ExportarExel_Click" 
        Text="Exportar Excel" style="margin-bottom: 0px" />
    </div>

</div>


</asp:Content>
