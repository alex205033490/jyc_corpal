<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FProyecto.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FProyecto" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <style type="text/css">
        .style1
        {
           height: 27px;
       }
        .style2
        {
           height: 44px;
       }
       .style3
       {
           width: 156px;
       }
       .style4
       {
           height: 27px;
           width: 156px;
       }
       .style6
       {
           height: 44px;
           width: 134px;
       }
       .style7
       {
           height: 21px;
       }
       .style8
       {
           width: 156px;
           height: 21px;
       }
    </style>
    <link href="../Styles/Style_GProyecto.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"  >
    </asp:ScriptManager>
    

    <div class="Centrar">
    <div class="titulo"><h3>GESTIONAR EDIFICIO</h3></div>
    <div class="GP1">
    <table style="width: 565px; height: 86px; margin-left: 13px;" >
            <tr>
                <td>
                    
                    <asp:Label ID="Label2" runat="server" Text="Nombre :" Font-Size="X-Small"></asp:Label>
                    
                </td>
                <td>
                </td>
                <td>
                    &nbsp;<asp:Label ID="Label3" runat="server" Text="Direccion :" Font-Size="X-Small"></asp:Label>
                </td>
                <td>
                </td>
                <td class="style3">
                    
                    <asp:Label ID="Label4" runat="server" Text="Encargado de Pago :" 
                        Font-Size="X-Small"></asp:Label>
                    
                </td>
                <td>
                </td>
              
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txNombre" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="EL NOMBRE DEL PROYECTO ES OBLIGATORIO"
                        ForeColor="Red" ControlToValidate="txNombre">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txDireccion" runat="server"></asp:TextBox>
                </td>
                <td>                    
                </td>
                <td class="style3">
                    <asp:DropDownList ID="ddlEncargadoPago" runat="server" 
                        Font-Size="Small" Height="20px" Width="129px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    
                     <asp:Label ID="lbzona" runat="server" Text="Zona :" Font-Size="X-Small"></asp:Label>
                    
                    </td>
                <td class="style7">
                </td>
                <td class="style7">
                     <asp:Label ID="Label5" runat="server" Font-Size="X-Small" Text="Departamento :"></asp:Label>
                </td>
                <td class="style7">
                </td>
                <td class="style8">
                    &nbsp;</td>
                <td class="style7">
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:DropDownList ID="dd_Zona" runat="server" Height="20px" Width="130px">
                    </asp:DropDownList> 
                </td>
                <td class="style1">
                </td>
                <td class="style1">
                    <asp:DropDownList ID="dd_departamento" runat="server" Height="20px" 
                        Width="133px">
                        <asp:ListItem>Santa Cruz</asp:ListItem>
                        <asp:ListItem>Cochabamba</asp:ListItem>
                        <asp:ListItem>La Paz</asp:ListItem>
                        <asp:ListItem>Beni</asp:ListItem>
                        <asp:ListItem>Potosi</asp:ListItem>
                        <asp:ListItem>Oruro</asp:ListItem>
                        <asp:ListItem>Tarija</asp:ListItem>
                        <asp:ListItem>Chuquisaca</asp:ListItem>
                        <asp:ListItem>Pando</asp:ListItem>
                        <asp:ListItem>Asuncion-Paraguay</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style1">
                </td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style1">
                </td>
            </tr>
           
            </table>
            <table style="width: 485px; margin-left: 59px">
            <tr>
            <td class="style6">
                    <asp:Button ID="btnNuevo" runat="server" Text="Limpiar" 
                    CausesValidation="False" OnClick="btnNuevo_Click" Height="25px" 
                        Width="85px" Font-Size="Small" />
                </td>
            <td class="style2">
                    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" 
                    OnClick="btnRegistrar_Click" Height="25px" Width="85px" 
                        OnClientClick="return confirm('Desea Registrar?')" Font-Size="Small" />
                </td>
            <td class="style2">
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" 
                    OnClick="btnModificar_Click" Height="25px" Width="85px" Font-Size="Small" />
                </td>
            <td class="style2">
                <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" Height="25px" 
                    Width="85px" Font-Size="Small" onclick="BtnBuscar_Click" />
                </td>
            </tr>
        </table>

    </div>
    
    <div class="GP2">
        <asp:GridView ID="gv_Proyecto" runat="server" BackColor="#CCCCCC" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
            CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
            onrowdeleting="gv_Proyecto_RowDeleting" 
            onselectedindexchanged="gv_Proyecto_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
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
    <div class = "GP1_1"></div>

    </div>




    
</asp:Content>
