<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FEncargadoPago.aspx.cs" Inherits="jycboliviaASP.net.FEncargadoPago" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_GEncargadoPago.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            height: 17px;
        }
        .style5
        {
            height: 17px;
            width: 238px;
        }
        .style6
        {
            width: 12px;
            height: 18px;
        }
        .style9
        {
            width: 13px;
            height: 17px;
        }
        .style10
        {
            width: 226px;
            height: 16px;
        }
        .style11
        {
            width: 226px;
            height: 17px;
        }
        .style17
        {
            height: 16px;
        }
        .style18
        {
            height: 16px;
            width: 238px;
        }
        .style19
        {
            height: 16px;
            width: 10px;
        }
        .style20
        {
            height: 25px;
        }
        .style21
        {
            height: 25px;
            width: 238px;
        }
        .style22
        {
            height: 25px;
            width: 10px;
        }
        .style25
        {
            height: 4px;
            width: 10px;
        }
        .style26
        {
            height: 4px;
        }
        .style27
        {
            width: 238px;
            height: 4px;
        }
        .style28
        {
            height: 18px;
            width: 238px;
        }
        .style30
        {
            height: 25px;
            width: 226px;
        }
        .style31
        {
            height: 4px;
            width: 226px;
        }
        .style32
        {
            height: 18px;
            width: 226px;
        }
        .style38
        {
            height: 31px;
            width: 233px;
        }
        .style44
        {
            width: 10px;
            height: 17px;
        }
        .style45
        {
            width: 10px;
            height: 18px;
        }
        .style46
        {
            height: 16px;
            width: 9px;
        }
        .style47
        {
            width: 9px;
            height: 17px;
        }
        .style48
        {
            height: 25px;
            width: 9px;
        }
        .style49
        {
            height: 4px;
            width: 9px;
        }
        .style50
        {
            width: 9px;
            height: 18px;
        }
        .style51
        {
            height: 16px;
            width: 13px;
        }
        .style52
        {
            height: 25px;
            width: 13px;
        }
        .style53
        {
            height: 4px;
            width: 13px;
        }
        .style54
        {
            width: 13px;
            height: 18px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>

   <div class="Centrar">
   <div class="titulo">
   <h3>GESTIONAR ENCARGADO DE PAGO</h3>
   </div>

<div class ="GEP1">
<table >
    <tr>
    <td class="style17">
        <asp:Label ID="Label4" runat="server" Text="NOMBRE" Font-Size="X-Small"></asp:Label>
    <label for="textfield"></label>
    </td>
    <td class="style51">
    </td>
    <td class="style18"> 
        <asp:Label ID="Label5" runat="server" Text="CARNET" Font-Size="X-Small"></asp:Label>
        </td>
    <td class="style19">
    </td>
    <td class="style46">
        <asp:Label ID="Label6" runat="server" Text="TELEFONO" Font-Size="X-Small"></asp:Label>
        </td>
    <td class="style10">
    </td>

    </tr>
    <tr>
    <td class="style9" ><asp:TextBox ID="tx_nombre" runat="server" Width="223px"></asp:TextBox>
    </td>
    <td class="style9">
        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
            ControlToValidate="tx_nombre" Display="Dynamic" ErrorMessage="NO ES UN NOMBRE" 
            ForeColor="#FF3300" ValidationExpression="^[^ ][a-zA-Z ]+[^ ]$">*</asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="tx_nombre" Display="Dynamic" 
            ErrorMessage="CAMPO NOMBRE OBLIGATORIO" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
    </td>
    <td class="style5"> 
    <asp:TextBox ID="tx_ci" runat="server" Width="140px" MaxLength="10" 
            style="margin-left: 0px" ></asp:TextBox>
    <asp:DropDownList ID="dList_Ciudad" runat="server">
        <asp:ListItem>SC</asp:ListItem>
        <asp:ListItem>BN</asp:ListItem>
        <asp:ListItem>PN</asp:ListItem>
        <asp:ListItem>LP</asp:ListItem>
        <asp:ListItem>OR</asp:ListItem>
        <asp:ListItem>PT</asp:ListItem>
        <asp:ListItem>CBBA</asp:ListItem>
        <asp:ListItem>CH</asp:ListItem>
        <asp:ListItem>TJ</asp:ListItem>
        <asp:ListItem>OTRO</asp:ListItem>
    </asp:DropDownList>
    </td>
    <td class="style44">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="tx_ci" ErrorMessage="Campo Carnet Obligatorio" 
            ForeColor="#FF3300">*</asp:RequiredFieldValidator>
    </td>
    <td class="style47">
    <asp:TextBox ID="tx_telefono" runat="server" MaxLength="15"></asp:TextBox>
    </td>
    <td class="style11">
            &nbsp;
    </td>
    </tr>
    <tr>
    <td class="style2"> 
        <asp:Label ID="Label7" runat="server" Text="DIRECCION" Font-Size="X-Small"></asp:Label>
        </td>
    <td class="style9">
    </td>
    <td class="style5"> 
        <asp:Label ID="Label8" runat="server" Text="MAIL" Font-Size ="X-Small"></asp:Label>
    </td>
    <td class="style44">
    </td>
    <td class="style47">  
        <asp:Label ID="Label9" runat="server" Text="CELULAR" Font-Size="X-Small"></asp:Label>
        </td> <td class="style11">
    </td>    
    </tr>
    <tr>
    <td class="style20">
    <asp:TextBox ID="tx_direccion" runat="server" Width="223px"></asp:TextBox> 
    </td>
    <td class="style52">
        </td>
    <td class="style21">
    <asp:TextBox ID="tx_email" runat="server" Width="237px"></asp:TextBox>
    </td>
    <td class="style22">
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ControlToValidate="tx_email" Display="Dynamic" 
            ErrorMessage="NO ES UN MAIL VALIDO" ForeColor="#FF3300" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
    </td>
    <td class="style48">
    <asp:TextBox ID="tx_celular" runat="server" MaxLength ="8" Height="19px" 
            Width="141px" ></asp:TextBox>
        </td>
    <td class="style30">
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="tx_celular" Display="Dynamic" 
            ErrorMessage="NUMERO DE CELULAR NO VALIDO" ForeColor="#FF3300" 
            ValidationExpression="(\(\d{3}\)|\d{3}-)?\d{8}">*</asp:RegularExpressionValidator>
    </td>
    </tr>
    <tr>
    <td class="style26"> 
        <asp:Label ID="Label3" runat="server" Text="FACTURAR A" Font-Size="X-Small"></asp:Label>
        </td>
    <td class="style53"></td>
    <td class="style27">
        <asp:Label ID="Label2" runat="server" Text="NIT" Font-Size="X-Small"></asp:Label>
        </td>
    <td class="style25"></td>
    <td class="style49">
        </td>
    <td class="style31"></td>
    </tr>
    <tr>
    <td class="style6">
    <asp:TextBox ID="tx_facturar_A" runat="server" Width="180px"></asp:TextBox>
        </td>
    <td class="style54"></td>
    <td class="style28">
    <asp:TextBox ID="tx_nit" runat="server" Width="116px" MaxLength="20" ></asp:TextBox> 
        </td>
    <td class="style45">
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
            ControlToValidate="tx_nit" Display="Dynamic" 
            ErrorMessage="NIT NO VALIDO" ForeColor="#FF3300" 
            ValidationExpression="^[0-9]+$">*</asp:RegularExpressionValidator>
        </td>
    <td class="style50">
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
            ShowMessageBox="True" ShowSummary="False" Width="148px" />
        </td>
    <td class="style32"></td>
    </tr>

    <tr>
    <td>
        <asp:Label ID="Label10" runat="server" Font-Size="Small" Text="Banco :"></asp:Label>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label11" runat="server" Font-Size="Small" Text="Observacion :"></asp:Label>
        </td>
    <td></td>
    <td></td>
    <td></td>
    </tr>
    <tr>
    <td>
        <asp:TextBox ID="tx_banco" runat="server" Width="180px"></asp:TextBox>
        </td>
    <td></td>
    <td>
        <asp:TextBox ID="tx_Observacion" runat="server" Height="75px" 
            TextMode="MultiLine" Width="242px"></asp:TextBox>
        </td>
    <td></td>
    <td></td>
    <td></td>
    </tr>

   </table>
<table style="width: 607px; margin-left: 72px; margin-top: 7px;">
<tr>
<td> 
    <asp:Button ID="btnNuevo" runat="server" onclick="btn_Nuevo" Text="Limpiar" 
            Width="75px" CausesValidation="False" Height="26px" />


    </td>
<td>


    <asp:Button ID="btnRegistrar" runat="server" onclick="btn_Registrar" 
            Text="Registrar" Width="75px" ForeColor="Black" Height="25px" />  
    </td>
<td>  
    <asp:Button ID="btnModificar" runat="server" onclick="btn_Modificar" Text="Modificar" 
                Width="75px" Height="25px" />
    </td>
<td>
    <asp:Button ID="btnBuscar" runat="server" onclick="btn_Buscar" Text="Buscar" 
            CausesValidation="False" Height="25px" />
                </td>
<td></td>
</tr>
</table>
</div>


   
<div class="GEP2"> 

        <asp:GridView ID="GridView1" runat="server" BackColor="#CCCCCC" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
            CellSpacing="2" DataKeyNames="codigo" ForeColor="Black" 
            OnRowDeleting="GridView1_RowDeleting" 
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowPaging="True" 
                OnPageIndexChanging="GridView1_PageIndexChanging" Height="16px" 
            style="margin-left: 3px; margin-top: 1px" Width="537px" 
            Font-Size="X-Small">
            <Columns>
                <asp:CommandField ShowSelectButton="True" 
                    SelectImageUrl="../Images/select.png" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lkEliminar" runat="server" CausesValidation="False" 
                            CommandName="Delete" OnClientClick="return confirm('Esta seguro de eliminar?')" 
                            Text="Eliminar"> </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#33CC33" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>

    </div>

    <div class="GEP3"></div> 








   
   </div>









</asp:Content>
