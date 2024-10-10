<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_DeudaPlanPago.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_DeudaPlanPago" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<%@ Register TagPrefix="inmoInfo" TagName="menuIzquierdo" Src="MenuIzquierdo.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_PlanPago.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>


 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 

 <table>
 <tr>
 <td>
     <table>
         <tr>
         <td>
            <inmoinfo:menuizquierdo ID="MenuIzquierdo1"  runat="server"/>
           </td>
         </tr>
         
         <tr>
         <td style="height:100px;"></td>
         </tr>

           
     </table>
  </td> 
  <td>
  

  <table>
    <tr>
    <td>
    <div class = "titulo">
     <h1>Banderas</h1>
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
        <asp:GridView ID="gv_edificionPlanPago" runat="server" BackColor="#CCCCCC" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
            CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
            <Columns>
                <asp:TemplateField HeaderText="Plan Pago">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Servicio Suspendido">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox2" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="solo Horario Oficina">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox3" runat="server" />
                    </ItemTemplate>
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
                <asp:Button ID="bt_marcados" runat="server" Text="Marcar" Height="25px" 
                    Width="100px" onclick="bt_marcados_Click" />
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
  
  
  </td>

  </tr>
  </table>













    



</asp:Content>
