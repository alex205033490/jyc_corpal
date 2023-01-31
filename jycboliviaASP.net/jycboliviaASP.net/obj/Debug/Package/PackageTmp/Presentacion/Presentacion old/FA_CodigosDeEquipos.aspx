<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CodigosDeEquipos.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CodigosDeEquipos"  EnableEventValidation = "false"  %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register TagPrefix="inmoInfo" TagName="menuIzquierdo" Src="MenuIzquierdo.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_CodigosDeEquipos.css" rel="stylesheet" type="text/css" />
 
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>


 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 
   <div class="Centrar">

   <table>
    <tr>
    <td>
    <div class = "titulo">
     <h1>Codificacion de Equipos</h1>
    </div>
    </td>        
    </tr>

    <tr>
    <td>
    <div class = "BuscarEdificio">
        <table>
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Exbo:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_exbo" runat="server"></asp:TextBox>
            </td>
        <td></td>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Size="Medium" Text="Edificio :"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_edificio" runat="server" Height="25px" Width="400px"></asp:TextBox>
            </td>
        <td></td>
        <td>
            <asp:Button ID="bt_buscar" runat="server" Height="25px" Text="Buscar" 
                Width="100px" onclick="bt_buscar_Click" />
            </td>
        <td></td>
        </tr>
        </table>
    </div>
    </td>
    </tr>

    <tr>
    <td>
    <div class="TablaEdificios">
        <asp:GridView ID="gv_CodificacionDeEdificio" runat="server" BackColor="#CCCCCC" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
            CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
            AutoGenerateColumns="False" 
            onrowdatabound="gv_CodificacionDeEdificio_RowDataBound">
                     
            <Columns>
                <asp:BoundField DataField="Edificio" HeaderText="Edificio" 
                    SortExpression="Edificio" />
                <asp:BoundField DataField="direccion" HeaderText="direccion" 
                    SortExpression="direccion" />
                <asp:BoundField DataField="Exbo" HeaderText="Exbo" SortExpression="Exbo" />
                <asp:BoundField DataField="Vendido" HeaderText="Vendido" 
                    SortExpression="Vendido" />
                <asp:BoundField DataField="Instalado" HeaderText="Instalado" 
                    SortExpression="Instalado" />
                <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
                <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                <asp:BoundField DataField="parada" HeaderText="parada" 
                    SortExpression="parada" />
                <asp:BoundField DataField="pasajero" HeaderText="pasajero" 
                    SortExpression="pasajero" />
                <asp:BoundField DataField="velocidad" HeaderText="velocidad" 
                    SortExpression="velocidad" />
                <asp:BoundField DataField="modelo" HeaderText="modelo" 
                    SortExpression="modelo" />
                <asp:TemplateField HeaderText="QREquipo" SortExpression="QREquipo">
                    <ItemTemplate>
                        <asp:Image ID="imagenQR" runat="server" Height="127px" Width="152px" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("QREquipo") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
                     
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="Gray" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </div>
    </td>
    </tr>

    <tr>
    <td>
        <div>
            <table>
            <tr>
            <td></td>
            <td>
                <asp:Button ID="bt_excel" runat="server" Text="Excel" Height="25px" 
                    Width="100px" onclick="bt_excel_Click" />
                </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            </tr>
            </table>
        </div>
    </td>
    </tr>


    </table>   

   </div>
  
  


</asp:Content>
