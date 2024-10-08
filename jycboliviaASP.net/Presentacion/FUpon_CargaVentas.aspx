<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaNew.Master" AutoEventWireup="true" CodeBehind="FUpon_CargaVentas.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FUpon_CargaVentas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="../Styles/Style_CargarDocumentos.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="card">
  <div class="card-header bg-warning text-black">
    Carga Ventas UponWeb
  </div>
  <ul class="list-group list-group-flush">
    <li class="list-group-item">
        <div class="row">
            <div class="col-md-12">
                <div class="datosEdificio">
<table>
    <tr>
        <td><asp:Label ID="Label2" runat="server" Text="Usuario:"></asp:Label></td>
        <td><asp:TextBox ID="tx_usuario" CssClass="form-control" runat="server"></asp:TextBox></td>
        <td><asp:Label ID="Label3" runat="server" Text="Contraseña:"></asp:Label></td>
        <td><asp:TextBox id="tx_passUsuario" class="form-control" type="password" runat="server" /></td>
    </tr>
</table>

<table>
<tr>
<td>
    <asp:Label ID="Label1" runat="server" Text="Glosa Compra:" Font-Size="Small"></asp:Label>
    <asp:TextBox ID="tx_glosaCompra" runat="server" CssClass="form-control" Width="400px"></asp:TextBox>
 </td>
<td>
    <asp:Button ID="bt_buscar" CssClass="btn btn-info" runat="server" Text="Buscar"  onclick="bt_buscar_Click" />
    </td>
</tr>
<tr>
<td>    
   <div class = "fakefile"> <asp:FileUpload ID="FileUpload1" runat="server"  
           Width="500px" BackColor="#CCCCCC"   /> </div>
</td>
<td>
    <asp:Button ID="bt_cargarDocumentos" runat="server" CssClass="btn btn-success"
        onclick="bt_cargarDocumentos_Click" Text="Cargar Archivo" />
    </td>
</tr>
</table>

<div class="col-md-12 offset-1" >
    <asp:Button ID="bt_eliminar" CssClass="btn btn-danger" runat="server" Text="Eliminar" OnClick="bt_eliminar_Click" />
    <asp:Button ID="bt_vaciarUpon" CssClass="btn btn-success" runat="server" Text="Vaciar Upon" OnClick="bt_vaciarUpon_Click" />
  
</div>

</div>
            </div>
        </div>
    </li>
    <li class="list-group-item">
        <div class="row">
            <div class="col-md-12">
                <div class="edificios">
    <asp:GridView ID="gv_ventasCargadas" runat="server" BackColor="#CCCCCC"
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4"
        CellSpacing="2" Font-Size="XX-Small" ForeColor="Black">
        
        <Columns>
            <asp:TemplateField HeaderText="Eliminar">
                <ItemTemplate>
                    <asp:CheckBox ID="cbk_Eliminar" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="VaciadoUpon">
                <ItemTemplate>
                    <asp:CheckBox ID="cbk_VaciarUpon" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
        <EditRowStyle BackColor="#33CC33" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#33CC33" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="Gray" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
        </div>
            </div>
        </div>
    </li>
    <li class="list-group-item">
        <div class="row">
            <div class="col-md-12">            
            </div>
        </div>
    </li>
  </ul>
</div>


</asp:Content>
