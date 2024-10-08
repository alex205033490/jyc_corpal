<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true"
    CodeBehind="FCorpal_Actualizacion.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FActualizacion" EnableEventValidation ="false" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_GActualizacion.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style5
        {
            height: 43px;
            width: 268px;
        }
        .style11
        {
            height: 43px;
            width: 36px;
        }
        .style13
        {
            height: 43px;
            width: 16px;
        }
        .style17
        {
            height: 30px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>


   <div class="Centrar">
   <div class="titulo"><h1> Gestionar Actualizacion </h1></div>

<div class = "GA1" >
      <table style="height: 58px; width: 308px; margin-left: 14px;">
            <tr>
                <td class="style11">
                    <asp:Label ID="Label2" runat="server" Text="Nombre:" />
                </td>
                <td class="style5">
                    <asp:TextBox ID="txtNombre" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="style13">
                    &nbsp;</td>
            </tr>
            
        </table>
        <table style="width: 305px; margin-left: 20px">
        <tr>
        <td class="style17">
                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" Width="50px" 
                        onclick="btnNuevo_Click" Height="25px" />
                    </td>
        <td class="style17">
                    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" 
                        OnClick="btnRegistrar_Click" Height="25px" 
                        OnClientClick = "return confirm('¿Esta seguro de Registrar?')" 
                        Width="70px" />
                    </td>
        <td class="style17">
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" 
                          onclick="btnModificar_Click" Height="25px" Width="70px" 
                        OnClientClick = "return confirm('¿Esta seguro de Actualizar el dato?')"  />
                </td>
         <td class="style17">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                           Height="25px" Width="58px" onclick="btnBuscar_Click" />
                </td>
        </tr>
        </table>
        </div>

<div class ="GA2" >
    <asp:GridView ID="gvActualizacion" runat="server" BackColor="#CCCCCC" BorderColor="#999999"
        BorderStyle="Solid" BorderWidth="3px" CellPadding="4" DataKeyNames="codigo" 
        onrowdeleting="GridView1_RowDeleting" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" CellSpacing="2" 
        ForeColor="Black" Width="218px" AllowPaging="True" 
                onpageindexchanging="GridView1_PageIndexChanging" Height="16px" 
                style="margin-left: 58px; margin-top: 0px;" Font-Size="X-Small">
        <Columns>
            <asp:CommandField ShowSelectButton="True" 
                SelectImageUrl="../Images/select.png" />
      <asp:TemplateField><ItemTemplate>
                 <asp:LinkButton ID="lkEliminar" CommandName="Delete" runat="server" 
                     Text = "Eliminar" OnClientClick = "return confirm('Esta seguro de eliminar?')" 
                     CausesValidation="False" ForeColor="Black"> </asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#33CC33" Font-Bold="True" ForeColor="Black" 
            BorderColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
        </div>

   
   </div>


   



   
</asp:Content>
