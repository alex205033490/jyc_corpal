<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FEstadisticaParqueAscensores.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FEstadisticaParqueAscensores" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <link href="../Styles/Style_EstadisticaParqueAscensores.css" rel="stylesheet" type="text/css" />
   <style type="text/css">
        .style1
        {
           width: 222px;
           height: 21px;
       }
        .style2
        {
            width: 145px;
           height: 21px;
       }
        .style4
        {
            width: 145px;
            height: 25px;
        }
       .style6
       {
           height: 42px;
       }
       .style7
       {
           height: 17px;
       }
       .style8
       {
           height: 17px;
           width: 130px;
       }
       .style9
       {
           height: 25px;
           width: 222px;
       }
       .style10
       {
           width: 222px;
           height: 17px;
       }
       .style11
       {
           width: 145px;
           height: 17px;
       }
       .style12
       {
           height: 19px;
       }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

<table>
<tr>
<td>
<div class = "GPA1">
<table style="width: 300px; height: 44px; ">
<tr>
<td class="style6">
    <asp:Label ID="Label4" runat="server" Text="ESTADISTICA PARQUE ASCENSORES" Font-Size="Small"></asp:Label>
</td>
</tr>
</table>
<table style="width: 167px; ">
<tr>
<td class="style7">
    <asp:Label ID="Label5" runat="server" Text="AÑO"></asp:Label>
    </td>
<td class="style8">
    <asp:DropDownList ID="ddlAnio" runat="server" Height="20px">
    </asp:DropDownList>
    </td>
    <td> 
        <asp:Button ID="btnListar" runat="server" Text="Listar" 
            onclick="btnListar_Click" Height="25px" Width="60px" />
    </td>
</tr>
</table>
    <asp:GridView ID="GridView1" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" ForeColor="Black" Width="42px" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" 
        Font-Size="X-Small" Height="45px" 
        style=" margin-top: 22px;">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#00CC00" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838"/>
    </asp:GridView>
    <br />

    <table style="width: 268px; height: 67px; margin-top: 13px">
    <tr>
    <td class="style9"> 
        <asp:Label ID="lbTotalParqueAscensor1" runat="server" 
            Text="TOTAL EQUIPO FUNCIONANDO: " Height="21px" Width="218px" 
            Visible="False" Font-Size="X-Small"></asp:Label></td>
    <td class="style4"> 
        <asp:Label ID="lbTotalParqueAscensor" runat="server" Text="0" 
            Visible="False" Font-Size="X-Small"></asp:Label>
        </td>
    </tr>
    <tr>
    <td class="style1"> 
        <asp:Label ID="lbTotalParqueAscensorPorcentaje1" runat="server" 
            Text="TOTAL EQUIPO FUNCIONANDO (%): " Height="24px" Width="243px" 
            Visible="False" style="margin-top: 0px; margin-bottom: 0px" 
            Font-Size="X-Small"></asp:Label> </td>
    <td class="style2"> 
        <asp:Label ID="lbTotalParqueAscensorPorcentaje" runat="server" Text="0" 
            Visible="False" Font-Size="X-Small"></asp:Label>
        </td>
    </tr>
    <tr>
    <td class="style12">
        <asp:Label ID="Label1" runat="server" Text="TOTAL EQUIPOS POR FUNCIONAR :" 
            Font-Size="X-Small"></asp:Label>
    </td>
    <td class="style12">
        <asp:Label ID="lb_TotalEquiposPorFuncionar" runat="server" Text="0" 
            Font-Size="X-Small"></asp:Label>
    </td>
    </tr>

    <tr>
    <td>
        <asp:LinkButton ID="lkb_faltantesMantenimiento" runat="server" 
            BackColor="White" ForeColor="#0066FF" 
            onclick="lkb_faltantesMantenimiento_Click">TOTAL FALTANTES MANTENIMIENTO:</asp:LinkButton>
        </td>
    <td>
        <asp:Label ID="lb_faltantesMantenimiento" runat="server" Font-Bold="False" 
            Font-Size="X-Small" Text="0"></asp:Label>
        </td>
    </tr>


    <tr>
    <td class="style10"> 
        <asp:Label ID="lbTotalEquipo1" runat="server" 
            Text="TOTAL EQUIPOS: " Visible="False" Font-Size="X-Small"></asp:Label></td>
    <td class="style11"> 
        <asp:Label ID="lbTotalEquipo" runat="server" Text="0" Visible="False" 
            Font-Size="X-Small"></asp:Label>
        </td></tr>
        <tr>
        <td>
        
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Exportar en Exel" />

        </td>
        <td></td>
        </tr>


    </table>
    </div>
</td>

<td>
<div class= "GPA2">
 <asp:GridView ID="GridView2" runat="server" BackColor="#CCCCCC" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
        CellSpacing="2" ForeColor="Black" Width="189px" style="margin-left: 5px; margin-top: 12px;" 
        AllowPaging="True" onpageindexchanging="GridView2_PageIndexChanging" 
        onselectedindexchanged="GridView2_SelectedIndexChanged" 
        Font-Size="X-Small" Height="16px" Visible="False">
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838"/>
    </asp:GridView>
</div>

</td>

</tr>
</table>

<div class="GPA3"></div>


</asp:Content>
