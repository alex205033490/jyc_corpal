<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_CronogramaTecnico.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_CronogramaTecnico" EnableEventValidation = "false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_CronogramaTecnico.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 11px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>
 
 <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>




  <div class="Centrar">
    <div class= "titulo">
    <h1>Cronograma Tecnicos</h1>    
    </div>
    <div class="crono1">
    <table>
    <tr>
    <td></td>
    <td>
        <asp:Label ID="Label1" runat="server" Text="Exbo :" Font-Size="Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_exbo" runat="server" Height="25px" Width="100px"></asp:TextBox>       
        </td>
    <td class="style1"></td>
    <td>
        <asp:Label ID="Label2" runat="server" Text="Edificio :" Font-Size="Small"></asp:Label>
        </td>
    <td>
        <asp:TextBox ID="tx_Edificio" runat="server" Height="25px" Width="250px"></asp:TextBox>
        </td>
    <td>
        &nbsp;</td>
     <td>
         <asp:Label ID="Label4" runat="server" Font-Size="Small" Text="Resp. Proyecto:"></asp:Label>
        </td>
     <td>
         <asp:DropDownList ID="dd_RespProyecto" runat="server" Width="150px">
         </asp:DropDownList>
        </td>
     <td></td>
    </tr>

    <tr>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
    <td>
        <asp:Label ID="Label3" runat="server" Text="Estado :" Font-Size="Small"></asp:Label>
        </td>
    <td>
        <asp:DropDownList ID="dd_estado" runat="server" Width="200px" Height="25px">
        </asp:DropDownList>
        </td>
    <td></td>
    <td>
        <asp:Label ID="Label5" runat="server" Font-Size="Small" Text="Tec. Instalador:"></asp:Label>
        </td>
    <td>
        <asp:DropDownList ID="dd_TecnicoInstalador" runat="server" Width="150px">
        </asp:DropDownList>
        </td>
    <td></td>

    </tr>


    <tr>
    <td></td>
    <td>
        &nbsp;</td>
    <td>
        <asp:Button ID="bt_faseI" runat="server" Height="25px" onclick="bt_faseI_Click" 
            Text="Calc Fase I" Width="90px" />
        </td>
    <td class="style1"></td>
    <td>&nbsp;</td>
    <td>
        <asp:Button ID="bt_faseII" runat="server" Height="25px" 
            onclick="bt_faseII_Click" Text="Calc Fase II" Width="100px" />
        </td>
    <td></td>
    <td>
     
        <asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
            onclick="bt_calcular_Click" Height="25px" />
     
        </td>
    </tr>
    </table>

    
    </div> 

    <div class="CronoEquipos">
        <asp:GridView ID="gv_equipos" runat="server" BackColor="#CCCCCC" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
            CellSpacing="2" Font-Size="X-Small" ForeColor="Black" 
            AutoGenerateColumns="False" onrowediting="gv_equipos_RowEditing" 
            onrowcancelingedit="gv_equipos_RowCancelingEdit" 
            onrowdatabound="gv_equipos_RowDataBound" onrowupdating="gv_equipos_RowUpdating" 
            onselectedindexchanged="gv_equipos_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="CodEquipo" HeaderText="CodEquipo" 
                    SortExpression="CodEquipo" />
                <asp:BoundField DataField="exbo" HeaderText="exbo" SortExpression="exbo" />
                <asp:BoundField DataField="Edificio" HeaderText="Edificio" 
                    SortExpression="Edificio" />
                <asp:BoundField DataField="Paradas" HeaderText="Paradas" 
                    SortExpression="Paradas" />
                <asp:BoundField DataField="modelo" HeaderText="modelo" 
                    SortExpression="modelo" />
                <asp:BoundField DataField="pasajero" HeaderText="pasajero" 
                    SortExpression="pasajero" />
                <asp:BoundField DataField="velocidad" HeaderText="velocidad" 
                    SortExpression="velocidad" />
                <asp:TemplateField HeaderText="fechaFaseI" 
                    SortExpression="fechaFaseI">
                    <EditItemTemplate>
                        <asp:TextBox ID="tx_fechaCronoTec" runat="server" 
                            Text='<%# Bind("fechaFaseI") %>'></asp:TextBox>
                        <asp:CalendarExtender ID="tx_fechaCronoTec_CalendarExtender" runat="server" 
                            TargetControlID="tx_fechaCronoTec">
                        </asp:CalendarExtender>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("fechaFaseI") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="cantDiasFaseI" HeaderText="cantDiasFaseI" 
                    SortExpression="cantDiasFaseI" />
                <asp:BoundField DataField="fechaConclusionFecha1" 
                    HeaderText="fechaConclusionFecha1" SortExpression="fechaConclusionFecha1" />
                <asp:TemplateField HeaderText="fechaFase2" SortExpression="fechaFase2">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fechaFase2") %>'></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
                            TargetControlID="TextBox1">
                        </asp:CalendarExtender>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("fechaFase2") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="cantDiasFaseII" HeaderText="cantDiasFaseII" 
                    SortExpression="cantDiasFaseII" />
                <asp:BoundField DataField="fechaConclusionFecha2" 
                    HeaderText="fechaConclusionFecha2" SortExpression="fechaConclusionFecha2" />
                <asp:BoundField DataField="Estado1" HeaderText="Estado1" 
                    SortExpression="Estado1" />
                <asp:BoundField DataField="Entrega Provisional (R-115)" 
                    HeaderText="Entrega Provisional (R-115)" />
                <asp:BoundField DataField="Acta Tecnico (R-117)" 
                    HeaderText="Acta Tecnico (R-117)" SortExpression="Acta Tecnico (R-117)" />
                <asp:BoundField DataField="fecha Definitiva (R-118.1)" 
                    HeaderText="fecha Definitiva (R-118.1)" 
                    SortExpression="fecha Definitiva (R-118.1)" />
                <asp:BoundField DataField="fecha Habilitacion Equipo (R-118.2)" 
                    HeaderText="fecha Habilitacion Equipo (R-118.2)" />
                <asp:BoundField DataField="fecha EntregaSegun Contrato" 
                    HeaderText="fecha EntregaSegun Contrato" 
                    SortExpression="fecha EntregaSegun Contrato" />
                <asp:BoundField DataField="ResponsableEquipo" HeaderText="ResponsableEquipo" />
                <asp:BoundField DataField="TecnicoInstalador" HeaderText="TecnicoInstalador" />
                <asp:BoundField DataField="codvariablesimec" HeaderText="codvariablesimec" 
                    SortExpression="codvariablesimec" />
            </Columns>
            <EditRowStyle BackColor="#99CC00" />
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
    
    <div class="Cronotabla">
    
        <asp:GridView ID="gv_tablaCronograma" runat="server" BackColor="#CCCCCC" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
            CellSpacing="2" Font-Size="X-Small" ForeColor="Black">
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    
    </div>
    
    <div class="excelCrono">
    <table>
    <tr>
    <td></td>
    <td>
    <asp:Button ID="bt_excel" runat="server" Height="25px" onclick="bt_excel_Click" 
            Text="Excel Equipos" Width="100px" />
    </td>
    <td></td>
    <td>
        <asp:Button ID="bt_excelFaseI" runat="server" Height="25px" 
            onclick="bt_excelFaseI_Click" Text="Excel Fase I" Width="100px" />
        </td>
    <td></td>
    <td>
        <asp:Button ID="bt_excelFaseII" runat="server" Height="25px" 
            onclick="bt_excelFaseII_Click" Text="Excel Fase II" Width="120px" />
        </td>
    <td></td>
    <td></td>
    <td></td>
    </tr>
    </table>
        
    </div>

     <div class="final">
    </div>

  </div>

</asp:Content>
