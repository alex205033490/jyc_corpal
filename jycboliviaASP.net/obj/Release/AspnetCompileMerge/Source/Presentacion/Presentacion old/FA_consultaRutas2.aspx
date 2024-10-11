<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FA_consultaRutas2.aspx.cs" Inherits="jycboliviaASP.net.Presentacion.FA_consultaRutas" %>
<%@ Register TagPrefix="inmoInfo" TagName="menu" Src="ControlUser.ascx" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Style_consultaRutasporDatos.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
          .CompletionList
        {
            padding: 5px 0 ;
            margin: 2px 0 0;            
          /*  position:absolute;  */
            height:150px;
            width:200px;
            background-color: White;
            cursor: pointer;
            border: solid ;  
            border-width: 1px;    
            font-size:x-small;
            overflow: auto;
                        }
                        
           .CompletionlistItem
           {
               font-size:x-small;           
            }             
                        
        .CompletionListMighlightedItem
        {
             background-color: Green;
             color: White;
            /* color: Lime;
           padding: 3px 20px;
            text-decoration: none;           
            background-repeat: repeat-x;
            outline: 0;*/            
            }     
        
        </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class = "menu">  
       <inmoInfo:menu ID="Menu1" runat="server"/>
   </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

<div class="centrar">
<div class="titulo">
<h3> 
    <asp:Label ID="Label1" runat="server" Text="Consulta Rutas"></asp:Label> </h3>
</div>

<table style="margin: 0 auto;">
<tr>
<td>
    <table>
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label8" runat="server" Font-Size="Small">CodRuta:</asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_codRuta" runat="server" Width="50px"></asp:TextBox>
            </td>
        <td></td>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Edificio:" Font-Size="Small"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_Edificio" runat="server" Width="200px"></asp:TextBox>
            <asp:AutoCompleteExtender ID="tx_Edificio_AutoCompleteExtender" runat="server" TargetControlID="tx_Edificio"
            Enabled="True" BehaviorID="AutoCompleteEx" CompletionInterval="200"
                    ServicePath="" servicemethod="GetlistaProyectos"
                    minimumprefixlength="2"  DelimiterCharacters="" enablecaching="true"                     
                    completionsetcount="30" 
                    ShowOnlyCurrentWordInCompletionListItem="True" 
                    CompletionListCssClass="CompletionList" 
            CompletionListItemCssClass="CompletionlistItem" 
            CompletionListHighlightedItemCssClass="CompletionListMighlightedItem"
            >
            </asp:AutoCompleteExtender>
            

            </td>
        <td></td>
        <td>
            <asp:Button ID="bt_Buscar" runat="server" Text="Buscar" 
                onclick="bt_Buscar_Click1" />
            </td>
        <td></td>
        
        </tr>

        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Exbo:" Font-Size="Small"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_Exbo" runat="server" Width="120px"></asp:TextBox>
            </td>
        <td></td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td></td>
        <td>
            &nbsp;</td>
        
        <td></td>
        </tr>
        <tr>
        <td></td>
        <td>
            <asp:Label ID="Label9" runat="server" Font-Size="Small" Text="Mes:"></asp:Label>
            </td>
        <td>
            <asp:DropDownList ID="dd_mes" runat="server" Width="100px">
                <asp:ListItem Value="1">Enero</asp:ListItem>
                <asp:ListItem Value="2">Febrero</asp:ListItem>
                <asp:ListItem Value="3">Marzo</asp:ListItem>
                <asp:ListItem Value="4">Abril</asp:ListItem>
                <asp:ListItem Value="5">Mayo</asp:ListItem>
                <asp:ListItem Value="6">Junio</asp:ListItem>
                <asp:ListItem Value="7">Julio</asp:ListItem>
                <asp:ListItem Value="8">Agosto</asp:ListItem>
                <asp:ListItem Value="9">Septiembre</asp:ListItem>
                <asp:ListItem Value="10">Octubre</asp:ListItem>
                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                <asp:ListItem Value="12">Diciembre</asp:ListItem>
            </asp:DropDownList>
            </td>
        <td></td>
        <td>
            <asp:Label ID="Label10" runat="server" Font-Size="Small" Text="Año:"></asp:Label>
            </td>
        <td>
            <asp:TextBox ID="tx_year" runat="server" Width="100px"></asp:TextBox>
            </td>
        <td></td>
        <td>
            &nbsp;</td>       
        <td></td>
        </tr>

   

    </table>
</td>
</tr>

<tr>
<td>
<div class="datosEdificio">
<table>
<tr>
<td></td>
<td>
    &nbsp;</td>
<td>    
   <div class = "fakefile"> 
   <asp:FileUpload ID="FileUpload1" runat="server" Width="500px" BackColor="#CCCCCC"   /> </div>
    </td>
<td></td>
<td>
    <asp:Button ID="bt_cargarDocumentos" runat="server" 
        onclick="bt_cargarDocumentos_Click" Text="Cargar" />
    </td>
<td></td>
</tr>
</table>

</div>
</td>
</tr>

<tr>
<td>
<div class="tabla">

    <asp:GridView ID="gv_tabla" runat="server" BackColor="#CCCCCC" 
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
</td>
</tr>

<tr>
<td>

    <asp:Label ID="Label7" runat="server" Font-Size="Small" 
        Text="Cantidad:"></asp:Label>
    <asp:TextBox ID="tx_cantidad" runat="server" Enabled="False"></asp:TextBox>

</td>
</tr>

<tr>
<td>

    <asp:Button ID="bt_excel" runat="server" Height="25px" onclick="bt_excel_Click" 
        style="margin-left: 24px" Text="Excel" Width="120px" />

</td>
</tr>
<tr>
<td>

</td>
</tr>

</table>

</div>
</asp:Content>
