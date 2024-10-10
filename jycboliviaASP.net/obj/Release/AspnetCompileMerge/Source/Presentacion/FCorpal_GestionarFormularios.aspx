<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_GestionarFormularios.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.WebForm1" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/Style_Gformulario.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


   <div class = "Centrar">
    <div class = "titulo">
     <h1>Gestionar Formularios</h1>
    </div>
    
    
    <div class="GF1">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Nombre :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_nombreF" runat="server" Width="229px"></asp:TextBox>
            </td>
            <td></td>
            <td>
            <asp:Button ID="bt_buscar" runat="server" Text="Buscar" Height="25px" 
                    onclick="bt_buscar_Click" style="height: 26px" />
            </td>
            
        </tr>
        <tr>
        <td></td>
            
            <td>
            <asp:Button ID="bt_insertar" runat="server" Text="Insertar" onclick="Button1_Click" 
                    Height="25px" />
             <asp:Button ID="bt_modificar" runat="server" Text="Modificar" 
                    onclick="Button2_Click" Height="25px" />
                <asp:Button ID="bt_limpiar" runat="server" Text="Limpiar" Height="25px" 
                    onclick="bt_limpiar_Click" />
                
            </td>
        </tr>

    </table>
</div>
    

    <div class="GF2">

    <asp:GridView ID="gv_formulario" runat="server" 
        onselectedindexchanged="gv_formulario_SelectedIndexChanged" 
        BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
        CellPadding="4" CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
<asp:TemplateField><ItemTemplate>
         <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="False"></asp:CheckBox>
      
</ItemTemplate>
</asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#669900" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>

</div>


    <div class="GF3">
    <asp:Button ID="bt_eliminarSeleccionado" runat="server" 
        Text="Eliminar Seleccionados" Height="25px" onclick="Button6_Click" />
    </div>    
    

    <div class="GF4"></div>
    

   </div>





</asp:Content>
