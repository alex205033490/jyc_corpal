<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FCorpal_GestonarResponsable.aspx.cs" Inherits="jycboliviaASP.net.WebForm2" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Styles_GestionarResp.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 144px;
        }
        #tx_Password
        {
            margin-left: 0px;
        }
        .style2
        {
            width: 224px;
        }
        .style3
        {
            width: 217px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  

<div class = "Centrar">
<div class = "titulo"><h1> Gestionar Responsable</h1>   </div>
<div class ="GR1">
        <table style="width: 897px">        
        <tr>
        <td class="style1">
            <asp:Label ID="Label1" runat="server" Text="Nombre :" Font-Size="X-Small"></asp:Label>
            </td>
        <td class="style2">
            <asp:Label ID="Label2" runat="server" Text="Direccion :" Font-Size="X-Small"></asp:Label>
            </td>  
            <td class="style3">
            <asp:Label ID="Label3" runat="server" Text="Telefono :" Font-Size="X-Small"></asp:Label>
            </td>      
            <td>
            <asp:Label ID="Label4" runat="server" Text="Celular :" Font-Size="X-Small"></asp:Label>
            </td>      
        </tr>
        <tr>
        <td class="style1">
            <asp:TextBox ID="tx_nombre" runat="server" Width="200px" 
                style="margin-right: 27px"></asp:TextBox></td>
        <td class="style2">
            <asp:TextBox ID="tx_direccion" runat="server" Width="200px"></asp:TextBox></td>  
            <td class="style3">
            <asp:TextBox ID="tx_telefono" runat="server" Width="200px"></asp:TextBox></td>      
            <td>
            <asp:TextBox ID="tx_celular" runat="server" Width="200px"></asp:TextBox></td>      
        </tr>
        <tr>
        <td class="style1">
            <asp:Label ID="Label5" runat="server" Text="Email :" Font-Size="X-Small"></asp:Label>
            </td>
        <td class="style2">
            <asp:Label ID="Label6" runat="server" Text="Departamento :" Font-Size="X-Small"></asp:Label>
            </td>  
            <td class="style3">
            <asp:Label ID="Label7" runat="server" Text="Ciudad :" Font-Size="X-Small"></asp:Label>
            </td>      
            <td>
            <asp:Label ID="Label8" runat="server" Text="Cargo :" Font-Size="X-Small"></asp:Label>
            </td>      
        </tr>
        <tr>
        <td class="style1">
            <asp:TextBox ID="tx_email" runat="server" Width="200px"></asp:TextBox></td>
        <td class="style2">
                  <asp:DropDownList ID="cb_dpto" runat="server" Height="25px" Width="200px">
                    <asp:ListItem>Santa Cruz</asp:ListItem>
                    <asp:ListItem>Cochabamba</asp:ListItem>
                    <asp:ListItem>La Paz</asp:ListItem>
                    <asp:ListItem>Chuquisaca</asp:ListItem>
                    <asp:ListItem>Beni</asp:ListItem>
                    <asp:ListItem>Pando</asp:ListItem>
                    <asp:ListItem>Oruro</asp:ListItem>
                    <asp:ListItem>Potosi</asp:ListItem>
                    <asp:ListItem>Tarija</asp:ListItem>
                      <asp:ListItem>Asuncion</asp:ListItem>
                </asp:DropDownList>
            </td>    
            <td class="style3">
            <asp:TextBox ID="tx_ciudad" runat="server" Width="200px"></asp:TextBox></td>      
            <td>            
                <asp:DropDownList ID="cb_cargo" runat="server" Height="27px" Width="200px">
                </asp:DropDownList>
            </td>    
        </tr>
        <tr>
        <td class="style1">
            <asp:Label ID="Label9" runat="server" Text="Usuario :" Font-Size="X-Small"></asp:Label>
            </td>
        <td class="style2">
            <asp:Label ID="Label10" runat="server" Text="PassWord :" Font-Size="X-Small"></asp:Label>
            </td>     
            <td class="style3"></td>      
            <td></td>   
        </tr>
        <tr>
        <td class="style1">
            <asp:TextBox ID="tx_usuario" runat="server" Width="200px"></asp:TextBox></td>
        <td class="style2">
            <input id="tx_Password" type="password" runat="server" /></td>    
            <td class="style3"></td>      
            <td></td>    
        </tr>
        <tr>
        <td class="style1">
                <asp:Button ID="bt_insertar" runat="server" Text="Insertar" 
                    onclick="Button1_Click" Width="70px" Height="35px" 
                    style="margin-left: 60px" />
            </td>
        <td class="style2">
            <asp:Button ID="bt_modificar" runat="server" Text="Modificar" Height="35px" 
                    onclick="Button2_Click" style="margin-left: 60px" Width="70px" />         
            </td>   
            <td class="style3">  
                <asp:Button ID="bt_limpiar" runat="server" Text="Limpiar" Height="35px" 
                    onclick="Button4_Click" style="margin-left: 60px" Width="70px" />
                </td>      
            <td>
                <asp:Button ID="bt_buscar" runat="server" Height="35px" 
                    onclick="bt_buscar_Click" Text="Buscar" style="margin-left: 60px" 
                    Width="70px" />
            </td>     
        </tr>
                   
        </table>        
        </div>



     <div class="GR2">
           <asp:GridView ID="GridView1" runat="server"    BackColor="#CCCCCC" 
               BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
               CellSpacing="2" ForeColor="Black" 
               onselectedindexchanged="GridView1_SelectedIndexChanged1" 
               AllowPaging="True" Font-Size="X-Small" 
               onpageindexchanging="GridView1_PageIndexChanging" PageSize="15" Width="900px">
               <FooterStyle BackColor="#CCCCCC" />
               <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
               <RowStyle BackColor="White" />
               <SelectedRowStyle BackColor="#00CC00" Font-Bold="True" ForeColor="White" />
               <SortedAscendingCellStyle BackColor="#F1F1F1" />
               <SortedAscendingHeaderStyle BackColor="#808080" />
               <SortedDescendingCellStyle BackColor="#CAC9C9" />
               <SortedDescendingHeaderStyle BackColor="#383838" />
               <Columns>
                   <asp:CommandField SelectText="Editar" ShowSelectButton="True" />
</Columns>
<Columns>
   <asp:TemplateField>
      <ItemTemplate>
         <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="False"></asp:CheckBox>
      </ItemTemplate>
   </asp:TemplateField>
</Columns>
           </asp:GridView>       
       </div>



<div class="botonEliminar">
       <asp:Button ID="bt_eliminar" runat="server"   
       Text="Eliminar Seleccionados" onclick="Button3_Click" Height="35px"  />
      </div>

      <div class="espacioBlanco"></div>
</div>






       
</asp:Content>
