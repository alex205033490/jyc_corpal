<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CronoRuta.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CronoRuta" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_cronogramaRutaVisitaMantenimiento.css" rel="stylesheet"
        type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="Centrar">
        <div class="titulo">
        <h3>Cronograma Ruta</h3>
        </div>    

    <div class = "vista1">
        <asp:Label ID="Label2" runat="server" Text="Seleccionar la semana a visitar segun la cantidad de visita"></asp:Label>
        <asp:GridView ID="gv_cronogramaVisita" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
            CellPadding="3" Font-Size="Small" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:TemplateField HeaderText="Cant. Visita">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Semana 1">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>            
                
                <asp:TemplateField HeaderText="Semana 2">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox2" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>            

                <asp:TemplateField HeaderText="Semana 3">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox3" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>            

                <asp:TemplateField HeaderText="Semana 4">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox4" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>            

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
    

    <div class="v2">
    <table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label3" runat="server" Text="Mes:"></asp:Label>
        </td>
    <td>
        <asp:DropDownList ID="dd_mes" runat="server">
            <asp:ListItem>Enero</asp:ListItem>
            <asp:ListItem>Febrero</asp:ListItem>
            <asp:ListItem>Marzo</asp:ListItem>
            <asp:ListItem>Abril</asp:ListItem>
            <asp:ListItem>Mayo</asp:ListItem>
            <asp:ListItem>Junio</asp:ListItem>
            <asp:ListItem>Julio</asp:ListItem>
            <asp:ListItem>Agosto</asp:ListItem>
            <asp:ListItem>Septiembre</asp:ListItem>
            <asp:ListItem>Octubre</asp:ListItem>
            <asp:ListItem>Noviembre</asp:ListItem>
            <asp:ListItem>Diciembre</asp:ListItem>
        </asp:DropDownList>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label4" runat="server" Text="Año:"></asp:Label>
        </td>
    <td>
        <asp:DropDownList ID="dd_anio" runat="server">
            <asp:ListItem>2017</asp:ListItem>
        </asp:DropDownList>
        </td>
    <td></td>
    <td>
        <asp:Button ID="bt_AgregarCrono" runat="server" Text="Actualizar" 
            onclick="bt_AgregarCrono_Click" />
        </td>
    <td></td>
    <td>
        <asp:Button ID="bt_buscar" runat="server" onclick="bt_buscar_Click" 
            Text="Buscar" />
        </td>
    <td></td>
    </tr>
    </table>
    </div>
    
    <br />    

     <div class="vista2">
         <asp:GridView ID="gv_datosCronogramaVisita" runat="server" BackColor="White" 
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
      
        </div>

    </div>
   
</asp:Content>
