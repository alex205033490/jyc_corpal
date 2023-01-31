<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FCorpal_ConciliacionBancaria.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_ConciliacionBancaria" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_ConciliacionBancaria.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

<div class="Centrar">
    <div class="titulo"><h3>Conciliacion Bancaria</h3></div>
    
    <table>
    <tr>
    <td></td><td>
        <asp:Label ID="Label9" runat="server" Text="Banco :"></asp:Label>
        </td><td>
            <asp:DropDownList ID="dd_banco" runat="server" Width="200px" 
                AutoPostBack="true" onselectedindexchanged="dd_banco_SelectedIndexChanged">
            </asp:DropDownList>
        </td><td></td><td>Cuenta:</td><td>
        <asp:DropDownList ID="dd_CuentaBancaria" runat="server" Width="200px" 
           AutoPostBack="true" onselectedindexchanged="dd_CuentaBancaria_SelectedIndexChanged">
        </asp:DropDownList>
        </td>
        <td></td>
    </tr>
    </table>
    
    <br />

    <table>  
    <tr>
    <td></td><td>
        <asp:Label ID="Label10" runat="server" Text="Cuenta:"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_cuentaview" runat="server" Font-Size="X-Small" 
                Width="120px"></asp:TextBox>
        </td><td></td><td></td><td></td><td>
        <asp:Label ID="Label11" runat="server" Text="Tipo:"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_tipoCuentaview" runat="server" Font-Size="X-Small" 
                Width="120px"></asp:TextBox>
        </td><td></td>
    </tr>
          
    <tr>
    <td></td><td>
        <asp:Label ID="Label3" runat="server" Text="JYC"></asp:Label>
        </td><td></td><td></td><td></td><td></td><td>
        <asp:Label ID="Label4" runat="server" Text="Banco"></asp:Label>
        </td><td></td><td></td>
    </tr>
    <tr>
    <td></td><td>
        <asp:Label ID="Label1" runat="server" Text="Saldo Anterior :" Font-Size="Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_saldoAnterior" runat="server" Font-Size="X-Small" 
                Width="120px">0</asp:TextBox>
        </td><td></td><td></td><td></td><td>
        <asp:Label ID="Label2" runat="server" Text="Extracto Bancario :" 
            Font-Size="Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_extractoBancario" runat="server" Font-Size="X-Small" 
                Width="120px">0</asp:TextBox>
        </td><td></td>
        <td>
            <asp:Button ID="bt_calcular" runat="server" onclick="bt_calcular_Click" 
                Text="Calcular" />
        </td>
        
    </tr>
    </table>
    
   
    <asp:Label ID="Label8" runat="server" Text="Cheque en circulacion"></asp:Label>
    <table>     
    <tr>    
    <td></td><td>
        <asp:Label ID="Label5" runat="server" Text="Fecha:" Font-Size="Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_FechaCheque" runat="server" Font-Size="X-Small" 
                Width="120px"></asp:TextBox>
            <asp:CalendarExtender ID="tx_FechaCheque_CalendarExtender" runat="server" 
                TargetControlID="tx_FechaCheque">
            </asp:CalendarExtender>
        </td><td></td><td>
        <asp:Label ID="Label6" runat="server" Text="Nro CHQ.:" Font-Size="Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_nrocheque" runat="server" Font-Size="X-Small" Width="120px"></asp:TextBox>
        </td><td>&nbsp;</td><td>
        <asp:Label ID="Label7" runat="server" Text="Monto:" Font-Size="Small"></asp:Label>
        </td><td>
            <asp:TextBox ID="tx_monto" runat="server" Font-Size="X-Small" Width="120px"></asp:TextBox>
        </td>
        <td></td>
        <td>
            <asp:Button ID="tx_Adicionar" runat="server" Text="Adicionar" 
                onclick="tx_Adicionar_Click" />
        </td>
        <td></td>
    </tr>
    </table>    
    <div class="datoscheques">
        <asp:GridView ID="gv_chequesCirculacion" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
            onrowdeleting="gv_chequesCirculacion_RowDeleting">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </div>
    <br />
    <div class="datosSuma">
        <asp:GridView ID="gv_calculos" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            Font-Size="Small" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </div>

     <div class="blanco">
    
         <asp:Button ID="bt_guardar" runat="server" onclick="bt_guardar_Click" 
             Text="Guardar" />
    
    </div>

    <div class="blanco">
    
    </div>

</div>

</asp:Content>
